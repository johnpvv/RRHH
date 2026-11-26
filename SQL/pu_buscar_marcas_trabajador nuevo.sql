USE [rrhhloes_]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create or ALTER PROCEDURE [dbo].[PU_BUSCAR_TRABAJADORES_MARCAS]
    @ANIO INT,
    @MES INT,
    @BUSQUEDA VARCHAR(100) = '',
    @CENTRO INT
AS
/*************************************************************************************************************
	NOMBRE: PU_BUSCAR_TRABAJADORES_MARCAS
	CREADOR: [John Vaccarella V.]
	FECHA CREACIÓN: 27/10/2026

	DESCRIPCIÓN:
	Busca trabajadores con marcaciones por centro y período, identifica anomalías en las marcas, problemas
	de identificación y marcas realizadas en otros centros.

	OPTIMIZACIÓN:
	Materializa resultados intermedios en tablas temporales para evitar la reevaluación de CTE y facilitar 
	las agrupaciones.

	PARÁMETROS:
	@ANIO      Año
	@MES       Mes
	@BUSQUEDA  Texto de búsqueda
	@CENTRO    Centro seleccionado

	PRUEBAS:
	EXEC dbo.PU_BUSCAR_TRABAJADORES_MARCAS 2026, 10, '', 1;

*************************************************************************************************************/
BEGIN
    SET NOCOUNT ON;
    DECLARE @FECHA_INICIO DATE = DATEFROMPARTS(@ANIO, @MES, 1);
    DECLARE @FECHA_FIN DATE = DATEADD(MONTH, 1, @FECHA_INICIO);
    SET @BUSQUEDA = LTRIM(RTRIM(ISNULL(@BUSQUEDA, '')));

    /*=========================================================
      1. BASE
    =========================================================*/

    SELECT
        M.IDMARCACION,
        M.CODIGO_EMP_RELOJ,
        M.IDRELOJ,
        M.F_H_MARCA,
        M.TIPO_MARCA,
        R.CODUNIOP AS CENTRO_RELOJ,
        UR.IDUSRRELOJ,
        UR.IDUSUARIO,
        U.RUT,
        U.DV,
        CONVERT(VARCHAR(20), U.RUT) + '-' + U.DV AS RUT_C,
        U.NOMBRE,
        U.AP_PATERNO,
        U.AP_MATERNO,
        CASE
            WHEN UR.IDUSRRELOJ IS NULL THEN 'SIN EQUIVALENCIA'
            WHEN U.IDUSUARIO IS NULL THEN 'PENDIENTE RRHH'
            ELSE 'REGISTRADO RRHH'
        END AS ESTADO_RRHH,
        CASE
            WHEN UR.IDUSUARIO IS NULL
                THEN 'R:' + M.CODIGO_EMP_RELOJ
            ELSE 'U:' + CAST(UR.IDUSUARIO AS VARCHAR(20))
        END AS TRABAJADOR_KEY
    INTO #BASE
    FROM dbo.M_MARCACIONES M
    LEFT JOIN dbo.M_RELOJES R ON R.IDRELOJ = M.IDRELOJ

    OUTER APPLY
    (
        SELECT TOP 1
            UR1.IDUSRRELOJ,
            UR1.IDUSUARIO
        FROM dbo.M_USR_RELOJ UR1
        WHERE UR1.IDUSRELOJ = M.CODIGO_EMP_RELOJ AND ISNULL(UR1.IDESTADO, 1) <> 3
        ORDER BY
            CASE WHEN UR1.IDUSUARIO IS NOT NULL THEN 0 ELSE 1 END,
            UR1.IDUSRRELOJ DESC
    ) UR
    LEFT JOIN dbo.M_USUARIOS U ON U.IDUSUARIO = UR.IDUSUARIO
    WHERE M.F_H_MARCA >= @FECHA_INICIO
      AND M.F_H_MARCA < @FECHA_FIN
      AND ISNULL(M.IDESTADO, 1) <> 3;

    /*=========================================================
      2. ÍNDICES TEMPORALES BASE
    =========================================================*/

    CREATE INDEX IX_BASE_IDUSUARIO ON #BASE (IDUSUARIO);
    CREATE INDEX IX_BASE_CENTRO ON #BASE (CENTRO_RELOJ);
    CREATE INDEX IX_BASE_TRABAJADOR ON #BASE (TRABAJADOR_KEY);

    /*=========================================================
      3. TRABAJADORES ELEGIBLES
    =========================================================*/

    SELECT DISTINCT B.IDUSUARIO
    INTO #ELEGIBLES
    FROM #BASE B
    WHERE B.IDUSUARIO IS NOT NULL
      AND
      (
          B.CENTRO_RELOJ = @CENTRO
          OR EXISTS
          (
              SELECT 1
              FROM dbo.M_USER_UNIDAD UU
              WHERE UU.IDUSUARIO = B.IDUSUARIO
                AND UU.CODUNIOP = @CENTRO
                AND ISNULL(UU.IDESTADO, 1) <> 3
          )
      );
    CREATE UNIQUE CLUSTERED INDEX IX_ELEGIBLES ON #ELEGIBLES (IDUSUARIO);

    /*=========================================================
      4. MARCAS SELECCIONADAS
    =========================================================*/

    SELECT B.*
    INTO #MARCAS
    FROM #BASE B
    WHERE
    (
        (
            B.IDUSUARIO IS NOT NULL
            AND EXISTS
            (
                SELECT 1
                FROM #ELEGIBLES E
                WHERE E.IDUSUARIO = B.IDUSUARIO
            )
        )
        OR
        (
            B.IDUSUARIO IS NULL
            AND B.CENTRO_RELOJ = @CENTRO
        )
    )

    AND
    (
        @BUSQUEDA = ''
        OR CAST(B.RUT AS VARCHAR(20)) LIKE '%' + @BUSQUEDA + '%'
        OR B.NOMBRE LIKE '%' + @BUSQUEDA + '%'
        OR B.AP_PATERNO LIKE '%' + @BUSQUEDA + '%'
        OR B.AP_MATERNO LIKE '%' + @BUSQUEDA + '%'
        OR B.CODIGO_EMP_RELOJ LIKE '%' + @BUSQUEDA + '%'
    );

    /*=========================================================
      5. ÍNDICES TEMPORALES MARCAS
    =========================================================*/

    CREATE INDEX IX_MARCAS_TRABAJADOR_FECHA ON #MARCAS (TRABAJADOR_KEY, F_H_MARCA);
    CREATE INDEX IX_MARCAS_IDUSUARIO ON #MARCAS (IDUSUARIO);

    /*=========================================================
      6. CONTEO DIARIO
    =========================================================*/

    SELECT
        TRABAJADOR_KEY,
        CAST(F_H_MARCA AS DATE) AS FECHA,
        SUM(CASE WHEN TIPO_MARCA = 0 THEN 1 ELSE 0 END) AS ENTRADAS,
        SUM(CASE WHEN TIPO_MARCA = 1 THEN 1 ELSE 0 END) AS SALIDAS

    INTO #CONTEO_DIAS
    FROM #MARCAS
    GROUP BY
        TRABAJADOR_KEY,
        CAST(F_H_MARCA AS DATE);

    CREATE UNIQUE CLUSTERED INDEX IX_CONTEO_DIAS ON #CONTEO_DIAS (TRABAJADOR_KEY, FECHA);

    /*=========================================================
      7. ESTADOS
    =========================================================*/

    SELECT
        M.TRABAJADOR_KEY,
        M.IDMARCACION,
        CASE
            WHEN C.ENTRADAS = 1 AND C.SALIDAS = 1
                THEN 'OK'
            WHEN C.ENTRADAS > 1 AND M.TIPO_MARCA = 0
                THEN 'ENTRADA REPETIDA'
            WHEN C.SALIDAS > 1 AND M.TIPO_MARCA = 1
                THEN 'SALIDA REPETIDA'
            WHEN C.ENTRADAS = 1 AND C.SALIDAS = 0
                THEN 'SALIDA FALTANTE'
            WHEN C.ENTRADAS = 0 AND C.SALIDAS = 1
                THEN 'ENTRADA FALTANTE'
            ELSE 'REVISAR'
        END AS ESTADO
    INTO #ESTADOS
    FROM #MARCAS M

    INNER JOIN #CONTEO_DIAS C ON C.TRABAJADOR_KEY = M.TRABAJADOR_KEY AND C.FECHA = CAST(M.F_H_MARCA AS DATE);
    CREATE INDEX IX_ESTADOS_TRABAJADOR ON #ESTADOS (TRABAJADOR_KEY);

    /*=========================================================
      8. PROBLEMAS
    =========================================================*/

    SELECT
        TRABAJADOR_KEY,
        COUNT(*) AS CANT_PROBLEMAS
    INTO #PROBLEMAS
    FROM #ESTADOS
    WHERE ESTADO <> 'OK'
    GROUP BY TRABAJADOR_KEY;
    CREATE UNIQUE CLUSTERED INDEX IX_PROBLEMAS ON #PROBLEMAS (TRABAJADOR_KEY);

    /*=========================================================
      9. RESULTADO FINAL
    =========================================================*/

    SELECT
        M.TRABAJADOR_KEY AS ID_SELECCION,
        MAX(M.IDUSUARIO) AS IDUSUARIO,
        MAX(M.CODIGO_EMP_RELOJ) AS CODIGO_EMP_RELOJ,
        MAX(M.RUT) AS RUT,
        MAX(M.DV) AS DV,
        MAX(M.RUT_C) AS RUT_C,
        CASE
            WHEN MAX(M.IDUSUARIO) IS NULL
                THEN 'Pendiente de identificación'

            ELSE LTRIM(RTRIM(
                MAX(ISNULL(M.NOMBRE, '')) + ' ' +
                MAX(ISNULL(M.AP_PATERNO, '')) + ' ' +
                MAX(ISNULL(M.AP_MATERNO, ''))
            ))
        END AS NOMBRE,
        COUNT(*) AS CANT_MARCAS,
        COUNT(DISTINCT CAST(M.F_H_MARCA AS DATE)) AS CANT_DIAS,
        COUNT(DISTINCT M.IDRELOJ) AS CANT_RELOJES,
        ISNULL(MAX(P.CANT_PROBLEMAS), 0) AS CANT_PROBLEMAS,
        SUM(
            CASE
                WHEN M.CENTRO_RELOJ IS NULL
                  OR M.CENTRO_RELOJ <> @CENTRO
                THEN 1
                ELSE 0
            END
        ) AS CANT_MARCAS_OTRO_CENTRO,

        CASE
            WHEN SUM(
                CASE
                    WHEN M.CENTRO_RELOJ IS NULL
                      OR M.CENTRO_RELOJ <> @CENTRO
                    THEN 1
                    ELSE 0
                END
            ) > 0
            THEN 'SI'
            ELSE 'NO'
        END AS ANOMALIA_CENTRO,

        CASE
            WHEN SUM(
                CASE
                    WHEN M.ESTADO_RRHH = 'SIN EQUIVALENCIA'
                    THEN 1 ELSE 0
                END
            ) > 0
                THEN 'SIN EQUIVALENCIA'

            WHEN SUM(
                CASE
                    WHEN M.ESTADO_RRHH = 'PENDIENTE RRHH'
                    THEN 1 ELSE 0
                END
            ) > 0
                THEN 'PENDIENTE RRHH'
            ELSE 'REGISTRADO RRHH'
        END AS ESTADO_RRHH

    FROM #MARCAS M
    LEFT JOIN #PROBLEMAS P ON P.TRABAJADOR_KEY = M.TRABAJADOR_KEY
    GROUP BY M.TRABAJADOR_KEY
    ORDER BY
        CASE
            WHEN LEFT(M.TRABAJADOR_KEY, 2) = 'R:'
            THEN 0
            ELSE 1
        END,
        NOMBRE;


    /*=========================================================
      10. LIMPIEZA
    =========================================================*/

    DROP TABLE #PROBLEMAS;
    DROP TABLE #ESTADOS;
    DROP TABLE #CONTEO_DIAS;
    DROP TABLE #MARCAS;
    DROP TABLE #ELEGIBLES;
    DROP TABLE #BASE;

END;
GO
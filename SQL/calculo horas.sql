
CREATE OR ALTER PROCEDURE dbo.pu_calcular_horas_trabajador
    @RUT VARCHAR(20),
    @MES INT,
    @ANIO INT
AS
BEGIN
/*******************************************************************************
NOMBRE: pu_calcular_horas_trabajador
CREADOR: [John Vaccarella V.]
FECHA CREACIÓN: 06/11/2026
DESCRIPCIÓN:
Calcula las horas trabajadas, atrasos, horas extras e inasistencias
de un trabajador para un mes y año determinado.
No almacena ni modifica información.

PARÁMETROS:
    @RUT       RUT del trabajador sin dígito verificador.
    @MES       Mes a calcular.
    @ANIO      Año a calcular.

PRUEBAS:
    EXEC dbo.pu_calcular_horas_trabajador
        @RUT = '16618847',
        @MES = 11,
        @ANIO = 2026;
*******************************************************************************/
    SET NOCOUNT ON;

    BEGIN TRY

        ------------------------------------------------------------
        -- 1. Validar parámetros
        ------------------------------------------------------------
        IF @RUT IS NULL OR LTRIM(RTRIM(@RUT)) = ''
        BEGIN
            RAISERROR('Debe ingresar el RUT del trabajador.', 16, 1);
            RETURN;
        END;

        IF @MES NOT BETWEEN 1 AND 12 OR @ANIO < 2000
        BEGIN
            RAISERROR('El mes o año ingresado no es válido.', 16, 1);
            RETURN;
        END;

        DECLARE @FECHA_INI DATE = DATEFROMPARTS(@ANIO, @MES, 1);
        DECLARE @FECHA_FIN DATE = EOMONTH(@FECHA_INI);

        ------------------------------------------------------------
        -- 2. Identificar trabajador
        ------------------------------------------------------------
        DECLARE @IDUSUARIO INT;
        DECLARE @IDTURNOS INT;
        DECLARE @TIPO_TURNO INT;
        DECLARE @FERIADOS INT;

        SELECT @IDUSUARIO = U.IDUSUARIO
        FROM dbo.M_USUARIOS U
        WHERE LTRIM(RTRIM(U.RUT)) = LTRIM(RTRIM(@RUT));

        IF @IDUSUARIO IS NULL
        BEGIN
            RAISERROR('No se encontró un trabajador con el RUT indicado.', 16, 1);
            RETURN;
        END;

        ------------------------------------------------------------
        -- 3. Obtener turno vigente
        ------------------------------------------------------------
        IF (
            SELECT COUNT(*)
            FROM dbo.M_TURNO_USUARIOS
            WHERE IDUSUARIO = @IDUSUARIO
              AND ISNULL(IDESTADO, 1) <> 3
        ) = 0
        BEGIN
            RAISERROR('El trabajador no tiene un turno vigente asignado.', 16, 1);
            RETURN;
        END;

        IF (
            SELECT COUNT(*)
            FROM dbo.M_TURNO_USUARIOS
            WHERE IDUSUARIO = @IDUSUARIO
              AND ISNULL(IDESTADO, 1) <> 3
        ) > 1
        BEGIN
            RAISERROR('El trabajador tiene más de un turno vigente. Debe revisar sus asignaciones.', 16, 1);
            RETURN;
        END;

        SELECT
            @IDTURNOS = T.IDTURNOS,
            @TIPO_TURNO = T.TIPO_TURNO,
            @FERIADOS = T.FERIADOS
        FROM dbo.M_TURNO_USUARIOS TU
        INNER JOIN dbo.M_TURNOS T
            ON T.IDTURNOS = TU.IDTURNOS
        WHERE TU.IDUSUARIO = @IDUSUARIO
          AND ISNULL(TU.IDESTADO, 1) <> 3;

        IF @IDTURNOS IS NULL
        BEGIN
            RAISERROR('No fue posible identificar el turno del trabajador.', 16, 1);
            RETURN;
        END;

        ------------------------------------------------------------
        -- 4. Validar relaciones con relojes
        ------------------------------------------------------------
        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.M_USR_RELOJ
            WHERE IDUSUARIO = @IDUSUARIO
              AND ISNULL(IDESTADO, 1) <> 3
        )
        BEGIN
            RAISERROR('El trabajador no tiene relaciones vigentes con relojes. No es posible calcular sus horas.', 16, 1);
            RETURN;
        END;

        ------------------------------------------------------------
        -- 5. Generar calendario del mes
        ------------------------------------------------------------
        ;WITH CALENDARIO AS
        (
            SELECT @FECHA_INI AS FECHA

            UNION ALL

            SELECT DATEADD(DAY, 1, FECHA)
            FROM CALENDARIO
            WHERE FECHA < @FECHA_FIN
        )
        SELECT FECHA
        INTO #CALENDARIO
        FROM CALENDARIO
        OPTION (MAXRECURSION 31);

        ------------------------------------------------------------
        -- 6. Obtener jornada programada por día
        ------------------------------------------------------------
        SELECT
            C.FECHA,
            CASE
                WHEN EXISTS
                (
                    SELECT 1
                    FROM dbo.M_FERIADOS F
                    WHERE CAST(F.FERIADO AS DATE) = C.FECHA
                )
                THEN 1 ELSE 0
            END AS ES_FERIADO,

            TD.IDTURNODIA,
            H.IDHORA,

            H.HORA_INI,
            H.HORA_FIN,

            CASE
                WHEN TD.IDTURNODIA IS NOT NULL THEN 1
                ELSE 0
            END AS TIENE_TURNO,

            CASE
                WHEN EXISTS
                (
                    SELECT 1
                    FROM dbo.M_FERIADOS F
                    WHERE CAST(F.FERIADO AS DATE) = C.FECHA
                )
                AND ISNULL(@FERIADOS, 1) = 0
                THEN 0

                WHEN TD.IDTURNODIA IS NOT NULL THEN 1
                ELSE 0
            END AS EXIGE_JORNADA

        INTO #JORNADAS

        FROM #CALENDARIO C

        OUTER APPLY
        (
            SELECT TOP 1
                TD.IDTURNODIA,
                TD.IDHORA
            FROM dbo.M_TURNO_DIA TD
            WHERE TD.IDTURNOS = @IDTURNOS
              AND ISNULL(TD.IDESTADO, 1) <> 3
              AND
              (
                  (
                      @TIPO_TURNO = 0
                      AND TD.FECHA IS NULL
                      AND TD.IDDIA =
                          (DATEDIFF(DAY, '19000101', C.FECHA) % 7) + 1
                  )
                  OR
                  (
                      @TIPO_TURNO = 1
                      AND CAST(TD.FECHA AS DATE) = C.FECHA
                  )
              )
            ORDER BY TD.IDTURNODIA
        ) TD

        LEFT JOIN dbo.TG_HORAS H ON H.IDHORA = TD.IDHORA;

        ------------------------------------------------------------
        -- 7. Obtener primera entrada y última salida del día
        ------------------------------------------------------------
        SELECT
            J.FECHA,

            MIN(CASE
                WHEN M.TIPO_MARCA = 0
                THEN M.F_H_MARCA
            END) AS ENTRADA_REAL,

            MAX(CASE
                WHEN M.TIPO_MARCA = 1
                THEN M.F_H_MARCA
            END) AS SALIDA_REAL

        INTO #MARCAS

        FROM #JORNADAS J

        LEFT JOIN dbo.M_USR_RELOJ UR ON UR.IDUSUARIO = @IDUSUARIO AND ISNULL(UR.IDESTADO, 1) <> 3

        LEFT JOIN dbo.M_MARCACIONES M ON M.IDRELOJ = UR.IDRELOJ
           AND M.CODIGO_EMP_RELOJ = CONVERT(VARCHAR(50), UR.IDUSRELOJ)
           AND M.F_H_MARCA >= CAST(J.FECHA AS DATETIME)
           AND M.F_H_MARCA < DATEADD(DAY, 1, CAST(J.FECHA AS DATETIME))
           AND M.TIPO_MARCA IN (0, 1)
           AND ISNULL(M.IDESTADO, 1) <> 3

        GROUP BY J.FECHA;

        ------------------------------------------------------------
        -- 8. Calcular horas diarias
        ------------------------------------------------------------
        SELECT
            J.FECHA,
            J.ES_FERIADO,
            J.TIENE_TURNO,
            J.EXIGE_JORNADA,
            J.HORA_INI AS HORA_ENTRADA_TURNO,
            J.HORA_FIN AS HORA_SALIDA_TURNO,
            M.ENTRADA_REAL,
            M.SALIDA_REAL,

            CASE
                WHEN M.ENTRADA_REAL IS NOT NULL
                 AND M.SALIDA_REAL IS NOT NULL
                THEN DATEDIFF(MINUTE, M.ENTRADA_REAL,
                    CASE
                        WHEN M.SALIDA_REAL < M.ENTRADA_REAL
                        THEN DATEADD(DAY, 1, M.SALIDA_REAL)
                        ELSE M.SALIDA_REAL
                    END)
                ELSE 0
            END AS MINUTOS_TRABAJADOS,

            CASE
                WHEN J.EXIGE_JORNADA = 1
                 AND J.HORA_INI IS NOT NULL
                 AND M.ENTRADA_REAL IS NOT NULL
                 AND CAST(M.ENTRADA_REAL AS TIME) > CAST(J.HORA_INI AS TIME)
                THEN DATEDIFF
                (
                    MINUTE,
                    CAST(J.HORA_INI AS TIME),
                    CAST(M.ENTRADA_REAL AS TIME)
                )
                ELSE 0
            END AS MINUTOS_ATRASO,

            CASE
                WHEN M.ENTRADA_REAL IS NULL
                 AND M.SALIDA_REAL IS NULL
                 AND J.EXIGE_JORNADA = 1
                THEN 1
                ELSE 0
            END AS INASISTENCIA,

            CASE
                WHEN M.ENTRADA_REAL IS NOT NULL
                 AND M.SALIDA_REAL IS NOT NULL
                 AND J.EXIGE_JORNADA = 0
                THEN DATEDIFF
                (
                    MINUTE,
                    M.ENTRADA_REAL,
                    CASE
                        WHEN M.SALIDA_REAL < M.ENTRADA_REAL
                        THEN DATEADD(DAY, 1, M.SALIDA_REAL)
                        ELSE M.SALIDA_REAL
                    END
                )

                WHEN M.SALIDA_REAL IS NOT NULL
                 AND J.EXIGE_JORNADA = 1
                 AND J.HORA_FIN IS NOT NULL
                THEN
                    CASE
                        WHEN CAST(M.SALIDA_REAL AS TIME) > CAST(J.HORA_FIN AS TIME)
                        THEN DATEDIFF
                        (
                            MINUTE,
                            CAST(J.HORA_FIN AS TIME),
                            CAST(M.SALIDA_REAL AS TIME)
                        )
                        ELSE 0
                    END

                ELSE 0
            END AS MINUTOS_EXTRAS
        INTO #RESULTADO_BASE
        FROM #JORNADAS J
        LEFT JOIN #MARCAS M ON M.FECHA = J.FECHA;


		------------------------------------------------------------
		-- 9. Resultado diario con clasificación de horas extras
		------------------------------------------------------------
		SELECT
			@IDUSUARIO AS IDUSUARIO,
			@RUT AS RUT,
			@IDTURNOS AS IDTURNOS,
			B.FECHA,

			CASE (DATEDIFF(DAY, '19000101', B.FECHA) % 7) + 1
				WHEN 1 THEN 'Lunes'
				WHEN 2 THEN 'Martes'
				WHEN 3 THEN 'Miércoles'
				WHEN 4 THEN 'Jueves'
				WHEN 5 THEN 'Viernes'
				WHEN 6 THEN 'Sábado'
				WHEN 7 THEN 'Domingo'
			END AS DIA,

			B.ES_FERIADO,
			B.TIENE_TURNO,
			B.EXIGE_JORNADA,

			B.HORA_ENTRADA_TURNO,
			B.HORA_SALIDA_TURNO,
			B.ENTRADA_REAL,
			B.SALIDA_REAL,

			-- Minutos trabajados
			B.MINUTOS_TRABAJADOS,

			-- Horas trabajadas en formato H y M
			CAST(B.MINUTOS_TRABAJADOS / 60 AS VARCHAR(10))
				+ 'H ' +
			RIGHT('0' + CAST(B.MINUTOS_TRABAJADOS % 60 AS VARCHAR(2)), 2)
				+ 'M' AS HORAS_TRABAJADAS_TEXTO,

			-- Atrasos
			B.MINUTOS_ATRASO,

			CAST(B.MINUTOS_ATRASO / 60 AS VARCHAR(10))
				+ 'H ' +
			RIGHT('0' + CAST(B.MINUTOS_ATRASO % 60 AS VARCHAR(2)), 2)
				+ 'M' AS HORAS_ATRASO_TEXTO,

			-- Extras totales
			B.MINUTOS_EXTRAS,

			CAST(B.MINUTOS_EXTRAS / 60 AS VARCHAR(10))
				+ 'H ' +
			RIGHT('0' + CAST(B.MINUTOS_EXTRAS % 60 AS VARCHAR(2)), 2)
				+ 'M' AS HORAS_EXTRAS_TEXTO,

			-- Extras diurnas:
			-- Lunes a viernes, incluyendo días hábiles sin turno.
			-- Los feriados con FERIADOS = 0 se clasifican como festivos.
			CASE
				WHEN (DATEDIFF(DAY, '19000101', B.FECHA) % 7) + 1
						BETWEEN 1 AND 5
				 AND NOT (B.ES_FERIADO = 1 AND ISNULL(@FERIADOS, 1) = 0)
				THEN B.MINUTOS_EXTRAS
				ELSE 0
			END AS MINUTOS_EXTRAS_DIURNAS,

			CAST(
				CASE
					WHEN (DATEDIFF(DAY, '19000101', B.FECHA) % 7) + 1
							BETWEEN 1 AND 5
					 AND NOT (B.ES_FERIADO = 1 AND ISNULL(@FERIADOS, 1) = 0)
					THEN B.MINUTOS_EXTRAS
					ELSE 0
				END / 60 AS VARCHAR(10)
			)
			+ 'H ' +
			RIGHT('0' + CAST(
				CASE
					WHEN (DATEDIFF(DAY, '19000101', B.FECHA) % 7) + 1
							BETWEEN 1 AND 5
					 AND NOT (B.ES_FERIADO = 1 AND ISNULL(@FERIADOS, 1) = 0)
					THEN B.MINUTOS_EXTRAS
					ELSE 0
				END % 60 AS VARCHAR(2)), 2)
			+ 'M' AS HORAS_EXTRAS_DIURNAS_TEXTO,

			-- Extras festivas:
			-- Sábados, domingos y feriados no considerados laborables.
			CASE
				WHEN (DATEDIFF(DAY, '19000101', B.FECHA) % 7) + 1
						IN (6, 7)
				  OR (B.ES_FERIADO = 1 AND ISNULL(@FERIADOS, 1) = 0)
				THEN B.MINUTOS_EXTRAS
				ELSE 0
			END AS MINUTOS_EXTRAS_FESTIVAS,

			CAST(
				CASE
					WHEN (DATEDIFF(DAY, '19000101', B.FECHA) % 7) + 1
							IN (6, 7)
					  OR (B.ES_FERIADO = 1 AND ISNULL(@FERIADOS, 1) = 0)
					THEN B.MINUTOS_EXTRAS
					ELSE 0
				END / 60 AS VARCHAR(10)
			)
			+ 'H ' +
			RIGHT('0' + CAST(
				CASE
					WHEN (DATEDIFF(DAY, '19000101', B.FECHA) % 7) + 1
							IN (6, 7)
					  OR (B.ES_FERIADO = 1 AND ISNULL(@FERIADOS, 1) = 0)
					THEN B.MINUTOS_EXTRAS
					ELSE 0
				END % 60 AS VARCHAR(2)), 2)
			+ 'M' AS HORAS_EXTRAS_FESTIVAS_TEXTO,

			B.INASISTENCIA

		INTO #RESULTADO

		FROM #RESULTADO_BASE B;

		-- Primer resultado: detalle diario
		SELECT *
		FROM #RESULTADO
		ORDER BY FECHA;



		------------------------------------------------------------
		-- 10. Resumen mensual
		------------------------------------------------------------
		SELECT
			@IDUSUARIO AS IDUSUARIO,
			@RUT AS RUT,
			@ANIO AS ANIO,
			@MES AS MES,
			-- Días trabajados
			COUNT(DISTINCT CASE
				WHEN MINUTOS_TRABAJADOS > 0 THEN FECHA
			END) AS TOTAL_DIAS_TRABAJADOS,
			-- Horas trabajadas
			SUM(MINUTOS_TRABAJADOS) AS TOTAL_MINUTOS_TRABAJADOS,
			CAST(SUM(MINUTOS_TRABAJADOS) / 60.0 AS DECIMAL(10,2))
				AS TOTAL_HORAS_TRABAJADAS,
			CAST(SUM(MINUTOS_TRABAJADOS) / 60 AS VARCHAR(10))
				+ 'H ' +
			RIGHT('0' + CAST(SUM(MINUTOS_TRABAJADOS) % 60 AS VARCHAR(2)), 2)
				+ 'M' AS TOTAL_HORAS_TRABAJADAS_TEXTO,
			-- Atrasos
			SUM(MINUTOS_ATRASO) AS TOTAL_MINUTOS_ATRASO,
			CAST(SUM(MINUTOS_ATRASO) / 60 AS VARCHAR(10))
				+ 'H ' +
			RIGHT('0' + CAST(SUM(MINUTOS_ATRASO) % 60 AS VARCHAR(2)), 2)
				+ 'M' AS TOTAL_HORAS_ATRASO_TEXTO,
			-- Horas extras totales
			SUM(MINUTOS_EXTRAS) AS TOTAL_MINUTOS_EXTRAS,
			CAST(SUM(MINUTOS_EXTRAS) / 60 AS VARCHAR(10))
				+ 'H ' +
			RIGHT('0' + CAST(SUM(MINUTOS_EXTRAS) % 60 AS VARCHAR(2)), 2)
				+ 'M' AS TOTAL_HORAS_EXTRAS_TEXTO,
			-- Extras diurnas
			SUM(MINUTOS_EXTRAS_DIURNAS) AS TOTAL_MINUTOS_EXTRAS_DIURNAS,

			CAST(SUM(MINUTOS_EXTRAS_DIURNAS) / 60 AS VARCHAR(10))
				+ 'H ' +
			RIGHT('0' + CAST(SUM(MINUTOS_EXTRAS_DIURNAS) % 60 AS VARCHAR(2)), 2)
				+ 'M' AS TOTAL_HORAS_EXTRAS_DIURNAS_TEXTO,
			-- Extras festivas
			SUM(MINUTOS_EXTRAS_FESTIVAS) AS TOTAL_MINUTOS_EXTRAS_FESTIVAS,
			CAST(SUM(MINUTOS_EXTRAS_FESTIVAS) / 60 AS VARCHAR(10))
				+ 'H ' +
			RIGHT('0' + CAST(SUM(MINUTOS_EXTRAS_FESTIVAS) % 60 AS VARCHAR(2)), 2)
				+ 'M' AS TOTAL_HORAS_EXTRAS_FESTIVAS_TEXTO,
			-- Inasistencias
			SUM(INASISTENCIA) AS TOTAL_INASISTENCIAS

		FROM #RESULTADO;

        DROP TABLE #RESULTADO;
        DROP TABLE #RESULTADO_BASE;
        DROP TABLE #MARCAS;
        DROP TABLE #JORNADAS;
        DROP TABLE #CALENDARIO;

    END TRY
    BEGIN CATCH

        IF OBJECT_ID('tempdb..#RESULTADO') IS NOT NULL DROP TABLE #RESULTADO;
        IF OBJECT_ID('tempdb..#RESULTADO_BASE') IS NOT NULL DROP TABLE #RESULTADO_BASE;
        IF OBJECT_ID('tempdb..#MARCAS') IS NOT NULL DROP TABLE #MARCAS;
        IF OBJECT_ID('tempdb..#JORNADAS') IS NOT NULL DROP TABLE #JORNADAS;
        IF OBJECT_ID('tempdb..#CALENDARIO') IS NOT NULL DROP TABLE #CALENDARIO;

        DECLARE @MENSAJE NVARCHAR(4000) = ERROR_MESSAGE();

        RAISERROR(@MENSAJE, 16, 1);

    END CATCH
END;

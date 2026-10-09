CREATE OR ALTER PROCEDURE [dbo].[PU_ACTUALIZA_SINCRONIZA_MARCAS]
    @IDSINCRONIZA INT,
    @IDUSUARIO INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @IDRELOJ INT;
    DECLARE @F_H_INICIO DATETIME;
    DECLARE @F_H_FIN DATETIME;
    DECLARE @CANT_LEIDA INT = 0;
    DECLARE @CANT_INSERT INT = 0;
    DECLARE @MENSAJE VARCHAR(800);
	/* ====================================================================
		SP que refresca una sincronizacion, y inserta los nuevos registros 
		asociados a esa sincronizacion en m_marcaciones desde m_marcas_reloj
		John Vaccarella V.
		06/10/2026
	===================================================================== */
    BEGIN TRY

        /* =========================================================
           1. OBTENER DATOS DE LA SINCRONIZACIÓN
           ========================================================= */

        SELECT
            @IDRELOJ = IDRELOJ,
            @F_H_INICIO = F_H_INICIO,
            @F_H_FIN = F_H_FIN
        FROM dbo.M_SINCRONIZACION
        WHERE IDSINCRONIZA = @IDSINCRONIZA
          AND IDESTADO = 1;

        IF @IDRELOJ IS NULL
        BEGIN
            SELECT
                @IDSINCRONIZA AS IDSINCRONIZA,
                0 AS CANT_LEIDA,
                0 AS CANT_INSERT,
                0 AS IDESTADO,
                'La sincronización no existe o no se encuentra activa.' AS MENSAJE;
            RETURN;
        END;

        /* =========================================================
           2. CONTAR MARCAS ACTUALES DEL PERÍODO EN EL RELOJ
           ========================================================= */

        SELECT @CANT_LEIDA = COUNT(*)
        FROM dbo.M_MARCA_RELOJ MR
        WHERE MR.IDRELOJ = @IDRELOJ
          AND MR.IDESTADO = 1
          AND DATETIMEFROMPARTS(
                MR.[YEAR],
                MR.[MONTH],
                MR.[DAY],
                MR.[HOUR],
                MR.[MINUTE],
                MR.[SECOND],
                0
              ) >= @F_H_INICIO
          AND DATETIMEFROMPARTS(
                MR.[YEAR],
                MR.[MONTH],
                MR.[DAY],
                MR.[HOUR],
                MR.[MINUTE],
                MR.[SECOND],
                0
              ) <= @F_H_FIN;

        /* =========================================================
           3. INSERTAR SOLAMENTE MARCAS NUEVAS
           ========================================================= */

        BEGIN TRANSACTION;
        INSERT INTO dbo.M_MARCACIONES
        (
            IDSINCRONIZA,
            IDRELOJ,
            CODIGO_EMP_RELOJ,
            F_H_MARCA,
            TIPO_MARCA,
            F_H_CARGA,
            TIPO_CARGA,
            OBSERVACIONES
        )
        SELECT
            @IDSINCRONIZA,
            MR.IDRELOJ,
            MR.ENROLLNUMBER,
            DATETIMEFROMPARTS(
                MR.[YEAR],
                MR.[MONTH],
                MR.[DAY],
                MR.[HOUR],
                MR.[MINUTE],
                MR.[SECOND],
                0
            ),
            MR.INOUTMODE,
            GETDATE(),
            1,
            'Actualización desde reloj'
        FROM dbo.M_MARCA_RELOJ MR
        WHERE MR.IDRELOJ = @IDRELOJ
          AND MR.IDESTADO = 1
          AND DATETIMEFROMPARTS(
                MR.[YEAR],
                MR.[MONTH],
                MR.[DAY],
                MR.[HOUR],
                MR.[MINUTE],
                MR.[SECOND],
                0
              ) >= @F_H_INICIO
          AND DATETIMEFROMPARTS(
                MR.[YEAR],
                MR.[MONTH],
                MR.[DAY],
                MR.[HOUR],
                MR.[MINUTE],
                MR.[SECOND],
                0
              ) <= @F_H_FIN
          AND NOT EXISTS
          (
              SELECT 1
              FROM dbo.M_MARCACIONES MC
              WHERE MC.IDRELOJ = MR.IDRELOJ
                AND MC.CODIGO_EMP_RELOJ = MR.ENROLLNUMBER
                AND MC.F_H_MARCA =
                    DATETIMEFROMPARTS(
                        MR.[YEAR],
                        MR.[MONTH],
                        MR.[DAY],
                        MR.[HOUR],
                        MR.[MINUTE],
                        MR.[SECOND],
                        0            )
          );

        SET @CANT_INSERT = @@ROWCOUNT;

        /* =========================================================
           4. ACTUALIZAR CABECERA
           ========================================================= */

        UPDATE dbo.M_SINCRONIZACION
        SET CANT_LEIDA = @CANT_LEIDA,
            CANT_INSERT = ISNULL(CANT_INSERT,0) + @CANT_INSERT,
            IDUSUARIO = @IDUSUARIO
        WHERE IDSINCRONIZA = @IDSINCRONIZA;
        COMMIT TRANSACTION;

        /* =========================================================
           5. RESULTADO
           ========================================================= */

        IF @CANT_INSERT = 0
        BEGIN
            SELECT
                @IDSINCRONIZA AS IDSINCRONIZA,
                @CANT_LEIDA AS CANT_LEIDA,
                0 AS CANT_INSERT,
                1 AS IDESTADO,
                'No existen marcas nuevas para registrar en el período seleccionado.' AS MENSAJE;
            RETURN;
        END;

        SELECT
            @IDSINCRONIZA AS IDSINCRONIZA,
            @CANT_LEIDA AS CANT_LEIDA,
            @CANT_INSERT AS CANT_INSERT,
            1 AS IDESTADO,
            'Sincronización actualizada correctamente. Se registraron ' + CAST(@CANT_INSERT AS VARCHAR(20)) + ' marcas nuevas.' AS MENSAJE;

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        SET @MENSAJE = ERROR_MESSAGE();
        RAISERROR(@MENSAJE,16,1);
        RETURN;
    END CATCH
END
GO
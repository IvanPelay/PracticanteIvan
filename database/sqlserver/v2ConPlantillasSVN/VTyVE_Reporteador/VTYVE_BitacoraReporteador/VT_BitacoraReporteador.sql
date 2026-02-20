-- ===================================================================
-- VT016Bitacora: Vista de Trabajo con historial de ejecuciones
-- ===================================================================
CREATE VIEW [dbo].[Vt016Bitacora] AS
SELECT 
    b.i_Cve_Generacion,
    b.i_Cve_Programacion,
    p.t_Nombre AS t_NombreProgramacion,
    pl.t_Nombre AS t_NombrePlantilla,
    b.f_FechaInicio,
    b.f_FechaFin,
    b.t_Estatus,
    b.i_RegistrosProcesados,
    b.t_RutaDocumento,
    b.t_IdDocumento,
    b.t_Error,
    b.t_ParametrosUsados,
    DATEDIFF(SECOND, b.f_FechaInicio, b.f_FechaFin) AS i_DuracionSegundos
FROM [dbo].[Bit016GeneracionReporteador] b
INNER JOIN [dbo].[Enc016ProgramacionReporteador] p ON b.i_Cve_Programacion = p.i_Cve_Programacion
INNER JOIN [dbo].[Cat016PlantillasReporteador] pl ON p.i_Cve_Plantilla = pl.i_Cve_Plantilla
WHERE b.i_Cve_Estado = 1
GO
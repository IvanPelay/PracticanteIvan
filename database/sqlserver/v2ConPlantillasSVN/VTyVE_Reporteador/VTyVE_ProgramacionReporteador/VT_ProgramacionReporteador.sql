-- ===================================================================
-- VT016Programacion: Vista de Trabajo con programaciones
-- ===================================================================
CREATE VIEW [dbo].[Vt016Programacion] AS
SELECT 
    p.i_Cve_Programacion,
    p.i_Cve_Plantilla,
    pl.t_Nombre AS t_NombrePlantilla,
    p.t_Nombre AS t_NombreProgramacion,
    CASE p.t_Frecuencia 
        WHEN 'U' THEN 'Única'
        WHEN 'D' THEN 'Diaria'
        WHEN 'S' THEN 'Semanal'
        WHEN 'M' THEN 'Mensual'
    END AS t_FrecuenciaDesc,
    p.t_DiasSemana,
    p.i_DiaMes,
    p.t_Hora,
    p.t_Parametros,
    p.f_ProximaEjecucion,
    p.f_UltimaEjecucion,
    p.f_FechaRegistro,
    p.t_UsuarioRegistro,
    CASE WHEN p.i_Cve_Status = 1 THEN 'Activo' ELSE 'Inactivo' END AS t_Status,
    CASE WHEN p.i_Cve_Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS t_Estado
FROM [dbo].[Enc016ProgramacionReporteador] p
INNER JOIN [dbo].[Cat016PlantillasReporteador] pl ON p.i_Cve_Plantilla = pl.i_Cve_Plantilla
WHERE p.i_Cve_Estado = 1
GO
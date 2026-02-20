-- ===================================================================
-- VT016Plantillas: Vista de Trabajo con los datos de las plantillas
-- ===================================================================
CREATE VIEW [dbo].[Vt016Plantillas] AS
SELECT 
    i_Cve_Plantilla,
    t_Nombre,
    t_RutaPlantilla,
    t_NombreBaseDatos,
    t_Consulta,
    t_ParametrosConfig,
    t_FormatoSalida,
    f_FechaRegistro,
    t_UsuarioRegistro,
    CASE WHEN i_Cve_Status = 1 THEN 'Activo' ELSE 'Inactivo' END AS t_Status,
    CASE WHEN i_Cve_Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS t_Estado
FROM [dbo].[Cat016PlantillasReporteador]
WHERE i_Cve_Estado = 1
GO

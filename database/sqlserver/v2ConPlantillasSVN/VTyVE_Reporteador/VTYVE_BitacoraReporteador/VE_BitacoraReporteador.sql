-- ===================================================================
-- VE016Bitacora: Vista de Entorno para bitácora
-- ===================================================================
CREATE VIEW [dbo].[Ve016Bitacora] AS
SELECT * FROM (
    SELECT 'i_Cve_Generacion' as Nombre, 1 AS Llave, 11 AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Clave Generación' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '1' AS TipoFiltro
    UNION ALL
    SELECT 'i_Cve_Programacion' as Nombre, 0 AS Llave, 11 AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Clave Programación' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_NombreProgramacion' as Nombre, 0 AS Llave, 150 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Programación' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_NombrePlantilla' as Nombre, 0 AS Llave, 200 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Plantilla' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 'f_FechaInicio' as Nombre, 0 AS Llave, 23 AS Longitud, 4 AS TipoDato, 1 AS Visible, 'Inicio' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 'f_FechaFin' as Nombre, 0 AS Llave, 23 AS Longitud, 4 AS TipoDato, 1 AS Visible, 'Fin' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_Estatus' as Nombre, 0 AS Llave, 20 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Estado' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 'i_RegistrosProcesados' as Nombre, 0 AS Llave, 11 AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Registros' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 'i_DuracionSegundos' as Nombre, 0 AS Llave, 11 AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Duración (s)' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_RutaDocumento' as Nombre, 0 AS Llave, 500 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Ruta Documento' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_IdDocumento' as Nombre, 0 AS Llave, 50 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'ID Documento' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_Error' as Nombre, 0 AS Llave, 2147483647 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Error' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_ParametrosUsados' as Nombre, 0 AS Llave, 2147483647 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Parámetros Usados' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro 
) AS VE
GO
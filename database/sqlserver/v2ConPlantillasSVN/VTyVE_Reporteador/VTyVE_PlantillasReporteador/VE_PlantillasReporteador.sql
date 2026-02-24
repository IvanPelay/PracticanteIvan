-- ===================================================================
-- VE016Plantillas: Vista de Entorno para configuración de UI
-- ===================================================================
CREATE VIEW [dbo].[Ve016Plantillas] AS
SELECT * FROM (
    SELECT 'i_Cve_Plantilla' as Nombre, 1 AS Llave, 11 AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Clave Plantilla' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '1' AS TipoFiltro
    UNION ALL
    SELECT 't_Nombre' as Nombre, 0 AS Llave, 200 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Nombre Plantilla' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_Descripcion' as Nombre, 0 AS Llave, 1000 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Descripcion Plantilla' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '0' AS TipoFiltro
    UNION ALL
    SELECT 't_RutaPlantilla' as Nombre, 0 AS Llave, 500 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Ruta Plantilla' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_NombreBaseDatos' as Nombre, 0 AS Llave, 100 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Base de Datos' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro --dudas si dejarlop o eliminarlo
    UNION ALL
    SELECT 't_Consulta' as Nombre, 0 AS Llave, 2147483647 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Consulta SQL' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '0' AS TipoFiltro 
    UNION ALL
    SELECT 't_ColumnasConfig' as Nombre, 0 AS Llave, 2147483647 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Config. Columnas (JSON)' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '0' AS TipoFiltro
    UNION ALL
    SELECT 't_ParametrosConfig' as Nombre, 0 AS Llave, 2147483647 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Config. Parámetros (JSON)' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '0' AS TipoFiltro --dudas los usuarios pueden cargar paqrametros e formato JSON??
    UNION ALL
    SELECT 't_FormatoSalida' as Nombre, 0 AS Llave, 10 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Formato Salida' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, 'XLSX' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 'f_FechaRegistro' as Nombre, 0 AS Llave, 23 AS Longitud, 4 AS TipoDato, 1 AS Visible, 'Fecha Registro' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_UsuarioRegistro' as Nombre, 0 AS Llave, 50 AS Longitud, 1 AS TipoDato, 1 As Visible, 'Usuario Registro' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_Estatus' as Nombre, 0 AS Llave, 10 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Estatus' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_Estado' as Nombre, 0 AS Llave, 10 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Estado' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
) AS VE
GO
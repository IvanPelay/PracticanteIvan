-- ===================================================================
-- VE016Programacion: Vista de Entorno para programaciones
-- ===================================================================
CREATE VIEW [dbo].[Ve016Programacion] AS
SELECT * FROM (
    SELECT 'i_Cve_Programacion' as Nombre, 1 AS Llave, 11 AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Clave Programación' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '1' AS TipoFiltro
    UNION ALL
    SELECT 'i_Cve_Plantilla' as Nombre, 0 AS Llave, 11 AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Clave Plantilla' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_NombrePlantilla' as Nombre, 0 AS Llave, 200 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Plantilla' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_NombreProgramacion' as Nombre, 0 AS Llave, 150 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Nombre Programación' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_FrecuenciaDesc' as Nombre, 0 AS Llave, 20 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Frecuencia' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_DiasSemana' as Nombre, 0 AS Llave, 20 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Días Semana' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 'i_DiaMes' as Nombre, 0 AS Llave, 2 AS Longitud, 0 AS TipoDato, 1 AS Visible, 'Día Mes' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_Hora' as Nombre, 0 AS Llave, 8 AS Longitud, 4 AS TipoDato, 1 AS Visible, 'Hora' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_Parametros' as Nombre, 0 AS Llave, 2147483647 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Parámetros (JSON)' AS NombreColumna, 1 AS PuedeInsertar, 1 AS PuedeModificar, '' AS ValorDefault, '2' TipoFiltro
    UNION ALL
    SELECT 'f_ProximaEjecucion' as Nombre, 0 AS Llave, 23 AS Longitud, 4 AS TipoDato, 1 AS Visible, 'Próxima Ejecución' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 'f_UltimaEjecucion' as Nombre, 0 AS Llave, 23 AS Longitud, 4 AS TipoDato, 1 AS Visible, 'Última Ejecución' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 'f_FechaRegistro' as Nombre, 0 AS Llave, 23 AS Longitud, 4 AS TipoDato, 1 AS Visible, 'Fecha Registro' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_UsuarioRegistro' as Nombre, 0 AS Llave, 50 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Usuario Registro' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_Estatus' as Nombre, 0 AS Llave, 10 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Estatus' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
    UNION ALL
    SELECT 't_Estado' as Nombre, 0 AS Llave, 10 AS Longitud, 1 AS TipoDato, 1 AS Visible, 'Estado' AS NombreColumna, 0 AS PuedeInsertar, 0 AS PuedeModificar, '' AS ValorDefault, '2' AS TipoFiltro
) AS VE
GO
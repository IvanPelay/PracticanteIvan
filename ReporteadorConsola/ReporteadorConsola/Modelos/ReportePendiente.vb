Imports System

Namespace Modelos
    'Representacion de un reporte listo para ejecutar
    Public Class ReportePendiente

        Public Property i_Cve_Programacion As Integer
        Public Property i_Cve_Generacion As Integer
        Public Property NombreReporte As String
        Public Property t_RutaPlantilla As String
        Public Property t_Consulta As String
        Public Property t_FormatoSalida As String
        Public Property t_ParametrosConfig As String
        Public Property t_ColumnasConfig As String
        Public Property t_Parametros As String
        Public Property t_DiasSemana As String
        Public Property i_DiaMes As Integer?
    End Class
End Namespace

Imports System

Namespace Modelos

    'Representa una programacion pendiente por ejecutar
    Public Class ProgramacionPendiente
        Public Property i_Cve_Programacion As Integer
        Public Property t_Nombre As String
        Public Property t_Frecuencia As String
        Public Property t_Hora As String
        Public Property f_ProximaEjecucion As DateTime?
        Public Property i_Cve_Plantilla As Integer
        Public Property t_NombrePlantilla As String
        Public Property t_Consulta As String
        Public Property t_ColumnasConfig As String
        Public Property t_ParametrosConfig As String
        Public Property t_RutaPlantilla As String
        Public Property t_Parametros As String

    End Class
End Namespace

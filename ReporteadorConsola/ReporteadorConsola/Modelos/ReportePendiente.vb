Imports System

Namespace Modelos
    'Representacion de un reporte listo para ejecutar
    Public Class ReportePendiente

        Public Property i_Cve_Programacion As Integer
        Public Property i_Cve_Generacion As Integer
        Public Property NombreReporte As String
        Public Property t_RutaPlantilla As String
        Public Property t_FormatoSalida As String
        Public Property t_NombreVista As String
        Public Property t_NombreSP As String
        Public Property t_ParametrosConfig As String
        Public Property t_ColumnasConfig As String
        Public Property t_Parametros As String
        Public Property t_DiasSemana As String
        Public Property i_DiaMes As Integer?
        Public Property t_Frecuencia As String
        Public Property f_ViegnciaInicio As DateTime
        Public Property f_VigenciaFin As DateTime?

        Public ReadOnly Property TieneSP As Boolean
            Get
                Return Not String.IsNullOrWhiteSpace(t_NombreSP)
            End Get
        End Property

        'indicar si la vigencia aun tiene valor
        Public ReadOnly Property VigenciaActiva As Boolean
            Get
                Dim ahora = DateTime.Now
                Return ahora >= f_ViegnciaInicio AndAlso (Not f_VigenciaFin.HasValue OrElse ahora <= f_VigenciaFin.Value)
            End Get
        End Property
    End Class
End Namespace

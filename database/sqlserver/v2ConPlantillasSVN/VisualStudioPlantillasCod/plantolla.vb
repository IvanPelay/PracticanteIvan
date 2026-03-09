Imports System
Imports System.Collections.Generic
Imports Newtonsoft.Json

Namespace Krom.Reporteador2.Modelos

    ''' <summary>
    ''' Modelo que representa una Plantilla de Reporte
    ''' Corresponde a la vista Vt016Plantillas
    ''' </summary>
    ''' 

    Public Class PlantillaModelo
        ' Propiedades que coinciden con la VT
        Public Property i_Cve_Plantilla As Integer
        Public Property t_Nombre As String
        Public Property t_Descripcion As String
        Public Property t_RutaPlantilla As String
        Public Property t_NombreBaseDatos As String
        Public Property t_Consulta As String
        Public Property t_ColumnasConfig As String
        Public Property t_ParametrosConfig As String
        Public Property t_FormatoSalida As String
        Public Property f_FechaRegistro As DateTime
        Public Property t_UsuarioRegistro As String
        Public Property t_Estatus As String
        Public Property t_Estado As String

        'Propiedades Auxiliares 
        Public Property EsNuevo As Boolean = False
        Public Property Parametros As List(Of ParametroConfigModel)
        Public Property Columnas As List(Of ColumnaConfigModel)

        ' Convertir t_parametros de JSON a lista de objetos
        Public Sub ConvertirParametros()
            If Not String.IsNullOrEmpty(t_ParametrosConfig) Then
                Try
                    Parametros = JsonConvert.DeserializeObject(Of List(Of ParametroConfigModel))(t_ParametrosConfig)
                Catch ex As JsonException
                    Parametros = New List(Of ParametroConfigModel)()
                End Try
            Else
                Parametros = New List(Of ParametroConfigModel)()
            End If
        End Sub

        'Convertir t_ColumnasConfig de JSON a lista de objetos
        Public Sub ConvertirColumnas()   ' ← NUEVO MÉTODO
            If Not String.IsNullOrEmpty(t_ColumnasConfig) Then
                Try
                    Columnas = JsonConvert.DeserializeObject(Of List(Of ColumnaConfigModel))(t_ColumnasConfig)
                Catch ex As JsonException
                    Columnas = New List(Of ColumnaConfigModel)()
                End Try
            Else
                Columnas = New List(Of ColumnaConfigModel)()
            End If
        End Sub

        ' Generar JSON a partir de la lista de parámetros para guardar en la base de datos
        Public Function GenerarParametrosJson() As String
            If Parametros IsNot Nothing AndAlso Parametros.Count > 0 Then
                Return JsonConvert.SerializeObject(Parametros, Formatting.Indented)
            End If
            Return String.Empty
        End Function

        'Generar JSON a partir de la lista de columnas para guardar en la base de datos
        Public Function GenerarColumnasJson() As String   ' ← NUEVO MÉTODO
            If Columnas IsNot Nothing AndAlso Columnas.Count > 0 Then
                Return JsonConvert.SerializeObject(Columnas, Formatting.Indented)
            End If
            Return String.Empty
        End Function

        'convertir todos los JSON del modelo
        Public Sub ConvertirTodosLosJson()
            ConvertirParametros()
            ConvertirColumnas()
        End Sub

        'validar que los parámetros requeridos tengan valor correcto
        Public Function ValidarParametros() As List(Of String)
            Dim errores As New List(Of String)

            If Parametros IsNot Nothing Then
                For Each param As ParametroConfigModel In Parametros
                    If String.IsNullOrWhiteSpace(param.Nombre) Then
                        errores.Add("Todos los parámetros deben tener un nombre")
                    End If
                    If String.IsNullOrWhiteSpace(param.Etiqueta) Then
                        errores.Add($"El parámetro '{param.Nombre}' debe tener una etiqueta")
                    End If
                Next
            End If
            Return errores
        End Function

        'validar que las columnas tengan estructura correcta
        Public Function ValidarColumnas() As List(Of String)
            Dim errores As New List(Of String)
            If Columnas IsNot Nothing Then
                For Each col In Columnas
                    If String.IsNullOrWhiteSpace(col.Campo) Then
                        errores.Add("Todas las columnas deben tener un nombre de campo")
                    End If
                Next
            End If
            Return errores
        End Function

        ' Modelo para configuración de parámetros
        Public Class ParametroConfigModel
            Public Property Nombre As String          ' Nombre del parámetro en SQL
            Public Property Tipo As String            ' 'string', 'int', 'date', 'decimal'
            Public Property Etiqueta As String        ' Texto a mostrar en UI
            Public Property Requerido As Boolean      ' Si es obligatorio
            Public Property ValorDefault As String    ' Valor por defecto
            Public Property Orden As Integer          ' Orden de aparición
        End Class

        ' Modelo para configuración de columnas
        Public Class ColumnaConfigModel
            Public Property Campo As String           ' Nombre del campo en el resultado SQL
            Public Property Etiqueta As String        ' Texto a mostrar en UI
            Public Property Formato As String         ' Formato para mostrar (ej. 'C2' para moneda)
            Public Property Orden As Integer          ' Orden de aparición
        End Class

        'clase para filtrar plantillas en la consulta
        Public Class FiltroPlantilla
            Public Property Nombre As String
            Public Property FechaInicio As DateTime?
            Public Property FechaFin As DateTime?
            Public Property Estatus As String
        End Class

    End Class

End Namespace
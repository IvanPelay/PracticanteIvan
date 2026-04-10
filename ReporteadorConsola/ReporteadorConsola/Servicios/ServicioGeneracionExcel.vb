Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports System.Linq
Imports Newtonsoft.Json
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports ReporteadorConsola.Modelos
Imports Wma.Exceptions
Imports gsol

Namespace Servicios
    Public Class ServicioGeneracionExcel


        ''' <summary>
        ''' Genera un archivo Excel a partir de una plantilla y datos
        ''' </summary>
        Public Function GenerarExcelDesdePlantilla(
            rutaPlantilla As String,
            datos As DataTable,
            nombreProgramacion As String,
            directorioSalidaBase As String,
            filaInicio As Integer,
            columnaInicio As Integer) As TagWatcher

            Dim estatus As New TagWatcher()
            Try
                ' Validar que la plantilla existe
                If Not File.Exists(rutaPlantilla) Then
                    estatus.SetError(Me, $"No se encontro la plantilla en la ruta: {rutaPlantilla}", TagWatcher.ErrorTypes.C6_012_12000)
                    Return estatus
                End If

                ' Crear directorio de salida si no existe
                If Not Directory.Exists(directorioSalidaBase) Then
                    Directory.CreateDirectory(directorioSalidaBase)
                End If

                ' Generar nombre de archivo único
                Dim timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss")
                Dim nombreLimpio = LimpiarNombreArchivo(nombreProgramacion)
                Dim nombreArchivoSalida = $"{nombreLimpio}_{timestamp}.xlsx"
                Dim rutaSalida = Path.Combine(directorioSalidaBase, nombreArchivoSalida)

                ' Procesar el Excel
                Using package As New ExcelPackage(New FileInfo(rutaPlantilla))
                    ' Usar la primera hoja
                    Dim worksheet = package.Workbook.Worksheets.First()

                    If datos IsNot Nothing AndAlso datos.Rows.Count > 0 Then
                        worksheet.Cells(filaInicio, columnaInicio).LoadFromDataTable(datos, False) 'Con True nos devolvera los encabezados
                        'autoajustar columnas 
                        worksheet.Cells(worksheet.Dimension.Address).AutoFitColumns()
                    Else
                        worksheet.Cells(filaInicio, columnaInicio).Value = "No se encontraron datos para este reporte"
                    End If

                    package.SaveAs(New FileInfo(rutaSalida))
                End Using

                estatus.SetOK()
                estatus.ObjectReturned = rutaSalida

            Catch ex As Exception
                estatus.SetError(Me, $"Error al generar el excel: {ex.Message}", TagWatcher.ErrorTypes.C6_012_1042)
            End Try
            Return estatus
        End Function

        ''' <summary>
        ''' Limpia un nombre de archivo quitando caracteres no válidos
        ''' </summary>
        Private Function LimpiarNombreArchivo(nombre As String) As String
            If String.IsNullOrEmpty(nombre) Then Return "Reporte"

            Dim invalidChars = Path.GetInvalidFileNameChars()
            Dim result = New String(nombre.Where(Function(c) Not invalidChars.Contains(c)).ToArray())

            ' Limitar longitud
            If result.Length > 50 Then
                result = result.Substring(0, 50)
            End If

            Return result.Trim()
        End Function
    End Class
End Namespace
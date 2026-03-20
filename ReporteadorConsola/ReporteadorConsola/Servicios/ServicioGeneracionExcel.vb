Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports Newtonsoft.Json
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports ReporteadorConsola.Modelos

Namespace Servicios
    Public Class ServicioGeneracionExcel


        ''' <summary>
        ''' Genera un archivo Excel a partir de una plantilla y datos
        ''' </summary>
        Public Function GenerarExcelDesdePlantilla(
            rutaPlantilla As String,
            datos As DataTable,
            configColumnasJson As String,
            nombreProgramacion As String) As String

            ' Validar que la plantilla existe
            If Not File.Exists(rutaPlantilla) Then
                Throw New FileNotFoundException($"No se encontró la plantilla: {rutaPlantilla}")
            End If

            ' Crear directorio de salida si no existe
            Dim directorioSalida = "C:\ReporteadorGenerados"
            If Not Directory.Exists(directorioSalida) Then
                Directory.CreateDirectory(directorioSalida)
            End If

            ' Generar nombre de archivo único
            Dim timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss")
            Dim nombreLimpio = LimpiarNombreArchivo(nombreProgramacion)
            Dim nombreArchivoSalida = $"{nombreLimpio}_{timestamp}.xlsx"
            Dim rutaSalida = Path.Combine(directorioSalida, nombreArchivoSalida)

            ' Cargar configuración de columnas
            Dim columnasConfig As List(Of ColumnaConfig) = Nothing
            If Not String.IsNullOrEmpty(configColumnasJson) Then
                Try
                    columnasConfig = JsonConvert.DeserializeObject(Of List(Of ColumnaConfig))(configColumnasJson)
                Catch ex As Exception
                    Console.WriteLine($"[ADVERTENCIA] Error deserializando config de columnas: {ex.Message}")
                End Try
            End If

            ' Procesar el Excel
            Using package As New ExcelPackage(New FileInfo(rutaPlantilla))
                ' Usar la primera hoja
                Dim worksheet = package.Workbook.Worksheets(0)

                ' Determinar dónde empiezan los datos (buscamos la primera fila vacía después de los encabezados)
                Dim filaInicioDatos = EncontrarFilaInicioDatos(worksheet)

                ' Escribir los datos
                EscribirDatosEnHoja(worksheet, datos, filaInicioDatos, columnasConfig)

                ' Aplicar formatos si hay configuración
                If columnasConfig IsNot Nothing AndAlso columnasConfig.Count > 0 Then
                    AplicarFormatos(worksheet, datos, filaInicioDatos, columnasConfig)
                End If

                ' Guardar el nuevo archivo
                package.SaveAs(New FileInfo(rutaSalida))
            End Using

            Return rutaSalida
        End Function

        ''' <summary>
        ''' Encuentra la primera fila vacía para comenzar a escribir datos
        ''' </summary>
        Private Function EncontrarFilaInicioDatos(worksheet As ExcelWorksheet) As Integer
            ' Buscar en las primeras 20 filas una fila que tenga celdas vacías
            For fila As Integer = 1 To 20
                Dim celdaA1 = worksheet.Cells(fila, 1).Value
                Dim celdaB1 = worksheet.Cells(fila, 2).Value

                ' Si la primera columna está vacía, es un buen candidato
                If celdaA1 Is Nothing OrElse String.IsNullOrEmpty(celdaA1.ToString()) Then
                    Return fila
                End If
            Next

            ' Si no encontramos, usar fila 10 como predeterminado
            Return 10
        End Function

        ''' <summary>
        ''' Escribe los datos en la hoja de Excel
        ''' </summary>
        Private Sub EscribirDatosEnHoja(
            worksheet As ExcelWorksheet,
            datos As DataTable,
            filaInicio As Integer,
            columnasConfig As List(Of ColumnaConfig))

            If datos Is Nothing OrElse datos.Rows.Count = 0 Then
                ' Sin datos, escribimos un mensaje
                worksheet.Cells(filaInicio, 1).Value = "No se encontraron datos para este reporte"
                Return
            End If

            ' Mapear columnas del DataTable a posiciones en Excel
            Dim mapaColumnas As New Dictionary(Of String, Integer)

            If columnasConfig IsNot Nothing AndAlso columnasConfig.Count > 0 Then
                ' Usar la configuración ordenada
                Dim columnasOrdenadas = columnasConfig.OrderBy(Function(c) c.Orden).ToList()
                For i As Integer = 0 To columnasOrdenadas.Count - 1
                    mapaColumnas(columnasOrdenadas(i).Campo) = i + 1 ' +1 porque Excel es 1-based
                Next
            Else
                ' Sin configuración, usar el orden del DataTable
                For i As Integer = 0 To datos.Columns.Count - 1
                    mapaColumnas(datos.Columns(i).ColumnName) = i + 1
                Next
            End If

            ' Escribir encabezados si es necesario (solo si la fila de inicio está vacía)
            Dim primeraFilaVacia = worksheet.Cells(filaInicio, 1).Value Is Nothing
            If primeraFilaVacia AndAlso columnasConfig IsNot Nothing Then
                For Each col In columnasConfig.OrderBy(Function(c) c.Orden)
                    If mapaColumnas.ContainsKey(col.Campo) Then
                        worksheet.Cells(filaInicio, mapaColumnas(col.Campo)).Value = col.Titulo
                        worksheet.Cells(filaInicio, mapaColumnas(col.Campo)).Style.Font.Bold = True
                    End If
                Next
                filaInicio += 1
            End If

            ' Escribir datos fila por fila
            For i As Integer = 0 To datos.Rows.Count - 1
                Dim filaExcel = filaInicio + i

                For Each kvp In mapaColumnas
                    Dim nombreColumna = kvp.Key
                    Dim columnaExcel = kvp.Value

                    If datos.Columns.Contains(nombreColumna) Then
                        Dim valor = datos.Rows(i)(nombreColumna)
                        worksheet.Cells(filaExcel, columnaExcel).Value = valor
                    End If
                Next
            Next
        End Sub

        ''' <summary>
        ''' Aplica formatos a las celdas según configuración
        ''' </summary>
        Private Sub AplicarFormatos(
            worksheet As ExcelWorksheet,
            datos As DataTable,
            filaInicio As Integer,
            columnasConfig As List(Of ColumnaConfig))

            ' Determinar la última fila con datos
            Dim ultimaFila = filaInicio + datos.Rows.Count - 1

            For Each col In columnasConfig
                ' Buscar la columna en el DataTable para saber su índice
                Dim indiceColumna As Integer? = Nothing
                For i As Integer = 0 To datos.Columns.Count - 1
                    If datos.Columns(i).ColumnName = col.Campo Then
                        indiceColumna = i + 1
                        Exit For
                    End If
                Next

                If indiceColumna.HasValue Then
                    ' Aplicar formato al rango de datos
                    Dim rango = worksheet.Cells(filaInicio, indiceColumna.Value, ultimaFila, indiceColumna.Value)

                    Select Case col.Formato
                        Case 0 ' Moneda
                            rango.Style.Numberformat.Format = "$ #,##0.00"
                        Case 1 ' Número
                            rango.Style.Numberformat.Format = "#,##0.00"
                        Case 2 ' Número Entero
                            rango.Style.Numberformat.Format = "#,##0"
                        Case 3 ' Porcentaje
                            rango.Style.Numberformat.Format = "0.00%"
                        Case 4 ' Fecha
                            rango.Style.Numberformat.Format = "dd/MM/yyyy"
                        Case 5 ' Texto
                            rango.Style.Numberformat.Format = "@"
                        Case Else ' Sin formato
                            ' No hacer nada
                    End Select
                End If
            Next

            ' Autoajustar columnas
            worksheet.Cells(worksheet.Dimension.Address).AutoFitColumns()
        End Sub

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
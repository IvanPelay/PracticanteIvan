Imports System
Imports System.Data
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports ReporteadorConsola.Modelos

Namespace Servicios

    Public Class ServicioBaseDeDatos

        Private ReadOnly _conexion As String
        Private ReadOnly _logPath As String
        Private ReadOnly _config As ConfiguracionReporteador

        Public Sub New(conexion As String, config As ConfiguracionReporteador)
            _conexion = conexion
            _config = config
            _logPath = config.DirectorioLogs

            'Asegurar que exista carpeta de los logs
            If Not Directory.Exists(_logPath) Then
                Directory.CreateDirectory(_logPath)
            End If
        End Sub

        'Obtener las programaciones Pendientes 
        Public Function ObtenerReportesPendientes() As List(Of ReportePendiente)
            Dim resultado As New List(Of ReportePendiente)()

            Try
                Using con As New SqlConnection(_conexion)
                    Using cmd As New SqlCommand("SP016ProcesarColaReportes", con)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@UsuarioSistema", "MOTOR_REPORTEADOR")

                        con.Open()

                        Using lector As SqlDataReader = cmd.ExecuteReader()
                            While lector.Read()
                                Dim reporte As New ReportePendiente()

                                reporte.i_Cve_Programacion = Convert.ToInt32(lector("i_Cve_Programacion"))
                                reporte.i_Cve_Generacion = Convert.ToInt32(lector("i_Cve_Generacion"))
                                reporte.NombreReporte = If(lector("NombreReporte") Is DBNull.Value, "", lector("NombreReporte").ToString())
                                reporte.t_RutaPlantilla = If(lector("t_RutaPlantilla") Is DBNull.Value, "", lector("t_RutaPlantilla").ToString())
                                reporte.t_FormatoSalida = If(lector("t_FormatoSalida") Is DBNull.Value, "", lector("t_FormatoSalida").ToString())
                                reporte.t_ParametrosConfig = If(lector("t_ParametrosConfig") Is DBNull.Value, "", lector("t_ParametrosConfig").ToString())
                                reporte.t_ColumnasConfig = If(lector("t_ColumnasConfig") Is DBNull.Value, "", lector("t_ColumnasConfig").ToString())
                                reporte.t_Parametros = If(lector("t_Parametros") Is DBNull.Value, "", lector("t_Parametros").ToString())
                                reporte.t_DiasSemana = If(lector("t_DiasSemana") Is DBNull.Value, "", lector("t_DiasSemana").ToString())
                                reporte.i_DiaMes = If(lector("i_DiaMes") Is DBNull.Value, Nothing, Convert.ToInt32(lector("i_DiaMes")))
                                reporte.t_Frecuencia = If(lector("t_Frecuencia") Is DBNull.Value, "", lector("t_frecuencia").ToString())
                                reporte.t_NombreVista = If(lector("t_NombreVista") Is DBNull.Value, "", lector("t_NombreVista").ToString())
                                reporte.t_NombreSP = If(lector("t_NombreSP") Is DBNull.Value, "", lector("t_NombreSP").ToString())
                                reporte.f_ViegnciaInicio = Convert.ToDateTime(lector("f_VigenciaInicio"))
                                reporte.f_VigenciaFin = If(lector("f_VigenciaFin") Is DBNull.Value, Nothing, Convert.ToDateTime(lector("f_VigenciaFin")))

                                resultado.Add(reporte)
                            End While

                        End Using

                    End Using
                End Using

                Console.WriteLine($"[DB] SP016ProcesarColaReportes ejecutado → {resultado.Count} reportes pendientes")
            Catch ex As Exception
                Console.WriteLine($"[ERROR] ObtenerReportesPendientes: {ex.Message}")
                LogError("ObtenerReportesPendientes", ex)
                Throw
            End Try

            Return resultado

        End Function


        'Ejecutar el reporte y obtener los resultados
        ' si tiene sp obtiene sus parametros obligatorios y resuelve el json con el inicio de periodo y fin del periodo
        Public Function EjecutarConsultaReporte(reporte As ReportePendiente) As DataTable
            If reporte.TieneSP Then
                Return EjecutarSP(reporte)
            Else
                Return EjecutarVista(reporte.t_NombreVista)
            End If
        End Function

        Private Function EjecutarSP(reporte As ReportePendiente) As DataTable
            Dim resultado As New DataTable()
            Try
                Using con As New SqlConnection(_conexion)
                    con.Open()
                    'obtener parametros obligatorios del SP
                    Dim obligatorios = ObtenerParametrosSP(con, reporte.t_NombreSP)

                    'resolver parametros con validación de cobertura
                    Dim resolutor = New ServicioCalculosFecha() 'probar como funciona con DateTime.Now()

                    Dim parametros = resolutor.Resolver(
                        reporte.t_Parametros,
                        reporte.t_ParametrosConfig,
                        reporte.t_Frecuencia,
                        obligatorios)

                    'EjecutarSP
                    Using cmd As New SqlCommand(reporte.t_NombreSP, con)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandTimeout = 120

                        For Each kvp In parametros
                            Dim nombre = If(kvp.Key.StartsWith("@"), kvp.Key, "@" & kvp.Key)
                            If obligatorios.Contains(nombre.ToUpperInvariant()) Then
                                cmd.Parameters.AddWithValue(nombre, If(kvp.Value Is Nothing, DBNull.Value, kvp.Value))
                            End If
                        Next

                        Using da As New SqlDataAdapter(cmd)
                            da.Fill(resultado)
                        End Using
                    End Using

                End Using

                Console.WriteLine($"[DB] SP '{reporte.t_NombreSP}' -> {resultado.Rows.Count} registros")
            Catch ex As InvalidOperationException
                'error de cobertura: re lanzar con contexto
                Throw New Exception($"Reporte '{reporte.NombreReporte}': {ex.Message}", ex)
            Catch ex As Exception
                LogError($"EjecutarSP_{reporte.t_NombreSP}", ex)
                Throw New Exception($"Error ejecutando SP '{reporte.t_NombreSP}': {ex.Message}", ex)

            End Try
            Return resultado
        End Function

        Private Function EjecutarVista(nombreVista As String) As DataTable
            Dim resultado As New DataTable()
            Try
                Using con As New SqlConnection(_conexion)
                    Using cmd As New SqlCommand($"SELECT * FROM [{nombreVista}]", con)
                        cmd.CommandTimeout = 120
                        con.Open()
                        Using da As New SqlDataAdapter(cmd)
                            da.Fill(resultado)
                        End Using
                    End Using
                End Using
                Console.WriteLine($"[DB] Vista '{nombreVista}' -> {resultado.Rows.Count} registros")
            Catch ex As Exception
                LogError($"EjecutarVista_{nombreVista}", ex)
                Throw New Exception($"Error ejecutando la vista '{nombreVista}': {ex.Message}", ex)
            End Try
            Return resultado
        End Function

        'Debolvemos todos los parametros del SP
        Public Function ObtenerParametrosSP(con As SqlConnection, nombreSP As String) As HashSet(Of String)
            Dim resultado As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            Try
                Using cmd As New SqlCommand(
                "SELECT UPPER(name) FROM sys.parameter " &
                "WHERE object_id = OBJECT_ID(@sp)", con)
                    cmd.Parameters.AddWithValue("@sp", nombreSP)
                    Using r = cmd.ExecuteReader()
                        While r.Read()
                            resultado.Add(r.GetString(0))
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Console.WriteLine($"[ADVERTENCIA] No se obtuvieron parámetros de '{nombreSP}': {ex.Message}")
            End Try
            Return resultado
        End Function


        'Actualizar el estado de una generacion para que nos marque completado o fallido
        Public Sub ActualizarEstadoGeneracion(iCveGeneracion As Integer, exito As Boolean, rutaDocumento As String,
                                              registros As Integer, errorMsg As String)

            Dim proceso As String = If(exito, "COMPLETADO", "FALLIDO")

            Dim query As String =
                        "UPDATE Bit016GeneracionReporteador SET " &
                        "f_FechaFin = GETDATE(), " &
                        "t_Proceso = @proceso, " &
                        "i_RegistrosProcesados = @registros, " &
                        "t_RutaDocumento = @ruta, " &
                        "t_Error = @error " &
                        "WHERE i_Cve_Generacion = @id"

            Try
                Using con As New SqlConnection(_conexion)
                    Using cmd As New SqlCommand(query, con)
                        cmd.Parameters.AddWithValue("@id", iCveGeneracion)
                        cmd.Parameters.AddWithValue("@proceso", proceso)
                        cmd.Parameters.AddWithValue("@registros", registros)
                        cmd.Parameters.AddWithValue("@ruta", If(String.IsNullOrEmpty(rutaDocumento),
                                                    DBNull.Value, CObj(rutaDocumento)))
                        cmd.Parameters.AddWithValue("@error", If(String.IsNullOrEmpty(errorMsg),
                                                    DBNull.Value, CObj(errorMsg)))
                        con.Open()
                        cmd.ExecuteNonQuery()

                    End Using
                End Using
            Catch ex As Exception
                Console.WriteLine($"[ERROR] ActualizarEstadoGeneracion: {ex.Message}")
                LogError("ActualizarEstadoGeneracion", ex)
            End Try

        End Sub

        'Sistema de Logs
        Private Sub LogError(origen As String, ex As Exception)
            Try
                Dim logFile = Path.Combine(_logPath, $"errores_{DateTime.Now:yyyyMMdd}.log")

                Dim mensaje As String =
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " &
                    $"[{origen}] {ex.Message}" & Environment.NewLine &
                    $"STACKTRACE:" & Environment.NewLine &
                    $"{ex.StackTrace}" & Environment.NewLine &
                    $"---------------------------------------------------" &
                    Environment.NewLine

                File.AppendAllText(logFile, mensaje)

            Catch
                'Si falla no hacemos nada o la cosa truena
            End Try
        End Sub

    End Class
End Namespace

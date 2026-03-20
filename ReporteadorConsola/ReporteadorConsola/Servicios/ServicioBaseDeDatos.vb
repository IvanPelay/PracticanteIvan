Imports System
Imports System.Data
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
                                reporte.t_Consulta = If(lector("t_Consulta") Is DBNull.Value, "", lector("t_Consulta").ToString())
                                reporte.t_FormatoSalida = If(lector("t_FormatoSalida") Is DBNull.Value, "", lector("t_FormatoSalida").ToString())
                                reporte.t_ParametrosConfig = If(lector("t_ParametrosConfig") Is DBNull.Value, "", lector("t_ParametrosConfig").ToString())
                                reporte.t_ColumnasConfig = If(lector("t_ColumnasConfig") Is DBNull.Value, "", lector("t_ColumnasConfig").ToString())
                                reporte.t_Parametros = If(lector("t_Parametros") Is DBNull.Value, "", lector("t_Parametros").ToString())
                                reporte.t_DiasSemana = If(lector("t_DiasSemana") Is DBNull.Value, "", lector("t_DiasSemana").ToString())
                                reporte.i_DiaMes = If(lector("i_DiaMes") Is DBNull.Value, Nothing, Convert.ToInt32(lector("i_DiaMes")))

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


        'Ejecutar consulta del Reporte
        Public Function EjecutarConsulta(consulta As String, parametros As Dictionary(Of String, Object)) As DataTable

            Dim resultado As New DataTable()

            Try
                Using con As New SqlConnection(_conexion)
                    Using cmd As New SqlCommand(consulta, con)
                        cmd.CommandTimeout = _config.CommandTimeout

                        If parametros IsNot Nothing Then
                            For Each p In parametros

                                Dim nombre = If(p.Key.StartsWith("@"), p.Key, "@" & p.Key)

                                cmd.Parameters.AddWithValue(nombre, If(p.Value Is Nothing, DBNull.Value, p.Value))
                            Next
                        End If

                        con.Open()

                        Using da As New SqlDataAdapter(cmd)
                            da.Fill(resultado)
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                LogError("EjecucionConsulta", ex)
                Throw New Exception("Error ejecutando consulta del reporte", ex)
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

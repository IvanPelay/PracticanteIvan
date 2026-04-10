Imports System
Imports System.Data
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports ReporteadorConsola.Modelos

Imports Gsol
Imports Gsol.BaseDatos.Operaciones
Imports Wma.Exceptions

Namespace Servicios

    Public Class ServicioBaseDeDatos

        Private ReadOnly _operaciones As OperacionesCatalogo
        Private ReadOnly _sistema As Organismo
        Private ReadOnly _logPath As String
        Private ReadOnly _config As ConfiguracionReporteador

        Public Sub New(operaciones As OperacionesCatalogo, sistema As Organismo, config As ConfiguracionReporteador)
            _operaciones = operaciones
            _sistema = sistema
            _config = config
            _logPath = config.DirectorioLogs

            'Asegurar que exista carpeta de los logs
            If Not Directory.Exists(_logPath) Then
                Directory.CreateDirectory(_logPath)
            End If
        End Sub

        'Obtener las programaciones Pendientes 
        Public Function ObtenerReportesPendientes() As TagWatcher
            Dim estatus As New TagWatcher
            Dim resultado As New List(Of ReportePendiente)()

            Try
                Dim query As String = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED EXEC SP016ProcesarColaReportes @UsuarioSistema = 'MOTOR_REPORTEADOR'"
                estatus = _sistema.ConexionSingleton.SQLServerSingletonConexion.EjecutaConsultaDirectaEstandarizadaOpenCloseDataTable(query)

                If estatus.Status = TagWatcher.TypeStatus.Ok Then
                    Dim dt As DataTable = CType(estatus.ObjectReturned, DataTable)

                    For Each lector As DataRow In dt.Rows
                        Dim reporte As New ReportePendiente()

                        reporte.i_Cve_Programacion = Convert.ToInt32(lector("i_Cve_Programacion"))
                        reporte.i_Cve_Generacion = Convert.ToInt32(lector("i_Cve_Generacion"))
                        reporte.NombreReporte = If(lector("NombreReporte") Is DBNull.Value, "", lector("NombreReporte").ToString())
                        reporte.t_RutaPlantilla = If(lector("t_RutaPlantilla") Is DBNull.Value, "", lector("t_RutaPlantilla").ToString())
                        reporte.t_FormatoSalida = If(lector("t_FormatoSalida") Is DBNull.Value, "", lector("t_FormatoSalida").ToString())
                        reporte.t_ParametrosConfig = If(lector("t_ParametrosConfig") Is DBNull.Value, "", lector("t_ParametrosConfig").ToString())
                        reporte.i_FilaInicio = Convert.ToInt32(lector("i_FilaInicio"))
                        reporte.i_ColumnaInicio = Convert.ToInt32(lector("i_ColumnaInicio"))
                        reporte.t_Parametros = If(lector("t_Parametros") Is DBNull.Value, "", lector("t_Parametros").ToString())
                        reporte.t_DiasSemana = If(lector("t_DiasSemana") Is DBNull.Value, "", lector("t_DiasSemana").ToString())
                        reporte.i_DiaMes = If(lector("i_DiaMes") Is DBNull.Value, Nothing, Convert.ToInt32(lector("i_DiaMes")))
                        reporte.t_Frecuencia = If(lector("t_Frecuencia") Is DBNull.Value, "", lector("t_frecuencia").ToString())
                        reporte.t_NombreVista = If(lector("t_NombreVista") Is DBNull.Value, "", lector("t_NombreVista").ToString())
                        reporte.t_NombreSP = If(lector("t_NombreSP") Is DBNull.Value, "", lector("t_NombreSP").ToString())
                        reporte.f_VigenciaInicio = Convert.ToDateTime(lector("f_VigenciaInicio"))
                        reporte.f_VigenciaFin = If(lector("f_VigenciaFin") Is DBNull.Value, Nothing, Convert.ToDateTime(lector("f_VigenciaFin")))

                        resultado.Add(reporte)
                    Next

                    estatus.ObjectReturned = resultado
                    Console.WriteLine($"[DB] SP016ProcesarColaReportes ejecutado → {resultado.Count} reportes pendientes")
                End If

            Catch ex As Exception
                Console.WriteLine($"[ERROR] ObtenerReportesPendientes: {ex.Message}")
                LogError("ObtenerReportesPendientes", ex)
                estatus.SetError(Me, $"Excepcion en obtener Reportes pendientes: {ex.Message}", TagWatcher.ErrorTypes.C6_012_1042)
            End Try

            Return estatus

        End Function


        'Ejecutar el reporte y obtener los resultados
        ' si tiene sp obtiene sus parametros obligatorios y resuelve el json con el inicio de periodo y fin del periodo
        Public Function EjecutarConsultaReporte(reporte As ReportePendiente) As TagWatcher
            If reporte.TieneSP Then
                Return EjecutarSP(reporte)
            Else
                Return EjecutarVista(reporte.t_NombreVista)
            End If
        End Function

        Private Function EjecutarSP(reporte As ReportePendiente) As TagWatcher
            Dim estatus As New TagWatcher()
            Try

                Dim estatusParametros = ObtenerParametrosSP(reporte.t_NombreSP)
                If estatusParametros.Status <> TagWatcher.TypeStatus.Ok Then
                    Return estatusParametros
                End If

                Dim obligatorios = CType(estatusParametros.ObjectReturned, HashSet(Of String))

                Dim resolutor = New ServicioCalculosFecha()
                Dim parametros = resolutor.Resolver(
                    reporte.t_Parametros,
                    reporte.t_ParametrosConfig,
                    reporte.t_Frecuencia,
                    obligatorios)

                Dim queryBuilder As New StringBuilder()
                queryBuilder.Append($"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED EXEC {reporte.t_NombreSP} ")

                Dim primerParametro As Boolean = True
                For Each kvp In parametros
                    Dim nombre = If(kvp.Key.StartsWith("@"), kvp.Key, "@" & kvp.Key)
                    If obligatorios.Contains(nombre.ToUpperInvariant()) Then
                        If Not primerParametro Then queryBuilder.Append(", ")

                        If kvp.Value Is Nothing OrElse kvp.Value Is DBNull.Value Then
                            queryBuilder.Append($"{nombre} = Null")
                        ElseIf TypeOf kvp.Value Is DateTime Then
                            queryBuilder.Append($"{nombre} = '{CType(kvp.Value, DateTime):yyyy-MM-dd HH:mm:ss}'")
                        ElseIf TypeOf kvp.Value Is String Then
                            queryBuilder.Append($"{nombre} = '{kvp.Value.ToString().Replace("'", "''")}'")
                        ElseIf TypeOf kvp.Value Is Boolean Then
                            queryBuilder.Append($"{nombre} = {If(CBool(kvp.Value), 1, 0)}")
                        Else
                            queryBuilder.Append($"{nombre} = {kvp.Value}")
                        End If
                        primerParametro = False
                    End If
                Next

                estatus = _sistema.ConexionSingleton.SQLServerSingletonConexion.EjecutaConsultaDirectaEstandarizadaOpenCloseDataTable(queryBuilder.ToString())

                If estatus.Status = TagWatcher.TypeStatus.Ok Then
                    Dim dt = CType(estatus.ObjectReturned, DataTable)
                    Console.WriteLine($"[DB] SP '{reporte.t_NombreSP}' -> {dt.Rows.Count} registros")
                End If

            Catch ex As InvalidOperationException
                estatus.SetError(Me, $"Reporte '{reporte.NombreReporte}': {ex.Message}", TagWatcher.ErrorTypes.C3_001_3005)
            Catch ex As Exception
                LogError($"EjecutarSP_{reporte.t_NombreSP}", ex)
                estatus.SetError(Me, $"Error ejecutando SP '{reporte.t_NombreSP}': {ex.Message}", TagWatcher.ErrorTypes.C6_012_1042)
            End Try
            Return estatus
        End Function

        Private Function EjecutarVista(nombreVista As String) As TagWatcher
            Dim estatus As New TagWatcher()
            Try
                Dim query As String = $"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED SELECT * FROM [{nombreVista}]"
                estatus = _sistema.ConexionSingleton.SQLServerSingletonConexion.EjecutaConsultaDirectaEstandarizadaOpenCloseDataTable(query)

                If estatus.Status = TagWatcher.TypeStatus.Ok Then
                    Dim dt = CType(estatus.ObjectReturned, DataTable)
                    Console.WriteLine($"[DB] Vista '{nombreVista}' -> {dt.Rows.Count} registros")
                End If

            Catch ex As Exception
                LogError($"EjecutarVista_{nombreVista}", ex)
                estatus.SetError(Me, $"Error ejecutando la vista '{nombreVista}': {ex.Message}", TagWatcher.ErrorTypes.C6_012_1042)
            End Try
            Return estatus
        End Function

        'Debolvemos todos los parametros del SP
        Public Function ObtenerParametrosSP(nombreSP As String) As TagWatcher

            Dim estatus As New TagWatcher()
            Dim resultado As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

            Try

                Dim query As String = $"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED SELECT UPPER(name) AS name FROM sys.parameters WHERE object_id = OBJECT_ID('{nombreSP}')"
                estatus = _sistema.ConexionSingleton.SQLServerSingletonConexion.EjecutaConsultaDirectaEstandarizadaOpenCloseDataTable(query)

                If estatus.Status = TagWatcher.TypeStatus.Ok Then
                    Dim dt = CType(estatus.ObjectReturned, DataTable)
                    For Each r As DataRow In dt.Rows
                        resultado.Add(r("name").ToString())
                    Next
                    estatus.ObjectReturned = resultado
                End If

            Catch ex As Exception
                Console.WriteLine($"[ADVERTENCIA] No se obtuvieron parámetros de '{nombreSP}': {ex.Message}")
                estatus.SetError(Me, $"Fallo de la lectura sys.parameters '{nombreSP}': {ex.Message}", TagWatcher.ErrorTypes.C6_012_1042)
            End Try
            Return estatus
        End Function


        'Actualizar el estado de una generacion para que nos marque completado o fallido
        Public Function ActualizarEstadoGeneracion(iCveGeneracion As Integer, exito As Boolean, rutaDocumento As String,
                                              registros As Integer, errorMsg As String) As TagWatcher

            Dim estatus As New TagWatcher
            Dim proceso As String = If(exito, "COMPLETADO", "FALLIDO")

            'formato seguro para sql
            Dim sqlRuta As String = If(String.IsNullOrEmpty(rutaDocumento), "NULL", $"'{rutaDocumento.Replace("'", "''")}'")
            Dim sqlError As String = If(String.IsNullOrEmpty(errorMsg), "NULL", $"'{errorMsg.Replace("'", "''")}'")

            Dim query As String =
                        $"UPDATE Bit016GeneracionReporteador SET " &
                        $"f_FechaFin = GETDATE(), " &
                        $"t_Proceso = '{proceso}', " &
                        $"i_RegistrosProcesados = {registros}, " &
                        $"t_RutaDocumento = {sqlRuta}, " &
                        $"t_Error = {sqlError} " &
                        $"WHERE i_Cve_Generacion = {iCveGeneracion}"

            Try
                estatus = _sistema.ConexionSingleton.SQLServerSingletonConexion.EjecutaConsultaDirectaEstandarizada(query)
            Catch ex As Exception
                Console.WriteLine($"[ERROR] ActualizarEstadoGeneracion: {ex.Message}")
                LogError("ActualizarEstadoGeneracion", ex)
                estatus.SetError(Me, $"Error al actualizar la bitacora: {ex.Message}", TagWatcher.ErrorTypes.C6_012_1042)
            End Try

            Return estatus
        End Function

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

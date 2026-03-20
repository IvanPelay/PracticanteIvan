Imports System
Imports System.IO
Imports System.Threading
Imports ReporteadorConsola.Servicios
Imports ReporteadorConsola.Modelos
Imports Newtonsoft.Json
Imports System.Configuration

Module Module1

    'Configuracion a la BD
    Private ReadOnly _intervaloEjecucion As Integer = 60000
    Private ReadOnly _cadenaConexion As String = ConfigurationManager.ConnectionStrings("Solium").ConnectionString

    Private _ejecutando As Boolean = True
    Private _servicioDB As ServicioBaseDeDatos
    Private _servicioExcel As ServicioGeneracionExcel
    Private _config As ConfiguracionReporteador

    Sub Main()
        Console.WriteLine("========================================")
        Console.WriteLine("REPORTEADOR AUTOMÁTICO - MODULO EJECUCIÓN")
        Console.WriteLine($"Inicio: {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
        Console.WriteLine("========================================")

        Try
            'Cargamos la configuracion
            _config = ConfiguracionReporteador.Instance
            Console.WriteLine($"Configuración cargada desde: {AppDomain.CurrentDomain.SetupInformation.ConfigurationFile}")

            Dim erroresConfiguracion = _config.ValidarConfiguracion()
            If erroresConfiguracion.Count > 0 Then
                Console.WriteLine("Errores en la configuración:")
                For Each miss In erroresConfiguracion
                    Console.WriteLine($" - {miss}")
                Next
                Console.WriteLine("Presione una tecla para salir...")
                Console.ReadKey()
                Return
            End If

            'Inicializar los servicios
            _servicioDB = New ServicioBaseDeDatos(_cadenaConexion)
            _servicioExcel = New ServicioGeneracionExcel()

            'configurar manejador
            AddHandler Console.CancelKeyPress, AddressOf ManejadorSalida

            'mostrar configuracion
            MostrarConfiguracion()

            'bucle principal del motor
            While _ejecutando
                EjecutarCicloProcesamiento()
                Thread.Sleep(_intervaloEjecucion)
            End While

        Catch ex As Exception
            Console.WriteLine($"[ERROR FATAL] {ex.Message}")
            Console.WriteLine(ex.StackTrace)
            LogErrorGlobal("Main", ex)
        End Try

        Console.WriteLine("Reporteador Detenido")
    End Sub

    'mostramos la configuracion actual
    Private Sub MostrarConfiguracion()
        Console.WriteLine("------------------------------------------")
        Console.WriteLine("Configuración actual:")
        Console.WriteLine($"  - BD: {_config.Conexion}")
        Console.WriteLine($"  - Directorio salida: {_config.DirectorioSalida}")
        Console.WriteLine($"  - Directorio plantillas: {_config.DirectorioPlantillas}")
        Console.WriteLine($"  - Directorio logs: {_config.DirectorioLogs}")
        Console.WriteLine($"  - Intervalo: {_config.IntervaloEjecucion / 1000} seg")
        Console.WriteLine($"  - Timeout consultas: {_config.CommandTimeout} seg")
        Console.WriteLine($"  - Usuario sistema: {_config.UsuarioSistema}")
        Console.WriteLine("------------------------------------------")
    End Sub

    'Ejecutamos un ciclo completo de procesamiento
    Private Sub EjecutarCicloProcesamiento()
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Iniciando ciclo de procesamiento")

        Try
            'Obtenemos los reportes pendientes
            Dim reportesPendientes = _servicioDB.ObtenerReportesPendientes()

            If reportesPendientes Is Nothing OrElse reportesPendientes.Count = 0 Then
                Console.WriteLine("No hay reportes pendientes por procesar")
            End If

            Console.WriteLine($"Se encontraron {reportesPendientes.Count} reportes pendientes")

            For Each reporte In reportesPendientes
                ProcesarReporte(reporte)
            Next

        Catch ex As Exception

            Console.WriteLine($"[ERROR] EjecutarCicloProcesamiento: {ex.Message}")
            LogErrorGlobal("EjecutarCicloProsesamiento", ex)

        End Try
    End Sub

    'Procesar un reporte individual
    Private Sub ProcesarReporte(reporte As ReportePendiente)
        Console.WriteLine($"Procesando: {reporte.NombreReporte} (Gen: {reporte.i_Cve_Generacion})")

        Dim rutaDocumento As String = Nothing
        Dim registrosProcesados = 0
        Dim exito As Boolean = False
        Dim errorMsg As String = Nothing

        Try
            'Validamos que tenemos lo minimo necesario
            ValidarReporte(reporte)

            'Construir parametros de la consulta
            Dim parametros = ConstruirParametros(reporte)

            'Ejecutar consulta SQL
            Console.WriteLine("Ejecutando la consulta SQL ...")
            Dim datos = _servicioDB.EjecutarConsulta(reporte.t_Consulta, parametros)
            registrosProcesados = If(datos Is Nothing, 0, datos.Rows.Count)
            Console.WriteLine($" Registros Obtenidos: {registrosProcesados}")

            'Generar archivo segun formato xlsx
            Select Case reporte.t_FormatoSalida.ToUpper()
                Case "XLSX", "XLS", ""
                    rutaDocumento = _servicioExcel.GenerarExcelDesdePlantilla(
                        reporte.t_RutaPlantilla,
                        datos,
                        reporte.t_ColumnasConfig,
                        reporte.NombreReporte
                    )
                Case Else
                    Throw New Exception($"Formato no soportado: {reporte.t_FormatoSalida}")
            End Select

            Console.WriteLine($"Archivo generado: {rutaDocumento}")
            exito = True

        Catch ex As Exception

            errorMsg = ex.Message
            Console.WriteLine($" ERROR {errorMsg}")
            LogErrorGlobal($"Procesar Reporte_ {reporte.i_Cve_Generacion}", ex)
        End Try


        'Actualizar la bitacora
        Try
            _servicioDB.ActualizarEstadoGeneracion(
                reporte.i_Cve_Generacion,
                exito,
                rutaDocumento,
                registrosProcesados,
                errorMsg
            )
            Console.WriteLine($" Estado Actualizado: {If(exito, "COMPLETADO", "FALLIDO")}")
        Catch ex As Exception
            Console.WriteLine($" Error Actualizando a la Bitácora: {ex.Message} ")
        End Try

        Console.WriteLine($"Procesamiento Finalizado: {If(exito, "EXITO", "FALLIDO")}")
        Console.WriteLine()

    End Sub

    'Validar que el reporte contenga los valores minimos necesarios
    Private Sub ValidarReporte(reporte As ReportePendiente)
        If String.IsNullOrEmpty(reporte.t_Consulta) Then
            Throw New Exception("La consulta SQL esta vacia")
        End If

        If String.IsNullOrEmpty(reporte.t_RutaPlantilla) AndAlso reporte.t_FormatoSalida.ToUpper() = "XLSX" Then
            Throw New Exception("La ruta de plantilla es Requerida para el Repórte")
        End If

        If Not File.Exists(reporte.t_RutaPlantilla) AndAlso reporte.t_FormatoSalida.ToUpper() = "XLSX" Then
            Throw New Exception($"No se encuentra la plantilla: {reporte.t_RutaPlantilla}")
        End If
    End Sub

    'Construir el diccionario de parametros para la consulta SQL
    Private Function ConstruirParametros(reporte As ReportePendiente) As Dictionary(Of String, Object)
        Dim parametros = New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)

        Try
            'Agregamos parametros fijos del sistema
            parametros("@FechaActual") = DateTime.Now.Date
            parametros("@FechaHoraActual") = DateTime.Now
            parametros("@UsuarioEjecucion") = "MOTOR_REPORTEADOR"

            'Procesar parametros de la programacion JSON
            If Not String.IsNullOrEmpty(reporte.t_Parametros) Then
                Dim parametrosProgramacion = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(reporte.t_Parametros)
                If parametrosProgramacion IsNot Nothing Then
                    For Each kvp In parametrosProgramacion
                        Dim nombreParametro = If(kvp.Key.StartsWith("@"), kvp.Key, "@" & kvp.Key)
                        parametros(nombreParametro) = kvp.Value
                    Next
                End If
            End If

            'Procesar configuracion de parametros en caso de valor default
            If Not String.IsNullOrEmpty(reporte.t_ParametrosConfig) Then
                Dim ConfiguracionParametros = JsonConvert.DeserializeObject(Of List(Of ParametroConfig))(reporte.t_ParametrosConfig)
                If ConfiguracionParametros IsNot Nothing Then
                    For Each parametro In ConfiguracionParametros
                        Dim nombreParametro = If(parametro.Nombre.StartsWith("@"), parametro.Nombre, "@" & parametro.Nombre)

                        'Si no existe el parametro usamos el valor default
                        If Not parametros.ContainsKey(nombreParametro) AndAlso Not String.IsNullOrEmpty(parametro.ValorDefault) Then
                            parametros(nombreParametro) = ConvertirValoresDefault(parametro.ValorDefault, parametro.Tipo)
                        End If
                    Next
                End If


            End If

        Catch ex As Exception
            Console.WriteLine($" [ADVERTENCIA] Error construyendo los parametros: {ex.Message}")
        End Try

        Return parametros

    End Function

    Private Function ConvertirValoresDefault(valor As String, tipo As String) As Object
        Select Case tipo.ToUpper()
            Case "INT", "INTEGER", "BIGINT", "SMALLINT"
                Dim resultado As Integer
                If Integer.TryParse(valor, resultado) Then Return resultado
                Return 0
            Case "DECIMAL", "NUMERIC", "FLOAT", "MONEY"
                Dim resultado As Decimal
                If Decimal.TryParse(valor, resultado) Then Return resultado
                Return 0D
            Case "DATE", "DATETIME", "SMALLDATETIME"
                Dim resultado As DateTime
                If DateTime.TryParse(valor, resultado) Then Return resultado
                Return DateTime.Now
            Case "BIT", "BOOLEAN"
                If valor.Equals("1", StringComparison.OrdinalIgnoreCase) OrElse valor.Equals("true", StringComparison.OrdinalIgnoreCase) Then
                    Return True
                End If
                Return False
            Case Else
                Return valor
        End Select

    End Function

    'Limpiar nombre del archivo
    Private Function LimpiarNombreArchivo(nombre As String) As String
        If String.IsNullOrEmpty(nombre) Then Return "Reporte"
        Dim IDInvalidos = Path.GetInvalidFileNameChars()
        Dim resultado = New String(nombre.Where(Function(c) Not IDInvalidos.Contains(c)).ToArray())
        If resultado.Length > 50 Then resultado = resultado.Substring(0, 50)
        Return resultado.Trim()
    End Function

    'Manejador de salida nos permitira hacer copiar y pegar en consola
    Private Sub ManejadorSalida(evento As Object, e As ConsoleCancelEventArgs)
        Console.WriteLine()
        Console.WriteLine("Deteniendo el reporteador ...")
        _ejecutando = False
        e.Cancel = True
    End Sub

    'Log de errores global
    Private Sub LogErrorGlobal(origen As String, ex As Exception)
        Try
            Dim logPath = _config?.DirectorioLogs ?? "C:\ReporteadorLogs"
            If Not Directory.Exists(logPath) Then
                Directory.CreateDirectory(logPath)
            End If

            Dim archivoLog = Path.Combine(logPath, $"errores_fatal_{DateTime.Now:yyyyMMdd}.log")
            Dim mensaje As String = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{origen}] {ex.Message}" &
                                       Environment.NewLine & ex.StackTrace &
                                       Environment.NewLine & "--------------------------------------------------" &
                                       Environment.NewLine
            File.AppendAllText(archivoLog, mensaje)

        Catch
            'Ignoramos los errores que pueda tener el log

        End Try
    End Sub

End Module

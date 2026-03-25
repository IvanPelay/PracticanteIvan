Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports Newtonsoft.Json
Imports ReporteadorConsola.Modelos

Namespace Servicios
    ''' <summary>
    '''   - INICIO_PERIODO / FIN_PERIODO: calculan el rango caído
    '''     automáticamente según la frecuencia del reporte.
    '''     El usuario ya NO necesita escribir expresiones de fecha
    '''     para el rango principal; el sistema lo deduce solo.
    '''
    '''   - Validación de parámetros obligatorios: antes de devolver
    '''     el diccionario verifica que todos los parámetros del SP
    '''     estén cubiertos (sin valor default en sys.parameters).
    '''
    ''' TABLA DE EXPRESIONES:
    ''' ┌─────────────────┬──────────────────────────────────────────┐
    ''' │ Expresión       │ Valor calculado                          │
    ''' ├─────────────────┼──────────────────────────────────────────┤
    ''' │ INICIO_PERIODO  │ Inicio del período caído según frecuencia│
    ''' │ FIN_PERIODO     │ Fin del período caído según frecuencia   │
    ''' │ HOY             │ DateTime.Today                           │
    ''' │ HOY-N / HOY+N   │ DateTime.Today +/- N días               │
    ''' │ INICIO_SEMANA   │ Lunes de la semana anterior              │
    ''' │ FIN_SEMANA      │ Domingo de la semana anterior            │
    ''' │ INICIO_MES      │ Día 1 del mes anterior                   │
    ''' │ FIN_MES         │ Último día del mes anterior              │
    ''' │ INICIO_QUINCENA │ Inicio de la quincena anterior           │
    ''' │ FIN_QUINCENA    │ Fin de la quincena anterior              │
    ''' │ (fecha fija)    │ Se parsea y convierte al tipo declarado  │
    ''' │ (valor fijo)    │ Se convierte al tipo declarado (RFC, etc)│
    ''' └─────────────────┴──────────────────────────────────────────┘
    '''
    ''' RANGOS POR FRECUENCIA para INICIO_PERIODO / FIN_PERIODO:
    ''' ┌────┬──────────────────────────────────────────────────────┐
    ''' │ D  │ Ayer (DateTime.Today.AddDays(-1))                    │
    ''' │ S  │ Lunes → Domingo de la semana anterior                │
    ''' │ M  │ Día 1 → último día del mes anterior                  │
    ''' │ Q  │ Día 1/16 → 15/último del período quincenal anterior  │
    ''' │ U  │ No aplica: usa fecha fija del JSON                   │
    ''' └────┴──────────────────────────────────────────────────────┘
    ''' </summary>
    Public Class ServicioCalculosFecha

        Private ReadOnly _fechaReferencia As DateTime

        Public Sub New(Optional fechaReferencia As DateTime? = Nothing)
            _fechaReferencia = If(fechaReferencia.HasValue, fechaReferencia.Value, DateTime.Now)
        End Sub

        '--------------
        'Metodo Principal
        '------------
        ''''<summary>
        ''''Resuelve todos los parametros del JSON y devuelve el diccionario 
        ''''listo para insertar en la vista o o al procedure
        ''''</summary>
        '''' <param name="jsonParametros">t_Parametros de la programación</param>
        '''' <param name="jsonParametrosConfig">t_ParametrosConfig de la plantilla</param>
        '''' <param name="frecuencia">D, S, M, Q, U</param>
        '''' <param name="parametrosObligatoriosSP">
        ''''   Nombres de parámetros requeridos por el SP (sin default).
        ''''   Si se pasa, se valida cobertura antes de devolver.
        '''' </param>

        Public Function Resolver(jsonParametros As String, jsonParametrosConfig As String,
                                 frecuencia As String, Optional parametrosObligatoriosSP As HashSet(Of String) = Nothing
                                 ) As Dictionary(Of String, Object)

            Dim resultado As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)

            'parametros del sistema
            resultado("@FechaActual") = _fechaReferencia.Date
            resultado("@FechaHoraActual") = _fechaReferencia
            resultado("@UsusarioEjecucion") = "MOTOR_REPORTEADOR"

            'Resolver parámetros de la programación
            If Not String.IsNullOrWhiteSpace(jsonParametros) Then
                Try
                    Dim valores = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(jsonParametros)

                    If valores IsNot Nothing Then
                        For Each kvp In valores
                            Dim nombre = NormalizarNombre(kvp.Key)
                            Dim config = BuscarConfig(jsonParametrosConfig, kvp.Key)
                            resultado(nombre) = ResolverValor(kvp.Value, config?.Tipo, frecuencia)
                        Next
                    End If

                Catch ex As Exception
                    Console.WriteLine($"[ADVERTENCIA] Error en parámetros de programación: {ex.Message}")

                End Try
            End If

            'Aplicar Default de ParametrosConfig para los no cubiertos
            If Not String.IsNullOrWhiteSpace(jsonParametrosConfig) Then
                Try
                    Dim configs = JsonConvert.DeserializeObject(Of List(Of ParametroConfig))(jsonParametrosConfig)

                    If configs IsNot Nothing Then
                        For Each cfg In configs
                            Dim nombre = NormalizarNombre(cfg.Nombre)
                            If Not resultado.ContainsKey(nombre) AndAlso Not String.IsNullOrWhiteSpace(cfg.ValorDefault) Then
                                resultado(nombre) = ResolverValor(cfg.ValorDefault, cfg.Tipo, frecuencia)
                            End If
                        Next
                    End If

                Catch ex As Exception
                    Console.WriteLine($"[ADVERTENCIA] Error en defaults de Config: {ex.Message}")

                End Try
            End If

            'validar que tenemos todos los parametros obligatorios en el SP
            If parametrosObligatoriosSP IsNot Nothing AndAlso parametrosObligatoriosSP.Count > 0 Then
                ValidarCobertura(resultado, parametrosObligatoriosSP)
            End If

            Return resultado

        End Function

        'Resolucion de valores individuales
        Public Function ResolverValor(valor As String, tipo As String, frecuencia As String) As Object

            If String.IsNullOrWhiteSpace(valor) Then Return DBNull.Value

            Dim v = valor.Trim().ToUpperInvariant()

            If EsExpresionDinamica(v) Then
                Return ResolverExpresion(v, frecuencia)
            End If

            Return ConvertirFijo(valor.Trim(), tipo)

        End Function

        'Deteccion de Expesiones Dinamicas
        Private Function EsExpresionDinamica(v As String) As Boolean
            Return v = "HOY" OrElse
                   v = "INICIO_PERIODO" OrElse
                   v = "FIN_PERIODO" OrElse
                   v = "INICIO_SEMANA" OrElse
                   v = "FIN_SEMANA" OrElse
                   v = "INICIO_MES" OrElse
                   v = "FIN_MES" OrElse
                   v = "INICIO_QUINCENA" OrElse
                   v = "FIN_QUINCENA" OrElse
                   v.StartsWith("HOY-") OrElse
                   v.StartsWith("HOY+")
        End Function

        'resolucion de expresiones
        Private Function ResolverExpresion(expresion As String, frecuencia As String) As DateTime
            Select Case expresion
                Case "HOY"
                    Return _fechaReferencia.Date
                    'rango automatico por frecuencia
                Case "INICIO_PERIODO"
                    Return CalcularInicioPeriodo(frecuencia)
                Case "FIN_PERIODO"
                    Return CalcularFinPeriodo(frecuencia)

                    'expesiones de semana
                Case "INICIO_SEMANA"
                    Return LunesSemanaAnterior()
                Case "FIN_SEMANA"
                    Return LunesSemanaAnterior().AddDays(6)

                    'expresiones de mes
                Case "INICIO_MES"
                    Dim ma = _fechaReferencia.AddMonths(-1)
                    Return New DateTime(ma.Year, ma.Month, 1)

                Case "FIN_MES"
                    Dim ma = _fechaReferencia.AddMonths(-1)
                    Return New DateTime(ma.Year, ma.Month, DateTime.DaysInMonth(ma.Year, ma.Month))

                    'expresiones de quincena
                Case "INICIO_QUINCENA"
                    Return InicioQuincenaAnterior()
                Case "FIN_QUINCENA"
                    Return FinQuincenaAnterior()

                Case Else
                    'HOY-N o HOY+N
                    If expresion.StartsWith("HOY-") OrElse expresion.StartsWith("HOY+") Then
                        Dim signo = If(expresion.StartsWith("HOY-"), -1, 1)
                        Dim n As Integer
                        If Integer.TryParse(expresion.Substring(4), n) Then
                            Return _fechaReferencia.Date.AddDays(signo * n)
                        End If
                    End If

                    Console.WriteLine($"[ADVERTENCIA] Expresion Desconocida: '{expresion}' Se usara hoy")
                    Return _fechaReferencia.Date

            End Select
        End Function

        'calcular periodo inicio y fin segun la frecuencia
        'calcular el inicio del periodo caído según la frecuencia
        'este es el rango que debe consultar nuestro reporte en la bd

        Private Function CalcularInicioPeriodo(frecuencia As String) As DateTime
            Select Case frecuencia?.ToUpperInvariant()
                Case "D"
                    'Periodo caido diario es decir ayer
                    Return _fechaReferencia.Date.AddDays(-1)

                Case "S"
                    'Lunes de la semana anterior completa
                    Return LunesSemanaAnterior()

                Case "M"
                    'Dia 1 del mes anterior
                    Dim ma = _fechaReferencia.AddMonths(-1)
                    Return New DateTime(ma.Year, ma.Month, 1)

                Case "Q"
                    'inicio de la quincena anterior
                    Return InicioQuincenaAnterior()

                Case "U"
                    'unica normalmente el usuario debe haber dado una fecha fija para la opcion unica si
                    'se llega al inicio periodo hubo algun fallo por lo que damos un error de configuracion
                    Console.WriteLine("[ADVERTENCIA] INICIO_PERIODO en frecuencia U: use fecha fija en el JSON")
                    Return _fechaReferencia.Date

                Case Else
                    Console.WriteLine(
                        $"[ADVERTENCIA] Frecuencia desconocida '{frecuencia}' " &
                        "para INICIO_PERIODO. Se usa hoy.")
                    Return _fechaReferencia.Date
            End Select
        End Function

        'calcular fin del periodo caido segun la frecuencia
        Private Function CalcularFinPeriodo(frecuencia As String) As DateTime
            Select Case frecuencia?.ToUpperInvariant
                Case "D"
                    'periodo caido diario es decir ayer
                    Return _fechaReferencia.Date.AddDays(-1)

                Case "S"
                    'Domingo de la semana anterior (lunes + 6)
                    Return LunesSemanaAnterior().AddDays(6)

                Case "M"
                    'Ultimo dia del mes anterior
                    Dim ma = _fechaReferencia.AddMonths(-1)
                    Return New DateTime(ma.Year, ma.Month, DateTime.DaysInMonth(ma.Year, ma.Month))

                Case "Q"
                    Return FinQuincenaAnterior()

                Case "U"
                    'definitivamente no deberia llegar hasta aqui igual marcamos error
                    Console.WriteLine(
                        "[ADVERTENCIA] FIN_PERIODO en frecuencia U: " &
                        "use fecha fija en el JSON.")
                    Return _fechaReferencia.Date

                Case Else
                    Console.WriteLine(
                      $"[ADVERTENCIA] Frecuencia desconocida '{frecuencia}' " &
                      "para FIN_PERIODO. Se usará hoy.")
                    Return _fechaReferencia.Date

            End Select
        End Function

        'calculos de periodos auxiliares semana y quincena
        'devolvemos lunes de la semana anterior y manejamos que la semana comienza en lunes
        Private Function LunesSemanaAnterior() As DateTime
            Dim hoy = _fechaReferencia.Date
            Dim diasDesdeLunes = (CInt(hoy.DayOfWeek) + 6) Mod 7
            Dim lunesEsta = hoy.AddDays(-diasDesdeLunes)
            Return lunesEsta.AddDays(-7)
        End Function

        Private Function InicioQuincenaAnterior() As DateTime
            Dim hoy = _fechaReferencia.Date
            If hoy.Day <= 15 Then
                'estamos en la primera quincena --> el rango a consultar es del 16 a fin de mes del mes pasado
                Dim mp = hoy.AddMonths(-1)
                Return New DateTime(mp.Year, mp.Month, 16)
            Else
                'estamos en la segunda quincena --> al rango a consultar es del 1 al 15 de este mes
                Return New DateTime(hoy.Year, hoy.Month, 1)
            End If
        End Function

        Private Function FinQuincenaAnterior() As DateTime
            Dim hoy = _fechaReferencia.Date
            If hoy.Day <= 15 Then
                Dim mp = hoy.AddMonths(1)
                Return New DateTime(mp.Year, mp.Month, DateTime.DaysInMonth(mp.Year, mp.Month))
            Else
                Return New DateTime(hoy.Year, hoy.Month, 15)
            End If
        End Function

        'validacion de cobertura (tenemos todos los parametros necesarios??
        Private Sub ValidarCobertura(parametrosResueltos As Dictionary(Of String, Object),
                                     parametrosObligatorios As HashSet(Of String))

            Dim faltantes As New List(Of String)

            For Each nombreSP In parametrosObligatorios
                'Normalizar para comparar
                Dim nombre = If(nombreSP.StartsWith("@"), nombreSP, "@" & nombreSP)

                If Not parametrosResueltos.ContainsKey(nombre) Then
                    faltantes.Add(nombre)
                ElseIf parametrosResueltos(nombre) Is DBNull.Value Then
                    faltantes.Add($"{nombre} (es DBNull)")
                End If
            Next

            If faltantes.Count > 0 Then
                Throw New InvalidOperationException($"Parametros obligatorios del SP sin valor: " &
                                                    String.Join(", ", faltantes) &
                                                    ". Revise el JSON de t_Parametros en la programacion " &
                                                    "o el valor Default en t_ParametrosConfig de la plantilla")
            End If

        End Sub

        Private Function ConvertirFijo(valor As String, tipo As String) As Object
            If String.IsNullOrWhiteSpace(tipo) Then Return valor

            Select Case tipo.ToUpperInvariant()
                Case "INT", "INTEGER", "BIGINT", "SMALLINT"
                    Dim r As Integer
                    Return If(Integer.TryParse(valor, r), CObj(r), CObj(0))

                Case "DECIMAL", "NUMERIC", "FLOAT", "MONEY"
                    Dim r As Decimal
                    Return If(Decimal.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, r), CObj(r), CObj(0D))

                Case "DATE", "DATETIME", "SMALLDATETIME"
                    Dim r As DateTime
                    Return If(DateTime.TryParse(valor, CultureInfo.InvariantCulture,
                              DateTimeStyles.None, r), CObj(r), CObj(DateTime.Now))

                Case "BIT", "BOOLEAN"
                    Return valor = "1" OrElse valor.Equals("true", StringComparison.OrdinalIgnoreCase)

                Case Else
                    Return valor
            End Select

        End Function

        'funciones auxiliares
        Private Function NormalizarNombre(nombre As String) As String
            If String.IsNullOrWhiteSpace(nombre) Then Return nombre
            Return If(nombre.StartsWith("@"), nombre, "@" & nombre)
        End Function

        Private Function BuscarConfig(jsonConfig As String, nombreParametro As String) As ParametroConfig
            If String.IsNullOrWhiteSpace(jsonConfig) Then Return Nothing
            Try
                Dim configs = JsonConvert.DeserializeObject(Of List(Of ParametroConfig))(jsonConfig)
                If configs Is Nothing Then Return Nothing
                Dim limpio = nombreParametro.TrimStart("@"c).ToUpperInvariant()
                Return configs.FirstOrDefault(Function(c)
                                                  Return c.Nombre.TrimStart("@"c).Equals(
                                                  limpio, StringComparison.OrdinalIgnoreCase)
                                              End Function)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

    End Class

End Namespace

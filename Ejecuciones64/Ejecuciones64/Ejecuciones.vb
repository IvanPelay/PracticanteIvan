Imports System.Diagnostics.Eventing.Reader
Imports System.Drawing.Text
Imports Gsol
Imports Gsol.BaseDatos.Operaciones
Imports Newtonsoft.Json

Public Class frm000ProgramarEjecucion
    Inherits FormularioBase64

#Region "Atributos"

    'clase para obtener parametros de la plantilla
    Private Class ParametrosPlantilla
        Public Property Nombre As String
        Public Property Tipo As String
        Public Property Etiqueta As String
        Public Property Requerido As Boolean
        Public Property ValorDefault As String
        Public Property Orden As Integer
    End Class

    Public Class PlantillaInfo
        Public Property Id As Integer
        Public Property Nombre As String
        Public Property ParametrosConfig As String
    End Class

    'clase para almacenar los valores actuales en el grid
    Private Class ParametroValor
        Public Property Nombre As String
        Public Property Tipo As String
        Public Property Valor As String
    End Class

    'Diccionario para almacenar los parámetros de la plantilla
    Private _parametrosActuales As New List(Of ParametroValor) 'Almacena los valores actuales de los parámetros
    Private _Plantillas As New List(Of PlantillaInfo)
    Private Shared ReadOnly _expresionesDate As String = "INICIO_PERIODO, FIN_PERIODO, INICIO_SEMANA, FIN_SEMANA, " &
        "INICIO_MES, FIN_MES, INICIO_QUINCENA, FIN_QUINCENA, HOY, HOY-N, HOY+N"
#End Region

#Region "Constructores"

    Public Sub New(ByVal ioperacionescatalogo_ As IOperacionesCatalogo,
            ByVal tipooperacion_ As IOperacionesCatalogo.TiposOperacionSQL)

        'Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        _ioperacionescatalogo = New OperacionesCatalogo

        _ioperacionescatalogo = ioperacionescatalogo_

        _modalidadoperativa = tipooperacion_

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        _sistema = New Organismo

        CargarPlantillasDisponibles()     'Cargar plantillas Disponibles
        ConfigurarGrid()        'Configurar columnas del grid de parámetros
        ConfigurarEventos()       'Configurar eventos de controles dinámicos

        Select Case tipooperacion_
            Case IOperacionesCatalogo.TiposOperacionSQL.Insercion
                LblAccion.Text = "Nuevo registro"
                'Valores por defecto
                ckbEstatus.Checked = True
                rbFrecuenciaU.Checked = True
                HoraEjecucion.Value = DateTime.Now.Date.AddHours(1) 'Hora por defecto a 1 hora de la hora actual
                dtpVigenciaInicio.Value = DateTime.Today
                dtpVigenciaFin.Value = DateTime.Today.AddYears(1)
                ActualizarEstadoVigencia()


            Case IOperacionesCatalogo.TiposOperacionSQL.Modificar

                LblAccion.Text = "Edición"

                PreparaModificacion()

            Case Else

        End Select

    End Sub

#End Region

#Region "Métodos"

    'configurar el grid de parámetros
    Private Sub ConfigurarGrid()
        dgvParametros.Columns.Clear()
        dgvParametros.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "Nombre", .HeaderText = "Parámetro",
            .ReadOnly = True, .Width = 110})

        dgvParametros.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "Valor", .HeaderText = "Valor",
            .Width = 130})

        dgvParametros.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "Tipo", .HeaderText = "Tipo",
            .ReadOnly = True, .Width = 55})

        ' Columna de ayuda: muestra expresiones disponibles para tipo Date
        Dim colInfo As New DataGridViewTextBoxColumn With {
            .Name = "ExpresionesInfo",
            .HeaderText = "Expresiones válidas",
            .ReadOnly = True,
            .Width = 160}
        colInfo.DefaultCellStyle.ForeColor = System.Drawing.Color.DimGray
        colInfo.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!)
        dgvParametros.Columns.Add(colInfo)

        dgvParametros.AllowUserToAddRows = False
        dgvParametros.AllowUserToDeleteRows = False
        dgvParametros.RowHeadersVisible = False
    End Sub


    Private Sub ConfigurarEventos()
        AddHandler cbPlantillas.SelectedIndexChanged, AddressOf CbPlantillas_SelectedIndexChanged
        AddHandler rbFrecuenciaU.CheckedChanged, AddressOf Frecuencia_CheckedChanged
        AddHandler rbFrecuenciaD.CheckedChanged, AddressOf Frecuencia_CheckedChanged
        AddHandler rbFrecuenciaS.CheckedChanged, AddressOf Frecuencia_CheckedChanged
        AddHandler rbFrecuenciaM.CheckedChanged, AddressOf Frecuencia_CheckedChanged
        AddHandler rbFrecuenciaQ.CheckedChanged, AddressOf Frecuencia_CheckedChanged
        AddHandler Me.FormClosing, AddressOf Frm000ProgramarEjecucion_FormClosing
    End Sub


    'evento cuando se cambia la plantilla seleccionada
    Private Sub CbPlantillas_SelectedIndexChanged(sender As Object, e As EventArgs)
        If cbPlantillas.SelectedItem IsNot Nothing AndAlso
           TypeOf cbPlantillas.SelectedItem Is PlantillaInfo Then
            Dim info = CType(cbPlantillas.SelectedItem, PlantillaInfo)
            CargarParametrosEnGrid(info.ParametrosConfig)
        End If
    End Sub

    Private Sub CargarParametrosEnGrid(jsonConfig As String)
        dgvParametros.Rows.Clear()
        _parametrosActuales.Clear()

        If String.IsNullOrEmpty(jsonConfig) Then
            'Mostrar mensaje de que no hay parametros
            dgvParametros.Rows.Add("Sin Parametros", "", "", "")
            dgvParametros.ReadOnly = True
            Return
        End If

        Try
            Dim parametros = JsonConvert.DeserializeObject(Of List(Of ParametrosPlantilla))(jsonConfig)

            If parametros IsNot Nothing AndAlso parametros.Count > 0 Then
                dgvParametros.ReadOnly = False

                For Each param In parametros.OrderBy(Function(p) p.Orden)
                    ' Columna de ayuda: solo para tipo Date
                    Dim ayuda As String = If(param.Tipo.Equals("Date", StringComparison.OrdinalIgnoreCase),
                                             _expresionesDate, "")
                    dgvParametros.Rows.Add(param.Nombre, param.ValorDefault, param.Tipo, ayuda)

                    _parametrosActuales.Add(New ParametroValor With {
                        .Nombre = param.Nombre,
                        .Valor = param.ValorDefault,
                        .Tipo = param.Tipo
                    })
                Next
            Else
                dgvParametros.Rows.Add("Sin parámetros configurados", "", "", "")
                dgvParametros.ReadOnly = True
            End If
        Catch ex As Exception
            MessageBox.Show("Error al procesar los parametros de la plantilla: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            dgvParametros.Rows.Add("Error al cargarl los parametros", "", "", "")
            dgvParametros.ReadOnly = True
        End Try
    End Sub

    Private Sub CargarPlantillasDisponibles()
        Try
            _Plantillas.Clear()
            'usar framework para buscar las plantilllas disponibles
            Dim operacionPlantillas As New OperacionesCatalogo()

            'configurar operación para consultar las plantillas disponibles
            'Dudas aqui como puedo consultar informacion de otro modulo y manejar espacio de trabajo
            operacionPlantillas.IdentificadorCatalogo = "i_Cve_Plantilla"
            operacionPlantillas.OperadorCatalogoConsulta = "Vt016Plantillas"
            'filtramos plantillas activas
            operacionPlantillas.ClausulasLibres = "i_Cve_Estado = 1 AND i_Cve_Estatus = 1"

            'verificar si hay registros
            If operacionPlantillas.CantidadVisibleRegistros > 0 Then

                'Recorrer resultados
                For i As Integer = 0 To operacionPlantillas.CantidadVisibleRegistros - 1
                    operacionPlantillas.IndicePaginacion = i

                    Dim plantilla As New PlantillaInfo With {
                        .Id = CInt(operacionPlantillas.CampoPorNombre("i_Cve_Plantilla")),
                        .Nombre = operacionPlantillas.CampoPorNombre("t_Nombre").ToString(),
                        .ParametrosConfig = If(operacionPlantillas.CampoPorNombre("t_ParametrosConfig") Is Nothing,
                                             "",
                                             operacionPlantillas.CampoPorNombre("t_ParametrosConfig").ToString())
                    }
                    _Plantillas.Add(plantilla)
                Next


                'configurar combo con las plantillas disponibles
                cbPlantillas.DisplayMember = "Nombre"
                cbPlantillas.ValueMember = "Id"
                cbPlantillas.DataSource = _Plantillas.ToList()

                'Seleccionar la primera plantilla por defecto
                If cbPlantillas.Items.Count > 0 Then
                    cbPlantillas.SelectedIndex = 0
                End If

            Else
                cbPlantillas.DataSource = Nothing
                cbPlantillas.Items.Clear()
                cbPlantillas.Items.Add("No hay plantillas disponibles")
                cbPlantillas.SelectedIndex = 0
                dgvParametros.Rows.Clear()
                dgvParametros.Rows.Add("No hay plantillas disponibles", "", "")
            End If

        Catch ex As Exception
            MessageBox.Show("Error al cargar plantillas disponibles: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cbPlantillas.DataSource = Nothing
            cbPlantillas.Items.Clear()
            cbPlantillas.Items.Add("No hay plantillas disponibles")
        End Try
    End Sub





    Public Overrides Sub PreparaModificacion()

        Try
            tbNombreProgramacion.Text = _ioperacionescatalogo.CampoPorNombre("t_Nombre").ToString()
            ckbEstatus.Checked = _ioperacionescatalogo.CampoPorNombre("i_Cve_Estatus").ToString() = "1"

            Dim frecuencia As String = _ioperacionescatalogo.CampoPorNombre("t_Frecuencia").ToString()
            rbFrecuenciaU.Checked = (frecuencia = "U")
            rbFrecuenciaD.Checked = (frecuencia = "D")
            rbFrecuenciaS.Checked = (frecuencia = "S")
            rbFrecuenciaM.Checked = (frecuencia = "M")
            rbFrecuenciaQ.Checked = (frecuencia = "Q")

            'Cargar hora de ejecución
            Dim horaStr = _ioperacionescatalogo.CampoPorNombre("t_Hora").ToString()

            If Not String.IsNullOrEmpty(horaStr) Then
                Dim hora As DateTime
                If DateTime.TryParseExact(horaStr, "HH:mm:ss", Nothing, Globalization.DateTimeStyles.None, hora) Then
                    HoraEjecucion.Value = DateTime.Today.Add(hora.TimeOfDay)
                End If
            End If

            ' Vigencia inicio
            Dim vigIniStr = _ioperacionescatalogo.CampoPorNombre("f_VigenciaInicio")?.ToString()
            If Not String.IsNullOrEmpty(vigIniStr) Then
                Dim vigIni As DateTime
                If DateTime.TryParse(vigIniStr, vigIni) Then
                    dtpVigenciaInicio.Value = vigIni
                End If
            End If

            'vigencia fin
            Dim vigFinStr = _ioperacionescatalogo.CampoPorNombre("f_VigenciaFin")?.ToString()
            If Not String.IsNullOrEmpty(vigFinStr) Then
                Dim vigFin As DateTime
                If DateTime.TryParse(vigFinStr, vigFin) Then
                    dtpVigenciaFin.Value = vigFin
                End If
            End If

            'Plantilla seleccionada
            Dim idPlantilla As Integer
            If Integer.TryParse(_ioperacionescatalogo.CampoPorNombre("i_Cve_Plantilla").ToString(), idPlantilla) Then
                SeleccionarPlantillaPorId(idPlantilla)
            End If

            'valores Parametros guardados
            Dim parametrosJson = _ioperacionescatalogo.CampoPorNombre("t_Parametros")?.ToString()
            If Not String.IsNullOrEmpty(parametrosJson) Then
                CargarValoresParametrosGuardados(parametrosJson)
            End If

            ActualizarEstadoVigencia()

        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos para la modificación: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    'Selecciona una plantilla por su Id
    Private Sub SeleccionarPlantillaPorId(id As Integer)
        For i As Integer = 0 To cbPlantillas.Items.Count - 1
            If TypeOf cbPlantillas.Items(i) Is PlantillaInfo Then
                If CType(cbPlantillas.Items(i), PlantillaInfo).Id = id Then
                    cbPlantillas.SelectedIndex = i
                    Return
                End If
            End If
        Next
    End Sub

    'carga los valores de los parámetros guardados en el grid
    Private Sub CargarValoresParametrosGuardados(jsonValores As String)
        If String.IsNullOrEmpty(jsonValores) Then Return
        Try
            Dim valores = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(jsonValores)
            If valores Is Nothing Then Return
            'Recorrer las filas del grid y asignar valores
            For Each row As DataGridViewRow In dgvParametros.Rows
                If Not row.IsNewRow Then
                    Dim nombreParam = row.Cells("Nombre").Value?.ToString()
                    If nombreParam IsNot Nothing AndAlso valores.ContainsKey(nombreParam) Then
                        row.Cells("Valor").Value = valores(nombreParam)
                    End If
                End If
            Next

        Catch ex As Exception
            Debug.WriteLine($"Error cargando los valores de los parámetros: {ex.Message}")
        End Try
    End Sub

    'control de vigencia según frecuencia
    Private Sub Frecuencia_CheckedChanged(sender As Object, e As EventArgs)
        ActualizarEstadoVigencia()
    End Sub

    ' Habilita o deshabilita dtpVigenciaFin según la frecuencia seleccionada.
    Private Sub ActualizarEstadoVigencia()
        Dim esUnica = rbFrecuenciaU.Checked

        dtpVigenciaFin.Enabled = True
        lblVigenciaFinOpc.Text = If(esUnica, "(opcional para unica vez)", "(Obligatoria para D/S/M/Q)")
        lblVigenciaFinOpc.ForeColor = If(esUnica, System.Drawing.Color.Gray, System.Drawing.Color.DarkRed)
    End Sub


    Public Overrides Sub RealizarInsercion()

        LlenarCatalogoConUI()

    End Sub

    Public Overrides Sub RealizarModificacion()

        LlenarCatalogoConUI()

    End Sub

    Private Sub LlenarCatalogoConUI()

        'validar que tenemos una plantilla seleccionada
        If cbPlantillas.SelectedItem Is Nothing OrElse Not TypeOf cbPlantillas.SelectedItem Is PlantillaInfo Then
            MessageBox.Show("Debe seleccionar una plantilla válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Llenar datos básicos
        _ioperacionescatalogo.CampoPorNombre("i_Cve_Plantilla") = cbPlantillas.SelectedValue
        _ioperacionescatalogo.CampoPorNombre("t_Nombre") = tbNombreProgramacion.Text
        _ioperacionescatalogo.CampoPorNombre("i_Cve_Estatus") = If(ckbEstatus.Checked, "1", "0")

        ' Lógica de Frecuencia (resumida)
        Dim freq As String = "U"
        If rbFrecuenciaD.Checked Then freq = "D"
        If rbFrecuenciaS.Checked Then freq = "S"
        If rbFrecuenciaM.Checked Then freq = "M"
        If rbFrecuenciaQ.Checked Then freq = "Q"
        _ioperacionescatalogo.CampoPorNombre("t_Frecuencia") = freq
        _ioperacionescatalogo.CampoPorNombre("t_Hora") = HoraEjecucion.Value.ToString("HH:mm:ss")

        'Días de la semana para frecuencia semanal
        If rbFrecuenciaS.Checked Then
            Dim dias As New List(Of String)
            If checkbL.Checked Then dias.Add("1")
            If checkbMartes.Checked Then dias.Add("2")
            If checkbMie.Checked Then dias.Add("3")
            If checkbJ.Checked Then dias.Add("4")
            If checkbV.Checked Then dias.Add("5")
            If checkbS.Checked Then dias.Add("6")
            If checkbD.Checked Then dias.Add("7")
            _ioperacionescatalogo.CampoPorNombre("t_DiasSemana") = String.Join(",", dias)
        End If

        'Dia del mes para la frecuencia mensual
        If rbFrecuenciaM.Checked Then
            _ioperacionescatalogo.CampoPorNombre("i_DiaMes") = numericUDMes.Value.ToString()
        End If

        'vigencia
        _ioperacionescatalogo.CampoPorNombre("f_VigenciaInicio") = dtpVigenciaInicio.Value.ToString("yyyy-mm-dd")

        'Para U sin fecha fin lo guardamos null en bd
        If rbFrecuenciaU.Checked Then
            _ioperacionescatalogo.CampoPorNombre("f_VigenciaFin") = dtpVigenciaFin.Value.ToString("yyyy-mm-dd")
        Else
            _ioperacionescatalogo.CampoPorNombre("f_VigenciaFin") = dtpVigenciaFin.Value.ToString("yyyy-MM-dd")
        End If

        ' GENERACIÓN DEL JSON DE VALORES
        Dim valoresFinales As New Dictionary(Of String, String)

        For Each row As DataGridViewRow In dgvParametros.Rows
            If Not row.IsNewRow Then
                Dim nombreParam As String = row.Cells("Nombre").Value?.ToString()
                Dim valorIngresado As String = If(row.Cells("Valor").Value Is Nothing, "", row.Cells("Valor").Value.ToString())

                If Not String.IsNullOrEmpty(nombreParam) Then
                    valoresFinales.Add(nombreParam, valorIngresado)
                End If
            End If
        Next

        ' Guardamos el JSON resultante
        _ioperacionescatalogo.CampoPorNombre("t_Parametros") =
            If(valoresFinales.Any(), JsonConvert.SerializeObject(valoresFinales), String.Empty)


        ' Campos de auditoría obligatorios por framework
        If _modalidadoperativa = IOperacionesCatalogo.TiposOperacionSQL.Insercion Then
            _ioperacionescatalogo.CampoPorNombre("f_FechaRegistro") = DateTime.Now
            _ioperacionescatalogo.CampoPorNombre("i_Cve_Estado") = "1"
            _ioperacionescatalogo.CampoPorNombre("f_ProximaEjecucion") = CalcularProximaEjecucion()
        End If
    End Sub

    'calcular la proxima ejecucion
    Private Function CalcularProximaEjecucion() As String
        Dim hora = HoraEjecucion.Value.TimeOfDay
        Dim proximaFecha = DateTime.Today.Add(hora)

        If proximaFecha <= DateTime.Now Then
            proximaFecha = proximaFecha.AddDays(1)
        End If

        ' Respetar f_VigenciaInicio: si la primera ejecución sería antes de la vigencia,
        ' adelantarla a la fecha de vigencia con la hora configurada
        Dim vigIni = dtpVigenciaInicio.Value.Date.Add(hora)
        If proximaFecha < vigIni Then proximaFecha = vigIni

        If rbFrecuenciaU.Checked Then
            ' Única: la próxima ejecución es exactamente la fecha/hora configurada
            ' (ya resuelta arriba considerando vigencia)

        ElseIf rbFrecuenciaD.Checked Then
            ' Diaria: ya resuelta arriba

        ElseIf rbFrecuenciaS.Checked Then
            ' SEMANAL: Buscar el siguiente día marcado en los checkboxes
            Dim diasSeleccionados As New List(Of DayOfWeek)
            If checkbL.Checked Then diasSeleccionados.Add(DayOfWeek.Monday)
            If checkbMartes.Checked Then diasSeleccionados.Add(DayOfWeek.Tuesday)
            If checkbMie.Checked Then diasSeleccionados.Add(DayOfWeek.Wednesday)
            If checkbJ.Checked Then diasSeleccionados.Add(DayOfWeek.Thursday)
            If checkbV.Checked Then diasSeleccionados.Add(DayOfWeek.Friday)
            If checkbS.Checked Then diasSeleccionados.Add(DayOfWeek.Saturday)
            If checkbD.Checked Then diasSeleccionados.Add(DayOfWeek.Sunday)

            ' Buscamos en los próximos 7 días cuál coincide
            Dim encontrado As Boolean = False
            For i As Integer = 0 To 7
                If diasSeleccionados.Contains(proximaFecha.DayOfWeek) Then
                    encontrado = True
                    Exit For
                End If
                proximaFecha = proximaFecha.AddDays(1)
            Next

        ElseIf rbFrecuenciaM.Checked Then
            ' MENSUAL: Ir al día específico del mes
            Dim diaDeseado = CInt(numericUDMes.Value)
            ' Ajustamos al último día del mes si el usuario puso 31 y el mes tiene 30
            Dim ultimoDiaMes = DateTime.DaysInMonth(proximaFecha.Year, proximaFecha.Month)
            Dim diaReal = Math.Min(diaDeseado, ultimoDiaMes)

            proximaFecha = New DateTime(proximaFecha.Year, proximaFecha.Month, diaReal).Add(hora)

            ' Si ese día ya pasó este mes, saltamos al siguiente mes
            If proximaFecha <= DateTime.Now Then
                proximaFecha = proximaFecha.AddMonths(1)
                ' Volvemos a validar el último día del nuevo mes
                ultimoDiaMes = DateTime.DaysInMonth(proximaFecha.Year, proximaFecha.Month)
                proximaFecha = New DateTime(proximaFecha.Year, proximaFecha.Month, Math.Min(diaDeseado, ultimoDiaMes)).Add(hora)
            End If

        ElseIf rbFrecuenciaQ.Checked Then
            ' QUINCENAL: Sumamos 15 días
            proximaFecha = proximaFecha.AddDays(15)
        End If

        Return proximaFecha.ToString("yyyy-MM-dd HH:mm:ss")
    End Function

    'validacion al cerrar el formulario
    Private Sub Frm000ProgramarEjecucion_FormClosing(sender As Object, e As FormClosingEventArgs)
        If Me.DialogResult <> DialogResult.OK Then
            'Validar nombre
            If String.IsNullOrEmpty(tbNombreProgramacion.Text) Then
                MessageBox.Show("El nombre de la programación es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True
                Return
            End If


            If cbPlantillas.SelectedItem Is Nothing OrElse Not TypeOf cbPlantillas.SelectedItem Is PlantillaInfo Then
                MessageBox.Show("Debe seleccionar una plantilla válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True
                Return
            End If
            ' Si todo está bien, se procederá a cerrar el formulario y realizar la inserción/modificación según corresponda.
        End If

        'validar parametros requeridos
        For Each row As DataGridViewRow In dgvParametros.Rows
            If Not row.IsNewRow AndAlso Not dgvParametros.ReadOnly Then
                Dim nombreParam = row.Cells("Nombre").Value?.ToString()
                Dim valorIngresado = row.Cells("Valor").Value?.ToString()
                Dim tipo = row.Cells("Tipo").Value?.ToString()

                If Not String.IsNullOrEmpty(nombreParam) AndAlso String.IsNullOrEmpty(valorIngresado) Then
                    Dim res = MessageBox.Show(
                        $"El parámetro '{Nombre}' no tiene valor. ¿Continuar sin asignarlo?",
                        "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If res = DialogResult.No Then
                        e.Cancel = True : Return
                    End If
                End If
            End If
        Next

        'Validar días de semana para frecuencia semanal
        If rbFrecuenciaS.Checked AndAlso Not (checkbL.Checked Or checkbMartes.Checked Or
               checkbMie.Checked Or checkbJ.Checked Or checkbV.Checked Or
               checkbS.Checked Or checkbD.Checked) Then
            MessageBox.Show("Debe seleccionar al menos un día para la frecuencia semanal.",
                              "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
            Return
        End If

        'vigencia f_Vigencia obligatoria para D/S/M/Q
        If Not rbFrecuenciaU.Checked Then
            If dtpVigenciaFin.Value.Date <= dtpVigenciaInicio.Value.Date Then
                MessageBox.Show("La fecha fin de la vigencia debe ser posterior a la fecha inicio",
                                "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True : Return
            End If
        End If

        'vigencia no anterior a hoy
        If _modalidadoperativa = IOperacionesCatalogo.TiposOperacionSQL.Insercion Then
            If dtpVigenciaInicio.Value.Date < DateTime.Today Then
                Dim res = MessageBox.Show("La fecha inicio de vigencia es anterior a hoy. ¿Desea continuar?",
                    "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If res = DialogResult.No Then
                    e.Cancel = True : Return
                End If
            End If
            End If

    End Sub

#End Region


End Class

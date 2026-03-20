<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm000PlantillasConfColParam
    Inherits FormularioBase64

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ControlPanel = New System.Windows.Forms.Panel()
        Me.lblVersionModulo = New System.Windows.Forms.Label()
        Me.groupBox3 = New System.Windows.Forms.GroupBox()
        Me.TCColumasnParametros = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.bModificarColumna = New System.Windows.Forms.Button()
        Me.bAgregarColumna = New System.Windows.Forms.Button()
        Me.ColumnasCargadas = New System.Windows.Forms.ListBox()
        Me.cbFormato = New System.Windows.Forms.ComboBox()
        Me.tbOrden = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbEtiqueta = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbCampo = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.ParametrosCargados = New System.Windows.Forms.ListBox()
        Me.bModificarParametro = New System.Windows.Forms.Button()
        Me.bAgregarParametro = New System.Windows.Forms.Button()
        Me.tbOrdenParametro = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TbValorDefault = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.checkbRequerido = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.tbEtiquetaParametro = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cbTipo = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.tbNombre = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.rtbConsultaSQL = New System.Windows.Forms.RichTextBox()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.chkEstatus = New System.Windows.Forms.CheckBox()
        Me.tbCliente = New System.Windows.Forms.TextBox()
        Me.RtbDescripcion = New System.Windows.Forms.RichTextBox()
        Me.lbDescripción = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.bSeleccionarPlantilla = New System.Windows.Forms.Button()
        Me.tbRutaPlantillaxlsx = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.tbNombrePlantilla = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ControlPanel.SuspendLayout()
        Me.groupBox3.SuspendLayout()
        Me.TCColumasnParametros.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LblMensaje
        '
        Me.LblMensaje.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LblMensaje.Location = New System.Drawing.Point(23, 556)
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(23, 5)
        Me.Label6.Size = New System.Drawing.Size(202, 31)
        Me.Label6.Text = "{Crear Plantilla}"
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.Color.DarkCyan
        Me.btnCancelar.FlatAppearance.BorderSize = 0
        Me.btnCancelar.Location = New System.Drawing.Point(307, 27)
        '
        'btnAceptar
        '
        Me.btnAceptar.BackColor = System.Drawing.Color.DarkCyan
        Me.btnAceptar.FlatAppearance.BorderSize = 0
        Me.btnAceptar.Location = New System.Drawing.Point(106, 26)
        '
        'LblAccion
        '
        Me.LblAccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAccion.Location = New System.Drawing.Point(26, 35)
        Me.LblAccion.Size = New System.Drawing.Size(149, 25)
        '
        'ControlPanel
        '
        Me.ControlPanel.BackColor = System.Drawing.Color.DarkCyan
        Me.ControlPanel.Controls.Add(Me.lblVersionModulo)
        Me.ControlPanel.Controls.Add(Me.btnAceptar)
        Me.ControlPanel.Controls.Add(Me.btnCancelar)
        Me.ControlPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ControlPanel.Location = New System.Drawing.Point(0, 575)
        Me.ControlPanel.Name = "ControlPanel"
        Me.ControlPanel.Size = New System.Drawing.Size(708, 78)
        Me.ControlPanel.TabIndex = 359
        Me.ControlPanel.Controls.SetChildIndex(Me.btnCancelar, 0)
        Me.ControlPanel.Controls.SetChildIndex(Me.btnAceptar, 0)
        Me.ControlPanel.Controls.SetChildIndex(Me.lblVersionModulo, 0)
        '
        'lblVersionModulo
        '
        Me.lblVersionModulo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVersionModulo.AutoSize = True
        Me.lblVersionModulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersionModulo.ForeColor = System.Drawing.Color.White
        Me.lblVersionModulo.Location = New System.Drawing.Point(645, 61)
        Me.lblVersionModulo.Name = "lblVersionModulo"
        Me.lblVersionModulo.Size = New System.Drawing.Size(58, 13)
        Me.lblVersionModulo.TabIndex = 357
        Me.lblVersionModulo.Text = "v.0.0.0.0"
        '
        'groupBox3
        '
        Me.groupBox3.Controls.Add(Me.TCColumasnParametros)
        Me.groupBox3.Location = New System.Drawing.Point(26, 271)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(649, 281)
        Me.groupBox3.TabIndex = 363
        Me.groupBox3.TabStop = False
        Me.groupBox3.Text = "Parametros: "
        '
        'TCColumasnParametros
        '
        Me.TCColumasnParametros.Controls.Add(Me.TabPage1)
        Me.TCColumasnParametros.Controls.Add(Me.TabPage2)
        Me.TCColumasnParametros.Location = New System.Drawing.Point(6, 19)
        Me.TCColumasnParametros.Name = "TCColumasnParametros"
        Me.TCColumasnParametros.SelectedIndex = 0
        Me.TCColumasnParametros.Size = New System.Drawing.Size(637, 256)
        Me.TCColumasnParametros.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(629, 230)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Columnas"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.bModificarColumna)
        Me.GroupBox4.Controls.Add(Me.bAgregarColumna)
        Me.GroupBox4.Controls.Add(Me.ColumnasCargadas)
        Me.GroupBox4.Controls.Add(Me.cbFormato)
        Me.GroupBox4.Controls.Add(Me.tbOrden)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.tbEtiqueta)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.tbCampo)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(617, 218)
        Me.GroupBox4.TabIndex = 364
        Me.GroupBox4.TabStop = False
        '
        'bModificarColumna
        '
        Me.bModificarColumna.Location = New System.Drawing.Point(507, 45)
        Me.bModificarColumna.Name = "bModificarColumna"
        Me.bModificarColumna.Size = New System.Drawing.Size(75, 23)
        Me.bModificarColumna.TabIndex = 15
        Me.bModificarColumna.Text = "Modificar"
        Me.bModificarColumna.UseVisualStyleBackColor = True
        '
        'bAgregarColumna
        '
        Me.bAgregarColumna.Location = New System.Drawing.Point(508, 19)
        Me.bAgregarColumna.Name = "bAgregarColumna"
        Me.bAgregarColumna.Size = New System.Drawing.Size(75, 23)
        Me.bAgregarColumna.TabIndex = 14
        Me.bAgregarColumna.Text = "Agregar"
        Me.bAgregarColumna.UseVisualStyleBackColor = True
        '
        'ColumnasCargadas
        '
        Me.ColumnasCargadas.FormattingEnabled = True
        Me.ColumnasCargadas.Location = New System.Drawing.Point(6, 78)
        Me.ColumnasCargadas.Name = "ColumnasCargadas"
        Me.ColumnasCargadas.Size = New System.Drawing.Size(591, 134)
        Me.ColumnasCargadas.TabIndex = 13
        '
        'cbFormato
        '
        Me.cbFormato.FormattingEnabled = True
        Me.cbFormato.Items.AddRange(New Object() {"Moneda", "Número", "Número Entero", "Porcentaje", "Fecha", "Texto", "Sin formato"})
        Me.cbFormato.Location = New System.Drawing.Point(245, 13)
        Me.cbFormato.Name = "cbFormato"
        Me.cbFormato.Size = New System.Drawing.Size(121, 21)
        Me.cbFormato.TabIndex = 12
        '
        'tbOrden
        '
        Me.tbOrden.Location = New System.Drawing.Point(245, 36)
        Me.tbOrden.Name = "tbOrden"
        Me.tbOrden.Size = New System.Drawing.Size(121, 20)
        Me.tbOrden.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(197, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Orden: "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(197, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Formato: "
        '
        'tbEtiqueta
        '
        Me.tbEtiqueta.Location = New System.Drawing.Point(54, 37)
        Me.tbEtiqueta.Name = "tbEtiqueta"
        Me.tbEtiqueta.Size = New System.Drawing.Size(121, 20)
        Me.tbEtiqueta.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Etiqueta: "
        '
        'tbCampo
        '
        Me.tbCampo.Location = New System.Drawing.Point(54, 13)
        Me.tbCampo.Name = "tbCampo"
        Me.tbCampo.Size = New System.Drawing.Size(121, 20)
        Me.tbCampo.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Campo: "
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox5)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(629, 230)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Parametros"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.ParametrosCargados)
        Me.GroupBox5.Controls.Add(Me.bModificarParametro)
        Me.GroupBox5.Controls.Add(Me.bAgregarParametro)
        Me.GroupBox5.Controls.Add(Me.tbOrdenParametro)
        Me.GroupBox5.Controls.Add(Me.Label9)
        Me.GroupBox5.Controls.Add(Me.TbValorDefault)
        Me.GroupBox5.Controls.Add(Me.Label10)
        Me.GroupBox5.Controls.Add(Me.checkbRequerido)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Controls.Add(Me.tbEtiquetaParametro)
        Me.GroupBox5.Controls.Add(Me.Label12)
        Me.GroupBox5.Controls.Add(Me.cbTipo)
        Me.GroupBox5.Controls.Add(Me.Label13)
        Me.GroupBox5.Controls.Add(Me.tbNombre)
        Me.GroupBox5.Controls.Add(Me.Label14)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(617, 218)
        Me.GroupBox5.TabIndex = 364
        Me.GroupBox5.TabStop = False
        '
        'ParametrosCargados
        '
        Me.ParametrosCargados.FormattingEnabled = True
        Me.ParametrosCargados.Location = New System.Drawing.Point(6, 85)
        Me.ParametrosCargados.Name = "ParametrosCargados"
        Me.ParametrosCargados.Size = New System.Drawing.Size(587, 121)
        Me.ParametrosCargados.TabIndex = 364
        '
        'bModificarParametro
        '
        Me.bModificarParametro.Location = New System.Drawing.Point(510, 48)
        Me.bModificarParametro.Name = "bModificarParametro"
        Me.bModificarParametro.Size = New System.Drawing.Size(75, 23)
        Me.bModificarParametro.TabIndex = 17
        Me.bModificarParametro.Text = "Modificar"
        Me.bModificarParametro.UseVisualStyleBackColor = True
        '
        'bAgregarParametro
        '
        Me.bAgregarParametro.Location = New System.Drawing.Point(511, 19)
        Me.bAgregarParametro.Name = "bAgregarParametro"
        Me.bAgregarParametro.Size = New System.Drawing.Size(75, 23)
        Me.bAgregarParametro.TabIndex = 16
        Me.bAgregarParametro.Text = "Agregar"
        Me.bAgregarParametro.UseVisualStyleBackColor = True
        '
        'tbOrdenParametro
        '
        Me.tbOrdenParametro.Location = New System.Drawing.Point(86, 55)
        Me.tbOrdenParametro.Name = "tbOrdenParametro"
        Me.tbOrdenParametro.Size = New System.Drawing.Size(100, 20)
        Me.tbOrdenParametro.TabIndex = 11
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 58)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(42, 13)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Orden: "
        '
        'TbValorDefault
        '
        Me.TbValorDefault.Location = New System.Drawing.Point(275, 59)
        Me.TbValorDefault.Name = "TbValorDefault"
        Me.TbValorDefault.Size = New System.Drawing.Size(100, 20)
        Me.TbValorDefault.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(199, 62)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(74, 13)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Valor Default: "
        '
        'checkbRequerido
        '
        Me.checkbRequerido.AutoSize = True
        Me.checkbRequerido.Location = New System.Drawing.Point(265, 42)
        Me.checkbRequerido.Name = "checkbRequerido"
        Me.checkbRequerido.Size = New System.Drawing.Size(15, 14)
        Me.checkbRequerido.TabIndex = 7
        Me.checkbRequerido.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(199, 42)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(62, 13)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "Requerido: "
        '
        'tbEtiquetaParametro
        '
        Me.tbEtiquetaParametro.Location = New System.Drawing.Point(86, 35)
        Me.tbEtiquetaParametro.Name = "tbEtiquetaParametro"
        Me.tbEtiquetaParametro.Size = New System.Drawing.Size(100, 20)
        Me.tbEtiquetaParametro.TabIndex = 5
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 38)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(52, 13)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "Etiqueta: "
        '
        'cbTipo
        '
        Me.cbTipo.FormattingEnabled = True
        Me.cbTipo.Items.AddRange(New Object() {"String", "Int", "Date", "Decimal"})
        Me.cbTipo.Location = New System.Drawing.Point(240, 13)
        Me.cbTipo.Name = "cbTipo"
        Me.cbTipo.Size = New System.Drawing.Size(100, 21)
        Me.cbTipo.TabIndex = 3
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(199, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(34, 13)
        Me.Label13.TabIndex = 2
        Me.Label13.Text = "Tipo: "
        '
        'tbNombre
        '
        Me.tbNombre.Location = New System.Drawing.Point(86, 13)
        Me.tbNombre.Name = "tbNombre"
        Me.tbNombre.Size = New System.Drawing.Size(100, 20)
        Me.tbNombre.TabIndex = 1
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(50, 13)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "Nombre: "
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.rtbConsultaSQL)
        Me.groupBox2.Location = New System.Drawing.Point(26, 170)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(649, 105)
        Me.groupBox2.TabIndex = 362
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "Consulta SQL: "
        '
        'rtbConsultaSQL
        '
        Me.rtbConsultaSQL.Location = New System.Drawing.Point(10, 20)
        Me.rtbConsultaSQL.Name = "rtbConsultaSQL"
        Me.rtbConsultaSQL.Size = New System.Drawing.Size(633, 75)
        Me.rtbConsultaSQL.TabIndex = 0
        Me.rtbConsultaSQL.Text = "SELECT * FROM Ventas WHERE Fecha BETWEEN @Inicio AND @Fin"
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.Label15)
        Me.groupBox1.Controls.Add(Me.chkEstatus)
        Me.groupBox1.Controls.Add(Me.tbCliente)
        Me.groupBox1.Controls.Add(Me.RtbDescripcion)
        Me.groupBox1.Controls.Add(Me.lbDescripción)
        Me.groupBox1.Controls.Add(Me.label3)
        Me.groupBox1.Controls.Add(Me.bSeleccionarPlantilla)
        Me.groupBox1.Controls.Add(Me.tbRutaPlantillaxlsx)
        Me.groupBox1.Controls.Add(Me.label2)
        Me.groupBox1.Controls.Add(Me.tbNombrePlantilla)
        Me.groupBox1.Controls.Add(Me.Label7)
        Me.groupBox1.Location = New System.Drawing.Point(26, 63)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(649, 101)
        Me.groupBox1.TabIndex = 360
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Datos Generales:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(308, 42)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(48, 13)
        Me.Label15.TabIndex = 19
        Me.Label15.Text = "Estatus: "
        '
        'chkEstatus
        '
        Me.chkEstatus.AutoSize = True
        Me.chkEstatus.Checked = True
        Me.chkEstatus.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEstatus.Location = New System.Drawing.Point(362, 41)
        Me.chkEstatus.Name = "chkEstatus"
        Me.chkEstatus.Size = New System.Drawing.Size(15, 14)
        Me.chkEstatus.TabIndex = 18
        Me.chkEstatus.UseVisualStyleBackColor = True
        '
        'tbCliente
        '
        Me.tbCliente.Location = New System.Drawing.Point(80, 35)
        Me.tbCliente.Name = "tbCliente"
        Me.tbCliente.Size = New System.Drawing.Size(169, 20)
        Me.tbCliente.TabIndex = 9
        Me.tbCliente.Text = "WME990813BAA"
        '
        'RtbDescripcion
        '
        Me.RtbDescripcion.Location = New System.Drawing.Point(80, 56)
        Me.RtbDescripcion.Name = "RtbDescripcion"
        Me.RtbDescripcion.Size = New System.Drawing.Size(330, 26)
        Me.RtbDescripcion.TabIndex = 8
        Me.RtbDescripcion.Text = ""
        '
        'lbDescripción
        '
        Me.lbDescripción.AutoSize = True
        Me.lbDescripción.Location = New System.Drawing.Point(7, 59)
        Me.lbDescripción.Name = "lbDescripción"
        Me.lbDescripción.Size = New System.Drawing.Size(69, 13)
        Me.lbDescripción.TabIndex = 7
        Me.lbDescripción.Text = "Descripción: "
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(7, 38)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(71, 13)
        Me.label3.TabIndex = 5
        Me.label3.Text = "Cliente/RFC: "
        '
        'bSeleccionarPlantilla
        '
        Me.bSeleccionarPlantilla.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bSeleccionarPlantilla.Location = New System.Drawing.Point(539, 15)
        Me.bSeleccionarPlantilla.Name = "bSeleccionarPlantilla"
        Me.bSeleccionarPlantilla.Size = New System.Drawing.Size(48, 23)
        Me.bSeleccionarPlantilla.TabIndex = 4
        Me.bSeleccionarPlantilla.Text = "..."
        Me.bSeleccionarPlantilla.UseVisualStyleBackColor = True
        '
        'tbRutaPlantillaxlsx
        '
        Me.tbRutaPlantillaxlsx.Location = New System.Drawing.Point(350, 17)
        Me.tbRutaPlantillaxlsx.Name = "tbRutaPlantillaxlsx"
        Me.tbRutaPlantillaxlsx.Size = New System.Drawing.Size(183, 20)
        Me.tbRutaPlantillaxlsx.TabIndex = 3
        Me.tbRutaPlantillaxlsx.Text = "C:\Reportes\Templates\Ventas.xlsx"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(308, 20)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(36, 13)
        Me.label2.TabIndex = 2
        Me.label2.Text = "Ruta: "
        '
        'tbNombrePlantilla
        '
        Me.tbNombrePlantilla.Location = New System.Drawing.Point(63, 15)
        Me.tbNombrePlantilla.Name = "tbNombrePlantilla"
        Me.tbNombrePlantilla.Size = New System.Drawing.Size(169, 20)
        Me.tbNombrePlantilla.TabIndex = 1
        Me.tbNombrePlantilla.Text = "Reporte de Ventas Mensual"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Nombre: "
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'frm000PlantillasConfColParam
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(708, 653)
        Me.Controls.Add(Me.groupBox3)
        Me.Controls.Add(Me.groupBox2)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.ControlPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MinimizeBox = False
        Me.Name = "frm000PlantillasConfColParam"
        Me.Text = "Gestor"
        Me.Controls.SetChildIndex(Me.ControlPanel, 0)
        Me.Controls.SetChildIndex(Me.groupBox1, 0)
        Me.Controls.SetChildIndex(Me.groupBox2, 0)
        Me.Controls.SetChildIndex(Me.groupBox3, 0)
        Me.Controls.SetChildIndex(Me.LblMensaje, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.LblAccion, 0)
        Me.ControlPanel.ResumeLayout(False)
        Me.ControlPanel.PerformLayout()
        Me.groupBox3.ResumeLayout(False)
        Me.TCColumasnParametros.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ControlPanel As System.Windows.Forms.Panel
    Friend WithEvents lblVersionModulo As System.Windows.Forms.Label
    Private WithEvents groupBox3 As GroupBox
    Private WithEvents groupBox2 As GroupBox
    Private WithEvents rtbConsultaSQL As RichTextBox
    Private WithEvents groupBox1 As GroupBox
    Private WithEvents label3 As Label
    Private WithEvents bSeleccionarPlantilla As Button
    Private WithEvents tbRutaPlantillaxlsx As TextBox
    Private WithEvents label2 As Label
    Private WithEvents tbNombrePlantilla As TextBox
    Private WithEvents Label7 As Label
    Friend WithEvents TCColumasnParametros As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents cbFormato As ComboBox
    Friend WithEvents tbOrden As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents tbEtiqueta As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents tbCampo As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents tbOrdenParametro As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents TbValorDefault As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents checkbRequerido As CheckBox
    Friend WithEvents Label11 As Label
    Friend WithEvents tbEtiquetaParametro As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents cbTipo As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents tbNombre As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents bModificarColumna As Button
    Friend WithEvents bAgregarColumna As Button
    Friend WithEvents ColumnasCargadas As ListBox
    Friend WithEvents ParametrosCargados As ListBox
    Friend WithEvents bModificarParametro As Button
    Friend WithEvents bAgregarParametro As Button
    Private WithEvents lbDescripción As Label
    Friend WithEvents RtbDescripcion As RichTextBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Private WithEvents tbCliente As TextBox
    Friend WithEvents chkEstatus As CheckBox
    Private WithEvents Label15 As Label
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm000ProgramarEjecucion
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ControlPanel = New System.Windows.Forms.Panel()
        Me.lblVersionModulo = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.ckbEstatus = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.tbNombreProgramacion = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.cbPlantillas = New System.Windows.Forms.ComboBox()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbFrecuenciaQ = New System.Windows.Forms.RadioButton()
        Me.HoraEjecucion = New System.Windows.Forms.DateTimePicker()
        Me.label3 = New System.Windows.Forms.Label()
        Me.groupBox3 = New System.Windows.Forms.GroupBox()
        Me.numericUDMes = New System.Windows.Forms.NumericUpDown()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.checkbD = New System.Windows.Forms.CheckBox()
        Me.checkbS = New System.Windows.Forms.CheckBox()
        Me.checkbV = New System.Windows.Forms.CheckBox()
        Me.checkbJ = New System.Windows.Forms.CheckBox()
        Me.checkbMie = New System.Windows.Forms.CheckBox()
        Me.checkbMartes = New System.Windows.Forms.CheckBox()
        Me.checkbL = New System.Windows.Forms.CheckBox()
        Me.rbFrecuenciaM = New System.Windows.Forms.RadioButton()
        Me.rbFrecuenciaS = New System.Windows.Forms.RadioButton()
        Me.rbFrecuenciaD = New System.Windows.Forms.RadioButton()
        Me.rbFrecuenciaU = New System.Windows.Forms.RadioButton()
        Me.groupBoxVigencia = New System.Windows.Forms.GroupBox()
        Me.lblVigenciaInicio = New System.Windows.Forms.Label()
        Me.dtpVigenciaInicio = New System.Windows.Forms.DateTimePicker()
        Me.lblVigenciaFin = New System.Windows.Forms.Label()
        Me.dtpVigenciaFin = New System.Windows.Forms.DateTimePicker()
        Me.lblVigenciaFinOpc = New System.Windows.Forms.Label()
        Me.groupBox4 = New System.Windows.Forms.GroupBox()
        Me.dgvParametros = New System.Windows.Forms.DataGridView()
        Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Valor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExpresionesInfo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ControlPanel.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.groupBox3.SuspendLayout()
        CType(Me.numericUDMes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox2.SuspendLayout()
        Me.groupBoxVigencia.SuspendLayout()
        Me.groupBox4.SuspendLayout()
        CType(Me.dgvParametros, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblMensaje
        '
        Me.LblMensaje.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LblMensaje.Location = New System.Drawing.Point(23, 530)
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(23, 5)
        Me.Label6.Size = New System.Drawing.Size(284, 31)
        Me.Label6.Text = "{Programar Ejecución}"
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
        Me.ControlPanel.Location = New System.Drawing.Point(0, 550)
        Me.ControlPanel.Name = "ControlPanel"
        Me.ControlPanel.Size = New System.Drawing.Size(449, 78)
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
        Me.lblVersionModulo.Location = New System.Drawing.Point(386, 61)
        Me.lblVersionModulo.Name = "lblVersionModulo"
        Me.lblVersionModulo.Size = New System.Drawing.Size(58, 13)
        Me.lblVersionModulo.TabIndex = 357
        Me.lblVersionModulo.Text = "v.0.0.0.0"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.ckbEstatus)
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Controls.Add(Me.tbNombreProgramacion)
        Me.GroupBox5.Controls.Add(Me.label2)
        Me.GroupBox5.Controls.Add(Me.cbPlantillas)
        Me.GroupBox5.Location = New System.Drawing.Point(29, 63)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(388, 80)
        Me.GroupBox5.TabIndex = 366
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Datos Generales"
        '
        'ckbEstatus
        '
        Me.ckbEstatus.AutoSize = True
        Me.ckbEstatus.Checked = True
        Me.ckbEstatus.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckbEstatus.Location = New System.Drawing.Point(272, 28)
        Me.ckbEstatus.Name = "ckbEstatus"
        Me.ckbEstatus.Size = New System.Drawing.Size(15, 14)
        Me.ckbEstatus.TabIndex = 365
        Me.ckbEstatus.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(225, 29)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 13)
        Me.Label8.TabIndex = 364
        Me.Label8.Text = "Activo: "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 13)
        Me.Label7.TabIndex = 360
        Me.Label7.Text = "Nombre: "
        '
        'tbNombreProgramacion
        '
        Me.tbNombreProgramacion.Location = New System.Drawing.Point(62, 22)
        Me.tbNombreProgramacion.Name = "tbNombreProgramacion"
        Me.tbNombreProgramacion.Size = New System.Drawing.Size(153, 20)
        Me.tbNombreProgramacion.TabIndex = 361
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(7, 54)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(49, 13)
        Me.label2.TabIndex = 362
        Me.label2.Text = "Plantilla: "
        '
        'cbPlantillas
        '
        Me.cbPlantillas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPlantillas.FormattingEnabled = True
        Me.cbPlantillas.Location = New System.Drawing.Point(62, 51)
        Me.cbPlantillas.Name = "cbPlantillas"
        Me.cbPlantillas.Size = New System.Drawing.Size(153, 21)
        Me.cbPlantillas.TabIndex = 363
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.rbFrecuenciaQ)
        Me.groupBox1.Controls.Add(Me.HoraEjecucion)
        Me.groupBox1.Controls.Add(Me.label3)
        Me.groupBox1.Controls.Add(Me.groupBox3)
        Me.groupBox1.Controls.Add(Me.groupBox2)
        Me.groupBox1.Controls.Add(Me.rbFrecuenciaM)
        Me.groupBox1.Controls.Add(Me.rbFrecuenciaS)
        Me.groupBox1.Controls.Add(Me.rbFrecuenciaD)
        Me.groupBox1.Controls.Add(Me.rbFrecuenciaU)
        Me.groupBox1.Location = New System.Drawing.Point(29, 149)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(388, 206)
        Me.groupBox1.TabIndex = 364
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Frecuencia:"
        '
        'rbFrecuenciaQ
        '
        Me.rbFrecuenciaQ.AutoSize = True
        Me.rbFrecuenciaQ.Location = New System.Drawing.Point(289, 20)
        Me.rbFrecuenciaQ.Name = "rbFrecuenciaQ"
        Me.rbFrecuenciaQ.Size = New System.Drawing.Size(73, 17)
        Me.rbFrecuenciaQ.TabIndex = 9
        Me.rbFrecuenciaQ.Text = "Quincenal"
        Me.rbFrecuenciaQ.UseVisualStyleBackColor = True
        '
        'HoraEjecucion
        '
        Me.HoraEjecucion.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.HoraEjecucion.Location = New System.Drawing.Point(42, 169)
        Me.HoraEjecucion.Name = "HoraEjecucion"
        Me.HoraEjecucion.ShowUpDown = True
        Me.HoraEjecucion.Size = New System.Drawing.Size(200, 20)
        Me.HoraEjecucion.TabIndex = 10
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(9, 172)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(33, 13)
        Me.label3.TabIndex = 11
        Me.label3.Text = "Hora:"
        '
        'groupBox3
        '
        Me.groupBox3.Controls.Add(Me.numericUDMes)
        Me.groupBox3.Location = New System.Drawing.Point(218, 57)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(132, 100)
        Me.groupBox3.TabIndex = 6
        Me.groupBox3.TabStop = False
        Me.groupBox3.Text = "Día Mes"
        '
        'numericUDMes
        '
        Me.numericUDMes.Location = New System.Drawing.Point(7, 20)
        Me.numericUDMes.Maximum = New Decimal(New Integer() {31, 0, 0, 0})
        Me.numericUDMes.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numericUDMes.Name = "numericUDMes"
        Me.numericUDMes.Size = New System.Drawing.Size(120, 20)
        Me.numericUDMes.TabIndex = 0
        Me.numericUDMes.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.checkbD)
        Me.groupBox2.Controls.Add(Me.checkbS)
        Me.groupBox2.Controls.Add(Me.checkbV)
        Me.groupBox2.Controls.Add(Me.checkbJ)
        Me.groupBox2.Controls.Add(Me.checkbMie)
        Me.groupBox2.Controls.Add(Me.checkbMartes)
        Me.groupBox2.Controls.Add(Me.checkbL)
        Me.groupBox2.Location = New System.Drawing.Point(12, 52)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(200, 111)
        Me.groupBox2.TabIndex = 5
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "Días Semana"
        '
        'checkbD
        '
        Me.checkbD.AutoSize = True
        Me.checkbD.Location = New System.Drawing.Point(97, 66)
        Me.checkbD.Name = "checkbD"
        Me.checkbD.Size = New System.Drawing.Size(68, 17)
        Me.checkbD.TabIndex = 0
        Me.checkbD.Text = "Domingo"
        '
        'checkbS
        '
        Me.checkbS.AutoSize = True
        Me.checkbS.Location = New System.Drawing.Point(97, 43)
        Me.checkbS.Name = "checkbS"
        Me.checkbS.Size = New System.Drawing.Size(63, 17)
        Me.checkbS.TabIndex = 1
        Me.checkbS.Text = "Sábado"
        '
        'checkbV
        '
        Me.checkbV.AutoSize = True
        Me.checkbV.Location = New System.Drawing.Point(97, 20)
        Me.checkbV.Name = "checkbV"
        Me.checkbV.Size = New System.Drawing.Size(61, 17)
        Me.checkbV.TabIndex = 2
        Me.checkbV.Text = "Viernes"
        '
        'checkbJ
        '
        Me.checkbJ.AutoSize = True
        Me.checkbJ.Location = New System.Drawing.Point(6, 88)
        Me.checkbJ.Name = "checkbJ"
        Me.checkbJ.Size = New System.Drawing.Size(60, 17)
        Me.checkbJ.TabIndex = 3
        Me.checkbJ.Text = "Jueves"
        '
        'checkbMie
        '
        Me.checkbMie.AutoSize = True
        Me.checkbMie.Location = New System.Drawing.Point(6, 66)
        Me.checkbMie.Name = "checkbMie"
        Me.checkbMie.Size = New System.Drawing.Size(71, 17)
        Me.checkbMie.TabIndex = 4
        Me.checkbMie.Text = "Miércoles"
        '
        'checkbMartes
        '
        Me.checkbMartes.AutoSize = True
        Me.checkbMartes.Location = New System.Drawing.Point(6, 43)
        Me.checkbMartes.Name = "checkbMartes"
        Me.checkbMartes.Size = New System.Drawing.Size(58, 17)
        Me.checkbMartes.TabIndex = 5
        Me.checkbMartes.Text = "Martes"
        '
        'checkbL
        '
        Me.checkbL.AutoSize = True
        Me.checkbL.Location = New System.Drawing.Point(7, 20)
        Me.checkbL.Name = "checkbL"
        Me.checkbL.Size = New System.Drawing.Size(55, 17)
        Me.checkbL.TabIndex = 6
        Me.checkbL.Text = "Lunes"
        '
        'rbFrecuenciaM
        '
        Me.rbFrecuenciaM.AutoSize = True
        Me.rbFrecuenciaM.Location = New System.Drawing.Point(218, 20)
        Me.rbFrecuenciaM.Name = "rbFrecuenciaM"
        Me.rbFrecuenciaM.Size = New System.Drawing.Size(65, 17)
        Me.rbFrecuenciaM.TabIndex = 3
        Me.rbFrecuenciaM.Text = "Mensual"
        Me.rbFrecuenciaM.UseVisualStyleBackColor = True
        '
        'rbFrecuenciaS
        '
        Me.rbFrecuenciaS.AutoSize = True
        Me.rbFrecuenciaS.Location = New System.Drawing.Point(146, 20)
        Me.rbFrecuenciaS.Name = "rbFrecuenciaS"
        Me.rbFrecuenciaS.Size = New System.Drawing.Size(66, 17)
        Me.rbFrecuenciaS.TabIndex = 2
        Me.rbFrecuenciaS.Text = "Semanal"
        Me.rbFrecuenciaS.UseVisualStyleBackColor = True
        '
        'rbFrecuenciaD
        '
        Me.rbFrecuenciaD.AutoSize = True
        Me.rbFrecuenciaD.Location = New System.Drawing.Point(86, 20)
        Me.rbFrecuenciaD.Name = "rbFrecuenciaD"
        Me.rbFrecuenciaD.Size = New System.Drawing.Size(54, 17)
        Me.rbFrecuenciaD.TabIndex = 1
        Me.rbFrecuenciaD.Text = "Díario"
        Me.rbFrecuenciaD.UseVisualStyleBackColor = True
        '
        'rbFrecuenciaU
        '
        Me.rbFrecuenciaU.AutoSize = True
        Me.rbFrecuenciaU.Location = New System.Drawing.Point(7, 20)
        Me.rbFrecuenciaU.Name = "rbFrecuenciaU"
        Me.rbFrecuenciaU.Size = New System.Drawing.Size(73, 17)
        Me.rbFrecuenciaU.TabIndex = 0
        Me.rbFrecuenciaU.Text = "Única vez"
        Me.rbFrecuenciaU.UseVisualStyleBackColor = True
        '
        'groupBoxVigencia
        '
        Me.groupBoxVigencia.Controls.Add(Me.lblVigenciaInicio)
        Me.groupBoxVigencia.Controls.Add(Me.dtpVigenciaInicio)
        Me.groupBoxVigencia.Controls.Add(Me.lblVigenciaFin)
        Me.groupBoxVigencia.Controls.Add(Me.dtpVigenciaFin)
        Me.groupBoxVigencia.Controls.Add(Me.lblVigenciaFinOpc)
        Me.groupBoxVigencia.Location = New System.Drawing.Point(29, 361)
        Me.groupBoxVigencia.Name = "groupBoxVigencia"
        Me.groupBoxVigencia.Size = New System.Drawing.Size(388, 72)
        Me.groupBoxVigencia.TabIndex = 367
        Me.groupBoxVigencia.TabStop = False
        Me.groupBoxVigencia.Text = "Vigencia de la programación:"
        '
        'lblVigenciaInicio
        '
        Me.lblVigenciaInicio.AutoSize = True
        Me.lblVigenciaInicio.Location = New System.Drawing.Point(6, 22)
        Me.lblVigenciaInicio.Name = "lblVigenciaInicio"
        Me.lblVigenciaInicio.Size = New System.Drawing.Size(41, 13)
        Me.lblVigenciaInicio.TabIndex = 0
        Me.lblVigenciaInicio.Text = "Desde:"
        '
        'dtpVigenciaInicio
        '
        Me.dtpVigenciaInicio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpVigenciaInicio.Location = New System.Drawing.Point(52, 18)
        Me.dtpVigenciaInicio.Name = "dtpVigenciaInicio"
        Me.dtpVigenciaInicio.Size = New System.Drawing.Size(120, 20)
        Me.dtpVigenciaInicio.TabIndex = 1
        Me.dtpVigenciaInicio.Value = New Date(2026, 3, 24, 0, 0, 0, 0)
        '
        'lblVigenciaFin
        '
        Me.lblVigenciaFin.AutoSize = True
        Me.lblVigenciaFin.Location = New System.Drawing.Point(185, 22)
        Me.lblVigenciaFin.Name = "lblVigenciaFin"
        Me.lblVigenciaFin.Size = New System.Drawing.Size(38, 13)
        Me.lblVigenciaFin.TabIndex = 2
        Me.lblVigenciaFin.Text = "Hasta:"
        '
        'dtpVigenciaFin
        '
        Me.dtpVigenciaFin.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpVigenciaFin.Location = New System.Drawing.Point(228, 18)
        Me.dtpVigenciaFin.Name = "dtpVigenciaFin"
        Me.dtpVigenciaFin.Size = New System.Drawing.Size(120, 20)
        Me.dtpVigenciaFin.TabIndex = 3
        Me.dtpVigenciaFin.Value = New Date(2027, 3, 24, 0, 0, 0, 0)
        '
        'lblVigenciaFinOpc
        '
        Me.lblVigenciaFinOpc.AutoSize = True
        Me.lblVigenciaFinOpc.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!)
        Me.lblVigenciaFinOpc.ForeColor = System.Drawing.Color.Gray
        Me.lblVigenciaFinOpc.Location = New System.Drawing.Point(228, 42)
        Me.lblVigenciaFinOpc.Name = "lblVigenciaFinOpc"
        Me.lblVigenciaFinOpc.Size = New System.Drawing.Size(129, 13)
        Me.lblVigenciaFinOpc.TabIndex = 4
        Me.lblVigenciaFinOpc.Text = "(obligatoria para D/S/M/Q)"
        '
        'groupBox4
        '
        Me.groupBox4.Controls.Add(Me.dgvParametros)
        Me.groupBox4.Location = New System.Drawing.Point(29, 439)
        Me.groupBox4.Name = "groupBox4"
        Me.groupBox4.Size = New System.Drawing.Size(388, 100)
        Me.groupBox4.TabIndex = 365
        Me.groupBox4.TabStop = False
        Me.groupBox4.Text = "Parámetros:"
        '
        'dgvParametros
        '
        Me.dgvParametros.AllowUserToAddRows = False
        Me.dgvParametros.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvParametros.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvParametros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvParametros.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nombre, Me.Valor, Me.Tipo, Me.ExpresionesInfo})
        Me.dgvParametros.Location = New System.Drawing.Point(6, 18)
        Me.dgvParametros.Name = "dgvParametros"
        Me.dgvParametros.RowHeadersVisible = False
        Me.dgvParametros.Size = New System.Drawing.Size(372, 75)
        Me.dgvParametros.TabIndex = 0
        '
        'Nombre
        '
        Me.Nombre.HeaderText = "Parámetro"
        Me.Nombre.Name = "Nombre"
        Me.Nombre.ReadOnly = True
        Me.Nombre.Width = 110
        '
        'Valor
        '
        Me.Valor.HeaderText = "Valor"
        Me.Valor.Name = "Valor"
        Me.Valor.Width = 130
        '
        'Tipo
        '
        Me.Tipo.HeaderText = "Tipo"
        Me.Tipo.Name = "Tipo"
        Me.Tipo.ReadOnly = True
        Me.Tipo.Width = 55
        '
        'ExpresionesInfo
        '
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.DimGray
        Me.ExpresionesInfo.DefaultCellStyle = DataGridViewCellStyle2
        Me.ExpresionesInfo.HeaderText = "Expresiones válidas"
        Me.ExpresionesInfo.Name = "ExpresionesInfo"
        Me.ExpresionesInfo.ReadOnly = True
        Me.ExpresionesInfo.Width = 160
        '
        'frm000ProgramarEjecucion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(449, 628)
        Me.Controls.Add(Me.groupBox4)
        Me.Controls.Add(Me.groupBoxVigencia)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.ControlPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MinimizeBox = False
        Me.Name = "frm000ProgramarEjecucion"
        Me.Text = "Gestor"
        Me.Controls.SetChildIndex(Me.ControlPanel, 0)
        Me.Controls.SetChildIndex(Me.GroupBox5, 0)
        Me.Controls.SetChildIndex(Me.groupBox1, 0)
        Me.Controls.SetChildIndex(Me.groupBoxVigencia, 0)
        Me.Controls.SetChildIndex(Me.groupBox4, 0)
        Me.Controls.SetChildIndex(Me.LblMensaje, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.LblAccion, 0)
        Me.ControlPanel.ResumeLayout(False)
        Me.ControlPanel.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.groupBox3.ResumeLayout(False)
        CType(Me.numericUDMes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox2.PerformLayout()
        Me.groupBoxVigencia.ResumeLayout(False)
        Me.groupBoxVigencia.PerformLayout()
        Me.groupBox4.ResumeLayout(False)
        CType(Me.dgvParametros, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ControlPanel As System.Windows.Forms.Panel
    Friend WithEvents lblVersionModulo As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents ckbEstatus As CheckBox
    Private WithEvents Label8 As Label
    Private WithEvents Label7 As Label
    Private WithEvents tbNombreProgramacion As TextBox
    Private WithEvents label2 As Label
    Private WithEvents cbPlantillas As ComboBox
    Private WithEvents groupBox1 As GroupBox
    Private WithEvents rbFrecuenciaQ As RadioButton
    Private WithEvents HoraEjecucion As DateTimePicker
    Private WithEvents label3 As Label
    Private WithEvents groupBox3 As GroupBox
    Private WithEvents numericUDMes As NumericUpDown
    Private WithEvents groupBox2 As GroupBox
    Private WithEvents checkbD As CheckBox
    Private WithEvents checkbS As CheckBox
    Private WithEvents checkbV As CheckBox
    Private WithEvents checkbJ As CheckBox
    Private WithEvents checkbMie As CheckBox
    Private WithEvents checkbMartes As CheckBox
    Private WithEvents checkbL As CheckBox
    Private WithEvents rbFrecuenciaM As RadioButton
    Private WithEvents rbFrecuenciaS As RadioButton
    Private WithEvents rbFrecuenciaD As RadioButton
    Private WithEvents rbFrecuenciaU As RadioButton
    ' Nuevos controles de vigencia
    Private WithEvents groupBoxVigencia As GroupBox
    Private WithEvents dtpVigenciaInicio As DateTimePicker
    Private WithEvents lblVigenciaInicio As Label
    Private WithEvents dtpVigenciaFin As DateTimePicker
    Private WithEvents lblVigenciaFin As Label
    Private WithEvents lblVigenciaFinOpc As Label
    ' Grid de parámetros
    Private WithEvents groupBox4 As GroupBox
    Friend WithEvents dgvParametros As DataGridView
    Friend WithEvents Nombre As DataGridViewTextBoxColumn
    Friend WithEvents Valor As DataGridViewTextBoxColumn
    Friend WithEvents Tipo As DataGridViewTextBoxColumn
    Friend WithEvents ExpresionesInfo As DataGridViewTextBoxColumn
End Class
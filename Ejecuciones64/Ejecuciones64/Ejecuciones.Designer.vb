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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ControlPanel = New System.Windows.Forms.Panel()
        Me.lblVersionModulo = New System.Windows.Forms.Label()
        Me.groupBox4 = New System.Windows.Forms.GroupBox()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
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
        Me.cbPlantillas = New System.Windows.Forms.ComboBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.tbNombreProgramacion = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.rbFrecuenciaQ = New System.Windows.Forms.RadioButton()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ckbEstatus = New System.Windows.Forms.CheckBox()
        Me.dgvParametros = New System.Windows.Forms.DataGridView()
        Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Valor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ControlPanel.SuspendLayout()
        Me.groupBox4.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.groupBox3.SuspendLayout()
        CType(Me.numericUDMes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.dgvParametros, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblMensaje
        '
        Me.LblMensaje.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LblMensaje.Location = New System.Drawing.Point(23, 425)
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
        Me.ControlPanel.Location = New System.Drawing.Point(0, 444)
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
        'groupBox4
        '
        Me.groupBox4.Controls.Add(Me.dgvParametros)
        Me.groupBox4.Location = New System.Drawing.Point(32, 344)
        Me.groupBox4.Name = "groupBox4"
        Me.groupBox4.Size = New System.Drawing.Size(385, 95)
        Me.groupBox4.TabIndex = 365
        Me.groupBox4.TabStop = False
        Me.groupBox4.Text = "Parametros: "
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
        Me.groupBox1.Location = New System.Drawing.Point(32, 132)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(372, 206)
        Me.groupBox1.TabIndex = 364
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Frecuencia: "
        '
        'HoraEjecucion
        '
        Me.HoraEjecucion.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.HoraEjecucion.Location = New System.Drawing.Point(42, 169)
        Me.HoraEjecucion.Name = "HoraEjecucion"
        Me.HoraEjecucion.Size = New System.Drawing.Size(200, 20)
        Me.HoraEjecucion.TabIndex = 8
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(9, 172)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(36, 13)
        Me.label3.TabIndex = 7
        Me.label3.Text = "Hora: "
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
        Me.numericUDMes.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.numericUDMes.Location = New System.Drawing.Point(7, 20)
        Me.numericUDMes.Maximum = New Decimal(New Integer() {31, 0, 0, 0})
        Me.numericUDMes.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numericUDMes.Name = "numericUDMes"
        Me.numericUDMes.Size = New System.Drawing.Size(120, 20)
        Me.numericUDMes.TabIndex = 0
        Me.numericUDMes.UseWaitCursor = True
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
        Me.checkbD.TabIndex = 8
        Me.checkbD.Text = "Domingo"
        Me.checkbD.UseVisualStyleBackColor = True
        '
        'checkbS
        '
        Me.checkbS.AutoSize = True
        Me.checkbS.Location = New System.Drawing.Point(97, 43)
        Me.checkbS.Name = "checkbS"
        Me.checkbS.Size = New System.Drawing.Size(63, 17)
        Me.checkbS.TabIndex = 7
        Me.checkbS.Text = "Sábado"
        Me.checkbS.UseVisualStyleBackColor = True
        '
        'checkbV
        '
        Me.checkbV.AutoSize = True
        Me.checkbV.Location = New System.Drawing.Point(97, 20)
        Me.checkbV.Name = "checkbV"
        Me.checkbV.Size = New System.Drawing.Size(61, 17)
        Me.checkbV.TabIndex = 6
        Me.checkbV.Text = "Viernes"
        Me.checkbV.UseVisualStyleBackColor = True
        '
        'checkbJ
        '
        Me.checkbJ.AutoSize = True
        Me.checkbJ.Location = New System.Drawing.Point(6, 88)
        Me.checkbJ.Name = "checkbJ"
        Me.checkbJ.Size = New System.Drawing.Size(60, 17)
        Me.checkbJ.TabIndex = 5
        Me.checkbJ.Text = "Jueves"
        Me.checkbJ.UseVisualStyleBackColor = True
        '
        'checkbMie
        '
        Me.checkbMie.AutoSize = True
        Me.checkbMie.Location = New System.Drawing.Point(6, 66)
        Me.checkbMie.Name = "checkbMie"
        Me.checkbMie.Size = New System.Drawing.Size(71, 17)
        Me.checkbMie.TabIndex = 5
        Me.checkbMie.Text = "Miércoles"
        Me.checkbMie.UseVisualStyleBackColor = True
        '
        'checkbMartes
        '
        Me.checkbMartes.AutoSize = True
        Me.checkbMartes.Location = New System.Drawing.Point(6, 43)
        Me.checkbMartes.Name = "checkbMartes"
        Me.checkbMartes.Size = New System.Drawing.Size(58, 17)
        Me.checkbMartes.TabIndex = 1
        Me.checkbMartes.Text = "Martes"
        Me.checkbMartes.UseVisualStyleBackColor = True
        '
        'checkbL
        '
        Me.checkbL.AutoSize = True
        Me.checkbL.Location = New System.Drawing.Point(7, 20)
        Me.checkbL.Name = "checkbL"
        Me.checkbL.Size = New System.Drawing.Size(55, 17)
        Me.checkbL.TabIndex = 0
        Me.checkbL.Text = "Lunes"
        Me.checkbL.UseVisualStyleBackColor = True
        '
        'rbFrecuenciaM
        '
        Me.rbFrecuenciaM.AutoSize = True
        Me.rbFrecuenciaM.Location = New System.Drawing.Point(218, 20)
        Me.rbFrecuenciaM.Name = "rbFrecuenciaM"
        Me.rbFrecuenciaM.Size = New System.Drawing.Size(65, 17)
        Me.rbFrecuenciaM.TabIndex = 3
        Me.rbFrecuenciaM.TabStop = True
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
        Me.rbFrecuenciaS.TabStop = True
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
        Me.rbFrecuenciaD.TabStop = True
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
        Me.rbFrecuenciaU.TabStop = True
        Me.rbFrecuenciaU.Text = "Única vez"
        Me.rbFrecuenciaU.UseVisualStyleBackColor = True
        '
        'cbPlantillas
        '
        Me.cbPlantillas.FormattingEnabled = True
        Me.cbPlantillas.Items.AddRange(New Object() {"Reporte de Ventas Mensual", "Reporte Diario de Pedimentos", "Anexo 24", "Samsung", "TAMSA"})
        Me.cbPlantillas.Location = New System.Drawing.Point(62, 51)
        Me.cbPlantillas.Name = "cbPlantillas"
        Me.cbPlantillas.Size = New System.Drawing.Size(153, 21)
        Me.cbPlantillas.TabIndex = 363
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
        'tbNombreProgramacion
        '
        Me.tbNombreProgramacion.Location = New System.Drawing.Point(62, 22)
        Me.tbNombreProgramacion.Name = "tbNombreProgramacion"
        Me.tbNombreProgramacion.Size = New System.Drawing.Size(153, 20)
        Me.tbNombreProgramacion.TabIndex = 361
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
        'rbFrecuenciaQ
        '
        Me.rbFrecuenciaQ.AutoSize = True
        Me.rbFrecuenciaQ.Location = New System.Drawing.Point(289, 20)
        Me.rbFrecuenciaQ.Name = "rbFrecuenciaQ"
        Me.rbFrecuenciaQ.Size = New System.Drawing.Size(73, 17)
        Me.rbFrecuenciaQ.TabIndex = 9
        Me.rbFrecuenciaQ.TabStop = True
        Me.rbFrecuenciaQ.Text = "Quincenal"
        Me.rbFrecuenciaQ.UseVisualStyleBackColor = True
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
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(225, 29)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(48, 13)
        Me.Label8.TabIndex = 364
        Me.Label8.Text = "Estatus: "
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
        'dgvParametros
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvParametros.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvParametros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvParametros.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nombre, Me.Valor, Me.Tipo})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvParametros.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvParametros.Location = New System.Drawing.Point(19, 14)
        Me.dgvParametros.Name = "dgvParametros"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvParametros.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvParametros.Size = New System.Drawing.Size(343, 75)
        Me.dgvParametros.TabIndex = 0
        '
        'Nombre
        '
        Me.Nombre.HeaderText = "Nombre"
        Me.Nombre.Name = "Nombre"
        Me.Nombre.ReadOnly = True
        '
        'Valor
        '
        Me.Valor.HeaderText = "Valor"
        Me.Valor.Name = "Valor"
        '
        'Tipo
        '
        Me.Tipo.HeaderText = "Tipo"
        Me.Tipo.Name = "Tipo"
        Me.Tipo.ReadOnly = True
        '
        'frm000ProgramarEjecucion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(449, 522)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.groupBox4)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.ControlPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MinimizeBox = False
        Me.Name = "frm000ProgramarEjecucion"
        Me.Text = "Gestor"
        Me.Controls.SetChildIndex(Me.ControlPanel, 0)
        Me.Controls.SetChildIndex(Me.groupBox1, 0)
        Me.Controls.SetChildIndex(Me.groupBox4, 0)
        Me.Controls.SetChildIndex(Me.GroupBox5, 0)
        Me.Controls.SetChildIndex(Me.LblMensaje, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.LblAccion, 0)
        Me.ControlPanel.ResumeLayout(False)
        Me.ControlPanel.PerformLayout()
        Me.groupBox4.ResumeLayout(False)
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.groupBox3.ResumeLayout(False)
        CType(Me.numericUDMes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox2.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.dgvParametros, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ControlPanel As System.Windows.Forms.Panel
    Friend WithEvents lblVersionModulo As System.Windows.Forms.Label
    Private WithEvents groupBox4 As GroupBox
    Private WithEvents groupBox1 As GroupBox
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
    Private WithEvents cbPlantillas As ComboBox
    Private WithEvents label2 As Label
    Private WithEvents tbNombreProgramacion As TextBox
    Private WithEvents Label7 As Label
    Private WithEvents rbFrecuenciaQ As RadioButton
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents ckbEstatus As CheckBox
    Private WithEvents Label8 As Label
    Friend WithEvents dgvParametros As DataGridView
    Friend WithEvents Nombre As DataGridViewTextBoxColumn
    Friend WithEvents Valor As DataGridViewTextBoxColumn
    Friend WithEvents Tipo As DataGridViewTextBoxColumn
End Class

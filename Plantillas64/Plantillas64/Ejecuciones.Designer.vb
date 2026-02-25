<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm000ProgramarEjecuciones
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
        Me.ControlPanel = New System.Windows.Forms.Panel()
        Me.lblVersionModulo = New System.Windows.Forms.Label()
        Me.groupBox4 = New System.Windows.Forms.GroupBox()
        Me.textBox2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dateTimePicker3 = New System.Windows.Forms.DateTimePicker()
        Me.label5 = New System.Windows.Forms.Label()
        Me.dateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.label4 = New System.Windows.Forms.Label()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.dateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.label3 = New System.Windows.Forms.Label()
        Me.groupBox3 = New System.Windows.Forms.GroupBox()
        Me.numericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.checkBox7 = New System.Windows.Forms.CheckBox()
        Me.checkBox6 = New System.Windows.Forms.CheckBox()
        Me.checkBox5 = New System.Windows.Forms.CheckBox()
        Me.checkBox4 = New System.Windows.Forms.CheckBox()
        Me.checkBox3 = New System.Windows.Forms.CheckBox()
        Me.checkBox2 = New System.Windows.Forms.CheckBox()
        Me.checkBox1 = New System.Windows.Forms.CheckBox()
        Me.radioButton4 = New System.Windows.Forms.RadioButton()
        Me.radioButton3 = New System.Windows.Forms.RadioButton()
        Me.radioButton2 = New System.Windows.Forms.RadioButton()
        Me.radioButton1 = New System.Windows.Forms.RadioButton()
        Me.comboBox1 = New System.Windows.Forms.ComboBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.textBox1 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ControlPanel.SuspendLayout()
        Me.groupBox4.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.groupBox3.SuspendLayout()
        CType(Me.numericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'LblMensaje
        '
        Me.LblMensaje.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LblMensaje.Location = New System.Drawing.Point(23, 417)
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
        Me.ControlPanel.Location = New System.Drawing.Point(0, 436)
        Me.ControlPanel.Name = "ControlPanel"
        Me.ControlPanel.Size = New System.Drawing.Size(556, 78)
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
        Me.lblVersionModulo.Location = New System.Drawing.Point(493, 61)
        Me.lblVersionModulo.Name = "lblVersionModulo"
        Me.lblVersionModulo.Size = New System.Drawing.Size(58, 13)
        Me.lblVersionModulo.TabIndex = 357
        Me.lblVersionModulo.Text = "v.0.0.0.0"
        '
        'groupBox4
        '
        Me.groupBox4.Controls.Add(Me.textBox2)
        Me.groupBox4.Controls.Add(Me.Label1)
        Me.groupBox4.Controls.Add(Me.dateTimePicker3)
        Me.groupBox4.Controls.Add(Me.label5)
        Me.groupBox4.Controls.Add(Me.dateTimePicker2)
        Me.groupBox4.Controls.Add(Me.label4)
        Me.groupBox4.Location = New System.Drawing.Point(32, 335)
        Me.groupBox4.Name = "groupBox4"
        Me.groupBox4.Size = New System.Drawing.Size(295, 100)
        Me.groupBox4.TabIndex = 365
        Me.groupBox4.TabStop = False
        Me.groupBox4.Text = "Parametros: "
        '
        'textBox2
        '
        Me.textBox2.Location = New System.Drawing.Point(81, 68)
        Me.textBox2.Name = "textBox2"
        Me.textBox2.Size = New System.Drawing.Size(195, 20)
        Me.textBox2.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Cliente/RFC: "
        '
        'dateTimePicker3
        '
        Me.dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateTimePicker3.Location = New System.Drawing.Point(83, 41)
        Me.dateTimePicker3.Name = "dateTimePicker3"
        Me.dateTimePicker3.Size = New System.Drawing.Size(200, 20)
        Me.dateTimePicker3.TabIndex = 11
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(4, 48)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(60, 13)
        Me.label5.TabIndex = 10
        Me.label5.Text = "Fecha Fin: "
        '
        'dateTimePicker2
        '
        Me.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateTimePicker2.Location = New System.Drawing.Point(83, 19)
        Me.dateTimePicker2.Name = "dateTimePicker2"
        Me.dateTimePicker2.Size = New System.Drawing.Size(200, 20)
        Me.dateTimePicker2.TabIndex = 9
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(6, 25)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(71, 13)
        Me.label4.TabIndex = 3
        Me.label4.Text = "Fecha Inicio: "
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.dateTimePicker1)
        Me.groupBox1.Controls.Add(Me.label3)
        Me.groupBox1.Controls.Add(Me.groupBox3)
        Me.groupBox1.Controls.Add(Me.groupBox2)
        Me.groupBox1.Controls.Add(Me.radioButton4)
        Me.groupBox1.Controls.Add(Me.radioButton3)
        Me.groupBox1.Controls.Add(Me.radioButton2)
        Me.groupBox1.Controls.Add(Me.radioButton1)
        Me.groupBox1.Location = New System.Drawing.Point(32, 123)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(356, 206)
        Me.groupBox1.TabIndex = 364
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Frecuencia: "
        '
        'dateTimePicker1
        '
        Me.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dateTimePicker1.Location = New System.Drawing.Point(42, 169)
        Me.dateTimePicker1.Name = "dateTimePicker1"
        Me.dateTimePicker1.Size = New System.Drawing.Size(200, 20)
        Me.dateTimePicker1.TabIndex = 8
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
        Me.groupBox3.Controls.Add(Me.numericUpDown1)
        Me.groupBox3.Location = New System.Drawing.Point(218, 57)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(132, 100)
        Me.groupBox3.TabIndex = 6
        Me.groupBox3.TabStop = False
        Me.groupBox3.Text = "Día Mes"
        '
        'numericUpDown1
        '
        Me.numericUpDown1.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.numericUpDown1.Location = New System.Drawing.Point(7, 20)
        Me.numericUpDown1.Maximum = New Decimal(New Integer() {31, 0, 0, 0})
        Me.numericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numericUpDown1.Name = "numericUpDown1"
        Me.numericUpDown1.Size = New System.Drawing.Size(120, 20)
        Me.numericUpDown1.TabIndex = 0
        Me.numericUpDown1.UseWaitCursor = True
        Me.numericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.checkBox7)
        Me.groupBox2.Controls.Add(Me.checkBox6)
        Me.groupBox2.Controls.Add(Me.checkBox5)
        Me.groupBox2.Controls.Add(Me.checkBox4)
        Me.groupBox2.Controls.Add(Me.checkBox3)
        Me.groupBox2.Controls.Add(Me.checkBox2)
        Me.groupBox2.Controls.Add(Me.checkBox1)
        Me.groupBox2.Location = New System.Drawing.Point(12, 52)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(200, 111)
        Me.groupBox2.TabIndex = 5
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "Días Semana"
        '
        'checkBox7
        '
        Me.checkBox7.AutoSize = True
        Me.checkBox7.Location = New System.Drawing.Point(97, 66)
        Me.checkBox7.Name = "checkBox7"
        Me.checkBox7.Size = New System.Drawing.Size(68, 17)
        Me.checkBox7.TabIndex = 8
        Me.checkBox7.Text = "Domingo"
        Me.checkBox7.UseVisualStyleBackColor = True
        '
        'checkBox6
        '
        Me.checkBox6.AutoSize = True
        Me.checkBox6.Location = New System.Drawing.Point(97, 43)
        Me.checkBox6.Name = "checkBox6"
        Me.checkBox6.Size = New System.Drawing.Size(63, 17)
        Me.checkBox6.TabIndex = 7
        Me.checkBox6.Text = "Sábado"
        Me.checkBox6.UseVisualStyleBackColor = True
        '
        'checkBox5
        '
        Me.checkBox5.AutoSize = True
        Me.checkBox5.Location = New System.Drawing.Point(97, 20)
        Me.checkBox5.Name = "checkBox5"
        Me.checkBox5.Size = New System.Drawing.Size(61, 17)
        Me.checkBox5.TabIndex = 6
        Me.checkBox5.Text = "Viernes"
        Me.checkBox5.UseVisualStyleBackColor = True
        '
        'checkBox4
        '
        Me.checkBox4.AutoSize = True
        Me.checkBox4.Location = New System.Drawing.Point(6, 88)
        Me.checkBox4.Name = "checkBox4"
        Me.checkBox4.Size = New System.Drawing.Size(60, 17)
        Me.checkBox4.TabIndex = 5
        Me.checkBox4.Text = "Jueves"
        Me.checkBox4.UseVisualStyleBackColor = True
        '
        'checkBox3
        '
        Me.checkBox3.AutoSize = True
        Me.checkBox3.Location = New System.Drawing.Point(6, 66)
        Me.checkBox3.Name = "checkBox3"
        Me.checkBox3.Size = New System.Drawing.Size(71, 17)
        Me.checkBox3.TabIndex = 5
        Me.checkBox3.Text = "Miércoles"
        Me.checkBox3.UseVisualStyleBackColor = True
        '
        'checkBox2
        '
        Me.checkBox2.AutoSize = True
        Me.checkBox2.Location = New System.Drawing.Point(6, 43)
        Me.checkBox2.Name = "checkBox2"
        Me.checkBox2.Size = New System.Drawing.Size(58, 17)
        Me.checkBox2.TabIndex = 1
        Me.checkBox2.Text = "Martes"
        Me.checkBox2.UseVisualStyleBackColor = True
        '
        'checkBox1
        '
        Me.checkBox1.AutoSize = True
        Me.checkBox1.Location = New System.Drawing.Point(7, 20)
        Me.checkBox1.Name = "checkBox1"
        Me.checkBox1.Size = New System.Drawing.Size(55, 17)
        Me.checkBox1.TabIndex = 0
        Me.checkBox1.Text = "Lunes"
        Me.checkBox1.UseVisualStyleBackColor = True
        '
        'radioButton4
        '
        Me.radioButton4.AutoSize = True
        Me.radioButton4.Location = New System.Drawing.Point(218, 20)
        Me.radioButton4.Name = "radioButton4"
        Me.radioButton4.Size = New System.Drawing.Size(65, 17)
        Me.radioButton4.TabIndex = 3
        Me.radioButton4.TabStop = True
        Me.radioButton4.Text = "Mensual"
        Me.radioButton4.UseVisualStyleBackColor = True
        '
        'radioButton3
        '
        Me.radioButton3.AutoSize = True
        Me.radioButton3.Location = New System.Drawing.Point(146, 20)
        Me.radioButton3.Name = "radioButton3"
        Me.radioButton3.Size = New System.Drawing.Size(66, 17)
        Me.radioButton3.TabIndex = 2
        Me.radioButton3.TabStop = True
        Me.radioButton3.Text = "Semanal"
        Me.radioButton3.UseVisualStyleBackColor = True
        '
        'radioButton2
        '
        Me.radioButton2.AutoSize = True
        Me.radioButton2.Location = New System.Drawing.Point(86, 20)
        Me.radioButton2.Name = "radioButton2"
        Me.radioButton2.Size = New System.Drawing.Size(54, 17)
        Me.radioButton2.TabIndex = 1
        Me.radioButton2.TabStop = True
        Me.radioButton2.Text = "Díario"
        Me.radioButton2.UseVisualStyleBackColor = True
        '
        'radioButton1
        '
        Me.radioButton1.AutoSize = True
        Me.radioButton1.Location = New System.Drawing.Point(7, 20)
        Me.radioButton1.Name = "radioButton1"
        Me.radioButton1.Size = New System.Drawing.Size(73, 17)
        Me.radioButton1.TabIndex = 0
        Me.radioButton1.TabStop = True
        Me.radioButton1.Text = "Única vez"
        Me.radioButton1.UseVisualStyleBackColor = True
        '
        'comboBox1
        '
        Me.comboBox1.FormattingEnabled = True
        Me.comboBox1.Items.AddRange(New Object() {"Reporte de Ventas Mensual", "Reporte Diario de Pedimentos", "Anexo 24", "Samsung", "TAMSA"})
        Me.comboBox1.Location = New System.Drawing.Point(84, 96)
        Me.comboBox1.Name = "comboBox1"
        Me.comboBox1.Size = New System.Drawing.Size(243, 21)
        Me.comboBox1.TabIndex = 363
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(29, 99)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(49, 13)
        Me.label2.TabIndex = 362
        Me.label2.Text = "Plantilla: "
        '
        'textBox1
        '
        Me.textBox1.Location = New System.Drawing.Point(84, 67)
        Me.textBox1.Name = "textBox1"
        Me.textBox1.Size = New System.Drawing.Size(242, 20)
        Me.textBox1.TabIndex = 361
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(28, 70)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 13)
        Me.Label7.TabIndex = 360
        Me.Label7.Text = "Nombre: "
        '
        'frm000ProgramarEjecuciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(556, 514)
        Me.Controls.Add(Me.groupBox4)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.comboBox1)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.textBox1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.ControlPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MinimizeBox = False
        Me.Name = "frm000ProgramarEjecuciones"
        Me.Text = "Gestor"
        Me.Controls.SetChildIndex(Me.LblMensaje, 0)
        Me.Controls.SetChildIndex(Me.ControlPanel, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.LblAccion, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.textBox1, 0)
        Me.Controls.SetChildIndex(Me.label2, 0)
        Me.Controls.SetChildIndex(Me.comboBox1, 0)
        Me.Controls.SetChildIndex(Me.groupBox1, 0)
        Me.Controls.SetChildIndex(Me.groupBox4, 0)
        Me.ControlPanel.ResumeLayout(False)
        Me.ControlPanel.PerformLayout()
        Me.groupBox4.ResumeLayout(False)
        Me.groupBox4.PerformLayout()
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.groupBox3.ResumeLayout(False)
        CType(Me.numericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ControlPanel As System.Windows.Forms.Panel
    Friend WithEvents lblVersionModulo As System.Windows.Forms.Label
    Private WithEvents groupBox4 As GroupBox
    Private WithEvents textBox2 As TextBox
    Private WithEvents Label1 As Label
    Private WithEvents dateTimePicker3 As DateTimePicker
    Private WithEvents label5 As Label
    Private WithEvents dateTimePicker2 As DateTimePicker
    Private WithEvents label4 As Label
    Private WithEvents groupBox1 As GroupBox
    Private WithEvents dateTimePicker1 As DateTimePicker
    Private WithEvents label3 As Label
    Private WithEvents groupBox3 As GroupBox
    Private WithEvents numericUpDown1 As NumericUpDown
    Private WithEvents groupBox2 As GroupBox
    Private WithEvents checkBox7 As CheckBox
    Private WithEvents checkBox6 As CheckBox
    Private WithEvents checkBox5 As CheckBox
    Private WithEvents checkBox4 As CheckBox
    Private WithEvents checkBox3 As CheckBox
    Private WithEvents checkBox2 As CheckBox
    Private WithEvents checkBox1 As CheckBox
    Private WithEvents radioButton4 As RadioButton
    Private WithEvents radioButton3 As RadioButton
    Private WithEvents radioButton2 As RadioButton
    Private WithEvents radioButton1 As RadioButton
    Private WithEvents comboBox1 As ComboBox
    Private WithEvents label2 As Label
    Private WithEvents textBox1 As TextBox
    Private WithEvents Label7 As Label
End Class

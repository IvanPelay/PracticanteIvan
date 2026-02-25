<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm000Plantillas
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
        Me.groupBox3 = New System.Windows.Forms.GroupBox()
        Me.lbConteoParametros = New System.Windows.Forms.Label()
        Me.lbConteoColumnas = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.button6 = New System.Windows.Forms.Button()
        Me.label5 = New System.Windows.Forms.Label()
        Me.button3 = New System.Windows.Forms.Button()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.richTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.comboBox1 = New System.Windows.Forms.ComboBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.button1 = New System.Windows.Forms.Button()
        Me.textBox2 = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.textBox1 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ControlPanel.SuspendLayout()
        Me.groupBox3.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        Me.groupBox1.SuspendLayout()
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
        'groupBox3
        '
        Me.groupBox3.Controls.Add(Me.lbConteoParametros)
        Me.groupBox3.Controls.Add(Me.lbConteoColumnas)
        Me.groupBox3.Controls.Add(Me.Label1)
        Me.groupBox3.Controls.Add(Me.button6)
        Me.groupBox3.Controls.Add(Me.label5)
        Me.groupBox3.Controls.Add(Me.button3)
        Me.groupBox3.Location = New System.Drawing.Point(85, 323)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(326, 85)
        Me.groupBox3.TabIndex = 363
        Me.groupBox3.TabStop = False
        Me.groupBox3.Text = "Parametros: "
        '
        'lbConteoParametros
        '
        Me.lbConteoParametros.AutoSize = True
        Me.lbConteoParametros.Location = New System.Drawing.Point(116, 54)
        Me.lbConteoParametros.Name = "lbConteoParametros"
        Me.lbConteoParametros.Size = New System.Drawing.Size(0, 13)
        Me.lbConteoParametros.TabIndex = 12
        '
        'lbConteoColumnas
        '
        Me.lbConteoColumnas.AutoSize = True
        Me.lbConteoColumnas.Location = New System.Drawing.Point(116, 25)
        Me.lbConteoColumnas.Name = "lbConteoColumnas"
        Me.lbConteoColumnas.Size = New System.Drawing.Size(0, 13)
        Me.lbConteoColumnas.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Columnas: "
        '
        'button6
        '
        Me.button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.button6.Location = New System.Drawing.Point(238, 48)
        Me.button6.Name = "button6"
        Me.button6.Size = New System.Drawing.Size(82, 23)
        Me.button6.TabIndex = 9
        Me.button6.Text = "Configurar Parametros"
        Me.button6.UseVisualStyleBackColor = True
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(18, 54)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(66, 13)
        Me.label5.TabIndex = 8
        Me.label5.Text = "Parametros: "
        '
        'button3
        '
        Me.button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.button3.Location = New System.Drawing.Point(238, 19)
        Me.button3.Name = "button3"
        Me.button3.Size = New System.Drawing.Size(82, 23)
        Me.button3.TabIndex = 8
        Me.button3.Text = "Configurar"
        Me.button3.UseVisualStyleBackColor = True
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.richTextBox1)
        Me.groupBox2.Location = New System.Drawing.Point(85, 170)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(326, 147)
        Me.groupBox2.TabIndex = 362
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "Consulta SQL: "
        '
        'richTextBox1
        '
        Me.richTextBox1.Location = New System.Drawing.Point(10, 20)
        Me.richTextBox1.Name = "richTextBox1"
        Me.richTextBox1.Size = New System.Drawing.Size(296, 75)
        Me.richTextBox1.TabIndex = 0
        Me.richTextBox1.Text = "SELECT * FROM Ventas WHERE Fecha BETWEEN @Inicio AND @Fin"
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.comboBox1)
        Me.groupBox1.Controls.Add(Me.label3)
        Me.groupBox1.Controls.Add(Me.button1)
        Me.groupBox1.Controls.Add(Me.textBox2)
        Me.groupBox1.Controls.Add(Me.label2)
        Me.groupBox1.Controls.Add(Me.textBox1)
        Me.groupBox1.Controls.Add(Me.Label7)
        Me.groupBox1.Location = New System.Drawing.Point(85, 63)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(332, 101)
        Me.groupBox1.TabIndex = 360
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Datos Generales:"
        '
        'comboBox1
        '
        Me.comboBox1.FormattingEnabled = True
        Me.comboBox1.Items.AddRange(New Object() {"Sys_Expert", "Solium", "Otra1", "Otra 2"})
        Me.comboBox1.Location = New System.Drawing.Point(96, 63)
        Me.comboBox1.Name = "comboBox1"
        Me.comboBox1.Size = New System.Drawing.Size(136, 21)
        Me.comboBox1.TabIndex = 6
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(7, 66)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(83, 13)
        Me.label3.TabIndex = 5
        Me.label3.Text = "Base de Datos: "
        '
        'button1
        '
        Me.button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.button1.Location = New System.Drawing.Point(238, 38)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(48, 23)
        Me.button1.TabIndex = 4
        Me.button1.Text = "..."
        Me.button1.UseVisualStyleBackColor = True
        '
        'textBox2
        '
        Me.textBox2.Location = New System.Drawing.Point(49, 41)
        Me.textBox2.Name = "textBox2"
        Me.textBox2.Size = New System.Drawing.Size(183, 20)
        Me.textBox2.TabIndex = 3
        Me.textBox2.Text = "C:\Reportes\Templates\Ventas.xlsx"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(7, 44)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(36, 13)
        Me.label2.TabIndex = 2
        Me.label2.Text = "Ruta: "
        '
        'textBox1
        '
        Me.textBox1.Location = New System.Drawing.Point(63, 17)
        Me.textBox1.Name = "textBox1"
        Me.textBox1.Size = New System.Drawing.Size(169, 20)
        Me.textBox1.TabIndex = 1
        Me.textBox1.Text = "Reporte de Ventas Mensual"
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
        'frm000Plantillas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(556, 514)
        Me.Controls.Add(Me.groupBox3)
        Me.Controls.Add(Me.groupBox2)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.ControlPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MinimizeBox = False
        Me.Name = "frm000Plantillas"
        Me.Text = "Gestor"
        Me.Controls.SetChildIndex(Me.LblMensaje, 0)
        Me.Controls.SetChildIndex(Me.ControlPanel, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.LblAccion, 0)
        Me.Controls.SetChildIndex(Me.groupBox1, 0)
        Me.Controls.SetChildIndex(Me.groupBox2, 0)
        Me.Controls.SetChildIndex(Me.groupBox3, 0)
        Me.ControlPanel.ResumeLayout(False)
        Me.ControlPanel.PerformLayout()
        Me.groupBox3.ResumeLayout(False)
        Me.groupBox3.PerformLayout()
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ControlPanel As System.Windows.Forms.Panel
    Friend WithEvents lblVersionModulo As System.Windows.Forms.Label
    Private WithEvents groupBox3 As GroupBox
    Private WithEvents Label1 As Label
    Private WithEvents button6 As Button
    Private WithEvents label5 As Label
    Private WithEvents button3 As Button
    Private WithEvents groupBox2 As GroupBox
    Private WithEvents richTextBox1 As RichTextBox
    Private WithEvents groupBox1 As GroupBox
    Private WithEvents comboBox1 As ComboBox
    Private WithEvents label3 As Label
    Private WithEvents button1 As Button
    Private WithEvents textBox2 As TextBox
    Private WithEvents label2 As Label
    Private WithEvents textBox1 As TextBox
    Private WithEvents Label7 As Label
    Private WithEvents lbConteoParametros As Label
    Private WithEvents lbConteoColumnas As Label
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm000Parametro
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TbOrden = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TbValorDefault = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.checkbRequerido = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbEtiqueta = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbTipo = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbNombre = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ControlPanel.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        Me.Label6.Size = New System.Drawing.Size(292, 31)
        Me.Label6.Text = "{Configurar Parametro}"
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TbOrden)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.TbValorDefault)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.checkbRequerido)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.tbEtiqueta)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cbTipo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.tbNombre)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(26, 64)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(302, 226)
        Me.GroupBox1.TabIndex = 360
        Me.GroupBox1.TabStop = False
        '
        'TbOrden
        '
        Me.TbOrden.Location = New System.Drawing.Point(85, 175)
        Me.TbOrden.Name = "TbOrden"
        Me.TbOrden.Size = New System.Drawing.Size(100, 20)
        Me.TbOrden.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 178)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(42, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Orden: "
        '
        'TbValorDefault
        '
        Me.TbValorDefault.Location = New System.Drawing.Point(85, 143)
        Me.TbValorDefault.Name = "TbValorDefault"
        Me.TbValorDefault.Size = New System.Drawing.Size(100, 20)
        Me.TbValorDefault.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 146)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Valor Default: "
        '
        'checkbRequerido
        '
        Me.checkbRequerido.AutoSize = True
        Me.checkbRequerido.Location = New System.Drawing.Point(85, 116)
        Me.checkbRequerido.Name = "checkbRequerido"
        Me.checkbRequerido.Size = New System.Drawing.Size(15, 14)
        Me.checkbRequerido.TabIndex = 7
        Me.checkbRequerido.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Requerido: "
        '
        'tbEtiqueta
        '
        Me.tbEtiqueta.Location = New System.Drawing.Point(85, 85)
        Me.tbEtiqueta.Name = "tbEtiqueta"
        Me.tbEtiqueta.Size = New System.Drawing.Size(100, 20)
        Me.tbEtiqueta.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Etiqueta: "
        '
        'cbTipo
        '
        Me.cbTipo.FormattingEnabled = True
        Me.cbTipo.Items.AddRange(New Object() {"String", "Int", "Date", "Decimal"})
        Me.cbTipo.Location = New System.Drawing.Point(85, 54)
        Me.cbTipo.Name = "cbTipo"
        Me.cbTipo.Size = New System.Drawing.Size(121, 21)
        Me.cbTipo.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Tipo: "
        '
        'tbNombre
        '
        Me.tbNombre.Location = New System.Drawing.Point(85, 24)
        Me.tbNombre.Name = "tbNombre"
        Me.tbNombre.Size = New System.Drawing.Size(100, 20)
        Me.tbNombre.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nombre: "
        '
        'frm000Parametro
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(556, 514)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ControlPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MinimizeBox = False
        Me.Name = "frm000Parametro"
        Me.Text = "Gestor"
        Me.Controls.SetChildIndex(Me.LblMensaje, 0)
        Me.Controls.SetChildIndex(Me.ControlPanel, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.LblAccion, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.ControlPanel.ResumeLayout(False)
        Me.ControlPanel.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ControlPanel As System.Windows.Forms.Panel
    Friend WithEvents lblVersionModulo As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cbTipo As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents tbNombre As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TbOrden As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TbValorDefault As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents checkbRequerido As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents tbEtiqueta As TextBox
    Friend WithEvents Label3 As Label
End Class

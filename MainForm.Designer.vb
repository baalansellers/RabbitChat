<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.btSend = New System.Windows.Forms.Button()
        Me.tbChat = New System.Windows.Forms.TextBox()
        Me.tbMessage = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btSend
        '
        Me.btSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btSend.Location = New System.Drawing.Point(373, 309)
        Me.btSend.Name = "btSend"
        Me.btSend.Size = New System.Drawing.Size(45, 47)
        Me.btSend.TabIndex = 1
        Me.btSend.Text = "Send"
        Me.btSend.UseVisualStyleBackColor = True
        '
        'tbChat
        '
        Me.tbChat.Location = New System.Drawing.Point(12, 12)
        Me.tbChat.Multiline = True
        Me.tbChat.Name = "tbChat"
        Me.tbChat.ReadOnly = True
        Me.tbChat.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbChat.Size = New System.Drawing.Size(406, 291)
        Me.tbChat.TabIndex = 3
        '
        'tbMessage
        '
        Me.tbMessage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbMessage.Location = New System.Drawing.Point(12, 309)
        Me.tbMessage.Multiline = True
        Me.tbMessage.Name = "tbMessage"
        Me.tbMessage.Size = New System.Drawing.Size(355, 47)
        Me.tbMessage.TabIndex = 0
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(430, 368)
        Me.Controls.Add(Me.tbMessage)
        Me.Controls.Add(Me.btSend)
        Me.Controls.Add(Me.tbChat)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(400, 400)
        Me.Name = "MainForm"
        Me.Text = "RabbitChat"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btSend As Button
    Friend WithEvents tbChat As TextBox
    Friend WithEvents tbMessage As TextBox
End Class

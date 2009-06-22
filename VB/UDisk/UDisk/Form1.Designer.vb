<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.NotifyIconAutoRun = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Menu_Enable = New System.Windows.Forms.ToolStripMenuItem
        Me.Menu_Exit = New System.Windows.Forms.ToolStripMenuItem
        Me.CheckBoxAddFile = New System.Windows.Forms.CheckBox
        Me.ButtonExit = New System.Windows.Forms.Button
        Me.ContextMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'NotifyIconAutoRun
        '
        Me.NotifyIconAutoRun.ContextMenuStrip = Me.ContextMenuStrip
        Me.NotifyIconAutoRun.Icon = CType(resources.GetObject("NotifyIconAutoRun.Icon"), System.Drawing.Icon)
        Me.NotifyIconAutoRun.Text = "DOSSTONED_AutoRun"
        Me.NotifyIconAutoRun.Visible = True
        '
        'ContextMenuStrip
        '
        Me.ContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Menu_Enable, Me.Menu_Exit})
        Me.ContextMenuStrip.Name = "ContextMenuStrip"
        Me.ContextMenuStrip.Size = New System.Drawing.Size(173, 48)
        '
        'Menu_Enable
        '
        Me.Menu_Enable.CheckOnClick = True
        Me.Menu_Enable.Name = "Menu_Enable"
        Me.Menu_Enable.Size = New System.Drawing.Size(172, 22)
        Me.Menu_Enable.Text = "Enable Protection"
        '
        'Menu_Exit
        '
        Me.Menu_Exit.Name = "Menu_Exit"
        Me.Menu_Exit.Size = New System.Drawing.Size(172, 22)
        Me.Menu_Exit.Text = "Exit(&E)"
        '
        'CheckBoxAddFile
        '
        Me.CheckBoxAddFile.AutoSize = True
        Me.CheckBoxAddFile.Location = New System.Drawing.Point(12, 12)
        Me.CheckBoxAddFile.Name = "CheckBoxAddFile"
        Me.CheckBoxAddFile.Size = New System.Drawing.Size(204, 16)
        Me.CheckBoxAddFile.TabIndex = 1
        Me.CheckBoxAddFile.Text = "添加DOSSTONED认证的AutoRun文件"
        Me.CheckBoxAddFile.UseVisualStyleBackColor = True
        '
        'ButtonExit
        '
        Me.ButtonExit.Location = New System.Drawing.Point(205, 231)
        Me.ButtonExit.Name = "ButtonExit"
        Me.ButtonExit.Size = New System.Drawing.Size(75, 23)
        Me.ButtonExit.TabIndex = 2
        Me.ButtonExit.Text = "退出（&Q）"
        Me.ButtonExit.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.CheckBoxAddFile)
        Me.Controls.Add(Me.ButtonExit)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.Text = "AutoRun 保护器"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.ContextMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents NotifyIconAutoRun As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Menu_Enable As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_Exit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheckBoxAddFile As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonExit As System.Windows.Forms.Button

End Class

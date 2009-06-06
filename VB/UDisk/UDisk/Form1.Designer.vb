<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.NotifyIconAutoRun = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Menu_Enable = New System.Windows.Forms.ToolStripMenuItem
        Me.Menu_Exit = New System.Windows.Forms.ToolStripMenuItem
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
        Me.ContextMenuStrip.Size = New System.Drawing.Size(173, 70)
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
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ContextMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents NotifyIconAutoRun As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Menu_Enable As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_Exit As System.Windows.Forms.ToolStripMenuItem

End Class

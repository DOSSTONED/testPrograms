<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FlashDiskProtector
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FlashDiskProtector))
        Me.NotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripProtect = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripRepair = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripExit = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusProtect = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.StatusRepair = New System.Windows.Forms.ToolStripStatusLabel
        Me.StatusAuthorised = New System.Windows.Forms.ToolStripStatusLabel
        Me.BtnExit = New System.Windows.Forms.Button
        Me.CheckBoxProtect = New System.Windows.Forms.CheckBox
        Me.CheckBoxRepair = New System.Windows.Forms.CheckBox
        Me.CheckBoxAuthorise = New System.Windows.Forms.CheckBox
        Me.BtnAbout = New System.Windows.Forms.Button
        Me.ListBox = New System.Windows.Forms.ListBox
        Me.ContextMenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'NotifyIcon
        '
        Me.NotifyIcon.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon.Icon = CType(resources.GetObject("NotifyIcon.Icon"), System.Drawing.Icon)
        Me.NotifyIcon.Text = "FlashDisk Protector"
        Me.NotifyIcon.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProtect, Me.ToolStripRepair, Me.ToolStripExit})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(193, 70)
        '
        'ToolStripProtect
        '
        Me.ToolStripProtect.Name = "ToolStripProtect"
        Me.ToolStripProtect.Size = New System.Drawing.Size(192, 22)
        Me.ToolStripProtect.Text = "Start Protection"
        '
        'ToolStripRepair
        '
        Me.ToolStripRepair.Name = "ToolStripRepair"
        Me.ToolStripRepair.Size = New System.Drawing.Size(192, 22)
        Me.ToolStripRepair.Text = "Repair infected folders"
        '
        'ToolStripExit
        '
        Me.ToolStripExit.Name = "ToolStripExit"
        Me.ToolStripExit.Size = New System.Drawing.Size(192, 22)
        Me.ToolStripExit.Text = "Exit(&X)"
        '
        'StatusProtect
        '
        Me.StatusProtect.Name = "StatusProtect"
        Me.StatusProtect.Size = New System.Drawing.Size(113, 17)
        Me.StatusProtect.Text = "Protection: Disabled"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusProtect, Me.StatusRepair, Me.StatusAuthorised})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 93)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(437, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 0
        '
        'StatusRepair
        '
        Me.StatusRepair.Name = "StatusRepair"
        Me.StatusRepair.Size = New System.Drawing.Size(91, 17)
        Me.StatusRepair.Text = "Repair: Disabled"
        '
        'StatusAuthorised
        '
        Me.StatusAuthorised.Name = "StatusAuthorised"
        Me.StatusAuthorised.Size = New System.Drawing.Size(109, 17)
        Me.StatusAuthorised.Text = "Authorise: Disabled"
        '
        'BtnExit
        '
        Me.BtnExit.Location = New System.Drawing.Point(350, 67)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(75, 23)
        Me.BtnExit.TabIndex = 2
        Me.BtnExit.Text = "Exit(&X)"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'CheckBoxProtect
        '
        Me.CheckBoxProtect.AutoSize = True
        Me.CheckBoxProtect.Location = New System.Drawing.Point(12, 12)
        Me.CheckBoxProtect.Name = "CheckBoxProtect"
        Me.CheckBoxProtect.Size = New System.Drawing.Size(110, 17)
        Me.CheckBoxProtect.TabIndex = 3
        Me.CheckBoxProtect.Text = "Enable Protection"
        Me.CheckBoxProtect.UseVisualStyleBackColor = True
        '
        'CheckBoxRepair
        '
        Me.CheckBoxRepair.AutoSize = True
        Me.CheckBoxRepair.Location = New System.Drawing.Point(12, 35)
        Me.CheckBoxRepair.Name = "CheckBoxRepair"
        Me.CheckBoxRepair.Size = New System.Drawing.Size(132, 17)
        Me.CheckBoxRepair.TabIndex = 4
        Me.CheckBoxRepair.Text = "Repair infected folders"
        Me.CheckBoxRepair.UseVisualStyleBackColor = True
        '
        'CheckBoxAuthorise
        '
        Me.CheckBoxAuthorise.AutoSize = True
        Me.CheckBoxAuthorise.Location = New System.Drawing.Point(12, 58)
        Me.CheckBoxAuthorise.Name = "CheckBoxAuthorise"
        Me.CheckBoxAuthorise.Size = New System.Drawing.Size(103, 17)
        Me.CheckBoxAuthorise.TabIndex = 5
        Me.CheckBoxAuthorise.Text = "Authorise Drives"
        Me.CheckBoxAuthorise.UseVisualStyleBackColor = True
        '
        'BtnAbout
        '
        Me.BtnAbout.Location = New System.Drawing.Point(350, 12)
        Me.BtnAbout.Name = "BtnAbout"
        Me.BtnAbout.Size = New System.Drawing.Size(75, 23)
        Me.BtnAbout.TabIndex = 6
        Me.BtnAbout.Text = "About(&A)"
        Me.BtnAbout.UseVisualStyleBackColor = True
        '
        'ListBox
        '
        Me.ListBox.FormattingEnabled = True
        Me.ListBox.Items.AddRange(New Object() {"Exception:"})
        Me.ListBox.Location = New System.Drawing.Point(224, 8)
        Me.ListBox.Name = "ListBox"
        Me.ListBox.Size = New System.Drawing.Size(120, 82)
        Me.ListBox.TabIndex = 7
        '
        'FlashDiskProtector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(437, 115)
        Me.Controls.Add(Me.ListBox)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.BtnAbout)
        Me.Controls.Add(Me.CheckBoxAuthorise)
        Me.Controls.Add(Me.CheckBoxRepair)
        Me.Controls.Add(Me.CheckBoxProtect)
        Me.Controls.Add(Me.BtnExit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FlashDiskProtector"
        Me.Text = "FlashDisk Protector"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents NotifyIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripRepair As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripProtect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusProtect As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents StatusRepair As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents BtnExit As System.Windows.Forms.Button
    Friend WithEvents CheckBoxProtect As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxRepair As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxAuthorise As System.Windows.Forms.CheckBox
    Friend WithEvents BtnAbout As System.Windows.Forms.Button
    Friend WithEvents StatusAuthorised As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ListBox As System.Windows.Forms.ListBox

End Class

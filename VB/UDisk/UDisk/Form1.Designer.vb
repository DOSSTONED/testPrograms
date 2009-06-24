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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.ButtonExit = New System.Windows.Forms.Button
        Me.TrackBar = New System.Windows.Forms.TrackBar
        Me.CheckBoxEnabled = New System.Windows.Forms.CheckBox
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusEnable = New System.Windows.Forms.ToolStripStatusLabel
        Me.CheckBoxClean1 = New System.Windows.Forms.CheckBox
        CType(Me.TrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonExit
        '
        Me.ButtonExit.Location = New System.Drawing.Point(205, 218)
        Me.ButtonExit.Name = "ButtonExit"
        Me.ButtonExit.Size = New System.Drawing.Size(75, 23)
        Me.ButtonExit.TabIndex = 0
        Me.ButtonExit.Text = "退出（&Q）"
        Me.ButtonExit.UseVisualStyleBackColor = True
        '
        'TrackBar
        '
        Me.TrackBar.LargeChange = 10
        Me.TrackBar.Location = New System.Drawing.Point(12, 167)
        Me.TrackBar.Maximum = 100
        Me.TrackBar.Minimum = 10
        Me.TrackBar.Name = "TrackBar"
        Me.TrackBar.Size = New System.Drawing.Size(268, 45)
        Me.TrackBar.TabIndex = 1
        Me.TrackBar.TickFrequency = 2
        Me.TrackBar.Value = 100
        '
        'CheckBoxEnabled
        '
        Me.CheckBoxEnabled.AutoSize = True
        Me.CheckBoxEnabled.Location = New System.Drawing.Point(12, 12)
        Me.CheckBoxEnabled.Name = "CheckBoxEnabled"
        Me.CheckBoxEnabled.Size = New System.Drawing.Size(126, 16)
        Me.CheckBoxEnabled.TabIndex = 2
        Me.CheckBoxEnabled.Text = "Enable Protection"
        Me.CheckBoxEnabled.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusEnable})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 244)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(292, 22)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusEnable
        '
        Me.ToolStripStatusEnable.Name = "ToolStripStatusEnable"
        Me.ToolStripStatusEnable.Size = New System.Drawing.Size(53, 17)
        Me.ToolStripStatusEnable.Text = "Disabled"
        '
        'CheckBoxClean1
        '
        Me.CheckBoxClean1.AutoSize = True
        Me.CheckBoxClean1.Location = New System.Drawing.Point(12, 34)
        Me.CheckBoxClean1.Name = "CheckBoxClean1"
        Me.CheckBoxClean1.Size = New System.Drawing.Size(138, 16)
        Me.CheckBoxClean1.TabIndex = 2
        Me.CheckBoxClean1.Text = "Recover Directories"
        Me.CheckBoxClean1.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.CheckBoxClean1)
        Me.Controls.Add(Me.CheckBoxEnabled)
        Me.Controls.Add(Me.TrackBar)
        Me.Controls.Add(Me.ButtonExit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.Text = "MainForm"
        CType(Me.TrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonExit As System.Windows.Forms.Button
    Friend WithEvents TrackBar As System.Windows.Forms.TrackBar
    Friend WithEvents CheckBoxEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusEnable As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents CheckBoxClean1 As System.Windows.Forms.CheckBox

End Class

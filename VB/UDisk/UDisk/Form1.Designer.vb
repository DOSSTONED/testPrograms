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
        Me.Label = New System.Windows.Forms.Label
        Me.ListBox = New System.Windows.Forms.ListBox
        Me.Label_Events = New System.Windows.Forms.Label
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
        Me.TrackBar.Location = New System.Drawing.Point(12, 196)
        Me.TrackBar.Maximum = 100
        Me.TrackBar.Minimum = 20
        Me.TrackBar.Name = "TrackBar"
        Me.TrackBar.Size = New System.Drawing.Size(187, 45)
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
        'Label
        '
        Me.Label.AutoSize = True
        Me.Label.Location = New System.Drawing.Point(10, 181)
        Me.Label.Name = "Label"
        Me.Label.Size = New System.Drawing.Size(137, 12)
        Me.Label.TabIndex = 4
        Me.Label.Text = "Transparent:(20%-100%)"
        '
        'ListBox
        '
        Me.ListBox.FormattingEnabled = True
        Me.ListBox.ItemHeight = 12
        Me.ListBox.Location = New System.Drawing.Point(160, 60)
        Me.ListBox.Name = "ListBox"
        Me.ListBox.Size = New System.Drawing.Size(120, 112)
        Me.ListBox.TabIndex = 5
        '
        'Label_Events
        '
        Me.Label_Events.AutoSize = True
        Me.Label_Events.Location = New System.Drawing.Point(158, 45)
        Me.Label_Events.Name = "Label_Events"
        Me.Label_Events.Size = New System.Drawing.Size(47, 12)
        Me.Label_Events.TabIndex = 4
        Me.Label_Events.Text = "Events:"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.ListBox)
        Me.Controls.Add(Me.Label_Events)
        Me.Controls.Add(Me.Label)
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
    Friend WithEvents Label As System.Windows.Forms.Label
    Friend WithEvents ListBox As System.Windows.Forms.ListBox
    Friend WithEvents Label_Events As System.Windows.Forms.Label

End Class

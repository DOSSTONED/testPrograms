<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MSTS_STARTER
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MSTS_STARTER))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.Button7 = New System.Windows.Forms.Button
        Me.AdvBtn1 = New System.Windows.Forms.Button
        Me.AdvBtn2 = New System.Windows.Forms.Button
        Me.AdvBtn3 = New System.Windows.Forms.Button
        Me.AdvBtn4 = New System.Windows.Forms.Button
        Me.AdvBtn5 = New System.Windows.Forms.Button
        Me.AdvBtn6 = New System.Windows.Forms.Button
        Me.AdvBtn7 = New System.Windows.Forms.Button
        Me.AdvBtn8 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(7, 0, 7, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "请选择启动选项："
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 36)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(229, 58)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "全屏启动"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(249, 36)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(229, 58)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "窗口启动"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(12, 100)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(229, 58)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "时间加速"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(247, 100)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(229, 58)
        Me.Button4.TabIndex = 2
        Me.Button4.Text = "时间加速及窗口模式"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(12, 164)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(229, 58)
        Me.Button5.TabIndex = 2
        Me.Button5.Text = "打开所在文件夹"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(247, 187)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(111, 35)
        Me.Button6.TabIndex = 3
        Me.Button6.Text = "高级选项"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(365, 187)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(111, 35)
        Me.Button7.TabIndex = 4
        Me.Button7.Text = "退出"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'AdvBtn1
        '
        Me.AdvBtn1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdvBtn1.Location = New System.Drawing.Point(12, 244)
        Me.AdvBtn1.Name = "AdvBtn1"
        Me.AdvBtn1.Size = New System.Drawing.Size(176, 23)
        Me.AdvBtn1.TabIndex = 5
        Me.AdvBtn1.Text = "游戏编辑器"
        Me.AdvBtn1.UseVisualStyleBackColor = True
        '
        'AdvBtn2
        '
        Me.AdvBtn2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdvBtn2.Location = New System.Drawing.Point(12, 273)
        Me.AdvBtn2.Name = "AdvBtn2"
        Me.AdvBtn2.Size = New System.Drawing.Size(176, 23)
        Me.AdvBtn2.TabIndex = 5
        Me.AdvBtn2.Text = "Route Control"
        Me.AdvBtn2.UseVisualStyleBackColor = True
        '
        'AdvBtn3
        '
        Me.AdvBtn3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdvBtn3.Location = New System.Drawing.Point(12, 302)
        Me.AdvBtn3.Name = "AdvBtn3"
        Me.AdvBtn3.Size = New System.Drawing.Size(176, 23)
        Me.AdvBtn3.TabIndex = 5
        Me.AdvBtn3.Text = "防止驾驶室出现白线"
        Me.AdvBtn3.UseVisualStyleBackColor = True
        '
        'AdvBtn4
        '
        Me.AdvBtn4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdvBtn4.Location = New System.Drawing.Point(12, 331)
        Me.AdvBtn4.Name = "AdvBtn4"
        Me.AdvBtn4.Size = New System.Drawing.Size(176, 23)
        Me.AdvBtn4.TabIndex = 5
        Me.AdvBtn4.Text = "各向异性过滤"
        Me.AdvBtn4.UseVisualStyleBackColor = True
        '
        'AdvBtn5
        '
        Me.AdvBtn5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdvBtn5.Location = New System.Drawing.Point(194, 244)
        Me.AdvBtn5.Name = "AdvBtn5"
        Me.AdvBtn5.Size = New System.Drawing.Size(176, 23)
        Me.AdvBtn5.TabIndex = 5
        Me.AdvBtn5.Text = "全屏抗锯齿"
        Me.AdvBtn5.UseVisualStyleBackColor = True
        '
        'AdvBtn6
        '
        Me.AdvBtn6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdvBtn6.Location = New System.Drawing.Point(194, 273)
        Me.AdvBtn6.Name = "AdvBtn6"
        Me.AdvBtn6.Size = New System.Drawing.Size(176, 23)
        Me.AdvBtn6.TabIndex = 5
        Me.AdvBtn6.Text = "疑难解答(Troubleshootting)"
        Me.AdvBtn6.UseVisualStyleBackColor = True
        '
        'AdvBtn7
        '
        Me.AdvBtn7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdvBtn7.Location = New System.Drawing.Point(194, 302)
        Me.AdvBtn7.Name = "AdvBtn7"
        Me.AdvBtn7.Size = New System.Drawing.Size(176, 23)
        Me.AdvBtn7.TabIndex = 5
        Me.AdvBtn7.Text = "(Empty)"
        Me.AdvBtn7.UseVisualStyleBackColor = True
        '
        'AdvBtn8
        '
        Me.AdvBtn8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdvBtn8.Location = New System.Drawing.Point(194, 331)
        Me.AdvBtn8.Name = "AdvBtn8"
        Me.AdvBtn8.Size = New System.Drawing.Size(176, 23)
        Me.AdvBtn8.TabIndex = 5
        Me.AdvBtn8.Text = "(Empty)"
        Me.AdvBtn8.UseVisualStyleBackColor = True
        '
        'MSTS_STARTER
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(490, 366)
        Me.Controls.Add(Me.AdvBtn8)
        Me.Controls.Add(Me.AdvBtn6)
        Me.Controls.Add(Me.AdvBtn5)
        Me.Controls.Add(Me.AdvBtn4)
        Me.Controls.Add(Me.AdvBtn7)
        Me.Controls.Add(Me.AdvBtn3)
        Me.Controls.Add(Me.AdvBtn2)
        Me.Controls.Add(Me.AdvBtn1)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MSTS_STARTER"
        Me.Text = "模拟火车启动"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents AdvBtn1 As System.Windows.Forms.Button
    Friend WithEvents AdvBtn2 As System.Windows.Forms.Button
    Friend WithEvents AdvBtn3 As System.Windows.Forms.Button
    Friend WithEvents AdvBtn4 As System.Windows.Forms.Button
    Friend WithEvents AdvBtn5 As System.Windows.Forms.Button
    Friend WithEvents AdvBtn6 As System.Windows.Forms.Button
    Friend WithEvents AdvBtn7 As System.Windows.Forms.Button
    Friend WithEvents AdvBtn8 As System.Windows.Forms.Button

End Class

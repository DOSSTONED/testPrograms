Public Class MSTS_STARTER

    Protected CurD As String

    Private Sub MSTS_STARTER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CurD = My.Computer.FileSystem.CurrentDirectory.ToString() + "\"

        If My.Computer.FileSystem.FileExists(CurD + "train.exe") And My.Computer.FileSystem.FileExists(CurD + "launcher.exe") Then
            Me.Height = 262

        Else
            MsgBox("未找到模拟火车主程序，本程序退出", MsgBoxStyle.Information, "模拟火车启动")
            End
        End If


        If Not My.Computer.FileSystem.FileExists(CurD + "Train Station\Route Control\RouteControl.exe") Then
            AdvBtn2.Enabled = False

        End If

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Application.Exit()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click

        If Button6.Text = "高级选项" Then
            Button6.Text = "基本选项"
            Me.Height = 394
        Else
            Button6.Text = "高级选项"
            Me.Height = 262
        End If


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Shell(CurD + "launcher.exe -rungame")
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Shell(CurD + "train.exe -vm:w")
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Shell(CurD + "train.exe /timeacceleration")
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Shell(CurD + "train.exe -vm:w /timeacceleration")
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Shell("explorer .")
    End Sub

    Private Sub AdvBtn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvBtn1.Click
        Shell(CurD + "launcher.exe -runeditor")
    End Sub

    Private Sub AdvBtn2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvBtn2.Click
        Shell(CurD + "Train Station\Route Control\RouteControl.exe")
    End Sub

    Private Sub AdvBtn3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvBtn3.Click
        Shell(CurD + "train.exe /nofiltercab")
    End Sub

    Private Sub AdvBtn4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvBtn4.Click
        Shell(CurD + "train.exe /anisotropic")
    End Sub

    Private Sub AdvBtn5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvBtn5.Click
        Shell(CurD + "train.exe /fsaa")
    End Sub

    Private Sub AdvBtn6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvBtn6.Click
        Shell(CurD + "launcher.exe")
    End Sub
End Class

'echo   │  1、正常启动                   │

'echo   │  2、时间加速                   │

'echo   │  3、窗口模式                   │

'echo   │  4、检查是否缺车               │

'echo   │  5、编辑器及工具               │

'echo   │  6、时间加速+窗口模式          │

'echo   │  7、打开模拟火车所在文件夹     │

'echo   │  8、疑难解答(Troubleshootting) │

'echo   │  9、防止驾驶室出现白线         │

'echo   │  A、各向异性过滤               │

'echo   │  B、全屏抗锯齿                 │

'echo   │  0、退 出                      │
'echo   ╰────────────────╯
'set /p choice=  请选择:
'if "%choice%"=="1" start launcher.exe -rungame
'if "%choice%"=="2" start train.exe /timeacceleration
'if "%choice%"=="3" start train.exe -vm:w
'if "%choice%"=="4" "Train Station\Route Control\RouteControl.exe"
'if "%choice%"=="5" start launcher.exe -runeditor
'if "%choice%"=="6" start train.exe -vm:w /timeacceleration
'if "%choice%"=="7" explorer .
'if "%choice%"=="8" start launcher.exe
'if "%choice%"=="9" start train.exe /nofiltercab
'if "%choice%"=="A" start train.exe /anisotropic
'if "%choice%"=="B" start train.exe /fsaa
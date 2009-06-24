Public Class MainForm

    Public Const WM_DEVICECHANGE = &H219
    Public Const DBT_DEVICEARRIVAL = &H8000
    Public Const DBT_CONFIGCHANGECANCELED = &H19
    Public Const DBT_CONFIGCHANGED = &H18
    Public Const DBT_CUSTOMEVENT = &H8006
    Public Const DBT_DEVICEQUERYREMOVE = &H8001
    Public Const DBT_DEVICEQUERYREMOVEFAILED = &H8002
    Public Const DBT_DEVICEREMOVECOMPLETE = &H8004
    Public Const DBT_DEVICEREMOVEPENDING = &H8003
    Public Const DBT_DEVICETYPESPECIFIC = &H8005
    Public Const DBT_DEVNODES_CHANGED = &H7
    Public Const DBT_QUERYCHANGECONFIG = &H17
    Public Const DBT_USERDEFINED = &HFFFF
    'Public ProtectUDisk As Boolean = 0 ' 1 for protected , 0 for not protected

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_DEVICECHANGE Then
            Select Case m.WParam
                Case WM_DEVICECHANGE
                Case DBT_DEVICEARRIVAL 'U盘插入
                    'If ProtectUDisk <> 0 Then
                    ListBox.Items.Add("UDisk Inserted")
                    Try
                        Dim AllDrives() As IO.DriveInfo = IO.DriveInfo.GetDrives()
                        Dim RemoveableDevices As IO.DriveInfo
                        Dim AutorunFiles As String

                        For Each RemoveableDevices In AllDrives
                            If RemoveableDevices.IsReady = True And RemoveableDevices.DriveType = IO.DriveType.Removable And CheckBoxEnabled.Checked = True Then
                                'MsgBox(RemoveableDevices.ToString()-":\"+"盘已插入")

                                AutorunFiles = RemoveableDevices.ToString + "Autorun.inf"

                                ''''''''''''''''''''''''
                                'Using fs As New System.IO.FileStream(AutorunFiles, IO.FileMode.Open)
                                'Using md5 As System.Security.Cryptography.MD5 = System.Security.Cryptography.MD5.Create()
                                'Dim hash As Byte() = md5.ComputeHash(fs)
                                'NotifyIconAutoRun.ShowBalloonTip(300, "Hash", BitConverter.ToString(hash), ToolTipIcon.Info)
                                'MsgBox(BitConverter.ToString(hash))
                                'End Using
                                'End Using
                                '''''''''''''''''''''''''''
                                'Dim FileReader As System.IO.StreamReader

                                'FileReader = My.Computer.FileSystem.OpenTextFileReader(AutorunFiles)
                                'Dim FirstString As String = FileReader.ReadLine()
                                If My.Computer.FileSystem.FileExists(AutorunFiles) Then
                                    '    FileReader.Close()
                                    My.Computer.FileSystem.DeleteFile(AutorunFiles, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                                    'NotifyIconAutoRun.ShowBalloonTip(300, "发现威胁", "文件 " + AutorunFiles + " 已删除", ToolTipIcon.Warning)
                                    'ElseIf FirstString = ";DOSSTONED Authorised" Then
                                    '    NotifyIconAutoRun.ShowBalloonTip(300, "U盘安全", RemoveableDevices.ToString.Remove(1, 2) + "盘已经通过DOSSTONED认证，可放心使用", ToolTipIcon.Info)
                                End If
                                'FileReader.Close()

                                If My.Computer.FileSystem.DirectoryExists(AutorunFiles) Then
                                    My.Computer.FileSystem.DeleteDirectory(AutorunFiles, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                                    ' NotifyIconAutoRun.ShowBalloonTip(300, "发现异常", "目录 " + AutorunFiles + " 已删除", ToolTipIcon.Warning)
                                End If
                            End If


                            RecoverDir(CheckBoxClean1.Checked, RemoveableDevices)

                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            'If CheckBoxAddFile.Checked = True Then
                            'Dim FileReader As System.IO.FileStream = System.IO.File.Create(AutorunFiles)
                            'FileReader.Write()
                            'Dim Files() As String = System.IO.Directory.GetFiles(RemoveableDevices.ToString, "*.DOSSTONED")
                            'Dim CurrentFile As String
                            'For Each CurrentFile In Files
                            'My.Computer.FileSystem.CopyFile(CurrentFile, AutorunFiles)
                            'Next

                            'End If
                            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                        Next
                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    End Try
                    ' End If
                Case DBT_CONFIGCHANGECANCELED
                Case DBT_CONFIGCHANGED
                Case DBT_CUSTOMEVENT
                Case DBT_DEVICEQUERYREMOVE
                Case DBT_DEVICEQUERYREMOVEFAILED
                Case DBT_DEVICEREMOVECOMPLETE 'U盘卸载
                    'NotifyIconAutoRun.ShowBalloonTip(300, "DOSSTONED", "U盘卸载！", ToolTipIcon.Info)
                Case DBT_DEVICEREMOVEPENDING
                Case DBT_DEVICETYPESPECIFIC
                Case DBT_DEVNODES_CHANGED
                Case DBT_QUERYCHANGECONFIG
                Case DBT_USERDEFINED
            End Select
        End If

        MyBase.WndProc(m)
    End Sub


    Private Sub RecoverDir(ByVal status As Integer, ByVal RemoveableDevices As IO.DriveInfo)

        If status <> 0 Then

            Dim DirectoriesInTop() As String = System.IO.Directory.GetDirectories(RemoveableDevices.ToString)
            Dim CurrentDirectoriesInTop As String
            Dim s As Integer = 0
            For Each CurrentDirectoriesInTop In DirectoriesInTop
                If My.Computer.FileSystem.FileExists(CurrentDirectoriesInTop + ".scr") Then
                    My.Computer.FileSystem.DeleteFile(CurrentDirectoriesInTop + ".scr", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                    s = 1
                End If
                If My.Computer.FileSystem.FileExists(CurrentDirectoriesInTop + ".exe") Then
                    My.Computer.FileSystem.DeleteFile(CurrentDirectoriesInTop + ".exe", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                    s = 1
                End If

                If s = 1 Then
                    SetAttr(CurrentDirectoriesInTop, FileAttribute.Normal)
                    SetAttr(CurrentDirectoriesInTop, FileAttribute.System)
                    ListBox.Items.Add("Directory " + CurrentDirectoriesInTop + "Recovered")
                End If

                'ListBox.Items.Add("Directory " + CurrentDirectoriesInTop + "Detected")
            Next

        End If
    End Sub


    Private Sub ButtonExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExit.Click
        Application.Exit()
    End Sub

    Private Sub TrackBar_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar.Scroll
        Me.Opacity = TrackBar.Value / 100
    End Sub

    Private Sub CheckBoxEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxEnabled.CheckedChanged, CheckBoxClean1.CheckedChanged

        If CheckBoxEnabled.Checked = True Then
            ToolStripStatusEnable.Text = "Enabled"
        Else
            ToolStripStatusEnable.Text = "Disabled"
        End If

    End Sub
End Class

Public Class FlashDiskProtector

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
    Public Const WM_CREATE = &H1
    Public ProtectStatus As Boolean = False
    Public RepairStatus As Boolean = False
    'Public ProtectUDisk As Boolean = 0 ' 1 for protected , 0 for not protected

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_DEVICECHANGE Then
            Select Case m.WParam
                Case WM_DEVICECHANGE
                
                Case DBT_DEVICEARRIVAL 'U盘插入
                    'If ProtectUDisk <> 0 Then
                    ListBox.Items.Add("FlashDisk Inserted")
                    Try
                        Dim AllDrives() As IO.DriveInfo = IO.DriveInfo.GetDrives()
                        Dim RemoveableDevices As IO.DriveInfo
                        Dim AutorunFiles As String

                        For Each RemoveableDevices In AllDrives
                            If RemoveableDevices.DriveType = IO.DriveType.Removable And RemoveableDevices.IsReady = True And ProtectStatus Then   'IO.DriveType.Fixed for harddisk
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



                                RecoverDir(RepairStatus, RemoveableDevices)
                            End If
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

        If m.Msg = WM_CREATE Then
            ListBox.Items.Add("Window Created")
        End If

        MyBase.WndProc(m)
    End Sub

    ''' <summary>
    ''' Here is the function writen by myself
    ''' </summary>
    ''' <param name="status"></param>
    ''' <param name="RemoveableDevices"></param>
    ''' <remarks></remarks>

    Private Sub RecoverDir(ByVal status As Integer, ByVal RemoveableDevices As IO.DriveInfo)

        If status <> 0 Then

            Dim DirectoriesInTop() As String = System.IO.Directory.GetDirectories(RemoveableDevices.ToString)
            Dim CurrentDirectoriesInTop As String
            Dim s As Integer = 0
            For Each CurrentDirectoriesInTop In DirectoriesInTop
                Try
                    If My.Computer.FileSystem.FileExists(CurrentDirectoriesInTop + ".scr") Then
                        My.Computer.FileSystem.DeleteFile(CurrentDirectoriesInTop + ".scr", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                        s = 1
                    End If
                    If My.Computer.FileSystem.FileExists(CurrentDirectoriesInTop + ".exe") Then
                        My.Computer.FileSystem.DeleteFile(CurrentDirectoriesInTop + ".exe", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                        s = 1
                    End If

                    If s = 1 Then

                        'SetAttr(CurrentDirectoriesInTop, FileAttribute.Normal)
                        'SetAttr(CurrentDirectoriesInTop, FileAttribute.System)
                        'ListBox.Items.Add("Directory " + CurrentDirectoriesInTop + "Recovered")
                        '删除Recycled程序


                        If My.Computer.FileSystem.FileExists(CurrentDirectoriesInTop + "Recycled.exe") Then
                            My.Computer.FileSystem.DeleteFile(CurrentDirectoriesInTop + "Recycled.exe", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                        End If

                        If My.Computer.FileSystem.DirectoryExists(CurrentDirectoriesInTop + "$Recycle.bin") Then
                            My.Computer.FileSystem.DeleteDirectory(CurrentDirectoriesInTop + "$Recycle.bin", FileIO.DeleteDirectoryOption.DeleteAllContents, FileIO.RecycleOption.DeletePermanently)
                        End If


                        Dim fs, f
                        fs = CreateObject("Scripting.FileSystemObject")
                        f = fs.GetFolder(CurrentDirectoriesInTop)
                        f.Attributes = 4 '用Attributes函数设置文件夹属性
                    End If

                    'ListBox.Items.Add("Directory " + CurrentDirectoriesInTop + "Detected")
                Catch ex As Exception
                    ListBox.Items.Add(ex.ToString())
                    MsgBox(ex.ToString())
                End Try


            Next

        End If
    End Sub


    Private Sub RunProtect()

        If ProtectStatus Then
            CheckBoxProtect.Checked = False
            ToolStripProtect.Checked = False
            StatusProtect.Text = "Protection: Disabled"
            ProtectStatus = False

        Else
            CheckBoxProtect.Checked = True
            ToolStripProtect.Checked = True
            StatusProtect.Text = "Protection: Enabled"
            ProtectStatus = True
        End If

    End Sub

    Private Sub RunRepair()
        If RepairStatus Then
            CheckBoxRepair.Checked = False
            ToolStripRepair.Checked = False
            StatusRepair.Text = "Repair: Disabled"
            RepairStatus = False
        Else
            CheckBoxRepair.Checked = True
            ToolStripRepair.Checked = True
            StatusRepair.Text = "Repair: Enabled"
            RepairStatus = True
        End If
    End Sub

    ''' <summary>
    ''' End myself function
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>


    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Application.Exit()
    End Sub

    Private Sub BtnAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAbout.Click

        MsgBox("FlashDisk Protector X86_64 Edition" + Chr(13) + Chr(10) + Chr(13) + Chr(10) + "DOSSTONED Authorised.", MsgBoxStyle.Information, "About me")
    End Sub

    Private Sub ToolStripExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripExit.Click
        Application.Exit()
    End Sub

    Private Sub CheckBoxProtect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxProtect.CheckedChanged
        RunProtect()
    End Sub

    Private Sub CheckBoxRepair_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxRepair.CheckedChanged
        RunRepair()
    End Sub

    Private Sub ToolStripProtect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripProtect.Click
        RunProtect()
    End Sub

    Private Sub ToolStripRepair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripRepair.Click
        RunRepair()
    End Sub

    Private Sub StatusProtect_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StatusProtect.DoubleClick

    End Sub

    Private Sub StatusRepair_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles StatusRepair.DoubleClick
        RunRepair()
    End Sub

    Private Sub FlashDiskProtector_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Hide()
        NotifyIcon.ShowBalloonTip(300, "Protector is running", "Protector is running", ToolTipIcon.Info)
    End Sub

    Private Sub NotifyIcon_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon.MouseDoubleClick
        Me.Show()
    End Sub

    Private Sub FlashDiskProtector_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RunProtect()
        RunRepair()
    End Sub
End Class

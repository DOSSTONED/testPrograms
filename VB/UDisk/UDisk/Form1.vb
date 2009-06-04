Imports System.IO

Public Class Form1

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

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_DEVICECHANGE Then
            Select Case m.WParam
                Case WM_DEVICECHANGE
                Case DBT_DEVICEARRIVAL 'U盘插入
                    Try
                        Dim AllDrives() As IO.DriveInfo = IO.DriveInfo.GetDrives()
                        Dim RemoveableDevices As IO.DriveInfo
                        Dim AutorunFiles As String

                        For Each RemoveableDevices In AllDrives
                            If RemoveableDevices.IsReady = True And RemoveableDevices.DriveType = IO.DriveType.Removable Then
                                'MsgBox(RemoveableDevices.ToString()-":\"+"盘已插入")

                                AutorunFiles = RemoveableDevices.ToString + "Autorun.inf"

                                If My.Computer.FileSystem.FileExists(AutorunFiles) Then
                                    My.Computer.FileSystem.DeleteFile(AutorunFiles, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                                    NotifyIconAutoRun.ShowBalloonTip(3000, "发现威胁", "文件 " + AutorunFiles + " 已删除", ToolTipIcon.Warning)

                                End If

                                If My.Computer.FileSystem.DirectoryExists(AutorunFiles) Then
                                    My.Computer.FileSystem.DeleteDirectory(AutorunFiles, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)

                                    'MsgBox("目录 " + AutorunFiles + " 已删除")
                                    NotifyIconAutoRun.ShowBalloonTip(3000, "发现异常", "目录 " + AutorunFiles + " 已删除", ToolTipIcon.Warning)
                                End If

                            End If
                        Next
                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    End Try

                Case DBT_CONFIGCHANGECANCELED
                Case DBT_CONFIGCHANGED
                Case DBT_CUSTOMEVENT
                Case DBT_DEVICEQUERYREMOVE
                Case DBT_DEVICEQUERYREMOVEFAILED
                Case DBT_DEVICEREMOVECOMPLETE 'U盘卸载
                    NotifyIconAutoRun.ShowBalloonTip(300, "DOSSTONED", "U盘卸载！", ToolTipIcon.Info)
                Case DBT_DEVICEREMOVEPENDING
                Case DBT_DEVICETYPESPECIFIC
                Case DBT_DEVNODES_CHANGED
                Case DBT_QUERYCHANGECONFIG
                Case DBT_USERDEFINED
            End Select
        End If

        MyBase.WndProc(m)
    End Sub

End Class


'本文来自CSDN博客，转载请标明出处：http://blog.csdn.net/wzuomin/archive/2007/07/27/1711720.aspx
Public Class Form1
    Public Declare Function inet_addr Lib "wsock32.dll" (ByVal s As String) As Long
    Public Declare Function SendARP Lib "iphlpapi.dll" (ByVal DestIP As Long, ByVal SrcIP As Long, ByVal pMacAddr As Long, ByVal PhyAddrLen As Long) As Long
    Public Declare Sub CopyMemory1 Lib "kernel32" Alias "RtlMoveMemory" (ByVal dst As Any, ByVal src As Any, ByVal bcount As Long)

    Public Function GetRemoteMACAddress(ByVal sRemoteIP As String)
        Dim dwRemoteIP As Long
        Dim pMacAddr As Long
        Dim bpMacAddr() As Byte
        Dim PhyAddrLen As Long
        Dim cnt As Long
        Dim tmp As String
        dwRemoteIP = inet_addr(sRemoteIP)
        If dwRemoteIP <> 0 Then
            PhyAddrLen = 6
            On Error Resume Next
            If SendARP(dwRemoteIP, 0&, pMacAddr, PhyAddrLen) = 0 Then
                If pMacAddr <> 0 And PhyAddrLen <> 0 Then
                    ReDim bpMacAddr(0 To PhyAddrLen - 1)
                CopyMemory1 bpMacAddr(0), pMacAddr, ByVal PhyAddrLen
                    For cnt = 0 To PhyAddrLen - 1
                        If bpMacAddr(cnt) = 0 Then
                            tmp = tmp & "00-"
                        Else
                            If Len(Hex$(bpMacAddr(cnt))) = 1 Then
                                tmp = tmp & "0" & Hex$(bpMacAddr(cnt)) & "-"
                            Else
                                tmp = tmp & Hex$(bpMacAddr(cnt)) & "-"
                            End If
                        End If
                    Next
                    If Len(tmp) > 0 Then
                        GetRemoteMACAddress = Left$(tmp, Len(tmp) - 1)
                    End If
                    Exit Function
                Else
                    GetRemoteMACAddress = False
                End If
            Else
                GetRemoteMACAddress = False
            End If 'SendARP
        Else
            GetRemoteMACAddress = False
        End If 'dwRemoteIP
    End Function

End Class

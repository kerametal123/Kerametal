Imports System.Runtime.InteropServices
'Poslije 15 sekundi
Public Class Idle

    <StructLayout(LayoutKind.Sequential)>
    Private Structure LASTINPUTINFO
        <MarshalAs(UnmanagedType.U4)>
        Public cbSize As Integer
        <MarshalAs(UnmanagedType.U4)>
        Public dwTime As Integer
    End Structure

    <DllImport("user32.dll")>
    Private Shared Function GetLastInputInfo(ByRef plii As LASTINPUTINFO) As Boolean
    End Function

    Private idletime As Integer
    Private lastInputInf As New LASTINPUTINFO()

    Public Function GetIdleTime() As Integer
        idletime = 0
        lastInputInf.cbSize = Marshal.SizeOf(lastInputInf)
        lastInputInf.dwTime = 0

        If GetLastInputInfo(lastInputInf) Then
            idletime = Environment.TickCount - lastInputInf.dwTime
        End If

        If idletime > 0 Then
            Return CType(idletime / 1000, Integer)
        Else
            Return 0
        End If
    End Function

    Public Function returnRow(ByVal int As Integer, ByVal prvi As String, ByVal status As Boolean)

        idletime = 1
        lastInputInf.cbSize = int
        lastInputInf.dwTime = prvi
        If lastInputInf.cbSize = lastInputInf.dwTime Then
            calculateRow(status)
        ElseIf lastInputInf.cbSize > lastInputInf.dwTime Then
            Return False
        ElseIf lastInputInf.cbSize < lastInputInf.dwTime Then
            Return True
        End If
        Return True
    End Function

    Public Function calculateRow(ByVal input As String)
        Return input
    End Function
End Class
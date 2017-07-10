Public Class Session
    Dim mysqli As New MySQLinfo
    Dim mysqlc As New MySQLcompany
    Public Function getSessionInfo(ByVal sessionId As String)
        mysqli.vratiTvrtke()
        Return True
    End Function
End Class

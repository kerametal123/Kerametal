Public Class mpBc
    Dim mysql As New MySQLinfo
    Dim licenca As New Licenciranje
    Dim XMLinfo As New XMLinfo

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub tipkaButtonMPbc_Click(sender As Object, e As RoutedEventArgs) Handles tipkaButtonMPbc.Click
        Globals.logMaker("Klik", sender)
    End Sub
End Class

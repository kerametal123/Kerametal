Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Parameters
Class MainWindow
    Dim sucelje As New Sucelje
    Dim licenciranje As New Licenciranje
    Dim XMLinfo As New XMLinfo
    Dim WMInfos As New WMInfo
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            If Globals.CheckForInternetConnection = True Then
                licenciranje.provjeriLicencuOnline()
            ElseIf Globals.CheckForInternetConnection = False Then
                tileBar.Visibility = Visibility.Hidden
            End If
            biRowValue.Content = Globals.databaseName
        Catch ex As Exception
            MessageBox.Show("err" & ex.Message)
        End Try
    End Sub
    Private Sub TileBarItem_Click(sender As Object, e As EventArgs)
        Globals.logMaker("Glavni izbornik, Maloprodaja", sender)
        Dim form As New mpBc()

        form.ShowDialog()

    End Sub
    Private Sub TileBarItem_Click_1(sender As Object, e As EventArgs)
        MessageBox.Show(Globals.databaseInfo)
    End Sub

    Private Sub TileBarItem_Click_2(sender As Object, e As EventArgs)

    End Sub
End Class

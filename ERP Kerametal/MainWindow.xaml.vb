Imports System.Globalization
Imports System.Threading
Class MainWindow
    Dim sucelje As New Sucelje
    Dim licenciranje As New Licenciranje
    Dim XMLinfo As New XMLinfo
    Dim WMInfos As New WMInfo
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        ' Create a new object, representing the German culture.  
        Dim Culture = CultureInfo.CreateSpecificCulture("hr-HR")

        ' The following line provides localization for the application's user interface. 
        Thread.CurrentThread.CurrentUICulture = Culture

        ' The following line provides localization for data formats. 
        Thread.CurrentThread.CurrentCulture = Culture

        ' Set this culture as the default culture for all threads in this application.  
        ' Note: The following properties are supported in the .NET Framework 4.5+ 
        CultureInfo.DefaultThreadCurrentCulture = Culture
        CultureInfo.DefaultThreadCurrentUICulture = Culture


        ' Note that the above code should
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

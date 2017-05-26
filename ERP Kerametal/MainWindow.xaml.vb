Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Parameters
Imports DevExpress.XtraBars
Imports DevExpress.Xpf.Bars
Imports ERP_Kerametal.MySQLinfo
Imports DevExpress.Xpf.Navigation

Class MainWindow
    Dim sucelje As New Sucelje
    Dim licenciranje As New Licenciranje
    Dim XMLinfo As New XMLinfo
    Dim WMInfos As New WMInfo
    Dim mysql As New MySQLinfo
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            If Globals.CheckForInternetConnection = True Then

                If licenciranje.provjeriLicencuOnline() = True Then
                    pripremiSucelje()
                    sucelje.opcijeMPtipke()
                    sucelje.opcijeVPtipke()
                    sucelje.opcijeUGtipke()
                    sucelje.opcijeFKtipke()
                ElseIf licenciranje.provjeriLicencuOnline() = False Then
                    MessageBox.Show("not ok")
                End If
            ElseIf Globals.CheckForInternetConnection = False Then
                ' maloprodaja.Visibility = Visibility.Hidden
            End If

        Catch ex As Exception
            MessageBox.Show("err" & ex.Message)
        End Try
    End Sub
    Private Sub TileBarItem_Click(sender As Object, e As EventArgs)
        Globals.logMaker("Glavni izbornik, Maloprodaja", sender)
        Dim form As New mpBc()

        form.ShowDialog()

    End Sub
    Public Function pripremiSucelje()
        mysql.infoInstalacije(Globals.cpuid)
        mysql.opcijeMp(Globals.cpuid)
        mysql.opcijeVp(Globals.cpuid)
        mysql.opcijeUg(Globals.cpuid)
        biRowValue.Content = "Konekcija: " + Globals.databaseName
        biColumnValue.Content = "   Tvrtka: " + Globals.tvrtka_naziv + "   Objekt: " + Globals.objekt_naziv + " Računalo: " + Globals.cpuid

        'Dodaj iteme
        For Each item As ReturnList In mysql.vratiTvrtke(Globals.cpuid)
            Dim barmanager1 As New BarManager
            Dim barcheckitem = New BarCheckItem()
            barcheckitem.Content = item.tvrtke_naziv
            barcheckitem.Name = item.dabase
            Tvrtka.Items.Add(barcheckitem)
        Next

        For Each item As ReturnList In mysql.vratiObjekte()
            Dim barmanager1 As New BarManager
            Dim BarCheckItem = New BarCheckItem()
            BarCheckItem.Content = item.objekti_naziv
            BarCheckItem.Name = item.objekti_adresa
            Objekt.Items.Add(BarCheckItem)
        Next
    End Function

    Public Function checkButtons()

    End Function

    Private Sub TileBarItem_Click_1(sender As Object, e As EventArgs)
        MessageBox.Show(Globals.databaseInfo)
    End Sub


    Private Sub TileBarItem_Click_2(sender As Object, e As EventArgs)

    End Sub



    Private Sub TileBarItem_Click_3(sender As Object, e As EventArgs)
        mysql.podaciInstalacija(Globals.cpuid)
    End Sub


    Public Function getClass()
        Try

        Catch ex As Exception

        End Try
    End Function
    Sub BuyPizzaHandler()
        MessageBox.Show("Button")
    End Sub

End Class

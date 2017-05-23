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

        'For Each item As ReturnList In mysql.maloprodajaIzbornik()
        '    Dim colour As String = "#ffaacc"
        '    Dim icona As String = "Filter/BarArgument_32x32.png"
        '    Color.FromRgb(Convert.ToByte(colour.Substring(1, 2), 16), Convert.ToByte(colour.Substring(3, 2), 16), Convert.ToByte(colour.Substring(5, 2), 16))
        '    Dim barmanager1 As New BarManager
        '    Dim TileBarItem = New TileBarItem()
        '    TileBarItem.Content = item.tipka2
        '    TileBarItem.Name = "ffss"
        '    AddHandler pokusaj1.Click, AddressOf BuyPizzaHandler
        '    Dim pizz As String = "BuyPizzaHandler"

        '    TileBarItem.Width = 150
        '    Icon = New BitmapImage(New Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/" + icona + ""))
        '    TileBarItem.TileGlyph = Icon
        '    TileBarItem.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#FF901B45"), Color))

        '    maloprodaja.Items.Add(TileBarItem)
        'Next

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



    Private Sub pokusaj1_Click(sender As Object, e As EventArgs) Handles pokusaj1.Click
        ade()

    End Sub

    Public Function ade()
        Dim array() As String = {Globals.prodaja, Globals.Kalkulacije, Globals.Robno, Globals.KUF, Globals.KIF, Globals.Narudzbenice, Globals.Nalozi, Globals.akcijskeCijene, Globals.servisnaRoba, Globals.Ostalo1, Globals.Ostalo2, Globals.Ostalo3}

        For Each value As String In array
            Dim s As String = value
            Dim parts As String() = s.Split(New Char() {","c})
            Dim icona As String = parts(1)
            Dim barmanager1 As New BarManager
            Dim TileBarItem = New TileBarItem()
            TileBarItem.Content = parts(3)
            TileBarItem.Name = "ffss"
            TileBarItem.Width = 150
            If parts(0) = 1 Then
                TileBarItem.Visibility = Visibility.Hidden
            ElseIf parts(0) = 2 Then

            End If

            Icon = New BitmapImage(New Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/" + icona + ""))
            TileBarItem.TileGlyph = Icon
            TileBarItem.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString(parts(2)), Color))
            maloprodaja.Items.Add(TileBarItem)
        Next
    End Function

    Private Sub Servisna_roba_Click(sender As Object, e As EventArgs) Handles Servisna_roba.Click

    End Sub

    Sub BuyPizzaHandler()
        MessageBox.Show("Button")
    End Sub
End Class

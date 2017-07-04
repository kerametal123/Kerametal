Imports DevExpress.Xpf.Bars
Imports ERP_Kerametal.MySQLinfo

Class MainWindow
    Dim licenciranje As New Licenciranje
    Dim XMLinfo As New XMLinfo
    Dim WMInfos As New WMInfo
    Dim mysql As New MySQLinfo
    Dim barmanager1 As New BarManager
    Dim BarCheckItem = New BarCheckItem()
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            If Globals.CheckForInternetConnection = True Then
                If licenciranje.provjeriLicencuOnline() = True Then
                    pripremiSucelje()
                    'Globals.newlook = True
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
    Public Function conMeni(ByVal table As String, ByVal update As Boolean)
        If Globals.newlook = False Then
            For Each item In mysql.vratiOpcijePrograma(table)
                Dim s As String = item
                Dim parts As String() = s.Split(New Char() {","c})
                Dim icona As String = parts(1)
                Dim barmanager1 As New BarManager
                Dim BarButtonItem = New BarButtonItem()
                BarButtonItem.Content = parts(3)
                'BarButtonItem.Name = parts(3)
                If parts(0) = 1 Then
                    BarButtonItem.IsVisible = False
                ElseIf parts(0) = 2 Then
                    BarButtonItem.IsVisible = True
                ElseIf parts(0) = 3 Then
                    BarButtonItem.DataContext = "ddd"
                ElseIf parts(0) = 4 Then
                ElseIf parts(0) = 8 And update = True Then
                    makeMenuBtn(Icon, parts(3))
                End If
                Icon = New BitmapImage(New Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/" + icona + ""))
                BarButtonItem.Glyph = Icon
                AddHandler BarButtonItem.ItemClick, Function(sender, e) makeMenuBtn(Icon, parts(3))
                'BarButtonItem.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString(parts(2)), Color))
                conMenu.Items.Add(BarButtonItem)
            Next
        ElseIf Globals.newlook = True Then
            tileBar.Items.Clear()
            labelcont.Visibility = Visibility.Hidden
            simpleButton.Visibility = Visibility.Hidden
            For Each item In mysql.vratiOpcijePrograma(table)
                Dim s As String = item
                Dim parts As String() = s.Split(New Char() {","c})
                Dim icona As String = parts(1)
                Dim barmanager1 As New BarManager
                Dim TileBarItem = New DevExpress.Xpf.Navigation.TileBarItem()
                TileBarItem.Content = parts(3)
                'TileBarItem.AllowGlyphTheming = True
                'BarButtonItem.Name = parts(3)
                If parts(0) = 1 Then
                    TileBarItem.Visibility = Visibility.Collapsed
                ElseIf parts(0) = 2 Then
                    TileBarItem.Visibility = Visibility.Visible
                ElseIf parts(0) = 3 Then
                ElseIf parts(0) = 4 Then
                ElseIf parts(0) = 8 And update = True Then
                    makeMenuBtn(Icon, parts(3))
                End If
                Icon = New BitmapImage(New Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/" + icona + ""))
                TileBarItem.TileGlyph = Icon
                TileBarItem.Height = "80"
                TileBarItem.Width = "120"
                ' AddHandler TileBarItem.ItemClick, Function(sender, e) makeMenuBtn(Icon, parts(3))
                TileBarItem.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#FF992F2F"), Color))
                tileBar.Items.Add(TileBarItem)
            Next
        End If
        Datoteke.Items.Clear()
        For Each item In mysql.vratiDatoteke(table)
            Dim s As String = item
            Dim parts As String() = s.Split(New Char() {","c})
            Dim icona As String = parts(1)
            Dim barmanager1 As New BarManager
            Dim barcheckitem = New BarCheckItem()
            barcheckitem.Content = parts(3)
            'BarButtonItem.Name = parts(3)
            If parts(0) = 1 Then
                barcheckitem.IsVisible = False
            ElseIf parts(0) = 2 Then
                barcheckitem.IsVisible = True
            ElseIf parts(0) = 3 Then
                barcheckitem.DataContext = "ddd"
            ElseIf parts(0) = 4 Then
            ElseIf parts(0) = 8 And update = True Then
            End If
            Icon = New BitmapImage(New Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/" + icona + ""))
            barcheckitem.Glyph = Icon
            AddHandler barcheckitem.ItemClick, Function(sender, e) makeMenuBtn(Icon, parts(3))
            'BarButtonItem.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString(parts(2)), Color))
            Datoteke.Items.Add(barcheckitem)
        Next
    End Function
    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Public Function makeMenuBtn(ByVal glyph As ImageSource, ByVal aa As String)
        labelcont.Content = aa
        simpleButton.Glyph = glyph
        AddHandler simpleButton.Click, Function(sender, e) clickBigBtn(aa)
    End Function
    Public Function clickBigBtn(ByVal aa As String)
        If aa = "Prodaja" Then
            ' Create a window from the page you need to show
            Dim mpBc As New mpBc()
            ' Open your page
            mpBc.Show()
        End If
        Return True
    End Function
    Public Function pripremiSucelje()
        mysql.infoInstalacije(Globals.cpuid)
        biRowValue.Content = "Konekcija: " + Globals.databaseName
        biColumnValue.Content = "   Tvrtka: " + Globals.tvrtka_naziv + "   Objekt: " + Globals.objekt_naziv + " Računalo: " + Globals.cpuid
        'Postavi sve
        pripremiTvrtke()
        pripremiGodine()
        pripremiPrograme()
        pripremiObjekte()
        'Postavi zadani program
        For Each itemLink As BarCheckItem In Program.Items
            If itemLink.IsChecked Then
                conMenu.Items.Clear()
                conMeni(itemLink.Name, True)
            End If
        Next
    End Function
    Public Function pripremiTvrtke()
        'Dodaj iteme
        For Each item As ReturnList In mysql.vratiTvrtke()
            Dim barmanager1 As New BarManager
            Dim barcheckitem = New BarCheckItem()
            barcheckitem.Content = item.tvrtke_naziv
            barcheckitem.Name = item.dabase
            barcheckitem.GroupIndex = 3
            If item.tvrtke_id = Globals.tvrtka Then
                barcheckitem.IsChecked = True
            End If
            AddHandler barcheckitem.ItemClick, Function(sender, e) postaviTvrtku(item.tvrtke_id)
            Tvrtka.Items.Add(barcheckitem)
        Next
    End Function
    Public Function postaviTvrtku(ByVal tvrtkaa As String)
        Globals.tvrtka = tvrtkaa
        Godina.Items.Clear()
        Objekt.Items.Clear()
        Program.Items.Clear()
        pripremiGodine()
    End Function
    Public Function pripremiObjekte()
        Objekt.Items.Clear()
        For Each item As ReturnList In mysql.vratiObjekte()
            Dim barmanager1 As New BarManager
            Dim BarCheckItem = New BarCheckItem()
            BarCheckItem.Content = item.objekti_naziv
            BarCheckItem.Name = item.objekti_adresa
            BarCheckItem.GroupIndex = 2
            If item.objekti_id = Globals.objekt Then
                BarCheckItem.IsChecked = True
            End If
            AddHandler BarCheckItem.ItemClick, Function(sender, e) endPrep(item.objekti_id)
            Objekt.Items.Add(BarCheckItem)
        Next
    End Function
    Public Function endPrep(ByVal obj As String)
        Globals.objekt = obj
        For Each itemLink As BarCheckItem In Program.Items
            If itemLink.IsChecked Then
                conMenu.Items.Clear()
                conMeni(itemLink.Name, True)
            End If
        Next
    End Function
    Public Function pripremiGodine()
        Godina.Items.Clear()
        For Each item As ReturnList In mysql.vratiGodine()
            Dim barmanager1 As New BarManager
            Dim BarCheckItem = New BarCheckItem()
            BarCheckItem.Content = item.godina
            BarCheckItem.Name = item.stringname_god
            BarCheckItem.GroupIndex = 5
            If item.idopcije_godina = Globals.aktivnaGodina Then
                BarCheckItem.IsChecked = True
            End If
            AddHandler BarCheckItem.ItemClick, Function(sender, e) postaviGodinu(item.idopcije_godina)
            Godina.Items.Add(BarCheckItem)
        Next
    End Function

    Public Function postaviGodinu(ByVal aa As String)
        Globals.aktivnaGodina = aa
        pripremiPrograme()
    End Function
    Public Function pripremiPrograme()
        Program.Items.Clear()
        For Each item As ReturnList In mysql.vratiPrograme()
            Dim barmanager1 As New BarManager
            Dim BarCheckItem = New BarCheckItem()
            BarCheckItem.Content = item.naziv_programa
            BarCheckItem.Name = item.tabela
            BarCheckItem.GroupIndex = 1
            If item.idprogrami = Globals.defaultProg Then
                BarCheckItem.IsChecked = True
            End If
            AddHandler BarCheckItem.ItemClick, Function(sender, e) postaviProgram(item.vrstaPrograma)
            Program.Items.Add(BarCheckItem)
        Next
    End Function
    Public Function postaviProgram(ByVal prog As String)
        Globals.programAktivni = prog
        pripremiObjekte()
    End Function
    Private Sub TileBarItem_Click_1(sender As Object, e As EventArgs)
        MessageBox.Show(Globals.databaseInfo)
    End Sub


    Private Sub TileBarItem_Click_2(sender As Object, e As EventArgs)

    End Sub



    Private Sub TileBarItem_Click_3(sender As Object, e As EventArgs)
        mysql.podaciInstalacija(Globals.cpuid)
    End Sub
    Private Sub Grid_MouseRightButtonDown(sender As Object, e As MouseButtonEventArgs)
        For Each itemLink As BarCheckItem In Program.Items
            If itemLink.IsChecked Then
                conMenu.Items.Clear()
                conMeni(itemLink.Name, True)
                Return
            End If
        Next
    End Sub

    Private Sub button_Click_1(sender As Object, e As RoutedEventArgs) Handles button.Click
        label.Content = "Tvrtka:" + Globals.tvrtka + "    Godina:" + Globals.aktivnaGodina + "  Program:" + Globals.programAktivni + "   Objekt:" + Globals.objekt
        For Each itemLink As BarCheckItem In Program.Items
            If itemLink.IsChecked Then
                conMenu.Items.Clear()
                conMeni(itemLink.Name, True)
            End If
        Next
    End Sub

    Private Sub simpleButton_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton.Click
        For Each itemlink As BarCheckItem In Program.Items
            Console.WriteLine(itemlink.Name)
        Next
    End Sub
End Class

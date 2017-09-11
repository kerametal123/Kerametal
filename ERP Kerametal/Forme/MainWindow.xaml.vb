Imports System.Text
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.WindowsUI
Imports ERP_Kerametal.MySQLinfo
Class MainWindow
    Dim licenciranje As New Licenciranje
    Dim XMLinfo As New XMLinfo
    Dim WMInfos As New WMInfo
    Dim mysql As New MySQLinfo
    Dim barmanager1 As New BarManager
    Dim BarCheckItem = New BarCheckItem()
    Dim rac As New Racunanje
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        runapp()
    End Sub
    Public Function runapp()
        Try
            If Globals.CheckForInternetConnection = True Then
                If licenciranje.provjeriLicencuOnline() = True Then
                    If mysql.login() = True Then
                        Globals.login = True
                        ' Create a window from the page you need to show
                        Dim Login As New Login()
                        ' Open your page
                        Login.ShowDialog()
                        pripremiSucelje()
                    ElseIf mysql.login() = False Then
                        Globals.login = False
                        pripremiSucelje()
                        Globals.newlook = True
                    End If
                ElseIf licenciranje.provjeriLicencuOnline() = False Then
                    MessageBox.Show("not ok")
                End If
            ElseIf Globals.CheckForInternetConnection = False Then
                ' maloprodaja.Visibility = Visibility.Hidden
                biMostRight.IsEnabled = False
                biMostRight.IsChecked = False

            End If
        Catch ex As Exception
            MessageBox.Show("err" & ex.Message)
        End Try
        'updateInterface()
    End Function
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
                    makeMenuBtn(icona, parts(3))
                End If
                Icon = New BitmapImage(New Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/" + icona + ""))
                BarButtonItem.Glyph = Icon
                AddHandler BarButtonItem.ItemClick, Function(sender, e) makeMenuBtn(icona, parts(3))
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
                    ' makeMenuBtn(Icon, parts(3))
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
            AddHandler barcheckitem.ItemClick, Function(sender, e) makeMenuBtn(icona, parts(3))
            'BarButtonItem.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString(parts(2)), Color))
            Datoteke.Items.Add(barcheckitem)
        Next
        Return True
    End Function
    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Public Function makeMenuBtn(ByVal glyph As String, ByVal aa As String)
        labelcont.Content = aa
        simpleButton.Glyph = New BitmapImage(New Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/" + glyph + ""))
        simpleButton.Content = aa
        'MessageBox.Show(glyph.ToString())
        'MessageBox.Show(labelcont.Content)
        AddHandler simpleButton.Click, Function(sender, e) clickBigBtn(aa)
        Return True
    End Function
    Public Function clickBigBtn(ByVal aa As String)
        If aa = "Prodaja" Then
            ' Create a window from the page you need to show
            Dim mpBc As New mpBc()
            ' Open your page
            mpBc.Show()
        End If

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
        Return True
    End Function
    Public Function pripremiTvrtke()
        'Dodaj iteme
        Tvrtka.Items.Clear()

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
        Return True
    End Function
    Public Function postaviTvrtku(ByVal tvrtkaa As String)
        Globals.tvrtka = tvrtkaa
        Godina.Items.Clear()
        Objekt.Items.Clear()
        Program.Items.Clear()
        pripremiGodine()
        Return True
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
        Return True
    End Function
    Public Function endPrep(ByVal obj As String)
        Globals.objekt = obj
        For Each itemLink As BarCheckItem In Program.Items
            If itemLink.IsChecked Then
                conMenu.Items.Clear()
                conMeni(itemLink.Name, True)
            End If
        Next
        Return True
    End Function
    Function RandomString(minCharacters As Integer, maxCharacters As Integer)
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXY"
        Static r As New Random
        Dim chactersInString As Integer = r.Next(minCharacters, maxCharacters)
        Dim sb As New StringBuilder
        For i As Integer = 1 To chactersInString
            Dim idx As Integer = r.Next(0, s.Length)
            sb.Append(s.Substring(idx, 1))
        Next
        Return sb.ToString()
    End Function
    Public Function pripremiGodine()




        '


        'Dodaj iteme
        Godina.Items.Clear()

        For Each item As ReturnList In mysql.vratiGodine()
            Dim barmanager1 As New BarManager
            Dim barcheckitem = New BarCheckItem()
            barcheckitem.Content = item.godina
            barcheckitem.Name = RandomString(2, 4)
            barcheckitem.GroupIndex = 8
            If item.idopcije_godina = Globals.aktivnaGodina Then
                barcheckitem.IsChecked = True
            End If
            AddHandler barcheckitem.ItemClick, Function(sender, e) postaviGodinu(item.idopcije_godina)
            Godina.Items.Add(barcheckitem)
        Next
        Return True
    End Function

    Public Function postaviGodinu(ByVal aa As String)
        Globals.aktivnaGodina = aa
        pripremiPrograme()
        Return True
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
        Return True
    End Function
    Public Function postaviProgram(ByVal prog As String)
        Globals.programAktivni = prog
        pripremiObjekte()
        Return True
    End Function
    Private Sub TileBarItem_Click_1(sender As Object, e As EventArgs)
        MessageBox.Show(Globals.databaseInfo)
    End Sub


    Private Sub TileBarItem_Click_2(sender As Object, e As EventArgs)

    End Sub



    Private Sub TileBarItem_Click_3(sender As Object, e As EventArgs)
        'mysql.podaciInstalacija(Globals.cpuid)
    End Sub
    Private Sub Grid_MouseRightButtonDown(sender As Object, e As MouseButtonEventArgs)
        'For Each itemLink As BarCheckItem In Program.Items
        '    If itemLink.IsChecked Then
        '        conMenu.Items.Clear()
        '        conMeni(itemLink.Name, True)
        '        Return
        '    End If
        'Next
    End Sub

    Private Sub button_Click_1(sender As Object, e As RoutedEventArgs) Handles button.Click

        runapp()
    End Sub

    Public Function updateInterfacee()


        For Each botun As String In mysql.vratiKontrole()
            Dim item As Object = radnaPovrsina.FindName(botun)

            If TypeOf item Is TextBox Then
                Dim txt As TextBox = DirectCast(item, TextBox)
                txt.IsEnabled = False
                ' txt.Background = New SolidColorBrush(Colors.LightYellow)
            ElseIf TypeOf item Is ListBox Then
                Dim lst As ListBox = DirectCast(item, ListBox)
                lst.Items.Add("Aha! you found me!")
            ElseIf TypeOf item Is Button Then
                Dim btn As Button = DirectCast(item, Button)
                btn.IsEnabled = False
                ' btn.Background = New SolidColorBrush(Colors.LightSeaGreen)
            ElseIf TypeOf item Is Label Then
                Dim lbl As Label = DirectCast(item, Label)
                lbl.IsEnabled = False
                ' lbl.Background = New SolidColorBrush(Colors.LightSeaGreen)
            ElseIf TypeOf item Is BarButtonItem Then
                Dim lbl As BarButtonItem = DirectCast(item, BarButtonItem)
                lbl.IsEnabled = False

            End If
        Next




    End Function
    Private Sub simpleButton_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton.Click
        For Each itemlink As BarCheckItem In Program.Items
            'Console.WriteLine(itemlink.Name)
        Next
    End Sub

    Private Sub biw_ItemClick(sender As Object, e As ItemClickEventArgs) Handles biw.ItemClick
        ' Create a window from the page you need to show
        Dim admin As New admin()
        ' Open your page
        admin.Show()
    End Sub

    Private Sub biMostRight_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles biMostRight.CheckedChanged
        If biMostRight.IsChecked = True Then
            Globals.adminmode = True
            admin.IsVisible = True
        ElseIf biMostRight.IsChecked = False Then
            Globals.adminmode = False
            admin.IsVisible = False
        End If
    End Sub

    Private Sub MenuItem_Click(sender As Object, e As RoutedEventArgs)
        Dim mi As MenuItem = TryCast(sender, MenuItem)
        Dim cm As ContextMenu = TryCast(mi.Parent, ContextMenu)
        Dim fe As FrameworkElement = TryCast(cm.PlacementTarget, FrameworkElement)
        MessageBox.Show(fe.Name)
    End Sub

    Private Sub labelcont_ContextMenuClosing(sender As Object, e As ContextMenuEventArgs) Handles labelcont.ContextMenuClosing

    End Sub

    Private Sub admin_ItemClick(sender As Object, e As ItemClickEventArgs) Handles admin.ItemClick
        ' Create a window from the page you need to show
        Dim admin As New UrediDozvole()
        ' Open your page
        admin.Show()
    End Sub

    Private Sub button1_Click(sender As Object, e As RoutedEventArgs) Handles button1.Click
        Globals.login = True
    End Sub
End Class

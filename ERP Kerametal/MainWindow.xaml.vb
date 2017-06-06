Imports DevExpress.Xpf.Bars
Imports ERP_Kerametal.MySQLinfo

Class MainWindow
    Dim sucelje As New Sucelje
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
    Public Function conMeni(ByVal table As String)
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

                ElseIf parts(0) = 8 Then
                    makeMenuBtn(Icon, parts(3))
                End If
                Icon = New BitmapImage(New Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/" + icona + ""))
                BarButtonItem.Glyph = Icon
                AddHandler BarButtonItem.ItemClick, Function(sender, e) makeMenuBtn(Icon, parts(3))
                'BarButtonItem.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString(parts(2)), Color))
                conMenu.Items.Add(BarButtonItem)

            Next

    End Function
    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Public Function makeMenuBtn(ByVal glyph As ImageSource, ByVal aa As String)
        labelcont.Content = aa
        simpleButton.Glyph = glyph
    End Function
    Public Function pripremiSucelje()
        mysql.infoInstalacije(Globals.cpuid)
        biRowValue.Content = "Konekcija: " + Globals.databaseName
        biColumnValue.Content = "   Tvrtka: " + Globals.tvrtka_naziv + "   Objekt: " + Globals.objekt_naziv + " Računalo: " + Globals.cpuid
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


        For Each item As ReturnList In mysql.vratiPrograme()
            Dim barmanager1 As New BarManager
            Dim BarCheckItem = New BarCheckItem()
            BarCheckItem.Content = item.naziv_programa
            BarCheckItem.Name = item.tabela
            BarCheckItem.GroupIndex = 1
            If item.idprogrami = Globals.defaultProg Then
                BarCheckItem.IsChecked = True
            End If
            AddHandler BarCheckItem.ItemClick, Function(sender, e) pripremiObjekte(item.vrstaPrograma)
            Program.Items.Add(BarCheckItem)
        Next
    End Function
    Public Function postaviTvrtku(ByVal tvrtka As String)
        Globals.tvrtka = tvrtka
    End Function
    Public Function pripremiObjekte(ByVal id As String)
        Globals.programAktivni = id
        Objekt.Items.Clear()
        For Each item As ReturnList In mysql.vratiObjekte(id)
            Dim barmanager1 As New BarManager
            Dim BarCheckItem = New BarCheckItem()
            BarCheckItem.Content = item.objekti_naziv
            BarCheckItem.Name = item.objekti_adresa
            BarCheckItem.GroupIndex = 2
            If item.objekti_id = Globals.objekt Then
                BarCheckItem.IsChecked = True
            End If
            AddHandler BarCheckItem.ItemClick, Function(sender, e) checkButtons(item.objekti_naziv)


            Objekt.Items.Add(BarCheckItem)
        Next
    End Function
    Public Function pripremiGodine(ByVal tvrtka As String)
        Globals.aktivnaGodina = ""
        Objekt.Items.Clear()
        For Each item As ReturnList In mysql.vratiObjekte(tvrtka)
            Dim barmanager1 As New BarManager
            Dim BarCheckItem = New BarCheckItem()
            BarCheckItem.Content = item.objekti_naziv
            BarCheckItem.Name = item.objekti_adresa
            BarCheckItem.GroupIndex = 2
            If item.objekti_id = Globals.objekt Then
                BarCheckItem.IsChecked = True
            End If
            AddHandler BarCheckItem.ItemClick, Function(sender, e) checkButtons(item.objekti_naziv)


            Objekt.Items.Add(BarCheckItem)
        Next
    End Function
    Public Function checkButtons(ByVal aa As String)
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
                conMeni(itemLink.Name)
                Return
            End If
        Next


    End Sub
    Public Function showClicked()

    End Function
    Private Sub conMenu_MouseRightButtonDown(sender As Object, e As MouseButtonEventArgs)

    End Sub




End Class

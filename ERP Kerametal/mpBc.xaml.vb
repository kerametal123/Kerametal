Imports System.Text.RegularExpressions
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.WindowsUI
Imports ERP_Kerametal.MySQLcompany
Public Class mpBc
    Dim racunanje As New Racunanje
    Dim mysql As New MySQLinfo
    Dim mysqlcomp As New MySQLcompany
    Dim licenca As New Licenciranje
    Dim XMLinfo As New XMLinfo
    Private mint_LastReceivedTimerID As Integer = 0
    Private mint_LastInitializedTimerID As Integer = 0
    Public Overridable Property AutoFilterCondition As AutoFilterCondition
    Dim intMilliseconds As Integer = 500000
    Dim objTimer As New System.Timers.Timer(intMilliseconds)
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        pripremiSucelje()

        AddHandler objTimer.Elapsed, AddressOf Window_TimerElapsed

        objTimer.AutoReset = False
        objTimer.Enabled = True
    End Sub
    Public Function popuniVrsteDokumenata()
        'Dodaj iteme
        For Each item As ReturnList In mysqlcomp.getVrsteDokumenata()
            Dim barmanager1 As New BarManager
            Dim ComboBoxItem = New ComboBoxItem()
            ComboBoxItem.Content = item.nazivDokumenta
            ComboBoxItem.Name = item.nazivDokumenta
            ComboBoxItem.Tag = item.idDokumenta


            tipoviDokumenataCbox.Items.Add(ComboBoxItem)
        Next


    End Function

    Public Function popuniDokumente(ByVal tip As String)
        'Dodaj iteme
        For Each item As ReturnList In mysqlcomp.getBrojeviDokumenata(tip)
            Dim barmanager1 As New BarManager
            Dim ComboBoxItem = New ComboBoxItem()
            ComboBoxItem.Content = item.brojDokumenta
            ComboBoxItem.Name = "Ime"
            ComboBoxItem.Tag = item.brojDokumenta


            brojeviDokumenataCbox.Items.Add(ComboBoxItem)
        Next


    End Function

    Public Function pripremiSucelje()
        gridPartneri.ItemsSource = mysqlcomp.getPartneriZaAktivnog
        'populate gridArtikli
        gridArtikli.ItemsSource = mysqlcomp.getArtikliZaAktivnog
        popuniVrsteDokumenata()


        gridArtikli.View.FocusedRowHandle = -1
        ocistiPrikazArtikla()
        Dim c1 As New DataGridTextColumn()
        c1.Header = "Sifra"
        c1.Width = 60
        c1.Binding = New Binding("Sifra")
        gridRacun.Columns.Add(c1)

        Dim c2 As New DataGridTextColumn()
        c2.Header = "Naziv artikla"
        c2.Width = 500
        c2.Binding = New Binding("Naziv")
        gridRacun.Columns.Add(c2)

        Dim c3 As New DataGridTextColumn()
        c3.Header = "Količina"
        c3.Width = 50
        c3.Binding = New Binding("Kolicina")
        gridRacun.Columns.Add(c3)

        Dim c4 As New DataGridTextColumn()
        c4.Header = "Cijena"
        c4.Width = 100
        c4.Binding = New Binding("Cijena")
        gridRacun.Columns.Add(c4)

        Dim c5 As New DataGridTextColumn()
        c5.Header = "Rabat"
        c5.Width = 50
        c5.Binding = New Binding("Rabat")
        gridRacun.Columns.Add(c5)

        Dim c6 As New DataGridTextColumn()
        c6.Header = "PC"
        c6.Width = 100
        c6.Binding = New Binding("PC")
        gridRacun.Columns.Add(c6)

        Dim c7 As New DataGridTextColumn()
        c7.Header = "Iznos"
        c7.Width = 100
        c7.Binding = New Binding("Iznos")
        gridRacun.Columns.Add(c7)
    End Function
    Private Sub GridControl_AsyncOperationCompleted(sender As Object, e As RoutedEventArgs)

    End Sub
    Private Sub textBox1_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBox1.TextChanged
        'Increment the counter for the number of times the textbox has been changed
        mint_LastInitializedTimerID = mint_LastInitializedTimerID + 1

        'Wait longer for shorter strings or strings without spaces
        Dim intMilliseconds As Integer = 500


        Dim objTimer As New System.Timers.Timer(intMilliseconds)
        AddHandler objTimer.Elapsed, AddressOf textBox1_TimerElapsed

        objTimer.AutoReset = False
        objTimer.Enabled = True

    End Sub

    Private Sub textBox1_TimerElapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        'Increment the counter for the number of times timers have elapsed
        mint_LastReceivedTimerID = mint_LastReceivedTimerID + 1

        'If the total number of textbox changes equals the total number of times timers have elapsed (fire method for only the latest character change)
        If mint_LastReceivedTimerID = mint_LastInitializedTimerID Then

            Me.Dispatcher.Invoke(Sub() MySearchMethod1(), System.Windows.Threading.DispatcherPriority.Normal)

        End If

    End Sub
    Private Sub textBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBox.TextChanged
        'Increment the counter for the number of times the textbox has been changed
        mint_LastInitializedTimerID = mint_LastInitializedTimerID + 1

        'Wait longer for shorter strings or strings without spaces
        Dim intMilliseconds As Integer = 500


        Dim objTimer As New System.Timers.Timer(intMilliseconds)
        AddHandler objTimer.Elapsed, AddressOf textBox_TimerElapsed

        objTimer.AutoReset = False
        objTimer.Enabled = True

    End Sub
    Private Sub textBox_TimerElapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        'Increment the counter for the number of times timers have elapsed
        mint_LastReceivedTimerID = mint_LastReceivedTimerID + 1

        'If the total number of textbox changes equals the total number of times timers have elapsed (fire method for only the latest character change)
        If mint_LastReceivedTimerID = mint_LastInitializedTimerID Then

            Me.Dispatcher.Invoke(Sub() MySearchMethod(), System.Windows.Threading.DispatcherPriority.Normal)

        End If

    End Sub
    Public Sub MySearchMethod()

        'Filtriranje grida, lokalno u RAM memoriji

        'Fire method on the Main UI Thread

        Dim filterValue As String = textBox.Text
        If Not [String].IsNullOrEmpty(filterValue) Then
            If Regex.IsMatch(filterValue, "^[0-9 ]+$") Then
                gridArtikli.Columns("naziv").AutoFilterValue = ""
                gridArtikli.Columns("sifra").AutoFilterValue = filterValue
            Else
                gridArtikli.Columns("sifra").AutoFilterValue = ""
                gridArtikli.Columns("naziv").AutoFilterCondition = AutoFilterCondition.Contains
                gridArtikli.Columns("naziv").AutoFilterValue = filterValue
            End If
        End If
    End Sub






    Private Sub ispravitiCheck_Click(sender As Object, e As RoutedEventArgs) Handles ispravitiCheck.Click
        If dodatiCheck.IsChecked = True Then
            dodatiCheck.IsChecked = False
        End If
        ispravitiCheck.IsChecked = True
        infoGrid.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#7FFF0000"), Color))
    End Sub

    Private Sub dodatiCheck_Click(sender As Object, e As RoutedEventArgs) Handles dodatiCheck.Click
        If ispravitiCheck.IsChecked = True Then
            ispravitiCheck.IsChecked = False
        End If
        dodatiCheck.IsChecked = True
        infoGrid.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#593AFF00"), Color))
    End Sub

    Private Sub simpleButton_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton.Click
        For Each item In mysqlcomp.getOperateri()

            operateriCombo.Items.Add(item.ime + " " + item.prezime)
        Next

    End Sub
    Private Sub simpleButton2_Copy2_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton2_Copy2.Click
        'For Each item In mysqlcomp.getGrupeArtikala()
        'grupeCbox.Items.Add(item.grupa)
        ' Next
        For Each item In mysqlcomp.getProizvodaci()
            Dim barmanager1 As New BarManager
            Dim comboboxitem = New ComboBoxItem()
            comboboxitem.Content = item.proiz
            comboboxitem.Tag = item.idproiz
            'AddHandler comboboxitem.Selected, Function() prijavi(item.idproiz)
            proizvodacCbox.Items.Add(comboboxitem)
        Next
    End Sub
    Private Sub dodatiCheck_Copy2_Click(sender As Object, e As RoutedEventArgs)
    End Sub

    Private Sub gridPartneri_SelectedItemChanged(sender As Object, e As SelectedItemChangedEventArgs)

    End Sub
    Public Sub MySearchMethod1()
        Dim filterValue As String = textBox1.Text
        gridPartneri.Columns("Naziv").AutoFilterCondition = AutoFilterCondition.Contains
        gridPartneri.Columns("Naziv").AutoFilterValue = filterValue
    End Sub

    Private Sub gridPartneri_SelectionChanged(sender As Object, e As GridSelectionChangedEventArgs)

    End Sub

    Private Sub simpleButton2_Copy1_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton2_Copy1.Click

    End Sub

    Private Sub gridPartneri_SelectedItemChanged_1(sender As Object, e As SelectedItemChangedEventArgs) Handles gridPartneri.SelectedItemChanged
        vozilaCbox.Items.Clear()
        vozacCbox.Items.Clear()
        For Each item As ReturnList In mysqlcomp.getVozila(gridPartneri.SelectedItem("sifra"))
            'For Each item As ReturnList In mysqlcomp.getVozila()
            Dim barmanager1 As New BarManager
            Dim comboboxitem = New ComboBoxItem()

            comboboxitem.Content = item.registracija
            comboboxitem.Tag = item.idvozila

            vozilaCbox.Items.Add(comboboxitem)

            vozilaCbox.SelectedIndex = 0
        Next
        makeMenuAdd(vozilaCbox)
    End Sub

    Private Sub vozilaCbox_DropDownClosed(sender As Object, e As EventArgs) Handles vozilaCbox.DropDownClosed
        vozacCbox.Items.Clear()
        Try

            For Each item As ReturnList In mysqlcomp.getVozaci(vozilaCbox.SelectedItem.tag)
                'For Each item As ReturnList In mysqlcomp.getVozila()
                Dim barmanager1 As New BarManager
                Dim comboboxitem = New ComboBoxItem()

                comboboxitem.Content = item.vozac


                vozacCbox.Items.Add(comboboxitem)

                vozacCbox.SelectedIndex = 0


            Next

        Catch ex As Exception

        End Try
        makeMenuAdd(vozacCbox)

    End Sub
    Public Function makeMenuAdd(ByVal e As Object)
        Dim comboboxitem1 = New ComboBoxItem()
        Dim comboboxitem2 = New ComboBoxItem()
        comboboxitem2.IsEnabled = False
        comboboxitem2.Content = "----------------"
        e.Items.Add(comboboxitem2)
        comboboxitem1.FontWeight = FontWeights.UltraBold
        comboboxitem1.Content = "Ne postoji na listi? Dodaj..."
        e.Items.Add(comboboxitem1)
    End Function

    Private Sub Window_MouseMove(sender As Object, e As MouseEventArgs)
        objTimer.Stop()
        objTimer.Start()
    End Sub
    Private Sub Window_TimerElapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        Me.Dispatcher.Invoke(Sub() showLogin(), System.Windows.Threading.DispatcherPriority.Normal)
    End Sub
    Public Function showLogin()
        WinUIMessageBox.Show(Me, "Isteklo je vrijeme za rad, molimo prijavite se!", "Ponovna prijava", CType("1", MessageBoxButton), MessageBoxResult.None, MessageBoxOptions.None)
        objTimer.Stop()
    End Function

    Private Sub simpleButton3_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton3.Click
        Dim alo = New WinUIDialogWindow
        alo.ShowDialogWindow(11, 22)
    End Sub

    Private Sub robaUsluge_Click(sender As Object, e As RoutedEventArgs) Handles robaUsluge.Click
        If robaUsluge.IsChecked = True Then
            infoGrid.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#593AFF00"), Color))
            uslugeContainer.Visibility = Visibility.Collapsed
            robaContainer.Visibility = Visibility.Visible
            robaUsluge1.IsChecked = False
        End If
    End Sub
    Private Sub robaUsluge1_Click(sender As Object, e As RoutedEventArgs) Handles robaUsluge1.Click
        If robaUsluge1.IsChecked = True Then
            infoGrid.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#5905F2FD"), Color))
            robaContainer.Visibility = Visibility.Collapsed
            uslugeContainer.Visibility = Visibility.Visible
            robaUsluge.IsChecked = False
        End If
    End Sub

    Private Sub gridArtikli_SelectedItemChanged(sender As Object, e As SelectedItemChangedEventArgs) Handles gridArtikli.SelectedItemChanged
        Try
            Dim tipArtikla As Integer
            Dim placanje As Integer
            sifraTemp.Content = gridArtikli.SelectedItem("sifra")
            nazivTemp.Content = gridArtikli.SelectedItem("naziv")
            kolicinaTemp.Content = kolicinaTbox.Text
            mpcTbox.Text = racunanje.zaokruziNaDvije(gridArtikli.SelectedItem("mpc"))
            rabatTbox.Text = rabatTbox.Text
            tipArtikla = gridArtikli.SelectedItem("tip")
            If gridArtikli.SelectedItem("placanje") > 0 Then
                placanje = gridArtikli.SelectedItem("placanje")
            ElseIf gridArtikli.SelectedItem("placanje") = 0 Then
                placanje = 0
            End If
            If gridArtikli.SelectedItem("pc") = 0 Then
                cijenaTemp.Content = racunanje.zaokruziNaDvije(gridArtikli.SelectedItem("mpc"))
            ElseIf gridArtikli.SelectedItem("pc") > 0 Then
                cijenaTemp.Content = racunanje.zaokruziNaDvije(gridArtikli.SelectedItem("pc"))
            End If

        Catch ex As Exception

        End Try
        prikaziArtikal()
    End Sub

    Public Function prikaziArtikal()
        'ocistiPrikazArtikla()
        cijenaTbox.Text = cijenaTemp.Content
        rabatTbox.Text = racunanje.zaokruziNaDvije(racunanje.racunajRabat(cijenaTemp.Content, mpcTbox.Text))
        iznosTbox.Text = racunanje.zaokruziNaDvije(racunanje.cijenaKolicina(cijenaTemp.Content, kolicinaTemp.Content))
    End Function
    Public Function ocistiPrikazArtikla()

    End Function

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        izracunajArtikalZaProdaju()
    End Sub

    Public Function izracunajArtikalZaProdaju()
        Dim item = New Item With {.Sifra = sifraTemp.Content, .Naziv = nazivTemp.Content, .Kolicina = kolicinaTemp.Content, .Cijena = cijenaTemp.Content, .Rabat = rabatTbox.Text, .PC = 1.0, .Iznos = 1.0}
        gridRacun.Items.Add(item)

    End Function

    Private Sub dodatiCheck_Copy2_Checked(sender As Object, e As RoutedEventArgs)

    End Sub
    Private Sub bacodeCbox_Checked(sender As Object, e As RoutedEventArgs) Handles bacodeCbox.Checked
        'Količina
        kolicinaTbox.IsEnabled = False
        kolicinaTbox.Text = 1
        'Cijena
        cijenaTbox.IsEnabled = False
        'Rabat
        rabatTbox.IsEnabled = False
        rabatCbox.IsChecked = False
        stringCbox.IsChecked = False
        sifraCbox.IsChecked = False
    End Sub

    Private Sub stringCbox_Checked(sender As Object, e As RoutedEventArgs) Handles stringCbox.Checked
        'Količina
        kolicinaTbox.IsEnabled = True
        kolicinaTbox.Text = 0
        'Cijena
        cijenaTbox.IsEnabled = True
        'Rabat
        rabatTbox.IsEnabled = False
        rabatCbox.IsChecked = False
        bacodeCbox.IsChecked = False
        sifraCbox.IsChecked = False
    End Sub

    Private Sub sifraCbox_Checked(sender As Object, e As RoutedEventArgs) Handles sifraCbox.Checked
        'Količina
        kolicinaTbox.IsEnabled = True
        kolicinaTbox.Text = 0
        'Cijena
        cijenaTbox.IsEnabled = True
        'Rabat
        rabatTbox.IsEnabled = False
        rabatCbox.IsChecked = False
        bacodeCbox.IsChecked = False
        stringCbox.IsChecked = False
    End Sub
    Private Sub cijenaTbox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles cijenaTbox.TextChanged
        If sender.IsFocused = True And sender.text.length > 0 Then
            rabatTbox.Text = racunanje.zaokruziNaDvije(100 - (cijenaTbox.Text / mpcTbox.Text * 100))
        End If
    End Sub
    Private Sub rabatTbox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles rabatTbox.TextChanged
        If sender.IsFocused = True And sender.text.length > 0 Then
            cijenaTemp.Content = racunanje.zaokruziNaDvije(mpcTbox.Text * (1 - rabatTbox.Text / 100))
            cijenaTbox.Text = cijenaTemp.Content
        End If

    End Sub
    Private Sub kolicinaTbox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles kolicinaTbox.TextChanged
        If sender.IsFocused = True And sender.text.length > 0 Then
            kolicinaTemp.Content = kolicinaTbox.Text
            iznosTbox.Text = racunanje.zaokruziNaDvije(cijenaTbox.Text * kolicinaTbox.Text)
        End If
    End Sub
    Private Sub iznosTbox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles iznosTbox.TextChanged
        If sender.IsFocused = True And sender.text.length > 0 Then
            Globals.cijenaUkupnoG = cijenaTbox.Text
            kolicinaTbox.Text = racunanje.zaokruziNaDvije(iznosTbox.Text / cijenaTbox.Text)

        End If
    End Sub

    Private Sub tipoviDokumenataCbox_DropDownClosed(sender As Object, e As EventArgs) Handles tipoviDokumenataCbox.DropDownClosed
        brojeviDokumenataCbox.Items.Clear()
        popuniDokumente(tipoviDokumenataCbox.SelectedItem.tag)
    End Sub

    Private Sub brojeviDokumenataCbox_DropDownClosed(sender As Object, e As EventArgs) Handles brojeviDokumenataCbox.DropDownClosed
        Try
            brojDokMain.Content = brojeviDokumenataCbox.SelectedItem.tag
        Catch ex As Exception

        End Try
    End Sub
End Class


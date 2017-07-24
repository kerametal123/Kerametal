﻿Imports System.Text.RegularExpressions
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Printing
Imports DevExpress.Xpf.WindowsUI
Imports ERP_Kerametal.MySQLcompany
Imports HtmlAgilityPack
Imports DevExpress.XtraBars.Alerter
Imports ERP_Kerametal

Public Class mpBc
    Dim enter As New EnterKeyTraversal
    Dim zadnji As Object
    Dim control As String
    Dim popup As New popupValid
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
        popuniVrsteDokumenata()
        tipoviDokumenataCbox.SelectedIndex = 0
        popuniDokumente(tipoviDokumenataCbox.SelectedItem.tag)
        brojeviDokumenataCbox.SelectedIndex = 0
        prodaja()
    End Sub
    Public Function popuniVrsteDokumenata()
        'Dodaj iteme
        For Each item As ReturnList In mysqlcomp.getVrsteDokumenata()
            Dim barmanager As New BarManager
            Dim ComboBoxItem = New ComboBoxItem()
            ComboBoxItem.Content = item.nazivDokumenta
            ComboBoxItem.Name = item.nazivDokumenta
            ComboBoxItem.Tag = item.idDokumenta
            tipoviDokumenataCbox.Items.Add(ComboBoxItem)

        Next
        Return True
    End Function
    Public Function popuniDokumente(ByVal tip As String)
        'Dodaj iteme
        Dim barmanager As New BarManager
        Dim ComboBoxItem1 = New ComboBoxItem()
        ComboBoxItem1.Content = "*"
        ComboBoxItem1.Name = "Novi"
        ComboBoxItem1.Tag = "Nova"
        brojeviDokumenataCbox.Items.Add(ComboBoxItem1)
        ComboBoxItem1.IsSelected = True
        For Each item As ReturnList In mysqlcomp.getBrojeviDokumenata(tip)
            Dim barmanager1 As New BarManager
            Dim ComboBoxItem = New ComboBoxItem()
            ComboBoxItem.Content = item.brojDokumenta
            ComboBoxItem.Name = "Ime"
            ComboBoxItem.Tag = item.brojDokumenta
            brojeviDokumenataCbox.Items.Add(ComboBoxItem)
        Next
        Return True
    End Function
    Public Function pripremiSucelje()
        gridPartneri.ItemsSource = mysqlcomp.getPartneriZaAktivnog
        'populate gridArtikli
        gridArtikli.ItemsSource = mysqlcomp.getArtikliZaAktivnog
        pripremiRacunGrid()
        getOperateri()
        pripremiPlacanjaGrid()
        'ocistiPrikaz()
        gridArtikli.View.FocusedRowHandle = -1
        Return True
    End Function
    Private Sub GridControl_AsyncOperationCompleted(sender As Object, e As RoutedEventArgs)
    End Sub
    Public Function pripremiRacunGrid()
        gridRacunNew.Items.Clear()
        Dim c1 As New DataGridTextColumn()
        c1.Header = "Sifra"
        c1.Width = 60
        c1.Binding = New Binding("Sifra")
        gridRacunNew.Columns.Add(c1)

        Dim c2 As New DataGridTextColumn()
        c2.Header = "Naziv artikla"
        c2.Width = 500
        c2.Binding = New Binding("Naziv")
        gridRacunNew.Columns.Add(c2)

        Dim c3 As New DataGridTextColumn()
        c3.Header = "Količina"
        c3.Width = 50
        c3.Binding = New Binding("Kolicina")
        gridRacunNew.Columns.Add(c3)

        Dim c4 As New DataGridTextColumn()
        c4.Header = "Cijena"
        c4.Width = 100
        c4.Binding = New Binding("Cijena")
        gridRacunNew.Columns.Add(c4)

        Dim c5 As New DataGridTextColumn()
        c5.Header = "Rabat"
        c5.Width = 50
        c5.Binding = New Binding("Rabat")
        gridRacunNew.Columns.Add(c5)

        Dim c6 As New DataGridTextColumn()
        c6.Header = "PC"
        c6.Width = 100
        c6.Binding = New Binding("PC")
        gridRacunNew.Columns.Add(c6)

        Dim c7 As New DataGridTextColumn()
        c7.Header = "Iznos"
        c7.Width = 100
        c7.Binding = New Binding("Iznos")
        gridRacunNew.Columns.Add(c7)
        Return True
    End Function
    Public Function pripremiRacunGridArhiva()

        Dim c1 As New GridColumn()
        c1.Header = "Sifra"
        c1.Width = 60
        c1.Binding = New Binding("sifra")
        gridRacun.Columns.Add(c1)

        Dim c2 As New GridColumn()
        c2.Header = "Naziv artikla"
        c2.Width = 500
        c2.Binding = New Binding("naziv")
        gridRacun.Columns.Add(c2)

        Dim c3 As New GridColumn()
        c3.Header = "Količina"
        c3.Width = 50
        c3.Binding = New Binding("kolicina")
        gridRacun.Columns.Add(c3)

        Dim c4 As New GridColumn()
        c4.Header = "Cijena"
        c4.Width = 100
        c4.Binding = New Binding("mpc")
        gridRacun.Columns.Add(c4)

        Dim c5 As New GridColumn()
        c5.Header = "Rabat"
        c5.Width = 50
        c5.Binding = New Binding("rabat")
        gridRacun.Columns.Add(c5)


        Dim c6 As New GridColumn()
        c6.Header = "Rabat 1"
        c6.Width = 100
        c6.Binding = New Binding("rabat1")
        gridRacun.Columns.Add(c6)

        Dim c7 As New GridColumn()
        c7.Header = "PC"
        c7.Width = 100
        c7.Binding = New Binding("pc")
        gridRacun.Columns.Add(c7)

        Dim c8 As New GridColumn()
        c8.Header = "Iznos"
        c8.Width = 100
        c8.Binding = New Binding("iznos")
        gridRacun.Columns.Add(c8)

        Dim c9 As New GridColumn()
        c9.Header = "Rabat 2"
        c9.Width = 100
        c9.Binding = New Binding("rabat2")
        gridRacun.Columns.Add(c9)
        Return True
    End Function
    Public Function pripremiPlacanjaGrid()
        Return True
    End Function
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
        Dim filterValue As String = textBox.Text
        Console.WriteLine(filterValue + "-" + Globals.pretraga)
        If Globals.pretraga = "sifra" Then
            gridArtikli.Columns("naziv").AutoFilterValue = ""
            gridArtikli.Columns("sifra").AutoFilterValue = filterValue
        ElseIf Globals.pretraga = "string" Then
            gridArtikli.Columns("sifra").AutoFilterValue = ""
            gridArtikli.Columns("naziv").AutoFilterCondition = AutoFilterCondition.Contains
            gridArtikli.Columns("naziv").AutoFilterValue = filterValue
        ElseIf Globals.pretraga = "barcode" Then
        gridArtikli.Columns("sifra").AutoFilterValue = ""
        gridArtikli.Columns("naziv").AutoFilterCondition = AutoFilterCondition.Contains
        gridArtikli.Columns("naziv").AutoFilterValue = filterValue
        End If
    End Sub
    Public Function prodaja()
        gridRacunNew.Visibility = Visibility.Visible
        gridRacun.Visibility = Visibility.Collapsed
        ispravitiCheck.IsChecked = False
        infoGrid.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#593AFF00"), Color))
        pripremiRacunGrid()
        Return True
    End Function
    Public Function ispravke()
        If brojeviDokumenataCbox.Text IsNot "*" Then
            gridRacunNew.Visibility = Visibility.Collapsed
            gridRacun.Visibility = Visibility.Visible
            dodatiCheck.IsChecked = False
            infoGrid.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#7FFF0000"), Color))
            'pripremiRacunGridArhiva()
            Return True
        Else
            MessageBox.Show("morate odabrati ")
        End If

    End Function
    Public Function pregledDokumenta()
        gridRacunNew.Visibility = Visibility.Collapsed
        gridRacun.Visibility = Visibility.Visible
        Return True
    End Function
    Private Sub ispravitiCheck_Click(sender As Object, e As RoutedEventArgs) Handles ispravitiCheck.Click
        Globals.urediDodaj = "uredi"
        urediDodaj()
    End Sub
    Private Sub dodatiCheck_Click(sender As Object, e As RoutedEventArgs) Handles dodatiCheck.Click
        Globals.urediDodaj = "dodaj"
        urediDodaj()
    End Sub
    Public Function getOperateri()
        For Each item In mysqlcomp.getOperateri()
            operateriCombo.Items.Add(item.ime + " " + item.prezime)
        Next
        operateriCombo.SelectedIndex = 0
        Return True
    End Function
    Private Sub simpleButton2_Copy2_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton2_Copy2.Click
        'For Each item In mysqlcomp.getGrupeArtikala()
        'grupeCbox.Items.Add(item.grupa)
        ' Next
        'For Each item In mysqlcomp.getProizvodaci()
        '    Dim barmanager1 As New BarManager
        '    Dim comboboxitem = New ComboBoxItem()
        '    comboboxitem.Content = item.proiz
        '    comboboxitem.Tag = item.idproiz
        '    'AddHandler comboboxitem.Selected, Function() prijavi(item.idproiz)
        '    proizvodacCbox.Items.Add(comboboxitem)
        'Next

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
        InputBox.Visibility = Visibility.Visible
        InputTextBox.Visibility = Visibility.Collapsed

        'Dim report As New Racun()
        'Dim window = New DocumentPreviewWindow()
        'report.Parameters("brojDokumenta").Value = brojeviDokumenataCbox.Text
        'report.Parameters("Parameter1").Value = tipoviDokumenataCbox.Tag
        'window.PreviewControl.DocumentSource = report
        'report.CreateDocument()
        'window.Show()
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
        Return True
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
        Return False
    End Function
    Private Sub simpleButton3_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton3.Click
        'gridPartneri.FindRowByValue("sifra", datumTbox.Text)
        MessageBox.Show(racunanje.PretvoriBrojUTekst(3725.0))
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
            'Samo prodajna cijena
            If gridArtikli.SelectedItem("pc") = 0 Then
                cijenaTemp.Content = racunanje.zaokruziNaDvije(gridArtikli.SelectedItem("mpc"))
            ElseIf gridArtikli.SelectedItem("pc") > 0 Then
                cijenaTemp.Content = racunanje.zaokruziNaDvije(gridArtikli.SelectedItem("pc"))
            End If
        Catch ex As Exception
        End Try
        prikaziArtikal()
    End Sub

    Public Function ocistiPrikaz()
        sifraTemp.Content = ""
        nazivTemp.Content = ""
        kolicinaTemp.Content = ""
    End Function
    Public Function prikaziArtikal()
        'ocistiPrikazArtikla()
        cijenaTbox.Text = cijenaTemp.Content
        rabatTbox.Text = racunanje.zaokruziNaDvije(racunanje.racunajRabat(cijenaTemp.Content, mpcTbox.Text))
        iznosTbox.Text = racunanje.zaokruziNaDvije(racunanje.cijenaKolicina(cijenaTemp.Content, kolicinaTemp.Content))
        Return True
    End Function

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        provjeriArtikalZaProdaju()
    End Sub
    Public Function izracunajArtikalZaProdaju()
        Dim item = New Item With {.Sifra = sifraTemp.Content, .Naziv = nazivTemp.Content, .Kolicina = kolicinaTemp.Content, .Cijena = cijenaTemp.Content, .Rabat = rabatTbox.Text, .PC = 1.0, .Iznos = 1.0}
        gridRacunNew.Items.Add(item)
        Return True
    End Function
    Public Function provjeriArtikalZaProdaju()
        Try
            Globals.sifraG = gridArtikli.SelectedItem("sifra")
            For Each item As ReturnList In mysqlcomp.stanjeArtikla()
                'Item >stanje< iz tabele artikala stanje tog artikla za zadani objekat
                labeldrLabel.Content = "Trenutačno na stanju ima: " + item.stanje + " " + gridArtikli.SelectedItem("jed") + " odabranog artikla"
                'Item >minZaliha< iz tabele artikala minimalna zaliha, ==i >i pokreće funkciju za upozorenje!
                labelprLabel.Content = "Minimalna zaliha artikla je: " + item.minZaliha + " " + gridArtikli.SelectedItem("jed") + " odabranog artikla"
            Next
        Catch ex As Exception
        End Try
        izracunajArtikalZaProdaju()
    End Function
    Private Sub dodatiCheck_Copy2_Checked(sender As Object, e As RoutedEventArgs)
    End Sub
    Private Sub bacodeCbox_Checked(sender As Object, e As RoutedEventArgs) Handles bacodeCbox.Checked

    End Sub
    Private Sub stringCbox_Checked(sender As Object, e As RoutedEventArgs) Handles stringCbox.Checked

    End Sub
    Private Sub sifraCbox_Checked(sender As Object, e As RoutedEventArgs) Handles sifraCbox.Checked

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
        If brojeviDokumenataCbox.SelectedIndex = 0 Then
            Globals.urediDodaj = "novo"
            urediDodaj()
        End If
    End Sub

    Private Sub brojeviDokumenataCbox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles brojeviDokumenataCbox.SelectionChanged
        If IsNothing(brojeviDokumenataCbox.SelectedItem) Then
        Else
            prikaziOdabraniDokument(tipoviDokumenataCbox.SelectedItem.tag, brojeviDokumenataCbox.SelectedItem.tag)
        End If
    End Sub
    Public Function prikaziOdabraniDokument(ByVal tag As String, ByVal broj As String)
        gridRacun.Columns.Clear()
        Try
            Globals.tipDokumenta = tag
            Globals.brojDokumenta = broj
            Dim ukupno As Decimal = 0
            Dim pdv As Decimal = 0
            Dim neto As Decimal = 0
            Dim mp_iznos As Decimal = 0
            Dim popusti As Decimal = 0
            Dim izn_bez_pdv As Decimal = 0
            Dim rabat As Decimal = 0
            Dim rabat_plus As Decimal = 0
            Dim sconto As Decimal = 0
            brojDokMain.Content = broj
            pripremiRacunGridArhiva()
            gridRacun.ItemsSource = mysqlcomp.getStavkeDokumenta(tag, broj)
            For Each item As ReturnList In mysqlcomp.getInfoStavkeDokumenta(tag, broj)
                rabat = racunanje.zaokruziNaDvije(item.rabatInfo)
                neto = racunanje.zaokruziNaDvije(item.netoInfo)
                ukupno = racunanje.zaokruziNaDvije(item.ukupnoInfo)
                pdv = racunanje.zaokruziNaDvije(item.pdvInfo)
                popusti = racunanje.zaokruziNaDvije(item.popustiInfo)
                izn_bez_pdv = racunanje.zaokruziNaDvije(item.bpdvInfo)
                rabat_plus = racunanje.zaokruziNaDvije(item.rabatPlusInfo)
                sconto = racunanje.zaokruziNaDvije(item.scontoInfo)
                If racunanje.zaokruziNaDvije(item.scontoInfo) < 0 Then
                    If racunanje.zaokruziNaDvije(item.rabatInfo) <> 0 Then
                        rabat = racunanje.zaokruziNaDvije(item.rabatInfo) + racunanje.zaokruziNaDvije(item.scontoInfo)
                    Else
                        rabat_plus = racunanje.zaokruziNaDvije(item.rabatPlusInfo) + racunanje.zaokruziNaDvije(item.scontoInfo)
                    End If
                    sconto = 0
                End If
                rabatTboxS.Text = rabat
                nettoTbox.Text = neto
                maticnaValutaTbox.Content = ukupno
                slovimaBroj.Content = racunanje.PretvoriBrojUTekst(ukupno)
                pdvTbox.Text = pdv
                odbiciTbox.Text = popusti
                iznosBezPDVTbox.Text = izn_bez_pdv
                rabatPlusTbox.Text = rabat_plus
                scontoTbox.Text = sconto
                datumTbox.Text = item.datumInfo
                gotovinaTbox.Text = item.gotovinaInfo
                karticeTbox.Text = item.karticeInfo
                ziralnoTbox.Text = item.ziralnoInfo
                ostaloTbox.Text = item.ostaloInfo
                datumTbox.Text = item.datumDInfo
                daniTbox.Text = item.daniInfo
                gridPartneri.Columns("sifra").AutoFilterValue = item.partnerInfo
            Next
        Catch ex As Exception
        End Try


        'ispravke()
        pregledDokumenta()
        Return True
    End Function
    Private Sub simpleButton_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton.Click
    End Sub
    Public Sub Main()
        Dim link As String = "https://www.google.com/finance/converter"
        'download page from the link into an HtmlDocument'
        Dim doc As HtmlDocument = New HtmlWeb().Load(link)
        'select <div> having class attribute equals fontdef1'
        Dim div As HtmlNode = doc.DocumentNode.SelectSingleNode("//div[@class='sfe-break-top']")
        'if the div is found, print the inner text'
        If Not div Is Nothing Then
            Console.WriteLine(div.InnerText.Trim())
        End If

    End Sub
    Private Sub ispravitiCheck_Checked(sender As Object, e As RoutedEventArgs) Handles ispravitiCheck.Checked

    End Sub
    Private Sub dodatiCheck_Checked(sender As Object, e As RoutedEventArgs) Handles dodatiCheck.Checked

    End Sub
    Private Sub kolicinaCbox_Unchecked(sender As Object, e As RoutedEventArgs) Handles kolicinaCbox.Unchecked
        kolicinaTbox.IsEnabled = False
    End Sub
    Private Sub kolicinaCbox_checked(sender As Object, e As RoutedEventArgs) Handles kolicinaCbox.Checked
        kolicinaTbox.IsEnabled = True
    End Sub
    Private Sub cijenaCbox_Unchecked(sender As Object, e As RoutedEventArgs) Handles cijenaCbox.Unchecked
        cijenaTbox.IsEnabled = False
    End Sub
    Private Sub cijenaCbox_checked(sender As Object, e As RoutedEventArgs) Handles cijenaCbox.Checked
        cijenaTbox.IsEnabled = True
    End Sub
    Private Sub rabatCbox_Checked(sender As Object, e As RoutedEventArgs) Handles rabatCbox.Checked
        rabatTbox.IsEnabled = True
    End Sub
    Private Sub rabatCbox_Unchecked(sender As Object, e As RoutedEventArgs) Handles rabatCbox.Unchecked
        rabatTbox.IsEnabled = False
    End Sub
    Private Sub YesButton_Click(sender As Object, e As RoutedEventArgs)
        'Dopunski rabat
        If control = "dopunskiRabatPost" Then
            Dim a As Object = 0
            a = racunanje.pretvoriTocke(InputTextBox.Text)
            If mysqlcomp.dopunskiRabatPost(a, Globals.brojDokumenta) = True Then
                prikaziOdabraniDokument(Globals.tipDokumenta, Globals.brojDokumenta)
            End If
        ElseIf control = "dopunskiRabatIzn" Then
            Dim iznos As Object = 0
            Dim a As Object = 0
            iznos = InputTextBox.Text
            a = racunanje.pretvoriTocke(iznos / maticnaValutaTbox.Content * 100)
            'iznos = InputTextBox.Text / maticnaValutaTbox.Text * 100
            'MessageBox.Show(a)
            If mysqlcomp.dopunskiRabatIzn(a, Globals.brojDokumenta) = True Then
                prikaziOdabraniDokument(Globals.tipDokumenta, Globals.brojDokumenta)
            End If
            'Sconto
        ElseIf control = "scontoPost" Then
            Dim a As Object = 0
            a = racunanje.pretvoriTocke(InputTextBox.Text)
            If mysqlcomp.scontoPost(a, Globals.brojDokumenta) = True Then
                prikaziOdabraniDokument(Globals.tipDokumenta, Globals.brojDokumenta)
            End If
        ElseIf control = "scontoIzn" Then
            Dim iznos As Object = 0
            Dim a As Object = 0
            iznos = InputTextBox.Text
            a = racunanje.pretvoriTocke(iznos / maticnaValutaTbox.Content * 100)
            If mysqlcomp.scontoIzn(a, Globals.brojDokumenta) = True Then
                prikaziOdabraniDokument(Globals.tipDokumenta, Globals.brojDokumenta)
            End If
        End If
        InputTextBox.Text = ""
        zadnji.focus()
        zadnji.SelectAll()
        InputBox.Visibility = Visibility.Collapsed
    End Sub
    Private Sub NoButton_Click(sender As Object, e As RoutedEventArgs)
        InputBox.Visibility = Visibility.Collapsed
    End Sub
    Private Sub dopunskiRabatPost_ItemClick(sender As Object, e As ItemClickEventArgs) Handles dopunskiRabatPost.ItemClick
        popupEditorSingleShow(rabatPlusTbox, "Unesite postotak dopunskog rabata:", True)
        control = "dopunskiRabatPost"
    End Sub
    Private Sub dopunskiRabatIzn_ItemClick(sender As Object, e As ItemClickEventArgs) Handles dopunskiRabatIzn.ItemClick
        popupEditorSingleShow(rabatPlusTbox, "Unesite iznos dopunskog rabata:", False)
        control = "dopunskiRabatIzn"
    End Sub
    Private Sub scontoPost_ItemClick(sender As Object, e As ItemClickEventArgs) Handles scontoPost.ItemClick
        popupEditorSingleShow(scontoTbox, "Unesite postotak sconta:", True)
        control = "scontoPost"
    End Sub
    Private Sub scontoIzn_ItemClick(sender As Object, e As ItemClickEventArgs) Handles scontoIzn.ItemClick
        popupEditorSingleShow(scontoTbox, "Unesite iznos sconta:", False)
        control = "scontoIzn"
    End Sub
    Public Function popupEditorSingleShow(ByVal sender As Object, ByVal tekst As String, ByVal postotak As Boolean)
        If postotak = False Then
            iBoxMsg.Text = tekst
            zadnji = sender
            InputBox.Visibility = Visibility.Visible
            InputTextBox.Focus()
            InputTextBox.SelectAll()
        ElseIf postotak = True Then
            iBoxMsg.Text = tekst
            zadnji = sender
            InputBox.Visibility = Visibility.Visible
            InputTextBox.Focus()
            InputTextBox.SelectAll()
        End If
        Return True
    End Function
    Private Sub DockPanell_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Enter Then
            Dim s As TextBox = TryCast(e.Source, TextBox)
            If s IsNot Nothing Then
                s.MoveFocus(New TraversalRequest(FocusNavigationDirection.[Next]))
            End If
            e.Handled = True
        End If
    End Sub
    Private Sub gotovinaTbox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles gotovinaTbox.TextChanged
        If sender.IsFocused = True And sender.text.length > 0 Then
        End If
    End Sub
    Private Sub simpleButton1_Copy_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton1_Copy.Click
        Dim report As New Racun()
        Dim window = New DocumentPreviewWindow()
        report.Parameters("brojDokumenta").Value = brojeviDokumenataCbox.Text
        report.Parameters("Parameter1").Value = tipoviDokumenataCbox.Tag
        window.PreviewControl.DocumentSource = report
        report.CreateDocument()
        window.Show()
    End Sub

    Private Sub Informacije_ItemClick(sender As Object, e As ItemClickEventArgs) Handles Informacije.ItemClick
        Dim artInfo As New artInfo(gridArtikli.SelectedItem("sifra"))
        artInfo.Show()
    End Sub

    Private Sub novoButton_Click(sender As Object, e As RoutedEventArgs) Handles novoButton.Click
        Globals.urediDodaj = "novo"
        urediDodaj()
    End Sub
    Public Function urediDodaj()
        If Globals.urediDodaj = "uredi" Then
            If brojeviDokumenataCbox.Text = "*" Or brojeviDokumenataCbox.Text = "" Then
                dodatiCheck.IsChecked = False
                ispravitiCheck.IsChecked = False
                infoGrid.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#593AFF00"), Color))
            ElseIf Not brojeviDokumenataCbox.Text = "*" Or brojeviDokumenataCbox.Text = "" Then
                dodatiCheck.IsChecked = False
                ispravitiCheck.IsChecked = True
                infoGrid.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#7FFF0000"), Color))
            End If
        ElseIf Globals.urediDodaj = "dodaj" Then
            If brojeviDokumenataCbox.Text = "*" Or brojeviDokumenataCbox.Text = "" Then
                dodatiCheck.IsChecked = False
                ispravitiCheck.IsChecked = False
                infoGrid.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#593AFF00"), Color))
            ElseIf Not brojeviDokumenataCbox.Text = "*" Or brojeviDokumenataCbox.Text = "" Then
                dodatiCheck.IsChecked = True
                ispravitiCheck.IsChecked = False
                infoGrid.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#5900DCFF"), Color))
            End If
        ElseIf Globals.urediDodaj = "novo" Then
            tipoviDokumenataCbox.SelectedIndex = 0
            brojeviDokumenataCbox.SelectedIndex = 0
            dodatiCheck.IsChecked = False
            ispravitiCheck.IsChecked = False
            infoGrid.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#593AFF00"), Color))
            brojeviDokumenataCbox.Items.Clear()
            popuniDokumente(tipoviDokumenataCbox.SelectedItem.tag)
            prodaja()
        End If


    End Function

    Private Sub bacodeCbox_Click(sender As Object, e As RoutedEventArgs) Handles bacodeCbox.Click
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
        Globals.pretraga = "barcode"
        MessageBox.Show("<div class='localhostinfo' name = 'ip_pass_uname' stlye='font_weight='12px bold' background_color='0''> Server=127.0.0.1;Database=kerametal;Uid=root;Pwd=samael89; </div>")
    End Sub

    Private Sub stringCbox_Click(sender As Object, e As RoutedEventArgs) Handles stringCbox.Click
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
        Globals.pretraga = "string"
    End Sub

    Private Sub sifraCbox_Click(sender As Object, e As RoutedEventArgs) Handles sifraCbox.Click
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
        Globals.pretraga = "sifra"
    End Sub
End Class

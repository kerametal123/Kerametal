Imports System.Data
Imports DevExpress.Xpf.Bars
Imports ERP_Kerametal.MySQLinfo

Public Class UrediDozvole
    Dim mysqlinfo As New MySQLinfo
    Dim mysqlcompany As New MySQLcompany
    Dim info As String
    Dim opcija As String
    Dim var As String
    Dim aktivnost As String
    Dim logiranje As String
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        rackor()
    End Sub
    Public Function ocisti()
        Try
            ime.Text = ""
            prezime.Text = ""
            email.Text = ""
            telefon.Text = ""
            tip.SelectedItem = -1
            korisnicko.Text = ""
            lozinka.Text = ""
            Return True
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Function populateKorisnici(ByVal strict As Boolean)
        If rkorisnici.IsChecked = True Then
            Dim t As New System.Data.DataTable
            t = mysqlinfo.getKorisnici(False)
            korisnici.Items.Clear()
            ' korisnicko.Text = t.Rows(0)("username").ToString()
            For Each row As DataRow In t.Rows
                Dim BarCheckItem = New ComboBoxItem()
                BarCheckItem.Content = row("ime") + " " + row("prezime")
                BarCheckItem.Tag = row("iduser")
                korisnici.Items.Add(BarCheckItem)
            Next row
        ElseIf rracunala.IsChecked = True Then

            Dim t As New System.Data.DataTable
            t = mysqlinfo.getInstalacije(False)
            korisnici.Items.Clear()
            ' korisnicko.Text = t.Rows(0)("username").ToString()
            For Each row As DataRow In t.Rows
                Dim BarCheckItem = New ComboBoxItem()
                BarCheckItem.Content = row("instalacije_naziv")
                BarCheckItem.Tag = row("instalacije_hwid")
                korisnici.Items.Add(BarCheckItem)
            Next row
        End If


    End Function
    Public Function pullInfo()
        Try
            Dim t As New System.Data.DataTable
            t = mysqlinfo.getKorisnik(korisnici.SelectedItem.tag)
            If Globals.tipKorisnika = "korisnik" Then
                korisnicko.Text = t.Rows(0)("username").ToString()
                lozinka.Text = t.Rows(0)("lozinka").ToString()
                ime.Text = t.Rows(0)("ime").ToString()
                prezime.Text = t.Rows(0)("prezime").ToString()
                email.Text = t.Rows(0)("email").ToString()
                telefon.Text = t.Rows(0)("telefon").ToString()
                'Uid.Content = t.Rows(0)("iduser").ToString()
                tip.SelectedIndex = t.Rows(0)("tip_korisnika").ToString() - 1
            ElseIf Globals.tipKorisnika = "racunalo" Then
                ime_Copy.Text = t.Rows(0)("instalacije_naziv").ToString()
                If t.Rows(0)("instalacije_aktivnost").ToString() = "1" Then
                    checkBox1.IsChecked = True
                ElseIf t.Rows(0)("instalacije_aktivnost").ToString() = "0" Then
                    checkBox1.IsChecked = False
                End If
                If t.Rows(0)("instalacije_login").ToString() = "1" Then
                    checkBox1_Copy.IsChecked = True
                ElseIf t.Rows(0)("instalacije_login").ToString() = "0" Then
                    checkBox1_Copy.IsChecked = False
                End If


            End If

        Catch ex As Exception

        End Try

    End Function
    Public Function populateTvrtke(ByVal strict As Boolean)
        Dim t As New System.Data.DataTable
        t = mysqlinfo.getTvrtke(strict)
        tvrtke.Items.Clear()
        ' korisnicko.Text = t.Rows(0)("username").ToString()
        For Each row As DataRow In t.Rows
            Dim BarCheckItem = New ComboBoxItem()
            BarCheckItem.Content = row("tvrtke_naziv")
            BarCheckItem.Tag = row("idtvrtke")
            tvrtke.Items.Add(BarCheckItem)
        Next row
        Dim BarCheckItems = New ComboBoxItem()
        BarCheckItems.Content = "----------------------"
        BarCheckItems.Tag = "qwer"
        BarCheckItems.IsEnabled = False
        Dim BarCheckItems1 = New ComboBoxItem()
        BarCheckItems1.Content = "Dodaj novu tvrtku"
        BarCheckItems1.FontWeight = FontWeights.Bold
        BarCheckItems1.Tag = "qwert"

        tvrtke.Items.Add(BarCheckItems)
        tvrtke.Items.Add(BarCheckItems1)
    End Function
    Public Function populateObjekti(ByVal tvrtka As String, ByVal all As Boolean)
        Try
            Dim t As New System.Data.DataTable
            If all = True Then
                t = mysqlinfo.getObjekti(tvrtka, True, mysqlinfo.getTipPrograma(programi.SelectedItem.tag))
            ElseIf all = False Then
                t = mysqlinfo.getObjekti(tvrtka, False, mysqlinfo.getTipPrograma(programi.SelectedItem.tag))
            End If

            objekti.Items.Clear()
            ' korisnicko.Text = t.Rows(0)("username").ToString()
            For Each row As DataRow In t.Rows
                Dim BarCheckItem = New ComboBoxItem()
                BarCheckItem.Content = row("objekti_naziv")
                BarCheckItem.Tag = row("idobjekti")
                objekti.Items.Add(BarCheckItem)
            Next row
            Dim BarCheckItems = New ComboBoxItem()
            BarCheckItems.Content = "----------------------"
            BarCheckItems.Tag = "qwer"
            BarCheckItems.IsEnabled = False
            Dim BarCheckItems1 = New ComboBoxItem()
            BarCheckItems1.Content = "Dodaj novi objekt"
            AddHandler BarCheckItems1.Selected, Function(sender, e) newObjekt()
            BarCheckItems1.FontWeight = FontWeights.Bold
            BarCheckItems1.Tag = "qwert"
            objekti.Items.Add(BarCheckItems)
            objekti.Items.Add(BarCheckItems1)
        Catch ex As Exception

        End Try

    End Function
    Public Function newObjekt()
        ' Create a window from the page you need to show
        Dim mpBc As New Objekti()
        ' Open your page
        mpBc.Show()
    End Function
    Public Function populateGodine()
        Dim t As New System.Data.DataTable
        t = mysqlinfo.getGodine()
        godine.Items.Clear()
        ' korisnicko.Text = t.Rows(0)("username").ToString()
        For Each row As DataRow In t.Rows
            Dim BarCheckItem = New ComboBoxItem()
            BarCheckItem.Content = row("stringname")
            BarCheckItem.Tag = row("idgodine")
            godine.Items.Add(BarCheckItem)
        Next row
        Dim BarCheckItems = New ComboBoxItem()
        BarCheckItems.Content = "----------------------"
        BarCheckItems.Tag = "qwer"
        BarCheckItems.IsEnabled = False
        Dim BarCheckItems1 = New ComboBoxItem()
        BarCheckItems1.Content = "Otvori novu godinu"
        BarCheckItems1.FontWeight = FontWeights.Bold
        BarCheckItems1.Tag = "qwert"
        godine.Items.Add(BarCheckItems)
        godine.Items.Add(BarCheckItems1)
    End Function
    Public Function populateProgrami()
        Dim t As New System.Data.DataTable
        t = mysqlinfo.getProgrami()
        programi.Items.Clear()
        ' sviProgrami.Items.Clear()
        ' korisnicko.Text = t.Rows(0)("username").ToString()
        For Each row As DataRow In t.Rows
            Dim BarCheckItem = New ComboBoxItem()
            BarCheckItem.Content = row("naziv_programa")
            BarCheckItem.Tag = row("idprogrami")
            'BarCheckItem.Name = row("vrstaPrograma").ToString
            programi.Items.Add(BarCheckItem)
            'sviProgrami.Items.Add(BarCheckItem)
        Next row
        Dim BarCheckItems = New ComboBoxItem()
        BarCheckItems.Content = "----------------------"
        BarCheckItems.Tag = "qwer"
        BarCheckItems.IsEnabled = False
        Dim BarCheckItems1 = New ComboBoxItem()
        BarCheckItems1.Content = "Dodaj novi program"
        BarCheckItems1.FontWeight = FontWeights.Bold
        BarCheckItems1.Tag = "qwert"
        programi.Items.Add(BarCheckItems)
        programi.Items.Add(BarCheckItems1)
    End Function
    'Public Function populateSviProgrami()
    '    Dim t As New System.Data.DataTable
    '    t = mysqlinfo.getProgrami()
    '    ' programi.Items.Clear()
    '    sviProgrami.Items.Clear()
    '    ' korisnicko.Text = t.Rows(0)("username").ToString()
    '    For Each row As DataRow In t.Rows
    '        Dim BarCheckItem = New ComboBoxItem()
    '        BarCheckItem.Content = row("naziv_programa")
    '        BarCheckItem.Tag = row("idprogrami")
    '        'BarCheckItem.Name = row("vrstaPrograma").ToString
    '        'programi.Items.Add(BarCheckItem)
    '        sviProgrami.Items.Add(BarCheckItem)
    '    Next row
    'End Function
    Private Sub tvrtke_DropDownClosed(sender As Object, e As EventArgs) Handles tvrtke.DropDownClosed
        populateObjekti(tvrtke.SelectedItem.tag, False)
        getOpcijeInfo()
        populateMenu()

    End Sub

    Private Sub primjeniBtn_Click(sender As Object, e As RoutedEventArgs) Handles primjeniBtn.Click

        If checkBox1.IsChecked = True Then
            aktivnost = "1"
        ElseIf checkBox1.IsChecked = False Then
            aktivnost = "0"
        End If
        If checkBox1_Copy.IsChecked = True Then
            logiranje = "1"
        ElseIf checkBox1_Copy.IsChecked = False Then
            logiranje = "0"
        End If
        If mysqlinfo.provjeriOpcijeTvrtke(var, korisnici.SelectedItem.tag, tvrtke.SelectedItem.tag, godine.SelectedItem.tag, programi.SelectedItem.tag, objekti.SelectedItem.tag) = True Then
            If mysqlinfo.provjeriOpcijeGodine(var, korisnici.SelectedItem.tag, tvrtke.SelectedItem.tag, godine.SelectedItem.tag, programi.SelectedItem.tag, objekti.SelectedItem.tag) = True Then
                If mysqlinfo.provjeriOpcijePrograma(var, korisnici.SelectedItem.tag, tvrtke.SelectedItem.tag, godine.SelectedItem.tag, programi.SelectedItem.tag, objekti.SelectedItem.tag) = True Then
                    If mysqlinfo.provjeriOpcijeObjekta(var, korisnici.SelectedItem.tag, tvrtke.SelectedItem.tag, godine.SelectedItem.tag, programi.SelectedItem.tag, objekti.SelectedItem.tag) = True Then
                        If opcija = "defaults" Then
                            Try
                                If var = "racunalo" Then

                                    If mysqlinfo.setKorisnikPocetnePostavke(objekti.SelectedItem.tag, tvrtke.SelectedItem.tag, programi.SelectedItem.tag, godine.SelectedItem.tag, korisnici.SelectedItem.tag, aktivnost, logiranje, ime_Copy.Text, "1", "1", "1", "1", var) = True Then
                                        MessageBox.Show("Postavke su uspješno primjenjene.")
                                    Else
                                        MessageBox.Show("Greška u postavkama.")
                                        'refreshPostavke()
                                    End If
                                ElseIf var = "korisnik" Then
                                    If mysqlinfo.setKorisnikPocetnePostavke(objekti.SelectedItem.tag, tvrtke.SelectedItem.tag, programi.SelectedItem.tag, godine.SelectedItem.tag, korisnici.SelectedItem.tag, lozinka.Text, korisnicko.Text, tip.SelectedItem.tag, email.Text, telefon.Text, ime.Text, prezime.Text, var) = True Then
                                        MessageBox.Show("Postavke su uspješno primjenjene.")
                                    Else
                                        MessageBox.Show("Greška u postavkama.")
                                        'refreshPostavke()
                                    End If
                                End If

                            Catch ex As Exception
                                MessageBox.Show(ex.ToString)
                            End Try

                        ElseIf opcija = "novo" Then
                            MessageBox.Show("spremljene nove opcije")
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Public Function getOpcije()
        'getmp()
        'getvp()
        'getfin()
        'getug()
        getDefaults()
    End Function

    'Public Function getug()
    '    Dim t As New System.Data.DataTable
    '    t = mysqlinfo.getOpcije("opcije_ug", korisnici.SelectedItem.tag)
    '    opcijeug.Items.Clear()
    '    ' korisnicko.Text = t.Rows(0)("username").ToString()
    '    For Each row As DataRow In t.Rows
    '        Dim BarCheckItem = New ComboBoxItem()
    '        BarCheckItem.Content = row("naziv")
    '        BarCheckItem.Tag = row("idopcije_ug")
    '        opcijeug.Items.Add(BarCheckItem)
    '    Next row
    'End Function
    'Public Function getmp()
    '    Dim t As New System.Data.DataTable
    '    t = mysqlinfo.getOpcije("opcije_mp", korisnici.SelectedItem.tag)
    '    opcijemp.Items.Clear()
    '    ' korisnicko.Text = t.Rows(0)("username").ToString()
    '    For Each row As DataRow In t.Rows
    '        Dim BarCheckItem = New ComboBoxItem()
    '        BarCheckItem.Content = row("naziv")
    '        BarCheckItem.Tag = row("idopcije_mp")
    '        opcijemp.Items.Add(BarCheckItem)
    '    Next row
    'End Function
    'Public Function getvp()
    '    Dim t As New System.Data.DataTable
    '    t = mysqlinfo.getOpcije("opcije_vp", korisnici.SelectedItem.tag)
    '    opcijevp.Items.Clear()
    '    ' korisnicko.Text = t.Rows(0)("username").ToString()
    '    For Each row As DataRow In t.Rows
    '        Dim BarCheckItem = New ComboBoxItem()
    '        BarCheckItem.Content = row("naziv")
    '        BarCheckItem.Tag = row("idopcije_vp")
    '        opcijevp.Items.Add(BarCheckItem)
    '    Next row
    'End Function
    'Public Function getfin()
    '    Dim t As New System.Data.DataTable
    '    t = mysqlinfo.getOpcije("opcije_fk", korisnici.SelectedItem.tag)
    '    opcijefin.Items.Clear()
    '    ' korisnicko.Text = t.Rows(0)("username").ToString()
    '    For Each row As DataRow In t.Rows
    '        Dim BarCheckItem = New ComboBoxItem()
    '        BarCheckItem.Content = row("naziv")
    '        BarCheckItem.Tag = row("idopcije_fk")
    '        opcijefin.Items.Add(BarCheckItem)
    '    Next row
    'End Function
    Public Function getDefaults()

        Dim t As New System.Data.DataTable
        t = mysqlinfo.getDefaults(korisnici.SelectedItem.tag)
        For Each row As DataRow In t.Rows
            'opcijefin.SelectedValue = row("FK")
            'opcijemp.SelectedValue = row("MP")
            'opcijeug.SelectedValue = row("UG")
            'opcijevp.SelectedValue = row("VP")
            'ooooo
            objekti.SelectedValue = row("objekt")
            tvrtke.SelectedValue = row("tvrtka")
            godine.SelectedValue = row("godina")
            programi.SelectedValue = row("defaultProg")
            info = row("objekt")
        Next row
    End Function

    Private Sub korisnici_DropDownClosed(sender As Object, e As EventArgs) Handles korisnici.DropDownClosed
        refreshPostavke()
        populateMenu()
        pullInfo()
    End Sub

    Public Function pripremiTipove()

        tip.Items.Clear()
        For Each item As ReturnList In mysqlinfo.vratiTipoveKorisnika()
            Dim BarCheckItem = New ComboBoxItem()
            BarCheckItem.Content = item.naziv_tipa
            BarCheckItem.Tag = item.idtipovi
            tip.Items.Add(BarCheckItem)
        Next
        Return True
    End Function
    Public Function refreshPostavke()
        Try
            populateTvrtke(False)
            populateGodine()
            populateProgrami()
            'populateSviProgrami()
            getDefaults()
            populateObjekti(tvrtke.SelectedItem.tag, True)
            getOpcije()
            getDefaults()
        Catch ex As Exception
        End Try
        'getOpcijeInfo()
        Try
            conMenu.Items.Clear()
            For Each item In mysqlinfo.getDetaljnoOpcije(korisnici.SelectedItem.tag, tvrtke.SelectedItem.tag, godine.SelectedItem.tag, objekti.SelectedItem.tag, mysqlinfo.getTabelaPrograma(programi.SelectedItem.tag))
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
                ElseIf parts(0) = 8 Then
                    ' makeMenuBtn(Icon, parts(3))
                End If
                Icon = New BitmapImage(New Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/" + icona + ""))
                TileBarItem.TileGlyph = Icon
                TileBarItem.Height = "80"
                TileBarItem.Width = "100"
                'AddHandler TileBarItem.ItemClick, Function(sender, e) makeMenuBtn()
                TileBarItem.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#FF992F2F"), Color))
                conMenu.Items.Add(TileBarItem)
            Next
            ' MessageBox.Show((mysqlinfo.getTabelaPrograma(programi.SelectedItem.tag)))
        Catch ex As Exception

        End Try
        getOpcijeInfo()
    End Function
    Public Function makemenubutton()

    End Function
    Private Sub TileBarItem_Click(sender As Object, e As EventArgs)
        Globals.logMaker("Glavni izbornik, Maloprodaja", sender)
        Dim form As New mpBc()
        form.ShowDialog()
    End Sub
    'Public Function prikaziIzbornik()
    '    Try
    '        conMenu.Items.Clear()
    '        For Each item In mysqlinfo.getDetaljnoOpcije(korisnici.SelectedItem.tag, tvrtke.SelectedItem.tag, godine.SelectedItem.tag, objekti.SelectedItem.tag, mysqlinfo.getTabelaPrograma(sviProgrami.SelectedItem.tag))
    '            Dim s As String = item
    '            Dim parts As String() = s.Split(New Char() {","c})
    '            Dim icona As String = parts(1)
    '            Dim barmanager1 As New BarManager
    '            Dim TileBarItem = New DevExpress.Xpf.Navigation.TileBarItem()
    '            TileBarItem.Content = parts(3)
    '            'TileBarItem.AllowGlyphTheming = True
    '            'BarButtonItem.Name = parts(3)
    '            If parts(0) = 1 Then
    '                TileBarItem.Visibility = Visibility.Collapsed
    '            ElseIf parts(0) = 2 Then
    '                TileBarItem.Visibility = Visibility.Visible
    '            ElseIf parts(0) = 3 Then
    '            ElseIf parts(0) = 4 Then
    '            ElseIf parts(0) = 8 Then
    '                ' makeMenuBtn(Icon, parts(3))
    '            End If
    '            Icon = New BitmapImage(New Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/" + icona + ""))
    '            TileBarItem.TileGlyph = Icon
    '            TileBarItem.Height = "80"
    '            TileBarItem.Width = "100"
    '            ' AddHandler TileBarItem.ItemClick, Function(sender, e) makeMenuBtn(Icon, parts(3))
    '            TileBarItem.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#FF992F2F"), Color))
    '            conMenu.Items.Add(TileBarItem)
    '        Next
    '        ' MessageBox.Show((mysqlinfo.getTabelaPrograma(programi.SelectedItem.tag)))
    '    Catch ex As Exception

    '    End Try

    'End Function
    Public Function getOpcijeInfo()
        Try
            nazivOpcije.Text = ""
            Dim t As New System.Data.DataTable
            t = mysqlinfo.getOpcije(korisnici.SelectedItem.tag, tvrtke.SelectedItem.tag, godine.SelectedItem.tag, objekti.SelectedItem.tag, mysqlinfo.getTabelaPrograma(programi.SelectedItem.tag))
            If t.Rows.Count And tvrtke.SelectedItem.tag And godine.SelectedItem.tag And objekti.SelectedItem.tag > 0 Then
                templateBtn.Visibility = Visibility.Hidden
                status.Content = ""
                For Each row As DataRow In t.Rows
                    nazivOpcije.Text = row("naziv")
                Next row
            Else
                templateBtn.Visibility = Visibility.Visible
                status.Content = "Odabrani korisnik nema postavke za odabranu kombinaciju."
            End If
        Catch ex As Exception
        End Try
    End Function
    Private Sub programi_DropDownClosed(sender As Object, e As EventArgs) Handles programi.DropDownClosed
        Try
            Dim t As New System.Data.DataTable
            t = mysqlinfo.getObjekti(tvrtke.SelectedItem.tag, False, mysqlinfo.getTipPrograma(programi.SelectedItem.tag))
            objekti.Items.Clear()
            ' korisnicko.Text = t.Rows(0)("username").ToString()
            For Each row As DataRow In t.Rows
                Dim BarCheckItem = New ComboBoxItem()
                BarCheckItem.Content = row("objekti_naziv")
                BarCheckItem.Tag = row("idobjekti")
                objekti.Items.Add(BarCheckItem)
            Next row
        Catch ex As Exception
        End Try
        getOpcijeInfo()
        populateMenu()
    End Sub
    Private Sub objekti_DropDownOpened(sender As Object, e As EventArgs) Handles objekti.DropDownOpened
        populateObjekti(tvrtke.SelectedItem.tag, False)
        populateMenu()
    End Sub
    Private Sub objekti_DropDownClosed(sender As Object, e As EventArgs) Handles objekti.DropDownClosed
        populateMenu()
        getOpcijeInfo()
    End Sub
    Public Function populateMenu()
        Try
            conMenu.Items.Clear()
            Dim opnr As Int32 = 0
            Dim finalnr As Int32
            For Each item In mysqlinfo.getDetaljnoOpcije(korisnici.SelectedItem.tag, tvrtke.SelectedItem.tag, godine.SelectedItem.tag, objekti.SelectedItem.tag, mysqlinfo.getTabelaPrograma(programi.SelectedItem.tag))
                Console.WriteLine(finalnr)
                Dim s As String = item
                Dim parts As String() = s.Split(New Char() {","c})
                Dim icona As String = parts(1)
                Dim defs As String = parts(0)
                Dim barmanager1 As New BarManager
                Dim TileBarItem = New DevExpress.Xpf.Navigation.TileBarItem()
                TileBarItem.Content = parts(3)
                'defs = parts(0).ToString
                'TileBarItem.AllowGlyphTheming = True
                'BarButtonItem.Name = parts(3)
                If parts(0) = 1 Then
                    TileBarItem.Visibility = Visibility.Collapsed
                ElseIf parts(0) = 2 Then
                    TileBarItem.Visibility = Visibility.Visible
                ElseIf parts(0) = 3 Then
                ElseIf parts(0) = 4 Then
                ElseIf parts(0) = 8 Then
                End If
                Icon = New BitmapImage(New Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/" + icona + ""))
                TileBarItem.TileGlyph = Icon
                TileBarItem.Height = "80"
                TileBarItem.Width = "100"
                AddHandler TileBarItem.Click, Function(sender, e) makeMenuBtn(TileBarItem.Content, icona, defs)
                TileBarItem.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#FF992F2F"), Color))
                conMenu.Items.Add(TileBarItem)
            Next
            ' MessageBox.Show((mysqlinfo.getTabelaPrograma(programi.SelectedItem.tag)))
        Catch ex As Exception
        End Try
    End Function
    Public Function makeMenuBtn(ByVal content As String, ByVal icon As String, ByVal defs As String)
        btnContent.Text = content
        simpleButton.Glyph = New BitmapImage(New Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/" + icon + ""))
        If defs = "8" Then
            setup.IsChecked = True
        Else
            setup.IsChecked = False
        End If
        Return True
    End Function
    Private Sub binovo_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles bidefaults.CheckedChanged
        checkovi()
    End Sub
    Public Function checkovi()
        If bidefaults.IsChecked = True Then
            pocetnePostavke()
        Else
            pravljenjePostavki()
        End If
    End Function
    Public Function pocetnePostavke()
        dodatno.Visibility = Visibility.Hidden
        opcija = "defaults"
        primjeniBtn.Visibility = Visibility.Visible
    End Function
    Public Function pravljenjePostavki()
        opcija = "novo"
        dodatno.Visibility = Visibility.Visible
        'primjeniBtn.Visibility = Visibility.Hidden
    End Function
    Private Sub binew_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles binew.CheckedChanged
        checkovi()
    End Sub

    Private Sub godine_DropDownClosed(sender As Object, e As EventArgs) Handles godine.DropDownClosed
        If rkorisnici.IsChecked = True Then
            getOpcijeInfo()
        End If

    End Sub

    Private Sub templateBtn_Click(sender As Object, e As RoutedEventArgs) Handles templateBtn.Click
        If rkorisnici.IsChecked = True Then
            Dim imeopcije As String
            If String.IsNullOrEmpty(nazivOpcije.Text) Then
                imeopcije = korisnici.SelectedItem.content + " " + programi.SelectedItem.Content
            Else
                imeopcije = nazivOpcije.Text
            End If

            Dim dialogBoxWithResult As New OdabirPostojeceOpcije(mysqlinfo.getTabelaPrograma(programi.SelectedItem.tag), tvrtke.SelectedItem.tag, godine.SelectedItem.tag, objekti.SelectedItem.tag, korisnici.SelectedItem.tag, imeopcije)
            ' Open dialog box and retrieve dialog result when ShowDialog returns
            Dim dialogResult? As Boolean = dialogBoxWithResult.ShowDialog()
            Select Case dialogResult
                Case True
                    'MessageBox.Show(dialogBoxWithResult.opcija)
                    refreshPostavke()
                    populateMenu()
                Case False
                    ' User canceled dialog box
                Case Else
                    ' Indeterminate
            End Select

        End If
    End Sub
    Public Function rackor()
        If rracunala.IsChecked = True Then
            'Kontrole
            racunalogrid.IsEnabled = True
            korisnikgrid.IsEnabled = False
            populateKorisnici(False)
            bidefaults.IsChecked = True
            opcija = "defaults"
            var = "racunalo"
            Globals.tipKorisnika = "racunalo"
            checkovi()
            ocisti()
            pripremiTipove()
            ' pripremiPostavke()
        ElseIf rkorisnici.IsChecked = True Then
            'Kontrole
            racunalogrid.IsEnabled = False
            korisnikgrid.IsEnabled = True
            populateKorisnici(False)
            bidefaults.IsChecked = True
            opcija = "defaults"
            var = "korisnik"
            Globals.tipKorisnika = "korisnik"
            checkovi()
            ocisti()
            pripremiTipove()
            ' pripremiPostavke()
        End If

    End Function
    Private Sub rracunala_Click(sender As Object, e As RoutedEventArgs) Handles rracunala.Click
        rackor()
    End Sub
    Private Sub rkorisnici_Click(sender As Object, e As RoutedEventArgs) Handles rkorisnici.Click
        rackor()
    End Sub
    Private Sub TileBarItem_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub simpleButton_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton.Click

    End Sub

    Private Sub current_ItemClick(sender As Object, e As ItemClickEventArgs) Handles current.ItemClick

    End Sub

    Private Sub ime_TextChanged(sender As Object, e As TextChangedEventArgs) Handles ime.TextChanged

    End Sub

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        'Update Opcije po kolumni

    End Sub
End Class

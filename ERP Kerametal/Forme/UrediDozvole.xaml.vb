Imports System.Data
Imports DevExpress.Xpf.Bars

Public Class UrediDozvole
    Dim mysqlinfo As New MySQLinfo
    Dim info As String
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        populateKorisnici(False)
    End Sub

    Public Function populateKorisnici(ByVal strict As Boolean)
        Dim t As New System.Data.DataTable
        t = mysqlinfo.getKorisnici(strict)
        korisnici.Items.Clear()
        ' korisnicko.Text = t.Rows(0)("username").ToString()
        For Each row As DataRow In t.Rows
            Dim BarCheckItem = New ComboBoxItem()
            BarCheckItem.Content = row("ime") + " " + row("prezime")
            BarCheckItem.Tag = row("iduser")
            korisnici.Items.Add(BarCheckItem)
        Next row
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
    End Function
    Public Function populateObjekti(ByVal tvrtka As String, ByVal all As Boolean)
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
    End Function
    Public Function populateSviProgrami()
        Dim t As New System.Data.DataTable
        t = mysqlinfo.getProgrami()
        ' programi.Items.Clear()
        sviProgrami.Items.Clear()
        ' korisnicko.Text = t.Rows(0)("username").ToString()
        For Each row As DataRow In t.Rows
            Dim BarCheckItem = New ComboBoxItem()
            BarCheckItem.Content = row("naziv_programa")
            BarCheckItem.Tag = row("idprogrami")
            'BarCheckItem.Name = row("vrstaPrograma").ToString
            'programi.Items.Add(BarCheckItem)
            sviProgrami.Items.Add(BarCheckItem)
        Next row
    End Function
    Private Sub tvrtke_DropDownClosed(sender As Object, e As EventArgs) Handles tvrtke.DropDownClosed
        populateObjekti(tvrtke.SelectedItem.tag, False)
    End Sub

    Private Sub refresh_Click(sender As Object, e As RoutedEventArgs) Handles refresh.Click
        'populateObjekti(tvrtke.SelectedItem.tag, False)
        If mysqlinfo.setKorisnikPocetnePostavke(objekti.SelectedItem.tag, tvrtke.SelectedItem.tag, programi.SelectedItem.tag, godine.SelectedItem.tag, korisnici.SelectedItem.tag, opcijemp.SelectedItem.tag, opcijevp.SelectedItem.tag, opcijefin.SelectedItem.tag, opcijeug.SelectedItem.tag) = True Then
            MessageBox.Show("Postavke su uspješno primjenjene.")
        Else
            MessageBox.Show("Greška u postavkama.")
            'refreshPostavke()
        End If
    End Sub

    Public Function getOpcije()
        getmp()
        getvp()
        getfin()
        getug()
        getDefaults()
    End Function

    Public Function getug()
        Dim t As New System.Data.DataTable
        t = mysqlinfo.getOpcije("opcije_ug", korisnici.SelectedItem.tag)
        opcijeug.Items.Clear()
        ' korisnicko.Text = t.Rows(0)("username").ToString()
        For Each row As DataRow In t.Rows
            Dim BarCheckItem = New ComboBoxItem()
            BarCheckItem.Content = row("naziv")
            BarCheckItem.Tag = row("idopcije_ug")
            opcijeug.Items.Add(BarCheckItem)
        Next row
    End Function
    Public Function getmp()
        Dim t As New System.Data.DataTable
        t = mysqlinfo.getOpcije("opcije_mp", korisnici.SelectedItem.tag)
        opcijemp.Items.Clear()
        ' korisnicko.Text = t.Rows(0)("username").ToString()
        For Each row As DataRow In t.Rows
            Dim BarCheckItem = New ComboBoxItem()
            BarCheckItem.Content = row("naziv")
            BarCheckItem.Tag = row("idopcije_mp")
            opcijemp.Items.Add(BarCheckItem)
        Next row
    End Function
    Public Function getvp()
        Dim t As New System.Data.DataTable
        t = mysqlinfo.getOpcije("opcije_vp", korisnici.SelectedItem.tag)
        opcijevp.Items.Clear()
        ' korisnicko.Text = t.Rows(0)("username").ToString()
        For Each row As DataRow In t.Rows
            Dim BarCheckItem = New ComboBoxItem()
            BarCheckItem.Content = row("naziv")
            BarCheckItem.Tag = row("idopcije_vp")
            opcijevp.Items.Add(BarCheckItem)
        Next row
    End Function
    Public Function getfin()
        Dim t As New System.Data.DataTable
        t = mysqlinfo.getOpcije("opcije_fk", korisnici.SelectedItem.tag)
        opcijefin.Items.Clear()
        ' korisnicko.Text = t.Rows(0)("username").ToString()
        For Each row As DataRow In t.Rows
            Dim BarCheckItem = New ComboBoxItem()
            BarCheckItem.Content = row("naziv")
            BarCheckItem.Tag = row("idopcije_fk")
            opcijefin.Items.Add(BarCheckItem)
        Next row
    End Function
    Public Function getDefaults()
        Dim t As New System.Data.DataTable
        t = mysqlinfo.getDefaults(korisnici.SelectedItem.tag)
        For Each row As DataRow In t.Rows
            opcijefin.SelectedValue = row("FK")
            opcijemp.SelectedValue = row("MP")
            opcijeug.SelectedValue = row("UG")
            opcijevp.SelectedValue = row("VP")
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
    End Sub
    Public Function refreshPostavke()
        Try
            populateTvrtke(False)
            populateGodine()
            populateProgrami()
            populateSviProgrami()
            getDefaults()
            populateObjekti(tvrtke.SelectedItem.tag, True)
            getOpcije()
            getDefaults()
        Catch ex As Exception
        End Try
    End Function

    Private Sub programi_DropDownClosed(sender As Object, e As EventArgs) Handles programi.DropDownClosed
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
    End Sub
    Public Function conMeni(ByVal table As String, ByVal update As Boolean)

        tileBar.Items.Clear()
        'labelcont.Visibility = Visibility.Hidden
        simpleButton.Visibility = Visibility.Hidden
        For Each item In mysqlinfo.vratiOpcijePrograma(table)
            Dim s As String = item
            Dim parts As String() = s.Split(New Char() {","c})
            Dim icona As String = parts(1)
            Dim barmanager1 As New BarManager
            Dim TileBarItem = New DevExpress.Xpf.Navigation.TileBarItem()
            TileBarItem.Content = parts(3)
            MessageBox.Show(parts(3))
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


    End Function
    Private Sub objekti_DropDownOpened(sender As Object, e As EventArgs) Handles objekti.DropDownOpened
        populateObjekti(tvrtke.SelectedItem.tag, False)
    End Sub

    Private Sub sviProgrami_DropDownClosed(sender As Object, e As EventArgs) Handles sviProgrami.DropDownClosed
        'mysqlinfo.getTipPrograma(programi.SelectedItem.tag)
        conMeni(mysqlinfo.getTabelaPrograma(programi.SelectedItem.tag), False)
        MessageBox.Show((mysqlinfo.getTabelaPrograma(programi.SelectedItem.tag)))
    End Sub
End Class

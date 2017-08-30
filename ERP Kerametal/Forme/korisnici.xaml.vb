Imports System.Data
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Grid
Imports ERP_Kerametal.MySQLinfo

Public Class korisnici
    Dim mysqlinfo As New MySQLinfo
    Dim mysqlcompany As New MySQLcompany
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        korisnici1.ItemsSource = mysqlinfo.getKorisnici(strict:=True)
        ocisti()
        pripremiObjekte()
        pripremiTvrtke()
        pripremiTipove()
        pripremiPostavke()
    End Sub

    Private Sub korisnici1_SelectedItemChanged(sender As Object, e As SelectedItemChangedEventArgs) Handles korisnici1.SelectedItemChanged, korisnici1.SelectedItemChanged
        pullInfo()
    End Sub

    Public Function pullInfo()
        Dim t As New System.Data.DataTable
        t = mysqlinfo.getKorisnik(korisnici1.SelectedItem("iduser"))
        korisnicko.Text = t.Rows(0)("username").ToString()
        lozinka.Text = t.Rows(0)("lozinka").ToString()
        ime.Text = t.Rows(0)("ime").ToString()
        prezime.Text = t.Rows(0)("prezime").ToString()
        email.Text = t.Rows(0)("email").ToString()
        telefon.Text = t.Rows(0)("telefon").ToString()
        uid.Content = t.Rows(0)("iduser").ToString()
        objekt.SelectedIndex = t.Rows(0)("objekt").ToString() - 1
        tvrtka.SelectedIndex = t.Rows(0)("tvrtka").ToString() - 1
        tip.SelectedIndex = t.Rows(0)("tip_korisnika").ToString() - 1
        Try


            presets.SelectedIndex = t.Rows(0)("postavke").ToString() - 1
        Catch ex As Exception

        End Try
    End Function
    Public Function pripremiObjekte()

        objekt.Items.Clear()
        For Each item As ReturnList In mysqlinfo.vratiObjekteSve()
            Dim BarCheckItem = New ComboBoxItem()
            BarCheckItem.Content = item.objekti_naziv
            BarCheckItem.Tag = item.idobjekti
            objekt.Items.Add(BarCheckItem)

        Next
        Return True
    End Function
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
    Public Function pripremiTvrtke()
        tvrtka.Items.Clear()
        For Each item As ReturnList In mysqlinfo.vratiTvrtkeSve()
            Dim BarCheckItem = New ComboBoxItem()
            BarCheckItem.Content = item.tvrtke_naziv
            BarCheckItem.Tag = item.tvrtke_id
            tvrtka.Items.Add(BarCheckItem)
        Next
        Return True
    End Function
    Public Function pripremiPostavke()
        Dim t As New System.Data.DataTable
        t = mysqlcompany.vratiPostavke()
        presets.Items.Clear()
        ' korisnicko.Text = t.Rows(0)("username").ToString()
        For Each row As DataRow In t.Rows
            Dim BarCheckItem = New ComboBoxItem()
            BarCheckItem.Content = row("naziv")
            BarCheckItem.Tag = row("idpresets")
            presets.Items.Add(BarCheckItem)
        Next row
    End Function
    Public Function pushInfo()


    End Function

    Public Function ocisti()
        Try
            ime.Text = ""
            prezime.Text = ""
            email.Text = ""
            telefon.Text = ""
            tvrtka.SelectedItem = -1
            objekt.SelectedItem = -1
            tip.SelectedItem = -1
            presets.SelectedItem = -1
            korisnicko.Text = ""
            lozinka.Text = ""
            Return True
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        Try
            If mysqlinfo.setKorisnik(ime.Text, prezime.Text, telefon.Text, email.Text, tvrtka.SelectedItem.tag, objekt.SelectedItem.tag, tip.SelectedItem.tag, korisnicko.Text, lozinka.Text, uid.Content, presets.SelectedItem.tag) = True Then
                MessageBox.Show("Informacije uspješno izmjenjene!")
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub button_Copy_Click(sender As Object, e As RoutedEventArgs) Handles button_Copy.Click
        ' pripremiObjekte()

    End Sub

    Private Sub button1_Click(sender As Object, e As RoutedEventArgs) Handles button1.Click
        Dim t As New System.Data.DataTable
        t = mysqlcompany.updateArtiklis()
        For Each row As DataRow In t.Rows
            Dim s2 As String = StrConv(row("ime"), VbStrConv.ProperCase)
            mysqlcompany.updateGrupe(row("idgrupeArtikala"), row("ime"))
            Console.WriteLine(row("ime"))
        Next row
    End Sub
End Class

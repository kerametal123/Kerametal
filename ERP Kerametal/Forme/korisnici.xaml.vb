Imports System.Data
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Grid
Imports ERP_Kerametal.MySQLinfo

Public Class korisnici
    'Dim mysqlinfo As New MySQLinfo
    'Dim mysqlcompany As New MySQLcompany
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        'korisnici1.ItemsSource = MySQLinfo.getKorisnici(strict:=True)
        'ocisti()
        'pripremiObjekte()
        'pripremiTvrtke()
        'pripremiTipove()
        'pripremiPostavke()
    End Sub

    'Private Sub korisnici1_SelectedItemChanged(sender As Object, e As SelectedItemChangedEventArgs) Handles korisnici1.SelectedItemChanged, korisnici1.SelectedItemChanged
    '    pullInfo()
    'End Sub


   

    'End Function

    'Public Function ocisti()
    '    Try
    '        ime.Text = ""
    '        prezime.Text = ""
    '        email.Text = ""
    '        telefon.Text = ""
    '        tvrtka.SelectedItem = -1
    '        objekt.SelectedItem = -1
    '        tip.SelectedItem = -1
    '        presets.SelectedItem = -1
    '        korisnicko.Text = ""
    '        lozinka.Text = ""
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    '    Return True
    'End Function

    'Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
    '    Try
    '        If mysqlinfo.setKorisnik(ime.Text, prezime.Text, telefon.Text, email.Text, tvrtka.SelectedItem.tag, objekt.SelectedItem.tag, tip.SelectedItem.tag, korisnicko.Text, lozinka.Text, uid.Content, presets.SelectedItem.tag) = True Then
    '            MessageBox.Show("Informacije uspješno izmjenjene!")
    '        End If

    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Private Sub button_Copy_Click(sender As Object, e As RoutedEventArgs) Handles button_Copy.Click
    '    ' pripremiObjekte()

    'End Sub

    'Private Sub button1_Click(sender As Object, e As RoutedEventArgs) Handles button1.Click
    '    Dim t As New System.Data.DataTable
    '    t = mysqlcompany.updateArtiklis()
    '    For Each row As DataRow In t.Rows
    '        Dim s2 As String = StrConv(row("ime"), VbStrConv.ProperCase)
    '        mysqlcompany.updateGrupe(row("idgrupeArtikala"), row("ime"))
    '        'Console.WriteLine(row("ime"))
    '    Next row
    'End Sub
End Class

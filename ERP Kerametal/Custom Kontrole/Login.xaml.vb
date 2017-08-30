Imports System.Data
Imports ERP_Kerametal.MySQLinfo

Public Class Login
    Dim mysqlinfo As New MySQLinfo
    Dim mysqlcompany As New MySQLcompany
    Dim strict As Boolean = True
    Public Function populateKorisnici()

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

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        populateKorisnici()

    End Sub

    Private Sub simpleButton_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton.Click
        If strict = True Then strict = False Else If strict = False Then strict = True
        populateKorisnici()
    End Sub

    Private Sub simpleButton_Copy_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton_Copy.Click
        If mysqlinfo.loginUser(korisnici.SelectedItem.tag, lozinka.Text) = True Then
            Globals.iduser = korisnici.SelectedItem.tag.ToString
            Me.Close()
        ElseIf mysqlinfo.loginUser(korisnici.SelectedItem.tag, lozinka.Text) = False Then
            warning.Content = "Lozinka ne odgovara za odabranog korisnika"
            lozinka.Text = ""
        End If
    End Sub
End Class

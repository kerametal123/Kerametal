Imports System.Data

Public Class Objekti
    Dim mysqlinfo As New MySQLinfo

    Public Function populate()

    End Function

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        objektiGrid.ItemsSource = mysqlinfo.getObjekti("1", True, "1")
        populateTvrtke(False)
        populateVrste()
    End Sub
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
    Public Function populateVrste()
        Dim t As New System.Data.DataTable
        t = mysqlinfo.getProgrami()
        vrsta.Items.Clear()
        ' korisnicko.Text = t.Rows(0)("username").ToString()
        For Each row As DataRow In t.Rows
            Dim BarCheckItem = New ComboBoxItem()
            BarCheckItem.Content = row("naziv_programa")
            BarCheckItem.Tag = row("vrstaPrograma")
            vrsta.Items.Add(BarCheckItem)
        Next row
    End Function
    Private Sub objektiGrid_SelectedItemChanged(sender As Object, e As DevExpress.Xpf.Grid.SelectedItemChangedEventArgs) Handles objektiGrid.SelectedItemChanged, objektiGrid.SelectedItemChanged
        Try
            imeobjekta.Text = objektiGrid.SelectedItem("objekti_naziv")
            adresa.Text = objektiGrid.SelectedItem("objekti_adresa")
            telefon.Text = objektiGrid.SelectedItem("objekti_telefon")
            fax.Text = objektiGrid.SelectedItem("objekti_fax")
            email.Text = objektiGrid.SelectedItem("objekti_email")
            web.Text = objektiGrid.SelectedItem("objekti_web")
            sifra.Text = objektiGrid.SelectedItem("sifraObjekta")
            tvrtke.SelectedValue = objektiGrid.SelectedItem("tvrtka")
            vrsta.SelectedValue = objektiGrid.SelectedItem("vrstaObjekta")
            If String.Compare(objektiGrid.SelectedItem("aktivnost"), "1") Then
                aktivnost.IsChecked = False
            Else
                aktivnost.IsChecked = True
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub simpleButton_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton.Click
        Dim act As String
        If aktivnost.IsChecked = True Then
            act = "1"
        ElseIf aktivnost.IsChecked = False Then
            act = "0"
        End If
        Try
            mysqlinfo.updateObjekt(imeobjekta.Text, adresa.Text, "1", tvrtke.SelectedItem.tag, vrsta.SelectedItem.tag, sifra.Text, telefon.Text, fax.Text, email.Text, web.Text, act, objektiGrid.SelectedItem("idobjekti"))

        Catch ex As Exception

        End Try

        objektiGrid.ItemsSource = mysqlinfo.getObjekti("1", True, "1")
    End Sub

    Private Sub simpleButton1_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton1.Click
        Dim act As String
        If aktivnost.IsChecked = True Then
            act = "1"
        ElseIf aktivnost.IsChecked = False Then
            act = "0"
        End If
        Try
            mysqlinfo.insertObjekti(imeobjekta.Text, adresa.Text, "1", tvrtke.SelectedItem.tag, "1", sifra.Text, telefon.Text, fax.Text, email.Text, web.Text, act, objektiGrid.SelectedItem("idobjekti"))

        Catch ex As Exception

        End Try

        objektiGrid.ItemsSource = mysqlinfo.getObjekti("1", True, "1")

    End Sub
End Class

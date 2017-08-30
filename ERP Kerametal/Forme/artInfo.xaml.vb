Imports System.Data
Imports DevExpress.Xpf.Bars

Public Class artInfo
    Dim TabItem = New TabItem()
    Dim mysql As New MySQLcompany
    Dim sifra As String
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        'wbrowser.Navigate("http://127.0.0.1/KerametalBackoffice/login.php")
        wbrowser.Navigate("http://127.0.0.1/KerametalBackoffice/slikeArtikala/1222.jpg")

    End Sub
    Public Function populate(ByVal sifra As String)
        For Each row As DataRow In mysql.infoArtikal(sifra).Rows
            tipArtikla.Content = row.Item("tip")
            sifraArtikla_Copy.Content = row.Item("grupa")
            proizvodjac.Content = row.Item("ime")
            If row.Item("objekt") = "21" Then
                label2.Content = row.Item("stanje")
            ElseIf row.Item("objekt") = "11" Then
                label1.Content = row.Item("stanje")
            End If
        Next
    End Function
    Public Sub New(ByVal sifra As String)
        InitializeComponent()
        sifraArtikla.Content = sifra
        populate(sifra)
    End Sub

End Class

Imports System.Data

Public Class OdabirPostojeceOpcije
    Public opcija As String
    Public Property SomeProperty As String
    Dim mysqlinfo As New MySQLinfo
    Dim tabelaq As String
    Dim tvrtkaq As String
    Dim godinaq As String
    Dim objektq As String
    Dim korisnikq As String
    Dim nazivq As String
    Public Sub New(ByVal tabela As String, ByVal tvrtka As String, ByVal godina As String, ByVal objekt As String, ByVal uid As String, ByVal imeopcije As String)
        tabelaq = tabela
        tvrtkaq = tvrtka
        godinaq = godina
        objektq = objekt
        korisnikq = uid
        nazivq = imeopcije
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        showOpcije(tabela, tvrtka, godina, objekt)

    End Sub

    Public Function getvp()

    End Function
    Public Function showOpcije(ByVal tabela As String, ByVal tvrtka As String, ByVal godina As String, ByVal objekt As String)
        Dim t As New System.Data.DataTable
        t = mysqlinfo.getOpcijePresets(tvrtka, godina, objekt, tabela)
        opcije.Items.Clear()
        ' korisnicko.Text = t.Rows(0)("username").ToString()
        For Each row As DataRow In t.Rows
            Dim BarCheckItem = New ComboBoxItem()
            BarCheckItem.Content = row("naziv")
            BarCheckItem.Tag = row("id" + tabela)
            opcije.Items.Add(BarCheckItem)
        Next row
    End Function
    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        Me.DialogResult = True
        Me.opcija = "Some Value"
        mysqlinfo.duplicirajOpciju(tabelaq, tvrtkaq, godinaq, objektq, korisnikq, nazivq, opcije.SelectedItem.content)
    End Sub
End Class

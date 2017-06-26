Imports HtmlAgilityPack

Public Class test
    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click

        Dim link As String = "https://www.google.com/finance/converter"
        'download page from the link into an HtmlDocument'
        Dim doc As HtmlDocument = New HtmlWeb().Load(link)
        'select <div> having class attribute equals fontdef1'
        Dim titleNode = doc.DocumentNode.SelectSingleNode("/html/head/title")
        'if the div is found, print the inner text'
        If Not titleNode Is Nothing Then
            Console.WriteLine(titleNode.InnerText.Trim())
        End If
    End Sub

End Class

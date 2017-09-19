Imports HtmlAgilityPack

Public Class test
    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click

        Dim link As String = "https://finance.google.com/finance/converter?a=" + textBox.Text + "&from=AMD&to=AED&meta=ei%3Dgr_AWdHeOdOQswG_y4KIAg"
        Console.WriteLine(link)
            'download page from the link into an HtmlDocument'
            Dim doc As HtmlDocument = New HtmlWeb().Load(link)
            'select <div> having class attribute equals fontdef1'
            Dim div As HtmlNode = doc.DocumentNode.SelectSingleNode("//span[@class='bld']")
            'if the div is found, print the inner text'
            If Not div Is Nothing Then
            textBox1.Text = div.InnerText.Trim()
        End If

    End Sub
End Class

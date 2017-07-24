Public Class artInfo
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        'wbrowser.Navigate("http://127.0.0.1/KerametalBackoffice/login.php")
        wbrowser.Navigate("http://127.0.0.1/KerametalBackoffice/slikeArtikala/1222.jpg")
    End Sub
    Public Sub New(ByVal sifra As String)

        ' This call is required by the designer.
        InitializeComponent()
        sifraArtikla.Content = sifra
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Function prikupiInformacije()
        Try

        Catch ex As Exception

        End Try
        Return True
    End Function

    Private Sub nazivArtikla_LostStylusCapture(sender As Object, e As StylusEventArgs) Handles nazivArtikla.LostStylusCapture

    End Sub
End Class

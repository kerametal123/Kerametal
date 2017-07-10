Public Class SelectAllTbox




    Public Property Text() As String
        Get
            Return textBox.Text
        End Get
        Set(ByVal value As String)

            textBox.Text = value
        End Set
    End Property

    Public Event TextChanged(sender As Object, e As TextChangedEventArgs)

    Private Sub textBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBox.TextChanged
        If System.Text.RegularExpressions.Regex.IsMatch(sender.Text, "[^0-9,-]+") Then
            MessageBox.Show("Molim koristite samo brojeve.")
            sender.Text = sender.Text.Remove(sender.Text.Length - 1)
        Else
            RaiseEvent TextChanged(sender, e)
        End If

    End Sub

    Private Sub textBox_GotMouseCapture(sender As Object, e As MouseEventArgs) Handles textBox.GotMouseCapture

        sender.SelectAll()

    End Sub
End Class

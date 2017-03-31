Imports System.Text.RegularExpressions
Imports DevExpress.Xpf.Grid

Public Class mpBc
    Dim mysql As New MySQLinfo
    Dim licenca As New Licenciranje
    Dim XMLinfo As New XMLinfo
    Public Overridable Property AutoFilterCondition As AutoFilterCondition
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub GridControl_AsyncOperationCompleted(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub textBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBox.TextChanged

        Dim filterValue As String = textBox.Text
        If Not [String].IsNullOrEmpty(filterValue) Then
            If Regex.IsMatch(filterValue, "^[0-9 ]+$") Then
                gridArtikli.Columns("naziv").AutoFilterValue = ""
                gridArtikli.Columns("sifra").AutoFilterValue = filterValue




            Else
                gridArtikli.Columns("sifra").AutoFilterValue = ""
                gridArtikli.Columns("naziv").AutoFilterCondition = AutoFilterCondition.Contains
                gridArtikli.Columns("naziv").AutoFilterValue = filterValue
            End If
        End If
    End Sub
End Class

Imports System.Data
Imports DevExpress.XtraReports.UI
Imports MySql.Data.MySqlClient

Public Class MySQLreporting
    Dim dbCon As MySqlConnection
    Dim konekcija As String = Globals.databaseInfo
    'Dim konekcija As String = "Server=127.0.0.1;Database=kerametal;Uid=root;Pwd=samael89;"

    Public Function ManageConnection(ByVal CloseConnection As Boolean, ByVal konekcija As String)
        Try
            dbCon = New MySqlConnection(konekcija)
            If CloseConnection = False Then
                If dbCon.State = ConnectionState.Closed Then _
                        dbCon.Open()
                Console.WriteLine(konekcija)
            Else
                dbCon.Close()
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Function infoSucelje()

    End Function




End Class


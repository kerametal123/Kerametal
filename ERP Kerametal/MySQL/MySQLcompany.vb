Imports System.Data
Imports MySql.Data.MySqlClient

Public Class MySQLcompany
    Dim dbCon As MySqlConnection
    Dim konekcija As String = Globals.databaseInfo

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
    Public Function tryConnection(ByVal CloseConnection As Boolean, ByVal conn As String)
        Try
            dbCon = New MySqlConnection(conn)
            If CloseConnection = False Then
                If dbCon.State = ConnectionState.Closed Then _
                        dbCon.Open()
                Console.WriteLine(conn)
            Else
                dbCon.Close()
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    'Provjerava username i password u bazi podataka, ako je broj redova veći od 0 vraća TRUE!
    Public Function login(ByVal username As String, ByVal password As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'

            Dim strQuery As String = "SELECT * FROM `korisnici` WHERE `username` = '" + username + "' and `password` = '" + password + "'"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()

            If reader.HasRows Then
                Return True
            Else
                Return False
            End If
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija)
        End Try
        Return False
    End Function
    'Koristeći username logiranog korisnika prikuplja informacije o dozvolama u List(Of String)
    'i vraća ga u funkciju koja je poziva
    Public Function infoSucelje(ByVal username As String)
        Dim result = New List(Of String)()
        Try
            ManageConnection(False, konekcija) 'Open connection'

            Dim strQuery As String = "SELECT gui_value FROM kerametal.presets inner join radnici as r inner join app_gui as app where r.preset = presets.idpresets and r.username = '" + username + "' and gui_name = presets.presetid;"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                result.Add(reader("gui_value").ToString())
            End While
            reader.Close()
            'Vraća podatke u Listi stringova
            Return result
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
            Return Nothing
        Finally
            ManageConnection(True, konekcija)
        End Try
    End Function
    'Koristeći username logiranog korisnika prikuplja informacije o dozvolama u List(Of String)
    'i vraća ga u funkciju koja je poziva
    Public Function infoSuceljeHwid(ByVal hwid As String)
        Dim result = New List(Of String)()
        Try
            ManageConnection(False, konekcija) 'Open connection'

            Dim strQuery As String = "SELECT gui_value FROM kerametal.presets inner join instalacije as r inner join app_gui as app where r.preset = presets.idpresets and r.hwid = '" + hwid + "' and gui_name = presets.presetid;"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                result.Add(reader("gui_value").ToString())
            End While
            reader.Close()
            'Vraća podatke u Listi stringova
            Return result
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
            Return Nothing
        Finally
            ManageConnection(True, konekcija)
        End Try
    End Function
    Public Function getArtikli() As DataTable
        Dim query As String = "select sifra,naziv,grupa,proiz,vpc,mpc,tarifa,stopa,pc from kerametal.artikli"
        Dim table As New DataTable()
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query, connection)

                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function getPartneri() As DataTable
        Dim query As String = "Select sifra,naziv,mjesto,opis,inozemni,maticni,obveznik,pb from partneri where objekt='21' order by naziv"
        Dim table As New DataTable()
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query, connection)

                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function getPartneriName(ByVal naziv As String) As DataTable
        Dim query As String = "Select sifra,naziv,mjesto,opis,inozemni,maticni,obveznik,pb from partneri where objekt='21' and naziv like '%" + naziv + "%' or sifra like '%" + naziv + "%' order by naziv"
        Dim table As New DataTable()
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query, connection)

                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
End Class

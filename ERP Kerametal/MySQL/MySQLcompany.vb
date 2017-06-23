﻿Imports System.Data
Imports MySql.Data.MySqlClient

Public Class MySQLcompany
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
    Public Function getPartneri() As DataTable
        Dim query As String = "Select sifra,naziv,mjesto,opis,inozemni,maticni,obveznik,pb from partneri where objekt='"+Globals.objekt+"' order by naziv"
        Dim table As New DataTable()
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query, connection)

                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function getPartneriName(ByVal naziv As String) As DataTable
        Dim query As String = "Select sifra,naziv,mjesto,opis,inozemni,maticni,obveznik,pb from partneri where objekt='" + Globals.objekt + "' and naziv like '%" + naziv + "%' or sifra like '%" + naziv + "%' order by naziv"
        Dim table As New DataTable()
        Dim query2 As String = "Select sifra, naziv, mjesto, opis, inozemni, maticni, obveznik, pb from partneri where objekt ='" + Globals.objekt + "' and naziv like '%" + naziv + "%' or sifra like '%" + naziv + "%' order by naziv"
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query, connection)

                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function getArtikliSvi()
        Dim query1 As String = "Select * from artikli LIMIT 0,10000"
        Dim table As New DataTable
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query1, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function getArtikliZaAktivnog()
        Dim query As String = "Select * from  artikli where objekt='" + Globals.objekt + "' order by naziv"
        Dim table As New DataTable()
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function getPartneriZaAktivnog()
        Dim query As String = "Select * from partneri where objekt='" + Globals.objekt + "' order by naziv;"
        Dim table As New DataTable()
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function getArtikliFilterRow()
        Dim query As String = "Select * from artikli where objekt ='" + Globals.objekt + "' order by naziv"
        Dim table As New DataTable()
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function getOperateri()
        Dim result = New List(Of ReturnList)
        Try
            Dim strQuery As String = "SELECT ime, prezime, username FROM info.users where userlevel = '3' and tvrtka = '" + Globals.tvrtka + "' and objekt= '" + Globals.objekt + "';"
            ManageConnection(False, konekcija) 'Open connection'
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.ime = reader(0)
                TempResult.prezime = reader(1)
                TempResult.username = reader(2)
                result.Add(TempResult)
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija) 'Close connection
        End Try
        Return result
    End Function
    Public Function getGrupeArtikala()
        Dim result = New List(Of ReturnList)
        Try
            Dim strQuery As String = "SELECT distinct(grupa) FROM '" + Globals.dabase + ".artikli;"
            ManageConnection(False, konekcija) 'Open connection'
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.grupa = reader(0)
                result.Add(TempResult)
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija) 'Close connection
        End Try
        Return result
    End Function
    Public Function getVrsteDokumenata()
        Dim result = New List(Of ReturnList)
        Try
            Dim strQuery As String = "SELECT naziv, sifra FROM " + Globals.dabase + ".tipovidokumenata where tip = 'prod';"
            Console.WriteLine(strQuery)
            ManageConnection(False, konekcija) 'Open connection'
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.nazivDokumenta = reader(0)
                TempResult.idDokumenta = reader(1)
                result.Add(TempResult)
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija) 'Close connection
        End Try
        Return result
    End Function
    Public Function getBrojeviDokumenata(ByVal tip As String)
        Dim result = New List(Of ReturnList)
        Try
            Dim strQuery As String = "SELECT broj FROM " + Globals.dabase + ".dok_zag_d where tip = '" + tip + "' and god = '" + Globals.aktivnaGodina + "' and objekt= '" + Globals.objekt + "' ORDER BY broj DESC limit 0, 100"
            Console.WriteLine(strQuery)
            ManageConnection(False, konekcija) 'Open connection'
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.brojDokumenta = reader(0)
                result.Add(TempResult)
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija) 'Close connection
        End Try
        Return result
    End Function
    Public Function getProizvodaci()
        Dim result = New List(Of ReturnList)
        Try
            Dim strQuery As String = "SELECT ime, idproizvodjaci FROM info.proizvodjaci;"
            ManageConnection(False, konekcija) 'Open connection'
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.proiz = reader(0)
                TempResult.idproiz = reader(1)
                result.Add(TempResult)
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija) 'Close connection
        End Try
        Return result
    End Function
    Public Function getProizvodaciId(ByVal stringname As String)
        Dim result = New List(Of ReturnList)
        Try
            Dim strQuery As String = "SELECT idproizvo FROM info.proizvodjaci where ime = '" + stringname + "'"
            ManageConnection(False, konekcija) 'Open connection'
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.idproiz = reader(0)
                result.Add(TempResult)
                '2,Actions/Reading_32x32.png,#FF901B45,Glavna Knjiga
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija) 'Close connection
        End Try
        Return result
    End Function
    Public Function getVozila(ByVal sifra As String)

        Dim result = New List(Of ReturnList)
        Try
            ManageConnection(False, konekcija) 'Open connection
            Dim strQuery As String = "Select registracija, idvozila FROM " + Globals.dabase + ".vozila where partner = '" + sifra + "'"
            'Dim strQuery As String = "Select registracija, idvozila FROM kerametal.vozila where partner = '1123'"
            'MessageBox.Show(strQuery)
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.registracija = reader(0)
                TempResult.idvozila = reader(1)
                result.Add(TempResult)
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija) 'Close connection
        End Try
        Return result
    End Function
    Public Function getVozaci(ByVal vozilo As String)

        Dim result = New List(Of ReturnList)
        Try
            ManageConnection(False, konekcija) 'Open connection
            Dim strQuery As String = "SELECT s.ime, s.prezime FROM kerametal.vozaci as s inner join vozila as v inner join vozac_vozilo as vv where idvozaci = vv.vozac and vv.vozilo = v.idvozila and v.idvozila = '" + vozilo + "' and v.partner = s.partner order by vv.idvozac_vozilo ASC ;"
            'Dim strQuery As String = "Select registracija, idvozila FROM kerametal.vozila where partner = '1123'"
            'MessageBox.Show(strQuery)
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.vozac = reader(0) + " " + reader(1)
                result.Add(TempResult)
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija) 'Close connection
        End Try
        Return result
    End Function
    Public Class ReturnList
        Public Property vozac As String
        Public Property ime As String
        Public Property prezime As String
        Public Property username As String
        Public Property grupa As String
        Public Property proiz As String
        Public Property idproiz As String
        Public Property vozaciArray As ArrayList
        Public Property registracija As String
        Public Property idvozila As String
        Public Property nazivDokumenta As String
        Public Property idDokumenta As String
        Public Property brojDokumenta As String
    End Class
End Class

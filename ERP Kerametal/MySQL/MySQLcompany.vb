Imports System.Data
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
                'Console.WriteLine(konekcija)
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
                ' Console.WriteLine(conn)
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
        Dim query As String = "Call getArtikli(1," + Globals.objekt + ")"
        Dim table As New DataTable()
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function getPartneriZaAktivnog()
        Dim query As String = "call getPartneri(" + Globals.objekt + ")"
        Dim table As New DataTable()
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function stanjeArtikla()
        Dim result = New List(Of ReturnList)
        Try
            Dim strQuery As String = "Call getStanjeArtikala('" + Globals.objekt + "','1','" + Globals.sifraG + "')"
            ManageConnection(False, konekcija) 'Open connection'
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.stanje = reader(0)
                TempResult.minZaliha = reader(1)
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
            Dim strQuery As String = "SELECT naziv, sifra, mnoz FROM " + Globals.dabase + ".tipovidokumenata where tip = 'prod';"
            Console.WriteLine(strQuery)
            ManageConnection(False, konekcija) 'Open connection'
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.nazivDokumenta = reader(0)
                TempResult.idDokumenta = reader(1)
                TempResult.dokmnoz = reader(2)
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
            Dim strQuery As String = "SELECT broj FROM " + Globals.dabase + ".dok_zag_d where tip = '" + tip + "' and god = '" + Globals.aktivnaGodina + "' and objekt= '" + Globals.objekt + "' and zak='0' ORDER BY broj DESC limit 0, 100"
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
            Dim strQuery As String = "SELECT ime, idproizvodjaci FROM proizvodjaci;"
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
            Dim strQuery As String = "SELECT idproizvo FROM proizvodjaci where ime = '" + stringname + "'"
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
            Dim strQuery As String = "SELECT s.ime, s.prezime FROM " + Globals.dabase + ".vozaci as s inner join vozila as v inner join vozac_vozilo as vv where idvozaci = vv.vozac and vv.vozilo = v.idvozila and v.idvozila = '" + vozilo + "' and v.partner = s.partner order by vv.idvozac_vozilo ASC ;"
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
    'Stavke dokumenta
    Public Function getStavkeDokumenta(ByVal tip As String, ByVal broj As String)
        Dim query1 As String = "SELECT * FROM " + Globals.dabase + ".dok_sta_d where tip = '" + tip + "' and god = '" + Globals.aktivnaGodina + "' and objekt='" + Globals.objekt + "' and broj = '" + broj + "'"
        Console.Write(query1)
        Dim table As New DataTable
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query1, connection)
                adapter.Fill(table)
                Return table
                ' getInfoStavkeDokumenta(tip, broj)
            End Using
        End Using
    End Function

    'infostavke Dokumenta
    Public Function getInfoStavkeDokumenta(ByVal tip As String, ByVal broj As String)
        Dim result = New List(Of ReturnList)
        Try
            ManageConnection(False, konekcija) 'Open connection
            Dim strQuery As String = "SELECT sum(kolicina * pc) as ukupno, sum(kolicina * pc * (stopa * 100) /(stopa + 100) / 100) as pdv, sum(kolicina * mpc) as mp_iznos, sum(kolicina * mpc * rabat1 / 100) as rabat, sum((kolicina * mpc) - (kolicina * mpc * rabat1 / 100) * rabat2 / 100) as rabat_plus, sum(kolicina * pc) - sum(kolicina * pc * (stopa * 100) /(stopa + 100) / 100) as neto, sum(kolicina * mpc) - sum(kolicina * pc) as popusti, sum(kolicina * pc) - (sum(kolicina * pc * (stopa * 100) /(stopa + 100) / 100)) -  (sum(kolicina * mpc) - sum(kolicina * pc))  as bpdv, (sum(kolicina * mpc) - sum(kolicina * pc)) - (sum(kolicina * mpc * rabat1 / 100))  - (sum((kolicina * mpc) - (kolicina * mpc * rabat1 / 100) * rabat2 / 100)) as sconto, s.dat, d.got as Gotovina, d.kar as Kartice,d.zir as Ziralno, d.ost as Ostalo, d.dat as datum, d.dani as dani, d.partner as part FROM " + Globals.dabase + ".dok_sta_d as s inner join " + Globals.dabase + ".dok_zag_d as d where s.tip = '" + tip + "' and s.god = '" + Globals.aktivnaGodina + "' and s.objekt='" + Globals.objekt + "' and s.broj = '" + broj + "' and s.broj = d.broj and s.tip = d.tip"

            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.ukupnoInfo = reader(0)
                TempResult.pdvInfo = reader(1)
                TempResult.mpIznosInfo = reader(2)
                TempResult.rabatInfo = reader(3)
                TempResult.rabatPlusInfo = reader(4)
                TempResult.netoInfo = reader(5)
                TempResult.popustiInfo = reader(6)
                TempResult.bpdvInfo = reader(7)
                TempResult.scontoInfo = reader(8)
                TempResult.datumInfo = reader(9)
                TempResult.gotovinaInfo = reader(10)
                TempResult.karticeInfo = reader(11)
                TempResult.ziralnoInfo = reader(12)
                TempResult.ostaloInfo = reader(13)
                TempResult.datumDInfo = reader(14)
                TempResult.daniInfo = reader(15)
                TempResult.partnerInfo = reader(16)
                result.Add(TempResult)
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija) 'Close connection
        End Try
        Return result
        getInfoZagDokumenta(tip, broj)
    End Function
    Public Function getInfoArtikal()
        Dim result = New List(Of ReturnList)
        Try
            ManageConnection(False, konekcija) 'Open connection
            Dim strQuery As String = "CALL `getArtikalInfo`(1, 21, '0075');"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList

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
    Public Function getInfoZagDokumenta(ByVal tip As String, ByVal broj As String)
        Dim result = New List(Of ReturnList)
        Try
            ManageConnection(False, konekcija) 'Open connection
            Dim strQuery As String = "SELECT got as Gotovina, kar as Kartice,zir as Ziralno, ost as Ostalo   FROM " + Globals.dabase + ".dok_sta_d where tip = '" + tip + "' and god = '" + Globals.aktivnaGodina + "' and objekt='" + Globals.objekt + "' and broj = '" + broj + "'"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList

                TempResult.gotovinaInfo = reader(0)
                TempResult.karticeInfo = reader(1)
                TempResult.ziralnoInfo = reader(2)
                If Not reader(3) = "" Then
                    TempResult.ostaloInfo = reader(3)
                End If

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
    Public Function dopunskiRabatPost(ByVal iznos As String, ByVal brojDokumenta As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim query As String = "update " + Globals.dabase + ".dok_sta_d set rabat2='" + iznos + "'  where tip= '" + Globals.tipDokumenta + "' and broj = '" + Globals.brojDokumenta + "' and objekt= '" + Globals.objekt + "'"
            Dim query2 As String = "update " + Globals.dabase + ".dok_sta_d set rabat = (1-((1-rabat1/100-((1-rabat1/100) *rabat2/100))-((1-rabat1/100-((1-rabat1/100)*rabat2/100))*sconto/100))) * 100 where tip= '" + Globals.tipDokumenta + "' and broj = '" + Globals.brojDokumenta + "' and objekt= '" + Globals.objekt + "'"
            Dim query3 As String = "update " + Globals.dabase + ".dok_sta_d set pc=mpc*(1-rabat/100)  where tip= '" + Globals.tipDokumenta + "' and broj = '" + Globals.brojDokumenta + "' and objekt= '" + Globals.objekt + "'"
            Dim query4 As String = "update " + Globals.dabase + ".dok_sta_d set iznos = kolicina * pc  where tip= '" + Globals.tipDokumenta + "' and broj = '" + Globals.brojDokumenta + "' and objekt= '" + Globals.objekt + "';"

            Dim QueryString As String = String.Concat(query, ";", query2, ";", query3, ";", query4)
            Dim SqlCmd As New MySqlCommand(QueryString, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function
    Public Function dopunskiRabatIzn(ByVal iznos As String, ByVal brojDokumenta As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim query As String = "update " + Globals.dabase + ".dok_sta_d set rabat2='" + iznos + "'   where tip= '" + Globals.tipDokumenta + "' and broj = '" + Globals.brojDokumenta + "' and objekt= '" + Globals.objekt + "'"
            Dim query2 As String = "update " + Globals.dabase + ".dok_sta_d set  rabat = (1-((1-rabat1/100-((1 -rabat1/100)*rabat2/100))-((1-rabat1 /100 - ((1-rabat1/100)*rabat2/100))*sconto/100)))*100  where tip= '" + Globals.tipDokumenta + "' and broj = '" + Globals.brojDokumenta + "' and objekt= '" + Globals.objekt + "'"
            Dim query3 As String = "update " + Globals.dabase + ".dok_sta_d set pc=mpc*(1-rabat/100) where tip= '" + Globals.tipDokumenta + "' and broj = '" + Globals.brojDokumenta + "' and objekt= '" + Globals.objekt + "'"
            Dim query4 As String = "update " + Globals.dabase + ".dok_sta_d set iznos = kolicina * pc  where tip= '" + Globals.tipDokumenta + "' and broj = '" + Globals.brojDokumenta + "' and objekt= '" + Globals.objekt + "';"
            Dim QueryString As String = String.Concat(query, ";", query2, ";", query3, ";", query4)
            Dim SqlCmd As New MySqlCommand(QueryString, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function
    'Sconto
    Public Function scontoPost(ByVal iznos As String, ByVal brojDokumenta As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim query As String = "update " + Globals.dabase + ".dok_sta_d set sconto='" + iznos + "'  where tip= '" + Globals.tipDokumenta + "' and broj = '" + Globals.brojDokumenta + "' and objekt= '" + Globals.objekt + "'"
            Dim query2 As String = "update " + Globals.dabase + ".dok_sta_d set rabat = (1-((1-rabat1/100-((1-rabat1/100) *rabat2/100))-((1-rabat1/100-((1-rabat1/100)*rabat2/100))*sconto/100))) * 100 where tip= '" + Globals.tipDokumenta + "' and broj = '" + Globals.brojDokumenta + "' and objekt= '" + Globals.objekt + "'"
            Dim query3 As String = "update " + Globals.dabase + ".dok_sta_d set pc=mpc*(1-rabat/100)  where tip= '" + Globals.tipDokumenta + "' and broj = '" + Globals.brojDokumenta + "' and objekt= '" + Globals.objekt + "'"
            Dim query4 As String = "update " + Globals.dabase + ".dok_sta_d set iznos = kolicina * pc  where tip= '" + Globals.tipDokumenta + "' and broj = '" + Globals.brojDokumenta + "' and objekt= '" + Globals.objekt + "';"

            Dim QueryString As String = String.Concat(query, ";", query2, ";", query3, ";", query4)
            Dim SqlCmd As New MySqlCommand(QueryString, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function
    Public Function vratiPostavke()
        Dim query1 As String = "SELECT * FROM presets;"
        Dim table As New DataTable
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query1, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function scontoIzn(ByVal iznos As String, ByVal brojDokumenta As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim query As String = "update " + Globals.dabase + ".dok_sta_d set sconto='" + iznos + "'  where tip= '" + Globals.tipDokumenta + "' and broj = '" + Globals.brojDokumenta + "' and objekt= '" + Globals.objekt + "'"
            Dim query2 As String = "update " + Globals.dabase + ".dok_sta_d set  rabat = (1-((1-rabat1/100-((1 -rabat1/100)*rabat2/100))-((1-rabat1 /100 - ((1-rabat1/100)*rabat2/100))*sconto/100)))*100  where tip= '" + Globals.tipDokumenta + "' and broj = '" + Globals.brojDokumenta + "' and objekt= '" + Globals.objekt + "'"
            Dim query3 As String = "update " + Globals.dabase + ".dok_sta_d set pc=mpc*(1-rabat/100)  where tip= '" + Globals.tipDokumenta + "' and broj = '" + Globals.brojDokumenta + "' and objekt= '" + Globals.objekt + "'"
            Dim query4 As String = "update " + Globals.dabase + ".dok_sta_d set iznos = kolicina * pc  where tip= '" + Globals.tipDokumenta + "' and broj = '" + Globals.brojDokumenta + "' and objekt= '" + Globals.objekt + "';"

            Dim QueryString As String = String.Concat(query, ";", query2, ";", query3, ";", query4)
            Dim SqlCmd As New MySqlCommand(QueryString, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function
    Public Function prodaja()
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Globals.random = Globals.randomize
            Dim strQuery As String = "INSERT INTO `kerametal`.`dok_zag_d` (`zak`, `dat_z`, `tip`, `broj`, `god`, `objekt`, `kasa`, `dat`, `partner`, `ime1`, `ime2`, `dani`, `prodavac`, `smjena`, `napomena`, `got`, `kar`, `ost`, `zir`, `interno`, `ostalo`, `izvoz`, `obveznik`, `entitet`, `pb`, `kif`, `print`, `valuta`, `tecaj`, `terminal`, `fiscal`, `fiscal1`, `ibk`, `ime3`, `ident`) VALUES ('0','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "', '12', '1557', '2016', '21', '1', '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "', '0', '0', '0', '0', '0', '0', ' ', '7.7', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '2', '0', '0', 'TRGOVINA-PC', '11377', '0', '0', 'nomeee', '');"
            Console.WriteLine("Kpr " + strQuery)
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function
    Public Function infoArtikal(ByVal sifra As String)
        Dim query As String = "SELECT *, i.ime FROM kerametal.artikli as a inner join proizvodjaci as i where sifra='" + sifra + "' and proiz = i.idproizvodjaci"
        Dim table As New DataTable()
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function


    'Prebaci u novu tabelu
    Public Function updateArtikli()
        Dim query As String = "SELECT distinct grupa FROM kerametal.artikli;"
        Dim table As New DataTable()
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function insertGrupa(ByVal ime As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Globals.random = Globals.randomize
            Dim strQuery As String = "INSERT INTO `kerametal`.`grupeartikala` (`ime`) VALUES ('" + ime + "');"
            Console.WriteLine("Kpr " + strQuery)
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function

    'Grupe to artikli
    Public Function updateGrupe(ByVal grupanr As String, ByVal grupaname As String)

        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim query As String = "UPDATE `kerametal`.`artikli` SET `grupa`='" + grupanr + "' WHERE `grupa`='" + grupaname + "';"

            Dim QueryString As String = String.Concat(query, ";")
            Dim SqlCmd As New MySqlCommand(QueryString, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True

    End Function

    Public Function updateArtiklis()
        Dim query As String = "SELECT ime, idgrupeartikala FROM kerametal.grupeartikala;"
        Dim table As New DataTable()
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function

    Public Class ReturnList
        Public Property gotovinaInfo As String
        Public Property karticeInfo As String
        Public Property ziralnoInfo As String
        Public Property ostaloInfo As String
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

        Public Property pdvInfo As String
        Public Property ukupnoInfo As String
        Public Property mpIznosInfo As String
        Public Property rabatInfo As String
        Public Property rabatPlusInfo As String
        Public Property netoInfo As String
        Public Property popustiInfo As String
        Public Property bpdvInfo As String
        Public Property scontoInfo As String
        Public Property datumInfo As String

        'Zalihe
        Public Property stanje As String
        Public Property minZaliha As String

        Public Property partnerInfo As String

        Public Property datumDInfo As String
        Public Property daniInfo As String
        Public Property dokmnoz As String

    End Class
End Class

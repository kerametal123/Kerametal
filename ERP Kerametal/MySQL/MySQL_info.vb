Imports System.Data
Imports MySql.Data.MySqlClient

Public Class MySQLinfo
    Dim xmlinfo As New XMLinfo
    Dim dbCon As MySqlConnection
    Dim konekcija As String = "Server=80.86.81.211;Database=info;Uid=root;Pwd=samael89;"
    Public dictionaryMP As New Dictionary(Of String, String)

    'Funkcija se brine i otvaranju i zatvaranju konekcija na bazu podataka, ovisno o tome da li 
    'je funkciji prosljeđeno TRUE ili FALSE (Boolean) ona će otvoriti/zatvoriti konekciju
    'Za višestruke upite bolje je ostaviti konekciju otvorenom.
    Public Sub ManageConnection(ByVal CloseConnection As Boolean, ByVal connection As String)
        Try
            'Pripremanje konekcije i upita'
            dbCon = New MySqlConnection(konekcija)
            If CloseConnection = False Then
                If dbCon.State = ConnectionState.Closed Then _
                    dbCon.Open()
                Console.WriteLine(konekcija)
            Else
                dbCon.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try

    End Sub
    'Upisuje hardware id računala u bazu podataka za kontrolu instalacija
    'Instalacija se može izgasiti kroz administratorsko sučelje na web portalu
    Public Function upisInstalacije(ByVal hwid As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'

            Dim strQuery As String = "INSERT INTO `instalacije` (`instalacije_hwid`) VALUES ('" + hwid + "')"

            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        upisRacunala(hwid)
        Return True
    End Function
    Public Function upisRacunala(ByVal hwid As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'

            Dim strQuery As String = "INSERT INTO `info`.`racunala` (`racunala_hwid`) VALUES ('" + hwid + "');"

            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function
    'Provjerava da li je aplikacija puštena (1) ili je izgašena (0) koristeći tinyint

    Public Function provjeraInstalacije(ByVal hwid As String, ByVal variabla As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'

            Dim strQuery As String = "SELECT `" + variabla + "` FROM `instalacije` WHERE `instalacije_hwid` = '" + hwid + "'"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()

            While reader.Read()
                If reader.GetByte(0) = "1" Then

                    Return True
                ElseIf reader.GetByte(0) = "0" Then
                    Return False
                End If
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija)
        End Try
        Return False
    End Function

    Public Function login()
        Try
            ManageConnection(False, konekcija) 'Open connection'

            Dim strQuery As String = "SELECT count(username) FROM info.users where username = 'admin';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()

            While reader.Read()
                If reader.GetByte(0) = "1" Then

                    Return True
                ElseIf reader.GetByte(0) = "0" Then
                    Return False
                End If
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija)
        End Try
        Return False
    End Function
    Public Function podaciInstalacija(ByVal hwid As String)
        Dim hardware As String = hwid
        Dim result = New List(Of String)()
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "SELECT  o.objekti_naziv,t.tvrtke_naziv,i.idinstalacije,
            i.instalacije_hwid, i.instalacije_naziv, i.instalacije_login, i.instalacije_tvrtka, t.dabase, p.postavke_naziv, 
            p.postavke_db1, p.postavke_db2, p.postavke_db3, p.postavke_db4, o.idobjekti, o.objekti_veza, op.* FROM info.instalacije as 
            i inner join info.tvrtke as t inner join opcije_mp as op inner join info.objekti as o inner join info.sqlpostavke as p where
            i.instalacije_tvrtka = t.idtvrtke and p.idpostavke = i.instalacija_postavke and o.idobjekti = i.instalacije_objekt and 
            instalacije_hwid = '" + Globals.cpuid + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()

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
    Public Function infoInstalacije(ByVal hwid As String)
        Dim hardware As String = hwid
        Dim result = New List(Of String)()
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "SELECT  o.objekti_naziv,t.tvrtke_naziv,i.idinstalacije, i.instalacije_hwid, 
            i.instalacije_naziv, i.instalacije_login, i.instalacije_tvrtka, t.dabase, p.postavke_naziv, p.postavke_db1, p.postavke_db2,
            p.postavke_db3, p.postavke_db4, o.idobjekti, o.objekti_veza, op.* FROM info.instalacije as i inner join info.tvrtke as t inner join 
            opcije_mp as op inner join info.objekti as o inner join info.sqlpostavke as p where i.instalacije_tvrtka = t.idtvrtke and 
            p.idpostavke = i.instalacija_postavke and o.idobjekti = i.instalacije_objekt and instalacije_hwid = '" + Globals.cpuid + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Globals.objekt = reader.GetString("idobjekti")
                Globals.tvrtka_naziv = reader.GetString("tvrtke_naziv")
                Globals.tvrtka = reader.GetString("instalacije_tvrtka")
                Globals.objekt_naziv = reader.GetString("objekti_naziv")
                Globals.racunalo_naziv = reader.GetString("instalacije_naziv")

                xmlinfo.infoXMLdb(reader.GetString("idobjekti"), reader.GetString("objekti_naziv"),
                                  reader.GetString("instalacije_tvrtka"), reader.GetString("tvrtke_naziv"),
                                  reader.GetString("dabase"), reader.GetString("postavke_db1"), reader.GetString("postavke_db2"),
                                  reader.GetString("postavke_db3"), reader.GetString("postavke_db4"))

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
    'Upisuje log u mysql
    Public Function upisLoga(ByVal action As String, ByVal type As String)
        Try
            ManageConnection(False, konekcija) 'Otvori konekciju

            Dim strQuery As String = "INSERT INTO `info`.`logdata` (`log_naziv`, `log_tip`, `log_hwid`) 
            VALUES ('" + action + "', '" + type + "', '" + Globals.cpuid + "');"

            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()
            ManageConnection(True, konekcija) 'Zatvori konekciju
        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function

    Public Function upisWMI(ByVal hddtotal As String, ByVal hddfree As String, ByVal compname As String,
                            ByVal manufacturer As String, ByVal model As String, ByVal osname As String, ByVal osversion As String,
                            ByVal systype As String, ByVal tpm As String, ByVal windir As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "UPDATE `racunala` SET `hddsize`='" + hddtotal + "', `hddfree`='" + hddfree + "',
            `compname`='" + compname + "', `manufacturer`='" + manufacturer + "', `model`='" + model + "' , `osname`='" + osname + "', 
            `osversion`='" + osversion + "', `systype`='" + systype + "',  `windir`='" + windir + "', `tpm`='" + tpm + "' WHERE 
            `racunala_hwid`='" + Globals.cpuid + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function
    Public Function getDozvoleZaAktivnog()
        Dim query As String = "SELECT  i.idinstalacije, i.instalacije_hwid, i.instalacije_naziv,
        i.instalacije_login, i.instalacije_tvrtka, t.dabase, p.postavke_naziv, p.postavke_db1, p.postavke_db2, 
        p.postavke_db3, p.postavke_db4, o.idobjekti, o.objekti_veza, op.* FROM info.instalacije as i inner join info.tvrtke 
        as t inner join opcije_mp as op inner join info.objekti as o inner join info.sqlpostavke as p where i.instalacije_tvrtka =
        t.idtvrtke and p.idpostavke = i.instalacija_postavke and o.idobjekti = i.instalacije_objekt and instalacije_hwid = '" + Globals.cpuid + "';"
        Dim table As New DataTable()
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function

    Public Function opcijeMp(ByVal hwid As String)
        Dim hardware As String = hwid
        Dim result = New List(Of String)()
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "SELECT * FROM info.opcije_mp as mp inner join instalacije as i where mp.idopcije_mp = 
            i.opcijeMP and i.instalacije_hwid  = '" + Globals.cpuid + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            ' Declare new Dictionary with String keys.

            While reader.Read()
                ' Add two keys.
                Globals.prodaja = reader.GetString("Prodaja")
                Globals.Kalkulacije = reader.GetString("Kalkulacije")
                Globals.Zaduznice = reader.GetString("Zaduznice")
                Globals.Predisponacije = reader.GetString("Predisponacije")
                Globals.Robno = reader.GetString("Robno")
                Globals.KUF = reader.GetString("KUF")
                Globals.KIF = reader.GetString("KIF")
                Globals.Narudzbenice = reader.GetString("Narudzbenice")
                Globals.Nalozi = reader.GetString("Nalozi")
                Globals.akcijskeCijene = reader.GetString("Akcijske_cijene")
                Globals.servisnaRoba = reader.GetString("Servisna_roba")
                Globals.Ostalo1 = reader.GetString("Ostalo1")
                Globals.Ostalo2 = reader.GetString("Ostalo2")
                Globals.Ostalo3 = reader.GetString("Ostalo3")
            End While
            reader.Close()
            'Vraća podatke u Listi stringova
            'Return result
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
            Return Nothing
        Finally
            ManageConnection(True, konekcija)
        End Try
    End Function
    Public Function opcijeVp(ByVal hwid As String)
        Dim hardware As String = hwid
        Dim result = New List(Of String)()
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "SELECT * FROM info.opcije_vp as Vp inner join instalacije as i where Vp.idopcije_vp = 
            i.opcijeVP and i.instalacije_hwid  = '" + Globals.cpuid + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            ' Declare new Dictionary with String keys.

            While reader.Read()
                ' Add two keys.
                Globals.vp_fakture = reader.GetString("Fakture")
                Globals.vp_Kalkulacije = reader.GetString("Kalkulacije")
                Globals.vp_Otpremnice = reader.GetString("Otpremnice")
                Globals.vp_Predisponacije = reader.GetString("Predisponacije")
                Globals.vp_Robno = reader.GetString("Robno")
                Globals.vp_KUF = reader.GetString("KUF")
                Globals.vp_KIF = reader.GetString("KIF")
                Globals.vp_Narudzbenice = reader.GetString("Narudzbenice")
                Globals.vp_Nalozi = reader.GetString("Nalozi")
                Globals.vp_akcijskeCijene = reader.GetString("Akcijske_cijene")
                Globals.vp_servisnaRoba = reader.GetString("Servisna_roba")
                Globals.vp_Elektronska_oprema = reader.GetString("Elektronska_oprema")
                Globals.vp_Ambalazni_otpad = reader.GetString("Ambalazni_otpad")
                Globals.vp_Web_fakture = reader.GetString("Web_fakture")
                Globals.vp_Ostalo1 = reader.GetString("Ostalo 1")
                Globals.vp_Ostalo2 = reader.GetString("Ostalo 2")
                Globals.vp_Ostalo3 = reader.GetString("Ostalo 3")
                Globals.vp_dat1 = reader.GetString("Dat 1")
                Globals.vp_dat2 = reader.GetString("Dat 2")
                Globals.vp_dat3 = reader.GetString("Dat 3")
            End While
            reader.Close()
            'Vraća podatke u Listi stringova
            'Return result
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
            Return Nothing
        Finally
            ManageConnection(True, konekcija)
        End Try
    End Function
    Public Function vratiTvrtke(ByVal hwid As String)
        Dim hardware As String = hwid
        Dim result = New List(Of ReturnList)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "SELECT tvrtke_naziv, dabase FROM info.tvrtke as tvrt inner join instalacije as i where
            i.instalacije_tvrtka = tvrt.idtvrtke and i.instalacije_hwid = '" + Globals.cpuid + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.tvrtke_naziv = reader(0)
                TempResult.dabase = reader(1)
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

    Public Function vratiObjekte()
        Dim result = New List(Of ReturnList)
        Try
            ManageConnection(False, konekcija) 'Open connection
            Dim strQuery As String = "SELECT objekti_naziv, objekti_adresa FROM info.objekti as o inner join instalacije as i where 
            i.instalacije_tvrtka = o.tvrtka and i.instalacije_hwid =  '" + Globals.cpuid + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.objekti_naziv = reader(0)
                TempResult.objekti_adresa = reader(1)
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

    Public Function vratiPrograme()
        Dim result = New List(Of ReturnList)
        Try
            ManageConnection(False, konekcija) 'Open connection
            Dim strQuery As String = "SELECT objekti_naziv, objekti_adresa FROM info.objekti where tvrtka  = '" + Globals.tvrtka + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.objekti_naziv = reader(0)
                TempResult.objekti_adresa = reader(1)
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
        Public Property tvrtke_naziv As String
        Public Property dabase As String
        Public Property objekti_naziv As String
        Public Property objekti_adresa As String
        'MP TIPKE

    End Class
End Class

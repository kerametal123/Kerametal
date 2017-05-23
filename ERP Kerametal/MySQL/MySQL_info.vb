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

    Public Function maloprodajaIzbornik()
        Dim result = New List(Of ReturnList)
        Try
            ManageConnection(False, konekcija) 'Open connection
            Dim strQuery As String = "SELECT * FROM info.opcije_mp as mp inner join instalacije as i where mp.idopcije_mp = i.opcijeMP and i.instalacije_hwid = '" + Globals.tvrtka + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.prodajaBtn = reader(3)
                TempResult.kalkulacijeBtn = reader(4)
                TempResult.zaduzniceBtn = reader(5)
                TempResult.predisponacijeBtn = reader(6)
                TempResult.robnoBtn = reader(7)
                TempResult.KUFBtn = reader(8)
                TempResult.KIFBtn = reader(9)
                TempResult.NarudzbeniceBTN = reader(10)
                TempResult.naloziBtn = reader(11)
                TempResult.ACBtn = reader(12)
                TempResult.SRBtn = reader(13)
                TempResult.ostalo1Btn = reader(14)
                TempResult.ostalo2Btn = reader(15)
                TempResult.ostalo3Btn = reader(16)
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
        Public Property prodajaBtn As String
        Public Property kalkulacijeBtn As String
        Public Property zaduzniceBtn As String
        Public Property predisponacijeBtn As String
        Public Property robnoBtn As String
        Public Property KUFBtn As String
        Public Property KIFBtn As String
        Public Property NarudzbeniceBTN As String
        Public Property naloziBtn As String
        Public Property ACBtn As String
        Public Property SRBtn As String
        Public Property ostalo1Btn As String
        Public Property ostalo2Btn As String
        Public Property ostalo3Btn As String
    End Class
End Class

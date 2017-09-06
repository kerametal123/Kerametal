Imports System.Data
Imports MySql.Data.MySqlClient

Public Class MySQLinfo
    Dim xmlinfo As New XMLinfo
    Dim dbCon As MySqlConnection
    Dim konekcija As String = "Server=127.0.0.1;Database=info;Uid=root;Pwd=samael89;Pooling=false"
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
                Console.WriteLine("Zatvaram konekciju -----------------------------------")
                MySqlConnection.ClearAllPools()
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
            ManageConnection(False, konekcija)

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
            Dim strQuery As String = "SELECT `" + variabla + "` FROM info.instalacije WHERE `instalacije_hwid` = '" + hwid + "'"
            'MessageBox.Show(strQuery)
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
    'Logins
    Public Function login()
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "SELECT instalacije_login FROM info.instalacije where instalacije_hwid = '" + Globals.cpuid + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()

            While reader.Read()
                If reader.GetString(0) = "1" Then
                    Return True
                ElseIf reader.GetString(0) = "0" Then
                    Return False
                End If
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija)
        End Try
        Return True
    End Function

    Public Function loginUser(ByVal username As String, ByVal password As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "SELECT count(*) FROM users where iduser = '" + username + "' and lozinka = '" + password + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()

            While reader.Read()
                If reader.GetString(0) = "1" Then
                    Return True
                ElseIf reader.GetString(0) = "0" Then
                    Return False
                End If
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija)
        End Try
        Return True
    End Function
    Public Function podaciInstalacija(ByVal hwid As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "SELECT instalacije_objekt FROM instalacije where instalacije_hwid = '" + Globals.cpuid + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()

            While reader.Read()
                Globals.objekt = reader.GetString(0)
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija)
        End Try
        Return True
    End Function
    Public Function infoInstalacije(ByVal hwid As String)
        Dim hardware As String = hwid
        Dim result = New List(Of String)()
        Try
            Dim strQuery As String
            ManageConnection(False, konekcija) 'Open connection'
            If Globals.login = False Then
                strQuery = "SELECT  i.defaultProg,i.godina, i.defaultProg, o.objekti_naziv,t.tvrtke_naziv,i.idinstalacije, i.instalacije_hwid, 
            i.instalacije_naziv, i.instalacije_login, i.instalacije_tvrtka, t.dabase, p.postavke_naziv, p.postavke_db1, p.postavke_db2,
            p.postavke_db3, p.postavke_db4, o.idobjekti, o.objekti_veza, op.* FROM info.instalacije as i inner join info.tvrtke as t inner join 
            opcije_mp as op inner join info.objekti as o inner join info.sqlpostavke as p where i.instalacije_tvrtka = t.idtvrtke and 
            p.idpostavke = i.instalacija_postavke and o.idobjekti = i.instalacije_objekt and instalacije_hwid ='" + Globals.cpuid + "';"
            ElseIf Globals.login = True Then
                strQuery = "SELECT i.defaultProg, i.godina, i.defaultProg, o.objekti_naziv,t.tvrtke_naziv,i.iduser as idinstalacije, i.username, 
                i.ime as instalacije_naziv, i.login, i.tvrtka as instalacije_tvrtka, t.dabase, p.postavke_naziv, p.postavke_db1, p.postavke_db2,
                p.postavke_db3, p.postavke_db4, o.idobjekti, o.objekti_veza, op.* FROM info.users as i inner join info.tvrtke as t inner join 
                opcije_mp as op inner join info.objekti as o inner join info.sqlpostavke as p where i.tvrtka = t.idtvrtke and 
                p.idpostavke = i.postavke and o.idobjekti = i.objekt and i.iduser ='" + Globals.iduser + "';"
            End If
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Globals.programAktivni = reader.GetString("defaultProg")
                Globals.aktivnaGodina = reader.GetString("godina")
                Globals.objekt = reader.GetString("idobjekti")
                Globals.tvrtka_naziv = reader.GetString("tvrtke_naziv")
                Globals.tvrtka = reader.GetString("instalacije_tvrtka")
                Globals.objekt_naziv = reader.GetString("objekti_naziv")
                Globals.racunalo_naziv = reader.GetString("instalacije_naziv")
                Globals.defaultProg = reader.GetString("defaultProg")
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

    ''' <summary>
    ''' 'Obsolete?
    ''' </summary>
    ''' <returns></returns>
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

    Public Function vratiTvrtke()
        Dim result = New List(Of ReturnList)
        Try
            Dim strQuery As String
            ManageConnection(False, konekcija) 'Open connection'
            If Globals.login = False Then
                strQuery = "SELECT tvrt.tvrtke_naziv, tvrt.dabase, tvrt.idtvrtke FROM info.tvrtke as tvrt  inner join instalacije as i inner join opcije_tvrtke as optv where
             i.instalacije_hwid = '" + Globals.cpuid + "' and optv.racunalo = '" + Globals.cpuid + "' and optv.tvrtka = tvrt.idtvrtke;"
            ElseIf Globals.login = True Then
                strQuery = "SELECT tvrt.tvrtke_naziv, tvrt.dabase, tvrt.idtvrtke FROM info.tvrtke as tvrt  inner join users as i inner join opcije_tvrtke as optv where
             i.iduser = '" + Globals.iduser + "' and optv.korisnik = '" + Globals.iduser + "' and optv.tvrtka = tvrt.idtvrtke;"
            End If


            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            'While reader.Read()

            'End While
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.tvrtke_naziv = reader(0)
                TempResult.dabase = reader(1)
                TempResult.tvrtke_id = reader(2)
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
            Dim strQuery As String
            ManageConnection(False, konekcija) 'Open connection
            If Globals.login = False Then
                strQuery = "SELECT distinct objekti_naziv, objekti_adresa, idobjekti, idobjekti FROM info.objekti as obj inner join opcije_programa as o inner join instalacije as i inner join opcije_objekta as opob inner join opcije_godina as opog where 
            obj.tvrtka = '" + Globals.tvrtka + "' and opob.racunalo = '" + Globals.cpuid + "' and opob.objekt = idobjekti and obj.vrstaObjekta = '" + Globals.programAktivni + "' and opob.godina = '" + Globals.aktivnaGodina + "'"
            ElseIf Globals.login = True Then
                strQuery = "SELECT distinct objekti_naziv, objekti_adresa, idobjekti, idobjekti FROM info.objekti as obj inner join opcije_programa as o inner join users as i inner join opcije_objekta as opob inner join opcije_godina as opog where 
            obj.tvrtka = '" + Globals.tvrtka + "' and opob.korisnik = '" + Globals.iduser + "' and opob.objekt = idobjekti and obj.vrstaObjekta = '" + Globals.programAktivni + "' and opob.godina = '" + Globals.aktivnaGodina + "'"
            End If

            Console.Write(strQuery)
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.objekti_naziv = reader(0)
                TempResult.objekti_adresa = reader(1)
                TempResult.objekti_id = reader(2)
                TempResult.idobjekti = reader(3)
                'TempResult.vrstaObjekta = reader(3)
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
    Public Function vratiObjekteSve()
        Dim result = New List(Of ReturnList)
        Try
            ManageConnection(False, konekcija) 'Open connection
            Dim strQuery As String = "SELECT distinct objekti_naziv, objekti_adresa, idobjekti, idobjekti FROM info.objekti"
            ' MessageBox.Show(strQuery)
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.objekti_naziv = reader(0)
                TempResult.objekti_adresa = reader(1)
                TempResult.objekti_id = reader(2)
                TempResult.idobjekti = reader(3)
                'TempResult.vrstaObjekta = reader(3)
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
    Public Function vratiTvrtkeSve()
        Dim result = New List(Of ReturnList)
        Try
            Dim strQuery As String = "SELECT tvrtke_naziv, dabase, idtvrtke FROM tvrtke;"
            ManageConnection(False, konekcija) 'Open connection'
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            'While reader.Read()

            'End While
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.tvrtke_naziv = reader(0)
                TempResult.dabase = reader(1)
                TempResult.tvrtke_id = reader(2)
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
    Public Function vratiTipoveKorisnika()
        Dim result = New List(Of ReturnList)
        Try
            Dim strQuery As String = "SELECT idtipovi_korisnika, naziv FROM tipovi_korisnika;"
            ManageConnection(False, konekcija) 'Open connection'
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            'While reader.Read()

            'End While
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.idtipovi = reader(0)
                TempResult.naziv_tipa = reader(1)
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
    Public Function vratiGodine()
        Dim result = New List(Of ReturnList)
        Try
            ManageConnection(False, konekcija) 'Open connection
            Dim strQuery As String
            If Globals.login = False Then
                strQuery = "SELECT distinct go.stringname, g.godina, g.stringname FROM info.opcije_godina as g inner join opcije_tvrtke as t inner join godine as go   where 
            t.racunalo = '" + Globals.cpuid + "' and t.tvrtka = '" + Globals.tvrtka + "' and g.tvrtka = t.tvrtka and go.idgodine = g.godina"
            ElseIf Globals.login = True Then
                strQuery = "SELECT distinct go.stringname, g.godina, g.stringname FROM info.opcije_godina as g inner join opcije_tvrtke as t inner join godine as go   where 
            t.korisnik = '" + Globals.iduser + "' and t.tvrtka = '" + Globals.tvrtka + "' and g.tvrtka = t.tvrtka and go.idgodine = g.godina"
            End If

            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.godina = reader(0)
                TempResult.idopcije_godina = reader(1)
                TempResult.stringname_god = reader(2)
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
            Dim strQuery As String
            If Globals.login = False Then
                strQuery = "SELECT naziv_programa, tabela, idprogrami, p.vrstaPrograma FROM info.programi as p INNER JOIN opcije_programa as op WHERE op.korisnik = '" + Globals.cpuid + "' AND p.idprogrami = op.program and op.tvrtka = '" + Globals.tvrtka + "' and op.godina = '" + Globals.aktivnaGodina + "';"
            ElseIf Globals.login = True Then
                strQuery = "SELECT naziv_programa, tabela, idprogrami, p.vrstaPrograma FROM info.programi as p INNER JOIN opcije_programa as op WHERE op.korisnik = '" + Globals.iduser + "' AND p.idprogrami = op.program and op.tvrtka = '" + Globals.tvrtka + "' and op.godina = '" + Globals.aktivnaGodina + "';"
            End If
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.naziv_programa = reader(0)
                TempResult.tabela = reader(1)
                TempResult.idprogrami = reader(2)
                TempResult.vrstaPrograma = reader(3)
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
    Public Function vratiOpcijePrograma(ByVal tabela As String)
        Dim result As New ArrayList
        Try
            ManageConnection(False, konekcija) 'Open connection
            Dim strQuery As String = "SELECT op1,op2,op3,op4,op5,op6,op7,op8,op9,op10,op11,op12,op13,op14,op15 FROM info." + tabela + " where instalacija = '" + Globals.cpuid + "' and tvrtka = '" + Globals.tvrtka + "' and objekt = '" + Globals.objekt + "' and godina = '" + Globals.aktivnaGodina + "';"
            'Dim strQuery2 As String = "SELECT i.instalacije_naziv, t.tvrtke_naziv, t.dabase, i.instalacije_aktivnost, i.instalacije_login, i.instalacija_postavke, o.objekti_naziv, i.opcijeMP as MP, i.opcijeVP as VP, i.OpcijeFK as FK, i.OpcijeUG as UG FROM info.instalacije as i inner join tvrtke as t inner join objekti as o where t.idtvrtke = i.instalacije_tvrtka and o.idobjekti = i.instalacije_objekt and i.instalacije_hwid = 'BFEBFBFF000306A9';"
            ' Dim strQuery3 As String = "UPDATE events SET realchannelname = :value, chanid = :channelid WHERE eventname = :configname and datetime =:datetime and twchannel =:twchannel ;"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                result.Add(reader(0))
                result.Add(reader(1))
                result.Add(reader(2))
                result.Add(reader(3))
                result.Add(reader(4))
                result.Add(reader(5))
                result.Add(reader(6))
                result.Add(reader(7))
                result.Add(reader(8))
                result.Add(reader(9))
                result.Add(reader(10))
                result.Add(reader(11))
                result.Add(reader(12))
                result.Add(reader(13))
                result.Add(reader(14))
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija) 'Close connection
        End Try
        Return result
    End Function
    Public Function vratiDatoteke(ByVal tabela As String)
        Dim result As New ArrayList
        Try
            ManageConnection(False, konekcija) 'Open connection
            Dim strQuery As String = "SELECT dat1,dat2,dat3,dat4,dat5,dat6,dat7,dat8,dat9,dat10,dat11,dat12,dat13,dat14,dat15 FROM info." + tabela + " where instalacija = '" + Globals.cpuid + "' and tvrtka = '" + Globals.tvrtka + "' and objekt = '" + Globals.objekt + "' and godina = '" + Globals.aktivnaGodina + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                result.Add(reader(0))
                result.Add(reader(1))
                result.Add(reader(2))
                result.Add(reader(3))
                result.Add(reader(4))
                result.Add(reader(5))
                result.Add(reader(6))
                result.Add(reader(7))
                result.Add(reader(8))
                result.Add(reader(9))
                result.Add(reader(10))
                result.Add(reader(11))
                result.Add(reader(12))
                result.Add(reader(13))
                result.Add(reader(14))
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija) 'Close connection
        End Try
        Return result
    End Function

    Public Function dozvolePostavki()
        Dim postavke = New List(Of String)()
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "Select unosIspravkeMenu,blagProd,faktureMenu,printAfterMenu,blagProd1,faktureMenu1,
otpremniceMenu1,mogFaktVP,pregledRuc,brojila,otptofakt,skupniBtn,dofakturiranje,nedostatneKolicine,fiscalMenuBtn,nonfiscalMenuBtn,a4RacunBtn,
ljetnoVrijemeBtn,zimskoVrijemeBtn,ulazNovcaBtn,izlazNovcaBtn FROM info.opcije_mp where instalacija = '" + Globals.cpuid + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                postavke.Add(reader.GetString("unosIspravkeMenu"))
                postavke.Add(reader.GetString("blagProd"))
                postavke.Add(reader.GetString("faktureMenu"))
                postavke.Add(reader.GetString("printAfterMenu"))
                postavke.Add(reader.GetString("blagProd1"))
                postavke.Add(reader.GetString("faktureMenu1"))
                postavke.Add(reader.GetString("otpremniceMenu1"))
                postavke.Add(reader.GetString("mogFaktVP"))
                postavke.Add(reader.GetString("pregledRuc"))
                postavke.Add(reader.GetString("brojila"))
                postavke.Add(reader.GetString("otptofakt"))
                postavke.Add(reader.GetString("skupniBtn"))
                postavke.Add(reader.GetString("dofakturiranje"))
                postavke.Add(reader.GetString("nedostatneKolicine"))
                postavke.Add(reader.GetString("fiscalMenuBtn"))
                postavke.Add(reader.GetString("nonfiscalMenuBtn"))
                postavke.Add(reader.GetString("a4RacunBtn"))
                postavke.Add(reader.GetString("ljetnoVrijemeBtn"))
                postavke.Add(reader.GetString("zimskoVrijemeBtn"))
                postavke.Add(reader.GetString("ulazNovcaBtn"))
                postavke.Add(reader.GetString("izlazNovcaBtn"))
            End While
            reader.Close()
            'Vraća podatke u Listi stringova
            Return postavke
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
            Return Nothing
        Finally
            ManageConnection(True, konekcija)
        End Try

    End Function

    'Admin part

    Public Function getAdminInfo()
        Dim postavke = New List(Of String)()
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "SELECT instalacije_naziv, t.tvrtke_naziv, instalacije_aktivnost, instalacije_login,
o.objekti_naziv, opcijeMP,
opcijeVP, OpcijeFK, opcijeUG, godina, instalacije_preset
FROM info.instalacije inner join tvrtke as t inner join objekti as o where o.idobjekti = instalacije_objekt and instalacije_tvrtka = t.idtvrtke and instalacije_hwid = '" + Globals.cpuid + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                postavke.Add(reader.GetString("instalacije_naziv"))
                postavke.Add(reader.GetString("tvrtke_naziv"))
                postavke.Add(reader.GetString("instalacije_aktivnost"))
                postavke.Add(reader.GetString("instalacije_login"))
                postavke.Add(reader.GetString("objekti_naziv"))
                postavke.Add(reader.GetString("opcijeMP"))
                postavke.Add(reader.GetString("opcijeVP"))
                postavke.Add(reader.GetString("OpcijeFK"))
                postavke.Add(reader.GetString("opcijeUG"))
                postavke.Add(reader.GetString("godina"))
                postavke.Add(reader.GetString("preset"))
            End While
            reader.Close()
            'Vraća podatke u Listi stringova
            Return postavke
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
            Return Nothing
        Finally
            ManageConnection(True, konekcija)
        End Try

    End Function
    Public Function radSaKontrolom(ByVal ime As String, ByVal vrijednost As String, ByVal postavka As String)
        If provjeriKontrolu(ime) = True Then
            Try
                ManageConnection(False, konekcija) 'Open connection'
                Dim strQuery As String = "UPDATE `info`.`kontrole` SET `" + postavka + "`='" + vrijednost + "' WHERE `hwid`='" + Globals.cpuid + "' and `imeKontrole` = '" + ime + "';"
                Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
                SqlCmd.ExecuteNonQuery()

                ManageConnection(True, konekcija) 'Close connection'

            Catch ex As Exception
                Return False
                MsgBox("Error " & ex.Message)
            End Try
            Return True
        Else
            Try
                ManageConnection(False, konekcija) 'Open connection'
                Dim strQuery As String = "INSERT INTO `info`.`kontrole` (`imeKontrole`, `hwid`,`" + postavka + "`) VALUES ('" + ime + "', '" + Globals.cpuid + "', '" + vrijednost + "');"
                Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
                SqlCmd.ExecuteNonQuery()
                ManageConnection(True, konekcija) 'Close connection'
            Catch ex As Exception
                Return False
                MsgBox("Error " & ex.Message)
            End Try
            Return True
        End If
        ManageConnection(True, konekcija)
    End Function
    Public Function provjeriKontrolu(ByVal imekontrole As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "SELECT * FROM info.kontrole WHERE `hwid` = '" + Globals.cpuid + "' and `imeKontrole` = '" + imekontrole + "'"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                If reader.HasRows Then
                    Return True
                Else
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
    Public Function vratiKontrole()
        Dim query As String = "Select * FROM info.kontrole where hwid = '" + Globals.cpuid + "';"
        Dim table As New DataTable()
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function blokirajTipku(ByVal ime As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'

            Dim strQuery As String = "INSERT INTO `info`.`kontrole` (`imeKontrole`, `hwid`,`enabled`) VALUES ('" + ime + "', '" + Globals.cpuid + "', '0');"

            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function
    Public Function sakrijTipku(ByVal ime As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'

            Dim strQuery As String = "INSERT INTO `info`.`kontrole` (`imeKontrole`, `hwid`,`visible`) VALUES ('" + ime + "', '" + Globals.cpuid + "', '0');"

            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function
    Public Function otkrijTipku(ByVal ime As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "INSERT INTO `info`.`kontrole` (`imeKontrole`, `hwid`,`visible`) VALUES ('" + ime + "', '" + Globals.cpuid + "', '1');"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()
            ManageConnection(True, konekcija) 'Close connection'
        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function
    Public Function deblokirajTipku(ByVal ime As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "INSERT INTO `info`.`kontrole` (`imeKontrole`, `hwid`,`enabled`) VALUES ('" + ime + "', '" + Globals.cpuid + "', '1');"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()
            ManageConnection(True, konekcija) 'Close connection'
        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function

    Public Function getFiscalPostavke()
        Dim result = New List(Of ReturnList)
        Try
            ManageConnection(False, konekcija) 'Open connection
            Dim strQuery As String = "SELECT fiscalIn, fiscalOut, IOSA FROM racunala WHERE `racunala_hwid` = '" + Globals.cpuid + "'"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                Globals.fiscalIn = reader(0)
                Globals.fiscalOut = reader(1)
                Globals.iosa = reader(2)
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija) 'Close connection
        End Try
    End Function

    Public Function getKorisnici(ByVal strict As Boolean)
        Dim query1 As String
        If strict = True Then
            query1 = "SELECT * FROM info.users where objekt = '" + Globals.objekt + "';"
            Console.Write(query1)
        Else
            query1 = "SELECT * FROM info.users;"
        End If



        Dim table As New DataTable
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query1, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function getInstalacije(ByVal strict As Boolean)
        Dim query1 As String
        If strict = True Then
            query1 = "SELECT * FROM info.instalacije where instalacije_objekt = '" + Globals.objekt + "';"
            Console.Write(query1)
        Else
            query1 = "SELECT * FROM info.instalacije;"
        End If



        Dim table As New DataTable
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query1, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function getTvrtke(ByVal strict As Boolean)
        Dim query1 As String
        If strict = True Then
            query1 = "SELECT * FROM info.tvrtke where aktivnost = '1';"
            Console.Write(query1)
        Else
            query1 = "SELECT * FROM info.tvrtke;"
        End If
        Dim table As New DataTable
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query1, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function

    Public Function getObjekti(ByVal tvrtka As String, ByVal all As Boolean, ByVal tip As String)
        Dim query1 As String
        If all = True Then
            query1 = "SELECT * FROM info.objekti;"
        ElseIf all = False Then
            query1 = "SELECT * FROM info.objekti where tvrtka = '" + tvrtka + "' and vrstaObjekta ='" + tip + "';"
            'MessageBox.Show(query1)
        End If

        Dim table As New DataTable
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query1, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function getProgrami()
        Dim query1 As String
        query1 = "SELECT * FROM info.programi;"
        Dim table As New DataTable
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query1, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function getTipPrograma(ByVal idprograma As String)

        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "SELECT vrstaPrograma FROM info.programi where idprogrami ='" + idprograma + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()

            While reader.Read()
                Return reader.GetString(0)

            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija)
        End Try
        Return True
    End Function
    Public Function getTabelaPrograma(ByVal idprograma As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "SELECT tabela FROM info.programi where idprogrami ='" + idprograma + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()

            While reader.Read()
                Return reader.GetString(0)

            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija)
        End Try
        Return True
    End Function
    Public Function getGodine()
        Dim query1 As String
        query1 = "SELECT * FROM info.godine;"
        Dim table As New DataTable
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query1, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function getKorisnik(ByVal id As String)
        Dim query1 As String = "SELECT * FROM info.users where iduser='" + id + "';"
        Dim table As New DataTable
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query1, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function getOpcije(ByVal tabela As String, ByVal user As String)
        Dim query1 As String
        query1 = "SELECT * FROM " + tabela + ";"
        Dim table As New DataTable
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query1, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function

    Public Function getDefaults(ByVal iduser As String)
        Dim query1 As String
        query1 = "SELECT objekt, tvrtka, aktivnost, login, preset, mp,vp,ug,fk, defaultProg, godina FROM info.users where iduser ='" + iduser + "';"
        Dim table As New DataTable
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query1, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function
    Public Function getDetaljnoOpcije(ByVal iduser As String, ByVal tvrtka As String, ByVal godina As String, ByVal objekt As String, ByVal tabela As String)
        Dim result As New ArrayList
        Try
            ManageConnection(False, konekcija) 'Open connection
            Dim strQuery As String = "SELECT op1,op2,op3,op4,op5,op6,op7,op8,op9,op10,op11,op12,op13,op14,op15 FROM " + tabela + "  where korisnik ='" + iduser + "' and tvrtka ='" + tvrtka + "' and godina ='" + godina + "' and objekt ='" + objekt + "';"
            'Dim strQuery2 As String = "SELECT i.instalacije_naziv, t.tvrtke_naziv, t.dabase, i.instalacije_aktivnost, i.instalacije_login, i.instalacija_postavke, o.objekti_naziv, i.opcijeMP as MP, i.opcijeVP as VP, i.OpcijeFK as FK, i.OpcijeUG as UG FROM info.instalacije as i inner join tvrtke as t inner join objekti as o where t.idtvrtke = i.instalacije_tvrtka and o.idobjekti = i.instalacije_objekt and i.instalacije_hwid = 'BFEBFBFF000306A9';"
            ' Dim strQuery3 As String = "UPDATE events SET realchannelname = :value, chanid = :channelid WHERE eventname = :configname and datetime =:datetime and twchannel =:twchannel ;"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                result.Add(reader(0))
                result.Add(reader(1))
                result.Add(reader(2))
                result.Add(reader(3))
                result.Add(reader(4))
                result.Add(reader(5))
                result.Add(reader(6))
                result.Add(reader(7))
                result.Add(reader(8))
                result.Add(reader(9))
                result.Add(reader(10))
                result.Add(reader(11))
                result.Add(reader(12))
                result.Add(reader(13))
                result.Add(reader(14))
            End While
            reader.Close()
        Catch ex As MySqlException
            Console.WriteLine("Error: " & ex.ToString())
        Finally
            ManageConnection(True, konekcija) 'Close connection
        End Try
        Return result

    End Function
    Public Function setKorisnik(ByVal ime As String, ByVal prezime As String, ByVal telefon As String, ByVal email As String, ByVal tvrtka As String, ByVal objekt As String, ByVal tip As String, ByVal username As String, ByVal lozinka As String, ByVal iduser As String, ByVal preset As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "UPDATE `info`.`users` SET `username`='" + username + "', `email`='" + email + "', `ime`='" + ime + "', `prezime`='" + prezime + "', `telefon`='" + telefon + "',`postavke`='" + preset + "', `objekt`='" + objekt + "', `tip_korisnika`='" + tip + "', `lozinka`='" + lozinka + "' WHERE `iduser`='" + iduser + "';"
            Console.Write(strQuery)
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function
    Public Function updateObjekt(ByVal naziv As String, ByVal adresa As String, ByVal veza As String, ByVal tvrtka As String, ByVal vrsta As String, ByVal sifra As String, ByVal telefon As String, ByVal fax As String, ByVal email As String, ByVal web As String, ByVal aktivnost As String, ByVal id As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "UPDATE `info`.`objekti` SET `objekti_naziv`='" + naziv + "', `objekti_adresa`='" + adresa + "', `objekti_veza`='" + veza + "', `tvrtka`='" + tvrtka + "', `vrstaObjekta`='" + vrsta + "', `sifraObjekta`='" + sifra + "', `objekti_telefon`='" + telefon + "', `objekti_fax`='" + fax + "', `objekti_email`='" + email + "', `objekti_web`='" + web + "', `aktivnost`='" + aktivnost + "' WHERE `idobjekti`='" + id + "';"
            Console.Write(strQuery)
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function
    Public Function insertObjekti(ByVal naziv As String, ByVal adresa As String, ByVal veza As String, ByVal tvrtka As String, ByVal vrsta As String, ByVal sifra As String, ByVal telefon As String, ByVal fax As String, ByVal email As String, ByVal web As String, ByVal aktivnost As String, ByVal id As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'

            Dim strQuery As String = "INSERT INTO `info`.`objekti` (`objekti_naziv`, `objekti_adresa`, `objekti_veza`, `tvrtka`, `vrstaObjekta`, `sifraObjekta`, `objekti_telefon`, `objekti_fax`, `objekti_email`, `objekti_web`, `aktivnost`) VALUES ('" + naziv + "', '" + adresa + "', '" + veza + "', '" + tvrtka + "', '" + vrsta + "', '" + sifra + "', '" + telefon + "', '" + fax + "', '" + email + "', '" + web + "', '" + aktivnost + "');"
            Console.WriteLine(strQuery)
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function
    Public Function getOpcije(ByVal iduser As String, ByVal tvrtka As String, ByVal godina As String, ByVal objekt As String, ByVal tabela As String)

        Dim query1 As String
        query1 = "SELECT * FROM " + tabela + "  where korisnik ='" + iduser + "' and tvrtka ='" + tvrtka + "' and godina ='" + godina + "' and objekt ='" + objekt + "';"
        Dim table As New DataTable
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query1, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using

    End Function
    Public Function getOpcijePresets(ByVal tvrtka As String, ByVal godina As String, ByVal objekt As String, ByVal tabela As String)

        Dim query1 As String
        query1 = "SELECT * FROM " + tabela + "  where  tvrtka ='" + tvrtka + "' and godina ='" + godina + "' and objekt ='" + objekt + "';"
        Dim table As New DataTable
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query1, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using

    End Function
    Public Function setKorisnikPocetnePostavke(ByVal objekt As String, ByVal tvrtka As String, ByVal defaultProg As String, ByVal godina As String, ByVal user As String, ByVal lozinka As String, ByVal korisnicko As String, ByVal tip As String, ByVal email As String, ByVal telefon As String, ByVal ime As String, ByVal prezime As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "UPDATE `info`.`users` SET `objekt`='" + objekt + "', `tvrtka`='" + tvrtka + "', `defaultProg`='" + defaultProg + "', `godina`='" + godina + "', `lozinka`='" + lozinka + "', `username`='" + korisnicko + "', `tip_korisnika`='" + tip + "', `email`='" + email + "', `telefon`='" + telefon + "', `ime`='" + ime + "', `prezime`='" + prezime + "'  WHERE `iduser`='" + user + "';"
            Console.Write(strQuery)
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function
    Public Function duplicirajOpciju(ByVal tabela As String, ByVal tvrtka As String, ByVal godina As String, ByVal objekt As String, ByVal uid As String, ByVal name As String, ByVal idopcije As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'

            Dim strQuery As String = "INSERT INTO " + tabela + " (`tvrtka`,`godina`,`objekt`,`korisnik`,`naziv`,`opc`,`op1`,`op2`,`op3`,`op4`,`op5`,`op6`,`op7`,`op8`,`op9`,`op10`,`op11`,`op12`,`op13`,`op14`,`op15`,`dat1`,`dat2`,`dat3`,`dat4`,`dat5`,`dat6`,`dat7`,`dat8`,`dat9`,`dat10`,`dat11`,`dat12`,`dat13`,`dat14`,`dat15`,`datoteke3`,`datoteke4`,`datoteke5`,`unosIspravkeMenu`,`blagProd`,`faktureMenu`,`printAfterMenu`,`blagProd1`,`faktureMenu1`,`otpremniceMenu1`,`mogFaktVp`,`brojila`,`otptofakt`,`skupniBtn`,`dofakturiranje`,`nedostatneKolicine`,`fiscalMenuBtn`,`nonfiscalMenuBtn`,`a4RacunBtn`,`ljetnoVrijemeBtn`,`zimskoVrijemeBtn`,`ulazNovcaBtn`,`izlazNovcaBtn`)
SELECT " + tvrtka + "," + godina + "," + objekt + "," + uid + ",'" + name + "',`opc`,`op1`,`op2`,`op3`,`op4`,`op5`,`op6`,`op7`,`op8`,`op9`,`op10`,`op11`,`op12`,`op13`,`op14`,`op15`,`dat1`,`dat2`,`dat3`,`dat4`,`dat5`,`dat6`,`dat7`,`dat8`,`dat9`,`dat10`,`dat11`,`dat12`,`dat13`,`dat14`,`dat15`,`datoteke3`,`datoteke4`,`datoteke5`,`unosIspravkeMenu`,`blagProd`,`faktureMenu`,`printAfterMenu`,`blagProd1`,`faktureMenu1`,`otpremniceMenu1`,`mogFaktVp`,`brojila`,`otptofakt`,`skupniBtn`,`dofakturiranje`,`nedostatneKolicine`,`fiscalMenuBtn`,`nonfiscalMenuBtn`,`a4RacunBtn`,`ljetnoVrijemeBtn`,`zimskoVrijemeBtn`,`ulazNovcaBtn`,`izlazNovcaBtn` FROM " + tabela + " WHERE naziv = '" + idopcije + "';"
            Console.WriteLine(strQuery)
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function
    Public Class ReturnList
        Public Property tvrtke_naziv As String
        Public Property dabase As String
        Public Property objekti_naziv As String
        Public Property objekti_adresa As String
        Public Property idobjekti As String
        Public Property naziv_programa As String
        Public Property vrstaObjekta As String
        Public Property objekti_id As String
        Public Property tvrtke_id As String
        Public Property tabela As String
        Public Property idprogrami As String
        Public Property vrstaPrograma As String
        'PROGRAMI
        Public Property program1 As String
        Public Property program2 As String
        Public Property program3 As String
        Public Property program4 As String
        Public Property program5 As String
        Public Property program6 As String
        Public Property program7 As String
        Public Property program8 As String
        Public Property program9 As String
        Public Property program10 As String
        Public Property program11 As String
        Public Property program12 As String
        Public Property program13 As String
        Public Property program14 As String
        Public Property program15 As String
        'DATOTEKE
        Public Property datoteka1 As String
        Public Property datoteka2 As String
        Public Property datoteka3 As String
        Public Property datoteka4 As String
        Public Property datoteka5 As String
        Public Property datoteka6 As String
        Public Property datoteka7 As String
        Public Property datoteka8 As String
        Public Property datoteka9 As String
        Public Property datoteka10 As String
        Public Property datoteka11 As String
        Public Property datoteka12 As String
        Public Property datoteka13 As String
        Public Property datoteka14 As String
        Public Property datoteka15 As String
        Public Property tipkeMP As Array
        'Godine
        Public Property godina As String
        Public Property idopcije_godina As String
        Public Property stringname_god As String


        Public Property idtipovi As String
        Public Property naziv_tipa As String
    End Class
End Class

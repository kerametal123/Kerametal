Imports System.Data
Imports MySql.Data.MySqlClient

Public Class MySQLinfo
    Dim xmlinfo As New XMLinfo
    Dim dbCon As MySqlConnection
    Dim konekcija As String = "Server=127.0.0.1;Database=info;Uid=root;Pwd=samael89;"
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

    Public Function login()
        Try
            ManageConnection(False, konekcija) 'Open connection'

            Dim strQuery As String = "SELECT instalacije_login FROM info.instalacije where instalacije_hwid = '" + Globals.cpuid + "';"
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

    End Function
    Public Function test()
        MessageBox.Show(podaciInstalacija("dasda"))
    End Function
    Public Function podaciInstalacija(ByVal hwid As String)
        Dim hardware As String = hwid
        Dim result = New List(Of String)()
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "SELECT  o.objekti_naziv,t.tvrtke_naziv,i.idinstalacije,
            i.instalacije_hwid, i.instalacije_naziv, i.instalacije_login, i.instalacije_tvrtka, t.dabase, p.postavke_naziv, 
            p.postavke_db1, p.postavke_db2, p.postavke_db3, p.postavke_db4, o.sifraObjekta, o.objekti_veza, op.* FROM info.instalacije as 
            i inner join info.tvrtke as t inner join opcije_mp as op inner join info.objekti as o inner join info.sqlpostavke as p where
            i.instalacije_tvrtka = t.idtvrtke and p.idpostavke = i.instalacija_postavke and o.sifraObjekta = i.instalacije_objekt and 
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
            Dim strQuery As String = "SELECT  i.defaultProg,i.godina, i.defaultProg, o.objekti_naziv,t.tvrtke_naziv,i.idinstalacije, i.instalacije_hwid, 
            i.instalacije_naziv, i.instalacije_login, i.instalacije_tvrtka, t.dabase, p.postavke_naziv, p.postavke_db1, p.postavke_db2,
            p.postavke_db3, p.postavke_db4, o.sifraObjekta, o.objekti_veza, op.* FROM info.instalacije as i inner join info.tvrtke as t inner join 
            opcije_mp as op inner join info.objekti as o inner join info.sqlpostavke as p where i.instalacije_tvrtka = t.idtvrtke and 
            p.idpostavke = i.instalacija_postavke and o.sifraObjekta = i.instalacije_objekt and instalacije_hwid = '" + Globals.cpuid + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Globals.programAktivni = reader.GetString("defaultProg")
                Globals.aktivnaGodina = reader.GetString("godina")
                Globals.objekt = reader.GetString("sifraObjekta")
                Globals.tvrtka_naziv = reader.GetString("tvrtke_naziv")
                Globals.tvrtka = reader.GetString("instalacije_tvrtka")
                Globals.objekt_naziv = reader.GetString("objekti_naziv")
                Globals.racunalo_naziv = reader.GetString("instalacije_naziv")
                Globals.defaultProg = reader.GetString("defaultProg")
                xmlinfo.infoXMLdb(reader.GetString("sifraObjekta"), reader.GetString("objekti_naziv"),
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
        p.postavke_db3, p.postavke_db4, o.sifraObjekta, o.objekti_veza, op.* FROM info.instalacije as i inner join info.tvrtke 
        as t inner join opcije_mp as op inner join info.objekti as o inner join info.sqlpostavke as p where i.instalacije_tvrtka =
        t.idtvrtke and p.idpostavke = i.instalacija_postavke and o.sifraObjekta = i.instalacije_objekt and instalacije_hwid = '" + Globals.cpuid + "';"
        Dim table As New DataTable()
        Using connection As New MySqlConnection(konekcija)
            Using adapter As New MySqlDataAdapter(query, connection)
                adapter.Fill(table)
                Return table
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Foreach
    ''' </summary>
    ''' <returns></returns>
    Public Function vratiTvrtke()
        Dim result = New List(Of ReturnList)
        Try
            Dim strQuery As String = "SELECT tvrt.tvrtke_naziv, tvrt.dabase, tvrt.idtvrtke FROM info.tvrtke as tvrt  inner join instalacije as i inner join opcije_tvrtke as optv where
             i.instalacije_hwid = '" + Globals.cpuid + "' and optv.racunalo = '" + Globals.cpuid + "' and optv.tvrtka = tvrt.idtvrtke;"
            ManageConnection(False, konekcija) 'Open connection'
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
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
    'Vrste plaćanja  
    Public Function getVrstePlacanja()
        Dim query1 As String = "SELECT naziv as Placanja FROM vrste_placanja;"
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
    Public Function vratiObjekte()
        Dim result = New List(Of ReturnList)

        Try
            ManageConnection(False, konekcija) 'Open connection
            Dim strQuery As String = "SELECT distinct objekti_naziv, objekti_adresa, sifraObjekta FROM info.objekti as obj inner join opcije_programa as o inner join instalacije as i inner join opcije_objekta as opob inner join opcije_godina as opog where 
            obj.tvrtka = '" + Globals.tvrtka + "' and opob.racunalo = '" + Globals.cpuid + "' and opob.objekt = sifraObjekta and obj.vrstaObjekta = '" + Globals.programAktivni + "' and opob.godina = '" + Globals.aktivnaGodina + "'"
            ' MessageBox.Show(strQuery)
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                Dim TempResult As New ReturnList
                TempResult.objekti_naziv = reader(0)
                TempResult.objekti_adresa = reader(1)
                TempResult.objekti_id = reader(2)
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
    Public Function vratiGodine()
        Dim result = New List(Of ReturnList)

        Try
            ManageConnection(False, konekcija) 'Open connection
            Dim strQuery As String = "SELECT distinct go.stringname, g.godina, g.stringname FROM info.opcije_godina as g inner join opcije_tvrtke as t inner join godine as go   where 
            t.racunalo = '" + Globals.cpuid + "' and t.tvrtka = '" + Globals.tvrtka + "' and g.tvrtka = t.tvrtka and go.stringname = g.godina"
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
            Dim strQuery As String = "SELECT naziv_programa, tabela, idprogrami, p.vrstaPrograma FROM info.programi as p INNER JOIN opcije_programa as op WHERE op.racunalo = '" + Globals.cpuid + "' AND p.idprogrami = op.program and op.tvrtka = '" + Globals.tvrtka + "' and op.godina = '" + Globals.aktivnaGodina + "';"
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
    Public Function installInfo()
        Try
            infoInstalacije("")

        Catch ex As Exception

        End Try
    End Function
    Public Class ReturnList
        Public Property tvrtke_naziv As String
        Public Property dabase As String
        Public Property objekti_naziv As String
        Public Property objekti_adresa As String
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
    End Class
End Class

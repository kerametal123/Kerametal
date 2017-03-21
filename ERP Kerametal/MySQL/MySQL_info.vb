Imports System.Data
Imports MySql.Data.MySqlClient

Public Class MySQLinfo
    Dim xmlinfo As New XMLinfo
    Dim dbCon As MySqlConnection
    Dim konekcija As String = "Server=80.86.81.211;Database=info;Uid=root;Pwd=samael89;"

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
    Public Function podaciInstalacija(ByVal hwid As String)
        Dim hardware As String = hwid
        Dim result = New List(Of String)()
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "SELECT i.instalacije_objekt,t.dabase, p.postavke_db1,p.postavke_db2,p.postavke_db3,p.postavke_db4 FROM sqlpostavke as p INNER JOIN instalacije as i INNER JOIN tvrtke as t 
                                      where p.idpostavke = i.instalacija_postavke and i.instalacije_tvrtka = t.idtvrtke and i.instalacije_hwid ='" + hardware + "'"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                xmlinfo.infoXMLdb(reader.GetString("dabase"), reader.GetString("postavke_db1"), reader.GetString("postavke_db2"), reader.GetString("postavke_db3"), reader.GetString("postavke_db4"))
                Globals.objekt = reader.GetString("instalacije_objekt")
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
    Public Function podaciGlobal()
        Dim hardware As String = Globals.cpuid
        Dim result = New List(Of String)()
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "SELECT * FROM sqlpostavke where postavke_installid ='" + hardware + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            Dim reader As MySqlDataReader = SqlCmd.ExecuteReader()
            While reader.Read()
                xmlinfo.infoXMLdb("", reader.GetString("postavke_db1"), reader.GetString("postavke_db2"), reader.GetString("postavke_db3"), reader.GetString("postavke_db4"))
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
            ManageConnection(False, konekcija) 'Open connection'

            Dim strQuery As String = "INSERT INTO `info`.`logdata` (`log_naziv`, `log_tip`, `log_hwid`) VALUES ('" + action + "', '" + type + "', '" + Globals.cpuid + "');"

            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function

    Public Function upisWMI(ByVal hddtotal As String, ByVal hddfree As String, ByVal compname As String, ByVal manufacturer As String, ByVal model As String, ByVal osname As String, ByVal osversion As String, ByVal systype As String, ByVal tpm As String, ByVal windir As String)
        Try
            ManageConnection(False, konekcija) 'Open connection'
            Dim strQuery As String = "UPDATE `racunala` SET `hddsize`='" + hddtotal + "', `hddfree`='" + hddfree + "', `compname`='" + compname + "', `manufacturer`='" + manufacturer + "', `model`='" + model + "' , `osname`='" + osname + "', `osversion`='" + osversion + "', `systype`='" + systype + "',  `windir`='" + windir + "', `tpm`='" + tpm + "' WHERE `racunala_hwid`='" + Globals.cpuid + "';"
            Dim SqlCmd As New MySqlCommand(strQuery, dbCon)
            SqlCmd.ExecuteNonQuery()

            ManageConnection(True, konekcija) 'Close connection'

        Catch ex As Exception
            Return False
            MsgBox("Error " & ex.Message)
        End Try
        Return True
    End Function

End Class

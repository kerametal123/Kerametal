Imports System.IO
Public Class Licenciranje
    Dim mysql As New MySQLinfo
    Dim folders As New Folders
    Dim XMLinfo As New XMLinfo
    Dim wminfos As New WMInfo
    Dim punoImePutanje As String = Path.Combine(Globals.rootPath, "Licenca.atkm")
    Dim fileLoc As String = punoImePutanje
    Public Function provjeriLicencuOnline()
        If folders.mkFolderRoot() = True Then


            Dim fs As FileStream = Nothing

            'Ako ne postoji licenca kreiraj je.
            If (Not File.Exists(fileLoc)) Then
                'Kreiranje licence
                If kreirajLicencu(punoImePutanje) = True Then
                    checkActivity()
                ElseIf kreirajLicencu(punoImePutanje) = False Then
                    Return False
                End If
            Else
            End If
        ElseIf folders.mkFolderRoot() = False Then
            checkActivity()
        End If


        Return True
    End Function
    Public Function provjeriLicencuOffline()

    End Function
    Public Function checkActivity()
        Dim fileReader As System.IO.StreamReader
        fileReader =
My.Computer.FileSystem.OpenTextFileReader(punoImePutanje)
        Dim stringReader As String
        stringReader = fileReader.ReadLine()
        'setContact()
        If mysql.provjeraInstalacije(stringReader, "instalacije_aktivnost") = True Then

            mysql.podaciInstalacija(CpuId)
            Globals.cpuid = CpuId()
            XMLinfo.getDbInfo()
            wminfos.upisWmi()

            Return True
        ElseIf mysql.provjeraInstalacije(stringReader, "instalacije_aktivnost") = False Then
            MessageBox.Show("Aplikaciji nije dozvoljen rad!")
            Application.Current.Shutdown()
        End If
    End Function
    Public Function kreirajLicencu(ByVal ffn As String)
        Dim fileLoc As String = ffn
        Dim fs As FileStream = Nothing
        fs = File.Create(fileLoc)
        Using fs
        End Using
        Using sw As StreamWriter = New StreamWriter(fileLoc)
            sw.Write(CpuId)
            dbLicense(CpuId)
        End Using
        Return True

    End Function
    'Poziva funkciju za upis hardware_id u bazu
    Public Function dbLicense(ByVal cpuid As String)
        If mysql.upisInstalacije(cpuid) = True Then
            MessageBox.Show("Uspješno!")
            Globals.cpuid = cpuid
            Application.Current.Shutdown()
        ElseIf mysql.upisInstalacije(cpuid) = False Then
            MessageBox.Show("Instalacija sa ovim brojem već postoji u bazi ili je baza nedostupna!")
        End If
    End Function
    'Kreira hardware_id koristeći microsoftov WMI Query i vraća ga u string formatu
    Public Function CpuId() As String
        Dim computer As String = "."
        Dim wmi As Object = GetObject("winmgmts:" &
    "{impersonationLevel=impersonate}!\\" &
    computer & "\root\cimv2")
        Dim processors As Object = wmi.ExecQuery("Select * from " &
    "Win32_Processor")

        Dim cpu_ids As String = ""
        For Each cpu As Object In processors
            cpu_ids = cpu_ids & ", " & cpu.ProcessorId
        Next cpu
        If cpu_ids.Length > 0 Then cpu_ids =
    cpu_ids.Substring(2)

        Return cpu_ids
    End Function
End Class

Public Class Fiscal
    Dim file As System.IO.StreamWriter

    Public Function createFile()
        Globals.fiscalIn = "E:\test.txt"
        Globals.fiscalOut = "E:\test1.txt"
        Globals.iosa = "1234567890"
        Dim filepath As String = Globals.fiscalIn
        If Not System.IO.File.Exists(filepath) Then
            System.IO.File.Create(filepath).Dispose()
        ElseIf System.IO.File.Exists(filepath) Then
            System.IO.File.Delete(filepath)
            System.IO.File.Create(filepath).Dispose()
        End If
        Return True
    End Function

    Public Function fiscalArtikli(ByVal artikal As List(Of String), ByVal artikliProd As List(Of String), ByVal placanja As List(Of String), ByVal poruka As String, ByVal poruka1 As String, ByVal kupac As Boolean, Optional ByVal ibk As String = "", Optional ByVal parameter0 As String = "", Optional ByVal parameter1 As String = "", Optional ByVal parameter2 As String = "")
        Try
            createFile()
            file = My.Computer.FileSystem.OpenTextFileWriter(Globals.fiscalIn, True)
            For Each art As String In artikal
                'Dodaj artikal i promjeni mu cijenu
                file.WriteLine(art)
            Next
            file.WriteLine("48,1,______,_,__;" + Globals.iosa + ";" & "1" & ";" & "00000" & ";1;")
            For Each artProd As String In artikliProd
                'Prodaj artikle
                file.WriteLine(artProd)
            Next
            For Each vPlacanja As String In placanja
                'Prodaj artikle
                file.WriteLine(vPlacanja)
            Next
            If kupac = True Then
                'Ako je potrebno dodati kupca
                file.WriteLine("55,1,______,_,__;" + ibk + ";" & parameter0 & ";" + parameter1 + ";" + parameter2 + ";")
            Else
            End If
            file.WriteLine("350,1,______,_,__;+6" & poruka & ";")
            file.WriteLine("350,1,______,_,__;+7" & poruka1 & ";")
            'Otvori kasu 
            file.WriteLine("106,1,______,_,__;")
            closeFiscal(True)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function reklamirajFiskalni(ByVal art As String)
        Try
            createFile()
            'Reklamirani račun
            file.WriteLine("48,1,______,_,__;" + Globals.iosa + ";" & "1" & ";" & "0000000" & ";1;")
            closeFiscal(False)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Function getDuplicate(ByVal recNr As String)
        Try
            file.WriteLine("56,1,______,_,__;")
        Catch ex As Exception

        End Try
    End Function
    Public Function closeFiscal(ByVal closeReceipt As Boolean)
        Try
            If closeReceipt = True Then
                'Zatvori račun
                file.WriteLine("56,1,______,_,__;")
                file.Close()
            Else
                file.Close()
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

End Class

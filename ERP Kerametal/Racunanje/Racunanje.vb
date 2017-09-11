Imports MS.Internal

Public Class Racunanje
    Public Function zaokruziNaDvije(ByVal input As Decimal)
        Dim output As Decimal
        Try
            ' output = pretvoriTocke(input)
            '  output = Format(input, "0.00")
            output = Format(input, "0.00")

        Catch ex As Exception

        End Try
        Return output
    End Function
    Public Function pretvoriTocke(ByVal sirovo As String)
        Dim value1 As String = sirovo
        'Console.WriteLine(value1)

        ' Replace every instance of the string.
        Dim value2 As String = value1.Replace(",", ".")
        Return value2

    End Function
    Public Function racunajRabat(ByVal prviBroj As Double, ByVal drugiBroj As Double)
        Dim rabat As Double
        Try
            rabat = 100 - prviBroj / drugiBroj * 100
        Catch ex As Exception

        End Try
        Return rabat
    End Function
    Public Function cijenaKolicina(ByVal cijena As Double, ByVal kolicina As Double)
        Dim cijenaUkupno As Double
        Try
            cijenaUkupno = cijena * kolicina
        Catch ex As Exception

        End Try
        Return cijenaUkupno
    End Function

    Private varCifreJedinice As Object
    Private varCifreDoDvadeset As Object
    Private varCifreDesetice As Object
    Private varCifreStotine As Object

    Public Function PretvoriBrojUTekst(ByVal dblUlazniBroj As Double) As String
        Dim strCifra As String
        Dim intDuzinaCifre As Integer
        Dim intBrojTrojki As Integer
        Dim intOstatak As Integer
        Dim intBrojac As Integer
        Dim strMINUS As String
        '1234
        varCifreJedinice = {"", "jedan", "dva", "tri", "četiri", "pet",
                                  "šest", "sedam", "osam", "devet"}

        varCifreDoDvadeset = {"deset", "jedanaest", "dvanaest", "trinaest",
                                  "četrnaest", "petnaest", "šesnaest", "sedamnaest", "osamnaest", "devetnaest"}

        varCifreDesetice = {"", "", "dvadeset", "trideset", "četrdeset",
                                  "pedeset", "šezdeset", "sedamdeset", "osamdeset", "devedeset"}

        varCifreStotine = {"", "sto", "dvijesto", "tristo", "četiristo", "petsto",
                                 "šesto", "sedamsto", "osamsto", "devetsto", "SRF info HD"}

        If dblUlazniBroj < 0 Then
            strMINUS = "-"
            dblUlazniBroj = dblUlazniBroj
        End If

        PretvoriBrojUTekst = ""
        strCifra = Format(dblUlazniBroj, "###################.00")
        strCifra = Left(strCifra, Len(strCifra) - 3)
        intDuzinaCifre = Len(strCifra)
        intOstatak = intDuzinaCifre Mod 3
        intBrojTrojki = (intDuzinaCifre / 3) - ((intDuzinaCifre Mod 3) / 3)

        If dblUlazniBroj < 1000000000000.0# Then
            If intOstatak > 0 Then
                PretvoriBrojUTekst = OdrediTekstPodcifre(Left(strCifra, intOstatak), intBrojTrojki)
                strCifra = Right(strCifra, Len(strCifra) - intOstatak)
            End If
            For intBrojac = intBrojTrojki To 0 Step -1
                PretvoriBrojUTekst = PretvoriBrojUTekst & OdrediTekstPodcifre(Left(strCifra, 3), intBrojac - 1)
                If Len(strCifra) > 3 Then
                    strCifra = Right(strCifra, Len(strCifra) - 3)
                End If
            Next
        Else
        End If

        strCifra = Format(dblUlazniBroj, "###################.00")
        strCifra = Right(strCifra, 2)

        If Len(PretvoriBrojUTekst) > 0 Then
            'PretvoriBrojUTekst = UCase(Left(PretvoriBrojUTekst, 1)) & Right(PretvoriBrojUTekst, Len(PretvoriBrojUTekst) - 1)
            If (Right(PretvoriBrojUTekst, 5) = "Jedan") Or (Right(PretvoriBrojUTekst, 5) = "jedan") Then
                PretvoriBrojUTekst = PretvoriBrojUTekst & " " & strCifra & "/100."
            Else
                PretvoriBrojUTekst = PretvoriBrojUTekst & " " & strCifra & "/100."
            End If
        End If
    End Function

    Private Function OdrediTekstPodcifre(ByVal strPodcifra As String,
                                         intVelicina As Integer) As String

        Dim strTekst As String

        OdrediTekstPodcifre = ""

        If Val(strPodcifra) <> 0 Then
            strTekst = OdrediTekst(strPodcifra)

            Select Case intVelicina
                Case 0

                    OdrediTekstPodcifre = strTekst

                Case 1

                    'Slucaj za 11000,12000,13000,14000
                    If strPodcifra = "11" Or strPodcifra = "12" Or strPodcifra = "13" Or strPodcifra = "14" Then

                        OdrediTekstPodcifre = strTekst
                        OdrediTekstPodcifre = OdrediTekstPodcifre & "tisuća"

                    Else

                        'korekcija za slucaj za 2000, 22000, 32000 ....
                        If Right(strPodcifra, 1) = "2" Then
                            OdrediTekstPodcifre = Left(strTekst, Len(strTekst) - 1) & "e"
                        Else
                            OdrediTekstPodcifre = strTekst
                        End If

                        Select Case Right(strPodcifra, 1)
                     'Slucaj za 21000, 31000 ....
                            Case "1"
                                OdrediTekstPodcifre = Left(OdrediTekstPodcifre, Len(OdrediTekstPodcifre) - 2) & "natisuća"
                            Case "2", "3", "4"
                                OdrediTekstPodcifre = OdrediTekstPodcifre & "tisuće"
                            Case Else
                                OdrediTekstPodcifre = OdrediTekstPodcifre & "tisuća"
                        End Select

                        'Slucaj za 1000
                        If Val(strPodcifra) = 1 Then
                            OdrediTekstPodcifre = "tisuću"
                        End If

                    End If
                Case 2
                    OdrediTekstPodcifre = strTekst & "miliona"
                    If Val(strPodcifra) = 1 Then
                        OdrediTekstPodcifre = "milion"
                    End If
                Case 3
                    If Val(strPodcifra) = 1 Then
                        OdrediTekstPodcifre = "milijardu"
                        Exit Function
                    End If
                    If Val(Right(strPodcifra, 2)) > 5 And Val(Right(strPodcifra, 2)) < 21 Then
                        OdrediTekstPodcifre = strTekst & "milijardi"
                        Exit Function
                    End If
                    If Right(strPodcifra, 1) = "2" Then
                        OdrediTekstPodcifre = Left(strTekst, Len(strTekst) - 1) & "e"
                    Else
                        OdrediTekstPodcifre = strTekst
                    End If
                    Select Case Right(strPodcifra, 1)
                        Case "1"
                            OdrediTekstPodcifre = Left(OdrediTekstPodcifre, Len(OdrediTekstPodcifre) - 2) & "namilijarda"
                        Case "2", "3", "4"
                            OdrediTekstPodcifre = OdrediTekstPodcifre & "milijarde"
                        Case Else
                            OdrediTekstPodcifre = OdrediTekstPodcifre & "milijardi"
                    End Select
                    If Val(strPodcifra) = 1 Then
                        OdrediTekstPodcifre = "milijardu"
                    End If
            End Select
        End If
    End Function

    Private Function OdrediTekstDoSto(strPodcifra As String) As String
        OdrediTekstDoSto = ""

        Select Case Val(strPodcifra)
            Case 1 To 9
                OdrediTekstDoSto = varCifreJedinice(Val(strPodcifra))
            Case 10 To 19
                OdrediTekstDoSto = varCifreDoDvadeset(Val(strPodcifra) - 10)
            Case 20 To 99
                OdrediTekstDoSto = varCifreDesetice(Val(Left(strPodcifra, 1))) & varCifreJedinice(Val(Right(strPodcifra, 1)))
        End Select
    End Function

    Private Function OdrediTekst(strPodcifra As String) As String
        OdrediTekst = ""
        Select Case Val(strPodcifra)
            Case 1 To 99
                OdrediTekst = OdrediTekstDoSto(Val(strPodcifra))
            Case 100 To 999
                OdrediTekst = varCifreStotine(Val(Left(strPodcifra, 1))) & OdrediTekstDoSto(Val(Right(strPodcifra, 2)))
        End Select

    End Function
End Class

Public Class Racunanje
    Public Function zaokruziNaDvije(ByVal input As Decimal)
        Dim output As Decimal
        Try
            ' output = pretvoriTocke(input)
            output = Format(input, "0.00")
        Catch ex As Exception

        End Try
        Return output
    End Function
    Public Function pretvoriTocke(ByVal sirovo As String)
        Dim value1 As String = sirovo
        Console.WriteLine(value1)

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
    Public Function calcVrstePlacanja(ByVal gotovina As Double, ByVal kartice As Double, ByVal ziralno As Double, ByVal ostalo As Double, ByVal ukupno As Double, ByVal sender As Object)

    End Function
    Public Function rabatCalc(ByVal rabatPost As Boolean, ByVal rabatVrij As Boolean, ByVal scontoPost As Boolean, ByVal scontoVrij As Boolean, ByVal tip As String, ByVal broj As String)
        'kasa sconto (dodatni popust, uplata prije ugovorenog roka)

    End Function
End Class

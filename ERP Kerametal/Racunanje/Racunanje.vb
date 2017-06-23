﻿Public Class Racunanje
    Public Function zaokruziNaDvije(ByVal input As Double)
        Dim output As Decimal
        Try
            output = Format(Convert.ToDouble(input), "####.#0")
        Catch ex As Exception

        End Try
        Return output
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
End Class
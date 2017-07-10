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
    Public Function routines1()
        Dim report As New Racun()
        Dim route As Object
        Dim fieldName As Object
        Dim owner As Boolean

        Try
            'Prikaz banke
            report.imeBanke1.Visible = True
            report.brojBanke1.Visible = True
            report.imeBanke2.Visible = True
            report.brojBanke2.Visible = True
            report.imeBanke3.Visible = True
            report.brojBanke3.Visible = True
            ''
            report.XrLabel75.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "dok_zag_d.mpc", "{0:}")})
            report.XrLabel75.Dpi = 100.0!
            report.XrLabel75.LocationFloat = New DevExpress.Utils.PointFloat(702.7643!, 0!)
            report.XrLabel75.Name = "XrLabel75"
            report.XrLabel75.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
            report.XrLabel75.SizeF = New System.Drawing.SizeF(44.5907!, 18.0!)
            report.XrLabel75.StylePriority.UseTextAlignment = False
            report.XrLabel75.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Catch ex As Exception

        End Try
    End Function

End Class

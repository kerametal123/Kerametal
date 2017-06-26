Imports System.Net
Public Class Globals
    Public Shared rootPath As String =
IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ERPKerametal\")
    'Odabrani artikal
    Public Shared sifraG = ""
    Public Shared nazivG = ""
    Public Shared mpcG = ""
    Public Shared kolicinaG = ""
    Public Shared rabatG = ""
    Public Shared pcG = ""
    Public Shared iznosG = ""
    Public Shared vpcG = ""
    Public Shared ncG = ""
    Public Shared pluG = ""
    Public Shared placanjeG = ""
    Public Shared tipArtiklaG = ""
    'Cijena koja se koristi
    Public Shared cijenaG = ""
    Public Shared cijenaUkupnoG = ""
    Public Shared tipDokumenta = ""
    Public Shared brojDokumenta = ""
    Public Shared newlook As Boolean
    Public Shared login As Boolean
    Public Shared programAktivni = ""
    Public Shared dabase = ""
    Public Shared vrstaObjekta = ""
    Public Shared aktivnaGodina = ""
    Public Shared databaseInfo = ""
    Public Shared databaseName = ""
    Public Shared defaultProg = ""
    'Potrebno za identifikaciju
    Public Shared objekt = ""
    Public Shared tvrtka = ""
    Public Shared godina = ""
    Public Shared lastEdit = ""
    'Samo za informacije
    Public Shared tvrtka_naziv = ""
    Public Shared objekt_naziv = ""
    Public Shared cpuid = ""

    'Samo racunalo
    Public Shared racunalo_naziv = ""

    'Array MP
    Public Shared prodaja = "", Kalkulacije = "", Zaduznice = "", Predisponacije = "", Robno = "", KUF = "", KIF = "", Narudzbenice = "", Nalozi = "", akcijskeCijene = "", servisnaRoba = "", Ostalo1 = "", Ostalo2 = "", Ostalo3 = "", Ostalo4 = ""
    'Array VP
    Public Shared vp_fakture = "", vp_Kalkulacije = "", vp_Otpremnice = "", vp_Predisponacije = "", vp_Robno = "", vp_KUF = "", vp_KIF = "", vp_Narudzbenice = "", vp_Nalozi = "", vp_akcijskeCijene = "", vp_servisnaRoba = "", vp_Elektronska_oprema = "", vp_Ambalazni_otpad = "", vp_Web_fakture = "", vp_Ostalo1 = "", vp_Ostalo2 = "", vp_Ostalo3 = "", vp_dat1 = "", vp_dat2 = "", vp_dat3 = ""
    'Array UG
    Public Shared ug_prodaja = "", ug_Kalkulacije = "", ug_Zaduznice = "", ug_Predisponacije = "", ug_Robno = "", ug_KUF = "", ug_KIF = "", ug_Narudzbenice = "", ug_Nalozi = "", ug_akcijskeCijene = "", ug_servisnaRoba = "", ug_Ostalo1 = "", ug_Ostalo2 = "", ug_Ostalo3 = "", ug_Ostalo4 = "", ug_Ostalo5 = ""
    'Array FK
    Public Shared fk_glavna_knjiga = "", fk_saldo_konti = "", fk_blagajna = "", fk_aa = "", fk_bb = "", fk_KUF = "", fk_KIF = "", fk_ostalo1 = "", fk_ostalo2 = "",
        fk_ostalo3 = "", fk_ostalo4 = "", fk_ostalo5 = "", fk_ostalo6 = "", fk_ostalo7 = "", fk_ostalo8 = "", fk_ostalo9 = "",
        fk_partneri = "", fk_kontni_plan = "", fk_o1 = "", fk_o2 = "", fk_o3 = "", fk_o4 = "", fk_o5 = "", fk_o6 = "", fk_o7 = "", fk_o8 = ""
    Public Shared urediDodaj = ""
    Public Shared Function CheckForInternetConnection() As Boolean
        Dim we As MainWindow = Application.Current.Windows(0)
        Try
            Using client = New WebClient()
                Using stream = client.OpenRead("http://www.google.com")
                    we.biCenter.IsEnabled = True
                    Return True
                End Using
            End Using
        Catch
            we.biCenter.IsEnabled = False
            Return False
        End Try
    End Function
    Public Shared Function logMaker(ByVal action As String, ByVal type As Object)
        Try

            Dim mysql As New MySQLinfo
            mysql.upisLoga(action, type.ToString())
        Catch
            Return False
        End Try
        Return False
    End Function
    Public Function sessionId()

    End Function
End Class

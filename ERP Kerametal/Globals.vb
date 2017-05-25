Imports System.Net
Public Class Globals
    Public Shared rootPath As String =
IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ERPKerametal\")
    Public Shared databaseInfo = ""
    Public Shared databaseName = ""

    'Potrebno za identifikaciju
    Public Shared objekt = ""
    Public Shared tvrtka = ""
    Public Shared godina = ""

    'Samo za informacije
    Public Shared tvrtka_naziv = ""
    Public Shared objekt_naziv = ""
    Public Shared cpuid = ""

    'Samo racunalo
    Public Shared racunalo_naziv = ""

    'Array MP
    Public Shared prodaja = "", Kalkulacije = "", Zaduznice = "", Predisponacije = "", Robno = "", KUF = "", KIF = "", Narudzbenice = "", Nalozi = "", akcijskeCijene = "", servisnaRoba = "", Ostalo1 = "", Ostalo2 = "", Ostalo3 = ""
    'Array VP
    Public Shared vp_fakture = "", vp_Kalkulacije = "", vp_Otpremnice = "", vp_Predisponacije = "", vp_Robno = "", vp_KUF = "", vp_KIF = "", vp_Narudzbenice = "", vp_Nalozi = "", vp_akcijskeCijene = "", vp_servisnaRoba = "", vp_Elektronska_oprema = "", vp_Ambalazni_otpad = "", vp_Web_fakture = "", vp_Ostalo1 = "", vp_Ostalo2 = "", vp_Ostalo3 = "", vp_dat1 = "", vp_dat2 = "", vp_dat3 = ""
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
    End Function
    Public Function sessionId()

    End Function
End Class

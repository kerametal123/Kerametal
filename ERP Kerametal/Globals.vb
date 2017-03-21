Imports System.Net
Public Class Globals
    Public Shared rootPath As String =
IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ERPKerametal\")
    Public Shared databaseInfo = ""
    Public Shared databaseName = ""
    Public Shared objekt = ""
    Public Shared cpuid = ""
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

End Class

Imports System.IO
Public Class Folders
    Public Function mkFolderRoot()
        Dim putanja As String
        putanja = Globals.rootPath
        Try
            ' Determine whether the directory exists.
            If Directory.Exists(putanja) Then
                Return False
            Else
                ' Try to create the directory.
                Dim di As DirectoryInfo = Directory.CreateDirectory(putanja)
                Return True
            End If
        Catch e As Exception
            Return False
        End Try
        Return True
    End Function

End Class

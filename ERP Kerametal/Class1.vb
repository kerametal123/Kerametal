Public Class Class1
    Dim r1 As String
    Dim r2 As String
    Dim sconto As String
    Dim show As String

    Public Function calc()
        If r1 = "0" Then

        Else
            If r2 = "0" Then
                If sconto = "0" Then
                    show = r1 + "+" + r2
                Else
                    show = r1 + "+" + r2 + "+" + sconto
                End If
            End If
        End If
    End Function

End Class

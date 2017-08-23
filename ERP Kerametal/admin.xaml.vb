Public Class admin
    Dim mysqlsrvc As New MySQLService
    Dim mysqlinfo As New MySQLinfo
    Private Sub Window_Initialized(sender As Object, e As EventArgs)
        ' getOvlasti()
    End Sub
    Public Function getOvlasti()
        'Select 
        Dim infos As List(Of String)
        Try

        Catch ex As Exception

        End Try
        hwid.Text = Globals.cpuid
        installname.Text = mysqlinfo.getAdminInfo(0)
        objekt.Text = mysqlinfo.getAdminInfo(4)
        godina.Text = mysqlinfo.getAdminInfo(9)
        tvrtka.Text = mysqlinfo.getAdminInfo(1)

        ' mp.Content = "1111"
        'vp.Content = mysqlinfo.getAdminInfo(6)
        ' fin.Content = mysqlinfo.getAdminInfo(7)
        'ug.Content = mysqlinfo.getAdminInfo(8)
    End Function

    Private Sub biOpen_ItemClick(sender As Object, e As DevExpress.Xpf.Bars.ItemClickEventArgs) Handles biOpen.ItemClick
        getOvlasti()
    End Sub
End Class

Imports System.IO
Public Class Sucelje
        Dim mysql As New MySQLcompany
        Dim w As MainWindow = Application.Current.Windows(0)


    Public Function provjeriDozvoleSuceljaVidljivostiHwid()
            Dim window = Windows.Application.Current.MainWindow
            Dim visuals = GetVisualChildren(Of FrameworkElement)(window)
                    Dim child = visuals.OfType(Of FrameworkElement)()
            Dim match = child.FirstOrDefault(Function(x) x.Name = "biCenter")
        match.Visibility = Visibility.Collapsed
    End Function


    End Class

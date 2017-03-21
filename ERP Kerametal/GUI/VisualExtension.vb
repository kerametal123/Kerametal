Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Media
'It's a kind of magic :D
Module VisualExtensions
    <System.Runtime.CompilerServices.Extension>
    Public Iterator Function GetVisualChildren(Of T As Visual)(parent As DependencyObject) As IEnumerable(Of T)
        Dim child As T = Nothing
        Dim numVisuals As Integer = VisualTreeHelper.GetChildrenCount(parent)
        For i As Integer = 0 To numVisuals - 1
            Dim v As Visual = DirectCast(VisualTreeHelper.GetChild(parent, i), Visual)
            child = TryCast(v, T)
            If v IsNot Nothing Then
                For Each item In GetVisualChildren(Of T)(v)
                    Yield item
                Next
            End If
            If child IsNot Nothing Then
                Yield child
            End If
        Next
    End Function
    Private Function FindChildByname(Of T As FrameworkElement)(parent As DependencyObject, name As String) As T
        Dim child As T = Nothing
        For i As Integer = 0 To VisualTreeHelper.GetChildrenCount(parent) - 1
            Dim ch = VisualTreeHelper.GetChild(parent, i)
            child = TryCast(ch, T)
            If child IsNot Nothing AndAlso child.Name = name Then
                Exit For
            Else
                child = FindChildByname(Of T)(ch, name)
            End If

            If child IsNot Nothing Then
                Exit For
            End If
        Next
        Return child
    End Function
End Module
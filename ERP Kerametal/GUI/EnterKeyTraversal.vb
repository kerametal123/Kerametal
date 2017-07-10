Public Class EnterKeyTraversal
    Public Shared Function GetIsEnabled(obj As DependencyObject) As Boolean
        Return CBool(obj.GetValue(IsEnabledProperty))
    End Function

    Public Shared Sub SetIsEnabled(obj As DependencyObject, value As Boolean)
        obj.SetValue(IsEnabledProperty, value)
    End Sub

    Private Shared Sub ue_PreviewKeyDown(sender As Object, e As System.Windows.Input.KeyEventArgs)
        Dim ue = TryCast(e.OriginalSource, FrameworkElement)

        If e.Key = Key.Enter Then
            e.Handled = True
            ue.MoveFocus(New TraversalRequest(FocusNavigationDirection.[Next]))
        End If
    End Sub

    Private Shared Sub ue_Unloaded(sender As Object, e As RoutedEventArgs)
        Dim ue = TryCast(sender, FrameworkElement)
        If ue Is Nothing Then
            Return
        End If
        'preview down
        RemoveHandler ue.Unloaded, AddressOf ue_Unloaded
        RemoveHandler ue.PreviewKeyDown, AddressOf ue_PreviewKeyDown
    End Sub

    'predview up    
    Public Shared ReadOnly IsEnabledProperty As DependencyProperty = DependencyProperty.RegisterAttached("IsEnabled", GetType(Boolean), GetType(EnterKeyTraversal), New UIPropertyMetadata(False, AddressOf IsEnabledChanged))

    Private Shared Sub IsEnabledChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim ue = TryCast(d, FrameworkElement)
        If ue Is Nothing Then
            Return
        End If
        'create handler
        If CBool(e.NewValue) Then
            'undo
            AddHandler ue.Unloaded, AddressOf ue_Unloaded
            'redo
            AddHandler ue.PreviewKeyDown, AddressOf ue_PreviewKeyDown
        Else
            'remove handler
            RemoveHandler ue.PreviewKeyDown, AddressOf ue_PreviewKeyDown
        End If
    End Sub
End Class

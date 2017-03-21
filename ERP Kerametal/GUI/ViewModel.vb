Imports System.ComponentModel

Public Class ViewModel
    Implements INotifyPropertyChanged

    Public Sub New()
        Me.myTextValue = "default value..."
    End Sub
    Private myTextValue As String = String.Empty
    Public Property MyTextProperty() As String
        Get
            Return Me.myTextValue
        End Get

        Set(ByVal value As String)
            Me.myTextValue = value
            NotifyPropertyChanged("MyTextProperty")
        End Set
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged

    Private Sub NotifyPropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

End Class
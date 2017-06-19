Imports System.Text.RegularExpressions
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Grid


Public Class mpBc
    Dim mysql As New MySQLinfo
    Dim mysqlcomp As New MySQLcompany
    Dim licenca As New Licenciranje
    Dim XMLinfo As New XMLinfo
    Private mint_LastReceivedTimerID As Integer = 0
    Private mint_LastInitializedTimerID As Integer = 0
    Public Overridable Property AutoFilterCondition As AutoFilterCondition
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        dodatiCheck.IsChecked = True
        infoGrid.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#593AFF00"), Color))
    End Sub

    Private Sub GridControl_AsyncOperationCompleted(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub textBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles textBox.TextChanged
        'Increment the counter for the number of times the textbox has been changed
        mint_LastInitializedTimerID = mint_LastInitializedTimerID + 1

        'Wait longer for shorter strings or strings without spaces
        Dim intMilliseconds As Integer = 500


        Dim objTimer As New System.Timers.Timer(intMilliseconds)
        AddHandler objTimer.Elapsed, AddressOf textBox_TimerElapsed

        objTimer.AutoReset = False
        objTimer.Enabled = True

    End Sub
    Private Sub textBox_TimerElapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        'Increment the counter for the number of times timers have elapsed
        mint_LastReceivedTimerID = mint_LastReceivedTimerID + 1

        'If the total number of textbox changes equals the total number of times timers have elapsed (fire method for only the latest character change)
        If mint_LastReceivedTimerID = mint_LastInitializedTimerID Then

            Me.Dispatcher.Invoke(Sub() MySearchMethod(), System.Windows.Threading.DispatcherPriority.Normal)

        End If

    End Sub
    Public Sub MySearchMethod()

        'Filtriranje grida, lokalno u RAM memoriji

        'Fire method on the Main UI Thread

        Dim filterValue As String = textBox.Text
        If Not [String].IsNullOrEmpty(filterValue) Then
            If Regex.IsMatch(filterValue, "^[0-9 ]+$") Then
                gridArtikli.Columns("naziv").AutoFilterValue = ""
                gridArtikli.Columns("sifra").AutoFilterValue = filterValue
            Else
                gridArtikli.Columns("sifra").AutoFilterValue = ""
                gridArtikli.Columns("naziv").AutoFilterCondition = AutoFilterCondition.Contains
                gridArtikli.Columns("naziv").AutoFilterValue = filterValue
            End If
        End If
    End Sub


    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        'populate gridArtikli
        gridArtikli.ItemsSource = mysqlcomp.getArtikliZaAktivnog
    End Sub

    Private Sub button_Copy_Click(sender As Object, e As RoutedEventArgs) Handles button_Copy.Click
        'populate gridPartneri
        gridPartneri.ItemsSource = mysqlcomp.getPartneriZaAktivnog
        Globals.logMaker("Partneri grid populate", sender)

    End Sub

    Private Sub ispravitiCheck_Click(sender As Object, e As RoutedEventArgs) Handles ispravitiCheck.Click
        If dodatiCheck.IsChecked = True Then
            dodatiCheck.IsChecked = False
        End If
        ispravitiCheck.IsChecked = True
        infoGrid.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#7FFF0000"), Color))
    End Sub

    Private Sub dodatiCheck_Click(sender As Object, e As RoutedEventArgs) Handles dodatiCheck.Click
        If ispravitiCheck.IsChecked = True Then
            ispravitiCheck.IsChecked = False
        End If
        dodatiCheck.IsChecked = True
        infoGrid.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString("#593AFF00"), Color))
    End Sub

    Private Sub simpleButton_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton.Click
        For Each item In mysqlcomp.getOperateri()

            operateriCombo.Items.Add(item.ime + " " + item.prezime)
        Next
    End Sub

    Private Sub simpleButton2_Copy2_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton2_Copy2.Click
        'For Each item In mysqlcomp.getGrupeArtikala()
        'grupeCbox.Items.Add(item.grupa)
        ' Next
        For Each item In mysqlcomp.getProizvodaci()
            Dim barmanager1 As New BarManager
            Dim comboboxitem = New ComboBoxItem()
            comboboxitem.Content = item.proiz
            comboboxitem.Tag = item.idproiz
            'AddHandler comboboxitem.Selected, Function() prijavi(item.idproiz)
            proizvodacCbox.Items.Add(comboboxitem)
        Next
    End Sub
    Private Sub simpleButton3_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton3.Click

    End Sub

    Private Sub simpleButton2_Click(sender As Object, e As RoutedEventArgs) Handles simpleButton2.Click

    End Sub

    Private Sub dodatiCheck_Copy2_Click(sender As Object, e As RoutedEventArgs) Handles dodatiCheck_Copy2.Click
        MessageBox.Show(Globals.tvrtka)
    End Sub
End Class

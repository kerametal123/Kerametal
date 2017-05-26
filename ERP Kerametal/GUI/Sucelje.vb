Imports System.IO
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Navigation

Public Class Sucelje
    Dim mysql As New MySQLcompany
    Dim w As MainWindow = Application.Current.Windows(0)
    Public Property Icon As BitmapImage

    Public Function provjeriDozvoleSuceljaVidljivostiHwid()
        Dim window = Windows.Application.Current.MainWindow
        Dim visuals = GetVisualChildren(Of FrameworkElement)(window)
        Dim child = visuals.OfType(Of FrameworkElement)()
        Dim match = child.FirstOrDefault(Function(x) x.Name = "biCenter")
        match.Visibility = Visibility.Collapsed
    End Function

    Public Function opcijeMPtipke()
        Dim array() As String = {Globals.prodaja, Globals.Kalkulacije, Globals.Robno, Globals.KUF, Globals.KIF, Globals.Narudzbenice, Globals.Nalozi, Globals.akcijskeCijene, Globals.servisnaRoba, Globals.Ostalo1, Globals.Ostalo2, Globals.Ostalo3}

        For Each value As String In array
            Dim s As String = value
            Dim parts As String() = s.Split(New Char() {","c})
            Dim icona As String = parts(1)
            Dim barmanager1 As New BarManager
            Dim BarButtonItem = New BarButtonItem()
            BarButtonItem.Content = parts(3)
            'BarButtonItem.Name = parts(3)
            If parts(0) = 1 Then
                BarButtonItem.IsVisible = False
            ElseIf parts(0) = 2 Then

            ElseIf parts(0) = 3 Then

            ElseIf parts(0) = 4 Then

            End If
            Icon = New BitmapImage(New Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/" + icona + ""))
            BarButtonItem.LargeGlyph = Icon
            'BarButtonItem.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString(parts(2)), Color))
            w.maloprodaja.Items.Add(BarButtonItem)
        Next
    End Function
    Public Function opcijeVPtipke()
        Dim array() As String = {Globals.vp_fakture, Globals.vp_Kalkulacije, Globals.vp_Otpremnice, Globals.vp_Predisponacije, Globals.vp_Robno, Globals.vp_KUF, Globals.vp_KIF, Globals.vp_Narudzbenice, Globals.vp_Nalozi, Globals.vp_akcijskeCijene, Globals.vp_servisnaRoba, Globals.vp_Elektronska_oprema, Globals.vp_Ambalazni_otpad, Globals.vp_Web_fakture, Globals.vp_Ostalo1, Globals.vp_Ostalo2, Globals.Ostalo3, Globals.vp_dat1, Globals.vp_dat2, Globals.vp_dat3}

        For Each value As String In array
            Dim s As String = value
            Dim parts As String() = s.Split(New Char() {","c})
            Dim icona As String = parts(1)
            Dim barmanager1 As New BarManager
            Dim BarButtonItem = New BarButtonItem()
            BarButtonItem.Content = parts(3)
            'BarButtonItem.Name = parts(3)
            If parts(0) = 1 Then
                BarButtonItem.IsVisible = False
            ElseIf parts(0) = 2 Then

            ElseIf parts(0) = 3 Then

            ElseIf parts(0) = 4 Then

            End If
            Icon = New BitmapImage(New Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/" + icona + ""))
            BarButtonItem.LargeGlyph = Icon
            'BarButtonItem.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString(parts(2)), Color))
            w.veleprodaja.Items.Add(BarButtonItem)
        Next
    End Function
    Public Function opcijeUGtipke()
        Dim array() As String = {Globals.ug_prodaja, Globals.ug_Kalkulacije, Globals.ug_Zaduznice, Globals.ug_Predisponacije, Globals.ug_Robno, Globals.ug_KUF, Globals.ug_KIF, Globals.ug_Narudzbenice, Globals.ug_Nalozi, Globals.ug_akcijskeCijene, Globals.ug_servisnaRoba, Globals.ug_Ostalo1, Globals.ug_Ostalo2, Globals.ug_Ostalo3, Globals.ug_Ostalo4, Globals.ug_Ostalo5}

        For Each value As String In array
            Dim s As String = value
            Dim parts As String() = s.Split(New Char() {","c})
            Dim icona As String = parts(1)
            Dim barmanager1 As New BarManager
            Dim BarButtonItem = New BarButtonItem()
            BarButtonItem.Content = parts(3)
            'BarButtonItem.Name = parts(3)
            If parts(0) = 1 Then
                BarButtonItem.IsVisible = False
            ElseIf parts(0) = 2 Then

            ElseIf parts(0) = 3 Then

            ElseIf parts(0) = 4 Then

            End If
            Icon = New BitmapImage(New Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/" + icona + ""))
            BarButtonItem.LargeGlyph = Icon
            'BarButtonItem.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString(parts(2)), Color))
            w.ugostiteljstvo.Items.Add(BarButtonItem)
        Next
    End Function

    Public Function opcijeFKtipke()
        Dim array() As String = {Globals.fk_glavna_knjiga, Globals.fk_saldo_konti, Globals.fk_blagajna, Globals.fk_aa, Globals.fk_bb, Globals.fk_KUF, Globals.fk_KIF, Globals.fk_ostalo1, Globals.fk_ostalo2, Globals.fk_ostalo3, Globals.fk_ostalo4, Globals.fk_ostalo5, Globals.fk_ostalo6, Globals.fk_ostalo7, Globals.fk_ostalo8, Globals.fk_ostalo9, Globals.fk_partneri, Globals.fk_kontni_plan}

        For Each value As String In array
            Dim s As String = value
            Dim parts As String() = s.Split(New Char() {","c})
            Dim icona As String = parts(1)
            Dim barmanager1 As New BarManager
            Dim BarButtonItem = New BarButtonItem()
            BarButtonItem.Content = parts(3)
            'BarButtonItem.Name = parts(3)
            If parts(0) = 1 Then
                BarButtonItem.IsVisible = False
            ElseIf parts(0) = 2 Then

            ElseIf parts(0) = 3 Then

            ElseIf parts(0) = 4 Then

            End If
            Icon = New BitmapImage(New Uri("pack://application:,,,/DevExpress.Images.v16.1;component/Images/" + icona + ""))
            BarButtonItem.LargeGlyph = Icon
            'BarButtonItem.Background = New SolidColorBrush(DirectCast(ColorConverter.ConvertFromString(parts(2)), Color))
            w.financije.Items.Add(BarButtonItem)
        Next
    End Function
End Class

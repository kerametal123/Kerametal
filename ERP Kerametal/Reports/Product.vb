Class Item
    'plaćanje u tabeli artikala: if null = ništa, else if 1 = akcijska cijena samo za gotovinsko plaćanje i ako je pc različito od nule
    'ako je bar kod onda je cijena i količina zaključana po defaultu i rabat

    Private _Sifra As String
    Private _Naziv As String
    Private _Kolicina As String
    Private _Cijena As String 'MPC
    Private _Rabat As String
    Private _PC As String 'PRODAJNA CIJENA IZ TABELE
    Private _Iznos As String
    Property Sifra() As String
        Get
            Return _Sifra
        End Get
        Set(ByVal value As String)
            Me._Sifra = value
        End Set
    End Property

    Property Naziv() As String
        Get
            Return _Naziv
        End Get
        Set(ByVal value As String)
            Me._Naziv = value
        End Set
    End Property
    Property Kolicina() As Integer
        Get
            Return _Kolicina
        End Get
        Set(ByVal value As Integer)
            Me._Kolicina = value
        End Set
    End Property
    Property Cijena() As Decimal
        Get
            Return _Cijena
        End Get
        Set(ByVal value As Decimal)
            Me._Cijena = value
        End Set
    End Property

    Property Rabat() As Integer
        Get
            Return _Rabat
        End Get
        Set(ByVal value As Integer)
            Me._Rabat = value
        End Set
    End Property
    Property PC() As Decimal
        Get
            Return _PC
        End Get
        Set(ByVal value As Decimal)
            Me._PC = value
        End Set
    End Property

    Property Iznos() As Decimal
        Get
            Return _Iznos
        End Get
        Set(ByVal value As Decimal)
            Me._Iznos = Me._Kolicina * Me.PC
        End Set
    End Property

End Class
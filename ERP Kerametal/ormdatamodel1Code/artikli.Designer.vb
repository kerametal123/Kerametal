﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
Namespace kerametal

    Partial Public Class artikli
        Inherits XPLiteObject
        Dim fan As Integer
        <Key(True)>
        Public Property an() As Integer
            Get
                Return fan
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("an", fan, value)
            End Set
        End Property
        Dim ftip As Integer
        Public Property tip() As Integer
            Get
                Return ftip
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("tip", ftip, value)
            End Set
        End Property
        Dim fsifra As String
        <Size(5)>
        Public Property sifra() As String
            Get
                Return fsifra
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("sifra", fsifra, value)
            End Set
        End Property
        Dim fobjekt As String
        <Size(5)>
        Public Property objekt() As String
            Get
                Return fobjekt
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("objekt", fobjekt, value)
            End Set
        End Property
        Dim fnaziv As String
        <Size(200)>
        Public Property naziv() As String
            Get
                Return fnaziv
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("naziv", fnaziv, value)
            End Set
        End Property
        Dim fjed As String
        <Size(20)>
        Public Property jed() As String
            Get
                Return fjed
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("jed", fjed, value)
            End Set
        End Property
        Dim fozn As String
        <Size(15)>
        Public Property ozn() As String
            Get
                Return fozn
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("ozn", fozn, value)
            End Set
        End Property
        Dim fgrupa As String
        <Size(20)>
        Public Property grupa() As String
            Get
                Return fgrupa
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("grupa", fgrupa, value)
            End Set
        End Property
        Dim fproiz As String
        <Size(20)>
        Public Property proiz() As String
            Get
                Return fproiz
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("proiz", fproiz, value)
            End Set
        End Property
        Dim ftarifa As String
        <Size(10)>
        Public Property tarifa() As String
            Get
                Return ftarifa
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("tarifa", ftarifa, value)
            End Set
        End Property
        Dim fnc As Double
        Public Property nc() As Double
            Get
                Return fnc
            End Get
            Set(ByVal value As Double)
                SetPropertyValue(Of Double)("nc", fnc, value)
            End Set
        End Property
        Dim fvpc As Double
        Public Property vpc() As Double
            Get
                Return fvpc
            End Get
            Set(ByVal value As Double)
                SetPropertyValue(Of Double)("vpc", fvpc, value)
            End Set
        End Property
        Dim fmpc As Double
        Public Property mpc() As Double
            Get
                Return fmpc
            End Get
            Set(ByVal value As Double)
                SetPropertyValue(Of Double)("mpc", fmpc, value)
            End Set
        End Property
        Dim felekt_razred As String
        <Indexed(Name:="elekt_razred")>
        <Size(5)>
        Public Property elekt_razred() As String
            Get
                Return felekt_razred
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("elekt_razred", felekt_razred, value)
            End Set
        End Property
        Dim felekt_podrazred As String
        <Size(50)>
        Public Property elekt_podrazred() As String
            Get
                Return felekt_podrazred
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("elekt_podrazred", felekt_podrazred, value)
            End Set
        End Property
        Dim ftezina As Double
        Public Property tezina() As Double
            Get
                Return ftezina
            End Get
            Set(ByVal value As Double)
                SetPropertyValue(Of Double)("tezina", ftezina, value)
            End Set
        End Property
        Dim ftar_broj As String
        <Size(50)>
        Public Property tar_broj() As String
            Get
                Return ftar_broj
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("tar_broj", ftar_broj, value)
            End Set
        End Property
        Dim fstopa As Double
        Public Property stopa() As Double
            Get
                Return fstopa
            End Get
            Set(ByVal value As Double)
                SetPropertyValue(Of Double)("stopa", fstopa, value)
            End Set
        End Property
        Dim fid As Integer
        <Indexed(Name:="id")>
        Public Property id() As Integer
            Get
                Return fid
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("id", fid, value)
            End Set
        End Property
        Dim fdatum As DateTime
        Public Property datum() As DateTime
            Get
                Return fdatum
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("datum", fdatum, value)
            End Set
        End Property
        Dim fplu As Double
        Public Property plu() As Double
            Get
                Return fplu
            End Get
            Set(ByVal value As Double)
                SetPropertyValue(Of Double)("plu", fplu, value)
            End Set
        End Property
        Dim fdok As String
        <Size(50)>
        Public Property dok() As String
            Get
                Return fdok
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("dok", fdok, value)
            End Set
        End Property
        Dim fpc As Double
        Public Property pc() As Double
            Get
                Return fpc
            End Get
            Set(ByVal value As Double)
                SetPropertyValue(Of Double)("pc", fpc, value)
            End Set
        End Property
        Dim fplacanje As Integer
        Public Property placanje() As Integer
            Get
                Return fplacanje
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("placanje", fplacanje, value)
            End Set
        End Property
    End Class

End Namespace

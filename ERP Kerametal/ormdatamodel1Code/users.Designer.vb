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

    Partial Public Class users
        Inherits XPLiteObject
        Dim fusername As String
        <Key()>
        <Size(30)>
        Public Property username() As String
            Get
                Return fusername
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("username", fusername, value)
            End Set
        End Property
        Dim fpassword As String
        <Size(40)>
        Public Property password() As String
            Get
                Return fpassword
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("password", fpassword, value)
            End Set
        End Property
        Dim fusersalt As String
        <Size(8)>
        Public Property usersalt() As String
            Get
                Return fusersalt
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("usersalt", fusersalt, value)
            End Set
        End Property
        Dim fuserid As String
        <Size(32)>
        Public Property userid() As String
            Get
                Return fuserid
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("userid", fuserid, value)
            End Set
        End Property
        Dim fuserlevel As Byte
        Public Property userlevel() As Byte
            Get
                Return fuserlevel
            End Get
            Set(ByVal value As Byte)
                SetPropertyValue(Of Byte)("userlevel", fuserlevel, value)
            End Set
        End Property
        Dim femail As String
        <Size(50)>
        Public Property email() As String
            Get
                Return femail
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("email", femail, value)
            End Set
        End Property
        Dim ftimestamp As UInteger
        Public Property timestamp() As UInteger
            Get
                Return ftimestamp
            End Get
            Set(ByVal value As UInteger)
                SetPropertyValue(Of UInteger)("timestamp", ftimestamp, value)
            End Set
        End Property
        Dim factkey As String
        <Size(35)>
        Public Property actkey() As String
            Get
                Return factkey
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("actkey", factkey, value)
            End Set
        End Property
        Dim fip As String
        <Size(15)>
        Public Property ip() As String
            Get
                Return fip
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("ip", fip, value)
            End Set
        End Property
        Dim fregdate As UInteger
        Public Property regdate() As UInteger
            Get
                Return fregdate
            End Get
            Set(ByVal value As UInteger)
                SetPropertyValue(Of UInteger)("regdate", fregdate, value)
            End Set
        End Property
    End Class

End Namespace

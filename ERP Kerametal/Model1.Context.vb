﻿'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Partial Public Class kerametalEntities
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=kerametalEntities")
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        Throw New UnintentionalCodeFirstException()
    End Sub

    Public Property active_guests() As DbSet(Of active_guests)
    Public Property active_users() As DbSet(Of active_users)
    Public Property artikli() As DbSet(Of artikli)
    Public Property banned_users() As DbSet(Of banned_users)
    Public Property users() As DbSet(Of users)
    Public Property configuration() As DbSet(Of configuration)
    Public Property dok_sta_d() As DbSet(Of dok_sta_d)

End Class

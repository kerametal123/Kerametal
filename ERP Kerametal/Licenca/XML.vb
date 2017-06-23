Imports System.Xml



Public Class XMLinfo
    Dim mysqlcomp As New MySQLcompany
    Dim putanjaDatoteke As String =
IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Kerametal\")
    Public Function infoXMLdb(ByVal objekt As String, ByVal objektname As String, ByVal tvrtka As String, ByVal tvrtkaname As String, ByVal dabase As String, ByVal db1 As String, ByVal db2 As String, ByVal db3 As String, ByVal db4 As String)
        Dim writer As New XmlTextWriter(Globals.rootPath & "settings.xml", System.Text.Encoding.UTF8)
        writer.WriteStartDocument(True)
        writer.Formatting = Formatting.Indented
        writer.Indentation = 2
        writer.WriteStartElement("Table")
        createNode(1, "db1", db1, writer, dabase)
        createNode(2, "db2", db2, writer, dabase)
        createNode(3, "db3", db3, writer, dabase)
        createNode(4, "db4", db4, writer, dabase)
        writer.WriteEndElement()
        writer.WriteEndDocument()
        writer.Close()
    End Function
    Private Sub createNode(ByVal pID As String, ByVal pName As String, ByVal pPrice As String, ByVal writer As XmlTextWriter, ByVal dabase As String)
        writer.WriteStartElement("Database")
        writer.WriteStartElement("database_id")
        writer.WriteString(pID)
        writer.WriteEndElement()
        writer.WriteStartElement("database_name")
        writer.WriteString(dabase)
        writer.WriteEndElement()
        writer.WriteStartElement("database_string")
        writer.WriteString(pPrice)
        writer.WriteEndElement()
        writer.WriteEndElement()
    End Sub
    Public Function getDbInfo()
        Dim xmlDoc As New XmlDocument()
        xmlDoc.Load(Globals.rootPath & "settings.xml")
        Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("Database")
        Dim pID As String = "", pName As String = "", pString As String = ""
        For Each node As XmlNode In nodes
            pID = node.SelectSingleNode("database_id").InnerText
            pName = node.SelectSingleNode("database_name").InnerText
            pString = node.SelectSingleNode("database_string").InnerText
            If mysqlcomp.tryConnection(False, pString) = True Then
                Globals.databaseInfo = pString
                Globals.databaseName = pID + " - " + pName
                Globals.dabase = pName
                Exit For
            End If
        Next
    End Function
End Class

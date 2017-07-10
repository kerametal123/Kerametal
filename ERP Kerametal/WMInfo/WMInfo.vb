Imports System.Management
Public Class WMInfo
    Private objOS As ManagementObjectSearcher
    Private objCS As ManagementObjectSearcher
    Private objMgmt As ManagementObject
    Private m_strComputerName As String
    Private m_strManufacturer As String
    Private m_StrModel As String
    Private m_strOSName As String
    Private m_strOSVersion As String
    Private m_strSystemType As String
    Private m_strTPM As String
    Private m_strWindowsDir As String
    Dim mysqlinfo As New MySQLinfo
    ' MessageBox.Show("Hard Disk Size = " & dblSize.ToString() & " GB") 'Display Result
    Public Function GetHDFreeSpace(ByVal strDrive As String) As Double

        'Ensure Valid Drive Letter Entered, Else, Default To C
        If strDrive = "" OrElse strDrive Is Nothing Then

            strDrive = "C"

        End If

        'Make Use Of Win32_LogicalDisk To Obtain Hard Disk Properties
        Dim moHD As New ManagementObject("Win32_LogicalDisk.DeviceID=""" + strDrive + ":""")

        'Get Info
        moHD.[Get]()

        'Get Hard Disk Free Space


        Dim dblFree As Double 'Store Size

        dblFree = Math.Round(Convert.ToDouble(moHD("FreeSpace")) / 1024 / 1024 / 1024) 'Call GetHDFreeSpace Sub and Divide 3 Times By 1024 ( Byte ) To Give GB

        Return dblFree 'Display Result

    End Function

    Public Function GetHDSize(ByVal strDrive As String) As Double 'Get Size of Specified Disk

        'Ensure Valid Drive Letter Entered, Else, Default To C
        If strDrive = "" OrElse strDrive Is Nothing Then

            strDrive = "C"

        End If

        'Make Use Of Win32_LogicalDisk To Obtain Hard Disk Properties
        Dim moHD As New ManagementObject("Win32_LogicalDisk.DeviceID=""" + strDrive + ":""")

        'Get Info
        moHD.[Get]()

        'Get Hard Disk Size
        Dim dblSize As Double 'Store Size

        dblSize = Math.Round(Convert.ToDouble(moHD("Size")) / 1024 / 1024 / 1024) 'Call GetHDSize Sub and Divide 3 Times By 1024 ( Byte ) To Give GB 

        Return dblSize 'Display Result
    End Function
    Public Sub New()

        objOS = New ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem")
        objCS = New ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem")
        For Each objMgmt In objOS.Get


            m_strOSName = objMgmt("name").ToString()
            m_strOSVersion = objMgmt("version").ToString()
            m_strComputerName = objMgmt("csname").ToString()
            m_strWindowsDir = objMgmt("windowsdirectory").ToString()
        Next

        For Each objMgmt In objCS.Get
            m_strManufacturer = objMgmt("manufacturer").ToString()
            m_StrModel = objMgmt("model").ToString()
            m_strSystemType = objMgmt("systemtype").ToString
            m_strTPM = objMgmt("totalphysicalmemory").ToString()
        Next
    End Sub

    Public ReadOnly Property ComputerName()
        Get
            ComputerName = m_strComputerName
        End Get

    End Property
    Public ReadOnly Property Manufacturer()
        Get
            Manufacturer = m_strManufacturer
        End Get

    End Property
    Public ReadOnly Property Model()
        Get
            Model = m_StrModel
        End Get

    End Property
    Public ReadOnly Property OsName()
        Get
            OsName = m_strOSName
        End Get

    End Property

    Public ReadOnly Property OSVersion()
        Get
            OSVersion = m_strOSVersion
        End Get

    End Property
    Public ReadOnly Property SystemType()
        Get
            SystemType = m_strSystemType
        End Get

    End Property
    Public ReadOnly Property TotalPhysicalMemory()
        Get
            TotalPhysicalMemory = m_strTPM
        End Get

    End Property

    Public ReadOnly Property WindowsDirectory()
        Get
            WindowsDirectory = m_strWindowsDir
        End Get

    End Property

    ''' <summary>
    ''' Windows media intrumentation
    ''' </summary>
    ''' <returns></returns>
    Public Function upisWmi()
        mysqlinfo.upisWMI(GetHDSize("C"), GetHDFreeSpace("C"), m_strComputerName, m_strManufacturer, m_StrModel, m_strOSName, m_strOSVersion, m_strSystemType, m_strTPM, m_strWindowsDir)
        Return True
    End Function
End Class

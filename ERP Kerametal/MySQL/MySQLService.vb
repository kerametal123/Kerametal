Imports System
Imports System.Management
Imports System.ServiceProcess
Imports System.Diagnostics
Public Class MySQLService
    Dim status As String

    Public Function procMysql()

        Try
            Dim mo As New Management.ManagementObject("Win32_Service.Name=’mysql’")
            Globals.sqlstate = "Busy…"
            'Check if MySQL Service is installed. If not it will close the form.
            Try
                mo.Get()
            Catch ex As Exception
                'MessageBox.Show("Neednstall MySQL Server", "MySQL Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'Close()
            End Try

            'Check if the StartMode = Disabled. If so it will change it to Manual
            If mo("StartMode").ToString = "Disabled" Then
                Dim inParams, outParams As ManagementBaseObject
                Dim result As Integer

                'Get an input parameters object for this method
                inParams = mo.GetMethodParameters("ChangeStartMode")
                'Set the StartMode to ‘Manual’
                inParams("StartMode") = "Manual"
                outParams = mo.InvokeMethod("ChangeStartMode", inParams, Nothing)
                'Check for errors 0 means the request has been accepted.
                result = Convert.ToInt32(outParams("returnValue"))

                'Handle errors if any
                If result <> 0 Then
                    Dim myErrMsg As String = ""
                    Select Case result
                        Case 1
                            myErrMsg = "Zahtjev nije podržan."
                        Case 2
                            myErrMsg = "Nemate ovlasti za pokretanje Servisa."
                        Case 3
                            myErrMsg = "Ne možete ugasiti servise jer bi to uzokovalo prekid rada drugih servisa koji ovise o njemu."
                        Case 4
                            myErrMsg = "The requested control code is not valid, or it is unacceptable to the service."
                        Case 5
                            myErrMsg = "The requested control code cannot be sent to the service because the state of the service (Win32_BaseService State property) is equal to 0, 1, or 2."
                        Case 6
                            myErrMsg = "Servis je pokrenut."
                        Case 7
                            myErrMsg = "The service did not respond to the start request in a timely fashion."
                        Case 8
                            myErrMsg = "Interactive process."
                        Case 9
                            myErrMsg = "The directory path to the service executable file was not found."
                        Case 10
                            myErrMsg = "Servis je već pokrenut."
                        Case 11
                            myErrMsg = "The database to add a new service is locked."
                        Case 12
                            myErrMsg = "A dependency on which this service relies has been removed from the system."
                        Case 13
                            myErrMsg = "The service failed to find the service needed from a dependent service."
                        Case 14
                            myErrMsg = "The service has been disabled from the system."
                        Case 15
                            myErrMsg = " The service does not have the correct authentication to run on the system."
                        Case 16
                            myErrMsg = "This service is being removed from the system."
                        Case 17
                            myErrMsg = "There is no execution thread for the service."
                        Case 18
                            myErrMsg = "There are circular dependencies when starting the service."
                        Case 19
                            myErrMsg = "Već postoji servis sa istim imenom."
                        Case 20
                            myErrMsg = "There are invalid characters in the name of the service."
                        Case 21
                            myErrMsg = "Invalid parameters have been passed to the service."
                        Case 22
                            myErrMsg = "The account which this service is to run under is either invalid or lacks the permissions to run the service."
                        Case 23
                            myErrMsg = "The service exists in the database of services available from the system."
                        Case 24
                            myErrMsg = "Servis je pauziran u sistemu."

                    End Select
                    Throw New Exception("ChangeStartMode method error code " & result & ControlChars.NewLine & myErrMsg)
                End If
            End If

            'Check status of MySQL Server
            'If the service is running all is fine
            'Else it will wait for the Server to run, or attempt to start the Server
            'The status will be updated in the ToolStripStatusLabel tsStatus

            'ServiceControllerStatus Meanings
            '1 = Stopped – The Service is not running.
            '2 = StartPending – The Service is starting.
            '3 = StopPending – The Service is stopping.
            '4 = Running – The Service is running.
            '5 = ContinuePending – The Service continue is pending.
            '6 = PausePending – The Service pause is pending.
            '7 = Paused – The service is paused.
            Dim sc As New ServiceController("mysql")
            Select Case sc.Status
                Case 1
                    Globals.sqlstate = "MySQL Server is not running, please wait…"
                    sc.Start()
                    Globals.sqlstate = "Starting MySQL Server, please wait…"
                    sc.WaitForStatus(ServiceControllerStatus.Running)
                    Globals.sqlstate = "Ready"
                Case 2
                    Globals.sqlstate = "MySQL Server is starting, please wait…"
                    sc.WaitForStatus(ServiceControllerStatus.Running)
                    Globals.sqlstate = "Ready"
                Case 3
                    Globals.sqlstate = "MySQL Server is stopping, please wait…"
                    sc.WaitForStatus(ServiceControllerStatus.Stopped)
                    Globals.sqlstate = "Starting MySQL Server, please wait…"
                    sc.Start()
                    sc.WaitForStatus(ServiceControllerStatus.Running)
                    Globals.sqlstate = "Ready"
                Case 4
                    Globals.sqlstate = "Ready"
                Case 5, 6, 7
                    Globals.sqlstate = "MySQL Server is stopping, please wait…"
                    sc.Stop()
                    sc.WaitForStatus(ServiceControllerStatus.Stopped)
                    Globals.sqlstate = "Starting MySQL Server, please wait…"
                    sc.Start()
                    sc.WaitForStatus(ServiceControllerStatus.Running)
                    Globals.sqlstate = "Ready"
            End Select
        Catch ex As Exception
            ' MessageBox.Show(Err.Description, "MySQL Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Me.Close()
        End Try
        Console.Write(status)
        Return True
    End Function
End Class

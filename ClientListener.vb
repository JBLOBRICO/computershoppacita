Imports MySql.Data.MySqlClient
Imports System.Threading
Imports System.Runtime.InteropServices

Public Class ClientListener
    Private PCID As Integer = 1 ' <-- Set this PC's ID
    Private listenThread As Thread
    Private sessionActive As Boolean = False
    Private sessionStart As DateTime
    Private sessionFixed As Boolean = False
    Private sessionDuration As TimeSpan = TimeSpan.Zero
    Private notificationShown As Boolean = False
    Private ratePerHour As Decimal = 20D ' optional, for calculating usage if needed

    ' ====== Windows API for locking PC ======
    <DllImport("user32.dll")>
    Private Shared Sub LockWorkStation()
    End Sub

    Private Sub ClientListener_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        listenThread = New Thread(AddressOf ListenForCommands)
        listenThread.IsBackground = True
        listenThread.Start()

        ' Optional: timer to check fixed sessions
        Dim sessionTimer As New System.Windows.Forms.Timer()
        AddHandler sessionTimer.Tick, AddressOf SessionTimer_Tick
        sessionTimer.Interval = 1000
        sessionTimer.Start()
    End Sub

    ' ====== Listen for Commands ======
    Private Sub ListenForCommands()
        While True
            Try
                OpenConnection()
                Dim cmd As New MySqlCommand("SELECT CommandID, CommandType, CommandParameter FROM Commands WHERE PCID=@PCID AND Executed=0", conn)
                cmd.Parameters.AddWithValue("@PCID", PCID)
                Dim dr As MySqlDataReader = cmd.ExecuteReader()

                Dim commands As New List(Of (Integer, String, String))
                While dr.Read()
                    commands.Add((dr.GetInt32("CommandID"), dr.GetString("CommandType"), dr.GetString("CommandParameter")))
                End While
                dr.Close()

                For Each c In commands
                    Dim commandID = c.Item1
                    Dim type = c.Item2
                    Dim param = c.Item3
                    ExecuteCommand(type, param)

                    ' Mark executed
                    Dim updateCmd As New MySqlCommand("UPDATE Commands SET Executed=1 WHERE CommandID=@ID", conn)
                    updateCmd.Parameters.AddWithValue("@ID", commandID)
                    updateCmd.ExecuteNonQuery()
                Next

            Catch ex As Exception
                ' Optional: log error
            Finally
                CloseConnection()
            End Try
            Thread.Sleep(1000)
        End While
    End Sub

    ' ====== Execute Command ======
    Private Sub ExecuteCommand(commandType As String, param As String)
        Select Case commandType.ToLower()
            Case "start_session"
                Invoke(Sub()
                           If Not sessionActive Then
                               sessionActive = True
                               sessionStart = DateTime.Now
                               sessionFixed = False
                               sessionDuration = TimeSpan.Zero
                               notificationShown = False
                               MessageBox.Show("Session started!", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information)
                           End If
                       End Sub)

            Case "end_session"
                Invoke(Sub()
                           If sessionActive Then
                               EndSession()
                           End If
                       End Sub)

            Case "fixed_session"
                ' param = duration in minutes
                Invoke(Sub()
                           If Not sessionActive Then
                               sessionActive = True
                               sessionFixed = True
                               sessionStart = DateTime.Now
                               If Integer.TryParse(param, Nothing) Then
                                   sessionDuration = TimeSpan.FromMinutes(Convert.ToInt32(param))
                               End If
                               notificationShown = False
                               MessageBox.Show("Fixed session started! Duration: " & sessionDuration.ToString("hh\:mm\:ss"), "Client", MessageBoxButtons.OK, MessageBoxIcon.Information)
                           End If
                       End Sub)

            Case "notify"
                Invoke(Sub()
                           MessageBox.Show(param, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information)
                       End Sub)

            Case "shutdown"
                Invoke(Sub()
                           If MessageBox.Show("Shutdown command received. Proceed?", "Shutdown", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                               Process.Start("shutdown", "/s /t 5")
                           End If
                       End Sub)

            Case "restart"
                Invoke(Sub()
                           If MessageBox.Show("Restart command received. Proceed?", "Restart", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                               Process.Start("shutdown", "/r /t 5")
                           End If
                       End Sub)

            Case Else
                ' Unknown command
        End Select
    End Sub

    ' ====== Fixed Session Timer ======
    Private Sub SessionTimer_Tick(sender As Object, e As EventArgs)
        Try
            If sessionActive AndAlso sessionFixed Then
                Dim elapsed As TimeSpan = DateTime.Now - sessionStart
                Dim remaining As TimeSpan = sessionDuration - elapsed

                If remaining.TotalMinutes <= 5 AndAlso Not notificationShown Then
                    notificationShown = True
                    Invoke(Sub() MessageBox.Show("⏰ 5 minutes left on this fixed session!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information))
                End If

                If elapsed >= sessionDuration Then
                    ' End fixed session automatically
                    Invoke(Sub() EndSession())
                End If
            End If
        Catch
            ' Prevent crash
        End Try
    End Sub

    ' ====== End Session ======
    Private Sub EndSession()
        If sessionFixed Then
            MessageBox.Show("Fixed session ended automatically.", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Session ended.", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        ' Lock workstation
        LockWorkStation()

        ' Reset
        sessionActive = False
        sessionFixed = False
        sessionDuration = TimeSpan.Zero
        notificationShown = False
    End Sub

    ' ====== DB CONNECTION ======
    Private conn As MySqlConnection
    Private Sub OpenConnection()
        If conn Is Nothing Then
            conn = New MySqlConnection("server=localhost;user id=root;password=;database=pacitacmpdb;")
        End If
        If conn.State <> ConnectionState.Open Then conn.Open()
    End Sub
    Private Sub CloseConnection()
        If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then conn.Close()
    End Sub

    Private Sub ClientListener_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If listenThread IsNot Nothing AndAlso listenThread.IsAlive Then
            listenThread.Abort()
        End If
    End Sub
End Class

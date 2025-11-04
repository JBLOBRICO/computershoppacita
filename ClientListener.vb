Imports MySql.Data.MySqlClient
Imports System.Threading

Public Class ClientListener
    Private clientPCID As Integer = 1 ' <-- Set this PC's ID
    Private listenerThread As Thread
    Private updateTimer As System.Windows.Forms.Timer
    Private sessionActive As Boolean = False
    Private sessionStart As DateTime
    Private sessionFixed As Boolean = False
    Private sessionDuration As TimeSpan = TimeSpan.Zero
    Private notificationShown As Boolean = False
    Private ratePerHour As Decimal = 20D
    Private elapsedServerTime As TimeSpan = TimeSpan.Zero

    ' ====== FORM LOAD ======
    Private Sub ClientListener_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Start listening for server commands
        listenerThread = New Thread(AddressOf ListenForCommands)
        listenerThread.IsBackground = True
        listenerThread.Start()

        ' Timer to update UI with live elapsed time & amount
        updateTimer = New System.Windows.Forms.Timer()
        AddHandler updateTimer.Tick, AddressOf UpdateLiveTimer
        updateTimer.Interval = 1000
        updateTimer.Start()
    End Sub

    ' ====== Listen for Commands ======
    Private Sub ListenForCommands()
        While True
            Try
                OpenConnection()
                ' Get commands for this PC
                Dim cmd As New MySqlCommand("SELECT CommandID, CommandType, CommandParameter FROM Commands WHERE PCID=@PCID AND Executed=0", conn)
                cmd.Parameters.AddWithValue("@PCID", clientPCID)
                Dim dr As MySqlDataReader = cmd.ExecuteReader()

                Dim commands As New List(Of (Integer, String, String))
                While dr.Read()
                    commands.Add((dr.GetInt32("CommandID"), dr.GetString("CommandType"), dr.GetString("CommandParameter")))
                End While
                dr.Close()

                ' Execute commands
                For Each c In commands
                    Dim commandID = c.Item1
                    Dim type = c.Item2
                    Dim param = c.Item3
                    ExecuteCommand(type, param)

                    ' Mark command as executed
                    Dim updateCmd As New MySqlCommand("UPDATE Commands SET Executed=1 WHERE CommandID=@ID", conn)
                    updateCmd.Parameters.AddWithValue("@ID", commandID)
                    updateCmd.ExecuteNonQuery()
                Next
            Catch
                ' Prevent crash
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
                           sessionActive = True
                           sessionStart = DateTime.Now
                           sessionFixed = False
                           sessionDuration = TimeSpan.Zero
                           notificationShown = False
                           elapsedServerTime = TimeSpan.Zero
                           MessageBox.Show("Session started by server.", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information)
                       End Sub)

            Case "fixed_session"
                Invoke(Sub()
                           sessionActive = True
                           sessionFixed = True
                           sessionStart = DateTime.Now
                           If Integer.TryParse(param, Nothing) Then
                               sessionDuration = TimeSpan.FromMinutes(Convert.ToInt32(param))
                           End If
                           notificationShown = False
                           elapsedServerTime = TimeSpan.Zero
                           MessageBox.Show("Fixed session started for " & sessionDuration.ToString("hh\:mm\:ss"), "Client", MessageBoxButtons.OK, MessageBoxIcon.Information)
                       End Sub)

            Case "end_session"
                Invoke(Sub()
                           EndSession()
                       End Sub)

            Case "shutdown"
                Invoke(Sub()
                           MessageBox.Show("Shutdown command received.", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information)
                       End Sub)

            Case "message"
                Invoke(Sub()
                           MessageBox.Show("Server Message: " & param, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                       End Sub)
        End Select
    End Sub

    ' ====== Update Live Timer ======
    Private Sub UpdateLiveTimer(sender As Object, e As EventArgs)
        If sessionActive Then
            elapsedServerTime = DateTime.Now - sessionStart
            Dim totalAmount As Decimal = Math.Round(CDec(elapsedServerTime.TotalHours) * ratePerHour, 2)

            ' Update timer label
            lblTimer.Text = $"Usage Time: {elapsedServerTime:hh\:mm\:ss} | ₱{totalAmount:F2}"

            ' Notify 5 minutes left for fixed session
            If sessionFixed AndAlso (sessionDuration - elapsedServerTime).TotalMinutes <= 5 AndAlso Not notificationShown Then
                notificationShown = True
                MessageBox.Show("⏰ 5 minutes left on this fixed session!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            ' Change panel color based on session type
            Dim remaining As TimeSpan = sessionDuration - elapsedServerTime
            If sessionFixed AndAlso remaining.TotalMinutes <= 5 Then
                pnlTimer.BackColor = Color.Orange
            Else
                pnlTimer.BackColor = Color.Green
            End If
        Else
            pnlTimer.BackColor = Color.Gray
            lblTimer.Text = "Usage Time: 00:00 | ₱0.00"
        End If
    End Sub

    ' ====== End Session ======
    Private Sub EndSession()
        If sessionFixed Then
            MessageBox.Show("Fixed session ended automatically.", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Session ended.", "Client", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        sessionActive = False
        sessionFixed = False
        sessionDuration = TimeSpan.Zero
        notificationShown = False
        elapsedServerTime = TimeSpan.Zero
    End Sub

    ' ====== DB Connection ======
    Private conn As MySqlConnection
    Private dr As MySqlDataReader
    Private Sub OpenConnection()
        If conn Is Nothing Then
            conn = New MySqlConnection("server=localhost;user id=root;password=;database=pacitacmpdb;")
        End If
        If conn.State <> ConnectionState.Open Then conn.Open()
    End Sub
    Private Sub CloseConnection()
        If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then conn.Close()
    End Sub

    ' ====== Form Closing ======
    Private Sub ClientListener_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If listenerThread IsNot Nothing AndAlso listenerThread.IsAlive Then listenerThread.Abort()
    End Sub
End Class

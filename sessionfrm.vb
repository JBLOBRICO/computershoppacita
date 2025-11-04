Imports MySql.Data.MySqlClient

Public Class sessionfrm
    ' ====== CONFIG ======
    Private ratePerHour As Decimal = 20D
    Private PCStatus As New Dictionary(Of Integer, String)
    Private SelectedPCID As Integer = -1
    Private SelectedPCName As String = ""
    Private notificationShown As New Dictionary(Of Integer, Boolean)
    Private PCStartTimes As New Dictionary(Of Integer, DateTime)
    Private PCFixedSessions As New Dictionary(Of Integer, Boolean)
    Private PCDurations As New Dictionary(Of Integer, TimeSpan)
    Private isLoading As Boolean = False

    ' ====== FORM LOAD ======
    Private Sub sessionfrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            isLoading = True
            If conn Is Nothing Then
                MessageBox.Show("Database connection is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            LoadComputers()
            lblTimer.Text = "Usage Time: 00:00 | ₱0.00"
            txtSales.Enabled = False

            If cmbSessionType IsNot Nothing Then
                AddHandler cmbSessionType.SelectedIndexChanged, AddressOf cmbSessionType_SelectedIndexChanged
            End If

            If btnAddMinutes IsNot Nothing Then
                AddHandler btnAddMinutes.Click, AddressOf btnAddMinutes_Click
            End If

            If sessionTimer IsNot Nothing Then
                sessionTimer.Interval = 1000
                sessionTimer.Start()
            End If
        Catch ex As Exception
            MessageBox.Show("Error during form load: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            isLoading = False
        End Try
    End Sub

    Private Sub cmbSessionType_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If cmbSessionType.SelectedItem IsNot Nothing Then
                txtSales.Enabled = (cmbSessionType.SelectedItem.ToString() = "Fixed")
            Else
                txtSales.Enabled = False
            End If
        Catch
        End Try
    End Sub

    ' ====== LOAD COMPUTERS ======
    Private Sub LoadComputers()
        If conn Is Nothing Then Return
        Try
            pnlComputers.Controls.Clear()
            OpenConnection()

            Dim query As String = "SELECT * FROM computers"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader()

            While dr.Read()
                If dr.IsDBNull(dr.GetOrdinal("ComputerID")) Then Continue While
                Dim pcID As Integer = dr("ComputerID")
                Dim pcName As String = If(Not dr.IsDBNull(dr.GetOrdinal("ComputerName")), dr("ComputerName").ToString(), "Unknown PC")
                Dim status As String = If(Not dr.IsDBNull(dr.GetOrdinal("Status")), dr("Status").ToString(), "Available")

                If PCStartTimes.ContainsKey(pcID) Then status = "In Use"

                Dim btnPC As New Button() With {
                    .Text = pcName,
                    .Name = "btnPC" & pcID,
                    .Tag = pcID,
                    .Width = 150,
                    .Height = 100,
                    .Margin = New Padding(10),
                    .Font = New Font("Segoe UI", 9, FontStyle.Bold),
                    .FlatStyle = FlatStyle.Flat
                }

                Select Case status
                    Case "Available" : btnPC.BackColor = Color.LightGray
                    Case "In Use" : btnPC.BackColor = Color.LightGreen
                    Case "Maintenance" : btnPC.BackColor = Color.IndianRed
                    Case Else : btnPC.BackColor = Color.LightGray
                End Select

                AddHandler btnPC.Click, AddressOf PC_Click
                pnlComputers.Controls.Add(btnPC)
                PCStatus(pcID) = status
            End While
            dr.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading computers: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' ====== SELECT PC ======
    Private Sub PC_Click(sender As Object, e As EventArgs)
        Try
            Dim btn As Button = TryCast(sender, Button)
            If btn Is Nothing OrElse btn.Tag Is Nothing Then Return

            SelectedPCID = CInt(btn.Tag)
            SelectedPCName = btn.Text
            lblPCName.Text = "PC Name: " & SelectedPCName

            If PCStatus.ContainsKey(SelectedPCID) Then
                lblStatus.Text = "Status: " & PCStatus(SelectedPCID)
            Else
                lblStatus.Text = "Status: Unknown"
            End If

            UpdateTimerLabel(SelectedPCID)
        Catch ex As Exception
            MessageBox.Show("Error selecting PC: " & ex.Message)
        End Try
    End Sub

    ' ====== SEND COMMAND TO CLIENT ======
    Private Sub SendCommand(PCID As Integer, commandType As String, Optional param As String = "")
        Try
            OpenConnection()
            Dim query As String = "INSERT INTO Commands (PCID, CommandType, CommandParameter) VALUES (@PCID, @CommandType, @Param)"
            cmd = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@PCID", PCID)
            cmd.Parameters.AddWithValue("@CommandType", commandType)
            cmd.Parameters.AddWithValue("@Param", param)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Error sending command: " & ex.Message)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' ====== START SESSION ======
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Try
            If SelectedPCID = -1 Then
                MessageBox.Show("Please select a PC first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            If Not PCStatus.ContainsKey(SelectedPCID) Then
                MessageBox.Show("Invalid PC selected.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim status = PCStatus(SelectedPCID)
            If status = "In Use" Then
                MessageBox.Show("This PC is already in use.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            ElseIf status = "Maintenance" Then
                MessageBox.Show("This PC is under maintenance.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim sessionType As String = cmbSessionType.SelectedItem?.ToString()
            If String.IsNullOrEmpty(sessionType) Then
                MessageBox.Show("Please select a session type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim fixedAmount As Decimal = 0
            If sessionType = "Fixed" Then
                If String.IsNullOrWhiteSpace(txtSales.Text) Then
                    MessageBox.Show("Please enter a fixed amount.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                If Not Decimal.TryParse(txtSales.Text, fixedAmount) Then
                    MessageBox.Show("Enter a valid numeric amount.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                If fixedAmount <= 0 Then
                    MessageBox.Show("Amount must be greater than 0.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                Dim duration As TimeSpan = TimeSpan.FromMinutes((fixedAmount / ratePerHour) * 60)
                PCFixedSessions(SelectedPCID) = True
                PCDurations(SelectedPCID) = duration
                notificationShown(SelectedPCID) = False
            Else
                PCFixedSessions(SelectedPCID) = False
            End If

            If PCStartTimes.ContainsKey(SelectedPCID) Then
                MessageBox.Show("This PC already has an active session.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            PCStartTimes(SelectedPCID) = DateTime.Now
            PCStatus(SelectedPCID) = "In Use"
            LoadComputers()
            UpdateTimerLabel(SelectedPCID)

            ' ✅ Send command to client to start session
            SendCommand(SelectedPCID, "start_session")

            MessageBox.Show("Session started for " & SelectedPCName)
        Catch ex As Exception
            MessageBox.Show("Error starting session: " & ex.Message)
        End Try
    End Sub

    ' ====== END SESSION ======
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        Try
            If SelectedPCID = -1 Then
                MessageBox.Show("Please select a PC first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            If Not PCStartTimes.ContainsKey(SelectedPCID) Then
                MessageBox.Show("This PC has no active session.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If PCFixedSessions.ContainsKey(SelectedPCID) AndAlso PCFixedSessions(SelectedPCID) Then
                MessageBox.Show("Fixed sessions cannot be ended manually.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' ✅ Send command to client to end session
            SendCommand(SelectedPCID, "end_session")

            EndSession(SelectedPCID)
        Catch ex As Exception
            MessageBox.Show("Error ending session: " & ex.Message)
        End Try
    End Sub

    ' ====== ADD EXTRA MINUTES ======
    Private Sub btnAddMinutes_Click(sender As Object, e As EventArgs)
        Try
            If SelectedPCID = -1 Then
                MessageBox.Show("Please select a PC first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            If String.IsNullOrWhiteSpace(txtExtraMinutes.Text) Then
                MessageBox.Show("Enter extra minutes value.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim extra As Integer
            If Integer.TryParse(txtExtraMinutes.Text, extra) AndAlso extra > 0 Then
                If PCFixedSessions.ContainsKey(SelectedPCID) AndAlso PCFixedSessions(SelectedPCID) Then
                    PCDurations(SelectedPCID) = PCDurations(SelectedPCID).Add(TimeSpan.FromMinutes(extra))
                    MessageBox.Show(extra & " minutes added.")
                Else
                    MessageBox.Show("Extra minutes can only be added to Fixed sessions.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                MessageBox.Show("Enter a valid positive number for extra minutes.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            txtExtraMinutes.Clear()
        Catch ex As Exception
            MessageBox.Show("Error adding minutes: " & ex.Message)
        End Try
    End Sub

    ' ====== TIMER TICK ======
    Private Sub sessionTimer_Tick(sender As Object, e As EventArgs) Handles sessionTimer.Tick
        Try
            For Each pcID In PCStartTimes.Keys.ToList()
                Dim elapsed As TimeSpan = DateTime.Now - PCStartTimes(pcID)
                Dim totalAmount As Decimal = Math.Round(CDec(elapsed.TotalHours) * ratePerHour, 2)

                For Each ctrl As Control In pnlComputers.Controls
                    If TypeOf ctrl Is Button AndAlso ctrl.Tag IsNot Nothing AndAlso CInt(ctrl.Tag) = pcID Then
                        ctrl.Text = $"{SelectedPCName}{vbLf}{elapsed:hh\:mm\:ss} | ₱{totalAmount:F2}"
                    End If
                Next

                If pcID = SelectedPCID Then
                    lblTimer.Text = $"Usage Time: {elapsed:hh\:mm\:ss} | ₱{totalAmount:F2}"
                End If

                If PCFixedSessions.ContainsKey(pcID) AndAlso PCFixedSessions(pcID) Then
                    Dim duration As TimeSpan = PCDurations(pcID)
                    Dim remaining As TimeSpan = duration - elapsed

                    If remaining.TotalMinutes <= 5 AndAlso Not notificationShown(pcID) Then
                        MessageBox.Show("⏰ 5 minutes left on this session!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        notificationShown(pcID) = True
                    End If

                    If elapsed >= duration Then
                        ' ✅ Send end_session command automatically
                        SendCommand(pcID, "end_session")
                        EndSession(pcID, autoEnd:=True)
                    End If
                End If
            Next
        Catch ex As Exception
            ' Prevent timer crash
        End Try
    End Sub

    ' ====== END SESSION HELPER ======
    Private Sub EndSession(pcID As Integer, Optional autoEnd As Boolean = False)
        Try
            If Not PCStartTimes.ContainsKey(pcID) Then Return

            Dim elapsed As TimeSpan = DateTime.Now - PCStartTimes(pcID)
            Dim totalAmount As Decimal = Math.Round(CDec(elapsed.TotalHours) * ratePerHour, 2)

            PCStartTimes.Remove(pcID)
            If PCFixedSessions.ContainsKey(pcID) Then PCFixedSessions.Remove(pcID)
            If PCDurations.ContainsKey(pcID) Then PCDurations.Remove(pcID)
            If notificationShown.ContainsKey(pcID) Then notificationShown.Remove(pcID)
            PCStatus(pcID) = "Available"

            If pcID = SelectedPCID Then lblTimer.Text = "Usage Time: 00:00 | ₱0.00"
            LoadComputers()

            If autoEnd Then
                MessageBox.Show("Fixed session ended automatically.", "Session Ended", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Session ended. Total Amount: ₱" & totalAmount.ToString("F2"))
            End If
        Catch ex As Exception
            MessageBox.Show("Error ending session: " & ex.Message)
        End Try
    End Sub

    ' ====== UPDATE TIMER LABEL ======
    Private Sub UpdateTimerLabel(pcID As Integer)
        Try
            If PCStartTimes.ContainsKey(pcID) Then
                Dim elapsed As TimeSpan = DateTime.Now - PCStartTimes(pcID)
                Dim totalAmount As Decimal = Math.Round(CDec(elapsed.TotalHours) * ratePerHour, 2)
                lblTimer.Text = $"Usage Time: {elapsed:hh\:mm\:ss} | ₱{totalAmount:F2}"
            Else
                lblTimer.Text = "Usage Time: 00:00 | ₱0.00"
            End If
        Catch
        End Try
    End Sub

    ' ====== SHUTDOWN PC ======
    ' ====== SHUTDOWN & SEND MESSAGE VALIDATION COMBINED ======

    ' ====== SHUTDOWN PC ======
    Private Sub btnShutdown_Click(sender As Object, e As EventArgs) Handles btnShutdown.Click
        Try
            ' ✅ Validation: PC must be selected
            If SelectedPCID = -1 Then
                MessageBox.Show("Please select a PC first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' ✅ Validation: Check PC status exists
            If Not PCStatus.ContainsKey(SelectedPCID) Then
                MessageBox.Show("Invalid PC selected.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim status = PCStatus(SelectedPCID)

            ' ✅ Validation: Cannot shutdown if PC under maintenance
            If status = "Maintenance" Then
                MessageBox.Show("Cannot shutdown. PC is under maintenance.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' ✅ Validation: Warning if PC is not in use
            If Not PCStartTimes.ContainsKey(SelectedPCID) Then
                Dim confirm As DialogResult = MessageBox.Show("This PC is not in use. Do you still want to shutdown?",
                                                          "Confirm Shutdown", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If confirm = DialogResult.No Then Return
            End If

            ' ✅ Send shutdown command
            SendCommand(SelectedPCID, "shutdown")
            MessageBox.Show("Shutdown command sent to " & SelectedPCName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error sending shutdown: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ====== SEND MESSAGE ======
    Private Sub btnSendMsg_Click(sender As Object, e As EventArgs) Handles btnSendMsg.Click
        Try
            ' ✅ Validation: PC must be selected
            If SelectedPCID = -1 Then
                MessageBox.Show("Please select a PC first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' ✅ Validation: Check PC status exists
            If Not PCStatus.ContainsKey(SelectedPCID) Then
                MessageBox.Show("Invalid PC selected.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim status = PCStatus(SelectedPCID)

            ' ✅ Validation: Cannot send message if PC under maintenance
            If status = "Maintenance" Then
                MessageBox.Show("Cannot send message. PC is under maintenance.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' ✅ Validation: Warning if PC is not in use
            If Not PCStartTimes.ContainsKey(SelectedPCID) Then
                Dim confirm As DialogResult = MessageBox.Show("This PC is not currently in use. Do you still want to send a message?",
                                                          "Confirm Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If confirm = DialogResult.No Then Return
            End If

            ' ✅ Validation: Message cannot be empty
            If String.IsNullOrWhiteSpace(txtMessage.Text) Then
                MessageBox.Show("Enter a message first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' ✅ Optional: Maximum message length
            If txtMessage.Text.Length > 250 Then
                MessageBox.Show("Message cannot exceed 250 characters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' ✅ Send message command
            SendCommand(SelectedPCID, "message", txtMessage.Text.Trim())
            MessageBox.Show("Message sent to " & SelectedPCName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtMessage.Clear()
        Catch ex As Exception
            MessageBox.Show("Error sending message: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ' ====== LOGOUT HANDLING ======
    Public Function CanLogout() As Boolean
        Try
            If PCStartTimes.Count > 0 Then
                MessageBox.Show("Cannot logout while PCs have active sessions.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
            Return True
        Catch
            Return False
        End Try
    End Function

    Private Sub pnlComputers_Paint(sender As Object, e As PaintEventArgs) Handles pnlComputers.Paint

    End Sub
End Class

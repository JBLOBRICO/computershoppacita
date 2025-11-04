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
    Private ActiveSalesIDs As New Dictionary(Of Integer, Integer) ' ✅ store SaleID para sure update
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

            AddHandler cmbSessionType.SelectedIndexChanged, AddressOf cmbSessionType_SelectedIndexChanged
            AddHandler btnAddMinutes.Click, AddressOf btnAddMinutes_Click

            sessionTimer.Interval = 1000
            sessionTimer.Start()
        Catch ex As Exception
            MessageBox.Show("Error during form load: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            isLoading = False
        End Try
    End Sub

    Private Sub cmbSessionType_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtSales.Enabled = (cmbSessionType.SelectedItem?.ToString() = "Fixed")
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
        Dim btn As Button = TryCast(sender, Button)
        If btn Is Nothing OrElse btn.Tag Is Nothing Then Return

        SelectedPCID = CInt(btn.Tag)
        SelectedPCName = btn.Text.Split(vbLf)(0)
        lblPCName.Text = "PC Name: " & SelectedPCName
        lblStatus.Text = "Status: " & If(PCStatus.ContainsKey(SelectedPCID), PCStatus(SelectedPCID), "Unknown")
        UpdateTimerLabel(SelectedPCID)
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
            MessageBox.Show("Error sending command: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' ====== START SESSION ======
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        If SelectedPCID = -1 Then
            MessageBox.Show("Please select a PC first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If PCStatus.ContainsKey(SelectedPCID) AndAlso PCStatus(SelectedPCID) = "In Use" Then
            MessageBox.Show("This PC is already in use.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim sessionType As String = cmbSessionType.SelectedItem?.ToString()
        If String.IsNullOrEmpty(sessionType) Then
            MessageBox.Show("Please select a session type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim fixedAmount As Decimal = 0
        If sessionType = "Fixed" Then
            If Not Decimal.TryParse(txtSales.Text, fixedAmount) OrElse fixedAmount <= 0 Then
                MessageBox.Show("Enter a valid fixed amount.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            PCFixedSessions(SelectedPCID) = True
            PCDurations(SelectedPCID) = TimeSpan.FromMinutes((fixedAmount / ratePerHour) * 60)
            notificationShown(SelectedPCID) = False
        Else
            PCFixedSessions(SelectedPCID) = False
        End If

        Dim startTime As DateTime = DateTime.Now
        OpenConnection()
        Dim insertQuery As String = "INSERT INTO sales (ComputerID, UserID, StartTime, RatePerHour, PaymentStatus) VALUES (@ComputerID,@UserID,@StartTime,@RatePerHour,'Unpaid')"
        cmd = New MySqlCommand(insertQuery, conn)
        cmd.Parameters.AddWithValue("@ComputerID", SelectedPCID)
        cmd.Parameters.AddWithValue("@UserID", LoggedInUserID)
        cmd.Parameters.AddWithValue("@StartTime", startTime)
        cmd.Parameters.AddWithValue("@RatePerHour", ratePerHour)
        cmd.ExecuteNonQuery()

        ' ✅ Get the new SaleID
        Dim saleID As Integer = CInt(cmd.LastInsertedId)
        ActiveSalesIDs(SelectedPCID) = saleID

        CloseConnection()

        PCStartTimes(SelectedPCID) = startTime
        If Not PCDurations.ContainsKey(SelectedPCID) Then PCDurations(SelectedPCID) = TimeSpan.Zero
        PCStatus(SelectedPCID) = "In Use"
        LoadComputers()

        ' Send command to client
        If sessionType = "Fixed" Then
            SendCommand(SelectedPCID, "fixed_session", CInt(PCDurations(SelectedPCID).TotalMinutes).ToString())
        Else
            SendCommand(SelectedPCID, "start_session")
        End If

        MessageBox.Show("Session started for " & SelectedPCName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' ====== END SESSION ======
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        If SelectedPCID = -1 OrElse Not PCStartTimes.ContainsKey(SelectedPCID) Then
            MessageBox.Show("This PC has no active session.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Are you sure you want to end this session?", "Confirm End Session", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            EndSession(SelectedPCID)
        End If
    End Sub

    Private Sub EndSession(pcID As Integer)
        If Not PCStartTimes.ContainsKey(pcID) Then Return
        Dim startTime As DateTime = PCStartTimes(pcID)
        Dim endTime As DateTime = DateTime.Now
        Dim elapsed As TimeSpan = endTime - startTime
        Dim totalAmount As Decimal

        If PCFixedSessions.ContainsKey(pcID) AndAlso PCFixedSessions(pcID) Then
            totalAmount = Math.Round(CDec(PCDurations(pcID).TotalHours * ratePerHour), 2)
        Else
            totalAmount = Math.Round(CDec(elapsed.TotalHours * ratePerHour), 2)
        End If

        ' ✅ Update by SaleID (guaranteed match)
        OpenConnection()
        Dim updateQuery As String = "UPDATE sales SET EndTime=@EndTime, TotalAmount=@TotalAmount, PaymentStatus='Paid' WHERE SaleID=@SaleID"
        cmd = New MySqlCommand(updateQuery, conn)
        cmd.Parameters.AddWithValue("@EndTime", endTime)
        cmd.Parameters.AddWithValue("@TotalAmount", totalAmount)
        cmd.Parameters.AddWithValue("@SaleID", ActiveSalesIDs(pcID))
        cmd.ExecuteNonQuery()
        CloseConnection()

        ' cleanup
        ActiveSalesIDs.Remove(pcID)
        PCStartTimes.Remove(pcID)
        If PCFixedSessions.ContainsKey(pcID) Then PCFixedSessions.Remove(pcID)
        If PCDurations.ContainsKey(pcID) Then PCDurations.Remove(pcID)
        If notificationShown.ContainsKey(pcID) Then notificationShown.Remove(pcID)
        PCStatus(pcID) = "Available"

        lblTimer.Text = "Usage Time: 00:00 | ₱0.00"
        LoadComputers()
        MessageBox.Show("Session ended. Total Amount: ₱" & totalAmount.ToString("F2"), "Session Ended", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' ====== ADD EXTRA AMOUNT ======
    Private Sub btnAddMinutes_Click(sender As Object, e As EventArgs)
        If SelectedPCID = -1 Then
            MessageBox.Show("Please select a PC first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim extraAmount As Decimal
        If Decimal.TryParse(txtExtraMinutes.Text, extraAmount) AndAlso extraAmount > 0 Then
            If PCFixedSessions.ContainsKey(SelectedPCID) AndAlso PCFixedSessions(SelectedPCID) Then
                PCDurations(SelectedPCID) = PCDurations(SelectedPCID).Add(TimeSpan.FromMinutes((extraAmount / ratePerHour) * 60))
                notificationShown(SelectedPCID) = False
                MessageBox.Show("Added ₱" & extraAmount.ToString("F2") & " to the session.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Extra amount can only be added to Fixed sessions.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("Enter a valid positive number for extra amount.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        txtExtraMinutes.Clear()
    End Sub

    ' ====== TIMER TICK ======
    Private Sub sessionTimer_Tick(sender As Object, e As EventArgs) Handles sessionTimer.Tick
        For Each pcID In PCStartTimes.Keys.ToList()
            Dim elapsed As TimeSpan = DateTime.Now - PCStartTimes(pcID)
            Dim amount As Decimal
            Dim displayText As String = SelectedPCName

            If PCFixedSessions.ContainsKey(pcID) AndAlso PCFixedSessions(pcID) Then
                Dim remaining As TimeSpan = PCDurations(pcID) - elapsed
                amount = Math.Round(CDec(PCDurations(pcID).TotalHours * ratePerHour), 2)
                If remaining.TotalMinutes <= 5 AndAlso Not notificationShown(pcID) Then
                    MessageBox.Show("⏰ 5 minutes left on this session!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    notificationShown(pcID) = True
                End If
                If remaining.TotalSeconds <= 0 Then
                    SendCommand(pcID, "end_session")
                    EndSession(pcID)
                    Continue For
                End If
                displayText &= vbLf & $"Remaining: {remaining:hh\:mm\:ss} | ₱{amount:F2}"
            Else
                amount = Math.Round(CDec(elapsed.TotalHours * ratePerHour), 2)
                displayText &= vbLf & $"{elapsed:hh\:mm\:ss} | ₱{amount:F2}"
            End If

            For Each ctrl As Control In pnlComputers.Controls
                If TypeOf ctrl Is Button AndAlso ctrl.Tag IsNot Nothing AndAlso CInt(ctrl.Tag) = pcID Then
                    ctrl.Text = displayText
                End If
            Next

            If pcID = SelectedPCID Then
                lblTimer.Text = displayText.Split(vbLf)(1)
            End If
        Next
    End Sub

    ' ====== UPDATE TIMER LABEL ======
    Private Sub UpdateTimerLabel(pcID As Integer)
        If PCStartTimes.ContainsKey(pcID) Then
            Dim elapsed As TimeSpan = DateTime.Now - PCStartTimes(pcID)
            Dim labelText As String = If(PCFixedSessions.ContainsKey(pcID) AndAlso PCFixedSessions(pcID),
                                         $"Remaining: {PCDurations(pcID) - elapsed:hh\:mm\:ss} | ₱{Math.Round(CDec(PCDurations(pcID).TotalHours * ratePerHour), 2):F2}",
                                         $"Usage Time: {elapsed:hh\:mm\:ss} | ₱{Math.Round(CDec(elapsed.TotalHours * ratePerHour), 2):F2}")
            lblTimer.Text = labelText
        Else
            lblTimer.Text = "Usage Time: 00:00 | ₱0.00"
        End If
    End Sub

    ' ====== CAN LOGOUT ======
    Public ReadOnly Property CanLogout As Boolean
        Get
            Return PCStartTimes.Count = 0
        End Get
    End Property

    ' ====== DB CONNECTION HELPERS ======
    Private Sub OpenConnection()
        If conn.State <> ConnectionState.Open Then conn.Open()
    End Sub

    Private Sub CloseConnection()
        If conn.State <> ConnectionState.Closed Then conn.Close()
    End Sub

    Private Sub pnlComputers_Paint(sender As Object, e As PaintEventArgs) Handles pnlComputers.Paint
    End Sub
End Class

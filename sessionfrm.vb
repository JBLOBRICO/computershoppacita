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
    Private ActiveSalesIDs As New Dictionary(Of Integer, Integer)
    Private isLoading As Boolean = False

    ' ====== FORM LOAD ======
    Private Sub sessionfrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            isLoading = True
            If conn Is Nothing Then
                MessageBox.Show("Database connection not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show("Error during form load: " & ex.Message)
        Finally
            isLoading = False
        End Try
    End Sub

    Private Sub cmbSessionType_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtSales.Enabled = (cmbSessionType.SelectedItem?.ToString() = "Fixed")
    End Sub

    ' ====== LOAD COMPUTERS ======
    Private Sub LoadComputers()
        Try
            pnlComputers.Controls.Clear()
            OpenConnection()

            Dim query As String = "SELECT * FROM computers"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader()

            While dr.Read()
                Dim pcID As Integer = dr("ComputerID")
                Dim pcName As String = dr("ComputerName").ToString()
                Dim status As String = If(PCStatus.ContainsKey(pcID), PCStatus(pcID), dr("Status").ToString())

                If PCStartTimes.ContainsKey(pcID) Then status = "In Use"

                Dim btnPC As New Button() With {
                    .Text = pcName,
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
                End Select

                AddHandler btnPC.Click, AddressOf PC_Click
                pnlComputers.Controls.Add(btnPC)
                PCStatus(pcID) = status
            End While
            dr.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading computers: " & ex.Message)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' ====== SELECT PC ======
    Private Sub PC_Click(sender As Object, e As EventArgs)
        Dim btn As Button = TryCast(sender, Button)
        If btn Is Nothing Then Return

        SelectedPCID = CInt(btn.Tag)
        SelectedPCName = btn.Text.Split(vbLf)(0)

        lblPCName.Text = "PC Name: " & SelectedPCName
        lblStatus.Text = "Status: " & If(PCStatus.ContainsKey(SelectedPCID), PCStatus(SelectedPCID), "Unknown")
        UpdateTimerLabel(SelectedPCID)
    End Sub

    ' ====== SEND COMMAND ======
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
        If SelectedPCID = -1 Then
            MessageBox.Show("Please select a PC first.")
            Return
        End If

        If PCStatus.ContainsKey(SelectedPCID) AndAlso PCStatus(SelectedPCID) = "In Use" Then
            MessageBox.Show("This PC is already in use.")
            Return
        End If

        Dim sessionType As String = cmbSessionType.SelectedItem?.ToString()
        If String.IsNullOrEmpty(sessionType) Then
            MessageBox.Show("Please select a session type.")
            Return
        End If

        Dim fixedAmount As Decimal = 0
        Dim startTime As DateTime = DateTime.Now

        OpenConnection()

        Dim insertQuery As String = ""
        cmd = New MySqlCommand()

        If sessionType = "Fixed" Then
            If Not Decimal.TryParse(txtSales.Text, fixedAmount) OrElse fixedAmount <= 0 Then
                MessageBox.Show("Enter a valid fixed amount.")
                CloseConnection()
                Return
            End If

            PCFixedSessions(SelectedPCID) = True
            PCDurations(SelectedPCID) = TimeSpan.FromMinutes((fixedAmount / ratePerHour) * 60)
            notificationShown(SelectedPCID) = False

            ' Insert FIXED session: Paid already, no EndTime yet
            insertQuery = "INSERT INTO sales (ComputerID, UserID, StartTime, RatePerHour, TotalAmount, PaymentStatus) 
                           VALUES (@ComputerID,@UserID,@StartTime,@RatePerHour,@TotalAmount,'Paid')"
            cmd = New MySqlCommand(insertQuery, conn)
            cmd.Parameters.AddWithValue("@TotalAmount", fixedAmount)

        Else
            PCFixedSessions(SelectedPCID) = False
            ' Insert REGULAR session: unpaid until manually ended
            insertQuery = "INSERT INTO sales (ComputerID, UserID, StartTime, RatePerHour, PaymentStatus) 
                           VALUES (@ComputerID,@UserID,@StartTime,@RatePerHour,'Unpaid')"
            cmd = New MySqlCommand(insertQuery, conn)
        End If

        cmd.Parameters.AddWithValue("@ComputerID", SelectedPCID)
        cmd.Parameters.AddWithValue("@UserID", LoggedInUserID)
        cmd.Parameters.AddWithValue("@StartTime", startTime)
        cmd.Parameters.AddWithValue("@RatePerHour", ratePerHour)
        cmd.ExecuteNonQuery()

        Dim saleID As Integer = CInt(cmd.LastInsertedId)
        ActiveSalesIDs(SelectedPCID) = saleID
        CloseConnection()

        ' Update state
        PCStartTimes(SelectedPCID) = startTime
        PCStatus(SelectedPCID) = "In Use"
        UpdatePCButton(SelectedPCID)

        ' Send command to client
        If sessionType = "Fixed" Then
            SendCommand(SelectedPCID, "fixed_session", CInt(PCDurations(SelectedPCID).TotalMinutes).ToString())
        Else
            SendCommand(SelectedPCID, "start_session")
        End If

        MessageBox.Show("Session started for " & SelectedPCName)
    End Sub

    ' ====== END SESSION ======
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        If SelectedPCID = -1 OrElse Not PCStartTimes.ContainsKey(SelectedPCID) Then
            MessageBox.Show("This PC has no active session.")
            Return
        End If

        If PCFixedSessions.ContainsKey(SelectedPCID) AndAlso PCFixedSessions(SelectedPCID) Then
            MessageBox.Show("⚠ Cannot manually end a Fixed session. It will end automatically when time is up.",
                            "Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Are you sure you want to end this session?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            EndSession(SelectedPCID)
        End If
    End Sub

    Private Sub EndSession(pcID As Integer)
        If Not PCStartTimes.ContainsKey(pcID) Then Return

        Dim startTime As DateTime = PCStartTimes(pcID)
        Dim endTime As DateTime = DateTime.Now
        Dim elapsed As TimeSpan = endTime - startTime
        Dim totalAmount As Decimal = Math.Round(CDec(elapsed.TotalHours * ratePerHour), 2)

        Try
            OpenConnection()

            Dim saleID As Integer = If(ActiveSalesIDs.ContainsKey(pcID), ActiveSalesIDs(pcID), 0)
            If saleID = 0 Then
                Dim q As String = "SELECT SaleID FROM sales WHERE ComputerID=@PCID AND PaymentStatus='Unpaid' ORDER BY StartTime DESC LIMIT 1"
                Dim lookupCmd As New MySqlCommand(q, conn)
                lookupCmd.Parameters.AddWithValue("@PCID", pcID)
                Dim result = lookupCmd.ExecuteScalar()
                If result IsNot Nothing Then saleID = CInt(result)
            End If

            Dim updateQuery As String = "UPDATE sales SET EndTime=@EndTime, TotalAmount=@TotalAmount, PaymentStatus='Paid' WHERE SaleID=@SaleID"
            cmd = New MySqlCommand(updateQuery, conn)
            cmd.Parameters.AddWithValue("@EndTime", endTime)
            cmd.Parameters.AddWithValue("@TotalAmount", totalAmount)
            cmd.Parameters.AddWithValue("@SaleID", saleID)
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show("Error ending session: " & ex.Message)
        Finally
            CloseConnection()
        End Try

        ' CLEANUP
        ActiveSalesIDs.Remove(pcID)
        PCStartTimes.Remove(pcID)
        PCFixedSessions.Remove(pcID)
        PCDurations.Remove(pcID)
        notificationShown.Remove(pcID)
        PCStatus(pcID) = "Available"

        lblTimer.Text = "Usage Time: 00:00 | ₱0.00"
        UpdatePCButton(pcID)
        MessageBox.Show("Session ended. Total Amount: ₱" & totalAmount.ToString("F2"))
    End Sub

    ' ====== TIMER ======
    Private Sub sessionTimer_Tick(sender As Object, e As EventArgs) Handles sessionTimer.Tick
        For Each pcID In PCStartTimes.Keys.ToList()
            Dim elapsed As TimeSpan = DateTime.Now - PCStartTimes(pcID)
            Dim amount As Decimal
            Dim displayText As String = "PC " & pcID

            If PCFixedSessions.ContainsKey(pcID) AndAlso PCFixedSessions(pcID) Then
                Dim remaining As TimeSpan = PCDurations(pcID) - elapsed
                amount = Math.Round(CDec(PCDurations(pcID).TotalHours * ratePerHour), 2)

                If remaining.TotalMinutes <= 5 AndAlso Not notificationShown(pcID) Then
                    MessageBox.Show("⏰ 5 minutes left for " & "PC " & pcID)
                    notificationShown(pcID) = True
                End If

                If remaining.TotalSeconds <= 0 Then
                    SendCommand(pcID, "end_session")
                    AutoEndFixedSession(pcID)
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

    ' ====== AUTO END FOR FIXED ======
    Private Sub AutoEndFixedSession(pcID As Integer)
        Try
            OpenConnection()
            Dim saleID As Integer = ActiveSalesIDs(pcID)
            Dim endTime As DateTime = DateTime.Now
            Dim query As String = "UPDATE sales SET EndTime=@EndTime WHERE SaleID=@SaleID"
            cmd = New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@EndTime", endTime)
            cmd.Parameters.AddWithValue("@SaleID", saleID)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Error updating fixed session end: " & ex.Message)
        Finally
            CloseConnection()
        End Try

        ' Reset PC status
        ActiveSalesIDs.Remove(pcID)
        PCStartTimes.Remove(pcID)
        PCFixedSessions.Remove(pcID)
        PCDurations.Remove(pcID)
        notificationShown.Remove(pcID)
        PCStatus(pcID) = "Available"
        UpdatePCButton(pcID)
        lblTimer.Text = "Usage Time: 00:00 | ₱0.00"
        MessageBox.Show("Fixed session for PC " & pcID & " has ended automatically.")
    End Sub

    ' ====== ADD EXTRA ======
    Private Sub btnAddMinutes_Click(sender As Object, e As EventArgs)
        If SelectedPCID = -1 Then
            MessageBox.Show("Select a PC first.")
            Return
        End If

        Dim extraAmount As Decimal
        If Decimal.TryParse(txtExtraMinutes.Text, extraAmount) AndAlso extraAmount > 0 Then
            If PCFixedSessions.ContainsKey(SelectedPCID) AndAlso PCFixedSessions(SelectedPCID) Then
                PCDurations(SelectedPCID) = PCDurations(SelectedPCID).Add(TimeSpan.FromMinutes((extraAmount / ratePerHour) * 60))
                notificationShown(SelectedPCID) = False
                MessageBox.Show("Added ₱" & extraAmount.ToString("F2") & " to this session.")
            Else
                MessageBox.Show("Can only add extra to Fixed sessions.")
            End If
        Else
            MessageBox.Show("Enter a valid amount.")
        End If
        txtExtraMinutes.Clear()
    End Sub

    ' ====== HELPERS ======
    Private Sub UpdatePCButton(pcID As Integer)
        For Each ctrl As Control In pnlComputers.Controls
            If TypeOf ctrl Is Button AndAlso ctrl.Tag IsNot Nothing AndAlso CInt(ctrl.Tag) = pcID Then
                ctrl.BackColor = If(PCStatus(pcID) = "In Use", Color.LightGreen, Color.LightGray)
            End If
        Next
    End Sub

    Private Sub UpdateTimerLabel(pcID As Integer)
        If PCStartTimes.ContainsKey(pcID) Then
            Dim elapsed As TimeSpan = DateTime.Now - PCStartTimes(pcID)
            If PCFixedSessions.ContainsKey(pcID) AndAlso PCFixedSessions(pcID) Then
                Dim remaining As TimeSpan = PCDurations(pcID) - elapsed
                lblTimer.Text = $"Remaining: {remaining:hh\:mm\:ss} | ₱{Math.Round(CDec(PCDurations(pcID).TotalHours * ratePerHour), 2):F2}"
            Else
                lblTimer.Text = $"Usage Time: {elapsed:hh\:mm\:ss} | ₱{Math.Round(CDec(elapsed.TotalHours * ratePerHour), 2):F2}"
            End If
        Else
            lblTimer.Text = "Usage Time: 00:00 | ₱0.00"
        End If
    End Sub

    Public ReadOnly Property CanLogout As Boolean
        Get
            Return PCStartTimes.Count = 0
        End Get
    End Property

    Private Sub OpenConnection()
        If conn.State <> ConnectionState.Open Then conn.Open()
    End Sub
    Private Sub CloseConnection()
        If conn.State <> ConnectionState.Closed Then conn.Close()
    End Sub

    Private Sub pnlComputers_Paint(sender As Object, e As PaintEventArgs) Handles pnlComputers.Paint
    End Sub
End Class

Imports MySql.Data.MySqlClient
Imports System.Threading

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

    ' ====== ADD AMOUNT / EXTEND SESSION ======
    Private Sub btnAddMinutes_Click(sender As Object, e As EventArgs)
        Try
            If SelectedPCID = -1 Then
                MessageBox.Show("Please select a PC first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If Not PCFixedSessions.ContainsKey(SelectedPCID) OrElse Not PCFixedSessions(SelectedPCID) Then
                MessageBox.Show("Add amount is only allowed for fixed sessions.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Validate input
            Dim additionalAmount As Decimal = 0
            If String.IsNullOrWhiteSpace(txtSales.Text) OrElse Not Decimal.TryParse(txtSales.Text, additionalAmount) Then
                MessageBox.Show("Enter a valid numeric amount to add.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If additionalAmount <= 0 Then
                MessageBox.Show("Amount must be greater than 0.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Calculate extra minutes
            Dim extraMinutes As Double = (additionalAmount / ratePerHour) * 60
            If PCDurations.ContainsKey(SelectedPCID) Then
                PCDurations(SelectedPCID) = PCDurations(SelectedPCID).Add(TimeSpan.FromMinutes(extraMinutes))
            Else
                PCDurations(SelectedPCID) = TimeSpan.FromMinutes(extraMinutes)
            End If

            ' Update sales table: increment TotalAmount
            Try
                OpenConnection()
                Dim updateQuery As String = "UPDATE sales SET TotalAmount = IFNULL(TotalAmount, 0) + @AddAmount " &
                                        "WHERE ComputerID=@ComputerID AND EndTime IS NULL"
                cmd = New MySqlCommand(updateQuery, conn)
                cmd.Parameters.AddWithValue("@AddAmount", additionalAmount)
                cmd.Parameters.AddWithValue("@ComputerID", SelectedPCID)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show("Failed to update session amount in database: " & ex.Message)
            Finally
                CloseConnection()
            End Try

            ' Send command to client to extend session
            SendCommand(SelectedPCID, "extend_session", extraMinutes.ToString())

            ' Reset input
            txtSales.Clear()
            MessageBox.Show("Session extended by ₱" & additionalAmount.ToString("F2") & " (" & extraMinutes.ToString("F0") & " mins).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            UpdateTimerLabel(SelectedPCID)

        Catch ex As Exception
            MessageBox.Show("Error adding amount: " & ex.Message)
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

            ' ====== Store start session in DB ======
            Try
                OpenConnection()
                Dim insertQuery As String = "INSERT INTO sales (ComputerID, UserID, StartTime, RatePerHour, PaymentStatus) " &
                                            "VALUES (@ComputerID, @UserID, @StartTime, @RatePerHour, 'Unpaid')"
                cmd = New MySqlCommand(insertQuery, conn)
                cmd.Parameters.AddWithValue("@ComputerID", SelectedPCID)
                cmd.Parameters.AddWithValue("@UserID", 1002) ' <-- replace with logged-in user
                cmd.Parameters.AddWithValue("@StartTime", DateTime.Now)
                cmd.Parameters.AddWithValue("@RatePerHour", ratePerHour)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show("Failed to insert session into database: " & ex.Message)
            Finally
                CloseConnection()
            End Try

            PCStartTimes(SelectedPCID) = DateTime.Now
            PCStatus(SelectedPCID) = "In Use"
            LoadComputers()
            UpdateTimerLabel(SelectedPCID)

            ' Send command to client
            If sessionType = "Fixed" Then
                Dim minutes As Integer = CInt(PCDurations(SelectedPCID).TotalMinutes)
                SendCommand(SelectedPCID, "fixed_session", minutes.ToString())
            Else
                SendCommand(SelectedPCID, "start_session")
            End If

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

            ' Send command to client to end session
            SendCommand(SelectedPCID, "end_session")
            EndSession(SelectedPCID)
        Catch ex As Exception
            MessageBox.Show("Error ending session: " & ex.Message)
        End Try
    End Sub

    ' ====== END SESSION HELPER ======
    Private Sub EndSession(pcID As Integer, Optional autoEnd As Boolean = False)
        Try
            If Not PCStartTimes.ContainsKey(pcID) Then Return

            Dim elapsed As TimeSpan = DateTime.Now - PCStartTimes(pcID)
            Dim totalAmount As Decimal = Math.Round(CDec(elapsed.TotalHours) * ratePerHour, 2)

            ' ====== Store end session in DB ======
            Try
                OpenConnection()
                Dim updateQuery As String = "UPDATE sales SET EndTime=@EndTime, TotalAmount=@TotalAmount " &
                                            "WHERE ComputerID=@ComputerID AND EndTime IS NULL"
                cmd = New MySqlCommand(updateQuery, conn)
                cmd.Parameters.AddWithValue("@EndTime", DateTime.Now)
                cmd.Parameters.AddWithValue("@TotalAmount", totalAmount)
                cmd.Parameters.AddWithValue("@ComputerID", pcID)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show("Failed to update session in database: " & ex.Message)
            Finally
                CloseConnection()
            End Try

            ' Clean up
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
    Public ReadOnly Property CanLogout As Boolean
        Get
            ' For example, you may want to prevent logout for fixed sessions in progress
            If SelectedPCID <> -1 AndAlso PCFixedSessions.ContainsKey(SelectedPCID) AndAlso PCFixedSessions(SelectedPCID) Then
                Return False
            End If
            Return True
        End Get
    End Property
    Private Sub pnlComputers_Paint(sender As Object, e As PaintEventArgs) Handles pnlComputers.Paint
    End Sub
End Class

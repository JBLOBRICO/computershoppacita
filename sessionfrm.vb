Imports MySql.Data.MySqlClient

Public Class sessionfrm
    ' ====== CONFIG ======
    Private ratePerHour As Decimal = 20D
    Private sessionDuration As TimeSpan
    Private PCStatus As New Dictionary(Of Integer, String)
    Private SelectedPCID As Integer = -1
    Private SelectedPCName As String = ""
    Private currentStartTime As DateTime
    Private timerRunning As Boolean = False
    Private notificationShown As Boolean = False

    ' ====== LOAD FORM ======
    Private Sub sessionfrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadComputers()
        lblTimer.Text = "Usage Time: 00:00"
        txtSales.Enabled = False
        AddHandler cmbSessionType.SelectedIndexChanged, AddressOf cmbSessionType_SelectedIndexChanged
    End Sub

    Private Sub cmbSessionType_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtSales.Enabled = (cmbSessionType.SelectedItem.ToString() = "Fixed")
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
                Dim status As String = dr("Status").ToString()

                Dim btnPC As New Button()
                btnPC.Text = pcName
                btnPC.Name = "btnPC" & pcID
                btnPC.Tag = pcID
                btnPC.Width = 120
                btnPC.Height = 100
                btnPC.Margin = New Padding(10)
                btnPC.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                btnPC.FlatStyle = FlatStyle.Flat

                Select Case status
                    Case "Available"
                        btnPC.BackColor = Color.LightGray
                    Case "In Use"
                        btnPC.BackColor = Color.LightGreen
                    Case "Maintenance"
                        btnPC.BackColor = Color.IndianRed
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
        Dim btn As Button = CType(sender, Button)
        SelectedPCID = btn.Tag
        SelectedPCName = btn.Text
        lblPCName.Text = "PC Name: " & SelectedPCName
        lblStatus.Text = "Status: " & PCStatus(SelectedPCID)
    End Sub

    ' ====== START SESSION ======
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        If SelectedPCID = -1 Then
            MessageBox.Show("Please select a PC first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

        ' Determine session type
        Dim sessionType As String = cmbSessionType.SelectedItem.ToString()
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

            sessionDuration = TimeSpan.FromMinutes((fixedAmount / ratePerHour) * 60) ' convert hours to minutes
        Else
            sessionDuration = TimeSpan.Zero ' open session
        End If

        Try
            OpenConnection()

            ' Insert sale
            Dim insertQuery As String = "
                INSERT INTO sales (ComputerID, UserID, StartTime, RatePerHour, PaymentStatus)
                VALUES (@compID, @userID, NOW(), @rate, 'Unpaid');"
            cmd = New MySqlCommand(insertQuery, conn)
            cmd.Parameters.AddWithValue("@compID", SelectedPCID)
            cmd.Parameters.AddWithValue("@userID", LoggedInUserID)
            cmd.Parameters.AddWithValue("@rate", ratePerHour)
            cmd.ExecuteNonQuery()

            ' Update PC status
            Dim updateQuery As String = "UPDATE computers SET Status='In Use' WHERE ComputerID=@id"
            cmd = New MySqlCommand(updateQuery, conn)
            cmd.Parameters.AddWithValue("@id", SelectedPCID)
            cmd.ExecuteNonQuery()

            ' Start timer
            currentStartTime = DateTime.Now
            timerRunning = True
            sessionTimer.Start()
            notificationShown = False
            lblTimer.Text = "Usage Time: 00:00"

            MessageBox.Show("Session started for " & SelectedPCName)
            LoadComputers()

        Catch ex As Exception
            MessageBox.Show("Error starting session: " & ex.Message)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' ====== END SESSION ======
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        If SelectedPCID = -1 Then
            MessageBox.Show("Please select a PC first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            OpenConnection()
            Dim getSaleQuery As String = "SELECT SaleID, StartTime FROM sales WHERE ComputerID=@id AND PaymentStatus='Unpaid' ORDER BY SaleID DESC LIMIT 1"
            cmd = New MySqlCommand(getSaleQuery, conn)
            cmd.Parameters.AddWithValue("@id", SelectedPCID)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                Dim saleID As Integer = dr("SaleID")
                Dim startTime As DateTime = dr("StartTime")
                dr.Close()

                Dim duration As TimeSpan = DateTime.Now - startTime
                Dim hours As Decimal = CDec(duration.TotalHours)
                Dim totalAmount As Decimal = Math.Round(hours * ratePerHour, 2)

                ' Update sale
                Dim updateSale As String = "UPDATE sales SET EndTime=NOW(), TotalAmount=@total, PaymentStatus='Paid' WHERE SaleID=@saleID"
                cmd = New MySqlCommand(updateSale, conn)
                cmd.Parameters.AddWithValue("@total", totalAmount)
                cmd.Parameters.AddWithValue("@saleID", saleID)
                cmd.ExecuteNonQuery()

                ' Reset PC
                Dim updatePC As String = "UPDATE computers SET Status='Available' WHERE ComputerID=@id"
                cmd = New MySqlCommand(updatePC, conn)
                cmd.Parameters.AddWithValue("@id", SelectedPCID)
                cmd.ExecuteNonQuery()

                timerRunning = False
                sessionTimer.Stop()
                lblTimer.Text = "Usage Time: 00:00"

                MessageBox.Show("Session ended. Total Amount: ₱" & totalAmount.ToString("F2"))
                LoadComputers()
            Else
                dr.Close()
                MessageBox.Show("No active session found for this PC.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error ending session: " & ex.Message)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' ====== SESSION TIMER ======
    Private Sub sessionTimer_Tick(sender As Object, e As EventArgs) Handles sessionTimer.Tick
        If timerRunning Then
            Dim elapsed As TimeSpan = DateTime.Now - currentStartTime
            lblTimer.Text = "Usage Time: " & elapsed.ToString("hh\:mm\:ss")

            ' Show 5-min remaining notification for fixed session
            If sessionDuration.TotalMinutes > 0 AndAlso Not notificationShown Then
                Dim remaining As TimeSpan = sessionDuration - elapsed
                If remaining.TotalMinutes <= 5 AndAlso remaining.TotalMinutes > 0 Then
                    MessageBox.Show("⏰ 5 minutes left on this session!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    notificationShown = True
                End If
            End If

            ' Auto-end fixed session
            If sessionDuration.TotalMinutes > 0 AndAlso elapsed >= sessionDuration Then
                btnEnd.PerformClick()
            End If
        End If
    End Sub
End Class

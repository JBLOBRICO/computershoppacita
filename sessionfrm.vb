Imports MySql.Data.MySqlClient

Public Class sessionfrm
    ' ====== CONFIG ======
    Private ratePerHour As Decimal = 20D
    Private PCStatus As New Dictionary(Of Integer, String)
    Private SelectedPCID As Integer = -1
    Private SelectedPCName As String = ""
    Private notificationShown As New Dictionary(Of Integer, Boolean)
    Private PCStartTimes As New Dictionary(Of Integer, DateTime)
    Private PCFixedSessions As New Dictionary(Of Integer, Boolean) ' Tracks if PC session is Fixed
    Private PCDurations As New Dictionary(Of Integer, TimeSpan) ' Tracks each PC’s fixed duration

    ' ====== FORM LOAD ======
    Private Sub sessionfrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadComputers()
        lblTimer.Text = "Usage Time: 00:00 | ₱0.00"
        txtSales.Enabled = False
        AddHandler cmbSessionType.SelectedIndexChanged, AddressOf cmbSessionType_SelectedIndexChanged
        AddHandler btnAddMinutes.Click, AddressOf btnAddMinutes_Click

        sessionTimer.Interval = 1000
        sessionTimer.Start()
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
                Dim status As String = dr("Status").ToString()

                ' Force status to In Use if session is active
                If PCStartTimes.ContainsKey(pcID) Then status = "In Use"

                ' Button with real-time info
                Dim btnPC As New Button()
                btnPC.Text = pcName
                btnPC.Name = "btnPC" & pcID
                btnPC.Tag = pcID
                btnPC.Width = 150
                btnPC.Height = 100
                btnPC.Margin = New Padding(10)
                btnPC.Font = New Font("Segoe UI", 9, FontStyle.Bold)
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
        UpdateTimerLabel(SelectedPCID)
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

        ' Start session
        PCStartTimes(SelectedPCID) = DateTime.Now
        PCStatus(SelectedPCID) = "In Use"
        LoadComputers()
        UpdateTimerLabel(SelectedPCID)
        MessageBox.Show("Session started for " & SelectedPCName)
    End Sub

    ' ====== END SESSION ======
    Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
        If SelectedPCID = -1 Then
            MessageBox.Show("Please select a PC first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Prevent ending Fixed sessions manually
        If PCFixedSessions.ContainsKey(SelectedPCID) AndAlso PCFixedSessions(SelectedPCID) Then
            MessageBox.Show("Fixed sessions cannot be ended manually.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        EndSession(SelectedPCID)
    End Sub

    ' ====== ADD EXTRA MINUTES ======
    Private Sub btnAddMinutes_Click(sender As Object, e As EventArgs)
        If SelectedPCID = -1 Then Return
        If String.IsNullOrWhiteSpace(txtExtraMinutes.Text) Then Return

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
    End Sub

    ' ====== SESSION TIMER ======
    Private Sub sessionTimer_Tick(sender As Object, e As EventArgs) Handles sessionTimer.Tick
        For Each pcID In PCStartTimes.Keys.ToList()
            Dim elapsed As TimeSpan = DateTime.Now - PCStartTimes(pcID)
            Dim totalAmount As Decimal = Math.Round(CDec(elapsed.TotalHours) * ratePerHour, 2)

            ' Update button text for real-time display
            For Each ctrl As Control In pnlComputers.Controls
                If TypeOf ctrl Is Button AndAlso CInt(ctrl.Tag) = pcID Then
                    ctrl.Text = $"{ctrl.Text.Split(vbLf)(0)}{vbLf}{elapsed:hh\:mm\:ss} | ₱{totalAmount:F2}"
                End If
            Next

            ' Update selected PC label
            If pcID = SelectedPCID Then
                lblTimer.Text = $"Usage Time: {elapsed:hh\:mm\:ss} | ₱{totalAmount:F2}"
            End If

            ' Auto-end Fixed session
            If PCFixedSessions.ContainsKey(pcID) AndAlso PCFixedSessions(pcID) Then
                Dim duration As TimeSpan = PCDurations(pcID)
                Dim remaining As TimeSpan = duration - elapsed

                If remaining.TotalMinutes <= 5 AndAlso Not notificationShown(pcID) Then
                    MessageBox.Show("⏰ 5 minutes left on this session!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    notificationShown(pcID) = True
                End If

                If elapsed >= duration Then
                    EndSession(pcID, autoEnd:=True)
                End If
            End If
        Next
    End Sub

    ' ====== END SESSION HELPER ======
    Private Sub EndSession(pcID As Integer, Optional autoEnd As Boolean = False)
        If PCStartTimes.ContainsKey(pcID) Then
            Dim elapsed As TimeSpan = DateTime.Now - PCStartTimes(pcID)
            Dim totalAmount As Decimal = Math.Round(CDec(elapsed.TotalHours) * ratePerHour, 2)

            PCStartTimes.Remove(pcID)
            If PCFixedSessions.ContainsKey(pcID) Then PCFixedSessions.Remove(pcID)
            If PCDurations.ContainsKey(pcID) Then PCDurations.Remove(pcID)
            If notificationShown.ContainsKey(pcID) Then notificationShown.Remove(pcID)
            PCStatus(pcID) = "Available"

            ' Update selected PC label if it was this PC
            If pcID = SelectedPCID Then lblTimer.Text = "Usage Time: 00:00 | ₱0.00"

            LoadComputers()

            If autoEnd Then
                MessageBox.Show("Fixed session ended automatically.", "Session Ended", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Session ended. Total Amount: ₱" & totalAmount.ToString("F2"))
            End If
        End If
    End Sub

    ' ====== UPDATE TIMER LABEL ======
    Private Sub UpdateTimerLabel(pcID As Integer)
        If PCStartTimes.ContainsKey(pcID) Then
            Dim elapsed As TimeSpan = DateTime.Now - PCStartTimes(pcID)
            Dim totalAmount As Decimal = Math.Round(CDec(elapsed.TotalHours) * ratePerHour, 2)
            lblTimer.Text = $"Usage Time: {elapsed:hh\:mm\:ss} | ₱{totalAmount:F2}"
        Else
            lblTimer.Text = "Usage Time: 00:00 | ₱0.00"
        End If
    End Sub

    ' ====== LOGOUT HANDLING ======
    Public Function CanLogout() As Boolean
        If PCStartTimes.Count > 0 Then
            MessageBox.Show("Cannot logout while PCs have active sessions.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

#Region "DESIGNER CODE"
    ' Include your InitializeComponent here, with AddMinutes textbox/button, sessionTimer, labels, etc.
#End Region

End Class

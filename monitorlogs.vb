Imports MySql.Data.MySqlClient

Public Class MonitorLogs

    Private Sub MonitorLogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadActivePCs()
        LoadStaffSessions()
        LoadSystemLogs()
        LoadPCFilter()

        ' Default date range (today)
        dtpFrom.Value = DateTime.Today
        dtpTo.Value = DateTime.Today
    End Sub

    ' =========================
    ' Load Active PCs
    ' =========================
    Private Sub LoadActivePCs()
        Try
            Dbconnection.OpenConnection()
            Dim query As String = "SELECT ComputerID, ComputerName, Status FROM computers WHERE Status='In Use'"
            Dbconnection.da = New MySqlDataAdapter(query, Dbconnection.conn)
            Dbconnection.dt = New DataTable()
            Dbconnection.da.Fill(Dbconnection.dt)
            dgvActivePCs.DataSource = Dbconnection.dt
        Catch ex As Exception
            MessageBox.Show("Error loading active PCs: " & ex.Message)
        Finally
            Dbconnection.CloseConnection()
        End Try
    End Sub

    ' =========================
    ' Load Staff Sessions (with filters)
    ' =========================
    Private Sub LoadStaffSessions(Optional selectedPC As String = "All PCs", Optional fromDate As Date? = Nothing, Optional toDate As Date? = Nothing)
        Try
            Dbconnection.OpenConnection()

            Dim query As String = "
                SELECT 
                    u.UserID AS 'Staff ID',
                    u.Username AS 'Username',
                    u.FullName AS 'Full Name',
                    c.ComputerName AS 'PC Assigned',
                    s.LoginTime AS 'Login Time',
                    s.LogoutTime AS 'Logout Time',
                    TIMEDIFF(IFNULL(s.LogoutTime, NOW()), s.LoginTime) AS 'Duration',
                    CASE WHEN s.LogoutTime IS NULL THEN 'Active' ELSE 'Offline' END AS 'Status'
                FROM users u
                LEFT JOIN staff_sessions s ON u.UserID = s.UserID
                LEFT JOIN computers c ON s.ComputerID = c.ComputerID
                WHERE u.Role = 'Staff'
            "

            ' Apply filters
            If selectedPC <> "All PCs" Then
                query &= " AND c.ComputerName = @pcName"
            End If
            If fromDate.HasValue AndAlso toDate.HasValue Then
                query &= " AND DATE(s.LoginTime) BETWEEN @fromDate AND @toDate"
            End If

            query &= " ORDER BY s.LoginTime DESC;"

            Dbconnection.cmd = New MySqlCommand(query, Dbconnection.conn)
            If selectedPC <> "All PCs" Then
                Dbconnection.cmd.Parameters.AddWithValue("@pcName", selectedPC)
            End If
            If fromDate.HasValue AndAlso toDate.HasValue Then
                Dbconnection.cmd.Parameters.AddWithValue("@fromDate", fromDate.Value.ToString("yyyy-MM-dd"))
                Dbconnection.cmd.Parameters.AddWithValue("@toDate", toDate.Value.ToString("yyyy-MM-dd"))
            End If

            Dbconnection.da = New MySqlDataAdapter(Dbconnection.cmd)
            Dbconnection.dt = New DataTable()
            Dbconnection.da.Fill(Dbconnection.dt)
            dgvStaffSessions.DataSource = Dbconnection.dt

        Catch ex As Exception
            MessageBox.Show("Error loading staff sessions: " & ex.Message)
        Finally
            Dbconnection.CloseConnection()
        End Try
    End Sub

    ' =========================
    ' Load System Logs (with filters)
    ' =========================
    Private Sub LoadSystemLogs(Optional selectedPC As String = "All PCs", Optional fromDate As Date? = Nothing, Optional toDate As Date? = Nothing)
        Try
            Dbconnection.OpenConnection()

            ' Assume your system_logs table has columns: LogID, FullName, Role, Message, LogDate, ComputerName (optional)
            ' If you don’t have ComputerName, remove that filter part.
            Dim query As String = "
                SELECT 
                    LogID AS 'Log ID',
                    FullName AS 'Staff Name',
                    Role AS 'Role',
                    Message AS 'Activity',
                    LogDate AS 'Timestamp'
                FROM system_logs
                WHERE 1=1
            "

            ' Add PC filter if logs are linked to a computer
            If selectedPC <> "All PCs" Then
                query &= " AND Message LIKE CONCAT('%', @pcName, '%')" ' or use ComputerName column if available
            End If

            ' Add date filter
            If fromDate.HasValue AndAlso toDate.HasValue Then
                query &= " AND DATE(LogDate) BETWEEN @fromDate AND @toDate"
            End If

            query &= " ORDER BY LogDate DESC;"

            Dbconnection.cmd = New MySqlCommand(query, Dbconnection.conn)

            If selectedPC <> "All PCs" Then
                Dbconnection.cmd.Parameters.AddWithValue("@pcName", selectedPC)
            End If
            If fromDate.HasValue AndAlso toDate.HasValue Then
                Dbconnection.cmd.Parameters.AddWithValue("@fromDate", fromDate.Value.ToString("yyyy-MM-dd"))
                Dbconnection.cmd.Parameters.AddWithValue("@toDate", toDate.Value.ToString("yyyy-MM-dd"))
            End If

            Dbconnection.da = New MySqlDataAdapter(Dbconnection.cmd)
            Dbconnection.dt = New DataTable()
            Dbconnection.da.Fill(Dbconnection.dt)
            dgvSystemLogs.DataSource = Dbconnection.dt

            dgvSystemLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dgvSystemLogs.Columns("Timestamp").DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss"
            dgvSystemLogs.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            dgvSystemLogs.DefaultCellStyle.Font = New Font("Segoe UI", 10)

        Catch ex As Exception
            MessageBox.Show("Error loading system logs: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Dbconnection.CloseConnection()
        End Try
    End Sub

    ' =========================
    ' Load PC Filter Dropdown
    ' =========================
    Private Sub LoadPCFilter()
        Try
            Dbconnection.OpenConnection()
            Dim query As String = "SELECT ComputerName FROM computers"
            Dbconnection.cmd = New MySqlCommand(query, Dbconnection.conn)
            Dim reader As MySqlDataReader = Dbconnection.cmd.ExecuteReader()
            cmbFilterPC.Items.Clear()
            cmbFilterPC.Items.Add("All PCs")
            While reader.Read()
                cmbFilterPC.Items.Add(reader("ComputerName").ToString())
            End While
            cmbFilterPC.SelectedIndex = 0
            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading PC filter: " & ex.Message)
        Finally
            Dbconnection.CloseConnection()
        End Try
    End Sub

    ' =========================
    ' Refresh / Filter Button
    ' =========================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim selectedPC As String = cmbFilterPC.SelectedItem.ToString()
        Dim fromDate As Date = dtpFrom.Value.Date
        Dim toDate As Date = dtpTo.Value.Date

        LoadActivePCs()
        LoadStaffSessions(selectedPC, fromDate, toDate)
        LoadSystemLogs(selectedPC, fromDate, toDate)
    End Sub

    ' =========================
    ' UI Event Placeholders
    ' =========================
    Private Sub cmbFilterPC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFilterPC.SelectedIndexChanged
        ' Optional: auto-refresh on PC change
        ' btnRefresh.PerformClick()
    End Sub

    Private Sub pnlData_Paint(sender As Object, e As PaintEventArgs) Handles pnlData.Paint
    End Sub

End Class

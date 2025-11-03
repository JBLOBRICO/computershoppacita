Imports MySql.Data.MySqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class frmanalytics

    Private Sub frmanalytics_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadFinancialKPIs()
        LoadDailyRevenueChart()
        LoadMonthlyRevenueChart()
        LoadPCUsageChart()
        LoadBillingStatusChart()
        LoadReplacementMaintenanceChart()
        LoadPendingBilling()
        LoadReplacementRequests()
    End Sub

#Region "Load KPI Labels"
    Private Sub LoadFinancialKPIs()
        Try
            Using conn As New MySqlConnection("server=localhost;user id=root;password=;database=pacitacmpdb;")
                conn.Open()

                ' Total Revenue
                Dim cmdRevenue As New MySqlCommand("SELECT IFNULL(SUM(TotalAmount),0) FROM sales WHERE PaymentStatus='Paid'", conn)
                lblTotalRevenue.Text = "💰 Total Revenue: ₱" & Convert.ToDecimal(cmdRevenue.ExecuteScalar()).ToString("F2")

                ' Total Sessions
                Dim cmdSessions As New MySqlCommand("SELECT COUNT(*) FROM sales", conn)
                lblTotalSessions.Text = "🖥 Total Sessions: " & cmdSessions.ExecuteScalar().ToString()

                ' Outstanding Billing
                Dim cmdOutstanding As New MySqlCommand("SELECT IFNULL(SUM(Amount),0) FROM billing WHERE Status='Pending'", conn)
                lblOutstandingBilling.Text = "🧾 Outstanding Billing: ₱" & Convert.ToDecimal(cmdOutstanding.ExecuteScalar()).ToString("F2")

                ' Maintenance Requests
                Dim cmdMaintenance As New MySqlCommand("SELECT COUNT(*) FROM computers WHERE Status='Maintenance'", conn)
                lblTotalMaintenance.Text = "🛠 Maintenance Requests: " & cmdMaintenance.ExecuteScalar().ToString()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region

#Region "Charts Loading"

    ' 1️⃣ Daily Revenue (Column)
    Private Sub LoadDailyRevenueChart()
        Try
            Using conn As New MySqlConnection("server=localhost;user id=root;password=;database=pacitacmpdb;")
                conn.Open()
                Dim cmd As New MySqlCommand("
                    SELECT CAST(StartTime AS DATE) AS SaleDate, SUM(TotalAmount) AS Revenue
                    FROM sales
                    GROUP BY CAST(StartTime AS DATE)
                    ORDER BY SaleDate ASC", conn)
                Dim reader = cmd.ExecuteReader()

                ' Ensure series exists
                If ChartDailyRevenue.Series.IndexOf("Revenue") = -1 Then ChartDailyRevenue.Series.Add("Revenue")

                With ChartDailyRevenue
                    .Series("Revenue").ChartType = SeriesChartType.Column
                    .Series("Revenue").Points.Clear()
                    .Series("Revenue").IsValueShownAsLabel = True
                    .ChartAreas(0).AxisX.Title = "Date"
                    .ChartAreas(0).AxisY.Title = "Revenue (₱)"
                End With

                While reader.Read()
                    Dim value As Decimal = Convert.ToDecimal(reader("Revenue"))
                    Dim dateLabel As String = Convert.ToDateTime(reader("SaleDate")).ToString("MMM dd")
                    Dim idx As Integer = ChartDailyRevenue.Series("Revenue").Points.AddXY(dateLabel, value)
                    ChartDailyRevenue.Series("Revenue").Points(idx).Label = value.ToString("F2")
                End While
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ' 2️⃣ Monthly Revenue (last 6 months)
    Private Sub LoadMonthlyRevenueChart()
        Try
            Using conn As New MySqlConnection("server=localhost;user id=root;password=;database=pacitacmpdb;")
                conn.Open()
                Dim cmd As New MySqlCommand("
                    SELECT DATE_FORMAT(StartTime,'%Y-%m') AS Month, SUM(TotalAmount) AS Revenue
                    FROM sales
                    WHERE StartTime >= DATE_SUB(CURDATE(), INTERVAL 6 MONTH)
                    GROUP BY Month
                    ORDER BY Month ASC", conn)
                Dim reader = cmd.ExecuteReader()

                If ChartMonthlyRevenue.Series.IndexOf("Revenue") = -1 Then ChartMonthlyRevenue.Series.Add("Revenue")

                With ChartMonthlyRevenue
                    .Series("Revenue").ChartType = SeriesChartType.Column
                    .Series("Revenue").Points.Clear()
                    .Series("Revenue").IsValueShownAsLabel = True
                    .ChartAreas(0).AxisX.Title = "Month"
                    .ChartAreas(0).AxisY.Title = "Revenue (₱)"
                End With

                While reader.Read()
                    Dim value As Decimal = Convert.ToDecimal(reader("Revenue"))
                    Dim monthLabel As String = Convert.ToDateTime(reader("Month") & "-01").ToString("MMM yyyy")
                    Dim idx As Integer = ChartMonthlyRevenue.Series("Revenue").Points.AddXY(monthLabel, value)
                    ChartMonthlyRevenue.Series("Revenue").Points(idx).Label = value.ToString("F2")
                End While
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ' 3️⃣ PC Usage (Column)
    Private Sub LoadPCUsageChart()
        Try
            Using conn As New MySqlConnection("server=localhost;user id=root;password=;database=pacitacmpdb;")
                conn.Open()
                Dim cmd As New MySqlCommand("
                    SELECT c.ComputerName, COUNT(s.SaleID) AS Sessions
                    FROM computers c
                    LEFT JOIN sales s ON c.ComputerID = s.ComputerID
                    GROUP BY c.ComputerID", conn)
                Dim reader = cmd.ExecuteReader()

                If ChartPCUsage.Series.IndexOf("PCUsage") = -1 Then ChartPCUsage.Series.Add("PCUsage")

                With ChartPCUsage
                    .Series("PCUsage").ChartType = SeriesChartType.Column
                    .Series("PCUsage").Points.Clear()
                    .Series("PCUsage").IsValueShownAsLabel = True
                    .ChartAreas(0).AxisX.Title = "Computer"
                    .ChartAreas(0).AxisY.Title = "Sessions"
                End With

                While reader.Read()
                    Dim value As Integer = Convert.ToInt32(reader("Sessions"))
                    Dim computerName As String = reader("ComputerName").ToString()
                    Dim idx As Integer = ChartPCUsage.Series("PCUsage").Points.AddXY(computerName, value)
                    ChartPCUsage.Series("PCUsage").Points(idx).Label = value.ToString()
                End While
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ' 4️⃣ Billing Status (Pie)
    Private Sub LoadBillingStatusChart()
        Try
            Using conn As New MySqlConnection("server=localhost;user id=root;password=;database=pacitacmpdb;")
                conn.Open()
                Dim cmd As New MySqlCommand("
                    SELECT Status, COUNT(*) AS CountStatus
                    FROM billing
                    GROUP BY Status", conn)
                Dim reader = cmd.ExecuteReader()

                If ChartBillingStatus.Series.IndexOf("Billing") = -1 Then ChartBillingStatus.Series.Add("Billing")

                With ChartBillingStatus
                    .Series("Billing").ChartType = SeriesChartType.Pie
                    .Series("Billing").Points.Clear()
                    .Series("Billing").IsValueShownAsLabel = True
                End With

                While reader.Read()
                    Dim value As Integer = Convert.ToInt32(reader("CountStatus"))
                    Dim status As String = reader("Status").ToString()
                    Dim idx As Integer = ChartBillingStatus.Series("Billing").Points.AddXY(status, value)
                    ChartBillingStatus.Series("Billing").Points(idx).Label = status & " (" & value.ToString() & ")"
                End While
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    ' 5️⃣ Replacement / Maintenance Requests (Pie)
    Private Sub LoadReplacementMaintenanceChart()
        Try
            Using conn As New MySqlConnection("server=localhost;user id=root;password=;database=pacitacmpdb;")
                conn.Open()
                Dim cmd As New MySqlCommand("
                    SELECT Status, COUNT(*) AS CountStatus
                    FROM replacements
                    GROUP BY Status", conn)
                Dim reader = cmd.ExecuteReader()

                If ChartReplacement.Series.IndexOf("Replacement") = -1 Then ChartReplacement.Series.Add("Replacement")

                With ChartReplacement
                    .Series("Replacement").ChartType = SeriesChartType.Pie
                    .Series("Replacement").Points.Clear()
                    .Series("Replacement").IsValueShownAsLabel = True
                End With

                While reader.Read()
                    Dim value As Integer = Convert.ToInt32(reader("CountStatus"))
                    Dim status As String = reader("Status").ToString()
                    Dim idx As Integer = ChartReplacement.Series("Replacement").Points.AddXY(status, value)
                    ChartReplacement.Series("Replacement").Points(idx).Label = status & " (" & value.ToString() & ")"
                End While
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

#Region "Pending Tables"

    Private Sub LoadPendingBilling()
        Try
            Using conn As New MySqlConnection("server=localhost;user id=root;password=;database=pacitacmpdb;")
                conn.Open()
                Dim da As New MySqlDataAdapter("
                    SELECT b.BillID, s.SaleID, b.Amount, b.Status 
                    FROM billing b 
                    LEFT JOIN sales s ON b.SaleID = s.SaleID 
                    WHERE b.Status='Pending'", conn)
                Dim dt As New DataTable()
                da.Fill(dt)
                dgvPendingBilling.DataSource = dt
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub LoadReplacementRequests()
        Try
            Using conn As New MySqlConnection("server=localhost;user id=root;password=;database=pacitacmpdb;")
                conn.Open()
                Dim da As New MySqlDataAdapter("
                    SELECT ReplacementID, ComputerID, ItemName, Status, Cost 
                    FROM replacements 
                    WHERE Status='Pending'", conn)
                Dim dt As New DataTable()
                da.Fill(dt)
                dgvReplacementRequests.DataSource = dt
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

End Class

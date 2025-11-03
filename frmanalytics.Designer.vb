<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmanalytics
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.PanelMain = New System.Windows.Forms.Panel()
        Me.PanelTop = New System.Windows.Forms.Panel()

        ' KPI Cards
        Me.CardTotalRevenue = New System.Windows.Forms.Panel()
        Me.lblTotalRevenue = New System.Windows.Forms.Label()
        Me.CardTotalSessions = New System.Windows.Forms.Panel()
        Me.lblTotalSessions = New System.Windows.Forms.Label()
        Me.CardOutstandingBilling = New System.Windows.Forms.Panel()
        Me.lblOutstandingBilling = New System.Windows.Forms.Label()
        Me.CardTotalMaintenance = New System.Windows.Forms.Panel()
        Me.lblTotalMaintenance = New System.Windows.Forms.Label()

        ' Charts
        Me.ChartDailyRevenue = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.ChartMonthlyRevenue = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.ChartPCUsage = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.ChartBillingStatus = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.ChartReplacement = New System.Windows.Forms.DataVisualization.Charting.Chart()

        ' Chart Labels
        Me.lblDailyRevenue = New System.Windows.Forms.Label()
        Me.lblMonthlyRevenue = New System.Windows.Forms.Label()
        Me.lblPCUsage = New System.Windows.Forms.Label()
        Me.lblBillingStatus = New System.Windows.Forms.Label()
        Me.lblReplacement = New System.Windows.Forms.Label()

        ' DataGrids
        Me.dgvPendingBilling = New System.Windows.Forms.DataGridView()
        Me.dgvReplacementRequests = New System.Windows.Forms.DataGridView()

        '=============================
        ' PanelMain
        '=============================
        Me.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMain.AutoScroll = True
        Me.PanelMain.BackColor = System.Drawing.Color.WhiteSmoke

        '=============================
        ' PanelTop - KPI Cards
        '=============================
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Height = 150
        Me.PanelTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PanelTop.Padding = New System.Windows.Forms.Padding(10)

        ' KPI Cards size
        Dim kpiWidth As Integer = 300, kpiHeight As Integer = 120
        Dim kpiGap As Integer = 20

        ' Card: Total Revenue
        Me.CardTotalRevenue.BackColor = System.Drawing.Color.FromArgb(52, 152, 219)
        Me.CardTotalRevenue.Size = New System.Drawing.Size(kpiWidth, kpiHeight)
        Me.CardTotalRevenue.Location = New System.Drawing.Point(10, 10)
        Me.lblTotalRevenue.ForeColor = System.Drawing.Color.White
        Me.lblTotalRevenue.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalRevenue.AutoSize = True
        Me.lblTotalRevenue.Location = New System.Drawing.Point(10, 50)
        Me.lblTotalRevenue.Text = "💰 Total Revenue: ₱0.00"
        Me.CardTotalRevenue.Controls.Add(Me.lblTotalRevenue)

        ' Card: Total Sessions
        Me.CardTotalSessions.BackColor = System.Drawing.Color.FromArgb(46, 204, 113)
        Me.CardTotalSessions.Size = New System.Drawing.Size(kpiWidth, kpiHeight)
        Me.CardTotalSessions.Location = New System.Drawing.Point(10 + kpiWidth + kpiGap, 10)
        Me.lblTotalSessions.ForeColor = System.Drawing.Color.White
        Me.lblTotalSessions.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalSessions.AutoSize = True
        Me.lblTotalSessions.Location = New System.Drawing.Point(10, 50)
        Me.lblTotalSessions.Text = "🖥 Total Sessions: 0"
        Me.CardTotalSessions.Controls.Add(Me.lblTotalSessions)

        ' Card: Outstanding Billing
        Me.CardOutstandingBilling.BackColor = System.Drawing.Color.FromArgb(231, 76, 60)
        Me.CardOutstandingBilling.Size = New System.Drawing.Size(kpiWidth, kpiHeight)
        Me.CardOutstandingBilling.Location = New System.Drawing.Point(10 + 2 * (kpiWidth + kpiGap), 10)
        Me.lblOutstandingBilling.ForeColor = System.Drawing.Color.White
        Me.lblOutstandingBilling.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblOutstandingBilling.AutoSize = True
        Me.lblOutstandingBilling.Location = New System.Drawing.Point(10, 50)
        Me.lblOutstandingBilling.Text = "🧾 Outstanding Billing: ₱0.00"
        Me.CardOutstandingBilling.Controls.Add(Me.lblOutstandingBilling)

        ' Card: Total Maintenance
        Me.CardTotalMaintenance.BackColor = System.Drawing.Color.FromArgb(241, 196, 15)
        Me.CardTotalMaintenance.Size = New System.Drawing.Size(kpiWidth, kpiHeight)
        Me.CardTotalMaintenance.Location = New System.Drawing.Point(10 + 3 * (kpiWidth + kpiGap), 10)
        Me.lblTotalMaintenance.ForeColor = System.Drawing.Color.White
        Me.lblTotalMaintenance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalMaintenance.AutoSize = True
        Me.lblTotalMaintenance.Location = New System.Drawing.Point(10, 50)
        Me.lblTotalMaintenance.Text = "🛠 Maintenance Requests: 0"
        Me.CardTotalMaintenance.Controls.Add(Me.lblTotalMaintenance)

        ' Add cards to PanelTop
        Me.PanelTop.Controls.Add(Me.CardTotalRevenue)
        Me.PanelTop.Controls.Add(Me.CardTotalSessions)
        Me.PanelTop.Controls.Add(Me.CardOutstandingBilling)
        Me.PanelTop.Controls.Add(Me.CardTotalMaintenance)

        '=============================
        ' Charts vertical layout
        '=============================
        Dim currentY As Integer = 160
        Dim chartWidth As Integer = 1100, chartHeight As Integer = 350
        Dim leftMargin As Integer = 20, labelHeight As Integer = 20, gap As Integer = 20

        ' Daily Revenue
        Me.lblDailyRevenue.Text = "📊 Daily Revenue (Revenue per day for the last month)"
        Me.lblDailyRevenue.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblDailyRevenue.Location = New System.Drawing.Point(leftMargin, currentY)
        Me.lblDailyRevenue.Size = New System.Drawing.Size(chartWidth, labelHeight)
        Me.ChartDailyRevenue.Location = New System.Drawing.Point(leftMargin, currentY + labelHeight)
        Me.ChartDailyRevenue.Size = New System.Drawing.Size(chartWidth, chartHeight)
        Me.ChartDailyRevenue.ChartAreas.Add("ChartArea1")
        Me.ChartDailyRevenue.Series.Add("Revenue")
        currentY += labelHeight + chartHeight + gap

        ' Monthly Revenue
        Me.lblMonthlyRevenue.Text = "📊 Monthly Revenue (Last 6 Months)"
        Me.lblMonthlyRevenue.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblMonthlyRevenue.Location = New System.Drawing.Point(leftMargin, currentY)
        Me.lblMonthlyRevenue.Size = New System.Drawing.Size(chartWidth, labelHeight)
        Me.ChartMonthlyRevenue.Location = New System.Drawing.Point(leftMargin, currentY + labelHeight)
        Me.ChartMonthlyRevenue.Size = New System.Drawing.Size(chartWidth, chartHeight)
        Me.ChartMonthlyRevenue.ChartAreas.Add("ChartArea1")
        Me.ChartMonthlyRevenue.Series.Add("Revenue")
        currentY += labelHeight + chartHeight + gap

        ' PC Usage
        Me.lblPCUsage.Text = "💻 PC Usage Sessions (Number of sessions per computer)"
        Me.lblPCUsage.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblPCUsage.Location = New System.Drawing.Point(leftMargin, currentY)
        Me.lblPCUsage.Size = New System.Drawing.Size(chartWidth, labelHeight)
        Me.ChartPCUsage.Location = New System.Drawing.Point(leftMargin, currentY + labelHeight)
        Me.ChartPCUsage.Size = New System.Drawing.Size(chartWidth, chartHeight)
        Me.ChartPCUsage.ChartAreas.Add("ChartArea1")
        Me.ChartPCUsage.Series.Add("PCUsage")
        currentY += labelHeight + chartHeight + gap

        ' Billing Status
        Me.lblBillingStatus.Text = "🧾 Billing Status (Paid vs Pending)"
        Me.lblBillingStatus.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblBillingStatus.Location = New System.Drawing.Point(leftMargin, currentY)
        Me.lblBillingStatus.Size = New System.Drawing.Size(chartWidth, labelHeight)
        Me.ChartBillingStatus.Location = New System.Drawing.Point(leftMargin, currentY + labelHeight)
        Me.ChartBillingStatus.Size = New System.Drawing.Size(chartWidth, chartHeight)
        Me.ChartBillingStatus.ChartAreas.Add("ChartArea1")
        Me.ChartBillingStatus.Series.Add("Billing")
        currentY += labelHeight + chartHeight + gap

        ' Replacement / Maintenance
        Me.lblReplacement.Text = "🔧 Replacement / Maintenance Requests"
        Me.lblReplacement.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblReplacement.Location = New System.Drawing.Point(leftMargin, currentY)
        Me.lblReplacement.Size = New System.Drawing.Size(chartWidth, labelHeight)
        Me.ChartReplacement.Location = New System.Drawing.Point(leftMargin, currentY + labelHeight)
        Me.ChartReplacement.Size = New System.Drawing.Size(chartWidth, chartHeight)
        Me.ChartReplacement.ChartAreas.Add("ChartArea1")
        Me.ChartReplacement.Series.Add("Replacement")
        currentY += labelHeight + chartHeight + gap

        ' Pending Billing Table
        Me.dgvPendingBilling.Location = New System.Drawing.Point(leftMargin, currentY)
        Me.dgvPendingBilling.Size = New System.Drawing.Size(chartWidth, 200)
        Me.dgvPendingBilling.ReadOnly = True
        Me.dgvPendingBilling.AllowUserToAddRows = False
        Me.dgvPendingBilling.AllowUserToDeleteRows = False
        Me.dgvPendingBilling.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPendingBilling.BorderStyle = BorderStyle.Fixed3D
        currentY += 200 + gap

        ' Replacement Requests Table
        Me.dgvReplacementRequests.Location = New System.Drawing.Point(leftMargin, currentY)
        Me.dgvReplacementRequests.Size = New System.Drawing.Size(chartWidth, 200)
        Me.dgvReplacementRequests.ReadOnly = True
        Me.dgvReplacementRequests.AllowUserToAddRows = False
        Me.dgvReplacementRequests.AllowUserToDeleteRows = False
        Me.dgvReplacementRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        '=============================
        ' Add controls to PanelMain
        '=============================
        Me.PanelMain.Controls.Add(Me.PanelTop)
        Me.PanelMain.Controls.Add(Me.lblDailyRevenue)
        Me.PanelMain.Controls.Add(Me.ChartDailyRevenue)
        Me.PanelMain.Controls.Add(Me.lblMonthlyRevenue)
        Me.PanelMain.Controls.Add(Me.ChartMonthlyRevenue)
        Me.PanelMain.Controls.Add(Me.lblPCUsage)
        Me.PanelMain.Controls.Add(Me.ChartPCUsage)
        Me.PanelMain.Controls.Add(Me.lblBillingStatus)
        Me.PanelMain.Controls.Add(Me.ChartBillingStatus)
        Me.PanelMain.Controls.Add(Me.lblReplacement)
        Me.PanelMain.Controls.Add(Me.ChartReplacement)
        Me.PanelMain.Controls.Add(Me.dgvPendingBilling)
        Me.PanelMain.Controls.Add(Me.dgvReplacementRequests)

        '=============================
        ' Form
        '=============================
        Me.ClientSize = New System.Drawing.Size(1200, 700)
        Me.Controls.Add(Me.PanelMain)
        Me.Text = "Owner Dashboard Analytics"
        Me.BackColor = System.Drawing.Color.WhiteSmoke
    End Sub

    ' Form Controls
    Friend WithEvents PanelMain As Panel
    Friend WithEvents PanelTop As Panel
    Friend WithEvents CardTotalRevenue As Panel
    Friend WithEvents CardTotalSessions As Panel
    Friend WithEvents CardOutstandingBilling As Panel
    Friend WithEvents CardTotalMaintenance As Panel
    Friend WithEvents lblTotalRevenue As Label
    Friend WithEvents lblTotalSessions As Label
    Friend WithEvents lblOutstandingBilling As Label
    Friend WithEvents lblTotalMaintenance As Label
    Friend WithEvents ChartDailyRevenue As DataVisualization.Charting.Chart
    Friend WithEvents ChartMonthlyRevenue As DataVisualization.Charting.Chart
    Friend WithEvents ChartPCUsage As DataVisualization.Charting.Chart
    Friend WithEvents ChartBillingStatus As DataVisualization.Charting.Chart
    Friend WithEvents ChartReplacement As DataVisualization.Charting.Chart
    Friend WithEvents lblDailyRevenue As Label
    Friend WithEvents lblMonthlyRevenue As Label
    Friend WithEvents lblPCUsage As Label
    Friend WithEvents lblBillingStatus As Label
    Friend WithEvents lblReplacement As Label
    Friend WithEvents dgvPendingBilling As DataGridView
    Friend WithEvents dgvReplacementRequests As DataGridView
End Class

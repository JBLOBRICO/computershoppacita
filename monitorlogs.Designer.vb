<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MonitorLogs
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents pnlTop As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents pnlMain As Panel

    ' Monitoring controls
    Friend WithEvents dgvActivePCs As DataGridView
    Friend WithEvents dgvStaffSessions As DataGridView
    Friend WithEvents dgvSystemLogs As DataGridView
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnExport As Button
    Friend WithEvents lblFilterPC As Label
    Friend WithEvents cmbFilterPC As ComboBox
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents lblFrom As Label
    Friend WithEvents lblTo As Label
    Friend WithEvents pnlFilters As Panel
    Friend WithEvents pnlData As Panel

    ' Labels for each grid
    Friend WithEvents lblActivePCs As Label
    Friend WithEvents lblStaffSessions As Label
    Friend WithEvents lblSystemLogs As Label

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlData = New System.Windows.Forms.Panel()
        Me.dgvSystemLogs = New System.Windows.Forms.DataGridView()
        Me.lblSystemLogs = New System.Windows.Forms.Label()
        Me.dgvStaffSessions = New System.Windows.Forms.DataGridView()
        Me.lblStaffSessions = New System.Windows.Forms.Label()
        Me.dgvActivePCs = New System.Windows.Forms.DataGridView()
        Me.lblActivePCs = New System.Windows.Forms.Label()
        Me.pnlFilters = New System.Windows.Forms.Panel()
        Me.cmbFilterPC = New System.Windows.Forms.ComboBox()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.lblFilterPC = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.pnlTop.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlData.SuspendLayout()
        CType(Me.dgvSystemLogs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvStaffSessions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvActivePCs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFilters.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(56, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.pnlTop.Controls.Add(Me.lblTitle)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1280, 70)
        Me.pnlTop.TabIndex = 1
        '
        'lblTitle
        '
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.lblTitle.Size = New System.Drawing.Size(1280, 70)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Monitoring & Logs"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlMain.Controls.Add(Me.pnlData)
        Me.pnlMain.Controls.Add(Me.pnlFilters)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 70)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlMain.Size = New System.Drawing.Size(1280, 650)
        Me.pnlMain.TabIndex = 0
        '
        'pnlData
        '
        Me.pnlData.Controls.Add(Me.dgvSystemLogs)
        Me.pnlData.Controls.Add(Me.lblSystemLogs)
        Me.pnlData.Controls.Add(Me.dgvStaffSessions)
        Me.pnlData.Controls.Add(Me.lblStaffSessions)
        Me.pnlData.Controls.Add(Me.dgvActivePCs)
        Me.pnlData.Controls.Add(Me.lblActivePCs)
        Me.pnlData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlData.Location = New System.Drawing.Point(20, 80)
        Me.pnlData.Name = "pnlData"
        Me.pnlData.Padding = New System.Windows.Forms.Padding(0, 20, 0, 0)
        Me.pnlData.Size = New System.Drawing.Size(1240, 550)
        Me.pnlData.TabIndex = 0
        '
        'dgvSystemLogs
        '
        Me.dgvSystemLogs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvSystemLogs.BackgroundColor = System.Drawing.Color.White
        Me.dgvSystemLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSystemLogs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSystemLogs.Location = New System.Drawing.Point(0, 380)
        Me.dgvSystemLogs.Name = "dgvSystemLogs"
        Me.dgvSystemLogs.ReadOnly = True
        Me.dgvSystemLogs.RowHeadersWidth = 51
        Me.dgvSystemLogs.Size = New System.Drawing.Size(1240, 170)
        Me.dgvSystemLogs.TabIndex = 2
        '
        'lblSystemLogs
        '
        Me.lblSystemLogs.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblSystemLogs.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!)
        Me.lblSystemLogs.Location = New System.Drawing.Point(0, 360)
        Me.lblSystemLogs.Name = "lblSystemLogs"
        Me.lblSystemLogs.Size = New System.Drawing.Size(1240, 20)
        Me.lblSystemLogs.TabIndex = 3
        Me.lblSystemLogs.Text = "System Logs"
        '
        'dgvStaffSessions
        '
        Me.dgvStaffSessions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvStaffSessions.BackgroundColor = System.Drawing.Color.White
        Me.dgvStaffSessions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvStaffSessions.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgvStaffSessions.Location = New System.Drawing.Point(0, 210)
        Me.dgvStaffSessions.Name = "dgvStaffSessions"
        Me.dgvStaffSessions.ReadOnly = True
        Me.dgvStaffSessions.RowHeadersWidth = 51
        Me.dgvStaffSessions.Size = New System.Drawing.Size(1240, 150)
        Me.dgvStaffSessions.TabIndex = 1
        '
        'lblStaffSessions
        '
        Me.lblStaffSessions.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblStaffSessions.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!)
        Me.lblStaffSessions.Location = New System.Drawing.Point(0, 190)
        Me.lblStaffSessions.Name = "lblStaffSessions"
        Me.lblStaffSessions.Size = New System.Drawing.Size(1240, 20)
        Me.lblStaffSessions.TabIndex = 4
        Me.lblStaffSessions.Text = "Staff Sessions"
        '
        'dgvActivePCs
        '
        Me.dgvActivePCs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvActivePCs.BackgroundColor = System.Drawing.Color.White
        Me.dgvActivePCs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvActivePCs.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgvActivePCs.Location = New System.Drawing.Point(0, 40)
        Me.dgvActivePCs.Name = "dgvActivePCs"
        Me.dgvActivePCs.ReadOnly = True
        Me.dgvActivePCs.RowHeadersWidth = 51
        Me.dgvActivePCs.Size = New System.Drawing.Size(1240, 150)
        Me.dgvActivePCs.TabIndex = 0
        '
        'lblActivePCs
        '
        Me.lblActivePCs.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblActivePCs.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!)
        Me.lblActivePCs.Location = New System.Drawing.Point(0, 20)
        Me.lblActivePCs.Name = "lblActivePCs"
        Me.lblActivePCs.Size = New System.Drawing.Size(1240, 20)
        Me.lblActivePCs.TabIndex = 5
        Me.lblActivePCs.Text = "Active PCs"
        '
        'pnlFilters
        '
        Me.pnlFilters.BackColor = System.Drawing.Color.White
        Me.pnlFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilters.Controls.Add(Me.cmbFilterPC)
        Me.pnlFilters.Controls.Add(Me.dtpFrom)
        Me.pnlFilters.Controls.Add(Me.dtpTo)
        Me.pnlFilters.Controls.Add(Me.lblFilterPC)
        Me.pnlFilters.Controls.Add(Me.lblFrom)
        Me.pnlFilters.Controls.Add(Me.lblTo)
        Me.pnlFilters.Controls.Add(Me.btnRefresh)
        Me.pnlFilters.Controls.Add(Me.btnExport)
        Me.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilters.Location = New System.Drawing.Point(20, 20)
        Me.pnlFilters.Name = "pnlFilters"
        Me.pnlFilters.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlFilters.Size = New System.Drawing.Size(1240, 60)
        Me.pnlFilters.TabIndex = 1
        '
        'cmbFilterPC
        '
        Me.cmbFilterPC.Location = New System.Drawing.Point(151, 15)
        Me.cmbFilterPC.Name = "cmbFilterPC"
        Me.cmbFilterPC.Size = New System.Drawing.Size(150, 21)
        Me.cmbFilterPC.TabIndex = 1
        '
        'dtpFrom
        '
        Me.dtpFrom.Location = New System.Drawing.Point(393, 16)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(120, 20)
        Me.dtpFrom.TabIndex = 3
        '
        'dtpTo
        '
        Me.dtpTo.Location = New System.Drawing.Point(564, 16)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(120, 20)
        Me.dtpTo.TabIndex = 5
        '
        'lblFilterPC
        '
        Me.lblFilterPC.Location = New System.Drawing.Point(81, 18)
        Me.lblFilterPC.Name = "lblFilterPC"
        Me.lblFilterPC.Size = New System.Drawing.Size(100, 23)
        Me.lblFilterPC.TabIndex = 0
        Me.lblFilterPC.Text = "Filter PC:"
        '
        'lblFrom
        '
        Me.lblFrom.Location = New System.Drawing.Point(343, 19)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(100, 23)
        Me.lblFrom.TabIndex = 2
        Me.lblFrom.Text = "From:"
        '
        'lblTo
        '
        Me.lblTo.Location = New System.Drawing.Point(536, 18)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(100, 23)
        Me.lblTo.TabIndex = 4
        Me.lblTo.Text = "To:"
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(711, 8)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(80, 35)
        Me.btnRefresh.TabIndex = 6
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnExport
        '
        Me.btnExport.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExport.ForeColor = System.Drawing.Color.White
        Me.btnExport.Location = New System.Drawing.Point(797, 8)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(80, 35)
        Me.btnExport.TabIndex = 7
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = False
        '
        'monitorlogs
        '
        Me.ClientSize = New System.Drawing.Size(1280, 720)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "monitorlogs"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Monitoring & Logs - Pacita Computer Shop"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTop.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlData.ResumeLayout(False)
        CType(Me.dgvSystemLogs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvStaffSessions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvActivePCs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFilters.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
End Class

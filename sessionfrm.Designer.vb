<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class sessionfrm
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlComputers = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlInfo = New System.Windows.Forms.Panel()
        Me.lblPCName = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblTimer = New System.Windows.Forms.Label()
        Me.lblSales = New System.Windows.Forms.Label()
        Me.txtSales = New System.Windows.Forms.TextBox()
        Me.lblSessionType = New System.Windows.Forms.Label()
        Me.cmbSessionType = New System.Windows.Forms.ComboBox()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.sessionTimer = New System.Windows.Forms.Timer(Me.components)
        Me.pnlHeader.SuspendLayout()
        Me.pnlInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(58, 90, 164)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Size = New System.Drawing.Size(956, 39)
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(15, 5)
        Me.lblTitle.Text = "💻 Manage Sessions"
        '
        'pnlComputers
        '
        Me.pnlComputers.AutoScroll = True
        Me.pnlComputers.BackColor = System.Drawing.Color.FromArgb(240, 242, 245)
        Me.pnlComputers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlComputers.Padding = New System.Windows.Forms.Padding(15, 13, 15, 13)
        '
        'pnlInfo
        '
        Me.pnlInfo.BackColor = System.Drawing.Color.White
        Me.pnlInfo.Controls.Add(Me.lblPCName)
        Me.pnlInfo.Controls.Add(Me.lblStatus)
        Me.pnlInfo.Controls.Add(Me.lblTimer)
        Me.pnlInfo.Controls.Add(Me.lblSessionType)
        Me.pnlInfo.Controls.Add(Me.cmbSessionType)
        Me.pnlInfo.Controls.Add(Me.lblSales)
        Me.pnlInfo.Controls.Add(Me.txtSales)
        Me.pnlInfo.Controls.Add(Me.btnStart)
        Me.pnlInfo.Controls.Add(Me.btnEnd)
        Me.pnlInfo.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlInfo.Padding = New System.Windows.Forms.Padding(15, 13, 15, 13)
        Me.pnlInfo.Size = New System.Drawing.Size(225, 359)
        '
        'lblPCName
        '
        Me.lblPCName.AutoSize = True
        Me.lblPCName.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblPCName.Text = "PC Name: -"
        Me.lblPCName.Location = New System.Drawing.Point(8, 10)
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblStatus.Text = "Status: -"
        Me.lblStatus.Location = New System.Drawing.Point(8, 40)
        '
        'lblTimer
        '
        Me.lblTimer.AutoSize = True
        Me.lblTimer.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblTimer.Text = "Usage Time: 00:00"
        Me.lblTimer.Location = New System.Drawing.Point(8, 70)
        '
        'lblSessionType
        '
        Me.lblSessionType.AutoSize = True
        Me.lblSessionType.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblSessionType.Text = "Session Type:"
        Me.lblSessionType.Location = New System.Drawing.Point(8, 100)
        '
        'cmbSessionType
        '
        Me.cmbSessionType.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.cmbSessionType.Items.AddRange(New Object() {"Open", "Fixed"})
        Me.cmbSessionType.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cmbSessionType.Location = New System.Drawing.Point(8, 125)
        Me.cmbSessionType.Size = New System.Drawing.Size(188, 29)
        Me.cmbSessionType.SelectedIndex = 0
        '
        'lblSales
        '
        Me.lblSales.AutoSize = True
        Me.lblSales.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblSales.Text = "Amount (₱):"
        Me.lblSales.Location = New System.Drawing.Point(8, 160)
        '
        'txtSales
        '
        Me.txtSales.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtSales.Location = New System.Drawing.Point(8, 185)
        Me.txtSales.Size = New System.Drawing.Size(188, 29)
        '
        'btnStart
        '
        Me.btnStart.BackColor = System.Drawing.Color.FromArgb(58, 90, 164)
        Me.btnStart.FlatStyle = FlatStyle.Flat
        Me.btnStart.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnStart.ForeColor = Color.White
        Me.btnStart.Location = New System.Drawing.Point(8, 225)
        Me.btnStart.Size = New System.Drawing.Size(188, 26)
        Me.btnStart.Text = "▶ Start Session"
        '
        'btnEnd
        '
        Me.btnEnd.BackColor = Color.FromArgb(200, 60, 70)
        Me.btnEnd.FlatStyle = FlatStyle.Flat
        Me.btnEnd.Font = New System.Drawing.Font("Segoe UI", 11.0!, FontStyle.Bold)
        Me.btnEnd.ForeColor = Color.White
        Me.btnEnd.Location = New System.Drawing.Point(8, 260)
        Me.btnEnd.Size = New System.Drawing.Size(188, 26)
        Me.btnEnd.Text = "⏹ End Session"
        '
        'sessionTimer
        '
        Me.sessionTimer.Interval = 1000
        '
        'sessionfrm
        '
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(956, 398)
        Me.Controls.Add(Me.pnlComputers)
        Me.Controls.Add(Me.pnlInfo)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Manage Sessions"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlInfo.ResumeLayout(False)
        Me.pnlInfo.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents pnlComputers As FlowLayoutPanel
    Friend WithEvents pnlInfo As Panel
    Friend WithEvents lblPCName As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents lblTimer As Label
    Friend WithEvents lblSales As Label
    Friend WithEvents txtSales As TextBox
    Friend WithEvents btnStart As Button
    Friend WithEvents btnEnd As Button
    Friend WithEvents sessionTimer As Timer
    Friend WithEvents lblSessionType As Label
    Friend WithEvents cmbSessionType As ComboBox
End Class

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
        Me.lblSessionType = New System.Windows.Forms.Label()
        Me.cmbSessionType = New System.Windows.Forms.ComboBox()
        Me.lblSales = New System.Windows.Forms.Label()
        Me.txtSales = New System.Windows.Forms.TextBox()
        Me.lblExtraMinutes = New System.Windows.Forms.Label()
        Me.txtExtraMinutes = New System.Windows.Forms.TextBox()
        Me.btnAddMinutes = New System.Windows.Forms.Button()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.btnSendMsg = New System.Windows.Forms.Button()
        Me.btnShutdown = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.sessionTimer = New System.Windows.Forms.Timer(Me.components)
        Me.pnlHeader.SuspendLayout()
        Me.pnlInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(164, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(956, 39)
        Me.pnlHeader.TabIndex = 2
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(15, 5)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(226, 30)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "💻 Manage Sessions"
        '
        'pnlComputers
        '
        Me.pnlComputers.AutoScroll = True
        Me.pnlComputers.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.pnlComputers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlComputers.Location = New System.Drawing.Point(0, 39)
        Me.pnlComputers.Name = "pnlComputers"
        Me.pnlComputers.Padding = New System.Windows.Forms.Padding(15, 13, 15, 13)
        Me.pnlComputers.Size = New System.Drawing.Size(731, 481)
        Me.pnlComputers.TabIndex = 0
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
        Me.pnlInfo.Controls.Add(Me.lblExtraMinutes)
        Me.pnlInfo.Controls.Add(Me.txtExtraMinutes)
        Me.pnlInfo.Controls.Add(Me.btnAddMinutes)
        Me.pnlInfo.Controls.Add(Me.lblMessage)
        Me.pnlInfo.Controls.Add(Me.txtMessage)
        Me.pnlInfo.Controls.Add(Me.btnSendMsg)
        Me.pnlInfo.Controls.Add(Me.btnShutdown)
        Me.pnlInfo.Controls.Add(Me.btnStart)
        Me.pnlInfo.Controls.Add(Me.btnEnd)
        Me.pnlInfo.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlInfo.Location = New System.Drawing.Point(731, 39)
        Me.pnlInfo.Name = "pnlInfo"
        Me.pnlInfo.Padding = New System.Windows.Forms.Padding(15, 13, 15, 13)
        Me.pnlInfo.Size = New System.Drawing.Size(225, 481)
        Me.pnlInfo.TabIndex = 1
        '
        'lblPCName
        '
        Me.lblPCName.AutoSize = True
        Me.lblPCName.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblPCName.Location = New System.Drawing.Point(23, 23)
        Me.lblPCName.Name = "lblPCName"
        Me.lblPCName.Size = New System.Drawing.Size(111, 25)
        Me.lblPCName.TabIndex = 0
        Me.lblPCName.Text = "PC Name: -"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblStatus.Location = New System.Drawing.Point(23, 53)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(65, 21)
        Me.lblStatus.TabIndex = 1
        Me.lblStatus.Text = "Status: -"
        '
        'lblTimer
        '
        Me.lblTimer.AutoSize = True
        Me.lblTimer.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblTimer.Location = New System.Drawing.Point(23, 83)
        Me.lblTimer.Name = "lblTimer"
        Me.lblTimer.Size = New System.Drawing.Size(188, 21)
        Me.lblTimer.TabIndex = 2
        Me.lblTimer.Text = "Usage Time: 00:00 | ₱0.00"
        '
        'lblSessionType
        '
        Me.lblSessionType.AutoSize = True
        Me.lblSessionType.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblSessionType.Location = New System.Drawing.Point(22, 115)
        Me.lblSessionType.Name = "lblSessionType"
        Me.lblSessionType.Size = New System.Drawing.Size(102, 21)
        Me.lblSessionType.TabIndex = 3
        Me.lblSessionType.Text = "Session Type:"
        '
        'cmbSessionType
        '
        Me.cmbSessionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSessionType.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.cmbSessionType.Items.AddRange(New Object() {"Open", "Fixed"})
        Me.cmbSessionType.Location = New System.Drawing.Point(8, 125)
        Me.cmbSessionType.Name = "cmbSessionType"
        Me.cmbSessionType.Size = New System.Drawing.Size(188, 29)
        Me.cmbSessionType.TabIndex = 4
        '
        'lblSales
        '
        Me.lblSales.AutoSize = True
        Me.lblSales.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblSales.Location = New System.Drawing.Point(22, 163)
        Me.lblSales.Name = "lblSales"
        Me.lblSales.Size = New System.Drawing.Size(92, 21)
        Me.lblSales.TabIndex = 5
        Me.lblSales.Text = "Amount (₱):"
        '
        'txtSales
        '
        Me.txtSales.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtSales.Location = New System.Drawing.Point(8, 185)
        Me.txtSales.Name = "txtSales"
        Me.txtSales.Size = New System.Drawing.Size(188, 29)
        Me.txtSales.TabIndex = 6
        '
        'lblExtraMinutes
        '
        Me.lblExtraMinutes.AutoSize = True
        Me.lblExtraMinutes.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblExtraMinutes.Location = New System.Drawing.Point(8, 220)
        Me.lblExtraMinutes.Name = "lblExtraMinutes"
        Me.lblExtraMinutes.Size = New System.Drawing.Size(98, 21)
        Me.lblExtraMinutes.TabIndex = 13
        Me.lblExtraMinutes.Text = "Add Amount"
        '
        'txtExtraMinutes
        '
        Me.txtExtraMinutes.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtExtraMinutes.Location = New System.Drawing.Point(8, 245)
        Me.txtExtraMinutes.Name = "txtExtraMinutes"
        Me.txtExtraMinutes.Size = New System.Drawing.Size(100, 29)
        Me.txtExtraMinutes.TabIndex = 14
        '
        'btnAddMinutes
        '
        Me.btnAddMinutes.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(164, Byte), Integer))
        Me.btnAddMinutes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddMinutes.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnAddMinutes.ForeColor = System.Drawing.Color.White
        Me.btnAddMinutes.Location = New System.Drawing.Point(115, 245)
        Me.btnAddMinutes.Name = "btnAddMinutes"
        Me.btnAddMinutes.Size = New System.Drawing.Size(81, 29)
        Me.btnAddMinutes.TabIndex = 15
        Me.btnAddMinutes.Text = "Add"
        Me.btnAddMinutes.UseVisualStyleBackColor = False
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblMessage.Location = New System.Drawing.Point(8, 280)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(74, 21)
        Me.lblMessage.TabIndex = 16
        Me.lblMessage.Text = "Message:"
        '
        'txtMessage
        '
        Me.txtMessage.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.txtMessage.Location = New System.Drawing.Point(8, 303)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(188, 29)
        Me.txtMessage.TabIndex = 17
        '
        'btnSendMsg
        '
        Me.btnSendMsg.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(164, Byte), Integer))
        Me.btnSendMsg.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSendMsg.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnSendMsg.ForeColor = System.Drawing.Color.White
        Me.btnSendMsg.Location = New System.Drawing.Point(8, 338)
        Me.btnSendMsg.Name = "btnSendMsg"
        Me.btnSendMsg.Size = New System.Drawing.Size(188, 41)
        Me.btnSendMsg.TabIndex = 18
        Me.btnSendMsg.Text = "✉ Send Message"
        Me.btnSendMsg.UseVisualStyleBackColor = False
        '
        'btnShutdown
        '
        Me.btnShutdown.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.btnShutdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShutdown.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnShutdown.ForeColor = System.Drawing.Color.White
        Me.btnShutdown.Location = New System.Drawing.Point(8, 385)
        Me.btnShutdown.Name = "btnShutdown"
        Me.btnShutdown.Size = New System.Drawing.Size(188, 41)
        Me.btnShutdown.TabIndex = 19
        Me.btnShutdown.Text = "⏻ Shutdown PC"
        Me.btnShutdown.UseVisualStyleBackColor = False
        '
        'btnStart
        '
        Me.btnStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(164, Byte), Integer))
        Me.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStart.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnStart.ForeColor = System.Drawing.Color.White
        Me.btnStart.Location = New System.Drawing.Point(8, 432)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(188, 41)
        Me.btnStart.TabIndex = 7
        Me.btnStart.Text = "▶ Start Session"
        Me.btnStart.UseVisualStyleBackColor = False
        '
        'btnEnd
        '
        Me.btnEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.btnEnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEnd.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnEnd.ForeColor = System.Drawing.Color.White
        Me.btnEnd.Location = New System.Drawing.Point(8, 479)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(188, 41)
        Me.btnEnd.TabIndex = 8
        Me.btnEnd.Text = "⏹ End Session"
        Me.btnEnd.UseVisualStyleBackColor = False
        '
        'sessionTimer
        '
        Me.sessionTimer.Interval = 1000
        '
        'sessionfrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(956, 520)
        Me.Controls.Add(Me.pnlComputers)
        Me.Controls.Add(Me.pnlInfo)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "sessionfrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
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
    Friend WithEvents lblExtraMinutes As Label
    Friend WithEvents txtExtraMinutes As TextBox
    Friend WithEvents btnAddMinutes As Button
    Friend WithEvents btnStart As Button
    Friend WithEvents btnEnd As Button
    Friend WithEvents sessionTimer As Timer
    Friend WithEvents lblSessionType As Label
    Friend WithEvents cmbSessionType As ComboBox

    ' New controls
    Friend WithEvents lblMessage As Label
    Friend WithEvents txtMessage As TextBox
    Friend WithEvents btnSendMsg As Button
    Friend WithEvents btnShutdown As Button
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AdminFrm
    Inherits System.Windows.Forms.Form

    'Dispose
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Components
    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlSidebar = New System.Windows.Forms.Panel()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.btnMonitoring = New System.Windows.Forms.Button()
        Me.btnPCManagement = New System.Windows.Forms.Button()
        Me.btnStaffManagement = New System.Windows.Forms.Button()
        Me.lblLogo = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlSidebar.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSidebar
        '
        Me.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(25, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.pnlSidebar.Controls.Add(Me.btnLogout)
        Me.pnlSidebar.Controls.Add(Me.btnMonitoring)
        Me.pnlSidebar.Controls.Add(Me.btnPCManagement)
        Me.pnlSidebar.Controls.Add(Me.btnStaffManagement)
        Me.pnlSidebar.Controls.Add(Me.lblLogo)
        Me.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSidebar.Location = New System.Drawing.Point(0, 0)
        Me.pnlSidebar.Name = "pnlSidebar"
        Me.pnlSidebar.Size = New System.Drawing.Size(180, 729)
        Me.pnlSidebar.TabIndex = 0
        '
        'btnLogout
        '
        Me.btnLogout.FlatAppearance.BorderSize = 0
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!)
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.Location = New System.Drawing.Point(0, 650)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(180, 60)
        Me.btnLogout.TabIndex = 3
        Me.btnLogout.Text = "Logout"
        Me.btnLogout.UseVisualStyleBackColor = True
        '
        'btnMonitoring
        '
        Me.btnMonitoring.FlatAppearance.BorderSize = 0
        Me.btnMonitoring.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMonitoring.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!)
        Me.btnMonitoring.ForeColor = System.Drawing.Color.White
        Me.btnMonitoring.Location = New System.Drawing.Point(0, 227)
        Me.btnMonitoring.Name = "btnMonitoring"
        Me.btnMonitoring.Size = New System.Drawing.Size(180, 60)
        Me.btnMonitoring.TabIndex = 2
        Me.btnMonitoring.Text = "🧾  Monitoring / Logs"
        Me.btnMonitoring.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMonitoring.UseVisualStyleBackColor = True
        '
        'btnPCManagement
        '
        Me.btnPCManagement.FlatAppearance.BorderSize = 0
        Me.btnPCManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPCManagement.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!)
        Me.btnPCManagement.ForeColor = System.Drawing.Color.White
        Me.btnPCManagement.Location = New System.Drawing.Point(0, 160)
        Me.btnPCManagement.Name = "btnPCManagement"
        Me.btnPCManagement.Size = New System.Drawing.Size(180, 60)
        Me.btnPCManagement.TabIndex = 1
        Me.btnPCManagement.Text = "💻  PC Management"
        Me.btnPCManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPCManagement.UseVisualStyleBackColor = True
        '
        'btnStaffManagement
        '
        Me.btnStaffManagement.FlatAppearance.BorderSize = 0
        Me.btnStaffManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStaffManagement.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!)
        Me.btnStaffManagement.ForeColor = System.Drawing.Color.White
        Me.btnStaffManagement.Location = New System.Drawing.Point(0, 100)
        Me.btnStaffManagement.Name = "btnStaffManagement"
        Me.btnStaffManagement.Size = New System.Drawing.Size(180, 60)
        Me.btnStaffManagement.TabIndex = 0
        Me.btnStaffManagement.Text = "👤  Staff Management"
        Me.btnStaffManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnStaffManagement.UseVisualStyleBackColor = True
        '
        'lblLogo
        '
        Me.lblLogo.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblLogo.ForeColor = System.Drawing.Color.White
        Me.lblLogo.Location = New System.Drawing.Point(0, 20)
        Me.lblLogo.Name = "lblLogo"
        Me.lblLogo.Size = New System.Drawing.Size(180, 40)
        Me.lblLogo.TabIndex = 0
        Me.lblLogo.Text = "PACITA CAFE"
        Me.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(180, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1079, 729)
        Me.pnlMain.TabIndex = 2
        '
        'AdminFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1259, 729)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlSidebar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "AdminFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Admin Dashboard - Pacita Computer Shop"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlSidebar.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlSidebar As Panel
    Friend WithEvents btnLogout As Button
    Friend WithEvents btnMonitoring As Button
    Friend WithEvents btnPCManagement As Button
    Friend WithEvents btnStaffManagement As Button
    Friend WithEvents lblLogo As Label
    Friend WithEvents pnlMain As Panel
End Class

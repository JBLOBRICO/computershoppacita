<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frmpcstatus
    Inherits System.Windows.Forms.Form

    ' Clean up resources
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

    Private components As System.ComponentModel.IContainer

    ' Required by the Designer
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.dgvPCStatus = New System.Windows.Forms.DataGridView()
        Me.pnlSide = New System.Windows.Forms.Panel()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblAvailable = New System.Windows.Forms.Label()
        Me.lblInUse = New System.Windows.Forms.Label()
        Me.lblMaintenance = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        CType(Me.dgvPCStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSide.SuspendLayout()
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
        Me.pnlHeader.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(15, 7)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(230, 30)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "🖥️  Computer Status"
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.dgvPCStatus)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 39)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlMain.Size = New System.Drawing.Size(731, 359)
        Me.pnlMain.TabIndex = 1
        '
        'dgvPCStatus
        '
        Me.dgvPCStatus.AllowUserToAddRows = False
        Me.dgvPCStatus.AllowUserToDeleteRows = False
        Me.dgvPCStatus.AllowUserToResizeColumns = False
        Me.dgvPCStatus.AllowUserToResizeRows = False
        Me.dgvPCStatus.BackgroundColor = System.Drawing.Color.White
        Me.dgvPCStatus.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvPCStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPCStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPCStatus.Location = New System.Drawing.Point(15, 15)
        Me.dgvPCStatus.Name = "dgvPCStatus"
        Me.dgvPCStatus.ReadOnly = True
        Me.dgvPCStatus.RowHeadersVisible = False
        Me.dgvPCStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPCStatus.Size = New System.Drawing.Size(701, 329)
        Me.dgvPCStatus.TabIndex = 0
        '
        'pnlSide
        '
        Me.pnlSide.BackColor = System.Drawing.Color.White
        Me.pnlSide.Controls.Add(Me.btnRefresh)
        Me.pnlSide.Controls.Add(Me.lblTotal)
        Me.pnlSide.Controls.Add(Me.lblAvailable)
        Me.pnlSide.Controls.Add(Me.lblInUse)
        Me.pnlSide.Controls.Add(Me.lblMaintenance)
        Me.pnlSide.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlSide.Location = New System.Drawing.Point(731, 39)
        Me.pnlSide.Name = "pnlSide"
        Me.pnlSide.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlSide.Size = New System.Drawing.Size(225, 359)
        Me.pnlSide.TabIndex = 2
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(164, Byte), Integer))
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(13, 220)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(188, 32)
        Me.btnRefresh.TabIndex = 4
        Me.btnRefresh.Text = "🔄 Refresh Status"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotal.Location = New System.Drawing.Point(10, 15)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(96, 21)
        Me.lblTotal.TabIndex = 0
        Me.lblTotal.Text = "Total PCs: 0"
        '
        'lblAvailable
        '
        Me.lblAvailable.AutoSize = True
        Me.lblAvailable.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblAvailable.ForeColor = System.Drawing.Color.Gray
        Me.lblAvailable.Location = New System.Drawing.Point(10, 55)
        Me.lblAvailable.Name = "lblAvailable"
        Me.lblAvailable.Size = New System.Drawing.Size(115, 21)
        Me.lblAvailable.TabIndex = 1
        Me.lblAvailable.Text = "🟢 Available: 0"
        '
        'lblInUse
        '
        Me.lblInUse.AutoSize = True
        Me.lblInUse.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblInUse.ForeColor = System.Drawing.Color.Green
        Me.lblInUse.Location = New System.Drawing.Point(10, 85)
        Me.lblInUse.Name = "lblInUse"
        Me.lblInUse.Size = New System.Drawing.Size(95, 21)
        Me.lblInUse.TabIndex = 2
        Me.lblInUse.Text = "🟢 In Use: 0"
        '
        'lblMaintenance
        '
        Me.lblMaintenance.AutoSize = True
        Me.lblMaintenance.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblMaintenance.ForeColor = System.Drawing.Color.IndianRed
        Me.lblMaintenance.Location = New System.Drawing.Point(10, 115)
        Me.lblMaintenance.Name = "lblMaintenance"
        Me.lblMaintenance.Size = New System.Drawing.Size(141, 21)
        Me.lblMaintenance.TabIndex = 3
        Me.lblMaintenance.Text = "🔧 Maintenance: 0"
        '
        'Frmpcstatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(956, 398)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlSide)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frmpcstatus"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PC Status"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        CType(Me.dgvPCStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSide.ResumeLayout(False)
        Me.pnlSide.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents pnlMain As Panel
    Friend WithEvents dgvPCStatus As DataGridView
    Friend WithEvents pnlSide As Panel
    Friend WithEvents lblTotal As Label
    Friend WithEvents lblAvailable As Label
    Friend WithEvents lblInUse As Label
    Friend WithEvents lblMaintenance As Label
    Friend WithEvents btnRefresh As Button
End Class

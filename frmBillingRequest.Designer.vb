<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBillingRequest
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.dgvBilling = New System.Windows.Forms.DataGridView()
        Me.pnlSide = New System.Windows.Forms.Panel()
        Me.btnRequestBill = New System.Windows.Forms.Button()
        Me.lblPending = New System.Windows.Forms.Label()
        Me.lblTotalAmount = New System.Windows.Forms.Label()
        Me.lblTotalBills = New System.Windows.Forms.Label()
        Me.ttButtons = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        CType(Me.dgvBilling, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlHeader.Size = New System.Drawing.Size(960, 50)
        Me.pnlHeader.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(20, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(214, 30)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "📑 Billing Manager"
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.dgvBilling)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 50)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlMain.Size = New System.Drawing.Size(730, 400)
        Me.pnlMain.TabIndex = 0
        '
        'dgvBilling
        '
        Me.dgvBilling.AllowUserToAddRows = False
        Me.dgvBilling.AllowUserToDeleteRows = False
        Me.dgvBilling.AllowUserToResizeColumns = False
        Me.dgvBilling.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgvBilling.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvBilling.BackgroundColor = System.Drawing.Color.White
        Me.dgvBilling.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(164, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvBilling.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvBilling.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBilling.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvBilling.EnableHeadersVisualStyles = False
        Me.dgvBilling.Location = New System.Drawing.Point(15, 15)
        Me.dgvBilling.Name = "dgvBilling"
        Me.dgvBilling.ReadOnly = True
        Me.dgvBilling.RowHeadersVisible = False
        Me.dgvBilling.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvBilling.Size = New System.Drawing.Size(700, 370)
        Me.dgvBilling.TabIndex = 0
        '
        'pnlSide
        '
        Me.pnlSide.BackColor = System.Drawing.Color.White
        Me.pnlSide.Controls.Add(Me.btnRequestBill)
        Me.pnlSide.Controls.Add(Me.lblPending)
        Me.pnlSide.Controls.Add(Me.lblTotalAmount)
        Me.pnlSide.Controls.Add(Me.lblTotalBills)
        Me.pnlSide.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlSide.Location = New System.Drawing.Point(730, 50)
        Me.pnlSide.Name = "pnlSide"
        Me.pnlSide.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlSide.Size = New System.Drawing.Size(230, 400)
        Me.pnlSide.TabIndex = 1
        '
        'btnRequestBill
        '
        Me.btnRequestBill.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(164, Byte), Integer))
        Me.btnRequestBill.FlatAppearance.BorderSize = 0
        Me.btnRequestBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRequestBill.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnRequestBill.ForeColor = System.Drawing.Color.White
        Me.btnRequestBill.Location = New System.Drawing.Point(15, 200)
        Me.btnRequestBill.Name = "btnRequestBill"
        Me.btnRequestBill.Size = New System.Drawing.Size(200, 35)
        Me.btnRequestBill.TabIndex = 0
        Me.btnRequestBill.Text = "➕ Request New Bill"
        Me.ttButtons.SetToolTip(Me.btnRequestBill, "Click to create a new billing request")
        Me.btnRequestBill.UseVisualStyleBackColor = False
        '
        'lblPending
        '
        Me.lblPending.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblPending.ForeColor = System.Drawing.Color.Gray
        Me.lblPending.Location = New System.Drawing.Point(15, 85)
        Me.lblPending.Name = "lblPending"
        Me.lblPending.Size = New System.Drawing.Size(100, 23)
        Me.lblPending.TabIndex = 2
        Me.lblPending.Text = "🕐 Pending Bills: 0"
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblTotalAmount.ForeColor = System.Drawing.Color.Gray
        Me.lblTotalAmount.Location = New System.Drawing.Point(15, 50)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(100, 23)
        Me.lblTotalAmount.TabIndex = 3
        Me.lblTotalAmount.Text = "💵 Total Amount: ₱0"
        '
        'lblTotalBills
        '
        Me.lblTotalBills.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalBills.Location = New System.Drawing.Point(15, 15)
        Me.lblTotalBills.Name = "lblTotalBills"
        Me.lblTotalBills.Size = New System.Drawing.Size(100, 23)
        Me.lblTotalBills.TabIndex = 4
        Me.lblTotalBills.Text = "Total Bills: 0 🧾"
        '
        'frmBillingRequest
        '
        Me.ClientSize = New System.Drawing.Size(960, 450)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlSide)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmBillingRequest"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Billing Manager"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        CType(Me.dgvBilling, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSide.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents pnlMain As Panel
    Friend WithEvents dgvBilling As DataGridView
    Friend WithEvents pnlSide As Panel
    Friend WithEvents btnRequestBill As Button
    Friend WithEvents lblTotalBills As Label
    Friend WithEvents lblTotalAmount As Label
    Friend WithEvents lblPending As Label
    Friend WithEvents ttButtons As ToolTip

End Class

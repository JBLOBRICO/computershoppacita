<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Pcmanagement
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents pnlTop As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents pnlMain As Panel
    Friend WithEvents pnlInputs As Panel

    Friend WithEvents dgvPC As DataGridView
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnClear As Button

    Friend WithEvents lblPCID As Label
    Friend WithEvents lblPCName As Label
    Friend WithEvents lblPCStatus As Label
    Friend WithEvents txtPCID As TextBox
    Friend WithEvents txtPCName As TextBox
    Friend WithEvents cmbPCStatus As ComboBox

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.dgvPC = New System.Windows.Forms.DataGridView()
        Me.pnlInputs = New System.Windows.Forms.Panel()
        Me.lblPCID = New System.Windows.Forms.Label()
        Me.txtPCID = New System.Windows.Forms.TextBox()
        Me.lblPCName = New System.Windows.Forms.Label()
        Me.txtPCName = New System.Windows.Forms.TextBox()
        Me.lblPCStatus = New System.Windows.Forms.Label()
        Me.cmbPCStatus = New System.Windows.Forms.ComboBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.pnlTop.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        CType(Me.dgvPC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInputs.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.pnlTop.Controls.Add(Me.lblTitle)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1200, 70)
        Me.pnlTop.TabIndex = 1
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(20, 15)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(477, 32)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "PC Management — Pacita Computer Shop"
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.dgvPC)
        Me.pnlMain.Controls.Add(Me.pnlInputs)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 70)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlMain.Size = New System.Drawing.Size(1200, 630)
        Me.pnlMain.TabIndex = 0
        '
        'dgvPC
        '
        Me.dgvPC.AllowUserToAddRows = False
        Me.dgvPC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPC.BackgroundColor = System.Drawing.Color.White
        Me.dgvPC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPC.Location = New System.Drawing.Point(20, 20)
        Me.dgvPC.MultiSelect = False
        Me.dgvPC.Name = "dgvPC"
        Me.dgvPC.ReadOnly = True
        Me.dgvPC.RowHeadersWidth = 51
        Me.dgvPC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPC.Size = New System.Drawing.Size(810, 590)
        Me.dgvPC.TabIndex = 0
        '
        'pnlInputs
        '
        Me.pnlInputs.BackColor = System.Drawing.Color.White
        Me.pnlInputs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlInputs.Controls.Add(Me.lblPCID)
        Me.pnlInputs.Controls.Add(Me.txtPCID)
        Me.pnlInputs.Controls.Add(Me.lblPCName)
        Me.pnlInputs.Controls.Add(Me.txtPCName)
        Me.pnlInputs.Controls.Add(Me.lblPCStatus)
        Me.pnlInputs.Controls.Add(Me.cmbPCStatus)
        Me.pnlInputs.Controls.Add(Me.btnAdd)
        Me.pnlInputs.Controls.Add(Me.btnUpdate)
        Me.pnlInputs.Controls.Add(Me.btnDelete)
        Me.pnlInputs.Controls.Add(Me.btnClear)
        Me.pnlInputs.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlInputs.Location = New System.Drawing.Point(830, 20)
        Me.pnlInputs.Name = "pnlInputs"
        Me.pnlInputs.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlInputs.Size = New System.Drawing.Size(350, 590)
        Me.pnlInputs.TabIndex = 1
        '
        'lblPCID
        '
        Me.lblPCID.AutoSize = True
        Me.lblPCID.Location = New System.Drawing.Point(10, 10)
        Me.lblPCID.Name = "lblPCID"
        Me.lblPCID.Size = New System.Drawing.Size(38, 13)
        Me.lblPCID.TabIndex = 0
        Me.lblPCID.Text = "PC ID:"
        '
        'txtPCID
        '
        Me.txtPCID.Location = New System.Drawing.Point(120, 10)
        Me.txtPCID.Name = "txtPCID"
        Me.txtPCID.Size = New System.Drawing.Size(200, 20)
        Me.txtPCID.TabIndex = 1
        '
        'lblPCName
        '
        Me.lblPCName.AutoSize = True
        Me.lblPCName.Location = New System.Drawing.Point(10, 50)
        Me.lblPCName.Name = "lblPCName"
        Me.lblPCName.Size = New System.Drawing.Size(55, 13)
        Me.lblPCName.TabIndex = 2
        Me.lblPCName.Text = "PC Name:"
        '
        'txtPCName
        '
        Me.txtPCName.Location = New System.Drawing.Point(120, 50)
        Me.txtPCName.Name = "txtPCName"
        Me.txtPCName.Size = New System.Drawing.Size(200, 20)
        Me.txtPCName.TabIndex = 3
        '
        'lblPCStatus
        '
        Me.lblPCStatus.AutoSize = True
        Me.lblPCStatus.Location = New System.Drawing.Point(10, 96)
        Me.lblPCStatus.Name = "lblPCStatus"
        Me.lblPCStatus.Size = New System.Drawing.Size(40, 13)
        Me.lblPCStatus.TabIndex = 6
        Me.lblPCStatus.Text = "Status:"
        '
        'cmbPCStatus
        '
        Me.cmbPCStatus.Items.AddRange(New Object() {"Available", "In Use", "Maintenance"})
        Me.cmbPCStatus.Location = New System.Drawing.Point(120, 96)
        Me.cmbPCStatus.Name = "cmbPCStatus"
        Me.cmbPCStatus.Size = New System.Drawing.Size(200, 21)
        Me.cmbPCStatus.TabIndex = 7
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.ForeColor = System.Drawing.Color.White
        Me.btnAdd.Location = New System.Drawing.Point(120, 146)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(100, 35)
        Me.btnAdd.TabIndex = 8
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'btnUpdate
        '
        Me.btnUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdate.ForeColor = System.Drawing.Color.White
        Me.btnUpdate.Location = New System.Drawing.Point(230, 146)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(100, 35)
        Me.btnUpdate.TabIndex = 9
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.ForeColor = System.Drawing.Color.White
        Me.btnDelete.Location = New System.Drawing.Point(120, 196)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 35)
        Me.btnDelete.TabIndex = 10
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Location = New System.Drawing.Point(230, 196)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(100, 35)
        Me.btnClear.TabIndex = 11
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'pcmanagement
        '
        Me.ClientSize = New System.Drawing.Size(1200, 700)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "pcmanagement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PC Management - Pacita Computer Shop"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        CType(Me.dgvPC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInputs.ResumeLayout(False)
        Me.pnlInputs.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
End Class

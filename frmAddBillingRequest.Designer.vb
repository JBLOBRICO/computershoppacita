<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAddBillingRequest
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
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlContent = New System.Windows.Forms.Panel()
        Me.txtsaleid = New System.Windows.Forms.TextBox()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.dtpDueDate = New System.Windows.Forms.DateTimePicker()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.lblDueDate = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(164, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(411, 39)
        Me.pnlHeader.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(13, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(197, 25)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "➕ Add New Request"
        '
        'pnlContent
        '
        Me.pnlContent.BackColor = System.Drawing.Color.White
        Me.pnlContent.Controls.Add(Me.txtsaleid)
        Me.pnlContent.Controls.Add(Me.cmbCategory)
        Me.pnlContent.Controls.Add(Me.txtDescription)
        Me.pnlContent.Controls.Add(Me.txtAmount)
        Me.pnlContent.Controls.Add(Me.dtpDueDate)
        Me.pnlContent.Controls.Add(Me.cmbStatus)
        Me.pnlContent.Controls.Add(Me.lblCategory)
        Me.pnlContent.Controls.Add(Me.lblDescription)
        Me.pnlContent.Controls.Add(Me.lblAmount)
        Me.pnlContent.Controls.Add(Me.lblDueDate)
        Me.pnlContent.Controls.Add(Me.lblStatus)
        Me.pnlContent.Controls.Add(Me.btnSave)
        Me.pnlContent.Controls.Add(Me.btnCancel)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 39)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Padding = New System.Windows.Forms.Padding(21, 22, 21, 22)
        Me.pnlContent.Size = New System.Drawing.Size(411, 290)
        Me.pnlContent.TabIndex = 1
        '
        'txtsaleid
        '
        Me.txtsaleid.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtsaleid.Location = New System.Drawing.Point(47, 7)
        Me.txtsaleid.Name = "txtsaleid"
        Me.txtsaleid.Size = New System.Drawing.Size(57, 27)
        Me.txtsaleid.TabIndex = 10
        Me.txtsaleid.Visible = False
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Items.AddRange(New Object() {"Billing", "Replacement"})
        Me.cmbCategory.Location = New System.Drawing.Point(129, 35)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(241, 28)
        Me.cmbCategory.TabIndex = 0
        '
        'txtDescription
        '
        Me.txtDescription.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtDescription.Location = New System.Drawing.Point(129, 74)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(241, 53)
        Me.txtDescription.TabIndex = 1
        '
        'txtAmount
        '
        Me.txtAmount.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtAmount.Location = New System.Drawing.Point(129, 139)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(241, 27)
        Me.txtAmount.TabIndex = 2
        '
        'dtpDueDate
        '
        Me.dtpDueDate.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDueDate.Location = New System.Drawing.Point(129, 173)
        Me.dtpDueDate.Name = "dtpDueDate"
        Me.dtpDueDate.Size = New System.Drawing.Size(241, 27)
        Me.dtpDueDate.TabIndex = 3
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Items.AddRange(New Object() {"Pending", "Approved", "Paid"})
        Me.cmbStatus.Location = New System.Drawing.Point(129, 208)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(241, 28)
        Me.cmbStatus.TabIndex = 4
        Me.cmbStatus.Visible = False
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.lblCategory.Location = New System.Drawing.Point(39, 37)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(72, 20)
        Me.lblCategory.TabIndex = 5
        Me.lblCategory.Text = "Category:"
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.lblDescription.Location = New System.Drawing.Point(39, 76)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(88, 20)
        Me.lblDescription.TabIndex = 6
        Me.lblDescription.Text = "Description:"
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.lblAmount.Location = New System.Drawing.Point(39, 141)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(65, 20)
        Me.lblAmount.TabIndex = 7
        Me.lblAmount.Text = "Amount:"
        '
        'lblDueDate
        '
        Me.lblDueDate.AutoSize = True
        Me.lblDueDate.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.lblDueDate.Location = New System.Drawing.Point(39, 176)
        Me.lblDueDate.Name = "lblDueDate"
        Me.lblDueDate.Size = New System.Drawing.Size(75, 20)
        Me.lblDueDate.TabIndex = 8
        Me.lblDueDate.Text = "Due Date:"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.lblStatus.Location = New System.Drawing.Point(39, 211)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(52, 20)
        Me.lblStatus.TabIndex = 9
        Me.lblStatus.Text = "Status:"
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(164, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(129, 251)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(103, 30)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "💾 Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Gray
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(266, 251)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(103, 30)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "✖ Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'frmAddBillingRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(411, 329)
        Me.Controls.Add(Me.pnlContent)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmAddBillingRequest"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add Billing Request"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlContent.ResumeLayout(False)
        Me.pnlContent.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents pnlContent As Panel
    Friend WithEvents cmbCategory As ComboBox
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents dtpDueDate As DateTimePicker
    Friend WithEvents cmbStatus As ComboBox
    Friend WithEvents lblCategory As Label
    Friend WithEvents lblDescription As Label
    Friend WithEvents lblAmount As Label
    Friend WithEvents lblDueDate As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents txtsaleid As TextBox
End Class

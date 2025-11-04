<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ClientListener
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.pnlTimer = New System.Windows.Forms.Panel()
        Me.lblTimer = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'pnlTimer
        '
        Me.pnlTimer.BackColor = System.Drawing.Color.Gray
        Me.pnlTimer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTimer.Location = New System.Drawing.Point(20, 20)
        Me.pnlTimer.Name = "pnlTimer"
        Me.pnlTimer.Size = New System.Drawing.Size(260, 60)
        Me.pnlTimer.TabIndex = 0
        Me.pnlTimer.Padding = New Padding(5)
        Me.pnlTimer.Controls.Add(Me.lblTimer)
        '
        'lblTimer
        '
        Me.lblTimer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTimer.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTimer.ForeColor = System.Drawing.Color.White
        Me.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTimer.Text = "Usage Time: 00:00 | ₱0.00"
        '
        'ClientListener
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(300, 120)
        Me.Controls.Add(Me.pnlTimer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ClientListener"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Client Listener"
        Me.TopMost = True
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlTimer As Panel
    Friend WithEvents lblTimer As Label
End Class

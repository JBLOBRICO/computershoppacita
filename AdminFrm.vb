Public Class AdminFrm
    Private Sub LoadChildForm(childForm As Form)
        pnlMain.Controls.Clear()
        childForm.TopLevel = False
        childForm.FormBorderStyle = FormBorderStyle.None
        childForm.Dock = DockStyle.Fill
        pnlMain.Controls.Add(childForm)
        childForm.Show()
    End Sub

    Public Sub New()
        InitializeComponent()

        ' Turn on double buffering to prevent flicker
        Me.DoubleBuffered = True
        SetDoubleBuffered(pnlMain)
    End Sub

    Private Sub SetDoubleBuffered(ctrl As Control)
        If SystemInformation.TerminalServerSession Then Return
        Dim prop = ctrl.GetType().GetProperty("DoubleBuffered", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
        prop.SetValue(ctrl, True, Nothing)
    End Sub

    Private Sub btnStaffManagement_Click(sender As Object, e As EventArgs) Handles btnStaffManagement.Click
        LoadChildForm(New StaffManagement())
    End Sub

    Private Sub pnlTop_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub btnPCManagement_Click(sender As Object, e As EventArgs) Handles btnPCManagement.Click
        LoadChildForm(New Pcmanagement())
    End Sub

    Private Sub btnMonitoring_Click(sender As Object, e As EventArgs) Handles btnMonitoring.Click
        LoadChildForm(New MonitorLogs())
    End Sub

    Private Sub pnlMain_Paint(sender As Object, e As PaintEventArgs) Handles pnlMain.Paint

    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click

        If MsgBox("Are you sure you want to logout?", vbYesNo Or vbQuestion, "Confirmation") = vbYes Then
            Me.Close()
            Form1.Show()
        End If
    End Sub
End Class
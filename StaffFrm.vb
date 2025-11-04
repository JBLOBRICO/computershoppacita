Imports Mysqlx.XDevAPI

Public Class StaffFrm
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


    Private Sub pnlMain_Paint(sender As Object, e As PaintEventArgs) Handles pnlMain.Paint

    End Sub

    Private Sub btnSession_Click(sender As Object, e As EventArgs) Handles btnSession.Click
        LoadChildForm(New Sessionfrm())
    End Sub

    Private Sub btnPCStatus_Click(sender As Object, e As EventArgs) Handles btnPCStatus.Click
        LoadChildForm(New Frmpcstatus())
    End Sub

    Private Sub btnSales_Click(sender As Object, e As EventArgs) Handles btnSales.Click
        LoadChildForm(New frmsalestracker())
    End Sub

    Private Sub btnBilling_Click(sender As Object, e As EventArgs) Handles btnBilling.Click
        LoadChildForm(New frmBillingRequest())
    End Sub

    Private Sub pnlHeader_Paint(sender As Object, e As PaintEventArgs) Handles pnlHeader.Paint

    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        ' Check if the current child form is Sessionfrm
        Dim sessionForm As sessionfrm = Nothing

        For Each ctrl As Control In pnlMain.Controls
            If TypeOf ctrl Is sessionfrm Then
                sessionForm = CType(ctrl, sessionfrm)
                Exit For
            End If
        Next

        ' If Sessionfrm is open, check CanLogout
        If sessionForm IsNot Nothing Then
            If Not sessionForm.CanLogout() Then
                ' Active sessions exist, prevent logout
                Return
            End If
        End If

        ' Confirm logout
        If MsgBox("Are you sure you want to logout?", vbQuestion + vbYesNo, "Logout") = vbYes Then
            Me.Close()
            Form1.Show()
        End If
    End Sub

    Private Sub lblStaffName_Click(sender As Object, e As EventArgs) Handles lblStaffName.Click

    End Sub

    Private Sub StaffFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Display the currently logged-in staff name
            If Not String.IsNullOrEmpty(LoggedInFullName) Then
                lblStaffName.Text = "Welcome, " & LoggedInFullName
            ElseIf Not String.IsNullOrEmpty(loginusername) Then
                lblStaffName.Text = "Welcome, " & loginusername
            Else
                lblStaffName.Text = "Welcome, Staff"
            End If
        Catch ex As Exception
            lblStaffName.Text = "Welcome, Staff"
        End Try
    End Sub
End Class
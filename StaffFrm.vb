Imports MySql.Data.MySqlClient
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
        ' Get the open Sessionfrm in pnlMain
        Dim sessionForm As sessionfrm = pnlMain.Controls.OfType(Of sessionfrm)().FirstOrDefault()

        ' Check if sessionForm exists
        If sessionForm IsNot Nothing Then
            ' Ensure CanLogout() exists and check active sessions
            Dim canLogout As Boolean
            Try
                canLogout = sessionForm.CanLogout()
            Catch ex As Exception
                MessageBox.Show("Cannot check active sessions: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try

            If Not canLogout Then
                MessageBox.Show("Cannot logout while PCs have active sessions.", "Active Sessions", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        End If

        ' Confirm logout
        If MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.Close()
            Form1.Show()
        End If
    End Sub

    Private Sub lblStaffName_Click(sender As Object, e As EventArgs) Handles lblStaffName.Click

    End Sub
    ' ====== CAN LOGOUT PROPERTY BASED ON DATABASE ======
    Public ReadOnly Property CanLogout As Boolean
        Get
            Try
                OpenConnection()
                ' Check if there are any active fixed sessions in the database
                Dim query As String = "SELECT COUNT(*) FROM sales WHERE EndTime IS NULL AND TotalAmount > 0"
                cmd = New MySqlCommand(query, conn)
                Dim activeFixedSessions As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return activeFixedSessions = 0
            Catch ex As Exception
                MessageBox.Show("Error checking active sessions: " & ex.Message)
                ' Fail-safe: prevent logout if error occurs
                Return False
            Finally
                CloseConnection()
            End Try
        End Get
    End Property

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

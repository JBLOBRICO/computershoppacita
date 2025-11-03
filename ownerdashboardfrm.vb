Imports MySql.Data.MySqlClient

Public Class ownerdashboardfrm

    Private Sub ownerdashboardfrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConnection()

            LoadSummaryCards()
            LoadRecentSales()
            LoadPendingRequests()

        Catch ex As Exception
            MessageBox.Show("Error loading dashboard: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' -----------------------------
    ' SUMMARY CARDS SECTION
    ' -----------------------------
    Private Sub LoadSummaryCards()
        flowCards.Controls.Clear()

        Dim cardTitles() As String = {"Active PCs", "Daily Revenue", "Unpaid Sales", "Pending Replacements", "Pending Billings"}
        Dim queries() As String = {
            "SELECT COUNT(*) FROM computers WHERE Status='In Use'",
            "SELECT IFNULL(SUM(TotalAmount),0) FROM sales WHERE DATE(StartTime)=CURDATE()",
            "SELECT COUNT(*) FROM sales WHERE PaymentStatus='Unpaid'",
            "SELECT COUNT(*) FROM replacements WHERE Status='Pending'",
            "SELECT COUNT(*) FROM billing WHERE Status='Pending'"
        }
        Dim colors() As Color = {
            Color.FromArgb(52, 152, 219),
            Color.FromArgb(46, 204, 113),
            Color.FromArgb(231, 76, 60),
            Color.FromArgb(241, 196, 15),
            Color.FromArgb(155, 89, 182)
        }

        For i As Integer = 0 To cardTitles.Length - 1
            Dim count As String = GetScalarValue(queries(i))
            Dim card As Panel = CreateCard(cardTitles(i), count, colors(i))
            flowCards.Controls.Add(card)
        Next
    End Sub

    Private Function CreateCard(title As String, value As String, color As Color) As Panel
        Dim pnl As New Panel()
        pnl.Size = New Size(210, 120)
        pnl.Margin = New Padding(10)
        pnl.BackColor = color
        pnl.Padding = New Padding(10)
        pnl.BorderStyle = BorderStyle.None
        pnl.Cursor = Cursors.Hand

        Dim lblTitle As New Label()
        lblTitle.Text = title
        lblTitle.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.AutoSize = True
        lblTitle.Location = New Point(10, 10)

        Dim lblValue As New Label()
        lblValue.Text = value
        lblValue.Font = New Font("Segoe UI Semibold", 22, FontStyle.Bold)
        lblValue.ForeColor = Color.White
        lblValue.AutoSize = True
        lblValue.Location = New Point(10, 50)

        pnl.Controls.Add(lblTitle)
        pnl.Controls.Add(lblValue)
        Return pnl
    End Function

    Private Function GetScalarValue(query As String) As String
        Try
            Dim result As Object
            OpenConnection()
            Using cmd As New MySqlCommand(query, conn)
                result = cmd.ExecuteScalar()
            End Using
            If result IsNot Nothing Then
                Return result.ToString()
            Else
                Return "0"
            End If
        Catch ex As Exception
            Return "0"
        Finally
            CloseConnection()
        End Try
    End Function

    ' -----------------------------
    ' RECENT SALES SECTION
    ' -----------------------------
    Private Sub LoadRecentSales()
        Try
            OpenConnection()
            Dim query As String =
                "SELECT s.SaleID, c.ComputerName, u.Username, s.TotalAmount, s.PaymentStatus " &
                "FROM sales s " &
                "INNER JOIN computers c ON s.ComputerID=c.ComputerID " &
                "INNER JOIN users u ON s.UserID=u.UserID " &
                "ORDER BY s.StartTime DESC LIMIT 5"

            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvRecentSales.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Error loading recent sales: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    ' -----------------------------
    ' PENDING REQUESTS SECTION
    ' -----------------------------
    Private Sub LoadPendingRequests()
        Try
            OpenConnection()
            Dim query As String =
                "SELECT 'Billing' AS RequestType, BillID AS RequestID, Amount AS Amount, Status " &
                "FROM billing WHERE Status='Pending' " &
                "UNION ALL " &
                "SELECT 'Replacement', ReplacementID, Cost, Status FROM replacements WHERE Status='Pending'"

            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvRequests.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Error loading pending requests: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub


End Class

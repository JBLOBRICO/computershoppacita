Imports MySql.Data.MySqlClient

Public Class frmBillingRequest

    Private Sub frmBillingRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRequestsData()
        CheckApprovalStatus()
    End Sub

    '============================
    ' OPEN ADD BILLING REQUEST FORM
    '============================
    Private Sub btnRequestBill_Click(sender As Object, e As EventArgs) Handles btnRequestBill.Click
        Dim addForm As New frmAddBillingRequest()
        If addForm.ShowDialog() = DialogResult.OK Then
            LoadRequestsData()
        End If
    End Sub

    '============================
    ' LOAD ALL REQUESTS (BILLING + REPLACEMENT)
    '============================
    Private Sub LoadRequestsData()
        Try
            OpenConnection()

            Dim query As String = "
                SELECT 
                    b.BillID AS RequestID, 
                    'Billing' AS RequestType,
                    s.SaleID AS RelatedID,
                    b.BillDate AS RequestDate, 
                    b.Amount AS Cost,
                    b.Status,
                    u.FullName AS ApprovedBy
                FROM billing b
                LEFT JOIN users u ON b.ApprovedBy = u.UserID
                LEFT JOIN sales s ON b.SaleID = s.SaleID

                UNION ALL

                SELECT 
                    r.ReplacementID AS RequestID,
                    'Replacement' AS RequestType,
                    r.ComputerID AS RelatedID,
                    r.RequestDate AS RequestDate,
                    r.Cost AS Cost,
                    r.Status,
                    u.FullName AS ApprovedBy
                FROM replacements r
                LEFT JOIN users u ON r.ApprovedBy = u.UserID
            "

            Dim dt As New DataTable()
            Using cmd As New MySqlCommand(query, conn)
                Dim da As New MySqlDataAdapter(cmd)
                da.Fill(dt)
            End Using

            dgvBilling.DataSource = dt

            ' Update summary labels safely
            Dim totalAmount As Decimal = 0
            If dt.Rows.Count > 0 Then
                totalAmount = If(IsDBNull(dt.Compute("SUM(Cost)", "")), 0, Convert.ToDecimal(dt.Compute("SUM(Cost)", "")))
            End If

            lblTotalBills.Text = $"Total Requests: {dt.Rows.Count} 🧾"
            lblTotalAmount.Text = $"💵 Total Amount: ₱{totalAmount}"
            lblPending.Text = $"🕐 Pending Requests: {dt.Select("Status='Pending'").Length}"

        Catch ex As Exception
            MessageBox.Show("Error loading requests data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

    '============================
    ' CHECK APPROVAL STATUS
    '============================
    Private Sub CheckApprovalStatus()
        Try
            OpenConnection()

            Dim query As String = "
                SELECT a.ApprovalID, a.RequestID, a.RequestType, a.Remarks, a.ApprovalStatus
                FROM approvals a
                WHERE a.ApprovedBy IS NOT NULL
                  AND a.IsSeen = 0
            "

            Dim dt As New DataTable()
            Using cmd As New MySqlCommand(query, conn)
                Dim da As New MySqlDataAdapter(cmd)
                da.Fill(dt)
            End Using

            For Each row As DataRow In dt.Rows
                Dim approvalID As Integer = row("ApprovalID")
                Dim requestID As Integer = row("RequestID")
                Dim requestType As String = row("RequestType")
                Dim remarks As String = If(IsDBNull(row("Remarks")), "", row("Remarks"))
                Dim approvalStatus As String = row("ApprovalStatus").ToString()

                ' Update table status accordingly
                Dim updateQuery As String = ""
                If requestType = "Billing" Then
                    updateQuery = "UPDATE billing SET Status=@Status, ApprovedBy=@ApprovedBy WHERE BillID=@ID"
                ElseIf requestType = "Replacement" Then
                    updateQuery = "UPDATE replacements SET Status=@Status, ApprovedBy=@ApprovedBy WHERE ReplacementID=@ID"
                End If

                Using cmdUpdate As New MySqlCommand(updateQuery, conn)
                    cmdUpdate.Parameters.AddWithValue("@Status", If(approvalStatus = "Approved", "Paid", "Rejected"))
                    cmdUpdate.Parameters.AddWithValue("@ApprovedBy", LoggedInUserID)
                    cmdUpdate.Parameters.AddWithValue("@ID", requestID)
                    cmdUpdate.ExecuteNonQuery()
                End Using

                ' Show message only once
                MessageBox.Show($"Your {requestType} request (ID {requestID}) has been {approvalStatus}." & vbCrLf &
                                $"Remarks: {remarks}", "Request Update", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Mark as seen
                Using cmdSeen As New MySqlCommand("UPDATE approvals SET IsSeen=1 WHERE ApprovalID=@ApprovalID", conn)
                    cmdSeen.Parameters.AddWithValue("@ApprovalID", approvalID)
                    cmdSeen.ExecuteNonQuery()
                End Using
            Next

        Catch ex As Exception
            MessageBox.Show("Error checking approvals: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CloseConnection()
        End Try
    End Sub

End Class

Imports MySql.Data.MySqlClient

Public Class frmBillingApproval

    Private Sub frmBillingApproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPendingRequests()
    End Sub

    '====================================================
    ' LOAD ALL PENDING REQUESTS FROM BILLING + REPLACEMENT
    '====================================================
    Private Sub LoadPendingRequests()
        Try
            Dim query As String = "
                SELECT 
                    'Billing' AS RequestType, 
                    b.BillID AS RequestID, 
                    b.BillDate AS RequestDate, 
                    b.Amount, 
                    b.Status 
                FROM billing b 
                WHERE b.Status = 'Pending'
                UNION ALL
                SELECT 
                    'Replacement' AS RequestType, 
                    r.ReplacementID AS RequestID, 
                    r.RequestDate, 
                    r.Cost AS Amount, 
                    r.Status 
                FROM replacements r 
                WHERE r.Status = 'Pending'
                ORDER BY RequestDate DESC;
            "

            Dim dt As New DataTable
            Using da As New MySqlDataAdapter(query, conn)
                da.Fill(dt)
            End Using

            dgvBillingRequests.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Error loading requests: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '====================================================
    ' SEARCH FILTER
    '====================================================
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim dt As DataTable = CType(dgvBillingRequests.DataSource, DataTable)
        If dt IsNot Nothing Then
            Dim dv As New DataView(dt)
            dv.RowFilter = $"RequestType LIKE '%{txtSearch.Text}%' OR Convert(RequestID, 'System.String') LIKE '%{txtSearch.Text}%'"
            dgvBillingRequests.DataSource = dv
        End If
    End Sub

    '====================================================
    ' APPROVE BUTTON
    '====================================================
    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        If dgvBillingRequests.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a request to approve.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim requestType As String = dgvBillingRequests.SelectedRows(0).Cells("RequestType").Value.ToString()
        Dim requestID As Integer = CInt(dgvBillingRequests.SelectedRows(0).Cells("RequestID").Value)

        Try
            Dim updateQuery As String = ""
            If requestType = "Billing" Then
                updateQuery = "UPDATE billing SET Status='Paid', ApprovedBy=@ApprovedBy WHERE BillID=@ID"
            ElseIf requestType = "Replacement" Then
                updateQuery = "UPDATE replacements SET Status='Approved', ApprovedBy=@ApprovedBy WHERE ReplacementID=@ID"
            End If

            Using cmd As New MySqlCommand(updateQuery, conn)
                cmd.Parameters.AddWithValue("@ApprovedBy", LoggedInUserID)
                cmd.Parameters.AddWithValue("@ID", requestID)
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            End Using

            ' Insert approval record
            InsertApprovalLog(requestType, requestID, "Approved")

            MessageBox.Show($"{requestType} request approved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadPendingRequests()
        Catch ex As Exception
            conn.Close()
            MessageBox.Show("Error approving request: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '====================================================
    ' REJECT BUTTON
    '====================================================
    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        If dgvBillingRequests.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a request to reject.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim requestType As String = dgvBillingRequests.SelectedRows(0).Cells("RequestType").Value.ToString()
        Dim requestID As Integer = CInt(dgvBillingRequests.SelectedRows(0).Cells("RequestID").Value)

        Try
            Dim updateQuery As String = ""
            If requestType = "Billing" Then
                updateQuery = "UPDATE billing SET Status='Rejected', ApprovedBy=@ApprovedBy WHERE BillID=@ID"
            ElseIf requestType = "Replacement" Then
                updateQuery = "UPDATE replacements SET Status='Rejected', ApprovedBy=@ApprovedBy WHERE ReplacementID=@ID"
            End If

            Using cmd As New MySqlCommand(updateQuery, conn)
                cmd.Parameters.AddWithValue("@ApprovedBy", LoggedInUserID) ' <-- fix here
                cmd.Parameters.AddWithValue("@ID", requestID)
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            End Using

            ' Log rejection in approvals table
            InsertApprovalLog(requestType, requestID, "Rejected")

            MessageBox.Show($"{requestType} request has been rejected.", "Rejected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadPendingRequests()
        Catch ex As Exception
            conn.Close()
            MessageBox.Show("Error rejecting request: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '====================================================
    ' INSERT INTO APPROVAL LOG
    '====================================================
    Private Sub InsertApprovalLog(requestType As String, requestID As Integer, status As String)
        Try
            Dim query As String = "
                INSERT INTO approvals (RequestType, RequestID, ApprovedBy, Remarks, ApprovalStatus, IsSeen)
                VALUES (@Type, @ReqID, @ApprovedBy, @Remarks, @ApprovalStatus, 0)
            "

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Type", requestType)
                cmd.Parameters.AddWithValue("@ReqID", requestID)
                cmd.Parameters.AddWithValue("@ApprovedBy", LoggedInUserID)
                cmd.Parameters.AddWithValue("@Remarks", status)
                cmd.Parameters.AddWithValue("@ApprovalStatus", status)
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            End Using
        Catch ex As Exception
            conn.Close()
            MessageBox.Show("Error inserting approval log: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvBillingRequests_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBillingRequests.CellContentClick
        ' Reserved for future features
    End Sub

End Class

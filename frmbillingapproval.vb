Imports MySql.Data.MySqlClient

Public Class frmBillingApproval

    '==============================
    ' FORM LOAD
    '==============================
    Private Sub frmBillingApproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPendingRequests()
    End Sub

    '==============================
    ' LOAD PENDING BILLING & REPLACEMENTS
    '==============================
    Private Sub LoadPendingRequests()
        Try
            Dim query As String = "
                SELECT 
                    'Billing' AS RequestType, 
                    b.BillID AS RequestID, 
                    b.BillDate AS RequestDate, 
                    b.Amount, 
                    b.ApprovedBy,
                    '' AS Description,
                    b.Status 
                FROM billing b 
                WHERE b.Status = 'Pending'
                UNION ALL
                SELECT 
                    'Replacement' AS RequestType, 
                    r.ReplacementID AS RequestID, 
                    r.RequestDate, 
                    r.Cost AS Amount, 
                    r.ApprovedBy,
                    r.Description,
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
            dgvBillingRequests.AutoResizeColumns()
        Catch ex As Exception
            MessageBox.Show("Error loading requests: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '==============================
    ' SEARCH FILTER
    '==============================
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim dt As DataTable = TryCast(dgvBillingRequests.DataSource, DataTable)
        If dt IsNot Nothing Then
            Dim dv As New DataView(dt)
            dv.RowFilter = $"RequestType LIKE '%{txtSearch.Text}%' OR Convert(RequestID, 'System.String') LIKE '%{txtSearch.Text}%' OR Description LIKE '%{txtSearch.Text}%'"
            dgvBillingRequests.DataSource = dv
        End If
    End Sub

    '==============================
    ' APPROVE REQUEST
    '==============================
    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        ' Validation: check if a row is selected
        If dgvBillingRequests.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a request to approve.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim row = dgvBillingRequests.SelectedRows(0)
        Dim status As String = row.Cells("Status").Value.ToString()
        If status = "Paid" Or status = "Approved" Then
            MessageBox.Show("This request is already approved.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim requestType As String = row.Cells("RequestType").Value.ToString()
        Dim requestID As Integer = CInt(row.Cells("RequestID").Value)
        Dim description As String = row.Cells("Description").Value.ToString()

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

            InsertApprovalLog(requestType, requestID, "Approved", description)

            MessageBox.Show($"{requestType} request approved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadPendingRequests()
        Catch ex As Exception
            conn.Close()
            MessageBox.Show("Error approving request: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '==============================
    ' REJECT REQUEST
    '==============================
    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        ' Validation: check if a row is selected
        If dgvBillingRequests.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a request to reject.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim row = dgvBillingRequests.SelectedRows(0)
        Dim status As String = row.Cells("Status").Value.ToString()
        If status = "Rejected" Then
            MessageBox.Show("This request is already rejected.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        ElseIf status = "Paid" Or status = "Approved" Then
            MessageBox.Show("This request is already approved, cannot reject.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim requestType As String = row.Cells("RequestType").Value.ToString()
        Dim requestID As Integer = CInt(row.Cells("RequestID").Value)
        Dim description As String = row.Cells("Description").Value.ToString()

        Try
            Dim updateQuery As String = ""
            If requestType = "Billing" Then
                updateQuery = "UPDATE billing SET Status='Rejected', ApprovedBy=@ApprovedBy WHERE BillID=@ID"
            ElseIf requestType = "Replacement" Then
                updateQuery = "UPDATE replacements SET Status='Rejected', ApprovedBy=@ApprovedBy WHERE ReplacementID=@ID"
            End If

            Using cmd As New MySqlCommand(updateQuery, conn)
                cmd.Parameters.AddWithValue("@ApprovedBy", LoggedInUserID)
                cmd.Parameters.AddWithValue("@ID", requestID)
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            End Using

            InsertApprovalLog(requestType, requestID, "Rejected", description)

            MessageBox.Show($"{requestType} request has been rejected.", "Rejected", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadPendingRequests()
        Catch ex As Exception
            conn.Close()
            MessageBox.Show("Error rejecting request: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '==============================
    ' INSERT INTO APPROVAL LOG
    '==============================
    Private Sub InsertApprovalLog(requestType As String, requestID As Integer, status As String, description As String)
        Try
            Dim query As String = "
                INSERT INTO approvals (RequestType, RequestID, ApprovedBy, Remarks, ApprovalDate)
                VALUES (@Type, @ReqID, @ApprovedBy, @Remarks, NOW())
            "
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Type", requestType)
                cmd.Parameters.AddWithValue("@ReqID", requestID)
                cmd.Parameters.AddWithValue("@ApprovedBy", LoggedInUserID)
                cmd.Parameters.AddWithValue("@Remarks", status & If(String.IsNullOrEmpty(description), "", " - " & description))
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            End Using
        Catch ex As Exception
            conn.Close()
            MessageBox.Show("Error inserting approval log: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class

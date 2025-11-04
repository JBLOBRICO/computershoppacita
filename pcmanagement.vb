Imports MySql.Data.MySqlClient

Public Class Pcmanagement

    ' Form Load
    Private Sub Pcmanagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbPCStatus.SelectedIndex = 0
        LoadComputers()
        txtPCID.Enabled = False ' ID is auto-increment
    End Sub

    ' Load computers into DataGridView
    Private Sub LoadComputers()
        Try
            Dbconnection.OpenConnection()
            Dim query As String = "SELECT * FROM computers"
            Dbconnection.da = New MySqlDataAdapter(query, Dbconnection.conn)
            Dbconnection.dt = New DataTable()
            Dbconnection.da.Fill(Dbconnection.dt)
            dgvPC.DataSource = Dbconnection.dt
        Catch ex As Exception
            MessageBox.Show("Error loading computers: " & ex.Message)
        Finally
            Dbconnection.CloseConnection()
        End Try
    End Sub

    ' Clear input fields
    Private Sub ClearFields()
        txtPCID.Clear()
        txtPCName.Clear()
        cmbPCStatus.SelectedIndex = 0
    End Sub

    ' Add button
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ' Validate input
        If txtPCName.Text.Trim() = "" Or cmbPCStatus.SelectedIndex = -1 Then
            MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Confirm add
        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to add this computer?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.No Then Return

        Try
            Dbconnection.OpenConnection()

            ' Check for duplicate name
            Dim checkQuery As String = "SELECT COUNT(*) FROM computers WHERE ComputerName=@name"
            Dbconnection.cmd = New MySqlCommand(checkQuery, Dbconnection.conn)
            Dbconnection.cmd.Parameters.AddWithValue("@name", txtPCName.Text)
            Dim count As Integer = Convert.ToInt32(Dbconnection.cmd.ExecuteScalar())
            If count > 0 Then
                MessageBox.Show("A computer with this name already exists.", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Insert new record
            Dim query As String = "INSERT INTO computers (ComputerName, Status) VALUES (@name, @status)"
            Dbconnection.cmd = New MySqlCommand(query, Dbconnection.conn)
            Dbconnection.cmd.Parameters.AddWithValue("@name", txtPCName.Text)
            Dbconnection.cmd.Parameters.AddWithValue("@status", cmbPCStatus.SelectedItem.ToString())
            Dbconnection.cmd.ExecuteNonQuery()

            MessageBox.Show("Computer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error adding computer: " & ex.Message)
        Finally
            Dbconnection.CloseConnection()
            LoadComputers()
            ClearFields()
        End Try
    End Sub

    ' Update button
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        ' Validate
        If txtPCID.Text = "" Then
            MessageBox.Show("Select a computer to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If txtPCName.Text.Trim() = "" Or cmbPCStatus.SelectedIndex = -1 Then
            MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Confirm update
        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to update this computer?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.No Then Return

        Try
            Dbconnection.OpenConnection()
            Dim query As String = "UPDATE computers SET ComputerName=@name, Status=@status WHERE ComputerID=@id"
            Dbconnection.cmd = New MySqlCommand(query, Dbconnection.conn)
            Dbconnection.cmd.Parameters.AddWithValue("@id", txtPCID.Text)
            Dbconnection.cmd.Parameters.AddWithValue("@name", txtPCName.Text)
            Dbconnection.cmd.Parameters.AddWithValue("@status", cmbPCStatus.SelectedItem.ToString())
            Dbconnection.cmd.ExecuteNonQuery()

            MessageBox.Show("Computer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error updating computer: " & ex.Message)
        Finally
            Dbconnection.CloseConnection()
            LoadComputers()
            ClearFields()
        End Try
    End Sub

    ' Delete button
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        ' Validate
        If txtPCID.Text = "" Then
            MessageBox.Show("Select a computer to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Confirm delete
        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to delete this computer?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.No Then Return

        Try
            Dbconnection.OpenConnection()
            Dim query As String = "DELETE FROM computers WHERE ComputerID=@id"
            Dbconnection.cmd = New MySqlCommand(query, Dbconnection.conn)
            Dbconnection.cmd.Parameters.AddWithValue("@id", txtPCID.Text)
            Dbconnection.cmd.ExecuteNonQuery()
            MessageBox.Show("Computer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error deleting computer: " & ex.Message)
        Finally
            Dbconnection.CloseConnection()
            LoadComputers()
            ClearFields()
        End Try
    End Sub

    ' Clear button
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    ' Fill fields when selecting a row
    Private Sub dgvPC_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPC.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvPC.Rows(e.RowIndex)
            txtPCID.Text = row.Cells("ComputerID").Value.ToString()
            txtPCName.Text = row.Cells("ComputerName").Value.ToString()
            cmbPCStatus.SelectedItem = row.Cells("Status").Value.ToString()
        End If
    End Sub

    Private Sub dgvPC_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPC.CellContentClick
    End Sub

    Private Sub pnlInputs_Paint(sender As Object, e As PaintEventArgs) Handles pnlInputs.Paint
    End Sub

End Class

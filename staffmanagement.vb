Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class StaffManagement

    ' ✅ Helper function to fetch data into DataTable
    Private Function GetDataTable(query As String) As DataTable
        Dim dt As New DataTable
        Try
            Dbconnection.OpenConnection()
            Dbconnection.cmd = New MySqlCommand(query, Dbconnection.conn)
            Dbconnection.da = New MySqlDataAdapter(Dbconnection.cmd)
            Dbconnection.da.Fill(dt)
        Catch ex As Exception
            MessageBox.Show("Error fetching data: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Dbconnection.CloseConnection()
        End Try
        Return dt
    End Function

    ' ✅ Load staff data into DataGridView
    Private Sub LoadStaffData()
        dgvStaff.DataSource = GetDataTable("SELECT UserID, Username, FullName, Role, DateCreated FROM users WHERE Role='Staff'")
        dgvStaff.ClearSelection()
        ClearInputs()
    End Sub

    ' ✅ Clear input fields
    Private Sub ClearInputs()
        txtID.Clear()
        txtUsername.Clear()
        txtFullName.Clear()
        cmbRole.SelectedIndex = -1
        txtPassword.Clear()
    End Sub

    ' ✅ Form Load event
    Private Sub StaffManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStaffData()
        cmbRole.Items.Clear()
        cmbRole.Items.Add("Staff")
        cmbRole.Items.Add("Admin")
    End Sub

    ' ✅ Add new staff
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ' Basic validation
        If txtUsername.Text = "" Or txtFullName.Text = "" Or cmbRole.SelectedIndex = -1 Or txtPassword.Text = "" Then
            MessageBox.Show("Please fill all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Confirm before adding
        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to add this staff?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.No Then Return

        Try
            Dbconnection.OpenConnection()

            ' Check for duplicate username
            Dbconnection.cmd = New MySqlCommand("SELECT COUNT(*) FROM users WHERE Username=@username", Dbconnection.conn)
            Dbconnection.cmd.Parameters.AddWithValue("@username", txtUsername.Text)
            Dim count As Integer = Convert.ToInt32(Dbconnection.cmd.ExecuteScalar())

            If count > 0 Then
                MessageBox.Show("Username already exists! Please choose a different username.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Insert new user
            Dbconnection.cmd = New MySqlCommand(
                "INSERT INTO users (Username, FullName, Role, Password) VALUES (@username, @fullname, @role, @password)",
                Dbconnection.conn)
            Dbconnection.cmd.Parameters.AddWithValue("@username", txtUsername.Text)
            Dbconnection.cmd.Parameters.AddWithValue("@fullname", txtFullName.Text)
            Dbconnection.cmd.Parameters.AddWithValue("@role", cmbRole.SelectedItem.ToString())
            Dbconnection.cmd.Parameters.AddWithValue("@password", txtPassword.Text)
            Dbconnection.cmd.ExecuteNonQuery()

            MessageBox.Show("User added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadStaffData()
            ClearInputs()

        Catch ex As Exception
            MessageBox.Show("Error adding user: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Dbconnection.CloseConnection()
        End Try
    End Sub

    ' ✅ Update selected staff
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If txtID.Text = "" Then
            MessageBox.Show("Select a staff to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If txtUsername.Text = "" Or txtFullName.Text = "" Or cmbRole.SelectedIndex = -1 Then
            MessageBox.Show("Please fill all fields before updating.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Confirm before updating
        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to update this staff?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.No Then Return

        Try
            Dbconnection.OpenConnection()
            Dbconnection.cmd = New MySqlCommand(
                "UPDATE users SET Username=@username, FullName=@fullname, Role=@role, Password=@password WHERE UserID=@id", Dbconnection.conn)
            Dbconnection.cmd.Parameters.AddWithValue("@username", txtUsername.Text)
            Dbconnection.cmd.Parameters.AddWithValue("@fullname", txtFullName.Text)
            Dbconnection.cmd.Parameters.AddWithValue("@role", cmbRole.SelectedItem.ToString())
            Dbconnection.cmd.Parameters.AddWithValue("@password", txtPassword.Text)
            Dbconnection.cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text))
            Dbconnection.cmd.ExecuteNonQuery()

            MessageBox.Show("Staff updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadStaffData()
            ClearInputs()

        Catch ex As Exception
            MessageBox.Show("Error updating staff: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Dbconnection.CloseConnection()
        End Try
    End Sub

    ' ✅ Delete selected staff
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If txtID.Text = "" Then
            MessageBox.Show("Select a staff to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Confirm before deleting
        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to delete this staff?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.No Then Return

        Try
            Dbconnection.OpenConnection()
            Dbconnection.cmd = New MySqlCommand("DELETE FROM users WHERE UserID=@id", Dbconnection.conn)
            Dbconnection.cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text))
            Dbconnection.cmd.ExecuteNonQuery()

            MessageBox.Show("Staff deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadStaffData()
            ClearInputs()

        Catch ex As Exception
            MessageBox.Show("Error deleting staff: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Dbconnection.CloseConnection()
        End Try
    End Sub

    ' ✅ Clear button
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearInputs()
    End Sub

    ' ✅ Handle DataGridView row click
    Private Sub dgvStaff_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStaff.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvStaff.Rows(e.RowIndex)
            txtID.Text = row.Cells("UserID").Value.ToString()
            txtUsername.Text = row.Cells("Username").Value.ToString()
            txtFullName.Text = row.Cells("FullName").Value.ToString()
            cmbRole.SelectedItem = row.Cells("Role").Value.ToString()
            txtPassword.Clear() ' do not show passwords
        End If
    End Sub

    Private Sub pnlInputs_Paint(sender As Object, e As PaintEventArgs) Handles pnlInputs.Paint
        ' Optional UI paint event
    End Sub

    Private Sub dgvStaff_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStaff.CellContentClick
        ' Optional event
    End Sub

End Class

Imports System.Data.OleDb

Public Class FormBarang

    Public conn As OleDbConnection
    Public da As OleDbDataAdapter
    Public ds As DataSet
    Public cmd As OleDbCommand
    Public dr As OleDbDataReader
    Public str As String

    Public Sub koneksi()
        str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb"
        conn = New OleDbConnection(str)
        If conn.State = ConnectionState.Closed Then conn.Open()
    End Sub

    Private Sub FormBarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        koneksi()
        da = New OleDbDataAdapter("SELECT * FROM Barang", conn)
        ds = New DataSet()
        da.Fill(ds, "Barang")

        DataGridView1.DataSource = ds.Tables("Barang")
        conn.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MessageBox.Show("Semua field harus diisi!")
            Exit Sub
        End If

        ' Koneksi ke database
        koneksi()

        ' Mulai transaksi
        Dim transaction As OleDbTransaction = conn.BeginTransaction()

        ' Periksa apakah ID_Barang sudah ada
        Dim queryCheck As String = "SELECT COUNT(*) FROM Barang WHERE ID_Barang = ?"
        cmd = New OleDbCommand(queryCheck, conn)
        cmd.Transaction = transaction
        cmd.Parameters.AddWithValue("?", TextBox1.Text)

        Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

        Try
            If count > 0 Then
                ' Jika data sudah ada, lakukan update
                Dim queryUpdate As String = "UPDATE Barang SET Nama_Barang = ?, Tipe_Barang = ?, Harga_Barang = ? WHERE ID_Barang = ?"
                cmd = New OleDbCommand(queryUpdate, conn)
                cmd.Transaction = transaction

                cmd.Parameters.AddWithValue("?", TextBox2.Text)
                cmd.Parameters.AddWithValue("?", ComboBox1.Text)
                cmd.Parameters.AddWithValue("?", Convert.ToDecimal(TextBox3.Text))
                cmd.Parameters.AddWithValue("?", TextBox1.Text)

                cmd.ExecuteNonQuery()
                MessageBox.Show("Data berhasil diperbarui!")
            Else
                ' Jika data belum ada, lakukan insert
                Dim queryInsert As String = "INSERT INTO Barang (ID_Barang, Nama_Barang, Tipe_Barang, Harga_Barang) VALUES (?, ?, ?, ?)"
                cmd = New OleDbCommand(queryInsert, conn)
                cmd.Transaction = transaction

                cmd.Parameters.AddWithValue("?", TextBox1.Text)
                cmd.Parameters.AddWithValue("?", TextBox2.Text)
                cmd.Parameters.AddWithValue("?", ComboBox1.Text)
                cmd.Parameters.AddWithValue("?", Convert.ToDecimal(TextBox3.Text))

                cmd.ExecuteNonQuery()
                MessageBox.Show("Data berhasil disimpan!")
            End If

            ' Commit transaksi setelah berhasil
            transaction.Commit()

            ' Refresh DataGridView
            da = New OleDbDataAdapter("SELECT * FROM Barang", conn)
            ds = New DataSet()
            da.Fill(ds, "Barang")
            DataGridView1.DataSource = ds.Tables("Barang")
        Catch ex As Exception
            ' Jika ada error, rollback transaksi
            transaction.Rollback()
            MessageBox.Show("Error: " & ex.Message)
        Finally
            ' Tutup koneksi
            conn.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()

    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not Char.IsControl(e.KeyChar) And Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ' Isi TextBox dari kolom
            TextBox1.Text = row.Cells(0).Value.ToString()
            TextBox2.Text = row.Cells(1).Value.ToString()
            ComboBox1.Text = row.Cells(2).Value.ToString()
            TextBox3.Text = row.Cells(3).Value
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If (e.RowIndex >= 0) Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            TextBox1.Text = row.Cells(0).Value.ToString()
            TextBox2.Text = row.Cells(1).Value.ToString()
            ComboBox1.Text = row.Cells(2).Value.ToString()
            TextBox3.Text = row.Cells(3).Value
        End If
    End Sub
End Class
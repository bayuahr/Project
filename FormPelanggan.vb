Imports System.Data.OleDb

Public Class FormPelanggan

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
    Private Sub FormPelanggan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        RichTextBox1.Clear()
        koneksi()
        da = New OleDbDataAdapter("SELECT * FROM Pelanggan", conn)
        ds = New DataSet()
        da.Fill(ds, "Pelanggan")

        DataGridView1.DataSource = ds.Tables("Pelanggan")
        conn.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        RichTextBox1.Clear()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or RichTextBox1.Text = "" Then
            MessageBox.Show("Semua field harus diisi!")
            Exit Sub
        End If

        ' Koneksi ke database
        koneksi()

        ' Mulai transaksi
        Dim transaction As OleDbTransaction = conn.BeginTransaction()

        ' Periksa apakah ID_Barang sudah ada
        Dim queryCheck As String = "SELECT COUNT(*) FROM Pelanggan WHERE ID_Pelanggan = ?"
        cmd = New OleDbCommand(queryCheck, conn)
        cmd.Transaction = transaction
        cmd.Parameters.AddWithValue("?", TextBox1.Text)

        Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

        Try
            If count > 0 Then
                ' Jika data sudah ada, lakukan update
                Dim queryUpdate As String = "UPDATE Pelanggan SET Nama = ?, Alamat = ?, No_HP = ? WHERE ID_Pelanggan = ?"
                cmd = New OleDbCommand(queryUpdate, conn)
                cmd.Transaction = transaction

                cmd.Parameters.AddWithValue("?", TextBox2.Text)
                cmd.Parameters.AddWithValue("?", RichTextBox1.Text)
                cmd.Parameters.AddWithValue("?", (TextBox3.Text))
                cmd.Parameters.AddWithValue("?", TextBox1.Text)

                cmd.ExecuteNonQuery()
                MessageBox.Show("Data berhasil diperbarui!")
            Else
                ' Jika data belum ada, lakukan insert
                Dim queryInsert As String = "INSERT INTO Pelanggan (ID_Pelanggan, Nama, Alamat, No_HP) VALUES (?, ?, ?, ?)"
                cmd = New OleDbCommand(queryInsert, conn)
                cmd.Transaction = transaction

                cmd.Parameters.AddWithValue("?", TextBox1.Text)
                cmd.Parameters.AddWithValue("?", TextBox2.Text)
                cmd.Parameters.AddWithValue("?", RichTextBox1.Text)
                cmd.Parameters.AddWithValue("?", (TextBox3.Text))

                cmd.ExecuteNonQuery()
                MessageBox.Show("Data berhasil disimpan!")
            End If

            ' Commit transaksi setelah berhasil
            transaction.Commit()

            ' Refresh DataGridView
            da = New OleDbDataAdapter("SELECT * FROM Pelanggan", conn)
            ds = New DataSet()
            da.Fill(ds, "Pelanggan")
            DataGridView1.DataSource = ds.Tables("Pelanggan")
        Catch ex As Exception
            ' Jika ada error, rollback transaksi
            transaction.Rollback()
            MessageBox.Show("Error: " & ex.Message)
        Finally
            ' Tutup koneksi
            conn.Close()
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ' Isi TextBox dari kolom
            TextBox1.Text = row.Cells(0).Value.ToString()
            TextBox2.Text = row.Cells(1).Value.ToString()
            RichTextBox1.Text = row.Cells(2).Value.ToString()
            TextBox3.Text = row.Cells(3).Value
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If (e.RowIndex >= 0) Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            TextBox1.Text = row.Cells(0).Value.ToString()
            TextBox2.Text = row.Cells(1).Value.ToString()
            RichTextBox1.Text = row.Cells(2).Value.ToString()
            TextBox3.Text = row.Cells(3).Value
        End If
    End Sub
End Class
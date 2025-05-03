Imports System.Data.OleDb

Public Class FormPenjualan

    Public conn As OleDbConnection
    Public da As OleDbDataAdapter
    Public cmd As OleDbCommand
    Public ds As DataSet
    Public str As String

    Public Sub koneksi()
        str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb"
        conn = New OleDbConnection(str)
        If conn.State = ConnectionState.Closed Then conn.Open()
    End Sub

    Private Sub FormPenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        TextBox1.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        NumericUpDown1.Value = 1
        DateTimePicker1.Value = DateTime.Now

        Dim dt As New DataTable()
        Dim dt2 As New DataTable()
        da = New OleDbDataAdapter("SELECT * FROM Pelanggan", conn)
        da.Fill(dt)

        ' Binding ke ComboBox
        ComboBox1.DataSource = dt
        ComboBox1.DisplayMember = "Nama"
        ComboBox1.ValueMember = "ID_Pelanggan"

        da = New OleDbDataAdapter("SELECT * FROM Barang", conn)
        da.Fill(dt2)

        ' Binding ke ComboBox
        ComboBox2.DataSource = dt2
        ComboBox2.DisplayMember = "Nama_Barang"
        ComboBox2.ValueMember = "ID_Barang"

        Try
            da = New OleDbDataAdapter("SELECT 
                Penjualan.ID_Transaksi, 
                Penjualan.TGL_Penjualan, 
                Pelanggan.Nama, 
                Barang.Nama_Barang, 
                Barang.Harga_Barang, 
                Penjualan.Jumlah_Pembelian, 
                Penjualan.Pajak, 
                Penjualan.Total_Pembelian, 
                Penjualan.Tunai, 
                Penjualan.Kembalian,
                Penjualan.ID_Pelanggan,
                Penjualan.ID_Barang
            FROM 
                (Penjualan 
                INNER JOIN Barang ON Penjualan.ID_Barang = Barang.ID_Barang)
                INNER JOIN Pelanggan ON Pelanggan.ID_Pelanggan = Penjualan.ID_Pelanggan", conn)
            ds = New DataSet()
            da.Fill(ds, "Penjualan")
            DataGridView1.DataSource = ds.Tables("Penjualan")
        Catch ex As Exception
            MsgBox("Query Error: " & ex.Message)
        End Try


        conn.Close()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex >= 0 Then
            Dim idBarang As String = ComboBox2.SelectedValue.ToString()

            koneksi()
            Dim query As String = "SELECT * FROM Barang WHERE ID_Barang = ?"
            Dim cmd As New OleDbCommand(query, conn)
            cmd.Parameters.AddWithValue("?", idBarang)

            Dim dr As OleDbDataReader = cmd.ExecuteReader()
            If dr.Read() Then
                TextBox6.Text = dr("Harga_Barang").ToString()
            End If
            HitungTotal()
            dr.Close()
            conn.Close()
        End If
    End Sub

    Private Sub HitungTotal()
        Dim harga As Decimal
        Dim jumlah As Integer
        Dim pajak As Decimal
        Dim total As Decimal
        Dim tunai As Decimal
        Dim kembalian As Decimal

        ' Ambil harga dan jumlah pembelian
        Decimal.TryParse(TextBox6.Text, harga)
        jumlah = NumericUpDown1.Value

        ' Hitung total dan pajak
        total = harga * jumlah
        pajak = CInt(total * 0.1) ' dibulatkan ke integer
        Dim totalBayar = total + pajak

        ' Tampilkan total dan pajak
        TextBox3.Text = total.ToString()
        TextBox4.Text = pajak.ToString()
        TextBox5.Text = totalBayar.ToString()

        ' Ambil tunai dari TextBox10
        Decimal.TryParse(TextBox7.Text, tunai)
        kembalian = tunai - totalBayar

        ' Tampilkan kembalian
        TextBox8.Text = kembalian.ToString()
    End Sub


    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        HitungTotal()
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        HitungTotal()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        NumericUpDown1.Value = 1
        DateTimePicker1.Value = DateTime.Now
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or TextBox6.Text = "" Or TextBox3.Text = "" Then
            MessageBox.Show("Semua field harus diisi!")
            Exit Sub
        End If

        koneksi()
        Dim transaction As OleDbTransaction = conn.BeginTransaction()

        Try
            ' Cek apakah ID transaksi sudah ada
            Dim queryCheck As String = "SELECT COUNT(*) FROM Penjualan WHERE ID_Transaksi = ?"
            cmd = New OleDbCommand(queryCheck, conn, transaction)
            cmd.Parameters.AddWithValue("?", TextBox1.Text.ToString())
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If count > 0 Then
                ' Jika ada, update
                Dim queryUpdate As String = "UPDATE Penjualan SET 
                TGL_Penjualan = ?, 
                ID_Pelanggan = ?, 
                ID_Barang = ?, 
                Harga_Barang = ?,
                Jumlah_Pembelian = ?, 
                Pajak = ?, 
                Tunai = ?
                WHERE ID_Transaksi = ?"
                cmd = New OleDbCommand(queryUpdate, conn, transaction)
                cmd.Parameters.AddWithValue("?", DateTimePicker1.Value.ToString())
                cmd.Parameters.AddWithValue("?", ComboBox1.SelectedValue.ToString())
                cmd.Parameters.AddWithValue("?", ComboBox2.SelectedValue.ToString())
                cmd.Parameters.AddWithValue("?", Convert.ToInt32(TextBox6.Text)) ' Harga
                cmd.Parameters.AddWithValue("?", NumericUpDown1.Value)
                cmd.Parameters.AddWithValue("?", Convert.ToInt32(TextBox4.Text)) ' Pajak
                cmd.Parameters.AddWithValue("?", Convert.ToInt32(TextBox7.Text)) ' Tunai
                cmd.Parameters.AddWithValue("?", (TextBox1.Text.ToString()))

                cmd.ExecuteNonQuery()
                MessageBox.Show("Data penjualan berhasil diperbarui!")
            Else
                ' Jika belum ada, insert baru
                Dim queryInsert As String = "INSERT INTO Penjualan 
                (ID_Transaksi, TGL_Penjualan, ID_Pelanggan, ID_Barang, Harga_Barang, Jumlah_Pembelian, Pajak, Tunai)
                VALUES (?, ?, ?, ?, ?, ?, ?, ?)"
                cmd = New OleDbCommand(queryInsert, conn, transaction)
                cmd.Parameters.AddWithValue("?", TextBox1.Text.ToString())
                cmd.Parameters.AddWithValue("?", DateTimePicker1.Value.ToString())
                cmd.Parameters.AddWithValue("?", ComboBox1.SelectedValue.ToString())
                cmd.Parameters.AddWithValue("?", ComboBox2.SelectedValue.ToString())
                cmd.Parameters.AddWithValue("?", Convert.ToInt32(TextBox6.Text)) ' Harga
                cmd.Parameters.AddWithValue("?", NumericUpDown1.Value)
                cmd.Parameters.AddWithValue("?", Convert.ToInt32(TextBox4.Text)) ' Pajak
                cmd.Parameters.AddWithValue("?", Convert.ToInt32(TextBox7.Text)) ' Tunai

                cmd.ExecuteNonQuery()
                MessageBox.Show("Data penjualan berhasil disimpan!")
            End If

            transaction.Commit()

            ' Refresh DataGridView
            da = New OleDbDataAdapter("SELECT 
            Penjualan.ID_Transaksi, 
            Penjualan.TGL_Penjualan, 
            Pelanggan.Nama, 
            Barang.Nama_Barang, 
            Barang.Harga_Barang, 
            Penjualan.Jumlah_Pembelian, 
            Penjualan.Pajak, 
            Penjualan.Total_Pembelian, 
            Penjualan.Tunai, 
            Penjualan.Kembalian,
            Penjualan.ID_Pelanggan,
            Penjualan.ID_Barang
            FROM (Penjualan 
            INNER JOIN Barang ON Penjualan.ID_Barang = Barang.ID_Barang)
            INNER JOIN Pelanggan ON Pelanggan.ID_Pelanggan = Penjualan.ID_Pelanggan", conn)
            ds = New DataSet()
            da.Fill(ds, "Penjualan")
            DataGridView1.DataSource = ds.Tables("Penjualan")

        Catch ex As Exception
            transaction.Rollback()
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If (e.RowIndex >= 0) Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            TextBox1.Text = row.Cells(0).Value.ToString()
            Dim cellValue As String = row.Cells(5).Value.ToString()

            ' Check if the value can be parsed to an integer
            Dim parsedValue As Integer
            If Integer.TryParse(cellValue, parsedValue) Then
                NumericUpDown1.Value = parsedValue
            Else
                ' Handle the case where the value is not a valid integer (e.g., set to 0)
                NumericUpDown1.Value = 0
            End If

            TextBox6.Text = Convert.ToInt32(row.Cells(4).Value.ToString())
            TextBox7.Text = Convert.ToInt32(row.Cells(8).Value.ToString())
            TextBox4.Text = Convert.ToInt32(row.Cells(6).Value.ToString())
            DateTimePicker1.Value = row.Cells(1).Value.ToString()
            ComboBox1.SelectedValue = row.Cells(10).Value
            ComboBox2.SelectedValue = row.Cells(11).Value.ToString()
            HitungTotal()
        End If
    End Sub
End Class

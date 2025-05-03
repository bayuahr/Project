Imports System.Data.OleDb

Public Class LaporanPenjualan

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
    Private Sub LaporanPenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dv As New DataView(ds.Tables("Penjualan"))
        Dim tglFilter As String = "#" & DateTimePicker1.Value.ToString("MM/dd/yyyy") & "#"
        dv.RowFilter = "TGL_Penjualan = " & tglFilter & " AND ID_Barang LIKE '%" & ComboBox2.Text & "%' AND ID_Pelanggan LIKE '%" & ComboBox1.Text & "%'"

        DataGridView1.DataSource = dv
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        PrintDialog1.Document = PrintDocument1
        PrintDocument1.DefaultPageSettings.Landscape = True
        If PrintDialog1.ShowDialog() = DialogResult.OK Then
            PrintDocument1.Print()
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim font As New Font("Arial", 10)
        Dim y As Integer = 100
        Dim x As Integer = 50
        Dim rowHeight As Integer = 20
        Dim currentRow As Integer = 0
        ' Header
        For i As Integer = 0 To DataGridView1.Columns.Count - 1
            e.Graphics.DrawString(DataGridView1.Columns(i).HeaderText, font, Brushes.Black, x + (i * 100), y)
        Next
        y += rowHeight

        ' Data Rows
        While currentRow < DataGridView1.Rows.Count
            Dim row As DataGridViewRow = DataGridView1.Rows(currentRow)
            For i As Integer = 0 To DataGridView1.Columns.Count - 1
                Dim value = If(row.Cells(i).Value IsNot Nothing, row.Cells(i).Value.ToString(), "")
                e.Graphics.DrawString(value, font, Brushes.Black, x + (i * 100), y)
            Next
            y += rowHeight
            currentRow += 1

            ' Jika sudah melebihi halaman
            If y > e.MarginBounds.Bottom Then
                e.HasMorePages = True
                Return
            End If
        End While

        currentRow = 0
        e.HasMorePages = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()

    End Sub
End Class
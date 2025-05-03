Imports System.Data.OleDb
Imports System.Drawing.Printing

Public Class LaporanBarang

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
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dv As New DataView(ds.Tables("Barang"))
        dv.RowFilter = "ID_Barang LIKE '%" & TextBox1.Text & "%' AND Nama_Barang LIKE '%" & TextBox2.Text & "%' AND Tipe_Barang LIKE '%" & ComboBox1.Text & "%'"
        DataGridView1.DataSource = dv
    End Sub


    Private Sub LaporanBarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Clear()
        TextBox2.Clear()
        koneksi()
        da = New OleDbDataAdapter("SELECT * FROM Barang", conn)
        ds = New DataSet()
        da.Fill(ds, "Barang")

        DataGridView1.DataSource = ds.Tables("Barang")
        conn.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        PrintDialog1.Document = PrintDocument1
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
        Application.Exit()
    End Sub
End Class
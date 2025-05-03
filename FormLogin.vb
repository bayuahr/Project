Imports System.Data.OleDb

Public Class FormLogin

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
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        koneksi()
        Dim txtUser As String = TextBox1.Text
        Dim txtPass As String = TextBox2.Text

        cmd = New OleDbCommand("SELECT * FROM Users WHERE Username='" & txtUser & "' AND Password='" & txtPass & "'", conn)
        dr = cmd.ExecuteReader
        If dr.Read() Then
            MsgBox("Login Berhasil!", MsgBoxStyle.Information, "Info")
            FormMenu.Show()
            Me.Hide()
        Else
            MsgBox("Login Gagal! Username atau Password Salah", MsgBoxStyle.Critical, "Error")
        End If
        dr.Close()
        conn.Close()


    End Sub
End Class

Public Class FormMenu


    Private Sub FormMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim splash As New SplashScreen()
        splash.Show()
        Application.DoEvents() ' Memaksa UI update

        ' Simulasi loading 2 detik
        Threading.Thread.Sleep(2000)

        splash.Close()

    End Sub

    Private Sub KeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeluarToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub BarangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarangToolStripMenuItem.Click
        FormBarang.Show()
    End Sub

    Private Sub PelangganToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PelangganToolStripMenuItem.Click
        FormPelanggan.Show()
    End Sub

    Private Sub PenjualanToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PenjualanToolStripMenuItem1.Click
        FormPenjualan.Show()
    End Sub

    Private Sub BarangToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles BarangToolStripMenuItem1.Click
        LaporanBarang.Show()
    End Sub

    Private Sub PelangganToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PelangganToolStripMenuItem1.Click
        LaporanPelanggan.Show()

    End Sub

    Private Sub PenjualanToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PenjualanToolStripMenuItem2.Click
        LaporanPenjualan.Show()

    End Sub
End Class
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LaporanBarang
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataSet1 = New System.Data.DataSet()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(592, 516)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 32)
        Me.Button3.TabIndex = 25
        Me.Button3.Text = "Keluar"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 218)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(655, 283)
        Me.DataGridView1.TabIndex = 24
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(200, 180)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(102, 32)
        Me.Button2.TabIndex = 23
        Me.Button2.Text = "Tampilkan"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(119, 180)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 32)
        Me.Button1.TabIndex = 22
        Me.Button1.Text = "Reset"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 143)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 17)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Tipe Barang"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Umum", "Khusus"})
        Me.ComboBox1.Location = New System.Drawing.Point(120, 140)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(269, 24)
        Me.ComboBox1.TabIndex = 18
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(120, 95)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(269, 22)
        Me.TextBox2.TabIndex = 17
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(120, 54)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(269, 22)
        Me.TextBox1.TabIndex = 16
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(284, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(166, 25)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Laporan Barang"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 17)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Nama Barang"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 17)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "ID Barang"
        '
        'DataSet1
        '
        Me.DataSet1.DataSetName = "NewDataSet"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(308, 180)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(102, 32)
        Me.Button4.TabIndex = 26
        Me.Button4.Text = "Cetak"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintDocument1
        '
        '
        'LaporanBarang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(679, 576)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "LaporanBarang"
        Me.Text = "LaporanBarang"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button3 As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents DataSet1 As DataSet
    Friend WithEvents Button4 As Button
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
End Class

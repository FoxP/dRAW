<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ReportForm
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportForm))
        Me.DataGridViewMain = New System.Windows.Forms.DataGridView()
        Me.StatusStripMain = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabelLeft = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabelMain = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabelRight = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.DataGridViewMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStripMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridViewMain
        '
        Me.DataGridViewMain.AllowUserToAddRows = False
        Me.DataGridViewMain.AllowUserToDeleteRows = False
        Me.DataGridViewMain.AllowUserToOrderColumns = True
        Me.DataGridViewMain.AllowUserToResizeRows = False
        Me.DataGridViewMain.BackgroundColor = System.Drawing.SystemColors.Control
        Me.DataGridViewMain.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewMain.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewMain.Name = "DataGridViewMain"
        Me.DataGridViewMain.ReadOnly = True
        Me.DataGridViewMain.RowHeadersVisible = False
        Me.DataGridViewMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewMain.ShowCellErrors = False
        Me.DataGridViewMain.ShowCellToolTips = False
        Me.DataGridViewMain.ShowEditingIcon = False
        Me.DataGridViewMain.ShowRowErrors = False
        Me.DataGridViewMain.Size = New System.Drawing.Size(784, 561)
        Me.DataGridViewMain.TabIndex = 0
        '
        'StatusStripMain
        '
        Me.StatusStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelLeft, Me.ToolStripStatusLabelMain, Me.ToolStripStatusLabelRight})
        Me.StatusStripMain.Location = New System.Drawing.Point(0, 539)
        Me.StatusStripMain.Name = "StatusStripMain"
        Me.StatusStripMain.Size = New System.Drawing.Size(784, 22)
        Me.StatusStripMain.SizingGrip = False
        Me.StatusStripMain.TabIndex = 1
        Me.StatusStripMain.Text = "StatusStripMain"
        '
        'ToolStripStatusLabelLeft
        '
        Me.ToolStripStatusLabelLeft.Name = "ToolStripStatusLabelLeft"
        Me.ToolStripStatusLabelLeft.Size = New System.Drawing.Size(313, 17)
        Me.ToolStripStatusLabelLeft.Spring = True
        '
        'ToolStripStatusLabelMain
        '
        Me.ToolStripStatusLabelMain.Name = "ToolStripStatusLabelMain"
        Me.ToolStripStatusLabelMain.Size = New System.Drawing.Size(142, 17)
        Me.ToolStripStatusLabelMain.Text = "ToolStripStatusLabelMain"
        '
        'ToolStripStatusLabelRight
        '
        Me.ToolStripStatusLabelRight.Name = "ToolStripStatusLabelRight"
        Me.ToolStripStatusLabelRight.Size = New System.Drawing.Size(313, 17)
        Me.ToolStripStatusLabelRight.Spring = True
        '
        'ReportForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.StatusStripMain)
        Me.Controls.Add(Me.DataGridViewMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ReportForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "dRAW : Report"
        CType(Me.DataGridViewMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStripMain.ResumeLayout(False)
        Me.StatusStripMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridViewMain As DataGridView
    Friend WithEvents StatusStripMain As StatusStrip
    Friend WithEvents ToolStripStatusLabelLeft As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabelMain As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabelRight As ToolStripStatusLabel
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.gbDelete = New System.Windows.Forms.GroupBox()
        Me.gbDeleteInput = New System.Windows.Forms.GroupBox()
        Me.cbGetDeletedFolderPath = New System.Windows.Forms.Button()
        Me.cbRecursive = New System.Windows.Forms.CheckBox()
        Me.tbDeletedFolderPath = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbMoveToOrphanFolder = New System.Windows.Forms.CheckBox()
        Me.cbMoveToRecycleBin = New System.Windows.Forms.CheckBox()
        Me.cbDeletedFileFormat = New System.Windows.Forms.ComboBox()
        Me.gbMatch = New System.Windows.Forms.GroupBox()
        Me.gbMatchInput = New System.Windows.Forms.GroupBox()
        Me.cbSame = New System.Windows.Forms.CheckBox()
        Me.cbGetMatchingFolderPath = New System.Windows.Forms.Button()
        Me.tbMatchingFolderPath = New System.Windows.Forms.TextBox()
        Me.rbExist = New System.Windows.Forms.RadioButton()
        Me.cbOnly = New System.Windows.Forms.Label()
        Me.rbNotExist = New System.Windows.Forms.RadioButton()
        Me.cbMatchingFileFormat = New System.Windows.Forms.ComboBox()
        Me.ToolTipMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.cbAbout = New System.Windows.Forms.Button()
        Me.cbApply = New System.Windows.Forms.Button()
        Me.pb = New System.Windows.Forms.ProgressBar()
        Me.cbSwap = New System.Windows.Forms.Button()
        Me.cbDryRun = New System.Windows.Forms.Button()
        Me.gbDelete.SuspendLayout()
        Me.gbDeleteInput.SuspendLayout()
        Me.gbMatch.SuspendLayout()
        Me.gbMatchInput.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbDelete
        '
        Me.gbDelete.Controls.Add(Me.gbDeleteInput)
        Me.gbDelete.Controls.Add(Me.Label1)
        Me.gbDelete.Controls.Add(Me.cbMoveToOrphanFolder)
        Me.gbDelete.Controls.Add(Me.cbMoveToRecycleBin)
        Me.gbDelete.Controls.Add(Me.cbDeletedFileFormat)
        Me.gbDelete.Location = New System.Drawing.Point(12, 7)
        Me.gbDelete.Name = "gbDelete"
        Me.gbDelete.Size = New System.Drawing.Size(474, 152)
        Me.gbDelete.TabIndex = 0
        Me.gbDelete.TabStop = False
        Me.gbDelete.Text = " File extension to delete / move : "
        '
        'gbDeleteInput
        '
        Me.gbDeleteInput.Controls.Add(Me.cbGetDeletedFolderPath)
        Me.gbDeleteInput.Controls.Add(Me.cbRecursive)
        Me.gbDeleteInput.Controls.Add(Me.tbDeletedFolderPath)
        Me.gbDeleteInput.Location = New System.Drawing.Point(10, 66)
        Me.gbDeleteInput.Name = "gbDeleteInput"
        Me.gbDeleteInput.Size = New System.Drawing.Size(443, 73)
        Me.gbDeleteInput.TabIndex = 4
        Me.gbDeleteInput.TabStop = False
        Me.gbDeleteInput.Text = "Input directory : "
        '
        'cbGetDeletedFolderPath
        '
        Me.cbGetDeletedFolderPath.Location = New System.Drawing.Point(359, 40)
        Me.cbGetDeletedFolderPath.Name = "cbGetDeletedFolderPath"
        Me.cbGetDeletedFolderPath.Size = New System.Drawing.Size(75, 23)
        Me.cbGetDeletedFolderPath.TabIndex = 7
        Me.cbGetDeletedFolderPath.Text = "Set directory"
        Me.cbGetDeletedFolderPath.UseVisualStyleBackColor = True
        '
        'cbRecursive
        '
        Me.cbRecursive.AutoSize = True
        Me.cbRecursive.Location = New System.Drawing.Point(10, 19)
        Me.cbRecursive.Name = "cbRecursive"
        Me.cbRecursive.Size = New System.Drawing.Size(109, 17)
        Me.cbRecursive.TabIndex = 5
        Me.cbRecursive.Text = "Recursive search"
        Me.cbRecursive.UseVisualStyleBackColor = True
        '
        'tbDeletedFolderPath
        '
        Me.tbDeletedFolderPath.Enabled = False
        Me.tbDeletedFolderPath.Location = New System.Drawing.Point(10, 42)
        Me.tbDeletedFolderPath.Name = "tbDeletedFolderPath"
        Me.tbDeletedFolderPath.Size = New System.Drawing.Size(338, 20)
        Me.tbDeletedFolderPath.TabIndex = 6
        Me.tbDeletedFolderPath.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Move to :"
        '
        'cbMoveToOrphanFolder
        '
        Me.cbMoveToOrphanFolder.AutoSize = True
        Me.cbMoveToOrphanFolder.Location = New System.Drawing.Point(145, 46)
        Me.cbMoveToOrphanFolder.Name = "cbMoveToOrphanFolder"
        Me.cbMoveToOrphanFolder.Size = New System.Drawing.Size(112, 17)
        Me.cbMoveToOrphanFolder.TabIndex = 3
        Me.cbMoveToOrphanFolder.Text = """orphan"" directory"
        Me.cbMoveToOrphanFolder.UseVisualStyleBackColor = True
        '
        'cbMoveToRecycleBin
        '
        Me.cbMoveToRecycleBin.AutoSize = True
        Me.cbMoveToRecycleBin.Checked = True
        Me.cbMoveToRecycleBin.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbMoveToRecycleBin.Location = New System.Drawing.Point(70, 46)
        Me.cbMoveToRecycleBin.Name = "cbMoveToRecycleBin"
        Me.cbMoveToRecycleBin.Size = New System.Drawing.Size(77, 17)
        Me.cbMoveToRecycleBin.TabIndex = 2
        Me.cbMoveToRecycleBin.Text = "recycle bin"
        Me.cbMoveToRecycleBin.UseVisualStyleBackColor = True
        '
        'cbDeletedFileFormat
        '
        Me.cbDeletedFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDeletedFileFormat.FormattingEnabled = True
        Me.cbDeletedFileFormat.Location = New System.Drawing.Point(20, 20)
        Me.cbDeletedFileFormat.Name = "cbDeletedFileFormat"
        Me.cbDeletedFileFormat.Size = New System.Drawing.Size(433, 21)
        Me.cbDeletedFileFormat.TabIndex = 1
        '
        'gbMatch
        '
        Me.gbMatch.Controls.Add(Me.gbMatchInput)
        Me.gbMatch.Controls.Add(Me.rbExist)
        Me.gbMatch.Controls.Add(Me.cbOnly)
        Me.gbMatch.Controls.Add(Me.rbNotExist)
        Me.gbMatch.Controls.Add(Me.cbMatchingFileFormat)
        Me.gbMatch.Location = New System.Drawing.Point(498, 7)
        Me.gbMatch.Name = "gbMatch"
        Me.gbMatch.Size = New System.Drawing.Size(474, 152)
        Me.gbMatch.TabIndex = 9
        Me.gbMatch.TabStop = False
        Me.gbMatch.Text = " Corresponding file extension to search : "
        '
        'gbMatchInput
        '
        Me.gbMatchInput.Controls.Add(Me.cbSame)
        Me.gbMatchInput.Controls.Add(Me.cbGetMatchingFolderPath)
        Me.gbMatchInput.Controls.Add(Me.tbMatchingFolderPath)
        Me.gbMatchInput.Location = New System.Drawing.Point(11, 66)
        Me.gbMatchInput.Name = "gbMatchInput"
        Me.gbMatchInput.Size = New System.Drawing.Size(443, 73)
        Me.gbMatchInput.TabIndex = 13
        Me.gbMatchInput.TabStop = False
        Me.gbMatchInput.Text = "Input directory : "
        '
        'cbSame
        '
        Me.cbSame.AutoSize = True
        Me.cbSame.Location = New System.Drawing.Point(10, 19)
        Me.cbSame.Name = "cbSame"
        Me.cbSame.Size = New System.Drawing.Size(84, 17)
        Me.cbSame.TabIndex = 14
        Me.cbSame.Text = "Same as left"
        Me.cbSame.UseVisualStyleBackColor = True
        '
        'cbGetMatchingFolderPath
        '
        Me.cbGetMatchingFolderPath.Location = New System.Drawing.Point(358, 40)
        Me.cbGetMatchingFolderPath.Name = "cbGetMatchingFolderPath"
        Me.cbGetMatchingFolderPath.Size = New System.Drawing.Size(75, 23)
        Me.cbGetMatchingFolderPath.TabIndex = 16
        Me.cbGetMatchingFolderPath.Text = "Set directory"
        Me.cbGetMatchingFolderPath.UseVisualStyleBackColor = True
        '
        'tbMatchingFolderPath
        '
        Me.tbMatchingFolderPath.Enabled = False
        Me.tbMatchingFolderPath.Location = New System.Drawing.Point(10, 42)
        Me.tbMatchingFolderPath.Name = "tbMatchingFolderPath"
        Me.tbMatchingFolderPath.Size = New System.Drawing.Size(338, 20)
        Me.tbMatchingFolderPath.TabIndex = 15
        Me.tbMatchingFolderPath.TabStop = False
        '
        'rbExist
        '
        Me.rbExist.AutoSize = True
        Me.rbExist.Location = New System.Drawing.Point(62, 45)
        Me.rbExist.Name = "rbExist"
        Me.rbExist.Size = New System.Drawing.Size(46, 17)
        Me.rbExist.TabIndex = 11
        Me.rbExist.TabStop = True
        Me.rbExist.Text = "exist"
        Me.rbExist.UseVisualStyleBackColor = True
        '
        'cbOnly
        '
        Me.cbOnly.AutoSize = True
        Me.cbOnly.Location = New System.Drawing.Point(18, 47)
        Me.cbOnly.Name = "cbOnly"
        Me.cbOnly.Size = New System.Drawing.Size(45, 13)
        Me.cbOnly.TabIndex = 13
        Me.cbOnly.Text = "Only if : "
        '
        'rbNotExist
        '
        Me.rbNotExist.AutoSize = True
        Me.rbNotExist.Location = New System.Drawing.Point(110, 45)
        Me.rbNotExist.Name = "rbNotExist"
        Me.rbNotExist.Size = New System.Drawing.Size(72, 17)
        Me.rbNotExist.TabIndex = 12
        Me.rbNotExist.TabStop = True
        Me.rbNotExist.Text = "NOT exist"
        Me.rbNotExist.UseVisualStyleBackColor = True
        '
        'cbMatchingFileFormat
        '
        Me.cbMatchingFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMatchingFileFormat.FormattingEnabled = True
        Me.cbMatchingFileFormat.Location = New System.Drawing.Point(21, 20)
        Me.cbMatchingFileFormat.Name = "cbMatchingFileFormat"
        Me.cbMatchingFileFormat.Size = New System.Drawing.Size(435, 21)
        Me.cbMatchingFileFormat.TabIndex = 10
        '
        'cbAbout
        '
        Me.cbAbout.Location = New System.Drawing.Point(878, 171)
        Me.cbAbout.Name = "cbAbout"
        Me.cbAbout.Size = New System.Drawing.Size(95, 23)
        Me.cbAbout.TabIndex = 19
        Me.cbAbout.Text = "?"
        Me.cbAbout.UseVisualStyleBackColor = True
        '
        'cbApply
        '
        Me.cbApply.Location = New System.Drawing.Point(257, 171)
        Me.cbApply.Name = "cbApply"
        Me.cbApply.Size = New System.Drawing.Size(230, 23)
        Me.cbApply.TabIndex = 18
        Me.cbApply.Text = "Apply"
        Me.cbApply.UseVisualStyleBackColor = True
        '
        'pb
        '
        Me.pb.Enabled = False
        Me.pb.Location = New System.Drawing.Point(-1, 207)
        Me.pb.MarqueeAnimationSpeed = 5
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(986, 15)
        Me.pb.TabIndex = 20
        '
        'cbSwap
        '
        Me.cbSwap.Location = New System.Drawing.Point(475, 26)
        Me.cbSwap.Name = "cbSwap"
        Me.cbSwap.Size = New System.Drawing.Size(34, 23)
        Me.cbSwap.TabIndex = 8
        Me.cbSwap.Text = "< >"
        Me.cbSwap.UseVisualStyleBackColor = True
        '
        'cbDryRun
        '
        Me.cbDryRun.Location = New System.Drawing.Point(11, 171)
        Me.cbDryRun.Name = "cbDryRun"
        Me.cbDryRun.Size = New System.Drawing.Size(230, 23)
        Me.cbDryRun.TabIndex = 17
        Me.cbDryRun.Text = "Dry-run"
        Me.cbDryRun.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 221)
        Me.Controls.Add(Me.cbDryRun)
        Me.Controls.Add(Me.cbSwap)
        Me.Controls.Add(Me.pb)
        Me.Controls.Add(Me.cbApply)
        Me.Controls.Add(Me.cbAbout)
        Me.Controls.Add(Me.gbMatch)
        Me.Controls.Add(Me.gbDelete)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.Text = "dRAW"
        Me.gbDelete.ResumeLayout(False)
        Me.gbDelete.PerformLayout()
        Me.gbDeleteInput.ResumeLayout(False)
        Me.gbDeleteInput.PerformLayout()
        Me.gbMatch.ResumeLayout(False)
        Me.gbMatch.PerformLayout()
        Me.gbMatchInput.ResumeLayout(False)
        Me.gbMatchInput.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbDelete As GroupBox
    Friend WithEvents cbDeletedFileFormat As ComboBox
    Friend WithEvents gbMatch As GroupBox
    Friend WithEvents cbMatchingFileFormat As ComboBox
    Friend WithEvents rbNotExist As RadioButton
    Friend WithEvents rbExist As RadioButton
    Friend WithEvents tbDeletedFolderPath As TextBox
    Friend WithEvents cbGetDeletedFolderPath As Button
    Friend WithEvents ToolTipMain As ToolTip
    Friend WithEvents cbAbout As Button
    Friend WithEvents cbApply As Button
    Friend WithEvents cbRecursive As CheckBox
    Friend WithEvents cbMoveToRecycleBin As CheckBox
    Friend WithEvents pb As ProgressBar
    Friend WithEvents cbSwap As Button
    Friend WithEvents cbMoveToOrphanFolder As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cbOnly As Label
    Friend WithEvents cbDryRun As Button
    Friend WithEvents cbGetMatchingFolderPath As Button
    Friend WithEvents tbMatchingFolderPath As TextBox
    Friend WithEvents gbDeleteInput As GroupBox
    Friend WithEvents gbMatchInput As GroupBox
    Friend WithEvents cbSame As CheckBox
End Class

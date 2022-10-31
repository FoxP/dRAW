'-----------------------------------------------------------------------------------------------------------------------------------------------
'
'	This program is free software; you can redistribute it and/or
'	modify it under the terms of the GNU General Public License
'	as published by the Free Software Foundation; either version 2
'	of the License, or (at your option) any later version.
'
'	This program is distributed in the hope that it will be useful,
'	but WITHOUT ANY WARRANTY; without even the implied warranty of
'	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'	GNU General Public License for more details.
'
'	You should have received a copy of the GNU General Public License
'	along with this program; if not, write to the Free Software Foundation,
'	Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
'
'	Name :
'				dRAW
'	Author :
'				Paul RENARD
'
'-----------------------------------------------------------------------------------------------------------------------------------------------

Imports System.IO
Imports System.Threading
Imports System.Configuration

Public Class MainForm

    Public dicFileFormats As New Dictionary(Of String, String)
    Public t As Thread

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.Text = My.Application.Info.AssemblyName & " - v" & My.Application.Info.Version.ToString

        ToolTipMain.SetToolTip(cbAbout, "About " & My.Application.Info.AssemblyName)
        ToolTipMain.SetToolTip(cbGetDeletedFolderPath, "Select a directory")
        ToolTipMain.SetToolTip(cbGetMatchingFolderPath, "Select a directory")
        ToolTipMain.SetToolTip(cbRecursive, "Recursivity in selected directory")
        ToolTipMain.SetToolTip(cbMoveToRecycleBin, "Don't delete permanently")
        ToolTipMain.SetToolTip(cbMoveToOrphanFolder, "Move files to " & """" & "orphan" & """" & " directory(s)")
        ToolTipMain.SetToolTip(cbDeletedFileFormat, "File format that will be deleted")
        ToolTipMain.SetToolTip(cbMatchingFileFormat, "File format to match / search for")
        ToolTipMain.SetToolTip(cbSwap, "Swap file formats")
        ToolTipMain.SetToolTip(cbSame, "Same directory as left input directory")
        ToolTipMain.SetToolTip(cbDryRun, "Do a trial run with no permanent changes : only preview.")

        'Delete / check for corrupted "user.config" file

        Try
            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal)
        Catch exception As ConfigurationErrorsException
            File.Delete(exception.Filename)
        End Try

#If DEBUG Then
        My.Settings.Reset()
#End If

        'Load app config

        For Each s As String In My.Settings.FileFormats
            If Not dicFileFormats.ContainsKey(s) Then
                dicFileFormats.Add(s, s.Substring(0, 4).Trim)
            End If
        Next

        Dim listFileFormats As New List(Of String)
        listFileFormats = dicFileFormats.Keys.ToList

        cbDeletedFileFormat.DataSource = listFileFormats
        cbMatchingFileFormat.BindingContext = New BindingContext
        cbMatchingFileFormat.DataSource = listFileFormats

        'Restore user settings

        If My.Settings.sFileFormatToDelete <> String.Empty Then
            If dicFileFormats.ContainsKey(My.Settings.sFileFormatToDelete) Then
                cbDeletedFileFormat.Text = My.Settings.sFileFormatToDelete
            End If
        End If
        If My.Settings.sFileFormatToMatch <> String.Empty Then
            If dicFileFormats.ContainsKey(My.Settings.sFileFormatToMatch) Then
                cbMatchingFileFormat.Text = My.Settings.sFileFormatToMatch
            End If
        End If

        AddHandler cbDeletedFileFormat.SelectedIndexChanged, AddressOf cbDeletedFileFormat_SelectedIndexChanged
        AddHandler cbMatchingFileFormat.SelectedIndexChanged, AddressOf cbMatchingFileFormat_SelectedIndexChanged

        If My.Settings.bRecursive Then
            cbRecursive.Checked = True
        End If

        AddHandler cbRecursive.CheckedChanged, AddressOf cbRecursive_CheckedChanged

        If Not My.Settings.bMoveToRecycleBin Then
            cbMoveToRecycleBin.Checked = False
        End If

        AddHandler cbMoveToRecycleBin.CheckedChanged, AddressOf cbMoveToRecycleBin_CheckedChanged

        If My.Settings.bMoveToOrphanFolder Then
            cbMoveToOrphanFolder.Checked = True
        End If

        AddHandler cbMoveToOrphanFolder.CheckedChanged, AddressOf cbMoveToOrphanedFolder_CheckedChanged

        If Not My.Settings.sDeletedFolderPath = String.Empty Then
            tbDeletedFolderPath.Text = My.Settings.sDeletedFolderPath
        End If

        AddHandler tbDeletedFolderPath.TextChanged, AddressOf tbDeletedFolderPath_TextChanged

        If Not My.Settings.sMatchingFolderPath = String.Empty Then
            tbMatchingFolderPath.Text = My.Settings.sMatchingFolderPath
        End If

        AddHandler tbMatchingFolderPath.TextChanged, AddressOf tbMatchingFolderPath_TextChanged

        If My.Settings.bSameFolderPath Then
            cbSame.Checked = True
        End If

        AddHandler cbSame.CheckedChanged, AddressOf cbSame_CheckedChanged

    End Sub

    Sub updateApplyTooltip()
        If Not cbDeletedFileFormat.Enabled Then
            ToolTipMain.SetToolTip(cbApply, "Stop " & If(cbRecursive.Checked, "(recursively) ", "") & If(cbMoveToRecycleBin.Checked, "moving files to recycle bin", If(cbMoveToOrphanFolder.Checked, "moving files to " & """" & "orphan" & """" & " directory(s)", "deleting files")) & "...")
        Else
            ToolTipMain.SetToolTip(cbApply, If(cbMoveToRecycleBin.Checked, "Move to recycle bin", If(cbMoveToOrphanFolder.Checked, "Move to " & """" & "orphan" & """" & " directory(s)", "Delete")) & " " & cbDeletedFileFormat.Text.Substring(0, 4).Trim & " files" & If(tbDeletedFolderPath.Text <> String.Empty, " from " & """" & Path.GetFileName(tbDeletedFolderPath.Text) & """" & " directory", "") & " if matching " & cbMatchingFileFormat.Text.Substring(0, 4).Trim & " " & If(rbExist.Checked, "exists", "does not exist") & If(tbMatchingFolderPath.Text <> String.Empty, " in " & """" & Path.GetFileName(tbMatchingFolderPath.Text) & """" & " directory", "") & If(cbRecursive.Checked, ", recursively", "") & ".")
        End If
    End Sub

    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        'Restore user settings

        If My.Settings.bExist Then
            rbExist.Checked = True
        Else
            rbNotExist.Checked = True
        End If

        AddHandler rbExist.CheckedChanged, AddressOf rbExist_CheckedChanged
        AddHandler rbNotExist.CheckedChanged, AddressOf rbNotExist_CheckedChanged

    End Sub

    Private Sub cbDeletedFileFormat_SelectedIndexChanged()
        My.Settings.sFileFormatToDelete = cbDeletedFileFormat.Text
    End Sub
    Private Sub cbMatchingFileFormat_SelectedIndexChanged()
        My.Settings.sFileFormatToMatch = cbMatchingFileFormat.Text
    End Sub

    Private Sub rbExist_CheckedChanged()
        If rbExist.Checked Then
            My.Settings.bExist = True
        End If
    End Sub

    Private Sub rbNotExist_CheckedChanged()
        If rbNotExist.Checked Then
            My.Settings.bExist = False
        End If
    End Sub

    Private Sub cbGetDeletedFolderPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbGetDeletedFolderPath.Click
        If Not tbDeletedFolderPath.Text = String.Empty Then
            tbDeletedFolderPath.Text = getFolderFromDialog(, tbDeletedFolderPath.Text)
        Else
            tbDeletedFolderPath.Text = getFolderFromDialog()
        End If
    End Sub

    Private Sub cbGetMatchingFolderPath_Click(sender As Object, e As EventArgs) Handles cbGetMatchingFolderPath.Click
        If Not tbMatchingFolderPath.Text = String.Empty Then
            tbMatchingFolderPath.Text = getFolderFromDialog(, tbMatchingFolderPath.Text)
        Else
            tbMatchingFolderPath.Text = getFolderFromDialog()
        End If
        If cbSame.Checked Then
            If tbMatchingFolderPath.Text <> tbDeletedFolderPath.Text Then
                cbSame.Checked = False
            End If
        End If
    End Sub

    Private Sub cbRecursive_CheckedChanged()
        If cbRecursive.Checked Then
            My.Settings.bRecursive = True
        Else
            My.Settings.bRecursive = False
        End If
    End Sub

    Private Sub cbMoveToRecycleBin_CheckedChanged(sender As Object, e As EventArgs)
        If cbMoveToRecycleBin.Checked Then
            My.Settings.bMoveToRecycleBin = True
            cbMoveToOrphanFolder.Checked = False
        Else
            My.Settings.bMoveToRecycleBin = False
        End If
    End Sub

    Private Sub cbMoveToOrphanedFolder_CheckedChanged(sender As Object, e As EventArgs)
        If cbMoveToOrphanFolder.Checked Then
            My.Settings.bMoveToOrphanFolder = True
            cbMoveToRecycleBin.Checked = False
        Else
            My.Settings.bMoveToOrphanFolder = False
        End If
    End Sub

    Private Sub cbSame_CheckedChanged(sender As Object, e As EventArgs) Handles cbSame.CheckedChanged
        If cbSame.Checked Then
            My.Settings.bSameFolderPath = True
            tbMatchingFolderPath.Text = tbDeletedFolderPath.Text
        Else
            My.Settings.bSameFolderPath = False
        End If
    End Sub

    Private Sub tbDeletedFolderPath_TextChanged(sender As Object, e As EventArgs)
        My.Settings.sDeletedFolderPath = tbDeletedFolderPath.Text
        If cbSame.Checked Then
            tbMatchingFolderPath.Text = tbDeletedFolderPath.Text
        End If
    End Sub

    Private Sub tbMatchingFolderPath_TextChanged(sender As Object, e As EventArgs)
        My.Settings.sMatchingFolderPath = tbMatchingFolderPath.Text
    End Sub

    Private Sub cbApply_Click(sender As Object, e As EventArgs) Handles cbApply.Click, cbDryRun.Click

        Dim bDryRun As Boolean
        If sender Is cbDryRun Then
            bDryRun = True
        End If

        'Stop / abort
        If Not cbDeletedFileFormat.Enabled Then
            Try
                t.Abort()
            Catch ex As Exception
                'Oops
            End Try
            Call lockOrUnlockUI(True, bDryRun)
            Exit Sub
        End If

        'Start / apply
        If tbDeletedFolderPath.Text <> String.Empty And tbMatchingFolderPath.Text <> String.Empty Then
            Dim bDeletedFolderPathExist As Boolean = Directory.Exists(tbDeletedFolderPath.Text)
            Dim bMatchingFolderPathExist As Boolean = Directory.Exists(tbMatchingFolderPath.Text)
            If bDeletedFolderPathExist And bMatchingFolderPathExist Then

                Dim filesDic As New List(Of TheFile)

                If Not bDryRun Then
                    If MessageBox.Show(Me, If(cbMoveToRecycleBin.Checked, "Move to recycle bin", If(cbMoveToOrphanFolder.Checked, "Move to " & """" & "orphan" & """" & " directory(s)", "Delete")) & " " & cbDeletedFileFormat.Text.Substring(0, 4).Trim & " files" & If(tbDeletedFolderPath.Text <> String.Empty, " from " & """" & Path.GetFileName(tbDeletedFolderPath.Text) & """" & " directory", "") & " if matching " & cbMatchingFileFormat.Text.Substring(0, 4).Trim & " " & If(rbExist.Checked, "exists", "does not exist") & If(tbMatchingFolderPath.Text <> String.Empty, " in " & """" & Path.GetFileName(tbMatchingFolderPath.Text) & """", "") & " directory" & If(cbRecursive.Checked, ", recursively", "") & "? " & vbNewLine & vbNewLine & """" & "OK" & """" & " to continue, " & """" & "Cancel" & """" & " to abort.", My.Application.Info.AssemblyName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = vbCancel Then
                        Exit Sub
                    End If
                End If

                Dim sExtensionToDelete As String = dicFileFormats(cbDeletedFileFormat.Text)
                Dim sExtensionToMatch As String = dicFileFormats(cbMatchingFileFormat.Text)
                Dim bMoveToRecycleBin As Boolean = cbMoveToRecycleBin.Checked
                Dim bMoveToOrphanFolder As Boolean = cbMoveToOrphanFolder.Checked
                Dim sDeletedFolderPath As String = tbDeletedFolderPath.Text
                Dim sMatchingFolderPath As String = tbMatchingFolderPath.Text
                Dim bExist As Boolean = rbExist.Checked

                Call lockOrUnlockUI(False, bDryRun)

                Dim t As New Thread(Sub()

                                        For Each sFilePathToDelete As String In GetFilesRecursive(sDeletedFolderPath, cbRecursive.Checked)
                                            If Path.GetExtension(sFilePathToDelete).ToUpper = "." & sExtensionToDelete.ToUpper Then
                                                Dim sFilePathToMatch As String = Path.Combine(Path.GetDirectoryName(sFilePathToDelete.Replace(sDeletedFolderPath, sMatchingFolderPath)), Path.GetFileNameWithoutExtension(sFilePathToDelete)) & "." & sExtensionToMatch
                                                If bExist Then
                                                    If File.Exists(sFilePathToMatch) Then
                                                        Debug.Print(sExtensionToMatch & " found, will " & If(cbMoveToRecycleBin.Checked, "move to recycle bin", If(cbMoveToOrphanFolder.Checked, "move to " & """" & "orphan" & """" & " directory(s)", "delete")) & " " & sExtensionToDelete & " : " & sFilePathToDelete)
                                                        Call deleteFile(bMoveToRecycleBin, bMoveToOrphanFolder, sFilePathToDelete, filesDic, bDryRun)
                                                    End If
                                                Else
                                                    If Not File.Exists(sFilePathToMatch) Then
                                                        Debug.Print(sExtensionToMatch & " NOT found, will " & If(cbMoveToRecycleBin.Checked, "move to recycle bin", If(cbMoveToOrphanFolder.Checked, "move to " & """" & "orphan" & """" & " directory(s)", "delete")) & " " & sExtensionToDelete & " : " & sFilePathToDelete)
                                                        Call deleteFile(bMoveToRecycleBin, bMoveToOrphanFolder, sFilePathToDelete, filesDic, bDryRun)
                                                    End If
                                                End If
                                            End If
                                        Next

                                        If Me.InvokeRequired Then
                                            Me.Invoke(Sub() Call lockOrUnlockUI(True, bDryRun))
                                        Else
                                            Call lockOrUnlockUI(True, bDryRun)
                                        End If

                                        If Me.InvokeRequired Then
                                            Me.Invoke(Sub() Call showReport(filesDic))
                                        Else
                                            Call showReport(filesDic)
                                        End If

                                    End Sub)
                t.IsBackground = True
                t.SetApartmentState(ApartmentState.STA)
                t.Start()
            Else
                If (Not bDeletedFolderPathExist) And (Not bMatchingFolderPathExist) Then
                    If cbSame.Checked Then
                        MessageBox.Show(Me, "Oops, can't find :" & vbNewLine & vbNewLine & tbDeletedFolderPath.Text, My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show(Me, "Oops, can't find :" & vbNewLine & vbNewLine & "- " & tbDeletedFolderPath.Text & vbNewLine & "- " & tbMatchingFolderPath.Text, My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                ElseIf (Not bDeletedFolderPathExist) Then
                    MessageBox.Show(Me, "Oops, can't find :" & vbNewLine & vbNewLine & tbDeletedFolderPath.Text, My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ElseIf (Not bMatchingFolderPathExist) Then
                    MessageBox.Show(Me, "Oops, can't find :" & vbNewLine & vbNewLine & tbMatchingFolderPath.Text, My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Else
            If tbDeletedFolderPath.Text = String.Empty And tbMatchingFolderPath.Text = String.Empty Then
                MessageBox.Show(Me, "Oops, empty input directories.", My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show(Me, "Oops, empty input directory.", My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

    End Sub

    Sub deleteFile(ByVal bMoveToRecycleBin As Boolean, ByVal bMoveToOrphanFolder As Boolean, ByVal sFilePathToDelete As String, ByRef filesDic As List(Of TheFile), ByVal bDryRun As Boolean)
        If bMoveToRecycleBin Then
            Try
                If Not bDryRun Then My.Computer.FileSystem.DeleteFile(sFilePathToDelete, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                filesDic.Add(New TheFile(sFilePathToDelete, "Moved to recycle bin"))
            Catch ex As Exception
                'Oops
                filesDic.Add(New TheFile(sFilePathToDelete, "Error : Can't move to recycle bin"))
            End Try
        ElseIf bMoveToOrphanFolder Then
            If Not Path.GetFileName(Path.GetDirectoryName(sFilePathToDelete)).ToLower = "orphan" Then
                Dim sOrphanFolderPath As String = Path.Combine(Path.GetDirectoryName(sFilePathToDelete), "orphan")
                Try
                    If Not Directory.Exists(sOrphanFolderPath) Then
                        If Not bDryRun Then Directory.CreateDirectory(sOrphanFolderPath)
                    End If
                    If Not bDryRun Then File.Move(sFilePathToDelete, Path.Combine(sOrphanFolderPath, Path.GetFileName(sFilePathToDelete)))
                    filesDic.Add(New TheFile(sFilePathToDelete, "Moved to " & """" & "orphan" & """" & " directory"))
                Catch ex As Exception
                    'Oops
                    filesDic.Add(New TheFile(sFilePathToDelete, "Error : Can't move to " & """" & "orphan" & """" & " directory"))
                End Try
            Else
                Debug.Print("Ignored, already moved : " & sFilePathToDelete)
                filesDic.Add(New TheFile(sFilePathToDelete, "Ignored : already moved to " & """" & "orphan" & """" & " directory"))
            End If
        Else
            Try
                If Not bDryRun Then File.Delete(sFilePathToDelete)
                filesDic.Add(New TheFile(sFilePathToDelete, "Deleted"))
            Catch ex As Exception
                'Oops
                filesDic.Add(New TheFile(sFilePathToDelete, "Error : Can't delete"))
            End Try
        End If
    End Sub

    Private Sub cbAbout_Click(sender As Object, e As EventArgs) Handles cbAbout.Click
        ABOUT.StartPosition = FormStartPosition.CenterParent
        ABOUT.ShowDialog()
    End Sub

    Private Sub cbApply_MouseEnter(sender As Object, e As EventArgs) Handles cbApply.MouseEnter
        Call updateApplyTooltip()
    End Sub

    Sub lockOrUnlockUI(ByVal bUnlock As Boolean, ByVal bDryRun As Boolean)
        If Not bUnlock Then
            pb.Enabled = True
            pb.Style = ProgressBarStyle.Marquee
            If bDryRun Then
                cbDryRun.Text = "Stop"
            Else
                cbApply.Text = "Stop"
            End If
        Else
            pb.Enabled = False
            pb.Style = ProgressBarStyle.Blocks
            If bDryRun Then
                cbDryRun.Text = "Dry-run"
            Else
                cbApply.Text = "Apply"
            End If
        End If
        cbDeletedFileFormat.Enabled = bUnlock
        cbMatchingFileFormat.Enabled = bUnlock
        cbMoveToRecycleBin.Enabled = bUnlock
        cbMoveToOrphanFolder.Enabled = bUnlock
        cbSwap.Enabled = bUnlock
        cbSame.Enabled = bUnlock
        rbExist.Enabled = bUnlock
        rbNotExist.Enabled = bUnlock
        cbRecursive.Enabled = bUnlock
        cbGetDeletedFolderPath.Enabled = bUnlock
        cbGetMatchingFolderPath.Enabled = bUnlock
        If bDryRun Then
            cbApply.Enabled = bUnlock
        Else
            cbDryRun.Enabled = bUnlock
        End If
    End Sub

    Private Sub cbSwap_Click(sender As Object, e As EventArgs) Handles cbSwap.Click
        Dim sDeletedFileFormat = cbDeletedFileFormat.Text
        cbDeletedFileFormat.Text = cbMatchingFileFormat.Text
        cbMatchingFileFormat.Text = sDeletedFileFormat
    End Sub

    Sub showReport(ByVal filesDic As List(Of TheFile))

        ReportForm.DataGridViewMain.DataSource = Nothing
        ReportForm.DataGridViewMain.DataSource = filesDic

        For Each column As DataGridViewColumn In ReportForm.DataGridViewMain.Columns
            column.SortMode = DataGridViewColumnSortMode.Automatic
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Next

        ReportForm.StartPosition = FormStartPosition.CenterParent
        ReportForm.ShowDialog(Me)

    End Sub

    Public Class TheFile

        Public Sub New(ByVal path As String, ByVal status As String)
            _path = path
            _status = status
        End Sub

        Private _path As String
        Public Property File() As String
            Get
                Return _path
            End Get
            Set(ByVal value As String)
                _path = value
            End Set
        End Property

        Private _status As String
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property

    End Class

End Class

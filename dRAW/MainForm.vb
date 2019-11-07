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
        ToolTipMain.SetToolTip(cbGetInputFolderPath, "Select a directory")
        ToolTipMain.SetToolTip(cbRecursive, "Recursivity in selected directory")
        ToolTipMain.SetToolTip(cbMoveToRecycleBin, "Don't delete permanently")
        ToolTipMain.SetToolTip(cbMoveToOrphanFolder, "Move files to " & """" & "orphan" & """" & " directory(s)")
        ToolTipMain.SetToolTip(cbDeletedFileFormat, "File format that will be deleted")
        ToolTipMain.SetToolTip(cbMatchingFileFormat, "File format to match / search for")
        ToolTipMain.SetToolTip(cbSwap, "Swap file formats")

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

        If Not My.Settings.sInputFolderPath = String.Empty Then
            tbInputFolderPath.Text = My.Settings.sInputFolderPath
        End If

        AddHandler tbInputFolderPath.TextChanged, AddressOf tbInputFolderPath_TextChanged

        'TODO : readme git

    End Sub

    Sub updateApplyTooltip()
        If Not cbDeletedFileFormat.Enabled Then
            ToolTipMain.SetToolTip(cbApply, "Stop " & If(cbRecursive.Checked, "(recursively) ", "") & If(cbMoveToRecycleBin.Checked, "moving files to recycle bin", If(cbMoveToOrphanFolder.Checked, "moving files to " & """" & "orphan" & """" & " directory(s)", "deleting files")) & "...")
        Else
            ToolTipMain.SetToolTip(cbApply, If(cbMoveToRecycleBin.Checked, "Move to recycle bin", If(cbMoveToOrphanFolder.Checked, "Move to " & """" & "orphan" & """" & " directory(s)", "Delete")) & " " & cbDeletedFileFormat.Text.Substring(0, 4).Trim & " files if matching " & cbMatchingFileFormat.Text.Substring(0, 4).Trim & " " & If(rbExist.Checked, "exists", "does not exist") & If(tbInputFolderPath.Text <> String.Empty, " in " & """" & Path.GetFileName(tbInputFolderPath.Text) & """", "") & If(cbRecursive.Checked, ", recursively", "") & ".")
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

    Private Sub cbGetInputFolderPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbGetInputFolderPath.Click
        tbInputFolderPath.Text = getFolderFromDialog()
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

    Private Sub tbInputFolderPath_TextChanged(sender As Object, e As EventArgs)
        My.Settings.sInputFolderPath = tbInputFolderPath.Text
    End Sub

    Private Sub cbApply_Click(sender As Object, e As EventArgs) Handles cbApply.Click, cbDryRun.Click

        'Stop / abort
        If Not cbDeletedFileFormat.Enabled Then
            Try
                t.Abort()
            Catch ex As Exception
                'Oops
            End Try
            Call lockOrUnlockUI(True)
            Exit Sub
        End If

        'Start / apply
        If tbInputFolderPath.Text <> String.Empty Then
            If Directory.Exists(tbInputFolderPath.Text) Then

                Dim filesDic As New List(Of TheFile)
                Dim bDryRun As Boolean
                If sender Is cbDryRun Then
                    bDryRun = True
                End If
                If Not bDryRun Then
                    If MessageBox.Show(Me, If(cbMoveToRecycleBin.Checked, "Move to recycle bin", If(cbMoveToOrphanFolder.Checked, "Move to " & """" & "orphan" & """" & " directory(s)", "Delete")) & " " & cbDeletedFileFormat.Text.Substring(0, 4).Trim & " files if matching " & cbMatchingFileFormat.Text.Substring(0, 4).Trim & " " & If(rbExist.Checked, "exists", "does not exist") & If(tbInputFolderPath.Text <> String.Empty, " in " & """" & Path.GetFileName(tbInputFolderPath.Text) & """", "") & " directory" & If(cbRecursive.Checked, ", recursively", "") & "? " & vbNewLine & vbNewLine & """" & "OK" & """" & " to continue, " & """" & "Cancel" & """" & " to abort.", My.Application.Info.AssemblyName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = vbCancel Then
                        Exit Sub
                    End If
                End If

                Dim sExtensionToDelete As String = dicFileFormats(cbDeletedFileFormat.Text)
                Dim sExtensionToMatch As String = dicFileFormats(cbMatchingFileFormat.Text)
                Dim bMoveToRecycleBin As Boolean = cbMoveToRecycleBin.Checked
                Dim bMoveToOrphanFolder As Boolean = cbMoveToOrphanFolder.Checked
                Dim sInputFolderPath As String = tbInputFolderPath.Text
                Dim bExist As Boolean = rbExist.Checked

                Call lockOrUnlockUI(False)

                    Dim t As New Thread(Sub()

                                            For Each sFilePathToDelete As String In GetFilesRecursive(sInputFolderPath, cbRecursive.Checked)
                                                If Path.GetExtension(sFilePathToDelete).ToUpper = "." & sExtensionToDelete.ToUpper Then
                                                    Dim sFilePathToMatch As String = Path.Combine(Path.GetDirectoryName(sFilePathToDelete), Path.GetFileNameWithoutExtension(sFilePathToDelete)) & "." & sExtensionToMatch
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
                                                Me.Invoke(Sub() Call lockOrUnlockUI(True))
                                            Else
                                                Call lockOrUnlockUI(True)
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
                    MessageBox.Show(Me, "Oops, can't find :" & vbNewLine & vbNewLine & tbInputFolderPath.Text, My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show(Me, "Oops, empty input folder.", My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Sub lockOrUnlockUI(ByVal bUnlock As Boolean)
        If Not bUnlock Then
            pb.Enabled = True
            pb.Style = ProgressBarStyle.Marquee
            cbApply.Text = "Stop"
        Else
            pb.Enabled = False
            pb.Style = ProgressBarStyle.Blocks
            cbApply.Text = "Apply"
        End If
        cbDeletedFileFormat.Enabled = bUnlock
        cbMatchingFileFormat.Enabled = bUnlock
        cbMoveToRecycleBin.Enabled = bUnlock
        cbMoveToOrphanFolder.Enabled = bUnlock
        cbSwap.Enabled = bUnlock
        rbExist.Enabled = bUnlock
        rbNotExist.Enabled = bUnlock
        cbRecursive.Enabled = bUnlock
        cbGetInputFolderPath.Enabled = bUnlock
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

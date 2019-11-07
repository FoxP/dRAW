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

Module SharedCode

    Function getFolderFromDialog(Optional ByVal sDialogTitle As String = "Select a folder",
         Optional ByVal sInitialDirectory As String = "::{450d8fba-ad25-11d0-98a8-0800361b1103}",
         Optional ByVal bRestoreDirectory As Boolean = True
        ) As String
        Dim fd As OpenFileDialog = New OpenFileDialog()

        fd.Title = sDialogTitle
        fd.InitialDirectory = sInitialDirectory
        fd.RestoreDirectory = bRestoreDirectory
        fd.FileName = sDialogTitle
        fd.Filter = "Folder|*.folder"
        fd.CheckPathExists = True
        fd.ShowReadOnly = False
        fd.ReadOnlyChecked = True
        fd.CheckFileExists = False
        fd.ValidateNames = False

        If fd.ShowDialog() = DialogResult.OK Then
            Return Path.GetDirectoryName(fd.FileName)
        Else
            Return String.Empty
        End If
    End Function

    Function GetFilesRecursive(ByVal sPath As String, ByVal bIsRecursive As Boolean, Optional ByVal sExclude As List(Of String) = Nothing) As List(Of String)
        Dim listResult As New List(Of String)
        Dim stack As New Stack(Of String)

        stack.Push(sPath)

        Do While (stack.Count > 0)
            Dim dir As String = stack.Pop
            Try
                listResult.AddRange(Directory.GetFiles(dir, "*.*"))

                If bIsRecursive Then
                    Dim directoryName As String
                    For Each directoryName In Directory.GetDirectories(dir)
                        stack.Push(directoryName)
                    Next
                End If
            Catch ex As Exception
            End Try
        Loop

        Return listResult
    End Function

End Module

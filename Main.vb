Imports System.IO
Imports System.Security.Permissions

Public Class Main

    Function FileSplit(ByVal mapfile As String) As String()
        Dim i = mapfile.LastIndexOf("\")
        Return { _
            mapfile.Substring(0, i), _
            mapfile.Substring(i + 1, mapfile.Length - i - 1)}
    End Function

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFile.Text = My.Settings.URT
            txtCommands.Text = My.Settings.cmds

            Dim args() As String = System.Environment.GetCommandLineArgs()

            If args.Count >= 2 And txtFile.Text.Contains(".exe") Then
                Dim str = ""
                For i = 1 To args.Count - 1
                    str &= args(i) & " "
                Next

                '+g_gametype 9 +g_nodamage 1 +sv_pure 0
                Dim root = FileSplit(txtFile.Text)(0)
                Dim shit = "+set g_gametype 0"
                If str.Contains(shit) Then str = str.Remove(str.IndexOf(shit), Len(shit))

                Dim cmdargs = String.Format("+set fs_homepath ""{0}"" +set fs_basepath ""{0}"" {1} {2}", root, txtCommands.Text, str)

                'MsgBox(cmdargs)

                Process.Start(txtFile.Text, cmdargs)
                Me.Close()
            End If

        Catch ex As Exception
            MsgBox("An error occurred: " & ex.ToString)
            Me.Close()
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        My.Settings.URT = txtFile.Text
        My.Settings.cmds = txtCommands.Text
        My.Settings.Save()
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = My.Application.Info.DirectoryPath
        openFileDialog1.Filter = "Urban Terror .EXE |*.exe"
        openFileDialog1.Title = "Please select your Urban Terror .EXE"

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            txtFile.Text = openFileDialog1.FileName
        End If
    End Sub
End Class

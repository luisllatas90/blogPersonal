Imports Scripting


Partial Class FRMWEB
    Inherits System.Web.UI.Page
    Dim fso As New FileSystemObject
    Sub mostrararbol(ByVal x As TreeNode)

        Dim xfolder As Folder
        Dim tmpfolder As Folder
        xfolder = fso.GetFolder(x.Value)
        For Each tmpfolder In xfolder.SubFolders
            Dim trvnode As New TreeNode
            trvnode.Value = tmpfolder.Path
            trvnode.Text = tmpfolder.Name
            x.ChildNodes.Add(trvnode)
            mostrararbol(trvnode)
        Next

    End Sub
    Protected Sub cmdcargar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcargar.Click
        Dim nodex As New TreeNode

        nodex.Value = "E:\documentos aula virtual\archivoscv\"
        nodex.Text = "E:\documentos aula virtual\archivoscv\"
        Me.trvcarpetas.Nodes.Add(nodex)

        mostrararbol(nodex)
    End Sub

    Protected Sub trvcarpetas_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvcarpetas.SelectedNodeChanged
        ' mostrar los archivos
        Dim xfile As File
        Me.lstarchivos.Items.Clear()
        Dim xfolder As Folder
        xfolder = fso.GetFolder(Me.trvcarpetas.SelectedNode.Value)

        For Each xfile In xfolder.Files
            Me.lstarchivos.Items.Add(xfile.Name)
        Next

    End Sub
End Class

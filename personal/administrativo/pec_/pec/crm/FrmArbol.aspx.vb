
Partial Class administrativo_pec_crm_FrmListaInteresados
    Inherits System.Web.UI.Page
    'Private Sub CentroCostosPrimerNivel()
    '    Try
    '        Dim obj As New clsPlanOperativoAnual
    '        Dim dts As New Data.DataTable

    '        'Cargamos la data de las categorias
    '        dts = obj.ArbolCentroCostos(531)

    '        Dim strBodyTree As New StringBuilder

    '        If dts.Rows.Count > 0 Then
    '            For i As Integer = 0 To dts.Rows.Count - 1

    '                strBodyTree.Append("<ul>")
    '                strBodyTree.Append("<li class='parent_li'><span title='Expand this branch' class='parent'><i class='fa fa-folder-down fa-chevron-circle-down'></i>" + dts.Rows(i).Item("descripcion_Cco").ToString + "</span><a href=''></a>")
    '                strBodyTree.Append("<ul>")
    '                strBodyTree.Append("<li style='display: none;'><span title='Expand this branch' class='child'><i class='fa fa-file'></i>2</span> <a href=''></a></li>")
    '                strBodyTree.Append("</ul>")
    '                strBodyTree.Append("</li>")
    '                strBodyTree.Append("</ul>")

    '            Next
    '        Else
    '            strBodyTree.Append("")
    '        End If

    '        Me.CuerpoArbol.InnerHtml = strBodyTree.ToString

    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try

    'End Sub

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    CentroCostosPrimerNivel()
    'End Sub
End Class

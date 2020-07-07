
Partial Class administrativo_pec_crm_FrmListaInteresados
    Inherits System.Web.UI.Page

    'Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
    '    If IsPostBack = False Then
    '        ListaInteresados()
    '    End If

    'End Sub

    'Private Sub ListaInteresados()
    '    Dim obj As New ClsCRM
    '    Dim dt As New Data.DataTable
    '    Dim strTbody As New StringBuilder
    '    dt = obj.ListaInteresados(0)
    '    If dt.Rows.Count > 0 Then
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            strTbody.Append("<tr role='row'>")
    '            strTbody.Append("<td width='15%'>" & dt.Rows(i).Item("NumeroDocumento") & "</td>")
    '            strTbody.Append("<td width='50%'>" & dt.Rows(i).Item("Interesado") & "</td>")
    '            strTbody.Append("<td width='25%'>" & dt.Rows(i).Item("email") & "</td>")
    '            strTbody.Append("<td width='5%' align='center'><button type='button' class='btn btn-sm btn-info' data-toggle='modal' data-target='.modal2' data-id='" & obj.EncrytedString64(dt.Rows(i).Item("Codigo")) & "'  title='Editar' ><i class='ion-edit'></i></button></td>")
    '            strTbody.Append("<td width='5%' align='center'><button type='button' class='btn btn-sm btn-danger' data-toggle='modal' data-target='.bs-example-modal-sm' data-id='" & obj.EncrytedString64(dt.Rows(i).Item("Codigo")) & "' title='Eliminar' ><i class='ion-close'></i></button></td>")
    '            strTbody.Append("</tr>")
    '        Next
    '    Else
    '        strTbody.Append("")
    '    End If
    '    TbInteresados.InnerHtml = strTbody.ToString
    'End Sub

 
End Class

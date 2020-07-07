Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web.Security
Imports EncriptaCodigos
'Imports cEntidad
'Imports cLogica

Imports System.Collections.Generic
Partial Class CampusVirtualEstudiante_aulavirtual
    Inherits System.Web.UI.Page
    'Private oeCursoProgramado As eCursoProgramado
    'Private olCursoProgramado As lCursoProgramado
    'Private dt As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not Me.Page.User.Identity.IsAuthenticated Then
        'Response.Redirect("~/Default.aspx")

        '    Exit Sub
        'Else
        If Session("codigo_Alu").ToString.Length > 0 Then
            'Response.Write("<script>alert('" & Session("codigo_Alu").ToString & "')</script>")

            Dim dtt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtt = obj.TraerDataTable("MOODLE_ConsultarCodigoAcceso", "AL", Session("codigo_Alu"))
            obj.CerrarConexion()
            ''MsgBox(dtt.Rows(0).Item("codigo_pso").ToString())
            'Response.Write("<script>alert('" & dtt.Rows(0).Item("codigo_pso").ToString() & "')</script>")
            ''MsgBox(dtt.Rows(0).Item("ClaveInterna_Pso").ToString())
            'Response.Write("<script>alert('" & dtt.Rows(0).Item("ClaveInterna_Pso").ToString() & "')</script>")

            obj = Nothing
            'Response.Write(dtt.Rows(0).Item("codigo_pso"))
            'Listar()
            'Me.avm7.Value = Session("hu")
            Me.avm7.Value = dtt.Rows(0).Item("codigo_pso").ToString()
            ' Me.avm9.Text = dtt.Rows(0).Item("codigo_pso").ToString()
            ' Me.avm8.Value = Session("hp")
            Me.avm8.Value = dtt.Rows(0).Item("ClaveInterna_Pso").ToString()

            'Response.Write(Me.avm7.Value.ToString & "<br>")
            'Response.Write(Me.avm8.Value)

        Else
            Response.Redirect("~/Default.aspx")
        End If
        'End If
     

    End Sub

    'Private Sub Listar()
    '    Dim strTbody As New StringBuilder
    '    Dim codigo_cup As Integer = 0
    '    Dim i As Integer = 0
    '    'dt = New DataTable

    '    '    oeCursoProgramado = New eCursoProgramado
    '    '    olCursoProgramado = New lCursoProgramado
    '    '    oeCursoProgramado.codigoTest = Session("codigo_test")
    '    '    oeCursoProgramado.codigo_cac = Session("Codigo_Cac")
    '    '    oeCursoProgramado.codigo_Alu = Session("Codigo_Alu")
    '    '    dt = olCursoProgramado.moodleConsultarCursosMatriculados(oeCursoProgramado)

    '    '    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
    '    '        For i = 0 To dt.Rows.Count - 1
    '    '            strTbody.Append("<tr role='row'>")
    '    '            strTbody.Append("<td> " & (i + 1) & "</td>")
    '    '            strTbody.Append("<td>" & dt.Rows(i).Item("descripcion_cac").ToString & "-" & dt.Rows(i).Item("nombre_Cur").ToString & "-" & dt.Rows(i).Item("grupoHor_Cup").ToString & "</td>")
    '    '            strTbody.Append("<td>" & dt.Rows(i).Item("docente").ToString & "</td>")
    '    '            strTbody.Append("<td>" & dt.Rows(i).Item("estado").ToString & "</td>")
    '    '            strTbody.Append("</tr>")
    '    '        Next
    '    '    End If
    '    '    Me.tbCursos.InnerHtml = strTbody.ToString
    '    '    dt = Nothing
    'End Sub
End Class

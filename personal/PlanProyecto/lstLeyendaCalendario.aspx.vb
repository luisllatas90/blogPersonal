
Partial Class PlanProyecto_lstLeyendaCalendario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim codigo_pro As Integer = 0
        If (Right(Request.QueryString("pro"), 1) = "T") Then
            codigo_pro = Request.QueryString("pro").ToString.Substring(0, Request.QueryString("pro").ToString.Length - 1)
        Else
            codigo_pro = Request.QueryString("pro")
        End If
        'Response.Write("Proyecto: " & Request.QueryString("pro"))
        'tabla.InnerHtml = "TEST"
        tabla.InnerHtml = ListaLeyendaProyecto(codigo_pro)

    End Sub

    Private Function ListaLeyendaProyecto(ByVal cod_pro As Integer) As String
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            Dim dtLeyenda As New Data.DataTable
            Dim strLeyenda As String = ""
            obj.AbrirConexion()
            If (Request.QueryString("titPro").ToString.StartsWith("[Calendario]") = True) Then
                dtLeyenda = obj.TraerDataTable("PLAN_BuscaGrupoProyectoActividad", 0, cod_pro, "")
            Else
                dtLeyenda = obj.TraerDataTable("PLAN_BuscaGrupoProyectoActividad", cod_pro, 0, "")
            End If

            obj.CerrarConexion()

            If (dtLeyenda.Rows.Count > 0) Then
                strLeyenda = "<table width='100%' style='border:1px; border-style:solid; border-color:Black'>"
                For i As Integer = 0 To dtLeyenda.Rows.Count - 1
                    strLeyenda = strLeyenda & "<tr>"
                    strLeyenda = strLeyenda & "<td>" & dtLeyenda.Rows(i).Item("nombre_gpr").ToString & "</td>"
                    strLeyenda = strLeyenda & "<td><label style='background:" & dtLeyenda.Rows(i).Item("color_gpr").ToString & "'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label></td>"
                    strLeyenda = strLeyenda & "</tr>"
                Next
                strLeyenda = strLeyenda & "<tr>"
                strLeyenda = strLeyenda & "<td>OTROS</td>"
                strLeyenda = strLeyenda & "<td><label style='background:#0B2161'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label></td>"
                strLeyenda = strLeyenda & "</tr>"
                strLeyenda = strLeyenda & "</table>"
            End If
            obj = Nothing

            Return strLeyenda
        Catch ex As Exception
            Return "Error al generar Leyenda"
        End Try
    End Function
End Class

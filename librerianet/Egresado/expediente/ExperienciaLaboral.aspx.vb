Partial Class ExperienciaLaboral
    Inherits System.Web.UI.Page
    Public Editar As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'If IsPostBack = False Then
            CargarExperiencia()
            'End If
            If Page.Request.QueryString("del") IsNot Nothing Then
                btnBorrar_Click(decode(Page.Request.QueryString("del")))
            End If
        Catch ex As Exception
            Response.Redirect("sesion.aspx")
            'Response.Write(ex.Message & " -  " & ex.StackTrace)
        End Try

    End Sub

    Sub CargarExperiencia()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tbl As Data.DataTable
        Dim html As String = ""
        tbl = obj.TraerDataTable("[ALUMNI_ConsultarAlumni_ExperienciaLaboral]", Session("codigo_alu"))
        Me.GridExperiencia.InnerHtml = Session("codigo_alu")
        Dim classCss As String = ""
        Dim qs As String = ""
        If tbl.Rows.Count Then
            Session("codigo_pso") = tbl.Rows(0).Item("codigo_pso").ToString
            'Me.experiencia.SelectedValue = "S"            
            html &= "<table class=""tabla"" cellspacing=""0"">"
            html &= "<tr><th>Empresa</th><th>Desde</th><th>Hasta</th><th>Sector</th><th>Área</th><th>Cargo</th><th>Ciudad</th><th>Editar</th><th>Borrar</th></tr>"
            For i As Integer = 0 To tbl.Rows.Count - 1
                If i Mod 2 Then
                    classCss = "fila1"
                Else
                    classCss = "fila2"
                End If
                qs = encode(tbl.Rows(i).Item("id").ToString)
                html &= "<tr class=""" & classCss & """>"
                html &= "<td>" & tbl.Rows(i).Item("Empresa").ToString & "</td>"
                html &= "<td>" & tbl.Rows(i).Item("Fecha_Inicio").ToString & "</td>"
                html &= "<td>" & tbl.Rows(i).Item("Fecha_Fin").ToString & "</td>"
                'html &= "<td>" & tbl.Rows(i).Item("Estado").ToString & "</td>"
                html &= "<td>" & tbl.Rows(i).Item("Sector").ToString & "</td>"
                html &= "<td>" & tbl.Rows(i).Item("Area").ToString & "</td>"
                html &= "<td>" & tbl.Rows(i).Item("Cargo").ToString & "</td>"
                html &= "<td>" & tbl.Rows(i).Item("Ciudad").ToString & "</td>"
                html &= "<td><a class=""ifancybox"" href=""ExperienciaLaboralRegistrar.aspx?mod=" & qs & """><img src=""../archivos/editar.png"" alt=""Editar"" /></a></td>"
                html &= "<td><a class="""" href=""?del=" & qs & """ onclick=""return confirm_delete();""><img src=""../archivos/borrar.png"" alt=""Borrar"" /></a></td>"
                html &= "</tr>"
            Next
            html &= "</table>"
            GridExperiencia.InnerHtml = html
        Else
            'Me.experiencia.SelectedValue = "N"
            GridExperiencia.InnerHtml = "<br /><font style=""font-size:11px;""><i>Aún no has registrado Experiencia Laboral.</i></font>"
        End If
        tbl.Dispose()
        obj.CerrarConexion()
        obj = Nothing

    End Sub
    'Protected Sub experiencia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles experiencia.SelectedIndexChanged
    '    If Me.experiencia.SelectedValue = "S" Then
    '        Me.cmdGuardar0.Visible = True
    '    Else
    '        Me.cmdGuardar0.Visible = False
    '    End If
    'End Sub

    ' Protected Sub btnBorrar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    Sub btnBorrar_Click(ByVal codigo_exp As Integer)
        Try
            eliminarExp(codigo_exp)
            CargarExperiencia()
        Catch ex As Exception
        End Try
    End Sub
    Sub eliminarExp(ByVal codigo_exp As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("ALUMNI_EliminarExperiencia", codigo_exp)
        obj.CerrarConexion()
        obj = Nothing
        CargarExperiencia()
    End Sub

    Function encode(ByVal str As String) As String
        Return (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(str)))
    End Function

    Function decode(ByVal str As String) As String
        Return System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(str))
    End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("GradosyTitulos.aspx")
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Response.Redirect("perfil.aspx")
    End Sub
End Class



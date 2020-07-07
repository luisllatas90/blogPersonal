
Partial Class Encuesta_ReportesEvaluacionDocente_GraficarEncuestaDesempenioDetallado
    Inherits System.Web.UI.Page

    Protected Sub cmdDetallado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDetallado.Click
        Response.Redirect("GraficarEncuestaDesempenio.aspx?id=" & Request.QueryString("id") & "&per=" & Request.QueryString("per") & "&cup=" & Request.QueryString("cup") & _
                          "&cac=" & Request.QueryString("cac") & "&cev=" & Request.QueryString("cev") & "&ctf=" & Request.QueryString("ctf") & "&dac=" & Request.QueryString("dac"))
    End Sub

    Protected Sub cmdRetornar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRetornar.Click
        Response.Redirect("SeccionD_DesempenioDocentexItems.aspx?id=" & Request.QueryString("id") & "&per=" & Request.QueryString("per") & "&ctf=" & Request.QueryString("ctf") & _
                          "&cac=" & Request.QueryString("cac") & "&cev=" & Request.QueryString("cev") & "&dac=" & Request.QueryString("dac"))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Objcnx As New ClsConectarDatos
            Dim ObjFun As New ClsFunciones
            Dim datosCurso As New Data.DataTable
            Try
                Objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                Objcnx.AbrirConexion()
                datosCurso = Objcnx.TraerDataTable("EAD_ConsultarDatosCursoProfesor", Request.QueryString("per"), Request.QueryString("cac"), Request.QueryString("cup"))
                If datosCurso.Rows.Count > 0 Then
                    lblProfesor.Text = datosCurso.Rows(0).Item("Persona").ToString
                    lblEscuela.Text = datosCurso.Rows(0).Item("nombre_Cpf").ToString
                    lblGrupoHor.Text = "Grupo Horario: " & datosCurso.Rows(0).Item("grupoHor_Cup").ToString
                    lblCurso.Text = datosCurso.Rows(0).Item("nombre_Cur").ToString
                End If
                Objcnx.CerrarConexion()
            Catch ex As Exception
                Response.Write("Ocurrió un error:" & ex.Message)
            End Try
        End If
    End Sub
End Class

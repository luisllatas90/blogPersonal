
Partial Class Equipo_EditarSolicitud
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim tblReq As New Data.DataTable
            tblReq = ObjCnx.TraerDataTable("paReq_ConsultarSolicitud", Request.QueryString("id_sol"))
            If tblReq.Rows.Count > 0 Then
                Me.LblAplicacion.Text = tblReq.Rows(0).Item("descripcion_apl")
                Me.LblArea.Text = tblReq.Rows(0).Item("descripcion_cco")
                Me.LblPersona.Text = tblReq.Rows(0).Item("personal")
                Me.LblTipo.Text = tblReq.Rows(0).Item("descripcion_tsol")
                Me.txtDescripcion.Text = tblReq.Rows(0).Item("descripcion_sol")
                Me.txtfecha.Text = tblReq.Rows(0).Item("fecha_sol")
                Me.txtobservacion.Text = IIf(tblReq.Rows(0).Item("observacion_sol") Is DBNull.Value, "", tblReq.Rows(0).Item("observacion_sol"))
                Me.CboPrioridad.SelectedValue = tblReq.Rows(0).Item("prioridad_sol")
            End If
        End If
    End Sub

    Protected Sub CmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdCancelar.Click
        Response.Redirect("../Consultas/SolicitudesPendientes.aspx?id_sol=" & Request.QueryString("id_sol") & "&id=" & Request.QueryString("id"))
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ObjCnx.Ejecutar("pa_ReqActualizarSolicitud", CInt(Request.QueryString("id_sol").ToString), Me.txtDescripcion.Text, Me.CboPrioridad.SelectedValue, Me.txtfecha.Text, Me.txtobservacion.Text)
        Response.Redirect("../Consultas/SolicitudesPendientes.aspx?id=" & Request.QueryString("id"))
    End Sub
End Class

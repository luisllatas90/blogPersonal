
Partial Class Equipo_AdminSolicitud
    Inherits System.Web.UI.Page
    Private cod_per As Int32
    Private id_act As Int32

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        cod_per = Request.QueryString("id")
        id_act = Request.QueryString("id_act")
    End Sub

    Protected Sub FormView1_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdateEventArgs) Handles FormView1.ItemUpdating
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        e.Cancel = True
        Try
            'Response.Write(e.NewValues.Item(7))
            ObjCnx.IniciarTransaccion()
            With e.NewValues
                ObjCnx.Ejecutar("paReq_ActualizarEstadoSolicitud", CInt(Request.QueryString("id_act").ToString), .Item(3), cod_per, .Item(7), Request.QueryString("tipo").ToString)
                'Response.Write(Request.QueryString("id_act").ToString & " / " & .Item(3) & " / " & cod_per & " / " & .Item(7) & " / " & Request.QueryString("tipo").ToString)
            End With
            ObjCnx.TerminarTransaccion()
            Page.RegisterStartupScript("Exito", "<script>alert('Se registraron los datos correctamente'); window.opener.location.reload();window.close();</script>")
        Catch ex As Exception
            ObjCnx.AbortarTransaccion()
            Response.Write(ex.Message)
            Page.RegisterStartupScript("Fallo", "<script>alert('Ocurrió un error al grabar los datos')</script>")
        End Try
        ObjCnx = Nothing
    End Sub

End Class

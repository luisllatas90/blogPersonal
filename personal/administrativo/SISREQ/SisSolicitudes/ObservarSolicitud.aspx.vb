
Partial Class SisSolicitudes_ObservarSolicitud
    Inherits System.Web.UI.Page

    Protected Sub CmdCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdCerrar.Click
        Page.RegisterStartupScript("Cerrar", "<script>window.close();</script>")
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            Obj.IniciarTransaccion()
            Obj.Ejecutar("SOL_AgregarSolicitudComoObservada", Me.CboInstancia.SelectedValue, Me.TxtObservacion.Text, CInt(Request.QueryString("id_Eva")))
            If Request.QueryString("codigo_eob") <> Nothing Then
                Obj.Ejecutar("SOL_ActualizarObservacion", Request.QueryString("codigo_eob"))
            End If
            Obj.TerminarTransaccion()
            Page.RegisterStartupScript("Exito", "<script>alert('Se insertaron correctamente los datos'); window.close();</script>")
            Dim ObjMail As New ClsEnviaMail
            ObjMail.EnviarMailObservadas(CInt(Request.QueryString("id_sol")), Me.CboInstancia.SelectedValue, Me.TxtObservacion.Text)
        Catch ex As Exception
            Obj.AbortarTransaccion()
            Response.Write(ex.Message)
        End Try
    End Sub

End Class

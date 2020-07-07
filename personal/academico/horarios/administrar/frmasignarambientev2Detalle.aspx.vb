
Partial Class academico_horarios_administrar_frmasignarambientev2Detalle
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load       
    End Sub

    Protected Sub gridDetalle_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gridDetalle.RowUpdating
        Try
            Dim objcnx As New ClsConectarDatos
            Dim fechaini, fechafin As Date
            fechaini = e.NewValues(0).ToString
            fechafin = e.NewValues(1).ToString
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()
            objcnx.Ejecutar("AsingarAmbiente_ActualizarDetalle", CInt(Me.gridDetalle.DataKeys.Item(e.RowIndex).Values(0).ToString), fechaini, fechafin, CInt(Page.Request.QueryString("id")))
            objcnx.CerrarConexion()
            e.Cancel = True
            Me.RegisterStartupScript("cerrar", "<script>alert('Se actualizó');window.close();</script>")
            ' ClientScript.RegisterStartupScript(Me.GetType, "cerrar", "<script>window.close();</script>")
            'System.InvalidCastException
        Catch ex As Exception
            Me.RegisterStartupScript("cerrar", "<script>alert('Error');;</script>")
        End Try
        
    End Sub

    Protected Sub gridDetalle_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridDetalle.RowDeleting
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim tb As Data.DataTable
            tb = obj.TraerDataTable("AsingarAmbiente_EliminaDetalle", CInt(Me.gridDetalle.DataKeys.Item(e.RowIndex).Values(0).ToString))
            obj.CerrarConexion()
            e.Cancel = True
            If tb.Rows.Count Then
                Me.RegisterStartupScript("Aviso", "<script>alert('" & tb.Rows(0).Item(0).ToString & "')</script>")
            Else
                Me.RegisterStartupScript("cerrar", "<script>alert('Se ha eliminado el registro'); window.close();</script>")
            End If
            'ClientScript.RegisterStartupScript(Me.GetType, "cerrar", "<script>window.close();</script>")
        Catch ex As Exception
            Me.RegisterStartupScript("cerrar", "<script>alert('Error');</script>")
        End Try
    End Sub
End Class

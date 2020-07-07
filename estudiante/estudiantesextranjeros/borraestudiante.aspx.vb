
Partial Class librerianet_estudiantesextranjeros_borraestudiante
    Inherits System.Web.UI.Page

    Protected Sub gvEstudiante_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvEstudiante.RowDeleting
        eliminaritem(e.RowIndex)
        e.Cancel = True
    End Sub

    Sub eliminaritem(ByVal fila As Integer)
        Dim ObjCnx As New ClsConectarDatos
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        ObjCnx.AbrirConexion()
        ObjCnx.Ejecutar("EXT_EliminarAlumnoExtranjero", Me.gvEstudiante.DataKeys.Item(fila).Values(0))
        ObjCnx.CerrarConexion()
        gvEstudiante.databind()
    End Sub

    Protected Sub gvEstudiante_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEstudiante.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
        End If
    End Sub
End Class


Partial Class frmbuscacliente
    Inherits System.Web.UI.Page
    Dim cn As New clsaccesodatos
    Protected Sub cmdbuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdbuscar.Click
        Dim dts As New System.Data.DataSet, cadenabuscar As String
        ' buscar nombres del proveedor
        cadenabuscar = Replace(" " & Me.txtnombre.Text.Trim & " ", " ", "%")
        cn.abrirconexion()
        dts = cn.consultar("dbo.sp_buscacliente", "BT", "TO", cadenabuscar & "%", "", "", "")
        cn.cerrarconexion()
        Me.lstinformacion.DataSource = dts.Tables("consulta")
        Me.lstinformacion.DataBind()
    End Sub

    Protected Sub lstinformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles lstinformacion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")
            e.Row.Attributes.Add("onmouseleave", "resaltar(this,0)")
            e.Row.Attributes.Add("ondblclick", "mostrarConsultaComprasProveedor('" & e.Row.Cells(1).Text & "','" & e.Row.Cells(0).Text & "'); window.close();")
            'e.Row.Attributes.Add("onclick", "window.opener.document.all.txtnumerodocumento.text='asdasdsadas'")
        End If
    End Sub

    Protected Sub lstinformacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstinformacion.SelectedIndexChanged

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class

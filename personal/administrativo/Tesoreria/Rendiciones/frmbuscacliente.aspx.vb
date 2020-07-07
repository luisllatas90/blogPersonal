Imports System.Data

Partial Class frmbuscacliente

    Inherits System.Web.UI.Page
    Dim cn As New clsaccesodatos

    Protected Sub cmdbuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdbuscar.Click
        If Me.txtcliente.Text = "" Then
            Me.lblmensaje.Text = "Ingrese criterio de búsqueda"
            Exit Sub
        End If
        Dim dts As New DataSet
        cn.abrirconexion()
        dts = cn.consultar("sp_buscacliente", "BT", "PE", Me.txtcliente.Text.ToString.Trim & "%", "", "", "")
        cn.cerrarconexion()
        Me.lstinformacion.DataSource = dts
        Me.lstinformacion.DataBind()

        If dts.Tables("consulta").Rows.Count > 0 Then
            Me.lblmensaje.Text = dts.Tables("consulta").Rows.Count.ToString & "registros mostrados"
        Else
            Me.lblmensaje.Text = ""
        End If

    End Sub

    Protected Sub cmdaceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdaceptar.Click
        Response.Write("<script> window.opener.form1.lblcodigo_tcl.value='asas'</script>")
    End Sub
End Class

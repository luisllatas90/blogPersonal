
Partial Class frmdescuentosporplanilla
    Inherits System.Web.UI.Page
    Dim cn As New clsaccesodatos
    Sub mostrarcliente()

        Dim dts As New System.Data.DataSet
        cn.abrirconexion()
        dts = cn.consultar("sp_buscacliente", "BT", "PE", "%", "", "")
        cn.cerrarconexion()

        Me.cbocliente.DataSource = dts
        Me.cbocliente.DataTextField = "nombres"
        Me.cbocliente.DataValueField = "codigo_tcl"
        Me.cbocliente.DataBind()
        'Me.cbocliente.SelectedValue = 0
    End Sub
    Sub mostrarInformacion()
        Dim dts As New System.Data.DataSet
        cn.abrirconexion()
        If Me.chkincluircancelados.Checked = False Then
            dts = cn.consultar("dbo.sp_verdocumentoemitidos", "12", Me.cbocliente.SelectedValue, "", "", "", "", "", "")
        Else
            dts = cn.consultar("dbo.sp_verdocumentoemitidos", "13", Me.cbocliente.SelectedValue, "", "", "", "", "", "")
        End If
        cn.cerrarconexion()
        Me.lstinformacioncargos.DataSource = dts.Tables("consulta")
        Me.lstinformacioncargos.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            Me.mostrarcliente()
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdconsultar.Click
        mostrarInformacion()
    End Sub

    Protected Sub lstinformacioncargos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles lstinformacioncargos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")
            e.Row.Attributes.Add("onmouseleave", "resaltar(this,0)")
        End If
    End Sub

    Protected Sub lstinformacioncargos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstinformacioncargos.SelectedIndexChanged

    End Sub
End Class

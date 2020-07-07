
Partial Class frmverpagos
    Inherits System.Web.UI.Page
    Dim cn As New clsaccesodatos
    Public Sub mostrarinformacion(ByVal codigo_ddp As Integer)
        ' consultar por el detalle de deudapagar
        Dim dtsdetalleegreso As New System.Data.DataSet

        cn.abrirconexion()
        dtsdetalleegreso = cn.consultar("dbo.sp_verdocumentoemitidos", "MP1", codigo_ddp, "", "")
        cn.cerrarconexion()

        Me.lstinformacioncargos.DataSource = dtsdetalleegreso.Tables("consulta")
        Me.lstinformacioncargos.DataBind()

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsEncr As New EncriptaCodigos.clsEncripta, Envcodigo As String
        If Me.IsPostBack = False Then
            Envcodigo = Mid(clsEncr.Decodifica(Me.Request.QueryString("codigo_ddp")), 4)
            Me.mostrarinformacion(Envcodigo)
        End If

    End Sub

    Protected Sub lstinformacioncargos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles lstinformacioncargos.RowDataBound
        Dim CENCRIP As New EncriptaCodigos.clsEncripta
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Cells(7).Text = "Ver detalles"
            'Dim hl As New HyperLink()
            'hl.NavigateUrl = ""
            'hl.ImageUrl = "~/iconos/buscar.gif"
            'e.Row.Cells(7).Controls.Add(hl)
            'e.Row.Cells(7).Text = "~/imagenes/buscar.gif"
            e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")
            e.Row.Attributes.Add("onmouseleave", "resaltar(this,0)")
            e.Row.Cells(9).Attributes.Add("onclick", "window.open('frmdocumentoegreso.aspx?codigo_egr=" & CENCRIP.Codifica("069" & e.Row.Cells(0).Text.ToString) & "','','toolbar=no, width=1200, height=750')")
        End If
    End Sub

    Protected Sub lstinformacioncargos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstinformacioncargos.SelectedIndexChanged

    End Sub
End Class

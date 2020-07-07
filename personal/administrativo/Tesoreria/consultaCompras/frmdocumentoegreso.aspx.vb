
Partial Class frmdocumentoegreso
    Inherits System.Web.UI.Page
    Dim cn As New clsaccesodatos
    Sub mostrarinformacion(ByVal codigo_egr As Integer)
        Dim dtsegreso As New System.Data.DataSet, dtsdetalleegreso As New System.Data.DataSet
        cn.abrirconexion()
        dtsegreso = cn.consultar("sp_verdocumentoemitidos", "DECCO", codigo_egr.ToString, "", "")
        dtsdetalleegreso = cn.consultar("sp_verdocumentoemitidos", "DDE", codigo_egr.ToString, "", "")
        cn.cerrarconexion()

        With dtsegreso.Tables("consulta")
            Me.lblcliente.Text = .Rows(0).Item("nombres")
            Me.lbltipodocumento.Text = .Rows(0).Item("descripcion_tdo")
            Me.lbloperador.Text = .Rows(0).Item("usuarioreg_egr")
            Me.lblnumerodocumento.Text = .Rows(0).Item("seriedoc_egr").ToString & "-" & .Rows(0).Item("numerodoc_egr").ToString
            Me.lblfecha.Text = .Rows(0).Item("fechagen_egr")
            Me.lblfechareg.Text = .Rows(0).Item("fechareg_egr")
            Me.lblobservacion.Text = .Rows(0).Item("observacion_egr")
            Me.lblterminal.Text = .Rows(0).Item("hostreg_egr")

        End With
        'Me.lstinformacioncargos.DataSource = Nothing
        Me.lstinformacioncargos.DataSource = dtsdetalleegreso.Tables("consulta")
        Me.lstinformacioncargos.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsEncr As New EncriptaCodigos.clsEncripta, Envcodigo As String
        If Me.IsPostBack = False Then
            Envcodigo = Mid(clsEncr.Decodifica(Me.Request.QueryString("codigo_egr")), 4)
            Me.mostrarinformacion(Envcodigo)
        End If
    End Sub

    Protected Sub lstinformacioncargos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles lstinformacioncargos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")
            e.Row.Attributes.Add("onmouseleave", "resaltar(this,0)")
            

        End If
    End Sub

    Protected Sub lstinformacioncargos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstinformacioncargos.SelectedIndexChanged

    End Sub

    Protected Sub lstinformacioncargos_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles lstinformacioncargos.Sorting
        If e.SortDirection = SortDirection.Descending Then
            e.SortDirection = SortDirection.Ascending
        Else
            e.SortDirection = SortDirection.Descending
        End If


    End Sub
End Class

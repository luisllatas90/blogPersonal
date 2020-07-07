
Partial Class frmdocumentoingreso
    Inherits System.Web.UI.Page

    Dim cn As New clsaccesodatos
    Sub mostrarinformacion(ByVal codigo_ing As Integer)
        Dim dtsegreso As New System.Data.DataSet, dtsdetalleingreso As New System.Data.DataSet
        cn.abrirconexion()
        dtsegreso = cn.consultar("sp_verdocumentoemitidos", "DICCO", codigo_ing.ToString, "", "")
        dtsdetalleingreso = cn.consultar("sp_verdocumentoemitidos", "DDIIA", codigo_ing.ToString, "", "")
        cn.cerrarconexion()

        With dtsegreso.Tables("consulta")
            Me.lblcliente.Text = .Rows(0).Item("nombres")
            Me.lbltipodocumento.Text = .Rows(0).Item("descripcion_tdo")
            Me.lbloperador.Text = .Rows(0).Item("usuarioreg_ing")
            Me.lblnumerodocumento.Text = .Rows(0).Item("seriedoc_ing").ToString & "-" & .Rows(0).Item("numerodoc_ing").ToString
            Me.lblfecha.Text = .Rows(0).Item("fecha_ing")
            Me.lblfechareg.Text = .Rows(0).Item("fechareg_ing")
            Me.lblobservacion.Text = .Rows(0).Item("observacion_ing")
            Me.lblterminal.Text = .Rows(0).Item("hostreg_ing")

        End With
        'Me.lstinformacioncargos.DataSource = Nothing
        Me.lstinformacioncargos.DataSource = dtsdetalleingreso.Tables("consulta")
        Me.lstinformacioncargos.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsEncr As New EncriptaCodigos.clsEncripta, Envcodigo As String
        If Me.IsPostBack = False Then
            Envcodigo = Mid(clsEncr.Decodifica(Me.Request.QueryString("codigo_ing")), 4)
            Me.mostrarinformacion(Envcodigo)
        End If
    End Sub

    Protected Sub lstinformacioncargos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles lstinformacioncargos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")
            e.Row.Attributes.Add("onmouseleave", "resaltar(this,0)")
            If e.Row.Cells(6).Text <> "" Then
                'e.Row.Cells(11).Text = ""
            Else
                'e.Row.Cells(11).Text = "DET. Rend"
            End If

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

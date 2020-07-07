
Partial Class Usuario_ListaRequerimientos
    Inherits System.Web.UI.Page
    Public id_sol As Int16
    Public cod_per As Int32

    Protected Sub frmlistrequerimientos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmlistrequerimientos.Load
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        id_sol = CInt(Request.QueryString("id_sol"))
        cod_per = CInt(Request.QueryString("id"))
        If Not IsPostBack Then
            Dim datos As New Data.DataTable
            
            datos = Objcnx.TraerDataTable("paReq_ConsultarSolicitud", id_sol)
            If datos.Rows.Count > 0 Then
                Me.LblSolicitud.Text = datos.Rows(0).Item("descripcion_sol").ToString
                Me.LblPrioridad.Text = datos.Rows(0).Item("prioridad").ToString
                Me.LblTipo.Text = datos.Rows(0).Item("descripcion_tsol").ToString
                Me.LblArea.Text = datos.Rows(0).Item("descripcion_cco").ToString
            End If
            CmdNuevo.Attributes.Add("OnClick", "AbrirPopUp('requerimiento.aspx?ac=N&id=" & cod_per.ToString & "&id_sol=" & id_sol.ToString & "' ,'220','580')")
            GvReq.DataSource = Objcnx.TraerDataTable("paReq_ConsultarRequerimientos", id_sol)
            Me.GvReq.DataBind()
        End If

        Objcnx = Nothing
    End Sub

    Protected Sub GvReq_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvReq.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(7).Attributes.Add("OnClick", "javascript:location.href='AgregarActividades.aspx?ac=A&id_req=" & fila.Row("id_req").ToString & "&id_sol=" & id_sol.ToString & "&id=" & cod_per.ToString & "'; return false;")
            'e.Row.Cells(7).Attributes.Add("OnClick", "AbrirPopUp('AgregarActividades.aspx?ac=M&id_req=" & fila.Row("id_req").ToString & "&id_sol=" & id_sol.ToString & "&id=" & cod_per.ToString & "','220','580')")
            e.Row.Cells(8).Attributes.Add("OnClick", "AbrirPopUp('requerimiento.aspx?ac=M&id_req=" & fila.Row("id_req").ToString & "&id_sol=" & id_sol.ToString & "&id=" & cod_per.ToString & "','220','580')")
        End If
    End Sub

    Protected Sub GvReq_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GvReq.RowDeleting
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim key As New Int16
        Try
            Objcnx.IniciarTransaccion()
            Dim rpta As Int16
            rpta = 0
            'key = Me.GvReq.DataKeys.Item(Me.GvReq.SelectedIndex).Values(0).ToString()
            key = Me.GvReq.DataKeys(e.RowIndex).Value
            rpta = Objcnx.Ejecutar("paReq_EliminarRequerimiento", key, rpta) 'e.Keys.Item(0)
            Objcnx.TerminarTransaccion()
            e.Cancel = True

            Select Case rpta
                Case 1
                    Response.Write("<script>alert('Ocurrió un error al eliminar')</script>")
                Case 2
                    Response.Write("<script>alert('La eliminación se realizó correctamente')</script>")
                Case 3
                    Response.Write("<script>alert('No se pudo eliminar el requerimiento porque tiene registros relacionados')</script>")
                Case Else
                    Response.Write("<script>alert('No se pudo eliminar el requerimiento porque tiene registros relacionados')</script>")
            End Select
            Me.CmdActualizar_Click(sender, e)

        Catch ex As Exception
            Objcnx.AbortarTransaccion()
            'Response.Write("<script>alert('Ocurrio un error al procesar los datos')</script>")
            Response.Write(ex.Message)
        End Try
        Objcnx = Nothing


    End Sub

    Protected Sub LnkVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkVolver.Click
        Response.Redirect("RegistrarRequerimientos.aspx?id=" & cod_per)
    End Sub


    Protected Sub CmdActualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdActualizar.Click
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        GvReq.DataSource = Objcnx.TraerDataTable("paReq_ConsultarRequerimientos", id_sol)
        Me.GvReq.DataBind()
    End Sub

End Class


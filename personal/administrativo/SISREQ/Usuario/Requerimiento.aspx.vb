
Partial Class Usuario_Requerimiento
    Inherits System.Web.UI.Page
    Public accion As String
    Public cod_per As Int32
    Public id_sol As Int16
    Public id_req As Int16
    Protected Sub frmRequerimiento_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmRequerimiento.Load
        accion = Request.QueryString("ac")
        cod_per = CInt(Request.QueryString("id"))
        id_sol = CInt(Request.QueryString("id_sol"))
        id_req = CInt(Request.QueryString("id_req"))
        If Not IsPostBack Then
            Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ClsFunciones.LlenarListas(Me.CboTipo, Objcnx.TraerDataTable("paReq_ConsultarTipoSolicitud", "r"), "id_tsol", "descripcion_tsol", "--Seleccione Tipo--")
            Me.CboPrioridad.Enabled = False
            Me.CboPrioridad.SelectedValue = 5
            If accion = "M" Then
                Dim datos As New Data.DataTable
                datos = Objcnx.TraerDataTable("paReq_ConsultarPorRequerimiento", id_req)
                Me.TxtRequerimiento.Text = datos.Rows(0).Item("descripcion_req").ToString
                Me.CboPrioridad.SelectedValue = datos.Rows(0).Item("prioridad_req").ToString
                Me.CboTipo.SelectedValue = datos.Rows(0).Item("id_tsol").ToString
                Me.CboPrioridad.Enabled = True
            End If
            Objcnx = Nothing
        End If
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            'Objcnx.IniciarTransaccion()
            If accion = "N" Then
                Objcnx.Ejecutar("paReq_InsertarRequerimiento", Me.TxtRequerimiento.Text, Me.CboPrioridad.SelectedValue, Me.CboTipo.SelectedValue, id_sol)
            ElseIf accion = "M" Then
                Objcnx.Ejecutar("paReq_UpdateRequerimiento", Me.TxtRequerimiento.Text, Me.CboPrioridad.SelectedValue, Me.CboTipo.SelectedValue, id_req)
            End If
            'Objcnx.TerminarTransaccion()

            Response.Write("<script>alert('Se registraron correctamente los datos')</script>")
            Page.RegisterStartupScript("CERRAR", "<script>window.close();</script>")
            'Response.Redirect("ListaRequerimientos.aspx?id_sol=" & id_sol.ToString & "&id=" & cod_per.ToString)
        Catch ex As Exception
            'Objcnx.AbortarTransaccion()
            'Response.Write(ex.Message)
            Response.Write("<SCRIPT>alert('Ocurrio un error al procesar los datos' )</SCRIPT>")
        End Try
        Objcnx = Nothing
    End Sub

    Protected Sub CmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdCancelar.Click
        'Response.Redirect("ListaRequerimientos.aspx?id_sol=" & id_sol.ToString & "&id=" & cod_per.ToString)
        'Response.Redirect("ListaRequerimientos.aspx?" & Page.ClientQueryString)
        Page.RegisterStartupScript("CERRAR", "<script>window.close();</script>")
    End Sub

End Class

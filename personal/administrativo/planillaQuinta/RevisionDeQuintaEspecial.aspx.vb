
Partial Class librerianet_planillaQuinta_RevisionDeQuintaEspecial
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objpre As New ClsPresupuesto
            Dim objPlla As New clsPlanilla
            Dim objfun As New ClsFunciones
            Dim datos As New Data.DataTable

            '*** Cargar datos de Proceso presupuestal ***
            datos = objpre.ObtenerListaProcesos()
            If datos.Rows.Count > 0 Then
                objfun.CargarListas(cboPeriodoPresu, datos, "codigo_ejp", "descripcion_ejp")
            End If

            '*** Cargar Datos Planilla *** 
            datos = objPlla.ConsultarPlanilla(Request.QueryString("ctf"))
            objfun.CargarListas(cboPlanilla, datos, "codigo_plla", "PLanilla")

            ConsultarCentroCostos()
        End If
    End Sub

    Private Sub ConsultarCentroCostos()
        Dim objPlla As New clsPlanilla
        Dim datos As New Data.DataTable

        '### Cargar Datos Centro Costos ###
        datos = objPlla.ConsultarCentroCostosQuintaParaRevision(Request.QueryString("id"), cboPlanilla.SelectedValue, CByte(chkCecos.Checked))
        gvCecos.DataSource = datos
        gvCecos.DataBind()
    End Sub

    Protected Sub gvCecos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCecos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvCecos','Select$" & e.Row.RowIndex & "');")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            e.Row.Cells(0).Attributes.Add("tooltip", "seleccione algún centro de costos para visualizar el detalle")
        End If
    End Sub

    Protected Sub chkCecos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCecos.CheckedChanged
        ConsultarCentroCostos()
    End Sub

    Protected Sub gvCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCecos.SelectedIndexChanged
        'Dim obj As New ClsConectarDatos
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString        
        'Dim dt As Data.DataTable
        'obj.AbrirConexion()
        'dt = obj.TraerDataTable("PRESU_ConsultarAnexoDetallePlanillaEjecutadoPorTrabajadoryCco", Me.cboPlanilla.SelectedValue, _
        '                        Me.gvCecos.DataKeys.Item(Me.gvCecos.SelectedIndex).Values("codigo_cco"), Request.QueryString("id"))
        'obj.CerrarConexion()

        gvDetalleQuinta.DataSourceID = SqlDataSource1.ID
        gvDetalleQuinta.DataBind()
        If Request.QueryString("ctf") = 1 Then
            Me.cmdEnviar.Enabled = True
            Me.cmdRetornar.Enabled = True
        Else
            If (hddSw.Value.Trim = "") Then hddSw.Value = 1
            If hddSw.Value > 0 Then
                cmdEnviar.Enabled = False
                cmdRetornar.Enabled = False
            Else
                cmdEnviar.Enabled = True
                cmdRetornar.Enabled = True
            End If
        End If
    End Sub
 
    Protected Sub cboPlanilla_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPlanilla.SelectedIndexChanged
        ConsultarCentroCostos()
        gvDetalleQuinta.DataSourceID = SqlDataSource1.ID
        gvDetalleQuinta.DataBind()
        Me.lblRespuesta.Text = ""
    End Sub

    Protected Sub gvDetalleQuinta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalleQuinta.RowDataBound
        Dim fila As Data.DataRowView
        fila = e.Row.DataItem        
        '### verifica si hay algún estado diferente de borrador, esto para bloquear la opción de envío ###
        If e.Row.RowType = DataControlRowType.DataRow Then            
            If e.Row.RowIndex = 0 Then
                hddSw.Value = 0
            End If            
            If fila.Item("Estado").ToString.Trim <> "Registrado" Then
                hddSw.Value = hddSw.Value + 1
            End If
        End If
    End Sub

    Protected Sub cmdEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnviar.Click
        Dim objPlla As New clsPlanilla
        Dim Mensaje As String
	Dim rpta As Int32
        Try
            If gvCecos.SelectedValue > 0 Then
                rpta = objPlla.EnviarDetallePlanillaQuinta(cboPlanilla.SelectedValue, gvCecos.SelectedValue, Request.QueryString("id"))
	      If rpta = -1 Then
                Me.lblRespuesta.Text = "No se puede enviar porque ha expirado la fecha límite para enviar las quintas especiales indicada por Dirección de Personal"
              ElseIf rpta = -2 Then
                Me.lblRespuesta.Text = "No se puede enviar porque la planilla para este centro de costos ya fue enviada a Dirección de Personal"
              else
                    Mensaje = "Se ha aprobado la planilla de quinta especial del <b>" & gvCecos.SelectedRow.Cells(0).Text & "</b> para ser procesada.<br>"
                Mensaje = Mensaje & "<br> Observaciones: " & Me.txtMensaje.Text
                objPlla.EnviarCorreo(gvCecos.SelectedValue, 3, Mensaje)
                    Me.txtMensaje.Text = ""
                Me.lblRespuesta.Text = "Se enviaron correctamente los datos"
                Me.lblRespuesta.ForeColor = Drawing.Color.Blue
	      end if
            Else
                'ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('Debe seleccionar un centro de costos para ver su detalle');", True)
                Me.lblRespuesta.Text = "Debe seleccionar un centro de costos para ver su detalle"
                Me.lblRespuesta.ForeColor = Drawing.Color.Red
            End If
        Catch ex As Exception
            'ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('Ocurrió un error al guardar los datos');", True)
            Me.lblRespuesta.Text = "Ocurrió un error al registrar los datos"
            Me.lblRespuesta.ForeColor = Drawing.Color.Red
        End Try
        objPlla = Nothing
    End Sub

    Protected Sub cmdRetornar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRetornar.Click
        Dim objPlla As New clsPlanilla
        Dim Mensaje As String
	Dim rpta As Int32
        Try
            If gvCecos.SelectedValue > 0 Then
                rpta = objPlla.RegresarDetallePlanillaQuinta(cboPlanilla.SelectedValue, gvCecos.SelectedValue, Request.QueryString("id"))
              If rpta = -1 Then
                Me.lblRespuesta.Text = "No se puede retornar porque ha expirado la fecha límite para modificación de quintas especiales indicada por Dirección de Personal"
              ElseIf rpta = -2 Then
                Me.lblRespuesta.Text = "No se puede retornar porque la planilla para este centro de costos ya fue enviada a Dirección de Personal"
              else
                Mensaje = "Se ha retornado la información de planilla de quinta especial del <b>" & gvCecos.SelectedRow.Cells(0).Text & "</b> para su corrección.<br>"
                Mensaje = Mensaje & "<br> Observaciones: " & Me.txtMensaje.Text
                objPlla.EnviarCorreo(gvCecos.SelectedValue, 2, Mensaje)
                Me.txtMensaje.Text = ""
                Me.lblRespuesta.Text = "Se retornaron los datos a estado Borrador para su corrección"
                Me.lblRespuesta.ForeColor = Drawing.Color.Blue
	      End If
            Else
                'ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('Debe seleccionar un centro de costos para ver su detalle');", True)
                Me.lblRespuesta.Text = "Debe seleccionar un centro de costos para ver su detalle"
                Me.lblRespuesta.ForeColor = Drawing.Color.Red
            End If
        Catch ex As Exception
            'ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('Ocurrió un error al guardar los datos');", True)
            Me.lblRespuesta.Text = "Ocurrió un error al registrar los datos"
            Me.lblRespuesta.ForeColor = Drawing.Color.Red
        End Try
        objPlla = Nothing
    End Sub
End Class

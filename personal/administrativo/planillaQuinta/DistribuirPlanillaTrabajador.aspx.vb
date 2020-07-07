
Partial Class planillaQuinta_DistribuirPlanillaTrabajador
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objpre As New ClsPresupuesto
            Dim objPlla As New clsPlanilla
            Dim objfun As New ClsFunciones
            Dim datos As New Data.DataTable

            'Cargar Datos Planilla
            datos = objPlla.ConsultarPlanilla()
            objfun.CargarListas(cboPlanilla, datos, "codigo_plla", "PLanilla")

            'Cargar unidades de gestión
            datos = objpre.ObtenerListaCentroCostos("G", 523)
            objfun.CargarListas(Me.cboUnidadGestion, datos, "codigo_cco", "descripcion_cco", "Todos")

            'Cargar Centro de costos
            'datos = objpre.ObtenerListaCentroCostos("1", 523, Request.QueryString("id"))
            datos = objpre.ConsultaCentroCostosConPermisos(1, Request.QueryString("id"), "")
            'objfun.CargarListas(cboCecos, datos, "codigo_Cco", "descripcion_Cco", ">> Seleccione <<")
            objfun.CargarListas(cboCecos, datos, "codigo_Cco", "nombre", ">> Seleccione <<")
            MostrarBusquedaCeCos(False)

            Me.txtPorcentaje.Attributes.Add("onKeyPress", "validarnumero()")
            Me.lblCodTrabajador.Style.Add("horizontalalign", "center")

            objpre = Nothing
            objPlla = Nothing
            objfun = Nothing
            datos.Dispose()
        End If
    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Dim objpre As New ClsPresupuesto
        Dim objPlla As New clsPlanilla
        Dim objfun As New ClsFunciones
        Dim datos As New Data.DataTable

        datos = objPlla.DistribucioPlanillaTrabajador(Me.cboPlanilla.SelectedValue, Me.cboUnidadGestion.SelectedValue, txtTrabajador.Text.Trim)
        If datos.Rows.Count > 0 Then
            Me.gvTrabajador.DataSource = datos
        Else
            Me.gvTrabajador.DataSource = Nothing
        End If
        Me.gvTrabajador.DataBind()
        objpre = Nothing
        objPlla = Nothing
        objfun = Nothing
        datos.Dispose()
    End Sub

    Protected Sub gvTrabajador_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTrabajador.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            'e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvTrabajador','Select$" & e.Row.RowIndex & "');")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            e.Row.Style.Add("cursor", "hand")
        End If
    End Sub

    Protected Sub gvTrabajador_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvTrabajador.SelectedIndexChanged
        Me.lblCodTrabajador.Text = Me.gvTrabajador.DataKeys.Item(Me.gvTrabajador.SelectedIndex).Values(0)
        Me.lblTrabajador.Text = Me.gvTrabajador.SelectedRow.Cells(0).Text
        Me.lblDedicacion.Text = Me.gvTrabajador.DataKeys.Item(Me.gvTrabajador.SelectedIndex).Values(1)
        Me.lblTipo.Text = Me.gvTrabajador.DataKeys.Item(Me.gvTrabajador.SelectedIndex).Values(2)

        ConsultarDistribucion()
        Cargarfoto(IIf(Me.gvTrabajador.DataKeys.Item(Me.gvTrabajador.SelectedIndex).Values(3) Is DBNull.Value, "", Me.gvTrabajador.DataKeys.Item(Me.gvTrabajador.SelectedIndex).Values(3)))
        Me.lblMsj.Text = ""
        Me.txtPorcentaje.Text = ""
        Me.cboCecos.SelectedValue = -1
        Me.txtObservacion.Text = ""
    End Sub

    Private Sub Cargarfoto(ByVal foto As String)
        If foto <> "" Then
            '    Me.imgFoto.Visible = True
            '    Me.imgFoto.ImageUrl = "images/fotovacia.gif"
            'Else
            Me.imgFoto.Visible = True
            Me.imgFoto.ImageUrl = "../../../personal/imgpersonal/" & foto
        End If
    End Sub
    Private Sub ConsultarDistribucion()
        Dim objPlla As New clsPlanilla
        Dim objfun As New ClsFunciones
        Dim datos As New Data.DataTable
        gvDistribucion.DataSource = objPlla.ConsultarDistribucionDetallePlanilla(Me.cboPlanilla.SelectedValue, Me.lblCodTrabajador.Text)
        gvDistribucion.DataBind()
        DistribuirPorcentaje()
    End Sub

    Sub DistribuirPorcentaje()
        Dim sum As Double = 0
        Me.lblTotalItem.Text = "100%"
        For i As Int16 = 0 To Me.gvDistribucion.Rows.Count - 1
            sum = sum + gvDistribucion.Rows(i).Cells(1).Text
        Next
        Me.lblDistribuidoItem.Text = sum & "%"
        Me.lblPorDistribuir.Text = (100 - sum).ToString & "%"
    End Sub

    Protected Sub ImgBuscarCecos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarCecos.Click
        BuscarCeCos()
    End Sub

    Private Sub BuscarCeCos()
        Dim objPre As ClsPresupuesto
        objPre = New ClsPresupuesto
        gvCecos.DataSource = objPre.ConsultaCentroCostosConPermisos(1, Request.QueryString("id"), txtBuscaCecos.Text)
        gvCecos.DataBind()
        objPre = Nothing
        Panel3.Visible = True
    End Sub

    Protected Sub lnkBusquedaAvanzada_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBusquedaAvanzada.Click
        If lnkBusquedaAvanzada.Text.Trim = "Busqueda Simple" Then
            MostrarBusquedaCeCos(False)
            lnkBusquedaAvanzada.Text = "Busqueda Avanzada"
        Else
            MostrarBusquedaCeCos(True)
            lnkBusquedaAvanzada.Text = "Busqueda Simple"
        End If
    End Sub

    Private Sub MostrarBusquedaCeCos(ByVal valor As Boolean)
        Me.txtBuscaCecos.Visible = valor
        Me.ImgBuscarCecos.Visible = valor
        Me.lblTextBusqueda.Visible = valor
        Me.cboCecos.Visible = Not (valor)
    End Sub

    Protected Sub gvCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCecos.SelectedIndexChanged
        cboCecos.SelectedValue = Me.gvCecos.DataKeys.Item(Me.gvCecos.SelectedIndex).Values(0)
        MostrarBusquedaCeCos(False)
        Panel3.Visible = False
        lnkBusquedaAvanzada.Text = "Busqueda Avanzada"
    End Sub

    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        'Try
        Dim distribuido As Double
        Dim objPlla As New clsPlanilla
        Dim Rpta As Int32
        distribuido = Mid(Me.lblDistribuidoItem.Text, 1, Me.lblDistribuidoItem.Text.Length - 1)
        If hddCodigo_ddplla.Value <> "" Then
            If CDbl(distribuido) - CDbl(Me.gvDistribucion.SelectedRow.Cells(1).Text) + CDbl(Me.txtPorcentaje.Text) <= 100 Then
                'Response.Write("sdfjhjfhsjdfh")
                Rpta = objPlla.ModificarDistribucionDetallePlanilla(Me.hddCodigo_ddplla.Value, Me.cboPlanilla.SelectedValue, Me.lblCodTrabajador.Text, Me.cboCecos.SelectedValue, Me.txtPorcentaje.Text, Request.QueryString("id"), txtObservacion.Text)
                If Rpta = -1 Then
                    lblMsj.Text = "No se puede registrar porque ha expirado la fecha límite para registrar distribución de carga indicada por Dirección de Personal"
                ElseIf Rpta = -2 Then
                    lblMsj.Text = "No se puede registrar porque la planilla para este centro de costos ya fue procesada a Dirección de Personal"
                ElseIf Rpta = -3 Then
                    lblMsj.Text = "Ocurrió un error al procesar los datos"
                Else
                    Me.lblMsj.Text = "Se guardaron correctamente los datos"
                End If
            Else
                Me.lblMsj.Text = "No puede ingresar esta cantidad porque sobrepasa el 100%"
            End If
        Else
            If CDbl(distribuido) + Me.txtPorcentaje.Text <= 100 Then
                'Response.Write("aaaaaaaaaaaaaaa")
                Rpta = objPlla.RegistrarDistribucionDetallePlanilla(Me.cboPlanilla.SelectedValue, Me.lblCodTrabajador.Text, Me.cboCecos.SelectedValue, Me.txtPorcentaje.Text, Request.QueryString("id"), txtObservacion.Text)
                If Rpta = -1 Then
                    lblMsj.Text = "No se puede registrar porque ha expirado la fecha límite para registrar distribución de carga indicada por Dirección de Personal"
                ElseIf Rpta = -2 Then
                    lblMsj.Text = "No se puede registrar porque la planilla para este centro de costos ya fue procesada a Dirección de Personal"
                ElseIf Rpta = -3 Then
                    lblMsj.Text = "Ocurrió un error al procesar los datos"
                Else
                    Me.lblMsj.Text = "Se guardaron correctamente los datos"
                End If

            Else
                Me.lblMsj.Text = "No puede ingresar esta cantidad porque sobrepasa el 100%"
            End If
        End If
        'ClientScript.RegisterStartupScript(Me.GetType, "Correcto", "alert(Se guardaron correctamente los datos');", True)
        ConsultarDistribucion()
        gvDistribucion.DataBind()
        DistribuirPorcentaje()
        'Catch ex As Exception
        '    Me.lblMsj.Text = "Ocurrió un error al procesar los datos"
        '    'ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert(Ocurrió un error al procesar los datos');", True)
        'End Try
    End Sub

    Protected Sub gvDistribucion_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDistribucion.RowDeleting
        Dim objPlla As New clsPlanilla
        Dim Rpta As Int32
        Rpta = objPlla.EliminarDistribucionDetallePlanilla(Me.gvDistribucion.DataKeys.Item(e.RowIndex).Values(0))
        If Rpta = -1 Then
            lblMsj.Text = "No se puede registrar porque ha expirado la fecha límite para registrar distribución de carga indicada por Dirección de Personal"
        ElseIf Rpta = -2 Then
            lblMsj.Text = "No se puede registrar porque la planilla para este centro de costos ya fue procesada a Dirección de Personal"
        ElseIf Rpta = -3 Then
            lblMsj.Text = "Ocurrió un error al procesar los datos"
        Else
            Me.lblMsj.Text = "Se guardaron correctamente los datos"
        End If
        ConsultarDistribucion()
        gvDistribucion.DataBind()
        Me.lblMsj.Text = "Se eliminó correctamente el registro"
    End Sub

    Protected Sub gvDistribucion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDistribucion.SelectedIndexChanged
        Me.txtPorcentaje.Text = Me.gvDistribucion.SelectedRow.Cells(1).Text
        Me.txtObservacion.Text = HttpUtility.HtmlDecode(Me.gvDistribucion.SelectedRow.Cells(2).Text)
        Me.cboCecos.SelectedValue = Me.gvDistribucion.DataKeys.Item(Me.gvDistribucion.SelectedIndex).Values(1)
        Me.hddCodigo_ddplla.Value = Me.gvDistribucion.DataKeys.Item(Me.gvDistribucion.SelectedIndex).Values(0)
        ConsultarDistribucion()
        Me.lblMsj.Text = ""
    End Sub


End Class

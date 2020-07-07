
Partial Class indicadores_frmPanelEvaluacionAnualPlan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargarPlanesRegistrados()
                CargarCentroCosto()
                CargarListaEvaluaciones()
                hfCodigo_eval.Value = 0
                'ListaPlanesInformes()
                'btnAgregar.Attributes.Add("language", "javascript")
                'btnAgregar.Attributes.Add("OnClick", "return confirm('Esta seguro?');")
                lnkEvaluacion.ForeColor = Drawing.Color.Blue
                lnkAccesoInformes.ForeColor = Drawing.Color.Black

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ListaPlanesInformes()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            gvListaPlanesDocs.DataSource = Nothing
            gvListaPlanesDocs.DataBind()

            If ddlPersonalAcceso.SelectedValue <> 0 Then
                dts = obj.ListaPlanesInformes(ddlPersonalAcceso.SelectedValue)
                If dts.Rows.Count > 0 Then
                    gvListaPlanesDocs.DataSource = dts
                    gvListaPlanesDocs.DataBind()
                End If
            Else
                gvListaPlanesDocs.DataSource = Nothing
                gvListaPlanesDocs.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarListaEvaluaciones()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListaEvaluaciones
            If dts.Rows.Count > 0 Then
                gvLista.DataSource = dts
                gvLista.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarCentroCosto()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ConsultarCentroCostosEvaluacion
            ddlCentroCosto.DataSource = dts
            ddlCentroCosto.DataTextField = "descripcion_Cco"
            ddlCentroCosto.DataValueField = "codigo_cco"
            ddlCentroCosto.DataBind()

            ddlCentroCosto.SelectedValue = 1
            CargarListaTrabajadores(ddlCentroCosto.SelectedValue)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarListaTrabajadores(ByVal codigo_ceco As Integer)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            'dts = obj.ConsultarPersonalDirectorPersonal_v2("%", 1)

            dts = obj.ConsultarPersonalEvaluacion("%", codigo_ceco)
            ddlResponsable.DataSource = dts
            ddlResponsable.DataTextField = "personal"
            ddlResponsable.DataValueField = "codigo_per"
            ddlResponsable.DataBind()

            'LLena el combo para los accesos a los informes.
            ddlPersonalAcceso.DataSource = dts
            ddlPersonalAcceso.DataTextField = "personal"
            ddlPersonalAcceso.DataValueField = "codigo_per"
            ddlPersonalAcceso.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarPlanesRegistrados()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarPlanesRegistrados(Me.Request.QueryString("ctf"), Me.Request.QueryString("id"))
            ddlPlan.DataSource = dts
            ddlPlan.DataTextField = "descripcion"
            ddlPlan.DataValueField = "codigo"
            ddlPlan.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarListaAñosPlan(ByVal Codigo_plan As Integer)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            If ddlPlan.SelectedValue <> 0 Then
                dts = obj.ListaAñosEvaluacion(Codigo_plan)
                ddlAño.DataSource = dts
                ddlAño.DataTextField = "descripcion"
                ddlAño.DataValueField = "codigo"
                ddlAño.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub ddlPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlan.SelectedIndexChanged
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            If ddlPlan.SelectedValue <> 0 Then
                CargarListaAñosPlan(ddlPlan.SelectedValue)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlCentroCosto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCentroCosto.SelectedIndexChanged
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            If ddlCentroCosto.SelectedValue <> 0 Then
                dts = obj.ConsultarPersonalEvaluacion("%", ddlCentroCosto.SelectedValue)
                ddlResponsable.DataSource = dts
                ddlResponsable.DataTextField = "personal"
                ddlResponsable.DataValueField = "codigo_per"
                ddlResponsable.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function validacionesAcceso() As Boolean
        Try
            Dim obj As New clsInvestigacion
            Dim dts As New Data.DataTable
            Dim Fila As GridViewRow
            Dim marcados As Int16 = 0
            Dim sw As Byte = 0

            '--#Validamos que haya seleccionado el trabajador que visualizara los informes del plan.
            If ddlPersonalAcceso.SelectedValue = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de seleccionar el responsable.');", True)
                ddlPersonalAcceso.Focus()
                Exit Function
            End If

            '--# Validamos que se haya marcado por lo menos una fila
            For I As Int16 = 0 To gvListaPlanesDocs.Rows.Count - 1
                Fila = Me.gvListaPlanesDocs.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        marcados = marcados + 1
                    End If
                End If
            Next

            If marcados = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Favor de marcar con un check los planes a cuyos informes tendrá acceso el responsable seleccionado.');", True)
                Exit Function
            End If

            '--# Salida de la Validacion:
            sw = 1
            If (sw = 1) Then
                Return True
            End If
            Return False

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Function validaciones() As Boolean
        Dim obj As New clsInvestigacion
        Dim dts As New Data.DataTable
        Dim sw As Byte = 0

        If ddlPlan.SelectedValue = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de seleccionar el Plan Estratégico.');", True)
            ddlPlan.Focus()
            Exit Function
        End If

        If txtFechaInicio.Text.Trim = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de ingresar una fecha de inicio');", True)
            txtFechaInicio.Focus()
            Exit Function
        Else
            If Not IsDate(txtFechaInicio.Text) Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de ingresar una fecha válida para la fecha de inicio');", True)
                txtFechaInicio.Focus()
                Exit Function
            End If
        End If

        If txtFechaFin.Text.Trim = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de ingresar una fecha final');", True)
            txtFechaFin.Focus()
            Exit Function
        Else
            If Not IsDate(txtFechaFin.Text) Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de ingresar una fecha válida para la fecha final');", True)
                txtFechaFin.Focus()
                Exit Function
            End If
        End If

        If IsDate(txtFechaInicio.Text) And IsDate(txtFechaFin.Text) Then
            If CDate(txtFechaInicio.Text) >= CDate(txtFechaFin.Text) Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La fecha inicial no puede ser mayor o igual a la fecha final, favor de corregir');", True)
                txtFechaInicio.Focus()
                Exit Function
            End If
        End If

        If ddlAño.SelectedValue = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de seleccionar un año para el Plan Estratégico Seleccionado.');", True)
            ddlAño.Focus()
            Exit Function
        End If



        If ddlResponsable.SelectedValue = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de seleccionar el responsable de adjuntar el documento para el Plan.');", True)
            ddlResponsable.Focus()
            Exit Function
        End If

      

        sw = 1
        If (sw = 1) Then
            Return True
        End If
        Return False
    End Function

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            'btnAgregar.Text = "Hecho."

            If validaciones() = False Then
                Exit Sub
            End If

            If hfCodigo_eval.Value = 0 Then
                '------------------------
                ' Registro
                '------------------------
                dts = obj.RegistrarEvaluacionesPlan(Me.ddlPlan.SelectedValue, Me.txtFechaInicio.Text, Me.txtFechaFin.Text, Me.ddlAño.SelectedValue, Me.ddlResponsable.SelectedValue, Me.Request.QueryString("id"))
                If (dts.Rows(0).Item("rpt") > 0) Then
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Registro guardado correctamente.');", True)
                    LimpiaControles()
                    EstadoControles()
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Error al guardar, verificar que el año no se encuentre registrado para el periodo seleccionado.');", True)
                    ddlAño.Focus()
                End If
            Else
                '------------------------
                ' Modificacion
                '------------------------
                dts = obj.ModificarEvaluacionesPlan(hfCodigo_eval.Value, ddlPlan.SelectedValue, Me.txtFechaInicio.Text, Me.txtFechaFin.Text, Me.ddlAño.SelectedValue, Me.ddlResponsable.SelectedValue, Me.Request.QueryString("id"))
                Select Case dts.Rows(0).Item("rpt")
                    Case 1
                        ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Registro fue actualizado correctamente.');", True)
                        LimpiaControles()
                        EstadoControles()
                        ddlPlan.Enabled = True
                    Case 0
                        ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Error, no se pudo actualizar el registro.');", True)
                    Case -1
                        ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Error, ya se encuentra registrado este año para el periodo.');", True)
                End Select
            End If
            CargarListaEvaluaciones()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EstadoControles()
        Try
            btnAgregar.Text = "     Agregar"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiaControles()
        Try
            ddlPlan.SelectedValue = 0
            ddlAño.SelectedValue = 0
            txtFechaFin.Text = ""
            txtFechaInicio.Text = ""
            ddlCentroCosto.SelectedValue = 1
            ddlResponsable.SelectedValue = 0
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvLista.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion   
                Dim seleccion As GridViewRow
                Dim codigopdo_seleccion As String

                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                hfCodigo_eval.Value = Convert.ToString(gvLista.DataKeys(seleccion.RowIndex).Values("codigo_eval"))
                codigopdo_seleccion = Convert.ToString(gvLista.DataKeys(seleccion.RowIndex).Values("codigo_eval"))
                ddlPlan.SelectedValue = Convert.ToInt32(gvLista.DataKeys(seleccion.RowIndex).Values("codigo_plan"))
                'Cargamos los años--------------------------------------------------------------------------------------------
                CargarListaAñosPlan(Convert.ToInt32(gvLista.DataKeys(seleccion.RowIndex).Values("codigo_plan")))
                '-------------------------------------------------------------------------------------------------------------
                ddlResponsable.SelectedValue = Convert.ToInt32(gvLista.DataKeys(seleccion.RowIndex).Values("responsable_eval"))
                ddlAño.SelectedValue = HttpUtility.HtmlDecode(gvLista.Rows(seleccion.RowIndex).Cells.Item(2).Text)
                txtFechaInicio.Text = HttpUtility.HtmlDecode(gvLista.Rows(seleccion.RowIndex).Cells.Item(3).Text).ToString
                txtFechaFin.Text = HttpUtility.HtmlDecode(gvLista.Rows(seleccion.RowIndex).Cells.Item(4).Text).ToString
                'Cambio nombre del boton Guardar por Modificar

                ddlPlan.Enabled = False
                btnAgregar.Text = "       Modificar"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvLista.RowDeleting
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.EliminarEvaluacionPlan(Me.gvLista.DataKeys(e.RowIndex).Value)
            If (dts.Rows(0).Item("rpt") > 0) Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('El registro fue eliminado correctamente.');", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Error, no pudo eliminar el registro.');", True)
            End If
            CargarListaEvaluaciones()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub cmdEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEliminar.Click
        Try
            Dim obj As New clsIndicadores
            Dim Fila As GridViewRow
            Dim marcados As Integer

            For I As Int16 = 0 To gvListaResponsableDocs.Rows.Count - 1
                Fila = Me.gvListaResponsableDocs.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        marcados = marcados + 1
                    End If
                End If
            Next

            If marcados = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Favor de marcar con un check los planes asignados al responsable que desea eliminar.');", True)
                Exit Sub
            End If

            For I As Int16 = 0 To gvListaResponsableDocs.Rows.Count - 1
                Fila = gvListaResponsableDocs.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        obj.EliminarControlAccesoEvaluacion(gvListaResponsableDocs.DataKeys.Item(Fila.RowIndex).Values("codigo_cae"), Me.Request.QueryString("id"))
                    End If
                End If
            Next
            ConsultaAsignacionesResponsables()
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('El registro fue eliminado correctamente.');", True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    
    Protected Sub btnAgregarDocs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarDocs.Click
        Try
            Dim obj As New clsIndicadores
            Dim Fila As GridViewRow
            If validacionesAcceso() = False Then
                Exit Sub
            End If
            'Registro
            For I As Int16 = 0 To gvListaPlanesDocs.Rows.Count - 1
                Fila = gvListaPlanesDocs.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        obj.RegistrarControlAccesoEvaluacion(gvListaPlanesDocs.DataKeys.Item(Fila.RowIndex).Values("codigo_eval"), ddlPersonalAcceso.SelectedValue, Me.Request.QueryString("id"))
                    End If
                End If
            Next
            ListaPlanesInformes()
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Registro guardado correctamente.');", True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlPersonalAcceso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPersonalAcceso.SelectedIndexChanged
        Try
            ListaPlanesInformes()
            ConsultaAsignacionesResponsables()
            'MostrarLnkConsultaAsignaciones()
            MostrarLnkAsignaciones()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ConsultaAsignacionesResponsables()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            If ddlPersonalAcceso.SelectedValue <> 0 Then
                dts = obj.ListaPlanesAsigandosResponsable(Me.ddlPersonalAcceso.SelectedValue)
                gvListaResponsableDocs.DataSource = dts
                gvListaResponsableDocs.DataBind()
            Else
                gvListaResponsableDocs.DataSource = Nothing
                gvListaResponsableDocs.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkConsultaAsignaciones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkConsultaAsignaciones.Click
        Try
            If ddlPersonalAcceso.SelectedValue = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione un Responsable.');", True)
                ddlPersonalAcceso.Focus()
                Exit Sub
            End If

            MostrarLnkConsultaAsignaciones()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkAsignaciones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAsignaciones.Click
        Try
            If ddlPersonalAcceso.SelectedValue = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione un Responsable.');", True)
                ddlPersonalAcceso.Focus()
                Exit Sub
            End If

            ListaPlanesInformes()
            MostrarLnkAsignaciones()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub MostrarLnkConsultaAsignaciones()
        Try
            lnkConsultaAsignaciones.ForeColor = Drawing.Color.Blue
            lnkAsignaciones.ForeColor = Drawing.Color.Black
            pnlAsignaciones.Visible = False
            pnlConsultaAsignaciones.Visible = True
            ConsultaAsignacionesResponsables()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub MostrarLnkAsignaciones()
        Try
            lnkConsultaAsignaciones.ForeColor = Drawing.Color.Black
            lnkAsignaciones.ForeColor = Drawing.Color.Blue
            pnlAsignaciones.Visible = True
            pnlConsultaAsignaciones.Visible = False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

   
    Protected Sub lnkAccesoInformes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAccesoInformes.Click
        Try
            MostrarLnkAccesoInformes()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub MostrarLnkAccesoInformes()
        Try
            lnkEvaluacion.ForeColor = Drawing.Color.Black
            lnkAccesoInformes.ForeColor = Drawing.Color.Blue


            pnlEvaluacion.Visible = False
            pnlVistaDocs.Visible = True

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkEvaluacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEvaluacion.Click
        Try
            MostarLnkEvaluacion()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub MostarLnkEvaluacion()
        Try
            lnkEvaluacion.ForeColor = Drawing.Color.Blue
            lnkAccesoInformes.ForeColor = Drawing.Color.Black

            pnlEvaluacion.Visible = True
            pnlVistaDocs.Visible = False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class



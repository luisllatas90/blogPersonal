
Partial Class Indicadores_Formularios_frmMantenimientoPlanPerspectiva
    Inherits System.Web.UI.Page

    Dim usuario As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Agrego Hcano : 29-09-17
        'Expirar Sesion
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        '
        Try
            If Not IsPostBack Then
                'Configuraciones iniciales referente a la Perspectiva
                If rbtPerspectiva.Checked = True Then
                    gvRegistroPlan.Visible = False
                    gvRegistrosPers.Visible = True
                    ConsultarGridRegistroPerspectivas("%")
                End If

                'Configuraciones iniciales referente al Plan
                'CargarComboCentroCostos()
                If rbtPlan.Checked = True Then
                    gvRegistroPlan.Visible = True
                    gvRegistrosPers.Visible = False
                    ConsultarGridRegistroPlanes("%")
                End If

                '### Verificamos si existe un plan activo ###
                VerificaPlanActivo()
                '---------------/


            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub VerificaPlanActivo()
        Dim obj As New clsIndicadores
        Dim dts As New Data.DataTable
        dts = obj.AdvertenciaPlanVigente()
        If dts.Rows.Count > 0 Then
            If dts.Rows(0).Item("rpt").ToString = "1" Then
                lblMensajePlan.Visible = True
                lblMensajePlan.ForeColor = Drawing.Color.Red
                lblMensajePlan.Text = dts.Rows(0).Item("Mensaje").ToString
            Else
                lblMensajePlan.Visible = False
            End If
        End If
    End Sub

    Private Sub ConsultarGridRegistroPlanes(ByVal vParametro As String)
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores
            dts = obj.ConsultarPlanes(vParametro)
            'Response.Write(dts.Rows.Count)
            gvRegistroPlan.DataSource = dts
            gvRegistroPlan.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub ConsultarGridRegistroPerspectivas(ByVal vParametro As String)
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores
            dts = obj.ConsultarPerspectivas(vParametro)
            'Response.Write(dts.Rows.Count)
            gvRegistrosPers.DataSource = dts
            gvRegistrosPers.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub cmdGuardarPers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarPers.Click
        Try
            Dim dts As New Data.DataTable
            Dim dtsv As New Data.DataTable
            Dim obj As New clsIndicadores

            lblMensajePers.Visible = True

            If Page.IsValid Then        'Si la pagina no tiene errores, ingresa.
                'Nuevo registro
                If lblCodigopers.Text = "" Then
                    dtsv = obj.ValidarDuplicadoPerspectiva(txtDescripcionPers.Text.Trim.ToUpper.ToString, 0, "N", Me.txtAbreviatura.Text.ToUpper.ToString.Trim)
                    If dtsv.Rows(0).Item("rpt").ToString = "1" Then
                        lblMensajePers.ForeColor = Drawing.Color.Black
                        lblMensajePers.Text = dtsv.Rows(0).Item("Mensaje").ToString
                        Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                        Me.avisos.Attributes.Add("class", "mensajeError")
                        Exit Sub
                    End If
                    dts = obj.InsertarPerspectiva(txtDescripcionPers.Text.Trim.ToUpper.ToString, Request.QueryString("id"), Me.txtAbreviatura.Text.ToUpper.ToString.Trim, Me.txtColor.Text.Trim)
                Else
                    'Edicion de registro
                    dtsv = obj.ValidarDuplicadoPerspectiva(txtDescripcionPers.Text.Trim.ToUpper.ToString, CType(lblCodigopers.Text.Trim, Integer), "M", Me.txtAbreviatura.Text.ToUpper.ToString.Trim)
                    If dtsv.Rows(0).Item("rpt").ToString = "1" Then
                        lblMensajePers.ForeColor = Drawing.Color.Black
                        lblMensajePers.Text = dtsv.Rows(0).Item("Mensaje").ToString
                        Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                        Me.avisos.Attributes.Add("class", "mensajeError")
                        Exit Sub
                    End If
                    dts = obj.ModificarPerspectiva(CType(lblCodigopers.Text.Trim, Integer), txtDescripcionPers.Text.Trim.ToUpper.ToString, Request.QueryString("id"), Me.txtAbreviatura.Text.ToUpper.ToString.Trim, Me.txtColor.Text.Trim)
                    'RestablecerEstadoPlan()
                    RestablecerEstado()
                End If


                If dts.Rows(0).Item("rpt").ToString = "1" Then
                    lblMensajePers.Visible = True
                    lblMensajePers.ForeColor = Drawing.Color.Black
                    lblMensajePers.Text = dts.Rows(0).Item("Mensaje").ToString
                    Me.Image1.Attributes.Add("src", "../Images/accept.png")
                    Me.avisos.Attributes.Add("class", "mensajeExito")
                Else
                    lblMensajePers.Visible = True
                    lblMensajePers.ForeColor = Drawing.Color.Black
                    lblMensajePers.Text = dts.Rows(0).Item("Mensaje").ToString
                    Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                    Me.avisos.Attributes.Add("class", "mensajeError")
                End If

                'Cargamos la lista de registros en el gridview 
                gvRegistroPlan.Visible = False
                gvRegistrosPers.Visible = True
                ConsultarGridRegistroPerspectivas("%")
                LimpiarCamposPerspectiva()

                rbtPlan.Checked = False
                rbtPerspectiva.Checked = True

            Else 'Limpia la cadena inválida de la caja de texto
                txtDescripcionPers.Text = ""
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub RestablecerEstado()
        Try
            lblCodigopers.Text = ""
            cmdGuardarPers.Text = "   Guardar"
            txtColor.Text = ""
            txtColor.BackColor = Drawing.Color.White
            txtAbreviatura.Enabled = True
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub RestablecerEstadoPlan()
        Try
            lblCodigoPlan.Text = ""
            'ddlCentroCosto.SelectedValue = 0
            cmdGuardarPlan.Text = "   Guardar"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarCamposPerspectiva()
        Try
            txtAbreviatura.Text = ""
            txtDescripcionPers.Text = ""
            txtColor.Text = ""
            txtColor.BackColor = Drawing.Color.White
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvRegistrosPers_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvRegistrosPers.PageIndexChanging
        Try
            gvRegistrosPers.PageIndex = e.NewPageIndex
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            gvRegistroPlan.Visible = False
            gvRegistrosPers.Visible = True
            dts = obj.ConsultarPerspectivas(txtBuscar.Text.Trim.ToString)
            gvRegistrosPers.DataSource = dts
            gvRegistrosPers.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvRegistros_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvRegistrosPers.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion   
                Dim seleccion As GridViewRow
                Dim codigopers_seleccion As Integer
                'lblCodigo.Visible = True            'Solo para pruebas, debe estar oculto 

                '1. Obtengo la linea del gridview que fue cliqueada
                'Response.Write(codigoobj_seleccion)
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                codigopers_seleccion = Convert.ToInt32(gvRegistrosPers.DataKeys(seleccion.RowIndex).Values("Codigo"))


                '2. Obtengo el datakey de la linea que donde está el boton que cliqueé
                'lblCodigo.Text = gvVariable.DataKeys(seleccion.RowIndex).Value.ToString
                lblCodigopers.Text = codigopers_seleccion

                'Recuperamos los valores de las celas de la fila seleccionada, para mostrarlos en los controles
                txtDescripcionPers.Text = HttpUtility.HtmlDecode(gvRegistrosPers.Rows(seleccion.RowIndex).Cells.Item(4).Text)
                txtColor.Text = HttpUtility.HtmlDecode(gvRegistrosPers.Rows(seleccion.RowIndex).Cells.Item(3).Text)
                txtColor.BackColor = System.Drawing.ColorTranslator.FromHtml(HttpUtility.HtmlDecode(gvRegistrosPers.Rows(seleccion.RowIndex).Cells.Item(3).Text))
                txtAbreviatura.Text = Convert.ToString(gvRegistrosPers.DataKeys(seleccion.RowIndex).Values("abreviatura"))

                'Cambio nombre del boton Guardar por Modificar
                cmdGuardarPers.Text = "    Modificar"
                lblMensajePers.Text = ""
                Me.txtAbreviatura.Enabled = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvRegistros_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvRegistrosPers.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                If e.Row.Cells(3).Text.Trim <> "" Then
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(e.Row.Cells(3).Text)
                Else
                    e.Row.BackColor = Drawing.Color.White
                End If

                'Asignar un numero correlativo.
                e.Row.Cells(0).Text = e.Row.RowIndex + 1
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvRegistros_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvRegistrosPers.RowDeleting
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            Dim dtsV As New Data.DataTable

            'Validamos: Si la perspectiva esta configurada en PlanPerspectiva y esta activa no podra eliminarse.

            dtsV = obj.ValidarEliminacionPerspectiva(gvRegistrosPers.DataKeys(e.RowIndex).Value)
            If dtsV.Rows.Count > 0 Then
                If dtsV.Rows(0).Item("Mensaje").ToString <> "" Then
                    lblMensajePers.Visible = True
                    lblMensajePers.ForeColor = Drawing.Color.Black
                    lblMensajePers.Text = dtsV.Rows(0).Item("Mensaje").ToString
                    Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                    Me.avisos.Attributes.Add("class", "mensajeError")
                    Exit Sub
                End If
            End If


            'Si cumple con la validacion anterior, pasa a cambiar de esta la perspectiva
            dts = obj.EliminarPerspectiva(gvRegistrosPers.DataKeys(e.RowIndex).Value.ToString())

            If dts.Rows(0).Item("rpt").ToString = "1" Then
                lblMensajePers.Visible = True
                lblMensajePers.ForeColor = Drawing.Color.Black
                lblMensajePers.Text = dts.Rows(0).Item("Mensaje").ToString
                Me.Image1.Attributes.Add("src", "../Images/accept.png")
                Me.avisos.Attributes.Add("class", "mensajeExito")
            Else
                lblMensajePers.Visible = True
                lblMensajePers.ForeColor = Drawing.Color.Black
                lblMensajePers.Text = dts.Rows(0).Item("Mensaje").ToString
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
            End If

            'Consultamos la lista de registros
            ConsultarGridRegistroPerspectivas("%")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdGuardarPlan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarPlan.Click
        Try
            Dim dts As New Data.DataTable
            Dim dtsv As New Data.DataTable
            Dim obj As New clsIndicadores
            Dim EstadoVigencia As String = "0"

            lblMensajePlan.Visible = True
            'lblMensajePlan.ForeColor = Drawing.Color.Green

            If Page.IsValid Then        'Si la pagina no tiene errores, ingresa.

                ''Aqui validamos:
                'If Me.txtAbreviatura_pla.Text.Trim.ToString = "" Then
                '    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de registrar la abreviatura del plan, que se utilizará para el encabezado del informe.');", True)
                '    Exit Sub
                'End If

                If chkVigencia.Checked = True Then EstadoVigencia = "1"
                If chkVigencia.Checked = False Then EstadoVigencia = "0"

                'Nuevo registro
                If lblCodigoPlan.Text = "" Then
                    'Response.Write("Entra Insertar")
                    '--## Validacion de la descripcion y fechas de inicio y fin
                    'dtsv = obj.ValidarRangoFechaPlan(Me.txtDescripcionplan.Text.Trim.ToUpper.ToString, Me.txtFechaInicio.Text.Trim, Me.txtFechaFin.Text.Trim, "N", 0)
                    If dtsv.Rows.Count > 0 Then
                        lblMensajePlan.ForeColor = Drawing.Color.Black
                        lblMensajePlan.Text = dtsv.Rows(0).Item("Mensaje").ToString
                        Me.Image2.Attributes.Add("src", "../Images/exclamation.png")
                        Me.avisos2.Attributes.Add("class", "mensajeError")
                        Exit Sub
                    End If

                    '---### Insercion de nuevos registros ---###

                    'Response.Write("Abr:->> " & Me.txtAbreviatura_pla.Text.ToString.Trim)
                    'Response.Write("<br />")

                    dts = obj.InsertarPlan(Me.txtDescripcionplan.Text.Trim.ToUpper.ToString, _
                                           EstadoVigencia, Me.txtFechaInicio.Text.Trim, _
                                           Me.txtFechaFin.Text.Trim, Request.QueryString("id"), _
                                           Me.txtAbreviatura_pla.Text.ToString.Trim)
                    RestablecerEstadoPlan()
                Else
                    '--## Validacion de la descripcion y fechas de inicio y fin
                    'Response.Write("Entra Modificar")
                    'dtsv = obj.ValidarRangoFechaPlan(Me.txtDescripcionplan.Text.Trim.ToUpper.ToString, Me.txtFechaInicio.Text.Trim, Me.txtFechaFin.Text.Trim, "M", CType(lblCodigoPlan.Text.Trim, Integer))
                    If dtsv.Rows.Count > 0 Then
                        lblMensajePlan.Visible = True
                        lblMensajePlan.ForeColor = Drawing.Color.Black
                        lblMensajePlan.Text = dtsv.Rows(0).Item("Mensaje").ToString
                        Me.Image2.Attributes.Add("src", "../Images/exclamation.png")
                        Me.avisos2.Attributes.Add("class", "mensajeError")
                        Exit Sub
                    End If
                    'Edicion de registro

                    'Response.Write("Abr:->> " & Me.txtAbreviatura_pla.Text.ToString.Trim)
                    'Response.Write("<br />")

                    dts = obj.ModificarPlan(CType(lblCodigoPlan.Text.Trim, Integer), _
                                            txtDescripcionplan.Text.ToString.ToUpper, _
                                            EstadoVigencia, txtFechaInicio.Text.Trim, _
                                            txtFechaFin.Text.Trim, Request.QueryString("id"), _
                                            Me.txtAbreviatura_pla.Text.ToString.Trim)

                    RestablecerEstadoPlan()
                End If

                If dts.Rows(0).Item("rpt").ToString = "1" Then
                    lblMensajePlan.Visible = True
                    lblMensajePlan.ForeColor = Drawing.Color.Black
                    lblMensajePlan.Text = dts.Rows(0).Item("Mensaje").ToString
                    Me.Image2.Attributes.Add("src", "../Images/accept.png")
                    Me.avisos2.Attributes.Add("class", "mensajeExito")
                ElseIf dts.Rows(0).Item("rpt").ToString = "0" Then
                    lblMensajePlan.Visible = True
                    lblMensajePlan.ForeColor = Drawing.Color.Black
                    lblMensajePlan.Text = dts.Rows(0).Item("Mensaje").ToString
                    Me.Image2.Attributes.Add("src", "../Images/exclamation.png")
                    Me.avisos2.Attributes.Add("class", "mensajeError")
                ElseIf dts.Rows(0).Item("rpt").ToString = "2" Then
                    lblMensajePlan.Visible = True
                    lblMensajePlan.ForeColor = Drawing.Color.Black
                    lblMensajePlan.Text = dts.Rows(0).Item("Mensaje").ToString
                    Me.Image2.Attributes.Add("src", "../Images/exclamation.png")
                    Me.avisos2.Attributes.Add("class", "mensajeError")
                End If

                'Cargamos la lista de registros en el gridview 
                gvRegistroPlan.Visible = True
                gvRegistrosPers.Visible = False
                ConsultarGridRegistroPlanes("%")
                LimpiarCamposPlan()


                rbtPlan.Checked = True
                rbtPerspectiva.Checked = False

                '### Verificamos que exista un plan activo 
                'VerificaPlanActivo()
            Else 'Limpia la cadena inválida de la caja de texto
                txtDescripcionPers.Text = ""
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarCamposPlan()
        Try
            txtDescripcionplan.Text = ""
            txtFechaInicio.Text = ""
            txtFechaFin.Text = ""
            chkVigencia.Checked = False
            txtAbreviatura_pla.Text = ""
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvRegistroPlan_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvRegistroPlan.PageIndexChanging
        Try
            gvRegistroPlan.PageIndex = e.NewPageIndex
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            gvRegistroPlan.Visible = True
            gvRegistrosPers.Visible = False
            dts = obj.ConsultarPlanes(txtBuscar.Text.Trim.ToString)
            gvRegistroPlan.DataSource = dts
            gvRegistroPlan.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvRegistroPlan_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvRegistroPlan.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion  

                'Limpiamos el Fondo de los mensajes de aviso.
                Me.Image2.Attributes.Add("src", "../Images/beforelastchild.GIF")
                Me.avisos2.Attributes.Add("class", "none")


                Dim seleccion As GridViewRow
                Dim codigopla_seleccion As Integer
                'lblCodigo.Visible = True            'Solo para pruebas, debe estar oculto 

                '1. Obtengo la linea del gridview que fue cliqueada
                'Response.Write(codigoobj_seleccion)
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                codigopla_seleccion = Convert.ToInt32(gvRegistroPlan.DataKeys(seleccion.RowIndex).Values("Codigo"))


                '2. Obtengo el datakey de la linea que donde está el boton que cliqueé
                'lblCodigo.Text = gvVariable.DataKeys(seleccion.RowIndex).Value.ToString
                lblCodigoPlan.Text = codigopla_seleccion

                'Recuperamos los valores de las celas de la fila seleccionada, para mostrarlos en los controles
                txtDescripcionplan.Text = HttpUtility.HtmlDecode(gvRegistroPlan.Rows(seleccion.RowIndex).Cells.Item(2).Text)

                If HttpUtility.HtmlDecode(gvRegistroPlan.Rows(seleccion.RowIndex).Cells.Item(3).Text).ToString = "Si" Then Me.chkVigencia.Checked = True : chkVigencia.Text = "Activo"
                If HttpUtility.HtmlDecode(gvRegistroPlan.Rows(seleccion.RowIndex).Cells.Item(3).Text).ToString = "No" Then Me.chkVigencia.Checked = False : chkVigencia.Text = "Inactivo"

                txtFechaInicio.Text = HttpUtility.HtmlDecode(gvRegistroPlan.Rows(seleccion.RowIndex).Cells.Item(4).Text)
                txtFechaFin.Text = HttpUtility.HtmlDecode(gvRegistroPlan.Rows(seleccion.RowIndex).Cells.Item(5).Text)

                txtAbreviatura_pla.Text = HttpUtility.HtmlDecode(gvRegistroPlan.Rows(seleccion.RowIndex).Cells.Item(7).Text).Trim.ToString


                'Apuntamos al codigo, recuperado en el grid oculto
                'ddlCentroCosto.SelectedValue = Convert.ToInt32(gvRegistroPlan.DataKeys(seleccion.RowIndex).Values("Codigo_cco"))
                'Cambio nombre del boton Guardar por Modificar
                cmdGuardarPlan.Text = "    Modificar"
                lblMensajePlan.Text = ""
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvRegistroPlan_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvRegistroPlan.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim fila As Data.DataRowView
            'fila = e.Row.DataItem
            'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            'e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

            e.Row.Cells(1).Width = 20
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub


    Protected Sub gvRegistroPlan_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvRegistroPlan.RowDeleting
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            Dim dtsV As New Data.DataTable

            'Validacion1: Valida que el plan no se encuentre configurado en la tabla PlanPerspectiva y tenga estado 1
            dtsV = obj.ValidaEliminacionPlan(gvRegistroPlan.DataKeys(e.RowIndex).Value)
            If dtsV.Rows.Count > 0 Then
                If dtsV.Rows(0).Item("Mensaje").ToString <> "" Then
                    lblMensajePlan.Visible = True
                    lblMensajePlan.ForeColor = Drawing.Color.Black
                    lblMensajePlan.Text = dtsV.Rows(0).Item("Mensaje").ToString
                    Me.Image2.Attributes.Add("src", "../Images/exclamation.png")
                    Me.avisos2.Attributes.Add("class", "mensajeError")
                    Exit Sub
                End If
            End If

            'Validacion2: Un plan Activo no puede ser Eliminado
            dtsV = obj.ValidaEliminacionPlanActivo(gvRegistroPlan.DataKeys(e.RowIndex).Value)
            If dtsV.Rows.Count > 0 Then
                If dtsV.Rows(0).Item("Mensaje").ToString <> "" Then
                    lblMensajePlan.Visible = True
                    lblMensajePlan.ForeColor = Drawing.Color.Black
                    lblMensajePlan.Text = dtsV.Rows(0).Item("Mensaje").ToString
                    Me.Image2.Attributes.Add("src", "../Images/exclamation.png")
                    Me.avisos2.Attributes.Add("class", "mensajeError")
                    Exit Sub
                End If
            End If

            'Elimina, siempre y cuando cumpla con la validacion anterior.
            dts = obj.EliminarPlan(gvRegistroPlan.DataKeys(e.RowIndex).Value.ToString())

            If dts.Rows(0).Item("rpt").ToString = "1" Then
                lblMensajePlan.Visible = True
                lblMensajePlan.ForeColor = Drawing.Color.Black
                lblMensajePlan.Text = dts.Rows(0).Item("Mensaje").ToString
                Me.Image2.Attributes.Add("src", "../Images/accept.png")
                Me.avisos2.Attributes.Add("class", "mensajeExito")
            Else
                lblMensajePlan.Visible = True
                lblMensajePlan.ForeColor = Drawing.Color.Black
                lblMensajePlan.Text = dts.Rows(0).Item("Mensaje").ToString
                Me.Image2.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos2.Attributes.Add("class", "mensajeError")
            End If

            'Consultamos la lista de registros
            ConsultarGridRegistroPlanes("%")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            If Page.IsValid Then

                Me.lblMensajePlan.Text = ""
                Me.lblMensajePlan.Visible = False
                Me.Image2.Attributes.Add("src", "../Images/beforelastchild.GIF")
                Me.avisos2.Attributes.Add("class", "none")

                Me.lblMensajePers.Text = ""
                Me.lblMensajePers.Visible = False
                Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
                Me.avisos.Attributes.Add("class", "none")


                If rbtPerspectiva.Checked = True Then
                    gvRegistroPlan.Visible = False
                    gvRegistrosPers.Visible = True
                    dts = obj.ConsultarPerspectivas(txtBuscar.Text.Trim.ToString)
                    gvRegistrosPers.DataSource = dts
                    gvRegistrosPers.DataBind()
                End If

                If rbtPlan.Checked = True Then
                    gvRegistroPlan.Visible = True
                    gvRegistrosPers.Visible = False
                    dts = obj.ConsultarPlanes(txtBuscar.Text.Trim.ToString)
                    gvRegistroPlan.DataSource = dts
                    gvRegistroPlan.DataBind()
                End If
            End If
            
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub chkVigencia_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkVigencia.CheckedChanged
        Try
            If chkVigencia.Checked = False Then
                chkVigencia.Text = "Inactivo"
            Else
                chkVigencia.Text = "Activo"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Try
            txtDescripcionPers.Text = ""
            txtAbreviatura.Text = ""
            Me.lblMensajePers.Text = ""
            Me.lblMensajePers.Visible = False
            Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
            Me.avisos.Attributes.Add("class", "none")

            txtColor.BackColor = Drawing.Color.White
            txtColor.Text = ""
            txtAbreviatura.Enabled = True
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            txtDescripcionplan.Text = ""
            txtFechaFin.Text = ""
            txtFechaInicio.Text = ""
            chkVigencia.Checked = False

            Me.lblMensajePlan.Text = ""
            Me.lblMensajePlan.Visible = False
            Me.Image2.Attributes.Add("src", "../Images/beforelastchild.GIF")
            Me.avisos2.Attributes.Add("class", "none")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub CustomValidator2_ServerValidate(ByVal source As System.Object, _
                                                  ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator2.ServerValidate
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ValidarPalabrasReservadas(args.Value.ToString.Trim)
            If dts.Rows(0).Item("Encontro") > 0 Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub CustomValidator3_ServerValidate(ByVal source As System.Object, _
                                                 ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator3.ServerValidate
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ValidarPalabrasReservadas(args.Value.ToString.Trim)
            If dts.Rows(0).Item("Encontro") > 0 Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub CustomValidator4_ServerValidate(ByVal source As System.Object, _
                                                ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator4.ServerValidate
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ValidarPalabrasReservadas(args.Value.ToString.Trim)
            If dts.Rows(0).Item("Encontro") > 0 Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvRegistroPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvRegistroPlan.SelectedIndexChanged

    End Sub

    Protected Sub gvRegistrosPers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvRegistrosPers.SelectedIndexChanged

    End Sub
End Class

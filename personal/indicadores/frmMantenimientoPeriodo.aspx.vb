
Partial Class Indicadores_Formularios_frmMantenimientoPeriodo
    Inherits System.Web.UI.Page

    Dim usuario As Integer
    Dim validacustom As Boolean = True

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'CargaComboConfiguracionPerspectivaPlan()
                CargaComboPeriodicidad()
                ConsultarGridRegistros("%")

            End If
            'Debe tomar del inicio de sesión
            usuario = Request.QueryString("id")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaComboPeriodicidad()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.CargaListaPeriodicidad
            If dts.Rows.Count > 0 Then
                ddlPeriodicidad.DataSource = dts
                ddlPeriodicidad.DataTextField = "descripcion"
                ddlPeriodicidad.DataValueField = "codigo"
                ddlPeriodicidad.DataBind()
            Else
                ddlPeriodicidad.datasource = Nothing
                ddlPeriodicidad.databind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    '

    Private Sub ConsultarGridRegistros(ByVal vParametro As String)
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores
            dts = obj.ConsultarPeriodo(vParametro)
            gvPeriodicidad.DataSource = dts
            gvPeriodicidad.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            Dim dts As New Data.DataTable
            Dim dtsV As New Data.DataTable
            Dim obj As New clsIndicadores


            lblMensaje.Visible = False
            lblMensaje.Text = ""
            lblMensaje.ForeColor = Drawing.Color.Green

            If Page.IsValid Then
                If validacustom Then
                    'Nuevo registro
                    If lblCodigo.Text = "" Then
                        'Verificamos que las fechas de inicio y fin no correspondan a un periodo activo ya registrado
                        dtsV = obj.ValidaFechasPeriodo(txtPeriodo.Text.Trim.ToUpper.ToString, txtFechaInicio.Text.Trim.ToString, txtFechaFinal.Text.Trim.ToString, "N", "0")
                        If dtsV.Rows.Count > 0 Then
                            lblMensaje.Visible = True
                            lblMensaje.ForeColor = Drawing.Color.Black
                            lblMensaje.Text = dtsV.Rows(0).Item("Mensaje").ToString
                            Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                            Me.avisos.Attributes.Add("class", "mensajeError")
                            Exit Sub
                        End If
                        dts = obj.InsertarPeriodo(txtPeriodo.Text.Trim.ToUpper.ToString, txtFechaInicio.Text.Trim.ToString, txtFechaFinal.Text.Trim.ToString, usuario, ddlPeriodicidad.selectedValue)
                    Else
                        'Edicion de registro
                        'Verificamos que las fechas de inicio y fin no correspondan a un periodo activo ya registrado
                        'Response.Write(txtPeriodo.Text.Trim.ToUpper.ToString)
                        'Response.Write("<br />")
                        'Response.Write(txtFechaInicio.Text.Trim.ToString)
                        'Response.Write("<br />")
                        'Response.Write(txtFechaFinal.Text.Trim.ToString)
                        'Response.Write("<br />")
                        'Response.Write("M")
                        'Response.Write("<br />")
                        'Response.Write(lblCodigo.Text.ToString)

                        dtsV = obj.ValidaFechasPeriodo(txtPeriodo.Text.Trim.ToUpper.ToString, _
                                                       txtFechaInicio.Text.Trim.ToString, _
                                                       txtFechaFinal.Text.Trim.ToString, "M", lblCodigo.Text.ToString)
                        If dtsV.Rows.Count > 0 Then
                            lblMensaje.Visible = True
                            lblMensaje.ForeColor = Drawing.Color.Black
                            lblMensaje.Text = dtsV.Rows(0).Item("Mensaje").ToString
                            Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                            Me.avisos.Attributes.Add("class", "mensajeError")
                            Exit Sub
                        End If
                        'Response.Write("Entra modificar")
                        dts = obj.ModificarPeriodo(CType(lblCodigo.Text.ToString, String), txtPeriodo.Text.Trim.ToUpper.ToString, txtFechaInicio.Text.Trim.ToString, txtFechaFinal.Text.Trim.ToString, usuario, CType(ddlPeriodicidad.selectedValue, Integer))
                        RestablecerEstado()
                    End If

                    If dts.Rows(0).Item("rpt").ToString = "1" Then
                        lblMensaje.Visible = True
                        lblMensaje.ForeColor = Drawing.Color.Black
                        lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString()
                        Me.Image1.Attributes.Add("src", "../Images/accept.png")
                        Me.avisos.Attributes.Add("class", "mensajeExito")
                    Else
                        lblMensaje.Visible = True
                        lblMensaje.ForeColor = Drawing.Color.Black
                        lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
                        Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                        Me.avisos.Attributes.Add("class", "mensajeError")
                    End If

                    'Cargamos la lista de registros en el gridview 
                    ConsultarGridRegistros("%")
                    RestablecerControles()
                End If
            End If

            ConsultarGridRegistros("%")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub RestablecerEstado()
        Try
            lblCodigo.Text = ""
            cmdGuardar.Text = "   Guardar"
            ddlPeriodicidad.SelectedValue = 0
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarCampos()
        Try
            txtFechaFinal.Text = ""
            txtFechaInicio.Text = ""
            txtPeriodo.Text = ""
            lblCodigo.Text = ""
            ddlPeriodicidad.SelectedValue = 0
            lblMensaje.Text = ""
            Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
            Me.avisos.Attributes.Add("class", "none")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub RestablecerControles()
        Try
            txtFechaFinal.Text = ""
            txtFechaInicio.Text = ""
            txtPeriodo.Text = ""
            lblCodigo.Text = ""
            ddlPeriodicidad.SelectedValue = 0
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvPeriodicidad_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvPeriodicidad.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion   
                Dim seleccion As GridViewRow
                Dim codigopdo_seleccion As String
                'lblCodigo.Visible = True            'Solo para pruebas, debe estar oculto 

                '1. Obtengo la linea del gridview que fue cliqueada
                'Response.Write(codigoobj_seleccion)
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                codigopdo_seleccion = Convert.ToString(gvPeriodicidad.DataKeys(seleccion.RowIndex).Values("Codigo"))
                ddlPeriodicidad.SelectedValue = Convert.ToInt32(gvPeriodicidad.DataKeys(seleccion.RowIndex).Values("codigo_peri"))

                '2. Obtengo el datakey de la linea que donde está el boton que cliqueé
                'lblCodigo.Text = gvVariable.DataKeys(seleccion.RowIndex).Value.ToString
                lblCodigo.Text = codigopdo_seleccion

                'Recuperamos los valores de las celas de la fila seleccionada, para mostrarlos en los controles
                txtPeriodo.Text = HttpUtility.HtmlDecode(gvPeriodicidad.Rows(seleccion.RowIndex).Cells.Item(2).Text)
                txtFechaInicio.Text = HttpUtility.HtmlDecode(gvPeriodicidad.Rows(seleccion.RowIndex).Cells.Item(3).Text)
                txtFechaFinal.Text = HttpUtility.HtmlDecode(gvPeriodicidad.Rows(seleccion.RowIndex).Cells.Item(4).Text)

                'Cambio nombre del boton Guardar por Modificar
                cmdGuardar.Text = "    Modificar"
                Me.lblMensaje.Text = ""

                LimpiaAviso()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiaAviso()
        Try
            lblMensaje.Text = ""
            Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
            Me.avisos.Attributes.Add("class", "none")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvPeriodicidad_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPeriodicidad.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim fila As Data.DataRowView
            'fila = e.Row.DataItem
            'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            'e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub gvPeriodicidad_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvPeriodicidad.RowDeleting
        Try
            Dim obj As New clsIndicadores
            Dim dtsV As New Data.DataTable
            Dim dts As New Data.DataTable

            'Validamos que el periodo no este incluido en la configuracion con la fórmula
            dtsV = obj.ValidaPeriodoFormula(gvPeriodicidad.DataKeys(e.RowIndex).Value.ToString())

            If dtsV.Rows.Count > 0 Then
                If dtsV.Rows(0).Item("Mensaje").ToString <> "" Then
                    lblMensaje.Visible = True
                    lblMensaje.Text = dtsV.Rows(0).Item("Mensaje").ToString
                    lblMensaje.ForeColor = Drawing.Color.Black
                    Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                    Me.avisos.Attributes.Add("class", "mensajeError")
                    Exit Sub
                End If
            End If


            '---####   Entra a este bloque si cumple con la validacion anterior  #####
            dts = obj.EliminaroPeriodo(gvPeriodicidad.DataKeys(e.RowIndex).Value.ToString())

            If dts.Rows(0).Item("rpt").ToString = "1" Then
                lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                Me.Image1.Attributes.Add("src", "../Images/accept.png")
                Me.avisos.Attributes.Add("class", "mensajeExito")
            Else
                lblMensaje.Visible = True
                lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
                lblMensaje.ForeColor = Drawing.Color.Black
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
            End If

            'Consultamos la lista de registros
            ConsultarGridRegistros("%")
            RestablecerControles()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            'Para Validar Caja de Busqueda
            If Page.IsValid Then        'Si no ha ingresado cadenas inválidas (select, php, script)
                Dim dts As New Data.DataTable
                Dim obj As New clsIndicadores

                dts = obj.ConsultarPeriodo(txtBuscar.Text.Trim.ToString)
                gvPeriodicidad.DataSource = dts
                gvPeriodicidad.DataBind()
            Else 'Limpia la cadena inválida de la caja de texto
                txtBuscar.Text = ""
            End If
            
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvPeriodicidad_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvPeriodicidad.PageIndexChanging
        Try
            gvPeriodicidad.PageIndex = e.NewPageIndex
            ConsultarGridRegistros("%")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Try
            LimpiarCampos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlPeriodicidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPeriodicidad.SelectedIndexChanged
        Try
            txtPeriodo.Focus()
            Me.lblMensaje.Text = ""
            Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
            Me.avisos.Attributes.Add("class", "none")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub CustomValidator2_ServerValidate(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
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

End Class

﻿
Partial Class Indicadores_Formularios_frmMantenimientoObjetivos
    Inherits System.Web.UI.Page

    Dim usuario As Integer
    Dim validacustom As Boolean = True

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Agrego Hcano : 29-09-17
        'Expirar Sesion
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        '
        Try
            If Not IsPostBack Then
                CargaComboConfiguracionPerspectivaPlan()
                'ConsultarGridRegistros("%")
                VerificaPlanActivo()
                CargarComboAños()
                CargarComboPlanes()
                EstadoControlBusqueda(False)
                ListaEjes()
            End If

            'Debe tomar del inicio de sesión
            usuario = Request.QueryString("id")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ListaEjes()
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            dts = obj.ListaEjesObjetivos()
            If dts.Rows.Count > 0 Then
                gvListaEjes.DataSource = dts
                gvListaEjes.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EstadoControlBusqueda(ByVal vEstado As Boolean)
        Try
            ddlPersBus.Enabled = vEstado
            ddlAnioBus.Enabled = vEstado
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub VerificaPlanActivo()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.AdvertenciaPlanVigente()
            If dts.Rows.Count > 0 Then
                If dts.Rows(0).Item("rpt").ToString = "1" Then
                    lblMensaje.Visible = True
                    lblMensaje.ForeColor = Drawing.Color.Red
                    lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
                Else
                    lblMensaje.Visible = False
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaComboConfiguracionPerspectivaPlan()
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            'Se envía valor 0 para que liste todos los registros
            dts = obj.ConsultarConfiguracionPerspectivaPlan(Me.Request.QueryString("ctf"), Request.QueryString("id"))
            ddlPerspectivaplan.DataSource = dts
            ddlPerspectivaplan.DataTextField = "Descripcion"
            ddlPerspectivaplan.DataValueField = "Codigo"
            ddlPerspectivaplan.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function RegistrarEjes(ByVal codigo_obj As Integer) As Boolean
        Dim obj As New clsIndicadores
        Dim dts As New Data.DataTable

        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Dim vCodigo_eje As Integer

        For i As Integer = 0 To gvListaEjes.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = gvListaEjes.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If (valor = True) Then
                sw = 1
                vCodigo_eje = Convert.ToInt32(gvListaEjes.DataKeys(Fila.RowIndex).Value)
                obj.RegistrarEjesObjetivos(codigo_obj, vCodigo_eje, Me.Request.QueryString("id"))
            End If
        Next

        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function


    Private Function validaCheckEje() As Boolean
        Dim Fila As GridViewRow
        Dim contador As Integer = 0
        For i As Integer = 0 To gvListaEjes.Rows.Count - 1
            Fila = gvListaEjes.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If (valor = True) Then
                contador = contador + 1
            End If
        Next
        'Si tiene por lo menos un eje activo podra guardar / modificar.
        If (contador >= 1) Then
            Return True
        End If

        'Si no tienen ningun eje seleccionado no podra guardar/ Modificar
        Return False

    End Function

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            Dim dts As New Data.DataTable
            Dim dtsV As New Data.DataTable
            Dim obj As New clsIndicadores

            lblMensaje.Visible = True
            lblMensaje.ForeColor = Drawing.Color.Green

            If Page.IsValid Then        'Si la pagina no tiene errores, ingresa.

                If validacustom Then 'Si no ha ingresado cadenas inválidas (select, php, script)
                    'Nuevo registro

                    '-------------------------------------------------------------------------------------
                    'Validacion de ejes. Un objetivo tiene que tener asignado por los menos un eje
                    If validaCheckEje() = False Then
                        ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Usted debe seleccionar por lo menos un eje de la lista para poder continuar.');", True)
                        Exit Sub
                    End If
                    '--------------------------------------------------------------------------------------

                    If lblCodigo.Text = "" Then

                        'Validacion de duplicidad de la descripcion del objetivo
                        'dtsV = obj.ValidaObjetivoDuplicado("N", 0, txtDescripcion.Text.ToString.ToUpper.Trim, Me.txtAbreviatura.Text.Trim.ToUpper, ddlPerspectivaplan.SelectedValue)
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

                        dts = obj.InsertarObjetivo(ddlPerspectivaplan.SelectedValue, _
                                                   txtDescripcion.Text.ToString.ToUpper.Trim, _
                                                   usuario, _
                                                   Me.txtAbreviatura.Text.ToUpper.ToString.Trim, _
                                                   txtDesdeOptimo.Text, txtHastaOptimo.Text, _
                                                    txtDesdeAmbar.Text, txtHastaAmbar.Text, _
                                                    txtDesdeRojo.Text, txtHastaRojo.Text, _
                                                    txtMeta.Text, ddlAños.SelectedValue, _
                                                    ddlDireccionEscalaMetas.SelectedValue)

                        '-------------------------------------------------------------------------------'
                        ' Registro de los Ejes
                        '-------------------------------------------------------------------------------'
                        Dim returnCod As Integer = dts.Rows(0).Item("codigo_obj")
                        RegistrarEjes(returnCod)

                    Else
                        'Validacion de duplicidad de la descripcion del objetivo
                        'dtsV = obj.ValidaObjetivoDuplicado("M", CType(lblCodigo.Text.Trim, Integer), txtDescripcion.Text.ToString.ToUpper.Trim, Me.txtAbreviatura.Text.Trim.ToUpper, ddlPerspectivaplan.SelectedValue)
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

                        'Edicion de registro
                        dts = obj.ModificarObjetivo(CType(lblCodigo.Text.Trim, Integer), _
                                                    ddlPerspectivaplan.SelectedValue, _
                                                    txtDescripcion.Text.Trim.ToUpper.ToString, _
                                                    usuario, _
                                                    Me.txtAbreviatura.Text.ToUpper.ToString.Trim, _
                                                    txtDesdeOptimo.Text, txtHastaOptimo.Text, _
                                                    txtDesdeAmbar.Text, txtHastaAmbar.Text, _
                                                    txtDesdeRojo.Text, txtHastaRojo.Text, _
                                                    txtMeta.Text, ddlAños.SelectedValue, _
                                                    ddlDireccionEscalaMetas.SelectedValue)


                        '-------------------------------------------------------------------------------'
                        ' Registro de los Ejes
                        '-------------------------------------------------------------------------------'
                        RegistrarEjes(CType(lblCodigo.Text.Trim, Integer))

                    End If

                    'Response.Write(dts.Rows(0).Item("rpt").ToString)
                    'Response.Write("<br />")

                    If dts.Rows(0).Item("rpt").ToString = "1" Then
                        lblMensaje.Visible = True
                        lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
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

                    'Cargamos la lista de registros en el gridview 
                    ConsultarGridRegistros("%")
                    LimpiarCampos()

                Else 'Limpia la cadena inválida de la caja de texto
                    txtDescripcion.Text = ""
                End If
            End If
            Me.ListaEjes()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarCampos()
        lblCodigo.Text = ""
        txtAbreviatura.Text = ""
        txtDescripcion.Text = ""
        ddlPerspectivaplan.SelectedValue = 0
        txtMeta.Text = ""
        txtDesdeAmbar.Text = ""
        txtDesdeOptimo.Text = ""
        txtDesdeRojo.Text = ""
        txtHastaAmbar.Text = ""
        txtHastaOptimo.Text = ""
        txtHastaRojo.Text = ""
        ddlAños.SelectedValue = 0
        ddlDireccionEscalaMetas.SelectedValue = "-"

        'Cambio nombre del boton Guardar por Modificar
        cmdGuardar.Text = "   Guardar"

        'lblMensaje.Visible = True
        'lblMensaje.Text = ""
        'Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
        'Me.avisos.Attributes.Add("class", "none")
    End Sub

    Private Sub LimpiarAviso()
        lblCodigo.Text = ""
        cmdGuardar.Text = "   Guardar"

        lblMensaje.Visible = False
        lblMensaje.Text = ""
        Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
        Me.avisos.Attributes.Add("class", "none")
    End Sub

    Private Sub ConsultarGridRegistros(ByVal vParametro As String)
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores
            dts = obj.ConsultarObjetivos(vParametro, ddlPlan2.SelectedValue, Me.ddlPersBus.SelectedValue, ddlAnioBus.SelectedValue)

            'Response.Write(dts.Rows.Count)
            If dts.Rows.Count > 0 Then
                gvObjetivos.DataSource = dts
                gvObjetivos.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        LimpiarAviso()
        LimpiarCampos()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "refresh", "window.setTimeout('var url = window.location.href;window.location.href = url',0);", True)
    End Sub

    Protected Sub gvObjetivos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvObjetivos.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion   

                '--------------------------------------------------
                'Limiamos la grilla de los ejes.
                ListaEjes()
                '--------------------------------------------------

                Dim seleccion As GridViewRow
                Dim codigoobj_seleccion As Integer


                'If rbtTodos.Checked = False Then
                CargaComboConfiguracionPerspectivaPlan()

                'lblCodigo.Visible = True            'Solo para pruebas, debe estar oculto 

                '1. Obtengo la linea del gridview que fue cliqueada
                'Response.Write(codigoobj_seleccion)
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                codigoobj_seleccion = Convert.ToInt32(gvObjetivos.DataKeys(seleccion.RowIndex).Values("Codigo"))


                '2. Obtengo el datakey de la linea que donde está el boton que cliqueé
                'lblCodigo.Text = gvVariable.DataKeys(seleccion.RowIndex).Value.ToString
                lblCodigo.Text = codigoobj_seleccion

                'Recuperamos los valores de las celas de la fila seleccionada, para mostrarlos en los controles
                txtAbreviatura.Text = HttpUtility.HtmlDecode(gvObjetivos.Rows(seleccion.RowIndex).Cells.Item(3).Text)
                txtDescripcion.Text = HttpUtility.HtmlDecode(gvObjetivos.Rows(seleccion.RowIndex).Cells.Item(4).Text)

                txtDesdeOptimo.Text = gvObjetivos.DataKeys(seleccion.RowIndex).Values("DesdeOptimo").ToString
                txtHastaOptimo.Text = gvObjetivos.DataKeys(seleccion.RowIndex).Values("HastaOptimo").ToString
                txtDesdeAmbar.Text = gvObjetivos.DataKeys(seleccion.RowIndex).Values("DesdeAmbar").ToString
                txtHastaAmbar.Text = gvObjetivos.DataKeys(seleccion.RowIndex).Values("HastaAmbar").ToString
                txtDesdeRojo.Text = gvObjetivos.DataKeys(seleccion.RowIndex).Values("DesdeRojo").ToString
                txtHastaRojo.Text = gvObjetivos.DataKeys(seleccion.RowIndex).Values("HastaRojo").ToString
                txtMeta.Text = gvObjetivos.Rows(seleccion.RowIndex).Cells.Item(6).Text.ToString
                ddlAños.SelectedValue = gvObjetivos.Rows(seleccion.RowIndex).Cells.Item(7).Text.ToString

                'Response.Write(gvObjetivos.DataKeys(seleccion.RowIndex).Values("direccionescala_obj"))
                Select Case gvObjetivos.DataKeys(seleccion.RowIndex).Values("direccionescala_obj").ToString
                    Case "Ascendente"
                        ddlDireccionEscalaMetas.SelectedIndex = 1
                    Case "Descendente"
                        ddlDireccionEscalaMetas.SelectedIndex = 2
                    Case Else
                        ddlDireccionEscalaMetas.SelectedIndex = 0
                End Select

                'ddlDireccionEscalaMetas.SelectedValue = gvObjetivos.Rows(seleccion.RowIndex).Cells.Item(8).Text.ToString

                '--//------------------------------------------------------------------------------
                Dim dts As New Data.DataTable
                Dim obj As New clsIndicadores
                Dim Bandera As Boolean = False


                dts = obj.ConsultarConfiguracionPerspectivaPlan(Request.QueryString("ctf"), Request.QueryString("id"))

                If dts.Rows.Count > 0 Then
                    For i As Integer = 0 To dts.Rows.Count - 1
                        If dts.Rows(i).Item("Codigo") = gvObjetivos.DataKeys(seleccion.RowIndex).Values("codigo_ppla") Then
                            Bandera = True
                        End If
                    Next
                End If

                If Bandera = True Then
                    lblMensaje.Visible = False
                    lblMensaje.Text = ""
                    Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
                    Me.avisos.Attributes.Add("class", "none")
                    ddlPerspectivaplan.SelectedValue = Convert.ToInt32(gvObjetivos.DataKeys(seleccion.RowIndex).Values("codigo_ppla"))
                Else
                    ddlPerspectivaplan.SelectedValue = 0
                    lblMensaje.Visible = True
                    lblMensaje.ForeColor = Drawing.Color.Black
                    lblMensaje.Text = "La perspectiva ha sido eliminada."
                    Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                    Me.avisos.Attributes.Add("class", "mensajeError")
                End If
                '--//------------------------------------------------------------------------------

                '-----------------------------------------------------------------------------------------------------
                'Asignamos los ejes que tiene asignados el objetivo. 
                '-----------------------------------------------------------------------------------------------------
                Dim dts_ejes As New Data.DataTable
                dts_ejes = obj.ListaEjesAsignados(codigoobj_seleccion)
                For i As Integer = 0 To dts_ejes.Rows.Count - 1
                    For Each row As GridViewRow In gvListaEjes.Rows
                        Dim check As CheckBox = TryCast(row.FindControl("chkElegir"), CheckBox)
                        If gvListaEjes.DataKeys(row.RowIndex).Value() = dts_ejes.Rows(i).Item("codigo_eje") Then
                            check.Checked = True
                        End If
                    Next
                Next
                '----------------------------------------------------------------------------------------------------

                'Cambio nombre del boton Guardar por Modificar
                cmdGuardar.Text = "    Modificar"

                ConsultarGridRegistros("%")
                'End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvObjetivos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvObjetivos.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim fila As Data.DataRowView
            'fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            Select Case e.Row.Cells(8).Text
                Case "DESC"
                    e.Row.Cells(8).Text = "<center><img src='../images/desc.png' alt='" & "Descendente" & "'  style='border: 0px'/></center>"
                Case "ASC"
                    e.Row.Cells(8).Text = "<center><img src='../images/asc.png' alt='" & "Ascendente" & "' style='border: 0px'/></center>"
                Case "N/D"
                    e.Row.Cells(8).Text = "<center><img src='../images/porDefinir.png'  alt='" & "No Definido" & "' style='border: 0px'/></center>"
            End Select
        End If
    End Sub


    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            If Page.IsValid Then        'Si no ha ingresado cadenas inválidas (select, php, script)
                Dim dts As New Data.DataTable
                Dim obj As New clsIndicadores
                Dim texto As String

                If txtBuscar.Text.Trim.ToString = "" Then
                    texto = "%"
                Else
                    texto = txtBuscar.Text.Trim.ToString
                End If

                dts = obj.ConsultarObjetivos(texto, CType(ddlPlan2.SelectedValue, Integer), ddlPersBus.SelectedValue, ddlAnioBus.SelectedValue)

                gvObjetivos.DataSource = dts
                gvObjetivos.DataBind()
            Else 'Limpia la cadena inválida de la caja de texto
                txtBuscar.Text = ""
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvObjetivos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvObjetivos.RowDeleting
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            Dim dtsV As New Data.DataTable

            'La columna Codigo esta oculta. Por eso uso el DataKey de la fila seleccionada
            'lblMensaje.Text = obj.EliminarVariable(gvObjetivos.DataKeys(e.RowIndex).Value.ToString())
            'lblMensaje.Text = gvObjetivos.DataKeys(e.RowIndex).Value.ToString()

            dtsV = obj.ValidaObjetivoIndicadorEliminacion(gvObjetivos.DataKeys(e.RowIndex).Value)
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

            '### Ejecuta este bloque siempre y cuando el objetivo no este amarrado a ningun indicador. ####

            dts = obj.EliminaroObjetivo(gvObjetivos.DataKeys(e.RowIndex).Value.ToString())
            If dts.Rows(0).Item("rpt").ToString = "1" Then
                lblMensaje.Visible = True
                lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
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

            '-------------------------------------------------------------------------------------------------
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

    'Para Validar Caja de Busqueda
    Protected Sub CustomValidator5_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator5.ServerValidate
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

    Private Sub CargarComboAños()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'Modificado xDguevara 09.05.2012
            dts = obj.ConsultarPeriodosPosteriores(8, 0)

            ddlAños.DataSource = dts
            ddlAños.DataTextField = "Descripcion"
            ddlAños.DataValueField = "Codigo"
            ddlAños.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboPlanes()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListaPlanes(Request.QueryString("ctf"), Request.QueryString("id"))

            If dts.Rows.Count > 0 Then
                ddlPlan2.DataSource = dts
                ddlPlan2.DataTextField = "Descripcion"
                ddlPlan2.DataValueField = "Codigo"
                ddlPlan2.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlPlan2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlan2.SelectedIndexChanged
        Try
            Dim dts As New Data.DataTable
            Dim dts2 As New Data.DataTable
            Dim obj As New clsIndicadores


            If ddlPlan2.SelectedValue <> 0 Then
                EstadoControlBusqueda(True)
                dts = obj.ListaPerspectivasxPlanBusqueda(Me.ddlPlan2.SelectedValue)
                If dts.Rows.Count > 0 Then
                    ddlPersBus.DataSource = dts
                    ddlPersBus.DataTextField = "Descripcion"
                    ddlPersBus.DataValueField = "Codigo"
                    ddlPersBus.DataBind()
                End If

                dts2 = obj.ListaAñosObjetivosBusqueda(Me.ddlPlan2.SelectedValue)
                If dts2.Rows.Count > 0 Then
                    ddlAnioBus.DataSource = dts2
                    ddlAnioBus.DataTextField = "Descripcion"
                    ddlAnioBus.DataValueField = "Codigo"
                    ddlAnioBus.DataBind()
                End If
            Else
                EstadoControlBusqueda(False)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class

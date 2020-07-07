
Partial Class Indicadores_Formularios_frmConfiguracionPlanPerspectiva
    Inherits System.Web.UI.Page

    Dim usuario As Integer
    Dim BanderaPerspectiva As Byte

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'ConsultarGridRegistros()

                'Al cargar la web mostramos en el combo todos los planes por defecto
                CargarComboPlanes()
                CargarRegistrosPerspectivas()

                CargarComboCentroCostos()



                
            End If

            'Debe tomar del inicio de sesión
            usuario = Request.QueryString("id")

            '--/Incializamos las variable en 0
            BanderaPerspectiva = 0

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboCentroCostos()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListarCentroCostos()
            ddlCentroCosto.DataSource = dts
            ddlCentroCosto.DataTextField = "descripcion_Cco"
            ddlCentroCosto.DataValueField = "codigo_Cco"
            ddlCentroCosto.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub



    Private Sub CargarRegistrosPerspectivas()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ConsultarPerspectivas("%")
            gvListaPerspectivas.DataSource = dts
            gvListaPerspectivas.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboPlanes()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListaPlanes(Me.Request.QueryString("ctf"), Me.Request.QueryString("id"))
            ddlPlan.DataSource = dts
            If dts.Rows.Count > 0 Then
                ddlPlan.DataTextField = "Descripcion"
                ddlPlan.DataValueField = "Codigo"
                ddlPlan.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    



    Protected Sub gvListaPerspectivas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaPerspectivas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim fila As Data.DataRowView
            'fila = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            'If ddlPeriodicidad.SelectedValue = 0 Then lblMensaje.Visible = True : lblMensaje.ForeColor = Drawing.Color.Red : lblMensaje.Text = "Seleccione una Periocidad" : Exit Sub

            If (validaCheckActivo() = True) Then

                '--// Personalizar el mensaje de aviso //

                If BanderaPerspectiva = 0 Then
                    lblMensaje.Visible = True
                    lblMensaje.Text = "(*) Las perspectivas fueron agregadas correctamente."
                    lblMensaje.ForeColor = Drawing.Color.Black
                    Me.Image1.Attributes.Add("src", "../Images/accept.png")
                    Me.avisos.Attributes.Add("class", "mensajeExito")
                Else
                    lblMensaje.Visible = True
                    lblMensaje.Text = "(*) Algunas de las perspectivas ya estan registradas. Solo se registraron las nuevas perspectivas."
                    lblMensaje.ForeColor = Drawing.Color.Black
                    Me.Image1.Attributes.Add("src", "../Images/accept.png")
                    Me.avisos.Attributes.Add("class", "mensajeExito")
                End If
                '--/---------------------------------------------------


                Dim obj As New clsIndicadores
                Dim dts As New Data.DataTable

                dts = obj.ListaPerspectivasSegunPlan(ddlPlan.SelectedValue)
                If dts.Rows.Count > 0 Then
                    gvPlanPerspectiva.DataSource = dts
                    gvPlanPerspectiva.DataBind()
                End If

                LimpiarControles()

            Else
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = "(*) Debe seleccionar una perspectiva como mínimo para poder agregar al plan seleccionado."
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
            End If
            BanderaPerspectiva = 0
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Function validaCheckActivo() As Boolean
        Dim obj As New clsIndicadores
        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Dim vCodigo_pers As Integer

        '--/ Para verificar si la perspectiva ya esta registrada para un determinado plan i centro de costo
        '--/ Muestra el mensaje pero no deja insertar las perspectivas ya insertadas para ese plan y ceco

        BanderaPerspectiva = 0

        For i As Integer = 0 To gvListaPerspectivas.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = gvListaPerspectivas.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If (valor = True) Then
                sw = 1
                'Con este codigo recuperamos los datos de una determinada celda de la fila
                'Me.lblDestinatario.Text = Me.gvVariable.Rows(i).Cells(1).Text

                vCodigo_pers = Convert.ToInt32(gvListaPerspectivas.DataKeys(Fila.RowIndex).Value)

                'Validacion para alertar al usuario que algunas de las perspectivas elegidas ya estaba registradas.
                Dim dtsV As New Data.DataTable
                dtsV = obj.ValidarExistenciaPerspectiva(Me.ddlPlan.SelectedValue, vCodigo_pers, ddlCentroCosto.SelectedValue)
                If dtsV.Rows.Count > 0 Then
                    If dtsV.Rows(0).Item("Rpta") <> 0 Then
                        BanderaPerspectiva = 1
                    End If
                End If

                obj.InsertaPerspectivaPlan(Me.ddlPlan.SelectedValue, vCodigo_pers, usuario, ddlCentroCosto.SelectedValue)
            End If
        Next

        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function


    Private Sub LimpiarControles()
        Try
            'ddlPlan.SelectedValue = 0
            'ddlCentroCosto.SelectedValue = 0
            CargarRegistrosPerspectivas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlan.SelectedIndexChanged
        Try
            If ddlPlan.SelectedValue = 0 Then
                gvPlanPerspectiva.DataSource = Nothing
                gvPlanPerspectiva.DataBind()
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = "Seleccione un plan de la lista desplegable."
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
                Exit Sub
            End If

            If ddlPlan.SelectedValue <> 0 Then
                gvPlanPerspectiva.DataSource = Nothing
                gvPlanPerspectiva.DataBind()

                ListaPerspectivasPlan(ddlPlan.SelectedValue)
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub ListaPerspectivasPlan(ByVal vCodigo_pla As Integer)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListaPerspectivasSegunPlan(vCodigo_pla)
            If dts.Rows.Count > 0 Then
                gvPlanPerspectiva.DataSource = dts
                gvPlanPerspectiva.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvPlanPerspectiva_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPlanPerspectiva.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim fila As Data.DataRowView
            'fila = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub gvListaPerspectivas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvListaPerspectivas.RowDeleting
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.EliminarPerspectiva(gvListaPerspectivas.DataKeys(e.RowIndex).Value.ToString())

            If dts.Rows(0).Item("rpt").ToString = "1" Then
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Green
                lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
            Else
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Red
                lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
            End If

            'Consultamos la lista de registros
            CargarRegistrosPerspectivas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvPlanPerspectiva_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvPlanPerspectiva.RowDeleting
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            Dim dtsV As New Data.DataTable

            '--// Validacion: Verificar si el planperspectiva se encuentra asignado a un objetivo activo, no puede eliminar  --//.
            dtsV = obj.ValidarEliminacionPlanPerspectiva(gvPlanPerspectiva.DataKeys(e.RowIndex).Value)
            If dtsV.Rows.Count > 0 Then
                If dtsV.Rows(0).Item("Mensaje").ToString() <> "" Then
                    lblMensaje.Visible = True
                    lblMensaje.ForeColor = Drawing.Color.Black
                    lblMensaje.Text = dtsV.Rows(0).Item("Mensaje").ToString
                    Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                    Me.avisos.Attributes.Add("class", "mensajeError")
                    Exit Sub
                End If
            End If
            '--//-----------------------------------------/

            'Elimina siempre y cuando el plan  - perspectiva no este amarrado con un obj.
            dts = obj.EliminarPlanPerspectiva(gvPlanPerspectiva.DataKeys(e.RowIndex).Value.ToString())

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

            ListaPerspectivasPlan(ddlPlan.SelectedValue)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvPlanPerspectiva_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvPlanPerspectiva.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion   
                Dim seleccion As GridViewRow
                Dim codigoppla_seleccion As Integer
                'lblCodigo.Visible = True            'Solo para pruebas, debe estar oculto 

                '1. Obtengo la linea del gridview que fue cliqueada
                'Response.Write(codigoobj_seleccion)
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                codigoppla_seleccion = Convert.ToInt32(gvPlanPerspectiva.DataKeys(seleccion.RowIndex).Values("Codigo"))


                '2. Obtengo el datakey de la linea que donde está el boton que cliqueé
                'lblCodigo.Text = gvVariable.DataKeys(seleccion.RowIndex).Value.ToString
                lblCodigo.Text = codigoppla_seleccion

                'Recuperamos los valores de las celas de la fila seleccionada, para mostrarlos en los controles
                'txtDescripcion.Text = HttpUtility.HtmlDecode(gvPlanPerspectiva.Rows(seleccion.RowIndex).Cells.Item(3).Text)
                ddlPlan.SelectedValue = Convert.ToInt32(gvPlanPerspectiva.DataKeys(seleccion.RowIndex).Values("codigo_pla"))
                ddlCentroCosto.SelectedValue = Convert.ToInt32(gvPlanPerspectiva.DataKeys(seleccion.RowIndex).Values("codigo_cco"))

                Dim codigo_pers As Integer = Convert.ToInt32(gvPlanPerspectiva.DataKeys(seleccion.RowIndex).Values("codigo_pers"))
                lblCodPers.text = codigo_pers

                If validaPerspectivaCheckActivo(codigo_pers) = True Then
                    lblMensaje.Visible = True
                    lblMensaje.Text = "Cheko"
                End If

                'Cambio nombre del boton Guardar por Modificar
                cmdGuardar.Text = "    Modificar"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function validaPerspectivaCheckActivo(ByVal codigo_pers As Integer) As Boolean
        Dim Fila As GridViewRow
        Dim sw As Byte = 0


        For i As Integer = 0 To gvListaPerspectivas.Rows.Count - 1
            'Response.Write(gvListaPerspectivas.Rows(i).Cells(2).Text())
            'Response.Write("<br/>")
            If codigo_pers = gvListaPerspectivas.Rows(i).Cells(2).Text() Then
                'Response.Write("Entra al if")
                Fila = gvListaPerspectivas.Rows(i)
                'Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True
                sw = 1
            End If

            'Capturamos las filas que estan activas



            'If (valor = True) Then
            '    sw = 1
            '    Me.lblDestinatario.Text = Me.lblDestinatario.Text & "|" & Me.gvwEgresados.Rows(i).Cells(7).Text
            'End If
        Next

        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function

    Protected Sub gvListaPerspectivas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvListaPerspectivas.PageIndexChanging
        Try
            gvListaPerspectivas.PageIndex = e.NewPageIndex
            CargarRegistrosPerspectivas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Protected Sub gvPlanPerspectiva_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvPlanPerspectiva.PageIndexChanging
    '    Try
    '        gvListaPerspectivas.PageIndex = e.NewPageIndex
    '        ListaPerspectivasPlan(ddlPlan.SelectedValue)
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Try
            If Page.IsValid Then
                Dim dts As New Data.DataTable
                Dim obj As New clsIndicadores

                dts = obj.ConsultarPerspectivas(txtBuscar.Text.Trim)
                gvListaPerspectivas.DataSource = dts
                gvListaPerspectivas.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Try
            ddlCentroCosto.SelectedValue = 0
            txtbuscar.text = ""
            lblMensaje.Visible = False
            lblMensaje.Text = ""
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

End Class

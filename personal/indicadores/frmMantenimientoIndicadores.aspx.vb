
Partial Class Indicadores_Formularios_frmMantenimientoIndicadores
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
                'ConsultarGridRegistros()                
                CargarComboPlanes()
                CargarComboAños()
                CargarListaOperacionesIndicador()

                'Debe tomar del inicio de sesión
                usuario = Request.QueryString("id")
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarListaOperacionesIndicador()
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            dts = obj.ListaOperacionesIndicador
            If dts.Rows.Count > 0 Then
                ddlTipoOperacion.DataSource = dts
                ddlTipoOperacion.DataTextField = "Descripcion"
                ddlTipoOperacion.DataValueField = "Codigo"
                ddlTipoOperacion.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub ConsultarGridRegistros()
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            'Se envía valor 0 para que liste todos los registros            
            dts = obj.ConsultarIndicador(Me.ddlPlan2.SelectedValue, txtBuscar.Text, Me.ddlAnioBus.SelectedValue, Me.ddlListaObjetivos.SelectedValue)
            gvIndicadores.DataSource = dts
            gvIndicadores.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboPlanes()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListaPlanes(Me.Request.QueryString("ctf"), Request.QueryString("id"))

            If dts.Rows.Count > 0 Then
                ddlPlan.DataSource = dts
                ddlPlan.DataTextField = "Descripcion"
                ddlPlan.DataValueField = "Codigo"
                ddlPlan.DataBind()

                ddlPlan2.DataSource = dts
                ddlPlan2.DataTextField = "Descripcion"
                ddlPlan2.DataValueField = "Codigo"
                ddlPlan2.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboPerspectivas()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListarPerspectivas(ddlPlan.SelectedValue)

            If dts.Rows.Count > 0 Then
                ddlPerspectiva.DataSource = dts
                ddlPerspectiva.DataTextField = "Descripcion"
                ddlPerspectiva.DataValueField = "Codigo"
                ddlPerspectiva.DataBind()
            End If
                    
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboObjetivos()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListarObjetivos(ddlPerspectiva.SelectedValue, ddlPlan.SelectedValue)
            ddlObjetivos.DataSource = dts
            ddlObjetivos.DataTextField = "Descripcion"
            ddlObjetivos.DataValueField = "Codigo"
            ddlObjetivos.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub CargarComboAños()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ConsultarPeriodosPosteriores(8, 0)
            ddlAños.DataSource = dts
            ddlAños.DataTextField = "Descripcion"
            ddlAños.DataValueField = "Codigo"
            ddlAños.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click

        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            lblMensaje.Visible = True

            If Page.IsValid Then        'Si no ha ingresado cadenas inválidas (select, php, script)

                If txtBasal.Text = "" Then
                    txtBasal.Text = 0
                End If

                'Nuevo registro

                If ddlDireccionEscalaMetas.SelectedValue = "Descendente" And txtBasal.Text = 0 Then

                    lblMensaje.Visible = True
                    lblMensaje.Text = "Cuando el comportamiento del indicador es descendente, es obligatorio que se registre el Basal."

                    'Para avisos
                    Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                    Me.avisos.Attributes.Add("class", "mensajeError")

                    Exit Sub

                End If

                If lblCodigo.Text = "" Then

                    'Retorna el codigo de la variable 
                    'lblMensaje.Text = obj.InsertarIndicador(txtDescripcion.Text.ToUpper, ddlObjetivos.SelectedValue, CType(txtPonderacion.Text, Decimal), CType(txtDesdeOptimo.Text, Decimal), CType(txtHastaOptimo.Text, Decimal), CType(txtDesdeAmbar.Text, Decimal), CType(txtHastaAmbar.Text, Decimal), CType(txtDesdeRojo.Text, Decimal), CType(txtHastaRojo.Text, Decimal), usuario)
                    lblMensaje.Text = obj.InsertarIndicador(txtDescripcion.Text.ToUpper, ddlObjetivos.SelectedValue, txtPonderacion.Text, txtDesdeOptimo.Text, txtHastaOptimo.Text, txtDesdeAmbar.Text, txtHastaAmbar.Text, txtDesdeRojo.Text, txtHastaRojo.Text, Request.QueryString("id"), txtMeta.Text, ddlTipoDato.Text, ddlAños.SelectedValue, ddlDireccionEscalaMetas.SelectedValue, txtBasal.Text, ddlTipoOperacion.SelectedValue)

                    '***********************************************************************************
                    '***********************  Edicion de registro **************************************
                    '***********************************************************************************
                Else

                    'lblMensaje.Text = obj.ModificarIndicador(lblCodigo.Text, txtDescripcion.Text.ToUpper, ddlObjetivos.SelectedValue, CType(txtPonderacion.Text, Decimal), CType(txtDesdeOptimo.Text, Decimal), CType(txtHastaOptimo.Text, Decimal), CType(txtDesdeAmbar.Text, Decimal), CType(txtHastaAmbar.Text, Decimal), CType(txtDesdeRojo.Text, Decimal), CType(txtHastaRojo.Text, Decimal), usuario)
                    'Response.Write(lblCodigo.Text)
                    'Response.Write("---")
                    'Response.Write(txtDescripcion.Text.ToUpper)
                    'Response.Write("---")
                    'Response.Write(ddlObjetivos.SelectedValue)
                    'Response.Write("---")
                    'Response.Write(txtPonderacion.Text)
                    'Response.Write("---")
                    'Response.Write(txtDesdeOptimo.Text)
                    'Response.Write("---")
                    'Response.Write(txtHastaOptimo.Text)
                    'Response.Write("---")
                    'Response.Write(txtDesdeAmbar.Text)
                    'Response.Write("---")
                    'Response.Write(txtHastaAmbar.Text)
                    'Response.Write("---")
                    'Response.Write(txtDesdeRojo.Text)
                    'Response.Write("---")
                    'Response.Write(txtHastaRojo.Text)

                    lblMensaje.Text = obj.ModificarIndicador(lblCodigo.Text, txtDescripcion.Text.ToUpper, ddlObjetivos.SelectedValue, txtPonderacion.Text, txtDesdeOptimo.Text, txtHastaOptimo.Text, txtDesdeAmbar.Text, txtHastaAmbar.Text, txtDesdeRojo.Text, txtHastaRojo.Text, usuario, txtMeta.Text, ddlTipoDato.Text, ddlAños.SelectedValue, ddlDireccionEscalaMetas.SelectedValue, txtBasal.Text, ddlTipoOperacion.SelectedValue)

                    'Cambio nombre del boton Modificar por Guardar
                    cmdGuardar.Text = "   Guardar"

                End If

                If lblMensaje.Text = "Datos registrados correctamente." Or lblMensaje.Text = "Datos modificados correctamente." Then
                    'Para avisos
                    Me.Image1.Attributes.Add("src", "../Images/accept.png")
                    Me.avisos.Attributes.Add("class", "mensajeExito")
                Else
                    'Para avisos
                    Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                    Me.avisos.Attributes.Add("class", "mensajeError")
                End If

                'Refrescar listado
                ConsultarGridRegistros()
                limpiarcajas()

            Else 'Limpia la cadena inválida de la caja de texto
                txtDescripcion.Text = ""
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvIndicadores_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvIndicadores.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            'Direccion
            Select Case e.Row.Cells(13).Text
                Case "DESC"
                    e.Row.Cells(13).Text = "<center><img src='../images/desc.png' alt='" & "Descendente" & "'  style='border: 0px'/></center>"
                Case "ASC"
                    e.Row.Cells(13).Text = "<center><img src='../images/asc.png' alt='" & "Ascendente" & "' style='border: 0px'/></center>"
                Case "N/D"
                    e.Row.Cells(13).Text = "<center><img src='../images/porDefinir.png'  alt='" & "No Definido" & "' style='border: 0px'/></center>"
            End Select

            'Tipo Operacion
            If e.Row.Cells(9).Text = "ND" Then  'No definido
                e.Row.Cells(9).Text = "<center><img src='../images/alert.png'  alt='" & "No Definido" & "' style='border: 0px'/></center>"
            End If

            'xDguevara 24.05.2012
            'Acciones para cargar el dropdownList, para mostrar los valores de avance del indicadores por semestre segun el año.
            If e.Row.Cells(10).Text <> "" Then
                Dim codigo_int As Integer = gvIndicadores.DataKeys(e.Row.RowIndex).Values(0)
                Dim combo As DropDownList = DirectCast(e.Row.FindControl("ddlSemestre"), DropDownList)
                combo.ClearSelection()
                If combo IsNot DBNull.Value Then
                    Me.prcCargarComboGridView(combo, codigo_int, CType(e.Row.Cells(10).Text, Integer))
                End If
            End If


            '---------------------------------------------------------------------------------------------------------
            '12 Es la celda a pinta, referente al avance del indicador.
            '13 es el codigo_st en el datakey para obtener el estado del semaforo.
            Dim val As Integer = gvIndicadores.DataKeys(e.Row.RowIndex).Values(13)
            'Response.Write(val)

            If val <> 0 Then
                Select Case gvIndicadores.DataKeys(e.Row.RowIndex).Values(13)
                    Case 1  ' Verde
                        e.Row.Cells(12).BackColor = Drawing.Color.Green
                        e.Row.Cells(12).ForeColor = Drawing.Color.White
                    Case 2  ' Ambar
                        e.Row.Cells(12).BackColor = Drawing.Color.Orange
                        e.Row.Cells(12).ForeColor = Drawing.Color.Blue
                    Case 3  ' Rojo
                        e.Row.Cells(12).BackColor = Drawing.Color.Red
                        e.Row.Cells(12).ForeColor = Drawing.Color.White
                End Select
            End If

            '-------------------------------------------------------------------------------------------------------------
            

        End If
    End Sub



    Protected Sub ddlSemestre_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'Me.Response.Write("click")


            Dim ddl As DropDownList = DirectCast(sender, DropDownList)
            Dim row As GridViewRow = TryCast(ddl.NamingContainer, GridViewRow)
            Dim i As Int16



            If row IsNot Nothing Then
                Response.Write(CType(row.FindControl("ddlSemestre"), DropDownList).SelectedValue)





                'If CType(row.FindControl("DropDownList1"), DropDownList).SelectedValue = "0" Then
                '    For i = 3 To 33
                '        row.Cells(i).Text = 0
                '    Next
                'ElseIf CType(row.FindControl("DropDownList1"), DropDownList).SelectedValue = "1" Then
                '    For i = 3 To 33
                '        row.Cells(i).Text = 1
                '    Next
                'End If
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Public Sub prcCargarComboGridView(ByVal cboCombo As DropDownList, ByVal codigo_ind As Integer, ByVal anio As Integer)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.Avance_Indicador_Semestral(codigo_ind, anio)
            cboCombo.DataSource = dts
            cboCombo.DataTextField = "AvanceSemestral"
            cboCombo.DataValueField = "Codigo"
            cboCombo.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvCategoria_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvIndicadores.RowDeleting
        Try
            Dim obj As New clsIndicadores

            'La columna Codigo esta oculta. Por eso uso el DataKey de la fila seleccionada
            lblMensaje.Text = obj.EliminarIndicador(gvIndicadores.DataKeys(e.RowIndex).Value.ToString(), usuario)
            lblMensaje.Visible = True

            If lblMensaje.Text = "Registro eliminado con éxito." Then
                'Para avisos
                Me.Image1.Attributes.Add("src", "../Images/accept.png")
                Me.avisos.Attributes.Add("class", "mensajeExito")
            Else
                'Para avisos
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
            End If

            'Refrescar campos
            limpiarcajas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        ConsultarGridRegistros()
    End Sub


    Protected Sub gvCategoria_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvIndicadores.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion                
                LimpiarCampos()

                Dim seleccion As GridViewRow
                Dim codigo_ind_seleccion As Integer
                Dim obj As New clsIndicadores
                Dim dts As New Data.DataTable
                Dim bandera As Integer
                bandera = 0

                '1. Obtengo la linea del gridview que fue cliqueada
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)

                codigo_ind_seleccion = Convert.ToInt32(gvIndicadores.DataKeys(seleccion.RowIndex).Values("Codigo"))

                '2. Grabo en lbl oculto, para luego poder modificar
                lblCodigo.Text = codigo_ind_seleccion

                '3. Paso los datos del grid a los controles
                txtDescripcion.Text = HttpUtility.HtmlDecode(gvIndicadores.Rows(seleccion.RowIndex).Cells.Item(4).Text)
                txtMeta.Text = gvIndicadores.Rows(seleccion.RowIndex).Cells.Item(6).Text.ToString
                txtPonderacion.Text = gvIndicadores.Rows(seleccion.RowIndex).Cells.Item(7).Text.ToString
                ddlPlan.SelectedValue = Convert.ToInt32(gvIndicadores.DataKeys(seleccion.RowIndex).Values("codigo_pla"))

                '4. Cargar combo perspectivas
                CargarComboPerspectivas()

                'Verificar que la perspectiva esté en el combo                
                dts = obj.ListarPerspectivas(Convert.ToInt32(gvIndicadores.DataKeys(seleccion.RowIndex).Values("codigo_pla")))
                If dts.Rows.Count > 0 Then
                    For x As Integer = 0 To dts.Rows.Count - 1
                        If dts.Rows(x).Item("Codigo") = gvIndicadores.DataKeys(seleccion.RowIndex).Values("CodigoPerspectiva") Then
                            bandera = 1
                        End If
                    Next
                End If

                If bandera = 1 Then
                    ddlPerspectiva.SelectedValue = gvIndicadores.DataKeys(seleccion.RowIndex).Values("CodigoPerspectiva")
                    CargarComboObjetivos()
                    ddlObjetivos.SelectedValue = gvIndicadores.DataKeys(seleccion.RowIndex).Values("codigo_obj")
                Else
                    ddlPerspectiva.SelectedValue = 0
                    ddlObjetivos.Items.Add("--SELECCIONE--")
                    ddlObjetivos.Items(0).Value = "0"
                    ddlObjetivos.SelectedValue = 0
                    lblMensaje.Visible = True
                    lblMensaje.Text = "La perspectiva y objetivo del Indicador han sido eliminados. Por favor, debe cambiarlos."
                    'Para avisos
                    Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                    Me.avisos.Attributes.Add("class", "mensajeError")
                End If

                txtDesdeOptimo.Text = gvIndicadores.DataKeys(seleccion.RowIndex).Values("DesdeOptimo")
                txtHastaOptimo.Text = gvIndicadores.DataKeys(seleccion.RowIndex).Values("HastaOptimo")
                txtDesdeAmbar.Text = gvIndicadores.DataKeys(seleccion.RowIndex).Values("DesdeAmbar")
                txtHastaAmbar.Text = gvIndicadores.DataKeys(seleccion.RowIndex).Values("HastaAmbar")
                txtDesdeRojo.Text = gvIndicadores.DataKeys(seleccion.RowIndex).Values("DesdeRojo")
                txtHastaRojo.Text = gvIndicadores.DataKeys(seleccion.RowIndex).Values("HastaRojo")

                'Tipo de dato, ubicacion
                'ddlTipoDato.SelectedValue = gvIndicadores.Rows(seleccion.RowIndex).Cells.Item(8).Text
                Select Case gvIndicadores.Rows(seleccion.RowIndex).Cells.Item(8).Text
                    Case "C"
                        ddlTipoDato.SelectedIndex = 1
                    Case "P"
                        ddlTipoDato.SelectedIndex = 2
                    Case "R"
                        ddlTipoDato.SelectedIndex = 3
                    Case Else
                        ddlTipoDato.SelectedIndex = 0
                End Select

                ddlAños.SelectedValue = gvIndicadores.Rows(seleccion.RowIndex).Cells.Item(10).Text


                Select Case gvIndicadores.DataKeys(seleccion.RowIndex).Values("direccionescala").ToString
                    Case "ASC"
                        ddlDireccionEscalaMetas.SelectedIndex = 1
                    Case "DESC"
                        ddlDireccionEscalaMetas.SelectedIndex = 2
                    Case Else
                        ddlDireccionEscalaMetas.SelectedIndex = 0
                End Select

                If gvIndicadores.DataKeys(seleccion.RowIndex).Values("codigo_top").ToString <> "" Then
                    ddlTipoOperacion.SelectedValue = gvIndicadores.DataKeys(seleccion.RowIndex).Values("codigo_top").ToString
                Else
                    ddlTipoOperacion.SelectedValue = 0
                End If

                txtBasal.Text = gvIndicadores.Rows(seleccion.RowIndex).Cells.Item(5).Text

                'Cambio nombre del boton Guardar por Modificar
                cmdGuardar.Text = "   Modificar"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarCampos()
        
        limpiarcajas()
        lblMensaje.Text = ""
        Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
        Me.avisos.Attributes.Add("class", "none")
        'Cambio nombre del boton Guardar por Modificar
        cmdGuardar.Text = "   Guardar"
    End Sub

    Private Sub limpiarcajas()
        lblCodigo.Text = ""
        txtDescripcion.Text = ""
        txtPonderacion.Text = ""
        ddlPerspectiva.SelectedValue = 0
        ddlObjetivos.Items.Clear()
        txtDesdeAmbar.Text = ""
        txtDesdeOptimo.Text = ""
        txtDesdeRojo.Text = ""
        txtHastaAmbar.Text = ""
        txtHastaOptimo.Text = ""
        txtHastaRojo.Text = ""
        txtMeta.Text = ""
        ddlTipoDato.SelectedValue = "S"
        ddlPlan.SelectedValue = 0
        ddlAños.SelectedValue = 0
        ddlDireccionEscalaMetas.SelectedValue = "-"
        txtBasal.Text = ""
        ddlTipoDato.SelectedValue = 0
        ddlTipoOperacion.SelectedValue = 0
    End Sub

    

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        LimpiarCampos()
        lblMensaje.Text = ""
    End Sub


    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            'Para Validar Caja de Busqueda
            If Page.IsValid Then        'Si no ha ingresado cadenas inválidas (select, php, script)
                ConsultarGridRegistros()
                'Dim dts As New Data.DataTable
                'Dim obj As New clsIndicadores

                'dts = obj.ConsultarIndicadorPorNombre(txtBuscar.Text)
                'gvIndicadores.DataSource = dts
                'gvIndicadores.DataBind()
            Else 'Limpia la cadena inválida de la caja de texto
                txtBuscar.Text = ""
            End If

            
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub ddlPerspectiva_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPerspectiva.SelectedIndexChanged
        Try
            If ddlPerspectiva.SelectedValue <> 0 Then
                CargarComboObjetivos()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
    '    Try
    '        Dim obj As New clsIndicadores
    '        Dim dts As New Data.DataTable

    '        dts = obj.ValidarPalabrasReservadas(args.Value.ToString.Trim)
    '        If dts.Rows(0).Item("Encontro") > 0 Then
    '            args.IsValid = False
    '        Else
    '            args.IsValid = True
    '        End If

    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try

    'End Sub

    Protected Sub CustomValidator2_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator2.ServerValidate
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

    Protected Sub ddlPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlan.SelectedIndexChanged
        Try

            If ddlPlan.SelectedValue <> 0 Then
                CargarComboPerspectivas()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlPlan2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlan2.SelectedIndexChanged
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores
            Dim dts2 As New Data.DataTable

            If ddlPlan2.SelectedValue <> 0 Then
                dts = obj.ListaAniosObjetivosBusqueda(Me.ddlPlan2.SelectedValue)
                If dts.Rows.Count > 0 Then
                    ddlAnioBus.DataSource = dts
                    ddlAnioBus.DataTextField = "Descripcion"
                    ddlAnioBus.DataValueField = "Codigo"
                    ddlAnioBus.DataBind()
                End If

                dts2 = obj.ListaObjetivosBusqueda(Me.ddlPlan2.SelectedValue, "%")
                If dts2.Rows.Count > 0 Then
                    ddlListaObjetivos.DataSource = dts2
                    ddlListaObjetivos.DataTextField = "Descripcion"
                    ddlListaObjetivos.DataValueField = "Codigo"
                    ddlListaObjetivos.DataBind()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlAnioBus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAnioBus.SelectedIndexChanged
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            If ddlPlan2.SelectedValue <> 0 Then
                dts = obj.ListaObjetivosBusqueda(Me.ddlPlan2.SelectedValue, Me.ddlAnioBus.SelectedValue)
                ddlListaObjetivos.DataSource = dts
                ddlListaObjetivos.DataTextField = "Descripcion"
                ddlListaObjetivos.DataValueField = "Codigo"
                ddlListaObjetivos.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


End Class




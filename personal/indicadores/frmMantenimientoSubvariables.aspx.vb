
Partial Class Indicadores_Formularios_frmMantenimientoSubvariables
    Inherits System.Web.UI.Page

    Dim usuario As Integer
    'Dim validacustom As Boolean = True

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ConsultarGridRegistros()
                CargarComboCentroCostos()
                CargarComboVariable()
                CargarComboFacultad()

                'Debe tomar del inicio de sesión
                usuario = Request.QueryString("id")
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
            dts = obj.ConsultarSubvariable("0")
            gvSubvariable.DataSource = dts
            gvSubvariable.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboCentroCostos()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListarCentroCostos()
            ddlCco.DataSource = dts
            ddlCco.DataTextField = "descripcion_Cco"
            ddlCco.DataValueField = "codigo_Cco"
            ddlCco.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboFacultad()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable

            dts = obj.ConsultarFacultad()
            ddlFacultad.DataSource = dts
            ddlFacultad.DataTextField = "nombre_fac"
            ddlFacultad.DataValueField = "codigo_fac"
            ddlFacultad.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboVariable()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            '=====================================================================================
            'Lista todas las variables configuradas para el usuario que ha iniciado sesion.
            '=====================================================================================
            dts = obj.ListarVariables(Request.QueryString("id"), 1)
            ddlVariable.DataSource = dts
            ddlVariable.DataTextField = "Descripcion"
            ddlVariable.DataValueField = "Codigo"
            ddlVariable.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click

        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            lblMensaje.Visible = True

            'If validacustom Then 'Si no ha ingresado cadenas inválidas (select, php, script)

            'Nuevo registro
            If lblCodigo.Text = "" Then
                lblMensaje.Text = obj.InsertarSubvariable(ddlVariable.SelectedValue, ddlCco.SelectedValue, txtDescripcion.Text, usuario, ddlFacultad.SelectedValue)
            Else
                'Edicion de registro
                lblMensaje.Text = obj.ModificarSubvariable(lblCodigo.Text, ddlCco.SelectedValue, ddlVariable.SelectedValue, txtDescripcion.Text, usuario, ddlFacultad.SelectedValue)
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

            'Else 'Limpia la cadena inválida de la caja de texto
            'txtDescripcion.Text = ""
            'End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvSubvariable_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSubvariable.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1


            'Muestra al usuario si la variable tiene registrados valores en los diferentes periodos.
            If e.Row.Cells(5).Text = "SI" Then
                e.Row.Cells(5).Text = "<center><img src='../images/y_sumatoria.png' style='border: 0px' alt='Variable con Valor'/></center>"

                'Si tiene valor asignado, no puede modifcar la subvariable
                e.Row.Cells(10).Text = "<center><img src='../images/closed.png' style='border: 0px' alt='Variable Bloqueda - Editar'/></center>"
                e.Row.Cells(11).Text = "<center><img src='../images/closed.png' style='border: 0px' alt='Variable Bloqueda - Eliminar'/></center>"
                e.Row.Cells(10).Enabled = False
                e.Row.Cells(11).Enabled = False
            Else
                e.Row.Cells(5).Text = "<center><img src='../images/N_Exist.png' style='border: 0px' alt='Variable sin valor'/></center>"
                e.Row.Cells(10).Enabled = True
                e.Row.Cells(11).Enabled = True
            End If

            Dim PerteneceFormula As Integer = CType(sender.DataKeys(e.Row.RowIndex).Item("PertenceFormula"), Integer)
            If PerteneceFormula = 1 Then

                'Editar
                e.Row.Cells(10).Text = "<center><img src='../images/closed.png' style='border: 0px' alt='Variable Bloqueda - Editar'/></center>"
                e.Row.Cells(10).Enabled = False

                'Eliminar
                e.Row.Cells(11).Text = "<center><img src='../images/closed.png' style='border: 0px' alt='Variable Bloqueda - Eliminar'/></center>"
                e.Row.Cells(11).Enabled = False
            End If


        End If
    End Sub

    Protected Sub gvCategoria_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvSubvariable.RowDeleting
        Try
            Dim obj As New clsIndicadores

            'La columna Codigo esta oculta. Por eso uso el DataKey de la fila seleccionada
            lblMensaje.Text = obj.EliminarSubvariable(gvSubvariable.DataKeys(e.RowIndex).Value.ToString())
            lblMensaje.Visible = True

            'Response.Write(gvSubvariable.DataKeys(e.RowIndex).Value.ToString())
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


    Protected Sub gvCategoria_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvSubvariable.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion                
                lblMensaje.Text = ""
                Dim seleccion As GridViewRow
                'Dim pos As Integer

                '1. Obtengo la linea del gridview que fue cliqueada
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)

                '2. Grabo el codigo en lbl oculto
                lblCodigo.Text = gvSubvariable.DataKeys(seleccion.RowIndex).Values("Codigo")

                txtDescripcion.Text = HttpUtility.HtmlDecode(Trim(gvSubvariable.Rows(seleccion.RowIndex).Cells.Item(3).Text))
                ddlVariable.SelectedValue = gvSubvariable.DataKeys(seleccion.RowIndex).Values("CodigoVar")
                ddlCco.SelectedValue = Convert.ToInt32(gvSubvariable.DataKeys(seleccion.RowIndex).Values("CodigoCC"))
                ddlFacultad.SelectedValue = Convert.ToInt32(gvSubvariable.DataKeys(seleccion.RowIndex).Values("CodigoFac"))

                'pos = InStr(1, gvSubvariable.Rows(seleccion.RowIndex).Cells.Item(1).Text, "-")
                'txtVariable.Text = HttpUtility.HtmlDecode(Trim(Left(gvSubvariable.Rows(seleccion.RowIndex).Cells.Item(1).Text, pos - 1)))
                'txtDescripcion.Text = HttpUtility.HtmlDecode(Trim(Mid(gvSubvariable.Rows(seleccion.RowIndex).Cells.Item(1).Text, pos + 1)))

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
    End Sub

    Private Sub limpiarcajas()
        lblCodigo.Text = ""
        txtDescripcion.Text = ""
        ddlCco.SelectedValue = 0
        ddlVariable.SelectedValue = "%"
        ddlFacultad.SelectedValue = 0
        'Cambio nombre del boton Guardar por Modificar
        cmdGuardar.Text = "   Guardar"
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        LimpiarCampos()
    End Sub


    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            'Para Validar Caja de Busqueda
            If Page.IsValid Then        'Si no ha ingresado cadenas inválidas (select, php, script)
                Dim dts As New Data.DataTable
                Dim obj As New clsIndicadores

                dts = obj.ConsultarSubvariablePorNombre(txtBuscar.Text)
                gvSubvariable.DataSource = dts
                gvSubvariable.DataBind()
            Else 'Limpia la cadena inválida de la caja de texto
                txtBuscar.Text = ""
            End If
            
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
    '    Try
    '        Dim cadena As String

    '        cadena = txtDescripcion.Text

    '        If InStr(1, cadena, "select") > 0 Or InStr(1, cadena, "script") > 0 Or InStr(1, cadena, "php") > 0 Then
    '            args.IsValid = False
    '            validacustom = False
    '            Exit Sub
    '        End If


    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '        'args.IsValid = False
    '        Exit Sub
    '    End Try
    '    'args.IsValid = True
    'End Sub

    Protected Sub ddlVariable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlVariable.SelectedIndexChanged
        Try
            If ddlVariable.SelectedValue <> "0" And ddlFacultad.SelectedValue <> 0 Then
                txtDescripcion.Text = ddlVariable.SelectedItem.Text & "-FAC. " & ddlFacultad.SelectedItem.Text
            Else
                txtDescripcion.Text = ""
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlFacultad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFacultad.SelectedIndexChanged
        Try
            If ddlVariable.SelectedValue <> "0" And ddlFacultad.SelectedValue <> 0 Then
                txtDescripcion.Text = ddlVariable.SelectedItem.Text & " - FAC. " & ddlFacultad.SelectedItem.Text
            Else
                txtDescripcion.Text = ""
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

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

    Protected Sub gvSubvariable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvSubvariable.SelectedIndexChanged

    End Sub
End Class




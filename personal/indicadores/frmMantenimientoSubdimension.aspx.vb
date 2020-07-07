
Partial Class Indicadores_Formularios_frmMantenimientoSubdimension
    Inherits System.Web.UI.Page

    Dim usuario As Integer



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ConsultarGridRegistros()
                CargarComboVariables()

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
            dts = obj.ConsultarSubdimension("0")
            gvSubdimension.DataSource = dts
            gvSubdimension.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboVariables()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListarVariables(0, 0)
            ddlVariable.DataSource = dts
            ddlVariable.DataTextField = "Descripcion"
            ddlVariable.DataValueField = "Codigo"
            ddlVariable.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboSubvariable()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListarSubvariables_Variable(ddlVariable.SelectedValue, Request.QueryString("id"), 0)
            ddlSubvariable.DataSource = dts
            ddlSubvariable.DataTextField = "Descripcion"
            ddlSubvariable.DataValueField = "Codigo"
            ddlSubvariable.DataBind()
            'ddlSubvariable.Items(0).Text = "--SELECCIONE--"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboDimension()
        Try            
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListarDimension_Subvariable(ddlSubvariable.SelectedValue, Request.QueryString("id"), 0)
            ddlDimension.DataSource = dts
            ddlDimension.DataTextField = "Descripcion"
            ddlDimension.DataValueField = "Codigo"
            ddlDimension.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Private Sub CargarComboNivelSubdimension()
    '    Try
    '        If ddlDimension.SelectedValue <> 0 Then
    '            Dim obj As New clsIndicadores
    '            Dim dts As New Data.DataTable
    '            dts = obj.ConsultarNivelSubConfiguradas_Dimension(ddlDimension.SelectedValue)
    '            ddlNivelSubdimension.DataSource = dts
    '            ddlNivelSubdimension.DataTextField = "Descripcion"
    '            ddlNivelSubdimension.DataValueField = "Codigo"
    '            ddlNivelSubdimension.DataBind()
    '        End If


    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    Private Sub CargarComboNivelSubdimension()
        Try
            If ddlVariable.SelectedValue <> "0" Then
                Dim obj As New clsIndicadores
                Dim dts As New Data.DataTable

                dts = obj.ListarSubdimensionesConfiguradas(ddlVariable.SelectedValue)
                ddlNivelSubdimension.DataSource = dts
                ddlNivelSubdimension.DataTextField = "Descripcion"
                ddlNivelSubdimension.DataValueField = "Codigo"
                ddlNivelSubdimension.DataBind()

            End If


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboTipoSubdimension()
        Try
            If ddlNivelSubdimension.SelectedValue <> 0 Then
                Dim obj As New clsIndicadores
                Dim dts As New Data.DataTable

                dts = obj.ConsultarTiposSubdimension(ddlNivelSubdimension.SelectedValue)
                ddlTipoSubdimension.DataSource = dts
                ddlTipoSubdimension.DataTextField = "Descripcion"
                ddlTipoSubdimension.DataValueField = "Abreviatura"
                ddlTipoSubdimension.DataBind()
                'ddlSemana.Items(0).Text = "Semestral"
            End If
            

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click

        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores
            Dim subdimension As String

            lblMensaje.Visible = True

            subdimension = txtDimension.Text.ToString + "-" + txtDescripcion.Text.ToUpper.ToString

            'Nuevo registro
            If lblCodigo.Text = "" Then
                lblMensaje.Text = obj.InsertarSubdimension(ddlDimension.SelectedValue, subdimension, ddlTipoSubdimension.SelectedValue, usuario)

            Else
                'Edicion de registro
                lblMensaje.Text = obj.ModificarSubdimension(lblCodigo.Text, ddlDimension.SelectedValue, subdimension, ddlTipoSubdimension.SelectedValue, usuario)
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


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvSubdimension_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSubdimension.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            'Muestra al usuario si la variable tiene registrados valores en los diferentes periodos.
            If e.Row.Cells(7).Text = "SI" Then
                e.Row.Cells(7).Text = "<center><img src='../images/y_sumatoria.png' style='border: 0px' alt='Variable con Valor'/></center>"

                'Si tiene valor asignado, no puede modifcar la subvariable
                e.Row.Cells(14).Text = "<center><img src='../images/closed.png' style='border: 0px' alt='Variable Bloqueda - Editar'/></center>"
                e.Row.Cells(15).Text = "<center><img src='../images/closed.png' style='border: 0px' alt='Variable Bloqueda - Eliminar'/></center>"
                e.Row.Cells(14).Enabled = False
                e.Row.Cells(15).Enabled = False
            Else
                e.Row.Cells(7).Text = "<center><img src='../images/N_Exist.png' style='border: 0px' alt='Variable sin valor'/></center>"
                e.Row.Cells(14).Enabled = True
                e.Row.Cells(15).Enabled = True
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

    Protected Sub gvCategoria_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvSubdimension.RowDeleting
        Try
            Dim obj As New clsIndicadores

            'La columna Codigo esta oculta. Por eso uso el DataKey de la fila seleccionada
            lblMensaje.Text = obj.EliminarSubdimension(gvSubdimension.DataKeys(e.RowIndex).Value.ToString())
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


    Protected Sub gvSubdimension_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvSubdimension.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion                

                Dim seleccion As GridViewRow
                'Dim pos As Integer
                '1. Obtengo la linea del gridview que fue cliqueada
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)

                '2. Obtengo el datakey de la linea que donde está el boton que cliqueé
                'lblCodigo.Text = gvDimension.DataKeys(seleccion.RowIndex).Value.ToString
                lblCodigo.Text = gvSubdimension.DataKeys(seleccion.RowIndex).Values("Codigo")

                'pos = InStr(1, gvSubdimension.Rows(seleccion.RowIndex).Cells.Item(1).Text, "-")
                'txtVariable.Text = HttpUtility.HtmlDecode(Trim(Left(gvSubdimension.Rows(seleccion.RowIndex).Cells.Item(1).Text, pos - 1)))

                ddlVariable.SelectedValue = gvSubdimension.DataKeys(seleccion.RowIndex).Values("CodigoVariable")

                'Cargar combo Subvariable
                CargarComboSubvariable()
                CargarComboNivelSubdimension()

                ddlSubvariable.SelectedValue = gvSubdimension.DataKeys(seleccion.RowIndex).Values("CodigoSub")
                ddlNivelSubdimension.SelectedValue = Convert.ToInt32(gvSubdimension.DataKeys(seleccion.RowIndex).Values("CodigoNivelSub"))

                'Cargar Combo Dimension
                CargarComboDimension()
                ddlDimension.SelectedValue = gvSubdimension.DataKeys(seleccion.RowIndex).Values("CodigoDim")

                'Cargar Combo TipoSubdimension
                CargarComboTipoSubdimension()
                ddlTipoSubdimension.SelectedValue = Convert.ToString(gvSubdimension.DataKeys(seleccion.RowIndex).Values("abreviatura_tsub"))

                'Decodifico para evitar que salgan caracteres especiales en lugar de las vocales con tilde
                txtDimension.Text = HttpUtility.HtmlDecode(gvSubdimension.Rows(seleccion.RowIndex).Cells.Item(4).Text)
                txtDescripcion.Text = gvSubdimension.DataKeys(seleccion.RowIndex).Values("TipoSub")

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
        txtDimension.Text = ""
        txtDescripcion.Text = ""

        ddlVariable.SelectedValue = "%"
        ddlSubvariable.Items.Clear()
        ddlDimension.Items.Clear()
        ddlNivelSubdimension.Items.Clear()
        ddlTipoSubdimension.Items.Clear()

        'Cambio nombre del boton Guardar por Modificar
        cmdGuardar.Text = "   Guardar"
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        LimpiarCampos()
        lblMensaje.Text = ""
    End Sub


    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            'Para Validar Caja de Busqueda
            If Page.IsValid Then        'Si no ha ingresado cadenas inválidas (select, php, script)
                Dim dts As New Data.DataTable
                Dim obj As New clsIndicadores

                dts = obj.ConsultarSubdimensionPorNombre(txtBuscar.Text)
                gvSubdimension.DataSource = dts
                gvSubdimension.DataBind()
            Else 'Limpia la cadena inválida de la caja de texto
                txtBuscar.Text = ""
            End If
           
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlNivelSubdimension_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlNivelSubdimension.SelectedIndexChanged
        Try
            If ddlNivelSubdimension.SelectedValue <> 0 Then
                CargarComboTipoSubdimension()
            Else
                ddlTipoSubdimension.Items.Clear()
            End If
            txtDimension.Text = ""
            txtDescripcion.Text = ""
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlVariable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlVariable.SelectedIndexChanged
        Try
            If ddlVariable.SelectedValue <> "0" Then
                CargarComboSubvariable()
                CargarComboNivelSubdimension()
            Else
                ddlSubvariable.Items.Clear()
            End If

            'Limpiar los otros combos            
            ddlDimension.Items.Clear()
            ddlTipoSubdimension.Items.Clear()

            txtDescripcion.Text = ""
            txtDimension.Text = ""

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlSubvariable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubvariable.SelectedIndexChanged
        Try
            If ddlSubvariable.SelectedValue <> "0" Then
                CargarComboDimension()
            Else
                ddlDimension.Items.Clear()
            End If
            txtDimension.Text = ""
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub ddlTipoSubdimension_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoSubdimension.SelectedIndexChanged
        Try
            If ddlTipoSubdimension.SelectedValue <> "" And ddlNivelSubdimension.SelectedValue <> 0 And ddlDimension.SelectedValue <> "0" And ddlSubvariable.SelectedValue <> "0" And ddlVariable.SelectedValue <> "0" Then
                txtDimension.Text = ddlDimension.SelectedItem.Text
                txtDescripcion.Text = ddlTipoSubdimension.SelectedItem.Text
            Else
                txtDescripcion.Text = ""
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlDimension_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDimension.SelectedIndexChanged
        Try
            If ddlDimension.SelectedValue <> "0" Then
                txtDimension.Text = ddlDimension.SelectedItem.Text
            Else
                txtDimension.Text = ""
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
End Class






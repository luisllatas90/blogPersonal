
Partial Class Indicadores_Formularios_frmMantenimientoCategorias
    Inherits System.Web.UI.Page

    Dim usuario As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ConsultarGridRegistros()

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
            dts = obj.ConsultarCategoria(0)
            gvCategoria.DataSource = dts
            gvCategoria.DataBind()
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
                'Nuevo registro
                If lblCodigo.Text = "" Then
                    lblMensaje.Text = obj.InsertarCategoria(txtDescripcion.Text.ToUpper, usuario)
                Else
                    'Edicion de registro
                    lblMensaje.Text = obj.ModificarCategoria(txtDescripcion.Text.ToUpper, usuario, lblCodigo.Text)
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

    Protected Sub gvCategoria_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCategoria.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim fila As Data.DataRowView
            'fila = e.Row.DataItem
            'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            'e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub gvCategoria_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvCategoria.RowDeleting
        Try
            Dim obj As New clsIndicadores

            'lblMensaje.Text = obj.EliminarCategoria(gvCategoria.Rows(e.RowIndex).Cells(0).Text)

            'La columna Codigo esta oculta. Por eso uso el DataKey de la fila seleccionada

            lblMensaje.Text = obj.EliminarCategoria(gvCategoria.DataKeys(e.RowIndex).Value.ToString())
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


    Protected Sub gvCategoria_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCategoria.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion                

                Dim seleccion As GridViewRow
                '1. Obtengo la linea del gridview que fue cliqueada
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)

                '2. Obtengo el datakey de la linea que donde está el boton que cliqueé
                lblCodigo.Text = gvCategoria.DataKeys(seleccion.RowIndex).Value.ToString
                txtDescripcion.Text = gvCategoria.Rows(seleccion.RowIndex).Cells.Item(2).Text

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub RestablecerCampos()
        limpiarcajas()
        lblMensaje.Text = ""
        Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
        Me.avisos.Attributes.Add("class", "none")
    End Sub

    Private Sub limpiarcajas()
        lblCodigo.Text = ""
        txtDescripcion.Text = ""
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        RestablecerCampos()
    End Sub
    Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
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

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            'Para Validar Caja de Busqueda
            If Page.IsValid Then        'Si no ha ingresado cadenas inválidas (select, php, script)
                Dim dts As New Data.DataTable
                Dim obj As New clsIndicadores

                dts = obj.ConsultarCategoriaPorNombre(txtBuscar.Text)
                gvCategoria.DataSource = dts
                gvCategoria.DataBind()
            Else 'Limpia la cadena inválida de la caja de texto
                txtBuscar.Text = ""
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
End Class


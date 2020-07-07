
Partial Class Indicadores_Formularios_frmMantenimientoDimension
    Inherits System.Web.UI.Page

    Dim usuario As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ConsultarGridRegistros()
                CargarComboCentroCostos()
                CargarComboSubvariable()
                CargarComboTipoDimension()                

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
            dts = obj.ConsultarDimension("0")
            gvDimension.DataSource = dts
            gvDimension.DataBind()            
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
            'ddlSemana.Items(0).Text = "Semestral"

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboSubvariable()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListarSubvariables

            ddlSubvariable.DataSource = dts
            ddlSubvariable.DataTextField = "Descripcion"
            ddlSubvariable.DataValueField = "Codigo"
            ddlSubvariable.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboTipoDimension()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListarTipoDimension
            ddlTipoDimension.DataSource = dts
            ddlTipoDimension.DataTextField = "Descripcion"
            ddlTipoDimension.DataValueField = "Abreviatura"
            ddlTipoDimension.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboDpto()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            dts = obj.ConsultarDptoAcademico
            ddlDptoEsc.DataSource = dts
            ddlDptoEsc.DataTextField = "DptoAcademico"
            ddlDptoEsc.DataValueField = "codigo"
            ddlDptoEsc.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboEscuela()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            dts = obj.ConsultarCarreraProfesional
            ddlDptoEsc.DataSource = dts
            ddlDptoEsc.DataTextField = "nombre_cpf"
            ddlDptoEsc.DataValueField = "codigo_cpf"
            ddlDptoEsc.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click

        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores            
            Dim codigo_cpf As Integer
            Dim codigo_dac As Integer

            lblMensaje.Visible = True

            If ddlTipoDimension.SelectedValue = "D" Then
                codigo_dac = ddlDptoEsc.SelectedValue
                codigo_cpf = 0
            End If

            If ddlTipoDimension.SelectedValue = "E" Then
                codigo_dac = 0
                codigo_cpf = ddlDptoEsc.SelectedValue
            End If

            'Nuevo registro
            If lblCodigo.Text = "" Then
                lblMensaje.Text = obj.InsertarDimension(ddlCco.SelectedValue, ddlSubvariable.SelectedValue, ddlTipoDimension.SelectedValue, txtDescripcion.Text, usuario, codigo_cpf, codigo_dac)

            Else
                'Edicion de registro
                lblMensaje.Text = obj.ModificarDimension(lblCodigo.Text, ddlCco.SelectedValue, ddlSubvariable.SelectedValue, ddlTipoDimension.SelectedValue, txtDescripcion.Text, usuario, codigo_cpf, codigo_dac)
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

    Protected Sub gvDimension_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDimension.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            'Muestra al usuario si la variable tiene registrados valores en los diferentes periodos.
            If e.Row.Cells(6).Text = "SI" Then
                e.Row.Cells(6).Text = "<center><img src='../images/y_sumatoria.png' style='border: 0px' alt='Variable con Valor'/></center>"

                'Si tiene valor asignado, no puede modifcar la subvariable
                e.Row.Cells(13).Text = "<center><img src='../images/closed.png' style='border: 0px' alt='Variable Bloqueda - Editar'/></center>"
                e.Row.Cells(14).Text = "<center><img src='../images/closed.png' style='border: 0px' alt='Variable Bloqueda - Eliminar'/></center>"
                e.Row.Cells(13).Enabled = False
                e.Row.Cells(14).Enabled = False
            Else
                e.Row.Cells(6).Text = "<center><img src='../images/N_Exist.png' style='border: 0px' alt='Variable sin valor'/></center>"
                e.Row.Cells(13).Enabled = True
                e.Row.Cells(14).Enabled = True
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

    Protected Sub gvCategoria_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDimension.RowDeleting
        Try
            Dim obj As New clsIndicadores

            'La columna Codigo esta oculta. Por eso uso el DataKey de la fila seleccionada
            lblMensaje.Text = obj.EliminarDimension(gvDimension.DataKeys(e.RowIndex).Value.ToString())
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


    Protected Sub gvCategoria_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDimension.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion                

                lblMensaje.Text = ""
                Dim seleccion As GridViewRow
                'Dim pos As Integer
                Dim codigo_cpf As Integer
                Dim codigo_dac As Integer

                '1. Obtengo la linea del gridview que fue cliqueada
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)

                '2. Obtengo el datakey de la linea que donde está el boton que cliqueé                
                lblCodigo.Text = gvDimension.DataKeys(seleccion.RowIndex).Values("Codigo")

                'pos = InStr(1, gvDimension.Rows(seleccion.RowIndex).Cells.Item(1).Text, "-")
                'txtVariable.Text = HttpUtility.HtmlDecode(Trim(Left(gvDimension.Rows(seleccion.RowIndex).Cells.Item(1).Text, pos - 1)))
                'txtDescripcion.Text = HttpUtility.HtmlDecode(Trim(Mid(gvDimension.Rows(seleccion.RowIndex).Cells.Item(1).Text, pos + 1)))
                txtDescripcion.Text = HttpUtility.HtmlDecode(Trim(gvDimension.Rows(seleccion.RowIndex).Cells.Item(3).Text))

                '3. Decodifico para evitar que salgan caracteres especiales en lugar de las vocales con tilde
                ddlTipoDimension.SelectedValue = Convert.ToString(gvDimension.DataKeys(seleccion.RowIndex).Values("AbreviaturaTipoDim"))
                ddlSubvariable.SelectedValue = gvDimension.DataKeys(seleccion.RowIndex).Values("CodigoSub")
                ddlCco.SelectedValue = Convert.ToInt32(gvDimension.DataKeys(seleccion.RowIndex).Values("CodigoCC"))

                '4.
                codigo_cpf = Convert.ToInt32(gvDimension.DataKeys(seleccion.RowIndex).Values("codigo_cpf"))
                codigo_dac = Convert.ToInt32(gvDimension.DataKeys(seleccion.RowIndex).Values("codigo_dac"))

                If codigo_cpf <> "0" Then
                    CargarComboEscuela()
                    lblEscuelaDpto.Text = "Escuela"
                    ddlDptoEsc.SelectedValue = codigo_cpf
                End If

                If codigo_dac <> "0" Then
                    CargarComboDpto()
                    lblEscuelaDpto.Text = "Departamento"
                    ddlDptoEsc.SelectedValue = codigo_dac
                End If

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
        ddlSubvariable.SelectedValue = 0
        ddlCco.SelectedValue = 0
        ddlTipoDimension.SelectedValue = 0
        ddlDptoEsc.Items.Clear()

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

                dts = obj.ConsultarDimensionPorNombre(txtBuscar.Text)
                gvDimension.DataSource = dts
                gvDimension.DataBind()
            Else 'Limpia la cadena inválida de la caja de texto
                txtBuscar.Text = ""
            End If
          
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

       Protected Sub ddlsubvariable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubvariable.SelectedIndexChanged
        Try
            If ddlSubvariable.SelectedValue <> "0" Then
                If ddlTipoDimension.SelectedValue = "D" Then
                    If ddlDptoEsc.SelectedValue <> 0 Then
                        txtDescripcion.Text = HttpUtility.HtmlDecode(Trim(Left(ddlSubvariable.SelectedItem.Text, InStr(1, ddlSubvariable.SelectedItem.Text, "-") - 1))) & " - " & ddlDptoEsc.SelectedItem.Text
                    End If
                ElseIf ddlTipoDimension.SelectedValue = "E" Then
                    If ddlDptoEsc.SelectedValue <> 0 Then
                        txtDescripcion.Text = HttpUtility.HtmlDecode(Trim(Left(ddlSubvariable.SelectedItem.Text, InStr(1, ddlSubvariable.SelectedItem.Text, "-") - 1))) & " - ES. " & ddlDptoEsc.SelectedItem.Text
                    End If
                Else
                    txtDescripcion.Text = ""
                End If
            Else
                txtDescripcion.Text = ""
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlTipoDimension_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoDimension.SelectedIndexChanged
        Try
            If ddlTipoDimension.SelectedItem.Text = "Departamento" Then
                CargarComboDpto()
                lblEscuelaDpto.Text = "Departamento"
            End If

            If ddlTipoDimension.SelectedItem.Text = "Escuela" Then
                CargarComboEscuela()
                lblEscuelaDpto.Text = "Escuela"
            End If

            If ddlTipoDimension.SelectedItem.Text = "--SELECCIONE--" Then
                ddlDptoEsc.Items.Clear()
                lblEscuelaDpto.Text = "Escuela/Departamento"
                txtDescripcion.Text = ""
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlDptoEsc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDptoEsc.SelectedIndexChanged
        Try
            If ddlDptoEsc.SelectedValue <> 0 And ddlSubvariable.SelectedValue <> "0" Then
                If ddlTipoDimension.SelectedValue = "E" Then
                    txtDescripcion.Text = HttpUtility.HtmlDecode(Trim(Left(ddlSubvariable.SelectedItem.Text, InStr(1, ddlSubvariable.SelectedItem.Text, "-") - 1))) & " - ES. " & ddlDptoEsc.SelectedItem.Text
                Else
                    txtDescripcion.Text = HttpUtility.HtmlDecode(Trim(Left(ddlSubvariable.SelectedItem.Text, InStr(1, ddlSubvariable.SelectedItem.Text, "-") - 1))) & " - " & ddlDptoEsc.SelectedItem.Text
                End If

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
End Class





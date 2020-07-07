
Partial Class indicadores_frmNivelAccesoVariablesCeco
    Inherits System.Web.UI.Page

    Dim codigo_ceco As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargarCategoria()
                CargarListaUnidadNegocio()

                ddlSubunidadNegocio.Enabled = False
                ddlVariable.Enabled = False
                btnAsignar.Enabled = False
                btnTodo.Enabled = False
                'btnEliminarCecos.Enabled = False

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarListaUnidadNegocio()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListaUnidadNegocio_cnf
            If dts.Rows.Count > 0 Then
                ddlUnidadNegocio.DataSource = dts
                ddlUnidadNegocio.DataTextField = "Descripcion"
                ddlUnidadNegocio.DataValueField = "Codigo"
                ddlUnidadNegocio.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarListaVariables(ByVal codigo_cat As Integer)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListaVariables_cnf(codigo_cat)
            If dts.Rows.Count > 0 Then
                ddlVariable.DataSource = dts
                ddlVariable.DataTextField = "Descripcion"
                ddlVariable.DataValueField = "Codigo"
                ddlVariable.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarCategoria()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListaCaregoria_cnf
            If dts.Rows.Count > 0 Then
                ddlCategoria.DataSource = dts
                ddlCategoria.DataTextField = "Descripcion"
                ddlCategoria.DataValueField = "Codigo"
                ddlCategoria.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub ddlCategoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategoria.SelectedIndexChanged
        Try
            If ddlCategoria.SelectedValue <> 0 Then
                CargarListaVariables(ddlCategoria.SelectedValue)
                ddlVariable.Enabled = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlUnidadNegocio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUnidadNegocio.SelectedIndexChanged
        Try
            If ddlUnidadNegocio.SelectedValue <> 0 Then
                CargarListaSubUnidadNegocio(ddlUnidadNegocio.SelectedValue)
                ddlSubunidadNegocio.Enabled = True

                lblMensaje.Visible = False
                lblMensaje.Text = ""
                Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
                Me.avisos.Attributes.Add("class", "none")

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarListaSubUnidadNegocio(ByVal codigo_uneg As Integer)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListaSubUnidadNegocio_cnf(codigo_uneg)
            If dts.Rows.Count > 0 Then
                ddlSubunidadNegocio.DataSource = dts
                ddlSubunidadNegocio.DataTextField = "Descripcion"
                ddlSubunidadNegocio.DataValueField = "Codigo"
                ddlSubunidadNegocio.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub ddlSubunidadNegocio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubunidadNegocio.SelectedIndexChanged
        Try
            ListaCentroCostoParametros()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub ListaCentroCostoParametros()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            If txtBusceco.Text.Trim <> "" Then
                Busqueda()
            Else
                If ddlUnidadNegocio.SelectedValue <> 0 And ddlSubunidadNegocio.SelectedValue <> 0 Then
                    dts = obj.ListaCecos_cnf(ddlUnidadNegocio.SelectedValue, ddlSubunidadNegocio.SelectedValue)
                    If dts.Rows.Count > 0 Then
                        gvListaCecos.DataSource = dts
                        gvListaCecos.DataBind()
                    Else
                        gvListaCecos.DataSource = Nothing
                        gvListaCecos.DataBind()
                    End If
                End If
            End If
            
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaCecos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvListaCecos.PageIndexChanging
        Try
            gvListaCecos.PageIndex = e.NewPageIndex
            ListaCentroCostoParametros()
            btnBuscarCecos_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaCecos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaCecos.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion   
                Dim seleccion As GridViewRow
                'Dim codigo_seleccion As Integer
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                codigo_ceco = Convert.ToInt32(gvListaCecos.DataKeys(seleccion.RowIndex).Values("Codigo"))
                lblCodigoCeco.Text = codigo_ceco

        

                If lblCodigoCeco.Text <> "" Then
                    lblMensaje.Visible = False
                    lblMensaje.Text = ""
                    Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
                    Me.avisos.Attributes.Add("class", "none")

                    btnAsignar.Enabled = True
                    btnEliminarCecos.Enabled = True
                    btnTodo.Enabled = True

                End If

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaCecos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaCecos.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Cells(1).Text = e.Row.RowIndex + 1

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlVariable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlVariable.SelectedIndexChanged
        Try
            ListaVariables()

            lblMensaje.Visible = False
            lblMensaje.Text = ""
            Me.Image1.Attributes.Add("src", "../Images/beforelastchild.GIF")
            Me.avisos.Attributes.Add("class", "none")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ListaVariables()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            'Response.Write(ddlVariable.SelectedValue)
            If ddlVariable.SelectedValue <> "0" Then
                dts = obj.ListaJerarquiaVariables_cnf(ddlVariable.SelectedValue)
                If dts.Rows.Count > 0 Then
                    gvJerarquiaVariables.DataSource = dts
                    gvJerarquiaVariables.DataBind()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvJerarquiaVariables_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvJerarquiaVariables.RowCommand
        Try
            Dim seleccion As GridViewRow
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)

            Dim codigo_jer As String = gvJerarquiaVariables.DataKeys(seleccion.RowIndex).Values("Codigo").ToString
            Dim nivel As Integer = gvJerarquiaVariables.DataKeys(seleccion.RowIndex).Values("Nivel").ToString
            '----------------------------------------------------------------------------------------------'

            If e.CommandName = "EliminaCeco" Then
                dts = obj.EliminaNivelAccesoVariable(codigo_jer, nivel)
                mensaje(dts.Rows(0).Item("rpt"), dts.Rows(0).Item("mensaje").ToString)
            End If

            '----------------------------------------------------------------------------------------------'
            If e.CommandName = "Addceco" Then
                If lblCodigoCeco.Text <> "" Then
                    codigo_ceco = CType(lblCodigoCeco.Text, Integer)
                    Select Case nivel
                        Case 1
                            dts = obj.InsertaNivelAccesoVariable(codigo_ceco, nivel, codigo_jer, "0", "0", "0", Request.QueryString("id"))
                            mensaje(dts.Rows(0).Item("rpt"), dts.Rows(0).Item("mensaje").ToString)
                            'Response.Write("<script>alert('" + errorMessage + "')</script>");
                        Case 2
                            dts = obj.InsertaNivelAccesoVariable(codigo_ceco, nivel, "0", codigo_jer, "0", "0", Request.QueryString("id"))
                            mensaje(dts.Rows(0).Item("rpt"), dts.Rows(0).Item("mensaje").ToString)
                        Case 3
                            dts = obj.InsertaNivelAccesoVariable(codigo_ceco, nivel, "0", "0", codigo_jer, "0", Request.QueryString("id"))
                            mensaje(dts.Rows(0).Item("rpt"), dts.Rows(0).Item("mensaje").ToString)
                        Case 4
                            dts = obj.InsertaNivelAccesoVariable(codigo_ceco, nivel, "0", "0", "0", codigo_jer, Request.QueryString("id"))
                            mensaje(dts.Rows(0).Item("rpt"), dts.Rows(0).Item("mensaje").ToString)
                    End Select
                Else
                    lblMensaje.Visible = True
                    lblMensaje.Text = "Seleccionar un Centro de Costo para la Variable."
                    Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                    Me.avisos.Attributes.Add("class", "mensajeError")
                End If
            End If


            ListaVariables()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub mensaje(ByVal rpt As Integer, ByVal mensaje As String)
        Try
            If rpt > 0 Then
                lblMensaje.Visible = True
                lblMensaje.Text = mensaje
                Me.Image1.Attributes.Add("src", "../Images/accept.png")
                Me.avisos.Attributes.Add("class", "mensajeExito")
            Else
                lblMensaje.Visible = True
                lblMensaje.Text = mensaje
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvJerarquiaVariables_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvJerarquiaVariables.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim nombre_var As String = ""

                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                'e.Row.Cells(1).Text = e.Row.RowIndex + 1

                e.Row.Cells(1).Text = "<a href='frmListaCecosConfiguradosVariables.aspx?codigo_var=" & e.Row.Cells(2).Text & "&nivel=" & e.Row.Cells(3).Text & "&KeepThis=true&TB_iframe=true&height=600&width=800&modal=true' title='Ver Detalle' class='thickbox'><img src='../images/lista_cecos_variable.png' border=0 /><a/>"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

  
    Protected Sub btnBuscarCecos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarCecos.Click
        Try
            Busqueda()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub Busqueda()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            Dim UnidadNegocio As String = ""
            Dim SubUnidadNegocio As String = ""

            If ddlUnidadNegocio.SelectedValue = 0 Then
                UnidadNegocio = "%"
                SubUnidadNegocio = "%"
            Else
                UnidadNegocio = ddlUnidadNegocio.Text.Trim
            End If

            If SubUnidadNegocio <> "%" Then
                If ddlSubunidadNegocio.SelectedValue = 0 Then
                    SubUnidadNegocio = "%"
                Else
                    SubUnidadNegocio = ddlSubunidadNegocio.Text.Trim
                End If
            End If


            dts = obj.BurcarCeco(txtBusceco.Text.Trim, "%", "%")
            If dts.Rows.Count > 0 Then
                gvListaCecos.DataSource = dts
                gvListaCecos.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAsignar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAsignar.Click
        Try
            If (validaCheckActivo() = True) Then
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = "Las variables fueron asignadas al centro de costo seleccionado."
                Me.Image1.Attributes.Add("src", "../Images/accept.png")
                Me.avisos.Attributes.Add("class", "mensajeExito")

                ListaVariables()

            Else
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = "Error al asignar, las variables al centro de costo actual, seleccione el centro de costo."
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
            End If

            'ListaCentroCostoParametros()
            ddlSubunidadNegocio_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function validaCheckActivo() As Boolean
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            Dim Fila As GridViewRow
            Dim sw As Byte = 0
            Dim codigo_jer As String
            Dim nivel As Integer

            For i As Integer = 0 To gvJerarquiaVariables.Rows.Count - 1
                'Capturamos las filas que estan activas
                Fila = gvJerarquiaVariables.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked

                If (valor = True) Then
                    sw = 1
                    'codigo_jer = gvJerarquiaVariables.DataKeys(Fila.RowIndex).Value
                    codigo_jer = (gvJerarquiaVariables.DataKeys(Fila.RowIndex).Values("Codigo").ToString)
                    nivel = Convert.ToInt32(gvJerarquiaVariables.DataKeys(Fila.RowIndex).Values("Nivel"))

                    codigo_ceco = CType(lblCodigoCeco.Text, Integer)
                    If codigo_ceco <> 0 Then
                        Select Case nivel
                            Case 1
                                dts = obj.InsertaNivelAccesoVariable(codigo_ceco, nivel, codigo_jer, "0", "0", "0", Request.QueryString("id"))
                            Case 2
                                dts = obj.InsertaNivelAccesoVariable(codigo_ceco, nivel, "0", codigo_jer, "0", "0", Request.QueryString("id"))
                            Case 3
                                dts = obj.InsertaNivelAccesoVariable(codigo_ceco, nivel, "0", "0", codigo_jer, "0", Request.QueryString("id"))
                            Case 4
                                dts = obj.InsertaNivelAccesoVariable(codigo_ceco, nivel, "0", "0", "0", codigo_jer, Request.QueryString("id"))
                        End Select
                    End If
                End If
            Next

            If (sw = 1) Then
                Return True
            End If

            Return False

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function


    Protected Sub btnEliminarCecos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminarCecos.Click
        Try
            If (validaCheckActivoElimna() = True) Then
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = "Se elimino el centro de costo configurado para las variables seleccionadas"
                Me.Image1.Attributes.Add("src", "../Images/accept.png")
                Me.avisos.Attributes.Add("class", "mensajeExito")

                ListaVariables()

            Else
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = "Error al eliminar, favor de seleccionar una o mas variables."
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Function validaCheckActivoElimna() As Boolean
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            Dim Fila As GridViewRow
            Dim sw As Byte = 0
            Dim codigo_jer As String
            Dim nivel As Integer

            For i As Integer = 0 To gvJerarquiaVariables.Rows.Count - 1
                'Capturamos las filas que estan activas
                Fila = gvJerarquiaVariables.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked

                If (valor = True) Then
                    sw = 1
                    codigo_jer = gvJerarquiaVariables.DataKeys(Fila.RowIndex).Value
                    nivel = Convert.ToInt32(gvJerarquiaVariables.DataKeys(Fila.RowIndex).Values("Nivel"))

                    dts = obj.EliminaNivelAccesoVariable(codigo_jer, nivel)
                End If
            Next

            If (sw = 1) Then
                Return True
            End If

            Return False

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Protected Sub btnTodo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTodo.Click
        Try
            If lblCodigoCeco.Text <> "" Then
                Dim obj As New clsIndicadores
                Dim dts As New Data.DataTable

                dts = obj.PermisoGeneralVariables(CType(lblCodigoCeco.Text.Trim, Integer), Request.QueryString("id"))
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = "El centro de costo seleccionado tiene permiso para todas las variables registradas."
                Me.Image1.Attributes.Add("src", "../Images/accept.png")
                Me.avisos.Attributes.Add("class", "mensajeExito")

                ListaVariables()
            Else
                lblMensaje.Visible = True
                lblMensaje.Text = "Seleccionar un Centro de Costo para la Variable."
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvJerarquiaVariables_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvJerarquiaVariables.SelectedIndexChanged

    End Sub

    Protected Sub gvListaCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvListaCecos.SelectedIndexChanged

    End Sub
End Class


Partial Class frmatencionasignarespFC
    Inherits System.Web.UI.Page
#Region "variables"

#End Region
#Region "eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../sinacceso.html")
        End If
        Try
            If IsPostBack = False Then
                Me.actualiza.Visible = "false"
                'mt_consultarEscalas(Me.ddlEstado.SelectedValue) 'Llena la tabla
                escuelas() 'Ceci
                listar()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub listar()
        mt_consultarEscalas(Me.ddlEstado.SelectedValue) 'Llena la tabla
    End Sub
    Protected Sub gvListaEscalas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaEscalas.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../sinacceso.html")
        End If
        Try
            Me.hfCodigo_ecs.Value = Me.gvListaEscalas.DataKeys(e.CommandArgument).Values("codigo_ecs")
            If (e.CommandName = "Editar") Then
                Call mt_limpiarMensaje()
                'Call mt_consultarEscalaPorCodigo_ecs(hfCodigo_ecs.Value)
                Call mt_mostrarActualizar()
                'ElseIf (e.CommandName = "Desactivar") Then
                '    Call mt_ActualizaEscala(hfCodigo_ecs.Value, "SOLO ESTADO", "", "1", 0, 0)
                '    Me.ddlEstado.SelectedValue = -1
                '    Call mt_consultarEscalas(Me.ddlEstado.SelectedValue)
                'ElseIf (e.CommandName = "Activar") Then
                '    Call mt_limpiarMensaje()
                '    Call mt_consultarEscalaPorCodigo_ecs(hfCodigo_ecs.Value)
                '    Call mt_mostrarActualizar()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Protected Sub gvListaEscalas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaEscalas.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.TableSection = TableRowSection.TableHeader
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim lsDataKeyValue As String = gvListaEscalas.DataKeys(e.Row.RowIndex).Values("eliminado_ecs").ToString()
                Dim lnkActiva As LinkButton = (CType(e.Row.Cells(5).FindControl("btnActivar"), LinkButton))
                Dim lnkDesactiva As LinkButton = (CType(e.Row.Cells(5).FindControl("btnDesactivar"), LinkButton))

                If lsDataKeyValue = 1 Then
                    lnkActiva.Visible = True
                    lnkDesactiva.Visible = False
                Else
                    lnkActiva.Visible = False
                    lnkDesactiva.Visible = True
                End If


            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    'Protected Sub btnActualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActualizar.Click
    '    If (Session("id_per") Is Nothing) Then
    '        Response.Redirect("../sinacceso.html")
    '    End If
    '    Try
    '        If mt_validaFormulario() = True Then
    '            Dim dt As New Data.DataTable
    '            Dim dtValida As New Data.DataTable
    '            '''' lo desactivo momentaneamente para realizar la validación
    '            If hfCodigo_ecs.Value <> "0" Then
    '                'mt_ActualizaEscala(Me.hfCodigo_ecs.Value, "SOLO ESTADO", Me.ddlCondicion.SelectedValue, "1", Me.txtValorMinimo.Text, Me.txtValorMáximo.Text)
    '            End If
    '            'dtValida = mt_ValidaRangoCalifiacion(Me.txtValorMinimo.Text, Me.txtValorMáximo.Text)
    '            If dtValida.Rows.Count > 0 Then
    '                If dtValida.Rows(0).Item("respuesta") = "1" Then
    '                    'dt = mt_ActualizaEscala(Me.hfCodigo_ecs.Value, Me.txtDetalle.Text, Me.ddlCondicion.SelectedValue, "0", Me.txtValorMinimo.Text, Me.txtValorMáximo.Text)
    '                    If dt.Rows.Count > 0 Then
    '                        If dt.Rows(0).Item("Respuesta") = 1 Then
    '                            'Call mt_validaFormulario()
    '                            Me.lblmensaje.Attributes.Add("class", "alert alert-success")
    '                            Me.lblmensaje.InnerHtml() = "<p>Se modificó la escala con éxito</p>"
    '                            'mt_limpiaControles()
    '                            Me.ddlEstado.SelectedValue = -1
    '                            mt_consultarEscalas(Me.ddlEstado.SelectedValue)
    '                        End If
    '                    End If
    '                Else
    '                    Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
    '                    Me.lblmensaje.InnerHtml() = "<p>El rango de notas se encuentra fuera de intervalos disponibles</p>"
    '                    Exit Sub
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Response.Write(ex.Message.ToString)
    '    End Try

    'End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        'Call mt_limpiaControles()
        'Me.hfCodigo_ecs.Value = "0"
        'Call mt_limpiarMensaje()
        'Call mt_mostrarActualizar()
        listar()
    End Sub
    Protected Sub btnResgresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResgresar.Click
        Call mt_limpiaControles()
        Call mt_limpiaControles()
        Call mt_ocultarActualizar()
    End Sub
    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        Call mt_consultarEscalas(Me.ddlEstado.SelectedValue)
    End Sub
#End Region

#Region "procedimiento y funciones"

    Private Sub mt_consultarEscalas(ByVal estado As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ListaEscalaCalificacionSustentacion", "", 0, estado)
        If dt.Rows.Count > 0 Then
            Me.gvListaEscalas.DataSource = dt
            Me.gvListaEscalas.DataBind()
        Else
            Me.gvListaEscalas.DataSource = Nothing
            Me.gvListaEscalas.DataBind()
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub escuelas()

        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("ConsultarEscuelaPorPersonal", 3, Session("id_per"), 2)
            obj.CerrarConexion()

            Me.cboEscuela.DataTextField = "nombre_cpf"
            Me.cboEscuela.DataValueField = "codigo_cpf"
            Me.cboEscuela.DataSource = dt
            Me.cboEscuela.DataBind()

        Catch ex As Exception
            'Me.lblMensaje.Text = "Error al cargar las Escuelas"
        End Try

    End Sub

    Private Sub mt_limpiarMensaje()
        Me.lblmensaje.Attributes.Remove("class")
        Me.lblmensaje.InnerHtml = ""
    End Sub
    'Private Sub mt_consultarEscalaPorCodigo_ecs(ByVal codigo_ecs As Integer)
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim dt As New Data.DataTable
    '    dt = obj.TraerDataTable("SUST_ListaEscalaCalificacionSustentacion", "XCOD", codigo_ecs, "0")
    '    If dt.Rows.Count > 0 Then
    '        Me.txtDetalle.Text = dt.Rows(0).Item("descripcion_ecs").ToString
    '        'Me.txtValorMinimo.Text = dt.Rows(0).Item("valorminimo_ecs")
    '        'Me.txtValorMáximo.Text = dt.Rows(0).Item("valormaximo_ecs")
    '        Me.ddlCondicion.SelectedValue = dt.Rows(0).Item("condicion_ecs")
    '    End If
    '    obj.CerrarConexion()
    'End Sub
    Private Function mt_validaFormulario() As Boolean
        If Me.hfCodigo_ecs.Value = "" Then
            Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
            Me.lblmensaje.InnerHtml() = "<p>Seleccione un concepto de  la tabla</p>"            
            Return False
        End If
        If Me.cboResponsable.SelectedValue = "" Then
            Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
            Me.lblmensaje.InnerHtml() = "<p>Seleccione un responsable</p>"

            Return False
        End If
        'If Me.txtValorMinimo.Text = "" Then
        '    Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
        '    Me.lblmensaje.InnerHtml() = "<p>Ingrese el valor mínimo</p>"
        '    Me.txtValorMinimo.Focus()
        '    Return False
        'End If
        'If Me.txtValorMáximo.Text = "" Then
        '    Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
        '    Me.lblmensaje.InnerHtml() = "<p>Ingrese el valor máximo</p>"
        '    Me.txtValorMáximo.Focus()
        '    Return False
        'End If
        'If (Val(Me.txtValorMáximo.Text) < Val(Me.txtValorMinimo.Text)) Or (Val(Me.txtValorMinimo.Text) > Val(Me.txtValorMáximo.Text)) Then
        '    Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
        '    Me.lblmensaje.InnerHtml() = "<p>Valores inválidos</p>"
        '    Me.txtValorMinimo.Focus()
        '    Return False
        'End If
        'If (Val(Me.txtValorMinimo.Text) < 0 Or Val(Me.txtValorMinimo.Text) > 20) Then
        '    Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
        '    Me.lblmensaje.InnerHtml() = "<p>Valor mínimo inválido</p>"
        '    Me.txtValorMinimo.Focus()
        '    Return False
        'End If
        'If (Val(Me.txtValorMáximo.Text) > 20 Or Val(Me.txtValorMáximo.Text) < 0) Then
        '    Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
        '    Me.lblmensaje.InnerHtml() = "<p>Valor máximo inválido</p>"
        '    Me.txtValorMinimo.Focus()
        '    Return False
        'End If
        Return True
    End Function
    'Private Function mt_ActualizaEscala(ByVal codigo_ecs As Integer, ByVal descripcion_ecs As String, ByVal condicion_ecs As String, ByVal eliminado_ecs As String, ByVal valorMinimo_ecs As Integer, ByVal valorMaximo As Integer) As Data.DataTable
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim dt As New Data.DataTable
    '    dt = obj.TraerDataTable("SUST_ActualizaEscalaCalificacionSustentacion", codigo_ecs, descripcion_ecs, condicion_ecs, eliminado_ecs, valorMinimo_ecs, valorMaximo, Session("id_per"))
    '    obj.CerrarConexion()
    '    Return dt
    'End Function
    Private Sub mt_limpiaControles()
        'Me.txtDetalle.Text = String.Empty      
        Me.cboResponsable.SelectedValue = ""
        Me.hfCodigo_ecs.Value = ""
    End Sub
    Private Sub mt_mostrarActualizar()
        Me.lista.Visible = False
        Me.actualiza.Visible = True
    End Sub
    Private Sub mt_ocultarActualizar()
        Me.lista.Visible = True
        Me.actualiza.Visible = False
    End Sub
    'Private Function mt_ValidaRangoCalifiacion(ByVal valMin As Integer, ByVal valMaximo As Integer) As Data.DataTable
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim dt As New Data.DataTable
    '    dt = obj.TraerDataTable("SUST_ValidaRangoEscalaSust", valMin, valMaximo)
    '    obj.CerrarConexion()
    '    Return dt
    'End Function

#End Region








End Class

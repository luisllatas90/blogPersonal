
Partial Class GestionEgresado_SustentacionTesis_FrmConfigTiempoAccion
    Inherits System.Web.UI.Page
#Region "Variables"

#End Region
#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If IsPostBack = False Then
                mt_ConsultarConfiguracion()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Protected Sub gvConfiguracion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvConfiguracion.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            Me.hfCodigo_cta.Value = Me.gvConfiguracion.DataKeys(e.CommandArgument).Values("codigo_cta")
            If (e.CommandName = "Editar") Then
                Call mt_limpiarMensaje()
                Call mt_consultarConfiguracionPorCodigo_cta(hfCodigo_cta.Value)
            End If
        Catch ex As Exception
             Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Protected Sub btnActualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActualizar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If mt_validaFormulario() = True Then
                Dim dt As New Data.DataTable
                dt = mt_ActualizaConfiguracion(Me.hfCodigo_cta.Value, Me.txtDescripcion_cta.Text, Me.txtTiempo_cta.Text, Me.txtMensajeDato.Text)
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("Respuesta") = 1 Then
                        Call mt_validaFormulario()
                        Me.lblmensaje.Attributes.Add("class", "alert alert-success")
                        Me.lblmensaje.InnerHtml() = "<p>Se modificó la configuración con éxito</p>"
                        'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje").ToString + "')", True)
                        mt_limpiaControles()
                        mt_ConsultarConfiguracion()
                    End If
                End If
            End If
        Catch ex As Exception
            'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','No se pudo actualizar la configuración:" + ex.Message.ToString + "')", True)
            Call mt_limpiarMensaje()
            Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
            Me.lblmensaje.InnerHtml() = "<p>No se pudo relaizar la actualización: " & ex.Message.ToString & "</p>"
        End Try
    End Sub
    Protected Sub gvConfiguracion_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvConfiguracion.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim objGridView As GridView = CType(sender, GridView)
                Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objtablecell As TableCell = New TableCell()
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, "", "#D9534F")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 2, "VALIDACIÓN", "#D9534F")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 2, "MENSAJE", "#D9534F")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, "", "#D9534F")
                objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#End Region
#Region "Procedimientos y funciones"

    Private Sub mt_ConsultarConfiguracion()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ListaConfiguracion", "", 0)
        If dt.Rows.Count > 0 Then
            Me.gvConfiguracion.DataSource = dt
            Me.gvConfiguracion.DataBind()
        Else
            Me.gvConfiguracion.DataSource = Nothing
            Me.gvConfiguracion.DataBind()
        End If
        obj.CerrarConexion()
    End Sub
    Private Sub mt_consultarConfiguracionPorCodigo_cta(ByVal codigo_cta As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ListaConfiguracion", "XCOD", codigo_cta)
        If dt.Rows.Count > 0 Then
            Me.txtDescripcion_cta.Text = dt.Rows(0).Item("descripcion_cta").ToString
            Me.txtTiempo_cta.Text = dt.Rows(0).Item("datoValidacion_cta")
            Me.txtTipoTiempo_cta.Text = dt.Rows(0).Item("unidadValidacion_cta")
            Me.txtMensajeDato.Text = dt.Rows(0).Item("datoMensaje_cta")
            Me.txtMensajeTipo.Text = dt.Rows(0).Item("unidadMensaje_cta")
        End If
        obj.CerrarConexion()
    End Sub
    Private Function mt_validaFormulario() As Boolean
        If Me.hfCodigo_cta.Value = "" Then
            ''Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione un concepto')", True)
            Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
            Me.lblmensaje.InnerHtml() = "<p>Seleccione un concepto de  la tabla</p>"
            Me.txtDescripcion_cta.Focus()
            Return False
        End If
        If Me.txtDescripcion_cta.Text = "" Then
            'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Ingrese un concepto')", True)
            Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
            Me.lblmensaje.InnerHtml() = "<p>Ingrese un concepto</p>"
            Me.txtDescripcion_cta.Focus()
            Return False
        End If
        If Me.txtTiempo_cta.Text = "" Then
            'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Ingrese un valor')", True)
            Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
            Me.lblmensaje.InnerHtml() = "<p>Ingrese el tiempo</p>"
            Me.txtTiempo_cta.Focus()
            Return False
        End If
        Return True
    End Function
    Private Function mt_ActualizaConfiguracion(ByVal codigo_cta As Integer, ByVal descripcion_cta As String, ByVal tiempo_cta As String, ByVal datoMensaje_cta As String) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ActualizaConfiguracion", codigo_cta, descripcion_cta, tiempo_cta, Session("id_per"), datoMensaje_cta)
        obj.CerrarConexion()
        Return dt
    End Function
    Private Sub mt_limpiaControles()

        Me.hfCodigo_cta.Value = String.Empty
        Me.txtDescripcion_cta.Text = String.Empty
        Me.txtTiempo_cta.Text = String.Empty
        Me.txtTipoTiempo_cta.Text = String.Empty
        Me.txtMensajeTipo.Text = String.Empty
        Me.txtMensajeDato.Text = String.Empty

    End Sub
    Private Sub mt_limpiarMensaje()
        Me.lblmensaje.Attributes.Remove("class")
        Me.lblmensaje.InnerHtml = ""
    End Sub
    Protected Sub mt_AgregarCabecera(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal celltext As String, ByVal backcolor As String, Optional ByVal paint As Boolean = False)
        objtablecell = New TableCell()
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan
        objtablecell.Style.Add("color", "#FFFFFF")

        If paint Then
            objtablecell.Style.Add("background-color", backcolor)
            objtablecell.Style.Add("font-weight", "600")

        End If

        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

#End Region




    
    
End Class

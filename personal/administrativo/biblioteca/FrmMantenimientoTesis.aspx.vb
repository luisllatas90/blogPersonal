
Partial Class administrativo_biblioteca_FrmMantenimientoTesis
    Inherits System.Web.UI.Page

    Sub CargarTesis(ByVal tipo As String, ByVal anio As String, ByVal autor As String, ByVal estado As String)
        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        ' Lista Tesis por Año, Autor y estado.
        dt = obj.ListarTesis(tipo, anio, autor, estado)
        'Me.lblMensajeFormulario.Text = "Se encontraron " & dt.Rows.Count.ToString & " registro(s)."
        If dt.Rows.Count = 0 Then
            Me.lblMensajeFormulario.Text = "No se encontraron registros"
        Else
            'Me.dgvTesis.DataSource = dt
            'Me.dgvAsignar.Columns.Item(4).Visible = False 'codigo_pla
            Me.hdcod.Value = dt.Rows(0).Item("codigo_Tes")
            Me.txtTesis.Value = dt.Rows(0).Item("Titulo_Tes")
            Me.txtUrl.Text = dt.Rows(0).Item("url_tes")
            Me.lblAlumno.Text = dt.Rows(0).Item("Responsable")
            Me.lblFacultad.Text = dt.Rows(0).Item("nombre_fac")
            Me.lblPrograma.Text = dt.Rows(0).Item("nombre_cpf")
        End If
        'Me.dgvTesis.DataBind()
        dt = Nothing
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If validarCampos() = True Then
                Dim dt As New Data.DataTable
                Dim obj As New ClsGradosyTitulos
                If Page.IsValid Then        'Si la pagina no tiene errores, ingresa.
                    dt = obj.ActualizarTesis(Me.hdcod.Value, Me.txtTesis.Value, Me.txtUrl.Text, "")
                    Response.Redirect("FrmListaTesis.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&msj=M")
                    'Response.Redirect("FrmListaTesis.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Me.ddlPlan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Request.QueryString("cb3") & "&msj=M")
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        'Response.Redirect("FrmListaPlanOperativoAnual.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub



    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("FrmListaTesis.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&msj=C")
        'Response.Redirect("FrmListaTesis.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Request.QueryString("cb1") & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3"))
    End Sub

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If IsPostBack = False Then
            CargarTesis("E", "", Request.QueryString("c"), "")
        End If

    End Sub
    Function validarCampos() As Boolean
        If Me.txtTesis.Value = "" Then
            Me.lblMensajeFormulario.Text = "Ingrese Correctamente el Titulo de las Tesis"
            Me.aviso.Attributes.Add("class", "mensajeError")
            Return False
        End If
        If Me.txtTesis.Value.Length > 500 Then
            Me.lblMensajeFormulario.Text = "Titulo Demasiado Largo para Poder Se Registrado"
            Me.aviso.Attributes.Add("class", "mensajeError")
            Return False
        End If
        If Me.txtUrl.Text = "" Then
            Me.lblMensajeFormulario.Text = "Ingrese la URL de Repositorio de Tesis"
            Me.aviso.Attributes.Add("class", "mensajeError")
            Return False
        End If
        Return True
    End Function
End Class

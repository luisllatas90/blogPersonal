Partial Class administrativo_pec2_frmActividadEvento
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If (Request.QueryString("cco") <> Nothing) Then
                Me.Hdcodigo_cco.Value = Request.QueryString("cco")
                'Me.txtHora.Attributes.Add("onkeyup", "mascara(this,':',patron,true)")'fatima.vasquez 15-08-2018
                'Me.txtHoraFin.Attributes.Add("onkeyup", "mascara(this,':',patron,true)")'fatima.vasquez 15-08-2018
                'Me.txtFecha.Attributes.Add("onkeyup", "mascara(this,'/',patron2,true)")'fatima.vasquez 15-08-2018
                CargacboActividad()
                CargacboServicioConcepto() 'fatima.vasquez 10-08-2018
                'CargaGrupos()
                VerificaCheckPermisos()
                'Carga Combo Grupo
                'For i As Integer = 1 To 30
                '    Me.cboGrupo.Items.Add(i)
                'Next
                If (Request.QueryString("codigo_aev") <> 0) Then

                    Dim obj As New ClsConectarDatos
                    Dim dtActividades As New Data.DataTable
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
                    obj.AbrirConexion()
                    dtActividades = obj.TraerDataTable("EVE_BuscaActividadEvento", Request.QueryString("codigo_aev"), Me.Hdcodigo_cco.Value)
                    obj.CerrarConexion()

                    Me.txtNombre.Text = dtActividades.Rows(0)("nombre_aev").ToString()
                    Me.txtCupos.Text = dtActividades.Rows(0)("cupos_aev").ToString()
                    Me.txtCosto.Text = dtActividades.Rows(0)("costo_aev").ToString()
                    Me.cboServicioConcepto.SelectedValue = dtActividades.Rows(0)("codigo_sco").ToString()
                    Me.cboActividad.SelectedValue = dtActividades.Rows(0)("codigo_act").ToString()
                    Me.Hdcodigo_aev.Value = dtActividades.Rows(0)("codigo_aev").ToString()
                End If
            End If
        End If
        CargaGrid()
    End Sub

    Private Sub ActualizaEstadoPermiso(ByVal estado As Boolean)
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("EVE_ActualizaAsistenciaEventos", Me.Hdcodigo_cco.Value, estado)
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write("Error al actualizar permisos")
        End Try
    End Sub

    Private Sub VerificaCheckPermisos()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("EVE_ConsultaPermisosEvento", Me.Hdcodigo_cco.Value)
            obj.CerrarConexion()

            If (dt.Rows.Count > 0) Then
                If ((dt.Rows(0).Item("asistencia_dev").ToString = "") _
                    Or (dt.Rows(0).Item("asistencia_dev").ToString = "false")) Then
                    Me.chkPermiso.Checked = False
                Else
                    Me.chkPermiso.Checked = True
                End If
            End If

            obj = Nothing
            dt.Dispose()
        Catch ex As Exception
            Response.Write("Error al verificar permisos")
        End Try
    End Sub

    Private Sub CargacboActividad()
        Dim obj As New ClsConectarDatos
        Dim dtActividad As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtActividad = obj.TraerDataTable("EVE_BuscaActividades", 0, "")
        obj.CerrarConexion()
        Me.cboActividad.DataSource = dtActividad
        Me.cboActividad.DataTextField = "descripcion_Act"
        Me.cboActividad.DataValueField = "codigo_Act"
        Me.cboActividad.DataBind()
        dtActividad.Dispose()
    End Sub

    'Private Sub CargaGrupos()
    '    Dim obj As New ClsConectarDatos
    '    Dim dtGrupos As New Data.DataTable
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    dtGrupos = obj.TraerDataTable("EVE_BuscaGruposActEvn", Me.Hdcodigo_cco.Value)
    '    obj.CerrarConexion()
    '    Me.gvGrupos.DataSource = dtGrupos
    '    Me.gvGrupos.DataBind()
    '    dtGrupos.Dispose()
    'End Sub

    Private Sub CargaGrid()
        Dim obj As New ClsConectarDatos
        Dim dtActividades As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtActividades = obj.TraerDataTable("EVE_BuscaActividadEvento", 0, Me.Hdcodigo_cco.Value)
        obj.CerrarConexion()
        Me.gvActividadEvento.DataSource = dtActividades
        Me.gvActividadEvento.DataBind()
        dtActividades.Dispose()
    End Sub

    'Protected Sub gvActividadEvento_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvActividadEvento.RowDeleting
    '    If (Me.gvActividadEvento.Rows.Count > 0) Then

    '        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ConfirmarEliminar();", True)

    '        If HdRespEliminar.Value = "True" Then
    '            Dim obj As New ClsConectarDatos
    '            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
    '            obj.AbrirConexion()
    '            obj.Ejecutar("EVE_EliminaActividadEvento", Me.gvActividadEvento.DataKeys(e.RowIndex).Item(0))
    '            obj.CerrarConexion()

    '            CargaGrid()
    '            Response.Write("<script>alert('Registro eliminado con éxito')</script>")
    '            'CargaGrupos()
    '        End If
    '    End If
    'End Sub

    Protected Sub gvActividadEvento_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvActividadEvento.RowCommand
        Dim commandname As String = e.CommandName
        Dim codigo_Aev As String

        If commandname.Equals("Eliminar") Then
            Dim lnkEliminar As LinkButton = CType(e.CommandSource, LinkButton)
            codigo_Aev = lnkEliminar.CommandArgument
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("EVE_EliminaActividadEvento", codigo_Aev)
            obj.CerrarConexion()

            CargaGrid()
            Response.Write("<script>alert('Registro eliminado con éxito')</script>")
        End If

        If commandname.Equals("Modificar") Then
            Dim lnkModificar As LinkButton = CType(e.CommandSource, LinkButton)
            codigo_Aev = lnkModificar.CommandArgument
            Response.Write("<script>location.href='frmActividadEvento.aspx?mod=" & Request.QueryString("mod") & "&id=" & Request.QueryString("id") & "&ctf=1&cco=" & Request.QueryString("cco") & "&codigo_aev=" & codigo_Aev & "'</script>")
        End If
    End Sub


    Private Function SiExisteActividaEvento(ByVal codigo_cco As Integer, ByVal codigo_act As Integer) As Boolean
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("EVE_ValidaActividadEvento", codigo_cco, codigo_act)
            obj.CerrarConexion()

            If (dt.Rows.Count = 0) Then
                Return True
            Else
                Return False
            End If

            Return False

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ExisteActividadEventoDescripcion(ByVal codigo_cco As Integer, ByVal codigo_Aev As Integer, ByVal descripcion As String) As Boolean
        Try
            If codigo_Aev = 0 Then
                Dim obj As New ClsConectarDatos
                Dim dt As New Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
                obj.AbrirConexion()
                dt = obj.TraerDataTable("EVE_ValidaExisteActividadEvento", codigo_cco, descripcion)
                obj.CerrarConexion()

                If (dt.Rows.Count = 0) Then
                    Return True
                Else
                    Return False
                End If

                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        Try
            'If (validaFechas() = True) Then 'fatima.vasquez 15-08-2018
            If (validaIngreso() = True) Then
                Dim codigo_aev As Integer
                codigo_aev = Me.Hdcodigo_aev.Value

                'If (SiExisteActividaEvento(Me.Hdcodigo_cco.Value, Me.cboActividad.SelectedValue) = True) Then
                If (ExisteActividadEventoDescripcion(Me.Hdcodigo_cco.Value, codigo_aev, Me.txtNombre.Text) = True) Then
                    If (Me.chkPermiso.Checked = True) Then
                        ActualizaEstadoPermiso(1)
                    Else
                        ActualizaEstadoPermiso(0)
                    End If

                    Dim obj As New ClsConectarDatos
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
                    obj.AbrirConexion()
                    obj.Ejecutar("EVE_AgregaActividadEvento", Me.cboActividad.SelectedValue, _
                                 Me.Hdcodigo_cco.Value, Me.txtNombre.Text.ToUpper, 1, _
                                 Me.txtCupos.Text, Me.txtCosto.Text, Me.cboServicioConcepto.SelectedValue, codigo_aev) 'fatima.vasquez 15-08-2018
                    'Inicio - fatima.vasquez 15-08-2018
                    ' Me.txtFecha.Text & " " & Me.txtHora.Text, _
                    ' Me.txtLugar.Text, Me.cboGrupo.Text, _
                    ' Me.txtFecha.Text & " " & Me.txtHoraFin.Text, _
                    'Fin - fatima.vasquez 15-08-2018
                    obj.CerrarConexion()
                    'CargaGrupos()
                    CargaGrid()
                    VerificaCheckPermisos()
                    'End If
                    Response.Write("<script>alert('Registro guardado con éxito')</script>")
                Else
                    Response.Write("<script>alert('Ya existe una actividad con el mismo nombre')</script>")
                End If
                Response.Write("<script>location.href='frmActividadEvento.aspx?mod=" & Request.QueryString("mod") & "&id=" & Request.QueryString("id") & "&ctf=1&cco=" & Request.QueryString("cco") & "&codigo_aev=0'</script>")
            End If
            'End If 'fatima.vasquez 15-08-2018
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'fatima.vasquez 15-08-2018
    'Private Function validaFechas() As Boolean                
    '    If (Me.txtHora.Text.Trim.Length <> 5) Then
    '        Response.Write("<script>alert('Formato de hora incorrecto')</script>")
    '        Me.txtHora.Text = ""
    '        Return False
    '    End If

    '    If (Me.txtHoraFin.Text.Trim.Length <> 5) Then
    '        Response.Write("<script>alert('Formato de hora incorrecto')</script>")
    '        Me.txtHoraFin.Text = ""
    '        Return False
    '    End If

    '    Try
    '        Date.Parse(Me.txtFecha.Text)            
    '    Catch ex As Exception
    '        Me.txtFecha.Text = ""
    '        Response.Write("<script>alert('Fecha incorrecta')</script>")
    '        Return False
    '    End Try

    '    Try
    '        Date.Parse(Me.txtFecha.Text & " " & Me.txtHora.Text)
    '    Catch ex As Exception
    '        Me.txtHora.Text = ""
    '        Response.Write("<script>alert('Formato de hora incorrecto.')</script>")
    '        Return False
    '    End Try

    '    Try
    '        Date.Parse(Me.txtFecha.Text & " " & Me.txtHoraFin.Text)
    '    Catch ex As Exception
    '        Me.txtHoraFin.Text = ""
    '        Response.Write("<script>alert('Formato de hora incorrecto')</script>")
    '        Return False
    '    End Try

    '    Dim fec1 As Date
    '    Dim fec2 As Date
    '    fec1 = Date.Parse(Me.txtFecha.Text & " " & Me.txtHora.Text)
    '    fec2 = Date.Parse(Me.txtFecha.Text & " " & Me.txtHoraFin.Text)        
    '    If (fec1 > fec2) Then
    '        Response.Write("<script>alert('La fecha inicio no puede ser mayor a fecha fin')</script>")
    '        Me.txtHoraFin.Text = ""
    '        Return False
    '    End If

    '    Return True
    'End Function

    Private Function validaIngreso() As Boolean
        If (Me.cboActividad.SelectedValue.Trim = "") Then
            Response.Write("<script>alert('Debe seleccionar una actividad')</script>")
            Me.cboActividad.Focus()
            Return False
        End If

        If (Me.txtNombre.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar un nombre')</script>")
            Me.txtNombre.Focus()
            Return False
        End If
        'Inicio - fatima.vasquez 15-08-2018
        'If (Me.txtFecha.Text.Length = 10) Then
        '    If (Me.txtFecha.Text.Trim = "") Then
        '        Response.Write("<script>alert('Debe ingresar la fecha de la actividad')</script>")
        '        Me.txtFecha.Focus()
        '        Return False
        '    End If
        'Else
        '    Response.Write("<script>alert('Formato de Fecha Incorrecto')</script>")
        '    Me.txtFecha.Focus()
        '    Return False
        'End If
        'If (Me.txtHora.Text.Trim = "") Then
        '    Response.Write("<script>alert('Debe ingresar la hora de la actividad')</script>")
        '    Me.txtHora.Focus()
        '    Return False        
        'End If

        'If (Me.txtHoraFin.Text.Trim = "") Then
        '    Response.Write("<script>alert('Debe ingresar la hora final de la actividad')</script>")
        '    Me.txtHoraFin.Focus()
        '    Return False        
        'End If

        'If (Me.txtLugar.Text.Trim = "") Then
        '    Response.Write("<script>alert('Debe ingresar el lugar de la actividad')</script>")
        '    Me.txtLugar.Focus()
        '    Return False
        'End If

        'If (Me.cboGrupo.Text.Trim = "") Then
        '    Response.Write("<script>alert('Debe ingresar el grupo')</script>")
        '    Me.cboGrupo.Focus()
        '    Return False
        'End If

        If (Me.txtCosto.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar el costo de la actividad')</script>")
            Me.txtCosto.Focus()
            Return False
        End If

        If (Me.txtCupos.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar los cupos de la actividad')</script>")
            Me.txtCupos.Focus()
            Return False
        End If
        'Fin - fatima.vasquez 15-08-2018

        Return True
    End Function

    Protected Sub cmdRptAsistencia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRptAsistencia.Click
        Response.Redirect("../../../../rptusat/?/PRIVADOS/EVENTOS/LOG_RptAsistenciaEvento&mod=" & Request.QueryString("mod") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id") & "&codigo_cco=" & Me.Hdcodigo_cco.Value)
        'Response.Redirect("../../../../reportServer/?/PRIVADOS/EVENTOS/LOG_RptAsistenciaEvento&mod=" & Request.QueryString("mod") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id") & "&codigo_cco=" & Me.Hdcodigo_cco.Value)
    End Sub

    Protected Sub btnRefrescar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefrescar.Click
        Response.Write("<script>location.href='frmActividadEvento.aspx?mod=" & Request.QueryString("mod") & "&id=" & Request.QueryString("id") & "&ctf=1&cco=" & Request.QueryString("cco") & "&codigo_aev=0'</script>")
    End Sub

    'Inicio - fatima.vasquez 10-08-2018
    Protected Sub gvActividadEvento_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvActividadEvento.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim cpa As String = ""
            fila = e.Row.DataItem

            e.Row.Cells(5).Text = "<a href='frmActividadesProgramacion.aspx?codigo_aev=" & fila.Row("codigo_aev") & "&nom= " & fila.Row("nombre_aev") & "&KeepThis=true&TB_iframe=true&height=355&width=500&modal=true' title='Programación' class='thickbox'>Editar</a>"
            'e.Row.Cells(7).Text = "<a target='_blank' href='http://serverdev/reportServer/?/PRIVADOS/EVENTOS/EVE_RptInscritoActividadEvento&codigo_aev=" & fila.Row("codigo_aev") & "'>Ver</a>"
            e.Row.Cells(7).Text = "<a target='_blank' href='https://intranet.usat.edu.pe/rptusat/?/PRIVADOS/EVENTOS/EVE_RptInscritoActividadEvento&codigo_aev=" & fila.Row("codigo_aev") & "'>Ver</a>"
        End If
    End Sub

    Private Sub CargacboServicioConcepto()
        Dim obj As New ClsConectarDatos
        Dim dtServicioConcepto As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtServicioConcepto = obj.TraerDataTable("EVE_ConsultarServicioPorCeco", Me.Hdcodigo_cco.Value)
        obj.CerrarConexion()
        Me.cboServicioConcepto.DataSource = dtServicioConcepto
        Me.cboServicioConcepto.DataTextField = "descripcion_sco"
        Me.cboServicioConcepto.DataValueField = "codigo_sco"
        Me.cboServicioConcepto.DataBind()
        Me.cboServicioConcepto.Items.Insert(0, New ListItem("", "0"))
        dtServicioConcepto.Dispose()
    End Sub
    'Fin - fatima.vasquez 10-08-2018
End Class
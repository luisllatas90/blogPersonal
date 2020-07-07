
Partial Class administrativo_pec2_frmActividadEvento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If (Request.QueryString("cco") <> Nothing) Then
                Me.Hdcodigo_cco.Value = Request.QueryString("cco")
                Me.txtHora.Attributes.Add("onkeyup", "mascara(this,':',patron,true)")
                Me.txtHoraFin.Attributes.Add("onkeyup", "mascara(this,':',patron,true)")
                CargacboActividad()
                CargaGrupos()

                'Carga ComboGrupo
                For i As Integer = 1 To 30
                    Me.cboGrupo.Items.Add(i)
                Next
            End If
        End If
        CargaGrid()
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

    Private Sub CargaGrupos()
        Dim obj As New ClsConectarDatos
        Dim dtGrupos As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtGrupos = obj.TraerDataTable("EVE_BuscaGruposActEvn", Me.Hdcodigo_cco.Value)
        obj.CerrarConexion()
        Me.gvGrupos.DataSource = dtGrupos
        Me.gvGrupos.DataBind()
        dtGrupos.Dispose()
    End Sub

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

    Protected Sub gvActividadEvento_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvActividadEvento.RowDeleting
        CargaGrid()
    End Sub

    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        Try
            If (validaIngreso() = True) Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
                obj.AbrirConexion()
                obj.Ejecutar("EVE_AgregaActividadEvento", Me.cboActividad.SelectedValue, _
                             Me.Hdcodigo_cco.Value, Me.txtNombre.Text, _
                             Me.txtFecha.Text & " " & Me.txtHora.Text, _
                             Me.txtLugar.Text, Me.cboGrupo.Text, _
                             Me.txtFecha.Text & " " & Me.txtHoraFin.Text)
                obj.CerrarConexion()
                CargaGrupos()
                CargaGrid()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try        
    End Sub

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

        If (Me.txtFecha.Text.Length = 10) Then
            If (Me.txtFecha.Text.Trim = "") Then
                Response.Write("<script>alert('Debe ingresar la fecha de la actividad')</script>")
                Me.txtFecha.Focus()
                Return False
            End If
        Else
            Response.Write("<script>alert('Formato de Fecha Incorrecto')</script>")
            Me.txtFecha.Focus()
            Return False
        End If        

        If (Me.txtHora.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar la hora de la actividad')</script>")
            Me.txtHora.Focus()
            Return False
        Else
            If (Me.txtHora.Text.Trim.Length < 5) Then
                Response.Write("<script>alert('Formato de Hora Incorrecto')</script>")
                Me.txtHora.Focus()
                Return False
            End If
        End If

        If (Me.txtHoraFin.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar la hora final de la actividad')</script>")
            Me.txtHoraFin.Focus()
            Return False
        Else
            If (Me.txtHoraFin.Text.Trim.Length < 5) Then
                Response.Write("<script>alert('Formato de Hora Incorrecto')</script>")
                Me.txtHoraFin.Focus()
                Return False
            End If
        End If

        If (Me.txtLugar.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar el lugar de la actividad')</script>")
            Me.txtLugar.Focus()
            Return False
        End If

        If (Me.cboGrupo.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar el grupo')</script>")
            Me.cboGrupo.Focus()
            Return False
        End If

        Return True
    End Function

    Protected Sub cmdRptAsistencia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRptAsistencia.Click
        Response.Redirect("../../rptusat/?/PRIVADOS/EVENTOS/LOG_RptEntregaMaterialResumen&mod=" & Request.QueryString("mod") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id") & "&codigo_act=" & Me.cboActividad.SelectedValue & "&codigo_cco=" & Me.Hdcodigo_cco.Value)
    End Sub
End Class

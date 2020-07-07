
Partial Class administrativo_pec2_frmAsistenciaEvento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            CargaCentroCostos()
            CargaCboDocumento()
            Me.calFecha.SelectedDate = Date.Today
            Me.txtHora.Attributes.Add("onkeyup", "mascara(this,':',patron,true)")
            'Select Case Request.QueryString("mod")
            '    Case 0 'Otros
            '        Me.lblTitulo.Text = "Inscripciones"
            '    Case 1 'Epre
            '        Me.lblTitulo.Text = "Inscripción a Escuela PreUniversitaria"
            '    Case 2 'Pregrado
            '        Me.lblTitulo.Text = "Inscripción a Escuelas de PreGrado"
            '    Case 3 'Profesionalización
            '        Me.lblTitulo.Text = "Inscripción a Programas de Profesionalización"
            '    Case 4 'Complementarios
            '        Me.lblTitulo.Text = "Inscripción a Centro de Idiomas y Complementarios"
            '    Case 5 'PostGrado
            '        Me.lblTitulo.Text = "Inscripción a Escuela de PostGrado"
            '    Case 6 'Educación Contínua
            '        Me.lblTitulo.Text = "Inscripción a Programas de Educación Contínua"
            '    Case Else
            '        Me.lblTitulo.Text = "Inscripción a eventos registrados"
            'End Select
        End If
    End Sub

    Private Sub CargaCentroCostos()
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(Me.cboEvento, obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), "", Request.QueryString("mod")), "codigo_Cco", "Nombre", ">> Seleccione<<")
        obj.CerrarConexion()
        obj = Nothing
        objfun = Nothing        
    End Sub

    Private Sub CargaCboActividad(ByVal cco As String)
        Dim obj As New ClsConectarDatos
        Dim dtAct As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtAct = obj.TraerDataTable("EVE_BuscaGrupoAev", cco)
        Me.cboActividad.DataSource = dtAct
        Me.cboActividad.DataValueField = "grupo_aev"
        Me.cboActividad.DataTextField = "Grupo"
        Me.cboActividad.DataBind()
        obj.CerrarConexion()
        dtAct.Dispose()
    End Sub

    Private Sub CargaCboDocumento()
        Me.cboDocumento.Items.Add("DNI")
        Me.cboDocumento.Items.Add("PASAPORTE")
        Me.cboDocumento.Items.Add("CARNET EXTRANJERIA")
    End Sub

    Protected Sub cboEvento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEvento.SelectedIndexChanged
        CargaCboActividad(Me.cboEvento.SelectedValue)
    End Sub

    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            If (ValidaForm() = True) Then
                Dim codigo_cco As String
                codigo_cco = Me.cboEvento.SelectedValue

                'Buscamos si Existe la persona
                Dim dt_Persona As New Data.DataTable
                Dim codigo_pso As String = "0"
                obj.AbrirConexion()
                dt_Persona = obj.TraerDataTable("EVE_BuscaPersonaxDocumento", Me.txtDocumento.Text)
                obj.CerrarConexion()

                If (dt_Persona.Rows.Count > 0) Then
                    codigo_pso = dt_Persona.Rows(0).Item("codigo_Pso")

                    'Capturamos el ciclo actual
                    Dim dt_Cac As New Data.DataTable
                    Dim codigo_cac As String = "0"
                    obj.AbrirConexion()
                    dt_Cac = obj.TraerDataTable("EVE_CicloAcademicoActual")
                    obj.CerrarConexion()

                    If (dt_Cac.Rows.Count > 0) Then
                        codigo_cac = dt_Cac.Rows(0).Item("codigo_Cac")
                    End If

                    'Buscamos Deudas
                    Dim dt_Deuda As New Data.DataTable
                    Dim blnExisteDeuda As Boolean = False
                    obj.AbrirConexion()
                    dt_Deuda = obj.TraerDataTable("EVE_BuscaDeudaPersona", codigo_pso, codigo_cac, codigo_cco)
                    obj.CerrarConexion()

                    If (dt_Deuda.Rows.Count = 0) Then
                        Me.lblMensaje.Text = "No se encuentra inscrito en este evento "
                    Else
                        'Verificamos si el saldo es 0
                        For i As Integer = 0 To dt_Deuda.Rows.Count - 1
                            If (Double.Parse(dt_Deuda.Rows(i).Item("saldo_Deu").ToString) > 0) Then
                                blnExisteDeuda = True
                            End If
                        Next

                        If (blnExisteDeuda = False) Then
                            Dim dt_Actividad As New Data.DataTable
                            obj.AbrirConexion()
                            dt_Actividad = obj.TraerDataTable("EVE_VerificaActividad", codigo_cco, Me.cboActividad.SelectedValue, _
                                               Me.calFecha.SelectedDate & " " & Me.txtHora.Text)
                            obj.CerrarConexion()

                            'Verificamos si existe Actividad para esa fecha-hora
                            If (dt_Actividad.Rows.Count > 0) Then
                                'Cruce Horario
                                Dim dt_Horario As Data.DataTable
                                Dim blnExisteCruce As Boolean = False
                                obj.AbrirConexion()
                                dt_Horario = obj.TraerDataTable("EVE_BuscaCruceHorario", codigo_cco, _
                                                    Me.cboActividad.SelectedValue, codigo_pso, _
                                                    Me.calFecha.SelectedDate & " " & Me.txtHora.Text)
                                obj.CerrarConexion()                              
                                If (dt_Horario.Rows(0).Item("Evento").ToString.Trim = "OK") Then
                                    'Insertamos Registro                                
                                    If (blnExisteCruce = False) Then
                                        obj.AbrirConexion()
                                        obj.Ejecutar("EVE_RegistrarParticipacion", codigo_cco, _
                                                     Me.cboActividad.SelectedValue, codigo_pso, _
                                                     "Asistencia a Evento " & Me.cboEvento.Text, _
                                                     Me.calFecha.SelectedDate & " " & Me.txtHora.Text)
                                        obj.CerrarConexion()

                                        Me.lblMensaje.Text = "Registro Guardado"
                                    End If
                                Else
                                    blnExisteCruce = True
                                    Me.lblMensaje.Text = dt_Horario.Rows(0).Item("Evento")
                                End If

                                dt_Cac.Dispose()
                                dt_Deuda.Dispose()
                                dt_Persona.Dispose()
                                dt_Horario.Dispose()
                                dt_Actividad.Dispose()
                            Else
                                Me.lblMensaje.Text = "No existe actividad para la fecha y hora seleccionada"
                            End If
                        Else
                            Me.lblMensaje.Text = "Tiene Deuda Pendiente "
                        End If
                    End If
                Else
                    Me.lblMensaje.Text = "No existe la Persona"
                End If
            End If
        Catch ex As Exception
            Me.lblMensaje.Text = ex.Message
        End Try
    End Sub

    Private Function ValidaForm() As Boolean

        If (Me.calFecha.SelectedDate = "12:00:00 am") Then
            Response.Write("<script>alert('Debe seleccionar una fecha')</script>")
            Me.calFecha.Focus()
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

        If (Me.cboEvento.Text = ">> Seleccione<<") Then
            Response.Write("<script>alert('Debe seleccionar un Evento')</script>")
            Me.cboEvento.Focus()
            Return False
        End If

        If (Me.cboActividad.Text.Trim = "") Then
            Response.Write("<script>alert('Debe seleccionar una actividad')</script>")
            Me.cboActividad.Focus()
            Return False
        End If

        If (Me.cboDocumento.Text.Trim = "") Then
            Response.Write("<script>alert('Debe seleccionar una tipo de documento')</script>")
            Me.cboDocumento.Focus()
            Return False
        End If

        If (Me.txtDocumento.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar el numero de documento')</script>")
            Me.txtDocumento.Focus()
            Return False
        End If

        Return True
    End Function
End Class

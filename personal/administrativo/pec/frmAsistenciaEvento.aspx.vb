
Partial Class administrativo_pec2_frmAsistenciaEvento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim hora As String = Date.Now.Hour.ToString
        Dim minuto As String = Date.Now.Minute.ToString

        If (Date.Now.Hour.ToString.Length < 2) Then
            hora = "0" & Date.Now.Hour.ToString
        End If

        If (Date.Now.Minute.ToString.Length < 2) Then
            minuto = "0" & Date.Now.Minute.ToString
        End If

        'Me.txtHora.Text = hora & ":" & minuto

        If IsPostBack = False Then
            CargaCentroCostos()
            CargaCboDocumento()
            Me.calFecha.SelectedDate = Date.Today.ToString()
            ' Me.txtHora.Attributes.Add("onkeyup", "mascara(this,':',patron,true)")
            Me.txtDocumento.Attributes.Add("onkeyup", "seleccionaFoco()")

            'Capturamos el ciclo actual
            Dim dt_Cac As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt_Cac = obj.TraerDataTable("EVE_CicloAcademicoActual")
            obj.CerrarConexion()

            If (dt_Cac.Rows.Count > 0) Then
                Me.HdCicloActual.Value = dt_Cac.Rows(0).Item("codigo_Cac")
            End If            
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
        divConfirmaRegistrar.Visible = False
        Me.txtDocumento.Focus()
    End Sub

    Private Sub VerificaCheckPermisos()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("EVE_ConsultaPermisosEvento", Me.cboEvento.SelectedValue)
            obj.CerrarConexion()

            If (dt.Rows.Count > 0) Then
                If (dt.Rows(0).Item("asistencia_dev") = True) Then
                    Me.HdPermiteAsistencia.Value = True
                Else
                    Me.HdPermiteAsistencia.Value = False
                End If
            End If

            obj = Nothing
            dt.Dispose()
        Catch ex As Exception
            Response.Write("Error al verificar permisos")
        End Try
    End Sub

    Private Sub CargaCentroCostos()
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        'Response.Write("ctf -> " & Request.QueryString("ctf"))
        'Response.Write("<br/>")
        'Response.Write("id -> " & Request.QueryString("id"))
        'Response.Write("<br/>")
        'Response.Write(" -  ")
        'Response.Write("<br/>")
        'Response.Write("mod -> " & Request.QueryString("mod"))

        objfun.CargarListas(Me.cboEvento, obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", _
                                                             Request.QueryString("ctf"), _
                                                             Request.QueryString("id"), "", _
                                                             Request.QueryString("mod")), "codigo_Cco", "Nombre", ">> Seleccione<<")
        obj.CerrarConexion()
        obj = Nothing
        objfun = Nothing        
    End Sub

    Private Sub CargaCboActividad(ByVal cco As String, ByVal fecha As Date)
        Dim obj As New ClsConectarDatos
        Dim dtAct As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        dtAct = obj.TraerDataTable("EVE_BuscaGrupoAev", cco, fecha)
        Me.cboActividad.DataSource = dtAct
        Me.cboActividad.DataValueField = "codigo_aev"
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
        CargaCboActividad(Me.cboEvento.SelectedValue, Me.calFecha.SelectedDate)
        VerificaCheckPermisos()
    End Sub

    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            Me.lblMensaje.Text = ""
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

                    'Buscamos Deudas
                    Dim dt_Deuda As New Data.DataTable
                    Dim blnExisteDeuda As Boolean = False
                    obj.AbrirConexion()
                    dt_Deuda = obj.TraerDataTable("EVE_BuscaDeudaPersonaPagoWeb", codigo_pso, Me.HdCicloActual.Value, codigo_cco)
                    'Response.Write("EVE_BuscaDeudaPersona " & codigo_pso & ", " & Me.HdCicloActual.Value & ", " & codigo_cco)
                    obj.CerrarConexion()

                    If (dt_Deuda.Rows.Count = 0) Then
                        Me.lblMensaje.Text = "No se encuentra inscrito en este evento"
                        'Me.lblMensaje.Font.Size = 14
                        'Me.lblMensaje.BackColor = Drawing.Color.LightPink
                        'Me.lblMensaje.ForeColor = Drawing.Color.DarkRed
                        'Me.lblMensaje.BorderColor = Drawing.Color.Pink
                        Me.DivMensaje.Attributes.Add("class", "alert alert-warning")
                        Me.txtDocumento.Text = ""
                        blnExisteDeuda = True
                    Else
                        'Verificamos si el saldo es 0
                        For i As Integer = 0 To dt_Deuda.Rows.Count - 1
                            If (Double.Parse(dt_Deuda.Rows(i).Item("saldo_Deu").ToString) > 0) And (Double.Parse(dt_Deuda.Rows(i).Item("montopagoweb").ToString) = 0) Then
                                'blnExisteDeuda = False
                                blnExisteDeuda = True
                                'Me.lblMensaje.Font.Size = 14
                                'Me.lblMensaje.ForeColor = Drawing.Color.Green
                                Me.DivMensaje.Attributes.Add("class", "alert alert-danger")

                                Me.lblMensaje.Text = "Tiene Deuda Pendiente, debe ir a caja a cancelar.<br/>" & dt_Persona.Rows(0).Item("NombreCompleto") & " - " & Me.txtDocumento.Text
                                Me.txtDocumento.Focus()
                                Exit For
                                Exit Try
                            End If
                        Next
						blnExisteDeuda = False
                    End If

                    VerificaCheckPermisos()
                    'Response.Write(blnExisteDeuda & ", " & Me.HdPermiteAsistencia.Value)
                    If ((blnExisteDeuda = False) And (Me.HdPermiteAsistencia.Value = True)) Then
                        Dim dt_Actividad As New Data.DataTable
                        obj.AbrirConexion()
                        dt_Actividad = obj.TraerDataTable("EVE_VerificaActividad", codigo_cco, Me.cboActividad.SelectedValue, _
                                           Me.calFecha.SelectedDate & " " & hour(Now) & " : " & minute(Now) & " : " & second(Now))
                        obj.CerrarConexion()

                        'Verificamos si existe Actividad para esa fecha-hora
                        If (dt_Actividad.Rows.Count > 0) Then
                            'Cruce Horario
                            'Dim dt_Horario As Data.DataTable   '12/11 Se bloquea
                            obj.AbrirConexion()

                            Dim dtActividades As New Data.DataTable
                            Dim strActividades As String = ""
                            dtActividades = obj.TraerDataTable("EVE_RetornaActxGrupo", codigo_cco, Me.cboActividad.SelectedValue)

                            If (dtActividades.Rows.Count = 0) Then
                                strActividades = 0
                            Else
                                For j As Integer = 0 To dtActividades.Rows.Count - 1
                                    strActividades = dtActividades.Rows(j).Item("codigo_aev").ToString & "," & strActividades
                                Next

                                'strActividades = strActividades & "," & dtActividades.Rows(dtActividades.Rows.Count - 1).Item("codigo_aev").ToString
                            End If
                            obj.CerrarConexion()

                            Dim ObjCnx1 As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                            Dim valor_cruce As Integer
                            valor_cruce = ObjCnx1.TraerValor("EVE_BuscaCruceHorario", codigo_cco, _
                                                strActividades, codigo_pso, _
                                                Me.calFecha.SelectedDate & " " & Hour(Now) & " : " & Minute(Now) & " : " & Second(Now), Me.cboActividad.SelectedValue)
                            'dt_Horario = obj.TraerDataTable("EVE_BuscaCruceHorario", codigo_cco, _
                            If valor_cruce = 0 Then
                                'obj.CerrarConexion()
                                'Response.Write(Me.calFecha.SelectedDate & " " & Hour(Now) & " : " & Minute(Now) & " : " & Second(Now))

                                'If (dt_Horario.Rows.Count = 0) Then
                                'Response.Write("cco: " & codigo_cco)
                                'Response.Write("<br/>")
                                'Response.Write("aev: " & Me.cboActividad.SelectedValue)

                                'Insertamos Registro                                
                                obj.AbrirConexion()
                                obj.Ejecutar("EVE_RegistrarParticipacion", codigo_cco, _
                                             Me.cboActividad.SelectedValue, codigo_pso, _
                                             "Asistencia a Evento " & Me.cboEvento.Text, _
                                             Me.calFecha.SelectedDate & " " & Hour(Now) & " : " & Minute(Now) & " : " & Second(Now))
                                obj.CerrarConexion()

                                Me.lblMensaje.Text = "Registro Guardado Correctamente.. <br/>" & dt_Persona.Rows(0).Item("NombreCompleto") & " / DNI: " & Me.txtDocumento.Text
                                'Me.lblMensaje.Font.Size = 13
                                'Me.lblMensaje.BackColor = Drawing.Color.LimeGreen
                                'Me.lblMensaje.ForeColor = Drawing.Color.DarkGreen
                                'Me.lblMensaje.BorderColor = Drawing.Color.LimeGreen
                                Me.DivMensaje.Attributes.Add("class", "alert alert-success")

                                Me.txtDocumento.Text = ""

                            ElseIf valor_cruce = 1 Then


                                Me.lblMensaje.Text = "Usted ya está participando de esta actividad"  '+ dt_Horario.Rows(0).Item("evento")
                                'Me.lblMensaje.Font.Size = 13
                                'Me.lblMensaje.BackColor = Drawing.Color.LightSalmon
                                'Me.lblMensaje.ForeColor = Drawing.Color.DarkRed
                                'Me.lblMensaje.BorderColor = Drawing.Color.Pink
                                Me.DivMensaje.Attributes.Add("class", "alert alert-danger")

                                Me.txtDocumento.Text = ""

                            ElseIf valor_cruce = 2 Then                                
                                'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "myFuncionAlerta", "myFuncionAlerta();", True)

                                Me.DivMensaje.Visible = True
                                Me.lblMensaje.Text = "Está participando en otra actividad durante estas horas"  'dt_Horario.Rows(0).Item("evento")
                                'Me.lblMensaje.Font.Size = 13
                                'Me.lblMensaje.BackColor = Drawing.Color.LightYellow
                                'Me.lblMensaje.ForeColor = Drawing.Color.Green
                                'Me.lblMensaje.BorderColor = Drawing.Color.Yellow
                                Me.DivMensaje.Attributes.Add("class", "alert alert-warning")

                                'btnefe_Click(sender, e)

                                'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "validar", "validar();", True)

                                divConfirmaRegistrar.Visible = True

                                'If (hdn_confirmacion.Value = "1") Then

                                '    obj.AbrirConexion()   '13/11/19 Se añade: Permite el registro de todas maneras
                                '    obj.Ejecutar("EVE_RegistrarParticipacion", codigo_cco, _
                                '                 Me.cboActividad.SelectedValue, codigo_pso, _
                                '                 "Asistencia a Evento " & Me.cboEvento.Text, _
                                '                 Me.calFecha.SelectedDate & " " & Hour(Now) & " : " & Minute(Now) & " : " & Second(Now))
                                '    obj.CerrarConexion()
                                '    Me.lblMensaje.Text = "Registro Guardado Correctamente.. <br/>" & dt_Persona.Rows(0).Item("NombreCompleto") & " / DNI: " & Me.txtDocumento.Text
                                '    Me.DivMensaje.Attributes.Add("class", "alert alert-success")
                                'End If

                            End If

                            dt_Deuda.Dispose()
                            dt_Persona.Dispose()
                            'dt_Horario.Dispose()
                            dt_Actividad.Dispose()
                        Else                            
                            Me.lblMensaje.Text = "No existe actividad para la fecha y hora seleccionada"
                            'Me.lblMensaje.Font.Size = 13
                            'Me.lblMensaje.BackColor = Drawing.Color.LightPink
                            'Me.lblMensaje.ForeColor = Drawing.Color.DarkRed
                            'Me.lblMensaje.BorderColor = Drawing.Color.Pink
                            Me.DivMensaje.Attributes.Add("class", "alert alert-danger")
                            Me.txtDocumento.Text = ""
                        End If

                    Else                        
                        If (Me.HdPermiteAsistencia.Value = False) Then
                            Me.lblMensaje.Text = "Esta actividad no registra asistencia"
                            'Me.lblMensaje.Font.Size = 13
                            'Me.lblMensaje.BackColor = Drawing.Color.LightSalmon
                            'Me.lblMensaje.ForeColor = Drawing.Color.DarkRed
                            'Me.lblMensaje.BorderColor = Drawing.Color.Pink
                            Me.DivMensaje.Attributes.Add("class", "alert alert-danger")
                            Me.txtDocumento.Text = ""
                        End If
                        
                    End If
                Else                    
                    Me.lblMensaje.Text = "No existe la Persona con DNI: " & Me.txtDocumento.Text
                    'Me.lblMensaje.Font.Size = 13
                    'Me.lblMensaje.BackColor = Drawing.Color.LightPink
                    'Me.lblMensaje.ForeColor = Drawing.Color.DarkRed
                    'Me.lblMensaje.BorderColor = Drawing.Color.Pink
                    Me.DivMensaje.Attributes.Add("class", "alert alert-danger")
                    Me.txtDocumento.Text = ""
                End If
            End If

            ' Me.txtDocumento.Text = ""
            Me.txtDocumento.Focus()

        Catch ex As Exception
            Me.lblMensaje.Text = ex.Message
        End Try
    End Sub

    Private Function ValidaForm() As Boolean

        If (Me.calFecha.SelectedDate = "12:00:00 am") Then
            Response.Write("<script>alert('Debe seleccionar una fecha')</script>")
            'Me.calFecha.Focus()
            Return False
        End If

        'If (Me.txtHora.Text.Trim = "") Then
        '    Response.Write("<script>alert('Debe ingresar la hora de la actividad')</script>")
        '    'Me.txtHora.Focus()
        '    Return False
        'Else
        '    If (Me.txtHora.Text.Trim.Length < 5) Then
        '        Response.Write("<script>alert('Formato de Hora Incorrecto')</script>")
        '        'Me.txtHora.Focus()
        '        Return False
        '    End If
        'End If

        If (Me.cboEvento.Text = ">> Seleccione<<") Then
            Response.Write("<script>alert('Debe seleccionar un Evento')</script>")
            'Me.cboEvento.Focus()
            Return False
        End If

        If (Me.cboActividad.Text.Trim = "") Then
            Response.Write("<script>alert('Debe seleccionar una actividad')</script>")
            'Me.cboActividad.Focus()
            Return False
        End If

        If (Me.cboDocumento.Text.Trim = "") Then
            Response.Write("<script>alert('Debe seleccionar una tipo de documento')</script>")
            'Me.cboDocumento.Focus()
            Return False
        End If

        If (Me.txtDocumento.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar el numero de documento')</script>")
            Me.txtDocumento.Focus()
            Return False
        End If

        Return True
    End Function

    Protected Sub calFecha_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calFecha.SelectionChanged
        CargaCboActividad(Me.cboEvento.SelectedValue, Me.calFecha.SelectedDate)
    End Sub

    Protected Sub cboActividad_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboActividad.TextChanged
        lblMensaje.Text = ""
    End Sub

    Private Sub prueba_registro()

        
    End Sub

    Protected Sub btnConfirmarSI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmarSI.Click

        Me.DivMensaje.Visible = True

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim codigo_cco As String
        codigo_cco = Me.cboEvento.SelectedValue

        Dim doc As String
        doc = Me.txtDocumento.Text
        'Buscamos si Existe la persona
        Dim dt_Persona As New Data.DataTable
        Dim codigo_pso As String = "0"
        obj.AbrirConexion()
        dt_Persona = obj.TraerDataTable("EVE_BuscaPersonaxDocumento", Me.txtDocumento.Text)
        obj.CerrarConexion()

        If (dt_Persona.Rows.Count > 0) Then
            codigo_pso = dt_Persona.Rows(0).Item("codigo_Pso")
        End If

        'If (hdn_confirmacion.Value = "1") Then

        obj.AbrirConexion()   '13/11/19 Se añade: Permite el registro de todas maneras
        obj.Ejecutar("EVE_RegistrarParticipacion", codigo_cco, _
                     Me.cboActividad.SelectedValue, codigo_pso, _
                     "Asistencia a Evento " & Me.cboEvento.Text, _
                     Me.calFecha.SelectedDate & " " & Hour(Now) & " : " & Minute(Now) & " : " & Second(Now))
        obj.CerrarConexion()

        Me.lblMensaje.Text = "Registro Guardado Correctamente.. <br/>" & dt_Persona.Rows(0).Item("NombreCompleto") & " / DNI: " & doc
        Me.DivMensaje.Attributes.Add("class", "alert alert-success")
        Me.txtDocumento.Text = ""
        'End If

    End Sub

    Protected Sub btnConfirmarNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmarNO.Click

        Me.divConfirmaRegistrar.Visible = False
        Me.lblMensaje.Text = ""
        Me.DivMensaje.Visible = False
    End Sub
End Class

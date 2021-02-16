Imports System.Data

Partial Class academico_cargalectiva_FrmConfiguraSustitorioPP
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If (IsPostBack = False) Then
            VerificaTipoUsuario()
            CargaEscuela()
            CargarCentroCostos()
            CargaCiclo()
            VerificaCronograma()

            CargarFechas()
            CargarCapacidad()
            CargarFechasExamen()
        End If
        ''Para asignar o solicitar
        'If Page.Request.QueryString("codigo_amb") > 0 And Page.Request.QueryString("estado") <> "" Then
        '    Dim codigo_lho As Integer = 0
        '    Dim obj As New ClsConectarDatos
        '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        '    codigo_lho = Session("h_codigolho")
        '    obj.AbrirConexion()
        '    obj.Ejecutar("HorarioPE_RegistrarAmbienteSol", codigo_lho, CInt(Page.Request.QueryString("codigo_amb")), Page.Request.QueryString("estado"))
        '    If EnviarCorreo(codigo_lho) Then
        '        Session("h_codigolho") = 0
        '        Me.PanelExaRec.visible = True
        '        Me.PanelHorarioRegistro.visible = False
        '        Me.PanelBuscar.visible = False

        '    Else
        '        Response.Write("<script>alert('Ocurrió un error al enviar el correo 1')</script>")
        '    End If
        'End If
    End Sub

    Private Sub CargarCentroCostos()
        '=================================
        'Permisos por CECO
        '=================================
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'objfun.CargarListas(cboCecos, obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), "", Request.QueryString("mod")), "codigo_Cco", "Nombre", ">> Seleccione<<")
        'objfun.CargarListas(cboCecos, obj.TraerDataTable("EVE_BuscaCcoId", Request.QueryString("id"), Request.QueryString("ctf"), Request.QueryString("mod"), "N"), "codigo_Cco", "descripcion_Cco", ">> Seleccione<<")

        '07.06.19 se cambió valor de  Request.QueryString("ctf") a 0  para el parametro ctf , debido a que en esta pagina solo debe filtrar por las carreras que tenga el usuario en el tipo de estudio
        If Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 85 Or Request.QueryString("ctf") = 143 Or Request.QueryString("ctf") = 15 Then
            objfun.CargarListas(cboCecos, obj.TraerDataTable("EVE_BuscaCcoId", Request.QueryString("id"), 0, Request.QueryString("mod"), "T"), "codigo_Cco", "descripcion_Cco")
        Else
            objfun.CargarListas(cboCecos, obj.TraerDataTable("EVE_BuscaCcoId", Request.QueryString("id"), 0, Request.QueryString("mod"), "N"), "codigo_Cco", "descripcion_Cco", ">> Seleccione<<")
        End If



    End Sub

    Function EnviarCorreo(ByVal codigo_lho As Integer) As Boolean
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tbCorreo As New Data.DataTable
        obj.AbrirConexion()
        tbCorreo = obj.TraerDataTable("HorarioPE_EnviarCorreo", codigo_lho)
        obj.CerrarConexion()
        obj = Nothing
        Dim objCorreo As New ClsEnvioMailAmbiente
        Dim bodycorreo As String
        If tbCorreo.Rows.Count Then
            bodycorreo = "<html>"
            bodycorreo = bodycorreo & "<body style=""font-size:12px;text-align:justify; font-family:Tahoma;""> <div style=""color:#284775; Background-color:white; border-color:#284775; border:1px solid; padding:10px;"">"
            bodycorreo = bodycorreo & "<p><b>" & tbCorreo.Rows(0).Item("header") & "</b></p>"
            bodycorreo = bodycorreo & "<p>" & tbCorreo.Rows(0).Item("cco") & "</p>"
            bodycorreo = bodycorreo & "<p>" & tbCorreo.Rows(0).Item("descripcion") & "</p>"
            bodycorreo = bodycorreo & "<p>" & tbCorreo.Rows(0).Item("body") & "</p>"
            bodycorreo = bodycorreo & "<table style=""font-size:12px;font-family:Tahoma;border:#99bae2 1px solid;"" cellSpacing=0 cellPadding=4 border=""0"">"
            bodycorreo = bodycorreo & "<tr style=""color:  #284775; background-color:#E8EEF7; font-weight:bold;""><td>Día</td><td>Fecha</td><td>Ambiente</td><td>Horario</td><td>Capacidad</td><td>Ubicación</td></tr>"
            bodycorreo = bodycorreo & "<tr><td>" & tbCorreo.Rows(0).Item("dia") & "</td><td>" & tbCorreo.Rows(0).Item("fechaInicio") & "</td><td>" & tbCorreo.Rows(0).Item("Ambiente") & "</td><td>" & tbCorreo.Rows(0).Item("Horario") & "</td><td style=""text-align:center;"">" & tbCorreo.Rows(0).Item("capacidad") & "</td><td>" & tbCorreo.Rows(0).Item("ubicacion") & "</td></tr>"
            bodycorreo = bodycorreo & "</table>"
            bodycorreo = bodycorreo & "<p>" & tbCorreo.Rows(0).Item("footer") & "</p>"
            bodycorreo = bodycorreo & "<p> Atte,<br/><b>" & tbCorreo.Rows(0).Item("firma") & "</b></p>"
            bodycorreo = bodycorreo & "</div></body></html>"
            Try
                'tbCorreo.Rows(0).Item("EnviarA") = "yperez@usat.edu.pe"
                'tbCorreo.Rows(0).Item("cc") = ""
                objCorreo.EnviarMailAd("campusvirtual@usat.edu.pe", tbCorreo.Rows(0).Item("firma"), tbCorreo.Rows(0).Item("EnviarA"), tbCorreo.Rows(0).Item("SubjectA") & " - Módulo de Solicitud de Ambientes", bodycorreo, True, tbCorreo.Rows(0).Item("cc"))
                Return True
            Catch ex As Exception
                Response.Write("<script>alert('" & ex.Message & "')</script>")
            End Try
        Else
            objCorreo.EnviarMailAd("campusvirtual@usat.edu.pe", "error", "yperez@usat.edu.pe", "error - Módulo de Solicitud de Ambientes", codigo_lho, True, "")
            Return True
        End If
    End Function

    Private Sub VerificaCronograma()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim dt As New Data.DataTable
        Try            
            dt = obj.TraerDataTable("ACAD_VerificaCronogramaExamenPP_v3", Me.cboCiclo.SelectedValue, Request.QueryString("mod"))
            'andy.diaz 23/12/2020: Habilito las operaciones para COORD. DIRECCIÓN ACADÉMICA y DIRECCIÓN ACADÉMICA
            'If (dt.Rows.Count = 0) Then
            '    Me.btnBuscar.Enabled = False
            '    Me.btnGenerar.Enabled = False
            '    Me.lblMensaje.Text = "El cronograma no permite registrar examenes sustitutorios."
            'Else
            '    Me.btnBuscar.Enabled = True
            '    Me.btnGenerar.Enabled = True
            '    Me.lblMensaje.Text = ""
            'End If

            Dim tfuCoordinadorDirAcad As Integer = 85
            Dim tfuAdmin As Integer = 1
            Dim tienePermiso As Boolean = (Request.QueryString("ctf") = tfuCoordinadorDirAcad _
                                           OrElse Request.QueryString("ctf") = tfuAdmin)
            If dt.Rows.Count > 0 OrElse tienePermiso Then
                Me.btnBuscar.Enabled = True
                Me.btnGenerar.Enabled = True
                Me.lblMensaje.Text = ""
            Else
                Me.btnBuscar.Enabled = False
                Me.btnGenerar.Enabled = False
                Me.lblMensaje.Text = "El cronograma no permite registrar examenes sustitutorios."
            End If
            '/andy.diaz 23/12/2020
        Catch ex As Exception
            Response.Write(ex)
            Me.lblMensaje.Text = "Error al verificar el cronograma: " & ex.Message
            Me.btnBuscar.Enabled = False
            Me.btnGenerar.Enabled = False
        End Try
    End Sub

    Private Sub VerificaTipoUsuario()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim dt As New Data.DataTable
        Try
            'Verificamos si el profesor es de filosofia
            dt = obj.TraerDataTable("ACAD_VerificaProfesorFilosofia", Session("id_per"))
            If (dt.Rows.Count = 0) Then
                Me.HdTipo.Value = "N"   'No es de Filosofia
            ElseIf (dt.Rows.Count > 0) Then
                Me.HdTipo.Value = "S"   'Si es de Filosofia
            End If

            If Request.QueryString("ctf") = 90 Then

            End If

        Catch ex As Exception
            Me.lblMensaje.Text = "Error al reconocer el usuario: " & ex.Message
        End Try
    End Sub

    Private Sub CargaCiclo()
        Dim dt As New Data.DataTable
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            dt = obj.TraerDataTable("ACAD_ListaCicloAcademico")
            obj = Nothing

            Me.cboCiclo.DataSource = dt
            Me.cboCiclo.DataTextField = "descripcion_cac"
            Me.cboCiclo.DataValueField = "codigo_cac"
            Me.cboCiclo.DataBind()
            dt = Nothing

        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar los ciclos académicos: " & ex.Message
        End Try
    End Sub

    Private Sub CargaEscuela()

    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Buscar()
        If Me.cboEstado.SelectedValue = "1" Then
            btnGenerar.enabled = False
            btnCopiarHorario.enabled = True
        Else
            btnGenerar.enabled = True
            btnCopiarHorario.enabled = False
        End If
    End Sub

    Protected Sub gvDatos_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvDatos.RowCancelingEdit
        gvDatos.EditIndex = -1
        Me.btnBuscar.Enabled = True
        Me.btnGenerar.Enabled = True
        Buscar()
    End Sub

    Protected Sub gvDatos_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvDatos.RowEditing
        Try
            Me.btnBuscar.Enabled = False
            Me.btnGenerar.Enabled = False
            gvDatos.EditIndex = e.NewEditIndex

            Dim id As Integer = Convert.ToInt32(gvDatos.DataKeys(e.NewEditIndex).Value)
            Dim row As DataRow = BuscarRegistro(id) 'Debe ir la fila

            Buscar()

            Dim combo As DropDownList = TryCast(gvDatos.Rows(e.NewEditIndex).FindControl("ddlDocente"), DropDownList)
            Dim texto As TextBox = TryCast(gvDatos.Rows(e.NewEditIndex).FindControl("txtAsistencia"), TextBox)

            If combo IsNot Nothing Then
                combo.DataSource = ListaDocente()
                combo.DataTextField = "Docente"
                combo.DataValueField = "codigo_per"
                combo.DataBind()
            End If
            'Response.Write("<script>alert(" & Convert.ToString(row("codigo_per")) & ");</script>")
            combo.SelectedValue = Convert.ToString(row("codigo_per"))
            texto.Text = Convert.ToString(row("asistenciarec_cup"))
        Catch ex As Exception
            Me.lblMensaje.Text = "Error: " & ex.Message & "<>" & ex.Source
        End Try
    End Sub

    Private Sub Buscar()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim dt As New Data.DataTable
        Dim Tipo As Integer = 0
        Try
            If (Me.HdTipo.Value = "S") Then
                Tipo = 2
            ElseIf (Me.HdTipo.Value = "N") Then
                Tipo = 0
            End If

            dt = obj.TraerDataTable("ACAD_ListaCursosProgramadosPP", Me.cboEstado.SelectedValue + Tipo, 0, Me.cboCiclo.SelectedValue, Me.txtCurso.Text, Me.cboCecos.SelectedValue, Me.Request.QueryString("mod"))

            If (Me.cboEstado.SelectedValue = 0) Then
                Me.gvDatos.DataSource = dt
                Me.gvDatos.DataBind()

                Me.gvDatos.Visible = True
                Me.gvProgramado.Visible = False
            Else
                Me.gvProgramado.DataSource = dt
                Me.gvProgramado.DataBind()

                Me.gvDatos.Visible = False
                Me.gvProgramado.Visible = True

            End If

            dt = Nothing
            obj = Nothing
            Me.lblMensaje.Text = ""

            'Session("HB_codigo_cpf") = Me.cboEscuela.SelectedValue
            Session("HB__codigo_cco") = Me.cboCecos.SelectedValue
            Session("HB_estado") = Me.cboEstado.SelectedValue
            Session("HB_curso") = Me.txtCurso.Text


        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar la consulta: " & ex.Message
        End Try
    End Sub

    Private Function BuscarRegistro(ByVal codigo_cup As Integer) As DataRow
        Dim dt As New Data.DataTable
        Try
            Dim Tipo As Integer = 0



            If (Me.HdTipo.Value = "S") Then
                Tipo = 2
            ElseIf (Me.HdTipo.Value = "N") Then
                Tipo = 0
            End If

            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            dt = obj.TraerDataTable("ACAD_ListaCursosProgramadosPP", Me.cboEstado.SelectedValue + Tipo, codigo_cup, Me.cboCiclo.SelectedValue, "", 0)
            obj = Nothing

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al buscar el registro: " & ex.Message
            Return Nothing
        End Try
    End Function

    Private Function ListaDocente() As Data.DataTable
        Dim dt As New Data.DataTable
        Try
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            dt = obj.TraerDataTable("ACAD_ListaDocentes")
            obj = Nothing

            Return dt
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al buscar al cargar docentes: " & ex.Message
            Return Nothing
        End Try
    End Function

    Protected Sub gvDatos_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvDatos.RowUpdating
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim dt As New DataTable
        Dim dtCarga As New DataTable
        Dim texto As TextBox
        Dim sw As Byte = 0
        Try
            Dim id As Integer = Convert.ToInt32(gvDatos.DataKeys(e.RowIndex).Value)
            Dim combo As DropDownList = TryCast(gvDatos.Rows(e.RowIndex).FindControl("ddlDocente"), DropDownList)
            Dim personal As Integer = Convert.ToInt32(combo.SelectedValue)

            texto = TryCast(gvDatos.Rows(e.RowIndex).FindControl("txtAsistencia"), TextBox)

            'Buscamos cursos relacionados

            dt = obj.TraerDataTable("ACAD_BuscaCursosxCursoPadre", id)

            'For i As Integer = 0 To dt.Rows.Count - 1
            'dtCarga = obj.TraerDataTable("ACAD_VerificaCargaAcademica", dt.Rows(i).Item("codigo_Cup"))
            'If (dtCarga.Rows.Count = 0) Then
            'sw = 1
            'Me.lblMensaje.Text = "El curso no tiene docente asignado."
            'End If
            'Next

            If (sw = 0) Then
                Me.lblMensaje.Text = "sw = 0"
                For i As Integer = 0 To dt.Rows.Count - 1
                    'Generamos los cursos para el examen de recuperacion
                    obj.Ejecutar("ACAD_CreaCursoSustitutorio", dt.Rows(i).Item("codigo_Cup"), personal, Session("id_per"), 0)
                Next
            End If

            gvDatos.EditIndex = -1
            Me.btnBuscar.Enabled = True
            Me.btnGenerar.Enabled = True
            Buscar()
        Catch ex As Exception
            Me.lblMensaje.Text = " Error al actualizar registro: " & ex.Message
        End Try
    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim Fila As GridViewRow
        Dim dt As New DataTable
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim sw As Byte = 0
        Try
            For I As Int16 = 0 To Me.gvDatos.Rows.Count - 1
                Fila = Me.gvDatos.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        sw = 1
                    End If
                End If
            Next

            If (sw = 0) Then
                Me.lblMensaje.Text = "Debe seleccionar algun registro."
                Exit Sub
            End If

            For I As Int16 = 0 To Me.gvDatos.Rows.Count - 1
                Fila = Me.gvDatos.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    'Solo los que tienen el check activo
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        'Buscamos cursos relacionados
                        dt = obj.TraerDataTable("ACAD_BuscaCursosxCursoPadre", gvDatos.DataKeys(I).Value)
                        'Registramos los cursos relaciondos                              
                        For j As Integer = 0 To dt.Rows.Count - 1
                            obj.Ejecutar("ACAD_CreaCursoSustitutorio", dt.Rows(j).Item("codigo_cup"), 0, Session("id_per"), 0)
                        Next
                    End If
                End If
            Next

            Me.lblMensaje.Text = ""
            Buscar()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al generar: " & ex.Message
        End Try
    End Sub

    Protected Sub gvProgramado_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvProgramado.RowDeleting
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim dt As New DataTable
        Try
            'Buscamos matriculados en los cursos programados para examen
            dt = obj.TraerDataTable("ACAD_VerificaMatriculadosGrupo", gvProgramado.DataKeys(e.RowIndex).Value.ToString)
            If (dt.Rows.Count = 0) Then
                'Si no existen matriculados elimina el curso
                obj.Ejecutar("ACAD_EliminaGrupoRecuperacion", gvProgramado.DataKeys(e.RowIndex).Value.ToString)
                Me.lblMensaje.Text = ""
            Else
                Me.lblMensaje.Text = "No se puede eliminar el curso porque tiene alumnos matriculados."
            End If
            Buscar()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al eliminar curso: " & ex.Message
        End Try
    End Sub

    Protected Sub gvProgramado_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProgramado.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "SolicitarAmbiente") Then
            Session("h_codigo_cup") = gvProgramado.DataKeys(index).Values("codigo_cup")
            Me.PanelHorarioRegistro.visible = True
            Me.PanelExaRec.visible = False
            Me.lblNombreCur.text = Me.cboCiclo.selecteditem.text & " - " & gvProgramado.DataKeys(index).Values("nombre_Cur") & " - " & gvProgramado.DataKeys(index).Values("grupoHor_Cup")
            Me.txtDescripcion.text = Me.lblNombreCur.text
            Me.lblNombreCarrera.Text = Me.cboCecos.SelectedItem.Text
            Me.lblNombreCur0.text = Me.lblNombreCur.text
            Me.lblNombreCarrera0.text = Me.lblNombreCarrera.text
            Me.btnBuscar.enabled = False
            Me.btnGenerar.enabled = False
        End If
    End Sub

    Sub CargarFechasExamen()
        ddlDia.Items.clear()
        'Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Dim dt As New Data.DataTable
        Dim fechaInicio As Date
        Dim fechaFin As Date
        ClsFunciones.LlenarListas(Me.ddlTipoAmbiente, obj.TraerDataTable("AsignarAmbiente_ListarAmbientes"), "codigo_tam", "descripcion_Tam", "<<TODOS>>")
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("horarioPe_consultarCronogramaRecPP", Me.cboCiclo.SelectedValue, Request.QueryString("mod"))
        obj.CerrarConexion()
        obj = Nothing
        If tb.Rows.Count > 0 Then
            fechaInicio = tb.Rows(0).Item("fechaIni_Cro")
            fechaFin = tb.Rows(0).Item("fechaFin_Cro")

            Dim nombreFecha As String = ""
            If fechaInicio < fechaFin Then
                Do
                    Dim fechaInicioG As New Date(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day)
                    nombreFecha = WeekdayName(Weekday(fechaInicio)).ToUpper & " " & fechaInicioG.ToString.Substring(0, 10)
                    ddlDia.Items.Add(New ListItem(nombreFecha, fechaInicioG))
                    fechaInicio = fechaInicio.AddDays(1)
                Loop While fechaInicio <= fechaFin
            End If
        End If

    End Sub

    Sub CargarFechas()
        Dim item As String
        For i As Integer = 7 To 23
            If i < 10 Then
                item = "0" & i.ToString
            Else
                item = i.ToString
            End If
            Me.ddlInicioHora.Items.Add(New ListItem(item.ToString(), item.ToString()))
            Me.ddlFinHora.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i
        Me.ddlInicioHora.SelectedIndex = 0
        Me.ddlFinHora.SelectedIndex = 1

        For i As Integer = 0 To 59
            If i < 10 Then
                item = "0" & i.ToString
            Else
                item = i.ToString
            End If
            Me.ddlInicioMinuto.Items.Add(New ListItem(item.ToString(), item.ToString()))
            Me.ddlFinMinuto.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i
        Me.ddlFinHora.SelectedIndex = 21

    End Sub

    Sub CargarCapacidad()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.ddlCap.DataSource = obj.TraerDataTable("HorarioPE_ConsultarCapacidad")
        Me.ddlCap.DataTextField = "capacidad_amb"
        Me.ddlCap.DataValueField = "capacidad_amb"
        Me.ddlCap.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub cboCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCiclo.SelectedIndexChanged
        VerificaCronograma()
        CargarFechasExamen()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Session("h_codigo_cup") = 0
        Me.PanelHorarioRegistro.visible = False
        Me.PanelExaRec.visible = True
        Me.btnBuscar.enabled = True
        Me.btnGenerar.enabled = True
    End Sub

    Protected Sub btnRegistrarPers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrarPers.Click
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim día As String
        Dim nombre_hor As String = ""
        Dim horaFin As String = ""
        Dim usu As Integer = CInt(Session("id_per"))
        Dim fechaInicio As New Date

        día = WeekdayName(Weekday(CDate(Me.ddlDia.SelectedValue))).substring(0, 2).toupper()
        nombre_hor = Me.ddlInicioHora.SelectedItem.Text & ":" & Me.ddlInicioMinuto.SelectedItem.Text
        horaFin = Me.ddlFinHora.SelectedItem.Text & ":" & Me.ddlFinMinuto.SelectedItem.Text
        fechaInicio = New Date(CInt(DatePart(DateInterval.Year, CDate(Me.ddlDia.SelectedValue))), CInt(DatePart(DateInterval.Month, CDate(Me.ddlDia.SelectedValue))), CInt(DatePart(DateInterval.Day, CDate(Me.ddlDia.SelectedValue))), CInt(nombre_hor.Substring(0, 2)), CInt(nombre_hor.Substring(3, 2)), 0)

        obj.AbrirConexion()
        tb = obj.TraerDataTable("HorarioPE_Registrar", día, Session("h_codigo_cup"), nombre_hor, horaFin, usu, fechaInicio, fechaInicio, 1, IIf(Me.txtDescripcion.Text.Length > 0, Me.txtDescripcion.Text.Trim, DBNull.Value), Me.ddlCap.SelectedValue, 12, 0)
        obj.CerrarConexion()

        If tb.Rows.Count > 0 Then
            Session("h_codigolho") = tb.Rows(0).Item(0)
            Me.lblHorario.text = día & " " & Me.ddlDia.SelectedValue & " " & nombre_hor & " a " & horaFin

            Me.PanelHorarioRegistro.visible = False
            Me.panelBuscar.visible = True
        End If
    End Sub



    Protected Sub gridAmbientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAmbientes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If gridAmbientes.DataKeys(e.Row.RowIndex).Values("Accion").ToString > 0 Then
                Dim tb As New Data.DataTable
                Dim ultimo As Integer = e.Row.Cells.Count
                tb = gridAmbientes.DataSource

                'Preferencial
                If e.Row.Cells(0).Text = "S" Then
                    e.Row.Cells(0).Text = "<img src='private/images/star.png' title='Ambiente preferencial'>"
                End If
                'Normal
                If e.Row.Cells(0).Text = "N" Then
                    e.Row.Cells(0).Text = "<img src='private/images/door.png' title='Ambiente'>"
                End If
                tb.Dispose()
            End If
        End If
    End Sub

    Protected Sub btnBuscarH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarH.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tb As New Data.DataTable
        Dim tbAmb As New Data.DataTable
        Dim idsamb As String = ""
        obj.AbrirConexion()
        tb = obj.TraerDataTable("HorarioPE_ConsultarAsignacionAmbienteSol2", Session("h_codigolho"), 0, 0, 0, 0, 0, 0, Me.ddlTipoAmbiente.SelectedValue)

        obj.CerrarConexion()
        If tb.Rows.Count Then
            For i As Integer = 0 To tb.Rows.Count - 1
                idsamb = idsamb & tb.Rows(i).Item("codigo_amb").ToString & ","
            Next
            idsamb = idsamb.ToString.Substring(0, idsamb.Length - 1)
            obj.AbrirConexion()
            Me.gridAmbientes.DataSource = obj.TraerDataTable("Ambiente_ListarAmbienteCaracSol", idsamb)
            Me.gridAmbientes.DataBind()
            obj.CerrarConexion()
        Else
            Me.gridAmbientes.DataSource = Nothing
            Me.gridAmbientes.DataBind()
        End If
        obj = Nothing
    End Sub

    Protected Sub gridAmbientes_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridAmbientes.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "AsignarAmbiente") Then
            Dim codigo_lho As Integer = 0, estado As String = ""
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            codigo_lho = Session("h_codigolho")
            obj.AbrirConexion()
            If gridAmbientes.DataKeys(index).Values("Tipo") = "S" Then
                estado = "P"
            Else
                estado = "A"
            End If


            obj.Ejecutar("HorarioPE_RegistrarAmbienteSol", codigo_lho, gridAmbientes.DataKeys(index).Values("Accion"), estado)
            If EnviarCorreo(codigo_lho) Then
                Session("h_codigolho") = 0
                Me.PanelExaRec.visible = True
                Me.PanelHorarioRegistro.visible = False
                Me.PanelBuscar.visible = False
                Me.btnBuscar.enabled = True
                Me.btnGenerar.enabled = True

                btnBuscar_Click(sender, e)
                Me.gridAmbientes.datasource = Nothing
                Me.gridAmbientes.databind()
                Me.ddltipoambiente.selectedindex = 0
            Else
                'Response.Write("<script>alert('Ocurrió un error al enviar el correo 1')</script>")
            End If
        End If
    End Sub


    Protected Sub gvProgramado_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvProgramado.RowDataBound
        Dim btnHorario As Button
        Dim chkPadre As CheckBox
        Dim chkHijo As CheckBox
        btnHorario = e.Row.FindControl("Button1")
        chkPadre = e.Row.FindControl("chkElegirPadre")
        chkHijo = e.Row.FindControl("chkElegirHijo")

        If e.Row.RowType = DataControlRowType.DataRow Then
            If gvProgramado.DataKeys(e.Row.RowIndex).Values("ambiente") <> "-" Then
                btnHorario.Enabled = False
                chkPadre.Enabled = True
                chkHijo.Enabled = False
            Else
                btnHorario.Enabled = True
                chkPadre.Enabled = False
                chkHijo.Enabled = True
            End If

        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        Me.PanelBuscar.visible = False
        Me.pnlPregunta.visible = True
        Me.lblFecha.text = Me.lblHorario.text
        Me.lblActividad.text = txtDescripcion.text


    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        Me.PanelBuscar.visible = True
        Me.pnlPregunta.visible = False

    End Sub

    Protected Sub btnSi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSi.Click
        'Elimnar el registro de horario


        If Session("h_codigolho") IsNot Nothing Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("HorarioPE_EliminarLH", CInt(Session("h_codigolho")))
            obj.CerrarConexion()
            obj = Nothing
            Session("h_codigolho") = 0
            Session("h_codigo_cup") = 0
            Me.PanelHorarioRegistro.visible = False
            Me.PanelExaRec.visible = True
            Me.btnBuscar.enabled = True
            Me.btnGenerar.enabled = True
            Me.pnlPregunta.visible = False
        End If

    End Sub

    Protected Sub btnCopiarHorario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopiarHorario.Click
        Dim Fila As GridViewRow
        Dim FilaPadre As GridViewRow
        Dim FilaHijo As GridViewRow
        Dim dt As New DataTable

        Dim sw As Byte = 0
        Dim numPadres As Integer = 0
        Dim numHijos As Integer = 0

        ' #Solo puede eligir un registro principal
        For I As Int16 = 0 To Me.gvProgramado.Rows.Count - 1
            Fila = Me.gvProgramado.Rows(I)
            If Fila.RowType = DataControlRowType.DataRow Then
                If CType(Fila.FindControl("chkElegirPadre"), CheckBox).Checked = True Then
                    numPadres = numPadres + 1
                End If
            End If
        Next

        If (numPadres > 1) Then
            Me.lblMensaje.Text = "Solo puede elegir un registro principal"
            Exit Sub
        Else
            Me.lblMensaje.Text = ""
        End If

        '# Debe elegir por lo menos 1 registro hijo
        For I As Int16 = 0 To Me.gvProgramado.Rows.Count - 1
            Fila = Me.gvProgramado.Rows(I)
            If Fila.RowType = DataControlRowType.DataRow Then
                If CType(Fila.FindControl("chkElegirHijo"), CheckBox).Checked = True Then
                    numHijos = numHijos + 1
                End If
            End If
        Next

        If (numHijos < 1) Then
            Me.lblMensaje.Text = "Debe elegir por lo menos 1 registro sin ambiente"
            Exit Sub
        Else
            Me.lblMensaje.Text = ""
        End If


        '#Copiar horarios
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString




        For I As Int16 = 0 To Me.gvProgramado.Rows.Count - 1
            FilaPadre = Me.gvProgramado.Rows(I)
            If FilaPadre.RowType = DataControlRowType.DataRow Then
                If CType(FilaPadre.FindControl("chkElegirPadre"), CheckBox).Checked = True Then
                    For j As Integer = 0 To Me.gvProgramado.Rows.Count - 1
                        FilaHijo = Me.gvProgramado.Rows(j)
                        If FilaHijo.RowType = DataControlRowType.DataRow Then
                            If CType(FilaHijo.FindControl("chkElegirHijo"), CheckBox).Checked = True Then
                                obj.AbrirConexion()
                                obj.Ejecutar("Acad_RecuperacionCopiarHorario", gvProgramado.DataKeys(I).Values("codigo_cup"), gvProgramado.DataKeys(j).Values("codigo_cup"))
                                obj.CerrarConexion()
                            End If
                        End If
                    Next
                End If
            End If
        Next

        btnBuscar_Click(sender, e)
    End Sub
End Class


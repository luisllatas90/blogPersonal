Partial Class academico_horarios_frmCambioAmbiente
    Inherits System.Web.UI.Page
#Region "Variables"

    Dim tipoestudio, codigo_tfu, curso, codigo_cac, tipo, codigo_usu As String
    Dim md_Funciones As d_Funciones
    Dim md_Horario As d_Horario
    Dim me_CicloAcademico As e_CicloAcademico
    Dim me_CarreraProfesional As e_CarreraProfesional
    Dim me_Horario As e_Horario


    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum
#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        codigo_tfu = Request.QueryString("ctf")
        'tipoestudio = Request.QueryString("mod")
        tipoestudio = "2"
        codigo_usu = Request.QueryString("id")



        'If (Request.QueryString("mod") <> 2) Then
        '    If (Request.QueryString("mod") <> 10) Then
        '        tipoestudio = Request.QueryString("mod")
        '    Else
        '        tipoestudio = 10
        '    End If
        'Else
        '    tipoestudio = 2
        'End If

        If curso = "" Then curso = "%"
        'If codigo_cac = "" Then codigo_cac = Session("codigo_cac")

        If IsPostBack = False Then
            Call mt_CargarComboCicloAcademico()
            Call mt_cargarCarrreraProfesional()
        End If
    End Sub

    Protected Sub lbBusca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbBusca.Click
        Try
            'validar
            If ddlSemAca.SelectedValue = "" Then
                Call mt_ShowMessage("Complete los datos de búsqueda", MessageType.warning)
            End If
            Call mt_cargarCursosProgramados()
            Me.udpListado.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub

    Protected Sub gvListaCursosProg_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaCursosProg.RowCommand
        Dim codigo_cup, nombre_cur_tabReg, grupo_tabReg As String
        'Dim codigo_amb_tabReg, nombre_cur_tabReg, ambiente_tabReg, codigo_Lho_tabReg, capac_tabReg, hIni_tabReg, hFin_tabReg, dia_tabReg, grupo_tabReg, matriculados, codigo_cac_tabReg As String
        Try

            Dim index As Integer = 0 : index = CInt(e.CommandArgument)

            codigo_cup = Me.gvListaCursosProg.DataKeys(index).Values("codigo_cup")

            nombre_cur_tabReg = Me.gvListaCursosProg.DataKeys(index).Values("nombre_Cur")
            'ambiente_tabReg = Me.gvListaCursosProg.DataKeys(index).Values("descripcion_amb") + " - " + Me.gvListaCursosProg.DataKeys(index).Values("descripcionReal_amb")
            'codigo_Lho_tabReg = Me.gvListaCursosProg.DataKeys(index).Values("codigo_Lho")
            'codigo_amb_tabReg = Me.gvListaCursosProg.DataKeys(index).Values("codigo_amb")
            'capac_tabReg = Me.gvListaCursosProg.DataKeys(index).Values("capacidad_amb")
            'dia_tabReg = Me.gvListaCursosProg.DataKeys(index).Values("dia_lho")
            'hIni_tabReg = Me.gvListaCursosProg.DataKeys(index).Values("nombre_Hor")
            'hFin_tabReg = Me.gvListaCursosProg.DataKeys(index).Values("horaFin_Lho")
            grupo_tabReg = Me.gvListaCursosProg.DataKeys(index).Values("grupoHor_cup")
            'matriculados = Me.gvListaCursosProg.DataKeys(index).Values("nroMatriculados_cup")

            Select Case e.CommandName
                Case "selCambHor"
                    Call mt_cargarFormAmbiente(codigo_cup, nombre_cur_tabReg, grupo_tabReg)

                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('registro-tab');", True)
                    Me.udpRegistro.Update()

                    'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModal", "openModal();", True)
                    'Me.udpModalAmbiente.Update()
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub

    Protected Sub gvListaCursosProg_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaCursosProg.RowDataBound
        'Para armar el datatable
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub lbBuscaAmb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbBuscaAmb.Click
        Try
            Call mt_cargarAmbientesParaCambiar()
            Me.udpListaAmbientes.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub lbRetornoListaAmb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbRetornoListaAmb.Click
        Call mt_mostrarColCursosAmbientes()
        Call mt_cargarCursosProgramados()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)
        Me.udpListado.Update()

    End Sub

    Protected Sub gvListaAmbientesCursos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaAmbientesCursos.RowCommand
        Dim codigo_cup_tabEnv, codigo_lho_tabEnv, ambiente_lho_tabEnv As String

        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            codigo_cup_tabEnv = Me.gvListaAmbientesCursos.DataKeys(index).Values("codigo_cup")
            codigo_lho_tabEnv = Me.gvListaAmbientesCursos.DataKeys(index).Values("codigo_lho")
            ambiente_lho_tabEnv = Me.gvListaAmbientesCursos.DataKeys(index).Values("descripcionReal_amb")

            Me.txtCod_lhoTabEnv.Text = codigo_lho_tabEnv
            Me.hfCod_lhoTabEnv.Value = codigo_lho_tabEnv

            Me.txtCurso_tabEnv.Text = Me.txtCursoTabReg.Text
            Me.txtAmbiente_tabEnv.Text = ambiente_lho_tabEnv
            Me.txtCapacTabEnv.Text = Me.gvListaAmbientesCursos.DataKeys(index).Values("capacidad_amb")
            Me.txtDiaTabEnv.Text = Me.gvListaAmbientesCursos.DataKeys(index).Values("dia_lho")
            Me.txtHorIniTabEnv.Text = Me.gvListaAmbientesCursos.DataKeys(index).Values("nombre_Hor")
            Me.txtHorFinTabEnv.Text = Me.gvListaAmbientesCursos.DataKeys(index).Values("horaFin_Lho")
            Me.txtMatTabEnv.Text = Me.gvListaAmbientesCursos.DataKeys(index).Values("nroMatriculados_cup")

            Select Case e.CommandName

                Case "selCambHor"
                    md_Horario = New d_Horario : me_Horario = New e_Horario
                    Dim dt As New Data.DataTable

                    With me_Horario
                        .operacion = "GEN"
                        .codigo_tes = tipoestudio
                        .codigo_cac = Me.ddlSemAca.SelectedValue
                        .codigo_act = "8" ' -- es el de ambientes y grupo ohorario
                    End With

                    dt = md_Horario.Horario_ValidaCambioAmbiente(me_Horario)
                    'validamos 
                    If dt.Rows.Count > 0 Then
                        With dt.Rows(0)
                            If .Item("vigente_cte") = "1" Then
                                If .Item("fechaFin_Cro") < Date.Today Then
                                    Call mt_ShowMessage("La fecha actual no permite realizar cambios por estar fuera de cronograma. Comunicarse con Dirección Académica", MessageType.warning)
                                    Me.udpRegistro.Update()
                                    Exit Sub
                                End If
                            Else
                                Call mt_ShowMessage("El ciclo académico seleccionado no se encuentra vigente actualmente", MessageType.warning)
                                Me.udpRegistro.Update()
                                Exit Sub
                            End If
                        End With
                    Else
                        Call mt_ShowMessage("No se encontraron ambientes requeridos", MessageType.warning)
                    End If

                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('envio-tab');", True)
                    Me.udpEnvio.Update()

            End Select


          

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub lbRetTabEnv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbRetTabEnv.Click
        Call mt_LimpiarTabEnvio()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('registro-tab');", True)
        Me.udpRegistro.Update()
    End Sub

    Protected Sub gvAmbParaCambiar_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAmbParaCambiar.RowCommand
        Dim codLhoNuevo, codAmbNuevo, descAmbNuevo, capAmbNuevo As String

        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)

            codLhoNuevo = Me.gvAmbParaCambiar.DataKeys(index).Values("codigo_lho")
            codAmbNuevo = Me.gvAmbParaCambiar.DataKeys(index).Values("codigo_Amb")
            descAmbNuevo = Me.gvAmbParaCambiar.DataKeys(index).Values("descripcion_Amb") & " - " & Me.gvAmbParaCambiar.DataKeys(index).Values("descripcionReal_Amb")
            capAmbNuevo = Me.gvAmbParaCambiar.DataKeys(index).Values("capacidad_Amb")

            Select Case e.CommandName
                Case "CambiarAmb"

                    For Each Fila As GridViewRow In Me.gvListaAmbientesCursos.Rows


                        'this.dataGridView.Rows[x].Cells["IdCliente"].Value.ToString(); 

                        If Fila.Cells(0).Text = Me.hfCod_lhoTabEnv.Value Then
                            Fila.Cells(11).Text = codLhoNuevo
                            Fila.Cells(12).Text = codAmbNuevo
                            Fila.Cells(13).Text = descAmbNuevo
                            Fila.Cells(14).Text = capAmbNuevo
                            'Negrita
                            Fila.Cells(13).Font.Bold = True
                            Fila.Cells(13).BackColor = Drawing.Color.Yellow
                            Fila.Cells(14).Font.Bold = True
                            Fila.Cells(14).BackColor = Drawing.Color.Yellow

                        End If

                    Next

                    Call mt_LimpiarTabEnvio()

                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('registro-tab');", True)
                    Me.udpRegistro.Update()
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lbGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGuardar.Click
        Dim cuentaLineasNoCambiadas, filasTotal, numeroMayor As Integer
        Dim filasIguales As Boolean = True
        Dim numberOne, contador As Integer

        Try
            'validamos que las lineas no esten vacias
            filasTotal = Me.gvListaAmbientesCursos.Rows.Count
            cuentaLineasNoCambiadas = 0
            contador = 0
            For Each Fila As GridViewRow In Me.gvListaAmbientesCursos.Rows
                contador = contador + 1
                'si el codAmNuevo
                If Fila.Cells(11).Text = "" Then
                    cuentaLineasNoCambiadas = cuentaLineasNoCambiadas + 1
                End If
                'obtenemos la primera cantidad y revisamos si el numero es igual que el anterior
                If contador = 1 Then
                    numberOne = Fila.Cells(4).Text
                    numeroMayor = Fila.Cells(4).Text
                Else
                    'filas iguales
                    If Fila.Cells(4).Text = numberOne Then
                        filasIguales = True
                    Else
                        filasIguales = False
                    End If
                    'numero mayor
                    If Fila.Cells(4).Text > numeroMayor Then
                        numeroMayor = Fila.Cells(4).Text
                    End If
                End If
            Next

            

            'mt_ShowMessage("la primera cantidad es: " & numberOne & " y las filas son iguales? " & filasIguales & " y el numero mayor es: " & numeroMayor, MessageType.warning)
            'Me.udpRegistro.Update()


            'sin son filas iguales 
            If filasIguales Then
                If cuentaLineasNoCambiadas > 0 Then
                    Call mt_ShowMessage("Debe realizar cambio de ambiente a todos los horarios", MessageType.warning)
                    Me.udpRegistro.Update()
                    Exit Sub
                End If
            Else ' cuando las filas no son inguales
                ' y no ha ralizado nigun cambio
                If contador = cuentaLineasNoCambiadas Then
                    Call mt_ShowMessage("Debe realizar algún cambio", MessageType.warning)
                    Me.udpRegistro.Update()
                    Exit Sub
                End If
                'recorro la grilla y verifica que los nuevos ambientes la capacidad sea igual o mayor a la cantidad mayor
                For Each FilaG1 As GridViewRow In Me.gvListaAmbientesCursos.Rows
                    If FilaG1.Cells(14).Text <> "" Then
                        If FilaG1.Cells(14).Text < numeroMayor Then
                            Call mt_ShowMessage("Las cantidad de los nuevos ambientes, no son adecuadas", MessageType.warning)
                            Me.udpRegistro.Update()
                            Exit Sub
                        End If
                    End If
                Next

            End If

            'Call mt_ShowMessage("pase las validaciones", MessageType.warning)
            'Me.udpRegistro.Update()



            '''' Para actualizar el nuevo ambiente
            For Each Fila As GridViewRow In Me.gvListaAmbientesCursos.Rows

                If Fila.Cells(11).Text <> "" Then
                    md_Horario = New d_Horario : me_Horario = New e_Horario

                    Dim dt As New Data.DataTable

                    With me_Horario
                        .codigo_lho = Fila.Cells(0).Text
                        .codigo_amb = Fila.Cells(12).Text
                    End With

                    dt = md_Horario.Horario_CambiarAmbiente(me_Horario)
                End If

            Next
            '''' Para Intercambiar con el otro ambiente
            For Each Fila As GridViewRow In Me.gvListaAmbientesCursos.Rows

                If Fila.Cells(11).Text <> "" Then
                    md_Horario = New d_Horario : me_Horario = New e_Horario

                    Dim dt As New Data.DataTable

                    With me_Horario
                        .codigo_lho = Fila.Cells(11).Text
                        .codigo_amb = Fila.Cells(1).Text
                    End With

                    dt = md_Horario.Horario_CambiarAmbiente(me_Horario)
                End If

            Next
            Call mt_mostrarColCursosAmbientes()
            Call mtCargarAmbientesCursos(hfCodCupTabReg.Value)
            Me.txtCapReqTabEnv.Text = String.Empty

            Call mt_ShowMessage("Cambios Guardados", MessageType.success)
            Me.udpRegistro.Update()


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub


#End Region

#Region "Métodos y Funciones"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboCicloAcademico()
        Try
            md_Funciones = New d_Funciones
            md_Horario = New d_Horario
            me_CicloAcademico = New e_CicloAcademico

            Dim dt As New Data.DataTable

            With me_CicloAcademico
                .tipooperacion = "TO"
                .tipocac = "0"
            End With

            dt = md_Horario.ObtenerCicloAcademicoHorario(me_CicloAcademico)

            Call md_Funciones.CargarCombo(Me.ddlSemAca, dt, "codigo_cac", "descripcion_cac", True, "[-- SELECCIONE --]", "")

        Catch ex As Exception
            Throw ex
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_cargarCarrreraProfesional()

        'Response.Write(codigo_tfu + " " + tipoestudio + " " + codigo_usu)

        md_Funciones = New d_Funciones

        md_Horario = New d_Horario
        me_CarreraProfesional = New e_CarreraProfesional

        Dim dtCarrProf As New Data.DataTable

        Try
            With me_CarreraProfesional
                .operacion = "MA"
                .codigo_test = tipoestudio
            End With

            If codigo_tfu = 1 Or codigo_tfu = 7 Or codigo_tfu = 16 Or codigo_tfu = 18 Or codigo_tfu = 85 Or codigo_tfu = 181 Then
                tipo = "S"
                If (CInt(tipoestudio) <> 5) Then
                    If (CInt(tipoestudio) <> 10) Then
                        With me_CarreraProfesional
                            .operacion = "MA"
                            .codigo_test = tipoestudio
                        End With
                        'Set rsEscuela=obj.Consultar("ConsultarCarreraProfesional","FO","MA",tipoestudio)
                    Else
                        With me_CarreraProfesional
                            .operacion = "GO"
                            .codigo_test = "0"
                        End With
                        'rsEscuela = obj.Consultar("ConsultarCarreraProfesional", "FO", "GO", 0)
                    End If
                    dtCarrProf = md_Horario.ListarCarreraProfesionalHorario(me_CarreraProfesional)
                Else
                    With me_CarreraProfesional
                        .operacion = "0"
                        .codigo_test = ""
                    End With
                    'rsEscuela = obj.Consultar("ACAD_CarrerasPostgrado", "FO", 0, "")
                    dtCarrProf = md_Horario.ListarCarreraPostGrado(me_CarreraProfesional)
                End If

            Else
                If (CInt(tipoestudio) <> 5) Then
                    With me_CarreraProfesional
                        .operacion = "ESC"
                        .codigo_test = tipoestudio
                        .cod_user = codigo_usu
                    End With
                    dtCarrProf = md_Horario.ConsultarAcceso(me_CarreraProfesional)
                    'rsEscuela = obj.Consultar("consultaracceso", "FO", "ESC", tipoestudio, codigo_usu)
                Else
                    With me_CarreraProfesional
                        .operacion = "0"
                        .codigo_test = ""
                    End With
                    dtCarrProf = md_Horario.ListarCarreraPostGrado(me_CarreraProfesional)
                    'rsEscuela = obj.Consultar("ACAD_CarrerasPostgrado", "FO", 0, "")
                End If
            End If

            Call md_Funciones.CargarCombo(Me.ddlCarProf, dtCarrProf, "codigo_cpf", "nombre_cpf", True, "[-- TODOS --]", "0")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_cargarCursosProgramados()
        md_Horario = New d_Horario
        me_Horario = New e_Horario
        Dim dtAmbiente As New Data.DataTable

        Try
            With me_Horario
                .operacion = "GEN"
                .nombre_cur = Me.txtCursoList.Text
                .codigo_cac = Me.ddlSemAca.SelectedValue
                .codigo_cpf = Me.ddlCarProf.SelectedValue
            End With

            dtAmbiente = md_Horario.Horario_ListarCursosProgramadosParaCambiar(me_Horario)

            If dtAmbiente.Rows.Count > 0 Then

                Me.gvListaCursosProg.DataSource = dtAmbiente
                Me.gvListaCursosProg.DataBind()
            Else
                Call mt_ShowMessage("No se encontraron cursos programados", MessageType.warning)
            End If

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_cargarFormAmbiente(ByVal codigo_cup As String, ByVal nombre_cur As String, ByVal grupo As String)

        Try

            Call mtCargarAmbientesCursos(codigo_cup)
            Me.txtCursoTabReg.Text = nombre_cur
            Me.txtGrupTabReg.Text = grupo
            Me.txtCacTabReg.Text = Me.ddlSemAca.SelectedItem.Text
            Me.hfCodCupTabReg.Value = codigo_cup


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    '----- Carga la grilla Ambientes cursos aqui se oculta las columnas
    Private Sub mtCargarAmbientesCursos(ByVal codigo_cup As String)
        md_Horario = New d_Horario
        me_Horario = New e_Horario
        Dim dtAmb As New Data.DataTable

        Try
            With me_Horario
                .operacion = "GEN"
                .codigo_cup = codigo_cup

            End With

            dtAmb = md_Horario.Horario_ListarAmbientesCursos(me_Horario)

            If dtAmb.Rows.Count > 0 Then
                Me.gvListaAmbientesCursos.DataSource = dtAmb
                Me.gvListaAmbientesCursos.DataBind()
                'Para ocultar las columnas y no pierda el valor
                Call mt_ocultarColCursosAmbientes()

            Else
                Call mt_ShowMessage("No se encontraron ambientes para este curso", MessageType.warning)
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub

    Private Sub mt_cargarAmbientesParaCambiar()
        md_Horario = New d_Horario
        me_Horario = New e_Horario
        Dim dtAmbParaCambiar As New Data.DataTable

        Try
            With me_Horario

                .operacion = "GEN"
                .dia = Me.txtDiaTabEnv.Text
                .hora_ini = Me.txtHorIniTabEnv.Text
                .hora_fin = Me.txtHorFinTabEnv.Text
                .capacidad_actual = Me.txtCapacTabEnv.Text
                .capacidad_necesaria = Me.txtCapReqTabEnv.Text
                .nro_mat = Me.txtMatTabEnv.Text
                .codigo_cac = Me.ddlSemAca.SelectedValue

            End With

            dtAmbParaCambiar = md_Horario.Horario_ListarAmbientesParaCambiar(me_Horario)

            If dtAmbParaCambiar.Rows.Count > 0 Then
                Me.gvAmbParaCambiar.DataSource = dtAmbParaCambiar
                Me.gvAmbParaCambiar.DataBind()
            Else
                Call mt_ShowMessage("No se encontraron ambientes requeridos", MessageType.warning)
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub

    Private Sub mt_LimpiarTabEnvio()

        Me.gvAmbParaCambiar.DataSource = Nothing
        Me.gvAmbParaCambiar.DataBind()

    End Sub

    Private Sub mt_ocultarColCursosAmbientes()
        gvListaAmbientesCursos.Columns(0).Visible = False
        gvListaAmbientesCursos.Columns(1).Visible = False
        gvListaAmbientesCursos.Columns(11).Visible = False
        gvListaAmbientesCursos.Columns(12).Visible = False
    End Sub

    Private Sub mt_mostrarColCursosAmbientes()
        gvListaAmbientesCursos.Columns(0).Visible = True
        gvListaAmbientesCursos.Columns(1).Visible = True
        gvListaAmbientesCursos.Columns(11).Visible = True
        gvListaAmbientesCursos.Columns(12).Visible = True
    End Sub



#End Region


   
End Class

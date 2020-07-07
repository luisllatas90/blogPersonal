Imports System.Data

Partial Class Alumni_frmRegistrarEgresados
    Inherits System.Web.UI.Page

#Region "Variables de clase"
    Dim codigo_tfu, codigo_usu, codigo_test As String
    Dim md_Funciones As d_Funciones
    Dim md_CarreraProfesional As d_CarreraProfesional
    Dim md_PlanEstudio As d_PlanEstudio
    Dim md_Alumno As d_Alumno
    Dim md_RequisitoEgreso As d_RequisitoEgreso
    Dim md_cicloAcademico As d_CicloAcademico
    Dim md_alumnoRequisito As d_AlumnoRequisito

    'Endtidades
    Dim me_CarreraProfesional As e_CarreraProfesional
    Dim me_planEstudio As e_PlanEstudio
    Dim me_alumno As e_Alumno
    Dim me_RequisitoEgreso As e_RequisitoEgreso
    Dim me_cicloAcademico As e_CicloAcademico
    Dim me_alumnoRequisito As e_AlumnoRequisito

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

#End Region

#Region "Eventos"
    Protected Sub lbBusca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbBusca.Click
        If ddlCarrrera.SelectedValue = "" Or ddlPlanEst.SelectedValue = "" Then
            Me.udpFiltros.Update()
            Call mt_ShowMessage("Debe completar los datos", MessageType.warning)
            'Me.udpScripts.Update()
            'Me.udpListado.Update()
            Exit Sub
        Else
            Me.udpFiltros.Update()
            mt_CargarGrillaAlumnos()
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If

            codigo_tfu = Request.QueryString("ctf") '1
            codigo_usu = Request.QueryString("id") 'Session("id_per") -- 684'
            codigo_test = "2" 'Request.QueryString("mod")

            'codigo_tfu = "1" '1
            'codigo_usu = "684" 'Session("id_per") -- 684'
            'codigo_test = "2" 'Request.QueryString("mod")

            If IsPostBack = False Then
                Call mt_CargarComboProfesional()
                Call mt_CargarComboCicloAcademico()
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub ddlCarrrera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarrrera.SelectedIndexChanged
        Try
            'Me.txtPrueba.Text = ddlCarrrera.SelectedValue
            'Me.udpListado.Update()
            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "alert", "alert('Hola');", True)
            Me.udpFiltros.Update()
            Call mt_CargarComboPlan()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub ddlSemestre1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemestre1.SelectedIndexChanged
        Try
            Me.udpFiltros.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)
            mt_CargarCronograma()
            Me.udpListado.Update()

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub gvAlumnosReq_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAlumnosReq.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.TableSection = TableRowSection.TableHeader
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Cells(5).Text = IIf(fila.Row("estadoactual_alu") = 0, "Inactivo", "Activo")

            If e.Row.Cells(5).Text = "Activo" Then
                e.Row.Cells(5).Font.Bold = True
                '---- ****** para desactivar el check cuando el alumno no cumple con todos los requisitos
                'md_RequisitoEgreso = New d_RequisitoEgreso : me_RequisitoEgreso = New e_RequisitoEgreso

                'Dim dtReqAct As New Data.DataTable
                'me_alumno.codigo_alu = fila.Row("codigo_alu")
                'dtReqAct = md_RequisitoEgreso.ListarRequisitosByAlumno(me_alumno)

                'Dim cuentaReq As Integer = 0
                'For Each row As DataRow In dtReqAct.Rows
                '    If (row("cumplio").ToString() = "false") Then
                '        cuentaReq = cuentaReq + 1
                '    End If
                'Next
                'If cuentaReq > 0 Then
                '    e.Row.Cells(0).Enabled = False
                'End If
                '---- ****** fin
            Else
                e.Row.Cells(0).Enabled = False
                'e.Row.Cells(11).Enabled = False
                'e.Row.Cells(0).Visible = False
                e.Row.Cells(5).Font.Bold = False
                e.Row.Cells(5).ForeColor = System.Drawing.Color.Red
            End If

            CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")

        End If

    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            Me.udpScripts.Update()
            '--- valida que se marque uno
            Dim cuenta As Integer = 0
            If Me.gvAlumnosReq.Rows.Count > 0 Then
                For Each fila As GridViewRow In Me.gvAlumnosReq.Rows
                    If CType(fila.FindControl("chkElegir"), CheckBox).Checked Then
                        cuenta = +1
                    End If
                Next
            End If
            ''-----------------------------------
            If cuenta > 0 Then
                For Each filaI As GridViewRow In Me.gvAlumnosReq.Rows
                    If CType(filaI.FindControl("chkElegir"), CheckBox).Checked Then
                        md_Alumno = New d_Alumno : me_alumno = New e_Alumno

                        Dim dtInsert As New Data.DataTable
                        Dim dtInsAlumn As New Data.DataTable
                        With me_alumno
                            me_alumno.codigo_alu = Me.gvAlumnosReq.DataKeys(filaI.RowIndex).Item("codigo_alu").ToString
                            me_alumno.codigo_per = codigo_usu
                        End With
                        dtInsert = md_Alumno.FinalizarPlanEstudio(me_alumno)
                        dtInsert = md_Alumno.FinalizarPlanEstudioAlumni(me_alumno)
                        Me.udpFiltros.Update()
                        Call mt_ShowMessage("El registro se realizó con éxito", MessageType.success)
                        mt_CargarGrillaAlumnos()
                    End If
                Next
                'Call mt_ShowMessage("El registro se realizó con éxito", MessageType.error)
            Else
                Me.udpFiltros.Update()
                Call mt_ShowMessage("Debe marcar por lo menos un alumno", MessageType.error)
                Exit Sub
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try


    End Sub

    Protected Sub gvAlumnosReq_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAlumnosReq.RowCommand
        Try

            'Call mt_ShowMessage("¡Se actualizó el estado de los requisitos!", MessageType.success)

            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("codigo_alu") = Me.gvAlumnosReq.DataKeys(index).Values("codigo_alu")

            Select Case e.CommandName
                Case "Editar"
                    mt_cargarFormAlumno(Session("codigo_alu"))
                    'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('registro-tab');", True)
                    'Me.udpRegistro.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModal", "openModal();", True)
                    Me.udpModal.Update()
            End Select


        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Procedimientos"

    Private Sub mt_CargarComboProfesional()
        Try
            md_Funciones = New d_Funciones : md_CarreraProfesional = New d_CarreraProfesional : me_CarreraProfesional = New e_CarreraProfesional
            Dim dtCarreraProfesional As New Data.DataTable
            me_CarreraProfesional.codigo_per = codigo_usu
            me_CarreraProfesional.codigo_tfu = codigo_tfu
            me_CarreraProfesional.codigo_test = codigo_test
            dtCarreraProfesional = md_CarreraProfesional.ListarCarreraProfesionalByAcceso(me_CarreraProfesional)
            Call md_Funciones.CargarCombo(Me.ddlCarrrera, dtCarreraProfesional, "codigo_cpf", "nombre_cpf", True, "[-- SELECCIONE --]", "")

            dtCarreraProfesional.Dispose()
            'udpFiltro.Update()

        Catch ex As Exception
            Response.Write(ex)
        End Try
    End Sub
    Private Sub mt_CargarComboPlan()
        Try
            md_Funciones = New d_Funciones : md_PlanEstudio = New d_PlanEstudio : me_planEstudio = New e_PlanEstudio
            Dim dtPlanEstudio As New Data.DataTable
            me_planEstudio.codigo_cpf = Me.ddlCarrrera.SelectedValue
            'Me.txtPrueba.Text = me_planEstudio.codigo_cpf
            dtPlanEstudio = md_PlanEstudio.ListarPlanEstudioByCarrera(me_planEstudio)
            Call md_Funciones.CargarCombo(Me.ddlPlanEst, dtPlanEstudio, "codigo_Pes", "descripcion_Pes", True, "[-- SELECCIONE --]", "")
            Me.udpListado.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub mt_CargarGrillaAlumnos()
        me_alumno = New e_Alumno : md_Alumno = New d_Alumno : md_Funciones = New d_Funciones
        Try
            me_alumno.codigo_pes = Me.ddlPlanEst.SelectedValue
            me_alumno.tempcodigo_cpf = Me.ddlCarrrera.SelectedValue
            me_alumno.nombres = Me.txtAlumno.Text
            me_alumno.codigo_pcur = "0"

            Dim dtAlumo As New Data.DataTable
            dtAlumo = md_Alumno.ListarAlumnoReqEgreByPEstudioCprofe(me_alumno)
            Me.gvAlumnosReq.DataSource = dtAlumo
            Me.gvAlumnosReq.DataBind()
            'dtAlumo.Dispose()

            'Call md_Funciones.AgregarHearders(gvAlumnosReq)
            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)
            Me.udpListado.Update()


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub
    Private Sub mt_cargarFormAlumno(ByVal codigo_alu As String)
        Dim condicion As Integer
        Try
            md_Alumno = New d_Alumno : me_alumno = New e_Alumno : md_RequisitoEgreso = New d_RequisitoEgreso : me_RequisitoEgreso = New e_RequisitoEgreso
            With me_alumno
                .codigo_alu = codigo_alu
            End With
            Dim dtAlumno As New Data.DataTable
            Dim dtRequisito As New Data.DataTable

            dtAlumno = md_Alumno.ListarAlumnoReq(me_alumno)

            If dtAlumno.Rows.Count = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Exit Sub

            With dtAlumno.Rows(0)
                Me.txtCodUniAlum.Text = .Item("codigoUniver_Alu").ToString
                Me.txtNombres.Text = .Item("nombres").ToString
                Me.hdCodigo_alu.Value = me_alumno.codigo_alu.ToString
            End With
            dtRequisito = md_RequisitoEgreso.ListarRequisitosByAlumno(me_alumno)
            If dtRequisito.Rows.Count = 0 Then mt_ShowMessage("El Alumno no tiene requisitos configurados.", MessageType.warning) : Exit Sub
            'validar que haya cumplido todos los requisitos
            condicion = 0
            For Each row As DataRow In dtRequisito.Rows
                If (row("cumplio").ToString() = "false") Then
                    condicion = condicion + 1
                End If
            Next
            Me.HdCondicion.Value = condicion.ToString

            If condicion > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "validaReq", "validaReq(" & condicion & ");", True)
                'Me.btnGuardar.Enabled = False
            Else
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "NoValidaReq", "NoValidaReq();", True)
                'Me.btnGuardar.Enabled = True
            End If

            '-------------------------
            Me.gvRequisitos.DataSource = dtRequisito
            Me.gvRequisitos.DataBind()
            'gvRequisitos.Dispose()

        Catch ex As Exception
            'Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
            Response.Write(ex)
        End Try
    End Sub
    Private Sub mt_CargarComboCicloAcademico()
        Try
            md_Funciones = New d_Funciones : md_CicloAcademico = New d_CicloAcademico : me_CicloAcademico = New e_CicloAcademico
            Dim dt As New Data.DataTable
            dt = md_CicloAcademico.ObtenerCicloAcademico

            Call md_Funciones.CargarCombo(Me.ddlSemestre1, dt, "codigo_cac", "descripcion_cac", True, "[-- SELECCIONE --]", "")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_CargarCronograma()
        Try
            Dim objcnx As New ClsConectarDatos
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()
            Dim TablaCronograma As Data.DataTable
            TablaCronograma = objcnx.TraerDataTable("ConsultarCicloAcademico", "EGR", codigo_test)
            objcnx.CerrarConexion()
            Me.lblAsigna.Text = "No establecido"
            Dim y As Integer = 0
            For x As Integer = 0 To TablaCronograma.Rows.Count - 1
                If TablaCronograma.Rows(x).Item("descripcion_cac").ToString = Me.ddlSemestre1.SelectedItem.Text Then
                    Me.lblAsigna.Text = IIf(TablaCronograma.Rows(x).Item("fechaini_cro").ToString <> "", TablaCronograma.Rows(x).Item("fechaini_cro").ToString.Substring(0, 10), "") & " al " & IIf(TablaCronograma.Rows(x).Item("fechafin_cro").ToString <> "", TablaCronograma.Rows(x).Item("fechafin_cro").ToString.Substring(0, 10), "")
                    y = x
                    Exit For
                End If
            Next
            If Me.lblAsigna.Text = " al " Then
                Me.lblAsigna.Text = "No establecido"
                Me.cmdGuardar.Enabled = False
            Else
                If Date.Now >= CDate(TablaCronograma.Rows(y).Item("fechaini_cro")) And Date.Now <= CDate(TablaCronograma.Rows(y).Item("fechafin_cro")) Then
                    Me.cmdGuardar.Visible = True
                    Me.cmdGuardar.Enabled = True
                Else
                    Me.cmdGuardar.Visible = True
                    Me.cmdGuardar.Enabled = False
                End If
            End If

            'MTesen indicó que para Coordinacion academica y Dirección académica debe tener activo siempre el boton
            If (Request.QueryString("ctf") = 85 Or Request.QueryString("ctf") = 181) Then
                Me.cmdGuardar.Enabled = True
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
        Me.udpListado.Update()
    End Sub

#End Region
    
    
End Class

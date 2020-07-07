
Partial Class administrativo_pec_frmProgramacionAcademicaPRE
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim tbl As Data.DataTable
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")
            '=================================
            'Permisos por Escuela
            '=================================
            Dim obj As New ClsConectarDatos
            Dim objfun As New ClsFunciones
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            objfun.CargarListas(cboCecos, obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), "", Request.QueryString("mod")), "codigo_Cco", "Nombre", ">> Seleccione<<")
            '=================================
            'Llenar combos
            '=================================
            objfun.CargarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0), "codigo_cac", "descripcion_cac")
            objfun.CargarListas(Me.dpCodigo_per, obj.TraerDataTable("ConsultarDocente", "ACT", 0, 0), "codigo_per", "docente", "--Seleccione el Profesor--")

            obj.CerrarConexion()
            obj = Nothing
            objfun = Nothing
            Me.txtInicio.Attributes.Add("OnKeyDown", "return false")
            Me.txtFin.Attributes.Add("OnKeyDown", "return false")
            Me.txtRetiro.Attributes.Add("OnKeyDown", "return false")
            Me.txtVacantes.Attributes.Add("onKeyPress", "validarnumero()")

            If Request.QueryString("Accion") = "M" Then

                dpCodigo_cac.SelectedValue = Request.QueryString("codigo_cac")
                cboCecos.SelectedValue = Request.QueryString("codigo_cco")
                cboCecos_SelectedIndexChanged(sender, e)
                dpCodigo_pes.SelectedValue = Request.QueryString("codigo_pes")
                MostrarDatos()

            End If
        End If
    End Sub
    Private Sub CargarDetalleGrupoHorario(ByVal tipo As String, ByVal ID As String, ByVal accion As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tblcurso As Data.DataTable
        obj.AbrirConexion()
        tblcurso = obj.TraerDataTable("CAR_ConsultarProcesoProgramacionAcademica", tipo, Me.dpCodigo_pes.SelectedValue, ID, Me.dpCodigo_cac.SelectedValue, 0)

        If tblcurso.Rows.Count > 0 Then
            Me.lblFecha.Text = tblcurso.Rows(0).Item("fechadoc_cup")
            Me.txtInicio.Text = tblcurso.Rows(0).Item("fechainicio_cup")
            Me.txtFin.Text = tblcurso.Rows(0).Item("fechafin_cup")
            Me.txtRetiro.Text = tblcurso.Rows(0).Item("fecharetiro_cup")
            Me.txtVacantes.Text = tblcurso.Rows(0).Item("vacantes_cup")
            Me.dpestado_cup.SelectedValue = tblcurso.Rows(0).Item("estado_cup")
            Me.lblOperador.Text = tblcurso.Rows(0).Item("login_per")
            Me.txtGrupoHor_cup.Text = tblcurso.Rows(0).Item("grupohor_cup")

            '================================================================
            If accion = "e" Then
                If tblcurso.Rows(0).Item("Nivelacion_cup") = True Then
                    chkNivelacion.Checked = True
                Else
                    chkNivelacion.Checked = False
                End If
            Else
                chkNivelacion.Checked = False
            End If
            '================================================================

        End If
        Me.hdcodigo_cup.Value = ID
        tblcurso.Dispose()

        Me.grwProfesores.DataBind()
        Me.fraDetalleGrupoHorario.Visible = True
        Me.fraProfesores.Visible = True

        Me.dpestado_cup.Enabled = tipo = 3
        Me.lblgrupohor_cup.Visible = tipo = 3
        'Me.txtGrupoHor_cup.Visible = tipo = 3
        Me.txtGrupoHor_cup.Visible = True
        cmdGuardar.Text = IIf(tipo = 3, "Actualizar", "Guardar")
        cmdGuardar.Enabled = True

        'Mostrar cursos disponibles para PROGRAMAR
        If ID = 0 Then
            Me.dpCurso.Visible = True
            Me.lblnombre_cur.Visible = False
            Me.cmdAsignarProfesor.Visible = False
        Else
            'Mostrar alumnos Matriculados
            Me.cmdAsignarProfesor.Visible = True
            Me.dpCodigo_per.Visible = True
            Me.lblnombre_cur.Visible = True
            Me.dpCurso.Visible = False

            'Mostrar Profesores Asignados
            'Me.grwProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 4, Me.hdcodigo_cup.Value, 0, 0, 0)
            Me.grwProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademicaPRE", Me.hdcodigo_cup.Value)
            Me.grwProfesores.DataBind()


            'Cargar Horario del curso 'yperez
            ' Me.gridHorario.DataSource = obj.TraerDataTable("HorarioPE_Consultar", Me.hdcodigo_cup.Value)
            ' Me.gridHorario.DataBind()

        End If
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim Fila As GridViewRow
        Dim swEditar As Boolean
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim codigo_usu As Integer = Request.QueryString("id")
        Dim valoresdevueltos(1) As String

        Me.cmdGuardar.Enabled = False
        '==================================
        'Grabar Programación y Matricula
        '==================================
        Try
            swEditar = False
            obj.AbrirConexion()
            If Me.cmdGuardar.Text = "Guardar" Then

                If (Me.txtVacantes.Text = "" Or Me.dpCodigo_per.SelectedValue = -1 Or Me.dpCurso.SelectedValue = -1) Then
                    RegisterStartupScript("Error", "<script>alert('Complete los datos correctamente de Curso,Vacantes y Profesor')</script>")
                    Me.cmdGuardar.Enabled = True
                    Exit Sub
                End If

                '==================================
                'Programar el Curso
                '================================== 
                'Response.Write(" CAC=" & Me.dpCodigo_cac.SelectedValue & ", PES= " & Me.dpCodigo_pes.SelectedValue & ", CUP= " & Me.dpCurso.SelectedValue & ", VACANTES= " & Me.txtVacantes.Text & "," & "C" & ", INICIO" & Me.txtInicio.Text & ", FIN= " & Me.txtFin.Text & ", RETIRO= " & Me.txtRetiro.Text & ", USU=" & codigo_usu & ", CECO= " & Me.cboCecos.SelectedValue)

                '=====================
                'Antes esta Funcionando esta linea 
                'obj.Ejecutar("EVE_AgregarCursoProgramado_Individual", 0, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.dpCurso.SelectedValue, DBNull.Value, Me.txtVacantes.Text, "C", CDate(Me.txtInicio.Text), CDate(Me.txtFin.Text), CDate(Me.txtRetiro.Text), codigo_usu, "", 0, Me.cboCecos.SelectedValue, 0).copyto(valoresdevueltos, 0)

                '===============================================================================================================================================================================================================================================================================================================================================================================================================
                'Se replico el procedure para evitar errores debido a que se le envia un parametro mas
                'Campo Nivelacion =1
                obj.Ejecutar("EVE_AgregarCursoProgramado_Individual_V1", 0, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.dpCurso.SelectedValue, DBNull.Value, Me.txtVacantes.Text, "C", CDate(Me.txtInicio.Text), CDate(Me.txtFin.Text), CDate(Me.txtRetiro.Text), codigo_usu, "", 0, Me.cboCecos.SelectedValue, IIf(chkNivelacion.Checked = True, 1, 0), 0).copyto(valoresdevueltos, 0)
                '===============================================================================================================================================================================================================================================================================================================================================================================================================

                'Response.Write(">>><>>>PROBANDO")
                If (valoresdevueltos(0) <> "0" And valoresdevueltos(0) <> "") Then
                    '==================================
                    'Asignar Carga Académica
                    '==================================

                    'Response.Write(valoresdevueltos(0))

                    obj.Ejecutar("CAR_AgregarCargaAcademicaPRE", Me.dpCodigo_per.SelectedValue, valoresdevueltos(0), codigo_usu, Request.QueryString("mod"), System.DBNull.Value)
                Else
                    Exit Sub
                End If

            ElseIf (Me.txtVacantes.Text = "" Or Me.grwProfesores.Rows.Count = 0) Then
                RegisterStartupScript("Error", "<script>alert('Complete los datos correctamente de Vacantes y Profesor')</script>")
                Me.cmdGuardar.Enabled = True
                Exit Sub
            Else
                '==================================
                'Modificar curso programado
                '==================================
                'obj.Ejecutar("ModificarCursoProgramado", Me.hdcodigo_cup.Value, Me.txtGrupoHor_cup.Text.Trim, Me.dpestado_cup.SelectedValue, Me.txtVacantes.Text, CDate(Me.txtInicio.Text), CDate(Me.txtFin.Text), CDate(Me.txtRetiro.Text), codigo_usu, " ", Me.dpCodigo_pes.SelectedValue, 0, 0) '.copyto(valoresdevueltos, 0)
                '========================
                'Se replico el procedure para evitar errores en otras paginas
                'Se envia otro parametro: nivelacion_cup
                'xDguevara 27.11.2012
                obj.Ejecutar("ModificarCursoProgramado_V2", Me.hdcodigo_cup.Value, Me.txtGrupoHor_cup.Text.Trim, Me.dpestado_cup.SelectedValue, Me.txtVacantes.Text, CDate(Me.txtInicio.Text), CDate(Me.txtFin.Text), CDate(Me.txtRetiro.Text), codigo_usu, " ", Me.dpCodigo_pes.SelectedValue, IIf(chkNivelacion.Checked = True, 1, 0), 0, 0) '.copyto(valoresdevueltos, 0)
                swEditar = True


            End If
            '==================================
            'Actualizar Controles
            '==================================

            grwGruposProgramados.DataBind()
            Me.grwGruposProgramados.DataSource = obj.TraerDataTable("ConsultarDatosProgramaEspecial", 7, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, 0, 0)
            Me.grwGruposProgramados.DataBind()

            obj.CerrarConexion()
            obj = Nothing
            If swEditar = True Then
                Dim objCnx As New ClsConectarDatos
                objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString
                objCnx.AbrirConexion()
                objCnx.Ejecutar("AUV_ActualizarFechasCursoVirtualesPorCurso", hdcodigo_cup.Value)
                objCnx.CerrarConexion()
            End If

            Me.LimpiarDatos(True)
            'ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, "string".GetType(), "Programar", "alert('" & valoresdevueltos(0) & "');", True)

            'Aqui vamos a llamar al consultar. 05.08.2013 xDguevara para mostrar los cambios.
            Me.MostrarDatos()
        Catch ex As Exception
            obj = Nothing
            hdcodigo_cup.Value = 0
            'ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, "string".GetType(), "Programar", "alert('Ocurrió un Error interno en el sistema\nConctáctese con desarrollosistemas@usat.edu.pe');", True)
        End Try
    End Sub
    Private Sub LimpiarDatos(ByVal estado As Boolean)
        Me.fraDetalleGrupoHorario.Visible = False
        Me.fraProfesores.Visible = False
        Me.fraGruposProgramados.Visible = estado
        Me.grwProfesores.DataBind()
        Me.dpCurso.SelectedValue = -1
        Me.dpCodigo_per.SelectedValue = -1
        Me.hdcodigo_cup.Value = 0

        'Agregado xDguevara 27.11.2012
        Me.chkNivelacion.Checked = False
    End Sub
    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        Me.cmdAgregar.Enabled = False
        Me.fraGruposProgramados.Visible = False
        CargarDetalleGrupoHorario("7", 0, "n")
        Me.cmdAgregar.Enabled = True
        chkNivelacion.visible = True
        lblNiv.visible = True
        '===================================
        'Response.Write(dpCodigo_cac.SelectedValue)
        'Response.Write("<br />")
        'Response.Write(cboCecos.SelectedValue)
        'Response.Write("<br />")

    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        LimpiarDatos(True)
    End Sub
    Protected Sub grwGruposProgramados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwGruposProgramados.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            Me.lblGrupos.Text = "Lista de Grupos Horario Programados (" & e.Row.RowIndex + 1 & ")"
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")


            e.Row.Cells(7).Text = IIf(fila.Row("estado_cup") = False, "Cerrado", "Abierto")
            If CBool(fila.Item("nivelacion_cup")) = True Then
                e.Row.ForeColor = Drawing.Color.IndianRed
            End If

            '================================================
            'Cargar Profesores y Horario
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim gr As BulletedList = CType(e.Row.FindControl("lstProfesores"), BulletedList)
            'gr.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 4, fila.Row("codigo_cup"), Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, 0)
            gr.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademicaPRE", fila.Row("codigo_cup"))
            gr.DataBind()
            obj.CerrarConexion()
            obj = Nothing
            '================================================
        End If
    End Sub
    Protected Sub grwGruposProgramados_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwGruposProgramados.RowDeleting
        Dim obj As New ClsConectarDatos
        Dim valoresdevueltos(1) As String
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("EliminarCursoProgramado", grwGruposProgramados.DataKeys(e.RowIndex).Values("codigo_cup"), 0).copyto(valoresdevueltos, 0)
        'Cargar los Cursos Programados

        Me.grwGruposProgramados.DataSource = Nothing
        Me.grwGruposProgramados.DataBind()

        Me.grwGruposProgramados.DataSource = obj.TraerDataTable("ConsultarDatosProgramaEspecial", 7, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, 0, 0)
        Me.grwGruposProgramados.DataBind()


        obj.CerrarConexion()
        obj = Nothing
        e.Cancel = True
        Me.lblGrupos.Text = "Lista de Grupos Horario Programados (" & Me.grwGruposProgramados.Rows.Count & ")"
        ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, "string".GetType(), "EliminarProgramacion", "alert('" & valoresdevueltos(0) & "');", True)
    End Sub
    Protected Sub cmdAsignarProfesor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAsignarProfesor.Click
        If dpCodigo_per.SelectedValue <> -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("CAR_AgregarCargaAcademicaPRE", Me.dpCodigo_per.SelectedValue, Me.hdcodigo_cup.Value, Request.QueryString("id"), Request.QueryString("mod"), System.DBNull.Value)
            obj.CerrarConexion()
            obj = Nothing
            Me.CargarDetalleGrupoHorario(3, Me.hdcodigo_cup.Value, "p")
        End If
    End Sub
    Protected Sub grwProfesores_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwProfesores.RowDeleting
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'obj.Ejecutar("EliminarCargaAcademica", grwProfesores.DataKeys(e.RowIndex).Values("codigo_per"), Me.hdcodigo_cup.Value, Request.QueryString("id"))
        obj.Ejecutar("ACAD_EliminarCargaAcademicaPRE", grwProfesores.DataKeys(e.RowIndex).Values("codigo_per"), Me.hdcodigo_cup.Value, Request.QueryString("id"))
        'Cargar denuevo
        'Me.grwProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 4, Me.hdcodigo_cup.Value, 0, 0, 0)
        Me.grwProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademicaPRE", Me.hdcodigo_cup.Value)
        Me.grwProfesores.DataBind()
        obj.CerrarConexion()
        obj = Nothing
        e.Cancel = True
    End Sub
    Protected Sub grwProfesores_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwProfesores.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(0).Text = "<a target='_blank' href='../../academico/horarios/vsthorariodocente.asp?modo=A&codigo_per=" & fila.Row("codigo_per") & "&codigo_Cac=" & Me.dpCodigo_cac.SelectedValue & "'>" & fila.Row("docente") & "</a>"
        End If
    End Sub

    Private Sub MostrarDatos()
        Try

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            '=================================
            'Cargar Grupos Programados
            '=================================

            Me.grwGruposProgramados.DataBind()
            'Me.grwGruposProgramados.DataSource = obj.TraerDataTable("EVE_ConsultarProgramacionAcademica", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.cboCecos.SelectedValue)
            '<<dguevara 01.08.2013 se agregaron los parametros>>
            Me.grwGruposProgramados.DataSource = obj.TraerDataTable("EVE_ConsultarProgramacionAcademica", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.cboCecos.SelectedValue, ddlFiltro.SelectedValue, txtBusqueda.Text.Trim.ToString)
            Me.grwGruposProgramados.DataBind()

            'Cargar los cursos del Plan de Estudios
            ClsFunciones.LlenarListas(Me.dpCurso, obj.TraerDataTable("ConsultarCursoProgramado", 1, Me.dpCodigo_pes.SelectedValue, Me.dpCodigo_cac.SelectedValue, "", ""), "codigo_cur", "nombre_cur", "--Seleccione el curso--")


            If Me.grwGruposProgramados.Rows.Count = 0 Then
                Me.lblGrupos.Text = "Programar Nuevos Grupos Horario"
            Else
                Me.lblGrupos.Text = "Lista de Grupos Horario Programados"
            End If
            Me.fraGruposProgramados.Visible = True



            obj.CerrarConexion()
            obj = Nothing

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function validaciones() As Boolean
        'Function agregada xdguevara 31.07.2013
        Dim sw As Byte = 0

        If cboCecos.SelectedValue = -1 Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de seleccionar el Centro de Costos  a consultar.');", True)
            cboCecos.Focus()
            Exit Function
        End If

        sw = 1
        If (sw = 1) Then
            Return True
        End If
        Return False

    End Function

    Protected Sub cmdVer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVer.Click

        'Realizar la validacion antes de iniciar la busqueda, debido a que estaba mostrando un error.
        'xDuevara 31.07.2013
        If validaciones() = False Then
            Exit Sub
        End If

        LimpiarDatos(False)
        If Me.dpCodigo_pes.SelectedValue <> -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            '=================================
            'Cargar Grupos Programados
            '=================================

            Me.grwGruposProgramados.DataBind()
            'Me.grwGruposProgramados.DataSource = obj.TraerDataTable("EVE_ConsultarProgramacionAcademica", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.cboCecos.SelectedValue)
            Me.grwGruposProgramados.DataSource = obj.TraerDataTable("EVE_ConsultarProgramacionAcademica", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.cboCecos.SelectedValue, ddlFiltro.SelectedValue, txtBusqueda.Text.Trim.ToString)
            Me.grwGruposProgramados.DataBind()

            'Response.Write("Codigo_cac = " & Me.dpCodigo_cac.SelectedValue)
            'Response.Write("Codigo_pes = " & Me.dpCodigo_pes.SelectedValue)
            'Response.Write("Centro Costos=" & Me.cboCecos.SelectedValue)

            'Cargar los cursos del Plan de Estudios
            ClsFunciones.LlenarListas(Me.dpCurso, obj.TraerDataTable("ConsultarCursoProgramado", 1, Me.dpCodigo_pes.SelectedValue, Me.dpCodigo_cac.SelectedValue, "", ""), "codigo_cur", "nombre_cur", "--Seleccione el curso--")

            obj.CerrarConexion()
            obj = Nothing

            If Me.grwGruposProgramados.Rows.Count = 0 Then
                Me.lblGrupos.Text = "Programar Nuevos Grupos Horario"
            Else
                Me.lblGrupos.Text = "Lista de Grupos Horario Programados"
            End If
            Me.fraGruposProgramados.Visible = True
        End If
    End Sub
    Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCecos.SelectedIndexChanged

        'Response.Write("ceco:" + cboCecos.SelectedValue)

        Me.dpCodigo_pes.Items.Clear()
        Me.grwGruposProgramados.Dispose()
        LimpiarDatos(False)
        Me.cmdVer.Visible = False
        If cboCecos.SelectedValue <> -1 Then
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")

            Dim obj As New ClsConectarDatos
            Dim objfun As New ClsFunciones
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            '=================================
            'Llenar combos
            '=================================
            objfun.CargarListas(Me.dpCodigo_pes, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 10, 1, Me.cboCecos.SelectedValue, 0), "codigo_pes", "descripcion_pes")
            objfun = Nothing
            obj.CerrarConexion()
            obj = Nothing
            Me.cmdVer.Visible = Me.dpCodigo_pes.Items.Count > 0
        End If
    End Sub


    Protected Sub ibtnInvitar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        'Response.Write("btn hfCodigo_cup:-> " & hfCodigo_cup.Value)
        'ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "javascript:location.href='frmListaAlumnosProfesionalizacion.aspx?id=" & Me.Request.QueryString("id") & "&codigo_cup=" & hfCodigo_cup.Value & "&Codigo_cac=" & dpCodigo_cac.SelectedValue & "&Codigo_pes=" & dpCodigo_pes.SelectedValue & "&Codigo_cco=" & cboCecos.SelectedValue & "&ctf=" & Request.QueryString("ctf") & "&mod=" & Request.QueryString("mod") & "&Accion=" & "L" & "';", True)
        'ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmListaAlumnosProfesionalizacion.aspx?id=" & Me.Request.QueryString("id") & "&codigo_cup=" & hfCodigo_cup.Value & "&Codigo_cac=" & dpCodigo_cac.SelectedValue & "&Codigo_pes=" & dpCodigo_pes.SelectedValue & "&Codigo_cco=" & cboCecos.SelectedValue & "&ctf=" & Request.QueryString("ctf") & "&mod=" & Request.QueryString("mod") & "&Accion=" & "L" & "';", True)
        'ibtnInvitar.OnClientClick = "AbrirPopUp('frmListaAlumnosProfesionalizacion.aspx?id=" & Me.Request.QueryString("id") & "&codigo_cup=" & hfCodigo_cup.Value & "&Codigo_cac=" & dpCodigo_cac.SelectedValue & "&Codigo_pes=" & dpCodigo_pes.SelectedValue & "&Codigo_cco=" & cboCecos.SelectedValue & "&ctf=" & Request.QueryString("ctf") & "&mod=" & Request.QueryString("mod") & "&Accion=" & "L" & " ','680','1100','yes','yes','yes','yes')"
        '==========================
        Try
            Dim strURL As String
            Dim ScriptString As String
            Dim ibtnInvitar As ImageButton
            Dim row As GridViewRow
            Dim hfCodigo_cup As HiddenField


            ibtnInvitar = sender
            row = ibtnInvitar.NamingContainer
            hfCodigo_cup = row.FindControl("hfcodigo_cup")

            'Sesiones:
            Session("s_id") = Me.Request.QueryString("id")
            Session("s_codigo_cup") = hfCodigo_cup.Value
            Session("s_codigo_cac") = dpCodigo_cac.SelectedValue
            Session("s_codigo_pes") = dpCodigo_pes.SelectedValue
            Session("s_cboCecos") = cboCecos.SelectedValue
            Session("s_ctf") = Request.QueryString("ctf")
            Session("s_mod") = Request.QueryString("mod")
            Session("s_accion") = "L"


            'dguevara -02.08.2013-
            'Temporalmente voy a enviar los parametros asi, luego los cambio por sessiones para mejorar la seguridad
            'strURL = "frmListaAlumnosProfesionalizacion.aspx?id=" & Me.Request.QueryString("id") & "&codigo_cup=" & hfCodigo_cup.Value & "&Codigo_cac=" & dpCodigo_cac.SelectedValue & "&Codigo_pes=" & dpCodigo_pes.SelectedValue & "&Codigo_cco=" & cboCecos.SelectedValue & "&ctf=" & Request.QueryString("ctf") & "&mod=" & Request.QueryString("mod") & "&Accion=" & "L" & ""
            strURL = "frmListaAlumnosProfesionalizacion.aspx"
            ScriptString = "<script language='javascript'>"
            ScriptString += "window.open('" & strURL & "', '_blank','height=750,width=1280,top=50,left=50,resizable=yes,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes');"
            ScriptString += "</script>"
            ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", ScriptString)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub grwGruposProgramados_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwGruposProgramados.RowCommand
        Me.hdcodigo_cup.Value = 0
        If (e.CommandName = "editar") Then

            chkNivelacion.visible = False       'Agregado xDguevara
            lblNiv.visible = False

            Me.fraGruposProgramados.Visible = False
            Me.lblnombre_cur.Text = Me.grwGruposProgramados.Rows(Convert.ToInt32(e.CommandArgument).ToString).Cells(0).Text
            Me.hdcodigo_cup.Value = Convert.ToString(Me.grwGruposProgramados.DataKeys(Convert.ToInt32(e.CommandArgument)).Value)
            Me.CargarDetalleGrupoHorario(3, Me.hdcodigo_cup.Value, "e")
        End If

    End Sub

    Protected Sub ddlFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFiltro.SelectedIndexChanged
        Try
            Me.MostrarDatos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dpCodigo_pes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpCodigo_pes.SelectedIndexChanged
        Try
            Me.MostrarDatos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub cmdRegistrarHorario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegistrarHorario.Click
        Session("h_id") = Me.Request.QueryString("id")
        Session("h_codigo_cup") = Me.hdcodigo_cup.Value
        Me.cmdRegistrarHorario.OnClientClick = "ejecutar();"
        'Response.Redirect("frmProgramarHorarios.aspx")
    End Sub

    Protected Sub gridHorario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridHorario.RowDataBound
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tb As New Data.DataTable
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddlAmb As DropDownList
            ddlAmb = e.Row.FindControl("ddlAmbiente")
            If gridHorario.DataKeys(e.Row.RowIndex).Values("codigo_lho").ToString <> 0 Then
                obj.AbrirConexion()
                tb = obj.TraerDataTable("HorarioPE_ConsultarAsignacionAmbiente", gridHorario.DataKeys(e.Row.RowIndex).Values("codigo_lho"), dpCodigo_pes.SelectedValue, dpCodigo_cac.SelectedValue)
                obj.CerrarConexion()
                If tb.Rows.Count Then
                    For i As Integer = 0 To tb.Rows.Count - 1
                        ddlAmb.Items.Add(New ListItem(tb.Rows(i).Item("ambiente"), tb.Rows(i).Item("codigo_amb")))
                    Next

                    If gridHorario.DataKeys(e.Row.RowIndex).Values("codigo_amb").ToString = 0 Then
                        ddlAmb.Items.Add(New ListItem("--Seleccionar--", -1))
                        ddlAmb.SelectedValue = -1
                    Else
                        ddlAmb.SelectedValue = gridHorario.DataKeys(e.Row.RowIndex).Values("codigo_amb")
                    End If
                Else
                    ddlAmb.Items.Add(New ListItem("No asignaron ambientes", 0))
                End If

            End If
        End If
        obj = Nothing
        tb.Dispose()
    End Sub

    Protected Sub cmdGuardarAmbiente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarAmbiente.Click
        Dim Fila As GridViewRow
        Dim codigo_aam As Integer = 0
        Dim codigo_lho As Integer = 0
        Dim obj As New ClsConectarDatos
        Dim msj As String = "Se asignaron los ambientes: "
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        For i As Integer = 0 To Me.gridHorario.Rows.Count - 1
            Fila = Me.gridHorario.Rows(i)
            'If gridHorario.DataKeys(i).Values("codigo_amb").ToString <> "0" Then
            codigo_lho = gridHorario.DataKeys(i).Values("codigo_lho").ToString
            codigo_aam = CType(Fila.FindControl("ddlAmbiente"), DropDownList).SelectedValue
            obj.AbrirConexion()
            obj.Ejecutar("HorarioPE_RegistrarAmbiente", codigo_lho, codigo_aam)
            msj = msj & CType(Fila.FindControl("ddlAmbiente"), DropDownList).SelectedItem.Text & ","
            'End If            
            'verificar cuando se tiene mas de un horario,
            ' no registrar si amb=0 o si ddlambiente es 0
        Next
        msj = msj.Substring(0, msj.Length - 1)
        obj.AbrirConexion()
        Me.gridHorario.DataSource = obj.TraerDataTable("HorarioPE_Consultar", Me.hdcodigo_cup.Value)
        obj.CerrarConexion()
        Me.gridHorario.DataBind()
        lblMsjHorarioAmbiente.Text = msj
        obj = Nothing
    End Sub

End Class

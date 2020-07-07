﻿
Partial Class frmprogramacioncademicaeve
    Inherits System.Web.UI.Page

    Public Errores As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tbl As Data.DataTable
        Dim codigo_tfu As Int16 = Request.QueryString("ctf")
        Dim codigo_usu As Integer = Request.QueryString("id")

        If Not IsPostBack Then

            Me.lbldia.Text = ""
            '=================================
            'Permisos por Escuela
            '=================================
            Dim obj As New ClsConectarDatos
            Dim objfun As New ClsFunciones
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            'objfun.CargarListas(cboCecos, obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos_Xtest", Request.QueryString("ctf"), Request.QueryString("id"), "", Request.QueryString("mod")), "codigo_Cco", "Nombre", ">> Seleccione<<")
            objfun.CargarListas(cboCecos, obj.TraerDataTable("EVE_BuscaCcoId", Request.QueryString("id"), Request.QueryString("ctf"), Request.QueryString("mod"), "N"), "codigo_Cco", "descripcion_Cco", ">> Seleccione<<")

            '=================================
            'Llenar combos
            '=================================
            objfun.CargarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0), "codigo_cac", "descripcion_cac")
            objfun.CargarListas(Me.dpCodigo_per, obj.TraerDataTable("ConsultarDocente", "ACT", 0, 0), "codigo_per", "docente", "--Seleccione el Profesor--")
            ClsFunciones.LlenarListas(Me.ddlTipoAmbiente, obj.TraerDataTable("AsignarAmbiente_ListarAmbientes"), "codigo_tam", "descripcion_Tam", "<<TODOS>>")
           
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
            ' Cargarhorario()

            '=>> Horarios
            consultarTipos()

            CargarFechas()
            chkVarias_CheckedChanged(sender, e)
            CargarDia()

            Me.ddlCap.DataSource = obj.TraerDataTable("HorarioPE_ConsultarCapacidad")
            Me.ddlCap.DataTextField = "capacidad_amb"
            Me.ddlCap.DataValueField = "capacidad_amb"
            Me.ddlCap.DataBind()
            obj.CerrarConexion()
            obj = Nothing
            CargarDatos()
            cargarDiaVariasSesiones()
            cargarFechaLimite()
        End If

        'If Request.QueryString("mod") = 3 Then
        '    Me.ddlInicioDia.AutoPostBack = True
        '    Me.ddlInicioMes.AutoPostBack = True
        '    Me.ddlInicioAño.AutoPostBack = True
        'Else
        '    Me.ddlInicioDia.AutoPostBack = False
        '    Me.ddlInicioMes.AutoPostBack = False
        '    Me.ddlInicioAño.AutoPostBack = False
        'End If               
    End Sub
    Sub cargarFechaLimite()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.lblLimite.Text = "Fecha Límite: " & obj.TraerDataTable("HORARIOPE_ConsultarAmbienteConfig", 1).Rows(0)(1).ToString
        obj.CerrarConexion()

        If Request.QueryString("mod") = 5 Then
            Me.lblLimite.Visible = False
        End If
    End Sub
    Sub cargarDiaVariasSesiones()
        Dim nombre_hor As String = ""
        Dim horaFin As String = ""
        Dim fechaInicio As New Date
        nombre_hor = Me.ddlInicioHora.SelectedItem.Text & ":" & Me.ddlInicioMinuto.SelectedItem.Text
        horaFin = Me.ddlFinHora.SelectedItem.Text & ":" & Me.ddlFinMinuto.SelectedItem.Text
        'fechaInicio = New Date(CInt(Me.ddlInicioAño.SelectedValue), CInt(Me.ddlInicioMes.SelectedValue), CInt(Me.ddlInicioDia.SelectedValue), CInt(nombre_hor.Substring(0, 2)), CInt(nombre_hor.Substring(3, 2)), 0)
        fechaInicio = New Date(CInt(DatePart(DateInterval.Year, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Month, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Day, CDate(txtDesde.Value))), CInt(nombre_hor.Substring(0, 2)), CInt(nombre_hor.Substring(3, 2)), 0)
        Me.ddlDiaSelPer.SelectedValue = Replace((WeekdayName(Weekday(fechaInicio))).Substring(0, 2).ToUpper, "Á", "A")
        txtHasta.Value = DateAdd(DateInterval.Day, 15, CDate(txtDesde.Value))
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
                objCorreo.EnviarMailAd("campusvirtual@usat.edu.pe", tbCorreo.Rows(0).Item("firma"), tbCorreo.Rows(0).Item("EnviarA"), tbCorreo.Rows(0).Item("SubjectA") & " - Módulo de Solicitud de Ambientes", bodycorreo, True, tbCorreo.Rows(0).Item("cc"))
                Return True
            Catch ex As Exception
                Response.Write("<script>alert('" & ex.Message & "')</script>")
            End Try
        Else
            Response.Write("<script>alert('" & tbCorreo.Rows.Count & "')</script>")
        End If
    End Function

    Private Sub CargarDetalleGrupoHorario(ByVal tipo As String, ByVal ID As String, ByVal accion As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tblcurso As Data.DataTable
        obj.AbrirConexion()
        tblcurso = obj.TraerDataTable("CAR_ConsultarProcesoProgramacionAcademica", tipo, Me.dpCodigo_pes.SelectedValue, ID, Me.dpCodigo_cac.SelectedValue, 0)

        If tblcurso.Rows.Count > 0 Then
            Me.lblFecha.Text = tblcurso.Rows(0).Item("fechadoc_cup").ToString.Substring(0, 10)
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
        Me.fraDetalleAmbiente.Visible = True

        Me.dpestado_cup.Enabled = tipo = 3
        Me.lblgrupohor_cup.Visible = tipo = 3
        Me.txtGrupoHor_cup.Visible = tipo = 3
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
            'CambioGo
            'Me.grwProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 4, Me.hdcodigo_cup.Value, 0, 0, 0)
            Me.grwProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 11, Me.hdcodigo_cup.Value, 0, 0, 0)
            Me.grwProfesores.DataBind()

        End If
        obj.CerrarConexion()
        obj = Nothing
        divHorReg.Visible = False
        Cargarhorario()
        If ID = 0 Then
            Me.gridHorario.DataSource = Nothing
            Me.gridHorario.DataBind()
        End If

    End Sub

    Sub Cargarhorario()
        'Cargar Horario del curso 'yperez
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.gridHorario.DataSource = obj.TraerDataTable("HorarioPE_ConsultarSolxCup", Me.hdcodigo_cup.Value, CInt(Me.CheckBox1.Checked)) ')
        Me.gridHorario.DataBind()
        obj.CerrarConexion()
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
                'X Campo Nivelacion =1  
                ' Campo Curso Faltante Desaprobado
                obj.Ejecutar("EVE_AgregarCursoProgramado_Individual_V2", 0, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.dpCurso.SelectedValue, DBNull.Value, Me.txtVacantes.Text, "C", CDate(Me.txtInicio.Text), CDate(Me.txtFin.Text), CDate(Me.txtRetiro.Text), codigo_usu, "", 0, Me.cboCecos.SelectedValue, IIf(chkFaltanteDesap.Checked = True, 1, 0), 0).copyto(valoresdevueltos, 0)
                '===============================================================================================================================================================================================================================================================================================================================================================================================================
                Me.hdcodigo_cup_lho.Value = valoresdevueltos(0)


                'Response.Write(">>><>>>PROBANDO")
                If (valoresdevueltos(0) <> "0" And valoresdevueltos(0) <> "") Then


                    Me.cmdRegistrarHorario.enabled = True
                    '==================================
                    'Asignar Carga Académica
                    '==================================

                    'Response.Write(valoresdevueltos(0))
                    'cambioGo
                    'obj.Ejecutar("CAR_AgregarCargaAcademica", Me.dpCodigo_per.SelectedValue, valoresdevueltos(0), codigo_usu, Request.QueryString("mod"), System.DBNull.Value)
                    obj.Ejecutar("CAR_AgregarPreCargaAcademica_go", valoresdevueltos(0), Me.dpCodigo_per.SelectedValue, codigo_usu, "", "")


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
                obj.Ejecutar("ModificarCursoProgramado_V2", Me.hdcodigo_cup.Value, Me.txtGrupoHor_cup.Text.Trim, Me.dpestado_cup.SelectedValue, Me.txtVacantes.Text, CDate(Me.txtInicio.Text), CDate(Me.txtFin.Text), CDate(Me.txtRetiro.Text), codigo_usu, " ", Me.dpCodigo_pes.SelectedValue, IIf(chkFaltanteDesap.Checked = True, 1, 0), 0, 0) '.copyto(valoresdevueltos, 0)
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

                Me.cmdRegistrarHorario.enabled = True
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
        Me.fraDetalleAmbiente.Visible = False
        Me.fraGruposProgramados.Visible = estado
        Me.grwProfesores.DataBind()
        Me.dpCurso.SelectedValue = -1
        Me.dpCodigo_per.SelectedValue = -1
        Me.hdcodigo_cup.Value = 0

        'Agregado xDguevara 27.11.2012
        Me.chkNivelacion.Checked = False
        Me.chkFaltanteDesap.Checked = False
    End Sub
    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        Me.cmdAgregar.Enabled = False
        Me.fraGruposProgramados.Visible = False
        CargarDetalleGrupoHorario("7", 0, "n")
        Me.cmdAgregar.Enabled = True
        chkNivelacion.Visible = False

        Me.cmdRegistrarHorario.enabled = False

        'chkFaltanteDesap.Visible = True 'CambioGo
        chkFaltanteDesap.Visible = False
        'chkFaltanteDesap.checked = True

        'lblNiv.Visible = True 'cambioGO
        lblNiv.Visible = False 'cambioGO
        '===================================
        'Response.Write(dpCodigo_cac.SelectedValue)
        'Response.Write("<br />")
        'Response.Write(cboCecos.SelectedValue)
        'Response.Write("<br />")
        CargarDatosCursos()
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
            'cambioGo
            'gr.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 4, fila.Row("codigo_cup"), Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, 0)
            gr.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 11, fila.Row("codigo_cup"), Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, 0)
            gr.DataBind()
            obj.CerrarConexion()
            obj = Nothing
            '================================================
        End If
    End Sub
    Protected Sub grwGruposProgramados_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwGruposProgramados.RowDeleting
        Dim obj As New ClsConectarDatos
        Dim valoresdevueltos(1) As String
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            'obj.Ejecutar("EliminarCursoProgramado", grwGruposProgramados.DataKeys(e.RowIndex).Values("codigo_cup"), 0).copyto(valoresdevueltos, 0)
            obj.Ejecutar("EliminarCursoProgramado", grwGruposProgramados.DataKeys(e.RowIndex).Values("codigo_cup"), Request.QueryString("id"), 0).copyto(valoresdevueltos, 0)

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
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, "string".GetType(), "ErrorEliminacion", "alert('Error: " & ex.message & "');", True)
        End Try        
    End Sub
    Protected Sub cmdAsignarProfesor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAsignarProfesor.Click
        If dpCodigo_per.SelectedValue <> -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            'obj.Ejecutar("CAR_AgregarCargaAcademica", Me.dpCodigo_per.SelectedValue, Me.hdcodigo_cup.Value, Request.QueryString("id"), Request.QueryString("mod"), System.DBNull.Value)
            'CambioGo
            obj.Ejecutar("CAR_AgregarPreCargaAcademica_go", Me.hdcodigo_cup.Value, Me.dpCodigo_per.SelectedValue, Request.QueryString("id"), "", "")
            obj.CerrarConexion()
            obj = Nothing
            Me.CargarDetalleGrupoHorario(3, Me.hdcodigo_cup.Value, "p")
        End If
    End Sub
    Protected Sub grwProfesores_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwProfesores.RowDeleting
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        'CambioGo
        'obj.Ejecutar("EliminarCargaAcademica", grwProfesores.DataKeys(e.RowIndex).Values("codigo_per"), Me.hdcodigo_cup.Value, Request.QueryString("id"))
        obj.Ejecutar("CAR_EliminarPreCargaAcademica_go", Me.hdcodigo_cup.Value, grwProfesores.DataKeys(e.RowIndex).Values("codigo_per"), Request.QueryString("id"))


        'CambioGo
        'Me.grwProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 4, Me.hdcodigo_cup.Value, 0, 0, 0)
        Me.grwProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 11, Me.hdcodigo_cup.Value, 0, 0, 0)
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

            If e.Row.Cells(1).Text <> "Sugerido" Then
                e.Row.Cells(5).Text = ""
            End If
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
            'ClsFunciones.LlenarListas(Me.dpCurso, obj.TraerDataTable("ConsultarCursoProgramado", 1, Me.dpCodigo_pes.SelectedValue, Me.dpCodigo_cac.SelectedValue, "", ""), "codigo_cur", "nombre_cur", "--Seleccione el curso--")
            'verificar cuando cambie el check de cursos faltantes

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
            'Response.Write(Me.dpCodigo_cac.SelectedValue & "<br>")
            'Response.Write(Me.dpCodigo_pes.SelectedValue & "<br>")
            'Response.Write(Me.cboCecos.SelectedValue & "<br>")
            'Response.Write(Me.ddlFiltro.SelectedValue & "<br>")
            'Response.Write(Me.txtBusqueda.Text.Trim.ToString & "<br>")
            Me.grwGruposProgramados.DataSource = obj.TraerDataTable("EVE_ConsultarProgramacionAcademica2", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.cboCecos.SelectedValue, ddlFiltro.SelectedValue, txtBusqueda.Text.Trim.ToString)
            Me.grwGruposProgramados.DataBind()

            'Response.Write("Codigo_cac = " & Me.dpCodigo_cac.SelectedValue)
            'Response.Write("Codigo_pes = " & Me.dpCodigo_pes.SelectedValue)
            'Response.Write("Centro Costos=" & Me.cboCecos.SelectedValue)

            'Cargar los cursos del Plan de Estudios
            'CambioGo
            ClsFunciones.LlenarListas(Me.dpCurso, obj.TraerDataTable("ConsultarCursoProgramado", 1, Me.dpCodigo_pes.SelectedValue, Me.dpCodigo_cac.SelectedValue, "", ""), "codigo_cur", "nombre_Cur_go", "--Seleccione el curso--")

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

    Protected Sub ibtnRecuperacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try

            Dim ibtnRecuperacion As ImageButton
            Dim row As GridViewRow
            Dim hfCodigo_cup As HiddenField


            ibtnRecuperacion = sender
            row = ibtnRecuperacion.NamingContainer
            hfCodigo_cup = row.FindControl("hfcodigo_cupRec")



        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub ibtnInvitar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    '    'Response.Write("btn hfCodigo_cup:-> " & hfCodigo_cup.Value)
    '    'ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "javascript:location.href='frmListaAlumnosProfesionalizacion.aspx?id=" & Me.Request.QueryString("id") & "&codigo_cup=" & hfCodigo_cup.Value & "&Codigo_cac=" & dpCodigo_cac.SelectedValue & "&Codigo_pes=" & dpCodigo_pes.SelectedValue & "&Codigo_cco=" & cboCecos.SelectedValue & "&ctf=" & Request.QueryString("ctf") & "&mod=" & Request.QueryString("mod") & "&Accion=" & "L" & "';", True)
    '    'ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmListaAlumnosProfesionalizacion.aspx?id=" & Me.Request.QueryString("id") & "&codigo_cup=" & hfCodigo_cup.Value & "&Codigo_cac=" & dpCodigo_cac.SelectedValue & "&Codigo_pes=" & dpCodigo_pes.SelectedValue & "&Codigo_cco=" & cboCecos.SelectedValue & "&ctf=" & Request.QueryString("ctf") & "&mod=" & Request.QueryString("mod") & "&Accion=" & "L" & "';", True)
    '    'ibtnInvitar.OnClientClick = "AbrirPopUp('frmListaAlumnosProfesionalizacion.aspx?id=" & Me.Request.QueryString("id") & "&codigo_cup=" & hfCodigo_cup.Value & "&Codigo_cac=" & dpCodigo_cac.SelectedValue & "&Codigo_pes=" & dpCodigo_pes.SelectedValue & "&Codigo_cco=" & cboCecos.SelectedValue & "&ctf=" & Request.QueryString("ctf") & "&mod=" & Request.QueryString("mod") & "&Accion=" & "L" & " ','680','1100','yes','yes','yes','yes')"
    '    '==========================
    '    Try
    '        Dim strURL As String
    '        Dim ScriptString As String
    '        Dim ibtnInvitar As ImageButton
    '        Dim row As GridViewRow
    '        Dim hfCodigo_cup As HiddenField


    '        ibtnInvitar = sender
    '        row = ibtnInvitar.NamingContainer
    '        hfCodigo_cup = row.FindControl("hfcodigo_cup")

    '        'Sesiones:
    '        Session("s_id") = Me.Request.QueryString("id")
    '        Session("s_codigo_cup") = hfCodigo_cup.Value
    '        Session("s_codigo_cac") = dpCodigo_cac.SelectedValue
    '        Session("s_codigo_pes") = dpCodigo_pes.SelectedValue
    '        Session("s_cboCecos") = cboCecos.SelectedValue
    '        Session("s_ctf") = Request.QueryString("ctf")
    '        Session("s_mod") = Request.QueryString("mod")
    '        Session("s_accion") = "L"


    '        'dguevara -02.08.2013-
    '        'Temporalmente voy a enviar los parametros asi, luego los cambio por sessiones para mejorar la seguridad
    '        'strURL = "frmListaAlumnosProfesionalizacion.aspx?id=" & Me.Request.QueryString("id") & "&codigo_cup=" & hfCodigo_cup.Value & "&Codigo_cac=" & dpCodigo_cac.SelectedValue & "&Codigo_pes=" & dpCodigo_pes.SelectedValue & "&Codigo_cco=" & cboCecos.SelectedValue & "&ctf=" & Request.QueryString("ctf") & "&mod=" & Request.QueryString("mod") & "&Accion=" & "L" & ""
    '        strURL = "frmListaAlumnosProfesionalizacion.aspx"
    '        ScriptString = "<script language='javascript'>"
    '        ScriptString += "window.open('" & strURL & "', '_blank','height=750,width=1280,top=50,left=50,resizable=yes,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes');"
    '        ScriptString += "</script>"
    '        ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", ScriptString)
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    Protected Sub ibtnInvitar2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
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
            strURL = "frmListaAlumnosProfesionalizacionF.aspx"
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

            chkNivelacion.Visible = False       'Agregado xDguevara

            lblNiv.Visible = False


            Me.fraGruposProgramados.Visible = False
            'Me.lblnombre_cur.Text = Me.grwGruposProgramados.Rows(Convert.ToInt32(e.CommandArgument).ToString).Cells(0).Text
            Me.lblnombre_cur.Text = Me.grwGruposProgramados.Rows(Convert.ToInt32(e.CommandArgument).ToString).Cells(0).Text & " " & Me.grwGruposProgramados.Rows(Convert.ToInt32(e.CommandArgument).ToString).Cells(1).Text
            Me.hdcodigo_cup.Value = Convert.ToString(Me.grwGruposProgramados.DataKeys(Convert.ToInt32(e.CommandArgument)).Value)
            Me.CargarDetalleGrupoHorario(3, Me.hdcodigo_cup.Value, "e")
            Me.cmdRegistrarHorario.enabled = True
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
        Me.lblCurso.Visible = True

        Me.gridHorario.Visible = False
        ' Me.cmdVer0.Visible = False
        Me.cmdRegistrarHorario.Visible = False
        Me.CheckBox1.Visible = False
        Me.pnlRegistrar.Visible = True
        Me.DetallesCurso.Visible = False
        Me.lblCurso.Text = Me.lblnombre_cur.Text & " - " & Me.txtGrupoHor_cup.text
        CargarFechas()
        Me.chkVarias.Checked = False
        chkVarias_CheckedChanged(sender, e)
        Me.txtDescripcion.Text = ""
        Me.ddlCap.SelectedIndex = 0
        Me.ddlNro.SelectedIndex = 0
        Me.ddlTipSolicitud.SelectedIndex = 0
        Me.lblmsj.Text = ""
        Me.chkAudi.Checked = False
        'Otro tipo de horario para PROFESIONALIZACION
        If Request.QueryString("mod") = 3 Then
            Me.ddlInicioHora.Visible = False
            Me.ddlInicioMinuto.Visible = False
            Me.ddlFinHora.Visible = False
            Me.ddlFinMinuto.Visible = False
        Else
            Me.ddlHoraInicioProf.Visible = False
            Me.ddlHoraFinProf.Visible = False
            Me.lbldia.Visible = False
        End If
        Me.lblCruce.visible = False
        Me.gvCruces.visible = False
        Me.gvCruces.datasource = Nothing
        Me.gvCruces.databind()
    End Sub

    Protected Sub gridHorario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridHorario.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(7).Text = "1" Then
                e.Row.Cells(7).Text = "<img src='image/star.png' title='Ambiente preferencial'>"

            ElseIf e.Row.Cells(7).Text = "0" And e.Row.Cells(8).Text = "Sin ambiente solicitado" Then
                e.Row.Cells(7).Text = "-"
            ElseIf e.Row.Cells(7).Text = "0" Then
                e.Row.Cells(7).Text = "<img src='image/door.png' title='Ambiente normal'>"
            End If

            e.Row.Cells(8).Font.Bold = True
            e.Row.Cells(3).Font.Bold = True

            If e.Row.Cells(9).Text = "Pendiente" Then
                e.Row.Cells(9).ForeColor = Drawing.Color.Green
            Else
                e.Row.Cells(9).ForeColor = Drawing.Color.Blue
            End If

            If e.Row.Cells(9).Text = "Pendiente" And e.Row.Cells(8).Text <> "Sin ambiente solicitado" Then
                e.Row.Cells(10).Text = "-"
            End If
            If e.Row.Cells(9).Text = "Asignado" Then
                e.Row.Cells(10).Text = "-"
            End If
            If e.Row.Cells(8).Text = "Sin ambiente solicitado" Then
                e.Row.Cells(11).Text = "-"
            End If


            If CDate(e.Row.Cells(2).Text) < CDate(Today) Then
                e.Row.Cells(9).ForeColor = Drawing.Color.Gray
                e.Row.Cells(9).Text = e.Row.Cells(9).Text & " - Finalizado"
            End If
            If CDate(e.Row.Cells(2).Text) = CDate(Today) Then
                e.Row.Cells(9).ForeColor = Drawing.Color.IndianRed
                e.Row.Cells(9).Text = e.Row.Cells(9).Text & " - Hoy"
            End If
        End If

    End Sub
    Sub consultarTipos()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.ddlTipSolicitud.DataSource = obj.TraerDataTable("HorarioPE_ListarTipoSolicitud")
        obj.CerrarConexion()
        obj = Nothing
        Me.ddlTipSolicitud.DataTextField = "nombre_ambts"
        Me.ddlTipSolicitud.DataValueField = "codigo_ambts"
        Me.ddlTipSolicitud.DataBind()
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

        For i As Integer = 1 To 31
            If i < 10 Then
                item = "0" & i.ToString
            Else
                item = i.ToString
            End If
            '  Me.ddlInicioDia.Items.Add(New ListItem(item.ToString(), item.ToString()))
            ' Me.ddlFinDia.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i

        For i As Integer = Today.Year To Today.Year + 5
            item = i.ToString
            ' Me.ddlInicioAño.Items.Add(New ListItem(item.ToString(), item.ToString()))
            '  Me.ddlFinAño.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i
        ' Me.ddlInicioMes.SelectedIndex = Today.Month - 1
        ' Me.ddlFinMes.SelectedIndex = Today.Month - 1
        ' Me.ddlInicioDia.SelectedIndex = Today.Day - 1s
        'Me.ddlFinDia.SelectedIndex = Day(DateSerial(Year(Now.Date), Month(Now.Date) + 1, 0)) - 1 'Today.Day + 1
        txtDesde.Value = Today.Date
        txtHasta.Value = Today.Date
    End Sub


    Protected Sub gridHorario_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridHorario.RowDeleting
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("HorarioPE_EliminarLH", gridHorario.DataKeys(e.RowIndex).Values("codigo_lho"))
        Cargarhorario()
        obj.CerrarConexion()
        obj = Nothing
        e.Cancel = True
    End Sub



    Protected Sub gridHorario_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridHorario.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)

        ' Response.Write(e.CommandName.ToString)

        If (e.CommandName = "LimpiarAmbiente") Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("HorarioPE_LimpiarAmbiente", gridHorario.DataKeys(index).Values("codigo_lho"), 1)
            Cargarhorario()
            obj.CerrarConexion()
            obj = Nothing
        End If

        If (e.CommandName = "SolicitarAmbiente") Then
            Session("h_codigolho") = gridHorario.DataKeys(index).Values("codigo_lho")
            Me.gridHorario.Visible = False
            Me.cmdRegistrarHorario.Visible = False
            Me.CheckBox1.Visible = False
            Me.DetallesCurso.Visible = False
            Me.pnlRegistrar.Visible = False
            Me.pnlSolictar.Visible = True
            CargarDatos()

        End If

        If (e.CommandName = "ModificarLho") Then
            Try

            
                Dim codigo As Integer = gridHorario.DataKeys(index).Values("codigo_lho")
                Dim obj As New ClsConectarDatos
                Dim tb As New Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                tb = obj.TraerDataTable("ACAD_horarioListar", 0, codigo)
                obj.CerrarConexion()
                obj = Nothing
                Me.txtcodhor.Value = codigo
                Me.txtdescripcionhor.Value = tb.Rows(0).Item("descripcion_lho").ToString()
                ActivarFormularioHorario(True)
            Catch ex As Exception

            End Try
        End If
    End Sub


    Private Sub ActivarFormularioHorario(ByVal sw As Boolean)
        If sw Then
            Me.gridHorario.Enabled = False
            Me.divHorReg.Visible = True
        Else
            Me.gridHorario.Enabled = True
            Me.divHorReg.Visible = False
            Me.txtdescripcionhor.Value = ""
            Me.txtcodhor.Value = ""
        End If

    End Sub


    Protected Sub chkVarias_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkVarias.CheckedChanged
        'Me.ddlFinDia.Enabled = chkVarias.Checked
        '  Me.ddlFinMes.Enabled = chkVarias.Checked
        ' Me.ddlFinAño.Enabled = chkVarias.Checked

        Me.txtHasta.Disabled = Not chkVarias.Checked
        Me.ddlDiaSelPer.Enabled = chkVarias.Checked
        cargarDiaVariasSesiones()
    End Sub

    Protected Sub btnRegistrarPers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrarPers.Click
        Me.lblmsj.Text = ""


        If Me.ddlTipSolicitud.SelectedValue = 0 Then
            Me.lblmsj.Text = "Debe seleccionar un Motivo de Solicitud"
            Exit Sub
        End If
        If Me.txtDescripcion.Text.Trim = "" Then
            Me.lblmsj.Text = "Debe escribir una descripción de la actividad"
            Exit Sub
        End If


        Dim fechaInicio As New Date
        Dim fechaFin As New Date
        Dim nombre_hor As String = ""
        Dim horaFin As String = ""
        Dim día As String
        Dim día1 As String
        nombre_hor = Me.ddlHoraInicioProf.SelectedItem.Text & ":" & Me.ddlInicioMinuto.SelectedItem.Text
        horaFin = Me.ddlHoraFinProf.SelectedItem.Text & ":" & Me.ddlFinMinuto.SelectedItem.Text
        fechaInicio = New Date(CInt(DatePart(DateInterval.Year, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Month, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Day, CDate(txtDesde.Value))), CInt(nombre_hor.Substring(0, 2)), CInt(nombre_hor.Substring(3, 2)), 0)
        fechaFin = New Date(CInt(DatePart(DateInterval.Year, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Month, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Day, CDate(txtDesde.Value))), CInt(horaFin.Substring(0, 2)), CInt(horaFin.Substring(3, 2)), 0)
        día = Me.ddlDiaSelPer.SelectedValue


        '##Validar día domingo
        If WeekdayName(Weekday(fechaInicio)) = "domingo" Then
            Me.pnlPregunta.Visible = True
            Me.pnlRegistrar.Visible = False
            Me.Label1.Text = Me.txtDesde.Value
            Me.lblActividad.Text = Me.txtDescripcion.Text
            Me.lblFecha.Text = Me.txtDesde.Value
            Exit Sub
        End If

        'CambioGo
        '##Validar que el horario a registrar no se cruce en el mismo ciclo_cur, cco y  semestre
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        If Me.hdcodigo_cup.value = 0 Then
            Me.hdcodigo_cup = hdcodigo_cup_lho
        End If
        tb = obj.TraerDataTable("horarioGo_VerificarCruce", Me.hdcodigo_cup.Value, fechaInicio, fechaInicio)
        If tb.rows.count > 0 Then
            Me.lblmsj.text = "Existe cruce de Horario" '& fechaInicio & " - " & fechaFin
            Me.lblCruce.visible = True
            Me.gvCruces.visible = True
            Me.gvCruces.datasource = tb
            Me.gvCruces.databind()
        Else
            Me.lblCruce.visible = False
            Me.gvCruces.visible = False
            Me.gvCruces.datasource = Nothing
            Me.gvCruces.databind()
            RegistrarHorario()
        End If

    End Sub

    Public Function ValidarFecha(ByVal fechaInicio As Date, ByVal fechaFin As Date) As Boolean
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        ' 0. hora correctas
        If (CDate(fechaInicio.ToString("H:mm")) > CDate(fechaFin.ToString("H:mm"))) And Me.chkVarias.Checked = False Then
            Errores = "Verifique las horas"
            Return False
        End If

        '1. Fuera de Fecha
        If CDate(fechaInicio.ToString("dd/MM/yyyy")) < CDate(Today) Then
            Errores = "No puede registrar solicitudes en días anteriores al día hoy"
            Return False
        End If

        '2. Fecha Límite
        ' se comentó temporalmenente para que no valide fecha límite
        ' se volvió a activar
        obj.AbrirConexion()
        tb = obj.TraerDataTable("HORARIOPE_ConsultarAmbienteConfig", 1)
        Dim FechaLimite As Date
        FechaLimite = CDate(tb.Rows(0)(1))
        obj.CerrarConexion()
        If CDate(fechaInicio.ToString("dd/MM/yyyy")) > CDate(FechaLimite.ToString("dd/MM/yyyy")) And Request.QueryString("mod") <> 5 And chkAudi.Checked = False Then
            Errores = "Solo pueden registrarse solicitudes hasta el " & FechaLimite
            Return False
        End If


        '3. Feriados
        obj.AbrirConexion()
        tb = obj.TraerDataTable("HorarioPE_ConsultarFeriado", CInt(DatePart(DateInterval.Day, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Month, CDate(txtDesde.Value))))
        obj.CerrarConexion()
        If tb.Rows.Count Then
            Errores = "No puede registrar solicitud para un Día No Laborable"
            Return False
        End If

        '4. No registrar domingos: Valido para varias solicitudes
        If WeekdayName(Weekday(fechaInicio)) = "domingo" And Me.chkVarias.Checked Then
            Return False
        End If

        Return True

    End Function
    Sub RegistrarHorario()
        Try
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim HayCru As Boolean = False
            Dim día As String
            Dim nombre_hor As String = ""
            Dim horaFin As String = ""
            Dim usu As Integer = CInt(Request.QueryString("id"))
            Dim msjVariasSesSI As String = ""
            Dim msjVariasSesNO As String = ""
            Dim TbHorariosReg As New Data.DataTable
            Dim TbHorariosCru As New Data.DataTable

            día = Me.ddlDiaSelPer.SelectedValue
            nombre_hor = Me.ddlInicioHora.SelectedItem.Text & ":" & Me.ddlInicioMinuto.SelectedItem.Text
            horaFin = Me.ddlFinHora.SelectedItem.Text & ":" & Me.ddlFinMinuto.SelectedItem.Text

            'If Request.QueryString("mod") = 3 Then
            '    nombre_hor = Me.ddlHoraInicioProf.SelectedItem.Text & ":" & Me.ddlInicioMinuto.SelectedItem.Text
            '    horaFin = Me.ddlHoraFinProf.SelectedItem.Text & ":" & Me.ddlFinMinuto.SelectedItem.Text
            'End If

            Dim fechaInicio As New Date '(CInt(Me.ddlInicioAño.SelectedValue), CInt(Me.ddlInicioMes.SelectedValue), CInt(Me.ddlInicioDia.SelectedValue), CInt(nombre_hor.Substring(0, 2)), CInt(nombre_hor.Substring(3, 2)), 0)
            Dim fechaFin As New Date '(CInt(Me.ddlFinAño.SelectedValue), CInt(Me.ddlFinMes.SelectedValue), CInt(Me.ddlFinDia.SelectedValue), CInt(horaFin.Substring(0, 2)), CInt(horaFin.Substring(3, 2)), 0)

            Dim nroAmbientes As Integer = Me.ddlNro.SelectedValue
            For i As Integer = 1 To nroAmbientes
                fechaInicio = New Date(CInt(DatePart(DateInterval.Year, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Month, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Day, CDate(txtDesde.Value))), CInt(nombre_hor.Substring(0, 2)), CInt(nombre_hor.Substring(3, 2)), 0)
                fechaFin = New Date(CInt(DatePart(DateInterval.Year, CDate(txtHasta.Value))), CInt(DatePart(DateInterval.Month, CDate(txtHasta.Value))), CInt(DatePart(DateInterval.Day, CDate(txtHasta.Value))), CInt(horaFin.Substring(0, 2)), CInt(horaFin.Substring(3, 2)), 0)

                If chkVarias.Checked Then
                    If fechaInicio < fechaFin Then
                        Do
                            tb = New Data.DataTable
                            If WeekdayName(Weekday(fechaInicio)) = Me.ddlDiaSelPer.SelectedItem.Text.ToLower Then
                                obj.AbrirConexion()
                                TbHorariosReg = obj.TraerDataTable("HorarioPE_Consultar", Me.hdcodigo_cup.Value, "F", fechaInicio)
                                obj.CerrarConexion()

                                If ValidarFecha(fechaInicio, fechaFin) Then
                                    obj.AbrirConexion()
                                    Dim fechaInicioG As New Date(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, fechaInicio.Hour, fechaInicio.Minute, 0)
                                    Dim fechaFinG As New Date(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, fechaFin.Hour, fechaFin.Minute, 0)
                                    tb = obj.TraerDataTable("HorarioPE_Registrar", día, Me.hdcodigo_cup.Value, nombre_hor, horaFin, usu, fechaInicioG, fechaFinG, 1, IIf(Me.txtDescripcion.Text.Length > 0, Me.txtDescripcion.Text.Trim, DBNull.Value), Me.ddlCap.SelectedValue, Me.ddlTipSolicitud.SelectedValue, CInt(Me.chkAudi.Checked))
                                    obj.CerrarConexion()
                                Else
                                    msjVariasSesNO = msjVariasSesNO & fechaInicio.ToString.Substring(0, 10) & " " & nombre_hor & " - " & horaFin & ", "
                                End If

                            End If
                            fechaInicio = fechaInicio.AddDays(1)
                        Loop While fechaInicio <= fechaFin
                    Else
                        Response.Write("Fecha Fin debe ser mayor a Fecha Inicio")
                        Exit Sub
                    End If


                Else

                    'fechaFin = New Date(CInt(Me.ddlInicioAño.SelectedValue), CInt(Me.ddlInicioMes.SelectedValue), CInt(Me.ddlInicioDia.SelectedValue), CInt(horaFin.Substring(0, 2)), CInt(horaFin.Substring(3, 2)), 0)
                    fechaFin = New Date(CInt(DatePart(DateInterval.Year, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Month, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Day, CDate(txtDesde.Value))), CInt(horaFin.Substring(0, 2)), CInt(horaFin.Substring(3, 2)), 0)


                    If ValidarFecha(fechaInicio, fechaFin) Then
                        obj.AbrirConexion()
                        Dim culture As New System.Globalization.CultureInfo("es-ES")
                        día = (culture.DateTimeFormat.GetDayName(fechaInicio.DayOfWeek)).ToString.Substring(0, 2).ToUpper
                        día = Replace(día, "Á", "A")
                        tb = obj.TraerDataTable("HorarioPE_Registrar", día, Me.hdcodigo_cup.Value, nombre_hor, horaFin, usu, fechaInicio, fechaFin, 1, IIf(Me.txtDescripcion.Text.Length > 0, Me.txtDescripcion.Text.Trim, DBNull.Value), Me.ddlCap.SelectedValue, Me.ddlTipSolicitud.SelectedValue, CInt(Me.chkAudi.Checked))
                        obj.CerrarConexion()
                    Else
                        Me.lblmsj.Text = "No se puede registrar: " & fechaInicio.ToString.Substring(0, 10) & " " & nombre_hor & " - " & horaFin & " : " & Errores
                    End If
                End If
            Next

            If tb.Rows.Count And Me.chkVarias.Checked = False Then
                Session("h_codigolho") = tb.Rows(0).Item(0).ToString
                Me.DetallesCurso.Visible = False
                Me.pnlRegistrar.Visible = False
                Me.pnlSolictar.Visible = True
                CargarDatos()
            End If
            If Me.chkVarias.Checked Then
                Me.lblCurso.Visible = False
                Me.pnlRegistrar.Visible = False
                Me.pnlSolictar.Visible = False
                Me.DetallesCurso.Visible = True
                ' Me.cmdVer0.Visible = True
                Me.cmdRegistrarHorario.Visible = True
                Me.CheckBox1.Visible = True
                Me.gridHorario.Visible = True
                Me.lblmsj.Text = ""
                Cargarhorario()
            End If
            tb.Dispose()
            obj = Nothing
            VerificarSolicitudAuditorio()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Sub VerificarSolicitudAuditorio()
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim TablaDatosRegistro As Data.DataTable
            TablaDatosRegistro = obj.TraerDataTable("HorarioPE_Consultar", Me.ddlHorarios.SelectedValue, "A")
            If TablaDatosRegistro.Rows(0).Item(0) Then
                Me.ddlTipoAmbiente.SelectedValue = 11
                Me.ddlTipoAmbiente.Enabled = False
            Else
                Me.ddlTipoAmbiente.SelectedValue = -1
                Me.ddlTipoAmbiente.Enabled = True
            End If
            obj.CerrarConexion()
            obj = Nothing
            TablaDatosRegistro = Nothing
            Me.gridAmbientes.DataSource = Nothing
            Me.gridAmbientes.DataBind()
        Catch ex As Exception

        End Try

    End Sub
    Sub CargarDatos()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ClsFunciones.LlenarListas(Me.ddlAudio, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "audio", 0, "T"), "codigo_camb", "descripcion_cam")
        ClsFunciones.LlenarListas(Me.ddlDis, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "distribución", 0, "T"), "codigo_camb", "descripcion_cam")
        ClsFunciones.LlenarListas(Me.ddlOtros, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "otros", 0, "T"), "codigo_camb", "descripcion_cam")
        ClsFunciones.LlenarListas(Me.ddlSillas, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "sillas", 0, "T"), "codigo_camb", "descripcion_cam")
        ClsFunciones.LlenarListas(Me.ddlVideo, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "video", 0, "T"), "codigo_camb", "descripcion_cam")
        ClsFunciones.LlenarListas(Me.ddlVenti, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "ventilación", 0, "T"), "codigo_camb", "descripcion_cam")


        Me.ddlHorarios.DataSource = obj.TraerDataTable("HorarioPE_Consultar", Me.hdcodigo_cup.Value, "S")
        Me.ddlHorarios.DataTextField = "dia"
        Me.ddlHorarios.DataValueField = "codigo_lho"
        Me.ddlHorarios.DataBind()
        If Session("h_codigolho") <> "0" Then
            Me.ddlHorarios.SelectedValue = Session("h_codigolho")
        End If
        obj.CerrarConexion()
        obj = Nothing
        VerificarSolicitudAuditorio()
        ' Me.Label1.Text = Session("h_nombre_cur")
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tb As New Data.DataTable
        Dim tbAmb As New Data.DataTable
        Dim idsamb As String = ""
        obj.AbrirConexion()
        tb = obj.TraerDataTable("HorarioPE_ConsultarAsignacionAmbienteSol2", ddlHorarios.SelectedValue, ddlAudio.SelectedValue, ddlVideo.SelectedValue, ddlSillas.SelectedValue, ddlDis.SelectedValue, ddlOtros.SelectedValue, Me.ddlVenti.SelectedValue, Me.ddlTipoAmbiente.SelectedValue)
        obj.CerrarConexion()
        If tb.Rows.Count Then
            For i As Integer = 0 To tb.Rows.Count - 1
                idsamb = idsamb & tb.Rows(i).Item("codigo_amb").ToString & ","
            Next
            idsamb = idsamb.ToString.Substring(0, idsamb.Length - 1)
            obj.AbrirConexion()
            Me.gridAmbientes.DataSource = obj.TraerDataTable("Ambiente_ListarAmbienteCaracSolXCup", idsamb)

            Me.gridAmbientes.DataBind()

            obj.CerrarConexion()
            ListadoAmbientes.Visible = True
        Else
            Me.gridAmbientes.DataSource = Nothing
            Me.gridAmbientes.DataBind()
            ListadoAmbientes.Visible = False
        End If

        obj = Nothing
    End Sub

    Protected Sub gridAmbientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAmbientes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim tb As New Data.DataTable
            Dim ultimo As Integer = e.Row.Cells.Count
            tb = gridAmbientes.DataSource
            Dim imgbtn As New Image
            imgbtn = CType(e.Row.FindControl("imgSol"), Image)
            For i As Integer = 2 To tb.Columns.Count + 1
                e.Row.Cells(ultimo - 1).Text = (e.Row.Cells(ultimo - 1).Text.Split("_")).GetValue(0).ToString()
                If e.Row.Cells(i - 1).Text = "1" Then
                    e.Row.Cells(i - 1).Text = "<img src='image/yes.png' title=""si"">"
                End If
                If e.Row.Cells(i - 1).Text = "0" Then
                    e.Row.Cells(i - 1).Text = "<img src='image/no.png'  title=""no"" >"
                End If
                'Preferencial
                If e.Row.Cells(1).Text = "S" Then
                    e.Row.Cells(1).Text = "<img src='image/star.png' title='Ambiente preferencial'>"
                    imgbtn.ImageUrl = "image/savego.png"
                End If
                'Normal
                If e.Row.Cells(1).Text = "N" Then
                    e.Row.Cells(1).Text = "<img src='image/door.png' title='Ambiente'>"
                    imgbtn.ImageUrl = "image/save.png"
                End If
            Next
            'No mostrar nombres reales
            'e.Row.Cells(1).Text = "Ambiente Disponible N° " & (e.Row.RowIndex + 1).ToString
            tb.Dispose()
        End If
    End Sub


    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.lblCurso.Visible = False
        Me.pnlRegistrar.Visible = False
        Me.pnlSolictar.Visible = False
        Me.DetallesCurso.Visible = True
        ' Me.cmdVer0.Visible = True
        Me.cmdRegistrarHorario.Visible = True
        Me.CheckBox1.Visible = True
        Me.gridHorario.Visible = True
        Me.lblmsj.Text = ""
        Me.lblCruce.visible = False
        Me.gvCruces.visible = False
        Me.gvCruces.datasource = Nothing
        Me.gvCruces.databind()
        Cargarhorario()
    End Sub

    Protected Sub gridAmbientes_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridAmbientes.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "Asignar/Solicitar") Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("HorarioPE_RegistrarAmbienteSol", Me.ddlHorarios.SelectedValue, Split(gridAmbientes.DataKeys(index).Values("Ambiente"), "_").GetValue(1).ToString(), "S")
            Cargarhorario()
            If EnviarCorreo(Me.ddlHorarios.SelectedValue) Then
                Me.DetallesCurso.Visible = True
                Me.gridHorario.Visible = True
                Me.pnlSolictar.Visible = False
                Me.gridAmbientes.DataSource = Nothing
                Me.gridAmbientes.DataBind()
                Me.lblCurso.Text = ""
                Me.cmdRegistrarHorario.Visible = True
                Me.CheckBox1.Visible = True
            Else
                Response.Write("<script>alert('Ocurrió un error al enviar correo')</script>")
            End If
            obj.CerrarConexion()
            obj = Nothing
        End If

    End Sub

    Protected Sub btnCancelar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar0.Click
        Me.DetallesCurso.Visible = True
        Me.gridHorario.Visible = True
        Me.pnlSolictar.Visible = False
        Me.gridAmbientes.DataSource = Nothing
        Me.gridAmbientes.DataBind()
        Me.lblCurso.Text = ""
        Me.cmdRegistrarHorario.Visible = True
        Me.CheckBox1.Visible = True
        Cargarhorario()
    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Cargarhorario()
    End Sub


    Sub CargarDia()
        Me.ddlHoraInicioProf.Enabled = True
        Me.ddlHoraFinProf.Enabled = True
        Dim fechaInicio As New Date
        Dim nombre_hor As String = ""
        nombre_hor = Me.ddlInicioHora.SelectedItem.Text & ":" & Me.ddlInicioMinuto.SelectedItem.Text
        If Request.QueryString("mod") = 3 Then
            nombre_hor = Me.ddlHoraInicioProf.SelectedItem.Text & ":" & Me.ddlInicioMinuto.SelectedItem.Text
        End If
        'fechaInicio = New Date(CInt(Me.ddlInicioAño.SelectedValue), CInt(Me.ddlInicioMes.SelectedValue), CInt(Me.ddlInicioDia.SelectedValue))
        fechaInicio = New Date(CInt(DatePart(DateInterval.Year, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Month, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Day, CDate(txtDesde.Value))), CInt(nombre_hor.Substring(0, 2)), CInt(nombre_hor.Substring(3, 2)), 0)

        Me.lbldia.Text = "Día de Sesión:" & WeekdayName(Weekday(fechaInicio)).ToUpper

        If WeekdayName(Weekday(fechaInicio)) = "sábado" Then
            Me.ddlHoraInicioProf.SelectedValue = 15
            Me.ddlHoraFinProf.SelectedValue = 18
        ElseIf WeekdayName(Weekday(fechaInicio)) <> "domingo" Then
            Me.ddlHoraInicioProf.SelectedValue = 19
            Me.ddlHoraFinProf.SelectedValue = 23
        Else
            ' Me.ddlHoraInicioProf.Enabled = False
            ' Me.ddlHoraFinProf.Enabled = False
        End If

    End Sub

    Protected Sub btnSi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSi.Click
        Me.pnlPregunta.Visible = False
        RegistrarHorario()
    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        Me.pnlPregunta.Visible = False
        Me.pnlRegistrar.Visible = True
        Me.txtDesde.Focus()
    End Sub

    Protected Sub ddlHorarios_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlHorarios.SelectedIndexChanged
        VerificarSolicitudAuditorio()
    End Sub

    Protected Sub chkFaltanteDesap_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFaltanteDesap.CheckedChanged
        CargarDatosCursos()
    End Sub

    Private Sub CargarDatosCursos()
        Try

            'Response.Write(chkFaltanteDesap.Checked)
            'Response.Write("<br>1")
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'CambioGo
            'If Me.chkFaltanteDesap.Checked Then

            'ClsFunciones.LlenarListas(Me.dpCurso, obj.TraerDataTable("ConsultarCursoProgramado", 1, Me.dpCodigo_pes.SelectedValue, Me.dpCodigo_cac.SelectedValue, "", ""), "codigo_cur", "nombre_cur_go", "--Seleccione el curso--")
            'Else
            'CambioGo
            'ClsFunciones.LlenarListas(Me.dpCurso, obj.TraerDataTable("ConsultarCursoProgramado", 1, Me.dpCodigo_pes.SelectedValue, Me.dpCodigo_cac.SelectedValue, "", ""), "codigo_cur", "nombre_cur_go", "--Seleccione el curso--")
            'End if
            ClsFunciones.LlenarListas(Me.dpCurso, obj.TraerDataTable("ConsultarCursoProgramado", 1, Me.dpCodigo_pes.SelectedValue, Me.dpCodigo_cac.SelectedValue, "", Me.cboCecos.SelectedValue), "codigo_cur", "nombre_cur_go", "--Seleccione el curso--")
            'End If

            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelarLho_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarLho.Click
        ActivarFormularioHorario(False)
    End Sub

    Protected Sub btnGuardarLho_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarLho.Click
        Try
            Dim obj As New ClsConectarDatos
            Dim lblResultado As Boolean
            Dim codigo As Integer = CInt(Me.txtcodhor.Value)
            Dim descripcion As String = Me.txtdescripcionhor.Value.ToString
            'Response.Write(codigo & ", " & descripcion)
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            lblResultado = obj.Ejecutar("ACAD_horarioReg", "A", codigo, descripcion)
            obj.CerrarConexion()
            If lblResultado Then
                ActivarFormularioHorario(False)
                Cargarhorario()
            Else
                Response.Write("Error")
            End If
        Catch ex As Exception

        End Try
    End Sub

  

    Protected Sub dpCurso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpCurso.SelectedIndexChanged
        Try
            Me.lblCurso.text = dpCurso.selecteditem.text
        Catch ex As Exception

        End Try
    End Sub
End Class

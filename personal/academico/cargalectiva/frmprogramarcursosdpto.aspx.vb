﻿
Partial Class frmprogramarcursosdpto
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Dim tbl As Data.DataTable

            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Session("id_per")

            '=================================
            'Permisos por Escuela
            '=================================
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            If codigo_tfu = 1 Or codigo_tfu = 7 Or codigo_tfu = 16 Then
                tbl = obj.TraerDataTable("ConsultarDepartamentoAcademico", "TO", 0)
            Else
                tbl = obj.TraerDataTable("consultaraccesorecurso", 11, codigo_usu, 0, 0)
            End If
            '=================================
            'Llenar combos
            '=================================
            ClsFunciones.LlenarListas(Me.dpCodigo_Dac, tbl, 0, 1, "--Seleccione el Dpto--")
            ClsFunciones.LlenarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "UCI", 0), "codigo_cac", "descripcion_cac")
            tbl.Dispose()
            obj.CerrarConexion()
            obj = Nothing

            Me.txtInicio.Attributes.Add("OnKeyDown", "return false")
            Me.txtFin.Attributes.Add("OnKeyDown", "return false")
            Me.txtRetiro.Attributes.Add("OnKeyDown", "return false")
            Me.txtGrupos.Attributes.Add("onKeyPress", "validarnumero()")
            Me.txtVacantes.Attributes.Add("onKeyPress", "validarnumero()")

        End If
    End Sub
    Private Sub CargarDetalleGrupoHorario(ByVal tipo As String, ByVal ID As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tblcurso As Data.DataTable
        obj.AbrirConexion()
        tblcurso = obj.TraerDataTable("CAR_ConsultarProcesoProgramacionAcademicaXDpto", tipo, ID, Me.dpCodigo_Dac.SelectedValue, Me.dpCodigo_cac.SelectedValue, 0)

        Me.lblFecha.Text = tblcurso.Rows(0).Item("fechadoc_cup")
        Me.txtInicio.Text = tblcurso.Rows(0).Item("fechainicio_cup")
        Me.txtFin.Text = tblcurso.Rows(0).Item("fechafin_cup")
        Me.txtRetiro.Text = tblcurso.Rows(0).Item("fecharetiro_cup")
        Me.txtVacantes.Text = tblcurso.Rows(0).Item("vacantes_cup")
        Me.dpestado_cup.SelectedValue = tblcurso.Rows(0).Item("estado_cup")
        Me.lblOperador.Text = tblcurso.Rows(0).Item("login_per")
        Me.txtGrupoHor_cup.Text = tblcurso.Rows(0).Item("grupohor_cup")
        If tipo = 3 Then
            Me.ChkPrimerCiclo.Checked = tblcurso.Rows(0).Item("soloPrimerCiclo_cup")
        ElseIf tipo = 0 Then
            Me.ChkPrimerCiclo.Checked = False
        End If
        Me.hdcodigo_cup.Value = ID
        Me.txtGrupos.Text = 1
        tblcurso.Dispose()

        Me.grwEquivalencias.DataBind()
        Me.fraEquivalencias.Visible = False
        Me.fraDetalleGrupoHorario.Visible = True

        Me.txtGrupos.Enabled = tipo = 0
        Me.dpestado_cup.Enabled = tipo = 3
        Me.lblgrupohor_cup.Visible = tipo = 3
        Me.txtGrupoHor_cup.Visible = tipo = 3
        cmdGuardar.Text = IIf(tipo = 3, "Actualizar", "Guardar")

        'Mostrar cursos disponibles para PROGRAMAR
        If ID = 0 Then
            Me.dpCurso.Visible = True
            Me.lblnombre_cur.Visible = False
            Me.cmdDesagrupar.visible = False
            ClsFunciones.LlenarListas(Me.dpCurso, obj.TraerDataTable("CAR_ConsultarProcesoProgramacionAcademicaXDpto", "-1", Me.dpCodigo_Dac.SelectedValue, Me.dpCodigo_cac.SelectedValue, "", ""), "nombre_cur", "nombre_cur", "--Seleccione el curso--")
        Else
            'Mostrar cursos agrupados
            Me.dpCurso.Visible = False
            Me.lblnombre_cur.Visible = True
            Me.grwEquivalencias.DataSource = obj.TraerDataTable("CAR_ConsultarCursosProgramadosHIJO", Me.hdcodigo_cup.value)
            Me.grwEquivalencias.DataBind()
            Me.fraEquivalencias.Visible = (Me.grwEquivalencias.Rows.Count > 0)
            Me.cmdGuardar.enabled = Me.grwEquivalencias.visible
            Me.cmdDesagrupar.visible = True

            'Si es complementarios MOSTRAR TODO.
            If Me.dpCodigo_Dac.SelectedValue = 9 Then
                ClsFunciones.LlenarListas(Me.dpCodigo_per, obj.TraerDataTable("ConsultarDocente", "TD", Me.dpCodigo_Dac.SelectedValue, 0), "codigo_per", "docente", "--Seleccione el Profesor.--")
            Else
                'ClsFunciones.LlenarListas(Me.dpCodigo_per, obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 5, Me.dpCodigo_Dac.SelectedValue, Me.hdCodigo_Cup.Value, Me.dpCodigo_cac.SelectedValue, 0), "codigo_per", "profesor", "--Seleccione el Profesor adscrito al Dpto.--")
                'YPEREZ 17.01.20 Cambiar lista de docentes a adscripción docente
                ClsFunciones.LlenarListas(Me.dpCodigo_per, obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 17, Me.dpCodigo_cac.SelectedValue, Me.hdCodigo_Cup.Value, 0, Request.QueryString("ctf")), "codigo_per", "docente", "--Seleccione el Profesor adscrito al Dpto.Acad--")
            End If

            'Mostrar Profesores Sugeridos
            'ClsFunciones.LlenarListas(Me.blstProfesoresSugeridos, obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 6, Me.dpCodigo_Dac.SelectedValue, Me.hdCodigo_Cup.Value, Me.dpCodigo_cac.SelectedValue), "codigo_per", "profesor")

            'Mostrar Profesores Asignados
            Me.grwProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 4, Me.hdCodigo_Cup.Value, 0, 0, 0)
            Me.grwProfesores.DataBind()
            Me.fraProfesores.Visible = True
        End If
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click

        If CInt(Me.txtVacantes.Text) > CInt(Me.hddCapacidadMin.Value) Then
            ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, Me.GetType(), "Programar", "alert('No puede asignar mas vacantes debido a la disponibilidad de aula asignada maxima es " & Me.hddCapacidadMin.Value.ToString & "');", True)
            Exit Sub
        End If

        Me.cmdGuardar.Enabled = False
        Dim Fila As GridViewRow
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim codigo_usu As Integer = Session("id_per")
        Dim valoresdevueltos(1) As String
        Dim EscuelaPadre As Boolean = True
        '==================================
        'Validar Controles
        '==================================
        If Me.txtGrupos.Text = "" Or Me.txtGrupoHor_cup.Text = "" Or Me.txtVacantes.Text = "" Then Exit Sub

        '==================================
        'Grabar Programación
        '==================================
        Try
            obj.IniciarTransaccion()
            If Me.cmdGuardar.Text = "Guardar" Then
                'Indicar Nro de Grupos
                For j As Int16 = 1 To Me.txtGrupos.Text
                    'Recorrer escuelas marcadas
                    For I As Int16 = 0 To Me.grwEquivalencias.Rows.Count - 1
                        Fila = Me.grwEquivalencias.Rows(I)
                        If Fila.RowType = DataControlRowType.DataRow Then
                            If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                                'Programar Padre
                                If EscuelaPadre = True Then
                                    'Response.Write("1. CAR_AgregarCursoProgramado_Individual 0,'" & Me.dpCodigo_cac.SelectedValue & "','" & Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_pes") & "','" & Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_cur") & "'," & DBNull.Value & ",'" & Me.txtVacantes.Text & "'," & "'C'" & ",'" & CDate(Me.txtInicio.Text) & "','" & CDate(Me.txtFin.Text) & "','" & CDate(Me.txtRetiro.Text) & "','" & codigo_usu & "'," & "''" & ",'" & Me.dpCodigo_Dac.SelectedValue & "','" & ChkPrimerCiclo.Checked & "', 0")
                                    obj.Ejecutar("CAR_AgregarCursoProgramado_Individual", 0, Me.dpCodigo_cac.SelectedValue, Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_pes"), Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_cur"), DBNull.Value, Me.txtVacantes.Text, "C", CDate(Me.txtInicio.Text), CDate(Me.txtFin.Text), CDate(Me.txtRetiro.Text), codigo_usu, "", Me.dpCodigo_Dac.SelectedValue, IIf(ChkPrimerCiclo.Checked = False, 0, 1), 0).copyto(valoresdevueltos, 0)
                                    EscuelaPadre = False
                                ElseIf (valoresdevueltos(0) <> "0" And valoresdevueltos(0) <> "" And EscuelaPadre = False) Then 'Programar HIJO
                                    'Response.Write("2. CAR_AgregarCursoProgramado_Individual 0,'" & Me.dpCodigo_cac.SelectedValue & "','" & Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_pes") & "','" & Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_cur") & "'," & DBNull.Value & ",'" & Me.txtVacantes.Text & "'," & "'C'" & ",'" & CDate(Me.txtInicio.Text) & "','" & CDate(Me.txtFin.Text) & "','" & CDate(Me.txtRetiro.Text) & "','" & codigo_usu & "'," & "''" & ",'" & Me.dpCodigo_Dac.SelectedValue & "','" & ChkPrimerCiclo.Checked & "', 0")
                                    obj.Ejecutar("CAR_AgregarCursoProgramado_Individual", valoresdevueltos(0), Me.dpCodigo_cac.SelectedValue, Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_pes"), Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_cur"), "AUTOMATICO", Me.txtVacantes.Text, "C", CDate(Me.txtInicio.Text), CDate(Me.txtFin.Text), CDate(Me.txtRetiro.Text), codigo_usu, "", Me.dpCodigo_Dac.SelectedValue, IIf(ChkPrimerCiclo.Checked = False, 0, 1), 0)
                                    'EscuelaPadre = False
                                Else
                                    obj.AbortarTransaccion()
                                    'ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, "string".GetType(), "Programar", "alert('Ocurrió un Error interno en el sistema\nConctáctese con desarrollosistemas@usat.edu.pe');", True)
                                    Exit Sub
                                End If
                            End If
                        End If
                    Next
                    EscuelaPadre = True
                Next
            Else
                obj.Ejecutar("ModificarCursoProgramado", Me.hdcodigo_cup.Value, Me.txtGrupoHor_cup.Text.Trim, Me.dpestado_cup.SelectedValue, Me.txtVacantes.Text, CDate(Me.txtInicio.Text), CDate(Me.txtFin.Text), CDate(Me.txtRetiro.Text), codigo_usu, " ", Me.dpCodigo_Dac.SelectedValue, ChkPrimerCiclo.Checked, 0).copyto(valoresdevueltos, 0)
            End If
            obj.TerminarTransaccion()

            '==================================
            'Actualizar Controles
            '==================================
            obj.AbrirConexion()
            Me.grwGruposProgramados.DataSource = obj.TraerDataTable("CAR_ConsultarCursosProgramadosXDpto", 0, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_Dac.SelectedValue, 0)
            Me.grwGruposProgramados.DataBind()
            Me.cmdAgrupar.Visible = Me.grwGruposProgramados.Rows.Count > 0
            obj.CerrarConexion()
            obj = Nothing

            Me.LimpiarDatos(True)
            'ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, "string".GetType(), "Programar", "alert('" & valoresdevueltos(0) & "');", True)
        Catch ex As Exception
            obj.AbortarTransaccion()
            obj = Nothing
            hdcodigo_cup.Value = 0
            ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, "string".GetType(), "ErrorGuardar", "alert('Error:" & ex.Message & "');", True)
        End Try
    End Sub
    Private Sub LimpiarDatos(ByVal estado As Boolean)
        Me.fraEquivalencias.visible = False
        Me.fraDetalleGrupoHorario.Visible = False
        Me.fraProfesores.visible = False
        Me.fraGruposProgramados.Visible = True
        Me.grwEquivalencias.DataBind()
        Me.dpCurso.DataBind()
        Me.hdCodigo_cup.value = 0
    End Sub
    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        Me.cmdAgregar.Enabled = False
        Me.fraGruposProgramados.Visible = False
        CargarDetalleGrupoHorario("0", 0)
        Me.cmdAgregar.Enabled = True
        Me.hddCapacidadMin.Value = 1000
    End Sub
    Protected Sub grwGruposProgramados_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwGruposProgramados.RowCommand
        Dim tblCro As Data.DataTable
        Me.hdcodigo_cup.Value = 0
        If (e.CommandName = "editar") Then
            Me.fraGruposProgramados.Visible = False
            Me.lblnombre_cur.Text = Me.grwGruposProgramados.Rows(Convert.ToInt32(e.CommandArgument).ToString).Cells(1).Text
            Me.hdcodigo_cup.Value = Convert.ToString(Me.grwGruposProgramados.DataKeys(Convert.ToInt32(e.CommandArgument)).Value)

            Me.CargarDetalleGrupoHorario(3, Me.hdCodigo_cup.value)
            ConsultarAmbienteAsignado()
            '=================================
            'Validar cronograma para la asiganción de carga
            '=================================
            Dim objCro As New ClsConectarDatos
            objCro.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objCro.AbrirConexion()
            tblCro = objCro.TraerDataTable("ConsultarCronograma", "PC", Me.dpCodigo_cac.SelectedValue)
            objCro.CerrarConexion()
            If tblCro.Rows.Count = 0 Then
                cmdAsignarProfesor.Enabled = False
            Else
                cmdAsignarProfesor.Enabled = True
            End If
            objCro = Nothing
        End If
    End Sub

    Protected Sub ConsultarAmbienteAsignado()
        Dim objCnx As New ClsConectarDatos
        Dim Datos As New Data.DataTable
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        objCnx.AbrirConexion()
        Datos = objCnx.TraerDataTable("ACAD_ConsultarAmbienteAsignadoCursoProgramado", Me.hdcodigo_cup.Value)
        objCnx.CerrarConexion()
        If Datos.Rows.Count > 0 Then
            Me.hddCapacidadMin.Value = Datos.Rows(0).Item("capacidadMin")
        Else
            Me.hddCapacidadMin.Value = 1000
        End If
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
            e.Row.Cells(8).Text = IIf(fila.Row("estado_cup") = False, "Cerrado", "Abierto")
            CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")

            'Cargar Profesores y Horario
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim gr As BulletedList = CType(e.Row.FindControl("lstProfesores"), BulletedList)
            gr.DataSource = obj.TraerDataTable("CAR_ConsultarCursosProgramadosXDpto", 1, Me.dpcodigo_cac.selectedvalue, Me.dpcodigo_dac.selectedvalue, fila.row("codigo_cup"))
            gr.DataBind()
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub
    Protected Sub grwEquivalencias_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwEquivalencias.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")
        End If
    End Sub
    Protected Sub grwGruposProgramados_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwGruposProgramados.RowDeleting
        Dim obj As New ClsConectarDatos
        Dim valoresdevueltos(1) As String
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("EliminarCursoProgramado", grwGruposProgramados.DataKeys(e.RowIndex).Values("codigo_cup"), 0).copyto(valoresdevueltos, 0)
        'Cargar los Cursos Programados
        Me.grwGruposProgramados.DataSource = obj.TraerDataTable("CAR_ConsultarCursosProgramadosXDpto", 0, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_Dac.SelectedValue, 0)
        Me.grwGruposProgramados.DataBind()
        obj.CerrarConexion()
        obj = Nothing
        e.Cancel = True
        Me.lblGrupos.Text = "Lista de Grupos Horario Programados (" & Me.grwGruposProgramados.Rows.Count & ")"
        Me.cmdAgrupar.Visible = Me.grwGruposProgramados.Rows.Count > 0
        ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, "string".GetType(), "EliminarProgramacion", "alert('" & valoresdevueltos(0) & "');", True)
    End Sub
    Protected Sub dpCurso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpCurso.SelectedIndexChanged
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Mostrar Equivalencias para Escuelas Regulares y Complementarios: Modo AGREGAR
        'Response.Write(Me.dpCurso.SelectedValue)
        Me.grwEquivalencias.DataSource = obj.TraerDataTable("CAR_ConsultarCursoEquivalenteAProgramarXDpto", Me.dpCodigo_Dac.SelectedValue, Me.dpCurso.SelectedValue, Me.dpCodigo_cac.SelectedValue)
        Me.grwEquivalencias.DataBind()
        Me.fraEquivalencias.Visible = (Me.grwEquivalencias.Rows.Count > 0)
        Me.cmdGuardar.enabled = Me.grwEquivalencias.visible
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub cmdAgrupar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgrupar.Click
        Dim codigo_cupPadre As Integer
        Dim Fila As GridViewRow
        Dim codigo_usu As Integer = Session("id_per")
        Dim EscuelaPadre As Boolean = True
        Dim obj As New ClsConectarDatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        For I As Int16 = 0 To Me.grwGruposProgramados.Rows.Count - 1
            Fila = Me.grwGruposProgramados.Rows(I)
            If Fila.RowType = DataControlRowType.DataRow Then
                If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                    'Programar Padre
                    If EscuelaPadre = True Then
                        codigo_cupPadre = Me.grwGruposProgramados.DataKeys.Item(Fila.RowIndex).Values("codigo_cup")
                        EscuelaPadre = False
                    ElseIf codigo_cupPadre > 0 Then
                        obj.Ejecutar("CAR_AgruparCursosProgramados", codigo_cupPadre, Me.grwGruposProgramados.DataKeys.Item(Fila.RowIndex).Values("codigo_cup"), codigo_usu)
                        EscuelaPadre = False
                    Else
                        Exit Sub
                    End If
                End If
            End If
        Next
        'Cargar cursos programados
        Me.grwGruposProgramados.DataSource = obj.TraerDataTable("CAR_ConsultarCursosProgramadosXDpto", 0, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_Dac.SelectedValue, 0)
        Me.grwGruposProgramados.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub cmdDesagrupar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDesagrupar.Click
        Dim codigo_cupPadre As Integer
        Dim Fila As GridViewRow
        Dim codigo_usu As Integer = Session("id_per")
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        For I As Int16 = 0 To Me.grwEquivalencias.Rows.Count - 1
            Fila = Me.grwEquivalencias.Rows(I)
            If Fila.RowType = DataControlRowType.DataRow Then
                If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                    codigo_cupPadre = Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_cup")
                    obj.Ejecutar("CAR_DesAgruparCursosProgramados", codigo_cupPadre, Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_cup"), codigo_usu)
                End If
            End If
        Next
        'Cargar cursos programados equivalentes
        Me.grwEquivalencias.DataSource = obj.TraerDataTable("CAR_ConsultarCursosProgramadosHIJO", Me.hdcodigo_cup.value)
        Me.grwEquivalencias.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub cmdAsignarProfesor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAsignarProfesor.Click
        If dpcodigo_per.selectedValue <> -2 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("CAR_AgregarCargaAcademica", Me.dpCodigo_per.SelectedValue, Me.hdcodigo_cup.Value, Session("id_per"), Request.QueryString("mod"), System.DBNull.Value)
            obj.CerrarConexion()
            obj = Nothing
            Me.CargarDetalleGrupoHorario(3, Me.hdCodigo_cup.value)
        End If
    End Sub
    Protected Sub grwProfesores_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwProfesores.RowDeleting
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("EliminarCargaAcademica", grwProfesores.DataKeys(e.RowIndex).Values("codigo_per"), Me.hdcodigo_cup.Value, Session("id_per"))
        'Cargar denuevo
        Me.grwProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 4, Me.hdCodigo_Cup.Value, 0, 0, 0)
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
            e.Row.Cells(0).Text = "<a target='_blank' href='../horarios/vsthorariodocente.asp?modo=A&codigo_per=" & fila.Row("codigo_per") & "&codigo_Cac=" & Me.dpCodigo_cac.SelectedValue & "'>" & fila.Row("docente") & "</a>"
        End If
    End Sub
    Protected Sub cmdVer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVer.Click

        LimpiarDatos(True)
        If Me.dpCodigo_Dac.SelectedValue <> -2 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            '=================================
            'Cargar Grupos Programados
            '=================================
            Me.grwGruposProgramados.DataSource = obj.TraerDataTable("CAR_ConsultarCursosProgramadosXDpto", 0, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_Dac.SelectedValue, 0)
            Me.grwGruposProgramados.DataBind()
            obj.CerrarConexion()
            obj = Nothing
            Me.cmdAgrupar.visible = Me.grwGruposProgramados.rows.count > 0
            If Me.grwGruposProgramados.Rows.Count = 0 Then
                Me.lblGrupos.Text = "Programar Nuevos Grupos Horario"
            Else
                Me.lblGrupos.Text = "Lista de Grupos Horario Programados"
            End If
        End If
    End Sub
 
    Protected Sub grwGruposProgramados_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwGruposProgramados.SelectedIndexChanged

    End Sub
End Class

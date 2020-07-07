
Partial Class frmmatricula_evento
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")
            '=================================
            'Permisos por CECO
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
            objfun = Nothing
            obj = Nothing

            Me.txtInicio.Attributes.Add("OnKeyDown", "return false")
            Me.txtFin.Attributes.Add("OnKeyDown", "return false")
            Me.txtRetiro.Attributes.Add("OnKeyDown", "return false")
            Me.txtVacantes.Attributes.Add("onKeyPress", "validarnumero()")
        End If
    End Sub
    Private Sub CargarDetalleGrupoHorario(ByVal tipo As String, ByVal ID As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tblcurso As Data.DataTable
        obj.AbrirConexion()
        tblcurso = obj.TraerDataTable("CAR_ConsultarProcesoProgramacionAcademica", tipo, Me.dpCodigo_pes.SelectedValue, ID, Me.dpCodigo_cac.SelectedValue, 0)

        Me.lblFecha.Text = tblcurso.Rows(0).Item("fechadoc_cup")
        Me.txtInicio.Text = tblcurso.Rows(0).Item("fechainicio_cup")
        Me.txtFin.Text = tblcurso.Rows(0).Item("fechafin_cup")
        Me.txtRetiro.Text = tblcurso.Rows(0).Item("fecharetiro_cup")
        Me.txtVacantes.Text = tblcurso.Rows(0).Item("vacantes_cup")
        Me.dpestado_cup.SelectedValue = tblcurso.Rows(0).Item("estado_cup")
        Me.lblOperador.Text = tblcurso.Rows(0).Item("login_per")
        Me.txtGrupoHor_cup.Text = tblcurso.Rows(0).Item("grupohor_cup")
        Me.hdcodigo_cup.Value = ID
        tblcurso.Dispose()

        Me.grwAlumnosPlan.DataBind()
        Me.grwProfesores.DataBind()
        Me.fraAlumnosPlan.Visible = True
        Me.fraDetalleGrupoHorario.Visible = True
        Me.fraProfesores.Visible = True

        Me.dpestado_cup.Enabled = tipo = 3
        Me.lblgrupohor_cup.Visible = tipo = 3
        Me.txtGrupoHor_cup.Visible = tipo = 3
        cmdGuardar.Text = IIf(tipo = 3, "Actualizar", "Guardar")

        'Mostrar cursos disponibles para PROGRAMAR
        If ID = 0 Then
            Me.dpCurso.Visible = True
            Me.dpVersion.Visible = True
            Me.lblnombre_cur.Visible = False
            Me.cmdAsignarProfesor.Visible = False
        Else
            'Mostrar alumnos Matriculados
            Me.cmdGuardar.Enabled = True
            Me.cmdAsignarProfesor.Visible = True
            Me.dpCodigo_per.Visible = True
            Me.lblnombre_cur.Visible = True
            Me.dpCurso.Visible = False
            Me.dpVersion.Visible = False
            Me.grwAlumnosPlan.DataSource = obj.TraerDataTable("ConsultarDatosProgramaEspecial", 8, Me.hdcodigo_cup.Value, Me.dpCodigo_pes.SelectedValue, 0, 0)
            Me.grwAlumnosPlan.DataBind()
            Me.fraAlumnosPlan.Visible = (Me.grwAlumnosPlan.Rows.Count > 0)

            'Mostrar Profesores Asignados
            Me.grwProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 4, Me.hdcodigo_cup.Value, 0, 0, 0)
            Me.grwProfesores.DataBind()
        End If
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim Fila As GridViewRow
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim codigo_usu As Integer = Request.QueryString("id")
        Dim valoresdevueltos(1) As String

        Me.cmdGuardar.Enabled = False
        '==================================
        'Grabar Programación y Matricula
        '==================================
        Try
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
                obj.Ejecutar("CAR_AgregarCursoProgramado_Individual", 0, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.dpCurso.SelectedValue, DBNull.Value, Me.txtVacantes.Text, "C", CDate(Me.txtInicio.Text), CDate(Me.txtFin.Text), CDate(Me.txtRetiro.Text), codigo_usu, "", 0, 0).copyto(valoresdevueltos, 0)

                If (valoresdevueltos(0) <> "0" And valoresdevueltos(0) <> "") Then
                    '==================================
                    'Asignar Carga Académica
                    '==================================
                    obj.Ejecutar("CAR_AgregarCargaAcademica", Me.dpCodigo_per.SelectedValue, valoresdevueltos(0), codigo_usu, System.DBNull.Value)
                    '==================================
                    'Matricular a los estudiantes
                    '==================================
                    For I As Int16 = 0 To Me.grwAlumnosPlan.Rows.Count - 1
                        Fila = Me.grwAlumnosPlan.Rows(I)
                        If Fila.RowType = DataControlRowType.DataRow Then
                            If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                                obj.Ejecutar("AgregarMatriculaWeb", "A", Me.grwAlumnosPlan.DataKeys.Item(Fila.RowIndex).Values("codigo_alu"), dpCodigo_pes.SelectedValue, Me.dpCodigo_cac.SelectedValue, "P", "Matricula por MPE", valoresdevueltos(0).ToString & ",", "1,", "N", "P", "MAT", codigo_usu, "PC", System.DBNull.Value, System.DBNull.Value, System.DBNull.Value)
                            End If
                        End If
                    Next
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
                obj.Ejecutar("ModificarCursoProgramado", Me.hdcodigo_cup.Value, Me.txtGrupoHor_cup.Text.Trim, Me.dpestado_cup.SelectedValue, Me.txtVacantes.Text, CDate(Me.txtInicio.Text), CDate(Me.txtFin.Text), CDate(Me.txtRetiro.Text), codigo_usu, " ", Me.dpCodigo_pes.SelectedValue, 0) '.copyto(valoresdevueltos, 0)
            End If
            '==================================
            'Actualizar Controles
            '==================================
            Me.grwGruposProgramados.DataSource = obj.TraerDataTable("ConsultarDatosProgramaEspecial", 7, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, 0, 0)
            Me.grwGruposProgramados.DataBind()

            obj.CerrarConexion()
            obj = Nothing

            Me.LimpiarDatos(True)
            'ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, "string".GetType(), "Programar", "alert('" & valoresdevueltos(0) & "');", True)
        Catch ex As Exception
            obj = Nothing
            hdcodigo_cup.Value = 0
            'ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, "string".GetType(), "Programar", "alert('Ocurrió un Error interno en el sistema\nConctáctese con desarrollosistemas@usat.edu.pe');", True)
        End Try
    End Sub
    Private Sub LimpiarDatos(ByVal estado As Boolean)
        Me.fraAlumnosPlan.Visible = False
        Me.fraDetalleGrupoHorario.Visible = False
        Me.fraProfesores.Visible = False
        Me.fraGruposProgramados.Visible = estado
        Me.grwAlumnosPlan.DataBind()
        Me.grwProfesores.DataBind()
        Me.dpCurso.SelectedValue = -1
        Me.dpVersion.SelectedValue = -1
        Me.dpCodigo_per.SelectedValue = -1
        Me.hdcodigo_cup.Value = 0
    End Sub
    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        Me.cmdAgregar.Enabled = False
        Me.fraGruposProgramados.Visible = False
        CargarDetalleGrupoHorario("7", 0)
        Me.cmdAgregar.Enabled = True
    End Sub
    Protected Sub grwGruposProgramados_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwGruposProgramados.RowCommand
        Me.hdcodigo_cup.Value = 0
        If (e.CommandName = "editar") Then
            Me.fraGruposProgramados.Visible = False
            Me.lblnombre_cur.Text = Me.grwGruposProgramados.Rows(Convert.ToInt32(e.CommandArgument).ToString).Cells(0).Text
            Me.hdcodigo_cup.Value = Convert.ToString(Me.grwGruposProgramados.DataKeys(Convert.ToInt32(e.CommandArgument)).Value)
            Me.CargarDetalleGrupoHorario(3, Me.hdcodigo_cup.Value)
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
            e.Row.Cells(7).Text = IIf(fila.Row("estado_cup") = False, "Cerrado", "Abierto")

            'Cargar Profesores y Horario
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim gr As BulletedList = CType(e.Row.FindControl("lstProfesores"), BulletedList)
            gr.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 4, fila.Row("codigo_cup"), Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, 0)
            gr.DataBind()
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub
    Protected Sub grwAlumnosPlan_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwAlumnosPlan.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")
            e.Row.Cells(1).Text = e.Row.RowIndex + 1
            e.Row.Cells(4).Text = IIf(fila.Row("estadoactual_alu") = 0, "Inactivo", "Activo")
            e.Row.Cells(5).Text = IIf(fila.Row("estadodeuda_alu") = 1, "Con Deuda", "Sin Deuda")
        End If
    End Sub
    Protected Sub grwGruposProgramados_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwGruposProgramados.RowDeleting
        Dim obj As New ClsConectarDatos
        Dim valoresdevueltos(1) As String
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("EliminarCursoProgramado", grwGruposProgramados.DataKeys(e.RowIndex).Values("codigo_cup"), 0).copyto(valoresdevueltos, 0)
        'Cargar los Cursos Programados
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
            obj.Ejecutar("CAR_AgregarCargaAcademica", Me.dpCodigo_per.SelectedValue, Me.hdcodigo_cup.Value, Request.QueryString("id"), System.DBNull.Value)
            obj.CerrarConexion()
            obj = Nothing
            Me.CargarDetalleGrupoHorario(3, Me.hdcodigo_cup.Value)
        End If
    End Sub
    Protected Sub grwProfesores_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwProfesores.RowDeleting
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("EliminarCargaAcademica", grwProfesores.DataKeys(e.RowIndex).Values("codigo_per"), Me.hdcodigo_cup.Value, Request.QueryString("id"))
        'Cargar denuevo
        Me.grwProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 4, Me.hdcodigo_cup.Value, 0, 0, 0)
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
            e.Row.Cells(0).Text = "<a target='_blank' href='../../../personal/academico/horarios/vsthorariodocente.asp?modo=A&codigo_per=" & fila.Row("codigo_per") & "&codigo_Cac=" & Me.dpCodigo_cac.SelectedValue & "'>" & fila.Row("docente") & "</a>"
        End If
    End Sub
    Protected Sub cmdVer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVer.Click
        LimpiarDatos(False)
        If Me.dpCodigo_pes.SelectedValue <> -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            '=================================
            'Cargar Grupos Programados
            '=================================
            Me.grwGruposProgramados.DataSource = obj.TraerDataTable("ConsultarDatosProgramaEspecial", 7, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, 0, 0)
            Me.grwGruposProgramados.DataBind()
            'Cargar los cursos del Plan de Estudios
            ClsFunciones.LlenarListas(Me.dpCurso, obj.TraerDataTable("ConsultarCursoProgramado", 1, Me.dpCodigo_pes.SelectedValue, Me.dpCodigo_cac.SelectedValue, "", ""), "codigo_cur", "nombre_cur", "--Seleccione el curso--")

            'Cargar las versiones de un Plan de Estudios
            ClsFunciones.LlenarListas(Me.dpVersion, obj.TraerDataTable("ConsultarDatosProgramaEspecial", 3, Me.dpCodigo_pes.SelectedValue, Me.dpCodigo_cac.SelectedValue, 0, 0), "edicionProgramaEspecial_Alu", "edicionProgramaEspecial_Alu", "--Seleccione la versión--")
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
    Protected Sub dpVersion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpVersion.SelectedIndexChanged
        Me.grwAlumnosPlan.Visible = False
        If dpVersion.SelectedValue <> -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Me.grwAlumnosPlan.DataSource = obj.TraerDataTable("ConsultarDatosProgramaEspecial", 9, Me.dpCodigo_pes.SelectedValue, Me.dpVersion.SelectedValue, 0, 0)
            Me.grwAlumnosPlan.DataBind()
            Me.cmdGuardar.Enabled = Me.grwAlumnosPlan.Rows.Count > 0
            Me.txtVacantes.Text = Me.grwAlumnosPlan.Rows.Count
            Me.grwAlumnosPlan.Visible = True
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub
    Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCecos.SelectedIndexChanged
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
End Class

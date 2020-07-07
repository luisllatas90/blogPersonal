
Partial Class frmmatricula_cursoprogramado
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

            obj.CerrarConexion()
            objfun = Nothing
            obj = Nothing
        End If
    End Sub
    Private Sub CargarDetalleGrupoHorario(ByVal tipo As String, ByVal ID As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.grwAlumnosPlan.DataBind()
        Me.fraAlumnosPlan.Visible = True

        'Mostrar cursos disponibles para PROGRAMAR
        Me.grwAlumnosPlan.DataSource = obj.TraerDataTable("EVE_ConsultarAlumnoXCentroCostos", Me.cboCecos.SelectedValue, Me.hdcodigo_cup.Value)
        Me.grwAlumnosPlan.DataBind()
        Me.fraAlumnosPlan.Visible = (Me.grwAlumnosPlan.Rows.Count > 0)

        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim Fila As GridViewRow
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim codigo_usu As Integer = Request.QueryString("id")
        Dim valoresdevueltos(1) As String

        'Me.cmdGuardar.Enabled = False
        Try
            Dim marcados As Int16 = 0
            For I As Int16 = 0 To Me.grwAlumnosPlan.Rows.Count - 1
                Fila = Me.grwAlumnosPlan.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        marcados = marcados + 1
                    End If
                End If
            Next
            If marcados = 0 Then
                Page.RegisterStartupScript("falta", "alert('Debe marcar a los estudiantes que se matricularán en el grupo horario');")
                Exit Sub
            End If

            obj.AbrirConexion()
            '==================================
            'Matricular a los estudiantes
            '==================================

            For I As Int16 = 0 To Me.grwAlumnosPlan.Rows.Count - 1
                Fila = Me.grwAlumnosPlan.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        obj.Ejecutar("AgregarMatriculaWeb", "A", Me.grwAlumnosPlan.DataKeys.Item(Fila.RowIndex).Values("codigo_alu"), dpCodigo_pes.SelectedValue, Me.dpCodigo_cac.SelectedValue, "P", "Matricula por MPE", hdcodigo_cup.Value & ",", "1,", "N", "P", "MAT", codigo_usu, "PC", System.DBNull.Value, System.DBNull.Value, 1, System.DBNull.Value)
                    End If
                End If
            Next

            '==================================
            'Actualizar Controles
            '==================================
            Me.grwGruposProgramados.DataSource = obj.TraerDataTable("ConsultarDatosProgramaEspecial", 7, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.cboCecos.SelectedValue, 0)
            Me.grwGruposProgramados.DataBind()

            obj.CerrarConexion()
            obj = Nothing

            Me.LimpiarDatos(True)
            Page.RegisterStartupScript("ok", "<script>alert('Las matrículas se han registrado correctamente');</script>")
            cmdVer_Click(sender, e)
        Catch ex As Exception
            obj = Nothing
            hdcodigo_cup.Value = 0
            Page.RegisterStartupScript("error", "alert('Ocurrió un error en el registro" & Err.Description & "');")
        End Try
    End Sub
    Private Sub LimpiarDatos(ByVal estado As Boolean)
        Me.fraAlumnosPlan.Visible = False
        Me.fraGruposProgramados.Visible = estado
        Me.grwAlumnosPlan.DataBind()
        Me.hdcodigo_cup.Value = 0
    End Sub
    Protected Sub grwGruposProgramados_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwGruposProgramados.RowCommand
        Me.hdcodigo_cup.Value = 0
        If (e.CommandName = "editar") Then
            Me.fraGruposProgramados.Visible = False
            Me.hdcodigo_cup.Value = Convert.ToString(Me.grwGruposProgramados.DataKeys(Convert.ToInt32(e.CommandArgument)).Value)
            Dim FilaSeleccionada As GridViewRow = grwGruposProgramados.Rows(Convert.ToInt32(e.CommandArgument))
            Me.lblnombre_cur.Text = FilaSeleccionada.Cells(0).Text & " (Grupo: " & FilaSeleccionada.Cells(2).Text & ")"

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
            e.Row.Cells(5).ForeColor = IIf(fila.Row("estadodeuda_alu") = 1, Drawing.Color.Red, Drawing.Color.Black)
            e.Row.Cells(6).Text = IIf(fila.Row("matriculado") = 0, "No", "Sí")

            If CInt(fila.Row("matriculado")) > 0 Or fila.Row("estadoactual_alu") = 0 Then  'Or fila.Row("estadodeuda_alu") = 1 ' se quitó validación por deuda
                e.Row.Cells(0).Text = ""
            End If
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
            Me.grwGruposProgramados.DataSource = obj.TraerDataTable("EVE_ConsultarProgramacionAcademica", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.cboCecos.SelectedValue)
            Me.grwGruposProgramados.DataBind()

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
            'Llenar Planes de Estudio
            '=================================
            objfun.CargarListas(Me.dpCodigo_pes, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 10, 1, Me.cboCecos.SelectedValue, 0), "codigo_pes", "descripcion_pes")


            objfun = Nothing
            obj.CerrarConexion()
            obj = Nothing
            Me.cmdVer.Visible = Me.dpCodigo_pes.Items.Count > 0
        End If
    End Sub

    Protected Sub grwAlumnosPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwAlumnosPlan.SelectedIndexChanged

    End Sub
End Class

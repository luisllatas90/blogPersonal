
Partial Class academico_horarios_administrar_AmbientesPorReasignar
    Inherits System.Web.UI.Page

    Protected Sub cmdVer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVer.Click
        ConsultarCursosYAmbientesAsignados()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim tbl As Data.DataTable
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")

            '===================================
            'Permisos por Departamento académico
            '===================================
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
            ClsFunciones.LlenarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "UCI", 0), "codigo_cac", "descripcion_cac", "--Seleccione--")
            ClsFunciones.LlenarListas(Me.dpTipoAmbiente, obj.TraerDataTable("ACAD_ConsultarTipoAmbiente"), "codigo_tam", "descripcion_Tam")
            dpTipoAmbiente.SelectedValue = 3
            tbl.Dispose()
            obj.CerrarConexion()
            obj = Nothing
            Me.dpAmbientes.Items.Add("--Seleccione--")
 
        End If
    End Sub

    Private Sub ConsultarCursosYAmbientesAsignados()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        '=========================
        'Cargar Grupos Programados
        '=========================
        Me.gvCursosProgramados.DataSource = obj.TraerDataTable("ACAD_ConsultarCursosHorariosAsignadosxDpto", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_Dac.SelectedValue, "R", dpTipoAmbiente.SelectedValue)
        Me.gvCursosProgramados.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub gvCursosProgramados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCursosProgramados.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

            If e.Row.Cells(6).Text = "Abierto" Then
                e.Row.Cells(6).ForeColor = Drawing.Color.Blue
            Else
                e.Row.Cells(6).ForeColor = Drawing.Color.Red
            End If

            If e.Row.Cells(3).Text = True Then
                e.Row.Cells(3).Text = "Si"
            Else
                e.Row.Cells(3).Text = "No"
            End If

            Select Case e.Row.Cells(10).Text
                Case "A" : e.Row.Cells(10).ForeColor = Drawing.Color.Blue
                Case "R" : e.Row.Cells(10).ForeColor = Drawing.Color.Green
                Case "P" : e.Row.Cells(10).ForeColor = Drawing.Color.Red
                Case "E" : e.Row.Cells(10).ForeColor = Drawing.Color.Gray
            End Select

        End If
    End Sub

    Protected Sub gvCursosProgramados_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvCursosProgramados.RowDeleting
        Dim codigo_cup, codigo_lho As Integer
        Try
            codigo_cup = gvCursosProgramados.DataKeys.Item(e.RowIndex).Values(0)
            codigo_lho = gvCursosProgramados.DataKeys.Item(e.RowIndex).Values(1)
            ActualizarEstadoAmbiente(codigo_cup, codigo_lho, "E")

            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Programar", "alert('Se liberó el ambiente " & Me.gvCursosProgramados.Rows(e.RowIndex).Cells(7).Text & " en el horario " & Me.gvCursosProgramados.Rows(e.RowIndex).Cells(9).Text & " correctamente');", True)
            ConsultarCursosYAmbientesAsignados()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Error", "alert('Ocurrió un error al procesar los datos');", True)
        End Try
    End Sub

    Private Sub ActualizarEstadoAmbiente(ByVal codigo_cup As Integer, ByVal codigo_lho As Integer, ByVal estado_lho As String)
        Dim obj As New ClsConectarDatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("ACAD_ActualizarEstadoAmbiente", codigo_cup, codigo_lho, estado_lho)
        obj.CerrarConexion()
    End Sub

    Protected Sub gvCursosProgramados_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCursosProgramados.SelectedIndexChanged
        Dim obj As New ClsConectarDatos
        Dim Fun As New ClsFunciones
        Dim dia, horaIni, horaFin As String
      
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
     
        dia = gvCursosProgramados.DataKeys.Item(gvCursosProgramados.SelectedIndex).Values(2)
        horaIni = gvCursosProgramados.DataKeys.Item(gvCursosProgramados.SelectedIndex).Values(3)
        horaFin = gvCursosProgramados.DataKeys.Item(gvCursosProgramados.SelectedIndex).Values(4)
        '===============
        'Cargar Horarios
        '===============
        If chkFiltrar.Checked = False Then
            Fun.CargarListas(Me.dpAmbientes, obj.TraerDataTable("ACAD_ConsultarAmbientesDisponiblesXHorario", Me.dpCodigo_cac.SelectedValue, dia, horaIni, horaFin, -1), "codigo_amb", "Ambiente", "--Seleccione--")
        Else
            Fun.CargarListas(Me.dpAmbientes, obj.TraerDataTable("ACAD_ConsultarAmbientesDisponiblesXHorario", Me.dpCodigo_cac.SelectedValue, dia, horaIni, horaFin, gvCursosProgramados.Rows(gvCursosProgramados.SelectedIndex).Cells(4).Text), "codigo_amb", "Ambiente", "--Seleccione--")
        End If
        obj.CerrarConexion()
        obj = Nothing

        Me.gvHorarioSemana.DataSource = Nothing
        Me.gvHorarioSemana.DataBind()

    End Sub

    Protected Sub dpAmbientes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpAmbientes.SelectedIndexChanged
        Dim codigo_cup, codigo_lho As Integer
        codigo_cup = gvCursosProgramados.DataKeys.Item(gvCursosProgramados.SelectedIndex).Values(0)
        codigo_lho = gvCursosProgramados.DataKeys.Item(gvCursosProgramados.SelectedIndex).Values(1)

        Dim obj As New ClsConectarDatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        '===============
        'Cargar Horarios
        '===============
        Me.gvHorarioSemana.DataSource = obj.TraerDataTable("ACAD_ConsultarHorarioDisponible", Me.dpCodigo_cac.SelectedValue, codigo_cup, dpAmbientes.SelectedValue, codigo_lho)
        Me.gvHorarioSemana.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub gvHorarioSemana_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvHorarioSemana.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).BackColor = Drawing.Color.LightBlue  '    #FFCC66
            If e.Row.Cells(1).Text = "1" Then
                e.Row.Cells(1).BackColor = Drawing.Color.Red
                e.Row.Cells(1).Text = ""
            ElseIf e.Row.Cells(1).Text = "0" Then
                e.Row.Cells(1).BackColor = Drawing.Color.Yellow
                e.Row.Cells(1).Text = ""
            End If
            If e.Row.Cells(2).Text = "1" Then
                e.Row.Cells(2).BackColor = Drawing.Color.Red
                e.Row.Cells(2).Text = ""
            ElseIf e.Row.Cells(2).Text = "0" Then
                e.Row.Cells(2).BackColor = Drawing.Color.Yellow
                e.Row.Cells(2).Text = ""
            End If
            If e.Row.Cells(3).Text = "1" Then
                e.Row.Cells(3).BackColor = Drawing.Color.Red
                e.Row.Cells(3).Text = ""
            ElseIf e.Row.Cells(3).Text = "0" Then
                e.Row.Cells(3).BackColor = Drawing.Color.Yellow
                e.Row.Cells(3).Text = ""
            End If
            If e.Row.Cells(4).Text = "1" Then
                e.Row.Cells(4).BackColor = Drawing.Color.Red
                e.Row.Cells(4).Text = ""
            ElseIf e.Row.Cells(4).Text = "0" Then
                e.Row.Cells(4).BackColor = Drawing.Color.Yellow
                e.Row.Cells(4).Text = ""
            End If
            If e.Row.Cells(5).Text = "1" Then
                e.Row.Cells(5).BackColor = Drawing.Color.Red
                e.Row.Cells(5).Text = ""
            ElseIf e.Row.Cells(5).Text = "0" Then
                e.Row.Cells(5).BackColor = Drawing.Color.Yellow
                e.Row.Cells(5).Text = ""
            End If
            If e.Row.Cells(6).Text = "1" Then
                e.Row.Cells(6).BackColor = Drawing.Color.Red
                e.Row.Cells(6).Text = ""
            ElseIf e.Row.Cells(6).Text = "0" Then
                e.Row.Cells(6).BackColor = Drawing.Color.Yellow
                e.Row.Cells(6).Text = ""
            End If
        End If
    End Sub


    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim codigo_cup As Integer
        Dim codigo_lho As Integer
        Dim obj As New ClsConectarDatos
        Try

            codigo_cup = gvCursosProgramados.DataKeys.Item(gvCursosProgramados.SelectedIndex).Values(0)
            codigo_lho = gvCursosProgramados.DataKeys.Item(gvCursosProgramados.SelectedIndex).Values(1)

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.IniciarTransaccion()
            obj.Ejecutar("ACAD_ActualizarAmbienteEnHorario", codigo_cup, codigo_lho, Me.dpAmbientes.SelectedValue, Request.QueryString("id"))
            obj.TerminarTransaccion()
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Asignar", "alert('Se asignó el ambiente seleccionado');", True)


            ConsultarCursosYAmbientesAsignados()
            Me.gvHorarioSemana.DataSource = Nothing
            Me.gvHorarioSemana.DataBind()
            Me.dpAmbientes.Items.Clear()
            Me.dpAmbientes.Items.Add("--Seleccione--")

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Error", "alert('Ocurrió un error al procesar los datos, vuelva a intentarlo');", True)
        End Try
    End Sub

End Class

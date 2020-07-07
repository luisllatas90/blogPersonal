
Partial Class academico_estudiante_RecalcularInhabilitacion
    Inherits System.Web.UI.Page

    Public Enum MessageType
        Success
        Errors
        Info
        Warning
    End Enum

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../ErrorSistema.aspx")
        End If
        Try
            If IsPostBack = False Then
                CargaSemestre()
                CargaCarreraProfesional()
                CargarCursos()
            End If
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Errors)
        End Try
    End Sub

    Private Sub CargaCarreraProfesional()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            If (Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 85 Or Request.QueryString("ctf") = 181) Then
                dt = obj.TraerDataTable("ACAD_BuscaCarreraProfesional", "T", Request.QueryString("mod"), 0, "", 0)
            Else
                dt = obj.TraerDataTable("ACAD_BuscaCarreraProfesional", "", Request.QueryString("mod"), 0, "", Session("id_per"))
            End If
            obj.CerrarConexion()

            Me.cboCarrera.DataSource = dt
            Me.cboCarrera.DataValueField = "codigo_cpf"
            Me.cboCarrera.DataTextField = "nombre_cpf"
            Me.cboCarrera.DataBind()
            obj = Nothing


        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Errors)
        End Try
    End Sub
  
    Private Sub CargarCursos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("Inh_ConsultarCursos", Me.cboSemestre.SelectedValue, Me.cboCarrera.SelectedValue)
            obj.CerrarConexion()

            Me.cboCursos.DataSource = dt
            Me.cboCursos.DataValueField = "codigo_Cup"
            Me.cboCursos.DataTextField = "curso"
            Me.cboCursos.DataBind()

            obj = Nothing
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Errors)
        End Try
    End Sub
    Private Sub CargaSemestre()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_BuscaCicloAcademico", 0, "")



            Me.cboSemestre.DataSource = dt
            Me.cboSemestre.DataValueField = "codigo_cac"
            Me.cboSemestre.DataTextField = "descripcion_cac"
            Me.cboSemestre.DataBind()

            dt = obj.TraerDataTable("Inh_ConsultarSemestreActual")
            Me.cboSemestre.SelectedValue = dt.Rows(0).Item("codigo_cac")
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Errors)
        End Try
    End Sub


   

    'I. Inhabilitado        A. Anula Inhabilitados
    'Private Sub ValidaCronograma(ByVal tipo As String)
    '    Dim obj As New ClsConectarDatos
    '    Dim dt As New Data.DataTable
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    Try
    '        If Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 85 _
    '                Or Request.QueryString("ctf") = 181 Then
    '            'Me.btnBuscar.Enabled = True
    '            'Me.btnInactivar.Enabled = True
    '        Else
    '            obj.AbrirConexion()
    '            If tipo = "I" Then
    '                dt = obj.TraerDataTable("ACAD_InhabilitacionApta", Me.cboSemestre.SelectedValue, Request.QueryString("mod"))
    '            Else
    '                'dt = obj.TraerDataTable("ACAD_InhabilitacionApta", Me.cboSemestreAnula.SelectedValue, Request.QueryString("mod"))
    '            End If

    '            obj.CerrarConexion()

    '            If dt.Rows.Count > 0 Then
    '                If tipo = "I" Then
    '                    'Me.btnBuscar.Enabled = True
    '                    'Me.btnInactivar.Enabled = True
    '                Else
    '                    'Me.btnBuscarAnula.Enabled = True
    '                    'Me.btnAnula.Enabled = True
    '                End If
    '            Else
    '                ShowMessage("Cronograma para registrar inhabilitaciones " & Me.cboSemestre.SelectedItem.Text & " no está disponible. Comunicarse con Dirección Académica.", MessageType.Info)
    '                'Me.btnBuscar.Enabled = False
    '                'Me.btnInactivar.Enabled = False
    '                'Me.btnBuscarAnula.Enabled = False
    '                'Me.btnAnula.Enabled = False
    '            End If
    '        End If

    '        dt.Dispose()
    '        obj = Nothing
    '    Catch ex As Exception
    '        ShowMessage("Error: " & ex.Message, MessageType.Errors)
    '    End Try
    'End Sub

   
   
   

    Protected Sub cboCarrera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarrera.SelectedIndexChanged
        'Try
        CargarCursos()
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub CargarEstudiantes()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("Inh_ConsultarInhabilitados", Me.cboCursos.SelectedValue, Me.cboEstado.SelectedValue)
            obj.CerrarConexion()

            If dt.Rows.Count Then
                Me.gridAlumnos.DataSource = dt
            Else
                Me.gridAlumnos.DataSource = Nothing                
            End If
            Me.gridAlumnos.DataBind()

            obj = Nothing
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Errors)
        End Try
    End Sub
    Protected Sub cboCursos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCursos.SelectedIndexChanged
        CargarEstudiantes()
    End Sub

    Protected Sub cboEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEstado.SelectedIndexChanged
        CargarEstudiantes()
    End Sub

    Protected Sub btnRecalcular_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecalcular.Click
        Dim Fila As GridViewRow
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim sw As Byte = 0
        Try
            For I As Int16 = 0 To Me.gridAlumnos.Rows.Count - 1
                Fila = Me.gridAlumnos.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        sw = 1
                    End If
                End If
            Next

            If (sw = 0) Then
                ShowMessage("Mensaje: " & "Debe seleccionar un registro.", MessageType.Success)
                Exit Sub
            End If



            For I As Int16 = 0 To Me.gridAlumnos.Rows.Count - 1
                Fila = Me.gridAlumnos.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        obj.AbrirConexion()
                        obj.Ejecutar("Inh_Recalcular", gridAlumnos.DataKeys(I).Values("codigo_dma"), gridAlumnos.DataKeys(I).Values("codigo_alu"), Me.cboCursos.SelectedValue, Me.cboSemestre.SelectedValue, Session("id_per"))
                        obj.CerrarConexion()                       
                    End If
                End If
            Next

            Try
                obj.AbrirConexion()
                obj.Ejecutar("Moodle_ActualizarLista", Me.cboCursos.SelectedValue)
                obj.CerrarConexion()
            Catch ex As Exception

            End Try

            ShowMessage("Se re-calcularon los valores para los estudiantes seleccionados", MessageType.Success)
            CargarEstudiantes()

        Catch ex As Exception
            ShowMessage("Error: " & ex.Message, MessageType.Errors)
        End Try
    End Sub

    Protected Sub gridAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAlumnos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim index As Integer = e.Row.RowIndex
            If gridAlumnos.DataKeys(e.Row.RowIndex).Values("inh") <> 1 Then
                If CDec(e.Row.Cells(13).Text) > 30.0 Then
                    e.Row.Cells(13).ForeColor = Drawing.Color.Red
                End If
            End If

            If gridAlumnos.DataKeys(e.Row.RowIndex).Values("inh") = 1 Then
                e.Row.Cells(5).Text = e.Row.Cells(5).Text & "<font style='color:red;'> (INH)</font>"
            End If
            
        End If
    End Sub
End Class

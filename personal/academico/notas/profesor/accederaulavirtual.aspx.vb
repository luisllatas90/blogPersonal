
Partial Class academico_notas_profesor_accederaulavirtual
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private cod_user As Integer

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        cod_user = Session("id_per")

        If Not IsPostBack Then
            Call ConsultarCicloAcademico()
            Call ConsultarCargaAcademica()
        Else
            Call mt_RefreshGrid()
        End If
    End Sub

    Protected Sub cboCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCiclo.SelectedIndexChanged
        Call ConsultarCargaAcademica()
    End Sub

    Protected Sub gvCarga_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCarga.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As New LinkButton()
            
            btn = New LinkButton()
            btn.ID = "btnAcceder"
            btn.Text = "<i class='fa fa-share-square'></i>"
            btn.CommandArgument = e.Row.RowIndex
            btn.CommandName = "cmdAcceder"
            btn.ToolTip = "Ir a Aula Virtual"
            btn.OnClientClick = "return confirm('¿Desea ir al aula virtual?');"
            btn.CssClass = "btn btn-primary btn-sm"
            e.Row.Cells(2).Controls.Add(btn)

            e.Row.Cells(3).Text = gvCarga.DataKeys(e.Row.RowIndex).Item("grupoHor_cup").ToString
            e.Row.Cells(4).Text = gvCarga.DataKeys(e.Row.RowIndex).Item("ciclo_cur").ToString
            e.Row.Cells(5).Text = gvCarga.DataKeys(e.Row.RowIndex).Item("nombre_cpf").ToString
            e.Row.Cells(6).Text = gvCarga.DataKeys(e.Row.RowIndex).Item("matriculados").ToString

            btn = New LinkButton()
            btn.ID = "btnDocente"
            btn.Text = "Actualizar <i class='fa fa-refresh'></i>"
            btn.CommandArgument = e.Row.RowIndex
            btn.CommandName = "actDocente"
            btn.ToolTip = "Actualizar lista de docentes"
            btn.CssClass = "btn btn-warning btn-sm"
            e.Row.Cells(7).Controls.Add(btn)

            btn = New LinkButton()
            btn.ID = "btnEstudiante"
            btn.Text = "Actualizar <i class='fa fa-refresh'></i>"
            btn.CommandArgument = e.Row.RowIndex
            btn.CommandName = "actEstudiante"
            btn.ToolTip = "Actualizar lista de estudiantes"
            btn.CssClass = "btn btn-success btn-sm"
            e.Row.Cells(8).Controls.Add(btn)

            e.Row.Cells(9).Text = gvCarga.DataKeys(e.Row.RowIndex).Item("silabo").ToString

            If e.Row.Cells(0).Text <> "0" Then
                e.Row.Cells(0).Text = "ACTIVADO"
                e.Row.ForeColor = Drawing.Color.DarkBlue
            Else
                e.Row.Cells(0).Text = "PENDIENTE"
                e.Row.ForeColor = Drawing.Color.Green
                e.Row.Cells(2).Text = "-"
                e.Row.Cells(7).Text = "-"
                e.Row.Cells(8).Text = "-"
            End If

            'Por Luis Q.T. | 03SEP2019 | Se adicionó condición para descargar Sílabo generado por el sistema
            If String.IsNullOrEmpty(e.Row.Cells(9).Text.ToString) Then
                Dim var As String = Me.gvCarga.DataKeys(e.Row.RowIndex).Values("codigo_cup").ToString
                e.Row.Cells(9).Text = "<a href='#' onClick=downSilabo('" & var & "');>Descargar</a>"
            Else
                If e.Row.Cells(9).Text.ToString.ToUpper <> "NO DISPONIBLE" Then
                    Dim url As String = "../../../../silabos/"
                    e.Row.Cells(9).Text = "<a href=""" & url & e.Row.Cells(9).Text & """>Descargar</a>"
                Else
                    e.Row.Cells(9).Text = ""
                End If
            End If

            e.Row.Font.Bold = True
        End If
    End Sub

    Protected Sub gvCarga_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCarga.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)

        If index >= 0 Then
            Select Case e.CommandName
                Case "cmdAcceder"
                    Call mt_AccederAula(index, "")
                Case "cmdActivar"
                    'Call activarCurso(gvCarga.DataKeys(index).Values("codigo_cup"), Me.ddlFormato.SelectedValue.ToString, Me.ddlSeccion.SelectedItem.Text)
                    'Call ConsultarCargaAcademica()
                Case "actDocente"
                    Call actualizarListaDocente(gvCarga.DataKeys(index).Values("codigo_cup"))
                    Call ConsultarCargaAcademica()
                Case "actEstudiante"
                    Call actualizarListaEstudiante(gvCarga.DataKeys(index).Values("codigo_cup"))
                    Call ConsultarCargaAcademica()
                Case Else

            End Select
        End If
    End Sub

    Protected Sub lnkMoodle2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMoodle2.Click
        Call mt_AccederAula(1, "no")
    End Sub

#End Region

#Region "Metodos"

    Public Sub ConsultarCicloAcademico()
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            Dim codigo_cac As Integer = 0

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("Moodle_ListarCiclos", "X", CInt(Request.QueryString("id").ToString))
            obj.CerrarConexion()

            Me.ddlCiclo.DataSource = dt
            Me.ddlCiclo.DataTextField = "descripcion_cac"
            Me.ddlCiclo.DataValueField = "codigo_cac"
            Me.ddlCiclo.DataBind()

            codigo_cac = ObtenerCicloVigente()

            If Me.ddlCiclo.Items.Count > 0 And codigo_cac > 0 Then
                Me.ddlCiclo.SelectedValue = codigo_cac
            End If

        Catch ex As Exception
            Call mt_ShowMessage("Error al cargar el ciclo", MessageType.Error)
        End Try
    End Sub

    Public Sub ConsultarCargaAcademica()
        Try
            Call mt_CrearColumns()

            If Me.ddlCiclo.SelectedValue <> "0" Then
                Dim dt As New Data.DataTable
                Dim obj As New ClsConectarDatos

                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dt = obj.TraerDataTable("Moodle_ConsultarCargaAcademica", Me.ddlCiclo.SelectedValue, CInt(Request.QueryString("id").ToString))
                obj.CerrarConexion()

                If dt.Rows.Count > 0 Then
                    Me.gvCarga.DataSource = dt
                    Me.gvCarga.DataBind()
                Else
                    Me.gvCarga.DataSource = Nothing
                    Me.gvCarga.DataBind()
                    Call mt_ShowMessage("No existe carga académica para el ciclo seleccionado", MessageType.Info)
                End If
            Else
                Me.gvCarga.DataSource = Nothing
                Me.gvCarga.DataBind()
                Call mt_ShowMessage("No existe carga académica para el ciclo seleccionado", MessageType.Info)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message & " - " & ex.StackTrace, MessageType.Error)
        End Try
    End Sub

    Private Sub activarCurso(ByVal codigo_cup As Integer, ByVal format As String, ByVal nrosecciones As String)
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("Moodle_CrearCursoV31", codigo_cup, format, nrosecciones)
            obj.CerrarConexion()
            obj = Nothing

            If dt.Rows.Count Then
                If dt.Rows(0).Item("idmoodle") > 0 Then
                    Call mt_ShowMessage("Se ha creado correctamente los cursos seleccionados", MessageType.Success)
                Else
                    Call mt_ShowMessage("Ha ocurrido un error, por favor vuelva a intentarlo", MessageType.Info)
                End If
            End If
            dt = Nothing
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message & " - " & ex.StackTrace, MessageType.Error)
        End Try
    End Sub

    Private Sub actualizarListaEstudiante(ByVal codigo_cup As Integer)
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            'Retirados
            obj.AbrirConexion()
            obj.Ejecutar("[Moodle_RetirarEstudiantes_xCurso]", codigo_cup)
            obj.CerrarConexion()

            'Matriculados
            obj.AbrirConexion()
            obj.Ejecutar("[Moodle_ActualizarLista]", codigo_cup)
            obj.CerrarConexion()

            Call mt_ShowMessage("Actualización de lista de estudiantes finalizada.", MessageType.Success)
            obj = Nothing
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message & " - " & ex.StackTrace, MessageType.Error)
        End Try
    End Sub

    Private Sub actualizarListaDocente(ByVal codigo_cup As Integer)
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            'Agregar Docentes
            obj.AbrirConexion()
            obj.Ejecutar("[Moodle_ActualizarListaDocente]", codigo_cup)
            obj.CerrarConexion()

            Call mt_ShowMessage("Actualización de lista de docentes finalizada.", MessageType.Success)
            obj = Nothing
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message & " - " & ex.StackTrace, MessageType.Error)
        End Try
    End Sub

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_AccederAula(ByVal index As Integer, ByVal modo As String)
        Try
            Dim obj As New ClsConectarDatos
            Dim dtUsr As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            obj.AbrirConexion()
            dtUsr = obj.TraerDataTable("MOODLE_ConsultarCodigoAcceso", "PE", cod_user)
            obj.CerrarConexion()

            If dtUsr.Rows.Count > 0 Then
                Dim _url As String = "//intranet.usat.edu.pe/aulavirtual/login/index.php"
                Dim _user As String = dtUsr.Rows(0).Item("codigo_pso").ToString
                Dim _pass As String = dtUsr.Rows(0).Item("ClaveInterna_Pso").ToString
                Dim _curso As String = IIf(modo.Equals("no"), modo, "grades")
                Dim _idcurso As String = "0"

                If ConfigurationManager.AppSettings("CorreoUsatActivo") = "0" Then
                    '_url = "//10.10.14.69/aulavirtual/login/index.php"
                End If

                If Not modo.Equals("no") Then
                    _idcurso = Me.gvCarga.DataKeys(index).Values("idcurso_mdl")
                End If

                If _idcurso <> "-1" Then
                    _idcurso = IIf(modo.Equals("no"), modo, _idcurso)

                    Response.Clear()
                    Dim sb = New System.Text.StringBuilder()
                    sb.Append("<html>")
                    sb.AppendFormat("<body onload='document.forms[0].submit()'>")
                    sb.AppendFormat("<form action='{0}' method='post' target='_blank'>", _url)
                    sb.AppendFormat("<input type='hidden' name='avm7' value='{0}'>", _user)
                    sb.AppendFormat("<input type='hidden' name='avm8' value='{0}'>", _pass)
                    sb.AppendFormat("<input type='hidden' name='curso' value='{0}'>", _curso) ' no
                    sb.AppendFormat("<input type='hidden' name='idcurso' value='{0}'>", _idcurso) ' no
                    sb.Append("</form>")
                    sb.Append("</body>")
                    sb.Append("</html>")
                    Response.Write(sb.ToString())
                Else
                    Call mt_ShowMessage("No se puede acceder al Aula Virtual porque aún no ha sido creada", MessageType.Info)
                End If
            Else
                Call mt_ShowMessage("Ocurrió un Error para iniciar sesión en el Aula Virtual", MessageType.Warning)
            End If

            Call ConsultarCargaAcademica()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CrearColumns()
        Try
            Dim tfield As New TemplateField()

            If Me.gvCarga.Columns.Count > 2 Then
                For i As Integer = 3 To Me.gvCarga.Columns.Count
                    Me.gvCarga.Columns.RemoveAt(Me.gvCarga.Columns.Count - 1)
                Next
            End If

            tfield = New TemplateField()
            tfield.HeaderText = "Acceder"
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            'tfield.ItemStyle.Width = Unit.Pixel(82)
            Me.gvCarga.Columns.Add(tfield)

            tfield = New TemplateField()
            tfield.HeaderText = "Grupo"
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            'tfield.ItemStyle.Width = Unit.Pixel(137)
            Me.gvCarga.Columns.Add(tfield)

            tfield = New TemplateField()
            tfield.HeaderText = "Ciclo"
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            'tfield.ItemStyle.Width = Unit.Pixel(55)
            Me.gvCarga.Columns.Add(tfield)

            tfield = New TemplateField()
            tfield.HeaderText = "Programa de estudios"
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            'tfield.ItemStyle.Width = Unit.Pixel(244)
            Me.gvCarga.Columns.Add(tfield)

            tfield = New TemplateField()
            tfield.HeaderText = "Nro.Mat."
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            'tfield.ItemStyle.Width = Unit.Pixel(82)
            Me.gvCarga.Columns.Add(tfield)

            tfield = New TemplateField()
            tfield.HeaderText = "Docentes asignados"
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            'tfield.ItemStyle.Width = Unit.Pixel(170)
            Me.gvCarga.Columns.Add(tfield)

            tfield = New TemplateField()
            tfield.HeaderText = "Lista de estudiantes"
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            'tfield.ItemStyle.Width = Unit.Pixel(170)
            Me.gvCarga.Columns.Add(tfield)

            tfield = New TemplateField()
            tfield.HeaderText = "Sílabo"
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            'tfield.ItemStyle.Width = Unit.Pixel(65)
            Me.gvCarga.Columns.Add(tfield)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_RefreshGrid()
        Try
            For Each _Row As GridViewRow In Me.gvCarga.Rows
                gvCarga_RowDataBound(Me.gvCarga, New GridViewRowEventArgs(_Row))
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Funciones"

    Function ObtenerCicloVigente() As Integer
        Try
            Dim dt As New System.Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCicloAcademico", "VIG", 1)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("codigo_cac")
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function

#End Region

End Class

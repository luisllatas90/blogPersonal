Partial Class Programacion
    Inherits System.Web.UI.Page

    Private con As String = "0"
    Private evt As String = "0"
    Private est As String = "ABIERTO"
    Private des As String = ""
    Private cat As String = "P" 'Programadas
    Private cpf As String = ""
    Private C As ClsConectarDatos

    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSATIMPORT").ConnectionString
        End If
    End Sub

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler btnRegistrar.ServerClick, AddressOf btnRegistrar_Click
        AddHandler btnRefrescar.ServerClick, AddressOf btnRefrescar_Click

        Try
            con = Request.Params("con").Trim()
            evt = Request.Params("evt").Trim()
            est = Request.Params("est").Trim()
            des = Request.Params("des").Trim()
            cat = Request.Params("cat").Trim()

            If Not IsPostBack Then
                Me.lblEvento.InnerText = des

                ddlCarrera.Visible = cat.Equals("I")
                lblCarrera.Visible = cat.Equals("I")

                If Not String.IsNullOrEmpty(cat) AndAlso cat.Equals("I") Then
                    Call mt_CargarCarreraProfesional()
                    cpf = ddlCarrera.SelectedValue.ToString
                End If

                Call MostrarDetalle(cat, evt, cpf)
            Else
                Call RefreshGrid()
            End If
        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "');</script>")
        End Try
    End Sub

    Protected Sub grwProgramacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwProgramacion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim celda As TableCellCollection = e.Row.Cells
            Dim evento As String = Me.grwProgramacion.DataKeys(e.Row.RowIndex).Values.Item("evento")
            Dim scheduleId As String = Me.grwProgramacion.DataKeys(e.Row.RowIndex).Values.Item("schedule_id")
            Dim idx As Integer = e.Row.RowIndex + 1

            Dim btnEditar As New HtmlButton
            With btnEditar
                .ID = "btnEditar" & idx
                .Attributes.Add("class", "btn btn-primary btn-sm")
                .Attributes.Add("type", "button")
                .Attributes.Add("codigo_pro", e.Row.Cells(0).Text.Trim())
                .Attributes.Add("title", "Editar tarea")
                .InnerHtml = "<i class='fa fa-edit' title='Editar tarea'></i>"

                AddHandler .ServerClick, AddressOf btnEditar_Click
            End With
            celda(7).Controls.Add(btnEditar)

            Dim btnEnviar As New HtmlButton
            With btnEnviar
                .ID = "btnEnviar" & idx
                .Attributes.Add("class", "btn btn-success btn-sm")
                .Attributes.Add("type", "button")
                .Attributes.Add("codigo_pro", e.Row.Cells(0).Text.Trim())
                .Attributes.Add("schedule_id", scheduleId)
                .Attributes.Add("evento", evento)
                .Attributes.Add("title", "Enviar inmediatamente")

                If e.Row.Cells(5).Text.Trim().Equals("Inactivo") Then
                    .Attributes.Add("disabled", True)
                Else
                    .Attributes.Remove("disabled")
                End If

                .InnerHtml = "<i class='fa fa-share-square' title='Enviar inmediatamente'></i>"
                AddHandler .ServerClick, AddressOf btnEnviar_Click
            End With
            celda(8).Controls.Add(btnEnviar)

            Dim btnEstado As New HtmlButton
            With btnEstado
                .ID = "btnEstado" & idx
                .Attributes.Add("class", "btn btn-danger btn-sm")
                .Attributes.Add("type", "button")
                .Attributes.Add("codigo_pro", e.Row.Cells(0).Text.Trim())
                .Attributes.Add("schedule_id", scheduleId)
                .Attributes.Add("evento", evento)
                .Attributes.Add("title", "Desactivar tarea")

                If e.Row.Cells(5).Text.Trim().Equals("Inactivo") Then
                    .Attributes.Add("disabled", True)
                Else
                    .Attributes.Remove("disabled")
                End If

                .InnerHtml = "<i class='fa fa-ban' title='Desactivar tarea'></i>"
                AddHandler .ServerClick, AddressOf btnEstado_Click
            End With
            celda(9).Controls.Add(btnEstado)

            Dim btnLog As New HtmlButton
            With btnLog
                .ID = "btnLog" & idx
                .Attributes.Add("class", "btn btn-info btn-sm")
                .Attributes.Add("type", "button")
                .Attributes.Add("codigo_pro", e.Row.Cells(0).Text.Trim())
                .Attributes.Add("schedule_id", scheduleId)
                .Attributes.Add("evento", evento)
                .Attributes.Add("title", "Revisar el historial")
                .InnerHtml = "<i class='fa fa-search' title='Revisar el historial'></i>"
                AddHandler .ServerClick, AddressOf btnLog_Click
            End With
            celda(10).Controls.Add(btnLog)

            grwProgramacion.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cod As String = "0"
        cpf = ddlCarrera.SelectedValue.ToString
        ifrmProgramacionMantenimiento.Attributes("src") = "ProgramacionMantenimiento.aspx?cod=" & cod & "&con=" & con & "&evt=" & evt & "&des=" & des & "&cat=" & cat & "&cpf=" & cpf
        panProgramacion.Update()
    End Sub

    Protected Sub btnRefrescar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            cpf = ddlCarrera.SelectedValue.ToString
            Call MostrarDetalle(cat, evt, cpf)
            'Call RefreshGrid()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertMessage", "alert('" & ex.Message & "')", True)
        End Try
    End Sub

    Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim cod As String = button.Attributes("codigo_pro")
        cpf = ddlCarrera.SelectedValue.ToString
        ifrmProgramacionMantenimiento.Attributes("src") = "ProgramacionMantenimiento.aspx?cod=" & cod & "&con=" & con & "&evt=" & evt & "&des=" & des & "&cat=" & cat & "&cpf=" & cpf
        panProgramacion.Update()
    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim cod As String = button.Attributes("codigo_pro")

            C.AbrirConexion()
            Dim resultado As Object() = C.Ejecutar("PRO_EjecutarProgramacion", cod, 0, "")
            C.CerrarConexion()

            Select Case resultado(0)
                Case "1"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.panProgramacion.GetType, "Pop", "<script>mostrarMensaje('Tarea enviada con éxito', 'success');</script>", False)
                    Call btnRefrescar_Click(sender, e)
                Case "0"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.panProgramacion.GetType, "Pop", "<script>mostrarMensaje('" & resultado(1) & "', 'warning');</script>", False)
                Case "-1"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.panProgramacion.GetType, "Pop", "<script>mostrarMensaje('" & resultado(1) & "', 'danger');</script>", False)
            End Select

            If resultado(1) <> "1" Then
                button.Attributes.Remove("disabled")
                udpProgramacion.Update()
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertMessage", "alert('" & ex.Message & "')", True)
            Throw ex
        End Try
    End Sub

    Protected Sub btnEstado_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim cod As String = button.Attributes("codigo_pro")

        C.AbrirConexion()
        Dim lo_Resultado() As Object = C.Ejecutar("PRO_DesactivarProgramacion", cod)
        C.CerrarConexion()

        If lo_Resultado(0) = "1" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.panProgramacion.GetType, "Pop", "<script>mostrarMensaje('La tarea ha sido desactivada', 'danger');</script>", False)
            Call btnRefrescar_Click(sender, e)
        End If
    End Sub

    Protected Sub btnLog_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim cod As String = button.Attributes("codigo_pro")
        ifrmProgramacionLog.Attributes("src") = "ProgramacionLog.aspx?cod=" & cod
        panProgramacionLog.Update()
    End Sub

    Protected Sub ddlCarrera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarrera.SelectedIndexChanged
        Call btnRefrescar_Click(sender, e)
        udpCabecera.Update()
    End Sub

#End Region

#Region "Métodos"

    Private Sub MostrarDetalle(ByVal pCat As String, ByVal pEvt As String, ByVal pCpf As String)
        Try
            Dim dt As New System.Data.DataTable("Programacion")
            Dim codigo_eve As Object = pEvt
            pCpf = IIf(String.IsNullOrEmpty(pCpf), "0", pCpf)

            If String.IsNullOrEmpty(codigo_eve) Then
                codigo_eve = DBNull.Value
            End If

            C.AbrirConexion()
            dt = C.TraerDataTable("PRO_listarProgramacionEvento", pCat, codigo_eve, pCpf)
            C.CerrarConexion()

            Me.grwProgramacion.DataSource = dt
            Me.grwProgramacion.DataBind()
            dt.Dispose()

            udpProgramacion.Update()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertMessage", "alert('" & ex.Message & "')", True)
        End Try
    End Sub

    Private Sub mt_CargarCarreraProfesional()
        Dim dt As New Data.DataTable

        Try
            C.AbrirConexion()
            dt = C.TraerDataTable("ConsultarCarreraProfesionalV3", "CF", 2, -1, -1)
            C.CerrarConexion()

            ddlCarrera.DataSource = dt
            ddlCarrera.DataTextField = "nombre_Cpf"
            ddlCarrera.DataValueField = "codigo_Cpf"
            ddlCarrera.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    Private Sub RefreshGrid()
        For Each _Row As GridViewRow In grwProgramacion.Rows
            grwProgramacion_RowDataBound(grwProgramacion, New GridViewRowEventArgs(_Row))
        Next
    End Sub

#End Region

#Region "Funciones"

    Public Function PintarAvanceDIV(ByVal porc As Object) As String
        Dim style As String = ""

        If porc Is Nothing Then
            Return ""
        End If

        If CInt(porc) <= 25 Then
            style = "progress-bar-danger"
        ElseIf CInt(porc) > 25 And CInt(porc) <= 50 Then
            style = "progress-bar-warning"
        ElseIf CInt(porc) > 50 And CInt(porc) <= 75 Then
            style = "progress-bar-info"
        Else
            style = "progress-bar-success"
        End If

        Return style
    End Function

    Public Function CalcularPorcentaje(ByVal a As Decimal, ByVal b As Decimal) As Decimal
        Dim division As Decimal = 0
        If a <> 0 AndAlso b <> 0 Then
            division = a / b
        End If

        Return Math.Round(division * 100, 2)
    End Function

    Public Function EnviadosPorcentaje(ByVal nroEnviados As String, _
                             ByVal nroTotal As String, _
                             Optional ByVal flagPorcentaje As Boolean = False) As String
        Return CalcularPorcentaje(nroEnviados, nroTotal) & "%"
    End Function

    Public Function RestantesPorcentaje(ByVal nroEnviados As String, _
                              ByVal nroTotal As String, _
                              Optional ByVal flagPorcentaje As Boolean = False) As String
        Return CalcularPorcentaje(nroTotal - nroEnviados, nroTotal) & "%"
    End Function

#End Region

End Class

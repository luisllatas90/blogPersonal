Partial Class academico_horarios_frmSolicitudAmbienteV2
    Inherits System.Web.UI.Page


    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Buscar()
    End Sub

    Sub Buscar()
        Me.gvSolicitud.Visible = True
        Dim fecha As Date
        fecha = Today()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        'Response.Write(": " & Me.ddlCco.SelectedValue & "," & Me.ddlEstado.SelectedValue & "," & fecha.AddDays(CInt(Me.ddlFecha.SelectedValue) * +1) & "," & IIf(Me.ddlFecha.SelectedValue = 0, 0, 1) & "," & IIf(Me.ddlFecha.SelectedValue = -1, 1, 0) & "," & CInt(Request.QueryString("id")) & ",0," & Me.ddlSolicitante.SelectedValue & "," & Me.ddlAmbiente.SelectedValue)
        'Response.Write()
        'Response.Write("P3: " & )
        'Response.Write("P4: " & )
        'Response.Write("P5: ")
        'Response.Write("P6: ")
        'Response.Write("P7: ")
        'tb = obj.TraerDataTable("HorarioPE_Solicitudesv2", Me.ddlCco.SelectedValue, Me.ddlEstado.SelectedValue, fecha.AddDays(CInt(Me.ddlFecha.SelectedValue) * +1), IIf(Me.ddlFecha.SelectedValue = 0, 0, 1), IIf(Me.ddlFecha.SelectedValue = -1, 1, 0), CInt(Request.QueryString("id")), 0, Me.ddlSolicitante.SelectedValue, Me.ddlAmbiente.SelectedValue)
        tb = obj.TraerDataTable("HorarioPE_Solicitudesv2_Rec", Me.ddlCco.SelectedValue, Me.ddlEstado.SelectedValue, CInt(Request.QueryString("id")), 0, Me.ddlSolicitante.SelectedValue, Me.ddlAmbiente.SelectedValue, Me.cboCiclo.SelectedValue)
        obj.CerrarConexion()
        obj = Nothing
        If tb.Rows.Count Then
            Me.gvSolicitud.DataSource = tb
        Else
            Me.gvSolicitud.DataSource = Nothing
        End If
        Me.gvSolicitud.DataBind()

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            CargaCiclo()
            CargarCco()
            cargarSolicitante()
            cargarAmbiente()
            Buscar()
        End If
    End Sub
    Sub CargarCco()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim fecha As Date = Today

        'Response.Write("P1: " & fecha.AddDays(CInt(Me.ddlFecha.SelectedValue) * +1) & "</br>")
        'Response.Write("P2: " & IIf(Me.ddlFecha.SelectedValue = 0, 0, 1) & "</br>")
        'Response.Write("P3: " & IIf(Me.ddlFecha.SelectedValue = -1, 1, 0) & "</br>")

        Me.ddlCco.DataSource = obj.TraerDataTable("HorariosPE_ConsultarCcoSol_v2_Rec", cboCiclo.selectedvalue)
        Me.ddlCco.DataTextField = "descripcion_Cco"
        Me.ddlCco.DataValueField = "codigo_cco"
        Me.ddlCco.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Sub cargarSolicitante()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim fecha As Date = Today

        'Response.Write("P1: " & fecha.AddDays(CInt(Me.ddlFecha.SelectedValue) * +1) & "</br>")
        'Response.Write("P2: " & IIf(Me.ddlFecha.SelectedValue = 0, 0, 1) & "</br>")
        'Response.Write("P3: " & IIf(Me.ddlFecha.SelectedValue = -1, 1, 0) & "</br>")

        'Me.ddlSolicitante.DataSource = obj.TraerDataTable("horariope_ConsultarSolicitantes_Rec", fecha.AddDays(CInt(Me.ddlFecha.SelectedValue) * +1), IIf(Me.ddlFecha.SelectedValue = 0, 0, 1), IIf(Me.ddlFecha.SelectedValue = -1, 1, 0))
        Me.ddlSolicitante.DataSource = obj.TraerDataTable("horariope_ConsultarSolicitantes_Rec", cboCiclo.selectedvalue)
        Me.ddlSolicitante.DataTextField = "personal"
        Me.ddlSolicitante.DataValueField = "codigo_per"
        Me.ddlSolicitante.DataBind()
        Me.ddlSolicitante.SelectedValue = 0
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Sub cargarAmbiente()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim fecha As Date = Today

        'Response.Write("P1: " & fecha.AddDays(CInt(Me.ddlFecha.SelectedValue) * +1) & "</br>")
        'Response.Write("P2: " & IIf(Me.ddlFecha.SelectedValue = 0, 0, 1) & "</br>")
        'Response.Write("P3: " & IIf(Me.ddlFecha.SelectedValue = -1, 1, 0) & "</br>")

        'Me.ddlAmbiente.DataSource = obj.TraerDataTable("horariope_ConsultarAmbientesFiltro", fecha.AddDays(CInt(Me.ddlFecha.SelectedValue) * +1), IIf(Me.ddlFecha.SelectedValue = 0, 0, 1), IIf(Me.ddlFecha.SelectedValue = -1, 1, 0))
        Me.ddlAmbiente.DataSource = obj.TraerDataTable("horariope_ConsultarAmbientesFiltro_Rec", cboCiclo.selectedvalue)
        Me.ddlAmbiente.DataTextField = "ambiente"
        Me.ddlAmbiente.DataValueField = "codigo_amb"
        Me.ddlAmbiente.DataBind()
        Me.ddlAmbiente.SelectedValue = 0

        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Function LimpiarAmbiente(ByVal codigo_lho As Integer) As Boolean
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("HorarioPE_LimpiarAmbiente", codigo_lho, 1)
        obj.CerrarConexion()
        obj = Nothing
        If EnviarCorreo(codigo_lho) Then
            Return True
        Else
            Return False '
        End If
    End Function
    Protected Sub gvSolicitud_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvSolicitud.RowCommand
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim codigo_lho As Integer = gvSolicitud.DataKeys(index).Values("codigo_Lho")
        If (e.CommandName = "LimpiarAmbiente") Then

            If Me.chkCorreo.Checked Then
                Me.PanelTexto.Visible = True
                Me.gvSolicitud.Visible = False
                Session("LhoLA") = codigo_lho
                Me.txtTexto.Focus()
            Else
                If LimpiarAmbiente(codigo_lho) Then
                    Buscar()
                Else
                    Response.Write("Ocurrió un error")
                End If
            End If            
        End If

        If (e.CommandName = "ActualizaAmbiente") Then
            obj.AbrirConexion()
            'Me.gridEditar.DataSource = obj.TraerDataTable("HorarioPE_Solicitudesv2", 0, "%", Date.Now, 0, 0, CInt(Request.QueryString("id")), codigo_lho)
            Me.gridEditar.DataSource = obj.TraerDataTable("HorarioPE_Solicitudesv2_Rec", 0, "%", CInt(Request.QueryString("id")), codigo_lho, 0, 0, Me.cboCiclo.selectedvalue)
            Me.gridEditar.DataBind()
            obj.CerrarConexion()
            obj = Nothing
            Me.gvSolicitud.Visible = False
            Me.gridEditar.Visible = True
        End If

        If (e.CommandName = "AprobarAmbiente") Then
            obj.AbrirConexion()
            obj.Ejecutar("HorarioPE_AprobarSolicitud", codigo_lho, gvSolicitud.DataKeys(index).Values("codigo_amb"))
            obj.CerrarConexion()
            obj = Nothing
            If EnviarCorreo(gvSolicitud.DataKeys(index).Values("codigo_Lho")) Then
                Buscar()
            Else
                Response.Write("Ocurrió un error")
            End If
        End If
        obj = Nothing
    End Sub
    Function EnviarCorreo(ByVal codigo_lho As Integer) As Boolean
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tbCorreo As New Data.DataTable
        obj.AbrirConexion()
        tbCorreo = obj.TraerDataTable("HorarioPE_EnviarCorreo_Rec", codigo_lho)
        'Response.Write("codigo_lho" & codigo_lho)
        obj.CerrarConexion()
        obj = Nothing
        Dim objCorreo As New ClsEnvioMailAmbiente
        Dim bodycorreo As String
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
        bodycorreo = bodycorreo & "<p>" & IIf(Me.chkCorreo.Checked, txtTexto.Text.Trim, "") & "</p>"
        bodycorreo = bodycorreo & "<p>" & tbCorreo.Rows(0).Item("footer") & "</p>"
        bodycorreo = bodycorreo & "<p> Atte,<br/><b>" & tbCorreo.Rows(0).Item("firma") & "</b></p>"
        bodycorreo = bodycorreo & "</div></body></html>"
        ' tbCorreo.Rows(0).Item("cc") = ""
        ' tbCorreo.Rows(0).Item("EnviarA") = "yperez@usat.edu.pe"
        If objCorreo.EnviarMailAd("campusvirtual@usat.edu.pe", tbCorreo.Rows(0).Item("firma"), tbCorreo.Rows(0).Item("EnviarA"), tbCorreo.Rows(0).Item("SubjectA") & " - Módulo de Solicitud de Ambientes", bodycorreo, True, tbCorreo.Rows(0).Item("cc")) Then
            Return True
        Else
            Return False
        End If
    End Function



    Protected Sub gridEditar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridEditar.RowDataBound
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tb As New Data.DataTable
        Dim index As Integer
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddlAmb As DropDownList
            ddlAmb = e.Row.FindControl("ddlAmbiente")
            index = e.Row.RowIndex
            obj.AbrirConexion()
            tb = obj.TraerDataTable("HorarioPE_ConsultarAsignacionAmbienteSol3", gridEditar.DataKeys(e.Row.RowIndex).Values("codigo_Lho"), CInt(Request.QueryString("id")))
            obj.CerrarConexion()
            If tb.Rows.Count Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    ddlAmb.Items.Add(New ListItem(tb.Rows(i).Item("ambiente"), tb.Rows(i).Item("codigo_amb")))
                Next
                ddlAmb.SelectedValue = gridEditar.DataKeys(e.Row.RowIndex).Values("codigo_amb")
            Else
                ddlAmb.Items.Add(New ListItem("Sin ambientes disponibles", -1))
            End If
            If (e.Row.Cells(12).Text) = "A" Then
                e.Row.Cells(3).ForeColor = Drawing.Color.Blue
            Else
                e.Row.Cells(3).ForeColor = Drawing.Color.Green
            End If
        End If
        obj = Nothing
        tb.Dispose()
    End Sub



    Protected Sub gvSolicitud_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSolicitud.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If (e.Row.Cells(14).Text) = "A" Then
                e.Row.Cells(3).ForeColor = Drawing.Color.Blue
                e.Row.Cells(1).Text = ""
            Else
                e.Row.Cells(3).ForeColor = Drawing.Color.Green
            End If
        End If
    End Sub

    Protected Sub gridEditar_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridEditar.RowCommand
        Dim Fila As GridViewRow

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Fila = Me.gridEditar.Rows(index)
        If (e.CommandName = "AprobarAmbiente") Then
            obj.AbrirConexion()
            obj.Ejecutar("HorarioPE_AprobarSolicitud", gridEditar.DataKeys(index).Values("codigo_Lho"), CType(Fila.FindControl("ddlAmbiente"), DropDownList).SelectedValue)
            obj.CerrarConexion()
            obj = Nothing

            If EnviarCorreo(gridEditar.DataKeys(index).Values("codigo_Lho")) Then
                Buscar()
                Me.gridEditar.DataSource = Nothing
                Me.gridEditar.DataBind()
                Me.gridEditar.Visible = False
            Else
                Response.Write("Ocurrió un error")
            End If
        End If
        obj = Nothing
    End Sub


    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.gridEditar.DataSource = Nothing
        Me.gridEditar.DataBind()
        Me.gridEditar.Visible = False
        Buscar()
    End Sub

  

    Protected Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        LimpiarAmbiente(Session("LhoLA"))
        Me.PanelTexto.Visible = False
        Me.gvSolicitud.Visible = True
        Session("LhoLA") = 0
        Me.txtTexto.Text = ""
        Me.chkCorreo.Checked = False
        Buscar()
    End Sub

    Protected Sub btnOk0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk0.Click
        Me.PanelTexto.Visible = False
        Me.gvSolicitud.Visible = True
        Session("LhoLA") = 0
        Me.txtTexto.Text = ""
        Me.chkCorreo.Checked = False
        Buscar()
    End Sub
    Private Sub CargaCiclo()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Dim dt As New Data.DataTable

        Try
            dt = obj.TraerDataTable("ACAD_ListaCicloAcademico")
            obj = Nothing

            Me.cboCiclo.DataSource = dt
            Me.cboCiclo.DataTextField = "descripcion_cac"
            Me.cboCiclo.DataValueField = "codigo_cac"
            Me.cboCiclo.DataBind()
            dt = Nothing

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCiclo.SelectedIndexChanged
        CargarCco()
        cargarAmbiente()
        cargarSolicitante()
    End Sub
End Class


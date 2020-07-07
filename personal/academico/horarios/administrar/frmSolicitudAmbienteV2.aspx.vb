﻿Partial Class academico_horarios_frmSolicitudAmbienteV2
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
        tb = obj.TraerDataTable("HorarioPE_Solicitudesv2", Me.ddlCco.SelectedValue, Me.ddlEstado.SelectedValue, fecha.AddDays(CInt(Me.ddlFecha.SelectedValue) * +1), IIf(Me.ddlFecha.SelectedValue = 0, 0, 1), IIf(Me.ddlFecha.SelectedValue = -1, 1, 0), CInt(Request.QueryString("id")), 0, Me.ddlSolicitante.SelectedValue, Me.ddlAmbiente.SelectedValue)
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

        If (Session("id_per") Is Nothing) Then

            Response.Redirect("../../../../sinacceso.html")

        End If

        If Not IsPostBack Then
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

        Me.ddlCco.DataSource = obj.TraerDataTable("HorariosPE_ConsultarCcoSol_v2", fecha.AddDays(CInt(Me.ddlFecha.SelectedValue) * +1), IIf(Me.ddlFecha.SelectedValue = 0, 0, 1), IIf(Me.ddlFecha.SelectedValue = -1, 1, 0))
        Me.ddlCco.DataTextField = "descripcion_Cco"
        Me.ddlCco.DataValueField = "codigo_cco"
        Me.ddlCco.DataBind()
        Me.ddlCco.SelectedValue = 3391
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

        Me.ddlSolicitante.DataSource = obj.TraerDataTable("horariope_ConsultarSolicitantes", fecha.AddDays(CInt(Me.ddlFecha.SelectedValue) * +1), IIf(Me.ddlFecha.SelectedValue = 0, 0, 1), IIf(Me.ddlFecha.SelectedValue = -1, 1, 0))
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

        Me.ddlAmbiente.DataSource = obj.TraerDataTable("horariope_ConsultarAmbientesFiltro", fecha.AddDays(CInt(Me.ddlFecha.SelectedValue) * +1), IIf(Me.ddlFecha.SelectedValue = 0, 0, 1), IIf(Me.ddlFecha.SelectedValue = -1, 1, 0))
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
            Me.gridEditar.DataSource = obj.TraerDataTable("HorarioPE_Solicitudesv2", 0, "%", Date.Now, 0, 0, CInt(Request.QueryString("id")), codigo_lho)
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
        tbCorreo = obj.TraerDataTable("HorarioPE_EnviarCorreo", codigo_lho)
        obj.CerrarConexion()
        obj = Nothing

        'Dim objCorreo As New ClsEnvioMailAmbiente  '23/10 Se comenta
        'Dim bodycorreo As String   '23/10 Se comenta
        Dim strMensaje As String  '23/10
        'Dim cls As New ClsMail  '23/10 Se cambia de Clase
        Dim header, ceco, desc, body, dia, foot, firma, amb, horario, ubica, asunto, envia_a As String
        Dim fec_ini As Date
        Dim capa, cod_per As Integer '28/10

        header = tbCorreo.Rows(0).Item("header")
        ceco = tbCorreo.Rows(0).Item("cco")
        desc = tbCorreo.Rows(0).Item("descripcion")
        body = tbCorreo.Rows(0).Item("body")
        dia = tbCorreo.Rows(0).Item("dia")
        foot = tbCorreo.Rows(0).Item("footer")
        cod_per = tbCorreo.Rows(0).Item("Cod_Per") '28/10 se añade
        firma = tbCorreo.Rows(0).Item("firma")
        fec_ini = tbCorreo.Rows(0).Item("fechaInicio")
        amb = tbCorreo.Rows(0).Item("Ambiente")
        horario = tbCorreo.Rows(0).Item("Horario")
        capa = tbCorreo.Rows(0).Item("capacidad")
        ubica = tbCorreo.Rows(0).Item("ubicacion")
        asunto = tbCorreo.Rows(0).Item("SubjectA")

        strMensaje = "<html>"
        strMensaje = strMensaje & "<body style=""font-size:12px;text-align:justify; font-family:Tahoma;"">"
        strMensaje = strMensaje & "<table style=""color:#284775; Background-color:white; border-color:#284775; border:1px solid; padding:10px;"">"
        strMensaje = strMensaje & "<p><b>" & header & "</b></p>"
        strMensaje = strMensaje & "<p>" & ceco & "</p>"
        strMensaje = strMensaje & "<p>" & desc & "</p>"
        strMensaje = strMensaje & "<p>" & body & "</p>"
        strMensaje = strMensaje & "<table style=""font-size:12px;font-family:Tahoma;border:#99bae2 1px solid;"" cellSpacing=0 cellPadding=4 border=""0"">"
        strMensaje = strMensaje & "<tr style=""color:  #284775; background-color:#E8EEF7; font-weight:bold;""><td>Día</td><td>Fecha</td><td>Ambiente</td><td>Horario</td><td>Capacidad</td><td>Ubicación</td></tr>"
        strMensaje = strMensaje & "<tr><td>" & dia & "</td><td>" & fec_ini & "</td><td>" & amb & "</td><td>" & horario & "</td><td style=""text-align:center;"">" & capa & "</td><td>" & ubica & "</td></tr>"
        strMensaje = strMensaje & "</table>"
        strMensaje = strMensaje & "<p>" & IIf(Me.chkCorreo.Checked, txtTexto.Text.Trim, "") & "</p>"
        strMensaje = strMensaje & "<p>" & foot & "</p>"
        strMensaje = strMensaje & "<p> Atte,<br/><b>" & firma & "</b></p>"
        strMensaje = strMensaje & "</table>"
        strMensaje = strMensaje & "</body></html>"

        If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
            envia_a = tbCorreo.Rows(0).Item("EnviarA") & "; " & tbCorreo.Rows(0).Item("cc") 'Si es para Producción
        Else
            envia_a = "cgastelo@usat.edu.pe" '& "; " & tbCorreo.Rows(0).Item("cc") 'Para Producción
        End If

        '23/10 Cambia a Clase EnviarMail
        'If cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", tbCorreo.Rows(0).Item("EnviarA"), tbCorreo.Rows(0).Item("SubjectA") & " - Módulo de Solicitud de Ambientes", strMensaje, True, tbCorreo.Rows(0).Item("cc"), "") Then

        '28/10 Se añade
        Dim obj1 As New ClsConectarDatos
        obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString       
        Dim ds As New Data.DataTable
        obj1.AbrirConexion()
        ds = obj1.TraerDataTable("ConsultaDatosColaborador", cod_per)
        obj1.CerrarConexion()

        Dim obj2 As New ClsConectarDatos
        obj2.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tb As New Data.DataTable
        obj2.AbrirConexion()
        tb = obj2.TraerDataTable("InsertaEnvioCorreosMasivo", ds.Rows(0).Item("codigo_Pso"), asunto & " - Módulo de Solicitud de Ambientes", envia_a, strMensaje, 55, "mfhuidobro@usat.edu.pe", "")
        obj2.CerrarConexion()

        If tb.Rows.Count Then

            If tb.Rows(0).Item("Respuesta") = 1 Then
                Return True 'Insertó
            Else
                Return False
            End If

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

    Protected Sub ddlFecha_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFecha.SelectedIndexChanged
        CargarCco()
        cargarAmbiente()
        cargarSolicitante()
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
End Class


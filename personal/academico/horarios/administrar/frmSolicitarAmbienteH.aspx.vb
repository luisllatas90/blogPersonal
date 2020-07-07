Partial Class administrativo_pec_frmSolicitarAmbiente
    Inherits System.Web.UI.Page
    Sub CargarDatos()
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.ddlAudio, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "audio", 0, "T"), "codigo_camb", "descripcion_cam")
            ClsFunciones.LlenarListas(Me.ddlDis, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "distribución", 0, "T"), "codigo_camb", "descripcion_cam")
            ClsFunciones.LlenarListas(Me.ddlOtros, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "otros", 0, "T"), "codigo_camb", "descripcion_cam")
            ClsFunciones.LlenarListas(Me.ddlSillas, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "sillas", 0, "T"), "codigo_camb", "descripcion_cam")
            ClsFunciones.LlenarListas(Me.ddlVideo, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "video", 0, "T"), "codigo_camb", "descripcion_cam")
            ClsFunciones.LlenarListas(Me.ddlVenti, obj.TraerDataTable("Ambiente_ListarCaracteristicas", "ventilación", 0, "T"), "codigo_camb", "descripcion_cam")
            ClsFunciones.LlenarListas(Me.ddlTipoAmbiente, obj.TraerDataTable("AsignarAmbiente_ListarAmbientes"), "codigo_tam", "descripcion_Tam", "<<TODOS>>")
            Me.ddlHorarios.DataSource = obj.TraerDataTable("HorarioPE_Consultar", Session("h_id"), "O")
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
        Catch ex As Exception
            Exit Sub
        End Try
        'solicitud_auditorio
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            CargarDatos()
        End If
        'Para asignar o solicitar
        If Page.Request.QueryString("codigo_amb") > 0 And Page.Request.QueryString("estado") <> "" Then
            Dim codigo_lho As Integer = 0
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            codigo_lho = Session("h_codigolho")
            obj.AbrirConexion()
            obj.Ejecutar("HorarioPE_RegistrarAmbienteSol", codigo_lho, CInt(Page.Request.QueryString("codigo_amb")), Page.Request.QueryString("estado"))
            If EnviarCorreo(codigo_lho) Then
                Session("h_codigolho") = 0
                Response.Redirect("frmPrestamoAlquilerAmbiente.aspx?id=" & Session("h_id") & "&ctf=" & Request.QueryString("ctf"))
            Else
                Response.Write("<script>alert('Ocurrió un error al enviar el correo 1')</script>")
            End If
        End If
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
                'tbCorreo.Rows(0).Item("EnviarA") = "ravalos@usat.edu.pe"
                'tbCorreo.Rows(0).Item("cc") = ""
                objCorreo.EnviarMailAd("campusvirtual@usat.edu.pe", tbCorreo.Rows(0).Item("firma"), tbCorreo.Rows(0).Item("EnviarA"), tbCorreo.Rows(0).Item("SubjectA") & " - Módulo de Solicitud de Ambientes", bodycorreo, True, tbCorreo.Rows(0).Item("cc"))
                Return True
            Catch ex As Exception
                Response.Write("<script>alert('" & ex.Message & "')</script>")
            End Try
        Else
            objCorreo.EnviarMailAd("campusvirtual@usat.edu.pe", "error", "yperez@usat.edu.pe", "error - Módulo de Solicitud de Ambientes", codigo_lho, True, "")
            Return True
        End If
    End Function
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tb As New Data.DataTable
        Dim tbAmb As New Data.DataTable
        Dim idsamb As String = ""
        obj.AbrirConexion()
        tb = obj.TraerDataTable("HorarioPE_ConsultarAsignacionAmbienteSol2", ddlHorarios.SelectedValue, ddlAudio.SelectedValue, ddlVideo.SelectedValue, ddlSillas.SelectedValue, ddlDis.SelectedValue, ddlOtros.SelectedValue, Me.ddlVenti.SelectedValue, Me.ddlTipoAmbiente.SelectedValue)
        Session("h_codigolho") = Me.ddlHorarios.SelectedValue
        obj.CerrarConexion()
        If tb.Rows.Count Then
            For i As Integer = 0 To tb.Rows.Count - 1
                idsamb = idsamb & tb.Rows(i).Item("codigo_amb").ToString & ","
            Next
            idsamb = idsamb.ToString.Substring(0, idsamb.Length - 1)
            obj.AbrirConexion()
            Me.gridAmbientes.DataSource = obj.TraerDataTable("Ambiente_ListarAmbienteCaracSol", idsamb)
            Me.gridAmbientes.DataBind()
            obj.CerrarConexion()
        Else
            Me.gridAmbientes.DataSource = Nothing
            Me.gridAmbientes.DataBind()
        End If
        obj = Nothing
    End Sub

    Protected Sub gridAmbientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAmbientes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If gridAmbientes.DataKeys(e.Row.RowIndex).Values("accion").ToString > 0 Then
                Dim tb As New Data.DataTable
                Dim ultimo As Integer = e.Row.Cells.Count
                tb = gridAmbientes.DataSource

                For i As Integer = 3 To tb.Columns.Count  '+ 1
                    If e.Row.Cells(i - 1).Text = "1" Then
                        e.Row.Cells(i - 1).Text = "<img src='images/yes.png' title=""si"">"
                    End If
                    If e.Row.Cells(i - 1).Text = "0" Then
                        e.Row.Cells(i - 1).Text = "<img src='images/no.png'  title=""no"" >"
                    End If
                    'Preferencial
                    If e.Row.Cells(0).Text = "S" Then
                        e.Row.Cells(0).Text = "<img src='images/star.png' title='Ambiente preferencial'>"
                        e.Row.Cells(ultimo - 16).Text = "<a href=""" & "frmSolicitarAmbienteH.aspx?codigo_amb=" & gridAmbientes.DataKeys(e.Row.RowIndex).Values("accion").ToString & "&estado=P" & "&ctf=" & Request.QueryString("ctf") & """><img src=""images/savego.png"" style=""border:0px;"" alt=""Solicitar""></img> Solicitar</a>"
                    End If
                    'Normal
                    If e.Row.Cells(0).Text = "N" Then
                        e.Row.Cells(0).Text = "<img src='images/door.png' title='Ambiente'>"
                        e.Row.Cells(ultimo - 16).Text = "<a href=""" & "frmSolicitarAmbienteH.aspx?codigo_amb=" & gridAmbientes.DataKeys(e.Row.RowIndex).Values("accion").ToString & "&estado=A" & "&ctf=" & Request.QueryString("ctf") & """><img src=""images/save.png"" style=""border:0px;"" alt=""Asignar""></img> Asignar</a>"
                    End If
                Next
                'No mostrar nombres reales
                'e.Row.Cells(1).Text = "Ambiente Disponible N° " & (e.Row.RowIndex + 1).ToString
                tb.Dispose()
            End If
        End If
    End Sub
    Protected Sub btnCancelar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar0.Click
        Response.Redirect("frmPrestamoAlquilerAmbiente.aspx?id=" & Session("h_id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub
    Sub VerificarSolicitudAuditorio()
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
    End Sub
    Protected Sub ddlHorarios_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlHorarios.SelectedIndexChanged
        VerificarSolicitudAuditorio()

    End Sub
End Class

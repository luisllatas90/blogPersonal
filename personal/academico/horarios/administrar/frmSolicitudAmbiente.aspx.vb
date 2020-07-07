Partial Class academico_horarios_frmSolicitudAmbiente
    Inherits System.Web.UI.Page


    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim fecha As Date
        fecha = Today()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("HorarioPE_Solicitudes", Me.ddlCco.SelectedValue, Me.ddlEstado.SelectedValue, fecha.AddDays(CInt(Me.ddlFecha.SelectedValue) * +1), IIf(Me.ddlFecha.SelectedValue = 0, 0, 1), IIf(Me.ddlFecha.SelectedValue = -1, 1, 0), CInt(Request.QueryString("id")))
        If tb.Rows.Count Then
            Me.gvSolicitud.DataSource = tb
        Else
            Me.gvSolicitud.DataSource = Nothing
        End If
        Me.gvSolicitud.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub gvSolicitud_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSolicitud.RowDataBound
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tb As New Data.DataTable
        Dim index As Integer
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddlAmb As DropDownList
            ddlAmb = e.Row.FindControl("ddlAmbiente")
            index = e.Row.RowIndex
            If gvSolicitud.DataKeys(e.Row.RowIndex).Values("codigo_Lho").ToString <> 0 Then
                obj.AbrirConexion()
                tb = obj.TraerDataTable("HorarioPE_ConsultarAsignacionAmbienteSol3", gvSolicitud.DataKeys(e.Row.RowIndex).Values("codigo_Lho"), CInt(Request.QueryString("id")))
                obj.CerrarConexion()
                ' Response.Write(gvSolicitud.DataKeys(e.Row.RowIndex).Values("codigo_lho")) '& "," & dpCodigo_pes.SelectedValue & "," & dpCodigo_cac.SelectedValue & "," & tipo)
                If tb.Rows.Count Then
                    For i As Integer = 0 To tb.Rows.Count - 1
                        ddlAmb.Items.Add(New ListItem(tb.Rows(i).Item("ambiente"), tb.Rows(i).Item("codigo_amb")))
                    Next
                    'If gvSolicitud.DataKeys(e.Row.RowIndex).Values("codigo_amb") = 0 Then
                    '    ddlAmb.Items.Add(New ListItem("--Seleccionar--", -1))
                    '    ddlAmb.SelectedValue = -1
                    'Else
                    '    ddlAmb.SelectedValue = gvSolicitud.DataKeys(e.Row.RowIndex).Values("codigo_amb")
                    'End If
                    ddlAmb.SelectedValue = gvSolicitud.DataKeys(e.Row.RowIndex).Values("codigo_amb")
                Else
                    ddlAmb.Items.Add(New ListItem("Sin ambientes disponibles", -1))
                End If

                If (e.Row.Cells(14).Text) = "A" Then
                    e.Row.Cells(3).ForeColor = Drawing.Color.Blue
                Else
                    e.Row.Cells(3).ForeColor = Drawing.Color.Green
                End If

                'obj.AbrirConexion()
                'Dim tbCarac As New Data.DataTable
                'tbCarac = obj.TraerDataTable("Ambiente_ListarCaracteristicas", "", gvSolicitud.DataKeys(e.Row.RowIndex).Values("codigo_amb"), "S")
                'obj.CerrarConexion()

                'Dim audio = "", video = "", sillas = "", distri = "", venti = "", otros = ""
                'For i As Integer = 0 To tbCarac.Rows.Count - 1
                '    If tbCarac.Rows(i).Item("categoria_cam") = "Audio" Then
                '        audio &= tbCarac.Rows(i).Item("descripcion_cam") & "<br/>"
                '    End If
                '    If tbCarac.Rows(i).Item("categoria_cam") = "Video" Then
                '        video &= tbCarac.Rows(i).Item("descripcion_cam") & "<br/>"
                '    End If
                '    If tbCarac.Rows(i).Item("categoria_cam") = "Sillas" Then
                '        sillas &= tbCarac.Rows(i).Item("descripcion_cam") & "<br/>"
                '    End If
                '    If tbCarac.Rows(i).Item("categoria_cam") = "Distribución" Then
                '        distri &= tbCarac.Rows(i).Item("descripcion_cam") & "<br/>"
                '    End If
                '    If tbCarac.Rows(i).Item("categoria_cam") = "Ventilación" Then
                '        venti &= tbCarac.Rows(i).Item("descripcion_cam") & "<br/>"
                '    End If
                '    If tbCarac.Rows(i).Item("categoria_cam") = "Otros" Then

                '        otros &= tbCarac.Rows(i).Item("descripcion_cam") & "<br/>"
                '    End If
                'Next

                'Dim lblAudio, lblVideo, lblSillas, lblDistri, lblVenti, lblOtros As Label
                'lblAudio = e.Row.FindControl("lblAudio")
                'lblVideo = e.Row.FindControl("lblVideo")
                'lblSillas = e.Row.FindControl("lblSillas")
                'lblDistri = e.Row.FindControl("lblDistri")
                'lblVenti = e.Row.FindControl("lblVenti")
                'lblOtros = e.Row.FindControl("lblOtros")
                'lblAudio.Text = IIf(audio = "", "Cualquiera", audio)
                'lblVideo.Text = IIf(video = "", "Cualquiera", video)
                'lblSillas.Text = IIf(sillas = "", "Cualquiera", sillas)
                'lblDistri.Text = IIf(distri = "", "Cualquiera", distri)
                'lblVenti.Text = IIf(venti = "", "Cualquiera", venti)
                'lblOtros.Text = IIf(otros = "", "Cualquiera", otros)
            End If
            End If
        obj = Nothing
        tb.Dispose()
    End Sub


    Protected Sub gvSolicitud_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvSolicitud.RowDeleting
        Dim Fila As GridViewRow
        Fila = Me.gvSolicitud.Rows(e.RowIndex)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("HorarioPE_AprobarSolicitud", gvSolicitud.DataKeys(e.RowIndex).Values("codigo_Lho"), CType(Fila.FindControl("ddlAmbiente"), DropDownList).SelectedValue)
        obj.CerrarConexion()
        obj = Nothing
        If EnviarCorreo(gvSolicitud.DataKeys(e.RowIndex).Values("codigo_Lho")) Then
            '  btnBuscar_Click(sender, e)
            gvSolicitud.Rows(e.RowIndex).Cells(3).ForeColor = Drawing.Color.Blue            

        Else
            Response.Write("Ocurrió un error")
        End If
        e.Cancel = True
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Me.ddlCco.DataSource = obj.TraerDataTable("HorariosPE_ConsultarCcoSol")
            Me.ddlCco.DataTextField = "descripcion_Cco"
            Me.ddlCco.DataValueField = "codigo_cco"
            Me.ddlCco.DataBind()
            Me.ddlCco.SelectedValue = 0
            obj.CerrarConexion()
            obj = Nothing
            'Me.ddlFecha.SelectedIndex = 3
            btnBuscar_Click(sender, e)
        End If
    End Sub

    Protected Sub gvSolicitud_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvSolicitud.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "LimpiarAmbiente") Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("HorarioPE_LimpiarAmbiente", gvSolicitud.DataKeys(index).Values("codigo_Lho"), 1)
            obj.CerrarConexion()
            obj = Nothing
            If EnviarCorreo(gvSolicitud.DataKeys(index).Values("codigo_Lho")) Then
                ' btnBuscar_Click(sender, e)
                gvSolicitud.Rows(index).ForeColor = Drawing.Color.Green
                'gvSolicitud.DeleteRow(index)
            Else
                Response.Write("Ocurrió un error")
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
        bodycorreo = "<html>"
        bodycorreo = bodycorreo & "<body style=""text-align:justify; font-family:Tahoma;""> <div style=""color:#284775; Background-color:white; border-color:#284775; border:1px solid; padding:10px;"">"
        bodycorreo = bodycorreo & "<p><b>" & tbCorreo.Rows(0).Item("header") & "</b></p>"
        bodycorreo = bodycorreo & "<p>" & tbCorreo.Rows(0).Item("body") & "</p>"
        bodycorreo = bodycorreo & "<table style=""border:#99bae2 1px solid;"" cellSpacing=0 cellPadding=4 border=""0"">"
        bodycorreo = bodycorreo & "<tr style=""color:  #284775; background-color:#E8EEF7; font-weight:bold;""><td>Tipo</td><td>Día</td><td>Fecha Inicio</td><td>Fecha Fin</td><td>Ambiente</td><td>Horario</td><td>Capacidad</td><td>Ubicación</td></tr>"
        bodycorreo = bodycorreo & "<tr><td>" & tbCorreo.Rows(0).Item("tipo") & "</td><td>" & tbCorreo.Rows(0).Item("dia") & "</td><td>" & tbCorreo.Rows(0).Item("fechaInicio") & "</td><td>" & tbCorreo.Rows(0).Item("fechaFin") & "</td><td>" & tbCorreo.Rows(0).Item("Ambiente") & "</td><td>" & tbCorreo.Rows(0).Item("Horario") & "</td><td>" & tbCorreo.Rows(0).Item("capacidad") & "</td><td>" & tbCorreo.Rows(0).Item("ubicacion") & "</td></tr>"
        bodycorreo = bodycorreo & "</table>"
        bodycorreo = bodycorreo & "<p>" & tbCorreo.Rows(0).Item("footer") & "</p>"
        bodycorreo = bodycorreo & "<p> Atte,<br/><b>" & tbCorreo.Rows(0).Item("firma") & "</b></p>"
        bodycorreo = bodycorreo & "</div></body></html>"
		  tbCorreo.Rows(0).Item("cc") = "yperez@usat.edu.pe"
        tbCorreo.Rows(0).Item("EnviarA") = "yperez@usat.edu.pe"
        If objCorreo.EnviarMailAd("campusvirtual@usat.edu.pe", tbCorreo.Rows(0).Item("firma"), tbCorreo.Rows(0).Item("EnviarA"), tbCorreo.Rows(0).Item("SubjectA") & " - Módulo de Solicitud de Ambientes", bodycorreo, True, tbCorreo.Rows(0).Item("cc")) Then
            Return True
        Else
           Return False
        End If
    End Function
    'Response.Write("desde=>" & fecha)
    'Response.Write("hasta=>" & fecha.AddDays(CInt(Me.ddlFecha.SelectedValue) * +1))
    'Response.Write("bit=>" & IIf(Me.ddlFecha.SelectedValue = 0, 0, 1))
    'Response.Write("codigo_cac=>" & Me.ddlCiclo.SelectedValue)



End Class


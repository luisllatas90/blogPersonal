
Partial Class academico_horarios_administrar_frmSolicitudConfirmar
    Inherits System.Web.UI.Page

    Protected Sub gridHorario_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridHorario.RowCommand
        Me.gridHorario.Visible = False
        Me.pnlPregunta.Visible = True
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "LimpiarAmbiente") Then
            Me.lblAcción.Text = "CANCELAR"            
        End If
        If (e.CommandName = "ConfirmarAmbiente") Then
            Me.lblAcción.Text = "CONFIRMAR"            
        End If
        Me.lblFecha.Text = "[" & gridHorario.DataKeys(index).Values("dia_lho") & " " & gridHorario.DataKeys(index).Values("fechaIni_lho") & "]"
        Me.lblActividad.Text = gridHorario.DataKeys(index).Values("descripcion_lho")
        Me.lblAmbiente.Text = gridHorario.DataKeys(index).Values("ambiente")
        Me.codigo_lho.Value = gridHorario.DataKeys(index).Values("codigo_lho")
    End Sub
    Function EnviarCorreo(ByVal codigo_lho As Integer) As Boolean
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tbCorreo As New Data.DataTable
        obj.AbrirConexion()
        tbCorreo = obj.TraerDataTable("HorarioPE_EnviarCorreoNotificar", codigo_lho)
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

                objCorreo.EnviarMailAd("campusvirtual@usat.edu.pe", tbCorreo.Rows(0).Item("firma"), tbCorreo.Rows(0).Item("EnviarA"), tbCorreo.Rows(0).Item("SubjectA") & " - Módulo de Solicitud de Ambientes", bodycorreo, True, tbCorreo.Rows(0).Item("cc"))
                Return True
            Catch ex As Exception
                Response.Write("<script>alert('" & ex.Message & "')</script>")
            End Try
        Else
            Response.Write("<script>alert('a')</script>")
        End If
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            consultarHorarios()
        End If
    End Sub
    Sub consultarHorarios()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dias_desde As Integer, dias_hasta As Integer
        dias_desde = 0
        dias_hasta = 15
        Me.gridHorario.DataSource = obj.TraerDataTable("[HorarioPE_ConsultarSolConfirmar]", CInt(Request.QueryString("id")), dias_desde, dias_hasta)
        Me.gridHorario.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub gridHorario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridHorario.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(7).Text = "1" Then
                e.Row.Cells(7).Text = "<img src='images/star.png' title='Ambiente preferencial'>"

            ElseIf e.Row.Cells(7).Text = "0" And e.Row.Cells(8).Text = "Sin ambiente solicitado" Then
                e.Row.Cells(7).Text = "-"
            ElseIf e.Row.Cells(7).Text = "0" Then
                e.Row.Cells(7).Text = "<img src='images/door.png' title='Ambiente normal'>"
            End If

            e.Row.Cells(8).Font.Bold = True
            e.Row.Cells(3).Font.Bold = True

            If e.Row.Cells(9).Text = "Pendiente" Then
                e.Row.Cells(9).ForeColor = Drawing.Color.Green
                e.Row.Cells(10).Text = "-" 'no puede confirmar una solicitud pendiente
            Else
                e.Row.Cells(9).ForeColor = Drawing.Color.Blue
            End If

            'If e.Row.Cells(9).Text = "Pendiente" And e.Row.Cells(8).Text <> "Sin ambiente solicitado" Then
            '    e.Row.Cells(10).Text = "-"
            'End If
            'If e.Row.Cells(9).Text = "Asignado" Then
            '    e.Row.Cells(10).Text = "-"
            'End If
            If e.Row.Cells(8).Text = "Sin ambiente solicitado" Then
                e.Row.Cells(11).Text = "-"
                e.Row.Cells(10).Text = "-"
            End If


            If CDate(e.Row.Cells(2).Text) < CDate(Today) Then
                e.Row.Cells(9).ForeColor = Drawing.Color.Gray
                e.Row.Cells(9).Text = e.Row.Cells(9).Text & " - Finalizado"
            End If
            If CDate(e.Row.Cells(2).Text) = CDate(Today) Then
                e.Row.Cells(9).ForeColor = Drawing.Color.IndianRed
                e.Row.Cells(9).Text = e.Row.Cells(9).Text & " - Hoy"
            End If

            If e.Row.Cells(12).Text = "Confirmada" Then
                e.Row.Cells(12).ForeColor = Drawing.Color.Blue
                e.Row.Cells(10).Text = ""

            ElseIf e.Row.Cells(12).Text = "Cancelada" Then
                e.Row.Cells(12).ForeColor = Drawing.Color.Red
                e.Row.Cells(11).Text = ""

            Else
                e.Row.Cells(12).ForeColor = Drawing.Color.Green
            End If

        End If
    End Sub

    Protected Sub btnSi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSi.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        If Me.lblAcción.Text = "CONFIRMAR" Then
            obj.Ejecutar("HorarioPE_ActualizarEstado2", Me.codigo_lho.Value, "C")
        End If
        If Me.lblAcción.Text = "CANCELAR" Then
            obj.Ejecutar("HorarioPE_LimpiarAmbiente", Me.codigo_lho.Value, 1)
            obj.Ejecutar("HorarioPE_ActualizarEstado2", Me.codigo_lho.Value, "L")
        End If
        EnviarCorreo(Me.codigo_lho.Value)
        consultarHorarios()
        obj.CerrarConexion()
        obj = Nothing
        Me.pnlPregunta.Visible = False
        Me.codigo_lho.Value = 0
        Me.gridHorario.Visible = True
    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        Me.pnlPregunta.Visible = False
        Me.codigo_lho.Value = 0
        Me.gridHorario.Visible = True
        Me.gridHorario.DataBind()
    End Sub
End Class

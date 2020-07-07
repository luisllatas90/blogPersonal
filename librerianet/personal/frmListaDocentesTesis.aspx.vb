
Partial Class personal_frmListaDocentesTesis
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ListaDocentesHorasTesis()
        End If
    End Sub

    Private Sub ListaDocentesHorasTesis()
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsPersonal

            dts = obj.HorasTesisDiferentes
            If dts.Rows.Count > 0 Then
                gvListaTrabajadores.DataSource = dts
                gvListaTrabajadores.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaTrabajadores_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaTrabajadores.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(0).Text = e.Row.RowIndex + 1

                If e.Row.Cells(11).Text = "0" Then
                    e.Row.Cells(11).Text = "<center><img src='../images/email_close_docente.png' alt='" & "No enviado" & "'  style='border: 0px'/></center>"
                Else
                    e.Row.Cells(11).Text = "<center><img src='../images/email_forward_docente.png' alt='" & "No enviado" & "'  style='border: 0px'/></center>"
                End If

                'Response.Write(gvListaTrabajadores.DataKeys(e.Row.RowIndex).Values(0))
                'Response.Write("<br />")

                Dim codigo_per As Integer = gvListaTrabajadores.DataKeys(e.Row.RowIndex).Values(0)
                If codigo_per <> 0 Then
                    Dim combo As DropDownList = DirectCast(e.Row.FindControl("ddlListaTesis"), DropDownList)
                    combo.ClearSelection()
                    If combo IsNot DBNull.Value Then
                        Me.prcCargarComboGridView(combo, codigo_per)
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Sub prcCargarComboGridView(ByVal cboCombo As DropDownList, ByVal codigo_per As Integer)
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable

            'Lista todas las tesis de los alumnos matriculados, en el ciclo actual./ Ciclo actual por defecto.
            dts = obj.ListaTesisDocente(codigo_per)
            cboCombo.DataSource = dts
            cboCombo.DataTextField = "titulo_tes"
            cboCombo.DataValueField = "codigo_tes"
            cboCombo.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub img_EnviarCorreo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_EnviarCorreo.Click
        Try
            If txtAsunto.Text.Trim = "" Then
                lblMensaje.ForeColor = Drawing.Color.Red
                lblMensaje.Text = "Favor de ingresar el asunto del correo a enviar."
                Exit Sub
            End If

            If txtMensaje.Text.Trim = "" Then
                lblMensaje.ForeColor = Drawing.Color.Red
                lblMensaje.Text = "Favor de ingresar un mensaje de alerta, referente a las horas de tesis."
                Exit Sub
            End If

            If (validaCheckActivo() = True) Then
                img_EnviarCorreo.Enabled = False
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Blue
                lblMensaje.ForeColor = Drawing.Color.Blue
                lblMensaje.Text = "   El Email fue enviado correctamente."
                img_EnviarCorreo.Enabled = False
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "refresh", "window.setTimeout('var url = window.location.href;window.location.href = url',3000);", True)
            Else
                lblMensaje.ForeColor = Drawing.Color.Red
                lblMensaje.Text = "   Favor de seleccionar por lo menos un docente de la lista para enviar el correo."
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function validaCheckActivo() As Boolean
        Dim obj As New clsPersonal
        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Dim vCodigo_per As Integer
        Dim Trabajador As String
        Dim dts As New Data.DataTable

        Dim idPer As Integer
        idPer = Request.QueryString("id")

        For i As Integer = 0 To gvListaTrabajadores.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = gvListaTrabajadores.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkSel"), CheckBox).Checked
            If (valor = True) Then
                sw = 1

                'Seleccionaloes el codigo de la variable, para poder generar los valores 
                vCodigo_per = Convert.ToInt32(gvListaTrabajadores.DataKeys(Fila.RowIndex).Value)
                Trabajador = Me.gvListaTrabajadores.Rows(i).Cells(3).Text

                'Envia un Email de comunicado, y no realiza ninguna accion. 
                obj.EnviarEmailComunicado(vCodigo_per, txtMensaje.Text.Trim, Trabajador, idPer, txtAsunto.Text.Trim)

                'Actualizamos los dos campos agregado en la tabla Datos personal.
                obj.ActualizarEstadosEnvio(vCodigo_per)
            End If
        Next

        If (sw = 1) Then
            Return True
        End If
        Return False
    End Function

End Class

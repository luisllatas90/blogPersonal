
Partial Class GestionInvestigacion_FrmListaJurado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        If IsPostBack = False Then
            ListarCicloAcademico()
            'ListarDocentes()
            'If Request("ctf") <> 1 Then
            '    Dim obj As New ClsGestionInvestigacion
            '    Me.ddlDocente.SelectedValue = obj.EncrytedString64(Session("id_per"))
            'End If
        End If
    End Sub

    Private Sub ListarCicloAcademico()
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ConsultarCicloAcademico("CVP", "")
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.ddlSemestre.Items.Add(New ListItem(dt.Rows(i).Item("descripcion_cac"), dt.Rows(i).Item("codigo_cac")))
        Next
    End Sub

    Private Sub ListarDocentes()
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        Me.ddlDocente.Items.Clear()
        Me.ddlDocente.Items.Add(New ListItem("-- Seleccione --", ""))
        dt = obj.ListarDocenteJuradoTesis(Me.ddlSemestre.SelectedValue, Me.ddlEtapa.SelectedValue, Session("id_per"), Request("ctf"))
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.ddlDocente.Items.Add(New ListItem(dt.Rows(i).Item("descripcion"), dt.Rows(i).Item("codigo")))
        Next
    End Sub

    Private Sub ListarTesisJurado(ByVal codigo_cac As Integer, ByVal abrev_etapa As String, ByVal codigo_per As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListarTesisJurado(codigo_cac, abrev_etapa, codigo_per, Session("id_per"), Request("ctf"))
        If dt.Rows.Count > 0 Then
            Me.gvTesisJurado.DataSource = dt
            Me.gvTesisJurado.DataBind()
            'Me.txtNota.Text = dt.Rows(0).Item("nota").ToString
        Else
            Me.gvTesisJurado.DataSource = ""
            Me.gvTesisJurado.DataBind()
        End If
        
    End Sub

    Private Sub ListarObservaciones()
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListarObservacionesJurado(Me.hdcodtes.Value, Me.hdcodjur.Value, Me.hdcodcac.Value)
        If dt.Rows.Count > 0 Then
            Me.gvDetalleObservaciones.DataSource = dt
            Me.hdcodObs.Value = dt.Rows(0).Item("codigo_obt")
            Me.txtNota.Text = dt.Rows(0).Item("nota").ToString
        Else
            dt = Nothing
        End If
        Me.gvDetalleObservaciones.DataBind()
    End Sub

    Protected Sub ddlSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemestre.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        If Me.ddlSemestre.SelectedValue <> "" And Me.ddlEtapa.SelectedValue <> "" Then
            ListarDocentes()
        End If
        Me.gvTesisJurado.DataSource = ""
        Me.gvTesisJurado.DataBind()
    End Sub

    Protected Sub ddlEtapa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEtapa.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        If Me.ddlSemestre.SelectedValue <> "" And Me.ddlEtapa.SelectedValue <> "" Then
            ListarDocentes()
        End If
        Me.gvTesisJurado.DataSource = ""
        Me.gvTesisJurado.DataBind()
    End Sub

    Protected Sub ddlDocente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDocente.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Me.gvTesisJurado.DataSource = ""
        Me.gvTesisJurado.DataBind()
        If Me.ddlDocente.SelectedValue <> "" And ddlSemestre.SelectedValue <> "" And ddlEtapa.SelectedValue <> "" Then
            ListarTesisJurado(Me.ddlSemestre.SelectedValue, Me.ddlEtapa.SelectedValue, Me.ddlDocente.SelectedValue)
        End If
    End Sub

    Protected Sub gvTesisJurado_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTesisJurado.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        If e.CommandName = "Observar" Then
            Me.hdcodtes.Value = Me.gvTesisJurado.DataKeys(e.CommandArgument).Values("codigo_tes")
            Me.hdcodjur.Value = Me.gvTesisJurado.DataKeys(e.CommandArgument).Values("codigo_jur")
            Me.hdcodcac.Value = Me.gvTesisJurado.DataKeys(e.CommandArgument).Values("codigo_cac")
            Me.txtNota.Text = ""
            ListarObservaciones()
            Me.mdRegistro.Attributes.Remove("class")
            Me.mdRegistro.Attributes.Add("class", "modal fade in")
            Me.mdRegistro.Attributes.CssStyle.Remove("display")
            Me.mdRegistro.Attributes.CssStyle.Add("display", "block")
            ListarTesisJurado(Me.ddlSemestre.SelectedValue, Me.ddlEtapa.SelectedValue, Me.ddlDocente.SelectedValue)
        End If
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        'Ocultar Modal de Confirmación
        Me.mdRegistro.Attributes.Remove("class")
        Me.mdRegistro.Attributes.Add("class", "modal fade")
        Me.mdRegistro.Attributes.CssStyle.Remove("display")
        Me.mdRegistro.Attributes.CssStyle.Add("display", "none")
        LimpiarControles()
        If Me.ddlDocente.SelectedValue <> "" And ddlSemestre.SelectedValue <> "" And ddlEtapa.SelectedValue <> "" Then
            ListarTesisJurado(Me.ddlSemestre.SelectedValue, Me.ddlEtapa.SelectedValue, Me.ddlDocente.SelectedValue)
        End If
    End Sub

    Private Sub LimpiarControles()
        Me.ddlTIpo.SelectedValue = ""
        Me.txtObservacion.Text = ""
        Me.hdcodObs.Value = "0"
        Me.hdcodtes.Value = "0"
        Me.hdcodjur.Value = "0"
        Me.hdcodcac.Value = "0"
        Me.lblRegistro.Attributes.Remove("class")
        Me.lblRegistro.InnerText = ""
    End Sub

    Protected Sub gvTesisJurado_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTesisJurado.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If DataBinder.Eval(e.Row.DataItem, "archivo") = "" Then
                e.Row.Cells(5).Text = "Sin Adjunto"
                e.Row.Cells(5).ForeColor = Drawing.Color.Red
            Else
                Dim lb As New LinkButton
                lb.Text = "Descargar"
                lb.OnClientClick = "fnDownload('" + DataBinder.Eval(e.Row.DataItem, "archivo") + "');return false;"
                e.Row.Cells(5).Controls.Add(lb)
                e.Row.Cells(5).ForeColor = Drawing.Color.Red
            End If

            If DataBinder.Eval(e.Row.DataItem, "archivoacta") = "" Then
                e.Row.Cells(7).Text = "Sin Adjunto"
                e.Row.Cells(7).ForeColor = Drawing.Color.Red
            Else
                Dim lb As New LinkButton
                lb.Text = "Descargar"
                lb.OnClientClick = "fnDownload('" + DataBinder.Eval(e.Row.DataItem, "archivoacta") + "');return false;"
                e.Row.Cells(7).Controls.Add(lb)
                e.Row.Cells(7).ForeColor = Drawing.Color.Red
            End If

            If Me.gvTesisJurado.DataKeys(e.Row.RowIndex).Values("linkinforme") = "" Then
                e.Row.Cells(8).Text = ""
                e.Row.Cells(8).ForeColor = Drawing.Color.Red
            Else
                Dim lb As New LinkButton
                lb.Text = "link"
                lb.CssClass = "btn btn-sm btn-warning btn-radius"
                lb.OnClientClick = "window.open('" + Me.gvTesisJurado.DataKeys(e.Row.RowIndex).Values("linkinforme") + "');return false;"
                e.Row.Cells(8).Controls.Add(lb)
                e.Row.Cells(8).ForeColor = Drawing.Color.Red
            End If
        End If
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Me.lblRegistro.Attributes.Remove("class")
        Me.lblRegistro.InnerText = ""
        If Me.ddlTIpo.SelectedValue <> "" Then
            If Me.txtObservacion.Text <> "" Then
                Dim obj As New ClsGestionInvestigacion
                Dim dt As New Data.DataTable
                dt = obj.AgregarObservacionTesis(Me.hdcodObs.Value, Me.hdcodtes.Value, Me.hdcodjur.Value, Me.hdcodcac.Value, 0, Me.ddlTIpo.SelectedValue, Me.txtObservacion.Text)
                If dt.Rows(0).Item("Respuesta") = 1 Then
                    Me.hdcodObs.Value = dt.Rows(0).Item("cod")
                    Me.lblRegistro.InnerText = dt.Rows(0).Item("Mensaje")
                    Me.lblRegistro.Attributes.Add("class", "alert alert-success")
                    Me.ddlTIpo.SelectedValue = ""
                    Me.txtObservacion.Text = ""
                Else
                    Me.lblRegistro.InnerText = "No se pudo agregar Observación"
                    Me.lblRegistro.Attributes.Add("class", "alert alert-danger")
                End If

            Else
                Me.lblRegistro.InnerText = "Debe ingresar una descripción de la observación"
                Me.lblRegistro.Attributes.Add("class", "alert alert-danger")
            End If
        Else
            Me.lblRegistro.InnerText = "Debe seleccionar un tipo de observación"
            Me.lblRegistro.Attributes.Add("class", "alert alert-danger")
        End If
        ListarObservaciones()

    End Sub

    Protected Sub gvDetalleObservaciones_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvDetalleObservaciones.PageIndexChanging
        Me.gvDetalleObservaciones.PageIndex = e.NewPageIndex
        ListarObservaciones()
    End Sub

    Protected Sub gvDetalleObservaciones_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDetalleObservaciones.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        If e.CommandName = "Eliminar" Then
            Dim obj As New ClsGestionInvestigacion
            Dim dt As New Data.DataTable
            dt = obj.EliminarObservacionTesis(Me.gvDetalleObservaciones.DataKeys(e.CommandArgument).Values("codigo_dot"))
            If dt.Rows(0).Item("Respuesta") = 1 Then
                Me.hdcodObs.Value = dt.Rows(0).Item("cod")
                Me.lblRegistro.InnerText = dt.Rows(0).Item("Mensaje")
                Me.lblRegistro.Attributes.Add("class", "alert alert-success")
            Else
                Me.lblRegistro.InnerText = "No se pudo eliminar Observación"
                Me.lblRegistro.Attributes.Add("class", "alert alert-danger")
            End If
            ListarObservaciones()
        End If
    End Sub

    Protected Sub btnGuardarNota_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarNota.Click
        Me.lblRegistro.Attributes.Remove("class")
        Me.lblRegistro.InnerText = ""
        If Me.gvDetalleObservaciones.Rows.Count > 0 Then
            If ValidaSoloNumeros0a20(Me.txtNota.Text) = True Then
                Dim obj As New ClsGestionInvestigacion
                Dim dt As New Data.DataTable
                dt = obj.AgregarNotaObservacionTesis(Me.hdcodObs.Value, Me.hdcodtes.Value, Me.hdcodjur.Value, Me.hdcodcac.Value, Me.txtNota.Text)
                If dt.Rows(0).Item("Respuesta") = 1 Then
                    Me.hdcodObs.Value = dt.Rows(0).Item("cod")
                    Me.lblRegistro.InnerText = dt.Rows(0).Item("Mensaje")
                    Me.lblRegistro.Attributes.Add("class", "alert alert-success")
                Else
                    Me.lblRegistro.InnerText = "No se pudo Actualizar Nota"
                    Me.lblRegistro.Attributes.Add("class", "alert alert-danger")
                End If
            Else
                Me.lblRegistro.InnerText = "solo se puede colocar una nota de 0 a 20"
                Me.lblRegistro.Attributes.Add("class", "alert alert-danger")
            End If
        Else
            Me.lblRegistro.InnerText = "para colocar nota se debe agregar al menos una observación"
            Me.lblRegistro.Attributes.Add("class", "alert alert-danger")
        End If
    End Sub

    Function ValidaSoloNumeros0a20(ByVal cadena As String) As Boolean
        Try
            Dim estructura As String = "^([0-9]|[1][0-9]|20)$"
            Dim match As Match = Regex.Match(cadena.Trim(), estructura, RegexOptions.IgnoreCase)
            If match.Success Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub btnCerrarModal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrarModal.Click
        'Ocultar Modal de Confirmación
        Me.mdRegistro.Attributes.Remove("class")
        Me.mdRegistro.Attributes.Add("class", "modal fade")
        Me.mdRegistro.Attributes.CssStyle.Remove("display")
        Me.mdRegistro.Attributes.CssStyle.Add("display", "none")
        LimpiarControles()
        If Me.ddlDocente.SelectedValue <> "" And ddlSemestre.SelectedValue <> "" And ddlEtapa.SelectedValue <> "" Then
            ListarTesisJurado(Me.ddlSemestre.SelectedValue, Me.ddlEtapa.SelectedValue, Me.ddlDocente.SelectedValue)
        End If
    End Sub




End Class

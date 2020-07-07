﻿Partial Class personal_academico_tesis_frmcambiaretapatesis
    Inherits System.Web.UI.Page
    Dim TesisAnterior As Int32 = -1
    Dim AutorAnterior As Int32 = -1
    Dim PrimeraFila As Int32 = -1
    Dim Contador As Int32 = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ClsFunciones.LlenarListas(Me.dpFase, obj.TraerDataTable("TES_ConsultarEtapaInvestigacionTesis", 0, 0, 0, 0), "codigo_Eti", "nombre_Eti")

            If Request.QueryString("ctf") = 1 Then
                If Request.QueryString("mod") = 10 Then
                    ClsFunciones.LlenarListas(Me.dpEscuela, obj.TraerDataTable("ConsultarCarreraProfesional", "GO", 0), "codigo_cpf", "nombre_cpf", "-Seleccione la Carrera Profesional-")
                Else
                    ClsFunciones.LlenarListas(Me.dpEscuela, obj.TraerDataTable("ConsultarCarreraProfesional", "MA", 0), "codigo_cpf", "nombre_cpf", "-Seleccione la Carrera Profesional-")
                End If
            Else
                ClsFunciones.LlenarListas(Me.dpEscuela, obj.TraerDataTable("consultaracceso", "ESC", "", Session("id_per")), "codigo_cpf", "nombre_cpf", "-Seleccione la Carrera Profesional-")
            End If
            Me.dpEscuela.SelectedIndex = 0
            Me.txtFechaAprobacion.Text = Now.Date
            Me.txtFechaAprobacion.Attributes.Add("OnKeyDown", "return false")
            Me.dpAcciones.Attributes.Add("OnChange", "MostrarVentanaEstado(this.value)")
            Me.dpFase.Attributes.Add("OnChange", "OcultarTabla()")
            Me.imgGuardar.Attributes.Add("Style", "display:none")
        End If
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            Contador = Contador + 1
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "HabilitarEnvio(this)")
            'Combinar celdas
            If TesisAnterior = fila("codigo_tes") Then
                'Incrementar the fila combinada
                'If Me.GridView1.Rows(PrimeraFila).Cells(2).RowSpan = 0 Then
                '    Me.GridView1.Rows(PrimeraFila).Cells(2).RowSpan = 2
                'Else
                '    Me.GridView1.Rows(PrimeraFila).Cells(2).RowSpan += 1
                '    'Quitar la celda
                '    e.Row.Cells.RemoveAt(2)
                'End If
                e.Row.Cells(0).Text = ""
                e.Row.Cells(1).Text = ""
                e.Row.Cells(2).Text = ""
                e.Row.Cells(3).Text = ""
                e.Row.Cells(8).Text = ""
                Contador = Contador - 1
            Else
                e.Row.Cells(0).CssClass = "bordesup"
                e.Row.Cells(1).CssClass = "bordesup"
                e.Row.Cells(2).CssClass = "bordesup"
                e.Row.Cells(3).CssClass = "bordesup"
                e.Row.Cells(4).CssClass = "bordesup"
                e.Row.Cells(5).CssClass = "bordesup"
                e.Row.Cells(6).CssClass = "bordesup"
                e.Row.Cells(7).CssClass = "bordesup"
                e.Row.Cells(8).CssClass = "bordesup"
                e.Row.VerticalAlign = VerticalAlign.Middle
                TesisAnterior = fila("codigo_tes").ToString()
                PrimeraFila = e.Row.RowIndex

                e.Row.Cells(0).Text = Contador
            End If
            'Combiar los autores que se repiten
            If AutorAnterior = fila("codigo_alu") Then
                e.Row.Cells(4).Text = ""
            Else
                e.Row.Cells(4).CssClass = "bordesup"
                e.Row.Cells(5).CssClass = "bordesup"
                e.Row.Cells(6).CssClass = "bordesup"
                e.Row.Cells(7).CssClass = "bordesup"
                e.Row.Cells(8).CssClass = "bordesup"
                AutorAnterior = fila("codigo_alu")
            End If
        End If
    End Sub
    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.GridView1.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.GridView1)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=tesisporetapa.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Protected Sub dpEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpEscuela.SelectedIndexChanged
        Me.dpCurso.Items.Clear()
        Me.imgBuscar.Visible = False
        Me.GridView1.Visible = False
        Me.cmdExportar.Visible = False

        If Me.dpEscuela.SelectedValue > 0 Then
            Me.imgBuscar.Visible = True
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ClsFunciones.LlenarListas(Me.dpCurso, obj.TraerDataTable("TES_ConsultarEtapaInvestigacionTesis", 4, Me.dpEscuela.SelectedValue, 0, 0), "codigo_cur", "nombre_cur", "--Todos los Seminarios de Tesis--")
            obj = Nothing
        End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim I As Integer
        Dim Fila As GridViewRow
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        Try
            obj.IniciarTransaccion()
            For I = 0 To Me.GridView1.Rows.Count - 1
                Fila = Me.GridView1.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        '==================================
                        ' Guardar los datos
                        '==================================
                        obj.Ejecutar("TES_AgregarEtapaInvestigacionTesis", CDate(Me.txtFechaAprobacion.Text).ToShortDateString, Me.GridView1.DataKeys.Item(Fila.RowIndex).Values(0), 1, Me.hdFase.Value, Session("id_per"), Me.TxtComentario.Text.Trim, Me.chkBloquear.Checked)
                    End If
                End If
            Next
            obj.TerminarTransaccion()
            obj = Nothing
            Page.RegisterStartupScript("CambioEstado", "<script>alert('Se han guardado los Cambios de Estado correctamente');location.href='frmcambiaretapatesis.aspx?ctf=" & Request.QueryString("ctf") & "&id=" & Session("id_per") & "'</script>")

        Catch ex As Exception
            obj.AbortarTransaccion()
            Me.cmdGuardar.Visible = False
            Me.LblMensaje.Text = "Ocurrió un Error al Registrar el estado. Intente mas tarde." & Chr(13) & ex.Message
            obj = Nothing
        End Try
    End Sub

    Protected Sub imgGuardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgGuardar.Click
        Dim I As Integer
        Dim Fila As GridViewRow
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        Try
            'obj.IniciarTransaccion()
            For I = 0 To Me.GridView1.Rows.Count - 1
                Fila = Me.GridView1.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        '==================================
                        ' Guardar los datos
                        '==================================
                        obj.Ejecutar("TES_BloquearAvanceTesis", Me.GridView1.DataKeys.Item(Fila.RowIndex).Values("codigo_tes"), Me.dpAcciones.SelectedValue, Session("id_per"))
                    End If
                End If
            Next
            'obj.TerminarTransaccion()
            obj = Nothing
            Page.RegisterStartupScript("Habilitar", "<script>alert('Se han guardado los cambios correctamente');location.href='frmcambiaretapatesis.aspx?ctf=" & Request.QueryString("ctf") & "&id=" & Session("id_per") & "'</script>")

        Catch ex As Exception
            obj.AbortarTransaccion()
            Me.imgGuardar.Visible = False
            Me.LblMensaje.Text = "Ocurrió un Error al guardar los cambios. Intente mas tarde." & Chr(13) & ex.Message
            obj = Nothing
        End Try
    End Sub
    Private Sub ConsultarTesis()
        Try
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

            Me.GridView1.DataSource = obj.TraerDataTable("TES_ConsultarListaTesisCambiarEtapa", Me.dpEscuela.SelectedValue, Me.dpFase.SelectedValue, Me.txtTermino.Text.Trim, Me.dpCurso.SelectedValue, Me.dpCurso.SelectedItem.Text)
            Me.GridView1.DataBind()
            Me.GridView1.Visible = True
            'Cargar la siguiente fase a la seleccionada
            Dim tbl As Data.DataTable
            tbl = obj.TraerDataTable("TES_ConsultarEtapaInvestigacionTesis", 5, Me.dpFase.SelectedValue, 0, 0)
            If tbl.Rows.Count > 0 Then
                Me.lblFase.Text = tbl.Rows(0).Item("nombre_eti")
                Me.hdFase.Value = tbl.Rows(0).Item("codigo_eti")
            End If
            obj = Nothing

            Me.cmdExportar.Visible = Me.GridView1.Rows.Count > 0
            'Limpiar combo de acciones
            Me.dpAcciones.Items.Clear()
            Me.dpAcciones.Items.Add(New ListItem("--Seleccione una acción--", "-2"))
            Me.dpAcciones.Items.Add(New ListItem("Habilitar asesoria de tesis", "0"))
            Me.dpAcciones.Items.Add(New ListItem("Deshabilitar asesoría de tesis", "1"))
            'En la ultima etapa debe de quitarse el item del combo::cambiar estado
            If (Me.dpFase.SelectedValue < 7 And Me.GridView1.Rows.Count > 0) Then
                Me.dpAcciones.Items.Add(New ListItem("Cambiar estado de tesis", "2"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
       
    End Sub

    Protected Sub dpFase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpFase.SelectedIndexChanged
        ConsultarTesis()
    End Sub

    Protected Sub imgBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBuscar.Click
        ConsultarTesis()
    End Sub
End Class


Partial Class Encuesta_ReportesEvaluacionDocente_SeccionD_DesempenioDocentexItems
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Objcnx As New ClsConectarDatos
            Dim datosDepAcad As New Data.DataTable
            Dim ObjFun As New ClsFunciones
            Dim codigo_tfu As Int16
            Try
                Objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                codigo_tfu = Request.QueryString("ctf")
                Objcnx.AbrirConexion()
                If codigo_tfu = 1 Or codigo_tfu = 7 Or codigo_tfu = 16 Then
                    cboDepartamentoAcad.Items.Add(" ")
                    cboDepartamentoAcad.Enabled = False
                    ClsFunciones.LlenarListas(Me.cboPersona, objcnx.TraerDataTable("ConsultarPersonal", "TP", "1"), "codigo_Per", "personal")
                Else
                    cboDepartamentoAcad.Enabled = True
                    datosDepAcad = Objcnx.TraerDataTable("ConsultarAccesoRecurso", "11", Request.QueryString("id"), "", "")
                    If datosDepAcad.Rows.Count > 0 Then
                        ObjFun.CargarListas(Me.cboDepartamentoAcad, datosDepAcad, "codigo_Dac", "Nombre")
                        'cboDepartamentoAcad.SelectedIndex = 0
                        cboDepartamentoAcad_SelectedIndexChanged(sender, e)
                    Else
                        cboDepartamentoAcad.Items.Add(">>No definido<<")
                        cboDepartamentoAcad.ForeColor = Drawing.Color.Red
                    End If
                End If

                    '### Cargar ciclo académico y número de evaluación ###
                ObjFun.CargarListas(cboCicloAcad, objcnx.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")
                'ObjFun.CargarListas(cboNroEvaluacion, Objcnx.TraerDataTable("EAD_ConsultarCronogramaEvaluacionDocente", "DD", cboCicloAcad.SelectedValue), "codigo_cev", "descripcion_cev", "<<No definido>>")
                Objcnx.CerrarConexion()
                Objcnx = Nothing
                ObjFun = Nothing

                cboPersona.SelectedValue = Request.QueryString("per")
                cboCicloAcad.SelectedValue = Request.QueryString("cac")
                cboCicloAcad_SelectedIndexChanged(sender, e)
                cboNroEvaluacion.SelectedValue = Request.QueryString("cev")
                Me.lblPveinte.Visible = False
                Me.dtlPveinte.Dispose()
                Me.dtlPveinte.DataSource = Nothing
                Me.dtlPveinte.DataBind()
            Catch ex As Exception
                Response.Write(ex.Message)
                ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error')", True)
            End Try
        End If
    End Sub

    Protected Sub gvDesempenio_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDesempenio.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvDesempenio','Select$" & e.Row.RowIndex & "');")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Style.Add("cursor", "hand")
        End If
    End Sub

    Protected Sub gvDesempenio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDesempenio.SelectedIndexChanged
        Dim objcnx As New ClsConectarDatos
        Dim datos As New Data.DataTable
        objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        gvPreguntas.Visible = True
        If cboNroEvaluacion.Items.Count <> -1 Then
            objcnx.AbrirConexion()
            datos = objcnx.TraerDataTable("EAD_ConsultarPromedioObtenidoXCurso", cboPersona.SelectedValue, gvDesempenio.DataKeys.Item(gvDesempenio.SelectedIndex).Value, cboCicloAcad.SelectedValue, cboNroEvaluacion.SelectedValue)
            objcnx.CerrarConexion()
            If datos.Rows.Count > 0 Then
                gvPreguntas.DataSource = datos
                lblNroEstudiantes.Text = "Estudiantes que respondieron la encuesta: " & datos.Rows(1).Item("nro_alumnos") & "/" & datos.Rows(1).Item("matriculados") & " matriculados"
            End If
            Me.lblPveinte.Visible = True
            Me.lblNroEstudiantes.Visible = True
        Else
            gvPreguntas.DataSource = Nothing
        End If
        gvPreguntas.DataBind()
    End Sub

    Protected Sub gvPreguntas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPreguntas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            If fila.Row.Item("EsPadre") = 0 Then
                e.Row.Font.Bold = True
                e.Row.BackColor = Drawing.Color.Azure
            End If
        End If
    End Sub

    Protected Sub imgExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgExcel.Click
        If gvPreguntas.Rows.Count > 0 Then
            Axls()
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "Advertencia", "alert('Seleccione uno de los cursos que dicta el profesor para exportar la información')", True)
        End If
    End Sub

    Protected Sub imgGraficar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgGraficar.Click
        Dim cup As Int32
        If gvPreguntas.Rows.Count > 0 Then
            cup = gvDesempenio.DataKeys.Item(gvDesempenio.SelectedIndex).Value
            Response.Redirect("GraficarEncuestaDesempenio.aspx?id=" & Request.QueryString("id") & "&per=" & cboPersona.SelectedValue & "&cup=" & cup & "&cac=" & cboCicloAcad.SelectedValue & _
                              "&cev=" & cboNroEvaluacion.SelectedValue & "&ctf=" & Request.QueryString("ctf") & "&dac=" & cboDepartamentoAcad.SelectedValue)
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "Advertencia", "alert('Seleccione uno de los cursos que dicta el profesor para visualizar el gráfico')", True)
        End If
    End Sub


    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        Dim objcnx As New ClsConectarDatos
        Dim datosCarga As New Data.DataTable
        gvPreguntas.Visible = False
        If cboPersona.SelectedIndex > -1 Then
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()
            datosCarga = objcnx.TraerDataTable("EAD_ConsultarCargaAcademica", "C", cboPersona.SelectedValue, cboCicloAcad.SelectedValue)
            objcnx.CerrarConexion()
            If datosCarga.Rows.Count > 0 Then
                gvDesempenio.DataSource = datosCarga
            Else
                gvDesempenio.DataSource = Nothing
            End If
            gvDesempenio.DataBind()
            dtlPveinte.Dispose()
            Me.lblNroEstudiantes.Visible = False
            Me.lblPveinte.Visible = False
        End If
    End Sub

    Protected Sub cboCicloAcad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCicloAcad.SelectedIndexChanged
        Dim objCnx As New ClsConectarDatos
        Dim objFun As New ClsFunciones
        Dim datosEvaluacion As New Data.DataTable
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objCnx.AbrirConexion()
        datosEvaluacion = objCnx.TraerDataTable("EAD_ConsultarCronogramaEvaluacionDocente", "DD", cboCicloAcad.SelectedValue)
        objCnx.CerrarConexion()
        If datosEvaluacion.Rows.Count > 0 Then
            objFun.CargarListas(cboNroEvaluacion, datosEvaluacion, "codigo_cev", "descripcion_cev")
        Else
            cboNroEvaluacion.Items.Clear()
            cboNroEvaluacion.Items.Add(">>No definido<<")
            cboNroEvaluacion.Items(0).Value = -1
        End If
        objCnx = Nothing
        objFun = Nothing
    End Sub


    Protected Sub cboDepartamentoAcad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDepartamentoAcad.SelectedIndexChanged
        If Request.QueryString("ctf") <> 1 And Request.QueryString("ctf") <> 7 And Request.QueryString("ctf") <> 16 Then
            Dim datosPersonal As New Data.DataTable
            Dim Objcnx As New ClsConectarDatos
            Dim Objfun As New ClsFunciones
            Objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Objcnx.AbrirConexion()
            datosPersonal = Objcnx.TraerDataTable("EAD_ConsultarPDP", cboDepartamentoAcad.SelectedValue)
            Objcnx.CerrarConexion()
            Objfun.CargarListas(cboPersona, datosPersonal, "codigo_Per", "Personal")
            Objcnx = Nothing
            Objfun = Nothing
        Else
            cboPersona.Items.Add(">>Usted no tiene permisos<<")
            cboPersona.ForeColor = Drawing.Color.Red
        End If
    End Sub

    '### Exportar a Excel ###
    Private Sub Axls()
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=ReporteEncuestas_.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        Response.Write(HTML(Me.gvPreguntas))
        Response.End()

    End Sub

    Private Function HTML(ByVal grid As GridView) As String
        Dim Page1 As New Page()
        Dim Form2 As New HtmlForm()
        Dim label As New Label

        grid.EnableViewState = False
        Page1.EnableViewState = False
        Page1.Controls.Add(Form2)
        Page1.EnableEventValidation = False

        label.Text = "<B>REPORTE DE EVALUACIÓN - DESEMPEÑO DOCENTE </B><BR><BR>" & _
                     "CARRERA: " & gvDesempenio.SelectedRow.Cells(0).Text & "<BR>" & _
                     "CURSO: " & gvDesempenio.SelectedRow.Cells(1).Text & "<BR>" & _
                     "MODALIDAD: " & gvDesempenio.SelectedRow.Cells(2).Text & "<BR>" & _
                     "CICLO: " & gvDesempenio.SelectedRow.Cells(3).Text & "<BR>" & _
                     "GRUPO HORARIO: " & gvDesempenio.SelectedRow.Cells(4).Text & "<BR>"

        Form2.Controls.Add(label)
        Form2.Controls.Add(lblNroEstudiantes)
        Form2.Controls.Add(grid)

        Dim builder1 As New System.Text.StringBuilder()
        Dim writer1 As New System.IO.StringWriter(builder1)
        Dim writer2 As New HtmlTextWriter(writer1)

        Page1.DesignerInitialize()
        Page1.RenderControl(writer2)
        Page1.Dispose()
        Page1 = Nothing
        Return builder1.ToString()
    End Function

    Protected Sub cboPersona_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPersona.SelectedIndexChanged
        Me.lblPveinte.Visible = False
    End Sub

    Protected Sub dtlPveinte_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dtlPveinte.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            Dim fila As Data.DataRowView
            fila = e.Item.DataItem
            If fila.Item("evaluador_eed") = "D" Then
                e.Item.Font.Bold = True
            End If
        End If
    End Sub

    Protected Sub dtlPveinte_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtlPveinte.SelectedIndexChanged

    End Sub
End Class

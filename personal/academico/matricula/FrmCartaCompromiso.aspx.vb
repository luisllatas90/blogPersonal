
Partial Class academico_matricula_FrmCartaCompromiso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Param1: codigo_alu     param2: codigo_cac      id: codigo_per    
            If (Request.QueryString("param1") IsNot Nothing And _
                Request.QueryString("param2") IsNot Nothing And _
                Request.QueryString("id") IsNot Nothing) Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                ClsFunciones.LlenarListas(Me.dpcodigo_cac, obj.TraerDataTable("consultarcicloacademico", "UCI", 0), "codigo_Cac", "descripcion_cac")
                obj.CerrarConexion()

                Me.hdCodigo_alu.Value = Request.QueryString("param1")

                Me.dpcodigo_cac.SelectedValue = Request.QueryString("param2")
                Me.dpcodigo_cac.Enabled = False

                Dim dt As New Data.DataTable
                obj.AbrirConexion()
                dt = obj.TraerDataTable("ACAD_RetornaCodUniversitario", Request.QueryString("param1"))
                obj.CerrarConexion()
                obj = Nothing

                If (dt.Rows.Count > 0) Then
                    hdCodigoUniver.Value = dt.Rows(0).Item("codigoUniver_Alu").ToString
                    BuscaAlumno(hdCodigoUniver.Value)
                    Me.grwCursosDesaprobados.Enabled = False
                    Me.grwMarcados.Enabled = False
                End If
            Else
                fradatos.Visible = False
            End If
        End If
    End Sub

    Private Sub BuscaAlumno(ByVal CodUniversitario As String)
        Dim obj As New ClsConectarDatos
        Dim Tbl As Data.DataTable

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Me.dpcodigo_cac.Visible = False
        Me.cmdGenerar.Visible = False

        Me.lblmensaje.Text = ""
        'Cargar datos de estudiantes
        Tbl = obj.TraerDataTable("consultaracceso", "E", CodUniversitario, 0)
        If Tbl.Rows.Count > 0 Then
            Me.lblalumno.Text = Tbl.Rows(0).Item("alumno")
            Me.lblescuela.Text = Tbl.Rows(0).Item("nombre_cpf")
            Me.lblcicloingreso.Text = Tbl.Rows(0).Item("cicloing_alu")
            Me.lblPlan.Text = Tbl.Rows(0).Item("descripcion_pes")
            Me.hdCodigo_alu.Value = Tbl.Rows(0).Item("codigo_alu")
            fradatos.Visible = True

            Try
                'Me.HdFacultad.Value = Tbl.Rows(0).Item("codigo_cpf")
                Dim dtFacultad As New Data.DataTable
                dtFacultad = obj.TraerDataTable("MAT_BuscaFacultad", Tbl.Rows(0).Item("codigo_cpf"), 0)
                If (dtFacultad.Rows.Count > 0) Then
                    Me.HdFacultad.Value = "FACULTAD DE " & dtFacultad.Rows(0).Item("nombre_Fac")
                End If
            Catch ex As Exception
                Response.Write("MAT_BuscaFacultad " & Tbl.Rows(0).Item("codigo_cpf") & ", 0")
                Response.Write(ex.Message)
            End Try


            'Cargar cursos desaprobados
            'Me.grwCursosDesaprobados.DataSource = obj.TraerDataTable("MAT_ConsultarCartasCompromiso", 0, hdCodigo_alu.Value, 0, 0)
            Me.grwCursosDesaprobados.DataSource = obj.TraerDataTable("MAT_ConsultarCartasCompromiso_v2", hdCodigo_alu.Value, Request.QueryString("param2"))
            Me.grwCursosDesaprobados.DataBind()

            Dim Fila As GridViewRow
            For I As Int16 = 0 To Me.grwCursosDesaprobados.Rows.Count - 1
                Fila = Me.grwCursosDesaprobados.Rows(I)
                CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True
            Next

            If Me.grwCursosDesaprobados.Rows.Count > 0 Then
                Me.dpcodigo_cac.Visible = True
                Me.cmdGenerar.Visible = True
                Me.cmdGenerar.Enabled = True
            End If
        End If
        Tbl.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub grwCursosDesaprobados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwCursosDesaprobados.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

            If fila.Row("firmocarta") = 0 Then
                e.Row.Cells(7).Text = "No"
                e.Row.Cells(0).FindControl("chkElegir").Visible = True
                CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")
            Else
                e.Row.Cells(7).Text = "Sí"
                e.Row.Cells(0).FindControl("chkElegir").Visible = True
                CType(e.Row.FindControl("chkElegir"), CheckBox).Checked = True
            End If
        End If
    End Sub

    Protected Sub cmdGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGenerar.Click
        Dim obj As New ClsConectarDatos
        Dim codigo_usu As Int64 = Request.QueryString("id")
        Dim Fila As GridViewRow
        Dim Marcados As Int16

        'Verificar que haya marcado filas
        For I As Int16 = 0 To Me.grwCursosDesaprobados.Rows.Count - 1
            Fila = Me.grwCursosDesaprobados.Rows(I)
            If Fila.RowType = DataControlRowType.DataRow Then
                If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                    Marcados = Marcados + 1
                End If
            End If
        Next
        If Marcados = 0 Then
            Me.lblmensaje.Text = "Debe seleccionar las asignaturas que firmará carta de compromiso"
            Exit Sub
        End If

        'Crear tabla temporal de cursos marcados
        Dim tblmarcados As New Data.DataTable
        'tblmarcados.Columns.Add(New Data.DataColumn("Semestre", GetType(String)))
        tblmarcados.Columns.Add(New Data.DataColumn("Asignatura", GetType(String)))
        tblmarcados.Columns.Add(New Data.DataColumn("Nro. veces", GetType(String)))
        tblmarcados.Columns.Add(New Data.DataColumn("Ciclo", GetType(String)))
        'tblmarcados.Columns.Add(New Data.DataColumn("Crd", GetType(String)))
        'tblmarcados.Columns.Add(New Data.DataColumn("Nota", GetType(String)))

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try

            obj.AbrirConexion()
            '==================================
            'Desactivar los planes de estudio
            '==================================
            For I As Int16 = 0 To Me.grwCursosDesaprobados.Rows.Count - 1
                Fila = Me.grwCursosDesaprobados.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        obj.Ejecutar("MAT_AgregarCartasCompromiso", Me.grwCursosDesaprobados.DataKeys.Item(Fila.RowIndex).Values("codigo_dma"), 1, Me.dpcodigo_cac.SelectedValue, codigo_usu)

                        'Agregar a Tabla Temporal
                        Dim FilaNueva As Data.DataRow

                        FilaNueva = tblmarcados.NewRow()
                        'FilaNueva("semestre") = Me.grwCursosDesaprobados.Rows(I).Cells(1).Text
                        FilaNueva("asignatura") = HttpUtility.HtmlDecode(Me.grwCursosDesaprobados.Rows(I).Cells(2).Text.ToString)
                        FilaNueva("Nro. veces") = Me.grwCursosDesaprobados.Rows(I).Cells(5).Text
                        FilaNueva("ciclo") = Me.grwCursosDesaprobados.Rows(I).Cells(3).Text
                        'FilaNueva("crd") = Me.grwCursosDesaprobados.Rows(I).Cells(4).Text                        
                        'FilaNueva("nota") = Me.grwCursosDesaprobados.Rows(I).Cells(6).Text
                        'Añadir fila
                        tblmarcados.Rows.Add(FilaNueva)
                    End If
                End If
            Next
            obj.CerrarConexion()

            obj.AbrirConexion()
            'Cargar cursos desaprobados
            'Me.grwCursosDesaprobados.DataSource = obj.TraerDataTable("MAT_ConsultarCartasCompromiso", 0, hdCodigo_alu.Value, 0, 0)
            Me.grwCursosDesaprobados.DataSource = obj.TraerDataTable("MAT_ConsultarCartasCompromiso_v2", hdCodigo_alu.Value, Request.QueryString("param2"))
            Me.grwCursosDesaprobados.DataBind()
            obj.CerrarConexion()

            'Mostrar mensajes
            'Me.lblmensaje.Text = "Se han actualizado los datos correctamente"
            ClientScript.RegisterStartupScript(Me.GetType, "ok", "alert('Se han guardado correctamente los cambios. Clic en Aceptar para Generar la Carta')", True)

            'Generar Word
            Dim lbltenor1 As New Label
            Dim lbltenor2 As New Label
            Dim grw As New GridView
            Dim escuela As String

            escuela = "ESCUELA DE " & Me.lblescuela.Text.ToString

            lbltenor1.Text = "<h2 align='center'>ACTA DE COMPROMISO DE MEJORA EN EL DESEMPEÑO ACADEMICO</h2>"
            lbltenor1.Text = lbltenor1.Text & "<p style='text-align:justify'>En Chiclayo, siendo las " & Date.Now.TimeOfDay.ToString.Substring(0, 8)
            lbltenor1.Text = lbltenor1.Text & " del día " & Date.Now.Date & ", en las Oficinas de la " & Me.HdFacultad.Value
            lbltenor1.Text = lbltenor1.Text & ", se presentó " & Me.lblalumno.Text
            lbltenor1.Text = lbltenor1.Text & " identificado con Código de Matrícula " & hdCodigoUniver.Value
            lbltenor1.Text = lbltenor1.Text & ", estudiante de la " & escuela
            lbltenor1.Text = lbltenor1.Text & ". Luego de la revisión del historial académico, en presencia del Director de Escuela/ Asesor "
            lbltenor1.Text = lbltenor1.Text & "de matrícula que suscribe el presente, se ha determinado que el estudiante ha desaprobado las siguientes asignaturas:</p>"

            lbltenor2.Text = "<p style='text-align:justify'>"
            lbltenor2.Text = lbltenor2.Text & "En ese sentido, el estudiante se <span style='font-weight:bold'><u>compromete a APROBAR</u></span> en el presente ciclo " & Me.dpcodigo_cac.SelectedItem.Text
            lbltenor2.Text = lbltenor2.Text & ", las asignaturas señaladas y asistir a sus reuniones con el tutor asignado durante todo el semestre, declarando conocer sobre las consecuencias de su "
            lbltenor2.Text = lbltenor2.Text & "incumplimiento establecido en el artículo nº 48 del reglamento de estudios de Pregrado "
            lbltenor2.Text = lbltenor2.Text & "en el que también dispone el retiro del alumno en caso de reincidencia.</p>"

            'lbltenor2.Text = lbltenor2.Text & "<p style='text-align:justify'>"
            'lbltenor2.Text = lbltenor2.Text & "De igual manera el estudiante se <span style='font-weight:bold'><u>COMPROMETE A APROBAR  TODAS LAS ASIGNATURAS </u></span> del <span style='font-weight:bold'><u>NIVEL DE IMPLANTACION</u></span> para poder pasar al <u>NIVEL DE INTERNALIZACION</u>.</p><br />"

            lbltenor2.Text = lbltenor2.Text & "<p><span style='font-weight:bold'>En señal de conformidad se suscribe la presente.</span></p><br /><br />"
            lbltenor2.Text = lbltenor2.Text & "<p>Padre de Familia &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Estudiante</p>"
            lbltenor2.Text = lbltenor2.Text & "<br /><br /><br />"
            lbltenor2.Text = lbltenor2.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Director de la Escuela<br />"
            lbltenor2.Text = lbltenor2.Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Universidad Católica Santo Toribio de Mogrovejo"


            grw.DataSource = tblmarcados
            grw.DataBind()
            tblmarcados.Dispose()

            Dim Page As New Page
            Dim form As New HtmlForm

            'Page.EnableViewState = False
            'Page.EnableEventValidation = False
            Page.Controls.Add(form)
            form.Controls.Add(lbltenor1)
            form.Controls.Add(grw)
            form.Controls.Add(lbltenor2)

            Dim sb As New StringBuilder
            Dim SW As New System.IO.StringWriter(sb)
            Dim htw As New HtmlTextWriter(SW)

            Page.DesignerInitialize()
            Page.RenderControl(htw)
            Response.Clear()
            'Response.Buffer = True
            Response.ContentType = "application/vnd.ms-word"
            'Response.ContentType = "application/pdf"
            Response.AddHeader("Content-Disposition", "attachment;filename=cargacompromiso-" & hdCodigoUniver.Value & ".doc")
            'Response.AddHeader("content-length", "attachment;filename=cargacompromiso-" & hdCodigoUniver.Value & ".pdf")
            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.Default
            Response.Write(sb.ToString())
            Response.End()

            Me.cmdGenerar.Enabled = False
        Catch ex As Exception
            obj = Nothing
            Me.cmdGenerar.Enabled = True
            Me.lblmensaje.Text = "Ocurrió un error al actualizar los datos: " & ex.Message & "<br/>Contáctese con desarrollosistemas@usat.edu.pe"
        End Try
    End Sub

End Class


Partial Class frmtesis
    Inherits System.Web.UI.Page
    Private tblTempAutor As Data.DataTable
    Private tblTempAutorView As Data.DataView
    Private tblTempLinea As Data.DataTable
    Private tblTempLineaView As Data.DataView
    Private tblTempAsesor As Data.DataTable
    Private tblTempAsesorView As Data.DataView
    Private Sub CargaAutoresTemporal()
        Me.grwAutor.DataSource = tblTempAutorView
        Me.grwAutor.DataBind()
    End Sub
    Private Sub CargarLineaTemporal()
        Me.grdLineas.DataSource = tblTempLineaView
        Me.grdLineas.DataBind()
    End Sub
    Private Sub CargarAsesoresTemporal()
        Me.grdAsesores.DataSource = tblTempAsesorView
        Me.grdAsesores.DataBind()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("tblTempAutorData") Is Nothing Then

            tblTempAutor = New Data.DataTable()
            tblTempAutor.Columns.Add(New Data.DataColumn("codigo_Rtes", GetType(String)))
            tblTempAutor.Columns.Add(New Data.DataColumn("codigo_alu", GetType(String)))
            tblTempAutor.Columns.Add(New Data.DataColumn("codigouniver_alu", GetType(String)))
            tblTempAutor.Columns.Add(New Data.DataColumn("alumno", GetType(String)))
            tblTempAutor.Columns.Add(New Data.DataColumn("cicloing_alu", GetType(String)))
            tblTempAutor.Columns.Add(New Data.DataColumn("nombre_cpf", GetType(String)))
            tblTempAutor.Columns.Add(New Data.DataColumn("codigo_fac", GetType(String)))

            tblTempLinea = New Data.DataTable()
            tblTempLinea.Columns.Add(New Data.DataColumn("guardado", GetType(String)))
            tblTempLinea.Columns.Add(New Data.DataColumn("codigo_are", GetType(String)))
            tblTempLinea.Columns.Add(New Data.DataColumn("nombre_are", GetType(String)))

            tblTempAsesor = New Data.DataTable()
            tblTempAsesor.Columns.Add(New Data.DataColumn("codigo_rTes", GetType(String)))
            tblTempAsesor.Columns.Add(New Data.DataColumn("codigo_per", GetType(String)))
            tblTempAsesor.Columns.Add(New Data.DataColumn("docente", GetType(String)))
            tblTempAsesor.Columns.Add(New Data.DataColumn("descripcion_tpe", GetType(String)))
            tblTempAsesor.Columns.Add(New Data.DataColumn("descripcion_ded", GetType(String)))
            tblTempAsesor.Columns.Add(New Data.DataColumn("descripcion_tpi", GetType(String)))
            tblTempAsesor.Columns.Add(New Data.DataColumn("codigo_tpi", GetType(String)))

            Session("tblTempAutorData") = tblTempAutor
            Session("tblTempLineaData") = tblTempLinea
            Session("tblTempAsesorData") = tblTempAsesor
        Else
            tblTempAutor = Session("tblTempAutorData")
            tblTempLinea = Session("tblTempLineaData")
            tblTempAsesor = Session("tblTempAsesorData")
        End If

        tblTempAutorView = New Data.DataView(tblTempAutor)
        tblTempLineaView = New Data.DataView(tblTempLinea)
        tblTempAsesorView = New Data.DataView(tblTempAsesor)
        'tblTempAutorView.Sort = "Item"
        If IsPostBack = False Then
            Me.CargarDatosTesis()
            Me.CargaAutoresTemporal()
            Me.CargarLineaTemporal()
            Me.CargarAsesoresTemporal()
        End If
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        If Me.grwAutor.Rows.Count = 0 Then
            Page.RegisterStartupScript("error", "<script>alert('Debe registrar el autor(es) de la tesis')</script>")
        ElseIf Me.grdAsesores.Rows.Count = 0 Then
            Page.RegisterStartupScript("error", "<script>alert('Debe registrar el asesor(es) de la tesis')</script>")
        Else
            GuardarDatos(Request.QueryString("accion"))
        End If
    End Sub
    Public Sub GuardarDatos(ByVal modo As String)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim codigo_tes As Integer
        Dim id As Int16 = Request.QueryString("id")

        Try
            obj.IniciarTransaccion()

            If Request.QueryString("accion") = "M" Then
                obj.Ejecutar("TES_ModificarTesis", Request.QueryString("codigo_tes"), Me.txtTitulo.Text.Trim, Me.txtProblema.Text.Trim, Me.txtResumen.Text.Trim, CDate(Me.txtFechaInicio.Text), CDate(Me.txtFechaFin.Text), Me.dpenfoque.SelectedItem.Text, Me.txtResolucion.Text.Trim, id)
                codigo_tes = Request.QueryString("codigo_tes")
            Else
                codigo_tes = obj.Ejecutar("TES_AgregarTesis", 11, "000", Me.txtTitulo.Text.Trim, Me.txtProblema.Text.Trim, Me.txtResumen.Text.Trim, CDate(Me.txtFechaInicio.Text), CDate(Me.txtFechaFin.Text), Me.hdCodigo_Eti.Value, 1, id, Me.dpenfoque.SelectedItem.Text, Me.txtResolucion.Text.Trim, 0)
            End If

            If codigo_tes = 0 Then
                LimpiarTablasTemporales()
                obj.AbortarTransaccion()
                Page.RegisterStartupScript("error", "<script>alert('Ha ocurrido un error al grabar la tesis\n Intente denuevo o contáctese con desarrollosistemas@usat.edu.pe')</script>")
                Exit Sub
            End If

            'Guardar Autores de investigación
            For i As Int16 = 0 To tblTempAutor.Rows.Count - 1
                obj.Ejecutar("TES_AgregarResponsableTesis", 3, tblTempAutor.Rows(i).Item("codigo_alu"), codigo_tes, id, "")
            Next

            'Guardar Lineas de investigación
            For j As Int16 = 0 To tblTempLinea.Rows.Count - 1
                obj.Ejecutar("TES_AgregarAreaInvestigacionTesis", tblTempLinea.Rows(j).Item("codigo_are").ToString, codigo_tes)
            Next

            'Guardar Asesores de investigación
            For k As Int16 = 0 To tblTempAsesor.Rows.Count - 1
                obj.Ejecutar("TES_AgregarResponsableTesis", tblTempAsesor.Rows(k).Item("codigo_tpi"), tblTempAsesor.Rows(k).Item("codigo_per"), codigo_tes, id, "")
            Next

            obj.TerminarTransaccion()
            LimpiarTablasTemporales()
            Page.RegisterStartupScript("guardar", "<script>alert('Los datos se han guardado correctamente');location.href='lsttesis.aspx?id=" & id & "'</script>")
        Catch ex As Exception
            LimpiarTablasTemporales()
            obj.AbortarTransaccion()
            Page.RegisterStartupScript("error", "<script>alert('Ha ocurrido un error en la transacción\n" & ex.Message & "')</script>")
            obj = Nothing
        End Try
    End Sub
    Private Sub CargarDatosTesis()
        Me.txtFechaInicio.Attributes.Add("OnKeyDown", "return false")
        Me.txtFechaFin.Attributes.Add("OnKeyDown", "return false")
        Me.lblFase.Text = "PROYECTO DE INVESTIGACIÓN"

        If Request.QueryString("accion") = "M" Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Dim Tbl As New Data.DataTable

            Tbl = obj.TraerDataTable("TES_ConsultarTesis", 0, Request.QueryString("codigo_tes"), 0, 0)

            If Tbl.Rows.Count > 0 Then

                Me.lblEtiquetaRegistrado.Visible = True
                Me.lblcodigo.Text = Tbl.Rows(0).Item("codigoreg_tes").ToString
                Me.hdCodigo_Eti.Value = Tbl.Rows(0).Item("codigo_Eti")
                Me.lblFase.Text = Tbl.Rows(0).Item("nombre_Eti")
                Me.txtTitulo.Text = Tbl.Rows(0).Item("Titulo_Tes")
                Me.txtProblema.Text = Tbl.Rows(0).Item("Problema_Tes")
                Me.txtResumen.Text = Replace(Tbl.Rows(0).Item("Resumen_Tes"), "<br>", Chr(13))
                Me.txtFechaInicio.Text = Tbl.Rows(0).Item("fechainicio_Tes")
                Me.txtFechaFin.Text = Tbl.Rows(0).Item("fechafin_Tes")
                Me.txtResolucion.Text = Tbl.Rows(0).Item("resolucion_tes").tostring
                Me.dpenfoque.Text = Tbl.Rows(0).Item("enfoque_tes").tostring

                Me.lblRegistrado.Text = Tbl.Rows(0).Item("OpRegistro") & "&nbsp;&nbsp;" & Tbl.Rows(0).Item("FechaReg_Tes")
                If Tbl.Rows(0).Item("codigo_Eti") = 4 Then
                    Me.grdAsesores.Columns(2).Visible = True
                End If

                '*******************************************
                'CARGAR AUTORES DE LA TESIS
                '*******************************************
                Dim tblAutor As Data.DataTable
                tblAutor = obj.TraerDataTable("TES_ConsultarResponsableTesis", 2, Request.QueryString("codigo_tes"), 0, 0)
                For i As Int16 = 0 To tblAutor.Rows.Count - 1
                    Me.AgregarAutores(tblAutor.Rows(i).Item("codigo_rtes"), tblAutor.Rows(i).Item("codigo_alu"), tblAutor.Rows(i).Item("codigouniver_alu"), tblAutor.Rows(i).Item("alumno"), tblAutor.Rows(i).Item("cicloing_alu"), tblAutor.Rows(i).Item("nombre_cpf"), tblAutor.Rows(i).Item("codigo_fac"))
                Next
                Me.CargaAutoresTemporal()

                If tblAutor.Rows.Count > 0 Then
                    '*******************************************
                    'CARGAR TODAS LAS LINEAS INV. SEGUN AUTOR
                    '*******************************************
                    Me.cboLineas.DataSource = obj.TraerDataTable("TES_ConsultarAreaInvestigacionTesis", 2, tblAutor.Rows(0).Item("codigo_fac"), Request.QueryString("codigo_tes"), 0)
                    Me.cboLineas.DataBind()
                    Me.cboLineas.Enabled = Me.cboLineas.Items.Count > 0
                    Me.cmdAgregarLineas.Visible = Me.cboLineas.Items.Count > 0
                    '*******************************************
                    'CARGAR LINEAS INV. REGISTRADAS SEGUN AUTOR
                    '*******************************************
                    Dim tblLineas As Data.DataTable
                    tblLineas = obj.TraerDataTable("TES_ConsultarAreaInvestigacionTesis", 1, Request.QueryString("codigo_tes"), 0, 0)
                    For i As Int16 = 0 To tblLineas.Rows.Count - 1
                        Me.AgregarLineas(tblLineas.Rows(i).Item("guardado"), tblLineas.Rows(i).Item("codigo_are"), tblLineas.Rows(i).Item("nombre_are"))
                    Next
                    Me.CargarLineaTemporal()
                    tblLineas.Dispose()
                End If
                '*******************************************
                'CARGAR ASESORES
                '*******************************************
                Dim tblasesores As Data.DataTable
                tblasesores = obj.TraerDataTable("TES_ConsultarResponsableTesis", 3, Request.QueryString("codigo_tes"), 0, 1)
                For i As Int16 = 0 To tblasesores.Rows.Count - 1
                    Me.AgregarAsesor(tblasesores.Rows(i).Item("codigo_rtes"), tblasesores.Rows(i).Item("codigo_per"), tblasesores.Rows(i).Item("descripcion_tpi"), tblasesores.Rows(i).Item("codigo_tpi"), tblasesores.Rows(i).Item("docente"), tblasesores.Rows(i).Item("descripcion_tpe"), tblasesores.Rows(i).Item("descripcion_ded"))
                Next
                Me.CargarAsesoresTemporal()
                tblAutor.Dispose()
                tblasesores.Dispose()
            End If
            Tbl.Dispose()
            obj = Nothing
        End If
    End Sub
    Protected Sub CmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        LimpiarTablasTemporales()
        Response.Redirect("lsttesis.aspx?id=" & Request.QueryString("id"))
    End Sub
    Protected Sub grdLineas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLineas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Attributes.Add("onclick", "return confirm('¿Esta seguro que desea eliminar la línea de investigación?');")
        End If
    End Sub
    Protected Sub grdAsesores_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAsesores.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(2).Attributes.Add("onclick", "return confirm('¿Esta seguro que desea quitar al asesor de la investigación?');")
        End If
    End Sub
    Protected Sub grwAutor_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwAutor.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
               e.Row.Cells(2).Attributes.Add("onclick", "return confirm('¿Esta seguro que desea quitar al autor de la investigación?');")
        End If
    End Sub
    Private Sub EliminarResponsable(ByVal codigo_RTes As Int32)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        obj.Ejecutar("TES_EliminarResponsableTesis", "E", codigo_RTes, Request.QueryString("codigo_tes"), Request.QueryString("id"), "")
        obj = Nothing
    End Sub
    Protected Sub grwAutor_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwAutor.RowDeleting
        If grwAutor.DataKeys(e.RowIndex).Value <> 0 Then
            EliminarResponsable(grwAutor.DataKeys(e.RowIndex).Value)
            tblTempAutor.Rows.RemoveAt(e.RowIndex)
        Else
            tblTempAutor.Rows.RemoveAt(e.RowIndex)
            If Me.grwAutor.Rows.Count = 0 Then
                Me.cboLineas.Items.Clear()
                Me.cmdAgregarLineas.Visible = False
            End If
        End If
        Me.CargaAutoresTemporal()
        e.Cancel = True
    End Sub
    Protected Sub grdLineas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdLineas.RowDeleting
        If grdLineas.DataKeys(e.RowIndex).Values("guardado") <> 0 Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            obj.Ejecutar("TES_EliminarAreaInvestigacionTesis", grdLineas.DataKeys(e.RowIndex).Values("codigo_are").ToString, Request.QueryString("codigo_tes"))
            tblTempLinea.Rows.RemoveAt(e.RowIndex)
            obj = Nothing
        Else
            tblTempLinea.Rows.RemoveAt(e.RowIndex)
        End If
        Me.CargarLineaTemporal()
        e.Cancel = True
    End Sub
    Protected Sub grdAsesores_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdAsesores.RowDeleting
        If grdAsesores.DataKeys(e.RowIndex).Value <> 0 Then
            EliminarResponsable(grdAsesores.DataKeys(e.RowIndex).Value)
            tblTempAsesor.Rows.RemoveAt(e.RowIndex)
        Else
            tblTempAsesor.Rows.RemoveAt(e.RowIndex)
        End If
        Me.CargarAsesoresTemporal()
        e.Cancel = True
    End Sub
    Protected Sub grwAlumnos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwAlumnos.RowCommand
        Dim nro As Integer = Convert.ToInt32(e.CommandArgument)
        Me.AgregarAutores(0, CType(Me.grwAlumnos.Rows.Item(nro).Cells(1).FindControl("lblcodigo_alu"), Label).Text, CType(Me.grwAlumnos.Rows.Item(nro).Cells(1).FindControl("lblCodigo"), Label).Text, CType(Me.grwAlumnos.Rows.Item(nro).Cells(1).FindControl("lblAlumno"), Label).Text, CType(Me.grwAlumnos.Rows.Item(nro).Cells(1).FindControl("lblingreso"), Label).Text, CType(Me.grwAlumnos.Rows.Item(nro).Cells(1).FindControl("lblEscuela"), Label).Text, CType(Me.grwAlumnos.Rows.Item(nro).Cells(1).FindControl("lblcodigo_fac"), Label).Text)
        'Cargar Datos
        Me.CargaAutoresTemporal()
        Me.Panel1.Visible = False
        Me.grwAutor.Visible = True

        'Cargar lineas de investigación
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Me.cboLineas.DataSource = obj.TraerDataTable("TES_ConsultarAreaInvestigacionTesis", 2, CType(Me.grwAlumnos.Rows.Item(nro).Cells(1).FindControl("lblcodigo_fac"), Label).Text, 0, 0)
        Me.cboLineas.DataBind()
        obj = Nothing

        Me.cmdAgregarLineas.Visible = False
        Me.cboLineas.Enabled = False
        If Me.cboLineas.Items.Count > 0 Then
            Me.cmdAgregarLineas.Visible = True
            Me.cboLineas.Enabled = True
            Me.cboLineas.Items.Add("La escuela no ha registrado líneas de investigación")
        End If
        'End If
    End Sub
    Protected Sub imgBuscarAutor_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBuscarAutor.Click
        If Me.txtAlumno.Text.ToString <> "" Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Dim Tbl As New Data.DataTable
            Me.grwAlumnos.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis", 0, Me.txtAlumno.Text.Trim, 0, 0)
            Me.grwAlumnos.DataBind()
            obj = Nothing
            Me.grwAutor.Visible = False
            Me.Panel1.Visible = True
        End If
    End Sub
    Protected Sub imgBuscarAsesor_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBuscarAsesor.Click
        If Me.txtProfesor.Text.Trim <> "" Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Me.cboProfesores.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis", 1, Me.txtProfesor.Text.Trim, 0, 0)
            Me.cboProfesores.DataBind()
            obj = Nothing
            Me.grdAsesores.Visible = False
            Me.cboProfesores.Visible = True
            Me.cmdAgregarAsesor.Visible = Me.cboProfesores.Items.Count > 0
        End If
    End Sub
    Protected Sub cmdAgregarAsesor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregarAsesor.Click
        If cboProfesores.SelectedValue <> "" Then
            Dim txtdescripcion_tpi, txtcodigo_tpi As String
            If Me.grdAsesores.Rows.Count = 0 Then
                txtdescripcion_tpi = "ASESOR"
                txtcodigo_tpi = 5
            Else
                txtdescripcion_tpi = "CO-ASESOR"
                txtcodigo_tpi = 6
            End If

            Me.AgregarAsesor(0, Me.cboProfesores.SelectedValue, txtdescripcion_tpi, txtcodigo_tpi, Me.cboProfesores.SelectedItem.Text, "", "")
            Me.CargarAsesoresTemporal()
            Me.cboProfesores.Visible = False
            Me.cmdAgregarAsesor.Visible = False
            Me.txtProfesor.Text = ""
            Me.grdAsesores.Visible = True
        End If
    End Sub
    Protected Sub cmdAgregarLineas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregarLineas.Click
        If Me.cboLineas.Items.Count > 0 Then
            Me.AgregarLineas(0, Me.cboLineas.SelectedValue, Me.cboLineas.SelectedItem.Text)
            Me.CargarLineaTemporal()
            Me.grdLineas.Visible = True
        End If
    End Sub
    Private Sub LimpiarTablasTemporales()
        Session("tblTempAutorData") = Nothing
        Session("tblTempLineaData") = Nothing
        Session("tblTempAsesorData") = Nothing

        tblTempAsesor.Dispose()
        tblTempAutor.Dispose()
        tblTempLinea.Dispose()
    End Sub
    Private Sub AgregarAutores(ByVal codigo_rtes As Int32, ByVal codigo_alu As Int32, ByVal codigouniver_alu As String, ByVal alumno As String, ByVal cicloing_alu As String, ByVal nombre_cpf As String, ByVal codigo_fac As Int16)
        Dim fila As Data.DataRow

        fila = tblTempAutor.NewRow()

        fila("codigo_rtes") = codigo_rtes
        fila("codigo_alu") = codigo_alu
        fila("codigouniver_alu") = codigouniver_alu
        fila("alumno") = alumno
        fila("cicloing_alu") = cicloing_alu
        fila("nombre_cpf") = nombre_cpf
        fila("codigo_fac") = codigo_fac

        'Añadir fila
        tblTempAutor.Rows.Add(fila)
    End Sub
    Private Sub AgregarAsesor(ByVal codigo_rtes As Int32, ByVal codigo_per As Int32, ByVal descripcion_tpi As String, ByVal codigo_tpi As Int16, ByVal docente As String, ByVal descripcion_tpe As String, ByVal descripcion_ded As String)
        Dim fila As Data.DataRow
        fila = tblTempAsesor.NewRow()

        fila("codigo_Rtes") = codigo_rtes
        fila("codigo_per") = codigo_per
        fila("descripcion_tpi") = descripcion_tpi
        fila("codigo_tpi") = codigo_tpi
        fila("docente") = docente
        fila("descripcion_tpe") = descripcion_tpe
        fila("descripcion_ded") = descripcion_ded
        'Añadir fila
        tblTempAsesor.Rows.Add(fila)
    End Sub
    Private Sub AgregarLineas(ByVal guardado As String, ByVal codigo_are As Int16, ByVal nombre_are As String)
        Dim fila As Data.DataRow

        fila = tblTempLinea.NewRow()
        fila("guardado") = guardado
        fila("codigo_are") = codigo_are
        fila("nombre_are") = nombre_are
        'Añadir fila
        tblTempLinea.Rows.Add(fila)
        'Eliminar fila
        Me.cboLineas.Items.RemoveAt(cboLineas.SelectedIndex)
    End Sub
End Class
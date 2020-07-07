Imports System.IO
Partial Class frmtesis
    Inherits System.Web.UI.Page
    Private tblTempAutor As Data.DataTable
    Private tblTempAutorView As Data.DataView
    Private tblTempLinea As Data.DataTable
    Private tblTempLineaView As Data.DataView

    Private Sub CargaAutoresTemporal()
        Me.grwAutor.DataSource = tblTempAutorView
        Me.grwAutor.DataBind()
    End Sub
    Private Sub CargarLineaTemporal()
        Session("tblTempLineaData") = tblTempLinea
        Me.grdLineas.DataSource = tblTempLineaView
        Me.grdLineas.DataBind()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("id_per") = "" Or Request.QueryString("id") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If Session("tblTempAutorData") Is Nothing Then
            tblTempAutor = New Data.DataTable()
            tblTempAutor.Columns.Add(New Data.DataColumn("codigo_Rtes", GetType(String)))
            tblTempAutor.Columns.Add(New Data.DataColumn("codigo_alu", GetType(String)))
            tblTempAutor.Columns.Add(New Data.DataColumn("codigouniver_alu", GetType(String)))
            tblTempAutor.Columns.Add(New Data.DataColumn("alumno", GetType(String)))
            tblTempAutor.Columns.Add(New Data.DataColumn("cicloing_alu", GetType(String)))

            Session("tblTempAutorData") = tblTempAutor
        Else
            tblTempAutor = Session("tblTempAutorData")

        End If


        If Session("tblTempLineaData") Is Nothing Then
            tblTempLinea = New Data.DataTable()
            tblTempLinea.Columns.Add(New Data.DataColumn("guardado", GetType(String)))
            tblTempLinea.Columns.Add(New Data.DataColumn("codigo_are", GetType(String)))
            tblTempLinea.Columns.Add(New Data.DataColumn("nombre_are", GetType(String)))

            Session("tblTempLineaData") = tblTempLinea
        Else
            tblTempLinea = Session("tblTempLineaData")
        End If

        tblTempAutorView = New Data.DataView(tblTempAutor)
        tblTempLineaView = New Data.DataView(tblTempLinea)

        If IsPostBack = False Then
            MuestraFinanciamiento(False)
            Me.LimpiarTablasTemporales()
            Me.CargaCicloAcademico()
            Me.CargarDatosTesis()
            'Agregado 17/11/2011
        End If
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        If Me.grwAutor.Rows.Count = 0 Then
            Page.RegisterStartupScript("error", "<script>alert('Debe registrar el autor(es) de la tesis')</script>")
        Else
            GuardarDatos(Request.QueryString("accion"))
        End If
    End Sub

    Public Sub GuardarDatos(ByVal modo As String)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim codigo_tes As Integer, duracion As Integer = 0
        Dim presupuesto As Decimal = 0.0
        Dim id As Int16 = Request.QueryString("id")

        Try
            If (Me.txtMeses.Text.Trim.Length = 0) Then
                duracion = 0
            Else
                duracion = Integer.Parse(Me.txtMeses.Text.Trim)
            End If

            If (Me.txtPresupuesto.Text.Trim.Length = 0) Then
                presupuesto = 0
            Else
                presupuesto = Decimal.Parse(Me.txtPresupuesto.Text.Trim)
            End If

            obj.IniciarTransaccion()
            If Request.QueryString("accion") = "M" Then
                'Response.Write("TES_ModificarTesis '" & Me.dpTipoTesis.SelectedValue & "','" & Request.QueryString("codigo_tes") & "','" & Me.txtTitulo.Text.Trim & "','" & Me.txtProblema.Text.Trim & "','" & Me.txtResumen.Text.Trim & "','" & CDate(Me.txtFechaInicio.Text) & "','" & CDate(Me.txtFechaFin.Text) & "','" & Me.dpenfoque.SelectedItem.Text & "','" & Me.txtResolucion.Text.Trim & "','" & id & "','" & Me.txtExpediente.Text.Trim & "','" & duracion & "','" & presupuesto & "','" & Me.ddlFinanciado.SelectedValue)
                obj.Ejecutar("TES_ModificarTesis", Me.dpTipoTesis.SelectedValue, Request.QueryString("codigo_tes"), Me.txtTitulo.Text.Trim, Me.txtProblema.Text.Trim, Me.txtResumen.Text.Trim, CDate(Me.txtFechaInicio.Text), CDate(Me.txtFechaFin.Text), Me.dpenfoque.SelectedItem.Text, Me.txtResolucion.Text.Trim, id, Me.txtExpediente.Text.Trim, duracion, presupuesto, Me.ddlFinanciado.SelectedValue, Me.txtFinanciado.Text, Me.txtObjetivoGeneral.Text, Me.txtObjetivoEspecifico.Text)
                codigo_tes = Request.QueryString("codigo_tes")
            Else
                If Me.dpEscuela.SelectedValue = 27 Then
                    'campos no obligatorios para la escuela de psicologia
                    'registrar fecha inicio y fin = today , resolución=0
                    codigo_tes = obj.Ejecutar("TES_AgregarTesis", Me.dpTipoTesis.SelectedValue, "000", Me.txtTitulo.Text.Trim, Me.txtProblema.Text.Trim, Me.txtResumen.Text.Trim, Today, Today, Me.hdCodigo_Eti.Value, 1, id, Me.dpenfoque.SelectedItem.Text, "0", Me.txtExpediente.Text.Trim, duracion, presupuesto, Me.ddlFinanciado.SelectedValue, Me.txtFinanciado.Text, Me.txtObjetivoGeneral.Text, Me.txtObjetivoEspecifico.Text, 0)
                Else
                    codigo_tes = obj.Ejecutar("TES_AgregarTesis", Me.dpTipoTesis.SelectedValue, "000", Me.txtTitulo.Text.Trim, Me.txtProblema.Text.Trim, Me.txtResumen.Text.Trim, CDate(Me.txtFechaInicio.Text), CDate(Me.txtFechaFin.Text), Me.hdCodigo_Eti.Value, 1, id, Me.dpenfoque.SelectedItem.Text, Me.txtResolucion.Text.Trim, Me.txtExpediente.Text.Trim, duracion, presupuesto, Me.ddlFinanciado.SelectedValue, Me.txtFinanciado.Text, Me.txtObjetivoGeneral.Text, Me.txtObjetivoEspecifico.Text, 0)
                End If

            End If
            Session("codigo_tes") = codigo_tes

            If codigo_tes = 0 Then
                LimpiarTablasTemporales()
                obj.AbortarTransaccion()
                Page.RegisterStartupScript("error", "<script>alert('El título de la tesis ya está registrada, verifique en la busqueda')</script>")
                Exit Sub
            End If

            'Guardar Autores de investigación
            For i As Int16 = 0 To tblTempAutor.Rows.Count - 1
                Dim fecha As DateTime = Now.Date
                obj.Ejecutar("TES_AgregarResponsableTesis", 3, tblTempAutor.Rows(i).Item("codigo_alu"), codigo_tes, id, fecha.ToString("dd/MM/yyyy"), "")
            Next

            'Guardar Lineas de investigación
            For j As Int16 = 0 To tblTempLinea.Rows.Count - 1
                obj.Ejecutar("TES_AgregarAreaInvestigacionTesis", tblTempLinea.Rows(j).Item("codigo_are").ToString, codigo_tes)
            Next

            ''Guardar Asesores de investigación
            'For k As Int16 = 0 To tblTempAsesor.Rows.Count - 1
            '    obj.Ejecutar("TES_AgregarResponsableTesis", tblTempAsesor.Rows(k).Item("codigo_tpi"), tblTempAsesor.Rows(k).Item("codigo_per"), codigo_tes, id, "")
            'Next
            CargarArchivoPDF()
            obj.TerminarTransaccion()
            LimpiarTablasTemporales()
            Page.RegisterStartupScript("guardar", "<script>window.parent.location.reload();self.parent.tb_remove()</script>")
        Catch ex As Exception
            LimpiarTablasTemporales()
            Page.RegisterStartupScript("error", "<script>alert('Error: " & ex.Message & "')</script>")
        End Try

        'Catch ex As Exception
        '    LimpiarTablasTemporales()
        '    obj.AbortarTransaccion()
        '    Page.RegisterStartupScript("error", "<script>alert('Ha ocurrido un error en la transacción\nConsulte con desarrollosistemas@usat.edu.pe')</script>")
        '    obj = Nothing
        'End Try
    End Sub

    Private Sub CargaCicloAcademico()
        Try
            Dim obj As New ClsConectarDatos
            Dim tblciclo As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tblciclo = obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0)
            If tblciclo.rows.count > 0 Then
                ClsFunciones.LlenarListas(Me.ddlCiclo, tblciclo, "codigo_cac", "descripcion_cac", "--Todos--")
            End If
        Catch ex As Exception
            Response.Write(ex.message)
        End Try
    End Sub

    Private Sub CargarDatosTesis()
        Dim obj As New ClsConectarDatos
        Dim tblescuela As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        If Request.QueryString("ctf") = 1 Then
            tblescuela = obj.TraerDataTable("ConsultarCarreraProfesional", "MA", 0)
        Else
            tblescuela = obj.TraerDataTable("consultaracceso", "ESC", "", Request.QueryString("id"))
        End If
        ClsFunciones.LlenarListas(Me.dpEscuela, tblescuela, "codigo_cpf", "nombre_cpf", "--Seleccione la Escuela--")
        'obj.CerrarConexion()

        Me.cboLineas.DataSource = obj.TraerDataTable("TES_ConsultarAreaInvestigacionTesis", 3, 0, 0, 0)
        Me.cboLineas.DataBind()

        If cboLineas.Items.Count > 0 Then
            Me.cboLineas.Enabled = True
            Me.cmdAgregarLineas.Visible = True
        Else
            Me.cboLineas.Enabled = False
            Me.cmdAgregarLineas.Visible = False
        End If
        obj.CerrarConexion()

        If Request.QueryString("accion") = "M" Then
            Dim Tbl As New Data.DataTable
            obj.AbrirConexion()
            Tbl = obj.TraerDataTable("TES_ConsultarTesis", 0, Request.QueryString("codigo_tes"), 0, 0)
            obj.CerrarConexion()
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
                Me.txtResolucion.Text = Tbl.Rows(0).Item("resolucion_tes").ToString
                Me.dpenfoque.Text = Tbl.Rows(0).Item("enfoque_tes").ToString
                Me.txtExpediente.Text = Tbl.Rows(0).Item("numeroexp_tes").ToString
                Me.dpTipoTesis.SelectedValue = Tbl.Rows(0).Item("Codigo_TIn")
                Me.txtPresupuesto.Text = Tbl.Rows(0).Item("presupuesto_tes").ToString
                Me.txtMeses.Text = Tbl.Rows(0).Item("duracion_tes").ToString
                Me.txtObjetivoGeneral.Text = Tbl.Rows(0).Item("objgeneral_tes")
                Me.txtObjetivoEspecifico.Text = Tbl.Rows(0).Item("objespecifico_tes")
                Me.ddlCiclo.selectedvalue = Request.QueryString("cac")
                Me.dpEscuela.selectedvalue = Request.QueryString("cpf")
                If (Tbl.Rows(0).Item("financiado_tes").ToString = "") Then
                    Me.ddlFinanciado.SelectedValue = "A"
                Else
                    Me.ddlFinanciado.SelectedValue = Tbl.Rows(0).Item("financiado_tes")
                End If
                Me.txtFinanciado.Text = Tbl.Rows(0).Item("auspiciador_tes")

                If Tbl.Rows(0).Item("Codigo_TIn") <> 11 Then 'Si no son tesis
                    Me.txtExpediente.Visible = True
                    Me.lblNumeroExp.Visible = True
                End If

                Me.lblRegistrado.Text = Tbl.Rows(0).Item("OpRegistro") & "&nbsp;&nbsp;" & Tbl.Rows(0).Item("FechaReg_Tes")

                CargaResponsableTesis()

                'Cargar Archivo si existe
                Dim curFile As String = Server.MapPath("../../../filesTesis/" & Request.QueryString("codigo_tes") & ".pdf")
                texto.innerHTML = " >>Sin Adjunto"
                If (File.Exists(curFile)) Then
                    texto.innerHTML = "<a href='../../../filesTesis/" & Request.QueryString("codigo_tes") & ".pdf' target='_blank'>Ver Adjunto</a>"
                End If

                ''Cargar Líneas de investigación
                'Me.cboLineas.DataSource = obj.TraerDataTable("TES_ConsultarAreaInvestigacionTesis", 2, Me.dpEscuela.SelectedValue, 0, 0)
                'Me.cboLineas.DataBind()
                'obj.CerrarConexion()
                'obj = Nothing

                'Cargar Líneas de investigación
                obj.AbrirConexion()
                'Me.cboLineas.DataSource = obj.TraerDataTable("TES_ConsultarAreaInvestigacionTesis", 2, Me.dpEscuela.SelectedValue, 0, 0)
                Me.cboLineas.DataSource = obj.TraerDataTable("TES_ConsultarAreaInvestigacionTesis", 3, 0, 0, 0)
                Me.cboLineas.DataBind()
                obj.CerrarConexion()
                obj = Nothing

                If Me.cboLineas.Items.Count > 0 Then
                    Me.cboLineas.Enabled = True
                    Me.cmdAgregarLineas.Visible = True
                Else
                    Me.cboLineas.Enabled = False
                    Me.cmdAgregarLineas.Visible = False
                End If



            End If
            Tbl.Dispose()
        End If

        obj = Nothing
    End Sub

    Private Sub cargaResponsableTesis()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            '*******************************************
            'CARGAR AUTORES DE LA TESIS
            '*******************************************
            Dim tblAutor As Data.DataTable
            obj.AbrirConexion()
            tblAutor = obj.TraerDataTable("TES_ConsultarResponsableTesis", 2, Request.QueryString("codigo_tes"), 0, 0)
            For i As Int16 = 0 To tblAutor.Rows.Count - 1
                Me.AgregarAutores(tblAutor.Rows(i).Item("codigo_rtes"), tblAutor.Rows(i).Item("codigo_alu"), tblAutor.Rows(i).Item("codigouniver_alu"), tblAutor.Rows(i).Item("alumno"), tblAutor.Rows(i).Item("cicloing_alu"))
            Next
            Me.CargaAutoresTemporal()
            obj.CerrarConexion()

            If tblAutor.Rows.Count > 0 Then

                '*******************************************
                'CARGAR TODAS LAS LINEAS INV. SEGUN AUTOR
                '*******************************************
                obj.AbrirConexion()
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
                obj.CerrarConexion()
                Me.CargarLineaTemporal()
                tblLineas.Dispose()
            End If

            tblAutor.Dispose()
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub

    Protected Sub grdLineas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLineas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Attributes.Add("onclick", "return confirm('¿Esta seguro que desea eliminar la línea de investigación?');")
        End If
    End Sub
    Protected Sub grwAutor_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwAutor.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(3).Attributes.Add("onclick", "return confirm('¿Esta seguro que desea quitar al autor de la investigación?');")
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
            If (e.RowIndex > 0) Then
                tblTempAutor.Rows.RemoveAt(e.RowIndex)
            End If
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
        Try
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
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
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
        tblTempAutor.Dispose()
        tblTempLinea.Dispose()
        tblTempLinea.Rows.Clear()
    End Sub
    Private Sub AgregarAutores(ByVal codigo_rtes As Int32, ByVal codigo_alu As Int32, ByVal codigouniver_alu As String, ByVal alumno As String, ByVal cicloing_alu As String)
        Dim fila As Data.DataRow

        fila = tblTempAutor.NewRow()

        fila("codigo_rtes") = codigo_rtes
        fila("codigo_alu") = codigo_alu
        fila("codigouniver_alu") = codigouniver_alu
        fila("alumno") = alumno
        fila("cicloing_alu") = cicloing_alu

        'Añadir fila
        tblTempAutor.Rows.Add(fila)
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
        If Me.cboLineas.Items.Count > 0 Then
            Me.cboLineas.Items.RemoveAt(cboLineas.SelectedIndex)
        End If
    End Sub
    Protected Sub dpTipoTesis_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpTipoTesis.SelectedIndexChanged
        If dpTipoTesis.SelectedValue = 11 Then 'Tesis
            Me.dpenfoque.Enabled = True
            Me.lblNumeroExp.Visible = False
            Me.txtExpediente.Visible = False
        Else
            Me.dpenfoque.Enabled = False
            Me.lblNumeroExp.Visible = True
            Me.txtExpediente.Visible = True
        End If
    End Sub
    Protected Sub cmdAgregarAutor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregarAutor.Click
        Me.AgregarAutores(0, Me.cboAutor.SelectedValue, "", Me.cboAutor.SelectedItem.Text, "")
        'Cargar Datos
        Me.CargaAutoresTemporal()
    End Sub
    Protected Sub dpEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpEscuela.SelectedIndexChanged
        'Me.cboAutor.Items.Clear()
        'Me.cboLineas.Items.Clear()
        'Me.ValidarAutorLineas(False)
        'If Me.dpEscuela.SelectedValue <> -1 Then
        '    Dim obj As New ClsConectarDatos
        '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        '    obj.AbrirConexion()
        '    'Cargar Autores

        '    'Me.cboAutor.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis", 0, Me.dpEscuela.SelectedValue, 0, 0)
        '    Me.cboAutor.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis_v2", 0, Me.dpEscuela.SelectedValue, 0, 0, ddlciclo.selectedvalue)
        '    Me.cboAutor.DataBind()

        '    'Cargar Líneas de investigación
        '    Me.cboLineas.DataSource = obj.TraerDataTable("TES_ConsultarAreaInvestigacionTesis", 2, Me.dpEscuela.SelectedValue, 0, 0)
        '    Me.cboLineas.DataBind()
        '    obj.CerrarConexion()
        '    obj = Nothing

        '    'Verificar autores según la carrera
        '    If Me.cboAutor.Items.Count > 0 Then
        '        Me.cmdAgregarAutor.Visible = True
        '        Me.cboAutor.Enabled = True
        '    End If

        '    'Verificar líneas cargadas
        '    If Me.cboLineas.Items.Count > 0 Then
        '        Me.cmdAgregarLineas.Visible = True
        '        Me.cboLineas.Enabled = True
        '        Me.cboLineas.Items.Add("La escuela no ha registrado líneas de investigación")
        '    End If

        '    'Datos opcionales para la Escuela de Psicologia (codigo_cpf = 27):
        '    'Fecha de Inicio, Fecha de Termino, Nro de Resolucion
        '    If Me.dpEscuela.selectedValue = 27 Then

        '        Me.rqResolucion.enabled = False
        '        Me.RequiredFieldValidator4.enabled = False
        '        Me.RequiredFieldValidator5.enabled = False
        '        Me.CompareValidator1.enabled = False
        '    Else
        '        Me.rqResolucion.enabled = True
        '        Me.RequiredFieldValidator4.enabled = True
        '        Me.RequiredFieldValidator5.enabled = True
        '        Me.CompareValidator1.enabled = True

        '    End If

        '    Me.cmdGuardar.Enabled = True
        'End If

        Call FiltradoCarreraCiclo()
    End Sub


    Private Sub FiltradoCarreraCiclo()
        Me.cboAutor.Items.Clear()
        'Me.cboLineas.Items.Clear()
        Me.ValidarAutorLineas(False)
        If Me.dpEscuela.SelectedValue <> -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            'Cargar Autores

            'Me.cboAutor.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis", 0, Me.dpEscuela.SelectedValue, 0, 0)
            'se modifico para incluir el ciclo academico 17/10/2011 dguevara
            Me.cboAutor.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis_v2", 0, Me.dpEscuela.SelectedValue, 0, 0, ddlciclo.selectedvalue)
            Me.cboAutor.DataBind()

            ''Cargar Líneas de investigación
            'Me.cboLineas.DataSource = obj.TraerDataTable("TES_ConsultarAreaInvestigacionTesis", 2, Me.dpEscuela.SelectedValue, 0, 0)
            'Me.cboLineas.DataBind()
            'obj.CerrarConexion()
            'obj = Nothing

            'Verificar autores según la carrera
            If Me.cboAutor.Items.Count > 0 Then
                Me.cmdAgregarAutor.Visible = True
                Me.cboAutor.Enabled = True
            End If

            'Verificar líneas cargadas
            If Me.cboLineas.Items.Count > 0 Then
                Me.cmdAgregarLineas.Visible = True
                Me.cboLineas.Enabled = True
                'Me.cboLineas.Items.Add(New ListItem("La escuela no ha registrado líneas de investigación", "-1"))
            End If

            'Datos opcionales para la Escuela de Psicologia (codigo_cpf = 27):
            'Fecha de Inicio, Fecha de Termino, Nro de Resolucion
            If Me.dpEscuela.selectedValue = 27 Then

                Me.rqResolucion.enabled = False
                Me.RequiredFieldValidator4.enabled = False
                Me.RequiredFieldValidator5.enabled = False
                Me.CompareValidator1.enabled = False
            Else
                Me.rqResolucion.enabled = True
                Me.RequiredFieldValidator4.enabled = True
                Me.RequiredFieldValidator5.enabled = True
                Me.CompareValidator1.enabled = True

            End If

            Me.cmdGuardar.Enabled = True
        End If
    End Sub

    Private Sub ValidarAutorLineas(ByVal estado As Boolean)
        Me.cmdGuardar.Enabled = estado
        Me.cmdAgregarLineas.Visible = estado
        Me.cboLineas.Enabled = estado
        Me.cmdAgregarAutor.Visible = estado
        Me.cboAutor.Enabled = estado
        Me.cmdGuardar.Enabled = estado
    End Sub

    Protected Sub ddlCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCiclo.SelectedIndexChanged
        'Response.Write(ddlciclo.selectedvalue)
        If dpEscuela.selectedValue <> -1 Then
            Call FiltradoCarreraCiclo()
        End If

    End Sub

    Sub CargarArchivoPDF()
        Dim path As String = Server.MapPath("../../../filesTesis/")
        Dim fileOK As Boolean = False
        If FileUpload1.HasFile Then
            Dim fileExtension As String
            fileExtension = System.IO.Path. _
                GetExtension(FileUpload1.FileName).ToLower()
            Dim allowedExtensions As String() = _
                {".pdf"}
            For i As Integer = 0 To allowedExtensions.Length - 1
                If fileExtension = allowedExtensions(i) Then
                    fileOK = True
                End If
            Next
            If fileOK Then
                Try
                    FileUpload1.PostedFile.SaveAs(path & Session("codigo_tes") & ".pdf")
                    '   Response.Write("File uploaded!")
                Catch ex As Exception
                    ' Response.Write("File could not be uploaded.")
                End Try
            Else
                ' Response.Write("Cannot accept files of this type.")  
            End If
        End If

    End Sub


    Protected Sub cboLineas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLineas.SelectedIndexChanged

    End Sub

    Protected Sub ddlFinanciado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFinanciado.SelectedIndexChanged
        If (Me.ddlFinanciado.SelectedIndex = 0) Then
            MuestraFinanciamiento(False)
        Else
            MuestraFinanciamiento(True)
        End If
    End Sub

    Private Sub MuestraFinanciamiento(ByVal sw As Boolean)
        Me.lblFinanciado.Visible = sw
        Me.txtFinanciado.Visible = sw
    End Sub
End Class
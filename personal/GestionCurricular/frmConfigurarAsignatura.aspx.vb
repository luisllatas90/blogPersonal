Imports System.Collections.Generic

Partial Class GestionCurricular_frmConfigurarAsignatura
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private cod_user As Integer
    Private cod_ctf As Integer
    Private idTabla As Integer = 24
    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles") '"http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"

    Private oeDiseñoAsignatura As e_DiseñoAsignatura, odDiseñoAsignatura As d_DiseñoAsignatura ' 20200107 - ENevado

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If

            cod_user = Session("id_per")

            If Not String.IsNullOrEmpty(Session("cod_ctf")) Then
                cod_ctf = Session("cod_ctf")
            Else
                cod_ctf = Request.QueryString("ctf")
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            If IsPostBack = False Then
                'Session("gc_codigo_cac") = 0
                'Session("gc_codigo_cpf") = 0
                'Session("gc_codigo_est") = 0
                'Session("gc_codigo_pes") = 0
                'Session("gc_codigo_cur") = 0
                'Session("gc_codigo_dis") = 0
                'Session("gc_nombre_cur") = ""
                mt_CargarSemestre()

                Me.txtBuscar.Attributes.Add("onKeyPress", "txtBuscar_onKeyPress('" & Me.btnBuscar.ClientID & "', event)")
                Me.txtBuscar.Visible = False
                Me.btnBuscar.Visible = False
                Session("delAnexo") = False
                'Me.txtBuscar.Attributes.Add("onkeypress", "txtBuscar_onKeyPress(this,'" & Me.btnBuscar.ClientID & "')")
                btnFuArchivo.Attributes.Add("onClick", "document.getElementById('" + fuArchivo.ClientID + "').click();")

                If Not String.IsNullOrEmpty(Session("gc_codigo_cac")) Then
                    If cboSemestre.Items.Count > 0 Then Me.cboSemestre.SelectedValue = Session("gc_codigo_cac")
                    mt_CargarCarreraProfesional(Me.cboSemestre.SelectedValue, cod_user, cod_ctf)
                    If cboCarrProf.Items.Count > 0 Then Me.cboCarrProf.SelectedValue = Session("gc_codigo_cpf")
                    Me.cboEstado.SelectedValue = Session("gc_codigo_est")
                    mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), IIf(Me.cboCarrProf.SelectedValue = -1, 0, Me.cboCarrProf.SelectedValue), Me.cboEstado.SelectedValue, cod_user, cod_ctf)
                End If
                'Session("gc_codigo_cac") = "" : Session("gc_codigo_cpf") = "" : Session("gc_codigo_pes") = "" : Session("gc_codigo_cur") = "" : Session("gc_codigo_dis") = "" : Session("gc_nombre_cur") = "" : Session("gc_codigo_est") = ""
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSemestre.SelectedIndexChanged
        Try
            mt_CargarCarreraProfesional(Me.cboSemestre.SelectedValue, cod_user, cod_ctf)
            If Me.cboCarrProf.Items.Count > 0 Then Me.cboCarrProf.SelectedIndex = 0
            If Me.gvAsignatura.Rows.Count > 0 Then mt_CargarDatos(0, 0, -1, cod_user, cod_ctf)
            Session("gc_codigo_cac") = ""
            lblMensaje.Text = ""
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboCarrProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarrProf.SelectedIndexChanged
        Try
            Me.cboEstado.SelectedIndex = 1
            mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), IIf(Me.cboCarrProf.SelectedValue = -1, 0, Me.cboCarrProf.SelectedValue), Me.cboEstado.SelectedValue, cod_user, cod_ctf)
            Session("gc_codigo_cac") = ""
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvAsignatura_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAsignatura.PageIndexChanging
        If Session("gc_AsignaturaCA") IsNot Nothing Then
            Me.gvAsignatura.DataSource = CType(Session("gc_AsignaturaCA"), Data.DataTable)
            Me.gvAsignatura.DataBind()
        End If
        
        Me.gvAsignatura.PageIndex = e.NewPageIndex
        Me.gvAsignatura.DataBind()
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            If Session("gc_AsignaturaCA") Is Nothing Then
                Call mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), IIf(Me.cboCarrProf.SelectedValue = -1, 0, Me.cboCarrProf.SelectedValue), Me.cboEstado.SelectedValue, cod_user, cod_ctf)
            End If

            Dim dt As New Data.DataTable
            Dim dv As New Data.DataView
            Dim strBuscar As String = ""

            If Me.txtBuscar.Text.Trim <> "" Then
                strBuscar = Me.txtBuscar.Text.Trim.ToUpper.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U")
                dv = New Data.DataView(CType(Session("gc_AsignaturaCA"), Data.DataTable), "nombre_Cur_Aux like '%" & strBuscar & "%'", "", Data.DataViewRowState.CurrentRows)
                dt = dv.ToTable
            Else
                dt = CType(Session("gc_AsignaturaCA"), Data.DataTable)
            End If

            Me.gvAsignatura.DataSource = dt
            Me.gvAsignatura.DataBind()

            Me.txtBuscar.Focus()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvAsignatura_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAsignatura.RowCommand
        Dim obj As New ClsConectarDatos
        Dim index, codigo_Pes, codigo_Cur, codigo_dis As Integer
        Dim dtUni, dtRes, dtCon, dtEst, dtRef, dtAnexo As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            index = CInt(e.CommandArgument)
            codigo_Pes = CInt(Me.gvAsignatura.DataKeys(index).Values("codigo_pes"))
            codigo_Cur = CInt(Me.gvAsignatura.DataKeys(index).Values("codigo_cur"))
            codigo_dis = CInt(Me.gvAsignatura.DataKeys(index).Values("codigo_dis"))
            Session("gc_codigo_cac") = Me.cboSemestre.SelectedValue
            Session("gc_codigo_cpf") = Me.cboCarrProf.SelectedValue
            Session("gc_codigo_est") = Me.cboEstado.SelectedValue
            Session("gc_codigo_pes") = codigo_Pes
            Session("gc_codigo_cur") = codigo_Cur
            Session("gc_codigo_dis") = codigo_dis
            Session("gc_nombre_cur") = Me.gvAsignatura.DataKeys(index).Values("nombre_Cur")
            Session("cod_ctf") = cod_ctf
            Select Case e.CommandName
                Case "FrmResultadoAprendizaje", "frmCriteriosEvaluacion", "FrmContenidosAsignatura", "frmEstrategiasDidactica", "frmReferenciaBibliografica"
                    If fc_tieneCronograma() Then
                        Response.Redirect("~/gestioncurricular/" & e.CommandName & ".aspx", False)
                    End If

                Case "Enviar"
                    obj.AbrirConexion()
                    dtUni = obj.TraerDataTable("COM_ListarResultadoAprendizaje", -1, codigo_Pes, codigo_Cur, Me.cboSemestre.SelectedValue, -1)
                    dtRes = obj.TraerDataTable("ResultadoAprendizaje_Listar", "", -1, codigo_dis, "")
                    dtCon = obj.TraerDataTable("COM_ListarContenidoAsignatura", codigo_dis, -1, 0)
                    dtEst = obj.TraerDataTable("EstrategiaDidactica_Listar", -1, codigo_dis, "")
                    dtRef = obj.TraerDataTable("ReferenciaBibliografica_Listar", -1, codigo_dis, -1, "")
                    obj.CerrarConexion()
                    ' Mostrar Unidades de Asignatura
                    Me.gvUnidad.DataSource = dtUni
                    Me.gvUnidad.DataBind()
                    If Me.gvUnidad.Rows.Count > 0 Then mt_AgruparFilas(Me.gvUnidad.Rows, 0, 2)
                    Me.lblDA.InnerText = "[OK]" : Me.lblDA.Attributes.Add("style", "color:black")
                    If Me.gvUnidad.Rows.Count = 0 Then Me.lblDA.InnerText = "¡ No se ha registrado ninguna Unidad para esta Asignatura !" : Me.lblDA.Attributes.Add("style", "color:red")
                    ' Mostrar Criterios de Evaluación 
                    Me.gvResultados.DataSource = dtRes
                    Me.gvResultados.DataBind()
                    If Me.gvResultados.Rows.Count > 0 Then mt_AgruparFilas(Me.gvResultados.Rows, 0, 5)
                    Me.lblRA.InnerText = fc_validarRA()
                    If Me.lblRA.InnerText = "[OK]" Then Me.lblRA.Attributes.Add("style", "color:black") Else Me.lblRA.Attributes.Add("style", "color:red")
                    ' Mostrar Contenido de Asignatura
                    Me.gvContenido.DataSource = dtCon
                    Me.gvContenido.DataBind()
                    If Me.gvContenido.Rows.Count > 0 Then mt_AgruparFilas(Me.gvContenido.Rows, 0, 1)
                    Me.lblCA.InnerText = fc_ValidarCA()
                    If Me.lblCA.InnerText = "[OK]" Then Me.lblCA.Attributes.Add("style", "color:black") Else Me.lblCA.Attributes.Add("style", "color:red")
                    ' Mostrar Estrategias Didácticas
                    Me.gvEstrategia.DataSource = dtEst
                    Me.gvEstrategia.DataBind()
                    Me.lblED.InnerText = "[OK]" : Me.lblED.Attributes.Add("style", "color:black")
                    If Me.gvEstrategia.Rows.Count = 0 Then Me.lblED.InnerText = "¡ Ingrese al menos una Estrategia Didáctica para esta asignatura !" : Me.lblED.Attributes.Add("style", "color:red")
                    ' Mostrar Referencias Bibliográficas
                    Me.gvReferencia.DataSource = dtRef
                    Me.gvReferencia.DataBind()
                    Me.lblRB.InnerText = fc_validarRB()
                    If Me.lblRB.InnerText = "[OK]" Then Me.lblRB.Attributes.Add("style", "color:black") Else Me.lblRB.Attributes.Add("style", "color:red")
                    Page.RegisterStartupScript("Pop", "<script>openModal('1');</script>")
                Case "Clonar"
                    If fc_tieneCronograma() Then
                        Me.lblNombreCurso.Text = Session("gc_nombre_cur")
                        Me.rb1.Checked = True
                        Me.cboDiseño.Enabled = True ': Me.cboDiseño.SelectedIndex = 0
                        Me.rb2.Checked = False
                        Me.cboDiseño2.Enabled = False ': Me.cboDiseño2.SelectedIndex = 0
                        mt_CargarDiseñoAsignatura(codigo_Pes, codigo_Cur, Me.cboSemestre.SelectedValue)
                        mt_CargarDiseñoAsignatura2(codigo_Pes, codigo_Cur, Me.cboSemestre.SelectedValue)
                        Me.gvUA.DataSource = Nothing : Me.gvUA.DataBind()
                        Me.gvCE.DataSource = Nothing : Me.gvCE.DataBind()
                        Me.gvCA.DataSource = Nothing : Me.gvCA.DataBind()
                        Me.gvED.DataSource = Nothing : Me.gvED.DataBind()
                        Me.gvRB.DataSource = Nothing : Me.gvRB.DataBind()
                        'Me.gvED.DataSource = Nothing : Me.gvED.DataBind()
                        Me.panModal.Update()
                        'mt_ShowMessage(codigo_Pes & " - " & codigo_Cur & " - " & Me.cboSemestre.SelectedValue, MessageType.Success)
                        Page.RegisterStartupScript("Pop", "<script>openModal('2');</script>")
                    End If

                Case "Subir"
                    'obj.AbrirConexion()
                    'dtAnexo = obj.TraerDataTable("COM_ObtenerAnexoAsignatura", codigo_dis, idTabla)
                    'obj.CerrarConexion()
                    ' 20200107 - ENevado ---------------------------------------------------------------------------\
                    odDiseñoAsignatura = New d_DiseñoAsignatura : oeDiseñoAsignatura = New e_DiseñoAsignatura
                    With oeDiseñoAsignatura
                        .codigo_dis = codigo_dis : .idTabla = idTabla
                    End With
                    dtAnexo = odDiseñoAsignatura.fc_ListarArchivoAnexo(oeDiseñoAsignatura)
                    '-----------------------------------------------------------------------------------------------/
                    If dtAnexo.Rows.Count > 0 Then
                        If dtAnexo.Rows(0).Item("archivo").ToString.Trim.Contains("No se eligió") Then
                            spnFile.InnerText = dtAnexo.Rows(0).Item("archivo").ToString.Trim
                            btnQuitarAnexo.Visible = False
                        Else
                            spnFile.InnerText = dtAnexo.Rows(0).Item("archivo").ToString.Trim & " (Subido el: " & dtAnexo.Rows(0).Item("fechaAnexo_dis").ToString.Trim & ")"
                            btnQuitarAnexo.Visible = True
                            'Session("dea_anexo_origen") = dtAnexo.rows(0).item("origen_dis") ' 20200107 - ENevado
                        End If
                    End If
                    dtAnexo.Dispose()

                    Me.panModal.Update()
                    Page.RegisterStartupScript("Pop", "<script>openModal('3', '" & Session("gc_nombre_cur") & "');</script>")
                Case "Ver"
                    Dim clsPdf As New clsGenerarPDF
                    Dim memory As New System.IO.MemoryStream
                    Dim memory_pdf As New System.IO.MemoryStream
                    Dim codigo_cup, idTransa, var As Long
                    Dim _modular As Boolean

                    'mt_ShowMessage("cac: " & Session("gc_codigo_cac") & " pes: " & codigo_Pes & " cur: " & codigo_Cur, MessageType.Warning)
                    codigo_cup = fc_ObtieneCodCup(Session("gc_codigo_cac"), codigo_Pes, codigo_Cur)
                    idTransa = CLng(Me.gvAsignatura.DataKeys(index).Values("codigo_dis"))
                    var = CLng(Me.gvAsignatura.DataKeys(index).Values("IdArchivo_Anexo"))
                    _modular = CBool(Me.gvAsignatura.DataKeys(index).Values("modular_pcu"))
                    'Response.Write("codigo_cup: " & codigo_cup & " idtransa: " & idTransa & " var: " & var)
                    clsPdf.fuente = Server.MapPath(".") & "/segoeui.ttf"
                    'clsPdf.anexo = Server.MapPath(".") & "/ManualTutoria.pdf"
                    'Response.Write("Paso 1")
                    'clsPdf.mt_GenerarSilabo2(codigo_cup, Server.MapPath(".") & "/logo_usat.png", memory, True)
                    If _modular Then
                        clsPdf.mt_GenerarSilabo2(codigo_cup, Server.MapPath(".") & "/logo_usat.png", memory, True)
                    Else
                        clsPdf.mt_GenerarSilabo(codigo_cup, Server.MapPath(".") & "/logo_usat.png", memory, True)
                    End If
                    'mt_ShowMessage("mod: " & _modular, MessageType.Warning)
                    'Response.Write("Paso 2")
                    Dim bytes() As Byte = memory.ToArray
                    memory.Close()

                    If var <> -1 Then
                        Dim bytes_anexo() As Byte = fc_ObtenerArchivo(idTransa, 24, "SU3WMBVV4W", var)
                        mt_CopiarPdf(bytes, bytes_anexo, memory_pdf, True)
                        Dim bytes_pdf() As Byte = memory_pdf.ToArray
                        memory_pdf.Close()
                        Response.Clear()
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("content-length", bytes_pdf.Length.ToString())
                        Response.BinaryWrite(bytes_pdf)
                    Else
                        ' 20200331-ENevado ================================================
                        mt_CopiarPdf(bytes, memory_pdf)
                        Dim bytes_adenda() As Byte = memory_pdf.ToArray
                        memory_pdf.Close()
                        '==================================================================
                        Response.Clear()
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("content-length", bytes_adenda.Length.ToString())
                        Response.BinaryWrite(bytes_adenda)
                    End If
            End Select
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEstado.SelectedIndexChanged
        Try
            If cboSemestre.SelectedValue = -1 Then
                mt_ShowMessage("¡ Seleccione Semestre Académico !", MessageType.Warning)
                cboSemestre.Focus()
            Else
                mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), IIf(Me.cboCarrProf.SelectedValue = -1, 0, Me.cboCarrProf.SelectedValue), Me.cboEstado.SelectedValue, cod_user, cod_ctf)
            End If
            Session("gc_codigo_cac") = ""
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvResultados_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim objGridView As GridView = CType(sender, GridView)
                Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objtablecell As TableCell = New TableCell()
                mt_AgregarCabecera(objgridviewrow, objtablecell, 2, "Resultado de Aprendizaje")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 2, "Indicadores de Aprendizaje")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, "Evidencia de Evaluación")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 2, "Instrumento de Evaluación")
                objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvResultados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvResultados.RowDataBound
        Try
            Dim tipo_prom, tipo_prom2 As Integer
            Dim peso_res, peso_ind, peso_ins As Double
            Dim codigo_ind, codigo_ins As Integer
            tipo_prom = Me.gvResultados.DataKeys(e.Row.RowIndex).Values("tipo_prom")
            tipo_prom2 = Me.gvResultados.DataKeys(e.Row.RowIndex).Values("tipo_prom2")
            peso_res = Me.gvResultados.DataKeys(e.Row.RowIndex).Values("peso_res")
            peso_ind = Me.gvResultados.DataKeys(e.Row.RowIndex).Values("peso_ind")
            peso_ins = Me.gvResultados.DataKeys(e.Row.RowIndex).Values("peso_ins")
            codigo_ind = Me.gvResultados.DataKeys(e.Row.RowIndex).Values("codigo_ind")
            codigo_ins = Me.gvResultados.DataKeys(e.Row.RowIndex).Values("codigo_ins")
            If e.Row.RowType = DataControlRowType.DataRow Then
                If tipo_prom = 2 Then
                    e.Row.Cells(3).Text = CStr(peso_ind * 100) & " %"
                Else
                    e.Row.Cells(3).Text = IIf(codigo_ind = -1, "", "Promedio Simple")
                End If
                If tipo_prom2 = 2 Then
                    e.Row.Cells(6).Text = CStr(peso_ins * 100) & " %"
                Else
                    e.Row.Cells(6).Text = IIf(codigo_ins = -1, "", "Promedio Simple")
                End If
                e.Row.Cells(1).Text = CStr(peso_res * 100) & " %"
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            obj.Ejecutar("DiseñoAsignatura_observar", Session("gc_codigo_dis"), "E", "", cod_user)
            obj.CerrarConexion()
            mt_ShowMessage("Se envió correctamente el Diseño de Asignatura", MessageType.Success)
            mt_CargarDatos(Me.cboSemestre.SelectedValue, Me.cboCarrProf.SelectedValue, Me.cboEstado.SelectedValue, cod_user, cod_ctf)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnDA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDA.Click
        Try
            'Page.RegisterStartupScript("Mensaje", "<script>console.log('" & Session("gc_codigo_dis") & "');</script>")
            Response.Redirect("~/gestioncurricular/FrmResultadoAprendizaje.aspx")
            'Page.RegisterStartupScript("Mensaje", "<script>console.log('xd 2');</script>")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnRA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRA.Click
        Try
            Response.Redirect("~/gestioncurricular/frmCriteriosEvaluacion.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnCA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCA.Click
        Try
            Response.Redirect("~/gestioncurricular/FrmContenidosAsignatura.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnED_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnED.Click
        Try
            Response.Redirect("~/gestioncurricular/frmEstrategiasDidactica.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnRB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRB.Click
        Try
            Response.Redirect("~/gestioncurricular/frmReferenciaBibliografica.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    'Protected Sub btnDiseño_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDiseño.Click
    '    Try
    '        If cboSemestre.SelectedIndex > -1 Then
    '            If cboSemestre.SelectedValue = -1 Then Throw New Exception("¡ Seleccione un Sementre Academico !")
    '            Session("gc_codigo_cac") = cboSemestre.SelectedValue
    '            Response.Redirect("http://serverdev/campusvirtual/personal/gestioncurricular/FrmResultadoAprendizaje.aspx")
    '        End If
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    Protected Sub gvAsignatura_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim objGridView As GridView = CType(sender, GridView)
                Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objtablecell As TableCell = New TableCell()
                mt_AgregarCabecera(objgridviewrow, objtablecell, 2, 1, "Mis Asignaturas")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 12, 1, "Diseño de la Asignatura")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 4, 1, "Acciones")
                objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGuardar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar2.Click
        Dim obj As New ClsConectarDatos
        Dim codigo_dis As Integer = -1
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            If Me.rb1.Checked Then
                codigo_dis = Me.cboDiseño.SelectedValue
            Else
                codigo_dis = Me.cboDiseño2.SelectedValue
            End If
            obj.AbrirConexion()
            If Me.rb1.Checked Then
                'Throw New Exception("DEA_DiseñoAsignatura_clonar " & codigo_dis)
                obj.Ejecutar("DEA_DiseñoAsignatura_clonar", codigo_dis, Me.cboSemestre.SelectedValue, cod_user)
            Else
                'Throw New Exception("DEA_DiseñoAsignatura_Importar " & codigo_dis)
                obj.Ejecutar("DEA_DiseñoAsignatura_Importar", codigo_dis, Me.cboSemestre.SelectedValue, Session("gc_codigo_pes"), Session("gc_codigo_cur"), cod_user)
            End If
            mt_ShowMessage("Se importo correctamente el Diseño de Asignatura", MessageType.Success)
            obj.CerrarConexion()
            mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), IIf(Me.cboCarrProf.SelectedValue = -1, 0, Me.cboCarrProf.SelectedValue), Me.cboEstado.SelectedValue, cod_user, cod_ctf)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboDiseño_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDiseño.SelectedIndexChanged
        'Dim obj As New ClsConectarDatos
        'Dim dtUni, dtRes, dtCon, dtEst, dtRef As Data.DataTable
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            mt_CargarInfoDiseño(IIf(Me.cboDiseño.SelectedValue = -1, 0, Me.cboDiseño.SelectedValue), 1)
            'obj.AbrirConexion()
            'dtUni = obj.TraerDataTable("COM_ListarResultadoAprendizaje", Me.cboDiseño.SelectedValue, -1, -1, -1, -1)
            'dtRes = obj.TraerDataTable("ResultadoAprendizaje_Listar", "", -1, Me.cboDiseño.SelectedValue, "")
            'dtCon = obj.TraerDataTable("COM_ListarContenidoAsignatura", Me.cboDiseño.SelectedValue, -1, 0)
            'dtEst = obj.TraerDataTable("EstrategiaDidactica_Listar", -1, Me.cboDiseño.SelectedValue, "")
            'dtRef = obj.TraerDataTable("ReferenciaBibliografica_Listar", -1, Me.cboDiseño.SelectedValue, -1, "")
            'obj.CerrarConexion()
            'Me.gvUA.DataSource = dtUni : Me.gvUA.DataBind()
            'If Me.gvUA.Rows.Count > 0 Then mt_AgruparFilas(Me.gvUA.Rows, 0, 2)
            'Me.gvCE.DataSource = dtRes : Me.gvCE.DataBind()
            'If Me.gvCE.Rows.Count > 0 Then mt_AgruparFilas(Me.gvCE.Rows, 0, 5)
            'Me.gvCA.DataSource = dtCon : Me.gvCA.DataBind()
            'If Me.gvCA.Rows.Count > 0 Then mt_AgruparFilas(Me.gvCA.Rows, 0, 1)
            'Me.gvED.DataSource = dtEst : Me.gvED.DataBind()
            'Me.gvRB.DataSource = dtRef : Me.gvRB.DataBind()
            'Me.panModal.Update()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGuardarAnexo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarAnexo.Click
        'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'Dim JSONresult As String = ""
        'Dim Data As New Dictionary(Of String, Object)()
        'Dim list As New List(Of Dictionary(Of String, Object))()

        'Dim obj As New ClsConectarDatos
        Dim codigo_dis As Integer
        'Dim dt As New Data.DataTable
        Dim respuesta As String = ""
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            codigo_dis = Session("gc_codigo_dis")

            If Not (IsDBNull(Session("perlogin"))) And Session("perlogin").ToString <> "" Then
                If codigo_dis = 0 Then Throw New Exception("¡ Seleccione una Asignatura !")

                If Me.fuArchivo.HasFile AndAlso Not Session("delAnexo") Then
                    Dim Archivos As HttpFileCollection = Request.Files
                    For i As Integer = 0 To Archivos.Count - 1
                        'Data.Add("Step", idTabla & "-" & codigo_dis & "-" & Archivos(i).FileName)

                        Call fc_SubirArchivo(idTabla, codigo_dis, Archivos(i))
                    Next
                End If

                oeDiseñoAsignatura = New e_DiseñoAsignatura : odDiseñoAsignatura = New d_DiseñoAsignatura
                With oeDiseñoAsignatura
                    .codigo_dis = codigo_dis : .idTabla = idTabla
                End With

                'obj.AbrirConexion()
                If Not Session("delAnexo") Then
                    'obj.Ejecutar("DiseñoAsignatura_SubirAnexo", idTabla, codigo_dis)
                    odDiseñoAsignatura.fc_RegistrarArchivoAnexo(oeDiseñoAsignatura)
                Else
                    'obj.Ejecutar("DiseñoAsignatura_QuitarAnexo", codigo_dis)
                    odDiseñoAsignatura.fc_EliminarArchivoAnexo(oeDiseñoAsignatura)
                End If
                'obj.CerrarConexion()
            Else
                Throw New Exception("Inicie Sesión")
            End If

            If Not Session("delAnexo") Then
                Call mt_ShowMessage("El Anexo fue cargado con éxito", MessageType.Success)
            Else
                Call mt_ShowMessage("El Anexo fue quitado de la asignatura", MessageType.Info)
            End If

            'Data.Add("Status", "OK")
            'Data.Add("Message", respuesta)
            'list.Add(Data)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)

            'Data.Add("Status", "Fail")
            'Data.Add("Message", ex.Message & " - " & Session("perlogin").ToString)
            'list.Add(Data)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)
        Finally
            spnFile.InnerText = ""
            Session("delAnexo") = False
            btnFuArchivo.Disabled = False
            btnQuitarAnexo.Visible = False
            RequiredFieldValidator1.Enabled = True
            updFileUpload.Update()
        End Try
    End Sub

    Protected Sub btnQuitarAnexo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuitarAnexo.Click
        Dim anexo As String = spnFile.InnerText
        spnFile.InnerHtml = "<strike style='color: red'><em>" & anexo & "</em></strike>"

        Session("delAnexo") = True
        btnFuArchivo.Disabled = True
        btnQuitarAnexo.Visible = False
        RequiredFieldValidator1.Enabled = False

        updFileUpload.Update()
    End Sub

    Protected Sub btnSalirAnexo_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirAnexo.ServerClick
        spnFile.InnerText = ""
        Session("delAnexo") = False
        btnFuArchivo.Disabled = False
        btnQuitarAnexo.Visible = False
        RequiredFieldValidator1.Enabled = True
        updFileUpload.Update()
    End Sub

    Protected Sub gvAsignatura_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAsignatura.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.Cells(2).Text = "OBSERVADO" Then
                    If Len(Me.gvAsignatura.DataKeys(e.Row.RowIndex).Values("observacion")) > 60 Then
                        e.Row.Cells(3).Text = Left(Me.gvAsignatura.DataKeys(e.Row.RowIndex).Values("observacion"), 60) & " ..."
                        e.Row.Cells(3).ToolTip = Me.gvAsignatura.DataKeys(e.Row.RowIndex).Values("observacion")
                    Else
                        e.Row.Cells(3).Text = Me.gvAsignatura.DataKeys(e.Row.RowIndex).Values("observacion")
                    End If
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub rb1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Me.cboDiseño.Enabled = rb1.Checked
            Me.rb2.Checked = Not rb1.Checked
            ' Me.rb2.Enabled = Not rb1.Checked
            Me.cboDiseño2.Enabled = Not rb1.Checked
            Me.cboDiseño2.SelectedIndex = 0
            mt_CargarInfoDiseño(0, 0)
            Me.panModal.Update()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub rb2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Me.cboDiseño2.Enabled = rb2.Checked
            Me.rb1.Checked = Not rb2.Checked
            'Me.rb1.Enabled = Not rb2.Checked
            Me.cboDiseño.Enabled = Not rb2.Checked
            Me.cboDiseño.SelectedIndex = 0
            mt_CargarInfoDiseño(0, 0)
            Me.panModal.Update()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboDiseño2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDiseño2.SelectedIndexChanged
        'Dim obj As New ClsConectarDatos
        'Dim dtUni, dtRes, dtCon, dtEst, dtRef As Data.DataTable
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            mt_CargarInfoDiseño(IIf(Me.cboDiseño2.SelectedValue = -1, 0, Me.cboDiseño2.SelectedValue), 2)
            'obj.AbrirConexion()
            'dtUni = obj.TraerDataTable("COM_ListarResultadoAprendizaje", Me.cboDiseño2.SelectedValue, -1, -1, -1, -1)
            'dtRes = obj.TraerDataTable("ResultadoAprendizaje_Listar", "", -1, Me.cboDiseño2.SelectedValue, "")
            'dtCon = obj.TraerDataTable("COM_ListarContenidoAsignatura", Me.cboDiseño2.SelectedValue, -1, 0)
            'dtEst = obj.TraerDataTable("EstrategiaDidactica_Listar", -1, Me.cboDiseño2.SelectedValue, "")
            'dtRef = obj.TraerDataTable("ReferenciaBibliografica_Listar", -1, Me.cboDiseño2.SelectedValue, -1, "")
            'obj.CerrarConexion()
            'Me.gvUA.DataSource = dtUni : Me.gvUA.DataBind()
            'If Me.gvUA.Rows.Count > 0 Then mt_AgruparFilas(Me.gvUA.Rows, 0, 2)
            'Me.gvCE.DataSource = dtRes : Me.gvCE.DataBind()
            'If Me.gvCE.Rows.Count > 0 Then mt_AgruparFilas(Me.gvCE.Rows, 0, 5)
            'Me.gvCA.DataSource = dtCon : Me.gvCA.DataBind()
            'If Me.gvCA.Rows.Count > 0 Then mt_AgruparFilas(Me.gvCA.Rows, 0, 1)
            'Me.gvED.DataSource = dtEst : Me.gvED.DataBind()
            'Me.gvRB.DataSource = dtRef : Me.gvRB.DataBind()
            'Me.panModal.Update()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

    Private Sub mt_CargarSemestre()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCicloAcademico", "DA", "")
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboSemestre, dt, "codigo_Cac", "descripcion_Cac")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCarreraProfesional(ByVal codigo_cac As Integer, ByVal user As Integer, ByVal ctf As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()

            If ctf = 1 Or ctf = 232 Or ctf = 236 Then user = -1
            dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "CD", ctf, -1, -1, codigo_cac, user, ctf)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboCarrProf, dt, "codigo_Cpf", "nombre_Cpf")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer, ByVal codigo_est As Integer, ByVal user As Integer, ByVal ctf As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable("Datos")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            If ctf = 1 Or ctf = 232 Then user = -1

            obj.AbrirConexion()
            dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "AD", codigo_est, codigo_cpf, ctf, codigo_cac, user, ctf) 'ctf: nuevo parámetro, por Luis Q.T | 19DIC2019
            obj.CerrarConexion()

            Session("gc_AsignaturaCA") = dt
            Me.gvAsignatura.DataSource = dt 'CType(Session("gc_AsignaturaCA"), Data.DataTable)
            Me.gvAsignatura.DataBind()
            dt.Dispose()

            Me.txtBuscar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
            Me.btnBuscar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
            If Me.gvAsignatura.Rows.Count > 0 Then fc_tieneCronograma(True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_AgruparFilas(ByVal gridViewRows As GridViewRowCollection, ByVal startIndex As Integer, ByVal totalColumns As Integer)
        If totalColumns = 0 Then Return
        Dim i As Integer, count As Integer = 1
        Dim lst As ArrayList = New ArrayList()
        lst.Add(gridViewRows(0))
        Dim ctrl As TableCell
        ctrl = gridViewRows(0).Cells(startIndex)
        For i = 1 To gridViewRows.Count - 1
            Dim nextTbCell As TableCell = gridViewRows(i).Cells(startIndex)
            If ctrl.Text = nextTbCell.Text Then
                count += 1
                nextTbCell.Visible = False
                lst.Add(gridViewRows(i))
            Else
                If count > 1 Then
                    ctrl.RowSpan = count
                    ctrl.VerticalAlign = VerticalAlign.Middle
                    mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
                End If
                count = 1
                lst.Clear()
                ctrl = gridViewRows(i).Cells(startIndex)
                lst.Add(gridViewRows(i))
            End If
        Next
        If count > 1 Then
            ctrl.RowSpan = count
            ctrl.VerticalAlign = VerticalAlign.Middle
            mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
        End If
        count = 1
        lst.Clear()
    End Sub

    Protected Sub mt_AgregarCabecera(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal celltext As String)
        objtablecell = New TableCell()
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan
        'objtablecell.Style.Add("background-color", backcolor)
        'objtablecell.Style.Add("BackColor", backcolor)
        'objtablecell.Style.Add("Font-Bold", "true")
        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

    Protected Sub mt_AgregarCabecera(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal rowspan As Integer, ByVal celltext As String, Optional ByVal tooltip As String = "")
        objtablecell = New TableCell()
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan
        objtablecell.RowSpan = rowspan
        objtablecell.ToolTip = tooltip
        objtablecell.VerticalAlign = VerticalAlign.Middle
        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

    Private Sub mt_CargarDiseñoAsignatura(ByVal codigo_pes As Integer, ByVal codigo_cur As Integer, ByVal codigo_cac As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "CL", -1, codigo_pes, codigo_cur, codigo_cac, -1)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboDiseño, dt, "codigo_dis", "descripcion_Cac")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CopiarPdf(ByVal bytes_pdf() As Byte, ByVal bytes_anexo() As Byte, ByVal memory As System.IO.Stream, Optional ByVal lb_adenda As Boolean = False)
        Dim pdfFile_anexo As iTextSharp.text.pdf.PdfReader
        Dim pdfFile_silabo As iTextSharp.text.pdf.PdfReader
        Dim pdfFile_adenda As iTextSharp.text.pdf.PdfReader
        Dim doc As iTextSharp.text.Document
        Dim pCopy As iTextSharp.text.pdf.PdfWriter
        'Dim msOutput As MemoryStream = New MemoryStream()
        pdfFile_anexo = New iTextSharp.text.pdf.PdfReader(bytes_anexo)
        pdfFile_silabo = New iTextSharp.text.pdf.PdfReader(bytes_pdf)
        If lb_adenda Then pdfFile_adenda = New iTextSharp.text.pdf.PdfReader(Server.MapPath(".") & "/Adenda.pdf")
        doc = New iTextSharp.text.Document()
        pCopy = New iTextSharp.text.pdf.PdfSmartCopy(doc, memory)
        doc.Open()

        'For k As Integer = 0 To files.Count - 1
        'pdfFile = New iTextSharp.text.pdf.PdfReader(Server.MapPath(".") & "/ManualTutoria.pdf")

        'mt_AddWaterMark(pCopy, "BORRADOR")

        For j As Integer = 1 To pdfFile_silabo.NumberOfPages
            CType(pCopy, iTextSharp.text.pdf.PdfSmartCopy).AddPage(pCopy.GetImportedPage(pdfFile_silabo, j))
            'mt_AddWaterMark(pCopy, "BORRADOR")
        Next

        pCopy.FreeReader(pdfFile_silabo)
        pdfFile_silabo.Close()

        'mt_AddWaterMark(pCopy, "BORRADOR")

        If lb_adenda Then
            For g As Integer = 1 To pdfFile_adenda.NumberOfPages
                CType(pCopy, iTextSharp.text.pdf.PdfSmartCopy).AddPage(pCopy.GetImportedPage(pdfFile_adenda, g))
            Next
            pCopy.FreeReader(pdfFile_adenda)
            pdfFile_adenda.Close()
        End If

        For i As Integer = 1 To pdfFile_anexo.NumberOfPages
            CType(pCopy, iTextSharp.text.pdf.PdfSmartCopy).AddPage(pCopy.GetImportedPage(pdfFile_anexo, i))
            'mt_AddWaterMark(pCopy, "BORRADOR")
        Next

        pCopy.FreeReader(pdfFile_anexo)
        'Next

        pdfFile_anexo.Close()

        pCopy.Close()
        doc.Close()
    End Sub

    Private Sub mt_CopiarPdf(ByVal bytes_pdf() As Byte, ByVal memory As System.IO.Stream)
        Dim pdfFile_silabo As iTextSharp.text.pdf.PdfReader
        Dim pdfFile_adenda As iTextSharp.text.pdf.PdfReader
        Dim doc As iTextSharp.text.Document
        Dim pCopy As iTextSharp.text.pdf.PdfWriter
        pdfFile_silabo = New iTextSharp.text.pdf.PdfReader(bytes_pdf)
        pdfFile_adenda = New iTextSharp.text.pdf.PdfReader(Server.MapPath(".") & "/Adenda.pdf")
        doc = New iTextSharp.text.Document()
        pCopy = New iTextSharp.text.pdf.PdfSmartCopy(doc, memory)
        doc.Open()

        For j As Integer = 1 To pdfFile_silabo.NumberOfPages
            CType(pCopy, iTextSharp.text.pdf.PdfSmartCopy).AddPage(pCopy.GetImportedPage(pdfFile_silabo, j))
        Next

        pCopy.FreeReader(pdfFile_silabo)
        pdfFile_silabo.Close()


        For g As Integer = 1 To pdfFile_adenda.NumberOfPages
            CType(pCopy, iTextSharp.text.pdf.PdfSmartCopy).AddPage(pCopy.GetImportedPage(pdfFile_adenda, g))
        Next

        pCopy.FreeReader(pdfFile_adenda)
        pdfFile_adenda.Close()

        pCopy.Close()
        doc.Close()
    End Sub

    Private Sub mt_CargarDiseñoAsignatura2(ByVal codigo_pes As Integer, ByVal codigo_cur As Integer, ByVal codigo_cac As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            'Throw New Exception(codigo_pes & "-" & codigo_cur & "-" & codigo_cac)
            dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "C2", -1, codigo_pes, codigo_cur, codigo_cac, -1)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboDiseño2, dt, "codigo_dis", "descripcion_Cac")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarInfoDiseño(ByVal codigo_dis As Integer, ByVal _tipo As Integer)
        Dim obj As New ClsConectarDatos
        Dim dtUni, dtRes, dtCon, dtEst, dtRef As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dtUni = obj.TraerDataTable("COM_ListarResultadoAprendizaje", codigo_dis, iif(_tipo = 2, -1, Session("gc_codigo_pes")), -1, -1, -1)
            dtRes = obj.TraerDataTable("ResultadoAprendizaje_Listar", "", -1, codigo_dis, "")
            dtCon = obj.TraerDataTable("COM_ListarContenidoAsignatura", codigo_dis, -1, 0)
            dtEst = obj.TraerDataTable("EstrategiaDidactica_Listar", -1, codigo_dis, "")
            dtRef = obj.TraerDataTable("ReferenciaBibliografica_Listar", -1, codigo_dis, -1, "")
            obj.CerrarConexion()
            Me.gvUA.DataSource = dtUni : Me.gvUA.DataBind()
            If Me.gvUA.Rows.Count > 0 Then mt_AgruparFilas(Me.gvUA.Rows, 0, 2)
            Me.gvCE.DataSource = dtRes : Me.gvCE.DataBind()
            If Me.gvCE.Rows.Count > 0 Then mt_AgruparFilas(Me.gvCE.Rows, 0, 5)
            Me.gvCA.DataSource = dtCon : Me.gvCA.DataBind()
            If Me.gvCA.Rows.Count > 0 Then mt_AgruparFilas(Me.gvCA.Rows, 0, 1)
            Me.gvED.DataSource = dtEst : Me.gvED.DataBind()
            Me.gvRB.DataSource = dtRef : Me.gvRB.DataBind()
            Me.panModal.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_AddWaterMark(ByVal pdfwrite As iTextSharp.text.pdf.PdfWriter, ByVal texto As String)
        Dim bfTimes As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.TIMES_ITALIC, iTextSharp.text.pdf.BaseFont.CP1252, False)
        Dim times As iTextSharp.text.Font = New iTextSharp.text.Font(bfTimes, 145.5F, iTextSharp.text.Font.ITALIC, iTextSharp.text.BaseColor.LIGHT_GRAY)
        iTextSharp.text.pdf.ColumnText.ShowTextAligned(pdfwrite.DirectContent, 1, New iTextSharp.text.Phrase(texto, times), 295.5F, 450.0F, 55)
    End Sub

#End Region

#Region "Funciones"

    Private Function fc_validarRA() As String
        Dim i, codigo_res, codigo_ind, codigo_ins, tipo_prom, tipo_prom2 As Integer
        Dim peso_res, peso_ind, peso_ins As Double
        Dim nombre_res, nombre_ind, nombre_evi, nombre_ins As String
        Dim respon As String = "[OK]"
        codigo_res = -1 : codigo_ind = -1 : peso_res = 0
        nombre_res = String.Empty : nombre_ind = String.Empty : nombre_evi = String.Empty : nombre_ins = String.Empty
        For i = 0 To Me.gvResultados.Rows.Count - 1
            If codigo_res <> CInt(Me.gvResultados.DataKeys(i).Values("codigo_res")) Then
                If i > 0 And tipo_prom = 2 And Math.Round(peso_ind, 2) <> 1 Then
                    respon = "¡ El Resultado de Aprendizaje: " & nombre_res & ", la suma de los pesos de sus indicadores no es 100% !" : Return respon
                End If
                codigo_res = CInt(Me.gvResultados.DataKeys(i).Values("codigo_res"))
                nombre_res = Me.gvResultados.DataKeys(i).Values("descripcion_res")
                tipo_prom = CInt(Me.gvResultados.DataKeys(i).Values("tipo_prom"))
                If CDbl(Me.gvResultados.DataKeys(i).Values("peso_res")) = 0.0 Then
                    respon = "¡ El Resultado de Aprendizaje: " & nombre_res & ", no tiene peso !" : Return respon
                End If
                peso_res += CDbl(Me.gvResultados.DataKeys(i).Values("peso_res"))
                peso_ind = 0
            End If
            If CInt(Me.gvResultados.DataKeys(i).Values("codigo_ind")) = -1 Then
                respon = "¡ El Resultado de Aprendizaje: " & nombre_res & ", no se ha registrado un indicador !" : Return respon
            End If
            'nombre_ind = Me.gvResultados.DataKeys(i).Values("descripcion_ind")
            If codigo_ind <> CInt(Me.gvResultados.DataKeys(i).Values("codigo_ind")) Then
                codigo_ind = CInt(Me.gvResultados.DataKeys(i).Values("codigo_ind"))
                nombre_ind = Me.gvResultados.DataKeys(i).Values("descripcion_ind")
                tipo_prom2 = CInt(Me.gvResultados.DataKeys(i).Values("tipo_prom2"))
                peso_ins = 0
                If tipo_prom = 2 Then
                    If CDbl(Me.gvResultados.DataKeys(i).Values("peso_ind")) = 0.0 Then
                        respon = "¡ El Indicador de Aprendizaje: " & nombre_ind & ", no tiene peso !" : Return respon
                    End If
                    peso_ind += CDbl(Me.gvResultados.DataKeys(i).Values("peso_ind"))
                End If
            End If
            If CInt(Me.gvResultados.DataKeys(i).Values("codigo_indEvi")) = -1 Then
                respon = "¡ El Indicador de Aprendizaje: " & nombre_ind & ", no se ha registrado una evidencia !" : Return respon
            End If
            nombre_evi = Me.gvResultados.DataKeys(i).Values("descripcion_evi")
            If CInt(Me.gvResultados.DataKeys(i).Values("codigo_eviIns")) = -1 Then
                respon = "¡ la Evidencia de Aprendizaje: " & nombre_evi & ", no se ha registrado un instrumento !" : Return respon
            End If
            If codigo_ins <> CInt(Me.gvResultados.DataKeys(i).Values("codigo_ins")) Then
                codigo_ins = CInt(Me.gvResultados.DataKeys(i).Values("codigo_ins"))
                nombre_ins = Me.gvResultados.DataKeys(i).Values("descripcion_ins")
                If tipo_prom2 = 2 Then
                    If CDbl(Me.gvResultados.DataKeys(i).Values("peso_ins")) = 0.0 Then
                        respon = "¡ El Instrumento de Evaluación: " & nombre_ins & ", no tiene peso !" : Return respon
                    End If
                    peso_ins += CDbl(Me.gvResultados.DataKeys(i).Values("peso_ins"))
                End If
            End If
        Next
        If Math.Round(peso_res, 2) <> 1 Then respon = "¡ La suma de los pesos de los Resultados de Aprendizaje no es 100% ! " : Return respon
        If tipo_prom = 2 And Math.Round(peso_ind, 2) <> 1 Then
            respon = "¡ El Resultado de Aprendizaje: " & nombre_res & ", la suma de los pesos de sus indicadores no es 100% !" : Return respon
        End If
        If tipo_prom2 = 2 And Math.Round(peso_ins, 2) <> 1 Then
            respon = "¡ El Indicador de Aprendizaje: " & nombre_ind & ", la suma de los pesos de sus instrumentos no es 100% !" : Return respon
        End If
        Return respon
    End Function

    Private Function fc_validarRB() As String
        Dim i, cont_usat, cont_comp, codigo_tip As Integer
        Dim nombre_ref As String
        Dim respon As String = "[OK]"
        If Me.gvReferencia.Rows.Count = 0 Then respon = "¡ Ingrese al menos una Referencia Bibliográfica para esta asignatura !" : Return respon
        cont_usat = 0 : cont_comp = 0
        For i = 0 To Me.gvReferencia.Rows.Count - 1
            codigo_tip = CInt(Me.gvReferencia.DataKeys(i).Values("codigo_tip"))
            nombre_ref = Me.gvReferencia.DataKeys(i).Values("nombre")
            'If codigo_tip = 3 Then cont_usat += 1
            'If codigo_tip = 4 Then cont_comp += 1
            If nombre_ref = "USAT" Then cont_usat += 1
            If nombre_ref = "Complementarias" Then cont_comp += 1
        Next
        If cont_usat = 0 Then respon = "¡ Ingrese al menos una Referencia Bibliográfica USAT !" : Return respon
        If cont_comp = 0 Then respon = "¡ Ingrese al menos una Referencia Bibliográfica Complementarias !" : Return respon
        Return respon
    End Function

    Private Function fc_ValidarCA() As String
        Dim respon As String = "[OK]"
        Dim _flag As Boolean = False
        Dim _nombre_uni As String
        Dim _codigo_uni, _codigo_aux As Integer
        If Me.gvContenido.Rows.Count = 0 Then respon = "¡ Ingrese al menos un contenido por unidad de la asignatura !" : Return respon
        For i As Integer = 0 To Me.gvUnidad.Rows.Count - 1
            _flag = False
            _codigo_uni = CInt(Me.gvUnidad.DataKeys(i).Values("codigo_uni"))
            _nombre_uni = Me.gvUnidad.DataKeys(i).Values("descripcion_uni")
            For j As Integer = 0 To Me.gvContenido.Rows.Count - 1
                _codigo_aux = Me.gvContenido.DataKeys(j).Values("codigo_uni")
                If _codigo_uni = _codigo_aux Then
                    _flag = True
                    Exit For
                End If
            Next
            If Not _flag Then
                respon = "¡ Ingrese contenido a la Unidad: " & _nombre_uni & " !" : Return respon
            End If
        Next
        Return respon
    End Function

    Private Function fc_SubirArchivo(ByVal idTabla As String, ByVal codigo As String, ByVal file As HttpPostedFile) As String
        Dim list As New Dictionary(Of String, String)
        Try
            Dim _TablaId As String = idTabla
            Dim _Archivo As HttpPostedFile = file
            Dim _TransaccionId As String = codigo
            Dim _Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim _Usuario As String = Session("perlogin").ToString
            Dim Input(_Archivo.ContentLength) As Byte

            Dim br As New IO.BinaryReader(_Archivo.InputStream)
            Dim binData As Byte() = br.ReadBytes(_Archivo.InputStream.Length)
            Dim base64 As Object = System.Convert.ToBase64String(binData)
            Dim _Nombre As String = IO.Path.GetFileName(_Archivo.FileName.Replace("&", "_").Replace("'", "_").Replace("*", "_"))

            Dim wsCloud As New ClsArchivosCompartidos

            list.Add("Fecha", _Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(_Archivo.FileName))
            list.Add("Nombre", _Nombre)
            list.Add("TransaccionId", _TransaccionId)
            list.Add("TablaId", _TablaId)
            list.Add("NroOperacion", "")
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", _Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", _Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Session("perlogin").ToString)

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function fc_ObtenerArchivo(ByVal idTransa As Long, ByVal idTabla As Integer, ByVal token As String, ByVal IdArchivo As Long) As Byte()
        Try
            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Generic.Dictionary(Of String, String)
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim usuario As String = Session("perlogin")
            Dim bytes() As Byte = Nothing

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, idTabla, idTransa, token)
            obj.CerrarConexion()

            If tb.Rows.Count = 0 Then
                'Throw New Exception("¡ Archivo no encontrado !")
                obj.AbrirConexion()
                'tbDis = obj.TraerDataTable("DiseñoAsignatura_Listar", "", idTransa, -1, -1, -1, -1)
                tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 3, idTabla, IdArchivo, token)
                If tb.Rows.Count = 0 Then Throw New Exception("¡ Archivo no encontrado !")

                obj.CerrarConexion()
            End If

            list.Add("IdArchivo", tb.Rows(0).Item("IdArchivo").ToString)
            list.Add("Usuario", usuario)
            list.Add("Token", token)

            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", usuario)
            Dim imagen As String = fc_ResultFile(result)

            If tb.Rows.Count > 0 Then
                bytes = Convert.FromBase64String(imagen)
            End If
            'Response.Write(envelope)

            Return bytes

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function fc_ResultFile(ByVal cadXml As String) As String
        Try
            Dim xError As String()
            Dim nsMgr As System.Xml.XmlNamespaceManager
            Dim xml As System.Xml.XmlDocument = New System.Xml.XmlDocument()
            xml.LoadXml(cadXml)
            nsMgr = New System.Xml.XmlNamespaceManager(xml.NameTable)
            nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim res As System.Xml.XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body", nsMgr)
            xError = res.InnerText.Split(":")
            If xError.Length = 2 Then
                Throw New Exception(res.InnerText)
            End If
            Return res.InnerText
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function fc_ObtieneCodCup(ByVal _codcac As Integer, ByVal _codpes As Integer, ByVal _codcur As Integer) As Long
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            Dim _codcup As Long = -1
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "XP", -1, _codpes, _codcur, _codcac, -1)
            If dt.Rows.Count > 0 Then
                _codcup = CLng(dt.Rows(0).Item("codigo_cup"))
            End If
            obj.CerrarConexion()
            Return _codcup
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function fc_tieneCronograma(Optional ByVal mostrarEnEtiqueta As Boolean = False) As Boolean
        Dim obj As New ClsConectarDatos
        Dim objRes As Object()
        Dim codigo_cac As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        '[Inicio] Condición por Luis Q.T| 17DIC2019
        codigo_cac = IIf(String.IsNullOrEmpty(Me.cboSemestre.SelectedValue), "0", Me.cboSemestre.SelectedValue)

        obj.AbrirConexion()
        objRes = obj.Ejecutar("DEA_validarAccionEnCronograma", "DA", codigo_cac, "2")
        obj.CerrarConexion()

        If Not mostrarEnEtiqueta AndAlso cod_ctf = 1 Then 'Agregado por Luis Q.T| 17DIC2019 | Permite que el administrador visualice el diseño
            Return True
        End If

        If objRes Is Nothing Then
            If Not mostrarEnEtiqueta Then Call mt_ShowMessage("El plazo para diseñar la asignatura ha caducado. Consulte con Dirección Académica", MessageType.Info)
            lblMensaje.Text = "El plazo para diseñar la asignatura ha caducado. Consulte con Dirección Académica"
            Return False
        Else
            If objRes(0).ToString.Equals("0") Then
                If Not mostrarEnEtiqueta Then Call mt_ShowMessage("El plazo para diseñar la asignatura ha caducado. Consulte con Dirección Académica", MessageType.Info)
                lblMensaje.Text = "El plazo para diseñar la asignatura ha caducado. Consulte con Dirección Académica"
                Return False
            End If
        End If
        '[Fin] Condición por Luis Q.T| 17DIC2019

        lblMensaje.Text = ""
        Return True
    End Function

#End Region

End Class

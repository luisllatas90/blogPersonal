Imports System.Collections.Generic

Partial Class GestionCurricular_frmConfigurarAsignatura
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private cod_user As Integer '= 5751
    Private cod_ctf As Integer
    Private idTabla As Integer = 24
    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles") '"http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"

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
                Response.Redirect("../../../sinacceso.html")
            End If
            cod_user = Session("id_per")
            cod_ctf = Request.QueryString("ctf")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            cod_user = Session("id_per")

            If IsPostBack = False Then
                'Session("gc_codigo_cac") = 0
                'Session("gc_codigo_cpf") = 0
                'Session("gc_codigo_est") = 0
                'Session("gc_codigo_pes") = 0
                'Session("gc_codigo_cur") = 0
                'Session("gc_codigo_dis") = 0
                'Session("gc_nombre_cur") = ""
                mt_CargarSemestre()
                Me.txtBuscar.Visible = False
                Me.btnBuscar.Visible = False
                Me.txtBuscar.Attributes.Add("onkeypress", "txtBuscar_onKeyPress(this,'" & Me.btnBuscar.ClientID & "')")
                btnFuArchivo.Attributes.Add("onClick", "document.getElementById('" + fuArchivo.ClientID + "').click();")
            End If
            If Not String.IsNullOrEmpty(Session("gc_codigo_cac")) Then
                If cboSemestre.Items.Count > 0 Then Me.cboSemestre.SelectedValue = Session("gc_codigo_cac")
                mt_CargarCarreraProfesional(Me.cboSemestre.SelectedValue, cod_user)
                If cboCarrProf.Items.Count > 0 Then Me.cboCarrProf.SelectedValue = Session("gc_codigo_cpf")
                Me.cboEstado.SelectedValue = Session("gc_codigo_est")
                mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), IIf(Me.cboCarrProf.SelectedValue = -1, 0, Me.cboCarrProf.SelectedValue), Me.cboEstado.SelectedValue, cod_user)
            End If
            'Session("gc_codigo_cac") = "" : Session("gc_codigo_cpf") = "" : Session("gc_codigo_pes") = "" : Session("gc_codigo_cur") = "" : Session("gc_codigo_dis") = "" : Session("gc_nombre_cur") = "" : Session("gc_codigo_est") = ""
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSemestre.SelectedIndexChanged
        Try
            mt_CargarCarreraProfesional(Me.cboSemestre.SelectedValue, cod_user)
            If Me.cboCarrProf.Items.Count > 0 Then Me.cboCarrProf.SelectedIndex = 0
            If Me.gvAsignatura.Rows.Count > 0 Then mt_CargarDatos(0, 0, -1, cod_user)
            Session("gc_codigo_cac") = ""
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboCarrProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarrProf.SelectedIndexChanged
        Try
            Me.cboEstado.SelectedIndex = 1
            mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), IIf(Me.cboCarrProf.SelectedValue = -1, 0, Me.cboCarrProf.SelectedValue), Me.cboEstado.SelectedValue, cod_user)
            Session("gc_codigo_cac") = ""
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvAsignatura_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAsignatura.PageIndexChanging
        Me.gvAsignatura.DataSource = CType(Session("gc_AsignaturaCA"), Data.DataTable)
        Me.gvAsignatura.DataBind()
        Me.gvAsignatura.PageIndex = e.NewPageIndex
        Me.gvAsignatura.DataBind()
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim dt, dtAux As New Data.DataTable
            Dim dv As Data.DataView
            If Me.txtBuscar.Text.Trim <> "" Then
                dv = New Data.DataView(CType(Session("gc_AsignaturaCA"), Data.DataTable), "nombre_Cur like '" & Me.txtBuscar.Text.Trim & "%'".Trim, "", Data.DataViewRowState.CurrentRows)
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
            Select Case e.CommandName
                Case "FrmResultadoAprendizaje", "frmCriteriosEvaluacion", "FrmContenidosAsignatura", "frmEstrategiasDidactica", "frmReferenciaBibliografica"
                    Response.Redirect("~/gestioncurricular/" & e.CommandName & ".aspx", False)
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
                    mt_CargarDiseñoAsignatura(codigo_Pes, codigo_Cur, Me.cboSemestre.SelectedValue)
                    Me.gvUA.DataSource = Nothing : Me.gvUA.DataBind()
                    Me.gvCE.DataSource = Nothing : Me.gvCE.DataBind()
                    Me.gvCA.DataSource = Nothing : Me.gvCA.DataBind()
                    Me.gvED.DataSource = Nothing : Me.gvED.DataBind()
                    Me.gvRB.DataSource = Nothing : Me.gvRB.DataBind()
                    'Me.gvED.DataSource = Nothing : Me.gvED.DataBind()
                    Me.panModal.Update()
                    'mt_ShowMessage(codigo_Pes & " - " & codigo_Cur & " - " & Me.cboSemestre.SelectedValue, MessageType.Success)
                    Page.RegisterStartupScript("Pop", "<script>openModal('2');</script>")
                Case "Subir"
                    obj.AbrirConexion()
                    dtAnexo = obj.TraerDataTable("COM_ObtenerAnexoAsignatura", codigo_dis, idTabla)
                    obj.CerrarConexion()

                    If dtAnexo.Rows.Count > 0 Then
                        If dtAnexo.Rows(0).Item("archivo").ToString.Trim.Contains("No se eligió") Then
                            spnFile.InnerText = dtAnexo.Rows(0).Item("archivo").ToString.Trim
                        Else
                            spnFile.InnerText = dtAnexo.Rows(0).Item("archivo").ToString.Trim & " (Subido el: " & dtAnexo.Rows(0).Item("fechaAnexo_dis").ToString.Trim & ")"
                        End If
                    End If
                    dtAnexo.Dispose()

                    Me.panModal.Update()
                    Page.RegisterStartupScript("Pop", "<script>openModal('3');</script>")
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
                mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), IIf(Me.cboCarrProf.SelectedValue = -1, 0, Me.cboCarrProf.SelectedValue), Me.cboEstado.SelectedValue, cod_user)
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
            mt_CargarDatos(Me.cboSemestre.SelectedValue, Me.cboCarrProf.SelectedValue, Me.cboEstado.SelectedValue, cod_user)
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
                mt_AgregarCabecera(objgridviewrow, objtablecell, 3, 1, "Acciones")
                objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGuardar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar2.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            obj.Ejecutar("DEA_DiseñoAsignatura_clonar", Me.cboDiseño.SelectedValue, Me.cboSemestre.SelectedValue, cod_user)
            mt_ShowMessage("Se guardo correctamente el Diseño de Asignatura", MessageType.Success)
            obj.CerrarConexion()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboDiseño_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDiseño.SelectedIndexChanged
        Dim obj As New ClsConectarDatos
        Dim dtUni, dtRes, dtCon, dtEst, dtRef As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dtUni = obj.TraerDataTable("COM_ListarResultadoAprendizaje", Me.cboDiseño.SelectedValue, -1, -1, -1, -1)
            dtRes = obj.TraerDataTable("ResultadoAprendizaje_Listar", "", -1, Me.cboDiseño.SelectedValue, "")
            dtCon = obj.TraerDataTable("COM_ListarContenidoAsignatura", Me.cboDiseño.SelectedValue, -1, 0)
            dtEst = obj.TraerDataTable("EstrategiaDidactica_Listar", -1, Me.cboDiseño.SelectedValue, "")
            dtRef = obj.TraerDataTable("ReferenciaBibliografica_Listar", -1, Me.cboDiseño.SelectedValue, -1, "")
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
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub


    Protected Sub btnGuardarAnexo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarAnexo.Click
        'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'Dim JSONresult As String = ""
        'Dim Data As New Dictionary(Of String, Object)()
        'Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsConectarDatos
        Dim codigo_dis As Integer
        Dim dt As New Data.DataTable
        Dim respuesta As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            codigo_dis = Session("gc_codigo_dis")

            If Not (IsDBNull(Session("perlogin"))) And Session("perlogin").ToString <> "" Then

                If codigo_dis = 0 Then Throw New Exception("¡ Seleccione una Asignatura !")

                If Me.fuArchivo.HasFile Then
                    Dim Archivos As HttpFileCollection = Request.Files
                    For i As Integer = 0 To Archivos.Count - 1
                        'Data.Add("Step", idTabla & "-" & codigo_dis & "-" & Archivos(i).FileName)

                        Call fc_SubirArchivo(idTabla, codigo_dis, Archivos(i))
                    Next
                End If

                obj.AbrirConexion()
                obj.Ejecutar("DiseñoAsignatura_SubirAnexo", idTabla, codigo_dis)
                obj.CerrarConexion()
            Else
                Throw New Exception("Inicie Sesión")
            End If

            Call mt_ShowMessage("¡ Anexo actualizado correctamente !", MessageType.Success)

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

    Private Sub mt_CargarCarreraProfesional(ByVal codigo_cac As Integer, ByVal user As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            'user = 356 '359
            If cod_ctf = 1 Or cod_ctf = 232 Then user = -1
            dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "CD", -1, -1, -1, codigo_cac, user)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboCarrProf, dt, "codigo_Cpf", "nombre_Cpf")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer, ByVal codigo_est As Integer, ByVal user As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable("Datos")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            'cod_user = 356 '359
            If cod_ctf = 1 Or cod_ctf = 232 Then user = -1
            dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "AD", codigo_est, codigo_cpf, -1, codigo_cac, user)
            obj.CerrarConexion()
            Session("gc_AsignaturaCA") = dt
            Me.gvAsignatura.DataSource = CType(Session("gc_AsignaturaCA"), Data.DataTable)
            Me.gvAsignatura.DataBind()
            Me.txtBuscar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
            Me.btnBuscar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
            'Me.gvCoordinador.PageIndex = 0
            'dt.Dispose()
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

#End Region

End Class

﻿Imports System.Collections.Generic
Imports System.IO

Partial Class GestionCurricular_frmPublicarSilabo
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private cod_user As Integer
    Private cod_ctf As Integer
    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles") '"http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"
    Private odCursoProgramado As d_CursoProgramado, oeCursoProgramado As e_CursoProgramado ' 20191230 - ENevado
    
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
            cod_ctf = Request.QueryString("ctf")

            If IsPostBack = False Then
                Session("gc_dtCursoProg") = Nothing
                Call mt_CargarSemestre()

                Me.txtBuscar.Attributes.Add("onKeyPress", "txtBuscar_onKeyPress('" & Me.btnBuscar.ClientID & "', event)")
                Me.txtBuscar.Visible = False
                Me.btnBuscar.Visible = False
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSemestre.SelectedIndexChanged
        Try
            mt_CargarTipoEstudio(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue))
            If cboCarPro.Items.Count > 0 Then Me.cboCarPro.SelectedIndex = 0
            If cboPlanEstudio.Items.Count > 0 Then Me.cboPlanEstudio.SelectedIndex = 0
            If Me.gvAsignatura.Rows.Count > 0 Then Me.gvAsignatura.DataSource = Nothing : Me.gvAsignatura.DataBind()
            lblMensaje.Text = ""
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipoEstudio.SelectedIndexChanged
        Try
            mt_CargarCarreraProfesional(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), Me.cboTipoEstudio.SelectedValue, cod_user)
            If cboPlanEstudio.Items.Count > 0 Then Me.cboPlanEstudio.SelectedIndex = 0
            If Me.gvAsignatura.Rows.Count > 0 Then Me.gvAsignatura.DataSource = Nothing : Me.gvAsignatura.DataBind()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboCarPro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarPro.SelectedIndexChanged
        Try
            mt_CargarPlanEstudio(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), Me.cboCarPro.SelectedValue)
            If Me.gvAsignatura.Rows.Count > 0 Then Me.gvAsignatura.DataSource = Nothing : Me.gvAsignatura.DataBind()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboPlanEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPlanEstudio.SelectedIndexChanged
        Try
            mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), Me.cboPlanEstudio.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnDescargar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'btnListar_Click(Nothing, Nothing)
            'Page.RegisterStartupScript("BusyBox", "<script>fc_ocultarBusy();</script>")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    'EPENA 23/08/2019{
    Protected Sub btnDespublicar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'btnListar_Click(Nothing, Nothing)
            'Page.RegisterStartupScript("BusyBox", "<script>fc_ocultarBusy();</script>")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub
    '}EPENA 23/08/2019
    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        Dim codigo_dis As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            codigo_dis = Session("gc_codigo_dis")
            obj.AbrirConexion()
            obj.Ejecutar("DiseñoAsignatura_observar", codigo_dis, Me.txtObservacion.Text, cod_user)
            obj.CerrarConexion()
            mt_ShowMessage("¡ Se ha registrado la observación al sílabo !", MessageType.Success)
            cboPlanEstudio_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            If Session("gc_dtCursoProg") Is Nothing Then
                Call mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), Me.cboPlanEstudio.SelectedValue)
            End If

            Dim dt As New Data.DataTable
            Dim dv As New Data.DataView
            Dim strBuscar As String = ""

            If Me.txtBuscar.Text.Trim <> "" Then
                strBuscar = Me.txtBuscar.Text.Trim.ToUpper.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U")
                dv = New Data.DataView(CType(Session("gc_dtCursoProg"), Data.DataTable), "nombre_Cur_Aux like '%" & strBuscar & "%'", "", Data.DataViewRowState.CurrentRows)
                dt = dv.ToTable
            Else
                dt = CType(Session("gc_dtCursoProg"), Data.DataTable)
            End If

            Me.gvAsignatura.DataSource = dt
            Me.gvAsignatura.DataBind()

            Me.txtBuscar.Focus()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvAsignatura_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim objGridView As GridView = CType(sender, GridView)
                Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objtablecell As TableCell = New TableCell()
                mt_AgregarCabecera(objgridviewrow, objtablecell, 5, 1, "Mis Asignaturas")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, 1, "Diseño")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, 1, "Evaluaciones")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, 1, "Sesiones")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, 1, "Fechas")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, 1, "")
                objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvAsignatura_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAsignatura.RowCommand
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim Data As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim clsPdf As New clsGenerarPDF
        Dim memory As New System.IO.MemoryStream
        Dim memory_pdf As New System.IO.MemoryStream
        Dim index, codigo_cup, codigo_cur, codigo_pes, codigo_cac, codigo_dis As Integer
        Dim obj As New ClsConectarDatos
        Dim idTransa, var As Long

        Try
            index = CInt(e.CommandArgument)
            codigo_cup = CInt(Me.gvAsignatura.DataKeys(index).Values("codigo_cup"))
            idTransa = CLng(Me.gvAsignatura.DataKeys(index).Values("codigo_dis"))
            var = CLng(Me.gvAsignatura.DataKeys(index).Values("IdArchivo_Anexo"))
            
            Select Case e.CommandName
                Case "Ver"
                    'Dim curso As String
                    'curso = Me.gvAsignatura.DataKeys(index).Values("nombre_Cur") & " " & Me.gvAsignatura.DataKeys(index).Values("grupoHor_Cup")

                    clsPdf.fuente = Server.MapPath(".") & "/segoeui.ttf"
                    'clsPdf.anexo = Server.MapPath(".") & "/ManualTutoria.pdf"

                    clsPdf.mt_GenerarSilabo(codigo_cup, Server.MapPath(".") & "/logo_usat.png", memory, True)

                    Dim bytes() As Byte = memory.ToArray
                    memory.Close()

                    
                    If var <> -1 Then
                        Dim bytes_anexo() As Byte = fc_ObtenerArchivo(idTransa, 24, "SU3WMBVV4W", var)
                        'mt_CopiarPdf(bytes, bytes_anexo, memory_pdf, True)
                        mt_CopiarPdf(bytes, bytes_anexo, memory_pdf)
                        Dim bytes_pdf() As Byte = memory_pdf.ToArray
                        memory_pdf.Close()
                        Response.Clear()
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("content-length", bytes_pdf.Length.ToString())
                        'Response.AddHeader("content-disposition", "filename=" & curso.Replace(",", "") & ";size=" & bytes_pdf.Length.ToString())
                        Response.Write(idTransa)
                        Response.BinaryWrite(bytes_pdf)
                    Else
                        '' 20200331-ENevado ================================================
                        'mt_CopiarPdf(bytes, memory_pdf)
                        'Dim bytes_adenda() As Byte = memory_pdf.ToArray
                        'memory_pdf.Close()
                        ''==================================================================
                        Response.Clear()
                        Response.ContentType = "application/pdf"
                        'Response.AddHeader("content-length", bytes_adenda.Length.ToString())
                        ''Response.AddHeader("content-disposition", "filename=" & curso.Replace(",", "") & ";size=" & bytes.Length.ToString())
                        'Response.BinaryWrite(bytes_adenda)
                        Response.AddHeader("content-length", bytes.Length.ToString())
                        'Response.AddHeader("content-disposition", "filename=" & curso.Replace(",", "") & ";size=" & bytes.Length.ToString())
                        Response.BinaryWrite(bytes)
                    End If

                    'OnClientClick="return confirm('¿Desea visualizar el silabo?');"

                    'mt_ShowMessage("no seas malcriado", MessageType.Warning)

                Case "Publicar"
                    If fc_tieneCronograma("publicar") Then

                        Dim codigo_sib As Integer
                        Dim dt As New Data.DataTable
                        Dim respuesta, nombre_silabo, curso, grupo, semestre As String
                        Dim _modular As Boolean

                        respuesta = "" : nombre_silabo = ""
                        curso = Me.gvAsignatura.DataKeys(index).Values("nombre_Cur")
                        grupo = Me.gvAsignatura.DataKeys(index).Values("grupoHor_Cup")
                        semestre = Me.gvAsignatura.DataKeys(index).Values("descripcion_Cac")
                        _modular = CBool(Me.gvAsignatura.DataKeys(index).Values("modular_pcu"))

                        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                        If Not (IsDBNull(Session("perlogin"))) And Session("perlogin").ToString <> "" Then
                            obj.IniciarTransaccion()
                            dt = obj.TraerDataTable("SilaboCurso_insertar", codigo_cup, 0, Date.Now.Date, 1, cod_user)
                            If dt.Rows.Count = 0 Then Throw New Exception("¡ No se pudo realizar la publicación del sílabo")
                            codigo_sib = CInt(dt.Rows(0).Item(0).ToString)

                            'Data.Add("Step", idtabla & "-" & codigo_sib & "-" & CStr(codigo_cup) & ".pdf")
                            clsPdf.fuente = Server.MapPath(".") & "/segoeui.ttf"
                            If _modular Then
                                clsPdf.mt_GenerarSilabo2(codigo_cup, Server.MapPath(".") & "/logo_usat.png", memory)
                            Else
                                clsPdf.mt_GenerarSilabo(codigo_cup, Server.MapPath(".") & "/logo_usat.png", memory)
                            End If
                            'clsPdf.mt_GenerarSilabo(codigo_cup, Server.MapPath(".") & "/logo_usat.png", memory)
                            nombre_silabo = "Silabo " & semestre & " " & curso & " " & grupo & ".pdf"
                            'mt_ShowMessage(nombre_silabo, MessageType.Warning)
                            If var <> -1 Then
                                Dim bytes() As Byte = memory.ToArray
                                memory.Close()
                                Dim bytes_anexo() As Byte = fc_ObtenerArchivo(idTransa, 24, "SU3WMBVV4W", var)
                                'mt_CopiarPdf(bytes, bytes_anexo, memory_pdf, True)
                                mt_CopiarPdf(bytes, bytes_anexo, memory_pdf)
                                Dim bytes_pdf() As Byte = memory_pdf.ToArray
                                respuesta = fc_SubirArchivo(22, codigo_sib, codigo_cup, memory_pdf, nombre_silabo)
                                memory_pdf.Close()
                            Else
                                Dim bytes() As Byte = memory.ToArray
                                memory.Close()
                                '' 20200331-ENevado ================================================
                                'mt_CopiarPdf(bytes, memory_pdf)
                                'Dim bytes_adenda() As Byte = memory_pdf.ToArray
                                'respuesta = fc_SubirArchivo(22, codigo_sib, codigo_cup, memory_pdf, nombre_silabo)
                                'memory_pdf.Close()
                                ''==================================================================
                                respuesta = fc_SubirArchivo(22, codigo_sib, codigo_cup, memory, nombre_silabo)
                                memory.Close()
                            End If

                            obj.Ejecutar("SilaboCurso_actualizar", codigo_sib, codigo_cup, 0, Date.Now.Date, 1, cod_user)
                            obj.TerminarTransaccion()
                            mt_ShowMessage("¡ Se ha publicado el silabo correctamente !", MessageType.Success)
                            'mt_CargarDatos(Me.cboSemestre.SelectedValue, Me.cboTipoEstudio.SelectedValue)
                            cboPlanEstudio_SelectedIndexChanged(Nothing, Nothing)
                        Else
                            Throw New Exception("Inicie Session")
                        End If
                        'Data.Add("Status", "OK")
                        'Data.Add("Message", respuesta)
                        'list.Add(Data)
                        'JSONresult = serializer.Serialize(list)
                        'Response.Write(JSONresult)
                    End If
                Case "Descargar"
                    Dim idArchivo As Long
                    idArchivo = CLng(Me.gvAsignatura.DataKeys(index).Values("IdArchivo"))
                    If idArchivo = 0 Then Throw New Exception("¡ Este silabo no se encuentra disponible !")
                    'Page.RegisterStartupScript("BusyBox", "<script>fc_ocultarBusy();</script>")
                    'Response.Write("<script>fc_OcultarBussy();</script>")
                    mt_DescargarArchivo(idArchivo, 22, "YAXVXFQACX")
                    'RegisterStartupScript("", "<script>fc_OcultarBussy();</script>")
                    'Page.ClientScript.RegisterStartupScript(Me.GetType, "script", "return fc_OcultarBussy()")
                    'btnListar_Click(Nothing, Nothing)
                    mt_ShowMessage("¡ Se ha descargado el silabo correctamente !", MessageType.Success)

                    'EPENA 23/08/2019{
                Case "Despublicar"
                    If fc_tieneCronograma("retirar") Then
                        If Not (IsDBNull(Session("perlogin"))) And Session("perlogin").ToString <> "" Then
                            codigo_cup = CLng(Me.gvAsignatura.DataKeys(index).Values("codigo_cup"))
                            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                            obj.AbrirConexion()
                            obj.Ejecutar("DEA_DespublicarSilabo", codigo_cup, cod_user)
                            obj.CerrarConexion()

                            mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), Me.cboPlanEstudio.SelectedValue)
                        Else
                            Throw New Exception("Inicie Sesión")
                        End If
                    End If
                    '}EPENA 23/08/2019
                Case "Observar"
                    Dim dtDis As New Data.DataTable
                    codigo_cac = CInt(Me.gvAsignatura.DataKeys(index).Values("codigo_Cac"))
                    codigo_cur = CInt(Me.gvAsignatura.DataKeys(index).Values("codigo_Cur"))
                    codigo_pes = CInt(Me.gvAsignatura.DataKeys(index).Values("codigo_Pes"))
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                    obj.AbrirConexion()
                    dtDis = obj.TraerDataTable("DiseñoAsignatura_Listar", "", -1, codigo_pes, codigo_cur, codigo_cac, -1)
                    If dtDis.Rows.Count > 0 Then
                        codigo_dis = dtDis.Rows(0).Item(0)
                        Session("gc_codigo_dis") = codigo_dis
                        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Editar" & "');</script>")
                    End If
                    obj.CerrarConexion()
            End Select
        Catch ex As Exception
            If e.CommandName = "Publicar" Then obj.AbortarTransaccion()
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
            'Data.Add("Status", "Fail")
            'Data.Add("Message", ex.Message & " - " & Session("perlogin").ToString)
            'list.Add(Data)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)
            'Response.Write(ex.Message)
        Finally
            'memory.Close()
            'memory_pdf.Close()
        End Try
    End Sub

    Protected Sub gvAsignatura_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAsignatura.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow AndAlso e.Row.RowIndex >= 0 Then
                Dim i As Integer = e.Row.RowIndex
                Dim ins_total, ins_asign, ins_pend, ses_total, ses_asign, ses_pend, fec_total, fec_asign, fec_pend As String

                ins_total = Me.gvAsignatura.DataKeys(i).Values("instr_total")
                ins_asign = Me.gvAsignatura.DataKeys(i).Values("instr_asign")
                ins_pend = Me.gvAsignatura.DataKeys(i).Values("instr_pend")

                ses_total = Me.gvAsignatura.DataKeys(i).Values("sesion_total")
                ses_asign = Me.gvAsignatura.DataKeys(i).Values("sesion_asign")
                ses_pend = Me.gvAsignatura.DataKeys(i).Values("sesion_pend")

                fec_total = Me.gvAsignatura.DataKeys(i).Values("fechas_total")
                fec_asign = Me.gvAsignatura.DataKeys(i).Values("fechas_asign")
                fec_pend = Me.gvAsignatura.DataKeys(i).Values("fechas_pend")

                e.Row.Cells(6).Text = ins_total & "     |     " & ins_asign & "     |     " & CStr(ins_pend.Length - ins_pend.Replace("|", "").Length)
                e.Row.Cells(6).ToolTip = ins_pend.Replace("|", vbCr).Replace("|", vbLf)
                e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center

                e.Row.Cells(7).Text = ses_total & "     |     " & ses_asign & "     |     " & CStr(ses_pend.Length - ses_pend.Replace("|", "").Length)
                e.Row.Cells(7).ToolTip = ses_pend.Replace("|", vbCr).Replace("|", vbLf)
                e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center

                e.Row.Cells(8).Text = fec_total & "     |     " & fec_asign & "     |     " & CStr(CInt(Len(fec_pend) / 11))
                e.Row.Cells(8).ToolTip = fec_pend.Replace("|", vbCr).Replace("|", vbLf)
                e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvAsignatura_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAsignatura.PageIndexChanging
        Me.gvAsignatura.DataSource = CType(Session("gc_dtCursoProg"), Data.DataTable)
        Me.gvAsignatura.DataBind()
        Me.gvAsignatura.PageIndex = e.NewPageIndex
        Me.gvAsignatura.DataBind()
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

    Private Sub mt_CargarTipoEstudio(ByVal codigo_cac As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_ConsultarTipoEstudio", "PS", codigo_cac)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboTipoEstudio, dt, "codigo_test", "descripcion_test")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCarreraProfesional(ByVal codigo_cac As Integer, ByVal codigo_test As Integer, ByVal user As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            'cod_user = 648
            If cod_ctf = 1 Or cod_ctf = 232 Then user = -1
            dt = obj.TraerDataTable("ConsultarCarreraProfesionalV3", "PS", codigo_cac, codigo_test, user, cod_ctf)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboCarPro, dt, "codigo_Cpf", "nombre_Cpf")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarPlanEstudio(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarPlanEstudio", "PS", codigo_cac, codigo_cpf)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboPlanEstudio, dt, "codigo_Pes", "descripcion_Pes")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos(ByVal codigo_cac As Integer, ByVal codigo_pes As Integer)
        ' 20191230 - ENevado -----------------------------------------------------------------------------\
        Dim dt As New System.Data.DataTable
        Try
            odCursoProgramado = New d_CursoProgramado : oeCursoProgramado = New e_CursoProgramado
            With oeCursoProgramado
                .TipoOperacion = "PS" : .codigo_pes = codigo_pes : .codigo_cac = codigo_cac : .codigo_ctf = cod_ctf
            End With
            dt = odCursoProgramado.fc_ListarCursoProgramado(oeCursoProgramado)
            Session("gc_dtCursoProg") = dt
            Me.gvAsignatura.DataSource = dt 'CType(Session("gc_dtCursoProg"), Data.DataTable)
            Me.gvAsignatura.DataBind()

            If dt IsNot Nothing Then
                Me.txtBuscar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
                Me.btnBuscar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
                If Me.gvAsignatura.Rows.Count > 0 Then fc_tieneCronograma("publicar o retirar", True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
        ' ------------------------------------------------------------------------------------------------/
    End Sub

    Private Sub mt_DescargarArchivo(ByVal IdArchivo As Long, ByVal idTabla As Integer, ByVal token As String)
        Try
            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Dictionary(Of String, String)
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim usuario As String = Session("perlogin")

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, idTabla, IdArchivo, token)
            obj.CerrarConexion()
            If tb.Rows.Count = 0 Then Throw New Exception("¡ Archivo no encontrado !")

            list.Add("IdArchivo", tb.Rows(0).Item("IdArchivo").ToString)
            list.Add("Usuario", usuario)
            list.Add("Token", token)

            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", usuario)
            Dim imagen As String = fc_ResultFile(result)

            If tb.Rows.Count > 0 Then
                Dim extencion As String
                extencion = tb.Rows(0).Item("Extencion")
                Select Case tb.Rows(0).Item("Extencion")
                    Case ".txt"
                        extencion = "text/plain"
                    Case ".doc"
                        extencion = "application/ms-word"
                    Case ".xls"
                        extencion = "application/vnd.ms-excel"
                    Case ".gif"
                        extencion = "image/gif"
                    Case ".jpg"
                    Case ".jpeg"
                    Case "jpeg"
                        extencion = "image/jpeg"
                    Case "png"
                        extencion = "image/png"
                    Case ".bmp"
                        extencion = "image/bmp"
                    Case ".wav"
                        extencion = "audio/wav"
                    Case ".ppt"
                        extencion = "application/mspowerpoint"
                    Case ".dwg"
                        extencion = "image/vnd.dwg"
                    Case ".pdf"
                        extencion = "application/pdf"
                    Case Else
                        extencion = "application/octet-stream"
                End Select

                Dim bytes As Byte() = Convert.FromBase64String(imagen)
                Response.Clear()
                Response.Buffer = False
                Response.Charset = ""
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = extencion
                Response.AddHeader("content-disposition", "attachment;filename=" & tb.Rows(0).Item("NombreArchivo").ToString.Replace(",", ""))
                Response.AppendHeader("Content-Length", bytes.Length.ToString())
                Response.BinaryWrite(bytes)
                Response.End()
            End If
            'Response.Write(envelope)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub mt_CopiarPdf(ByVal bytes_pdf() As Byte, ByVal bytes_anexo() As Byte, ByVal memory As System.IO.Stream)
    '    Dim pdfFile_anexo As iTextSharp.text.pdf.PdfReader
    '    Dim pdfFile_silabo As iTextSharp.text.pdf.PdfReader
    '    Dim doc As iTextSharp.text.Document
    '    Dim pCopy As iTextSharp.text.pdf.PdfWriter
    '    'Dim msOutput As MemoryStream = New MemoryStream()
    '    pdfFile_anexo = New iTextSharp.text.pdf.PdfReader(bytes_anexo)
    '    pdfFile_silabo = New iTextSharp.text.pdf.PdfReader(bytes_pdf)
    '    doc = New iTextSharp.text.Document()
    '    pCopy = New iTextSharp.text.pdf.PdfSmartCopy(doc, memory)
    '    doc.Open()

    '    'mt_AddWaterMark(pCopy, "BORRADOR")

    '    'For k As Integer = 0 To files.Count - 1
    '    'pdfFile = New iTextSharp.text.pdf.PdfReader(Server.MapPath(".") & "/ManualTutoria.pdf")

    '    For j As Integer = 1 To pdfFile_silabo.NumberOfPages
    '        CType(pCopy, iTextSharp.text.pdf.PdfSmartCopy).AddPage(pCopy.GetImportedPage(pdfFile_silabo, j))
    '    Next

    '    pCopy.FreeReader(pdfFile_silabo)
    '    pdfFile_silabo.Close()

    '    For i As Integer = 1 To pdfFile_anexo.NumberOfPages
    '        CType(pCopy, iTextSharp.text.pdf.PdfSmartCopy).AddPage(pCopy.GetImportedPage(pdfFile_anexo, i))
    '    Next

    '    pCopy.FreeReader(pdfFile_anexo)
    '    'Next

    '    pdfFile_anexo.Close()

    '    pCopy.Close()
    '    doc.Close()
    'End Sub

    Private Sub mt_CopiarPdf(ByVal bytes_pdf() As Byte, ByVal bytes_anexo() As Byte, ByVal memory As System.IO.Stream, Optional ByVal lb_adenda As Boolean = False)
        Dim pdfFile_anexo As iTextSharp.text.pdf.PdfReader
        Dim pdfFile_silabo As iTextSharp.text.pdf.PdfReader
        Dim pdfFile_adenda As iTextSharp.text.pdf.PdfReader
        Dim doc As iTextSharp.text.Document
        Dim pCopy As iTextSharp.text.pdf.PdfWriter
        pdfFile_anexo = New iTextSharp.text.pdf.PdfReader(bytes_anexo)
        pdfFile_silabo = New iTextSharp.text.pdf.PdfReader(bytes_pdf)
        If lb_adenda Then pdfFile_adenda = New iTextSharp.text.pdf.PdfReader(Server.MapPath(".") & "/Adenda.pdf")
        doc = New iTextSharp.text.Document()
        pCopy = New iTextSharp.text.pdf.PdfSmartCopy(doc, memory)
        doc.Open()

        For j As Integer = 1 To pdfFile_silabo.NumberOfPages
            CType(pCopy, iTextSharp.text.pdf.PdfSmartCopy).AddPage(pCopy.GetImportedPage(pdfFile_silabo, j))
        Next

        pCopy.FreeReader(pdfFile_silabo)
        pdfFile_silabo.Close()

        If lb_adenda Then
            For g As Integer = 1 To pdfFile_adenda.NumberOfPages
                CType(pCopy, iTextSharp.text.pdf.PdfSmartCopy).AddPage(pCopy.GetImportedPage(pdfFile_adenda, g))
            Next
            pCopy.FreeReader(pdfFile_adenda)
            pdfFile_adenda.Close()
        End If

        For i As Integer = 1 To pdfFile_anexo.NumberOfPages
            CType(pCopy, iTextSharp.text.pdf.PdfSmartCopy).AddPage(pCopy.GetImportedPage(pdfFile_anexo, i))
        Next

        pCopy.FreeReader(pdfFile_anexo)

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

#End Region

#Region "Funciones"

    Function fc_SubirArchivo(ByVal idTabla As String, ByVal codigo As String, ByVal nro_operacion As String, ByVal file As System.IO.MemoryStream, ByVal name As String) As String
        Dim list As New Dictionary(Of String, String)
        Try
            Dim _TablaId As String = idTabla
            Dim _TransaccionId As String = codigo
            Dim _Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim _Usuario As String = Session("perlogin").ToString

            Dim binData As Byte() = file.ToArray
            Dim base64 As Object = System.Convert.ToBase64String(binData)
            Dim _Nombre As String = name.Replace("&", "_").Replace("'", "_").Replace("*", "_")

            Dim wsCloud As New ClsArchivosCompartidos

            list.Add("Fecha", _Fecha)
            list.Add("Extencion", ".pdf")
            list.Add("Nombre", _Nombre)
            list.Add("TransaccionId", _TransaccionId)
            list.Add("TablaId", _TablaId)
            list.Add("NroOperacion", nro_operacion)
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
                obj.CerrarConexion()
                If tb.Rows.Count = 0 Then Throw New Exception("¡ Archivo no encontrado !")
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

    Private Function fc_tieneCronograma(ByVal accion As String, Optional ByVal mostrarEnEtiqueta As Boolean = False) As Boolean
        Dim obj As New ClsConectarDatos
        Dim objRes As Object()
        Dim codigo_cac As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        '[Inicio] Condición por Luis Q.T| 17DIC2019
        codigo_cac = IIf(String.IsNullOrEmpty(Me.cboSemestre.SelectedValue), "0", Me.cboSemestre.SelectedValue)

        obj.AbrirConexion()
        objRes = obj.Ejecutar("DEA_validarAccionEnCronograma", "PS", codigo_cac, "2")
        obj.CerrarConexion()

        If objRes Is Nothing Then
            If Not mostrarEnEtiqueta Then Call mt_ShowMessage("El plazo para " & accion & " el sílabo ha caducado. Consulte con Dirección Académica", MessageType.Info)
            lblMensaje.Text = "El plazo para " & accion & " el sílabo ha caducado. Consulte con Dirección Académica"
            Return False
        Else
            If objRes(0).ToString.Equals("0") Then
                If Not mostrarEnEtiqueta Then Call mt_ShowMessage("El plazo para " & accion & " el sílabo ha caducado. Consulte con Dirección Académica", MessageType.Info)
                lblMensaje.Text = "El plazo para " & accion & " el sílabo ha caducado. Consulte con Dirección Académica"
                Return False
            End If
        End If
        '[Fin] Condición por Luis Q.T| 17DIC2019

        lblMensaje.Text = ""
        Return True
    End Function

#End Region

End Class

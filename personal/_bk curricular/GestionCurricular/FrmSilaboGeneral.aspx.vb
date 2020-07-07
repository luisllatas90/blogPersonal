﻿Imports System.Collections.Generic
Imports System.IO

Partial Class GestionCurricular_FrmSilaboGeneral
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer '= 523
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

            If IsPostBack = False Then
                Dim dtFecha As Data.DataTable = New Data.DataTable("dtFecha")
                dtFecha.Columns.Add("fechas")
                dtFecha.Columns.Add("evento")
                dtFecha.Columns.Add("es_feriado")
                dtFecha.Columns.Add("tipos")
                ViewState("dtFecha") = dtFecha

                Session("codigo_fec") = Nothing
                Call mt_CargarSemestre()
                Call mt_CargarCarreraProf()

                If Not String.IsNullOrEmpty(Session("codigo_pes")) Then
                    If Me.ddlSemestre.Items.Count > 0 Then Me.ddlSemestre.SelectedValue = Session("codigo_cac") : Call mt_CargarCarreraProf()
                    If Me.ddlCarreraProf.Items.Count > 0 Then Me.ddlCarreraProf.SelectedValue = Session("codigo_cpf") : Call mt_CargarPlanEstudio(Session("codigo_cac"), Session("codigo_cpf"))
                    If Me.ddlPlanEstudio.Items.Count > 0 Then Me.ddlPlanEstudio.SelectedValue = Session("codigo_pes") : Call mt_CargarDatos()

                    Session.Remove("codigo_cac")
                    Session.Remove("codigo_pes")
                    Session.Remove("codigo_cpf")
                    Session.Remove("curso")
                End If
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemestre.SelectedIndexChanged
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlCarreraProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarreraProf.SelectedIndexChanged
        Try
            Call mt_CargarPlanEstudio(IIf(Me.ddlSemestre.SelectedValue = -1, 0, Me.ddlSemestre.SelectedValue), Me.ddlCarreraProf.SelectedValue)
            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlPlanEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlanEstudio.SelectedIndexChanged
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Dim flag As Boolean = True

        If ddlCarreraProf.SelectedValue = -1 Then
            Page.RegisterStartupScript("alerta", "<script>alert('Seleccione la Carrera Profesional');</script>")
            Me.ddlCarreraProf.Focus()
            flag = False
        End If

        If ddlSemestre.SelectedValue = -1 Then
            Page.RegisterStartupScript("alerta", "<script>alert('Seleccione el Semestre');</script>")
            Me.ddlSemestre.Focus()
            flag = False
        End If

        If flag Then
            Call mt_CargarDatos()
        Else
            Me.hdCodigoDis.Value = ""
            Me.hdCodigoCur.Value = ""
            Me.hdCodigoCup.Value = ""

            Me.gvResultados.DataSource = Nothing
            Me.gvResultados.DataBind()
        End If
    End Sub

    Protected Sub btnGuardarActa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarActa.Click
        'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'Dim JSONresult As String = ""
        'Dim Data As New Dictionary(Of String, Object)()
        'Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim idtabla, codigo_cup As Integer
        Dim dt As New Data.DataTable
        Dim respuesta As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            If Not (IsDBNull(Session("perlogin"))) And Session("perlogin").ToString <> "" Then
                codigo_cup = CInt(Me.hdCodigoCup.Value)
                If codigo_cup = 0 Then Throw New Exception("¡ Seleccione un Curso !")
                idtabla = 20 ' Desarrollo
                'idtabla = 12 ' Produccion
                If Me.fuArchivo.HasFile Then
                    Dim Archivos As HttpFileCollection = Request.Files
                    For i As Integer = 0 To Archivos.Count - 1
                        'Data.Add("Step", idtabla & "-" & codigo_dec & "-" & Archivos(i).FileName)
                        Call fc_SubirArchivo(idtabla, codigo_cup, Archivos(i))
                    Next
                End If
                obj.AbrirConexion()
                obj.Ejecutar("CursoProgramado_SubirActa", codigo_cup)
                obj.CerrarConexion()
            Else
                Throw New Exception("Inicie Sesión")
            End If

            Call mt_ShowMessage("¡ Se subió el acta correctamente !", MessageType.Success)

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

    Protected Sub gvResultados_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvResultados.RowCommand
        Try
            Dim index As Integer = CInt(e.CommandArgument)
            If index >= 0 Then
                Dim codigo_cac, codigo_dis, codigo_cur, codigo_cup, codigo_pes, codigo_cpf, nombre_cur As String
                codigo_cur = Me.gvResultados.DataKeys(index).Values(0).ToString()
                codigo_dis = Me.gvResultados.DataKeys(index).Values(2).ToString()
                codigo_cup = Me.gvResultados.DataKeys(index).Values(4).ToString()
                nombre_cur = Me.gvResultados.DataKeys(index).Values(6).ToString()
                codigo_pes = Me.gvResultados.DataKeys(index).Values(7).ToString()
                codigo_cac = Me.gvResultados.DataKeys(index).Values(8).ToString()
                codigo_cpf = Me.gvResultados.DataKeys(index).Values(9).ToString()

                Me.hdCodigoDis.Value = codigo_dis
                Me.hdCodigoCur.Value = codigo_cur
                Me.hdCodigoCup.Value = codigo_cup

                Session("codigo_cac") = codigo_cac
                Session("codigo_cup") = codigo_cup

                If e.CommandName.Equals("RegistrarFechas") Then
                    Me.divAlertModal.Visible = False
                    Me.lblMensaje.InnerText = ""
                    updMensaje.Update()

                    Page.RegisterStartupScript("Pop", "<script>openModal('registrar','');</script>")
                ElseIf e.CommandName.Equals("DescargarSilabo") Then
                    Call mt_DescargarArchivo(codigo_cup)
                ElseIf e.CommandName.Equals("BajarActa") Then
                    Dim memory As New System.IO.MemoryStream
                    Dim nombreArchivo As String = "Acta " & Me.gvResultados.DataKeys(index).Values(5).ToString() & " " & Me.gvResultados.DataKeys(index).Values(1).ToString()
                    Call mt_GenerarActa(codigo_cup, CStr(Me.ddlSemestre.SelectedValue), Server.MapPath(".") & "/logo_usat.png", memory, True)
                    Dim bytes() As Byte = memory.ToArray
                    memory.Close()
                    Response.Clear()
                    Response.Buffer = False
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-disposition", "attachment;filename=" & nombreArchivo & ".pdf")
                    Response.AddHeader("content-length", bytes.Length.ToString())
                    Response.BinaryWrite(bytes)
                    Response.End()

                ElseIf e.CommandName.Equals("SubirActa") Then
                    Page.RegisterStartupScript("Pop", "<script>openModal('','subirActa');</script>")
                ElseIf e.CommandName.Equals("Instrumentos") Then
                    Session("codigo_dis") = codigo_dis
                    Session("codigo_cur") = codigo_cur
                    Session("codigo_pes") = codigo_pes
                    Session("codigo_cpf") = codigo_cpf
                    Session("curso") = nombre_cur
                    'Response.Write("<script language='javascript'> window.open('FrmAdicionarInstrumentosContenido.aspx', 'window', 'height=600,width=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>")
                    Page.RegisterStartupScript("Pop", "<script>closeModal();</script>")
                    Response.Redirect("~/GestionCurricular/FrmAdicionarInstrumentosContenido.aspx")

                ElseIf e.CommandName.Equals("SubirActa") Then
                    Session("codigo_cup") = codigo_cup
                End If

                Call mt_CargarDatosSesion(codigo_dis, codigo_cur, codigo_cup)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvSesion_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvSesion.RowCancelingEdit
        gvSesion.EditIndex = -1
        Call mt_CargarDatosSesion(Me.hdCodigoDis.value, Me.hdCodigoCur.value, Me.hdCodigoCup.value)
    End Sub

    Protected Sub gvSesion_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvSesion.RowEditing
        gvSesion.EditIndex = e.NewEditIndex
        Session("codigo_fec") = gvSesion.DataKeys(gvSesion.EditIndex).Values(2).ToString()
        Call mt_CargarDatosSesion(Me.hdCodigoDis.value, Me.hdCodigoCur.value, Me.hdCodigoCup.value)
    End Sub

    Protected Sub gvSesion_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvSesion.RowUpdating
        If gvSesion.EditIndex > -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

            Try
                Dim ddl As DropDownList
                ddl = CType(gvSesion.Rows(e.RowIndex).FindControl("ddlFecha"), DropDownList)
                If ddl.SelectedValue <> "" Then
                    Dim flag As Boolean = False
                    Dim motivo As String = ""
                    Dim tipo As String = ""
                    Dim dt As Data.DataTable = TryCast(ViewState("dtFecha"), Data.DataTable)

                    If dt IsNot Nothing Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(i).Item(2) = True And dt.Rows(i).Item(0).ToString.Contains(ddl.SelectedItem.Value.ToString()) Then
                                motivo = dt.Rows(i).Item(1).ToString
                                tipo = dt.Rows(i).Item(3).ToString
                                tipo = IIf(tipo.Equals("FI"), "Feriado Institucional", "Feriado Calendario")
                                
                                flag = True
                                Exit For
                            End If
                        Next
                        dt.Dispose()
                    End If

                    If flag Then
                        Call mt_ShowMessage("La fecha " & ddl.SelectedItem.Text.ToString().Substring(0, 15) & ". No puede ser seleccionada porque es " & tipo, MessageType.Info, True)
                        Return
                    End If

                    Dim codigo_ses As String = gvSesion.DataKeys(e.RowIndex).Values(0).ToString()
                    Dim codigo_fec As String = gvSesion.DataKeys(e.RowIndex).Values(3).ToString()
                    codigo_fec = IIf(String.IsNullOrEmpty(codigo_fec), "0", codigo_fec)

                    obj.AbrirConexion()
                    obj.Ejecutar("COM_ActualizarFechaSesion", codigo_fec, codigo_ses, Me.hdCodigoCup.Value, ddl.SelectedValue, ddl.SelectedItem.ToString, cod_user)
                    obj.CerrarConexion()

                    gvSesion.EditIndex = -1
                    Call mt_CargarDatosSesion(Me.hdCodigoDis.Value, Me.hdCodigoCur.Value, Me.hdCodigoCup.Value)
                Else
                    Call mt_ShowMessage("Seleccione una fecha válida", MessageType.Info, True)
                End If
            Catch ex As Exception
                Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Info, True)
            End Try
        End If
    End Sub

    Protected Sub gvSesion_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvSesion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddl As DropDownList = CType(e.Row.FindControl("ddlFecha"), DropDownList)

            If Not ddl Is Nothing Then
                ddl.DataSource = fc_CargarHorario()
                ddl.DataValueField = "fechas"
                ddl.DataTextField = "descripcion"
                ddl.DataBind()

                'Agregar fila en blanco
                ddl.Items.Insert(0, New ListItem("[-- Seleccione una fecha --]", ""))
                ddl.SelectedValue = gvSesion.DataKeys(e.Row.RowIndex).Values(2).ToString()

                'Seleccionar por defecto el id actual
                'Dim fecha As String = CType(e.Row.FindControl("lblFecha"), Label).Text
                'ddl.Items.FindByValue(fecha).Selected = True
                'dt.Dispose()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim ddl As DropDownList = CType(e.Row.FindControl("ddlNewFecha"), DropDownList)
            If ddl IsNot Nothing Then
                ddl.DataSource = fc_CargarHorario()
                ddl.DataValueField = "fechas"
                ddl.DataTextField = "descripcion"
                ddl.DataBind()

                'Agregar fila en blanco
                ddl.Items.Insert(0, New ListItem("[-- Seleccione una fecha --]", ""))
            End If
        End If
    End Sub

    Protected Sub OnDeleteFecha(ByVal sender As Object, ByVal e As EventArgs)
        Dim dt As Data.DataTable = TryCast(ViewState("dtDesarrollo"), Data.DataTable)
        Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
        Dim codigo_ses As String = gvSesion.DataKeys(row.RowIndex).Item("codigo_ses").ToString
        Dim codigo_fec As String = gvSesion.DataKeys(row.RowIndex).Item("codigo_fec").ToString

        If Not String.IsNullOrEmpty(codigo_ses) And Not String.IsNullOrEmpty(codigo_fec) Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

            obj.AbrirConexion()
            obj.Ejecutar("COM_EliminarFechaSesion", codigo_fec, codigo_ses)
            obj.CerrarConexion()

            gvSesion.EditIndex = -1
            Call mt_CargarDatosSesion(Me.hdCodigoDis.Value, Me.hdCodigoCur.Value, Me.hdCodigoCup.Value)
        End If
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType, Optional ByVal modal As Boolean = False)
        If modal Then
            Me.divAlertModal.Visible = True
            Me.lblMensaje.InnerText = Message
            Me.divAlertModal.Focus()
            Me.lblMensaje.Focus()
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.divAlertModal)
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.lblMensaje)
            updMensaje.Update()
        Else
            Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
        End If
    End Sub

    Private Sub mt_CargarSemestre()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCicloAcademico", "DA", "")
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlSemestre, dt, "codigo_Cac", "descripcion_Cac")
            dt.dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCarreraProf()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            'cod_user = 359 '359
            If cod_user = 684 Then
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "CF", "2", cod_user)
            Else
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "UC", "2", cod_user)
            End If
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlCarreraProf, dt, "codigo_Cpf", "nombre_Cpf")
            dt.dispose()
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
            mt_CargarCombo(Me.ddlPlanEstudio, dt, "codigo_Pes", "descripcion_Pes")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

    Private Sub mt_CargarDatos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim semestre, carrera, plan As String
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            semestre = IIf(String.IsNullOrEmpty(Me.ddlSemestre.SelectedValue), "0", Me.ddlSemestre.SelectedValue)
            carrera = IIf(String.IsNullOrEmpty(Me.ddlCarreraProf.SelectedValue), "0", Me.ddlCarreraProf.SelectedValue)
            plan = IIf(String.IsNullOrEmpty(Me.ddlPlanEstudio.SelectedValue), "0", Me.ddlPlanEstudio.SelectedValue)
            'cod_user = 359 '359
            obj.AbrirConexion()
            dt = obj.TraerDataTable("CursoProgramado_Listar_V2", "PS", -1, plan, -1, semestre, cod_user, ddlEstado.SelectedValue)
            obj.CerrarConexion()
            Me.gvResultados.DataSource = dt
            Me.gvResultados.DataBind()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_CargarDatosSesion(ByVal codigo_dis As String, ByVal codigo_cur As String, ByVal codigo_cup As String)
        Dim dtSesion As Data.DataTable = fc_GetSesion(codigo_dis, codigo_cur, codigo_cup)
        Try
            Me.gvSesion.DataSource = dtSesion
            Me.gvSesion.DataBind()

            If Me.gvSesion.Rows.Count > 0 Then
                Call mt_AgruparFilas(Me.gvSesion.Rows, 0, 2)
            End If

            Me.udpSesion.Update()

            Me.divAlertModal.Visible = False
            Me.lblMensaje.InnerText = ""
            updMensaje.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString(), ex)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para generar silabo en pdf
    ''' </summary>
    ''' <param name="codigo_cup"></param>
    ''' <param name="codigo_cac"></param>
    ''' <param name="sourceIcon"></param>
    ''' <param name="memory"></param>
    ''' <param name="vista"></param>
    ''' <remarks></remarks>
    Public Sub mt_GenerarActa(ByVal codigo_cup As Integer, ByVal codigo_cac As String, ByVal sourceIcon As String, ByVal memory As System.IO.Stream, Optional ByVal vista As Boolean = False)
        Dim obj As New ClsConectarDatos
        Dim dtCab As New Data.DataTable
        Dim dtDet As New Data.DataTable
        Dim TEst As String
        Dim imageURL As String = Server.MapPath(".") & "/logo_usat.png"
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dtCab = obj.TraerDataTable("COM_EstructuraActaSilabo", codigo_cup, codigo_cac, "C")
            dtDet = obj.TraerDataTable("COM_EstructuraActaSilabo", codigo_cup, codigo_cac, "D")
            TEst = dtCab.Rows(0).Item("codigo_test").ToString()

            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)

            pdfDoc.Open()

            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(6)
            pdfTable.SetWidths(New Single() {5.0F, 10.0F, 5.0F, 30.0F, 30.0F, 20.0F})
            pdfTable.WidthPercentage = 100.0F
            pdfTable.DefaultCell.Border = 0
            pdfTable.DefaultCell.Padding = 0

            ' 0: Cabecera del Acta ----------------------------------------------------------------------

            Dim srcIcon As String = sourceIcon
            Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
            usatIcon.ScalePercent(30.0F)
            usatIcon.Alignment = iTextSharp.text.Element.ALIGN_LEFT

            Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
            cellIcon.HorizontalAlignment = 1
            cellIcon.VerticalAlignment = 5
            cellIcon.Border = 15
            cellIcon.Colspan = 2

            pdfTable.AddCell(cellIcon)
            pdfTable.AddCell(fc_CeldaTexto("ACTA DE EXPOSICIÓN", 14.0F, 1, 15, 3, 1, 1, 5))
            pdfTable.AddCell(fc_CeldaTexto("SISTEMA DE GESTIÓN DE LA CALIDAD" & Chr(13) & "CÓDIGO: USAT-EA-R-06" & Chr(13) & "VERSIÓN", 6.0F, 1, 15, 1, 1, 1, 5))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea

            pdfTable.AddCell(fc_CeldaTexto("DOCUMENTO EXPUESTO", 7.0F, 1, 15, 2, 2, 1, 5))
            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 15, 1, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("Protocolo de Seguridad del ", 7.0F, 0, 15, 3, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("ambiente").ToString, 7.0F, 0, 15, 1, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("Sílabo", 7.0F, 0, 15, 1, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("FECHA DE EXPOSICIÓN", 7.0F, 1, 15, 1, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("fecha").ToString, 7.0F, 0, 15, 1, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea

            ' 1: Datos Generales ------------------------------------------------------------------------

            pdfTable.AddCell(fc_CeldaTexto("I. DATOS GENERALES", 7.0F, 1, 15, 6, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("FACULTAD", 7.0F, 1, 15, 2, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("nombre_Fac").ToString, 7.0F, 0, 15, 4, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("PROGRAMA DE ESTUDIOS", 7.0F, 1, 15, 2, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("descripcion_Pes").ToString, 7.0F, 0, 15, 4, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("SEMESTRE ACADÉMICO", 7.0F, 1, 15, 2, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("cicloAcadInicio_Pes").ToString, 7.0F, 0, 15, 4, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("TIPO DE SERVICIO", 7.0F, 1, 15, 2, 5, 1, 5))
            pdfTable.AddCell(fc_CeldaTexto(IIf(TEst.Equals("2"), "X", ""), 7.0F, 0, 15, 1, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("Pregrado", 7.0F, 0, 15, 3, 1, 0))
            pdfTable.AddCell(fc_CeldaTexto(IIf(TEst.Equals("10"), "X", ""), 7.0F, 0, 15, 1, 1, 0))
            pdfTable.AddCell(fc_CeldaTexto("Pregrado: Programa para gente que trabaja (GO)", 7.0F, 0, 15, 3, 1, 0))
            pdfTable.AddCell(fc_CeldaTexto(IIf(TEst.Equals("8"), "X", ""), 7.0F, 0, 15, 1, 1, 0))
            pdfTable.AddCell(fc_CeldaTexto("Segunda Especialidad", 7.0F, 0, 15, 3, 1, 0))
            pdfTable.AddCell(fc_CeldaTexto(IIf(TEst.Equals("5"), "X", ""), 7.0F, 0, 15, 1, 1, 0))
            pdfTable.AddCell(fc_CeldaTexto("Postgrado", 7.0F, 0, 15, 3, 1, 0))
            pdfTable.AddCell(fc_CeldaTexto(IIf(TEst.Equals("6"), "X", ""), 7.0F, 0, 15, 1, 1, 0))
            pdfTable.AddCell(fc_CeldaTexto("Educación continua", 7.0F, 0, 15, 3, 1, 0))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea

            ' 1: Datos de Asignatura --------------------------------------------------------------------

            pdfTable.AddCell(fc_CeldaTexto("II. DATOS DE ASIGNATURA", 7.0F, 1, 15, 6, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("ASIGNATURA", 7.0F, 1, 15, 2, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("nombre_Cur").ToString, 7.0F, 0, 15, 4, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("CICLO DE LA ASIGNATURA", 7.0F, 1, 15, 2, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("ciclo_Cur").ToString, 7.0F, 0, 15, 4, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("GRUPO HORARIO", 7.0F, 1, 15, 2, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("grupoHor_Cup").ToString, 7.0F, 0, 15, 4, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("DOCENTE", 7.0F, 1, 15, 2, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("docente").ToString, 7.0F, 0, 15, 4, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea

            ' 2: Listado de estudiantes -----------------------------------------------------------------

            pdfTable.AddCell(fc_CeldaTexto("III. LISTADO DE ESTUDIANTES", 7.0F, 1, 15, 6, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("N°", 7.0F, 1, 15, 1, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("APELLIDOS Y NOMBRES", 7.0F, 1, 15, 3, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("CÓDIGO", 7.0F, 1, 15, 1, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("FIRMA", 7.0F, 1, 15, 1, 1, 1))

            For index As Integer = 0 To dtDet.Rows.Count - 1
                pdfTable.AddCell(fc_CeldaTexto((index + 1).ToString, 7.0F, 0, 15, 1, 1, 0))
                pdfTable.AddCell(fc_CeldaTexto(dtDet.Rows(index).Item("estudiante").ToString, 7.0F, 0, 15, 3, 1, 0))
                pdfTable.AddCell(fc_CeldaTexto(dtDet.Rows(index).Item("codigoUniver_Alu").ToString, 7.0F, 0, 15, 1, 1, 1))
                pdfTable.AddCell(fc_CeldaTexto("", 7.0F, 0, 15, 1, 1, 0))
            Next

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea

            ' 3: Firma ----------------------------------------------------------------------------------

            pdfTable.AddCell(fc_CeldaTexto("______________________________________", 7.0F, 1, 0, 4, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("______________________________________", 7.0F, 1, 0, 2, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("FIRMA DOCENTE", 7.0F, 1, 0, 4, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("FIRMA COORDINADOR LABORATORIO", 7.0F, 1, 0, 2, 1, 1))

            pdfDoc.Add(pdfTable)

            pdfDoc.Close()
            dtCab.Dispose()
            dtDet.Dispose()
        Catch ex As Exception
            Throw ex
        Finally
            obj.CerrarConexion()
        End Try
    End Sub

    Private Sub mt_DescargarArchivo(ByVal codigo_cup As Long)
        Try
            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Dictionary(Of String, String)
            Dim obj As New ClsConectarDatos
            Dim tb, dt As New Data.DataTable
            Dim cif As New ClsCRM
            Dim usuario As String = Session("perlogin")
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("SilaboCurso_listar", -1, codigo_cup, 1)
            If dt.Rows.Count = 0 Then Throw New Exception("¡ No existe silabo para este curso !")
            'tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, 20, dt.Rows(0).Item("codigo_sil"), "YAXVXFQACX") ' Desarrollo
            tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, 22, dt.Rows(0).Item("codigo_sil"), "YAXVXFQACX") ' Produccion
            obj.CerrarConexion()
            If tb.Rows.Count = 0 Then Throw New Exception("¡ No se encontró el archivo !")
            list.Add("IdArchivo", tb.Rows(0).Item("IdArchivo").ToString)
            list.Add("Usuario", usuario)
            list.Add("Token", "YAXVXFQACX") ' Desarrollo
            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", usuario)
            Dim imagen As String = fc_ResultFile(result)
            Dim bytes As Byte() = Convert.FromBase64String(imagen)
            Response.Clear()
            Response.Buffer = False
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment;filename=" + tb.Rows(0).Item("NombreArchivo").ToString)
            Response.AppendHeader("Content-Length", bytes.Length.ToString())
            Response.BinaryWrite(bytes)
            Response.End()
            'Response.Write(envelope)
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
                    Call mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
                End If
                count = 1
                lst.Clear()
                ctrl = gridViewRows(i).Cells(startIndex)
                lst.Add(gridViewRows(i))
            End If
        Next
        If count > 1 Then
            ctrl.RowSpan = count
            Call mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
        End If
        count = 1
        lst.Clear()
    End Sub

#End Region

#Region "Funciones"

    Private Function fc_CargarHorario() As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable("data")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            Dim dia_fec As String = Session("codigo_fec")
            dia_fec = IIf(String.IsNullOrEmpty(dia_fec), "", dia_fec)

            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarHorarioDocente", Session("codigo_cup"), Session("codigo_cac"), dia_fec)

            Dim columns As String() = {"fechas", "evento", "es_feriado", "tipo"}
            Dim dtFecha As Data.DataTable = New Data.DataView(dt).ToTable(False, columns)

            ViewState("dtFecha") = dtFecha

            obj.CerrarConexion()
        Catch ex As Exception
            Throw ex
        End Try

        Return dt
    End Function

    Private Function fc_GetSesion(ByVal codigo_dis As String, ByVal codigo_cur As String, ByVal codigo_cup As String) As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        obj.AbrirConexion()
        codigo_dis = IIf(String.IsNullOrEmpty(codigo_dis), "-1", codigo_dis)
        codigo_cur = IIf(String.IsNullOrEmpty(codigo_cur), "-1", codigo_cur)
        codigo_cup = IIf(String.IsNullOrEmpty(codigo_cup), "-1", codigo_cup)

        dt = obj.TraerDataTable("COM_ListarFechaSesion", codigo_dis, codigo_cur, codigo_cup)
        obj.CerrarConexion()

        Return dt
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

            Dim br As New BinaryReader(_Archivo.InputStream)
            Dim binData As Byte() = br.ReadBytes(_Archivo.InputStream.Length)
            Dim base64 As Object = System.Convert.ToBase64String(binData)
            Dim _Nombre As String = Path.GetFileName(_Archivo.FileName.Replace("&", "_").Replace("'", "_").Replace("*", "_"))

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

    Private Function fc_ResultFile(ByVal cadXml As String) As String
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

#End Region

#Region "Funciones2"

    ''' <summary>
    ''' Función para crear una celda tipo texto con más atributos
    ''' </summary>
    ''' <param name="_text">Contenido de la Celda</param>
    ''' <param name="_size">Tamaño de letra del contenido de la celda</param>
    ''' <param name="_style">Estilo del Contenido de la Celda. 0: NORMAL, 1: BOLD, 2: ITALIC, 3: BOLDITALIC, 4: UNDERLINE</param>
    ''' <param name="_border">Borde de la Celda. 0: NO_BORDER, 1: TOP_BORDER , 2: BOTTON_BORDER, 4: LEFT_BORDER, 8: RIGHT_BORDER, 15: FULL_BORDER </param>
    ''' <param name="_colspan"></param>
    ''' <param name="_rowspan"></param>
    ''' <param name="_haligment">Alineación horizontal del contenido de la celda. 0: LEFT, 1: CENTER, 2: RIGHT, 4: TOP, 5: MIDDLE</param>
    ''' <param name="_valigment">Alineación vertical del contenido de la celda. 0: LEFT, 1: CENTER, 2: RIGHT, 4: TOP, 5: MIDDLE</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fc_CeldaTexto(ByVal _text As String, ByVal _size As Single, ByVal _style As Integer, ByVal _border As Integer, _
                                 ByVal _colspan As Integer, ByVal _rowspan As Integer, ByVal _haligment As Integer, Optional ByVal _valigment As Integer = 1) As iTextSharp.text.pdf.PdfPCell
        Dim celdaITC As iTextSharp.text.pdf.PdfPCell
        Dim fontITC As iTextSharp.text.Font
        fontITC = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style)
        celdaITC = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(_text, fontITC))
        celdaITC.Border = _border
        celdaITC.Colspan = _colspan
        celdaITC.Rowspan = _rowspan
        celdaITC.HorizontalAlignment = _haligment
        celdaITC.VerticalAlignment = _valigment

        If Not String.IsNullOrEmpty(_text) Then
            celdaITC.Padding = 6
        End If

        Return celdaITC
    End Function

    ''' <summary>
    ''' Función para crear una celta tipo texto con más atributos
    ''' </summary>
    ''' <param name="_text">Contenido de la Celda</param>
    ''' <param name="_size">Tamano de letra del contenido de la celda</param>
    ''' <param name="_style">Estilo del Contenido de la Celda. 0: NORMAL, 1: BOLD, 2: ITALIC, 3: BOLDITALIC, 4: UNDERLINE</param>
    ''' <param name="_border">Borde de la Celda. 0: NO_BORDER, 1: TOP_BORDER , 2: BOTTON_BORDER, 4: LEFT_BORDER, 8: RIGHT_BORDER, 15: FULL_BORDER </param>
    ''' <param name="_colspan"></param>
    ''' <param name="_rowspan"></param>
    ''' <param name="_haligment">Alineacion horizontal del contenido de la celda. 0: LEFT, 1: CENTER, 2: RIGHT, 4: TOP, 5: MIDDLE</param>
    ''' <param name="_fontcolor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fc_CeldaTexto(ByVal _text As String, ByVal _size As Single, ByVal _style As Integer, ByVal _border As Integer, _
                                 ByVal _colspan As Integer, ByVal _rowspan As Integer, ByVal _haligment As Integer, _
                                 ByVal _fontcolor As iTextSharp.text.BaseColor) As iTextSharp.text.pdf.PdfPCell
        Dim celdaITC As iTextSharp.text.pdf.PdfPCell
        Dim fontITC As iTextSharp.text.Font
        fontITC = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style, _fontcolor)
        celdaITC = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(_text, fontITC))
        celdaITC.Border = _border
        celdaITC.Colspan = _colspan
        celdaITC.Rowspan = _rowspan
        celdaITC.HorizontalAlignment = _haligment
        celdaITC.Padding = 6
        Return celdaITC
    End Function

    ''' <summary>
    ''' Función para crear una celta tipo texto
    ''' </summary>
    ''' <param name="_text">Contenido de la Celda</param>
    ''' <param name="_size">Tamano de letra del contenido de la celda</param>
    ''' <param name="_style">Estilo del Contenido de la Celda. 0: NORMAL, 1: BOLD, 2: ITALIC, 3: BOLDITALIC</param>
    ''' <param name="_haligment">Alineacion horizontal del contenido de la celda. 0: LEFT, 1: CENTER, 2: RIGHT, 4: TOP, 5: MIDDLE</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fc_CeldaTexto2(ByVal _text As String, ByVal _size As Single, ByVal _style As Integer, ByVal _haligment As Integer) As iTextSharp.text.pdf.PdfPCell
        Dim celdaITC2 As iTextSharp.text.pdf.PdfPCell
        Dim fontITC2 As iTextSharp.text.Font
        fontITC2 = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style)
        celdaITC2 = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(_text, fontITC2))
        celdaITC2.HorizontalAlignment = _haligment
        Return celdaITC2
    End Function

#End Region

End Class

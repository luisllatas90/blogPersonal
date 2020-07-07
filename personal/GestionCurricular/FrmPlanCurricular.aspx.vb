﻿Imports System.Collections.Generic
Imports System.IO

Partial Class GestionCurricular_FrmPlanCurricular
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private cod_user As Integer '= 684
    Private cod_ctf As Integer
    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles") '"http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"
    Private idTabla As Integer = 18
    Private idTablaDecreto As Integer = 19

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

            If Not IsPostBack Then
                btnFuArchivoDecreto.Attributes.Add("onClick", "document.getElementById('" + fuArchivoDecreto.ClientID + "').click();")
                btnFuArchivoPlan.Attributes.Add("onClick", "document.getElementById('" + fuArchivoPlan.ClientID + "').click();")

                Call mt_CargarCarreraProfesional()
            Else
                'If Me.fuArchivoDecreto.HasFile Then
                '    lblFuArchivoDecreto.InnerText = Me.fuArchivoDecreto.PostedFile.FileName.ToString
                '    updFileDecreto.Update()
                'End If

                'If Me.fuArchivoPlan.HasFile Then
                '    lblFuArchivoPlan.InnerText = Me.fuArchivoPlan.PostedFile.FileName.ToString
                '    updFilePlan.Update()
                'End If

                Call RefreshGrid()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            If ddlCarreraProf.SelectedValue = -1 Then
                Call mt_ShowMessage("Seleccione una carrera profesional", MessageType.Info)
            Else
                Dim cod As String = Me.ddlCarreraProf.SelectedValue
                Dim año As Int16 = Year(Now)

                Me.hdCodigoPlan.Value = ""
                Me.hdCodigoCpf.Value = cod

                Me.lblCarrera.Text = Me.ddlCarreraProf.SelectedItem.ToString
                Me.txtNombre.Text = "PLAN CURRICULAR " & año.ToString

                spnFileDecreto.InnerHtml = "No se eligió decreto"
                spnFilePlan.InnerHtml = "No se eligió el plan"

                'Me.RequiredFieldValidator1.Enabled = True
                'Me.RequiredFieldValidator2.Enabled = True

                Page.RegisterStartupScript("Pop", "<script>openModal('nuevo', '" & cod & "', '');</script>")
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.gvPlanCurricular.Rows.Count = 0 Then
                Call mt_ShowMessage("No existe plan curricular para ésta carrera profesional", MessageType.Info)
                Me.hdCodigoPlan.Value = ""
                Me.hdCodigoCpf.Value = ""

                Call mt_CargarDatos()
            Else
                Dim button As HtmlButton = DirectCast(sender, HtmlButton)
                Dim cod, vig As String

                Me.hdCodigoPlan.Value = button.Attributes("codigo_pcur")
                Me.hdCodigoCpf.Value = button.Attributes("codigo_cpf")
                Me.txtNombre.Text = button.Attributes("nombre_pcur")
                Me.txtNroDecreto.Text = button.Attributes("decreto")
                Me.lblCarrera.Text = button.Attributes("nombre_cpf")
                Me.spnFilePlan.InnerText = button.Attributes("archivoPlan")
                Me.spnFileDecreto.InnerText = button.Attributes("archivoDecreto")

                'Me.RequiredFieldValidator1.Enabled = False
                'Me.RequiredFieldValidator2.Enabled = False

                cod = button.Attributes("codigo_cpf")
                vig = button.Attributes("vigente")

                Page.RegisterStartupScript("Pop", "<script>openModal('editar', '" & cod & "', '" & vig & "');</script>")
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnVerDecreto_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim codigo As String = button.Attributes("codigo_pcur")

            Call mt_DescargarArchivo(codigo, "decreto")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Warning)
        End Try
    End Sub

    Protected Sub btnVerPlan_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim codigo As String = button.Attributes("codigo_pcur")

            Call mt_DescargarArchivo(codigo, "plan")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Warning)
        End Try
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'Dim JSONresult As String = ""
        'Dim Data As New Dictionary(Of String, Object)()
        'Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsConectarDatos
        Dim codigo_pcur, codigo_cpf, vigente As Integer
        Dim dt As New Data.DataTable
        Dim nuevo As Boolean = False
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            If Not (IsDBNull(Session("perlogin"))) And Session("perlogin").ToString <> "" Then
                ''If Me.fuArchivo.HasFile And Me.fuPlanCurricular.HasFile Then
                vigente = IIf(Me.chkVigente.Checked, 1, 0)

                obj.IniciarTransaccion()

                If String.IsNullOrEmpty(Me.hdCodigoPlan.Value) Then
                    dt = obj.TraerDataTable("COM_RegistrarPlanCurricular", Me.txtNombre.Text, Me.txtNroDecreto.Text, Me.ddlCarreraProf.SelectedValue, vigente, idTabla, idTablaDecreto, cod_user)

                    If dt.Rows.Count > 0 Then
                        codigo_pcur = CInt(dt.Rows(0).Item(0).ToString)
                        codigo_cpf = Me.ddlCarreraProf.SelectedValue
                        nuevo = True
                    End If
                Else
                    codigo_pcur = CInt(Me.hdCodigoPlan.Value)
                    codigo_cpf = CInt(Me.hdCodigoCpf.Value)
                End If

                If Me.fuArchivoDecreto.HasFile Then
                    Dim Archivos As HttpFileCollection = Request.Files
                    Call fc_SubirArchivo(idTablaDecreto, codigo_pcur, Archivos(0))
                End If

                If Me.fuArchivoPlan.HasFile Then
                    Dim Archivos As HttpFileCollection = Request.Files
                    Call fc_SubirArchivo(idTabla, codigo_pcur, Archivos(1))
                End If

                obj.Ejecutar("COM_ActualizarPlanCurricular", codigo_pcur, Me.txtNombre.Text, Me.txtNroDecreto.Text, codigo_cpf, vigente, idTabla, idTablaDecreto)

                'Data.Add("Status", "OK")
                'Data.Add("Message", "aaa")
                'list.Add(Data)
                'JSONresult = serializer.Serialize(list)
                'Response.Write(JSONresult)

                obj.TerminarTransaccion()

                If nuevo Then
                    Call mt_ShowMessage("Plan curricular registrado", MessageType.Success)
                Else
                    Call mt_ShowMessage("Plan curricular actualizado", MessageType.Success)
                End If

                Call mt_CargarDatos()

                ''Else
                ''    Throw New Exception("Seleccione un documento. Solo se permiten archivos PDF")
                ''End If
            Else
                Throw New Exception("Inicie Sesión")
            End If
        Catch ex As Exception
            obj.AbortarTransaccion()
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)

            'Data.Add("Status", "Fail")
            'Data.Add("Message", ex.Message & " - " & Session("perlogin").ToString)
            'list.Add(Data)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)
        End Try
    End Sub

    Protected Sub ddlCarreraProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarreraProf.SelectedIndexChanged
        Call mt_CargarDatos()
    End Sub

    Protected Sub gvPlanCurricular_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPlanCurricular.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim celda As TableCellCollection = e.Row.Cells
            Dim codigo_cpf As String = Me.gvPlanCurricular.DataKeys(e.Row.RowIndex).Values.Item("codigo_cpf")
            Dim nombre_cpf As String = Me.gvPlanCurricular.DataKeys(e.Row.RowIndex).Values.Item("nombre_cpf")
            Dim nombre_pcur As String = Me.gvPlanCurricular.DataKeys(e.Row.RowIndex).Values.Item("nombre_pcur")
            Dim vigente_pcur As String = Me.gvPlanCurricular.DataKeys(e.Row.RowIndex).Values.Item("vigente_pcur")
            Dim codigo_pcur As String = Me.gvPlanCurricular.DataKeys(e.Row.RowIndex).Values.Item("codigo_pcur")
            Dim archivoPlan As String = Me.gvPlanCurricular.DataKeys(e.Row.RowIndex).Values.Item("archivoPlan")
            Dim archivoDecreto As String = Me.gvPlanCurricular.DataKeys(e.Row.RowIndex).Values.Item("archivoDecreto")

            Dim idx As Integer = e.Row.RowIndex + 1

            Dim btnEditar As New HtmlButton
            With btnEditar
                .ID = "btnEditar" & idx
                .Attributes.Add("class", "btn btn-primary btn-sm")
                .Attributes.Add("type", "button")
                .Attributes.Add("codigo_pcur", codigo_pcur)
                .Attributes.Add("nombre_pcur", nombre_pcur)
                .Attributes.Add("codigo_cpf", codigo_cpf)
                .Attributes.Add("nombre_cpf", nombre_cpf)
                .Attributes.Add("decreto", CStr(e.Row.Cells(1).Text).Trim())
                .Attributes.Add("vigente", vigente_pcur)
                .Attributes.Add("archivoPlan", archivoPlan)
                .Attributes.Add("archivoDecreto", archivoDecreto)
                .Attributes.Add("title", "Editar aprobación")
                .InnerHtml = "<i class='fa fa-edit' title='Editar aprobación'></i>"

                AddHandler .ServerClick, AddressOf btnEditar_Click
            End With

            If cod_ctf <> 9 Then
                celda(3).Controls.Add(btnEditar)
            End If

            Dim btnVerDecreto As New HtmlButton
            With btnVerDecreto
                .ID = "btnVerDecreto" & idx
                .Attributes.Add("class", "btn btn-info btn-sm")
                .Attributes.Add("type", "button")
                .Attributes.Add("codigo_pcur", codigo_pcur)
                .Attributes.Add("title", "Visualizar Decreto")
                .InnerHtml = "<i class='fa fa-download' title='Visualizar Decreto'></i>"

                AddHandler .ServerClick, AddressOf btnVerDecreto_Click
            End With
            celda(4).Controls.Add(btnVerDecreto)

            Dim btnVerPlan As New HtmlButton
            With btnVerPlan
                .ID = "btnVerPlan" & idx
                .Attributes.Add("class", "btn btn-warning btn-sm")
                .Attributes.Add("type", "button")
                .Attributes.Add("codigo_pcur", codigo_pcur)
                .Attributes.Add("title", "Visualizar Plan")
                .InnerHtml = "<i class='fa fa-download' title='Visualizar Plan'></i>"

                AddHandler .ServerClick, AddressOf btnVerPlan_Click
            End With
            celda(5).Controls.Add(btnVerPlan)

            gvPlanCurricular.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarCarreraProfesional()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            If cod_ctf = 1 Or cod_ctf = 232 Or cod_ctf = 138 Then
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "CF", "2", cod_user)
            Else
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "UC", "2", cod_user)
            End If
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlCarreraProf, dt, "codigo_Cpf", "nombre_Cpf")
            dt.Dispose()
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
        If ddlCarreraProf.SelectedValue = -1 Or ddlCarreraProf.SelectedValue = -2 Then
            Me.ddlCarreraProf.Focus()
            Me.gvPlanCurricular.DataSource = Nothing
            Me.gvPlanCurricular.DataBind()

            Call mt_ShowMessage("Seleccione una carrera profesional", MessageType.Info)
            Me.btnAgregar.Visible = False
        Else
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

            Try
                Me.btnAgregar.Visible = True

                If cod_ctf <> 9 And cod_ctf <> 218 Then
                    cod_ctf = -1
                End If

                obj.AbrirConexion()
                dt = obj.TraerDataTable("COM_ListarPlanCurricular_V2", ddlCarreraProf.SelectedValue, -1, idTabla, idTablaDecreto, cod_ctf)
                obj.CerrarConexion()
                Me.gvPlanCurricular.DataSource = dt
                Me.gvPlanCurricular.DataBind()
            Catch ex As Exception
                Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
            End Try
        End If
    End Sub

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    Private Sub RefreshGrid()
        For Each _Row As GridViewRow In gvPlanCurricular.Rows
            gvPlanCurricular_RowDataBound(gvPlanCurricular, New GridViewRowEventArgs(_Row))
        Next
    End Sub

    Private Sub mt_DescargarArchivo(ByVal codigo_pcur As Long, ByVal tipo As String)
        Try
            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Dictionary(Of String, String)
            Dim obj As New ClsConectarDatos
            Dim tb, dt As New Data.DataTable
            Dim cif As New ClsCRM
            Dim usuario As String = Session("perlogin") '"USAT\ESAAVEDRA"
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarPlanCurricular_V2", ddlCarreraProf.SelectedValue, codigo_pcur, idTabla, idTablaDecreto)
            If dt.Rows.Count = 0 Then Throw New Exception("No existe plan curricular para esta carrera")

            If tipo.Equals("plan") Then
                tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, idTabla, codigo_pcur, "1TEP17WQYR") ' Desarrollo
            Else
                tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, idTablaDecreto, codigo_pcur, "ZAYRMZ61LP") ' Desarrollo
            End If

            obj.CerrarConexion()
            If tb.Rows.Count = 0 Then Throw New Exception("No existe archivo del plan curricular para esta carrera")
            list.Add("IdArchivo", tb.Rows(0).Item("IdArchivo").ToString)
            list.Add("Usuario", usuario)

            If tipo.Equals("plan") Then
                list.Add("Token", "1TEP17WQYR") ' Desarrollo
            Else
                list.Add("Token", "ZAYRMZ61LP") ' Desarrollo
            End If

            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", usuario)
            Dim imagen As String = fc_ResultFile(result)
            Dim bytes As Byte() = Convert.FromBase64String(imagen)
            Response.Clear()
            Response.Buffer = False
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment;filename=" & tb.Rows(0).Item("NombreArchivo").ToString.Replace(",", ""))
            Response.AppendHeader("Content-Length", bytes.Length.ToString())
            Response.BinaryWrite(bytes)
            Response.End()
            'Response.Write(envelope)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Funciones"

    Function fc_SubirArchivo(ByVal idTabla As String, ByVal codigo As String, ByVal file As HttpPostedFile) As String
        Dim list As New Dictionary(Of String, String)
        Try
            Dim _TablaId As String = idTabla
            Dim _Archivo As HttpPostedFile = file
            Dim _TransaccionId As String = codigo
            Dim _Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim _Usuario As String = Session("perlogin").ToString '"USAT\ESAAVEDRA"
            Dim Input(_Archivo.ContentLength) As Byte

            Dim br As New BinaryReader(_Archivo.InputStream)
            Dim binData As Byte() = br.ReadBytes(_Archivo.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)
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
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", _Usuario)

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

#End Region

End Class

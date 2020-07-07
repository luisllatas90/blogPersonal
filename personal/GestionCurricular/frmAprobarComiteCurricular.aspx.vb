Imports System.Collections.Generic
Imports System.IO

Partial Class GestionCurricular_frmAprobarComiteCurricular
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer
    Dim cod_ctf As Integer
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
                Response.Redirect("../../sinacceso.html")
            End If
            cod_user = Request.QueryString("id")
            cod_ctf = Request.QueryString("ctf")
            If IsPostBack = False Then
                Call mt_CargarCarreraProfesional()
                Call cboCarProf_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAdjuntar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim x As Integer
        Try
            If Me.gvComiteCurricular.Rows.Count = 0 Then
                Page.RegisterStartupScript("alerta", "<script>alert('¡ No existen miembros registrados en para esta carrera profesional !');</script>")
                'Me.btnListar.Focus()
                Me.hdcodigo_com.Value = ""
            Else
                For x = 0 To Me.gvComiteCurricular.Rows.Count - 1
                    Me.hdcodigo_com.Value = Me.gvComiteCurricular.DataKeys(x).Values("codigo_com")
                    If Me.gvComiteCurricular.DataKeys(x).Values("Estado").ToString = "APROBADO" Then
                        Page.RegisterStartupScript("alerta", "<script>alert('¡ El Comité Curricular esta Aprobado !');</script>")
                    Else
                        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Aprobar" & "');</script>")
                    End If
                Next
            End If
        Catch ex As Exception
            mt_ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    'Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
    '    If cboCarProf.SelectedValue = -1 Then
    '        Page.RegisterStartupScript("alerta", "<script>alert('¡ Seleccione una Carrera Profesional !');</script>")
    '        Me.cboCarProf.Focus()
    '        Me.gvComiteCurricular.DataSource = Nothing
    '        Me.gvComiteCurricular.DataBind()
    '    Else
    '        mt_CargarDatos()
    '    End If
    'End Sub

    'Protected Sub btnListar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar2.Click
    '    If cboCarProf.SelectedValue = -1 Then
    '        'Page.RegisterStartupScript("alerta", "<script>alert('¡ Seleccione una Carrera Profesional !');</script>")
    '        mt_ShowMessage("¡ Seleccione una Carrera Profesional !", MessageType.Warning)
    '        Me.cboCarProf.Focus()
    '        Me.gvComiteCurricular.DataSource = Nothing
    '        Me.gvComiteCurricular.DataBind()
    '    Else
    '        mt_CargarDatos()
    '    End If
    'End Sub

    Protected Sub cboCarProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarProf.SelectedIndexChanged
        If cboCarProf.SelectedValue = -1 Then
            mt_ShowMessage("¡ Seleccione una Carrera Profesional !", MessageType.Warning)
            Me.cboCarProf.Focus()
            Me.gvComiteCurricular.DataSource = Nothing
            Me.gvComiteCurricular.DataBind()
        Else
            mt_CargarDatos()
        End If
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim Data As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim codigo_com, idtabla, codigo_dec As Integer
        Dim dt As New Data.DataTable
        Dim respuesta As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            If Not (IsDBNull(Session("perlogin"))) And Session("perlogin").ToString <> "" Then
                codigo_com = CInt(Me.hdcodigo_com.Value)
                obj.IniciarTransaccion()
                dt = obj.TraerDataTable("ComiteCurricularDecreto_insertar", codigo_com, CDate(Me.txtFechaAprobacion.Text), Me.txtNroDecreto.Text, 0, cod_user)
                If dt.Rows.Count > 0 Then
                    codigo_dec = CInt(dt.Rows(0).Item(0).ToString)
                    'Data.Add("Add", codigo_dec)
                End If
                idtabla = 17 ' Desarrollo
                'idtabla = 12 ' Produccion
                Dim str As String = ""
                If Me.fuArchivo.HasFile Then
                    Dim Archivos As HttpFileCollection = Request.Files
                    For i As Integer = 0 To Archivos.Count - 1
                        str = fc_SubirArchivo(idtabla, codigo_dec, Archivos(i))
                        'Data.Add("Step", idtabla & "-" & codigo_dec & "-" & Archivos(i).FileName)
                    Next
                End If
                'Data.Add("file", str)
                obj.Ejecutar("ComiteCurricularDecreto_actualizar", codigo_dec, codigo_com, CDate(Me.txtFechaAprobacion.Text), Me.txtNroDecreto.Text, 1, 0, cod_user)
                'Data.Add("Update", codigo_dec)
                obj.TerminarTransaccion()
                mt_CargarDatos()
            Else
                Throw New Exception("Inicie Session")
            End If

            'Data.Add("Status", "OK")
            'Data.Add("Message", respuesta)
            'list.Add(Data)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)

            mt_ShowMessage("¡ Se ha aprobó el comité curricular !", MessageType.Success)

        Catch ex As Exception
            obj.AbortarTransaccion()
            'obj.CerrarConexion()
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
            'Data.Add("Status", "Fail")
            'Data.Add("Message", ex.Message)
            'list.Add(Data)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)
        End Try
    End Sub

    Protected Sub gvComiteCurricular_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvComiteCurricular.RowCommand
        Dim obj As New ClsConectarDatos
        Dim index, codigo_com As Integer
        Dim msj As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            index = CInt(e.CommandArgument)
            codigo_com = Me.gvComiteCurricular.DataKeys(index).Values("codigo_com")
            obj.AbrirConexion()
            Select Case e.CommandName
                Case "Habilitar"
                    obj.Ejecutar("ComiteCurricular_Aprobar", "H", codigo_com, 1, cod_user)
                    msj = "habilitó"
                Case "Deshabilitar"
                    obj.Ejecutar("ComiteCurricular_Aprobar", "H", codigo_com, 0, cod_user)
                    msj = "deshabilitó"
                Case "Alta"
                    obj.Ejecutar("ComiteCurricular_Aprobar", "V", codigo_com, 1, cod_user)
                    msj = "dió de alta"
                Case "Baja"
                    obj.Ejecutar("ComiteCurricular_Aprobar", "V", codigo_com, 0, cod_user)
                    msj = "dió de baja"
            End Select
            obj.CerrarConexion()
            mt_ShowMessage("¡ Se " & msj & " el comite curricular ! ", MessageType.Success)
            mt_CargarDatos()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

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
            'If cod_user = 684 Then
            '    dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "CF", "2", cod_user)
            'Else
            '    dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "UC", "2", cod_user)
            'End If
            dt = obj.TraerDataTable("COM_ListarCarreraProfesional", cod_user, cod_ctf)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboCarProf, dt, "codigo_Cpf", "nombre_Cpf")
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
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ComiteCurricularMiembros_Listar2", "LC", cboCarProf.SelectedValue)
            obj.CerrarConexion()
            Me.gvComiteCurricular.DataSource = dt
            Me.gvComiteCurricular.DataBind()
            'If Me.gvComiteCurricular.Rows.Count > 0 Then mt_AgruparFilas(Me.gvComiteCurricular.Rows, 0, 5)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
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
            mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
        End If
        count = 1
        lst.Clear()
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
            'Dim _Usuario As String = Session("perlogin").ToString
            Dim _Usuario As String = "SERVERDEV\esaavedra"
            Dim Input(_Archivo.ContentLength) As Byte

            Dim br As New BinaryReader(_Archivo.InputStream)
            Dim binData As Byte() = br.ReadBytes(_Archivo.InputStream.Length)
            'Dim base64 = System.Convert.ToBase64String(binData)
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

            Return fc_ResultFile(result)

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

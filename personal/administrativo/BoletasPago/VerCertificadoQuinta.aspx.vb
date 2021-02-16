Imports System.Collections.Generic
Imports System.Xml

Partial Class administrativo_BoletasPago_VerBoletaPago
    Inherits System.Web.UI.Page

    Dim ruta As String = ConfigurationManager.AppSettings("SharedFiles")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If IsPostBack = False Then
            ListarPersonal()
        End If
    End Sub

    Private Sub ListarPersonal()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        Dim clsf As New ClsCRM
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'tb = obj.TraerDataTable("dbo.USP_LISTAR_PERSONA_BOLETA")
        tb = obj.TraerDataTable("USP_LISTAR_PERSONA_CERTIFICADOQTA", "1", Session("id_per"))
        obj.CerrarConexion()
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlPersonal.Items.Add(New ListItem(tb.Rows(i).Item("DatosPersona"), clsf.EncriptaTexto(tb.Rows(i).Item("CodigoPer").ToString())))
            Next
        End If

    End Sub


    Private Sub ListarCertQuinta(ByVal codigo_usu As Integer)
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        '  codigo_usu = Request.Form("Param01")
        ' Response.Write(codigo_usu)
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim JSONresult As String = ""
        tb = obj.TraerDataTable("dbo.USP_LISTARCERTIFICADOSQTA_PERSONA", "CT", 0, codigo_usu, Session("id_per"))
        obj.CerrarConexion()
        Dim cif As New ClsCRM

        If tb.Rows.Count > 0 Then
            Me.gvLista.DataSource = tb
        Else
            Me.gvLista.DataSource = Nothing
        End If
        Me.gvLista.DataBind()

        'Dim list As New List(Of Dictionary(Of String, Object))()
        'For i As Integer = 0 To tb.Rows.Count - 1

        '    Dim Data As New Dictionary(Of String, Object)()
        '    'Data.Add("Codigo", cif.EncriptaTexto(tb.Rows(i).Item("Codigo")))
        '    'Data.Add("FechaIni", tb.Rows(i).Item("FechaIni"))
        '    'Data.Add("Periodo", tb.Rows(i).Item("Periodo"))
        '    'Data.Add("Mes", tb.Rows(i).Item("Mes"))
        '    'Data.Add("FechaFin", tb.Rows(i).Item("FechaFin"))
        '    'Data.Add("TipoPlanilla", tb.Rows(i).Item("TipoPlanilla"))
        '    'Data.Add("Estado", tb.Rows(i).Item("Estado"))
        '    'Data.Add("FechaPago", tb.Rows(i).Item("FechaPago"))
        '    'Data.Add("CodigoTplla", tb.Rows(i).Item("CodigoTplla"))
        '    'Data.Add("IdArchivosCompartidos", cif.EncriptaTexto(tb.Rows(i).Item("IdArchivosCompartido")))
        '    'Data.Add("BolGenerado", tb.Rows(i).Item("BolGenerado"))
        '    Data.Add("Codigo", cif.EncriptaTexto(tb.Rows(i).Item("Codigo")))
        '    Data.Add("FechaIni", tb.Rows(i).Item("FechaIni"))
        '    Data.Add("FechaFin", tb.Rows(i).Item("FechaFin"))
        '    'Data.Add("NroDias", tb.Rows(i).Item("NroDias"))
        '    Data.Add("NroDias", "")
        '    'Data.Add("NroMemo", tb.Rows(i).Item("NroMemo"))
        '    Data.Add("NroMemo", "")
        '    'Data.Add("IdArchivosCompartidos", cif.EncriptaTexto(tb.Rows(i).Item("IdArchivosCompartido")))
        '    Data.Add("IdArchivosCompartidos", tb.Rows(i).Item("IdArchivosCompartido"))
        '    Data.Add("VacGenerado", tb.Rows(i).Item("VacGenerado"))
        '    list.Add(Data)
        'Next
        'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        'JSONresult = serializer.Serialize(list)
        'Response.Write(JSONresult)
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        ListarCertQuinta(Session("id_per"))
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingBC", "fnLoading(false)", True)

    End Sub

    Protected Sub gvLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvLista.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If (e.CommandName = "Aceptar") Then
            Dim clsf As New ClsCRM
            Dim periodo As Integer = Me.gvLista.DataKeys(e.CommandArgument).Values("Codigo")
            Dim codigo_per As Integer = clsf.DesencriptaTexto(Me.ddlPersonal.SelectedValue)
            GenerarCertificadoQuinta(periodo, codigo_per)
        End If
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingRC", "fnLoading(false)", True)

    End Sub


    Sub GenerarCertificadoQuinta(ByVal Codigo_dvac As Integer, ByVal Codigo_per As Integer)
        ' CodigoPer = Request.Form("PersonaId")
        'Response.Write(Request.Form("PersonaId"))
        ' Response.Write(Desencriptar("PERU"))
        ' Dim b As New BinaryReader(post.InputStream)
        '  Dim by() As Byte = b.ReadByte(post.ContentLength)
        Try
            Dim cif As New ClsCRM
            'Dim Codigo_dvac As Integer = cif.DesencriptaTexto(Request.Form("Param1"))
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos

            Dim IpRemoto As String = Request.UserHostAddress.ToString
            Dim HostName As String = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName.ToString

            'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            'obj.AbrirConexion()

            'tb = obj.TraerDataTable("dbo.USP_LISTARVACACIONES_PERSONA", "UN", Codigo_dvac, 0, Session("id_per"))
            'obj.CerrarConexion()

            Dim list As New Dictionary(Of String, String)
            Dim wsCloud As New ClsArchivosCompartidos
            list.Add("CodigoPer", CInt(Codigo_per))
            list.Add("CodigoDvac", CInt(Codigo_dvac))
            list.Add("TablaId", 3)                                     'Boletas de Pago
            list.Add("Usuario", Session("perlogin").ToString().Trim)
            list.Add("param8", Session("perlogin").ToString().Trim)
            Dim envelope As String = wsCloud.SoapEnvelopeGenerarQuinta(list)
            'Response.Write(envelope)

            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/GenerateQuinta", Session("perlogin"))

            'Response.Write(result)
            Dim Data As ResultMessage = ResultMessageQuinta(result)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Mensaje6", "fnMensaje('success','" + result + "')", True)

            'Response.Write(Data)

            'Response.Write(cif.EncrytedString64(Data.StatusBody.Code))
            Dim tblRes As New Data.DataTable
            If Data.Status = "OK" Then
                obj.AbrirConexion()
                tblRes = obj.TraerDataTable("dbo.USP_CertificadoQta", 0, CInt(Codigo_per), CInt(Codigo_dvac), Data.StatusBody.Code, "", Session("perlogin").ToString().Trim, HostName, IpRemoto, "R")
                obj.CerrarConexion()

                If tblRes.Rows.Count > 0 Then
                    If tblRes.Rows(0)("Status") = "ERROR" Then
                        'Data.Status = tblRes.Rows(0)("Status")
                        'Data.StatusBody.Message = tblRes.Rows(0)("Message")
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Mensaje1", "fnMensaje('error','No se pudo generar certificado de quinta" + tblRes.Rows(0)("Message") + "')", True)
                    Else
                        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Mensaje2", "fnMensaje('success','Certificado de Quinta generado correctamente')", True)

                    End If
                    'Data.StatusBody.Code = tblRes.Rows(0)("IdArchivosCompartido")
                End If
            Else
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Mensaje3", "fnMensaje('error','No se pudo generar certificado de quinta')", True)
            End If
            ''Data.StatusBody.Code = cif.EncrytedString64(Data.StatusBody.Code)
            'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            'Dim JSONresult As String = ""

            'JSONresult = serializer.Serialize(Data)
            'JSONresult = serializer.Serialize(envelope)
            'Response.Write(JSONresult)
        Catch ex As Exception
            'Dim JSONresult As String = ""
            'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

            'Dim Data As New Dictionary(Of String, Object)()
            'Data.Add("error msje", ex.Message & " SUBIR ARCHIVO--- " & ex.StackTrace)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Mensaje1", "fnMensaje('error','No se pudo generar certificado de quinta: " + ex.Message.ToString + " - " + ex.StackTrace + "')", True)

            'JSONresult = serializer.Serialize(Data)
            'Response.Write(JSONresult)
        End Try
    End Sub

    Function ResultMessageQuinta(ByVal cadXml As String) As ResultMessage
        Dim nsMgr As XmlNamespaceManager
        Dim xml As XmlDocument = New XmlDocument()
        xml.LoadXml(cadXml)
        nsMgr = New XmlNamespaceManager(xml.NameTable)
        nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
        nsMgr.AddNamespace("xm", "http://usat.edu.pe")
        Dim Code1 As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateQuintaResponse/xm:GenerateQuintaResult", nsMgr)
        Dim Status As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateQuintaResponse/xm:GenerateQuintaResult/xm:Status", nsMgr)
        Dim Code As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateQuintaResponse/xm:GenerateQuintaResult/xm:StatusBody/xm:Code", nsMgr)
        Dim Message As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateQuintaResponse/xm:GenerateQuintaResult/xm:StatusBody/xm:Message", nsMgr)
        '  Dim mNombre = xml.ReadElementString("nombre")
        ' Return res.InnerXml '.InnerText
        '   Response.Write("dd" + res.InnerText)
        Dim cif As New ClsCRM
        Dim result As New ResultMessage
        result.Status = Status.InnerXml
        result.StatusBody.Code = Code.InnerXml
        result.StatusBody.Message = Message.InnerXml

        Return result
    End Function

End Class

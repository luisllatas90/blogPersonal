Imports System.Collections.Generic
Imports System.Xml
Imports System.Web.Script.Serialization

Partial Class Admision_frm_ResultadosAdmision
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    Private ReadOnly soap As New ClsAdmisionSOAP
    Private ReadOnly urlServicio As String = ConfigurationManager.AppSettings("RutaCampusLocal") & "WSUSAT/WSUSAT.asmx"
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim codigosCco As String = Request.QueryString("cco")
                Dim codigosCac As String = Request.QueryString("cac")

                codigosCac = "77" 'PENDIENTE! -> Este código se enviaría como parámetro al insertarse en el iframe

                mt_CargarEventosAdmision("CECO", codigosCco, codigosCac)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Métodos"
    Private Sub mt_CargarEventosAdmision(ByVal tipoConsulta As String, ByVal codigosCco As String, ByVal codigosCac As String)
        Try
            Dim lo_Datos As New Dictionary(Of String, String)
            With lo_Datos
                .Add("tipoConsulta", "CECO")
                .Add("codigosCco", codigosCco)
                .Add("codigosCac", codigosCac)
            End With

            Dim nombreNodo As String = "ConsultarEventoAdmision"
            Dim xmlRespuesta As New XmlDocument : xmlRespuesta.LoadXml(soap.lr_RealizarPeticionSOAP(urlServicio, nombreNodo, lo_Datos))
            Dim ns As XmlNamespaceManager = New XmlNamespaceManager(xmlRespuesta.NameTable) : ns.AddNamespace("ns", "http://tempuri.org/")
            Dim rutaNodos As String = "//ns:" & nombreNodo & "Response/ns:" & nombreNodo & "Result/ns:e_ListItem"
            Dim respuesta As XmlNodeList = xmlRespuesta.DocumentElement.SelectNodes(rutaNodos, ns)

            Dim dtEventoAdmision As New Data.DataTable
            dtEventoAdmision.Columns.Add("codigo_Cco")
            dtEventoAdmision.Columns.Add("descripcion_Cco")

            For Each nodo As XmlNode In respuesta
                dtEventoAdmision.Rows.Add(nodo.Item("Valor").InnerText, nodo.Item("Nombre").InnerText)
            Next

            rptEventoAdmision.DataSource = dtEventoAdmision
            rptEventoAdmision.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class

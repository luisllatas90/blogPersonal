Imports System.Collections.Generic
Imports System.Xml

Partial Class Admision_frm_ResultadosAdmisionBuscar
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    Private ReadOnly soap As New ClsAdmisionSOAP
    Private ReadOnly urlServicio As String = ConfigurationManager.AppSettings("RutaCampusLocal") & "WSUSAT/WSUSAT.asmx"
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                mensaje.Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Try
            Dim codigoCco As Integer = Request.QueryString("cco")
            Dim nroDocIdentidad As String = dni.Value
            mt_ConsultarCondicionIngreso("RC", codigoCco, nroDocIdentidad)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Métodos"
    Private Sub mt_ConsultarCondicionIngreso(ByVal tipoConsulta As String, ByVal codigoCco As String, ByVal nroDocIdentidad As String)
        Try
            Dim lo_Datos As New Dictionary(Of String, String)
            With lo_Datos
                .Add("tipoConsulta", "RF")
                .Add("codigoCco", codigoCco)
                .Add("nroDocIdentidad", nroDocIdentidad)
            End With

            Dim nombreNodo As String = "ConsultarCondicionIngreso"
            Dim xmlRespuesta As New XmlDocument : xmlRespuesta.LoadXml(soap.lr_RealizarPeticionSOAP(urlServicio, nombreNodo, lo_Datos))
            Dim ns As XmlNamespaceManager = New XmlNamespaceManager(xmlRespuesta.NameTable) : ns.AddNamespace("ns", "http://tempuri.org/")
            Dim rutaNodos As String = "//ns:" & nombreNodo & "Response/ns:" & nombreNodo & "Result/ns:e_ListItem"
            Dim respuesta As XmlNode = xmlRespuesta.DocumentElement.SelectSingleNode(rutaNodos, ns)

            If respuesta Is Nothing Then
                mensaje.Attributes.Item("class") = "alert alert-danger"
                mensaje.InnerHtml = "<b>No se ha encontrado ningún registro con ese número de documento de identidad</b>"
            Else
                Dim estadoNota As String = respuesta.Item("Adicional3").InnerText.Trim
                If estadoNota = "C" Then
                    Select Case respuesta.Item("Valor").InnerText.Trim
                        Case "P"
                            mensaje.Attributes.Item("class") = "alert alert-danger"
                        Case "A"
                            mensaje.Attributes.Item("class") = "alert alert-warning"
                        Case "I"
                            mensaje.Attributes.Item("class") = "alert alert-success"
                    End Select

                    Dim msg As String = "Hola <b>" & respuesta.Item("Adicional4").InnerText.Trim & "</b>" & _
                                        " tu condición es: <br><br><b style='font-size: 20px;'>" & respuesta.Item("Nombre").InnerText & _
                                        "</b><br><br> a la carrera de: <b>" & respuesta.Item("Adicional5").InnerText.Trim & "</b>"
                    mensaje.InnerHtml = msg

                Else
                    mensaje.Attributes.Item("class") = "alert alert-info"
                    mensaje.InnerHtml = "<b>Las notas aún no han sido pubicadas</b>"
                End If

            End If
            mensaje.Visible = True
            udpMensaje.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class

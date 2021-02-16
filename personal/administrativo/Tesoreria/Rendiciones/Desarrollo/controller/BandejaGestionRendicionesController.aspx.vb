
Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Web
Imports System.Net
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Text
Imports System.Web.Script.Serialization
Partial Class administrativo_Tesoreria_Rendiciones_Desarrollo_controller_BandejaGestionRendicionesController
    Inherits System.Web.UI.Page

    Protected g_DataSesion As ClsDataSesion
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim l_Rpt As ClsRespuestaServidor = New ClsRespuestaServidor()
        Dim l_Funcion As String = Request.Form("Funcion")
        Dim l_Data As String = Request.Form("Data")
        Try

            'g_DataSesion = New ClsDataSesion(Session("id_per"), Session("perlogin"), Session("nombreper"))
            g_DataSesion = New ClsDataSesion(684, "USAT\esaavedra", "SAAVEDRA SANCHEZ, HUGO ENRIQUE")
            Select Case l_Funcion
                Case "ObtenerListaRendicion"
                    ObtenerListaRendicion(l_Data)

                Case "ObtenerDetalleRendicion"
                    ObtenerDetalleRendicion(l_Data)

                Case Else
                    Response.Write("Funcion enviada no ha sido encontrada")
            End Select

        Catch ex As Exception
            l_Rpt.LogError.SetException(ex)
            Response.Write(New JavaScriptSerializer().Serialize(l_Rpt))
        End Try

    End Sub

    Sub ObtenerListaRendicion(ByRef p_Data As String)
        Response.Write(New JavaScriptSerializer().Serialize(New ClsBandejaGestionRendiciones(g_DataSesion).ObtenerListaRendicion(New JavaScriptSerializer().Deserialize(Of ClsEntradaBandejaGestionRendiciones)(p_Data))))
    End Sub

    Sub ObtenerDetalleRendicion(ByRef p_Data As String)
        Response.Write(New JavaScriptSerializer().Serialize(New ClsBandejaGestionRendiciones(g_DataSesion).ObtenerDetalleRendicion(New JavaScriptSerializer().Deserialize(Of ClsEntradaBandejaGestionRendiciones)(p_Data))))
    End Sub

End Class

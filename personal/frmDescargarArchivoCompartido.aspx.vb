Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class frmDescargarArchivoCompartido
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES
    Dim me_ArchivoCompartido As e_ArchivoCompartido

    'DATOS
    Dim md_ArchivoCompartido As New d_ArchivoCompartido
    Dim md_Funciones As New d_Funciones

    'VARIABLES
    Dim cod_user As Integer = 0

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing OrElse Session("perlogin") Is Nothing) Then
                Response.Redirect("sinacceso.html")
            End If

            cod_user = Session("id_per")

            If mt_DescargarArchivo(CInt(Request("Id").ToString.Trim)) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", "window.close();", True)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function mt_DescargarArchivo(ByVal IdArchivosCompartidos As Integer) As Boolean
        Try
            'Obtener los datos del archivo compartido
            me_ArchivoCompartido = md_ArchivoCompartido.GetArchivoCompartido(IdArchivosCompartidos)            

            me_ArchivoCompartido.usuario_act = Session("perlogin")
            me_ArchivoCompartido.ruta_archivo = ConfigurationManager.AppSettings("SharedFiles")

            me_ArchivoCompartido.content_type = md_Funciones.ObtenerContentType(me_ArchivoCompartido.extencion.Trim)

            Dim archivo As Byte() = md_ArchivoCompartido.ObtenerArchivoCompartido(me_ArchivoCompartido)

            Response.Clear()
            Response.Buffer = False
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = me_ArchivoCompartido.content_type
            Response.AddHeader("content-disposition", "attachment;filename=" & me_ArchivoCompartido.nombre_archivo.Replace(",", ""))
            Response.AppendHeader("Content-Length", archivo.Length.ToString())
            Response.BinaryWrite(archivo)
            Response.End()

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region

End Class

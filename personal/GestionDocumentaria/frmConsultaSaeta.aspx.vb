Imports System.Collections.Generic
Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization

Partial Class GestionDocumentaria_frmConsultaSaeta
    Inherits System.Web.UI.Page
#Region "variables"
    Dim codigo_tfu As Integer
    Dim codigo_usu As Integer
    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

#End Region
#Region "eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        codigo_tfu = Request.QueryString("ctf")
        'tipoestudio = Request.QueryString("mod")
        'tipoestudio = "2"
        codigo_usu = Request.QueryString("id")

        Try
            If IsPostBack = False Then
                mt_llenarGrillaDiploma()
                
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        Try
            '-------- ------------------
            'Call mt_respuestaProveedor()

            '---------------------------
            '------------------ conseguir el codigo del evio 
            Dim dtRespuesta As New Data.DataTable()
            dtRespuesta.Columns.AddRange(New Data.DataColumn(2) {New Data.DataColumn("codigoOperacionGrupo"), New Data.DataColumn("egresado"), New Data.DataColumn("mensajeOperacionFirma")})

            Dim dt As New Data.DataTable
            dt = mt_ConsultaDiplomasEnvio()
            If dt.Rows.Count > 0 Then

                Dim codigoOperacionGrupo As Integer
                Dim egresado As String
                Dim codigo_trl As Integer
                Dim estado As String

                For Each row As Data.DataRow In dt.Rows
                    codigoOperacionGrupo = CStr(row("codigoOperacionGrupo"))
                    egresado = CStr(row("egresado"))
                    codigo_trl = CStr(row("codigo_trl"))
                    'hago la consulta con la ws del proveedor
                    estado = mt_ConsultaEstadoProveedor(codigoOperacionGrupo)
                    If estado = "DOCUMENTO FIRMADO EXITOSAMENTE" Then
                        ''Actualiza estado de la diploma
                        Call mt_actualizaEstadoDiploma("DOCUMENTO FIRMADO EXITOSAMENTE")
                        ''finaliza etapa del trámite
                        'Call mt_finalizaEtapatramite(codigo_trl)
                    End If
                    dtRespuesta.Rows.Add(codigoOperacionGrupo, egresado, estado)
                Next

                Me.gvListaDiplomas.DataSource = dtRespuesta
                Me.gvListaDiplomas.DataBind()
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)

                '

            End If
            '------------------------------------------------------
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region
#Region "Metodos y funciones"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Function mt_ConsultaDiplomasEnvio() As Data.DataTable
        Dim dt As New Data.DataTable

        Try
            Dim md_EnvioDiplomasConsulEstadoFirmaEstado As New d_EnvioDiplomasProveedorDetalle
            Dim me_EnvioDiplomaDetalle As New e_EnvioDiplomasProveedorDetalle

            With me_EnvioDiplomaDetalle
                .operacion = "CEF"
                .estadoOperacionFirma = "-1"
            End With
            dt = md_EnvioDiplomasConsulEstadoFirmaEstado.ListarEstadoFirmaDiplomaProveedor(me_EnvioDiplomaDetalle)
            Return dt
        Catch ex As Exception
            dt = Nothing
            Return dt
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function
    Private Sub mt_respuestaProveedor()
        Try
            'crear datatable de respuesta
            Dim dtRespuesta As New Data.DataTable()
            dtRespuesta.Columns.AddRange(New Data.DataColumn(2) {New Data.DataColumn("codigoOperacionGrupo"), New Data.DataColumn("egresado"), New Data.DataColumn("mensajeOperacionFirma")})
            dtRespuesta.Rows.Add(1, "A", "John Hammond")
            dtRespuesta.Rows.Add(2, "B", "Mudassar Khan")
            dtRespuesta.Rows.Add(3, "A", "Suzanne Mathews")
            dtRespuesta.Rows.Add(4, "B", "Robert Schidner")
            Me.gvListaDiplomas.DataSource = dtRespuesta
            Me.gvListaDiplomas.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_llenarGrillaDiploma()
        Dim dt As New Data.DataTable
        dt = mt_ConsultaDiplomasEnvio()
        If dt.Rows.Count > 0 Then
            Me.gvListaDiplomas.DataSource = dt
            Me.gvListaDiplomas.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)
        End If
    End Sub
    Private Function mt_ConsultaEstadoProveedor(ByVal codigoOperacionGrupo As Integer) As String
        Try
            Dim estado As String = String.Empty
            Dim request As HttpWebRequest
            Dim response As HttpWebResponse
            Dim reader As StreamReader
            Dim rawresponse As String

            If Not (ConfigurationManager.AppSettings("CorreoUsatActivo") = 1) Then
                'DEV
                request = DirectCast(WebRequest.Create("http://34.198.218.135:8080/wsplussignertitulousat/FirmaDigital/recibirArchivosGrupoOperacionCliente"), HttpWebRequest)
            Else
                'PRD
                request = DirectCast(WebRequest.Create("http://34.198.218.135:8080/wsplussignertitulousat/FirmaDigital/recibirArchivosGrupoOperacionCliente"), HttpWebRequest)
            End If

            request.Method = "PUT"
            request.ContentType = "application/json"

            Dim putData As String = String.Empty

            putData = "{""codigoOperacionGrupo"": " & codigoOperacionGrupo & "}"

            request.ContentLength = putData.Length

            Dim requestWriter As StreamWriter = New StreamWriter(request.GetRequestStream(), Encoding.Default)
            requestWriter.Write(putData)
            requestWriter.Close()

            response = DirectCast(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())

            rawresponse = reader.ReadToEnd()

            Dim json As String = rawresponse

            Dim jss As New JavaScriptSerializer()
            'Dim dict As Dictionary(Of String, String) = jss.Deserialize(Of Dictionary(Of String, String))(rawresponse)
            Dim eArchivo As New lArchivo
            eArchivo = jss.Deserialize(Of lArchivo)(rawresponse)

            estado = eArchivo.mensajeOperacionGrupo


            'Call mt_ShowMessage(key, MessageType.success)
            Return estado
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
            Return String.Empty
        End Try
    End Function
    Public Sub mt_actualizaEstadoDiploma(ByVal estado As String)
        Try
            Dim md_EnvioDiplomasProveedor As New d_EnvioDiplomasProveedorDetalle()
            Dim dt As New Data.DataTable
            Dim me_envioDiplomaDetalle As New e_EnvioDiplomasProveedorDetalle
            With me_envioDiplomaDetalle
                .operacion = "E"
                .mensajeOperacionFirma = estado
                If .mensajeOperacionFirma = "DOCUMENTO FIRMADO EXITOSAMENTE" Then
                    .estadoOperacionFirma = "5"
                End If
            End With
            dt = md_EnvioDiplomasProveedor.RegistrarEnvioDiplomasProveedorDetalle(me_envioDiplomaDetalle)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub
    Public Function mt_finalizaEtapatramite(ByVal codigo_trl As Integer) As Integer
        Dim codigo_dta As Integer
        Try
            Dim oGradosTitulos As New ClsGradosyTitulos
            Dim dtCodigoDta As New Data.DataTable

            dtCodigoDta = oGradosTitulos.TraeCodigo_dta("TRL", codigo_trl)
            If dtCodigoDta.Rows.Count > 0 Then
                codigo_dta = dtCodigoDta.Rows(0).Item("codigo_dta")
            End If

            clsDocumentacion.ApruebaEtapaTramite(codigo_dta, codigo_tfu, 684, "", "", "")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Function

#End Region


End Class

Public Class lArchivo

#Region "Declaracion de variables"

    Private _estadoOperacionGrupo As Integer
    Private _mensajeOperacionGrupo As String
    
#End Region

#Region "Propiedades"

    Public Property estadoOperacionGrupo() As Integer
        Get
            Return _estadoOperacionGrupo
        End Get
        Set(ByVal value As Integer)
            _estadoOperacionGrupo = value
        End Set
    End Property

    Public Property mensajeOperacionGrupo() As String
        Get
            Return _mensajeOperacionGrupo
        End Get
        Set(ByVal value As String)
            _mensajeOperacionGrupo = value
        End Set
    End Property
#End Region

End Class


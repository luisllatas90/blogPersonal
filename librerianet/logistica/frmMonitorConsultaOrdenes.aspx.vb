Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Web
Imports System.Net
Imports System.Xml
Imports System.Xml.Serialization

Partial Class logistica_frmMonitorConsultaOrdenes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Me.pnlDatosGenerales.Visible = False
                Me.pnlObservaciones.Visible = False
                Me.pnlArchivos.Visible = False '20180910 Enevado
            End If
        Catch ex As Exception
            Response.Write("Error al cargar página: " & ex.Message)
        End Try        
    End Sub

    Protected Sub cmdBuscarOrd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscarOrd.Click
        Try
            Me.gvCabOrden.DataSourceID = SqlDataSource4.ID
            Me.gvCabOrden.DataBind()
        Catch ex As Exception
            Response.Write("Error al buscar: " & ex.Message)
        End Try        
    End Sub

    Protected Sub gvCabOrden_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCabOrden.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvCabOrden','Select$" & e.Row.RowIndex & "');")
            e.Row.Style.Add("cursor", "hand")
        End If
    End Sub

    Protected Sub lnkVerDatos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkVerDatos.Click
        Me.pnlObservaciones.Visible = False
        Me.pnlDatosGenerales.Visible = True
        Me.pnlArchivos.Visible = False
    End Sub

    Protected Sub lnkVerRevisiones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkVerRevisiones.Click
        Me.pnlObservaciones.Visible = True
        Me.pnlDatosGenerales.Visible = False
        Me.pnlArchivos.Visible = False
    End Sub

    Protected Sub rbtBuscarPor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtBuscarPor.SelectedIndexChanged
        Me.txtNro.Text = ""
        If rbtBuscarPor.SelectedValue = "OR" Then
            Me.lblTipo.Visible = True
            Me.cboTipoOrden.Visible = True
            Me.lblNro.Text = "Número de orden / Proveedor"
            Me.txtNro.Width = 100
        ElseIf rbtBuscarPor.SelectedValue = "PE" Then
            Me.lblTipo.Visible = False
            Me.cboTipoOrden.Visible = False
            Me.lblNro.Text = "Número de Pedido"
            Me.txtNro.Width = 30
        End If
    End Sub

    Protected Sub gvCabOrden_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCabOrden.SelectedIndexChanged
        Dim estado As String
        Me.gvDetalleCompra.DataSourceID = SqlDataSource2.ID
        Me.gvDetalleCompra.DataBind()
        Me.pnlDatosGenerales.Visible = True
        Me.pnlObservaciones.Visible = False
        Me.pnlArchivos.Visible = False
        estado = gvCabOrden.DataKeys.Item(Me.gvCabOrden.SelectedIndex).Values("descripcionEstado_Eped").ToString
        Me.hdCodigo_Rco.Value = gvCabOrden.DataKeys(Me.gvCabOrden.SelectedIndex).Values("codigo_Rco").ToString '20180910 Enevado
        If estado = "Aprobado" Then
            Me.lblEstado.Text = "Estado de orden: <font color=green>" & estado & "</font>"
        ElseIf estado = "Rechazado" Then
            Me.lblEstado.Text = "Estado de orden: <font color=red>" & estado & "</font>"
        ElseIf estado = "Generado" Then
            Me.lblEstado.Text = "Estado de orden: <font color=blue>" & estado & "</font>"
        End If

    End Sub

    Protected Sub gvRevisiones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvRevisiones.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(2).Text = "Pendiente" Then
                e.Row.ForeColor = Drawing.Color.Red
            ElseIf e.Row.Cells(2).Text = "Conforme" Then
                e.Row.ForeColor = Drawing.Color.Green
            Else
                e.Row.ForeColor = Drawing.Color.Blue

            End If
        End If
    End Sub

    ''' <summary>
    ''' 20180910 ENevado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lnkVerArchivos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkVerArchivos.Click
        Me.pnlObservaciones.Visible = False
        Me.pnlDatosGenerales.Visible = False
        Me.pnlArchivos.Visible = True
        Try
            'Response.Write("<script>alert('" + Me.hdCodigo_Rco.Value + "')</script>")
            Dim obj As New ClsConectarDatos
            Dim dt As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("USP_listarArchivosCompartidos2", Me.hdCodigo_Rco.Value)
            obj.CerrarConexion()
            Me.gvArchivos.DataSource = dt
            Me.gvArchivos.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    ''' <summary>
    ''' 20180910 ENevado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvArchivos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvArchivos.RowCommand
        Try
            Dim IdFile As String
            Dim index As Integer
            index = CInt(e.CommandArgument)
            IdFile = gvArchivos.DataKeys.Item(index).Values("IdArchivosCompartidos").ToString
            mt_DescargarArchivo(IdFile)
        Catch ex As Exception
            Response.Write("<script type='text/javascript'>alert('" & ex.Message & "');</script>")
        End Try
    End Sub

    ''' <summary>
    ''' 20180911 ENevado
    ''' </summary>
    ''' <param name="IdArchivo"></param>
    ''' <remarks></remarks>
    Private Sub mt_DescargarArchivo(ByVal IdArchivo As String)
        Try
            Dim usuario_session_ As String() = Session("perlogin").ToString.Split(New Char() {"\"c})
            Dim usuario_session As String = usuario_session_(1)

            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Dictionary(Of String, String)

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            'Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.USP_LISTARARCHIVOSCOMPARTIDOS", 2, 6, IdArchivo, "BQ2PQUV34J")
            obj.CerrarConexion()
            Dim resultData As New List(Of Dictionary(Of String, Object))()
            Dim rpta As New Dictionary(Of String, Object)()
            list.Add("IdArchivo", IdArchivo)
            list.Add("Usuario", "USAT\ESAAVEDRA")
            list.Add("Token", "BQ2PQUV34J")
            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            'Dim result As String = wsCloud.PeticionRequestSoap("http://serverqa/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Session("perlogin").ToString)
            Dim result As String = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Session("perlogin").ToString)
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
                Response.Buffer = False
                Response.Charset = ""
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = extencion
                Response.AddHeader("content-disposition", "attachment;filename=" + tb.Rows(0).Item("NombreArchivo"))
                Response.AppendHeader("Content-Length", bytes.Length.ToString())
                Response.BinaryWrite(bytes)
                Response.End()

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 20180911 ENevado
    ''' </summary>
    ''' <param name="cadXml"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function fc_ResultFile(ByVal cadXml As String) As String
        Try
            Dim xError As String()
            Dim nsMgr As XmlNamespaceManager
            Dim xml As XmlDocument = New XmlDocument()
            xml.LoadXml(cadXml)
            nsMgr = New XmlNamespaceManager(xml.NameTable)
            nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim res As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body", nsMgr)
            xError = res.InnerText.Split(":")
            If xError.Length = 2 Then
                Throw New Exception(res.InnerText)
            End If
            Return res.InnerText
        Catch ex As Exception
            Throw ex
        End Try
        
    End Function

End Class

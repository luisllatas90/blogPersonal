Imports System.Collections.Generic
Imports System.IO
Imports System.Xml

Partial Class FrmSubirArchivos
    Inherits System.Web.UI.Page
    Dim cnx As New ClsConectarDatos
    Private ruta As String = "http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx" ' Serverdev
    'Private ruta As String = "http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx" ' Producción

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            Dim dts As New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            cnx.AbrirConexion()
            dts = cnx.TraerDataTable("ConsultaAdjuntoTramite", 50)
            cnx.CerrarConexion()
            Me.gvArchivos.DataSource = dts
            Me.gvArchivos.DataBind()
        End If
    End Sub

    Protected Sub SubirButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.divMensaje.InnerHtml = ""
        Me.divMensaje.Attributes.Remove("Class")
        Dim idtabla As Integer = 13
        Dim codigo_per As Integer = Session("id_per")
        Try
            If Me.files.HasFile Then
                'permite .jpg, .jpeg, .pdf o .rar
                Dim ExtensionesPermitidas As String() = {".jpg", ".jpeg", ".pdf", ".rar"}
                Dim valida As Integer = 0
                Dim Archivos As HttpFileCollection = Request.Files
                For j As Integer = 0 To Archivos.Count - 1
                    For i As Integer = 0 To ExtensionesPermitidas.Length - 1
                        If Path.GetExtension(Archivos(j).FileName) = ExtensionesPermitidas(i) Then
                            'Response.Write(Path.GetExtension(Archivos(j).FileName) + " - " + ExtensionesPermitidas(i) + "<br>")
                            valida = valida + 1
                        End If
                    Next
                Next

                If valida = Archivos.Count Then ' si todos los archivos tienen formato permitido subimos los archivos, sino NO.
                    Dim respuesta As String = ""
                    For i As Integer = 0 To Archivos.Count - 1
                        respuesta = SubirArchivo(idtabla, 0, Archivos(i), 0, codigo_per)
                        If respuesta.Contains("Registro procesado correctamente") Then
                            Me.divMensaje.InnerHtml = Me.divMensaje.InnerHtml + "<span style='color:green'><i class='ion-checkmark-round'></i> Archivo Adjuntado Correctamente : " + Archivos(i).FileName.ToString + " </span><br>"
                        Else
                            Me.divMensaje.InnerHtml = Me.divMensaje.InnerHtml + "<span style='color:red'><i class='ion-close-round'></i> No se Pudo Adjuntar Archivo : " + Archivos(i).FileName.ToString + "</span><br> "
                        End If
                    Next
                    Me.divMensaje.Attributes.Add("Class", "alert alert-success")
                Else
                    Me.divMensaje.InnerHtml = "Solo se aceptan tipos de archivo con extension '.jpg','.jpeg','.pdf','.rar'"
                    Me.divMensaje.Attributes.Add("Class", "alert alert-danger")
                End If
            Else
                Me.divMensaje.InnerHtml = "Seleccione al menos un archivo para Adjuntar."
                Me.divMensaje.Attributes.Add("Class", "alert alert-danger")
            End If
        Catch ex As Exception
            Me.divMensaje.InnerHtml = "Los Archivos no se pudieron Subir: " & ex.Message
            Me.divMensaje.Attributes.Add("Class", "alert alert-danger")

        End Try
    End Sub

    Function SubirArchivo(ByVal id_tabla As Integer, ByVal nro_transaccion As String, ByVal ArchivoaSubir As HttpPostedFile, ByVal tipo As String, ByVal usuario_per As Integer) As String
        Try
            Dim codigo_tablaArchivo As String = id_tabla ' ID de tablaArchivo
            Dim archivo As HttpPostedFile = ArchivoaSubir
            Dim nro_operacion As String = ""
            Dim id_tablaProviene As String = nro_transaccion

            Dim Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim Usuario As String = Session("perlogin").ToString
            Dim Input(archivo.ContentLength) As Byte

            Dim b As New BinaryReader(archivo.InputStream)
            Dim binData As Byte() = b.ReadBytes(archivo.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)

            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)

            Dim nombre_archivo As String = System.IO.Path.GetFileName(archivo.FileName.Replace("&", "_").Replace("'", "_").Replace("*", "_"))

            list.Add("Fecha", Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(archivo.FileName))
            list.Add("Nombre", nombre_archivo)
            list.Add("TransaccionId", id_tablaProviene)
            list.Add("TablaId", codigo_tablaArchivo)
            list.Add("NroOperacion", nro_operacion)
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Session("perlogin").ToString)
            Return result
        Catch ex As Exception
            Dim Data1 As New Dictionary(Of String, Object)()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim list As New List(Of Dictionary(Of String, Object))()
            Data1.Add("rpta", "1 - SUBIR ARCHIVO")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)
            Return JSONresult
        End Try
    End Function


    Protected Sub Descargar_click(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim seleccion As GridViewRow
        seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
        Response.Write("<script>alert('" + Me.gvArchivos.DataKeys(seleccion.RowIndex).Values("ID").ToString + "')</script>")
    End Sub


    Private Sub DescargarArchivo(ByVal idarchivo As String, ByVal token As String)
        Dim JSONresult As String = ""
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim resultData As New List(Of Dictionary(Of String, Object))()
        Try

            If Not Session("perlogin") Is Nothing Then

                Dim cif As New ClsCRM
                Dim wsCloud As New ClsArchivosCompartidosV2
                Dim list As New Dictionary(Of String, String)
                Dim Usuario As String = Session("perlogin")
                list.Add("IdArchivo", idarchivo)
                list.Add("Usuario", Usuario)
                list.Add("Token", token)

                Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
                Dim result As String = ""

                result = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", Usuario)
                Dim imagen As String = ResultFile(result)

                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("File", imagen.ToString)
                Data.Add("result", result.ToString)
                Data.Add("Param1", idarchivo)
                Data.Add("Status", "OK")
                Data.Add("Message", "La sessión del usuario ha finalizado<h4>Ir a Menu Principal/Personal/Boletas de Pago/Ver Boleta de pago </h4>")
                resultData.Add(Data)

            Else
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("Status", "ERROR")
                Data.Add("Message", "La sessión del usuario ha finalizado<h4>Ir a Menu Principal/Personal/Boletas de Pago/Ver Boleta de pago </h4>")
                Data.Add("File", "")
                Data.Add("Nombre", "")
                Data.Add("Extencion", "")
                Data.Add("Path", "")

                Data.Add("Per", Session("perlogin").ToString)
                resultData.Add(Data)
            End If
            JSONresult = serializer.Serialize(resultData)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", "ERROR")
            Data.Add("Message", ex.Message & " " & ex.StackTrace)
            Data.Add("PerLogin", Session("perlogin"))
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub

    Function ResultFile(ByVal cadXml As String) As String
        Dim nsMgr As XmlNamespaceManager
        Dim xml As XmlDocument = New XmlDocument()
        xml.LoadXml(cadXml)
        nsMgr = New XmlNamespaceManager(xml.NameTable)
        nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
        Dim res As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body", nsMgr)
        '  Dim mNombre = xml.ReadElementString("nombre")
        Return res.InnerText
        '   Response.Write("dd" + res.InnerText)
    End Function


    Protected Sub gvArchivos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvArchivos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim myLink As HyperLink = New HyperLink()
            myLink.NavigateUrl = "javascript:void(0)"
            myLink.Text = "Descargar"
            myLink.CssClass = "btn btn-primary"
            myLink.Attributes.Add("onclick", "DescargarArchivo('" & Me.gvArchivos.DataKeys(e.Row.RowIndex).Values("ID").ToString & "','" & Me.gvArchivos.DataKeys(e.Row.RowIndex).Values("token").ToString & "')")

            e.Row.Cells(3).Controls.Add(myLink)
        End If

    End Sub
End Class

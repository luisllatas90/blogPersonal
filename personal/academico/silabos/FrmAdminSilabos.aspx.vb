Imports System.Collections.Generic
Imports System.IO

Partial Class academico_silabos_FrmAdminSilabos
    Inherits System.Web.UI.Page
    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles") '"http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"
    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If (Session("id_per") Is Nothing) Then
        '    Response.Redirect("../../../sinacceso.html")
        'End If

        If IsPostBack = False Then
            Me.HdCronograma.Value = True
            CargaCiclo()
            CargaCarreraProfesional()
            ValidaCronograma()
        End If
    End Sub

    Private Sub CargaCiclo()
        Dim dt As New Data.DataTable
        Dim cls As New ClsConectarDatos
        cls.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            cls.AbrirConexion()
            dt = cls.TraerDataTable("ConsultarCicloAcademico", "TO", 0)
            cls.CerrarConexion()

            Me.cboSemestre.DataSource = dt
            Me.cboSemestre.DataTextField = "descripcion_cac"
            Me.cboSemestre.DataValueField = "codigo_cac"
            Me.cboSemestre.DataBind()
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.ToString, MessageType.Error)
        End Try
    End Sub

    Private Sub CargaCarreraProfesional()
        Dim dt As New Data.DataTable
        Dim cls As New ClsConectarDatos
        cls.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            cls.AbrirConexion()
            dt = cls.TraerDataTable("EVE_ConsultarCarreraProfesional", Request.QueryString("mod"), Request.QueryString("ctf"), Session("id_per"))
            cls.CerrarConexion()

            Me.cboCarrera.DataSource = dt
            Me.cboCarrera.DataTextField = "nombre_cpf"
            Me.cboCarrera.DataValueField = "codigo_cpf"
            Me.cboCarrera.DataBind()
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.ToString, MessageType.Error)
        End Try
    End Sub

    Private Sub ValidaCronograma()
        Dim dt As New Data.DataTable
        Dim cls As New ClsConectarDatos
        Dim f1 As Date
        Dim f2 As Date
        cls.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            cls.AbrirConexion()
            dt = cls.TraerDataTable("ACAD_ConsultarCronograma", "SI", Me.cboSemestre.SelectedValue, Request.QueryString("mod"))
            cls.CerrarConexion()

            If dt.Rows.Count = 0 Then
                'ShowMessage("No puede registrar silabos por encontrarse fuera de fecha, coordinar con Dirección académica", MessageType.Error)
                Me.lblMensaje.Text = "No puede registrar silabos por encontrarse fuera de fecha, coordinar con Dirección académica"
                HdCronograma.Value = False
                lblMensaje.Style.Item("display") = "block"

                lblMensaje.CssClass = "form-control alert-danger"
            Else
                f1 = dt.Rows(0).Item("fechaIni_Cro")
                f2 = dt.Rows(0).Item("fechaFin_Cro")

                Me.lblMensaje.Text = "Fechas definidas para subir silabos: " & f1.ToString("dd/MM/yyyy") & "-" & f2.ToString("dd/MM/yyyy")
                HdCronograma.Value = True
                lblMensaje.Style.Item("display") = "block"
                lblMensaje.CssClass = "form-control alert-info"
                'ShowMessage("Fechas definidas para subir silabos: " & f1.ToString("dd/MM/yyyy") & "-" & f1.ToString("dd/MM/yyyy"), MessageType.Info)
            End If
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.ToString, MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Buscar()
    End Sub

    Private Sub Buscar()
        Dim dt As New Data.DataTable
        Dim cls As New ClsConectarDatos
        cls.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            cls.AbrirConexion()
            dt = cls.TraerDataTable("ACAD_ConsultarCursoProgramadoSilabo", Me.cboCarrera.SelectedValue, Me.cboSemestre.SelectedValue, "", 0)
            cls.CerrarConexion()
            lblnum.Value = dt.Rows.Count
            HdCiclo.Value = cboSemestre.Items(cboSemestre.SelectedIndex).Text


            Me.gvCursos.DataSource = dt
            Me.gvCursos.DataBind()
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.ToString, MessageType.Error)
        End Try
    End Sub

    Protected Sub gvCursos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCursos.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            If (e.CommandName = "descargar") Then

                Dim codigo_cup As Integer = CInt(gvCursos.DataKeys(index).Values("codigo_cup").ToString)
                Dim idTransa As Integer = CInt(gvCursos.DataKeys(index).Values("codigo_dis").ToString)
                Dim var As Integer = CInt(gvCursos.DataKeys(index).Values("IdArchivo_Anexo").ToString)

                ' Response.Write(codigo_cup & "<br>")
                'Response.Write(idTransa & "<br>")
                'Response.Write(var & "<br>")

                'If var = 0 Then Throw New Exception("¡ Este silabo no se encuentra disponible !")
                'Page.RegisterStartupScript("BusyBox", "<script>fc_ocultarBusy();</script>")
                'Response.Write("<script>fc_OcultarBussy();</script>")
                mt_DescargarArchivo(codigo_cup)
                'RegisterStartupScript("", "<script>fc_OcultarBussy();</script>")
                'Page.ClientScript.RegisterStartupScript(Me.GetType, "script", "return fc_OcultarBussy()")
                'btnListar_Click(Nothing, Nothing)
                ShowMessage("¡ Se ha descargado el silabo correctamente !", MessageType.Success)

            ElseIf (e.CommandName = "Agregar") Then
                Dim codigo_cup As Integer = CInt(gvCursos.DataKeys(index).Values(0).ToString)
                Dim nombre_cur As String = gvCursos.Rows(index).Cells(4).Text.ToString()
                Dim grupo As String = gvCursos.Rows(index).Cells(7).Text.ToString()

                HdCup.Value = codigo_cup
                Me.divTit.InnerHtml = "Subir Sílabo del curso: " & nombre_cur & "[" & grupo & "]"

                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script>openModal('modalArchivo');</script>")
                RefreshGrid()
            ElseIf (e.CommandName = "Eliminar") Then
                Dim codigo_cup As Integer = CInt(gvCursos.DataKeys(index).Values("codigo_cup").ToString)
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                obj.Ejecutar("AgregarSilabo", "E", codigo_cup, Session("id_per"))
                obj.CerrarConexion()
                obj = Nothing

                ShowMessage("¡ Se ha eliminado el silabo correctamente !", MessageType.Success)
                Buscar()
                RefreshGrid()
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " -- " & ex.StackTrace)
        End Try


        'CommandName="Descargar" 
    End Sub

    Protected Sub gvCursos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCursos.RowDataBound
        Try



            If (e.Row.RowType.ToString = "DataRow") Then
                Dim lnkVer As LinkButton = CType(e.Row.FindControl("lnkVer"), LinkButton)
                Dim lnkEliminar As LinkButton = CType(e.Row.FindControl("lnkEliminar"), LinkButton)
                Dim lnkAgregar As LinkButton = CType(e.Row.FindControl("lnkAgregar"), LinkButton)
                Dim lnkCancelar As LinkButton = CType(e.Row.FindControl("lnkCancelar"), LinkButton)



                lnkVer.Visible = False
                lnkEliminar.Visible = False
                lnkAgregar.Visible = False
                lnkCancelar.visible = False


                If HdCronograma.Value = True Then   'Cronograma activo



                    If e.Row.Cells(11).Text.Length = 6 Then

                        Dim myLink As HyperLink = New HyperLink()
                        myLink.NavigateUrl = "javascript:void(0)"
                        myLink.Text = "Descargar"
                        myLink.CssClass = "btn btn-primary btn-xs"
                        myLink.ImageUrl = "img/search.png"
                        myLink.Attributes.Add("onclick", "DescargarArchivo('" & e.Row.Cells(12).Text & "')")
                        e.Row.Cells(10).Controls.Clear()
                        e.Row.Cells(10).Controls.Add(myLink)
                    ElseIf e.Row.Cells(11).Text = "No Disponible" Then

                        lnkEliminar.Visible = False
                        lnkAgregar.Visible = True
                        e.Row.Cells(10).Text = "<label class='label label-warning'>No Disponible</label>"

                    Else

                        Dim curFile As String = Server.MapPath("../../../silabos/" & e.Row.Cells(11).Text)
               
                        If (File.Exists(curFile)) Then
                            Dim myLink As HyperLink = New HyperLink()
                            myLink.NavigateUrl = "../../../silabos/" & e.Row.Cells(11).Text
                            myLink.Text = "Descargar"
                            myLink.Target = "_blank"
                            myLink.CssClass = "btn btn-default btn-xs"
                            myLink.ImageUrl = "img/search.png"
                            'myLink.Attributes.Add("onclick", "DescargarArchivo('" & e.Row.Cells(13).Text & "')")
                            e.Row.Cells(10).Controls.Clear()
                            e.Row.Cells(10).Controls.Add(myLink)
                            lnkEliminar.Visible = True
                        Else
                            lnkEliminar.Visible = False
                            lnkAgregar.Visible = True
                            e.Row.Cells(10).Text = "<label class='label label-danger'>No existe archivo</label>"
                        End If


                    End If


                Else
                    lnkCancelar.Visible = True
                    e.Row.Cells(10).Text = ""



                    'Cronograma vencido
                    'e.Row.Cells(9).Text = "<img src='img/cancel.png' alt='Fuera de fecha' />"
                End If


            End If


            If Me.lblnum.Value > 0 Then
                e.Row.Cells(0).Visible = False
                e.Row.Cells(9).Visible = False
                ' e.Row.Cells(10).Visible = False
                e.Row.Cells(11).Visible = False
                e.Row.Cells(12).Visible = False
                e.Row.Cells(13).Visible = False
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cboSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSemestre.SelectedIndexChanged
        ValidaCronograma()
        Buscar()
        RefreshGrid()
    End Sub



    Protected Sub ImageAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub ImageCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If FileUploadControl.HasFile Then

            Try

                'If FileUploadControl.PostedFile.ContentType = "zip" Then
                If FileUploadControl.PostedFile.ContentType = "application/x-zip-compressed" Then
                    'Response.Write(FileUploadControl.PostedFile.ContentLength)
                    'If FileUploadControl.PostedFile.ContentLength < (102400 * 2) Then
                    If FileUploadControl.PostedFile.ContentLength < 300000 Then

                        ' Dim filename As String = Path.GetFileName(FileUploadControl.FileName)
                        'Response.Write(Server.MapPath("~/../silabos/2019-II/") & filename)

                        FileUploadControl.SaveAs(Server.MapPath("~/../silabos/" & HdCiclo.Value.ToString & "/") & HdCup.Value.ToString & ".zip")

                        Dim obj As New ClsConectarDatos
                        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                        obj.AbrirConexion()
                        obj.Ejecutar("AgregarSilabo", "A", HdCup.Value, Session("id_per"))
                        obj.CerrarConexion()
                        obj = Nothing


                        'StatusLabel.Text = "Upload status: File uploaded!"
                        Buscar()
                        RefreshGrid()
                        'Response.Write("Upload status: File uploaded!")
                        'mt_Mensaje("success", "Arhivo adjuntado con éxito")
                        ShowMessage("¡ Arhivo adjuntado con éxito !", MessageType.Success)

                    Else
                        'StatusLabel.Text = "Upload status: The file has to be less than 100 kb!"
                        'Response.Write("Upload status: The file has to be less than 200 kb!")
                        'mt_Mensaje("warning", "El archivo supera los 300 Kb")
                        ShowMessage("¡ El archivo supera los 300 Kb !", MessageType.Warning)

                    End If
                Else
                    'StatusLabel.Text = "Upload status: Only JPEG files are accepted!"
                    'Response.Write("Upload status: Solo se aceptan archivos zip!")
                    ' mt_Mensaje("warning", "Solo se aceptan archivos zip")
                    ShowMessage("¡ Solo se aceptan archivos zip !", MessageType.Warning)
                End If

            Catch ex As Exception
                'StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " & ex.Message
                'Response.Write("Upload status: The file could not be uploaded. The following error occured: " & ex.Message)
                'mt_Mensaje("danger", "Error al adjuntar archivo")
                ShowMessage("¡ Error al adjuntar archivo !", MessageType.Error)
            End Try
        End If

        HdCup.Value = ""
        RefreshGrid()
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        RefreshGrid()
    End Sub



    Private Sub mt_DescargarArchivo(ByVal IdArchivo As Long)
        Try
            Dim idTabla As Integer = 22
            Dim token As String = "YAXVXFQACX"
            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Dictionary(Of String, String)
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim usuario As String = Session("perlogin")

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, idTabla, IdArchivo, token)
            obj.CerrarConexion()
            If tb.Rows.Count = 0 Then Throw New Exception("¡ Archivo no encontrado !")

            list.Add("IdArchivo", tb.Rows(0).Item("IdArchivo").ToString)
            list.Add("Usuario", usuario)
            list.Add("Token", token)

            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", usuario)
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
                Response.Clear()
                Response.Buffer = False
                Response.Charset = ""
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = extencion
                Response.AddHeader("content-disposition", "attachment;filename=" & tb.Rows(0).Item("NombreArchivo").ToString.Replace(",", ""))
                Response.AppendHeader("Content-Length", bytes.Length.ToString())
                Response.BinaryWrite(bytes)
                Response.End()
            End If
            'Response.Write(envelope)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
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

    Private Sub RefreshGrid()

        For Each _Row As GridViewRow In gvCursos.Rows
            gvCursos_RowDataBound(gvCursos, New GridViewRowEventArgs(_Row))
        Next

    End Sub
End Class

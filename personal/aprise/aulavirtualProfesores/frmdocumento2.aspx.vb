
Partial Class aulavirtual_frmdocumento2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.FechaIni.Attributes.Add("OnKeyDown", "return false;")
            Me.FechaFin.Attributes.Add("OnKeyDown", "return false;")
            Me.FechaIni.Text = CDate(Request.QueryString("ficursovirtual")).ToShortDateString   'Now.ToShortDateString '
            Me.HoraIni.SelectedValue = Now.Hour
            Me.FechaFin.Text = CDate(Request.QueryString("ffcursovirtual")).ToShortDateString 'DateAdd(DateInterval.Day, 1, Now).ToShortDateString '
            Me.HoraFin.SelectedValue = Now.Hour

            If Request.QueryString("accion") = "modificardocumento" Then
                Me.FileArchivo.Visible = False
                Me.LblUbicacion.Visible = False
                Me.ValidarSubir.EnableClientScript = False
                Me.ValidarSubir.Enabled = False
                Me.LblPublicacion.Visible = False
                Me.DDLPermiso.Visible = False

                Dim iddocumento As String
                iddocumento = Request.QueryString("iddocumento")

                Dim Datos As Data.DataTable
                Dim ObjCMUSAT As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
                Datos = ObjCMUSAT.TraerDataTable("ConsultarDocumento", "3", iddocumento, "", "", "")
                ObjCMUSAT = Nothing

                Me.HidenArchivo.Value = Datos.Rows(0).Item(2).ToString
                Me.TxtNombre.Text = Datos.Rows(0).Item(3).ToString
                Me.FechaIni.Text = CDate(Datos.Rows(0).Item(5).ToString).ToShortDateString
                Me.FechaFin.Text = CDate(Datos.Rows(0).Item(6).ToString).ToShortDateString
                Me.HoraIni.SelectedValue = Mid(Datos.Rows(0).Item(5).ToString, 12, 2)
                Me.HoraFin.SelectedValue = Mid(Datos.Rows(0).Item(6).ToString, 12, 2)
                Me.MinIni.SelectedValue = Mid(Datos.Rows(0).Item(5).ToString, 15, 2)
                Me.MinFin.SelectedValue = Mid(Datos.Rows(0).Item(6).ToString, 15, 2)
                Me.TxtComentario.Text = Datos.Rows(0).Item(7).ToString
                Me.DDLPermiso.SelectedValue = Datos.Rows(0).Item(13)
                Datos = Nothing
            End If
        End If
    End Sub
    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim archivo, usuario, icursovirtual, ruta, fileExtension As String
        Dim valordevuelto, iddocumento, refcodigo_ccv As Integer
        Dim fileOk As Boolean
        Dim finicio, ffin As DateTime

        Dim ObjCMDUSAT As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)

        usuario = Request.QueryString("idusuario")
        icursovirtual = Request.QueryString("idcursovirtual")
        refcodigo_ccv = Request.QueryString("refcodigo_ccv")

        finicio = CDate(Me.FechaIni.Text & " " & Me.HoraIni.Text & ":" & Me.MinIni.Text & ":00")
        ffin = CDate(Me.FechaFin.Text & " " & Me.HoraFin.Text & ":" & Me.MinFin.Text & ":00")

        '#########################################################################
        'Verifico que sea diferente de modificar para agregar el documento.
        '#########################################################################
        If Request.QueryString("accion") <> "modificardocumento" Then
            ruta = "T:\documentos aula virtual\archivoscv\" & icursovirtual.ToString & "\documentos\"
            'ruta = "C:\Inetpub\wwwroot\campusvirtual\archivoscv\" & icursovirtual.ToString & "\documentos\"
		Dim carpetaDestino As New System.IO.DirectoryInfo(ruta)
            	If carpetaDestino.Exists = False Then carpetaDestino.Create()

            If Me.FileArchivo.HasFile = True Then
                fileExtension = System.IO.Path.GetExtension(FileArchivo.FileName).ToLower()

                '##########################################################################
                'Aki colocamos el array de los archivos NO permitidos
                '##########################################################################
                Dim allowedExtensions As String() = {".exe", ".dat", ".sys", ".dll", ".js", ".asp", ".php", ".aspx"}
                For i As Integer = 0 To allowedExtensions.Length - 1
                    If fileExtension <> allowedExtensions(i) Then
                        fileOk = True
                    Else
                        Me.LblMensaje.Text = "Archivo no permitido"
                        Exit Sub
                    End If
                Next
                If fileOk = True Then
                    Me.LblMensaje.Text = ""
                    '######################################################################
                    ' Capturamos el nombre de usuario
                    '######################################################################
                    archivo = Replace(usuario, "\", "")
                    If Left(archivo, 4) = "USAT" Then
                        archivo = Right(archivo, Len(archivo) - 4)
                    End If
                    archivo = archivo & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & fileExtension
                    Try
                        '###########################################################################
                        ' Guardamos el archivo y luego agregamos el documento en la base de datos
                        '###########################################################################
                        FileArchivo.PostedFile.SaveAs(ruta & archivo)
                        ObjCMDUSAT.IniciarTransaccion()
                        valordevuelto = ObjCMDUSAT.Ejecutar("DI_AgregarDocumento", archivo, Me.TxtNombre.Text, usuario, finicio, ffin, Me.TxtComentario.Text, icursovirtual, Me.DDLPermiso.SelectedValue, refcodigo_ccv, 0)
                        ObjCMDUSAT.TerminarTransaccion()
                        If valordevuelto > 0 Then
                            ObjCMDUSAT = Nothing
                            If Me.DDLPermiso.SelectedValue = 2 Then
                                Dim PagRedirec As String
                                PagRedirec = "frmagregarusuariosrecurso.asp?accion=agregarpermisos&nombretabla=documento&tipodoc=A&idtabla=" & valordevuelto & "&" & Replace(Page.ClientQueryString, "&accion=modificardocumento", "", , , CompareMethod.Text)
                                Response.Redirect("../../personal/aulavirtual/usuario/" & PagRedirec)
                            Else
                                'Response.Write("<script>window.opener.location='../../personal/aulavirtual/moodleusat/tematicacurso.asp'; window.close();</script>")
                                Response.Write("<script>window.opener.location.reload(); window.close();</script>")
                            End If
                        End If
                    Catch ex As Exception
                        ObjCMDUSAT.AbortarTransaccion()
                        Me.LblMensaje.Text = "Ocurrió un Error al Registrar los datos. " & ex.Message
                        ObjCMDUSAT = Nothing
                    End Try
                End If
            Else
                Me.LblMensaje.Text = "No ha subido ningun archivo"
            End If
        Else
            Try
                '#########################################################################
                'Modifico los datos del documento, mas no el documento en si.
                '#########################################################################
                Me.LblMensaje.Text = ""
                iddocumento = Request.QueryString("iddocumento")
                ObjCMDUSAT.IniciarTransaccion()
                ObjCMDUSAT.Ejecutar("DI_ModificarDocumento", iddocumento, Me.HidenArchivo.Value, Me.TxtNombre.Text, usuario, finicio, ffin, Me.TxtComentario.Text)
                ObjCMDUSAT.TerminarTransaccion()
                Response.Write("<script>window.opener.location.reload(); window.close();</script>")
            Catch ex As Exception
                ObjCMDUSAT.AbortarTransaccion()
                Me.LblMensaje.Text = "Ocurrió un Error al Registrar los datos."
            End Try
            ObjCMDUSAT = Nothing
        End If

    End Sub
End Class


Partial Class frmpaginaweb
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.hdidcursovirtual.Value = Request.QueryString("idcursovirtual").ToString
            Me.hdidusuario.Value = Request.QueryString("idusuario").ToString
            Me.TxtComentario.Attributes.Add("onKeyUp", "ContarTextArea(TxtComentario,8000,lblcontador)")
            Me.TxtNombre.Focus()

            If Request.QueryString("accion") = "modificardocumento" Then
                Dim iddocumento As String
                iddocumento = Request.QueryString("iddocumento")               

                Dim Datos As Data.DataTable
                Dim ObjCMUSAT As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxCMUSAT").ConnectionString)
                Datos = ObjCMUSAT.TraerDataTable("ConsultarDocumento", "3", iddocumento, "", "", "")
                ObjCMUSAT = Nothing

                Me.TxtNombre.Text = Datos.Rows(0).Item("titulodocumento").ToString
                Me.TxtComentario.Text = Replace(Datos.Rows(0).Item("paginaweb").ToString, "<br>", Chr(13))
                Datos.Dispose()
            End If
        End If
    End Sub
    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxCMUSAT").ConnectionString)

        Try
            Dim finicio, ffin As String
            Dim iddocumento As String = "0"

            'finicio = CDate(Me.txtFechaInicio.Text & " " & Me.HoraIni.Text & ":" & Me.MinIni.Text & ":00")
            'ffin = CDate(Me.txtFechaFin.Text & " " & Me.HoraFin.Text & ":" & Me.MinFin.Text & ":00")
            finicio = Now
            ffin = DateAdd(DateInterval.Month, 5, Now)
            iddocumento = Request.QueryString("iddocumento")

            If InStr(Me.TxtComentario.Text, "<script") > 0 Then
                Obj = Nothing
                Me.LblMensaje.Text = "No está permitido contenido JavaScript"
                Exit Sub
            End If

		if request.querystring("accion")="agregardocumento" then
			iddocumento=""
		end if

            'Grabar información
            Obj.IniciarTransaccion()

            If iddocumento <> "" Then
                Obj.Ejecutar("DI_AgregarPaginaWeb", "M", iddocumento, Me.TxtNombre.Text.Trim, finicio, ffin, Me.hdidcursovirtual.Value, Me.hdidusuario.Value, Replace(Me.TxtComentario.Text.Trim, Chr(13), "<br>"), Request.QueryString("refiddocumento"),Request.QueryString("refcodigo_ccv"), 0)
            Else
                iddocumento = Obj.Ejecutar("DI_AgregarPaginaWeb", "A", 0, Me.TxtNombre.Text.Trim, finicio, ffin, Me.hdidcursovirtual.Value, Me.hdidusuario.Value, Replace(Me.TxtComentario.Text.Trim, Chr(13), "<br>"), Request.QueryString("refiddocumento"),Request.QueryString("refcodigo_ccv"), 0)
            End If
            Obj.TerminarTransaccion()

            Page.RegisterStartupScript("GrabadoWeb", "<script>alert('Se han guardado los datos correctamente\n A continuación se visualizará la página web creada');location.href='vistapaginaweb.aspx?iddocumento=" + iddocumento + "'</script>")

            Obj = Nothing
        Catch ex As Exception
            Me.LblMensaje.Text = "Error: " & ex.Message
            Obj = Nothing
        End Try
    End Sub
End Class

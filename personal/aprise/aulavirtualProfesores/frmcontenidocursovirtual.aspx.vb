
Partial Class aulavirtual_frmcontenidocursovirtual
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.FechaIni.Attributes.Add("OnKeyDown", "return false;")
            Me.FechaFin.Attributes.Add("OnKeyDown", "return false;")
            Me.TxtOrden.Attributes.Add("onKeyPress", "validarnumero()")
            Me.TxtComentario.Attributes.Add("onkeypress", "ContarTextArea(this);")

            Me.FechaIni.Text = CDate(Request.QueryString("ficursovirtual")).ToShortDateString   'Now.ToShortDateString '
            Me.HoraIni.SelectedValue = Now.Hour
            Me.FechaFin.Text = CDate(Request.QueryString("ffcursovirtual")).ToShortDateString 'DateAdd(DateInterval.Day, 1, Now).ToShortDateString '
            Me.HoraFin.SelectedValue = Now.Hour

            If Request.QueryString("accion") = "modificarcontenidocursovirtual" Then
                Dim codigo_ccv As String
                codigo_ccv = Request.QueryString("codigo_ccv")
                Me.LblPublicacion.Visible = False
                Me.DDLPermiso.Visible = False

                If Request.QueryString("mododesarrollo_cv") = "I" Then
                    Me.TxtNombre.Enabled = False
                End If

                Dim Datos As Data.DataTable
                Dim ObjCMUSAT As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
                Datos = ObjCMUSAT.TraerDataTable("Consultarcontenidocursovirtual", 1, codigo_ccv, "", "")
                ObjCMUSAT = Nothing

                Me.TxtOrden.Text = Datos.Rows(0).Item("orden_ccv").ToString
                Me.TxtNombre.Text = Datos.Rows(0).Item("titulo_ccv").ToString
                Me.FechaIni.Text = CDate(Datos.Rows(0).Item("fechaini_ccv").ToString).ToShortDateString
                Me.FechaFin.Text = CDate(Datos.Rows(0).Item("fechafin_ccv").ToString).ToShortDateString
                Me.HoraIni.SelectedValue = Mid(Datos.Rows(0).Item("fechaini_ccv").ToString, 12, 2)
                Me.HoraFin.SelectedValue = Mid(Datos.Rows(0).Item("fechafin_ccv").ToString, 12, 2)
                Me.MinIni.SelectedValue = Mid(Datos.Rows(0).Item("fechaini_ccv").ToString, 15, 2)
                Me.MinFin.SelectedValue = Mid(Datos.Rows(0).Item("fechafin_ccv").ToString, 15, 2)
                Me.TxtComentario.Text = Datos.Rows(0).Item("descripcion_ccv").ToString
                Me.DDLPermiso.SelectedValue = Datos.Rows(0).Item("idtipopublicacion")
                Datos = Nothing
            End If
        End If
    End Sub
    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim usuario, icursovirtual As String
        Dim valordevuelto, codigo_ccv, refcodigo_ccv As Integer
        Dim finicio, ffin As DateTime

        Dim ObjCMDUSAT As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)

        usuario = Request.QueryString("idusuario")
        icursovirtual = Request.QueryString("idcursovirtual")
        refcodigo_ccv = Request.QueryString("refcodigo_ccv")

        finicio = CDate(Me.FechaIni.Text & " " & Me.HoraIni.Text & ":" & Me.MinIni.Text & ":00")
        ffin = CDate(Me.FechaFin.Text & " " & Me.HoraFin.Text & ":" & Me.MinFin.Text & ":00")

        '#########################################################################
        'Verifico que sea diferente de modificar para agregar el contenidocursovirtual.
        '#########################################################################
        If Request.QueryString("accion") <> "modificarcontenidocursovirtual" Then

            Me.LblMensaje.Text = ""
            Try
                '###########################################################################
                ' Guardamos el archivo y luego agregamos el contenidocursovirtual en la base de datos
                '###########################################################################
                ObjCMDUSAT.IniciarTransaccion()
                valordevuelto = ObjCMDUSAT.Ejecutar("DI_Agregarcontenidocursovirtual", icursovirtual, finicio, ffin, Me.TxtNombre.Text, Me.TxtComentario.Text, refcodigo_ccv, usuario, Me.TxtOrden.Text, Me.DDLPermiso.SelectedValue, 0)
                ObjCMDUSAT.TerminarTransaccion()
                ObjCMDUSAT = Nothing
                If valordevuelto > 0 Then
                    ObjCMDUSAT = Nothing
                    If Me.DDLPermiso.SelectedValue = 2 Then
                        Dim PagRedirec As String
                        PagRedirec = "frmagregarusuariosrecurso.asp?accion=agregarpermisos&nombretabla=contenidocursovirtual&tipodoc=A&idtabla=" & valordevuelto & "&" & Replace(Page.ClientQueryString, "&accion=modificarcontenidocursovirtual", "", , , CompareMethod.Text)
                        Response.Redirect("../../personal/aulavirtual/usuario/" & PagRedirec)
                    Else
                        Response.Write("<script>window.opener.location.reload(); window.close();</script>")
                    End If
                End If
            Catch ex As Exception
                ObjCMDUSAT.AbortarTransaccion()
                Me.LblMensaje.Text = "Ocurrió un Error al Registrar el tema." & ex.Message
                ObjCMDUSAT = Nothing
            End Try
        Else
            Try
                '#########################################################################
                'Modifico los datos del contenidocursovirtual, mas no el contenidocursovirtual en si.
                '#########################################################################
                Me.LblMensaje.Text = ""
                codigo_ccv = Request.QueryString("codigo_ccv")
                ObjCMDUSAT.IniciarTransaccion()
                ObjCMDUSAT.Ejecutar("DI_Modificarcontenidocursovirtual", codigo_ccv, finicio, ffin, Me.TxtNombre.Text, Me.TxtComentario.Text, usuario, Me.txtOrden.text)
                ObjCMDUSAT.TerminarTransaccion()
                ''../../personal/aulavirtual/moodleusat/tematicacurso.asp
                ObjCMDUSAT = Nothing
                Response.Write("<script>window.opener.location.reload();window.close()</script>")
            Catch ex As Exception
                ObjCMDUSAT.AbortarTransaccion()
                Me.LblMensaje.Text = "Ocurrió un Error al Registrar el tema."
                ObjCMDUSAT = Nothing
            End Try
        End If

    End Sub
End Class

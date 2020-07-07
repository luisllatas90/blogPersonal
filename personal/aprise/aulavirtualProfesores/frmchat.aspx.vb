
Partial Class aulavirtual_frmchat
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.FechaIni.Attributes.Add("OnKeyDown", "return false;")
            Me.FechaFin.Attributes.Add("OnKeyDown", "return false;")
            Me.FechaIni.Text = CDate(Request.QueryString("ficursovirtual")).ToShortDateString   'Now.ToShortDateString '
            Me.HoraIni.SelectedValue = Now.Hour
            Me.FechaFin.Text = CDate(Request.QueryString("ffcursovirtual")).ToShortDateString 'DateAdd(DateInterval.Day, 1, Now).ToShortDateString '
            Me.HoraFin.SelectedValue = Now.Hour

            If Request.QueryString("accion") = "modificarchat" Then
                Dim idchat As String
                idchat = Request.QueryString("idchat")

                Me.LblPublicacion.Visible = False
                Me.DDLPermiso.Visible = False

                Dim Datos As Data.DataTable
                Dim ObjCMUSAT As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
                Datos = ObjCMUSAT.TraerDataTable("Consultarchat", "0", idchat, "", "")
                ObjCMUSAT = Nothing

                Me.TxtNombre.Text = Datos.Rows(0).Item(1).ToString
                Me.FechaIni.Text = CDate(Datos.Rows(0).Item(3).ToString).ToShortDateString
                Me.FechaFin.Text = CDate(Datos.Rows(0).Item(4).ToString).ToShortDateString
                Me.HoraIni.SelectedValue = Mid(Datos.Rows(0).Item(3).ToString, 12, 2)
                Me.HoraFin.SelectedValue = Mid(Datos.Rows(0).Item(4).ToString, 12, 2)
                Me.MinIni.SelectedValue = Mid(Datos.Rows(0).Item(3).ToString, 15, 2)
                Me.MinFin.SelectedValue = Mid(Datos.Rows(0).Item(4).ToString, 15, 2)
                Me.TxtComentario.Text = Datos.Rows(0).Item(2).ToString
                Me.DDLPermiso.SelectedValue = Datos.Rows(0).Item(6)
                Datos = Nothing
            End If
        End If
    End Sub
    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim usuario, icursovirtual As String
        Dim valordevuelto, idchat, refcodigo_ccv As Integer
        Dim finicio, ffin As DateTime

        Dim ObjCMDUSAT As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)

        usuario = Request.QueryString("idusuario")
        icursovirtual = Request.QueryString("idcursovirtual")
        refcodigo_ccv = Request.QueryString("refcodigo_ccv")

        finicio = CDate(Me.FechaIni.Text & " " & Me.HoraIni.Text & ":" & Me.MinIni.Text & ":00")
        ffin = CDate(Me.FechaFin.Text & " " & Me.HoraFin.Text & ":" & Me.MinFin.Text & ":00")

        '#########################################################################
        'Verifico que sea diferente de modificar para agregar el chat.
        '#########################################################################
        If Request.QueryString("accion") <> "modificarchat" Then

            Me.LblMensaje.Text = ""
            Try
                '###########################################################################
                ' Guardamos el archivo y luego agregamos el chat en la base de datos
                '###########################################################################
                ObjCMDUSAT.IniciarTransaccion()
                valordevuelto = ObjCMDUSAT.Ejecutar("DI_Agregarchat", icursovirtual, Me.TxtNombre.Text, Me.TxtComentario.Text, finicio, ffin, usuario, Me.DDLPermiso.SelectedValue, refcodigo_ccv, 0)
                ObjCMDUSAT.TerminarTransaccion()
                ObjCMDUSAT = Nothing
                If valordevuelto > 0 Then
                    ObjCMDUSAT = Nothing
                    If Me.DDLPermiso.SelectedValue = 2 Then
                        Dim PagRedirec As String
                        PagRedirec = "frmagregarusuariosrecurso.asp?accion=agregarpermisos&nombretabla=chat&tipodoc=A&idtabla=" & valordevuelto & "&" & Replace(Page.ClientQueryString, "&accion=modificarchat", "", , , CompareMethod.Text)
                        Response.Redirect("../../personal/aulavirtual/usuario/" & PagRedirec)
                    Else
                        Response.Write("<script>window.opener.location.reload(); window.close();</script>")
                    End If
                End If
            Catch ex As Exception
                ObjCMDUSAT.AbortarTransaccion()
                Me.LblMensaje.Text = "Ocurrió un Error al Registrar la sesión de chat." & ex.Message
                ObjCMDUSAT = Nothing
            End Try
        Else
            Try
                '#########################################################################
                'Modifico los datos del chat, mas no el chat en si.
                '#########################################################################
                Me.LblMensaje.Text = ""
                idchat = Request.QueryString("idchat")
                ObjCMDUSAT.IniciarTransaccion()
                ObjCMDUSAT.Ejecutar("DI_Modificarchat", idchat, Me.TxtNombre.Text, Me.TxtComentario.Text, finicio, ffin, usuario)
                ObjCMDUSAT.TerminarTransaccion()
                ''../../personal/aulavirtual/moodleusat/tematicacurso.asp
                ObjCMDUSAT = Nothing
                Response.Write("<script>window.opener.location.reload();window.close()</script>")
            Catch ex As Exception
                ObjCMDUSAT.AbortarTransaccion()
                Me.LblMensaje.Text = "Ocurrió un Error al Registrar la sesión de chat." & ex.message
                ObjCMDUSAT = Nothing
            End Try
        End If

    End Sub
End Class

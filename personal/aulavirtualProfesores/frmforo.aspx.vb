
Partial Class aulavirtual_frmforo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
	    Me.FechaIni.Attributes.Add("OnKeyDown", "return false;")
            Me.FechaFin.Attributes.Add("OnKeyDown", "return false;")
            Me.FechaIni.Text = CDate(Request.QueryString("ficursovirtual")).ToShortDateString   'Now.ToShortDateString '
            Me.HoraIni.SelectedValue = Now.Hour
            Me.FechaFin.Text = CDate(Request.QueryString("ffcursovirtual")).ToShortDateString 'DateAdd(DateInterval.Day, 1, Now).ToShortDateString '
            Me.HoraFin.SelectedValue = Now.Hour

            If Request.QueryString("accion") = "modificarforo" Then
                Dim idforo As String
                idforo = Request.QueryString("idforo")

                Dim Datos As Data.DataTable
                Dim ObjCMUSAT As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
                Datos = ObjCMUSAT.TraerDataTable("Consultarforo", "4", idforo, "", "")
                ObjCMUSAT = Nothing

                Me.TxtNombre.Text = Datos.Rows(0).Item("tituloforo").ToString
		Me.FechaIni.Text = CDate(Datos.Rows(0).Item("fechainicio").ToString).ToShortDateString
                Me.FechaFin.Text = CDate(Datos.Rows(0).Item("fechafin").ToString).ToShortDateString
                Me.HoraIni.SelectedValue = Mid(Datos.Rows(0).Item("fechainicio").ToString, 12, 2)
                Me.HoraFin.SelectedValue = Mid(Datos.Rows(0).Item("fechafin").ToString, 12, 2)
                Me.MinIni.SelectedValue = Mid(Datos.Rows(0).Item("fechainicio").ToString, 15, 2)
                Me.MinFin.SelectedValue = Mid(Datos.Rows(0).Item("fechafin").ToString, 15, 2)
                
                Me.TxtComentario.Text = Datos.Rows(0).Item(3).ToString
                Datos = Nothing
            End If
        End If
    End Sub
    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim usuario, icursovirtual As String
        Dim valordevuelto, idforo, refcodigo_ccv As Integer
	Dim finicio, ffin As DateTime

        Dim ObjCMDUSAT As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)

        usuario = Request.QueryString("idusuario")
        icursovirtual = Request.QueryString("idcursovirtual")
        refcodigo_ccv = Request.QueryString("refcodigo_ccv")
	finicio = CDate(Me.FechaIni.Text & " " & Me.HoraIni.Text & ":" & Me.MinIni.Text & ":00")
        ffin = CDate(Me.FechaFin.Text & " " & Me.HoraFin.Text & ":" & Me.MinFin.Text & ":00")

        '#########################################################################
        'Verifico que sea diferente de modificar para agregar el foro.
        '#########################################################################
        If Request.QueryString("accion") <> "modificarforo" Then

            Me.LblMensaje.Text = ""
            Try
                '###########################################################################
                ' Guardamos el archivo y luego agregamos el foro en la base de datos
                '###########################################################################
                ObjCMDUSAT.IniciarTransaccion()
                valordevuelto = ObjCMDUSAT.Ejecutar("DI_Agregarforo", icursovirtual,finicio, ffin, Me.TxtNombre.Text, Me.TxtComentario.Text, usuario, refcodigo_ccv, 0)
                ObjCMDUSAT.TerminarTransaccion()
                ObjCMDUSAT = Nothing
                If valordevuelto > 0 Then
                    ObjCMDUSAT = Nothing
                    Response.Write("<script>window.opener.location.reload(); window.close();</script>")
                Else
                    Response.Write("<script>alert('Ha ocurrido un error al grabar intente denuevo')</script>")
                End If
            Catch ex As Exception
                ObjCMDUSAT.AbortarTransaccion()
                Me.LblMensaje.Text = "Ocurrió un Error al Registrar la foro." & ex.Message
                ObjCMDUSAT = Nothing
            End Try
        Else
            Try
                '#########################################################################
                'Modifico los datos del foro, mas no el foro en si.
                '#########################################################################
                Me.LblMensaje.Text = ""
                idforo = Request.QueryString("idforo")
                ObjCMDUSAT.IniciarTransaccion()
                ObjCMDUSAT.Ejecutar("DI_Modificarforo", idforo,finicio, ffin, Me.TxtNombre.Text, Me.TxtComentario.Text, usuario)
                ObjCMDUSAT.TerminarTransaccion()
                ''../../personal/aulavirtual/moodleusat/tematicacurso.asp
                ObjCMDUSAT = Nothing
                Response.Write("<script>window.opener.location.reload();window.close()</script>")
            Catch ex As Exception
                ObjCMDUSAT.AbortarTransaccion()
                Me.LblMensaje.Text = "Ocurrió un Error al Registrar lo foro."
                ObjCMDUSAT = Nothing
            End Try
        End If

    End Sub
End Class

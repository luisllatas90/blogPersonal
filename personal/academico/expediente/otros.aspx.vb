
Imports System.Data

Partial Class otros
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id") <> "" And Session("id") = "" Then
            Session("Id") = Request.QueryString("id")
        End If
        If IsPostBack = False Then
            Dim objPersonal As New Personal
            objPersonal.codigo = Session("id")
            Try
                Me.TxtDescripcion.Text = objPersonal.ObtieneDatosAdicionales.Rows(0).Item(0).ToString
                Me.TxtHabilidades.Text = objPersonal.ObtieneDatosAdicionales.Rows(0).Item(1).ToString
                Me.TxtLimitaciones.Text = objPersonal.ObtieneDatosAdicionales.Rows(0).Item(2).ToString
                Me.TxtHobbies.Text = objPersonal.ObtieneDatosAdicionales.Rows(0).Item(3).ToString
            Catch ex As Exception

            End Try
            objPersonal = Nothing
        End If
    End Sub

    Private Sub GuardarOtros()
        Dim ObjPersonal As New Personal
        ObjPersonal.codigo = Request.QueryString("Id")
        Dim valor = ObjPersonal.GrabarAdicionales(Me.TxtDescripcion.Text, Me.TxtHabilidades.Text, Me.TxtLimitaciones.Text, Me.TxtHobbies.Text)
        Dim strMsg As String
        Select Case valor
            Case -1
                strMsg = "<script>alert('Ocurrió un error, intentelo nuevamente.')</script>"
                Page.RegisterStartupScript("Error", strMsg)
            Case -2
                Me.TxtDescripcion.Text = ""
                Me.TxtHabilidades.Text = ""
                Me.TxtHobbies.Text = ""
                Me.TxtLimitaciones.Text = ""
                strMsg = "<script>alert('Debe de registrar primero sus datos personales.')</script>"
                Page.RegisterStartupScript("Error", strMsg)
            Case 1
                strMsg = "<script>alert('Se guardaron los datos con satisfacción.')</script>"
                Page.RegisterStartupScript("Exito", strMsg)
        End Select
        ObjPersonal = Nothing
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        '-----------------------------------------------------------------------------------------------
        ' Verifica que el trabajador haya aceptado la declaracion jurada para el registro de sus datos
        '-----------------------------------------------------------------------------------------------
        Dim objPersonal As New Personal
        Dim dts As New Data.DataTable
        objPersonal.codigo = Request.QueryString("id")
        dts = objPersonal.VerificaDeclaracionJuradaPersonal(Request.QueryString("id"))
        If dts.Rows.Count > 0 Then
            If dts.Rows(0).Item("rpt") = 0 Then
                Me.lblDeclarante.Text = dts.Rows(0).Item("Declarante").ToString
                mpeInforme.Show()
            Else
                GuardarOtros()
            End If
        End If
        '--------------
    End Sub

    'Se ejecuta cuando acepta el modal
    Protected Sub btnGuardarInforme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarInforme.Click
        Try
            Dim objPersonal As New Personal
            Dim i As Integer = objPersonal.ActualizarEstadoDeclaracionJurada(Request.QueryString("id"))
            GuardarOtros()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub CmdGuardar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar0.Click
        Response.Redirect("distinciones.aspx?menu=" & Request.QueryString("menu") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Los datos consignados no fueron registrados.');", True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class

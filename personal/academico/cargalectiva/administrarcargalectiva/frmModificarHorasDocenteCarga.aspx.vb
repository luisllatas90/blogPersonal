
Partial Class academico_cargalectiva_administrarcargalectiva_frmModificarHorasDocenteCarga
    Inherits System.Web.UI.Page

    'Valores para poder actualizar la caga academica
    Dim codigo_per As Integer
    Dim codigo_cup As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Try
                Dim docente As String
                Dim totalhoras As Integer
                Dim th As String       'total horas del curso programado


                codigo_per = Request.QueryString("codigo_per")
                codigo_cup = Request.QueryString("codigo_cup")

                docente = Request.QueryString("docente")
                totalhoras = Request.QueryString("totalhoras")

                Me.lblDocente.text = Request.QueryString("docente")
                Me.txtHoras.text = Request.QueryString("totalhoras")
                txtTh.text = Request.QueryString("th")

                'Response.Write(codigo_per)
                'Response.Write(" - ")
                'Response.Write(codigo_cup)

                'Response.Write(" - ")
                'Response.Write(docente)
                'Response.Write(" - ")
                'Response.Write(totalhoras)
                'Response.Write(th)
            Catch ex As Exception
                Response.Write(ex.message)
            End Try
        End If

    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            Dim vMensaje As String

            If CInt(txtHoras.text.trim) < 0 Or txtHoras.text.trim = "" Then
                vMensaje = "Se requiere ingresar una cantidad determinada de horas mayores a (0) para continuar , verifique"
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If CInt(txtHoras.text) > CInt(txtTh.text) Then
                vMensaje = "Las horas asiganadas deben ser menor o igual a las horas programadas " & "(" & txtTh.text & ")" & ", verifique"
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            obj.Ejecutar("CAR_ActualizarCargaAcademica", Request.QueryString("codigo_per"), Request.QueryString("codigo_cup"), CInt(txtHoras.text))
            Dim xB As Boolean = True


            If xB = True Then
                vMensaje = "La operación se realizo correctamente"
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            End If
            
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


End Class

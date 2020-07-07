
Partial Class Encuesta_EncuestaES_EncuestaGuarderiaSalir
    Inherits System.Web.UI.Page
    Dim pagina As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'pagina = "http://server-test/campusvirtual/estudiante/principal.asp"
            'EN REAL
            pagina = "../../../estudiante/principal.asp"

            If Request.QueryString("rpta").ToString <> "undefined" Then
                Dim objcnx As New ClsConectarDatos
                Dim datos As New Data.DataTable
                Dim codigo_eed As Integer = 0
                objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                objcnx.AbrirConexion()
                datos = objcnx.TraerDataTable("EncuestaES_GuardarEncuesta", CInt(Session("codigo_cev")), CInt(Session("codigo_alu")))
                If datos.Rows.Count Then
                    codigo_eed = (datos.Rows(0).Item("codigo_eed").ToString)
                    If codigo_eed > 0 Then
                        objcnx.Ejecutar("EncuestaES_GuardarRespuesta", CInt(Request.QueryString("rpta").ToString), codigo_eed, 0, "")
                        objcnx.Ejecutar("EncuestaES_GuardarRespuesta", 755, codigo_eed, 0, "") 'No
                    End If
                End If
                objcnx.CerrarConexion()
                objcnx = Nothing
                ClientScript.RegisterStartupScript(Me.GetType, "Encuesta", "alert('Gracias por llenar la encuesta');location.href='" & pagina & "';", True)
            Else
                pagina = "EncuestaGuarderia.aspx"
                ClientScript.RegisterStartupScript(Me.GetType, "Encuesta", "alert('Debe responder la primera pregunta');location.href='" & pagina & "';", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " " & ex.StackTrace)
            ClientScript.RegisterStartupScript(Me.GetType, "Encuesta", "alert('" & ex.Message & " " & ex.StackTrace & "');", True)
        End Try
    End Sub
End Class

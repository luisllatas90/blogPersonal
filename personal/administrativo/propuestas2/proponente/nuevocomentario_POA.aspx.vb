Partial Class proponente_nuevocomentario
    Inherits System.Web.UI.Page

    Protected Sub cmdAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAceptar.Click
        Try
            'Dim codigo_prp, codigo_dap, codigo_dip, codigo_cop, modifica As String
            Dim codigo_cop As String = ""
            Dim rsDatos As New Data.DataTable
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim codigo_ipr As Integer = 0

            rsDatos = ObjCnx.TraerDataTable("ConsultarInvolucradoPropuesta", "RK", Request.QueryString("codigo_prp"), Request.QueryString("codigo_per"))
            codigo_ipr = rsDatos.Rows(0).Item(0)

            codigo_cop = ObjCnx.TraerValor("RegistraComentario", codigo_ipr, 0, "A", Me.txtAsunto.Text, Server.HtmlDecode(Me.txtComentario.Text), "O", 0)

            ''============================================================================================================================================================================
            ''----------Agregado dguevara: 05/10/2011
            ''Envia email siempre que el tipo de propuesta sea [13 - POSTGRADO]
            ''----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            'Dim rsTipoPropuesta As New Data.DataTable
            'Dim Codigo_Tpr As Integer       'Almacenamos el Codigo del tipo de Propuesta
            'Dim Propuesta As String

            'rsTipoPropuesta = ObjCnx.TraerDataTable("VerificarTipoPropuesta", "CS", Request.QueryString("codigo_prp")) ', Request.QueryString("codigo_per"))
            'Codigo_Tpr = rsTipoPropuesta.Rows(0).Item("codigo_Tpr")
            'Propuesta = rsTipoPropuesta.Rows(0).Item("Propuesta")

            ''Response.Write(Codigo_Tpr)

            'If Codigo_Tpr = 13 Then
            '    Dim correo As String
            '    Dim NombreUsuario As String
            '    'Dim mensaje_1 As String
            '    'Dim mensaje_prueba As String
            '    'Dim mensaje_2 As String

            '    Dim rsEmail As New Data.DataTable
            '    Dim objMail As New ClsMail

            '    'consultamos el email del usuario
            '    rsEmail = ObjCnx.TraerDataTable("ConsultarPersonal", "CO", Request.QueryString("codigo_per"))
            '    correo = rsEmail.Rows(0).Item("emailUSAT").ToString    'obtenemos el email 
            '    NombreUsuario = rsEmail.Rows(0).Item("personal")       'obtenemos el nombre del usuario que ha iniciado sesion 

            '    Dim Asunto As String = "Observación de Propuesta: " & Propuesta

            '    ''---------------------------------------------------------------------------------------------------------------------------------------
            '    ''CONSULTAR ESTO?
            '    ''---------------------------------------------------------------------------------------------------------------------------------------
            '    ''If Trim(LCase(correo)) <> "@usat.edu.pe" Then
            '    ''    ' ''--------Para Pruebas correo dguevara: DANTE GUEVARA
            '    ''    'mensaje_prueba = "Estimado(a):  Guevara Alarcón Dante la propuesta, <br><B>" & Propuesta & "</B> ha sido comentada por el usuario " & NombreUsuario & "<br><br><B> Asunto: " & txtAsunto.Text.Trim & "<br> Comentario: </B><br>"
            '    ''    ''objMail.EnviarMail(    [De string],     [NombreEnvia string],[Para string],[Asunto string],         [Mensaje string], [HTML Boolean],[ConCopia string *Opc],[ReplyTo string *Opc])
            '    ''    'objMail.EnviarMail("campusvirtual@usat.edu.pe", NombreUsuario, "dguevara@usat.edu.pe", Asunto, mensaje_prueba & " " & txtComentario.Text.Trim & "<br><br>Atte.<br>Campus Virtual - USAT.", True, "", correo)
            '    ''    ''------------------------------

            '    ''    ''===Envio de Correos:
            '    ''    ''Correo a [CHUMACERO ANCAJIMA SHIRLEY VERONICA]: vchumacero@usat.edu.pe
            '    ''    mensaje_1 = "Estimado(a):  CHUMACERO ANCAJIMA SHIRLEY VERONICA la propuesta. <B>" & Propuesta & "</B>, ha sido comentada por el usuario " & NombreUsuario & "<br><br><B> Asunto: " & txtAsunto.Text.Trim & "<br> Comentario: </B><br><br>"
            '    ''    objMail.EnviarMail("campusvirtual@usat.edu.pe", NombreUsuario, "vchumacero@usat.edu.pe", Asunto, mensaje_1 & " " & txtComentario.Text.Trim & "<br><br>Atte.<br>Campus Virtual - USAT.", True, "", correo)

            '    ''    ''Correo a [CAMPOS OLAZABAL PATRICIA JULIA] : pcampos@usat.edu.pe
            '    ''    mensaje_2 = "Estimado(a):  CAMPOS OLAZABAL PATRICIA JULIA la propuesta. <B>" & Propuesta & "</B>, ha sido comentada por el usuario " & NombreUsuario & "<br><br><B> Asunto: " & txtAsunto.Text.Trim & "<br> Comentario: </B><br><br>"
            '    ''    objMail.EnviarMail("campusvirtual@usat.edu.pe", NombreUsuario, "pcampos@usat.edu.pe", Asunto, mensaje_2 & " " & txtComentario.Text.Trim & "<br><br>Atte.<br>Campus Virtual - USAT.", True, "", correo)
            '    ''End If
            'End If

            ''============================================================================================================================================================================


            ''codigo_cop = ObjCnx.TraerValor("RegistraComentario", codigo_ipr, 0, "A", Me.txtAsunto.Text, Me.txtComentario.Text, "O", 0)
            Response.Redirect("comentarios_POA.aspx?codigo_prp=" & Request.QueryString("codigo_prp") & "&codigo_per=" & Request.QueryString("codigo_per"))

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Response.Redirect("comentarios_POA.aspx?codigo_prp=" & Request.QueryString("codigo_prp") & "&codigo_per=" & Request.QueryString("codigo_per"))
    End Sub
End Class

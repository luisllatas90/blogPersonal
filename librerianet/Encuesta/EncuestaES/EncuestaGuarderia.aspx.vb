﻿
Partial Class EvaluacionAlumnoDocente_EncuestaGuarderiaInfantil
    Inherits System.Web.UI.Page
    Dim pagina As String = ""
    Dim datos As New Data.DataTable


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'pagina = "http://server-test/campusvirtual/estudiante/abriraplicacion.asp?codigo_tfu=3&codigo_apl=8&descripcion_apl=Campus Virtual USAT"

        'EN REAL
        pagina = "../../../estudiante/abriraplicacion.asp?codigo_tfu=3&codigo_apl=8&descripcion_apl=Campus Virtual USAT"

        txtObligatorio.InnerHtml = "<a style ='text-decoration:none;' href='" & pagina & "'><span style='color:green;font-weight:bold;text-decoration:none;'>Encuesta Opcional</span><br>Clic aquí para responder después.</a>"

        Dim objcnx As New ClsConectarDatos
        objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objcnx.AbrirConexion()
        datos = objcnx.TraerDataTable("VerificarEncuestaTipoES", CInt(Session("codigo_alu")))
        objcnx.CerrarConexion()
        If datos.Rows.Count Then
            If datos.Rows(0).Item("eed").ToString <> "-1" Then
                objcnx = Nothing
                Response.Redirect(pagina)
            End If
        End If



        If Not IsPostBack Then
            objcnx.AbrirConexion()
            datos = New Data.DataTable
            datos = objcnx.TraerDataTable("EncuestaES_ListarPreguntas")
            Dim Preguntas As String = ""
            Dim codigo_eva As String = ""
            Dim codigo_padre As String = ""
            Dim texto As String = ""
            If datos.Rows.Count Then
                Session("codigo_cev") = (datos.Rows(0).Item("codigo_cev").ToString)
                Preguntas &= "<table>"
                For i As Integer = 0 To datos.Rows.Count - 1
                    codigo_eva = datos.Rows(i).Item("codigo_eva").ToString
                    texto = datos.Rows(i).Item("numero_eva").ToString & " " & datos.Rows(i).Item("pregunta_eva").ToString
                    If datos.Rows(i).Item("conRespuesta_eva").ToString = "N" Then
                        codigo_padre = codigo_eva
                        Preguntas &= "<tr><td class=""style12"" colspan=""2"">"
                        Preguntas &= texto
                        Preguntas &= "</td></tr>"
                    ElseIf datos.Rows(i).Item("conRespuesta_eva").ToString = "S" Then
                        Preguntas &= "<td class=""style8"" colspan=""2"">"
                        If datos.Rows(i).Item("tipoPregunta_cev").ToString = "A" Then
                            codigo_padre = codigo_eva
                        End If
                        Preguntas &= "<input type=""radio"" name=""Respuesta_" & codigo_padre & """ value=""" & datos.Rows(i).Item("codigo_eva").ToString & """/>"
                        Preguntas &= texto
                        If datos.Rows(i).Item("tipoPregunta_cev").ToString = "A" Then
                            Preguntas &= "&nbsp;&nbsp;&nbsp<input type=""text"" name= ""RespuestaTexto_" & codigo_eva & """  style=""width: 31px""/>"
                        End If
                        Preguntas &= "</td></tr>"
                    ElseIf datos.Rows(i).Item("conRespuesta_eva").ToString = "X" Then
                        Preguntas &= "<td class=""style8"" colspan=""2"">"
                        Preguntas &= "<input type=""button"" id=""btnSalir"" value=""b. NO soy madre de familia."" class=""guardar btn"" OnClick = ""o_O();"" />"
                        Preguntas &= "</td></tr>"
                    End If
                Next
                Preguntas &= "</table>"
                Me.TablaPreguntas.InnerHtml = Preguntas
                datos.Dispose()
            End If
            objcnx.CerrarConexion()
        End If
        objcnx = Nothing
    End Sub

    Function validar() As Boolean
        Dim displayValues As New StringBuilder()
        Dim postedValues As NameValueCollection = Request.Form
        Dim nextKey As String
        Dim rptas As Integer = 0
        If postedValues.AllKeys.Length Then
            For i As Integer = 0 To postedValues.AllKeys.Length - 1
                nextKey = postedValues.AllKeys(i)
                If nextKey.Substring(0, 2) <> "__" Then
                    If postedValues(i).ToString <> "" Then
                        If nextKey.ToString.Contains("Respuesta_") Or nextKey.ToString.Contains("RespuestaTexto_") Then
                            rptas += 1
                        End If
                    End If
                End If
            Next
        End If
        If rptas < 11 Then
            Return False
        End If
        Return True
    End Function


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click, Button2.Click
        If validar() Then
            Dim codigo_eed As Integer = 0
            Try
                Dim objcnx As New ClsConectarDatos
                objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                objcnx.AbrirConexion()
                Dim datos As New Data.DataTable
                Dim displayValues As New StringBuilder()
                Dim postedValues As NameValueCollection = Request.Form
                Dim nextKey As String
                Dim cadena As String()

                If postedValues.AllKeys.Length Then
                    datos = objcnx.TraerDataTable("EncuestaES_GuardarEncuesta", CInt(Session("codigo_cev")), CInt(Session("codigo_alu")))
                    If datos.Rows.Count Then
                        codigo_eed = CInt(datos.Rows(0).Item("codigo_eed").ToString)
                        If codigo_eed > 0 Then
                            For i As Integer = 0 To postedValues.AllKeys.Length - 1
                                nextKey = postedValues.AllKeys(i)
                                If nextKey.Substring(0, 2) <> "__" Then
                                    cadena = nextKey.ToString.Split("_")
                                    'displayValues.Append("<br>")
                                    'displayValues.Append(nextKey)
                                    'displayValues.Append(" = ")
                                    'displayValues.Append(postedValues(i))
                                    If postedValues(i).ToString <> "" Then
                                        If nextKey.ToString.Contains("Respuesta_") Then
                                            If cadena(1) <> postedValues(i).ToString Then
                                                objcnx.Ejecutar("EncuestaES_GuardarRespuesta", CInt(postedValues(i).ToString), codigo_eed, 0, "")
                                            End If
                                        ElseIf nextKey.ToString.Contains("RespuestaTexto_") Then
                                            objcnx.Ejecutar("EncuestaES_GuardarRespuesta", CInt(cadena(1).ToString), codigo_eed, 1, postedValues(i).ToString)
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If
                objcnx.CerrarConexion()
                objcnx = Nothing
                ClientScript.RegisterStartupScript(Me.GetType, "Encuesta", "alert('Gracias por llenar la encuesta');location.href='" & pagina & "';", True)
            Catch ex As Exception
                Response.Write(ex.Message & ex.StackTrace)
            End Try
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "Encuesta", "alert('Faltan respuestas');", True)
        End If
    End Sub

End Class

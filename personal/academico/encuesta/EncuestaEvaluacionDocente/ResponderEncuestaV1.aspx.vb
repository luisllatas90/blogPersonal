
Partial Class academico_encuesta_EncuestaEvaluacionDocente_ResponderEncuesta
    Inherits System.Web.UI.Page
    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If CInt(Session("id_per")) = 0 Then
                ShowMessage("La sesión ha expirado, porfavor volver a ingresar al campus virtual", MessageType.Info)
            Else
                Cargarfoto()
                Me.lblDocente.Text = Session("eva_docente")
                Me.lblDpto.Text = Session("eva_semestre")
                Me.lblDedicacion.Text = Session("eva_dedicacion")
                CargarPreguntas()
            End If


        End If


        


    End Sub
    Sub Cargarfoto()
        Dim strFoto As Boolean = False
        Dim strRutaFoto As String = Server.MapPath("..\..\..\imgpersonal\" & Session("eva_codigoper") & ".jpg")
        strFoto = (System.IO.File.Exists(strRutaFoto))
        If strFoto = True Then
            Me.imagePer.ImageUrl = "../../../../personal/imgpersonal/" & Session("eva_codigoper") & ".jpg"
        Else
            Me.imagePer.ImageUrl = "../../../../personal/imgpersonal/fotovacia3.png"
        End If
    End Sub
    Sub CargarPreguntas()
        Try

            If CInt(Session("id_per")) = 0 Then
                ShowMessage("La sesión ha expirado, porfavor volver a ingresar al campus virtual", MessageType.Info)
            Else

                gvPreguntas.DataSource = Nothing
                Me.gvPreguntas.DataBind()
                Dim dt As New Data.DataTable
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dt = obj.TraerDataTable("EAD_ConsultarEvaluacionDesempenio", Session("eva_Tipo"), Session("eva_codigocev"), "C")
                obj.CerrarConexion()
                If dt.Rows.Count Then
                    Me.gvPreguntas.DataSource = dt
                Else
                    Me.gvPreguntas.DataSource = Nothing
                End If
                Me.gvPreguntas.DataBind()


                'Cargar Pregunta Abierta
                obj.AbrirConexion()
                dt = obj.TraerDataTable("EAD_ConsultarEvaluacionDesempenio", Session("eva_Tipo"), Session("eva_codigocev"), "A")
                obj.CerrarConexion()
                If dt.Rows.Count Then
                    Session("codigo_PA") = dt.Rows(0).Item("codigo_Eva").ToString
                    Me.lblPreguntaAbierta.Visible = True
                    Me.txtPreguntaAbierta.Visible = True
                    Me.lblPreguntaAbierta.Text = dt.Rows(0).Item("pregunta_Eva").ToString
                Else
                    Session("codigo_PA") = 0
                    Me.txtPreguntaAbierta.Visible = False
                    Me.lblPreguntaAbierta.Visible = False
                End If


            End If
        Catch ex As Exception

        End Try

    End Sub
    Function validar() As Boolean

        Dim nropreguntas As Integer = 0
        If Session("eva_Tipo") = "S" Or Session("eva_Tipo") = "X" Then
            nropreguntas = 18
        Else
            nropreguntas = 18
        End If

        Dim respuesta_eva As Integer = 0
        Dim respondio As Integer = 0
        For Each row As GridViewRow In gvPreguntas.Rows
            respuesta_eva = 0
            Dim radioSi As Boolean = CType(row.FindControl("rbSi"), RadioButton).Checked
            Dim radioNo As Boolean = CType(row.FindControl("rbNo"), RadioButton).Checked

            If radioSi Then
                respuesta_eva = 2
            End If
            If radioNo Then
                respuesta_eva = 1
            End If

            If respuesta_eva > 0 Then
                respondio += 1
            End If
        Next

        If respondio = nropreguntas Then
            Return True
        ElseIf respondio = 0 Then
            Return False
        End If
    End Function
    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        registrar()
    End Sub
    Sub registrar()
        Dim vale As String = 0
        'Dim origen As Integer = 0

        'If Request.Browser.IsMobileDevice Then
        '    origen = 1
        'End If


        If validar() Then
            Dim cod_per As Integer
            cod_per = CInt(Session("id_per"))
            If cod_per > 0 Then
                Dim objcnx As New ClsConectarDatos
                objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                Try

                    Dim codigo_eva As Integer
                    Dim codigo_eed As Integer
                    Dim tabla As Data.DataTable
                    Dim respuesta_eva As Integer
                    Dim datos As New Data.DataTable

                    objcnx.IniciarTransaccion()

                    tabla = objcnx.TraerDataTable("EAD_AgregarEncuestaEvaluacionDDV2", cod_per, 0, Session("eva_codigoper"), Session("eva_Tipo"), Session("eva_codigocev"), Session("eva_codigoTipo"), 0, Session("origen"))

                    If tabla.Rows.Count > 0 Then
                        codigo_eed = tabla.Rows(0).Item("codigo_eed")

                        'Registrar pregunta abierta
                        If Session("codigo_PA") <> 0 Then
                            objcnx.Ejecutar("EAD_AgregarRespuestaAbiertaEvaluacion", Session("codigo_PA"), Me.txtPreguntaAbierta.Text.Trim, codigo_eed)

                        End If

                        'registrar las demás preguntas
                        If codigo_eed > 0 Then

                            For Each row As GridViewRow In gvPreguntas.Rows

                                Dim radioSi As Boolean = CType(row.FindControl("rbSi"), RadioButton).Checked
                                Dim radioNo As Boolean = CType(row.FindControl("rbNo"), RadioButton).Checked
                                If radioSi Then
                                    respuesta_eva = 1
                                End If
                                If radioNo Then
                                    respuesta_eva = 0
                                End If
                                codigo_eva = gvPreguntas.DataKeys.Item(row.RowIndex).Values("codigo_eva")
                                objcnx.Ejecutar("EAD_AgregarRespuestaEvaluacionV2", codigo_eva, respuesta_eva, Session("eva_codigocev"), Session("eva_codigoper"), cod_per)
                            Next
                            objcnx.TerminarTransaccion()
                        Else
                            objcnx.AbortarTransaccion()
                        End If
                    End If                  
                    objcnx.CerrarConexion()
                    objcnx = Nothing

                    Session("eva_codigoper") = "0"
                    Response.Redirect("EncuestaDirector.aspx?tipoEval=" & Session("eva_Tipo"))

                Catch ex As Exception
                    objcnx.AbortarTransaccion()
                    ShowMessage("Ha ocurrido un error al registrar las respuestas", MessageType.Error)
                End Try
            Else
                ShowMessage("La sesión ha expirado, porfavor volver a ingresar al campus virtual", MessageType.Error)
            End If
        Else
            ShowMessage("Por favor complete todas las preguntas", MessageType.Info)
        End If
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Session("eva_codigoper") = "0"
        Response.Redirect("EncuestaDirector.aspx?tipoEval=" & Session("eva_Tipo"))
    End Sub

    Protected Sub gvPreguntas_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPreguntas.PreRender
        If gvPreguntas.Rows.Count > 0 Then
            gvPreguntas.UseAccessibleHeader = True
            gvPreguntas.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub btnRegresar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar2.Click
        Session("eva_codigoper") = "0"
        Response.Redirect("EncuestaDirector.aspx?tipoEval=" & Session("eva_Tipo"))
    End Sub

    Protected Sub btnGuardar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar2.Click
        registrar()
    End Sub

    Protected Sub gvPreguntas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPreguntas.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim ddlSi As RadioButton, ddlNo As RadioButton
                ddlSi = TryCast(e.Row.FindControl("rbSi"), RadioButton)
                ddlNo = TryCast(e.Row.FindControl("rbNo"), RadioButton)

                If Request.Browser.IsMobileDevice Then
                    ddlSi.CssClass = "radiorespuesta"
                    ddlNo.CssClass = "radiorespuesta"
                    Session("origen") = 1
                Else
                    ddlSi.CssClass = "radionormal"
                    ddlNo.CssClass = "radionormal"
                    Session("origen") = 0
                End If



            End If

        Catch ex As Exception

        End Try
        
    End Sub
End Class


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
                llenarEscala()
                CargarPreguntas()
            End If
        End If

    End Sub
    Sub llenarEscala()
        Me.GridView1.DataSource = Nothing
        Dim tbEscla As New Data.DataTable
        tbEscla.Columns.Add("1")
        tbEscla.Columns.Add("2")
        'tbEscla.Columns.Add("3")
        'tbEscla.Columns.Add("4")

        'tbEscla.Rows.Add("NO LOGRADO", "EN DESARROLLO", "LOGRADO", "SATISFACTORIO")
        tbEscla.Rows.Add("0", "1")

        Me.GridView1.DataSource = tbEscla
        Me.GridView1.DataBind()
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

                If Session("eva_Tipo") <> "O" Then  '26-10 Observador
                    lblpreg.visible = False
                    Txtpreguntafinal.text = ""
                    Txtpreguntafinal.visible = False
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
    Function validar() As Boolean

        Dim nropreguntas As Integer = 0
        If Session("eva_Tipo") = "S" Or Session("eva_Tipo") = "X" Then
            '--nropreguntas = 18
            nropreguntas = 17
        Else
            '-nropreguntas = 18
            nropreguntas = 17
        End If

        Dim respuesta_eva As Integer = 0
        Dim respondio As Integer = 0
        For Each row As GridViewRow In gvPreguntas.Rows
            respuesta_eva = 0
            Dim rbUno As Boolean = CType(row.FindControl("rbUno"), RadioButton).Checked
            Dim rbDos As Boolean = CType(row.FindControl("rbDos"), RadioButton).Checked
            'Dim rbTres As Boolean = CType(row.FindControl("rbTres"), RadioButton).Checked
            'Dim rbCuatro As Boolean = CType(row.FindControl("rbCuatro"), RadioButton).Checked

            If rbUno Then
                respuesta_eva = 1
            ElseIf rbDos Then
                respuesta_eva = 1
                'ElseIf rbTres Then   '23-10-2020 Se comenta
                '    respuesta_eva = 1
                'ElseIf rbCuatro Then
                '    respuesta_eva = 1
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
                    Dim respuesta_eva As Decimal
                    Dim datos As New Data.DataTable
                    Dim rbUno As Boolean = False
                    Dim rbDos As Boolean = False
                    'Dim rbTres As Boolean = False   '26-10 CGASTELO
                    'Dim rbCuatro As Boolean = False

                    objcnx.IniciarTransaccion()

                    tabla = objcnx.TraerDataTable("EAD_AgregarEncuestaEvaluacionDDV2", cod_per, 0, Session("eva_codigoper"), Session("eva_Tipo"), Session("eva_codigocev"), Session("eva_codigoTipo"), 0, Session("origen"))
                   
                    If tabla.Rows.Count > 0 Then
                        codigo_eed = tabla.Rows(0).Item("codigo_eed")

                        'registrar las demás preguntas
                        If codigo_eed > 0 Then
                            
                            For Each row As GridViewRow In gvPreguntas.Rows

                                rbUno = CType(row.FindControl("rbUno"), RadioButton).Checked
                                rbDos = CType(row.FindControl("rbDos"), RadioButton).Checked
                                '//Ahora se comentan los radio 3 y 4
                                'rbTres = CType(row.FindControl("rbTres"), RadioButton).Checked
                                'rbCuatro = CType(row.FindControl("rbCuatro"), RadioButton).Checked

                                'PREPARACIÓN DE CLASE 
                                'If gvPreguntas.DataKeys.Item(row.RowIndex).Values("codigo_comp") = 5 Then
                                'If rbUno Then
                                'respuesta_eva = 0
                                'ElseIf rbDos Then
                                'respuesta_eva = 1 ' 0 23/10/2020
                                'ElseIf rbTres Then
                                '    respuesta_eva = 0
                                'ElseIf rbCuatro Then
                                '    respuesta_eva = 2.5
                                'End If

                                'Else 'DESARROLLO DE CLASE 
                                'If rbUno Then
                                '    respuesta_eva = 0
                                'ElseIf rbDos Then
                                '    respuesta_eva = 0.5
                                'ElseIf rbTres Then
                                '    respuesta_eva = 0.75
                                'ElseIf rbCuatro Then
                                '    respuesta_eva = 1
                                'End If

                                'End If

                                If rbUno Then   '28/10
                                    respuesta_eva = 0
                                ElseIf rbDos Then
                                    respuesta_eva = 1
                                End If

                                codigo_eva = gvPreguntas.DataKeys.Item(row.RowIndex).Values("codigo_eva")

                                objcnx.Ejecutar("EAD_AgregarRespuestaEvaluacionV2", codigo_eva, respuesta_eva, Session("eva_codigocev"), Session("eva_codigoper"), cod_per, "")

                            Next

                            If Txtpreguntafinal.visible = True Then
                                'pregunta abierta,la 19 solo para Observador
                                objcnx.Ejecutar("EAD_AgregarRespuestaEvaluacionV2", val(codigo_eva + 1), 0, Session("eva_codigocev"), Session("eva_codigoper"), cod_per, trim(Me.Txtpreguntafinal.text))
                            End If                            

                            objcnx.TerminarTransaccion()
                            'ShowMessage("Se registraron las respuestas correctamente.", MessageType.Success) '27/10 Ceci

                        Else
                            objcnx.AbortarTransaccion()
                        End If
                    End If

                    objcnx.CerrarConexion()
                    objcnx = Nothing

                    Session("eva_codigoper") = "0"
                    Response.Redirect("EncuestaDirector.aspx?tipoEval=" & Session("eva_Tipo") & "&resp=si")

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
            Dim codigo_comp As Integer = 0

            If e.Row.RowType = DataControlRowType.DataRow Then

                'codigo_comp = gvPreguntas.DataKeys.Item(e.Row.RowIndex).Values("codigo_comp") 'Se comenta 27/10 Ceci


                Dim ddlUno As RadioButton, ddlDos As RadioButton, ddlTres As RadioButton, ddlCuatro As RadioButton, ddlCinco As RadioButton

                ddlUno = TryCast(e.Row.FindControl("rbUno"), RadioButton)
                ddlDos = TryCast(e.Row.FindControl("rbDos"), RadioButton)
                'ddlTres = TryCast(e.Row.FindControl("rbTres"), RadioButton)
                'ddlCuatro = TryCast(e.Row.FindControl("rbCuatro"), RadioButton)

      
                If Request.Browser.IsMobileDevice Then
                    ddlUno.CssClass = "radiorespuesta"
                    ddlDos.CssClass = "radiorespuesta"
                    'ddlTres.CssClass = "radiorespuesta"
                    'ddlCuatro.CssClass = "radiorespuesta"
                    Session("origen") = 1
                Else
                    ddlUno.CssClass = "radionormal"
                    ddlDos.CssClass = "radionormal"
                    'ddlTres.CssClass = "radionormal"
                    'ddlCuatro.CssClass = "radionormal"

                    Session("origen") = 0
                End If

                'If codigo_comp = 5 Then   '// Se comenta 27/10 Ceci
                '    ddlUno.text = "NO"
                '    ddlDos.text = "-"
                '    ddlTres.text = "-"
                '    ddlCuatro.text = "SÍ"

                '    ddlDos.enabled = False
                '    ddlTres.enabled = False
                'Else
                '    ddlUno.text = "NO LOGRADO"
                '    ddlDos.text = "EN DESARROLLO"
                '    ddlTres.text = "LOGRADO"
                '    ddlCuatro.text = "SATISFACTORIO"
                'End If



            End If

        Catch ex As Exception

        End Try
        
    End Sub
End Class

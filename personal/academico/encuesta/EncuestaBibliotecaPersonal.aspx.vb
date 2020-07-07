
Partial Class EvaluacionDocente_Estudiante
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objcnx As New ClsConectarDatos
            Dim ObjCif As New PryCifradoNet.ClsCifradoNet
            Dim datos, datosCicloAcad, datosEvaluacionDD As New Data.DataTable
            hddCodigo_cev.value = 12
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Try

                cmdGuardarArriba.Enabled = False
                cmdGuardarAbajo.Enabled = False

                Dim cup, codigo_usu, codigo_cac As String

                codigo_usu = Request.QueryString("xcxu")
                codigo_cac = Request.QueryString("xcxa")

                objcnx.AbrirConexion()
                datosCicloAcad = objcnx.TraerDataTable("VerificarEncuestaBibliotecaEstudiante", codigo_usu, codigo_cac)
                objcnx.CerrarConexion()
                If datosCicloAcad.Rows.Count = 0 Then
                    hddcodigo_cac.Value = codigo_cac
                    objcnx.AbrirConexion()
                    datosEvaluacionDD = objcnx.TraerDataTable("EAD_ConsultarEvaluacionVigenteXTipo", "BI")
                    objcnx.CerrarConexion()
                    If datosEvaluacionDD.Rows.Count > 0 Then
                        objcnx.AbrirConexion()
                        cmdGuardarArriba.Enabled = True
                        cmdGuardarAbajo.Enabled = True
                        gvPreguntas.DataSource = objcnx.TraerDataTable("EAD_ConsultarEvaluacionDesempenioBIB", 12)
                        gvPreguntas.DataBind()
                        objcnx.CerrarConexion()
                    Else
                        cmdGuardarArriba.Enabled = False
                        cmdGuardarAbajo.Enabled = False
                    End If
                Else
                    Dim redireccion As String
                    redireccion = "location.href='http://www.usat.edu.pe/campusvirtual/personal/listaaplicaciones.asp'"
                    ClientScript.RegisterStartupScript(Me.GetType, "ENCUESTA", "alert('Usted ya ha respondido la encuesta.');", True)
                    ClientScript.RegisterStartupScript(Me.GetType, "redirecionar1", redireccion, True)
                End If
                objcnx = Nothing

            Catch ex As Exception
                Response.Write(ex.Message)
                ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error 2');", True)
            End Try
        End If
    End Sub

    Protected Sub gvPreguntas_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPreguntas.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ctrOpciones As New RadioButtonList
            Dim ctrValidar As New RequiredFieldValidator
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Cells(2).ColumnSpan = 5
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Visible = False
            If gvPreguntas.DataKeys.Item(e.Row.RowIndex).Values(1) <> "N" Then
                Dim NUM As Int16 = 4

                For i As Int16 = 0 To NUM
                    ctrOpciones.Items.Add(" ")
                    ctrOpciones.RepeatDirection = RepeatDirection.Horizontal
                    If gvPreguntas.DataKeys.Item(e.Row.RowIndex).Values(2) = "A" Then
                        If i < 4 Then
                            ctrOpciones.Items.Item(i).Value = i + 1
                        Else
                            ctrOpciones.Items.Item(i).Value = 0
                        End If
                    Else
                        If i < 4 Then
                            ctrOpciones.Items.Item(i).Value = NUM + 1
                        Else
                            ctrOpciones.Items.Item(i).Value = 0
                        End If
                        NUM -= 1
                    End If
                Next

                ctrOpciones.ID = "opcion"
                ctrValidar.ControlToValidate = ctrOpciones.ID
                ctrValidar.ValidationGroup = "Guardar"
                ctrValidar.ErrorMessage = "Faltan preguntas por responder"
                ctrValidar.Text = "*"
                ctrValidar.ForeColor = Drawing.Color.Red
                e.Row.Cells(2).Controls.Add(ctrOpciones)
                e.Row.Cells(2).Controls.Add(ctrValidar)



            Else
                e.Row.Cells(0).Font.Bold = True
                e.Row.Cells(1).Font.Bold = True
                e.Row.BackColor = Drawing.Color.AliceBlue
            End If
        End If
    End Sub



    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarAbajo.Click, cmdGuardarArriba.Click
        Dim objcnx As New ClsConectarDatos
        objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim ObjCif As New PryCifradoNet.ClsCifradoNet
        Dim tper As String
        Try

            Dim codigo_eva, codigo_eed, valoresDevueltos(1) As Int32
            Dim codigo_res, i As Int16
            Dim codigo_per As Int32 = 0
            Dim conrespuesta_eva As String
            Dim lista As New RadioButtonList
            Dim datos, tipoper As New Data.DataTable
            Dim id_Alu, cup As String


            id_Alu = Request.QueryString("xcxu")

            tper = "x"
            '***Obtener tipo de personal
            objcnx.AbrirConexion()
            tipoper = objcnx.TraerDataTable("EncuestaBibObtenerTipoPersonal", id_Alu)
            objcnx.CerrarConexion()

            If tipoper.Rows.Count > 0 Then
                tper = tipoper.Rows(0).Item(0).ToString
            End If
            '***


            objcnx.IniciarTransaccion()
            objcnx.Ejecutar("EAD_AgregarEncuestaEvaluacionDD", CInt(id_Alu), 0, 0, tper, hddCodigo_cev.Value, 0).copyto(valoresDevueltos, 0)
            codigo_eed = valoresDevueltos(0)
            If codigo_eed > 0 Then
                For i = 1 To gvPreguntas.Rows.Count
                    codigo_eva = gvPreguntas.DataKeys.Item(i - 1).Values(0)
                    conrespuesta_eva = gvPreguntas.DataKeys.Item(i - 1).Values(1)
                    If conrespuesta_eva <> "N" Then
                        lista = gvPreguntas.Controls(0).Controls(i).Controls(2).Controls(0)
                        codigo_res = lista.SelectedValue
                        objcnx.Ejecutar("EAD_AgregarRespuestaEvaluacionBI", codigo_eva, codigo_res, codigo_eed)
                    End If
                Next


                'Guardar Respuesta Abierta:
                Dim sel As Integer

                If Me.Radio1.Checked Then
                    sel = 1
                ElseIf Me.Radio2.Checked Then
                    sel = 2
                ElseIf Me.Radio3.Checked Then
                    sel = 3
                ElseIf Me.Radio4.Checked Then
                    sel = 4
                ElseIf Me.Radio5.Checked Then
                    sel = 5
                Else
                    sel = 0
                End If


                If sel = 0 Then
                    ClientScript.RegisterStartupScript(Me.GetType, "Falta", "alert('Favor de responder la última pregunta'); document.getElementById('txtPreguntaAbierta').focus();", True)
                    Exit Sub
                ElseIf sel = 5 Then
                    If Me.txtPreguntaAbierta.Value.Trim = "" Then
                        ClientScript.RegisterStartupScript(Me.GetType, "Falta", "alert('En la última pregunta, ha seleccionado [5]Otro, favor de especificar cuál...'); document.getElementById('txtPreguntaAbierta').focus(); document.getElementById('capa').style.borderColor = '#76b7f5';", True)
                        Exit Sub
                    End If
                End If



                'ServerTest
                'objcnx.Ejecutar("EAD_AgregarRespuestaAbiertaEvaluacionBIP", 623, sel, Me.txtPreguntaAbierta.Value.Trim, codigo_eed)
                'ServerReal
                objcnx.Ejecutar("EAD_AgregarRespuestaAbiertaEvaluacionBIP", 620, sel, Me.txtPreguntaAbierta.Value.Trim, codigo_eed)

                objcnx.TerminarTransaccion()
            Else
                objcnx.AbortarTransaccion()

                ClientScript.RegisterStartupScript(Me.GetType, "no habilitado", "alert('Usted ya contestó la evaluación para este curso');", True)
            End If

            cmdGuardarAbajo.Enabled = False
            cmdGuardarArriba.Enabled = False

            datos = Nothing


            Dim redireccion As String

            redireccion = "location.href='http://www.usat.edu.pe/campusvirtual/personal/listaaplicaciones.asp'"

            ClientScript.RegisterStartupScript(Me.GetType, "correcto", "alert('Gracias por llenar la encuesta');", True)
            ClientScript.RegisterStartupScript(Me.GetType, "redirecionar1", redireccion, True)
        Catch ex As Exception
            objcnx.AbortarTransaccion()
            Response.Write(":Error:::" & ex.Message)

        End Try
    End Sub



    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Dim redireccion As String
        redireccion = "location.href='http://www.usat.edu.pe/campusvirtual/personal/listaaplicaciones.asp'"
        ClientScript.RegisterStartupScript(Me.GetType, "redirecionar1", redireccion, True)
    End Sub




End Class

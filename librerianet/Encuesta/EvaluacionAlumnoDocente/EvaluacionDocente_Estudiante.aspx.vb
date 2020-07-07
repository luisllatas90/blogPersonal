Partial Class EvaluacionDocente_Estudiante
    Inherits System.Web.UI.Page
    Public pagina As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'pagina = "http://server-test/campusvirtual/estudiante/abriraplicacion.asp?codigo_tfu=3&codigo_apl=8&descripcion_apl=Campus Virtual USAT&op=1"

        'EN REAL
        pagina = "../../../estudiante/abriraplicacion.asp?codigo_tfu=3&codigo_apl=8&descripcion_apl=Campus Virtual USAT&op=1"

        Dim texto As String
        texto = Date.Now.DayOfWeek.ToString
        ' If (texto = "Saturday" Or texto = "Sábado" Or texto = "Sunday" Or texto = "Domingo" Or texto = "Sabado") Then
        'txtObligatorio.InnerHtml = "<span style='color:red;font-weight:bold'>Encuesta Obligatoria</span><br>Para ingresar al campus virtual, antes debes responder la encuesta."
        ' Else
        txtObligatorio.InnerHtml = "<a style ='text-decoration:none;' href='" & pagina & "'><span style='color:green;font-weight:bold;text-decoration:none;'>Encuesta Opcional</span><br>Clic aquí para responder después.</a>"
        'End If

        If Not IsPostBack Then
            Dim objcnx As New ClsConectarDatos
            Dim ObjCif As New PryCifradoNet.ClsCifradoNet
            Dim datos, datosCicloAcad, datosEvaluacionDD As New Data.DataTable
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Try
                cmdGuardarArriba.Enabled = False
                cmdGuardarAbajo.Enabled = False
                Dim cup, id_Alu As String
                id_Alu = Session("codigo_alu")
                cup = Session("EAD_codigocup")

                objcnx.AbrirConexion()
                datosCicloAcad = objcnx.TraerDataTable("ConsultarCicloAcademico", "CV", "1")
                objcnx.CerrarConexion()
                If datosCicloAcad.Rows.Count > 0 Then
                    hddcodigo_cac.Value = datosCicloAcad.Rows(0).Item("codigo_cac")
                    lblSemestre.Text = "Semestre " & datosCicloAcad.Rows(0).Item("descripcion_cac").ToString
                    objcnx.AbrirConexion()
                    datosEvaluacionDD = objcnx.TraerDataTable("EAD_ConsultarEvaluacionVigenteXTipo", "DD")
                    objcnx.CerrarConexion()
                    If datosEvaluacionDD.Rows.Count > 0 Then
                        objcnx.AbrirConexion()
                        datos = objcnx.TraerDataTable("EAD_ConsultarCursoEvaluacionY", CInt(cup))
                        objcnx.CerrarConexion()
                        If datos.Rows.Count > 0 Then
                            gvDesempenio.DataSource = datos
                            lblCurso.Text = datos.Rows(0).Item("nombre_Cur").ToString
                        Else
                            gvDesempenio.DataSource = Nothing
                        End If
                        gvDesempenio.DataBind()
                    Else
                        ClientScript.RegisterStartupScript(Me.GetType, "Encuesta", "alert('No existe Encuesta Pendiente');", True)
                    End If
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "ciclo no vigente", "alert('No existe un ciclo académico vigente');", True)
                End If
                objcnx = Nothing
                cmdGuardarAbajo.Visible = False
                cmdGuardarArriba.Visible = False
                txtPVeinte.Visible = False
                lblPVeinte.Visible = False
                Me.lblComentarioPVeinte.Visible = False
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


                    ctrOpciones.Items.Item(i).Text = "" & i + 1
                    If ctrOpciones.Items.Item(i).Value = 0 Then
                        ctrOpciones.Items.Item(i).Text = "NS/NA"
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

    Protected Sub gvDesempenio_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDesempenio.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim objcnx As New ClsConectarDatos
            Dim datos As New Data.DataTable

            Dim Id_Alu As String
            Id_Alu = Session("codigo_alu")

            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvDesempenio','Select$" & e.Row.RowIndex & "');")
            e.Row.Style.Add("cursor", "hand")

            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()
            datos = objcnx.TraerDataTable("EAD_ConsultarPuntajeDesempenio", "E", CInt(fila.Row("codigo_cup")), CInt(Id_Alu), CInt(fila.Row("codigo_cac")), CInt(fila.Row("codigo_per")))

            objcnx.CerrarConexion()
            If datos.Rows.Count > 0 Then
                e.Row.Cells(5).Text = datos.Rows(0).Item("puntaje")
                e.Row.Cells(6).Text = datos.Rows(1).Item("puntaje")
                e.Row.Cells(7).Text = datos.Rows(2).Item("puntaje")
                e.Row.Cells(8).Text = datos.Rows(3).Item("puntaje")
                e.Row.Cells(9).Text = datos.Rows(4).Item("puntaje")
                e.Row.Cells(10).Text = datos.Rows(5).Item("puntaje")
                e.Row.Cells(11).Text = datos.Rows(6).Item("puntaje")

                e.Row.Cells(5).ForeColor = Drawing.Color.Blue
                e.Row.Cells(6).ForeColor = Drawing.Color.Blue
                e.Row.Cells(7).ForeColor = Drawing.Color.Blue
                e.Row.Cells(8).ForeColor = Drawing.Color.Blue
                e.Row.Cells(9).ForeColor = Drawing.Color.Blue
                e.Row.Cells(10).ForeColor = Drawing.Color.Blue
                e.Row.Cells(11).ForeColor = Drawing.Color.Blue
            Else
                e.Row.Cells(0).Font.Bold = True
                e.Row.Cells(1).Font.Bold = True
            End If
        End If
    End Sub

    Protected Sub gvDesempenio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDesempenio.SelectedIndexChanged
        Dim objcnx As New ClsConectarDatos
        Dim codigo_cup As Int32 = gvDesempenio.DataKeys.Item(Me.gvDesempenio.SelectedIndex).Values("codigo_cup")
        Dim codigo_per As Int32 = gvDesempenio.DataKeys.Item(Me.gvDesempenio.SelectedIndex).Values("codigo_per")
        Dim datos, datosPVeinte As New Data.DataTable

        Dim Id_Alu As String
        Id_Alu = Session("codigo_alu")

        objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objcnx.AbrirConexion()
        datos = objcnx.TraerDataTable("EAD_ConsultarCursoEvaluado", codigo_cup, codigo_per, "E", CInt(Id_Alu))
        objcnx.CerrarConexion()
        If datos.Columns.Count = 1 Then
            If datos.Rows(0).Item("codigo_cev") IsNot DBNull.Value Then
                hddCodigo_cev.Value = datos.Rows(0).Item("codigo_cev")
                objcnx.AbrirConexion()
                gvPreguntas.DataSource = objcnx.TraerDataTable("EAD_ConsultarEvaluacionDesempenio", "E", hddCodigo_cev.Value)
                datosPVeinte = objcnx.TraerDataTable("EAD_ConsultarEvaluacionDesempenioxPregunta", "E", "20.", hddCodigo_cev.Value)
                objcnx.CerrarConexion()
                cmdGuardarAbajo.Enabled = True
                cmdGuardarArriba.Enabled = True
                cmdGuardarAbajo.Visible = True
                cmdGuardarArriba.Visible = True
                If datosPVeinte.Rows.Count > 0 Then
                    Me.txtPVeinte.Visible = True
                    Me.lblPVeinte.Visible = True
                    Me.lblPVeinte.Text = datosPVeinte.Rows(0).Item("numero_eva").ToString + " " + datosPVeinte.Rows(0).Item("pregunta_eva").ToString
                    Me.HddPVeinte.Value = CInt(datosPVeinte.Rows(0).Item("codigo_eva"))
                    Me.lblComentarioPVeinte.Visible = True
                End If
            Else
                cmdGuardarAbajo.Visible = False
                cmdGuardarArriba.Visible = False
            End If
        Else
            If datos.Rows.Count > 0 Then
                gvPreguntas.DataSource = Nothing
                gvPreguntas.EmptyDataText = "El docente seleccionado del curso ya fue evaluado selecione otro docente a evaluar"
                gvPreguntas.EmptyDataRowStyle.ForeColor = Drawing.Color.Red
                Me.txtPVeinte.Visible = False
                Me.lblPVeinte.Visible = False
                Me.lblComentarioPVeinte.Visible = False
                cmdGuardarAbajo.Visible = False
                cmdGuardarArriba.Visible = False
            End If
        End If
        gvPreguntas.DataBind()
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarAbajo.Click, cmdGuardarArriba.Click
        Dim objcnx As New ClsConectarDatos
        Try
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            Dim codigo_eva, codigo_eed, valoresDevueltos(1) As Int32
            Dim codigo_res, i As Int16
            Dim codigo_cup As Int32 = gvDesempenio.DataKeys.Item(Me.gvDesempenio.SelectedIndex).Values("codigo_cup")
            Dim codigo_cac As Int32 = gvDesempenio.DataKeys.Item(Me.gvDesempenio.SelectedIndex).Values("codigo_cac")
            Dim codigo_per As Int32 = gvDesempenio.DataKeys.Item(Me.gvDesempenio.SelectedIndex).Values("codigo_per")
            Dim conrespuesta_eva As String
            Dim lista As New RadioButtonList
            Dim datos As New Data.DataTable

            Dim id_Alu As String
            id_Alu = Session("codigo_alu").ToString


            objcnx.IniciarTransaccion()
            objcnx.Ejecutar("EAD_AgregarEncuestaEvaluacionDD", CInt(id_Alu), CInt(codigo_cup), codigo_per, "E", hddCodigo_cev.Value, 0).copyto(valoresDevueltos, 0)
            codigo_eed = valoresDevueltos(0)
            If codigo_eed > 0 Then
                For i = 1 To gvPreguntas.Rows.Count
                    codigo_eva = gvPreguntas.DataKeys.Item(i - 1).Values(0)
                    conrespuesta_eva = gvPreguntas.DataKeys.Item(i - 1).Values(1)
                    If conrespuesta_eva <> "N" Then
                        lista = gvPreguntas.Controls(0).Controls(i).Controls(2).Controls(0)
                        codigo_res = lista.SelectedValue
                        objcnx.Ejecutar("EAD_AgregarRespuestaEvaluacion", codigo_eva, codigo_res, codigo_eed)
                    End If
                Next
                objcnx.Ejecutar("EAD_AgregarRespuestaAbiertaEvaluacion", HddPVeinte.Value, Me.txtPVeinte.Text, codigo_eed)
                objcnx.TerminarTransaccion()
            Else
                objcnx.AbortarTransaccion()
                ClientScript.RegisterStartupScript(Me.GetType, "no habilitado", "alert('Usted ya contestó la evaluación para este curso');", True)
            End If
            cmdGuardarAbajo.Enabled = False
            cmdGuardarArriba.Enabled = False
            datos = Nothing
            objcnx.AbrirConexion()
            datos = objcnx.TraerDataTable("EncuestaEstudiantePendiente", CInt((id_Alu)), CInt((codigo_cup)), hddcodigo_cac.Value)
            If datos.Rows.Count > 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "Encuesta", "alert('Se guardaron correctamente los datos, gracias por llenar la encuesta.');location.href = '" & pagina & " '; ", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "Encuesta", "alert('Terminaste correctamente todas las encuestas, VUELVE a ingresar al campus virtual');", True)
            End If
            objcnx.CerrarConexion()
            objcnx = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error 1')", True)
        End Try
    End Sub
End Class

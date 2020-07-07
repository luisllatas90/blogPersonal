
Partial Class _EvaluacionDocente_Docente
    Inherits System.Web.UI.Page
    Public pagina As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pagina = "http://server-test/campusvirtual/personal/listaaplicaciones.asp"

        'EN REAL
        pagina = "../../../../personal/listaaplicaciones.asp"

        Dim texto As String
        texto = Date.Now.DayOfWeek.ToString
        If (texto = "Saturday" Or texto = "Sábado" Or texto = "Sunday" Or texto = "Domingo" Or texto = "Sabado") Then
            txtObligatorio.InnerHtml = "<span style='color:red;font-weight:bold;'>Encuesta Obligatoria</span><br>Para ingresar al campus virtual, antes debe responder la encuesta."
        Else
            txtObligatorio.InnerHtml = "<a style ='text-decoration:none;' href='" & pagina & "'><span style='color:green;font-weight:bold;text-decoration:none;'>Encuesta Opcional</span><br>Clic aquí para responder después.</a>"
        End If

        If Not IsPostBack Then
            Dim objcnx As New ClsConectarDatos
            Dim ObjCif As New PryCifradoNet.ClsCifradoNet
            Dim datosCarga, datosCicloAcad, datosEvaluacionDD As New Data.DataTable
            Dim datoscurso As Data.DataTable
            Dim cod_per As Integer
            Dim id As String

            Try
                objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                cod_per = CInt(Session("id_per"))
                cmdGuardarArriba.Enabled = False
                cmdGuardarAbajo.Enabled = False
                objcnx.AbrirConexion()
                datosCicloAcad = objcnx.TraerDataTable("ConsultarCicloAcademico", "CV", "1")
                objcnx.CerrarConexion()
                lblEncuesta.Visible = False

                If datosCicloAcad.Rows.Count > 0 Then
                    hddcodigo_cac.Value = datosCicloAcad.Rows(0).Item("codigo_cac")
                    lblSemestre.Text = "Semestre " & datosCicloAcad.Rows(0).Item("descripcion_cac").ToString
                    objcnx.AbrirConexion()
                    datosEvaluacionDD = objcnx.TraerDataTable("EAD_ConsultarEvaluacionVigenteXTipo", "DD")
                    datoscurso = objcnx.TraerDataTable("ConsultarCursosEncuestaDocente", cod_per)
                    objcnx.CerrarConexion()
                    If datosEvaluacionDD.Rows.Count > 0 Then
                        objcnx.AbrirConexion()
                        datosCarga = objcnx.TraerDataTable("ConsultarCursosEncuestaDocente", cod_per)
                        objcnx.CerrarConexion()
                        If datosCarga.Rows.Count > 0 Then
                            txtProfesor.Text = datosCarga.Rows(0).Item("docente").ToString
                            gvDesempenio.DataSource = datosCarga
                            lblEncuesta.Visible = False
                        Else
                            gvDesempenio.DataSource = Nothing
                            lblEncuesta.Visible = True
                        End If
                        gvDesempenio.DataBind()
                    Else
                        lblEncuesta.Visible = True
                    End If

                    ' Me.lblPendientes.Text = datoscurso.Select("codigo_eed = 0").Length
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "ciclo no vigente", "alert('No existe un ciclo académico vigente')", True)
                End If

                objcnx = Nothing
                cmdGuardarAbajo.Visible = False
                cmdGuardarArriba.Visible = False
                txtPVeinte.Visible = False
                Me.lblComentarioPVeinte.Visible = False
                lblPVeinte.Visible = False

            Catch ex As Exception
                Response.Write(ex.Message)
                ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error')", True)
            End Try
        End If

    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarAbajo.Click, cmdGuardarArriba.Click
        Dim objcnx As New ClsConectarDatos
        objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            If Me.txtPVeinte.Text.length > 3 Then

                Dim codigo_eva, codigo_eed, valoresDevueltos(1) As Int32
                Dim codigo_res, i As Int16
                Dim codigo_cup As Int32 = gvDesempenio.DataKeys.Item(Me.gvDesempenio.SelectedIndex).Values(0)
                Dim codigo_cac As Int32 = gvDesempenio.DataKeys.Item(Me.gvDesempenio.SelectedIndex).Values(1)
                Dim conrespuesta_eva As String
                Dim lista As New RadioButtonList
                Dim datos As New Data.DataTable
                Dim id As String
                Dim cod_per As Integer
                Dim ObjCif As New PryCifradoNet.ClsCifradoNet

                cod_per = CInt(Session("id_per"))

                objcnx.IniciarTransaccion()
                objcnx.Ejecutar("EAD_AgregarEncuestaEvaluacionDD", cod_per, codigo_cup, cod_per, "D", hddCodigo_cev.Value, 0).copyto(valoresDevueltos, 0)
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
                End If

                objcnx.AbrirConexion()
                gvDesempenio.DataSource = objcnx.TraerDataTable("EAD_ConsultarCargaAcademica", "C", cod_per, hddcodigo_cac.Value)
                objcnx.CerrarConexion()
                gvDesempenio.DataBind()
                cmdGuardarAbajo.Enabled = False
                cmdGuardarArriba.Enabled = False
                Me.lblPVeinte.Enabled = False
                Me.txtPVeinte.Enabled = False
                Me.txtPVeinte.Text = ""
                'ClientScript.RegisterStartupScript(Me.GetType, "Encuesta", "alert('Se guardaron correctamente los datos');", True)

                ''#### Verificación de evaluacion en cursos asignados como carga académica ####
                'Dim datoscurso As Data.DataTable
                'objcnx.AbrirConexion()
                'datoscurso = objcnx.TraerDataTable("EAD_ConsultarCursoEvaluacionDocente", cod_per)
                'objcnx.CerrarConexion()

                'If Request.QueryString("mo") = "L" Then
                '    Dim sw As Integer
                '    sw = 0
                '    i = 0
                '    Do
                '        If (CDbl(datoscurso.Rows(i).Item("codigo_eed")) = 0) Then
                '            sw = 1
                '        End If
                '        i = i + 1
                '    Loop While (sw = 0 And i < datoscurso.Rows.Count)
                '    Me.lblPendientes.Text = datoscurso.Select("codigo_eed = 0").Length
                '    datoscurso = Nothing
                '    If (sw = 0) Then
                '        ClientScript.RegisterStartupScript(Me.GetType, "redirecionar1", "location.href='../../../listaaplicaciones.asp';", True)
                '    End If
                'End If
                objcnx = Nothing
                ClientScript.RegisterStartupScript(Me.GetType, "Encuesta", "alert('Se guardaron correctamente los datos, gracias por llenar la encuesta.');location.href = '" & pagina & " '; ", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ingrese un comentario en la pregunta 20');", True)
            End If
        Catch ex As Exception
            objcnx.AbortarTransaccion()
            'Response.Write(ex.Message)
            'ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error 2');", True)
        End Try
    End Sub


    Protected Sub gvDesempenio_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDesempenio.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim objcnx As New ClsConectarDatos
            Dim datos As New Data.DataTable
            Dim id As String
            Dim ObjCif As New PryCifradoNet.ClsCifradoNet
            Dim cod_per As Integer

 
            cod_per = CInt(Session("id_per"))

            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvDesempenio','Select$" & e.Row.RowIndex & "');")
            e.Row.Style.Add("cursor", "hand")

            'Consultar puntaje obtenido
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()
            datos = objcnx.TraerDataTable("EAD_ConsultarPuntajeDesempenio", "D", CInt(fila.Row("codigo_cup")), cod_per, CInt(fila.Row("codigo_cac")), cod_per)

            objcnx.CerrarConexion()
            If datos.Rows.Count > 0 Then
                e.Row.Cells(6).Text = datos.Rows(0).Item("puntaje")
                e.Row.Cells(7).Text = datos.Rows(1).Item("puntaje")
                e.Row.Cells(8).Text = datos.Rows(2).Item("puntaje")
                e.Row.Cells(9).Text = datos.Rows(3).Item("puntaje")
                e.Row.Cells(10).Text = datos.Rows(4).Item("puntaje")
                e.Row.Cells(11).Text = datos.Rows(5).Item("puntaje")
                e.Row.Cells(12).Text = datos.Rows(6).Item("puntaje")

                e.Row.Cells(6).ForeColor = Drawing.Color.Blue
                e.Row.Cells(7).ForeColor = Drawing.Color.Blue
                e.Row.Cells(8).ForeColor = Drawing.Color.Blue
                e.Row.Cells(9).ForeColor = Drawing.Color.Blue
                e.Row.Cells(10).ForeColor = Drawing.Color.Blue
                e.Row.Cells(11).ForeColor = Drawing.Color.Blue
                e.Row.Cells(12).ForeColor = Drawing.Color.Blue
            End If
        End If
    End Sub

    Protected Sub gvDesempenio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDesempenio.SelectedIndexChanged
        Dim objcnx As New ClsConectarDatos
        Dim codigo_cup As Int32 = gvDesempenio.DataKeys.Item(Me.gvDesempenio.SelectedIndex).Values(0)
        Dim datos, datosPVeinte As New Data.DataTable
        Dim id As String
        Dim ObjCif As New PryCifradoNet.ClsCifradoNet
        Dim cod_per As Integer

       
        Try
            cod_per = CInt(Session("id_per"))

            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()
            datos = objcnx.TraerDataTable("EAD_ConsultarCursoEvaluado", codigo_cup, cod_per, "D", cod_per)
            objcnx.CerrarConexion()
            If datos.Rows.Count = 1 Then
                If datos.Rows(0).Item("codigo_cev") IsNot DBNull.Value Then
                    hddCodigo_cev.Value = datos.Rows(0).Item("codigo_cev")
                    objcnx.AbrirConexion()
                    gvPreguntas.DataSource = objcnx.TraerDataTable("EAD_ConsultarEvaluacionDesempenio", "D", hddCodigo_cev.Value)
                    datosPVeinte = objcnx.TraerDataTable("EAD_ConsultarEvaluacionDesempenioxPregunta", "D", "20.", hddCodigo_cev.Value)
                    objcnx.CerrarConexion()
                    cmdGuardarAbajo.Enabled = True
                    cmdGuardarAbajo.Visible = True
                    cmdGuardarArriba.Enabled = True
                    cmdGuardarArriba.Visible = True
                    lblEncuesta.Visible = False
                    If datosPVeinte.Rows.Count > 0 Then
                        Me.lblComentarioPVeinte.Visible = True
                        Me.txtPVeinte.Visible = True
                        Me.lblPVeinte.Visible = True
                        Me.lblPVeinte.Enabled = True
                        Me.txtPVeinte.Enabled = True
                        Me.HddPVeinte.Value = CInt(datosPVeinte.Rows(0).Item("codigo_eva"))
                    End If
                Else
                    lblEncuesta.Visible = True
                    cmdGuardarAbajo.Visible = False
                    cmdGuardarArriba.Visible = False
                End If
            Else
                If datos.Rows.Count > 0 Then
                    gvPreguntas.DataSource = Nothing
                    gvPreguntas.EmptyDataText = "Este curso ya ha sido evaluado por usted, seleccione otro curso a evaluar"
                    gvPreguntas.EmptyDataRowStyle.ForeColor = Drawing.Color.Red
                    cmdGuardarAbajo.Visible = False
                    cmdGuardarArriba.Visible = False
                    Me.lblComentarioPVeinte.Visible = False
                    Me.txtPVeinte.Visible = False
                    Me.lblPVeinte.Visible = False
                End If
            End If
            gvPreguntas.DataBind()
            objcnx = Nothing
        Catch ex As Exception
            '  Response.Write(ex.Message)
            ' ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error 3')", True)
        End Try
    End Sub
 

    Protected Sub gvPreguntas_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPreguntas.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ctrOpciones As New RadioButtonList
            Dim ctrValidar As New RequiredFieldValidator
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem

            e.Row.Cells(2).ColumnSpan = 5
            'e.Row.Cells(2).Width = 100
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Visible = False
            'e.Row.Cells(6).Width = 40
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
                ctrValidar.Text = "*"
                ctrValidar.ErrorMessage = "Faltan preguntas por responder"
                ctrValidar.ForeColor = Drawing.Color.Red
                e.Row.Cells(2).Controls.Add(ctrOpciones)
                e.Row.Cells(2).Controls.Add(ctrValidar)
            Else
                e.Row.Cells(0).Font.Bold = True
                e.Row.Cells(1).Font.Bold = True
            End If

        End If
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        'redireccion = "location.href='../../../../personal/listaaplicaciones.asp'"
        'ClientScript.RegisterStartupScript(Me.GetType, "redirecionar1", redireccion, True)
        Response.Redirect("../../../../personal/listaaplicaciones.asp")
    End Sub
End Class

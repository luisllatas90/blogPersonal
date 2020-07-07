﻿'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class EvaluacionDocente_Estudiante
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objcnx As New ClsConectarDatos
            Dim ObjCif As New PryCifradoNet.ClsCifradoNet
            Dim datos, datosCicloAcad, datosEvaluacionDD As New Data.DataTable
            Dim dtDatosTutorado As Data.DataTable
            hddCodigo_cev.Value = 14
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Try
                cmdGuardarArriba.Enabled = False
                cmdGuardarAbajo.Enabled = False

                Dim id_Alu As Integer
                id_Alu = CInt(Session("codigo_alu"))

                '#ConsultarDatos de la Encuesta     
                objcnx.AbrirConexion()
                dtDatosTutorado = objcnx.TraerDataTable("TutoriaConsultarDatos", id_Alu, CInt(Session("codigo_cac")))
                objcnx.CerrarConexion()
                If dtDatosTutorado.Rows.Count Then
                    Me.lblEstudiante.Text = dtDatosTutorado.Rows(0).Item("Estudiante").ToString
                    Me.lblTutor.Text = dtDatosTutorado.Rows(0).Item("Tutor").ToString
                    Me.lblEscuela.Text = dtDatosTutorado.Rows(0).Item("Escuela").ToString

                    Session("codigo_per") = dtDatosTutorado.Rows(0).Item("codigo_per").ToString
                End If
                dtDatosTutorado.Dispose()

                objcnx.AbrirConexion()
                datosCicloAcad = objcnx.TraerDataTable("ConsultarCicloAcademico", "CV", "1")
                objcnx.CerrarConexion()
                If datosCicloAcad.Rows.Count > 0 Then
                    hddcodigo_cac.Value = datosCicloAcad.Rows(0).Item("codigo_cac")
                    lblSemestre.Text = "Semestre " & datosCicloAcad.Rows(0).Item("descripcion_cac").ToString
                    objcnx.AbrirConexion()
                    datosEvaluacionDD = objcnx.TraerDataTable("EAD_ConsultarEvaluacionVigenteXTipo", "TU")
                    objcnx.CerrarConexion()
                    If datosEvaluacionDD.Rows.Count > 0 Then
                        objcnx.AbrirConexion()
                        cmdGuardarArriba.Enabled = True
                        cmdGuardarAbajo.Enabled = True
                        gvPreguntas.DataSource = objcnx.TraerDataTable("EAD_ConsultarEvaluacionDesempenioTUTORIA", "1", 9)
                        gvPreguntas.DataBind()
                        gvPreguntas0.DataSource = objcnx.TraerDataTable("EAD_ConsultarEvaluacionDesempenioTUTORIA", "2", 9)
                        gvPreguntas0.DataBind()
                        objcnx.CerrarConexion()
                    Else
                        cmdGuardarArriba.Enabled = False
                        cmdGuardarAbajo.Enabled = False
                    End If
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "ciclo no vigente", "alert('No existe un ciclo académico vigente');", True)
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
                        If i < 5 Then
                            ctrOpciones.Items.Item(i).Value = i + 1
                        Else
                            ctrOpciones.Items.Item(i).Value = 0
                        End If
                    Else
                        If i < 5 Then
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
        Try
            Dim codigo_eva, codigo_eed, valoresDevueltos(1) As Int32
            Dim codigo_res, i As Int16
            Dim codigo_per As Int32 = CInt(Session("codigo_per"))
            Dim conrespuesta_eva As String
            Dim lista As New RadioButtonList
            Dim datos As New Data.DataTable
            objcnx.IniciarTransaccion()
            objcnx.Ejecutar("EAD_AgregarEncuestaEvaluacionDD", CInt(Session("codigo_alu")), 0, codigo_per, "E", hddCodigo_cev.Value, 0).copyto(valoresDevueltos, 0)
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

                For i = 1 To gvPreguntas0.Rows.Count
                    codigo_eva = gvPreguntas0.DataKeys.Item(i - 1).Values(0)
                    conrespuesta_eva = gvPreguntas0.DataKeys.Item(i - 1).Values(1)
                    If conrespuesta_eva <> "N" Then
                        lista = gvPreguntas0.Controls(0).Controls(i).Controls(2).Controls(0)
                        codigo_res = lista.SelectedValue
                        objcnx.Ejecutar("EAD_AgregarRespuestaEvaluacion", codigo_eva, codigo_res, codigo_eed)
                    End If
                Next
                objcnx.TerminarTransaccion()
            Else
                objcnx.AbortarTransaccion()

                ClientScript.RegisterStartupScript(Me.GetType, "no habilitado", "alert('Usted ya contestó la evaluación ');", True)
            End If
            cmdGuardarAbajo.Enabled = False
            cmdGuardarArriba.Enabled = False
            datos = Nothing
            Dim redireccion As String
            redireccion = "location.href='../../'"
            ClientScript.RegisterStartupScript(Me.GetType, "correcto", "alert('Gracias por tu colaboración, vuelve a ingresar al Campus Virtual USAT');", True)
            Session("codigo_alu") = ""
            ClientScript.RegisterStartupScript(Me.GetType, "redirecionar1", redireccion, True)
        Catch ex As Exception
            objcnx.AbortarTransaccion()
            Response.Write(":Error:::" & ex.Message)
        End Try
    End Sub

    Protected Sub gvPreguntas0_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPreguntas0.RowCreated
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
            If gvPreguntas0.DataKeys.Item(e.Row.RowIndex).Values(1) <> "N" Then
                Dim NUM As Int16 = 4

                For i As Int16 = 0 To NUM
                    ctrOpciones.Items.Add(" ")
                    ctrOpciones.RepeatDirection = RepeatDirection.Horizontal
                    If gvPreguntas0.DataKeys.Item(e.Row.RowIndex).Values(2) = "A" Then
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
End Class

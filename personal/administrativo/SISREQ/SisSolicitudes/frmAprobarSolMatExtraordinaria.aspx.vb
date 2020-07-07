
Partial Class administrativo_SISREQ_SisSolicitudes_frmConfirmarPagoSolicitudes
    Inherits System.Web.UI.Page


    Protected Sub gvLista_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvLista.SelectedIndexChanged
        Dim Ruta As New EncriptaCodigos.clsEncripta
        Dim obj As New clsTramitesSolicitudes
        Dim datos As New Data.DataTable

        Me.txtCodigoUni.Text = gvLista.SelectedRow.Cells(3).Text
        'Me.txtNombre.Text = gvLista.SelectedRow.Cells(4).Text.ToUpper
        'Me.txtCarrera.Text=(Me.gvLista.SelectedDataKey.Item().ToString
        Me.txtPlan.Text = Me.gvLista.SelectedDataKey.Item(4).ToString
        Me.txtCiclo.Text = Me.gvLista.SelectedDataKey.Item(3).ToString
        Me.txtCarrera.Text = Me.gvLista.SelectedDataKey.Item(5).ToString
        Me.txtSol.Text = Me.gvLista.SelectedRow.Cells(1).Text

        Me.imgFoto.ImageUrl = "//intranet.usat.edu.pe/imgestudiantes/" & Ruta.CodificaWeb("069" & gvLista.SelectedRow.Cells(3).Text)
        Session("foto") = Me.imgFoto.ImageUrl
        imgFoto.Width = 118
        imgFoto.BorderColor = Drawing.Color.Black
        imgFoto.Height = 120

        '-----------------------------------------------------------------------------------------------'
        datos = obj.ConsultarCursosExamenExtraordinario(Me.gvLista.SelectedDataKey.Item(0).ToString, _
                                                      Me.gvLista.SelectedDataKey.Item(2).ToString)
        Me.gvListaCursos.DataSource = datos
        Me.gvListaCursos.DataBind()


        If Me.gvLista.SelectedDataKey.Item(2).ToString = "P" Then
            HabilitarOpcionesEvaluacion(True)
        End If
        If Me.gvLista.SelectedDataKey.Item(2).ToString = "A" Then
            HabilitarOpcionesEvaluacion(False)
            Me.gvListaCursos.Enabled = True

        End If
      
        'ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmAprobarSolMatExtraordinaria.aspx?id=" & Request.QueryString("id") & "';", True)
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim obj As New clsTramitesSolicitudes
        Dim objTdt As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim objfun As New ClsFunciones

        Dim datos As New Data.DataTable
        Dim codigo_cpf As String
        Dim resultados As New Data.DataTable
        Try

            datos = objTdt.TraerDataTable("SOL_ConsultarCodCpfPorResponsable", Request.QueryString("id"))
            codigo_cpf = datos.Rows(0).Item("codigo_Cpf").ToString
            'Response.Write(codigo_cpf)
            Me.txtCod_Cco.Text = datos.Rows(0).Item("codigo_Cco").ToString
            If Me.cboTipo.SelectedValue = 1 Then
                resultados = objTdt.TraerDataTable("SOL_ConsultarSolExamExtraPendientes", CInt(codigo_cpf), "P", _
                                             Me.cboEstado.SelectedValue, _
                                             txtParametroBusqueda.Text)
            Else
                resultados = objTdt.TraerDataTable("SOL_ConsultarSolExamExtraPendientes", CInt(codigo_cpf), "A", _
                                                             Me.cboEstado.SelectedValue, _
                                                             txtParametroBusqueda.Text)
            End If
          
            Me.gvLista.DataSource = resultados
            Me.gvLista.DataBind()
            objfun.CargarListas(Me.dpCodigo_cac, objTdt.TraerDataTable("ConsultarCicloAcademico", "TO", 0), "codigo_cac", "descripcion_cac")
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            datos = Nothing
            resultados = Nothing
            obj = Nothing
            objTdt = Nothing
            objfun = Nothing
        End Try
    End Sub

    Protected Sub gvListaCursos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvListaCursos.SelectedIndexChanged

        Dim objTdt As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim obj As New clsTramitesSolicitudes
        If Me.gvListaCursos.SelectedDataKey.Item(2).ToString = "P" Then

            Dim datos As New Data.DataTable
            Dim estado As New Data.DataTable
            Dim codigo_Cac As Integer
            Dim codigo_Pes As Int16
            Dim codigo_Cur As Integer
            Dim codigo_Cup As New Data.DataTable
            Dim detalle_Mat As New Data.DataTable
            Dim codigo_Mat As New Data.DataTable
            Dim codigo_Sol As String
            Dim codigo_Alu As String
            Dim total_Creditos As Integer = 0
            Dim suma_Total As Decimal = 0.0
            Dim cantFilasCursos As Integer
            Dim codigo_Per As String
            Dim fechaFin, fechaRetiro, fechaInicio As Date
            Dim CodigoCup_Reg As Integer = Nothing
            Dim CodigoMat_Reg As Integer = Nothing
            Dim codigo_Deu As New Data.DataTable

            fechaInicio = DateTime.Now.ToString
            fechaFin = fechaInicio.AddDays(40)
            fechaRetiro = fechaFin

            codigo_Pes = Me.gvLista.SelectedDataKey.Item(6).ToString
            codigo_Sol = Me.gvLista.SelectedDataKey.Item(0).ToString()
            codigo_Alu = Me.gvLista.SelectedDataKey.Item(1).ToString()
            Me.txtCodigoAlu.Text = codigo_Alu
            cantFilasCursos = Me.gvListaCursos.Rows.Count
            codigo_Per = Request.QueryString("id")
            codigo_Cur = Me.gvListaCursos.SelectedDataKey.Item(1)


            'CONSULTAMOS SI EL ESTUDIANTE ESTA MATRICULADO EN EL CICLO ACADEMICO SELECIONADO
            If Me.gvLista.SelectedDataKey.Item(7).ToString = Nothing Then
                codigo_Cac = Me.dpCodigo_cac.SelectedValue
                codigo_Mat = obj.ConsultarMatriculaEstudiante(codigo_Alu, codigo_Cac)
                CodigoMat_Reg = codigo_Mat.Rows(0).Item("Codigo").ToString
                Response.Write("CodigoCac: " & codigo_Cac)
                Response.Write("CodigoMat: " & CodigoMat_Reg)
            Else
                codigo_Cac = gvLista.SelectedDataKey.Item(7).ToString
                Response.Write("CodigoCac: " & codigo_Cac)
            End If


            'Try 
            ' objTdt.IniciarTransaccion()
            If Me.cboAprobacion.SelectedValue = "A" Then

                objTdt.IniciarTransaccion()

                codigo_Cup = objTdt.TraerDataTable("SOL_ProgramarCursoExamenExtraordinario", 1, codigo_Cac, _
                                           codigo_Pes, codigo_Cur, 1, "X", fechaInicio, fechaFin, _
                                          fechaRetiro, codigo_Per, "PROGRAMACIÓN POR EXAMEN EXTRAORDINARIO", 0, _
                                           CInt(Me.txtCod_Cco.Text), 0)
                CodigoCup_Reg = codigo_Cup.Rows(0).Item("codigo_Cup").ToString
                total_Creditos = codigo_Cup.Rows(0).Item("creditos_Cur")


                Response.Write("codCup: " & CodigoCup_Reg)
                Response.Write(" total créditos: " & total_Creditos)

                If CodigoMat_Reg = 0 Then
                    '            'Matriculamos'
                    'codigo_Mat = objTdt.TraerDataTable("SOL_RegistrarMatriculaExamenExtraordinario", "X", fechaInicio, _
                    '                             codigo_Alu, CInt(Me.dpCodigo_cac.Text), "P", _
                    '                             "Matrícula Examen Extraordinario/Web", codigo_Per, "N", _
                    '                             "Solicitud Web de Examen Extraordinario", "X", total_Creditos, 250)
                    'CodigoMat_Reg = codigo_Mat.Rows(0).Item("codigo_Mat")
                    'Response.Write("codMat: " & CodigoMat_Reg)
                    ''Registramos Detalle Matricula'
                    'detalle_Mat = objTdt.TraerDataTable("SOL_RegistrarDetalleMatriculaExamExtraordinario", CodigoMat_Reg, _
                    '                        CodigoCup_Reg, total_Creditos, fechaInicio, codigo_Per, _
                    '                       codigo_Cur, codigo_Pes, codigo_Alu)

                    '            codigo_Deu = objTdt.TraerDataTable("SOL_GenerarDudaExamExtraordinario", fechaInicio, codigo_Alu, codigo_Cac, _
                    '                                                                "Deuda Examen Extraordinario", 250, _
                    '                                                                CInt(Me.txtCod_Cco.Text), codigo_Pes, _
                    '                                                                codigo_Cur, CodigoCup_Reg)

                    '            '  Response.Write("  detMat: " & detalle_Mat.Rows(0).Item("codigo_Dma").ToString)
                    '            '  Response.Write("  DEUDA: " & codigo_Deu.Rows(0).Item("codigo_Deu").ToString)



                    '            estado = objTdt.TraerDataTable("SOL_EvaluarCursosExamExtraordinario", CChar(Me.cboAprobacion.SelectedValue), _
                    '                                    CInt(codigo_Sol), CodigoMat_Reg, CodigoCup_Reg, codigo_Cur, _
                    '                                    codigo_Deu)

                    '            '  Response.Write(" Estado: " & estado.Rows(0).Item("evaluacion").ToString)
                Else

                    '            'Actualizamos creditos e importe de matricula '
                    '
                    'Registramos Detalle Matricula'
                    detalle_Mat = objTdt.TraerDataTable("SOL_RegistrarDetalleMAtriculaExamExtraordinario", CodigoMat_Reg, _
                                             CodigoCup_Reg, total_Creditos, fechaInicio, codigo_Per, _
                                            codigo_Cur, codigo_Pes, codigo_Alu)

                    Response.Write("  detMat: " & detalle_Mat.Rows(0).Item("codigo_Dma").ToString)

                    codigo_Deu = objTdt.TraerDataTable("SOL_GenerarDudaExamExtraordinario", fechaInicio, codigo_Alu, codigo_Cac, _
                                                                       "Deuda Examen Extraordinario", 250, _
                                                                       CInt(Me.txtCod_Cco.Text), codigo_Pes, _
                                                                       codigo_Cur, CodigoCup_Reg)
                    Response.Write("  DEUDA: " & codigo_Deu.Rows(0).Item("codigo_Deu").ToString)

                    objTdt.TraerDataTable("SOL_ActualizarMatriculaExamExtraordinario", CodigoMat_Reg, _
                                               total_Creditos, 250)

                    estado = objTdt.TraerDataTable("SOL_EvaluarCursosExamExtraordinario", "A", _
                                                      CInt(codigo_Sol), CodigoMat_Reg, CodigoCup_Reg, codigo_Cur, _
                                                      codigo_Deu)

                    Response.Write(" Estado: " & estado.Rows(0).Item("evaluacion").ToString)
                End If

            End If

                '    'Catch ex As Exception
                '    ' objTdt.AbortarTransaccion()
                '    'Finally
            objTdt.TerminarTransaccion()
            objTdt = Nothing
                '    datos = Nothing
                '    codigo_Cac = Nothing
                '    codigo_Pes = Nothing
                '    codigo_Cur = Nothing
                '    codigo_Cup = Nothing
                '    detalle_Mat = Nothing
                '    codigo_Mat = Nothing
                '    codigo_Sol = Nothing
                '    codigo_Alu = Nothing
                '    codigo_Deu = Nothing
                '    Me.gvListaCursos.DataBind()
                '    ' End Try
                'Else
                '    ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('El Curso que intenta Aprobar ya se encuentra aprobado');", True)
            End If

            'Dim datosCursos As New Data.DataTable
            'datosCursos = obj.ConsultarCursosExamenExtraordinario(Me.gvLista.SelectedDataKey.Item(0).ToString, _
            '                                      Me.gvLista.SelectedDataKey.Item(2).ToString)
            'Me.gvListaCursos.DataSource = datosCursos
            'Me.gvListaCursos.DataBind()



    End Sub

    Protected Sub lnkDatosEvento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDatosEvento.Click, gvLista.SelectedIndexChanged
        'EnviarAPagina("detalleevento.aspx?mod=" & Request.QueryString("mod"))
        Me.pnlDatosEstudiante.Visible = True
        Me.fradetalle.Visible = False
    End Sub

    Private Sub EnviarAPagina(ByVal pagina As String)
        Me.fradetalle.Attributes("src") = pagina & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cco=" '& Request.QueryString("id")
    End Sub

    Protected Sub lnkVerSolicitud_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkVerSolicitud.Click
        Me.pnlDatosEstudiante.Visible = False
        Me.fradetalle.Visible = True

        ' Me.lnkVerSolicitud.PostBackUrl = ("EvaluacionFinal.aspx?mod=" & Request.QueryString("mod") & "1&id=" & Request.QueryString("id"))

        ' EnviarAPagina("lstinscritoseventocargo.aspx?mod=" & Request.QueryString("mod") & "&tab=1") ' & Request.QueryString("id"))

        Me.fradetalle.Attributes("src") = "EvaluacionFinal.aspx?mod=" & Request.QueryString("mod") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") '& "&cco=" '& Request.QueryString("id")

    End Sub


    Protected Sub lnkHistorial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkHistorial.Click
        Me.pnlDatosEstudiante.Visible = False
        Me.fradetalle.Visible = True

        ' Me.lnkVerSolicitud.PostBackUrl = ("EvaluacionFinal.aspx?mod=" & Request.QueryString("mod") & "1&id=" & Request.QueryString("id"))

        ' EnviarAPagina("lstinscritoseventocargo.aspx?mod=" & Request.QueryString("mod") & "&tab=1") ' & Request.QueryString("id"))

        Me.fradetalle.Attributes("src") = "../../../librerianet/academico/historial_personal.aspx?mod=" & Request.QueryString("mod") & "&id=" & Request.QueryString("id") & "&ctf=1" '& "&cco=" & CInt(Me.txtCodigoAlu.Text)

    End Sub

    Protected Sub lnkDeudas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeudas.Click

        Me.pnlDatosEstudiante.Visible = False
        Me.fradetalle.Visible = True

        ' Me.lnkVerSolicitud.PostBackUrl = ("EvaluacionFinal.aspx?mod=" & Request.QueryString("mod") & "1&id=" & Request.QueryString("id"))

        ' EnviarAPagina("lstinscritoseventocargo.aspx?mod=" & Request.QueryString("mod") & "&tab=1") ' & Request.QueryString("id"))

        Me.fradetalle.Attributes("src") = "../../../librerianet/academico/admincuentaper.aspx?mod=" & Request.QueryString("mod") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") '& "&cco=" '& Request.QueryString("id")

    End Sub

    Protected Sub gvListaCursos_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvListaCursos.RowEditing

        Me.pnlProgramarProfesor.Visible = True
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(Me.dpCodigo_per, obj.TraerDataTable("ConsultarDocente", "ACT", 0, 0), "codigo_per", "docente", "--Seleccione el Profesor--")
        obj.CerrarConexion()
        'ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('El Curso que intenta Aprobar ya se encuentra aprobado');", True)
    End Sub

    Protected Sub btnCalificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalificar.Click
        If Me.gvLista.SelectedDataKey.Item(2).ToString = "A" Then
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('La solicitud que intenta evaluar ya se encuentra Aprobada');", True)
            HabilitarOpcionesEvaluacion(False)
            If cboAprobacion.SelectedValue = "A" Then
                Me.gvListaCursos.Enabled = True
            End If
        Else
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim codigo_Per As Integer
            Dim codigo_Sol As Integer
            codigo_Per = Request.QueryString("id")
            codigo_Sol = Me.gvLista.SelectedDataKey.Item(0).ToString()
            Response.Write("Codigo_Sol " & codigo_Sol)

            obj.Ejecutar("SOL_EvaluarSolicitudExamExtraordinario", 1, codigo_Per, DateTime.Now.ToString, _
                                                                Me.txtObservacion.Text, Me.cboAprobacion.SelectedValue.ToString, _
                                                                1, CInt(codigo_Sol))

            HabilitarOpcionesEvaluacion(False)
            If cboAprobacion.SelectedValue = "A" Then
                Me.gvListaCursos.Enabled = True
            End If
            obj = Nothing
        End If
        
    End Sub

    Public Sub HabilitarOpcionesEvaluacion(ByVal estado As Boolean)
        Me.cboAprobacion.Enabled = estado
        Me.dpCodigo_cac.Enabled = estado
        Me.txtObservacion.Enabled = estado
        Me.btnCalificar.Enabled = estado
    
    End Sub


    Protected Sub gvListaCursos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvListaCursos.RowDeleting

        If Me.gvLista.SelectedDataKey.Item(2).ToString = "P" Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim codigo_Sol As Integer
            Dim CodigoMat_Reg As Integer
            Dim codigo_Cur As Integer


            codigo_Sol = Me.gvLista.SelectedDataKey.Item(0).ToString()
            codigo_Cur = Me.gvListaCursos.SelectedDataKey.Item(1)

            obj.Ejecutar("SOL_EvaluarCursosExamExtraordinario", "R", CInt(codigo_Sol), CodigoMat_Reg, _
                                              0, 0, 0)
            Me.gvListaCursos.DataBind()
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('No se puede rechazar un curso que ya se encuentra aprobado y con deuda generada');", True)
        End If

    End Sub

    'Protected Sub cmdAsignarProfesor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAsignarProfesor.Click
    '    If dpCodigo_per.SelectedValue <> -1 Then
    '        Dim obj As New ClsConectarDatos
    '        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        obj.AbrirConexion()
    '        obj.Ejecutar("CAR_AgregarCargaAcademica", Me.dpCodigo_per.SelectedValue, Me.hdcodigo_cup.Value, Request.QueryString("id"), Request.QueryString("mod"), System.DBNull.Value)
    '        obj.CerrarConexion()
    '        obj = Nothing
    '        'Me.CargarDetalleGrupoHorario(3, Me.hdcodigo_cup.Value, "p")
    '    End If
    'End Sub

    Protected Sub gvLista_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles gvLista.RowUpdated

    End Sub
End Class

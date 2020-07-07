﻿
Partial Class SisSolicitudes_Solicitud
    Inherits System.Web.UI.Page
    Dim total As Double
    Dim mensaje As String = ""
    Public fotoEstudiante As String = ""

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim datos, EstadoCuenta As New Data.DataTable
        datos = Obj.TraerDataTable("SOL_ConsultarAlumnoParaSolicitud", Me.CboBuscarPor.SelectedValue, Me.TxtBuscar.Text)
        If datos.Rows.Count > 0 Then
            Page.RegisterStartupScript("Solicitud", "<script>Panel1.style.visibility ='visible' </script>")
            FvDatos.DataSource = datos
            FvDatos.DataBind()            
            Dim Ruta As New EncriptaCodigos.clsEncripta
            Me.ImgFoto.Visible = True
            Me.FvDatos.Visible = True
            Me.Panel1.Visible = True
            Me.CblSeleccionarRet.Items.Clear()
            Me.CblSeleccionar.Items.Clear()
            Me.cboEscuela.Visible = False
            total = 0
            ImgFoto.Width = 80
            ImgFoto.BorderColor = Drawing.Color.Black
            ImgFoto.BorderWidth = 1
            With datos.Rows(0)
                TxtNumSolicitud.Attributes.Add("onKeyPress", "validarnumero()")
                Me.TxtResponsable.Text = .Item("alumno").ToString
                Me.TxtDireccion.Text = .Item("direccion_dal").ToString
                Me.TxtUrbDis.Text = .Item("urbanizacion_dal").ToString & " - " & .Item("nombre_dis").ToString
                Me.TxtTelefonos.Text = .Item("Telefono").ToString
                Me.TxtEscuela.Text = .Item("nombre_cpf").ToString
                Me.TxtCodMatricula.Text = .Item("codigouniver_alu").ToString
                Session("codigo_pes") = .Item("codigo_pes").ToString
                Session("codigo_alu") = .Item("codigo_alu").ToString
                Me.HddCodigocpf.Value = .Item("codigo_cpf").ToString
                '---------------------------------------------------------------------------------------------------------------
                'Fecha: 29.10.2012
                'Usuario: dguevara
                'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
                '---------------------------------------------------------------------------------------------------------------
                Me.ImgFoto.ImageUrl = "//intranet.usat.edu.pe/imgestudiantes/" & Ruta.CodificaWeb("069" & .Item("codigouniver_alu").ToString)
                Session("foto") = "//intranet.usat.edu.pe/imgestudiantes/" & Ruta.CodificaWeb("069" & .Item("codigouniver_alu").ToString)
            End With

            'EstadoCuenta = Obj.TraerDataTable("ConsultarMovimientosAlumno", datos.Rows(0).Item("codigouniver_alu").ToString, 0, 0, 0, 0, "P")
            'If EstadoCuenta.Rows.Count > 0 Then
            '    Me.GvEstadoCue.DataSource = EstadoCuenta
            '    Me.GvEstadoCue.DataBind()
            'End If



            datos = Obj.TraerDataTable("ConsultarCicloAcademico", "CV", 1)
            If datos.Rows.Count > 0 Then
                Me.TxtSemestre.Text = datos.Rows(0).Item("descripcion_cac").ToString
            End If
            If Me.RblAsunto.Items.Count = 3 Then
                Page.RegisterStartupScript("Msg", "<script>alert('La fecha límite de retiro de asignaturas ya venció por tal motivo esta opción ha sido deshabilitada')</script>")
            End If
            VisualizarControlesRetiros(LblIndicar, CblSeleccionar, Panel2, False)
            VisualizarControlesRetiros(LblIndicarRet, CblSeleccionarRet, PanelRet, False)
            'Me.GvEstadoCue.Visible = True



        Else
            Page.RegisterStartupScript("Sin Registros", "<script>alert('No se encontró ningún estudiante registrado con este código')</script>")
            'Page.RegisterStartupScript("Solicitud", "<script>Panel1.style.visibility ='visible' </script>")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        hfMensaje.Value = VerificarCursoNotaFinal()
        If Me.RblAsunto.SelectedValue.ToString = "2" And Me.hfMensaje.Value.Length Then
            Me.CmdGuardar.Attributes.Add("OnClick", "javascript:if (confirm('" & hfMensaje.Value & "') == false) return false;")
        End If

        If Not IsPostBack Then
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            'Llenar lista tipo motivosolicitudsolicitudcliente
            ClsFunciones.LlenarListas(Me.CblMotivo, Obj.TraerDataTable("SOL_ConsultarTipoMotivoSolicitud", 1, 1), "codigo_mso", "descripcion_mso")
            Me.HddTotalChk.Value = Me.CblMotivo.Items.Count
            'Llenar lista tipo asunto
            ClsFunciones.LlenarListas(Me.RblAsunto, Obj.TraerDataTable("SOL_ConsultarTipoAsuntoSolicitud", 1, 1), "codigo_tas", "descripcion_tas")
            Me.HddTotalRbl.Value = Me.RblAsunto.Items.Count



            Me.TxtFecha.Text = Date.Now.ToShortDateString
        End If
    End Sub

    Protected Sub RblAsunto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RblAsunto.SelectedIndexChanged
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Procedimiento As String = "", Texto As String = ""
        Page.RegisterStartupScript("Solicitud", "<script>Panel1.style.visibility ='visible' </script>")
        Me.RbNo.Checked = True
        Me.TxtSemAnulacion.Text = ""
        Me.CblSeleccionar.ClearSelection()
        Me.CblSeleccionarRet.ClearSelection()
        Me.cvEscuela.Enabled = False
        Me.cboEscuela.Visible = False
        Select Case Me.RblAsunto.SelectedValue
            Case 1, 18
                Me.LblIndicar.Text = ""
                Me.CblSeleccionar.Items.Clear()
                Me.CblSeleccionarRet.Items.Clear()
                Me.HddTotalSel.Value = 0
                Me.HddTotalSelRet.Value = 0
                VisualizarControlesRetiros(LblIndicar, CblSeleccionar, Panel2, False)
                VisualizarControlesRetiros(LblIndicarRet, CblSeleccionarRet, PanelRet, False)
            Case 3, 14 'Retiro de asignatura

                Me.LblIndicar.Text = "Seleccionar asignatura(s)"
                If Me.RblAsunto.SelectedValue = 3 Then
                    ClsFunciones.LlenarListas(Me.CblSeleccionar, Obj.TraerDataTable("ConsultarNotas", "SO", Session("codigo_alu"), "NC", ""), "codigo_cur", "Descripcion")
                Else
                    ClsFunciones.LlenarListas(Me.CblSeleccionar, Obj.TraerDataTable("ConsultarNotas", "SO", Session("codigo_alu"), "CC", ""), "codigo_cur", "Descripcion")
                End If
                Me.HddTotalSel.Value = Me.CblSeleccionar.Items.Count
                Me.HddTotalSelRet.Value = 0
                VisualizarControlesRetiros(LblIndicar, CblSeleccionar, Panel2, True)
                VisualizarControlesRetiros(LblIndicarRet, CblSeleccionarRet, PanelRet, False)

                'Case 6, 7, 13, 17 'Agregado de asignatura - Cambio de grupo horario 
            Case 6, 7, 10, 13, 17 'Agregado de asignatura - Cambio de grupo horario 
                Me.LblIndicar.Text = "Seleccionar asignatura(s)"
                If RblAsunto.SelectedValue = 13 Then
                    ClsFunciones.LlenarListas(Me.CblSeleccionar, Obj.TraerDataTable("ConsultarPlanEstudio", "SO", Session("codigo_alu"), "CC"), "codigo_cur", "Descripcion")
                Else
                    ClsFunciones.LlenarListas(Me.CblSeleccionar, Obj.TraerDataTable("ConsultarPlanEstudio", "SO", Session("codigo_alu"), "NC"), "codigo_cur", "Descripcion")
                End If
                Me.HddTotalSel.Value = Me.CblSeleccionar.Items.Count
                Me.HddTotalSelRet.Value = 0
                VisualizarControlesRetiros(LblIndicar, CblSeleccionar, Panel2, True)
                VisualizarControlesRetiros(LblIndicarRet, CblSeleccionarRet, PanelRet, False)

                'Case 10 'Examen Extraordinario - clluen 21/08/11
                '    Me.LblIndicar.Text = "Seleccionar Tesis"
                '    If RblAsunto.SelectedValue = 10 Then
                '        ClsFunciones.LlenarListas(Me.CblSeleccionar, Obj.TraerDataTable("ConsultarPlanEstudio", "SO", Session("codigo_alu"), "TE"), "codigo_cur", "Descripcion")
                '    Else
                '        ClsFunciones.LlenarListas(Me.CblSeleccionar, Obj.TraerDataTable("ConsultarPlanEstudio", "SO", Session("codigo_alu"), "NC"), "codigo_cur", "Descripcion")
                '    End If
                '    Me.HddTotalSel.Value = Me.CblSeleccionar.Items.Count
                '    Me.HddTotalSelRet.Value = 0
                '    VisualizarControlesRetiros(LblIndicar, CblSeleccionar, Panel2, True)
                '    VisualizarControlesRetiros(LblIndicarRet, CblSeleccionarRet, PanelRet, False)

            Case 9
                Me.LblIndicar.Text = "Agregar Asignatura(s)" ' NC: no complementarios
                ClsFunciones.LlenarListas(Me.CblSeleccionar, Obj.TraerDataTable("ConsultarPlanEstudio", "SO", Session("codigo_alu"), "NC"), "codigo_cur", "Descripcion")
                Me.HddTotalSel.Value = Me.CblSeleccionar.Items.Count
                VisualizarControlesRetiros(LblIndicar, CblSeleccionar, Panel2, True)

                Me.LblIndicarRet.Text = "Retirar asignatura(s)" ' NC: no complementarios
                ClsFunciones.LlenarListas(Me.CblSeleccionarRet, Obj.TraerDataTable("ConsultarNotas", "SO", Session("codigo_alu"), "NC", ""), "codigo_cur", "Descripcion")
                Me.HddTotalSelRet.Value = Me.CblSeleccionarRet.Items.Count
                VisualizarControlesRetiros(LblIndicarRet, CblSeleccionarRet, PanelRet, True)

            Case 12
                Me.LblIndicar.Text = "Agregar Asignatura(s)" ' CC:   complementarios
                ClsFunciones.LlenarListas(Me.CblSeleccionar, Obj.TraerDataTable("ConsultarPlanEstudio", "SO", Session("codigo_alu"), "CC"), "codigo_cur", "Descripcion")
                Me.HddTotalSel.Value = Me.CblSeleccionar.Items.Count
                VisualizarControlesRetiros(LblIndicar, CblSeleccionar, Panel2, True)

                Me.LblIndicarRet.Text = "Retirar asignatura(s)" ' CC:   complementarios
                ClsFunciones.LlenarListas(Me.CblSeleccionarRet, Obj.TraerDataTable("ConsultarNotas", "SO", Session("codigo_alu"), "CC", ""), "codigo_cur", "Descripcion")
                Me.HddTotalSelRet.Value = Me.CblSeleccionarRet.Items.Count
                VisualizarControlesRetiros(LblIndicarRet, CblSeleccionarRet, PanelRet, True)

            Case 16
                Me.LblIndicar.Text = "Indicar Escuela Profesional"
                ClsFunciones.LlenarListas(Me.cboEscuela, Obj.TraerDataTable("ConsultarCarreraProfesional", "MA", 0), "codigo_cpf", "nombre_cpf", "<<Seleccione>>")
                Me.CblSeleccionar.Items.Clear()
                Me.CblSeleccionarRet.Items.Clear()
                VisualizarControlesRetiros(LblIndicar, CblSeleccionar, Panel2, False)
                VisualizarEscuela(True)
                Me.cvEscuela.Enabled = True
                Me.HddTotalSel.Value = 0
                Me.HddTotalSelRet.Value = 0

            Case Else
                Me.LblIndicar.Text = "Seleccionar ciclo(s) académico(s)"
                ClsFunciones.LlenarListas(Me.CblSeleccionar, Obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")
                Me.HddTotalSel.Value = Me.CblSeleccionar.Items.Count
                Me.HddTotalSelRet.Value = 0
                VisualizarControlesRetiros(LblIndicar, CblSeleccionar, Panel2, True)
                VisualizarControlesRetiros(LblIndicarRet, CblSeleccionarRet, PanelRet, False)

        End Select
    End Sub

    Private Sub VisualizarEscuela(ByVal valor As Boolean)
        Me.cboEscuela.Visible = valor
        Me.LblIndicar.Visible = valor
    End Sub

    Private Sub VisualizarControlesRetiros(ByVal lbltexto As Label, ByVal cblSel As CheckBoxList, ByVal panelSel As Panel, ByVal valor As Boolean)
        cblSel.Visible = valor
        lbltexto.Visible = valor
        panelSel.Visible = valor
    End Sub
    Private Function VerificarCursoNotaFinal() As String
        If Me.RblAsunto.SelectedValue.ToString = "2" Then
            ' #Modificacion: 02/11/12 por YPEREZ#
            ' #Pedir confirmación al usuario cuando se hace retiro por asunto: RETIRO DE SEMESTRE ACADÉMICO#
            ' #en caso el estudiante cuente con nota final en algun curso en el ciclo#
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim datosNotas As Data.DataTable
            datosNotas = New Data.DataTable
            For ci As Integer = 0 To Me.CblSeleccionar.Items.Count - 1
                If Me.CblSeleccionar.Items(ci).Selected.ToString = "True" Then
                    datosNotas = Obj.TraerDataTable("SOL_VerificarCursoNotaFinal", CInt(Session("codigo_alu")), Me.CblSeleccionar.Items(ci).Value)
                    If datosNotas.Rows.Count > 0 Then
                        mensaje &= "Existen " & datosNotas.Rows.Count & " asignaturas con registro de notas finales en el ciclo " & Me.CblSeleccionar.Items(ci).Text
                        mensaje &= "\n"
                    End If
                End If
            Next

        End If
        If mensaje <> "" Then
            mensaje &= "¿Desea de todas maneras realizar el retiro de los semestres académicos seleccionados?"
        End If

        Return mensaje
    End Function

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim codigo_per As Int32 = Request.QueryString("id")
        Dim codigo_sol As Int32
        Dim datos As Data.DataTable
        Dim EsSolicitudPP As Boolean = False
        Dim DescripcionAsunto As String = ""
        Dim errorm As String = ""

        Try
            If Me.TxtNumSolicitud.Text <> "" Then
                'Verifica si el código de la solicitud se encuentra asignada a alguna solicitud registrada
                datos = Obj.TraerDataTable("SOL_ConsultarSolicitudesPendientes", 6, Me.TxtNumSolicitud.Text) ' 8: matricula especial extraordinaria

                If datos.Rows.Count > 0 Then
                    ClientScript.RegisterStartupScript(Me.GetType, "NroSol Registrada", "alert('El número de la solicitud se encuentra asignada al estudiante " & datos.Rows(0).Item("responsable_sol").ToString & "')", True)

                ElseIf Me.RblAsunto.SelectedValue = 8 And Obj.TraerDataTable("SOL_ConsultarSolicitud_ExamExtraordinario", Session("codigo_alu"), 8).Rows.Count > 0 Then
                    ClientScript.RegisterStartupScript(Me.GetType, "Exam Extraordinario", "alert('El estudiante ya tiene una solicitud de Matricula Especial Extraordinaria registrada')", True)
                    gvEstadoCue.DataBind()

                Else

                    '### Registrar solicitud ###
                    '********* Crea la descripción del asunto a registrar **********
                    DescripcionAsunto = CrearDescripcionAsunto()
                    Dim i As Int16
                    Obj.IniciarTransaccion()
                    '********* Inserta la solicitud **********
                    codigo_sol = Obj.Ejecutar("SOL_AgregarSolicitudCliente", "E", CInt(Session("codigo_alu")), Me.TxtFecha.Text, _
                                              Me.TxtNumSolicitud.Text, Me.TxtObservaciones.Text, 1, "P", 1, Me.TxtResponsable.Text, _
                                              Me.TxtCodMatricula.Text, Me.TxtDireccion.Text, Me.TxtTelefonos.Text, "", codigo_per)


                    If Me.RblAsunto.SelectedValue = 16 Then
                        DescripcionAsunto = cboEscuela.SelectedItem.Text
                    End If

                    '********* Inserta asuntos de la solicitud **********
                    For i = 0 To Me.RblAsunto.Items.Count - 1

                        If Me.RblAsunto.Items(i).Selected = True Then

                            Obj.Ejecutar("SOL_AgregarAsuntoSolicitudCliente", codigo_sol, Me.RblAsunto.Items(i).Value, DescripcionAsunto)
                            'errorm = "codigosol: " & codigo_sol & " Asunto: " & Me.RblAsunto.Items(i).Value & " descrip:  " & DescripcionAsunto


                        End If

                    Next

                    '********* Inserta asunto anulación de deuda a la solicitud **********
                    If RbSi.Checked = True Then
                        Obj.Ejecutar("SOL_AgregarAsuntoSolicitudCliente", codigo_sol, 5, Me.TxtSemAnulacion.Text)
                    End If

                    '********* Inserta motivos a la solicitud **********
                    For i = 0 To Me.CblMotivo.Items.Count - 1
                        If Me.CblMotivo.Items(i).Selected = True Then
                            If Me.CblMotivo.Items(i).Value = 8 Then
                                Obj.Ejecutar("SOL_AgregarMotivoSolicitudCliente", codigo_sol, Me.CblMotivo.Items(i).Value, Me.TxtOtros.Text)
                            Else
                                Obj.Ejecutar("SOL_AgregarMotivoSolicitudCliente", codigo_sol, Me.CblMotivo.Items(i).Value, Me.CblMotivo.Items(i).Text)
                            End If
                        End If
                    Next

                    '********* verifica si es programa especial busca la carrera a la que pertenece y reemplaza el codigo_cpf**********
                    Dim datCarrera As New Data.DataTable
                    'If Me.HddCodigocpf.Value = 25 Then
                    '    datCarrera = Obj.TraerDataTable("SOL_ConsultarCarreraProfesionalPE", Session("codigo_alu"))
                    '    If datCarrera.Rows.Count > 0 And datCarrera.Rows(0).Item("ppcodigo_cpf") > 0 Then
                    '        Me.HddCodigocpf.Value = datCarrera.Rows(0).Item("ppcodigo_cpf")
                    '    End If
                    'End If
                    If Me.RblAsunto.SelectedValue = 16 Then
                        Me.HddCodigocpf.Value = Me.cboEscuela.SelectedValue
                    End If

                    '********* Agrega evaluaciones según configuracion del asunto **********
                    datos = Obj.TraerDataTable("SOL_ConsultarConfiguracionDependencia", 1, codigo_sol, 1)
                    For i = 0 To datos.Rows.Count - 1
                        'Antes: SOL_AgregarEvaluacionSolicitudCliente
                        Obj.Ejecutar("SOL_AgregarEvaluacionSolicitudCliente_v2", 1, codigo_sol, 0, CInt(datos.Rows(i).Item("CentroCostos").ToString), _
                                     CInt(datos.Rows(i).Item("nivel").ToString), "", Me.HddCodigocpf.Value)
                    Next

                    ' #### BEGIN UPDATED: 07/11/12 por YPEREZ######
                    ' #Para los estudiantes de profesionalización#
                    ' #Inserta como evaluador solo al director académico del programa, con nivel = a director de escuela#
                    'Dim dtRPP As Data.DataTable
                    'dtRPP = New Data.DataTable
                    'dtRPP = Obj.TraerDataTable("SOL_ObtenerRevisorPP", CInt(Session("codigo_alu")), Me.HddCodigocpf.Value)
                    'If dtRPP.Rows.Count Then
                    '    For i = 0 To dtRPP.Rows.Count - 1
                    '        Obj.Ejecutar("SOL_AgregarEvaluacionSolicitudClientePP", codigo_sol, CInt(dtRPP.Rows(i).Item("codigo_Cco").ToString), _
                    '                   CInt(dtRPP.Rows(i).Item("codigo_per").ToString))
                    '    Next
                    '    EsSolicitudPP = True
                    'End If
                    ' ####END UPDATED: 07/11/12 por YPEREZ######                    
                    Obj.TerminarTransaccion()


                    '********* Envia mail a la instancia de evaluación **********

                    '#### BEGIN UPDATED: 08/11/12 por YPEREZ######
                    '#Para los estudiantes de profesionalización.
                    '#Envia mail al coordinador del programa con CC: madrianzen/lrossmorrey
                    Dim ObjMail As New ClsEnviaMail
                    If EsSolicitudPP Then
                        ObjMail.ConsultarEnvioMailPP(codigo_sol, Session("foto"))
                    Else
                        ObjMail.ConsultarEnvioMail(codigo_sol, Session("foto"))
                    End If
                    '####END UPDATED: 08/11/12 por YPEREZ######

                    LimpiarControles()
                    ClientScript.RegisterStartupScript(Me.GetType, "Exito", "alert('Se registraron correctamente los datos')", True)
                    Me.gvEstadoCue.Visible = False
                End If
            End If

        Catch ex As Exception
            Obj.AbortarTransaccion()
            Response.Write(ex.StackTrace)
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('Ocurrió un error al procesar los datos')", True)
        End Try

    End Sub

    Private Function CrearDescripcionAsunto() As String
        Dim Descripcion_asu As String = ""
        If Me.RblAsunto.SelectedValue = 9 Or Me.RblAsunto.SelectedValue = 12 Then
            Descripcion_asu = LblIndicar.Text & ": "
            For i As Int16 = 0 To Me.CblSeleccionar.Items.Count - 1
                If Me.CblSeleccionar.Items(i).Selected = True Then
                    Descripcion_asu = Descripcion_asu.ToString & Me.CblSeleccionar.Items(i).Text & " \ "
                End If
            Next
            Descripcion_asu = Descripcion_asu.ToString & " || " & LblIndicarRet.Text & ": "
            For i As Int16 = 0 To Me.CblSeleccionarRet.Items.Count - 1
                If Me.CblSeleccionarRet.Items(i).Selected = True Then
                    Descripcion_asu = Descripcion_asu.ToString & Me.CblSeleccionarRet.Items(i).Text & " \ "
                End If
            Next
        Else
            For i As Int16 = 0 To Me.CblSeleccionar.Items.Count - 1
                If Me.CblSeleccionar.Items(i).Selected = True Then
                    Descripcion_asu = Me.CblSeleccionar.Items(i).Text & " \ " & Descripcion_asu.ToString
                End If
            Next
        End If
        Return Descripcion_asu
    End Function

    Private Sub LimpiarControles()
        Me.TxtCodMatricula.Text = ""
        Me.TxtBuscar.Text = ""
        Me.TxtDireccion.Text = ""
        Me.TxtEscuela.Text = ""
        Me.TxtFecha.Text = Date.Now.ToShortDateString
        Me.TxtNumSolicitud.Text = ""
        Me.TxtObservaciones.Text = ""
        Me.TxtResponsable.Text = ""
        Me.TxtSemAnulacion.Text = ""
        Me.TxtSemestre.Text = ""
        Me.TxtTelefonos.Text = ""
        Me.TxtUrbDis.Text = ""
        Me.CblSeleccionar.Controls.Clear()
        Me.CblSeleccionarRet.Controls.Clear()
        Me.CblMotivo.Controls.Clear()
    End Sub


    Protected Sub CboBuscarPor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboBuscarPor.SelectedIndexChanged
        If Me.CboBuscarPor.SelectedValue = 0 Then
            Me.TxtBuscar.MaxLength = 10
        Else
            Me.TxtBuscar.MaxLength = 50
        End If
    End Sub

    Protected Sub GvEstadoCue_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEstadoCue.RowDataBound

        '    If e.Row.RowType = DataControlRowType.DataRow Then
        '        e.Row.Cells(9).Text = CDbl(e.Row.Cells(5).Text) + CDbl(e.Row.Cells(6).Text)
        '        e.Row.Cells(9).Text = FormatNumber(e.Row.Cells(9).Text, 2)
        '        total = total + e.Row.Cells(9).Text
        '    End If
        '    If e.Row.RowType = DataControlRowType.Footer Then
        '        e.Row.Cells(9).Text = FormatNumber(total, 2)
        '        e.Row.Cells(6).Text = "Total"
        '        e.Row.Cells(6).Font.Bold = True
        '    End If
    End Sub

    Protected Sub CblSeleccionar_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CblSeleccionar.SelectedIndexChanged
        Page.RegisterStartupScript("Solicitud", "<script>Panel1.style.visibility ='visible' </script>")
        If Me.RblAsunto.SelectedValue.ToString = "2" And Me.hfMensaje.Value.Length Then
            hfMensaje.Value = VerificarCursoNotaFinal()
        End If
    End Sub
End Class

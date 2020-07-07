
Partial Class frmListaAlumnosProfesionalizacion
    Inherits System.Web.UI.Page

    Dim Accion As String = ""

    Dim s_per As Integer
    Dim s_codigo_cup As Integer
    Dim s_codigo_cac As Integer
    Dim s_codigo_pes As Integer
    Dim s_cboCecos As Integer
    Dim s_ctf As Integer
    Dim s_mod As Integer
    Dim s_accion As String
    Dim s_accion_set As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'No comentar:
            '===================================================
            s_per = Session.Item("s_id")
            s_codigo_cup = Session.Item("s_codigo_cup")
            s_codigo_cac = Session.Item("s_codigo_cac")
            s_codigo_pes = Session.Item("s_codigo_pes")
            s_cboCecos = Session.Item("s_cboCecos")
            s_ctf = Session.Item("s_ctf")
            s_mod = Session.Item("s_mod")
            s_accion = Session.Item("s_accion")

            'Seteamos el inicio

            If Request.QueryString("Accion") = "C" Then
                s_accion_set = Request.QueryString("Accion")
            Else
                If (s_accion = "") Or (s_accion Is Nothing) Then
                    s_accion_set = Request.QueryString("Accion")
                Else
                    s_accion_set = Session.Item("s_accion")
                End If
            End If
            '===================================================

            '# Pruebas : 
            ''Impresion de valores
            'Response.Write("codigo_cur : ->" + Me.Request.QueryString("codigo_cur"))
            'Response.Write("<br />")
            'Response.Write("ceco : ->" + Me.Request.QueryString("ceco"))
            'Response.Write("<br />")
            'Response.Write("<br />")
            '------------------------------------------------------------------------------------
            'Response.Write("s_per " + s_per.ToString)
            'Response.Write("<br />")
            'Response.Write("s_codigo_cup " + s_codigo_cup.ToString)
            'Response.Write("<br />")
            'Response.Write("s_codigo_cac " + s_codigo_cac.ToString)
            'Response.Write("<br />")
            'Response.Write("s_codigo_pes " + s_codigo_pes.ToString)
            'Response.Write("<br />")
            'Response.Write("s_cboCecos " + s_cboCecos.ToString)
            'Response.Write("<br />")
            'Response.Write("s_ctf " + s_ctf.ToString)
            'Response.Write("<br />")
            'Response.Write("s_mod " + s_mod.ToString)
            'Response.Write("<br />")
            'Response.Write("s_accion " + s_accion)

            If Not IsPostBack Then

                Select Case s_accion_set 's_accion 'Request.QueryString("Accion")
                    Case "L"
                        If validaciones() = False Then Exit Sub
                        'Entra en este bloque cuando es redireccionado de la pagina de programacion de cursos.
                        lblTitulo.Text = "PRE-MATRICULA PROGRAMA DE PROFESIONALIZACIÓN"
                        pnlMatricula.Visible = True
                        pnlConsulta.Visible = False
                        pnlTabs.Visible = True
                        datosmatricula()
                    Case "C"
                        lblTitulo.Text = ".:: LISTADO DE ALUMNOS ::."
                        If Request.QueryString("Tipo") = "T" Then
                            Dim obj As New clsProfesionalizacion
                            Dim dts As New Data.DataTable
                            dts = obj.ConsultaCentroCosto(Me.Request.QueryString("Ceco"))
                            If dts.Rows.Count > 0 Then
                                lblPrograma.Text = dts.Rows(0).Item("descripcion_Cco").ToString.ToUpper()
                            Else
                                lblPrograma.Text = "TODOS LOS PROGRAMAS"
                            End If
                        Else
                            lblPrograma.Text = Request.QueryString("Programa")
                        End If
                        lblCurso.Text = Request.QueryString("codigo_cur")
                        pnlMatricula.Visible = False
                        pnlConsulta.Visible = True
                        pnlTabs.Visible = False
                        btnPrematricula.Visible = False
                        cmdEliminar.Visible = False
                End Select
                EntraLinks()
                'Carga la lista de alumnos que van a ser invitdos.
                'ListaAlumnos(Request.QueryString("Accion"))
                'ListaAlumnos(s_accion)
                ListaAlumnos(s_accion_set)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function validaciones() As Boolean
        Dim sw As Byte = 0
        If (Session.Item("s_id") Is Nothing) Or _
        (Session.Item("s_codigo_cup") Is Nothing) Or _
        (Session.Item("s_codigo_cac") Is Nothing) Or _
        (Session.Item("s_codigo_pes") Is Nothing) Or _
        (Session.Item("s_cboCecos") Is Nothing) Or _
        (Session.Item("s_ctf") Is Nothing) Or _
        (Session.Item("s_mod") Is Nothing) Or _
        (Session.Item("s_accion") Is Nothing) Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Su sesión a expirado, favor de volver a ingresar a la página de invitaciones de cursos programados de profesionalización.');", True)
            Exit Function
        End If

        sw = 1
        If (sw = 1) Then
            Return True
        End If
        Return False
    End Function

    Private Sub EntraLinks()
        Try
            'If validaciones() = False Then Exit Sub
            lnkConsulta.ForeColor = Drawing.Color.Blue
            lnkPrematriculados.ForeColor = Drawing.Color.Black
            lbkMatriculados.ForeColor = Drawing.Color.Black
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub datosmatricula()
        Try
            If validaciones() = False Then Exit Sub
            Dim obj As New clsProfesionalizacion
            Dim dts As New Data.DataTable
            'dts = obj.MostrarDatosPreMatricula(Me.Request.QueryString("Codigo_cup"), Me.Request.QueryString("Codigo_cac"))
            'Sessiones:
            dts = obj.MostrarDatosPreMatricula(s_codigo_cup, s_codigo_cac)
            If dts.Rows.Count > 0 Then
                lblCursoMatricula.Text = dts.Rows(0).Item("nombre_cur").ToString
                lblPlanEstudios.Text = dts.Rows(0).Item("descripcion_pes").ToString
                lblCicloAcademico.Text = dts.Rows(0).Item("descripcion_cac").ToString

                lblGrupoHorario.Text = dts.Rows(0).Item("grupoHor_cup").ToString
                lblEscuelaProfesional.Text = dts.Rows(0).Item("nombre_cpf").ToString

                lblFechaInicio_cup.Text = dts.Rows(0).Item("fechainicio_Cup").ToString
                lblFechaFin_cup.Text = dts.Rows(0).Item("fechafin_Cup").ToString

            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaAlumnos.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                Dim bandera As Boolean = False  'Para identificar los alumnos que pueden ser invitados

                fila = e.Row.DataItem
                e.Row.Cells(1).Text = e.Row.RowIndex + 1

                'Validacion:
                '==================================================================================================
                'Si el alumno tiene nota>=8 el check se activa para poder hacer la pre-matricula.
                'Esta linea se comento, a solicitud de profesionalizacion el 17.01.2013
                'xDguevara.
                'If fila("notaFinal_Dma") >= 8 Then
                '    CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")
                'Else
                '    e.Row.Cells(0).Text = ""
                'End If
                '==================================================================================================


                'If hdfAccion.Value = "P" Then
                '    e.Row.Cells(0).Text = ""
                'Else
                '    CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")
                'End If

                If fila("estadoactual_alu") = "INACTIVO" Then
                    e.Row.Cells(5).ForeColor = Drawing.Color.Red
                End If


                'If Request.QueryString("Accion") = "L" Then
                If s_accion = "L" Then  'sessiones s_accion
                    gvListaAlumnos.Columns(0).Visible = True
                    '======================================================================================
                    'Valida que si el alumno ya esta matriculado en dos cursos, y tienen deudas
                    'No podra ser invitado para un tercer curso de nivelacion
                    If fila("cntDeudas") > 2 Then
                        e.Row.Cells(0).Text = ""
                        e.Row.ForeColor = Drawing.Color.Red
                        e.Row.Cells(3).ToolTip = "Con Deudas Pendientes"
                        e.Row.Cells(0).Text = "<center><img src='../../images/prof_red_deudas.png' style='border: 0px' alt=''/></center>"
                        'e.Row.Cells(14).Text = "<center><img src='../../images/prof_red_deudas.png' style='border: 0px' alt=''/></center>"
                        'bandera = True
                    End If
                    '=======================================================================================

                    '==========================================================================================
                    'Se agrego para identifcar a los alumnos que no han llevado el curso.
                    If fila("EstadoNota") = "FF" Then
                        e.Row.ForeColor = Drawing.Color.Green
                        e.Row.Cells(14).Text = "<center><img src='../../images/prof_no_curso.png' style='border: 0px' alt=''/></center>"
                        bandera = True
                    End If
                    '==========================================================================================


                    If fila("notaFinal_Dma") < 8 And fila("EstadoNota") <> "FF" Then
                        e.Row.Cells(2).ForeColor = System.Drawing.Color.Brown
                        e.Row.Cells(10).ForeColor = System.Drawing.Color.Brown
                        e.Row.Cells(14).Text = "<center><img src='../../images/prof_nota_menor_ocho.png' style='border: 0px' alt=''/></center>"
                        bandera = True
                    End If


                    If bandera = False Then
                        e.Row.Cells(14).Text = "<center><img src='../../images/prof_invitar.gif' style='border: 0px' alt=''/></center>"
                    End If


                Else
                    gvListaAlumnos.Columns(0).Visible = False
                End If

                'If Request.QueryString("Accion") = "C" Then
                If s_accion = "L" Then
                    If fila("cntDeudas") > 2 Then
                        e.Row.ForeColor = Drawing.Color.Red
                        e.Row.Cells(3).ToolTip = "Con Deudas Pendientes"
                        bandera = True
                    End If
                End If

                If s_accion_set = "C" Then
                    gvListaAlumnos.Columns(0).Visible = False
                    If fila("notaFinal_Dma") < 8 Then
                        e.Row.ForeColor = Drawing.Color.Red
                    Else
                        e.Row.ForeColor = Drawing.Color.Green
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEliminar.Click
        Try

            Dim mensaje(1) As String
            Dim Fila As GridViewRow
            Dim obj As New ClsConectarDatos
            Dim codigo_usu As Integer = s_per
            Dim altert As String
            Dim marcados As Int16 = 0

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            If validaciones() = False Then Exit Sub
            For I As Int16 = 0 To Me.gvListaAlumnos.Rows.Count - 1
                Fila = Me.gvListaAlumnos.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        marcados = marcados + 1
                    End If
                End If
            Next

            'Verfica que exitan alumnos seleccionados.
            If marcados = 0 Then
                ListaAlumnos("P")
                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Debe marcar a los estudiantes que desea eliminar la invitación al curso de nivelación programado.');", True)
                Exit Sub
            End If

            obj.AbrirConexion()
            For I As Int16 = 0 To Me.gvListaAlumnos.Rows.Count - 1
                Fila = Me.gvListaAlumnos.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then

                        'Aqui retiramos a alumno del curso de nivelación programado.
                        obj.Ejecutar("PRO_EliminarPreMatriculaProfesionalizacion", _
                                     Me.gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("codigo_Alu"), _
                                     s_codigo_cac, _
                                     s_codigo_cup, _
                                     codigo_usu, "").copyto(mensaje, 0)

                    End If
                End If
            Next


            obj.CerrarConexion()
            obj = Nothing
            ListaAlumnos("P")

            'Mostramos el mensaje de alerta =========================================================================
            altert = mensaje(0).ToString
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('" & altert & "')", True)
            '=========================================================================================================
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnPrematricula_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrematricula.Click
        Try
            Dim MensajeOUT(1) As String
            Dim MensajeValidacion(1) As String

            Dim Costo As String = ""
            Dim Fila As GridViewRow
            Dim obj As New ClsConectarDatos
            Dim marcados As Int16 = 0

            Dim codigo_usu As Integer = s_per 'Request.QueryString("id")
            Dim valoresdevueltos(1) As String
            Dim altert As String
            Dim ObjMailNet As New ClsMail
            Dim mensaje As String = ""
            Dim para As String = ""



            '===============================================================================
            '==[Validación Nº 1]==
            'Validacion, que haya marcado alumnos para matricular.

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Me.cmdEliminar.Visible = False

            If validaciones() = False Then Exit Sub
            For I As Int16 = 0 To Me.gvListaAlumnos.Rows.Count - 1
                Fila = Me.gvListaAlumnos.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        marcados = marcados + 1
                    End If
                End If
            Next

            'Verfica que exitan alumnos seleccionados.
            If marcados = 0 Then
                lnkPrematriculados.ForeColor = Drawing.Color.Blue
                ListaAlumnos("L")
                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Debe marcar a los estudiantes que se matricularán en el grupo horario');", True)
                Exit Sub
            End If
            '===============================================================================

            '==[Validación Nº 2]==
            obj.AbrirConexion()

            For J As Int16 = 0 To Me.gvListaAlumnos.Rows.Count - 1
                Fila = Me.gvListaAlumnos.Rows(J)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then

                        'Veriicamos
                        'obj.Ejecutar("ValidaCursoProgramadoAlumno", Me.gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("codigo_Alu"), Me.Request.QueryString("Codigo_cup"), System.DBNull.Value).copyto(MensajeValidacion, 0)
                        'Con sessiones:
                        obj.Ejecutar("ValidaCursoProgramadoAlumno", Me.gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("codigo_Alu"), s_codigo_cup, System.DBNull.Value).copyto(MensajeValidacion, 0)

                        'Cerramos el proceso, debido a que se encontro una matricula para ese curso programado perndiente.
                        If MensajeValidacion(0).ToString.Length > 0 Then
                            altert = MensajeValidacion(0).ToString
                            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('" & altert & "')", True)
                            Exit Sub
                        End If
                    End If
                End If
            Next
            
            '=====================================================[Fin Validación]

            '===============================================================================
            'Matricular a los estudiantes
            'obj.AbrirConexion()
            'obj.IniciarTransaccion()

            For I As Int16 = 0 To Me.gvListaAlumnos.Rows.Count - 1
                Fila = Me.gvListaAlumnos.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then

                        'testing de registro:
                        '==========================================================================================================================================================================================================================================================================================================================================================================================================///
                        'Response.Write("AgregarMatriculaWebInvitarProfesionalizacion 'A', '" & Me.gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("codigo_Alu") & "', '" & Session.Item("s_codigo_pes") & "', '" & Session.Item("s_codigo_cac") & "', 'P', 'Matricula por SOLNIV', '" & Session.Item("s_codigo_cup") & "','1,', 'N', 'P', 'MAT', '" & codigo_usu & "', 'PC', NULL, NULL, 1, NULL")
                        'Response.Write("<br>")
                        '==========================================================================================================================================================================================================================================================================================================================================================================================================///

                        '** Descomentar **'
                        ''Con sessiones:
                        obj.Ejecutar("AgregarMatriculaWebInvitarProfesionalizacion", _
                                     "A", _
                                     Me.gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("codigo_Alu"), _
                                     s_codigo_pes, _
                                     s_codigo_cac, _
                                     "P", _
                                     "Matricula por SOLNIV", _
                                     s_codigo_cup & ",", _
                                     "1,", _
                                     "N", _
                                     "P", _
                                     "MAT", _
                                     codigo_usu, _
                                     "PC", _
                                     System.DBNull.Value, _
                                     System.DBNull.Value, _
                                     1, _
                                     System.DBNull.Value).copyto(MensajeOUT, 0)


                        If MensajeOUT(0).ToString.Length > 0 Then
                            altert = MensajeOUT(0).ToString
                            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('" & altert & "')", True)
                            Exit Sub
                        End If

                        '====================================================================================
                        'Esto qdo x unas pruebas q se hizo!.!!
                        'Monto Configurado por codigo duro.
                        If gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("Codigo_cpf") = 52 Then
                            'Profesionalizacion Administracion
                            Costo = "150.00"
                        Else
                            'Profesionalizacion Otros Programas
                            Costo = "150.00"
                        End If
                        '====================================================================================


                        '======================================================================================================
                        'Notificacion de correo electronico de la pre-matricula 
                        '======================================================================================================
                        If gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("eMail_Alu").ToString.Trim <> "" Then
                            'Envia Email Estudiante
                            '==============
                            para = "</br><font face='Courier'>" & "Estimado(a) estudiante: <b>" & gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("alumno").ToString.Trim & "</b>"
                            mensaje = "</br></br><P><ALIGN='justify'> Reciba nuestro cordial saludo. En esta oportunidad le comunicamos que se ha programado el curso de nivelación de <b>" & lblCursoMatricula.Text & "</b>,el mismo que se desarrollará del :" & lblFechaInicio_cup.Text & " al " & lblFechaFin_cup.Text & "de " & Year(CDate(lblFechaFin_cup.Text)) & ",costo  S/." & Costo & " nuevos soles; orientado a quienes tienen pendiente de aprobar el curso y han obtenido una nota final entre 8 y 13.</br>" & "Si desea matricularse en este curso de nivelación, sírvase verificar que cumple con las siguientes condiciones:</br>" & "</P>"
                            mensaje = mensaje & "</br> Tener en orden su situación administrativa, caso contrario comunicarse con el Coordinador del Programa.</font>"
                            mensaje = mensaje & "</br> Le invitamos a realizar su matrícula, teniendo plazo hasta el mismo día en que se inicia el curso -no se autorizarán postergaciones- y de manera automática se generará el cargo en su estado de cuenta, con fecha de vencimiento " & lblFechaInicio_cup.Text & ".</font>"
                            mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"

                            'Descomentar
                            'ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Profesionalizacón", gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("eMail_Alu").ToString.Trim, "Programas de Profesionalización", para & mensaje, True)

                            '--------------//
                            'Comentar
                            'ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Profesionalizacón", gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("email_Per").ToString.Trim, "Programas de Profesionalización", para & mensaje, True)
                            'ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Pruebas Sistemas Profesionalizacón", "dguevara@usat.edu.pe", "Programas de Profesionalización", para & mensaje, True)
                            '==============================
                        Else
                            If gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("email2_Alu").ToString.Trim <> "" Then
                                'Envia Email Estudiante
                                '==============
                                para = "</br><font face='Courier'>" & "Estimado(a): <b>" & gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("alumno").ToString.Trim & "</b>"
                                mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, el curso <b>" & lblCursoMatricula.Text & "</b>, se ha programado bajo la modalidad de NIVELACIÓN, desde :" & lblFechaInicio_cup.Text & " hasta " & lblFechaFin_cup.Text & ", el importe a cargar será de S/." & Costo & "</br>" & ".Favor CONFIRMAR SU MATRICULA por el Campus Virtual opción Nivelación. " & "</P>"
                                mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"

                                'Descomentar()
                                'ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Profesionalizacón", gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("email2_Alu").ToString.Trim, "Programas de Profesionalización", para & mensaje, True)

                                '--------------//
                                'ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Profesionalizacón", gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("email_Per").ToString.Trim, "Programas de Profesionalización", para & mensaje, True)
                                'Comentar
                                'ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Pruebas Sistemas Profesionalizacón", "dguevara@usat.edu.pe", "Programas de Profesionalización", para & mensaje, True)
                                '==============
                            Else
                                '=================
                                'Cuando no tiene correo registrado, email al coordinador.
                                para = "</br><font face='Courier'>" & "Estimado(a): <b>" & gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("Coodinador").ToString.Trim & "</b>"
                                mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, el alumno <b>" & gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("alumno").ToString.Trim & "</b>, ha sido pre-matriculado en el curso " & lblCursoMatricula.Text & ", pero no se le notifico via email, debido a problemas con el correo" & "</P>"
                                mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"

                                'Descomentar
                                'ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Profesionalizacón", gvListaAlumnos.DataKeys.Item(Fila.RowIndex).Values("email_Per").ToString.Trim, "Programas de Profesionalización", para & mensaje, True)

                                'Comentar:
                                'ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Pruebas Sistemas Profesionalizacón", "dguevara@usat.edu.pe", "Programas de Profesionalización", para & mensaje, True)
                                '=================
                            End If
                        End If
                        '=====================================================================================================
                    End If
                End If
            Next
            '===============================================================================

            '-----------------------------------
            'obj.TerminarTransaccion()
            '-----------------------------------

            obj.CerrarConexion()
            obj = Nothing

            lnkConsulta.ForeColor = Drawing.Color.Black
            lnkPrematriculados.ForeColor = Drawing.Color.Blue
            lbkMatriculados.ForeColor = Drawing.Color.Black
            cmdEliminar.Visible = True
            cmdEliminar.Enabled = True
            btnPrematricula.Enabled = False
            ListaAlumnos("P")

            'Response.Write("<script>alert('Las pre-matrículas se han registrado correctamente. Se enviaron los email correspondientes.');</script>")
            Page.RegisterStartupScript("ok", "<script>alert('Las pre-matrículas se han registrado correctamente. Se enviaron los email correspondientes.');</script>")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ListaAlumnos(ByVal Accion As String)
        Try
            Dim obj As New clsProfesionalizacion
            Dim dts As New Data.DataTable


            If Accion <> "C" Then
                If validaciones() = False Then Exit Sub
                '==[ Entra al bloque cuando el TIPO=L -> Cuando llama de la pagina de programación de cursos.
                
                'Response.Write("<br \>")
                'Response.Write("codigo_cpf: " & Me.Request.QueryString("ceco").ToString)
                'dts = obj.ListaAlumnosPorCursoPrograma("", "", "", 0, 0, Accion, Me.Request.QueryString("codigo_cup"), Me.Request.QueryString("Codigo_cco"), Me.Request.QueryString("codigo_cac"), Me.Request.QueryString("id"))
                'Con sessiones:
                'Enviamos el lblGrupoHorario para hacer el filtro por grupo para: pre-matriculados y matriculados.
                dts = obj.ListaAlumnosPorCursoPrograma(lblGrupoHorario.Text.Trim.ToString, "", "", 0, 0, Accion, s_codigo_cup, s_cboCecos, s_codigo_cac, s_per)
            Else
                If Request.QueryString("Tipo") = "U" Then
                    'Filtramos la lista de alumnos por el programa, curso y estado
                    dts = obj.ListaAlumnosPorCursoPrograma(Me.Request.QueryString("codigo_cur"), Me.Request.QueryString("Programa"), Me.Request.QueryString("Tipo"), Me.Request.QueryString("estado"), 0, "C", 0, 0, 0, Me.Request.QueryString("id"))
                Else
                    
                    'Filtramos a todos los alumnos que pertencen a a la carrera de todos los programas segun el estado.
                    dts = obj.ListaAlumnosPorCursoPrograma(Me.Request.QueryString("codigo_cur"), _
                                                           "", Me.Request.QueryString("Tipo") _
                                                           , Me.Request.QueryString("estado") _
                                                           , Me.Request.QueryString("ceco") _
                                                           , "C", 0, 0, 0, Me.Request.QueryString("id"))
                End If
            End If

            If dts.Rows.Count > 0 Then
                gvListaAlumnos.DataSource = dts
                gvListaAlumnos.DataBind()
            Else
                gvListaAlumnos.DataSource = Nothing
                gvListaAlumnos.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkConsulta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkConsulta.Click
        Try
            If validaciones() = False Then Exit Sub
            lnkPrematriculados.ForeColor = Drawing.Color.Black
            lbkMatriculados.ForeColor = Drawing.Color.Black
            lnkConsulta.ForeColor = Drawing.Color.Blue
            ListaAlumnos("L")

            btnPrematricula.Visible = True
            btnPrematricula.Enabled = True
            cmdEliminar.Enabled = False
            cmdEliminar.Visible = True

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lbkMatriculados_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbkMatriculados.Click
        Try
            If validaciones() = False Then Exit Sub
            lnkPrematriculados.ForeColor = Drawing.Color.Black
            lbkMatriculados.ForeColor = Drawing.Color.Blue
            lnkConsulta.ForeColor = Drawing.Color.Black
            ListaAlumnos("M")

            btnPrematricula.Enabled = False
            cmdEliminar.Enabled = False

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
    '    Try
    '        If Request.QueryString("Accion") = "L" Then
    '            'ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "javascript:location.href='frmprogramacioncademicaeve.aspx?codigo_cac=" & Me.Request.QueryString("codigo_cac") & "&codigo_pes=" & Me.Request.QueryString("codigo_pes") & "&codigo_cco=" & Request.QueryString("codigo_cco") & "&id=" & Request.QueryString("id") & "&mod=" & Request.QueryString("mod") & "&ctf=" & Request.QueryString("ctf") & "&Accion=" & "M" & "';", True)
    '            Response.Redirect("frmprogramacioncademicaeve.aspx?codigo_cac=" & Me.Request.QueryString("codigo_cac") & "&codigo_pes=" & Me.Request.QueryString("codigo_pes") & "&codigo_cco=" & Request.QueryString("codigo_cco") & "&id=" & Request.QueryString("id") & "&mod=" & Request.QueryString("mod") & "&ctf=" & Request.QueryString("ctf") & "&Accion=" & "M")
    '        Else
    '            Response.Redirect("frmprogramacioncademicaeve.aspx?codigo_cac=" & Me.Request.QueryString("codigo_cac") & "&codigo_pes=" & Me.Request.QueryString("codigo_pes") & "&codigo_cco=" & Request.QueryString("codigo_cco") & "&id=" & Request.QueryString("id") & "&mod=" & Request.QueryString("mod") & "&ctf=" & Request.QueryString("ctf") & "&Accion=" & "M")
    '            'ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "javascript:location.href='frmConsolidadoAlumnosProfesionalizacion.aspx?Ceco=" & Request.QueryString("Ceco") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&Accion=" & "C" & "';", True)
    '        End If

    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    Protected Sub lnkPrematriculados_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrematriculados.Click
        Try
            If validaciones() = False Then Exit Sub
            'Lista todos los alumnos prematriculados
            ListaAlumnos("P")
            lnkPrematriculados.ForeColor = Drawing.Color.Blue
            lbkMatriculados.ForeColor = Drawing.Color.Black
            lnkConsulta.ForeColor = Drawing.Color.Black

            btnPrematricula.Visible = True
            cmdEliminar.Visible = True
            btnPrematricula.Enabled = False
            cmdEliminar.Enabled = True
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Try
            Dim sb As StringBuilder = New StringBuilder()
            Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
            Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
            Dim Page As Page = New Page()
            Dim form As HtmlForm = New HtmlForm()

            Me.gvListaAlumnos.EnableViewState = False

            Page.EnableEventValidation = False
            Page.DesignerInitialize()
            Page.Controls.Add(form)
            form.Controls.Add(Me.gvListaAlumnos)
            Page.RenderControl(htw)
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", "attachment;filename=ListadoAlumnosProfesionalizacion" & ".xls")
            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.Default
            Response.Write(sb.ToString())
            Response.End()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

  
End Class

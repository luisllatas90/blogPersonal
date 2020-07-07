﻿Imports System.Data

Partial Class frmasignarcargaacademica
    Inherits System.Web.UI.Page

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")
            Dim Modulo As Integer = Request.QueryString("mod")
            Session("PermitirCarga") = "NO"
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            '=========================================
            'Llenas Ciclo & Escuela, Profesores
            '=========================================
            Dim objFun As New ClsFunciones
            objFun.CargarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "UCI", 0), "codigo_cac", "descripcion_cac")
            objFun.CargarListas(Me.dpCodigo_cpf, obj.TraerDataTable("EVE_ConsultarCarreraProfesional", Modulo, codigo_tfu, codigo_usu), "codigo_cpf", "nombre_cpf", "--Seleccione la Carrera Profesional--")

            '=========================================
            'Obtener el Dpto. Académico como Director
            '=========================================
            Dim tblDpto As Data.DataTable
            tblDpto = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 2, codigo_usu, codigo_tfu, 0, 0)
            If tblDpto.Rows.Count > 0 Then
                Me.lblnombre_dac.Text = Replace(tblDpto.Rows(0).Item("nombre_dac").ToString, "DEPARTAMENTO ACADEMICO", "DPTO:")
                Me.hdcodigo_dac.Value = tblDpto.Rows(0).Item("codigo_dac").ToString
                '=========================================
                'Cargar Profesores adscritos
                '=========================================
                'objFun.CargarListas(Me.dpCodigo_per, obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 5, Me.hdcodigo_dac.Value, Me.hdCodigo_Cup.Value, Me.dpCodigo_cac.SelectedValue, 0), "codigo_per", "profesor", "--Seleccione el Profesor adscrito al Dpto.--")

                'objFun.CargarListas(Me.lstPersonal, obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 5, Me.hdcodigo_dac.Value, Me.hdCodigo_Cup.Value, Me.dpCodigo_cac.SelectedValue, 0), "codigo_per", "profesor", "--Seleccione el Profesor adscrito al Dpto.--")


            End If

            obj.CerrarConexion()
            obj = Nothing

            fnLoading(True)
            mostrar(1)
            Me.lblMensaje.Visible = False
            Me.ValidarPermisoAcciones()

        End If

        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "DivLoadEstilo", "fnEstilo();", True)
    End Sub
    Protected Sub dpCodigo_cac_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpCodigo_cac.SelectedIndexChanged
        Me.hdCodigo_Cup.Value = 0
        Me.grwGruposProgramados.Visible = False
    End Sub
    Protected Sub grwGruposProgramados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwGruposProgramados.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            e.Row.Cells(11).Text = IIf(fila.Row("estado_cup") = False, "Cerrado", "Abierto")
            e.Row.Attributes("OnClick") = Page.ClientScript.GetPostBackClientHyperlink(Me.grwGruposProgramados, "Select$" + e.Row.RowIndex.ToString)


            'Cargar Profesores y Horario
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim gr As BulletedList = CType(e.Row.FindControl("lstProfesores"), BulletedList)
            gr.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 16, fila.Row("codigo_cup"), 0, 0, 0)
            gr.DataBind()
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub
    Protected Sub grwGruposProgramados_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwGruposProgramados.SelectedIndexChanged, cmdPopUp.Click
        Try

            Dim dt As Data.DataTable

            If Session("PermitirCarga") = "SI" Then
                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "DivLoad", "fnDivLoad('report',100);  fnEstilo();", True)
                mostrar(2)

                Me.hdCodigo_Cup.Value = Me.grwGruposProgramados.DataKeys.Item(Me.grwGruposProgramados.SelectedIndex).Values("codigo_cup").ToString
                Me.lblcpf.Text = Me.grwGruposProgramados.DataKeys.Item(Me.grwGruposProgramados.SelectedIndex).Values("nombre_Cpf").ToString
                Me.lblcac.Text = Me.grwGruposProgramados.DataKeys.Item(Me.grwGruposProgramados.SelectedIndex).Values("descripcion_Cac").ToString
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                'Cargar Profesores asignados
                Me.dlProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 14, Me.hdCodigo_Cup.Value, Me.dpCodigo_cac.SelectedValue, 0, 0)
                Me.dlProfesores.DataBind()

                'Cargar profesores sugeridos
                ClsFunciones.LlenarListas(Me.blstProfesoresSugeridos, obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 6, Me.hdcodigo_dac.Value, Me.hdCodigo_Cup.Value, Me.dpCodigo_cac.SelectedValue, 0), "codigo_per", "profesor")
                'Cargar Horario del curso

                dt = obj.TraerDataTable("ACAD_RetornaDocentesPermitidos", Me.hdCodigo_Cup.Value)

                Me.lblCarga.Text = (dt.Rows(0).Item("docentes")) & " de " & dt.Rows(0).Item("nrodocente_cup")
                NumDocente(dt.Rows(0).Item("nrodocente_cup"))

                obj.CerrarConexion()
                obj = Nothing

                hdaCruceHorarioDisponible.Value = ""
                hdaCruceCurso.Value = ""
                aCruceHorarioDisponible.Visible = False
                aCruceCurso.Visible = False

                If dt.Rows(0).Item("docentes") = dt.Rows(0).Item("nrodocente_cup") Then
                    Me.gDataHorarioCurso.Enabled = False
                Else
                    Me.gDataHorarioCurso.Enabled = True
                End If

                fnListarDocentes()
                fnListarHorario()
                'fnValidaGridHorarioCurso()

                If fnValidaGridHorarioCurso() Then
                    'Me.dpCodigo_per2.Enabled = False
                Else
                    'Me.dpCodigo_per2.Enabled = True
                End If

                Me.lblcurso.Text = CType(Me.grwGruposProgramados.Rows.Item(Me.grwGruposProgramados.SelectedIndex).Cells(1).FindControl("lblnombre_cur"), Label).Text
                Me.lblgrupo.Text = Me.grwGruposProgramados.Rows.Item(Me.grwGruposProgramados.SelectedIndex).Cells(2).Text

                Me.lblGrupoProfesores.Text = "DOCENTES ASIGNADOS (" & Me.dlProfesores.Items.Count & ")"
                fnBotonera()
                Me.dpCodigo_per2.SelectedIndex = 0
                Me.mpeFicha.Show()



            End If
        Catch ex As Exception
            Response.Write("grwGruposProgramados_SelectedIndexChanged: " & ex.Message)
        End Try
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            ' Response.Write("antes")
            Me.aCruceCurso.Visible = False
            Me.aCruceHorarioDisponible.Visible = False

            If fnValidaGuardar() Then

                Dim obj As New ClsConectarDatos
                Dim dt As New Data.DataTable
                Dim msj(1) As Object
                Dim strMax As String = ""
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

                obj.AbrirConexion()

                dt = obj.TraerDataTable("ACAD_RetornaDocentesPermitidos", Me.hdCodigo_Cup.Value)

                If (dt.Rows.Count > 0) Then
                    If (dt.Rows(0).Item("nrodocente_cup") >= dt.Rows(0).Item("docentes") + 1) Then

                        'Response.Write(hdSelHorario.Value)

                        'Response.Write("CAR_AgregarCargaAcademicaV2 " & Me.dpCodigo_per2.SelectedValue & "," & Me.hdCodigo_Cup.Value & "," & Request.QueryString("id") & "," & Request.QueryString("mod") & "," & hdSelHorario.Value)

                        obj.Ejecutar("CAR_AgregarCargaAcademicaV2", Me.dpCodigo_per2.SelectedValue, Me.hdCodigo_Cup.Value, Request.QueryString("id"), Request.QueryString("mod"), hdSelHorario.Value, "").copyto(msj, 0)
                        'Limpiar valores
                        Me.dlProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 14, Me.hdCodigo_Cup.Value, Me.dpCodigo_cac.SelectedValue, 0, 0)
                        Me.dlProfesores.DataBind()

                        strMax = ""
                        Me.hdSelHorario.Value = ""
                    Else
                        strMax = " <b style='color:Red'>Ha superado el maximo de asignaci&oacute;n de docentes para el curso seleccionado</b>"
                    End If

                    dt = obj.TraerDataTable("ACAD_RetornaDocentesPermitidos", Me.hdCodigo_Cup.Value)
                    Me.lblCarga.Text = (dt.Rows(0).Item("docentes")) & " de " & dt.Rows(0).Item("nrodocente_cup")
                    NumDocente(dt.Rows(0).Item("nrodocente_cup"))
                    If dt.Rows(0).Item("docentes") = dt.Rows(0).Item("nrodocente_cup") Then
                        'Me.cmdGuardar.Enabled = False
                        Me.gDataHorarioCurso.Enabled = False
                    Else
                        Me.gDataHorarioCurso.Enabled = True
                    End If

                    Me.lblCargaReg.Text = strMax
                    Me.lblCargaReg.Visible = True

                    fnListarHorario()
                  
                End If


                obj.CerrarConexion()

                obj = Nothing
                fnBotonera()

                '
                hdaCruceCurso.Value = ""
                aCruceCurso.Visible = False
                hdaCruceHorarioDisponible.Value = ""
                aCruceHorarioDisponible.Visible = False
                '




                Me.lblGrupoProfesores.Text = "DOCENTES ASIGNADOS (" & Me.dlProfesores.Items.Count & ")"
                Me.mpeFicha.Show()
            End If

        Catch ex As Exception
            Response.Write("cmdGuardar_Click: " & ex.Message)
        End Try

    End Sub

    Private Sub fnBotonera()
        If gDataHorarioCurso.Enabled Then
            Me.cmdGuardar.Enabled = True
        Else
            Me.cmdGuardar.Enabled = False
        End If


    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Dim codigo_usu As Integer = Request.QueryString("id")
        Dim codigo_tfu As Integer = Request.QueryString("ctf")

        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "DivLoad", "fnDivLoad('report',1000);", True)
        Me.grwGruposProgramados.Visible = False
        Me.hdCodigo_Cup.Value = 0
        If Me.dpCodigo_cpf.SelectedValue <> -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            If dpCodigo_cpf.SelectedValue = 19 Then
                Me.grwGruposProgramados.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoProgramacionAcademicaV2Comp", 6, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_cpf.SelectedValue, codigo_tfu, Me.dpEstado.SelectedValue, codigo_usu)
            Else
                Me.grwGruposProgramados.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoProgramacionAcademicaV2", 6, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_cpf.SelectedValue, codigo_tfu, Me.dpEstado.SelectedValue, codigo_usu)
            End If


            Me.grwGruposProgramados.DataBind()
            Me.grwGruposProgramados.Visible = True

            If Me.grwGruposProgramados.Rows.Count > 0 Then
                Me.btnExportar.Visible = True
            Else
                Me.btnExportar.Visible = False
            End If

            obj.CerrarConexion()
            obj = Nothing
            Me.ValidarPermisoAcciones()
            Me.dpCodigo_per.SelectedValue = -1
            Me.hdcodigo_cac.Value = Me.dpCodigo_cac.SelectedValue



        End If
    End Sub

    Protected Sub dlProfesores_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlProfesores.DeleteCommand
        If Me.dlProfesores.DataKeys.Count >= 1 Then
            Dim dt As Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("EliminarCargaAcademicaV2", CType(e.Item.FindControl("hdCodigo_per"), HiddenField).Value, Me.hdCodigo_Cup.Value, Request.QueryString("id"))
            'Cargar Profesores asignados
            Me.dlProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 14, Me.hdCodigo_Cup.Value, 0, 0, 0)
            Me.dlProfesores.DataBind()

            dt = obj.TraerDataTable("ACAD_RetornaDocentesPermitidos", Me.hdCodigo_Cup.Value)

            'Me.hdCodigo_Cup.Value = 0
            obj.CerrarConexion()
            obj = Nothing
            Me.mpeFicha.Show()
            Me.lblCarga.Text = (dt.Rows(0).Item("docentes")) & " de " & dt.Rows(0).Item("nrodocente_cup")
            Me.lblCargaReg.Visible = False

            If dt.Rows(0).Item("docentes") = dt.Rows(0).Item("nrodocente_cup") Then
                'Me.cmdGuardar.Enabled = False
                Me.gDataHorarioCurso.Enabled = False
            Else
                Me.gDataHorarioCurso.Enabled = True
            End If

            fnListarHorario()
            fnBotonera()
            If fnValidaGridHorarioCurso() Then
                'Me.dpCodigo_per2.Enabled = False
            Else
                'Me.dpCodigo_per2.Enabled = True
            End If
            'Dim sender As Object
            'cmdBuscar_Click(sender, New EventArgs())
        End If
    End Sub

    Protected Sub cmdEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnviar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            obj.Ejecutar("AgregarAccesoRecurso", "A", Request.QueryString("id"), Request.QueryString("id"), "cargaacademica", 0, True, True, True, True, CDate(Now), CDate(DateAdd(DateInterval.Day, 1, Now)), Me.txtmotivo.Text.Trim)
            obj.CerrarConexion()
            obj = Nothing
            Me.EnviarMensaje()
            Page.RegisterStartupScript("Solicitar", "<script>alert('Se ha enviado correctamente su solicitud de acceso. \nPor favor espere mientras ViceRectorado Académico responda su Solicitud vía E-mail')</script>")
        Catch ex As Exception
            Response.Write("cmdEnviar_Click: " & ex.Message)
            obj = Nothing
            Page.RegisterStartupScript("Solicitar", "<script>alert('Ocurrió un Error al Enviar la solicitud')</script>")
        End Try
    End Sub
    Protected Sub blstProfesoresSugeridos_Click(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.BulletedListEventArgs) Handles blstProfesoresSugeridos.Click
        dpCodigo_per2.SelectedValue = blstProfesoresSugeridos.Items(e.Index).Value
        VerificaCruce()
    End Sub
    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        'Set rs=ObjCarga.Consultar("ACAD_RetornaDocentesPermitidos","FO",codigo_cup)	
    End Sub
    Protected Sub gDataHorarioCurso_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gDataHorarioCurso.PreRender
        If gDataHorarioCurso.Rows.Count > 0 Then
            gDataHorarioCurso.UseAccessibleHeader = True
            gDataHorarioCurso.HeaderRow.TableSection = TableRowSection.TableHeader
        End If

    End Sub
    Protected Sub gDataHorarioCurso_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gDataHorarioCurso.RowDataBound
        Try

            Dim index As Integer = 0
            Dim NumCarga As Integer = CInt(hdNumCarga.Value.ToString)
            Dim sel As Integer = 0

            If e.Row.RowType = DataControlRowType.DataRow Then
                index = e.Row.RowIndex
                Dim checkAcceso As CheckBox
                checkAcceso = e.Row.FindControl("chkElegir")
                ' Response.Write(e.Row.FindControl("sel"))
                'checkAcceso.Checked = True

                If NumCarga = 1 Then
                    checkAcceso.Checked = True

                Else
                    checkAcceso.Checked = False
                    'Response.Write(e.Row.DataItem("sel").ToString)
                    'sel = CInt(e.Row.DataItem("sel").ToString)

                    ' If sel = 1 Then
                    'checkAcceso.Checked = True
                    'checkAcceso.Enabled = False

                    'End If



                    'sel = e.Row.
                    '  objTemp = gDataHorarioCurso.DataKeys(index).Value("sel")
                    '  Response.Write(objTemp)
                    'If Not objTemp Is Nothing Then
                    '    id = objTemp.ToString()
                    '    Response.Write(id)
                    'End If

                    '   sel = gDataHorarioCurso.DataKeys(index).Values("sel").ToString()




                End If
                'Response.Write("chkd: " & checkAcceso.Checked)

            End If
        Catch ex As Exception
            Response.Write("gDataHorarioCurso_RowDataBound : " & ex.Message)
        End Try
    End Sub

    Protected Sub dpCodigo_per2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpCodigo_per2.SelectedIndexChanged
        ' Me.dpCodigo_per2.SelectedValue = Me.dpCodigo_per2.SelectedValue
        Try
            VerificaCruce()
            ConsultarDisponibilidadHorarioDocente()
            VerificaCruceDisponible()
        Catch ex As Exception
            Response.Write("dpCodigo_per2_SelectedIndexChanged: " & ex.Message)
        End Try

    End Sub

    Protected Sub gDataHorarioDisponible_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gDataHorarioDisponible.PreRender
        If gDataHorarioDisponible.Rows.Count > 0 Then
            gDataHorarioDisponible.UseAccessibleHeader = True
            gDataHorarioDisponible.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub btnAceptarCruceHorarioCurso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarCruceHorarioCurso.Click
        hdaCruceCurso.Value = "1"
        aCruceCurso.Visible = True
        'ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "closeModal(1);", True)

    End Sub

    Protected Sub btnAceptarCruceHorarioDisponible_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarCruceHorarioDisponible.Click
        hdaCruceHorarioDisponible.Value = "1"
        aCruceHorarioDisponible.Visible = True
        'ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "closeModal(2);", True)

    End Sub

    Protected Sub aCruceCurso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles aCruceCurso.Click
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            SeleccionHorarioGrid()

            dt = obj.TraerDataTable("ACAD_ConsultarCurceAsignacionCargaV2", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_per2.SelectedValue, Me.hdCodigo_Cup.Value, Me.hdSelHorario.Value)
            If dt.Rows.Count > 0 Then
                Me.lblMensajeCruce.Text = "El horario del docente presenta: " & dt.Rows.Count & " cruce(s) de horario."

                'Me.dgvCruceHorarioDocente.DataSource = dt
                'Me.dgvCruceHorarioDocente.DataBind()

                fnTablaCruceHorarioCurso(dt)

                Me.btnAceptarCruceHorarioCurso.Visible = False
                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "openModal(1);", True)
            Else
                Me.lblMensajeCruce.Text = ""
                'Me.dgvCruceHorarioDocente.DataBind()
                Me.tbdCruceHorarioDocente.InnerHtml = ""

            End If
            obj.CerrarConexion()
            obj = Nothing
            Me.mpeFicha.Show()
        Catch ex As Exception
            Response.Write("aCruceCurso_Click: " & ex.Message)
        End Try

    End Sub

    Protected Sub aCruceHorarioDisponible_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles aCruceHorarioDisponible.Click
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim codigos_Hdo As String = hdSelHorario.Value

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            SeleccionHorarioGrid()
            'Response.Write(hdSelHorario.Value)

            'dt = obj.TraerDataTable("ACAD_ConsultarCurceAsignacionCarga", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_per2.SelectedValue, Me.hdCodigo_Cup.Value)
            dt = obj.TraerDataTable("ACAD_ConsultarCruceDisponibilidadHorarioV2", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_per2.SelectedValue, Me.hdCodigo_Cup.Value, Me.hdSelHorario.Value)
            If dt.Rows.Count > 0 Then
                ' Me.lblMensajeCruceDisponible.Text = "El horario del docente presenta: " & dt.Rows.Count & " cruce(s) de horario."
                Me.lblMensajeCruceDisponible.Text = "ADVERTENCIA: Según la disponibilidad registrada por el docente, el curso no puede ser asignado en el siguiente horario"

                'Me.dgvCruceHorarioDisponibleDocente.DataSource = dt
                'Me.dgvCruceHorarioDisponibleDocente.DataBind()
                fnTablaCruceHorarioDisponibleDocente(dt)

                Me.btnAceptarCruceHorarioDisponible.Visible = False
                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "openModal(2);", True)


            Else
                Me.lblMensajeCruceDisponible.Text = ""
                'Me.dgvCruceHorarioDisponibleDocente.DataBind()
                Me.tbdCruceHorarioDisponibleDocente.InnerHtml = ""
            End If
            obj.CerrarConexion()
            obj = Nothing
            ' Me.mpeFicha.Show()


        Catch ex As Exception
            Response.Write("aCruceHorarioDisponible_Click: " & ex.Message)
        End Try


    End Sub

    Protected Sub btnCerrarCruceHorario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrarCruceHorario.Click
        SeleccionHorarioGrid()
        VerificaCruceDisponible()

    End Sub

    Protected Sub dpCodigo_per_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpCodigo_per2.SelectedIndexChanged
        Me.hdaCruceCurso.Value = ""
        Me.hdaCruceHorarioDisponible.Value = ""
        aCruceHorarioDisponible.Visible = False
        aCruceCurso.Visible = False
        ConsultarDisponibilidadHorarioDocente()
        VerificaCruce()
        VerificaCruceDisponible()
        fnValidaDiaCursoyDisponibilidad()
        fnValidarHorasDia()

    End Sub


    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        'Dim sender As Object
        Dim st As New StringBuilder
        cmdBuscar_Click(sender, New EventArgs())
        mostrar(1)
        Me.gDataHorarioDisponible.DataSource = Nothing
        Me.gDataHorarioDisponible.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "DivLoad", "fnDivLoad('report',1000);", True)

    End Sub
    Protected Sub grwGruposProgramados_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwGruposProgramados.PreRender
        If grwGruposProgramados.Rows.Count > 0 Then
            grwGruposProgramados.UseAccessibleHeader = True
            grwGruposProgramados.HeaderRow.TableSection = TableRowSection.TableHeader
        End If

    End Sub

#End Region

#Region "Metodos"


    Private Sub fnTablaCruceHorarioCurso(ByVal dt As DataTable)
        Try
            Dim i As Integer = 0
            Dim t As New StringBuilder

            For i = 0 To dt.Rows.Count - 1
                t.Append("<tr>")
                t.Append("<td style='border-left: solid 1px;'>" & dt.Rows(i).Item("Escuela").ToString & "</td>")
                t.Append("<td>" & dt.Rows(i).Item("Curso").ToString & "</td>")
                t.Append("<td>" & dt.Rows(i).Item("Grupo").ToString & "</td>")
                t.Append("<td>" & dt.Rows(i).Item("Día").ToString & "</td>")
                t.Append("<td>" & dt.Rows(i).Item("Inicio").ToString & "</td>")
                t.Append("<td>" & dt.Rows(i).Item("Fin").ToString & "</td>")
                t.Append("<td style='border-left: solid 1px;'>" & dt.Rows(i).Item("CruceInicio").ToString & "</td>")
                t.Append("<td style='border-right: solid 1px;'>" & dt.Rows(i).Item("CruceFin").ToString & "</td>")
                t.Append("</tr>")
            Next
            Me.tbdCruceHorarioDocente.InnerHtml = t.ToString
        Catch ex As Exception
            Response.Write("fnTablaCruceHorarioCurso: " & ex.Message)
        End Try

    End Sub

    Private Sub fnTablaCruceHorarioDisponibleDocente(ByVal dt As DataTable)
        Try
            Dim i As Integer = 0
            Dim t As New StringBuilder

            For i = 0 To dt.Rows.Count - 1
                t.Append("<tr>")
                t.Append("<td style='border-left: solid 1px;background-color:#D8D8D8;'>" & dt.Rows(i).Item("dia").ToString & "</td>")
                t.Append("<td style='background-color:#D8D8D8;'>" & dt.Rows(i).Item("Inicio").ToString & "</td>")
                t.Append("<td style='background-color:#D8D8D8;'>" & dt.Rows(i).Item("Fin").ToString & "</td>")
                t.Append("<td style='border-left: solid 1px;'>" & dt.Rows(i).Item("CruceInicio").ToString & "</td>")
                t.Append("<td >" & dt.Rows(i).Item("CruceFin").ToString & "</td>")
                t.Append("<td style='border-right: solid 1px;'> " & dt.Rows(i).Item("TIPO").ToString & "</td>")
                t.Append("</tr>")
            Next
            Me.tbdCruceHorarioDisponibleDocente.InnerHtml = t.ToString

        Catch ex As Exception

        End Try

    End Sub


    Private Sub fnLoading(ByVal sw As Boolean)
        If sw Then
            ' Response.Write(1 & "<br>")
            Me.loading.Attributes.Remove("class")
            Me.loading.Attributes.Add("class", "hidden")
        Else
            ' Response.Write(0 & "<br>")
            Me.loading.Attributes.Remove("class")
            Me.loading.Attributes.Add("class", "")
            ' Me.loading.Attributes.Add("class", "show")
        End If
    End Sub
    Private Sub NumDocente(ByVal num As Integer)
        Me.hdNumCarga.Value = num
        'Me.lblNumCarga.Text = num

    End Sub
    Public Sub EnviarMensaje()
        'Enviar email
        Dim ObjMailNet As New ClsMail
        Dim Mensaje, Correo, AsuntoCorreo, ConCopiaA As String

        Mensaje = "<h3 style='font-color:blue'>Hay 1 Solicitud Pendiente para Habilitar el acceso a Modificar Carga Académica, registrada el " & Now.ToString & "</h3>"
        Mensaje = Mensaje & "<h4>La Solicitud ha sido requerda por la Dirección del " & Me.lblnombre_dac.Text & "</h2>"
        '---------------------------------------------------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: dguevara
        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
        '----------------------
        Mensaje = Mensaje & "<h4>Para atender esta solicitud, <a href=""https://intranet.usat.edu.pe/campusvirtual/personal/academico/cargalectiva/frmsolicitudcambios.aspx?id=296"">Haga clic aquí</a></h4>"
        Mensaje = Mensaje & "<h5>Se recomienda no reeenviar este email, ya que contiene datos de acceso al sistema. <br>Si el enlace no funciona ingresar a www.usat.edu.pe/campusvirtual, Módulo Académico, Movimientos, Carga Académica, Habilitar acceso</h5>"

        Correo = "mcervera@usat.edu.pe"
        AsuntoCorreo = "1 Solicitud Pendiente para habilitar Acceso a Modificar Carga Académica"
        ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Módulo de Carga Académica", Correo, AsuntoCorreo, Mensaje, True)
        ObjMailNet = Nothing
    End Sub

    Private Sub ValidarPermisoAcciones()
        If Me.dpCodigo_cpf.SelectedValue <> -1 And Me.grwGruposProgramados.Rows.Count > 0 Then

            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")
            Dim codigo_test As Integer = Request.QueryString("mod")
            '=========================================
            'Verificar permisos de acción
            '=========================================
            Dim Agregar, Modificar, Eliminar As Boolean

            If codigo_tfu <> 1 Then
                Dim tblPermisos As Data.DataTable
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                tblPermisos = obj.TraerDataTable("ValidarPermisoAccionesEnProcesoMatriculaCargaV2", "0", Me.dpCodigo_cac.SelectedValue, codigo_usu, "cargaacademica", codigo_test)
                If tblPermisos.Rows.Count > 0 Then
                    Agregar = tblPermisos.Rows(0).Item("agregar_acr")
                    Modificar = tblPermisos.Rows(0).Item("modificar_acr")
                    Eliminar = tblPermisos.Rows(0).Item("eliminar_acr")

                Else

                End If
                ' FIN
                obj.CerrarConexion()
                obj = Nothing
            Else
                Agregar = True
                Modificar = True
                Eliminar = True


            End If

            '=========================================
            'Validar Acción de Botones
            '=========================================
            Me.lblMensaje.Visible = True

            If Agregar = True Then
                Me.cmdAgregar.Visible = False ' True

                Me.lnkSolicitar.Visible = False

                Me.lblMensaje.Text = "<img src='../../../images/bien.gif' style='width:15px;height:15px;'>&nbsp;Su acceso para realizar modificaciones ha sido Habilitado"
                Session("PermitirCarga") = "SI"
            Else
                Me.lnkSolicitar.Visible = True
                Me.lblMensaje.Text = "<img src='../../../images/bloquear.gif' style='width:15px;height:15px;'>&nbsp;El Acceso para Agregar/Modificar la Carga Académica ha finalizado."
                Me.cmdAgregar.Visible = False
                Session("PermitirCarga") = "NO"
            End If
        End If
    End Sub

    Private Sub SeleccionHorarioGrid() 'fnCantidadSelectHorario
        Try

            Dim i As Integer = 0
            Dim canSel As Integer = 0
            Dim strSelHorario As String = ""

            canSel = CInt(hdNumCarga.Value)
            Dim sel As Integer = 0
            Dim filas As Integer = gDataHorarioCurso.Rows.Count
            Dim Fila As GridViewRow

            hdSelHorario.Value = ""
            ' Response.Write(filas & "  -  " & sel)
            For i = 0 To filas - 1
                Fila = Me.gDataHorarioCurso.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                '  Dim enable As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Enabled

                'If (valor = True) And (valor = True) Then
                If (valor = True) Then
                    sel = sel + 1
                    strSelHorario = strSelHorario & Me.gDataHorarioCurso.DataKeys(i).Values("codigo_Hdo").ToString & ","
                End If
            Next
            hdSelHorario.Value = strSelHorario
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ConsultarDisponibilidadHorarioDocente()
        Try

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("ACAD_RestriccionPersonal_Consultar", "4", 0, 0, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_per2.SelectedValue, 0, "")
            Me.gDataHorarioDisponible.DataSource = tb
            Me.gDataHorarioDisponible.DataBind()
            Session("gDataHorarioDisponible") = tb
            obj.CerrarConexion()
            obj = Nothing


        Catch ex As Exception
            Dim script As String = "fnMensaje('error','" & ex.Message & "'); "
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try
    End Sub

    Private Sub fnListarHorario()

        Me.gDataHorarioCurso.DataSource = Nothing
        Me.gDataHorarioCurso.DataBind()
        Session("gDataHorarioCurso") = Nothing

        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable

        'Response.Write(hdCodigo_Cup.Value)

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("ConsultarHorarios", 22, Me.hdCodigo_Cup.Value, 0, 0)

       
        If dt.Rows.Count > 0 Then
            Me.gDataHorarioCurso.DataSource = dt
            Me.gDataHorarioCurso.DataBind()
            Session("gDataHorarioCurso") = dt
        End If

        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub chckchanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim contador As Integer = 0
        Dim chckheader As CheckBox = CType(gDataHorarioCurso.HeaderRow.FindControl("chkall"), CheckBox)
        'contador = 0
        For Each row As GridViewRow In gDataHorarioCurso.Rows
            Dim chckrw As CheckBox = CType(row.FindControl("chkElegir"), CheckBox)
            If chckheader.Checked = True Then
                chckrw.Checked = True
            Else
                chckrw.Checked = False
            End If
            If chckrw.Checked = True Then
                contador = contador + 1
                '  row.ControlStyle.BackColor = Drawing.Color.AntiqueWhite
                row.ControlStyle.Font.Bold = True
            Else
                '  row.ControlStyle.BackColor = Drawing.Color.White
                row.ControlStyle.Font.Bold = False
            End If
        Next
        'Me.lblContadorSeleccionado.Text = contador.ToString
    End Sub

#End Region

#Region "Procedimiento"
    Private Sub VerificaCruceDisponible()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim codigos_Hdo As String = hdSelHorario.Value

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'dt = obj.TraerDataTable("ACAD_ConsultarCurceAsignacionCarga", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_per2.SelectedValue, Me.hdCodigo_Cup.Value)
            dt = obj.TraerDataTable("ACAD_ConsultarCruceDisponibilidadHorarioV2", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_per2.SelectedValue, Me.hdCodigo_Cup.Value, Me.hdSelHorario.Value)
            If dt.Rows.Count > 0 Then
                'Me.lblMensajeCruceDisponible.Text = "El docente ya tiene asignado los siguientes cursos: se encontró" & dt.Rows.Count & " cruce(s) de horario."
                Me.lblMensajeCruceDisponible.Text = "ADVERTENCIA: Según la disponibilidad registrada por el docente, el curso no puede ser asignado en el siguiente horario"

                Me.btnAceptarCruceHorarioDisponible.Visible = False
                Me.hdaCruceHorarioDisponible.Value = ""
                Me.aCruceHorarioDisponible.Visible = True

                'Me.dgvCruceHorarioDisponibleDocente.DataSource = dt
                'Me.dgvCruceHorarioDisponibleDocente.DataBind()
                'Me.aCruceHorarioDisponible.Visible = True
                fnTablaCruceHorarioDisponibleDocente(dt)



                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "openModal(2);", True)

            Else
                Me.lblMensajeCruceDisponible.Text = ""
                'Me.dgvCruceHorarioDisponibleDocente.DataBind()
                Me.tbdCruceHorarioDisponibleDocente.InnerHtml = ""
                Me.aCruceHorarioDisponible.Visible = False
            End If
            obj.CerrarConexion()
            obj = Nothing
            Me.mpeFicha.Show()

            If dt.Rows.Count > 0 Then

            Else

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub VerificaCruce()
        Try

            ' Response.Write(Me.dpCodigo_per2.SelectedIndex)
            If Me.dpCodigo_per2.SelectedIndex = -1 Then
                Me.dpCodigo_per2.SelectedIndex = 0
            End If

            If Me.dpCodigo_per2.SelectedIndex > 0 Then

                Dim obj As New ClsConectarDatos
                Dim dt As New Data.DataTable

                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()

                SeleccionHorarioGrid()
                dt = obj.TraerDataTable("ACAD_ConsultarCurceAsignacionCargaV2", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_per2.SelectedValue, Me.hdCodigo_Cup.Value, Me.hdSelHorario.Value)
                If dt.Rows.Count > 0 Then
                    Me.lblMensajeCruce.Text = "El horario del docente presenta: " & dt.Rows.Count & " cruce(s) de horario."

                    btnAceptarCruceHorarioCurso.Visible = False
                    Me.hdaCruceCurso.Value = ""
                    aCruceCurso.Visible = True



                    'Me.dgvCruceHorarioDocente.DataSource = dt
                    'Me.dgvCruceHorarioDocente.DataBind()

                    fnTablaCruceHorarioCurso(dt)

                    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "openModal(1);", True)
                Else
                    aCruceHorarioDisponible.Visible = False
                    Me.lblMensajeCruce.Text = ""
                    Me.tbdCruceHorarioDocente.InnerHtml = ""
                    'Me.dgvCruceHorarioDocente.DataBind()
                End If
                obj.CerrarConexion()
                obj = Nothing
                Me.mpeFicha.Show()

            Else

                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Mensaje", "fnMensaje('warning','Seleccione docente');", True)

            End If
        Catch ex As Exception
            Response.Write("Error cruce: " & ex.Message)
        End Try

    End Sub

    Private Sub mostrar(ByVal tipo As Int16)
        If tipo = 1 Then
            PanelLista.Visible = True
            PanelRegistro.Visible = False
        ElseIf tipo = 2 Then
            PanelLista.Visible = False
            PanelRegistro.Visible = True
        End If

    End Sub

    Private Sub fnListarDocentes()
        Try
            'Response.Write(Me.hdCodigo_Cup.Value)
            Dim obj As New ClsConectarDatos
            Dim objFun As New ClsFunciones
            Dim dt As New Data.DataTable
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_per As Integer = Request.QueryString("id")

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 13, Me.dpCodigo_cac.SelectedValue, Me.hdCodigo_Cup.Value, codigo_per, codigo_tfu)
            'Yperez 13.01.20 --TIPO 17 Solo lista docentes adscritos
            dt = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 17, Me.dpCodigo_cac.SelectedValue, Me.hdCodigo_Cup.Value, codigo_per, codigo_tfu)

            'objFun.CargarListas(Me.dpCodigo_per, dt, "codigo_Per", "docente", "--------------Seleccione el Profesor adscrito al Dpto--------------")
            objFun.CargarListas(Me.dpCodigo_per2, dt, "codigo_Per", "docente", "--------------Seleccione el Profesor adscrito al Dpto--------------")
            obj.CerrarConexion()

            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub fnValidaDiaCursoyDisponibilidad()
        Try
            ' ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "DivLoadEstilo", "fnEstilo();", True)
            'SeleccionHorarioGrid()
            Dim i As Integer = 0
            Dim k As Integer = 0
            Dim contar As Integer = 0
            Dim filas As Integer = gDataHorarioCurso.Rows.Count
            Dim Fila As GridViewRow
            Dim codlho As Integer = 0
            Dim dt As New DataTable

            Dim tc As Boolean = False


            ' Response.Write(dpCodigo_per2.SelectedValue)
            If dpCodigo_per2.SelectedValue = -1 Then
                lblVerificaDia.InnerHtml = ""
                Exit Sub

            End If

            Dim dedicacion As String
            dedicacion = dpCodigo_per2.SelectedItem.Text.ToString
            'Response.Write(dedicacion)

            dt = CType(Session("gDataHorarioCurso"), Data.DataTable)

            Dim dt2 As New DataTable
            dt2 = CType(Session("gDataHorarioDisponible"), Data.DataTable)
            lblVerificaDia.InnerHtml = ""

            If dedicacion.Contains("TIEMPO COMPLETO") Then
                lblVerificaDia.InnerHtml = ""
                tc = True
            Else
                tc = False
            End If
            ' Response.Write(filas & "  -  " & sel)

            If tc = False Then


                For i = 0 To filas - 1
                    Fila = Me.gDataHorarioCurso.Rows(i)
                    Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                    'Dim enable As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Enabled
                    'If (valor = True) And (enable = True) Then
                    If (valor = True) Then
                        'Response.Write("hlo:" & Me.gDataHorarioCurso.DataKeys(i).Values("codigo_Hdo").ToString)
                        'strSelHorario = strSelHorario & Me.gDataHorarioCurso.DataKeys(i).Values("codigo_Hdo").ToString & ","
                        codlho = CInt(Me.gDataHorarioCurso.DataKeys(i).Values("codigo_Hdo").ToString)


                        If codlho > 0 Then


                            Dim nomDiaCompleto As String = fnBuscarNombreDia(dt, codlho)
                            '  Response.Write(nomDiaCompleto)

                            If dt2.Rows.Count > 0 Then


                                '    Response.Write(nomDiaCompleto & "<br>")
                                '    For k = 0 To dt2.Rows.Count - 1
                                '        Response.Write(dt2.Rows(k).Item("nomdia").ToString & "=" & nomDiaCompleto & "<br>")
                                '        If nomDia <> "" And dt2.Rows(k).Item("nomdia").ToString = nomDiaCompleto Then
                                '            contar = contar + 1
                                '            Exit For
                                '        End If
                                '    Next

                                For k = 0 To dt2.Rows.Count - 1
                                    If dt2.Rows(k).Item("nomdia").ToString = nomDiaCompleto Then
                                        contar = contar + 1
                                        Exit For
                                    End If
                                Next

                            End If

                            If contar = 0 Then

                                If nomDiaCompleto <> "" Then

                                    lblVerificaDia.InnerHtml = "<i class='ion-android-warning'></i> ADVERTENCIA: No existe disponibilidad del docente para el día: " & nomDiaCompleto & "  en el curso seleccionado"

                                Else
                                    lblVerificaDia.InnerHtml = ""
                                End If

                            Else

                                lblVerificaDia.InnerHtml = ""
                            End If
                            contar = 0


                        End If
                    End If
                Next
            Else
                lblVerificaDia.InnerHtml = "<i class='ion-android-warning'></i> Ud. Tiene dedicación de TIEMPO COMPLETO"
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Function fnBuscarNombreDia(ByVal dt As DataTable, ByVal codHdo As Integer) As String
        Try
            Dim st As String = ""
            Dim i As Integer = 0

            For i = 0 To dt.Rows.Count - 1

                If dt.Rows(i).Item("codigo_Hdo") = codHdo Then
                    st = dt.Rows(i).Item("dia_Lho").ToString()
                    'Response.Write(st)
                    st = fnNombreDiaCompleto(st)
                    Exit For
                End If
            Next
            Return st

        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function fnNombreDiaCompleto(ByVal dia As String) As String
        Try
            Dim n As String

            Select Case dia
                Case "LU"
                    n = "LUNES"
                Case "MA"
                    n = "MARTES"
                Case "MI"
                    n = "MIERCOLES"
                Case "JU"
                    n = "JUEVES"
                Case "VI"
                    n = "VIERNES"
                Case "SA"
                    n = "SABADO"
                Case Else
                    n = ""

            End Select

            Return n
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Private Function fnValidaGridHorarioCurso() As Boolean '' verificar uso, se comentó
        Try
            Dim i As Integer = 0
            Dim canSel As Integer = 0
            'Dim strSelHorario As String = ""

            canSel = CInt(hdNumCarga.Value)
            Dim sel As Integer = 0
            Dim filas As Integer = gDataHorarioCurso.Rows.Count
            Dim Fila As GridViewRow
            hdSelHorario.Value = ""

            ' Response.Write(filas & "  -  " & sel)
            For i = 0 To filas - 1
                Fila = Me.gDataHorarioCurso.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                'Dim enable As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Enabled
                '                If (valor = True) And (enable = True) Then
                If (valor = True) Then
                    sel = sel + 1
                    'strSelHorario = strSelHorario & Me.gDataHorarioCurso.DataKeys(i).Values("codigo_Hdo").ToString & ","
                End If
            Next


            If sel = 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "Funciones"
    Private Function fnValidaGuardar() As Boolean
        Try
            Dim per As String = Me.dpCodigo_per2.SelectedValue
            '  Response.Write("codigo_per: " & per)


            If fnCantidadSelectHorario() = False Then

                Dim script As String = "fnMensaje('warning','Seleccion horaro del curso'); "
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)

                Return False

            End If


            If per = "" Or per = "-1" Then
                Dim script As String = "fnMensaje('warning','Seleccione docente'); fnFoco('dpCodigo_per2')"
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)

                Return False
            End If

            If VerificaCruceHorario() Then

                'Return False
            End If


            If VerificaCruceHorarioDisponible() Then


                'Return False
            End If


            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Private Function fnCantidadSelectHorario() As Boolean
        Try
            Dim i As Integer = 0
            Dim canSel As Integer = 0
            Dim strSelHorario As String = ""

            canSel = CInt(hdNumCarga.Value)
            Dim sel As Integer = 0
            Dim filas As Integer = gDataHorarioCurso.Rows.Count
            Dim Fila As GridViewRow

            hdSelHorario.Value = ""
            ' Response.Write(filas & "  -  " & sel)
            For i = 0 To filas - 1
                Fila = Me.gDataHorarioCurso.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                ' Dim enable As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Enabled
                'If (valor = True) And (enable = True) Then
                If (valor = True) Then
                    sel = sel + 1
                    strSelHorario = strSelHorario & Me.gDataHorarioCurso.DataKeys(i).Values("codigo_Hdo").ToString & ","
                End If
            Next


            Dim script As String = ""
            ' Response.Write("<br>" & filas & "  -  " & sel)
            hdSelHorario.Value = strSelHorario
            If canSel = 1 Then
                If filas = sel Then
                    Return True
                Else
                    script = "fnMensaje('warning','Seleccione todos los horario del curso <i class=ion-android-checkbox-outline></i> '); "
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)

                    Return False
                End If
            ElseIf canSel > 1 Then
                If sel >= 1 Then
                    Return True
                Else
                    script = "fnMensaje('warning','Seleccion al menos un horario del curso <i class=ion-android-checkbox-outline></i> '); "
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                    Return False
                End If
            Else
                script = "fnMensaje('warning','Seleccion horario del curso'); "
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
                Return False
            End If


        Catch ex As Exception
            Return False
        End Try

    End Function
    Private Function VerificaCruceHorario() As Boolean
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim codigos_Hdo As String = hdSelHorario.Value

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'dt = obj.TraerDataTable("ACAD_ConsultarCurceAsignacionCarga", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_per2.SelectedValue, Me.hdCodigo_Cup.Value)
            dt = obj.TraerDataTable("ACAD_ConsultarCruceAsignacionCargaV2", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_per2.SelectedValue, Me.hdCodigo_Cup.Value, Me.hdSelHorario.Value)
            If dt.Rows.Count > 0 Then
                Me.lblMensajeCruce.Text = "El horario del docente presenta: " & dt.Rows.Count & " cruce(s) de horario."

                'Response.Write("cc " & hdaCruceCurso.Value.ToString)
                'Me.dgvCruceHorarioDocente.DataSource = dt
                'Me.dgvCruceHorarioDocente.DataBind()
                fnTablaCruceHorarioCurso(dt)

                If hdaCruceCurso.Value.ToString = "" Then
                    btnAceptarCruceHorarioCurso.Visible = True
                End If
                If hdaCruceCurso.Value.ToString <> "1" Then
                    'Response.Write("abre")
                    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "openModal(1);", True)
                End If

                ' aCruceCurso.Visible = True
            Else
                Me.lblMensajeCruce.Text = ""
                'Me.dgvCruceHorarioDocente.DataBind()
                Me.tbdCruceHorarioDocente.InnerHtml = ""
                aCruceCurso.Visible = False

            End If
            obj.CerrarConexion()
            obj = Nothing
            Me.mpeFicha.Show()
            If dt.Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function
    Private Function VerificaCruceHorarioDisponible() As Boolean
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim codigos_Hdo As String = hdSelHorario.Value.ToString

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()



            'dt = obj.TraerDataTable("ACAD_ConsultarCurceAsignacionCarga", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_per2.SelectedValue, Me.hdCodigo_Cup.Value)
            dt = obj.TraerDataTable("ACAD_ConsultarCruceDisponibilidadHorarioV2", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_per2.SelectedValue, Me.hdCodigo_Cup.Value, Me.hdSelHorario.Value)
            If dt.Rows.Count > 0 Then
                'Me.lblMensajeCruceDisponible.Text = "El horario del docente presenta: " & dt.Rows.Count & " cruce(s) de horario."
                Me.lblMensajeCruceDisponible.Text = "ADVERTENCIA: Según la disponibilidad registrada por el docente, el curso no puede ser asignado en el siguiente horario"

                'Me.dgvCruceHorarioDisponibleDocente.DataSource = dt
                'Me.dgvCruceHorarioDisponibleDocente.DataBind()
                fnTablaCruceHorarioDisponibleDocente(dt)
                Me.aCruceHorarioDisponible.Visible = True


                If hdaCruceHorarioDisponible.Value.ToString = "" Then
                    btnAceptarCruceHorarioDisponible.Visible = True
                End If

                If hdaCruceHorarioDisponible.Value.ToString <> "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "openModal(2);", True)
                End If


            Else
                Me.lblMensajeCruceDisponible.Text = ""
                'Me.dgvCruceHorarioDisponibleDocente.DataBind()
                Me.tbdCruceHorarioDisponibleDocente.InnerHtml = ""
                Me.aCruceHorarioDisponible.Visible = False
            End If
            obj.CerrarConexion()
            obj = Nothing
            Me.mpeFicha.Show()
            If dt.Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub fnValidarHorasDia()
        Try
            Dim i As Integer = 0
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim codigos_Hdo As String = hdSelHorario.Value.ToString
            Dim st As New StringBuilder
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("CAD_ConsultarTotalHorasPorDia", "1", Me.dpCodigo_per2.SelectedValue, Me.hdcodigo_cac.Value)
            obj.CerrarConexion()
            obj = Nothing

            If dt.Rows.Count > 0 Then
                st.Append("<ul class='list-group'>")

                For i = 0 To dt.Rows.Count - 1
                    st.Append("<li class='list-group-item'>")
                    If CInt(dt.Rows(i).Item("totalHD").ToString) >= 7 Then
                        st.Append("<span class='badge pull-right bg-danger'>" & dt.Rows(i).Item("totalHD").ToString & " hrs.</span>" & dt.Rows(i).Item("dia").ToString)
                    Else
                        st.Append("<span class='badge pull-right bg-primary'>" & dt.Rows(i).Item("totalHD").ToString & " hrs.</span>" & dt.Rows(i).Item("dia").ToString)
                    End If
                    st.Append("</li>")
                Next


                st.Append("</ul>")
                divTotalHorasDia.InnerHtml = st.ToString
                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "openModal(3);", True)
            End If


            dt = Nothing
        Catch ex As Exception
            Dim script As String = "fnMensaje('error','" & ex.Message & "'); "
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try
    End Sub


    Protected Sub chkview_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If fnValidaGridHorarioCurso() Then
                Me.dpCodigo_per2.SelectedIndex = 0
                'Me.dpCodigo_per2.Enabled = False
            Else
                'Me.dpCodigo_per2.Enabled = True

            End If
            VerificaCruce()
            VerificaCruceDisponible()
            ConsultarDisponibilidadHorarioDocente()
            fnValidaDiaCursoyDisponibilidad()
            ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "DivLoadEstilo", "fnEstilo();", True)
        Catch ex As Exception

        End Try
    End Sub
#End Region


    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.grwGruposProgramados.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.grwGruposProgramados)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=AsignacionDeVacantes.xls")
        'Response.Charset = "UTF-8"
        'Response.ContentEncoding = Encoding.Default        
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class
﻿
Partial Class academico_cargalectiva_FrmAsignaCargaAcademicaPRE
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")
            Dim Modulo As Integer = Request.QueryString("mod")
			
			

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            '=========================================
            'Llenas Ciclo & Escuela, Profesores
            '=========================================
            Dim objFun As New ClsFunciones
            objFun.CargarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "UCI", 0), "codigo_cac", "descripcion_cac")
            objFun.CargarListas(Me.dpCodigo_cpf, obj.TraerDataTable("EVE_ConsultarCarreraProfesional", Modulo, codigo_tfu, codigo_usu), "codigo_cpf", "nombre_cpf", "--Seleccione la Escuela Profesional--")

            '=========================================
            'Obtener el Dpto. Académico como Director
            '=========================================
            Dim tblDpto As Data.DataTable
            'tblDpto = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 2, codigo_usu, codigo_tfu, 0, 0)
            tblDpto = obj.TraerDataTable("CAR_ConsultarCargaAcademicaPRE", codigo_usu)
            If tblDpto.Rows.Count > 0 Then
                Me.lblnombre_dac.Text = Replace(tblDpto.Rows(0).Item("nombre_dac").ToString, "DEPARTAMENTO ACADEMICO", "DPTO:")
                Me.hdcodigo_dac.Value = tblDpto.Rows(0).Item("codigo_dac").ToString
                '=========================================
                'Cargar Profesores adscritos
                '=========================================
                objFun.CargarListas(Me.dpCodigo_per, obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 5, Me.hdcodigo_dac.Value, Me.hdCodigo_Cup.Value, Me.dpCodigo_cac.SelectedValue, 0), "codigo_per", "profesor", "--Seleccione el Profesor adscrito al Dpto.--")
            End If

            obj.CerrarConexion()
            obj = Nothing
            Me.lblmensaje.visible = False
            Me.ValidarPermisoAcciones()
        End If
    End Sub
    Protected Sub dpCodigo_cac_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpCodigo_cac.SelectedIndexChanged
        Me.hdCodigo_Cup.Value = 0
        Me.grwGruposProgramados.visible = False
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
            gr.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 4, fila.row("codigo_cup"), Me.dpcodigo_cac.selectedvalue, 0, 0)
            gr.DataBind()
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub
    Protected Sub grwGruposProgramados_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwGruposProgramados.SelectedIndexChanged, cmdPopUp.Click
        Me.hdCodigo_Cup.Value = Me.grwGruposProgramados.DataKeys.Item(Me.grwGruposProgramados.SelectedIndex).Values("codigo_cup").ToString
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Cargar Profesores asignados
        Me.dlProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 4, Me.hdCodigo_Cup.Value, 0, 0, 0)
        Me.dlProfesores.DataBind()

        'Cargar profesores sugeridos
        ClsFunciones.LlenarListas(Me.blstProfesoresSugeridos, obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 6, Me.hdcodigo_dac.Value, Me.hdCodigo_Cup.Value, Me.dpCodigo_cac.SelectedValue, 0), "codigo_per", "profesor")
        'Cargar Horario del curso

        'Response.Write(Me.grwGruposProgramados.Rows.Item(Me.grwGruposProgramados.SelectedIndex).Cells(15).Text)
        'ClsFunciones.LlenarListas(Me.bllHorarioCurso, obj.TraerDataTable("ConsultarHorarios", 4, Me.grwGruposProgramados.Rows.Item(Me.grwGruposProgramados.SelectedIndex).Cells(15).Text, 0, 0), "horas", "horas")
        ClsFunciones.LlenarListas(Me.bllHorarioCurso, obj.TraerDataTable("ConsultarHorarios", 4, Me.hdCodigo_Cup.Value, 0, 0), "horas", "horas")

        obj.CerrarConexion()
        obj = Nothing
        Me.lblcurso.Text = CType(Me.grwGruposProgramados.Rows.Item(Me.grwGruposProgramados.SelectedIndex).Cells(1).FindControl("lblnombre_cur"), Label).Text
        Me.lblgrupo.Text = Me.grwGruposProgramados.Rows.Item(Me.grwGruposProgramados.SelectedIndex).Cells(2).Text

        Me.lblGrupoProfesores.Text = "PROFESORES ASIGNADOS (" & Me.dlProfesores.Items.Count & ")"

        Me.mpeFicha.Show()
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim msj(1) As Object
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        obj.AbrirConexion()

        dt = obj.TraerDataTable("ACAD_RetornaDocentesPermitidos", Me.hdCodigo_Cup.Value)

        If (dt.Rows.Count > 0) Then
            If (dt.Rows(0).Item("nrodocente_cup") >= dt.Rows(0).Item("docentes") + 1) Then
                obj.Ejecutar("CAR_AgregarCargaAcademica", Me.dpCodigo_per.SelectedValue, Me.hdCodigo_Cup.Value, Request.QueryString("id"), Request.QueryString("mod"), "").copyto(msj, 0)

                'Limpiar valores
                Me.dlProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 4, Me.hdCodigo_Cup.Value, 0, 0, 0)
                Me.dlProfesores.DataBind()
            Else

            End If


            dt = obj.TraerDataTable("ACAD_RetornaDocentesPermitidos", Me.hdCodigo_Cup.Value)
            Me.lblCarga.Text = dt.Rows(0).Item("docentes") & " de " & dt.Rows(0).Item("nrodocente_cup")

        End If


        obj.CerrarConexion()
        obj = Nothing
        Me.lblGrupoProfesores.Text = "PROFESORES ASIGNADOS (" & Me.dlProfesores.Items.Count & ")"
        Me.mpeFicha.Show()

    End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Me.grwGruposProgramados.Visible = False
        Me.hdCodigo_Cup.Value = 0
        If Me.dpCodigo_cpf.SelectedValue <> -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Me.grwGruposProgramados.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoProgramacionAcademica", 6, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_cpf.SelectedValue, Me.hdcodigo_dac.Value, Me.dpEstado.SelectedValue)
            Me.grwGruposProgramados.DataBind()
            Me.grwGruposProgramados.Visible = True
            obj.CerrarConexion()
            obj = Nothing
            Me.ValidarPermisoAcciones()
        End If
    End Sub
    Protected Sub dlProfesores_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlProfesores.DeleteCommand
        If Me.dlProfesores.DataKeys.Count >= 1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("EliminarCargaAcademica", CType(e.Item.FindControl("hdCodigo_per"), HiddenField).Value, Me.hdCodigo_Cup.Value, Request.QueryString("id"))
            'Cargar Profesores asignados
            Me.dlProfesores.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 4, Me.hdCodigo_Cup.Value, 0, 0, 0)
            Me.dlProfesores.DataBind()
            Me.hdCodigo_Cup.Value = 0
            obj.CerrarConexion()
            obj = Nothing
            Me.mpeFicha.Show()
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
            obj = Nothing
            Page.RegisterStartupScript("Solicitar", "<script>alert('Ocurrió un Error al Enviar la solicitud')</script>")
        End Try
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
        If Me.dpCodigo_cpf.SelectedValue <> -1 And Me.grwGruposProgramados.rows.count > 0 Then

            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")
            '=========================================
            'Verificar permisos de acción
            '=========================================
            Dim Agregar, Modificar, Eliminar As Boolean
            If codigo_tfu <> 1 Then
                Dim tblPermisos As Data.DataTable
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                tblPermisos = obj.TraerDataTable("ValidarPermisoAccionesEnProcesoMatriculaCarga", "3", Me.dpCodigo_cac.SelectedValue, codigo_usu, "cargaacademica")
                If tblPermisos.Rows.Count > 0 Then
                    Agregar = tblPermisos.Rows(0).Item("agregar_acr")
                    Modificar = tblPermisos.Rows(0).Item("modificar_acr")
                    Eliminar = tblPermisos.Rows(0).Item("eliminar_acr")
                End If
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
            Me.lblMensaje.visible = True
            If Agregar = True Then
                Me.cmdAgregar.Visible = False ' True
                Me.lnkSolicitar.Visible = False

                Me.lblMensaje.Text = "<img src='../../../images/bien.gif'>&nbsp;Su acceso para realizar modificaciones ha sido Habilitado"
            Else
                Me.lnkSolicitar.Visible = True
                Me.lblMensaje.Text = "<img src='../../../images/bloquear.gif'>&nbsp;El Acceso para Agregar/Modificar la Carga Académica ha finalizado."
                Me.cmdAgregar.Visible = False
            End If
        End If
    End Sub



    Protected Sub blstProfesoresSugeridos_Click(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.BulletedListEventArgs) Handles blstProfesoresSugeridos.Click
        dpCodigo_per.SelectedValue = blstProfesoresSugeridos.Items(e.Index).Value
        VerificaCruce()

    End Sub

    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        'Set rs=ObjCarga.Consultar("ACAD_RetornaDocentesPermitidos","FO",codigo_cup)	
    End Sub

    Protected Sub dpCodigo_per_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpCodigo_per.SelectedIndexChanged
        VerificaCruce()
    End Sub
    Private Sub VerificaCruce()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        dt = obj.TraerDataTable("ACAD_ConsultarCurceAsignacionCarga", Me.dpCodigo_cac.selectedvalue, Me.dpCodigo_per.SelectedValue, Me.hdCodigo_Cup.Value)
        If dt.Rows.Count > 0 Then
            Me.lblMensajeCruce.Text = "El horario del profesor presenta: " & dt.Rows.Count & " cruce(s) de horario."

            Me.dgvCruceHorarioDocente.DataSource = dt
            Me.dgvCruceHorarioDocente.DataBind()

        Else
            Me.lblMensajeCruce.Text = ""
            Me.dgvCruceHorarioDocente.DataBind()
        End If
        obj.CerrarConexion()
        obj = Nothing
        Me.mpeFicha.Show()
    End Sub
End Class

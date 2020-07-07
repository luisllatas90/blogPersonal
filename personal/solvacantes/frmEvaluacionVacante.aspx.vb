﻿Imports System.IO   'Ultil para exporta a excel: dguevara 12.11.2013

Partial Class solvacantes_frmEvaluacionVacante
    Inherits System.Web.UI.Page
    Dim codigo_per As Int32 = -1
    Dim contador As Int16 = 0
    Dim PrimeraFila As Int16 = -1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If

        If Not IsPostBack Then
            Me.ddlEstado.SelectedValue = "P"
            CargaCicloAcademicos()
            CargarDepartamentoAcademico()
            RegistroSolicitudes()    '** Carga todas las solicitudes registradas **'
            MostrarDetalle()
            CargarCantidadEstados()  '** Muestra la cantidad de registros por estado **'
            'CargarDataList()        '** no cargamos datalist al iniciar xq genera error, debido a q no hay una solicitud selccionada.**'        --<< x este me sale la alerta de la instancia >>
            HabilitarControlesPerfil()
        End If
    End Sub

    Private Sub HabilitarControlesPerfil()
        Try
            '41     Vicerectorado Academico.
            '116    VICERRECTORADO DE PROFESORES
            'If Me.Request.QueryString("ctf") = 41 Or Me.Request.QueryString("ctf") = 1 Then
            If Me.Request.QueryString("ctf") = 116 Or Me.Request.QueryString("ctf") = 1 Or Me.Request.QueryString("ctf") = 94 Then
                Me.btnAprobar.Enabled = True
                Me.btnRechazar.Enabled = True
            Else
                Me.btnAprobar.Enabled = False
                Me.btnRechazar.Enabled = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarCargaAcademica()
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable

            dts = obj.VistaCargaAcademica("E", Me.ddlCicloAcademicoCarga.SelectedValue, Me.gvLista.SelectedRow.Cells(1).Text)
            If dts.Rows.Count > 0 Then
                grwCargaAcademica.DataSource = dts
            Else
                grwCargaAcademica.DataSource = Nothing
            End If
            grwCargaAcademica.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarDataList()
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable

            dts = obj.CargarListaComentarios(Me.gvLista.SelectedRow.Cells(1).Text)
            If dts.Rows.Count > 0 Then
                lblmensajecomentarios.Visible = False
                lblmensajecomentarios.Text = ""
                Me.DataList1.DataSource = dts
                Me.DataList1.DataBind()
            Else
                lblmensajecomentarios.Visible = True
                lblmensajecomentarios.Text = "   No se encontraron comentarios registrados para la solicitud vacante seleccionada."
                Me.DataList1.DataSource = Nothing
                Me.DataList1.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub CargarCantidadEstados()
        Try
            '** dguevara:08:11:2013 - 02:36pm
            Dim obj As New clsSolicitudVacante
            Dim cantidad As Integer
            cantidad = obj.CantidadSolEstado("A")   'Aprobados
            Me.lblnumAprobados.Text = "<b>Aprobados</b> (<font color='red'>" & cantidad.ToString & "</font>)"
            cantidad = obj.CantidadSolEstado("R")   'Aprobados
            Me.lblnumRechazados.Text = "<b>Rechazados</b> (<font color='red'>" & cantidad.ToString & "</font>)"
            cantidad = obj.CantidadSolEstado("P")   'Aprobados
            Me.lblnumPendientes.Text = "<b>Pendientes</b> (<font color='red'>" & cantidad.ToString & "</font>)"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub MostrarDetalle()
        Try
            If Me.HiddenField.Value = 0 Then
                Me.pnlMenu.Visible = False
                Me.pnlSeleccion.Visible = True
                If gvLista.Rows.Count = 0 Then
                    lblMensajeEntrada.Text = ""
                Else
                    lblMensajeEntrada.Text = "Favor de seleccionar un registro para ver sus detalles..."
                End If
            Else
                Me.pnlMenu.Visible = True
                Me.pnlSeleccion.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    '** Lista de todas las solicitudes **
    Private Sub RegistroSolicitudes()
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable

            dts = obj.ListaRegistroSolicitudesVacantes(Me.ddlCicloAcademico.SelectedValue, Me.ddlEstado.SelectedValue, Me.ddlDepartamento.SelectedValue, 0, "E", Session("id_per"))
            Me.lblnumeroregistros.Text = "Se encontraron ( <b><font color='yellow'>" & dts.Rows.Count.ToString & "</font></b> ) solicitudes registradas."
            If dts.Rows.Count > 0 Then
                gvLista.DataSource = dts
                gvLista.DataBind()
            Else
                gvLista.DataSource = Nothing
                gvLista.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub CargarDepartamentoAcademico()
        '** dguevara 24.10.2013 **
        '** Lista todos los permisos, no necesita tener configurado los permisos por departamento academico **'
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable

            dts = obj.ListaDepartamentosAcademicos("L", Session("id_per"))
            If dts.Rows.Count > 0 Then
                Me.ddlDepartamento.DataSource = dts
                Me.ddlDepartamento.DataTextField = "descripcion_dac"
                Me.ddlDepartamento.DataValueField = "Codigo_cac"
                Me.ddlDepartamento.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub CargaCicloAcademicos()
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable

            dts = obj.ListaCicloAcademicoSolicitudes
            If dts.Rows.Count > 0 Then

                '::: Combo Ciclo academico para la vacante creada :::
                Me.ddlCicloAcademico.DataSource = dts
                Me.ddlCicloAcademico.DataTextField = "descripcion_Cac"
                Me.ddlCicloAcademico.DataValueField = "codigo_Cac"
                Me.ddlCicloAcademico.DataBind()

                '::: Combo Ciclo academico para la mostrar la carga asignada :::
                Me.ddlCicloAcademicoCarga.DataSource = dts
                Me.ddlCicloAcademicoCarga.DataTextField = "descripcion_Cac"
                Me.ddlCicloAcademicoCarga.DataValueField = "codigo_Cac"
                Me.ddlCicloAcademicoCarga.DataBind()

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvLista.PageIndexChanging
        Try
            '** dguevara: 11.11.2013 **'
            'En las propiedes del gvLista establecemos
            '-------------------------------------------------
            '** AllowPaging="True"
            '** PageSize="6"
            '-------------------------------------------------
            gvLista.PageIndex = e.NewPageIndex
            Me.RegistroSolicitudes()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLista.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(0).Text = e.Row.RowIndex + 1
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvLista','Select$" & e.Row.RowIndex & "');")
                e.Row.Style.Add("cursor", "hand")


                '** 1       ADMINISTRADOR DEL SISTEMA
                '** 41      VICERECTOR ACADÉMICO 
                '** 116     VICERRECTORADO DE PROFESORES ->>CERVERA VALLEJOS MIRTHA FLOR
                'If (Me.Request.QueryString("ctf") = 41 Or Me.Request.QueryString("ctf") = 1) Then
                If (Me.Request.QueryString("ctf") = 116 Or Me.Request.QueryString("ctf") = 1 Or Me.Request.QueryString("ctf") = 94) Then
                    If (e.Row.Cells(5).Text.ToString.Trim = "A" Or e.Row.Cells(5).Text.ToString.Trim = "R") Then
                        Dim Cb As CheckBox
                        Cb = e.Row.FindControl("chkElegir")
                        Cb.Visible = True
                        Cb.Enabled = False
                        'e.Row.Cells(6).Text = "<center><img src='../../images/solicitudvacantes/sollocked.png' style='border:0px' alt='Solicitud Bloqueada'/></center>"
                        'e.Row.Cells(6).Text = "<center><img src='../../images/solicitudvacantes/sollocked.png' style='border:0px' alt='Solicitud Bloqueada'/></center>"
                    End If
                Else
                    'Perfiles solo para consultas.
                    Dim Cb As CheckBox
                    Cb = e.Row.FindControl("chkElegir")
                    Cb.Visible = True
                    Cb.Enabled = False
                End If


                '** Formateamos las columnas **'
                Select Case e.Row.Cells(5).Text.ToString.Trim
                    Case "P"    '** Solicitud Pendiente **
                        e.Row.Cells(5).Text = "<center><img src='../../images/solicitudvacantes/solPendiente.png' style='border:0px' alt='Solicitud Pendiente'/></center>"
                    Case "R"    '** Solicitud Rechazada**
                        e.Row.Cells(5).Text = "<center><img src='../../images/solicitudvacantes/solRechazado.png' style='border:0px' alt='Solicitud Rechazada'/></center>"
                    Case "A"    '** Solicitud Aprobada **
                        e.Row.Cells(5).Text = "<center><img src='../../images/solicitudvacantes/solArpobado.png' style='border:0px' alt='Solicitud Aprobada' /></center>"
                End Select
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub



    Protected Sub ddlCicloAcademico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCicloAcademico.SelectedIndexChanged
        Try
            RegistroSolicitudes()
            LimpiarFiltros()
            Me.ddlCicloAcademicoCarga.SelectedValue = Me.ddlCicloAcademico.SelectedValue
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        Try
            RegistroSolicitudes()
            LimpiarFiltros()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarFiltros()
        Try
            gvLista.SelectedIndex = -1
            Me.HiddenField.Value = 0
            MostrarDetalle()
            Menu1.Items(0).ImageUrl = "../../images/solicitudvacantes/TagButtons/btnDatos.png"
            Menu1.Items(1).ImageUrl = "../../images/solicitudvacantes/TagButtons/btnComentarios.png"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub ddlDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamento.SelectedIndexChanged
        Try
            RegistroSolicitudes()
            LimpiarFiltros()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvLista_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvLista.SelectedIndexChanged
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable

            Me.HiddenField.Value = gvLista.SelectedRow.Cells(1).Text
            'Me.lblIdsolicitud.Text = Me.HiddenField.Value
            'Me.lblIdsolicitud.Font.Bold = True
            'Me.lblIdsolicitud.ForeColor = Drawing.Color.Blue

            MostrarDetalle()
            Me.Menu1.Items(0).ImageUrl = "../../images/solicitudvacantes/TagButtons/btnDatosSel.png"    'Para apuntar a la pestaña inicial
            Me.Menu1.Items(1).ImageUrl = "../../images/solicitudvacantes/TagButtons/btnComentarios.png"
            MultiView1.ActiveViewIndex = 0  '** con esta linea asignamos a la vista por defecto que deseamos mostrar **'
            Me.txtComentario.Text = ""


            If Me.HiddenField.Value > 0 Then
                CargarDataList()        '** Cargamos todos los comentarios registrados **'
                CargarCargaAcademica()  '** Muestra la carga academica **'
                dts = obj.ConsultarSolictudVacante(HiddenField.Value)
                If dts.Rows.Count > 0 Then
                    Me.lblnombrevacante.Text = dts.Rows(0).Item("Docente").ToString
                    Me.lblDpto.Text = dts.Rows(0).Item("nombre_Dac").ToString
                    Me.lblDedicacion.Text = dts.Rows(0).Item("Descripcion_Ded").ToString
                    Me.lblCeco.Text = dts.Rows(0).Item("descripcion_Cco").ToString
                    Me.lblFechaFin.Text = dts.Rows(0).Item("intervalo_fechas").ToString
                    'Me.lblFechaInicio.Text = dts.Rows(0).Item("FechaIni_svac").ToString
                    Me.lblJustificacion.Text = dts.Rows(0).Item("Observacion").ToString

                    '-----------------------------------------------------------------------------------------
                    'Bloqueamos los comentarios, cuando la solicitud vacante se encuentre en estado #APROBADA
                    '** dguevara:11.11.2013 **
                    If dts.Rows(0).Item("EstadoRev_svac").ToString = "A" Then
                        Me.txtComentario.Enabled = False
                        Me.btnEnviar.Enabled = False
                        Me.lblmensajecomentarios.Text = "<b><font color='green'>Solictud Aprobada, Ud. no podrá registrar comentarios.</font></b>"
                    Else
                        Me.txtComentario.Enabled = True
                        Me.btnEnviar.Enabled = True
                    End If
                    '-----------------------------------------------------------------------------------------

                    If dts.Rows(0).Item("codigo_ded") = 3 Or dts.Rows(0).Item("codigo_ded") = 7 Then
                        Me.pnlMenor20.Visible = True
                        Me.pnlMayor20.Visible = False
                        'FormatCurrency(valor, 2)
                        Me.lblPrecioHora.Text = "<b><font color='red'>" & FormatCurrency(dts.Rows(0).Item("Salario2"), 2) & "</font></b>"
                        Me.lblHorasSemanales.Text = CInt(dts.Rows(0).Item("Numhoras_svac"))
                        Me.lblRemuneracion.Text = ""
                    Else
                        Me.pnlMenor20.Visible = False
                        Me.pnlMayor20.Visible = True

                        If dts.Rows(0).Item("Salario2").ToString.Contains("[") = True Then
                            Dim monto As String = FormatCurrency(CDbl(dts.Rows(0).Item("Salario2").ToString.Substring(0, dts.Rows(0).Item("Salario2").ToString.IndexOf("[").ToString.Trim)), 2).ToString
                            Dim ultimaremuneracion As String = dts.Rows(0).Item("Salario2").ToString.Substring(dts.Rows(0).Item("Salario2").ToString.IndexOf("["), dts.Rows(0).Item("Salario2").ToString.Length - dts.Rows(0).Item("Salario2").ToString.IndexOf("[")).ToString
                            'Response.Write(ultimaremuneracion)
                            Me.lblRemuneracion.Text = "<b><font color='red'>" & monto.ToString & " " & "<font color='green'>" & ultimaremuneracion.ToString & "</font>" & "</font></b>"
                        Else
                            Me.lblRemuneracion.Text = "<b><font color='red'>" & FormatCurrency(dts.Rows(0).Item("Salario2"), 2) & "</font></b>"
                        End If
                        Me.lblPrecioHora.Text = ""

                        Select Case dts.Rows(0).Item("codigo_ded")
                            Case 1
                                Me.lblHorasSemanales.Text = "40"
                            Case 2
                                Me.lblHorasSemanales.Text = "20"
                            Case Else
                                Me.lblHorasSemanales.Text = ""
                        End Select
                    End If
                End If

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    '** dguevara:06.11.2013 **'
    '** Importante para la creacion de los tabs **'
    Protected Sub Menu1_MenuItemClick(ByVal sender As Object, ByVal e As MenuEventArgs) Handles Menu1.MenuItemClick
        Dim i As Integer
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value)
        'Response.Write("menu" & Int32.Parse(e.Item.Value))

        'Make the selected menu item reflect the correct imageurl
        For i = 0 To Menu1.Items.Count - 1
            If i = e.Item.Value Then
                If i = 0 Then
                    Menu1.Items(i).ImageUrl = "../../images/solicitudvacantes/TagButtons/btnDatosSel.png"
                ElseIf i = 1 Then
                    Menu1.Items(i).ImageUrl = "../../images/solicitudvacantes/TagButtons/btnComentariosSel.png"
                ElseIf i = 2 Then
                    Menu1.Items(i).ImageUrl = "../../images/solicitudvacantes/TagButtons/btnCargaAcademicaSel.png"
                    'ElseIf i = 3 Then
                    '    Menu1.Items(i).ImageUrl = "~/Images/TagButtons/btnIvnEstadoSel.png"
                End If
            Else
                If i = 0 Then
                    'Menu1.Items(i).ImageUrl = "../../images/solicitudvacantes/TagButtons/btnDatosSel.png"
                    Menu1.Items(i).ImageUrl = "../../images/solicitudvacantes/TagButtons/btnDatos.png"
                ElseIf i = 1 Then
                    Menu1.Items(i).ImageUrl = "../../images/solicitudvacantes/TagButtons/btnComentarios.png"
                ElseIf i = 2 Then
                    Menu1.Items(i).ImageUrl = "../../images/solicitudvacantes/TagButtons/btnCargaAcademica.png"
                    'ElseIf i = 3 Then
                    '    Menu1.Items(i).ImageUrl = "~/Images/TagButtons/btnInvEstado.png"
                End If
            End If
        Next
    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Try
            LimpiarControles()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarComentario()
        Try
            Me.txtComentario.Text = ""
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarControles()
        Try
            RegistroSolicitudes()
            gvLista.SelectedIndex = -1
            ddlDepartamento.SelectedValue = -1
            ddlEstado.SelectedValue = "P"
            Me.HiddenField.Value = 0
            MostrarDetalle()
            Me.Menu1.Items(0).ImageUrl = "../../images/solicitudvacantes/TagButtons/btnDatos.png"    'Para apuntar a la pestaña inicial
            Me.Menu1.Items(1).ImageUrl = "../../images/solicitudvacantes/TagButtons/btnComentarios.png"    'Para apuntar a la pestaña inicial

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAprobar.Click
        Try
            If validarChecking() = True Then
                If CalificarSolictudVacante("A") > 0 Then
                    LimpiarControles()  'Aqui tb vuelve a consultar los registros
                    CargarCantidadEstados()
                    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Las solicitudes seleccionadas han sido Aprobadas.');", True)
                Else
                    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ocurrio un problema al tratar de Aprobar las solicitudes seleccionadas, favor de comunicarse con desarrollo de sistemas.');", True)
                    Exit Sub
                End If
            Else
                Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ud. debe seleccionar una o varias solicitudes para efectuar la calificación.');", True)
                Exit Sub
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EnviarEmail(ByVal tipo As String, ByVal codigo_svac As Integer)
        Try
            'Enviamos el correo solo al director del departamento, que es el mismo que ha registrado la solicitud vacante
            '** Solicitado esaavedra:::14.11.2013
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable
            Dim rpt As Integer = 0
            '------------------------------------------------------------------------------------------------------------
            Dim ObjMailNet As New ClsMail       '** Objeto para hacer el envio de correos:: dguevara 14.11.2013 **
            Dim mensaje As String = ""
            Dim para As String = ""
            '------------------------------------------------------------------------------------------------------------

            dts = obj.ConulstaEmail(codigo_svac, "E")
            If dts.Rows.Count > 0 Then
                para = "</br><font face='Courier'>" & "Estimado(a): <b>" & dts.Rows(0).Item("director").ToString & "</b>"

                If tipo = "A" Then
                    '::: APROBADO :::'
                    mensaje = "</br><P><ALIGN='justify'> Reciba un cordial saludo, en esta oportunidad le comunicamos que la siguiente solicitud de plaza vacante <b>ha sido APROBADA por Vicerrectorado de Profesores.</b>"
                    mensaje = mensaje & "Se precisa que el precio y/o remuneración propuesta será evaluada por la Administarción General, Dirección de Personal y Presupuesto, para la viabilidad de la misma.</P>"

                Else
                    '::: DESAPROBADO :::'
                    mensaje = "</br><P><ALIGN='justify'> Reciba un cordial saludo, en esta oportunidad le comunicamos que la siguiente solicitud de plaza vacante <b>ha sido RECHAZADA por Vicerrectorado Académico</b>:</br></P>"
                End If

                mensaje = mensaje & "<table border='1' bordercolor='black' style='width:100%'>"
                'row 1
                mensaje = mensaje & "<tr>"
                'campo 1:
                mensaje = mensaje & "<td bgcolor='#006699'>"
                mensaje = mensaje & "<font color='white' face='Courier'>Nombre Vacante: "
                mensaje = mensaje & "</td>"

                mensaje = mensaje & "<td>"
                mensaje = mensaje & "<font face='Courier'>" & dts.Rows(0).Item("docente").ToString & ""
                mensaje = mensaje & "</td>"
                'campo 2
                mensaje = mensaje & "<td bgcolor='#006699'>"
                mensaje = mensaje & "<font color='white' face='Courier'>Departamento Académico: "
                mensaje = mensaje & "</td>"
                mensaje = mensaje & "<td>"
                mensaje = mensaje & "<font face='Courier'>" & dts.Rows(0).Item("nombre_dac").ToString & ""
                mensaje = mensaje & "</td>"
                mensaje = mensaje & "</tr>"
                '******

                'row 2
                mensaje = mensaje & "<tr>"
                'campo 1
                mensaje = mensaje & "<td bgcolor='#006699'>"
                mensaje = mensaje & "<font color='white' face='Courier'>Dedicación: "
                mensaje = mensaje & "</td>"
                mensaje = mensaje & "<td>"
                mensaje = mensaje & "<font face='Courier'>" & dts.Rows(0).Item("Descripcion_Ded").ToString & ""
                mensaje = mensaje & "</td>"
                'campo 2
                mensaje = mensaje & "<td bgcolor='#006699'>"
                mensaje = mensaje & "<font color='white' face='Courier'>C.Costo: "
                mensaje = mensaje & "</td>"
                mensaje = mensaje & "<td>"
                mensaje = mensaje & "<font face='Courier'>" & dts.Rows(0).Item("descripcion_Cco").ToString & ""
                mensaje = mensaje & "</td>"
                mensaje = mensaje & "</tr>"
                '******

                'row 3
                mensaje = mensaje & "<tr>"
                'campo 1
                mensaje = mensaje & "<td bgcolor='#006699'>"
                mensaje = mensaje & "<font color='white' face='Courier'>Fecha Inicio: "
                mensaje = mensaje & "</td>"
                mensaje = mensaje & "<td>"
                mensaje = mensaje & "<font face='Courier'>" & dts.Rows(0).Item("FechaIni_svac").ToString & ""
                mensaje = mensaje & "</td>"
                'campo 2
                mensaje = mensaje & "<td bgcolor='#006699'>"
                mensaje = mensaje & "<font color='white' face='Courier'>Fecha Fin: "
                mensaje = mensaje & "</td>"
                mensaje = mensaje & "<td>"
                mensaje = mensaje & "<font face='Courier'>" & dts.Rows(0).Item("FechaFin_svac").ToString & ""
                mensaje = mensaje & "</td>"
                mensaje = mensaje & "</tr>"

                'row 4
                If (dts.Rows(0).Item("codigo_ded") = 3 Or dts.Rows(0).Item("codigo_ded") = 7) Then '<20 horas
                    mensaje = mensaje & "<tr>"
                    'campo 1
                    mensaje = mensaje & "<td bgcolor='#006699'>"
                    mensaje = mensaje & "<font color='white' face='Courier'>Horas Semanales: "
                    mensaje = mensaje & "</td>"
                    mensaje = mensaje & "<td>"
                    mensaje = mensaje & "<font face='Courier'>" & dts.Rows(0).Item("Numhoras_svac").ToString & ""
                    mensaje = mensaje & "</td>"
                    'campo 2
                    mensaje = mensaje & "<td bgcolor='#006699'>"
                    mensaje = mensaje & "<font color='white' face='Courier'>Precio Hora Propuesta: "
                    mensaje = mensaje & "</td>"
                    mensaje = mensaje & "<td>"
                    mensaje = mensaje & "<font face='Courier'>" & FormatCurrency(dts.Rows(0).Item("Salario"), 2).ToString & ""
                    mensaje = mensaje & "</td>"
                    mensaje = mensaje & "</tr>"
                Else
                    mensaje = mensaje & "<tr>"
                    'campo 1
                    mensaje = mensaje & "<td bgcolor='#006699'>"
                    mensaje = mensaje & "<font color='white' face='Courier'>Remuneración Propuesta: "
                    mensaje = mensaje & "</td>"
                    mensaje = mensaje & "<td>"
                    mensaje = mensaje & "<font face='Courier'>" & FormatCurrency(dts.Rows(0).Item("Salario"), 2).ToString & ""
                    mensaje = mensaje & "</td>"
                    mensaje = mensaje & "</tr>"
                End If

                'row 5
                mensaje = mensaje & "<tr>"
                'campo 1
                mensaje = mensaje & "<td bgcolor='#006699'>"
                mensaje = mensaje & "<font color='white' face='Courier'>Justificacipón: "
                mensaje = mensaje & "</td>"
                'campo 2
                mensaje = mensaje & "<td colspan='3'>"
                mensaje = mensaje & "<font face='Courier'>" & dts.Rows(0).Item("Observacion").ToString & ""
                mensaje = mensaje & "</td>"
                mensaje = mensaje & "</tr>"

                '**Fin tabla
                mensaje = mensaje & "</table>"
                mensaje = mensaje & "<font face='Courier'></br> Atte.<br><br>Campus Virtual - USAT.</font>"

                '':::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual - USAT", dts.Rows(0).Item("email"), "Registro Solictud Vacantes", para & mensaje, True)
                '':::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

                '':::: Pruebas :::'
                'Response.Write(para)
                'Response.Write("<br>")
                'Response.Write(mensaje)

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub btnRechazar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRechazar.Click
        Try
            If validarChecking() = True Then
                If CalificarSolictudVacante("R") > 0 Then
                    LimpiarControles()  'Aqui tb vuelve a consultar los registros
                    CargarCantidadEstados()
                    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Las solicitudes seleccionadas han sido Rechazadas.');", True)
                Else
                    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ocurrio un problema al tratar de rechazar las solicitudes seleccionadas, favor de comunicarse con desarrollo de sistemas.');", True)
                    Exit Sub
                End If
            Else
                Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ud. debe seleccionar una o varias solicitudes para efectuar la calificación.');", True)
                Exit Sub
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function CalificarSolictudVacante(ByVal tipo As String) As Integer
        Dim obj As New clsSolicitudVacante
        Dim rpt As Integer = 0
        Dim Fila As GridViewRow
        For i As Integer = 0 To gvLista.Rows.Count - 1
            Fila = gvLista.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If valor = True Then
                rpt = obj.CalificarSolicitud(tipo, Me.gvLista.Rows(i).Cells(1).Text, Session("id_per"))
                ':::::: ENVIO DE CORRREOS :::::'
                EnviarEmail(tipo, Me.gvLista.Rows(i).Cells(1).Text)
            End If
        Next
        Return rpt
    End Function

    Private Function validarChecking() As Boolean
        Dim sw As Boolean = False
        Dim Fila As GridViewRow

        For i As Integer = 0 To gvLista.Rows.Count - 1
            Fila = gvLista.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If valor = True Then
                sw = True
            End If
        Next
        If sw = True Then
            Return True
        End If
        Return False
    End Function

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        ':::: desarrollado x dguevara :::::'
        'Solicitud: rtimana - esaavedra. 
        'Se pidio que cuando el perfil del Admin General comente, se le envie un email al perfil de Rolando Timana -> Asistente Personal.
        '=======================================================================================================================================

        '** Registramos los comentarios **'
        Try
            Dim obj As New clsSolicitudVacante
            Dim rpt As Integer

            If Me.txtComentario.Text.Trim.Length = 0 Then
                Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Favor ingrese un comentario para la solicitud vacante seleccionada.');", True)
                Exit Sub
            End If

            'Response.Write(Me.gvLista.SelectedRow.Cells(1).Text)
            rpt = obj.RegistrarComentario(Me.txtComentario.Text, Session("id_per"), Me.gvLista.SelectedRow.Cells(1).Text)
            If rpt > 0 Then
                '(35)ADMINISTRADOR GENERAL: Si el perfil que se ha ingresado es el Administrador General, se envia la notificación del comentario a rtimana.
                If Me.Request.QueryString("ctf") = 35 Then
                    NotificacionEmailComentario(Me.gvLista.SelectedRow.Cells(1).Text, rpt)
                End If

                Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('El comentario se ha registrado correctamente.');", True)
                LimpiarComentario() '** limpia el txtcomentario
                CargarDataList()    '** consulta los comentarios de la solicitud seleccionada.
                CargarCargaAcademica()
                CargarCantidadEstados()
            Else
                Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ocurrio un error al tratar de registrar el comentario, favor de comunicarse con desarrollo de sistemas.')", True)
                Exit Sub
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    ':: La notificación se envia a todos los usuarios que tengan el perfil de ASISTENTE DE PERSONAL ::'
    ':: Agregado el 22.11.2013 ::
    Private Sub NotificacionEmailComentario(ByVal codigo_svac As Integer, ByVal codigo_csv As Integer)
        Try
            '(94)	ASISTENTE DE PERSONAL : TIMANA VASQUEZ ROLANDO GILBERTO - LLUNCOR IZASIGA JANETH EVELIN - DIAZ CUBAS LILIANA MARLENI
            'Para este caso en concreto solo se enviara la notificación a Timana y LLuncor.
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable
            Dim dtsSol As New Data.DataTable
            Dim dtsCsv As New Data.DataTable    'Para almacenar el comentario registrado.
            '------------------------------------------------------------------------------------------------------------
            Dim ObjMailNet As New ClsMail       '** Objeto para hacer el envio de correos:: dguevara 14.11.2013 **
            Dim mensaje As String = ""
            Dim para As String = ""
            '------------------------------------------------------------------------------------------------------------

            dts = obj.ConsultaMiembrosPerfil("AP", 94)
            If dts.Rows.Count > 0 Then
                'Consulto la data de la solicitud
                dtsSol = obj.ConulstaEmail(codigo_svac, "E")
                dtsCsv = obj.ConsultarComentariosRegistrados("C", codigo_csv)
                For i As Integer = 0 To dts.Rows.Count - 1
                    If dts.Rows(i).Item("codigo_per") <> 506 Then       ':: 506 -> ldias
                        'Destinatario:
                        para = "</br><font face='Courier'>" & "Estimado(a): <b>" & dts.Rows(i).Item("nombrepersonal").ToString & "</b>"

                        mensaje = "</br><P><ALIGN='justify'> Reciba un cordial saludo, en esta oportunidad le comunicamos que el  Administrador General ha hecho el siguiente comentario a la solicitud de plaza vacante</br></P>"

                        mensaje = mensaje & "<table border='1' bordercolor='black' style='width:100%'>"
                        'row 1
                        mensaje = mensaje & "<tr>"
                        'campo 1:
                        mensaje = mensaje & "<td bgcolor='#006699'>"
                        mensaje = mensaje & "<font color='white' face='Courier'>Nombre Vacante: "
                        mensaje = mensaje & "</td>"

                        mensaje = mensaje & "<td>"
                        mensaje = mensaje & "<font face='Courier'>" & dtsSol.Rows(0).Item("docente").ToString & ""
                        mensaje = mensaje & "</td>"
                        'campo 2
                        mensaje = mensaje & "<td bgcolor='#006699'>"
                        mensaje = mensaje & "<font color='white' face='Courier'>Departamento Académico: "
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td>"
                        mensaje = mensaje & "<font face='Courier'>" & dtsSol.Rows(0).Item("nombre_dac").ToString & ""
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"
                        '******

                        'row 2
                        mensaje = mensaje & "<tr>"
                        'campo 1
                        mensaje = mensaje & "<td bgcolor='#006699'>"
                        mensaje = mensaje & "<font color='white' face='Courier'>Dedicación: "
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td>"
                        mensaje = mensaje & "<font face='Courier'>" & dtsSol.Rows(0).Item("Descripcion_Ded").ToString & ""
                        mensaje = mensaje & "</td>"
                        'campo 2
                        mensaje = mensaje & "<td bgcolor='#006699'>"
                        mensaje = mensaje & "<font color='white' face='Courier'>C.Costo: "
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td>"
                        mensaje = mensaje & "<font face='Courier'>" & dtsSol.Rows(0).Item("descripcion_Cco").ToString & ""
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"
                        '******

                        'row 3
                        mensaje = mensaje & "<tr>"
                        'campo 1
                        mensaje = mensaje & "<td bgcolor='#006699'>"
                        mensaje = mensaje & "<font color='white' face='Courier'>Fecha Inicio: "
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td>"
                        mensaje = mensaje & "<font face='Courier'>" & dtsSol.Rows(0).Item("FechaIni_svac").ToString & ""
                        mensaje = mensaje & "</td>"
                        'campo 2
                        mensaje = mensaje & "<td bgcolor='#006699'>"
                        mensaje = mensaje & "<font color='white' face='Courier'>Fecha Fin: "
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td>"
                        mensaje = mensaje & "<font face='Courier'>" & dtsSol.Rows(0).Item("FechaFin_svac").ToString & ""
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"

                        'row 4
                        If (dtsSol.Rows(0).Item("codigo_ded") = 3 Or dtsSol.Rows(0).Item("codigo_ded") = 7) Then '<20 horas
                            mensaje = mensaje & "<tr>"
                            'campo 1
                            mensaje = mensaje & "<td bgcolor='#006699'>"
                            mensaje = mensaje & "<font color='white' face='Courier'>Horas Semanales: "
                            mensaje = mensaje & "</td>"
                            mensaje = mensaje & "<td>"
                            mensaje = mensaje & "<font face='Courier'>" & dtsSol.Rows(0).Item("Numhoras_svac").ToString & ""
                            mensaje = mensaje & "</td>"
                            'campo 2
                            mensaje = mensaje & "<td bgcolor='#006699'>"
                            mensaje = mensaje & "<font color='white' face='Courier'>Precio Hora Propuesta: "
                            mensaje = mensaje & "</td>"
                            mensaje = mensaje & "<td>"
                            mensaje = mensaje & "<font face='Courier'>" & FormatCurrency(dtsSol.Rows(0).Item("Salario"), 2).ToString & ""
                            mensaje = mensaje & "</td>"
                            mensaje = mensaje & "</tr>"
                        Else
                            mensaje = mensaje & "<tr>"
                            'campo 1
                            mensaje = mensaje & "<td bgcolor='#006699'>"
                            mensaje = mensaje & "<font color='white' face='Courier'>Remuneración Propuesta: "
                            mensaje = mensaje & "</td>"
                            mensaje = mensaje & "<td>"
                            mensaje = mensaje & "<font face='Courier'>" & FormatCurrency(dtsSol.Rows(0).Item("Salario"), 2).ToString & ""
                            mensaje = mensaje & "</td>"
                            mensaje = mensaje & "</tr>"
                        End If

                        'row 5
                        mensaje = mensaje & "<tr>"
                        'campo 1
                        mensaje = mensaje & "<td bgcolor='#006699'>"
                        mensaje = mensaje & "<font color='white' face='Courier'>Justificacipón: "
                        mensaje = mensaje & "</td>"
                        'campo 2
                        mensaje = mensaje & "<td colspan='3'>"
                        mensaje = mensaje & "<font face='Courier'>" & dtsSol.Rows(0).Item("Observacion").ToString & ""
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"


                        'Agregamos una fila mas, para el comentario registrado.
                        mensaje = mensaje & "<tr>"
                        'campo 1
                        mensaje = mensaje & "<td bgcolor='#006699'>"
                        mensaje = mensaje & "<font color='white' face='Courier'>Comentario Registrado: "
                        mensaje = mensaje & "</td>"
                        'campo 2
                        mensaje = mensaje & "<td colspan='3'>"
                        mensaje = mensaje & "<font face='Courier'>" & dtsCsv.Rows(0).Item("Mensaje_csv").ToString & " [Fecha Registro: " & dtsCsv.Rows(0).Item("fechareg_csv").ToString & "]"
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"

                        '**Fin tabla
                        mensaje = mensaje & "</table>"
                        mensaje = mensaje & "<font face='Courier'></br> Atte.<br><br>Campus Virtual - USAT.</font>"

                        '':::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
                        ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual - USAT", dts.Rows(0).Item("email"), "Registro Solictud Vacantes", para & mensaje, True)
                        '':::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

                        '':::: Pruebas :::'
                        'Response.Write(para)
                        'Response.Write("<br>")
                        'Response.Write(mensaje)

                    End If
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnExportarExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportarExcel.Click
        '** dguevara: 12.11.2013 **'
        '** Código para hacer una exportación de datos a partir de una consulta, con un gridview dinamico **'
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable
            dts = obj.ListaRegistroSolicitudesVacantes(Me.ddlCicloAcademico.SelectedValue, Me.ddlEstado.SelectedValue, Me.ddlDepartamento.SelectedValue, 0, "X", Session("id_per"))
            If dts.Rows.Count > 0 Then

                Dim gvExportar As New GridView
                gvExportar.AllowPaging = False
                gvExportar.DataSource = dts
                gvExportar.DataBind()

                Response.Clear()
                Response.Buffer = True
                Response.AddHeader("content-disposition", "attachment;filename=ListaSolicitudesVacantes.xls")
                Response.Charset = ""
                Response.ContentType = "application/vnd.ms-excel"

                Dim sw As New StringWriter()
                Dim hw As New HtmlTextWriter(sw)

                '**# Formateamos la Cabecera **# ::: dguevara :::
                '---------------------------------------------------------------------
                gvExportar.HeaderRow.Cells(0).Style.Add("height", "35px")
                For i As Integer = 0 To dts.Columns.Count - 1
                    gvExportar.HeaderRow.Cells(i).Style.Add("background-color", "#7F0076")
                    gvExportar.HeaderRow.Cells(i).Style.Add("width", "auto")
                    gvExportar.HeaderRow.ForeColor = Drawing.Color.White

                Next
                '** Otra forma de hacerlo **' :::: dguevara
                'gvExportar.HeaderRow.BackColor = Drawing.Color.White
                'For Each cell As TableCell In gvExportar.HeaderRow.Cells
                '    cell.BackColor = gvExportar.HeaderStyle.BackColor
                'Next

                '---------------------------------------------------------------------
                '** Colores para las filas alternas **'
                For Each row As GridViewRow In gvExportar.Rows
                    'row.BackColor = Drawing.Color.Red
                    For Each cell As TableCell In row.Cells
                        '*Alternamos el colore de las filas.
                        If row.RowIndex Mod 2 = 0 Then
                            cell.BackColor = Drawing.Color.White
                        Else
                            cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFDD8")
                        End If
                        cell.CssClass = "textmode"
                    Next
                Next
                '---------------------------------------------------------------------

                gvExportar.RenderControl(hw)
                Dim style As String = "<style> .textmode{mso-number-format:\@;}</style>"
                Response.Write(style)
                Response.Output.Write(sw.ToString())
                Response.Flush()
                Response.End()

            Else
                Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('No se encontraron datos para Exportar.');", True)
                Exit Sub
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub grwCargaAcademica_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwCargaAcademica.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                Dim CeldasCombinadas As Int16 = 1
                fila = e.Row.DataItem
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this)")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this)")

                contador = contador + 1
                'Combinar celdas
                If codigo_per = fila("codigo_per") Then
                    e.Row.Cells(0).Text = ""
                    e.Row.Cells(1).Text = ""
                    'e.Row.Cells(2).Text = ""
                    contador = contador - 1
                    'e.Row.Cells(0).RowSpan = contador
                Else
                    e.Row.Cells(0).CssClass = "bordesup" 'Autor
                    e.Row.Cells(1).CssClass = "bordesup" 'Autor
                    'e.Row.Cells(2).CssClass = "bordesup" 'Autor
                    e.Row.VerticalAlign = VerticalAlign.Middle
                    codigo_per = fila("codigo_per").ToString()
                    PrimeraFila = e.Row.RowIndex
                    'e.Row.Cells(0).Text = contador
                End If

                ''Asignar linea separadora
                e.Row.Cells(2).CssClass = "bordesup" 'Autor
                e.Row.Cells(3).CssClass = "bordesup" 'Autor
                e.Row.Cells(4).CssClass = "bordesup" 'Estado
                e.Row.Cells(4).CssClass = "bordesup"
                e.Row.Cells(5).CssClass = "bordesup"
                e.Row.Cells(6).CssClass = "bordesup"
                e.Row.Cells(7).CssClass = "bordesup"
                e.Row.Cells(8).CssClass = "bordesup"
                e.Row.Cells(9).CssClass = "bordesup"
                e.Row.Cells(10).CssClass = "bordesup"


            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlCicloAcademicoCarga_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCicloAcademicoCarga.SelectedIndexChanged
        Try
            CargarCargaAcademica()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class

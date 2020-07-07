﻿
Partial Class personal_frmSolicitudesVacantes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If

        Try
            '** validacion javascript: numero decimales **'
            '** El numero de horas es validado solo para numeros enteros ** solicitado rtimana: 30.10.2013
            Me.txthoras.Attributes.Add("onkeypress", "javascript:return ValidaSoloNumeros(event);")
            Me.txtpreciohora.Attributes.Add("onkeypress", "javascript:return ValidNum(event);")
            Me.txtRemuneración.Attributes.Add("onkeypress", "javascript:return ValidNum(event);")

            If Not IsPostBack Then
                If ValidarPermisos() > 0 Then
                    CargaCicloAcademicos()
                    CargarDepartamentoAcademico()
                    'CargarCecos()                  '** Estos dependen del departamento **'
                    CargarDedicacion()
                    CargarTipoPersona()
                    RegistroSolicitudes()           '** Listo todos los registros de vacantes **'
                    Me.lblMensaje.Visible = False
                    Me.lblMensaje.Text = ""
                    EstadosControles(True)
                Else
                    Me.lblMensaje.Visible = True
                    Me.lblMensaje.Text = "<font color='red'><b><u><br>Mensaje:</u></b></font><br><br>Ud. no tiene los permisos necesarios para hacer el registro de solicitud de vacantes. Favor de comunicarse con <font color='blue'><b>desarrollo de sistemas</b></font> y solicitar los accesos.<br><br>"
                    EstadosControles(False)
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        
    End Sub

    Private Sub EstadosControles(ByVal estado As Boolean)
        Try
            Me.ddlDepartamento.Enabled = estado
            Me.ddlTipoVacante.Enabled = estado
            Me.ddlDedicacion.Enabled = estado
            Me.ddlCicloAcademico.Enabled = estado
            Me.txtFechaFin.Enabled = estado
            Me.txtFechaInicio.Enabled = estado
            Me.txtObservacion.Enabled = estado
            Me.txtformacion.Enabled = estado
            Me.ddlCeco.Enabled = estado
            Me.btnSoliciar.Enabled = estado
            Me.btnLimpiar.Enabled = estado
            Me.btnFechaInicio.Disabled = Not estado
            Me.btnFechaFin.Disabled = Not estado
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Function ValidarPermisos() As Integer
        Try
            '** Este método permite validar los permisos del usuario que accede **'
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable
            dts = obj.ConsultaAccesos(Me.Session("id_per"))
            Return dts.Rows.Count
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub CargarTipoPersona()
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable

            dts = obj.ListaTipoPersona("C")
            If dts.Rows.Count > 0 Then
                ddlBusTipoPersona.DataSource = dts
                ddlBusTipoPersona.DataTextField = "descripcion"
                ddlBusTipoPersona.DataValueField = "codigo"
                ddlBusTipoPersona.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarDedicacion()
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable

            dts = obj.ListaDedicaciones("R")
            If dts.Rows.Count > 0 Then
                Me.ddlDedicacion.DataSource = dts
                Me.ddlDedicacion.DataTextField = "descripcion"
                Me.ddlDedicacion.DataValueField = "Codigo"
                Me.ddlDedicacion.DataBind()
            End If

            dts = obj.ListaDedicaciones("C")
            If dts.Rows.Count > 0 Then
                Me.ddlBusDedicacion.DataSource = dts
                Me.ddlBusDedicacion.DataTextField = "descripcion"
                Me.ddlBusDedicacion.DataValueField = "Codigo"
                Me.ddlBusDedicacion.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarCecos()
        Try
            '** Lista los cecos de los departamentos y escuelas **
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable

            'Response.Write("dpto: " & Me.ddlDepartamento.SelectedValue)
            dts = obj.ListaCecosEscuelaDpto(Me.ddlDepartamento.SelectedValue)
            'Response.Write("<br>")
            'Response.Write(dts.Rows.Count)
            If dts.Rows.Count > 0 Then
                Me.ddlCeco.DataSource = dts
                Me.ddlCeco.DataTextField = "nombrececo"
                Me.ddlCeco.DataValueField = "codigo"
                Me.ddlCeco.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarDepartamentoAcademico()
        '** dguevara 24.10.2013 **
        '** Módulo de Permisos  **
        '** Estos departamentos cargan según el los permisos que tiene el usuario, segun departamento academico **
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable

            dts = obj.ListaDepartamentosAcademicos("S", Session("id_per"))
            If dts.Rows.Count > 1 Then
                Me.ddlDepartamento.DataSource = dts
                Me.ddlDepartamento.DataTextField = "descripcion_dac"
                Me.ddlDepartamento.DataValueField = "Codigo_cac"
                Me.ddlDepartamento.DataBind()
                Me.lblMensaje.Visible = False
                Me.lblMensaje.Text = ""
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
                Me.ddlCicloAcademico.DataSource = dts
                Me.ddlCicloAcademico.DataTextField = "descripcion_Cac"
                Me.ddlCicloAcademico.DataValueField = "codigo_Cac"
                Me.ddlCicloAcademico.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlDedicacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDedicacion.SelectedIndexChanged
        Try
            'Response.Write("Dedicacion: " & Me.ddlDedicacion.SelectedValue)
            If Me.ddlDedicacion.SelectedValue <> -1 Then
                If Me.ddlDedicacion.SelectedValue = 3 Or Me.ddlDedicacion.SelectedValue = 7 Then  'Dedicacion: TIEMPO PARCIAL < 20 HRS.
                    Me.pnlmenor20.Visible = False
                    Me.pnlOtros.Visible = True
                    Me.txthoras.Focus()
                Else
                    Me.pnlmenor20.Visible = True
                    Me.pnlOtros.Visible = False
                    Me.txtRemuneración.Focus()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function SolicitarNuevaVacante() As Integer
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable
            Dim salarioHoras As String
            Dim salarioRemun As String
            Dim numhoras As Double
            Dim rpt As Integer


            If Validacion() = True Then
                '** dguevara 25.10.2013 **
                '--** solicion : desde el cliente ** --
                '--** el encriptar salario se hizo por aca debido a que por el sql no encontraba la forma como hacerlo salia error dandole los parámetros ** y mas naa!!--
                If Me.ddlDedicacion.SelectedValue = 3 Or Me.ddlDedicacion.SelectedValue = 7 Then
                    'salarioHoras = obj.encriptarSalario(CDbl(Me.txthoras.Text.Trim))   'Error de mela
                    salarioHoras = obj.encriptarSalario(CDbl(Me.txtpreciohora.Text.Trim))
                    numhoras = CDbl(txthoras.Text)
                    salarioRemun = ""
                Else
                    salarioRemun = obj.encriptarSalario(CDbl(txtRemuneración.Text.Trim))
                    numhoras = 0
                    salarioHoras = ""
                End If

                '-----------------------------------------------------------
                'Response.Write("Codigo_cac: " & Me.ddlCicloAcademico.SelectedValue)
                'Response.Write("<br>")
                'Response.Write("ddlDepartamento: " & Me.ddlDepartamento.SelectedValue)
                'Response.Write("<br>")
                'Response.Write("ddlCeco: " & Me.ddlCeco.SelectedValue)
                'Response.Write("<br>")
                'Response.Write("ddlDedicacion: " & Me.ddlDedicacion.SelectedValue)
                'Response.Write("<br>")
                'Response.Write("salarioRemun: " & salarioRemun)
                'Response.Write("<br>")
                'Response.Write("numhoras: " & numhoras)
                'Response.Write("<br>")
                'Response.Write("salarioHoras: " & salarioHoras)
                'Response.Write("<br>")
                'Response.Write("codigo_per: " & Me.Request.QueryString("id"))
                'Response.Write("<br>")
                'Response.Write("tipo: " & "N")
                'Response.Write("<br>")
                'Response.Write("fecIni: " & txtFechaInicio.Text)
                'Response.Write("<br>")
                'Response.Write("fecFin: " & txtFechaFin.Text)
                'Response.Write("<br>")
                'Response.Write("txtObservacion: " & txtObservacion.Text)

                '==========================================================================================
                '--** Proceso para agregar el registro----------------------:
                rpt = obj.AgregarSolicitudVacante(Me.ddlCicloAcademico.SelectedValue, _
                                                Me.ddlDepartamento.SelectedValue, _
                                                Me.ddlCeco.SelectedValue, _
                                                Me.ddlDedicacion.SelectedValue, _
                                                salarioRemun, _
                                                numhoras, _
                                                salarioHoras, _
                                                Session("id_per"), "N", _
                                                txtFechaInicio.Text, txtFechaFin.Text, _
                                                txtObservacion.Text.Trim, Me.txtformacion.Text.Trim)

                'If rpt > 0 Then
                '    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('La solicitud fue registrada correctamente.')", True)
                'Else
                '    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan datos", "alert('Ocurrio un error al tratar de guardar la solicitud, favor de volver a interntarlo o comuniquese con Desarrollo de Sistemas - USAT.');", True)
                'End If

                '** Listamos las solicitudes registradas **.
                RegistroSolicitudes()
                '** Limpiamos los controles al hacer el registro.
                LimpiarControles()
                '==========================================================================================
            End If

            Return rpt
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Function SolicitaVacanteExistente() As Integer
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable
            Dim rpt As Integer
            Dim salarioHoras As String
            Dim salarioRemun As String
            Dim numhoras As Double

            If Validacion() = True Then
                If Me.HiddenField.Value <> 0 Then
                    '** dguevara 31.10.2013 **
                    If Me.ddlDedicacion.SelectedValue = 3 Or Me.ddlDedicacion.SelectedValue = 7 Then
                        'salarioHoras = obj.encriptarSalario(CDbl(Me.txthoras.Text.Trim))   'Error de mela
                        salarioHoras = obj.encriptarSalario(CDbl(Me.txtpreciohora.Text.Trim))
                        numhoras = CDbl(txthoras.Text)
                        salarioRemun = ""
                    Else
                        salarioRemun = obj.encriptarSalario(CDbl(txtRemuneración.Text.Trim))
                        numhoras = 0
                        salarioHoras = ""
                    End If

                    '** dguevara: 31.10.2013 **
                    'Para el registro de la vacante existente, solo hay que guardar los datos en la tablas #SolicitudVacante y #EvaluacionSolictudVacante
                    'Esto debido a que no debemos modificar los datos de PERSONA,PERSONAL,DATOSPERSONAL ** solicitado por rtimana **

                    '--** Proceso para agregar el registro----------------------:
                    rpt = obj.AgregarSolicitudVacanteExistente(Me.ddlCicloAcademico.SelectedValue, _
                                                    Me.ddlDepartamento.SelectedValue, _
                                                    Me.ddlCeco.SelectedValue, _
                                                    Me.ddlDedicacion.SelectedValue, _
                                                    salarioRemun, _
                                                    numhoras, _
                                                    salarioHoras, _
                                                    Session("id_per"), "E", _
                                                    txtFechaInicio.Text, txtFechaFin.Text, _
                                                    txtObservacion.Text.Trim, _
                                                    Me.HiddenField.Value, Me.txtformacion.Text.Trim)

                    'If rpt > 0 Then
                    '    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('La solicitud fue registrada correctamente.')", True)
                    'Else
                    '    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan datos", "alert('Ocurrio un error al tratar de guardar la solicitud, favor de volver a interntarlo o comuniquese con Desarrollo de Sistemas - USAT.');", True)
                    'End If


                    '** Listamos las solicitudes registradas **.
                    RegistroSolicitudes()
                    '** Limpiamos los controles al hacer el registro.
                    LimpiarControles()

                Else
                    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('No se ha seleccionado el trabajador, favor de buscar la vacante.');", True)
                    Exit Function
                End If
            End If

            Return rpt
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub ModificarVacante()
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable
            Dim salarioHoras As String
            Dim salarioRemun As String
            Dim numhoras As Double
            Dim rpt As Integer

            If Validacion() = True Then
                '--** el encriptar salario se hizo por aca debido a que por el sql no encontraba la forma como hacerlo salia error dandole los parámetros ** y mas naa!!--
                If Me.ddlDedicacion.SelectedValue = 3 Or Me.ddlDedicacion.SelectedValue = 7 Then
                    'salarioHoras = obj.encriptarSalario(CDbl(Me.txthoras.Text.Trim))   'Error de mela
                    salarioHoras = obj.encriptarSalario(CDbl(Me.txtpreciohora.Text.Trim))
                    numhoras = CDbl(txthoras.Text)
                    salarioRemun = ""
                Else
                    salarioRemun = obj.encriptarSalario(CDbl(txtRemuneración.Text.Trim))
                    numhoras = 0
                    salarioHoras = ""
                End If

                'Valores:
                'Response.Write("<br>")
                'Response.Write("codigo_svac " & CInt(Me.lblCodigo_svac.Text))
                'Response.Write("<br>")
                'Response.Write("Ciclo " & Me.ddlCicloAcademico.SelectedValue)
                'Response.Write("<br>")
                'Response.Write("Dpo " & Me.ddlDepartamento.SelectedValue)
                'Response.Write("<br>")
                'Response.Write("ceco " & Me.ddlCeco.SelectedValue)
                'Response.Write("<br>")
                'Response.Write("Dedicacion " & Me.ddlDedicacion.SelectedValue)
                'Response.Write("<br>")
                'Response.Write("Salario " & salarioRemun.ToString)
                'Response.Write("<br>")
                'Response.Write("numhoras " & numhoras)
                'Response.Write("<br>")
                'Response.Write("salarioHoras: " & salarioHoras)
                'Response.Write("<br>")
                'Response.Write("id" & Me.Request.QueryString("id"))
                'Response.Write("<br>")
                'Response.Write("tipo" & ddlTipoVacante.SelectedValue)
                'Response.Write("<br>")
                'Response.Write("f.i" & txtFechaInicio.Text)
                'Response.Write("<br>")
                'Response.Write("f.f" & txtFechaFin.Text)
                'Response.Write("<br>")
                'Response.Write("obs" & txtObservacion.Text.Trim)
                'Response.Write("<br>")
                'Response.Write("dhiden" & Me.HiddenField.Value)


                ''''''--** Proceso para agregar el registro----------------------:
                rpt = obj.ModificarSolicitudVacante(CInt(Me.lblCodigo_svac.Text), Me.ddlCicloAcademico.SelectedValue, _
                                                Me.ddlDepartamento.SelectedValue, _
                                                Me.ddlCeco.SelectedValue, _
                                                Me.ddlDedicacion.SelectedValue, _
                                                salarioRemun, _
                                                numhoras, _
                                                salarioHoras, _
                                                Session("id_per"), _
                                                Me.ddlTipoVacante.SelectedValue, _
                                                txtFechaInicio.Text, txtFechaFin.Text, _
                                                txtObservacion.Text.Trim, Me.HiddenField.Value, Me.txtformacion.Text.Trim)

                If rpt > 0 Then
                    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('La solicitud fue actualizada correctamente.')", True)
                Else
                    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan datos", "alert('Ocurrio un error al tratar de actualizar la solicitud, favor de volver a interntarlo o comuniquese con Desarrollo de Sistemas - USAT.');", True)
                End If

                '** Listamos las solicitudes registradas **.
                RegistroSolicitudes()
                '** Limpiamos los controles al hacer el registro.
                LimpiarControles()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnSoliciar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSoliciar.Click
        Try
            Dim rpt As Integer
            'Response.Write("hiiden: " & HiddenField.Value)
            If Me.lblCodigo_svac.Text = "" Then     'Registra una nueva vacante, sea de tipo NUEVO o EXISTENTE.
                If Me.HiddenField.Value = 0 Then
                    rpt = SolicitarNuevaVacante()
                Else
                    rpt = SolicitaVacanteExistente()
                End If
                '** Mostramos el alerta **'
                If rpt > 0 Then
                    '** -------------------------------------------------------------- **'
                    '   Envia los email a los revisores, Campana - Timana - Lavado 
                    EnvioCorreo(rpt)
                    '** -------------------------------------------------------------- **'
                    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('La solicitud fue registrada correctamente.')", True)
                    'Else
                    'Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan datos", "alert('Ocurrio un error al tratar de guardar la solicitud, favor de volver a interntarlo o comuniquese con Desarrollo de Sistemas - USAT.');", True)
                End If
            Else
                'Response.Write("Modificar")
                ModificarVacante()
                btnLimpiar_Click(sender, e)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EnvioCorreo(ByVal codigo_svac As Integer)
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable
            '------------------------------------------------------------------------------------------------------------
            Dim ObjMailNet As New ClsMail       '** Objeto para hacer el envio de correos:: dguevara 14.11.2013 **
            Dim mensaje As String = ""
            Dim para As String = ""
            '------------------------------------------------------------------------------------------------------------
            dts = obj.ConulstaEmail(codigo_svac, "S")
            If dts.Rows.Count > 0 Then
                For i As Integer = 0 To dts.Rows.Count - 1
                    para = "</br><font face='Courier'>" & "Estimado(a): <b>" & dts.Rows(i).Item("revisor").ToString & "</b>"
                    mensaje = "</br><P><ALIGN='justify'> Reciba un cordial saludo, en esta oportunidad le comunicamos que se ha registrado una solicitud vacante con los siguientes datos:</br></P>"
                    mensaje = mensaje & "<table border='1' bordercolor='black' style='width:100%'>"
                    'row 1
                    mensaje = mensaje & "<tr>"
                    'campo 1:
                    mensaje = mensaje & "<td bgcolor='#006699'>"
                    mensaje = mensaje & "<font color='white' face='Courier'>Nombre Vacante: "
                    mensaje = mensaje & "</td>"

                    mensaje = mensaje & "<td>"
                    mensaje = mensaje & "<font face='Courier'>" & dts.Rows(i).Item("docente").ToString & ""
                    mensaje = mensaje & "</td>"
                    'campo 2
                    mensaje = mensaje & "<td bgcolor='#006699'>"
                    mensaje = mensaje & "<font color='white' face='Courier'>Departamento Académico: "
                    mensaje = mensaje & "</td>"
                    mensaje = mensaje & "<td>"
                    mensaje = mensaje & "<font face='Courier'>" & dts.Rows(i).Item("nombre_dac").ToString & ""
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
                    mensaje = mensaje & "<font face='Courier'>" & dts.Rows(i).Item("Descripcion_Ded").ToString & ""
                    mensaje = mensaje & "</td>"
                    'campo 2
                    mensaje = mensaje & "<td bgcolor='#006699'>"
                    mensaje = mensaje & "<font color='white' face='Courier'>C.Costo: "
                    mensaje = mensaje & "</td>"
                    mensaje = mensaje & "<td>"
                    mensaje = mensaje & "<font face='Courier'>" & dts.Rows(i).Item("descripcion_Cco").ToString & ""
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
                    mensaje = mensaje & "<font face='Courier'>" & dts.Rows(i).Item("FechaIni_svac").ToString & ""
                    mensaje = mensaje & "</td>"
                    'campo 2
                    mensaje = mensaje & "<td bgcolor='#006699'>"
                    mensaje = mensaje & "<font color='white' face='Courier'>Fecha Fin: "
                    mensaje = mensaje & "</td>"
                    mensaje = mensaje & "<td>"
                    mensaje = mensaje & "<font face='Courier'>" & dts.Rows(i).Item("FechaFin_svac").ToString & ""
                    mensaje = mensaje & "</td>"
                    mensaje = mensaje & "</tr>"

                    'row 4
                    If (dts.Rows(i).Item("codigo_ded") = 3 Or dts.Rows(i).Item("codigo_ded") = 7) Then '<20 horas
                        mensaje = mensaje & "<tr>"
                        'campo 1
                        mensaje = mensaje & "<td bgcolor='#006699'>"
                        mensaje = mensaje & "<font color='white' face='Courier'>Horas Semanales: "
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td>"
                        mensaje = mensaje & "<font face='Courier'>" & dts.Rows(i).Item("Numhoras_svac").ToString & ""
                        mensaje = mensaje & "</td>"
                        'campo 2
                        mensaje = mensaje & "<td bgcolor='#006699'>"
                        mensaje = mensaje & "<font color='white' face='Courier'>Precio Hora Propuesta: "
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td>"
                        mensaje = mensaje & "<font face='Courier'>" & FormatCurrency(dts.Rows(i).Item("Salario"), 2).ToString & ""
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"
                    Else
                        mensaje = mensaje & "<tr>"
                        'campo 1
                        mensaje = mensaje & "<td bgcolor='#006699'>"
                        mensaje = mensaje & "<font color='white' face='Courier'>Remuneración Propuesta: "
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "<td>"
                        mensaje = mensaje & "<font face='Courier'>" & FormatCurrency(dts.Rows(i).Item("Salario"), 2).ToString & ""
                        mensaje = mensaje & "</td>"
                        mensaje = mensaje & "</tr>"
                    End If

                    'row 5
                    mensaje = mensaje & "<tr>"
                    'campo 1
                    mensaje = mensaje & "<td bgcolor='#006699'>"
                    mensaje = mensaje & "<font color='white' face='Courier'>Justificación: "
                    mensaje = mensaje & "</td>"
                    'campo 2
                    mensaje = mensaje & "<td colspan='3'>"
                    mensaje = mensaje & "<font face='Courier'>" & dts.Rows(i).Item("Observacion").ToString & ""
                    mensaje = mensaje & "</td>"
                    mensaje = mensaje & "</tr>"

                    '**Fin tabla
                    mensaje = mensaje & "</table>"
                    mensaje = mensaje & "<font face='Courier'></br> Atte.<br><br>Campus Virtual - USAT.</font>"

                    ':::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
                    ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual - USAT", dts.Rows(i).Item("email").ToString, "Registro Solictud Vacantes", para & mensaje, True)
                    ':::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

                    ':::: Pruebas :::'
                    'Response.Write(para)
                    'Response.Write("<br>")
                    'Response.Write(mensaje)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarControles()
        Try
            Me.pnlmenor20.Visible = False
            Me.pnlOtros.Visible = False

            Me.txthoras.Text = ""
            Me.txtpreciohora.Text = ""
            Me.txtRemuneración.Text = ""

            Me.ddlDedicacion.SelectedValue = -1
            'Me.ddlDepartamento.SelectedValue = -1
            Me.ddlDedicacion.SelectedValue = -1
            'Me.ddlCeco.SelectedValue = -1
            ddlCeco.DataSource = Nothing
            ddlCeco.DataBind()

            Me.txtFechaInicio.Text = ""
            Me.txtFechaFin.Text = ""
            Me.txtObservacion.Text = ""
            Me.txtformacion.Text = ""

            Me.ddlTipoVacante.Width = 550
            Me.ddlTipoVacante.SelectedValue = "N"
            Me.lnkBuscarVacante.Visible = False

            Me.pnlDatoVacante.Visible = False
            Me.HiddenField.Value = 0
            Me.lblDocente.Text = ""

            '** Actualizamos el registro de solicitudes **'
            gvLista.SelectedIndex = -1
            gvVacantes.SelectedIndex = -1
            RegistroSolicitudes()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub RegistroSolicitudes()
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable
            Dim codigo_ceco As Integer
            '** implementar procedure : SOL_ListaRegistroSolicitudesVacantes

            'Response.Write("ciclo: " & Me.ddlCicloAcademico.SelectedValue)
            'Response.Write("<br>")
            'Response.Write("dpto: " & Me.ddlDepartamento.SelectedValue)
            'Response.Write("<br>")
            'Response.Write("ceco: " & Me.ddlCeco.SelectedValue)
            'Response.Write("<br>")

            'Esta nota la puse xq como aun no carga el ceco, me daba error. :::: dguevara 12.11.2013 :::
            If ddlCeco.SelectedValue.ToString = "" Then
                codigo_ceco = 0
            Else
                codigo_ceco = Me.ddlCeco.SelectedValue
            End If

            dts = obj.ListaRegistroSolicitudesVacantes(Me.ddlCicloAcademico.SelectedValue, "", _
                                                       Me.ddlDepartamento.SelectedValue, _
                                                       codigo_ceco, "S", Session("id_per"))
            'Response.Write("cantidad: " & dts.Rows.Count)
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

    Private Function Validacion() As Boolean
        Try
            '24.10.2013 ** dguevara ** 02:53pm

            Dim sw As Byte = 0
            If Me.ddlDedicacion.SelectedValue <> -1 Then

                If Me.ddlDedicacion.SelectedValue = 3 Or Me.ddlDedicacion.SelectedValue = 7 Then
                    '** Dedicacion: TIEMPO PARCIAL < 20 HRS. **
                    '** Validacion de Horas                  **

                    If Me.txthoras.Text.Trim.Length = 0 Then
                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ingrese una cantidad válida de horas.');", True)
                        Me.txthoras.Focus()
                        Exit Function
                    End If

                    '** Precio Hora  **
                    If txtpreciohora.Text.Trim.Length = 0 Then
                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ingrese un precio por hora correcto');", True)
                        Me.txtpreciohora.Focus()
                        Exit Function
                    End If

                    If CDbl(txtpreciohora.Text) <= 0 Then
                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('El precio por hora debe ser mayor a cero.');", True)
                        Me.txtpreciohora.Focus()
                        Exit Function
                    End If

                Else
                    '** Otras dedicaciones **
                    If Me.txtRemuneración.Text.Trim.Length = 0 Then
                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ingrese una remuneración correcta.');", True)
                        txtRemuneración.Focus()
                        Exit Function
                    End If

                    If CDbl(txtRemuneración.Text) <= 0 Then
                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('La remuneración debe ser mayor a cero.');", True)
                        txtRemuneración.Focus()
                        Exit Function
                    End If
                End If

                '===================================================================
                ':: Validaciones Especificas ::'
                '::: DEDICACION <20 HORAS :::
                If Me.ddlDedicacion.SelectedValue = 3 Then '<20
                    If CDbl(Me.txthoras.Text) <= 0 Then
                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('El número de horas debe ser mayor a cero y menor a 20');", True)
                        Me.txthoras.Focus()
                        Exit Function
                    End If

                    If CDbl(Me.txthoras.Text) >= 20 Then
                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('El número de horas debe ser mayor a cero y menor a 20');", True)
                        Me.txthoras.Focus()
                        Exit Function
                    End If
                End If

                '::: DEDICACION >20 HORAS :::
                If Me.ddlDedicacion.SelectedValue = 7 Then
                    If CDbl(Me.txthoras.Text) <= 20 Then
                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('El número de horas debe ser mayor a 20 y menor a 40');", True)
                        Me.txthoras.Focus()
                        Exit Function
                    End If

                    If CDbl(Me.txthoras.Text) >= 40 Then
                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('El número de horas debe ser mayor a 20 y menor a 40');", True)
                        Me.txthoras.Focus()
                        Exit Function
                    End If
                    'If CDbl(Me.txthoras.Text) < 20 And CDbl(Me.txthoras.Text) > 39 Then
                    '    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('El número de horas debe ser mayor a 20 y menor a 40');", True)
                    '    Me.txthoras.Focus()
                    '    Exit Function
                    'End If
                End If
            End If

            If IsDate(Me.txtFechaInicio.Text.Trim) = True And IsDate(Me.txtFechaFin.Text.Trim) = True Then
                If CDate(txtFechaInicio.Text) > CDate(Me.txtFechaFin.Text) Then
                    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('La fecha de inicio no debe ser mayor que la fecha fin, favor de modifcar.')", True)
                    Me.txtFechaInicio.Focus()
                    Exit Function
                End If
            End If

            '*** Validacion cuando se quiere guardar una vacante Existente ***'
            If Me.ddlTipoVacante.SelectedValue = "E" Then
                If Me.HiddenField.Value = 0 Then
                    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ud. no ha asignado una vacante existente, favor de buscar la vacante.');", True)
                    Exit Function
                End If
            End If


            '**  retorno del valor **
            sw = 1
            If (sw = 1) Then
                Return True
            End If

            Return False

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Protected Sub ddlCeco_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCeco.SelectedIndexChanged
        Try
            'Response.Write("ceco: " & ddlCeco.SelectedValue)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamento.SelectedIndexChanged
        Try
            CargarCecos()
            RegistroSolicitudes()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvLista.RowCommand
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable

            If e.CommandName.Equals("Select") Then
                Dim seleccion As GridViewRow
                Dim codigo_svac As Integer

                '** Obtenemos el Id de la Solicitud **'
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                codigo_svac = Convert.ToInt32(gvLista.DataKeys(seleccion.RowIndex).Values("Codigo"))

                '** Modificamos el Titulo
                Me.lblCodigo_svac.Text = codigo_svac.ToString
                Me.lblTitulo.Text = "Modificar la Solicitud: "

                '** Hacemos la consuta para asignar los datos a los controles **'
                '** No lo asigno de la tabla directamente, debido a que puede ser editada por personal y no seria datos online **'

                dts = obj.ConsultarSolictudVacante(codigo_svac)
                If (dts.Rows.Count > 0) Then
                    Me.btnSoliciar.Text = "     Modificar"
                    Me.btnSoliciar.CssClass = "editarvac"
                    Me.btnSoliciar.ToolTip = "Modifica la solicitud vacante mientras se encuentre pendiente."

                    Me.ddlTipoVacante.SelectedValue = dts.Rows(0).Item("tipo_svac")
                    Me.ddlTipoVacante.Enabled = False

                    Me.ddlCicloAcademico.SelectedValue = dts.Rows(0).Item("Codigo_cac")
                    Me.ddlDepartamento.SelectedValue = dts.Rows(0).Item("Codigo_dac")
                    CargarCecos()
                    Me.ddlCeco.SelectedValue = dts.Rows(0).Item("codigo_ceco")
                    Me.txtFechaInicio.Text = dts.Rows(0).Item("FechaIni_svac")
                    Me.txtFechaFin.Text = dts.Rows(0).Item("FechaFin_svac")
                    Me.txtObservacion.Text = dts.Rows(0).Item("Observacion").ToString
                    Me.txtformacion.Text = dts.Rows(0).Item("Formacion_svac").ToString

                    Me.ddlDedicacion.SelectedValue = dts.Rows(0).Item("codigo_ded")
                    If (dts.Rows(0).Item("codigo_ded") = 3 Or dts.Rows(0).Item("codigo_ded") = 7) Then
                        ' TIEMPO PARCIAL < 20 HRS.
                        Me.pnlmenor20.Visible = False
                        Me.pnlOtros.Visible = True
                        Me.txthoras.Text = dts.Rows(0).Item("Numhoras_svac")
                        Me.txtpreciohora.Text = dts.Rows(0).Item("Salario")
                    Else
                        ' OTROS
                        Me.pnlmenor20.Visible = True
                        Me.pnlOtros.Visible = False
                        Me.txtRemuneración.Text = dts.Rows(0).Item("Salario")
                    End If

                    If dts.Rows(0).Item("Tipo_svac") = "E" Then
                        pnlDatoVacante.Visible = True
                        lnkBuscarVacante.Visible = True
                        ddlTipoVacante.Width = 440
                        lblDocente.Text = dts.Rows(0).Item("docente").ToString.Trim
                        Me.HiddenField.Value = dts.Rows(0).Item("codigo_per")           'Asignamos el codigo_per
                    Else
                        pnlDatoVacante.Visible = False
                        lnkBuscarVacante.Visible = False
                        ddlTipoVacante.Width = 550
                    End If
                Else
                    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ocurrio un error al cargar los datos, volver a intentarlo o comuniquese con desarrollo de sistemas USAT.');", True)
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLista.RowDataBound
        Try
            '** dguevara : 28.10.2013 ** 
            Dim codigo_ded As Integer

            If e.Row.RowType = DataControlRowType.DataRow Then
                codigo_ded = gvLista.DataKeys(e.Row.RowIndex).Values(1) '*** los datakeysname se enumeran desde cero. 1:codigo_ded
                'Response.Write(codigo_ded)
                If codigo_ded <> 3 And codigo_ded <> 7 Then
                    e.Row.Cells(10).Text = ""
                    e.Row.Cells(10).BackColor = Drawing.Color.Gold
                End If

                '** locked las solicitudes con estado A->Aprobado :::: R-> Rechazado.
                If (e.Row.Cells(13).Text.ToString.Trim = "A" Or e.Row.Cells(13).Text.ToString.Trim = "R") Then
                    e.Row.Cells(20).Text = "<center><img src='../../images/solicitudvacantes/sollocked.png' style='border:0px' alt='Edición Bloqueada'/></center>"
                    e.Row.Cells(21).Text = "<center><img src='../../images/solicitudvacantes/sollocked.png' style='border:0px' alt='Eliminación Bloqueada'/></center>"
                End If


                '** Seteamos el estaddo ** 
                'e.Row.Cells(13).Text = "<center><img src='../images/closed.png' style='border: 0px' alt='Bloqueado'/></center>"
                Select Case e.Row.Cells(13).Text.ToString.Trim
                    Case "P"    '** Solicitud Pendiente **
                        e.Row.Cells(13).Text = "<center><img src='../../images/solicitudvacantes/solPendiente.png' style='border:0px' alt='Solicitud Pendiente'/></center>"
                    Case "R"    '** Solicitud Rechazada**
                        e.Row.Cells(13).Text = "<center><img src='../../images/solicitudvacantes/solRechazado.png' style='border:0px' alt='Solicitud Rechazada'/></center>"
                    Case "A"    '** Solicitud Aprobada **
                        e.Row.Cells(13).Text = "<center><img src='../../images/solicitudvacantes/solArpobado.png' style='border:0px' border='Solicitud Aprobada' /></center>"
                End Select

                '** Tipo Variable Existenre **'
                If e.Row.Cells(14).Text = "E" Then
                    e.Row.Cells(12).Text = e.Row.Cells(12).Text + " <font color='red'><b>Reingresante</b></font>"
                End If


            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Try
            Me.LimpiarControles()
            Me.RegistroSolicitudes()
            gvLista.SelectedIndex = -1
            gvVacantes.SelectedIndex = -1
            Me.lblTitulo.Text = "Registrar Solictud Vacante "
            Me.lblCodigo_svac.Text = ""
            Me.btnSoliciar.Text = "     Solicitar"
            Me.btnSoliciar.CssClass = "solicitarvac"
            Me.btnSoliciar.ToolTip = "Registra una vacante."

            Me.ddlTipoVacante.Enabled = True
            Me.ddlTipoVacante.SelectedValue = "N"

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlCicloAcademico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCicloAcademico.SelectedIndexChanged
        Try
            '** dguevara:28.10.2013
            '** Llamamos al procedimiento que carga todos los registros de solicitudes vigentes  **
            Me.RegistroSolicitudes()



            '====== Se quito esta validadcion por solicitud de Hugo Saavedra el 07.03.2014 ====='
            '** El boton solicitar solo estara activo para ciclos superiores al ciclo viegente ** : requerimiento **
            'Response.Write(Me.verificarCicloActivo)
            'If Me.ddlCicloAcademico.SelectedValue > Me.verificarCicloActivo Then
            '    Me.btnSoliciar.Enabled = True
            '    Me.btnSoliciar.ToolTip = "Registra una vacante"
            'Else
            '    Me.btnSoliciar.Enabled = False
            '    Me.btnSoliciar.ToolTip = "Solo se puede solicitar vacantes para ciclos superiores al actual."
            'End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function verificarCicloActivo() As Integer
        Try
            Dim obj As New clsSolicitudVacante
            Return obj.retornaCicloVigente()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function


    Protected Sub ddlTipoVacante_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoVacante.SelectedIndexChanged
        Try

            If ddlTipoVacante.SelectedValue = "E" Then
                '** Vacante Existente **
                ddlTipoVacante.Width = 440
                lnkBuscarVacante.Visible = True     'Activa el link:
                pnlDatoVacante.Visible = True
                Me.lblDocente.Text = "Pulse en el link <b><i>Buscar Vacante</i></b>"
            Else
                '** Nueva Vacante **
                ddlTipoVacante.Width = 550
                lnkBuscarVacante.Visible = False
                pnlDatoVacante.Visible = False
                lblDocente.Text = ""
                HiddenField.Value = 0
                gvVacantes.SelectedIndex = -1
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkBuscarVacante_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBuscarVacante.Click
        Try
            '** dguevara :  31.10.2013 ** '
            pnlRegistro.Visible = False
            pnlBusqueda.Visible = True
            gvVacantes.SelectedIndex = -1       'Esta propiedad la utilizo para limpiar la fila seleccionada anteriormente.
            Me.HiddenField.Value = 0            'Establecemos el valor en 0 para limpiar de una seleccion anterior
            BusquedaVacantes()
            Me.lblInstrucciones.Text = "Seleccione el trabajador al cual se desea generar la Vacante."
            Me.lblInstrucciones.Font.Bold = False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub BusquedaVacantes()
        Try
            Dim obj As New clsSolicitudVacante
            Dim dts As New Data.DataTable

            dts = obj.BuscarVacates(Me.txtBuscar.Text.Trim, Me.ddlBusDedicacion.SelectedValue, Me.ddlBusTipoPersona.SelectedValue)
            Me.lblnumreg.Text = "Se encontraron " & dts.Rows.Count.ToString & " registros."
            Me.lblnumreg.ForeColor = Drawing.Color.Blue
            Me.lblnumreg.Font.Bold = True
            If dts.Rows.Count > 0 Then
                gvVacantes.DataSource = dts
                gvVacantes.DataBind()
            Else
                gvVacantes.DataSource = Nothing
                gvVacantes.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            BusquedaVacantes()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvVacantes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvVacantes.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvVacantes','Select$" & e.Row.RowIndex & "');")
                e.Row.Style.Add("cursor", "hand")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvVacantes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvVacantes.SelectedIndexChanged
        'Response.Write(gvVacantes.SelectedRow.Cells(0).Text)
        HiddenField.Value = gvVacantes.SelectedRow.Cells(0).Text
        If HiddenField.Value > 0 Then
            btnEnviar.Text = "  Enviar"
            btnEnviar.CssClass = "enviarvac"
            gvVacantes.SelectedRow.Cells(1).ForeColor = Drawing.Color.Blue
            Me.lblInstrucciones.Text = "Ud. desea generar una vacante para el trabajador => <b><font color='blue'>" & gvVacantes.SelectedRow.Cells(1).Text & "</font></b><br>Pulse el botón <b><u>Enviar</u><b>."
        End If
    End Sub


    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Try
            'Response.Write("hidden: " & HiddenField.Value)

            If HiddenField.Value > 0 Then       'Entre en este bloque cuando selecciono el trabajador.
                Me.pnlBusqueda.Visible = False
                Me.pnlRegistro.Visible = True
                Me.pnlDatoVacante.Visible = True

                Me.txtBuscar.Text = ""
                Me.ddlBusDedicacion.SelectedValue = -1
                Me.ddlBusTipoPersona.SelectedValue = -1
                btnEnviar.Text = "  Regresar"
                btnEnviar.CssClass = "regresarvac"

                Me.lblDocente.Text = gvVacantes.SelectedRow.Cells(1).Text.Trim.ToString.ToUpper
                Me.lblDocente.Font.Bold = True
                Me.lblDocente.ForeColor = Drawing.Color.Blue
            Else
                Me.pnlBusqueda.Visible = False
                Me.pnlRegistro.Visible = True
                Me.pnlDatoVacante.Visible = False
                btnEnviar.Text = "  Enviar"
                btnEnviar.CssClass = "enviarvac"

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvLista.RowDeleting
        'Response.Write("Eliminacion")
        'Response.Write(gvLista.DataKeys(e.RowIndex).Value)
        Dim rpt As Integer
        Dim obj As New clsSolicitudVacante
        rpt = obj.EliminarSolicitudVacante(gvLista.DataKeys(e.RowIndex).Value, Session("id_per"))
        If rpt = 1 Then
            Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('La solicitud fue dada de baja correctamente.')", True)
            '** Consultamos los registros de solicitudes **'
            Me.RegistroSolicitudes()
            btnLimpiar_Click(sender, e)
        Else
            Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Fatos", "alert('Ocurrio un error al tratar de dar de baja la solicitud vacante, favor de voler a intentarlo o comuniquese con desarrollo de sistemas.')", True)
        End If

    End Sub

End Class

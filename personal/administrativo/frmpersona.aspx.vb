﻿Imports System.Globalization
Imports System.Drawing
Partial Class frmpersona
    Inherits System.Web.UI.Page

    Dim codigo_test As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-PE")
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","

        'Comentar -------------------------------------------------
        'Response.Write("Accion: " & Request.QueryString("accion"))
        '**--------------------------------------------------------
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If

        cli.Value = IIf(Not String.IsNullOrEmpty(Request.QueryString("cli")), Request.QueryString("cli"), 0) 'andy.diaz 19/06/2019

        If IsPostBack = False Then

          

            'Dim script As String
            'script = _
            '"function fnImprimir()" & _
            '"  {" & _
            '"  alert(1) " & _
            '"    document.getElementById('frmImprimir').submit();" & _
            '"  }"


            'ScriptManager.RegisterClientScriptBlock( _
            '    Me, _
            '    GetType(Page), _
            '    "fnImp", _
            '    script, _
            '    True)



            Me.dpprovincia.Items.Add("--Seleccione--") : Me.dpprovincia.Items(0).Value = -1
            Me.dpdistrito.Items.Add("--Seleccione--") : Me.dpdistrito.Items(0).Value = -1
            Me.dppais.Items.Add("--Seleccione--") : Me.dppais.Items(0).Value = -1
            Me.dpTipoEstudioEst.Items.Add("--Seleccione--") : Me.dpTipoEstudioEst.Items(0).Value = -1

            'andy.diaz 19/06/2020
            If Request.QueryString("mod").ToString = "3" Or Request.QueryString("mod").ToString = "10" Then
                tr1.Visible = True
            Else
                tr1.Visible = False
            End If
            '--------

            '*** dguevara 10.10.2013 ***----------------------------------------------------------------------
            'Me.ddlTipoParticipante.Items.Add("--Seleccione--") : ddlTipoParticipante.Items(0).Value = -1
            '-------------------------------------------------------------------------------------------------

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim cco As String = Request.QueryString("cco")
            Dim tbl As Data.DataTable
            obj.AbrirConexion()
            '========================================================
            'Cargar datos del evento=cco
            '========================================================
            tbl = obj.TraerDataTable("EVE_ConsultarEventos", 0, cco, 0)

            ' If Request.QueryString("mod").ToString = "3" Then
            If Request.QueryString("mod").ToString = "10" Then
                Me.trEscuela.Visible = True
                ' Me.trColegio.Visible = True
            Else
                Me.trEscuela.Visible = False
                Me.trColegio.Visible = False
            End If

            '*** andy.diaz 19.06.2019: Agrego campos para POSTGRADO ***
            Me.trCargoEmpresa.Visible = (Request.QueryString("mod").ToString = "5")

            If tbl.Rows.Count > 0 Then
                'Asignar valores a Cajas temporales
                Me.hdcodigo_cpf.Value = tbl.Rows(0).Item("codigo_cpf")
                Me.hdgestionanotas.Value = tbl.Rows(0).Item("gestionanotas_dev")

                'Cargar listas Departamento y Modalidad del Centro de Costos
                ClsFunciones.LlenarListas(Me.dpdepartamento, obj.TraerDataTable("ConsultarLugares", 2, 156, 0), "codigo_dep", "nombre_dep", "--Seleccione--")
                'ClsFunciones.LlenarListas(Me.dpModalidad, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 3, cco, 0, 0), "codigo_min", "nombre_min", "--Seleccione--")

                ClsFunciones.LlenarListas(Me.dpModalidad, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 7, Request.QueryString("mod"), 0, 0), "codigo_min", "nombre_min", "--Seleccione--")

                '--add dguevara 09.10.2013 ------------------------------------------------------------------------------------------**
                ClsFunciones.LlenarListas(Me.ddlTipoParticipante, obj.TraerDataTable("EVE_ConsultarTipoParticipante"), "codigo_tpar", "descripcion_tpar")
                '**------------------------------------------------------------------------------------------------------------------**
                'Cargar Escuelas: TipoEstudio=1; SubTipoEstudio=1
                'ClsFunciones.LlenarListas(Me.dpCodigo_cpf, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 2, 3, 1, 0), "codigo_cpf", "nombre_cpf", "--Seleccione--")
                ClsFunciones.LlenarListas(Me.dpCodigo_cpf, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 2, 10, 1, 0), "codigo_cpf", "nombre_cpf", "--Seleccione--")

                'Carga lista Pais
                ClsFunciones.LlenarListas(Me.dppais, obj.TraerDataTable("ConsultarLugares", "E", 156, 0), "codigo_Pai", "nombre_Pai", "--Seleccione--")


                'INCIO ED AGREGAR COLEGIO PARA GO
                ClsFunciones.LlenarListas(Me.dpTipoEstudioEst, obj.TraerDataTable("TipoEstudioPerfil_Listar", "1", 0, ""), "codigo_tep", "nombre_tep", "--Seleccione--")


                'Cargar Paises del colegio
                ClsFunciones.LlenarListas(Me.dpPaisColegio, obj.TraerDataTable("ConsultarLugares", 1, 0, 0), "codigo_pai", "nombre_pai", "--Seleccione--")
                ClsFunciones.LlenarListas(Me.dpdepartamentocolegio, obj.TraerDataTable("ConsultarLugares", 2, 156, 0), "codigo_dep", "nombre_dep", "--Seleccione--")
                'Cargar Años de promoción
                Dim i As Integer
                Me.dpPromocion.Items.Clear()
                Me.dpPromocion.Items.Add("--Seleccione--")
                Me.dpPromocion.Items(0).Value = -1
                For i = (Now.Year + 1) To 1940 Step -1
                    Me.dpPromocion.Items.Add(i)
                Next
                Me.dpPromocion.SelectedValue = -1

                'FIN ED

                'VALIDAR BLOQUEDO DE INSCRIPCIÓN POR CONTAR LA PERSONA CON DEUDAS A LA UNIVERSIDAD
                chkValidarDeuda.Checked = tbl.Rows(0).Item("validarDeuda_dev")

                'Si gestiona notas, debe verificar si hay plan de estudios
                If tbl.Rows(0).Item("gestionanotas_dev") = 1 Then
                    If IsDBNull(tbl.Rows(0).Item("codigo_pes")) = True Or tbl.Rows(0).Item("codigo_pes").ToString = "" Then
                        Me.lblmensaje.Text = "No puede registrar participantes en este evento, debido a que no se ha registrado un Plan de Estudios."
                        Me.lnkComprobarDNI.Visible = False
                        Me.lnkComprobarNombres.Visible = False
                        Me.dpTipoDoc.Enabled = False
                        Me.cmdGuardar.Enabled = False
                        Me.DesbloquearNombres(False)
                        Me.DesbloquearOtrosDatos(False)
                        Exit Sub
                    End If
                Else
                    'Si no gestiona notas, bloquea la modalidad y asigna una por defecto, según codigo_cpf=9
                    Me.dpModalidad.Enabled = False
                    Me.dpModalidad.SelectedValue = tbl.Rows(0).Item("codigo_min").ToString
                End If
            End If

            '==============================================================
            'Acción Modificar: Cambia datos adicionales, NO DNI y nombres
            '==============================================================
            If Request.QueryString("accion") = "M" Then
                Me.cmdLimpiar.Visible = False
                Me.lnkComprobarDNI.Visible = False
                Me.lnkComprobarNombres.Visible = False

                Me.BuscarPersona("COE", Request.QueryString("pso"), True)

                If Request.QueryString("mod").ToString = "10" Then
                    BuscarEstudiosSecundariosGO(Request.QueryString("pso"), 0)
                End If


                Me.txtdni.Enabled = False
                Me.dpTipoDoc.Enabled = False
                Me.cmdCancelar.UseSubmitBehavior = False
                Me.cmdCancelar.Attributes.Add("onclick", "self.parent.tb_remove();")
            Else
                Me.hdcodigo_pso.Value = 0
                Me.cmdCancelar.UseSubmitBehavior = True
                Me.DesbloquearNombres(False)
                Me.DesbloquearOtrosDatos(False)
            End If

            Me.hdcodigo_cco.Value = Request.QueryString("cco")

            obj.CerrarConexion()
            obj = Nothing
        End If 'Fin postback
    End Sub

    Private Sub BuscarEstudiosSecundariosGO(ByVal pso As String, ByVal cod As String)
        Dim obj As New ClsConectarDatos
        Dim tblalumno As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        tblalumno = obj.TraerDataTable("ACAD_GOUSAT_ALuColegio", "1", pso, 0)

        If tblalumno.Rows(0).Item("codigopaiscolegio").ToString <> "" Then
            'Verificar que el país no sea Perú para asignar OTROS como valor
            If tblalumno.Rows(0).Item("codigopaiscolegio") <> 156 Then
                Me.dpdepartamentocolegio.Items.Add("OTROS") : Me.dpdepartamentocolegio.Items(0).Value = -2
                Me.dpprovinciacolegio.Items.Add("OTROS") : Me.dpprovinciacolegio.Items(0).Value = -2
                Me.dpdistritocolegio.Items.Add("OTROS") : Me.dpdistritocolegio.Items(0).Value = -2
                Me.dpCodigo_col.Items.Add("OTROS") : Me.dpCodigo_col.Items(0).Value = -2
            ElseIf tblalumno.Rows(0).Item("codigo_col").ToString <> "" Then
                'Cargar Departamentos/Provincia/Distrito/Colegio
                ClsFunciones.LlenarListas(Me.dpdepartamentocolegio, obj.TraerDataTable("ConsultarLugares", 2, tblalumno.Rows(0).Item("codigopaiscolegio"), 0), "codigo_dep", "nombre_dep", "--Seleccione--")
                ClsFunciones.LlenarListas(Me.dpprovinciacolegio, obj.TraerDataTable("ConsultarLugares", 3, tblalumno.Rows(0).Item("depcolegio"), 0), "codigo_pro", "nombre_pro", "--Seleccione--")
                ClsFunciones.LlenarListas(Me.dpdistritocolegio, obj.TraerDataTable("ConsultarLugares", 4, tblalumno.Rows(0).Item("procolegio"), 0), "codigo_dis", "nombre_dis", "--Seleccione--")
                ClsFunciones.LlenarListas(Me.dpCodigo_col, obj.TraerDataTable("PEC_ConsultarInstitucionesEducativasPorUbicacion", "DIS", tblalumno.Rows(0).Item("discolegio"), Nothing), "codigo_ied", "nombre_ied", "--Seleccione--")

                'Si hay datos cargados en los combos, predetermina lo encontrado como alumno
                If Me.dpdepartamentocolegio.Items.Count > 0 Then Me.dpdepartamentocolegio.SelectedValue = IIf(tblalumno.Rows(0).Item("depcolegio") Is DBNull.Value, -1, tblalumno.Rows(0).Item("depcolegio"))
                If Me.dpprovinciacolegio.Items.Count > 0 Then Me.dpprovinciacolegio.SelectedValue = IIf(tblalumno.Rows(0).Item("procolegio") Is DBNull.Value, -1, tblalumno.Rows(0).Item("procolegio"))
                If Me.dpdistritocolegio.Items.Count > 0 Then Me.dpdistritocolegio.SelectedValue = IIf(tblalumno.Rows(0).Item("discolegio") Is DBNull.Value, -1, tblalumno.Rows(0).Item("discolegio"))
                If Me.dpCodigo_col.Items.Count > 0 Then Me.dpCodigo_col.SelectedValue = IIf(tblalumno.Rows(0).Item("codigo_col") Is DBNull.Value, -1, tblalumno.Rows(0).Item("codigo_col"))
                dpPaisColegio.SelectedValue = tblalumno.Rows(0).Item("codigopaiscolegio")
                dpPromocion.SelectedValue = tblalumno.Rows(0).Item("añoEgresoSec_Dal")
            End If
        End If
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Private Sub BuscarPersona(ByVal tipo As String, ByVal valor As String, Optional ByVal mostrardni As Boolean = False)

        'Response.Write("<br>")
        'Response.Write("Busca los datos a mostrar, para modificar...,")
        'Response.Write("<br>")

        Me.lblmensaje.Text = ""
        Me.lblmensaje0.Text = "" '04/11/19
        Me.cmdGuardar.Enabled = True
        Dim obj As New ClsConectarDatos
        Dim ExistePersona As Boolean = False

        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim tbl As Data.DataTable
            Dim tblalumno, tblalumno2 As Data.DataTable
            Dim dtParticipacion As New Data.DataTable   '**dguevara**10.10.2013**
            Dim dtDatosLaborales As New Data.DataTable 'andy.diaz 19.06.2019
            Dim dtDatosAlumno As New Data.DataTable 'andy.diaz 20/02/2019
            obj.AbrirConexion()
            '==================================
            'Buscar a la Persona
            '==================================
            tbl = obj.TraerDataTable("PERSON_ConsultarPersona", tipo, valor)
            If tbl.Rows.Count > 0 Then
                ExistePersona = True
                '==================================
                'Buscar Deudas de la persona
                '==================================
                If Request.QueryString("accion") <> "M" Then
                    Dim dtDeudas As New Data.DataTable

                    ' ## Modificado por mvillavicencio 06/08/2012. 
                    'Consulta Deudas Pendientes (vencidas y no vencidas). Antes solo las vencidas.

                    ''Response.Write("CAJ_ConsultarDeudasPendientesPersonaxCco " & tbl.Rows(0).Item("codigo_pso") & " , " & Request.QueryString("mod") & " , " & Request.QueryString("cco"))

                    'dtDeudas = obj.TraerDataTable("CAJ_ConsultarDeudasPersona", tbl.Rows(0).Item("codigo_pso"))

                    'Response.Write(tbl.Rows(0).Item("codigo_pso"))
                    'Response.Write(Request.QueryString("mod"))

                    dtDeudas = obj.TraerDataTable("CAJ_ConsultarDeudasPendientesPersonaxCco", tbl.Rows(0).Item("codigo_pso"), Request.QueryString("mod"), Request.QueryString("cco"))

                    '###############################################################################

                    Me.grwDeudas.DataSource = dtDeudas
                    Me.grwDeudas.DataBind()
                    'Response.Write(dtDeudas.Rows.Count)

                    If (dtDeudas.Rows.Count = 0) Then
                        dtDeudas = obj.TraerDataTable("CAJ_ConsultarDeudasPersonaxCco", tbl.Rows(0).Item("codigo_pso"), Request.QueryString("cco"))

                        Me.grwDeudas.DataSource = dtDeudas
                        Me.grwDeudas.DataBind()

                        If (dtDeudas.Rows.Count = 0) Then
                            dtDeudas = obj.TraerDataTable("CAJ_ConsultarDeudasPendientesPersona", tbl.Rows(0).Item("codigo_pso"), Request.QueryString("mod"))

                            Me.grwDeudas.DataSource = dtDeudas
                            Me.grwDeudas.DataBind()
                           
                            If (dtDeudas.Rows.Count > 0) Then

                                Me.lblmensaje0.Text = "No se puede inscribir porque tiene deuda pendiente" '31/10

                            End If

                        Else

                            Me.lblmensaje0.Text = "No se puede inscribir porque tiene deuda pendiente" '31/10

                        End If

                        'Me.grwDeudas.DataSource = dtDeudas
                        'Me.grwDeudas.DataBind()

                    Else

                        Me.lblmensaje0.Text = "No se puede inscribir porque tiene deuda pendiente" '31/10

                    End If

                End If

                '***==================================================================================
                'Buscar que tipo de participación tiene el inscrito: 10.10.2013 **dguevara**
                dtParticipacion = obj.TraerDataTable("INS_ConsultarTipoParticipacion", tbl.Rows(0).Item("codigo_pso"), Request.QueryString("cco"))
                If dtParticipacion.Rows.Count > 0 Then
                    Me.ddlTipoParticipante.SelectedValue = dtParticipacion.Rows(0).Item("codigo_tpar")
                Else
                    'Para que seleccione.
                    Me.ddlTipoParticipante.SelectedValue = 0
                End If
                '***==================================================================================


                '==================================
                'Buscar Datos del alumno
                '==================================                
                tblalumno = obj.TraerDataTable("PERSON_ConsultarAlumnoPersona", 0, tbl.Rows(0).Item("codigo_pso"), Request.QueryString("cco"), 0)

                'Cargar datos del alumno
                If tblalumno.Rows.Count > 0 Then
                    'Response.Write(tblalumno.Rows(0).Item("codigo_min").ToString)
                    'Me.dpModalidad.SelectedValue = tblalumno.Rows(0).Item("codigo_min").ToString

                    For i As Integer = 0 To Me.dpModalidad.Items.Count - 1
                        If (dpModalidad.Items(i).Value = tblalumno.Rows(0).Item("codigo_min").ToString) Then
                            dpModalidad.SelectedIndex = i
                        End If
                    Next

                    If hdcodigo_cpf.Value = 9 Then
                        Me.dpModalidad.Enabled = False
                    End If
                End If
                tblalumno.Dispose()
                tblalumno = Nothing

                '==================================
                'Mostrar Combo Pais o Dpto/Prov/Distrito
                '==================================
                '                Response.Write(tbl.Rows(0).Item("tipoDocIdent_Pso").ToString & " -<br/>")
                'Si es Peruano, muestra combos Dpto, Distrito y Provincia
                If tbl.Rows(0).Item("tipoDocIdent_Pso").ToString = "DNI" Then
                    MuestraCombosDptoDistritoProvincia()
                End If

                'Si es Extranjero, muestra combo País
                If tbl.Rows(0).Item("tipoDocIdent_Pso").ToString = "PAS" Or _
                tbl.Rows(0).Item("tipoDocIdent_Pso").ToString = "CARNÉ DE EXTRANJERÍA" Or _
                tbl.Rows(0).Item("tipoDocIdent_Pso").ToString = "CARNE EXTRANJERIA" Or _
                tbl.Rows(0).Item("tipoDocIdent_Pso").ToString = "CE." Then
                    MuestraComboPais()
                End If


                '==================================
                'Buscar Dpto/Prov/Distrito Persona
                '==================================
                If tbl.Rows(0).Item("codigo_dep").ToString <> "" Then
                    'Cargar Lista: Provincia y Distrito
                    ClsFunciones.LlenarListas(Me.dpprovincia, obj.TraerDataTable("ConsultarLugares", 3, tbl.Rows(0).Item("codigo_dep"), 0), "codigo_pro", "nombre_pro", "--Seleccione--")
                    ClsFunciones.LlenarListas(Me.dpdistrito, obj.TraerDataTable("ConsultarLugares", 4, tbl.Rows(0).Item("codigo_pro"), 0), "codigo_dis", "nombre_dis", "--Seleccione--")
                End If

                dtDatosLaborales = obj.TraerDataTable("ACAD_selDatosLaborales", cli.Value)
                If dtDatosLaborales.Rows.Count > 0 Then
                    txtCargo.Text = dtDatosLaborales.Rows(0).Item("cargoActual_Dal").ToString
                    txtCentroTrabajo.Text = dtDatosLaborales.Rows(0).Item("centroTrabajo_Dal").ToString
                End If

                dtDatosAlumno = obj.TraerDataTable("ALU_DatosAlumno", cli.Value)
                If dtDatosAlumno.Rows.Count > 0 Then
                    txtciudad.Text = dtDatosAlumno.Rows(0).Item("ciudad_Dal")
                End If

            End If
            obj.CerrarConexion()
            obj = Nothing

            If ExistePersona = True Then
                Me.hdcodigo_pso.Value = tbl.Rows(0).Item("codigo_Pso")
                If mostrardni = True Then
                    If tbl.Rows(0).Item("numeroDocIdent_Pso").ToString <> "" Then
                        Me.txtdni.Text = tbl.Rows(0).Item("numeroDocIdent_Pso").ToString
                        Me.txtdni.Enabled = False
                    Else
                        Me.txtdni.Enabled = True
                    End If
                End If

                Me.txtAPaterno.Text = tbl.Rows(0).Item("apellidoPaterno_Pso")
                Me.txtAMaterno.Text = tbl.Rows(0).Item("apellidoMaterno_Pso").ToString
                Me.txtNombres.Text = tbl.Rows(0).Item("nombres_Pso")
                If tbl.Rows(0).Item("fechanacimiento_pso").ToString <> "" Then
                    Me.txtFechaNac.Text = CDate(tbl.Rows(0).Item("fechaNacimiento_Pso")).ToShortDateString
                End If

                Me.dpSexo.SelectedValue = -1

                If (tbl.Rows(0).Item("sexo_Pso") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("sexo_Pso").ToString.Trim <> "") Then
                    Me.dpSexo.SelectedValue = tbl.Rows(0).Item("sexo_Pso").ToString.ToUpper
                End If

                If (tbl.Rows(0).Item("tipoDocIdent_Pso").ToString = "CE.") Then

                    Me.dpTipoDoc.SelectedIndex = 1
                Else
                    'Inicio Hcano 27-06-17
                    If tbl.Rows(0).Item("tipoDocIdent_Pso").ToString = "PASAPORTE" Then
                        Me.dpTipoDoc.SelectedValue = "PAS"
                    Else
                        Me.dpTipoDoc.SelectedValue = tbl.Rows(0).Item("tipoDocIdent_Pso").ToString
                    End If
                    'Fin Hcano 27-06-17

                End If
                Me.txtemail1.Text = tbl.Rows(0).Item("emailPrincipal_Pso").ToString
                Me.txtemail2.Text = tbl.Rows(0).Item("emailAlternativo_Pso").ToString
                Me.txtdireccion.Text = tbl.Rows(0).Item("direccion_Pso").ToString
                Me.txttelefono.Text = tbl.Rows(0).Item("telefonoFijo_Pso").ToString
                Me.txtcelular.Text = tbl.Rows(0).Item("telefonoCelular_Pso").ToString

                Me.dpEstadoCivil.SelectedValue = -1

                If (tbl.Rows(0).Item("estadoCivil_Pso") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("estadoCivil_Pso").ToString.Trim <> "") Then
                    Me.dpEstadoCivil.SelectedValue = tbl.Rows(0).Item("estadoCivil_Pso").ToString.ToUpper
                End If
                Me.txtruc.Text = tbl.Rows(0).Item("nroRuc_Pso").ToString

                'Si hay pais extranjero registrado para la persona
                If tbl.Rows(0).Item("codigo_Pai").ToString <> "" Then
                    If tbl.Rows(0).Item("codigo_Pai").ToString <> 156 Then
                        Me.dppais.SelectedValue = tbl.Rows(0).Item("codigo_Pai")
                    End If
                End If

                'Si hay distrito registrado para la persona
                If tbl.Rows(0).Item("codigo_dep").ToString <> "" Then
                    If tbl.Rows(0).Item("codigo_dep") = 26 Or tbl.Rows(0).Item("codigo_dep").ToString = "" Then
                        Me.dpdepartamento.SelectedValue = -1
                    Else
                        Me.dpdepartamento.SelectedValue = tbl.Rows(0).Item("codigo_dep")
                    End If
                    If tbl.Rows(0).Item("codigo_pro") = 1 Or tbl.Rows(0).Item("codigo_pro").ToString = "" Then
                        Me.dpprovincia.SelectedValue = -1
                    ElseIf Me.dpprovincia.Items.Count > 0 Then
                        Me.dpprovincia.SelectedValue = tbl.Rows(0).Item("codigo_pro")
                    End If

                    If tbl.Rows(0).Item("codigo_dis") = 1 Or tbl.Rows(0).Item("codigo_dis").ToString = "" Then
                        Me.dpdistrito.SelectedValue = -1
                    ElseIf Me.dpdistrito.Items.Count > 0 Then
                        Me.dpdistrito.SelectedValue = tbl.Rows(0).Item("codigo_dis")
                    End If
                End If

                'Buscar si tiene DEUDAS
                '## Modificado por mvillavicencio 06/08/2012. 
                'Se puede registrar si tiene 1 o 0 deudas

                EvaluarDeuda()
                'Bloque nombres cuando no es Administrador
                DesbloquearNombres(False)
                DesbloquearOtrosDatos(True)
                lnkComprobarNombres.Visible = False
            ElseIf ValidarNroIdentidad() = True Then
                Me.hdcodigo_pso.Value = 0
                lnkComprobarNombres.Visible = True
                DesbloquearNombres(True)
                DesbloquearOtrosDatos(False)

                If Me.txtAPaterno.Enabled = True Then
                    Me.txtAPaterno.Focus()
                End If
            End If
            tbl.Dispose()
            tbl = Nothing
        Catch ex As Exception
            Me.lblmensaje.Text = "Error: " & ex.Message
            Me.lblmensaje.Visible = True
            'obj.CerrarConexion()
            'obj = Nothing
        End Try
    End Sub

    Protected Sub EvaluarDeuda()
        Dim tblalumno As New Data.DataTable

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        obj.AbrirConexion()
        tblalumno = obj.TraerDataTable("PERSON_ConsultarAlumnoPersona", 4, Me.txtdni.Text.Trim, 2, 0)
        codigo_test = 0
        If tblalumno.Rows.Count > 0 Then
            codigo_test = tblalumno.Rows(0).Item("codigo_test").ToString
        End If
        obj.CerrarConexion()
        'Fecha cambio 24/04/2019
        'La sra. Julia envío un correo de porque NO VALIDA deudas de un alumno 
        'y es porque no validaba para pre grado --> Se quitó del condicional: And codigo_test <> "2"
        If grwDeudas.Rows.Count > 0 And Request.QueryString("ctf") <> "146" And Request.QueryString("ctf") <> "19" Then 'Bloqueo a personas con deuda excepto al perfil de Coord. General
            If chkValidarDeuda.Checked = True Then

                Me.lblmensaje.Text = "No puede registrar a la persona porque tiene más de una (1) deuda PENDIENTE.<br/> Debe coordinar con la Oficina de Pensiones para regularizar su estado."
                Me.lblmensaje.Visible = True
                Me.grwDeudas.Visible = True
                Me.cmdGuardar.Enabled = False

                'Lanzando el alert con Response.Write, salia error Js: Objeto no soporta esta propiedad o método
                'Response.Write("<script> alert('No puede registrar a la persona porque tiene deudas PENDIENTES. Debe coordinar con la Oficina de Pensiones para regularizar su estado.')</script>")
                Dim sbMensaje2 As New StringBuilder
                sbMensaje2.Append("<script type='text/javascript'>")
                sbMensaje2.AppendFormat("alert('No puede registrar a la persona porque tiene más de una (1) deuda PENDIENTE. Debe coordinar con la Oficina de Pensiones para regularizar su estado.')")
                sbMensaje2.Append("</script>")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje2.ToString, False)


            Else
                Me.lblmensaje.Text = "La persona cuenta con " & grwDeudas.Rows.Count & " deudas PENDIENTES a la Universidad."
                Me.lblmensaje.Visible = True
                Me.grwDeudas.Visible = True
                Me.cmdGuardar.Enabled = False
            End If

        Else

            Me.lblmensaje.Visible = False
            Me.lblmensaje.Text = ""
            Me.grwDeudas.Visible = False
            Me.cmdGuardar.Enabled = True
            If Request.QueryString("ctf") = "146" Or codigo_test = "2" Then
                Me.lblmensaje.Text = "La persona cuenta con " & grwDeudas.Rows.Count & " deudas PENDIENTES a la Universidad."
                Me.lblmensaje.Visible = True
                Me.grwDeudas.Visible = True
            End If

        End If

    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            If ValidarNroIdentidad() = True Then
                Me.grwDeudas.Visible = False
                Me.lblmensaje.Text = ""

                Dim codigo_psoNuevo(1) As String
                Dim codigo_cliNuevo(2) As String
                Dim codigo_itparNuevo(1) As String

                Dim tcl As String = "E"
                Dim cli As Integer = 0
                Dim id As String = Request.QueryString("id")
                Dim cco As String = Request.QueryString("cco")
                Dim pso As String
                Dim obj As New ClsConectarDatos
                Dim dtParticipacion As New Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                'Try

                If Request.QueryString("accion") = "A" Then
                    'Entra cuando se va hacer un nuevo registro
                    pso = Me.hdcodigo_pso.Value
                Else
                    'Aqui entra cuando se va a modificar.
                    pso = Request.QueryString("pso")
                End If

                obj.AbrirConexion()
                '=================================================================
                'Verificar deudas de la persona que existe
                '=================================================================
                Me.grwDeudas.DataSource = obj.TraerDataTable("CAJ_ConsultarDeudasPersonaxCco", pso, Request.QueryString("cco"))
                'Response.Write(1)
                Me.grwDeudas.DataBind()

                If (grwDeudas.Rows.Count = 0) Then
                    Me.grwDeudas.DataSource = obj.TraerDataTable("CAJ_ConsultarDeudasPendientesPersona", pso, Request.QueryString("mod"))
                    Me.grwDeudas.DataBind()
                End If

                'Si va editar su nombre que no valide deudas
                If (Request.QueryString("accion") <> "M") Then
                    EvaluarDeuda()
                    If grwDeudas.Rows.Count > 0 And Request.QueryString("ctf") <> "146" And codigo_test <> "2" Then 'Bloqueo a personas con deuda excepto al perfil de Coord. General
                        'Response.Write(3)
                        Me.lblmensaje.Text = "No puede registrar a la persona porque tiene deudas PENDIENTES.<br/> Debe coordinar con la Oficina de Pensiones para regularizar su estado."
                        Me.grwDeudas.Visible = True
                        Me.lblmensaje.Visible = True
                        obj.CerrarConexion()
                        Exit Sub
                    End If
                End If

                'Response.Write(3)

                If chkValidarDeuda.Checked = True Then
                    If (grwDeudas.Rows.Count > 0) Then
                        Me.lblmensaje.Text = "No puede registrar a la persona porque tiene deudas PENDIENTES.<br/> Debe coordinar con la Oficina de Pensiones para regularizar su estado."
                        Me.lblmensaje.Visible = True
                        Me.grwDeudas.Visible = True
                        Me.cmdGuardar.Enabled = False
                    End If
                Else
                    Me.lblmensaje.Text = "La persona cuenta con " & grwDeudas.Rows.Count & " deudas PENDIENTES a la Universidad."
                    Me.lblmensaje.Visible = True
                    Me.grwDeudas.Visible = True
                    Me.cmdGuardar.Enabled = True
                End If
                obj.CerrarConexion()

                obj.IniciarTransaccion()
                '=================================================================
                'Grabar a la persona: Aquí se verifica si EXISTE.
                '=================================================================

                'Asignar pais
                Dim pais As Integer
                If dpTipoDoc.SelectedValue = "DNI" Then
                    pais = 156
                Else
                    pais = dppais.SelectedValue
                End If

                'Response.Write(3)
                'Si la fecha de nacimiento no ha sido ingresada, se envia el valor vacio
                'De lo contrario se convierte a tipo Date                
                If Me.txtFechaNac.Text.Trim = "" Then
                    obj.Ejecutar("PERSON_Agregarpersona", pso, _
                    UCase(Me.txtAPaterno.Text.Trim), UCase(Me.txtAMaterno.Text.Trim), UCase(Me.txtNombres.Text.Trim), _
                    "01/01/1900", Me.dpSexo.SelectedValue, _
                    Me.dpTipoDoc.SelectedValue, Me.txtdni.Text.Trim, _
                    LCase(Me.txtemail1.Text.Trim), LCase(Me.txtemail2.Text.Trim), _
                    UCase(Me.txtdireccion.Text.Trim), Me.dpdistrito.SelectedValue, _
                    Me.txttelefono.Text.Trim, Me.txtcelular.Text.Trim, Me.dpEstadoCivil.SelectedValue, Me.txtruc.Text.ToString, _
                    id, pais, "0").copyto(codigo_psoNuevo, 0)
                Else
                    'lblmensaje.Visible = True
                    'lblmensaje.Text = "pso: " & pso & _
                    '"nombrs: " & txtAPaterno.Text & txtAMaterno.Text & txtNombres.Text & _
                    '"nacim: " & txtFechaNac.Text & " sexo: " & dpSexo.SelectedValue & _
                    '"dpTipoDoc" & dpTipoDoc.SelectedValue & "nro dni: " & txtdni.Text & _
                    '"email1: " & txtemail1.Text & " email2: " & txtemail2.Text & _
                    '"direcc: " & txtdireccion.Text & "distito: " & dpdistrito.SelectedValue & _
                    '"tlf: " & txttelefono.Text & " celu: " & txtcelular.Text & _
                    '"estado civil: " & dpEstadoCivil.SelectedValue & " ruc: " & txtruc.Text & _
                    '"id: " & id & "pais: " & pais


                    obj.Ejecutar("PERSON_Agregarpersona", pso, _
                    UCase(Me.txtAPaterno.Text.Trim), UCase(Me.txtAMaterno.Text.Trim), UCase(Me.txtNombres.Text.Trim), _
                    CDate(Me.txtFechaNac.Text.Trim), Me.dpSexo.SelectedValue, _
                    Me.dpTipoDoc.SelectedValue, Me.txtdni.Text.Trim, _
                    LCase(Me.txtemail1.Text.Trim), LCase(Me.txtemail2.Text.Trim), _
                    UCase(Me.txtdireccion.Text.Trim), Me.dpdistrito.SelectedValue, _
                    Me.txttelefono.Text.Trim, Me.txtcelular.Text.Trim, Me.dpEstadoCivil.SelectedValue, Me.txtruc.Text.ToString, _
                    id, pais, "0").copyto(codigo_psoNuevo, 0)
                    'Response.Write(4)
                End If

                If codigo_psoNuevo(0).ToString = "0" Then
                    obj.AbortarTransaccion()
                    Me.lblmensaje.Text = "Ocurrió un error al registrar los datos Persona. Contáctese con desarrollosistemas@usat.edu.pe"
                    Exit Sub
                Else
                    '**************************************************************************'
                    '** Agregamos el tipo participación *** por dguevara 09.10.2013 ***
                    'En este punto nos devuelte el codigo_psoNuevo si es <inserta> o <modifica>.--<<<<<<<<<<
                    'Response.Write(codigo_psoNuevo(0))

                    'Agregar Tipo Participacion en el registro
                    'obj.Ejecutar("EVE_AgregarInscripcionesTipoParticipacion", _
                    '         codigo_psoNuevo(0), _
                    '         Request.QueryString("cco"), _
                    '         Me.ddlTipoParticipante.SelectedValue, _
                    '         Me.Request.QueryString("id"), "0").copyto(codigo_itparNuevo, 0)

                    '=================================================================
                    'Agregar && Modificar Tipo Participacion
                    obj.Ejecutar("EVE_AgregarEditarInscripcionesTipoParticipacion", _
                             codigo_psoNuevo(0), _
                             Request.QueryString("cco"), _
                             Me.ddlTipoParticipante.SelectedValue, _
                             Me.Request.QueryString("id"), _
                             Request.QueryString("accion"), "0").copyto(codigo_itparNuevo, 0)
                    'Response.Write(5)
                    If codigo_itparNuevo(0) = "0" Then
                        obj.AbortarTransaccion()
                        Exit Sub
                        '*** dguevara ***' 
                        'No mostramos el mensaje debido a que llama a otra pagina, estaria por las puras...
                        'Me.lblmensaje.Text = "Ocurrió un error al registrar los datos Persona <Tipo Participación>. Contáctese con desarrollosistemas@usat.edu.pe"
                    End If
                    '***************************************************************************'
                End If

                '========================================================
                'andy.diaz 11/03/2020 - Registro al postulante en el CRM
                '========================================================
                Dim lo_Salida As New Object()
                Dim dt As Data.DataTable

                'Parámetros
                Dim codigoOri As Integer = 6 'OFICINA INFORMES
                Dim nombreEve As String = "OFICINA ADMISIÓN"
                Dim descripcionCac As String = ""
                Dim codigoTest As Integer = -1
                Dim codigoCpf As Integer = -1
                Dim descripcionTest As String = ""

                dt = obj.TraerDataTable("LOG_BuscaCentroCosto", cco)
                If dt.Rows.Count > 0 Then
                    codigoTest = dt.Rows(0).Item("codigo_test")
                    codigoCpf = dt.Rows(0).Item("codigo_cpf")
                    descripcionTest = dt.Rows(0).Item("descripcion_test")
                End If

                'Para GO se obtiene el codigo_cpf del selector de carrera
                If Request.QueryString("mod").ToString = "10" Then
                    codigoCpf = Me.dpCodigo_cpf.SelectedValue
                End If

                dt = obj.TraerDataTable("ACAD_RetornaCicloAdmisionPorTipoEstudio", codigoTest)
                If dt.Rows.Count > 0 Then
                    descripcionCac = dt.Rows(0).Item("descripcion_Cac")
                End If

                If descripcionCac = "" Then
                    obj.AbortarTransaccion()
                    Me.lblmensaje0.Text = "No se ha definido un ciclo activo para el procedo de admisión para el tipo de estudio " & descripcionTest & ". Contáctese con serviciosti@usat.edu.pe"
                    Me.lblmensaje0.Visible = True
                    Exit Sub
                End If

                Dim codigoMin As Integer = Me.dpModalidad.SelectedValue
                Dim codigoDoci As Integer = 0

                Select Case Me.dpTipoDoc.SelectedValue
                    Case "DNI"
                        codigoDoci = 1
                    Case "CARNÉ DE EXTRANJERÍA"
                        codigoDoci = 4
                    Case "PAS"
                        codigoDoci = 7
                End Select

                Dim numerodocInt As String = Me.txtdni.Text.Trim
                Dim apepaternoInt As String = Me.txtAPaterno.Text.Trim
                Dim apematernoInt As String = Me.txtAMaterno.Text.Trim
                Dim nombresInt As String = Me.txtNombres.Text.Trim
                Dim fechanacimientoInt As String = Me.txtFechaNac.Text.Trim
                Dim gradoInt As String = "E" 'EGRESADO, para pregrado se utiliza el nuevo formulario
                Dim codigoIed As Integer = 0
                'Para GO se obtiene el codigo_ied del selector de colegio
                If Request.QueryString("mod").ToString = "10" Then
                    codigoIed = dpCodigo_col.SelectedValue
                End If

                Dim estadoInt As Integer = 1
                Dim telNumeroTei As String = Me.txttelefono.Text.Trim
                Dim celNumeroTei As String = Me.txtcelular.Text.Trim
                Dim descripcionEmi As String = Me.txtemail1.Text.Trim
                Dim codigoDep As Integer = Me.dpdepartamento.SelectedValue
                Dim codigoPro As Integer = Me.dpprovincia.SelectedValue
                Dim codigoDis As Integer = Me.dpdistrito.SelectedValue
                Dim direccionDin As String = Me.txtdireccion.Text.Trim
                Dim verificadoEmi As Integer = 0

                lo_Salida = obj.Ejecutar("WS_InformacionInteresado" _
                             , codigoOri _
                             , cco _
                             , nombreEve _
                             , descripcionCac _
                             , codigoTest _
                             , codigoMin _
                             , codigoDoci _
                             , numerodocInt _
                             , apepaternoInt _
                             , apematernoInt _
                             , nombresInt _
                             , fechanacimientoInt _
                             , gradoInt _
                             , codigoIed _
                             , codigoCpf _
                             , estadoInt _
                             , telNumeroTei _
                             , celNumeroTei _
                             , descripcionEmi _
                             , codigoDep _
                             , codigoPro _
                             , codigoDis _
                             , direccionDin _
                             , "", "", "", "", "", "", "" _
                             , id _
                             , verificadoEmi _
                             , "0", "", "0" _
                        )

                If lo_Salida(0) <> "1" Then
                    'obj.AbortarTransaccion()
                    Me.lblmensaje0.Text = "Ocurrió un error al intentar registrar los datos en el CRM. El procedimiento devolvió el mensaje: " & lo_Salida(1) & ". Contáctese con serviciosti@usat.edu.pe"
                    Me.lblmensaje0.Visible = True
                    'Exit Sub
                Else
                    Me.lblmensaje0.Visible = False
                End If
                '========================================================

                'Response.Write("antes if")
                If CBool(Me.hdgestionanotas.Value) = True Then
                    '===============================================================================================================
                    'Grabar como ESTUDIANTE: Siempre y cuando gestione notas
                    '===============================================================================================================                    
                    'Si la fecha de nacimiento no ha sido ingresada, se envia el valor vacio
                    'De lo contrario se convierte a tipo Date                
                    If Me.txtFechaNac.Text.Trim = "" Then
                        obj.Ejecutar("EVE_AgregarParticipanteEventoGestionaNotas", cco, Me.dpModalidad.SelectedValue, _
                             UCase(Me.txtAPaterno.Text.Trim), UCase(Me.txtAMaterno.Text.Trim), UCase(Me.txtNombres.Text.Trim), _
                             "01/01/1900", Me.dpSexo.SelectedValue, _
                             Me.dpTipoDoc.SelectedValue, Me.txtdni.Text.Trim, _
                             LCase(Me.txtemail1.Text.Trim), LCase(Me.txtemail2.Text.Trim), _
                             UCase(Me.txtdireccion.Text.Trim), Me.dpdistrito.SelectedValue, _
                             Me.txttelefono.Text.Trim, Me.txtcelular.Text.Trim, Me.dpEstadoCivil.SelectedValue, _
                             Me.GeneraClave, id, codigo_psoNuevo(0), Me.dppais.SelectedValue, "0", txtciudad.Text.Trim).copyto(codigo_cliNuevo, 0)

                        'Response.Write(6)
                    Else
                        obj.Ejecutar("EVE_AgregarParticipanteEventoGestionaNotas", cco, Me.dpModalidad.SelectedValue, _
                             UCase(Me.txtAPaterno.Text.Trim), UCase(Me.txtAMaterno.Text.Trim), UCase(Me.txtNombres.Text.Trim), _
                             CDate(Me.txtFechaNac.Text.Trim), Me.dpSexo.SelectedValue, _
                             Me.dpTipoDoc.SelectedValue, Me.txtdni.Text.Trim, _
                             LCase(Me.txtemail1.Text.Trim), LCase(Me.txtemail2.Text.Trim), _
                             UCase(Me.txtdireccion.Text.Trim), Me.dpdistrito.SelectedValue, _
                             Me.txttelefono.Text.Trim, Me.txtcelular.Text.Trim, Me.dpEstadoCivil.SelectedValue, _
                             Me.GeneraClave, id, codigo_psoNuevo(0), Me.dppais.SelectedValue, "0", txtciudad.Text.Trim).copyto(codigo_cliNuevo, 0)
                        'Response.Write(7)
                    End If

                    If codigo_cliNuevo(0).ToString = "0" Then
                        obj.AbortarTransaccion()
                        Me.lblmensaje.Text = "Ocurrió un error al registrar los datos del participante. Contáctese con desarrollosistemas@usat.edu.pe"
                        Me.lblmensaje.Visible = True
                        Exit Sub
                    End If
                    If codigo_cliNuevo(0).ToString = "-1" Then
                        obj.AbortarTransaccion()
                        Me.lblmensaje.Text = "No puede registrar participantes en este evento, debido a que no se ha registrado un Plan de Estudios."
                        Me.lblmensaje.Visible = True
                        Exit Sub
                    End If




                    'Todas las personas se registran como alumnos
                    tcl = "E"
                    cli = codigo_cliNuevo(0)
                Else
                    '===============================================================================================================
                    'Grabar como ESTUDIANTE: Siempre y cuando NO gestione notas
                    '===============================================================================================================

                    If Me.txtFechaNac.Text.Trim = "" Then
                        obj.Ejecutar("EVE_AgregarParticipanteEventoNoGestionaNotas", cco, Me.dpModalidad.SelectedValue, _
                             UCase(Me.txtAPaterno.Text.Trim), UCase(Me.txtAMaterno.Text.Trim), UCase(Me.txtNombres.Text.Trim), _
                             "01/01/1900", Me.dpSexo.SelectedValue, _
                             Me.dpTipoDoc.SelectedValue, Me.txtdni.Text.Trim, _
                             LCase(Me.txtemail1.Text.Trim), LCase(Me.txtemail2.Text.Trim), _
                             UCase(Me.txtdireccion.Text.Trim), Me.dpdistrito.SelectedValue, _
                             Me.txttelefono.Text.Trim, Me.txtcelular.Text.Trim, Me.dpEstadoCivil.SelectedValue, _
                             Me.GeneraClave, id, codigo_psoNuevo(0), Me.dppais.SelectedValue, "E", "0").copyto(codigo_cliNuevo, 0)
                    Else
                        obj.Ejecutar("EVE_AgregarParticipanteEventoNoGestionaNotas", cco, Me.dpModalidad.SelectedValue, _
                             UCase(Me.txtAPaterno.Text.Trim), UCase(Me.txtAMaterno.Text.Trim), UCase(Me.txtNombres.Text.Trim), _
                             CDate(Me.txtFechaNac.Text.Trim), Me.dpSexo.SelectedValue, _
                             Me.dpTipoDoc.SelectedValue, Me.txtdni.Text.Trim, _
                             LCase(Me.txtemail1.Text.Trim), LCase(Me.txtemail2.Text.Trim), _
                             UCase(Me.txtdireccion.Text.Trim), Me.dpdistrito.SelectedValue, _
                             Me.txttelefono.Text.Trim, Me.txtcelular.Text.Trim, Me.dpEstadoCivil.SelectedValue, _
                             Me.GeneraClave, id, codigo_psoNuevo(0), Me.dppais.SelectedValue, "E", "0").copyto(codigo_cliNuevo, 0)
                    End If

                    'codigo_cliNuevo(0): es el tipo de cliente: P / E
                    'codigo_cliNuevo(1): es el ID de la persona o alumno

                    If codigo_cliNuevo(1).ToString = "0" Then
                        obj.AbortarTransaccion()
                        Me.lblmensaje.Text = "Ocurrió un error al registrar los datos del participante. Contáctese con desarrollosistemas@usat.edu.pe"
                        Me.lblmensaje.Visible = True
                        Exit Sub

                    Else
                        'Incio: agregadao por edgard  para actualizar carrera profesional del combo en caso programa GO
                        If Request.QueryString("mod").ToString = "3" Or Request.QueryString("mod").ToString = "10" Then
                            If Me.dpCodigo_cpf.SelectedValue > 0 Then
                                obj.Ejecutar("ACAD_GOUSAT_updcpf", CInt(codigo_cliNuevo(1)), Me.dpCodigo_cpf.SelectedValue)

                            End If
                            '=================================================================
                            'actualiza el tipo perfil de estudio seleccionado Epena 04/10/2017 
                            '=================================================================
                            If Me.dpTipoEstudioEst.SelectedValue > 0 Then
                                obj.Ejecutar("ACAD_GOUSAT_updperfilestudio", CInt(cli), Me.dpTipoEstudioEst.SelectedValue)
                            End If
                        End If
                        'fin

                    End If

                    If codigo_cliNuevo(1).ToString = "-1" Then
                        obj.AbortarTransaccion()
                        Me.lblmensaje.Text = "No puede registrar participantes en este evento, debido a que no se ha registrado un Plan de Estudios."
                        Me.lblmensaje.Visible = True
                        Exit Sub
                    End If

                    'Se obtiene el ID de personal o alumno para asignar el tipo
                    tcl = codigo_cliNuevo(0)
                    cli = codigo_cliNuevo(1)
                End If

                If Request.QueryString("mod").ToString = "3" Or Request.QueryString("mod").ToString = "10" Then
                    If Me.dpCodigo_cpf.SelectedValue > 0 Then
                        obj.Ejecutar("ACAD_GOUSAT_updcpf", CInt(cli), Me.dpCodigo_cpf.SelectedValue)

                    End If
                    '=================================================================
                    'actualiza el tipo perfil de estudio seleccionado Epena 04/10/2017  
                    '=================================================================
                    If Me.dpTipoEstudioEst.SelectedValue > 0 Then
                        obj.Ejecutar("ACAD_GOUSAT_updperfilestudio", CInt(cli), Me.dpTipoEstudioEst.SelectedValue)
                    End If


                    If Me.dpCodigo_cpf.SelectedValue > 0 Then
                        obj.Ejecutar("ACAD_GOUSAT_updEstSec", CInt(cli), Me.dpPromocion.SelectedValue, dpPaisColegio.SelectedValue, dpCodigo_col.SelectedValue)

                    End If
                End If

                '*** andy.diaz 19.06.2019: Agrego campos Cargo y Empresa para modalidad POSTGRADO ***
                If Request.QueryString("mod").ToString = "5" Then
                    obj.Ejecutar("ACAD_updDatosLaborales", cli, Me.txtCargo.Text.Trim.ToUpper, Me.txtCentroTrabajo.Text.Trim.ToUpper)
                End If

                obj.TerminarTransaccion()
                obj = Nothing
                '=================================================================
                'Se ha registrado correctamente.
                '=================================================================

                Me.lblmensaje.Text = "Se han registrado los datos correctamente."
                Me.lblmensaje.Visible = True





                If Request.QueryString("accion") = "A" Then
                    If Request.QueryString("mod") = "5" Then 'POSGRADO
                        Dim codigoCco As Integer = Request.QueryString("cco")
                        EnviarCorreoAccesos(cli, codigoCco, id)
                    End If

                    'Page.Request.QueryString.ToString

                    '*** Descomentar la siguiente linea: 09.10.2013 *** ---
                    Response.Redirect("pec/frmgeneracioncargos.aspx?mod=" & Request.QueryString("mod") & "&id=" & Request.QueryString("id") & "&cco=" & cco & "&tcl=" & tcl & "&cli=" & cli & "&pso=" & codigo_psoNuevo(0) & "&ctf=" & Request.QueryString("ctf"))
                    'Response.Write("pec/frmgeneracioncargos.aspx?mod=" & Request.QueryString("mod") & "&id=" & Request.QueryString("id") & "&cco=" & cco & "&tcl=" & tcl & "&cli=" & cli & "&pso=" & codigo_psoNuevo(0) & "&ctf=" & Request.QueryString("ctf"))
                    'Else
                    '    Response.Write("<script>")
                    '    Response.Write("window.open('frmExportaFichaPostulacionGO.aspx?pso=" & codigo_psoNuevo(0).ToString & "&cli=" & cli.ToString & " &KeepThis=true&TB_iframe=true&height=400&width=700&modal=false')")

                    '    Response.Write("<" & "/script>")
                    '    Response.Write("frmExportaFichaPostulacionGO.aspx?pso=" & codigo_psoNuevo(0) & "&cli=" & cli & " &KeepThis=true&TB_iframe=true&height=400&width=700&modal=false")
                Else
                    'EXPORTAR FICHA
                    Me.pso.Value = codigo_psoNuevo(0)
                    Me.cli.Value = cli
                    Dim script As String = "fnImprimir()"
                    ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), "fnImp", script, True)

                End If



                'Catch ex As Exception
                '    obj.AbortarTransaccion()
                '    Me.lblmensaje.Text = "Ocurrió un Error al Registrar el estado. Intente mas tarde." & Chr(13) & ex.Message
                '    Me.lblmensaje.Visible = True
                '    obj = Nothing
                'End Try
            Else
                Me.lblmensaje.Text = Me.lblmensaje.Text & " No encontró DNI"
                Me.lblmensaje.Visible = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " LINEA: " & ex.StackTrace)
        End Try
    End Sub

    Private Sub EnviarCorreoAccesos(ByVal codigoAlu As Integer, ByVal codigoCco As Integer, ByVal codigoPer As Integer)
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            Dim cls As New clsEvento
            Dim blnResultado As Boolean = cls.EnviaFichaInscripcion(codigoAlu, codigoCco, codigoPer)
            If (blnResultado = True) Then
                Response.Write("<script>alert('Correo Enviado')</script>")
            Else
                Response.Write("<script>alert('No se pudo enviar el correo electronico al alumno')</script>")
            End If

            obj.CerrarConexion()
            obj = Nothing

        Catch ex As Exception
            Response.Write(ex.Message & " LINEA: " & ex.StackTrace)
        End Try
    End Sub

    Private Sub wait(ByVal seconds As Integer)



        For i As Integer = 0 To seconds * 100
            System.Threading.Thread.Sleep(10)

        Next

    End Sub
    Private Function GeneraLetra() As String
        GeneraLetra = Chr(((Rnd() * 100) Mod 25) + 65)
    End Function
    Private Function GeneraClave() As String
        Randomize()
        Dim Letras As String
        Dim Numeros As String
        Letras = GeneraLetra() & GeneraLetra()
        Numeros = Format((Rnd() * 8888) + 1111, "0000")
        GeneraClave = Letras & Numeros
    End Function
    Protected Sub dpprovincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpprovincia.SelectedIndexChanged
        Me.dpdistrito.Items.Clear()
        If Me.dpprovincia.SelectedValue <> -1 Then
            Me.dpdistrito.Items.Add("--Seleccione--") : Me.dpdistrito.Items(0).Value = -1
            Me.dpdistrito.Enabled = True
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.dpdistrito, obj.TraerDataTable("ConsultarLugares", 4, Me.dpprovincia.SelectedValue, 0), "codigo_dis", "nombre_dis", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
        Else
            Me.dpdistrito.Enabled = False
        End If
    End Sub
    Protected Sub dpdepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpdepartamento.SelectedIndexChanged
        Me.dpdistrito.Items.Clear()
        Me.dpprovincia.Items.Clear()

        If Me.dpdepartamento.SelectedValue <> -1 Then
            Me.dpprovincia.Items.Add("--Seleccione--") : Me.dpprovincia.Items(0).Value = -1
            Me.dpdistrito.Items.Add("--Seleccione--") : Me.dpdistrito.Items(0).Value = -1
            Me.dpprovincia.Enabled = True
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.dpprovincia, obj.TraerDataTable("ConsultarLugares", 3, Me.dpdepartamento.SelectedValue, 0), "codigo_pro", "nombre_pro", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
        Else
            Me.dpprovincia.Enabled = False
            Me.dpdistrito.Enabled = False
        End If
    End Sub
    Private Sub DesbloquearNombres(ByVal estado As Boolean)
        'Me.dpTipo.Enabled = estado
        Me.txtAPaterno.Enabled = estado
        Me.txtAMaterno.Enabled = estado
        Me.txtNombres.Enabled = estado
    End Sub
    Private Sub DesbloquearOtrosDatos(ByVal estado As Boolean)
        Me.txtFechaNac.Enabled = estado
        Me.dpSexo.Enabled = estado
        Me.txtemail1.Enabled = estado
        Me.txtemail2.Enabled = estado
        Me.txtdireccion.Enabled = estado
        Me.dppais.Enabled = estado
        Me.dpdepartamento.Enabled = estado
        Me.dpprovincia.Enabled = estado
        Me.dpdistrito.Enabled = estado
        Me.txttelefono.Enabled = estado
        Me.txtcelular.Enabled = estado
        Me.dpEstadoCivil.Enabled = estado
        Me.txtruc.Enabled = estado
        Me.txtCargo.Enabled = estado
        Me.txtCentroTrabajo.Enabled = estado

        Me.dpModalidad.Enabled = False
        If hdcodigo_cpf.Value <> 9 Then
            Me.dpModalidad.Enabled = estado
        End If

        'Me.txtFechaNac.SkinID = IIf(estado = False, "CajaTextoBloqueado", "CajaTextoObligatorio")
        'Me.dpSexo.SkinID = IIf(estado = False, "ComboBloqueado", "ComboObligatorio")
        'Me.txtemail1.SkinID = IIf(estado = False, "CajaTextoBloqueado", "CajaTextoObligatorio")
        'Me.txtemail2.SkinID = IIf(estado = False, "CajaTextoBloqueado", "CajaTextoObligatorio")
        'Me.txtdireccion.SkinID = IIf(estado = False, "CajaTextoBloqueado", "CajaTextoObligatorio")
        'Me.dpdepartamento.SkinID = IIf(estado = False, "ComboBloqueado", "ComboObligatorio")
        'Me.dpprovincia.SkinID = IIf(estado = False, "ComboBloqueado", "ComboObligatorio")
        'Me.dpdistrito.SkinID = IIf(estado = False, "ComboBloqueado", "ComboObligatorio")
        'Me.txttelefono.SkinID = IIf(estado = False, "CajaTextoBloqueado", "CajaTextoObligatorio")
        'Me.txtcelular.SkinID = IIf(estado = False, "CajaTextoBloqueado", "CajaTextoObligatorio")
        'Me.dpEstadoCivil.SkinID = IIf(estado = False, "ComboBloqueado", "ComboObligatorio")
        'Me.txtruc.SkinID = IIf(estado = False, "CajaTextoBloqueado", "CajaTextoObligatorio")
        'Me.dpModalidad.SkinID = IIf(estado = False, "ComboBloqueado", "ComboObligatorio")
    End Sub
    Private Sub LimpiarCajas()
        Me.txtAPaterno.Text = ""
        Me.txtAMaterno.Text = ""
        Me.txtNombres.Text = ""
        Me.txtFechaNac.Text = ""
        Me.dpSexo.SelectedValue = -1
        'Me.dpTipoDoc.SelectedValue = -1
        Me.txtemail1.Text = ""
        Me.txtemail2.Text = ""
        Me.txtdireccion.Text = ""
        'Me.dpdepartamento.SelectedValue = -1
        Me.dpprovincia.SelectedValue = -1
        Me.dpdistrito.SelectedValue = -1
        Me.txttelefono.Text = ""
        Me.txtcelular.Text = ""
        Me.dpEstadoCivil.SelectedValue = -1
        Me.txtruc.Text = ""
        If hdcodigo_cpf.Value <> 9 Then
            Me.dpModalidad.SelectedValue = -1
        End If
    End Sub
    Protected Sub grwCoincidencias_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwCoincidencias.SelectedIndexChanged
        Me.hdcodigo_pso.Value = Me.grwCoincidencias.DataKeys.Item(Me.grwCoincidencias.SelectedIndex).Values(0)
        Me.DesbloquearOtrosDatos(True)
        Me.BuscarPersona("COE", Me.hdcodigo_pso.Value, True)
        trConcidencias.Visible = False
        lnkComprobarNombres.Text = "Clic aquí para buscar coincidencias"
    End Sub

    Protected Sub lnkComprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkComprobarNombres.Click
        Try
            Me.hdcodigo_pso.Value = 0
            If ValidarNroIdentidad() = True Then
                If lnkComprobarNombres.Text = "Clic aquí para buscar coincidencias" Then
                    trConcidencias.Visible = False
                    DesbloquearOtrosDatos(False)
                    Me.lblmensaje.Text = ""
                    Me.cmdGuardar.Enabled = False
                    'Si es peruano debe validar que ingrese los 2 apellidos
                    If Me.dpTipoDoc.SelectedIndex = 0 And Me.txtAPaterno.Text.Trim.Length < 3 And Me.txtAMaterno.Text.Trim.Length < 3 Then
                        Me.lblmensaje.Text = "Debe ingresar el apellido paterno o materno de la persona a registrar y luego [buscar coincidencias]"
                        Exit Sub
                    ElseIf Me.dpTipoDoc.SelectedIndex = 1 And Me.txtAPaterno.Text.Trim.Length < 3 Then
                        Me.lblmensaje.Text = "Debe ingresar el apellido paterno de la persona a registrar y luego [buscar coincidencias]"
                        Exit Sub
                    End If

                    Dim obj As New ClsConectarDatos
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    obj.AbrirConexion()
                    Me.grwCoincidencias.DataSource = obj.TraerDataTable("PERSON_ConsultarPersona", "APE", Me.txtAPaterno.Text.Trim & " " & Me.txtAMaterno.Text.Trim)
                    Me.grwCoincidencias.DataBind()
                    obj.CerrarConexion()
                    obj = Nothing
                    If grwCoincidencias.Rows.Count > 0 Then
                        Me.grwCoincidencias.Visible = True
                        Me.lblmensaje.Text = "Se encontraron coincidencias de la persona"
                        trConcidencias.Visible = True
                        Me.lnkComprobarNombres.Text = "[Clic aquí si está seguro que es una persona nueva]"
                    Else
                        Me.grwCoincidencias.Visible = False
                        Me.ProcederARegistarlo(True)
                    End If
                Else
                    Me.ProcederARegistarlo(True)
                    trConcidencias.Visible = False
                    If Me.txtFechaNac.Enabled = True Then
                        Me.txtFechaNac.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            Me.lblmensaje.Text = "Error: " & ex.Message
        End Try
    End Sub
    Private Sub ProcederARegistarlo(ByVal Si As Boolean)
        Me.lblmensaje.Text = "No se encontraron coincidencias de la persona. Puede proceder a registrarlo."
        Me.lblmensaje.Visible = Si
        Me.DesbloquearOtrosDatos(Si)
        Me.cmdGuardar.Enabled = Si
    End Sub

    Protected Sub lnkComprobarDNI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkComprobarDNI.Click
        LimpiarCajas()
        Me.hdcodigo_pso.Value = 0
        If ValidarNroIdentidad() = True Then
            lnkComprobarNombres.Text = "Clic aquí para buscar coincidencias"
            BuscarPersona("DNIE", Me.txtdni.Text.Trim)
        Else
            DesbloquearNombres(False)
            DesbloquearOtrosDatos(False)
        End If
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Response.Redirect("pec/lstinscritoseventocargo.aspx?mod=" & Request.QueryString("mod") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cco=" & Request.QueryString("cco"))
    End Sub
    Protected Sub cmdLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLimpiar.Click
        Me.hdcodigo_pso.Value = 0
        Me.lblmensaje.Text = ""
        Me.dpdepartamento.SelectedValue = -1
        Me.dpprovincia.Items.Clear()
        Me.dpdistrito.Items.Clear()
        Me.lnkComprobarNombres.Visible = False
        Me.grwDeudas.Visible = False
        Me.grwCoincidencias.Visible = False
        Me.LimpiarCajas()
        Me.txtdni.Text = ""
        Me.txtdni.Enabled = True
        Me.txtdni.Focus()
    End Sub
    Private Function ValidarNroIdentidad(Optional ByVal IrAlFoco As Boolean = True) As Boolean
        'Limpiar txt
        Me.lblmensaje.Text = ""
        Me.lblmensaje.Visible = False
        'Validar DNI
        If Me.dpTipoDoc.SelectedIndex = 0 Then
            If Me.txtdni.Text.Length <> 8 OrElse IsNumeric(Me.txtdni.Text.Trim) = False OrElse Me.txtdni.Text = "00000000" Then
                Me.lblmensaje.Text = "El número de DNI es incorrecto. Mínimo 8 caracteres"
                Me.lblmensaje.Visible = True
                ValidarNroIdentidad = False
                If IrAlFoco = True Then Me.txtdni.Focus()
                'Response.Write(1)
            Else
                'Response.Write(2)
                ValidarNroIdentidad = True
            End If
        ElseIf Me.dpTipoDoc.SelectedIndex = 2 And Me.txtdni.Text.Length < 9 Then
            Me.lblmensaje.Text = "El número de pasaporte es incorrecto. Mínimo 9 caracteres"
            Me.lblmensaje.Visible = True
            ValidarNroIdentidad = False
            If IrAlFoco = True Then Me.txtdni.Focus()
            'Response.Write(3)
        Else
            'Response.Write(4)
            'Me.lblmensaje.Text = "OK -"
            ValidarNroIdentidad = True
        End If
    End Function

    Protected Sub dpTipoDoc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpTipoDoc.SelectedIndexChanged
        'Si es Peruano, carga combos Dpto, Distrito y Provincia
        If Me.dpTipoDoc.SelectedIndex = 0 Then
            MuestraCombosDptoDistritoProvincia()
        End If

        'Si es Extranjero, carga como País
        If Me.dpTipoDoc.SelectedIndex = 1 Or Me.dpTipoDoc.SelectedIndex = 2 Then
            MuestraComboPais()
        End If

    End Sub

    Private Sub MuestraComboPais()
        'Si es Extranjero, carga como País
        lblDepartamento.Text = "País"
        dppais.Visible = True
        CompareValidator8.Visible = True
        txtciudad.Visible = True
        lblciudad.Visible = True

        'Oculto los controles correspondientes a DNI
        dpdepartamento.Visible = False
        dpprovincia.Visible = False
        dpdistrito.Visible = False
        lblProvincia.Visible = False
        lblDistrito.Visible = False
        CompareValidator4.Visible = False
        CompareValidator5.Visible = False
        CompareValidator6.Visible = False

    End Sub


    Private Sub MuestraCombosDptoDistritoProvincia()

        'Si es Peruano, carga combos Dpto, Distrito y Provincia
        lblDepartamento.Text = "Departamento"
        dpdepartamento.Visible = True
        dpprovincia.Visible = True
        dpdistrito.Visible = True
        lblProvincia.Visible = True
        lblDistrito.Visible = True
        CompareValidator4.Visible = True
        CompareValidator5.Visible = True
        CompareValidator6.Visible = True

        'Oculto los controles correspondientes a Carné de Extranjería
        dppais.Visible = False
        CompareValidator8.Visible = False
        txtciudad.Visible = False
        lblciudad.Visible = False
    End Sub

    Protected Sub dpdepartamentocolegio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpdepartamentocolegio.SelectedIndexChanged
        Me.dpprovinciacolegio.Items.Clear()
        Me.dpdistritocolegio.Items.Clear()
        Me.dpCodigo_col.Items.Clear()
        If dpdepartamentocolegio.SelectedValue <> -1 Then
            Me.dpprovinciacolegio.Items.Add("--Seleccione--") : Me.dpprovinciacolegio.Items(0).Value = -1
            Me.dpdistritocolegio.Items.Add("--Seleccione--") : Me.dpdistritocolegio.Items(0).Value = -1
            Me.dpCodigo_col.Items.Add("--Seleccione--") : Me.dpCodigo_col.Items(0).Value = -1

            Me.dpprovinciacolegio.Enabled = True
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            'Cargar provincia
            ClsFunciones.LlenarListas(Me.dpprovinciacolegio, obj.TraerDataTable("ConsultarLugares", 3, Me.dpdepartamentocolegio.SelectedValue, 0), "codigo_pro", "nombre_pro", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub

    Protected Sub dpprovinciacolegio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpprovinciacolegio.SelectedIndexChanged
        Me.dpdistritocolegio.Items.Clear()
        Me.dpCodigo_col.Items.Clear()
        If Me.dpprovinciacolegio.SelectedValue <> -1 Then
            Me.dpdistritocolegio.Items.Add("--Seleccione--") : Me.dpdistritocolegio.Items(0).Value = -1
            Me.dpCodigo_col.Items.Add("--Seleccione--") : Me.dpCodigo_col.Items(0).Value = -1

            Me.dpdistritocolegio.Enabled = True
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.dpdistritocolegio, obj.TraerDataTable("ConsultarLugares", 4, Me.dpprovinciacolegio.SelectedValue, 0), "codigo_dis", "nombre_dis", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub

    Protected Sub dpdistritocolegio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpdistritocolegio.SelectedIndexChanged
        CargarColegios()
    End Sub
    Private Sub CargarColegios()
        Me.dpCodigo_col.Items.Clear()
        'Me.imgActualizarColegio.Visible = False
        'Me.lblAgregarColegio.Visible = False
        If Me.dpdistritocolegio.SelectedValue <> -1 Then
            Me.dpCodigo_col.Items.Add("--Seleccione--") : Me.dpCodigo_col.Items(0).Value = -1
            Me.dpCodigo_col.Enabled = True
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            'ClsFunciones.LlenarListas(Me.dpCodigo_col, obj.TraerDataTable("ConsultarLugares", 5, Me.dpdistritocolegio.SelectedValue, 0), "codigo_col", "nombre_col", "--Seleccione--")
            ClsFunciones.LlenarListas(Me.dpCodigo_col, obj.TraerDataTable("PEC_ConsultarInstitucionesEducativasPorUbicacion", "DIS", dpdistritocolegio.SelectedValue, Nothing), "codigo_ied", "nombre_ied", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
            'EnlaceColegios()
        End If
    End Sub

    Protected Sub dpPaisColegio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpPaisColegio.SelectedIndexChanged
        Try

        
            Me.dpdepartamentocolegio.Items.Clear()
            Me.dpdistritocolegio.Items.Clear()
            Me.dpCodigo_col.Items.Clear()

            If Me.dpPaisColegio.SelectedValue <> -1 Then
                Me.dpdistritocolegio.Items.Add("--Seleccione--") : Me.dpdistritocolegio.Items(0).Value = -1
                Me.dpCodigo_col.Items.Add("--Seleccione--") : Me.dpCodigo_col.Items(0).Value = -1

                Me.dpdistritocolegio.Enabled = True
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                ClsFunciones.LlenarListas(Me.dpdepartamentocolegio, obj.TraerDataTable("ConsultarLugares", 2, Me.dpPaisColegio.SelectedValue, 0), "codigo_Dep", "nombre_Dep", "--Seleccione--")
                obj.CerrarConexion()
                obj = Nothing
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
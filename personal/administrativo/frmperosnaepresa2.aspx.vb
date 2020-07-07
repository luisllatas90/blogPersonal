Partial Class administrativo_frmperosnaepresa2    
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            'Dim cco As String = Request.QueryString("cco")
            Session("cco") = Request.QueryString("cco")
            Session("accion") = Request.QueryString("accion")
            Session("pso") = Request.QueryString("pso")
            Session("idusuario") = Request.QueryString("id")
            Session("ctf") = Request.QueryString("ctf")
            Session("tcl") = Request.QueryString("tcl")
            Session("codigo_test") = Request.QueryString("mod")
            Session("cli") = Request.QueryString("mod")

            'If Session("tcl") = "E" And cli <> "" Then
            '    cli = Request.QueryString("cli")
            'End If


            Dim tbl As Data.DataTable
            obj.AbrirConexion()

            '========================================================
            'Ocultar tr que contiene grid de coincidencias. mvillavicencio
            '========================================================
            trConcidencias.Visible = False
            '========================================================

            '========================================================
            'Cargar datos del evento=cco
            '========================================================
            tbl = obj.TraerDataTable("EVE_ConsultarEventos", 0, Session("cco"), 0)
            Me.pnlDatos.Visible = False
            If tbl.Rows.Count > 0 Then
                'Asignar valores a Cajas temporales
                Me.hdcodigo_cpf.Value = tbl.Rows(0).Item("codigo_cpf")
                Me.hdgestionanotas.Value = tbl.Rows(0).Item("gestionanotas_dev")

                'Cargar Modalidad de ingreso
                ClsFunciones.LlenarListas(Me.dpModalidad, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 7, 1, 0, 0), "codigo_min", "nombre_min", "--Seleccione--")

                'Cargar ciclos para preinscribirse
                ClsFunciones.LlenarListas(Me.dpCicloIng_alu, obj.TraerDataTable("ConsultarPreSemetreInscripcion"), "codigo_cac", "descripcion_cac")

                'Cargar Escuelas: TipoEstudio=1; SubTipoEstudio=1
                ClsFunciones.LlenarListas(Me.dpCodigo_cpf, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 2, 2, 1, 0), "codigo_cpf", "nombre_cpf", "--Seleccione--")

                'Cargar Paises del colegio
                ClsFunciones.LlenarListas(Me.dpPaisColegio, obj.TraerDataTable("ConsultarLugares", 1, 0, 0), "codigo_pai", "nombre_pai", "--Seleccione--")
                ClsFunciones.LlenarListas(Me.dpdepartamentocolegio, obj.TraerDataTable("ConsultarLugares", 2, 156, 0), "codigo_dep", "nombre_dep", "--Seleccione--")

                MostrarBusquedaColegios(False)
                Me.dpPaisColegio.SelectedValue = 156
                'Cargar combos del colegio
                Me.dpdepartamentocolegio.Items.Add("--Seleccione--") : Me.dpdepartamentocolegio.Items(0).Value = -1
                Me.dpprovinciacolegio.Items.Add("--Seleccione--") : Me.dpprovinciacolegio.Items(0).Value = -1
                Me.dpdistritocolegio.Items.Add("--Seleccione--") : Me.dpdistritocolegio.Items(0).Value = -1
                Me.dpCodigo_col.Items.Add("--Seleccione--") : Me.dpCodigo_col.Items(0).Value = -1

                'Cargar Departamento
                ClsFunciones.LlenarListas(Me.dpdepartamento, obj.TraerDataTable("ConsultarLugares", 2, 156, 0), "codigo_dep", "nombre_dep", "--Seleccione--")
                Me.dpprovincia.Items.Add("--Seleccione--") : Me.dpprovincia.Items(0).Value = -1
                Me.dpdistrito.Items.Add("--Seleccione--") : Me.dpdistrito.Items(0).Value = -1

                '############################################################################################################
                'Añadido Por mvillavicencio 28/06/2012
                'Cargar Departamento - Lugar nacimiento
                ClsFunciones.LlenarListas(Me.dpdepartamentonac, obj.TraerDataTable("ConsultarLugares", 2, 156, 0), "codigo_dep", "nombre_dep", "--Seleccione--")
                Me.dpprovincianac.Items.Add("--Seleccione--") : Me.dpprovincianac.Items(0).Value = -1
                Me.dpdistritonac.Items.Add("--Seleccione--") : Me.dpdistritonac.Items(0).Value = -1

                'Cargar Departamento - Padre
                ClsFunciones.LlenarListas(Me.dpdepartamentoPadre, obj.TraerDataTable("ConsultarLugares", 2, 156, 0), "codigo_dep", "nombre_dep", "--Seleccione--")
                Me.dpprovinciaPadre.Items.Add("--Seleccione--") : Me.dpprovinciaPadre.Items(0).Value = -1
                Me.dpdistritoPadre.Items.Add("--Seleccione--") : Me.dpdistritoPadre.Items(0).Value = -1

                'Cargar Departamento - Apoderado
                ClsFunciones.LlenarListas(Me.dpdepartamentoApoderado, obj.TraerDataTable("ConsultarLugares", 2, 156, 0), "codigo_dep", "nombre_dep", "--Seleccione--")
                Me.dpprovinciaApoderado.Items.Add("--Seleccione--") : Me.dpprovinciaApoderado.Items(0).Value = -1
                Me.dpdistritoApoderado.Items.Add("--Seleccione--") : Me.dpdistritoApoderado.Items(0).Value = -1

                'Cargar Paises Lugar Nacimiento
                ClsFunciones.LlenarListas(Me.dpPaisNacimiento, obj.TraerDataTable("ConsultarLugares", 1, 0, 0), "codigo_pai", "nombre_pai", "--Seleccione--")

                '############################################################################################################

                'Cargar Años de promoción
                Dim i As Integer
                Me.dpPromocion.Items.Clear()
                Me.dpPromocion.Items.Add("--Seleccione--")
                Me.dpPromocion.Items(0).Value = -1
                For i = (Now.Year + 1) To 1940 Step -1
                    Me.dpPromocion.Items.Add(i)
                Next
                Me.dpPromocion.SelectedValue = -1

                'Si gestiona notas, debe verificar si hay plan de estudios
                'If (IsDBNull(tbl.Rows(0).Item("codigo_pes")) = True Or tbl.Rows(0).Item("codigo_pes").ToString = "") Then
                '    Me.lblmensaje.Text = "No puede registrar participantes en este evento, debido a que no se ha registrado un Plan de Estudios."
                '    Me.lnkComprobarDNI.Visible = False
                '    'Me.lnkComprobarNombres.Visible = False
                '    Me.dpTipoDoc.Enabled = False
                '    Me.cmdGuardar.Enabled = False
                '    Me.DesbloquearNombres(False)
                '    Me.DesbloquearOtrosDatos(False)
                '    Exit Sub
                'End If


                'Else
                '    'Si no gestiona notas, bloquea la modalidad y asigna una por defecto, según codigo_cpf=9
                '    Me.dpModalidad.Enabled = False
                '    Me.dpModalidad.SelectedValue = tbl.Rows(0).Item("codigo_min")
            End If

            '==============================================================
            'Acción Modificar: Cambia datos adicionales, NO DNI y nombres
            '==============================================================
            'If Request.QueryString("accion") = "M" Then
            If Session("accion") = "M" Then
                Me.cmdLimpiar.Visible = False
                Me.lnkComprobarDNI.Visible = False
                Me.lnkComprobarNombres.Visible = False
                'Me.BuscarPersona("COE", Request.QueryString("pso"), True)
                Me.BuscarPersona("COE", Session("pso"), True)
                Me.txtdni.Enabled = False
                Me.dpTipoDoc.Enabled = False
                Me.cmdCancelar.UseSubmitBehavior = False
                Me.cmdCancelar.Attributes.Add("onclick", "self.parent.tb_remove();")

                'Añadir colegios
                Me.lblAgregarColegio.Visible = False
            Else
                Me.hdcodigo_pso.Value = 0
                'Me.lblAgregarColegio.Text = "<a href='pec/frmDatosColegio.aspx?accion=A&box=S&dep=" & Me.dpdepartamentocolegio.SelectedValue & "&pro=" & Me.dpprovinciacolegio.SelectedValue & "&dis=" & Me.dpdistritocolegio.SelectedValue & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&KeepThis=true&TB_iframe=true&height=460&width=700&modal=true' title='Cambiar estado' class='thickbox'>&nbsp;Agregar...<a/>"
                Me.lblAgregarColegio.Text = "<a href='pec/frmDatosColegio.aspx?accion=A&box=S&dep=" & Me.dpdepartamentocolegio.SelectedValue & "&pro=" & Me.dpprovinciacolegio.SelectedValue & "&dis=" & Me.dpdistritocolegio.SelectedValue & "&id=" & Session("idusuario") & "&ctf=" & Session("ctf") & "&KeepThis=true&TB_iframe=true&height=460&width=700&modal=true' title='Cambiar estado' class='thickbox'>&nbsp;Agregar...<a/>"
                Me.cmdCancelar.UseSubmitBehavior = True
                Me.DesbloquearNombres(True)
                Me.DesbloquearOtrosDatos(False)
            End If

            '==============================================================
            'Valida si es Traslado externo: no exigir datos de colegio
            '==============================================================
            'If Request.QueryString("cco") = 2126 Then
            If Session("cco") = 2126 Then
                Me.CompareValidator9.Enabled = False
                Me.CompareValidator10.Enabled = False
                Me.CompareValidator11.Enabled = False
                Me.CompareValidator12.Enabled = False
                Me.CompareValidator13.Enabled = False
            Else
                Me.CompareValidator9.Enabled = True
                Me.CompareValidator10.Enabled = True
                Me.CompareValidator11.Enabled = True
                Me.CompareValidator12.Enabled = True
                Me.CompareValidator13.Enabled = True
            End If
            obj.CerrarConexion()
            obj = Nothing
        End If

        '========================================================
        'Disparar evento de grabar
        '========================================================
        If Request("hide") = 1 Then
            Dim sbMensaje2 As New StringBuilder
            sbMensaje2.Append("<script type='text/javascript'>")
            sbMensaje2.AppendFormat("alert('cco:" & Session("cco") & " | cicloing:" & dpCicloIng_alu.SelectedValue & " | cpf:" & Me.dpCodigo_cpf.SelectedValue & " |modalid:" & dpModalidad.SelectedValue & " | codigotest " & Session("codigo_test") & "|" & CDate(Me.txtFechaNac.Text.Trim) & "')")
            sbMensaje2.Append("</script>")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje2.ToString, False)

            cmdGuardar_Click(sender, e)
        End If
        '========================================================
    End Sub
    Private Sub BuscarPersona(ByVal tipo As String, ByVal valor As String, Optional ByVal mostrardni As Boolean = False)
        Me.lblmensaje.Text = ""
        Me.cmdGuardar.Enabled = True
        Me.imgActualizarColegio.Visible = False
        Me.lblAgregarColegio.Visible = False

        Dim obj As New ClsConectarDatos
        Dim ExistePersona As Boolean = False
        Dim TipoConsultaAlumno As Byte = 1 'Es decir que busque solo por persona
        'Dim cli As String = 0 'se asigna en variable global
        'Try
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tbl As Data.DataTable
        Dim tblalumno As Data.DataTable
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
            'If Request.QueryString("accion") <> "M" Then
            If Session("accion") <> "M" Then
                ' ## Modificado por mvillavicencio 07/05/2012. 
                'Consulta Deudas Pendientes (vencidas y no vencidas). Antes solo las vencidas.

                'Me.grwDeudas.DataSource = obj.TraerDataTable("CAJ_ConsultarDeudasPersona", tbl.Rows(0).Item("codigo_pso"))
                Me.grwDeudas.DataSource = obj.TraerDataTable("CAJ_ConsultarDeudasPendientesPersona", tbl.Rows(0).Item("codigo_pso"))
                Me.grwDeudas.DataBind()
                'ElseIf Request.QueryString("tcl") = "E" And Request.QueryString("cli") <> "" Then
            ElseIf Session("tcl") = "E" And Session("cli") <> "" Then
                TipoConsultaAlumno = 2 'Es decir que modificará al alumno específico
                'cli = Request.QueryString("cli") 'se asigna el valor en variable global
            End If

            '==================================
            'Buscar/Mostrar Datos del alumno EPRE
            '==================================            
            'tblalumno = obj.TraerDataTable("PERSON_ConsultarAlumnoPersona", TipoConsultaAlumno, tbl.Rows(0).Item("codigo_pso"), cli, Request.QueryString("cco"))
            tblalumno = obj.TraerDataTable("PERSON_ConsultarAlumnoPersona", TipoConsultaAlumno, tbl.Rows(0).Item("codigo_pso"), Session("cli"), Session("cco"))

            'Cargar datos del alumno de EPRE: solo para colegio
            If tblalumno.Rows.Count > 0 Then
                'Si es modificación y encuentra alumno, carga datos de cicloingreso, escuela y modalidad
                'If Request.QueryString("accion") = "M" Then
                If Session("accion") = "M" Then
                    Me.dpCicloIng_alu.SelectedValue = tblalumno.Rows(0).Item("codigo_cac").ToString
                    Me.dpCodigo_cpf.SelectedValue = tblalumno.Rows(0).Item("tempcodigo_cpf").ToString
                    Me.dpModalidad.SelectedValue = tblalumno.Rows(0).Item("codigo_min").ToString
                    Me.dpModalidad.Enabled = False
                    '========================================================================
                    'Bloquear Escuela/Ciclo Ingreso/Modalidad: Solo el adminisrtador Cambia
                    '========================================================================
                    If tblalumno.Rows(0).Item("condicion_alu").ToString.Trim = "P" Then
                        'Sólo si es Postulante que pueda modificar la Escuela
                        Me.dpCodigo_cpf.Enabled = True
                        Me.dpCicloIng_alu.Enabled = True

                    End If
                End If

                Me.dpPaisColegio.SelectedValue = tblalumno.Rows(0).Item("codigopaiscolegio").ToString
                If (tblalumno.Rows(0).Item("añoEgresoSec_Dal").ToString <> "") Then
                    Me.dpPromocion.SelectedValue = tblalumno.Rows(0).Item("añoEgresoSec_Dal").ToString
                End If
                Me.chkCentroAplicacion.Checked = CDbl(tblalumno.Rows(0).Item("colegioAplicacion_Alu"))

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
                    End If
                End If
            End If
            tblalumno.Dispose()
            tblalumno = Nothing


            '==================================
            'Buscar Dpto/Prov/Distrito Persona
            '==================================
            If tbl.Rows(0).Item("codigo_dep").ToString <> "" Then
                'Cargar Lista: Provincia y Distrito
                ClsFunciones.LlenarListas(Me.dpprovincia, obj.TraerDataTable("ConsultarLugares", 3, tbl.Rows(0).Item("codigo_dep"), 0), "codigo_pro", "nombre_pro", "--Seleccione--")
                ClsFunciones.LlenarListas(Me.dpdistrito, obj.TraerDataTable("ConsultarLugares", 4, tbl.Rows(0).Item("codigo_pro"), 0), "codigo_dis", "nombre_dis", "--Seleccione--")
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
            If (tbl.Rows(0).Item("fechaNacimiento_Pso") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("fechaNacimiento_Pso").ToString.Trim <> "") Then
                Me.txtFechaNac.Text = CDate(tbl.Rows(0).Item("fechaNacimiento_Pso")).ToShortDateString
            End If

            Me.dpSexo.SelectedValue = -1

            If (tbl.Rows(0).Item("sexo_Pso") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("sexo_Pso").ToString.Trim <> "") Then
                Me.dpSexo.SelectedValue = tbl.Rows(0).Item("sexo_Pso").ToString.ToUpper
            End If

            Me.dpTipoDoc.SelectedValue = tbl.Rows(0).Item("tipoDocIdent_Pso").ToString
            Me.txtemail1.Text = "" & tbl.Rows(0).Item("emailPrincipal_Pso").ToString
            Me.txtemail2.Text = "" & tbl.Rows(0).Item("emailAlternativo_Pso").ToString
            Me.txtdireccion.Text = "" & tbl.Rows(0).Item("direccion_Pso").ToString
            Me.txttelefono.Text = "" & tbl.Rows(0).Item("telefonoFijo_Pso").ToString
            Me.txtcelular.Text = "" & tbl.Rows(0).Item("telefonoCelular_Pso").ToString
            Me.dpEstadoCivil.SelectedValue = -1

            If (tbl.Rows(0).Item("estadoCivil_Pso") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("estadoCivil_Pso").ToString.Trim <> "") Then
                Me.dpEstadoCivil.SelectedValue = tbl.Rows(0).Item("estadoCivil_Pso").ToString.ToUpper
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

            EnlaceColegios()

            If grwDeudas.Rows.Count > 0 Then
                Me.lblmensaje.Text = "No puede registrar a la persona porque tiene deudas PENDIENTES.<br/> Debe coordinar con la Oficina de Pensiones para regularizar su estado."

                Response.Write("<script> alert('No puede registrar a la persona porque tiene deudas PENDIENTES. Debe coordinar con la Oficina de Pensiones para regularizar su estado.')</script>")

                Me.lblmensaje.Visible = True
                Me.grwDeudas.Visible = True
                Me.cmdGuardar.Enabled = False
            Else
                Me.lblmensaje.Visible = False
                Me.lblmensaje.Text = ""
                Me.grwDeudas.Visible = False
                Me.cmdGuardar.Enabled = True
            End If

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
        'Catch ex As Exception
        '    'obj.CerrarConexion()
        '    obj = Nothing
        '    Me.lblmensaje.Text = "Ocurrió un error " & ex.Message
        '    Me.lblmensaje.Visible = True
        'End Try
    End Sub

    Private Function ValidaForm() As Boolean

        If Me.dpTipoDoc.SelectedItem.Text = "DNI" Then
            'If (Me.txtdni.Text.Trim <> "" And Me.txtdni.Text.Trim.Length = 8) Then
            '    Return True
            'Else
            '    Return False
            'End If
        End If



        Return True
    End Function

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click

        If (ValidaForm() = True) Then
            If ValidarNroIdentidad() = True Then
                Me.grwDeudas.Visible = False
                Me.lblmensaje.Text = ""

                Dim codigo_psoNuevo(1) As String
                Dim codigo_cliNuevo(2) As String
                Dim tcl As String = "E"
                Dim cli As Integer = 0
                'Dim id As String = Request.QueryString("id") 'se asigna en variable global
                'Dim cco As String = Request.QueryString("cco") 'se asigna en variable global
                Dim pso As String
                Dim obj As New ClsConectarDatos
                Dim tbl2 As Data.DataTable
                Dim codigouniversitario As String
                Dim password As String

                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                'Try
                'If Request.QueryString("accion") = "A" Then
                If Session("accion") = "A" Then
                    pso = Me.hdcodigo_pso.Value
                Else
                    pso = Session("pso")
                End If

                obj.AbrirConexion()
                '=================================================================
                'Verificar deudas de la persona que existe
                '=================================================================

                ' ## Modificado por mvillavicencio 07/05/2012. 
                'Consulta Deudas Pendientes (vencidas y no vencidas). Antes solo las vencidas.

                'Me.grwDeudas.DataSource = obj.TraerDataTable("CAJ_ConsultarDeudasPersona", pso)
                Me.grwDeudas.DataSource = obj.TraerDataTable("CAJ_ConsultarDeudasPendientesPersona", pso)

                Me.grwDeudas.DataBind()

                If grwDeudas.Rows.Count > 0 Then
                    Me.lblmensaje.Text = "No puede registrar a la persona porque tiene deudas PENDIENTES.<br/> Debe coordinar con la Oficina de Pensiones para regularizar su estado."
                    Me.grwDeudas.Visible = True
                    obj.CerrarConexion()
                    Exit Sub
                End If
                obj.CerrarConexion()

                obj.IniciarTransaccion()
                '=================================================================
                'Grabar a la persona: Aquí se verifica si EXISTE.
                '=================================================================
                obj.Ejecutar("PERSON_Agregarpersona", pso, _
                    UCase(Me.txtAPaterno.Text.Trim), UCase(Me.txtAMaterno.Text.Trim), UCase(Me.txtNombres.Text.Trim), _
                    CDate(Me.txtFechaNac.Text.Trim), Me.dpSexo.SelectedValue, _
                    Me.dpTipoDoc.SelectedValue, Me.txtdni.Text.Trim, _
                    LCase(Me.txtemail1.Text.Trim), LCase(Me.txtemail2.Text.Trim), _
                    UCase(Me.txtdireccion.Text.Trim), Me.dpdistrito.SelectedValue, _
                    Me.txttelefono.Text.Trim, Me.txtcelular.Text.Trim, Me.dpEstadoCivil.SelectedValue, "", _
                    684, 0).copyto(codigo_psoNuevo, 0)


                If codigo_psoNuevo(0).ToString = "0" Then
                    obj.AbortarTransaccion()
                    Me.lblmensaje.Text = "Ocurrió un error al registrar los datos Persona. Contáctese con desarrollosistemas@usat.edu.pe"
                    Me.lblmensaje.Visible = True
                    Exit Sub
                End If

                'Dim sbMensaje2 As New StringBuilder
                'sbMensaje2.Append("<script type='text/javascript'>")
                'sbMensaje2.AppendFormat("alert('cco:" & Session("cco") & " | cicloing:" & dpCicloIng_alu.SelectedValue & " | cpf:" & Me.dpCodigo_cpf.SelectedValue & " |modalid:" & dpModalidad.SelectedValue & " | codigotest " & Session("codigo_test") & "|" & CDate(Me.txtFechaNac.Text.Trim) & "')")
                'sbMensaje2.Append("</script>")
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje2.ToString, False)


                '===============================================================================================================
                'Grabar como ESTUDIANTE: Siempre gestione notas: consultar el tipo de estudio para asignar NIVEL
                '===============================================================================================================
                'Dim codigo_test As Int16 = Request.QueryString("mod") 'se asigna en variable global

                Dim colegio As String
                If dpPaisColegio.SelectedValue <> 156 Then 'Si no es Perú, el colegio es NULO
                    colegio = 1
                Else
                    colegio = Me.dpCodigo_col.SelectedValue
                End If

                'Modificado por mvillavicencio: se creó v2 del procedimiento, para grabar más datos en datosalumno
                obj.Ejecutar("EVE_AgregarParticipanteEPPGestionaNotas_v2", Session("cco"), _
                        Me.dpCicloIng_alu.SelectedValue, dpCodigo_cpf.SelectedValue, _
                        Me.dpModalidad.SelectedValue, _
                        Session("codigo_test"), _
                         UCase(Me.txtAPaterno.Text.Trim), UCase(Me.txtAMaterno.Text.Trim), UCase(Me.txtNombres.Text.Trim), _
                         CDate(Me.txtFechaNac.Text.Trim), Me.dpSexo.SelectedValue, _
                         Me.dpTipoDoc.SelectedValue, Me.txtdni.Text.Trim, _
                         LCase(Me.txtemail1.Text.Trim), LCase(Me.txtemail2.Text.Trim), _
                         UCase(Me.txtdireccion.Text.Trim), Me.dpdistrito.SelectedValue, _
                         Me.txttelefono.Text.Trim, Me.txtcelular.Text.Trim, Me.dpEstadoCivil.SelectedValue, _
                         Me.GeneraClave, _
                         Me.dpPaisColegio.SelectedValue, colegio, Me.dpPromocion.SelectedItem.Text, Me.chkCentroAplicacion.Checked, _
                         Session("idusuario"), codigo_psoNuevo(0), _
                         dpdistritonac.SelectedValue, dpOperador.SelectedValue, _
                         0, 0, 0, _
                         txtAPaternoPadre.Text & " " & txtAMaternoPadre.Text & " " & txtNombresPadre.Text, _
                         txtdireccionPadre.Text, txturbanizacionPadre.Text, dpdistritoPadre.SelectedValue, _
                         txttelefonoPadre.Text, txttelefonooficinaPadre.Text, txtcelularPadre.Text, _
                         dpOperadorPadre.SelectedValue, txtAPaternoApoderado.Text & " " & _
                         txtAMaternoApoderado.Text & " " & txtNombresApoderado.Text, _
                         txtdireccionApoderado.Text, txturbanizacionApoderado.Text, _
                         txttelefonoApoderado.Text, txttelefonooficinaApoderado.Text, _
                         txtcelularApoderado.Text, dpOperadorApoderado.SelectedValue, _
                         txtObservaciones.Text, "0").copyto(codigo_cliNuevo, 0)

                '===============================================================================================================
                'Grabar como ESTUDIANTE: Siempre gestione notas: consultar el tipo de estudio para asignar NIVEL
                '===============================================================================================================
                If Me.dpModalidad.SelectedValue = 14 Or Me.dpModalidad.SelectedValue = 6 _
                    Or Me.dpModalidad.SelectedValue = 3 Or Me.dpModalidad.SelectedValue = 23 Or Me.dpModalidad.SelectedValue = 29 Then
                    obj.Ejecutar("PEC_ActualizarPlanIngresoDirecto", CInt(codigo_cliNuevo(0)), CInt(dpCodigo_cpf.SelectedValue))
                End If
                '===============================================================================================================
                If codigo_cliNuevo(0).ToString = "0" Then
                    obj.AbortarTransaccion()
                    Me.lblmensaje.Text = "Ocurrió un error al registrar los datos del participante. Contáctese con desarrollosistemas@usat.edu.pe"
                    Me.lblmensaje.Visible = True
                    Exit Sub
                End If
                If codigo_cliNuevo(0).ToString = "-1" Then
                    obj.AbortarTransaccion()
                    Me.lblmensaje.Text = "No puede registrar participantes en este Programa, debido a que no se ha registrado un Plan de Estudios."
                    Me.lblmensaje.Visible = True
                    Exit Sub
                End If

                '=================================================================
                'Enviar a su cuenta de correo el codigo universitario y password
                '=================================================================

                If txtemail1.Text <> "" Then

                    Dim obj2 As New ClsConectarDatos
                    obj2.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

                    Response.Write(codigo_psoNuevo(0))
                    Response.Write(Session("cco"))
                    obj2.AbrirConexion()
                    tbl2 = obj2.TraerDataTable("PERSON_ConsultarAlumnoPersona", 0, codigo_psoNuevo(0), Session("cco"), 0)

                    If tbl2.Rows.Count > 0 Then
                        codigouniversitario = tbl2.Rows(0).Item("codigoUniver_Alu")
                        password = tbl2.Rows(0).Item("password_Alu")

                        Response.Write(codigouniversitario)
                        Dim objMail As New ClsMail
                        Dim mensaje As String

                        'Enviar el correo
                        mensaje = "<br><br>Su registro ha sido activado.<br><br> Su código universitario es " & _
                        codigouniversitario & " y su password es " & password & _
                        ". <br><br>Atte.<br><br>Campus Virtual - USAT."

                        'objMail.EnviarMail("campusvirtual@usat.edu.pe", "Escuela Pre Universitaria", txtemail1.Text, "Registro Activado", mensaje, True)
                        objMail.EnviarMail("campusvirtual@usat.edu.pe", "Escuela Pre Universitaria", "monicavm88@gmail.com", "Registro Activado", mensaje, True)
                    End If
                End If

                'Todas las personas se registran como alumnos
                tcl = "E"
                cli = codigo_cliNuevo(0)

                obj.TerminarTransaccion()
                obj = Nothing
                '=================================================================
                'Se ha registrado correctamente.
                '=================================================================

                Me.lblmensaje.Text = "Se han registrado los datos correctamente."

                Me.lblmensaje.Visible = True
                'If Request.QueryString("accion") = "A" Then
                If Session("accion") = "A" Then
                    'Response.Redirect("pec/frmgeneracioncargos.aspx?mod=" & Request.QueryString("mod") & "&id=" & Request.QueryString("id") & "&cco=" & cco & "&tcl=" & tcl & "&cli=" & cli & "&pso=" & codigo_psoNuevo(0) & "&ctf=" & Request.QueryString("ctf"))
                End If

                'Catch ex As Exception
                '    obj.AbortarTransaccion()
                '    Me.lblmensaje.Text = "Ocurrió un Error al Registrar el estado. Intente mas tarde." & Chr(13) & ex.Message
                '    Me.lblmensaje.Visible = True
                '    obj = Nothing
                'End Try
            End If
        Else
            Me.lblmensaje.Text = "Debe ingresar el DNI"
        End If

    End Sub
    Private Function GeneraClave() As String
        Randomize()
        GeneraClave = Right(UCase(Me.txtAPaterno.Text), 1) & _
            Left(UCase(Me.txtNombres.Text), 1) & _
            CInt(Rnd() * 4) & CInt(Rnd() * 5) & CInt(Rnd() * 9) & CInt(Rnd() * 7)
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
        Me.dpprovincia.Items.Clear()
        Me.dpdistrito.Items.Clear()
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
        Me.dpdepartamento.Enabled = estado
        Me.dpprovincia.Enabled = estado
        Me.dpdistrito.Enabled = estado
        Me.txttelefono.Enabled = estado
        Me.txtcelular.Enabled = estado
        Me.dpEstadoCivil.Enabled = estado
        Me.dpPaisColegio.Enabled = estado
        Me.dpdepartamentocolegio.Enabled = estado
        Me.dpprovinciacolegio.Enabled = estado
        Me.dpdistritocolegio.Enabled = estado
        Me.dpCodigo_col.Enabled = estado
        Me.dpPromocion.Enabled = estado
        chkCentroAplicacion.Enabled = estado
        lnkBusquedaAvanzada.Enabled = estado
        Me.imgActualizarColegio.Enabled = estado
        'Me.dpCodigo_cpf.Enabled = estado
        'Me.cboCicloIng_alu.Enabled = estado
        'Me.dpModalidad.Enabled = estado

        'Me.dpModalidad.Enabled = False
        'If hdcodigo_cpf.Value <> 9 Then
        'End If
    End Sub
    Private Sub LimpiarCajas()
        Me.txtAPaterno.Text = ""
        Me.txtAMaterno.Text = ""
        Me.txtNombres.Text = ""
        Me.txtFechaNac.Text = ""
        Me.dpSexo.SelectedValue = -1
        Me.dpTipoDoc.SelectedValue = -1
        Me.txtemail1.Text = ""
        Me.txtemail2.Text = ""
        Me.txtdireccion.Text = ""
        'Me.dpdepartamento.SelectedValue = -1
        Me.dpprovincia.SelectedValue = -1
        Me.dpdistrito.SelectedValue = -1
        Me.txttelefono.Text = ""
        Me.txtcelular.Text = ""
        Me.dpEstadoCivil.SelectedValue = -1
        Me.dpPaisColegio.SelectedValue = 156
        'Me.dpdepartamentocolegio.SelectedValue=-1
        Me.dpprovinciacolegio.SelectedValue = -1
        Me.dpdistritocolegio.SelectedValue = -1
        Me.dpCodigo_col.SelectedValue = -1
        Me.dpPromocion.SelectedValue = -1
        Me.chkCentroAplicacion.Checked = False

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
        Me.hdcodigo_pso.Value = 0
        If ValidarNroIdentidad() = True Then
            If lnkComprobarNombres.Text = "Clic aquí para buscar coincidencias" Then
                trConcidencias.Visible = False
                DesbloquearOtrosDatos(False)
                Me.lblmensaje.Text = ""
                Me.cmdGuardar.Enabled = False
                'Si es peruano debe validar que ingrese los 2 apellidos
                If Me.dpTipoDoc.SelectedIndex = 0 And Me.txtAPaterno.Text.Trim.Length < 2 And Me.txtAMaterno.Text.Trim.Length < 2 Then
                    Me.lblmensaje.Text = "Debe ingresar el apellido paterno o materno de la persona a registrar y luego [buscar coincidencias]"
                    Exit Sub
                ElseIf Me.dpTipoDoc.SelectedIndex = 1 And Me.txtAPaterno.Text.Trim.Length < 2 Then
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
                    trConcidencias.Visible = True 'Agregado por mvillavicencio
                    Me.grwCoincidencias.Visible = True
                    Me.lblmensaje.Text = "Se encontraron coincidencias de la persona"
                    trConcidencias.Visible = True
                    Me.lnkComprobarNombres.Text = "[Clic aquí si está seguro que es una persona nueva]"
                    'Verificar si ya tiene seleccionado un colegio no mostrar busqueda 
                    If dpCodigo_col.SelectedValue > 0 Then
                        MostrarBusquedaColegios(False)
                    End If
                    lnkBusquedaAvanzada.Enabled = True
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

        If Me.txtdni.Text.Length > 0 Then 'Que no valide DNI en blanco
            If ValidarNroIdentidad() = True Then
                BuscarPersona("DNIE", Me.txtdni.Text.Trim)
                'Me.grwDeudas.Visible = False
                Me.grwCoincidencias.Visible = False
                lnkComprobarNombres.Text = "Clic aquí para buscar coincidencias"
            Else
                DesbloquearNombres(False)
                DesbloquearOtrosDatos(False)
                'Verificar si ya tiene seleccionado un colegio no mostrar busqueda 
                If dpCodigo_col.SelectedValue > 0 Then
                    MostrarBusquedaColegios(False)
                End If
                lnkBusquedaAvanzada.Visible = True
            End If
        End If
    End Sub
    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        'Response.Redirect("pec/lstinscritoseventocargo.aspx?mod=" & Request.QueryString("mod") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cco=" & Request.QueryString("cco"))
        Response.Redirect("pec/lstinscritoseventocargo.aspx?mod=" & Session("codigo_test") & "&id=" & Session("idusuario") & "&ctf=" & Session("ctf") & "&cco=" & Session("cco"))
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
            'No validar si el DNI esá vacío
            If Me.txtdni.Text.Length = 0 Then
                ValidarNroIdentidad = True
            ElseIf Me.txtdni.Text.Length <> 8 OrElse IsNumeric(Me.txtdni.Text.Trim) = False OrElse Me.txtdni.Text = "00000000" Then
                Me.lblmensaje.Text = "El número de DNI es incorrecto. Mínimo 8 caracteres"
                Me.lblmensaje.Visible = True
                ValidarNroIdentidad = False
                If IrAlFoco = True Then Me.txtdni.Focus()
                'Response.Write(1)
            Else
                'Response.Write(2)
                ValidarNroIdentidad = True
            End If
        ElseIf Me.dpTipoDoc.SelectedIndex = 1 And Me.txtdni.Text.Length < 9 Then
            Me.lblmensaje.Text = "El número de pasaporte es incorrecto. Mínimo 9 caracteres"
            Me.lblmensaje.Visible = True
            ValidarNroIdentidad = False
            If IrAlFoco = True Then Me.txtdni.Focus()
            'Response.Write(3)
        Else
            'Response.Write(4)
            ValidarNroIdentidad = True
        End If
    End Function
    Protected Sub dpPaisColegio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpPaisColegio.SelectedIndexChanged
        Me.dpdepartamentocolegio.Items.Clear()
        Me.dpprovinciacolegio.Items.Clear()
        Me.dpdistritocolegio.Items.Clear()
        Me.dpCodigo_col.Items.Clear()

        If dpPaisColegio.SelectedValue = 156 Then
            Me.dpdepartamentocolegio.Enabled = True

            Me.dpdepartamentocolegio.Items.Add("--Seleccione--") : Me.dpdepartamentocolegio.Items(0).Value = -1
            Me.dpprovinciacolegio.Items.Add("--Seleccione--") : Me.dpprovinciacolegio.Items(0).Value = -1
            Me.dpdistritocolegio.Items.Add("--Seleccione--") : Me.dpdistritocolegio.Items(0).Value = -1
            Me.dpCodigo_col.Items.Add("--Seleccione--") : Me.dpCodigo_col.Items(0).Value = -1

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            'Cargar Departamento
            ClsFunciones.LlenarListas(Me.dpdepartamentocolegio, obj.TraerDataTable("ConsultarLugares", 2, Me.dpPaisColegio.SelectedValue, 0), "codigo_dep", "nombre_dep", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
        Else
            Me.dpdepartamentocolegio.Enabled = False
            Me.dpprovinciacolegio.Enabled = False
            Me.dpdistritocolegio.Enabled = False
            Me.dpCodigo_col.Enabled = False

            Me.dpdepartamentocolegio.Items.Add("OTROS") : Me.dpdepartamentocolegio.Items(0).Value = -2
            Me.dpprovinciacolegio.Items.Add("OTROS") : Me.dpprovinciacolegio.Items(0).Value = -2
            Me.dpdistritocolegio.Items.Add("OTROS") : Me.dpdistritocolegio.Items(0).Value = -2
            Me.dpCodigo_col.Items.Add("OTROS") : Me.dpCodigo_col.Items(0).Value = -2
        End If
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
        MostrarBusquedaColegios(False)
        pnlDatos.Visible = False
    End Sub
    Private Sub CargarColegios()
        Me.dpCodigo_col.Items.Clear()
        Me.imgActualizarColegio.Visible = False
        Me.lblAgregarColegio.Visible = False
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
            EnlaceColegios()
        End If
    End Sub
    Protected Sub imgActualizarColegio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgActualizarColegio.Click
        CargarColegios()
    End Sub
    Private Sub EnlaceColegios()
        Me.lblAgregarColegio.Visible = False ' True
        Me.imgActualizarColegio.Visible = True
        'Me.lblAgregarColegio.Text = "<a href='pec/frmDatosColegio.aspx?accion=A&box=S&dep=" & Me.dpdepartamentocolegio.SelectedValue & "&pro=" & Me.dpprovinciacolegio.SelectedValue & "&dis=" & Me.dpdistritocolegio.SelectedValue & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&KeepThis=true&TB_iframe=true&height=460&width=700&modal=true' title='Cambiar estado' class='thickbox'>&nbsp;Agregar...<a/>"
        Me.lblAgregarColegio.Text = "<a href='pec/frmDatosColegio.aspx?accion=A&box=S&dep=" & Me.dpdepartamentocolegio.SelectedValue & "&pro=" & Me.dpprovinciacolegio.SelectedValue & "&dis=" & Me.dpdistritocolegio.SelectedValue & "&id=" & Session("idusuario") & "&ctf=" & Session("ctf") & "&KeepThis=true&TB_iframe=true&height=460&width=700&modal=true' title='Cambiar estado' class='thickbox'>&nbsp;Agregar...<a/>"
    End Sub

    Protected Sub ImgBuscarColegios_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarColegios.Click
        BuscarColegios("PEC_ConsultarInstitucionesEducativas", "COL", Me.txtColegio.Text, "grid")
    End Sub

    Sub BuscarColegios(ByVal procedimiento As String, ByVal tipo As String, ByVal parametro As String, ByVal control As String)
        Dim objCnx As New ClsConectarDatos
        Dim objFun As New ClsFunciones
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objCnx.AbrirConexion()
        If control = "grid" Then
            gvColegios.DataSource = objCnx.TraerDataTable(procedimiento, tipo, parametro, Nothing)
            gvColegios.DataBind()
            pnlDatos.Visible = True
        Else
            objFun.CargarListas(dpCodigo_col, objCnx.TraerDataTable(procedimiento, tipo, CInt(parametro)), "codigo_ied", "nombre_ied", "--Seleccione--")
            pnlDatos.Visible = False
        End If
        objCnx.CerrarConexion()
        objCnx = Nothing
    End Sub

    Protected Sub lnkBusquedaAvanzada_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBusquedaAvanzada.Click
        Me.txtColegio.Text = IIf(Me.dpCodigo_col.SelectedValue > 0, Me.dpCodigo_col.SelectedItem.Text, "")
        Me.txtColegio.Focus()
        If lnkBusquedaAvanzada.Text.Trim = "Busqueda Simple" Then
            MostrarBusquedaColegios(False)
            lnkBusquedaAvanzada.Text = "Busqueda Avanzada"
        Else
            MostrarBusquedaColegios(True)
            lnkBusquedaAvanzada.Text = "Busqueda Simple"
        End If
    End Sub

    Private Sub MostrarBusquedaColegios(ByVal valor As Boolean)
        Me.txtColegio.Visible = valor
        Me.ImgBuscarColegios.Visible = valor
        Me.imgActualizarColegio.Visible = False
        Me.lblTextBusqueda.Visible = valor
        Me.dpCodigo_col.Visible = Not (valor)
    End Sub

    Protected Sub gvColegios_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvColegios.SelectedIndexChanged
        Try
            Response.Write(Me.gvColegios.DataKeys.Item(Me.gvColegios.SelectedIndex).Values(0))
            dpdepartamentocolegio.SelectedValue = Me.gvColegios.DataKeys.Item(Me.gvColegios.SelectedIndex).Values(3)
            dpdepartamentocolegio_SelectedIndexChanged(sender, e)
            dpprovinciacolegio.SelectedValue = Me.gvColegios.DataKeys.Item(Me.gvColegios.SelectedIndex).Values(2)
            dpprovinciacolegio_SelectedIndexChanged(sender, e)
            dpdistritocolegio.SelectedValue = Me.gvColegios.DataKeys.Item(Me.gvColegios.SelectedIndex).Values(1)
            BuscarColegios("PEC_ConsultarInstitucionesEducativasPorUbicacion", "DIS", dpdistritocolegio.SelectedValue, "Combo")
            Me.dpCodigo_col.SelectedValue = Me.gvColegios.DataKeys.Item(Me.gvColegios.SelectedIndex).Values(0)
            gvColegios.Dispose()
            lnkBusquedaAvanzada_Click(sender, e)
        Catch ex As Exception
            Me.lblmensaje.Text = "Error: " & ex.Message
        End Try
    End Sub

    '############### Por mvillavicencio 28/06/2012
    Protected Sub dpdepartamentonac_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpdepartamentonac.SelectedIndexChanged
        Me.dpprovincianac.Items.Clear()
        Me.dpdistritonac.Items.Clear()
        If Me.dpdepartamentonac.SelectedValue <> -1 Then
            Me.dpprovincianac.Items.Add("--Seleccione--") : Me.dpprovincianac.Items(0).Value = -1
            Me.dpdistritonac.Items.Add("--Seleccione--") : Me.dpdistritonac.Items(0).Value = -1
            Me.dpprovincianac.Enabled = True
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.dpprovincianac, obj.TraerDataTable("ConsultarLugares", 3, Me.dpdepartamentonac.SelectedValue, 0), "codigo_pro", "nombre_pro", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
        Else
            Me.dpprovincianac.Enabled = False
            Me.dpdistritonac.Enabled = False
        End If
    End Sub

    Protected Sub dpprovincianac_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpprovincianac.SelectedIndexChanged
        Me.dpdistritonac.Items.Clear()
        If Me.dpprovincianac.SelectedValue <> -1 Then
            Me.dpdistritonac.Items.Add("--Seleccione--") : Me.dpdistritonac.Items(0).Value = -1
            Me.dpdistritonac.Enabled = True
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.dpdistritonac, obj.TraerDataTable("ConsultarLugares", 4, Me.dpprovincianac.SelectedValue, 0), "codigo_dis", "nombre_dis", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
        Else
            Me.dpdistritonac.Enabled = False
        End If
    End Sub

    Protected Sub dpdepartamentoPadre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpdepartamentoPadre.SelectedIndexChanged
        Me.dpprovinciaPadre.Items.Clear()
        Me.dpdistritoPadre.Items.Clear()
        If Me.dpdepartamentoPadre.SelectedValue <> -1 Then
            Me.dpprovinciaPadre.Items.Add("--Seleccione--") : Me.dpprovinciaPadre.Items(0).Value = -1
            Me.dpdistritoPadre.Items.Add("--Seleccione--") : Me.dpdistritoPadre.Items(0).Value = -1
            Me.dpprovinciaPadre.Enabled = True
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.dpprovinciaPadre, obj.TraerDataTable("ConsultarLugares", 3, Me.dpdepartamentoPadre.SelectedValue, 0), "codigo_pro", "nombre_pro", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
        Else
            Me.dpprovinciaPadre.Enabled = False
            Me.dpdistritoPadre.Enabled = False
        End If
    End Sub

    Protected Sub dpprovinciaPadre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpprovinciaPadre.SelectedIndexChanged
        Me.dpdistritoPadre.Items.Clear()
        If Me.dpprovinciaPadre.SelectedValue <> -1 Then
            Me.dpdistritoPadre.Items.Add("--Seleccione--") : Me.dpdistritoPadre.Items(0).Value = -1
            Me.dpdistritoPadre.Enabled = True
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.dpdistritoPadre, obj.TraerDataTable("ConsultarLugares", 4, Me.dpprovinciaPadre.SelectedValue, 0), "codigo_dis", "nombre_dis", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
        Else
            Me.dpdistritoPadre.Enabled = False
        End If
    End Sub

    Protected Sub dpdepartamentoApoderado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpdepartamentoApoderado.SelectedIndexChanged
        Me.dpprovinciaApoderado.Items.Clear()
        Me.dpdistritoApoderado.Items.Clear()
        If Me.dpdepartamentoApoderado.SelectedValue <> -1 Then
            Me.dpprovinciaApoderado.Items.Add("--Seleccione--") : Me.dpprovinciaApoderado.Items(0).Value = -1
            Me.dpdistritoApoderado.Items.Add("--Seleccione--") : Me.dpdistritoApoderado.Items(0).Value = -1
            Me.dpprovinciaApoderado.Enabled = True
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.dpprovinciaApoderado, obj.TraerDataTable("ConsultarLugares", 3, Me.dpdepartamentoApoderado.SelectedValue, 0), "codigo_pro", "nombre_pro", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
        Else
            Me.dpprovinciaApoderado.Enabled = False
            Me.dpdistritoApoderado.Enabled = False
        End If
    End Sub

    Protected Sub dpprovinciaApoderado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpprovinciaApoderado.SelectedIndexChanged
        Me.dpdistritoApoderado.Items.Clear()
        If Me.dpprovinciaApoderado.SelectedValue <> -1 Then
            Me.dpdistritoApoderado.Items.Add("--Seleccione--") : Me.dpdistritoApoderado.Items(0).Value = -1
            Me.dpdistritoApoderado.Enabled = True
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.dpdistritoApoderado, obj.TraerDataTable("ConsultarLugares", 4, Me.dpprovinciaApoderado.SelectedValue, 0), "codigo_dis", "nombre_dis", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
        Else
            Me.dpdistritoApoderado.Enabled = False
        End If
    End Sub

    Protected Sub lnkGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkGrabar.Click
        If (ValidaForm() = True) Then
            If ValidarNroIdentidad() = True Then
                Me.grwDeudas.Visible = False
                Me.lblmensaje.Text = ""

                Dim codigo_psoNuevo(1) As String
                Dim codigo_cliNuevo(2) As String
                Dim tcl As String = "E"
                Dim cli As Integer = 0
                'Dim id As String = Request.QueryString("id") 'se asigna en variable global
                'Dim cco As String = Request.QueryString("cco") 'se asigna en variable global
                Dim pso As String
                Dim obj As New ClsConectarDatos
                Dim tbl2 As Data.DataTable
                Dim codigouniversitario As String
                Dim password As String

                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                'Try
                'If Request.QueryString("accion") = "A" Then
                If Session("accion") = "A" Then
                    pso = Me.hdcodigo_pso.Value
                Else
                    pso = Session("pso")
                End If

                obj.AbrirConexion()
                '=================================================================
                'Verificar deudas de la persona que existe
                '=================================================================

                ' ## Modificado por mvillavicencio 07/05/2012. 
                'Consulta Deudas Pendientes (vencidas y no vencidas). Antes solo las vencidas.

                'Me.grwDeudas.DataSource = obj.TraerDataTable("CAJ_ConsultarDeudasPersona", pso)
                Me.grwDeudas.DataSource = obj.TraerDataTable("CAJ_ConsultarDeudasPendientesPersona", pso)

                Me.grwDeudas.DataBind()

                If grwDeudas.Rows.Count > 0 Then
                    Me.lblmensaje.Text = "No puede registrar a la persona porque tiene deudas PENDIENTES.<br/> Debe coordinar con la Oficina de Pensiones para regularizar su estado."
                    Me.grwDeudas.Visible = True
                    obj.CerrarConexion()
                    Exit Sub
                End If
                obj.CerrarConexion()

                obj.IniciarTransaccion()
                '=================================================================
                'Grabar a la persona: Aquí se verifica si EXISTE.
                '=================================================================
                obj.Ejecutar("PERSON_Agregarpersona", pso, _
                    UCase(Me.txtAPaterno.Text.Trim), UCase(Me.txtAMaterno.Text.Trim), UCase(Me.txtNombres.Text.Trim), _
                    CDate(Me.txtFechaNac.Text.Trim), Me.dpSexo.SelectedValue, _
                    Me.dpTipoDoc.SelectedValue, Me.txtdni.Text.Trim, _
                    LCase(Me.txtemail1.Text.Trim), LCase(Me.txtemail2.Text.Trim), _
                    UCase(Me.txtdireccion.Text.Trim), Me.dpdistrito.SelectedValue, _
                    Me.txttelefono.Text.Trim, Me.txtcelular.Text.Trim, Me.dpEstadoCivil.SelectedValue, "", _
                    684, 0).copyto(codigo_psoNuevo, 0)


                If codigo_psoNuevo(0).ToString = "0" Then
                    obj.AbortarTransaccion()
                    Me.lblmensaje.Text = "Ocurrió un error al registrar los datos Persona. Contáctese con desarrollosistemas@usat.edu.pe"
                    Me.lblmensaje.Visible = True
                    Exit Sub
                End If

                'Dim sbMensaje2 As New StringBuilder
                'sbMensaje2.Append("<script type='text/javascript'>")
                'sbMensaje2.AppendFormat("alert('cco:" & Session("cco") & " | cicloing:" & dpCicloIng_alu.SelectedValue & " | cpf:" & Me.dpCodigo_cpf.SelectedValue & " |modalid:" & dpModalidad.SelectedValue & " | codigotest " & Session("codigo_test") & "|" & CDate(Me.txtFechaNac.Text.Trim) & "')")
                'sbMensaje2.Append("</script>")
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje2.ToString, False)


                '===============================================================================================================
                'Grabar como ESTUDIANTE: Siempre gestione notas: consultar el tipo de estudio para asignar NIVEL
                '===============================================================================================================
                'Dim codigo_test As Int16 = Request.QueryString("mod") 'se asigna en variable global

                Dim colegio As String
                If dpPaisColegio.SelectedValue <> 156 Then 'Si no es Perú, el colegio es NULO
                    colegio = 1
                Else
                    colegio = Me.dpCodigo_col.SelectedValue
                End If

                'Modificado por mvillavicencio: se creó v2 del procedimiento, para grabar más datos en datosalumno
                obj.Ejecutar("EVE_AgregarParticipanteEPPGestionaNotas_v2", Session("cco"), _
                        Me.dpCicloIng_alu.SelectedValue, dpCodigo_cpf.SelectedValue, _
                        Me.dpModalidad.SelectedValue, _
                        Session("codigo_test"), _
                         UCase(Me.txtAPaterno.Text.Trim), UCase(Me.txtAMaterno.Text.Trim), UCase(Me.txtNombres.Text.Trim), _
                         CDate(Me.txtFechaNac.Text.Trim), Me.dpSexo.SelectedValue, _
                         Me.dpTipoDoc.SelectedValue, Me.txtdni.Text.Trim, _
                         LCase(Me.txtemail1.Text.Trim), LCase(Me.txtemail2.Text.Trim), _
                         UCase(Me.txtdireccion.Text.Trim), Me.dpdistrito.SelectedValue, _
                         Me.txttelefono.Text.Trim, Me.txtcelular.Text.Trim, Me.dpEstadoCivil.SelectedValue, _
                         Me.GeneraClave, _
                         Me.dpPaisColegio.SelectedValue, colegio, Me.dpPromocion.SelectedItem.Text, Me.chkCentroAplicacion.Checked, _
                         Session("idusuario"), codigo_psoNuevo(0), _
                         dpdistritonac.SelectedValue, dpOperador.SelectedValue, _
                         0, 0, 0, _
                         txtAPaternoPadre.Text & " " & txtAMaternoPadre.Text & " " & txtNombresPadre.Text, _
                         txtdireccionPadre.Text, txturbanizacionPadre.Text, dpdistritoPadre.SelectedValue, _
                         txttelefonoPadre.Text, txttelefonooficinaPadre.Text, txtcelularPadre.Text, _
                         dpOperadorPadre.SelectedValue, txtAPaternoApoderado.Text & " " & _
                         txtAMaternoApoderado.Text & " " & txtNombresApoderado.Text, _
                         txtdireccionApoderado.Text, txturbanizacionApoderado.Text, _
                         txttelefonoApoderado.Text, txttelefonooficinaApoderado.Text, _
                         txtcelularApoderado.Text, dpOperadorApoderado.SelectedValue, _
                         txtObservaciones.Text, "0").copyto(codigo_cliNuevo, 0)

                '===============================================================================================================
                'Grabar como ESTUDIANTE: Siempre gestione notas: consultar el tipo de estudio para asignar NIVEL
                '===============================================================================================================
                If Me.dpModalidad.SelectedValue = 14 Or Me.dpModalidad.SelectedValue = 6 _
                    Or Me.dpModalidad.SelectedValue = 3 Or Me.dpModalidad.SelectedValue = 23 Or Me.dpModalidad.SelectedValue = 29 Then
                    obj.Ejecutar("PEC_ActualizarPlanIngresoDirecto", CInt(codigo_cliNuevo(0)), CInt(dpCodigo_cpf.SelectedValue))
                End If
                '===============================================================================================================
                If codigo_cliNuevo(0).ToString = "0" Then
                    obj.AbortarTransaccion()
                    Me.lblmensaje.Text = "Ocurrió un error al registrar los datos del participante. Contáctese con desarrollosistemas@usat.edu.pe"
                    Me.lblmensaje.Visible = True
                    Exit Sub
                End If
                If codigo_cliNuevo(0).ToString = "-1" Then
                    obj.AbortarTransaccion()
                    Me.lblmensaje.Text = "No puede registrar participantes en este Programa, debido a que no se ha registrado un Plan de Estudios."
                    Me.lblmensaje.Visible = True
                    Exit Sub
                End If

                '=================================================================
                'Enviar a su cuenta de correo el codigo universitario y password
                '=================================================================

                If txtemail1.Text <> "" Then

                    Dim obj2 As New ClsConectarDatos
                    obj2.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

                    Response.Write(codigo_psoNuevo(0))
                    Response.Write(Session("cco"))
                    obj2.AbrirConexion()
                    tbl2 = obj2.TraerDataTable("PERSON_ConsultarAlumnoPersona", 0, codigo_psoNuevo(0), Session("cco"), 0)

                    If tbl2.Rows.Count > 0 Then
                        codigouniversitario = tbl2.Rows(0).Item("codigoUniver_Alu")
                        password = tbl2.Rows(0).Item("password_Alu")

                        Response.Write(codigouniversitario)
                        Dim objMail As New ClsMail
                        Dim mensaje As String

                        'Enviar el correo
                        mensaje = "<br><br>Su registro ha sido activado.<br><br> Su código universitario es " & _
                        codigouniversitario & " y su password es " & password & _
                        ". <br><br>Atte.<br><br>Campus Virtual - USAT."

                        'objMail.EnviarMail("campusvirtual@usat.edu.pe", "Escuela Pre Universitaria", txtemail1.Text, "Registro Activado", mensaje, True)
                        objMail.EnviarMail("campusvirtual@usat.edu.pe", "Escuela Pre Universitaria", "monicavm88@gmail.com", "Registro Activado", mensaje, True)
                    End If
                End If

                'Todas las personas se registran como alumnos
                tcl = "E"
                cli = codigo_cliNuevo(0)

                obj.TerminarTransaccion()
                obj = Nothing
                '=================================================================
                'Se ha registrado correctamente.
                '=================================================================

                Me.lblmensaje.Text = "Se han registrado los datos correctamente."

                Me.lblmensaje.Visible = True
                'If Request.QueryString("accion") = "A" Then
                If Session("accion") = "A" Then
                    'Response.Redirect("pec/frmgeneracioncargos.aspx?mod=" & Request.QueryString("mod") & "&id=" & Request.QueryString("id") & "&cco=" & cco & "&tcl=" & tcl & "&cli=" & cli & "&pso=" & codigo_psoNuevo(0) & "&ctf=" & Request.QueryString("ctf"))
                End If

                'Catch ex As Exception
                '    obj.AbortarTransaccion()
                '    Me.lblmensaje.Text = "Ocurrió un Error al Registrar el estado. Intente mas tarde." & Chr(13) & ex.Message
                '    Me.lblmensaje.Visible = True
                '    obj = Nothing
                'End Try
            End If
        Else
            Me.lblmensaje.Text = "Debe ingresar el DNI"
        End If
    End Sub

End Class

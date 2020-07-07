﻿Partial Class frmpersona
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.dpprovincia.Items.Add("--Seleccione--") : Me.dpprovincia.Items(0).Value = -1
            Me.dpdistrito.Items.Add("--Seleccione--") : Me.dpdistrito.Items(0).Value = -1

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim cco As String = Request.QueryString("cco")
            Dim tbl As Data.DataTable
            obj.AbrirConexion()
            '========================================================
            'Cargar datos del evento=cco
            '========================================================
            tbl = obj.TraerDataTable("EVE_ConsultarEventos", 0, cco, 0)

            If tbl.Rows.Count > 0 Then
                'Asignar valores a Cajas temporales
                Me.hdcodigo_cpf.Value = tbl.Rows(0).Item("codigo_cpf")
                Me.hdgestionanotas.Value = tbl.Rows(0).Item("gestionanotas_dev")

                'Cargar listas Departamento y Modalidad del Centro de Costos
                ClsFunciones.LlenarListas(Me.dpdepartamento, obj.TraerDataTable("ConsultarLugares", 2, 156, 0), "codigo_dep", "nombre_dep", "--Seleccione--")
                'ClsFunciones.LlenarListas(Me.dpModalidad, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 3, cco, 0, 0), "codigo_min", "nombre_min", "--Seleccione--")

                ClsFunciones.LlenarListas(Me.dpModalidad, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 7, Request.QueryString("mod"), 0, 0), "codigo_min", "nombre_min", "--Seleccione--")


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

            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub
    Private Sub BuscarPersona(ByVal tipo As String, ByVal valor As String, Optional ByVal mostrardni As Boolean = False)

        Me.lblmensaje.Text = ""
        Me.cmdGuardar.Enabled = True
        Dim obj As New ClsConectarDatos
        Dim ExistePersona As Boolean = False
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim tbl As Data.DataTable
            Dim tblalumno As Data.DataTable
            obj.AbrirConexion()
            '==================================
            'Buscar a la Persona
            '==================================
            tbl = obj.TraerDataTable("PERSON_ConsultarPersona", tipo, valor)
            If tbl.Rows.Count > 0 Then
                existepersona = True
                '==================================
                'Buscar Deudas de la persona
                '==================================
                If Request.QueryString("accion") <> "M" Then
                    Me.grwDeudas.DataSource = obj.TraerDataTable("CAJ_ConsultarDeudasPersona", tbl.Rows(0).Item("codigo_pso"))
                    Me.grwDeudas.DataBind()
                End If
                '==================================
                'Buscar Datos del alumno
                '==================================
                tblalumno = obj.TraerDataTable("PERSON_ConsultarAlumnoPersona", 0, tbl.Rows(0).Item("codigo_pso"), Request.QueryString("cco"), 0)
                'Cargar datos del alumno
                If tblalumno.Rows.Count > 0 Then
                    Me.dpModalidad.SelectedValue = tblalumno.Rows(0).Item("codigo_min").ToString
                    If hdcodigo_cpf.Value = 9 Then
                        Me.dpModalidad.Enabled = False
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
                If tbl.Rows(0).Item("fechanacimiento_pso").ToString <> "" Then
                    Me.txtFechaNac.Text = CDate(tbl.Rows(0).Item("fechaNacimiento_Pso")).ToShortDateString
                End If

                Me.dpSexo.SelectedValue = -1

                If (tbl.Rows(0).Item("sexo_Pso") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("sexo_Pso").ToString.Trim <> "") Then
                    Me.dpSexo.SelectedValue = tbl.Rows(0).Item("sexo_Pso").ToString.ToUpper
                End If

            Me.dpTipoDoc.SelectedValue = tbl.Rows(0).Item("tipoDocIdent_Pso").ToString
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
            If grwDeudas.Rows.Count > 0 Then
                Me.lblmensaje.Text = "No puede registrar a la persona porque tiene deudas PENDIENTES.<br/> Debe coordinar con la Oficina de Pensiones para regularizar su estado."
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
        Catch ex As Exception
            Me.lblmensaje.Text = "Error " & ex.Message
            Me.lblmensaje.Visible = True
            'obj.CerrarConexion()
            'obj = Nothing
        End Try
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        If ValidarNroIdentidad() = True Then
            Me.grwDeudas.Visible = False
            Me.lblmensaje.Text = ""

            Dim codigo_psoNuevo(1) As String
            Dim codigo_cliNuevo(2) As String
            Dim tcl As String = "E"
            Dim cli As Integer = 0
            Dim id As String = Request.QueryString("id")
            Dim cco As String = Request.QueryString("cco")
            Dim pso As String
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            'Try
            If Request.QueryString("accion") = "A" Then
                pso = Me.hdcodigo_pso.Value
            Else
                pso = Request.QueryString("pso")
            End If

            obj.AbrirConexion()
            '=================================================================
            'Verificar deudas de la persona que existe
            '=================================================================
            Me.grwDeudas.DataSource = obj.TraerDataTable("CAJ_ConsultarDeudasPersona", pso)
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
                Me.txttelefono.Text.Trim, Me.txtcelular.Text.Trim, Me.dpEstadoCivil.SelectedValue, Me.txtruc.Text.ToString, _
                id, "0").copyto(codigo_psoNuevo, 0)

            If codigo_psoNuevo(0).ToString = "0" Then
                obj.AbortarTransaccion()
                Me.lblmensaje.Text = "Ocurrió un error al registrar los datos Persona. Contáctese con desarrollosistemas@usat.edu.pe"
                Exit Sub
            End If

            If CBool(Me.hdgestionanotas.Value) = True Then
                '===============================================================================================================
                'Grabar como ESTUDIANTE: Siempre y cuando gestione notas
                '===============================================================================================================
                obj.Ejecutar("EVE_AgregarParticipanteEventoGestionaNotas", cco, Me.dpModalidad.SelectedValue, _
                         UCase(Me.txtAPaterno.Text.Trim), UCase(Me.txtAMaterno.Text.Trim), UCase(Me.txtNombres.Text.Trim), _
                         CDate(Me.txtFechaNac.Text.Trim), Me.dpSexo.SelectedValue, _
                         Me.dpTipoDoc.SelectedValue, Me.txtdni.Text.Trim, _
                         LCase(Me.txtemail1.Text.Trim), LCase(Me.txtemail2.Text.Trim), _
                         UCase(Me.txtdireccion.Text.Trim), Me.dpdistrito.SelectedValue, _
                         Me.txttelefono.Text.Trim, Me.txtcelular.Text.Trim, Me.dpEstadoCivil.SelectedValue, _
                         Me.GeneraClave, id, codigo_psoNuevo(0), "0").copyto(codigo_cliNuevo, 0)

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
                obj.Ejecutar("EVE_AgregarParticipanteEventoNoGestionaNotas", cco, Me.dpModalidad.SelectedValue, _
                         UCase(Me.txtAPaterno.Text.Trim), UCase(Me.txtAMaterno.Text.Trim), UCase(Me.txtNombres.Text.Trim), _
                         CDate(Me.txtFechaNac.Text.Trim), Me.dpSexo.SelectedValue, _
                         Me.dpTipoDoc.SelectedValue, Me.txtdni.Text.Trim, _
                         LCase(Me.txtemail1.Text.Trim), LCase(Me.txtemail2.Text.Trim), _
                         UCase(Me.txtdireccion.Text.Trim), Me.dpdistrito.SelectedValue, _
                         Me.txttelefono.Text.Trim, Me.txtcelular.Text.Trim, Me.dpEstadoCivil.SelectedValue, _
                         Me.GeneraClave, id, codigo_psoNuevo(0), "E", "0").copyto(codigo_cliNuevo, 0)

                'codigo_cliNuevo(0): es el tipo de cliente: P / E
                'codigo_cliNuevo(1): es el ID de la persona o alumno
                If codigo_cliNuevo(1).ToString = "0" Then
                    obj.AbortarTransaccion()
                    Me.lblmensaje.Text = "Ocurrió un error al registrar los datos del participante. Contáctese con desarrollosistemas@usat.edu.pe"
                    Me.lblmensaje.Visible = True
                    Exit Sub
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

            obj.TerminarTransaccion()
            obj = Nothing
            '=================================================================
            'Se ha registrado correctamente.
            '=================================================================

            Me.lblmensaje.Text = "Se han registrado los datos correctamente."
            Me.lblmensaje.Visible = True
            If Request.QueryString("accion") = "A" Then
                'Page.Request.QueryString.ToString
                Response.Redirect("pec/frmgeneracioncargos.aspx?mod=" & Request.QueryString("mod") & "&id=" & Request.QueryString("id") & "&cco=" & cco & "&tcl=" & tcl & "&cli=" & cli & "&pso=" & codigo_psoNuevo(0))
            End If

            'Catch ex As Exception
            '    obj.AbortarTransaccion()
            '    Me.lblmensaje.Text = "Ocurrió un Error al Registrar el estado. Intente mas tarde." & Chr(13) & ex.Message
            '    Me.lblmensaje.Visible = True
            '    obj = Nothing
            'End Try
        Else
            Me.lblmensaje.Text = "No encontró DNI"
            Me.lblmensaje.Visible = True
        End If
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
        Me.dpdepartamento.Enabled = estado
        Me.dpprovincia.Enabled = estado
        Me.dpdistrito.Enabled = estado
        Me.txttelefono.Enabled = estado
        Me.txtcelular.Enabled = estado
        Me.dpEstadoCivil.Enabled = estado
        Me.txtruc.Enabled = estado

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
End Class
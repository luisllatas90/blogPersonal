﻿
Partial Class academico_frmActualizaDatoAlumno
    Inherits System.Web.UI.Page

    Private Sub CargaModalidadIngreso()
        Dim dtt As New Data.DataTable
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        'Me.cboModalidadIng.DataSource = obj.TraerDataTable("ConsultarModalidadIngreso", "TO", "")

        dtt = obj.TraerDataTable("ConsultarModalidadIngreso", "NV", "")
        If dtt.Rows.Count > 0 Then
            Me.cboModalidadIng.DataSource = dtt
            Me.cboModalidadIng.DataValueField = "codigo_Min"
            Me.cboModalidadIng.DataTextField = "nombre_Min"
            Me.cboModalidadIng.DataBind()
        End If
        obj = Nothing
    End Sub

    Private Sub CargaCicloAcademico()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Me.cboCicloIngreso.DataSource = obj.TraerDataTable("ConsultarCicloAcademico", "TO", "")
        Me.cboCicloIngreso.DataValueField = "descripcion_Cac"
        Me.cboCicloIngreso.DataTextField = "descripcion_Cac"
        Me.cboCicloIngreso.DataBind()
        obj = Nothing
    End Sub

    Private Sub CargaCarreraProfesional()
        'Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        'Dim dtCarreras As Data.DataTable = obj.TraerDataTable("ConsultarCarreraProfesional", "TE", Request.QueryString("mod"))

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dtCarreras As New data.datatable
        dtCarreras = obj.TraerDataTable("ConsultarCarreraProfesional", "TE", Request.QueryString("mod"))

        Me.cboCarreraProf.DataSource = dtCarreras
        Me.cboCarreraProf.DataValueField = "codigo_Cpf"
        Me.cboCarreraProf.DataTextField = "nombre_Cpf"
        Me.cboCarreraProf.DataBind()
        obj = Nothing

        If cboCarreraProf.SelectedIndex <> -1 Then
            CargaPlanEstudios(cboCarreraProf.SelectedValue)
        End If
    End Sub

    Private Sub CargaPlanEstudios(ByVal Carrera As String)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Me.cboPlanEstudio.DataSource = obj.TraerDataTable("ConsultarPlanEstudio", "CP", Carrera, "")
        Me.cboPlanEstudio.DataValueField = "codigo_Pes"
        Me.cboPlanEstudio.DataTextField = "descripcion_Pes"
        Me.cboPlanEstudio.DataBind()
        obj = Nothing
    End Sub

    Private Sub CargaOtrosCombos()
        cboTipoDocumento.Items.Clear()
        cboTipoDocumento.Items.Add("DNI")
        cboTipoDocumento.Items.Add("CARNÉ DE EXTRANJERÍA")
        cboTipoDocumento.Items.Add("PASAPORTE")

        cboSexo.Items.Clear()
        cboSexo.Items.Add("FEMENINO")
        cboSexo.Items.Add("MASCULINO")

        cboEstado.Items.Clear()
        cboEstado.Items.Add("ACTIVO")
        cboEstado.Items.Add("INACTIVO")

        cboCondicion.Items.Clear()
        cboCondicion.Items.Add("INGRESANTE")
        cboCondicion.Items.Add("POSTULANTE")
    End Sub

    Private Sub BuscaAlumno()

        'Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Dim dt As Data.DataTable
        Try
            CargarMaxL()
            'Response.Write(">>>" & Me.txtCodUniversitario_Buscar.Text)
            'dt = obj.TraerDataTable("ConsultarAlumno", "CU", Me.txtCodUniversitario_Buscar.Text)
            dt = obj.TraerDataTable("ConsultarAlumno_ActualizaDatos", Me.txtCodUniversitario_Buscar.Text)
            With dt
                If (.Rows.Count > 1) Then
                    Response.Write("<script>alert('Existe más 1 persona relacionado con ese codigo')</script>")
                Else

                    If (.Rows.Count < 1) Then
                        Response.Write("<script>alert('No se encontró al usuario')</script>")
                        Me.txtCodUniversitario_Buscar.Focus()

                    Else
                        If (dt.Rows(0).Item("codigo_test").ToString = Request.QueryString("mod")) Then
                            hfCodAlu.Value = dt.Rows(0).Item("codigo_Alu").ToString

                            txtApellidoPat.Text = dt.Rows(0).Item("apellidoPat_Alu").ToString
                            txtApellidoMat.Text = dt.Rows(0).Item("apellidoMat_Alu").ToString
                            txtNombres.Text = dt.Rows(0).Item("nombres_Alu").ToString
                            cboEstado.SelectedIndex = IIf(dt.Rows(0).Item("estadoActual_Alu").ToString.Trim = "1", 0, 1)

                            If (dt.Rows(0).Item("fechaNacimiento_Alu").ToString <> "") Then
                                txtFecha.Text = Date.Parse(dt.Rows(0).Item("fechaNacimiento_Alu")).ToString("dd/MM/yyyy")
                            End If
                            cboSexo.SelectedIndex = IIf(dt.Rows(0).Item("sexo_Alu") = "M", 1, 0)
                            If (dt.Rows(0).Item("tipoDocIdent_Alu").ToString.Trim = "DNI" Or _
                                dt.Rows(0).Item("tipoDocIdent_Alu").ToString.Trim = "CARNÉ DE EXTRANJERÍA" Or _
                                dt.Rows(0).Item("tipoDocIdent_Alu").ToString.Trim = "PASAPORTE") Then
                                cboTipoDocumento.SelectedValue = dt.Rows(0).Item("tipoDocIdent_Alu").ToString
                            Else
                                cboTipoDocumento.SelectedIndex = -1
                            End If

                            txtNroDocumento.Text = dt.Rows(0).Item("nroDocIdent_Alu").ToString
                            cboCicloIngreso.SelectedValue = dt.Rows(0).Item("cicloIng_Alu").ToString
                            txtEmail.Text = dt.Rows(0).Item("eMail_Alu").ToString
                            cboCondicion.SelectedValue = IIf(dt.Rows(0).Item("condicion_Alu").ToString.Trim = "I", "INGRESANTE", "POSTULANTE")
                            cboCondicion_SelectedIndexChanged(Nothing, Nothing)


                            'Dim dtCarreras As Data.DataTable = obj.TraerDataTable("ConsultarCarreraProfesional", "TE", Request.QueryString("mod"))
                            Dim dtCarreras As New Data.DataTable
                            dtCarreras = obj.TraerDataTable("ConsultarCarreraProfesional", "TE", Request.QueryString("mod"))

                            Me.cboCarreraProf.DataSource = dtCarreras
                            Me.cboCarreraProf.DataValueField = "codigo_Cpf"
                            Me.cboCarreraProf.DataTextField = "nombre_Cpf"
                            Me.cboCarreraProf.DataBind()


                            Dim verifica As Integer = 0

                            For j As Integer = 0 To cboCarreraProf.Items.Count - 1
                                If cboCarreraProf.Items(j).Value = dt.Rows(0).Item("codigo_Cpf") Then
                                    verifica = 1
                                End If
                            Next

                            If verifica = 1 Then

                                cboCarreraProf.SelectedValue = dt.Rows(0).Item("codigo_Cpf")

                                Dim verifica_vigencia As Integer = 0
                                For k As Integer = 0 To dtCarreras.Rows.Count - 1
                                    If dtCarreras.Rows(k).Item("codigo_cpf") = dt.Rows(0).Item("codigo_Cpf") Then
                                        If dtCarreras.Rows(k).Item("vigencia_cpf").ToString = True Then
                                            verifica_vigencia = 1
                                        End If
                                        'Response.Write("<script>alert('" + dtCarreras.Rows(k).Item("vigencia_cpf").ToString + "')</script>")
                                    End If
                                Next

                                If verifica_vigencia = 0 Then
                                    'No Vigente
                                    Me.lblCarreraVigencia.Text = "LA CARRERA PROFESIONAL NO SE ENCUENTRA VIGENTE"
                                Else ' Vigente
                                    Me.lblCarreraVigencia.Text = ""
                                End If
                                Me.hdvigencia.Value = 1
                            Else
                                cboCarreraProf.Items.Add(New ListItem(dt.Rows(0).Item("nombre_Cpf"), dt.Rows(0).Item("codigo_Cpf")))
                                cboCarreraProf.SelectedValue = dt.Rows(0).Item("codigo_Cpf")
                                Me.lblCarreraVigencia.Text = "LA CARRERA PROFESIONAL NO SE ENCUENTRA VIGENTE"
                                Me.hdvigencia.Value = 0
                            End If


                            CargaPlanEstudios(cboCarreraProf.SelectedValue)

                            cboPlanEstudio.SelectedValue = dt.Rows(0).Item("codigo_Pes")
                            chkAplicacion.Checked = dt.Rows(0).Item("colegioAplicacion_Alu").ToString.Trim

                            'Modalidad de Ingreso
                            cboModalidadIng.SelectedValue = dt.Rows(0).Item("codigo_Min")

                            txtObs.Text = dt.Rows(0).Item("observacion_Dal")
                            txtTelefonoCasa.Text = dt.Rows(0).Item("telefonoCasa_Dal")
                            txtTelefonoMovil.Text = dt.Rows(0).Item("telefonoMovil_Dal")

                            btnBitacoraObservaciones.Visible = True

                            '==================================================================================================================
                            'min_fechamat: Cuando es traslado externo se trae el registro de fecha de ultima mat de la institucion del traslado
                            '==================================================================================================================
                            Me.txtfechamat.Text = IIf(dt.Rows(0).Item("min_fechamat").ToString = "", "", dt.Rows(0).Item("min_fechamat"))
                            If dt.Rows(0).Item("codigo_Min") = 2 Then
                                Me.trfechamat.Visible = True
                            Else
                                Me.trfechamat.Visible = False
                            End If



                            '========================================================================
                            'Marcar categorias y beneficios asignados para ese alumno 21-09-12
                            '========================================================================

                            Dim tblcategoria As Data.DataTable
                            Dim tblbeneficio As Data.DataTable
                            Dim sbMensaje2 As New StringBuilder
                            tblcategoria = obj.TraerDataTable("EPRE_ConsultarCategoriasPostulacionAlumno", dt.Rows(0).Item("codigo_Alu"))

                            sbMensaje2.Append("<script type='text/javascript'>")
                            sbMensaje2.AppendFormat("LimpiarItems('{0}','{1}');", lstMultipleValues2.ClientID, txtSelectedMLValues2.ClientID)
                            If tblcategoria.Rows.Count() > 0 Then
                                For i As Integer = 0 To tblcategoria.Rows.Count() - 1
                                    sbMensaje2.AppendFormat("MarcarItems('{0}','{1}','{2}');", lstMultipleValues2.ClientID, txtSelectedMLValues2.ClientID, tblcategoria.Rows(i).Item("descripcion_cap"))
                                Next
                            Else
                                sbMensaje2.AppendFormat("MarcarItems('{0}','{1}','{2}');", lstMultipleValues2.ClientID, txtSelectedMLValues2.ClientID, "--Sin Categoría--")
                            End If

                            tblbeneficio = obj.TraerDataTable("EPRE_ConsultarBeneficiosPostulacionAlumno", dt.Rows(0).Item("codigo_alu"))

                            sbMensaje2.AppendFormat("LimpiarItems('{0}','{1}');", lstMultipleValues.ClientID, txtSelectedMLValues.ClientID)

                            If tblbeneficio.Rows.Count() > 0 Then
                                For i As Integer = 0 To tblbeneficio.Rows.Count() - 1
                                    sbMensaje2.AppendFormat("MarcarItems('{0}','{1}','{2}');", lstMultipleValues.ClientID, txtSelectedMLValues.ClientID, tblbeneficio.Rows(i).Item("descripcion_bp"))
                                Next
                            Else
                                sbMensaje2.AppendFormat("MarcarItems('{0}','{1}','{2}');", lstMultipleValues.ClientID, txtSelectedMLValues.ClientID, "--Sin Beneficio--")
                            End If

                            sbMensaje2.Append("</script>")
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje2.ToString, False)
                            '========================================================================

                            'Alcanzó vacante - andy.diaz 28-12-2018
                            chkAlcanzoVacante.Checked = dt.Rows(0).Item("alcanzo_vacante").ToString.Trim
                            'Eliminado - andy.diaz 29-10-2019
                            chkEliminado.Checked = dt.Rows(0).Item("eliminado_Alu").ToString.Trim

                            ViewState("origEliminadoAlu") = chkEliminado.Checked
                            ViewState("origNombreCpf") = cboCarreraProf.SelectedItem.Text
                            ViewState("origNombreMin") = cboModalidadIng.SelectedItem.Text

                            Dim codigoAlu As Integer = dt.Rows(0).Item("codigo_alu")
                            Dim tipoConsulta As String = "F"
                            Dim dtDatosAlumno As Data.DataTable = obj.TraerDataTable("ADM_ConsultarAlumnoInscripcion", codigoAlu, tipoConsulta)

                            If dtDatosAlumno.Rows.Count > 0 Then
                                ViewState("origPrecioCredito") = dtDatosAlumno.Rows(0).Item("precioCreditoAct_Alu")
                            End If
                            
                        Else
                            InitControlesForm()
                            If Request.QueryString("mod") = "2" Then
                                Response.Write("<script>alert('El alumno no es estudiante de Pre Grado')</script>")
                            ElseIf Request.QueryString("mod") = "3" Then
                                Response.Write("<script>alert('El alumno no es estudiante de Profesionalización')</script>")
                            ElseIf Request.QueryString("mod") = "10" Then
                                Response.Write("<script>alert('El alumno no es estudiante de GO')</script>")
                            Else
                                Response.Write("<script>alert('El alumno no pertenece a este tipo de estudio')</script>")
                            End If
                        End If
                    End If
                End If
            End With
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Error al buscar datos del alumno')</script>")
        End Try
    End Sub

    Private Function VerificaIngresoDatos(ByVal codigoAlu As Integer) As Boolean
        Try
            If (txtApellidoMat.Text.Trim = "") Then
                Response.Write("<script>alert('Debe ingresar el apellido materno')</script>")
                txtApellidoMat.Focus()
                Return False
            End If

            If (txtApellidoPat.Text.Trim = "") Then
                Response.Write("<script>alert('Debe ingresar el apellido paterno')</script>")
                txtApellidoPat.Focus()
                Return False
            End If

            If (txtNombres.Text.Trim = "") Then
                Response.Write("<script>alert('Debe ingresar nombres')</script>")
                txtNombres.Focus()
                Return False
            End If

            If (txtFecha.Text.Trim = "") Then
                Response.Write("<script>alert('Debe ingresar la fecha de nacimiento')</script>")
                txtFecha.Focus()
                Return False
            End If

            If txtFecha.Text.Trim.Length <> 10 Then
                Response.Write("<script>alert('Error al digitar la fecha de nacimiento, formato: [01/01/1999]')</script>")
                txtFecha.Focus()
                Return False
            End If

            'If (txtfechamat.Text.Trim = "") Then
            '    Response.Write("<script>alert('Debe ingresar la Fecha Primera Matricula ')</script>")
            '    txtfechamat.Focus()
            '    Return False
            'End If



            If (txtNroDocumento.Text.Trim = "") Then
                Response.Write("<script>alert('Debe ingresar el tipo de documento')</script>")
                txtNroDocumento.Focus()
                Return False
            End If

            If (ViewState("eliminado_alu") <> chkEliminado.Checked) And chkEliminado.Checked Then
                'Se está eliminando a un alumno, tengo que validar que no tenga deudas pendientes
                Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
                Dim dtDeudas As Data.DataTable = obj.TraerDataTable("consultarDeudasPendientesEstudiantes", "1", codigoAlu)

                If dtDeudas.Rows.Count > 0 Then
                    Response.Write("<script>alert('El alumno tiene deudas pendientes, para eliminarlo debe anular primero sus deudas.')</script>")
                    chkEliminado.Focus()
                    obj = Nothing
                    Return False
                End If
            End If

            'Verifico que se haya seleccionado un plan de estudios vigente
            'Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            'Dim dtPlanEstudio As Data.DataTable = obj.TraerDataTable("ConsultarPlanEstudio", "CO", cboPlanEstudio.SelectedValue, "")

            'If dtPlanEstudio.Rows.Count > 0 Then
            '    Dim vigenciaPes As Boolean = dtPlanEstudio.Rows(0).Item("vigencia_Pes")
            '    Dim defectoPes As Boolean = dtPlanEstudio.Rows(0).Item("defecto_Pes")

            '    If Not vigenciaPes OrElse Not defectoPes Then
            '        Response.Write("<script>alert('El plan seleccionado no se encuentra vigente o no es el plan por defecto')</script>")
            '        txtFecha.Focus()
            '        obj = Nothing
            '        Return False
            '    End If
            'End If

            'obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Error al verificar los datos del formulario')</script>")
            Return False
        End Try
        Return True
    End Function

    Private Function ActualizaDatosAlumnos() As Boolean
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim Resultado As Integer = -1

        Dim id As String
        Try
            If ValidarSeleccionGrid() Then
                'id = Request.QueryString("id")
                id = Session("id_per")

                Dim estadoActual As Integer = 1
                Select Case cboEstado.SelectedIndex
                    Case 0
                        estadoActual = 1
                    Case 1
                        estadoActual = 0
                    Case 2
                        estadoActual = -1
                End Select

                Resultado = obj.Ejecutar("CAJ_ModificarAlumno_v5" _
                            , hfCodAlu.Value _
                            , txtApellidoPat.Text.ToUpper _
                            , txtApellidoMat.Text.ToUpper _
                            , txtNombres.Text.ToUpper _
                            , estadoActual _
                            , txtFecha.Text _
                            , cboSexo.SelectedValue _
                            , cboTipoDocumento.SelectedValue _
                            , txtNroDocumento.Text _
                            , cboCicloIngreso.SelectedValue _
                            , txtEmail.Text.Trim _
                            , cboModalidadIng.SelectedValue _
                            , cboCondicion.SelectedValue.Substring(0, 1) _
                            , Integer.Parse(cboPlanEstudio.SelectedValue) _
                            , chkAplicacion.Checked _
                            , Integer.Parse(id) _
                            , UCase(txtObs.Text) _
                            , Request.UserHostAddress _
                            , CType(txtSelectedMLValues2.Value, String) _
                            , CType(txtSelectedMLValues.Value, String) _
                            , txtTelefonoCasa.Text _
                            , txtTelefonoMovil.Text _
                            , chkAlcanzoVacante.Checked _
                            , chkEliminado.Checked) 'Eliminado - andy.diaz 29/10/2019


                '========================================================================================
                'Grabar fecha primera mat (de la otra institucion) si es tralado externo . epena 04/10/17
                '========================================================================================

                If cboModalidadIng.SelectedValue = 2 Then

                    obj.Ejecutar("CAJ_ModificarAlumno_v4_upd" _
                            , hfCodAlu.Value _
                            , txtfechamat.Text)

                End If



                '=================================================================
                'Grabar categorias y beneficios. mvillavicencio 21/09/12
                '=================================================================

                'Eliminar todos registros
                obj.Ejecutar("EPRE_BorrarCategoriasPostulacionAlumno", hfCodAlu.Value)
                obj.Ejecutar("EPRE_BorrarBeneficiosPostulacionAlumno", hfCodAlu.Value)

                If CType(txtSelectedMLValues2.Value, String) <> "" Then
                    'Insertar items
                    Dim tabla() As String
                    tabla = Split(txtSelectedMLValues2.Value, ",")

                    For i As Integer = 0 To UBound(tabla)
                        obj.Ejecutar("EPRE_GuardarCategoriaPostulacion", hfCodAlu.Value, tabla(i))
                    Next

                End If

                If CType(txtSelectedMLValues.Value, String) <> "" Then
                    'Insertar items
                    Dim tabla2() As String
                    tabla2 = Split(txtSelectedMLValues.Value, ",")

                    For i As Integer = 0 To UBound(tabla2)
                        obj.Ejecutar("EPRE_GuardarBeneficioPostulacion", hfCodAlu.Value, tabla2(i))
                    Next
                End If
                '=================================================================
                'Dim dtCarreras As Data.DataTable = CType(Session("cboCarrera"), Data.DataTable)
                'Me.cboCarreraProf.DataSource = dtCarreras
                'Me.cboCarreraProf.DataBind()
            End If

            If (Resultado <> -1) Then
                Me.txtfechamat.Text = ""
                Return True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            Return False
        End Try
    End Function

    Private Function ValidarSeleccionGrid() As Boolean
        Dim sbMensaje As New StringBuilder

        Dim mensaje As String

        Dim tbl As Data.DataTable
        'Dim codigo_test As Integer = 1 -- andy.diaz 20/03/2019
        Dim codigo_test As Integer = Request.QueryString("mod") 'Ahora el procedimiento también filtra por tipo de estudio -- andy.diaz 20/03/2019
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        If cboCondicion.SelectedValue = "INGRESANTE" Then
            If codigo_test <> 6 Then '-- andy.diaz 21/05/2019: No aplica la validación para EDUCACION CONTÍNUA
                '________________________________________________________
                '1. Verificar en la base de datos  
                tbl = obj.TraerDataTable("EPRE_ListarAlumnosPorPersona", cboCicloIngreso.SelectedValue, txtNroDocumento.Text, "I", codigo_test, hfCodAlu.Value)

                If tbl.Rows.Count() > 0 Then

                    mensaje = "No se puede " & _
                             "asignar como Ingresante al participante porque ya se ha elegido como ingresante" & _
                             " en otra modalidad y/o proceso de admisión."
                    Response.Write("<script>alert('" & mensaje & "')</script>")
                    cboCondicion.Focus()
                    Return False
                End If
            End If
        End If

        Return True

    End Function

    Private Sub LimpiaControles()
        txtApellidoMat.Text = ""
        txtApellidoPat.Text = ""
        txtCodUniversitario_Buscar.Text = ""
        txtEmail.Text = ""
        txtFecha.Text = Date.Now.ToString("dd/MM/yyyy")
        txtNombres.Text = ""
        txtNroDocumento.Text = ""
        hfCodAlu.Value = 0
        txtObs.Text = ""
        txtFecha.Text = ""
        btnBitacoraObservaciones.Visible = False
        cboEstado.SelectedIndex = -1
        cboSexo.SelectedIndex = -1

        txtTelefonoCasa.Text = ""
        txtTelefonoMovil.Text = ""

        lblCarreraVigencia.Text = ""

        chkAlcanzoVacante.Checked = False
        chkEliminado.Checked = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Try
                InitControlesForm()
                LimpiarDatosOrig()
            Catch ex As Exception
                Response.Write(ex.Message.ToString)
            End Try
        End If
    End Sub

    Private Sub InitControlesForm()
        Me.cboPlanEstudio.Enabled = (Request.QueryString("mod") = "5") 'SOLO HABILITADO PARA POSGRADO
        Me.trfechamat.Visible = False
        CargaModalidadIngreso()
        CargaCicloAcademico()
        CargaCarreraProfesional()
        CargaOtrosCombos()
        '----------------------------- Para DropDownList con CheckBoxs ---------------------------------
        lstMultipleValues2.Attributes.Add("onclick", "FindSelectedItems(this," + txtSelectedMLValues2.ClientID + ");")
        lstMultipleValues.Attributes.Add("onclick", "FindSelectedItems(this," + txtSelectedMLValues.ClientID + ");")
        CargarComboCategorias()
        CargarComboBeneficios()
        '----------------------------------------------------------------------------------------------
        LimpiaControles()
    End Sub

    Private Sub CargarComboCategorias()
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            ClsFunciones.LlenarListas(lstMultipleValues2, obj.TraerDataTable("EPRE_ListarCategoriasPostulacion", "%"), "codigo_cap", "descripcion_cap", "--Sin Categoría--")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarComboBeneficios()
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            ClsFunciones.LlenarListas(lstMultipleValues, obj.TraerDataTable("EPRE_ListarBeneficiosPostulacion", "%"), "codigo_bp", "descripcion_bp", "--Sin Beneficio--")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cboCarreraProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarreraProf.SelectedIndexChanged
        Try
            If cboCarreraProf.SelectedIndex <> -1 Then
                'Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
                'Dim dtCarreras As Data.DataTable = obj.TraerDataTable("ConsultarCarreraProfesional", "TE", Request.QueryString("mod"))

                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                Dim dtCarreras As New data.datatable
                dtCarreras = obj.TraerDataTable("ConsultarCarreraProfesional", "TE", Request.QueryString("mod"))


                Dim verifica_vigencia As Integer = 0
                For k As Integer = 0 To dtCarreras.Rows.Count - 1
                    If dtCarreras.Rows(k).Item("codigo_cpf") = cboCarreraProf.SelectedValue Then
                        If dtCarreras.Rows(k).Item("vigencia_cpf").ToString = True Then
                            verifica_vigencia = 1
                        End If
                        'Response.Write("<script>alert('" + dtCarreras.Rows(k).Item("vigencia_cpf").ToString + "')</script>")
                    End If
                Next

                If verifica_vigencia = 0 Then
                    'No Vigente
                    Me.lblCarreraVigencia.Text = "LA CARRERA PROFESIONAL NO SE ENCUENTRA VIGENTE"
                Else ' Vigente
                    Me.lblCarreraVigencia.Text = ""
                End If


                CargaPlanEstudios(cboCarreraProf.SelectedValue)
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        If (txtCodUniversitario_Buscar.Text.Trim <> "") Then
            BuscaAlumno()
        Else
            Response.Write("<script>alert('Debe ingresar algun código')</script>")
        End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim codigoAlu As Integer = hfCodAlu.Value

        If VerificaIngresoDatos(codigoAlu) = True Then
            If ActualizaDatosAlumnos() = True Then
                'Verifico si se ha modificado el precio crédito del alumno, en ese caso envío un correo a pensiones
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()

                Dim tipoConsulta As String = "F"
                Dim dtDatosAlumno As Data.DataTable = obj.TraerDataTable("ADM_ConsultarAlumnoInscripcion", hfCodAlu.Value, tipoConsulta)

                obj.CerrarConexion()

                If dtDatosAlumno.Rows.Count > 0 Then
                    ViewState("newPrecioCredito") = dtDatosAlumno.Rows(0).Item("precioCreditoAct_Alu")

                    If ViewState("newPrecioCredito") <> ViewState("origPrecioCredito") Then
                        EnviarCorreoPensiones(codigoAlu)
                    End If
                End If

                If Me.hdvigencia.Value = 0 Then
                    Me.cboCarreraProf.Items.RemoveAt(cboCarreraProf.Items.Count - 1)
                    CargaPlanEstudios(cboCarreraProf.SelectedValue)
                End If
                InitControlesForm()
                Response.Write("<script>alert('Los datos se han guardado correctamente')</script>")
            Else
                Response.Write("<script>alert('No se actualizaron los datos del alumno')</script>")
            End If
        End If
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        InitControlesForm()
    End Sub

    Protected Sub cboModalidadIng_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboModalidadIng.SelectedIndexChanged
        Try
            If cboModalidadIng.SelectedValue = 2 Then
                Me.trfechamat.Visible = True
            Else
                Me.trfechamat.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub CargarMaxL()
        If cboTipoDocumento.selecteditem.text = "DNI" Then
            Me.txtNroDocumento.MaxLength = 8
        Else
            Me.txtNroDocumento.MaxLength = 11
        End If
    End Sub

    Protected Sub cboCondicion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCondicion.SelectedIndexChanged
        If cboCondicion.SelectedValue.Trim = "INGRESANTE" Then
            chkAlcanzoVacante.Enabled = True
        Else
            chkAlcanzoVacante.Enabled = False
            chkAlcanzoVacante.Checked = False
        End If
        udpAlcanzoVacante.Update()
    End Sub

    Private Sub LimpiarDatosOrig()
        ViewState.Remove("origPrecioCredito")
        ViewState.Remove("newPrecioCredito")
        ViewState.Remove("origEliminadoAlu")
        ViewState.Remove("origNombreCpf")
        ViewState.Remove("origNombreMin")
    End Sub

    Private Sub EnviarCorreoPensiones(ByVal codigoAlu As Integer)
        Dim mo_Cnx As New ClsConectarDatos
        Dim mo_RepoAdmision As New ClsAdmision
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        mo_Cnx.AbrirConexion()

        Dim tipoConsulta As String = "CO"
        Dim dt As Data.DataTable = mo_RepoAdmision.ConsultarDatosAlumno(tipoConsulta, codigoAlu)

        Dim tipo As String = "TO"
        Dim codigoPer As String = Request.QueryString("id")
        Dim usuario As String = ""
        Dim correoUsuario As String = ""
        Dim correoPensiones As String = ""

        Dim dtDatosUsuario As Data.DataTable = mo_RepoAdmision.ConsultarDatosUsuario(tipo, codigoPer)
        If dtDatosUsuario.Rows.Count > 0 Then
            usuario = dtDatosUsuario.Rows(0).Item("usuario_per")
            correoUsuario = dtDatosUsuario.Rows(0).Item("email_Per")
        End If

        Dim dtDatosPensiones As Data.DataTable = mo_RepoAdmision.ConsultarDatosUsuario(tipo, ClsAdmision.CodigoJefaPensiones)
        If dtDatosPensiones.Rows.Count > 0 Then
            correoPensiones = dtDatosPensiones.Rows(0).Item("email_Per")
        End If

        If dt.Rows.Count > 0 Then
            Dim codigoUniversitario As String = dt.Rows(0).Item("codigoUniver_Alu")
            Dim postulante As String = dt.Rows(0).Item("alumno")

            Dim mensaje As String = "<font face='Trebuchet MS'>"
            mensaje &= "<div style=""font-size: 14px;"">"
            mensaje &= "<p>Estimado(a):</p>"
            mensaje &= "<p>Se ha generado un cambio en el precio crédito del postulante " & postulante & " con código " & codigoUniversitario
            mensaje &= "<ul>"
            mensaje &= "<li>S/." & ViewState("origPrecioCredito") & " a S/." & ViewState("newPrecioCredito") & "</li>"
            mensaje &= "</ul>"
            mensaje &= "Este cambio se ha realizado desde el módulo de: GESTIÓN DE ADMISIÓN Y ESCUELA PRE > Movimientos > Actualizar Datos"

            If Not String.IsNullOrEmpty(usuario) Then
                mensaje &= "<p><b>Usuario:</b> " & usuario & "</p>"
            End If
            mensaje &= "<table style=""font-family: Calibri, Helvetica, Arial, sans-serif; font-size: 14px; border-collapse: collapse;"">"
            mensaje &= "<tr>"
            mensaje &= "<th colspan=""2"" style=""background-color: #C9D583"">Datos originales</th>"
            mensaje &= "<tr/>"
            mensaje &= "<tr>"
            mensaje &= "<th style=""text-align: right"">Carrera: </th>"
            mensaje &= "<td style=""min-width: 350px;"">&nbsp;&nbsp;" & ViewState("origNombreCpf") & "&nbsp;&nbsp;</td>"
            mensaje &= "</tr>"
            mensaje &= "<tr>"
            mensaje &= "<th style=""text-align: right"">Modalidad: </th>"
            mensaje &= "<td style=""min-width: 350px;"">&nbsp;&nbsp;" & ViewState("origNombreMin") & "&nbsp;&nbsp;</td>"
            mensaje &= "</tr>"
            mensaje &= "</table>"

            mensaje &= "<br>"

            mensaje &= "<table style=""min-width = 500px; font-family: Calibri, Helvetica, Arial, sans-serif; font-size: 14px; border-collapse: collapse;"">"
            mensaje &= "<tr>"
            mensaje &= "<th colspan=""2"" style=""background-color: #56D1CA"">Datos actuales</th>"
            mensaje &= "<tr/>"
            mensaje &= "<tr>"
            mensaje &= "<th style=""text-align: right"">Carrera: </th>"
            mensaje &= "<td style=""min-width: 350px;"">&nbsp;&nbsp;" & cboCarreraProf.SelectedItem.Text & "&nbsp;&nbsp;</td>"
            mensaje &= "</tr>"
            mensaje &= "<tr>"
            mensaje &= "<th style=""text-align: right"">Modalidad: </th>"
            mensaje &= "<td style=""min-width: 350px;"">&nbsp;&nbsp;" & cboModalidadIng.SelectedItem.Text & "&nbsp;&nbsp;</td>"
            mensaje &= "</tr>"
            mensaje &= "</table>"

            mensaje &= "<p>Atentamente.<br>Campus Virtual USAT</p>"
            mensaje &= "</div>"
            mensaje &= "</font>"

            mo_Cnx.AbrirConexion()
            dt = mo_Cnx.TraerDataTable("InsertaEnvioCorreosMasivo", ClsAdmision.CodigoJefaPensiones, "Cambio de precio crédito", correoPensiones, mensaje, 32, ClsAdmision.CorreoServiciosTI, "")
            dt = mo_Cnx.TraerDataTable("InsertaEnvioCorreosMasivo", codigoPer, "Cambio de precio crédito", correoUsuario, mensaje, 32, ClsAdmision.CorreoServiciosTI, "")
            mo_Cnx.CerrarConexion()

            'TODAVÍA NO EXISTE EN PROD
            'me_EnvioCorreosMasivo = md_EnvioCorreosMasivo.GetEnvioCorreosMasivo(0)
            'With me_EnvioCorreosMasivo
            '    .operacion = "I"
            '    .cod_user = Request.QueryString("id")
            '    .tipoCodigoEnvio_ecm = "codigo_per"
            '    .codigoEnvio_ecm = ClsAdmision.JefaPensiones
            '    .codigo_apl = 32 'ADMISIÓN Y ESC PRE
            '    .correo_destino = ClsAdmision.CorreoJefaPensiones
            '    .correo_respuesta = ClsAdmision.CorreoServiciosTI
            '    .asunto = "Cambio de precio crédito"
            '    .cuerpo = mensaje
            'End With
            'dt = md_EnvioCorreosMasivo.RegistrarEnvioCorreosMasivo(me_EnvioCorreosMasivo) 
        End If
        mo_Cnx.CerrarConexion()
    End Sub
End Class


Partial Class academico_FrmActualizaDatosPP
    Inherits System.Web.UI.Page

    Private Sub CargaModalidadIngreso()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Me.cboModalidadIng.DataSource = obj.TraerDataTable("ConsultarModalidadIngreso", "TO", "")
        Me.cboModalidadIng.DataValueField = "codigo_Min"
        Me.cboModalidadIng.DataTextField = "nombre_Min"
        Me.cboModalidadIng.DataBind()
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
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Me.cboCarreraProf.DataSource = obj.TraerDataTable("ConsultarCarreraProfesional", "TE", "3")
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
        cboTipoDocumento.Items.Add("DNI")
        cboTipoDocumento.Items.Add("CARNÉ DE EXTRANJERÍA")
        cboTipoDocumento.Items.Add("PASAPORTE")

        cboSexo.Items.Add("FEMENINO")
        cboSexo.Items.Add("MASCULINO")

        cboCondicion.Items.Add("INGRESANTE")
        cboCondicion.Items.Add("POSTULANTE")
    End Sub

    Private Sub BuscaAlumno()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim dt As Data.DataTable
        Try
            'Response.Write(">>>" & Me.txtCodUniversitario_Buscar.Text)
            'dt = obj.TraerDataTable("ConsultarAlumno", "CU", Me.txtCodUniversitario_Buscar.Text)
            dt = obj.TraerDataTable("ConsultarAlumno_ActualizaDatos", Me.txtCodUniversitario_Buscar.Text)
            With dt
                If (.Rows.Count > 1) Then
                    Response.Write("<script>alert('Existe más 1 persona relacionado con ese codigo')</script>")
                Else

                    If (.Rows.Count < 1) Then
                        Response.Write("<script>alert('No se encontró al usuario')</script>")
                    Else
                        If (dt.Rows(0).Item("codigo_test").ToString = "3") Then
                            hfCodAlu.Value = dt.Rows(0).Item("codigo_Alu").ToString

                            txtApellidoPat.Text = dt.Rows(0).Item("apellidoPat_Alu").ToString
                            txtApellidoMat.Text = dt.Rows(0).Item("apellidoMat_Alu").ToString
                            txtNombres.Text = dt.Rows(0).Item("nombres_Alu").ToString
                            HdEstadoActual.Value = dt.Rows(0).Item("estadoActual_Alu").ToString
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
                            cboCarreraProf.SelectedValue = dt.Rows(0).Item("codigo_Cpf")
                            CargaPlanEstudios(cboCarreraProf.SelectedValue)
                            cboPlanEstudio.SelectedValue = dt.Rows(0).Item("codigo_Pes")
                            chkAplicacion.Checked = dt.Rows(0).Item("colegioAplicacion_Alu").ToString.Trim
                            cboModalidadIng.SelectedValue = dt.Rows(0).Item("codigo_Min")
                            txtObs.Text = dt.Rows(0).Item("observacion_Dal")
                            btnBitacoraObservaciones.Visible = True

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
                        Else
                            Response.Write("<script>alert('El alumno no es estudiante de Profesionalización')</script>")
                        End If
                    End If
                End If
            End With
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("<script>alert('Error al buscar datos del alumno')</script>")
        End Try
    End Sub

    Private Function VerificaIngresoDatos() As Boolean
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

        Return True
    End Function

    Private Function ActualizaDatosAlumnos() As Boolean
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim Resultado As Integer = -1
        Dim id As String
        Try
            If ValidarSeleccionGrid() Then
                id = Request.QueryString("id")
                Resultado = obj.Ejecutar("CAJ_ModificarAlumno_v4" _
                            , hfCodAlu.Value _
                            , txtApellidoPat.Text _
                            , txtApellidoMat.Text _
                            , txtNombres.Text _
                            , HdEstadoActual.Value _
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
                            , CType(txtSelectedMLValues.Value, String))

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

            End If

            If (Resultado <> -1) Then
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
        Dim obj As New ClsConectarDatos
        Dim tbl As Data.DataTable
        'Dim codigo_test As Integer = 1 -- andy.diaz 20/03/2019
        Dim codigo_test As Integer = 3 'Profesionalización, ahora el procedimiento también filtra por tipo de estudio -- andy.diaz 20/03/2019

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        If cboCondicion.SelectedValue = "INGRESANTE" Then
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
            Else
                Return True
            End If
        Else
            Return True
        End If

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
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
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
        End If
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
        If cboCarreraProf.SelectedIndex <> -1 Then
            CargaPlanEstudios(cboCarreraProf.SelectedValue)
        End If
    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        If (txtCodUniversitario_Buscar.Text.Trim <> "") Then
            BuscaAlumno()
        Else
            Response.Write("<script>alert('Debe ingresar algun código')</script>")
        End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        If VerificaIngresoDatos() = True Then
            If ActualizaDatosAlumnos() = True Then
                LimpiaControles()
                Response.Write("<script>alert('Los datos se han guardado correctamente')</script>")
            Else
                Response.Write("<script>alert('Error al actualizar datos del alumno')</script>")
            End If
        End If
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        LimpiaControles()
    End Sub

End Class

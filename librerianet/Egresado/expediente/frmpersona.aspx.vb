Partial Class frmpersona
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load        
        If IsPostBack = False Then
            Dim obj As New ClsConectarDatos
            Dim dtModalidad As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.dpdepartamento, obj.TraerDataTable("ConsultarLugares", 2, 156, 0), "codigo_dep", "nombre_dep", "--Seleccione--")
            dtModalidad = obj.TraerDataTable("ALUMNI_BuscaModalidad", 0)
            obj.CerrarConexion()

            Me.dpModalidad.DataTextField = "nombre_Min"
            Me.dpModalidad.DataValueField = "codigo_Min"
            Me.dpModalidad.DataSource = dtModalidad
            Me.dpModalidad.DataBind()
            dtModalidad.Dispose()

            Me.dpprovincia.Items.Add("---Seleccione---") : Me.dpprovincia.Items(0).Value = -1
            Me.dpdistrito.Items.Add("---Seleccione---") : Me.dpdistrito.Items(0).Value = -1

            'Cargar Combo de Sectores
            Dim dtSectores As New Data.DataTable
            obj.AbrirConexion()
            dtSectores = obj.TraerDataTable("ALUMNI_BuscaSector", 0)
            obj.CerrarConexion()
            ClsFunciones.LlenarListas(Me.dpSector, dtSectores, "codigo_sec", "nombre_sec", "--Seleccione--")
            dtSectores.Dispose()

            tablaNombre.Visible = False
            If (Request.QueryString("pso") <> Nothing) Then
                Dim dtEstados As New Data.DataTable
                obj.AbrirConexion()
                dtEstados = obj.TraerDataTable("ALUMNI_RetornaEstadoEgresados", Me.Request.QueryString("pso"))
                obj.CerrarConexion()

                If (dtEstados.Rows(0).Item("PreEgresado") <> "2") Then
                    Dim denc As New EncriptaCodigos.clsEncripta
                    Me.hdcodigo_pso.Value = denc.Decodifica(Request.QueryString("pso")).Substring(3)
                    BuscarPersona("COE", Me.hdcodigo_pso.Value, True)
                    'Confirma Egreso
                    obj.AbrirConexion()
                    obj.Ejecutar("ALUMNI_ConfirmaPreEgresado", Me.hdcodigo_pso.Value)
                    obj.CerrarConexion()
                    Me.lblmensaje.Text = "Acaba de confirmar su estado de Egresado USAT"

                    tablaNombre.Visible = True
                    'Enviar correo a director de Alumni
                    Dim dtDirector As New Data.DataTable
                    obj.AbrirConexion()
                    dtDirector = obj.TraerDataTable("ALUMNI_RetornaDirectorCco", 875)
                    obj.CerrarConexion()

                    Me.tablaNombre.Visible = True
                    If (dtDirector.Rows.Count > 0) Then
                        Dim EnviaMail As New ClsMail
                        Dim Encripta As New EncriptaCodigos.clsEncripta
                        Dim strMensaje As String

                        strMensaje = "Se activo como Egresado USAT: <br/>"
                        strMensaje = "Nombre: " & Me.txtAPaterno.Text & " " & Me.txtAMaterno.Text & ", " & Me.txtNombres.Text & "<br/>"
                        strMensaje = "Correo Electronico: " & Me.txtemail1.Text

                        'EnviaMail.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual USAT", dtDirector.Rows(0).Item("usuario_per").ToString & "@usat.edu.pe", "Confirmacion de Egresado", strMensaje, True)
                        Me.tablaNombre.Visible = False
                    End If

                    BloquearControles(False)
                End If                
                dtEstados.Dispose()
            End If

            obj = Nothing
        End If
    End Sub

    Private Sub BloquearControles(ByVal sw As Boolean)
        Me.lnkComprobarDNI.Visible = sw
        Me.lnkComprobarNombres.Visible = sw
        'Me.txtFechaNac.Enabled = sw
        'Me.dpSexo.Enabled = sw
        Me.dpEstadoCivil.Enabled = sw
        Me.txtConyuge.Enabled = sw
        Me.txtFecha.Enabled = sw
        Me.txtemail1.Enabled = sw
        Me.txtemail2.Enabled = sw
        Me.txtdireccion.Enabled = sw
        Me.dpdepartamento.Enabled = sw
        Me.dpprovincia.Enabled = sw
        Me.dpdistrito.Enabled = sw
        Me.txttelefono.Enabled = sw
        Me.txtcelular.Enabled = sw
        Me.txtruc.Enabled = sw
        'Me.dpModalidad.Enabled = sw
        'Me.dpNivel.Enabled = sw
        Me.txtFormacion.Enabled = sw
        Me.txtExperiencia.Enabled = sw
        Me.txtOtrosEstudios.Enabled = sw
        Me.fileCV.Enabled = sw
        Me.fileFoto.Enabled = sw
        Me.chkMostrar.Enabled = sw
        Me.cmdGuardar.Enabled = sw
        Me.cmdLimpiar.Enabled = sw
    End Sub

    Private Sub BuscarPersona(ByVal tipo As String, ByVal valor As String, Optional ByVal mostrardni As Boolean = False)
        Me.lblmensaje.Text = ""
        Me.cmdGuardar.Enabled = True        
        Dim obj As New ClsConectarDatos
        Dim ExistePersona As Boolean = False

        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim tbl As Data.DataTable            
            '==================================
            'Buscar a la Persona
            '==================================
            obj.AbrirConexion()
            tbl = obj.TraerDataTable("ALUMNI_ConsultarPersona", tipo, valor)
            obj.CerrarConexion()
            If tbl.Rows.Count > 0 Then
                existepersona = True                
            End If

            If ExistePersona = True Then                
                tablaNombre.Visible = True

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

                Me.txtConyuge.Text = tbl.Rows(0).Item("conyugue_pso").ToString
                Me.txtFecha.Text = tbl.Rows(0).Item("fechaMat_pso").ToString
                Me.dpTipoDoc.SelectedValue = tbl.Rows(0).Item("tipoDocIdent_Pso").ToString
                Me.txtemail1.Text = tbl.Rows(0).Item("emailPrincipal_Pso").ToString
                Me.txtemail2.Text = tbl.Rows(0).Item("emailAlternativo_Pso").ToString
                Me.txtdireccion.Text = tbl.Rows(0).Item("direccion_Pso").ToString
                Me.txttelefono.Text = tbl.Rows(0).Item("telefonoFijo_Pso").ToString
                Me.txtcelular.Text = tbl.Rows(0).Item("telefonoCelular_Pso").ToString
                Me.dpModalidad.SelectedValue = tbl.Rows(0).Item("codigo_Min")
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
                        obj.AbrirConexion()
                        ClsFunciones.LlenarListas(Me.dpprovincia, obj.TraerDataTable("ConsultarLugares", 3, tbl.Rows(0).Item("codigo_dep"), 0), "codigo_pro", "nombre_pro", "--Seleccione--")
                        ClsFunciones.LlenarListas(Me.dpdistrito, obj.TraerDataTable("ConsultarLugares", 4, tbl.Rows(0).Item("codigo_pro"), 0), "codigo_dis", "nombre_dis", "--Seleccione--")
                        obj.CerrarConexion()
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

                'Buscamos si existe el egresado
                Dim dt_Egresado As New Data.DataTable
                obj.AbrirConexion()
                dt_Egresado = obj.TraerDataTable("ALUMNI_BuscaEgresado", Me.hdcodigo_pso.Value)
                obj.CerrarConexion()               
                If (dt_Egresado.Rows.Count > 0) Then                    
                    Me.dpNivel.Text = dt_Egresado.Rows(0).Item("nivel_Ega").ToString.ToUpper
                    Me.txtFormacion.Text = dt_Egresado.Rows(0).Item("formacionacad_ega")
                    Me.txtExperiencia.Text = dt_Egresado.Rows(0).Item("experiencialab_ega")
                    Me.txtOtrosEstudios.Text = dt_Egresado.Rows(0).Item("otrosestudios_ega")
                    Me.HdFileCV.Value = dt_Egresado.Rows(0).Item("cv_Ega")
                    Me.HdFileFoto.Value = dt_Egresado.Rows(0).Item("foto_Ega")
                    Me.lblActualizacion.Text = dt_Egresado.Rows(0).Item("fechaActualizacion_Ega")
                    If (dt_Egresado.Rows(0).Item("visibilidad_Ega") = 0) Then
                        Me.chkMostrar.Checked = False
                    Else
                        Me.chkMostrar.Checked = True
                    End If

                    Me.rblSituacionLaboral.SelectedValue = dt_Egresado.Rows(0).Item("actualmenteLabora_Ega")
                    If (dt_Egresado.Rows(0).Item("actualmenteLabora_Ega").ToString = "S") Then
                        Me.txtEmpresaLabora.Text = dt_Egresado.Rows(0).Item("EmpresaLabora_Ega")
                        Me.dpSector.SelectedValue = dt_Egresado.Rows(0).Item("codigo_sec")
                        Me.rblTipoEmpresa.SelectedValue = dt_Egresado.Rows(0).Item("tipoEmpresa_Ega")
                        Me.txtCargoActual.Text = dt_Egresado.Rows(0).Item("cargoActual_Ega")
                        Me.txtDireccionEmpresa.Text = dt_Egresado.Rows(0).Item("direccionEmpresa_Ega")
                        Me.txtTelefonoProfesional.Text = dt_Egresado.Rows(0).Item("telefono_Ega")
                        Me.txtCorreoProfesional.Text = dt_Egresado.Rows(0).Item("correoProfesional_Ega")
                    End If

                    If (dt_Egresado.Rows(0).Item("promedioTrabajo") = "3") Then
                        Me.chkTresMeses.Checked = True
                    Else
                        Me.chkTresMeses.Checked = False
                        Me.txtNumMeses.Text = dt_Egresado.Rows(0).Item("promedioTrabajo")
                    End If

                    Me.lblmensaje.Text = ""
                Else
                    BuscaPreEgresado()
                    Me.lblmensaje.Text = "No esta registrado como Egresado"
                End If

                'Bloque nombres cuando no es Administrador
                DesbloquearNombres(False)
                DesbloquearOtrosDatos(True)
                lnkComprobarNombres.Visible = False
            ElseIf ValidarNroIdentidad() = True Then
                tablaNombre.Visible = False
                Me.hdcodigo_pso.Value = 0
                lnkComprobarNombres.Visible = True
                DesbloquearNombres(True)
                DesbloquearOtrosDatos(False)

                If Me.txtAPaterno.Enabled = True Then
                    Me.txtAPaterno.Focus()
                End If
                Me.cmdGuardar.Enabled = False
            End If
            tbl.Dispose()
            obj = Nothing
            tbl = Nothing
        Catch ex As Exception
            tablaNombre.Visible = False
            Me.lblmensaje.Text = "Error " & ex.Message
            Me.lblmensaje.Visible = True
            'obj.CerrarConexion()
            'obj = Nothing
        End Try
    End Sub

    Private Sub BuscaPreEgresado()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tbl As Data.DataTable
        '==================================
        'Buscar a la Persona
        '==================================
        obj.AbrirConexion()
        tbl = obj.TraerDataTable("ALUMNI_BuscaPreEgresado", Me.hdcodigo_pso.Value)
        obj.CerrarConexion()
        'Me.hdcodigo_pso.Value = tbl.Rows(0).Item("codigo_Pso")

        If (tbl.Rows.Count > 0) Then
            LimpiaFormulario()
            If tbl.Rows(0).Item("numeroDocIdent_Pso").ToString <> "" Then
                Me.txtdni.Text = tbl.Rows(0).Item("numeroDocIdent_Pso").ToString
                Me.txtdni.Enabled = False
            Else
                Me.txtdni.Enabled = True
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

            Me.txtConyuge.Text = tbl.Rows(0).Item("conyugue_pso").ToString
            Me.txtFecha.Text = tbl.Rows(0).Item("fechaMat_pso").ToString
            Me.dpTipoDoc.SelectedValue = tbl.Rows(0).Item("tipoDocIdent_Pso").ToString
            Me.txtemail1.Text = tbl.Rows(0).Item("emailPrincipal_Pso").ToString
            Me.txtemail2.Text = tbl.Rows(0).Item("emailAlternativo_Pso").ToString
            Me.txtdireccion.Text = tbl.Rows(0).Item("direccion_Pso").ToString
            Me.txttelefono.Text = tbl.Rows(0).Item("telefonoFijo_Pso").ToString
            Me.txtcelular.Text = tbl.Rows(0).Item("telefonoCelular_Pso").ToString
            Me.dpModalidad.SelectedValue = tbl.Rows(0).Item("codigo_Min")
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
                    obj.AbrirConexion()
                    ClsFunciones.LlenarListas(Me.dpprovincia, obj.TraerDataTable("ConsultarLugares", 3, tbl.Rows(0).Item("codigo_dep"), 0), "codigo_pro", "nombre_pro", "--Seleccione--")
                    ClsFunciones.LlenarListas(Me.dpdistrito, obj.TraerDataTable("ConsultarLugares", 4, tbl.Rows(0).Item("codigo_pro"), 0), "codigo_dis", "nombre_dis", "--Seleccione--")
                    obj.CerrarConexion()
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

            Me.dpNivel.Text = tbl.Rows(0).Item("nivel_Ega").ToString.ToUpper
            Me.txtFormacion.Text = tbl.Rows(0).Item("formacionacad_ega")
            Me.txtExperiencia.Text = tbl.Rows(0).Item("experiencialab_ega")
            Me.txtOtrosEstudios.Text = tbl.Rows(0).Item("otrosestudios_ega")
            Me.HdFileCV.Value = tbl.Rows(0).Item("cv_Ega")
            Me.HdFileFoto.Value = tbl.Rows(0).Item("foto_Ega")
            Me.lblActualizacion.Text = tbl.Rows(0).Item("fechaActualizacion_Ega")
            If (tbl.Rows(0).Item("visibilidad_Ega") = 0) Then
                Me.chkMostrar.Checked = False
            Else
                Me.chkMostrar.Checked = True
            End If

            Me.rblSituacionLaboral.SelectedValue = tbl.Rows(0).Item("actualmenteLabora_Ega")
            If (tbl.Rows(0).Item("actualmenteLabora_Ega").ToString = "S") Then
                Me.txtEmpresaLabora.Text = tbl.Rows(0).Item("EmpresaLabora_Ega")
                Me.dpSector.SelectedValue = tbl.Rows(0).Item("codigo_sec")
                Me.rblTipoEmpresa.SelectedValue = tbl.Rows(0).Item("tipoEmpresa_Ega")
                Me.txtCargoActual.Text = tbl.Rows(0).Item("cargoActual_Ega")
                Me.txtDireccionEmpresa.Text = tbl.Rows(0).Item("direccionEmpresa_Ega")
                Me.txtTelefonoProfesional.Text = tbl.Rows(0).Item("telefono_Ega")
                Me.txtCorreoProfesional.Text = tbl.Rows(0).Item("correoProfesional_Ega")
            End If

            If (tbl.Rows(0).Item("promedioTrabajo") = "3") Then
                Me.chkTresMeses.Checked = True
            Else
                Me.chkTresMeses.Checked = False
                Me.txtNumMeses.Text = tbl.Rows(0).Item("promedioTrabajo")
            End If

            Me.lblmensaje.Text = ""
        End If
    End Sub

    Private Function validaIngresoForm() As Boolean
        If (Me.dpEstadoCivil.SelectedValue = "CASADO") Then
            If (Me.txtConyuge.Text.Trim = "") Then
                Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Ingrese a su cónyuge')</SCRIPT>")
                Me.txtConyuge.Focus()
                Return False
            End If

            If (Me.txtFecha.Text.Trim = "") Then
                Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Ingrese la fecha de su matrimonio')</SCRIPT>")
                Me.txtFecha.Focus()
                Return False
            End If
        End If

        If (Me.rblSituacionLaboral.SelectedValue.ToString = "") Then
            Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Debe indicar si actualmente esta laborando')</SCRIPT>")
            Return False
        Else
            If (Me.rblSituacionLaboral.SelectedValue.ToString = "S") Then
                If (Me.txtEmpresaLabora.Text.Trim = "") Then
                    Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Ingrese la empresa donde actualmente labora')</SCRIPT>")
                    Me.txtEmpresaLabora.Focus()
                    Return False
                End If

                If (Me.dpSector.SelectedValue.ToString = "") Then
                    Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Seleccione el sector donde actualmente labora')</SCRIPT>")
                    Me.dpSector.Focus()
                    Return False
                End If

                If (Me.rblTipoEmpresa.SelectedValue.ToString = "") Then
                    Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Seleccione el tipo de empresa donde actualmente labora')</SCRIPT>")
                    Me.rblTipoEmpresa.Focus()
                    Return False
                End If

                If (Me.txtCargoActual.Text.Trim = "") Then
                    Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Ingrese el cargo que desempeña en la empresa donde actualmente labora')</SCRIPT>")
                    Me.txtCargoActual.Focus()
                    Return False
                End If
            End If
        End If

        If ((Me.chkTresMeses.Checked = False) And (Me.txtNumMeses.Text.Trim = "")) Then
            Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('Debe indicar cuantos meses le llevó en conseguir el puesto acorde a su formación')</SCRIPT>")
            Return False
        End If

        Return True
    End Function

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click                 
        Try
            If (validaIngresoForm() = True) Then                
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

                Dim nomCurriculum As String = ""
                Dim nomFoto As String = ""
                Dim strRutaCV As String
                Dim strRutaFoto As String
                Dim archivo As String = ""
                Dim sw As Byte = 0
                While (sw = 0)
                    archivo = GeneraToken()

                    'Busca si existen archivos
                    Dim dtArchivos As Data.DataTable
                    obj.AbrirConexion()
                    dtArchivos = obj.TraerDataTable("ALUMNI_ExisteArchivoPreEgresado", archivo)
                    obj.CerrarConexion()

                    If (dtArchivos.Rows.Count = 0) Then
                        sw = 1
                    End If
                End While

                If (Me.fileCV.HasFile = True) Then
                    strRutaCV = Server.MapPath("..\Egresado\curriculum")
                    nomCurriculum = archivo & System.IO.Path.GetExtension(Me.fileCV.FileName).ToString
                    Me.fileCV.PostedFile.SaveAs(strRutaCV & "\" & nomCurriculum)
                ElseIf (Me.PostedFileCV IsNot Nothing) Then
                    strRutaCV = Server.MapPath("..\Egresado\curriculum")
                    nomCurriculum = archivo & System.IO.Path.GetExtension(Me.fileCV.FileName).ToString
                    Me.fileCV.PostedFile.SaveAs(strRutaCV & "\" & nomCurriculum)
                Else
                    nomCurriculum = Me.HdFileCV.Value
                End If

                If (Me.fileFoto.HasFile = True) Then
                    strRutaFoto = Server.MapPath("..\Egresado\fotos")
                    nomFoto = archivo & System.IO.Path.GetExtension(Me.fileFoto.FileName).ToString
                    Me.fileFoto.PostedFile.SaveAs(strRutaFoto & "\" & nomFoto)
                ElseIf (Me.PostedFileFoto IsNot Nothing) Then
                    strRutaFoto = Server.MapPath("..\Egresado\fotos")
                    nomFoto = archivo & System.IO.Path.GetExtension(Me.fileFoto.FileName).ToString
                    Me.fileFoto.PostedFile.SaveAs(strRutaFoto & "\" & nomFoto)
                Else
                    nomFoto = Me.HdFileFoto.Value
                End If

                'Registramos a Egresado            
                If ValidarNroIdentidad() = True Then
                    Dim meses As Integer = 0
                    If (Me.chkTresMeses.Checked = True) Then
                        meses = 3
                    Else
                        meses = Integer.Parse(Me.txtNumMeses.Text)
                    End If

                    Dim sector As Integer
                    If (Me.dpSector.SelectedValue = "-1") Then
                        sector = 14
                    Else
                        sector = Me.dpSector.SelectedValue
                    End If

                    Dim strEstado As String
                    Dim dtEstados As New Data.DataTable
                    obj.AbrirConexion()
                    dtEstados = obj.TraerDataTable("ALUMNI_RetornaEstadoEgresados", Me.hdcodigo_pso.Value)
                    obj.CerrarConexion()

                    If (dtEstados.Rows(0).Item("PreEgresado") = "2") Then
                        strEstado = "1"
                    Else
                        strEstado = "0"
                    End If

                    obj.AbrirConexion()
                    obj.Ejecutar("ALUMNI_RegistraPreEgresado", Me.hdcodigo_pso.Value, _
                                Me.dpTipoDoc.Text, Me.txtdni.Text.Trim, _
                                Me.txtAPaterno.Text, Me.txtAMaterno.Text, _
                                Me.txtNombres.Text, Me.txtFechaNac.Text, _
                                Me.dpSexo.SelectedValue, Me.dpEstadoCivil.SelectedValue, _
                                Me.txtConyuge.Text, Me.txtFecha.Text, _
                                Me.txtemail1.Text, Me.txtemail2.Text, _
                                Me.txtdireccion.Text, Me.dpdistrito.Text, _
                                Me.txttelefono.Text, Me.txtcelular.Text, _
                                Me.txtruc.Text, Me.dpModalidad.SelectedValue, _
                                Me.txtFormacion.Text, Me.txtExperiencia.Text, _
                                Me.txtOtrosEstudios.Text, nomCurriculum, nomFoto, _
                                Me.chkMostrar.Checked, Me.dpNivel.SelectedItem.Text, 0, strEstado, _
                                Me.rblSituacionLaboral.SelectedValue, Me.txtEmpresaLabora.Text, _
                                sector, Me.rblTipoEmpresa.SelectedValue, _
                                Me.txtCargoActual.Text, Me.txtDireccionEmpresa.Text, _
                                Me.txtTelefonoProfesional.Text, Me.txtCorreoProfesional.Text, _
                                meses)
                    obj.CerrarConexion()

                    'Si existe Egresado actualiza 
                    If (dtEstados.Rows(0).Item("Egresado") = "2") Then
                        obj.AbrirConexion()
                        obj.Ejecutar("ALUMNI_RegistraEgresado", Me.hdcodigo_pso.Value, _
                                    Me.txtFormacion.Text, Me.txtExperiencia.Text, _
                                    Me.txtOtrosEstudios.Text, Me.HdFileCV.Value, _
                                    Me.HdFileFoto.Value, Me.chkMostrar.Checked, _
                                    Me.dpNivel.SelectedItem.Text, 1, Me.txtdni.Text, _
                                    Me.rblSituacionLaboral.SelectedValue, Me.txtEmpresaLabora.Text, _
                                    sector, Me.rblTipoEmpresa.SelectedValue, _
                                    Me.txtCargoActual.Text, Me.txtDireccionEmpresa.Text, _
                                    Me.txtTelefonoProfesional.Text, Me.txtCorreoProfesional.Text, _
                                    meses, Me.txtFecha.Text, Me.txtConyuge.Text)

                        obj.CerrarConexion()

                    End If
                    dtEstados.Dispose()

                    Response.Write("<script>alert('Egresado registrado')</script>")
                    Me.cmdGuardar.Enabled = False
                    'Confirmacion de correo de actualizacion
                    If (strEstado = "1") Then
                        'Enviar correo a director de Alumni
                        Dim dtDirector As New Data.DataTable
                        obj.AbrirConexion()
                        dtDirector = obj.TraerDataTable("ALUMNI_RetornaDirectorCco", 875)
                        obj.CerrarConexion()

                        If (dtDirector.Rows.Count > 0) Then
                            Dim EnviaMail As New ClsMail
                            Dim Encripta As New EncriptaCodigos.clsEncripta
                            Dim strMensaje As String

                            strMensaje = "Se activo como Egresado USAT: <br/>"
                            strMensaje = "Nombre: " & Me.txtAPaterno.Text & " " & Me.txtAMaterno.Text & ", " & Me.txtNombres.Text & "<br/>"
                            strMensaje = "Correo Electronico: " & Me.txtemail1.Text

                            'EnviaMail.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual USAT", dtDirector.Rows(0).Item("usuario_per").ToString & "@usat.edu.pe", "Confirmacion de Egresado", strMensaje, True)
                        End If

                    ElseIf (strEstado = "0") Then
                        Dim EnviaMail As New ClsMail
                        Dim Encripta As New EncriptaCodigos.clsEncripta
                        Dim strMensaje As String

                        strMensaje = "Se ha registrado como Egresado de la USAT. Haga Clic para confirmar "
                        strMensaje = "http://server-test/campusvirtual/librerianet/Egresado/frmPersona.aspx?pso=" & Encripta.Codifica("069" & Me.hdcodigo_pso.Value)
                        'EnviaMail.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual USAT", Me.txtemail1.Text, "Confirmacion de Egresado", strMensaje, True)

                        Me.lblmensaje.Text = "Egresado registrado. Por favor revise su correo electrónico para confirmar."
                    End If
                End If
            End If                        
        Catch ex As Exception
            'Response.Write(ex.InnerException)
            Response.Write(ex.Message)
        End Try
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
        'Me.txtFechaNac.Enabled = estado
        'Me.dpSexo.Enabled = estado
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

        If (Me.rblSituacionLaboral.SelectedIndex = -1) Then
            BloqueaControlesEmpresa(False)
        Else
            BloqueaControlesEmpresa(True)
        End If
        'If hdcodigo_cpf.Value <> 9 Then
        'Me.dpModalidad.Enabled = False
        'End If

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
        'If hdcodigo_cpf.Value <> 9 Then
        'Me.dpModalidad.SelectedValue = -1
        'End If
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
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            'Verificamos si tambien existe en Egresado
            Dim dtEgresado As New Data.DataTable
            obj.AbrirConexion()
            dtEgresado = obj.TraerDataTable("ALUMNI_VerificaEgresado", Me.txtdni.Text.Trim)
            obj.CerrarConexion()

            If (dtEgresado.Rows(0).Item(0).ToString.Trim <> "") Then                
                lnkComprobarNombres.Text = "Clic aquí para buscar coincidencias"
                BuscarPersona("DNIE", Me.txtdni.Text.Trim)
                If (dtEgresado.Rows(0).Item(0).ToString.ToUpper = "BACHILLER") Then
                    Me.dpNivel.SelectedValue = "BACHILLER"
                ElseIf (dtEgresado.Rows(0).Item(0).ToString.ToUpper = "EGRESADO") Then
                    Me.dpNivel.SelectedValue = "EGRESADO"
                ElseIf (dtEgresado.Rows(0).Item(0).ToString.ToUpper <> "BACHILLER" Or _
                        dtEgresado.Rows(0).Item(0).ToString.ToUpper <> "EGRESADO") Then
                    Me.dpNivel.SelectedValue = "TITULADO"
                End If

                Dim dtValida As Data.DataTable
                obj.AbrirConexion()
                dtValida = obj.TraerDataTable("ALUMNI_ValidaEgresado", Me.txtdni.Text.Trim)
                obj.CerrarConexion()

                If (dtValida.Rows(0).Item(0) = "NoRegistrado") Then
                    Me.lbMensajeEgresado.Text = ""
                    Me.tablaNombre.Visible = True                    
                Else
                    LimpiaFormulario()
                    Me.lbMensajeEgresado.Text = "Ud. ya ha sido registrado"
                    Me.tablaNombre.Visible = False
                    Me.txtdni.Enabled = True
                    Me.txtdni.Focus()
                End If

            Else
                tablaNombre.Visible = False
                Me.lbMensajeEgresado.Text = "Ud. todavia no es egresado de la universidad"
            End If
        Else
            DesbloquearNombres(False)
            DesbloquearOtrosDatos(False)
        End If
    End Sub    

    Protected Sub cmdLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLimpiar.Click
        LimpiaFormulario()
        Me.hdcodigo_pso.Value = 0
        Me.txtdni.Enabled = True
        Me.txtdni.Focus()
        Me.cmdGuardar.Enabled = True
        Me.tablaNombre.Visible = False
    End Sub

    Private Sub LimpiaFormulario()
        Me.HdFileCV.Value = ""
        Me.HdFileFoto.Value = ""
        Me.PostedFileCV = Nothing
        Me.PostedFileFoto = Nothing
        Me.lblmensaje.Text = ""
        Me.dpdepartamento.SelectedValue = -1
        Me.dpprovincia.Items.Clear()
        Me.dpdistrito.Items.Clear()
        Me.lnkComprobarNombres.Visible = False
        Me.grwCoincidencias.Visible = False
        Me.LimpiarCajas()
        Me.txtdni.Text = ""
        Me.txtFormacion.Text = ""
        Me.txtExperiencia.Text = ""
        Me.txtOtrosEstudios.Text = ""
        Me.chkMostrar.Checked = False
        Me.chkTresMeses.Checked = False
        Me.txtNumMeses.Text = ""
        Me.HdFileCV.Value = ""
        Me.HdFileFoto.Value = ""
        Me.hdcodigo_pso.Value = "0"
        Me.rblSituacionLaboral.SelectedIndex = -1
        Me.rblTipoEmpresa.SelectedIndex = -1
        Me.txtEmpresaLabora.Text = ""
        Me.txtDireccionEmpresa.Text = ""
        Me.txtCargoActual.Text = ""
        Me.txtCorreoProfesional.Text = ""
        Me.txtTelefonoProfesional.Text = ""
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

    Private Function GeneraToken() As String
        Dim rnd As New Random
        Dim ubicacion As Integer
        Dim strNumeros As String = "0123456789"
        Dim strLetraMin As String = "abcdefghijklmnopqrstuvwxyz"
        Dim strLetraMay As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim Token As String = ""
        Dim strCadena As String = ""
        strCadena = strLetraMin & strNumeros & strLetraMay
        While Token.Length < 10
            ubicacion = rnd.Next(0, strCadena.Length)
            If (ubicacion = 62) Then
                Token = Token & strCadena.Substring(ubicacion - 1, 1)
            End If
            If (ubicacion < 62) Then
                Token = Token & strCadena.Substring(ubicacion, 1)
            End If
        End While

        Return Token
    End Function

    
    Protected Sub rblSituacionLaboral_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblSituacionLaboral.SelectedIndexChanged
        If (Me.rblSituacionLaboral.SelectedIndex = 1) Then
            'Limpiamos Controles
            Me.rblTipoEmpresa.SelectedIndex = -1
            Me.txtEmpresaLabora.Text = ""
            Me.txtDireccionEmpresa.Text = ""
            Me.txtTelefonoProfesional.Text = ""
            Me.txtCorreoProfesional.Text = ""
            Me.txtCargoActual.Text = ""

            'Bloqueamos controles
            BloqueaControlesEmpresa(False)
        Else
            BloqueaControlesEmpresa(True)
        End If
    End Sub

    Private Sub BloqueaControlesEmpresa(ByVal sw As Boolean)
        Me.rblTipoEmpresa.Enabled = sw
        Me.txtEmpresaLabora.Enabled = sw
        Me.txtDireccionEmpresa.Enabled = sw
        Me.txtTelefonoProfesional.Enabled = sw
        Me.txtCorreoProfesional.Enabled = sw
        Me.txtCargoActual.Enabled = sw
        Me.dpSector.Enabled = sw
    End Sub

    Protected Sub chkTresMeses_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTresMeses.CheckedChanged
        Me.txtNumMeses.Enabled = Not Me.chkTresMeses.Checked
        Me.txtNumMeses.Text = ""
    End Sub

    Protected Sub fileFoto_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles fileFoto.PreRender
        If (Me.fileFoto.HasFile = True) Then
            PostedFileFoto = Me.fileFoto.PostedFile
        End If
    End Sub

    Protected Sub fileCV_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles fileCV.PreRender
        If (Me.fileCV.HasFile = True) Then
            PostedFileCV = Me.fileCV.PostedFile
        End If
    End Sub

    Private Property PostedFileFoto() As HttpPostedFile
        Get
            If Page.Session("postedFileFoto") IsNot Nothing Then
                Return Page.Session("postedFileFoto")
            End If
            Return Nothing
        End Get
        Set(ByVal value As HttpPostedFile)
            Page.Session("postedFileFoto") = value
        End Set
    End Property

    Private Property PostedFileCV() As HttpPostedFile
        Get
            If Page.Session("postedFileCV") IsNot Nothing Then
                Return Page.Session("postedFileCV")
            End If
            Return Nothing
        End Get
        Set(ByVal value As HttpPostedFile)
            Page.Session("postedFileCV") = value
        End Set
    End Property
End Class
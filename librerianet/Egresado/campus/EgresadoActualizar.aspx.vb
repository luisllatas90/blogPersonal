Partial Class frmpersona
    Inherits System.Web.UI.Page
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load        
        If IsPostBack = False Then
            cargarDatosPagina()
            CargarDatosPersonales()
            CargarDatosEgresado()
        End If
    End Sub
    Sub cargarDatosPagina()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ClsFunciones.LlenarListas(Me.dpdepartamento, obj.TraerDataTable("ConsultarLugares", 2, 156, 0), "codigo_dep", "nombre_dep", "--Seleccione--")
        Me.dpprovincia.Items.Add("--Seleccione--") : Me.dpprovincia.Items(0).Value = -1
        Me.dpdistrito.Items.Add("--Seleccione--") : Me.dpdistrito.Items(0).Value = -1
        ClsFunciones.LlenarListas(Me.dpSector, obj.TraerDataTable("ALUMNI_BuscaSector", 0), "codigo_sec", "nombre_sec", "--Seleccione--")        
        obj.CerrarConexion()
        obj = Nothing

    End Sub
    Sub CargarDatosPersonales()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Dim tbl As Data.DataTable
        tbl = obj.TraerDataTable("ALUMNI_ConsultarDatosEgresado", Session("codigo_alu"))
        If tbl.Rows.Count Then
            Me.hdcodigo_pso.Value = tbl.Rows(0).Item("codigo_Pso")
            Me.lblApellidoPat.Text = tbl.Rows(0).Item("apellidoPaterno_Pso")
            Me.lblApellidoMat.Text = tbl.Rows(0).Item("apellidoMaterno_Pso").ToString()
            Me.lblNombres.Text = tbl.Rows(0).Item("nombres_Pso")
            Me.lblDNI.Text = tbl.Rows(0).Item("numeroDocIdent_Pso")
            If tbl.Rows(0).Item("fechanacimiento_pso").ToString <> "" Then
                Me.lblFechaNac.Text = CDate(tbl.Rows(0).Item("fechaNacimiento_Pso")).ToShortDateString
            End If
            If (tbl.Rows(0).Item("sexo_Pso") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("sexo_Pso").ToString.Trim <> "") Then                
                If (tbl.Rows(0).Item("sexo_Pso").ToString.ToUpper) = "F" Then
                    Me.lblSexo.Text = "Femenino"
                Else
                    Me.lblSexo.Text = "Masculino"
                End If
            End If
            Me.lblCarrera.Text = tbl.Rows(0).Item("nombre_cpf").ToString
            Me.txtEmailP.Text = tbl.Rows(0).Item("emailPrincipal_Pso").ToString
            Me.txtEmailA.Text = tbl.Rows(0).Item("emailAlternativo_Pso").ToString

            Me.txtDir.Text = tbl.Rows(0).Item("direccion_pso").ToString

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

            Me.txtFijo.Text = tbl.Rows(0).Item("telefonoFijo_Pso").ToString
            Me.txtCelular.Text = tbl.Rows(0).Item("telefonoCelular_Pso").ToString
            Me.txtruc.Text = tbl.Rows(0).Item("nroruc_Pso").ToString
            Me.txtConyuge.Text = tbl.Rows(0).Item("conyugue_pso").ToString
            Me.txtFechaMat.Text = tbl.Rows(0).Item("fechaMat_pso")
            Me.dpEstadoCivil.SelectedValue = -1
            If (tbl.Rows(0).Item("estadoCivil_Pso") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("estadoCivil_Pso").ToString.Trim <> "") Then
                Me.dpEstadoCivil.SelectedValue = tbl.Rows(0).Item("estadoCivil_Pso").ToString.ToUpper
            End If

        End If
        tbl.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Sub CargarDatosEgresado()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim dt_Egresado As New Data.DataTable
        obj.AbrirConexion()
        dt_Egresado = obj.TraerDataTable("ALUMNI_BuscaEgresado", Me.hdcodigo_pso.Value)
        obj.CerrarConexion()
        obj = Nothing

        If (dt_Egresado.Rows.Count > 0) Then
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

            Me.txtEmpresaLabora.Text = dt_Egresado.Rows(0).Item("EmpresaLabora_Ega")
            Me.dpSector.SelectedValue = dt_Egresado.Rows(0).Item("codigo_sec").ToString
            Me.rblTipoEmpresa.SelectedValue = dt_Egresado.Rows(0).Item("tipoEmpresa_Ega").ToString
            Me.txtCargoActual.Text = dt_Egresado.Rows(0).Item("cargoActual_Ega")
            Me.txtDireccionEmpresa.Text = dt_Egresado.Rows(0).Item("direccionEmpresa_Ega")
            Me.txtTelefonoProfesional.Text = dt_Egresado.Rows(0).Item("telefono_Ega")
            Me.txtCorreoProfesional.Text = dt_Egresado.Rows(0).Item("correoProfesional_Ega")
            Me.dpNivel.SelectedValue = dt_Egresado.Rows(0).Item("nivel_Ega").ToString
            Me.txtNumMeses.Text = dt_Egresado.Rows(0).Item("promedioTrabajo").ToString
            Me.dpSector.SelectedValue = dt_Egresado.Rows(0).Item("codigo_sec").ToString
            Me.rblSituacionLaboral.SelectedValue = dt_Egresado.Rows(0).Item("actualmenteLabora_Ega").ToString
            If (dt_Egresado.Rows(0).Item("cv_Ega").ToString.Trim <> "") Then
                Me.lnkDescarga.Attributes.Add("href", "../curriculum/" & dt_Egresado.Rows(0).Item("cv_Ega").ToString)
            Else
                Me.lnkDescarga.Attributes.Add("href", "#")
            End If

            If (dt_Egresado.Rows(0).Item("foto_Ega").ToString.Trim <> "") Then
                FotoAlumno.ImageUrl = "../fotos/" & dt_Egresado.Rows(0).Item("foto_Ega")
            Else
                FotoAlumno.ImageUrl = "../archivos/no_disponible.jpg"
            End If


            If (dt_Egresado.Rows(0).Item("promedioTrabajo") = "3") Then
                Me.chkTresMeses.Checked = True
            Else
                Me.chkTresMeses.Checked = False
                Me.txtNumMeses.Text = dt_Egresado.Rows(0).Item("promedioTrabajo")
            End If
        End If

        dt_Egresado.Dispose()
    End Sub
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

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try

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

            '#Subir CV
            If (Me.fileCV.HasFile = True) Then
                strRutaCV = Server.MapPath("..\curriculum")                
                nomCurriculum = archivo & System.IO.Path.GetExtension(Me.fileCV.FileName).ToString
                Me.fileCV.PostedFile.SaveAs(strRutaCV & "\" & nomCurriculum)
            Else
                nomCurriculum = Me.HdFileCV.Value
            End If



            '#Subir Foto
            If (Me.fileFoto.HasFile = True) Then
                strRutaFoto = Server.MapPath("..\fotos")
                nomFoto = archivo & System.IO.Path.GetExtension(Me.fileFoto.FileName).ToString
                Me.fileFoto.PostedFile.SaveAs(strRutaFoto & "\" & nomFoto)
            Else
                nomFoto = Me.HdFileFoto.Value
            End If


            Dim meses As Integer = 0
            If (Me.chkTresMeses.Checked = True) Then
                meses = 3
            Else

                If Me.txtNumMeses.Text.Trim = "" Then
                    meses = 0
                Else
                    meses = Integer.Parse(Me.txtNumMeses.Text)
                End If
            End If

            Dim sector As Integer
            If (Me.dpSector.SelectedValue = "-1") Then
                sector = 14
            Else
                sector = Me.dpSector.SelectedValue
            End If


            '"Registro de Datos Personales
            obj.AbrirConexion()
            obj.Ejecutar("ALUMNI_ActualizaDatos", _
                            Session("codigo_alu"), Me.txtEmailP.Text, Me.txtEmailA.Text _
                            , Me.txtDir.Text, Me.dpdistrito.SelectedValue, Me.txtFijo.Text.Trim, Me.txtCelular.Text.Trim _
                            , Me.txtruc.Text.Trim, Me.dpEstadoCivil.SelectedItem.Text)
            obj.CerrarConexion()

            '#Registro de Datos Profesionales
            obj.AbrirConexion()
            obj.Ejecutar("ALUMNI_RegistraEgresado", Me.hdcodigo_pso.Value, _
                        Me.txtFormacion.Text, Me.txtExperiencia.Text, _
                        Me.txtOtrosEstudios.Text, nomCurriculum, _
                        nomFoto, Me.chkMostrar.Checked, _
                        Me.dpNivel.SelectedItem.Text, 1, Me.lblDNI.Text, _
                        Me.rblSituacionLaboral.SelectedValue, Me.txtEmpresaLabora.Text, _
                        sector, Me.rblTipoEmpresa.SelectedValue, _
                        Me.txtCargoActual.Text, Me.txtDireccionEmpresa.Text, _
                        Me.txtTelefonoProfesional.Text, Me.txtCorreoProfesional.Text, _
                        meses, Me.txtFechaMat.Text, Me.txtConyuge.Text)
            obj.CerrarConexion()
            Response.Write("<script>alert('Ficha de Egresado Actualizada')</script>")
            CargarDatosPersonales()
            CargarDatosEgresado()            
        Catch ex As Exception
            Response.Write("<br />" & ex.Message & " -  " & ex.StackTrace)
        End Try
    End Sub
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
End Class
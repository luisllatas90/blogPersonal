Partial Class frmpersona
    Inherits System.Web.UI.Page
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                cargarDatosPagina()
                CargarDatosPersonales()
                CargarDatosEgresado()
            End If
            LoadDJ()
            cargarJS()

        Catch ex As Exception
            ' Response.Redirect("sesion.aspx")
            Response.Write(ex.Message & " -  " & ex.StackTrace)
        End Try      
    End Sub
    Sub LoadDJ()
        If Request.Form("aceptaDj") IsNot Nothing Then
            If Request.Form("aceptaDj") = "NO" Then
                InsertaDJ()
                Registrar()                
            End If
        End If
        VerificarDJ()
    End Sub
    Sub VerificarDJ()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("Alumni_AceptoDeclaracionJurada", decode(Me.hdcodigo_pso.Value))

        If dt.Rows(0).Item("id") <> "0" Then
            Me.cmdGuardar.CssClass = "guardar"
            Me.aceptaDj.Attributes.Add("value", "SI")
        Else
            Me.lblnombre.Text = dt.Rows(0).Item("nombreEgresado")
            Me.aceptaDj.Attributes.Add("value", "NO")
            Me.cmdGuardar.CssClass = "guardar  CallFancyBox_DeclaracionJurada"
        End If

    End Sub
    Sub InsertaDJ()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("ALUMNI_InsertaBitacoraUpdateDatos", decode(Me.hdcodigo_pso.Value), "DJ")
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Sub cargarDatosPagina()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ClsFunciones.LlenarListas(Me.dpdepartamento, obj.TraerDataTable("ConsultarLugares", 2, 156, 0), "codigo_dep", "nombre_dep", "--Seleccione--")
        Me.dpprovincia.Items.Add("--Seleccione--") : Me.dpprovincia.Items(0).Value = -1
        Me.dpdistrito.Items.Add("--Seleccione--") : Me.dpdistrito.Items(0).Value = -1
        obj.CerrarConexion()
        obj = Nothing
        Me.DDlAnio.Items.Add("Año")
        For i As Integer = (Now.Year - 17) To 1915 Step -1 'clluen
            Me.DDlAnio.Items.Add(i.ToString)
        Next
    End Sub
    Sub CargarDatosPersonales()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tbl As Data.DataTable
        tbl = obj.TraerDataTable("ALUMNI_ConsultarDatosEgresado", Session("codigo_alu"))
        If tbl.Rows.Count Then
            Me.hdcodigo_pso.Value = encode(tbl.Rows(0).Item("codigo_Pso"))
            Me.lblApellidoPat.Text = tbl.Rows(0).Item("apellidoPaterno_Pso")            'Apellido Paterno
            Me.lblApellidoMat.Text = tbl.Rows(0).Item("apellidoMaterno_Pso").ToString() 'Apellido Materno
            Me.lblNombres.Text = tbl.Rows(0).Item("nombres_Pso")                        'Nombres


            Me.lblnombre.Text = Session("nombreEgresado")
            If IsDate(tbl.Rows(0).Item("fechaNacimiento_Pso").ToString) Then
                DDlAnio.SelectedValue = Year(tbl.Rows(0).Item("fechaNacimiento_Pso").ToString)      'Año de Nacimiento
            End If
            Me.lblDNI.Text = tbl.Rows(0).Item("numeroDocIdent_Pso")                     'NroDocIdent

            If tbl.Rows(0).Item("fechanacimiento_pso").ToString >= Now Then
                'Me.lblanionac.Text = tbl.Rows(0).Item("anionac") * -1 + Now.Year         'edad
            Else
                'Me.lblanionac.Text = tbl.Rows(0).Item("anionac") * -1 + Now.Year - 1     'edad
            End If
            Me.txtEmailP.Text = tbl.Rows(0).Item("emailPrincipal_Pso").ToString
            Me.txtEmailA.Text = tbl.Rows(0).Item("emailAlternativo_Pso").ToString
            Me.txtPais.Text = tbl.Rows(0).Item("nombre_pai").ToString

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
            Me.txtCelular1.Text = tbl.Rows(0).Item("telefonoCelular_Pso").ToString
            Me.txtCelular2.Text = tbl.Rows(0).Item("telefonoCelular2_Pso").ToString

            Me.dpEstadoCivil.SelectedValue = -1
            If (tbl.Rows(0).Item("estadoCivil_Pso") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("estadoCivil_Pso").ToString.Trim <> "") Then
                Me.dpEstadoCivil.SelectedValue = tbl.Rows(0).Item("estadoCivil_Pso").ToString.ToUpper
            End If
            Me.dpSexo.SelectedValue = -1
            If (tbl.Rows(0).Item("sexo_Pso") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("sexo_Pso").ToString.Trim <> "") Then
                Me.dpSexo.SelectedValue = tbl.Rows(0).Item("sexo_Pso").ToString.ToUpper
            End If
            Me.dpReligion.SelectedValue = -1
            If (tbl.Rows(0).Item("religion_Dal") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("religion_Dal").ToString.Trim <> "") Then
                Me.dpReligion.SelectedValue = tbl.Rows(0).Item("religion_Dal").ToString.ToUpper
            End If
            Me.dpSacramento.SelectedValue = -1
            If (tbl.Rows(0).Item("ultimoSacramento_Dal") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("ultimoSacramento_Dal").ToString.Trim <> "") Then
                Me.dpSacramento.SelectedValue = tbl.Rows(0).Item("ultimoSacramento_Dal").ToString.ToUpper
            End If
            Me.dpTipoDocIdent.SelectedValue = -1
            If (tbl.Rows(0).Item("tipoDocIdent_Pso") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("tipoDocIdent_Pso").ToString.Trim <> "") Then
                Me.dpTipoDocIdent.SelectedValue = tbl.Rows(0).Item("tipoDocIdent_Pso").ToString.ToUpper
            End If
            Me.dpdianac.SelectedValue = -1
            If (tbl.Rows(0).Item("dianac") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("dianac").ToString.Trim <> "") Then
                Me.dpdianac.SelectedValue = tbl.Rows(0).Item("dianac").ToString.ToUpper
            End If
            Me.dpmesnac.SelectedValue = -1
            If (tbl.Rows(0).Item("mesnac") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("mesnac").ToString.Trim <> "") Then
                Me.dpmesnac.SelectedValue = tbl.Rows(0).Item("mesnac").ToString.ToUpper
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
        dt_Egresado = obj.TraerDataTable("ALUMNI_BuscaEgresado", decode(Me.hdcodigo_pso.Value))
        obj.CerrarConexion()
        obj = Nothing
        If (dt_Egresado.Rows.Count > 0) Then
            Me.HdFileFoto.Value = dt_Egresado.Rows(0).Item("foto_Ega")
            If (dt_Egresado.Rows(0).Item("foto_Ega").ToString.Trim <> "") Then
                FotoAlumno.ImageUrl = "../fotos/" & dt_Egresado.Rows(0).Item("foto_Ega")
            Else
                FotoAlumno.ImageUrl = IIf(Me.dpSexo.SelectedValue = "F", "../archivos/female.png", "../archivos/male.png")
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
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Registrar()
    End Sub

    Sub Registrar()
        Try
            If validar() Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                Dim nomFoto As String = ""
                Dim strRutaFoto As String
                Dim archivo As String = ""
                Dim sw As Byte = 0
                Dim dian As String = Me.dpdianac.SelectedValue()
                Dim mesn As String = Me.dpmesnac.SelectedValue()
                Dim anio As String = Me.DDlAnio.SelectedValue()
                'Dim fechaNac As String = anio + "-" + mesn + "-" + dian
                Dim fechaNac As String = dian + "-" + mesn + "-" + anio
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
                '#Subir Foto
                If (Me.fileFoto.HasFile = True) Then
                    strRutaFoto = Server.MapPath("..\fotos")
                    nomFoto = archivo & System.IO.Path.GetExtension(Me.fileFoto.FileName).ToString
                    Me.fileFoto.PostedFile.SaveAs(strRutaFoto & "\" & nomFoto)

                    '#########################
                    'Insertar en bitácora
                    obj.AbrirConexion()
                    obj.Ejecutar("ALUMNI_InsertaBitacoraUpdateDatos", CInt(decode(Me.hdcodigo_pso.Value)), "FO")
                    obj.CerrarConexion()
                    '#########################
                Else
                    nomFoto = Me.HdFileFoto.Value
                End If
                Dim mensaje As String = ""

                '"Registro de Datos Personales
                obj.AbrirConexion()
                obj.Ejecutar("ALUMNI_ActualizaDatosCV", Session("codigo_alu"), Me.txtEmailP.Text, Me.txtEmailA.Text _
                  , Me.txtDir.Text, Me.dpdistrito.SelectedValue, Me.txtFijo.Text.Trim, Me.txtCelular1.Text.Trim _
                  , Me.txtCelular2.Text.Trim, Me.dpSexo.SelectedValue, Me.dpEstadoCivil.SelectedItem.Text, fechaNac _
                  , Me.dpReligion.SelectedValue, Me.dpSacramento.SelectedValue)
                obj.CerrarConexion()
                '#Registro de Datos Profesionales
                obj.AbrirConexion()
                obj.Ejecutar("ALUMNI_ActualizaDatosEgre", Session("codigo_alu"), "D", "", "", "", "", "", "", nomFoto)
                obj.CerrarConexion()
                Response.Redirect("perfil.aspx")

            End If
        Catch ex As Exception
            Response.Write("<br />" & ex.Message & " -  " & ex.StackTrace)
        End Try
    End Sub
    Function validar() As Boolean
        Dim mensaje As String = ""
        If Me.txtDir.Text.Trim = "" Then
            mensaje &= "</br>Debe registrar su dirección."
        End If
        If Me.txtFijo.Text.Trim = "" And Me.txtCelular1.Text.Trim = "" And Me.txtCelular2.Text.Trim = "" Then
            mensaje &= "</br>* Favor de indicar al menos un número de contacto."
        End If
        If Me.txtEmailP.Text.Trim = "" And Me.txtEmailA.Text.Trim = "" Then
            mensaje &= "</br>* Favor de indicar al menos una Dirección de Correo Electrónico."
        End If
        If HdFileFoto.Value.ToString = "" And Me.fileFoto.HasFile = False Then
            mensaje &= "</br>* Debe subir su Foto"
        End If
        If mensaje <> "" Then
            lblMensajeFrm.Text = mensaje
            Return False
        Else
            lblMensajeFrm.Text = ""
            Return True
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


    Sub cargarJS()
        Dim ruta As String = ""
        ruta = Server.MapPath("..\") & "\jquery\empresas.js"
        Dim fileCreatedDate As DateTime = System.IO.File.GetLastWriteTime(ruta)
        fileCreatedDate.Date.ToShortDateString()
        If Date.Today.ToShortDateString <> fileCreatedDate.Date.ToShortDateString() Then
            System.IO.File.Delete(ruta)
            System.IO.File.AppendAllText(ruta, crearJS("ALUMNI_ListarEmpresas", "txtinstitucion"))
            ruta = Server.MapPath("..\") & "\jquery\ciudades.js"
            System.IO.File.Delete(ruta)
            System.IO.File.AppendAllText(ruta, crearJS("ALUMNI_ListarCiudades", "txtciudad"))
            ruta = Server.MapPath("..\") & "\jquery\sectores.js"
            System.IO.File.Delete(ruta)
            System.IO.File.AppendAllText(ruta, crearJS("ALUMNI_ListarSector", "txtSector"))
            ruta = Server.MapPath("..\") & "\jquery\areas.js"
            System.IO.File.Delete(ruta)
            System.IO.File.AppendAllText(ruta, crearJS("ALUMNI_ListarAreas", "txtarea"))
            ruta = Server.MapPath("..\") & "\jquery\cargos.js"
            System.IO.File.Delete(ruta)
            System.IO.File.AppendAllText(ruta, crearJS("ALUMNI_ListarCargo", "txtcargo"))
            ruta = Server.MapPath("..\") & "\jquery\tipocontrato.js"
            System.IO.File.Delete(ruta)
            System.IO.File.AppendAllText(ruta, crearJS("ALUMNI_ListarTipoContrato", "txttipocontrato"))
        End If
    End Sub

    Function crearJS(ByVal sp As String, ByVal ctrl As String) As String
        Dim cadena As String = ""
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable(sp)
        obj.CerrarConexion()
        If dt.Rows.Count Then
            cadena = "$(function() { var availableTags = ["""
            For i As Integer = 0 To dt.Rows.Count - 1
                cadena &= dt.Rows(i).Item("nombre").ToString.Trim & ""","""
            Next
            cadena = cadena.Substring(0, cadena.Length - 2)
            cadena &= "]; $( ""#" & ctrl & """ ).autocomplete({ source: availableTags });});"
        End If
        Return cadena
    End Function
    Function encode(ByVal str As String) As String
        Return (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(str)))
    End Function
    Function decode(ByVal str As String) As String
        Return System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(str))
    End Function
End Class

Partial Class administrativo_propuestas1_secretariageneral_convenios_frmRegistrarConvenio
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
           
            Session("codigo_cni") = (Request.QueryString("codigo_cni"))
            CargarListas()
            CargarPalabras()

            If Session("codigo_cni") = "" Then
                Session("codigo_cni") = 0
                Me.btnQuitar.Enabled = False
            Else
                CargarConvenio()
            End If
            'If Request.QueryString("modifica") = "1" Then
            '   
            'End If

        End If

    End Sub

    Private Sub CargarConvenio()
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try           
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarConveniosV2", Session("codigo_cni"))
            obj.CerrarConexion()

            If dt.Rows.Count Then
                Me.txtCodigo.Text = dt.Rows(0).Item("codigoIden_cni").ToString
                Me.ddlInstitucion.SelectedValue = dt.Rows(0).Item("codigo_Ins")
                ' Me.txtDenominacion.Text = dt.Rows(0).Item("denominacion_Cni").ToString
                Me.txtDescripcion.Text = dt.Rows(0).Item("descripcion_cni").ToString
                Me.txtAlcance.Text = dt.Rows(0).Item("alcance_cni").ToString
                Me.txtVinculo.Text = dt.Rows(0).Item("vinculos_cni").ToString
                Me.txtduracion.Text = dt.Rows(0).Item("Duracion_Cni").ToString
                If (dt.Rows(0).Item("periodoDuracion_Cni").ToString = "") Then
                    Me.ddlPeriodo.SelectedIndex = 0
                Else
                    Me.ddlPeriodo.SelectedValue = dt.Rows(0).Item("periodoDuracion_Cni")
                End If

                Me.chkDuracionIndefinida.Checked = IIf(dt.Rows(0).Item("Duracion_Cni").ToString.Length = 0, True, False)
                Me.chkRenovacion.Checked = IIf(dt.Rows(0).Item("renovacion_Cni").ToString = "0", False, True)
                Me.txtDesde.Text = dt.Rows(0).Item("fechaInicio_Cni")
                Me.ddlModalidad.SelectedValue = dt.Rows(0).Item("codigo_Mdc")
                Me.ddlAmbito.SelectedValue = dt.Rows(0).Item("codigo_Amc")
                Me.ddlSector.SelectedValue = dt.Rows(0).Item("codigo_sec")
                Me.ddlCco.SelectedValue = dt.Rows(0).Item("codigo_cco")
                Me.ddlResponsable.SelectedValue = dt.Rows(0).Item("codigo_Per")
                Me.txtContacto.Text = dt.Rows(0).Item("contacto_cni").ToString
                Me.txtSitioweb.Text = dt.Rows(0).Item("sitioweb_cni").ToString
                Me.txtEmail.Text = dt.Rows(0).Item("email_cni").ToString
                Me.txtObservacion.Text = dt.Rows(0).Item("observacion_Cni").ToString
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Sub CargarListas()
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            Dim objFun As New ClsFunciones
            objFun.CargarListas(Me.ddlInstitucion, obj.TraerDataTable("ConsultarInstitucion", "LI", "", 0), "codigo_Ins", "Institucion", "<<Seleccione>>")
            objFun.CargarListas(Me.ddlModalidad, obj.TraerDataTable("ConsultarModalidadConvenio", "TO", 0), "codigo_mdc", "descripcion_mdc", "<<Seleccione>>")
            objFun.CargarListas(Me.ddlResponsable, obj.TraerDataTable("ConsultarPersonal", "LI", ""), "codigo_per", "personal", "<<Seleccione>>")
            objFun.CargarListas(Me.ddlSector, obj.TraerDataTable("SECTORCONVENIO_LISTAR"), "codigo_sec", "nombre_sec", "<<Seleccione>>")
            objFun.CargarListas(Me.ddlCco, obj.TraerDataTable("CentroCostosConvenio_LISTAR"), "codigo_cco", "descripcion_cco", "<<Seleccione>>")
            objFun.CargarListas(Me.ddlAmbito, obj.TraerDataTable("ConsultarAmbitoConvenio", "TO", 0), "codigo_amc", "descripcion_amc", "<<Seleccione>>")
            objFun = Nothing

            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Sub CargarPalabras()
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            Me.gvPalabrasDisponible.DataSource = obj.TraerDataTable("ConvenioKeys_listar", "D", CInt(Session("codigo_cni")))
            Me.gvPalabrasDisponible.DataBind()
            Me.gvPalabrasRegistradas.DataSource = obj.TraerDataTable("ConvenioKeys_listar", "R", CInt(Session("codigo_cni")))
            Me.gvPalabrasRegistradas.DataBind()
            obj.CerrarConexion()
            obj = Nothing

            If gvPalabrasRegistradas.Rows.Count Then
                Me.btnQuitar.Enabled = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar.Click
        RegistrarActualizarConvenio()
    End Sub
    Sub CargarArchivoPDF()
        Dim path As String = Server.MapPath("../../../../../convenios/")
        Dim fileOK As Boolean = False
        If FileUpload1.HasFile Then
            Dim fileExtension As String
            fileExtension = System.IO.Path. _
                GetExtension(FileUpload1.FileName).ToLower()
            Dim allowedExtensions As String() = _
                {".pdf"}
            For i As Integer = 0 To allowedExtensions.Length - 1
                If fileExtension = allowedExtensions(i) Then
                    fileOK = True
                End If
            Next
            If fileOK Then
                Try
                    FileUpload1.PostedFile.SaveAs(path & Session("codigo_cni") & ".pdf")
                    '   Response.Write("File uploaded!")
                Catch ex As Exception
                    ' Response.Write("File could not be uploaded.")
                End Try
            Else
                ' Response.Write("Cannot accept files of this type.")
            End If
        End If

    End Sub
    Sub RegistrarActualizarConvenio()
        Dim obj As New ClsConectarDatos
        Dim editar As Boolean = True
        Dim periodo As Integer = Nothing
        Dim duracion As Integer = Nothing
        Try

            If Session("codigo_cni") = 0 Then
                editar = False
            End If

            Dim tbcin As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.IniciarTransaccion()


            If Me.txtduracion.Text.Trim = "" Then
                duracion = Nothing
            Else
                periodo = Me.ddlPeriodo.SelectedValue
                duracion = CInt(Me.txtduracion.Text)
            End If

            tbcin = obj.TraerDataTable("RegistraConvenioInstitucional", Session("codigo_cni"), "", ddlAmbito.SelectedValue, Me.ddlModalidad.SelectedValue, duracion, periodo, CDate(Me.txtDesde.Text), IIf(Me.chkRenovacion.Checked, 1, 0), Me.txtObservacion.Text, "", Me.txtCodigo.Text, Me.ddlSector.SelectedValue, Me.txtContacto.Text, Me.txtEmail.Text, Me.txtSitioweb.Text, Me.txtDescripcion.Text, Me.txtAlcance.Text, Me.txtVinculo.Text, 0)
            Session("codigo_cni") = tbcin.Rows(0).Item(0)
            obj.Ejecutar("RegistraBitacoraConvenios", tbcin.Rows(0).Item(0), Request.QueryString("id"), "I", "Registro de Convenio: " & Session("codigo_cni") & " Código: " & Me.txtCodigo.Text)


            If Not editar Then
                obj.Ejecutar("RegistraResponsableConvenio", "NU", tbcin.Rows(0).Item(0), Me.ddlResponsable.SelectedValue, Me.ddlCco.SelectedValue)
                obj.Ejecutar("RegistraBitacoraConvenios", tbcin.Rows(0).Item(0), Request.QueryString("id"), "I", "Registro Responsable: " & Me.ddlResponsable.SelectedItem.Text & " cco: " & Me.ddlCco.SelectedItem.Text)

                obj.Ejecutar("RegistraConvenioInstitucion", "NU", tbcin.Rows(0).Item(0), Me.ddlInstitucion.SelectedValue, "", "", 0)
                obj.Ejecutar("RegistraBitacoraConvenios", tbcin.Rows(0).Item(0), Request.QueryString("id"), "I", "Registro de Institución: " & Me.ddlInstitucion.SelectedItem.Text)

            Else
                obj.Ejecutar("RegistraResponsableConvenio", "MO", tbcin.Rows(0).Item(0), Me.ddlResponsable.SelectedValue, Me.ddlCco.SelectedValue)
                obj.Ejecutar("RegistraBitacoraConvenios", tbcin.Rows(0).Item(0), Request.QueryString("id"), "M", "Modifica Responsable: " & Me.ddlResponsable.SelectedItem.Text & " cco: " & Me.ddlCco.SelectedItem.Text)

                obj.Ejecutar("RegistraConvenioInstitucion", "MO", tbcin.Rows(0).Item(0), Me.ddlInstitucion.SelectedValue, "", "", 0)
                obj.Ejecutar("RegistraBitacoraConvenios", tbcin.Rows(0).Item(0), Request.QueryString("id"), "I", "Modifica de Institución: " & Me.ddlInstitucion.SelectedItem.Text)

            End If

            CargarArchivoPDF()

            obj.TerminarTransaccion()
            obj.CerrarConexion()

            Limpiar()

            Response.Write("<script> alert('Se registró exitosamente')</script>")
        Catch ex As Exception
            obj.AbortarTransaccion()
            'Response.Write("<script> alert('Ocurrió un error')</script>")
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try
        obj = Nothing
    End Sub

    Sub Limpiar()
        Me.txtcodigo.text = ""
        Me.txtAlcance.Text = ""
        Me.txtContacto.Text = ""
        ' Me.txtDenominacion.Text = ""
        Me.txtDescripcion.Text = ""
        Me.txtDesde.Text = Today
        Me.txtduracion.Text = ""
        Me.txtEmail.Text = ""
        Me.txtObservacion.Text = ""
        Me.txtSitioweb.Text = ""
        Me.txtVinculo.Text = ""
        Me.ddlAmbito.SelectedIndex = -1
        Me.ddlCco.SelectedValue = -1
        Me.ddlInstitucion.SelectedValue = -1
        Me.ddlModalidad.SelectedValue = -1
        Me.ddlPeriodo.SelectedValue = 1
        Me.ddlResponsable.SelectedValue = -1
        Me.ddlSector.SelectedValue = -1
        Session("codigo_cni") = 0
    End Sub
    Protected Sub chkDuracionIndefinida_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDuracionIndefinida.CheckedChanged
        Me.txtduracion.Enabled = Not Me.chkDuracionIndefinida.Checked
        Me.ddlPeriodo.Enabled = Not Me.chkDuracionIndefinida.Checked
        Me.txtduracion.Text = ""

    End Sub

    Protected Sub btnAgregar_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            RegistrarActualizarConvenio()
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            Dim Fila As GridViewRow
            For i As Integer = 0 To Me.gvPalabrasDisponible.Rows.Count - 1
                Fila = Me.gvPalabrasDisponible.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegirD"), CheckBox).Checked
                If (valor = True) Then
                    obj.AbrirConexion()
                    obj.Ejecutar("ConvenioKeys_Registrar", Session("codigo_cni"), Me.gvPalabrasDisponible.DataKeys(i).Values("codigo_ckw").ToString())
                    obj.CerrarConexion()
                End If
            Next
            obj = Nothing
            CargarPalabras()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnQuitar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuitar.Click
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim Fila As GridViewRow
            For i As Integer = 0 To Me.gvPalabrasRegistradas.Rows.Count - 1
                Fila = Me.gvPalabrasRegistradas.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegirR"), CheckBox).Checked
                If (valor = True) Then
                    obj.AbrirConexion()
                    obj.Ejecutar("ConvenioKeys_Registrar", Session("codigo_cni"), Me.gvPalabrasRegistradas.DataKeys(i).Values("codigo_ckw").ToString())
                    obj.CerrarConexion()
                End If
            Next
            obj = Nothing
        Catch ex As Exception

        End Try
    End Sub
End Class

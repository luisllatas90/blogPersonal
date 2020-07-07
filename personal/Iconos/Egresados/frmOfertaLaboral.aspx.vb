
Partial Class Egresado_frmOfertaLaboral
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            Dim cls As New clsEnvioMensajeAlumni
            Dim dtDirector As New Data.DataTable
            dtDirector = cls.RetornaDirectorAlumni()

            If (dtDirector.Rows.Count = 0) Then
                Me.lblMensaje.Text = "Error al buscar el director de ALUMNI."
            Else                
                Me.HdDirectorAlumni.Value = dtDirector.Rows(0).Item(0).ToString

                Me.dpDuracion.Items.Add("DIAS")
                Me.dpDuracion.Items.Add("MESES")
                Me.dpDuracion.Items.Add("AÑO")

                Me.dpTrabajo.Items.Add("TIEMPO COMPLETO")
                Me.dpTrabajo.Items.Add("MEDIO TIEMPO")
                Me.dpTrabajo.Items.Add("POR HORAS")
                Me.dpTrabajo.Items.Add("BECAS/PRACTICAS")
                Me.dpTrabajo.Items.Add("DESDE CASA")

                CargaDepartamentos()
                CargaSectores()
                CargaCarreras()

                Me.gvDetalles.DataBind()
                Me.txtDuracion.Attributes.Add("onkeypress", "solonumeros() ;")
                MostrarBusqueda(False)

                Me.HdAccion.Value = "N"
                If (Request.QueryString("of") IsNot Nothing) Then
                    AsignaOferta(Request.QueryString("of"))
                    Me.HdAccion.Value = "M"
                End If

                Me.txtInicioPublica.Text = Now.ToString("dd/MM/yyyy")
                Me.txtFinPublica.Text = Now.ToString("dd/MM/yyyy")
            End If
        End If
    End Sub

    Private Sub AsignaOferta(ByVal codigo_ofe As String)
        Dim dtDatos As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtDatos = obj.TraerDataTable("ALUMNI_RetornaOferta", codigo_ofe)
        obj.CerrarConexion()

        If (dtDatos.Rows.Count > 0) Then
            Me.HdCodigo_Ofe.Value = dtDatos.Rows(0).Item("codigo_ofe")
            Me.txtTitulo.Text = dtDatos.Rows(0).Item("titulo_ofe")
            Me.txtEmpresa.Text = dtDatos.Rows(0).Item("nombrePro")
            Me.HdCodigo_pro.Value = dtDatos.Rows(0).Item("idPro")
            Me.txtContacto.Text = dtDatos.Rows(0).Item("contacto_ofe")
            Me.txtTelefono.Text = dtDatos.Rows(0).Item("telefonocontacto_ofe")
            Me.txtDescripcion.Text = dtDatos.Rows(0).Item("descripcion_ofe")
            Me.txtRequisitos.Text = dtDatos.Rows(0).Item("requisitos_ofe")
            Me.txtCorreo.Text = dtDatos.Rows(0).Item("correocontacto_ofe")
            Me.txtLugar.Text = (dtDatos.Rows(0).Item("lugar_ofe"))
            Me.dpTrabajo.Text = dtDatos.Rows(0).Item("tipotrabajo_ofe")
            Me.txtDuracion.Text = dtDatos.Rows(0).Item("duracion_ofe").ToString.Substring(0, 2)
            Me.dpDuracion.Text = dtDatos.Rows(0).Item("duracion_ofe").ToString.Substring(3).Trim
            Me.chkMostrar.Checked = dtDatos.Rows(0).Item("visible_ofe")
            Me.dpSector.Text = dtDatos.Rows(0).Item("codigo_sec")
            Me.txtInicioPublica.Text = dtDatos.Rows(0).Item("fechaInicioAnuncio")
            Me.txtFinPublica.Text = dtDatos.Rows(0).Item("fechaFinAnuncio")

            'Detalles de la Oferta
            Dim dtDetalle As New Data.DataTable
            obj.AbrirConexion()
            dtDetalle = obj.TraerDataTable("ALUMNI_DetalleOferta", Me.HdCodigo_Ofe.Value)
            obj.CerrarConexion()
            Me.gvDetalles.DataSource = dtDetalle
            Me.gvDetalles.DataBind()

            dtDetalle.Dispose()
        End If

        dtDatos.Dispose()
        obj = Nothing
    End Sub

    Private Sub CargaCarreras()
        Dim obj As New ClsConectarDatos
        Dim dtCarrera As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtCarrera = obj.TraerDataTable("ALUMNI_ListaCarreraProfesional", 0)
        obj.CerrarConexion()
        Me.dpCarrera.DataTextField = "nombre_Cpf"
        Me.dpCarrera.DataValueField = "codigo_Cpf"
        Me.dpCarrera.DataSource = dtCarrera
        Me.dpCarrera.DataBind()

        dtCarrera.Dispose()
        obj = Nothing
    End Sub

    Private Sub CargaDepartamentos()
        Dim obj As New ClsConectarDatos
        Dim dtDepartamento As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtDepartamento = obj.TraerDataTable("ALUMNI_BuscaDepartamento", 0)
        obj.CerrarConexion()

        Me.dpDepartamento.DataTextField = "nombre_Dep"
        Me.dpDepartamento.DataValueField = "codigo_Dep"
        Me.dpDepartamento.DataSource = dtDepartamento
        Me.dpDepartamento.DataBind()

        Me.dpDepartamento.SelectedValue = 13

        dtDepartamento.Dispose()
        obj = Nothing
    End Sub

    Private Sub CargaSectores()
        Dim obj As New ClsConectarDatos
        Dim dtSector As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtSector = obj.TraerDataTable("ALUMNI_BuscaSector", 0)
        obj.CerrarConexion()

        Me.dpSector.DataTextField = "nombre_sec"
        Me.dpSector.DataValueField = "codigo_sec"
        Me.dpSector.DataSource = dtSector
        Me.dpSector.DataBind()

        dtSector.Dispose()
        obj = Nothing
    End Sub

    Protected Sub btnBusca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBusca.Click
        Me.txtFiltro.Text = ""
        MostrarBusqueda(True)
    End Sub

    Private Sub MostrarBusqueda(ByVal sw As Boolean)
        Me.lblFiltroRuc.Visible = sw        
        Me.txtFiltro.Visible = sw
        Me.gvEmpresa.Visible = sw
        Me.btnBusqueda.Visible = sw
        Me.btnBusca.Visible = Not sw
    End Sub

    Protected Sub btnBusqueda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBusqueda.Click
        If (Me.txtFiltro.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar el ruc o el nombre de la empresa')</script>")
            Me.txtFiltro.Focus()
        Else
            BuscaEmpresas()
        End If

    End Sub

    Private Sub BuscaEmpresas()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim dtEmpresas As New Data.DataTable
        obj.AbrirConexion()
        dtEmpresas = obj.TraerDataTable("ALUMNI_BuscaEmpresa", 0, Me.txtFiltro.Text, Me.txtFiltro.Text)
        obj.CerrarConexion()

        Me.gvEmpresa.DataSource = dtEmpresas
        Me.gvEmpresa.DataBind()

        dtEmpresas.Dispose()
        obj = Nothing
    End Sub

    Protected Sub gvEmpresa_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles gvEmpresa.SelectedIndexChanging
        Me.HdCodigo_pro.Value = Me.gvEmpresa.DataKeys(e.NewSelectedIndex).Values(0)
        Me.txtEmpresa.Text = ReemplazaTildes(Me.gvEmpresa.Rows(e.NewSelectedIndex).Cells(1).Text)

        MostrarBusqueda(False)
    End Sub

    Private Function ReemplazaTildes(ByVal cadena As String) As String
        Dim NuevaCadena As String
        NuevaCadena = cadena
        NuevaCadena = NuevaCadena.Replace("&#193;", "A")
        NuevaCadena = NuevaCadena.Replace("&#201;", "E")
        NuevaCadena = NuevaCadena.Replace("&#205;", "I")
        NuevaCadena = NuevaCadena.Replace("&#211;", "O")
        NuevaCadena = NuevaCadena.Replace("&#217;", "U")
        NuevaCadena = NuevaCadena.Replace("&#218;", "U")
        NuevaCadena = NuevaCadena.Replace("&#209;", "Ñ")
        NuevaCadena = NuevaCadena.Replace("&amp;", "&")
        NuevaCadena = NuevaCadena.Replace("&#180;", "'")
        NuevaCadena = NuevaCadena.Replace("&quot;", "'")
        NuevaCadena = NuevaCadena.Replace("&#186;", "º")
        Return NuevaCadena
    End Function

    Protected Sub gvEmpresa_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvEmpresa.PageIndexChanging
        gvEmpresa.PageIndex = e.NewPageIndex()
        BuscaEmpresas()
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If (Me.gvDetalles.Rows.Count > 0) Then
                If (validaForm() = True) Then
                    Dim obj As New ClsConectarDatos
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    'Inserta Cabecera y Detalle
                    Dim dtCod As New Data.DataTable

                    Dim strDuracion As String = Me.txtDuracion.Text
                    While strDuracion.Length < 2
                        strDuracion = "0" & strDuracion
                    End While
                    strDuracion = strDuracion & " " & Me.dpDuracion.Text

                    Dim blnMostrar As Boolean = False
                    Dim strEstado As String = "R"
                    If (Request.QueryString("id") = Me.HdDirectorAlumni.Value) Then
                        blnMostrar = True
                        strEstado = "A"
                    Else
                        blnMostrar = False
                        strEstado = "R"
                    End If

                    obj.AbrirConexion()
                    dtCod = obj.TraerDataTable("ALUMNI_RegistraOferta", Me.HdCodigo_Ofe.Value, Me.HdCodigo_pro.Value, _
                                 Me.dpDepartamento.SelectedValue, Me.txtTitulo.Text, _
                                 Me.txtDescripcion.Text, Me.txtRequisitos.Text, _
                                 Me.txtContacto.Text, Me.txtCorreo.Text, _
                                 Me.txtTelefono.Text, Me.txtLugar.Text, _
                                 Me.dpTrabajo.SelectedValue, strDuracion, _
                                 Me.txtInicioPublica.Text, Me.txtFinPublica.Text, _
                                 Me.dpSector.SelectedValue, blnMostrar, strEstado)
                    Me.lblMensaje.Text = "Oferta Laboral Registrada"
                    obj.CerrarConexion()
                    'LimpiaControles()
                    Response.Redirect("frmListaOfertas.aspx?id=" & Request.QueryString("id"))
                End If
            ElseIf (Me.gvDetalles.Rows.Count = 0) Then
                Me.lblMensaje.Text = "Su oferta laboral no esta relacionado con ninguna carrera profesional"
            End If
            
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try        
    End Sub

    Private Function validaForm() As Boolean
        If (Me.txtTitulo.Text.Trim = "") Then
            Response.Write("<script>alert('Debe ingresar el nombre de la oferta laboral')</script>")
            Me.txtTitulo.Focus()
            Return False
        End If


        If (Me.txtDescripcion.Text.Trim = "") Then
            Response.Write("<script>alert('Debe describir la oferta laboral')</script>")
            Me.txtDescripcion.Focus()
            Return False
        End If

        If (Me.txtRequisitos.Text.Trim = "") Then
            Response.Write("<script>alert('Ingrese los requisitos para postular a su oferta de trabajo')</script>")
            Me.txtRequisitos.Focus()
            Return False
        End If

        If (Me.HdCodigo_pro.Value = "") Then
            Response.Write("<script>alert('Ingrese el nombre de la empresa')</script>")
            Me.txtEmpresa.Focus()
            Return False
        End If

        If (Me.txtLugar.Text.Trim = "") Then            
            Response.Write("<script>alert('Ingresar el lugar de la oferta laboral')</script>")
            Me.txtLugar.Focus()
            Return False
        End If

        If (Me.txtDuracion.Text.Trim = "") Then
            Response.Write("<script>alert('Ingrese la duración de la oferta')</script>")
            Me.txtDuracion.Focus()
            Return False
        End If

        If (Me.txtContacto.Text.Trim = "") Then
            Response.Write("<script>alert('Ingrese el contacto de la empresa')</script>")
            Me.txtContacto.Focus()
            Return False
        End If

        'If (Me.txtTelefono.Text.Trim = "" Or Me.txtCorreo.Text.Trim = "") Then
        '    Response.Write("<script>alert('Ingrese correo o teléfono del contacto')</script>")
        '    Me.txtTelefono.Focus()
        '    Return False
        'End If

        If (Me.txtInicioPublica.Text.Length = 10) Then
            If (Me.txtInicioPublica.Text.Trim = "") Then
                Response.Write("<script>alert('Debe ingresar la fecha de publicación de la oferta')</script>")
                Me.txtInicioPublica.Focus()
                Return False
            End If
        Else
            Response.Write("<script>alert('Formato de Fecha Incorrecto')</script>")
            Me.txtInicioPublica.Focus()
            Return False
        End If

        If (Me.txtFinPublica.Text.Length = 10) Then
            If (Me.txtFinPublica.Text.Trim = "") Then
                Response.Write("<script>alert('Debe ingresar la fecha final de la oferta')</script>")
                Me.txtFinPublica.Focus()
                Return False
            End If
        Else
            Response.Write("<script>alert('Formato de Fecha Incorrecto')</script>")
            Me.txtFinPublica.Focus()
            Return False
        End If

        Return True
    End Function

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            If (validaForm() = True) Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                If (VerificaDetalles() = False) Then
                    If (Me.HdCodigo_Ofe.Value = "") Then
                        'Inserta cabecera y detalle
                        Dim dtCod As New Data.DataTable
                        Dim strDuracion As String = Me.txtDuracion.Text
                        While strDuracion.Length < 2
                            strDuracion = "0" & strDuracion
                        End While
                        strDuracion = strDuracion & " " & Me.dpDuracion.Text

                        Dim blnMostrar As Boolean = False
                        Dim strEstado As String = "R"                        
                        If (Request.QueryString("id") = Me.HdDirectorAlumni.Value) Then
                            blnMostrar = True
                            strEstado = "A"
                        Else
                            blnMostrar = False
                            strEstado = "R"
                        End If

                        If (Me.HdAccion.Value = "N") Then
                            obj.IniciarTransaccion()
                            dtCod = obj.TraerDataTable("ALUMNI_RegistraOferta", 0, Me.HdCodigo_pro.Value, _
                                         Me.dpDepartamento.SelectedValue, Me.txtTitulo.Text, _
                                         Me.txtDescripcion.Text, Me.txtRequisitos.Text, _
                                         Me.txtContacto.Text, Me.txtCorreo.Text, _
                                         Me.txtTelefono.Text, Me.txtLugar.Text, _
                                         Me.dpTrabajo.SelectedValue, strDuracion, _
                                         Me.txtInicioPublica.Text, Me.txtFinPublica.Text, _
                                         Me.dpSector.SelectedValue, blnMostrar, _
                                         strEstado)

                            If (dtCod.Rows.Count > 0) Then
                                Me.HdCodigo_Ofe.Value = dtCod.Rows(0).Item(0).ToString
                                obj.Ejecutar("ALUMNI_RegistraDetalleOferta", Me.HdCodigo_Ofe.Value, Me.dpCarrera.SelectedValue)
                            End If

                            obj.TerminarTransaccion()
                        ElseIf (Me.HdAccion.Value = "M") Then
                            obj.AbrirConexion()
                            obj.Ejecutar("ALUMNI_RegistraDetalleOferta", Me.HdCodigo_Ofe.Value, Me.dpCarrera.SelectedValue)
                            obj.CerrarConexion()
                        End If
                        
                    Else
                        'Inserta Detalle
                        obj.AbrirConexion()
                        obj.Ejecutar("ALUMNI_RegistraDetalleOferta", Me.HdCodigo_Ofe.Value, Me.dpCarrera.SelectedValue)
                        obj.CerrarConexion()
                    End If
                End If
                obj = Nothing
                CargaDetalles()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try        
    End Sub

    Private Function VerificaDetalles() As Boolean
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim dtDetalle As New Data.DataTable
        obj.AbrirConexion()
        If (Me.HdCodigo_Ofe.Value = "") Then
            dtDetalle = obj.TraerDataTable("ALUMNI_ValidaDetalleOferta", 0, Me.dpCarrera.SelectedValue)
        Else
            dtDetalle = obj.TraerDataTable("ALUMNI_ValidaDetalleOferta", Me.HdCodigo_Ofe.Value, Me.dpCarrera.SelectedValue)
        End If
        obj.CerrarConexion()
        obj = Nothing

        If (dtDetalle.Rows.Count = 0) Then
            Me.lblMensaje.Text = ""
            Return False
        End If

        Me.lblMensaje.Text = "Ya registro esta carrera profesional"
        Return True
    End Function

    Private Sub CargaDetalles()        
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim dtDetalle As New Data.DataTable
        obj.AbrirConexion()
        dtDetalle = obj.TraerDataTable("ALUMNI_DetalleOferta", Me.HdCodigo_Ofe.Value)
        obj.CerrarConexion()
        Me.gvDetalles.DataSource = dtDetalle
        Me.gvDetalles.DataBind()

        dtDetalle.Dispose()
        obj = Nothing
    End Sub

    Protected Sub gvDetalles_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDetalles.RowDeleting
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("ALUMNI_EliminaDetalleOFerta", Me.gvDetalles.DataKeys(e.RowIndex).Values(0))
        obj.CerrarConexion()
        obj = Nothing
        CargaDetalles()
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        If (Me.HdAccion.Value = "N") Then
            'Elimina Detalle y Cabecera
            If (Me.HdCodigo_Ofe.Value <> "") Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString            
                obj.AbrirConexion()
                obj.Ejecutar("ALUMNI_EliminaRegistroOferta", Me.HdCodigo_Ofe.Value)
                obj.CerrarConexion()

                obj = Nothing
            End If
        End If
        Response.Redirect("frmListaOfertas.aspx")
    End Sub

    Private Sub LimpiaControles()
        Me.HdAccion.Value = "N"
        Me.HdCodigo_Ofe.Value = ""
        Me.HdCodigo_pro.Value = ""
        Me.txtDuracion.Text = ""
        Me.txtTitulo.Text = ""
        Me.txtInicioPublica.Text = ""
        Me.txtFinPublica.Text = ""
        Me.txtRequisitos.Text = ""
        Me.txtDescripcion.Text = ""
        Me.txtEmpresa.Text = ""
        Me.txtTelefono.Text = ""
        Me.txtContacto.Text = ""
        Me.txtCorreo.Text = ""
    End Sub
End Class

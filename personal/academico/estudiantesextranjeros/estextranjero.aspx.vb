
Partial Class librerianet_estudiantesextranjeros_estextranjero
    Inherits System.Web.UI.Page

    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Dim objcnx As New ClsConectarDatos
        objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objcnx.AbrirConexion()
        'gvdocentes.DataSource = objcnx.TraerDataTable("EAD_ConsultarPDP", cboDepartamento.SelectedValue)
        'objcnx.Ejecutar ("registrarESTEXT",Me.txtApellidos.Text,...) 
        Me.txtcarnet.text = Me.txtcarnet.text.trim
        Me.txtapellidos.text = Me.txtapellidos.Text.ToUpper.Trim
        Me.txtnombres.text = Me.txtnombres.text.toUpper.Trim
        'Me.txtfechaNacimiento.text 
        'me.cbosexo.text ="M"
        'me.cboconvenio.text ="S"
        Me.txtpasaporte.text = Me.txtpasaporte.text.trim
        Me.txtnacionalidad.text = Me.txtnacionalidad.text.toupper.trim
        Me.txtpais_residencia.text = Me.txtpais_residencia.text.toupper.trim
        Me.txtlocalidad.text = Me.txtlocalidad.text.toupper.trim
        Me.txtdireccionPermanente.text = Me.txtdireccionPermanente.text.toupper.trim
        Me.txtzipcode.text = Me.txtzipcode.text.trim
        Me.txtgrupoSangineo.text = Me.txtgrupoSangineo.text.trim
        Me.txtuniversidadOrigen.text = Me.txtuniversidadOrigen.text.toupper.trim
        Me.txtpaisUniversidadOrigen.text = Me.txtpaisUniversidadOrigen.text.toupper.trim
        Me.txtfaculty.text = Me.txtfaculty.text.toupper.trim
        Me.txtsemetreActual.text = Me.txtsemetreActual.text.toupper.trim
        Me.txtescuelausat.text = Me.txtescuelausat.text.toupper.trim
        'me.txtfechaInicial.text =now()
        'me.txtfechaFinal.text = now()
        Me.txteMail_Alu.text = Me.txteMail_Alu.text.trim
        Me.txttelefonos.text = Me.txttelefonos.text.trim
        Me.txtcontactnombre1.text = Me.txtcontactnombre1.text.trim
        Me.txtcontactparentesco1.text = Me.txtcontactparentesco1.text.trim
        Me.txtcontactemail1.text = Me.txtcontactemail1.text.trim
        Me.txtcontacttelefono1.text = Me.txtcontacttelefono1.text.trim
        Me.txtcontactnombre2.text = Me.txtcontactnombre2.text.trim
        Me.txtcontactparentesco2.text = Me.txtcontactparentesco2.text.trim
        Me.txtcontactemail2.text = Me.txtcontactemail2.text.trim
        Me.txtcontacttelefono2.text = Me.txtcontacttelefono2.text.trim
        Me.txtcontactnombre3.text = Me.txtcontactnombre3.text.trim
        Me.txtcontactparentesco3.text = Me.txtcontactparentesco3.text.trim
        Me.txtcontactemail3.text = Me.txtcontactemail3.text.trim
        Me.txtcontacttelefono3.text = Me.txtcontacttelefono3.text.trim
        Me.txtcurso1.text = Me.txtcurso1.text.trim
        Me.txtcalificativo1.text = Me.txtcalificativo1.text.trim
        Me.txtcurso2.text = Me.txtcurso2.text.trim
        Me.txtcalificativo2.text = Me.txtcalificativo2.text.trim
        Me.txtcurso3.text = Me.txtcurso3.text.trim
        Me.txtcalificativo3.text = Me.txtcalificativo3.text.trim
        Me.txtcurso4.text = Me.txtcurso4.text.trim
        Me.txtcalificativo4.text = Me.txtcalificativo4.text.trim
        Me.txtcurso5.text = Me.txtcurso5.text.trim
        Me.txtcalificativo5.text = Me.txtcalificativo5.text.trim
        Me.txtcurso6.text = Me.txtcurso6.text.trim
        Me.txtcalificativo6.text = Me.txtcalificativo6.text.trim
        Me.txtcomentariofinal.text = Me.txtcomentariofinal.text.trim
        Me.txtcomentariocoordinador.text = Me.txtcomentariocoordinador.text.trim

        objcnx.Ejecutar("AgregarEstudianteExtranjero", Me.txtcarnet.text, Me.txtapellidos.text, Me.txtnombres.text, CDate(Me.txtfechaNacimiento.text), Me.cbosexo.text, Me.cboconvenio.text, Me.txtpasaporte.text, Me.txtnacionalidad.text, Me.txtpais_residencia.text, Me.txtlocalidad.text, Me.txtdireccionPermanente.text, Me.txtzipcode.text, Me.txtgrupoSangineo.text, Me.txtuniversidadOrigen.text, Me.txtpaisUniversidadOrigen.text, Me.txtfaculty.text, Me.txtsemetreActual.text, Me.txtescuelausat.text, CDate(Me.txtfechaInicial.text), CDate(Me.txtfechaFinal.text), Me.txteMail_Alu.text, Me.txttelefonos.text, Me.txtcontactnombre1.text, Me.txtcontactparentesco1.text, Me.txtcontactemail1.text, Me.txtcontacttelefono1.text, Me.txtcontactnombre2.text, Me.txtcontactparentesco2.text, Me.txtcontactemail2.text, Me.txtcontacttelefono2.text, Me.txtcontactnombre3.text, Me.txtcontactparentesco3.text, Me.txtcontactemail3.text, Me.txtcontacttelefono3.text, Me.txtcurso1.text, Me.txtcalificativo1.text, Me.txtcurso2.text, Me.txtcalificativo2.text, Me.txtcurso3.text, Me.txtcalificativo3.text, Me.txtcurso4.text, Me.txtcalificativo4.text, Me.txtcurso5.text, Me.txtcalificativo5.text, Me.txtcurso6.text, Me.txtcalificativo6.text, Me.txtcomentariofinal.text, Me.txtcomentariocoordinador.text)

        objcnx.CerrarConexion()
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ' Inicializar los textbox antes de empezar a llenar 
        Me.txtfechaNacimiento.text = DateSerial(Year(now()) - 20, 1, 1)
        Me.cbosexo.text = "M"
        If month(now()) > 2 And month(now()) < 8 Then
            Me.txtfechaInicial.text = DateSerial(Year(now()), 4, 1)
        End If
        If month(now()) > 7 And month(now()) <= 12 Then
            Me.txtfechaInicial.text = DateSerial(Year(now()), 8, 1)
        End If

        If month(now()) > 2 And month(now()) < 8 Then
            Me.txtfechaFinal.text = DateSerial(Year(now()), 7, 20)
        End If
        If month(now()) > 7 And month(now()) <= 12 Then
            Me.txtfechaFinal.text = DateSerial(Year(now()), 12, 15)
        End If
    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        ' Limpiar los TextBox cuando se presiona el boton cancelar
        Me.txtcarnet.text = space(10)
        Me.txtapellidos.text = space(50)
        Me.txtnombres.text = space(50)
        Me.txtfechaNacimiento.text = now()
        Me.cbosexo.text = "M"
        Me.cboconvenio.text = "S"
        Me.txtpasaporte.text = space(50)
        Me.txtnacionalidad.text = space(50)
        Me.txtpais_residencia.text = space(50)
        Me.txtlocalidad.text = space(50)
        Me.txtdireccionPermanente.text = space(100)
        Me.txtzipcode.text = space(50)
        Me.txtgrupoSangineo.text = space(50)
        Me.txtuniversidadOrigen.text = space(50)
        Me.txtpaisUniversidadOrigen.text = space(50)
        Me.txtfaculty.text = space(50)
        Me.txtsemetreActual.text = space(50)
        Me.txtescuelausat.text = space(50)

        If month(now()) > 2 And month(now()) < 8 Then
            Me.txtfechaInicial.text = DateSerial(Year(now()), 4, 1)
        End If
        If month(now()) > 7 And month(now()) <= 12 Then
            Me.txtfechaInicial.text = DateSerial(Year(now()), 8, 1)
        End If

        If month(now()) > 2 And month(now()) < 8 Then
            Me.txtfechaFinal.text = DateSerial(Year(now()), 7, 20)
        End If
        If month(now()) > 7 And month(now()) <= 12 Then
            Me.txtfechaFinal.text = DateSerial(Year(now()), 12, 15)
        End If
        Me.txteMail_Alu.text = space(100)
        Me.txttelefonos.text = space(50)
        Me.txtcontactnombre1.text = space(50)
        Me.txtcontactparentesco1.text = space(50)
        Me.txtcontactemail1.text = space(50)
        Me.txtcontacttelefono1.text = space(50)
        Me.txtcontactnombre2.text = space(50)
        Me.txtcontactparentesco2.text = space(50)
        Me.txtcontactemail2.text = space(50)
        Me.txtcontacttelefono2.text = space(50)
        Me.txtcontactnombre3.text = space(50)
        Me.txtcontactparentesco3.text = space(50)
        Me.txtcontactemail3.text = space(50)
        Me.txtcontacttelefono3.text = space(50)
        Me.txtcurso1.text = space(50)
        Me.txtcalificativo1.text = space(10)
        Me.txtcurso2.text = space(50)
        Me.txtcalificativo2.text = space(10)
        Me.txtcurso3.text = space(50)
        Me.txtcalificativo3.text = space(10)
        Me.txtcurso4.text = space(50)
        Me.txtcalificativo4.text = space(10)
        Me.txtcurso5.text = space(50)
        Me.txtcalificativo5.text = space(10)
        Me.txtcurso6.text = space(50)
        Me.txtcalificativo6.text = space(10)
        Me.txtcomentariofinal.text = space(255)
        Me.txtcomentariocoordinador.text = space(255)
    End Sub
End Class

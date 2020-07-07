
Partial Class librerianet_estudiantesextranjeros_padronextranjeros
    Inherits System.Web.UI.Page


    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim ObjCnx As New ClsConectarDatos
        Dim Datos As Data.DataTable
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        ObjCnx.AbrirConexion()
        Datos = ObjCnx.TraerDataTable("dbo.EXT_ConsultarAlumnoExtranjero", Me.GridView1.DataKeys.Item(Me.GridView1.SelectedIndex).Values(0))
        With Datos.Rows(0)
            Me.txtcarnet.text = .item("carnet")
            Me.txtapellidos.text = .item("apellidos")
            Me.txtnombres.text = .item("nombres")
            Me.txtfechaNacimiento.text = .item("fechaNacimiento")
            Me.cbosexo.text = .item("sexo")
            Me.cboconvenio.text = .item("convenio")
            Me.txtpasaporte.text = .item("pasaporte")
            Me.txtnacionalidad.text = .item("nacionalidad")
            Me.txtpais_residencia.text = .item("pais_residencia")
            Me.txtlocalidad.text = .item("localidad")
            Me.txtdireccionPermanente.text = .item("direccionPermanente")
            Me.txtzipcode.text = .item("zipcode")
            Me.txtgrupoSangineo.text = .item("grupoSangineo")
            Me.txtuniversidadOrigen.text = .item("universidadOrigen")
            Me.txtpaisUniversidadOrigen.text = .item("paisUniversidadOrigen")
            Me.txtfaculty.text = .item("faculty")
            Me.txtsemetreActual.text = .item("semetreActual")
            Me.txtescuelausat.text = .item("escuelausat")
            Me.txtfechaInicial.text = .item("fechaInicial")
            Me.txtfechaFinal.text = .item("fechaFinal")
            Me.txteMail_Alu.text = .item("eMail_Alu")
            Me.txttelefonos.text = .item("telefonos")
            Me.txtcontactnombre1.text = .item("contactnombre1")
            Me.txtcontactparentesco1.text = .item("contactparentesco1")
            Me.txtcontactemail1.text = .item("contactemail1")
            Me.txtcontacttelefono1.text = .item("contacttelefono1")
            Me.txtcontactnombre2.text = .item("contactnombre2")
            Me.txtcontactparentesco2.text = .item("contactparentesco2")
            Me.txtcontactemail2.text = .item("contactemail2")
            Me.txtcontacttelefono2.text = .item("contacttelefono2")
            Me.txtcontactnombre3.text = .item("contactnombre3")
            Me.txtcontactparentesco3.text = .item("contactparentesco3")
            Me.txtcontactemail3.text = .item("contactemail3")
            Me.txtcontacttelefono3.text = .item("contacttelefono3")
            Me.txtcurso1.text = .item("curso1")
            Me.txtcalificativo1.text = .item("calificativo1")
            Me.txtcurso2.text = .item("curso2")
            Me.txtcalificativo2.text = .item("calificativo2")
            Me.txtcurso3.text = .item("curso3")
            Me.txtcalificativo3.text = .item("calificativo3")
            Me.txtcurso4.text = .item("curso4")
            Me.txtcalificativo4.text = .item("calificativo4")
            Me.txtcurso5.text = .item("curso5")
            Me.txtcalificativo5.text = .item("calificativo5")
            Me.txtcurso6.text = .item("curso6")
            Me.txtcalificativo6.text = .item("calificativo6")
            Me.txtcomentariofinal.text = .item("comentariofinal")
            Me.txtcomentariocoordinador.text = .item("comentariocoordinador")
        End With
        ObjCnx.CerrarConexion()
    End Sub

 
    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Dim ObjCnx As New ClsConectarDatos
        Dim Datos As Data.DataTable
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        ObjCnx.AbrirConexion()
        ObjCnx.Ejecutar("dbo.EXT_ActualizaAlumnoExtranjero", Me.GridView1.DataKeys.Item(Me.GridView1.SelectedIndex).Values(0), Me.txtcarnet.text, Me.txtapellidos.text, Me.txtnombres.text, Me.txtfechaNacimiento.text, Me.cbosexo.text, Me.cboconvenio.text, Me.txtpasaporte.text, Me.txtnacionalidad.text, Me.txtpais_residencia.text, Me.txtlocalidad.text, Me.txtdireccionPermanente.text, Me.txtzipcode.text, Me.txtgrupoSangineo.text, Me.txtuniversidadOrigen.text, Me.txtpaisUniversidadOrigen.text, Me.txtfaculty.text, Me.txtsemetreActual.text, Me.txtescuelausat.text, Me.txtfechaInicial.text, Me.txtfechaFinal.text, Me.txteMail_Alu.text, Me.txttelefonos.text, Me.txtcontactnombre1.text, Me.txtcontactparentesco1.text, Me.txtcontactemail1.text, Me.txtcontacttelefono1.text, Me.txtcontactnombre2.text, Me.txtcontactparentesco2.text, Me.txtcontactemail2.text, Me.txtcontacttelefono2.text, Me.txtcontactnombre3.text, Me.txtcontactparentesco3.text, Me.txtcontactemail3.text, Me.txtcontacttelefono3.text, Me.txtcurso1.text, Me.txtcalificativo1.text, Me.txtcurso2.text, Me.txtcalificativo2.text, Me.txtcurso3.text, Me.txtcalificativo3.text, Me.txtcurso4.text, Me.txtcalificativo4.text, Me.txtcurso5.text, Me.txtcalificativo5.text, Me.txtcurso6.text, Me.txtcalificativo6.text, Me.txtcomentariofinal.text, Me.txtcomentarioCoordinador.text)
        ObjCnx.CerrarConexion()
        Me.GridView1.databind()
    End Sub
End Class

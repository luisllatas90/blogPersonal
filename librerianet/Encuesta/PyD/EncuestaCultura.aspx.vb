
Partial Class librerianet_Encuesta_PyD_EncuestaCultura
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim ObjCnx As New ClsConectarDatos
            Dim datosAlu, datosEsc As New Data.DataTable
            Dim ciclo() As Integer = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            datosAlu = ObjCnx.TraerDataTable("consultarAlumnoGeneral", "CO", mid(Request.QueryString("codigo_alu"), 1, Request.QueryString("codigo_alu").Length - 2))
            If datosAlu.Rows.Count > 0 Then
                Me.cboSexo.SelectedValue = IIf(datosAlu.Rows(0).Item("sexo_alu") = "F", 2, 1)
                Me.txtEscuela.Text = datosAlu.Rows(0).Item("nombre_cpf")
                Me.txtEdad.Text = Date.Now.Year - CDate(datosAlu.Rows(0).Item("fechaNacimiento_Alu")).Year
                datosEsc = ObjCnx.TraerDataTable("SOL_ConsultarCarreraProfesionalPE", Request.QueryString("codigo_alu"))
                If datosEsc.Rows.Count > 0 Then
                    If datosEsc.Rows(0).Item("PPcodigo_cpf") IsNot DBNull.Value Then
                        Me.txtEscuela.Text = Me.txtEscuela.Text & ": " & datosEsc.Rows(0).Item("PPnombre_cpf")
                    End If
                End If

                '#### Llenar provincia ####
                ClsFunciones.LlenarListas(cboProvincia, ObjCnx.TraerDataTable("sp_ver_provincia", 13), "codigo_pro", "nombre_pro", "--Seleccione Provincia--")
                ClsFunciones.LlenarListas(cboDistrito, ObjCnx.TraerDataTable("sp_ver_distrito", Me.cboProvincia.SelectedValue), "codigo_dis", "nombre_dis", "--Seleccione Distrito--")
            End If
            ObjCnx.CerrarConexion()
            cboCiclo.DataSource = ciclo
            cboCiclo.DataBind()
            txtNroLibrosLeidos.Attributes.Add("onKeyPress", "validarnumero()")
            cboIdentificacion.Attributes.Add("onClick", "HabilitaEspecificar(this, " & Me.txtIdentificacion.ID & ", 6)")
            cboGrupoCultural.Attributes.Add("onClick", "HabilitaEspecificar(this, " & Me.txtGrupoCultural.ID & ", 1)") ' P8
            cboArtesPlasticas.Attributes.Add("onClick", "HabilitaEspecificar(this, " & Me.txtArtesPlasticas.ID & ", 1)") ' P10
            cboIncursionarArte.Attributes.Add("onClick", "HabilitaEspecificar(this, " & Me.txtIncursionarArte.ID & ", 2)") 'P11
            cboPreferenciaDanzas.Attributes.Add("onClick", "HabilitaEspecificar(this, " & Me.txtPreferenciaDanzas.ID & ", 1)") ' P18
            cboConocesDanzas.Attributes.Add("onClick", "HabilitaEspecificar(this, " & Me.txtConocesDanzas.ID & ", 1)") ' P19
            cblTipoMusica.Attributes.Add("onClick", "HabilitaEspecificaTexto(" & Me.txtEspecifiqueMusica.ID & ", 'cblTipoMusica_8')") ' P19
            cblExpCulturales.Attributes.Add("onClick", "HabilitaEspecificaMultiple('cblExpCulturales_')") ' P21 musica
        End If
    End Sub

    Protected Sub cboProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProvincia.SelectedIndexChanged
        Dim ObjCnx As New ClsConectarDatos
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        ObjCnx.AbrirConexion()
        ClsFunciones.LlenarListas(cboDistrito, ObjCnx.TraerDataTable("sp_ver_distrito", Me.cboProvincia.SelectedValue), "codigo_dis", "nombre_dis", "--Seleccione Distrito--")
        ObjCnx.CerrarConexion()
    End Sub

   
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim ObjCnx As New ClsConectarDatos
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        ObjCnx.AbrirConexion()
        ObjCnx.Ejecutar("ENC_AgregarEncuestaCultura", Mid(Request.QueryString("codigo_alu"), 1, Request.QueryString("codigo_alu").Length - 2), txtEscuela.Text, _
                       cboCiclo.SelectedValue, txtEdad.Text, IIf(cboSexo.SelectedValue = 1, "F", "M"), _
                       cboColegio.SelectedValue & cboColegio.SelectedItem.Text, _
                       txtUrbanizacion.Text, cboDistrito.SelectedItem.Text, cboProvincia.SelectedItem.Text, _
                       IIf(cblTiemposLibres.Items(0).Selected = True, cblTiemposLibres.Items(0).Value, 0), _
                       IIf(cblTiemposLibres.Items(1).Selected = True, cblTiemposLibres.Items(1).Value, 0), _
                       IIf(cblTiemposLibres.Items(2).Selected = True, cblTiemposLibres.Items(2).Value, 0), _
                       IIf(cblTiemposLibres.Items(3).Selected = True, cblTiemposLibres.Items(3).Value, 0), _
                       IIf(cblTiemposLibres.Items(4).Selected = True, cblTiemposLibres.Items(4).Value, 0), _
                       IIf(cblTiemposLibres.Items(5).Selected = True, cblTiemposLibres.Items(5).Value, 0), _
                       IIf(cblTiemposLibres.Items(6).Selected = True, cblTiemposLibres.Items(6).Value, 0), _
                       IIf(cblTiemposLibres.Items(7).Selected = True, cblTiemposLibres.Items(7).Value, 0), _
                       cboLiteratura.SelectedValue & cboLiteratura.SelectedItem.Text, _
                       cboLectura.SelectedValue & cboLectura.SelectedItem.Text, txtNroLibrosLeidos.Text, _
                       txtLibroLeido1.Text, txtLibroLeido2.Text, cboCulturaDep.SelectedValue & cboCulturaDep.SelectedItem.Text, _
                       cboIdentificacion.SelectedValue & cboIdentificacion.SelectedItem.Text, _
                       cboGrupoCultural.SelectedValue, txtGrupoCultural.Text, cboConciencia.SelectedValue, _
                       cboArtesPlasticas.SelectedValue, txtArtesPlasticas.Text, _
                       cboIncursionarArte.SelectedValue, txtIncursionarArte.Text, cboPreferenciaMusica.SelectedValue, _
                       IIf(cblTipoMusica.Items(0).Selected = True, cblTipoMusica.Items(0).Value, 0), _
                       IIf(cblTipoMusica.Items(1).Selected = True, cblTipoMusica.Items(1).Value, 0), _
                       IIf(cblTipoMusica.Items(2).Selected = True, cblTipoMusica.Items(2).Value, 0), _
                       IIf(cblTipoMusica.Items(3).Selected = True, cblTipoMusica.Items(3).Value, 0), _
                       IIf(cblTipoMusica.Items(4).Selected = True, cblTipoMusica.Items(4).Value, 0), _
                       IIf(cblTipoMusica.Items(5).Selected = True, cblTipoMusica.Items(5).Value, 0), _
                       IIf(cblTipoMusica.Items(6).Selected = True, cblTipoMusica.Items(6).Value, 0), _
                       IIf(cblTipoMusica.Items(7).Selected = True, cblTipoMusica.Items(7).Value, 0), _
                       IIf(cblTipoMusica.Items(8).Selected = True, cblTipoMusica.Items(8).Value, 0), _
                       txtEspecifiqueMusica.Text, cboIncursionarMusica.SelectedValue, txtIncursionarMusica.Text, _
                       cboPreferenciaTeatro.SelectedValue, txtPreferenciaTeatro.Text, _
                       IIf(cblTipoTeatro.Items(0).Selected = True, cblTipoTeatro.Items(0).Value, 0), _
                       IIf(cblTipoTeatro.Items(1).Selected = True, cblTipoTeatro.Items(1).Value, 0), _
                       IIf(cblTipoTeatro.Items(2).Selected = True, cblTipoTeatro.Items(2).Value, 0), _
                       IIf(cblTipoTeatro.Items(3).Selected = True, cblTipoTeatro.Items(3).Value, 0), _
                       IIf(cblTipoTeatro.Items(4).Selected = True, cblTipoTeatro.Items(4).Value, 0), _
                       IIf(cblTipoTeatro.Items(5).Selected = True, cblTipoTeatro.Items(5).Value, 0), _
                       IIf(cblTipoTeatro.Items(6).Selected = True, cblTipoTeatro.Items(6).Value, 0), _
                       cboIncursionarTeatro.SelectedValue, txtIncursionarTeatro.Text, cboPreferenciaDanzas.SelectedValue, _
                       txtPreferenciaDanzas.Text, cboConocesDanzas.SelectedValue, txtConocesDanzas.Text, _
                       cboIncursionarDanza.SelectedValue, txtIncursionarDanza.Text, _
                       IIf(cblExpCulturales.Items(0).Selected = True, cblExpCulturales.Items(0).Value, 0), _
                       txtExpMusica.Text, _
                       IIf(cblExpCulturales.Items(1).Selected = True, cblExpCulturales.Items(1).Value, 0), _
                       txtExpDanza.Text, _
                       IIf(cblExpCulturales.Items(2).Selected = True, cblExpCulturales.Items(2).Value, 0), _
                       txtExpLiteratura.Text, _
                       IIf(cblExpCulturales.Items(3).Selected = True, cblExpCulturales.Items(3).Value, 0), _
                       txtExpArtesania.Text, _
                       IIf(cblExpCulturales.Items(4).Selected = True, cblExpCulturales.Items(4).Value, 0), _
                       txtExpGastronomia.Text, _
                       IIf(cblExpCulturales.Items(5).Selected = True, cblExpCulturales.Items(5).Value, 0), _
                       txtExpArqueologia.Text, _
                       IIf(cblExpCulturales.Items(6).Selected = True, cblExpCulturales.Items(6).Value, 0), _
                       txtExpOtros.Text)
        ObjCnx.CerrarConexion()
        ClientScript.RegisterStartupScript(Me.GetType, "Exito", "alert('Gracias por llenar la encuesta');", True)
        ClientScript.RegisterStartupScript(Me.GetType, "Cerrar", "window.close();", True)
    End Sub

End Class

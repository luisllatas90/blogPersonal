'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class librerianet_academico_frmactualizardu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)

            Me.telefonoCasa_Dal.Attributes.Add("onkeypress", "validarnumero()")
            Me.telefonoMovil_Dal.Attributes.Add("onkeypress", "validarnumero()")
            Me.telefonofam_dal.Attributes.Add("onkeypress", "validarnumero()")
            Me.hdID.Value = Request.Form("hdcodigo_usu")

            ClsFunciones.LlenarListas(Me.dpProvincia, obj.TraerDataTable("ConsultarLugares", 3, 13, 0), "codigo_pro", "nombre_pro", "Seleccione la provincia")

            'Cargar datos del alumno
            Me.dlEstudiante.DataSource = obj.TraerDataTable("ConsultarAlumno", "CU", Request.Form("hdIdent_usu"))
            Me.dlEstudiante.DataBind()
            obj = Nothing
        End If
    End Sub
    Protected Sub dpProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpProvincia.SelectedIndexChanged
        'If dpProvincia.SelectedValue <> -2 Then
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)

        ClsFunciones.LlenarListas(Me.dpDistrito, obj.TraerDataTable("ConsultarLugares", 4, Me.dpProvincia.SelectedValue, 0), "codigo_dis", "nombre_dis", "Seleccione el distrito")
        'End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            Dim email1, email2 As String
            email1 = LCase(Me.email1.Text.Trim) & Me.dpProveedor1.Text
            email2 = LCase(Me.email2.Text.Trim)
            
            If email2 <> "" Then
                If email2.Length > 3 And Me.dpProveedor2.SelectedValue <> "-2" Then
                    email2 = email2 & Me.dpProveedor2.Text
                Else
                    RegisterStartupScript("MssError", "<script>alert('Deber ingresar el proveedor de email de su correo electrónico secundario" & email2 & "')</script>")
                    Exit Sub
                End If
            End If
            cmdGuardar.Enabled = False
            cmdCancelar.Enabled = False

            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
	    obj.IniciarTransaccion()
            obj.Ejecutar("ActualizarDatosAlumnoOfPensiones", Me.hdID.Value, UCase(Me.direccion_dal.Text.Trim), UCase(Me.urbanizacion_dal.Text.Trim), Me.dpDistrito.SelectedValue, Me.telefonoCasa_Dal.Text.Trim, Me.telefonoMovil_Dal.Text.Trim, Me.telefonofam_dal.Text.Trim, email1, email2, Me.hdID.Value)
	    obj.TerminarTransaccion()

            'Mostrar mensaje de alerta
            'RegisterStartupScript("OkActualizacion", "<script>alert('Los datos ingresados, se han GUARDADO CORRECTAMENTE.\nHaga clic en el botón Aceptar para volver a ingresar al campus virtual.');location.href='http://www.usat.edu.pe/campusvirtual'</script>")
            RegisterStartupScript("OkActualizacion", "<script>alert('Los datos ingresados, se han GUARDADO CORRECTAMENTE.\nHaga clic en el botón Aceptar para volver a ingresar al campus virtual.');location.href='../..'</script>")
        Catch ex As Exception
            cmdGuardar.Enabled = True
            cmdCancelar.Enabled = True
            RegisterStartupScript("ErrActualizacion", "<script>alert('Ha ocurrido un ERROR al guardar los datos.\nHaga clic en el botón Aceptar e ingrese denuevo" & ex.Message & "')</script>")
        End Try

    End Sub

    Protected Sub dlEstudiante_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlEstudiante.ItemDataBound
        Dim ruta As String
        Dim img As Image
        Dim obEnc As Object
        obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

        ruta = obEnc.CodificaWeb("069" & CType(e.Item.FindControl("lblcodigo"), Label).Text)
        'ruta = "http://www.usat.edu.pe/imgestudiantes/" & ruta
        ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
        img = e.Item.FindControl("FotoAlumno")
        img.ImageUrl = ruta

        obEnc = Nothing
    End Sub
End Class

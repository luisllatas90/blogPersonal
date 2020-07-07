'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class frmcambiardatosalumno
    Inherits System.Web.UI.Page
    Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
    Protected Sub frmcambiardatosalumno_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        obj = Nothing
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Me.telefonoCasa_Dal.Attributes.Add("onkeypress", "validarnumero()")
            Me.telefonoMovil_Dal.Attributes.Add("onkeypress", "validarnumero()")
            Me.telefonofam_dal.Attributes.Add("onkeypress", "validarnumero()")
            Me.hdID.Value = Request.QueryString("x")
            Me.hdOp.Value = Request.QueryString("id")
            'Me.hdID.Value = "1354" 'Request.QueryString("x")
            'Me.hdOp.Value = "471" 'Request.QueryString("id")

            ClsFunciones.LlenarListas(Me.dpBitacora, obj.TraerDataTable("ALU_ConsultarBitacoraDatosAlumno", 1, Me.hdID.Value, "P", 0, 0), "codigo_bda", "fechareg_bda", "--Datos Actuales--")

            'Cargar datos del alumno
            Me.dlEstudiante.DataSource = obj.TraerDataTable("ConsultarAlumno", "CU", Request.QueryString("c")) '"081IA10264"
            Me.dlEstudiante.DataBind()

            'obj = Nothing
            MostrarBitacoraCambios(0)

        End If
    End Sub

    Protected Sub dpProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpProvincia.SelectedIndexChanged
        'If dpProvincia.SelectedValue <> -2 Then
        'Dim obj3 As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(Me.Distrito_dal, obj.TraerDataTable("ConsultarLugares", 4, Me.dpProvincia.SelectedValue, 0), "codigo_dis", "nombre_dis", "- Seleccione -")
        'obj3 = Nothing
        'End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        'Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Try
            Dim email1, email2 As String
            email1 = LCase(Me.email_alu.Text.Trim) & Me.dpProveedor1.Text
            email2 = LCase(Me.email2_alu.Text.Trim)

            If email2 <> "" Then
                If email2.Length > 3 And Me.dpProveedor2.SelectedValue <> "-2" Then
                    email2 = email2 & Me.dpProveedor2.Text
                Else
                    obj = Nothing
                    RegisterStartupScript("MssError", "<script>alert('Deber ingresar el proveedor de email de su correo electrónico secundario" & email2 & "')</script>")
                    Exit Sub
                End If
            End If
            cmdGuardar.Enabled = False
            cmdCancelar.Enabled = False

            obj.IniciarTransaccion()
            obj.Ejecutar("ActualizarDatosAlumnoOfPensiones", Me.hdID.Value, UCase(Me.direccion_dal.Text.Trim), UCase(Me.urbanizacion_dal.Text.Trim), Me.Distrito_dal.SelectedValue, Me.telefonoCasa_Dal.Text.Trim, Me.telefonoMovil_Dal.Text.Trim, Me.telefonofam_dal.Text.Trim, email1, email2, Me.hdOp.Value)
            obj.TerminarTransaccion()
            obj = Nothing
            'Mostrar mensaje de alerta
            RegisterStartupScript("OkActualizacion", "<script>window.opener.location.reload();window.close()</script>")

        Catch ex As Exception
            obj.AbortarTransaccion()
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
    Protected Sub dpBitacora_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpBitacora.SelectedIndexChanged

        If dpBitacora.SelectedValue <> "-1" Then
            LimpiarValores(False)
            MostrarBitacoraCambios(Me.dpBitacora.SelectedValue)
        Else
            LimpiarValores(True)
            MostrarBitacoraCambios(0)
        End If
    End Sub
    Private Sub MostrarEmailProveedor(ByVal tipo As Int16, ByVal email As String)
        Dim proveedor As String
        Dim posicion As Int16

        If email.ToString <> "" Then
            posicion = InStr(email, "@")
            If posicion > 0 Then
                proveedor = Mid(email, posicion, Len(email))
                email = Left(email, Len(email) - Len(proveedor))

                If tipo = 1 Then
                    Me.email_alu.Text = email
                    Me.dpProveedor1.Text = proveedor
                Else
                    Me.email2_alu.Text = email
                    Me.dpProveedor2.Text = proveedor
                End If
            End If
        End If
    End Sub
    Private Sub MostrarBitacoraCambios(ByVal codigo_bda As Integer)
        'Dim obj2 As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim tbl As Data.DataTable

        If codigo_bda = 0 Then
            'Cargar datos personales del alumno
            tbl = obj.TraerDataTable("ConsultarAlumno", "RG", Me.hdID.Value)
            'Validar si hay datos
            If tbl.Rows.Count > 0 Then
                'Asignar los valores
                MostrarEmailProveedor(1, tbl.Rows(0).Item("email_alu"))
                MostrarEmailProveedor(2, tbl.Rows(0).Item("email2_alu"))

                'anio = Year(fechaNacimiento_alu)
                'sexo_alu = tbl.Rows(0).Item("sexo_alu")
                'tipodocident_alu = tbl.Rows(0).Item("tipodocident_alu")
                'nrodocident_alu = tbl.Rows(0).Item("nrodocident_alu")

                ''Campos de la tbl DATOSALUMNO
                'fecharegistro_Dal = tbl.Rows(0).Item("fecharegistro_Dal")
                '-----Datos de la familia/contacto-----------
                'PersonaFam_Dal = tbl.Rows(0).Item("PersonaFam_Dal")
                'direccionfam_dal = tbl.Rows(0).Item("direccionfam_dal")
                'urbanizacionfam_dal = tbl.Rows(0).Item("urbanizacionfam_dal")
                'distritoFam_Dal = tbl.Rows(0).Item("distritoFam_Dal")
                'nombreDisFam_Dal = tbl.Rows(0).Item("nombreDisFam_Dal")
                'codigoProFam_Dal = tbl.Rows(0).Item("codigoProFam_Dal")
                'nombreProFam_Dal = tbl.Rows(0).Item("nombreProFam_Dal")
                'codigoDepFam_Dal = tbl.Rows(0).Item("codigoDepFam_Dal")
                'nombreDepFam_Dal = tbl.Rows(0).Item("nombreDepFam_Dal")
                Me.telefonofam_dal.Text = tbl.Rows(0).Item("telefonofam_dal")

                '------Datos dónde reside el alumno----------
                Me.direccion_dal.Text = tbl.Rows(0).Item("direccion_dal")
                Me.urbanizacion_dal.Text = tbl.Rows(0).Item("urbanizacion_dal")

                'Cargar provincia
                ClsFunciones.LlenarListas(Me.dpProvincia, obj.TraerDataTable("ConsultarLugares", 3, 13, 0), "codigo_pro", "nombre_pro", "- Seleccione -")
                Me.dpProvincia.SelectedValue = tbl.Rows(0).Item("codigoPro_Dal")

                'Cargar distrito
                ClsFunciones.LlenarListas(Me.Distrito_dal, obj.TraerDataTable("ConsultarLugares", 4, tbl.Rows(0).Item("codigoPro_Dal")), "codigo_dis", "nombre_dis", "- Seleccione -")
                Me.Distrito_dal.SelectedValue = tbl.Rows(0).Item("distrito_dal")

                'religion_dal = tbl.Rows(0).Item("religion_dal")
                'ultimosacramento_dal = tbl.Rows(0).Item("ultimosacramento_dal")
                'estadocivil_Dal = tbl.Rows(0).Item("estadocivil_Dal")
                Me.telefonoCasa_Dal.Text = tbl.Rows(0).Item("telefonoCasa_Dal")
                Me.telefonoMovil_Dal.Text = tbl.Rows(0).Item("telefonoMovil_Dal")
                'me.telefonoTrabajo_Dal = tbl.Rows(0).Item("telefonoTrabajo_Dal")
            End If
        Else
            'Cargar datos del alumno
            tbl = obj.TraerDataTable("ALU_ConsultarBitacoraDatosAlumno", 2, Me.hdID.Value, "P", codigo_bda, 0)
            'Validar si hay datos
            If tbl.Rows.Count > 0 Then
                For i As Int16 = 0 To tbl.Rows.Count - 1
                    'Response.Write("campo=" & tbl.Rows(i).Item("nombrecampo_dbda").ToString & " valor=" & tbl.Rows(i).Item("valorcampo_dbda").ToString & "<br />")
                    Select Case LCase(tbl.Rows(i).Item("nombrecampo_dbda"))
                        Case "email_alu" : MostrarEmailProveedor(1, tbl.Rows(i).Item("valorcampo_dbda").ToString)
                        Case "email2_alu" : MostrarEmailProveedor(2, tbl.Rows(i).Item("valorcampo_dbda").ToString)
                        Case "direccion_dal" : Me.direccion_dal.Text = tbl.Rows(i).Item("valorcampo_dbda").ToString
                        Case "telefonofam_dal" : Me.telefonofam_dal.Text = tbl.Rows(i).Item("valorcampo_dbda").ToString
                        Case "telefonocasa_dal" : Me.telefonoCasa_Dal.Text = tbl.Rows(i).Item("valorcampo_dbda").ToString
                        Case "telefonomovil_dal" : Me.telefonoMovil_Dal.Text = tbl.Rows(i).Item("valorcampo_dbda").ToString
                        Case "urbanizacion_dal" : Me.urbanizacion_dal.Text = tbl.Rows(i).Item("valorcampo_dbda").ToString
                        Case "distrito_dal"
                            Dim tbl2 As Data.DataTable
                            tbl2 = obj.TraerDataTable("ConsultarLugares", 8, tbl.Rows(i).Item("valorcampo_dbda"), 0)
                            Me.Distrito_dal.Items.Add(tbl2.Rows(0).Item("nombre_dis").ToString)
                            Me.dpProvincia.Items.Add(tbl2.Rows(0).Item("nombre_pro").ToString)
                            tbl2.Dispose()
                    End Select
                Next
            End If
        End If
        tbl.Dispose()
        'obj2 = Nothing
    End Sub
    Private Sub LimpiarValores(ByVal estado As Boolean)
        Me.direccion_dal.Text = ""
        Me.urbanizacion_dal.Text = ""
        Me.email_alu.Text = ""
        Me.email2_alu.Text = ""
        Me.dpProveedor1.SelectedValue = -2
        Me.dpProveedor2.SelectedValue = -2
        Me.dpProvincia.Items.Clear()
        Me.Distrito_dal.Items.Clear()
        Me.telefonoCasa_Dal.Text = ""
        Me.telefonofam_dal.Text = ""
        Me.telefonoMovil_Dal.Text = ""

        For Each Con As Control In Me.Form.Controls
            If TypeOf Con Is DropDownList Then
                If CType(Con, DropDownList).ID.ToString <> Me.dpBitacora.ID.ToString Then
                    CType(Con, DropDownList).Enabled = estado
                End If
            End If
            If TypeOf Con Is TextBox Then CType(Con, TextBox).Enabled = estado
            If TypeOf Con Is Button Then CType(Con, Button).Enabled = estado
        Next
    End Sub

    Protected Sub frmcambiardatosalumno_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        obj = Nothing
    End Sub
End Class
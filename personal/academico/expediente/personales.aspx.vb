Imports System.Text
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Partial Class personales
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id") <> "" And Session("id") = "" Then
            Session("Id") = Request.QueryString("id")
        End If
        If IsPostBack = False Then
            Me.CmdGuardar.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
            Me.CmdGuardar.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

            Me.DDLReligion.Attributes.Add("onchange", "javascript:religion();return false;")
            Me.Img.Attributes.Add("onmouseover", "ddrivetip('Ingrese una imagen haciendo click en examinar, con una extension (*.jpg) y tamaño no mayor a 60 Kb.')")
            Me.Img.Attributes.Add("onMouseout", "hideddrivetip()")
            Dim objCombos As New Combos
            objCombos.LlenaPais(Me.DDlNacionalidad)
            objCombos.LlenaDepartamento(Me.DDLDepartamento)
            objCombos.LlenaProvincia(Me.DDLProvincia, 0)
            objCombos.LlenaDistrito(Me.DDLDistrito, 0)
            objCombos = Nothing
            Me.DDlAño.Items.Add("Año")
            For i As Integer = (Now.Year - 10) To 1915 Step -1
                Me.DDlAño.Items.Add(i.ToString)
            Next

            'xDguevara 02.08.2012 -----------
            CargarListaDesplegables()
            '--------------------------------

            Call mt_CargarComboOperadorInternet()
            Call mt_CargarComboOperadorMovil()

            'Primero llena los combos y con este procedimiento apunta que datos tiene el usuario.
            Call LlenaDatosPersonal()
        End If
    End Sub

    'Agregado por Dguevara 02.08.2012
    Private Sub CargarListaDesplegables()
        Try
            Dim ddl As New Combos
            'Cargamos los combos utilies para el UBIGEO
            ddl.LlenaDepartamento(ddlDepartamento1)
            ddl.LlenaDepartamento(ddlDepartamento2)
            ddl.LlenaProvincia(ddlProvincia1, 0)
            ddl.LlenaProvincia(ddlProvincia2, 0)
            ddl.LlenaDistrito(ddlDistrito1, 0)
            ddl.LlenaDistrito(ddlDistrito2, 0)

            'Cargamos los combos del tipo de via
            ddl.LlenaTipoVia(ddlTipoVia1)
            ddl.LlenaTipoVia(ddlTipoVia2)

            'Cargamos los combos del tipo de zona.
            ddl.LLenaTipoZona(ddlTipoZona1)
            ddl.LLenaTipoZona(ddlTipoZona2)

            'Cargamos el combo del nivel Educativo
            ddl.LLenaNivelEducativo(ddlNivelEducativo)

            'Cargamos la entidad financiera ->[Cuenta / CTS ]
            ddl.LlenaEntidadFinanciera(ddlEntidadFinancieraCuenta)
            ddl.LlenaEntidadFinanciera(ddlEntidadFinancieraCts)

            'Cargamos la informacion del seguro Social
            ddl.LlenaRegimenSalud(ddlRegimenSalud)

            'Cargamos el regimen pensionario.
            ddl.LlenaRegimenPensionario(ddlRegPension)

            'Carga Afp
            ddl.LlenaAfp(ddlAfps)

            'Carga EPS
            ddl.LlenaEPS(ddlCodigoEPS)

            'Carga SituacionEPS
            ddl.LlenaSituacionEPS(ddlSituacionEPS)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub DDLDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLDepartamento.SelectedIndexChanged
        Dim clscombos As New Combos
        clscombos.LlenaProvincia(Me.DDLProvincia, Me.DDLDepartamento.SelectedValue)
        clscombos.LlenaDistrito(Me.DDLDistrito, 0)
    End Sub

    Protected Sub LlenaDatosPersonal()
        Dim objPersonal As New Personal
        Dim i As Int16
        Dim ddl As New Combos

        objPersonal.codigo = CInt(Session("Id"))
        Dim datos As New DataTable
        datos = objPersonal.ObtieneDatosPersonales
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        With datos.Rows(0)

            Me.LblPaterno.Text = .Item(0).ToString                  'Apellido Paterno
            Me.LblMaterno.Text = .Item(1).ToString                  'Apellido Materno
            Me.LblNombres.Text = .Item(2).ToString                  'Nombres
            Me.DDLSexo.SelectedValue = .Item(3).ToString            'Sexo
            Me.DDLDocumento.SelectedValue = .Item(4).ToString       'Tipo Documento
            Me.DDLDocumento.Enabled = False
            Me.LblDocumento.Text = .Item(5).ToString                'Numero de Documento
            Me.DDLCivil.SelectedValue = .Item(6).ToString           'estado civil
            If IsDate(.Item(7).ToString) Then
                DDlDia.SelectedValue = Day(.Item(7).ToString)       'Dia de Nacimiento
                DDLMes.SelectedValue = Month(.Item(7).ToString)     'Mes de Nacimiento
                DDlAño.SelectedValue = Year(.Item(7).ToString)      'Año de Nacimiento
            End If
            Me.TxtDireccionPer.Text = .Item(8).ToString             'Direccion
            Me.TxtTelDomicilio.Text = .Item(9).ToString             'Telefono Domicilio
            Me.TxtMail1.Text = .Item(10).ToString                   'Mail 1
            Me.DDLReligion.SelectedValue = .Item(11).ToString       'Religion
            Me.DDLSacramento.SelectedValue = .Item(12).ToString     'Ultimo Sacramento

            'Telefono Trabajo y Anexo
            'Response.Write(.Item(13))
            'If Trim(.Item(13).ToString) <> "" Then
            '    i = InStr(.Item(13).ToString, "-")
            '    Me.TxtTelTrabajo.Text = Mid(.Item(13).ToString, 1, i - 1)       
            '    Me.TxtAnexo.Text = Mid(.Item(13).ToString, i + 1)
            'End If

            Me.TxtTelCelular.Text = .Item(14).ToString              'Telefono Celular
            Me.TxtMail2.Text = .Item(15).ToString                   'Mail 2
            Me.TxtEmerNombre.Text = .Item(16).ToString              'Emergencia NOmbre
            Me.TxtEmerDireccion.Text = .Item(17).ToString           'Emergencia Direccion
            Me.TxtEmerTelefono.Text = .Item(18).ToString            'Emergencia Telefono
            Me.DDlNacionalidad.SelectedValue = .Item(19).ToString   'Nacionalidad
            Me.DDLSangre.SelectedValue = .Item(21).ToString         'Tipo de Sangre

            If .Item("foto").ToString = "" Then
                Me.imgfoto.Visible = True
                Me.imgfoto.ImageUrl = "images/fotovacia.gif"
            Else
                Me.imgfoto.Visible = True
                Me.imgfoto.ImageUrl = "../../imgpersonal/" & .Item("foto")
            End If

            Dim objprovincia As Combos
            Dim strDep, strProv As String
            objprovincia = New Combos

            If .Item(20).ToString Is Nothing Or Trim(.Item(20).ToString) = "" Then
                Me.DDLDepartamento.SelectedValue = "0"
                Me.DDLProvincia.SelectedValue = "0"
                Me.DDLDistrito.SelectedValue = "0"
            Else
                strDep = objprovincia.ObtieneDepartamento(.Item(20).ToString)
                Me.DDLDepartamento.SelectedValue = strDep
                objprovincia.LlenaProvincia(Me.DDLProvincia, CInt(strDep))
                Me.DDLProvincia.SelectedValue = .Item(20).ToString

                strProv = objprovincia.ObtieneProvincia(.Item(22).ToString)
                Me.DDLProvincia.SelectedValue = strProv
                objprovincia.LlenaDistrito(Me.DDLDistrito, strProv)
                Me.DDLDistrito.SelectedValue = .Item(22).ToString

            End If

            '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            'agregado por dguevara 02.08.2012 ------------------------------------------------------
            txtNumerocuentaSueldo.Text = .Item("nroCuenta_Per").ToString
            txtNumerocuentaCts.Text = .Item("nroCuentaCTS_Per").ToString
            If .Item("monedaCta_Per").ToString <> "" Then
                ddlTipoMonedaSueldo.SelectedValue = .Item("monedaCta_Per").ToString.Trim
            End If
            If .Item("monedaCtaCTS_Per").ToString <> "" Then
                ddlTipoMonedaCts.SelectedValue = .Item("monedaCtaCTS_Per").ToString.Trim
            End If

            'Nivel Educativo
            If .Item("codigo_nive").ToString <> "" Then
                ddlNivelEducativo.SelectedValue = .Item("codigo_nive").ToString
            End If
            'Fecha de inscripcion en el registro pensionario
            If .Item("FechaInscripcionRegPen_Per").ToString <> "" Then
                txtfecinpensionario.Text = .Item("FechaInscripcionRegPen_Per").ToString
            End If
            'Fecha de ingreso a la USAT 
            If .Item("fechaIni_Per").ToString <> "" Then
                txtfecinusat.Text = .Item("fechaIni_Per").ToString
            End If
            ' ------  Direccion 01  ----
            If .Item("codigo_tvia").ToString <> "" Then
                ddlTipoVia1.SelectedValue = .Item("codigo_tvia").ToString.Trim
            Else
                ddlTipoVia1.SelectedValue = "0"
            End If
            If .Item("nombreVia_Per").ToString.Trim <> "" Then
                txtnombreVia_Per1.Text = .Item("nombreVia_Per").ToString
            End If

            If .Item("numeroVia_Per").ToString.Trim <> "" Then
                txtnumeroVia_Per1.Text = .Item("numeroVia_Per").ToString.Trim
            End If

            If .Item("interiorVia_Per").ToString.Trim <> "" Then
                txtinteriorVia_Per1.Text = .Item("interiorVia_Per").ToString.Trim
            End If
            '--Tipo Zona  ---------------------------------------------------------
            If .Item("codigo_tzon").ToString.Trim <> "" Then
                ddlTipoZona1.SelectedValue = .Item("codigo_tzon").ToString.Trim
            Else
                ddlTipoZona1.SelectedValue = "0"
            End If
            '-----------------------------------------------------------------------

            If .Item("nombreZona_Per").ToString.Trim <> "" Then
                txtnombreZona_Per1.Text = .Item("nombreZona_Per").ToString.Trim
            End If
            If .Item("referenciaZona_Per").ToString.Trim <> "" Then
                txtreferenciaZona_Per1.Text = .Item("referenciaZona_Per").ToString.Trim
            End If
            If .Item("referenciaESSALUD_Per") = True Then
                chkRefEssalud1.Checked = True
                chkRefEssalud1.ForeColor = Drawing.Color.Blue
            Else
                chkRefEssalud1.Checked = False
                chkRefEssalud1.ForeColor = Drawing.Color.Red
            End If
            ' ------  Direccion 02  -------------------------------------------------------------------

            If .Item("codigo2_tvia").ToString.Trim <> "" Then
                ddlTipoVia2.SelectedValue = .Item("codigo2_tvia").ToString
            End If
            If .Item("nombreVia2_Per").ToString.Trim <> "" Then
                txtnombreVia_Per2.Text = .Item("nombreVia2_Per").ToString.Trim
            End If
            If .Item("numeroVia2_Per").ToString.Trim <> "" Then
                txtnumeroVia_Per2.Text = .Item("numeroVia2_Per").ToString.Trim
            End If
            If .Item("interiorVia2_Per").ToString.Trim <> "" Then
                txtinteriorVia_Per2.Text = .Item("interiorVia2_Per").ToString.Trim
            End If
            If .Item("codigo2_tzon").ToString.Trim <> "" Then
                ddlTipoZona2.SelectedValue = .Item("codigo2_tzon").ToString.Trim
            Else
                ddlTipoZona2.SelectedValue = "0"
            End If
            If .Item("nombreZona2_Per").ToString.Trim <> "" Then
                txtnombreZona_Per2.Text = .Item("nombreZona2_Per").ToString.Trim
            End If
            If .Item("referenciaZona2_Per").ToString.Trim <> "" Then
                txtreferenciaZona_Per2.Text = .Item("referenciaZona2_Per").ToString.Trim
            End If
            If .Item("referenciaESSALUD2_Per") = True Then
                chkRefEssalud2.Checked = True
                chkRefEssalud2.ForeColor = Drawing.Color.Blue
            Else
                chkRefEssalud2.Checked = False
                chkRefEssalud2.ForeColor = Drawing.Color.Red
            End If

            'Entidadad Financiera Cuenta /CTS ------------------------------------------------
            If .Item("codigo_Efi").ToString.Trim <> "" Then
                ddlEntidadFinancieraCuenta.SelectedValue = .Item("codigo_Efi").ToString.Trim
            Else
                ddlEntidadFinancieraCuenta.SelectedValue = "0"
            End If
            If .Item("codigoEfi_CTS").ToString.Trim <> "" Then
                ddlEntidadFinancieraCts.SelectedValue = .Item("codigoEfi_CTS").ToString.Trim
            Else
                ddlEntidadFinancieraCts.SelectedValue = "0"
            End If
            '-------------------------------------------------------------------------------------
            If .Item("tipoCuenta_Per").ToString.Trim <> "" Then
                ddlTipoCuentaSueldo.SelectedValue = .Item("tipoCuenta_Per").ToString.Trim
            End If
            '---Seguridad Social ----
            If .Item("codigo_Rsa").ToString.Trim <> "" Then
                ddlRegimenSalud.SelectedValue = .Item("codigo_Rsa").ToString.Trim
            Else
                ddlRegimenSalud.SelectedValue = 0
            End If
            '--------------------------------------------------------------------------------------
            'Regimen Pensionario-----
            If .Item("regPensionario_Per") <> 0 Then
                ddlRegPension.SelectedValue = .Item("regPensionario_Per")
            Else
                ddlRegPension.SelectedValue = 0
            End If
            '---------------------------------------------------------------------------------------
            If .Item("situacionEPS_Per").ToString.Trim <> "" Then
                ddlSituacionEPS.SelectedValue = .Item("situacionEPS_Per").ToString.Trim
            Else
                ddlSituacionEPS.SelectedValue = "0"
            End If
            '----------------------------------------------------------------------------------------
            If .Item("cuspp_Per").ToString.Trim <> "" Then
                txtCussp.Text = .Item("cuspp_Per").ToString.Trim
            End If
            '-----------------------------------------------------------------------------------------
            'EPS
            If .Item("codigo_EPS").ToString.Trim <> "" Then
                ddlCodigoEPS.SelectedValue = .Item("codigo_EPS").ToString.Trim
            Else
                ddlCodigoEPS.SelectedValue = "0"
            End If
            '-----------------------------------------------------------------------------------------
            If .Item("SCTRSalud_Per").ToString.Trim <> "" Then
                ddlSCTRSalud.SelectedValue = .Item("SCTRSalud_Per").ToString.Trim
            End If
            'Codigo AFP
            If .Item("codigo_Afp") <> -1 Then
                'Response.Write(.Item("codigo_Afp"))
                ddlAfps.SelectedValue = .Item("codigo_Afp")
            Else
                ddlAfps.SelectedValue = -1
            End If
            'Numero de hijos --------------------------------------------------------------------------
            If .Item("numeroHijos_Per") <> 0 Then
                txtNumeroHijos.Text = .Item("numeroHijos_Per")
            Else
                txtNumeroHijos.Text = 0
            End If
            '------------------------------------------------------------------------------------------
            'Ubigeo direccion 01 ----------------------------------------------------------------------
            'domicilioUbiGeo_Per -> representa el codigo del distrito, util para consultar e insertar.
            If .Item("domicilioUbiGeo_Per").ToString <> "" Then
                If .Item("domicilioUbiGeo_Per").ToString <> "1" Then
                    Dim obj As New Personal
                    Dim dts As New DataTable
                    dts = obj.DatosUbigeo(.Item("domicilioUbiGeo_Per"))
                    If dts.Rows.Count > 0 Then
                        With dts.Rows(0)
                            'Cargamos el departamento
                            ddlDepartamento1.SelectedValue = .Item("codigo_Dep")
                            'Cargamos la lista de provincias que tiene el departamento
                            ddl.LlenaProvincia(ddlProvincia1, .Item("codigo_Dep"))
                            'Apuntams la provincia que le corresponde.
                            ddlProvincia1.SelectedValue = .Item("codigo_Pro")
                            'Cargamos la lista de distritos que tiene esa provincia.
                            ddl.LlenaDistrito(ddlDistrito1, .Item("codigo_Pro"))
                            'Apuntamos el distrito que tiene registrado
                            ddlDistrito1.SelectedValue = .Item("codigo_Dis")
                        End With
                    End If
                End If
            Else
                ddlDepartamento1.SelectedValue = 0
                ddlDistrito1.SelectedValue = 0
                ddlDistrito1.SelectedValue = 0
            End If


            If .Item("RUCTrabajador").ToString.Trim <> "" Then
                txtRuc.Text = .Item("RUCTrabajador").ToString.Trim
            End If

            '------------------------------------------------------------------------------------------
            'ubigeo direccion 02 -------------------------------------------------------------------------
            If .Item("domicilioUbiGeo2_Per").ToString.Trim <> "" Then
                If .Item("domicilioUbiGeo2_Per").ToString <> "1" Then
                    Dim obj As New Personal
                    Dim dts As New DataTable
                    dts = obj.DatosUbigeo(.Item("domicilioUbiGeo2_Per"))
                    If dts.Rows.Count > 0 Then
                        With dts.Rows(0)
                            'Cargamos el departamento
                            ddlDepartamento2.SelectedValue = .Item("codigo_Dep")
                            'Cargamos la lista de provincias que tiene el departamento
                            ddl.LlenaProvincia(ddlProvincia2, .Item("codigo_Dep"))
                            'Apuntams la provincia que le corresponde.
                            ddlProvincia2.SelectedValue = .Item("codigo_Pro")
                            'Cargamos la lista de distritos que tiene esa provincia.
                            ddl.LlenaDistrito(ddlDistrito2, .Item("codigo_Pro"))
                            'Apuntamos el distrito que tiene registrado
                            ddlDistrito2.SelectedValue = .Item("codigo_Dis")
                        End With
                    End If
                End If
            Else
                ddlDepartamento2.SelectedValue = 0
                ddlProvincia2.SelectedValue = 0
                ddlDistrito2.SelectedValue = 0
            End If

            '---------------------------------------------------------------------------------------------
            If Not String.IsNullOrEmpty(.Item("operadorInternet_Per").ToString.Trim) Then
                Me.ddlOperadorInternet.Text = .Item("operadorInternet_Per").ToString.Trim

                If String.IsNullOrEmpty(Me.ddlOperadorInternet.SelectedValue) Then
                    Me.txtOperadorInternet.Text = .Item("operadorInternet_Per").ToString.Trim
                    Me.txtOperadorInternet.ReadOnly = False
                    Me.ddlOperadorInternet.Text = "OTRO"
                End If
            End If

            If Not String.IsNullOrEmpty(.Item("operadorCelular_Per").ToString.Trim) Then
                Me.ddlOperadorMovil.Text = .Item("operadorCelular_Per").ToString.Trim

                If String.IsNullOrEmpty(Me.ddlOperadorMovil.SelectedValue) Then
                    Me.txtOperadorMovil.Text = .Item("operadorCelular_Per").ToString.Trim
                    Me.txtOperadorMovil.ReadOnly = False
                    Me.ddlOperadorMovil.Text = "OTRO"
                End If
            End If

        End With

        Me.chkFirmoCarta.Checked = ObjCnx.TraerValor("PER_ConsultarFirmaCarta", Request.QueryString("id"), 0)
        objPersonal = Nothing

    End Sub

    Private Sub GuardarDatos()
        Try
            'Response.Write("Entra a guardar.")

            Dim objPersonal As New Personal
            objPersonal.codigo = Request.QueryString("id")
            Dim dateFecNac As Date
            Dim strSacramento As String
            Dim valor As Int16 = 0

            Dim referenciaESSALUD_Per As Integer
            Dim referenciaESSALUD2_Per As Integer

            Dim script As String
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

            Try
                dateFecNac = CDate(Me.DDlDia.SelectedValue & "/" & Me.DDLMes.SelectedValue & "/" & Me.DDlAño.SelectedValue)
            Catch ex As Exception
                script = "<script>alert('Fecha de Nacimiento Incorrecta')</script>"
                Page.RegisterStartupScript("Fecha Incorrecta", script)
                Exit Sub
            End Try

            If Me.DDLReligion.SelectedValue = "No Catolico" Then
                strSacramento = ""
            Else
                strSacramento = Me.DDLSacramento.SelectedValue
            End If

            If chkRefEssalud1.Checked = True Then
                referenciaESSALUD_Per = 1
            Else
                referenciaESSALUD_Per = 0
            End If

            If chkRefEssalud2.Checked = True Then
                referenciaESSALUD2_Per = 1
            Else
                referenciaESSALUD2_Per = 0
            End If

            'Registra todos los datos del trabajador.
            valor = objPersonal.GrabaDatosPersonales(Me.LblPaterno.Text, _
                                                     Me.DDLSexo.SelectedValue, _
                                                     dateFecNac, _
                                                     Me.DDlNacionalidad.SelectedValue, _
                                                     Me.DDLCivil.SelectedValue, _
                                                     Me.DDLReligion.SelectedValue, _
                                                     strSacramento, _
                                                     Me.DDLSangre.SelectedValue, _
                                                     Me.DDLProvincia.SelectedValue, _
                                                     Me.DDLDistrito.SelectedValue, _
                                                     Me.TxtDireccionPer.Text, _
                                                     Me.TxtTelDomicilio.Text, _
                                                     Me.TxtTelCelular.Text, _
                                                     Me.TxtTelTrabajo.Text & "-" & Me.TxtAnexo.Text, _
                                                     Me.TxtMail1.Text, _
                                                     Me.TxtMail2.Text, _
                                                     Me.TxtEmerNombre.Text, _
                                                     Me.TxtEmerDireccion.Text, _
                                                     Me.TxtEmerTelefono.Text, _
                                                     Me.FuFoto, _
                                                     Me.txtRuc.Text.Trim, _
                                                     Me.txtNumeroHijos.Text.Trim, _
                                                     Me.ddlNivelEducativo.SelectedValue, _
                                                     Me.ddlDistrito1.SelectedValue, _
                                                     Me.ddlDistrito2.SelectedValue, _
                                                     Me.ddlTipoVia1.SelectedValue, _
                                                     Me.ddlTipoVia2.SelectedValue, _
                                                     Me.txtnombreVia_Per1.Text, _
                                                     Me.txtnombreVia_Per2.Text, _
                                                     Me.txtnumeroVia_Per1.Text, _
                                                     Me.txtnumeroVia_Per2.Text, _
                                                     Me.txtinteriorVia_Per1.Text, _
                                                     Me.txtinteriorVia_Per2.Text, _
                                                     referenciaESSALUD_Per, _
                                                     referenciaESSALUD2_Per, _
                                                     Me.txtnombreZona_Per1.Text, _
                                                     Me.txtnombreZona_Per2.Text, _
                                                     Me.txtreferenciaZona_Per1.Text, _
                                                     Me.txtreferenciaZona_Per2.Text, _
                                                     Me.ddlEntidadFinancieraCuenta.SelectedValue, _
                                                     Me.ddlEntidadFinancieraCts.SelectedValue, _
                                                     Me.txtNumerocuentaSueldo.Text.Trim, _
                                                     Me.txtNumerocuentaCts.Text.Trim, _
                                                     Me.ddlTipoMonedaSueldo.SelectedValue, _
                                                     Me.ddlTipoMonedaCts.SelectedValue, _
                                                     Me.ddlTipoCuentaSueldo.SelectedValue, _
                                                     Me.ddlRegimenSalud.SelectedValue, _
                                                     Me.txtCussp.Text, _
                                                     Me.ddlCodigoEPS.SelectedValue, _
                                                     Me.ddlRegimenSalud.SelectedValue, _
                                                     Me.ddlRegPension.SelectedValue, _
                                                     Me.ddlSituacionEPS.SelectedValue, _
                                                     CType(Me.txtfecinpensionario.Text.Trim, Date), _
                                                     CType(Me.txtfecinusat.Text.Trim, Date), _
                                                     Me.ddlTipoZona1.SelectedValue, _
                                                     Me.ddlTipoZona2.SelectedValue, _
                                                     Me.ddlOperadorInternet.SelectedValue, _
                                                     Me.ddlOperadorMovil.SelectedValue)


            ObjCnx.Ejecutar("PER_ActualizarFirmaCarta", Request.QueryString("id"), Me.chkFirmoCarta.Checked)

            If valor = -1 Then
                script = "<script>alert('Error en actualizar sus datos, intentelo nuevamente')</script>"
            ElseIf valor = -2 Then
                'Response.Write("<br />")
                'Response.Write(" solo jpg")

                script = "<script>alert('Error al ingresar su fotografia, la extension permitida es *.jpg')</script>"
            ElseIf valor = -3 Then
                script = "<script>alert('El tamaño de la imagen no debe superar los 60 Kb.')</script>"
            Else
                'Response.Write("<br />")
                'Response.Write(" datos grabados correctamente")

                script = "<script>alert('Los Datos se grabaron correctamente')</script>"
                Call LlenaDatosPersonal()
                Page.RegisterStartupScript("Mensajes", script)

                'Response.Redirect("perfil.aspx?menu=" & Request.QueryString("menu") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
                ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='perfil.aspx?menu=" & Request.QueryString("menu") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&tipo=M';", True)

                'ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmRegistrarInvestigaciones.aspx?idInv=" & row.Cells.Item(0).Text & "&id=" & Request.QueryString("id").ToString & "&ctf=" & Request.QueryString("ctf") & "&tipo=M';", True)
                'Response.Redirect("perfil.aspx?menu=" & Request.QueryString("menu") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
                'ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmRegistrarInvestigaciones.aspx?idInv=" & row.Cells.Item(0).Text & "&id=" & Request.QueryString("id").ToString & "&ctf=" & Request.QueryString("ctf") & "&tipo=M';", True)

            End If

            Call LlenaDatosPersonal()
            objPersonal = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Los datos consignados no fueron registrados.');", True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        GuardarDatos()
    End Sub

    Protected Sub DDLProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLProvincia.SelectedIndexChanged
        Dim clscombos As New Combos
        clscombos.LlenaDistrito(Me.DDLDistrito, Me.DDLProvincia.SelectedValue)
    End Sub

    Protected Sub ddlRegPension_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegPension.SelectedIndexChanged
        Try
            Dim objPersonal As New Personal
            Dim datos As New DataTable
            If ddlRegPension.SelectedValue <> 0 Then
                datos = objPersonal.ConsultaCodigo_afp(ddlRegPension.SelectedValue)
                If datos.Rows.Count > 0 Then
                    With datos.Rows(0)
                        ddlAfps.SelectedValue = .Item("codigo_Afp")
                    End With
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlDepartamento2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamento2.SelectedIndexChanged
        Try
            Dim ddl As New Combos
            If ddlDepartamento2.SelectedValue <> 0 Then
                ddlProvincia2.Enabled = True
                ddlDistrito2.Enabled = False
                ddlDistrito2.SelectedValue = 0

                ddl.LlenaProvincia(ddlProvincia2, ddlDepartamento2.SelectedValue)
            Else
                ddlProvincia2.SelectedValue = 0
                ddlDistrito2.SelectedValue = 0

                ddlProvincia2.Enabled = False
                ddlDistrito2.Enabled = False

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlProvincia2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProvincia2.SelectedIndexChanged
        Try
            Dim ddl As New Combos

            If ddlProvincia2.SelectedValue <> 0 Then
                ddlDistrito2.Enabled = True
                ddl.LlenaDistrito(ddlDistrito2, ddlProvincia2.SelectedValue)
            Else
                ddlDistrito2.SelectedValue = 0
                ddlDistrito2.Enabled = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlDepartamento1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamento1.SelectedIndexChanged
        Try
            Dim ddl As New Combos

            If ddlDepartamento1.SelectedValue <> 0 Then
                ddlProvincia1.Enabled = True
                ddlDistrito1.Enabled = False
                ddlDistrito1.SelectedValue = 0

                ddl.LlenaProvincia(ddlProvincia1, ddlDepartamento1.SelectedValue)
            Else
                ddlProvincia1.Enabled = False
                ddlDistrito1.Enabled = False
                ddlProvincia1.SelectedValue = 0
                ddlDistrito1.SelectedValue = 0
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlProvincia1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProvincia1.SelectedIndexChanged
        Try
            Dim ddl As New Combos
            If ddlProvincia1.SelectedValue <> 0 Then
                ddlDistrito1.Enabled = True
                ddl.LlenaDistrito(ddlDistrito1, ddlProvincia1.SelectedValue)

            Else
                ddlDistrito1.SelectedValue = 0
                ddlDistrito1.Enabled = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarComboOperadorInternet()
        Try
            Dim dt As New Data.DataTable : Dim md_Funciones As New d_Funciones
            dt = md_Funciones.ObtenerDataTable("OPERADOR_INTERNET")

            Call md_Funciones.CargarCombo(Me.ddlOperadorInternet, dt, "codigo", "nombre", True, "-- Seleccione --", "")
            dt.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarComboOperadorMovil()
        Try
            Dim dt As New Data.DataTable : Dim md_Funciones As New d_Funciones
            dt = md_Funciones.ObtenerDataTable("OPERADOR_MOVIL")

            Call md_Funciones.CargarCombo(Me.ddlOperadorMovil, dt, "codigo", "nombre", True, "-- Seleccione --", "")
            dt.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlOperadorInternet_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOperadorInternet.SelectedIndexChanged
        Try
            Me.txtOperadorInternet.Text = String.Empty

            If ddlOperadorInternet.SelectedValue.Equals("OTRO") Then
                Me.txtOperadorInternet.ReadOnly = False
                Me.txtOperadorInternet.Focus()
            Else
                Me.txtOperadorInternet.ReadOnly = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlOperadorMovil_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOperadorMovil.SelectedIndexChanged
        Try
            Me.txtOperadorMovil.Text = String.Empty

            If ddlOperadorMovil.SelectedValue.Equals("OTRO") Then
                Me.txtOperadorMovil.ReadOnly = False
                Me.txtOperadorMovil.Focus()
            Else
                Me.txtOperadorMovil.ReadOnly = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class

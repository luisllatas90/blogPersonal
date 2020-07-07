﻿'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class frmactualizardni
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("codigo_alu") Is Nothing) Then
                'Response.Redirect("../ErrorSistema.aspx")
            End If

            If IsPostBack = False Then
                Dim objEnc As New EncriptaCodigos.clsEncripta
                Dim obj As New ClsConectarDatos
                Dim datos As New Data.DataTable
                Dim objFun As New ClsFunciones

                'hddCodigo_alu.Value = Session("codigo_alu")
                'hddCodigo_reg.Value = Session("codigo_alu")
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                '******** estaba comentado ********
                hddCodigo_alu.Value = CInt(Mid(objEnc.Decodifica(Request.QueryString("c")), 4))
                hddCodigo_reg.Value = CInt(Mid(objEnc.Decodifica(Request.QueryString("x")), 4))
                '**********************************
                'Cargar datos del alumno
                'datos = obj.TraerDataTable("ConsultarAlumno", "DI", Session("codigo_alu")) 'hddCodigo_alu.Value) '"081IA10264"
				hddCodigo_alu.Value = "081IA10264"
                datos = obj.TraerDataTable("ConsultarAlumno", "DI", hddCodigo_alu.Value)
                obj.CerrarConexion()
                obj = Nothing


                If datos.Rows.Count > 0 Then
                    Me.dlEstudiante.DataSource = datos
                    Me.dlEstudiante.DataBind()
                    'Consultar lugares para colegio
                    objFun.CargarListas(ddlPais, CargarLugares("PAIS", 0), "codigo_pai", "nombre_pai")
                    ddlPais.SelectedValue = 156
                    ddlPais_SelectedIndexChanged(sender, e)
                    ddlDepartamento_SelectedIndexChanged(sender, e)
                    ddlProvincia_SelectedIndexChanged(sender, e)
                    ddlDistrito_SelectedIndexChanged(sender, e)

                    ' cargar datos
                    If datos.Rows(0).Item("tipoDocIdent_Pso").ToString.Trim = "DNI" Then
                        dpTipoDoc.SelectedValue = "DNI"
                        dpTipoDoc.visible = False
                        lblDni.text = dpTipoDoc.SelectedValue
                        Me.txtdni.MaxLength = 8
                    Else
                        dpTipoDoc.SelectedValue = "CE"
                        Me.txtdni.MaxLength = 15
                        lblDni.text = dpTipoDoc.SelectedValue
                    End If

                    'rblSexo.SelectedValue = datos.Rows(0).Item("sexo_Pso").ToString.Trim 'Sexo

                    If datos.Rows(0).Item("sexo_Pso").ToString.Trim() <> "" Then
                        Me.lblSexo.text = iif(datos.Rows(0).Item("sexo_Pso").ToString.Trim = "M", "Masculino", "Femino")
                        Me.rblSexo.visible = False
                        rblSexo.SelectedValue = datos.Rows(0).Item("sexo_Pso").ToString.Trim
                    Else
                        rblSexo.SelectedValue = datos.Rows(0).Item("sexo_Pso").ToString.Trim 'Sexo
                        Me.lblSexo.visible = False
                    End If

                    'Me.TxtFechaNac.Text = datos.Rows(0).Item("fechaNacimiento_Pso")

                    If datos.Rows(0).Item("fechaNacimiento_Pso").ToString.Trim() <> "" Then
                        lblFechaNac.text = datos.Rows(0).Item("fechaNacimiento_Pso")
                        TxtFechaNac.visible = False
                        TxtFechaNac.text = datos.Rows(0).Item("fechaNacimiento_Pso")
                    Else
                        TxtFechaNac.text = ""
                        Me.lblFechaNac.visible = False
                    End If


                    'Me.txtCorreo.Text = datos.Rows(0).Item("emailAlternativo_Pso") Correo

                    If datos.Rows(0).Item("emailAlternativo_Pso").ToString.Trim() <> "" Then
                        lblCorreo.text = datos.Rows(0).Item("emailAlternativo_Pso")
                        txtCorreo.visible = False
                        txtCorreo.text = datos.Rows(0).Item("emailAlternativo_Pso")
                    Else
                        lblCorreo.text = ""
                        Me.lblCorreo.visible = False
                    End If


                    'Me.txtdni.Text = IIf(datos.Rows(0).Item("dni") Is DBNull.Value, "", datos.Rows(0).Item("dni"))
                    If (datos.Rows(0).Item("dni") Is DBNull.Value) Then
                        Me.txtdni.text = ""
                        Me.txtdni.visible = True
                        Me.lblnrodoc.visible = False
                    Else
                        Me.txtdni.text = datos.Rows(0).Item("dni")
                        Me.txtdni.Visible = False
                        Me.lblnrodoc.text = txtdni.text
                        Me.dpTipoDoc.Visible = False
                    End If

                    'Response.Write(Me.hddCodigo_alu.Value)

                    Me.ddlPais.SelectedValue = datos.Rows(0).Item("codigo_Pai")

                    Me.ddlDepartamento.SelectedValue = datos.Rows(0).Item("codigo_Dep")
                    ddlDepartamento_SelectedIndexChanged(sender, e)
                    Me.ddlProvincia.SelectedValue = datos.Rows(0).Item("codigo_Pro")
                    ddlProvincia_SelectedIndexChanged(sender, e)
                    Me.ddlDistrito.SelectedValue = datos.Rows(0).Item("codigo_Dis")
                    ddlDistrito_SelectedIndexChanged(sender, e)
                    Me.ddlColegio.SelectedValue = datos.Rows(0).Item("codigo_ied")
                    ddlColegio_SelectedIndexChanged(sender, e)
                    dpTipoDoc_SelectedIndexChanged(sender, e)

                    If ddlColegio.SelectedValue <> -1 Then
                        ddlPais.Enabled = False
                        ddlDepartamento.Enabled = False
                        ddlProvincia.Enabled = False
                        ddlDistrito.Enabled = False
                        ddlColegio.Enabled = False

                        ddlPais.visible = False
                        ddlDepartamento.visible = False
                        ddlProvincia.visible = False
                        ddlDistrito.visible = False
                        ddlColegio.visible = False

                        lblPais.Text = ddlPais.SelectedItem.ToString
                        lblDep.Text = ddlDepartamento.SelectedItem.ToString
                        lblProvincia.Text = ddlProvincia.SelectedItem.ToString
                        lblDistrito.Text = ddlDistrito.SelectedItem.ToString
                        lblColegio.Text = ddlColegio.SelectedItem.ToString

                    Else
                        ddlPais.Enabled = True
                        ddlDepartamento.Enabled = True
                        ddlProvincia.Enabled = True
                        ddlDistrito.Enabled = True
                        ddlColegio.Enabled = True

                        lblPais.visible = False
                        lblDep.visible = False
                        lblProvincia.visible = False
                        lblDistrito.visible = False
                        lblColegio.visible = False

                    End If
                    If Request.QueryString("accion") = "M" Then
                        Me.cmdCancelar.UseSubmitBehavior = False
                        Me.cmdCancelar.Attributes.Add("onclick", "self.parent.tb_remove();")
                        Me.dpTipoDoc.Enabled = True
                    Else
                        Me.cmdCancelar.UseSubmitBehavior = True
                        Me.dpTipoDoc.Enabled = False
                    End If
                    Me.txtdni.Attributes.Add("onKeyPress", "validarnumero()")
                End If


            End If
        Catch ex As Exception
            Response.Write("Error:" & ex.Message)
        End Try

    End Sub

    Private Function CargarLugares(ByVal tipo As String, ByVal codigoP As Int16) As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim idtipo As Int16
        Dim datos As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        If tipo = "PAIS" Then       ' Pais
            idtipo = 1
        ElseIf tipo = "DEP" Then    ' Departamento
            idtipo = 2
        ElseIf tipo = "PRO" Then    ' Provincia
            idtipo = 3
        Else                        ' Distrito
            idtipo = 4
        End If

        obj.AbrirConexion()
        datos = obj.TraerDataTable("ConsultarLugares", idtipo, codigoP, 0)
        obj.CerrarConexion()
        Return datos
    End Function

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Me.lblmensaje.Text = ""
        'If ValidarNroIdentidad() = True Then
        Try
            If (ValidaFormulario() = True) Then
                Dim rpta(1) As String
                Dim obj As New ClsConectarDatos
                Dim Tipo As String
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()

                cmdGuardar.Enabled = False
                If Request.QueryString("Tipo") Is Nothing Then
                    Tipo = "P"
                Else
                    Tipo = Request.QueryString("Tipo")
                End If

                obj.AbrirConexion()
                If Me.ddlColegio.SelectedValue = -1 Then
                    obj.Ejecutar("alu_actualizardni", CInt(hddCodigo_alu.Value), Me.txtdni.Text.ToString.Trim, _
                             hddCodigo_reg.Value, Tipo, Me.dpTipoDoc.SelectedValue.ToString, _
                             Me.txtCorreo.Text, Me.rblSexo.SelectedValue, Me.TxtFechaNac.Text, DBNull.Value, Me.txtOtroColegio.Text, _
                             0).copyto(rpta, 0)
                Else
                    obj.Ejecutar("alu_actualizardni", CInt(hddCodigo_alu.Value), Me.txtdni.Text.ToString.Trim, _
                             hddCodigo_reg.Value, Tipo, Me.dpTipoDoc.SelectedValue.ToString, _
                             Me.txtCorreo.Text, Me.rblSexo.SelectedValue, Me.TxtFechaNac.Text, Me.ddlColegio.SelectedValue, "", _
                             0).copyto(rpta, 0)
                End If

                'If Request.QueryString("accion") <> "M" Then
                'Actualizar el campo que ya llenó dni. Osea actualizodatos_alu de la tbl alumno
                obj.Ejecutar("ALU_EstadoActualizarDatosAlumno", 1, hddCodigo_alu.Value)
                obj.Ejecutar("ACAD_CambiaClave", hddCodigo_alu.Value, Me.txtClaveNueva.Text)
                'End If
                obj.CerrarConexion()
                obj = Nothing

                Select Case rpta(0)
                    Case "1"
                        If Request.QueryString("accion") = "M" Then
                            Page.RegisterStartupScript("ok", "<script>alert('Los datos se han guardado correctamente');window.parent.location.reload();self.parent.tb_remove()</script>")
                        Else
                            Page.RegisterStartupScript("ok", "<script>alert('Los datos se han guardado correctamente')</script>")
                            AccederACampus()
                        End If
                    Case "2" : Me.lblmensaje.Text = "No puede guardar al estudiante 2 veces con el mismo DNI"
                    Case Else
                        Me.lblmensaje.Text = "El DNI que desea ingresar ya existe en la Base de datos asignada a: " & rpta(0).ToString
                End Select
            End If            
        Catch ex As Exception
            Me.lblmensaje.Text = "Ha ocurrido un ERROR al guardar los datos.<BR />" & ex.Message
        End Try
        'End If
    End Sub

    Public Function ValidaFormulario() As Boolean
        Dim obj As New ClsConectarDatos        
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            'Verifica que no tenga los textbox vacios
            If ((Me.txtClaveActual.Text.Trim = "") Or (Me.txtClaveNueva.Text.Trim = "")) Then
                Me.lblmensaje.Text = "Debe ingresar una clave"
                Return False
            End If

            'Verifica que no tenga DNI Vacios
            If (Me.txtdni.Text.Trim = "") Then
                Me.lblmensaje.Text = "Debe ingresar un DNI"
                Return False
            End If

            'Verifica que no se repitan el valor de los textbox
            If (Me.txtClaveActual.Text = Me.txtClaveNueva.Text) Then
                Me.lblmensaje.Text = "Las claves son iguales"
                Return False
            End If

            'Verifica que las contraseña anterior sea la correcta
            Dim dtPassword As New Data.DataTable
            obj.AbrirConexion()
            dtPassword = obj.TraerDataTable("ACAD_RetornaPassword", Me.txtClaveActual.Text, hddCodigo_alu.Value)
            obj.CerrarConexion()
            If (dtPassword.Rows.Count > 0) Then
                If (dtPassword.Rows(0).Item(0).ToString.ToUpper <> Me.txtClaveActual.Text.Trim.ToUpper) Then
                    Me.lblmensaje.Text = "La clave actual no coincide con la registrada en el sistema"
                    Return False
                End If
            End If            

            'Verifica que no haya un dni repetido en mismo codigo_test
            Dim dt As New Data.DataTable
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_VerificaDNI", Me.txtdni.Text, hddCodigo_alu.Value)
            obj.CerrarConexion()

            If (dt.Rows(0).Item(0).ToString = "S") Then
                Me.lblmensaje.Text = "Existe un DNI con este numero"
                Return False
            Else
                Me.lblmensaje.Text = ""
                Return True
            End If

        Catch ex As Exception
            Me.lblmensaje.Text = "Error en el registro de claves: " & ex.Message
        End Try
    End Function

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
    'Private Function ValidarNroIdentidad() As Boolean
    '    'Validar DNI
    '    If Me.dpTipoDoc.SelectedIndex = 1 And (Me.txtdni.Text.Length <> 8 Or _
    '                                           IsNumeric(Me.txtdni.Text.Trim) = False Or _
    '                                           Val(Me.txtdni.Text) < 1000000) Then
    '        Me.lblmensaje.Text = "El número de DNI es incorrecto. Mínimo 8 caracteres"
    '        ValidarNroIdentidad = False
    '        Me.txtdni.Focus()
    '        Exit Function
    '    Else
    '        ValidarNroIdentidad = True
    '    End If
    '    'Validar Carné de Extranjería
    '    If Me.dpTipoDoc.SelectedIndex = 2 And Me.txtdni.Text.Length < 13 Then
    '        Me.lblmensaje.Text = "El número de pasaporte es incorrecto. Mínimo 9 caracteres"
    '        ValidarNroIdentidad = False
    '        Me.txtdni.Focus()
    '        Exit Function
    '    Else
    '        ValidarNroIdentidad = True
    '    End If
    'End Function

    '****************** BOTON CANCELAR ******************************
    'Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
    '    'aqui programar para que llame al acceder de estudiante.
    '    If Request.QueryString("accion") <> "M" Then
    '        'Page.RegisterStartupScript("ok", "<script>alert('Lo sentimos, a partir de este año el DNI es obligatorio, mayor información en la Oficina de Pensiones');window.close();</script>")
    '        AccederACampus()
    '    Else
    '        'Page.RegisterStartupScript("cerrar", "<script>window.close();</script>")
    '    End If

    'End Sub

    Private Sub AccederACampus()
        If Request.QueryString("accion") <> "M" Then
            '-- Dni obligatorio --
            'ClientScript.RegisterStartupScript(Me.GetType, "cerrar", "window.close();", True)
            '-- Permitir que pase a campus--
            Dim rutaPag, pagina, mensaje As String
            mensaje = ""
            pagina = "avisos.asp"
            'Response.Write("<script>" & mensaje & "alert('Los datos se han guardado correctamente'); top.location.href='" & rutaPag & "'</script>")
            rutaPag = "../../estudiante/abriraplicacion.asp?codigo_tfu=3&codigo_apl=8&descripcion_apl=Campus Virtual USAT&enlace_apl=" & pagina & "&estilo_apl=N"
            Response.Redirect(rutaPag)

        End If
    End Sub

    Protected Sub dpTipoDoc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpTipoDoc.SelectedIndexChanged
        If Me.dpTipoDoc.SelectedValue = "DNI" Then
            Me.txtdni.MaxLength = 8
        Else
            Me.txtdni.MaxLength = 13
        End If
    End Sub

    Protected Sub ddlPais_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPais.SelectedIndexChanged
        Dim objFun As New ClsFunciones
        objFun.CargarListas(ddlDepartamento, CargarLugares("DEP", Me.ddlPais.SelectedValue), "codigo_dep", "nombre_dep", "-- Seleccione --")
        ddlDepartamento_SelectedIndexChanged(sender, e)
        If ddlPais.SelectedValue = 156 Then
            cvDepartamento.Enabled = True
            cvProvincia.Enabled = True
            cvDistrito.Enabled = True
        Else
            cvDepartamento.Enabled = False
            cvProvincia.Enabled = False
            cvDistrito.Enabled = False
        End If
    End Sub

    Protected Sub ddlDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamento.SelectedIndexChanged
        Dim objFun As New ClsFunciones
        objFun.CargarListas(ddlProvincia, CargarLugares("PRO", Me.ddlDepartamento.SelectedValue), "codigo_pro", "nombre_pro", "-- Seleccione --")
        ddlProvincia_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub ddlProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProvincia.SelectedIndexChanged
        Dim objFun As New ClsFunciones
        objFun.CargarListas(ddlDistrito, CargarLugares("DIS", Me.ddlProvincia.SelectedValue), "codigo_dis", "nombre_dis", "-- Seleccione --")
        ddlDistrito_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub ddlDistrito_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDistrito.SelectedIndexChanged
        Dim obj As New ClsConectarDatos
        Dim datos As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        datos = obj.TraerDataTable("PEC_ConsultarInstitucionesEducativasPorUbicacion", "DIS", ddlDistrito.SelectedValue)
        obj.CerrarConexion()
        Dim objFun As New ClsFunciones
        objFun.CargarListas(ddlColegio, datos, "codigo_ied", "nombre_ied", "Otro")
        ddlColegio_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub ddlColegio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlColegio.SelectedIndexChanged
        If ddlColegio.SelectedValue = -1 Then
            Me.txtOtroColegio.Visible = True
            'Me.rfvOtroColegio.Enabled = True
        Else
            Me.txtOtroColegio.Visible = False
            'rfvOtroColegio.Enabled = False
        End If
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        AccederACampus()
    End Sub
End Class
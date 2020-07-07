'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class Inscripcion_frmRegistroInscripcion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            Dim Obj As New ClsConectarDatos
            Dim codAlu As String
            Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            '     Try
            Dim Tbl As New Data.DataTable
            Dim obEnc As New EncriptaCodigos.clsEncripta
            codAlu = Mid(obEnc.Decodifica(Request.QueryString("ctm")), 4)
            codAlu = Mid(obEnc.Decodifica(codAlu), 4)

            If (Request.QueryString("cco") <> Nothing) Then
                Dim dtcco As New Data.DataTable
                Obj.AbrirConexion()
                dtcco = Obj.TraerDataTable("EVE_BuscaEventoToken", Request.QueryString("cco"))
                Obj.CerrarConexion()
                Me.Hdcodigo_cco.Value = dtcco.Rows(0).Item("codigo_cco").ToString.Trim()
                ' Response.Write(Me.Hdcodigo_cco.Value)
                Me.lblCentroCosto.Text = "EVENTO: " + dtcco.Rows(0).Item("descripcion_Cco").ToString
                Me.ImgEvento.ImageUrl = "afiches/" & dtcco.Rows(0).Item("imagen_dev").ToString.Trim()
            Else
            End If


            If (Request.QueryString("ctm") <> Nothing) Then
                If (Request.QueryString("ctm").Trim = "") Then
                    Response.Write("<script>alert('No se encontró al estudiante');</script>")
                    'Response.Redirect("http://www.usat.edu.pe/usat/")
                    Response.Redirect("//intranet.usat.edu.pe/usat/")
                End If
            Else
                Response.Write("<script>alert('No se encontró al estudiante');</script>")
                'Response.Redirect("http://www.usat.edu.pe/usat/")
                Response.Redirect("//intranet.usat.edu.pe/usat/")
            End If
            Me.Hdcodigo_alu.Value = codAlu ' Request.QueryString("ctm")
            Obj.AbrirConexion()
            Tbl = Obj.TraerDataTable("consultaracceso", "E", codAlu, 0)
            Obj.CerrarConexion()
            If Tbl.Rows.Count > 0 Then
                hddcodigo_alu.Value = Tbl.Rows(0).Item("codigo_alu")
                Me.lblcodigo.Text = Tbl.Rows(0).Item("codigouniver_alu")
                Me.lblalumno.Text = Tbl.Rows(0).Item("Alumno")
                Me.lblescuela.Text = Tbl.Rows(0).Item("nombre_cpf")
                Me.lblcicloingreso.Text = Tbl.Rows(0).Item("cicloing_alu")
                Me.lblPlan.Text = Tbl.Rows(0).Item("descripcion_pes")

                'Cargar la Foto
                Dim ruta As String


                ruta = obEnc.CodificaWeb("069" & Tbl.Rows(0).Item("codigouniver_alu").ToString)
                'ruta = "http://www.usat.edu.pe/imgestudiantes/" & ruta
                ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
                Me.FotoAlumno.ImageUrl = ruta   '"//intranet.usat.edu.pe/imgestudiantes/06C/1AIVCAAQ1OL.jpg" 
                obEnc = Nothing
                Tbl = Nothing


                If (Hdcodigo_cco.Value <> 0) Then
                    Dim dtServicios As New Data.DataTable
                    Obj.AbrirConexion()
                    dtServicios = Obj.TraerDataTable("LOG_BuscaServicioConcepto", 0, Hdcodigo_cco.Value)
                    Me.cboServicio.DataSource = dtServicios
                    Me.cboServicio.DataTextField = "etiqueta_scc"
                    Me.cboServicio.DataValueField = "codigo_Sco"
                    Me.cboServicio.DataBind()

                    If (Me.cboServicio.Items.Count > 0) Then
                        Me.cboServicio.SelectedIndex = 0
                    End If

                    Obj.CerrarConexion()
                    dtServicios.Dispose()
                End If

                'Buscamos el ciclo actual
                Dim dtCiclo As New Data.DataTable
                Obj.AbrirConexion()
                dtCiclo = Obj.TraerDataTable("LOG_CicloAcademicoActual")
                If (dtCiclo.Rows.Count > 0) Then
                    Hdcodigo_cac.Value = dtCiclo.Rows(0).Item("codigo_Cac")
                Else
                    Response.Write("<script>alert('No se encontró un ciclo academico');</script>")
                    Hdcodigo_cac.Value = 0
                End If
                Obj.CerrarConexion()
                dtCiclo.Dispose()
                CargaDeudas(Me.hddcodigo_alu.Value, Me.Hdcodigo_cac.Value, Me.Hdcodigo_cco.Value)
            Else
                Response.Write("<script>alert('No se encontró al alumno');</script>")
            End If
        Else
            If ((Me.hddcodigo_alu.Value.Trim <> "") And (Me.Hdcodigo_cco.Value.Trim <> "") _
                        And (Me.Hdcodigo_cac.Value.Trim <> "")) Then
                CargaDeudas(Me.hddcodigo_alu.Value, Me.Hdcodigo_cac.Value, Me.Hdcodigo_cco.Value)
            End If

            'Obj = Nothing
            '    Catch ex As Exception
            'Response.Write(ex.Message)
            '   End Try
        End If
    End Sub

    Private Sub CargaDeudas(ByVal codigo_Alu As String, ByVal codigo_cac As String, ByVal codigo_cco As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        Dim d As Integer = 0
        Dim entradas As Integer = 0
        Dim total As Integer = 0

        'Buscamos Deudas
        If (Me.Hdcodigo_cac.Value <> 0) Then
            Obj.AbrirConexion()
            Dim dtDeudas As New Data.DataTable
            dtDeudas = Obj.TraerDataTable("LOG_BuscaDeudaAlumno", codigo_Alu, codigo_cac, codigo_cco)
            Me.gvDeudas.DataSource = dtDeudas
            Me.gvDeudas.DataBind()
            obj.CerrarConexion()

            For d = 0 To dtDeudas.Rows.Count - 1
                total = total + CInt(dtDeudas.Rows(d).Item(2))
                'Response.Write(dtDeudas.Rows(d).Item(2))
            Next
            entradas = total / 25
            lblEntradas.Text = CStr(entradas)
            dtDeudas.Dispose()

            '  If (Me.gvDeudas.Rows.Count > 0) Then
            'Me.btnRegistrar.Enabled = False
            'Else
            '    Me.btnRegistrar.Enabled = True
            'End If
        End If
    End Sub

    Private Sub BloqueaControles(ByVal sw As Boolean)
        Me.btnCancelar.Enabled = sw
        Me.btnRegistrar.Enabled = sw
    End Sub



   
    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar.Click
        If (validaFormulario() = True) Then
            Dim Obj As New ClsConectarDatos

            Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            '  Try
            Dim codigo_alu As Integer
            Dim codigo_cac As Integer
            Dim cantidad As Integer
            Dim precio As Double
            Dim partes As Integer = 0
            Dim subtotal As Double = 0
            Dim aux As Double = 0

            codigo_alu = Me.hddcodigo_alu.Value
            codigo_cac = Hdcodigo_cac.Value
            precio = Me.txtPrecio.Text
            cantidad = Me.ddlCantidad.SelectedValue

            Dim dtServCon As New Data.DataTable
            Obj.AbrirConexion()
            dtServCon = Obj.TraerDataTable("ConsultarServicioConcepto", "CO", Me.cboServicio.SelectedValue)
            Obj.CerrarConexion()

            Dim strMoneda As String
            If (dtServCon.Rows.Count > 0) Then
                strMoneda = dtServCon.Rows(0).Item("moneda_sco")

                Dim lngCodigo_Deu As Object
                Obj.AbrirConexion()
                lngCodigo_Deu = Obj.Ejecutar("LOG_AgregarDeuda", "", "E", codigo_alu, Me.cboServicio.SelectedValue, _
                                        Me.Hdcodigo_cac.Value, CStr(cantidad) + " Entradas", _
                                        precio, 1, precio, "P", 1, Me.Hdcodigo_cco.Value, _
                                        1, 0, 0, 1)
                'Response.Write(CStr(precio) + "|ceco: " + Me.Hdcodigo_cco.Value + "| alu: " + CStr(codigo_alu) + "|sco: " + CStr(Me.cboServicio.SelectedValue) + "|cac: " + Me.Hdcodigo_cac.Value + "|deu: " + CStr(lngCodigo_Deu))
                Obj.Ejecutar("AVI_ActualizaInscripcion", Hdcodigo_alu.Value, Hdcodigo_cco.Value, Request.ServerVariables("REMOTE_ADDR"))
                Obj.CerrarConexion()
                Obj = Nothing
                CargaDeudas(Me.hddcodigo_alu.Value, Me.Hdcodigo_cac.Value, Me.Hdcodigo_cco.Value)
                Response.Write("<script> alert ('Operacion registrada correctamente.'); </script>")
                dtServCon.Dispose()
            Else
                Response.Write("<script> alert ('No se encontró al alumno'); </script>")
            End If
        End If
    End Sub

    Private Function validaFormulario() As Boolean
        If (Me.txtPrecio.Text.Trim = "") Then
            Response.Write("<script> alert ('Debe ingresar un cantidad'); </script>")
            Me.txtPrecio.Focus()
            Return False
        End If

        If (Me.cboServicio.Text.Trim = "") Then
            Response.Write("<script> alert ('Debe seleccionar un servicio'); </script>")
            Me.cboServicio.Focus()
            Return False
        End If

        Return True
    End Function

    ''Protected Sub btnRegistrar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar0.Click
    ''    Try
    ''        Dim cls As New ClsMail
    ''        Dim obj As New ClsConectarDatos
    ''        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    ''        Dim codigo As String ' = "HGYGAS"

    ''        'AVI_VerificaInscripcioAviso
    ''        'Consultamos si antes se registro en el evento
    ''        Dim dtVerifica As New Data.DataTable
    ''        obj.AbrirConexion()
    ''        dtVerifica = obj.TraerDataTable("AVI_VerificaInscripcioAviso", Hdcodigo_alu.Value, Hdcodigo_cco.Value)
    ''        obj.CerrarConexion()

    ''        If (dtVerifica.Rows.Count = 0) Then
    ''            'Consultamos información del evento
    ''            Dim dtCco As New Data.DataTable
    ''            obj.AbrirConexion()
    ''            dtCco = obj.TraerDataTable("EVE_BuscaEventoToken", Request.QueryString("cco"))
    ''            obj.CerrarConexion()

    ''            If (dtCco.Rows.Count > 0) Then
    ''                'Registramos y Generamos el Codigo
    ''                obj.AbrirConexion()
    ''                Dim dt As New Data.DataTable                    
    ''                dt = obj.TraerDataTable("AVI_RegistraCodigoInscripcion", Hdcodigo_alu.Value, Hdcodigo_cco.Value, Request.ServerVariables("REMOTE_ADDR").ToString.Trim)
    ''                obj.CerrarConexion()

    ''                If (dt.Rows.Count > 0) Then
    ''                    codigo = dt.Rows(0).Item(0).ToString
    ''                    'Verifica correo electrónico
    ''                    If (dt.Rows(0).Item(1).ToString <> "") Then
    ''                        cls.EnviarMail("campusvirtual@usat.edu.pe", dtCco.Rows(0).Item("descripcion_Cco").ToString, dt.Rows(0).Item(1).ToString, "Confirmación de Inscripción " & dtCco.Rows(0).Item("descripcion_Cco").ToString, "Para confirmar tu inscripción ingresa el código que te figura a continuación: <br> <h1>" & codigo & "</h1>", True, "", dtCco.Rows(0).Item("CorreoPersonal").ToString)
    ''                        'cls.EnviarMail("campusvirtual@usat.edu.pe", "Prueba Sistemas CONEIS", "csenmache@usat.edu.pe", "Confirmación de Inscripción " & dtCco.Rows(0).Item("descripcion_Cco").ToString, "Para confirmar tu inscripción ingresa el código que te figura a continuación: <br> <h1>" & codigo & "</h1>", True, "", dtCco.Rows(0).Item("CorreoPersonal").ToString)
    ''                        Response.Write("<script> alert('Información del evento enviada a su cuenta de correo. Por favor revisar!.'); </script>")
    ''                    End If
    ''                    If (dt.Rows(0).Item(2).ToString <> "") Then
    ''                        cls.EnviarMail("campusvirtual@usat.edu.pe", dtCco.Rows(0).Item("descripcion_Cco").ToString, dt.Rows(0).Item(2).ToString, "Confirmación de Inscripción " & dtCco.Rows(0).Item("descripcion_Cco").ToString, "Para confirmar tu inscripción ingresa el código que te figura a continuación: <br> <h1>" & codigo & "</h1>", True, "", dtCco.Rows(0).Item("CorreoPersonal").ToString)
    ''                        'cls.EnviarMail("campusvirtual@usat.edu.pe", "Prueba Sistemas CONEIS", "csenmache@usat.edu.pe", "Confirmación de Inscripción " & dtCco.Rows(0).Item("descripcion_Cco").ToString, "Para confirmar tu inscripción ingresa el código que te figura a continuación: <br> <h1>" & codigo & "</h1>", True, "", dtCco.Rows(0).Item("CorreoPersonal").ToString)
    ''                    End If
    ''                    Me.lblcodigoconfirmacion.Visible = True
    ''                    Me.txtCodigoConfirmacion.Visible = True
    ''                    Me.btnRegistrar.Visible = True
    ''                    Me.btnRegistrar0.Visible = False
    ''                Else
    ''                    Response.Write("<script> alert ('Error al enviar al correo.'); </script>")
    ''                End If
    ''            Else
    ''                Response.Write("<script> alert ('No se encontro el evento.'); </script>")
    ''            End If
    ''        Else
    ''            Response.Write("<script> alert ('Ud. ya esta registrado en este evento'); </script>")
    ''        End If
    ''    Catch ex As Exception
    ''        Response.Write("<script> alert ('Error al retornar el codigo generado: " & ex.Message & "'); </script>")
    ''    End Try        
    ''End Sub

    'Protected Sub ddlCantidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCantidad.SelectedIndexChanged
    '    Dim cantidad As Integer
    '    Dim subtotal As Double
    '    cantidad = Me.ddlCantidad.SelectedValue
    '    subtotal = cantidad * 25
    '    Me.txtPrecio.Text = "S/. " & FormatNumber(subtotal, 2)
    'End Sub



    Protected Sub ddlCantidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCantidad.SelectedIndexChanged
        Dim cantidad As Integer
        Dim subtotal As Double
        cantidad = Me.ddlCantidad.SelectedValue

        If Hdcodigo_cco.Value = 3168 Then 'reemplazar por el código del ceco creado'
            'Select Case cantidad
            '    Case 1 : subtotal = 35
            '    Case 2 : subtotal = 60
            '    Case 3 : subtotal = 95
            '    Case 4 : subtotal = 120
            '    Case 5 : subtotal = 155
            '    Case 6 : subtotal = 180
            '    Case 7 : subtotal = 215
            '    Case 8 : subtotal = 240
            '    Case 9 : subtotal = 275
            '    Case 10 : subtotal = 300
            'End Select
            'If cantidad = 1 Then
            '    subtotal = 35
            'ElseIf (cantidad Mod 2) = 0 Then
            '    subtotal = (cantidad / 2) * 60
            'Else
            '    subtotal = ((cantidad / 2)) * 60 + 35
            'End If
            subtotal = cantidad * 25
            Me.txtPrecio.Text = "S/. " & FormatNumber(subtotal, 2)
        End If

    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If Me.CheckBox1.Checked = True Then
            Me.btnRegistrar.Enabled = True
        Else
            Me.btnRegistrar.Enabled = False
        End If
    End Sub
End Class

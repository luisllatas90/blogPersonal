'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class Inscripcion_frmRegInscripcionPersonal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim Obj As New ClsConectarDatos
            Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            Try
                Dim Tbl As New Data.DataTable


                If (Request.QueryString("cco") <> Nothing) Then
                    Dim dtcco As New Data.DataTable
                    Obj.AbrirConexion()
                    dtcco = Obj.TraerDataTable("LOG_BuscaEventoToken", Request.QueryString("cco").Substring(0, 8))
                    Obj.CerrarConexion()
                    Me.Hdcodigo_cco.Value = dtcco.Rows(0).Item("codigo_cco")

                    'Response.Write(Me.Hdcodigo_cco.Value)

                    Me.lblCentroCosto.Text = "EVENTO: " + dtcco.Rows(0).Item("descripcion_Cco").ToString
                    If (dtcco.Rows(0).Item("imagen_dev").ToString.Trim() <> "") Then
                       ' Me.ImgEvento.ImageUrl = "afiches/" & dtcco.Rows(0).Item("imagen_dev").ToString.Trim()
                        'Me.ImgEvento.Visible = True
                    Else
                        ' Me.ImgEvento.Visible = False
                    End If
                End If

                'If (Request.QueryString("ctm") <> Nothing) Then
                '    If (Request.QueryString("ctm").Trim = "") Then
                '        Response.Write("<script>alert('No se encontró al estudiante');</script>")
                '        'Response.Redirect("http://www.usat.edu.pe/usat/")
                '        '   Response.Redirect("https://intranet.usat.edu.pe/usat/")
                '    End If
                'Else
                '    Response.Write("<script>alert('No se encontró al estudiante');</script>")
                '    'Response.Redirect("http://www.usat.edu.pe/usat/")
                '    'Response.Redirect("https://intranet.usat.edu.pe/usat/")
                'End If

                '  Dim objDeco As New EncriptaCodigos.clsEncripta
                Me.Hdcodigo_per.Value = Request.QueryString("cco").Substring(12, Request.QueryString("cco").Length - 12)
                Obj.AbrirConexion()
                Tbl = Obj.TraerDataTable("ConsultarPersonal", "CO", Me.Hdcodigo_per.Value)
                Obj.CerrarConexion()
                If Tbl.Rows.Count > 0 Then
                    Me.lblPersonal.Text = Tbl.Rows(0).Item("descripcion_Tpe")
                    Me.lblNombrePersonal.Text = Tbl.Rows(0).Item("personal")
                    Me.lblDocumento.Text = Tbl.Rows(0).Item("nroDocIdentidad_Per")
                    Me.lblCivil.Text = Tbl.Rows(0).Item("estadoCivil_Per")

                    'Cargar la Foto
                    Dim ruta As String
                    Dim obEnc As Object
                    'obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

                    'ruta = "http://www.usat.edu.pe/campusvirtual/personal/imgpersonal/" & Me.Hdcodigo_per.Value & ".jpg"
                    ruta = "../../personal/imgpersonal/" & Me.Hdcodigo_per.Value & ".jpg"
                    Me.FotoPersonal.ImageUrl = ruta
                    obEnc = Nothing
                    Tbl = Nothing


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


                    'Buscamos Deudas
                    If (Me.Hdcodigo_cac.Value <> 0) Then
                        cargarData()
                    End If
                Else
                    Response.Write("<script>alert('No se encontro al alumno');</script>")
                End If

                Obj = Nothing
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End If
    End Sub

    Private Sub registrar(ByVal personal As Integer, ByVal servicio As Integer, ByVal cantidad As Integer, ByVal subtotal As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        Dim lngCodigo_Deu As Object
        obj.AbrirConexion()
        lngCodigo_Deu = obj.Ejecutar("[LOG_AgregarDeudaPersonal]", "", "P", personal, servicio, _
                                Me.Hdcodigo_cac.Value, CStr(cantidad) + " Entradas", _
                                subtotal, 1, subtotal, "P", 1, Me.Hdcodigo_cco.Value, _
                                1, 0, 0, 1)
        'Response.Write(CStr(precio) + "|ceco: " + Me.Hdcodigo_cco.Value + "| alu: " + CStr(codigo_alu) + "|sco: " + CStr(Me.cboServicio.SelectedValue) + "|cac: " + Me.Hdcodigo_cac.Value + "|deu: " + CStr(lngCodigo_Deu))
        obj.Ejecutar("AVI_ActualizaInscripcion", personal, Hdcodigo_cco.Value, Request.ServerVariables("REMOTE_ADDR"))
        obj.CerrarConexion()
        obj = Nothing
        If lngCodigo_Deu <> -1 Then
            cargarData()
            Response.Write("<script>alert('Se registró la compra de su entrada');</script>")
        End If


    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        registrar(Hdcodigo_per.Value, 646, Me.txtCantidad1.Text, CInt(Me.txtCantidad1.Text) * 25)
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        ' registrar(Hdcodigo_per.Value, 1570, Me.txtCantidad2.Text, CInt(Me.txtCantidad2.Text) * 25)
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        'registrar(Hdcodigo_per.Value, 1571, Me.txtCantidad3.Text, CInt(Me.txtCantidad3.Text) * 10)
    End Sub
    Private Function consultarCompra(ByVal personal As Integer, ByVal servicio As Integer, ByVal codigo_Cac As Integer, ByVal cco As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString


        obj.AbrirConexion()
        dt = obj.TraerDataTable("AVI_ConsultarEntradas", personal, servicio, codigo_Cac, cco)
        obj.CerrarConexion()
        obj = Nothing
        Return dt

    End Function
    Public Sub cargarData()

        Me.lblCantidad2.Text = 0
        Me.lblCantidad3.Text = 0
        Me.lblSubtotal1.Text = 0
        Me.lblSubtotal2.Text = 0
        Me.lblSubtotal3.Text = 0
        Dim dt1 As New Data.DataTable
        Dim dt2 As New Data.DataTable
        Dim dt3 As New Data.DataTable

        '   CargaGrid(Me.Hdcodigo_per.Value, Me.Hdcodigo_cac.Value, Me.Hdcodigo_cco.Value)
        dt1 = consultarCompra(Me.Hdcodigo_per.Value, 646, Me.Hdcodigo_cac.Value, Me.Hdcodigo_cco.Value)
        If dt1.Rows(0).Item(0).ToString.Trim <> "" Then
            Me.lblCantidad1.Text = CInt(dt1.Rows(0).Item(0).ToString)
            Me.lblSubtotal1.Text = dt1.Rows(0).Item(1).ToString
        End If
        If CInt(lblCantidad1.Text) > 0 Then Me.Button1.Enabled = False
        dt1.Dispose()

        '   dt2 = consultarCompra(Me.Hdcodigo_per.Value, 3623, Me.Hdcodigo_cac.Value, Me.Hdcodigo_cco.Value)
        'If dt2.Rows(0).Item(0).ToString.Trim <> "" Then
        '    Me.lblCantidad2.Text = CInt(dt2.Rows(0).Item(0).ToString)
        '    Me.lblSubtotal2.Text = dt2.Rows(0).Item(1).ToString
        'End If
        'dt2.Dispose()

        'dt3 = consultarCompra(Me.Hdcodigo_per.Value, 3623, Me.Hdcodigo_cac.Value, Me.Hdcodigo_cco.Value)
        'If dt3.Rows(0).Item(0).ToString.Trim <> "" Then
        '    Me.lblCantidad3.Text = CInt(dt3.Rows(0).Item(0).ToString)
        '    Me.lblSubtotal3.Text = dt3.Rows(0).Item(1).ToString
        'End If

        Me.lblCantidad4.Text = CInt(Me.lblCantidad1.Text) '+ CInt(Me.lblCantidad2.Text) + CInt(Me.lblCantidad3.Text)
        Me.lblSubtotal4.Text = FormatNumber(CInt(Me.lblSubtotal1.Text), 2) ' + CInt(Me.lblSubtotal2.Text) + CInt(Me.lblSubtotal3.Text), 2)
        dt3.Dispose()
    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If Me.CheckBox1.Checked = True Then
            Me.Panel1.Enabled = True
        Else
            Me.Panel1.Enabled = False
        End If
    End Sub
End Class


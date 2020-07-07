﻿'----------------------------------------------------------------------
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
                    dtcco = Obj.TraerDataTable("LOG_BuscaEventoToken", Request.QueryString("cco"))
                    Obj.CerrarConexion()
                    Me.Hdcodigo_cco.Value = dtcco.Rows(0).Item("codigo_cco").ToString.Trim()
                    Me.lblCentroCosto.Text = "EVENTO: " + dtcco.Rows(0).Item("descripcion_Cco").ToString
                    If (dtcco.Rows(0).Item("imagen_dev").ToString.Trim() <> "") Then
                        Me.ImgEvento.ImageUrl = "afiches/" & dtcco.Rows(0).Item("imagen_dev").ToString.Trim()
                        Me.ImgEvento.Visible = True
                    Else
                        Me.ImgEvento.Visible = False
                    End If
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

                Dim objDeco As New EncriptaCodigos.clsEncripta
                Me.Hdcodigo_per.Value = Mid(objDeco.Decodifica(Request.QueryString("ctm")), 4)                

                Obj.AbrirConexion()
                Tbl = Obj.TraerDataTable("LOG_ConsultaPersonal", Me.Hdcodigo_per.Value)
                Obj.CerrarConexion()
                If Tbl.Rows.Count > 0 Then
                    Me.lblPersonal.Text = Tbl.Rows(0).Item("descripcion_Tpe")
                    Me.lblNombrePersonal.Text = Tbl.Rows(0).Item("NombreCompleto")
                    Me.lblDocumento.Text = Tbl.Rows(0).Item("nroDocIdentidad_Per")
                    Me.lblCivil.Text = Tbl.Rows(0).Item("estadoCivil_Per")

                    'Cargar la Foto
                    Dim ruta As String
                    Dim obEnc As Object
                    obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

                    'ruta = "http://www.usat.edu.pe/campusvirtual/personal/imgpersonal/" & Me.Hdcodigo_per.Value & ".jpg"
                    ruta = "../../personal/imgpersonal/" & Me.Hdcodigo_per.Value & ".jpg"
                    Me.FotoPersonal.ImageUrl = ruta
                    obEnc = Nothing
                    Tbl = Nothing


                    If (Hdcodigo_cco.Value <> 0) Then
                        Dim dtServicios As New Data.DataTable
                        Obj.AbrirConexion()
                        dtServicios = Obj.TraerDataTable("LOG_BuscaServicioConcepto", 0, Hdcodigo_cco.Value)
                        Me.cboServicio.DataSource = dtServicios
                        Me.cboServicio.DataTextField = "descripcion_Sco"
                        Me.cboServicio.DataValueField = "codigo_Sco"
                        Me.cboServicio.DataBind()

                        If (Me.cboServicio.Items.Count > 0) Then
                            Me.cboServicio.SelectedIndex = 0
                            CargaCboPartes()
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

                    'Buscamos Deudas
                    If (Me.Hdcodigo_cac.Value <> 0) Then
                        CargaGrid(Me.Hdcodigo_per.Value, Me.Hdcodigo_cac.Value, Me.Hdcodigo_cco.Value)
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

    Private Sub CargaGrid(ByVal codigo_per As String, ByVal codigo_cac As String, ByVal codigo_cco As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        Obj.AbrirConexion()
        Dim dtDeudas As New Data.DataTable
        dtDeudas = obj.TraerDataTable("LOG_BuscaDeudaPersonal", codigo_per, codigo_cac, codigo_cco)
        Me.gvDeudas.DataSource = dtDeudas
        Me.gvDeudas.DataBind()
        Obj.CerrarConexion()
        dtDeudas.Dispose()
        If (Me.gvDeudas.Rows.Count > 0) Then
            Me.btnRegistrar.Enabled = False
        Else
            Me.btnRegistrar.Enabled = True
        End If
    End Sub

    Private Sub BloqueaControles(ByVal sw As Boolean)
        Me.btnCancelar.Enabled = sw
        Me.btnRegistrar.Enabled = sw
    End Sub

    Protected Sub cboServicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboServicio.SelectedIndexChanged
        CargaCboPartes()
    End Sub

    Private Sub CargaCboPartes()
        Dim Obj As New ClsConectarDatos
        Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        Try
            Dim dtServicios As New Data.DataTable
            Obj.AbrirConexion()
            'Combo Servicio el value es Codigo_Sco
            dtServicios = Obj.TraerDataTable("LOG_BuscaServicioConcepto", 0, Me.Hdcodigo_cco.Value)
            Me.cboPartes.Items.Clear()
            If (dtServicios.Rows.Count > 0) Then
                If (dtServicios.Rows(0).Item("nroPartes_Sco").ToString.Trim <> "") Then
                    For i As Integer = 0 To dtServicios.Rows.Count - 1
                        If (dtServicios.Rows(i).Item("codigo_Sco") = Me.cboServicio.SelectedValue) Then
                            For j As Integer = 0 To Integer.Parse(dtServicios.Rows(i).Item("nroPartes_Sco").ToString) - 1
                                Me.cboPartes.Items.Add(j + 1)
                            Next
                            Me.txtPrecio.Text = dtServicios.Rows(i).Item("precio_Sco").ToString
                        End If
                    Next
                End If
                BloqueaControles(True)
            Else
                BloqueaControles(False)
            End If

            Obj.CerrarConexion()
            dtServicios.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message)
            Obj.CerrarConexion()
        End Try
    End Sub

    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar.Click
        If (validaFormulario() = True) Then
            Dim Obj As New ClsConectarDatos
            Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            Try
                Dim codigo_Per As Integer
                Dim codigo_cac As Integer
                Dim cantidad As Integer
                Dim precio As Double
                Dim partes As Integer
                codigo_Per = Me.Hdcodigo_per.Value
                codigo_cac = Me.Hdcodigo_cac.Value
                precio = Me.txtPrecio.Text
                partes = Me.cboPartes.Text

                If (Me.cboServicio.SelectedValue = 1) Then
                    'precio_sco = 100
                    'codigo_sco = 630
                    'codigo_cco = 2125
                    'cantidad = 1
                End If

                Dim dtServCon As New Data.DataTable
                Obj.AbrirConexion()
                dtServCon = Obj.TraerDataTable("ConsultarServicioConcepto", "CO", Me.cboServicio.SelectedValue)
                Obj.CerrarConexion()

                Dim strMoneda As String
                If (dtServCon.Rows.Count > 0) Then
                    strMoneda = dtServCon.Rows(0).Item("moneda_sco")

                    Dim dtDeudas As Data.DataTable
                    Obj.AbrirConexion()
                    dtDeudas = Obj.TraerDataTable("consultarExistenciaDeuda_v2", "P", Me.Hdcodigo_per.Value, Me.cboServicio.SelectedValue, Me.Hdcodigo_cac.Value, Me.Hdcodigo_cco.Value)
                    Obj.CerrarConexion()

                    If (dtDeudas.Rows.Count > 0) Then
                        Response.Write(" <script> alert ('ERROR: Este servicio ya lo ha solicitado. " & _
                            "\nPor este medio, sólo podrá solicitar cada servicio una vez por ciclo.'); </script")
                    Else
                        Dim lngCodigo_Deu As Object
                        Obj.AbrirConexion()

                        'Grabar Deuda
                        lngCodigo_Deu = Obj.Ejecutar("LOG_AgregarDeudaPersonal", "", "E", codigo_Per, Me.cboServicio.SelectedValue, _
                                                Me.Hdcodigo_cac.Value, "Inscripcion Web (" + CStr(partes) + " Partes)", _
                                                Me.txtPrecio.Text, strMoneda, Me.txtPrecio.Text, "P", 1, Me.Hdcodigo_cco.Value, _
                                                1, 0, 0, partes)

                        Obj.CerrarConexion()
                        Obj = Nothing
                        CargaGrid(Me.Hdcodigo_per.Value, Me.Hdcodigo_cac.Value, Me.Hdcodigo_cco.Value)
                        Response.Write("<script> alert ('Operacion registrada correctamente.'); </script>")
                    End If
                    dtDeudas.Dispose()
                End If
                dtServCon.Dispose()
            Catch ex As Exception
                Response.Write(ex.Message)
                Obj.CerrarConexion()
            End Try
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

        If (Me.cboPartes.Text.Trim = "") Then
            Response.Write("<script> alert ('Debe seleccionar en cuantas partes pagará el servicio'); </script>")
            Me.cboPartes.Focus()
            Return False
        End If
        Return True
    End Function
End Class


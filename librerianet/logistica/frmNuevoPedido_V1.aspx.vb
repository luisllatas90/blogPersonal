Imports System.Net
Imports System.IO
Imports System.IO.File
Imports System.Collections.Generic
Imports System

Partial Class presupuesto_areas_RegistrarPresupuestoDetalle
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim objpre As New ClsPresupuesto
                Dim objfun As New ClsFunciones
                Dim objlog As New ClsLogistica
                Dim datos As New Data.DataTable
                'Cargar datos de usuario
                datos = objpre.ObtenerDatosUsuario(Request.QueryString("id"))
                If datos.Rows.Count > 0 Then
                    lblUsuario.Text = datos.Rows(0).Item("usuario")
                    lblCargo.Text = datos.Rows(0).Item("Cargo")
                    lblCentroCostos.Text = datos.Rows(0).Item("CentroCostos")
                End If
                'DFLORES - mostrar ejp del presente año
                ' datos = objpre.ConsultarProcesoContable()
                datos = objpre.ConsultarProcesoContable_V1()
                If datos.Rows.Count > 0 Then
                    objfun.CargarListas(cboPeriodoPresu, datos, "codigo_ejp", "descripcion_ejp")
                End If
                '--------------------------------------

                'temporal hasta iniciar la ejecución del proceso 2015
                'cboPeriodoPresu.SelectedValue = 7

                If Me.cboPeriodoPresu.SelectedValue < 8 Then
                    'Me.gvPresupuesto.Columns.RemoveAt(1)
                    datos = objpre.ObtenerListaCentroCostos("1", 523, Request.QueryString("id"))
                Else
                    'Me.gvPresupuesto.Columns.RemoveAt(0)
                    ' Hcano
                    datos = objpre.ObtenerListaCentroCostos_POA(Request.QueryString("id"), Request.QueryString("ctf"), Me.cboPeriodoPresu.SelectedValue, "")
                    '
                End If

                objfun.CargarListas(cboCecos, datos, "codigo_cco", "descripcion_Cco", ">> Seleccione<<")

                datos.Dispose()
                'Cargar datos de programa presuspuestal
                'objfun.CargarListas(cboProgramaPresu, objpre.ObtenerListaProgramaPresupuestal(True), "codigo_ppr", "descripcion_ppr", ">> Seleccione <<")
                objfun.CargarListas(Me.cboEstado, objlog.ConsultarEstados("PE"), "codigo_Eped", "descripcionEstado_Eped")
                If Request.QueryString("Tipo") = "E" Then
                    BloquearCabecera(False)
                Else
                    BloquearCabecera(True)
                End If
                txtPrecioUnit.Attributes.Add("onKeyPress", "validarnumero()")
                txtCantidad.Attributes.Add("onKeyPress", "validarnumero()")
                'txtConcepto.Attributes.Add("onkeypress", "BuscarItems(Me,'" + this.Button1.ClientID + "')");
                Panel1.Visible = False
                Panel3.Visible = False
                MostrarBusquedaCeCos(False)
                Me.TxtFechaEsperada.Text = FormatDateTime(Now, DateFormat.ShortDate)

                'Carga Combo de Almacen
                CargaCboAlmacen()
                'Carga Combo de Personal
                CargaPersonal()
                'fuCargarArchivo.Attributes.Add("onkeypress", "return false;")
                'fuCargarArchivo.Attributes.Add("onkeydown", "return false;")
                'fuCargarArchivo.Attributes.Add("onkeyup", "return false;")
                'fuCargarArchivo.Attributes.Add("onpaste", "return false;") 
                'AdjuntoProy.Attributes.Add("onClick", "ModalAdjuntar2('" & objlog.EncrytedString64(0) & "');")
                cmdEnviar.Attributes.Add("onclick", " this.disabled = true; " + Page.ClientScript.GetPostBackEventReference(cmdEnviar, Nothing) + ";")
                'cmdEnviar.Attributes.Add("onclick", " this.disabled = true; ")
                Inhabilitar_Registro()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaPersonal()
        Dim obj As New ClsConectarDatos
        Dim dt_Per As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt_Per = obj.TraerDataTable("LOG_BuscaPersonal", 0, "")
        obj.CerrarConexion()
        Me.cboPerDespachar.DataSource = dt_Per
        Me.cboPerDespachar.DataTextField = "nombre"
        Me.cboPerDespachar.DataValueField = "codigo_per"
        Me.cboPerDespachar.DataBind()

        For i As Integer = 0 To Me.cboPerDespachar.Items.Count
            If (Me.cboPerDespachar.Items(i).Value = Request.QueryString("id")) Then
                Me.cboPerDespachar.SelectedIndex = i
                i = Me.cboPerDespachar.Items.Count
            End If
        Next

        dt_Per.Dispose()
        obj = Nothing
    End Sub

    Private Sub CargaCboAlmacen()
        Dim obj As New ClsConectarDatos
        Dim dt_Alm As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt_Alm = obj.TraerDataTable("LOG_BuscaAlmacenPrincipal", 0, 0)
        obj.CerrarConexion()
        Me.cboAlmacen.DataSource = dt_Alm
        Me.cboAlmacen.DataTextField = "nombre_alm"
        Me.cboAlmacen.DataValueField = "codigo_alm"
        Me.cboAlmacen.DataBind()
        Me.cboAlmacen.SelectedValue = 1

        Dim dt_TI As New Data.DataTable
        obj.AbrirConexion()
        dt_TI = obj.TraerDataTable("LOG_RetornaPersonalTI", Request.QueryString("id"))
        obj.CerrarConexion()

        'Si retorna registros pertenece a TI
        If (dt_TI.Rows.Count > 0) Then
            Me.cboAlmacen.Enabled = True
        Else
            Me.cboAlmacen.Enabled = False
        End If

        dt_Alm.Dispose()
        obj = Nothing
    End Sub

    Private Sub BloquearCabecera(ByVal valor As Boolean)
        Me.cboCecos.Enabled = valor
        Me.txtCecos.Enabled = valor
    End Sub

    Private Sub AgregarFilaGrid(ByRef tabla As Data.DataTable, ByVal texto As String)
        Dim fila As Data.DataRow
        fila = tabla.NewRow()
        fila("cantidad") = texto
        tabla.Rows.Add(fila)
    End Sub

    'Protected Sub ImgBuscarItems_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarItems.Click

    '    BuscarItems()
    '    Me.txtDetPresup.Text = ""
    '    pnlPresupuesto.Visible = False
    'End Sub

    Protected Sub LinkButton3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton3.Click

        BuscarItems()
        Me.txtDetPresup.Text = ""
        pnlPresupuesto.Visible = False
    End Sub

    Private Sub BuscarItems()
        Try
            'Dim objPre As ClsLogistica
            'objPre = New ClsLogistica            
            'gvItems.DataSource = objPre.ConsultarConceptos(rblMovimiento.SelectedValue, -1, -1, -1, -1, txtConcepto.Text)
            'gvItems.DataBind()
            'objPre = Nothing 

            '===Hcano 24-11-16 : Comento 

            'Dim obj As New ClsConectarDatos
            'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            'obj.AbrirConexion()
            'gvItems.DataSource = obj.TraerDataTable("LOG_ConsultarConceptoEgreso_v1", -1, -1, -1, -1, Me.txtConcepto.Text, Me.cboAlmacen.SelectedValue)
            'gvItems.DataBind()
            'obj.CerrarConexion()
            'obj = Nothing

            '===Fin Hcano

            '--Hcano 24-11-16 
            Dim objPre As ClsPresupuesto
            objPre = New ClsPresupuesto
            'solo consulto EGRESOS POR ARTICULOS
            gvItems.DataSource = objPre.ConsultarConceptos_POA("E", txtConcepto.Text)
            gvItems.DataBind()
            objPre = Nothing
            Panel1.Visible = True
            '--

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub gvItems_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvItems.SelectedIndexChanged
        'Aquí se asignan los valores cuando se selecciona un Item NO presupuestado  
        txtCodItem.Text = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(0)
        Me.txtConcepto.Text = HttpUtility.HtmlDecode(gvItems.SelectedRow.Cells(1).Text)
        Me.lblUnidad.Text = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values("iduni")
        Me.lblDescripcionUnidad.Text = HttpUtility.HtmlDecode(gvItems.SelectedRow.Cells(2).Text)
        Me.txtComentarioReq.Text = Me.HdUnidad.Value
        Me.txtPrecioUnit.Text = HttpUtility.HtmlDecode(gvItems.SelectedRow.Cells(3).Text)

        SeleccionarItem()
    End Sub
    Private Sub SeleccionarItem()
        'If Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(3) = True Then
        If Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(3) = 1 Then
            lblValores.Text = "Cantidad"
            lblTexto.Text = "Precio Unitario (S/.)"
        Else
            lblValores.Text = "Importe"
            lblTexto.Text = "Cantidad"
        End If
        gvItems.Dispose()
        Panel1.Visible = False
    End Sub
    Private Sub limpiarDistribucion()
        lblTotalItem.Text = "0.00"
        lblDistribuidoItem.Text = "0.00"
        lblPorDistribuir.Text = "0.00"
        lblNombreItem.Text = ""
        Me.gvDistribucion.DataSource = Nothing
        gvDistribucion.DataBind()
        Me.txtCodigo_Ecc.Text = ""
        Me.txtCantidadDistribucion.Text = ""
    End Sub
    Private Function sumarDistribucion() As Double
        Dim i As Integer
        Dim suma As Double
        suma = 0
        For i = 0 To gvDistribucion.Rows.Count - 1
            suma = suma + gvDistribucion.Rows(i).Cells(1).Text
        Next
        sumarDistribucion = suma
    End Function

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim ObjPre As New ClsPresupuesto
        Dim tipo As String = ""
        Dim objLog As New ClsLogistica
        Dim datos As New Data.DataTable
        Dim codigo_per As Integer
        Dim codigo_cco As Integer
        Dim codDetPresup As Integer
        Dim dts As New Data.DataTable
        Dim valor As Integer
        Dim codigo_dpe As Integer
        Dim ruta As String

        Try
            If detPedNuevo.Text = 0 Then
                valor = Me.cboCecos.SelectedValue
                consultarResponsableAprobacion(valor)
                Me.detPedNuevo.Text = 1
            End If

            'ScriptManager1.RegisterStartupScript(Me,Me, Me.GetType, "error", "alert('Ocurrió un error al procesar los datos:')", True)

            'Hcano(29 - 11 - 16) : Agrego() 'Or Me.hdDisponible.Value = 0' para guardar como no presupuestado
            If Me.txtDetPresup.Text = "" Or Me.hdDisponible.Value = 0 Then
                codDetPresup = 0
            Else
                codDetPresup = Me.txtDetPresup.Text
            End If

            If (Me.cboCecos.SelectedValue = -1) Then
                ScriptManager1.RegisterStartupScript(Me, Me.GetType, "FaltanDatosCco", "alert('Debe seleccionar un centro de costos')", True)
                Exit Sub
            End If

            If (Me.cboDetalleActividadPOA.SelectedValue = -1) Then
                ScriptManager1.RegisterStartupScript(Me, Me.GetType, "FaltanDatosActividad", "alert('Debe seleccionar una Actividad')", True)
                Exit Sub
            End If

            If txtCodItem.Text.ToString = "" Or Me.txtConcepto.Text.ToString = "" Or Me.txtPrecioUnit.Text = "" Then
                ScriptManager1.RegisterStartupScript(Me, Me.GetType, "FaltanDatos", "alert('Por favor complete los datos correctamente. Haga clic en el [ícono de búsqueda] y luego selecciónelo.')", True)
                Exit Sub
            End If

            If Me.txtCantidad.Text <= 0 Then
                ScriptManager1.RegisterStartupScript(Me, Me.GetType, "CorregirDatos", "alert('La cantidad a solicitar debe ser mayor a 0.')", True)
                Exit Sub
            End If

            If Me.hdDisponible.Value > 0 Then
              
                If Double.Parse(Double.Parse(Me.txtPrecioUnit.Text) * Double.Parse(Me.txtCantidad.Text)) > Double.Parse(Me.hdDisponible.Value) Then
                    ScriptManager1.RegisterStartupScript(Me, Me.GetType, "CorregirDatos", "alert('El PRECIO X CANTIDAD a SOLICITAR no debe superar lo DISPONIBLE presupuestado: S/. " & Me.hdDisponible.Value.ToString & "')", True)
                    Exit Sub
                End If
            End If
            'If Me.txtfile.PostedFile.FileName <> "" Then
            '    ScriptManager1.RegisterStartupScript(Me, Me.GetType, "CorregirDatos", "alert('El archivo adjunto supera el peso máximo: 4MB')", True)
            '    Exit Sub
            'End If

            objLog.AbrirTransaccionCnx()

            If txtPedido.Text = "" Then
                datos = ObjPre.ObtenerDatosUsuario(Request.QueryString("id"))
                If datos.Rows.Count > 0 Then
                    codigo_per = datos.Rows(0).Item("codigo_per")
                    codigo_cco = datos.Rows(0).Item("codigo_cco")
                End If
                'Registrar la cabecera del pedido sólo la primera vez                
                If (Me.cboAlmacen.SelectedValue = 4) Then
                    'Si es Almacen de TI que automaticamente pase a aprobado
                    Me.txtPedido.Text = objLog.AgregarPedido_v1(codigo_per, Me.cboCecos.SelectedValue, 11, 0, _
                                                            CDate("31/12/2009"), "", Me.cboPeriodoPresu.SelectedValue, _
                                                            Me.cboAlmacen.SelectedValue)
                Else
                    Me.txtPedido.Text = objLog.AgregarPedido_v1(codigo_per, Me.cboCecos.SelectedValue, 1, 0, _
                                                            CDate("31/12/2009"), "", Me.cboPeriodoPresu.SelectedValue, _
                                                            Me.cboAlmacen.SelectedValue)
                End If


            End If


            'Me.txtEspecificaciones.Text &= IIf(Trim(ruta) = "", "", "|" & ruta)


            ' Registrar el detalle del pedido 
            'Hcano 24-11-16 : 
            'Comento
            'objLog.AgregarDetallePedido_v1(Me.txtPedido.Text, txtCodItem.Text, cboCecos.SelectedValue, _
            '                          Me.txtPrecioUnit.Text, Me.txtCantidad.Text, Me.txtComentarioReq.Text, _
            '                          Me.TxtFechaEsperada.Text, 1, "S", rblModoDistribucion.SelectedValue, _
            '                          Me.cboProgramaPresu.SelectedValue, codDetPresup, Me.lblUnidad.Text, _
            '                          Me.cboPerDespachar.SelectedValue, False, Me.txtEspecificaciones.Text)
            'Agrego

            codigo_dpe = objLog.AgregarDetallePedido_POA(Me.txtPedido.Text, txtCodItem.Text, cboCecos.SelectedValue, _
                                           Me.txtPrecioUnit.Text, Me.txtCantidad.Text, Me.txtComentarioReq.Text, _
                                           Me.TxtFechaEsperada.Text, 1, "S", rblModoDistribucion.SelectedValue, _
                                           "1", codDetPresup, Me.lblUnidad.Text, _
                                           Me.cboPerDespachar.SelectedValue, False, Me.txtEspecificaciones.Text, Me.cboDetalleActividadPOA.SelectedValue)
            'ScriptManager1.RegisterStartupScript(Me, Me.GetType, "FaltanDatosCco", "alert('\'SubirArchivo(\'" & objLog.EncrytedString64(codigo_dpe) & "\',\'" & objLog.EncrytedString64(Me.txtPedido.Text) & "\')\'')", True)
            'If Me.fuCargarArchivo.PostedFile.FileName <> "" Then
            '    'ScriptManager1.RegisterStartupScript(Me,Me.GetType, "FaltanDatosCco", "SubirArchivo(\'" & objLog.EncrytedString64(codigo_dpe) & "\',\'" & objLog.EncrytedString64(Me.txtPedido.Text) & "\')", True)
            'ScriptManager1.RegisterStartupScript(Me, Me.GetType, "FaltanDatosCco", "alert('" & HdFile.value & "')", True)

            'End If


            If codigo_dpe <> 0 Then
                ScriptManager1.RegisterStartupScript(Me, Me.GetType, "FaltanDatosCco", "SubirArchivo('" & objLog.EncrytedString64(codigo_dpe) & "','" & objLog.EncrytedString64(Me.txtPedido.Text) & "')", True)

                'If fuCargarArchivo.PostedFile.FileName <> "" Then
                '    If Not SubirArchivo(codigo_dpe, CInt(Me.txtPedido.Text)) Then
                '        objLog.CancelarTransaccionCnx()
                '        ScriptManager1.RegisterStartupScript(Me,Me.GetType, "error", "alert('Ocurrió un error al adjuntar el archivo. Inténtelo nuevamente')", True)
                '    End If
                'End If
            End If
            'Fin Hcano 24-11-16

            objLog.CerrarTransaccionCnx()
            Me.lblDescripcionUnidad.Text = ""
            objLog = Nothing
            FinalizarIngresoDetalle()
            'Me.detPedNuevo.Text = 0
            pnlDistribuir.Visible = False
            ConsultarDatosPresupuesto()
            If txtPedido.Text <> "" Then
                cmdEnviar.Visible = True
                Me.cboCecos.Enabled = False
                lnkBusquedaAvanzada.Enabled = False
            End If
            Me.cboCecos.Enabled = True
            lnkBusquedaAvanzada.Enabled = True
            cboDetalleActividadPOA.Enabled = True
            Me.cboDetalleActividadPOA.SelectedValue = -1
            '
            Me.hdDisponible.Value = 0
            Me.gvDetallePedido.SelectedIndex = -1
            'Inhabilitar_Registro()
            Me.chkNoPresupuestado.Checked = False
            '
            Me.cboCecos.Enabled = False
            Me.lnkBusquedaAvanzada.Enabled = False
        Catch ex As Exception
            'objLog.CancelarTransaccionCnx()
            'Response.Write(ex.Message)
            ScriptManager1.RegisterStartupScript(Me, Me.GetType, "error", "alert('Ocurrió un error al procesar los datos: -" & ex.Message & "')", True)
        End Try
    End Sub

    Protected Sub Inhabilitar_Registro()
        Me.hdDisponible.Value = 0
        Me.txtConcepto.Enabled = False
        Me.txtConcepto.Text = ""
        Me.cboDetalleActividadPOA.Enabled = False
        Me.cboDetalleActividadPOA.SelectedValue = -1
        'Me.ImgBuscarItems.Visible = False
        Me.LinkButton3.Visible = False
        Me.txtComentarioReq.Enabled = False
        Me.txtComentarioReq.Text = ""
        Me.txtEspecificaciones.Enabled = False
        Me.txtEspecificaciones.Text = ""
        Me.txtPrecioUnit.Enabled = False
        Me.txtPrecioUnit.Text = ""
        Me.txtCantidad.Enabled = False
        Me.txtCantidad.Text = ""
        Me.cboPerDespachar.Enabled = False
        Me.TxtFechaEsperada.Enabled = False
        Me.TxtFechaEsperada.Text = ""
        Me.txtfile.Disabled = True
    End Sub

    Protected Sub Habilitar_Registro()
        Me.txtConcepto.Enabled = True
        Me.cboDetalleActividadPOA.Enabled = True
        'Me.ImgBuscarItems.Visible = True
        Me.LinkButton3.Visible = True
        Me.txtComentarioReq.Enabled = True
        Me.txtEspecificaciones.Enabled = True
        Me.txtPrecioUnit.Enabled = True
        Me.txtCantidad.Enabled = True
        Me.cboPerDespachar.Enabled = True
        Me.TxtFechaEsperada.Enabled = True
        Me.txtfile.Disabled = False
    End Sub

    Protected Function SubirArchivo(ByVal codigo_dep As Integer, ByVal codigo_ped As Integer) As Boolean
        'Dim nombre As String, temp As String
        'nombre = ""
        'Try
        '    If fuCargarArchivo.PostedFile.FileName <> "" And fuCargarArchivo.PostedFile.ContentLength < 4000000 Then
        '        nombre = fuCargarArchivo.PostedFile.FileName
        '        temp = "temp" & Now.ToString("ddMMyyyyHmmss.fff") & System.IO.Path.GetExtension(fuCargarArchivo.FileName)
        '        fuCargarArchivo.SaveAs(MapPath(temp))

        '        Dim Fecha As String = Format(Now(), "dd/MM/yyyy").ToString
        '        Dim objA As New ClsLogistica
        '        Dim objArc As New ClsArchivosCompartidos
        '        Dim list As New Dictionary(Of String, String)

        '        If ((System.IO.File.Exists(MapPath(temp)))) Then
        '            list.Add("Fecha", Fecha)
        '            list.Add("Extencion", System.IO.Path.GetExtension(nombre))
        '            list.Add("Nombre", nombre)
        '            list.Add("TransaccionId", codigo_dep.ToString)
        '            list.Add("TablaId", "2")
        '            list.Add("NroOperacion", codigo_ped.ToString)
        '            list.Add("Archivo", objA.ConvertFileToBase64(MapPath(temp)))
        '            list.Add("Usuario", Session("perlogin").ToString)
        '            list.Add("Equipo", "DSISTEMAS06")
        '            list.Add("Ip", "")
        '            list.Add("param8", Session("perlogin").ToString)

        '            Dim envelope As String = objArc.SoapEnvelope(list)
        '            Dim result As String = objArc.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Session("perlogin").ToString)

        '            If result.Contains("OK") Then
        '                Return True
        '            Else
        '                Return False
        '            End If
        '            System.IO.File.Delete(MapPath("") & "/ggg.txt")
        '            'System.IO.File.Delete(MapPath("ggg"))
        '        Else
        '            Return False
        '        End If
        '    Else
        '        Return False
        '    End If
        '    'If System.IO.File.Exists(MapPath("archivos/temp" & )) Then
        '    'fuCargarArchivo.SaveAs(MapPath("archivos/temp" & Now.ToString("ddMMyyyyHmmtt") & System.IO.Path.GetExtension(fuCargarArchivo.FileName)))
        '    'nombre = System.IO.Path.GetFileNameWithoutExtension(fuCargarArchivo.FileName) & "(1)" & System.IO.Path.GetExtension(fuCargarArchivo.FileName)

        '    'Else
        '    'fuCargarArchivo.SaveAs(MapPath("archivos/" & fuCargarArchivo.FileName.ToString))
        '    'nombre = fuCargarArchivo.FileName.ToString

        '    'End If

        '    'End If
        '    'Return True
        '    'Catch ex As Exception
        '    '    Throw ex
        '    'End Try
        '    'Try

        '    'If fuCargarArchivo.PostedFile.FileName <> "" And fuCargarArchivo.PostedFile.ContentLength < 4000000 Then
        '    '    Dim usuario_session_ As String() = Session("perlogin").ToString.Split(New Char() {"\"c})
        '    '    Dim usuario_session As String = usuario_session_(1)
        '    '    Dim Usuario As String = usuario_session

        '    '    Dim Fecha As String = Format(Now(), "dd/MM/yyyy").ToString
        '    '    Dim objA As New ClsLogistica
        '    '    Dim objArc As New ClsArchivosCompartidos
        '    '    Dim list As New Dictionary(Of String, String)

        '    '    list.Add("Fecha", Fecha)
        '    '    list.Add("Extencion", System.IO.Path.GetExtension(fuCargarArchivo.FileName))
        '    '    list.Add("Nombre", fuCargarArchivo.FileName)
        '    '    list.Add("TransaccionId", codigo_dep.ToString)
        '    '    list.Add("TablaId", "2")
        '    '    list.Add("NroOperacion", codigo_ped.ToString)
        '    '    list.Add("Archivo", objA.ConvertFileToBase64(fuCargarArchivo.FileName)
        '    '    list.Add("Usuario", Usuario)
        '    '    list.Add("Equipo", "DSISTEMAS06")
        '    '    list.Add("Ip", "10.10.1.5")
        '    '    list.Add("param8", Usuario)

        '    '    Dim envelope As String = objArc.SoapEnvelope(list)
        '    '    Dim result As String = objArc.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Session("perlogin").ToString)
        '    '    Response.Write(result)
        '    'End If
        'Catch ex As Exception
        '    Response.Write(ex.ToString)
        'End Try
    End Function

    Private Sub CargarDetalle()
        Dim objLog As New ClsLogistica
        If Me.cboPeriodoPresu.SelectedValue < 8 Then 'a partir del 2017
            gvDetallePedido.DataSource = objLog.ConsultarDetalle(Me.txtPedido.Text)
        Else
            gvDetallePedido.DataSource = objLog.ConsultarDetalle_V1(Me.txtPedido.Text) 'Hcano 25-11-16
        End If
        'gvDetallePedido.DataSource = objLog.ConsultarDetalle(Me.txtPedido.Text)
        gvDetallePedido.DataBind()
        objLog = Nothing
    End Sub


    Private Sub FinalizarIngresoDetalle()
        Dim objLog As New ClsLogistica
        Dim totalDetalle As Double
        totalDetalle = 0
        LimpiarDetalle()
        If Me.detPedNuevo.Text = 1 Then
            CargarDetalle()
        Else
            CargarDetalle()
        End If

        totalDetalle = objLog.ConsultarTotalDetalle(Me.txtPedido.Text)
        If totalDetalle = 0 Then
            Me.lblTitLista.Visible = False
            Me.lblTitTotal.Visible = False
            Me.lblTotalDetalle.Visible = False
            Me.lblTotalDetalle.Text = "0.00"

        Else
            Me.lblTitLista.Visible = True
            Me.lblTitTotal.Visible = True
            Me.lblTotalDetalle.Visible = True
            Me.lblTotalDetalle.Text = FormatNumber(totalDetalle, 2)
        End If
        limpiarDistribucion()
        pnlDistribuir.Visible = False
        objLog = Nothing
    End Sub
    Private Sub EliminarItemDetalle(ByVal codigo_dpe As Integer)
        Dim objLog As New ClsLogistica
        objLog.ElminarItemDetalle(codigo_dpe)
        objLog = Nothing
        CargarDetalle()
        objLog = Nothing
        FinalizarIngresoDetalle()
    End Sub
    Private Sub EliminarDistribucionItemDetalle(ByVal codigo_ecc As Integer)
        Dim objLog As New ClsLogistica
        objLog.EliminarDistribucionItemDetalle(codigo_ecc)
        objLog = Nothing
        ConsultarDistribucion()
    End Sub

    Private Sub LimpiarDetalle()
        'Me.cboProgramaPresu.SelectedIndex = -1
        txtConcepto.Text = ""
        txtCodItem.Text = ""
        txtPrecioUnit.Text = ""
        txtCantidad.Text = ""
        txtComentarioReq.Text = ""
        txtDetPresup.Text = ""
        txtEspecificaciones.Text = ""
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click, cmdCerrar.Click
        hddForzar.Value = 0
        ScriptManager1.RegisterStartupScript(Me, Me.GetType, "Existente", "javascript:divConfirmar.style.visibility='hidden'", True)
    End Sub

    Protected Sub cmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        hddForzar.Value = 1
        cmdGuardar_Click(sender, e)
    End Sub

    Protected Sub rblMovimiento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblMovimiento.SelectedIndexChanged
        txtConcepto.Text = ""
        txtCodItem.Text = ""
        txtPrecioUnit.Text = ""
        txtComentarioReq.Text = ""
        txtEspecificaciones.Text = ""

    End Sub

    Protected Sub lnkBusquedaAvanzada_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBusquedaAvanzada.Click
        If lnkBusquedaAvanzada.Text.Trim = "Busqueda Simple" Then
            MostrarBusquedaCeCos(False)
            lnkBusquedaAvanzada.Text = "Busqueda Avanzada"
            Me.gvCecos.Visible = False
        Else
            MostrarBusquedaCeCos(True)
            lnkBusquedaAvanzada.Text = "Busqueda Simple"
        End If
    End Sub

    Private Sub MostrarBusquedaCeCos(ByVal valor As Boolean)
        Me.txtBuscaCecos.Visible = valor
        'Me.ImgBuscarCecos.Visible = valor
        Me.LinkButton2.Visible = valor
        Me.lblTextBusqueda.Visible = valor
        Me.cboCecos.Visible = Not (valor)
        Panel3.Visible = (valor)
    End Sub

    'Protected Sub ImgBuscarCecos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarCecos.Click
    '    'BuscarCeCos()
    '    'Me.gvCecos.Visible = True
    'End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        BuscarCeCos()
        Me.gvCecos.Visible = True
    End Sub


    Private Sub BuscarCeCos()
        Try
            Dim objPre As ClsPresupuesto
            objPre = New ClsPresupuesto

            If Me.cboPeriodoPresu.SelectedValue < 8 Then ' Del 2017 en adelante
                gvCecos.DataSource = objPre.ConsultaCentroCostosConPermisos(Request.QueryString("ctf"), Request.QueryString("id"), txtBuscaCecos.Text)
            Else
                'Hcano 16-11-16
                gvCecos.DataSource = objPre.ObtenerListaCentroCostos_POA(Request.QueryString("id"), Request.QueryString("ctf"), Me.cboPeriodoPresu.SelectedValue, txtBuscaCecos.Text)
            End If

            gvCecos.DataBind()
            objPre = Nothing
            Panel3.Visible = True
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub

    Protected Sub gvCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCecos.SelectedIndexChanged
        Try
            cboCecos.SelectedValue = Me.gvCecos.DataKeys.Item(Me.gvCecos.SelectedIndex).Values("codigo_cco")
            ' ''----- agregar detalle Presupuesto a la grilla de items presupuestados
            Dim DatosIng, datosEgr As New Data.DataTable
            Dim Cod_cc As Integer
            Cod_cc = Me.gvCecos.DataKeys.Item(Me.gvCecos.SelectedIndex).Values("codigo_cco")
            Dim objcon As ClsLogistica
            objcon = New ClsLogistica
            gvPresupuesto.DataSource = ""
            'datosEgr = objcon.ConsultarDetallePresupuesto1(Me.cboPeriodoPresu.SelectedValue, Cod_cc, "E", Me.cboAlmacen.SelectedValue)
            If Me.cboPeriodoPresu.SelectedValue < 8 Then
                datosEgr = objcon.ConsultarDetallePresupuesto2(Me.cboPeriodoPresu.SelectedValue, Cod_cc, "E", Me.cboAlmacen.SelectedValue) 'dflores 11/08/16
            Else
                datosEgr = objcon.ConsultarDetallePresupuesto_POA(Me.cboPeriodoPresu.SelectedValue, cboCecos.SelectedValue, Me.cboAlmacen.SelectedValue) 'HCano 16/11/16
            End If

            gvPresupuesto.DataSource = datosEgr
            gvPresupuesto.DataBind()
            If datosEgr.Rows.Count = 0 Then
                pnlListaPresup.Visible = False
                pnlPresupuesto.Visible = False
            Else
                pnlListaPresup.Visible = True
                pnlPresupuesto.Visible = True
            End If
            '----------------------------------------
            datosEgr = Nothing
            MostrarBusquedaCeCos(False)
            Panel3.Visible = False
            lnkBusquedaAvanzada.Text = "Búsqueda Avanzada"
            '
            CargaActividades()
            Me.chkNoPresupuestado.Visible = True
            Me.chkNoPresupuestado.Checked = False
            Inhabilitar_Registro()
            'Me.gvCecos.Visible = False
            '
            Me.gvCecos.Visible = False
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub

    Protected Sub gvDetallePedido_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetallePedido.RowDataBound
        Dim objL As New ClsLogistica
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(6).Attributes.Add("OnClick", "javascript:ModalAdjuntar2('" & objL.EncrytedString64(DataBinder.Eval(e.Row.DataItem, "codigo_dpe").ToString()) & "','" & objL.EncrytedString64(Me.txtPedido.Text.ToString()) & "')")
        End If
    End Sub

    Protected Sub gvDetallePedido_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDetallePedido.RowDeleting
        Dim idCecoDetalle As Integer
        'crerado el 20.01.14 por fcastillo para validar aprobacion de pedido (opción eliminar del gvdetallepedido)
        EliminarItemDetalle(Me.gvDetallePedido.DataKeys.Item(e.RowIndex).Values(0))
        If gvDetallePedido.Rows.Count > 0 Then
            idCecoDetalle = Me.gvDetallePedido.DataKeys.Item(0).Item(3)
            consultarResponsableAprobacionDetalle(idCecoDetalle)
            modificarAprobacionDirectorInmediato(idCecoDetalle, CInt(Me.txtPedido.Text))
        Else
            Me.txtAprobacionDirInmed.Text = ""
            Me.txtAreaAprobInmed.Text = ""
        End If

    End Sub

    Protected Sub gvDetallePedido_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDetallePedido.SelectedIndexChanged
        'ConsultarDistribucion()
    End Sub

    Private Sub ConsultarDistribucion()
        Try
            Dim objLog As New ClsLogistica
            Dim objpre As New ClsPresupuesto
            Dim objfun As New ClsFunciones
            Dim datos As New Data.DataTable
            datos = objpre.ObtenerListaCentroCostos("1", 523, Request.QueryString("id"))
            objfun.CargarListas(cboCecosEjecucion, datos, "codigo_Cco", "descripcion_Cco", ">> Seleccione<<")
            datos.Dispose()
            limpiarDistribucion()
            txtCodigoDetalle.Text = gvDetallePedido.DataKeys.Item(gvDetallePedido.SelectedIndex).Values(0)
            lblNombreItem.Text = HttpUtility.HtmlDecode(gvDetallePedido.SelectedRow.Cells(0).Text)
            Me.gvDistribucion.DataSource = objLog.ConsultarDetalleItem(gvDetallePedido.DataKeys.Item(gvDetallePedido.SelectedIndex).Values(0))
            Me.gvDistribucion.DataBind()
            pnlDistribuir.Visible = True
            If gvDetallePedido.DataKeys.Item(gvDetallePedido.SelectedIndex).Values("modoDistribucion_Dpe") = "P" Then
                lblModoDistribucion.Text = "Porcentaje(%)"
                lblTotalItem.Text = "100.00%"
                lblDistribuidoItem.Text = FormatNumber(sumarDistribucion(), 2) & "%"
                lblPorDistribuir.Text = FormatNumber(100 - sumarDistribucion(), 2) & "%"
            Else
                lblModoDistribucion.Text = "Cantidad"
                'lblTotalItem.Text = HttpUtility.HtmlDecode(gvDetallePedido.SelectedRow.Cells(3).Text)
                lblTotalItem.Text = HttpUtility.HtmlDecode(gvDetallePedido.SelectedRow.Cells(4).Text)
                lblDistribuidoItem.Text = FormatNumber(sumarDistribucion(), 2)
                lblPorDistribuir.Text = FormatNumber(lblTotalItem.Text - sumarDistribucion(), 2)
            End If
            objLog = Nothing
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub

    Protected Sub gvDistribucion_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDistribucion.RowDeleting
        EliminarDistribucionItemDetalle(Me.gvDistribucion.DataKeys.Item(e.RowIndex).Values(0))
    End Sub

    Protected Sub gvDistribucion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDistribucion.SelectedIndexChanged
        Me.txtCantidadDistribucion.Text = HttpUtility.HtmlDecode(gvDistribucion.SelectedRow.Cells(1).Text)
        Me.cboCecosEjecucion.SelectedValue = gvDistribucion.DataKeys(gvDistribucion.SelectedIndex).Values(1)
        Me.txtCodigo_Ecc.Text = gvDistribucion.DataKeys(gvDistribucion.SelectedIndex).Values(0)
    End Sub


    Protected Sub cmdDistribuir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDistribuir.Click
        Dim objLog As New ClsLogistica
        Dim rpta As String
        Dim codigo_ecc As Integer
        If Me.txtCodigo_Ecc.Text = "" Then
            codigo_ecc = -1
        Else
            codigo_ecc = Me.txtCodigo_Ecc.Text
        End If
        objLog.AbrirTransaccionCnx()
        rpta = objLog.ActualizarDistribucionItem(codigo_ecc, Me.cboCecosEjecucion.SelectedValue, Me.txtCantidadDistribucion.Text, txtCodigoDetalle.Text)
        objLog.CerrarTransaccionCnx()
        If rpta <> "OK" Then
            ScriptManager1.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('" & rpta & "');", True)
        End If
        ConsultarDistribucion()
    End Sub
    'treyes 27/06/2016 Se modifica obtencion de saldo disponible de acuerdo a presupuesto
    Private Sub ConsultarPresupuesto()
        Dim objcon As ClsLogistica
        objcon = New ClsLogistica
        Dim DatosIng, datosEgr As New Data.DataTable
        'datosEgr = objcon.ConsultarDetallePresupuesto1(Me.cboPeriodoPresu.SelectedValue, cboCecos.SelectedValue, "E", Me.cboAlmacen.SelectedValue)
        If Me.cboPeriodoPresu.SelectedValue < 8 Then ' del 2017 en adelante
            datosEgr = objcon.ConsultarDetallePresupuesto2(Me.cboPeriodoPresu.SelectedValue, cboCecos.SelectedValue, "E", Me.cboAlmacen.SelectedValue) 'treyes
        Else
            'Hcano 22-11-16
            datosEgr = objcon.ConsultarDetallePresupuesto_POA(Me.cboPeriodoPresu.SelectedValue, cboCecos.SelectedValue, Me.cboAlmacen.SelectedValue)
        End If

        gvPresupuesto.DataSource = datosEgr
        gvPresupuesto.DataBind()
        If datosEgr.Rows.Count = 0 Then
            pnlListaPresup.Visible = False
            pnlPresupuesto.Visible = False
        Else
            pnlListaPresup.Visible = True
            pnlPresupuesto.Visible = True
        End If

    End Sub

    Protected Sub gvPresupuesto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPresupuesto.SelectedIndexChanged
        'Comenta Hcano 22-11-16
        ''Aquí se asignan los valores cuando se selecciona un Item PRESUPUESTADO
        'If gvPresupuesto.SelectedRow.Cells(5).Text > 0 Then
        '    LimpiarDetalle()
        '    txtDetPresup.Text = Me.gvPresupuesto.DataKeys.Item(Me.gvPresupuesto.SelectedIndex).Values(0)
        '    cboProgramaPresu.SelectedValue = gvPresupuesto.DataKeys.Item(gvPresupuesto.SelectedIndex).Values(1)
        '    txtCodItem.Text = Me.gvPresupuesto.DataKeys.Item(Me.gvPresupuesto.SelectedIndex).Values(2)
        '    Me.lblUnidad.Text = Me.gvPresupuesto.DataKeys.Item(Me.gvPresupuesto.SelectedIndex).Values(3)

        '    Me.txtConcepto.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(1).Text)
        '    Me.txtComentarioReq.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(4).Text)
        '    Me.lblDescripcionUnidad.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(3).Text)
        '    Me.txtPrecioUnit.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(5).Text)
        '    'Me.txtCantidad.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(5).Text)
        '    Me.hdDisponible.Value = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(7).Text)
        'End If

        'Hcano 22-11-16 : Se reemplaza programa presupuestal por Actividad POA
        Try
            If gvPresupuesto.SelectedRow.Cells(5).Text > 0 Then
                Me.chkNoPresupuestado.Checked = False
                LimpiarDetalle()
                txtDetPresup.Text = Me.gvPresupuesto.DataKeys.Item(Me.gvPresupuesto.SelectedIndex).Values(0)
                'cboProgramaPresu.SelectedValue = gvPresupuesto.DataKeys.Item(gvPresupuesto.SelectedIndex).Values(1)
                txtCodItem.Text = Me.gvPresupuesto.DataKeys.Item(Me.gvPresupuesto.SelectedIndex).Values(2)
                Me.lblUnidad.Text = Me.gvPresupuesto.DataKeys.Item(Me.gvPresupuesto.SelectedIndex).Values(3)
                cboDetalleActividadPOA.SelectedValue = gvPresupuesto.DataKeys.Item(gvPresupuesto.SelectedIndex).Values(4)
                cboDetalleActividadPOA.Enabled = False
                Me.TxtFechaEsperada.Text = gvPresupuesto.DataKeys.Item(gvPresupuesto.SelectedIndex).Values(5)

                Me.txtConcepto.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(1).Text)
                Me.txtComentarioReq.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(4).Text)
                Me.lblDescripcionUnidad.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(3).Text)
                Me.txtPrecioUnit.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(5).Text)
                'Me.txtCantidad.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(5).Text)
                Me.hdDisponible.Value = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(7).Text)
                If Me.hdDisponible.Value = 0 Then
                    Me.chkNoPresupuestado.Checked = True
                End If
                Habilitar_Registro()
                'Me.ImgBuscarItems.Visible = False
                'Me.LinkButton3.Visible = False
                Me.txtConcepto.Enabled = False
                Me.cboDetalleActividadPOA.Enabled = False
            End If
        Catch ex As Exception
            'ScriptManager1.RegisterStartupScript(Me, Me.GetType, "AlertGridPres", "alert('" & ex.Message & "');", True)
            Page.RegisterStartupScript("appMenu", "<script>AvisoAlerta('" & ex.Message & "');</script>")
        End Try        
        ' Fin Hcano
    End Sub

    Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCecos.SelectedIndexChanged
        ConsultarDatosPresupuesto()
        CargaActividades()
        Me.cboDetalleActividadPOA.Enabled = False
        Me.TxtFechaEsperada.Text = FormatDateTime(Now, DateFormat.ShortDate)
        Me.chkNoPresupuestado.Visible = True
        Me.chkNoPresupuestado.Checked = False
        Inhabilitar_Registro()
    End Sub
    Private Sub CargaActividades()
        Dim objfun As New ClsFunciones
        Dim datos As New Data.DataTable
        Dim objlogi As New ClsLogistica
        datos = objlogi.ObtenerListaActividades_POA(Me.cboPeriodoPresu.SelectedValue, Me.cboCecos.SelectedValue)
        objfun.CargarListas(cboDetalleActividadPOA, datos, "codigo", "descripcion", ">>Seleccione<<")
    End Sub
    Private Sub ConsultarDatosPresupuesto()
        Try
            Me.pnlPresupuesto.Visible = True
            ConsultarPresupuesto()
            LimpiarDetalle()
            Me.txtDetPresup.Text = ""
            cboDetalleActividadPOA.Enabled = True
            Panel1.Visible = False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub consultarResponsableAprobacion(ByVal valor As Integer)

        Dim dts As New Data.DataTable
        Dim objLog As New ClsLogistica

        If Me.txtAprobacionDirInmed.Text = "" Then
            ' creado por fcastillo 19.02.14 para validar y mostrar la instancia de aprobación inmediata durante el nuevo pedido

            dts = objLog.consultarResponsableCeco(valor)
            If dts.Rows.Count > 0 Then
                Me.txtAprobacionDirInmed.Text = dts.Rows(0).Item("Responsable")
                Me.txtAreaAprobInmed.Text = dts.Rows(0).Item("descripcion_Cco")
            End If

        End If

    End Sub
    Private Sub consultarResponsableAprobacionDetalle(ByVal valor As Integer)

        Dim dts As New Data.DataTable
        Dim objLog As New ClsLogistica
        ' creado por fcastillo 19.02.14 para validar y mostrar la instancia de aprobación inmediata durante el nuevo pedido

        dts = objLog.consultarResponsableCeco(valor)
        If dts.Rows.Count > 0 Then
            Me.txtAprobacionDirInmed.Text = dts.Rows(0).Item("Responsable")
            Me.txtAreaAprobInmed.Text = dts.Rows(0).Item("descripcion_Cco")
        End If
    End Sub

    Private Sub modificarAprobacionDirectorInmediato(ByVal ceco As Integer, ByVal pedido As Integer)

        Dim dts As New Data.DataTable
        Dim objLog As New ClsLogistica
        ' creado por fcastillo 19.02.14 para validar y mostrar la instancia de aprobación inmediata durante el nuevo pedido

        dts = objLog.consultarResponsableNuevoCeco(ceco, pedido)
        If dts.Rows.Count > 0 Then
            Me.txtAprobacionDirInmed.Text = dts.Rows(0).Item("Responsable")
            Me.txtAreaAprobInmed.Text = dts.Rows(0).Item("descripcion_Cco")

        End If

    End Sub

    Protected Sub cmdEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnviar.Click

        If gvDetallePedido.Rows.Count > 0 Then
            If Me.txtPedido.Text = "" Then
                ScriptManager1.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('Se envió el pedido satisfactoriamente');", True)
                Exit Sub
            End If
            If (Me.cboAlmacen.SelectedValue <> 4) Then
                Dim log As New ClsLogistica
                log.CalificarPedido(txtPedido.Text, Request.QueryString("id"), "C", "")
            End If
            ScriptManager1.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('Se envió el pedido satisfactoriamente');", True)
            ScriptManager1.RegisterStartupScript(Me, Me.GetType, "siguientepagina", "location.href='frmNuevoPedido_V1.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "';", True)
        Else
            ScriptManager1.RegisterStartupScript(Me, Me.GetType, "Alerta", "alert('El detalle del pedido no cuenta con Items Registrados');", True)
        End If
        'Me.detPedNuevo.Text = 0
    End Sub

    Protected Sub chkNoPresupuestado_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNoPresupuestado.CheckedChanged
        If Me.chkNoPresupuestado.Checked = True Then
            Me.Inhabilitar_Registro()
            Me.Habilitar_Registro()
        Else
            Me.Inhabilitar_Registro()
        End If
    End Sub
End Class

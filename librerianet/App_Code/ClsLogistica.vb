Imports System.Data
Imports System.IO

Public Class ClsLogistica
    Private cnx As New ClsConectarDatos
    Public Function ConsultarAlmacen(ByVal codigo_ped As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_consultarAlmacenxPedido", codigo_ped)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function AgregarPedido(ByVal codigo_per As Int32, ByVal codigo_cco As Int32, _
                                  ByVal estado_ped As Integer, ByVal importe_ped As Double, _
                                  ByVal fecha_Ped As String, ByVal observacion_Ped As String, ByVal EjercicioPresupuestal As Integer) As Int16
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer

            cnx.Ejecutar("LOG_RegistrarPedido", codigo_per, codigo_cco, estado_ped, importe_ped, fecha_Ped, observacion_Ped, EjercicioPresupuestal, 0).copyto(valoresdevueltos, 0)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 0
        End Try
    End Function
    Public Sub AgregarDetallePedido(ByVal codigo_ped As Integer, ByVal codigo_Art As Integer, ByVal codigo_cco As Integer, _
                                    ByVal precioReferencial As Double, ByVal cantidad As Double, ByVal observacion As String, _
                                    ByVal fechaEsperada_dpe As String, ByVal estado As Integer, ByVal tipo As String, ByVal modalidad As String, ByVal codigo_ppr As Integer, ByVal codigo_dpr As Integer, ByVal especificacion As String)

        cnx.Ejecutar("LOG_RegistrarDetallePedido", codigo_ped, codigo_Art, codigo_cco, precioReferencial, cantidad, observacion, fechaEsperada_dpe, estado, tipo, modalidad, codigo_ppr, codigo_dpr, especificacion)

    End Sub
    'Hcano
    Public Function AgregarDetallePedido_VPOA(ByVal codigo_ped As Integer, ByVal codigo_Art As Integer, ByVal codigo_cco As Integer, _
                                ByVal precioReferencial As Double, ByVal cantidad As Double, ByVal observacion As String, _
                                ByVal fechaEsperada_dpe As String, ByVal estado As Integer, ByVal tipo As String, ByVal modalidad As String, ByVal codigo_ppr As Integer, ByVal codigo_dpr As Integer, ByVal especificacion As String, ByVal codigo_dap As Integer) As Integer
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer

            cnx.Ejecutar("LOG_RegistrarDetallePedido_VPOA", codigo_ped, codigo_Art, codigo_cco, precioReferencial, cantidad, observacion, fechaEsperada_dpe, estado, tipo, modalidad, codigo_ppr, codigo_dpr, especificacion, codigo_dap).copyto(valoresdevueltos, 0)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    'Fin Hcano
    Public Function AgregarSubastaInversa(ByVal idPro As Integer, ByVal codCategoria As Integer, ByVal fecInicio As Date, ByVal fecFin As Date, ByVal descripcion As String, ByVal codigo_Per As String, ByVal precioBase As Boolean, ByVal mejorOferta As Boolean) As Int16
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer

            cnx.Ejecutar("LOG_RegistrarSubastaInversa", idPro, codCategoria, fecInicio, fecFin, descripcion, codigo_Per, 0, precioBase, mejorOferta).copyto(valoresdevueltos, 0)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 0
        End Try
    End Function
    Public Function ActualizarUsuarioProveedor(ByVal idPro As Integer) As String
        Try
            Dim rpta As String = ""
            Dim valoresdevueltos(1) As String
            cnx.Ejecutar("LOG_ActualizaUsuarioProveedor", idPro, rpta).copyto(valoresdevueltos, 0)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Sub ActualizarSubastaDocumento(ByVal codSubasta As Integer, ByVal idPro As Integer, ByVal rutafisica As String, ByVal estado As String)

        cnx.Ejecutar("LOG_ActualizarSubastaInversaDocumentos", codSubasta, idPro, rutafisica, estado)

    End Sub
    Public Sub AgregarProveedoresSubasta(ByVal codSubasta As Integer, ByVal idPro As Integer, ByVal participa As Boolean)

        cnx.Ejecutar("LOG_RegistrarProveedoresSubasta", codSubasta, idPro, participa)

    End Sub
    Public Sub ActualizarTablasSubastaInversa(ByVal codSubasta As Integer, ByVal fecInicio As Date, ByVal fecFin As Date, ByVal precioBase As Boolean, ByVal mejorOferta As Boolean, ByVal codCategoria As Integer, ByVal descripcion As String)

        cnx.Ejecutar("LOG_ActualizarTablasSubastaInversa", codSubasta, fecInicio, fecFin, precioBase, mejorOferta, codCategoria, descripcion)

    End Sub
    Public Sub AgregarPedidosSubasta(ByVal codSubasta As Integer, ByVal codigo_Ped As Integer, ByVal idArt As Integer)

        cnx.Ejecutar("LOG_RegistrarPedidosSubasta", codSubasta, codigo_Ped, idArt)

    End Sub
    Public Sub AgregarArticulosSubasta(ByVal codSubasta As Integer, ByVal idArt As Integer, ByVal cantidad As Double, ByVal total As Double, ByVal precioBase As Double, ByVal observacion As String, ByVal documento As String)

        cnx.Ejecutar("LOG_RegistrarArticulosSubasta", codSubasta, idArt, cantidad, total, precioBase, observacion, documento)

    End Sub
    Public Sub ActualizaOfertaProveedorSubasta(ByVal codSubasta As Integer, ByVal idArt As Integer, ByVal idPro As Integer, ByVal monto As Double)

        cnx.Ejecutar("LOG_ActualizaOfertaProveedorSubasta", codSubasta, idArt, idPro, monto)

    End Sub
    Public Sub AgregarNegociacionSubasta(ByVal codSubasta As Integer, ByVal idPro As Integer, ByVal comentario As String, ByVal asunto As String, ByVal codigoPer As Integer, ByVal registra As String)

        cnx.Ejecutar("LOG_RegistrarSubastaNegociacion", codSubasta, idPro, comentario, asunto, codigoPer, registra)

    End Sub
    Public Sub AgregarDocumentosSubasta(ByVal codSubasta As Integer, ByVal idPro As Integer, ByVal nombre As String, ByVal archivo As String, ByVal rutafisica As String)

        cnx.Ejecutar("LOG_RegistrarSubastaInversaDocumentos", codSubasta, idPro, nombre, archivo, rutafisica)

    End Sub
    Public Sub ActualizarSubastaInversa(ByVal codSubasta As Integer)

        cnx.Ejecutar("LOG_AprobarSubastaInversa", codSubasta)

    End Sub
    Public Sub AgregarCategoriaProveedor(ByVal idPro As Integer, ByVal codCategoria As Integer)

        cnx.Ejecutar("LOG_AgregarCategoriaProveedor", idPro,codCategoria)

    End Sub
    Public Sub AgregarSubastaProveedorOferta(ByVal codSubasta As Integer, ByVal idArt As Integer, ByVal idPro As Integer, ByVal cantidad As Double, ByVal precioBase As Double, ByVal monto As Double, ByVal observacion As String, ByVal documento As String)

        cnx.Ejecutar("LOG_AgregarSubastaProveedorOferta", codSubasta, idArt, idPro, cantidad, precioBase, monto, observacion, documento)

    End Sub
    Public Sub ActualizarProveedor(ByVal idPro As Integer, ByVal direccionPro As String, ByVal telefonoPro As String, ByVal faxPro As String, ByVal emailPro As String, ByVal ranking As Integer, ByVal passwordPro As String, ByVal participa As Boolean)

        'cnx.Ejecutar("LOG_ActualizarProveedor", idPro, direccionPro, telefonoPro, faxPro, emailPro, ranking, passwordPro, participa, observacion)
        cnx.Ejecutar("LOG_ActualizarProveedor", idPro, direccionPro, telefonoPro, faxPro, emailPro, ranking, passwordPro, participa)
    End Sub
    Public Sub ActualizarArticulo(ByVal idArt As Integer, ByVal descripcionArt As String, ByVal descripcionResArt As String, ByVal iduni As Integer, ByVal idcls As Integer, ByVal idRub As Integer, ByVal codCategoria As Integer)

        cnx.Ejecutar("LOG_ActualizarArticulo", idArt, descripcionArt, descripcionResArt, iduni, idcls, idRub, codCategoria)

    End Sub
    Public Sub ActualizarCategoriaArticuloProveedor(ByVal codCategoria As Integer, ByVal desCategoria As String, ByVal estado As String)

        cnx.Ejecutar("LOG_ActualizarCategoriaArticuloProveedor", codCategoria, desCategoria, estado)

    End Sub
    Public Sub AgregarCategoriaArticuloProveedor(ByVal desCategoria As String, ByVal estado As String)

        cnx.Ejecutar("LOG_AgregarCategoriaArticuloProveedor", desCategoria, estado)

    End Sub
    Public Sub ActualizarSubastaInversa(ByVal codSubasta As Integer, ByVal idPro As Integer)

        cnx.Ejecutar("LOG_ActualizarSubastaInversa", codSubasta, idPro)

    End Sub
    Public Sub AbrirTransaccionCnx()
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.IniciarTransaccion()
    End Sub

    Public Sub CerrarTransaccionCnx()
        cnx.TerminarTransaccion()
    End Sub

    Public Sub CancelarTransaccionCnx()
        cnx.AbortarTransaccion()
    End Sub
    Public Function ConsultarTotalDetalle(ByVal codigo_ped As Integer) As Double
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarTotalDetallePedido", codigo_ped)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item(0).ToString
    End Function

    Public Function actualizarCabeceraPedido(ByVal codigo_ped As Integer, ByVal importe_ped As Decimal) As Double
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ActualizarCabeceraPedido", codigo_ped, importe_ped)
        cnx.CerrarConexion()
        'Return dts.Rows(0).Item(0).ToString
    End Function
    Public Function ConsultarCategoriaArticuloProveedor(ByVal codCategoria As Integer, ByVal desCategoria As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultaCategoriaArticuloProveedor", codCategoria, desCategoria)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarArticulo(ByVal idArt As Integer, ByVal descripcionArt As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultaArticulo", idArt, descripcionArt)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarRubro(ByVal idRub As Integer, ByVal descripcionRub As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultaRubro", idRub, descripcionRub)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarClase(ByVal idcls As Integer, ByVal descripcioncls As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultaClase", idcls, descripcioncls)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarUnidad(ByVal iduni As Integer, ByVal descripcionuni As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultaUnidad", iduni, descripcionuni)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarDetalle(ByVal codigo_ped As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarDetallePedido", codigo_ped) 'lll inicialmente no ejecutado
        cnx.CerrarConexion()
        Return dts
    End Function
    '--Hcano
    Public Function ConsultarDetalle_V1(ByVal codigo_ped As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarDetallePedido_v1", codigo_ped) 'lll inicialmente no ejecutado
        cnx.CerrarConexion()
        Return dts
    End Function
    '--Fin Hcano
    'Public Function ConsultarDetallePedidoNuevo(ByVal codigo_ped As Integer) As Data.DataTable
    '    Dim dts As New Data.DataTable
    '    cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    cnx.AbrirConexion()
    '    dts = cnx.TraerDataTable("LOG_ConsultarDetallePedidoNuevo", codigo_ped) 'fcastillo 04.03.14
    '    cnx.CerrarConexion()
    '    Return dts
    'End Function
    Public Function BuscarOrden(ByVal codigo_ecc As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_BuscarOrdenCS", "EC", "", "", "", codigo_ecc)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarDetalleEjecutado(ByVal codigo_ped As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarDetallePedidoEjecutado", codigo_ped)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarDetalleItem(ByVal codigo_dpe As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarDistribucionPedido", codigo_dpe)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Sub ActualizaCategoriaArticulo(ByVal codCategoria As Integer, ByVal idArt As Integer)
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("LOG_ActualizaCategoriaArticulo", codCategoria, idArt)
        cnx.CerrarConexion()
    End Sub

    Public Function ElminarItemDetalle(ByVal codigo_dpe As Integer) As Integer
        Dim rpta As Integer = 0
        Dim valoresdevueltos(1) As Integer
       
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("LOG_ConsultarDetallePedidoParaEliminacion", codigo_dpe, DBNull.Value).copyto(valoresdevueltos, 0)
        rpta = valoresdevueltos(0)

        If rpta = 1 Then
            cnx.Ejecutar("LOG_EliminarDetallePedido", codigo_dpe)
        End If
        cnx.CerrarConexion()
        Return rpta
    End Function

    Public Sub EliminarDistribucionItemDetalle(ByVal codigo_ecc As Integer)
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("LOG_EliminarDistribucionItem", codigo_ecc)
        cnx.CerrarConexion()
    End Sub

    Public Sub EliminarDocumentoSubasta(ByVal codSubasta As Integer, ByVal idPro As Integer, ByVal ruta As String)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("LOG_EliminarSubastaInversaDocumentos", codSubasta, idPro, ruta)
        cnx.CerrarConexion()
    End Sub

    Public Sub EliminarProveedorCategoria(ByVal idPro As Integer, ByVal codCategoria As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("LOG_EliminarProveedorCategoria", idPro, codCategoria)
        cnx.CerrarConexion()
    End Sub

    Public Sub ElmininarPedido(ByVal codigo_ecc As Integer, Optional ByVal usuario As String = "")
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("LOG_EliminarPedido", codigo_ecc, usuario)
        cnx.CerrarConexion()
    End Sub

    Public Function ConsultarEstados(ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarEstados", tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarCategoria() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarCategoria")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarArticulosComprar(ByVal codCategoria As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarPedidosComprar", codCategoria)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarMensajes(ByVal codSubasta As Integer, ByVal idPro As Integer, ByVal codigo_Per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarSubastaNegociacion", codSubasta, idPro, codigo_Per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarSusbastaInversa(ByVal codSubasta As Integer, ByVal codCategoria As Integer, ByVal fecInicio As String, ByVal fecFin As String, ByVal idPro As Integer, ByVal estado As String, ByVal todos As Boolean) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsutaSubastaInversa", 0, codCategoria, fecInicio, fecFin, idPro, estado, todos)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarSusbastaProveedor(ByVal codSubasta As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsutaSubastaProveedor", codSubasta)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarSusbastaArticuloCantidad(ByVal codSubasta As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsutaSubastaArticuloCantidad", codSubasta)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarProveedorSubasta(ByVal codCategoria As Integer, ByVal codSubasta As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarProveedorSubasta", codCategoria, codSubasta)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarSubastaPedidoComprar(ByVal codCategoria As Integer, ByVal codSubasta As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarSubastaPedidosComprar", codCategoria, codSubasta)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarCategoriaProveedor(ByVal idPro As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarCategoriaProveedor", idPro)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarProveedor(ByVal idPro As Integer, ByVal nombrePro As String, ByVal rucPro As String, ByVal ranking As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsutaProveedor", idPro, nombrePro, rucPro, ranking)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarSusbastaProveedorOferta(ByVal codSubasta As Integer, ByVal idPro As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsutaSubastaProveedorOferta", codSubasta, idPro)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarSusbastaOfertaResumen(ByVal codSubasta As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsutaSubastaOfertaResumen", codSubasta)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarSusbastaPedido(ByVal codSubasta As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsutaSubastaPedido", codSubasta)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarSusbastaDocumento(ByVal codSubasta As Integer, ByVal idPro As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarSubastaDocumentos", codSubasta, idPro)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarProveedorCategoria(ByVal codCategoria As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarProveedorCategoria", codCategoria)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarDistribucionItem(ByVal codigo_ecc As Integer, ByVal codigo_cco As Integer, ByVal cantidad As Double, ByVal codigo_dpe As Integer) As String
        Try
            Dim rpta As String

            Dim valoresdevueltos(1) As String
            cnx.Ejecutar("LOG_ActualizarDistribucionItem", codigo_ecc, codigo_cco, cantidad, codigo_dpe, "").copyto(valoresdevueltos, 0)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 0
        End Try
    End Function
    Public Function ConsultarDetallePresupuesto(ByVal codigo_pct As Int64, ByVal codigo_cco As Int64, ByVal tipo As String) As Data.DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("LOG_RepConsultarPresupuestoPorCentroCosto", codigo_pct, codigo_cco, tipo)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function ConsultarDetallePresupuesto1(ByVal codigo_pct As Int64, ByVal codigo_cco As Int64, ByVal tipo As String, ByVal codigo_Alm As Integer) As Data.DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("LOG_RepConsultarPresupuestoPorCentroCosto1", codigo_pct, codigo_cco, tipo, codigo_Alm)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'treyes 27/06/2016 Se modifica cálculo de saldo disponible
    Public Function ConsultarDetallePresupuesto2(ByVal codigo_pct As Int64, ByVal codigo_cco As Int64, ByVal tipo As String, ByVal codigo_Alm As Integer) As Data.DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("LOG_RepConsultarPresupuestoPorCentroCosto1_V2", codigo_pct, codigo_cco, tipo, codigo_Alm)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarDetallePresupuesto_POA(ByVal codigo_ejp As Int64, ByVal codigo_cco As Int64, ByVal codigo_Alm As Integer) As Data.DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("LOG_RepConsultarPresupuestoPorCentroCosto1_POA", codigo_ejp, codigo_cco, codigo_Alm)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'Hcano 22-11-16
    Public Function ObtenerListaActividades_POA(ByVal codigo_ejp As Integer, ByVal codigo_cco As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            datos = ObjCnx.TraerDataTable("LOG_ListaDetalleActividadesPOA", codigo_ejp, codigo_cco)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ' fin hcano

    Public Sub calificarDetallePedido(ByVal codigo_ped As Integer, ByVal veredicto As String)
        'Try
        '----------------Envío mail --------------------------------------
        Dim dt As New Data.DataTable
        Dim dtEst As New Data.DataTable
        'Dim cnx As New ClsConectarDatos
        Dim calificar As Integer
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        If veredicto = "R" Then
            calificar = 13
        Else
            If veredicto = "C" Then
                calificar = 3
            Else
                calificar = 1
            End If
        End If
        cnx.AbrirConexion()
        cnx.Ejecutar("LOG_CalificarDetallePedido", codigo_ped, calificar)
        cnx.CerrarConexion()

        'Catch ex As Exception
        'cnx.AbortarTransaccion()
        'End Try

    End Sub
    Public Function ActivarDerivacion(ByVal codigo_ped As Integer) As String
        Try
            Dim cnx As New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim rpta As String
            Dim valoresdevueltos(1) As String
            cnx.AbrirConexion()
            cnx.Ejecutar("LOG_VerificarDerivacion", codigo_ped, "").copyto(valoresdevueltos, 0)
            cnx.CerrarConexion()
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.CerrarConexion()
            Return 0
        End Try
    End Function

    Public Sub DerivarPedido(ByVal codigo_ped As Integer, ByVal codigo_per As Integer, ByVal codigo_PerDerivador As Integer, _
                             ByVal observacion As String)
        Try
            Dim cnx As New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            cnx.AbrirConexion()
            cnx.Ejecutar("LOG_DerivarRevisor", codigo_ped, codigo_per, codigo_PerDerivador, observacion)
            cnx.CerrarConexion()
        Catch ex As Exception
            cnx.AbortarTransaccion()
        End Try
    End Sub
    Public Function ConsultarInstancias(ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarInstancias", tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarRevisiones(ByVal codigo_ped As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarEvaluacionPedido", codigo_ped)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarEstadosPedido(ByVal codigo_ped As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarEstadosPedido", codigo_ped)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarObservacionesPedido(ByVal codigo_ped As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarObservacionesPedido", codigo_ped)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarAtencionesPedido(ByVal codigo_ped As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarAtencionPedido", codigo_ped)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarPersonalDerivacion() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("spPla_ConsultarPersonal", "LO", 0)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarPersonaResponsableDeAprobacion() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("Log_ConsultarPersonalDerivarPedido")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarPersonalDerivacionCompras(ByVal tipoOrden As String, ByVal nivel As Int16) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarPersonalDerivacionCompras", tipoOrden, nivel)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Sub ConfirmarEntrega(ByVal idBol As Integer, ByVal usuario As String)
        Try
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            cnx.AbrirConexion()
            cnx.Ejecutar("LOG_ConfirmarEntregaAlmacen", idBol, usuario)
            cnx.CerrarConexion()
        Catch ex As Exception
            cnx.CerrarConexion()
        End Try
    End Sub

    Public Function ConsultarNivelInstancia(ByVal codigo_rco As Int64, ByVal codigo_per As Int64) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarNivelInstancia", codigo_per, codigo_rco)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarDetallePedidosOrden(ByVal codigo_rco As Int64) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarRelacionOrdenPedido", codigo_rco)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarOrdenObservadaPendiente(ByVal codigo_rcom As Int64) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("log_consultarordenobservadapendiente", codigo_rcom)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Sub DenegarItemDetalle(ByVal codigo_dpe As Integer)
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("LOG_DenegarDetallePedido", codigo_dpe)
        cnx.CerrarConexion()
    End Sub

    Public Function consultarCodigoRaizCeco(ByVal codigo_Ped As Integer) As DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.Ejecutar("LOG_ConsultarCodigoRaizPedido", codigo_Ped)
        Return dts
        cnx.CerrarConexion()
    End Function

    Public Sub ClonarPedido(ByVal codigo_ped As Integer)
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("LOG_ClonarPedido", codigo_ped)
        cnx.CerrarConexion()
    End Sub

    Public Function ConsultarItemDetalle(ByVal codigo_dpe As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarItemDetallePedido", codigo_dpe)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Sub ActualizarDetallePedido(ByVal codigo_ped As Integer, ByVal codigo_Art As Integer, ByVal codigo_cco As Integer, _
                                    ByVal precioReferencial As Double, ByVal cantidad As Double, ByVal observacion As String, _
                                    ByVal fechaEsperada_dpe As String, ByVal estado As Integer, ByVal tipo As String, ByVal modalidad As String, ByVal codigo_ppr As Integer, ByVal codigo_dpr As Integer, ByVal codigo_dpe As Integer, ByVal especificacion As String)

        cnx.Ejecutar("LOG_ActualizarDetallePedido", codigo_dpe, codigo_ped, codigo_Art, codigo_cco, precioReferencial, cantidad, observacion, fechaEsperada_dpe, estado, tipo, modalidad, codigo_ppr, codigo_dpr, especificacion)

    End Sub

    Public Sub ActualizarDetallePedido_VPOA(ByVal codigo_ped As Integer, ByVal codigo_Art As Integer, ByVal codigo_cco As Integer, _
                                ByVal precioReferencial As Double, ByVal cantidad As Double, ByVal observacion As String, _
                                ByVal fechaEsperada_dpe As String, ByVal estado As Integer, ByVal tipo As String, ByVal modalidad As String, ByVal codigo_ppr As Integer, ByVal codigo_dpr As Integer, ByVal codigo_dpe As Integer, ByVal especificacion As String, ByVal codigo_dap As Integer)

        cnx.Ejecutar("LOG_ActualizarDetallePedido_VPOA", codigo_dpe, codigo_ped, codigo_Art, codigo_cco, precioReferencial, cantidad, observacion, fechaEsperada_dpe, estado, tipo, modalidad, codigo_ppr, codigo_dpr, especificacion, codigo_dap)

    End Sub

    Public Function ConsultarConceptos(ByVal tipo As String, ByVal clase As Int16, ByVal linea As Int32, ByVal familia As Int32, ByVal subfamilia As Int32, ByVal concepto As String) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            If tipo = "I" Then ' *** Ingresos ***
                datos = (ObjCnx.TraerDataTable("LOG_ConsultarConceptoEgreso", clase, linea, familia, subfamilia, concepto.Trim))
            Else '*** Egresos ***
                datos = ObjCnx.TraerDataTable("LOG_ConsultarConceptoEgreso", clase, linea, familia, subfamilia, concepto.Trim)
            End If
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function ConsultarPedidos(ByVal codigo_ped As Integer, ByVal codigo_Cco As Integer, ByVal trabajador As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarPedidos", "AD", codigo_ped, codigo_Cco, trabajador)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarPedidos_SO(ByVal codigo_peR As Integer, ByVal codigo_inst As Integer, ByVal veredicto As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarPedidos", "SO", codigo_peR, codigo_inst, veredicto)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarPedidos_VE(ByVal codigo_ped As Integer, ByVal codigo_Cco As Integer, ByVal trabajador As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarPedidos", "VE", codigo_ped, codigo_Cco, trabajador)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarPedidosSolicitante(ByVal codigo_ped As Integer, ByVal codigo_per As Integer, ByVal trabajador As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarPedidos", "S2", codigo_ped, codigo_per, trabajador)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarPedidosSolicitante_V2(ByVal codigo_per As Integer, ByVal codigo_ped As Integer, ByVal veredicto As String, ByVal trabajador As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        If trabajador = "" Then
            dts = cnx.TraerDataTable("LOG_ConsultarPedidos_v2", "VE", codigo_per, codigo_ped, veredicto, DBNull.Value)
        Else
            dts = cnx.TraerDataTable("LOG_ConsultarPedidos_v2", "VE", codigo_per, DBNull.Value, veredicto, trabajador)
        End If

        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertarConfirmacionServicio(ByVal tipo As String, ByVal codigo_per As Integer, ByVal observaciones As String, ByVal codigo_rco As Integer) As Boolean
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("LOG_AgregarConfirmacionServicio", tipo, codigo_per, observaciones, codigo_rco)
        cnx.CerrarConexion()
        Return True
    End Function

    Public Function ConsultarPedidosPorOrdenCompra(ByVal codigo_rco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarPedidosPorOrdenCompra", codigo_rco)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Sub CambiarItemTesoreria(ByVal codigo_ecc As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("LOG_PasarEjecucionTesoreria", codigo_ecc)
        cnx.CerrarConexion()
    End Sub
    Public Sub CambiarItemCompras(ByVal codigo_ecc As Integer, ByVal codigo_ped As Integer, ByVal codigo_per As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString



        'cambio 25/11/2020   --- este es solo para el obtener el estado antes del cambio------------------
        Dim estadoOrig As String
        Dim dtEst As New Data.DataTable
        cnx.AbrirConexion()
        dtEst = cnx.TraerDataTable("LOG_InsertaBitacoraCambio", "CECC", codigo_per, codigo_ecc, "")
        cnx.CerrarConexion()
        estadoOrig = dtEst.Rows(0).Item("estado_ecc")
        '--------------------------------------------------------------------------------------------------


        cnx.AbrirConexion()
        cnx.Ejecutar("LOG_PasarEjecucionCompras", codigo_ecc)
        cnx.CerrarConexion()

        Dim ObjMailNet As New ClsEnviarCorreoAprobacionOrden 'ClsMail
        Dim correo As String
        Dim asunto As String
        Dim mensaje As String
        Dim nombreTrabajador As String
        Dim revisor As String
        Dim coreeoRevisor As String

        Dim dt, dt2 As New Data.DataTable
        
        'Si el pedido es enviado a COmpras se envía una notificación a TYONG: dflores 26/07/16


       
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("LOG_ConsultarDatosSolicitante", codigo_ped, codigo_per)
        cnx.CerrarConexion()

        cnx.AbrirConexion()
        dt2 = cnx.TraerDataTable("LOG_CosnultarDetalleEjecucionCentroCostosPorPedido1", codigo_ped, DBNull.Value)
        cnx.CerrarConexion()

        correo = dt.Rows(0).Item("correo").ToString
        coreeoRevisor = dt.Rows(0).Item("CorreoRevisor").ToString
        nombreTrabajador = dt.Rows(0).Item("personal").ToString
        revisor = dt.Rows(0).Item("revisor").ToString

        mensaje = "<table border=0 cellpadding=0 width=""100%"">"
        mensaje &= "<tr><td style=""border:none;border-bottom:solid #E33439 1.0pt;padding:.75pt .75pt .75pt .75pt""><p class=""MsoNormal""><span style=""font-size:14pt;font-family:&quot;Calibri&quot;,sans-serif;color:#E33439"">Sistema de Compras - Campus Virtual USAT</span><span style=""font-family:&quot;Calibri&quot;,sans-serif;color:#E33439""><o:p></o:p></span></p></td></tr>"
        mensaje &= "<tr><td style=""padding:.75pt .75pt .75pt .75pt""><p class=""MsoNormal""><b><span style=""font-size:1;font-family:&quot;Calibri&quot;,sans-serif""><o:p>&nbsp;</o:p></span></b></p>"

        asunto = "Pedido: " & codigo_ped & " EN COMPRAS"

        mensaje = "<table border=0 cellpadding=0>"
        mensaje &= "<tr><td style=""border:none;border-bottom:solid #E33439 1.0pt;padding:.75pt .75pt .75pt .75pt""><p class=""MsoNormal""><span style=""font-size:14pt;font-family:&quot;Calibri&quot;,sans-serif;color:#E33439"">Sistema de Compras - Campus Virtual USAT</span><span style=""font-family:&quot;Calibri&quot;,sans-serif;color:#E33439""><o:p></o:p></span></p></td></tr>"
        mensaje &= "<tr><td style=""padding:.75pt .75pt .75pt .75pt""><p class=""MsoNormal""><b><span style=""font-size:1;font-family:&quot;Calibri&quot;,sans-serif""><o:p>&nbsp;</o:p></span></b></p>"
        mensaje &= "<p class=""MsoNormal""><b><span style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">Estimado Sr. YONG WONG AUGUSTO ROBERTO </span></b><span style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif""><o:p></o:p></span></p>"
        mensaje &= "</td></tr>"
        mensaje = mensaje & "<tr><td colspan=2 style=""padding:.75pt .75pt .75pt .75pt""></td></tr>"
        mensaje = mensaje & "<tr><td colspan=2 style=""padding:.75pt .75pt .75pt .75pt""><p class=""MsoNormal""><span style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">El pedido N° " & codigo_ped & " solicitado por " & nombreTrabajador & " posee ítems que han sido enviados a COMPRAS."
        mensaje = mensaje & "<tr></tr><tr style =""background-color:#E33439;color:white; ""><td colspan=2 style=""padding:.75pt .75pt .75pt .75pt"" align=""center""><p class=""MsoNormal""><span style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">Artículo</td><td align=""center"" style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">Precio</td><td  align=""center"" style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">Cantidad</td><td  align=""center"" style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">Observación</td></tr>"


        If dt2.Rows.Count > 0 Then
            For index As Integer = 0 To dt2.Rows.Count - 1

                If dt2.Rows(index).Item("estado_Ecc") = 3 Then
                    mensaje = mensaje & "<tr><td colspan=2 style=""padding:.75pt .75pt .75pt .75pt ;font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif""><p class=""MsoNormal""><span style="" vertical-align:middle;font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">" & dt2.Rows(index).Item("descripcionArt") & "</td><td style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">  " & dt2.Rows(index).Item("PrecioArt") & "</td><td align=""right"" style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">" & Decimal.Round(CType(dt2.Rows(index).Item("CantTotalItem"), Decimal), 2) & "</td><td align=""right"" style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">" & dt2.Rows(index).Item("observacion_Dpe") & "</td></span></tr>"

                End If
            Next
        End If


        ObjMailNet.EnviarMailAd("campusvirtual@usat.edu.pe", "Sistema de Pedidos - Logística", "tyong@usat.edu.pe", asunto, mensaje, True)
        'ObjMailNet.EnviarMailAd("campusvirtual@usat.edu.pe", "Sistema de Pedidos - Logística", "olluen@usat.edu.pe", asunto, mensaje, True)
        'ObjMailNet.EnviarMailAd("campusvirtual@usat.edu.pe", "Sistema de Logística", "dflores@usat.edu.pe", asunto, mensaje, True)

        'cambio 25/11/2020 ---------------------------------insertar en bitacoralogistica --------------------------------------------------------
        Dim dt3 As New Data.DataTable
        cnx.AbrirConexion()
        dt3 = cnx.TraerDataTable("LOG_InsertaBitacoraCambio", "ECC", codigo_per, codigo_ecc, estadoOrig)
        cnx.CerrarConexion()
        '-------------------------------------------------------------------------------------------------------------------------------------------

    End Sub
    Public Function ConsultarPedidosPorItem(ByVal codigo_ped As Integer, ByVal codigo_Cco As Integer, ByVal trabajador As String, ByVal descripcionItem As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarPedidosPorItem", codigo_ped, codigo_Cco, trabajador, descripcionItem)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Sub CambiarItemDespachar(ByVal codigo_ecc As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("LOG_PasarEjecucionDespachar", codigo_ecc)
        cnx.CerrarConexion()
    End Sub
    Public Function ConsultarImporteNoPresupuestado(ByVal codigo_prp As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("LOG_ConsultarImporteNoPresupuestado", codigo_prp)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Sub EjecutarDeotroPresupuesto(ByVal codigo_ped As Integer, ByVal codigo_dpr As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("LOG_EjecutarDeotroPresupuesto", codigo_ped, codigo_dpr)
        cnx.CerrarConexion()
    End Sub
    Public Function AgregarPedido_v1(ByVal codigo_per As Int32, ByVal codigo_cco As Int32, _
                                  ByVal estado_ped As Integer, ByVal importe_ped As Double, _
                                  ByVal fecha_Ped As String, ByVal observacion_Ped As String, _
                                  ByVal EjercicioPresupuestal As Integer, ByVal almacen As Integer) As Integer
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer

            cnx.Ejecutar("LOG_RegistrarPedido_v1", codigo_per, codigo_cco, estado_ped, importe_ped, fecha_Ped, observacion_Ped, EjercicioPresupuestal, almacen, 0).copyto(valoresdevueltos, 0)
            rpta = valoresdevueltos(0)

            'rpta = cnx.Ejecutar("LOG_RegistrarPedido_v1", codigo_per, codigo_cco, estado_ped, importe_ped, fecha_Ped, observacion_Ped, EjercicioPresupuestal, almacen, 0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 0
        End Try
    End Function
    'Hcano
    Public Sub AgregarDetallePedido_V1(ByVal codigo_ped As Integer, ByVal codigo_Art As Integer, ByVal codigo_cco As Integer, _
                                    ByVal precioReferencial As Double, ByVal cantidad As Double, ByVal observacion As String, _
                                    ByVal fechaEsperada_dpe As String, ByVal estado As Integer, ByVal tipo As String, _
                                    ByVal modalidad As String, ByVal codigo_ppr As Integer, ByVal codigo_dpr As Integer, _
                                    ByVal idUni As Integer, ByVal codPer As Integer, ByVal conformidad As Boolean, ByVal especificacion As String)
        If (conformidad = False) Then
            cnx.Ejecutar("LOG_RegistrarDetallePedido_V1", codigo_ped, codigo_Art, codigo_cco, precioReferencial, cantidad, observacion, fechaEsperada_dpe, estado, tipo, modalidad, codigo_ppr, codigo_dpr, idUni, codPer, 0, especificacion)
        Else
            cnx.Ejecutar("LOG_RegistrarDetallePedido_V1", codigo_ped, codigo_Art, codigo_cco, precioReferencial, cantidad, observacion, fechaEsperada_dpe, estado, tipo, modalidad, codigo_ppr, codigo_dpr, idUni, codPer, 1, especificacion)
        End If
    End Sub

    Public Function AgregarDetallePedido_POA(ByVal codigo_ped As Integer, ByVal codigo_Art As Integer, ByVal codigo_cco As Integer, _
                                    ByVal precioReferencial As Double, ByVal cantidad As Double, ByVal observacion As String, _
                                    ByVal fechaEsperada_dpe As String, ByVal estado As Integer, ByVal tipo As String, _
                                    ByVal modalidad As String, ByVal codigo_ppr As Integer, ByVal codigo_dpr As Integer, _
                                    ByVal idUni As Integer, ByVal codPer As Integer, ByVal conformidad As Boolean, ByVal especificacion As String, ByVal codigo_dap As Integer) As Integer
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer

            If (conformidad = False) Then
                cnx.Ejecutar("LOG_RegistrarDetallePedido_POA", codigo_ped, codigo_Art, codigo_cco, precioReferencial, cantidad, observacion, fechaEsperada_dpe, estado, tipo, modalidad, codigo_ppr, codigo_dpr, idUni, codPer, 0, especificacion, codigo_dap).copyto(valoresdevueltos, 0)
            Else
                cnx.Ejecutar("LOG_RegistrarDetallePedido_POA", codigo_ped, codigo_Art, codigo_cco, precioReferencial, cantidad, observacion, fechaEsperada_dpe, estado, tipo, modalidad, codigo_ppr, codigo_dpr, idUni, codPer, 1, especificacion, codigo_dap).copyto(valoresdevueltos, 0)
            End If
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
        End Try
    End Function
    'Fin Hcano
    Public Sub AgregarDetallePedido_POA_v1(ByVal codigo_ped As Integer, ByVal codigo_Art As Integer, ByVal codigo_cco As Integer, _
                                ByVal precioReferencial As Double, ByVal cantidad As Double, ByVal observacion As String, _
                                ByVal fechaEsperada_dpe As String, ByVal estado As Integer, ByVal tipo As String, _
                                ByVal modalidad As String, ByVal codigo_ppr As Integer, ByVal codigo_dpr As Integer, _
                                ByVal idUni As Integer, ByVal codPer As Integer, ByVal conformidad As Boolean, ByVal especificacion As String, ByVal codigo_dap As Integer)
        Try

            If (conformidad = False) Then
                cnx.Ejecutar("LOG_RegistrarDetallePedido_POA", codigo_ped, codigo_Art, codigo_cco, precioReferencial, cantidad, observacion, fechaEsperada_dpe, estado, tipo, modalidad, codigo_ppr, codigo_dpr, idUni, codPer, 0, especificacion, codigo_dap)
            Else
                cnx.Ejecutar("LOG_RegistrarDetallePedido_POA", codigo_ped, codigo_Art, codigo_cco, precioReferencial, cantidad, observacion, fechaEsperada_dpe, estado, tipo, modalidad, codigo_ppr, codigo_dpr, idUni, codPer, 1, especificacion, codigo_dap)
            End If

        Catch ex As Exception
            cnx.AbortarTransaccion()
        End Try
    End Sub
    Public Sub EnvioConfirmacionPedido(ByVal codigo_ped As Integer, ByVal Articulo As String, _
                                    ByVal cantPed As Decimal, ByVal cantAten As Decimal, _
                                    ByVal codigo_per As Integer, ByVal Despachado As String)
        Try
            '----------------Envío mail --------------------------------------
            Dim ObjMailNet As New ClsMail
            Dim correo As String
            Dim asunto As String
            Dim mensaje As String
            Dim nombreTrabajador As String

            Dim dt_Per As New Data.DataTable
            Dim dt_Detalle As New Data.DataTable
            Dim cnx As New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            cnx.AbrirConexion()
            dt_Per = cnx.TraerDataTable("LOG_BuscaPersonal", codigo_per, "")
            cnx.CerrarConexion()
            'Tiene correo
            If (dt_Per.Rows.Count > 0) Then
                correo = dt_Per.Rows(0).Item("usuario_per").ToString & "@usat.edu.pe"
                nombreTrabajador = dt_Per.Rows(0).Item("Nombre").ToString
                asunto = "Pedido: " & codigo_ped & ": Entrega Confirmada"
                mensaje = nombreTrabajador & "</BR></BR>El pedido N°" & codigo_ped & " le ha sido ENTREGADO a " & Despachado & "<br />"
                mensaje = mensaje & "Articulo: " & Articulo & "<br />"
                mensaje = mensaje & "Cant. Pedida: " & cantPed & "<br />"
                mensaje = mensaje & "Cant. Atendida: " & cantAten & "<br />"
                mensaje = mensaje & "</BR></BR> Atte. </br> Campus Virtual "

                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Sistema de Pedidos - Logística", correo, asunto, mensaje, True)
            End If
        Catch ex As Exception
            cnx.AbortarTransaccion()
        End Try
    End Sub

    Public Sub EnvioSolicitaConfirmacionPedido(ByVal codigo_ped As Integer, ByVal Articulo As String, _
                                    ByVal cantPed As Decimal, ByVal cantAten As Decimal, _
                                    ByVal codigo_per As Integer, ByVal Despachado As String, _
                                    ByVal responderA As String)
        Try
            '----------------Envío mail --------------------------------------
            Dim ObjMailNet As New ClsMail
            Dim correo As String
            Dim asunto As String
            Dim mensaje As String
            Dim nombreTrabajador As String

            Dim dt_Per As New Data.DataTable
            Dim dt_Detalle As New Data.DataTable
            Dim cnx As New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            cnx.AbrirConexion()
            dt_Per = cnx.TraerDataTable("LOG_BuscaPersonal", codigo_per, "")
            cnx.CerrarConexion()
            'Tiene correo
            If (dt_Per.Rows.Count > 0) Then
                correo = dt_Per.Rows(0).Item("usuario_per").ToString & "@usat.edu.pe"
                nombreTrabajador = dt_Per.Rows(0).Item("Nombre").ToString
                asunto = "Pedido: " & codigo_ped & ": Solicita confirmacion"
                mensaje = nombreTrabajador & "</BR></BR>El pedido N°" & codigo_ped & " le ha sido ENTREGADO a " & Despachado & ", pero no ha sido confirmado.<br />"
                mensaje = mensaje & "Articulo: " & Articulo & "<br />"
                mensaje = mensaje & "Cant. Pedida: " & cantPed & "<br />"
                mensaje = mensaje & "Cant. Atendida: " & cantAten & "<br />"
                mensaje = mensaje & "<br/>Sirvase a confirmarlo a través de la opción 'CONFIRMACION DE RECEPCION' en el módulo de logística del campus virtual.<br/>"
                mensaje = mensaje & "</BR></BR> Atte. </br> Campus Virtual "

                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Sistema de Pedidos - Logística", correo, asunto, mensaje, True, "", responderA)
            End If
        Catch ex As Exception
            cnx.AbortarTransaccion()
        End Try
    End Sub
    Public Function consultarResponsableCeco(ByVal codigo_Ceco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("Log_ConsultarResponsableCeco", codigo_Ceco) ' 19.02.14 por Fcastillo - para saber el responsable de aprobación del Nuevo Pedido
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function consultarResponsableNuevoCeco(ByVal codigo_Ceco As Integer, ByVal codigo_Ped As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("Log_ModificarDirectorInmediato", codigo_Ceco, codigo_Ped) ' 19.02.14 por Fcastillo - para saber el responsable de aprobación del Nuevo Pedido
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function derivarAprobacionPedido(ByVal codigo_Per As Integer, ByVal codigo_Ped As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("Log_DerivarResponsableAprobacion", codigo_Per, codigo_Ped) ' 05.03.14 por Fcastillo - para derivar a los responsables de aprobar pedidos 
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Sub CalificarPedido(ByVal codigo_ped As Integer, ByVal codigo_per As Integer, _
                            ByVal veredicto As String, ByVal obs As String)
        Dim da As String
        Try
            '----------------Envío mail --------------------------------------
            Dim ObjMailNet As New ClsEnviarCorreoAprobacionOrden 'ClsMail
            Dim correo As String
            Dim asunto As String
            Dim mensaje As String
            Dim calificacion As String
            Dim nombreTrabajador As String
            Dim revisor As String
            Dim coreeoRevisor As String
            Dim estadoPedido As Integer

            Dim dt, dt2 As New Data.DataTable
            Dim dtEst, dtDetalle As New Data.DataTable
            Dim cnx As New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            calificacion = ""
            Select Case UCase(veredicto)
                Case "O" : calificacion = "OBSERVADO"
                Case "C" : calificacion = "CONFORME"
                Case "R" : calificacion = "RECHAZADO"
                Case "D" : calificacion = "DERIVADO"
            End Select

            cnx.AbrirConexion()
            dtEst = cnx.TraerDataTable("LOG_ConsultarEstadoPedido", codigo_ped) ' consulta de estado de pedido antes de ser calificado
            estadoPedido = dtEst.Rows(0).Item("Estado")
            cnx.CerrarConexion()
            cnx.AbrirConexion()
            cnx.Ejecutar("LOG_CalificarPedido", codigo_ped, codigo_per, veredicto, obs)
            cnx.CerrarConexion()

            cnx.AbrirConexion()
            dt = cnx.TraerDataTable("LOG_ConsultarDatosSolicitante", codigo_ped, codigo_per)
            cnx.CerrarConexion()
            dtDetalle = ConsultarDetalleEjecutado(codigo_ped)

            If (dt.Rows.Count > 0) Then
                correo = dt.Rows(0).Item("correo").ToString
                coreeoRevisor = dt.Rows(0).Item("CorreoRevisor").ToString
                nombreTrabajador = dt.Rows(0).Item("personal").ToString
                revisor = dt.Rows(0).Item("revisor").ToString
                asunto = "Pedido: " & codigo_ped & ": " & calificacion

                mensaje = "<style type=""text/css""> td{font-size:11.0pt;font-family:Calibri,sans-serif;}</style>"
                mensaje &= "<table border=0 cellpadding=0 width=""100%"">"
                mensaje &= "<tr><td style=""border:none;border-bottom:solid #E33439 1.0pt;padding:.75pt .75pt .75pt .75pt""><p class=""MsoNormal""><span style=""font-size:14pt;font-family:&quot;Calibri&quot;,sans-serif;color:#E33439"">Sistema de Compras - Campus Virtual USAT</span><span style=""font-family:&quot;Calibri&quot;,sans-serif;color:#E33439""><o:p></o:p></span></p></td></tr>"
                mensaje &= "<tr><td style=""padding:.75pt .75pt .75pt .75pt""><p class=""MsoNormal""><b><span style=""font-size:1;font-family:&quot;Calibri&quot;,sans-serif""><o:p>&nbsp;</o:p></span></b></p>"

                If estadoPedido = 1 Then

                    mensaje &= "<p class=""MsoNormal""><span style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">Estimado(a) <b>" & revisor & "</b> se ha generado correctamente su pedido N° " & codigo_ped & ". "

                    If (obs.Trim <> "") Then
                        mensaje = mensaje & "<br/><br/>  Observación: " & obs
                    Else
                        mensaje = mensaje & "<br/><br/>  Observación: Ninguna"
                    End If
                    mensaje &= "</span></p></td></tr></table>"

                    ObjMailNet.EnviarMailAd("campusvirtual@usat.edu.pe", "Sistema de Pedidos - Logística", coreeoRevisor, asunto, mensaje, True)
                    'ObjMailNet.EnviarMailAd("campusvirtual@usat.edu.pe", "Sistema de Pedidos - Logística", "dflores@usat.edu.pe", asunto, mensaje, True)


                Else

                    mensaje &= "<p class=""MsoNormal""><span style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">Estimado(a) <b>" & nombreTrabajador & "</b><br>El pedido N° " & codigo_ped & " ha sido calificado como <b>" & calificacion & "</b> por " & revisor & "."

                    If (obs.Trim <> "") Then
                        mensaje = mensaje & "<br/><br/> Observación: " & obs
                    Else
                        mensaje = mensaje & "<br/><br/> Observación: Ninguna"
                    End If
                    mensaje &= "</span></p></td></tr></table>"

                    ObjMailNet.EnviarMailAd("campusvirtual@usat.edu.pe", "Sistema de Logística", correo, asunto, mensaje, True)
                    'ObjMailNet.EnviarMailAd("campusvirtual@usat.edu.pe", "Sistema de Logística", "dflores@usat.edu.pe", asunto, mensaje, True)

                    'Si el pedido es enviado a COmpras se envía una notificación a TYONG: dflores 26/07/16
                    cnx.AbrirConexion()
                    dt2 = cnx.TraerDataTable("LOG_CosnultarDetalleEjecucionCentroCostosPorPedido1", codigo_ped, DBNull.Value)
                    cnx.CerrarConexion()
                    If dt2.Rows.Count > 0 Then
                        Dim cont As Integer = 0
                        mensaje = "<table border=0 cellpadding=0>"
                        mensaje &= "<tr><td style=""border:none;border-bottom:solid #E33439 1.0pt;padding:.75pt .75pt .75pt .75pt""><p class=""MsoNormal""><span style=""font-size:14pt;font-family:&quot;Calibri&quot;,sans-serif;color:#E33439"">Sistema de Compras - Campus Virtual USAT</span><span style=""font-family:&quot;Calibri&quot;,sans-serif;color:#E33439""><o:p></o:p></span></p></td></tr>"
                        mensaje &= "<tr><td style=""padding:.75pt .75pt .75pt .75pt""><p class=""MsoNormal""><b><span style=""font-size:1;font-family:&quot;Calibri&quot;,sans-serif""><o:p>&nbsp;</o:p></span></b></p>"
                        mensaje &= "<p class=""MsoNormal""><b><span style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">Estimado Sr. YONG WONG AUGUSTO ROBERTO </span></b><span style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif""><o:p></o:p></span></p>"
                        mensaje &= "</td></tr>"
                        mensaje = mensaje & "<tr><td colspan=2 style=""padding:.75pt .75pt .75pt .75pt""></td></tr>"
                        mensaje = mensaje & "<tr></tr><tr><td colspan=2 style=""padding:.75pt .75pt .75pt .75pt""><p class=""MsoNormal""><span style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">El pedido N° " & codigo_ped & " solicitado por " & nombreTrabajador & " posee ítems que han sido enviados a COMPRAS.</td></tr>"
                        mensaje = mensaje & "<tr></tr><tr style =""background-color:#E33439;color:white; ""><td colspan=2 style=""padding:.75pt .75pt .75pt .75pt"" align=""center""><p class=""MsoNormal""><span style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">Artículo</td><td align=""center"" style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">Precio</td><td  align=""center"" style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">Cantidad</td><td  align=""center"" style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">Observación</td></tr>"
                        asunto = "Pedido: " & codigo_ped & " en Compras "

                        For index As Integer = 0 To dt2.Rows.Count - 1

                            If dt2.Rows(index).Item("estado_Ecc") = 3 Then
                                mensaje = mensaje & "<tr><td colspan=2 style=""padding:.75pt .75pt .75pt .75pt ;font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif""><p class=""MsoNormal""><span style="" vertical-align:middle;font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">" & dt2.Rows(index).Item("descripcionArt") & "</td><td style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">  " & dt2.Rows(index).Item("PrecioArt") & "</td><td align=""right"" style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">" & Decimal.Round(CType(dt2.Rows(index).Item("CantTotalItem"), Decimal), 2) & "</td><td align=""right"" style=""font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif"">" & dt2.Rows(index).Item("observacion_Dpe") & "</td></span></tr>"
                                cont += 1
                            End If

                        Next

                        If cont > 0 Then
                            ObjMailNet.EnviarMailAd("campusvirtual@usat.edu.pe", "Sistema de Pedidos - Logística", "tyong@usat.edu.pe", asunto, mensaje, True)
                            'ObjMailNet.EnviarMailAd("campusvirtual@usat.edu.pe", "Sistema de Logística", "dflores@usat.edu.pe", asunto, mensaje, True)

                        End If

                    End If

                    '----------------Envío mail --------------------------------------
                    ' para enviar mensaje de alerta a presupuesto solo cuando un pedido observado ha sido corregido

                    If calificacion = "CONFORME" And estadoPedido = 12 Then
                        mensaje = "El usuario: " & nombreTrabajador & " ha levantado las observaciones hechas al Pedido N°: " & codigo_ped
                        ObjMailNet.EnviarMailAd("campusvirtual@usat.edu.pe", "Sistema de Pedidos - Logística", "rvelasco@usat.edu.pe", asunto, mensaje, True)
                        'ObjMailNet.EnviarMailAd("campusvirtual@usat.edu.pe", "Sistema de Pedidos - Logística", "dflores@usat.edu.pe", asunto, mensaje, True)
                    End If
                End If

            End If

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex

        End Try
    End Sub

    Public Sub actualizarVeredictoRevisores(ByVal codigo_ped As Integer, ByVal veredicto As String, ByVal fechaRevision As Date)
        Dim cnx As New ClsConectarDatos
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("LOG_ActualizarVeredictoRevisores", codigo_ped, veredicto, fechaRevision)
        cnx.CerrarConexion()
    End Sub
    Public Function ConvertFileToBase64(ByVal fileName As String) As String
        Return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName))
    End Function
    Public Sub RegistrarDocumentoDPE(ByVal codigo_dpe As Integer, ByVal descripcion As String, ByVal extension As String)
        Dim cnx As New ClsConectarDatos
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("LOG_GenerarArchivoPedidoLogistica", codigo_dpe, descripcion, extension)
        cnx.CerrarConexion()
    End Sub
    Public Function EncrytedString64(ByVal _stringToEncrypt As String) As String
        Dim result As String = ""
        Dim encryted As Byte()
        encryted = System.Text.Encoding.Unicode.GetBytes(_stringToEncrypt)
        result = Convert.ToBase64String(encryted)
        Return result
    End Function

    Public Function DecrytedString64(ByVal _stringToDecrypt As String) As String
        Dim result As String = ""
        Dim decryted As Byte()
        decryted = Convert.FromBase64String(_stringToDecrypt)
        result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.Length)
        Return result
    End Function

End Class

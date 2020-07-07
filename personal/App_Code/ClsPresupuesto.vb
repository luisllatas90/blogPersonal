Imports Microsoft.VisualBasic
Imports System.Data

Public Class ClsPresupuesto
    Private cnx As New ClsConectarDatos

    Public Function ObtenerListaCentroCostos(ByVal tipo As String, ByVal valor As Int32) As DataTable
        ' Try
        Dim ObjCnx As New ClsConectarDatos
        Dim Datos As New Data.DataTable
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        ObjCnx.AbrirConexion()
        Datos = ObjCnx.TraerDataTable("PRESU_ConsultarCentroCostos", tipo, valor)
        ObjCnx.CerrarConexion()
        ObjCnx = Nothing
        Return Datos
        'Catch ex As Exception
        '    Return Nothing
        'End Try
    End Function

    Public Function ObtenerListaCentroCostos(ByVal tipo As String, ByVal raiz As Int16, ByVal codigo_per As Int32) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            'datos = ObjCnx.TraerDataTable("presu_consultarareapresupuestal", tipo, raiz, codigo_per)
            datos = ObjCnx.TraerDataTable("presu_consultarcentrocostos", "U", codigo_per)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerListaCentroCostos_v2(ByVal tipo As String, ByVal codigo_per As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            'datos = ObjCnx.TraerDataTable("presu_consultarareapresupuestal", tipo, raiz, codigo_per)
            datos = ObjCnx.TraerDataTable("presu_consultarcentrocostos", tipo, codigo_per)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerListaPresupuesto(ByVal codigo_cco As Int64) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("presu_consultarlistapresupuestosporcentrocostos", codigo_cco)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerListaProcesos() As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("presu_consultarprocesosactivos")
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerTechosPresupuestales(ByVal codigo_ejp As Int32, ByVal codigo_cco As Int32) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_ConsultarTechos", codigo_ejp, codigo_cco)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarProcesoContable() As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("presu_consultarprocesocontable", "1")
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerListaEstados(ByVal tipo As String, ByVal valor As String) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_ConsultarEstadosPresupuesto", tipo, valor)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function AgregarPresupuesto(ByVal codigo_Cco As Int64, ByVal codigo_Pct As Int64, ByVal fechaInicio_Pto As DateTime, _
        ByVal fechaFin_Pto As DateTime, ByVal codigo_Per As Int64, ByVal observacion_Pto As String, ByVal totalIngresos_Pto As Decimal, _
        ByVal totalEgresos_Pto As Decimal) As String
        Dim ObjCnx As New ClsConectarDatos
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            Dim rpta As Int16
            ObjCnx.IniciarTransaccion()
            rpta = ObjCnx.Ejecutar("PRESU_AGREGARPRESUPUESTO", codigo_Cco, codigo_Pct, fechaInicio_Pto, fechaFin_Pto, _
                                   codigo_Per, observacion_Pto, totalIngresos_Pto, totalEgresos_Pto, 0)
            ObjCnx.TerminarTransaccion()
            Return rpta.ToString
        Catch ex As Exception
            ObjCnx.AbortarTransaccion()
            Return ex.Message
        End Try
        ObjCnx = Nothing
    End Function

    Public Function ConsultarDatosPresupuesto(ByVal tipo As Int16, ByVal codigo_pto As Int64) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("presu_consultarDatosPresupuesto", tipo, codigo_pto)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarDetalleEjecucion(ByVal codigo_dpr As Int64) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_ConsultarDetalleEjecucion", codigo_dpr)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function EliminarDetallePresupuesto(ByVal codigo_dpr As Integer, ByVal codigo_per As Integer) As Integer
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Byte
            ' ObjCnx.IniciarTransaccion()
            cnx.Ejecutar("PRESU_EliminarDetallePresupuesto", codigo_dpr, codigo_per).copyto(valoresdevueltos, 0)
            rpta = CInt(valoresdevueltos(0))
            'ObjCnx.TerminarTransaccion()
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 3 '"Error" & ex.Message
        End Try
    End Function

    Public Function ObtenerListaClase(ByVal tipo As String, ByVal valor As String) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            If valor <> "-2" Then
                If tipo = "COD" Then ' *** Por Código ***
                    Datos = ObjCnx.TraerDataTable("PRESU_ConsultarClasePorCodigo", CInt(valor))
                Else '*** Descripcion ***
                    Datos = ObjCnx.TraerDataTable("PRESU_ConsultarClasePorDescripcion", valor)
                End If
            Else
                Datos = Nothing
            End If
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerListaLinea(ByVal tipo As String, ByVal valor As String) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            If valor <> "-2" Then
                If tipo = "COD" Then ' *** Por Código ***
                    Datos = ObjCnx.TraerDataTable("PRESU_ConsultarLineaPorCodigo", CInt(valor))
                ElseIf tipo = "DES" Then '*** Descripcion ***
                    Datos = ObjCnx.TraerDataTable("PRESU_ConsultarLineaPorDescripcion", valor)
                Else '*** Clase ***'
                    Datos = ObjCnx.TraerDataTable("PRESU_ConsultarLineaPorClase", CInt(valor))
                End If
            Else
                Datos = Nothing
            End If
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerListaFamilia(ByVal tipo As String, ByVal valor1 As String, Optional ByVal valor2 As String = "0") As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            If tipo = "COD" Then ' *** Por Código ***
                Datos = ObjCnx.TraerDataTable("PRESU_ConsultarFamiliaPorCodigo", CInt(valor1))
            ElseIf tipo = "DES" Then '*** Descripcion ***
                Datos = ObjCnx.TraerDataTable("PRESU_ConsultarFamiliaPorDescripcion", valor1)
            Else '*** Clase y linea***'
                Datos = ObjCnx.TraerDataTable("PRESU_ConsultarFamiliaPorClaseLinea", CInt(valor1), CInt(valor2))
            End If
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerListaSubFamilia(ByVal tipo As String, ByVal valor1 As String, Optional ByVal valor2 As String = "0", Optional ByVal valor3 As String = "0") As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            If tipo = "COD" Then ' *** Por Código ***
                Datos = ObjCnx.TraerDataTable("PRESU_ConsultarSubFamiliaPorCodigo", CInt(valor1))
            ElseIf tipo = "DES" Then '*** Descripcion ***
                Datos = ObjCnx.TraerDataTable("PRESU_ConsultarSubFamiliaPorDescripcion", valor1)
            Else '*** Clase y linea***'
                Datos = ObjCnx.TraerDataTable("PRESU_ConsultarSubFamiliaPorClaseLineaFamilia", CInt(valor1), CInt(valor2), CInt(valor3))
            End If
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function ConsultarConceptos(ByVal tipo As String, ByVal clase As Int16, ByVal linea As Int32, ByVal familia As Int32, ByVal subfamilia As Int32, ByVal concepto As String) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            If tipo = "I" Then ' *** Ingresos ***
                datos = (ObjCnx.TraerDataTable("PRESU_ConsultarConceptoIngreso", clase, linea, familia, subfamilia, concepto.Trim))
            Else '*** Egresos ***
                datos = ObjCnx.TraerDataTable("PRESU_ConsultarConceptoEgreso", clase, linea, familia, subfamilia, concepto.Trim)
            End If
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarDetallePresupuesto(ByVal codigo_pct As Int64, ByVal codigo_cco As Int64, ByVal tipo As String) As Data.DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_RepConsultarPresupuestoPorCentroCosto", codigo_pct, codigo_cco, tipo)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function ObtenerDetallePresupuesto(ByVal codigo_dpr As Int64) As Data.DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_ConsultarDetallePresupuesto", codigo_dpr)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ConsultarPresupuestoPorOpciones(ByVal opcion As Int16, ByVal codigo_pct As Int64, ByVal codigo_cco As Int64) As Data.DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_RepConsultarPresupuestoPorOpciones", opcion, codigo_pct, codigo_cco)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function ConsultarPlanAnualCompras(ByVal codigo_pct As Int64, ByVal codigo_cco As Int64, ByVal codigo_cla As Int64) As Data.DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_RepConsultarPlanCompras", codigo_pct, codigo_cco, codigo_cla)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerListaProgramaPresupuestal() As Data.DataTable
        Try
            Dim Objcnx As New ClsConectarDatos
            Dim datos As New Data.DataTable
            Objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Objcnx.AbrirConexion()
            datos = Objcnx.TraerDataTable("PRESU_ConsultarProgramapresupuestal", 0, 0)
            Objcnx.CerrarConexion()
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerDatosUsuario(ByVal usuario As Int64) As Data.DataTable
        Try
            Dim Objcnx As New ClsConectarDatos
            Dim datos As New Data.DataTable
            Objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Objcnx.AbrirConexion()
            datos = Objcnx.TraerDataTable("PRESU_ConsultarDatosUsuario", usuario)
            Objcnx.CerrarConexion()
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

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
    Public Function AgregarPresupuesto(ByVal codigo_ejp As Int64, ByVal codigo_cco As Int32, _
                                       ByVal codigo_ppr As Int32, ByVal comentario As String, _
                                       ByVal prioridad As Int16, ByVal codigo_art As Int32, _
                                       ByVal codigo_rub As Int32, ByVal codigo_cplla As Int32, _
                                       ByVal detalledescripcion As String, ByVal iduni As Int16, _
                                       ByVal preciounit As Decimal, ByVal cantidad As Decimal, _
                                       ByVal vigencia As Int16, ByVal tipo As String, ByVal codigo_per As Int32, _
                                       ByVal indicocantidad As Byte, ByVal subprioridad As Int16, ByVal forzar As Byte) As Integer
        ' Try
        Dim rpta As Integer
        Dim valoresdevueltos(1) As Integer
        cnx.Ejecutar("PRESU_RegistrarPresupuesto", codigo_ejp, codigo_cco, codigo_ppr, comentario, _
                            prioridad, codigo_art, codigo_rub, codigo_cplla, detalledescripcion, iduni, _
                            preciounit, cantidad, vigencia, tipo, codigo_per, indicocantidad, subprioridad, forzar, 0).copyto(valoresdevueltos, 0)
        rpta = valoresdevueltos(0)
        Return rpta

        'Catch ex As Exception
        '    cnx.AbortarTransaccion()
        '    Return 0
        'End Try
    End Function

    Public Function AgregarDetalleEjecucion(ByVal codigo_dpr As Int64, ByVal descripcion_dej As String, _
                                            ByVal precio_dej As Decimal, ByVal cantidad_dej As Decimal) As Byte
        Try
            Dim rpta As Byte
            Dim valoresdevueltos(1) As Byte
            cnx.Ejecutar("PRESU_AgregarDetalleEjecucion", codigo_dpr, descripcion_dej, precio_dej, cantidad_dej, 0).copyto(valoresdevueltos, 0)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 0
        End Try
    End Function

    Public Function ConsultaCentroCostosConPermisos(ByVal tipo As Int64, ByVal codigo_per As Int64, ByVal descripcion_cco As String) As Data.DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_ConsultarCentroCostosXPermisos", tipo, codigo_per, descripcion_cco)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
		
		    Public Function ConsultarEjecutadoConsolidado(ByVal fechaInicio As Date, ByVal fechaFin As Date, ByVal todosCeco As Byte, _
                                                 ByVal ListaCodigo_Cco As String, ByVal todosProPre As Byte, ByVal ListaCodigo_Ppr As String, ByVal ListaEstado As String) As Data.DataTable
        Try

            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_ConsultarEjecutadoConsolidado", fechaInicio, fechaFin, todosCeco, ListaCodigo_Cco, todosProPre, ListaCodigo_Ppr, ListaEstado)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class

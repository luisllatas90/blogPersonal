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

    '======================= Agregado Hcano : 20-01-17 =============================================
    Public Function ObtenerListaCentroCostos_v1(ByVal tipo As String, ByVal codigo_per As Integer, ByVal ctf As Integer, ByVal codigo_ejp As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            'datos = ObjCnx.TraerDataTable("presu_consultarareapresupuestal", tipo, raiz, codigo_per)
            datos = ObjCnx.TraerDataTable("presu_consultarcentrocostos_V1", tipo, codigo_per, ctf, codigo_ejp)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    '======================= Fin Hcano : 20-01-17 =============================================

    '======================= Agregado Hcano : 04-07-17 =============================================
    Public Function ObtenerListaCentroCostos_v2(ByVal tipo As String, ByVal codigo_per As Integer, ByVal ctf As Integer, ByVal codigo_ejp As Integer, ByVal codigo_poa As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            datos = ObjCnx.TraerDataTable("presu_consultarcentrocostos_V2", tipo, codigo_per, ctf, codigo_ejp, codigo_poa)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    '======================= Fin Hcano : 04-07-17 =============================================
    '======================= Agregado Hcano : 22-11-16 =============================================
    Public Function ObtenerListaCentroCostos_POA(ByVal id As Integer, ByVal ctf As Integer, ByVal codigo_ejp As Integer, ByVal texto As String) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            'datos = ObjCnx.TraerDataTable("presu_consultarareapresupuestal", tipo, raiz, codigo_per)
            datos = ObjCnx.TraerDataTable("PRESU_ConsultarCentroCostos_POA", id, ctf, codigo_ejp, texto)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    '======================= Fin Agregado Hcano : 22-11-16 =============================================

    Public Function ObtenerListaCentroCostosNuevoPresupuesto(ByVal tipo As String, ByVal raiz As Int16, ByVal codigo_per As Int32) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            'datos = ObjCnx.TraerDataTable("presu_consultarareapresupuestal", tipo, raiz, codigo_per)
            datos = ObjCnx.TraerDataTable("presu_consultarcentrocostos", "NU", codigo_per)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'treyes
    Public Function ObtenerCecoActividadPOA(ByVal codigo_per As Int32, ByVal codigo_ejp As Int32, ByVal descripcion_cco As String) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            datos = ObjCnx.TraerDataTable("PRESU_ObtenerCecoActividadPOA", codigo_per, codigo_ejp, descripcion_cco)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'treyes

    ''hcano
    'Public Function ObtenerCecoActividadPOA_v2(ByVal codigo_per As Integer, ByVal codigo_ejp As Int32, ByVal descripcion_cco As String) As DataTable
    '    Try
    '        Dim ObjCnx As New ClsConectarDatos
    '        Dim datos As New Data.DataTable
    '        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        ObjCnx.AbrirConexion()
    '        datos = ObjCnx.TraerDataTable("PRESU_ObtenerCecoActividadPOA_v2", codigo_per, codigo_ejp, descripcion_cco)
    '        ObjCnx.CerrarConexion()
    '        ObjCnx = Nothing
    '        Return datos
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function
    ''hcano



    Public Function ObtenerDetalleActividadPOA(ByVal codigo_dap As Int32) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            datos = ObjCnx.TraerDataTable("PRESU_ObtenerDetalleActividadPOA", codigo_dap)
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

    Public Function ConsultarProcesoContable_V1() As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("presu_consultarprocesocontable", "2")
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'treyes
    Public Function ObtenerPeriodoPresupuestal(ByVal ctf As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_ObtenerPeriodoPresupuestal", ctf)
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
    'treyes 
    Public Function EliminarDetallePresupuesto_V2(ByVal codigo_dpr As Integer, ByVal codigo_per As Integer) As Integer
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Byte
            cnx.Ejecutar("PRESU_EliminarDetallePresupuesto_V2", codigo_dpr, codigo_per).copyto(valoresdevueltos, 0)
            rpta = CInt(valoresdevueltos(0))
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

    Public Function VisiblidadDetallePresupuesto(ByVal codigo_dpr As Integer, ByVal visible As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            datos = (ObjCnx.TraerDataTable("POA_VisibilidadDetallePresupuesto", codigo_dpr, visible))
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
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
    'treyes
    Public Function ConsultarConceptos_V2(ByVal tipo As String, ByVal concepto As String) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            datos = (ObjCnx.TraerDataTable("PRESU_ConsultarConcepto", tipo, concepto.Trim))
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'treyes

    'hcano 29-11-16 : Solo Articulos
    Public Function ConsultarConceptos_POA(ByVal tipo As String, ByVal concepto As String) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            datos = (ObjCnx.TraerDataTable("PRESU_ConsultarConcepto_POA", tipo, concepto.Trim))
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ValidaMontoPresupuestadoDetalle(ByVal codigo_dpr As Integer, ByVal codigo_dpl As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            datos = (ObjCnx.TraerDataTable("ValidaMontoPresupuestadoDetalle", codigo_dpr, codigo_dpl))
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function VerificaAutorizacionFinanzas_POA(ByVal codigo_pto As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            datos = (ObjCnx.TraerDataTable("POA_VerificaAutorizacionFinanzas", codigo_pto))
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'hcano

    Public Function ObtenerObservaciones(ByVal codigo_Pto As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            datos = (ObjCnx.TraerDataTable("PRESU_ObtenerObservaciones", codigo_Pto))
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'treyes
    Public Function ObtenerFuncionUsuarioPOA(ByVal codigo_per As Integer, ByVal ctf As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            datos = (ObjCnx.TraerDataTable("PRESU_ObtenerFuncionUsuarioPOA", codigo_per, ctf))
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
    'Hcano : 12-12-17 - Inicio
    Public Function ConsultarDetallePresupuesto_v1(ByVal codigo_pct As Int64, ByVal codigo_cco As Int64, ByVal tipo As String) As Data.DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_RepConsultarPresupuestoPorCentroCosto_V1", codigo_pct, codigo_cco, tipo)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'Hcano : 12-12-17 - Fin

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
    'treyes
    Public Function ObtenerDetallePresupuesto_V2(ByVal codigo_dpr As Int64) As Data.DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_ConsultarDetallePresupuesto_V2", codigo_dpr)
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
            Datos = ObjCnx.TraerDataTable("PRESU_RepConsultarPlanCompras_v3", codigo_pct, codigo_cco, codigo_cla)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'Hcano : 04-07-17 - Inicio
    Public Function ConsultarPlanAnualComprasV1(ByVal codigo_pct As Int64, ByVal codigo_cco As Int64, ByVal codigo_cla As Int64, ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_RepConsultarPlanCompras_v4", codigo_pct, codigo_cco, codigo_cla, id, ctf)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'Hcano : 04-07-17 - Fin
    Public Function ConsultarPedidosRealizados(ByVal codigo_pct As Integer, ByVal codigo_cco As Integer) As Data.DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("LOG_ConsultarPedidosRealizados", "DE", codigo_cco, codigo_pct)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtenerListaProgramaPresupuestal(Optional ByVal todos As Boolean = False) As Data.DataTable
        Try
            Dim Objcnx As New ClsConectarDatos
            Dim datos As New Data.DataTable
            Objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Objcnx.AbrirConexion()
            If todos = True Then
                datos = Objcnx.TraerDataTable("PRESU_ConsultarProgramapresupuestal", 2, 0)
            Else
                datos = Objcnx.TraerDataTable("PRESU_ConsultarProgramapresupuestal", 0, 0)
            End If

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
    'treyes
    Public Function AgregarPresupuesto_V2(ByVal codigo_ejp As Int64, ByVal codigo_cco As Int32, ByVal codigo_art As Int32, _
                                       ByVal codigo_rub As Int32, ByVal codigo_cplla As Int32, _
                                       ByVal detalledescripcion As String, ByVal iduni As Int16, _
                                       ByVal preciounit As Decimal, ByVal cantidad As Decimal, _
                                       ByVal vigencia As Int16, ByVal tipo As String, ByVal codigo_per As Int32, _
                                       ByVal indicocantidad As Byte, ByVal forzar As Byte, ByVal codigo_dap As Int32) As Integer
        Dim rpta As Integer
        Dim valoresdevueltos(1) As Integer
        cnx.Ejecutar("PRESU_RegistrarPresupuesto_V2", codigo_ejp, codigo_cco, codigo_art, codigo_rub, codigo_cplla, detalledescripcion, iduni, _
                            preciounit, cantidad, vigencia, tipo, codigo_per, indicocantidad, forzar, codigo_dap, 0).copyto(valoresdevueltos, 0)
        rpta = valoresdevueltos(0)
        Return rpta
    End Function
    'treyes
    Public Function ObservarPto(ByVal codigo_Pto As Int64, ByVal observacion As String, ByVal codigo_per As Integer, ByVal ctf As Integer) As Integer
        Dim rpta As Integer
        Dim valoresdevueltos(1) As Integer

        Try
            cnx.Ejecutar("PRESU_ObservarPto", codigo_Pto, observacion, codigo_per, ctf).copyto(valoresdevueltos, 0)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 2
        End Try

    End Function


    Public Function ObservarPtoxCodigoACP(ByVal codigo_acp As Int64, ByVal observacion As String, ByVal codigo_per As Integer, ByVal ctf As Integer) As Integer
        Dim rpta As Integer
        Dim valoresdevueltos(1) As Integer

        Try
            cnx.Ejecutar("PRESU_ObservarPtoxCodigoACP", codigo_acp, observacion, codigo_per, ctf).copyto(valoresdevueltos, 0)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 2
        End Try

    End Function

    'treyes
    Public Function AprobarPto(ByVal codigo_Pto As Int64, ByVal codigo_per As Integer, ByVal ctf As Integer) As Integer
        Dim rpta As Integer
        Dim valoresdevueltos(1) As Integer

        Try
            cnx.Ejecutar("PRESU_AprobarPto", codigo_Pto, codigo_per, ctf).copyto(valoresdevueltos, 0)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 2
        End Try

    End Function

    'treyes
    Public Function AprobarPtoxCodigoACP(ByVal codigo_acp As Int64, ByVal codigo_per As Integer, ByVal ctf As Integer) As Integer
        Dim rpta As Integer
        Dim valoresdevueltos(1) As Integer

        Try
            cnx.Ejecutar("PRESU_AprobarPtoxCodigoACP", codigo_acp, codigo_per, ctf).copyto(valoresdevueltos, 0)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 2
        End Try

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
    'treyes
    Public Function AgregarDetalleEjecucion_V2(ByVal codigo_dpr As Int64, ByVal descripcion_dej As String, _
                                           ByVal precio_dej As Decimal, ByVal cantidad_dej As Decimal) As Byte
        Try
            Dim rpta As Byte
            Dim valoresdevueltos(1) As Byte
            cnx.Ejecutar("PRESU_AgregarDetalleEjecucion_V2", codigo_dpr, descripcion_dej, precio_dej, cantidad_dej, 0).copyto(valoresdevueltos, 0)
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
    Public Function ConsultaCentroCostosConPermisosNuevoPresupuesto(ByVal tipo As Int64, ByVal codigo_per As Int64, ByVal descripcion_cco As String) As Data.DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_ConsultarCentroCostosXPermisosNuevoPresu", tipo, codigo_per, descripcion_cco)
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

    Public Sub TransferirPresupuesto(ByVal de_codigo_dpr As Integer, ByVal a_codigo_dpr As Integer, ByVal importe As Double)
        Try
            Dim ObjCnx As New ClsConectarDatos
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            ObjCnx.Ejecutar("PRESU_TransferirPresupuesto", de_codigo_dpr, a_codigo_dpr, importe)
            ObjCnx.CerrarConexion()
        Catch ex As Exception
            cnx.AbortarTransaccion()

        End Try
    End Sub
    Public Function ObtenerListaProgramaPresupuestalPersonal(Optional ByVal todos As Boolean = False) As Data.DataTable
        Try
            Dim Objcnx As New ClsConectarDatos
            Dim datos As New Data.DataTable
            Objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Objcnx.AbrirConexion()

            datos = Objcnx.TraerDataTable("PRESU_ConsultarProgramapresupuestal", 3, 0)

            Objcnx.CerrarConexion()
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'treyes
    Public Function ObtenerActividadesPOA(ByVal codigo_cco As Integer, ByVal codigo_ejp As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_ObtenerActividadesPOA", codigo_cco, codigo_ejp)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'treyes
    'hcano
    Public Function ObtenerActividadesPOA_v2(ByVal codigo_cco As Integer, ByVal codigo_ejp As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_ObtenerActividadesPOA_V2", codigo_cco, codigo_ejp)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'hcano



    Public Function ObtenerIngEgrActividadPOA(ByVal codigo_dap As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_ObtenerIngEgrActividadPOA", codigo_dap)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'treyes
    Public Function ObtenerMesesActividadPOA(ByVal codigo_dap As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_ObtenerMesesActividadPOA", codigo_dap)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'treyes
    Public Function ObtenerTopeIngPresupuestal(ByVal codigo_cco As Integer, ByVal codigo_ejp As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_ObtenerTopeIngPresupuestal", codigo_cco, codigo_ejp)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'treyes
    Public Function ValidarEnvioPto(ByVal codigo_cco As Integer, ByVal codigo_ejp As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_ValidarEnvioPto", codigo_cco, codigo_ejp)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'treyes
    Public Function EnviarPresupuesto(ByVal codigo_cco As Integer, ByVal codigo_ejp As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("PRESU_EnviarPresupuesto", codigo_cco, codigo_ejp)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'moises.vilchez
    Public Function PRESU_ListarPoas(ByVal Ejercicio As Integer, ByVal codigo_per As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            datos = ObjCnx.TraerDataTable("PRESU_ListarPoas", Ejercicio, codigo_per)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'HCano
    Public Function POA_MoverActividadPOA(ByVal codigo_dap As Integer, ByVal codigo_ejp As Integer, ByVal codigo_cco As Integer, ByVal mes_nuevo As Integer) As String
        Try
            Dim dts As New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            cnx.AbrirConexion()
            dts = cnx.TraerDataTable("POA_MoverActividadPOA", codigo_dap, codigo_ejp, codigo_cco, mes_nuevo)
            cnx.CerrarConexion()
            Return dts.Rows(0).Item("Mensaje").ToString
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function POA_VisibleMoverActividadPOA(ByVal codigo_pto As Integer) As Integer
        Try
            Dim dts As New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            cnx.AbrirConexion()
            dts = cnx.TraerDataTable("POA_VisibleMoverActividadPOA", codigo_pto)
            cnx.CerrarConexion()
            Return dts.Rows(0).Item("Mensaje").ToString
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'Hcano
    Public Function ObtenerPeriodoPresupuestalxCodigoDpr(ByVal codigo As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("POA_ObtenerPeriodoPresupuestalxCodigoDpr", codigo)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'Hcano
    Public Function ObtenerDatosTransferirPto(ByVal codigo As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("POA_ObtenerDatosTransferirPto", codigo)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'Hcano
    Public Function ListarProgProyTransferirPto(ByVal codigo_poa As Integer, ByVal codigo_acp As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("POA_ListaProgProyTransferir", codigo_poa, codigo_acp)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'Hcano
    Public Function ListarActividadesProgProy(ByVal codigo_acp As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("POA_ListarDetalleActividad", codigo_acp)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'Hcano
    Public Function ConsultarConceptoTransferir() As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("POA_ConsultarConceptoTransferir")
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'Hcano
    Public Function TransferirMontoPresupuesto(ByVal codigo_dpr As Integer, ByVal codigo_acp As Integer, ByVal codigo_Dap As Integer, ByVal idart As Integer, ByVal monto As String, ByVal mes As Integer, ByVal usuario As Integer) As DataTable
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("POA_TransferirPresupuestoProgProy", codigo_dpr, codigo_acp, codigo_Dap, idart, monto, mes, usuario)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Return Datos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class


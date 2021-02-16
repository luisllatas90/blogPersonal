Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Imports System
Imports System.Data

Public Class ClsBandejaGestionRendiciones

    Protected g_DataSesion As ClsDataSesion
    Protected g_AccesoDatos As ClsConectarDatos


    Public Sub New(p_DataSesion As ClsDataSesion)
        g_DataSesion = p_DataSesion
    End Sub

    Public Function ObtenerListaRendicion(p_ObjEntrada As ClsEntradaBandejaGestionRendiciones) As ClsRespuestaServidor

        Dim l_Rpt As ClsRespuestaServidor = New ClsRespuestaServidor()
        Dim l_ListaRendiciones As List(Of ClsEnRendicion)
        Dim l_EnRendicion As ClsEnRendicion
        Dim l_dt As DataTable
        Dim i As Integer = 0

        Try
            l_ListaRendiciones = New List(Of ClsEnRendicion)

            g_AccesoDatos = New ClsConectarDatos()
            g_AccesoDatos.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            g_AccesoDatos.AbrirConexion()
            l_dt = g_AccesoDatos.TraerDataTable("dbo.USP_DOCUMENTOS_X_RENDIR", "1", g_DataSesion.nCodUsuario, p_ObjEntrada.bFlgEstado, "", "")
            g_AccesoDatos.CerrarConexion()

            If Not l_dt Is Nothing AndAlso l_dt.Rows.Count > 0 Then
                For i = 0 To l_dt.Rows.Count - 1
                    l_EnRendicion = New ClsEnRendicion()
                    l_EnRendicion.nCodigo = l_dt.Rows(i).Item("Id").ToString
                    l_EnRendicion.cDescripcion = l_dt.Rows(i).Item("Obsservacion").ToString
                    l_EnRendicion.cDesAtendido = l_dt.Rows(i).Item("Nombres").ToString
                    l_EnRendicion.nNumImporte = l_dt.Rows(i).Item("Importe").ToString
                    l_EnRendicion.cDesMoneda = l_dt.Rows(i).Item("Moneda").ToString
                    l_EnRendicion.nDesImporte = l_EnRendicion.nNumImporte & " " & l_EnRendicion.cDesMoneda
                    l_EnRendicion.dFechaEgreso = l_dt.Rows(i).Item("Fecha").ToString
                    l_ListaRendiciones.Add(l_EnRendicion)
                Next
            End If

            l_Rpt.Resultado = l_ListaRendiciones

        Catch ex As Exception
            l_Rpt.LogError.SetException(ex)
        End Try

        Return l_Rpt
    End Function

    Public Function ObtenerDetalleRendicion(p_ObjEntrada As ClsEntradaBandejaGestionRendiciones) As ClsRespuestaServidor

        Dim l_Rpt As ClsRespuestaServidor = New ClsRespuestaServidor()
        Dim l_ListaRendiciones As List(Of ClsEnRendicion)

        Try
            l_ListaRendiciones = New List(Of ClsEnRendicion)

            l_Rpt.Resultado = g_DataSesion


        Catch ex As Exception
            l_Rpt.LogError.SetException(ex)
        End Try

        Return l_Rpt
    End Function


End Class

Public Class ClsEnRendicion

    Public nCodigo As Integer
    Public cDescripcion As String
    Public cDesAtendido As String
    Public nNumImporte As Decimal
    Public nDesImporte As String
    Public cDesMoneda As String
    Public dFechaEgreso As String
    Public Sub New()
        nCodigo = 0
        cDescripcion = ""
        cDesAtendido = ""
        nNumImporte = 0
        nDesImporte = ""
        cDesMoneda = ""
        dFechaEgreso = ""
    End Sub

End Class

Public Class ClsEntradaBandejaGestionRendiciones

    Public nTipEstado As Integer
    Public bFlgEstado As String
    Public nCodigo As Integer
    Public cDescripcion As String

    Public Sub New()
        Inicializar()
    End Sub

    Public Sub Inicializar()
        nTipEstado = 0
        bFlgEstado = ""
        nCodigo = 0
        cDescripcion = ""
    End Sub

End Class

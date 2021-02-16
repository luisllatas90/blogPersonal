Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Public Class ClsGenerarEvaluacion

#Region "Capa de Entidades"

    Public Class e_Evaluacion

        Public _tipoOpe As String = ""
        Public _codigo_evl As Integer = -1
        Public _codigo_cco As Integer = -1
        Public _codigo_tev As Integer = -1
        Public _nombre_evl As String = ""
        Public _estadovalidacion_evl As String = "P"
        Public _codigo_per As Integer = -1

        Public _virtual_evl As Boolean = False ' 20201124-ENevado

        Public _leInsert As New List(Of e_EvaluacionDetalle)
        Public _leEdit As New List(Of e_EvaluacionDetalle)
        Public _leDelete As New List(Of e_EvaluacionDetalle)

    End Class

    Public Class e_EvaluacionDetalle

        Public _tipoOpe As String = ""
        Public _codigo_evd As Integer = -1
        Public _codigo_evl As Integer = -1
        Public _codigo_prv As Integer = -1
        Public _orden_evd As Integer = -1
        Public _estadovalidacion_evd As String = "P"
        Public _codigo_per As Integer = -1

        Public _ruta As String = "" ' 20201130-ENevado
        Public _idArchivo As Integer = -1 ' 20201130-ENevado

        Public _oeObservacion As e_EvaluacionDetalle_Observacion

    End Class

    Public Class e_EvaluacionDetalle_Observacion

        Public _tipoOpe As String = ""
        Public _codigo_edo As Integer = -1
        Public _codigo_evd As Integer = -1
        Public _descripcion_edo As String = ""
        Public _codigo_per As Integer = -1

    End Class

#End Region

#Region "Capa de Datos"

    Public Class d_Evaluacion

        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_Evaluacion) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Evaluacion_Listar", ._tipoOpe, ._codigo_evl, ._codigo_cco, ._codigo_tev)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Insertar(ByVal obj As e_Evaluacion) As System.Data.DataTable
            Dim odEvalDetalle As New d_EvaluacionDetalle
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Evaluacion_Insertar", _
                                            ._codigo_cco, _
                                            ._codigo_tev, _
                                            ._nombre_evl, _
                                            ._codigo_per, _
                                            ._virtual_evl) ' 20201124-ENevado
                    If dt.Rows.Count > 0 Then
                        If ._leInsert.Count > 0 Then
                            For Each oeInsert As e_EvaluacionDetalle In ._leInsert
                                oeInsert._codigo_evl = dt.Rows(0).Item(0)
                                odEvalDetalle.fc_Insertar(oeInsert)
                            Next
                        End If
                    End If
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Actualizar(ByVal obj As e_Evaluacion) As System.Data.DataTable
            Dim odEvalDetalle As New d_EvaluacionDetalle
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Evaluacion_Actualizar", _
                                            ._codigo_evl, _
                                            ._nombre_evl, _
                                            ._estadovalidacion_evl, _
                                            ._codigo_per, _
                                            ._virtual_evl) ' 20201124-ENevado
                    If dt.Rows.Count > 0 Then
                        If ._leInsert.Count > 0 Then
                            For Each oeInsert As e_EvaluacionDetalle In ._leInsert
                                oeInsert._codigo_evl = dt.Rows(0).Item(0)
                                odEvalDetalle.fc_Insertar(oeInsert)
                            Next
                        End If
                        If ._leEdit.Count > 0 Then
                            For Each oeEdit As e_EvaluacionDetalle In ._leEdit
                                oeEdit._codigo_evl = dt.Rows(0).Item(0)
                                odEvalDetalle.fc_Actualizar(oeEdit)
                            Next
                        End If
                        If ._leDelete.Count > 0 Then
                            For Each oeDelete As e_EvaluacionDetalle In ._leDelete
                                oeDelete._codigo_evl = dt.Rows(0).Item(0)
                                odEvalDetalle.fc_Eliminar(oeDelete)
                            Next
                        End If
                    End If
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Eliminar(ByVal obj As e_Evaluacion) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Evaluacion_Eliminar", ._codigo_evl, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

    Public Class d_EvaluacionDetalle

        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_EvaluacionDetalle) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_EvaluacionDetalle_Listar", ._tipoOpe, ._codigo_evd, ._codigo_evl, ._codigo_prv, ._orden_evd)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Insertar(ByVal obj As e_EvaluacionDetalle) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_EvaluacionDetalle_Insertar", ._codigo_evl, ._codigo_prv, ._orden_evd, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Actualizar(ByVal obj As e_EvaluacionDetalle) As System.Data.DataTable
            Dim odObservacion As New d_EvaluacionDetalle_Observacion
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_EvaluacionDetalle_Actualizar", ._codigo_evd, ._codigo_prv, ._estadovalidacion_evd, ._codigo_per)
                    If dt.Rows.Count > 0 Then
                        If ._estadovalidacion_evd = "O" Then
                            If Not (._oeObservacion Is Nothing) Then
                                If ._oeObservacion._codigo_edo = -1 Then
                                    odObservacion.fc_Insertar(._oeObservacion)
                                Else
                                    odObservacion.fc_Actualizar(._oeObservacion)
                                End If

                            End If
                        End If
                    End If
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Eliminar(ByVal obj As e_EvaluacionDetalle) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_EvaluacionDetalle_Eliminar", ._codigo_evd, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_CargarExcelPreguntas(ByVal obj As e_EvaluacionDetalle) As Dictionary(Of String, String)
            Dim lo_Resultado As New Dictionary(Of String, String)
            Try
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSATIMPORT").ToString
                cnx.AbrirConexion()
                Dim lo_Salida As Object() = cnx.Ejecutar("ADM_ProcesarPreguntas_Test", _
                                                         obj._codigo_evl, _
                                                         obj._ruta, _
                                                         obj._idArchivo, _
                                                         obj._codigo_per, _
                                                         0, _
                                                         "")
                lo_Resultado.Item("rpta") = lo_Salida(0)
                lo_Resultado.Item("msg") = lo_Salida(1)
            Catch ex As Exception
                lo_Resultado.Item("rpta") = "-1"
                lo_Resultado.Item("msg") = ex.Message
            End Try
            Return lo_Resultado
        End Function


    End Class

    Public Class d_EvaluacionDetalle_Observacion

        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Insertar(ByVal obj As e_EvaluacionDetalle_Observacion) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_EvaluacionDetalle_Observacion_Insertar", ._codigo_evd, ._descripcion_edo, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Actualizar(ByVal obj As e_EvaluacionDetalle_Observacion) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_EvaluacionDetalle_Observacion_Actualizar", ._codigo_edo, ._descripcion_edo, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

#End Region

End Class

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Public Class ClsSistemaEvaluacion

#Region "Capa de Entidades"

    Public Class e_Componente

        Public _tipoOpe As String = ""
        Public _codigo_cmp As Integer = -1
        Public _nombre_cmp As String = ""
        Public _codigo_per As Integer = -1

        Public _codigo_com As String = ""
        Public _codigo_cca As String = ""

    End Class

    Public Class e_CompetenciaAprendizaje

        Public _tipoOpe As String = ""
        Public _codigo_cmp As Integer = -1
        Public _codigo_com As Integer = -1
        Public _nombre_com As String = ""
        Public _abreviatura_com As String = ""
        Public _codigo_per As Integer = -1

    End Class

    Public Class e_Componente_CompetenciaAprendizaje

        Public _tipoOpe As String = ""
        Public _codigo_cca As String = ""
        Public _codigo_cmp As Integer = -1
        Public _codigo_com As String = ""
        Public _codigo_per As Integer = -1

    End Class

    Public Class e_VacantesEvento

        Public _tipoConsulta As String = "GEN"
        Public _operacion As String = "I"
        Public _codigo_vae As String = ""
        Public _codigo_cco As Integer = 0
        Public _codigo_vac As Integer = 0
        Public _cantidad_vae As Integer = 0
        Public _cantidad_accesitarios_vae As Integer = 0
        Public _codigo_per_reg As Integer = 0
        Public _fecha_reg As Object = DBNull.Value
        Public _codigo_per_act As Integer = 0
        Public _fecha_act As Object = DBNull.Value
        Public _cod_usuario As Integer = 0
        'Datos adicionales
        Public _codigo_cac As Integer = 0
        Public _codigo_cpf As Integer = 0
        Public _codigo_min As Integer = 0

    End Class

    Public Class e_SubCompetencia

        Public _tipoOpe As String = ""
        Public _codigo_com As Integer = -1
        Public _codigo_scom As Integer = -1
        Public _nombre_scom As String = ""
        Public _codigo_per As Integer = -1

    End Class

    Public Class e_Indicador

        Public _tipoOpe As String = ""
        Public _codigo_scom As Integer = -1
        Public _codigo_ind As Integer = -1
        Public _nombre_ind As String = ""
        Public _descripcion_ind As String = ""
        Public _codigo_per As Integer = -1

    End Class

    Public Class e_PesoCompetencia

        Public _tipoOpe As String = ""
        Public _codigo_pcom As Integer = -1
        Public _codigo_cac As Integer = -1
        Public _codigo_cpf As Integer = -1
        Public _codigo_com As Integer = -1
        Public _peso_pcom As Decimal = 0.0
        Public _codigo_per As Integer = -1

        Public _codigo_cac_import As Integer = -1
        Public _aplicar_facultad As Boolean = False

    End Class

    Public Class e_TipoEvaluacion

        Public _tipoOpe As String = ""
        Public _codigo_tev As Integer = -1
        Public _nombre_tev As String = ""
        Public _peso_basica_tev As Decimal = 0
        Public _peso_intermedia_tev As Decimal = 0
        Public _peso_avanzada_tev As Decimal = 0
        Public _codigo_per As Integer = -1

        Public _virtual_tev As Boolean = False '20201123-ENevado

        Public _leInsert As List(Of e_TipoEvaluacion_Indicador)
        Public _leEdit As List(Of e_TipoEvaluacion_Indicador)
        Public _leDelet As List(Of e_TipoEvaluacion_Indicador)

    End Class

    Public Class e_TipoEvaluacion_Indicador

        Public _tipoOpe As String = ""
        Public _codigo_tei As Integer = -1
        Public _codigo_ind As Integer = -1
        Public _codigo_tev As Integer = -1
        Public _cantidad_preguntas_tev As Integer = -1
        Public _codigo_per As Integer = -1

    End Class

    Public Class e_ConfiguracionEvaluacionEvento

        Public _tipoOpe As String = ""
        Public _codigo_cee As Integer = -1
        Public _codigo_cco As Integer = -1
        Public _codigo_cpf As String = "-1"
        Public _codigo_tev As Integer = -1
        Public _cantidad_cee As Integer = -1
        Public _codigo_per As Integer = -1

        Public _codigo_ceep As String = ""

        Public _leInsert As List(Of e_ConfiguracionEvaluacionEvento_Peso)
        Public _leEdit As List(Of e_ConfiguracionEvaluacionEvento_Peso)

    End Class

    Public Class e_ConfiguracionEvaluacionEvento_Peso

        Public _tipoOPe As String = ""
        Public _codigo_cee As Integer = -1
        Public _codigo_ceep As Integer = -1
        Public _nro_orden_ceep As Integer = -1
        Public _peso_ceep As Decimal = 0.0
        Public _codigo_per As Integer = -1

    End Class

    Public Class e_DatosEventoAdmision

        Public _tipoConsulta As String = "GEN"
        Public _operacion As String = "I"
        Public _codigo_dea As String = ""
        Public _codigo_cco As Integer = 0
        Public _codigo_cac As Integer = 0
        Public _fechaEvento_dea As Object = DBNull.Value
        Public _codigo_per_reg As Integer = 0
        Public _fecha_reg As Object = DBNull.Value
        Public _codigo_per_act As Integer = 0
        Public _fecha_act As Object = DBNull.Value
        Public _cod_usuario As Integer = 0

    End Class

    Public Class e_ConfiguracionNotaMinima

        Public _tipoConsulta As String = "GEN"
        Public _operacion As String = "I"
        Public _codigo_cnm As String = ""
        Public _codigo_cpf As Integer = 0
        Public _codigo_cco As Integer = 0
        Public _nota_min_cnm As Decimal = 0.0
        Public _codigo_per_reg As Integer = 0
        Public _fecha_reg As Object = DBNull.Value
        Public _codigo_per_act As Integer = 0
        Public _fecha_act As Object = DBNull.Value
        Public _cod_usuario As Integer = 0

        'Datos adicionales
        Public _codigo_test As Integer = 2
        Public _nota_min_competencia As Decimal = 0.0

    End Class

    Public Class e_ConfiguracionNotaMinimaCompetencia

        Public _tipoConsulta As String = "GEN"
        Public _operacion As String = "I"
        Public _codigo_cnc As String = ""
        Public _codigo_cnm As Integer = 0
        Public _codigo_com As Integer = 0
        Public _nota_min_cnc As Integer = 0
        Public _codigo_per_reg As Integer = 0
        Public _fecha_reg As Object = DBNull.Value
        Public _codigo_per_act As Integer = 0
        Public _fecha_act As Object = DBNull.Value
        Public _cod_usuario As Integer = 0

        'Datos Adicionales
        Public _codigo_cpf As Integer = 0
    End Class

#End Region

#Region "Capa de Datos"

    Public Class d_Componente

        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_Componente) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Componente_Listar", ._tipoOpe, ._codigo_cmp)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Insertar(ByVal obj As e_Componente) As System.Data.DataTable
            Try
                Dim oeCCA As New e_Componente_CompetenciaAprendizaje, odCCA As New d_Componente_CompetenciaAprendizaje
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Componente_Insertar", ._nombre_cmp, ._codigo_per)
                    If dt.Rows.Count > 0 Then
                        oeCCA._codigo_cmp = dt.Rows(0).Item(0)
                        oeCCA._codigo_com = ._codigo_com
                        oeCCA._codigo_per = ._codigo_per
                        odCCA.fc_Insertar(oeCCA)
                    End If
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Actualizar(ByVal obj As e_Componente) As System.Data.DataTable
            Try
                Dim oeCCA As New e_Componente_CompetenciaAprendizaje, odCCA As New d_Componente_CompetenciaAprendizaje
                Dim _next As Boolean = False
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Componente_Actualizar", ._codigo_cmp, ._nombre_cmp, ._codigo_per)
                    If dt.Rows.Count > 0 Then
                        If ._codigo_cca.Length > 0 Then
                            oeCCA = New e_Componente_CompetenciaAprendizaje
                            oeCCA._codigo_cca = ._codigo_cca
                            oeCCA._codigo_per = ._codigo_per
                            odCCA.fc_Eliminar(oeCCA)
                        End If
                        If ._codigo_com.Length > 0 Then
                            oeCCA = New e_Componente_CompetenciaAprendizaje
                            oeCCA._codigo_cmp = dt.Rows(0).Item(0)
                            oeCCA._codigo_com = ._codigo_com
                            oeCCA._codigo_per = ._codigo_per
                            odCCA.fc_Insertar(oeCCA)
                        End If
                    End If
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Eliminar(ByVal obj As e_Componente) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Componente_Eliminar", ._codigo_cmp, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

    Public Class d_CompetenciaAprendizaje

        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_CompetenciaAprendizaje) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Competencia_Listar", ._tipoOpe, ._codigo_cmp, ._codigo_com)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Insertar(ByVal obj As e_CompetenciaAprendizaje) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Competencia_Insertar", ._nombre_com, ._abreviatura_com, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Actualizar(ByVal obj As e_CompetenciaAprendizaje) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Competencia_Actualizar", ._codigo_com, ._nombre_com, ._abreviatura_com, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

    Public Class d_Componente_CompetenciaAprendizaje

        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Insertar(ByVal obj As e_Componente_CompetenciaAprendizaje) As System.Data.DataTable
            Try
                Dim codigo_com() As String
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    codigo_com = ._codigo_com.Split(",")
                    For i As Integer = 0 To codigo_com.Length - 1
                        dt = cnx.TraerDataTable("ADM_Componente_CompetenciaAprendizaje_Insertar", ._codigo_cmp, codigo_com(i), ._codigo_per)
                    Next
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Eliminar(ByVal obj As e_Componente_CompetenciaAprendizaje) As System.Data.DataTable
            Try
                Dim codigo_cca() As String
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    codigo_cca = ._codigo_cca.Split(",")
                    For i As Integer = 0 To codigo_cca.Length - 1
                        dt = cnx.TraerDataTable("ADM_Componente_CompetenciaAprendizaje_Eliminar", codigo_cca(i), ._codigo_per)
                    Next
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

    Public Class d_SubCompetencia

        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_SubCompetencia) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_SubCompetencia_Listar", ._tipoOpe, ._codigo_com, ._codigo_scom)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Insertar(ByVal obj As e_SubCompetencia) As System.Data.DataTable
            Try
                'Dim oeCCA As New e_Componente_CompetenciaAprendizaje, odCCA As New d_Componente_CompetenciaAprendizaje
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_SubCompetencia_Insertar", ._codigo_com, ._nombre_scom, ._codigo_per)
                    'If dt.Rows.Count > 0 Then
                    '    oeCCA._codigo_cmp = dt.Rows(0).Item(0)
                    '    oeCCA._codigo_com = ._codigo_com
                    '    oeCCA._codigo_per = ._codigo_per
                    '    odCCA.fc_Insertar(oeCCA)
                    'End If
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Actualizar(ByVal obj As e_SubCompetencia) As System.Data.DataTable
            Try
                'Dim oeCCA As New e_Componente_CompetenciaAprendizaje, odCCA As New d_Componente_CompetenciaAprendizaje
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_SubCompetencia_Actualizar", ._codigo_scom, ._nombre_scom, ._codigo_per)
                    'If dt.Rows.Count > 0 Then
                    '    oeCCA._codigo_cmp = dt.Rows(0).Item(0)
                    '    oeCCA._codigo_com = ._codigo_com
                    '    oeCCA._codigo_per = ._codigo_per
                    '    odCCA.fc_Insertar(oeCCA)
                    'End If
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Eliminar(ByVal obj As e_SubCompetencia) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_SubCompetencia_Eliminar", ._codigo_scom, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

    Public Class d_Indicador

        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_Indicador) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Indicador_Listar", ._tipoOpe, ._codigo_scom, ._codigo_ind)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Insertar(ByVal obj As e_Indicador) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Indicador_Insertar", ._codigo_scom, ._nombre_ind, ._descripcion_ind, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Actualizar(ByVal obj As e_Indicador) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Indicador_Actualizar", ._codigo_ind, ._nombre_ind, ._descripcion_ind, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Eliminar(ByVal obj As e_Indicador) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Indicador_Eliminar", ._codigo_ind, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

    Public Class d_VacantesEvento
        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_VacantesEvento) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_VacantesEvento_Listar" _
                                            , ._tipoConsulta _
                                            , ._codigo_vae _
                                            , ._codigo_cco _
                                            , ._codigo_vac _
                                            , ._cantidad_vae _
                                            , ._cantidad_accesitarios_vae _
                                            , ._codigo_per_reg _
                                            , ._fecha_reg _
                                            , ._codigo_per_act _
                                            , ._fecha_act _
                                            , ._codigo_cac _
                                            , ._codigo_cpf _
                                            , ._codigo_min _
                                            )
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_IUD(ByVal obj As e_VacantesEvento) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New System.Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object()
                With obj
                    lo_Salida = cnx.Ejecutar("ADM_VacantesEvento_IUD", _
                                        ._operacion, _
                                        ._codigo_vae, _
                                        ._codigo_cco, _
                                        ._codigo_vac, _
                                        ._cantidad_vae, _
                                        ._cantidad_accesitarios_vae, _
                                        ._cod_usuario, _
                                        "0", "", "0")
                End With

                cnx.TerminarTransaccion()

                lo_Resultado.Item("rpta") = lo_Salida(0)
                lo_Resultado.Item("msg") = lo_Salida(1)
                lo_Resultado.Item("cod") = lo_Salida(2)

                Return lo_Resultado
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function

        'Otras funciones
        Public Function fc_ListarCicloAcademico() As Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                dt = cnx.TraerDataTable("ConsultarCicloAcademico", "TO", "")
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_ListarCarreraProfesional() As Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                dt = cnx.TraerDataTable("ConsultarCarreraProfesional", "TO", 2)
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_ListarModalidadIngreso() As Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                dt = cnx.TraerDataTable("ConsultarModalidadIngreso", "TO", "")
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_ListarCentroCostos(ByVal tipoOperacion As String, ByVal codigoCac As Integer, ByVal codUsuario As Integer) As Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                dt = cnx.TraerDataTable("ADM_ConsultarEventoAdmision", tipoOperacion, codigoCac, "", 1, 1, codUsuario)
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function
    End Class

    Public Class d_DatosEventoAdmision
        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_IUD(ByVal obj As e_DatosEventoAdmision) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object()
                With obj
                    lo_Salida = cnx.Ejecutar("ADM_DatosEventoAdmision_IUD", _
                                        ._operacion, _
                                        ._codigo_dea, _
                                        ._codigo_cco, _
                                        ._codigo_cac, _
                                        ._fechaEvento_dea, _
                                        ._cod_usuario, _
                                        "0", "", "0")
                End With

                cnx.TerminarTransaccion()

                lo_Resultado.Item("rpta") = lo_Salida(0)
                lo_Resultado.Item("msg") = lo_Salida(1)
                lo_Resultado.Item("cod") = lo_Salida(2)

                Return lo_Resultado
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function
    End Class

    Public Class d_PesoCompetencia

        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_PesoCompetencia) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_PesoCompetencia_Listar", ._tipoOpe, ._codigo_cac, ._codigo_cpf, ._codigo_com, ._codigo_pcom)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Insertar(ByVal obj As e_PesoCompetencia) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_PesoCompetencia_Insertar", ._codigo_cac, ._codigo_cpf, ._codigo_com, ._peso_pcom, ._aplicar_facultad, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Actualizar(ByVal obj As e_PesoCompetencia) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_PesoCompetencia_Actualizar", ._codigo_pcom, ._peso_pcom, ._aplicar_facultad, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Eliminar(ByVal obj As e_PesoCompetencia) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_PesoCompetencia_Eliminar", ._codigo_pcom, ._aplicar_facultad, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Importar(ByVal obj As e_PesoCompetencia) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_PesoCompetencia_Importar", ._codigo_cac, ._codigo_cac_import, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

    Public Class d_TipoEvaluacion

        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_TipoEvaluacion) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_TipoEvaluacion_Listar", ._tipoOpe, ._codigo_tev)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Insertar(ByVal obj As e_TipoEvaluacion) As System.Data.DataTable
            Dim odTipoEva_Indicador As New d_TipoEvaluacion_Indicador
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_TipoEvaluacion_Insertar", _
                                            ._nombre_tev, _
                                            ._peso_basica_tev, _
                                            ._peso_intermedia_tev, _
                                            ._peso_avanzada_tev, _
                                            ._codigo_per, _
                                            ._virtual_tev) '20201123-ENevado
                    If dt.Rows.Count > 0 Then
                        If ._leInsert.Count > 0 Then
                            For Each oeInsert As e_TipoEvaluacion_Indicador In ._leInsert
                                oeInsert._codigo_tev = dt.Rows(0).Item(0)
                                odTipoEva_Indicador.fc_Insertar(oeInsert)
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

        Public Function fc_Actualizar(ByVal obj As e_TipoEvaluacion) As System.Data.DataTable
            Dim odTipoEva_Indicador As New d_TipoEvaluacion_Indicador
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_TipoEvaluacion_Actualizar", _
                                            ._codigo_tev, _
                                            ._nombre_tev, _
                                            ._peso_basica_tev, _
                                            ._peso_intermedia_tev, _
                                            ._peso_avanzada_tev, _
                                            ._codigo_per, _
                                            ._virtual_tev) '20201123-ENevado
                    If dt.Rows.Count > 0 Then
                        If ._leInsert.Count > 0 Then
                            For Each oeInsert As e_TipoEvaluacion_Indicador In ._leInsert
                                oeInsert._codigo_tev = dt.Rows(0).Item(0)
                                odTipoEva_Indicador.fc_Insertar(oeInsert)
                            Next
                        End If
                        If ._leEdit.Count > 0 Then
                            For Each oeEdit As e_TipoEvaluacion_Indicador In ._leEdit
                                oeEdit._codigo_tev = dt.Rows(0).Item(0)
                                odTipoEva_Indicador.fc_Actualizar(oeEdit)
                            Next
                        End If
                        If ._leDelet.Count > 0 Then
                            For Each oeDelet As e_TipoEvaluacion_Indicador In ._leDelet
                                oeDelet._codigo_tev = dt.Rows(0).Item(0)
                                odTipoEva_Indicador.fc_Eliminar(oeDelet)
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

        Public Function fc_Eliminar(ByVal obj As e_TipoEvaluacion) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_TipoEvaluacion_Eliminar", ._codigo_tev, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

    Public Class d_TipoEvaluacion_Indicador

        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_TipoEvaluacion_Indicador) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_TipoEvaluacion_Indicador_Listar", ._tipoOpe, ._codigo_tev, ._codigo_ind, ._codigo_tei)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Insertar(ByVal obj As e_TipoEvaluacion_Indicador) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_TipoEvaluacion_Indicador_Insertar", ._codigo_tev, ._codigo_ind, ._cantidad_preguntas_tev, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Actualizar(ByVal obj As e_TipoEvaluacion_Indicador) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_TipoEvaluacion_Indicador_Actualizar", ._codigo_tei, ._cantidad_preguntas_tev, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Eliminar(ByVal obj As e_TipoEvaluacion_Indicador) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_TipoEvaluacion_Indicador_Eliminar", ._codigo_tei, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

    Public Class d_ConfiguracionEvaluacionEvento

        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_ConfiguracionEvaluacionEvento) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_ConfiguracionEvaluacionEvento_Listar", ._tipoOpe, ._codigo_cco, ._codigo_cpf, ._codigo_tev, ._codigo_cee)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Insertar(ByVal obj As e_ConfiguracionEvaluacionEvento) As System.Data.DataTable
            Dim odConfigEvalPeso As New d_ConfiguracionEvaluacionEvento_Peso
            Dim codigo_cpf() As String
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    codigo_cpf = ._codigo_cpf.Split(",")
                    For i As Integer = 0 To codigo_cpf.Length - 1
                        dt = cnx.TraerDataTable("ADM_ConfiguracionEvaluacionEvento_Insertar", ._codigo_cco, codigo_cpf(i), ._codigo_tev, ._cantidad_cee, ._codigo_per)
                        If dt.Rows.Count > 0 Then
                            If ._leInsert.Count > 0 Then
                                For Each oeInsert As e_ConfiguracionEvaluacionEvento_Peso In ._leInsert
                                    oeInsert._codigo_cee = dt.Rows(0).Item(0)
                                    odConfigEvalPeso.fc_Insertar(oeInsert)
                                Next
                            End If
                        End If
                    Next
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Actualizar(ByVal obj As e_ConfiguracionEvaluacionEvento) As System.Data.DataTable
            Dim oeConfigEvalPeso As e_ConfiguracionEvaluacionEvento_Peso, odConfigEvalPeso As New d_ConfiguracionEvaluacionEvento_Peso
            Try
                Dim codigo_ceep() As String
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    codigo_ceep = ._codigo_ceep.Split(",")
                    dt = cnx.TraerDataTable("ADM_ConfiguracionEvaluacionEvento_Actualizar", ._codigo_cee, ._cantidad_cee, ._codigo_per)
                    If dt.Rows.Count > 0 Then
                        If ._leInsert.Count > 0 Then
                            For Each oeInsert As e_ConfiguracionEvaluacionEvento_Peso In ._leInsert
                                oeInsert._codigo_cee = dt.Rows(0).Item(0)
                                odConfigEvalPeso.fc_Insertar(oeInsert)
                            Next
                        End If
                        If ._leEdit.Count > 0 Then
                            For Each oeEdit As e_ConfiguracionEvaluacionEvento_Peso In ._leEdit
                                oeEdit._codigo_cee = dt.Rows(0).Item(0)
                                odConfigEvalPeso.fc_Actualizar(oeEdit)
                            Next
                        End If
                        If codigo_ceep.Length > 0 Then
                            For i As Integer = 0 To codigo_ceep.Length - 1
                                If codigo_ceep(i) <> "" Then
                                    oeConfigEvalPeso = New e_ConfiguracionEvaluacionEvento_Peso
                                    oeConfigEvalPeso._codigo_ceep = codigo_ceep(i)
                                    oeConfigEvalPeso._codigo_per = ._codigo_per
                                    odConfigEvalPeso.fc_Eliminar(oeConfigEvalPeso)
                                End If
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

        Public Function fc_Eliminar(ByVal obj As e_ConfiguracionEvaluacionEvento) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_ConfiguracionEvaluacionEvento_Eliminar", ._codigo_cee, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

    Public Class d_ConfiguracionEvaluacionEvento_Peso

        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_ConfiguracionEvaluacionEvento_Peso) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_ConfiguracionEvaluacionEvento_Peso_Listar", ._tipoOPe, ._codigo_cee, ._codigo_ceep)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Insertar(ByVal obj As e_ConfiguracionEvaluacionEvento_Peso) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_ConfiguracionEvaluacionEvento_Peso_Insertar", ._codigo_cee, ._nro_orden_ceep, ._peso_ceep, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Actualizar(ByVal obj As e_ConfiguracionEvaluacionEvento_Peso) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_ConfiguracionEvaluacionEvento_Peso_Actualizar", ._codigo_ceep, ._peso_ceep, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_Eliminar(ByVal obj As e_ConfiguracionEvaluacionEvento_Peso) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_ConfiguracionEvaluacionEvento_Peso_Eliminar", ._codigo_ceep, ._codigo_per)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

    Public Class d_ConfiguracionNotaMinima
        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_ConfiguracionNotaMinima) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Configuracion_NotaMinima_Listar" _
                                            , ._tipoConsulta _
                                            , ._codigo_cnm _
                                            , ._codigo_cpf _
                                            , ._codigo_cco _
                                            , ._nota_min_cnm _
                                            , ._codigo_per_reg _
                                            , ._fecha_reg _
                                            , ._codigo_per_act _
                                            , ._fecha_act _
                                            , ._codigo_test _
                                            )
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_IUD(ByVal obj As e_ConfiguracionNotaMinima) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object()
                With obj
                    lo_Salida = cnx.Ejecutar("ADM_Configuracion_NotaMinima_IUD", _
                                        ._operacion, _
                                        ._codigo_cnm, _
                                        ._codigo_cpf, _
                                        ._codigo_cco, _
                                        ._nota_min_cnm, _
                                        ._cod_usuario, _
                                        "0", "", "0")
                End With

                cnx.TerminarTransaccion()

                lo_Resultado.Item("rpta") = lo_Salida(0)
                lo_Resultado.Item("msg") = lo_Salida(1)
                lo_Resultado.Item("cod") = lo_Salida(2)

                Return lo_Resultado
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function

        Public Function fc_AsignarNotasMinimasPorDefecto(ByVal obj As e_ConfiguracionNotaMinima) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object()
                With obj
                    lo_Salida = cnx.Ejecutar("ADM_AsignarNotasMinimasPorDefecto", _
                                        ._codigo_test, _
                                        ._codigo_cco, _
                                        ._codigo_cpf, _
                                        ._nota_min_cnm, _
                                        ._nota_min_competencia, _
                                        ._cod_usuario, _
                                        "0", "")
                End With

                cnx.TerminarTransaccion()

                lo_Resultado.Item("rpta") = lo_Salida(0)
                lo_Resultado.Item("msg") = lo_Salida(1)

                Return lo_Resultado
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function

    End Class

    Public Class d_ConfiguracionNotaMinimaCompetencia
        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_ConfiguracionNotaMinimaCompetencia) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Configuracion_NotaMinima_Competencia_Listar" _
                                            , ._tipoConsulta _
                                            , ._codigo_cnc _
                                            , ._codigo_cnm _
                                            , ._codigo_com _
                                            , ._nota_min_cnc _
                                            , ._codigo_per_reg _
                                            , ._fecha_reg _
                                            , ._codigo_per_act _
                                            , ._fecha_act _
                                            , ._codigo_cpf _
                                            )
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_IUD(ByVal obj As e_ConfiguracionNotaMinimaCompetencia) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object()
                With obj
                    lo_Salida = cnx.Ejecutar("ADM_Configuracion_NotaMinima_Competencia_IUD", _
                                        ._operacion, _
                                        ._codigo_cnc, _
                                        ._codigo_cnm, _
                                        ._codigo_com, _
                                        ._nota_min_cnc, _
                                        ._cod_usuario, _
                                        "0", "", "0")
                End With

                cnx.TerminarTransaccion()

                lo_Resultado.Item("rpta") = lo_Salida(0)
                lo_Resultado.Item("msg") = lo_Salida(1)
                lo_Resultado.Item("cod") = lo_Salida(2)

                Return lo_Resultado
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function

    End Class

#End Region

End Class

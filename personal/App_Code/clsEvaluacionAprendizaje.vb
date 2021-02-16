Imports Microsoft.VisualBasic
Imports System.Collections.Generic

#Region "Entidades"

''' <summary>
''' Clase Entidad Curso Programado
''' </summary>
''' <remarks></remarks>
Public Class e_CursoProgramado

    Public TipoOperacion As String
    Public codigo_cup As Integer = -1
    Public codigo_cac As Integer
    Public codigo_pes As Integer
    Public codigo_cur As Integer = -1
    Public estado_sil As String

    Public codigo_cor As Integer = -1
    Public codigo_per As Integer
    Public codigo_ctf As Integer

End Class

''' <summary>
''' Clase Entidad Diseño Asignatura
''' </summary>
''' <remarks></remarks>
Public Class e_DiseñoAsignatura

    Public codigo_dis As Integer
    Public idArchivoCompartido As Integer
    Public idTabla As Integer
    Public codigo_per As Integer
    Public codigo_pes As Integer
    Public codigo_cur As Integer
    Public codigo_cac As Integer

End Class

''' <summary>
''' Clase Entidad Coordinador Asignatura
''' </summary>
''' <remarks></remarks>
Public Class e_CoordinadorAsignatura

    Public TipoOperacion As String
    Public codigo_coo As Integer = -1
    Public codigo_cac As Integer
    Public codigo_cur As Integer = -1
    Public codigo_pes As Integer = -1
    Public codigo_cup As Integer = -1
    Public codigo_cpf As Integer
    Public codigo_tc As Integer = -1
    Public codigo_ctf As Integer = -1
    Public codigo_per As Integer
    Public codigo_per_reg As Integer
    Public indicador_coo As Integer = 0
    Public creditos_cur As Integer = 0 '--> Por Luis Q.T. 16DIC2020

End Class

Public Class e_CicloAcademico_Norma

    Public TipoOperacion As String = ""
    Public codigo_nor As Integer
    Public codigo_conf As Integer
    Public descripcion_nor As String
    Public estado_nor As Integer
    Public codigo_per As Integer

End Class

Public Class e_CicloAcademico_Conf

    Public TipoOperacion As String = ""
    Public codigo_conf As Integer
    Public codigo_cac As Integer
    Public codigo_test As Integer
    Public nombre_conf As String
    Public valor_conf As Integer
    Public codigo_per As Integer

End Class

Public Class e_CicloAcademico_TipoEstudio

    Public tipo As String = ""
    Public codigo_ctest As Integer = -1
    Public codigo_cac As Integer = -1
    Public codigo_test As Integer = -1
    Public vigente_cte As Boolean
    Public admision_cte As Boolean
    Public fechainicio_cte As Date
    Public fechafin_cte As Date
    Public codigo_per As Integer
    Public fecini_adm_cte As Date
    Public fecfin_adm_cte As Date

End Class

Public Class e_SuspensionPorHoras

    Public tipo As String = ""
    Public codigo_sph As Integer = -1
    Public descripcion_sph As String = ""
    Public fecha_sph As Date = #1/1/1901#
    Public horaInicio_sph As String = "00:00"
    Public horaFin_sph As String = "00:00"
    Public codigo_per As Integer = 684
    Public año As Integer = -1

End Class


Public Class e_CicloAcademicoFechas
    '20200706 JQuepuy
    Public codigo_cac As Integer
    Public fecha_ini As String
    Public fecha_fin As String

End Class

#End Region

#Region "Datos"

''' <summary>
''' Clase Dato Curso Programado
''' </summary>
''' <remarks></remarks>
Public Class d_CursoProgramado

    Private cnx As ClsConectarDatos
    Private dt As System.Data.DataTable

    ''' <summary>
    ''' Función para listar Curso Programado en Publicar Silabos
    ''' </summary>
    ''' <param name="obj">Clase Entidad Curso Programado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function fc_ListarCursoProgramado(ByVal obj As e_CursoProgramado) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("CursoProgramado_Listar", .TipoOperacion, .codigo_cup, .codigo_pes, .codigo_cur, .codigo_cac, .codigo_cor, .codigo_ctf)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Función para actualizar curso programado
    ''' </summary>
    ''' <param name="obj">Clase Entidad Curso Programado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function fc_ActualizarCursoProgramado(ByVal obj As e_CursoProgramado) As Boolean
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                cnx.Ejecutar("DEA_CursoProgramado_Actualizar", .TipoOperacion, .codigo_cup, .estado_sil, .codigo_per)
            End With
            cnx.CerrarConexion()
            Return True
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

End Class

''' <summary>
''' Clase Dato Diseño Asignatura
''' </summary>
''' <remarks></remarks>
Public Class d_DiseñoAsignatura

    Private cnx As ClsConectarDatos
    Private dt As System.Data.DataTable

    ''' <summary>
    ''' Función para Listar Archivos de Anexo del Diseño de Asignatura
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function fc_ListarArchivoAnexo(ByVal obj As e_DiseñoAsignatura) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("COM_ObtenerAnexoAsignatura", .codigo_dis, .idTabla)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Función para Eliminar el Archivo de Anexo del Diseño de Asignatura
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function fc_EliminarArchivoAnexo(ByVal obj As e_DiseñoAsignatura) As Boolean
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                cnx.Ejecutar("DiseñoAsignatura_QuitarAnexo", .codigo_dis)
            End With
            cnx.CerrarConexion()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Función para Registrar un Archivo de Anexo del Diseño de Asignatura
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function fc_RegistrarArchivoAnexo(ByVal obj As e_DiseñoAsignatura) As Boolean
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                cnx.Ejecutar("DiseñoAsignatura_SubirAnexo", .idTabla, .codigo_dis)
            End With
            cnx.CerrarConexion()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fc_DesaprobarDiseñoAsignatura(ByVal obj As e_DiseñoAsignatura) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("DEA_DiseñoAsignatura_desaprobar", .codigo_dis, .codigo_cac, .codigo_pes, .codigo_cur, .codigo_per)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Función que Anula el Diseño de Asignatura Actual
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function fc_AnularDiseñoAsignatura(ByVal obj As e_DiseñoAsignatura) As System.Data.DataTable '--> Por Luis Q.T. | 15DIC2020
        dt = New System.Data.DataTable

        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()

            With obj
                dt = cnx.TraerDataTable("DEA_DiseñoAsignatura_anular", .codigo_cac, .codigo_dis, .codigo_per)
            End With
            cnx.CerrarConexion()

        Catch ex As Exception
            dt.Rows.Clear() : dt.Columns.Clear()

            dt.Columns.Add("rpta", Type.GetType("System.String"))
            dt.Columns.Add("valor", Type.GetType("System.String"))

            Dim dr As Data.DataRow = dt.NewRow
            dr("rpta") = "0" : dr("valor") = ex.ToString().Replace(vbCr, " ").Replace(vbLf, "")
            dt.Rows.Add(dr)
        End Try

        Return dt
    End Function

End Class

''' <summary>
''' Clase Dato Coordinador Asignatura
''' </summary>
''' <remarks></remarks>
Public Class d_CoordinadorAsignatura

    Private cnx As ClsConectarDatos
    Private dt As System.Data.DataTable

    Public Function fc_ListarCoordinadorAsignatura(ByVal obj As e_CoordinadorAsignatura) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("COM_ListarCoordinadorAsignatura", .TipoOperacion, .codigo_coo, .codigo_cac, .codigo_cur, .codigo_pes, .codigo_cup, .codigo_cpf, .codigo_tc, .codigo_ctf)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fc_RegistrarCoordinadorAsignatura(ByVal obj As e_CoordinadorAsignatura) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("COM_RegistrarCoordinadorAsignatura", .codigo_per, .codigo_cac, .codigo_cur, IIf(.codigo_pes = -2, DBNull.Value, .codigo_pes), .codigo_per_reg, .indicador_coo, .creditos_cur)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fc_ActualizarCoordinadorAsignatura(ByVal obj As e_CoordinadorAsignatura) As Boolean
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                cnx.Ejecutar("COM_ActualizarCoordinadorAsignatura", .codigo_coo, .codigo_per, .codigo_cac, .codigo_cur, IIf(.codigo_pes = -2, DBNull.Value, .codigo_pes), .codigo_per_reg, .indicador_coo, .creditos_cur)
            End With
            cnx.CerrarConexion()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_CicloAcademico_Norma

    Private cnx As ClsConectarDatos
    Private dt As System.Data.DataTable

    Public Function fc_ListarCicloAcademico_Normas(ByVal obj As e_CicloAcademico_Norma) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("DEA_CicloAcademicoConf_Normas_listar", .TipoOperacion, .codigo_conf)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fc_RegistrarCicloAcademico_Normas(ByVal obj As e_CicloAcademico_Norma) As Boolean
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                cnx.Ejecutar("DEA_CicloAcademicoConf_Normas_Insertar", .codigo_conf, .descripcion_nor, .codigo_per)
            End With
            cnx.CerrarConexion()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fc_ActualizarCicloAcademico_Normas(ByVal obj As e_CicloAcademico_Norma) As Boolean
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                cnx.Ejecutar("DEA_CicloAcademicoConf_Normas_actualizar", .codigo_nor, .descripcion_nor, .codigo_per)
            End With
            cnx.CerrarConexion()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fc_EliminarCicloAcademico_Normas(ByVal obj As e_CicloAcademico_Norma) As Boolean
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                cnx.Ejecutar("DEA_CicloAcademicoConf_Normas_eliminar", .codigo_nor, .codigo_per)
            End With
            cnx.CerrarConexion()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_CicloAcademico_Conf

    Private cnx As ClsConectarDatos
    Private dt As System.Data.DataTable

    Public Function fc_ListarCicloAcademico_Conf(ByVal obj As e_CicloAcademico_Conf) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("DEA_CicloAcademicoConf_Conf_listar", .TipoOperacion, .codigo_cac, .codigo_test)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_CicloAcademico_TipoEstudio

    Private cnx As ClsConectarDatos
    Private dt As System.Data.DataTable

    Public Function fc_ListarCicloAcademico_TipoEstudio(ByVal obj As e_CicloAcademico_TipoEstudio) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("ACAD_CicloAcademico_TipoEstudio_listar", .tipo, .codigo_ctest, .codigo_cac, .codigo_test)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fc_RegistrarCicloAcademico_TipoEstudio(ByVal obj As e_CicloAcademico_TipoEstudio) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("ACAD_CicloAcademico_TipoEstudio_Insertar", .codigo_cac, .codigo_test, .vigente_cte, .admision_cte, .fechainicio_cte, .fechafin_cte, .fecini_adm_cte, .fecfin_adm_cte, .codigo_per)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fc_HabilitarCicloAcademico_TipoEstudio(ByVal obj As e_CicloAcademico_TipoEstudio) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("ACAD_CicloAcademico_TipoEstudio_Habilitar", .tipo, .codigo_ctest, .codigo_test, .codigo_per)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fc_ActualizarCicloAcademico_TipoEstudio(ByVal obj As e_CicloAcademico_TipoEstudio) As Boolean
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                cnx.Ejecutar("ACAD_CicloAcademico_TipoEstudio_editar", .codigo_ctest, .fechainicio_cte, .fechafin_cte, .fecini_adm_cte, .fecfin_adm_cte, .codigo_per)
            End With
            cnx.CerrarConexion()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_SuspensionPorHoras

    Private cnx As ClsConectarDatos
    Private dt As System.Data.DataTable

    Public Function fc_ListarSuspensionPorHoras(ByVal obj As e_SuspensionPorHoras) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("ACA_SuspensionPorHoras_Listar", .tipo, .codigo_sph, .año)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fc_RegistrarSuspensionPorHoras(ByVal obj As e_SuspensionPorHoras) As Boolean
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                cnx.Ejecutar("ACA_SuspensionPorHoras_insertar", .descripcion_sph, .fecha_sph, .horaInicio_sph, .horaFin_sph, .codigo_per)
            End With
            cnx.CerrarConexion()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fc_ActualizarSuspensionPorHoras(ByVal obj As e_SuspensionPorHoras) As Boolean
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                cnx.Ejecutar("ACA_SuspensionPorHoras_actualizar", .codigo_sph, .descripcion_sph, .fecha_sph, .horaInicio_sph, .horaFin_sph, .codigo_per)
            End With
            cnx.CerrarConexion()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fc_EliminarSuspensionPorHoras(ByVal obj As e_SuspensionPorHoras) As Boolean
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                cnx.Ejecutar("ACA_SuspensionPorHoras_eliminar", .codigo_sph, .codigo_per)
            End With
            cnx.CerrarConexion()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class


Public Class d_CicloAcademicoFechas
    '20200706 JQuepuy
    Private cnx As ClsConectarDatos
    Private dt As System.Data.DataTable


    Public Function fc_ListarCicloAcademico_Fechas(ByVal obj As e_CicloAcademicoFechas) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()

            dt = cnx.TraerDataTable("DEA_CicloAcademicoFechaListar", obj.codigo_cac)

            cnx.CerrarConexion()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function fc_GuardarCicloAcademico_Fechas(ByVal obj As e_CicloAcademicoFechas) As Boolean
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                cnx.Ejecutar("DEA_CicloAcademicoFechaGuardar", .codigo_cac, .fecha_ini, .fecha_fin)
            End With
            cnx.CerrarConexion()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

#End Region







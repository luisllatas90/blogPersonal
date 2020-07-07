Imports Microsoft.VisualBasic
Imports System.Collections.Generic

#Region "Entidades"

Public Class e_GrupoAdmisionVirtual

    Public tipoOperacion As String = ""
    Public codigo_gru As Integer = -1
    Public codigo_cco As String = ""
    Public codigo As String = ""
    Public nombre As String = ""
    Public aulaactiva As Boolean = False
    Public estado As Integer = -1
    Public idcourse As Integer = -1
    Public idcourserole As Integer = -1
    Public idcoursecontext As Integer = -1
    Public codigo_amb As Integer = -1
    Public capacidad As Integer = 0
    Public codigo_per As Integer = -1

End Class

Public Class e_GrupoAdmisionVirtual_Alumno

    Public tipoOperacion As String = ""
    Public codigo_gva As Integer = -1
    Public codigo_gru As Integer = -1
    Public codigo_alu As String = ""
    Public codigo_per As Integer = -1

End Class

Public Class e_GrupoAdmision_CentroCosto

    Public tipoOperacion As String = ""
    Public codigo_gcc As Integer = -1
    Public codigo_gru As Integer = -1
    Public codigo_cco As String = "-1"
    Public codigo_per As Integer = -1
    Public codigo_aux As String = ""

End Class

Public Class e_GrupoAdmision_Responsable

    Public tipoOperacion As String = ""
    Public codigo_gre As Integer = -1
    Public codigo_gru As Integer = -1
    Public codigo_per As String = ""
    Public codigo_usu As Integer = -1
    Public codigo_aux As String = ""

End Class

#End Region

#Region "Datos"

Public Class d_GrupoAdmisionVirtual

    Private cnx As ClsConectarDatos
    Private dt As System.Data.DataTable

    Public Function fc_ListarGrupoAdmisionVirtual(ByVal obj As e_GrupoAdmisionVirtual) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("ADM_GrupoAdmisionVirtual_Listar", .tipoOperacion, .codigo_gru, .codigo_cco)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function fc_RegistrarGrupoAdmisionVirtual(ByVal obj As e_GrupoAdmisionVirtual) As System.Data.DataTable
        Try
            Dim oeGCco As e_GrupoAdmision_CentroCosto
            Dim odGCco As New d_GrupoAdmision_CentroCosto
            'Dim _codigo_cup() As String
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("ADM_GrupoAdmisionVirtual_insertar", -1, .codigo, .nombre, .aulaactiva, .estado, .codigo_amb, .capacidad, .codigo_per)
                If dt.Rows.Count > 0 Then
                    '_codigo_cup = obj.codigo_cco.Split(",")
                    'For i As Integer = 0 To _codigo_cup.Length - 1
                    oeGCco = New e_GrupoAdmision_CentroCosto
                    'oeGCco.codigo_cco = _codigo_cup(i)
                    oeGCco.codigo_cco = obj.codigo_cco
                    oeGCco.codigo_per = obj.codigo_per
                    oeGCco.codigo_gru = dt.Rows(0).Item(0)
                    odGCco.fc_RegistrarGrupoAdmision_CentroCosto(oeGCco)
                    'Next
                End If
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function fc_ActualizarGrupoAdmisionVirtual(ByVal obj As e_GrupoAdmisionVirtual) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("ADM_GrupoAdmisionVirtual_actualizar", .codigo_gru, .codigo_cco, .codigo, .nombre, .aulaactiva, .estado, .codigo_amb, .capacidad, .codigo_per)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function fc_EliminarGrupoAdmisionVirtual(ByVal obj As e_GrupoAdmisionVirtual) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("ADM_GrupoAdmisionVirtual_eliminar", .codigo_gru, .codigo_per)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_GrupoAdmisionVirtual_Alumno

    Private cnx As ClsConectarDatos
    Private dt As System.Data.DataTable

    Public Function fc_ListarGrupoAdmisionVirtual_Alumno(ByVal obj As e_GrupoAdmisionVirtual_Alumno) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("ADM_GrupoAdmisionVirtual_Alumno_Listar", .tipoOperacion, .codigo_gru, .codigo_alu)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function fc_AgregarGrupoAdmisionVirtual_Alumno(ByVal obj As e_GrupoAdmisionVirtual_Alumno) As System.Data.DataTable
        Try
            Dim _codigo_alu() As String
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                _codigo_alu = .codigo_alu.Split(",")
                For i As Integer = 0 To _codigo_alu.Length - 1
                    dt = cnx.TraerDataTable("ADM_GrupoAdmisionVirtual_Alumno_agregar", .codigo_gru, _codigo_alu(i), .codigo_per)
                Next
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function fc_QuitarGrupoAdmisionVirtual_Alumno(ByVal obj As e_GrupoAdmisionVirtual_Alumno) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("ADM_GrupoAdmisionVirtual_Alumno_quitar", .codigo_gva, .codigo_per)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_GrupoAdmision_CentroCosto

    Private cnx As ClsConectarDatos
    Private dt As System.Data.DataTable

    Public Function fc_ListarGrupoAdmision_CentroCosto(ByVal obj As e_GrupoAdmision_CentroCosto) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("ADM_GrupoAdmision_CentroCosto_listar", .tipoOperacion, .codigo_gcc, .codigo_gru, .codigo_cco)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function fc_RegistrarGrupoAdmision_CentroCosto(ByVal obj As e_GrupoAdmision_CentroCosto) As System.Data.DataTable
        Try
            Dim _codigo_cco() As String, _codigo_aux() As String
            Dim _flag As Boolean = False
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()

            With obj
                If .codigo_aux.Length = 0 Then
                    _flag = True
                Else
                    _codigo_aux = .codigo_aux.Split(",")
                    For x As Integer = 0 To _codigo_aux.Length - 1
                        dt = cnx.TraerDataTable("ADM_GrupoAdmision_CentroCosto_quitar", _codigo_aux(x), .codigo_per)
                    Next
                    If dt.Rows.Count > 0 Then
                        _flag = True
                    End If
                End If
                If _flag Then
                    If .codigo_cco.Length > 0 Then
                        _codigo_cco = obj.codigo_cco.Split(",")
                        For i As Integer = 0 To _codigo_cco.Length - 1
                            dt = cnx.TraerDataTable("ADM_GrupoAdmision_CentroCosto_insertar", .codigo_gru, _codigo_cco(i), .codigo_per)
                        Next
                    End If
                End If
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_GrupoAdmision_Responsable

    Private cnx As ClsConectarDatos
    Private dt As System.Data.DataTable

    Public Function fc_ListarGrupoAdmision_Responsable(ByVal obj As e_GrupoAdmision_Responsable) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("ADM_GrupoAdmision_Responsable_listar", .tipoOperacion, .codigo_gre, .codigo_gru, .codigo_per)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

    Public Function fc_RegistrarGrupoAdmision_Responsable(ByVal obj As e_GrupoAdmision_Responsable) As System.Data.DataTable
        Try
            Dim _codigo_per() As String, _codigo_aux() As String
            Dim _flag As Boolean = False
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()

            With obj
                If .codigo_aux.Length = 0 Then
                    _flag = True
                Else
                    _codigo_aux = .codigo_aux.Split(",")
                    For x As Integer = 0 To _codigo_aux.Length - 1
                        dt = cnx.TraerDataTable("ADM_GrupoAdmision_Responsable_quitar", _codigo_aux(x), .codigo_usu)
                    Next
                    If dt.Rows.Count > 0 Then
                        _flag = True
                    End If
                End If
                If _flag Then
                    If .codigo_per.Length > 0 Then
                        _codigo_per = obj.codigo_per.Split(",")
                        For i As Integer = 0 To _codigo_per.Length - 1
                            dt = cnx.TraerDataTable("ADM_GrupoAdmision_Responsable_agregar", .codigo_gru, _codigo_per(i), .codigo_usu)
                        Next
                    End If
                End If
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

End Class


#End Region

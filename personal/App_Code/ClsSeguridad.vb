Imports System.Net
Imports System.IO
Imports System.Drawing
Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

#Region "ENTIDADES"

Public Class e_PermisoAccion

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_pea As String
    Public formulario_pea As String
    Public nombreFormulario_pea As String
    Public accion_pea As String
    Public descripcion_pea As String

    Public codigo_apl As String

    Public cod_user As String
    Public operacion As String

#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_pea = String.Empty
        nombreFormulario_pea = String.Empty
        formulario_pea = String.Empty
        accion_pea = String.Empty
        descripcion_pea = String.Empty

        codigo_apl = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_PermisoAccionFuncion

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_paf As String
    Public codigo_apl As String
    Public codigo_tfu As String
    Public codigo_per As String
    Public codigo_pea As String
    Public temporalidad_paf As String
    Public fechaInicio_paf As Date
    Public fechaFin_paf As Date
    Public verificarRestriccion_paf As String

    Public formulario_pea As String
    Public accion_pea As String
    Public mostrar_permiso As Boolean

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_paf = String.Empty
        codigo_apl = String.Empty
        codigo_tfu = String.Empty
        codigo_per = String.Empty
        codigo_pea = String.Empty
        temporalidad_paf = String.Empty
        fechaInicio_paf = #1/1/1901#
        fechaFin_paf = #1/1/1901#
        verificarRestriccion_paf = String.Empty

        formulario_pea = String.Empty
        accion_pea = String.Empty
        mostrar_permiso = False

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

#End Region

#Region "DATOS"

Public Class d_PermisoAccion
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarPermisoAccion(ByVal le_PermisoAccion As e_PermisoAccion) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("CONF_PermisoAccionListar", le_PermisoAccion.operacion, _
                                    le_PermisoAccion.codigo_pea, _
                                    le_PermisoAccion.formulario_pea, _
                                    le_PermisoAccion.nombreFormulario_pea, _
                                    le_PermisoAccion.accion_pea, _
                                    le_PermisoAccion.codigo_apl)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarPermisoAccion(ByVal le_PermisoAccion As e_PermisoAccion) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("CONF_PermisoAccionIUD", le_PermisoAccion.operacion, _
                                    le_PermisoAccion.cod_user, _
                                    le_PermisoAccion.codigo_pea, _
                                    le_PermisoAccion.formulario_pea, _
                                    le_PermisoAccion.nombreFormulario_pea, _
                                    le_PermisoAccion.accion_pea, _
                                    le_PermisoAccion.descripcion_pea)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetPermisoAccion(ByVal codigo As Integer) As e_PermisoAccion
        Try
            Dim me_PermisoAccion As New e_PermisoAccion

            If codigo > 0 Then
                me_PermisoAccion.operacion = "GEN"
                me_PermisoAccion.codigo_pea = codigo

                dt = ListarPermisoAccion(me_PermisoAccion)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro seleccionado no ha sido encontrado.")

                me_PermisoAccion = New e_PermisoAccion

                With me_PermisoAccion
                    .codigo_pea = dt.Rows(0).Item("codigo_pea")
                    .nombreFormulario_pea = dt.Rows(0).Item("nombreFormulario_pea")
                    .formulario_pea = dt.Rows(0).Item("formulario_pea")
                    .accion_pea = dt.Rows(0).Item("accion_pea")
                    .descripcion_pea = dt.Rows(0).Item("descripcion_pea")
                End With
            Else
                With me_PermisoAccion
                    .codigo_pea = 0
                    .cod_user = 0
                End With
            End If

            Return me_PermisoAccion
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_PermisoAccionFuncion
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarPermisoAccionFuncion(ByVal le_PermisoAccionFuncion As e_PermisoAccionFuncion) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("CONF_PermisoAccionFuncionListar", le_PermisoAccionFuncion.operacion, _
                                    le_PermisoAccionFuncion.codigo_paf, _
                                    le_PermisoAccionFuncion.codigo_apl, _
                                    le_PermisoAccionFuncion.codigo_tfu, _
                                    le_PermisoAccionFuncion.codigo_per, _
                                    le_PermisoAccionFuncion.codigo_pea, _
                                    le_PermisoAccionFuncion.formulario_pea, _
                                    le_PermisoAccionFuncion.accion_pea, _
                                    le_PermisoAccionFuncion.temporalidad_paf)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarPermisoAccionFuncion(ByVal le_PermisoAccionFuncion As e_PermisoAccionFuncion) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("CONF_PermisoAccionFuncionIUD", le_PermisoAccionFuncion.operacion, _
                                    le_PermisoAccionFuncion.cod_user, _
                                    le_PermisoAccionFuncion.codigo_paf, _
                                    le_PermisoAccionFuncion.codigo_apl, _
                                    le_PermisoAccionFuncion.codigo_tfu, _
                                    le_PermisoAccionFuncion.codigo_per, _
                                    le_PermisoAccionFuncion.codigo_pea, _
                                    le_PermisoAccionFuncion.temporalidad_paf, _
                                    le_PermisoAccionFuncion.fechaInicio_paf, _
                                    le_PermisoAccionFuncion.fechaFin_paf, _
                                    le_PermisoAccionFuncion.verificarRestriccion_paf)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetPermisoAccionFuncion(ByVal codigo As Integer) As e_PermisoAccionFuncion
        Try
            Dim me_PermisoAccionFuncion As New e_PermisoAccionFuncion

            If codigo > 0 Then
                me_PermisoAccionFuncion.operacion = "GEN"
                me_PermisoAccionFuncion.codigo_paf = codigo

                dt = ListarPermisoAccionFuncion(me_PermisoAccionFuncion)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro seleccionado no ha sido encontrado.")

                me_PermisoAccionFuncion = New e_PermisoAccionFuncion

                With me_PermisoAccionFuncion
                    .codigo_paf = dt.Rows(0).Item("codigo_paf")
                    .codigo_apl = dt.Rows(0).Item("codigo_apl")
                    .codigo_tfu = dt.Rows(0).Item("codigo_tfu")
                    .codigo_per = dt.Rows(0).Item("codigo_per")
                    .codigo_pea = dt.Rows(0).Item("codigo_pea")
                    .temporalidad_paf = dt.Rows(0).Item("temporalidad_paf")
                    .fechaInicio_paf = dt.Rows(0).Item("fechaInicio_paf")
                    .fechaFin_paf = dt.Rows(0).Item("fechaFin_paf")
                    .verificarRestriccion_paf = dt.Rows(0).Item("verificarRestriccion_paf")
                End With
            Else
                With me_PermisoAccionFuncion
                    .codigo_paf = 0
                    .codigo_apl = 0
                    .codigo_tfu = 0
                    .codigo_per = 0
                    .codigo_pea = 0
                    .fechaInicio_paf = #1/1/1901#
                    .fechaFin_paf = #1/1/1901#
                    .cod_user = 0
                End With
            End If

            Return me_PermisoAccionFuncion
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

#End Region

#Region "FUNCIONES"

Public Class g_Seguridad

#Region "PERMISOS"

    Public Shared Function VerificarPermiso(ByVal codigo_apl As Integer, ByVal codigo_tfu As Integer, _
                                     ByVal codigo_per As Integer, ByVal nombre_formulario As String, _
                                     ByVal accion_permiso As String, Optional ByVal mostrar_permiso As Boolean = False) As String
        Try
            Dim dt As New DataTable : Dim ls_mensaje As String = String.Empty
            Dim lb_TienePermiso As Boolean = False : Dim md_PermisoAccionFuncion As New d_PermisoAccionFuncion
            Dim le_PermisoAccionFuncion As New e_PermisoAccionFuncion

            With le_PermisoAccionFuncion
                .operacion = "LIS"
                .codigo_apl = codigo_apl
                .codigo_tfu = codigo_tfu
                .codigo_per = codigo_per
                .formulario_pea = nombre_formulario
                .accion_pea = accion_permiso
                .mostrar_permiso = mostrar_permiso
            End With

            dt = md_PermisoAccionFuncion.ListarPermisoAccionFuncion(le_PermisoAccionFuncion)

            If dt.Rows.Count > 0 Then lb_TienePermiso = True

            With le_PermisoAccionFuncion
                If Not lb_TienePermiso Then
                    If .mostrar_permiso Then
                        ls_mensaje = "No cuenta con el permiso necesario para realizar esta acción. \n Permiso: " & .accion_pea
                    Else
                        ls_mensaje = "No cuenta con el permiso necesario para realizar esta acción."
                    End If
                End If
            End With

            Return ls_mensaje
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function VerificarRestriccion(ByVal codigo_apl As Integer, ByVal codigo_tfu As Integer, _
                                         ByVal codigo_per As Integer, ByVal nombre_formulario As String, _
                                         ByVal accion_permiso As String, Optional ByVal mostrar_permiso As Boolean = False) As Boolean
        Try
            Dim dt As New DataTable : Dim md_PermisoAccionFuncion As New d_PermisoAccionFuncion
            Dim le_PermisoAccionFuncion As New e_PermisoAccionFuncion

            With le_PermisoAccionFuncion
                .operacion = "LIS"
                .codigo_apl = codigo_apl
                .codigo_tfu = codigo_tfu
                .codigo_per = codigo_per
                .formulario_pea = nombre_formulario
                .accion_pea = accion_permiso
                .mostrar_permiso = mostrar_permiso
            End With

            dt = md_PermisoAccionFuncion.ListarPermisoAccionFuncion(le_PermisoAccionFuncion)

            If dt.Rows.Count = 0 Then Return True

            For Each fila As DataRow In dt.Rows
                If fila("verificarRestriccion_paf") = "N" Then
                    Return False
                    Exit For
                End If
            Next

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class

#End Region

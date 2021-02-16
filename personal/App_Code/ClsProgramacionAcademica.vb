Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic

#Region "ENTIDADES"

Public Class e_PlanCurso

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_pes As String
    Public codigo_cur As String
    Public codigo_cpf As String
    Public codigo_cac As String
    Public ciclo_cur As String
    Public cod_user As String
    Public operacion As String

#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_pes = String.Empty
        codigo_cur = String.Empty
        codigo_cpf = String.Empty
        codigo_cac = String.Empty
        ciclo_cur = String.Empty
        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_CursoProgramadoACAD

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_cup As String
    Public codigo_cac As String
    Public codigo_pes As String
    Public codigo_cur As String
    Public codigo_cupPadre As String
    Public refcodigo_cup As String

    Public codigo_pese As String
    Public codigo_cure As String
    Public grupohor_cup As String
    Public tipoact_cup As String
    Public vacantes_cup As String
    Public tipo_cup As String
    Public fechainicio_cup As DateTime
    Public fechafin_cup As DateTime
    Public fecharetiro_cup As DateTime
    Public usuario_cup As String
    Public obs_cup As String
    Public soloprimerciclo_cup As Boolean
    Public multiescuela As Boolean
    Public turno_cup As String
    Public bloque_cup As String    

    Public cod_user As String
    Public operacion As String

    Public codigo_cpf As String
    Public codigo_dac As String
    Public codigodac_cup As String
    Public codigo_ceq_cad As String
    Public num_grupos As String
    Public estado_cup As Boolean

    Public lst_CursoAgrupado As List(Of e_CursoProgramadoACAD)
    Public lst_PreCargaAcademica As List(Of e_PreCargaAcademica)
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_cup = String.Empty
        codigo_cac = String.Empty
        codigo_pes = String.Empty
        codigo_cur = String.Empty
        codigo_cupPadre = String.Empty

        codigo_pese = String.Empty
        codigo_cure = String.Empty
        grupohor_cup = String.Empty
        tipoact_cup = String.Empty
        vacantes_cup = String.Empty
        tipo_cup = String.Empty
        fechainicio_cup = #1/1/1901#
        fechafin_cup = #1/1/1901#
        fecharetiro_cup = #1/1/1901#
        usuario_cup = String.Empty
        obs_cup = String.Empty
        soloprimerciclo_cup = False
        multiescuela = False
        turno_cup = String.Empty
        bloque_cup = String.Empty        

        cod_user = String.Empty
        operacion = String.Empty

        codigo_cpf = String.Empty
        codigo_dac = String.Empty
        refcodigo_cup = String.Empty
        codigodac_cup = String.Empty
        codigo_ceq_cad = String.Empty
        num_grupos = String.Empty
        estado_cup = False        
    End Sub

#End Region

End Class

Public Class e_Ambiente

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_amb As String

    Public cod_user As String
    Public operacion As String

    Public codigo_cup As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_amb = String.Empty
        cod_user = String.Empty
        operacion = String.Empty
        codigo_cup = String.Empty
    End Sub

#End Region

End Class

Public Class e_PreCargaAcademica

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_pcar As String
    Public codigo_cur As String
    Public codigo_pes As String
    Public codigo_cac As String
    Public codigo_per As String
    Public observaciones_pcar As String
    Public rutaarchivo_pcar As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_pcar = String.Empty
        codigo_cur = String.Empty
        codigo_pes = String.Empty
        codigo_cac = String.Empty
        codigo_per = String.Empty
        observaciones_pcar = String.Empty
        rutaarchivo_pcar = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_BloquesCursoProgramado

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_bcup As String
    Public codigo_cup As String
    Public numerohoras As String

    Public cod_user As String
    Public operacion As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_bcup = String.Empty
        codigo_cup = String.Empty
        numerohoras = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_AdscripcionDocente

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_ecs As String
    Public codigo_per As String
    Public codigo_cac As String
    Public codigo_dac As String
    Public codigo_cpf As String
    Public opcion_todos As String

    Public cod_user As String
    Public operacion As String

#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_ecs = String.Empty
        codigo_per = String.Empty
        codigo_cac = String.Empty
        codigo_dac = String.Empty
        codigo_cpf = String.Empty
        opcion_todos = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

#End Region

#Region "DATOS"

Public Class d_PlanCurso
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarPlanCurso(ByVal le_PlanCurso As e_PlanCurso) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ACAD_PlanCursoListar", le_PlanCurso.operacion, _
                                    le_PlanCurso.codigo_pes, _
                                    le_PlanCurso.codigo_cur, _
                                    le_PlanCurso.codigo_cpf, _
                                    le_PlanCurso.codigo_cac, _
                                    le_PlanCurso.ciclo_cur)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_CursoProgramadoACAD
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarCursoProgramado(ByVal le_CursoProgramado As e_CursoProgramadoACAD) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ACAD_CursoProgramadoListar", le_CursoProgramado.operacion, _
                                    le_CursoProgramado.codigo_cup, _
                                    le_CursoProgramado.codigo_cac, _
                                    le_CursoProgramado.codigo_pes, _
                                    le_CursoProgramado.codigo_cur, _
                                    le_CursoProgramado.codigo_cpf, _
                                    le_CursoProgramado.codigo_cupPadre)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarProfesorAdscrito(ByVal le_CursoProgramado As e_CursoProgramadoACAD) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ACAD_ProfesorAdscritoListar", le_CursoProgramado.operacion, _
                                    le_CursoProgramado.codigo_cac, _
                                    le_CursoProgramado.codigo_pes, _
                                    le_CursoProgramado.codigo_cur, _
                                    le_CursoProgramado.codigo_dac)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarCursoProgramado(ByVal le_CursoProgramado As e_CursoProgramadoACAD) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ACAD_CursoProgramadoIUD", le_CursoProgramado.operacion, _
                                    le_CursoProgramado.cod_user, _
                                    le_CursoProgramado.codigo_cup, _
                                    le_CursoProgramado.refcodigo_cup, _
                                    le_CursoProgramado.codigo_pes, _
                                    le_CursoProgramado.codigo_cur, _
                                    le_CursoProgramado.codigo_cac, _
                                    le_CursoProgramado.codigodac_cup, _
                                    le_CursoProgramado.codigo_ceq_cad, _
                                    le_CursoProgramado.num_grupos, _
                                    le_CursoProgramado.tipoact_cup, _
                                    le_CursoProgramado.vacantes_cup, _
                                    le_CursoProgramado.tipo_cup, _
                                    le_CursoProgramado.fechainicio_cup, _
                                    le_CursoProgramado.fechafin_cup, _
                                    le_CursoProgramado.fecharetiro_cup, _
                                    le_CursoProgramado.obs_cup, _
                                    le_CursoProgramado.soloprimerciclo_cup, _
                                    le_CursoProgramado.multiescuela, _
                                    le_CursoProgramado.grupohor_cup, _
                                    le_CursoProgramado.estado_cup)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetCursoProgramado(ByVal codigo As Integer) As e_CursoProgramadoACAD
        Try
            Dim me_CursoProgramado As New e_CursoProgramadoACAD

            If codigo > 0 Then
            Else
                With me_CursoProgramado
                    .codigo_cup = 0
                    .codigo_cac = 0
                    .codigo_pes = 0
                    .codigo_cur = 0
                    .codigo_cupPadre = 0
                    .codigo_pese = 0
                    .codigo_cure = 0
                    .vacantes_cup = 0
                    .fechainicio_cup = #1/1/1901#
                    .fechafin_cup = #1/1/1901#
                    .fecharetiro_cup = #1/1/1901#
                    .soloprimerciclo_cup = False
                    .multiescuela = False
                    .turno_cup = String.Empty
                    .bloque_cup = 0
                    .cod_user = 0
                    .codigo_cpf = 0
                    .codigo_dac = 0
                    .refcodigo_cup = 0
                    .codigodac_cup = 0                    
                    .num_grupos = 0
                    .estado_cup = False                
                End With
            End If

            Return me_CursoProgramado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RegistrarProgramacionCurso(ByVal le_CursoProgramado As e_CursoProgramadoACAD) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            Dim md_PreCargaAcademica As New d_PreCargaAcademica

            '===========================================
            'Registrar la modificacion de curso agrupado
            '===========================================
            For Each le_CursoAgrupado As e_CursoProgramadoACAD In le_CursoProgramado.lst_CursoAgrupado
                cnx.TraerDataTable("ACAD_CursoAgrupadoIUD", le_CursoAgrupado.operacion, _
                                    le_CursoAgrupado.codigo_cup, _
                                    le_CursoAgrupado.codigo_cac, _
                                    le_CursoAgrupado.codigo_pese, _
                                    le_CursoAgrupado.codigo_cure, _
                                    le_CursoAgrupado.grupohor_cup, _
                                    le_CursoAgrupado.tipoact_cup, _
                                    le_CursoAgrupado.vacantes_cup, _
                                    le_CursoAgrupado.tipo_cup, _
                                    le_CursoAgrupado.fechainicio_cup, _
                                    le_CursoAgrupado.fechafin_cup, _
                                    le_CursoAgrupado.fecharetiro_cup, _
                                    le_CursoAgrupado.usuario_cup, _
                                    le_CursoAgrupado.obs_cup, _
                                    le_CursoAgrupado.codigo_pes, _
                                    le_CursoAgrupado.codigo_cur, _
                                    le_CursoAgrupado.soloprimerciclo_cup, _
                                    le_CursoAgrupado.multiescuela, _
                                    le_CursoAgrupado.turno_cup, _
                                    le_CursoAgrupado.bloque_cup, _
                                    le_CursoAgrupado.refcodigo_cup)
            Next

            '===========================================
            'Desactivar a todos los Profesores sugeridos
            '===========================================
            Dim me_PreCargaAcademica As e_PreCargaAcademica = md_PreCargaAcademica.GetPreCargaAcademica(0)

            With me_PreCargaAcademica
                .operacion = "D"
                .codigo_cac = le_CursoProgramado.codigo_cac
                .codigo_pes = le_CursoProgramado.codigo_pes
                .codigo_cur = le_CursoProgramado.codigo_cur
                .cod_user = le_CursoProgramado.cod_user
            End With

            cnx.TraerDataTable("ACAD_PreCargaAcademicaIUD", me_PreCargaAcademica.operacion, _
                                    me_PreCargaAcademica.cod_user, _
                                    me_PreCargaAcademica.codigo_pcar, _
                                    me_PreCargaAcademica.codigo_cur, _
                                    me_PreCargaAcademica.codigo_pes, _
                                    me_PreCargaAcademica.codigo_cac, _
                                    me_PreCargaAcademica.codigo_per, _
                                    me_PreCargaAcademica.observaciones_pcar, _
                                    me_PreCargaAcademica.rutaarchivo_pcar)

            '======================================
            'Agregar/Reactivar Profesores sugeridos
            '======================================
            For Each le_PreCargaAcademica As e_PreCargaAcademica In le_CursoProgramado.lst_PreCargaAcademica
                cnx.TraerDataTable("ACAD_PreCargaAcademicaIUD", le_PreCargaAcademica.operacion, _
                                    le_PreCargaAcademica.cod_user, _
                                    le_PreCargaAcademica.codigo_pcar, _
                                    le_PreCargaAcademica.codigo_cur, _
                                    le_PreCargaAcademica.codigo_pes, _
                                    le_PreCargaAcademica.codigo_cac, _
                                    le_PreCargaAcademica.codigo_per, _
                                    le_PreCargaAcademica.observaciones_pcar, _
                                    le_PreCargaAcademica.rutaarchivo_pcar)
            Next

            '==========================
            'Registrar Curso Programado
            '==========================
            dt = cnx.TraerDataTable("ACAD_CursoProgramadoIUD", le_CursoProgramado.operacion, _
                                    le_CursoProgramado.cod_user, _
                                    le_CursoProgramado.codigo_cup, _
                                    le_CursoProgramado.refcodigo_cup, _
                                    le_CursoProgramado.codigo_pes, _
                                    le_CursoProgramado.codigo_cur, _
                                    le_CursoProgramado.codigo_cac, _
                                    le_CursoProgramado.codigodac_cup, _
                                    le_CursoProgramado.codigo_ceq_cad, _
                                    le_CursoProgramado.num_grupos, _
                                    le_CursoProgramado.tipoact_cup, _
                                    le_CursoProgramado.vacantes_cup, _
                                    le_CursoProgramado.tipo_cup, _
                                    le_CursoProgramado.fechainicio_cup, _
                                    le_CursoProgramado.fechafin_cup, _
                                    le_CursoProgramado.fecharetiro_cup, _
                                    le_CursoProgramado.obs_cup, _
                                    le_CursoProgramado.soloprimerciclo_cup, _
                                    le_CursoProgramado.multiescuela, _
                                    le_CursoProgramado.grupohor_cup, _
                                    le_CursoProgramado.estado_cup)

            If dt.Rows.Count = 0 Then cnx.AbortarTransaccion()
            If CInt(dt.Rows(0).Item("codigo_cup")) = 0 Then cnx.AbortarTransaccion()

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_Ambiente
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarAmbiente(ByVal le_Ambiente As e_Ambiente) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ACAD_AmbienteListar", le_Ambiente.operacion, _
                                    le_Ambiente.codigo_amb, _
                                    le_Ambiente.codigo_cup)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_PreCargaAcademica
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function RegistrarPreCargaAcademica(ByVal le_PreCargaAcademica As e_PreCargaAcademica) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ACAD_PreCargaAcademicaIUD", le_PreCargaAcademica.operacion, _
                                    le_PreCargaAcademica.cod_user, _
                                    le_PreCargaAcademica.codigo_pcar, _
                                    le_PreCargaAcademica.codigo_cur, _
                                    le_PreCargaAcademica.codigo_pes, _
                                    le_PreCargaAcademica.codigo_cac, _
                                    le_PreCargaAcademica.codigo_per, _
                                    le_PreCargaAcademica.observaciones_pcar, _
                                    le_PreCargaAcademica.rutaarchivo_pcar)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetPreCargaAcademica(ByVal codigo As Integer) As e_PreCargaAcademica
        Try
            Dim me_PreCargaAcademica As New e_PreCargaAcademica

            If codigo > 0 Then
            Else
                With me_PreCargaAcademica
                    .codigo_pcar = 0
                    .codigo_cur = 0
                    .codigo_pes = 0
                    .codigo_cac = 0
                    .codigo_per = 0
                    .cod_user = 0
                End With
            End If

            Return me_PreCargaAcademica
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_BloquesCursoProgramado
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarBloquesCursoProgramado(ByVal le_BloquesCursoProgramado As e_BloquesCursoProgramado) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ACAD_BloquesCursoProgramadoListar", le_BloquesCursoProgramado.operacion, _
                                    le_BloquesCursoProgramado.codigo_bcup, _
                                    le_BloquesCursoProgramado.codigo_cup)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarBloquesCursoProgramado(ByVal le_BloquesCursoProgramado As e_BloquesCursoProgramado) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ACAD_BloquesCursoProgramadoIUD", le_BloquesCursoProgramado.operacion, _
                                    le_BloquesCursoProgramado.cod_user, _
                                    le_BloquesCursoProgramado.codigo_bcup, _
                                    le_BloquesCursoProgramado.codigo_cup, _
                                    le_BloquesCursoProgramado.numerohoras)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetBloquesCursoProgramado(ByVal codigo As Integer) As e_BloquesCursoProgramado
        Try
            Dim me_BloquesCursoProgramado As New e_BloquesCursoProgramado

            If codigo > 0 Then
            Else
                With me_BloquesCursoProgramado
                    .codigo_bcup = 0
                    .codigo_cup = 0
                    .numerohoras = 0

                    .cod_user = 0
                End With
            End If

            Return me_BloquesCursoProgramado
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_AdscripcionDocente
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable


    Public Function ListarPersonalDocente(ByVal le_AdscripcionDocente As e_AdscripcionDocente) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ACAD_PersonalDocenteListar", le_AdscripcionDocente.operacion, _
                                    le_AdscripcionDocente.codigo_per, _
                                    le_AdscripcionDocente.codigo_cac, _
                                    le_AdscripcionDocente.codigo_dac, _
                                    le_AdscripcionDocente.codigo_cpf, _
                                    le_AdscripcionDocente.opcion_todos)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

#End Region

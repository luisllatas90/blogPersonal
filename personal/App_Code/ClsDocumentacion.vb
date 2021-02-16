Imports Microsoft.VisualBasic
Imports System.Collections.Generic

#Region "variables"


#End Region


#Region "ENTIDADES"

Public Class e_Documentacion

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_dot As String
    Public codigo_cda As String
    Public correlativo_dot As String
    Public anio_dot As String
    Public glosa_dot As String
    Public estado_dot As String
    Public usuarioReg_AUD As String

#End Region

#Region "Metodos"

    Private Sub Inicializar()

        codigo_dot = String.Empty
        codigo_cda = String.Empty
        correlativo_dot = String.Empty
        anio_dot = String.Empty
        glosa_dot = String.Empty
        estado_dot = String.Empty
        usuarioReg_AUD = String.Empty

    End Sub

#End Region

End Class

Public Class e_TipoDocumentacion

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_tid As String
    Public descripcion_tid As String
    Public abreviatura_tid As String
    Public usuarioReg As String


#End Region

#Region "Metodos"

    Private Sub Inicializar()

        codigo_tid = String.Empty
        descripcion_tid = String.Empty
        abreviatura_tid = String.Empty
        usuarioReg = String.Empty


    End Sub

#End Region


End Class

Public Class e_DocumentacionArchivo

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_doa As String
    Public codigo_dot As String
    Public codigo_shf As String
    Public estado_doa As String
    Public usuarioReg As String
    Public codigo_sol As String

#End Region

#Region "Metodos"

    Private Sub Inicializar()

        codigo_doa = String.Empty
        codigo_dot = String.Empty
        codigo_shf = String.Empty
        estado_doa = String.Empty
        usuarioReg = String.Empty
        codigo_sol = String.Empty

    End Sub




#End Region


End Class

Public Class e_ConfigurarDocumentoArea

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_cda As String
    Public codigo_tid As String
    Public codigo_are As String
    Public codigo_tfu As String
    Public estado_cda As String
    Public codigo_doc As String
    Public usuarioReg As String
    Public indFirma As String
    Public referenciaDoc_cda As String

#End Region

#Region "Metodos"

    Private Sub Inicializar()

        codigo_cda = String.Empty
        codigo_tid = String.Empty
        codigo_are = String.Empty
        codigo_tfu = String.Empty
        estado_cda = String.Empty
        usuarioReg = String.Empty
        codigo_doc = String.Empty
        indFirma = String.Empty
        referenciaDoc_cda = String.Empty


    End Sub




#End Region

End Class

Public Class e_Area

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_are As String
    Public descripcion_are As String
    Public abreviatura_are As String
    Public usuarioReg As String



#End Region

#Region "Metodos"

    Private Sub Inicializar()

        codigo_are = String.Empty
        descripcion_are = String.Empty
        abreviatura_are = String.Empty
        usuarioReg = String.Empty


    End Sub




#End Region

End Class

Public Class e_Documento

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_doc As String
    Public codigo_shf As String
    Public descripcion_doc As String
    Public abreviatura_doc As String
    Public usuarioReg As String


#End Region

#Region "Metodos"

    Private Sub Inicializar()

        codigo_doc = String.Empty
        codigo_shf = String.Empty
        descripcion_doc = String.Empty
        abreviatura_doc = String.Empty
        usuarioReg = String.Empty


    End Sub

#End Region


End Class

Public Class e_Firma

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_fma As String
    Public codigo_cda As String
    Public codigo_per As String
    Public codigo_tfu As String
    Public estado_fma As String
    Public orden_fma As String
    Public usarioReg As String
    Public fechaReg As String
    Public cod_alcance As String


#End Region

#Region "Metodos"

    Private Sub Inicializar()

        codigo_fma = String.Empty
        codigo_cda = String.Empty
        codigo_per = String.Empty
        codigo_tfu = String.Empty
        estado_fma = String.Empty
        orden_fma = String.Empty
        usarioReg = String.Empty
        fechaReg = String.Empty
        cod_alcance = String.Empty

    End Sub

#End Region


End Class

Public Class e_SolicitaDocumento
#Region "Constructor"
    Public Sub New()
        Inicializar()
    End Sub
#End Region
#Region "Propiedades"
    Public codigo_sol As String
    Public codigo_cda As String
    Public codigo_fac As String
    Public estado_sol As String
    Public fechaReg As String
    Public codigo_alu As String
    Public codigoUniver_Alu As String
    Public codigo_cac As String
    Public codigo_pso As String
    Public codigo_cpf As String
    Public codigo_dac As String
    Public codigo_tes As String
    Public nombreArchivo As String
    Public referencia01 As String
    Public usuarioReg As String
    Public fechaMod As String
    Public usuarioMod As String
    Public codigoDatos As String
    Public codigo_per_Atiende As String
#End Region
#Region "Metodos"
    Private Sub Inicializar()
        codigo_sol = String.Empty
        codigo_cda = String.Empty
        codigo_fac = String.Empty
        estado_sol = String.Empty
        fechaReg = String.Empty
        codigo_alu = String.Empty
        codigoUniver_Alu = String.Empty
        codigo_cac = String.Empty
        codigo_pso = String.Empty
        codigo_cpf = String.Empty
        codigo_dac = String.Empty
        codigo_tes = String.Empty
        nombreArchivo = String.Empty
        referencia01 = String.Empty
        usuarioReg = String.Empty
        fechaMod = String.Empty
        usuarioMod = String.Empty
        codigoDatos = String.Empty
        codigo_per_Atiende = String.Empty

    End Sub
#End Region

End Class

Public Class e_ObservaDocumentacion
#Region "Constructor"
    Public Sub New()
        Inicializar()
    End Sub
#End Region
#Region "Propiedades"
    Public codigo_dob As String
    Public codigo_dot As String
    Public codigo_sol As String
    Public observacion As String
    Public fechaReg As String
    Public usuarioReg As String
    Public fechaMod As String
    Public estado_dob As String
    Public usuarioMod As String
    Public respuesta As String

    Public operacion As String


#End Region
#Region "Metodos"
    Private Sub Inicializar()
        codigo_dob = String.Empty
        codigo_dot = String.Empty
        codigo_sol = String.Empty
        observacion = String.Empty
        fechaReg = String.Empty
        usuarioReg = String.Empty
        fechaMod = String.Empty
        estado_dob = String.Empty
        usuarioMod = String.Empty
        respuesta = String.Empty

        operacion = String.Empty
    End Sub
#End Region

End Class

#End Region


#Region "DATOS"

Public Class d_Documentacion

    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function RegistrarActualizarDocumentacion(ByVal le_Documentacion As e_Documentacion) As Data.DataTable
        Try
            'Dim codigo_doc As Integer = 0
            Dim dt As New Data.DataTable

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_RegistrarActualizarDocumentacion", le_Documentacion.codigo_dot, _
                                     le_Documentacion.codigo_cda, _
                                     le_Documentacion.correlativo_dot, _
                                     le_Documentacion.anio_dot, _
                                     le_Documentacion.glosa_dot, _
                                     le_Documentacion.estado_dot, _
                                     le_Documentacion.usuarioReg_AUD)

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarDocumentacion(ByVal operacion As String, ByVal codigo_doc As Integer, ByVal serieCorrelativoDoc As String) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListarDocumentacion", operacion, _
                                     codigo_doc, serieCorrelativoDoc)

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ConsultaCorrelativoDocumentacion(ByVal codigo_tid As Integer, ByVal codigo_are As Integer, ByVal codigo_tfu As Integer, ByVal codigo_doc As Integer, ByVal anio As Integer) As Data.DataTable

        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_DocumentacionCorrelativo", codigo_tid, _
                                     codigo_are, _
                                     codigo_tfu, _
                                     codigo_doc, _
                                     anio)
            cnx.TerminarTransaccion()
            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GeneraCorrelativoDocumentacion(ByVal codigo_tid As Integer, ByVal codigo_are As Integer, ByVal codigo_tfu As Integer, ByVal codigo_doc As Integer, ByVal anio As Integer, ByVal usuario As Integer) As Data.DataTable
        Dim dtDotCorrelativo As New Data.DataTable
        Dim dtDocumentacion As New Data.DataTable
        ' el dt que devuelve
        Dim dt As New Data.DataTable()
        dt.Columns.Add("codigo_cda")
        dt.Columns.Add("correlativo_dot")
        dt.Columns.Add("configuracion")
        dt.Columns.Add("codigo_dot")
        dt.Columns.Add("indFirma")

        Dim row As Data.DataRow = dt.NewRow()

        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dtDotCorrelativo = cnx.TraerDataTable("DOC_DocumentacionCorrelativo", codigo_tid, _
                                     codigo_are, _
                                     codigo_tfu, _
                                     codigo_doc, _
                                     anio)
            cnx.TerminarTransaccion()
            '------- este es para insertar el correlativo
            If dtDotCorrelativo.Rows.Count > 0 Then
                Dim me_documentacion As New e_Documentacion
                With dtDotCorrelativo.Rows(0)
                    me_documentacion.codigo_dot = 0
                    me_documentacion.codigo_cda = .Item("codigo_cda")
                    me_documentacion.correlativo_dot = .Item("correlativo_dot")
                    me_documentacion.glosa_dot = ""
                    me_documentacion.anio_dot = anio
                    me_documentacion.usuarioReg_AUD = usuario
                    me_documentacion.estado_dot = 1
                    row("indFirma") = .Item("indFirma")
                    row("codigo_cda") = .Item("codigo_cda")
                    row("correlativo_dot") = .Item("correlativo_dot")
                    row("configuracion") = .Item("configuracion")
                    'anio_dot = .Item("anio_dot")
                End With
                dtDocumentacion = RegistrarActualizarDocumentacion(me_documentacion)
                If dtDocumentacion.Rows.Count > 0 Then
                    With dtDocumentacion.Rows(0)
                        row("codigo_dot") = .Item("codigo_dot")
                    End With
                End If
            End If

            dt.Rows.Add(row)
            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistraActualizarSolicitud(ByVal codigo_sol As Integer, ByVal codigo_cda As Integer, ByVal usuarioReg As Integer, ByVal estadoSol As String, ByVal codigo_alu As Integer, ByVal codigo_cac As Integer, ByVal referencia01 As String, ByVal codigoUniver_Alu As String) As Data.DataTable
        Try
            'Dim codigo_doc As Integer = 0
            Dim dt As New Data.DataTable

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_RegistrarActualizaSolicitudDocumentacion", codigo_sol, codigo_cda, estadoSol, usuarioReg, codigo_alu, codigo_cac, referencia01, codigoUniver_Alu)

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    
    Public Function CrearActualizarFirmasDocumento(ByVal codigo_dofm As Integer, ByVal codigo_dot As Integer, ByVal codigo_shf As Integer, ByVal codigo_cda As Integer, ByVal usuarioReg As Integer, ByVal estado_dofm As String) As Integer
        Try
            Dim respuesta As Integer  ''devuelve 1 0 0 
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            respuesta = cnx.Ejecutar("DOC_CreaFirmasParaDocumento", codigo_dofm, codigo_cda, _
                                     codigo_dot, _
                                     codigo_shf, _
                                     usuarioReg, _
                                     estado_dofm)

            cnx.TerminarTransaccion()

            Return respuesta

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function BuscaDocumentoConfigurado(ByVal codigo_tid As Integer, ByVal codigo_doc As Integer, ByVal codigo_are As Integer, ByVal codigo_tfu As Integer) As Integer
        Try
            Dim dt As New Data.DataTable

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListaConfigurarDocumentoArea", "CDA", _
                                    0, "", codigo_are, codigo_doc, codigo_tfu, codigo_tid)

            If dt.Rows.Count = 0 Then Return 0

            Return CInt(dt.Rows(0).Item("codigo_cda"))
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function ListarCodigoDta(ByVal codigo_dot As Integer) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_TraeCodigo_dta", codigo_dot)

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    'Esta funcion es para actualizar el estado de envio de correo de la tabla ProgramacionSustentacionTesis
    Public Function ActualizaEnviaCorreoProgramacion(ByVal codigo_pst As Integer) As Integer
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            cnx.Ejecutar("TES_ActualizaEnviaCorreoProgramacion", codigo_pst)
            cnx.TerminarTransaccion()

            
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarEstados(ByVal operacion As String, ByVal codigo_est As Integer, ByVal descripcion_est As String) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListarEstados", operacion, _
                                     codigo_est, descripcion_est)

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    'Esta funcion es traer datos del trmaite por el codigo dta
    Public Function ListarDatosTramiteAlumno(ByVal operacion As String, ByVal codigo_dta As Integer) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListarDatosTramite", operacion, codigo_dta)

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_TipoDocumenntacion

    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function RegistrarActualizarTipoDocumentacion(ByVal le_TipoDocumentacion As e_TipoDocumentacion) As Integer
        Try
            Dim codigo_tid As Integer
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            codigo_tid = cnx.Ejecutar("DOC_RegistrarActualizarTipoDocumentacion", le_TipoDocumentacion.codigo_tid, _
                                     le_TipoDocumentacion.descripcion_tid, _
                                     le_TipoDocumentacion.abreviatura_tid, _
                                     le_TipoDocumentacion.usuarioReg)

            cnx.TerminarTransaccion()

            Return codigo_tid

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function ListarTipoDocumentacion(ByVal operacion As String, ByVal codigo_tid As Integer) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListarTipoDocumentacion", operacion, _
                                     codigo_tid)

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function


End Class

Public Class d_DocumentacionArchivo

    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function RegistrarActualizarDocumentacionArchivo(ByVal le_DocumentacionArchivo As e_DocumentacionArchivo) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()
            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_RegistrarActualizarDocumnetacionArchivo", le_DocumentacionArchivo.codigo_doa, _
                                     le_DocumentacionArchivo.codigo_dot, _
                                     le_DocumentacionArchivo.codigo_shf, _
                                     le_DocumentacionArchivo.estado_doa, _
                                     le_DocumentacionArchivo.usuarioReg, _
                                     le_DocumentacionArchivo.codigo_sol)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function ListarDocumentacionArchivo(ByVal operacion As String, ByVal codigo_doa As Integer) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListarDocumnetacionArchivo", operacion, _
                                     codigo_doa)

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function ListarSharedFile(ByVal codigo_dot As String, ByVal codigo_tbl As Integer) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListarSharedFile", codigo_dot, _
                                     codigo_tbl)

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function




End Class

Public Class d_ConfigurarDocumentoArea

    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function RegistrarActualizarConfigurarDocumentoArea(ByVal le_ConfigurarDocumentoArea As e_ConfigurarDocumentoArea) As Integer
        Try
            Dim codigo_cda As Integer = 0
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            codigo_cda = cnx.Ejecutar("DOC_RegistrarActualizarConfigurarDocumentoArea", le_ConfigurarDocumentoArea.codigo_cda, _
                                     le_ConfigurarDocumentoArea.codigo_tid, _
                                     le_ConfigurarDocumentoArea.codigo_are, _
                                     le_ConfigurarDocumentoArea.codigo_tfu, _
                                     le_ConfigurarDocumentoArea.estado_cda, _
                                     le_ConfigurarDocumentoArea.codigo_doc, _
                                     le_ConfigurarDocumentoArea.usuarioReg, _
                                     le_ConfigurarDocumentoArea.indFirma)

            cnx.TerminarTransaccion()

            Return codigo_cda

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function ListarConfigurarDocumentoArea(ByVal operacion As String, ByVal codigo_cda As Integer, ByVal referenciaDoc_cda As String, ByVal codigo_are As Integer, ByVal codigo_doc As Integer, ByVal codigo_tfu As Integer, ByVal codigo_tid As Integer) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListaConfigurarDocumentoArea", operacion, _
                                     codigo_cda, _
                                     referenciaDoc_cda, codigo_are, codigo_doc, codigo_tfu, codigo_tid) '-- cambiar el codigo_tfu en duro

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function ListarConfigurarDocumentoAreaXAbrev(ByVal operacion As String, ByVal abreviatura_doc As String, ByVal abreviatura_tid As String, ByVal abreviatura_are As String, ByVal codigo_tfu As Integer) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListaConfigurarDocumentoAreaXAbrev", operacion, _
                                     abreviatura_doc, _
                                     abreviatura_tid, abreviatura_are, codigo_tfu)

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function ListarTipoFuncion(ByVal operacion As String, ByVal codigo_tfu As Integer) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListaTipoFuncion", operacion, _
                                     codigo_tfu)

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function ListarUsuarioFirma(ByVal codigo_tfu As Integer) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListarUsuarioFirma", codigo_tfu)
            cnx.TerminarTransaccion()

            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try

    End Function


End Class

Public Class d_DocArea
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function RegistrarActualizarArea(ByVal le_Area As e_Area) As Integer
        Try
            Dim codigo_are As Integer = 0
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            codigo_are = cnx.Ejecutar("DOC_RegistraActualizaArea", le_Area.codigo_are, _
                                     le_Area.descripcion_are, _
                                     le_Area.abreviatura_are, _
                                     le_Area.usuarioReg)

            cnx.TerminarTransaccion()

            Return codigo_are

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function ListarArea(ByVal operacion As String, ByVal codigo_are As Integer, ByVal codigo_doc As Integer) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListaArea", operacion, _
                                     codigo_are, codigo_doc)

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function



End Class

Public Class d_Documento

    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function RegistrarActualizarDocumento(ByVal le_Documento As e_Documento) As Integer
        Try
            Dim codigo_doc As Integer
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            codigo_doc = cnx.Ejecutar("DOC_RegistrarActualizarDocumento", le_Documento.codigo_doc, _
                                     le_Documento.descripcion_doc, _
                                     le_Documento.abreviatura_doc, _
                                     le_Documento.usuarioReg)



            cnx.TerminarTransaccion()

            Return codigo_doc

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarDocumento(ByVal operacion As String, ByVal codigo_doc As Integer, ByVal codigo_tfu As Integer) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListarDocumento", operacion, _
                                     codigo_doc, codigo_tfu)

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_SolicitaDocumentacion
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function RegistraActualizaSolicitaDocumentacion(ByVal le_solicitaDocumentacion As e_SolicitaDocumento) As Data.DataTable
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_RegistrarActualizaSolicitudDocumentacion", _
                                    le_solicitaDocumentacion.codigo_sol, _
                                    le_solicitaDocumentacion.codigo_cda, _
                                    le_solicitaDocumentacion.codigo_fac, _
                                    le_solicitaDocumentacion.estado_sol, _
                                    le_solicitaDocumentacion.codigo_alu, _
                                    le_solicitaDocumentacion.codigoUniver_Alu, _
                                    le_solicitaDocumentacion.codigo_cac, _
                                    le_solicitaDocumentacion.codigo_pso, _
                                    le_solicitaDocumentacion.codigo_cpf, _
                                    le_solicitaDocumentacion.codigo_dac, _
                                    le_solicitaDocumentacion.codigo_tes, _
                                    le_solicitaDocumentacion.nombreArchivo, _
                                    le_solicitaDocumentacion.referencia01, _
                                    le_solicitaDocumentacion.usuarioReg, _
                                    le_solicitaDocumentacion.codigoDatos, _
                                    le_solicitaDocumentacion.codigo_per_Atiende)
            cnx.TerminarTransaccion()

            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarSolicitaDocumentacion(ByVal operacion As String, ByVal codigo_sol As Integer, ByVal codigo_doc As Integer, ByVal estado_sol As String, ByVal codigo_per As Integer) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListarSolicitudDocumentacion", operacion, _
                                     codigo_sol, codigo_doc, estado_sol, codigo_per)
            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function UbicaAtiendeDocumento(ByVal codigo_cda As Integer, ByVal codigo_alcance As Integer) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ConsultaSecretarioFacultad", codigo_cda, _
                                     codigo_alcance)
            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_configuraFirma
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function RegistraActualizarConfiguraFirma(ByVal le_configuraFirma As e_Firma) As Integer
        Try
            Dim codigo_fma As Integer
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            codigo_fma = cnx.Ejecutar("DOC_RegistraActualizaConfiguraFirma", le_configuraFirma.codigo_fma, _
                                     le_configuraFirma.codigo_cda, _
                                     le_configuraFirma.codigo_per, _
                                     le_configuraFirma.codigo_tfu, _
                                     le_configuraFirma.cod_alcance, _
                                     le_configuraFirma.estado_fma, _
                                     le_configuraFirma.orden_fma, _
                                     le_configuraFirma.usarioReg, _
                                     le_configuraFirma.fechaReg)
            cnx.TerminarTransaccion()

            Return codigo_fma

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarConfiguraFirma(ByVal operacion As String, ByVal codigo_fma As Integer, ByVal codigo_cda As Integer, ByVal codigo_tfu As Integer, ByVal codigo_per As Integer, ByVal codigo_doc As Integer, ByVal cod_alcance As String) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListaConfiguracionFirma", _
                                    operacion, _
                                    codigo_fma, codigo_cda, 181, codigo_per, codigo_doc, cod_alcance)

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ActualizarPersFirma(ByVal codigo_dofm As Integer, ByVal codigo_per As String) As Integer
        Try
            Dim codigo_fma As Integer
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            codigo_fma = cnx.Ejecutar("DOC_UpdatePersFirma", codigo_dofm, _
                                     codigo_per)
            cnx.TerminarTransaccion()

            Return codigo_fma

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarPersonaFirma(ByVal codigo As String, ByVal tipo As String) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("ACAD_ListarDirectorAcademico", _
                                    codigo, _
                                    tipo)

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ActualizaIdArchivoCompartido(ByVal codigo_dot As Integer) As Data.DataTable
        Try
            'Dim idArchivoCompartido As Integer
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ActualizaIdArchivoCompartido", _
                                    codigo_dot)
            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
    Public Function ListaEstadoDocumentoFirma(ByVal operacion As String, ByVal codigo As Integer) As Data.DataTable
        Try
            'Dim idArchivoCompartido As Integer
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListaEstadoDocFirma", _
                                    operacion, codigo)
            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function



End Class

Public Class clsDocumentacion
   
    Public Shared Function ObtenerSerieCorrelativoDocPorCda(ByVal codigo_cda As Integer, ByVal anio As Integer, ByVal usuario As Integer) As String
        Try
            Dim serieCorrelativoDoc As String = ""
            Dim codigo_tid As Integer
            Dim codigo_are As Integer
            Dim codigo_tfu As Integer
            Dim codigo_doc As Integer
            Dim indFirma As Boolean
            Dim codigo_dot As Integer

            'Dim codigo_cda As Integer 
            Dim md_configDocAre As New d_ConfigurarDocumentoArea
            Dim dtCda As New Data.DataTable
            dtCda = md_configDocAre.ListarConfigurarDocumentoArea("COD", codigo_cda, "", 0, 0, 0, 0)
            If dtCda.Rows.Count > 0 Then
                With dtCda.Rows(0)
                    codigo_tid = .Item("codigo_tid")
                    codigo_are = .Item("codigo_are")
                    codigo_tfu = .Item("codigo_tfu")
                    codigo_doc = .Item("codigo_doc")
                End With
            End If

            'Genera el correlativo con todos los parametros
            Dim md_documentacion As d_Documentacion
            Dim dt As New Data.DataTable
            md_documentacion = New d_Documentacion
            dt = md_documentacion.GeneraCorrelativoDocumentacion(codigo_tid, codigo_are, codigo_tfu, codigo_doc, anio, usuario)
            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    codigo_dot = .Item("codigo_dot")
                    codigo_cda = .Item("codigo_cda")
                    serieCorrelativoDoc = .Item("configuracion")
                    indFirma = .Item("indFirma") 'si es true genera firmas
                End With
            End If
            'Genera las firmas si es que hay firmas

            ''''''' ************************ Firmas
            'si requiere firma
            If indFirma Then
                md_documentacion.CrearActualizarFirmasDocumento(0, codigo_dot, 0, codigo_cda, usuario, "2")
            End If

            '''''''''' fin firmas


            Return serieCorrelativoDoc
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ObtenerSerieCorrelativoDocPorAbreviatura(ByVal abreviatura_tid As String, ByVal abreviatura_doc As String, ByVal abreviatura_are As String, ByVal anio As Integer, ByVal usuario As Integer) As String
        Try
            Dim serieCorrelativoDoc As String = ""
            Dim codigo_tid As Integer
            Dim codigo_are As Integer
            Dim codigo_tfu As Integer
            Dim codigo_doc As Integer
            Dim indFirma As Boolean
            Dim codigo_dot As Integer
            Dim codigo_cda As Integer
            ' traer el código_cda
            Dim dtCodCda As New Data.DataTable

            Dim md_configuraDocumentoArea As New d_ConfigurarDocumentoArea
            dtCodCda = md_configuraDocumentoArea.ListarConfigurarDocumentoAreaXAbrev("CCDA", abreviatura_doc, abreviatura_tid, abreviatura_are, 0) ''--- el codigo_tfu va en 0 por el momento
            If dtCodCda.Rows.Count > 0 Then
                'obtnemos el codigo_cda
                codigo_cda = dtCodCda.Rows(0).Item("codigo_cda")

                'Dim codigo_cda As Integer 
                Dim md_configDocAre As New d_ConfigurarDocumentoArea
                Dim dtCda As New Data.DataTable
                dtCda = md_configDocAre.ListarConfigurarDocumentoArea("COD", codigo_cda, "", 0, 0, 0, 0)
                If dtCda.Rows.Count > 0 Then
                    With dtCda.Rows(0)
                        codigo_tid = .Item("codigo_tid")
                        codigo_are = .Item("codigo_are")
                        codigo_tfu = .Item("codigo_tfu")
                        codigo_doc = .Item("codigo_doc")
                    End With
                End If

                'Genera el correlativo con todos los parametros
                Dim md_documentacion As d_Documentacion
                Dim dt As New Data.DataTable
                md_documentacion = New d_Documentacion
                dt = md_documentacion.GeneraCorrelativoDocumentacion(codigo_tid, codigo_are, codigo_tfu, codigo_doc, anio, usuario)
                If dt.Rows.Count > 0 Then
                    With dt.Rows(0)
                        codigo_dot = .Item("codigo_dot")
                        codigo_cda = .Item("codigo_cda")
                        serieCorrelativoDoc = .Item("configuracion")
                        indFirma = .Item("indFirma") 'si es true genera firmas
                    End With
                End If
                'Genera las firmas si es que hay firmas

                ''''''' ************************ Firmas
                'si requiere firma
                If indFirma Then
                    md_documentacion.CrearActualizarFirmasDocumento(0, codigo_dot, 0, codigo_cda, usuario, "2")
                End If

                '''''''''' fin firmas


                Return serieCorrelativoDoc

            End If

            Return serieCorrelativoDoc

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function generarDocumentoPdf(ByVal serieCorrelativoDoc As String, ByVal arreglo As Dictionary(Of String, String), Optional ByVal codigo_sol As Integer = 0, Optional ByVal memory As System.IO.MemoryStream = Nothing) As Integer

        If memory Is Nothing Then
            memory = New System.IO.MemoryStream
        End If

        Dim respuesta As String
        Dim codigo_dot As Integer
        Dim usuarioReg As Integer

        Dim server As HttpServerUtility = HttpContext.Current.Server
        Dim dt As New Data.DataTable
        'Dim sourceIcon As String = server.MapPath(".") & "/img/logo_usat.png"

        Dim sourceIcon As String = server.MapPath("~/") & "GestionDocumentaria/img/logo_usat.png"

        'Dim sourceIcon As String = "E:\ProyectoCampusVirtual\campusvirtual\personal\GestionDocumentaria/img/logo_usat.png"

        Try
            Dim md_documentacion As New d_Documentacion
            Dim md_configuraFirma As New d_configuraFirma

            Dim objGeneraDocumento As New clsGeneraDocumento

            '****** ver
            'objGeneraDocumento.fuente = server.MapPath(".") & "/font/segoeui.ttf"
            objGeneraDocumento.fuente = server.MapPath("~/") & "GestionDocumentaria/font/segoeui.ttf"
            'objGeneraDocumento.fuente = "E:\ProyectoCampusVirtual\campusvirtual\personal\GestionDocumentaria/font/segoeui.ttf"


            ''''*************************creo el documento pdf
            objGeneraDocumento.GenerarDocumento(arreglo.Item("nombreArchivo"), memory, serieCorrelativoDoc, sourceIcon, arreglo)

            '''' traigo el codigo_dot para insertarlo en el archivoc compartido *********
            dt = md_documentacion.ListarDocumentacion("COR", 0, serieCorrelativoDoc)
            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    codigo_dot = CInt(.Item(0))
                    usuarioReg = .Item("usuarioReg_AUD")
                End With
            Else


            End If
            ''*********************subir archivo
            respuesta = objGeneraDocumento.fc_SubirArchivo(30, codigo_dot, 0, memory, arreglo.Item("nombreArchivo") & ".pdf", arreglo.Item("sesionUsuario"))
            ''
            ' ''*******************documentacion archivo
            Dim me_documentacionArchivo As New e_DocumentacionArchivo
            Dim md_documentacionArchivo As New d_DocumentacionArchivo


            Dim dtSf As New Data.DataTable
            Dim codigo_shf As Integer

            ''traigo el sharedFile
            dtSf = md_documentacionArchivo.ListarSharedFile(codigo_dot, "30") '---30 codigo de la tabla
            If dtSf.Rows.Count > 0 Then
                With dtSf.Rows(0)
                    codigo_shf = .Item("idArchivosCompartidos")
                End With
            Else
                codigo_shf = 0
            End If

            With me_documentacionArchivo
                .codigo_doa = 0
                .codigo_dot = codigo_dot
                .codigo_shf = codigo_shf
                .estado_doa = "7"
                .usuarioReg = usuarioReg
                .codigo_sol = codigo_sol
            End With

            '
            'inserto la documentacionArchivo

            md_documentacionArchivo.RegistrarActualizarDocumentacionArchivo(me_documentacionArchivo)

            '' ''actualizo las firmas con el personal que debe firmar
            Dim dtFmaDoc As New Data.DataTable

            '
            dtFmaDoc = md_documentacion.ListarDocumentacion("AFM", codigo_dot, "")
            If dtFmaDoc.Rows.Count > 0 Then
                For i As Integer = 0 To dtFmaDoc.Rows.Count - 1
                    Dim codigo_dofm As Integer = dtFmaDoc.Rows(i).Item("codigo_dofm")
                    Dim dtPerFma As New Data.DataTable
                    Dim codigo_per As String = "0"

                    If dtFmaDoc.Rows(i).Item("codigo_tfu") = "57" Then  ''Consejo universitario
                        dtPerFma = md_configuraFirma.ListarPersonaFirma(dtFmaDoc.Rows(i).Item("cod_alcance"), "CU")
                        If dtPerFma.Rows.Count > 0 Then
                            With dtPerFma.Rows(0)
                                codigo_per = .Item("codigo_Per")
                            End With
                        End If
                        md_configuraFirma.ActualizarPersFirma(codigo_dofm, codigo_per)

                    ElseIf dtFmaDoc.Rows(i).Item("codigo_tfu") = "78" Then ''Director de escuela posgrado
                        dtPerFma = md_configuraFirma.ListarPersonaFirma(672, "CO") '--CO director de esccuela / posgrado / 672 es el centro de costos de posgrado
                        If dtPerFma.Rows.Count > 0 Then
                            With dtPerFma.Rows(0)
                                codigo_per = .Item("codigo_Per")
                            End With
                        End If
                        md_configuraFirma.ActualizarPersFirma(codigo_dofm, codigo_per)
                        '--- 23/10/2020
                    ElseIf dtFmaDoc.Rows(i).Item("codigo_tfu") = "181" Then ''dirección académica

                        dtPerFma = md_configuraFirma.ListarPersonaFirma(1653, "CO") '-- 1653 es el centro de costos de direccion académica y el cargo es director
                        If dtPerFma.Rows.Count > 0 Then
                            With dtPerFma.Rows(0)
                                codigo_per = .Item("codigo_Per")
                            End With
                        End If
                        md_configuraFirma.ActualizarPersFirma(codigo_dofm, codigo_per)


                    Else
                        If dtFmaDoc.Rows(i).Item("cod_alcance") = "F" Then
                            dtPerFma = md_configuraFirma.ListarPersonaFirma(arreglo.Item("codigo_fac"), "F")
                            If dtPerFma.Rows.Count > 0 Then
                                With dtPerFma.Rows(0)
                                    codigo_per = .Item("codigo_Per")
                                End With
                            End If
                            md_configuraFirma.ActualizarPersFirma(codigo_dofm, codigo_per)
                        End If
                    End If


                    ''alumnos = alumnos + dt.Rows(i).Item(3) & ", "
                Next
            End If

            Return codigo_dot

        Catch ex As Exception
            Throw ex
        End Try




    End Function

    Public Shared Function GeneraSolicitudDocumento(ByVal codigo_cda As Integer, ByVal codigoDatos As Integer, ByVal nombreArchivo As String, ByVal codigo_user As Integer, ByVal codigo_fac As Integer) As Integer
        Try
            Dim codigo_per_atiede As String = ""
            Dim dtAtiende As New Data.DataTable
            Dim dt As New Data.DataTable
            Dim me_solicitaDocumento As New e_SolicitaDocumento
            Dim md_solicitaDocumento As New d_SolicitaDocumentacion

            If codigo_cda = 3 Then ' Solicitud de resolucion de sustentacion
                dtAtiende = md_solicitaDocumento.UbicaAtiendeDocumento(codigo_cda, codigo_fac)
                If dtAtiende.Rows.Count > 0 Then
                    codigo_per_atiede = dtAtiende.Rows(0).Item("codigo_per")
                End If
                With me_solicitaDocumento
                    .codigo_sol = 0
                    .codigo_cda = codigo_cda
                    .estado_sol = "8" 'pendiente de generar
                    .codigo_tes = codigoDatos
                    .nombreArchivo = nombreArchivo
                    .usuarioReg = codigo_user
                    .codigoDatos = codigoDatos
                    .codigo_fac = codigo_fac
                    .codigo_per_Atiende = codigo_per_atiede

                End With
                dt = md_solicitaDocumento.RegistraActualizaSolicitaDocumentacion(me_solicitaDocumento)

                If dt.Rows.Count > 0 Then Return CInt(dt.Rows(0).Item("codigo_sol"))
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function GeneraSolicitudDocumentoXAbreviaturas(ByVal codigo_dta As Integer, ByVal abreviatura_doc As String, ByVal abreviatura_tid As String, ByVal abreviatura_are As String, ByVal codigoDatos As Integer, ByVal nombreArchivo As String, ByVal codigo_user As Integer, ByVal codigo_fac As Integer) As Integer
        Try
            Dim codigo_per_atiede As String = ""
            Dim codigo_cda As Integer
            Dim dtAtiende As New Data.DataTable
            Dim dt As New Data.DataTable
            Dim dtCda As New Data.DataTable

            Dim me_solicitaDocumento As New e_SolicitaDocumento
            Dim md_solicitaDocumento As New d_SolicitaDocumentacion

            Dim md_configuraDocumentoArea As New d_ConfigurarDocumentoArea
            dtCda = md_configuraDocumentoArea.ListarConfigurarDocumentoAreaXAbrev("CCDA", abreviatura_doc, abreviatura_tid, abreviatura_are, 0) ''--- el codigo_tfu va en 0 por el momento
            If dtCda.Rows.Count > 0 Then

                codigo_cda = dtCda.Rows(0).Item("codigo_cda")
                dtAtiende = md_solicitaDocumento.UbicaAtiendeDocumento(codigo_cda, codigo_fac)

                If dtAtiende.Rows.Count > 0 Then
                    codigo_per_atiede = dtAtiende.Rows(0).Item("codigo_per")
                End If
                With me_solicitaDocumento
                    .codigo_sol = 0
                    .codigo_cda = codigo_cda
                    .estado_sol = "8" 'pendiente de generar
                    .codigo_tes = "0"
                    .nombreArchivo = nombreArchivo
                    .usuarioReg = codigo_user
                    .codigoDatos = codigoDatos
                    .codigo_fac = codigo_fac
                    .codigo_per_Atiende = codigo_per_atiede
                    .referencia01 = codigo_dta

                End With
                dt = md_solicitaDocumento.RegistraActualizaSolicitaDocumentacion(me_solicitaDocumento)

                If dt.Rows.Count > 0 Then Return CInt(dt.Rows(0).Item("codigo_sol"))
            End If

            'If codigo_cda = 3 Then ' Solicitud de resolucion de sustentacion

            'End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Shared Function PrevioDocumentopdf(ByVal serieCorrelativoDoc As String, ByVal arreglo As Dictionary(Of String, String), Optional ByVal memory As System.IO.MemoryStream = Nothing) As Integer
        Try

        
            Dim server As HttpServerUtility = HttpContext.Current.Server
            Dim objGenerarDocumento As New clsGeneraDocumento

            objGenerarDocumento.fuente = server.MapPath("~/") & "GestionDocumentaria/font/segoeui.ttf"
            Dim sourceIcon As String = server.MapPath("~/") & "GestionDocumentaria/img/logo_usat.png"

            objGenerarDocumento.GenerarDocumento(arreglo.Item("nombreArchivo"), memory, serieCorrelativoDoc, sourceIcon, arreglo)
           

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function actualizaEstadoDoc(ByVal tipo As String, ByVal codigo As Integer, ByVal estado As String, ByVal codigo_user As Integer) As Boolean
        Try
            Dim dt As New Data.DataTable
            Dim md_documentacion As New d_Documentacion

            If tipo = "SOL" Then ' si es solicitud
                Dim me_solicitaDocumentacion As New e_SolicitaDocumento
                Dim md_solicitaDocumento As New d_SolicitaDocumentacion
                With me_solicitaDocumentacion
                    .codigo_sol = codigo
                    .codigo_cda = 0
                    .estado_sol = estado
                    .usuarioReg = codigo_user
                    .codigoDatos = 0
                End With
                'Call mt_ShowMessage(me_solicitaDocumentacion.codigo_sol & " " & me_solicitaDocumentacion.estado_sol & " " & me_solicitaDocumentacion.usuarioReg, MessageType.error)
                dt = md_solicitaDocumento.RegistraActualizaSolicitaDocumentacion(me_solicitaDocumentacion)
                Return IIf(CInt(dt.Rows(0).Item("codigo_sol")) <> 0, True, False)

            ElseIf tipo = "DOC" Then  ''AQUI DEBIO SER DOT ...... 
                Dim me_documentacion As New e_Documentacion
                md_documentacion = New d_Documentacion
                With me_documentacion
                    .anio_dot = 0
                    .codigo_cda = 0
                    .codigo_dot = codigo
                    .correlativo_dot = 0
                    .estado_dot = estado
                    .glosa_dot = ""
                    .usuarioReg_AUD = codigo_user
                End With
                dt = md_documentacion.RegistrarActualizarDocumentacion(me_documentacion)
                Return IIf(CInt(dt.Rows(0).Item("codigo_dot")) <> 0, True, False)

            ElseIf tipo = "DOA" Then
                Dim me_documentoArchivo As New e_DocumentacionArchivo
                Dim md_documentoArchivo As New d_DocumentacionArchivo
                With me_documentoArchivo
                    .codigo_doa = codigo
                    .codigo_dot = 0
                    .codigo_shf = 0
                    .codigo_sol = 0
                    .estado_doa = estado
                    .usuarioReg = codigo_user
                End With
                dt = md_documentoArchivo.RegistrarActualizarDocumentacionArchivo(me_documentoArchivo)
                Return IIf(CInt(dt.Rows(0).Item("codigo_doa")) <> 0, True, False)
            End If
            
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function EnviarCorreoAdjunto(ByVal codigo_dta As String, ByVal ruta_shf As String, ByVal codigo_usu As String, ByVal codigo_tfu As String) As String
        Dim mensaje As String = ""
        Try

            Dim codigoEnvio As Integer
            Dim emailAlumno As String = ""
            Dim md_documentacion As New d_Documentacion
            'Dim md_solicitaDocumento As New d_SolicitaDocumentacion
            'Dim codigo_univ_alu As String = ""

            Dim rptaEmail As Boolean = False
            'llamo esta clase por que ahi se genera la resolucion de sustentacion y traigo los datos para el envio de correos 
            'Dim objGeneraDocumento As New clsGeneraDocumento
            'Dim dtSol As New Data.DataTable
            Dim dtEmail As New Data.DataTable

            'dtSol = md_solicitaDocumento.ListarSolicitaDocumentacion("", codigo_sol, 0, "", 0)
            'If dtSol.Rows.Count > 0 Then
            '    With dtSol.Rows(0)
            '        codigo_univ_alu = .Item("codigoUniver_Alu")
            '    End With
            'End If

            dtEmail = md_documentacion.ListarDatosTramiteAlumno("EMAIL", codigo_dta)

            If dtEmail.Rows.Count > 0 Then

                codigoEnvio = ClsComunicacionInstitucional.ObtenerCodigoEnvio(codigo_usu, codigo_tfu, 77)

                If ConfigurationManager.AppSettings("CorreoUsatActivo") = "1" Then
                    emailAlumno = emailAlumno + dtEmail.Rows(0).Item("eMail_Alu")
                Else
                    emailAlumno = "olluen@usat.edu.pe"
                End If

                ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "TRVE", "FTVI", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "77", emailAlumno, "", "", ruta_shf, _
                      dtEmail.Rows(0).Item("glosaCorrelativo_trl"), dtEmail.Rows(0).Item("alumno"), dtEmail.Rows(0).Item("descripcion_ctr"))


            End If

        Catch ex As Exception
            Throw ex
        End Try
        Return mensaje
    End Function

    Public Shared Function ApruebaEtapaTramite(ByVal codigo_dta As String, ByVal codigo_tfu As String, ByVal codigo_per As Integer, ByVal idArchivoCompartido As String, ByVal flagObserva As String, ByVal textoObservacion As String) As String
        Dim mensaje As String = ""
        If codigo_dta <> 0 Or codigo_dta <> "" Then
            Dim dtTramite As New Data.DataTable
            dtTramite = mt_ActualizarEtapaTramite(codigo_dta, "1", flagObserva, codigo_tfu, codigo_per, idArchivoCompartido, textoObservacion)
            If dtTramite.Rows.Count > 0 Then ' SI HAY RESPUESTA DEL CAMBIO DE ETAPA
                If dtTramite.Rows(0).Item("revision") = False Then ' NoSI SE HIZO LA REVISIÓN
                    ' NO SE ACTUALIZO LA ETAPA DEL TRAMITE
                    mensaje = "No se pudo realizar la actualización de la etapa del trámite"
                End If
            Else
                mensaje = "No se pudo realizar la actualización de la etapa del trámite"
            End If
        Else
            'call mt_ShowMessage("Código del trámite invalido", MessageType.error)
            mensaje = "Código del trámite invalido"
        End If
        Return mensaje
    End Function

    Public Shared Function mt_ActualizarEtapaTramite(ByVal codigo_dta As Integer, ByVal tipooperacion As String, ByVal estadoaprobacion As String, ByVal codigo_tfu As Integer, ByVal codigo_per As Integer, ByVal idArchivoCompartido As String, ByVal textoObservacion As String) As Data.DataTable
        Try
            Dim cmp As New clsComponenteTramiteVirtualCVE
            Dim objcmp As New List(Of Dictionary(Of String, Object))()
            cmp._codigo_dta = codigo_dta
            'cmp.tipoOperacion = "1"
            cmp.tipoOperacion = tipooperacion
            cmp._codigo_per = codigo_per
            cmp._codigo_tfu = codigo_tfu ' tipo funcion director de escuela
            If estadoaprobacion = "A" Then
                cmp._estadoAprobacion = "A" ' DA CONFORMIDAD OSEA APRUEBA
                'cmp._observacionEvaluacion = "Su trámite ha sido aceptado, puede descargar su documento <span onClick=""downloadDoc(" & idArchivoCompartido & ");"" style=""cursor: pointer; color: blue;"">aquí</span>"
                cmp._observacionEvaluacion = "Su trámite ha sido aceptado"
                If idArchivoCompartido <> "" Then
                    cmp._idArchivoCompartido = idArchivoCompartido
                End If
            ElseIf estadoaprobacion = "O" Then
                cmp._estadoAprobacion = "O"
                cmp._observacionEvaluacion = textoObservacion
            Else
                cmp._estadoAprobacion = "R"
                cmp._observacionEvaluacion = "Tramite Rechazado"
            End If
            objcmp = cmp.mt_EvaluarTramite()
            Dim dt As New Data.DataTable
            dt.Columns.Add("revision")
            dt.Columns.Add("registros")
            dt.Columns.Add("email")
            For Each fila As Dictionary(Of String, Object) In objcmp
                dt.Rows.Add(fila.Item("evaluacion"), fila.Item("registos evaluados").ToString, fila.Item("email"))
            Next
            Return dt
        Catch ex As Exception
            Dim dt As New Data.DataTable
            dt.Columns.Add("revision")
            dt.Columns.Add("registros")
            dt.Columns.Add("email")

            dt.Rows.Add(False, "", False)

            Return dt
        End Try
    End Function

    


End Class

Public Class d_DetalleTramiteAlumno
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable
    Public Function ConsultaDatosTramite(ByVal codigo_dta As Integer) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ConsultaDatosTramite", codigo_dta)
                                    

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function
End Class

Public Class d_ObservaDocumentacion

    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable

    Public Function RegistrarActualizarObservaDoc(ByVal le_observaDocumento As e_ObservaDocumentacion) As Integer
        Try
            Dim codigo_doc As Integer
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            codigo_doc = cnx.Ejecutar("DOC_RegistrarActualizarObservacion", le_observaDocumento.codigo_dob, _
                                     le_observaDocumento.estado_dob, _
                                     le_observaDocumento.codigo_sol, _
                                     le_observaDocumento.codigo_dot, _
                                     le_observaDocumento.observacion, _
                                     le_observaDocumento.usuarioReg, _
                                     le_observaDocumento.respuesta)

            cnx.TerminarTransaccion()

            Return codigo_doc

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ListarDocumentosObsrvados(ByVal le_observaDocumento As e_ObservaDocumentacion) As Data.DataTable
        Try

            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("DOC_ListarObservacionDocumento", le_observaDocumento.operacion, _
                                     le_observaDocumento.codigo_dot, _
                                     le_observaDocumento.codigo_sol, _
                                     le_observaDocumento.estado_dob, _
                                     le_observaDocumento.usuarioReg, _
                                     le_observaDocumento.observacion)

            cnx.TerminarTransaccion()

            Return dt

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ActualizaRespuestaObservacionTramite(ByVal codigo_sol As Integer, ByVal codigo_dta As Integer, ByVal estado_dob As String, ByVal codigo_tfu As Integer) As Integer
        Try
            Dim rptaTrans As Integer
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            rptaTrans = cnx.Ejecutar("DOC_ActualizaRespuestaObservacionTramite", codigo_dta, _
                                     codigo_tfu, _
                                     estado_dob, _
                                     codigo_sol)

            cnx.TerminarTransaccion()

            Return rptaTrans

        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

#End Region



Imports Microsoft.VisualBasic

Public Class ClsNoAdeudos
    Private cnx As New ClsConectarDatos
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
    Public Function ConsultarDatosAlumno(ByVal codigoUniversitario As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("consultaracceso", "E", codigoUniversitario, "")
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function RegistrarNoAdeudo(ByVal codigo_alu As Integer, ByVal correo As String, ByVal codigo_per As Integer) As String
        Try
            Dim rpta As String
            rpta = ""
            Dim valoresdevueltos(1) As String
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            cnx.AbrirConexion()
            cnx.Ejecutar("ADE_RegistrarConstanciaNoAdeudos", correo, codigo_alu, codigo_per, "").copyto(valoresdevueltos, 0)
            rpta = valoresdevueltos(0)
            cnx.CerrarConexion()
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return (0)
        End Try
    End Function
    Public Function ConsultarNoAdeudos(ByVal tipo As String, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ADE_ConsultarNoAdeudos", codigo_per, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarRevisiones(ByVal codigo_cade As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ADE_ConsultarRevisionesAdeudos", codigo_cade)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Sub FinalizarNoAdeudo(ByVal codigo_cade As Integer)
        Try
            Dim valoresdevueltos(1) As String
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            cnx.AbrirConexion()
            cnx.Ejecutar("ADE_FinalizarConstanciaNoAdeudos", codigo_cade)
            cnx.CerrarConexion()

        Catch ex As Exception
            cnx.AbortarTransaccion()

        End Try
    End Sub

    Public Sub EvaluarNoAdeudo(ByVal estado_RAde As String, ByVal observacion_RAde As String, ByVal codigo_CAde As Integer, ByVal codigo_per As Integer)
        Try
            Dim valoresdevueltos(1) As String
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            cnx.AbrirConexion()
            cnx.Ejecutar("ADE_EvaluacionConstancia", estado_RAde, observacion_RAde, codigo_CAde, codigo_per)
            cnx.CerrarConexion()

        Catch ex As Exception
            cnx.AbortarTransaccion()

        End Try
    End Sub
    Public Function ConsultarUltimaRevision(ByVal codigo_cade As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ADE_ConsultarUltimaRevision", codigo_cade)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarDatosRemitente(ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarPersonal", "PE", id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaRevisiores() As Data.DataTable
        ' KB traer la lista de revisores
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ADE_ConsultarNoAdeudos", 0, "R")
        cnx.CerrarConexion()
        Return dts
    End Function
End Class

Imports Microsoft.VisualBasic

Public Class clsSolicitudVacante

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

    Public Function ListaCicloAcademicoSolicitudes() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("SOL_ListaCicloAcademicoSolicitudes")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaMiembrosPerfil(ByVal tipo As String, ByVal codigo_tfu As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("SOL_ConsultaMiembrosPerfil", tipo, codigo_tfu)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaAccesos(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("SOL_ConsultaAccesos", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function VistaCargaAcademica(ByVal tipo As String, ByVal codigo_cac As Integer, ByVal codigo_svac As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("SOL_VistaCargaAcademica", tipo, codigo_cac, codigo_svac)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConulstaEmail(ByVal codigo_svac As Integer, ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("SOL_ConsultaEmail", codigo_svac, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarComentariosRegistrados(ByVal tipo As String, ByVal codigo_csv As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("SOL_ConsultarComentariosRegistrados", tipo, codigo_csv)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargarListaComentarios(ByVal codigo_svac As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("SOL_CargarListaComentarios", codigo_svac)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaTipoPersona(ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("SOL_ListaTipoPersona", tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function BuscarVacates(ByVal parametroBusqueda As String, ByVal codigo_ded As Integer, ByVal codigo_tpe As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("SOL_ListaPersonalVacante", parametroBusqueda, codigo_ded, codigo_tpe)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaDepartamentosAcademicos(ByVal tipo As String, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("SOL_ListaDepartamentoAcademicos", tipo, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaCecosEscuelaDpto(ByVal codigo_dac As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("SOL_ListaCentroCostosEscuelasDpto", codigo_dac)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarSolictudVacante(ByVal codigo_svac As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("SOL_ConsultarSolictudVacante", codigo_svac)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaDedicaciones(ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("SOL_ListaDedicacion", tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function retornaCicloVigente() As Int32
        Dim rpta As Int32
        Dim valoresdevueltos(1) As Int32
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("SOL_ConsultaCicloVigente", 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
    End Function

    Public Function RegistrarComentario(ByVal comentario As String, ByVal codigo_per As Integer, ByVal codigo_svac As Integer) As Int32
        Dim rpta As Int32
        Dim valoresdevueltos(1) As Int32
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("SOL_RegistrarComentarioSolicitudVacante", comentario, codigo_per, codigo_svac, 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
    End Function


    Public Function CalificarSolicitud(ByVal tipo As String, ByVal codigo_svac As Integer, ByVal usercalifica As Integer) As Int32
        Dim rpta As Int32
        Dim valoresdevueltos(1) As Int32
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("SOL_CalificarSolicitud", tipo, codigo_svac, usercalifica, 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
    End Function


    Public Function EliminarSolicitudVacante(ByVal codigo_svac As Integer, ByVal codigo_per As Integer) As Int32
        Dim rpta As Int32
        Dim valoresdevueltos(1) As Int32
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("SOL_EliminarSolicitudVacante", codigo_svac, codigo_per, 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
    End Function


    '*** Devuelve todos los registros activos de solicitudes de vacantes.
    Public Function ListaRegistroSolicitudesVacantes(ByVal codigo_cac As Integer, _
                                                     ByVal estado As String, _
                                                     ByVal departamento As Integer, _
                                                     ByVal ceco As Integer, _
                                                     ByVal tipo As String, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("SOL_ListaRegistroSolicitudesVacantes", codigo_cac, estado, departamento, ceco, tipo, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function AgregarSolicitudVacante(ByVal codigo_cac As Integer, _
                                            ByVal Codigo_dac As Integer, _
                                            ByVal Codigo_ceco As Integer, _
                                            ByVal Codigo_ded As Integer, _
                                            ByVal Remuneracion_svac As String, _
                                            ByVal Numhoras_svac As Double, _
                                            ByVal PrecioHora_svac As String, _
                                            ByVal UsuarioReg_svac As Integer, _
                                            ByVal tipo As String, _
                                            ByVal fechaini As Date, _
                                            ByVal fechafin As Date, _
                                            ByVal observacion As String, _
                                            ByVal formacion As String) As Int32
        Dim rpta As Int32
        Dim valoresdevueltos(1) As Int32
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("SOL_AgregarSolicitudVacante", codigo_cac, Codigo_dac, Codigo_ceco, Codigo_ded, Remuneracion_svac, Numhoras_svac, PrecioHora_svac, UsuarioReg_svac, tipo, fechaini, fechafin, observacion, formacion, 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
    End Function

    Public Function ModificarSolicitudVacante(ByVal codigo_svac As Integer, ByVal codigo_cac As Integer, _
                                           ByVal Codigo_dac As Integer, _
                                           ByVal Codigo_ceco As Integer, _
                                           ByVal Codigo_ded As Integer, _
                                           ByVal Remuneracion_svac As String, _
                                           ByVal Numhoras_svac As Double, _
                                           ByVal PrecioHora_svac As String, _
                                           ByVal UsuarioReg_svac As Integer, ByVal tipo As String, _
                                           ByVal fechaini As Date, ByVal fechafin As Date, _
                                           ByVal observacion As String, _
                                           ByVal codigo_per As Integer, _
                                           ByVal formacion_svac As String) As Int32
        Dim rpta As Int32
        Dim valoresdevueltos(1) As Int32
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("SOL_ModificarSolicitudVacante", codigo_svac, _
                     codigo_cac, _
                     Codigo_dac, _
                     Codigo_ceco, _
                     Codigo_ded, _
                     Remuneracion_svac, _
                     Numhoras_svac, _
                     PrecioHora_svac, _
                     UsuarioReg_svac, _
                     tipo, _
                     fechaini, _
                     fechafin, _
                     observacion, codigo_per, formacion_svac, 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
    End Function

    Public Function AgregarSolicitudVacanteExistente(ByVal codigo_cac As Integer, _
                                            ByVal Codigo_dac As Integer, _
                                            ByVal Codigo_ceco As Integer, _
                                            ByVal Codigo_ded As Integer, _
                                            ByVal Remuneracion_svac As String, _
                                            ByVal Numhoras_svac As Double, _
                                            ByVal PrecioHora_svac As String, _
                                            ByVal UsuarioReg_svac As Integer, _
                                            ByVal tipo As String, _
                                            ByVal fechaini As Date, _
                                            ByVal fechafin As Date, _
                                            ByVal observacion As String, _
                                            ByVal codigo_per As Integer, _
                                            ByVal formacion_svac As String) As Int32
        Dim rpta As Int32
        Dim valoresdevueltos(1) As Int32
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("SOL_AgregarSolicitudVacanteExistente", codigo_cac, Codigo_dac, Codigo_ceco, Codigo_ded, Remuneracion_svac, Numhoras_svac, PrecioHora_svac, UsuarioReg_svac, tipo, fechaini, fechafin, observacion, codigo_per, formacion_svac, 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
    End Function

    Public Function encriptarSalario(ByVal salario As Double) As String
        Dim rpta As String
        Dim valoresdevueltos(1) As String
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("spPla_encriptar", salario, 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
    End Function

    Public Function CantidadSolEstado(ByVal tipo As String) As Integer
        Dim rpta As Int32
        Dim valoresdevueltos(1) As Int32
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("SOL_CantidadSolicitudesPorEstado", tipo, 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
    End Function

End Class

Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsIndicadores
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

    Public Function ConsultarPeriodicidad(ByVal codigo_peri As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarPeriodicidad", codigo_peri)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertarPeriodicidad(ByVal descripcion_peri As String, ByVal usuario As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertarPeriodicidad", descripcion_peri, usuario)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function EliminarPeriodicidad(ByVal codigo_peri As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarPeriodicidad", codigo_peri)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function ModificarPeriodicidad(ByVal descripcion_peri As String, ByVal usuario As Integer, ByVal codigo_peri As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ModificarPeriodicidad", descripcion_peri, usuario, codigo_peri)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function ConsultarCategoria(ByVal codigo_cat As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarCategoria", codigo_cat)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertarCategoria(ByVal descripcion_peri As String, ByVal usuario As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertarCategoria", descripcion_peri, usuario)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function EliminarCategoria(ByVal codigo_cat As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarCategoria", codigo_cat)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function ModificarCategoria(ByVal descripcion_cat As String, ByVal usuario As Integer, ByVal codigo_cat As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ModificarCategoria", descripcion_cat, usuario, codigo_cat)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    '12/01/2012 mvillavicencio
    '************ Mantenimiento de Variables **********************************************

    Public Function ConsultarVariable(ByVal codigo_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarVariable", codigo_var)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertarVariable(ByVal codigo_cat As Integer, _
                                     ByVal codigo_peri As Integer, _
                                     ByVal nombre_var As String, _
                                     ByVal usuario As Integer, _
                                     ByVal sumatoria As String) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertarVariable", codigo_cat, codigo_peri, nombre_var, usuario, sumatoria)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("CodigoVar").ToString
    End Function

    Public Function EliminarVariable(ByVal codigo_var As String, ByVal usuario As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarVariable", codigo_var, usuario)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function ModificarVariable(ByVal codigo_var As String, _
                                      ByVal codigo_cat As Integer, _
                                      ByVal codigo_peri As Integer, _
                                      ByVal nombre_var As String, _
                                      ByVal usuario As Integer, _
                                      ByVal sumatoria As String) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ModificarVariable", codigo_var, codigo_cat, codigo_peri, nombre_var, usuario, sumatoria)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function ConsultarVariablePorNombre(ByVal nombre_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarVariablePorNombre", nombre_var)
        cnx.CerrarConexion()
        Return dts
    End Function


    '13/01/2012 mvillavicencio
    '************ Mantenimiento de Subvariables **********************************************

    Public Function ConsultarSubvariable(ByVal codigo_sub As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarSubvariable", codigo_sub)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertarSubvariable(ByVal codigo_var As String, ByVal codigo_cco As Integer, ByVal descripcion_sub As String, ByVal usuario As Integer, ByVal codigo_fac As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertarSubvariable", codigo_var, codigo_cco, descripcion_sub, usuario, codigo_fac)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function EliminarSubvariable(ByVal codigo_sub As String) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarSubvariable", codigo_sub)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function ModificarSubvariable(ByVal codigo_sub As String, ByVal codigo_cco As Integer, ByVal codigo_var As String, ByVal nombre_sub As String, ByVal usuario As Integer, ByVal codigo_fac As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ModificarSubvariable", codigo_sub, codigo_cco, codigo_var, nombre_sub, usuario, codigo_fac)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function ConsultarSubvariablePorNombre(ByVal nombre_sub As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarSubvariablePorNombre", nombre_sub)
        cnx.CerrarConexion()
        Return dts
    End Function

    '16/01/2012 Lista Variables y el Item "Seleccione"
    Public Function ListarVariables(ByVal coidgo_per As Integer, ByVal ControlAcceso As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        'ControlAcceso [0] No filtra las variables por codigo_per / [1] si filtra las variables configuradas en CenctroCostoNivelIndicador
        dts = cnx.TraerDataTable("IND_ListarVariables", coidgo_per, ControlAcceso)
        cnx.CerrarConexion()

        Return dts
    End Function

    '16/01/2012 Lista CentroCostos y el Item "Seleccione"
    Public Function ListarCentroCostos() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ListarCentroCostos")
        cnx.CerrarConexion()
        Return dts
    End Function

    '16/01/2012 Lista Subvariables y el Item "Seleccione"
    Public Function ListarSubvariables() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ListarSubvariables")
        cnx.CerrarConexion()
        Return dts
    End Function

    '16/01/2012 Lista Tipo Dimension y el Item "Seleccione"
    Public Function ListarTipoDimension() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ListarTipoDimension")
        cnx.CerrarConexion()
        Return dts
    End Function

    '16/01/2012 mvillavicencio

    Public Function ConsultarDimension(ByVal codigo_dim As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarDimension", codigo_dim)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertarDimension(ByVal codigo_cco As Integer, ByVal codigo_sub As String, ByVal tipo_dim As String, ByVal descripcion_dim As String, ByVal usuario As Integer, ByVal codigo_cpf As Integer, ByVal codigo_dac As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertarDimension", codigo_cco, codigo_sub, tipo_dim, descripcion_dim, usuario, codigo_cpf, codigo_dac)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function EliminarDimension(ByVal codigo_dim As String) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarDimension", codigo_dim)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function ModificarDimension(ByVal codigo_dim As String, ByVal codigo_cco As Integer, ByVal codigo_sub As String, ByVal tipo_dim As String, ByVal descripcion_dim As String, ByVal usuario As Integer, ByVal codigo_cpf As Integer, ByVal codigo_dac As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ModificarDimension", codigo_dim, codigo_cco, codigo_sub, tipo_dim, descripcion_dim, usuario, codigo_cpf, codigo_dac)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function ConsultarDimensionPorNombre(ByVal nombre_dim As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarDimensionPorNombre", nombre_dim)
        cnx.CerrarConexion()
        Return dts
    End Function


    '18/01/2012 Consulta Niveles de Subdimension
    Public Function ConsultarNivelesSubdimension(ByVal codigo_dim1 As Integer, ByVal codigo_dim2 As Integer, ByVal codigo_dim3 As Integer, ByVal codigo_dim4 As Integer, ByVal codigo_dim5 As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarNivelesSubdimension", codigo_dim1, codigo_dim2, codigo_dim3, codigo_dim4, codigo_dim5)
        cnx.CerrarConexion()
        Return dts
    End Function
    'Insertar Configuracion Subdimensiones
    Public Function InsertarConfiguracionSubdimensiones(ByVal codigo_var As String, ByVal codigo_csub As Integer, ByVal codigo_nid As Integer, ByVal usuario As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertarConfiguracionSubdimensiones", codigo_var, codigo_csub, codigo_nid, usuario)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Bandera").ToString
    End Function

    'Consultar Niveles de Subdimension Configuradas para una Variable
    Public Function ConsultarSubdimensionesConfiguradas(ByVal codigo_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarSubdimensionesConfiguradas", codigo_var)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ConsultarNivelesSubdimensionUtilizados(ByVal codigo_var As String, ByVal codigo_csub As Integer, ByVal codigo_nid As Integer) As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ConsultarNivelesSubdimensionUtilizados", codigo_var, codigo_nid, codigo_nid)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Contador").ToString
    End Function

    Public Function EliminarSubdimensionConfigurada(ByVal codigo_var As String, ByVal codigo_csub As Integer, ByVal codigo_nid As Integer, ByVal usuario As Integer) As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarSubdimensionConfigurada", codigo_var, codigo_csub, codigo_nid, usuario)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("NumeroSub").ToString
    End Function

    '19/01
    Public Function ListarPeriodicidad() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ListarPeriodicidad")
        cnx.CerrarConexion()
        Return dts
    End Function

    '19/01/2012 mvillavicencio
    'incluye item "Seleccione"
    Public Function ListarDimension() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ListarDimension")
        cnx.CerrarConexion()
        Return dts
    End Function

    '19/01/2012
    'Consultar Niveles de Subdimension Configuradas para una Variable. Busca por codigo dimension
    Public Function ConsultarNivelSubConfiguradas_Dimension(ByVal codigo_dim As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarNivelSubConfiguradas_Dimension", codigo_dim)
        cnx.CerrarConexion()
        Return dts
    End Function

    'Consultar Tipos de Subdimension que pertenecen a un nivel de subdimension
    Public Function ConsultarTiposSubdimension(ByVal codigo_csub As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarTiposSubdimension", codigo_csub)
        cnx.CerrarConexion()
        Return dts
    End Function

    'Listar Subvariables que pertenecen a una Variable
    Public Function ListarSubvariables_Variable(ByVal codigo_var As String, ByVal codigo_per As Integer, ByVal controlAcceso As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ListarSubvariables_Variable", codigo_var, codigo_per, controlAcceso)
        cnx.CerrarConexion()
        Return dts
    End Function

    'incluye item "Seleccione"
    Public Function ListarDimension_Subvariable(ByVal codigo_aux As String, ByVal codigo_per As Integer, ByVal controlAcceso As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ListarDimension_Subvariable", codigo_aux, codigo_per, controlAcceso)
        cnx.CerrarConexion()
        Return dts
    End Function

    'Lista Niveles de Subdimension Configuradas para una Variable
    'incluye item "Seleccione"
    Public Function ListarSubdimensionesConfiguradas(ByVal codigo_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ListarSubdimensionesConfiguradas", codigo_var)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarSubdimensionPorNombre(ByVal nombre_sub As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarSubdimensionPorNombre", nombre_sub)
        cnx.CerrarConexion()
        Return dts
    End Function

    'ConsultarSemestre  xDguevara -------------------------------
    Public Function ConsultarSemestre(ByVal vPeriodicidad As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ConsultarSemestre", vPeriodicidad)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarVariableImportar(ByVal vTipo As String, ByVal vCodigo_peri As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ConsultaVariableImportar", vTipo, vCodigo_peri)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Sub EjecutaVariable01(ByVal vCodigo_var As Integer, ByVal vCodigo_cac As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        'cnx.Ejecutar("IND_ImportarValoresVariables", vCodigo_var, vCodigo_cac)
        cnx.Ejecutar("IND_ImportarVariable01", vCodigo_var, vCodigo_cac)
        cnx.CerrarConexion()
    End Sub

    '--Ejecucion de la Variable Nº 1 ---------------------------------------------------------------------------
    Public Sub EjecutaVariable01_1(ByVal vCodigo_var As Integer, ByVal vCodigo_cac As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("IND_NumeroTitulados", vCodigo_var, vCodigo_cac)
        cnx.CerrarConexion()
    End Sub

    Public Sub EjecutaVariable01_2(ByVal vCodigo_var As Integer, ByVal vCodigo_cac As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("IND_NumeroTituladosFacultad", vCodigo_var, vCodigo_cac)
        cnx.CerrarConexion()
    End Sub

    Public Sub EjecutaVariable01_3(ByVal vCodigo_var As Integer, ByVal vCodigo_cac As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("IND_NumeroTituladosEscuela", vCodigo_var, vCodigo_cac)
        cnx.CerrarConexion()
    End Sub

    Public Sub EjecutaVariable01_4(ByVal vCodigo_var As Integer, ByVal vCodigo_cac As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("IND_NumeroTituladosEscuelaSexo", vCodigo_var, vCodigo_cac)
        cnx.CerrarConexion()
    End Sub

    '--Ejecucion de la Variable Nº 2 ---------------------------------------------------------------------------
    Public Sub EjecutaVariable02_1(ByVal vCodigo_var As Integer, ByVal vCodigo_cac As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("IND_NumeroMatriculados", vCodigo_var, vCodigo_cac)
        cnx.CerrarConexion()
    End Sub

    Public Sub EjecutaVariable02_2(ByVal vCodigo_var As Integer, ByVal vCodigo_cac As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("IND_NumeroMatriculadosFacultad", vCodigo_var, vCodigo_cac)
        cnx.CerrarConexion()
    End Sub

    Public Sub EjecutaVariable02_3(ByVal vCodigo_var As Integer, ByVal vCodigo_cac As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("IND_NumeroMatriculadosEscuela", vCodigo_var, vCodigo_cac)
        cnx.CerrarConexion()
    End Sub

    Public Sub EjecutaVariable02_4(ByVal vCodigo_var As Integer, ByVal vCodigo_cac As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("IND_NumeroMatriculadosSexo", vCodigo_var, vCodigo_cac)
        cnx.CerrarConexion()
    End Sub


    '************************** MANTENIMIENTO DE SUBDIMENSIONES ******************************************

    Public Function InsertarSubdimension(ByVal codigo_dim As String, ByVal descripcion_sub As String, ByVal abreviatura_sub As String, ByVal usuario As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertarSubdimension", codigo_dim, descripcion_sub, abreviatura_sub, usuario)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function ConsultarSubdimension(ByVal codigo_sub As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarSubdimension", codigo_sub)
        cnx.CerrarConexion()
        Return dts
    End Function

    '---------------------------------------------------------------------------------

    '20/01/2012 Consulta Niveles de Dimension
    Public Function ConsultarNivelesDimension(ByVal codigo_naux As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarNivelesDimension", codigo_naux)
        cnx.CerrarConexion()
        Return dts
    End Function

    'InsertarConfiguracionDimensiones
    Public Function InsertarConfiguracionDimensiones(ByVal codigo_var As String, ByVal codigo_nid As Integer, ByVal codigo_naux As Integer, ByVal usuario As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertarConfiguracionDimensiones", codigo_var, codigo_nid, codigo_naux, usuario)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Bandera").ToString
    End Function

    ''Consultar Niveles de Dimension Configuradas para una Variable
    Public Function ConsultarDimensionesConfiguradas(ByVal codigo_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarDimensionesConfiguradas", codigo_var)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarNivelesDimensionUtilizados(ByVal codigo_var As String, ByVal abreviatura_nid As String) As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ConsultarNivelesDimensionUtilizados", codigo_var, abreviatura_nid)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Contador").ToString
    End Function

    Public Function EliminarDimensionConfigurada(ByVal codigo_var As String, ByVal codigo_nid As Integer, ByVal usuario As Integer, ByVal abreviatura_nid As String) As Data.DataTable 'Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarDimensionConfigurada", codigo_var, codigo_nid, usuario, abreviatura_nid)
        cnx.CerrarConexion()
        'Return dts.Rows(0).Item("NumeroDim").ToString
        Return dts
    End Function

    '********************** POR DANTE ****************************************************************

    '--Carga el combo con la data configurada de la perspectiva - plan, necesaria para el registro de los objetivos 
    '---###  Modificado 24.01.2012
    Public Function ConsultarConfiguracionPerspectivaPlan(ByVal ctf As Integer, ByVal codigo_per As Integer) As Data.DataTable 'Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaConfiguracionPerspectivaPlan", ctf, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertarObjetivo(ByVal vCodigo_ppla As Integer, _
                                     ByVal vNomre_obj As String, _
                                     ByVal usuario As Integer, _
                                     ByVal abreviatura As String, _
                                     ByVal desdeMaximo As Decimal, ByVal hastaMaximo As Decimal, _
                                     ByVal desdePromedio As Decimal, ByVal hastaPromedio As Decimal, _
                                     ByVal desdeMinimo As Decimal, ByVal hastaMinimo As Decimal, _
                                     ByVal meta_obj As Decimal, ByVal año_obj As Integer, _
                                     ByVal direccionescala As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertarObjetivo", vCodigo_ppla, vNomre_obj, usuario, abreviatura, desdeMaximo, hastaMaximo, desdePromedio, hastaPromedio, desdeMinimo, hastaMinimo, meta_obj, año_obj, direccionescala)
        cnx.CerrarConexion()
        Return dts
        'Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    '--Procedimiento para cargar los objetivos registrados y para las busquedas por descripcion del objetivo
    Public Function ConsultarObjetivos(ByVal vParametro As String, _
                                       ByVal codigo_pla As Integer, _
                                       ByVal abreviatura_pers As String, _
                                       ByVal anio As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarObjetivos", vParametro, codigo_pla, abreviatura_pers, anio)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminaroObjetivo(ByVal codigo_obj As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminaroObjetivo", codigo_obj)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ModificarObjetivo(ByVal Codigo_obj As Integer, _
                                      ByVal Codigo_ppla As Integer, _
                                      ByVal nombre_obj As String, _
                                      ByVal operador_obj As Integer, _
                                      ByVal abreviatura As String, _
                                      ByVal desdeMaximo As Decimal, ByVal hastaMaximo As Decimal, _
                                     ByVal desdePromedio As Decimal, ByVal hastaPromedio As Decimal, _
                                     ByVal desdeMinimo As Decimal, ByVal hastaMinimo As Decimal, _
                                     ByVal meta_obj As Decimal, ByVal año_obj As Integer, _
                                     ByVal direccionescala As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ModificarObjetivo", Codigo_obj, Codigo_ppla, nombre_obj, operador_obj, abreviatura, desdeMaximo, hastaMaximo, desdePromedio, hastaPromedio, desdeMinimo, hastaMinimo, meta_obj, año_obj, direccionescala)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertarPeriodo(ByVal descripcion_pdo As String, ByVal desde_pdo As String, ByVal hasta_pdo As String, ByVal usuario As Integer, ByVal codigo_peri As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertaPeriodo", descripcion_pdo, desde_pdo, hasta_pdo, usuario, codigo_peri)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarPeriodo(ByVal vParametro As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ConsultarPeriodo", vParametro)
        cnx.CerrarConexion()
        Return dts
    End Function

    '----Modificado el 01.02.2012 ------------------------------------------------------------------------////
    Public Function EliminaroPeriodo(ByVal codigo_pdo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarPeriodo", codigo_pdo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ModificarPeriodo(ByVal Codigo_pdo As String, ByVal descripcion_pdo As String, ByVal desde_pdo As String, ByVal hasta_pdo As String, ByVal usuario As Integer, ByVal codigo_peri As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ModificarPeriodo", Codigo_pdo, descripcion_pdo, desde_pdo, hasta_pdo, usuario, codigo_peri)
        cnx.CerrarConexion()
        Return dts
    End Function

    '############################# Actualizar!!
    Public Function ValidaFechasPeriodo(ByVal descripcion_pdo As String, ByVal desde_pdo As String, ByVal hasta_pdo As String, ByVal vOperacion As String, ByVal Codigo_pdo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValidarRangoFechaPeriodo", descripcion_pdo, desde_pdo, hasta_pdo, vOperacion, Codigo_pdo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function GeneraEstructuraVariable(ByVal Codigo_var As String, _
                                             ByVal vEstructura As Integer, _
                                             ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_GeneraEstructuraJerarquicaVariable", Codigo_var, vEstructura, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function


    '*************************************************************************************************
    '*Por mvillavicencio 23/01/12

    'Lista Objetivos y el Item "Seleccione"
    Public Function ListarObjetivos(ByVal codigo_pers As Integer, ByVal codigo_pla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ListarObjetivos", codigo_pers, codigo_pla)
        cnx.CerrarConexion()
        Return dts
    End Function

    'dts = obj.ConsultarIndicador(Me.ddlPlan2.SelectedValue, txtBuscar.Text, Me.ddlAnioBus.SelectedValue, Me.ddlListaObjetivos.SelectedValue)
    Public Function ConsultarIndicador(ByVal codigo_pla As Integer, _
                                       ByVal descripcion_ind As String, _
                                       ByVal anio As String, _
                                       ByVal abrCodeObj As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarIndicador", codigo_pla, descripcion_ind, anio, abrCodeObj)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function InsertarIndicador(ByVal descripcion_ind As String, _
                                      ByVal codigo_obj As Integer, _
                                      ByVal ponderacion_ind As Decimal, _
                                      ByVal desdeOptimo As Decimal, _
                                      ByVal hastaOptimo As Decimal, _
                                      ByVal desdeAmbar As Decimal, _
                                      ByVal hastaAmbar As Decimal, _
                                      ByVal desdeRojo As Decimal, _
                                      ByVal hastaRojo As Decimal, _
                                      ByVal usuario As Integer, _
                                      ByVal metaind As Decimal, _
                                      ByVal tipodato As String, _
                                      ByVal año As Integer, _
                                      ByVal direccionescala As String, _
                                      ByVal basal As Decimal, _
                                      ByVal tipoOperacion As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertarIndicador", descripcion_ind, _
                                 codigo_obj, _
                                 ponderacion_ind, _
                                 desdeOptimo, _
                                 hastaOptimo, _
                                 desdeAmbar, _
                                 hastaAmbar, _
                                 desdeRojo, _
                                 hastaRojo, _
                                 usuario, _
                                 metaind, _
                                 tipodato, _
                                 año, _
                                 direccionescala, _
                                 basal, _
                                 tipoOperacion)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function EliminarIndicador(ByVal codigo_ind As Integer, ByVal usuario As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarIndicador", codigo_ind, usuario)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    'Public Function ConsultarIndicadorPorNombre(ByVal nombre_ind As String) As Data.DataTable
    '    Dim dts As New Data.DataTable
    '    cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    cnx.AbrirConexion()

    '    dts = cnx.TraerDataTable("IND_ConsultarIndicadorPorNombre", nombre_ind)
    '    cnx.CerrarConexion()
    '    Return dts
    'End Function


    '*************************************************************************************************

    '--- DESDE EL 23.01.2012 ---- POR DGUEVARA

    Public Function InsertarPerspectiva(ByVal descripcion_pers As String, _
                                        ByVal operador_pers As Integer, _
                                        ByVal abreviatura As String, _
                                        ByVal color_pers As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertarPerspectiva", descripcion_pers, operador_pers, abreviatura, color_pers)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarPerspectivas(ByVal vParametro As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarPerspectiva", vParametro)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ModificarPerspectiva(ByVal codigo_pers As Integer, _
                                         ByVal descripcion_pers As String, _
                                         ByVal operador_pers As Integer, _
                                         ByVal abreviatura As String, _
                                         ByVal color_per As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ModificarPerspectiva", codigo_pers, descripcion_pers, operador_pers, abreviatura, color_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarPerspectiva(ByVal codigo_pers As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarPerspectiva", codigo_pers)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ConsultarPlanes(ByVal vParametro As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarPlan", vParametro)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarPlanes(ByVal ctf As Integer, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaPlanTablero", ctf, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    'Actualizado el 28.05.2013 xDguevara para cambios por el informe BSC.
    Public Function InsertarPlan(ByVal periodo_pla As String, _
                                 ByVal EstadoVigencia As String, _
                                 ByVal FechaIni As String, _
                                 ByVal FechaFin As String, _
                                 ByVal usuario As Integer, _
                                 ByVal abreviatura_pla As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertarPlan", periodo_pla, EstadoVigencia, FechaIni, FechaFin, usuario, abreviatura_pla)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarPlan(ByVal codigo_pla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarPlan", codigo_pla)
        cnx.CerrarConexion()
        Return dts
    End Function


    'Se modifico el 28.05.2013 para el Informe del BSC.
    Public Function ModificarPlan(ByVal codigo_pla As Integer, _
                                  ByVal periodo_pla As String, _
                                  ByVal EstadoVigencia As String, _
                                  ByVal FechaIni As String, _
                                  ByVal FechaFin As String, _
                                  ByVal usuario As Integer, _
                                  ByVal abreviatura_pla As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ModificarPlan", codigo_pla, periodo_pla, EstadoVigencia, FechaIni, FechaFin, usuario, abreviatura_pla)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ValidarDuplicadoPerspectiva(ByVal descripcion_pers As String, _
                                                ByVal codigo_pers As Integer, _
                                                ByVal operacion_pers As String, _
                                                ByVal abreviatura As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValidarDuplicadoPerspectiva", descripcion_pers, codigo_pers, operacion_pers, abreviatura)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ValidarRangoFechaPlan(ByVal Periodo_pla As String, ByVal FechaIni_pla As String, ByVal FechaFin_pla As String, ByVal Operador_pla As String, ByVal Codigo_pla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValidarRangoFechaPlan", Periodo_pla, FechaIni_pla, FechaFin_pla, Operador_pla, Codigo_pla)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function AdvertenciaPlanVigente() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_AdvertenciaPlanVigente")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaPlanes(ByVal ctf As Integer, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListarPlanes", ctf, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertaPerspectivaPlan(ByVal codigo_pla As Integer, ByVal Codigo_pers As Integer, ByVal usuario As Integer, ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertaPerspectivasPlanConfiguracion", codigo_pla, Codigo_pers, usuario, codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaPerspectivasSegunPlan(ByVal codigo_pla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaPerspectivasSegunPlan", codigo_pla)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarPlanPerspectiva(ByVal codigo_ppla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarPlanPerspectiva", codigo_ppla)
        cnx.CerrarConexion()
        Return dts
    End Function

    '**************************************************************************************************************
    '24/01/12 Por mvillavicencio

    Public Function ListarPerspectivas(ByVal vParametro As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ListarPerspectiva", vParametro)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ModificarIndicador(ByVal codigo_ind As Integer, _
                                       ByVal descripcion_ind As String, _
                                       ByVal codigo_obj As Integer, _
                                       ByVal ponderacion_ind As Decimal, _
                                       ByVal desdeOptimo As Decimal, _
                                       ByVal hastaOptimo As Decimal, _
                                       ByVal desdeAmbar As Decimal, _
                                       ByVal hastaAmbar As Decimal, _
                                       ByVal desdeRojo As Decimal, _
                                       ByVal hastaRojo As Decimal, _
                                       ByVal usuario As Integer, _
                                       ByVal metaind As Decimal, _
                                       ByVal tipodato_ind As String, _
                                       ByVal año As Integer, _
                                       ByVal direccionescala As String, _
                                       ByVal basal As Decimal, _
                                       ByVal codigo_top As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ModificarIndicador", codigo_ind, _
                                 descripcion_ind, _
                                 codigo_obj, _
                                 ponderacion_ind, _
                                 desdeOptimo, _
                                 hastaOptimo, _
                                 desdeAmbar, _
                                 hastaAmbar, _
                                 desdeRojo, _
                                 hastaRojo, _
                                 usuario, _
                                 metaind, _
                                 tipodato_ind, _
                                 año, _
                                 direccionescala, _
                                 basal, _
                                 codigo_top)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    '25/01/2012 Por mvillavicencio
    Public Function ConsultarCentroCostosConfigurados(ByVal codigo_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ConsultarCentroCostosConfigurados", codigo_var)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertarConfiguracionCentroCostos(ByVal codigo_cco As Integer, ByVal codigo_var As String) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertarConfiguracionCentroCostos", codigo_cco, codigo_var)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    'Consulta todos los centros de costos segun el parametro, y que no esten configurados para la variable
    Public Function ConsultarCentroCostosNoConfigurados(ByVal vCriterio As String, ByVal codigo_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ConsultarCentroCostosNoConfigurados", vCriterio, codigo_var)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarCentroCostoConfigurado(ByVal codigo_cco As Integer, ByVal codigo_var As String) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarCentroCostoConfigurado", codigo_cco, codigo_var)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function ConsultarValoresVariables(ByVal codigo_var As String, ByVal codigo_peri_desde As Integer, ByVal codigo_peri_hasta As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ConsultarValoresVariables", codigo_var, codigo_peri_desde, codigo_peri_hasta)
        cnx.CerrarConexion()
        Return dts
    End Function

    '25/01/12 Por Dguevara

    Public Function CargarDatosMenu() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INDObtenerOpcionesMenu")
        cnx.CerrarConexion()
        Return dts
    End Function

    '********************************************************************************************

    '26/01/12 Por mvillavicencio
    '18/01/2012 Consulta Niveles de Subdimension
    Public Function ConsultarNivelesSubvariable() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarNivelesSubvariable")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarPeriodosPosteriores(ByVal codigo_periodicidad As Integer, ByVal codigo_periodo As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ConsultarPeriodosPosteriores", codigo_periodicidad, codigo_periodo)
        cnx.CerrarConexion()
        Return dts
    End Function

    '27/01/12 Por mvillavicencio
    'incluye item "Seleccione"
    Public Function ListarSubdimension_Dimension(ByVal codigo_dim As String, ByVal codigo_per As Integer, ByVal controlAcceso As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ListarSubdimension_Dimension", codigo_dim, codigo_per, controlAcceso)
        cnx.CerrarConexion()
        Return dts
    End Function

    '31/01/12 Por mvillavicencio
    Public Function EliminarSubdimension(ByVal codigo_dim As String) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarSubdimension", codigo_dim)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function ModificarSubdimension(ByVal codigo_sub As String, ByVal codigo_dim As String, ByVal descripcion_sub As String, ByVal abreviatura_sub As String, ByVal usuario As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ModificarSubdimension", codigo_sub, codigo_dim, descripcion_sub, abreviatura_sub, usuario)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    'ConsultarPeriodos, Reeamplaza a ConsultarSemestre  xDguevara -------------------------------
    'ConsultarSemestre asigna los periodos en el procedimiento, mientras que ConsultarPeriodos los
    'busca en la tabla PeriodoInd
    Public Function ConsultarPeriodos(ByVal vPeriodicidad As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ConsultarPeriodos", vPeriodicidad)
        cnx.CerrarConexion()
        Return dts
    End Function

    '*******************************************************************************************
    'Por dguevara

    'Trabajo del 25.01.12 falta actualizar

    Public Function CargarObjetivosSegunPlan(ByVal Codigo_ppla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaObjetivosSegunPlan", Codigo_ppla)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargarIndicadoresSegunObjetivo(ByVal Codigo_obj As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarIndicadoresSegunObjetivo", Codigo_obj)
        cnx.CerrarConexion()
        Return dts
    End Function

    '----- ######    Métodos para cargar el Treeview xDguevara 27.01.2012 #####
    Public Function CargarListaCategorias() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaCategoriaTreeView")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargarListaCategorias2(ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaCategoriaTreeViewConfiguradas", codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargarListaPlanes() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ListaPlanTreeView")
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function CargarListaVariable(ByVal Codigo_cat As Integer, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        'dts = cnx.TraerDataTable("IND_CargarListaVariableTreeView", Codigo_cat, codigo_per)
        'Agregado 15.01.2013 xDguevara:
        dts = cnx.TraerDataTable("IND_CargarListaVariableTreeViewNivelAcceso", Codigo_cat, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    'xD 08.12.12
    Public Function CargarListaVariable2(ByVal Codigo_cat As Integer, ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarListaVariableTreeViewConfigurada", Codigo_cat, codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function CargarListaSubVariables(ByVal Codigo_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarListaSubVariableTreeView", Codigo_var)
        cnx.CerrarConexion()
        Return dts
    End Function

    'xD 08.12.12
    Public Function CargarListaSubVariables2(ByVal Codigo_var As String, ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarListaSubVariableTreeViewConfigurado", Codigo_var, codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargarListaDimension(ByVal Codigo_aux As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarListaDimensionTreeView", Codigo_aux)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargarListaDimension2(ByVal Codigo_aux As String, ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarListaDimensionTreeViewConfigurado", Codigo_aux, codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargarListaSubDimension(ByVal Codigo_dim As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarListaSubDimensionTreeView", Codigo_dim)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargarListaSubDimension2(ByVal Codigo_dim As String, ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarListaSubDimensionTreeViewConfigurado", Codigo_dim, codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertarModificarFormula(ByVal Codigo_for As Integer, ByVal Codigo_ind As Integer, ByVal descripcion_for As String, ByVal descripcionc_for As String, ByVal estado_for As String, ByVal operador_for As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertaModificaFormula", Codigo_for, Codigo_ind, descripcion_for, descripcionc_for, estado_for, operador_for)
        cnx.CerrarConexion()
        Return dts
    End Function

    '---###  Fin Treeview


    '31.01.2012

    Public Function ListaFormulasIndicadores(ByVal vParametro As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaFormulasIndicadores", vParametro)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminaFormula(ByVal codigo_for As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminaFormula", codigo_for)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargaListaPeriodicidad() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargaListaPeriodicidad")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaPeriodos() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaPeriodos")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaFormulasRegistradas(ByVal vParamentro As String, _
                                             ByVal codigo_pla As Integer, _
                                             ByVal anio As String, _
                                             ByVal abreviatura As String, ByVal sintaxis As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaFormulasRegistradas", vParamentro, codigo_pla, anio, abreviatura, sintaxis)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertaFormulaPeriodo(ByVal codigo_pdo As String, ByVal vCodigo_for As Integer, ByVal usuario As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertaFormulaPeriodo", codigo_pdo, vCodigo_for, usuario)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaFormulasConfiguradasPeriodo(ByVal codigo_pdo As String, _
                                          ByVal nombre_ind As String, _
                                          ByVal codigo_pla As Integer, _
                                          ByVal anio As String, _
                                          ByVal nombre_obj As String, _
                                          ByVal sintaxis As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaFormulasConfiguradasPeriodo", codigo_pdo, nombre_ind, codigo_pla, anio, nombre_obj, sintaxis)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ValidaInsercionFormulaPeriodo(ByVal codigo_pdo As String, ByVal codigo_for As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValidaInsercionFormulaPeriodo", codigo_pdo, codigo_for)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminaPeriodoFormula(ByVal codigo_fp As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarFormulaPeriodo", codigo_fp)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ValidarInclusionFormulaPeriodo(ByVal codigo_fp As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValidarInclusionFormulaPer", codigo_fp)
        cnx.CerrarConexion()
        Return dts
    End Function

    '*******************************************************************************************
    'Por mvillavicencio 02/02/12
    'Modificado por xDuevara 02.05.2012 

    Public Function ActualizarValorVariable(ByVal variable As String, _
                                            ByVal subvariable As String, _
                                            ByVal dimension As String, _
                                            ByVal subdimension As String, _
                                            ByVal periodo As String, _
                                            ByVal valor As Decimal, _
                                            ByVal codigo As Integer, _
                                            ByVal codigo_var As String, _
                                            ByVal codigo_aux As String, _
                                            ByVal codigo_dim As String, _
                                            ByVal codigo_sub As String, _
                                            ByVal importado As Boolean, _
                                            ByVal sumatoria_var As String, _
                                            ByVal DetalleNivel As Integer, _
                                            ByVal UltimoNivel As Integer, _
                                            ByVal desc_niv2 As String, _
                                            ByVal existebd_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ActualizarValorVariable", codigo, valor, UltimoNivel)
        cnx.CerrarConexion()
        Return dts
    End Function



    Public Function ListarValoresVariable(ByVal codigo_var As String, _
                                          ByVal codigo_aux As String, _
                                          ByVal codigo_dim As String, _
                                          ByVal codigo_sub As String, _
                                          ByVal codigo_pdo As String, _
                                          ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListarValoresVariable", codigo_var, codigo_aux, codigo_dim, codigo_sub, codigo_pdo, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function
    '*******************************************************************************************

    Public Function ListarCategoriasVariables() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaCaregorias")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarPeriodoSegunPeriodicidad(ByVal codigo_peri As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListarPeriodoSegunPeriodicidad", codigo_peri)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaVariablesPorCategoria(ByVal codigo_cat As Integer, ByVal codigo_peri As Integer, ByVal vTipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaVariablesPorCategoria", codigo_cat, codigo_peri, vTipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function RecuperarScriptsVariable(ByVal codigo_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_RecuperarScriptsVariable", codigo_var)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Sub ImportarDataVariable(ByVal vProcedimeinto As String, _
                                    ByVal vCodigo_var As String, _
                                    ByVal vCodigo_pdo As String)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar(vProcedimeinto, vCodigo_var, vCodigo_pdo)
        cnx.CerrarConexion()
    End Sub
    '1
    Public Function CargarPeriodicidadVariable(ByVal codigo_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarPeriodicidadVariable", codigo_var)
        cnx.CerrarConexion()
        Return dts
    End Function

    '2
    Public Function CargarPeriodoVariable(ByVal codigo_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarPeriodoVariable", codigo_var)
        cnx.CerrarConexion()
        Return dts
    End Function

    '3
    Public Function CargarVariableXPeriodo(ByVal codigo_var As String, ByVal codigo_cac As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarVariablePeriodo", codigo_var, codigo_cac)
        cnx.CerrarConexion()
        Return dts
    End Function

    '4
    Public Function CargarSubVariableXPeriodo(ByVal codigo_var As String, ByVal codigo_cac As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarListaSubvariablesPeriodo", codigo_var, codigo_cac)
        cnx.CerrarConexion()
        Return dts
    End Function

    '5
    Public Function CargarListaDimensionPeriodo(ByVal vcodigo_aux_cac As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarListaDimensionPeriodo", vcodigo_aux_cac)
        cnx.CerrarConexion()
        Return dts
    End Function

    '6
    Public Function CargarListaSubDimensionPeriodo(ByVal vcodigo_dim_cac As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarListaSubDimensionPeriodo", vcodigo_dim_cac)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaVariablesXCategoria(ByVal codigo_cat As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaVariablesXCategoria ", codigo_cat)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ValidarImportacionPorPeriodo(ByVal codigo_var As String, ByVal codigo_pdo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValidarImportacionPorPeriodo", codigo_var, codigo_pdo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaCodigoPeriodo() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaCodigoPeriodo")
        cnx.CerrarConexion()
        Return dts
    End Function

    'Por mvillavicencio 08/02/12
    'InsertarConfiguracionDimensiones
    Public Function InsertarConfiguracionSubvariables(ByVal codigo_var As String, ByVal codigo_naux As Integer, ByVal usuario As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertarConfiguracionSubvariables", codigo_var, codigo_naux, usuario)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Bandera").ToString
    End Function

    Public Function EliminarSubvariableConfigurada(ByVal codigo_var As String, ByVal codigo_naux As Integer, ByVal usuario As Integer, ByVal abreviatura_naux As String) As Data.DataTable 'Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarSubvariableConfigurada", codigo_var, codigo_naux, usuario, abreviatura_naux)
        cnx.CerrarConexion()
        'Return dts.Rows(0).Item("NumeroDim").ToString
        Return dts
    End Function

    ''Consultar Niveles de subVariable Configuradas para una Variable
    Public Function ConsultarSubvariablesConfiguradas(ByVal codigo_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarSubvariablesConfiguradas", codigo_var)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarNivelesAuxiliarUtilizados(ByVal codigo_var As String, ByVal abreviatura_naux As String) As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ConsultarNivelesAuxiliarUtilizados", codigo_var, abreviatura_naux)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Contador").ToString
    End Function

    '09/02/12 Elimina toda la configuracion de subdimensiones, dada una variable
    Public Function EliminarTodasSubdimensionesConfiguradas(ByVal codigo_var As String, ByVal usuario As Integer) As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarTodasSubdimensionConfiguradas", codigo_var, usuario)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("NumeroSub").ToString
    End Function

    Public Function ValidaObjetivoIndicadorEliminacion(ByVal codigo_obj As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValidaObjetivoIndicadorEliminacion", codigo_obj)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ValidarPalabrasReservadas(ByVal vParametro As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValidarPalabrasReservadas", vParametro)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ValidaPeriodoFormula(ByVal codigo_pdo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValidaPeriodoFormula", codigo_pdo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ValidarEliminacionPerspectiva(ByVal codigo_pers As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValidarEliminacionPerspec", codigo_pers)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ValidaEliminacionPlan(ByVal codigo_pla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValidaEliminacionPlan", codigo_pla)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ValidaEliminacionPlanActivo(ByVal codigo_pla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValidaEliminacionPlanActivo", codigo_pla)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ValidaObjetivoDuplicado(ByVal Operacion As String, ByVal codigo_obj As Integer, _
                                            ByVal descripcion_obj As String, _
                                            ByVal abreviatura As String, _
                                            ByVal codigo_ppla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValidaObjetivoDuplicado", Operacion, codigo_obj, descripcion_obj, abreviatura, codigo_ppla)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ValidarExistenciaPerspectiva(ByVal codigo_pla As Integer, ByVal codigo_pers As Integer, _
                                            ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValidarExistenciaPerspectiva", codigo_pla, codigo_pers, codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ValidarEliminacionPlanPerspectiva(ByVal codigo_ppla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValidarEliminacionPlanPerspectiva", codigo_ppla)
        cnx.CerrarConexion()
        Return dts
    End Function


    'Falta implemnetar esto ??? xDguevara
    Public Function VerificarValoresVariable(ByVal codigo_var As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_VerificarValoresVariable", codigo_var)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ValorCeroVariable(ByVal codigo_var As String, ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValorCeroVariable", codigo_var, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarPeriodicidadImportar() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ConsultarPeriodicidadImportar")
        cnx.CerrarConexion()
        Return dts
    End Function

    '24/02/2012
    Public Function ConsultarCategoriaPorNombre(ByVal descripcion_cat As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarCategoriaPorNombre", descripcion_cat)
        cnx.CerrarConexion()
        Return dts
    End Function

    '24/02/2012
    Public Function ConsultarPeriodicidadPorNombre(ByVal descripcion_peri As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarPeriodicidadPorNombre", descripcion_peri)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaPeriodosImportadosVariable(ByVal Codigo_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaPeriodosImportadosVariable", Codigo_var)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function NotificacionVariable(ByVal Codigo_var As String, ByVal codigo_pdo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ValorVariableNotificaciones", Codigo_var, codigo_pdo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaNotificacionesVariable(ByVal Codigo_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaNotificacionesVariable", Codigo_var)
        cnx.CerrarConexion()
        Return dts
    End Function

    '------------------------------------------------
    '16/03/12

    Public Function InsertarNivelAux(ByVal descripcion As String, ByVal abreviatura As String, ByVal usuario As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertarNivelAux", descripcion, abreviatura, usuario)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("nuevocodigo")
    End Function

    'InsertarConfiguracionDimensiones
    Public Function InsertarNivelesDim_para_NivelAux(ByVal codigo_naux As Integer, ByVal codigo_nid As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("InsertarNivelesDim_para_NivelAux", codigo_naux, codigo_nid)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Bandera").ToString
    End Function

    Public Function CargarListaPlan() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaPlanTreeView")
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function CargarListaPerspectivas(ByVal Codigo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaPerspectivasTreeView", Codigo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargarListaCentroCosto(ByVal Codigo_pla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaCecoTreeView", Codigo_pla)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargarListaObjetivos(ByVal Codigo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaObjetivosTreeView", Codigo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargarListaIndicadores(ByVal codigo_obj As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaIndicadoresTreeView", codigo_obj)
        cnx.CerrarConexion()
        Return dts
    End Function

    '20/03/2012 Consulta Niveles de Dimension según los codigo_aux enviados
    Public Function ConsultarNivelesDimension(ByVal codigo_aux1 As Integer, ByVal codigo_aux2 As Integer, ByVal codigo_aux3 As Integer, ByVal codigo_aux4 As Integer, ByVal codigo_aux5 As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarNivelesDimension_VariosAux", codigo_aux1, codigo_aux2, codigo_aux3, codigo_aux4, codigo_aux5)
        cnx.CerrarConexion()
        Return dts
    End Function


    '20/03/2012 Consulta Areas administrativas
    Public Function ConsultarAreasAdministrativas() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarAreasAdministrativas")
        cnx.CerrarConexion()
        Return dts
    End Function

    'Por mvillavicencio 20/03/12
    'Insertar el Detalle de Elementos del Nivel auxiliar configurado para una variable
    Public Sub InsertarDetalleConfiguracionSubvariable(ByVal codigo_var As String, ByVal codigo_naux As Integer, ByVal codigo_aaf As Integer, ByVal usuario As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("IND_InsertarDetalleConfiguracionSubvariable", codigo_var, codigo_naux, codigo_aaf, usuario)
        cnx.CerrarConexion()
        'Return dts.Rows(0).Item("Bandera").ToString
    End Sub

    'Por mvillavicencio 20/03/12
    'Consultar elementos del nivel aux Area administrativa, configurados para la variable
    Public Function ConsultarDetalleConfiguracionAux_Variable(ByVal codigo_var As String, ByVal codigo_naux As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ConsultarDetalleConfiguracionAux_Variable", codigo_var, codigo_naux)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarDetalleElementosSubvariableConfigurada(ByVal codigo_var As String, ByVal codigo_naux As Integer) As Data.DataTable 'Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarDetalleElementosSubvariableConfigurada", codigo_var, codigo_naux)
        cnx.CerrarConexion()
        'Return dts.Rows(0).Item("NumeroDim").ToString
        Return dts
    End Function

    Public Function ProcesaFormula(ByVal codigo_for As Integer, ByVal codigo_pdo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ProcesarFormulasIndividuales", codigo_for, codigo_pdo, "L")
        cnx.CerrarConexion()
        Return dts
    End Function

    '27.03.2012 xDguevara
    Public Function ListaInformacionFormulaPeriodo(ByVal Codigo_fp As Integer, ByVal codigo_pdo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_DetalleFormulaPeriodo", Codigo_fp, codigo_pdo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaDetalleFormula(ByVal Codigo_fp As Integer, ByVal codigo_pdo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaDetalleItemsFormula", Codigo_fp, codigo_pdo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarObjetivosSegunPerspectiva(ByVal año_obj As Integer, ByVal codigo_pla As Integer, ByVal codigo_cco As Integer, ByVal Codigo_pers As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListarObjetivosSegunPerspectiva", año_obj, codigo_pla, codigo_cco, Codigo_pers)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarObjetivosSegunPerspectivaEje(ByVal año_obj As Integer, _
                                                       ByVal codigo_pla As Integer, _
                                                       ByVal codigo_cco As Integer, _
                                                       ByVal Codigo_pers As Integer, _
                                                       ByVal codigo_eje As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListarObjetivosSegunPerspectivaEje", año_obj, codigo_pla, codigo_cco, Codigo_pers, codigo_eje)
        cnx.CerrarConexion()
        Return dts
    End Function


    'Por mvillavicencio
    Public Function ListaPerspectivasSegunPlanYCeCo(ByVal codigo_pla As Integer, ByVal codigo_ceco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaPerspectivasSegunPlanYCeCo", codigo_pla, codigo_ceco)
        cnx.CerrarConexion()
        Return dts
    End Function

    'xDguevara 
    Public Function ListaAnios(ByVal codigo_pla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaAños", codigo_pla)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaAniosObjetivosSegunPlan(ByVal codigo_pla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaAniosDelPlan", codigo_pla)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ImportarPlan(ByVal codigo_pla As Integer, _
                                 ByVal vCadenaOrigen As String, _
                                 ByVal vCadenaDestino As String, _
                                 ByVal vUsuario As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ImportarDataPlan", codigo_pla, vCadenaOrigen, vCadenaDestino, vUsuario)
        cnx.CerrarConexion()
        Return dts
    End Function

    '02/05/12
    Public Function ListarCentroCostosConPlan() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("IND_ListarCentroCostosConPlan")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaCecosPlan(ByVal codigo_pla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaCecosPlan", codigo_pla)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ListaPerspectivasPlanCeco(ByVal codigo_pla As Integer, ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaPerspectivasPlanCeco", codigo_pla, codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function

    '
    Public Function ListaEjes() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaEjes")
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ListaPerspectivasxPlanBusqueda(ByVal codigo_plan As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaPerspectivasxPlanBusqueda", codigo_plan)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaAñosObjetivosBusqueda(ByVal codigo_plan As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaAñosObjetivosBusqueda", codigo_plan)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaAniosObjetivosBusqueda(ByVal codigo_plan As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaAniosObjetivosBusqueda", codigo_plan)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaObjetivosBusqueda(ByVal codigo_plan As Integer, ByVal anio As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaObjetivosBusqueda", codigo_plan, anio)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaPerspectivasxPlanxCeco(ByVal codigo_plan As Integer, ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaPerspectivasxPlanxCeco", codigo_plan, codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaObjetivosxPlanxCecoxAnioxPers(ByVal codigo_plan As Integer, _
                                                       ByVal codigo_cco As Integer, _
                                                       ByVal anio As String, _
                                                       ByVal codigo_pers As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaPerspectivasxPlanxCeco", codigo_plan, codigo_cco, anio, codigo_pers)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaOperacionesIndicador() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaOperacionesIndicador")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function Avance_Indicador_Semestral(ByVal codigo_int As Integer, ByVal anio As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_Avance_Indicador_Semestral", codigo_int, anio)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaObjetivosFiltroFormula(ByVal codigo_pla As Integer, _
                                                ByVal codigo_Cco As Integer, _
                                                ByVal codigo_pers As Integer, _
                                                ByVal anio As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaObjetivosFiltroFormula", codigo_pla, codigo_Cco, codigo_pers, anio)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaIndicadoresFiltroFormula(ByVal codigo_pla As Integer, _
                                                ByVal codigo_Cco As Integer, _
                                                ByVal codigo_pers As Integer, _
                                                ByVal anio As String, _
                                                ByVal codigo_obj As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaIndicadoresFiltroFormula", codigo_pla, codigo_Cco, codigo_pers, anio, codigo_obj)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function VerificaValoresRegistradosVariable(ByVal Codigo_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_VerificaValoresRegistradosVariable", Codigo_var)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaCaregoria_cnf() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaCaregoria_cnf")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaVariables_cnf(ByVal codigo_cat As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaVariables_cnf", codigo_cat)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaUnidadNegocio_cnf() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaUnidadNegocio_cnf")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaSubUnidadNegocio_cnf(ByVal codigo_uneg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaSubUnidadNegocio_cnf", codigo_uneg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaCecos_cnf(ByVal codigo_uneg As Integer, ByVal codigo_subuneg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaCecos_cnf", codigo_uneg, codigo_subuneg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaJerarquiaVariables_cnf(ByVal codigo_var As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaJerarquiaVariables_cnf", codigo_var)
        cnx.CerrarConexion()
        Return dts
    End Function

    '
    Public Function InsertaNivelAccesoVariable(ByVal codigo_ceco As Integer, _
                                               ByVal nivel As Integer, _
                                               ByVal codigo_var As String, _
                                               ByVal codigo_aux As String, _
                                               ByVal codigo_dim As String, _
                                               ByVal codigo_sub As String, _
                                               ByVal operador As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertaNivelAccesoVariable", codigo_ceco, nivel, codigo_var, codigo_aux, codigo_dim, codigo_sub, operador)
        cnx.CerrarConexion()
        Return dts
    End Function

    '
    Public Function EliminaNivelAccesoVariable(ByVal codigo_var As String, ByVal Nivel As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminaNivelAccesoVariable", codigo_var, Nivel)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function BurcarCeco(ByVal centrocosto As String, _
                               ByVal codigo_subunidadnegocio As Integer, _
                               ByVal codigo_unidadnegocio As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_BurcarCeco_cnf", centrocosto, codigo_subunidadnegocio, codigo_unidadnegocio)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function PermisoGeneralVariables(ByVal codigo_cco As Integer, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_PermisoGeneralVariables", codigo_cco, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InformacionVariableCeco(ByVal tipo As String, ByVal codigo_var As String, _
                                            ByVal nivel As Integer, ByVal codigo_acc As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InformacionVariableCeco", tipo, codigo_var, nivel, codigo_acc)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaEjesObjetivos() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaEjesObjetivos")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function RegistrarEjesObjetivos(ByVal codigo_obj As Integer, ByVal codigo_eje As Integer, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_RegistrarEjesObjetivos", codigo_obj, codigo_eje, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaEjesAsignados(ByVal codigo_obj As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaEjesAsignados", codigo_obj)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargarPlanesRegistrados(ByVal ctf As Integer, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarPlanesRegistrados", ctf, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaAñosEvaluacion(ByVal codigo_plan As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaAñosEvaluacion", codigo_plan)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarCentroCostosEvaluacion() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarCentroCostosHP")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarPersonalEvaluacion(ByVal estado As String, ByVal codigo_cco As Integer) As Data.DataTable
        'Modificado el 24.09.2013 para evitar inconsistencias.
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ConsultarPersonalCentroCosto", estado, codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function

    'add 26.08.2013
    Public Function ListaPlanesAsigandosResponsable(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaPlanesAsigandosResponsable", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function RegistrarEvaluacionesPlan(ByVal codigo_plan As Integer, ByVal fecha_inicio As Date, ByVal fechafin As Date, ByVal anio As Integer, ByVal codigo_per As Integer, ByVal usuario As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_InsertarEvaluacionesPlan", codigo_plan, fecha_inicio, fechafin, anio, codigo_per, usuario)
        cnx.CerrarConexion()
        Return dts
    End Function

    'Add 23.08.2013
    Public Function RegistrarControlAccesoEvaluacion(ByVal codigo_eval As Integer, ByVal codigo_per As Integer, ByVal usuarioreg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_RegistrarControlAccesoEvaluacion", codigo_eval, codigo_per, usuarioreg)
        cnx.CerrarConexion()
        Return dts
    End Function

    'add 26.08.2013
    Public Function EliminarControlAccesoEvaluacion(ByVal codigo_cae As Integer, ByVal usuarioreg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarControlAccesoEvaluacion", codigo_cae, usuarioreg)
        cnx.CerrarConexion()
        Return dts
    End Function



    Public Function ModificarEvaluacionesPlan(ByVal codigo_eval As Integer, ByVal codigo_plan As Integer, ByVal fecha_inicio As Date, ByVal fechafin As Date, ByVal anio As Integer, ByVal codigo_per As Integer, ByVal usuario As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ModificarEvaluacionesPlan", codigo_eval, codigo_plan, fecha_inicio, fechafin, anio, codigo_per, usuario)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ListaEvaluaciones() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaEvaluacionesPlan")
        cnx.CerrarConexion()
        Return dts
    End Function

    'Add 23.08.2013
    Public Function ListaPlanesInformes(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaPlanesInformes", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarEvaluacionPlan(ByVal codigo_eval As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_EliminarEvaluacionPlan", codigo_eval)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaEvaluacionesPlanResponsable(ByVal codigo_per As Integer, ByVal ctf As Integer, ByVal anio As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaEvaluacionesPlanResponsable", codigo_per, ctf, anio)
        cnx.CerrarConexion()
        Return dts
    End Function

    'add 08.01.2014 --- solicitado por cardish
    Public Function CargarAñosRegistrados() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaAñosEvaluacionPlan")
        cnx.CerrarConexion()
        Return dts
    End Function



    'add26.08.2013
    Public Function ListaInformesEvaluacionAnual(ByVal codigo_per As Integer, ByVal ctf As Integer, ByVal anio As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_VistaEvaluacionAnual", codigo_per, ctf, anio)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function AgregarDocumentoPlan(ByVal codigo_eval As Integer, ByVal ruta As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_AgregarDocumentoPlan", codigo_eval, ruta)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Sub GuardarConfiguracionVariables(ByVal codigo_ceco As Integer, _
                                             ByVal cod_variable As String, _
                                             ByVal codigo_per As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("spIND_InsertaVariablesConfiguradas", codigo_ceco, cod_variable, codigo_per)
        cnx.CerrarConexion()
    End Sub

    Public Sub EliminarConfiguracionVariables(ByVal codigo_ceco As Integer, _
                                         ByVal cod_variable As String, _
                                         ByVal codigo_per As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("spIND_EliminarVariablesConfiguradas", codigo_ceco, cod_variable, codigo_per)
        cnx.CerrarConexion()
    End Sub

    '
    Public Function ListaUsuariosConfigurados(ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("spIND_ListaUsuariosConfigurados", codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function

    '01.03.2013
    Public Function ListaPlanUsuario(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaplanCeco", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaAniosSegunPlan(ByVal codigo_pla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaAniosSegunPlan", codigo_pla)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarCentroCostosVariablesPlan(ByVal descripcion_ceco As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListarCentroCostosVariablesPlan", descripcion_ceco)
        cnx.CerrarConexion()
        Return dts
    End Function

    '04.03.2013 xD
    Public Function CargarListaVariableTreeViewPorPlan(ByVal Codigo_cat As Integer, ByVal codigo_plan As Integer, ByVal codigo_cco As Integer, ByVal anio As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarListaVariableTreeViewPorPlan", Codigo_cat, codigo_plan, codigo_cco, anio)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargarListaSubVariablesTreeViewPorPlan(ByVal Codigo_var As String, ByVal codigo_cco As Integer, ByVal codigo_plan As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarListaSubVariablesTreeViewPorPlan", Codigo_var, codigo_cco, codigo_plan)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargarListaDimensionTreeViewPorPlan(ByVal Codigo_aux As String, ByVal codigo_cco As Integer, ByVal codigo_plan As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarListaDimensionTreeViewPorPlan", Codigo_aux, codigo_cco, codigo_plan)
        cnx.CerrarConexion()
        Return dts
    End Function

    '05.03.2013
    Public Function CargarListaSubDimensionTreeViewPorPlan(ByVal Codigo_dim As String, ByVal codigo_cco As Integer, ByVal codigo_pla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_CargarListaSubDimensionTreeViewPorPlan", Codigo_dim, codigo_cco, codigo_pla)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaGeneralVariablesSegunPlan(ByVal codigo_pla As String, ByVal codigo_cco As Integer, ByVal anio As Integer, ByVal estado As String, ByVal descripcion As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaVariablesxPlan", codigo_pla, codigo_cco, anio, estado, descripcion)
        cnx.CerrarConexion()
        Return dts
    End Function

End Class

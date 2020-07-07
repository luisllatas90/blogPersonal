Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsPlanOperativoAnual
    Private cnx As New ClsConectarDatos

    Public Function ListaPeis() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListarPlanes")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaPeisxResponsable(ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListaPeisRespyApoyo", id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ListaEjercicio() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PEI_ListarEjercicioPresupuestal")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaDireccionxPlan(ByVal codigo_pla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_CecosxPEI", codigo_pla)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaResponsablexArea(ByVal codigo_area As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ConsultaResponsablesxArea", codigo_area)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaPoas(ByVal codigo_poa As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListaPoas", codigo_poa)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaTipoActividad() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListaTipoActividad")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CeCosAsignadosxPOA(ByVal codigo_poa As Integer, ByVal codigo_tac As Integer, ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_CeCosAsignadosxPOA", codigo_poa, codigo_tac, codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargaProgramaPresupuestal(ByVal tipo As Integer, ByVal codigo_ppr As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_ConsultarProgramapresupuestal", tipo, codigo_ppr)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ArbolCentroCostos(ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ConsultarArbolCentroCostos", codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ConsultaPerspectivasxPEI(ByVal codigo_pei As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaPerspectivasSegunPlan", codigo_pei)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaObjetivosxPers(ByVal codigo_ppla As Integer, ByVal descripcion_ejp As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaObjetivosTreeView", codigo_ppla, descripcion_ejp)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaIndicadoresxObj(ByVal codigo_obj As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("IND_ListaIndicadoresTreeView", codigo_obj)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function POA_ListarPOAS(ByVal plan As String, ByVal ejecicio As String, ByVal vigencia As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListarPOAS", plan, ejecicio, vigencia)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarAsignacionPresupuestal(ByVal estado As String, ByVal codigo_pla As String, ByVal ejercicio As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        'dts = cnx.TraerDataTable("POA_ListaAsignacionPresupuestal", estado)
        dts = cnx.TraerDataTable("POA_ListaAsignacionPresupuestal", estado, codigo_pla, ejercicio)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function AtualizarPoa(ByVal codigo_ejp As Integer, ByVal codigo_pla As Integer, ByVal codigo_cco As Integer, ByVal nombre_poa As String, ByVal responsable As Integer, ByVal usuario As Integer, ByVal vigencia As Integer, ByVal codigo_poa As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ActualizarPlan", codigo_ejp, codigo_pla, codigo_cco, nombre_poa, responsable, usuario, vigencia, codigo_poa)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function EliminarPoa(ByVal codigo_poa As Integer, ByVal codigo_usu As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_EliminarPlan", codigo_poa, codigo_usu)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function
    Public Function AtualizarActividadPoa(ByVal abreviatura As String, ByVal descripcion As String, ByVal egresos As Decimal, ByVal ingresos As Decimal, _
                                          ByVal utilidad As Decimal, ByVal usuario As Integer, ByVal tipoactividad As Integer, ByVal responsable As Integer, _
                                          ByVal apoyo As Integer, ByVal codigo_cco As Integer, ByVal codigo_ppr As Integer, ByVal codigo_poa As Integer, _
                                          ByVal mes_ini As String, ByVal mes_fin As String, ByVal hdcodigo_ejp As Integer, ByVal hdcodigo_acp As Integer, _
                                          ByVal categoria As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ActualizarActividad", abreviatura, descripcion, egresos, ingresos, utilidad, usuario, tipoactividad, responsable, apoyo, codigo_cco, codigo_ppr, codigo_poa, mes_ini, mes_fin, hdcodigo_ejp, hdcodigo_acp, categoria)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function EliminarActividadPoa(ByVal codigo_poa As Integer, ByVal codigo_usu As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_EliminarActividad", codigo_poa, codigo_usu)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Sub ActualizarLimitePresupuestal(ByVal codigo_asp As Integer, ByVal codigo_poa As Integer, ByVal codigo_cco As Integer, ByVal codigo_tac As Integer, ByVal usuario_reg As Integer)
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ActualizarLimitePresupuestal", codigo_asp, codigo_poa, codigo_cco, codigo_tac, usuario_reg)
        cnx.CerrarConexion()
    End Sub

    Public Sub EliminarLimitePresupuestal(ByVal codigo_poa As Integer, ByVal usuario_mod As Integer)
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_EliminarLimitePresupuestal", codigo_poa, usuario_mod)
        cnx.CerrarConexion()
    End Sub

    Public Sub ActualizarMetaPOA(ByVal codigo_poa As Integer, ByVal limite_ingreso As Decimal, ByVal limite_egreso As Decimal, ByVal utilidad As Decimal)
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ActualizaMetas", codigo_poa, limite_ingreso, limite_egreso, utilidad)
        cnx.CerrarConexion()
    End Sub

    Public Function ListarCeCoAsignadoLimitePresupuestal(ByVal codigo_poa As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListarCeCoAsignadoLimitePresupuestal", codigo_poa)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_ListaPoasActividad(ByVal codigo_pla As Integer, ByVal codigo_ejp As Integer, ByVal codigo_per As Integer, ByVal codigo_tfu As Integer, ByVal opcion As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListaPoaActividad", codigo_pla, codigo_ejp, codigo_per, codigo_tfu, opcion)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaActividadesxPOA(ByVal codigo_poa As Integer, ByVal codigo_tac As Integer, ByVal codigo_per As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListaActividadesxPoa", codigo_poa, codigo_tac, codigo_per, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargaDatosActividad(ByVal codigo_acp As Integer, ByVal codigo_poa As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_CargaDatosActividad", codigo_acp, codigo_poa)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function IndicadoresAlineados(ByVal codigo_acp As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_IndicadoresAlineamiento", codigo_acp)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertarAlineamiento(ByVal usuario_reg As Integer, ByVal codigo_ind As Integer, ByVal codigo_acp As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_InsertarAlineamiento", usuario_reg, codigo_ind, codigo_acp)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function EliminarAlineamiento(ByVal usuario_reg As Integer, ByVal codigo_ali As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_EliminarAlineamiento", usuario_reg, codigo_ali)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_ListarPOASProgramas(ByVal plan As String, ByVal ejecicio As String, ByVal poa As String, ByVal actividad As String, ByVal opcion As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListarPOASProgramas", plan, ejecicio, poa, actividad, opcion)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertarObjetivosPOA(ByVal codigo_pobj As Integer, ByVal descripcion_pobj As String, ByVal usuario As Integer, ByVal estado_pobj As Integer, ByVal codigo_acp As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_InsertarObjetivosPOA", codigo_pobj, descripcion_pobj, usuario, estado_pobj, codigo_acp)

        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizaObjetivosPOA(ByVal usuario_reg As Integer, ByVal codigo_pobj As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ActualizarObjetivosPOA", usuario_reg, codigo_pobj)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_BuscarObjetivosPOA(ByVal codigo_pobj As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_BuscarObjetivosPOA", codigo_pobj)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_ListarActividadesObjetivos(ByVal codigo_plan As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListarActividadesObjetivos", codigo_plan)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_ListaTipoActividad() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListarTipoActividad")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_ListarObjetivosIndicadores(ByVal codigo_acp As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListarObjetivosIndicadores", codigo_acp)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_InsertarIndicador(ByVal codigo_pind As Integer, ByVal descripcion_pind As String, ByVal usuario As Integer, ByVal codigo_pobj As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_InsertarIndicadorPOA", codigo_pind, descripcion_pind, usuario, codigo_pobj)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function POA_lista_tipoActividad() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_lista_tipoActividad")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_ListaPersonalxCeco(ByVal codigo_cco As Integer, ByVal opcion As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_PersonalxCeco", codigo_cco, opcion)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function CrearCostoTemporalPOA(ByVal descripcion_cco As String, ByVal codigo_poa As Integer, ByVal codigo_tac As Integer, ByVal codigo_raiz As Integer, _
                                          ByVal codigo_ppr As Integer, ByVal responsable As Integer, ByVal usuario_reg As Integer, ByVal codigo_cpa As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_CreaCentroCostoTemporal", descripcion_cco, codigo_poa, codigo_tac, codigo_raiz, codigo_ppr, responsable, usuario_reg, codigo_cpa)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Sub EliminarIndicador(ByVal codigo_pind As Integer, ByVal usuario As Integer)
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_EliminarIndicador", codigo_pind, usuario)
        cnx.CerrarConexion()
    End Sub

    Public Sub EliminarObjetivo(ByVal codigo_pobj As Integer, ByVal usuario As Integer)
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_EliminarObjetivo", codigo_pobj, usuario)
        cnx.CerrarConexion()
    End Sub


    Public Function ConsultaCecoxcodigo(ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ConsultaCecoxcodigo", codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CabeceraEvaluarAlineamiento(ByVal codigo_pla As String, ByVal codigo_poa As Integer, ByVal codigo_ejp As String, ByVal codigo_iep As String, ByVal estado As String, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_CabeceraEvaluarAlineamiento", codigo_pla, codigo_poa, codigo_ejp, codigo_iep, estado, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EvaluarAlineamiento(ByVal codigo_pla As String, ByVal codigo_poa As Integer, ByVal codigo_ejp As String, ByVal codigo_iep As String, ByVal estado As String, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_EvaluarAlineamientoActividades", codigo_pla, codigo_poa, codigo_ejp, codigo_iep, estado, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EvaluarAlineamiento_V2(ByVal codigo_pla As String, ByVal codigo_poa As Integer, ByVal codigo_ejp As String, ByVal codigo_iep As String, ByVal estado As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_EvaluarAlineamientoActividades_V2", codigo_pla, codigo_poa, codigo_ejp, codigo_iep, estado)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EvaluarPresupuesto(ByVal codigo_pla As String, ByVal codigo_poa As Integer, ByVal codigo_ejp As String, ByVal codigo_iep As String, ByVal estado As String, ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_EvaluarPresupuestoActividad", codigo_pla, codigo_poa, codigo_ejp, codigo_iep, estado, id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarPresupuestoProgProy(ByVal codigo_pla As String, ByVal codigo_poa As Integer, ByVal codigo_ejp As String, ByVal codigo_iep As String, ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListaPresupuestoProgProy", codigo_pla, codigo_poa, codigo_ejp, codigo_iep, id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertarMetasObjetivosPOA(ByVal codigo_mobj As Integer, ByVal meta_mobj As Decimal, ByVal tiempo_mobj As String, ByVal usuario_reg As Integer, ByVal codigo_pobj As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_InsertarMetaObjetivosPOA", codigo_mobj, meta_mobj, tiempo_mobj, usuario_reg, codigo_pobj)
        cnx.CerrarConexion()
        Return dts
    End Function

    'POA_buscar_objetivo_indicado
    Public Function POA_buscar_objetivo_indicador(ByVal codigo_acp As Integer, ByVal codigo_pobj As Integer, ByVal codigo_pind As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_buscar_objetivo_indicador", codigo_acp, codigo_pobj, codigo_pind)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_Actualizar_Metas_Objetivos(ByVal usuario As Integer, ByVal codigo_pobj As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_Actualizar_Metas_Objetivos", codigo_pobj, usuario)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_InsertaDetalleActividad(ByVal codigo_dap As Integer, ByVal descripcion_dap As String, ByVal meta_dap As Decimal, ByVal fecini_dap As String, ByVal fecfin_dap As String, ByVal estado_dap As String, ByVal usuario As Integer, ByVal responsable_dap As Integer, ByVal codigo_acp As Integer, ByVal requiere_pto As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_InsertaDetalleActividad", codigo_dap, descripcion_dap, meta_dap, fecini_dap, fecfin_dap, estado_dap, usuario, responsable_dap, codigo_acp, requiere_pto)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_ListarDetalleActividad(ByVal codigo_acp As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListarDetalleActividad", codigo_acp)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_DatosDetalleActividad(ByVal codigo_dap As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_DatosDetalleActividad", codigo_dap)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_ListaProgProyPorCodDap(ByVal codigo_dap As Integer, ByVal codigo_acp As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListaProgProyPorCodDap", codigo_dap, codigo_acp)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_MoverDetalleActividad(ByVal codigo_acp As Integer, ByVal codigo_dap As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_MoverDetalleActividad", codigo_acp, codigo_dap)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function


    Public Function POA_ActualizarDetalleActividad(ByVal codigo_acp As Integer, ByVal usuario As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ActualizarDetalleActividad", codigo_acp, usuario)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_listar_mesesActividad(ByVal codigo_acp As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_listar_mesesActividad", codigo_acp)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InstanciaEstadoActividad(ByVal codigo_acp As Integer, ByVal codigo_InstanciaEstado As Integer, ByVal usuario_reg As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_EstadoInstanciaActividad", codigo_acp, codigo_InstanciaEstado, usuario_reg)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function
    Public Function POA_ActualizarObservacion(ByVal codigo_acp As Integer, ByVal descripcion_obs As String, ByVal usuario_reg As Integer, ByVal codigo_obs As Integer, ByVal codigo_iep As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ActualizarObservacion", codigo_acp, descripcion_obs, usuario_reg, codigo_obs, codigo_iep)
        ''codigo_obs = 0 ----> Inserta Observacion
        ''codigo_obs>0 ------> Corrigio observaciones
        ''codio_iep -> Codigo InstanciaEstado_POA
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function
    Public Function ListaObservaciones(ByVal codigo_acp As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListaObservaciones", codigo_acp)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListaPoasxInstanciaEstado(ByVal codigo_pla As Integer, ByVal codigo_ejp As Integer, ByVal InstanciaEstado As String, ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()


        dts = cnx.TraerDataTable("POA_ListaPoasxInstanciaEstado", codigo_pla, codigo_ejp, InstanciaEstado, id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaPoasxPEIxEjercicio(ByVal codigo_pla As Integer, ByVal codigo_ejp As Integer, ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListaPOASxPEIxEJP", codigo_pla, codigo_ejp, id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaAsignaFecha(ByVal codigo_pla As Integer, ByVal codigo_ejp As Integer, ByVal InstanciaEstado As String, ByVal estado As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListaAsignaFecha", codigo_pla, codigo_ejp, InstanciaEstado, estado)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargaDatosDetActividad(ByVal codigo_dap As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_CargaDatosDetActividad", codigo_dap)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function AsignaFechaRevision(ByVal codigo_dap As Integer, ByVal fecha As String, ByVal usuario_asig As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_AsignaFechaRevision", codigo_dap, fecha, usuario_asig)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_EliminarDetalleActividad(ByVal codigo_dap As Integer, ByVal usuario_mod As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_EliminarDetalleActividad", codigo_dap, usuario_mod)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_VerificarDetalleActividadPresupuesto(ByVal codigo_acp As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_VerificarDetalleActividadPresupuesto", codigo_acp)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_Elimina_limitePresupuestal(ByVal codigo_poa As Integer, ByVal codigo_asp As Integer, ByVal usuario As Integer, ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_Elimina_limitePresupuestal", codigo_poa, codigo_asp, usuario, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_Elimina_DetalleActividad(ByVal codigo_acp As Integer, ByVal codigo_dap As Integer, ByVal usuario As Integer, ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_Elimina_DetalleActividad", codigo_acp, codigo_dap, usuario, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_verificar_CECO_ActividadPoa(ByVal codigo_poa As Integer, ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_verificar_CECO_ActividadPoa", codigo_poa, codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaAsignaAporte(ByVal codigo_pla As Integer, ByVal codigo_ejp As Integer, ByVal codigo_poa As Integer, ByVal estado As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_AsignaAporteIndicadores", codigo_pla, codigo_ejp, codigo_poa, estado)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_ConsultarNombrePOA(ByVal codigo_poa As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ConsultarNombrePOA", codigo_poa)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_EliminarMetaObjetivo(ByVal codigo_pind As Integer, ByVal usuario As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_EliminarMetaObjetivo", codigo_pind, usuario)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_VerificarIndicador(ByVal codigo_pobj As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_VerificarIndicador", codigo_pobj)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_avanceIndicador(ByVal codigo_pla As String, ByVal codigo_poa As String, ByVal codigo_Ejp As String, ByVal codigo_acp As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_avanceIndicador", codigo_pla, codigo_poa, codigo_Ejp, codigo_acp)

        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_tipoActividad(ByVal codigo_poa As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_tipoActividad", codigo_poa)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_ListaPoas_Plan_Ejercicio(ByVal codigo_pla As String, ByVal codigo_ejp As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListaPoas_Plan_Ejercicio", codigo_pla, codigo_ejp)
        cnx.CerrarConexion()
        Return dts
    End Function

    'Public Function POA_InsertarAvanceIndicadorPOA(ByVal codigo_aip As Integer, ByVal avance_aip As Decimal, ByVal tiempo_aip As String, ByVal usuario As Integer, ByVal codigo_mip As Integer) As Data.DataTable
    '    Dim dts As New Data.DataTable
    '    cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    cnx.AbrirConexion()
    '    dts = cnx.TraerDataTable("POA_InsertarAvanceIndicadorPOA", codigo_aip, avance_aip, tiempo_aip, usuario, codigo_mip)

    '    cnx.CerrarConexion()
    '    Return dts
    'End Function

    'POA_verExiste_AvanceIndicador 117

    Public Function POA_verExiste_AvanceIndicador(ByVal codigo_aip As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_verExiste_AvanceIndicador", codigo_aip)

        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_InsertarAvanceIndicadorPOA(ByVal avance_aip As Decimal, ByVal tiempo_aip As String, ByVal usuario As Integer, ByVal codigo_mip As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_InsertarAvanceIndicadorPOA", avance_aip, tiempo_aip, usuario, codigo_mip)

        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_ActualizarAvanceIndicadorPOA(ByVal codigo_aip As Integer, ByVal avance_aip As Decimal, ByVal tiempo_aip As String, ByVal usuario As Integer, ByVal codigo_mip As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ActualizarAvanceIndicadorPOA", codigo_aip, avance_aip, tiempo_aip, usuario, codigo_mip)

        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_ValidarPresupuesto(ByVal codigo_acp As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ValidaPresupuesto", codigo_acp)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function POA_ConsultaImportesIngresoEgreso(ByVal codigo_poa As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ConsultaImportesIngresoEgreso", codigo_poa)

        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_EliminarObjetivosIndicadores(ByVal codigo_pobj As Integer, ByVal usuario As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_EliminarObjetivosIndicadores", codigo_pobj, usuario)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function POA_VerificarLimitePresupuestalEliminado(ByVal codigo_poa As Integer, ByVal codigo_cco As Integer) As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_VerificarLimitePresupuestalEliminado", codigo_poa, codigo_cco)

        cnx.CerrarConexion()
        Return dts.Rows(0).Item("dato").ToString
    End Function

    Public Function CargaEstadosEvalPto(ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_CargaEstadosEvalPto", id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_PermisosEdicionPresupuesto(ByVal codigo_pla As Integer, ByVal codigo_poa As Integer, ByVal codigo_ejp As Integer, ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_PermisosEdicionPresupuesto", codigo_pla, codigo_poa, codigo_ejp, id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function
    '
    Public Function POA_ActivaEdicionPresupuesto(ByVal codigo_acp As Integer, ByVal autoriza As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ActivaEdicionPresupuesto", codigo_acp, autoriza)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_verificarPtoyLimites(ByVal codigo_acp As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_verificarPtoyLimites", codigo_acp)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function POA_EnvioMails(ByVal codigo_acp As Integer, ByVal codigo_de As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_EnvioMails", codigo_acp, codigo_de)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_verificarMetaIndicador(ByVal codigo_pobj As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_verificarMetaIndicador", codigo_pobj)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("mensaje").ToString
    End Function

    Public Function POA_AsignarMetasPorIniciar_v1(ByVal codigo_poa As Integer, ByVal tipo As String, ByVal estado As String, ByVal id As Integer, ByVal ctf As Integer, ByVal codigo_pla As Integer, ByVal codigo_ejp As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_AsignarMetasPorIniciar_v1", codigo_poa, tipo, estado, id, ctf, codigo_pla, codigo_ejp)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_AsignarMetasPorIniciar(ByVal codigo_poa As Integer, ByVal tipo As String, ByVal estado As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_AsignarMetasPorIniciar", codigo_poa, tipo, estado)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_InicioActividades(ByVal codigo_iac As Integer, ByVal estadoInicial_iac As Integer, ByVal estadoFinal_iac As Integer, ByVal usuario_iac As Integer, ByVal codigo_dap As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_InicioActividades", codigo_iac, estadoInicial_iac, estadoFinal_iac, usuario_iac, codigo_dap)

        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function POA_ListaPeisVigentes(ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListaPeisVigentes", id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    'POA_ListaPoasxPEIyEjercicio 31, 154, 29 ,8
    Public Function POA_ListaPoasxPEIyEjercicio(ByVal id As Integer, ByVal ctf As Integer, ByVal codigo_pla As Integer, ByVal codigo_ejp As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListaPoasxPEIyEjercicioMetas", id, ctf, codigo_pla, codigo_ejp)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_ListaPeisVigentesMetas(ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListaPeisVigentesMetas", id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_CategoriaProgProy(ByVal codigo_cap As String, ByVal nombre_cap As String, ByVal estado_cap As Integer, ByVal usuario_reg As Integer, _
                                    ByVal fecha_reg As String, ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_CategoriaProgProy", codigo_cap, nombre_cap, estado_cap, usuario_reg, fecha_reg, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_CategoriaActividad(ByVal codigo_cat As Integer, ByVal nombre_cat As String, ByVal estado_cat As Integer, ByVal usuario_reg As Integer, _
                                    ByVal fecha_reg As String, ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_CategoriaActividad", codigo_cat, nombre_cat, estado_cat, usuario_reg, fecha_reg, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_CategoriaProgProyActividad(ByVal codigo_cpa As Integer, ByVal codigo_cap As String, ByVal codigo_cat As String, ByVal usuario_reg As Integer, _
                                   ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_CategoriaProgProyActividad", codigo_cpa, codigo_cap, codigo_cat, usuario_reg, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_VerificarCategoriaActividades(ByVal codigo_acp As Integer, ByVal codigo_cap As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_VerificarCategoriaActividades", codigo_acp, codigo_cap)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function POA_ActualizarCategoriaCeCo(ByVal codigo_cco As Integer, ByVal codigo_cpa As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ActualizarCategoriaCeCo", codigo_cco, codigo_cpa)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_AreasCategoria(ByVal codigo_aca As Integer, ByVal codigo_poa As String, ByVal codigo_cap As String, ByVal usuario_reg As Integer, _
                                   ByVal tipo As String, ByVal estado_aca As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_AreasCategoria", codigo_aca, codigo_poa, codigo_cap, usuario_reg, tipo, estado_aca)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_ClonarCaterogiasPOA(ByVal codigo_poa_act As Integer, ByVal codigo_poa_new As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ClonarCaterogiasPOA", codigo_poa_act, codigo_poa_new, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_CategoriaItem(ByVal codigo_cip As Integer, ByVal codigo_cat As String, ByVal codigoitem As String, ByVal precio_cip As Decimal, _
                                      ByVal codigo_art As String, ByVal tipo_cip As String, ByVal movimiento_cip As String, ByVal estado_cip As Integer, _
                                      ByVal usuario_reg As Integer, ByVal tipo As String, ByVal texto As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_CategoriaItem", codigo_cip, codigo_cat, codigoitem, precio_cip, codigo_art, tipo_cip, movimiento_cip, estado_cip, usuario_reg, tipo, texto)
        cnx.CerrarConexion()
        Return dts

    End Function

    Public Function POA_ClonarActividadPOA(ByVal codigo_act_act As Integer, ByVal codigo_act_new As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ClonarCActividadPOA", codigo_act_act, codigo_act_new, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function

    '' Inserta en módulo de Presupuesto
    Public Function PRESU_ListaItemActividad(ByVal codigo_acp As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_ListaItemActividad", codigo_acp)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function AgregarPresupuesto_V2(ByVal codigo_ejp As Int64, ByVal codigo_cco As Int32, ByVal codigo_art As Int32, _
                                       ByVal codigo_rub As Int32, ByVal codigo_cplla As Int32, _
                                       ByVal detalledescripcion As String, ByVal iduni As Int16, _
                                       ByVal preciounit As Decimal, ByVal cantidad As Decimal, _
                                       ByVal vigencia As Int16, ByVal tipo As String, ByVal codigo_per As Int32, _
                                       ByVal indicocantidad As Byte, ByVal forzar As Byte, ByVal codigo_dap As Int32) As Data.DataTable

        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_RegistrarPresupuesto_V2", codigo_ejp, codigo_cco, codigo_art, codigo_rub, codigo_cplla, detalledescripcion, _
                                 iduni, preciounit, cantidad, vigencia, tipo, codigo_per, indicocantidad, forzar, codigo_dap, 0)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function AgregarDetalleEjecucion_V2(ByVal codigo_dpr As Int64, ByVal descripcion_dej As String, _
                                           ByVal precio_dej As Decimal, ByVal cantidad_dej As Decimal) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_AgregarDetalleEjecucion_V2", codigo_dpr, descripcion_dej, precio_dej, cantidad_dej, 0)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_VerificaItemPresupuesto(ByVal codigo_dap As Integer) As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_VerificaItemPresupuesto", codigo_dap)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("nItem").ToString
    End Function

    Public Function ConsultaResponsablesActividad(ByVal tipo As String, ByVal codigo_acp As Integer, ByVal param1 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ResponsablesProgramaProyecto", tipo, codigo_acp, param1)
        cnx.CerrarConexion()
        Return dts
    End Function

    '======================= Agregado fatima.vasquez : 09-08-18 =============================================
    Public Function POA_verificarCentroCosto(ByVal codigo_poa As Integer, ByVal codigo_acp As Integer) As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_verificarCentroCosto", codigo_poa, codigo_acp)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje")
    End Function
    '======================= Fin fatima.vasquez : 09-08-18 =============================================


    '======================= Agregado Hcano : 06-12-18 =============================================
    Public Function POA_ResponsablePOA(ByVal codigo_poa As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ResponsablePOA", codigo_poa)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_CambiarResponsablePOA(ByVal codigo_poa As Integer, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_CambiarResponsablePOA", codigo_poa, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function
    '======================= Fin HCano: 06-12-18 =============================================

    Public Function POA_ConsultarCentroCosto(ByVal tipo As Integer, ByVal codigo_per As Integer, ByVal centrocostos As String, ByVal accion As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_ConsultarCentroCostosXPermisos_v3", tipo, codigo_per, centrocostos, accion)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function POA_AgregarCentroCostoAntiguoPOA(ByVal codigo_cco As Integer, ByVal codigo_ejp As Integer, ByVal codigo_poa As Integer, ByVal apoyo As Integer, ByVal usuario_Reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_AgregarCentroCostoAntiguoEnPOA", codigo_cco, codigo_ejp, codigo_poa, apoyo, usuario_Reg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function POA_ConsultarProgPresupuestalxCategoria(ByVal codigo_cap As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ListarCategoriaProgPresupuestal", codigo_cap)
        cnx.CerrarConexion()
        Return dts
    End Function

End Class

Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsInvestigacion
    Private cnx As New ClsConectarDatos

    Public Sub AbrirTransaccionCnx()
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.IniciarTransaccion()
    End Sub

    Public Sub CerrarTransaccionCnx()
        cnx.TerminarTransaccion()
    End Sub

    Public Function ListaEstadoRevisionInvestigacion(ByVal codigo_investigacion As Integer, ByVal codigo_instancia As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("inv_EstadosRevisionInvestigacion", codigo_investigacion, codigo_instancia)
        cnx.CerrarConexion()
        Return dts
    End Function

    'Agregado el 02.07.2013
    Public Function VistaResumenPerfil(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("inv_panelresumeninvestigaciones", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    'Agregado el 05.07.2013
    Public Function VistaResumenPerfilCoorRev(ByVal codigo_per As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("inv_panelresumeninvestigacionesporestado", codigo_per, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    'Agregado el 24.06.2013, para mostrar filtrar las investigaciones por parte de los revisores.
    Public Function CargarListaEstadoInvestigacion() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("inv_estado_investigacion")
        cnx.CerrarConexion()
        Return dts
    End Function

    '
    Public Function ListaInstanciasFiltro() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("Inv_ListaInstanciasFiltro")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaCentroCostos(ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaCentroCostos", codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaPersonal(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaPersonal", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaInstancia(ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaInstancia", id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaComite(ByVal id As Integer, ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaComite", id, codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaArea(ByVal id As Integer, ByVal codigo_comite As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaArea", id, codigo_comite)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultaLinea(ByVal id As Integer, ByVal codigo_area As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaLinea", id, codigo_area)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaComiteMiembros(ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaComiteMiembros", id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaInvestigaciones(ByVal id As Integer, ByVal ctf As Integer, ByVal codigo_per As Integer, ByVal titulo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaInvestigaciones", id, ctf, codigo_per, titulo)
        cnx.CerrarConexion()
        Return dts
    End Function

    'Nuevo 24.06.2013
    Public Function ConsultaInvestigacionesPorEstadoRevision(ByVal id As Integer, ByVal ctf As Integer, ByVal codigo_per As Integer, ByVal investigacion_estado_id As Integer, ByVal titulo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaInvestigacionesPorEstadoRevision", id, ctf, codigo_per, investigacion_estado_id, titulo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaDatosInvestigaciones(ByVal id As Integer, ByVal ctf As Integer, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaDatosInvestigaciones", id, ctf, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaVersion(ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaVersion", id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaNroVersiones(ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaNroVersiones", id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaBitacora(ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaBitacora", id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaResumen(ByVal id As Integer, ByVal activo As Boolean, ByVal id_revisor As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaResumen", id, activo, id_revisor)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaObservacion(ByVal id As Integer, ByVal avance_id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaObservacion", id, avance_id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function AgregaComite(ByVal nombre As String, ByVal personal_id As Integer, ByVal codigo_cco As Integer, ByVal instancia_id As Integer, ByVal usureg As String, ByVal activo As Boolean) As Integer
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer

            cnx.Ejecutar("INV_AgregaComite", nombre, personal_id, codigo_cco, instancia_id, usureg, activo, 0).copyto(valoresdevueltos, 0)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 0
        End Try
    End Function

    Public Sub AgregaMiembroComite(ByVal codigo_per As Integer, ByVal coordinador As Boolean, ByVal comite_id As Integer, ByVal instancia_id As Integer)
        cnx.Ejecutar("INV_AgregaMiembroComite", codigo_per, coordinador, comite_id, instancia_id)
    End Sub

    Public Sub AgregaObservacion(ByVal investigacion_id As Integer, ByVal investigacion_avance_id As Integer, ByVal descripcion As String, ByVal urlarchivo As String, ByVal investigacion_revisor_id As Integer, ByVal usureg As String)
        cnx.Ejecutar("INV_InsertaObservacion", investigacion_id, investigacion_avance_id, descripcion, urlarchivo, investigacion_revisor_id, usureg)
    End Sub

    Public Sub AgregaHito(ByVal investigacion_id As Integer, ByVal descripcion As String, ByVal fecha As String, ByVal usureg As String)
        cnx.Ejecutar("INV_AgregaHito", investigacion_id, descripcion, CDate(fecha), usureg)
    End Sub

    Public Sub ActualizaHito(ByVal id As Integer, ByVal investigacion_id As Integer, ByVal descripcion As String, ByVal fecha As String)
        cnx.Ejecutar("INV_ActualizaHito", id, investigacion_id, descripcion, CDate(fecha))
    End Sub

    Public Sub EliminaHito(ByVal id As Integer, ByVal investigacion_id As Integer)
        cnx.Ejecutar("INV_EliminaHito", id, investigacion_id)
    End Sub

    Public Sub AgregaAvance(ByVal investigacion_id As Integer, ByVal investigacion_hito_id As Integer, ByVal ruta As String, ByVal fecha As String, ByVal usureg As String)
        cnx.Ejecutar("INV_AgregaAvance", investigacion_id, investigacion_hito_id, ruta, CDate(fecha), usureg)
    End Sub

    Public Sub EliminaAvance(ByVal id As Integer, ByVal investigacion_id As Integer, ByVal investigacion_hito_id As Integer)
        cnx.Ejecutar("INV_EliminaAvance", id, investigacion_id, investigacion_hito_id)
    End Sub


    Public Sub AgregaResponsablesInvestigacion(ByVal codigo_per As Integer, _
                                               ByVal codigo_alu As Integer, _
                                               ByVal investigacion_id As Integer, _
                                               ByVal investigacion_participacion_id As Integer, _
                                               ByVal usureg As Integer, _
                                               ByVal tipo As String)
        cnx.Ejecutar("INV_AgregaResponsablesInvestigacion", codigo_per, _
                     codigo_alu, _
                     investigacion_id, _
                     investigacion_participacion_id, _
                     usureg, _
                     tipo)
    End Sub

    Public Sub EliminaMiembroComite(ByVal codigo_per As Integer, ByVal comite_id As Integer)
        cnx.Ejecutar("INV_EliminaMiembroComite", codigo_per, comite_id)
    End Sub

    Public Sub EliminaResponsablesInvestigacacion(ByVal codigo_per As Integer, ByVal investigacion_id As Integer, ByVal tipo As String)
        cnx.Ejecutar("INV_EliminaResponsablesInvestigacacion", codigo_per, investigacion_id, tipo)
    End Sub

    Public Sub ModificaComite(ByVal comite_id As Integer, ByVal nombre As String, ByVal codigo_cco As Integer, ByVal instancia_id As Integer, ByVal activo As Boolean)
        cnx.Ejecutar("INV_ModificaComite", comite_id, nombre, codigo_cco, instancia_id, activo)
    End Sub

    

    Public Sub AsignaCoordinadorComite(ByVal codigo_per As Integer, ByVal comite_id As Integer)
        cnx.Ejecutar("INV_AsignaCoordinadorComite", codigo_per, comite_id)
    End Sub


    Public Function InsertarArea(ByVal nombre As String, ByVal comite_id As Integer, ByVal usureg As Integer) As Integer
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer
            cnx.Ejecutar("INV_InsertarArea", nombre, comite_id, usureg, 0).copyto(valoresdevueltos, 0)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 0
        End Try
    End Function

    Public Function InsertarLinea(ByVal nombre As String, ByVal area_id As Integer, ByVal usureg As Integer) As Integer
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer
            cnx.Ejecutar("INV_InsertarLinea", nombre, area_id, usureg, 0).copyto(valoresdevueltos, 0)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 0
        End Try
    End Function

    Public Sub ActualizarArea(ByVal nombre As String, ByVal area_id As Integer, ByVal usureg As Integer)
        cnx.Ejecutar("inv_ActualizarArea", area_id, nombre, usureg)
    End Sub

    Public Sub ActualizarLinea(ByVal nombre As String, ByVal linea_id As Integer, ByVal usureg As Integer)
        cnx.Ejecutar("inv_ActualizarLinea", linea_id, nombre, usureg)
    End Sub
    Public Sub EliminarArea(ByVal area_id As Integer, ByVal usureg As Integer)
        cnx.Ejecutar("inv_EliminarArea", area_id, usureg)
    End Sub
    Public Sub EliminarLinea(ByVal linea_id As Integer, ByVal usureg As Integer)
        cnx.Ejecutar("inv_EliminarLinea", linea_id, usureg)
    End Sub
    Public Sub RegistrarResolucion(ByVal id As Long, ByVal nroresolucion As String, ByVal rutasolucion As String)
        cnx.Ejecutar("INV_RegistrarResolucion", id, nroresolucion, rutasolucion)
    End Sub
    Public Sub RegistrarInforme(ByVal id As Long, ByVal rutainforme As String)
        cnx.Ejecutar("INV_RegistrarInforme", id, rutainforme)
    End Sub

    Public Sub ModificarInvestigaciones(ByVal tipo As String, ByVal id_investigacion As Integer, _
                                        ByVal titulo As String, _
                                        ByVal fechaInicio As Date, _
                                        ByVal fechaFin As Date, _
                                        ByVal presupuesto As String, _
                                        ByVal tipoFinanciamiento As String, _
                                        ByVal beneficiarios As String, _
                                        ByVal resumen As String, _
                                        ByVal Ambito As String, _
                                        ByVal invetigacion_tipo_id As Integer, _
                                        ByVal codigo_per As Integer)

        cnx.Ejecutar("INV_ModificarInvestigaciones", tipo, id_investigacion, _
                                                     titulo, _
                                                     fechaInicio, _
                                                     fechaFin, _
                                                     presupuesto, _
                                                     tipoFinanciamiento, _
                                                     beneficiarios, _
                                                     resumen, _
                                                     Ambito, _
                                                     invetigacion_tipo_id, _
                                                     codigo_per)
    End Sub


    'xDuevara 14.08.2012
    Public Function ListaLineaInvestigacion() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListaLineaInvestigacion")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaInstancias() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListaInstancias")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaEtapaInvestigacion() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListaEtapaInvestigacion")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaTipoInvestigacion() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListaTipoInvestigacion")
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ListaParticipantesinvestigacion() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListaParticipantesinvestigacion")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaTipoPersonal(ByVal vTipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListaTipoPersonal", vTipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertarInvestigaciones(ByVal titulo As String, _
                                            ByVal fechaInicio As Date, _
                                            ByVal fechaFin As Date, _
                                            ByVal presupuesto As String, _
                                            ByVal tipoFinanciamiento As String, _
                                            ByVal beneficiarios As String, _
                                            ByVal resumen As String, _
                                            ByVal Ambito As String, _
                                            ByVal investigacion_linea_id As Integer, _
                                            ByVal invetigacion_etapa_id As Integer, _
                                            ByVal invetigacion_tipo_id As Integer, _
                                            ByVal invetigacion_instancia_id As Integer, _
                                            ByVal codigo_per As Integer, _
                                            ByVal id As Integer) As Integer
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer
            cnx.Ejecutar("INV_InsertarInvestigaciones", _
                         titulo, _
                         fechaInicio, _
                         fechaFin, _
                         presupuesto, _
                         tipoFinanciamiento, _
                         beneficiarios, _
                         resumen, _
                         Ambito, _
                         investigacion_linea_id, _
                         invetigacion_etapa_id, _
                         invetigacion_tipo_id, _
                         invetigacion_instancia_id, _
                         codigo_per, _
                         0).copyto(valoresdevueltos, id)

            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 0
        End Try
    End Function

    Public Sub AgregaRutaArchivoInvestigacion(ByVal investigacion_id As Integer, ByVal ruta As String, ByVal tipo As String)
        cnx.Ejecutar("INV_AgregaRutaArchivoInvestigacion", investigacion_id, ruta, tipo)
    End Sub

    Public Function MostrarRegistroID(ByVal id_investigacion As Integer, ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        If tipo = "MV" Then
            dts = cnx.TraerDataTable("INV_ConsultaInvestigacionVersionID", id_investigacion)
        Else
            dts = cnx.TraerDataTable("INV_ConsultaInvestigacionID", id_investigacion)
        End If
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaResponsableInvestigacion(ByVal id_investigacion As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListaResponsableInvestigacion", id_investigacion)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaComites() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListaComites")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaAreasPorComite(ByVal comite_id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListaAreasPorComite", comite_id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaLineasPorArea(ByVal area_id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListaLineasPorArea", area_id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EjecutaEnvioInvestigacion(ByVal id_investigacion As Integer, ByVal codigo_per As Integer, ByVal rpt As Integer) As Integer
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer
            cnx.Ejecutar("INV_EjecutaEnvioInvestigacion", id_investigacion, codigo_per, 0).copyto(valoresdevueltos, rpt)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 0
        End Try
    End Function

    'Agregado 08.07.2013 -> solicitado por Juan Carlos Iberico
    Public Function EjecutaEliminacionInvestigacion(ByVal id_investigacion As Integer, ByVal codigo_per As Integer, ByVal rpt As Integer) As Integer
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer
            cnx.Ejecutar("INV_EjecutaEliminacionInvestigacion", id_investigacion, codigo_per, 0).copyto(valoresdevueltos, rpt)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 0
        End Try
    End Function


    Public Function EjecutaEnvioInvestigacionVersiones(ByVal id_investigacion As Integer, ByVal codigo_per As Integer, ByVal rpt As Integer) As Integer
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer
            cnx.Ejecutar("INV_EjecutaEnvioInvestigacionVersion", id_investigacion, codigo_per, 0).copyto(valoresdevueltos, rpt)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 0
        End Try
    End Function


    Public Function ListaInvestigacionRevisores(ByVal id_investigacion As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListaInvestigacionRevisores", id_investigacion, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CambiaInstanciaInvestigacion(ByVal id_investigacion As Integer, ByVal ctf As Integer, ByVal codigo_per As Integer, ByVal rpt As Integer) As Integer
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer
            cnx.Ejecutar("INV_CambiaInstanciaInvestigacion", id_investigacion, ctf, codigo_per, 0).copyto(valoresdevueltos, rpt)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 0
        End Try
    End Function

    Public Function ConsultaAutorInvestigacion(ByVal id_investigacion As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaAutorInvestigacion", id_investigacion, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CalificaInvestigacion(ByVal id_investigacion As Integer, ByVal ctf As Integer, ByVal codigo_per As Integer, ByVal tipo As String, ByVal descripcion As String, ByVal ruta As String, ByVal rpt As Integer) As Integer
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer
            cnx.Ejecutar("INV_CalificaInvestigacion", id_investigacion, ctf, codigo_per, tipo, descripcion, ruta, 0).copyto(valoresdevueltos, rpt)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 0
        End Try
    End Function

    Public Function ActualizarInforme(ByVal id_investigacion As Integer, ByVal ruta As String, ByVal rpt As Integer) As Integer
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer
            cnx.Ejecutar("INV_ActualizarInforme", id_investigacion, ruta, 0).copyto(valoresdevueltos, rpt)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 0
        End Try
    End Function

    Public Function ActualizarResolucion(ByVal id_investigacion As Integer, ByVal nro_resolucion As String, ByVal ruta As String, ByVal rpt As Integer) As Integer
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer
            cnx.Ejecutar("INV_ActualizarResolucion", id_investigacion, nro_resolucion, ruta, 0).copyto(valoresdevueltos, rpt)
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Return 0
        End Try
    End Function


    Public Function RecuperaAutorCoodinardorInvestigacion(ByVal id_investigacion As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_RecuperaAutorCoodinardorInvestigacion", id_investigacion, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InstanciaInvestigacion(ByVal id_investigacion As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_InstanciaInvestigacion", id_investigacion)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaDirectorInvestigacion(ByVal id_investigacion As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListaMiembrosDirector", id_investigacion)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function VerificarResolucionInvestigacion(ByVal id_investigacion As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_VerificarResolucionInvestigacion", id_investigacion)
        cnx.CerrarConexion()
        Return dts
    End Function

End Class

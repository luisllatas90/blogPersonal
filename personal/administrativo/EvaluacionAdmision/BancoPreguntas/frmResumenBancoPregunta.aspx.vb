Imports ClsGlobales
Imports ClsBancoPreguntas
Imports ClsSistemaEvaluacion
Imports System.Collections.Generic

Partial Class BancoPreguntas_frmResumenBancoPregunta
    Inherits System.Web.UI.Page

#Region "Declaración de variables"
    Private oePreguntaEvaluacion As e_PreguntaEvaluacion, odPreguntaEvaluacion As d_PreguntaEvaluacion
    Private oeCompetenciaAprendizaje As e_CompetenciaAprendizaje, odCompetenciaAprendizaje As d_CompetenciaAprendizaje
    Private oeSubCompetencia As e_SubCompetencia, odSubCompetencia As d_SubCompetencia
    Private oeIndicador As e_Indicador, odIndicador As d_Indicador
    Private oeNivelComplejidadPregunta As e_NivelComplejidadPregunta, odNivelComplejidadPregunta As d_NivelComplejidadPregunta
    Private oeAlternativaEvaluacion As e_AlternativaEvaluacion, odAlternativaEvaluacion As d_AlternativaEvaluacion

    Private dtCompetenciaAprendizaje As Data.DataTable

    Private alternativasPorPregunta As Integer = 5
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            mt_Init()

            ''PENDIENTE!!
            'oePreguntaEvaluacion = New e_PreguntaEvaluacion : odPreguntaEvaluacion = New d_PreguntaEvaluacion
            'With oePreguntaEvaluacion
            '    .tipoConsulta = "GEN" 'Vista resumen
            '    .codigoNcp = 0
            '    .codigoInd = 0
            '    .cantidadPrv = 0
            'End With

            'Dim dt As Data.DataTable = odPreguntaEvaluacion.fc_Listar(oePreguntaEvaluacion)
            'If dt.Rows.Count > 0 Then
            '    'Response.Write(dt.Rows(0).Item("texto_prv"))
            '    Response.Write(ClsGlobales.fc_DesencriptaTexto64(dt.Rows(0).Item("texto_prv")))
            'End If

            'oeAlternativaEvaluacion = New e_AlternativaEvaluacion : odAlternativaEvaluacion = New d_AlternativaEvaluacion
            'With oeAlternativaEvaluacion
            '    .tipoConsulta = "GEN" 'Vista resumen
            'End With

            'dt = odAlternativaEvaluacion.fc_Listar(oeAlternativaEvaluacion)
            'If dt.Rows.Count > 0 Then
            '    Response.Write(ClsGlobales.fc_DesencriptaTexto64(dt.Rows(0).Item("texto_ale")))
            'End If
        End If
        mt_LimpiarParametros()
    End Sub

    Protected Sub grvList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvList.DataBound
        Try
            Dim values As Dictionary(Of String, Dictionary(Of String, Object)) = fn_RetornaEstructuraRowspan("competencia", "subCompetencia")

            With values.Item("competencia")
                .Item(KEY_INDEX_COL) = 0
            End With
            With values.Item("subCompetencia")
                .Item(KEY_INDEX_COL) = 1
            End With
            mt_AgruparFilasCustom(grvList, values)

            Dim dt As Data.DataTable = grvList.DataSource
            Dim cantidadBasica As Integer = 0, cantidadIntermedia As Integer = 0, cantidadAvanzada As Integer = 0
            For Each row As Data.DataRow In dt.Rows
                cantidadBasica += row.Item("cantidad_basica")
                cantidadIntermedia += row.Item("cantidad_intermedia")
                cantidadAvanzada += row.Item("cantidad_avanzada")
            Next

            If grvList.Rows.Count > 0 Then
                grvList.FooterRow.Cells(0).CssClass = "empty"
                grvList.FooterRow.Cells(1).CssClass = "empty"
                grvList.FooterRow.Cells(3).CssClass = "empty"
                grvList.FooterRow.Cells(4).CssClass = "empty"
                grvList.FooterRow.Cells(5).CssClass = "empty"
                grvList.FooterRow.Cells(2).Text = "TOTALES"
                grvList.FooterRow.Cells(3).Text = cantidadBasica
                grvList.FooterRow.Cells(4).Text = cantidadIntermedia
                grvList.FooterRow.Cells(5).Text = cantidadAvanzada
                grvList.FooterRow.Cells(6).Text = cantidadBasica + cantidadIntermedia + cantidadAvanzada
                grvList.FooterRow.Cells(6).CssClass = "alt"
            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvList_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim objGridView As GridView = CType(sender, GridView)
                Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objtablecell As TableCell = New TableCell()
                mt_AgregarCabecera(objgridviewrow, objtablecell, 3, "DATOS PRINCIPALES", "#FFFFFF", True)
                mt_AgregarCabecera(objgridviewrow, objtablecell, 3, "NIVELES DE COMPLEJIDAD ", "#FFFFFF", True)
                objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try

    End Sub

    Protected Sub btnListar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.ServerClick
        Try
            mt_ListarResumen()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnRegistrarSimple_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrarSimple.ServerClick
        Try
            mt_InitFormSimple()
            mt_SeleccionarTab("S")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'FORM PREGUNTA SIMPLE
    Protected Sub cmbCompetenciaSimple_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCompetenciaSimple.SelectedIndexChanged
        Try
            mt_InitComboSubCompetenciaSimple()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub cmbSubCompetenciaSimple_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSubCompetenciaSimple.SelectedIndexChanged
        Try
            mt_InitComboIndicadorSimple()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelarSimple_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarSimple.ServerClick
        Try
            mt_SeleccionarTab("R")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnGuardarSimple_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarSimple.ServerClick
        Try
            mt_GuardarSimple()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnRegistrarCompuesta_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrarCompuesta.ServerClick
        Try
            mt_InitFormCompuesta()
            mt_SeleccionarTab("C")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'FORM PREGUNTA COMPUESTA
    Protected Sub btnGenerarPreguntasCompuesta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerarPreguntasCompuesta.Click
        Try
            Dim cantidad As Integer
            If Not Integer.TryParse(txtCantidadPreguntas.Text.Trim, cantidad) Then
                mt_GenerarToastServidor(-1, "Debe ingresar la cantidad de preguntas")
                Exit Sub
            End If
            mt_GenerarPreguntasCompuesta(cantidad)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvPreguntasCompuesta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvPreguntasCompuesta.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'Cargo el selector de competencias (y se disparan los de sub competencias e indicadores)
                Dim cmbCompetenciaCompuesta As DropDownList = e.Row.FindControl("cmbCompetenciaCompuesta")
                mt_InitComboCompetenciaCompuesta(cmbCompetenciaCompuesta)

                'Cargo el selector de complejidad
                Dim cmbComplejidadCompuesta As DropDownList = e.Row.FindControl("cmbComplejidadCompuesta")
                mt_InitComboNivelComplejidadCompuesta(cmbComplejidadCompuesta)

                'Genero las alternativas dinámicamente
                Dim udpPanel As UpdatePanel = e.Row.FindControl("udpAlternativasCompuesta")
                mt_GenerarAlternativasCompuesta(udpPanel, e.Row.DataItem("i"), alternativasPorPregunta)
            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelarCompuesta_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarCompuesta.ServerClick
        Try
            mt_SeleccionarTab("R")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnGuardarCompuesta_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarCompuesta.ServerClick
        Try
            mt_GuardarCompuesta()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'Eventos delegados
    Protected Sub cmbCompetenciaCompuesta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim cmbCompetenciaCompuesta As DropDownList = sender
            Dim cmbSubCompetenciaCompuesta As DropDownList = cmbCompetenciaCompuesta.Parent.Parent.FindControl("cmbSubCompetenciaCompuesta")
            mt_InitComboSubCompetenciaCompuesta(cmbSubCompetenciaCompuesta, cmbCompetenciaCompuesta.SelectedValue)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub cmbSubCompetenciaCompuesta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim cmbSubCompetenciaCompuesta As DropDownList = sender
            Dim cmbIndicadorCompuesta As DropDownList = cmbSubCompetenciaCompuesta.Parent.Parent.FindControl("cmbIndicadorCompuesta")
            mt_InitComboIndicadorCompuesta(cmbIndicadorCompuesta, cmbSubCompetenciaCompuesta.SelectedValue)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

#End Region

#Region "Métodos"
    Private Sub mt_Init()
        Try
            divGrvList.Visible = False
            mt_InitComboFiltroCompetencia()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_LimpiarParametros()
        Try
            hddTipoVista.Value = ""
            hddParamsToastr.Value = ""

            'Parámetros para mensajes desde el servidor
            hddMenServMostrar.Value = "false"
            hddMenServRpta.Value = ""
            hddMenServTitulo.Value = ""
            hddMenServMensaje.Value = ""

            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_InitComboFiltroCompetencia()
        Try
            oeCompetenciaAprendizaje = New e_CompetenciaAprendizaje : odCompetenciaAprendizaje = New d_CompetenciaAprendizaje
            ClsGlobales.mt_LlenarListas(cmbFiltroCompetencia, odCompetenciaAprendizaje.fc_Listar(oeCompetenciaAprendizaje), "codigo_com", "nombre_com", "TODAS")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_ListarResumen()
        Try
            Dim codigoCom As Integer = cmbFiltroCompetencia.SelectedValue
            If codigoCom = -1 Then codigoCom = 0

            oePreguntaEvaluacion = New e_PreguntaEvaluacion : odPreguntaEvaluacion = New d_PreguntaEvaluacion
            With oePreguntaEvaluacion
                .tipoConsulta = "RES" 'Vista resumen
                .codigoCom = codigoCom
            End With

            Dim dt As Data.DataTable = odPreguntaEvaluacion.fc_Listar(oePreguntaEvaluacion)
            grvList.DataSource = dt
            grvList.DataBind()

            divGrvList.Visible = dt.Rows.Count > 0
            udpGrvList.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'FORM PREGUNTA SIMPLE
    Private Sub mt_InitFormSimple()
        Try
            mt_InitComboCompetenciaSimple()
            mt_InitComboNivelComplejidadSimple()

            cmbCompetenciaSimple.SelectedValue = -1 : cmbCompetenciaSimple_SelectedIndexChanged(Nothing, Nothing)
            cmbComplejidadSimple.SelectedValue = -1
            txtPreguntaSimple.Text = ""

            mt_GenerarAlternativasSimple(alternativasPorPregunta)

            udpSimple.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_InitComboCompetenciaSimple()
        Try
            oeCompetenciaAprendizaje = New e_CompetenciaAprendizaje : odCompetenciaAprendizaje = New d_CompetenciaAprendizaje
            ClsGlobales.mt_LlenarListas(cmbCompetenciaSimple, odCompetenciaAprendizaje.fc_Listar(oeCompetenciaAprendizaje), "codigo_com", "nombre_com", "-- SELECCIONE --")
            cmbCompetenciaSimple_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_InitComboSubCompetenciaSimple()
        Try
            oeSubCompetencia = New e_SubCompetencia : odSubCompetencia = New d_SubCompetencia
            With oeSubCompetencia
                ._tipoOpe = "1"
                ._codigo_com = cmbCompetenciaSimple.SelectedValue
            End With

            ClsGlobales.mt_LlenarListas(cmbSubCompetenciaSimple, odSubCompetencia.fc_Listar(oeSubCompetencia), "codigo_scom", "nombre_scom", "-- SELECCIONE --", maxLetrasDescripcion:=60)
            cmbSubCompetenciaSimple_SelectedIndexChanged(Nothing, Nothing)
            udpSubCompetenciaSimple.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_InitComboIndicadorSimple()
        Try
            oeIndicador = New e_Indicador : odIndicador = New d_Indicador
            With oeIndicador
                ._tipoOpe = "2"
                ._codigo_scom = cmbSubCompetenciaSimple.SelectedValue
            End With
            ClsGlobales.mt_LlenarListas(cmbIndicadorSimple, odIndicador.fc_Listar(oeIndicador), "codigo_ind", "descripcion_ind", "-- SELECCIONE --", "nombre_ind", 60)
            udpIndicadorSimple.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_InitComboNivelComplejidadSimple()
        Try
            oeNivelComplejidadPregunta = New e_NivelComplejidadPregunta : odNivelComplejidadPregunta = New d_NivelComplejidadPregunta
            ClsGlobales.mt_LlenarListas(cmbComplejidadSimple, odNivelComplejidadPregunta.fc_Listar(oeNivelComplejidadPregunta), "codigo_ncp", "nombre_ncp", "-- SELECCIONE --")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_GenerarAlternativasSimple(ByVal cantidad As Integer)
        Try
            Dim dt As New Data.DataTable
            dt.Columns.Add("i", GetType(Integer))

            For i As Integer = 1 To cantidad
                dt.Rows.Add(i)
            Next

            rpHeaderAltSimple.DataSource = dt
            rpHeaderAltSimple.DataBind()

            rpBodyAltSimple.DataSource = dt
            rpBodyAltSimple.DataBind()

            udpAlternativasSimple.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Function fc_GenerarChunks(ByVal texto As String) As Data.DataTable
        Dim dtChunks As New Data.DataTable

        Try
            dtChunks.Columns.Add("chunk", GetType(String))

            Dim origTextPrv As String = texto
            Dim c As Integer = 0, blockLimit As Integer = 8000, lastLimit As Integer = blockLimit
            Dim startIndex As Integer = 0

            While c * blockLimit < origTextPrv.Length
                startIndex = c * blockLimit
                lastLimit = Math.Min(blockLimit, origTextPrv.Length - startIndex)
                dtChunks.Rows.Add(origTextPrv.Substring(startIndex, lastLimit))
                c = c + 1
            End While
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
            Throw ex
        End Try

        Return dtChunks
    End Function

    Private Sub mt_GuardarSimple()
        Try
            If Not fc_ValidarDatosFormSimple() Then
                Exit Sub
            End If

            Dim rpta As New Dictionary(Of String, String)

            oePreguntaEvaluacion = New e_PreguntaEvaluacion : odPreguntaEvaluacion = New d_PreguntaEvaluacion
            oeAlternativaEvaluacion = New e_AlternativaEvaluacion : odAlternativaEvaluacion = New d_AlternativaEvaluacion

            Dim textoPrv As String = ClsGlobales.fc_EncriptaTexto64(txtPreguntaSimple.Text.Trim)
            Dim codigoPrv As Integer = 0
            With oePreguntaEvaluacion
                .operacion = "I"
                .codigoPrv = 0 'Siempre va a ser un nuevo registro
                .codigoInd = cmbIndicadorSimple.SelectedValue
                .codigoNcp = cmbComplejidadSimple.SelectedValue
                .tipoPrv = "U"
                .textoPrv = fc_GenerarChunks(textoPrv)
                .cantidadPrv = 1
                .identificadorPrv = ""
                .codUsuario = Request.QueryString("id")
            End With

            Dim lstAlternativaEvaluacion As New List(Of e_AlternativaEvaluacion)
            Dim c As Integer = 1
            For Each item As RepeaterItem In rpBodyAltSimple.Items
                oeAlternativaEvaluacion = New e_AlternativaEvaluacion

                Dim txtAlternativaSimple As TextBox = item.FindControl("txtAlternativaSimple")
                Dim rbtRespuestaSimple As HtmlInputRadioButton = item.FindControl("rbtRespuestaSimple")

                Dim textoAle As String = ClsGlobales.fc_EncriptaTexto64(txtAlternativaSimple.Text.Trim)
                With oeAlternativaEvaluacion
                    .operacion = "I"
                    .codigoAle = 0
                    .ordenAle = c
                    .textoAle = fc_GenerarChunks(textoAle)
                    .correctaAle = (Request("rbtRespuestaSimple") = c)
                    .codUsuario = Request.QueryString("id")
                End With
                lstAlternativaEvaluacion.Add(oeAlternativaEvaluacion)
                c += 1
            Next
            oePreguntaEvaluacion.alternativas = lstAlternativaEvaluacion

            Dim lstPreguntaEvaluacion As New List(Of e_PreguntaEvaluacion)
            lstPreguntaEvaluacion.Add(oePreguntaEvaluacion)

            rpta = odPreguntaEvaluacion.fc_GuardarMultiple(lstPreguntaEvaluacion)
            If rpta.Item("rpta") = "1" Then
                mt_ListarResumen()
                mt_SeleccionarTab("R")
            End If

            mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Function fc_ValidarDatosFormSimple() As Boolean
        'Datos generales
        If cmbCompetenciaSimple.SelectedValue = -1 Then
            mt_GenerarToastServidor(0, "Debe seleccionar una competencia")
            Return False
        End If

        If cmbSubCompetenciaSimple.SelectedValue = -1 Then
            mt_GenerarToastServidor(0, "Debe seleccionar una sub competencia")
            Return False
        End If

        If cmbIndicadorSimple.SelectedValue = -1 Then
            mt_GenerarToastServidor(0, "Debe seleccionar un indicador")
            Return False
        End If

        If cmbComplejidadSimple.SelectedValue = "-1" Then
            mt_GenerarToastServidor(0, "Debe seleccionar una complejidad")
            Return False
        End If

        If txtPreguntaSimple.Text.Trim = "" Then
            mt_GenerarToastServidor(0, "Debe asignar el contenido a la pregunta")
            Return False
        End If

        'Alternativas
        Dim altSimpleVacias As Boolean = False
        Dim altSimpleRespuesta As Boolean = False
        Dim c As Integer = 1
        For Each item As RepeaterItem In rpBodyAltSimple.Items
            Dim txtalternativaSimple As TextBox = item.FindControl("txtalternativaSimple")
            If txtalternativaSimple.Text.Trim = "" Then
                mt_GenerarToastServidor(0, "Debe asignar el contenido de la alternativa N° " & c)
                altSimpleVacias = True
                Exit For
            End If

            If Not altSimpleRespuesta Then
                altSimpleRespuesta = (Request("rbtRespuestaSimple") = c)
            End If
            c += 1
        Next
        If altSimpleVacias Then
            Return False
        End If
        If Not altSimpleRespuesta Then
            mt_GenerarToastServidor(0, "No ha marcado ninguna alternativa como respuesta correcta")
            Return False
        End If

        Return True
    End Function

    'FORM PREGUNTA COMPUESTA
    Private Sub mt_InitFormCompuesta()
        Try
            'Limpio los datos del formulario
            txtEnunciadoCompuesta.Text = ""
            txtCantidadPreguntas.Text = ""

            'Limpio la grilla de preguntas
            mt_GenerarPreguntasCompuesta(0)

            udpCompuesta.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_GenerarPreguntasCompuesta(ByVal cantidad As Integer)
        Try
            oeCompetenciaAprendizaje = New e_CompetenciaAprendizaje : odCompetenciaAprendizaje = New d_CompetenciaAprendizaje
            dtCompetenciaAprendizaje = odCompetenciaAprendizaje.fc_Listar(oeCompetenciaAprendizaje)

            Dim dt As New Data.DataTable
            dt.Columns.Add("i", GetType(Integer))

            For i As Integer = 1 To cantidad
                dt.Rows.Add(i)
            Next

            grvPreguntasCompuesta.DataSource = dt
            grvPreguntasCompuesta.DataBind()

            divPreguntasCompuesta.Visible = dt.Rows.Count > 0
            udpPreguntasCompuesta.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_InitComboCompetenciaCompuesta(ByVal cmb As DropDownList)
        Try
            ClsGlobales.mt_LlenarListas(cmb, dtCompetenciaAprendizaje, "codigo_com", "nombre_com", "-- SELECCIONE --")
            cmbCompetenciaCompuesta_SelectedIndexChanged(cmb, Nothing)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_InitComboSubCompetenciaCompuesta(ByVal cmb As DropDownList, ByVal codigoCom As Integer)
        Try
            oeSubCompetencia = New e_SubCompetencia : odSubCompetencia = New d_SubCompetencia
            With oeSubCompetencia
                ._tipoOpe = "1"
                ._codigo_com = codigoCom
            End With
            ClsGlobales.mt_LlenarListas(cmb, odSubCompetencia.fc_Listar(oeSubCompetencia), "codigo_scom", "nombre_scom", "-- SELECCIONE --", maxLetrasDescripcion:=60)
            cmbSubCompetenciaCompuesta_SelectedIndexChanged(cmb, Nothing)

            Dim panel As UpdatePanel = cmb.Parent.Parent
            panel.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_InitComboIndicadorCompuesta(ByVal cmb As DropDownList, ByVal codigoScom As Integer)
        Try
            oeIndicador = New e_Indicador : odIndicador = New d_Indicador
            With oeIndicador
                ._tipoOpe = "2"
                ._codigo_scom = codigoScom
            End With
            ClsGlobales.mt_LlenarListas(cmb, odIndicador.fc_Listar(oeIndicador), "codigo_ind", "descripcion_ind", "-- SELECCIONE --", "nombre_ind", 60)

            Dim panel As UpdatePanel = cmb.Parent.Parent
            panel.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_InitComboNivelComplejidadCompuesta(ByVal cmb As DropDownList)
        Try
            oeNivelComplejidadPregunta = New e_NivelComplejidadPregunta : odNivelComplejidadPregunta = New d_NivelComplejidadPregunta
            Dim dt As Data.DataTable = odNivelComplejidadPregunta.fc_Listar(oeNivelComplejidadPregunta)
            ClsGlobales.mt_LlenarListas(cmb, odNivelComplejidadPregunta.fc_Listar(oeNivelComplejidadPregunta), "codigo_ncp", "nombre_ncp", "-- SELECCIONE --")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_GenerarAlternativasCompuesta(ByVal panel As UpdatePanel, ByVal parentIndex As Integer, ByVal cantidad As Integer)
        Try
            Dim dt As New Data.DataTable
            dt.Columns.Add("j", GetType(Integer))
            dt.Columns.Add("k", GetType(Integer)) 'k -> Índice del parent (row)

            For i As Integer = 1 To cantidad
                dt.Rows.Add(i, parentIndex)
            Next

            Dim rpHeaderAltCompuesta As Repeater = panel.FindControl("rpHeaderAltCompuesta")
            Dim rpBodyAltCompuesta As Repeater = panel.FindControl("rpBodyAltCompuesta")

            rpHeaderAltCompuesta.DataSource = dt
            rpHeaderAltCompuesta.DataBind()

            rpBodyAltCompuesta.DataSource = dt
            rpBodyAltCompuesta.DataBind()

            panel.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_GuardarCompuesta()
        Try
            odPreguntaEvaluacion = New d_PreguntaEvaluacion
            odAlternativaEvaluacion = New d_AlternativaEvaluacion

            Dim rpta As New Dictionary(Of String, String)

            'Validaciones generales
            Dim cantidadPreguntas As Integer
            If Not Integer.TryParse(txtCantidadPreguntas.Text.Trim, cantidadPreguntas) Then
                mt_GenerarToastServidor(0, "La cantidad de preguntas debe ser un valor numérico")
                Exit Sub
            End If
            If cantidadPreguntas = 0 Then
                mt_GenerarToastServidor(0, "No ha generado ninguna pregunta")
                Exit Sub
            End If

            Dim codigoCom As Integer = 0
            Dim cmbCompetenciaCompuesta As DropDownList
            For Each row As GridViewRow In grvPreguntasCompuesta.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    cmbCompetenciaCompuesta = row.FindControl("cmbCompetenciaCompuesta")
                    If codigoCom <> 0 AndAlso codigoCom <> cmbCompetenciaCompuesta.SelectedValue Then
                        mt_GenerarToastServidor(0, "Las competencias de todas las preguntas deben ser las mismas")
                        Exit Sub
                    End If
                    codigoCom = cmbCompetenciaCompuesta.SelectedValue
                End If
            Next

            'Almaceno el enunciado
            Dim oeEnunciado As New e_PreguntaEvaluacion
            Dim enunciado As String = ClsGlobales.fc_EncriptaTexto64(txtEnunciadoCompuesta.Text.Trim)
            With oeEnunciado
                .operacion = "I"
                .codigoPrv = 0 'Siempre va a ser un nuevo registro
                .tipoPrv = "A"
                .textoPrv = fc_GenerarChunks(enunciado)
                .cantidadPrv = txtCantidadPreguntas.Text.Trim
                .identificadorPrv = "" 'PENDIENTE!?
                .codUsuario = Request.QueryString("id")
            End With

            'Almaceno las preguntas
            Dim lstPreguntaEvaluacion As New List(Of e_PreguntaEvaluacion)
            Dim j As Integer = 1
            For Each row As GridViewRow In grvPreguntasCompuesta.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    If Not fc_ValidarDatosFormCompuesta(row, j) Then
                        Exit Sub
                    End If

                    oePreguntaEvaluacion = New e_PreguntaEvaluacion

                    Dim cmbIndicadorCompuesta As DropDownList = row.FindControl("cmbIndicadorCompuesta")
                    Dim cmbComplejidadCompuesta As DropDownList = row.FindControl("cmbComplejidadCompuesta")
                    Dim txtPreguntaCompuesta As TextBox = row.FindControl("txtPreguntaCompuesta")
                    Dim rpBodyAltCompuesta As Repeater = row.FindControl("rpBodyAltCompuesta")

                    Dim textoPrv As String = ClsGlobales.fc_EncriptaTexto64(txtPreguntaCompuesta.Text.Trim)
                    Dim codigoPrv As Integer = 0
                    With oePreguntaEvaluacion
                        .operacion = "I"
                        .codigoPrv = 0 'Siempre va a ser un nuevo registro
                        .codigoInd = cmbIndicadorCompuesta.SelectedValue
                        .codigoNcp = cmbComplejidadCompuesta.SelectedValue
                        .tipoPrv = "A"
                        .textoPrv = fc_GenerarChunks(textoPrv)
                        .cantidadPrv = 1
                        .identificadorPrv = ""
                        .codUsuario = Request.QueryString("id")
                    End With

                    'Almaceno las alternativas para cada pregunta
                    Dim lstAlternativaEvaluacion As New List(Of e_AlternativaEvaluacion)
                    Dim k As Integer = 1
                    For Each item As RepeaterItem In rpBodyAltCompuesta.Items
                        oeAlternativaEvaluacion = New e_AlternativaEvaluacion

                        Dim txtAlternativaCompuesta As TextBox = item.FindControl("txtAlternativaCompuesta")
                        Dim rbtRespuestaCompuesta As HtmlInputRadioButton = item.FindControl("rbtRespuestaCompuesta")

                        Dim textoAle As String = ClsGlobales.fc_EncriptaTexto64(txtAlternativaCompuesta.Text.Trim)
                        With oeAlternativaEvaluacion
                            .operacion = "I"
                            .codigoAle = 0
                            .ordenAle = k
                            .textoAle = fc_GenerarChunks(textoAle)
                            .correctaAle = (Request("rbtRespuestaCompuesta" & j) = (j & k))
                            .codUsuario = Request.QueryString("id")
                        End With
                        lstAlternativaEvaluacion.Add(oeAlternativaEvaluacion)
                        k += 1
                    Next
                    oePreguntaEvaluacion.alternativas = lstAlternativaEvaluacion
                    lstPreguntaEvaluacion.Add(oePreguntaEvaluacion)
                End If
                j += 1
            Next

            rpta = odPreguntaEvaluacion.fc_GuardarMultiple(lstPreguntaEvaluacion, oeEnunciado)
            If rpta.Item("rpta") = "1" Then
                mt_ListarResumen()
                mt_SeleccionarTab("R")
            End If

            mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Function fc_ValidarDatosFormCompuesta(ByVal row As GridViewRow, ByVal j As Integer) As Boolean
        Try
            Dim cmbCompetenciaCompuesta As DropDownList = row.FindControl("cmbCompetenciaCompuesta")
            Dim cmbSubCompetenciaCompuesta As DropDownList = row.FindControl("cmbSubCompetenciaCompuesta")
            Dim cmbIndicadorCompuesta As DropDownList = row.FindControl("cmbIndicadorCompuesta")
            Dim cmbComplejidadCompuesta As DropDownList = row.FindControl("cmbComplejidadCompuesta")
            Dim txtPreguntaCompuesta As TextBox = row.FindControl("txtPreguntaCompuesta")

            'Datos generales
            If cmbCompetenciaCompuesta.SelectedValue = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar una competencia para la pregunta N° " & j)
                Return False
            End If

            If cmbSubCompetenciaCompuesta.SelectedValue = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar una sub competencia para la pregunta N° " & j)
                Return False
            End If

            If cmbIndicadorCompuesta.SelectedValue = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar un indicador para la pregunta N° " & j)
                Return False
            End If

            If cmbComplejidadCompuesta.SelectedValue = "-1" Then
                mt_GenerarToastServidor(0, "Debe seleccionar una complejidad para la pregunta N° " & j)
                Return False
            End If

            If txtPreguntaCompuesta.Text.Trim = "" Then
                mt_GenerarToastServidor(0, "Debe asignar el contenido a la pregunta N° " & j)
                Return False
            End If

            'Alternativas
            Dim rpBodyAltCompuesta As Repeater = row.FindControl("rpBodyAltCompuesta")
            Dim altCompuestaVacias As Boolean = False
            Dim altCompuestaRespuesta As Boolean = False

            Dim k As Integer = 1
            For Each item As RepeaterItem In rpBodyAltCompuesta.Items
                Dim txtalternativaCompuesta As TextBox = item.FindControl("txtalternativaCompuesta")
                If txtalternativaCompuesta.Text.Trim = "" Then
                    mt_GenerarToastServidor(0, "Debe asignar el contenido de la alternativa N° " & k & ", pregunta N° " & j)
                    altCompuestaVacias = True
                    Exit For
                End If

                If Not altCompuestaRespuesta Then
                    Dim rbtRespuestaCompuesta As HtmlInputRadioButton = item.FindControl("rbtRespuestaCompuesta")
                    altCompuestaRespuesta = (Request("rbtRespuestaCompuesta" & j) = (j & k))
                End If
                k += 1
            Next
            If altCompuestaVacias Then
                Return False
            End If
            If Not altCompuestaRespuesta Then
                mt_GenerarToastServidor(0, "No ha marcado ninguna alternativa de la pregunta N° " & j & " como respuesta correcta")
                Return False
            End If

            Return True
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Function

    'OTROS MÉTODOS
    Private Sub mt_SeleccionarTab(ByVal tipo As String)
        Try
            hddTipoVista.Value = tipo
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_GenerarToastServidor(ByVal rpta As String, ByVal msg As String)
        Try
            hddParamsToastr.Value = "rpta=" & rpta & "|msg=" & msg
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_GenerarMensajeServidor(ByVal titulo As String, ByVal rpta As Integer, ByVal mensaje As String)
        Try
            hddMenServMostrar.Value = "true"
            hddMenServRpta.Value = rpta
            hddMenServTitulo.Value = titulo
            hddMenServMensaje.Value = mensaje

            udpParams.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class

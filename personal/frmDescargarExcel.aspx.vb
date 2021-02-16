Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class frmDescargarExcel
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    'VARIABLES
    Dim cod_user As Integer = 0
    Dim dtTabla As DataTable

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing OrElse Session("perlogin") Is Nothing) Then
                Response.Redirect("sinacceso.html")
            End If

            cod_user = Session("id_per")

            If mt_GenerarExcel() Then
                Session("frmDescargarExcel.formulario") = Nothing
                Session("frmDescargarExcel.nombre_archivo") = Nothing

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", "window.close();", True)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try            
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function mt_GenerarExcel() As Boolean
        Try
            If Not mt_ObtenerDataTable() Then Return False

            If dtTabla.Rows.Count = 0 Then Call mt_ShowMessage("No se encontraron datos a exportar.", MessageType.warning) : Return False

            Dim response As HttpResponse = HttpContext.Current.Response
            response.Clear()
            response.ClearHeaders()
            response.ClearContent()
            response.Charset = Encoding.UTF8.WebName
            response.AddHeader("content-disposition", "attachment; filename=" + Session("frmDescargarExcel.nombre_archivo") + ".xls")
            response.AddHeader("Content-Type", "application/Excel")
            response.ContentType = "application/vnd.ms-excel"
            Using sw As New StringWriter()
                Using htw As New HtmlTextWriter(sw)
                    Dim gridView As New GridView()
                    gridView.DataSource = dtTabla
                    gridView.DataBind()
                    gridView.RenderControl(htw)
                    Dim style As String = "<style> td { mso-number-format:\@;} </style>"
                    response.Write(style)
                    response.Write(sw.ToString())
                    gridView.Dispose()
                    dtTabla.Dispose()
                    response.[End]()
                End Using
            End Using

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_ObtenerDataTable() As Boolean
        Try
            dtTabla = New DataTable                        
            Dim numero_parametros As Integer = 0

            Select Case Session("frmDescargarExcel.formulario")
                Case "frmEmitirDiplomasProveedor"
                    numero_parametros = 5

                    If Not fu_ValidarParametros(numero_parametros) Then Return False

                    Dim me_EnvioDiplomasProveedor As New e_EnvioDiplomasProveedor
                    Dim md_EnvioDiplomasProveedor As New d_EnvioDiplomasProveedor
                    Dim me_EnvioDiplomasProveedorDetalle As New e_EnvioDiplomasProveedorDetalle
                    Dim md_EnvioDiplomasProveedorDetalle As New d_EnvioDiplomasProveedorDetalle

                    If Session("frmDescargarExcel.param01").ToString.Equals("F") Then 'cmbTipoDiplomaFiltro
                        With me_EnvioDiplomasProveedor
                            .operacion = "FIS"
                            .codigo_scu = Session("frmDescargarExcel.param02").ToString 'cmbSesionConsejoFiltro
                            .codigo_tdg = Session("frmDescargarExcel.param03").ToString 'cmbTipoDenominacionFiltro
                        End With

                        dtTabla = md_EnvioDiplomasProveedor.FormatoTramaEmisionDiploma(me_EnvioDiplomasProveedor)

                        'Registrar Envío de Diploma Físico
                        If Session("frmDescargarExcel.param05").ToString.Equals("R") Then
                            me_EnvioDiplomasProveedor = md_EnvioDiplomasProveedor.GetEnvioDiplomasProveedor(0)

                            With me_EnvioDiplomasProveedor
                                .operacion = "I"
                                .cod_user = cod_user
                                .tipo_edp = Session("frmDescargarExcel.param01").ToString 'cmbTipoDiplomaFiltro
                                .codigo_scu = Session("frmDescargarExcel.param02").ToString 'cmbSesionConsejoFiltro
                                .codigo_tdg = Session("frmDescargarExcel.param03").ToString 'cmbTipoDenominacionFiltro
                                .tipo_emision = ""
                            End With

                            Dim dtCodigo As New DataTable
                            dtCodigo = md_EnvioDiplomasProveedor.RegistrarEnvioDiplomasProveedor(me_EnvioDiplomasProveedor)

                            If dtCodigo.Rows.Count > 0 Then
                                Dim codigo_edp As Integer = CInt(dtCodigo.Rows(0).Item("codigo_edp").ToString)

                                For Each row As DataRow In dtTabla.Rows
                                    me_EnvioDiplomasProveedorDetalle = md_EnvioDiplomasProveedorDetalle.GetEnvioDiplomasProveedorDetalle(0)

                                    With me_EnvioDiplomasProveedorDetalle
                                        .operacion = "I"
                                        .cod_user = cod_user
                                        .codigo_edp = codigo_edp
                                        .codigo_trl = CInt(row("codigo_trl"))
                                        .codigo_egr = CInt(row("codigo_egr"))
                                    End With

                                    md_EnvioDiplomasProveedorDetalle.RegistrarEnvioDiplomasProveedorDetalle(me_EnvioDiplomasProveedorDetalle)
                                Next
                            End If
                        End If

                        'Remover columnas que no serán mostradas
                        Call mt_OcultarColumnas(dtTabla, "codigo_egr", "codigo_trl", "Secretario_Gen", "Cargo_Secretario_Gen", "orden")

                    ElseIf Session("frmDescargarExcel.param01").ToString.Equals("E") Then 'cmbTipoDiplomaFiltro
                        With me_EnvioDiplomasProveedor
                            .operacion = "ELE"
                            .codigo_scu = Session("frmDescargarExcel.param02").ToString 'cmbSesionConsejoFiltro
                            .codigo_tdg = Session("frmDescargarExcel.param03").ToString 'cmbTipoDenominacionFiltro
                            .tipo_emision = Session("frmDescargarExcel.param04").ToString 'cmbTipoEmisionFiltro
                        End With

                        dtTabla = md_EnvioDiplomasProveedor.FormatoTramaEmisionDiploma(me_EnvioDiplomasProveedor)

                        'Remover columnas que no serán mostradas
                        Call mt_OcultarColumnas(dtTabla, "codigo_egr", "codigo_trl", "CODIGO_TEST", "APELLIDOPAT_ALU", "APELLIDOMAT_ALU", "NOMBRES_ALU")

                        If Session("frmDescargarExcel.param03").ToString = 3 OrElse Session("frmDescargarExcel.param03").ToString = 4 Then 'cmbTipoDenominacionFiltro                            
                            Call mt_OcultarColumnas(dtTabla, "FACULTAD", "ESCUELA")
                        End If

                        If Session("frmDescargarExcel.param04").ToString = "O" Then 'cmbTipoEmisionFiltro
                            Call mt_OcultarColumnas(dtTabla, "FECHA_DUPLICADO")
                        End If
                    End If

                    Call mt_LimpiarParametros(numero_parametros)
                    Return True

                Case "frmEgresado"
                    numero_parametros = 9

                    If Not fu_ValidarParametros(numero_parametros) Then Return False

                    Dim me_EgresadoAlumni As New e_EgresadoAlumni
                    Dim md_EgresadoAlumni As New d_EgresadoAlumni

                    With me_EgresadoAlumni
                        .operacion = "LIS"

                        If Session("frmDescargarExcel.param01").ToString.Equals(g_VariablesGlobales.NivelEstudioPreGrado) Then 'cmbNivelFiltro
                            .nivel_ega = g_VariablesGlobales.CodigoTestPreGrado
                        ElseIf Session("frmDescargarExcel.param01").ToString.Equals(g_VariablesGlobales.NivelEstudioPostGrado) Then 'cmbNivelFiltro
                            .nivel_ega = g_VariablesGlobales.CodigoTestPostGrado
                        ElseIf Session("frmDescargarExcel.param01").ToString.Equals(g_VariablesGlobales.NivelEstudioPostTitulo) Then 'cmbNivelFiltro
                            .nivel_ega = g_VariablesGlobales.CodigoTestPostTitulo
                        Else
                            .nivel_ega = g_VariablesGlobales.CodigoTestPreGrado & "," & _
                                g_VariablesGlobales.CodigoTestPostGrado & "," & _
                                g_VariablesGlobales.CodigoTestPostTitulo
                        End If

                        .modalidad_ega = Session("frmDescargarExcel.param02").ToString 'cmbModalidadFiltro
                        .codigo_fac = Session("frmDescargarExcel.param03").ToString 'Me.cmbFacultadFiltro
                        .codigo_cpf = Session("frmDescargarExcel.param04").ToString 'Me.cmbCarreraFiltro
                        .sexo_ega = Session("frmDescargarExcel.param05").ToString 'Me.cmbSexoFiltro
                        .anio_egreso = Session("frmDescargarExcel.param06").ToString 'Me.cmbAnioEgresoFiltro
                        .anio_bachiller = Session("frmDescargarExcel.param07").ToString 'Me.cmbAnioBachillerFiltro
                        .anio_titulo = Session("frmDescargarExcel.param08").ToString 'Me.cmbAnioTituloFiltro
                        .nombre_ega = Session("frmDescargarExcel.param09").ToString 'Me.txtNombreFiltro
                    End With

                    dtTabla = md_EgresadoAlumni.ListarEgresadoAlumni(me_EgresadoAlumni)

                    'Mostrar solo algunas columnas
                    mt_MostrarColumnas(dtTabla, "nivel", "modalidad", "apellidos", "nombres", "facultad", "escuela_profesional", _
                                       "sexo", "anio_egreso", "anio_bachiller", "anio_titulo", "correo")


                    Call mt_LimpiarParametros(numero_parametros)
                    Return True

                Case "frmEstructuraTramaElectronica"
                    numero_parametros = 5

                    If Not fu_ValidarParametros(numero_parametros) Then Return False

                    Dim me_EnvioDiplomasProveedor As New e_EnvioDiplomasProveedor
                    Dim md_EnvioDiplomasProveedor As New d_EnvioDiplomasProveedor

                    With me_EnvioDiplomasProveedor
                        .operacion = "REP"
                        .codigo_scu = Session("frmDescargarExcel.param01").ToString 'cmbSesionConsejoFiltro
                        .codigo_tdg = Session("frmDescargarExcel.param02").ToString 'cmbTipoDenominacionFiltro
                        .tipo_emision = Session("frmDescargarExcel.param03").ToString 'cmbTipoEmisionFiltro
                    End With

                    dtTabla = md_EnvioDiplomasProveedor.EstructuraTramaElectronica(me_EnvioDiplomasProveedor)

                    'Terminar el tramite
                    If Session("frmDescargarExcel.param04").ToString.Equals("R") Then
                        For Each row As DataRow In dtTabla.Rows
                            me_EnvioDiplomasProveedor = New e_EnvioDiplomasProveedor

                            With me_EnvioDiplomasProveedor
                                .cod_user = Session("id_per")
                                .codigo_dta = CInt(row("codigo_dta"))
                                .codigo_tfu = Session("frmDescargarExcel.param05").ToString
                            End With

                            md_EnvioDiplomasProveedor.EntregarTramite(me_EnvioDiplomasProveedor)
                        Next
                    End If

                    'Remover columnas que no serán mostradas
                    Call mt_OcultarColumnas(dtTabla, "codigo_egr", "codigo_trl", "codigo_dta", "APELLIDOPAT_ALU", "APELLIDOMAT_ALU", _
                                            "NOMBRES_ALU", "CODIGO_TEST", "FECHA_DUPLICADO")

                    If Session("frmDescargarExcel.param02").ToString = 3 OrElse Session("frmDescargarExcel.param02").ToString = 4 Then
                        Call mt_OcultarColumnas(dtTabla, "FACULTAD", "ESCUELA")
                    End If

                    If Session("frmDescargarExcel.param03").ToString = "O" Then 'cmbTipoEmisionFiltro
                        Call mt_OcultarColumnas(dtTabla, "FECHA_DUPLICADO")
                    End If

                    Call mt_LimpiarParametros(numero_parametros)
                    Return True

                Case "frmListaSolicitudEscolaridad"
                    numero_parametros = 1

                    If Not fu_ValidarParametros(numero_parametros) Then Return False

                    Dim me_SolicitudEscolaridad As New e_SolicitudEscolaridad
                    Dim md_SolicitudEscolaridad As New d_SolicitudEscolaridad

                    With me_SolicitudEscolaridad
                        .operacion = "LIS"
                        .anio_soe = Session("frmDescargarExcel.param01").ToString
                    End With

                    dtTabla = md_SolicitudEscolaridad.ListarSolicitudEscolaridad(me_SolicitudEscolaridad)

                    'Remover columnas que no serán mostradas
                    Call mt_OcultarColumnas(dtTabla, "codigo_soe", "codigo_dhab", "IdArchivosCompartidosRecibo", "IdArchivosCompartidosDNI")

                    Call mt_LimpiarParametros(numero_parametros)

                    Return True
            End Select

            mt_ShowMessage("El formulario no ha sido configurado para descargar archivos Excel.", MessageType.error)
            Return False
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarParametros(ByVal cantidad As Integer) As Boolean
        Try
            If Session("frmDescargarExcel.formulario") Is Nothing Then mt_ShowMessage("La variable con el nombre del formulario es nula.", MessageType.error) : Return False
            If Session("frmDescargarExcel.nombre_archivo") Is Nothing Then mt_ShowMessage("La variable con el nombre del archivo es nula.", MessageType.error) : Return False

            Dim nombre_variable As String

            For i As Integer = 1 To cantidad
                nombre_variable = "frmDescargarExcel.param" & Right("00" & i.ToString, 2)

                If Session(nombre_variable) Is Nothing Then mt_ShowMessage("La variable " & Right("00" & i.ToString, 2) & " es nula.", MessageType.error) : Return False
            Next

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_LimpiarParametros(ByVal cantidad As Integer)
        Try            
            Dim nombre_variable As String

            For i As Integer = 1 To cantidad
                nombre_variable = "frmDescargarExcel.param" & Right("00" & i.ToString, 2)
                Session(nombre_variable) = Nothing
            Next
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_MostrarColumnas(ByRef dt_Tabla As DataTable, ByVal ParamArray parametros() As String)
        Try            
            Dim lst_CamposOcultos As New List(Of String)
            Dim lb_Ocultar As Boolean = True

            If parametros.Length > 0 Then
                For Each column As DataColumn In dt_Tabla.Columns
                    lb_Ocultar = True

                    For Each parametro As String In parametros
                        If column.ColumnName.ToUpper.Equals(parametro.ToUpper) Then
                            lb_Ocultar = False
                            Exit For
                        End If
                    Next

                    If lb_Ocultar Then
                        lst_CamposOcultos.Add(column.ColumnName)
                    End If
                Next

                For Each campo_oculto As String In lst_CamposOcultos
                    dt_Tabla.Columns.Remove(campo_oculto)
                Next
            End If            
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_OcultarColumnas(ByRef dt_Tabla As DataTable, ByVal ParamArray parametros() As String)
        Try
            If parametros.Length > 0 Then
                For Each parametro As String In parametros
                    dt_Tabla.Columns.Remove(parametro)
                Next
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

End Class

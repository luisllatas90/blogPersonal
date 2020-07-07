﻿
Partial Class administrativo_pec2_frmListaPreInscritos
    Inherits System.Web.UI.Page

    Private estadoCheck As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If (Request.QueryString("id") = Nothing) Then
                Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('No se encontro al usuario')</SCRIPT>")
                BloquearControles(False)
            End If

            If (Request.QueryString("cco") = Nothing) Then
                Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('No se encontro el centro de costo')</SCRIPT>")
                BloquearControles(False)
            End If

            Me.HdCodigo_Cco.Value = Request.QueryString("cco")
            Me.btnEliminarPreInscrito.Enabled = False
            CargaCombo()
            VerificaCco()
        End If
    End Sub

    Private Sub BloquearControles(ByVal sw As Boolean)
        Me.btnBuscar.Enabled = sw
        Me.btnInscribir.Enabled = sw
        Me.btnEliminarPreInscrito.Enabled = sw
    End Sub

    Private Sub VerificaCco()
        Dim obj As New ClsConectarDatos
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            'Buscamos la Fecha de Inicio del Evento
            Dim dtCco As New Data.DataTable
            obj.AbrirConexion()
            dtCco = obj.TraerDataTable("LOG_BuscaCentroCosto", Me.HdCodigo_Cco.Value)
            obj.CerrarConexion()

            If (dtCco.Rows.Count > 0) Then
                If (dtCco.Rows(0).Item("fechainiciopropuesta_dev").ToString.Trim = "") Then
                    Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('No se encontró fecha de inicio al avento')</SCRIPT>")
                Else                    
                    If (Date.Parse(dtCco.Rows(0).Item("fechainiciopropuesta_dev").ToString()) <= Date.Today) Then
                        Me.btnEliminarPreInscrito.Enabled = True
                    Else
                    End If
                End If
            Else
                Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('No se encontró el evento')</SCRIPT>")
            End If
            dtCco.Dispose()
        Catch ex As Exception
            obj.CerrarConexion()
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaCombo()
        Me.ddlEstado.Items.Add("Todos")
        Me.ddlEstado.Items.Add("Confirmado")
        Me.ddlEstado.Items.Add("Sin Confirmar")
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        CargaGrid()
    End Sub

    Private Sub CargaGrid()
        Dim obj As New ClsConectarDatos
        Dim strEstado As String
        Try
            If (Me.ddlEstado.SelectedItem.Value = "Todos") Then
                strEstado = ""
            Else
                strEstado = Me.ddlEstado.SelectedItem.ToString.Substring(0, 1)
            End If            

            Dim cod_Cco As Integer
            cod_Cco = Integer.Parse(Request.QueryString("cco"))
            Me.HdCodigo_Cco.Value = cod_Cco

            Dim dtsConsulta As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtsConsulta = obj.TraerDataTable("EVE_BuscaPreInscritos", 0, strEstado, cod_Cco)
            gvPreInscritos.DataSource = dtsConsulta
            gvPreInscritos.DataBind()
            
            obj.CerrarConexion()
            obj = Nothing
            dtsConsulta.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message)
            obj.CerrarConexion()
        End Try
    End Sub

    Protected Sub gvPreInscritos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvPreInscritos.RowDeleting
        Dim obj As New ClsConectarDatos
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("EVE_EliminaPreInscripcion", _
                         gvPreInscritos.DataKeys(e.RowIndex).Value("codigo_pins").ToString, _
                         gvPreInscritos.DataKeys(e.RowIndex).Value("modo_pins").ToString)
            obj.CerrarConexion()
            CargaGrid()
        Catch ex As Exception
            Response.Write(ex.Message)
            obj.CerrarConexion()
        End Try
    End Sub

    Protected Sub btnInscribir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInscribir.Click
        Dim obj As New ClsConectarDatos
        Dim Fila As GridViewRow
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.IniciarTransaccion()

            Dim k As Integer
            'Buscamos el Ciclo Académico Activo
            Dim dtCicloAcademico As New Data.DataTable
            Dim codigo_cac As Integer
            dtCicloAcademico = obj.TraerDataTable("EVE_CicloAcademicoActual")
            If (dtCicloAcademico.Rows.Count > 0) Then
                codigo_cac = Integer.Parse(dtCicloAcademico.Rows(0).Item("codigo_Cac").ToString)
            End If
            dtCicloAcademico.Dispose()

            'Buscamos Servicio Concepto
            Dim dtServicio As New Data.DataTable
            Dim codigo_Sco As Integer
            Dim Precio_Sco As Double
            Dim strMoneda As String = "S"
            dtServicio = obj.TraerDataTable("LOG_BuscaServicioConcepto", 0, Me.HdCodigo_Cco.Value)

            For k = 0 To dtServicio.Rows.Count - 1
                If (dtServicio.Rows(k).Item("defecto").ToString.Trim = "True") Then
                    codigo_Sco = Integer.Parse(dtServicio.Rows(k).Item("codigo_Sco").ToString)
                    Precio_Sco = Double.Parse(dtServicio.Rows(k).Item("precio_Sco").ToString())
                    strMoneda = dtServicio.Rows(k).Item("moneda_Sco").ToString()
                End If
            Next
            dtServicio.Dispose()

            Dim i As Integer
            For i = 0 To Me.gvPreInscritos.Rows.Count - 1
                'Capturamos las filas que estan activas
                Fila = Me.gvPreInscritos.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                If (valor = True) Then
                    'Se confirma asistencia al evento
                    obj.Ejecutar("EVE_ConfirmaInscripcion", _
                            Me.gvPreInscritos.DataKeys.Item(Fila.RowIndex).Values("codigo_pins"))

                    '********************** GENERAMOS EL CARGO PARA LA PERSONA QUE SE INSCRIBIO **********************                    
                    'Buscamos la Pre-Inscripcion
                    Dim dtPreInscrito As New Data.DataTable
                    dtPreInscrito = obj.TraerDataTable("EVE_BuscaPreInscritos", _
                                        Me.gvPreInscritos.DataKeys.Item(Fila.RowIndex).Values("codigo_pins"), _
                                        "", Me.HdCodigo_Cco.Value)

                    If (dtPreInscrito.Rows.Count > 0) Then
                        Dim strTipo As String = ""

                        If (dtPreInscrito.Rows(0).Item("codigo_alu").ToString <> "") Then
                            strTipo = "E"
                        End If

                        If (dtPreInscrito.Rows(0).Item("codigo_per").ToString <> "") Then
                            strTipo = "P"
                        End If

                        If (dtPreInscrito.Rows(0).Item("codigo_pot").ToString <> "") Then
                            strTipo = "O"
                        End If

                        obj.Ejecutar("EVE_AgregarDeuda2", strTipo, dtPreInscrito.Rows(0).Item("codigo_alu") _
                                        , dtPreInscrito.Rows(0).Item("codigo_per") _
                                        , dtPreInscrito.Rows(0).Item("codigo_pot") _
                                        , dtPreInscrito.Rows(0).Item("codigo_pso") _
                                        , codigo_Sco, codigo_cac, Precio_Sco, strMoneda _
                                        , Precio_Sco, "P", 1, dtPreInscrito.Rows(0).Item("codigo_cco") _
                                        , 1, 0.0)
                    End If
                End If
            Next

            obj.TerminarTransaccion()
            CargaGrid()
            Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('Registro Guardado')</SCRIPT>")
        Catch ex As Exception
            Response.Write(ex.Message)
            obj.AbortarTransaccion()
        End Try
    End Sub

    Protected Sub btnEliminarPreInscrito_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminarPreInscrito.Click
        Dim obj As New ClsConectarDatos
        Dim Fila As GridViewRow
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            'Buscamos el Ciclo Académico Activo
            Dim dtCicloAcademico As New Data.DataTable
            Dim codigo_cac As Integer
            dtCicloAcademico = obj.TraerDataTable("EVE_CicloAcademicoActual")
            If (dtCicloAcademico.Rows.Count > 0) Then
                codigo_cac = Integer.Parse(dtCicloAcademico.Rows(0).Item("codigo_Cac").ToString)
            End If
            dtCicloAcademico.Dispose()

            For i As Integer = 0 To Me.gvPreInscritos.Rows.Count - 1
                Fila = Me.gvPreInscritos.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                If (valor = True) Then                    
                    Dim dtDeudas As New Data.DataTable
                    dtDeudas = obj.TraerDataTable("EVE_BuscaDeudaPersona", _
                                    Me.gvPreInscritos.DataKeys.Item(Fila.RowIndex).Values("codigo_pso"), _
                                    codigo_cac, Me.HdCodigo_Cco.Value)

                    'Si es CERO no tiene deudas y puede eliminarse
                    If (dtDeudas.Rows.Count = 0) Then
                        obj.Ejecutar("EVE_EliminaPreInscripcion", _
                             Me.gvPreInscritos.DataKeys.Item(Fila.RowIndex).Values("codigo_pins").ToString, _
                             Me.gvPreInscritos.DataKeys.Item(Fila.RowIndex).Values("modo_pins").ToString)
                    End If
                    dtDeudas.Dispose()
                End If
            Next
            obj.CerrarConexion()

            CargaGrid()
        Catch ex As Exception
            obj.CerrarConexion()
            Response.Write(ex.Message)
        End Try
    End Sub
End Class

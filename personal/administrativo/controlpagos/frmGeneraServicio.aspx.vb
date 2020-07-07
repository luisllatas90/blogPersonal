
Partial Class administrativo_controlpagos_frmGeneraServicio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Request.QueryString("id") IsNot Nothing) Then
                Dim cls As New ClsConectarDatos

                'Seleccionamos el Ciclo académico activo
                CargaCicloActivo()

                'Listamos los Conceptos del Ciclo actual
                CargaDetalleConceptos(Me.HdCodigo_cac.Value)

                'Inicializamos la acción
                Me.HdAccion.Value = "N"

                'Limpiamos los controles
                LimpiaControles()
            Else
                Me.lblMensaje.Text = "No se encontró al usuario"
            End If
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar la página: " & ex.Message
        End Try
    End Sub

    Private Sub CargaCicloActivo()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("EVE_CicloAcademicoActual")
            obj.CerrarConexion()
            obj = Nothing

            If dt.Rows.Count > 0 Then
                Me.HdCodigo_cac.Value = dt.Rows(0).Item("codigo_cac").ToString
                Me.lblCiclo.Text = "Ciclo Académico: " & dt.Rows(0).Item("descripcion_cac").ToString
            End If
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar archivo"
        End Try
    End Sub

    Private Function RetornaUsuarioPersonal(ByVal Codigo_per As Integer) As String
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("MAT_RetornaUsuarioPersonal", Codigo_per)
            obj.CerrarConexion()
            obj = Nothing

            If (dt.Rows.Count > 0) Then
                Return dt.Rows(0).Item("usuario_per").ToString
            Else
                Me.lblMensaje.Text = "No se encontró al usuario"
            End If

            Return ""
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar datos del usuario."
            Return ""
        End Try
    End Function

    Private Sub CargaDetalleConceptos(ByVal Ciclo As Integer)
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("MAT_BuscaServicio", Ciclo)
            obj.CerrarConexion()
            obj = Nothing

            gvDetalle.DataSource = dt
            gvDetalle.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar conceptos: " & ex.Message
        End Try
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click        
        Try
            'Capturamos el usuario del personal
            Dim user As String = RetornaUsuarioPersonal(Request.QueryString("id"))
            Dim UserReg As String = user & " | " & Request.ServerVariables("REMOTE_ADDR")

            If (Me.HdAccion.Value = "N") Then
                LimpiaControles()
            ElseIf (Me.HdAccion.Value = "M") Then
                LimpiaControles()
            End If
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al registrar concepto: " & ex.Message
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        CargaServicioConcepto(Me.txtConcepto.Text)
    End Sub

    Private Sub CargaServicioConcepto(ByVal Descripcion As String)
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("MAT_BuscaServicioConcepto", 0, Descripcion)
            obj.CerrarConexion()
            obj = Nothing

            Me.gvConceptos.DataSource = dt
            Me.gvConceptos.DataBind()
            Me.gvConceptos.Visible = True
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar conceptos: " & ex.Message
        End Try
    End Sub

    Protected Sub gvConceptos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvConceptos.PageIndexChanging
        gvConceptos.PageIndex = e.NewPageIndex()
        Me.gvConceptos.DataBind()
        CargaServicioConcepto(Me.txtConcepto.Text)
    End Sub

    Protected Sub gvConceptos_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles gvConceptos.SelectedIndexChanging
        Me.HdCodigo_sco.Value = Me.gvConceptos.DataKeys.Item(e.NewSelectedIndex).Values(0)
        Me.txtConcepto.Text = Me.gvConceptos.Rows(e.NewSelectedIndex).Cells("descripcion_Sco").Text
        Me.txtInicio.Text = Me.gvConceptos.Rows(e.NewSelectedIndex).Cells("fechaInicio_Sco").Text
        Me.txtVence.Text = Me.gvConceptos.Rows(e.NewSelectedIndex).Cells("fechaFin_Sco").Text
        Me.txtPago.Text = Me.gvConceptos.Rows(e.NewSelectedIndex).Cells("fechaFin_Sco").Text
        Me.txtMonto.Text = Me.gvConceptos.Rows(e.NewSelectedIndex).Cells("precio_Sco").Text

        Me.gvConceptos.Visible = False
    End Sub

    Private Sub LimpiaControles()
        Me.HdCodigo_sco.Value = ""
        Me.txtConcepto.Text = ""
        Me.txtInicio.Text = ""
        Me.txtVence.Text = ""
        Me.txtPago.Text = ""
        Me.txtMonto.Text = ""
        Me.HdAccion.Value = "N"

        'Limpiamos Grid Concepto
        Me.gvConceptos.DataSource = Nothing
        Me.gvConceptos.DataBind()
        Me.gvConceptos.Visible = False
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        LimpiaControles()
    End Sub


End Class


Partial Class administrativo_pension_FrmServicioConcepto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        Try
            If (IsPostBack = False) Then
                Me.lblMensaje.Text = ""
                Me.hdcodigo_dsb.Value = ""
                CargaConceptos()
                CargaCicloAcademico()
                CargaMeses()
                MuestraTablas(False)
            End If
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar página"
        End Try
    End Sub

    Private Sub CargaMeses()
        Me.cboMes.Items.Add("TODOS")
        Me.cboMes.Items.Add("Enero")
        Me.cboMes.Items.Add("Febrero")
        Me.cboMes.Items.Add("Marzo")
        Me.cboMes.Items.Add("Abril")
        Me.cboMes.Items.Add("Mayo")
        Me.cboMes.Items.Add("Junio")
        Me.cboMes.Items.Add("Julio")
        Me.cboMes.Items.Add("Agosto")
        Me.cboMes.Items.Add("Setiembre")
        Me.cboMes.Items.Add("Octubre")
        Me.cboMes.Items.Add("Noviembre")
        Me.cboMes.Items.Add("Diciembre")
    End Sub

    Private Sub CargaCicloAcademico()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ListaCicloAcademico")
            obj.CerrarConexion()
            obj = Nothing

            Me.cboCiclo.DataValueField = "codigo_cac"
            Me.cboCiclo.DataTextField = "descripcion_cac"
            Me.cboCiclo.DataSource = dt
            Me.cboCiclo.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub CargaConceptos()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("MAT_ListaServicioConcepto", 0, Me.cboTipo.SelectedValue)
            obj.CerrarConexion()
            obj = Nothing

            Me.gvServicios.DataSource = dt
            Me.gvServicios.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar conceptos: " & ex.Message
        End Try
    End Sub

    Protected Sub cboTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipo.SelectedIndexChanged
        CargaConceptos()
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        If (Me.hdcodigo_dsb.Value <> "") Then
            If (ValidaDatos() = True) Then                
                ActualizaDetalle(Me.hdcodigo_dsb.Value, Me.txtfecha.Text, Me.cboMes.SelectedItem.Text, Me.cboCiclo.SelectedValue)
                Me.hdcodigo_dsb.Value = ""
                Me.txtfecha.Text = ""
                CargaDetalle(ConcatenaConceptos)
            End If
        Else
            If (ValidaDatos() = True And ValidaDatosGrid() = True) Then
                Me.lblMensajeDetalle.Text = ""
                Dim Fila As GridViewRow
                For i As Integer = 0 To Me.gvServicios.Rows.Count - 1
                    Fila = Me.gvServicios.Rows(i)
                    Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                    If (valor = True) Then
                        AgregarDetalle(Me.txtfecha.Text, Me.cboMes.SelectedItem.Text, Me.gvServicios.DataKeys.Item(Fila.RowIndex).Values("codigo_sco"), Me.cboCiclo.SelectedValue)
                    End If
                Next
                CargaDetalle(ConcatenaConceptos)
                Me.txtfecha.Text = ""
            End If
        End If
    End Sub

    Private Sub ActualizaDetalle(ByVal Codigo As Integer, ByVal Fecha As Date, ByVal Mes As String, ByVal Ciclo As Integer)
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("MAT_ActualizaEnvioBanco", Codigo, Fecha.ToString("yyyy/MM/dd"), RetornaNumeroMes(Mes), Ciclo)
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al actualizar: " & ex.Message
        End Try
    End Sub

    Private Sub AgregarDetalle(ByVal fecha As Date, ByVal mes As String, ByVal concepto As Integer, ByVal Ciclo As Integer)
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("MAT_InsertaDetalleServicioBanco", fecha.ToString("yyyy/MM/dd"), RetornaNumeroMes(mes), concepto, Ciclo)
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al guardar: " & ex.Message
        End Try
    End Sub

    Private Function RetornaNumeroMes(ByVal Mes As String) As Integer
        Select Case Mes
            Case "TODOS" : Return 13
            Case "Enero" : Return 1
            Case "Febrero" : Return 2
            Case "Marzo" : Return 3
            Case "Abril" : Return 4
            Case "Mayo" : Return 5
            Case "Junio" : Return 6
            Case "Julio" : Return 7
            Case "Agosto" : Return 8
            Case "Setiembre" : Return 9
            Case "Octubre" : Return 10
            Case "Noviembre" : Return 11
            Case "Diciembre" : Return 12
        End Select
    End Function

    Private Function ValidaDatosGrid() As Boolean
        Dim scoDet, sco, mes, mesCbo As Integer
        For i As Integer = 0 To Me.gvFechas.Rows.Count - 1
            scoDet = Me.gvFechas.DataKeys.Item(i).Values("codigo_sco")
            mes = Me.gvFechas.DataKeys.Item(i).Values("mes_dsb")
            mesCbo = RetornaNumeroMes(Me.cboMes.SelectedValue)

            For j As Integer = 0 To Me.gvServicios.Rows.Count - 1
                sco = Me.gvServicios.DataKeys.Item(j).Values("codigo_sco")
                If (mesCbo = mes And sco = scoDet) Then
                    Me.lblMensajeDetalle.Text = "Ya agregó este envío"
                    Return False    'ya existe en el grid
                End If
            Next
        Next

        Return True
    End Function

    Private Function ValidaDatos() As Boolean
        If (Me.gvServicios.Rows.Count <= 0) Then
            Me.lblMensajeDetalle.Text = "No se encontró servicios asociados"
            Return False
        End If

        Try
            Date.Parse(Me.txtfecha.Text)    'Si hay error de tipo fecha retorna false
            Return True
        Catch ex As Exception
            Me.lblMensajeDetalle.Text = "Error al registrar la fecha"
            Return False
        End Try
    End Function

    Protected Sub btnConfigurar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfigurar.Click
        If (Me.cboTipo.SelectedValue = True) Then
            If (AptoParaEditar() = False) Then
                Me.lblMensaje.Text = ""
                MuestraTablas(True)
                CargaDetalle(ConcatenaConceptos)
            End If
        Else
            CargaDetalle(ConcatenaConceptos)
            MuestraTablas(True)
        End If
    End Sub

    Private Function AptoParaEditar() As Boolean
        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        'Verifica que solo tenga 1 check activo
        For i As Integer = 0 To Me.gvServicios.Rows.Count - 1
            Fila = Me.gvServicios.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If (valor = True) Then
                If (sw = 1) Then
                    Me.lblMensaje.Text = "Solo debe seleccionar un concepto"
                    Return False
                End If
                sw = 1
            End If
        Next

        If (sw = 0) Then
            Me.lblMensaje.Text = "Debe seleccionar un concepto"
            Return False
        End If

        Return True
    End Function

    Private Sub MuestraTablas(ByVal sw As Boolean)
        Me.tbDatos.Visible = sw
        Me.gvFechas.Visible = sw
        Me.btnAgregar.Visible = sw
        Me.btnConfigurar.Visible = Not sw
        Me.gvServicios.Enabled = Not sw
        Me.cboCiclo.Enabled = Not sw
        Me.cboTipo.Enabled = Not sw
    End Sub

    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        MuestraTablas(False)
        Me.lblMensajeDetalle.Text = ""
        Me.hdcodigo_dsb.Value = ""
        Dim Fila As GridViewRow
        For i As Integer = 0 To Me.gvServicios.Rows.Count - 1
            Fila = Me.gvServicios.Rows(i)
            CType(Fila.FindControl("chkElegir"), CheckBox).Checked = False            
        Next
    End Sub

    Private Sub CargaDetalle(ByVal codigo_sco As String)
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()            
            dt = obj.TraerDataTable("MAT_ListaDetalleServicioBanco", Me.cboCiclo.SelectedValue, codigo_sco)
            obj.CerrarConexion()
            obj = Nothing

            Me.gvFechas.DataSource = dt
            Me.gvFechas.DataBind()
        Catch ex As Exception
            Me.lblMensajeDetalle.Text = "Error al cargar los detalles: " & ex.Message
        End Try
    End Sub

    Private Function ConcatenaConceptos() As String
        Dim Fila As GridViewRow
        Dim sco As String = ""
        For i As Integer = 0 To Me.gvServicios.Rows.Count - 1
            Fila = Me.gvServicios.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If (valor = True) Then
                sco = sco & Me.gvServicios.DataKeys.Item(Fila.RowIndex).Values("codigo_sco").ToString & ","
            End If
        Next

        Return sco
    End Function

    Protected Sub gvFechas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvFechas.RowDeleting
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("MAT_EliminaServicioBanco", Me.gvFechas.DataKeys.Item(e.RowIndex).Values("codigo_dsb").ToString)
            obj.CerrarConexion()
            obj = Nothing

            CargaDetalle(ConcatenaConceptos)
        Catch ex As Exception
            Me.lblMensajeDetalle.Text = "Error al eliminar un registro: " & ex.Message
        End Try
    End Sub

    Protected Sub gvFechas_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvFechas.RowEditing
        If (Me.gvFechas.Rows.Count > 0) Then           
            Me.hdcodigo_dsb.Value = Me.gvFechas.DataKeys.Item(e.NewEditIndex).Values("codigo_dsb").ToString
            Me.cboMes.SelectedValue = Me.gvFechas.Rows(e.NewEditIndex).Cells(4).Text
            Me.txtfecha.Text = Me.gvFechas.Rows(e.NewEditIndex).Cells(5).Text
        End If
    End Sub
End Class

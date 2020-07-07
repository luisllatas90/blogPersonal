
Partial Class sistema_frmpermisosrecursos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            If (Request.QueryString("id") IsNot Nothing) Then
                CargaDocente()
                Me.hdPersonal.Value = 0
                Me.lblMensaje.Text = ""
            Else
                Me.lblMensaje.Text = "No se encontró usuario"
            End If
            
        End If
    End Sub

    Private Sub CargaDocente()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarPersonal", "TO", 0)
            obj.CerrarConexion()

            Me.LstDocente.DataValueField = "codigo_per"
            Me.LstDocente.DataTextField = "personal"
            Me.LstDocente.DataSource = dt
            Me.LstDocente.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error: " & ex.Message            
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Me.hdPersonal.Value = Me.LstDocente.SelectedValue
        CargaCentroCostos(Me.hdPersonal.Value, Me.cboModo.SelectedValue)
    End Sub

    Private Sub CargaCentroCostos(ByVal codigo_per As Integer, ByVal modo As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarAccesoRecurso_v2", modo, codigo_per, 0, 0)
            obj.CerrarConexion()

            Me.dgvCentroCosto.DataSource = dt
            Me.dgvCentroCosto.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error: " & ex.Message
        End Try        
    End Sub

    Protected Sub dgvCentroCosto_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgvCentroCosto.RowDeleting
        Dim obj As New ClsConectarDatos
        Try
            
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("AgregarAccesoRecurso", "E", Request.QueryString("id"), _
                         Me.hdPersonal.Value, retornaTabla, _
                         Me.dgvCentroCosto.Rows(e.RowIndex).Cells(0).Text, DBNull.Value, _
                         DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _
                         DBNull.Value, DBNull.Value, DBNull.Value)
            obj.CerrarConexion()            

            CargaCentroCostos(Me.hdPersonal.Value, Me.cboModo.SelectedValue)
        Catch ex As Exception
            Me.lblMensaje.Text = "Error: " & ex.Message
        End Try
    End Sub

    Protected Sub dgvCentroCosto_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvCentroCosto.RowDataBound
        'Columna 2: Marca
        If (e.Row.RowIndex >= 0) Then
            If (e.Row.Cells(2).Text.Trim <> "CHECKED") Then
                'Ocultar Columna 4: Quitar
                e.Row.Cells(4).Text = ""
            Else
                'Ocultar Columna 3: Activar
                e.Row.Cells(3).Text = ""
            End If
        End If
    End Sub

    Protected Sub dgvCentroCosto_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvCentroCosto.DataBound
        Me.dgvCentroCosto.Columns(2).Visible = False
    End Sub

    Private Function retornaTabla() As String
        Dim tabla As String = ""
        Select Case Me.cboModo.SelectedValue
            Case 1 : tabla = "carreraprofesional"
            Case 2 : tabla = "ambiente"
            Case 5 : tabla = "servicioconcepto"
            Case 8 : tabla = "centrocostos"
            Case 10 : tabla = "departamentoacademico"
        End Select

        Return tabla
    End Function

    Protected Sub dgvCentroCosto_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles dgvCentroCosto.RowEditing
        Dim obj As New ClsConectarDatos
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("AgregarAccesoRecurso", "S", Request.QueryString("id"), _
                         Me.hdPersonal.Value, retornaTabla, _
                         Me.dgvCentroCosto.Rows(e.NewEditIndex).Cells(0).Text, _
                         DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _
                         DBNull.Value, DBNull.Value, DBNull.Value)
            obj.CerrarConexion()

            CargaCentroCostos(Me.hdPersonal.Value, Me.cboModo.SelectedValue)
        Catch ex As Exception
            Me.lblMensaje.Text = "Error: " & ex.Message
        End Try
    End Sub
End Class

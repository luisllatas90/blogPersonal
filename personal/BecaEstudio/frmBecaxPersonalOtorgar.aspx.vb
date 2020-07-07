
Partial Class BecaEstudio_frmBecaxConvenio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargarCombos()
                CargarRegistros()

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarRegistros()
        Try
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("Beca_ListaBecaSolicitudPersonal", 0, Me.ddlCiclo.SelectedValue, Me.ddlDedicacion.selectedValue)
            'Response.Write(tb.Rows.Count)
            If tb.Rows.Count > 0 Then
                Me.gvListaBecas.DataSource = tb
            Else
                Me.gvListaBecas.DataSource = Nothing
            End If
            Me.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function validaCheckActivo() As Boolean
        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Dim d As Integer = 0
        Dim destinatarios As String = ""
        'Me.lblDestinatario.Text = ""
        For i As Integer = 0 To Me.gvListaBecas.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = Me.gvListaBecas.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If (valor = True) Then
                d = d + 1
                sw = 1                
            End If
        Next
        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function

    Sub CargarCombos()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("Beca_ConsultarCicloAcademico")

        Me.ddlCiclo.DataSource = tb
        Me.ddlCiclo.DataTextField = "descripcion_cac"
        Me.ddlCiclo.DataValueField = "codigo_cac"
        Me.ddlCiclo.DataBind()
        If Session("Beca_codigo_cac") IsNot Nothing Then
            Me.ddlCiclo.SelectedValue = Session("codigo_cac")
        End If

   

        tb = obj.TraerDataTable("Beca_ListaCobertura")
        Me.ddlCobertura.DataSource = tb
        Me.ddlCobertura.DataTextField = "porcentaje_bco"
        Me.ddlCobertura.DataValueField = "codigo_bco"
        Me.ddlCobertura.DataBind()


        obj.CerrarConexion()
        obj = Nothing
    End Sub



    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim valoresdevueltos(1) As Integer
    
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            Dim Fila As GridViewRow
            For i As Integer = 0 To Me.gvListaBecas.Rows.Count - 1
                Fila = Me.gvListaBecas.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                If (valor = True) Then
                    If CInt(Me.gvListaBecas.DataKeys(i).Values("codigo_bso").ToString) > 0 Then
                        tb = obj.TraerDataTable("Beca_RegistraSolictudBecaPersonal", _
                                                Me.gvListaBecas.DataKeys(i).Values("codigo_bso").ToString, _
                                                CInt(Me.ddlCobertura.SelectedValue))
                        
                        If tb.Rows(0).Item("rpta") = -1 Then
                            Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ocurrio un error al tratar de registrar la solicitud.');", True)
                            Exit Sub
                        Else
                                Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('La solicitud fue registrada correctamente.');", True)
                        End If
                    End If
                End If                    
            Next
            CargarRegistros()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaBecas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvListaBecas.RowDeleting        
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()        
        obj.Ejecutar("BECA_RechazaSolicitud", Me.gvListaBecas.DataKeys.Item(e.RowIndex).Values(0))
        obj.CerrarConexion()
        obj = Nothing

        CargarRegistros()
    End Sub

 
   

    Protected Sub ddlCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCiclo.SelectedIndexChanged
        CargarRegistros()
    End Sub


   

    Protected Sub ddlDedicacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDedicacion.SelectedIndexChanged
        CargarRegistros()
    End Sub
End Class

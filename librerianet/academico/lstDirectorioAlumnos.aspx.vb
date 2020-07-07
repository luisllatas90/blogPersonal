
Partial Class librerianet_academico_lstDirectorioAlumnos
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Llenar combos
            Dim x As Int16 = 0
            Me.dpLetra.Items.Clear()
            For i As Int16 = 65 To 90
                x = x + 1
                Me.dpLetra.Items.Add(Chr(i))
            Next

            'Cargar el ciclo académico
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            ClsFunciones.LlenarListas(Me.dpCiclo, obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0), "codigo_cac", "descripcion_Cac")
            obj = Nothing
            Me.cmdGuardar0.Attributes.Add("disabled", "true")
            Me.cmdGuardar1.Attributes.Add("disabled", "true")
        End If
    End Sub
    Private Sub BuscarDirectorioEstudiantes()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)

        Me.GridView1.DataSource = obj.TraerDataTable("ConsultarDirectorioAlumnos", 1, Me.dpLetra.Text, Me.dpRevision.SelectedValue, Me.dpCiclo.SelectedValue, 0)
        Me.GridView1.DataBind()
        obj = Nothing
    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        BuscarDirectorioEstudiantes()
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Dim id As String

        id = Request.QueryString("id")

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1 'Me.GridView1.DataKeys(e.Row.RowIndex).Value
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "HabilitarEnvio(this)")
            CType(e.Row.FindControl("cmdVer"), Button).Attributes.Add("OnClick", "AbrirPopUp('frmcambiardatosalumno.aspx?c=" & fila.Row("codigouniver_alu") & "&x=" & fila.Row("codigo_alu").ToString & "&id=" & ID & "','550','650','yes','yes');return(false);")

        End If
    End Sub
    Protected Sub cmdGuardar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar0.Click, cmdGuardar1.Click
        Dim I As Integer
        Dim Fila As GridViewRow
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)

        Try
            obj.IniciarTransaccion()
            For I = 0 To Me.GridView1.Rows.Count - 1
                Fila = Me.GridView1.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        '==================================
                        ' Guardar los datos
                        '==================================
                        obj.Ejecutar("ALU_ActualizarEstadoRevisionDatos", Me.GridView1.DataKeys.Item(Fila.RowIndex).Values("codigo_alu"), Request.QueryString("id"), 1)
                    End If
                End If
            Next
            obj.TerminarTransaccion()
            obj = Nothing
            BuscarDirectorioEstudiantes()

        Catch ex As Exception
            obj.AbortarTransaccion()
            Page.RegisterStartupScript("CambioEstado", "<script>alert('Ocurrió un Error al Registrar el estado. Intente mas tarde." & Chr(13) & ex.Message & "')</script>")
            obj = Nothing
        End Try
    End Sub
End Class

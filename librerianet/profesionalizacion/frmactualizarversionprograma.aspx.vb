
Partial Class frmactualizarversionprograma
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Llenar combos
            Dim x As Int16 = 0
            Me.dpVersion.Items.Clear()
            Me.dpVersion.Items.Add("--Todos--")
            Me.dpVersion.Items(0).Value = -1

            For i As Int16 = 0 To 11
                Me.dpVersion.Items.Add(i)
            Next

            Dim cls As New ClsFunciones
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            cls.CargarListas(Me.dpPlanEstudio, obj.TraerDataTable("ConsultarDatosProgramaEspecial", 2, Request.QueryString("ctf"), Request.QueryString("id"), 0, 0), "codigo_pes", "descripcion_pes", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
            cls = Nothing
        End If
    End Sub
    Private Sub BuscarDirectorioEstudiantes()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.GridView1.DataSource = obj.TraerDataTable("ConsultarDatosProgramaEspecial", 0, Me.dpPlanEstudio.SelectedValue, Me.dpVersion.SelectedValue, 0, 0)
        Me.GridView1.DataBind()
        obj.CerrarConexion()
        obj = Nothing
        Me.lblMensaje.Text = ""
        Me.cmdGuardar0.Visible = GridView1.Rows.Count > 0
        Me.cmdGuardar1.Visible = GridView1.Rows.Count > 0
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
            'If Request.QueryString("ctf") <> 1 And fila.Row("edicionProgramaEspecial_Alu") > 0 Then
            '    CType(e.Row.FindControl("dpversionprograma"), DropDownList).Enabled = true
            'CType(e.Row.FindControl("cmdVer"), Button).Attributes.Add("OnClick", "AbrirPopUp('frmcambiardatosalumno.aspx?c=" & fila.Row("codigouniver_alu") & "&x=" & fila.Row("codigo_alu").ToString & "&id=" & id & "','550','650','yes','yes');return(false);")
            'End If
        End If
    End Sub
    Protected Sub cmdGuardar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar0.Click, cmdGuardar1.Click
        Dim I As Integer
        Dim Fila As GridViewRow
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Try
            obj.IniciarTransaccion()
            obj.Ejecutar("ActualizarVersionProgramaEspecial", 0, 0, 0)

            For I = 0 To Me.GridView1.Rows.Count - 1
                Fila = Me.GridView1.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    Dim cbo As DropDownList = Fila.FindControl("dpversionprograma")
                    If cbo.Enabled = True Then
                        '==================================
                        ' Guardar los datos
                        '==================================
                        obj.Ejecutar("ActualizarVersionProgramaEspecial", 1, Me.GridView1.DataKeys.Item(Fila.RowIndex).Values("codigo_alu"), cbo.SelectedValue)
                    End If
                End If
            Next

            obj.Ejecutar("ActualizarVersionProgramaEspecial", 2, 0, 0)

            obj.TerminarTransaccion()
            obj = Nothing
            Me.lblMensaje.Text = "Los datos se actualizaron correctamente"

            'BuscarDirectorioEstudiantes()

        Catch ex As System.Data.SqlClient.SqlException
            obj.AbortarTransaccion()
            obj = Nothing
            If ex.Number = 2714 Then
                Me.lblMensaje.Text = "En estos momentos se está realizando otra operación, intentelo nuevamente"
            Else
                Me.lblMensaje.Text = ex.Number.ToString & "  " & "Ocurrió un Error al grabar los datos, intentelo nuevamente"
            End If

            

        End Try
    End Sub
End Class

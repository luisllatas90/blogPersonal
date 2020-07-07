
Partial Class DirectorDepartamento_administrarrevisores
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim ObjArbol As New Investigacion
            ClsFunciones.LlenarListas(Me.DDLUnidad, ObjArbol.ConsultarUnidadesInvestigacion("1D", Request.QueryString("id")), 0, 1, "---- Seleccione Unidad de Investigacion ----")
            ObjArbol = Nothing


        End If
    End Sub

    Protected Sub DDLUnidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLUnidad.SelectedIndexChanged
        Dim ObjPersonal As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        If Me.DDLUnidad.SelectedValue = 0 Then
            Me.LstPersonal.Items.Clear()
        Else
            ClsFunciones.LlenarListas(Me.LstPersonal, ObjPersonal.TraerDataTable("INV_ConsultarRevisoresFaltantes", Me.DDLUnidad.SelectedValue), "codigo_pcc", "datos_per")
        End If
        Me.GridAsignados.SelectedIndex = -1

        If Me.LstPersonal.Items.Count = 0 Then
            Me.CmdAsignar.Enabled = False
        Else
            Me.CmdAsignar.Enabled = True
        End If

        Me.LstAsignadas.Items.Clear()
        Me.LstFaltantes.Items.Clear()
    End Sub

    Protected Sub GridAsignados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridAsignados.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(1).Attributes.Add("OnClick", "javascript:__doPostBack('GridAsignados','Select$" & e.Row.RowIndex & "')")
        End If

    End Sub

    Protected Sub GridAsignados_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridAsignados.RowDeleting
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            obj.Ejecutar("INV_EliminarRevisor", e.Keys.Item(0))
            Me.GridAsignados.DataBind()
            Me.GridAsignados.SelectedIndex = -1
            Me.LstAsignadas.Items.Clear()
            Me.LstFaltantes.Items.Clear()
            ClsFunciones.LlenarListas(Me.LstPersonal, obj.TraerDataTable("INV_ConsultarRevisoresFaltantes", Me.DDLUnidad.SelectedValue), "codigo_pcc", "datos_per")
        Catch ex As Exception

        End Try
        e.Cancel = True
    End Sub

    Protected Sub GridAsignados_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridAsignados.SelectedIndexChanged
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(Me.LstFaltantes, Obj.TraerDataTable("INV_ConsultarInvestigacionSinAsignar", Me.DDLUnidad.SelectedValue), "codigo_inv", "titulo_inv")
        ClsFunciones.LlenarListas(Me.LstAsignadas, Obj.TraerDataTable("INV_ConsultarInvestigacionesAsignadas", Me.DDLUnidad.SelectedValue, Me.GridAsignados.SelectedValue), "codigo_inv", "titulo_inv")
        If Me.LstFaltantes.Items.Count = 0 Then
            Me.CmdAgregar.Enabled = False
        Else
            Me.CmdAgregar.Enabled = True
        End If
        If Me.LstAsignadas.Items.Count = 0 Then
            Me.CmdRetirar.Enabled = False
        Else
            Me.CmdRetirar.Enabled = True
        End If
        Me.Label1.Text = ""
    End Sub

    Protected Sub CmdAsignar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAsignar.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        Try
            Obj.Ejecutar("INV_InsertarRevisor", Me.LstPersonal.SelectedValue)
            ClsFunciones.LlenarListas(Me.LstPersonal, Obj.TraerDataTable("INV_ConsultarRevisoresFaltantes", Me.DDLUnidad.SelectedValue), "codigo_pcc", "datos_per")
            Me.GridAsignados.DataBind()
            Me.GridAsignados.SelectedIndex = -1
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAgregar.Click
        Dim lista As New SortedList
        For i As Int32 = 0 To Me.LstFaltantes.GetSelectedIndices.Length - 1
            lista.Add(i, Me.LstFaltantes.Items(Me.LstFaltantes.GetSelectedIndices.GetValue(i)).Value)
        Next
        Dim objinv As New Investigacion
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        If objinv.AgregarIvestigacionRevisor(lista, Me.GridAsignados.SelectedValue) <> -1 Then
            ClsFunciones.LlenarListas(Me.LstFaltantes, Obj.TraerDataTable("INV_ConsultarInvestigacionSinAsignar", Me.DDLUnidad.SelectedValue), "codigo_inv", "titulo_inv")
            ClsFunciones.LlenarListas(Me.LstAsignadas, Obj.TraerDataTable("INV_ConsultarInvestigacionesAsignadas", Me.DDLUnidad.SelectedValue, Me.GridAsignados.SelectedValue), "codigo_inv", "titulo_inv")

            If Me.LstFaltantes.Items.Count = 0 Then
                Me.CmdAgregar.Enabled = False
            Else
                Me.CmdAgregar.Enabled = True
            End If
            If Me.LstAsignadas.Items.Count = 0 Then
                Me.CmdRetirar.Enabled = False
            Else
                Me.CmdRetirar.Enabled = True
            End If

            Me.Label1.ForeColor = Drawing.Color.Blue
            Me.Label1.Text = "Se asignaron " & lista.Count.ToString & " investigaciones con satisfacción."
        Else
            Me.Label1.ForeColor = Drawing.Color.Red
            Me.Label1.Text = "Ocurrió un error al procesar los datos, intentelo nuevamente"
        End If
        objinv = Nothing
    End Sub

    Protected Sub CmdRetirar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdRetirar.Click
        Dim lista As New SortedList
        For i As Int32 = 0 To Me.LstAsignadas.GetSelectedIndices.Length - 1
            lista.Add(i, Me.LstAsignadas.Items(Me.LstAsignadas.GetSelectedIndices.GetValue(i)).Value)
        Next
        Dim objinv As New Investigacion
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        If objinv.RetirarInvestigacionRevisor(lista, Me.GridAsignados.SelectedValue) <> -1 Then
            ClsFunciones.LlenarListas(Me.LstFaltantes, Obj.TraerDataTable("INV_ConsultarInvestigacionSinAsignar", Me.DDLUnidad.SelectedValue), "codigo_inv", "titulo_inv")
            ClsFunciones.LlenarListas(Me.LstAsignadas, Obj.TraerDataTable("INV_ConsultarInvestigacionesAsignadas", Me.DDLUnidad.SelectedValue, Me.GridAsignados.SelectedValue), "codigo_inv", "titulo_inv")

            If Me.LstFaltantes.Items.Count = 0 Then
                Me.CmdAgregar.Enabled = False
            Else
                Me.CmdAgregar.Enabled = True
            End If
            If Me.LstAsignadas.Items.Count = 0 Then
                Me.CmdRetirar.Enabled = False
            Else
                Me.CmdRetirar.Enabled = True
            End If

            Me.Label1.ForeColor = Drawing.Color.Blue
            Me.Label1.Text = "Se retiraron " & lista.Count.ToString & " investigaciones con satisfacción."
        Else
            Me.Label1.ForeColor = Drawing.Color.Red
            Me.Label1.Text = "Ocurrió un error al procesar los datos, intentelo nuevamente"
        End If
        objinv = Nothing
    End Sub
End Class

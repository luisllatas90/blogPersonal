
Partial Class administrativo_biblioteca_FrmListaTesis
    Inherits System.Web.UI.Page

    Sub CargarTesis(ByVal tipo As String, ByVal anio As String, ByVal autor As String, ByVal estado As String)
        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        ' Lista Tesis por Año, Autor y estado.
        dt = obj.ListarTesis(tipo, anio, autor, estado)
        Me.lblMensajeFormulario.Text = "Se encontraron " & dt.Rows.Count.ToString & " registro(s)."
        If dt.Rows.Count = 0 Then
            'Me.lblMensajeFormulario.Text = "No se encontraron registros"
            Me.dgvTesis.DataSource = Nothing
        Else
            Me.dgvTesis.DataSource = dt
            'Me.dgvAsignar.Columns.Item(4).Visible = False 'codigo_pla
        End If
        Me.dgvTesis.DataBind()
        dt = Nothing
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        CargarTesis("L", Me.ddlAnio.SelectedValue, Me.txtAutor.Text, Me.ddlEstado.SelectedValue)
    End Sub

    Protected Sub dgvTesis_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgvTesis.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then
                Dim seleccion As GridViewRow
                Dim codigo As Integer
                Dim obj As New clsPlanOperativoAnual
                '1. Obtengo la linea del gridview que fue cliqueada
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                '2. Obtengo el datakey de la linea que donde está el boton que cliqueé
                codigo = Me.dgvTesis.DataKeys(seleccion.RowIndex).Values("codigo_tes")
                'Response.Redirect("FrmMantenimientoTesis.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cp=" & codigo_poa & "&cb1=" & Me.ddlPlan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlVigencia.SelectedValue)
                Response.Redirect("FrmMantenimientoTesis.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&c=" & codigo)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If IsPostBack = False Then
            ' Crea un nuevo Item
            Dim anio As Integer = Date.Now.Year
            Dim Datos As New Data.DataTable
            For i As Integer = 2002 To anio
                Dim List As New ListItem(i, i)
                ddlAnio.Items.Add(List)
            Next
            'ddlAnio.SelectedValue = Date.Now.Year
            Dim List1 As New ListItem("TODOS", "T")
            ddlAnio.Items.Add(List1)
            ddlAnio.SelectedValue = "T"
            'ddlAnio.DataBind()
        End If
 
    End Sub
End Class

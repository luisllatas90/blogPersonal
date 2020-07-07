Partial Class indicadores_POA_PROTOTIPOS_Registrar_POA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Or Request.QueryString("id") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Me.lblMensajeFormulario.Text = ""
            Me.ddlestado.SelectedValue = Request.QueryString("estado")
            Me.ddlPlan.SelectedValue = Request.QueryString("plan")
            Me.ddlEjercicio.SelectedValue = Request.QueryString("ejercicio")

            Call wf_cargarPEI()
            Call wf_cargarEjercicioPresupuestal()

            Call wf_cargarLista(Request.QueryString("estado"), 0)

            If Request.QueryString("UpDate") = "1" Then
                Me.lblrpta.Text = "Datos Registrados Correctamente"
                Me.aviso.Attributes.Add("class", "mensajeExito")
            End If
        End If
    End Sub

    Sub wf_cargarPEI()
        Dim obj As New clsPlanOperativoAnual
        Dim dtPEI As New Data.DataTable
        dtPEI = obj.ListaPeis
        Me.ddlPlan.DataSource = dtPEI
        Me.ddlPlan.DataTextField = "descripcion"
        Me.ddlPlan.DataValueField = "codigo"
        Me.ddlPlan.DataBind()
        dtPEI.Dispose()
        obj = Nothing
    End Sub

    Sub wf_cargarEjercicioPresupuestal()
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.ListaEjercicio
        Me.ddlEjercicio.DataSource = dtt
        Me.ddlEjercicio.DataTextField = "descripcion"
        Me.ddlEjercicio.DataValueField = "codigo"
        Me.ddlEjercicio.DataBind()
        dtt.Dispose()
        obj = Nothing

        'Response.Write(Me.ddlEjercicio.Items.Count - 1)
        Me.ddlEjercicio.SelectedIndex = Me.ddlEjercicio.Items.Count - 1
        'Me.ddlEjercicio.SelectedIndex = 2
    End Sub

    Function CargarGrid() As Data.DataTable
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.ListarAsignacionPresupuestal(Me.ddlestado.SelectedValue.ToString, IIf(ddlPlan.SelectedValue.ToString = 0, "%", ddlPlan.SelectedValue.ToString), IIf(ddlEjercicio.SelectedValue.ToString = 0, "%", ddlEjercicio.SelectedValue.ToString))
        obj = Nothing
        'Me.lblMensajeFormulario.Text = "Se encontraron " & dtt.Rows.Count & " registro(s)."
        Return dtt
    End Function

    Protected Sub dgvAsignar_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgvAsignar.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then
                Dim seleccion As GridViewRow

                ' ''1. Obtengo la linea del gridview que fue cliqueada
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                ' ''2. Obtengo el datakey de la linea que donde está el boton que cliqueé
                Me.HdCodigo_poa.Value = Me.dgvAsignar.DataKeys(seleccion.RowIndex).Values("codigo_poa").ToString
                Me.HdCodigo_cco.Value = Me.dgvAsignar.DataKeys(seleccion.RowIndex).Values("codigo_cco").ToString
                'Dim nombre_poa As String = dgvAsignar.Rows(seleccion.RowIndex).Cells(0).Text
                Dim nombre_poa As String = ""
                Dim limite_ingreso As Decimal = dgvAsignar.Rows(seleccion.RowIndex).Cells(3).Text
                Dim limite_egreso As Decimal = dgvAsignar.Rows(seleccion.RowIndex).Cells(4).Text
                'Dim utilidad As Decimal = dgvAsignar.Rows(seleccion.RowIndex).Cells(5).Text
                Dim utilidad As Decimal = 0
                Me.HdNombre_cco.Value = Me.dgvAsignar.DataKeys(seleccion.RowIndex).Values("nombre_cco").ToString

                Dim plan As Integer = ddlPlan.SelectedValue.ToString
                Dim ejercicio As Integer = ddlEjercicio.SelectedValue.ToString

                'Response.Write("ID=" & Request.QueryString("id") & " - ctf=" & Request.QueryString("ctf") & " - codigo_poa=" & HdCodigo_poa.Value & " - nombre_poa=" & nombre_poa & " - codigo_cco=" & HdCodigo_cco.Value & " - limite_ingreso=" & limite_ingreso & " - limite_egreso=" & limite_egreso & " - utilidad=" & utilidad & " - nombre_cco=" & HdNombre_cco.Value & " - estado=" & Me.ddlestado.SelectedValue & " - plan=" & plan & " - ejercicio=" & ejercicio)

                Response.Redirect("frmmantenimientolimitepresupuestal.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & _
                                "&codigo_poa=" & HdCodigo_poa.Value & "&nombre_poa=" & nombre_poa & _
                                "&codigo_cco=" & HdCodigo_cco.Value & "&limite_ingreso=" & limite_ingreso & _
                                "&limite_egreso=" & limite_egreso & "&utilidad=" & utilidad & "&nombre_cco=" & HdNombre_cco.Value & _
                                "&estado=" & Me.ddlestado.SelectedValue & "&plan=" & plan & "&ejercicio=" & ejercicio)

         

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Me.lblrpta.Text = ""
        Me.aviso.Attributes.Clear()
        Call wf_cargarLista(Me.ddlestado.SelectedValue.ToString, 0)
    End Sub

    Sub wf_cargarLista(ByVal estado As Integer, ByVal UpDate As String)
        Dim dt As New Data.DataTable
        dt = CargarGrid()
        If dt.Rows.Count = 0 Then
            Me.lblMensajeFormulario.Text = "No se encontraron registros"
            Me.dgvAsignar.DataSource = Nothing
        Else
            Me.dgvAsignar.DataSource = dt
        End If
        Me.dgvAsignar.DataBind()
        dt.Dispose()
    End Sub

End Class

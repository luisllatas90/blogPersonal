Imports System.Data
Imports System.Drawing

Partial Class Indicadores_Formularios_frmConfiguracionVariableCentroCosto
    Inherits System.Web.UI.Page

    Dim usuario As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargarComboVariables()
                CargaGridCentroCostos()
                'Debe tomar del inicio de sesión
                usuario = Request.QueryString("id")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub CargarComboVariables()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListarVariables()
            ddlVariable.DataSource = dts
            ddlVariable.DataTextField = "Descripcion"
            ddlVariable.DataValueField = "Codigo"
            ddlVariable.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaGridCentroCostos()
        Dim vCriterio As String = txtCentroCosto.Text.Trim
        Dim obj As New clsIndicadores
        Dim dts As New Data.DataTable

        'Consulta centro de costos no configurados para la variable
        'Segun el parametro de busqueda
        'Si no se ha elegido variable, devuelve todos los centro de costos
        dts = obj.ConsultarCentroCostosNoConfigurados(vCriterio, ddlVariable.SelectedValue)
        gvCCO1.DataSource = dts
        gvCCO1.DataBind()
        'gvCCO1.Dispose()
        'obj = Nothing
    End Sub


    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            'Para Validar Caja de Busqueda
            If Page.IsValid Then        'Si no ha ingresado cadenas inválidas (select, php, script)
                CargaGridCentroCostos()
            Else 'Limpia la cadena inválida de la caja de texto
                txtCentroCosto.Text = ""

            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaGridCentroCostosConfigurados()
        Dim obj As New clsIndicadores
        Dim dts As New Data.DataTable
        dts = obj.ConsultarCentroCostosConfigurados(ddlVariable.SelectedValue)
        gvCCO2.DataSource = dts
        gvCCO2.DataBind()
    End Sub

    Private Sub LimpiarGridCentroCostosSeleccionados()
        gvCCO2.DataSource = Nothing
        gvCCO2.DataBind()
    End Sub

    Protected Sub ddlVariable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlVariable.SelectedIndexChanged
        Try
            If ddlVariable.SelectedValue <> "0" Then
                Dim obj As New clsPersonal
                CargaGridCentroCostosConfigurados()

                'Vuelve a cargar el listado de centro de costos, para que no muestre los que estan configurados
                CargaGridCentroCostos()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Dim obj As New clsIndicadores
        Dim bandera As String

        bandera = 0
        lblMensaje.Visible = True

        'If txtCentroCosto.Text = "" Then
        '    lblMensaje.Text = "Ingrese el nombre del centro de costos a buscar. "
        '    txtCentroCosto.Focus()
        'End If

        For Each row As GridViewRow In gvCCO1.Rows

            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

            If check.Checked Then
                bandera = 1
                Dim codigo_cco As Integer = Convert.ToInt32(gvCCO1.DataKeys(row.RowIndex).Value)
                Dim codigo_var As String = ddlVariable.SelectedValue

                lblMensaje.Text = obj.InsertarConfiguracionCentroCostos(codigo_cco, codigo_var)

                If lblMensaje.Text = "Datos guardados correctamente." Then
                    'Para avisos
                    Me.Image1.Attributes.Add("src", "../Images/accept.png")
                    Me.avisos.Attributes.Add("class", "mensajeExito")
                Else
                    'Para avisos
                    Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                    Me.avisos.Attributes.Add("class", "mensajeError")
                End If
            End If
        Next

        CargaGridCentroCostosConfigurados()

        If (bandera = 0) Then
            lblMensaje.Text = "Seleccione el centro de costo que desea añadir. "

            'Para avisos
            Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
            Me.avisos.Attributes.Add("class", "mensajeError")
        End If

        'Cargamos nuevamente el gridview para quitar los registros chekados
        CargaGridCentroCostos()
    End Sub

    Protected Sub btnQuitar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuitar.Click
        Dim obj As New clsIndicadores
        Dim bandera As String
        bandera = 0
        lblMensaje.Visible = True

        For Each row As GridViewRow In gvCCO2.Rows
            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

            If check.Checked Then
                bandera = 1
                Dim codigo_cco As Integer = Convert.ToInt32(gvCCO2.DataKeys(row.RowIndex).Value)
                Dim codigo_var As String = ddlVariable.SelectedValue

                lblMensaje.Text = obj.EliminarCentroCostoConfigurado(codigo_cco, codigo_var)

                If lblMensaje.Text = "Se eliminó la configuración correctamente." Then                    
                    'Para avisos
                    Me.Image1.Attributes.Add("src", "../Images/accept.png")
                    Me.avisos.Attributes.Add("class", "mensajeExito")
                Else
                    'Para avisos
                    Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                    Me.avisos.Attributes.Add("class", "mensajeError")
                End If
            End If
        Next

        If (bandera = 0) Then
            lblMensaje.Text = "Seleccione el centro de costo que desea eliminar. "

            'Para avisos
            Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
            Me.avisos.Attributes.Add("class", "mensajeError")
        End If

        CargaGridCentroCostosConfigurados()
        'Cargamos nuevamente el gridview del listado de centro de costos para refrescar los cc que ya no estan configurados
        CargaGridCentroCostos()

    End Sub


    Protected Sub gvCCO1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvCCO1.PageIndexChanging
        gvCCO1.PageIndex = e.NewPageIndex
        CargaGridCentroCostos()
    End Sub

    Protected Sub gvCCO1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCCO1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim fila As Data.DataRowView
            'fila = e.Row.DataItem
            'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            'e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(1).Text = e.Row.RowIndex + 1
        End If
    End Sub


    Protected Sub gvCCO2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCCO2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim fila As Data.DataRowView
            'fila = e.Row.DataItem
            'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            'e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(1).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub CustomValidator2_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator2.ServerValidate
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ValidarPalabrasReservadas(args.Value.ToString.Trim)
            If dts.Rows(0).Item("Encontro") > 0 Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class

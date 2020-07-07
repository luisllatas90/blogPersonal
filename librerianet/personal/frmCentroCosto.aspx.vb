Imports System.Data

Partial Class personal_Default2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargaTipoActividad()
            Call MostrarOpciones(False)
        End If
    End Sub

    Private Sub CargaGridCentroCostosSeleccionados(ByVal vTipoActividad As Integer, ByVal vEsFacuDep As Integer, ByVal xTipo As String, ByVal xPeriodoLaborable As Integer)
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarCentroCostosSeleccionados(vTipoActividad, vEsFacuDep, xTipo, xPeriodoLaborable)
        gvCCO2.DataSource = dts
        gvCCO2.DataBind()
    End Sub

    Private Sub CargaTipoActividad()
        Dim objCentroCosto As New clsCentroCosto
        Dim dts As New Data.DataTable
        dts = objCentroCosto.ConsultarTipoActividad2()
        ddlTipoActividad.DataTextField = "descripcion_td"
        ddlTipoActividad.DataValueField = "codigo_td"
        ddlTipoActividad.DataSource = dts        
        ddlTipoActividad.DataBind()
        ddlTipoActividad.SelectedIndex = -1

    End Sub

    Protected Sub ddlTipoActividad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoActividad.SelectedIndexChanged
        Dim obj As New clsPersonal
        'Consulta el periodo laborable vigente
        Dim xPeriodoLaborable As Integer = obj.ConsultarPeriodoLaborableVigente()

        If ddlTipoActividad.SelectedIndex <> -1 Then
            If ddlTipoActividad.SelectedValue = 9 Then      '9 Facultad
                CargarFacultad()
                HabilitarEsFacuDep()
                LimpiarGridCentroCostosSeleccionados()
                Call MostrarOpciones(False)

            ElseIf ddlTipoActividad.SelectedValue = 5 Then  '5 Investigacion
                CargarDepartamento()
                HabilitarEsFacuDep()
                LimpiarGridCentroCostosSeleccionados()
                Call MostrarOpciones(False)

                '1 Administrativo Institucional '8 Horas Asistenciales Clinica USAT '16 Centro Pre 
            ElseIf ddlTipoActividad.SelectedValue = 1 Or ddlTipoActividad.SelectedValue = 8 Or ddlTipoActividad.SelectedValue = 16 Then
                DeshabilitarEsFacuDep()
                CargaGridCentroCostosSeleccionados(ddlTipoActividad.SelectedValue, 0, lblEsFacuDep.Text.Trim, xPeriodoLaborable)
                Call MostrarOpciones(False)

            ElseIf ddlTipoActividad.SelectedValue = 7 Then '7 Practicas externas
                CargarEscuela(7)
                HabilitarEsFacuDep()
                LimpiarGridCentroCostosSeleccionados()
                Call MostrarOpciones(False)

            ElseIf ddlTipoActividad.SelectedValue = 17 Then 'Carga Administrativa (Reemplaza a: Apoyo Admin en Facultad/Escuela)
                Call MostrarOpciones(True)
                Call HabilitarEsFacuDepCargaAcademica(True)
                Call LimpiarGridCentroCostosSeleccionados()

            Else
                Call MostrarOpciones(False)
                CargarEscuela(0)
                HabilitarEsFacuDep()
                LimpiarGridCentroCostosSeleccionados()
            End If
        End If
    End Sub

    Private Sub MostrarOpciones(ByVal vEstado As Boolean)
        Me.lblEsFacuDep.Visible = Not vEstado
        Me.rdbDepartamento.Visible = vEstado
        Me.rdbEscuela.Visible = vEstado
        Me.rdbFacultad.Visible = vEstado
    End Sub

    Private Sub LimpiarGridCentroCostosSeleccionados()
        gvCCO2.DataSource = Nothing
        gvCCO2.DataBind()
    End Sub

    Private Sub DeshabilitarEsFacuDep()
        ddlEsFacuDep.Enabled = False
        ddlEsFacuDep.Visible = False
        lblEsFacuDep.Text = ""
    End Sub

    Private Sub HabilitarEsFacuDep()
        ddlEsFacuDep.Enabled = True
        ddlEsFacuDep.Visible = True
    End Sub

    Private Sub CargarFacultad()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarFacultad()
        ddlEsFacuDep.DataSource = dts
        ddlEsFacuDep.DataTextField = "nombre_fac"
        ddlEsFacuDep.DataValueField = "codigo_fac"
        ddlEsFacuDep.DataBind()
        lblEsFacuDep.Text = "Facultad"
        ddlEsFacuDep.Enabled = True
    End Sub

    Private Sub CargarDepartamento()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarDptoAcademico()
        ddlEsFacuDep.DataSource = dts
        ddlEsFacuDep.DataTextField = "DptoAcademico"
        ddlEsFacuDep.DataValueField = "codigo"
        ddlEsFacuDep.DataBind()
        lblEsFacuDep.Text = "Departamento"
        ddlEsFacuDep.Enabled = True
    End Sub

    Private Sub CargarEscuela(ByVal tipo As Integer)
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable

        'Practica externa
        If tipo = 7 Then
            dts = obj.ConsultarCarreraProfesionalyCentros()
        Else
            dts = obj.ConsultarCarreraProfesional_v2()
        End If

        ddlEsFacuDep.DataSource = dts
        ddlEsFacuDep.DataTextField = "nombre_cpf"
        ddlEsFacuDep.DataValueField = "codigo_cpf"
        ddlEsFacuDep.DataBind()
        lblEsFacuDep.Text = "Escuela"
        ddlEsFacuDep.Enabled = True
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        CargaGridCentroCostos()
    End Sub

    Protected Sub gvCCO1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvCCO1.PageIndexChanging
        gvCCO1.PageIndex = e.NewPageIndex
        CargaGridCentroCostos()
    End Sub

    Private Sub CargaGridCentroCostos()
        Dim vCriterio As String = txtCentroCosto.Text.Trim
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        'Hcano 05-01-2016
        'dts = obj.ConsultarCentroCostos(vCriterio)
        'dts = obj.ConsultarCentroCostos_POA(vCriterio) 'Hcano 02-01-18
        dts = obj.ConsultarCentroCostosPlanilla_POA(vCriterio) 'Hcano 02-01-18
        'Fin Hcano
        gvCCO1.DataSource = dts
        gvCCO1.DataBind()
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Dim vMensaje As String = ""
        Dim bandera As String
        bandera = 0
        Dim obj As New clsPersonal
        Dim xPeriodoLaborable As Integer = obj.ConsultarPeriodoLaborableVigente()

        If ddlTipoActividad.Text = 0 Then vMensaje = "Seleccione el tipo de actividad."
        If ddlEsFacuDep.Enabled = True Then
            If ddlEsFacuDep.Text <> "" Then
                If ddlEsFacuDep.Text = 0 Then
                    vMensaje = vMensaje & "Seleccione " & lblEsFacuDep.Text & ". "
                End If
            End If
            
        End If
        If txtCentroCosto.Text = "" Then
            vMensaje = vMensaje & "Ingrese el nombre del centro de costos a buscar. "
            txtCentroCosto.Focus()
        End If

        If vMensaje = "" Then
            For Each row As GridViewRow In gvCCO1.Rows

                Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

                If check.Checked Then
                    bandera = 1
                    Dim xCod As Integer = Convert.ToInt32(gvCCO1.DataKeys(row.RowIndex).Value)
                    Dim xTipo As String = lblEsFacuDep.Text.Trim
                    'Consulta el periodo laborable vigente                  

                    If ddlEsFacuDep.Enabled = False Then
                        Dim dts As New DataTable
                        dts = obj.ConsultarDuplicadoCentroCostos(xCod, ddlTipoActividad.SelectedValue, 0, xTipo, xPeriodoLaborable)
                        If dts.Rows.Count = 0 Then
                            obj.InsertarCentroCostos(xCod, ddlTipoActividad.SelectedValue, 0, xTipo, xPeriodoLaborable)
                        Else
                            Dim myscript As String = "alert('El registro ya existe');"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        End If
                    Else
                        Dim dts As New DataTable
                        dts = obj.ConsultarDuplicadoCentroCostos(xCod, ddlTipoActividad.SelectedValue, ddlEsFacuDep.SelectedValue, xTipo, xPeriodoLaborable)
                        If dts.Rows.Count = 0 Then
                            obj.InsertarCentroCostos(xCod, ddlTipoActividad.SelectedValue, ddlEsFacuDep.SelectedValue, xTipo, xPeriodoLaborable)
                        Else
                            Dim myscript As String = "alert('El registro ya existe');"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        End If
                    End If
                End If

            Next

            If ddlEsFacuDep.Enabled = False Then
                CargaGridCentroCostosSeleccionados(ddlTipoActividad.SelectedValue, 0, lblEsFacuDep.Text.Trim, xPeriodoLaborable)
            Else
                CargaGridCentroCostosSeleccionados(ddlTipoActividad.SelectedValue, ddlEsFacuDep.SelectedValue, lblEsFacuDep.Text.Trim, xPeriodoLaborable)
            End If

            If (bandera = 0) Then
                vMensaje = vMensaje & "Seleccione el centro de costo que desea añadir. "
                Response.Write("<script>alert('" & vMensaje.Trim & "')</script>")
            End If

        Else
            Response.Write("<script>alert('" & vMensaje.Trim & "')</script>")
        End If
        'Cargamos nuevamente el gridview para quitar los registros chekados
        CargaGridCentroCostos()

    End Sub

    Protected Sub ddlEsFacuDep_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEsFacuDep.SelectedIndexChanged

        If ddlEsFacuDep.SelectedIndex <> -1 Then
            Dim obj As New clsPersonal
            Dim xPeriodoLaborable As Integer = obj.ConsultarPeriodoLaborableVigente()
            CargaGridCentroCostosSeleccionados(ddlTipoActividad.SelectedValue, ddlEsFacuDep.SelectedValue, lblEsFacuDep.Text.Trim, xPeriodoLaborable)
        End If
    End Sub


    Protected Sub btnQuitar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuitar.Click
        Dim obj As New clsPersonal
        'Consulta el periodo laborable vigente
        Dim xPeriodoLaborable As Integer = obj.ConsultarPeriodoLaborableVigente()

        If ddlTipoActividad.Text = 0 Then Exit Sub
        If ddlEsFacuDep.Enabled = True Then
            If ddlEsFacuDep.Text = "" Then
                Exit Sub
            End If
        End If

        For Each row As GridViewRow In gvCCO2.Rows
            Dim check As CheckBox = TryCast(row.FindControl("chkSeleccion"), CheckBox)

            If check.Checked Then
                Dim xCod As Integer = Convert.ToInt32(gvCCO2.DataKeys(row.RowIndex).Value)
                Dim xTipo As String = lblEsFacuDep.Text.Trim

                If ddlEsFacuDep.Enabled = False Then
                    obj.EliminarCentroCostos(xCod, ddlTipoActividad.SelectedValue, 0, xTipo, xPeriodoLaborable)
                Else
                    obj.EliminarCentroCostos(xCod, ddlTipoActividad.SelectedValue, ddlEsFacuDep.SelectedValue, xTipo, xPeriodoLaborable)
                End If


            End If
        Next

        If ddlEsFacuDep.Enabled = False Then
            CargaGridCentroCostosSeleccionados(ddlTipoActividad.SelectedValue, 0, lblEsFacuDep.Text.Trim, xPeriodoLaborable)
        Else
            CargaGridCentroCostosSeleccionados(ddlTipoActividad.SelectedValue, ddlEsFacuDep.SelectedValue, lblEsFacuDep.Text.Trim, xPeriodoLaborable)
        End If

    End Sub

    ' agregado 23/11/2011 debido al cambio de politicas 
    Protected Sub rdbDepartamento_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbDepartamento.CheckedChanged
        If Me.rdbDepartamento.Checked = True Then
            Call Me.CargarDepartamento()
            HabilitarEsFacuDepCargaAcademica(True)
        End If
    End Sub

    Protected Sub rdbFacultad_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbFacultad.CheckedChanged
        If Me.rdbFacultad.Checked = True Then
            Call Me.CargarFacultad()
            HabilitarEsFacuDepCargaAcademica(True)
        End If
    End Sub

    Protected Sub rdbEscuela_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbEscuela.CheckedChanged
        If Me.rdbEscuela.Checked = True Then
            Call CargarEscuela(0)
            HabilitarEsFacuDepCargaAcademica(True)
        End If
    End Sub

    Private Sub HabilitarEsFacuDepCargaAcademica(ByVal vEstado As Boolean)
        ddlEsFacuDep.Visible = vEstado
        ddlEsFacuDep.Enabled = vEstado
    End Sub

End Class

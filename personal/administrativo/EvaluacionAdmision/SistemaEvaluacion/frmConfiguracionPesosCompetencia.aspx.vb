Imports ClsGlobales
Imports ClsSistemaEvaluacion

Partial Class frmConfiguracionPesosCompetencia
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private oeCompetencia As e_CompetenciaAprendizaje, odCompetencia As d_CompetenciaAprendizaje
    Private oePesoCompetencia As e_PesoCompetencia, odPesoCompetencia As d_PesoCompetencia
    Private odGeneral As d_VacantesEvento
    Public cod_user As Integer = 684
    Private orden_col As Integer = 2

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                mt_CargarCompetencia()
                mt_CargarSemestreAcademico()
                mt_CargarProgramaEstudio()
                'Me.divProgramaEstudio.Visible = False
            Else
                mt_RefreshGrid()
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.cmbCicloAcademico.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione Semestre Academico !", MessageType.Warning) : Exit Sub
            'If Me.cmbFiltroCompetencia.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione Competencia !", MessageType.Warning) : Exit Sub
            mt_CrearColumnComp(CType(Session("adm_dtCompetencia"), Data.DataTable))
            mt_CargarPesos(Me.cmbCicloAcademico.SelectedValue)
            mt_CargarDatos()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub grvPesos_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim dt As Data.DataTable
        dt = CType(Session("adm_dtCompetencia"), Data.DataTable)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim objGridView As GridView = CType(sender, GridView)
            Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim objtablecell As TableCell = New TableCell()
            mt_AgregarCabecera(objgridviewrow, objtablecell, orden_col, "PROGRAMAS DE ESTUDIOS", "#FFFFFF", True)
            mt_AgregarCabecera(objgridviewrow, objtablecell, dt.Rows.Count, "COMPETENCIAS", "#FFFFFF", True)
            objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
        End If
    End Sub

    Protected Sub grvPesos_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim btn As New LinkButton()
        Dim codigo_cpf As Integer = -1, codigo_com As Integer = -1
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dt As Data.DataTable
            dt = CType(Session("adm_dtCompetencia"), Data.DataTable)
            codigo_cpf = Me.grvPesos.DataKeys(e.Row.RowIndex).Values("codigo_cpf")
            For i As Integer = 0 To dt.Rows.Count - 1
                codigo_com = dt.Rows(i).Item(0)
                e.Row.Cells(orden_col + i).Text = fc_ObtenerPeso(codigo_cpf, codigo_com)
            Next
            btn = New LinkButton()
            btn.ID = "btnConfigurar"
            btn.Text = "<i class='fa fa-cog'></i>"
            btn.CommandArgument = e.Row.RowIndex
            btn.CommandName = "Configurar"
            btn.CssClass = "btn btn-sm btn-accion btn-info"
            e.Row.Cells(orden_col + dt.Rows.Count).Controls.Add(btn)
        End If
    End Sub

    Protected Sub grvPesos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvPesos.RowCommand
        Dim index As Integer = -1, codigo_cpf As Integer = -1
        Dim nombre_cpf As String = String.Empty
        Dim dt As New Data.DataTable
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                codigo_cpf = Me.grvPesos.DataKeys(index).Values("codigo_cpf")
                nombre_cpf = Me.grvPesos.DataKeys(index).Values("nombre_cpf")
                Select Case e.CommandName
                    Case "Configurar"
                        Me.txtCicloAcademico.Text = Me.cmbCicloAcademico.SelectedItem.Text
                        Me.txtCarreraProfesional.Text = nombre_cpf
                        Me.chkFacultad.checked = True
                        oePesoCompetencia = New e_PesoCompetencia : odPesoCompetencia = New d_PesoCompetencia
                        With oePesoCompetencia
                            ._tipoOpe = "3" : ._codigo_cac = Me.cmbCicloAcademico.SelectedValue : ._codigo_cpf = codigo_cpf
                        End With
                        dt = odPesoCompetencia.fc_Listar(oePesoCompetencia)
                        Me.grvManPesos.DataSource = dt
                        Me.grvManPesos.DataBind()
                        mt_MostrarTabs(1)
                End Select
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mt_MostrarTabs(0)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim codigo_pcom As Integer = -1, codigo_cac As Integer = -1, codigo_cpf As Integer = -1, codigo_com As Integer = -1
        Dim peso As Double = 0, peso_ant As Double = 0
        Try
            codigo_cac = Me.cmbCicloAcademico.SelectedValue
            For i As Integer = 0 To Me.grvManPesos.Rows.Count - 1
                codigo_pcom = Me.grvManPesos.DataKeys(i).Values("codigo_pcom")
                codigo_cpf = Me.grvManPesos.DataKeys(i).Values("codigo_cpf")
                codigo_com = Me.grvManPesos.DataKeys(i).Values("codigo_com")
                peso_ant = Me.grvManPesos.DataKeys(i).Values("peso_pcom")
                Dim txtPeso As TextBox = CType(Me.grvManPesos.Rows(i).FindControl("txtPeso"), TextBox)
                peso = txtPeso.Text
                oePesoCompetencia = New e_PesoCompetencia : odPesoCompetencia = New d_PesoCompetencia
                If codigo_pcom = -1 Then
                    If peso > 0 Then
                        With oePesoCompetencia
                            ._codigo_cac = codigo_cac : ._codigo_cpf = codigo_cpf : ._codigo_com = codigo_com : ._peso_pcom = peso
                            : ._codigo_per = cod_user : ._aplicar_facultad = Me.chkFacultad.Checked
                        End With
                        odPesoCompetencia.fc_Insertar(oePesoCompetencia)
                    End If
                Else
                    oePesoCompetencia._codigo_pcom = codigo_pcom : oePesoCompetencia._codigo_per = cod_user
                    oePesoCompetencia._aplicar_facultad = Me.chkFacultad.Checked
                    If peso = 0 Then
                        odPesoCompetencia.fc_Eliminar(oePesoCompetencia)
                    Else
                        If peso_ant <> peso Then
                            oePesoCompetencia._peso_pcom = peso
                            oePesoCompetencia._aplicar_facultad = Me.chkFacultad.Checked
                            odPesoCompetencia.fc_Actualizar(oePesoCompetencia)
                        End If
                    End If
                End If
            Next
            mt_MostrarTabs(0)
            btnListar_Click(Nothing, Nothing)
            mt_ShowMessage("¡ Los Datos fueron actualizados exitosamente !", MessageType.Success)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAltImportar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As New Data.DataTable
        Try
            oePesoCompetencia = New e_PesoCompetencia : odPesoCompetencia = New d_PesoCompetencia
            With oePesoCompetencia
                ._codigo_cac = Me.cmbCicloAcademico.SelectedValue : ._codigo_cac_import = Me.cmbDesdeCicloAcademico.SelectedValue
                ._codigo_per = cod_user
            End With
            dt = odPesoCompetencia.fc_Importar(oePesoCompetencia)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0) <> -1 Then
                    btnListar_Click(Nothing, Nothing)
                    mt_ShowMessage("¡ Los Datos fueron importados exitosamente !", MessageType.Success)
                Else
                    mt_ShowMessage("¡ Ocurrió un error en el proceso, No hay datos para importar !", MessageType.Error)
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Dim dvAlerta As New Literal
        Dim cssclss As String
        Select Case type
            Case MessageType.Success
                cssclss = "alert-success"
            Case MessageType.Error
                cssclss = "alert-danger"
            Case MessageType.Warning
                cssclss = "alert-warning"
            Case Else
                cssclss = "alert-info"
        End Select
        dvAlerta.Text = "<div id='alert_div' style='margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;' class='alert " + cssclss + "'>"
        dvAlerta.Text += "  <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>"
        dvAlerta.Text += "  <span>" + Message + "</span>"
        dvAlerta.Text += "</div>"
        Me.divMensaje.Controls.Add(dvAlerta)
    End Sub

    Private Sub mt_MostrarTabs(ByVal idTab As Integer)
        Select Case idTab
            Case 0
                Me.navlistatab.Attributes("class") = "nav-item nav-link active"
                Me.navlistatab.Attributes("aria-selected") = "true"
                Me.navlista.Attributes("class") = "tab-pane fade show active"
                Me.navmantenimientotab.Attributes("class") = "nav-item nav-link"
                Me.navmantenimientotab.Attributes("aria-selected") = "false"
                Me.navmantenimiento.Attributes("class") = "tab-pane fade"
            Case 1
                Me.navlistatab.Attributes("class") = "nav-item nav-link"
                Me.navlistatab.Attributes("aria-selected") = "false"
                Me.navlista.Attributes("class") = "tab-pane fade"
                Me.navmantenimientotab.Attributes("class") = "nav-item nav-link active"
                Me.navmantenimientotab.Attributes("aria-selected") = "true"
                Me.navmantenimiento.Attributes("class") = "tab-pane fade show active"
        End Select
    End Sub

    Private Sub mt_CargarSemestreAcademico()
        odGeneral = New d_VacantesEvento
        mt_LlenarListas(Me.cmbCicloAcademico, odGeneral.fc_ListarCicloAcademico, "codigo_cac", "descripcion_cac", "-- SELECCIONE --")
        mt_LlenarListas(Me.cmbDesdeCicloAcademico, odGeneral.fc_ListarCicloAcademico, "codigo_cac", "descripcion_cac", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarProgramaEstudio()
        odGeneral = New d_VacantesEvento
        'mt_LlenarListas(Me.cmbCarreraProfesional, odGeneral.fc_ListarCarreraProfesional, "codigo_cpf", "nombre_cpf", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarCompetencia()
        oeCompetencia = New e_CompetenciaAprendizaje : odCompetencia = New d_CompetenciaAprendizaje
        Session("adm_dtCompetencia") = odCompetencia.fc_Listar(oeCompetencia)
    End Sub

    Private Sub mt_CrearColumnComp(ByVal dtColumn As Data.DataTable)
        Dim tfield As New TemplateField()
        If Me.grvPesos.Columns.Count > orden_col Then
            'mt_ShowMessage("mt_CrearColumns: " & dtColumns.Rows.Count & " | " & Me.gvAsignatura.Columns.Count, MessageType.Warning)
            For i As Integer = (orden_col + 1) To Me.grvPesos.Columns.Count
                Me.grvPesos.Columns.RemoveAt(Me.grvPesos.Columns.Count - 1)
            Next
        End If
        If dtColumn.Rows.Count = 0 Then
            tfield = New TemplateField()
            tfield.HeaderText = ""
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            Me.grvPesos.Columns.Add(tfield)
        Else
            For x As Integer = 0 To dtColumn.Rows.Count - 1
                tfield = New TemplateField()
                tfield.HeaderText = dtColumn.Rows(x).Item("nombre_corto_com").ToString.ToUpper
                tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                Me.grvPesos.Columns.Add(tfield)
            Next
        End If
        ' Crear Column para operaciones
        tfield = New TemplateField()
        tfield.HeaderText = "OPERACIONES"
        tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
        Me.grvPesos.Columns.Add(tfield)
    End Sub

    Private Sub mt_CargarPesos(ByVal codigo_cac As Integer)
        oePesoCompetencia = New e_PesoCompetencia : odPesoCompetencia = New d_PesoCompetencia
        oePesoCompetencia._tipoOpe = "2" : oePesoCompetencia._codigo_cac = codigo_cac
        Session("adm_dtPesos") = odPesoCompetencia.fc_Listar(oePesoCompetencia)
    End Sub

    Private Sub mt_CargarDatos()
        Dim dt As New Data.DataTable
        oePesoCompetencia = New e_PesoCompetencia : odPesoCompetencia = New d_PesoCompetencia
        oePesoCompetencia._tipoOpe = "1"
        dt = odPesoCompetencia.fc_Listar(oePesoCompetencia)
        Me.grvPesos.DataSource = dt
        Me.grvPesos.DataBind()
        If Me.grvPesos.Rows.Count > 0 Then mt_AgruparFilas(Me.grvPesos.Rows, 0, 2)
    End Sub

    Private Sub mt_RefreshGrid()
        For Each _Row As GridViewRow In Me.grvPesos.Rows
            grvPesos_OnRowDataBound(Me.grvPesos, New GridViewRowEventArgs(_Row))
        Next
    End Sub

#End Region

#Region "Funciones"

    Private Function fc_ObtenerPeso(ByVal codigo_cpf As Integer, ByVal codigo_com As Integer) As Double
        Dim _return As Double = 0
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("adm_dtPesos"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(dt, "codigo_cpf = " & codigo_cpf & " AND codigo_com = " & codigo_com, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                _return = dt.Rows(0).Item(4)
            End If
        End If
        Return _return
    End Function

#End Region


End Class

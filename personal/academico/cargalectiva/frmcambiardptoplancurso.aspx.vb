
Partial Class librerianet_cargaacademica_frmcambiardptoplancurso
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Me.grwPlanEscuela.DataSource = obj.TraerDataSet("ConsultarCursoPlan", 2, Request.QueryString("codigo_cur"), 0, 0)
            Me.grwPlanEscuela.DataBind()
            obj = Nothing
            Me.lblmensaje.Text = ""
        End If
    End Sub

    Protected Sub grwPlanEscuela_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwPlanEscuela.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            Dim combo As DropDownList

            combo = CType(e.Row.FindControl("dpcodigo_dac"), DropDownList)
            combo.ClearSelection()
            combo.Items.Clear()
            If combo IsNot DBNull.Value Then
                Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                combo.DataSource = obj.TraerDataTable("ConsultarDepartamentoAcademico", "AL", 0)
                combo.DataBind()
                obj = Nothing
                combo.SelectedValue = fila.Row("codigopcu_dac")
            End If
        End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim I As Integer
        Dim Fila As GridViewRow
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Me.lblmensaje.Text = ""
        Try
            'obj.IniciarTransaccion()
            For I = 0 To Me.grwPlanEscuela.Rows.Count - 1
                Fila = Me.grwPlanEscuela.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    '==================================
                    ' Guardar los datos
                    '==================================
                    obj.Ejecutar("ModificarDptoAcadPlanCurso", Request.QueryString("codigo_cur"), Me.grwPlanEscuela.DataKeys.Item(Fila.RowIndex).Values("codigo_pes"), CType(Fila.FindControl("dpcodigo_dac"), DropDownList).SelectedValue)
                End If
            Next
            'Cargar denuevo
            Me.grwPlanEscuela.DataBind()
            Me.grwPlanEscuela.DataSource = obj.TraerDataSet("ConsultarCursoPlan", 2, Request.QueryString("codigo_cur"), 0, 0)
            Me.grwPlanEscuela.DataBind()
            Me.lblmensaje.Text = "Se guardó correctamente"

        Catch ex As Exception
            Me.cmdGuardar.Visible = False
            Me.lblmensaje.Text = "Ocurrió un Error al Registrar el estado. Intente mas tarde." & Chr(13) & ex.Message
        End Try
        obj = Nothing
    End Sub
End Class

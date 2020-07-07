Imports System.Data

Partial Class frmConsultaEstadoCuenta
    Inherits System.Web.UI.Page

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If Not IsPostBack Then
            'hfCodigoAlu.Value = Session("codigo_alu")
            CargarDatosAlumno()
            CargarDeuda()
        End If
    End Sub

    Private Sub CargarDatosAlumno()
        Dim dtDatos As New DataTable
        Dim objAlumno As New ClsOdontologia
        dtDatos = objAlumno.ConsultarAlumnoGeneral(hfCodigoAlu.Value)
        If dtDatos.Rows.Count > 0 Then
            lblCodigo.Text = dtDatos.Rows(0).Item("codigoUniver_Alu")
            lblAlumno.Text = dtDatos.Rows(0).Item("alumno")
            lblCarrera.Text = dtDatos.Rows(0).Item("nombre_Cpf")
            lblFacultad.Text = dtDatos.Rows(0).Item("nombre_Fac")
        End If
    End Sub

    Private Sub CargarDeuda()
        Dim objPedido As New ClsOdontologia
        gvPedido.DataSource = objPedido.ConsultarDeudaAlumno(hfCodigoAlu.Value, 668)
        gvPedido.DataBind()
    End Sub

    Protected Sub gvPedido_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPedido.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvPedido','Select$" & e.Row.RowIndex & "');")
                e.Row.Style.Add("cursor", "hand")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvPedido_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPedido.SelectedIndexChanged
        'hfCodPedido.Value = gvPedido.SelectedRow.Cells(0).Text

    End Sub

    Protected Sub gvPedido_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvPedido.PageIndexChanging
        gvPedido.PageIndex = e.NewPageIndex
        CargarDeuda()
    End Sub
End Class

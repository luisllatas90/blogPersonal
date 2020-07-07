
Partial Class proponente_revisores
    Inherits System.Web.UI.Page

    Protected Sub dgvConsejoFacultad_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvConsejoFacultad.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            Select Case e.Row.Cells(2).Text.ToUpper
                Case "CONFORME"
                    e.Row.Cells(2).Text = "<img src='../images/conforme_small.gif'>"
                Case "PENDIENTE"
                    e.Row.Cells(2).Text = "<img src='../images/menu3.gif'>"
                Case "NO CONFORME"
                    e.Row.Cells(2).Text = "<img src='../images/noconforme_small.gif'>"
                Case "OBSERVADO"
                    e.Row.Cells(2).Text = "<img src='../images/editar_1_s.gif'>"
            End Select
        End If
    End Sub


    Protected Sub dgvRevisoresFacultad_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvRevisoresFacultad.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            Select Case e.Row.Cells(2).Text.ToUpper
                Case "CONFORME"
                    e.Row.Cells(2).Text = "<img src='../images/conforme_small.gif'>"
                Case "PENDIENTE"
                    e.Row.Cells(2).Text = "<img src='../images/menu3.gif'>"
                Case "NO CONFORME"
                    e.Row.Cells(2).Text = "<img src='../images/noconforme_small.gif'>"
                Case "OBSERVADO"
                    e.Row.Cells(2).Text = "<img src='../images/editar_1_s.gif'>"
            End Select
        End If
    End Sub

    Protected Sub dgvConsejoFacultad0_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvConsejoFacultad0.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            Select Case e.Row.Cells(2).Text.ToUpper
                Case "CONFORME"
                    e.Row.Cells(2).Text = "<img src='../images/conforme_small.gif'>"
                Case "PENDIENTE"
                    e.Row.Cells(2).Text = "<img src='../images/menu3.gif'>"
                Case "NO CONFORME"
                    e.Row.Cells(2).Text = "<img src='../images/noconforme_small.gif'>"
                Case "OBSERVADO"
                    e.Row.Cells(2).Text = "<img src='../images/editar_1_s.gif'>"
            End Select
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub dgvRevisoresFacultad0_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvRevisoresFacultad0.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            Select Case e.Row.Cells(2).Text.ToUpper
                Case "CONFORME"
                    e.Row.Cells(2).Text = "<img src='../images/conforme_small.gif'>"
                Case "PENDIENTE"
                    e.Row.Cells(2).Text = "<img src='../images/menu3.gif'>"
                Case "NO CONFORME"
                    e.Row.Cells(2).Text = "<img src='../images/noconforme_small.gif'>"
                Case "OBSERVADO"
                    e.Row.Cells(2).Text = "<img src='../images/editar_1_s.gif'>"
            End Select
        End If
    End Sub

    Protected Sub dgvDirectores_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvDirectores.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            Select Case e.Row.Cells(2).Text.ToUpper
                Case "CONFORME"
                    e.Row.Cells(2).Text = "<img src='../images/conforme_small.gif'>"
                Case "PENDIENTE"
                    e.Row.Cells(2).Text = "<img src='../images/menu3.gif'>"
                Case "NO CONFORME"
                    e.Row.Cells(2).Text = "<img src='../images/noconforme_small.gif'>"
                Case "OBSERVADO"
                    e.Row.Cells(2).Text = "<img src='../images/editar_1_s.gif'>"
            End Select
        End If
    End Sub


    Protected Sub dgvDirectores_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDirectores.SelectedIndexChanged

    End Sub

    Protected Sub dgvRevisoresFacultad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvRevisoresFacultad.SelectedIndexChanged

    End Sub

    Protected Sub dgvRevisoresPostGrado_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvRevisoresPostGrado.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            Select Case e.Row.Cells(2).Text.ToUpper
                Case "CONFORME"
                    e.Row.Cells(2).Text = "<img src='../images/conforme_small.gif'>"
                Case "PENDIENTE"
                    e.Row.Cells(2).Text = "<img src='../images/menu3.gif'>"
                Case "NO CONFORME"
                    e.Row.Cells(2).Text = "<img src='../images/noconforme_small.gif'>"
                Case "OBSERVADO"
                    e.Row.Cells(2).Text = "<img src='../images/editar_1_s.gif'>"
            End Select
        End If
    End Sub

    Protected Sub dgvRevisoresProfesionalizacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvRevisoresProfesionalizacion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            Select Case e.Row.Cells(2).Text.ToUpper
                Case "CONFORME"
                    e.Row.Cells(2).Text = "<img src='../images/conforme_small.gif'>"
                Case "PENDIENTE"
                    e.Row.Cells(2).Text = "<img src='../images/menu3.gif'>"
                Case "NO CONFORME"
                    e.Row.Cells(2).Text = "<img src='../images/noconforme_small.gif'>"
                Case "OBSERVADO"
                    e.Row.Cells(2).Text = "<img src='../images/editar_1_s.gif'>"
            End Select
        End If
    End Sub


    Protected Sub dgvRevisoresProfesionalizacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvRevisoresProfesionalizacion.SelectedIndexChanged

    End Sub
End Class

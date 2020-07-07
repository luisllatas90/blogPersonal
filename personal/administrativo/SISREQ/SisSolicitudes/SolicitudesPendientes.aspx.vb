﻿
Partial Class SisSolicitudes_SolicitudesPendientes
    Inherits System.Web.UI.Page

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim DatSolicitudes As New Data.DataTable
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        DatSolicitudes = Obj.TraerDataTable("SOL_ConsultarListaSolicitudes", Me.CboSeleccionar.SelectedValue, Me.TxtBuscar.Text)
        If DatSolicitudes.Rows.Count > 0 Then
            Me.GvSolicitudes.DataSource = DatSolicitudes
            Me.GvSolicitudes.SelectedIndex = -1
            ActivarControles(False)
        End If
        Me.GvSolicitudes.DataBind()
    End Sub

    Protected Sub CboSeleccionar_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboSeleccionar.SelectedIndexChanged
        If Me.CboSeleccionar.SelectedValue = 1 Then
            Me.TxtBuscar.MaxLength = 10
        Else
            Me.TxtBuscar.MaxLength = 50
        End If
    End Sub

    Protected Sub GvSolicitudes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvSolicitudes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem

            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            'e.Row.Attributes.Add("ToolTip", "Dar clic en la figura para seleccionar")

            e.Row.Style.Add("cursor", "hand")
            Select Case Fila.Item("Estado").ToString.Trim
                Case "P"
                    e.Row.Cells(9).Text = "Pendiente"
                    e.Row.Cells(9).ForeColor = Drawing.Color.Red
                Case "T"
                    e.Row.Cells(9).Text = "Finalizada"
                    e.Row.Cells(9).ForeColor = Drawing.Color.Green
                Case "A"
                    e.Row.Cells(9).Text = "Anulada"
                    e.Row.Cells(9).ForeColor = Drawing.Color.Red
                    e.Row.Cells(9).Font.Strikeout = True
            End Select
        End If
    End Sub

    Protected Sub GvSolicitudes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvSolicitudes.SelectedIndexChanged
        Dim Ruta As New EncriptaCodigos.clsEncripta
        Me.HddAlumno.Value = Me.GvSolicitudes.DataKeys.Item(Me.GvSolicitudes.SelectedIndex).Values(1).ToString
        Me.LblEstado.Text = Me.GvSolicitudes.DataKeys.Item(Me.GvSolicitudes.SelectedIndex).Values(2).ToString

        Select Case Me.LblEstado.Text
            Case "P"
                Me.LblEstado.ForeColor = Drawing.Color.Red
                Me.LblEstado.Font.Strikeout = False
                Me.LblEstado.Text = "Pendiente"
            Case "T"
                Me.LblEstado.ForeColor = Drawing.Color.Green
                Me.LblEstado.Font.Strikeout = False
                Me.LblEstado.Text = "Finalizada"
            Case "A"
                Me.LblEstado.ForeColor = Drawing.Color.Red
                Me.LblEstado.Font.Strikeout = True
                Me.LblEstado.Text = "Anulada"
        End Select

        Me.ImgFoto.Visible = True
        '---------------------------------------------------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: dguevara
        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
        '---------------------------------------------------------------------------------------------------------------
        Me.ImgFoto.ImageUrl = "//intranet.usat.edu.pe/imgestudiantes/" & Ruta.CodificaWeb("069" & Me.HddAlumno.Value)
        ActivarControles(True)
        'Response.Write(Me.GvSolicitudes.SelectedDataKey.Value)
    End Sub

    Protected Sub ActivarControles(ByVal valor As Boolean)
        If valor = True Then
            ClientScript.RegisterStartupScript(Me.GetType, "Habilitar1", "tblDatos.style.visibility='visible';", True)
        Else
            Page.RegisterStartupScript("Habilitar1", "<script>tblDatos.style.visibility='hidden';</script>")
        End If
    End Sub

    Protected Sub GvEvaluacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvEvaluacion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem
            Select Case Fila.Item("codigo_res").ToString.Trim  'Verifica si las instancias ya resolvieron la solicitud
                Case "0"
                    'Verifica si es Julia quien evalua no queda como pendiente por ella tiene la funcion de visualizacion mas no de calificar
                    If Fila.Item("codigo_per") = 473 Then
                        e.Row.Cells(2).ForeColor = Drawing.Color.Green
                        e.Row.Cells(2).Text = "-"
                    Else
                        e.Row.Cells(2).ForeColor = Drawing.Color.Red
                        e.Row.Cells(2).Text = "Pendiente"
                    End If
                Case "1"
                    e.Row.Cells(2).ForeColor = Drawing.Color.Green
                    e.Row.Cells(2).Text = "Ya dió respuesta"
                    If e.Row.Cells(3).Text.Trim = "APROBADO" Then
                        e.Row.Cells(3).ForeColor = Drawing.Color.Blue
                    Else
                        e.Row.Cells(3).ForeColor = Drawing.Color.Red
                    End If
                Case "2"
                    e.Row.Cells(2).ForeColor = Drawing.Color.Green
                    e.Row.Cells(2).Text = "Ya dió respuesta"
                    e.Row.Cells(3).ForeColor = Drawing.Color.Red
            End Select
        End If
    End Sub

    Protected Sub DvEstudiante_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DvEstudiante.DataBound
        If DvEstudiante.Rows.Count > 0 Then
            If DvEstudiante.Rows(5).Cells(1).Text = "Activo" Then
                DvEstudiante.Rows(5).Cells(1).ForeColor = Drawing.Color.Blue
            Else
                DvEstudiante.Rows(5).Cells(1).ForeColor = Drawing.Color.Red
            End If

            If DvEstudiante.Rows(7).Cells(1).Text = "Si" Then
                DvEstudiante.Rows(7).Cells(1).ForeColor = Drawing.Color.Red
            Else
                DvEstudiante.Rows(7).Cells(1).ForeColor = Drawing.Color.Blue
            End If

            DvEstudiante.Rows(6).Cells(1).ForeColor = Drawing.Color.Blue
        End If
    End Sub
End Class

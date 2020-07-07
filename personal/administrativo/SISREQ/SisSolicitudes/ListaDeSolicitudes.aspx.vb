﻿
Partial Class SisSolicitudes_ListaDeSolicitudes
    Inherits System.Web.UI.Page
    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Try
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            If cboCicloAcad.SelectedValue = -1 Then
                If Me.CboVer.SelectedValue = "0" Then
                    Me.GvSolicitudes.DataSource = Obj.TraerDataTable("SOL_ConsultarListaSolicitudes", 4, "")
                Else
                    Me.GvSolicitudes.DataSource = Obj.TraerDataTable("SOL_ConsultarListaSolicitudes", 5, Me.CboVer.SelectedValue)
                End If
            ElseIf CboVer.SelectedValue = "0" Then
                Me.GvSolicitudes.DataSource = Obj.TraerDataTable("SOL_ConsultarSolicitudesPorCiclo", 1, "", cboCicloAcad.SelectedValue)
            Else
                Me.GvSolicitudes.DataSource = Obj.TraerDataTable("SOL_ConsultarSolicitudesPorCiclo", 2, CboVer.SelectedValue, cboCicloAcad.SelectedValue)
            End If

            Me.GvSolicitudes.DataBind()
            Me.LblTotal.Text = Me.GvSolicitudes.Rows.Count.ToString & " registros"
            Me.LblTotal.ForeColor = Drawing.Color.Blue
            ActivarControles("hidden")
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "ErrVer", "alert('Ocurrió un error al consultar los datos')", True)
        End Try
    End Sub

    Protected Sub GvSolicitudes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvSolicitudes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem

            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            'e.Row.Attributes.Add("ToolTip", "Dar clic en la figura para seleccionar")

            e.Row.Style.Add("cursor", "hand")
            Select Case Fila.Item("Estado")
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
        Me.HddAlumno.Value = Me.GvSolicitudes.DataKeys.Item(Me.GvSolicitudes.SelectedIndex).Values(1).ToString
        Dim Ruta As New EncriptaCodigos.clsEncripta
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
        Me.ImgFoto.Dispose()
        Me.ImgFoto.Visible = True
        Me.ImgFoto.ImageUrl = ""
        '---------------------------------------------------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: dguevara
        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
        '---------------------------------------------------------------------------------------------------------------
        Me.ImgFoto.ImageUrl = "//intranet.usat.edu.pe/imgestudiantes/" & Ruta.CodificaWeb("069" & Me.HddAlumno.Value)
        Me.ImgFoto.DataBind()

        ActivarControles("visible")

    End Sub


    Protected Sub ActivarControles(ByVal valor As String)
        Page.RegisterStartupScript("Solicitud", "<script>tblDatos.style.visibility ='" & valor & "'</script>")
    End Sub

    Protected Sub GvEvaluacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvEvaluacion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem
            If CInt(Fila.Item("codigo_res")) = 0 Then 'Verifica si las instancias ya resolvieron la solicitud
                'Verifica si es Julia quien evalua no queda como pendiente por ella tiene la funcion de visualizacion mas no de calificar
                If Fila.Item("codigo_per") = 473 Then '-->Codigo per de Julia Danjanovic
                    e.Row.Cells(2).ForeColor = Drawing.Color.Green
                    e.Row.Cells(2).Text = "-"
                Else
                    e.Row.Cells(2).ForeColor = Drawing.Color.Red
                    e.Row.Cells(2).Text = "Pendiente"
                End If
            Else
                e.Row.Cells(2).ForeColor = Drawing.Color.Green
                e.Row.Cells(2).Text = "Ya dió respuesta"
                If e.Row.Cells(3).Text = "APROBADO" Then
                    e.Row.Cells(3).ForeColor = Drawing.Color.Blue
                Else
                    e.Row.Cells(3).ForeColor = Drawing.Color.Red
                End If
            End If
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                ClsFunciones.LlenarListas(cboCicloAcad, Obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac", ">>Selecione<<")
                ActivarControles("hidden")
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "ErrLoad", "alert('Ocurrió un error al consultar los datos')", True)
        End Try
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

            DvEstudiante.Rows(6).Cells(1).ForeColor = Drawing.Color.Red
        End If
    End Sub


End Class

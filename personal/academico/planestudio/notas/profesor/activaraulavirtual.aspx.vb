﻿
Partial Class academico_notas_profesor_activaraulavirtual
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ConsultarCicloAcademico()
            ConsultarCargaAcademica()
            LlenarSecciones()
        End If
    End Sub

    Public Sub ConsultarCicloAcademico()
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("Moodle_ListarCiclos")
            obj.CerrarConexion()
            Me.ddlCiclo.DataTextField = "descripcion_cac"
            Me.ddlCiclo.DataValueField = "codigo_cac"
            Me.ddlCiclo.DataSource = dt
            Me.ddlCiclo.SelectedIndex = 0
            Me.ddlCiclo.DataBind()

        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar el ciclo"
        End Try
    End Sub
    Sub LlenarSecciones()
        For i As Integer = 1 To 52
            Me.ddlSeccion.Items.Add(i.ToString)
        Next
        Me.ddlSeccion.DataBind()
        Me.ddlSeccion.SelectedValue = "17"
    End Sub
    Public Sub ConsultarCargaAcademica()
        Me.celdaGrid.Visible = False
        Me.celdaGrid.InnerHtml = ""
        Try
            If Me.ddlCiclo.SelectedValue <> "0" Then
                Dim dt As New Data.DataTable
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dt = obj.TraerDataTable("Moodle_ConsultarCargaAcademica", Me.ddlCiclo.SelectedValue, CInt(Request.QueryString("id").ToString))
                obj.CerrarConexion()
                If dt.Rows.Count Then
                    Me.gvCarga.DataSource = dt
                    Me.gvCarga.DataBind()
                Else
                    Me.celdaGrid.Visible = True
                    Me.celdaGrid.InnerHtml = "No existe carga académica para el ciclo seleccionado"
                End If

            Else
                Me.gvCarga.DataSource = Nothing
                Me.gvCarga.DataBind()
                Me.celdaGrid.Visible = True
                Me.celdaGrid.InnerHtml = "No existe carga académica para el ciclo seleccionado"
            End If

        Catch ex As Exception
            Me.lblMensaje.Text = ex.Message & " - " & ex.StackTrace '"Error al consultar la carga académica"
        End Try
    End Sub


    Protected Sub cboCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCiclo.SelectedIndexChanged

        ConsultarCargaAcademica()
        Me.lblMensaje.Text = ""
    End Sub

    

    Sub activarCurso(ByVal codigo_cup As Integer, ByVal format As String, ByVal nrosecciones As String)

        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("Moodle_CrearCursoV26", codigo_cup, format, nrosecciones)
            obj.CerrarConexion()
            obj = Nothing
            If dt.Rows.Count Then
                If dt.Rows(0).Item("idmoodle") > 0 Then
                    Me.lblMensaje.Text = "Se ha creado correctamente los cursos seleccionados"
                Else
                    Me.lblMensaje.Text = "Ha ocurrido un error, por favor vuelva a intentarlo"
                End If
            End If
            dt = Nothing

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub
    Sub actualizarLista(ByVal codigo_cup As Integer)
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("[Moodle_ActualizarLista]", codigo_cup)
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

    Protected Sub gvCarga_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCarga.RowDataBound

        If e.Row.RowIndex <> -1 Then
            If e.Row.Cells(1).Text <> "0" Then
                e.Row.Cells(1).Text = "ACTIVADO"
                e.Row.ForeColor = Drawing.Color.DarkBlue
                e.Row.Font.Bold = True
                e.Row.Cells(7).Text = "-"
            Else
                e.Row.Cells(1).Text = "PENDIENTE"
                e.Row.ForeColor = Drawing.Color.Green
                e.Row.Font.Bold = True
            End If
            Dim url As String = "../../../../../silabos/"
            If e.Row.Cells(9).Text <> "No disponible" Then
                e.Row.Cells(9).Text = "<a href=""" & url & e.Row.Cells(9).Text & """>Descargar</a>"
            End If
        End If
    End Sub

    Protected Sub btnActivarAula0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActivarAula0.Click
        Try
            For i As Integer = 0 To Me.gvCarga.Rows.Count - 1
                actualizarLista(Me.gvCarga.DataKeys(i).Values("codigo_cup").ToString)
            Next

            ConsultarCargaAcademica()
            Me.lblMensaje0.Text = "Actualización Finalizada."
        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

    Protected Sub gvCarga_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCarga.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "cmdActivar") Then
            activarCurso(gvCarga.DataKeys(index).Values("codigo_cup"), Me.ddlFormato.SelectedValue.ToString, Me.ddlSeccion.SelectedItem.Text)
            ConsultarCargaAcademica()

        End If
        If (e.CommandName = "cmdActualizar") Then
            actualizarLista(gvCarga.DataKeys(index).Values("codigo_cup"))
            ConsultarCargaAcademica()
            Me.lblMensaje0.Text = "Actualización Finalizada."
        End If

    End Sub

   
End Class

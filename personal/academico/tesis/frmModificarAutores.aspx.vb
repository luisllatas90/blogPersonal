﻿Imports System.IO
Partial Class frmModificarAutores
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("id_per") = "" Or Request.QueryString("id") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        Try
            If IsPostBack = False Then
                Me.CargaCicloAcademico()
                Dim tblescuela As New Data.DataTable
                Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
                If Request.QueryString("ctf") = 1 Then
                    tblescuela = obj.TraerDataTable("ConsultarCarreraProfesional", "MA", 0)
                Else
                    tblescuela = obj.TraerDataTable("consultaracceso", "ESC", "", Request.QueryString("id"))
                End If
                ClsFunciones.LlenarListas(Me.dpEscuela, tblescuela, "codigo_cpf", "nombre_cpf", "--Seleccione la Escuela--")
                If Request.QueryString("cpf") > 0 Then
                    Me.dpEscuela.SelectedValue = Request.QueryString("cpf")
                End If
                obj = Nothing
                CargarAutores()
                FiltradoCarreraCiclo()
                Me.lblTitulo.Text = Request.QueryString("titulo")
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
      

    End Sub

    Private Sub CargaCicloAcademico()
        Try
            Dim obj As New ClsConectarDatos
            Dim tblciclo As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tblciclo = obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0)
            If tblciclo.Rows.Count > 0 Then
                ClsFunciones.LlenarListas(Me.ddlCiclo, tblciclo, "codigo_cac", "descripcion_cac", "--Todos--")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarAutores()
        Try
            If Request.QueryString("codigo_tes") > 0 Then
                Dim dt As New Data.DataTable
                Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
                dt = obj.TraerDataTable("TES_ListarAutor", Request.QueryString("codigo_tes"))
                If dt.Rows.Count > 0 Then
                    Me.grwAutor.DataSource = dt
                    Me.grwAutor.DataBind()
                    Dim itemToRemove As New ListItem
                    For i As Integer = 0 To grwAutor.Rows.Count - 1
                        itemToRemove = cboAutor.Items.FindByValue(grwAutor.DataKeys(i).Values("codigo_alu"))
                        If itemToRemove IsNot Nothing Then
                            cboAutor.Items.Remove(itemToRemove)
                        End If
                    Next
                Else
                    Me.grwAutor.DataSource = ""
                    Me.grwAutor.DataBind()
                End If


                obj = Nothing
            Else
                Me.grwAutor.DataSource = ""
                Me.grwAutor.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub


    Private Sub FiltradoCarreraCiclo()
        Me.cboAutor.Items.Clear()
        If Me.dpEscuela.SelectedValue <> -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim tblAlumnos As New Data.DataTable
            tblAlumnos = obj.TraerDataTable("TES_ConsultarResponsableTesis_v2", 0, Me.dpEscuela.SelectedValue, 0, 0, ddlCiclo.SelectedValue)

            If tblAlumnos.Rows.Count > 0 Then
                ClsFunciones.LlenarListas(Me.cboAutor, tblAlumnos, "codigo_alu", "alumno", "--Seleccione alumno--")
                Me.cboAutor.Enabled = True
                If grwAutor.Rows.Count > 0 Then
                    Dim itemToRemove As New ListItem
                    For i As Integer = 0 To grwAutor.Rows.Count - 1
                        itemToRemove = cboAutor.Items.FindByValue(grwAutor.DataKeys(i).Values("codigo_alu"))
                        If itemToRemove IsNot Nothing Then
                            cboAutor.Items.Remove(itemToRemove)
                        End If
                    Next
                End If

            Else
                Me.cboAutor.Items.Clear()
                Me.cboAutor.Items.Add(New ListItem("--seleccione alumno--", "-1"))
                Me.cboAutor.Enabled = False
            End If
            obj.CerrarConexion()
        End If
    End Sub

    Protected Sub ddlCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCiclo.SelectedIndexChanged
        Try
            If dpEscuela.SelectedValue <> -1 Then
                Call FiltradoCarreraCiclo()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub dpEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpEscuela.SelectedIndexChanged
        Call FiltradoCarreraCiclo()
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        GuardarDatos()
    End Sub

    Public Sub GuardarDatos()
        If Session("id_per") = "" Or Request.QueryString("id") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Dim id As Int16 = Request.QueryString("id")
            Dim dt As New Data.DataTable
            dt = obj.TraerDataTable("TES_AgregarAutorTesis", Request.QueryString("codigo_tes"), Me.cboAutor.SelectedValue, 3, Me.txtMotivo.Text, id)
            obj = Nothing
            If dt.Rows(0).Item("Respuesta").ToString = "1" Then
                CargarAutores()
                Me.txtMotivo.Text = ""
                Me.cboAutor.SelectedValue = -1
            End If
            Page.RegisterStartupScript("error", "<script>alert('" + dt.Rows(0).Item("Mensaje").ToString + "')</script>")
        Catch ex As Exception
            Page.RegisterStartupScript("error", "<script>alert('Error: " & ex.Message.ToString & "')</script>")
        End Try
    End Sub


    Protected Sub grwAutor_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwAutor.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(4).Attributes.Add("onclick", "return confirm('¿Esta seguro que desea quitar al autor de la tesis?');")
        End If
    End Sub

    Private Function EliminarResponsable(ByVal codigo_RTes As Int32) As Data.DataTable
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("TES_EliminarAutorTesis", codigo_RTes, "", Request.QueryString("id"))
        obj = Nothing
        Return dt
    End Function

    Protected Sub grwAutor_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwAutor.RowDeleting
        If Session("id_per") = "" Or Request.QueryString("id") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If grwAutor.DataKeys(e.RowIndex).Value <> 0 Then
            Dim dt As New Data.DataTable
            dt = EliminarResponsable(grwAutor.DataKeys(e.RowIndex).Values("codigo_RTes"))
            Page.RegisterStartupScript("error", "<script>alert('" + dt.Rows(0).Item("Mensaje").ToString + "')</script>")
            CargarAutores()
            FiltradoCarreraCiclo()
        End If
        e.Cancel = True
    End Sub
 
End Class
﻿
Partial Class academico_matricula_administrar_frmReactivarMatricula
    Inherits System.Web.UI.Page

    Private Sub EnviarAPagina(ByVal pagina As String)        
        Me.fradetalle.Attributes("src") = pagina
    End Sub

    Private Function ReactivaMatricula(ByVal codMatricula As Integer) As Boolean
        Try
            Dim obj As New ClsConectarDatos            
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("MAT_ReactivaMatricula", Me.HdCicloActual.Value, gvAlumnos.SelectedRow.Cells(0).Text, codMatricula, Me.HdCodigoPer.Value)
            obj.CerrarConexion()
            obj = Nothing
            Return True
        Catch ex As Exception
            Me.lblAviso.Text = "Error al reactivar matrícula."
            Return False
        End Try
    End Function

    Private Function RetornaMatricula(ByVal codAlumno As Integer) As String
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("MAT_BuscaMatriculaAlumno", Me.HdCicloActual.Value, codAlumno)
            obj.CerrarConexion()
            obj = Nothing

            If (dt.Rows.Count > 0) Then
                Return dt.Rows(0).Item("codigo_Mat").ToString()
            Else
                Return "0"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            Return "-1"
        End Try
    End Function

    Private Sub CargaGrid()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("MAT_BuscaAlumno", Me.ddpTipo.SelectedValue, Me.txtBuscar.Text)
            obj.CerrarConexion()
            obj = Nothing

            Me.gvAlumnos.DataSource = dt
            Me.gvAlumnos.DataBind()
            dt.Dispose()
        Catch ex As Exception
            Me.lblAviso.Text = ex.Message
            '"Error al buscar alumno."
        End Try        
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        If (Me.txtBuscar.Text.Trim <> "") Then
            Me.lblAviso.Text = ""
            Me.lblMatricula.Text = ""
            CargaGrid()
        Else
            Me.lblAviso.Text = "Por favor ingrese " & Me.ddpTipo.SelectedItem.Text
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If (Request.QueryString("id") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        Else
            Me.HdCodigoPer.Value = Session("id_per")
        End If
        If (IsPostBack = False) Then
            Me.gvAlumnos.DataBind()

            'Capturamos el ciclo actual
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("LOG_CicloAcademicoActual")
            obj.CerrarConexion()
            obj = Nothing

            If dt.Rows.Count > 0 Then
                Me.HdCicloActual.Value = dt.Rows(0).Item("codigo_cac").ToString
            End If
        End If

        If (Me.gvAlumnos.Rows.Count > 0) Then            
            Me.tbDetalle.Visible = True
        Else
            Me.tbDetalle.Visible = False
        End If
    End Sub

    Protected Sub gvAlumnos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAlumnos.PageIndexChanging
        gvAlumnos.PageIndex = e.NewPageIndex()        
        CargaGrid()
    End Sub

    Protected Sub lnkDatos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDatos.Click
        EnviarAPagina("../../estudiante/misdatos.asp?codigouniver_alu=" & gvAlumnos.SelectedRow.Cells(1).Text & "&codigo_alu=" & gvAlumnos.SelectedRow.Cells(0).Text)
    End Sub

    Protected Sub lnkEstadoCuenta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEstadoCuenta.Click
        EnviarAPagina("adminestadocuenta.aspx?id=" & gvAlumnos.SelectedRow.Cells(0).Text)
    End Sub

    Protected Sub lnkMovimientos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMovimientos.Click
        If (Me.HdCicloActual.Value <> "") Then
            Dim strAlumno As String
            Dim strEscuela As String
            Dim strMatricula As String
            strAlumno = gvAlumnos.SelectedRow.Cells(2).Text
            strEscuela = gvAlumnos.SelectedRow.Cells(3).Text
            'strMatricula = gvAlumnos.SelectedRow.Cells(6).Text()
            strMatricula = RetornaMatricula(gvAlumnos.SelectedRow.Cells(0).Text)
            EnviarAPagina("../../estudiante/detallematricula.asp?reactivar=1&codigouniver_alu=" & gvAlumnos.SelectedRow.Cells(1).Text & "&codigo_alu=" & gvAlumnos.SelectedRow.Cells(0).Text & "&alumno=" & strAlumno & "&nombre_cpf=" & strEscuela & "&codigo_mat=" & strMatricula)
            Me.lblAviso.Text = ""
        Else
            Me.lblAviso.Text = "No se encuentra el ciclo actual"
        End If        
    End Sub

    Protected Sub gvAlumnos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvAlumnos.SelectedIndexChanged
        If Me.gvAlumnos.SelectedIndex <> -1 Then
            lnkDatos_Click(sender, e)
            Me.lblMatricula.Text = ""
            Me.lblAviso.Text = ""
        End If
    End Sub

    Protected Sub btnReactivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReactivar.Click
        Try
            Dim strMatricula As String
            strMatricula = RetornaMatricula(gvAlumnos.SelectedRow.Cells(0).Text)
            If (strMatricula = "0") Then
                Me.lblAviso.Text = "No se encontró matricula del estudiante para este semestre académico."
            ElseIf (strMatricula > 0) Then
                Me.lblAviso.Text = ""

                If (ReactivaMatricula(strMatricula) = True) Then
                    Me.lblMatricula.Text = "Matricula activada."
                    EnviarAPagina("adminestadocuenta.aspx?id=" & gvAlumnos.SelectedRow.Cells(0).Text)
                End If
            End If
        Catch ex As Exception
            Me.lblMatricula.Text = ""
            Me.lblAviso.Text = "Error al reactivar matricula."
        End Try        
    End Sub
End Class

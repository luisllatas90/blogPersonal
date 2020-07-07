﻿
Partial Class academico_frmLinkEgresados
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Request.QueryString("id") IsNot Nothing) Then
            If (IsPostBack = False) Then
                Me.HdCodigo_Alu.Value = Request.QueryString("id")
                BuscaDatosAlumno(Me.HdCodigo_Alu.Value)
            End If
        Else
            Response.Write("../ErrorSistema.aspx")
        End If
    End Sub

    Protected Sub lnkHistorial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkHistorial.Click
        If (Request.QueryString("param") = 1) Then
            EnviarAPagina("historial_personal.aspx?id=" & Me.HdCodigo_Alu.Value & "&egr=1&param=1")
        Else
            EnviarAPagina("historial_personal.aspx?id=" & Me.HdCodigo_Alu.Value & "&egr=1")
        End If
    End Sub

    Protected Sub lnkCursos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCursos.Click
        EnviarAPagina("frmCursosFaltantes.aspx?id=" & Me.HdCodigo_Alu.Value)
    End Sub

    Protected Sub lnkSolicitudes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSolicitudes.Click
        EnviarAPagina("frmSolicitudes.aspx?id=" & Me.HdCodigo_Alu.Value)
    End Sub

    Private Sub BuscaDatosAlumno(ByVal CodAlumno As Integer)
        Try
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Dim dt As New Data.DataTable
            Dim codigofoto As String = ""
            dt = obj.TraerDataTable("ACAD_DatosAlumno", CodAlumno)
            If (dt.Rows.Count > 0) Then
                Me.lblNombre.Text = dt.Rows(0).Item("NombreCompleto").ToString
                Me.lblCarrera.Text = dt.Rows(0).Item("nombre_Cpf").ToString
                Me.lblCicloIngreso.Text = dt.Rows(0).Item("cicloIng_Alu").ToString
                Me.lblEstado.Text = dt.Rows(0).Item("estado").ToString                

                'Me.imgAlumno.ImageUrl = "//intranet.usat.edu.pe/imgestudiantes/" & codigofoto
            Else
                Me.lblMensaje.Text = "No se encontró a ningun alumno"
            End If
        Catch ex As Exception
            Me.lblMensaje.Text = ex.Message()
        End Try
    End Sub

    Private Sub EnviarAPagina(ByVal pagina As String)
        Me.fradetalle.Attributes("src") = pagina
    End Sub
End Class

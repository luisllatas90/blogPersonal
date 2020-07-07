﻿
Partial Class academico_cargalectiva_frmNroDocentesxCurso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If (IsPostBack = False) Then
            CargaCiclo()
            CargaEscuela()
        End If
    End Sub

    Private Sub CargaCiclo()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim dt As New Data.DataTable
        Try
            dt = obj.TraerDataTable("ACAD_ListaCicloAcademico")
            Me.cboCiclo.DataSource = dt
            Me.cboCiclo.DataTextField = "descripcion_cac"
            Me.cboCiclo.DataValueField = "codigo_cac"
            Me.cboCiclo.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar los ciclos académicos: " & ex.Message
        End Try
    End Sub

    Private Sub CargaEscuela()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim dt As New Data.DataTable
        Try
            dt = obj.TraerDataTable("RPT_ListaEscuela", 2)
            Me.cboEscuela.DataSource = dt
            Me.cboEscuela.DataTextField = "nombre_cpf"
            Me.cboEscuela.DataValueField = "codigo_cpf"
            Me.cboEscuela.DataBind()

        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar los escuelas profesionales: " & ex.Message
        End Try
    End Sub

    Private Sub BuscaDocentes(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim dt As New Data.DataTable
        Try
            Me.hdAux.Value = ""
            dt = obj.TraerDataTable("ACAD_ListaDocentesxCurso", codigo_cac, codigo_cpf)
            Me.gvDocente.DataSource = dt
            Me.gvDocente.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar la lista de cursos y sus docentes: " & ex.Message
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Try
            If (ValidaDatos() = True) Then
                Dim Fila As GridViewRow
                For i As Integer = 0 To Me.gvDocente.Rows.Count - 1
                    Fila = Me.gvDocente.Rows(i)
                    Dim valor As String = CType(Fila.FindControl("txtDocentes"), TextBox).Text
                    'Response.Write(Me.gvDocente.Rows(i).Cells(0).Text & ", " & valor & "<br/>")
                    obj.Ejecutar("ACAD_ActualizaNroDocentesxCup", Me.gvDocente.Rows(i).Cells(0).Text, valor)
                Next
                'Me.lblMensaje.Text = "Se guardo correctamente"
            End If
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al guardar los datos: " & ex.Message
        End Try
    End Sub

    Private Function ValidaDatos() As Boolean
        Try
            Dim Fila As GridViewRow
            For i As Integer = 0 To Me.gvDocente.Rows.Count - 1
                Fila = Me.gvDocente.Rows(i)
                Dim valor As String = CType(Fila.FindControl("txtDocentes"), TextBox).Text
                Integer.Parse(valor)
            Next

            Return True
        Catch ex As Exception
            Me.lblMensaje.Text = "Verificar bien el tipo de datos: " & ex.Message
            Return False
        End Try
    End Function

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        BuscaDocentes(Me.cboCiclo.SelectedValue, Me.cboEscuela.SelectedValue)
    End Sub

End Class

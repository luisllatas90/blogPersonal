﻿
Partial Class administrativo_FrmCreditoOdontologia
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If

        If (IsPostBack = False) Then
            CargaCicloAcademico()
            Me.cboCicloIngreso.Enabled = False
            Me.cboEscuela.Enabled = False
        End If

    End Sub

    Private Sub CargaCarreraProfesional()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_CarrerasxFacultad", "", 0, Me.cboTipoEstudio.SelectedValue, 17)
            obj.CerrarConexion()

            Me.cboEscuela.DataSource = dt
            Me.cboEscuela.DataTextField = "nombre_cpf"
            Me.cboEscuela.DataValueField = "codigo_cpf"
            Me.cboEscuela.DataBind()

        Catch ex As Exception
            Aviso("E", "Error: " & ex.Message)
        End Try
    End Sub

    Private Sub CargaCicloAcademico()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_BuscaCicloAcademico", 0, "N")
            obj.CerrarConexion()
            Me.cboCicloIngreso.DataSource = dt
            Me.cboCicloIngreso.DataTextField = "descripcion_cac"
            Me.cboCicloIngreso.DataValueField = "descripcion_cac"
            Me.cboCicloIngreso.DataBind()

            dt.Dispose()
            dt = Nothing
        Catch ex As Exception
            Aviso("E", "Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Aviso(ByVal tipo As String, ByVal aviso As String)
        If tipo = "E" Then
            Me.lblMensaje.ForeColor = Drawing.Color.Red
            Me.lblMensaje.Font.Bold = True
        Else
            Me.lblMensaje.Font.Bold = False
            Me.lblMensaje.ForeColor = Drawing.Color.Black
        End If

        Me.lblMensaje.Text = aviso
    End Sub

    Private Sub CargaGrid(ByVal tipo As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Try
            Me.gvDetalle.DataSource = Nothing
            Me.gvDetalle.DataBind()

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            If (tipo = "P") Then    'P: Pregrado
                dt = obj.TraerDataTable("ODO_ListaPrecioCredito")
            Else    'O: Otros
                dt = obj.TraerDataTable("ODO_ListaCreditoxCarrera", Me.cboEscuela.SelectedValue, Me.cboCicloIngreso.Text)
            End If
            obj.CerrarConexion()
            Me.gvDetalle.DataSource = dt
            Me.gvDetalle.DataBind()
        Catch ex As Exception
            Aviso("E", "Error: " & ex.Message)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If (ValidaFormulario() = True) Then
                Dim Fila As GridViewRow
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

                obj.AbrirConexion()
                For i As Integer = 0 To Me.gvDetalle.Rows.Count - 1
                    Fila = Me.gvDetalle.Rows(i)
                    Dim valor As String = CType(Fila.FindControl("txtLimite"), TextBox).Text

                    obj.Ejecutar("ODO_ActualizaLimiteCredito", Me.gvDetalle.DataKeys.Item(Fila.RowIndex).Values("codigo_Alu"), valor)
                Next
                obj.CerrarConexion()                
                btnBuscar_Click(sender, e)
                Aviso("E", "Actualización finalizada.")
            End If            
        Catch ex As Exception                        
            Aviso("E", "Error: " & ex.Message)
        End Try
    End Sub

    Private Function ValidaFormulario() As Boolean
        Try
            Dim Fila As GridViewRow
            For i As Integer = 0 To Me.gvDetalle.Rows.Count - 1
                Fila = Me.gvDetalle.Rows(i)
                Dim valor As String = CType(Fila.FindControl("txtLimite"), TextBox).Text
                Double.Parse(valor)
            Next

            Return True
        Catch ex As Exception
            Aviso("E", "Error: Datos ingresados incorrectos")            
            Return False
        End Try
    End Function

    Protected Sub gvDetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalle.RowDataBound        
        If (Request.QueryString("ctf") = 19) Then
            e.Row.Cells(6).Enabled = True
        Else
            e.Row.Cells(6).Enabled = False
        End If

        If (e.Row.RowIndex > -1) Then
            If (e.Row.Cells(3).Text < 0) Then
                e.Row.Cells(3).ForeColor = Drawing.Color.Red
            End If
        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        If (Me.cboTipoEstudio.SelectedIndex = 0) Then
            CargaGrid("P")
            Aviso("N", "(*) Estudiantes de Odontología activos en el ciclo actual")
        Else
            CargaGrid("O")
            Aviso("N", "(*) Estudiantes de " & Me.cboEscuela.SelectedItem.Text & " con ciclo de matrícula " & Me.cboCicloIngreso.Text)
        End If
    End Sub

    Protected Sub cboTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipoEstudio.SelectedIndexChanged
        If (Me.cboTipoEstudio.SelectedIndex = 0) Then   'PreGrado
            Me.cboCicloIngreso.Enabled = False
            Me.cboEscuela.Enabled = False
        Else    'Otras especialidades
            Me.cboCicloIngreso.Enabled = True
            Me.cboEscuela.Enabled = True
            CargaCarreraProfesional()
        End If

    End Sub
End Class

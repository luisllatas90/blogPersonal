﻿Imports System.Data

Partial Class academico_FrmClonarCurso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If (IsPostBack = False) Then
            If (Request.QueryString("id") IsNot Nothing) Then
                Me.HdUsuario.Value = Session("id_per")
            Else
                Response.Redirect("../../../sinacceso.html")
            End If
            CargaCiclos()
            EscuelaProfesional()
        End If
    End Sub

    Private Sub CargaCiclos()
        Dim obj As New ClsConectarDatos
        Dim dt As New DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_BuscaCicloAcademico", 0, "")
            obj.CerrarConexion()

            Me.cboCicloInicio.DataSource = dt
            Me.cboCicloInicio.DataTextField = "descripcion_cac"
            Me.cboCicloInicio.DataValueField = "codigo_cac"
            Me.cboCicloInicio.DataBind()

            Me.cboCicloFinal.DataSource = dt
            Me.cboCicloFinal.DataTextField = "descripcion_cac"
            Me.cboCicloFinal.DataValueField = "codigo_cac"
            Me.cboCicloFinal.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar los semestres academicos"
        End Try
    End Sub

    Private Sub EscuelaProfesional()
        Dim obj As New ClsConectarDatos
        Dim dt As New DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_EscuelaProfesionalxDirector", Me.HdUsuario.Value, 2)
            obj.CerrarConexion()

            Me.cboEscuela.DataSource = dt
            Me.cboEscuela.DataTextField = "nombre_Cpf"
            Me.cboEscuela.DataValueField = "codigo_Cpf"
            Me.cboEscuela.DataBind()

        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar las carreras profesionales"
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New DataTable
        Try
            Dim CicloIni As String = Me.cboCicloInicio.SelectedItem.Text
            Dim CicloFin As String = Me.cboCicloFinal.SelectedItem.Text

            If ((CicloIni.Substring(5) = "0" And CicloFin.Substring(5) <> "0") Or _
                    (CicloIni.Substring(5) <> "0" And CicloFin.Substring(5) = "0")) Then
                Me.lblMensaje.Text = "No puede clonar un semestre de verano con un semestre regular"
                Exit Sub
            End If

            If (Me.cboEscuela.Items.Count = 0) Then
                Me.lblMensaje.Text = "Debe seleccionar una carrera profesional"
                Exit Sub
            End If

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_BuscaCursosAulasxCiclo", Me.cboCicloInicio.SelectedValue, Me.cboCicloFinal.SelectedValue, Me.cboEscuela.SelectedValue)
            obj.CerrarConexion()

            Me.gvCursos.DataSource = dt
            Me.gvCursos.DataBind()
            Me.lblMensaje.Text = ""
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al realizar la busqueda" & ex.Message
        End Try
    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim obj As New ClsConectarDatos
        Dim Fila As GridViewRow
        Try            
            If (ValidaCheckValidos() = True) Then
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

                For i As Integer = 0 To Me.gvCursos.Rows.Count - 1
                    obj.AbrirConexion()
                    Fila = Me.gvCursos.Rows(i)
                    Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                    If (valor = True) Then
                        obj.Ejecutar("ACAD_GenerarClonacionCurso", Me.gvCursos.Rows(i).Cells(1).Text, _
                            Me.gvCursos.Rows(i).Cells(5).Text, Me.cboCicloFinal.SelectedValue, _
                            Me.gvCursos.Rows(i).Cells(12).Text, Me.gvCursos.Rows(i).Cells(9).Text, _
                            Me.gvCursos.Rows(i).Cells(14).Text, Me.gvCursos.Rows(i).Cells(11).Text, _
                            Me.HdUsuario.Value)
                    End If
                    obj.CerrarConexion()
                Next

                btnBuscar_Click(sender, e)
                Me.lblMensaje.Text = "Registros generados."
            End If
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al generar los horarios: " & ex.Message
        End Try
    End Sub

    Private Function ValidaCheckValidos() As Boolean
        Dim Fila As GridViewRow

        If (Me.gvCursos.Rows.Count = 0) Then
            Me.lblMensaje.Text = "Debe seleccionar algun curso."
            Return False
        End If

        For i As Integer = 0 To Me.gvCursos.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = Me.gvCursos.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If (valor = True) Then
                If (Me.gvCursos.Rows(i).Cells(13).Text = "OCUPADO") Then
                    Me.lblMensaje.Text = "Existen ambientes OCUPADOS que no pueden ser clonados."
                    Return False
                End If
            End If
        Next        
        Me.lblMensaje.Text = ""
        Return True
    End Function

    Protected Sub cboCicloInicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCicloInicio.SelectedIndexChanged
        Me.gvCursos.DataSource = Nothing
        Me.gvCursos.DataBind()
    End Sub

    Protected Sub cboCicloFinal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCicloFinal.SelectedIndexChanged
        Me.gvCursos.DataSource = Nothing
        Me.gvCursos.DataBind()
    End Sub

    Protected Sub gvCursos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCursos.RowDataBound
        e.Row.Cells(1).Visible = False      'Codigo_cup
        e.Row.Cells(12).Visible = False     'Codigo_amb
        If (e.Row.Cells(13).Text.Equals("DISPONIBLE") = False) Then
            e.Row.Cells(0).Enabled = False
        End If
        e.Row.Cells(14).Visible = False     'Codigo_hor
    End Sub
End Class

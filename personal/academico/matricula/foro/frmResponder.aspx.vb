﻿Imports System.Data
Imports System.IO

Partial Class academico_matricula_foro_frmResponder
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Me.HdInstancia.Value = Request.QueryString("instancia")
            Me.HdIncidente.Value = Request.QueryString("incidente")
            CargaDatos(Me.HdIncidente.Value)
        End If
    End Sub

    Private Sub CargaDatos(ByVal CodIncidencia As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("MAT_ListaIncidenteForo", CodIncidencia, 0, 0, 0, 0, "%", "%")
            obj.CerrarConexion()

            If (dt.Rows.Count > 0) Then
                Me.lblIncidencia.Text = Me.HdIncidente.Value
                Me.lblEscuela.Text = dt.Rows(0).Item("nombre_Cpf")
                Me.lblCodUniv.Text = dt.Rows(0).Item("codigoUniver_Alu")
                Me.lblAlumno.Text = dt.Rows(0).Item("Estudiante")
                Me.lblAsunto.Text = dt.Rows(0).Item("asunto")
                Me.lblMensaje.Text = dt.Rows(0).Item("Mensaje")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnResponder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResponder.Click
        Dim obj As New ClsConectarDatos
        Dim sName As String = "", sExt As String = "", sTitle As String = ""
        Try
            If FileUpload1.HasFile Then
                sName = FileUpload1.FileName
                sExt = Path.GetExtension(sName)
                sTitle = generaNombreArchivo()
                FileUpload1.SaveAs(MapPath("archivos/" & sTitle & sExt))
            End If
            
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("MAT_RespondeIncidentePersonal", Me.HdIncidente.Value, Session("id_per"), _
                         Me.HdInstancia.Value, Me.txtResponder.Text, sTitle & sExt)
            obj.CerrarConexion()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Function generaNombreArchivo() As String
        Try
            Dim rnd As New Random
            Dim ubicacion As Integer
            Dim strNumeros As String = "0123456789"
            Dim strLetraMin As String = "abcdefghijklmnopqrstuvwxyz"
            Dim strLetraMay As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            Dim Token As String = ""
            Dim strCadena As String = ""
            strCadena = strLetraMin & strNumeros & strLetraMay
            While Token.Length < 10
                ubicacion = rnd.Next(0, strCadena.Length)
                If (ubicacion = 62) Then
                    Token = Token & strCadena.Substring(ubicacion - 1, 1)
                End If
                If (ubicacion < 62) Then
                    Token = Token & strCadena.Substring(ubicacion, 1)
                End If
            End While

            Return Token
        Catch ex As Exception
            Return "-1"
        End Try
    End Function
End Class

Imports System.Collections.Generic
Imports System.Reflection

Partial Class GestionCurricular_FrmCalificadorEstudiante
    Inherits System.Web.UI.Page

    Private cod_user As Integer '= 2238
    Private obj As ClsConectarDatos

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#Region "Eventos"

    Public Sub New()
        If obj Is Nothing Then
            obj = New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If
            cod_user = Session("id_per")

            If Not IsPostBack Then
                If Not Nothing = Session("gc_codigo_cac") AndAlso Session("gc_codigo_cup") AndAlso Session("gc_codigo_mat") Then
                    Call mt_CargarDatos(Session("gc_codigo_cac"), Session("gc_codigo_cup"), Session("gc_codigo_mat"))

                    lblAlumno.InnerText = Session("gc_alumno_nom")
                    btnBack.Visible = True
                    divFiltro1.Visible = False
                    divFiltro2.Visible = False
                Else
                    Call mt_CargarSemestre()

                    lblAlumno.InnerText = "Calificación por Alumno"
                    btnBack.Visible = True
                    divFiltro1.Visible = True
                    divFiltro2.Visible = True
                End If
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemestre.SelectedIndexChanged
        Call mt_CargarCursos(Me.ddlSemestre.SelectedValue)
        Call mt_CargarAlumnos(Me.ddlSemestre.SelectedValue, Me.ddlCurso.SelectedValue)
        Call mt_CargarDatos(Me.ddlSemestre.SelectedValue, Me.ddlCurso.SelectedValue, Me.ddlAlumno.SelectedValue)
    End Sub

    Protected Sub ddlCurso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCurso.SelectedIndexChanged
        Call mt_CargarAlumnos(Me.ddlSemestre.SelectedValue, Me.ddlCurso.SelectedValue)
        Call mt_CargarDatos(Me.ddlSemestre.SelectedValue, Me.ddlCurso.SelectedValue, Me.ddlAlumno.SelectedValue)
    End Sub

    Protected Sub ddlAlumno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAlumno.SelectedIndexChanged
        Call mt_CargarDatos(Me.ddlSemestre.SelectedValue, Me.ddlCurso.SelectedValue, Me.ddlAlumno.SelectedValue)
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Response.Redirect("~/GestionCurricular/frmGenerarPromedio.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Métodos"

    Private Sub mt_CargarSemestre()
        Dim dt As New Data.DataTable

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCicloAcademico", "DA", "")
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlSemestre, dt, "codigo_Cac", "descripcion_Cac")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCursos(ByVal codigo_cac As String)
        Dim dt As New Data.DataTable

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarCursosDocente", codigo_cac, cod_user, "D")
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlCurso, dt, "codigo_Cup", "nombre_Cur")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarAlumnos(ByVal codigo_cac As String, ByVal codigo_cup As String)
        Dim dt As New Data.DataTable

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_EstructuraActaSilabo", codigo_cup, codigo_cac, "D")
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlAlumno, dt, "codigo_mat", "estudiante")

            ddlAlumno.Items.Insert(0, New ListItem("[--- Seleccione Alumno ---]", "-1"))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos(ByVal codigo_cac As String, ByVal codigo_cup As String, ByVal codigo_mat As String)
        Dim dt As New Data.DataTable("Menu")

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarCalificadorEstudiante", codigo_cac, codigo_cup, codigo_mat)
            obj.CerrarConexion()

            Dim json As String = fc_TreeView(dt)
            Page.RegisterStartupScript("Load", "<script>LoadTree(" & json & ");</script>")

            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType, Optional ByVal modal As Boolean = False)
        If modal Then
        Else
            Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
        End If
    End Sub

#End Region

#Region "Funciones"

    Public Function fc_TreeView(ByVal dt As Data.DataTable) As String
        Dim idPadre As Data.DataRow() = dt.Select("id_ref = 0")
        Dim sb As StringBuilder = New StringBuilder()
        sb.Append("[")
        Dim listaJSON As String = fc_GenerarJSON(idPadre, dt, sb)
        Return listaJSON & "]"
    End Function

    Private Function fc_GenerarJSON(ByVal menu As Data.DataRow(), ByVal table As Data.DataTable, ByVal sb As StringBuilder) As String
        If menu.Length > 0 Then

            For Each dr As Data.DataRow In menu
                Dim id As String = dr("id").ToString()
                Dim unidad As String = dr("unidad").ToString()
                Dim descripcion As String = dr("descripcion").ToString()
                Dim nota As String = dr("nota").ToString()
                Dim peso As String = dr("peso").ToString()
                Dim nivel As String = dr("nivel").ToString()
                Dim idRef As String = dr("id_ref").ToString()

                sb.Append("{")

                If nivel.Equals("1") Then
                    sb.Append(String.Format(" ""text"": ""<b>{0}</b>"",", descripcion))
                    sb.Append(" ""color"": ""#FFFFFF"",")
                    If unidad.Equals("--") Then
                        sb.Append(" ""backColor"": ""#9E201C"",")
                    Else
                        sb.Append(" ""backColor"": ""#D9534F"",")
                    End If
                ElseIf nivel.Equals("2") Then
                    sb.Append(String.Format(" ""text"": ""{0}"",", descripcion))
                    sb.Append(" ""backColor"": ""#FFDEDD"",")
                Else
                    sb.Append(String.Format(" ""text"": ""{0}"",", descripcion))
                End If

                sb.Append(" ""state"":{ ""expanded"": ""true""}")

                If nivel.Equals("4") Then
                    sb.Append(String.Format(", ""tags"":[ ""{0}"", ""Nota"" ]", nota))
                ElseIf nivel.Equals("2") Then
                    sb.Append(String.Format(", ""tags"":[ ""Peso: {0}% |  {1}"" ]", peso, nota))
                ElseIf nivel.Equals("1") And unidad.Equals("--") Then
                    sb.Append(String.Format(", ""tags"":[ ""{0}"" ]", nota))
                End If

                Dim subMenu As Data.DataRow() = table.Select(String.Format("id_ref = '{0}'", id))
                If subMenu.Length > 0 AndAlso Not id.Equals(idRef) Then
                    Dim subMenuBuilder As StringBuilder = New StringBuilder()
                    sb.Append(String.Format(", ""nodes"": ["))
                    sb.Append(fc_GenerarJSON(subMenu, table, subMenuBuilder))
                    sb.Append("]")
                End If

                sb.Append("},")
            Next

            sb.Remove(sb.Length - 1, 1)
        End If

        Return sb.ToString()
    End Function

#End Region

End Class

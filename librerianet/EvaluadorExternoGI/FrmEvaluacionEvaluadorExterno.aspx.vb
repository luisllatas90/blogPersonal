
Partial Class GestionInvestigacion_FrmEvaluacionEvaluadorExterno
    Inherits System.Web.UI.Page

    Dim cod_evaluador_i As Integer
    Dim cod_evaluador_s As String
    Dim cod_concurso_i As Integer
    Dim cod_concurso_s As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsGestionInvestigacion
        Try
            'Ver el tema de autentificación

            If (Request.QueryString("EVE") <> "") Then
                cod_evaluador_i = obj.DecrytedString64(Request.QueryString("EVE"))
                cod_evaluador_s = Request.QueryString("EVE")
                Me.hdEVE.Value = cod_evaluador_s
            End If
            If (Request.QueryString("CON") <> "") Then
                cod_concurso_i = obj.DecrytedString64(Request.QueryString("CON"))
                cod_concurso_s = Request.QueryString("CON")
                Me.hdCON.Value = cod_concurso_s
            End If
            'Me.txtEvaluadorExterno.Value = cod_evaluador_i & "-" & cod_concurso_i
            Informacion()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub

    Private Sub Informacion()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim dt1 As New Data.DataTable
            Dim dt2 As New Data.DataTable
            Dim strBody As New StringBuilder

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("INV_listarEvaluadoresExternosCodigo", cod_evaluador_i)
            obj.CerrarConexion()

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    If (dt IsNot Nothing) Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            Me.txtEvaluadorExterno.Value = dt.Rows(i).Item("nombre_eve")
                        Next
                    End If

                End With
            End If

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("INV_ListarConcurso", "E", cod_concurso_i, "", "0", "%", 684, 1)
            obj.CerrarConexion()

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    If (dt IsNot Nothing) Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            Me.txtConcurso.Value = dt.Rows(i).Item("titulo_con")
                        Next
                    End If

                End With
            End If

            Me.txtRubrica.InnerHtml = "<a href=""Archivos/Concursos/Estructura/Rúbrica Evaluador externo.xlsx"" target=""_blank"" style=""font-weight: bold; font-style: oblique;"">Descargar Rúbrica</a>"

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub
End Class

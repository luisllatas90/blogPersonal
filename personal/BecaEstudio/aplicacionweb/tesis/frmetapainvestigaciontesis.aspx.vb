
Partial Class frmetapainvestigaciontesis
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim tbl As Data.DataTable
            tbl = obj.TraerDataTable("TES_ConsultarEtapaInvestigacionTesis", 3, Request.QueryString("codigo_tes"), 0, 0)

            Me.lblFase.Text = tbl.Rows(0).Item("nombre_eti")
            Me.hdFase.Value = tbl.Rows(0).Item("codigo_eti")
           
            tbl = Nothing
        End If
        Me.txtFechaAprobacion.Attributes.Add("OnKeyDown", "return false")
        'Me.txtFechaAprobacion.Text = Now.Date
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim codigo_tes, codigo_Etes, codigo_per As Integer

        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        codigo_per = Request.QueryString("codigo_per")
        codigo_Etes = Request.QueryString("codigo_Etes")
        codigo_tes = Request.QueryString("codigo_tes")

        If Request.QueryString("accion") = "A" Then
            Try
                '==================================
                ' Guardar los datos
                '==================================
                obj.IniciarTransaccion()
                obj.Ejecutar("TES_AgregarEtapaInvestigacionTesis", CDate(Me.txtFechaAprobacion.Text).ToShortDateString, codigo_tes, 1, Me.hdFase.Value, codigo_per, Me.TxtComentario.Text.Trim, Me.chkBloquear.Checked)
                obj.TerminarTransaccion()
                obj = Nothing
                Response.Write("<script>window.opener.location.reload();window.close()</script>")
            Catch ex As Exception
                obj.AbortarTransaccion()
                Me.LblMensaje.Text = "Ocurrió un Error al Registrar el archivo. Intente mas tarde." & Chr(13) & ex.Message
                obj = Nothing
            End Try
        End If
    End Sub
End Class

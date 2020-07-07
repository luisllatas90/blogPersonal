
Partial Class academico_horarios_frmhorarioambiente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            mostrar(1)
            fnLoading(True)

            'Parametros de configuracion{
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")
            Dim Modulo As Integer = Request.QueryString("mod")
            '}



        End If
    End Sub


#Region "Metodos"

#End Region


#Region "Procedimientos"
    Private Sub mostrar(ByVal tipo As Int16)
        If tipo = 1 Then
            PanelLista.Visible = True
            PanelRegistro.Visible = False
        ElseIf tipo = 2 Then
            PanelLista.Visible = False
            PanelRegistro.Visible = True
        End If

    End Sub
    Private Sub fnLoading(ByVal sw As Boolean)
        If sw Then
            ' Response.Write(1 & "<br>")
            Me.loading.Attributes.Remove("class")
            Me.loading.Attributes.Add("class", "hidden")
        Else
            ' Response.Write(0 & "<br>")
            Me.loading.Attributes.Remove("class")
            Me.loading.Attributes.Add("class", "")
            ' Me.loading.Attributes.Add("class", "show")
        End If
    End Sub

#End Region


#Region "Funciones"

#End Region
End Class

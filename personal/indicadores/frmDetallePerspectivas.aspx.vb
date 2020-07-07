
Partial Class indicadores_frmDetallePerspectivas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargarObjetivosPorTabPerspectiva(Request.QueryString("Codigo_pers"), Request.QueryString("codigo_plan"))
                'Response.Write(Request.QueryString("anio"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarObjetivosPorTabPerspectiva(ByVal vCodigo_pers As Integer, _
                                                 ByVal vCodigo_pla As Integer)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListarObjetivos(vCodigo_pers, vCodigo_pla)

            If dts.Rows.Count > 0 Then
                ddlObjetivos.DataSource = dts
                ddlObjetivos.DataValueField = "Codigo"
                ddlObjetivos.DataTextField = "Descripcion"
                ddlObjetivos.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlObjetivos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlObjetivos.SelectedIndexChanged
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            If ddlObjetivos.SelectedValue <> 0 Then
                dts = obj.CargarIndicadoresSegunObjetivo(ddlObjetivos.SelectedValue)
                If dts.Rows.Count > 0 Then
                    ddlIndicadores.DataSource = dts
                    ddlIndicadores.DataTextField = "Descripcion"
                    ddlIndicadores.DataValueField = "codigo"
                    ddlIndicadores.DataBind()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlIndicadores_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlIndicadores.SelectedIndexChanged
        Try
            If ddlIndicadores.SelectedValue <> 0 Then
                MostrarPanelesTab()
                lnkDatosEvento_Click(sender, e)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub MostrarPanelesTab()
        Me.tabs.Visible = True
        'Me.lnkDatosEvento.Visible = True
        'Me.lnkDatosEvento.Visible = True
    End Sub

    Private Sub EnviarAPagina(ByVal pagina As String, ByVal vPestaña As Integer)
        Try
            'Ejemplo de como pasar los parametros: xDguevara
            'Me.fradetalle.Attributes("src") = pagina & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cco=" & Me.cboCecos.SelectedValue

            Select Case vPestaña
                Case 1
                    Me.fradetalle.Attributes("src") = pagina & "&codigo_ind=" & ddlIndicadores.SelectedValue & "&anio=" & 2011
                Case 2
                    Me.fradetalle.Attributes("src") = pagina

            End Select

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub lnkDatosEvento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDatosEvento.Click
        Try
            'EnviarAPagina("https://intranet.usat.edu.pe/rptusat/?/PRIVADOS/ACADEMICO/IND_GraficoBarrasUnIndicador", 1)
            '---------------------------------------------------------------------------------------------------------------
            'Fecha: 29.10.2012
            'Usuario: dguevara
            'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
            '---------------------------------------------------------------------------------------------------------------
            Me.fradetalle.Attributes("src") = "//intranet.usat.edu.pe/rptusat/?/PRIVADOS/ACADEMICO/IND_GraficoBarrasUnIndicador" & "&codigo_ind=" & ddlIndicadores.SelectedValue & "&anio=" & Request.QueryString("anio")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class


Partial Class personal_academico_tesis_habilitar_frmhabilitarmatricula
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)

            Me.dpCodigo_cac.DataSource = obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0)
            Me.dpCodigo_cac.DataBind()

            Me.grwCronograma.DataSource = obj.TraerDataTable("ConsultarMatricula", 6, Me.dpCodigo_cac.SelectedValue, 0, 0)
            Me.grwCronograma.DataBind()

            'Cargar mensajes de activaciones realizadas
            Dim tblmensajes As Data.DataTable

            tblmensajes = obj.TraerDataTable("MAT_ConfigurarNuevaMatricula", 5, Me.dpCodigo_cac.SelectedValue, Request.QueryString("mod"))

            For i As Int16 = 0 To tblmensajes.Rows.Count - 1
                Select Case tblmensajes.Rows(i).Item("tipo")
                    Case 0 : Me.lbltipo0.Text = tblmensajes.Rows(i).Item("suceso").ToString
                    Case 1 : Me.lbltipo1.Text = tblmensajes.Rows(i).Item("suceso").ToString
                    Case 2 : Me.lbltipo2.Text = tblmensajes.Rows(i).Item("suceso").ToString
                    Case 3 : Me.lbltipo3.Text = tblmensajes.Rows(i).Item("suceso").ToString
                    Case 4 : Me.lbltipo4.Text = tblmensajes.Rows(i).Item("suceso").ToString
                    Case 6 : Me.lbltipo5.Text = tblmensajes.Rows(i).Item("suceso").ToString
                End Select
            Next
            tblmensajes.Dispose()
            obj = Nothing
        End If
        Me.cmdAperturar.Text = Me.dpCodigo_cac.SelectedItem.Text
    End Sub
    Protected Sub dpCodigo_cac_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpCodigo_cac.SelectedIndexChanged
        If dpCodigo_cac.SelectedValue <> "" Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)

            Me.grwCronograma.DataSource = obj.TraerDataTable("ConsultarMatricula", 6, Me.dpCodigo_cac.SelectedValue, 0, 0)
            Me.grwCronograma.DataBind()
            obj = Nothing
        End If
    End Sub

    Protected Sub cmdActivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdActivar.Click
        If Me.txtClave.Text = "dsmatricula2011" Then
            Me.txtClave.Enabled = False
            Me.lblmensaje.Text = ""
            'Habilitar botones
            Me.cmdAperturar.Enabled = True
            Me.cmdAperturar0.Enabled = True
            Me.cmdAperturar1.Enabled = True
            Me.cmdAperturar2.Enabled = True
            Me.cmdAperturar3.Enabled = True
            Me.cmdAperturar4.Enabled = True
        Else
            Me.txtClave.Enabled = True
            Me.lblmensaje.Text = "Intente denuevo. La clave es incorrecta"
        End If
    End Sub
    Protected Sub cmdAperturar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAperturar.Click
        EjecutarConfiguraciones(0)
    End Sub
    Protected Sub cmdAperturar0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAperturar0.Click
        EjecutarConfiguraciones(1)
    End Sub
    Private Sub EjecutarConfiguraciones(ByVal tipo As String)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Try
            obj.IniciarTransaccion()
            obj.Ejecutar("MAT_ConfigurarNuevaMatricula", tipo, Me.dpCodigo_cac.SelectedValue, Request.QueryString("mod"))
            obj.TerminarTransaccion()

            'Cargar mensajes
            Dim tblmensajes As Data.DataTable

            tblmensajes = obj.TraerDataTable("MAT_ConfigurarNuevaMatricula", 5, Me.dpCodigo_cac.SelectedValue, Request.QueryString("mod"))

            For i As Int16 = 0 To tblmensajes.Rows.Count - 1
                Select Case tblmensajes.Rows(i).Item("tipo")
                    Case 0 : Me.lbltipo0.Text = tblmensajes.Rows(i).Item("suceso").ToString
                    Case 1 : Me.lbltipo1.Text = tblmensajes.Rows(i).Item("suceso").ToString
                    Case 2 : Me.lbltipo2.Text = tblmensajes.Rows(i).Item("suceso").ToString
                    Case 3 : Me.lbltipo3.Text = tblmensajes.Rows(i).Item("suceso").ToString
                    Case 4 : Me.lbltipo4.Text = tblmensajes.Rows(i).Item("suceso").ToString
                    Case 6 : Me.lbltipo5.Text = tblmensajes.Rows(i).Item("suceso").ToString
                End Select
            Next
            tblmensajes.Dispose()
        Catch ex As Exception
            Me.lblmensaje.Text = "Ocurrió un error " & ex.Message
            obj.AbortarTransaccion()
        End Try
        obj = Nothing
    End Sub

    Protected Sub cmdAperturar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAperturar1.Click
        EjecutarConfiguraciones(2)
    End Sub

    Protected Sub cmdAperturar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAperturar2.Click
        EjecutarConfiguraciones(3)
    End Sub

    Protected Sub cmdAperturar3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAperturar3.Click
        EjecutarConfiguraciones(4)
    End Sub

    Protected Sub cmdAperturar4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAperturar4.Click
        EjecutarConfiguraciones(6)
    End Sub
End Class

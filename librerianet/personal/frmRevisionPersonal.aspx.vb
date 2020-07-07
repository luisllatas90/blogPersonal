
Partial Class personal_frmRevisionPersonal
    Inherits System.Web.UI.Page
    Dim codigo_per As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            If txtPersonal.Text <> "" Then
                Dim dts As New Data.DataTable
                Dim obj As New clsPersonal                
                dts = obj.PER_ConsultarPersonal("VA", txtPersonal.Text)
                gvListaPersonal.DataSource = dts
                gvListaPersonal.DataBind()
                gvListaPersonal.Columns(0).Visible = False 'oculto la columna ID porque es irrelevante para el usuario. Es fundamental ocultarla luego del DataBind porque sino no recibirá los datos                
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub MostrarPanelesTab()
        Try
            Me.tabs.Visible = False
            Me.lblMensaje.Visible = False

            'Cargar información de la Persona
            Dim obj As New ClsConectarDatos

            Dim tbl As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            tbl = obj.TraerDataTable("ConsultarPersonal", "CO", lblcodigo_per.Text)
            obj.CerrarConexion()
            obj = Nothing           

            'Mostrar Tabs
            Me.lnkDatosPersona.Visible = True
            Me.tabs.Visible = True            
            EnviarAPagina("detallepersona.aspx?codigo_per=" & lblcodigo_per.Text)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try        
    End Sub

    Private Sub EnviarAPagina(ByVal pagina As String)
        Try            
            Me.fradetalle.Attributes("src") = pagina
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkDatosEPersona_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDatosPersona.Click
        Try
            EnviarAPagina("detallepersona.aspx?codigo_per=" & lblcodigo_per.Text)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaPersonal_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvListaPersonal.PageIndexChanging
        Dim grilla As GridView = CType(sender, GridView)
        With grilla
            .PageIndex = e.NewPageIndex()
        End With
        Dim dts As New Data.DataTable
        Dim obj As New clsPersonal
        dts = obj.PER_ConsultarPersonal("VA", txtPersonal.Text)
        gvListaPersonal.DataSource = dts
        gvListaPersonal.DataBind()
        gvListaPersonal.Columns(0).Visible = False 'oculto la columna ID porque es irrelevante para el usuario. Es fundamental ocultarla luego del DataBind porque sino no recibirá los datos
    End Sub

    Protected Sub gvListaPersonal_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaPersonal.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion                

                Dim seleccion As GridViewRow
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)

                'Obtengo el datakey del registro seleccionado
                lblcodigo_per.Text = gvListaPersonal.DataKeys(seleccion.RowIndex).Value.ToString

                MostrarPanelesTab()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkHorario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkHorario.Click
        Try
            EnviarAPagina("frmVistaTestdeHorario.aspx?id=" & lblcodigo_per.Text)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkHorarioAdm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkHorarioAdm.Click
        Try
            'EnviarAPagina("vsthorariodocente.asp?modo=A&id=" & lblcodigo_per.Text)

            'EnviarAPagina("vsthorariodocente.asp?modo=A&id=" & lblcodigo_per.Text & "&ctf=1&codigo_cac=43") 'NO PINTA HORARIO
            EnviarAPagina("../../personal/academico/horarios/vsthorariodocente1.asp?codigo_cac=43&codigo_per=" & lblcodigo_per.Text & "&titulo=HORARIOS: DOCENTE&modo=A")
            'EnviarAPagina("vsthorariodocente.asp?codigo_cac=43&codigo_per=" & lblcodigo_per.Text & "&titulo=HORARIOS: DOCENTE&modo=A")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub txtPersonal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPersonal.TextChanged
        btnBuscar_Click(sender, e)
    End Sub
End Class

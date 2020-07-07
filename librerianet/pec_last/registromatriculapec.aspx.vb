Partial Class registromatriculapec
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.dpPrograma, obj.TraerDataTable("PEC_ConsultarProgramaEC", 8, codigo_usu, codigo_tfu, "FC"), "codigo_pec", "descripcion_pes", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing

            LimpiarTablasTemporales()
        End If
    End Sub

    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        Me.grwParticipantes.ShowFooter = True
        Me.CargarParticipantes()
    End Sub
    Protected Sub grwParticipantes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwParticipantes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(8).Attributes.Add("onclick", "return confirm('Acción irreversible: ¿Esta seguro que desea eliminar el participante?');")
            If CType(e.Row.Cells(2).FindControl("lblap"), Label).Text.Trim = "--" Then
                e.Row.Cells(7).Text = ""
            Else
                e.Row.Cells(0).Text = e.Row.RowIndex + 1
                e.Row.Cells(7).Text = "<a href='../frmpersona.aspx?t=E&accion=M&pec=" & Me.dpPrograma.SelectedValue & "&cl=" & fila.Row("codigo_alu") & "&op=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&KeepThis=true&TB_iframe=true&height=400&width=650&modal=true' title='Actualizar datos' class='thickbox'><img src='../../images/editar.gif' border=0 /><a/>"
            End If
        End If
    End Sub

    Protected Sub grwParticipantes_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwParticipantes.RowDeleting
        Dim obj As New ClsConectarDatos
        Dim mensaje(1) As String
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("PEC_EliminarMatriculaPEC", Me.dpPrograma.SelectedValue, grwParticipantes.DataKeys(e.RowIndex).Value, "").copyto(mensaje, 0)
        obj.CerrarConexion()
        obj = Nothing
        If mensaje(0).ToString <> "" Then
            Me.lblmensaje.Text = mensaje(0)
        Else
            Me.CargarParticipantes()
        End If
        e.Cancel = True
    End Sub
    Private Sub LimpiarTablasTemporales()
        Me.grwParticipantes.ShowFooter = False
        Me.grwParticipantes.DataBind()
    End Sub

    Private Sub CargarParticipantes()
        Dim obj As New ClsConectarDatos

        Me.lblmensaje.Text = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.grwParticipantes.DataSource = obj.TraerDataTable("PEC_ConsultarMatriculadosProgramaEC", 0, Me.dpPrograma.SelectedValue, 0, 0)
        Me.grwParticipantes.DataBind()
        Me.cmdAgregar.Visible = True

        'Mostrar siempre 1 fila para AGREGAR Participante
        If Me.grwParticipantes.Rows.Count = 0 Then
            Dim fila As Data.DataRow
            Dim tbl As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            tbl = obj.TraerDataTable("PEC_ConsultarMatriculadosProgramaEC", 0, 0, 0, 0)
            'Cargar Centro de Costos
            fila = tbl.NewRow()
            fila("codigo_alu") = 0
            fila("apellidopat_alu") = "--"
            fila("apellidomat_alu") = "--"
            fila("nombres_alu") = "--"

            'Añadir fila
            tbl.Rows.Add(fila)

            Me.grwParticipantes.DataSource = tbl
            Me.grwParticipantes.DataBind()
        End If
        Me.grwParticipantes.Visible = True
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub cmdVer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVer.Click
        Me.grwParticipantes.Visible = False
        Me.lblmensaje.Text = ""
        If Me.dpPrograma.SelectedValue <> -1 Then
            CargarParticipantes()
        End If
    End Sub
    Protected Sub imgGuardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim obj As New ClsConectarDatos
        Try
        Dim apellidopat_alu, apellidomat_alu, nombres_alu, sexo_alu, tipo, dni As String
        Dim codigo_usu As Integer = Request.QueryString("id")

        apellidopat_alu = CType(Me.grwParticipantes.FooterRow.Cells(2).FindControl("txtap"), TextBox).Text.Trim
        apellidomat_alu = CType(Me.grwParticipantes.FooterRow.Cells(3).FindControl("txtam"), TextBox).Text.Trim
        nombres_alu = CType(Me.grwParticipantes.FooterRow.Cells(4).FindControl("txtn"), TextBox).Text.Trim
        sexo_alu = CType(Me.grwParticipantes.FooterRow.Cells(5).FindControl("dpsexo"), DropDownList).SelectedValue
        tipo = CType(Me.grwParticipantes.FooterRow.Cells(6).FindControl("dptipo"), DropDownList).SelectedValue
        dni = CType(Me.grwParticipantes.FooterRow.Cells(6).FindControl("txtdni"), TextBox).Text.Trim

        Dim mensaje(1) As String
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("PEC_AgregarMatriculaPEC", Me.dpPrograma.SelectedValue, UCase(apellidopat_alu), UCase(apellidomat_alu), UCase(nombres_alu), sexo_alu, Me.GeneraClave(), codigo_usu, 0, tipo, dni, "").copyto(mensaje, 0)
        obj.CerrarConexion()
        obj = Nothing
        If mensaje(0).ToString <> "" Then
            Me.lblmensaje.Text = mensaje(0)
        Else
            Me.grwParticipantes.ShowFooter = False
            Me.CargarParticipantes()
        End If
        Catch ex As Exception
            obj = Nothing
            Me.lblmensaje.Text = "Ocurrió un error en el sistema. Contáctese con desarrollosistemas@usat.edu.pe. Envíe el siguiente texto:<Br/>" & ex.Message
        End Try
    End Sub

    Private Function GeneraLetra() As String
        GeneraLetra = Chr(((Rnd() * 100) Mod 25) + 65)
    End Function
    Private Function GeneraClave() As String
        Dim Letras As String
        Dim Numeros As String
        Letras = GeneraLetra() & GeneraLetra()
        Numeros = Format((Rnd() * 8888) + 1111, "0000")
        GeneraClave = Letras & Numeros
    End Function
End Class

Partial Class proponente_datospropuesta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Write(Request.QueryString("codigo_prp"))
        If Request.QueryString("codigo_prp") IsNot Nothing Then

            If Not IsPostBack Then
                Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                Dim rsEstados As New Data.DataTable
                rsEstados = ObjCnx.TraerDataTable("PRP_ConsultarEstadoPropuestas", Request.QueryString("codigo_prp"))
                Me.lblEstadoFacultad.Text = rsEstados.Rows(0).Item(0)
                Me.lblEstadoRectorado.Text = rsEstados.Rows(0).Item(1)
                Me.lblEstadoConsejo.Text = rsEstados.Rows(0).Item(2)
                Dim rsVersiones As New Data.DataTable
                rsVersiones = ObjCnx.TraerDataTable("CONSULTARVERSIONESPROPUESTA", "ES", Request.QueryString("codigo_prp"), 0)
                ClsFunciones.LlenarListas(Me.ddlversiones, rsVersiones, "version_dap", "version_dap")
                ConsultarDatos()
            End If
        Else
            Me.Form.Controls.Clear()
            Response.Write("Seleccione una propuesta.")
        End If

    End Sub
    Private Sub ListarArchivos(ByVal codigo_cop As String, ByVal codigo_dap As String, ByVal codigo_dip As String)
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Datos As Data.DataTable

        If codigo_cop <> "" Then
            Datos = Obj.TraerDataTable("ConsultarArchivosPropuesta", "TO", codigo_cop)
        Else
            If codigo_dap <> "0" Then
                Datos = Obj.TraerDataTable("ConsultarArchivosPropuesta", "CP", codigo_dap)
            Else
                Datos = Obj.TraerDataTable("ConsultarArchivosPropuesta", "CI", codigo_dip)
            End If
        End If
        Me.GridView1.DataSource = Datos
        Me.GridView1.DataBind()
        Obj = Nothing
        Datos.Dispose()
        Datos = Nothing
    End Sub
    Private Sub ConsultarDatos()
        Dim rsVersion As New Data.DataTable

        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        rsVersion = ObjCnx.TraerDataTable("CONSULTARVERSIONESPROPUESTA", "DA", Request.QueryString("codigo_prp"), Me.ddlversiones.SelectedValue)

        Me.lblPropuesta.Text = rsVersion.Rows(0).Item("nombre_Prp").ToString
        Me.lblProponente.Text = rsVersion.Rows(0).Item("apellidoPat_Per") & " " & rsVersion.Rows(0).Item("apellidoMat_Per") & " " & rsVersion.Rows(0).Item("Nombres_Per")
        Me.lblArea.Text = rsVersion.Rows(0).Item("descripcion_cco").ToString
        Me.lblTipoPropuesta.Text = rsVersion.Rows(0).Item("Descripcion_Tpr").ToString
        Me.lblInstancia.Text = rsVersion.Rows(0).Item("instancia_Prp").ToString.ToUpper

        Me.lblResumen.Text = rsVersion.Rows(0).Item("beneficios_dap").ToString
        Me.lblImportancia.Text = rsVersion.Rows(0).Item("importancia_dap").ToString

        Me.lblSimbolo.Text = rsVersion.Rows(0).Item("descripcion_tip").ToString
        Me.lblCambio.Text = rsVersion.Rows(0).Item("tipocambio_dap").ToString
        Me.lblSim0.Text = rsVersion.Rows(0).Item("simbolo_moneda").ToString
        Me.lblSim1.Text = rsVersion.Rows(0).Item("simbolo_moneda").ToString
        Me.lblSim2.Text = rsVersion.Rows(0).Item("simbolo_moneda").ToString
        Me.lblIngreso.Text = rsVersion.Rows(0).Item("ingreso_dap").ToString
        Me.lblEgreso.Text = rsVersion.Rows(0).Item("egreso_dap").ToString
        Me.lblIngresoMN.Text = rsVersion.Rows(0).Item("ingresoMN_dap").ToString
        Me.lblEgresoMN.Text = rsVersion.Rows(0).Item("egresoMN_dap").ToString
        Me.lblUtilidad.Text = rsVersion.Rows(0).Item("utilidad_dap").ToString
        Me.lblUtilidadMN.Text = rsVersion.Rows(0).Item("utilidadMN_dap").ToString

        Dim rsDatos As New Data.DataTable
        Dim codigo_dap, codigo_cop, codigo_dip As Integer
        rsDatos = ObjCnx.TraerDataTable("PRP_ConsultarDatosCodigosPropuesta", Request.QueryString("codigo_prp"), Me.ddlversiones.SelectedValue)
        codigo_dap = rsDatos.Rows(0).Item("codigo_dap")
        codigo_cop = rsDatos.Rows(0).Item("codigo_cop")
        codigo_dip = 0
        'Response.Write(Request.QueryString("codigo_prp"))
        ' Response.Write(Me.ddlversiones.SelectedValue)
        Call ListarArchivos(codigo_cop, codigo_dap, codigo_dip)

    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        'ConsultarDatos()
    End Sub

    Protected Sub ddlversiones_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlversiones.SelectedIndexChanged
        ConsultarDatos()
    End Sub


    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("presentacion_agenda.aspx?id_rec=" & Request.QueryString("id_rec"))
    End Sub

    Protected Sub cmdComentarios_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdComentarios.Click
        Response.Redirect("comentarios.aspx?codigo_prp=" & Request.QueryString("codigo_prp") & "&nombre_prp=" & Me.lblPropuesta.Text & "&id_rec=" & Request.QueryString("id_rec"))
    End Sub

    Protected Sub cmdCalificaciones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCalificaciones.Click
        Response.Redirect("revisores.aspx?codigo_prp=" & Request.QueryString("codigo_prp") & "&nombre_prp=" & Me.lblPropuesta.Text & "&id_rec=" & Request.QueryString("id_rec"))
    End Sub
End Class

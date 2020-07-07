
Partial Class personal_administrativo_pec_frmConsultarCargosPorCeco
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim objfun As New ClsFunciones
            Dim obj As New ClsConectarDatos
            Dim datos As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()


            'Cargar Datos Centro Costos
            datos = obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), "", Request.QueryString("mod"))
            objfun.CargarListas(cboCecos, datos, "codigo_Cco", "Nombre", ">> Seleccione <<")
            datos.Dispose()

            obj.CerrarConexion()

            Panel3.Visible = False
            MostrarBusquedaCeCos(False)

            btnConsultar.Enabled = False
            lblMensaje.Text = ""

        End If
    End Sub

    Protected Sub ImgBuscarCecos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarCecos.Click
        BuscarCeCos()
    End Sub

    Private Sub BuscarCeCos()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.abrirconexion()
        gvCecos.DataSource = obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), txtBuscaCecos.Text, Request.QueryString("mod"))
        gvCecos.DataBind()
        obj.cerrarconexion()
        obj = Nothing
        Panel3.Visible = True
    End Sub
    Private Sub MostrarBusquedaCeCos(ByVal valor As Boolean)
        Me.txtBuscaCecos.Visible = valor
        Me.ImgBuscarCecos.Visible = valor
        Me.lblTextBusqueda.Visible = valor
        Me.cboCecos.Visible = Not (valor)
    End Sub


    Protected Sub gvCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCecos.SelectedIndexChanged
        cboCecos.SelectedValue = Me.gvCecos.DataKeys.Item(Me.gvCecos.SelectedIndex).Values(0)
        MostrarBusquedaCeCos(False)
        Panel3.Visible = False
        lnkBusquedaAvanzada.Text = "Busqueda Avanzada"
        cboCecos_SelectedIndexChanged(sender, e)

    End Sub



    Protected Sub lnkBusquedaAvanzada_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBusquedaAvanzada.Click
        If lnkBusquedaAvanzada.Text.Trim = "Búsqueda Simple" Then
            MostrarBusquedaCeCos(False)
            lnkBusquedaAvanzada.Text = "Búsqueda Avanzada"
        Else
            MostrarBusquedaCeCos(True)
            lnkBusquedaAvanzada.Text = "Búsqueda Simple"
            txtBuscaCecos.Text = ""
        End If

        lblMensaje.Text = ""
    End Sub

    Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCecos.SelectedIndexChanged

        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        Dim datos As New Data.DataTable


        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        datos = obj.TraerDataTable("PRESU_ConsultarServiciosEnDeuda", cboCecos.SelectedValue)

        If datos.Rows.Count > 0 Then
            objfun.CargarListas(cboServ, datos, "codigo_Sco", "nombre", ">> Todos los servicios <<")
            btnConsultar.Enabled = True
        Else
            objfun.CargarListas(cboServ, datos, "codigo_Sco", "nombre", ">> No se encontró ningún servicio cargado <<")
            btnConsultar.Enabled = False
        End If

        datos.Dispose()

        obj.CerrarConexion()

        gvResultado.DataSource = Nothing
        gvResultado.DataBind()

        lblMensaje.Text = ""

    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click

        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        Dim datos As New Data.DataTable


        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        datos = obj.TraerDataTable("PRESU_ConsultarDeudasPorCeco", cboCecos.SelectedValue, cboServ.SelectedValue)
        gvResultado.DataSource = datos
        gvResultado.DataBind()


        datos.Dispose()

        obj.CerrarConexion()

        lblMensaje.Text = ""


    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click

        If gvResultado.Rows.Count > 0 Then
            'btnConsultar_Click(sender, e)
            Axls("Lista de Cargos", gvResultado, "CECO: " & cboCecos.SelectedItem.Text, "Campus Virtual USAT")
        Else
            lblMensaje.Text = "Debe ejecutar la consulta primero antes de exportar."
        End If

    End Sub

    Private Sub Axls(ByVal nombrearchivo As String, ByRef grid As GridView, ByVal titulo As String, ByVal piedepagina As String)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & nombrearchivo & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        grid.HeaderRow.BackColor = Drawing.Color.FromName("#3366CC")
        grid.HeaderRow.ForeColor = Drawing.Color.White
        grid.Columns(10).Visible = False
        Response.Write(ClsFunciones.HTML(grid, titulo, piedepagina))
        Response.End()
    End Sub
    Protected Sub gvResultado_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvResultado.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub
End Class

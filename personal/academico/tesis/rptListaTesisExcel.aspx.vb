
Partial Class academico_tesis_rptListaTesisExcel
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim Carrera As Integer = Request.QueryString("mod")
            Dim Ciclo As Integer = Request.QueryString("Opt")
            Dim Por As Integer = Request.QueryString("Por")
            Dim xPor As Integer = Request.QueryString("Por")
            Dim text As String = Request.QueryString("text")

            Call CargarGrid(Por, text, xPor, Carrera, Ciclo)
        End If
    End Sub

    Private Sub CargarGrid(ByVal Por As Integer, ByVal text As String, ByVal xPor As Integer, ByVal Carrera As Integer, ByVal Ciclo As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Me.grwListaTesis.DataSource = obj.TraerDataTable("TES_ConsultarTesis_v2", Por, text, xPor, Carrera, Ciclo)
        Me.grwListaTesis.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub


    Protected Sub grwListaTesis_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwListaTesis.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            'e.Row.Cells(4).Text = "<a href='detalleetapatesis.aspx?codigo_tes=" & fila.Row("codigo_tes") & "&codigo_per=" & Request.QueryString("id") & "&KeepThis=true&TB_iframe=true&height=400&width=650&modal=true' title='Cambiar estado' class='thickbox'>" & fila.Row("nombre_eti") & "&nbsp;<img src='../../../images/menu6.gif' border=0 /><a/>"
            'e.Row.Cells(6).Text = "<a href='asignarinvolucrado.aspx?codigo_tes=" & fila.Row("codigo_tes") & "&id=" & Request.QueryString("id") & "&KeepThis=true&TB_iframe=true&height=400&width=650&modal=true' title='Asignar Asesores / Jurado' class='thickbox'><img src='../../../images/contargrupo.gif' border=0 /><a/>"
            'e.Row.Cells(7).Text = "<a href='frmtesis.aspx?accion=M&codigo_tes=" & fila.Row("codigo_tes") & "&id=" & Request.QueryString("id") & "&KeepThis=true&TB_iframe=true&height=500&width=650&modal=true' title='Modificar Trabajos de Investigación' class='thickbox'><img src='../../../images/editar.gif' border=0 /><a/>"
            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            'Cargar Autores de la Tesis
            Dim obj As New ClsConectarDatos
            Dim grw As BulletedList
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            grw = CType(e.Row.FindControl("bAutores"), BulletedList)

            grw.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis", 9, fila.Row("codigo_tes"), 0, 0)
            grw.DataBind()
            'Cargar Autores de la Tesis
            grw = CType(e.Row.FindControl("bAsesores"), BulletedList)
            grw.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis", 10, fila.Row("codigo_tes"), 4, 7)
            grw.DataBind()
            obj.TerminarTransaccion()
            obj = Nothing
        End If
    End Sub

    Protected Sub cmdExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExport.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.grwListaTesis.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.grwListaTesis)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=tesis" & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class

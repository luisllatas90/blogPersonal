Imports System.Data
Imports System.IO
Imports System.Drawing

Partial Class frmmonitoreoderendiciones
    Inherits System.Web.UI.Page
    'Dim cn As New clsaccesodatos
    Public dtscliente As New System.Data.DataSet, k As Integer
    Public cn As New clsaccesodatos
    Dim dtsMonitor As New System.Data.DataTable
    Sub mostrarcliente()

        'Dim dts As New DataSet
        'cn.abrirconexion()
        'dts = cn.consultar("sp_buscacliente", "BT", "PE", "%", "", "")
        'cn.cerrarconexion()

        'Me.cbocliente.DataSource = dts
        'Me.cbocliente.DataTextField = "nombres"
        'Me.cbocliente.DataValueField = "codigo_tcl"
        'Me.cbocliente.DataBind()
        'Me.cbocliente.SelectedValue = 0
    End Sub
 
    Protected Sub ExportToExcel(ByVal sender As Object, ByVal e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Dim GridView1 As New GridView
        GridView1.DataSource = dtsMonitor
        GridView1.DataBind()
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            GridView1.AllowPaging = False
            ' Me.BindGrid()

            GridView1.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In GridView1.HeaderRow.Cells
                cell.BackColor = GridView1.HeaderStyle.BackColor
            Next
            For Each row As GridViewRow In GridView1.Rows
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = GridView1.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            GridView1.RenderControl(hw)
            'style to format numbers to string
            Dim style As String = "<style> .textmode { } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()
        End Using
    End Sub


    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If Me.IsPostBack = False Then
            '      Call mostrarcliente()
        End If
    End Sub
    Private Sub Listar()
        cn.abrirconexion()
        'Dim s As String = Request.QueryString("name_startsWith")
        dtsMonitor = cn.TraerDataTable("tes_rendiciones_exporta", "", "")
        cn.cerrarconexion()

    End Sub

    Private Sub getRenciones()
        cn.abrirconexion()
        'Dim s As String = Request.QueryString("name_startsWith")
        dtsMonitor = cn.TraerDataTable("tes_rendiciones_exporta", "", "")
        cn.cerrarconexion()

    End Sub

    Protected Sub cmdexportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexportar.Click
        Listar()
        ExportToExcel(sender, e)
    End Sub

    Protected Sub lstinformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("ondblclick", "MostrarDetalleRendicion('" & e.Row.Cells(0).Text.ToString & "')")
            e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")
            e.Row.Attributes.Add("onmouseout", "resaltar(this,0)")
            'e.Row.Attributes.Add("onclick", "window.open('frmadjuntararchivo.aspx?codigo_dren=1','as','toolbar=no')")
        End If
    End Sub


    Protected Sub chktodos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chktodos.CheckedChanged
        'Me.cbocliente.Enabled = Not Me.chktodos.Checked
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then

            cn.abrirconexion()
            dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
            cn.cerrarconexion()
        End If
    End Sub

    Protected Sub cmdbuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdbuscar.Click
        cn.abrirconexion()
        If Me.chktodos.Checked = False Then
            dtscliente = cn.consultar("spDocumentosEgresoRendir", "5", Me.cboestado.Text, txtCliente.Text, "", "", "")
        Else
            dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Me.cboestado.Text, "", "", "", "")
        End If
        cn.cerrarconexion()
    End Sub

End Class

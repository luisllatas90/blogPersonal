﻿
Partial Class Biblioteca_LibrosnoInventariados
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBIBUSAT").ConnectionString)
            ClsFunciones.LlenarListas(Me.CboEstado, obj.TraerDataTable("BI_ConsultarEstadoMaterial", "TO", "", ""), "idestado", "descripcionestado")
            ClsFunciones.LlenarListas(Me.cboinventarios, obj.TraerDataTable("INV_ConsultarInventarios"), "codInv", "DescripcionInv")
            Me.TxtFechafin.Text = Now.ToShortDateString
            Me.CboEstado.SelectedValue = 13
        End If
    End Sub

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBIBUSAT").ConnectionString)
        Dim Dtb_Libros As New Data.DataTable
        Dtb_Libros = obj.TraerDataTable("INV_ReporteInventario", Me.RblVer.SelectedValue, 0, cboinventarios.selectedvalue, Me.CboEstado.SelectedValue)
        If Dtb_Libros.Rows.Count > 0 Then
            Me.GvInventario.DataSource = Dtb_Libros
            Me.GvInventario.DataBind()
            With GvInventario
                .HeaderRow.Cells(0).Text = "Título"
                .HeaderRow.Cells(1).Text = "Nro. Ingreso"
                .HeaderRow.Cells(2).Text = "Dewey"
                .HeaderRow.Cells(3).Text = "Autor"
                .HeaderRow.Cells(4).Text = "Editorial"
                .HeaderRow.Cells(5).Text = "Edición"
                .HeaderRow.Cells(6).Text = "País"
                .HeaderRow.Cells(7).Text = "Fecha de Pub."
                .HeaderRow.Cells(8).Text = "Solicitante"
                .HeaderRow.Cells(9).Text = "Ubicación"
                .HeaderRow.Cells(10).Text = "Area"
                .HeaderRow.Cells(11).Text = "Estado"
            End With
        Else
            GvInventario.DataSource = Nothing
            GvInventario.DataBind()
        End If

    End Sub

    Protected Sub CmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdExportar.Click
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment;filename=reporte.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        Response.Write(HTML())
        Response.End()
    End Sub
    Public Function HTML() As String
        Dim page1 As New Page
        Dim form1 As New HtmlForm
        Dim Builder1 As New System.Text.StringBuilder
        Dim writer1 As New System.IO.StringWriter(Builder1)
        Dim writer2 As New HtmlTextWriter(writer1)

        Me.GvInventario.EnableViewState = False
        page1.EnableViewState = False
        page1.EnableEventValidation = False

        page1.Controls.Add(form1)
        'form1.Controls.Add(Me.LblTitulo)
        writer1.Write("<strong>" & Me.LblTitulo.Text & " " & Me.RblVer.SelectedItem.Text.ToUpper & " CON ESTADO " & Me.CboEstado.SelectedItem.Text.ToUpper & " A LA FECHA " & Me.TxtFechafin.Text & "</strong><br><br>")

        form1.Controls.Add(Me.GvInventario)

        page1.RenderControl(writer2)
        writer2.Write("<font face='verdana' size='2' color='#121212'><br><br><strong>Sistema de Biblioteca - Desarrollo de Sistemas <br>Actualizado al: " & Date.Now() & "<strong></font>")
        page1.Dispose()
        page1 = Nothing
        Return Builder1.ToString
    End Function
End Class

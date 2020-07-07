
Partial Class frmlstquintaespecial
    Inherits System.Web.UI.Page

    Dim cn As New clsaccesodatos

    Sub mostrarInformePrograma()
        Dim dts As New System.Data.DataSet
        cn.abrirconexion()
        dts = cn.consultar("dbo.ConsultarInformePrograma", "6", Me.cboprograma.SelectedValue, "", "", "", "", "", "")
        cn.cerrarconexion()

        Me.lstinformacion.DataSource = dts.Tables("consulta")
        Me.lstinformacion.DataBind()
        
    End Sub

    Sub mostrarprograma()
        Dim dtsPrograma As New System.Data.DataSet
        cn.abrirconexion()
        dtsPrograma = cn.consultar("dbo.spConsultarPrograma", "3", Session("codigo_TCL"), "", "", "")
        cn.cerrarconexion()
        Me.cboprograma.DataTextField = "descripcion_pro"
        Me.cboprograma.DataValueField = "codigo_pro"
        Me.cboprograma.DataSource = dtsPrograma.Tables("consulta")
        Me.cboprograma.DataBind()
        Me.cboprograma.SelectedValue = Session("codigo_pro")

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim codigo_per As String, dtscliente As New System.Data.DataSet

        If Me.IsPostBack = False Then
            codigo_per = 74 'Me.Request.QueryString("idper")

            cn.abrirconexion()
            dtscliente = cn.consultar("dbo.sp_buscacliente", "1", codigo_per, "PE", "", "", "", "", "", "")
            cn.cerrarconexion()
            Session("codigo_tcl") = dtscliente.Tables("consulta").Rows(0).Item("codigo_tcl")
            Me.lblusuario.Text = dtscliente.Tables("consulta").Rows(0).Item("nombres")
            Me.lblusuario.ToolTip = dtscliente.Tables("consulta").Rows(0).Item("nombres")
            Me.mostrarprograma()
            Me.mostrarInformePrograma()

        End If
    End Sub

    Protected Sub lstinformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles lstinformacion.RowDataBound
        ' columna 8 para la imagen
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Cells(7).Text = "Ver detalles"
            'Dim hl As New HyperLink()
            'hl.NavigateUrl = ""
            'hl.ImageUrl = "~/iconos/buscar.gif"
            'e.Row.Cells(7).Controls.Add(hl)
            'e.Row.Cells(7).Text = "~/imagenes/buscar.gif"
            e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")
            e.Row.Attributes.Add("onmouseleave", "resaltar(this,0)")
            e.Row.Cells(11).Attributes.Add("onclick", "Acciones('accion=Anularinformeprograma&codigo=" & e.Row.Cells(0).Text & "')")
            e.Row.Cells(10).Attributes.Add("onclick", "window.open ('frmquintaespecial.aspx?codigo_ipr=" & e.Row.Cells(0).Text & "','','toolbar=no,width=1100,height=780');")
        End If
    End Sub

    Protected Sub lstinformacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstinformacion.SelectedIndexChanged

    End Sub

    Protected Sub cmdagregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdagregar.Click

    End Sub

    Protected Sub cboprograma_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboprograma.SelectedIndexChanged
        mostrarInformePrograma()
        Session("codigo_pro") = Me.cboprograma.SelectedValue
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click


        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.lstinformacion.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.lstinformacion)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=InformesTesoreria.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class

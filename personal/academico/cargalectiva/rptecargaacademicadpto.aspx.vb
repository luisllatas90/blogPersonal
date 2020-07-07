﻿
Partial Class rptecargaacademicadpto
    Inherits System.Web.UI.Page
    Dim codigo_per As Int32 = -1
    Dim contador As Int16 = 0
    Dim PrimeraFila As Int16 = -1
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwCargaAcademica.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim CeldasCombinadas As Int16 = 1
            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this)")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this)")

            contador = contador + 1
            'Combinar celdas
            If codigo_per = fila("codigo_per") Then
                e.Row.Cells(0).Text = ""
                e.Row.Cells(1).Text = ""
                'e.Row.Cells(2).Text = ""
                contador = contador - 1
                'e.Row.Cells(0).RowSpan = contador
            Else
                e.Row.Cells(0).CssClass = "bordesup" 'Autor
                e.Row.Cells(1).CssClass = "bordesup" 'Autor
                'e.Row.Cells(2).CssClass = "bordesup" 'Autor
                e.Row.VerticalAlign = VerticalAlign.Middle
                codigo_per = fila("codigo_per").ToString()
                PrimeraFila = e.Row.RowIndex
                'e.Row.Cells(0).Text = contador
            End If
            ''Asignar linea separadora
            e.Row.Cells(2).CssClass = "bordesup" 'Autor
            e.Row.Cells(3).CssClass = "bordesup" 'Autor
            e.Row.Cells(4).CssClass = "bordesup" 'Estado
            e.Row.Cells(4).CssClass = "bordesup"
            e.Row.Cells(5).CssClass = "bordesup"
            e.Row.Cells(6).CssClass = "bordesup"
            e.Row.Cells(7).CssClass = "bordesup"
            e.Row.Cells(8).CssClass = "bordesup"
            e.Row.Cells(9).CssClass = "bordesup"
            e.Row.Cells(10).CssClass = "bordesup"
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Dim obj As New clsaccesodatos 'New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            obj.abrirconexion()
            ClsFunciones.LlenarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0), "codigo_cac", "descripcion_cac")
            obj.cerrarconexion()
            obj = Nothing
        End If
    End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        If Me.dpCodigo_cac.SelectedValue <> -1 Then
            Dim obj As New clsaccesodatos 'ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            obj.abrirconexion()
            Me.grwCargaAcademica.DataSource = obj.TraerDataTable("CAR_ConsultarCargaAcademicaParaRectorado", Me.dpFiltro.SelectedValue, Me.dpCodigo_cac.SelectedValue, 0, 0)
            Me.grwCargaAcademica.DataBind()
            obj.cerrarconexion()
            obj = Nothing
            Me.grwCargaAcademica.Visible = True
            Me.lblmensaje.Text = contador & " cursos programados."
        Else
            Me.lblmensaje.Text = "0 Carga Académica registrada."
            Me.grwCargaAcademica.Visible = False
        End If
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.grwCargaAcademica.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.grwCargaAcademica)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=InformeDeCargaAcademicaPorDpto" & Me.dpCodigo_cac.SelectedItem.Text & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class

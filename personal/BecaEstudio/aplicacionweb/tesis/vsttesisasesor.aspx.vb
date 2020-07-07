﻿
Partial Class personal_academico_tesis_vsttesisasesor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Dim tbCicloAcademico As Data.DataTable 'Agregado por mvillavicencio 21/11/2011

            'Agregado por mvillavicencio para cargar el ciclo académico 21/11/2011
            tbCicloAcademico = obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0)
            ClsFunciones.LlenarListas(Me.ddlCiclo, tbCicloAcademico, "codigo_cac", "descripcion_cac")

            ClsFunciones.LlenarListas(Me.dpDpto, obj.TraerDataTable("TES_ConsultarAsesoriaTesis", 0, Request.QueryString("id"), 0, 0, 0), "codigo_dac", "nombre_dac", ">>Seleccione el Departamento Académico<<")
            '## comentado por mvillavicencio 21/11/2011 ##
            '*'agregado            
            'ClsFunciones.LlenarListas(Me.dpCiclo, obj.TraerDataTable("TES_ConsultarAsesoriaTesis", 6, 0, 0, 0, 0), "ciclo", "ciclo", ">>Seleccione el año<<")
            '*'
            '###########

            Session("codigo_usu2") = Request.QueryString("id")
        End If
    End Sub
    Protected Sub DataList1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataList1.ItemDataBound
        Dim gr As GridView
        gr = CType(e.Item.FindControl("grdTesis"), GridView)

        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)

        'gr.DataSource = obj.TraerDataTable("TES_ConsultarAsesoriaTesis", 2, Me.DataList1.DataKeys(e.Item.ItemIndex), 0, 0, 0)
        'gr.DataSource = obj.TraerDataTable("TES_ConsultarAsesoriaTesis", 8, Me.DataList1.DataKeys(e.Item.ItemIndex), Me.dpciclo.selectedvalue, 0, 0)

        'Modificado por mvillavicencio 21/11/2011 Filtra por ciclo     
        gr.DataSource = obj.TraerDataTable("TES_ConsultarAsesoriaTesis", 11, Me.DataList1.DataKeys(e.Item.ItemIndex), Me.ddlciclo.selectedvalue, 0, 0)

        gr.DataBind()
        obj = Nothing
    End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdBuscar.Click
        Me.DataList1.Visible = False
        Me.GridView1.Visible = False
        Me.cmdExportar.Visible = False

        If Me.dpDpto.SelectedValue > 0 Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)

            If Me.dpVista.SelectedValue = 0 Then

                'Me.DataList1.DataSource = obj.TraerDataTable("TES_ConsultarAsesoriaTesis", 1, Me.dpDpto.SelectedValue, 0, 0, 0)                
                'Me.DataList1.DataSource = obj.TraerDataTable("TES_ConsultarAsesoriaTesis", 7, Me.dpDpto.SelectedValue, Me.dpCiclo.selectedValue, 0, 0)

                '##### modificado por mvillavicencio 21/11/2011. Filtra por ciclo academico, no por año #########
                Me.DataList1.DataSource = obj.TraerDataTable("TES_ConsultarAsesoriaTesis", 10, Me.dpDpto.SelectedValue, Me.ddlCiclo.selectedValue, 0, 0)
                '#######################################################

                Me.DataList1.DataBind()
                Me.DataList1.Visible = True

            Else
                'Me.GridView1.DataSource = obj.TraerDataTable("TES_ConsultarAsesoriaTesis", 3, Me.dpDpto.SelectedValue, 0, 0, 0)
                'Me.GridView1.DataSource = obj.TraerDataTable("TES_ConsultarAsesoriaTesis", 9, Me.dpDpto.SelectedValue, Me.dpciclo.selectedValue, 0, 0)

                '##### modificado por mvillavicencio 21/11/2011. Filtra por ciclo academico, no por año #########
                Me.GridView1.DataSource = obj.TraerDataTable("TES_ConsultarAsesoriaTesis", 12, Me.dpDpto.SelectedValue, Me.ddlciclo.selectedValue, 0, 0)
                '#######################################################

                Me.GridView1.DataBind()
                Me.GridView1.Visible = True
                Me.cmdExportar.Visible = True
            End If
            obj = Nothing
        End If
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.GridView1.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.GridView1)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=listaasesores.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    
End Class

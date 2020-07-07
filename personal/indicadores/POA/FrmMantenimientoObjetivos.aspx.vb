
Partial Class indicadores_POA_PROTOTIPOS_FrmMantenimientoObjetivos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            If Request.QueryString("tipo") = "Nuevo" Then
                Call wf_nuevo()

                ddl_Actividad.Visible = True
                txt_actividad.Visible = False

                Dim li_codigoPlan As Integer = Request.QueryString("codigo_plan")
                Call wf_cargarActividadesPOA(li_codigoPlan)
            Else
                ddl_Actividad.Visible = False
                txt_actividad.Visible = True
                txt_actividad.ReadOnly = True

                ddl_Actividad.Enabled = False
                Dim obj As New clsPlanOperativoAnual
                Dim dtt As New Data.DataTable
                Dim li_codigo As Integer = Request.QueryString("codigo_pobj")
                dtt = obj.POA_BuscarObjetivosPOA(li_codigo)

                If dtt.Rows.Count > 0 Then
                    txt_descripcion.Text = dtt.Rows(0).Item(1)
                    txt_meta1.Text = dtt.Rows(0).Item(2)
                    txt_meta2.Text = dtt.Rows(0).Item(3)
                    txt_meta3.Text = dtt.Rows(0).Item(4)
                    txt_meta4.Text = dtt.Rows(0).Item(5)
                    txt_actividad.Text = dtt.Rows(0).Item(7)
                Else

                End If
            End If
            txt_descripcion.Focus()
        End If
    End Sub

    Sub wf_cargarActividadesPOA(ByVal codigo_plan As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.POA_ListarActividadesObjetivos(codigo_plan)
        Me.ddl_Actividad.DataSource = dtt
        Me.ddl_Actividad.DataTextField = "descripcion"
        Me.ddl_Actividad.DataValueField = "codigo"
        Me.ddl_Actividad.DataBind()

        dtt.Dispose()
        obj = Nothing
    End Sub

    Sub wf_nuevo()
        txt_descripcion.Text = ""
        txt_meta1.Text = 0.0
        txt_meta2.Text = 0.0
        txt_meta3.Text = 0.0
        txt_meta4.Text = 0.0
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Response.Redirect("FrmListaObjetivos.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&plan=" & Request.QueryString("plan") & _
                  "&ejercicio=" & Request.QueryString("ejercicio") & "&tipoActividad=" & Request.QueryString("tipoActividad"))

    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim li_codigo_acp As Integer = 0
            Dim li_codigo_pobj As Integer = Request.QueryString("codigo_pobj")

            If Request.QueryString("tipo") = "Editar" Then
                li_codigo_acp = Request.QueryString("codigo_acp")
            Else
                li_codigo_acp = ddl_Actividad.SelectedItem.Value
            End If

            '' '' ''If Request.QueryString("tipo") = "Editar" Then
            '' '' ''    obj.ActualizaObjetivosPOA(li_user, li_codigo_pobj)
            '' '' ''End If

            Dim dtt As New Data.DataTable
            'obj.InsertarObjetivosPOA(li_codigo_pobj, txt_descripcion.Text, usuario, "1", li_codigo_acp)
            dtt = obj.InsertarObjetivosPOA(li_codigo_pobj, txt_descripcion.Text, Request.QueryString("id"), 1, li_codigo_acp)

            If IIf(txt_meta1.Text = 0, "", "T1") <> "" Then obj.InsertarMetasObjetivosPOA(txt_meta1.Text, IIf(txt_meta1.Text = 0, "", "T1"), Request.QueryString("id"), dtt.Rows(0).Item(0))
            If IIf(txt_meta2.Text = 0, "", "T2") <> "" Then obj.InsertarMetasObjetivosPOA(txt_meta2.Text, IIf(txt_meta2.Text = 0, "", "T2"), Request.QueryString("id"), dtt.Rows(0).Item(0))
            If IIf(txt_meta3.Text = 0, "", "T3") <> "" Then obj.InsertarMetasObjetivosPOA(txt_meta3.Text, IIf(txt_meta3.Text = 0, "", "T3"), Request.QueryString("id"), dtt.Rows(0).Item(0))
            If IIf(txt_meta4.Text = 0, "", "T4") <> "" Then obj.InsertarMetasObjetivosPOA(txt_meta4.Text, IIf(txt_meta4.Text = 0, "", "T4"), Request.QueryString("id"), dtt.Rows(0).Item(0))

            Response.Redirect("FrmListaObjetivos.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&plan=" & Request.QueryString("plan") & _
                              "&ejercicio=" & Request.QueryString("ejercicio") & "&tipoActividad=" & Request.QueryString("tipoActividad"))

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class

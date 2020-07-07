
Partial Class frmbuscarresponsabletesis
    Inherits System.Web.UI.Page
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Tbl As New Data.DataTable
        Dim termino As String
        termino = LCase(Me.txtTermino.Text.Trim)
        termino = Replace(termino, "á", "a")
        termino = Replace(termino, "é", "e")
        termino = Replace(termino, "í", "i")
        termino = Replace(termino, "ó", "o")
        termino = Replace(termino, "ú", "u")

        If Request.QueryString("codigo_tpi") = 3 Then
            Tbl = obj.TraerDataTable("TES_ConsultarResponsableTesis", 0, termino, Request.QueryString("codigo_tes"), 0)
            Me.GridView1.DataSource = Tbl
            Me.GridView1.DataBind()
            Me.GridView1.Visible = True
        Else
            Me.DataList1.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis", 1, termino, Request.QueryString("codigo_tes"), 0)
            Me.DataList1.DataBind()
            Me.DataList1.Visible = True
        End If

        obj = Nothing

    End Sub
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim fila As Integer = Convert.ToInt32(e.CommandArgument)
        GuardarDatos(GridView1.DataKeys(fila).Value)

    End Sub
    Protected Sub DataList1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles DataList1.ItemCommand
        GuardarDatos(CInt(DataList1.DataKeys(e.Item.ItemIndex)))
    End Sub
    Private Sub GuardarDatos(ByVal autor As Integer)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim codigo_tes As Integer
        Try
            obj.IniciarTransaccion()

            If Request.QueryString("accion") = "A" Then
                codigo_tes = obj.Ejecutar("TES_AgregarTesis", 11, DBNull.Value, Session("titulo_tes"), Session("problema_tes"), Session("resumen_tes"), CDate(Session("fechainicio_tes")), CDate(Session("fechafin_tes")), Session("codigo_Eti"), 1, Request.QueryString("id"), 0)
            Else
                obj.Ejecutar("TES_ModificarTesis", Request.QueryString("codigo_tes"), Session("titulo_tes"), Session("problema_tes"), Session("resumen_tes"), CDate(Session("fechainicio_tes")), CDate(Session("fechafin_tes")), Request.QueryString("id"))
                codigo_tes = Request.QueryString("codigo_tes")
                'Response.Write(Request.QueryString("codigo_tes") & "<br>" & Session("titulo_tes") & "<br>" & Session("problema_tes") & "<br>" & Session("resumen_tes") & "<br>" & CDate(Session("fechainicio_tes")) & "<br>" & CDate(Session("fechafin_tes")) & "<br>" & Session("codigo_eti") & "<br>" & Request.QueryString("id"))
            End If

            obj.Ejecutar("TES_AgregarResponsableTesis", Request.QueryString("codigo_tpi"), autor, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, codigo_tes, Request.QueryString("id"), "")

            obj.TerminarTransaccion()

            Response.Redirect("frmtesis.aspx?codigo_tes=" & codigo_tes & "&accion=M&id=" & Request.QueryString("id"))
        Catch ex As Exception
            obj.AbortarTransaccion()
            Me.lblError.Text = "Ha ocurrido un error en la transacción <br>" & ex.Message
            obj = Nothing
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.txtTermino.Focus()
        'Me.txtTermino.Attributes.Add("onkeyup", "if(event.keyCode==13){document.all.form1.submit()}")
        If Request.QueryString("codigo_tpi") = 3 Then
            Me.lblTitulo.Text = "Asignar autor(es)"
        Else
            Me.lblTitulo.Text = "Asignar asesor(es)"
        End If
    End Sub

    Protected Sub CmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdCancelar.Click
        Response.Redirect("frmtesis.aspx?accion=" & Request.QueryString("accion") & "&codigo_tes=" & Request.QueryString("codigo_tes") & "&id=" & Request.QueryString("id"))
    End Sub
End Class

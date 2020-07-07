Partial Class Equipo_Planificar
    Inherits System.Web.UI.Page

    Protected Sub FormView1_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdateEventArgs) Handles FormView1.ItemUpdating
        'Response.Write(e.OldValues.Count)
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            'ObjCnx.IniciarTransaccion()
            e.Cancel = True
            With e.NewValues
                If Request.QueryString("tipo").ToString = "s" Then
                    ObjCnx.Ejecutar("paReq_InsertarCronograma", .Item(5), .Item(6), .Item(7), CInt(Request.QueryString("id_sol")), Request.QueryString("tipo").ToString)
                Else
                    ObjCnx.Ejecutar("paReq_InsertarCronograma", .Item(5), .Item(6), .Item(7), CInt(Request.QueryString("id_act")), Request.QueryString("tipo").ToString)
                End If
            End With
            'ObjCnx.TerminarTransaccion()
            Response.Write("<script>alert('Se registraron los datos correctamente'); window.opener.location.reload();window.close();</script>")
            'Response.Write("<script>alert('Se grabaron los datos de manera satisfactoria')</script>")
        Catch ex As Exception
            'ObjCnx.AbortarTransaccion()
            Response.Write(ex.Message)
            'Response.Write("<script>alert('Ocurrió un error al grabar los datos')</script>")
        End Try
        ObjCnx = Nothing
        'For i As Int16 = 5 To 7
        '    Response.Write(e.NewValues.Item(i) & "<br>")
        'Next
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("tipo") = "" Or Request.QueryString("tipo") Is Nothing Then
                Session("tipo") = "s"
            Else
                Session("tipo") = Request.QueryString("tipo")
            End If
            If Request.QueryString("Pag").ToString = "C" Then
                Me.LblTitulo.Text = "ASIGNAR FECHAS"
            Else
                Me.LblTitulo.Text = "REPROGRAMAR FECHAS"
            End If
        End If
    End Sub
End Class

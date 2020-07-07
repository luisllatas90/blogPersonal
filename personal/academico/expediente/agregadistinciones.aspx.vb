
Partial Class agregadistinciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.TxtFecha.Attributes.Add("OnKeyDown", "javascript:return false;")
            Dim ObjCombos As New Combos
            ObjCombos.LlenaTipoDistincion(Me.DDLDistinciones)

            If Request.QueryString("codigo_dis") <> "" Then
                Dim datos As System.Data.DataTable
                Dim ObjDistin As New Personal
                datos = ObjDistin.ObtieneDistinciones(Request.QueryString("codigo_dis"), "MO")
                With datos.Rows(0)
                    Me.TxtDistincion.Text = .Item("nombre_dis")
                    Me.TxtOtorgado.Text = .Item("otorgado_dis")
                    Me.TxtCiudad.Text = .Item("ciudad_dis")
                    Me.TxtDescripcion.Text = .Item("motivo_dis")
                    Me.TxtFecha.Text = .Item("fechaentrega_dis")
                    Me.DDLDistinciones.SelectedValue = .Item("codigo_tdis")
                End With
                ObjDistin = Nothing
                datos = Nothing
            End If
            ObjCombos = Nothing
        End If
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim ObjPersonal As New Personal
        ObjPersonal.codigo = Session("Id")
        Dim valor As Integer
        If Request.QueryString("codigo_dis") = "" Then
            valor = ObjPersonal.GrabarDistinciones(Me.TxtDistincion.Text, Me.TxtOtorgado.Text, Me.TxtCiudad.Text, Me.TxtDescripcion.Text, Me.DDLDistinciones.SelectedValue, CType(Me.TxtFecha.Text, DateTime))
        Else
            valor = ObjPersonal.ModificarDistinciones(Me.TxtDistincion.Text, Me.TxtOtorgado.Text, Me.TxtCiudad.Text, Me.TxtDescripcion.Text, Me.DDLDistinciones.SelectedValue, CType(Me.TxtFecha.Text, DateTime), Request.QueryString("codigo_dis"))
        End If


        If valor = -1 Then
            Me.LblError.Text = "Ocurrio un error al grabar los datos, intentelo nuevamente"
        Else
            Response.Write("<script>window.opener.location.href='distinciones.aspx?id=" & Session("id") & "'; window.close();</script>")
        End If
        ObjPersonal = Nothing
    End Sub
End Class

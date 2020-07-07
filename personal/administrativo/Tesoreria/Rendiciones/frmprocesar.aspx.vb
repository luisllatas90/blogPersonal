
Partial Class frmprocesar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim cn As New clsaccesodatos, codigo_rend As Integer

        Dim rpta As Integer, mensaje As String = "", operacion As String, codigo_dren As String
        Dim idPersonal As Integer = 0

        operacion = Me.Request.QueryString("operacion").ToString
        If isnumeric(Me.Request.QueryString("id")) Then idPersonal = Me.Request.QueryString("id")
        'Response.Write(idPersonal)
        Select Case operacion

            Case ""

                Dim dtsdetalleegreso As New System.Data.DataSet, numero As String = ""
                codigo_rend = Me.Request.QueryString("codigo_rend")
                cn.abrirconexiontrans()
                cn.ejecutar("sp_cerrarrendicion", False, rpta, mensaje, idPersonal, codigo_rend, 0, "")
                cn.cerrarconexiontrans()
                If IsNumeric(codigo_rend) Then
                    cn.abrirconexion()
                    dtsdetalleegreso = cn.consultar("dbo.sp_verdocumentoemitidos", "VDREND", codigo_rend, "", "", "", "")
                    cn.cerrarconexion()
                    numero = dtsdetalleegreso.Tables("consulta").Rows(0).Item("numeracionanual_rend").ToString
                End If


                If rpta > 0 Then
                    'Response.Write("<script>alert ('Se ha finalizado con la rendicion de viaticos" & codigo_rend.ToString & "');window.opener.location.href=window.opener.location;window.close();</script>")
                    Response.Write("<script>alert ('Se ha finalizado con la rendicion de viaticos N° " & numero & "');window.opener.location.href=window.opener.location;window.close();</script>")

                Else
                    Response.Write("<script>alert ('" & mensaje & ", Finalizar la operación corresponde al área de Tesorería.\nAcercarse con la documentación física para finalizar su rendición.');window.close();</script>")
                End If
            Case "quitardetallerendicion"
                codigo_dren = Me.Request.QueryString("codigo_dren")
                'Dim rpta As Integer, mensaje As String = ""

                cn.abrirconexiontrans()
                cn.ejecutar("sp_quitardetallerendicion", False, rpta, mensaje, codigo_dren, 0, "")
                cn.cerrarconexiontrans()
                Response.Write("<script>alert ('" & mensaje & "');history.Back();</script>")

            Case "reactivar"
                codigo_rend = Me.Request.QueryString("codigo_rend")
                'Dim rpta As Integer, mensaje As String = ""

                cn.abrirconexiontrans()
                cn.ejecutar("sp_reactivarrendicion", False, rpta, mensaje, codigo_rend, 0, "")
                cn.cerrarconexiontrans()
                Response.Write("<script>alert ('Se ha reactivado la rendición');history.Back();</script>")

        End Select
    End Sub
End Class

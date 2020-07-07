
Partial Class administrativo_activofijo_listacaracteristicas
    Inherits System.Web.UI.Page
    Dim cod_user_i As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Ver el tema de autentificación
            If Not Me.Page.User.Identity.IsAuthenticated Then
                Response.Redirect("~/Default.aspx")
                Exit Sub
            Else
                If (Request.QueryString("id") <> "") Then
                    cod_user_i = Request.QueryString("id")
                    Me.hdUser.Value = cod_user_i

                End If
                ListarCaracteristicas()
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub

    Private Sub ListarCaracteristicas()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim strBody As New StringBuilder

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("AF_listarCaracteristicaAF")
            obj.CerrarConexion()
            obj = Nothing

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    If (dt IsNot Nothing) Then
                        For i As Integer = 0 To dt.Rows.Count - 1

                            strBody.Append("<tr id='" & dt.Rows(i).Item("id") & "'>")
                            strBody.Append("<td ><a href='#' class='btn btn-green btn-xs' onclick='fnEditar(" & dt.Rows(i).Item("id") & ", """ & dt.Rows(i).Item("caracteristica") & """, """ & dt.Rows(i).Item("estado") & """)' ><i class='ion-edit'></i></a>")
                            strBody.Append("<a href='#' class='btn btn-red btn-xs' onclick='fnBorrar(" & dt.Rows(i).Item("id") & ")' ><i class='ion-android-cancel'></i></a></td>")
                            strBody.Append("<td>" & i + 1 & "</td>")
                            strBody.Append("<td>" & dt.Rows(i).Item("caracteristica") & "</td>")
                            strBody.Append("<td>" & dt.Rows(i).Item("estado") & "</td>")
                            strBody.Append("</tr>")
                        Next
                    End If
                    Me.pCaracteristica.InnerHtml = strBody.ToString
                End With
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub

End Class

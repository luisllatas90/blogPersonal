'Imports iTextSharp.text.pdf
'Imports iTextSharp.text

Partial Class administrativo_activofijo_trasladoMercaderia_v2
    Inherits System.Web.UI.Page
    Dim cod_user_i As Integer
    Dim cod_ctf As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("~/Default.aspx")
                Exit Sub
            Else
                If (Request.QueryString("id") <> "") Then
                    Dim dt As New Data.DataTable
                    Dim obj As New ClsConectarDatos
                    cod_user_i = Request.QueryString("id")
                    Me.hdUser.Value = cod_user_i
                    cod_ctf = Request.QueryString("ctf")
                    Me.hdPerfil.Value = cod_ctf
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    obj.AbrirConexion()
                    dt = obj.TraerDataTable("SEG_ListaPersonal", cod_user_i)
                    If dt.Rows.Count > 0 Then
                        Me.hdNombreUser.Value = dt.Rows(0).Item("trabajador").ToString()
                        Me.hdCodCcoUser.Value = dt.Rows(0).Item("codigo_Cco")
                        Me.hdNomCcoUser.Value = dt.Rows(0).Item("descripcion_Cco").ToString()
                        Me.hdEmailUser.Value = dt.Rows(0).Item("usuario_per").ToString + "@usat.edu.pe"
                    End If
                    obj.CerrarConexion()
                    obj = Nothing
                End If

                'ListarTraslados()
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub

    Private Sub ListarTraslados()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim strBody As New StringBuilder
            Dim tipo As String = ""

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("AF_ListarTrasladosAF")
            obj.CerrarConexion()
            obj = Nothing

            Dim ctf As Integer
            ctf = CInt(Request.QueryString("ctf"))

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    If (dt IsNot Nothing) Then
                        For i As Integer = 0 To dt.Rows.Count - 1

                            strBody.Append("<tr id='" & dt.Rows(i).Item("c_id") & "'>")
                            strBody.Append("<td >")
                            If ctf = 1 Then
                                strBody.Append("<a href='#' class='btn btn-orange btn-xs' onclick='fnEditar(" & dt.Rows(i).Item("c_id") & ", """ & dt.Rows(i).Item("d_tip") & """, """ & dt.Rows(i).Item("c_per") & """, """ & dt.Rows(i).Item("d_nom") & """, """ & dt.Rows(i).Item("c_cco") & """, """ & dt.Rows(i).Item("d_cco") & """, """ & dt.Rows(i).Item("d_fec") & """, """ & dt.Rows(i).Item("d_obs") & """, """ & dt.Rows(i).Item("d_est") & """)' ><i class='ion-edit'></i></a>")
                            End If
                            strBody.Append("<a href='#' class='btn btn-red btn-xs' onclick='fnBorrar(" & dt.Rows(i).Item("c_id") & ")' ><i class='ion-android-cancel'></i></a>")
                            strBody.Append("</td>")
                            strBody.Append("<td>" & i + 1 & "</td>")
                            If (dt.Rows(i).Item("d_tip") = "I") Then
                                tipo = "INTERNA"
                            Else
                                tipo = "EXTERNA"
                            End If
                            strBody.Append("<td>" & tipo & "</td>")
                            strBody.Append("<td>" & dt.Rows(i).Item("d_nom") & "</td>")
                            strBody.Append("<td>" & dt.Rows(i).Item("d_cco") & "</td>")
                            strBody.Append("<td>" & dt.Rows(i).Item("d_fec") & "</td>")
                            strBody.Append("<td>" & dt.Rows(i).Item("d_est") & "</td>")
                            strBody.Append("</tr>")
                        Next
                    End If
                    Me.pTraslado.InnerHtml = strBody.ToString
                End With
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub

End Class

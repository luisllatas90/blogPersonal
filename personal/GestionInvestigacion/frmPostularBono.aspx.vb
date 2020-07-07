﻿
Partial Class GestionInvestigacion_frmPostularBono
    Inherits System.Web.UI.Page
    Dim cod_user_i As Integer
    Dim cod_user_s As String
    Dim cod_ctf As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If

        Dim obj As New ClsGestionInvestigacion
        Try
            ''Ver el tema de autentificación
            'If Not Me.Page.User.Identity.IsAuthenticated Then
            '    Response.Redirect("~/Default.aspx")
            '    Exit Sub
            'Else

            If (Request.QueryString("id") <> "") Then
                cod_user_s = obj.EncrytedString64(Request.QueryString("id"))
                cod_user_i = Request.QueryString("id")
                Me.hdUser.Value = cod_user_s
                cod_ctf = Request.QueryString("ctf")
                Me.hdCtf.Value = cod_ctf
                Me.hdId.Value = cod_user_i
                'Informacion()
                ListarBasesDatosRevista()
            End If
            'End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub

    Private Sub Informacion()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim dt1 As New Data.DataTable
            Dim dt2 As New Data.DataTable
            Dim strBody As New StringBuilder

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt1 = obj.TraerDataTable("INV_listaInvestigadores", cod_user_i, "PE")
            obj.CerrarConexion()

            If Not dt1 Is Nothing AndAlso dt1.Rows.Count > 0 Then
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dt = obj.TraerDataTable("INV_ListarBonoPublicacion", cod_user_i, "PE")
                obj.CerrarConexion()

                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    With dt.Rows(0)
                        If (dt IsNot Nothing) Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                strBody.Append("<tr id='" & dt.Rows(i).Item("c_bon") & "'>")
                                strBody.Append("<td>" & i + 1 & "</td>")
                                strBody.Append("<td>" & dt.Rows(i).Item("d_tit") & "</td>")
                                strBody.Append("<td>" & dt.Rows(i).Item("f_tit") & "</td>")
                                'strBody.Append("<td>" & dt.Rows(i).Item("r_bon") & "</td>")
                                strBody.Append("<td>" & dt.Rows(i).Item("con_bon") & "</td>")
                                strBody.Append("<td>" & dt.Rows(i).Item("fin_bon") & "</td>")
                                strBody.Append("</tr>")
                            Next
                        End If
                        Me.pBonoPublicacion.InnerHtml = strBody.ToString
                    End With
                End If
            Else

                strBody.Append("<center>Debe Registrarse como colaborar con actividad investigadora</center>")
                Me.ContenidoMensajeValidaInv.InnerHtml = strBody.ToString

            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub


    Private Sub ListarBasesDatosRevista()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("INV_ConsultarBaseDatosRevista")
            obj.CerrarConexion()

            'Dim str As String = ""
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboBDRevista.Items.Add(New ListItem(dt.Rows(i).Item("descripcion_bdr"), dt.Rows(i).Item("codigo_bdr")))
                    Me.cboBDRevistaE.Items.Add(New ListItem(dt.Rows(i).Item("descripcion_bdr"), dt.Rows(i).Item("codigo_bdr")))
                    'str = str + "<option value='" + dt.Rows(i).Item("codigo_bdr") + "'>" + dt.Rows(i).Item("descripcion_bdr") + "</option>"
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

End Class
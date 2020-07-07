
Partial Class GestionInvestigacion_frmRegistroInvestigadores
    Inherits System.Web.UI.Page
    Dim cod_user_i As Integer
    Dim cod_user_s As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsGestionInvestigacion
        Try
            'Ver el tema de autentificación
            'If Not Me.Page.User.Identity.IsAuthenticated Then
            '    Response.Redirect("~/Default.aspx")
            '    Exit Sub
            'Else
            If (Request.QueryString("id") <> "") Then
                cod_user_s = obj.EncrytedString64(Request.QueryString("id"))
                cod_user_i = Request.QueryString("id")
                Me.hdUser.Value = cod_user_s
                Me.hdCod.Value = cod_user_i

                Informacion()
                'ListarLineas()
                'End If
            End If

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
            Dim dt3 As New Data.DataTable
            Dim dt4 As New Data.DataTable
            Dim strBody As New StringBuilder
            Dim param1 As String = Request.QueryString("id")

            Dim obj1 As New ClsGestionInvestigacion

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("spPla_ConsultarDatosPersonal", "TO", param1)
            obj.CerrarConexion()

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    If (dt IsNot Nothing) Then
                        Me.txtNombre.Value = dt.Rows(0).Item("apellidoPat_Per").ToString + " " + dt.Rows(0).Item("apellidoMat_Per").ToString + ", " + dt.Rows(0).Item("nombres_Per").ToString
                        Me.divTipoDoc.InnerHtml = dt.Rows(0).Item("tipoDocIdentidad_Per").ToString
                        Me.txtDNI.Value = dt.Rows(0).Item("nroDocIdentidad_Per").ToString

                        obj.AbrirConexion()
                        dt1 = obj.TraerDataTable("ConsultarTipoPersonal", "TO", "")
                        obj.CerrarConexion()

                        Dim correo As String = dt.Rows(0).Item("email_Per").ToString

                        '' Para Pruebas
                        'Dim correo As String = "pruebas00@mailinator.com"
                        'Dim URL As String = "https://sandbox.orcid.org/oauth/authorize?client_id=APP-1F3MW3HOFU4NP0H3&response_type=code&scope=/authenticate&"

                        Dim URL As String = "https://orcid.org/oauth/authorize?client_id=APP-ID59GM8T9DT29K07&response_type=code&scope=/authenticate&" _
                                            + "family_names=" + StrConv(dt.Rows(0).Item("apellidoPat_Per").ToString + " " + dt.Rows(0).Item("apellidoMat_Per").ToString, VbStrConv.ProperCase) _
                                            + "&given_names=" + StrConv(dt.Rows(0).Item("nombres_Per").ToString, VbStrConv.ProperCase) _
                                            + "&email=" + correo _
                                            + "&lang=es" _
                                            + "&show_login=true" _
                                            + "&redirect_uri=https://orcid.org/my-orcid"
                        '' Para Pruebas
                        '+ "&redirect_uri=http://serverdev/campusvirtual/personal/GestionInvestigacion/frmRegistroInvestigadores.aspx"




                        Me.btnORCID.Attributes.Add("onclick", "CerrarSesionORCID();window.open('" + URL + "', '_blank', 'toolbar=no, scrollbars=yes, width=500, height=700, top=100, left=500'); return false;")


                        If Not dt1 Is Nothing AndAlso dt1.Rows.Count > 0 Then
                            With dt1.Rows(0)
                                If (dt1 IsNot Nothing) Then
                                    For i As Integer = 0 To dt1.Rows.Count - 1
                                        If (dt1.Rows(i).Item("codigo_Tpe").ToString = dt.Rows(0).Item("codigo_tpe").ToString) Then
                                            If (dt.Rows(0).Item("codigo_tpe").ToString = 1) Then
                                                Me.divAreaFacultad.InnerHtml = "Facultad"
                                                Me.txtTrabajador.Value = dt1.Rows(i).Item("descripcion_Tpe").ToString
                                                obj.AbrirConexion()
                                                dt3 = obj.TraerDataTable("INV_consultarDocente", param1, "DO")
                                                obj.CerrarConexion()
                                                Me.txtEspecialidad.Value = dt3.Rows(0).Item("nombre_Fac").ToString
                                            Else
                                                Me.divAreaFacultad.InnerHtml = "Área"
                                                Me.txtTrabajador.Value = dt1.Rows(i).Item("descripcion_Tpe").ToString
                                                obj.AbrirConexion()
                                                dt3 = obj.TraerDataTable("INV_consultarDocente", param1, "AR")
                                                obj.CerrarConexion()
                                                Me.txtEspecialidad.Value = dt3.Rows(0).Item("descripcion_Cco").ToString
                                            End If
                                        End If
                                    Next
                                End If
                            End With
                        End If

                        'exec spPla_ConsultarDatosPersonal 'XC',684

                        obj.AbrirConexion()
                        dt2 = obj.TraerDataTable("INV_listaInvestigadores", cod_user_i, "PE")
                        obj.CerrarConexion()


                        If Not dt2 Is Nothing AndAlso dt2.Rows.Count > 0 Then
                            With dt2.Rows(0)
                                If (dt2 IsNot Nothing) Then

                                    Me.txtURLDina.Value = dt2.Rows(0).Item("d_url").ToString
                                    Me.hdRevision.Value = dt2.Rows(0).Item("d_rev").ToString
                                    Me.hdDina.Value = dt2.Rows(0).Item("dina").ToString
                                    Me.hdRegina.Value = dt2.Rows(0).Item("regina").ToString
                                    Me.txtOrcid.Value = dt2.Rows(0).Item("orcid").ToString

                                    'Agregar por HCano
                                    Me.cboLinea.Items.Add(obj1.EncrytedString64(dt2.Rows(0).Item("cod_linea").ToString))
                                    Me.cboLinea.Value = obj1.EncrytedString64(dt2.Rows(0).Item("cod_linea").ToString)


                                End If
                            End With
                        Else
                            Me.txtURLDina.Value = ""
                            Me.hdRevision.Value = ""
                        End If

                        obj.AbrirConexion()
                        dt4 = obj.TraerDataTable("INV_facultadDepartamentoLinea", cod_user_i)
                        obj.CerrarConexion()


                        If Not dt4 Is Nothing AndAlso dt4.Rows.Count > 0 Then
                            With dt4.Rows(0)
                                If (dt4 IsNot Nothing) Then
                                    Me.txtDepartamento.Value = dt4.Rows(0).Item("nombre_dac").ToString
                                    'Me.txtLinea.Value = dt4.Rows(0).Item("nombre_lin").ToString
                                End If
                            End With
                        End If

                        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                        obj.AbrirConexion()
                        dt2 = obj.TraerDataTable("INV_listarGrupoInvestigador", param1)
                        obj.CerrarConexion()
                        Dim nombre_grupo As String = ""
                        Dim contador As Integer = 0
                        If Not dt2 Is Nothing AndAlso dt2.Rows.Count > 0 Then
                            With dt2.Rows(0)
                                If (dt2 IsNot Nothing) Then
                                    For i As Integer = 0 To dt2.Rows.Count - 1
                                        If (nombre_grupo <> dt2.Rows(i).Item("d_gru")) Then
                                            contador = contador + 1
                                            strBody.Append("<tr id='" & dt2.Rows(i).Item("c_gru") & "'>")
                                            strBody.Append("<td style='text-align:center'>" & contador & "</td>")
                                            strBody.Append("<td style='text-align:left'>" & dt2.Rows(i).Item("d_gru") & "</td>")
                                            strBody.Append("<td style='text-align:left'>" & dt2.Rows(i).Item("d_lin") & "</td>")
                                            strBody.Append("<td style='text-align:left'>" & dt2.Rows(i).Item("d_nom") & "</td>")
                                            strBody.Append("<td style='text-align:center'>" & dt2.Rows(i).Item("d_iea") & "</td>")
                                            strBody.Append("</tr>")
                                            nombre_grupo = dt2.Rows(i).Item("d_gru")
                                        End If
                                    Next
                                End If
                                Me.pGrupoInvestigacion.InnerHtml = strBody.ToString
                            End With
                        End If

                    End If
                End With
            End If



        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub

    'Private Sub ListarLineas()
    '    Dim obj As New ClsGestionInvestigacion
    '    Dim dt As New Data.DataTable
    '    dt = obj.ListaLineasInvestigacion("%")

    '    'Me.cboLinea.DataSource = dt
    '    Me.cboLinea.DataValueField = "codigo"
    '    Me.cboLinea.DataTextField = "nombre"

    '    '        Me.cboLinea.Items.Add(New ListItem("-- Seleccione --", ""))
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        Me.cboLinea.Items.Add(New ListItem(dt.Rows(i).Item("nombre").ToString, dt.Rows(i).Item("codigo").ToString))
    '    Next
    '    'Me.cboLinea.DataBind()
    'End Sub



End Class
Partial Class frmBecaPostular
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim dt2 As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("Beca_ConsultarCronogramaSolicitud")
            obj.CerrarConexion()
            If dt.Rows.Count Then
                If dt.Rows(0).Item("estado") = "D" Then
                    Session("BecaDisponible") = "SI"
                    mensajecro.Visible = False
                    Me.Label1.Text = "-El envío de Solicitudes de Becas se encuentra disponible desde el <b>" & dt.Rows(0).Item("desde").ToString & "</b> hasta <b>" & dt.Rows(0).Item("hasta").ToString & "</b>"
                    dt = Nothing
                    'Restricción para medicina
                    obj.AbrirConexion()
                    dt = obj.TraerDataTable("Beca_VerificarEstudiante", CInt(Session("codigo_alu")))
                    obj.CerrarConexion()
                    'If dt.Rows.Count = 0 Then
                    Try
                        obj.AbrirConexion()
                        dt2 = obj.TraerDataTable("Beca_ConsultarRequisitoAlumno", CInt(Session("codigo_alu")))
                        obj.CerrarConexion()
                    Catch ex As Exception
                        ' Response.Redirect("frmBecaPostular.aspx")
                        Response.Write(ex.Message & ex.StackTrace)
                    End Try

                    cargarRequisitos(dt2)
                    'Registrar(Beca)
                    If Request.Form("enviar") <> "" Then
                        CargarEnvio()
                    End If

                    'e
                    'Me.panelRequisitos.Visible = True
                    'Me.tablaRequisitos.Visible = False
                    ' End If

                Else
                    Me.mensaje.Visible = False
                    Me.mensajecro.Attributes("class") = "warning"
                    mensajecro.InnerHtml = "<b>El envío de Solicitudes de Becas estará disponible desde el <u>" & dt.Rows(0).Item("desde") & "</u> al <u>" & dt.Rows(0).Item("hasta") & "</u>. <b>"
                    Me.tablaRequisitos.Visible = False
                    CargarEnvio()
                    Session("BecaDisponible") = "NO"
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            'Response.Redirect("frmBecaPostular.aspx")
            Response.Write(ex.Message & ex.StackTrace)
        End Try

    End Sub
    Sub cargarRequisitos(ByVal dt As Data.DataTable)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim htmlTb As String = ""
        Dim img As String = "", textinfo As String = ""
        Dim pass As Integer = 1
        Dim f As Byte = 0
        If dt.Rows.Count Then
            For i As Integer = 0 To dt.Rows.Count - 1
                textinfo = "title="
                htmlTb = htmlTb & "<tr class=""row-" & f.ToString & """> "
                htmlTb = htmlTb & "<td class=""cell cell2"">" & dt.Rows(i).Item("descripcion_req") & "</td>"
                htmlTb = htmlTb & "<td class=""cell cell3"">" & IIf(dt.Rows(i).Item("valord_bsr").ToString = "1", "Sí", dt.Rows(i).Item("valord_bsr").ToString) & "</td>"
                htmlTb = htmlTb & "<td class=""cell cell4"">" & dt.Rows(i).Item("valora_bsr").ToString & "</td>"
                img = IIf(dt.Rows(i).Item("cumple_bsr").ToString = "1", "check.png", "exclamation.png")
                textinfo = textinfo & IIf(dt.Rows(i).Item("cumple_bsr").ToString = "1", "'Cumple'", "'No cumple'")
                htmlTb = htmlTb & "<td class=""cell cell5""><img style=""cursor:hand"" src=""images/" & img & """" & textinfo & """</td>"
                htmlTb = htmlTb & "</tr>"
                pass = pass * dt.Rows(i).Item("cumple_bsr")
            Next
            tb.InnerHtml = htmlTb
        End If

        pass = 1    'para pruebas
        If pass Then
            obj.AbrirConexion()
            dt = obj.TraerDataTable("Beca_ConsultarSolicitud", CInt(Session("codigo_alu")))
            obj.CerrarConexion()
            If dt.Rows.Count Then
                CargarEnvio()
            Else
                btn.InnerHtml = "<input " & Session("chk") & " class=""input-button add"" type=""submit"" value=""Solicitar Beca"" onclick=""javascript:confirmar();""/><input type=""hidden"" value=""postular"" name=""enviar"" />"
            End If
        Else
            Me.mensaje.Attributes("class") = "warning"
            mensaje.InnerHtml = "<i><br/>Lo sentimos, no cumple con los requisitos mínimos para solicitar beca de estudios.</i>"
        End If
    End Sub
    Sub CargarEnvio()
        If Session("BecaDisponible") = "SI" Then
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim dtDeuda As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("Beca_RegistrarSolicitud", CInt(Session("codigo_Alu")))
            obj.CerrarConexion()
            Dim f As Byte = 0
            Dim htmlTb As String = ""
            Dim header As String = ""
            Dim img As String = ""
            If dt.Rows.Count Then
                Dim UltimoDiaDelMes As Date
                'UltimoDiaDelMes = DateSerial(Year(Date.Now), Month(Date.Now) + 1, 0)
                UltimoDiaDelMes = DateSerial(Year(Date.Now), Month(Date.Now), 30)
                obj.AbrirConexion()
                dtDeuda = obj.TraerDataTable("Beca_VerificarDeuda", CInt(Session("codigo_Alu")))
                obj.CerrarConexion()
                ' If dtDeuda.Rows.Count = 0 Then
                obj.AbrirConexion()
                obj.Ejecutar("EVE_AgregarDeuda", "E", CInt(Session("codigo_alu")), 577, "Solicitud de Beca Campus Virtual", 5, "S", UltimoDiaDelMes, 638, 1, Date.Now, dt.Rows(0).Item("codigo_pso"), 0, 0, 0, 0)
                obj.CerrarConexion()
                'End If
                htmlTb = ""
                Me.panelEnvio.Visible = "false"
                htmlTb = htmlTb & "<tr class=""row-" & f.ToString & """> "
                htmlTb = htmlTb & "<td class=""cell cell2"">" & dt.Rows(0).Item("codigoUniver_Alu") & "</td>"
                htmlTb = htmlTb & "<td class=""cell cell3"">" & dt.Rows(0).Item("Estudiante") & "</td>"
                htmlTb = htmlTb & "<td class=""cell cell4"">" & dt.Rows(0).Item("Ciclo").ToString & "</td>"
                htmlTb = htmlTb & "<td class=""cell cell5"">" & dt.Rows(0).Item("fechaEnvio_bso").ToString & "</td>"
                htmlTb = htmlTb & "<td class=""cell cell5"">" & dt.Rows(0).Item("estado_bso").ToString & "</td>"
                tbEnviado.InnerHtml = htmlTb
                Select Case dt.Rows(0).Item("estado_bso").ToString
                    Case "enviado"
                        Me.mensaje.Attributes("class") = "info"
                        mensaje.InnerHtml = "La solicitud ha sido enviada. Al finalizar el proceso de recepción de solicitudes, se informará el resultado por este medio."
                    Case "rechazado"
                        header = "RESULTADO:"
                        Me.mensaje.Attributes("class") = "warning"
                        mensaje.InnerHtml = "Lo sentimos, en esta ocasión no has alcanzado ninguna de nuestras Becas. Te animamos a seguir esforzándote para una siguiente oportunidad.<br/>"
                    Case "aceptado"
                        header = "RESULTADO:"
                        Me.mensaje.Attributes("class") = "success"
                        mensaje.InnerHtml = "La USAT reconoce tus resultados académicos y te otorga este BENEFICIO por : <b>" & dt.Rows(0).Item("descripcion_bec").ToString & "</b>, esperando que continúes obteniendo éxitos."
                End Select
                mensaje.InnerHtml = "<b>" & header & "</b><br/>" & mensaje.InnerHtml & "<br /><br /><b>La Comisión de Becas.</b>"
                btn.InnerHtml = ""
            End If
        End If
    End Sub
End Class


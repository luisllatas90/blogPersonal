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
                mensaje.InnerHtml = "Disponible desde <b>" & dt.Rows(0).Item("desde").ToString & "</b> hasta <b>" & dt.Rows(0).Item("hasta").ToString & "</b>"
            Else
            End If
            dt = Nothing

            'Restricción para medicina
            obj.AbrirConexion()
            dt = obj.TraerDataTable("Beca_VerificarEstudiante", CInt(Session("codigo_alu")))
            obj.CerrarConexion()
            If dt.Rows.Count = 0 Then
                Try
                    obj.AbrirConexion()
                    dt2 = obj.TraerDataTable("Beca_ConsultarRequisitoAlumno", CInt(Session("codigo_alu")))
                    obj.CerrarConexion()
                Catch ex As Exception
                    Response.Redirect("frmBecaPostular.aspx")
                End Try
                cargarRequisitos(dt2)
                'Registrar(Beca)
                If Request.Form("enviar") <> "" Then
                    CargarEnvio()
                End If
            Else
                Me.panelRequisitos.Visible = True
                Me.tablaRequisitos.Visible = False                
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
    Sub cargarRequisitos(ByVal dt As Data.DataTable)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim htmlTb As String = ""
        Dim img As String = ""
        Dim pass As Integer = 1
        Dim f As Byte = 0
        If dt.Rows.Count Then
            For i As Integer = 0 To dt.Rows.Count - 1
                htmlTb = htmlTb & "<tr class=""row-" & f.ToString & """> "
                htmlTb = htmlTb & "<td class=""cell cell2"">" & dt.Rows(i).Item("descripcion_req") & "</td>"
                htmlTb = htmlTb & "<td class=""cell cell3"">" & IIf(dt.Rows(i).Item("valord_bsr").ToString = "1", "Sí", dt.Rows(i).Item("valord_bsr").ToString) & "</td>"
                htmlTb = htmlTb & "<td class=""cell cell4"">" & dt.Rows(i).Item("valora_bsr").ToString & "</td>"
                img = IIf(dt.Rows(i).Item("cumple_bsr").ToString = "1", "check.png", "exclamation.png")
                htmlTb = htmlTb & "<td class=""cell cell5""><img src=""images/" & img & """</td>"
                htmlTb = htmlTb & "</tr>"
                pass = pass * dt.Rows(i).Item("cumple_bsr")
            Next
            tb.InnerHtml = htmlTb
        End If

        'pass = 1
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
            mensaje.InnerHtml = "<i><br/>Lo sentimos, no cumple con los requisitos mínimos para solicitar beca de estudios.</i>"
        End If
    End Sub
    Sub CargarEnvio()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim dtDeuda As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("Beca_RegistrarSolicitud", CInt(Session("codigo_Alu")))
        obj.CerrarConexion()
        Dim f As Byte = 0
        Dim htmlTb As String = ""
        Dim img As String = ""
        If dt.Rows.Count Then
            Dim UltimoDiaDelMes As Date
            UltimoDiaDelMes = DateSerial(Year(Date.Now), Month(Date.Now) + 1, 0)
            obj.AbrirConexion()
            dtDeuda = obj.TraerDataTable("Beca_VerificarDeuda", CInt(Session("codigo_Alu")))
            If dtDeuda.Rows.Count = 0 Then
                obj.Ejecutar("EVE_AgregarDeuda", "E", CInt(Session("codigo_alu")), 577, "Solicitud de Beca", 3, "S", UltimoDiaDelMes, 638, 1, Date.Now, dt.Rows(0).Item("codigo_pso"), 0, 0, 0, 0)
            End If
            obj.CerrarConexion()
            htmlTb = ""
            Me.panelEnvio.Visible = "true"
            htmlTb = htmlTb & "<tr class=""row-" & f.ToString & """> "
            htmlTb = htmlTb & "<td class=""cell cell2"">" & dt.Rows(0).Item("codigoUniver_Alu") & "</td>"
            'htmlTb = htmlTb & "<td class=""cell cell3"">" & dt.Rows(0).Item("Estudiante") & "</td>"
            htmlTb = htmlTb & "<td class=""cell cell4"">" & dt.Rows(0).Item("Ciclo").ToString & "</td>"
            htmlTb = htmlTb & "<td class=""cell cell5"">" & dt.Rows(0).Item("fechaEnvio_bso").ToString & "</td>"
            htmlTb = htmlTb & "<td class=""cell cell5"">" & dt.Rows(0).Item("estado_bso").ToString & "</td>"
            tbEnviado.InnerHtml = htmlTb
            Select Case dt.Rows(0).Item("estado_bso").ToString
                Case "enviado"
                    mensaje.InnerHtml = "<i>Se ha enviado tu solicitud</i>"
                Case "rechazado"
                    mensaje.InnerHtml = "<i>Lo sentimos, en esta ocasión no has alcanzado ninguna de nuestras Becas. Te animamos a seguir esforzándote para una siguiente oportunidad.<br/></i>"
                Case "aceptado"
                    mensaje.InnerHtml = "<i>La USAT reconoce tus resultados académicos y te otorga esta Beca de Estudios, esperando que continúes obteniendo éxitos.</i>"
            End Select
            mensaje.InnerHtml = "<b>Resultado: </b><br/>" & mensaje.InnerHtml & "<br /><br /><b>La Comisión de Becas.</b>"
            btn.InnerHtml = ""
        End If
    End Sub
End Class


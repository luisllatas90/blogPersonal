
Partial Class GestionInvestigacion_FrmAjaxTablaPostResultadoFinal
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsConectarDatos
        Dim obj1 As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        Dim dt1 As New Data.DataTable
        Dim dt2 As New Data.DataTable
        Dim dt3 As New Data.DataTable
        Try
            'Ver el tema de autentificación
            'If Not Me.Page.User.Identity.IsAuthenticated Then
            '    Response.Redirect("~/Default.aspx")
            '    Exit Sub
            'Else
            'Me.txtURLDina.Value = Request.Form("param1")
            Dim strBody As New StringBuilder

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt2 = obj.TraerDataTable("INV_listaResultadoFinalConcurso", Request.Form("param1"))
            obj.CerrarConexion()
            Dim nombre_grupo As Integer = 0
            Dim contador As Integer = 0
            Dim cant_eva As Integer = 0
            Dim cod_eva As Integer = 0
            Dim mostrar As String = ""
            Dim NF As String = 0
            ' session dt2
            Session("lstPostFinal") = dt2
            If Not dt2 Is Nothing AndAlso dt2.Rows.Count > 0 Then
                With dt2.Rows(0)
                    If (dt2 IsNot Nothing) Then
                        For i As Integer = 0 To dt2.Rows.Count - 1
                            If (nombre_grupo <> dt2.Rows(i).Item("codigo_pos")) Then
                                contador = contador + 1
                                strBody.Append("<tr id='" & dt2.Rows(i).Item("codigo_pos") & "," & contador & "'>")
                                strBody.Append("<td style='text-align:center;width:5%;'>" & contador & "</td>")
                                If (dt2.Rows(i).Item("ganador") = 1) Then
                                    strBody.Append("<td style='text-align:center;width:45%;color:green;font-weight: bold;'>" & dt2.Rows(i).Item("titulo_pos") & " (GANADOR) </td>")
                                Else
                                    strBody.Append("<td style='text-align:center;width:45%;'>" & dt2.Rows(i).Item("titulo_pos") & "</td>")
                                End If
                                strBody.Append("<td style='text-align:center;width:20%'>" & dt2.Rows(i).Item("responsable") & "</td>")
                                If dt2.Rows(i).Item("calificacionglobal_pos") Is DBNull.Value Then
                                    NF = 0
                                    mostrar = "disabled"
                                Else
                                    If (dt2.Rows(i).Item("calificacionglobal_pos").ToString <> "0.00") Then
                                        mostrar = ""
                                        NF = dt2.Rows(i).Item("calificacionglobal_pos").ToString
                                    Else
                                        mostrar = "disabled"
                                        NF = 0
                                    End If
                                End If
                                strBody.Append("<td style='text-align:center;width:10%;'><input name='txtNotaGlobal[" + dt2.Rows(i).Item("codigo_pos").ToString + "]' type='text' id='txtNotaGlobal[" + dt2.Rows(i).Item("codigo_pos").ToString + "]' value='" + NF + "' class='form-control' disabled/></td>")
                                If (dt2.Rows(i).Item("ganador") = 0) Then
                                    Dim encriptado As String
                                    encriptado = obj1.EncrytedString64(dt2.Rows(i).Item("codigo_pos").ToString)
                                    strBody.Append("<td style='text-align:center;width:10%;'><button type='button' id='btnEG' name='btnEG' class='btn btn-sm btn-green' onclick='fnAgregarEve(" & dt2.Rows(i).Item("codigo_pos").ToString & ")' title='Agregar Evaluador Externo' ><i class='ion-android-person-add'></i></button></td>")
                                Else
                                    strBody.Append("<td style='text-align:center;width:10%;'><button type='button' id='btnEG' name='btnEG' class='btn btn-sm btn-green' onclick='' title='Agregar Evaluador Externo'><i class='ion-android-person-add'></i></button></td>")
                                End If
                                If (dt2.Rows(i).Item("ganador") = 0) Then
                                    strBody.Append("<td style='text-align:center;width:10%;'><button type='button' id='btnEG' name='btnEG' class='btn btn-sm btn-orange btn-block' onclick='fnGanador(" + dt2.Rows(i).Item("codigo_pos").ToString + ")' title='Ediaatar' ><i class='ion-trophy'></i></button></td>")
                                Else
                                    If (dt2.Rows(i).Item("ganador") = 1) Then
                                        strBody.Append("<td style='text-align:center;width:10%;'><button type='button' id='btnEG' name='btnEG' class='btn btn-sm btn-green btn-block' data-container='body' data-toggle='popover' data-trigger='hover' data-placement='left' data-content='¡.: Registro Ganador :.!' data-original-title='INFORMACION GANADOR'><i class='ion-trophy'></i></button></td>")
                                    Else
                                        strBody.Append("<td style='text-align:center;width:10%;'><button type='button' id='btnEG' name='btnEG' class='btn btn-sm btn-info btn-block' data-container='body' data-toggle='popover' data-trigger='hover'  data-placement='left' data-content='Puesto Ocupado: " + dt2.Rows(i).Item("ganador").ToString + "°' data-original-title='INFORMACION PARTICIPACIÓN'><i class='ion-trophy'></i></button></td>")
                                    End If
                                End If

                                strBody.Append("</tr>")
                                nombre_grupo = dt2.Rows(i).Item("codigo_pos")
                            End If
                        Next
                    End If
                    Me.tbPostulacion.InnerHtml = strBody.ToString
                End With
            End If

            'End If
            'End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub
End Class
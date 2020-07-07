
Partial Class indicadores_POA_Procesar
    Inherits System.Web.UI.Page

    Public Function ToJSON(ByVal dato As String) As String
        Dim jsonSerializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Return jsonSerializer.Serialize(dato)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        Dim codigo_pei,codigo_ejp,codigo_poa As Integer
        Dim estado As String
        codigo_pei = Request("param0")
        codigo_ejp = Request("param1")
        codigo_poa = Request("param2")
        estado = Request("param3")
        dt = obj.ListaAsignaAporte(codigo_pei, codigo_ejp, codigo_poa, estado)
        Dim pei_ant As Integer = 0
        Dim ind_ant As Integer = 0
        Dim poa_ant As Integer = 0
        Dim rowspan As Integer = 0
        Dim tabla As String = ""
        Dim nro As Integer = 0
        'tabla = tabla & "<thead><tr><th>Plan Estrátegico</th><th>indicador</th><th>Plan Operativo</th><th>Aporte</th><th>programa/Proyecto</th><th>Aporte</th></tr></thead>"
        For i As Integer = 0 To dt.Rows.Count - 1
            rowspan = rowspan + 1
            If pei_ant <> dt.Rows(i).Item("codigo_pla").ToString Then
                If i <> 0 Then
                    tabla = tabla & "<tr><td style='text-align:right' colspan='5'><input type='button' class='btnGuardar' value='  Guardar' onclick='Guardar(" & nro & ")' /></td></tr>"
                    tabla = tabla & "</tbody></table>"
                    'tabla = tabla & "</div></div></div></div>"
                    nro = nro + 1
                End If
                'tabla = tabla & "<div class='panel panel-primary'>"
                'tabla = tabla & "<div class='panel-heading'><b>" & dt.Rows(i).Item("Periodo_pla").ToString & " </b></div>"
                'tabla = tabla & "<div class='panel-body'>"
                'tabla = tabla & "<div class='panel panel-default'>"
                tabla = tabla & "<table width='100%' class='table table-condensed'>"
                tabla = tabla & "<thead >"
                tabla = tabla & "<tr><th colspan='5'>" & dt.Rows(i).Item("Periodo_pla").ToString & " </th></tr>"
                tabla = tabla & "<tr><th width='5%'>TOTAL</th><th width='30%'>INDICADOR</th><th width='5%'>APORTE</th><th width='30%'>POA</th><th width='35%'>PROGRAMA/PROYECTO</th></tr>"
                tabla = tabla & "</thead>"
                tabla = tabla & "<tbody>"
                tabla = tabla & "<tr><td><input type='text'  class='form-control input sm' style='width:70px; font-size:10px; font-weight:bold' id='TotalInd" & nro & "' readonly='readonly' /></td><td>" & dt.Rows(i).Item("descripcion_ind").ToString & "</td>"
                tabla = tabla & "<td><input type='text' nro=" & nro & " class='form-control sm' style='width:70px;font-size:10px;' onchange='Sumar(" & nro & ")' /></td>"
                tabla = tabla & "<td>" & dt.Rows(i).Item("nombre_poa").ToString & "</td>"
                tabla = tabla & "<td>" & dt.Rows(i).Item("resumen_acp").ToString & "</td></tr>"
                'tabla = tabla & "<td>" & dt.Rows(i).Item("resumen_acp").ToString & "</td>"
                'tabla = tabla & "<td><input type='text' class='form-control sm' style='width:60px; font-size:10px' id='AporteActividad" & i & "' /></td></tr>"
            Else
                '"<a href='#' id='" & dt.Rows(i).Item("codigo_ind").ToString & "' ><b>+&nbsp;</b></a>"
                tabla = tabla & "<tr>"
                If ind_ant = dt.Rows(i).Item("codigo_ind") Then
                    tabla = tabla & "<td></td><td></td>"
                    If poa_ant = dt.Rows(i).Item("codigo_poa") Then
                        tabla = tabla & "<td></td><td></td>"
                    Else
                        tabla = tabla & "<td> <input type='text' nro=" & nro & " class='form-control input sm' style='width:70px; font-size:10px; ' onchange='Sumar(" & nro & ")' /></td>"
                        tabla = tabla & "<td class='table-active'>" & dt.Rows(i).Item("nombre_poa").ToString & "</td>"
                    End If
                    tabla = tabla & "<td>" & dt.Rows(i).Item("resumen_acp").ToString & "</td>"
                Else
                    tabla = tabla & "<td style='text-align:right' colspan='5'><input type='button' class='btnGuardar' value='  Guardar' onclick='Guardar(" & nro & ")'  /> </td></tr>"
                    nro = nro + 1
                    tabla = tabla & "<td><input type='text' class='form-control input sm' style='width:70px; font-size:10px; font-weight:bold' id='TotalInd" & nro & "' readonly='readonly' /></td><td>" & dt.Rows(i).Item("descripcion_ind").ToString & "</td>"
                    tabla = tabla & "<td><input type='text' nro=" & nro & " class='form-control input sm' style='width:70px;' onchange='Sumar(" & nro & ")' /></td>"
                    tabla = tabla & "<td>" & dt.Rows(i).Item("nombre_poa").ToString & "</td>"
                    tabla = tabla & "<td>" & dt.Rows(i).Item("resumen_acp").ToString & "</td>"
                End If
                tabla = tabla & "</tr>"

                If dt.Rows.Count - 1 = i Then
                    tabla = tabla & "<tr><td style='text-align:right' colspan='5'><input type='button' class='btnGuardar' value='  Guardar' onclick='Guardar(" & nro & ")'/> </td></tr>"
                    tabla = tabla & "</tbody></table>"
                    'tabla = tabla & "</div></div></div></div>"
                End If
                'tabla = tabla & "<td>" & dt.Rows(i).Item("resumen_acp").ToString & "</td>"
                'tabla = tabla & "<td><input type='text' class='form-control sm' style='width:60px; font-size:10px' id='AporteActividad" & i & "' /></td></tr>"
            End If
            pei_ant = dt.Rows(i).Item("codigo_pla")
            ind_ant = dt.Rows(i).Item("codigo_ind")
            poa_ant = dt.Rows(i).Item("codigo_poa")
        Next
        tabla = tabla & "</tbody></table>"
        'tabla = tabla & "</div></div><br>"
        Response.Write(ToJSON(tabla))
    End Sub

End Class

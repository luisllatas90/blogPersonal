
Partial Class academico_horarios_vsthorariodocentedisponibilidad
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If IsPostBack = False Then
                Dim mod_ As Int16 = Request.QueryString("modo")
                Dim codigo_tfu As Int16 = Request.QueryString("ctf")
                Dim codigo_usu As Integer = Request.QueryString("id")

                If mod_ = 4 Then
                    codigo_tfu = 0
                End If

                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                ClsFunciones.LlenarListas(Me.ddlDepartamento, obj.TraerDataTable("ACAD_DepartamentoPersonalFuncion", "", codigo_usu, codigo_tfu), "codigo_Dac", "descripcion_dac")

                ClsFunciones.LlenarListas(Me.ddlCiclo, obj.TraerDataTable("ListaCicloAcademico"), "codigo_cac", "descripcion_cac")

                obj.CerrarConexion()
                fnLoading(True)

                fnCboPersonal()


                'Dim titulo As String
                'Dim tipo As Integer

                'Dim modo As String = Request.QueryString("modo")
                'Dim codigo_per As Integer = Request.QueryString("codigo_per")
                'Dim codigo_cac As Integer = Request.QueryString("codigo_cac")
                'titulo = Request.QueryString("titulo")
                'Dim descripcion_cac As String = Request.QueryString("descripcion_cac")








            End If
        Catch ex As Exception
            Response.Write(ex.Message & "  -- " & ex.StackTrace & "  -- " & ex.Source)
        End Try
    End Sub
    Private Function AnchoHora(ByVal cad As String) As String
        If Len(cad) < 2 Then
            AnchoHora = "0" & cad
        Else
            AnchoHora = cad
        End If
    End Function
    Private Sub fnCboPersonal()
        Try
            'Response.Write(Me.ddlCiclo.SelectedValue)
            'Response.Write(Me.ddlDepartamento.SelectedValue)
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.ddlDocente, obj.TraerDataTable("ACAD_DisponibilidadPersonal_Consultar", "", Me.ddlCiclo.SelectedValue, Me.ddlDepartamento.SelectedValue), "codigo_Per", "Personal")
            obj.CerrarConexion()

        Catch ex As Exception

        End Try

    End Sub
    Private Sub fnLoading(ByVal sw As Boolean)
        If sw Then
            ' Response.Write(1 & "<br>")
            Me.loading.Attributes.Remove("class")
            Me.loading.Attributes.Add("class", "hidden")
        Else
            ' Response.Write(0 & "<br>")
            Me.loading.Attributes.Remove("class")
            Me.loading.Attributes.Add("class", "")
            ' Me.loading.Attributes.Add("class", "show")
        End If
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click

        fnConsultar()
    End Sub

    Private Sub fnConsultar()
        Try
            ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "DivLoad", "fnDivLoad('report',1000);", True)
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ' tb = obj.TraerDataTable("acad_consolidadoxescuela_padres", "P", Me.ddlCiclo.selectedvalue, Me.ddlPlan.SelectedValue)

            tb = obj.TraerDataTable("ConsultarHorariosAmbiente", "21", ddlCiclo.SelectedValue, ddlDocente.SelectedValue, "", "")


            obj.CerrarConexion()
            obj = Nothing

            Dim dia, hora, diaBD, inicioBD, finBD, TextoCelda, marcas As String
            Dim tabla As New StringBuilder

            Dim f, c As Integer
            Dim i As Integer = 0
            dia = ""
            TextoCelda = ""
            If tb.Rows.Count > 0 Then

                marcas = 0
                tabla.Append("<table id='tblHorario' style='border-collapse: collapse;' width='100%'  border='1' bgcolor='white' bordercolor='#CCCCCC'>")
                tabla.Append("<tr class='etiquetaTabla' height='25px'>")
                tabla.Append("<th width='15%'>Horas</th>")
                tabla.Append("<th width='15%'>Lunes</th>")
                tabla.Append("<th width='15%'>Martes</th>")
                tabla.Append("<th width='15%'>Miércoles</th>")
                tabla.Append("<th width='15%'>Jueves</th>")
                tabla.Append("<th width='15%'>Viernes</th>")
                tabla.Append("<th width='15%'>Sábado</th>")
                tabla.Append("</tr>")

                For f = 1 To 16 'For para crear las filas con los <tr>
                    '   Response.Write("F" & f & "  ")
                    tabla.Append("<tr>")
                    For c = 0 To 6    'For para crear las columnas 
                      '  Response.Write("C" & c & "  ")
                        If c = 0 Then 'Crea la primera columna de la tabla, para mostrar el rango de horas.	
                            '============================================================================================================================================
                            'response.write  "<td width='15%' class='etiquetaTabla'>" & f+6 & ":10 - " & f+1+6 & ":00</td>"  'linea anterior con los 10 min
                            tabla.Append("<td width='15%' class='etiquetaTabla'>" & f + 6 & ":00 - " & f + 1 + 6 & ":00</td>")   'linea con horas exactas 08/11/2011
                            '============================================================================================================================================
                        Else

                            If c = 1 Then dia = "LU"
                            If c = 2 Then dia = "MA"
                            If c = 3 Then dia = "MI"
                            If c = 4 Then dia = "JU"
                            If c = 5 Then dia = "VI"
                            If c = 6 Then dia = "SA"

                            hora = AnchoHora(f + 6)
                            TextoCelda = ""
                            TextoCelda = vbTab & "<td>" & vbCrLf  'Crea la celda
                            For i = 0 To tb.Rows.Count - 1

                                diaBD = Mid(tb.Rows(i).Item("dia"), 1, 2)
                                inicioBD = Mid(tb.Rows(i).Item("horaInicio"), 1, 2)
                                finBD = Mid(tb.Rows(i).Item("horaFin"), 1, 2)

                                If Trim(dia) = Trim(diaBD) And Int(hora) >= Int(inicioBD) And Int(hora) < Int(finBD) Then
                                    marcas = marcas + 1
                                    ' Response.Write("[" & marcas & "]")
                                    TextoCelda = TextoCelda & tb.Rows(i).Item("estado")
                                    'If tb.Rows(i).Item("puede") = "S" Then
                                    '    TextoCelda = TextoCelda & tb.Rows(i).Item("estado") & "<br><font color='blue'>" & tb.Rows(i).Item("estado") & "</font><br><br>"
                                    'Else
                                    '    TextoCelda = TextoCelda & tb.Rows(i).Item("estado") & "<br><font color='red'>" & tb.Rows(i).Item("estado") & "</font><br><br>"
                                    'End If
                                End If

                            Next



                            'TextoCelda= "<td width='15%' id='" & dia & "' hora='" & hora & "'>" 

                            TextoCelda = TextoCelda & "</td>" & vbCrLf    'Muestra los cursos ya pintados en el horario.

                            tabla.Append(TextoCelda)
                        End If 'For para crear las columnas 
                    Next
                    tabla.Append("</tr>")


                Next 'For para crear las filas con los <tr>
                tabla.Append("</table>")


            Else
                tabla.Append("<table id='tblHorario' style='border-collapse: collapse;' width='100%'  border='1' bgcolor='white' bordercolor='#CCCCCC'>")
                tabla.Append("<tr class='etiquetaTabla' height='25px'>")
                tabla.Append("<th width='15%'>Horas</th>")
                tabla.Append("<th width='15%'>Lunes</th>")
                tabla.Append("<th width='15%'>Martes</th>")
                tabla.Append("<th width='15%'>Miércoles</th>")
                tabla.Append("<th width='15%'>Jueves</th>")
                tabla.Append("<th width='15%'>Viernes</th>")
                tabla.Append("<th width='15%'>Sábado</th>")
                tabla.Append("</tr>")
                tabla.Append("<tr>")
                tabla.Append("<td colspan=7 style='text-align:center; background-color: white; color: black; font-weight:bold;'>")
                tabla.Append("<h5>No se encontro disponibildiad de horario registrado para el docente seleccionado</h5>")
                tabla.Append("</td>")
                tabla.Append("</tr>")

            End If

            Me.tablacalendario.InnerHtml = tabla.ToString()


            ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pintar", "PintarCeldasHorarioDisponibleDocente(0)", True)
        Catch ex As Exception
            Dim script As String = "fnMensaje('error','" & ex.Message & "'); "
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alert", script, True)
        End Try


    End Sub

    Protected Sub ddlDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamento.SelectedIndexChanged
        fnCboPersonal()
    End Sub

    Protected Sub ddlCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCiclo.SelectedIndexChanged
        fnCboPersonal()
    End Sub

    Protected Sub ddlDocente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDocente.SelectedIndexChanged
        fnConsultar()
    End Sub
End Class


Partial Class PlanProyecto_lostCalendarioConsulta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CargaCalendarioAnual()
    End Sub

    Private Sub CargaCalendarioAnual()
        Dim strTabla As String = ""
        Dim strCabecera As String = ""
        Dim strLeyendaProy As String = ""
        Dim strTipo As String = ""

        Dim codigo_pro As Integer = 0
        If (Right(Request.QueryString("pro"), 1) = "T") Then
            codigo_pro = Request.QueryString("pro").ToString.Substring(0, Request.QueryString("pro").ToString.Length - 1)
            strTipo = "T"
        Else
            codigo_pro = Request.QueryString("pro")
            strTipo = "P"
        End If

        Dim obj As New ClsConectarDatos
        Dim dtAnio As New Data.DataTable
        Dim anio As Integer = Date.Today.Year
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtAnio = obj.TraerDataTable("PLAN_RetornaAnioSeleccion", strTipo, codigo_pro)
        obj.CerrarConexion()
        obj = Nothing

        If (dtAnio.Rows.Count > 0) Then
            anio = dtAnio.Rows(0).Item(0)
        End If

        strTabla = strTabla & "<table style='font-family: Arial; font-size:8px' cellpadding='3' cellspacing='0'>"
        'strTabla = strTabla & "<tr><td colspan='60' align='center' style='font-size:medium;'><b>" & Request.QueryString("titPro").ToString & "</b></td></tr>"
        strTabla = strTabla & fn_actividades()

        'If (Me.hfLeyenda.Value = 1) Then
        '    strLeyendaProy = ListaLeyendaProyecto(codigo_pro)
        'End If

        strTabla = strTabla & "</table><br/><br/>"

        Cal1.InnerHtml = strTabla

    End Sub

    Private Function fn_actividades() As String
        Dim strAct As String = ""
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Dim codigo_pro As Integer = 0
        Dim blnFinalizado As Boolean = False
        If (Right(Request.QueryString("pro").ToString, 1) = "T") Then
            codigo_pro = Request.QueryString("pro").ToString.Substring(0, Request.QueryString("pro").ToString.Length - 1)
            blnFinalizado = VerificaExiste(codigo_pro, 0)
        Else
            codigo_pro = Request.QueryString("pro")
            blnFinalizado = VerificaExiste(0, codigo_pro)
        End If

        If (blnFinalizado = False) Then
            If (Request.QueryString("titPro").ToString.StartsWith("[Calendario]") = True) Then
                dt = obj.TraerDataTable("CAL_ConsultarCalendarioAnual", codigo_pro)
            Else
                Dim strProyectos As String = ""
                Dim dtProy As New Data.DataTable
                dtProy = obj.TraerDataTable("PLAN_RetornaProyectosxTipo", codigo_pro)
                If (dtProy.Rows.Count > 0) Then
                    For j As Integer = 0 To dtProy.Rows.Count - 2
                        strProyectos = strProyectos & ", " & dtProy.Rows(j).Item("codigo_pro").ToString
                    Next
                    strProyectos = strProyectos & ", " & dtProy.Rows(dtProy.Rows.Count - 1).Item("codigo_pro").ToString
                End If
                dt = obj.TraerDataTable("CAL_ConsultarCalendarioAnual", strProyectos)
            End If
        Else
            If (Request.QueryString("titPro").ToString.StartsWith("[Calendario]") = True) Then
                dt = obj.TraerDataTable("CAL_ConsultaCalFin", codigo_pro)
            Else
                Dim strProyectos As String = ""
                Dim dtProy As New Data.DataTable
                dtProy = obj.TraerDataTable("PLAN_RetornaProyectosxTipo", codigo_pro)
                If (dtProy.Rows.Count > 0) Then
                    For j As Integer = 0 To dtProy.Rows.Count - 2
                        strProyectos = strProyectos & ", " & dtProy.Rows(j).Item("codigo_pro").ToString
                    Next
                    strProyectos = strProyectos & ", " & dtProy.Rows(dtProy.Rows.Count - 1).Item("codigo_pro").ToString
                End If
                dt = obj.TraerDataTable("CAL_ConsultaCalFin", strProyectos)
            End If
        End If



        'Me.hfLeyenda.Value = 0
        obj.CerrarConexion()

        'For del resultado de la consulta
        For i As Integer = 0 To dt.Rows.Count - 1
            If (dt.Rows(i).Item("visibilidad_apr") = True) Then
                strAct = strAct & "<tr style='height:25px;'>"
                If (dt.Rows(i).Item("proceso").ToString = "S") Then 'SI ES PROCESO
                    strAct = strAct & "<td colspan='2' style='width:175px; background:#CEE3F6; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b>" & dt.Rows(i).Item(1) & "</b></td>"
                    For j As Integer = 2 To 57
                        If ((j > 29 And j < 34) Or (j > 50 And j < 54)) Then
                            If (j > 29 And j < 34) Then
                                strAct = strAct & "<td style='width:17px; background: #D8D8D8; border-right-style:solid; border-right-color:" & retornaColor(j) & "; border-right-width:1.5px; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid;'>&nbsp;&nbsp;&nbsp;</td>"
                            End If
                            If (j > 50 And j < 54) Then
                                strAct = strAct & "<td style='width:13.5px; background: #D8D8D8; border-right-style:solid; border-right-color:" & retornaColor(j) & "; border-right-width:1.5px; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid;'>&nbsp;&nbsp;</td>"
                            End If
                        Else
                            If (j > 53) Then                                
                                strAct = strAct & "<td style='width:13.5px'>&nbsp;</td>"
                            Else
                                strAct = strAct & "<td style='border-right-style:solid; border-right-color:" & retornaColor(j) & "; border-right-width:1px; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid;'>&nbsp;</td>"
                            End If
                        End If
                    Next
                Else    'SI NO ES PROCESO                
                    If (dt.Rows(i).Item("feriado_apr") = False) Then                        
                        strAct = strAct & "<td colspan='2' style='width:175px; background:#CEE3F6; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'> " & dt.Rows(i).Item(1) & "</td>"
                    Else
                        strAct = strAct & "<td colspan='2' style='width:175px; background:#F7FE2E; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'> " & dt.Rows(i).Item(1) & "</td>"
                    End If


                    Dim strResponsables As String = ""
                    Dim sw As Byte = 0
                    Dim swColspan As Integer = 0
                    'Dim swFin As Byte = 0

                    'For del numero de semanas
                    For j As Integer = 2 To 57
                        'Preguntamos en que semana empieza la actividad

                        If (j < 54) Then
                            If (dt.Rows(i).Item(j).ToString = "") Then
                                'Veriicamos si esta en las semanas de vacaciones
                                If ((j > 29 And j < 34) Or (j > 50 And j < 54)) Then
                                    If (j > 29 And j < 34) Then
                                        strAct = strAct & "<td style='width:17px; background: #D8D8D8; border-right-style:solid; border-right-color:" & retornaColor(j) & "; border-right-width:1.5px; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid;'>&nbsp;&nbsp;&nbsp;</td>"
                                    End If
                                    If (j > 50 And j < 54) Then
                                        strAct = strAct & "<td style='width:13.5px; background: #D8D8D8; border-right-style:solid; border-right-color:" & retornaColor(j) & "; border-right-width:1.5px; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid;'>&nbsp;&nbsp;</td>"
                                    End If
                                Else
                                    If (j <= 57) Then
                                        Select Case j
                                            Case 21 To 29
                                                strAct = strAct & "<td style='width:13.5px; border-right-style:solid; border-right-color:" & retornaColor(j) & "; border-right-width:1.5px; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid;'>&nbsp;</td>"
                                            Case 32 To 50
                                                strAct = strAct & "<td style='width:13.4px; border-right-style:solid; border-right-color:" & retornaColor(j) & "; border-right-width:1.5px; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid;'>&nbsp;</td>"
                                            Case Else
                                                strAct = strAct & "<td style='width:13.5px; border-right-style:solid; border-right-color:" & retornaColor(j) & "; border-right-width:1.5px; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid;'>&nbsp;</td>"
                                        End Select
                                    End If

                                End If
                            Else    'Tiene semanas
                                Dim diaIni As String = ""
                                Dim diaFin As String = ""
                                If sw = 0 Then
                                    'Buscamos Responsables de la actividad
                                    Dim dtResponsable As New Data.DataTable
                                    If dt.Rows(i).Item(j).ToString <> "" Then
                                        dtResponsable = CargaDetalle(0, dt.Rows(i).Item(j))
                                        If (dtResponsable.Rows.Count = 0) Then
                                            strResponsables = strResponsables & "- No se registraron responsables"
                                        Else
                                            diaIni = DateTime.Parse(dtResponsable.Rows(0).Item("fechaInicio_apr").ToString).Day
                                            diaFin = DateTime.Parse(dtResponsable.Rows(0).Item("fechaFin_apr").ToString).Day
                                        End If
                                    End If
                                End If

                                Dim colorFeriado As String = "#F7FE2E"
                                If (dt.Rows(i).Item("feriado_apr") = True) Then
                                    colorFeriado = "#F7FE2E"
                                Else
                                    colorFeriado = dt.Rows(i).Item("color").ToString()
                                End If

                                If (dt.Rows(i).Item(j - 1).ToString = "") Then
                                    If (dt.Rows(i).Item(j + 1).ToString = "") Then
                                        strAct = strAct & "<td title='Actividad' align='center'  style='padding-left:0.3px; background: " & colorFeriado & "; color:" & dt.Rows(i).Item("color").ToString & "; cursor:hand; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'>" & _
                                        "<a href='frmActividadConsulta.aspx?apr=" & dt.Rows(i).Item(j) & "&KeepThis=true&TB_iframe=true&height=450&width=550' title='Actividades' class='thickbox' style='color:black'>" & IIf(dt.Rows(i).Item("mostrarDias_apr"), "<u>" & diaIni, "&nbsp;")
                                        strAct = strAct & IIf(dt.Rows(i).Item("mostrarDias_apr"), "1</u><br/>" & diaFin, "&nbsp;")
                                        strAct = strAct & "</a>" & "</td>"
                                    Else
                                        strAct = strAct & "<td title='Actividad' align='center'  style='padding-left:0.3px; background: " & colorFeriado & "; color:" & dt.Rows(i).Item("color").ToString & "; cursor:hand; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'>" & _
                                        "<a href='frmActividadConsulta.aspx?apr=" & dt.Rows(i).Item(j) & "&KeepThis=true&TB_iframe=true&height=450&width=550' title='Actividades' class='thickbox' style='color:black'>" & IIf(dt.Rows(i).Item("mostrarDias_apr"), diaIni, "&nbsp;")
                                        strAct = strAct & "</a>" & "</td>"
                                    End If
                                ElseIf (dt.Rows(i).Item(j + 1).ToString = "") Then
                                    strAct = strAct & "<td title='Actividad' align='center'  style='background: " & colorFeriado & "; color:" & dt.Rows(i).Item("color").ToString & "; cursor:hand; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'>" & _
                                           "<a href='frmActividadConsulta.aspx?apr=" & dt.Rows(i).Item(j) & "&KeepThis=true&TB_iframe=true&height=450&width=550' title='Actividades' class='thickbox' style='color:black'>" & IIf(dt.Rows(i).Item("mostrarDias_apr"), IIf(dt.Rows(i).Item("s1").ToString.Trim <> "", "<u>" & diaIni & "</u><br/>" & diaFin, diaFin), "&nbsp;")
                                    strAct = strAct & "</a>" & "</td>"
                                Else
                                    If (j = 53) Then
                                        strAct = strAct & "<td title='Actividad' align='center'  style='background: " & colorFeriado & "; color:" & dt.Rows(i).Item("color").ToString & "; cursor:hand; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'>" & _
                                            "<a href='frmActividadConsulta.aspx?apr=" & dt.Rows(i).Item(j) & "&KeepThis=true&TB_iframe=true&height=450&width=550' title='Actividades' class='thickbox' style='color:black'>" & IIf(dt.Rows(i).Item("mostrarDias_apr"), diaFin, "&nbsp;")
                                        strAct = strAct & "</a>" & "</td>"
                                    Else
                                        strAct = strAct & "<td title='Actividad' align='center'  style='background: " & colorFeriado & "; color:" & dt.Rows(i).Item("color").ToString & "; cursor:hand; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'>" & _
                                           "<a href='frmActividadConsulta.aspx?apr=" & dt.Rows(i).Item(j) & "&KeepThis=true&TB_iframe=true&height=450&width=550' title='Actividades' class='thickbox'>&nbsp;"
                                        strAct = strAct & "</a>" & "</td>"
                                    End If

                                End If
                            End If
                        Else
                            strAct = strAct & "<td style='width:13.5px;'>&nbsp;</td>"
                        End If

                    Next
                    strAct = strAct & "</tr>"
                End If
            End If
        Next

        Return strAct
    End Function

    Private Function CargaDetalle(ByVal codigo_res As Integer, ByVal codigo_apr As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            Dim dtResponsables As New Data.DataTable
            obj.AbrirConexion()
            dtResponsables = obj.TraerDataTable("PLAN_BuscaResponsables", codigo_res, codigo_apr)
            obj.CerrarConexion()
            obj = Nothing

            Return dtResponsables
        Catch ex As Exception
            Response.Write("Error al cargar responsables")
            Return New Data.DataTable
        End Try
    End Function

    Private Function retornaColor(ByVal fila As Integer) As String
        Dim strColor As String = "#D8DADB"
        Select Case fila
            Case 5
                strColor = "#848484"
            Case 9
                strColor = "#848484"
            Case 13
                strColor = "#848484"
            Case 18
                strColor = "#848484"
            Case 22
                strColor = "#848484"
            Case 27
                strColor = "#848484"
            Case 30
                strColor = "#FFFFFF"
            Case 31
                strColor = "#848484"
            Case 32
                strColor = "#FFFFFF"
            Case 35
                strColor = "#848484"
            Case 39
                strColor = "#848484"
            Case 44
                strColor = "#848484"
            Case 48
                strColor = "#848484"
            Case 51
                strColor = "#FFFFFF"
            Case 52
                strColor = "#FFFFFF"
            Case 53
                strColor = "#848484"
        End Select
        Return strColor
    End Function

    Private Function VerificaExiste(ByVal codigo_tpr As Integer, ByVal codigo_pro As Integer) As Boolean
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("PLAN_VerificaCalFinalizado", codigo_tpr, codigo_pro)
            If (dt.Rows.Count = 0) Then
                obj.CerrarConexion()
                obj = Nothing
                Return False
            End If

            obj.CerrarConexion()
            obj = Nothing
            Return True
        Catch ex As Exception
            Return True
            Response.Write("Error al verificar calendario finalizado: " & ex.Message)
        End Try

    End Function
End Class

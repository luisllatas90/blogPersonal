
Partial Class PlanProyecto_FrmExportaDirector

    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            If (Request.QueryString("pro") IsNot Nothing) Then
                CargaCalendarioAnual()
            Else
                calendario.InnerHtml = "No se selecciono ningun proyecto"
            End If
        End If
    End Sub

    Private Sub CargaCalendarioAnual()
        Dim strTabla As String = ""
        Dim strCabecera As String = ""
        Dim Tipo_pro As String = "T"    'T: Tipo    P: Proyecto

        Dim codigo_pro As Integer = 0
        If (Right(Request.QueryString("pro").ToString, 1) = "T") Then
            codigo_pro = Request.QueryString("pro").ToString.Substring(0, Request.QueryString("pro").ToString.Length - 1)
            Tipo_pro = "T"
        Else
            codigo_pro = Request.QueryString("pro")
            Tipo_pro = "P"
        End If

        Dim obj As New ClsConectarDatos
        Dim dtAnio As New Data.DataTable
        Dim anio As Integer = Date.Today.Year
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtAnio = obj.TraerDataTable("PLAN_RetornaAnioSeleccion", Tipo_pro, codigo_pro)
        obj.CerrarConexion()
        obj = Nothing

        If (dtAnio.Rows.Count > 0) Then
            anio = dtAnio.Rows(0).Item(0)
            hfAnio.Value = anio
        End If

        'Muestra el link para mostrar leyenda
        'strTabla = strTabla & "<a href='lstLeyendaCalendario.aspx?pro=" & Me.dpProyecto.SelectedValue & "&titPro=" & Me.dpProyecto.SelectedItem.Text & "' class='popup' style='color:Blue'><img border='0' src='../../images/librohoja.gif' />  <u>Mostrar Leyenda</u></a>"

        strTabla = strTabla & "<table style='font-family: Arial; font-size:x-small' cellpadding='3' cellspacing='0' width='1280px'>"
        strTabla = strTabla & "<tr><td colspan='60' align='center' style='font-size:medium;'><b>" & Request.QueryString("titPro") & "</b></td></tr>"
        strTabla = strTabla & fn_cabecera(anio)
        strTabla = strTabla & fn_actividades()
        strTabla = strTabla & "</table><br/><br/>"
        Me.hfCalendario.Value = strTabla
        calendario.InnerHtml = strTabla


        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Calendario.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        Response.Write(HTML(strTabla)) 'Llamada al procedimiento HTML
        Response.End()


        'If (Right(Request.QueryString("pro").ToString, 1) = "T") Then
        '    leyenda.InnerHtml = ListaLeyendaProyecto(Request.QueryString("pro").ToString.Substring(0, Request.QueryString("pro").ToString.Length - 1))
        'Else
        '    leyenda.InnerHtml = ListaLeyendaProyecto(Request.QueryString("pro"))
        'End If
    End Sub

    Private Function fn_cabecera(ByVal Anio As Integer) As String
        Dim strCab As String = ""
        strCab = "<tr>"
        strCab = strCab & "<td rowspan='3' align='center' style='width:150px; background: #CEE3F6; font-weight:bold; border-bottom-width:3px; border-bottom-color:#0B0B61; border-bottom-style:solid'>ACTIVIDAD ACADÉMICA - ADMINISTRATIVA</td>"
        strCab = strCab & "<td rowspan='2' align='center' style='width:75px; background: #CEE3F6; font-weight:bold; font-size:10px' valign='bottom'>MES</td>"
        strCab = strCab & "<td colspan='9' align='center' style='width:225px; border-left-style:solid; border-left-color:#FFFFFF; border-left-width:1px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px; border-bottom-width:3px; border-bottom-color:#FFFFFF; border-bottom-style:solid'>CICLO <br/>" & Anio & "-0</td>"
        strCab = strCab & "<td colspan='19' align='center' style='width:475px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:12px; border-bottom-width:3px; border-bottom-color:#FFFFFF; border-bottom-style:solid'>CICLO " & Anio & "-I</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #BDBDBD; font-weight:bold; font-size:12px; border-bottom-width:3px; border-bottom-color:#FFFFFF; border-bottom-style:solid'>PERIODO VACACIONAL</td>"
        strCab = strCab & "<td colspan='17' align='center' style='width:425px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:12px; border-bottom-width:3px; border-bottom-color:#FFFFFF; border-bottom-style:solid'>CICLO " & Anio & "-II</td>"
        strCab = strCab & "<td colspan='3' align='center' style='width:75px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #BDBDBD; font-weight:bold; font-size:12px; border-bottom-width:3px; border-bottom-color:#FFFFFF; border-bottom-style:solid'>P. FIN DE AÑO</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; font-weight:bold; font-size:12px;'></td>"
        strCab = strCab & "</tr>"
        strCab = strCab & "<tr>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; border-left-style:solid; border-left-color:#FFFFFF; border-left-width:1px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>ENERO</td>"  '
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>FEBRERO</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>MARZO</td>"
        strCab = strCab & "<td colspan='5' align='center' style='width:125px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>ABRIL</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>MAYO</td>"
        strCab = strCab & "<td colspan='5' align='center' style='width:125px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>JUNIO</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>JULIO</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>AGOSTO</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:125px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>SETIEMBRE</td>"
        strCab = strCab & "<td colspan='5' align='center' style='width:125px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>OCTUBRE</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>NOVIEMBRE</td>"
        strCab = strCab & "<td colspan='5' align='center' style='width:125px; border-right-style:solid; border-right-color:#FFFFFF; border-right-width:1px; background: #CEE3F6; font-weight:bold; font-size:11px'>DICIEMBRE</td>"
        strCab = strCab & "<td colspan='4' align='center' style='width:100px; font-weight:bold; font-size:11px'></td>"
        strCab = strCab & "</tr>"
        strCab = strCab & "<tr>"
        strCab = strCab & "<td align='center' style='width:75px; background: #CEE3F6; font-weight:bold; font-size:10px; border-bottom-width:3px; border-bottom-color:#0B0B61; border-bottom-style:solid'>SEMANA</td>"
        For i As Integer = 1 To 53
            If (i > 52) Then
                strCab = strCab & "<td align='center' style='width:25px; color: #0B0B61; font-weight:bold; font-size:9px;'>&nbsp;&nbsp;&nbsp;</td>"
            Else
                If (i < 10) Then
                    strCab = strCab & "<td align='center' style='border-style:solid; border-width:1.5px; width:25px; color: #0B0B61; font-weight:bold; font-size:9px; border-bottom-width:3px; border-bottom-color:#0B0B61; border-bottom-style:solid'>&nbsp;" & i & "&nbsp;</td>"
                Else
                    strCab = strCab & "<td align='center' style='border-style:solid; border-width:1.5px; width:25px; color: #0B0B61; font-weight:bold; font-size:9px; border-bottom-width:3px; border-bottom-color:#0B0B61; border-bottom-style:solid'>" & i & "</td>"
                End If

            End If
        Next

        strCab = strCab & "<tr>"
        Return strCab
    End Function

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
                        strAct = strAct & "<td colspan='2' style='width:175px; background:#CEE3F6; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'>" & dt.Rows(i).Item(1) & "</td>"
                    Else
                        strAct = strAct & "<td colspan='2' style='width:175px; background:#F7FE2E; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'>" & dt.Rows(i).Item(1) & "</td>"
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
                                        strAct = strAct & "<td title='Actividad' align='center'  style='padding-left:0.3px; background: " & colorFeriado & "; cursor:hand; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'>" & _
                                            IIf(dt.Rows(i).Item("mostrarDias_apr"), "<u>" & diaIni, "&nbsp;")
                                        strAct = strAct & IIf(dt.Rows(i).Item("mostrarDias_apr"), "</u><br/>" & diaFin, "&nbsp;")
                                        strAct = strAct & "</td>"
                                    Else
                                        strAct = strAct & "<td title='Actividad' align='center'  style='padding-left:0.3px; background: " & colorFeriado & "; cursor:hand; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'>" & _
                                            IIf(dt.Rows(i).Item("mostrarDias_apr"), diaIni, "&nbsp;")
                                        strAct = strAct & "</td>"
                                    End If
                                ElseIf (dt.Rows(i).Item(j + 1).ToString = "") Then
                                    strAct = strAct & "<td title='Actividad' align='center'  style='background: " & colorFeriado & "; cursor:hand; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'>" & _
                                            IIf(dt.Rows(i).Item("mostrarDias_apr"), diaFin, "&nbsp;")
                                    strAct = strAct & "</a>" & "</td>"
                                Else
                                    If (j = 53) Then
                                        strAct = strAct & "<td title='Actividad' align='center'  style='background: " & colorFeriado & "; cursor:hand; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'>" & _
                                            IIf(dt.Rows(i).Item("mostrarDias_apr"), diaFin, "&nbsp;")
                                        strAct = strAct & "</td>"
                                    Else
                                        strAct = strAct & "<td title='Actividad' align='center'  style='background: " & colorFeriado & "; cursor:hand; border-bottom-width:1px; border-bottom-color:#0B0B61; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'>&nbsp;</td>"
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

    'El procedimiento HTML que se encarga de dar o mantener formatos según corresponda:
    Public Function HTML(ByVal tabla As String) As String
        Try
            Dim page1 As New Page
            Dim form1 As New HtmlForm
            page1.EnableViewState = False
            page1.Controls.Add(form1)

            Dim builder1 As New StringBuilder
            Dim writer1 As New System.IO.StringWriter(builder1)
            Dim writer2 As New HtmlTextWriter(writer1)

            writer2.Write("<html xmlns='http://www.w3.org/1999/xhtml'>" & "<head><title>Datos</title><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1'>" & "<style></style></head><body>")
            'writer2.Write("<img src='http://server-test/campusvirtual/personal/PlanProyecto/css/images/Logo-USAT.png' />")
            writer2.Write(tabla)
            'writer2.Write("<table><tr><td></td><td></td><td><font face=Arial size=5><center>Título Principal</center></font></td></tr></table><br>")
            'writer2.Write("<table>\n<tr>\n<td></td><td class=TD width=35%><b>Fecha  :</b></td><td width=65% align=left>aaa</td>\n</tr>\n<tr>\n<td></td><td class=TD><b>Gerencia:</b></td><td>bbb</td>\n</tr>\n</table>\n<br><br>")

            page1.DesignerInitialize()
            page1.RenderControl(writer2)
            writer2.Write("</body></html>")
            page1.Dispose()
            page1 = Nothing
            Return builder1.ToString()
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class

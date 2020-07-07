
Partial Class indicadores_POA_FrmListaEvaluarPresupuesto
    Inherits System.Web.UI.Page
    Dim nombre_poa As String = String.Empty
    Dim CurrentRow1 As Integer = -1
    'Dim usuario, ctf As Integer

    Sub CargaPlanes()
        Dim obj As New clsPlanOperativoAnual
        Dim dtPEI As New Data.DataTable

        'dtPEI = obj.ListaPeis
        dtPEI = obj.ListaPeisxResponsable(Request.QueryString("id"), Request.QueryString("ctf"))

        Me.ddlplan.DataSource = dtPEI
        Me.ddlplan.DataTextField = "descripcion"
        Me.ddlplan.DataValueField = "codigo"
        Me.ddlplan.DataBind()
        dtPEI.Dispose()
        obj = Nothing
    End Sub

    Sub CargaEjercicio()
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.ListaEjercicio
        Me.ddlEjercicio.DataSource = dtt
        Me.ddlEjercicio.DataTextField = "descripcion"
        Me.ddlEjercicio.DataValueField = "codigo"
        Me.ddlEjercicio.DataBind()
        dtt.Dispose()
        obj = Nothing
        Me.ddlEjercicio.SelectedIndex = Me.ddlEjercicio.Items.Count - 1
    End Sub

    'Sub CargaEstados()
    '    Dim obj As New clsPlanOperativoAnual
    '    Dim dtt As New Data.DataTable
    '    dtt = obj.CargaEstadosEvalPto(Request.QueryString("id"), Request.QueryString("ctf"))
    '    Me.ddlestado.DataSource = dtt
    '    Me.ddlestado.DataTextField = "descripcion"
    '    Me.ddlestado.DataValueField = "codigo"
    '    Me.ddlestado.DataBind()
    '    dtt.Dispose()
    '    obj = Nothing
    'End Sub

    Sub CargaPoas(ByVal codigo_pla As Integer, ByVal codigo_ejp As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable

        'POA_ListaPeisRespyApoyo
        dtt = obj.ListaPoasxPEIxEjercicio(codigo_pla, codigo_ejp, Request.QueryString("id"), Request.QueryString("ctf"))
        Me.ddlPoa.DataSource = dtt
        Me.ddlPoa.DataTextField = "descripcion"
        Me.ddlPoa.DataValueField = "codigo"
        Me.ddlPoa.DataBind()
        dtt.Dispose()
        obj = Nothing
        'Me.ddlPoa.Items.Insert(0, New ListItem("--SELECCIONE--", "0"))
    End Sub

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        Try
            If Session("id_per") = "" Or Request.QueryString("id") = "" Then
                Response.Redirect("../../../sinacceso.html")
            End If

            If IsPostBack = False Then
                Dim Tabla As String
                CargaPlanes()
                CargaEjercicio()
                If Request.QueryString("cb1") <> "" Then
                    Me.ddlplan.SelectedValue = Request.QueryString("cb1")
                    Me.ddlEjercicio.SelectedValue = Request.QueryString("cb2")
                    Me.ddlEjercicio_SelectedIndexChanged(sender, e)
                    Me.ddlPoa.SelectedValue = Request.QueryString("cb3")
                End If
                Tabla = CargaActividadesPto(Me.ddlplan.SelectedValue, Me.ddlPoa.SelectedValue, Me.ddlEjercicio.SelectedValue, "%", Request.QueryString("id"), Request.QueryString("ctf"))
                TablaActividadesPto.InnerHtml = Tabla
                'End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim Tabla As String
            Tabla = CargaActividadesPto(Me.ddlplan.SelectedValue, Me.ddlPoa.SelectedValue, Me.ddlEjercicio.SelectedValue, "%", Request.QueryString("id"), Request.QueryString("ctf"))
            TablaActividadesPto.InnerHtml = Tabla
            Me.hdfila.Value = -1
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Function CargaActividadesPto(ByVal codigo_pladdl As Integer, ByVal codigo_poaddl As Integer, ByVal codigo_ejp As Integer, ByVal estado As String, ByVal id As Integer, ByVal ctf As Integer) As String
        Dim strTabla As String = ""
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        dt = obj.ListarPresupuestoProgProy(codigo_pladdl, codigo_poaddl, codigo_ejp, "%", id, ctf)
        If dt.Rows.Count > 0 Then
            'Para Celda
            'Dim estilo_celda As String
            '-----------Cabecera
            strTabla = strTabla & "<table style='width:99%;font-size:12px; border-collapse:collapse; border:1px;' border='1' CellSpacing='0' CellPadding='2' >"
            strTabla = strTabla & "<thead>"
            strTabla = strTabla & "<tr>"
            strTabla = strTabla & "<th rowspan='3' style='background-color:#3871b0;color:white;' >PLAN OPERATIVO ANUAL</th>"
            strTabla = strTabla & "<th rowspan='3' style='background-color:#3871b0;color:white;' >TIPO ACTIVIDAD</td>"
            strTabla = strTabla & "<th rowspan='3' style='background-color:#3871b0;color:white;' >PROGRAMA / PROYECTO [FEC. INI - FEC. FIN]</th>"
            strTabla = strTabla & "<th colspan='6' style='background-color:#3871b0;color:white;' >PRESUPUESTO</th>"
            strTabla = strTabla & "<th rowspan='3' style='background-color:#3871b0;color:white;' >INSTANCIA</th>"
            strTabla = strTabla & "<th rowspan='3' style='background-color:#3871b0;color:white;' >ESTADO</th>"
            strTabla = strTabla & "<th rowspan='3' width='80px'>VER DETALLE</th>"
            strTabla = strTabla & "</tr>"
            strTabla = strTabla & "</tr>"
            strTabla = strTabla & "<tr>"
            strTabla = strTabla & "<th colspan='2' style='background-color:#308cd9;color:white;'>INGRESOS</th>"
            strTabla = strTabla & "<th colspan='2' style='background-color:#ff4f4f;color:white;' >EGRESOS</th>"
            strTabla = strTabla & "<th colspan='2' style='background-color:#559155;color:white;' >EXCEDENTE</th>"
            strTabla = strTabla & "</tr>"
            strTabla = strTabla & "<tr>"
            strTabla = strTabla & "<th style='background-color:#308cd9;color:white;' width='80px'>TOPE PRESUPUESTO (S/.)</th>"
            strTabla = strTabla & "<th style='background-color:#308cd9;color:white;' width='80px'>MONTO REGISTRADO (S/.)</th>"
            strTabla = strTabla & "<th style='background-color:#ff4f4f;color:white;' width='80px'>TOPE PRESUPUESTO (S/.)</th>"
            strTabla = strTabla & "<th style='background-color:#ff4f4f;color:white;' width='80px'>MONTO REGISTRADO (S/.)</th>"
            strTabla = strTabla & "<th style='background-color:#559155;color:white;' width='80px'>TOPE PRESUPUESTO (S/.)</th>"
            strTabla = strTabla & "<th style='background-color:#559155;color:white;' width='80px'>MONTO REGISTRADO (S/.)</th>"
            strTabla = strTabla & "</tr>"

            strTabla = strTabla & "</thead>"
            strTabla = strTabla & "<tbody>"
            '----------Fin Cabecera

            '---Cuerpo
            Dim codigo_poa As Integer = 0
            Dim StrCab As String = ""
            Dim rowspanCab As Integer = 1
            Dim rowspanTac As Integer = 1
            Dim StrCeldas As String = ""
            Dim ingresos_prog As Decimal = 0
            Dim egresos_prog As Decimal = 0
            Dim utilidad_prog As Decimal = 0
            Dim ingresos_pto As Decimal = 0
            Dim egresos_pto As Decimal = 0
            Dim utilidad_pto As Decimal = 0

            Dim colorpoa_ing As String = "style='background-color:#c7e0f5;'"
            Dim colorpto_ing As String = "style='background-color:#ddecf9;'"
            Dim colorpoa_eg As String = "style='background-color:#ffcece;'"
            Dim colorpto_eg As String = "style='background-color:#ffe7e7;'"
            Dim colorpoa_uti As String = "style='background-color:#bfd9bf;'"
            Dim colorpto_uti As String = "style='background-color:#cfe3cf;'"
            For i As Integer = 0 To dt.Rows.Count - 1
                codigo_poa = dt.Rows(i).Item("codigo_poa")
                ' Valido que no haya desbordamiento
                If i + 1 <= dt.Rows.Count - 1 Then
                    'Comparo con el Siguiente Poa
                    If codigo_poa = dt.Rows(i + 1).Item("codigo_poa") Then
                        'Agrego Fila
                        StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("descripcion_tac") & "</td>"
                        StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("resumen_acp") & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpoa_ing & ">" & FormatNumber(dt.Rows(i).Item("ingresos_acp"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpto_ing & " ><B>" & FormatNumber(dt.Rows(i).Item("ingresos_pto"), 2) & "</B></td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpoa_eg & ">" & FormatNumber(dt.Rows(i).Item("egresos_acp"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpto_eg & " ><B>" & FormatNumber(dt.Rows(i).Item("egresos_pto"), 2) & "</B></td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpoa_uti & ">" & FormatNumber(dt.Rows(i).Item("utilidad_acp"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpto_uti & " ><B>" & FormatNumber(dt.Rows(i).Item("utilidad_pto"), 2) & "</B></td>"
                        StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("nombre_ins") & "</td>"
                        StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("nombre_est") & "</td>"
                        ingresos_prog = ingresos_prog + FormatNumber(dt.Rows(i).Item("ingresos_acp"), 2)
                        egresos_prog = egresos_prog + FormatNumber(dt.Rows(i).Item("egresos_acp"), 2)
                        utilidad_prog = utilidad_prog + FormatNumber(dt.Rows(i).Item("utilidad_acp"), 2)
                        ingresos_pto = ingresos_pto + FormatNumber(dt.Rows(i).Item("ingresos_pto"), 2)
                        egresos_pto = egresos_pto + FormatNumber(dt.Rows(i).Item("egresos_pto"), 2)
                        utilidad_pto = utilidad_pto + FormatNumber(dt.Rows(i).Item("utilidad_pto"), 2)
                        'StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>"
                        'If dt.Rows(i).Item("codigo_pto") > 0 Then
                        '    'si Tiene Presupuesto Colocamos el Link
                        '    StrCeldas = StrCeldas & "<a href='../../../librerianet/sesionesPOA.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf")
                        '    StrCeldas = StrCeldas & "&field=" & dt.Rows(i).Item("codigo_pto") & "&nombreCeco=" & dt.Rows(i).Item("descripcion_cco") & "&actividadPto=" & dt.Rows(i).Item("resumen_acp")
                        '    StrCeldas = StrCeldas & "&instancia=" & dt.Rows(i).Item("nombre_ins") & "&estado=" & dt.Rows(i).Item("nombre_est") & "&op=gesPto&opcion=EVAL_PTO&cb1=" & Me.ddlplan.SelectedValue
                        '    StrCeldas = StrCeldas & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlPoa.SelectedValue & "&cb4=" & Me.ddlestado.SelectedValue & "' title='Ver Detalle' ><b>Ver Detalle</b></a>"
                        'End If
                        'StrCeldas = StrCeldas & "</td>
                        StrCeldas = StrCeldas & "<td><a href='FrmListaResumenDetallePresupuesto.aspx"
                        StrCeldas = StrCeldas & "?id=" & Request.QueryString("id")
                        StrCeldas = StrCeldas & "&ctf=" & Request.QueryString("ctf")
                        StrCeldas = StrCeldas & "&codigo_acp=" & dt.Rows(i).Item("codigo_acp")
                        StrCeldas = StrCeldas & "&resumen_acp=" & dt.Rows(i).Item("resumen_acp")
                        StrCeldas = StrCeldas & "&cb1=" & Me.ddlplan.SelectedValue
                        StrCeldas = StrCeldas & "&cb2=" & Me.ddlEjercicio.SelectedValue
                        StrCeldas = StrCeldas & "&cb3=" & Me.ddlPoa.SelectedValue & "'"
                        StrCeldas = StrCeldas & " id='ref'><b>Ver Detalle</b></a></td>"
                        StrCeldas = StrCeldas & "</tr>"
                        'Aumento Rowspan
                        rowspanCab = rowspanCab + 1
                    Else
                        'Diferente Poa Agrego cabecera de Fila y Filas
                        StrCab = "<tr><td class='celda_combinada' style='vertical-align:middle' rowspan=" & rowspanCab + 2 & ">" & dt.Rows(i).Item("nombre_poa") & "</td>"
                        StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("descripcion_tac") & "</td>"
                        StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("resumen_acp") & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpoa_ing & ">" & FormatNumber(dt.Rows(i).Item("ingresos_acp"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpto_ing & " ><B>" & FormatNumber(dt.Rows(i).Item("ingresos_pto"), 2) & "</B></td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpoa_eg & ">" & FormatNumber(dt.Rows(i).Item("egresos_acp"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpto_eg & " ><B>" & FormatNumber(dt.Rows(i).Item("egresos_pto"), 2) & "</B></td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpoa_uti & ">" & FormatNumber(dt.Rows(i).Item("utilidad_acp"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpto_uti & " ><B>" & FormatNumber(dt.Rows(i).Item("utilidad_pto"), 2) & "</B></td>"
                        StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("nombre_ins") & "</td>"
                        StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("nombre_est") & "</td>"
                        'StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>"
                        'If dt.Rows(i).Item("codigo_pto") > 0 Then
                        '    'si Tiene Presupuesto Colocamos el Link
                        '    StrCeldas = StrCeldas & "<a href='../../../librerianet/sesionesPOA.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf")
                        '    StrCeldas = StrCeldas & "&field=" & dt.Rows(i).Item("codigo_pto") & "&nombreCeco=" & dt.Rows(i).Item("descripcion_cco") & "&actividadPto=" & dt.Rows(i).Item("resumen_acp")
                        '    StrCeldas = StrCeldas & "&instancia=" & dt.Rows(i).Item("nombre_ins") & "&estado=" & dt.Rows(i).Item("nombre_est") & "&op=gesPto&opcion=EVAL_PTO&cb1=" & Me.ddlplan.SelectedValue
                        '    StrCeldas = StrCeldas & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlPoa.SelectedValue & "&cb4=" & Me.ddlestado.SelectedValue & "' title='Ver Detalle' ><b>Ver Detalle</b></a>"
                        'End If
                        'StrCeldas = StrCeldas & "</td>
                        StrCeldas = StrCeldas & "<td><a href='FrmListaResumenDetallePresupuesto.aspx"
                        StrCeldas = StrCeldas & "?id=" & Request.QueryString("id")
                        StrCeldas = StrCeldas & "&ctf=" & Request.QueryString("ctf")
                        StrCeldas = StrCeldas & "&codigo_acp=" & dt.Rows(i).Item("codigo_acp")
                        StrCeldas = StrCeldas & "&resumen_acp=" & dt.Rows(i).Item("resumen_acp")
                        StrCeldas = StrCeldas & "&cb1=" & Me.ddlplan.SelectedValue
                        StrCeldas = StrCeldas & "&cb2=" & Me.ddlEjercicio.SelectedValue
                        StrCeldas = StrCeldas & "&cb3=" & Me.ddlPoa.SelectedValue & "'"
                        StrCeldas = StrCeldas & " id='ref'><b>Ver Detalle</b></a></td>"
                        StrCeldas = StrCeldas & "</tr>"

                        ingresos_prog = ingresos_prog + FormatNumber(dt.Rows(i).Item("ingresos_acp"), 2)
                        egresos_prog = egresos_prog + FormatNumber(dt.Rows(i).Item("egresos_acp"), 2)
                        utilidad_prog = utilidad_prog + FormatNumber(dt.Rows(i).Item("utilidad_acp"), 2)
                        ingresos_pto = ingresos_pto + FormatNumber(dt.Rows(i).Item("ingresos_pto"), 2)
                        egresos_pto = egresos_pto + FormatNumber(dt.Rows(i).Item("egresos_pto"), 2)
                        utilidad_pto = utilidad_pto + FormatNumber(dt.Rows(i).Item("utilidad_pto"), 2)

                        StrCeldas = StrCeldas & "<tr style='color:blue'><td colspan='2' align='center'>TOTAL REGISTRADO</td>"
                        StrCeldas = StrCeldas & "<td align='right'>" & FormatNumber(ingresos_prog, 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right'><B>" & FormatNumber(ingresos_pto, 2) & "</B></td>"
                        StrCeldas = StrCeldas & "<td align='right'>" & FormatNumber(egresos_prog, 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right'><B>" & FormatNumber(egresos_pto, 2) & "</B></td>"
                        StrCeldas = StrCeldas & "<td align='right'>" & FormatNumber(utilidad_prog, 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right'><B>" & FormatNumber(utilidad_pto, 2) & "</B></td>"
                        StrCeldas = StrCeldas & "<td colspan='3'></td></tr>"

                        StrCeldas = StrCeldas & "<tr style='color:red'><td colspan='2' align='center'>ASIGNACIÓN PRESUPUESTAL POA</td>"
                        StrCeldas = StrCeldas & "<td colspan='2' align='center'>" & FormatNumber(dt.Rows(i).Item("limite_ingreso"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td colspan='2' align='center'>" & FormatNumber(dt.Rows(i).Item("limite_egreso"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td colspan='2' align='center'>" & FormatNumber(dt.Rows(i).Item("utilidad"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td colspan='3' ></td></tr>"
                        'Agrego Fila Con Rowspan de Cabecera de Fila , reinicio Rowspan y Seteo Fila
                        strTabla = strTabla & StrCab & StrCeldas
                        rowspanCab = 1
                        StrCeldas = ""

                        ingresos_prog = 0
                        egresos_prog = 0
                        utilidad_prog = 0
                        ingresos_pto = 0
                        egresos_pto = 0
                        utilidad_pto = 0
                    End If
                Else
                    'ultima Fila
                    StrCab = "<tr><td class='celda_combinada' style='vertical-align:middle' rowspan=" & rowspanCab + 2 & ">" & dt.Rows(i).Item("nombre_poa") & "</td>"
                    StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("descripcion_tac") & "</td>"
                    StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("resumen_acp") & "</td>"
                    StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpoa_ing & ">" & FormatNumber(dt.Rows(i).Item("ingresos_acp"), 2) & "</td>"
                    StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpto_ing & " ><B>" & FormatNumber(dt.Rows(i).Item("ingresos_pto"), 2) & "</B></td>"
                    StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpoa_eg & ">" & FormatNumber(dt.Rows(i).Item("egresos_acp"), 2) & "</td>"
                    StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpto_eg & " ><B>" & FormatNumber(dt.Rows(i).Item("egresos_pto"), 2) & "</B></td>"
                    StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpoa_uti & ">" & FormatNumber(dt.Rows(i).Item("utilidad_acp"), 2) & "</td>"
                    StrCeldas = StrCeldas & "<td align='right' class='celda_combinada' " & colorpto_uti & " ><B>" & FormatNumber(dt.Rows(i).Item("utilidad_pto"), 2) & "</B></td>"
                    StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("nombre_ins") & "</td>"
                    StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("nombre_est") & "</td>"
                    'StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>"
                    'If dt.Rows(i).Item("codigo_pto") > 0 Then
                    '    'si Tiene Presupuesto Colocamos el Link
                    '    StrCeldas = StrCeldas & "<a href='../../../librerianet/sesionesPOA.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf")
                    '    StrCeldas = StrCeldas & "&field=" & dt.Rows(i).Item("codigo_pto") & "&nombreCeco=" & dt.Rows(i).Item("descripcion_cco") & "&actividadPto=" & dt.Rows(i).Item("resumen_acp")
                    '    StrCeldas = StrCeldas & "&instancia=" & dt.Rows(i).Item("nombre_ins") & "&estado=" & dt.Rows(i).Item("nombre_est") & "&op=gesPto&opcion=EVAL_PTO&cb1=" & Me.ddlplan.SelectedValue
                    '    StrCeldas = StrCeldas & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlPoa.SelectedValue & "&cb4=" & Me.ddlestado.SelectedValue & "' title='Ver Detalle' ><b>Ver Detalle</b></a>"
                    'End If
                    'StrCeldas = StrCeldas & "</td>
                    StrCeldas = StrCeldas & "<td><a href='FrmListaResumenDetallePresupuesto.aspx"
                    StrCeldas = StrCeldas & "?id=" & Request.QueryString("id")
                    StrCeldas = StrCeldas & "&ctf=" & Request.QueryString("ctf")
                    StrCeldas = StrCeldas & "&codigo_acp=" & dt.Rows(i).Item("codigo_acp")
                    StrCeldas = StrCeldas & "&resumen_acp=" & dt.Rows(i).Item("resumen_acp")
                    StrCeldas = StrCeldas & "&cb1=" & Me.ddlplan.SelectedValue
                    StrCeldas = StrCeldas & "&cb2=" & Me.ddlEjercicio.SelectedValue
                    StrCeldas = StrCeldas & "&cb3=" & Me.ddlPoa.SelectedValue & "'"
                    StrCeldas = StrCeldas & " id='ref'><b>Ver Detalle</b></a></td>"
                    StrCeldas = StrCeldas & "</tr>"

                    ingresos_prog = ingresos_prog + FormatNumber(dt.Rows(i).Item("ingresos_acp"), 2)
                    egresos_prog = egresos_prog + FormatNumber(dt.Rows(i).Item("egresos_acp"), 2)
                    utilidad_prog = utilidad_prog + FormatNumber(dt.Rows(i).Item("utilidad_acp"), 2)
                    ingresos_pto = ingresos_pto + FormatNumber(dt.Rows(i).Item("ingresos_pto"), 2)
                    egresos_pto = egresos_pto + FormatNumber(dt.Rows(i).Item("egresos_pto"), 2)
                    utilidad_pto = utilidad_pto + FormatNumber(dt.Rows(i).Item("utilidad_pto"), 2)

                    StrCeldas = StrCeldas & "<tr style='color:blue'><td colspan='2' align='center'>TOTAL REGISTRADO</td>"
                    StrCeldas = StrCeldas & "<td align='right'>" & FormatNumber(ingresos_prog, 2) & "</td>"
                    StrCeldas = StrCeldas & "<td align='right'><B>" & FormatNumber(ingresos_pto, 2) & "</B></td>"
                    StrCeldas = StrCeldas & "<td align='right'>" & FormatNumber(egresos_prog, 2) & "</td>"
                    StrCeldas = StrCeldas & "<td align='right'><B>" & FormatNumber(egresos_pto, 2) & "</B></td>"
                    StrCeldas = StrCeldas & "<td align='right'>" & FormatNumber(utilidad_prog, 2) & "</td>"
                    StrCeldas = StrCeldas & "<td align='right'><B>" & FormatNumber(utilidad_pto, 2) & "</B></td>"
                    StrCeldas = StrCeldas & "<td colspan='3'></td></tr>"
                    StrCeldas = StrCeldas & "<tr style='color:red'><td colspan='2' align='center'>ASIGNACIÓN PRESUPUESTAL POA</td>"
                    StrCeldas = StrCeldas & "<td colspan='2' align='center'>" & FormatNumber(dt.Rows(i).Item("limite_ingreso"), 2) & "</td>"
                    StrCeldas = StrCeldas & "<td colspan='2' align='center'>" & FormatNumber(dt.Rows(i).Item("limite_egreso"), 2) & "</td>"
                    StrCeldas = StrCeldas & "<td colspan='2' align='center'>" & FormatNumber(dt.Rows(i).Item("utilidad"), 2) & "</td>"
                    StrCeldas = StrCeldas & "<td colspan='3'></td></tr>"
                    'Agrego Fila Con Rowspan de Cabecera de Fila
                    strTabla = strTabla & StrCab & StrCeldas
                End If

            Next
            '---Fin Cuerpo
            strTabla = strTabla & "</tbody></table>"
        Else
            strTabla = ""
        End If

        Return strTabla
    End Function

    Protected Sub ddlplan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlplan.SelectedIndexChanged
        CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
    End Sub

    Protected Sub ddlEjercicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEjercicio.SelectedIndexChanged
        CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        If Me.HdData.Value <> "" Then
            Dim data As String = HdData.Value
            data = HttpUtility.UrlDecode(data)
            Response.Clear()
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", "attachment;filename=ResumenPresupuesto.xls")
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1252")
            'Response.Charset = "utf-8"
            HttpContext.Current.Response.Write(data)
            HttpContext.Current.Response.Flush()
            HttpContext.Current.Response.End()
            Me.HdData.Value = ""
        Else
            Response.Write("<script>alert('No Existen Elementos a Exportar')</script>")
        End If
    End Sub
End Class

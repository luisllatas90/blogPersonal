﻿
Partial Class indicadores_POA_FrmListaEvaluarPresupuesto
    Inherits System.Web.UI.Page
    Dim nombre_poa As String = String.Empty
    Dim CurrentRow1 As Integer = -1
    'Dim usuario, ctf As Integer

    Sub CargaPlanes()
        Dim obj As New clsPlanOperativoAnual
        Dim dtPEI As New Data.DataTable
        dtPEI = obj.ListaPeis
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

    Sub CargaEstados()
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.CargaEstadosEvalPto(Request.QueryString("id"), Request.QueryString("ctf"))
        Me.ddlestado.DataSource = dtt
        Me.ddlestado.DataTextField = "descripcion"
        Me.ddlestado.DataValueField = "codigo"
        Me.ddlestado.DataBind()
        dtt.Dispose()
        obj = Nothing
    End Sub

    Sub CargaPoas(ByVal codigo_pla As Integer, ByVal codigo_ejp As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.ListaPoasxInstanciaEstado(codigo_pla, codigo_ejp, "PTO", Request.QueryString("id"), Request.QueryString("ctf"))
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
                CargaEstados()
                'usuario = Request.QueryString("id")
                'ctf = Request.QueryString("ctf")
                If Request.QueryString("back") = "pto" Then
                    Me.ddlplan.SelectedValue = Request.QueryString("cb1")
                    Me.ddlEjercicio.SelectedValue = Request.QueryString("cb2")
                    Me.ddlEjercicio_SelectedIndexChanged(sender, e)
                    Me.ddlPoa.SelectedValue = Request.QueryString("cb3")
                    Me.ddlestado.SelectedValue = Request.QueryString("cb4")
                    Me.hdfila.Value = Request.QueryString("codigo_acp")
                    Me.btnBuscar_Click(sender, e)
                Else
                    Tabla = CargaActividadesPto(Me.ddlplan.SelectedValue, Me.ddlPoa.SelectedValue, Me.ddlEjercicio.SelectedValue, Me.ddlestado.SelectedValue, Request.QueryString("id"), Request.QueryString("ctf"))
                    TablaActividadesPto.InnerHtml = Tabla
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim Tabla As String
            Tabla = CargaActividadesPto(Me.ddlplan.SelectedValue, Me.ddlPoa.SelectedValue, Me.ddlEjercicio.SelectedValue, Me.ddlestado.SelectedValue, Request.QueryString("id"), Request.QueryString("ctf"))
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
        dt = obj.EvaluarPresupuesto(codigo_pladdl, codigo_poaddl, codigo_ejp, Me.ddlestado.SelectedValue, estado, id, ctf)
        If dt.Rows.Count > 0 Then
            'Para Celda
            'Dim estilo_celda As String
            '-----------Cabecera
            strTabla = strTabla & "<table style='width:99%;font-size:8px; border-collapse:collapse; border:1px;' border='1' CellSpacing='0' CellPadding='2' >"
            strTabla = strTabla & "<thead>"
            strTabla = strTabla & "<tr>"
            strTabla = strTabla & "<th rowspan='3' >PLAN OPERATIVO ANUAL</th>"
            strTabla = strTabla & "<th rowspan='3' >TIPO ACTIVIDAD</td>"
            strTabla = strTabla & "<th rowspan='3' >PROGRAMA / PROYECTO</th>"
            strTabla = strTabla & "<th colspan='6' >PRESUPUESTO</th>"
            strTabla = strTabla & "<th rowspan='3' width='75px' align='center' >VER DETALLE</th>"
            strTabla = strTabla & "</tr>"
            strTabla = strTabla & "<tr>"
            strTabla = strTabla & "<th colspan='2' >INGRESOS</th>"
            strTabla = strTabla & "<th colspan='2' >EGRESOS</th>"
            strTabla = strTabla & "<th colspan='2' >EXCEDENTE</th>"
            strTabla = strTabla & "</tr>"
            strTabla = strTabla & "<tr>"
            strTabla = strTabla & "<th>TOPE PRESUPUESTO (S/.)</th>"
            strTabla = strTabla & "<th>MONTO REGISTRADO (S/.)</th>"
            strTabla = strTabla & "<th>TOPE PRESUPUESTO (S/.)</th>"
            strTabla = strTabla & "<th>MONTO REGISTRADO (S/.)</th>"
            strTabla = strTabla & "<th>TOPE PRESUPUESTO (S/.)</th>"
            strTabla = strTabla & "<th>MONTO REGISTRADO (S/.)</th>"
            strTabla = strTabla & "</tr>"
            strTabla = strTabla & "</thead>"
            strTabla = strTabla & "</tbody>"
            '----------Fin Cabecera

            '---Cuerpo
            Dim codigo_poa As Integer = 0
            Dim StrCab As String = ""
            Dim rowspanCab As Integer = 1
            Dim rowspanTac As Integer = 1
            Dim StrCeldas As String = ""

            For i As Integer = 0 To dt.Rows.Count - 1
                codigo_poa = dt.Rows(i).Item("codigo_poa")
                ' Valido que no haya desbordamiento
                If i + 1 <= dt.Rows.Count - 1 Then
                    'Comparo con el Siguiente Poa
                    If codigo_poa = dt.Rows(i + 1).Item("codigo_poa") Then
                        'Agrego Fila
                        StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("descripcion_tac") & "</td>"
                        StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("resumen_acp") & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("ingresos_acp"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("ingresos_pto"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("egresos_acp"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("egresos_pto"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("utilidad_acp"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("utilidad_pto"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>"
                        If dt.Rows(i).Item("codigo_pto") > 0 Then
                            'si Tiene Presupuesto Colocamos el Link
                            StrCeldas = StrCeldas & "<a href='../../../librerianet/sesionesPOA.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf")
                            StrCeldas = StrCeldas & "&field=" & dt.Rows(i).Item("codigo_pto") & "&nombreCeco=" & dt.Rows(i).Item("descripcion_cco") & "&actividadPto=" & dt.Rows(i).Item("resumen_acp")
                            StrCeldas = StrCeldas & "&instancia=" & dt.Rows(i).Item("nombre_ins") & "&estado=" & dt.Rows(i).Item("nombre_est") & "&op=gesPto&opcion=EVAL_PTO&cb1=" & Me.ddlplan.SelectedValue
                            StrCeldas = StrCeldas & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlPoa.SelectedValue & "&cb4=" & Me.ddlestado.SelectedValue & "' title='Ver Detalle' ><b>Ver Detalle</b></a>"
                        End If
                        StrCeldas = StrCeldas & "</td></tr>"
                        'Aumento Rowspan
                        rowspanCab = rowspanCab + 1
                    Else
                        'Diferente Poa Agrego cabecera de Fila y Filas
                        StrCab = "<tr><td class='celda_combinada' rowspan=" & rowspanCab & ">" & dt.Rows(i).Item("nombre_poa") & "</td>"
                        StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("descripcion_tac") & "</td>"
                        StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("resumen_acp") & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("ingresos_acp"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("ingresos_pto"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("egresos_acp"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("egresos_pto"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("utilidad_acp"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("utilidad_pto"), 2) & "</td>"
                        StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>"
                        If dt.Rows(i).Item("codigo_pto") > 0 Then
                            'si Tiene Presupuesto Colocamos el Link
                            StrCeldas = StrCeldas & "<a href='../../../librerianet/sesionesPOA.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf")
                            StrCeldas = StrCeldas & "&field=" & dt.Rows(i).Item("codigo_pto") & "&nombreCeco=" & dt.Rows(i).Item("descripcion_cco") & "&actividadPto=" & dt.Rows(i).Item("resumen_acp")
                            StrCeldas = StrCeldas & "&instancia=" & dt.Rows(i).Item("nombre_ins") & "&estado=" & dt.Rows(i).Item("nombre_est") & "&op=gesPto&opcion=EVAL_PTO&cb1=" & Me.ddlplan.SelectedValue
                            StrCeldas = StrCeldas & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlPoa.SelectedValue & "&cb4=" & Me.ddlestado.SelectedValue & "' title='Ver Detalle' ><b>Ver Detalle</b></a>"
                        End If
                        StrCeldas = StrCeldas & "</td></tr>"
                        'Agrego Fila Con Rowspan de Cabecera de Fila , reinicio Rowspan y Seteo Fila
                        strTabla = strTabla & StrCab & StrCeldas
                        rowspanCab = 1
                        StrCeldas = ""
                    End If
                Else
                    'ultima Fila
                    StrCab = "<tr><td class='celda_combinada' rowspan=" & rowspanCab & ">" & dt.Rows(i).Item("nombre_poa") & "</td>"
                    StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("descripcion_tac") & "</td>"
                    StrCeldas = StrCeldas & "<td class='celda_combinada'>" & dt.Rows(i).Item("resumen_acp") & "</td>"
                    StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("ingresos_acp"), 2) & "</td>"
                    StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("ingresos_pto"), 2) & "</td>"
                    StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("egresos_acp"), 2) & "</td>"
                    StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("egresos_pto"), 2) & "</td>"
                    StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("utilidad_acp"), 2) & "</td>"
                    StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>" & FormatNumber(dt.Rows(i).Item("utilidad_pto"), 2) & "</td>"
                    StrCeldas = StrCeldas & "<td align='right' class='celda_combinada'>"
                    If dt.Rows(i).Item("codigo_pto") > 0 Then
                        'si Tiene Presupuesto Colocamos el Link
                        StrCeldas = StrCeldas & "<a href='../../../librerianet/sesionesPOA.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf")
                        StrCeldas = StrCeldas & "&field=" & dt.Rows(i).Item("codigo_pto") & "&nombreCeco=" & dt.Rows(i).Item("descripcion_cco") & "&actividadPto=" & dt.Rows(i).Item("resumen_acp")
                        StrCeldas = StrCeldas & "&instancia=" & dt.Rows(i).Item("nombre_ins") & "&estado=" & dt.Rows(i).Item("nombre_est") & "&op=gesPto&opcion=EVAL_PTO&cb1=" & Me.ddlplan.SelectedValue
                        StrCeldas = StrCeldas & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlPoa.SelectedValue & "&cb4=" & Me.ddlestado.SelectedValue & "' title='Ver Detalle' ><b>Ver Detalle</b></a>"
                    End If
                    StrCeldas = StrCeldas & "</td></tr>"
                    'Agrego Fila Con Rowspan de Cabecera de Fila
                    strTabla = strTabla & StrCab & StrCeldas
                End If

            Next
            '---Fin Cuerpo
            strTabla = strTabla & "</table>"
        Else
            strTabla = "<table width='100%'><tr><th>No se Encontraron Registros</th></tr></table>"
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
End Class

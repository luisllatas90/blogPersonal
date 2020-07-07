﻿
Partial Class indicadores_POA_PROTOTIPOS_Registrar_POA
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

    Sub CargaPoas(ByVal codigo_pla As Integer, ByVal codigo_ejp As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.ListaPoasxInstanciaEstado(codigo_pla, codigo_ejp, "PL", Request.QueryString("id"), Request.QueryString("ctf"))
        Me.ddlPoa.DataSource = dtt
        Me.ddlPoa.DataTextField = "descripcion"
        Me.ddlPoa.DataValueField = "codigo"
        Me.ddlPoa.DataBind()
        dtt.Dispose()
        obj = Nothing
        'Me.ddlPoa.Items.Insert(0, New ListItem("--SELECCIONE--", "0"))
    End Sub

    '============================Agregado fatima.vasquez 09-08-2018=====================================
    Sub CargaEstados()
        Dim ctf As Integer
        ctf = Request.QueryString("ctf")

        If ctf = 187 Then 'Dirección de Marketing
            Me.ddlestado.Items.Remove(ddlestado.Items.FindByText("Observados"))
            Me.ddlestado.Items.Insert(0, New ListItem("Observados", "22"))
            Me.ddlestado.Items.Remove(ddlestado.Items.FindByText("Pendientes"))
            Me.ddlestado.Items.Insert(0, New ListItem("Pendientes", "21"))
       
        End If
    End Sub
    '============================Fin fatima.vasquez 09-08-2018===========================================
  
    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        Try
            If Session("id_per") = "" Or Request.QueryString("id") = "" Then
                Response.Redirect("../../../sinacceso.html")
            End If

            If IsPostBack = False Then
                Dim Tabla As String
                CargaPlanes()
                CargaEjercicio()
                CargaEstados() 'Agregado fatima.vasquez 09-08-2018
                'usuario = Request.QueryString("id")
                'ctf = Request.QueryString("ctf")
                If Request.QueryString("accion") = "C" Then
                    Me.ddlplan.SelectedValue = Request.QueryString("cb1")
                    Me.ddlEjercicio.SelectedValue = Request.QueryString("cb2")
                    Me.ddlEjercicio_SelectedIndexChanged(sender, e)
                    Me.ddlPoa.SelectedValue = Request.QueryString("cb3")
                    Me.ddlestado.SelectedValue = Request.QueryString("cb4")
                    Me.hdfila.Value = Request.QueryString("codigo_acp")
                    Me.btnBuscar_Click(sender, e)
                Else
                    Tabla = CargaAlineamiento(Me.ddlplan.SelectedValue, Me.ddlPoa.SelectedValue, Me.ddlEjercicio.SelectedValue, Me.ddlestado.SelectedValue)
                    TablaAlineamiento.InnerHtml = Tabla
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim Tabla As String
            TablaAlineamiento.InnerHtml = ""
            If Me.ddlplan.SelectedValue <> 0 Then
                tabla = CargaAlineamiento(Me.ddlplan.SelectedValue, Me.ddlPoa.SelectedValue, Me.ddlEjercicio.SelectedValue, Me.ddlestado.SelectedValue)
                TablaAlineamiento.InnerHtml = Tabla
                Me.hdfila.Value = -1
                Me.lblrpta.Text = ""
                Me.aviso.Attributes.Clear()
            Else
                Response.Write("<script>alert('Seleccione Un Plan Estratégico')</script>")
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub treeAlineamiento_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles treeAlineamiento.SelectedNodeChanged
        Me.treeAlineamiento.SelectedNodeStyle.CssClass = "menu_elegido"
    End Sub


    Function CargaAlineamiento(ByVal codigo_pladdl As Integer, ByVal codigo_poaddl As Integer, ByVal codigo_ejp As Integer, ByVal estado As String) As String
        Dim strTabla As String
        strTabla = ""
        Dim colspan As Integer = 0
        Dim colspanTotal As Integer = 0
        'Para Celda
        Dim estilo_celda As String
        estilo_celda = " class='th_evaluacion'"
        Dim obj As New clsPlanOperativoAnual
        'Para cabecera
        Dim dt As New Data.DataTable
        Dim ctf As Integer = Request.QueryString("ctf")
        dt = obj.CabeceraEvaluarAlineamiento(codigo_pladdl, codigo_poaddl, codigo_ejp, Me.ddlestado.SelectedValue, estado, ctf)

        If dt.Rows.Count > 0 Then

            ' Para Plan,Perspectivas,Objetivos e Indicadores
            Dim codigo_pla As Integer = -1
            Dim codigo_obj As Integer = -1
            Dim StrPlan As String = ""
            Dim StrObjetivos As String = ""
            Dim StrIndicadores As String = ""

            Dim StrTemporal As String = ""
            Dim StrTipoActividad As String = ""
            Dim StrActividad As String = ""
            Dim StrActividadCab As String = ""
            Dim StrActividadDet As String = ""

            '******Cabecera******

            'total de Columnas
            colspanTotal = dt.Rows.Count

            'Indicadores
            For i As Integer = 0 To dt.Rows.Count - 1
                StrIndicadores = StrIndicadores & "<th " & estilo_celda & " colspan=" & colspan & ">" & dt.Rows(i).Item("descripcion_ind").ToString & "</th>"
            Next

            'Objetivos
            For i As Integer = 0 To dt.Rows.Count - 1
                codigo_obj = dt.Rows(i).Item("codigo_obj")
                If i + 1 <= dt.Rows.Count - 1 Then
                    If codigo_obj = dt.Rows(i + 1).Item("codigo_obj") Then
                        colspan = colspan + 1
                    Else
                        colspan = colspan + 1
                        StrObjetivos = StrObjetivos & "<th " & estilo_celda & " colspan=" & colspan & ">" & dt.Rows(i).Item("nombre_obj").ToString & "</th>"
                        colspan = 0
                    End If
                Else
                    colspan = colspan + 1
                    StrObjetivos = StrObjetivos & "<th " & estilo_celda & " colspan=" & colspan & ">" & dt.Rows(i).Item("nombre_obj").ToString & "</th>"
                End If
            Next

            'Planes
            colspan = 0
            For i As Integer = 0 To dt.Rows.Count - 1
                codigo_pla = dt.Rows(i).Item("codigo_pla")
                If i + 1 <= dt.Rows.Count - 1 Then
                    If codigo_pla = dt.Rows(i + 1).Item("codigo_pla") Then
                        colspan = colspan + 1
                    Else
                        colspan = colspan + 1
                        StrPlan = StrPlan & "<th " & estilo_celda & " colspan=" & colspan & ">" & dt.Rows(i).Item("periodo_pla").ToString & "</th>"
                        colspan = 0
                    End If
                Else
                    colspan = colspan + 1
                    StrPlan = StrPlan & "<th " & estilo_celda & " colspan=" & colspan & ">" & dt.Rows(i).Item("periodo_pla").ToString & "</th>"
                End If
            Next

            StrPlan = "<th width='150' " & estilo_celda & " rowspan='3'>PROGRAMA / PROYECTO</th>" & StrPlan
            StrPlan = "<th width='50' " & estilo_celda & " rowspan='3'>TIPO ACTIVIDAD</th>" & StrPlan
            StrPlan = "<th width='150' " & estilo_celda & " rowspan='3'>PLAN OPERATIVO ANUAL</th>" & StrPlan
            strTabla = strTabla & "<tr " & estilo_celda & ">" & StrPlan & "</tr>"
            strTabla = strTabla & "<tr " & estilo_celda & ">" & StrObjetivos & "</tr>"
            strTabla = strTabla & "<tr " & estilo_celda & ">" & StrIndicadores & "</tr>"
            '******Fin Cabecera******


            '----Cuerpo
            Dim dtbody As New Data.DataTable
            Dim codigo_acp As Integer = -1
            Dim codigo_tac As Integer = -1
            Dim codigo_poa As Integer = -1
            Dim descripcion_ind As String = ""
            Dim strFilas As String = ""
            dtbody = obj.EvaluarAlineamiento(codigo_pladdl, codigo_poaddl, codigo_ejp, Me.ddlestado.SelectedValue, estado, ctf)
            Dim rowspan As Integer = 0
            Dim rowspan_tac As Integer = 0
            Dim rowspan_acp As Integer = 0
            Dim marcas As String = ""
            Dim colorCelda As String = "#FFFFFF"

            For i As Integer = 0 To dtbody.Rows.Count - 1
                'If codigo_poa <> dt.Rows(i).Item("codigo_poa") And dt.Rows(i).Item("codigo_poa").ToString <> "" Then
                codigo_poa = dtbody.Rows(i).Item("codigo_poa")
                nombre_poa = dtbody.Rows(i).Item("nombre_poa")
                codigo_tac = dtbody.Rows(i).Item("codigo_tac")
                codigo_acp = dtbody.Rows(i).Item("codigo_acp")
                Dim codigo_iep As Integer = dtbody.Rows(i).Item("codigo_iep")
                descripcion_ind = dtbody.Rows(i).Item("descripcion_ind")

                'Dim fila_marcar As Integer = 18

                'Pintar Fila
                'Colocar Negrita programa observado, aprobado, pendiente
                If Me.hdfila.Value = dtbody.Rows(i).Item("codigo_acp") Then
                    If (codigo_iep = 3 And Request.QueryString("ctf") <> 187) Or codigo_iep = 22 Then 'Modificado por fatima.vasquez 09-08-2018
                        'Observado
                        colorCelda = "style='background-color:#FFCCCC;font-weight:bold;color:blue;'"
                    ElseIf (codigo_iep = 6 And Request.QueryString("ctf") <> 187) Or codigo_iep = 21 Then 'Modificado por fatima.vasquez 09-08-2018
                        'En Revision
                        colorCelda = "style='background-color:#CCFFFF;font-weight:bold;color:blue;'"
                    Else
                        'Otro
                        colorCelda = "style='background-color:#BFF0B6;font-weight:bold;color:blue;'"
                    End If
                Else
                    If (codigo_iep = 3 And Request.QueryString("ctf") <> 187) Or codigo_iep = 22 Then 'Modificado por fatima.vasquez 09-08-2018
                        'Observado
                        colorCelda = "style='background-color:#FFCCCC'"
                    ElseIf (codigo_iep = 6 And Request.QueryString("ctf") <> 187) Or codigo_iep = 21 Then 'Modificado por fatima.vasquez 09-08-2018
                        'En Revision
                        colorCelda = "style='background-color:#CCFFFF'"
                    Else
                        'Aprobado , Otro en Adelante
                        colorCelda = "style='background-color:#BFF0B6'"

                    End If

                End If

                If i + 1 <= dtbody.Rows.Count - 1 Then
                    'mismo codigo_poa
                    'If codigo_poa = dtbody.Rows(i + 1).Item("codigo_poa") Then
                    '    rowspan = rowspan + 1
                    '    '    'mismo tipo actividad
                    If codigo_tac = dtbody.Rows(i + 1).Item("codigo_tac") Then
                        rowspan_tac = rowspan_tac + 1
                        'mismo programa o proyecto
                        If i + 1 <= dtbody.Rows.Count - 1 Then
                            If codigo_acp = dtbody.Rows(i + 1).Item("codigo_acp") Then
                                rowspan_acp = rowspan_acp + 1

                                If rowspan_acp = 1 Then
                                    'primera fila, la que va a tener rowspan de filas de programa o proyecto
                                    For j As Integer = 0 To dt.Rows.Count - 1
                                        If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
                                            StrActividadCab = StrActividadCab & "<td style='text-align:center' class='celda_combinada' ><img src='../../Images/ok.gif' title='- " & dtbody.Rows(i).Item("descripcion_tac") & " : " & dtbody.Rows(i).Item("resumen_acp") & "&#13;- INDICADOR : " & descripcion_ind & " ' /></td>"
                                        Else
                                            StrActividadCab = StrActividadCab & "<td class='celda_combinada'></td>"
                                        End If
                                    Next
                                Else
                                    'demas Filas que va a tener rowspan de filas de programa o proyecto
                                    StrActividadDet = StrActividadDet & "<tr " & colorCelda & ">"
                                    For j As Integer = 0 To dt.Rows.Count - 1
                                        If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
                                            StrActividadDet = StrActividadDet & "<td style='text-align:center' class='celda_combinada' ><img src='../../Images/ok.gif' title='- " & dtbody.Rows(i).Item("descripcion_tac") & " : " & dtbody.Rows(i).Item("resumen_acp") & "&#13;- INDICADOR : " & descripcion_ind & " ' /></td>"
                                        Else
                                            StrActividadDet = StrActividadDet & "<td class='celda_combinada'></td>"
                                        End If
                                    Next
                                    StrActividadDet = StrActividadDet & "</tr>"
                                End If

                            Else
                                'El Siguiente Programa o proyecto sera diferente entonces:
                                rowspan_acp = rowspan_acp + 1
                                If rowspan_acp = 1 Then
                                    For j As Integer = 0 To dt.Rows.Count - 1
                                        If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
                                            StrActividadCab = StrActividadCab & "<td style='text-align:center' class='celda_combinada' ><img src='../../Images/ok.gif' title='- " & dtbody.Rows(i).Item("descripcion_tac") & " : " & dtbody.Rows(i).Item("resumen_acp") & "&#13;- INDICADOR : " & descripcion_ind & "' /></td>"
                                        Else
                                            StrActividadCab = StrActividadCab & "<td class='celda_combinada'></td>"
                                        End If
                                    Next
                                Else
                                    StrActividadDet = StrActividadDet & "<tr " & colorCelda & ">"
                                    For j As Integer = 0 To dt.Rows.Count - 1
                                        If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
                                            StrActividadDet = StrActividadDet & "<td style='text-align:center' class='celda_combinada' ><img src='../../Images/ok.gif' title='- " & dtbody.Rows(i).Item("descripcion_tac") & " : " & dtbody.Rows(i).Item("resumen_acp") & "&#13;- INDICADOR : " & descripcion_ind & " ' /></td>"
                                        Else
                                            StrActividadDet = StrActividadDet & "<td class='celda_combinada'></td>"
                                        End If
                                    Next
                                    StrActividadDet = StrActividadDet & "</tr>"
                                End If
                                'concateno la primera fila con el salto de fila
                                If rowspan_tac = 1 Then
                                    StrActividad = StrActividad & "<tr " & colorCelda & "><td class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                                    StrActividad = StrActividad & "<td class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("descripcion_tac") & "</td>"
                                    StrActividad = StrActividad & "<td width='150px' class='celda_combinada' rowspan= " & rowspan_acp & " ><a href='FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id")
                                    StrActividad = StrActividad & "&cb1=" & Me.ddlplan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlPoa.SelectedValue & "&cb4=" & Me.ddlestado.SelectedValue
                                    StrActividad = StrActividad & "&tipo_acc=PL&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "' >" & dtbody.Rows(i).Item("resumen_acp") & "</a><br><div style='text-align:center' class='Dias'><b>Hace " & dtbody.Rows(i).Item("tiempo") & "</b></div><div style='text-align:right'>&nbsp;"
                                    If codigo_iep = 3 Or codigo_iep = 6 Or codigo_iep = 21 Or codigo_iep = 22 Then 'Modificado fatima.vasquez 09-08-2018
                                        StrActividad = StrActividad & "<img style='cursor:pointer' src='../../Images/Okey.gif' title='Aprobar' tipo='A' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "' cod_pla='" & codigo_pla & "' cod_poa='" & codigo_poa & "'/>" 'Modificado fatima.vasquez 09-08-2018
                                        StrActividad = StrActividad & "&nbsp;<img style='cursor:pointer' src='../../Images/menus/noconforme_small.gif' title='Observar' tipo='O' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "' cod_pla='" & codigo_pla & "' cod_poa='" & codigo_poa & "'/>" 'Modificado fatima.vasquez 09-08-2018
                                    End If
                                    StrActividad = StrActividad & StrActividadCab & "</div></td></tr>"
                                    StrActividad = StrActividad & StrActividadDet
                                    StrActividadCab = ""
                                    StrActividadDet = ""
                                    rowspan_acp = 0
                                Else
                                    'concateno las demas filas del programa o proyecto
                                    StrActividad = StrActividad & "<tr " & colorCelda & "><td class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                                    StrActividad = StrActividad & "<td class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("descripcion_tac") & "</td>"
                                    StrActividad = StrActividad & "<td class='celda_combinada' rowspan= " & rowspan_acp & " ><a href='FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id")
                                    StrActividad = StrActividad & "&cb1=" & Me.ddlplan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlPoa.SelectedValue & "&cb4=" & Me.ddlestado.SelectedValue
                                    StrActividad = StrActividad & "&tipo_acc=PL&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "' >" & dtbody.Rows(i).Item("resumen_acp") & "</a><br><div style='text-align:center' class='Dias'><b>Hace " & dtbody.Rows(i).Item("tiempo") & "</b></div><div style='text-align:right'>&nbsp;"
                                    If codigo_iep = 3 Or codigo_iep = 6 Or codigo_iep = 21 Or codigo_iep = 22 Then 'Modificado fatima.vasquez 09-08-2018
                                        StrActividad = StrActividad & "<img style='cursor:pointer' src='../../Images/Okey.gif' title='Aprobar' tipo='A' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "' cod_pla='" & codigo_pla & "' cod_poa='" & codigo_poa & "'/>" 'Modificado fatima.vasquez 09-08-2018
                                        StrActividad = StrActividad & "&nbsp;<img style='cursor:pointer' src='../../Images/menus/noconforme_small.gif' title='Observar' tipo='O' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "' cod_pla='" & codigo_pla & "' cod_poa='" & codigo_poa & "'/>" 'Modificado fatima.vasquez 09-08-2018
                                    End If
                                    StrActividad = StrActividad & StrActividadCab & "</div></td></tr>"
                                    StrActividad = StrActividad & StrActividadDet
                                    StrActividadCab = ""
                                    StrActividadDet = ""
                                    rowspan_acp = 0
                                    End If

                            End If

                        Else
                            'El Siguiente Programa o proyecto sera diferente entonces:
                            rowspan_acp = rowspan_acp + 1
                            If rowspan_acp = 1 Then
                                For j As Integer = 0 To dt.Rows.Count - 1
                                    If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
                                        StrActividadCab = StrActividadCab & "<td style='text-align:center' class='celda_combinada' ><img src='../../Images/ok.gif' title='- " & dtbody.Rows(i).Item("descripcion_tac") & " : " & dtbody.Rows(i).Item("resumen_acp") & "&#13;- INDICADOR : " & descripcion_ind & " ' /></td>"
                                    Else
                                        StrActividadCab = StrActividadCab & "<td class='celda_combinada'></td>"
                                    End If
                                Next
                            Else
                                StrActividadDet = StrActividadDet & "<tr " & colorCelda & ">"
                                For j As Integer = 0 To dt.Rows.Count - 1
                                    If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
                                        StrActividadDet = StrActividadDet & "<td style='text-align:center' class='celda_combinada' ><img src='../../Images/ok.gif' title='- " & dtbody.Rows(i).Item("descripcion_tac") & " : " & dtbody.Rows(i).Item("resumen_acp") & "&#13;- INDICADOR : " & descripcion_ind & " ' /></td>"
                                    Else
                                        StrActividadDet = StrActividadDet & "<td class='celda_combinada'></td>"
                                    End If
                                Next
                                StrActividadDet = StrActividadDet & "</tr>"
                            End If

                            'concateno la primera fila con el salto de fila
                            If rowspan_tac = 1 Then
                                StrActividad = StrActividad & "<tr " & colorCelda & "><td class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                                StrActividad = StrActividad & "<td class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("descripcion_tac") & "</td>"
                                StrActividad = StrActividad & "<td class='celda_combinada' rowspan= " & rowspan_acp & " ><a> href='FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id")
                                StrActividad = StrActividad & "&cb1=" & Me.ddlplan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlPoa.SelectedValue & "&cb4=" & Me.ddlestado.SelectedValue
                                StrActividad = StrActividad & "&tipo_acc=PL&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "' >" & dtbody.Rows(i).Item("resumen_acp") & "</a><br><div style='text-align:center' class='Dias'><b>Hace " & dtbody.Rows(i).Item("tiempo") & "</b></div><div style='text-align:right'>&nbsp;"
                                If codigo_iep = 3 Or codigo_iep = 6 Or codigo_iep = 21 Or codigo_iep = 22 Then 'Modificado fatima.vasquez 09-08-2018
                                    StrActividad = StrActividad & "<img style='cursor:pointer' src='../../Images/Okey.gif' title='Aprobar' tipo='A' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "'  cod_pla='" & codigo_pla & "' cod_poa='" & codigo_poa & "'/>" 'Modificado fatima.vasquez 09-08-2018
                                    StrActividad = StrActividad & "&nbsp;<img style='cursor:pointer' src='../../Images/menus/noconforme_small.gif' title='Observar' tipo='O' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "'  cod_pla='" & codigo_pla & "' cod_poa='" & codigo_poa & "'/>" 'Modificado fatima.vasquez 09-08-2018
                                End If
                                StrActividad = StrActividad & StrActividadCab & "</div></td></tr>"
                                StrActividad = StrActividad & StrActividadDet
                                StrActividadCab = ""
                                StrActividadDet = ""
                                rowspan_acp = 0
                            Else
                                'concateno las demas filas del programa o proyecto
                                StrActividad = StrActividad & "<tr " & colorCelda & "><td class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                                StrActividad = StrActividad & "<td class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("descripcion_tac") & "</td>"
                                StrActividad = StrActividad & "<td class='celda_combinada' rowspan= " & rowspan_acp & " ><a href='FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id")
                                StrActividad = StrActividad & "&cb1=" & Me.ddlplan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlPoa.SelectedValue & "&cb4=" & Me.ddlestado.SelectedValue
                                StrActividad = StrActividad & "&tipo_acc=PL&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "' >" & dtbody.Rows(i).Item("resumen_acp") & "</a><br><div style='text-align:center' class='Dias'><b>Hace " & dtbody.Rows(i).Item("tiempo") & "</b></div><div style='text-align:right'>&nbsp;"
                                If codigo_iep = 3 Or codigo_iep = 6 Or codigo_iep = 21 Or codigo_iep = 22 Then 'Modificado fatima.vasquez 09-08-2018
                                    StrActividad = StrActividad & "<img style='cursor:pointer' src='../../Images/Okey.gif' title='Aprobar' tipo='A' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "'  cod_pla='" & codigo_pla & "' cod_poa='" & codigo_poa & "'/>" 'Modificado fatima.vasquez 09-08-2018
                                    StrActividad = StrActividad & "&nbsp;<img style='cursor:pointer' src='../../Images/menus/noconforme_small.gif' title='Observar' tipo='O' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "'  cod_pla='" & codigo_pla & "' cod_poa='" & codigo_poa & "'/>" 'Modificado fatima.vasquez 09-08-2018
                                End If
                                StrActividad = StrActividad & StrActividadCab & "</div></td></tr>"
                                StrActividad = StrActividad & StrActividadDet
                                StrActividadCab = ""
                                StrActividadDet = ""
                                rowspan_acp = 0
                                End If

                            End If

                    Else
                        'else diferente tipo actividad
                        'El Siguiente Programa o proyecto sera diferente entonces:
                        rowspan_acp = rowspan_acp + 1
                        If rowspan_acp = 1 Then
                            For j As Integer = 0 To dt.Rows.Count - 1
                                If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
                                    StrActividadCab = StrActividadCab & "<td style='text-align:center' class='celda_combinada' ><img src='../../Images/ok.gif' title='- " & dtbody.Rows(i).Item("descripcion_tac") & " : " & dtbody.Rows(i).Item("resumen_acp") & "&#13;- INDICADOR : " & descripcion_ind & " ' /></td>"
                                Else
                                    StrActividadCab = StrActividadCab & "<td class='celda_combinada'></td>"
                                End If
                            Next
                        Else
                            StrActividadDet = StrActividadDet & "<tr " & colorCelda & ">"
                            For j As Integer = 0 To dt.Rows.Count - 1
                                If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
                                    StrActividadDet = StrActividadDet & "<td style='text-align:center' class='celda_combinada' ><img src='../../Images/ok.gif' title='- " & dtbody.Rows(i).Item("descripcion_tac") & " : " & dtbody.Rows(i).Item("resumen_acp") & "&#13;- INDICADOR : " & descripcion_ind & " ' /></td>"
                                Else
                                    StrActividadDet = StrActividadDet & "<td class='celda_combinada'></td>"
                                End If
                            Next
                            StrActividadDet = StrActividadDet & "</tr>"
                        End If

                        'concateno la primera fila con el salto de fila
                        If rowspan_tac = 1 Then
                            '''''''''''''''''''''''''''''''fila combinada''''''''''''''''''''''''''''''''''''''''''''''''''
                            'StrActividad = StrActividad & "<tr><td style='font-size:9px' class='celda_combinada' colspan= '" & colspanTotal & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                            StrActividad = StrActividad & "<tr " & colorCelda & "><td class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                            StrActividad = StrActividad & "<td class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("descripcion_tac") & "</td>"
                            StrActividad = StrActividad & "<td class='celda_combinada' rowspan= " & rowspan_acp & " ><a href='FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id")
                            StrActividad = StrActividad & "&cb1=" & Me.ddlplan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlPoa.SelectedValue & "&cb4=" & Me.ddlestado.SelectedValue
                            StrActividad = StrActividad & "&tipo_acc=PL&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "' >" & dtbody.Rows(i).Item("resumen_acp") & "</a><br><div style='text-align:center' class='Dias'><b>Hace " & dtbody.Rows(i).Item("tiempo") & "</b></div><div style='text-align:right'>&nbsp;"
                            If codigo_iep = 3 Or codigo_iep = 6 Or codigo_iep = 21 Or codigo_iep = 22 Then 'Modificado fatima.vasquez 09-08-2018
                                StrActividad = StrActividad & "<img style='cursor:pointer' src='../../Images/Okey.gif' title='Aprobar' tipo='A' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "'  cod_pla='" & codigo_pla & "' cod_poa='" & codigo_poa & "'/>" 'Modificado fatima.vasquez 09-08-2018
                                StrActividad = StrActividad & "&nbsp;<img style='cursor:pointer' src='../../Images/menus/noconforme_small.gif' title='Observar' tipo='O' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "'  cod_pla='" & codigo_pla & "' cod_poa='" & codigo_poa & "'/>" 'Modificado fatima.vasquez 09-08-2018
                            End If
                            StrActividad = StrActividad & StrActividadCab & "</div></td></tr>"
                            StrActividad = StrActividad & StrActividadDet
                            StrActividadCab = ""
                            StrActividadDet = ""
                            rowspan_acp = 0
                        Else
                            'concateno las demas filas del programa o proyecto
                            StrActividad = StrActividad & "<tr " & colorCelda & "><td class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                            StrActividad = StrActividad & "<td class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("descripcion_tac") & "</td>"
                            StrActividad = StrActividad & "<td class='celda_combinada' rowspan= " & rowspan_acp & " ><a href='FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id")
                            StrActividad = StrActividad & "&cb1=" & Me.ddlplan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlPoa.SelectedValue & "&cb4=" & Me.ddlestado.SelectedValue
                            StrActividad = StrActividad & "&tipo_acc=PL&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "' >" & dtbody.Rows(i).Item("resumen_acp") & "</a><br><div style='text-align:center' class='Dias'><b>Hace " & dtbody.Rows(i).Item("tiempo") & "</b></div><div style='text-align:right'>&nbsp;"
                            If codigo_iep = 3 Or codigo_iep = 6 Or codigo_iep = 21 Or codigo_iep = 22 Then 'Modificado fatima.vasquez 09-08-2018
                                StrActividad = StrActividad & "<img style='cursor:pointer' src='../../Images/Okey.gif' title='Aprobar' tipo='A' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "'  cod_pla='" & codigo_pla & "' cod_poa='" & codigo_poa & "'/>" 'Modificado fatima.vasquez 09-08-2018
                                StrActividad = StrActividad & "&nbsp;<img style='cursor:pointer' src='../../Images/menus/noconforme_small.gif' title='Observar' tipo='O' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "'  cod_pla='" & codigo_pla & "' cod_poa='" & codigo_poa & "'/>" 'Modificado fatima.vasquez 09-08-2018
                            End If
                            StrActividad = StrActividad & StrActividadCab & "</div></td></tr>"
                            StrActividad = StrActividad & StrActividadDet
                            StrActividadCab = ""
                            StrActividadDet = ""
                            rowspan_acp = 0
                            End If
                        End If
                Else
                    'else diferente poa
                    'El Siguiente Programa o proyecto sera diferente entonces:
                    rowspan_acp = rowspan_acp + 1
                    If rowspan_acp = 1 Then
                        For j As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
                                StrActividadCab = StrActividadCab & "<td style='text-align:center' class='celda_combinada' ><img src='../../Images/ok.gif' title='- " & dtbody.Rows(i).Item("descripcion_tac") & " : " & dtbody.Rows(i).Item("resumen_acp") & "&#13;- INDICADOR : " & descripcion_ind & " ' /></td>"
                            Else
                                StrActividadCab = StrActividadCab & "<td class='celda_combinada'></td>"
                            End If
                        Next
                    Else
                        StrActividadDet = StrActividadDet & "<tr " & colorCelda & ">"
                        For j As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
                                StrActividadDet = StrActividadDet & "<td style='text-align:center' class='celda_combinada' ><img src='../../Images/ok.gif' title='- " & dtbody.Rows(i).Item("descripcion_tac") & " : " & dtbody.Rows(i).Item("resumen_acp") & "&#13;- INDICADOR : " & descripcion_ind & " ' /></td>"
                            Else
                                StrActividadDet = StrActividadDet & "<td class='celda_combinada'></td>"
                            End If
                        Next
                        StrActividadDet = StrActividadDet & "</tr>"
                    End If
                    'concateno la primera fila con el salto de fila
                    If rowspan_tac = 1 Then
                        StrActividad = StrActividad & "<tr " & colorCelda & "><td class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                        StrActividad = StrActividad & "<td class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("descripcion_tac") & "</td>"
                        StrActividad = StrActividad & "<td class='celda_combinada' rowspan= " & rowspan_acp & " ><a href='FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id")
                        StrActividad = StrActividad & "&cb1=" & Me.ddlplan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlPoa.SelectedValue & "&cb4=" & Me.ddlestado.SelectedValue
                        StrActividad = StrActividad & "&tipo_acc=PL&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "' >" & dtbody.Rows(i).Item("resumen_acp") & "</a><br><div style='text-align:center' class='Dias'><b>Hace " & dtbody.Rows(i).Item("tiempo") & "</b></div><div style='text-align:right'>&nbsp;"
                        If codigo_iep = 3 Or codigo_iep = 6 Or codigo_iep = 21 Or codigo_iep = 22 Then 'Modificado fatima.vasquez 09-08-2018
                            StrActividad = StrActividad & "<img style='cursor:pointer' src='../../Images/Okey.gif' title='Aprobar' tipo='A' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "'  cod_pla='" & codigo_pla & "' cod_poa='" & codigo_poa & "'/>" 'Modificado fatima.vasquez 09-08-2018
                            StrActividad = StrActividad & "&nbsp;<img style='cursor:pointer' src='../../Images/menus/noconforme_small.gif' title='Observar' tipo='O' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "'  cod_pla='" & codigo_pla & "' cod_poa='" & codigo_poa & "'/>" 'Modificado fatima.vasquez 09-08-2018
                        End If
                        StrActividad = StrActividad & StrActividadCab & "</div></td></tr>"
                        StrActividad = StrActividad & StrActividadDet
                        StrActividadCab = ""
                        StrActividadDet = ""
                        rowspan_acp = 0
                    Else
                        'concateno las demas filas del programa o proyecto
                        StrActividad = StrActividad & "<tr " & colorCelda & "><td class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                        StrActividad = StrActividad & "<td class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("descripcion_tac") & "</td>"
                        StrActividad = StrActividad & "<td class='celda_combinada' rowspan= " & rowspan_acp & " ><a href='FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id")
                        StrActividad = StrActividad & "&cb1=" & Me.ddlplan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Me.ddlPoa.SelectedValue & "&cb4=" & Me.ddlestado.SelectedValue
                        StrActividad = StrActividad & "&tipo_acc=PL&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "' >" & dtbody.Rows(i).Item("resumen_acp") & "</a><br><div style='text-align:center' class='Dias'><b>Hace " & dtbody.Rows(i).Item("tiempo") & "</b></div><div style='text-align:right'>&nbsp;"
                        If codigo_iep = 3 Or codigo_iep = 6 Or codigo_iep = 21 Or codigo_iep = 22 Then 'Modificado fatima.vasquez 09-08-2018
                            StrActividad = StrActividad & "<img style='cursor:pointer' src='../../Images/Okey.gif' title='Aprobar' tipo='A' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "'  cod_pla='" & codigo_pla & "' cod_poa='" & codigo_poa & "'/>" 'Modificado fatima.vasquez 09-08-2018
                            StrActividad = StrActividad & "&nbsp;<img style='cursor:pointer' src='../../Images/menus/noconforme_small.gif' title='Observar' tipo='O' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "'  cod_pla='" & codigo_pla & "' cod_poa='" & codigo_poa & "'/>" 'Modificado fatima.vasquez 09-08-2018
                        End If
                        StrActividad = StrActividad & StrActividadCab & "</div></td></tr>"
                        StrActividad = StrActividad & StrActividadDet
                        StrActividadCab = ""
                        StrActividadDet = ""
                        rowspan_acp = 0
                        End If
                    End If ' end if dtbody.rowcount

            Next
            '    '---- Fin Cuerpo

            strTabla = strTabla & StrActividad
            '
            strTabla = "<div id='div1'><table style='width:98%; border-collapse:collapse; border:1px;' border='1' CellSpacing='0' CellPadding='2' >" & strTabla & "</table></div>"

        End If 'end if 
        Return strTabla

    End Function

    Protected Sub ddlplan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlplan.SelectedIndexChanged
        CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
    End Sub

    Protected Sub btnAprobacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAprobacion.Click
        Try
            If (Session("id_per") = "") Then
                Response.Redirect("../../../sinacceso.html")
            End If
            Dim obj As New clsPlanOperativoAnual
            Dim dt As New Data.DataTable
            Dim mensaje As String
            '==================Agregado fatima.vasquez 09-08-2018=======================
            Dim codigo_poa As Integer = Me.hdcodigo_poa.Value
            Dim tipo_cco As Integer = obj.POA_verificarCentroCosto(codigo_poa, Me.hdcodigo_acp.Value)
            Dim ctf As Integer
            ctf = Request.QueryString("ctf")

            If tipo_cco = 1 And ctf = 187 Then 'Programa de Educación Continua 'Dirección de Marketing
                'Aprobado por Marketing
                mensaje = obj.InstanciaEstadoActividad(Me.hdcodigo_acp.Value, 6, Request.QueryString("id"))

                '=============== ENVIAR CORREO ===============================
                Dim objemail As New ClsMail
                Dim cuerpo, receptor, AsuntoCorreo As String
                dt = obj.ConsultaResponsablesActividad(1, Me.hdcodigo_acp.Value, "")

                cuerpo = "<html>"
                cuerpo = cuerpo & "<head>"
                cuerpo = cuerpo & "<title></title>"
                cuerpo = cuerpo & "<style>"
                cuerpo = cuerpo & ".FontType{font-family: calibri; font-size:12px; }"
                cuerpo = cuerpo & "</style>"
                cuerpo = cuerpo & "</head>"
                cuerpo = cuerpo & "<body>"
                cuerpo = cuerpo & "<table border=0 cellpadding=0 class=""FontType"">"
                cuerpo = cuerpo & "<tr><td colspan=2 ><b>Estimado(a):</b></td></tr>"
                cuerpo = cuerpo & "<tr><td colspan=2></br></br>El Programa/Proyecto  <b>" & dt.Rows(0).Item("resumen_acp").ToString & "</b>  ha Sido  <b>APROBADO</b>  por el ÁREA DE MARKETING.</td></tr>"
                cuerpo = cuerpo & "<tr><td colspan=2></br></br>Saludos Cordiales</td></tr>"
                cuerpo = cuerpo & "</table>"
                cuerpo = cuerpo & "</body>"
                cuerpo = cuerpo & "</html>"

                If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                    receptor = dt.Rows(0).Item("receptores").ToString
                Else
                    receptor = "hcano@usat.edu.pe"
                End If


                AsuntoCorreo = "[Evaluación de Programa/Proyecto : " & dt.Rows(0).Item("resumen_acp").ToString & "]"
                objemail.EnviarMail("campusvirtual@usat.edu.pe", "Evaluación de Programa/Proyecto", receptor, AsuntoCorreo, cuerpo, True)
                '=============== FIN ENVIAR CORREO ===============================

                obj.POA_ActualizarObservacion(Me.hdcodigo_acp.Value, " - RESUELTO", Request.QueryString("id"), 1, 22)
            Else
                'Aprobado por Planificacion
                mensaje = obj.InstanciaEstadoActividad(Me.hdcodigo_acp.Value, 9, Request.QueryString("id"))

                '=============== ENVIAR CORREO ===============================
                Dim objemail As New ClsMail
                Dim cuerpo, receptor, AsuntoCorreo As String
                dt = obj.ConsultaResponsablesActividad(1, Me.hdcodigo_acp.Value, "")

                cuerpo = "<html>"
                cuerpo = cuerpo & "<head>"
                cuerpo = cuerpo & "<title></title>"
                cuerpo = cuerpo & "<style>"
                cuerpo = cuerpo & ".FontType{font-family: calibri; font-size:12px; }"
                cuerpo = cuerpo & "</style>"
                cuerpo = cuerpo & "</head>"
                cuerpo = cuerpo & "<body>"
                cuerpo = cuerpo & "<table border=0 cellpadding=0 class=""FontType"">"
                cuerpo = cuerpo & "<tr><td colspan=2 ><b>Estimado(a):</b></td></tr>"
                cuerpo = cuerpo & "<tr><td colspan=2></br></br>El Programa/Proyecto  <b>" & dt.Rows(0).Item("resumen_acp").ToString & "</b>  ha Sido  <b>APROBADO</b>  por el ÁREA DE PLANIFICACIÓN.</td></tr>"
                cuerpo = cuerpo & "<tr><td colspan=2></br></br><i><b>(*)</b> El Personal de Apoyo indicado puede dirigirse al módulo de PRESUPUESTO para registrar el correspondiente al Programa/Proyecto (sólo si éste cuenta con uno asignado).</i></td></tr>"
                cuerpo = cuerpo & "<tr><td colspan=2></br></br>Saludos Cordiales</td></tr>"
                cuerpo = cuerpo & "</table>"
                cuerpo = cuerpo & "</body>"
                cuerpo = cuerpo & "</html>"

                If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                    receptor = dt.Rows(0).Item("receptores").ToString
                Else
                    receptor = "hcano@usat.edu.pe"
                End If

                AsuntoCorreo = "[Evaluación de Programa/Proyecto : " & dt.Rows(0).Item("resumen_acp").ToString & "]"
                objemail.EnviarMail("campusvirtual@usat.edu.pe", "Evaluación de Programa/Proyecto", receptor, AsuntoCorreo, cuerpo, True)
                '=============== FIN ENVIAR CORREO ===============================

                obj.POA_ActualizarObservacion(Me.hdcodigo_acp.Value, " - RESUELTO", Request.QueryString("id"), 1, 3)
            End If

            Call wf_RegistrarPresupuesto()
            Me.ddlplan.SelectedValue = hdcodigo_pla.Value
            Me.btnBuscar_Click(sender, e)
            'Para avisos
            Me.lblrpta.Text = "El Programa/Proyecto a sido Aprobado con éxito."
            Me.aviso.Attributes.Add("class", "mensajeExito")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Sub wf_RegistrarPresupuesto()
        'Insertar Items en Presupuesto
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        Dim dtRpta As New Data.DataTable
        Dim codigoArt, codigoRub, codigoPlla As Int64
        codigoArt = 0
        codigoRub = 0
        codigoPlla = 0
        Dim Meses() As String = New String() {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SETIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"}

        '*** Guarda cabecera de presupuesto ***
        dt = obj.PRESU_ListaItemActividad(Me.hdcodigo_acp.Value)
        For i As Integer = 0 To dt.Rows.Count - 1
            codigoArt = IIf(dt.Rows(i).Item("codigo_Art").ToString.Length > 0, dt.Rows(i).Item("codigo_Art"), 0)
            codigoRub = IIf(dt.Rows(i).Item("codigo_Rub").ToString.Length > 0, dt.Rows(i).Item("codigo_Rub"), 0)
            codigoPlla = IIf(dt.Rows(i).Item("codigo_Cplla").ToString.Length > 0, dt.Rows(i).Item("codigo_Cplla"), 0)

            dtRpta = obj.AgregarPresupuesto_V2(dt.Rows(i).Item("codigo_ejp"), dt.Rows(i).Item("codigo_cco"), codigoArt, _
            codigoRub, codigoPlla, dt.Rows(i).Item("detalle").ToString, dt.Rows(i).Item("iduni"), _
            dt.Rows(i).Item("precio"), dt.Rows(i).Item("cantidad"), dt.Rows(i).Item("vigencia"), dt.Rows(i).Item("tipo"), dt.Rows(i).Item("codigo_Per"), _
            dt.Rows(i).Item("indicoCantidades"), dt.Rows(i).Item("forzarDuplicado"), dt.Rows(i).Item("codigo_dap"))

            If dtRpta.Rows.Count > 0 Then
                For j As Int16 = 1 To 12
                    obj.AgregarDetalleEjecucion_V2(dtRpta.Rows(0).Item("rpta").ToString, Meses(j - 1), dt.Rows(i).Item("precio"), 0)
                Next
            End If
        Next
    End Sub

    Protected Sub btnObservacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnObservacion.Click
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim mensaje As String
            mensaje = obj.POA_ValidarPresupuesto(Me.hdcodigo_acp.Value)
            If mensaje = 0 Then

                '==================Agregado fatima.vasquez 09-08-2018=======================
                Dim codigo_poa As Integer = Me.hdcodigo_poa.Value
                Dim tipo_cco As Integer = obj.POA_verificarCentroCosto(codigo_poa, Me.hdcodigo_acp.Value)
                Dim ctf As Integer
                ctf = Request.QueryString("ctf")

                If tipo_cco = 1 And ctf = 187 Then 'Programa de Educación Continua 'Dirección de Marketing
                    ' Cambia Instancia-Estado a Registro Poa - Observado por Marketing
                    mensaje = obj.InstanciaEstadoActividad(Me.hdcodigo_acp.Value, 22, Request.QueryString("id"))
                    'Inserta Observación 
                    mensaje = obj.POA_ActualizarObservacion(Me.hdcodigo_acp.Value, Me.txtobservacion.Value, Request.QueryString("id"), 0, 22)
                    Me.ddlplan.SelectedValue = hdcodigo_pla.Value

                    '=============== ENVIAR CORREO ===============================
                    Dim objemail As New ClsMail
                    Dim dt As New Data.DataTable
                    Dim cuerpo, receptor, AsuntoCorreo As String
                    dt = obj.ConsultaResponsablesActividad(1, Me.hdcodigo_acp.Value, "")

                    cuerpo = "<html>"
                    cuerpo = cuerpo & "<head>"
                    cuerpo = cuerpo & "<title></title>"
                    cuerpo = cuerpo & "<style>"
                    cuerpo = cuerpo & ".FontType{font-family: calibri; font-size:12px; }"
                    cuerpo = cuerpo & "</style>"
                    cuerpo = cuerpo & "</head>"
                    cuerpo = cuerpo & "<body>"
                    cuerpo = cuerpo & "<table border=0 cellpadding=0 class=""FontType"">"
                    cuerpo = cuerpo & "<tr><td colspan=2 ><b>Estimado(a):</b></td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>El Programa/Proyecto  <b>" & dt.Rows(0).Item("resumen_acp").ToString & "</b>  ha Sido  <b>OBSERVADO</b>  por el ÁREA DE MARKETING, indicandose lo siguiente:</td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>" + Me.txtobservacion.Value + "</td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br><i><b>(*)</b> Para corregir las Observaciones deberá ingresar al Módulo <b>PLAN OPERATIVO ANUAL</b></i></td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>Saludos Cordiales</td></tr>"
                    cuerpo = cuerpo & "</table>"
                    cuerpo = cuerpo & "</body>"
                    cuerpo = cuerpo & "</html>"

                    If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                        receptor = dt.Rows(0).Item("receptores").ToString
                    Else
                        receptor = "hcano@usat.edu.pe"
                    End If

                    AsuntoCorreo = "[Evaluación de Programa/Proyecto : " & dt.Rows(0).Item("resumen_acp").ToString & "]"
                    objemail.EnviarMail("campusvirtual@usat.edu.pe", "Evaluación de Programa/Proyecto", receptor, AsuntoCorreo, cuerpo, True)
                    '=============== FIN ENVIAR CORREO ===============================
                Else
                    ' Cambia Instancia-Estado a Registro Poa - Observado por Planificación
                    mensaje = obj.InstanciaEstadoActividad(Me.hdcodigo_acp.Value, 3, Request.QueryString("id"))
                    'Inserta Observación.
                    'codigo_iep=3 - InstanciaActividad_POA
                    mensaje = obj.POA_ActualizarObservacion(Me.hdcodigo_acp.Value, Me.txtobservacion.Value, Request.QueryString("id"), 0, 3)
                    Me.ddlplan.SelectedValue = hdcodigo_pla.Value

                    '=============== ENVIAR CORREO ===============================
                    Dim objemail As New ClsMail
                    Dim dt As New Data.DataTable
                    Dim cuerpo, receptor, AsuntoCorreo As String
                    dt = obj.ConsultaResponsablesActividad(1, Me.hdcodigo_acp.Value, "")

                    cuerpo = "<html>"
                    cuerpo = cuerpo & "<head>"
                    cuerpo = cuerpo & "<title></title>"
                    cuerpo = cuerpo & "<style>"
                    cuerpo = cuerpo & ".FontType{font-family: calibri; font-size:12px; }"
                    cuerpo = cuerpo & "</style>"
                    cuerpo = cuerpo & "</head>"
                    cuerpo = cuerpo & "<body>"
                    cuerpo = cuerpo & "<table border=0 cellpadding=0 class=""FontType"">"
                    cuerpo = cuerpo & "<tr><td colspan=2 ><b>Estimado(a):</b></td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>El Programa/Proyecto  <b>" & dt.Rows(0).Item("resumen_acp").ToString & "</b>  ha Sido  <b>OBSERVADO</b>  por el ÁREA DE PLANIFICACIÓN, indicandose lo siguiente:</td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>" + Me.txtobservacion.Value + "</td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br><i><b>(*)</b> Para corregir las Observaciones deberá ingresar al Módulo <b>PLAN OPERATIVO ANUAL</b></i></td></tr>"
                    cuerpo = cuerpo & "<tr><td colspan=2></br></br>Saludos Cordiales</td></tr>"
                    cuerpo = cuerpo & "</table>"
                    cuerpo = cuerpo & "</body>"
                    cuerpo = cuerpo & "</html>"

                    If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                        receptor = dt.Rows(0).Item("receptores").ToString
                    Else
                        receptor = "hcano@usat.edu.pe"
                    End If

                    AsuntoCorreo = "[Evaluación de Programa/Proyecto : " & dt.Rows(0).Item("resumen_acp").ToString & "]"
                    objemail.EnviarMail("campusvirtual@usat.edu.pe", "Evaluación de Programa/Proyecto", receptor, AsuntoCorreo, cuerpo, True)
                    '=============== FIN ENVIAR CORREO ===============================
                End If

                Me.btnBuscar_Click(sender, e)
                'Para avisos
                Me.lblrpta.Text = "El Programa/Proyecto a sido Observado con éxito."
                Me.aviso.Attributes.Add("class", "mensajeExito")
            Else
                'Para avisos
                Me.lblrpta.Text = "El Programa/Proyecto No puede ser Observado, Cuenta Con Presupuesto Asignado."
                Me.aviso.Attributes.Add("class", "mensajeError")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Function EnviarMensaje(ByVal de As String, ByVal de_nombre As String, ByVal para As String, ByVal asunto As String, ByVal mensaje As String, ByVal copia As String, ByVal rutaarchivo As String, ByVal nombrearchivo As String, ByVal replyto As String) As Boolean
        Try
            Dim cls As New ClsEnviaMailPOA
            If cls.EnviarMailAd(de, de_nombre, para, asunto, mensaje, True, copia, replyto, rutaarchivo, nombrearchivo) Then
                cls = Nothing
                Return True
            Else
                cls = Nothing
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function


    Protected Sub ddlEjercicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEjercicio.SelectedIndexChanged
        CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue)
    End Sub


End Class


Partial Class indicadores_POA_PROTOTIPOS_Registrar_POA
    Inherits System.Web.UI.Page
    Dim nombre_poa As String = String.Empty
    Dim CurrentRow1 As Integer = -1


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

    Sub CargaPoas(ByVal codigo_pla As Integer, ByVal codigo_ejp As Integer, ByVal estado As String)
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.POA_ListarPOAS(codigo_pla, codigo_ejp, estado)

        Me.ddlPoa.DataSource = dtt
        Me.ddlPoa.DataTextField = "nombre_poa"
        Me.ddlPoa.DataValueField = "codigo_poa"
        Me.ddlPoa.DataBind()
        dtt.Dispose()
        obj = Nothing
        Me.ddlPoa.Items.Insert(0, New ListItem("--SELECCIONE--", "0"))
        'Me.ddlPoa.SelectedValue = 0
    End Sub
  
    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        Try
            If IsPostBack = False Then
                Dim Tabla As String
                CargaPlanes()
                CargaEjercicio()
                Tabla = CargaAlineamiento(Me.ddlplan.SelectedValue, Me.ddlPoa.SelectedValue, Me.ddlEjercicio.SelectedValue, Me.ddlestado.SelectedValue)
                TablaAlineamiento.InnerHtml = Tabla
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    'Protected Sub dgvAlineamiento_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvAlineamiento.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
    '        If nombre_poa = row("nombre_poa") Then
    '            If (Me.dgvAlineamiento.Rows(CurrentRow1).Cells(0).RowSpan = 0) Then
    '                Me.dgvAlineamiento.Rows(CurrentRow1).Cells(0).RowSpan = 2
    '            Else
    '                Me.dgvAlineamiento.Rows(CurrentRow1).Cells(0).RowSpan += 1
    '            End If
    '            e.Row.Cells.RemoveAt(0)
    '        Else
    '            e.Row.VerticalAlign = VerticalAlign.Middle
    '            nombre_poa = row("nombre_poa").ToString()
    '            CurrentRow1 = e.Row.RowIndex
    '        End If

    'Dim row1 As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
    'If resumen_acp = row("resumen_acp") Then
    '    If (Me.dgvAlineamiento.Rows(CurrentRow1).Cells(1).RowSpan = 0) Then
    '        Me.dgvAlineamiento.Rows(CurrentRow1).Cells(1).RowSpan = 2
    '    Else
    '        Me.dgvAlineamiento.Rows(CurrentRow1).Cells(1).RowSpan += 1
    '    End If
    '    e.Row.Cells.RemoveAt(0)
    'Else
    '    e.Row.VerticalAlign = VerticalAlign.Middle
    '    resumen_acp = row("resumen_acp").ToString()
    '    CurrentRow1 = e.Row.RowIndex
    'End If

    'Dim row2 As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
    'If nombre_obj = row("nombre_obj") And nombre_obj <> "" Then
    '    If (Me.dgvAlineamiento.Rows(CurrentRow3).Cells(1).RowSpan = 0) Then
    '        Me.dgvAlineamiento.Rows(CurrentRow3).Cells(1).RowSpan = 2
    '    Else
    '        Me.dgvAlineamiento.Rows(CurrentRow3).Cells(1).RowSpan += 1
    '    End If
    '    e.Row.Cells.RemoveAt(0)
    'Else
    '    e.Row.VerticalAlign = VerticalAlign.Middle
    '    nombre_obj = row("nombre_obj").ToString()
    '    CurrentRow3 = e.Row.RowIndex
    'End If


    'Dim row3 As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
    'If descripcion_ind = row3("descripcion_ind") And descripcion_ind <> "" Then
    '    If (Me.dgvAlineamiento.Rows(CurrentRow4).Cells(3).RowSpan = 0) Then
    '        Me.dgvAlineamiento.Rows(CurrentRow4).Cells(3).RowSpan = 2
    '    Else
    '        Me.dgvAlineamiento.Rows(CurrentRow4).Cells(3).RowSpan += 1
    '    End If
    '    e.Row.Cells.RemoveAt(3)
    'Else
    '    e.Row.VerticalAlign = VerticalAlign.Middle
    '    descripcion_ind = row3("descripcion_ind").ToString()
    '    CurrentRow4 = e.Row.RowIndex
    'End If
    '    End If
    'End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            If (Request.QueryString("id") = "") Then
                Response.Redirect("../../../../sinacceso.html")
            Else

                Dim Tabla As String
                Tabla = CargaAlineamiento(Me.ddlplan.SelectedValue, Me.ddlPoa.SelectedValue, Me.ddlEjercicio.SelectedValue, Me.ddlestado.SelectedValue)
                TablaAlineamiento.InnerHtml = Tabla
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
        estilo_celda = " style='backgroud-color:#3871b0; color:white; font-size:9px; border: solid 1px rgb(169,169,169);'"
        Dim obj As New clsPlanOperativoAnual
        'Para cabecera
        Dim dt As New Data.DataTable
        dt = obj.CabeceraEvaluarAlineamiento(codigo_pladdl, codigo_poaddl, codigo_ejp, estado)

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

            StrPlan = "<th width='140' " & estilo_celda & " rowspan='3'>PROGRAMA / PROYECTO</th>" & StrPlan
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
            Dim strFilas As String = ""
            dtbody = obj.EvaluarAlineamiento(codigo_pladdl, codigo_poaddl, codigo_ejp, estado)
            Dim rowspan As Integer = 0
            Dim rowspan_tac As Integer = 0
            Dim rowspan_acp As Integer = 0
            Dim marcas As String = ""

            For i As Integer = 0 To dtbody.Rows.Count - 1
                'If codigo_poa <> dt.Rows(i).Item("codigo_poa") And dt.Rows(i).Item("codigo_poa").ToString <> "" Then
                codigo_poa = dtbody.Rows(i).Item("codigo_poa")
                nombre_poa = dtbody.Rows(i).Item("nombre_poa")
                codigo_tac = dtbody.Rows(i).Item("codigo_tac")
                codigo_acp = dtbody.Rows(i).Item("codigo_acp")

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
                                            StrActividadCab = StrActividadCab & "<td style='font-size:9px; text-align:center' class='celda_combinada' ><img src='../../Images/aprobar1.png' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "' ind='" & dtbody.Rows(i).Item("descripcion_ind") & "' val='" & dtbody.Rows(i).Item("codigo_ali") & "' /></td>"
                                        Else
                                            StrActividadCab = StrActividadCab & "<td style='font-size:9px' class='celda_combinada'></td>"
                                        End If
                                    Next
                                Else
                                    'demas Filas que va a tener rowspan de filas de programa o proyecto
                                    StrActividadDet = StrActividadDet & "<tr>"
                                    For j As Integer = 0 To dt.Rows.Count - 1
                                        If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
                                            StrActividadDet = StrActividadDet & "<td style='font-size:9px; text-align:center' class='celda_combinada' ><img src='../../Images/aprobar1.png' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "' ind='" & dtbody.Rows(i).Item("descripcion_ind") & "' val='" & dtbody.Rows(i).Item("codigo_ali") & "' /></td>"
                                        Else
                                            StrActividadDet = StrActividadDet & "<td style='font-size:9px' class='celda_combinada'></td>"
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
                                            StrActividadCab = StrActividadCab & "<td style='font-size:9px; text-align:center' class='celda_combinada' ><img src='../../Images/aprobar1.png' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "' ind='" & dtbody.Rows(i).Item("descripcion_ind") & "' val='" & dtbody.Rows(i).Item("codigo_ali") & "' /></td>"
                                        Else
                                            StrActividadCab = StrActividadCab & "<td style='font-size:9px' class='celda_combinada'></td>"
                                        End If
                                    Next
                                Else
                                    StrActividadDet = StrActividadDet & "<tr>"
                                    For j As Integer = 0 To dt.Rows.Count - 1
                                        If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
                                            StrActividadDet = StrActividadDet & "<td style='font-size:9px; text-align:center' class='celda_combinada' ><img src='../../Images/aprobar1.png' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "' ind='" & dtbody.Rows(i).Item("descripcion_ind") & "' val='" & dtbody.Rows(i).Item("codigo_ali") & "' /></td>"

                                        Else
                                            StrActividadDet = StrActividadDet & "<td style='font-size:9px' class='celda_combinada'></td>"
                                        End If
                                    Next
                                    StrActividadDet = StrActividadDet & "</tr>"
                                End If
                                'concateno la primera fila con el salto de fila
                                If rowspan_tac = 1 Then
                                    StrActividad = StrActividad & "<tr><td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                                    StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("descripcion_tac") & "</td>"
                                    StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " ><a href='FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id")
                                    StrActividad = StrActividad & "&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "' >" & dtbody.Rows(i).Item("resumen_acp") & "</a></td>"
                                    StrActividad = StrActividad & StrActividadCab & "</tr>"
                                    StrActividad = StrActividad & StrActividadDet
                                    StrActividadCab = ""
                                    StrActividadDet = ""
                                    rowspan_acp = 0
                                Else
                                    'concateno las demas filas del programa o proyecto
                                    StrActividad = StrActividad & "<tr><td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                                    StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("descripcion_tac") & "</td>"
                                    StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " ><a href='FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id")
                                    StrActividad = StrActividad & "&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "' >" & dtbody.Rows(i).Item("resumen_acp") & "</a></td>"
                                    StrActividad = StrActividad & StrActividadCab & "</tr>"
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
                                        StrActividadCab = StrActividadCab & "<td style='font-size:9px; text-align:center' class='celda_combinada' ><img src='../../Images/aprobar1.png' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "' ind='" & dtbody.Rows(i).Item("descripcion_ind") & "' val='" & dtbody.Rows(i).Item("codigo_ali") & "' /></td>"
                                    Else
                                        StrActividadCab = StrActividadCab & "<td style='font-size:9px' class='celda_combinada'></td>"
                                    End If
                                Next
                            Else
                                StrActividadDet = StrActividadDet & "<tr>"
                                For j As Integer = 0 To dt.Rows.Count - 1
                                    If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
                                        StrActividadDet = StrActividadDet & "<td style='font-size:9px; text-align:center' class='celda_combinada' ><img src='../../Images/aprobar1.png' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "' ind='" & dtbody.Rows(i).Item("descripcion_ind") & "' val='" & dtbody.Rows(i).Item("codigo_ali") & "' /></td>"
                                    Else
                                        StrActividadDet = StrActividadDet & "<td style='font-size:9px' class='celda_combinada'></td>"
                                    End If
                                Next
                                StrActividadDet = StrActividadDet & "</tr>"
                            End If
                            'concateno la primera fila con el salto de fila
                            If rowspan_tac = 1 Then
                                StrActividad = StrActividad & "<tr><td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                                StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("descripcion_tac") & "</td>"
                                StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " ><a> href='FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id")
                                StrActividad = StrActividad & "&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "' >" & dtbody.Rows(i).Item("resumen_acp") & "</a></td>"
                                StrActividad = StrActividad & StrActividadCab & "</tr>"
                                StrActividad = StrActividad & StrActividadDet
                                StrActividadCab = ""
                                StrActividadDet = ""
                                rowspan_acp = 0
                            Else
                                'concateno las demas filas del programa o proyecto
                                StrActividad = StrActividad & "<tr><td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                                StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("descripcion_tac") & "</td>"
                                StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " ><a href='FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id")
                                StrActividad = StrActividad & "&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "' >" & dtbody.Rows(i).Item("resumen_acp") & "</a></td>"
                                StrActividad = StrActividad & StrActividadCab & "</tr>"
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
                                    StrActividadCab = StrActividadCab & "<td style='font-size:9px; text-align:center' class='celda_combinada' ><img src='../../Images/aprobar1.png' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "' ind='" & dtbody.Rows(i).Item("descripcion_ind") & "' val='" & dtbody.Rows(i).Item("codigo_ali") & "' /></td>"
                                Else
                                    StrActividadCab = StrActividadCab & "<td style='font-size:9px' class='celda_combinada'></td>"
                                End If
                            Next
                        Else
                            StrActividadDet = StrActividadDet & "<tr>"
                            For j As Integer = 0 To dt.Rows.Count - 1
                                If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
                                    StrActividadDet = StrActividadDet & "<td style='font-size:9px; text-align:center' class='celda_combinada' ><img src='../../Images/aprobar1.png'  data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "' ind='" & dtbody.Rows(i).Item("descripcion_ind") & "' val='" & dtbody.Rows(i).Item("codigo_ali") & "'  /></td>"
                                Else
                                    StrActividadDet = StrActividadDet & "<td style='font-size:9px' class='celda_combinada'></td>"
                                End If
                            Next
                            StrActividadDet = StrActividadDet & "</tr>"
                        End If
                        'concateno la primera fila con el salto de fila
                        If rowspan_tac = 1 Then
                            '''''''''''''''''''''''''''''''fila combinada''''''''''''''''''''''''''''''''''''''''''''''''''
                            'StrActividad = StrActividad & "<tr><td style='font-size:9px' class='celda_combinada' colspan= '" & colspanTotal & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            StrActividad = StrActividad & "<tr><td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                            StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("descripcion_tac") & "</td>"
                            StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " ><a href='FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id")
                            StrActividad = StrActividad & "&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "' >" & dtbody.Rows(i).Item("resumen_acp") & "</a></td>"
                            StrActividad = StrActividad & StrActividadCab & "</tr>"
                            StrActividad = StrActividad & StrActividadDet
                            StrActividadCab = ""
                            StrActividadDet = ""
                            rowspan_acp = 0
                        Else
                            'concateno las demas filas del programa o proyecto
                            StrActividad = StrActividad & "<tr><td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                            StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("descripcion_tac") & "</td>"
                            StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " ><a href='FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id")
                            StrActividad = StrActividad & "&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "' >" & dtbody.Rows(i).Item("resumen_acp") & "</a></td>"
                            StrActividad = StrActividad & StrActividadCab & "</tr>"
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
                                StrActividadCab = StrActividadCab & "<td style='font-size:9px; text-align:center' class='celda_combinada' ><img src='../../Images/aprobar1.png' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "' ind='" & dtbody.Rows(i).Item("descripcion_ind") & "' val='" & dtbody.Rows(i).Item("codigo_ali") & "'  /></td>"
                            Else
                                StrActividadCab = StrActividadCab & "<td style='font-size:9px' class='celda_combinada'></td>"
                            End If
                        Next
                    Else
                        StrActividadDet = StrActividadDet & "<tr>"
                        For j As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
                                StrActividadDet = StrActividadDet & "<td style='font-size:9px; text-align:center' class='celda_combinada' ><img src='../../Images/aprobar1.png' data-toggle='modal' cod_acp='" & codigo_acp & "' acp='" & dtbody.Rows(i).Item("resumen_acp") & "' ind='" & dtbody.Rows(i).Item("descripcion_ind") & "' val='" & dtbody.Rows(i).Item("codigo_ali") & "'  /></td>"
                            Else
                                StrActividadDet = StrActividadDet & "<td style='font-size:9px' class='celda_combinada'></td>"
                            End If
                        Next
                        StrActividadDet = StrActividadDet & "</tr>"
                    End If
                    'concateno la primera fila con el salto de fila
                    If rowspan_tac = 1 Then
                        StrActividad = StrActividad & "<tr><td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                        StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("descripcion_tac") & "</td>"
                        StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " ><a href='FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id")
                        StrActividad = StrActividad & "&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "' >" & dtbody.Rows(i).Item("resumen_acp") & "</a></td>"
                        StrActividad = StrActividad & StrActividadCab & "</tr>"
                        StrActividad = StrActividad & StrActividadDet
                        StrActividadCab = ""
                        StrActividadDet = ""
                        rowspan_acp = 0
                    Else
                        'concateno las demas filas del programa o proyecto
                        StrActividad = StrActividad & "<tr><td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("nombre_poa") & "</td>"
                        StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " >" & dtbody.Rows(i).Item("descripcion_tac") & "</td>"
                        StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada' rowspan= " & rowspan_acp & " ><a href='FrmMantenimientoActividadesPOA.aspx?id=" & Request.QueryString("id")
                        StrActividad = StrActividad & "&ctf=" & Request.QueryString("ctf") & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "' >" & dtbody.Rows(i).Item("resumen_acp") & "</a></td>"
                        StrActividad = StrActividad & StrActividadCab & "</tr>"
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
            'StrActividad = "<div id='div1'><table style='width:100%; border-collapse:collapse; border:1px;' border='1' CellSpacing='0' CellPadding='2' >" & StrActividad & "</table><div>"
            'strTabla = strTabla & StrActividad
        End If 'end if dt.rowcount
        Return strTabla

        ''----Cuerpo 2
            'Dim dtbody As New Data.DataTable
            'Dim codigo_acp As Integer = -1
            'Dim codigo_tac As Integer = -1
            'Dim codigo_poa As Integer = -1
            'Dim strFilas As String = ""
            'dtbody = obj.EvaluarAlineamiento(codigo_pladdl, codigo_poaddl, codigo_ejp, estado)
            'Dim rowspan As Integer = 0
            'Dim rowspan_tac As Integer = 0
            'Dim rowspan_acp As Integer = 0
            'Dim marcas As String = ""

            'For i As Integer = 0 To dtbody.Rows.Count - 1
            '    'If codigo_poa <> dt.Rows(i).Item("codigo_poa") And dt.Rows(i).Item("codigo_poa").ToString <> "" Then
            '    If codigo_poa = dtbody.Rows(i).Item("codigo_poa") Then
            '        If codigo_tac = dtbody.Rows(i).Item("codigo_tac") Then
            '            If codigo_acp = dtbody.Rows(i).Item("codigo_acp") Then

            '            Else
            '                StrActividad = StrActividad & "<tr><td style='font-size:9px;' class='celda_combinada' >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & dtbody.Rows(i).Item("resumen_acp") & "</td>"
            '                For j As Integer = 0 To dt.Rows.Count - 1
            '                    If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
            '                        StrActividad = StrActividad & "<td style='font-size:9px; class='celda_combinada' >" & dtbody.Rows(i).Item("codigo_ali") & "</td>"
            '                    Else
            '                        StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada'></td>"
            '                    End If
            '                Next
            '                StrActividad = StrActividad & "</tr>"
            '            End If
            '        Else
            '            StrActividad = StrActividad & "<tr><td style='font-size:9px; font-weight:bold;' class='celda_combinada' colspan='" & colspanTotal + 1 & "' >&nbsp;&nbsp;&nbsp;&nbsp;" & dtbody.Rows(i).Item("descripcion_tac") & "</td></tr>"
            '            StrActividad = StrActividad & "<tr><td style='font-size:9px; class='celda_combinada' >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & dtbody.Rows(i).Item("resumen_acp") & "</td>"
            '            For j As Integer = 0 To dt.Rows.Count - 1
            '                If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
            '                    StrActividad = StrActividad & "<td style='font-size:9px; class='celda_combinada' >" & dtbody.Rows(i).Item("codigo_ali") & "</td>"
            '                Else
            '                    StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada'></td>"
            '                End If
            '            Next
            '            StrActividad = StrActividad & "</tr>"
            '        End If
            '    Else
            '        StrActividad = StrActividad & "<tr><td style='font-size:9px; font-weight:bold;' class='celda_combinada' colspan='" & colspanTotal + 1 & "' >" & dtbody.Rows(i).Item("nombre_poa") & "</td></tr>"
            '        StrActividad = StrActividad & "<tr><td style='font-size:9px; font-weight:bold;' class='celda_combinada' colspan='" & colspanTotal + 1 & "' >&nbsp;&nbsp;&nbsp;&nbsp;" & dtbody.Rows(i).Item("descripcion_tac") & "</td></tr>"
            '        StrActividad = StrActividad & "<tr><td style='font-size:9px; class='celda_combinada'  >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & dtbody.Rows(i).Item("resumen_acp") & "</td>"
            '        For j As Integer = 0 To dt.Rows.Count - 1
            '            If dt.Rows(j).Item("codigo_ind") = dtbody.Rows(i).Item("codigo_ind") Then
            '                StrActividad = StrActividad & "<td style='font-size:9px; class='celda_combinada' >" & dtbody.Rows(i).Item("codigo_ali") & "</td>"
            '            Else
            '                StrActividad = StrActividad & "<td style='font-size:9px' class='celda_combinada'></td>"
            '            End If
            '        Next
            '        StrActividad = StrActividad & "</tr>"
            '    End If
            '    codigo_poa = dtbody.Rows(i).Item("codigo_poa")
            '    nombre_poa = dtbody.Rows(i).Item("nombre_poa")
            '    codigo_tac = dtbody.Rows(i).Item("codigo_tac")
            '    codigo_acp = dtbody.Rows(i).Item("codigo_acp")
            'Next

        '    strTabla = strTabla & StrActividad
        '    '
        '    strTabla = "<div id='div1'><table style='width:98%; border-collapse:collapse; border:1px;' border='1' CellSpacing='0' CellPadding='2' >" & strTabla & "</table></div>"
        '    'StrActividad = "<div id='div1'><table style='width:100%; border-collapse:collapse; border:1px;' border='1' CellSpacing='0' CellPadding='2' >" & StrActividad & "</table><div>"
        '    'strTabla = strTabla & StrActividad
        'End If 'end if dt.rowcount
        'Return strTabla


    End Function



    'Sub CargaArbol(ByVal dt As Data.DataTable)
    '    Dim valor As Integer = 0
    '    Dim codigo_acp As Integer = 0
    '    Dim codigo_obj As Integer = 0
    '    Dim codigo_ind As Integer = 0
    '    Dim nodo As Integer = -1
    '    Dim subnodo As Integer = -1
    '    Dim subnodo2 As Integer = -1
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        If valor <> dt.Rows(i).Item("codigo_poa") Then
    '            Dim newNode As TreeNode = New TreeNode(dt.Rows(i).Item("nombre_poa").ToString, dt.Rows(i).Item("codigo_poa").ToString)
    '            'newNode.PopulateOnDemand = True
    '            newNode.SelectAction = TreeNodeSelectAction.Expand
    '            Me.treeAlineamiento.Nodes.Add(newNode)
    '            nodo = Me.treeAlineamiento.Nodes.Count - 1

    '            If codigo_acp <> dt.Rows(i).Item("codigo_acp") And dt.Rows(i).Item("codigo_acp").ToString <> "-" Then
    '                Dim NewNode1 As TreeNode = New TreeNode(dt.Rows(i).Item("resumen_acp").ToString, dt.Rows(i).Item("codigo_acp").ToString)
    '                ' NewNode1.PopulateOnDemand = True
    '                NewNode1.SelectAction = TreeNodeSelectAction.Select
    '                Me.treeAlineamiento.Nodes(nodo).ChildNodes.Add(NewNode1)
    '                subnodo = Me.treeAlineamiento.Nodes(nodo).ChildNodes.Count - 1

    '                If Convert.ToInt32(dt.Rows(i).Item("codigo_obj").ToString) <> 0 Then
    '                    Dim NewNode2 As TreeNode = New TreeNode(dt.Rows(i).Item("nombre_obj").ToString, dt.Rows(i).Item("codigo_obj").ToString)
    '                    'NewNode2.PopulateOnDemand = True
    '                    NewNode2.SelectAction = TreeNodeSelectAction.Select
    '                    Me.treeAlineamiento.Nodes(nodo).ChildNodes(subnodo).ChildNodes.Add(NewNode2)
    '                    subnodo2 = Me.treeAlineamiento.Nodes(nodo).ChildNodes.Count - 1

    '                    If Convert.ToInt32(dt.Rows(i).Item("codigo_ind").ToString) <> 0 Then
    '                        Dim NewNode3 As TreeNode = New TreeNode(dt.Rows(i).Item("descripcion_ind").ToString, dt.Rows(i).Item("codigo_ind").ToString)
    '                        ' NewNode3.PopulateOnDemand = True
    '                        NewNode3.ShowCheckBox = True
    '                        Me.treeAlineamiento.Nodes(nodo).ChildNodes(subnodo).ChildNodes(subnodo2).ChildNodes.Add(NewNode3)
    '                    End If
    '                End If
    '            End If
    '        Else
    '            If codigo_acp <> dt.Rows(i).Item("codigo_acp") And dt.Rows(i).Item("codigo_acp").ToString <> "-" Then
    '                Dim NewNode1 As TreeNode = New TreeNode(dt.Rows(i).Item("resumen_acp").ToString, dt.Rows(i).Item("codigo_acp").ToString)
    '                ' NewNode1.PopulateOnDemand = True
    '                NewNode1.SelectAction = TreeNodeSelectAction.Select
    '                Me.treeAlineamiento.Nodes(nodo).ChildNodes.Add(NewNode1)
    '                subnodo = Me.treeAlineamiento.Nodes(nodo).ChildNodes.Count - 1

    '                If Convert.ToInt32(dt.Rows(i).Item("codigo_obj").ToString) <> 0 Then
    '                    Dim NewNode2 As TreeNode = New TreeNode(dt.Rows(i).Item("nombre_obj").ToString, dt.Rows(i).Item("codigo_obj").ToString)
    '                    'NewNode2.PopulateOnDemand = True
    '                    NewNode2.SelectAction = TreeNodeSelectAction.Select
    '                    Me.treeAlineamiento.Nodes(nodo).ChildNodes(subnodo).ChildNodes.Add(NewNode2)
    '                    subnodo2 = Me.treeAlineamiento.Nodes(nodo).ChildNodes(subnodo).ChildNodes.Count - 1

    '                    If codigo_ind <> dt.Rows(i).Item("codigo_ind") And codigo_acp <> dt.Rows(i).Item("codigo_acp") And codigo_obj <> dt.Rows(i).Item("codigo_obj") And Convert.ToInt32(dt.Rows(i).Item("codigo_ind").ToString) <> 0 Then
    '                        Dim NewNode3 As TreeNode = New TreeNode(dt.Rows(i).Item("descripcion_ind").ToString, dt.Rows(i).Item("codigo_ind").ToString)
    '                        'NewNode3.PopulateOnDemand = True
    '                        NewNode3.ShowCheckBox = True
    '                        Me.treeAlineamiento.Nodes(nodo).ChildNodes(subnodo).ChildNodes(subnodo2).ChildNodes.Add(NewNode3)
    '                    End If
    '                End If
    '            Else

    '                If codigo_obj <> dt.Rows(i).Item("codigo_obj") And Convert.ToInt32(dt.Rows(i).Item("codigo_obj").ToString) <> 0 Then
    '                    Dim NewNode2 As TreeNode = New TreeNode(dt.Rows(i).Item("nombre_obj").ToString, dt.Rows(i).Item("codigo_obj").ToString)
    '                    'NewNode2.PopulateOnDemand = True
    '                    NewNode2.SelectAction = TreeNodeSelectAction.Select
    '                    Me.treeAlineamiento.Nodes(nodo).ChildNodes(subnodo).ChildNodes.Add(NewNode2)
    '                    subnodo2 = Me.treeAlineamiento.Nodes(nodo).ChildNodes(subnodo).ChildNodes.Count - 1

    '                    If codigo_ind <> dt.Rows(i).Item("codigo_ind") And codigo_obj <> dt.Rows(i).Item("codigo_obj") And Convert.ToInt32(dt.Rows(i).Item("codigo_ind").ToString) <> 0 Then
    '                        Dim NewNode3 As TreeNode = New TreeNode(dt.Rows(i).Item("descripcion_ind").ToString, dt.Rows(i).Item("codigo_ind").ToString)
    '                        'NewNode3.PopulateOnDemand = True
    '                        NewNode3.ShowCheckBox = True
    '                        Me.treeAlineamiento.Nodes(nodo).ChildNodes(subnodo).ChildNodes(subnodo2).ChildNodes.Add(NewNode3)
    '                    End If
    '                Else
    '                    If codigo_ind <> dt.Rows(i).Item("codigo_ind") And codigo_obj = dt.Rows(i).Item("codigo_obj") And Convert.ToInt32(dt.Rows(i).Item("codigo_ind").ToString) <> 0 Then
    '                        Dim NewNode3 As TreeNode = New TreeNode(dt.Rows(i).Item("descripcion_ind").ToString, dt.Rows(i).Item("codigo_ind").ToString)
    '                        'NewNode3.PopulateOnDemand = True
    '                        NewNode3.ShowCheckBox = True
    '                        Me.treeAlineamiento.Nodes(nodo).ChildNodes(subnodo).ChildNodes(subnodo2).ChildNodes.Add(NewNode3)
    '                    End If

    '                End If
    '            End If
    '        End If
    '        valor = dt.Rows(i).Item("codigo_poa")
    '        codigo_acp = dt.Rows(i).Item("codigo_acp")
    '        codigo_obj = dt.Rows(i).Item("codigo_obj")
    '        codigo_ind = dt.Rows(i).Item("codigo_ind")
    '    Next
    '    Me.treeAlineamiento.RootNodeStyle.ForeColor = Drawing.Color.FromArgb(1, 37, 113, 167)
    'End Sub


    Protected Sub ddlplan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlplan.SelectedIndexChanged
        CargaPoas(Me.ddlplan.SelectedValue, Me.ddlEjercicio.SelectedValue, 1)
    End Sub


    Protected Sub btnAprobacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAprobacion.Click
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim mensaje As String
            'Aprobado por Planificacion
            mensaje = obj.InstanciaEstadoActividad(Me.hdcodigo_acp.Value, 9, Request.QueryString("id"))
            If mensaje = 0 Then

            Else

            End If
            Me.btnBuscar_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnObservacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnObservacion.Click
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim mensaje As String
            ' Cambia Instancia-Estado a Registro Poa - Observado
            mensaje = obj.InstanciaEstadoActividad(Me.hdcodigo_acp.Value, 3, Request.QueryString("id"))
            'Inserta Observación.
            mensaje = obj.POA_ActualizarObservacion(Me.hdcodigo_acp.Value, Me.txtobservacion.Value, Request.QueryString("id"), 0)
            Me.btnBuscar_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class

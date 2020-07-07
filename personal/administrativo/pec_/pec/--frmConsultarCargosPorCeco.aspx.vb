Imports System.Threading

Partial Class personal_administrativo_pec_frmConsultarCargosPorCeco
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then

            Dim objfun As New ClsFunciones
            Dim obj As New ClsConectarDatos
            Dim datos As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()


            'Cargar Datos Centro Costos
            'datos = obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), "", Request.QueryString("mod"))
            datos = obj.TraerDataTable("EVE_BuscaCcoId", Request.QueryString("id"), Request.QueryString("ctf"), Request.QueryString("mod"), "N")
            objfun.CargarListas(cboCecos, datos, "codigo_Cco", "descripcion_cco", ">> Seleccione <<")
            datos.Dispose()

            obj.CerrarConexion()

            Panel3.Visible = False
            MostrarBusquedaCeCos(False)

            btnConsultar.Enabled = False                        
            pager.Visible = False
        End If
    End Sub

    Protected Sub ImgBuscarCecos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarCecos.Click
        BuscarCeCos()
    End Sub

    Private Sub BuscarCeCos()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        gvCecos.DataSource = obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), txtBuscaCecos.Text, Request.QueryString("mod"))
        gvCecos.DataBind()
        obj.CerrarConexion()
        obj = Nothing
        Panel3.Visible = True
    End Sub
    Private Sub MostrarBusquedaCeCos(ByVal valor As Boolean)
        Me.txtBuscaCecos.Visible = valor
        Me.ImgBuscarCecos.Visible = valor
        Me.lblTextBusqueda.Visible = valor
        Me.cboCecos.Visible = Not (valor)
    End Sub


    Protected Sub gvCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCecos.SelectedIndexChanged
        cboCecos.SelectedValue = Me.gvCecos.DataKeys.Item(Me.gvCecos.SelectedIndex).Values(0)
        MostrarBusquedaCeCos(False)
        Panel3.Visible = False
        lnkBusquedaAvanzada.Text = "Busqueda Avanzada"
        cboCecos_SelectedIndexChanged(sender, e)

    End Sub



    Protected Sub lnkBusquedaAvanzada_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBusquedaAvanzada.Click
        If lnkBusquedaAvanzada.Text.Trim = "Búsqueda Simple" Then
            MostrarBusquedaCeCos(False)
            lnkBusquedaAvanzada.Text = "Búsqueda Avanzada"
        Else
            MostrarBusquedaCeCos(True)
            lnkBusquedaAvanzada.Text = "Búsqueda Simple"
            txtBuscaCecos.Text = ""
        End If

        lblMensaje.Text = ""
    End Sub

    Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCecos.SelectedIndexChanged

        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        Dim datos As New Data.DataTable


        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        datos = obj.TraerDataTable("PRESU_ConsultarServiciosEnDeuda", cboCecos.SelectedValue)

        If datos.Rows.Count > 0 Then
            objfun.CargarListas(cboServ, datos, "codigo_Sco", "nombre", ">> Todos los servicios <<")
            btnConsultar.Enabled = True
        Else
            objfun.CargarListas(cboServ, datos, "codigo_Sco", "nombre", ">> No se encontró ningún servicio cargado <<")
            btnConsultar.Enabled = False
        End If

        datos.Dispose()

        obj.CerrarConexion()

        gvResultado.DataSource = Nothing
        gvResultado.DataBind()

        lblMensaje.Text = ""


    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        Try
            CargarGridDeudas()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarGridDeudas()
        Try
            'Borrar variables de sesión


            'Session.Abandon() ' Comentado para CONEISC 07/08/12
            Dim obj As New ClsConectarDatos
            Dim datos As New Data.DataTable

            Dim totalpersonas As Integer = 0
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            datos = obj.TraerDataTable("PRESU_ConsultarDeudasPorCeco", cboCecos.SelectedValue, cboServ.SelectedValue)
            'Session("Deudas") = datos
            Session("TotalPersonas") = datos.Rows(0).Item(12)

            'mvillavicencio 31/07/12
            LlenarTablaPaginacionJQuery(datos, 1, "primero")
            'mvillavicencio 2012
            'CargarTabla(1, "primero")

            'gvResultado.DataSource = datos
            'gvResultado.DataBind()

            lblMensaje.Text = ""

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click

        '### Modificado por mvillavicencio 06/08/2012.
        'El gridview ya no se usa, sino una tabla html dinámica. 
        'Se valida que el div que contiene a la tabla no esté vacío.

        'If gvResultado.Rows.Count > 0 Then
        If reporte.InnerHtml <> "" Then
            'btnConsultar_Click(sender, e)
            Axls("Lista de Cargos", gvResultado, "CECO: " & cboCecos.SelectedItem.Text, "Campus Virtual USAT")
        Else
            lblMensaje.Text = "Debe ejecutar la consulta primero antes de exportar."
        End If

    End Sub

    '## Modificado por mvillavicencio 06/08/2012: En lugar de gridview, se usa una tabla html

    Private Sub Axls(ByVal nombrearchivo As String, ByRef grid As GridView, ByVal titulo As String, ByVal piedepagina As String)
        'Response.Clear()
        'Response.Buffer = True
        'Response.ContentType = "application/vnd.ms-xls"
        'Response.AddHeader("Content-Disposition", "attachment; filename=" & nombrearchivo & ".xls")
        'Response.Charset = "UTF-8"
        'Response.ContentEncoding = System.Text.Encoding.Default
        'grid.HeaderRow.BackColor = Drawing.Color.FromName("#3366CC")
        'grid.HeaderRow.ForeColor = Drawing.Color.White
        'grid.Columns(10).Visible = False
        'Response.Write(ClsFunciones.HTML(grid, titulo, piedepagina))
        'Response.End()

        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()

        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(reporte)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        'Response.AddHeader("Content-Disposition", "attachment;filename=inscritos_cargo" & ".xls")
        Response.AddHeader("Content-Disposition", "attachment;filename=" & nombrearchivo & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    'Private Sub Axls(ByVal nombrearchivo As String, ByRef grid As GridView, ByVal titulo As String, ByVal piedepagina As String)
    '    Response.Clear()
    '    Response.Buffer = True
    '    Response.ContentType = "application/vnd.ms-xls"
    '    Response.AddHeader("Content-Disposition", "attachment; filename=" & nombrearchivo & ".xls")
    '    Response.Charset = "UTF-8"
    '    Response.ContentEncoding = System.Text.Encoding.Default
    '    grid.HeaderRow.BackColor = Drawing.Color.FromName("#3366CC")
    '    grid.HeaderRow.ForeColor = Drawing.Color.White
    '    grid.Columns(10).Visible = False
    '    Response.Write(ClsFunciones.HTML(grid, titulo, piedepagina))
    '    Response.End()
    'End Sub

    '###################################################################################


    Protected Sub gvResultado_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvResultado.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    'mvillavicencio 31/07/2012
    Private Sub LlenarTablaPaginacionJQuery(ByVal datos As Data.DataTable, ByVal actual As Integer, ByVal tipo As String)
        Try
            'actual:          Pagina actual
            'total:           Total de registros
            'por_pagina:      Registros por pagina
            'enlace:          Texto del enlace
            'totalpaginas:     Total de paginas
            'Devuelve un texto que representa la paginacion

            Dim objfun As New ClsFunciones
            'Dim datos As New Data.DataTable
            Dim strTabla As String = ""
            Dim total As Integer = 0
            Dim por_pagina As Integer = 10
            Dim totalpaginas As Integer = 0

            ' datos = DirectCast(Session("Deudas"), Data.DataTable)
            total = datos.Rows.Count()
            'redondear arriba
            totalpaginas = redondear_arriba(total / por_pagina)

            lbltotalpaginas.Text = totalpaginas
            'lbltotal.Text = total 'mostraba el total de filas
            lbltotal.Text = Session("TotalPersonas") 'muestra el total de personas

            'Si la página actual es menor o igual que la última, se ejecuta la acción                        
            If actual <= totalpaginas Then
                lblactual.Text = actual
                strTabla = strTabla & "<table class='tablesorter' id='miTabla'>"
                strTabla = strTabla & "<thead>"
                strTabla = strTabla & "<tr><th style='width:40px'><b>Nº</b></th>"
                strTabla = strTabla & "<th style='width:100px'>COD. RESP.</b></th>"
                strTabla = strTabla & "<th style='width:100px'>ALUMNO USAT</b></th>"
                'strTabla = strTabla & "<th style='width:80px'>CLIENTE</b></th>"
                strTabla = strTabla & "<th style='width:60px'>CARRERA</b></th>"
                strTabla = strTabla & "<th style='width:80px'>FECHA CARGO</b></th>"
                strTabla = strTabla & "<th style='width:80px'>SERVICIO</b></th>"
                strTabla = strTabla & "<th style='width:80px'>CARGOS</b></th>"
                strTabla = strTabla & "<th style='width:80px'>ABONOS</b></th>"
                strTabla = strTabla & "<th style='width:80px'>SALDO</b></th>"
                strTabla = strTabla & "<th style='width:80px'>FECHA VENC</b></th>"
                strTabla = strTabla & "<th style='width:120px'>OBSERVACION</b></th>"
                strTabla = strTabla & "<th style='width:80px'>ESTADO</b></th>"
                strTabla = strTabla & "<th style='width:80px'>DETALLE</b></th>"
                strTabla = strTabla & "<th>..</th></tr>"
                strTabla = strTabla & "</thead>"
                strTabla = strTabla & "<tbody>"

                strTabla = strTabla & fn_llenardatos(datos, actual, por_pagina, totalpaginas, total, tipo)

                strTabla = strTabla & "</tbody>"
                strTabla = strTabla & "<tfoot>"
                strTabla = strTabla & "<tr><th><b>Nro</b></th>"
                strTabla = strTabla & "<th>COD. RESP.</b></th>"
                strTabla = strTabla & "<th style='width:100px'>ALUMNO USAT</b></th>"
                'strTabla = strTabla & "<th>CLIENTE</b></th>"
                strTabla = strTabla & "<th>CARRERA</b></th>"
                strTabla = strTabla & "<th>FECHA CARGO</b></th>"
                strTabla = strTabla & "<th>SERVICIO</b></th>"
                strTabla = strTabla & "<th>CARGOS</b></th>"
                strTabla = strTabla & "<th>ABONOS</b></th>"
                strTabla = strTabla & "<th>SALDO</b></th>"
                strTabla = strTabla & "<th>FECHA VENC</b></th>"
                strTabla = strTabla & "<th>OBSERVACION</b></th>"
                strTabla = strTabla & "<th>ESTADO</b></th>"
                strTabla = strTabla & "<th>DETALLE</b></th>"
                strTabla = strTabla & "<th>..</th></tr>"
                strTabla = strTabla & "</tfoot>"

                strTabla = strTabla & "</table><br/><br/>"

                reporte.InnerHtml = strTabla
            End If

            pager.Visible = True
            'datos.Dispose()

            'obj.CerrarConexion()

        Catch ex As Exception
            lblMensaje.Text = lblMensaje.Text & ex.Message
        End Try
    End Sub

    'mvillavicencio 31/07/2012
    Private Function fn_llenardatos(ByVal dt As Data.DataTable, ByVal actual As Integer, ByVal por_pagina As Integer, ByVal totalpaginas As Integer, ByVal total As Integer, ByVal tipo As String) As String
        Dim strAct As String = ""
        Dim registro_inicial As Integer = 0
        Dim registro_final As Integer = 0
        Dim colorfila As String = ""
        Dim totalalumnos_pagina As Integer = 0
        Dim nroinicial As Integer
        Dim usuario As Integer = Request.QueryString("id")

        registro_inicial = (actual - 1) * por_pagina
        registro_final = (registro_inicial + por_pagina) - 1

        If total < registro_final + 1 Then
            registro_final = total - 1
        End If

        For i As Integer = registro_inicial To registro_final
            If i > 0 Then
                If dt.Rows(i).Item(0) <> dt.Rows(i - 1).Item(0) Then
                    totalalumnos_pagina = totalalumnos_pagina + 1
                End If
            Else
                totalalumnos_pagina = totalalumnos_pagina + 1
            End If
        Next

        If tipo = "anterior" Then
            nroinicial = CInt(lblnroprimero.Text) - totalalumnos_pagina
        ElseIf tipo = "primero" Then
            nroinicial = 0
        ElseIf tipo = "siguiente" Then
            nroinicial = CInt(lblnroultimo.Text)
        ElseIf tipo = "ultimo" Then
            nroinicial = Session("TotalPersonas") - totalalumnos_pagina
        End If

        lblnroprimero.Text = nroinicial

        'For i As Integer = registro_inicial To registro_final
        For i As Integer = 0 To dt.Rows.Count() - 1
            strAct = strAct & "<tr>"

            'Para CONEISC 2012 mostrar siempre el nombre de los alumnos, asi tenga varios cargos
            If cboCecos.SelectedValue = 2499 Then
                nroinicial = nroinicial + 1
                strAct = strAct & "<td>" & nroinicial & "</td>"
                strAct = strAct & "<td>" & dt.Rows(i).Item(0) & "</td>"
                strAct = strAct & "<td>" & dt.Rows(i).Item(1) & "</td>"
            End If

            'Para los otros cco mostrar solo una vez el nombre de los alumnos
            If cboCecos.SelectedValue <> 2499 Then
                If i > 0 Then
                    If dt.Rows(i).Item(0) = dt.Rows(i - 1).Item(0) Then
                        If i <> registro_inicial Then
                            strAct = strAct & "<td style='width:160px'></td>"
                            strAct = strAct & "<td style='width:160px'></td>"
                            strAct = strAct & "<td style='width:160px'></td>"
                        Else
                            strAct = strAct & "<td style='width:160px'></td>"
                            strAct = strAct & "<td style='width:160px'>" & dt.Rows(i).Item(0) & "</td>"
                            strAct = strAct & "<td style='width:160px'>" & dt.Rows(i).Item(1) & "</td>"
                        End If


                    Else
                        nroinicial = nroinicial + 1
                        strAct = strAct & "<td>" & nroinicial & "</td>"
                        strAct = strAct & "<td>" & dt.Rows(i).Item(0) & "</td>"
                        strAct = strAct & "<td>" & dt.Rows(i).Item(1) & "</td>"
                    End If
                Else
                    nroinicial = nroinicial + 1
                    strAct = strAct & "<td> " & nroinicial & "</td>"
                    strAct = strAct & "<td> " & dt.Rows(i).Item(0) & "</td>"
                    strAct = strAct & "<td> " & dt.Rows(i).Item(1) & "</td>"

                End If
            End If

            For j As Integer = 2 To 9
                strAct = strAct & "<td> " & dt.Rows(i).Item(j) & "</td>"
            Next

            strAct = strAct & "<td> " & dt.Rows(i).Item(11) & "</td>"
            strAct = strAct & "<td><a href='javascript:abrirdetalleabonos(" & CInt(dt.Rows(i).Item(10)) & ")'> Ver abonos</a></td>"
            strAct = strAct & "<td><a href='javascript:abriractualizarobservacion(" & CInt(dt.Rows(i).Item(10)) & ", " & usuario & ", " & """" & dt.Rows(i).Item(9) & """" & " )'> Actualizar observación</a></td>"
            strAct = strAct & "</tr>"
        Next

        lblnroultimo.Text = nroinicial
        Return strAct
    End Function

    ''mvillavicencio 2012
    'Private Sub CargarTabla(ByVal actual As Integer, ByVal tipo As String)
    '    Try
    '        'actual:          Pagina actual
    '        'total:           Total de registros
    '        'por_pagina:      Registros por pagina
    '        'enlace:          Texto del enlace
    '        'totalpaginas:     Total de paginas
    '        'Devuelve un texto que representa la paginacion

    '        Dim objfun As New ClsFunciones
    '        Dim datos As New Data.DataTable
    '        Dim strTabla As String = ""
    '        Dim total As Integer = 0
    '        Dim por_pagina As Integer = 20
    '        Dim totalpaginas As Integer = 0

    '        datos = DirectCast(Session("Deudas"), Data.DataTable)
    '        total = datos.Rows.Count()
    '        'redondear arriba
    '        totalpaginas = redondear_arriba(total / por_pagina)

    '        lbltotalpaginas.Text = totalpaginas
    '        'lbltotal.Text = total 'muestraba el total de filas
    '        lbltotal.Text = Session("TotalPersonas") 'muestra el total de personas

    '        'Si la página actual es menor o igual que la última, se ejecuta la acción

    '        If actual <= totalpaginas Then
    '            lblactual.Text = actual
    '            strTabla = strTabla & "<table style='font-family: Arial; font-size:8px' cellpadding='3' cellspacing='0' style='border:1px solid #ece9d8'>"
    '            strTabla = strTabla & "<tr><td align='center' class='cabecera'><b>Nro</b></td>"
    '            strTabla = strTabla & "<td align='center' class='cabecera'><b>COD. RESP.</b></td>"
    '            strTabla = strTabla & "<td align='center' class='cabecera'><b>CLIENTE</b></td>"
    '            strTabla = strTabla & "<td align='center' class='cabecera'><b>CARRERA</b></td>"
    '            strTabla = strTabla & "<td align='center' class='cabecera'><b>FECHA CARGO</b></td>"
    '            strTabla = strTabla & "<td align='center' class='cabecera'><b>SERVICIO</b></td>"
    '            strTabla = strTabla & "<td align='center' class='cabecera'><b>CARGOS</b></td>"
    '            strTabla = strTabla & "<td align='center' class='cabecera'><b>ABONOS</b></td>"
    '            strTabla = strTabla & "<td align='center' class='cabecera'><b>SALDO</b></td>"
    '            strTabla = strTabla & "<td align='center' class='cabecera'><b>FECHA VENC</b></td>"
    '            strTabla = strTabla & "<td align='center' class='cabecera'><b>OBSERVACION</b></td>"
    '            strTabla = strTabla & "<td align='center' class='cabecera'><b>ESTADO</b></td>"
    '            strTabla = strTabla & "<td align='center' class='cabecera'><b>DETALLE</b></td></tr>"
    '            strTabla = strTabla & "<td align='center' class='cabecera'><b>..</b></td></tr>"
    '            strTabla = strTabla & "</tr>"

    '            strTabla = strTabla & fn_actividades(datos, actual, por_pagina, totalpaginas, total, tipo)

    '            strTabla = strTabla & "</table><br/><br/>"

    '            reporte.InnerHtml = strTabla
    '        End If


    '        'datos.Dispose()

    '        'obj.CerrarConexion()

    '    Catch ex As Exception

    '    End Try
    'End Sub

   


    ''mvillavicencio 2012
    'Private Function fn_actividades(ByVal dt As Data.DataTable, ByVal actual As Integer, ByVal por_pagina As Integer, ByVal totalpaginas As Integer, ByVal total As Integer, ByVal tipo As String) As String
    '    Dim strAct As String = ""
    '    Dim registro_inicial As Integer = 0
    '    Dim registro_final As Integer = 0
    '    Dim colorfila As String = ""
    '    Dim totalalumnos_pagina As Integer = 0
    '    Dim nroinicial As Integer
    '    Dim usuario As Integer = Request.QueryString("id")

    '    registro_inicial = (actual - 1) * por_pagina
    '    registro_final = (registro_inicial + por_pagina) - 1

    '    If total < registro_final + 1 Then
    '        registro_final = total - 1
    '    End If

    '    For i As Integer = registro_inicial To registro_final
    '        If i > 0 Then
    '            If dt.Rows(i).Item(0) <> dt.Rows(i - 1).Item(0) Then
    '                totalalumnos_pagina = totalalumnos_pagina + 1
    '            End If
    '        Else
    '            totalalumnos_pagina = totalalumnos_pagina + 1
    '        End If
    '    Next

    '    If tipo = "anterior" Then
    '        nroinicial = CInt(lblnroprimero.Text) - totalalumnos_pagina
    '    ElseIf tipo = "primero" Then
    '        nroinicial = 0
    '    ElseIf tipo = "siguiente" Then
    '        nroinicial = CInt(lblnroultimo.Text)
    '    ElseIf tipo = "ultimo" Then
    '        nroinicial = Session("TotalPersonas") - totalalumnos_pagina
    '    End If

    '    lblnroprimero.Text = nroinicial

    '    For i As Integer = registro_inicial To registro_final

    '        'Si la Fila es Numero Par, se pinta de color celeste
    '        If Val((i + 1) / 2) - Int(Val((i + 1) / 2)) <> 0 Then
    '            colorfila = "#dfefff"
    '        Else
    '            colorfila = "#ffffff"
    '        End If
    '        strAct = strAct & "<tr style='height:25px;'>"

    '        If i > 0 Then
    '            If dt.Rows(i).Item(0) = dt.Rows(i - 1).Item(0) Then

    '                If i <> registro_inicial Then
    '                    strAct = strAct & "<td style='width:60px; background:" & colorfila & "; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b></b></td>"
    '                    strAct = strAct & "<td style='width:60px; background:" & colorfila & "; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b> </b></td>"
    '                    strAct = strAct & "<td style='width:60px; background:" & colorfila & "; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b> </b></td>"
    '                Else
    '                    strAct = strAct & "<td style='width:60px; background:" & colorfila & "; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b></b></td>"
    '                    strAct = strAct & "<td style='width:60px; background:" & colorfila & "; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b> " & dt.Rows(i).Item(0) & "</b></td>"
    '                    strAct = strAct & "<td style='width:60px; background:" & colorfila & "; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b> " & dt.Rows(i).Item(1) & "</b></td>"
    '                End If

    '            Else
    '                nroinicial = nroinicial + 1
    '                strAct = strAct & "<td style='width:60px; background:" & colorfila & "; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b>" & nroinicial & "</b></td>"
    '                strAct = strAct & "<td style='width:60px; background:" & colorfila & "; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b> " & dt.Rows(i).Item(0) & "</b></td>"
    '                strAct = strAct & "<td style='width:60px; background:" & colorfila & "; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b> " & dt.Rows(i).Item(1) & "</b></td>"
    '            End If
    '        Else
    '            nroinicial = nroinicial + 1
    '            strAct = strAct & "<td style='width:60px; background:" & colorfila & "; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b> " & nroinicial & "</b></td>"
    '            strAct = strAct & "<td style='width:60px; background:" & colorfila & "; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b> " & dt.Rows(i).Item(0) & "</b></td>"
    '            strAct = strAct & "<td style='width:60px; background:" & colorfila & "; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b> " & dt.Rows(i).Item(1) & "</b></td>"

    '        End If


    '        For j As Integer = 2 To 9
    '            strAct = strAct & "<td style='width:175px; background:" & colorfila & "; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b> " & dt.Rows(i).Item(j) & "</b></td>"
    '        Next

    '        strAct = strAct & "<td style='width:60px; background:" & colorfila & "; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b> " & dt.Rows(i).Item(11) & "</b></td>"
    '        strAct = strAct & "<td style='width:175px; background:" & colorfila & "; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b><a href='javascript:abrirdetalleabonos(" & CInt(dt.Rows(i).Item(10)) & ")'> Ver abonos</a></b></td>"
    '        strAct = strAct & "<td style='width:175px; background:" & colorfila & "; font-size:11px; border-bottom-width:1px; border-bottom-color:#FFFFFF; border-bottom-style:solid; border-right-style:solid; border-right-color:#D8DADB; border-right-width:1px;'><b><a href='javascript:abriractualizarobservacion(" & CInt(dt.Rows(i).Item(10)) & ", " & usuario & " )'> Actualizar observación</a></b></td>"
    '        strAct = strAct & "</tr>"
    '        strAct = strAct & "</tr>"
    '    Next

    '    lblnroultimo.Text = nroinicial
    '    Return strAct
    'End Function

    ''mvillavicencio 2012
    'Protected Sub btnsiguiente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsiguiente.Click
    '    Try
    '        Dim actual As Integer = CInt(lblactual.Text) + 1
    '        CargarTabla(actual, "siguiente")

    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    ''mvillavicencio 2012
    'Protected Sub btnanterior_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnanterior.Click
    '    Try
    '        'Si la pagina actual no es la primera, se ejecuta la acción
    '        If lblactual.Text <> "1" Then
    '            Dim actual As Integer = CInt(lblactual.Text) - 1
    '            Dim nroinicial As Integer = 0

    '            CargarTabla(actual, "anterior")

    '        End If

    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    ''mvillavicencio 2012
    'Protected Sub btnprimero_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprimero.Click
    '    Try
    '        'Si la pagina actual no es la primera, se ejecuta la acción
    '        If lblactual.Text <> "1" Then
    '            CargarTabla(1, "primero")

    '        End If

    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    ''mvillavicencio 2012
    'Protected Sub btnultimo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnultimo.Click
    '    Try
    '        'Si la pagina actual es la ultima, no se ejecuta la acción
    '        If lblactual.Text <> lbltotalpaginas.Text Then
    '            CargarTabla(CInt(lbltotalpaginas.Text), "ultimo")

    '        End If


    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    'mvillavicencio 2012
    Private Function redondear_arriba(ByVal valor As Decimal) As Integer

        If (Int(valor) - valor < 0) Then
            'es que había decimales en el valor, devuelvo el valor entero + 1
            redondear_arriba = Int(valor) + 1
        Else
            'es que no había decimales
            redondear_arriba = valor
        End If
        Return redondear_arriba
    End Function
   
End Class

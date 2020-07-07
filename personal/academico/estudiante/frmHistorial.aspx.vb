﻿Imports System
Imports System.IO
Imports System.Data
Imports System.Web.UI
Partial Class academico_frmHistorial
    Inherits System.Web.UI.Page
    Dim crd As Integer = 0
    Dim notacrd As Double = 0
    Dim crdAR As Int16 = 0
    Dim crdAC As Int16 = 0
    Dim AAR As Int16 = 0
    Dim AAC As Int16 = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../ErrorSistema.aspx")
        End If

        If IsPostBack = False Then

            Dim Tbl As New Data.DataTable
            Dim codigo_alu As Integer

            codigo_alu = Request.QueryString("id")
            'codigo_alu = Session("codigo_alu")
            Dim obj1 As New ClsConectarDatos
            obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj1.AbrirConexion()
            Dim rsPromedio As New Data.DataTable
            rsPromedio = obj1.TraerDataTable("ACAD_ConsultarPromedioPonderadoGeneral", Request.QueryString("id"))
            obj1.CerrarConexion()
            lblPond.Text = Decimal.Round(rsPromedio.Rows(0).Item("promedio_est"), 4)
            obj1 = Nothing

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            Tbl = obj.TraerDataTable("consultaracceso", "C", codigo_alu, 0)
            If Tbl.Rows.Count > 0 Then
                Me.lblcodigo.Text = Tbl.Rows(0).Item("codigouniver_alu")
                Me.lblalumno.Text = Tbl.Rows(0).Item("alumno")

                Me.lblcicloingreso.Text = Tbl.Rows(0).Item("cicloing_alu")
                Me.lblPlan.Text = Tbl.Rows(0).Item("descripcion_pes")

                If Request.QueryString("m") = "s" And Tbl.Rows(0).Item("estadodeuda_alu") = 1 Then
                    Me.lblMensaje.Text = "No puede visualizar su historial académico por deudas pendientes"
                    Me.grwHistorial.Visible = False
                    Me.cmdExportar.Visible = False
                Else
                    Me.grwHistorial.Visible = True
                    Me.cmdExportar.Visible = True
                    'Cargar la Foto
                    Dim ruta As String
                    Dim obEnc As Object
                    obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

                    ruta = obEnc.CodificaWeb("069" & Tbl.Rows(0).Item("codigouniver_alu").ToString)
                    'ruta = "http://www.usat.edu.pe/imgestudiantes/" & ruta
                    ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
                    photo.Value = ruta.ToString

                    Me.FotoAlumno.ImageUrl = ruta
                    obEnc = Nothing
                    'Cargar escuelas que llevó
                    ClsFunciones.LlenarListas(Me.cboEscuela, obj.TraerDataTable("ConsultarAlumno", 24, codigo_alu), "codigo_cpf", "nombre_cpf", "--Todas las Escuelas que se matriculó--")
                    Me.cboEscuela.SelectedValue = Tbl.Rows(0).Item("codigo_cpf")

                    'Cargar el historial de la Escuela Actual
                    Me.grwHistorial.DataSource = obj.TraerDataTable("ConsultarNotas", 18, Me.cboEscuela.SelectedValue, Request.QueryString("id"), 0)
                    'Response.Write("cbo= " & Me.cboEscuela.SelectedValue & " id= " & Request.QueryString("id"))

                    Me.grwHistorial.DataBind()
                    MostrarResultados(Me.grwHistorial.Rows.Count)
                End If
            Else
                Me.lblMensaje.Text = "El estudiante no existe en la Base de datos"
                Me.FotoAlumno.Visible = False
            End If
            Tbl.Dispose()
            obj.CerrarConexion()
            obj = Nothing
        End If


    End Sub
    Private Function PintarNota(ByVal minima As Double, ByVal nota As Double) As String
        If nota >= minima Then
            PintarNota = "<span class='azul'>" & nota & "</span>"
        Else
            PintarNota = "<span class='rojo'>" & nota & "</span>"
        End If
    End Function
    Protected Sub VerificarMatricula(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView = e.Row.DataItem

            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            'e.Row.Cells(9).Attributes.Add("onclick", "AbrirPopUp('../../personal/academico/estudiante/frmdetalles.asp?codigo_dma=" & fila.Item("codigo_dma").ToString & "&codigouniver_alu=" & Me.lblcodigo.Text & "','450','650')") ' cambio 29.09
            e.Row.Cells(9).Attributes.Add("onclick", "AbrirPopUp('frmdetalles.asp?codigo_dma=" & fila.Item("codigo_dma").ToString & "&codigouniver_alu=" & Me.lblcodigo.Text & "','450','650')")

            '*******************************************
            'Asignar leyenda según el tipo de Matrícula
            '*******************************************
            Select Case fila.Item("tipomatricula_dma")
                Case "C" : e.Row.Cells(3).Text = fila.Item("nombre_cur") & "*"
                Case "U" : e.Row.Cells(3).Text = fila.Item("nombre_cur") & "**"
                Case "S" : e.Row.Cells(3).Text = fila.Item("nombre_cur") & "***"
            End Select

            '*******************************************
            'No mostrar veces desaprobadas =0
            '*******************************************
            If iif(fila.Item("vecesCurso_DmaUlt") Is dbnull.value, 0, fila.Item("vecesCurso_DmaUlt")) = 0 Then
                e.Row.Cells(8).Text = ""
            End If

            '*******************************************
            'No mostrar notas cuando no se ha llenado el registro
            '*******************************************
            If fila.Item("estadonota_cup") = "P" And fila.Item("estado_dma") <> "R" Then
                e.Row.Cells(6).Text = "-"
                e.Row.Cells(7).Text = "P"
                e.Row.CssClass = "P" 'pintar con el color
            Else
                e.Row.CssClass = fila.Item("condicion_dma") 'pintar con el color
            End If
            If fila.Item("estado_dma") = "R" Then
                e.Row.Cells(2).Text = e.Row.Cells(3).Text & "(Retirado)"
            End If


            '*******************************************
            'Almacenar valores Matriculados+Aprobados
            '*******************************************
            If fila.Item("condicion_dma") = "A" Then
                If fila.Item("tipomatricula_dma") <> "C" And fila.Item("estado_dma") <> "R" Then
                    crdAR += CDbl(fila.Item("creditocur_dma"))
                    AAR = AAR + 1
                ElseIf fila.Item("tipomatricula_dma") = "C" And fila.Item("estado_dma") <> "R" Then
                    crdAC += CDbl(fila.Item("creditocur_dma"))
                    AAC = AAC + 1
                End If

            End If

            If fila.Item("inhabilitado_dma") = True Then
                'e.Row.Cells(7).Text = e.Row.Cells(7).Text & "<span style='color:#828282;'> (INHABILITADO) </span>"

                'Yperez 22.07.19
                e.Row.Cells(0).Text = "<span style='color:#3c3c3c;'>" & e.Row.Cells(0).Text & "</span>"
                e.Row.Cells(1).Text = "<span style='color:#3c3c3c;'>" & e.Row.Cells(1).Text & "</span>"
                e.Row.Cells(2).Text = "<span style='color:#3c3c3c;'>" & e.Row.Cells(2).Text & "</span>"
                e.Row.Cells(3).Text = "<span style='color:#3c3c3c;'>" & e.Row.Cells(3).Text & "</span>"
                e.Row.Cells(4).Text = "<span style='color:#3c3c3c;'>" & e.Row.Cells(4).Text & "</span>"
                e.Row.Cells(5).Text = "<span style='color:#3c3c3c;'>" & e.Row.Cells(5).Text & "</span>"
                e.Row.Cells(6).Text = "<span style='color:#3c3c3c;'>-</span>"   'nota
                e.Row.Cells(7).Text = "<span style='color:#3c3c3c;'>INHABILITADO</span>"  'condicion
                e.Row.Cells(8).Text = "<span style='color:#3c3c3c;'>" & e.Row.Cells(8).Text & "</span>"
                e.Row.Cells(10).Text = "<span style='color:#3c3c3c;'>" & e.Row.Cells(10).Text & "</span>"
            End If

            '*******************************************
            'Almacenar valores sin tener en cuenta convalidaciones ni retiros
            '*******************************************
            If fila.Item("estado_dma") <> "R" And (fila.Item("condicion_dma") = "A" Or fila.Item("condicion_dma") = "D") Then
                crd += CDbl(fila.Item("creditocur_dma"))
                notacrd += CDbl(fila.Item("notacredito"))
            End If
            'ElseIf e.Row.RowType = DataControlRowType.Footer Then
            '    e.Row.Cells(3).Text = "TOTAL DE APROBADOS"
            '    e.Row.Cells(4).Text = crd
            '    If crd > 0 Then
            '        e.Row.Cells(6).Text = FormatNumber(notacrd / crd, 2)
            '    End If

            '    e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
            '    e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
            '    crd = 0
            '    notacrd = 0


        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Me.lblAsigAprob.Text = AAR.ToString
        Me.lblAsigAprobC.Text = AAC.ToString
        Me.lblCrdAprob.Text = crdAR.ToString
        Me.lblCrdAprobC.Text = crdAC.ToString
        Me.lblCrd.Text = Int(Me.lblCrdAprob.Text) + Int(Me.lblCrdAprobC.Text)
        Me.lblAsig.Text = Int(Me.lblAsigAprob.Text) + Int(Me.lblAsigAprobC.Text)
        If crd > 0 Then
            '     Me.lblPond.Text = FormatNumber(notacrd / crd, 2)
        End If

    End Sub
    Protected Sub cboEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEscuela.SelectedIndexChanged
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Cargar Record Académico
        Me.grwHistorial.DataSource = obj.TraerDataTable("ConsultarNotas", 18, Me.cboEscuela.SelectedValue, Request.QueryString("id"), 0)
        Me.grwHistorial.DataBind()
        obj.CerrarConexion()
        obj = Nothing
        MostrarResultados(Me.grwHistorial.Rows.Count)
    End Sub
    Private Sub MostrarResultados(ByVal condicion As Int16)
        Me.Panel1.Visible = condicion > 0
        Me.cmdExportar.Visible = condicion > 0
    End Sub
    Private Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        'Dim sb As StringBuilder = New StringBuilder()
        'Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        'Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        'Dim Page As Page = New Page()
        'Dim form As HtmlForm = New HtmlForm()
        'Me.grwHistorial.EnableViewState = False
        'Page.EnableEventValidation = False
        'Page.DesignerInitialize()
        'Page.Controls.Add(form)
        'form.Controls.Add(Me.grwHistorial)
        'Page.RenderControl(htw)
        'Response.Clear()
        'Response.Buffer = True
        'Response.ContentType = "application/vnd.ms-excel"
        'Response.AddHeader("Content-Disposition", "attachment;filename=HistorialCursosMatriculados.xls")        
        ''Response.Charset = "UTF-8"
        ''Response.ContentEncoding = Encoding.Default        
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)        
        'Response.Write(sb.ToString())
        'Response.End()

        Try

            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel; charset=UTF-8;base64"
            Response.Charset = "UTF-8"
            Response.ContentEncoding = System.Text.Encoding.UTF8

            Response.AddHeader("content-disposition", "attachment;filename=HistorialCursosMatriculados" & ".xls")

            Response.ContentType = "application/vnd.ms-excel"
            'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            'Dim sWriter As New StringWriter()
            'Dim hWriter As New HtmlTextWriter(sWriter)

            Dim Cab As New StringBuilder


            Dim fi As FileInfo = New FileInfo(Server.MapPath("../../../private/estilo.css"))
            Dim sb As New System.Text.StringBuilder
            Dim sr As StreamReader = fi.OpenText()
            Do While (sr.Peek() >= 0)
                sb.Append(sr.ReadLine())
            Loop
            sr.Close()
            'Response.Write(sb.ToString())
            'Response.Write("<br><br>")
            'divInfo.RenderControl(hWriter)
            'Response.Write(sb.ToString())
            'grwHistorial.RenderControl(hWriter)


            'Dim sb2 As New StringBuilder()
            ' grwHistorial.RenderControl(New HtmlTextWriter(New StringWriter(sb2)))

            'Dim s As String = sb.ToString()


            'Response.Output.Write(s.ToString())

            'Dim ruta As String = ""

            'Dim obEnc As Object
            'obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

            'ruta = obEnc.CodificaWeb("069" & Me.lblcodigo.Text.ToString)
            'ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta

            'Response.Write(photo.Value.ToString)

            Cab.Append("<br>")
            Cab.Append("<br>")
            Cab.Append("<table ID='tblDatos' border='0' bordercolor='#111111' cellpadding='3' cellspacing='0' class='contornotabla' width='100%'>")
            Cab.Append("<tr>")
            Cab.Append("<td rowspan='5'  colspan='2' valign='top' style='width:15%;'")
            Cab.Append("<img src='" & photo.Value.ToString & "' width='65' height='85' '></img>")
            Cab.Append("</td>")
            Cab.Append("<td style='width:15%;' colspan='2'>C&oacute;digo Universitario</td>")
            Cab.Append("<td class='usatsubtitulousuario' width='70%' colspan='5'>&nbsp;<label>" & Me.lblcodigo.Text.ToString & "</label></td>")
            Cab.Append("</tr>")
            Cab.Append("<tr>")
            Cab.Append("<td width='15%' colspan='2'>Apellidos y Nombres</td>")
            Cab.Append("<td class='usatsubtitulousuario' width='70%' colspan='2'>" & ReemplazaTilde(lblalumno.Text.ToString) & "</td>")
            Cab.Append("</tr>")
            Cab.Append("<tr>")
            Cab.Append("<td width='15%' colspan='2'>Ciclo de Ingreso</td>")
            Cab.Append("<td class='usatsubtitulousuario' width='70%' colspan='2'>" & lblcicloingreso.Text.ToString & "</td>")
            Cab.Append("</tr>")
            Cab.Append("<tr>")
            Cab.Append("<td width='15%' colspan='2'>Plan de Estudio</td>")
            Cab.Append("<td class='usatsubtitulousuario' width='70%' colspan='2'>" & lblPlan.Text.ToString & "</td>")
            Cab.Append("</tr>")
            Cab.Append("<tr>")
            Cab.Append("<td width='15%' colspan='2'>Escuela Profesional</td>")
            Cab.Append("<td class='usatsubtitulousuario' width='70%' colspan='2'>" & ReemplazaTilde(cboEscuela.SelectedItem.Text.ToString) & "</td>")
            Cab.Append("</tr>")
            Cab.Append("</table>")
            Cab.Append("<br>")
            Cab.Append("<br>")





            Dim filas As Integer = 0
            filas = grwHistorial.Rows.Count
            
            Cab.Append("<table style='width: 100%; border-collapse: collapse;' border='1' cellspacing='0' cellpadding='3'>")
            Cab.Append("<thead>")
            Cab.Append("<tr style='border: 1px solid rgb(153, 186, 226); color: rgb(51, 102, 204); background-color: rgb(232, 238, 247);'>")
            Cab.Append("<th scope='col'>Ciclo</th>")
            Cab.Append("<th scope='col'>Semestre</th>")
            Cab.Append("<th scope='col'>Area</th>")
            Cab.Append("<th scope='col'>Curso</th>")
            Cab.Append("<th scope='col'>Crd.</th>")
            Cab.Append("<th scope='col'>Grupo</th>")
            Cab.Append("<th scope='col'>Nota Final</th>")
            Cab.Append("<th scope='col'>Condici&oacute;n</th>")
            Cab.Append("<th scope='col'>Veces Desaprob.</th>")
            Cab.Append("<th scope='col'>Plan Est - Escuela</th>")
            Cab.Append("</tr>")
            Cab.Append("</thead>")
            Cab.Append("<tbody>")
            With grwHistorial
                For i As Integer = 0 To filas - 1
                    Cab.Append("<tr class='A' style='border: 1px solid rgb(194, 207, 241);'  bgcolor='#ffffff'>")
                    Cab.Append("<td align='center' style='width: 15%;'>" & .Rows(i).Cells(0).Text.ToString & "</td>")
                    Cab.Append("<td align='center'>" & .Rows(i).Cells(1).Text.ToString & "</td>")
                    Cab.Append("<td align='center' style='width: 5%;'>" & .Rows(i).Cells(2).Text.ToString & "</td>")
                    Cab.Append("<td>" & ReemplazaTilde(.Rows(i).Cells(3).Text.ToString) & "</td>")
                    Cab.Append("<td align='center'>" & .Rows(i).Cells(4).Text.ToString & "</td>")
                    Cab.Append("<td align='center'>" & .Rows(i).Cells(5).Text.ToString & "</td>")
                    Cab.Append("<td align='center'>" & .Rows(i).Cells(6).Text.ToString & "</td>")
                    Cab.Append("<td align='center'>" & .Rows(i).Cells(7).Text.ToString & "</td>")
                    Cab.Append("<td align='center'>" & .Rows(i).Cells(8).Text.ToString & "</td>")
                    Cab.Append("<td style='font-size: 7pt;'>" & .Rows(i).Cells(10).Text.ToString & "</td>")
                    Cab.Append("</tr>")


                Next

            End With
            Cab.Append("</tbody>")
            Cab.Append("</table>")
            Cab.Append("<br>")
            Cab.Append("<br>")

            Cab.Append("<table style='width: 100%;'>")
            Cab.Append("<tbody>")
            Cab.Append("<tr>")
            Cab.Append("<td style='width: 60%;'>* Matr&iacute;cula por Convalidaci&oacute;n<br>** Matr&iacute;cula por Examen de Ubicaci&oacute;n<br>*** Matr&iacute;cula por Examen de Suficiencia<br>**** Examen de Recuperaci&oacute;n<br></td>")
            Cab.Append("<td align='right' style='width: 40%;'>")
            Cab.Append("<table style='border-color: rgb(192, 192, 192); width: 100%; border-collapse: collapse;' border='1' cellspacing='0' cellpadding='3'>")
            Cab.Append("<tbody>")
            Cab.Append("<tr class='usatEncabezadoTabla'>")
            Cab.Append("<td width='30%' style='font-weight: bold;'>RESUMEN DE MATRICULA</td>")
            Cab.Append("<td width='30%' style='font-weight: bold;'>Regular</td>")
            Cab.Append("<td width='30%' style='font-weight: bold;'>Convalidaci&oacute;n</td>")
            Cab.Append("<td width='10%'>TOTAL</td>")
            Cab.Append("</tr>")
            Cab.Append("<tr>")
            Cab.Append("<td width='90%' style='font-weight: bold;'>Cr&eacute;ditos Aprobadas</td>")
            Cab.Append("<td width='90%' align='center' style='font-weight: bold;'><span class='azul' style='font-weight: bold;'>" & lblCrdAprob.Text.ToString & " </span></td>")
            Cab.Append("<td width='90%' align='center' style='font-weight: bold;'><span class='azul'  style='font-weight: bold;'>" & lblCrdAprobC.Text.ToString & "</span></td>")
            Cab.Append("<td width='90%' align='center' style='font-weight: bold;'><span class='azul'  style='font-weight: bold;'>" & lblCrd.Text.ToString & "</span></td>")
            Cab.Append("</tr>")
            Cab.Append("<tr>")
            Cab.Append("<td width='90%' style='font-weight: bold;'>Asignaturas Aprobadas</td>")
            Cab.Append("<td width='90%' align='center' style='font-weight: bold;'><span class='azul' id='lblAsigAprob' style='font-weight: bold;'>" & lblAsigAprob.Text.ToString & "</span></td>")
            Cab.Append("<td width='90%' align='center' style='font-weight: bold;'><span class='azul' id='lblAsigAprobC' style='font-weight: bold;'>" & lblAsigAprobC.Text.ToString & "</span></td>")
            Cab.Append("<td width='10%'><span class='azul' id='lblAsig' style='font-weight: bold;'>" & lblAsig.Text.ToString & "</span></td></tr>")
            Cab.Append("<tr>")
            Cab.Append("<td align='right' style='width: 180%; font-weight: bold;' colspan='3'>Promedio Ponderado Acumulado</td>")
            Cab.Append("<td width='10%'><span class='azul' id='lblPond' style='font-weight: bold;'>" & lblPond.Text.ToString & "</span></td>")
            Cab.Append("</tr>")
            Cab.Append("</tbody>")
            Cab.Append("</table>")
            Cab.Append("</td>")
            Cab.Append("</tr>")
            Cab.Append("</tbody></table>")


            Response.Write("<html><head><style type='text/css'>" & sb.ToString() & "</style></head><body>" & Cab.ToString & "</body></html>")

            Response.Flush()
            Response.[End]()




        Catch ex As Exception
            Response.Write(ex.ToString())
        End Try
    End Sub

    Public Function ReemplazaTilde(ByVal palabra As String) As String
        palabra = palabra.Replace("Á", "&Aacute;")
        palabra = palabra.Replace("É", "&Eacute;")
        palabra = palabra.Replace("Í", "&Iacute;")
        palabra = palabra.Replace("Ó", "&Oacute;")
        palabra = palabra.Replace("Ú", "&Uacute;")
        palabra = palabra.Replace("Ñ", "&Ntilde;")
        palabra = palabra.Replace("ñ", "&ntilde;")
        palabra = palabra.Replace("á", "&aacute;")
        palabra = palabra.Replace("é", "&eacute;")
        palabra = palabra.Replace("í", "&iacute;")
        palabra = palabra.Replace("ó", "&oacute;")
        palabra = palabra.Replace("ú", "&uacute;")

        Return palabra
    End Function
End Class

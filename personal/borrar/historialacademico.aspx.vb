Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web.Security
Imports EncriptaCodigos
Imports cEntidad
Imports cLogica
Imports System.Collections.Generic
Partial Class historialacademico
    Inherits System.Web.UI.Page
    Private oeHistorialAcademico As eHistorialAcademico
    Private olHistorialAcademico As lHistorialAcademico
    Private oeCarreraProfesional As eCarreraProfesional
    Private olCarreraProesional As lCarreraProesional

    Private oeAlumno As eAlumno
    Private olAlumno As lAlumno

    Private dtCpf As DataTable
    Private dtCiclos As DataTable
    Private dtCursos As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.Page.User.Identity.IsAuthenticated Then
            Response.Redirect("~/Default.aspx")
            Exit Sub
        Else
            Dim tipoAlumni As String
            tipoAlumni = Trim(Session("egresado_Alu"))

            If tipoAlumni = "1" Then
                Me.h3Titulo.InnerHtml = "Historial Académico Egresado"
                CabeceraEgresado()
            Else
                Me.h3Titulo.InnerHtml = "Historial Académico"
                Cabecera()
            End If
            'Historial()
            'Listar()

        End If

    End Sub
    Private Sub Cabecera()
        Dim strTbody As New StringBuilder
        strTbody.Append("<div class='panel-body'>")

        strTbody.Append("<div class='col-xs-6 col-md-2'>")
        strTbody.Append("<img src='" & Session("rutaFoto") & "' style= 'height:110px; width:110px'>")
        strTbody.Append("</div>")

        strTbody.Append("<div class='col-xs-12 col-md-10'>")
        strTbody.Append("<table class='display dataTable'><tr role='row'><td>Código Universitario </td>")
        strTbody.Append("<td>" & Session("codigoUniver_Alu") & "</td></tr>")
        strTbody.Append("<tr role='row'><td>Apellidos y Nombres </td>")
        strTbody.Append("<td>" & Session("nombreCompleto") & "</td></tr>")
        strTbody.Append("<tr role='row'><td>Carrera Profesional</td>")
        strTbody.Append("<td>" & ComboCpf() & "</td></tr>")
        strTbody.Append("<tr role='row'><td>Ciclo Ingreso </td>")
        strTbody.Append("<td>" & Session("cicloIng_Alu") & "</td></tr>")
        strTbody.Append("<tr role='row'><td>Plan de Estudio </td>")
        strTbody.Append("<td>" & Session("descripcion_Pes") & "</td></tr></table>")
        strTbody.Append("<div class='form-group'>")
        strTbody.Append("<div class='col-sm-offset-4 col-sm-8'>")
        strTbody.Append("<input type='button' id='btnConsultar' class='btn btn-success' value='Consultar'>")
        strTbody.Append("</div>")
        strTbody.Append("</div>")
        strTbody.Append("</div></div>")
        Me.pCabecera.innerHtml = strTbody.ToString
    End Sub
    Private Sub CabeceraEgresado()
        Dim strTbody As New StringBuilder
        Dim ruta As String
        ruta = "https://intranet.usat.edu.pe/campusvirtual/librerianet/egresado/fotos/"

        strTbody.Append("<div class='panel-body'>")
        strTbody.Append("<div class='col-xs-6 col-md-2'>")
        'strTbody.Append("<img src='" & ruta & Session("foto_AEgre") & "' style= 'height:110px; width:110px'>")
        'strTbody.Append("<img src='" & Session("rutaFotoEgre2") & "' style= 'height:110px; width:110px'>")
        strTbody.Append(Session("rutaFotoEgre2"))
        strTbody.Append("</div>")
        strTbody.Append("<div class='col-xs-12 col-md-10'>")
        strTbody.Append("<table class='display dataTable'>")
        strTbody.Append("<tr role='row'><td>Apellidos y Nombres </td>")
        strTbody.Append("<td>" & Session("nombreCompleto") & "</td></tr>")
        strTbody.Append("<tr role='row'><td>Documento de Identidad </td>")
        strTbody.Append("<td>" & Session("nroDocIdent_Alu") & "</td></tr>")
        strTbody.Append("<tr role='row'><td>Carrera Profesional</td>")
        strTbody.Append("<td>" & ComboCpfEgresado() & "</td></tr>")
        strTbody.Append("</table>")
        strTbody.Append("<div class='form-group'>")
        strTbody.Append("<div class='col-sm-offset-4 col-sm-8'>")
        strTbody.Append("<input type='button' id='btnConsultar' class='btn btn-success' value='Consultar'>")
        strTbody.Append("</div>")
        strTbody.Append("</div>")
        strTbody.Append("</div></div>")
        Me.pCabecera.InnerHtml = strTbody.ToString
    End Sub
    Private Function ComboCpf() As String
        Try
            Dim cbo As New StringBuilder
            Dim i As Integer = 0
            oeCarreraProfesional = New eCarreraProfesional
            olCarreraProesional = New lCarreraProesional
            dtCpf = New DataTable

            oeCarreraProfesional.tipooperacion = "24"
            oeCarreraProfesional.codigo_alu = Session("codigo_Alu")
            dtCpf = olCarreraProesional.ConsultarCarreraProfesionalHistorialAcademico(oeCarreraProfesional)

            ' cbo.Append("")
            cbo.Append("<select id='cbocpf' class='name_search form-control'>")
            ' cbo.Append("<option value='0'>--Todas las escuelas que se matriculó--</option>")
            If dtCpf.Rows.Count > 0 Then
                For i = 0 To dtCpf.Rows.Count - 1
                    If i = 0 Then
                        cbo.Append("<option value='" & dtCpf.Rows(i).Item("codigo_cpf").ToString & "' selected='selected'>" & dtCpf.Rows(i).Item("nombre_cpf").ToString & "</option>")
                    Else
                        cbo.Append("<option value='" & dtCpf.Rows(i).Item("codigo_cpf").ToString & "'>" & dtCpf.Rows(i).Item("nombre_cpf").ToString & "</option>")
                    End If

                Next
            End If
            cbo.Append("</select>")
            Return cbo.ToString
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function ComboCpfEgresado() As String
        Try
            Dim cbo As New StringBuilder
            Dim i As Integer = 0
            oeCarreraProfesional = New eCarreraProfesional
            olCarreraProesional = New lCarreraProesional

            oeAlumno = New eAlumno
            olAlumno = New lAlumno

            dtCpf = New DataTable
            oeAlumno.nroDocIdent_Alu = Session("nroDocIdent_Alu")
            dtCpf = olAlumno.ListarTiposEstudio(oeAlumno)

            cbo.Append("<select id='cbocpf' class='name_search form-control'>")
            'cbo.Append("<option value='0'>Seleccione</option>")
            If dtCpf.Rows.Count > 0 Then
                For i = 0 To dtCpf.Rows.Count - 1
                    If i = 0 Then
                        cbo.Append("<option value='" & dtCpf.Rows(i).Item("codigo_Alu").ToString & "' selected='selected'>" & dtCpf.Rows(i).Item("nombre_Cpf").ToString & "</option>")
                    Else
                        cbo.Append("<option value='" & dtCpf.Rows(i).Item("codigo_Alu").ToString & "'>" & dtCpf.Rows(i).Item("nombre_Cpf").ToString & "</option>")
                    End If
                Next
            End If
            cbo.Append("</select>")
            Return cbo.ToString
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub Listar()
        Try
            Dim strTbody As New StringBuilder
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim vecesCurso As String
            Dim estadoDma As String
            Dim totalCreditos As Double
            Dim notaCreditos As Double
            Dim promedio As Double
            Dim CAR As Integer = 0
            Dim AAR As Integer = 0
            Dim CAC As Integer = 0
            Dim AAC As Integer = 0
            dtCiclos = New DataTable

            If Session("lstHistorialAcademico") Is Nothing Then
                oeHistorialAcademico = New eHistorialAcademico
                olHistorialAcademico = New lHistorialAcademico
                oeHistorialAcademico.tipo_Hac = 1
                oeHistorialAcademico.codigoAlu_Hac = Session("codigo_Alu")
                oeHistorialAcademico.codigoCac_Hac = 0
                dtCiclos = olHistorialAcademico.ConsultarHistorialAcademico(oeHistorialAcademico)
                Session.Add("lstHistorialAcademico", dtCiclos)
            Else
                dtCiclos = Session("lstHistorialAcademico")
            End If


            If dtCiclos.Rows.Count > 0 Then
                strTbody.Append("<div class='panel-body'>")

                strTbody.Append("<div class='table-responsive'>")
                strTbody.Append("<table class='display dataTable'>")

                For i = 0 To dtCiclos.Rows.Count - 1
                    totalCreditos = 0
                    notaCreditos = 0
                    strTbody.Append("<tr class='odd' ><th class='text-center' colspan=10> " & dtCiclos.Rows(i).Item("descripcion_cac").ToString & "</th></tr>")
                    strTbody.Append("<tr><th class='text-center'>Área</th>")
                    strTbody.Append("<th class='text-center'>Código</th>")
                    strTbody.Append("<th class='text-center'>Curso</th>")
                    strTbody.Append("<th class='text-center'>Ciclo</th>")
                    strTbody.Append("<th class='text-center'>Crd.</th>")
                    strTbody.Append("<th class='text-center'>Grupo</th>")
                    strTbody.Append("<th class='text-center'>Veces Desap.</th>")
                    strTbody.Append("<th class='text-center'>Nota Final</th>")
                    strTbody.Append("<th class='text-center'>Observaciones</th>")
                    strTbody.Append("<th class='text-center'>Detalles</th></tr>")

                    dtCursos = New DataTable
                    oeHistorialAcademico = New eHistorialAcademico
                    olHistorialAcademico = New lHistorialAcademico
                    oeHistorialAcademico.tipo_Hac = 2
                    oeHistorialAcademico.codigoAlu_Hac = Session("codigo_Alu")
                    oeHistorialAcademico.codigoCac_Hac = dtCiclos.Rows(i).Item("codigo_cac")
                    dtCursos = olHistorialAcademico.ConsultarHistorialAcademico(oeHistorialAcademico)


                    If dtCursos IsNot Nothing Then
                        '    'Response.Write("ok")
                        'Else
                        If dtCursos.Rows.Count > 0 Then
                            'Response.Write(dtCursos.Rows.Count)
                            For j = 0 To dtCursos.Rows.Count - 1
                                estadoDma = ""

                                If dtCursos.Rows(j).Item("vecesCurso_Dmault").ToString = "0" Then
                                    vecesCurso = ""
                                Else
                                    vecesCurso = dtCursos.Rows(j).Item("vecesCurso_Dmault").ToString
                                End If

                                If dtCursos.Rows(j).Item("estado_Dma").ToString = "R" Then
                                    estadoDma = "Retirado"
                                End If

                                If estadoDma <> "Retirado" _
                                    And (dtCursos.Rows(j).Item("tipoMatricula_Dma").ToString = "N" Or dtCursos.Rows(j).Item("tipoMatricula_Dma").ToString = "A") Then


                                    totalCreditos += dtCursos.Rows(j).Item("creditoCur_Dma")


                                    notaCreditos += dtCursos.Rows(j).Item("NotaCredito")
                                End If

                        If dtCursos.Rows(j).Item("condicion_Dma").ToString = "A" Then
                            If dtCursos.Rows(j).Item("tipoMatricula_Dma").ToString <> "C" And dtCursos.Rows(j).Item("estado_Dma").ToString <> "R" Then
                                CAR += dtCursos.Rows(j).Item("creditoCur_Dma")
                                AAR += 1
                            ElseIf dtCursos.Rows(j).Item("tipoMatricula_Dma").ToString = "C" And dtCursos.Rows(j).Item("estado_Dma").ToString <> "R" Then
                                CAC += dtCursos.Rows(j).Item("creditoCur_Dma")
                                AAC += 1
                            End If
                        End If

                        strTbody.Append("<tr><td>" & dtCursos.Rows(j).Item("tipoCurso_Dma").ToString & "</td>")
                        strTbody.Append("<td>" & dtCursos.Rows(j).Item("identificador_cur").ToString & "</td>")

                        Select Case dtCursos.Rows(j).Item("tipoMatricula_Dma").ToString
                            Case "C" : strTbody.Append("<td>" & StrConv(dtCursos.Rows(j).Item("nombre_cur").ToString.ToLower, VbStrConv.ProperCase) & "*" & "</td>")
                            Case "U" : strTbody.Append("<td>" & StrConv(dtCursos.Rows(j).Item("nombre_cur").ToString.ToLower, VbStrConv.ProperCase) & "**" & "</td>")
                            Case "S" : strTbody.Append("<td>" & StrConv(dtCursos.Rows(j).Item("nombre_cur").ToString.ToLower, VbStrConv.ProperCase) & "***" & "</td>")
                            Case "R" : strTbody.Append("<td>" & StrConv(dtCursos.Rows(j).Item("nombre_cur").ToString.ToLower, VbStrConv.ProperCase) & "****" & "</td>")
                                    Case Else : strTbody.Append("<td>" & StrConv(dtCursos.Rows(j).Item("nombre_cur").ToString.ToLower, VbStrConv.ProperCase) & "</td>")
                                End Select

                                strTbody.Append("<font style='color: red;'> Inhabilitado</font></td>")


                        strTbody.Append("<td class='text-center'>" & dtCursos.Rows(j).Item("ciclo_cur").ToString & "</td>")
                        strTbody.Append("<td class='text-right'>" & dtCursos.Rows(j).Item("creditoCur_Dma").ToString & "</td>")

                        'If dtCursos.Rows(j).Item("estadonota_cup").ToString = "P" Or dtCursos.Rows(j).Item("estado_dma").ToString = "R" Then
                        '    strTbody.Append("<td class='text-center'> - </td>")
                        'Else
                        strTbody.Append("<td class='text-center'>" & dtCursos.Rows(j).Item("grupohor_cup").ToString & "</td>")
                        'End If

                        strTbody.Append("<td class='text-center'>" & vecesCurso & "</td>")
                        If dtCursos.Rows(j).Item("notaFinal_Dma") > 0 Then
                            strTbody.Append("<td class='text-right'>" & dtCursos.Rows(j).Item("notaFinal_Dma").ToString & "</td>")
                        Else
                            strTbody.Append("<td class = 'text-right text-danger'>" & dtCursos.Rows(j).Item("notaFinal_Dma").ToString & "</td>")
                        End If

                        strTbody.Append("<td class='text-center text-danger'>" & estadoDma & "</td>")
                        strTbody.Append("<td class='text-center'><a name='verDet' href='#' class='active' data-toggle='modal' data-target='#largemodal' data-id='" & dtCursos.Rows(j).Item("codigo_Dma").ToString & "'>Ver</a></td></tr>")
                            Next
                            strTbody.Append("<tr><th colspan = 3></th>")
                            strTbody.Append("<th >TOTAL</th>")
                            strTbody.Append("<th class='text-right'>" & totalCreditos & "</th>")
                            strTbody.Append("<th colspan=2></th>")

                            If totalCreditos > 0 Then
                                promedio = -1 ' CDec(dtCursos.Rows(j).Item("creditosTotal_mat")) / CDec(dtCursos.Rows(j).Item("sumaTotal_mat")) 'Math.Round(notaCreditos / totalCreditos, 2)
                            Else
                                promedio = 0
                            End If
                            strTbody.Append("<th class='text-right'>" & Convert.ToString(promedio) & "</th>")

                            strTbody.Append("<th colspan=2></th></tr>")
                            strTbody.Append("<tr><th>&nbsp;</th></tr>")
                        End If

                        dtCursos = Nothing

                    End If


                Next

                strTbody.Append("</table>")
                strTbody.Append("</div>")

                strTbody.Append("<div class='col-md-4'>")
                strTbody.Append("* Matrícula por Convalidación <br>")
                strTbody.Append("** Matrícula por Examen de Ubicación <br>")
                strTbody.Append("*** Matrícula por Examen de Suficiencia <br>")
                strTbody.Append("</div>")

                strTbody.Append("<div class='col-md-8'>")
                strTbody.Append("<table class='table table-bordered'>")
                strTbody.Append("<tr><th class='text-center' style ='line-height:1;'>Resumen</th><th class='text-center' style ='line-height:1;'>Matrícula Regular</th><th class='text-center' style ='line-height:1;'>Matrícula por Convalidación</th><th class='text-center' style ='line-height:1;'>Total</th></tr>")
                strTbody.Append("<tr><td style ='line-height:1;'>Créditos Aprobados</td><td class='text-right' style ='line-height:1;'> " & CAR & "</td><td class='text-right' style ='line-height:1;'>" & CAC & "</td><td class='text-right' style ='line-height:1;'>" & CAR + CAC & "</td></tr>")
                strTbody.Append("<tr><td style ='line-height:1;' style ='line-height:1;'>Asignaturas Aprobadas</td><td class='text-right' style ='line-height:1;'> " & AAR & "</td><td class='text-right' style ='line-height:1;'>" & AAC & "</td><td class='text-right' style ='line-height:1;'>" & AAR + AAC & "</td></tr>")
                strTbody.Append("</table>")
                strTbody.Append("</div>")
                strTbody.Append("</div>")

                strTbody.Append("<div class='modal fade' id='largemodal' tabindex='-1' role='dialog' aria-labelledby='myModalLabel' aria-hidden='true'>")
                strTbody.Append("<div class='modal-dialog-lg'>")
                strTbody.Append("<div class='modal-content'>")
                strTbody.Append("<div class='modal-header'>")
                strTbody.Append("<button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true' class='ti-close'></span></button>")
                strTbody.Append("<h4 class='modal-title' id='titulo'></h4>")
                strTbody.Append("</div>")
                strTbody.Append("<div class='modal-body'>")
                strTbody.Append("<table class='table table-bordered' id='tablaDetalleCurso'>")
                strTbody.Append("</table>")
                strTbody.Append("</div>")
                strTbody.Append("</div>")
                strTbody.Append("</div>")
                strTbody.Append("</div>")

                Me.pDatos.InnerHtml = strTbody.ToString
            End If
            dtCiclos = Nothing
        Catch ex As Exception
            'Response.Write(ex.Message)
        End Try

    End Sub

End Class

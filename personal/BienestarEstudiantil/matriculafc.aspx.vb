Imports System.IO
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web.Security
Imports EncriptaCodigos
Imports cEntidad
Imports cLogica
Imports cDatos
Imports System.Collections.Generic

Partial Class matriculafc
    Inherits System.Web.UI.Page
    'Private oeCursoProgramado As eCursoProgramado
    'Private olCursoProgramado As lCursoProgramado

    'Private oeMatricula As eMatricula
    'Private olMatricula As lMatricula

    'Dim listConfigMat As New List(Of Dictionary(Of String, Object))()
    'Dim dict As New Dictionary(Of String, Object)()
    ''Dim ponderado, notaMinima, precioCredito As Decimal
    ''Dim credMaxMat, totalCredAprob As Integer

    'Dim cartacompromiso As Boolean = False
    'Dim generadeuda As Boolean = False

    'Dim swIdioma As Boolean = False
    'Private dt As New DataTable
    'Dim titulo As String = ""

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    Try


    '        If Not Me.Page.User.Identity.IsAuthenticated Then
    '            Response.Redirect("~/Default.aspx")
    '            Exit Sub
    '        Else
    '            '  Response.Write(Session("carta"))
    '            '  Response.Write(Session("<br>"))
    '            ' Response.Write(Session("cartaST"))
    '            Dim olFunciones As New lFunciones



    '            Dim cpf As Boolean = True

    '            If Session("codigo_Cpf") = "262" Or Session("codigo_Cpf") = "263" Or Session("codigo_Cpf") = "269" Then
    '                cpf = False
    '            End If

    '            'Response.Write(Left(Session("codigoUniver_Alu").ToString(), 5))

    '            If Session("refrescar") = "Mat" Then
    '                Session("refrescar") = ""
    '                Dim scriptMsj As String = "fnMensaje('success', 'Su solicitud será respondida en el horario de atención de Lunes a Viernes de 08-00 - 17:00 hrs.');"
    '                ScriptManager.RegisterStartupScript(Me, GetType(Page), "MsjeInc", scriptMsj, True)

    '            End If


    '            If Session("tipo_Cac").ToString = "N" Then
    '                'PROCESO DE MATRICULA 
    '                titulo = "PROCESO DE MATRICULA"
    '                spGuardar.InnerHtml = "Guardar Matrícula"
    '            Else
    '                titulo = "CURSOS DE VERANO"
    '                spGuardar.InnerHtml = "Guardar Inscripción"
    '            End If

    '            Session("RetiroSituacion") = True
    '            Session("firmocarta") = "N"

    '            Dim AnioCiclo As String = Year(Date.Now).ToString
    '            Dim cic_ext As Boolean = False
    '            Dim strTbody As New StringBuilder
    '            Dim strIdioma As New StringBuilder
    '            h6Cuotas.InnerHtml = "Costo Cuota [" & Session("cuotas").ToString & "]"
    '            If Session("codigo_Alu").ToString.Length > 0 Then
    '                CargarPrerequisitos()

    '                If Session("codigo_min") <> 2 And (Session("cicloIng_Alu").ToString.Trim = (AnioCiclo & "-I") Or Session("cicloIng_Alu").ToString.Trim = (AnioCiclo & "-II")) Then
    '                    swIdioma = True
    '                    If Session("tipo_Cac").ToString = "N" Then

    '                        strIdioma.Append("<font style='color:red'><b>(*) Los alumnos con ciclo de Ingreso " & Year(Date.Now).ToString() & "-I" & " o " & Year(Date.Now).ToString() & "-II  no est&aacute;n autorizados para matricularse en curso de idiomas<b></font>")

    '                    End If

    '                    divIdioma.InnerHtml = strIdioma.ToString
    '                Else : swIdioma = False
    '                End If

    '                'Response.Write(Session("codigo_min"))
    '                If Session("cicloIng_Alu").ToString.Trim <> Session("descripcion_Cac").ToString.Trim Or (Session("codigo_min") = 2 Or Session("codigo_min") = 6) Then ' valida bloqueo ciclo de ingreso de cachimbos

    '                    cic_ext = True

    '                Else
    '                    If CInt(Session("codigo_mat")) > 0 Then
    '                        cic_ext = True
    '                    Else
    '                        If Left(Session("codigoUniver_Alu").ToString(), 5) = "172AE" Then
    '                            cic_ext = True
    '                        Else
    '                            cic_ext = False
    '                        End If



    '                    End If
    '                End If
    '            Else
    '                Response.Redirect("~/Default.aspx")
    '            End If
    '            '               Response.Write(cic_ext.ToString)
    '            '--------------------------------------------------------------------------------------------
    '            Dim script3 As String = "fnIncidentes()"
    '            ScriptManager.RegisterStartupScript(Me, GetType(Page), "lstInc", script3, True)


    '            If cic_ext Then


    '                If Session("tipo_Cac").ToString = "E" Then

    '                    If VerificarDeudaVerano() Then
    '                        'Response.Write("SI TIENE DEUDA")

    '                    Else
    '                        'Response.Write("NO TIENE DEUDA")
    '                        fnDivInscripcionCursoVerano()
    '                    End If


    '                End If


    '                ' If AluTestCro Then ' Descomentar y comentar abajo para pruebas en produccion
    '                If Cronograma() Then  ' Descomentar y comentar arriba para pruebas en produccion
    '                    If Session("tipo_Cac").ToString = "E" Then

    '                        If VerificarDeudaVerano() Then
    '                            'Response.Write("SIN DEUDA")
    '                            Me.divInfo.InnerHtml = ""
    '                        Else
    '                            'Response.Write("CON DEUDA")
    '                            fnDivInscripcionCursoVerano()
    '                        End If


    '                    End If

    '                    If SituacionAlumno() Then

    '                        '  Response.Write(Session("RetiroSituacion").ToString)
    '                        '--------------------------------------------------------------------------------------------
    '                        ' If AlumnoPersonalizado() Then
    '                        CargarDatosMatricula()


    '                        If Session("tipo_Cac").ToString = "N" Then

    '                            Listar()
    '                            divLeyenda.Visible = False
    '                            dict.Add("swR", True)
    '                        ElseIf Session("tipo_Cac").ToString = "E" Then
    '                            ListarVerano()
    '                            divLeyenda.Visible = True
    '                            dict.Add("swR", False)
    '                            MensajeVerano()
    '                        End If

    '                        'txtcuotas.Value = Session("cuotas")
    '                        dict.Add("cuotas", Session("cuotas"))
    '                        dict.Add("tipocac", Session("tipo_Cac"))
    '                        listConfigMat.Add(dict)
    '                        Session.Add("lstConfMatJson", listConfigMat)
    '                        Session.Remove("lstCursosSeleccion")



    '                        '--------------------------------------------------------------------------------------------

    '                    Else
    '                        'Response.Write(Session("firmocarta").ToString)
    '                        If CInt(Session("codigo_mat")) = 0 Then
    '                            If cartacompromiso Then
    '                                Me.divFrameMat.InnerHtml = ""
    '                                fnDivCartaPresentacion()

    '                                If generadeuda Then
    '                                    Me.divInfo.InnerHtml = ""
    '                                End If


    '                            End If
    '                            If generadeuda = False Then
    '                                Me.divInfo.InnerHtml = ""

    '                            End If

    '                        End If

    '                    End If ' FIN  If SituacionAlumno() Then

    '                End If  ' FIN  If Cronograma() Then



    '            Else

    '                If cpf = True Then
    '                    '    strTbody.Append("<div class='manage_buttons' id='FiltrosA'>")
    '                    '    strTbody.Append("<div class='row'>")
    '                    '    strTbody.Append("<div class='col-md-10 search'>")
    '                    'strTbody.Append("<h3>" & titulo & " " & Session("descripcion_Cac") & " </h3>")

    '                    If Session("tipo_Cac").ToString = "N" Then
    '                        MensajeIngresante(titulo)
    '                    End If
    '                    'strTbody.Append("<font style='font-family: Arial;font-size: 11pt'>Lo sentimos, el proceso de matrícula <font color='red'>no se encuentra habilitado para estudiantes ingresantes <b>" & Session("cicloIng_Alu").ToString.Trim & "</b> </font> <br>")
    '                    '  strTbody.Append("<font style='font-family: Arial;font-size: 11pt'>Bienvenido. La matrícula para ingresantes " & Session("cicloIng_Alu") & " es de manera automática Por favor revisar tu <font color='red';font-weight:bold;> <a href='#' onclick=f_Menu('cursosmatriculados.aspx');>Ficha de matrícula</a></font></font> <br>") '


    '                    'strTbody.Append("</div>")
    '                    'strTbody.Append("</div>")
    '                    'strTbody.Append("</div>")
    '                    'Me.divFrameMat.InnerHtml = strTbody.ToString

    '                End If



    '            End If
    '            '--------------------------------------------------------------------------------------------

    '            If cpf = False Then


    '                strTbody.Append("<div class='row'>")
    '                strTbody.Append("<div class='col-md-12 nopad-right'>")

    '                strTbody.Append("<div class='panel panel-piluku'>")
    '                strTbody.Append("<div class='panel-heading'>")
    '                strTbody.Append("<h3 class='panel-title'>")
    '                strTbody.Append("<i class='ion-locked'></i>&nbsp;Bloqueos")
    '                strTbody.Append("</h3>")
    '                strTbody.Append("</div>")
    '                strTbody.Append("<div class='panel-body'>")

    '                strTbody.Append("<table border='1' cellpadding='3' cellspacing='0' style='border-collapse: collapse;  bordercolor='#111111' width='100%' class='display dataTable'>")
    '                strTbody.Append("<thead>")
    '                strTbody.Append("<tr  style='background-color: #2196f3; color:#FFFFFF;'><th width='60%' style='text-align:center;' > <b>Motivo</b>")
    '                strTbody.Append("</th>")
    '                strTbody.Append("<th  style='text-align:center;' > <b>Acudir A</b>")
    '                strTbody.Append("</th>")

    '                strTbody.Append("</tr>")
    '                strTbody.Append("</thead>")

    '                strTbody.Append("<tbody>")

    '                strTbody.Append("<tr>")
    '                strTbody.Append("<td><i class='ion-android-done-all'></i> Matrícula no disponible</td>")
    '                strTbody.Append("<td>DIrección Académica</td>")
    '                strTbody.Append("</tr>")

    '                strTbody.Append("</tbody>")
    '                strTbody.Append("</table>")
    '                strTbody.Append("</div>")

    '                strTbody.Append("</div>")
    '                strTbody.Append("</div>")

    '                Me.divFrameMat.InnerHtml = strTbody.ToString

    '            End If
    '        End If


    '        If CronogramaIncidencia() Then
    '            Me.pSolInc.InnerHtml = "<button class='btn btn-red' id='btnForo' data-toggle='modal' data-target='#mdForoReg' ><i class='icon ti-themify-favicon page_header_icon'></i><span>&nbsp;Atenci&oacute;n de Solicitud</span></button>"
    '        Else
    '            Me.pSolInc.InnerHtml = ""
    '        End If

    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    'Public Function CronogramaIncidencia() As Boolean
    '    Try

    '        Dim strTbody As New StringBuilder

    '        Dim rpta As Boolean = True
    '        oeMatricula = New eMatricula
    '        olMatricula = New lMatricula
    '        oeMatricula.tipooperacion = "INC"
    '        oeMatricula.codigo_Cac = Session("Codigo_Cac")
    '        oeMatricula.codigo_Alu = Session("Codigo_Alu")
    '        dt = olMatricula.validarAccesoMatricula(oeMatricula)


    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

    '            Session("msjeincest") = "Su solicitud será respondida de manera virtual en el horario de atención " & dt.Rows(0).Item("Observacion_Cro").ToString()
    '            Return True
    '        Else
    '            Session("msjeincest") = ""
    '            Return False
    '        End If


    '    Catch ex As Exception

    '    End Try
    'End Function
    'Private Sub MensajeVerano()
    '    Dim strIdioma As New StringBuilder

    '    strIdioma.Append("<ul>")
    '    strIdioma.Append("<li>")
    '    strIdioma.Append("<font style='color:blue;font-size:12px'>Mínimo 15 alumnos por grupo.<br></font>")
    '    strIdioma.Append("</li>")
    '    strIdioma.Append("<li>")
    '    strIdioma.Append("<font style='color:blue;font-size:12px'>Número máximo de créditos: 11.<br></font>")
    '    strIdioma.Append("</li>")

    '    If Session("condicionciclo_anterior") = "P" Then
    '        ' strIdioma.Append("<li>")
    '        ' strIdioma.Append("<font style='color:blue;font-size:12px'>Sólo puedes inscribirte máximo en <b>1 asignatura</b>.<br></font>")
    '        ' strIdioma.Append("</li>")
    '    ElseIf Session("condicionciclo_anterior") = "C" Then
    '        strIdioma.Append("<li>")
    '        strIdioma.Append("<font style='color:blue;font-size:12px'>Sólo puedes inscribirte máximo en <b>3 asignaturas (Sólo las condicionadas)</b>.<br></font>")
    '        strIdioma.Append("</li>")
    '    Else
    '        'strIdioma.Append("<font style='color:blue;font-size:12px'>Sólo puedes inscribirte máximo en <b>3 asignaturas</b>.<br></font>")
    '    End If

    '    strIdioma.Append("</ul>")
    '    'liAdelantar.InnerHtml = ""
    '    'liAdelantar.Visible = False
    '    divIdioma.InnerHtml = strIdioma.ToString
    'End Sub
    'Public Function AlumnoPersonalizado() As Boolean
    '    Try
    '        oeMatricula = New eMatricula
    '        olMatricula = New lMatricula
    '        oeMatricula.tipooperacion = "ESP"
    '        oeMatricula.codigo_Cac = Session("Codigo_Cac")
    '        oeMatricula.codigo_Alu = Session("Codigo_Alu")
    '        dt = olMatricula.validarAccesoMatriculav2(oeMatricula)
    '        'Response.Write(dt.Rows(0).Item("mensaje_blo").ToString)
    '        If dt.Rows.Count > 0 Then
    '            If dt.Rows(0).Item("mensaje_blo").ToString = "OK" Then
    '                Return True
    '            Else
    '                Return False
    '            End If
    '        Else
    '            Return False
    '        End If

    '    Catch ex As Exception
    '        Return False
    '    End Try

    'End Function

    'Public Function VerificarDeudaVerano() As Boolean
    '    Try

    '        Dim oe As New eDeuda
    '        Dim ol As New lDeuda
    '        Dim dtd As New DataTable
    '        oe.alumno.codigo_Alu = Session("Codigo_Alu")
    '        oe.codigo_cac = Session("Codigo_Cac")
    '        oe.codigo_sco = 246
    '        dtd = ol.VerificarDeuda(oe)

    '        If dtd.Rows.Count > 0 Then
    '            Return False
    '        Else
    '            Return True
    '        End If


    '    Catch ex As Exception
    '        Return Nothing
    '    End Try


    'End Function

    'Private Sub MensajeIngresante(ByVal titulo As String)
    '    Dim str As New StringBuilder


    '    str.Append("<div class='row'>")
    '    str.Append("<div class='col-md-12'>")
    '    str.Append("<div class='panel panel-piluku'>")
    '    str.Append("<div class='panel-heading'>")
    '    str.Append("<h3 class='panel-title'>" & titulo & " " & Session("descripcion_Cac") & "</h3>")
    '    str.Append("</div>")
    '    str.Append("<div class='table-responsive'>")
    '    str.Append("<div class='panel-body'>")
    '    str.Append("<div class='row'>")
    '    str.Append("<div class='form-group'>")
    '    str.Append("<div class='col-sm-12'  style='text-align:justify'>")
    '    ' str.Append("Mediante la presente, acepto la generación del cargo de S/. 100 soles correspondientes a la Inscripción de Cursos de Verano 2018. ")


    '    str.Append("<center>")
    '    str.Append("<img src='assets/images/mensaje_ingresantes_2018-II.png?QQ=5' style='width:50%;height:50%;' />")
    '    str.Append("</center>")



    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")


    '    Me.divFrameMat.InnerHtml = str.ToString
    'End Sub


    'Private Sub fnDivInscripcionCursoVerano()
    '    Dim str As New StringBuilder


    '    str.Append("<div class='row'>")
    '    str.Append("<div class='col-md-12'>")
    '    str.Append("<div class='panel panel-piluku'>")
    '    str.Append("<div class='panel-heading'>")
    '    str.Append("<h3 class='panel-title'>Inscripci&oacute;n de Cursos de Verano</h3>")
    '    str.Append("</div>")
    '    str.Append("<div class='table-responsive'>")
    '    str.Append("<div class='panel-body'>")
    '    str.Append("<div class='row'>")
    '    str.Append("<div class='form-group'>")
    '    str.Append("<div class='col-sm-12'  style='text-align:justify'>")
    '    ' str.Append("Mediante la presente, acepto la generación del cargo de S/. 100 soles correspondientes a la Inscripción de Cursos de Verano 2018. ")


    '    str.Append("<center>")
    '    str.Append("<img src='assets/images/CONFIRMACION_INSCRIPCION_VERANO.png?a=5' style='width:50%;height:50%;' />")
    '    str.Append("</center>")



    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("<div class='row'>")
    '    str.Append("<div class='form-group'>")
    '    str.Append("<div class='col-sm-12'>")
    '    str.Append("<center>")
    '    str.Append("<a href='#bodyPrincipal' id ='btnInscripcionA' class='btn btn-success'>Acepto</a>&nbsp;")
    '    str.Append("<a href='#bodyPrincipal' id ='btnInscripcionC' class='btn btn-danger'>No Acepto</a>")
    '    str.Append("</center>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")


    '    Me.divInfo.InnerHtml = str.ToString
    'End Sub

    'Private Sub fnDivCartaPresentacion()
    '    Dim str As New StringBuilder


    '    str.Append("<div class='row'>")
    '    str.Append("<div class='col-md-12'>")
    '    str.Append("<div class='panel panel-piluku'>")
    '    str.Append("<div class='panel-heading'>")
    '    str.Append("<h3 class='panel-title'>Carta de Compromiso</h3>")
    '    str.Append("</div>")
    '    str.Append("<div class='table-responsive'>")
    '    str.Append("<div class='panel-body'>")
    '    str.Append("<div class='row'>")
    '    str.Append("<div class='form-group'>")
    '    str.Append("<div class='col-sm-12' >")
    '    ' str.Append("Mediante la presente, me obligo desde este momento libre y voluntariamente  a aprobar cada asignatura en la que me inscriba durante los meses de  enero y febrero de 2018, las mismas que he desaprobado en ciclos anteriores, caso contrario acepto la medida de separación definitiva y automática de la Universidad establecida en el  artículo 47° del Reglamento de Estudios de Pregrado y el artículo 102 de la Ley Universitaria 30220.  ")

    '    str.Append("<center>")
    '    str.Append("<img src='assets/images/COMPROMISO_TEMPORAL-2018-II.png?y=2' style='width:50%;height:50%;' />")
    '    'str.Append("<img src='assets/images/COMPROMISO_TEMPORAL_verano2018.png?y=2' style='width:50%;height:50%;' />")
    '    str.Append("</center>")




    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("<div class='row'>")
    '    str.Append("<div class='form-group'>")
    '    str.Append("<div class='col-sm-12'>")
    '    str.Append("<center>")
    '    str.Append("<a href='#bodyPrincipal' id ='btnCartaA' class='btn btn-success'>Acepto</a>&nbsp;")
    '    str.Append("<a href='#bodyPrincipal' id ='btnCartaC' class='btn btn-danger'>No Acepto</a>")
    '    str.Append("</center>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")
    '    str.Append("</div>")


    '    Me.divCarta.InnerHtml = str.ToString
    'End Sub
    'Public Function Cronograma() As Boolean
    '    Try

    '        'If Session("tipo_Cac").ToString = "E" Then
    '        '    If Session("codigo_test") = 2 Then
    '        '        fnDivInscripcionCursoVerano()
    '        '    End If
    '        'End If


    '        '#DATOS PARA PRUEBAS EN PRODUCCION - COnsulta la tabla: alumnomatriculapersonalizada {
    '        Dim AluTest As Boolean = False

    '        AluTest = AlumnoPersonalizado() ' si devuelve falso es xq no esta en la lista para pruebas
    '        '#}#DATOS PARA PRUEBAS EN PRODUCCION - COnsulta la tabla: alumnomatriculapersonalizada 


    '        If AluTest = False Then


    '            Dim strTbody As New StringBuilder
    '            Dim i As Integer
    '            Dim rpta As Boolean = True
    '            oeMatricula = New eMatricula
    '            olMatricula = New lMatricula
    '            oeMatricula.tipooperacion = "ALU"
    '            oeMatricula.codigo_Cac = Session("Codigo_Cac")
    '            oeMatricula.codigo_Alu = Session("codigo_Alu")
    '            'Response.Write("1")
    '            dt = olMatricula.validarAccesoMatricula(oeMatricula)
    '            'Response.Write("2")

    '            If dt.Rows.Count > 0 Then

    '                strTbody.Append("<div class='manage_buttons' id='FiltrosA'>")
    '                strTbody.Append("<div class='row'>")
    '                strTbody.Append("<div class='col-md-10 search'>")

    '                If Session("tipo_Cac").ToString = "E" Then
    '                    strTbody.Append("<h3>" & titulo & " " & Session("descripcion_Cac").ToString.Substring(0, 4) & " </h3>")
    '                Else
    '                    strTbody.Append("<h3>" & titulo & " " & Session("descripcion_Cac") & " </h3>")
    '                End If


    '                strTbody.Append("<font style='font-family: Arial;font-size: 11pt'>Usted tiene <font color='red'><B>NO DISPONIBLE<B></font> esta opción por los siguientes motivos:<br>")

    '                strTbody.Append("</div>")
    '                strTbody.Append("</div>")
    '                strTbody.Append("</div>")

    '                strTbody.Append("<div class='row'>")
    '                strTbody.Append("<div class='col-md-12 nopad-right'>")

    '                strTbody.Append("<div class='panel panel-piluku'>")
    '                strTbody.Append("<div class='panel-heading'>")
    '                strTbody.Append("<h3 class='panel-title'>")
    '                strTbody.Append("<i class='ion-locked'></i>&nbsp;Bloqueos")
    '                strTbody.Append("</h3>")
    '                strTbody.Append("</div>")
    '                strTbody.Append("<div class='panel-body'>")

    '                strTbody.Append("<table border='1' cellpadding='3' cellspacing='0' style='border-collapse: collapse;  bordercolor='#111111' width='100%' class='display dataTable'>")
    '                strTbody.Append("<thead>")
    '                strTbody.Append("<tr  style='background-color: #2196f3; color:#FFFFFF;'><th width='60%' style='text-align:center;' > <b>Motivo</b>")
    '                strTbody.Append("</th>")
    '                strTbody.Append("<th  style='text-align:center;' > <b>Acudir A</b>")
    '                strTbody.Append("</th>")

    '                strTbody.Append("</tr>")
    '                strTbody.Append("</thead>")

    '                strTbody.Append("<tbody>")
    '                For i = 0 To dt.Rows.Count - 1

    '                    strTbody.Append("<tr>")
    '                    strTbody.Append("<td><i class='ion-android-done-all'></i> " & dt.Rows(i).Item("mensaje_blo").ToString & "</td>")
    '                    strTbody.Append("<td>" & dt.Rows(i).Item("acudirA_blo").ToString & "</td>")
    '                    strTbody.Append("</tr>")
    '                Next
    '                strTbody.Append("</tbody>")
    '                strTbody.Append("</table>")
    '                strTbody.Append("</div>")

    '                strTbody.Append("</div>")
    '                strTbody.Append("</div>")



    '                If Session("tipo_Cac").ToString = "E" Then
    '                    Dim cS As Integer = 0
    '                    Dim cN As Integer = 0
    '                    Dim cV As Integer = 0


    '                    If dt.Rows.Count > 0 Then
    '                        For i = 0 To dt.Rows.Count - 1
    '                            If dt.Rows(i).Item("cargoauto").ToString = "S" Then
    '                                cS = cS + 1
    '                            ElseIf dt.Rows(i).Item("cargoauto").ToString = "N" Then
    '                                cN = cN + 1
    '                            ElseIf dt.Rows(i).Item("cargoauto").ToString = "" Then
    '                                cV = cV + 1
    '                            End If

    '                        Next
    '                    End If



    '                    If cN > 0 Then
    '                        Me.divInfo.InnerHtml = ""
    '                        Me.divFrameMat.InnerHtml = strTbody.ToString
    '                        generadeuda = False
    '                        Return False
    '                    ElseIf cS > 0 Then
    '                        fnDivInscripcionCursoVerano()
    '                        generadeuda = True
    '                        Me.divFrameMat.InnerHtml = ""
    '                        Return True
    '                    End If

    '                Else
    '                    generadeuda = False
    '                    Me.divFrameMat.InnerHtml = strTbody.ToString
    '                    Return False
    '                End If







    '                Me.divFrameMat.InnerHtml = strTbody.ToString









    '                Return False
    '            End If

    '            If CInt(Session("codigo_mat").ToString) <> 0 Then
    '                dt = olMatricula.validarConogramaAgregadosyRetiros(oeMatricula)

    '                If dt.Rows.Count > 0 Then

    '                    strTbody.Append("<div class='manage_buttons' id='FiltrosA'>")
    '                    strTbody.Append("<div class='row'>")
    '                    strTbody.Append("<div class='col-md-10 search'>")
    '                    strTbody.Append("<h3>PROCESO DE AGREGADOS Y RETIROS " & Session("descripcion_Cac") & " </h3>")
    '                    strTbody.Append("<font style='font-family: Arial;font-size: 11pt'>Lo sentimos, usted tiene <font color='red'><B>BLOQUEADA<B></font> esta opción por los siguientes motivos:<br>")

    '                    strTbody.Append("</div>")
    '                    strTbody.Append("</div>")
    '                    strTbody.Append("</div>")

    '                    strTbody.Append("<div class='row'>")
    '                    strTbody.Append("<div class='col-md-12 nopad-right'>")


    '                    strTbody.Append("<div class='panel panel-piluku'>")
    '                    strTbody.Append("<div class='panel-heading'>")
    '                    strTbody.Append("<h3 class='panel-title'>")
    '                    strTbody.Append("<i class='ion-locked'></i>&nbsp;Bloqueos")
    '                    strTbody.Append("</h3>")
    '                    strTbody.Append("</div>")
    '                    strTbody.Append("<div class='panel-body'>")

    '                    strTbody.Append("<table border='1' cellpadding='3' cellspacing='0' style='border-collapse: collapse;  bordercolor='#111111' width='100%' class='display dataTable'>")
    '                    strTbody.Append("<thead>")
    '                    strTbody.Append("<tr  style='background-color: #2196f3; color:#FFFFFF;'><th width='60%' style='text-align:center;' > <b>Motivo</b>")
    '                    strTbody.Append("</th>")
    '                    strTbody.Append("<th  style='text-align:center;' > <b>Acudir A</b>")
    '                    strTbody.Append("</th>")

    '                    strTbody.Append("</tr>")
    '                    strTbody.Append("</thead>")

    '                    strTbody.Append("<tbody>")
    '                    For i = 0 To dt.Rows.Count - 1
    '                        strTbody.Append("<tr>")
    '                        strTbody.Append("<td><i class='ion-android-done-all'></i> " & dt.Rows(i).Item("mensaje_blo").ToString & "</td>")
    '                        strTbody.Append("<td>" & dt.Rows(i).Item("acudirA_blo").ToString & "</td>")
    '                        strTbody.Append("</tr>")
    '                    Next
    '                    strTbody.Append("</tbody>")
    '                    strTbody.Append("</table>")
    '                    strTbody.Append("</div>")

    '                    strTbody.Append("</div>")
    '                    strTbody.Append("</div>")

    '                    Me.divFrameMat.InnerHtml = strTbody.ToString


    '                    Return False
    '                End If
    '                ''verificar si tiene solicitud de dscto 

    '                oeMatricula.tipooperacion = "SDM"  'SOLICITUD DESCUERNTO MATRICULA
    '                oeMatricula.codigo_Cac = Session("Codigo_Cac")
    '                oeMatricula.codigo_Alu = Session("Codigo_Alu")
    '                dt = olMatricula.validarAccesoMatriculav2(oeMatricula)
    '                If dt.Rows.Count > 0 Then

    '                    strTbody.Append("<div class='manage_buttons' id='FiltrosA'>")
    '                    strTbody.Append("<div class='row'>")
    '                    strTbody.Append("<div class='col-md-10 search'>")
    '                    strTbody.Append("<h3>PROCESO DE AGREGADOS Y RETIROS " & Session("descripcion_Cac") & " </h3>")
    '                    strTbody.Append("<font style='font-family: Arial;font-size: 11pt'>Lo sentimos, usted tiene <font color='red'><B>BLOQUEADA<B></font> esta opción por los siguientes motivos:<br>")

    '                    strTbody.Append("</div>")
    '                    strTbody.Append("</div>")
    '                    strTbody.Append("</div>")

    '                    strTbody.Append("<div class='row'>")
    '                    strTbody.Append("<div class='col-md-12 nopad-right'>")


    '                    strTbody.Append("<div class='panel panel-piluku'>")
    '                    strTbody.Append("<div class='panel-heading'>")
    '                    strTbody.Append("<h3 class='panel-title'>")
    '                    strTbody.Append("<i class='ion-locked'></i>&nbsp;Bloqueos")
    '                    strTbody.Append("</h3>")
    '                    strTbody.Append("</div>")
    '                    strTbody.Append("<div class='panel-body'>")

    '                    strTbody.Append("<table border='1' cellpadding='3' cellspacing='0' style='border-collapse: collapse;  bordercolor='#111111' width='100%' class='display dataTable'>")
    '                    strTbody.Append("<thead>")
    '                    strTbody.Append("<tr  style='background-color: #2196f3; color:#FFFFFF;'><th width='60%' style='text-align:center;' > <b>Motivo</b>")
    '                    strTbody.Append("</th>")
    '                    strTbody.Append("<th  style='text-align:center;' > <b>Acudir A</b>")
    '                    strTbody.Append("</th>")

    '                    strTbody.Append("</tr>")
    '                    strTbody.Append("</thead>")

    '                    strTbody.Append("<tbody>")
    '                    For i = 0 To dt.Rows.Count - 1
    '                        strTbody.Append("<tr>")
    '                        strTbody.Append("<td><i class='ion-android-done-all'></i> " & dt.Rows(i).Item("mensaje_blo").ToString & "</td>")
    '                        strTbody.Append("<td>" & dt.Rows(i).Item("acudirA_blo").ToString & "</td>")
    '                        strTbody.Append("</tr>")
    '                    Next
    '                    strTbody.Append("</tbody>")
    '                    strTbody.Append("</table>")
    '                    strTbody.Append("</div>")

    '                    strTbody.Append("</div>")
    '                    strTbody.Append("</div>")

    '                    Me.divFrameMat.InnerHtml = strTbody.ToString


    '                    Return False
    '                End If




    '            End If
    '            dt = Nothing
    '            Return True
    '        Else

    '            Return True

    '        End If

    '    Catch ex As Exception
    '        Response.Write(ex.Message)

    '    End Try
    'End Function



    'Public Function SituacionAlumno() As Boolean
    '    Try
    '        Dim strTbody As New StringBuilder
    '        Dim i As Integer
    '        Dim rpta As Boolean = True
    '        oeMatricula = New eMatricula
    '        olMatricula = New lMatricula
    '        oeMatricula.tipooperacion = "ALU"
    '        oeMatricula.codigo_Cac = Session("Codigo_Cac")
    '        oeMatricula.codigo_Alu = Session("Codigo_Alu")
    '        dt = olMatricula.validarAccesoMatriculav2(oeMatricula)



    '        ' If Session("tipo_Cac").ToString = "N" Then

    '        If dt.Rows.Count > 0 Then
    '            'Response.Write(dt.Rows(0).Item("mensaje_blo").ToString)

    '            ' strTbody.Append("<div class='row'>")
    '            'strTbody.Append("<div class='col-md-12'>")
    '            strTbody.Append("<div class='manage_buttons' id='FiltrosA'>")
    '            strTbody.Append("<div class='row'>")
    '            strTbody.Append("<div class='col-md-10 search'>")
    '            If Session("tipo_Cac").ToString = "E" Then
    '                strTbody.Append("<h3>" & titulo & " " & Session("descripcion_Cac").ToString.Substring(0, 4) & " </h3>")
    '            Else
    '                strTbody.Append("<h3>" & titulo & " " & Session("descripcion_Cac") & " </h3>")
    '            End If

    '            strTbody.Append("<font style='font-family: Arial;font-size: 11pt'>Usted tiene <font color='red'><B>NO DISPONIBLE<B></font> esta opción por los siguientes motivos:<br>")

    '            strTbody.Append("</div>")
    '            strTbody.Append("</div>")
    '            strTbody.Append("</div>")

    '            strTbody.Append("<div class='row'>")
    '            strTbody.Append("<div class='col-md-12 nopad-right'>")

    '            strTbody.Append("<div class='panel panel-piluku'>")
    '            strTbody.Append("<div class='panel-heading'>")
    '            strTbody.Append("<h3 class='panel-title'>")
    '            strTbody.Append("<i class='ion-locked'></i>&nbsp;Bloqueos")
    '            strTbody.Append("</h3>")
    '            strTbody.Append("</div>")
    '            strTbody.Append("<div class='panel-body'>")

    '            strTbody.Append("<table border='1' cellpadding='3' cellspacing='0' style='border-collapse: collapse;  bordercolor='#111111' width='100%' class='display dataTable'>")
    '            strTbody.Append("<thead>")
    '            strTbody.Append("<tr  style='background-color: #2196f3; color:#FFFFFF;'><th width='60%' style='text-align:center;' > <b>Motivo</b>")
    '            strTbody.Append("</th>")
    '            strTbody.Append("<th  style='text-align:center;' > <b>Acudir A</b>")
    '            strTbody.Append("</th>")

    '            strTbody.Append("</tr>")
    '            strTbody.Append("</thead>")

    '            strTbody.Append("<tbody>")
    '            For i = 0 To dt.Rows.Count - 1
    '                strTbody.Append("<tr>")
    '                strTbody.Append("<td><i class='ion-android-done-all'></i> " & dt.Rows(i).Item("mensaje_blo").ToString & "</td>")
    '                strTbody.Append("<td>" & dt.Rows(i).Item("acudirA_blo").ToString & "</td>")
    '                strTbody.Append("</tr>")
    '            Next
    '            strTbody.Append("</tbody>")
    '            strTbody.Append("</table>")
    '            strTbody.Append("</div>")

    '            strTbody.Append("</div>")
    '            strTbody.Append("</div>")

    '            'strTbody.Append("</div>")
    '            'strTbody.Append("</div>")






    '            'If dt.Rows(0).Item("firmocarta").ToString = "S" Then
    '            '    'Session("RetiroSituacion") = False
    '            '    Return True
    '            'Else


    '            '    If dt.Rows(0).Item("tipo").ToString = "4" Then
    '            '        'Response.Write("11")
    '            '        cartacompromiso = True  '' se activa para generar carta
    '            '        generadeuda = False
    '            '        Me.divInfo.InnerHtml = ""


    '            '        Return False

    '            '    Else
    '            '        'Response.Write("22")
    '            '        generadeuda = False
    '            '        cartacompromiso = False
    '            '        Me.divFrameMat.InnerHtml = strTbody.ToString
    '            '        Return False
    '            '    End If

    '            'End If





    '            '  Me.divFrameMat.InnerHtml = strTbody.ToString
    '            '  Return False


    '        End If

    '        'Else

    '        'End If

    '        dt = Nothing
    '        Return True


    '    Catch ex As Exception
    '        Return False
    '    Finally
    '        dt = Nothing
    '    End Try
    'End Function
    'Sub CargarPrerequisitos()

    '    oeMatricula = New eMatricula
    '    olMatricula = New lMatricula
    '    oeMatricula.codigo_Pes = Session("codigo_Pes")
    '    oeMatricula.codigo_Cac = Session("Codigo_Cac")
    '    oeMatricula.codigo_Alu = Session("Codigo_Alu")
    '    dt = olMatricula.consultarRequisitos(oeMatricula)
    '    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
    '        ' txtponderado.Value = dt.Rows(0).Item("Ponderado")
    '        'txtnotaMinima.Value = dt.Rows(0).Item("notaMinima")
    '        'txtcredMaxMat.Value = dt.Rows(0).Item("CreditoMaximoMatricula")
    '        ' txttotalCredAprob.Value = dt.Rows(0).Item("TotalCredAprobados")
    '        'If CDec(dt.Rows(0).Item("precioCredito")) = 0 Then
    '        '    txtprecioCredito.Value = 1
    '        'Else
    '        '    txtprecioCredito.Value = dt.Rows(0).Item("precioCredito")
    '        'End If


    '        If Session("tipo_Cac").ToString = "N" Then
    '            h6Promes.InnerHtml = "Promedio Ponderado <br>"
    '            h6Prom.InnerHtml = Format(dt.Rows(0).Item("Ponderado"), "###0.00")

    '        ElseIf Session("tipo_Cac").ToString = "E" Then
    '            h6Promes.InnerHtml = "Ciclo de Referencia <br>"
    '            h6Prom.InnerHtml = CInt(dt.Rows(0).Item("cicloRefMat")).ToString
    '            h6CostoSemestre.InnerHtml = "Costo Cursos Verano"
    '        End If


    '        Session.Add("codigo_mat", dt.Rows(0).Item("codigo_mat").ToString)

    '        ' txtmat.Value = dt.Rows(0).Item("codigo_mat").ToString


    '        Session.Add("condicionciclo_anterior", dt.Rows(0).Item("condicionciclo_anterior").ToString)


    '        'If dt.Rows(0).Item("condicionciclo_anterior").ToString = "O" Then
    '        '    Me.h6SituacionAlumno.InnerHtml = "Matrícula en observación"
    '        'ElseIf dt.Rows(0).Item("condicionciclo_anterior").ToString = "P" Then
    '        '    Me.h6SituacionAlumno.InnerHtml = "Matrícula con <br>problemas académicos"
    '        'ElseIf dt.Rows(0).Item("condicionciclo_anterior").ToString = "C" Then
    '        '    Me.h6SituacionAlumno.InnerHtml = "Matrícula Condicional"
    '        'Else
    '        '    Me.h6SituacionAlumno.InnerHtml = "Matrícula Regular"
    '        'End If


    '        If Session("tipo_Cac").ToString = "N" Then


    '            'CONFIGURACION 2017-II
    '            If dt.Rows(0).Item("condicionciclo_anterior").ToString = "O" Then
    '                Me.h6SituacionAlumno.InnerHtml = "Oservación"
    '            ElseIf dt.Rows(0).Item("condicionciclo_anterior").ToString = "P" Then
    '                Me.h6SituacionAlumno.InnerHtml = "Problemas académicos"
    '            ElseIf dt.Rows(0).Item("condicionciclo_anterior").ToString = "C" Then
    '                Me.h6SituacionAlumno.InnerHtml = "Problemas académicos"
    '            Else
    '                Me.h6SituacionAlumno.InnerHtml = "Regular"
    '            End If
    '            'CONFIGURACION 2017-II

    '        Else

    '            'CONFIGURACION 2018-0
    '            If dt.Rows(0).Item("condicionciclo_anterior").ToString = "O" Then
    '                Me.h6SituacionAlumno.InnerHtml = "Observación"
    '            ElseIf dt.Rows(0).Item("condicionciclo_anterior").ToString = "P" Then
    '                Me.h6SituacionAlumno.InnerHtml = "Problemas académicos"
    '            ElseIf dt.Rows(0).Item("condicionciclo_anterior").ToString = "C" Then
    '                Me.h6SituacionAlumno.InnerHtml = "Condicional"
    '            Else
    '                Me.h6SituacionAlumno.InnerHtml = "Regular"
    '            End If
    '            'CONFIGURACION 2018-0

    '        End If

    '        'txtcond.Value = dt.Rows(0).Item("condicionciclo_actual").ToString

    '        dict.Add("ponderado", dt.Rows(0).Item("Ponderado"))
    '        dict.Add("notaMinima", CDec(dt.Rows(0).Item("notaMinima")))
    '        dict.Add("credMaxMat", dt.Rows(0).Item("CreditoMaximoMatricula"))
    '        dict.Add("credMaxMatcnf", dt.Rows(0).Item("CreditoMaximoMatricula")) '#EPENA 26072019  [SQL] Incidencias de Matricula: 12.1 Redmine Tareas 4083
    '        Session.Add("credMaxMat", dt.Rows(0).Item("CreditoMaximoMatricula"))
    '        Session.Add("credMaxMatcnf", dt.Rows(0).Item("CreditoMaximoMatricula")) '#EPENA 26072019  [SQL] Incidencias de Matricula: 12.1 Redmine Tareas #4083
    '        dict.Add("totalCredAprob", dt.Rows(0).Item("TotalCredAprobados"))
    '        dict.Add("precioCred", dt.Rows(0).Item("precioCredito"))
    '        dict.Add("codMat", dt.Rows(0).Item("codigo_mat"))
    '        dict.Add("cond", dt.Rows(0).Item("condicionciclo_anterior").ToString)
    '        dict.Add("ade", CInt(dt.Rows(0).Item("adelantar")))
    '        dict.Add("niv", CInt(dt.Rows(0).Item("nivelar")))

    '        '------------ ciclo de verano 2017 - 0-----------
    '        dict.Add("cicloRefMat", CInt(dt.Rows(0).Item("cicloRefMat")))
    '        dict.Add("ivt", CInt(dt.Rows(0).Item("numCursosDesap")))
    '        dict.Add("ncrdSituacion", CInt(dt.Rows(0).Item("credSituacionMat")))

    '        If CInt(dt.Rows(0).Item("numCursosDesap")) = 1 Then
    '            dict.Add("cicloCurMax", CInt(dt.Rows(0).Item("cicloRefMat")))
    '            Session.Add("cicloCurMax", CInt(dt.Rows(0).Item("cicloRefMat")))
    '        Else
    '            dict.Add("cicloCurMax", 14)
    '            Session.Add("cicloCurMax", 14)
    '        End If

    '        '------------------------------------------------
    '        '-------------------Reglas-----------------------
    '        'Response.Write(dt.Rows(0).Item("cumpleElectivo"))
    '        dict.Add("rElec", CBool(dt.Rows(0).Item("cumpleElectivo")))
    '        Session.Add("rElec", CBool(dt.Rows(0).Item("cumpleElectivo")))

    '        dict.Add("elecPes", CInt(dt.Rows(0).Item("elecPes")))
    '        Session.Add("elecPes", CInt(dt.Rows(0).Item("elecPes")))

    '        dict.Add("elecAprob", CInt(dt.Rows(0).Item("elecAprob")))
    '        Session.Add("elecAprob", CInt(dt.Rows(0).Item("elecAprob")))

    '        dict.Add("rIdi", CBool(dt.Rows(0).Item("cumpleIdioma")))
    '        Session.Add("rIdi", CBool(dt.Rows(0).Item("cumpleIdioma")))

    '        'dict.Add("infoElec", dt.Rows(0).Item("cumpleElectivoInfo"))
    '        If dt.Rows(0).Item("cumpleElectivoInfo").ToString() <> "" Then
    '            liElec.InnerHtml = "<font style='color:blue;'>Tienes " & dt.Rows(0).Item("cumpleElectivoInfo").ToString() & " según tu plan de estudios.</font>"
    '        Else
    '            liElec.Visible = False
    '        End If

    '        '------------------------------------------------
    '        Session.Add("codigo_min", dt.Rows(0).Item("codigo_min"))
    '        dict.Add("codigo_min", dt.Rows(0).Item("codigo_min"))

    '        Session.Add("carta", dt.Rows(0).Item("carta"))
    '        dict.Add("carta", dt.Rows(0).Item("carta"))


    '        'Response.Write(Session("rIdi"))
    '        dt = Nothing
    '    End If

    'End Sub

    'Sub CargarDatosMatricula()
    '    oeMatricula = New eMatricula
    '    olMatricula = New lMatricula
    '    oeMatricula.codigo_Cpf = Session("codigo_Cpf")
    '    oeMatricula.codigo_Cac = Session("Codigo_Cac")
    '    oeMatricula.codigo_Sccg = 0
    '    dt = olMatricula.consultarDatosMatricula(oeMatricula)
    '    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

    '        'txtveces1.Value = dt.Rows(0).Item("desaprobado1veces").ToString.Replace(",", ".")
    '        ' txtvecesn.Value = dt.Rows(0).Item("desaprobado2veces").ToString.Replace(",", ".")
    '        'txtmaxCred.Value = dt.Rows(0).Item("maxCred")
    '        'txtmontoCredPen.Value = dt.Rows(0).Item("montoCredpen")

    '        dict.Add("c1veces", CDec(dt.Rows(0).Item("desaprobado1veces")))
    '        dict.Add("c2veces", CDec(dt.Rows(0).Item("desaprobado2veces")))
    '        dict.Add("maxCred", dt.Rows(0).Item("maxCred"))
    '        dict.Add("montoCredpen", dt.Rows(0).Item("montoCredpen"))

    '    End If
    '    Me.txtcpf.Value = Session("codigo_Cpf")
    'End Sub

    'Private Sub Listar()
    '    ' Dim strTbody As New StringBuilder
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Dim codigo_cup As Integer = 0
    '    Dim i As Integer = 0
    '    oeCursoProgramado = New eCursoProgramado
    '    olCursoProgramado = New lCursoProgramado
    '    oeCursoProgramado.codigo_Pes = Session("codigo_Pes")
    '    oeCursoProgramado.codigo_cac = Session("Codigo_Cac")
    '    oeCursoProgramado.codigo_Alu = Session("Codigo_Alu")
    '    dt = olCursoProgramado.consultarCursosHabilesMatricula(oeCursoProgramado)
    '    Session.Add("lstCursosDisponibles", dt)
    '    'Response.Write("sw " & swIdioma)

    '    ' Response.Write(dt.Rows.Count)
    '    Dim LstCiclos As String = ""


    '    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
    '        For i = 0 To dt.Rows.Count - 1
    '            LstCiclos = LstCiclos & dt.Rows(i).Item("ciclo_cur").ToString & ","

    '            If i < (dt.Rows.Count - 1) Then

    '                '  If swIdioma = False And dt.Rows(i).Item("tipocomplementario_cur").ToString <> "I" Then

    '                If swIdioma And dt.Rows(i).Item("tipocomplementario_cur").ToString = "I" Then
    '                    Continue For
    '                End If

    '                Dim dict As New Dictionary(Of String, Object)()
    '                If dt.Rows(i).Item("codigo_cur") <> dt.Rows(i + 1).Item("codigo_cur") Then

    '                    dict.Add("codigo_cur", dt.Rows(i).Item("codigo_cur"))
    '                    dict.Add("nombre_Cur", dt.Rows(i).Item("nombre_Cur").ToString)
    '                    dict.Add("vecesdesaprobada", dt.Rows(i).Item("vecesdesaprobada"))
    '                    dict.Add("electivo_cur", dt.Rows(i).Item("electivo_cur").ToString)
    '                    dict.Add("spc", CInt(dt.Rows(i).Item("soloPrimerCiclo_cup")))
    '                    If dt.Rows(i).Item("cup_Mat").ToString = "0" Then
    '                        dict.Add("selCurso", "")
    '                    Else
    '                        dict.Add("selCurso", dt.Rows(i).Item("cup_Mat"))
    '                    End If

    '                    dict.Add("cup_Mat", dt.Rows(i).Item("cup_Mat"))
    '                    dict.Add("cod_Dmat", dt.Rows(i).Item("cod_Dmat"))
    '                    dict.Add("credCurso", dt.Rows(i).Item("creditos_cur"))
    '                    dict.Add("nomCurso", dt.Rows(i).Item("nombre_Cur").ToString.Replace("Ñ", "N"))
    '                    dict.Add("vCred", dt.Rows(i).Item("creditos_cur"))
    '                    dict.Add("vElec", CInt(dt.Rows(i).Item("electivo_cur")))
    '                    dict.Add("vCic", CInt(dt.Rows(i).Item("ciclo_cur")))
    '                    dict.Add("vcur", CInt(dt.Rows(i).Item("codigo_cur")))
    '                    dict.Add("vvec", CInt(dt.Rows(i).Item("vecesdesaprobada")))
    '                    dict.Add("vtc", dt.Rows(i).Item("tipocomplementario_cur").ToString)
    '                    dict.Add("velap", CInt(dt.Rows(i).Item("elec_aprob")))
    '                    dict.Add("vnel", CInt(dt.Rows(i).Item("nroelectivos")))
    '                    dict.Add("vidiap", CInt(dt.Rows(i).Item("idioma_aprob")))
    '                    dict.Add("vnidi", CInt(dt.Rows(i).Item("nroidiomas")))
    '                    dict.Add("vpc", CInt(dt.Rows(i).Item("preciocalculadocurso")))
    '                    'dict.Add("sIdi", CInt(dt.Rows(i).Item("selIdioma")))

    '                    If CInt(dt.Rows(i).Item("selIdioma")) = 1 Then
    '                        dict.Add("sIdi", True)
    '                    Else
    '                        dict.Add("sIdi", False)
    '                    End If

    '                    If validaEstado(CInt(dt.Rows(i).Item("codigo_cur"))) Then
    '                        dict.Add("stt", 1)
    '                        If CInt(dt.Rows(i).Item("cur_Mat")) = 0 Then
    '                            dict.Add("vCurM", 0)

    '                            dict.Add("chk", False)

    '                        Else
    '                            dict.Add("vCurM", 1)

    '                            dict.Add("chk", True)
    '                        End If
    '                    Else
    '                        dict.Add("stt", 0)

    '                        If CInt(dt.Rows(i).Item("cur_Mat")) > 0 Then

    '                            dict.Add("vCurM", 1)
    '                            dict.Add("chk", True)
    '                        Else
    '                            dict.Add("vCurM", 0)
    '                            dict.Add("chk", False)
    '                        End If
    '                    End If

    '                    list.Add(dict)
    '                End If
    '            End If
    '            ' End If
    '        Next

    '        ' If swIdioma = False And dt.Rows(dt.Rows.Count - 1).Item("tipocomplementario_cur").ToString <> "I" Then


    '        If swIdioma And dt.Rows(dt.Rows.Count - 1).Item("tipocomplementario_cur").ToString = "I" Then

    '            dt = Nothing
    '            Session.Add("lstCursosDisponiblesJson", list)
    '            Exit Sub
    '        End If


    '        Dim dict2 As New Dictionary(Of String, Object)()


    '        dict2.Add("codigo_cur", dt.Rows(dt.Rows.Count - 1).Item("codigo_cur"))
    '        dict2.Add("nombre_Cur", dt.Rows(dt.Rows.Count - 1).Item("nombre_Cur").ToString)
    '        dict2.Add("vecesdesaprobada", dt.Rows(dt.Rows.Count - 1).Item("vecesdesaprobada"))
    '        dict2.Add("electivo_cur", dt.Rows(dt.Rows.Count - 1).Item("electivo_cur").ToString)
    '        dict2.Add("spc", CInt(dt.Rows(dt.Rows.Count - 1).Item("soloPrimerCiclo_cup")))
    '        If dt.Rows(dt.Rows.Count - 1).Item("cup_Mat").ToString = "0" Then
    '            dict2.Add("selCurso", "")
    '        Else
    '            dict2.Add("selCurso", dt.Rows(dt.Rows.Count - 1).Item("cup_Mat"))
    '        End If
    '        dict2.Add("cod_Dmat", dt.Rows(dt.Rows.Count - 1).Item("cod_Dmat"))
    '        dict2.Add("cup_Mat", dt.Rows(dt.Rows.Count - 1).Item("cup_Mat"))
    '        dict2.Add("credCurso", dt.Rows(dt.Rows.Count - 1).Item("creditos_cur"))
    '        dict2.Add("nomCurso", dt.Rows(dt.Rows.Count - 1).Item("nombre_Cur").ToString.Replace("Ñ", "N"))
    '        dict2.Add("vCred", dt.Rows(dt.Rows.Count - 1).Item("creditos_cur"))
    '        dict2.Add("vElec", CInt(dt.Rows(dt.Rows.Count - 1).Item("electivo_cur")))
    '        dict2.Add("vCic", CInt(dt.Rows(dt.Rows.Count - 1).Item("ciclo_cur")))
    '        dict2.Add("vcur", CInt(dt.Rows(dt.Rows.Count - 1).Item("codigo_cur")))
    '        dict2.Add("vvec", CInt(dt.Rows(dt.Rows.Count - 1).Item("vecesdesaprobada")))
    '        dict2.Add("vtc", dt.Rows(dt.Rows.Count - 1).Item("tipocomplementario_cur").ToString)
    '        dict2.Add("velap", CInt(dt.Rows(dt.Rows.Count - 1).Item("elec_aprob")))
    '        dict2.Add("vnel", CInt(dt.Rows(dt.Rows.Count - 1).Item("nroelectivos")))
    '        dict2.Add("vidiap", CInt(dt.Rows(dt.Rows.Count - 1).Item("idioma_aprob")))
    '        dict2.Add("vnidi", CInt(dt.Rows(dt.Rows.Count - 1).Item("nroidiomas")))
    '        dict2.Add("vpc", CInt(dt.Rows(dt.Rows.Count - 1).Item("preciocalculadocurso")))
    '        If CInt(dt.Rows(dt.Rows.Count - 1).Item("selIdioma")) = 1 Then
    '            dict2.Add("sIdi", True)
    '        Else
    '            dict2.Add("sIdi", False)
    '        End If


    '        If validaEstado(CInt(dt.Rows(dt.Rows.Count - 1).Item("codigo_cur"))) Then
    '            dict2.Add("stt", 1)
    '            If CInt(dt.Rows(dt.Rows.Count - 1).Item("cur_Mat")) = 0 Then
    '                dict2.Add("vCurM", 0)
    '                dict2.Add("chk", False)


    '            Else
    '                dict2.Add("vCurM", 1)
    '                dict2.Add("chk", True)
    '            End If
    '        Else
    '            dict2.Add("stt", 0)

    '            If CInt(dt.Rows(dt.Rows.Count - 1).Item("cur_Mat")) > 0 Then

    '                dict2.Add("chk", True)
    '            Else
    '                dict2.Add("vCurM", 0)
    '                dict2.Add("chk", False)
    '            End If
    '        End If
    '        list.Add(dict2)

    '    Else
    '        'strTbody.Append("")
    '    End If

    '    '#EPENA 02/08/2019 Consultar Observacion del PLan de estudio de los cursos que se listan   { 
    '    Dim olPlanEstudio As New lPlanestudio
    '    Dim oePlanEstudio As New ePlanestudio
    '    Dim dtPes As New Data.DataTable
    '    Dim liPes As String = ""
    '    With oePlanEstudio
    '        .tipooperacion = "1"
    '        .codigo_Pes = Session("codigo_Pes")
    '        .LstCiclos = LstCiclos.ToString
    '    End With

    '    dtPes = olPlanEstudio.ConsultarCursoPlanEstudioObservacion(oePlanEstudio)
    '    If Not dtPes Is Nothing AndAlso dtPes.Rows.Count > 0 Then
    '        For i = 0 To dtPes.Rows.Count - 1
    '            liPes = liPes & "<li>"
    '            liPes = liPes & "Para cursos del ciclo " & dtPes.Rows(i).Item("ciclo_pob").ToString & ": " & dtPes.Rows(i).Item("observacion_pob").ToString
    '            liPes = liPes & "</li>"

    '        Next
    '    End If

    '    ulpeo.InnerHtml = liPes.ToString
    '    '}#EPENA 02/08/2019 Consultar Observacion del PLan de estudio de los cursos que se lista
    '    dt = Nothing
    '    Session.Add("lstCursosDisponiblesJson", list)
    'End Sub

    'Private Sub ListarVerano()
    '    Try

    '        'Dim strTbody As New StringBuilder
    '        Dim list As New List(Of Dictionary(Of String, Object))()
    '        Dim codigo_cup As Integer = 0
    '        Dim i As Integer = 0

    '        Dim CRef As Integer = 0
    '        Dim Nivelado As Boolean = False

    '        CRef = CInt(h6Prom.InnerHtml.ToString)

    '        oeCursoProgramado = New eCursoProgramado
    '        olCursoProgramado = New lCursoProgramado

    '        oeCursoProgramado.codigo_Pes = Session("codigo_Pes")
    '        oeCursoProgramado.codigo_cac = Session("Codigo_Cac")
    '        oeCursoProgramado.codigo_Alu = Session("Codigo_Alu")

    '        dt = olCursoProgramado.consultarCursosHabilesMatricula(oeCursoProgramado)


    '        'Session.Add("lstCursosDisponibles", dt)

    '        'Response.Write("sw " & swIdioma)
    '        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
    '            Nivelado = fnNivelado(dt, CRef)
    '            Me.txttal.Value = Nivelado.ToString
    '            'Me.tAlver.Value = NoNivelado

    '            If Nivelado Then
    '                Me.h6SituacionAlumno.InnerHtml = "Nivelado"
    '            Else
    '                Me.h6SituacionAlumno.InnerHtml = "No Nivelado"
    '            End If

    '            'Response.Write("Nivelado: " & Nivelado)
    '            Dim dtVerano As New Data.DataTable()
    '            dtVerano = dt.Clone
    '            For i = 0 To dt.Rows.Count - 1

    '                If i < (dt.Rows.Count - 1) Then

    '                    Dim rowVerano As Data.DataRow = dtVerano.NewRow()

    '                    If swIdioma And dt.Rows(i).Item("tipocomplementario_cur").ToString = "I" Then
    '                        Continue For
    '                    End If

    '                    Dim dict As New Dictionary(Of String, Object)()
    '                    If dt.Rows(i).Item("codigo_cur") <> dt.Rows(i + 1).Item("codigo_cur") Then

    '                        If Nivelado Then

    '                            If CInt(dt.Rows(i).Item("ciclo_cur")) > CRef Then
    '                                '----------------------------------------------------------------------------------------
    '                                'Response.Write(dt.Rows(i).Item("nombre_Cur") & "<br>")
    '                                rowVerano.ItemArray = dt.Rows(i).ItemArray
    '                                dtVerano.Rows.Add(rowVerano)

    '                                dict.Add("codigo_cur", dt.Rows(i).Item("codigo_cur"))
    '                                dict.Add("nombre_Cur", dt.Rows(i).Item("nombre_Cur").ToString)
    '                                dict.Add("vecesdesaprobada", dt.Rows(i).Item("vecesdesaprobada"))
    '                                dict.Add("electivo_cur", dt.Rows(i).Item("electivo_cur").ToString)
    '                                dict.Add("spc", CInt(dt.Rows(i).Item("soloPrimerCiclo_cup")))

    '                                If dt.Rows(i).Item("cup_Mat").ToString = "0" Then
    '                                    dict.Add("selCurso", "")
    '                                Else
    '                                    dict.Add("selCurso", dt.Rows(i).Item("cup_Mat"))
    '                                End If

    '                                dict.Add("cup_Mat", dt.Rows(i).Item("cup_Mat"))
    '                                dict.Add("cod_Dmat", dt.Rows(i).Item("cod_Dmat"))
    '                                dict.Add("credCurso", dt.Rows(i).Item("creditos_cur"))
    '                                dict.Add("nomCurso", dt.Rows(i).Item("nombre_Cur").ToString.Replace("Ñ", "N"))
    '                                dict.Add("vCred", dt.Rows(i).Item("creditos_cur"))
    '                                dict.Add("vElec", CInt(dt.Rows(i).Item("electivo_cur")))
    '                                dict.Add("vCic", CInt(dt.Rows(i).Item("ciclo_cur")))
    '                                dict.Add("vcur", CInt(dt.Rows(i).Item("codigo_cur")))
    '                                dict.Add("vvec", CInt(dt.Rows(i).Item("vecesdesaprobada")))
    '                                dict.Add("vtc", dt.Rows(i).Item("tipocomplementario_cur").ToString)
    '                                dict.Add("velap", CInt(dt.Rows(i).Item("elec_aprob")))
    '                                dict.Add("vnel", CInt(dt.Rows(i).Item("nroelectivos")))
    '                                dict.Add("vidiap", CInt(dt.Rows(i).Item("idioma_aprob")))
    '                                dict.Add("vnidi", CInt(dt.Rows(i).Item("nroidiomas")))
    '                                dict.Add("vpc", CInt(dt.Rows(i).Item("preciocalculadocurso")))

    '                                If CInt(dt.Rows(i).Item("selIdioma")) = 1 Then
    '                                    dict.Add("sIdi", True)
    '                                Else
    '                                    dict.Add("sIdi", False)
    '                                End If

    '                                If validaEstado(CInt(dt.Rows(i).Item("codigo_cur"))) Then
    '                                    dict.Add("stt", 1)
    '                                    If CInt(dt.Rows(i).Item("cur_Mat")) = 0 Then
    '                                        dict.Add("vCurM", 0)

    '                                        dict.Add("chk", False)

    '                                    Else
    '                                        dict.Add("vCurM", 1)

    '                                        dict.Add("chk", True)
    '                                    End If
    '                                Else
    '                                    dict.Add("stt", 0)

    '                                    If CInt(dt.Rows(i).Item("cur_Mat")) > 0 Then

    '                                        dict.Add("vCurM", 1)
    '                                        dict.Add("chk", True)
    '                                    Else
    '                                        dict.Add("vCurM", 0)
    '                                        dict.Add("chk", False)
    '                                    End If
    '                                End If
    '                                list.Add(dict)
    '                                '----------------------------------------------------------------------------------------



    '                            End If  'If CInt(dt.Rows(i).Item("ciclo_cur")) > CReef Then

    '                        Else


    '                            'NoNivelado
    '                            'Response.Write("b")
    '                            rowVerano.ItemArray = dt.Rows(i).ItemArray
    '                            dtVerano.Rows.Add(rowVerano)
    '                            '----------------------------------------------------------------------------------------
    '                            dict.Add("codigo_cur", dt.Rows(i).Item("codigo_cur"))
    '                            dict.Add("nombre_Cur", dt.Rows(i).Item("nombre_Cur").ToString)
    '                            dict.Add("vecesdesaprobada", dt.Rows(i).Item("vecesdesaprobada"))
    '                            dict.Add("electivo_cur", dt.Rows(i).Item("electivo_cur").ToString)
    '                            dict.Add("spc", CInt(dt.Rows(i).Item("soloPrimerCiclo_cup")))

    '                            If dt.Rows(i).Item("cup_Mat").ToString = "0" Then
    '                                dict.Add("selCurso", "")
    '                            Else
    '                                dict.Add("selCurso", dt.Rows(i).Item("cup_Mat"))
    '                            End If

    '                            dict.Add("cup_Mat", dt.Rows(i).Item("cup_Mat"))
    '                            dict.Add("cod_Dmat", dt.Rows(i).Item("cod_Dmat"))
    '                            dict.Add("credCurso", dt.Rows(i).Item("creditos_cur"))
    '                            dict.Add("nomCurso", dt.Rows(i).Item("nombre_Cur").ToString.Replace("Ñ", "N"))
    '                            dict.Add("vCred", dt.Rows(i).Item("creditos_cur"))
    '                            dict.Add("vElec", CInt(dt.Rows(i).Item("electivo_cur")))
    '                            dict.Add("vCic", CInt(dt.Rows(i).Item("ciclo_cur")))
    '                            dict.Add("vcur", CInt(dt.Rows(i).Item("codigo_cur")))
    '                            dict.Add("vvec", CInt(dt.Rows(i).Item("vecesdesaprobada")))
    '                            dict.Add("vtc", dt.Rows(i).Item("tipocomplementario_cur").ToString)
    '                            dict.Add("velap", CInt(dt.Rows(i).Item("elec_aprob")))
    '                            dict.Add("vnel", CInt(dt.Rows(i).Item("nroelectivos")))
    '                            dict.Add("vidiap", CInt(dt.Rows(i).Item("idioma_aprob")))
    '                            dict.Add("vnidi", CInt(dt.Rows(i).Item("nroidiomas")))
    '                            dict.Add("vpc", CInt(dt.Rows(i).Item("preciocalculadocurso")))

    '                            If CInt(dt.Rows(i).Item("selIdioma")) = 1 Then
    '                                dict.Add("sIdi", True)
    '                            Else
    '                                dict.Add("sIdi", False)
    '                            End If

    '                            If validaEstado(CInt(dt.Rows(i).Item("codigo_cur"))) Then
    '                                dict.Add("stt", 1)
    '                                If CInt(dt.Rows(i).Item("cur_Mat")) = 0 Then
    '                                    dict.Add("vCurM", 0)

    '                                    dict.Add("chk", False)

    '                                Else
    '                                    dict.Add("vCurM", 1)

    '                                    dict.Add("chk", True)
    '                                End If
    '                            Else
    '                                dict.Add("stt", 0)

    '                                If CInt(dt.Rows(i).Item("cur_Mat")) > 0 Then

    '                                    dict.Add("vCurM", 1)
    '                                    dict.Add("chk", True)
    '                                Else
    '                                    dict.Add("vCurM", 0)
    '                                    dict.Add("chk", False)
    '                                End If
    '                            End If
    '                            list.Add(dict)
    '                            '----------------------------------------------------------------------------------------

    '                        End If 'If NoNivelado Then




    '                    End If
    '                End If

    '            Next

    '            If swIdioma And dt.Rows(dt.Rows.Count - 1).Item("tipocomplementario_cur").ToString = "I" Then
    '                dt = Nothing
    '                Session.Add("lstCursosDisponiblesJson", list)
    '                Exit Sub
    '            End If

    '            Dim dict2 As New Dictionary(Of String, Object)()
    '            Dim rowVerano2 As Data.DataRow = dtVerano.NewRow()

    '            If Nivelado Then

    '                If CInt(dt.Rows(dt.Rows.Count - 1).Item("ciclo_cur")) > CRef Then

    '                    rowVerano2.ItemArray = dt.Rows(dt.Rows.Count - 1).ItemArray
    '                    dtVerano.Rows.Add(rowVerano2)
    '                    ' Response.Write(dt.Rows(dt.Rows.Count - 1).Item("nombre_Cur") & "<br>")

    '                    dict2.Add("codigo_cur", dt.Rows(dt.Rows.Count - 1).Item("codigo_cur"))
    '                    dict2.Add("nombre_Cur", dt.Rows(dt.Rows.Count - 1).Item("nombre_Cur").ToString)
    '                    dict2.Add("vecesdesaprobada", dt.Rows(dt.Rows.Count - 1).Item("vecesdesaprobada"))
    '                    dict2.Add("electivo_cur", dt.Rows(dt.Rows.Count - 1).Item("electivo_cur").ToString)
    '                    dict2.Add("spc", CInt(dt.Rows(dt.Rows.Count - 1).Item("soloPrimerCiclo_cup")))
    '                    If dt.Rows(dt.Rows.Count - 1).Item("cup_Mat").ToString = "0" Then
    '                        dict2.Add("selCurso", "")
    '                    Else
    '                        dict2.Add("selCurso", dt.Rows(dt.Rows.Count - 1).Item("cup_Mat"))
    '                    End If
    '                    dict2.Add("cod_Dmat", dt.Rows(dt.Rows.Count - 1).Item("cod_Dmat"))
    '                    dict2.Add("cup_Mat", dt.Rows(dt.Rows.Count - 1).Item("cup_Mat"))
    '                    dict2.Add("credCurso", dt.Rows(dt.Rows.Count - 1).Item("creditos_cur"))
    '                    dict2.Add("nomCurso", dt.Rows(dt.Rows.Count - 1).Item("nombre_Cur").ToString.Replace("Ñ", "N"))
    '                    dict2.Add("vCred", dt.Rows(dt.Rows.Count - 1).Item("creditos_cur"))
    '                    dict2.Add("vElec", CInt(dt.Rows(dt.Rows.Count - 1).Item("electivo_cur")))
    '                    dict2.Add("vCic", CInt(dt.Rows(dt.Rows.Count - 1).Item("ciclo_cur")))
    '                    dict2.Add("vcur", CInt(dt.Rows(dt.Rows.Count - 1).Item("codigo_cur")))
    '                    dict2.Add("vvec", CInt(dt.Rows(dt.Rows.Count - 1).Item("vecesdesaprobada")))
    '                    dict2.Add("vtc", dt.Rows(dt.Rows.Count - 1).Item("tipocomplementario_cur").ToString)
    '                    dict2.Add("velap", CInt(dt.Rows(dt.Rows.Count - 1).Item("elec_aprob")))
    '                    dict2.Add("vnel", CInt(dt.Rows(dt.Rows.Count - 1).Item("nroelectivos")))
    '                    dict2.Add("vidiap", CInt(dt.Rows(dt.Rows.Count - 1).Item("idioma_aprob")))
    '                    dict2.Add("vnidi", CInt(dt.Rows(dt.Rows.Count - 1).Item("nroidiomas")))
    '                    dict2.Add("vpc", CInt(dt.Rows(dt.Rows.Count - 1).Item("preciocalculadocurso")))


    '                    If CInt(dt.Rows(dt.Rows.Count - 1).Item("selIdioma")) = 1 Then
    '                        dict2.Add("sIdi", True)
    '                    Else
    '                        dict2.Add("sIdi", False)
    '                    End If


    '                    If validaEstado(CInt(dt.Rows(dt.Rows.Count - 1).Item("codigo_cur"))) Then
    '                        dict2.Add("stt", 1)
    '                        If CInt(dt.Rows(dt.Rows.Count - 1).Item("cur_Mat")) = 0 Then
    '                            dict2.Add("vCurM", 0)
    '                            dict2.Add("chk", False)

    '                        Else
    '                            dict2.Add("vCurM", 1)
    '                            dict2.Add("chk", True)
    '                        End If
    '                    Else
    '                        dict2.Add("stt", 0)

    '                        If CInt(dt.Rows(dt.Rows.Count - 1).Item("cur_Mat")) > 0 Then

    '                            dict2.Add("chk", True)
    '                        Else
    '                            dict2.Add("vCurM", 0)
    '                            dict2.Add("chk", False)
    '                        End If
    '                    End If

    '                    list.Add(dict2)

    '                End If
    '            Else
    '                If CInt(dt.Rows(dt.Rows.Count - 1).Item("ciclo_cur")) <= CRef Then

    '                    rowVerano2.ItemArray = dt.Rows(dt.Rows.Count - 1).ItemArray
    '                    dtVerano.Rows.Add(rowVerano2)

    '                    dict2.Add("codigo_cur", dt.Rows(dt.Rows.Count - 1).Item("codigo_cur"))
    '                    dict2.Add("nombre_Cur", dt.Rows(dt.Rows.Count - 1).Item("nombre_Cur").ToString)
    '                    dict2.Add("vecesdesaprobada", dt.Rows(dt.Rows.Count - 1).Item("vecesdesaprobada"))
    '                    dict2.Add("electivo_cur", dt.Rows(dt.Rows.Count - 1).Item("electivo_cur").ToString)
    '                    dict2.Add("spc", CInt(dt.Rows(dt.Rows.Count - 1).Item("soloPrimerCiclo_cup")))
    '                    If dt.Rows(dt.Rows.Count - 1).Item("cup_Mat").ToString = "0" Then
    '                        dict2.Add("selCurso", "")
    '                    Else
    '                        dict2.Add("selCurso", dt.Rows(dt.Rows.Count - 1).Item("cup_Mat"))
    '                    End If
    '                    dict2.Add("cod_Dmat", dt.Rows(dt.Rows.Count - 1).Item("cod_Dmat"))
    '                    dict2.Add("cup_Mat", dt.Rows(dt.Rows.Count - 1).Item("cup_Mat"))
    '                    dict2.Add("credCurso", dt.Rows(dt.Rows.Count - 1).Item("creditos_cur"))
    '                    dict2.Add("nomCurso", dt.Rows(dt.Rows.Count - 1).Item("nombre_Cur").ToString.Replace("Ñ", "N"))
    '                    dict2.Add("vCred", dt.Rows(dt.Rows.Count - 1).Item("creditos_cur"))
    '                    dict2.Add("vElec", CInt(dt.Rows(dt.Rows.Count - 1).Item("electivo_cur")))
    '                    dict2.Add("vCic", CInt(dt.Rows(dt.Rows.Count - 1).Item("ciclo_cur")))
    '                    dict2.Add("vcur", CInt(dt.Rows(dt.Rows.Count - 1).Item("codigo_cur")))
    '                    dict2.Add("vvec", CInt(dt.Rows(dt.Rows.Count - 1).Item("vecesdesaprobada")))
    '                    dict2.Add("vtc", dt.Rows(dt.Rows.Count - 1).Item("tipocomplementario_cur").ToString)
    '                    dict2.Add("velap", CInt(dt.Rows(dt.Rows.Count - 1).Item("elec_aprob")))
    '                    dict2.Add("vnel", CInt(dt.Rows(dt.Rows.Count - 1).Item("nroelectivos")))
    '                    dict2.Add("vidiap", CInt(dt.Rows(dt.Rows.Count - 1).Item("idioma_aprob")))
    '                    dict2.Add("vnidi", CInt(dt.Rows(dt.Rows.Count - 1).Item("nroidiomas")))
    '                    dict2.Add("vpc", CInt(dt.Rows(dt.Rows.Count - 1).Item("preciocalculadocurso")))


    '                    If CInt(dt.Rows(dt.Rows.Count - 1).Item("selIdioma")) = 1 Then
    '                        dict2.Add("sIdi", True)
    '                    Else
    '                        dict2.Add("sIdi", False)
    '                    End If


    '                    If validaEstado(CInt(dt.Rows(dt.Rows.Count - 1).Item("codigo_cur"))) Then
    '                        dict2.Add("stt", 1)
    '                        If CInt(dt.Rows(dt.Rows.Count - 1).Item("cur_Mat")) = 0 Then
    '                            dict2.Add("vCurM", 0)
    '                            dict2.Add("chk", False)

    '                        Else
    '                            dict2.Add("vCurM", 1)
    '                            dict2.Add("chk", True)
    '                        End If
    '                    Else
    '                        dict2.Add("stt", 0)

    '                        If CInt(dt.Rows(dt.Rows.Count - 1).Item("cur_Mat")) > 0 Then

    '                            dict2.Add("chk", True)
    '                        Else
    '                            dict2.Add("vCurM", 0)
    '                            dict2.Add("chk", False)
    '                        End If
    '                    End If

    '                    list.Add(dict2)
    '                End If
    '            End If

    '            ' Response.Write(dtVerano.Rows.Count)
    '            Session.Add("lstCursosDisponibles", dtVerano)
    '        Else
    '            'strTbody.Append("")
    '        End If

    '        dt = Nothing
    '        Session.Add("lstCursosDisponiblesJson", list)
    '    Catch ex As Exception
    '        Response.Write(ex.Message & ex.StackTrace)
    '    End Try
    'End Sub

    'Private Function fnNivelado(ByVal dt As DataTable, ByVal cicloRef As Integer) As Boolean
    '    Try
    '        Dim i As Integer = 0
    '        Dim l As Integer = dt.Rows.Count
    '        Dim sw As Boolean = True
    '        Dim cumpleelectivo As Integer = 0
    '        Dim numelectivo As Integer = 0
    '        Dim numcursos As Integer = 0
    '        'Response.Write(dt.Rows.Count)
    '        For i = 0 To l - 1
    '            'Response.Write(dt.Rows(i).Item("nombre_cur") & "<br>")
    '            'Response.Write(dt.Rows(i).Item("grupohor_cup") & "<br>")
    '            If dt.Rows(i).Item("ciclo_cur") <= cicloRef Then

    '                If CBool(dt.Rows(i).Item("electivo_cur")) = True Then
    '                    numelectivo = numelectivo + 1
    '                    If CBool(dt.Rows(i).Item("cumpleElectivo")) = True Then

    '                        cumpleelectivo = cumpleelectivo + 1

    '                    End If
    '                End If

    '                numcursos = numcursos + 1

    '            End If
    '        Next
    '        'Response.Write(numcursos)

    '        If numcursos = 0 Then
    '            sw = True
    '        Else
    '            'Response.Write(numelectivo & "=" & cumpleelectivo)

    '            If numelectivo > 0 Then
    '                If numelectivo <> cumpleelectivo Then
    '                    sw = False
    '                Else
    '                    sw = True
    '                End If
    '            Else
    '                sw = False
    '            End If


    '        End If

    '        'Response.Write(sw)
    '        Return sw
    '    Catch ex As Exception
    '        Response.Write(ex.Message & "--" & ex.StackTrace)
    '        Return False
    '    End Try
    'End Function


    'Private Function validaEstado(ByVal codigo As Integer) As Boolean
    '    Try
    '        Dim dt2 As New DataTable
    '        Dim j As Integer
    '        Dim sw As Integer = 0
    '        dt2 = Session("lstCursosDisponibles")
    '        'Response.Write("CODIGO: " & codigo & "       ")
    '        For j = 0 To dt2.Rows.Count - 1

    '            If CInt(dt2.Rows(j).Item("codigo_cur")) = codigo AndAlso CBool(dt2.Rows(j).Item("estado_cup")) Then
    '                'Response.Write(dt2.Rows(j).Item("nombre_Cur") & "  " & CInt(dt2.Rows(j).Item("codigo_cur")) & "  " & CBool(dt2.Rows(j).Item("estado_cup")))
    '                If CInt(dt2.Rows(j).Item("vacantes_cup")) - CInt(dt2.Rows(j).Item("nromatriculados")) > 0 Then 'EPENA 30/07/2019 Cambia estado del cup si no hay vacantes 
    '                    sw = 1
    '                    Exit For
    '                End If
    '            End If

    '        Next

    '        If sw Then
    '            Return True
    '        Else
    '            Return False
    '        End If



    '    Catch ex As Exception

    '    End Try
    'End Function

    'Protected Sub btnGinc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGinc.Click
    '    Try
    '        Dim validFileTypes As String() = {"bmp", "gif", "png", "jpg", "jpeg", "docx", "xlsx", "pdf"}
    '        Dim isValidFile As Boolean = False
    '        Dim files As HttpFileCollection = Request.Files
    '        Dim postedFile As HttpPostedFile = files(0)

    '        Dim nombreFile As String = postedFile.FileName ' & CStr(Now.ToString)
    '        If postedFile.ContentLength < 500000 Then
    '            Dim ext As String = System.IO.Path.GetExtension(nombreFile)
    '            For i As Integer = 0 To validFileTypes.Length - 1
    '                If ext = "." & validFileTypes(i) Then
    '                    isValidFile = True
    '                    Exit For
    '                End If
    '            Next

    '            If isValidFile Or nombreFile = "" Then
    '                Dim olFunciones As New lFunciones
    '                Dim cod As String
    '                Dim oeIncidencia As New eIncidencia
    '                Dim olIncidencia As New lIncidencia
    '                Dim dt As New DataTable
    '                With oeIncidencia
    '                    .codigo_cac = Session("codigo_Cac")
    '                    .codigo_alu = Session("codigo_Alu")
    '                    .asunto = Me.txtincasunto.Value.ToString
    '                    ' .mensaje = Me.txtincmsje.Value.ToString & "<hr><font style='font-style:italic'>Su solicitud será respondida de manera virtual en el horario de atención de Lunes a Viernes de 08-00 - 17:00 hrs.</font>"
    '                    .mensaje = Me.txtincmsje.Value.ToString & "<hr><font style='font-style:italic'>" & Session("msjeincest").ToString & "</font>"
    '                    .adjunto = nombreFile
    '                End With

    '                If Me.txtincasunto.Value.ToString.Trim <> "" And Me.txtincmsje.Value.ToString.Trim <> "" Then
    '                    dt = olIncidencia.InsertaIncidenteForoAlumno(oeIncidencia)
    '                    'dt = Nothing

    '                    'Dim script As String = "fnRefrescarMat();"
    '                    'ScriptManager.RegisterStartupScript(Me, GetType(Page), "refrescarMat", script, True)

    '                    'Dim script As String = "f_Menu('matricula.aspx')"
    '                    'ScriptManager.RegisterStartupScript(Me, GetType(Page), "reloadmat", script, True)

    '                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
    '                        If dt.Rows(0).Item("ID") > 0 Then
    '                            cod = olFunciones.EncrytedString64(CStr(dt.Rows(0).Item("ID")))

    '                            postedFile.SaveAs(Server.MapPath("filesIncidentes/") & Path.GetFileName(cod & "-" & nombreFile))
    '                            Me.txtincasunto.Value = ""
    '                            Me.txtincmsje.Value = ""
    '                            Me.flArchivo.Dispose()
    '                        End If
    '                    End If

    '                    Session("msjeFileInc") = ""
    '                Else
    '                    Session("msjeFileInc") = "Ingrese Asunto y Mensaje"
    '                End If


    '            Else
    '                Session("msjeFileInc") = "Solo se permiten archivos: jpg,gif,png,jpeg,xlsx,docx,pdf"
    '            End If


    '        Else
    '            Session("msjeFileInc") = "Solo se permiten archivos con tamaño maximo de 500Kb. Ingrese otro archivo"
    '        End If

    '        Session("refrescar") = "Mat"
    '        'Dim script As String = "f_Menu('Main.aspx')"
    '        'ScriptManager.RegisterStartupScript(Me, GetType(Page), "Refrescar", script, True)
    '        Response.Redirect("Main.aspx")

    '    Catch ex As Exception

    '    End Try



    'End Sub



End Class

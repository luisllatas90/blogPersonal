Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web.Security

Imports cEntidad
Imports cLogica
Imports System.Collections.Generic
Imports System.Net

Partial Class Main
    Inherits System.Web.UI.Page
    Private oeMenuAplicacion As eMenuAplicacion
    Private olMenuAplicacion As lMenuAplicacion
    Private lsMenuAplicacion As List(Of eMenuAplicacion)
    Dim crear As Boolean = False

    Private dt As DataTable
    Private oeAlumno As eAlumno
    Private olAlumno As lAlumno

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.Page.User.Identity.IsAuthenticated Then
            FormsAuthentication.RedirectToLoginPage()
        End If

        '#001 INICIO - JR
        Dim tipoAlumni As String
        tipoAlumni = Trim(Session("egresado_Alu"))

        If tipoAlumni = "1" Then
            Response.Redirect("mainegresado.aspx")
        Else
            '#001 FIN - JR
            If Not Session("codigo_Alu") Is Nothing Then
                If Session("refrescar") = "Mat" Then
                    Dim script As String = "f_Menu('matricula.aspx')"
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "reloadmat", script, True)
                End If

                Me.lblCodigoUniversitario.Text = Session("codigoUniver_Alu")
                Me.imgAvatar.ImageUrl = Session("rutaFoto")
                '#002 INICIO - JR
                Me.hdDeudaAlumno.Value = Session("estadodeuda_alu")
                '#002 FIN - JR

                oeMenuAplicacion = New eMenuAplicacion
                olMenuAplicacion = New lMenuAplicacion
                lsMenuAplicacion = New List(Of eMenuAplicacion)
                oeMenuAplicacion.tipoOperacion = "17"
                oeMenuAplicacion.param1 = "8"
                oeMenuAplicacion.param2 = "3"
                oeMenuAplicacion.param3 = Session("codigo_test")
                lsMenuAplicacion = olMenuAplicacion.Listar(oeMenuAplicacion)
                Session.Add("lstMenu", lsMenuAplicacion)
                crear = True

                CargarAnuncios()

                If Not Session("lstMenu") Is Nothing AndAlso crear Then
                    lsMenuAplicacion = Session("lstMenu")
                    crear = True
                End If
                If crear Then
                    If CInt(Session("estadoActual_Alu")) = 1 Then
                        crearMenu(lsMenuAplicacion)
                        'Response.Write("activo")
                    Else
                        crearMenuInactivo(lsMenuAplicacion)
                        'Response.Write("inactivo")
                    End If

                End If

                If Session("codigo_test") = 2 Or Session("codigo_test") = 10 Or Session("codigo_test") = 3 Then

                    'Consultar si está vigente y en cronograma Encuesta Evaluación Docente
                    Dim oe As New eEncuesta
                    Dim ol As New lEncuesta
                    oe.tipo = "E" 'Cambio 29.05.17

                    If ol.EAD_ConsultarEvaluacionVigenteXTipo(oe).Rows.Count Then

                        Session("codigo_cev") = 0 ' Encuesta Vigente
                        Session("codigo_cev") = CInt(ol.EAD_ConsultarEvaluacionVigenteXTipo(oe).Rows(0).Item(0).ToString)
                        CargarEncuesta()
                    End If
                End If


                If Session("codigo_test") = 2 Then
                    If Session("tipo_cac").ToString = "N" Then
                        lblCICLO.InnerHtml = "<b>Semestre Acad&eacute;mico " & Session("descripcion_cac").ToString & "</b>"
                    Else
                        lblCICLO.InnerHtml = "<b>Ciclo verano " & Session("descripcion_cac").ToString & "</b>"
                    End If

                End If
            Else
                FormsAuthentication.RedirectToLoginPage()
            End If
       

            oeMenuAplicacion = Nothing
            olMenuAplicacion = Nothing
            lsMenuAplicacion = Nothing
            End If
        '#001 - JR

        If Session("codigo_test").ToString.Trim = "5" Then
            lnkMail.Attributes.Add("style", "display:none")
        Else
            lnkMail.Attributes.Add("style", "display:block")
        End If


        '#EPENA 08/02/2019 {
        If Session("codigo_cpf").ToString.Trim = "24" Or Session("codigo_cpf").ToString.Trim = "31" Then

            Me.imgResultado.ImageUrl = "~/assets/images/resultados/medicina.png"
        Else

            Me.imgResultado.ImageUrl = "~/assets/images/resultados/regulares.png"
        End If
        '}#08/02/2019

    End Sub
    '#001 - JR
    Private Sub CargarAnuncios()
        Dim strTbody As New StringBuilder
        oeAlumno = New eAlumno
        olAlumno = New lAlumno
        dt = New DataTable

        oeAlumno.param1 = "ALU"
        dt = olAlumno.ConsultarAnuncios(oeAlumno)

        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            'strTbody.Append("<OBJECT id='anuncio' type='text/html' data='" + dt.Rows(0).Item("url_anu").ToString + "' width='100%' height='200%' style='position: relative; overflow: hidden; height: 800px;'></OBJECT>")
            strTbody.Append("<OBJECT id='anuncio' type='text/html' data='" + dt.Rows(0).Item("url_anu").ToString + "' width='100%'  style='position: relative; z-index: 1; overflow: hidden; height: 10000px;'></OBJECT>")
        Else
            strTbody.Append("<center>No existen Anuncios</center>")
        End If
        Me.div_anuncio_bd.InnerHtml = strTbody.ToString
    End Sub
    '#001 - JR
    Private Sub CargarEncuesta()
        Try

        
            Dim strTbody As New StringBuilder
            Dim i As Integer = 0
            Dim j As Integer = 0


            Session("codigo_cup") = 0 ' Curso a encuestar
            Session("codigo_per") = 0 ' Docente del curso
            Session("encPreg") = 0
            Session("obligar") = "0"

            Dim dt As New DataTable
            Dim oe As New eEncuesta
            Dim ol As New lEncuesta

            oe.tipo = "DE"
            oe.codigo_alu = Session("codigo_Alu")
            oe.codigo_cac = Session("Codigo_Cac")
            oe.codigo_cev = Session("codigo_cev")

            'Cargar curso y docente
            dt = ol.EAD_ConsultarCursoEvaluacionYV2(oe)

            If dt.Rows.Count > 0 AndAlso dt.Rows(0).Item("encuestar").ToString = "SI" Then


                Session("obligar") = dt.Rows(0).Item("obligar").ToString

                If dt.Rows(0).Item("foto_Per") = "" Then
                    Me.imgDocente.ImageUrl = "imgpersonal/fotovacia2.png"
                Else
                    Me.imgDocente.ImageUrl = "imgpersonal/" & dt.Rows(0).Item("codigo_Per").ToString & ".jpg"
                End If

                Me.lblNombreDocente.Text = dt.Rows(0).Item("docente").ToString



                'Me.lblNombreDocente.Text = dt.Rows(0).Item("docente").ToString
                Me.lblCursoDocente.Text = dt.Rows(0).Item("nombre_Cur").ToString

                Session("codigo_cup") = dt.Rows(0).Item("codigo_cup").ToString
                Session("codigo_per") = dt.Rows(0).Item("codigo_Per").ToString

                'gvDesempenio.DataSource = dt
                'gvDesempenio.DataBind()

                oe.tipo = "E"
                dt = ol.EAD_ConsultarEvaluacionDesempenio(oe) 'consulta preguntas
                Session("encPreg") = dt.Rows.Count
                Me.np.Value = dt.Rows.Count
                If dt.Rows.Count > 0 Then

                    For i = 0 To dt.Rows.Count - 1
                        If dt.Rows(i).Item("tipopregunta_cev").ToString = "C" Then

                            strTbody.Append("<tr id='trfEnc" & i & "' style='border:1px solid;'>")
                            strTbody.Append("<td><b><center>" & i + 1 & "</center><input type='hidden' id='tord" & i & "' value='" & dt.Rows(i).Item("tipopregunta_cev").ToString & "'></b></td>")
                            strTbody.Append("<td style='text-align:justify'><p>" & dt.Rows(i).Item("pregunta_Eva").ToString & "</p><input type='hidden' id='idk" & i & "' value='" & dt.Rows(i).Item("codigo_eva").ToString & "'/></td>")

                            strTbody.Append("<td><center>")
                            If ((i = 0) Or (i = 21)) Then
                                strTbody.Append("<input type='radio' name='rdb" & i & "' value='1' style='display:block;' onclick='fnSelEnc(" & i & ")'>No</center>")
                            Else
                                strTbody.Append("<input type='radio' name='rdb" & i & "' value='1' style='display:block;' onclick='fnSelEnc(" & i & ")'>1</center>")

                            End If

                            strTbody.Append("</td>")

                            strTbody.Append("<td><center>")
                            If Not ((i = 0) Or (i = 21)) Then
                                strTbody.Append("<input type='radio' name='rdb" & i & "' value='2' style='display:block;' onclick='fnSelEnc(" & i & ")'/>2</center>")
                            End If
                            strTbody.Append("</td>")

                            strTbody.Append("<td><center>")
                            If Not ((i = 0) Or (i = 21)) Then
                                strTbody.Append("<input type='radio' name='rdb" & i & "' value='3' style='display:block;' onclick='fnSelEnc(" & i & ")'/>3</center>")
                            End If
                            strTbody.Append("</td>")

                            strTbody.Append("<td><center>")
                            If Not ((i = 0) Or (i = 21)) Then
                                strTbody.Append("<input type='radio' name='rdb" & i & "' value='4' style='display:block;' onclick='fnSelEnc(" & i & ")'/>4</center>")
                            End If
                            strTbody.Append("</td>")

                            strTbody.Append("<td><center>")
                            If ((i = 0) Or (i = 21)) Then
                                strTbody.Append("<input type='radio' name='rdb" & i & "' value='5' style='display:block;' onclick='fnSelEnc(" & i & ")'/>Sí</center>")
                            Else
                                strTbody.Append("<input type='radio' name='rdb" & i & "' value='5' style='display:block;' onclick='fnSelEnc(" & i & ")'/>5</center>")
                            End If
                            strTbody.Append("</td>")

                            strTbody.Append("</tr>")
                        Else
                            strTbody.Append("<tr id='trfEnc" & i & "' style='border:1px solid;'>")
                            strTbody.Append("<td><b><center>" & i + 1 & "</center><input type='hidden' id='tord" & i & "' value='" & dt.Rows(i).Item("tipopregunta_cev").ToString & "'></b></td>")
                            strTbody.Append("<td style='text-align:justify'><p>" & dt.Rows(i).Item("pregunta_Eva").ToString & "</p><input type='hidden' id='idk" & i & "' value='" & dt.Rows(i).Item("codigo_eva").ToString & "'/></td>")
                            strTbody.Append("<td colspan='5'><center>")
                            strTbody.Append("<textarea id='preg" & i & "'  style='width:100%' onkeyup='fnSelEncText(" & i & ",this)' maxlength='800'></textarea></center>")
                            strTbody.Append("</center></td>")
                            strTbody.Append("</tr>")
                            strTbody.Append("<td><center>")
                        End If

                    Next
                    ' strTbody.Append("<input id=""Text1"" type=""text"" />")


                    Me.tbdPregunta.InnerHtml = strTbody.ToString
                    dt = Nothing
                    oe = Nothing
                    ol = Nothing
                End If


            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub crearMenu(ByVal lista As List(Of eMenuAplicacion))
        Try

            Dim strMenu As New StringBuilder

            strMenu.Append("<ul class='list-unstyled menu-parent' id='mainMenu'>")

            'strMenu.Append("<li><a href='#' class='waves-effect waves-light' ><i class='icon ti-home'></i><span class='text '>Dashboard</span></a></li>")


            If lista.Count > 0 Then

                For Each Fila As eMenuAplicacion In lista
                    'If Lista.IdTrabajador = IdTrabajador Then IdCuentaCorriente = Lista.Id
                    'Exit For
                    'strMenu.Append("<li class='submenu'>")
                    'If esPadre(lista, Fila) = 0 Then

                    '    strMenu.Append("<a class='waves-effect waves-light' href='#" & Fila.codigo_Men & "'><i class='icon ti-layout-grid2'></i><span class='text'> " & Fila.descripcion_Men & "</span><i class='chevron ti-angle-right'></i></a>")


                    'Else


                    'End If


                    ' strMenu.Append("<li class='submenu'>")
                    If NoTienePadre(Fila) Then
                        If TieneHijos(lista, Fila) Then
                            strMenu.Append("<li class='submenu'>")
                            strMenu.Append("<a class='waves-effect waves-light' href='#" & Fila.codigo_Men & "'>")
                            strMenu.Append(" <i class='icon " & Fila.icono_Men & "'></i>")
                            strMenu.Append("<span class='text'> " & Fila.descripcion_Men & "</span>")
                            strMenu.Append("<i class='chevron ti-angle-right'></i></a>")
                            strMenu.Append(LlenarMenu(lista, Fila))
                            strMenu.Append("</li>")
                        Else
                            strMenu.Append("<li>")

                            If Fila.target = "" Then
                                strMenu.Append("<a href='#'  onclick=f_Menu('" & Fila.enlace_men & "')>")
                            Else
                                strMenu.Append("<a href='" & Fila.link & "' target='" & Fila.target & "' )>")
                            End If


                            strMenu.Append("<i class='icon " & Fila.icono_Men & "'></i>")
                            strMenu.Append("<span class='text'> " & Fila.descripcion_Men & "</span>")
                            strMenu.Append("</a>")
                            strMenu.Append("</li>")
                        End If

                    End If
                    ' strMenu.Append("</li>")
                Next

                strMenu.Append("</li>")
                strMenu.Append("</ul>")

                divLeftbar.InnerHtml = strMenu.ToString

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub crearMenuInactivo(ByVal lista As List(Of eMenuAplicacion))
        Try

            Dim strMenu As New StringBuilder

            strMenu.Append("<ul class='list-unstyled menu-parent' id='mainMenu'>")

            'strMenu.Append("<li><a href='#' class='waves-effect waves-light' ><i class='icon ti-home'></i><span class='text '>Dashboard</span></a></li>")


            If lista.Count > 0 Then

                For Each Fila As eMenuAplicacion In lista
                    If Fila.accesoAlumnoInactivo Then
                        If NoTienePadre(Fila) Then
                            If TieneHijos(lista, Fila) Then
                                strMenu.Append("<li class='submenu'>")
                                strMenu.Append("<a class='waves-effect waves-light' href='#" & Fila.codigo_Men & "'>")
                                strMenu.Append(" <i class='icon " & Fila.icono_Men & "'></i>")
                                strMenu.Append("<span class='text'> " & Fila.descripcion_Men & "</span>")
                                strMenu.Append("<i class='chevron ti-angle-right'></i></a>")
                                strMenu.Append(LlenarMenuInactivo(lista, Fila))
                                strMenu.Append("</li>")
                            Else
                                strMenu.Append("<li>")

                                If Fila.target = "" Then
                                    strMenu.Append("<a href='#'  onclick=f_Menu('" & Fila.enlace_men & "')>")
                                Else
                                    strMenu.Append("<a href='" & Fila.link & "' target='" & Fila.target & "' )>")
                                End If


                                strMenu.Append("<i class='icon " & Fila.icono_Men & "'></i>")
                                strMenu.Append("<span class='text'> " & Fila.descripcion_Men & "</span>")
                                strMenu.Append("</a>")
                                strMenu.Append("</li>")
                            End If

                        End If
                    End If
                Next

                strMenu.Append("</li>")
                strMenu.Append("</ul>")

                divLeftbar.InnerHtml = strMenu.ToString

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Function LlenarMenu(ByVal lista As List(Of eMenuAplicacion), ByVal oe As eMenuAplicacion) As String
        Try
            Dim cadMenu As String = ""
            '0: padre
            '1: padre con hijos y tiene padre
            '2: hijo
            'es padre



            If TienePadreHijos(lista, oe) Then


            End If

            If TieneHijos(lista, oe) Then

                cadMenu = cadMenu & "<ul class='list-unstyled' id='" & oe.codigo_Men & "'>"

                cadMenu = cadMenu & ListaHijos(lista, oe)

                cadMenu = cadMenu & "</ul>"



            End If



            Return cadMenu

        Catch ex As Exception
            Throw
        End Try

    End Function
    Private Function LlenarMenuInactivo(ByVal lista As List(Of eMenuAplicacion), ByVal oe As eMenuAplicacion) As String
        Try
            Dim cadMenu As String = ""
            '0: padre
            '1: padre con hijos y tiene padre
            '2: hijo
            'es padre




            If TieneHijos(lista, oe) Then

                If oe.accesoAlumnoInactivo Then
                    cadMenu = cadMenu & "<ul class='list-unstyled' id='" & oe.codigo_Men & "'>"

                    cadMenu = cadMenu & ListaHijosInactivos(lista, oe)

                    cadMenu = cadMenu & "</ul>"

                End If

            End If



            Return cadMenu

        Catch ex As Exception
            Throw
        End Try

    End Function
    Private Function ListaHijos(ByVal lista As List(Of eMenuAplicacion), ByVal oe As eMenuAplicacion) As String
        Try
            Dim cad As String = ""
            For Each Fila As eMenuAplicacion In lista

                If Fila.codigoRaiz_Men = oe.codigo_Men Then
                    If Not TieneHijos(lista, Fila) Then
                        'cad = cad & "<li><a href='main.aspx?modulo=" & Fila.enlace_men.Replace(".aspx", "") & "'>" & Fila.descripcion_Men & "</a></li>"

                        If Fila.target = "" Then
                            'If Fila.enlace_men = "estadocuentaresumen.aspx" And Session("codigo_Alu") = 41954 Then
                            '    cad = cad & "<li><a href='#' onclick=f_Menu('estadocuentaresumen.aspx')>" & Fila.descripcion_Men & "</a></li>"
                            'Else
                            '    If Fila.enlace_men <> "estadocuentaresumen.aspx" And Session("codigo_Alu") <> 41954 Then
                            '        cad = cad & "<li><a href='#' onclick=f_Menu('" & Fila.enlace_men & "')>" & Fila.descripcion_Men & "</a></li>"
                            '    End If
                            'End If
                            cad = cad & "<li><a href='#' onclick=f_Menu('" & Fila.enlace_men & "')>" & Fila.descripcion_Men & "</a></li>"
                        Else
                                cad = cad & "<li><a href='" & Fila.link & "' target='" & Fila.target & "'>" & Fila.descripcion_Men & "</a></li>"
                        End If


                    Else
                        LlenarMenu(lista, Fila)

                    End If

                End If

            Next
            Return cad
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function ListaHijosInactivos(ByVal lista As List(Of eMenuAplicacion), ByVal oe As eMenuAplicacion) As String
        Try
            Dim cad As String = ""
            For Each Fila As eMenuAplicacion In lista

                If Fila.codigoRaiz_Men = oe.codigo_Men Then
                    If Not TieneHijos(lista, Fila) Then
                        'cad = cad & "<li><a href='main.aspx?modulo=" & Fila.enlace_men.Replace(".aspx", "") & "'>" & Fila.descripcion_Men & "</a></li>"
                        If Fila.accesoAlumnoInactivo Then
                            If Fila.target = "" Then
                                'If Fila.enlace_men = "estadocuentaresumen.aspx" And Session("codigo_Alu") = 41954 Then
                                '    cad = cad & "<li><a href='#' onclick=f_Menu('estadocuentaresumen.aspx')>" & Fila.descripcion_Men & "</a></li>"
                                'Else
                                '    If Fila.enlace_men <> "estadocuentaresumen.aspx" And Session("codigo_Alu") <> 41954 Then
                                '        cad = cad & "<li><a href='#' onclick=f_Menu('" & Fila.enlace_men & "')>" & Fila.descripcion_Men & "</a></li>"
                                '    End If
                                'End If

                                cad = cad & "<li><a href='#' onclick=f_Menu('" & Fila.enlace_men & "')>" & Fila.descripcion_Men & "</a></li>"
                            Else
                                    cad = cad & "<li><a href='" & Fila.link & "' target='" & Fila.target & "'>" & Fila.descripcion_Men & "</a></li>"
                            End If
                        End If

                    Else
                        LlenarMenuInactivo(lista, Fila)

                    End If

                End If

            Next
            Return cad
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function ListaPadreHijos(ByVal lista As List(Of eMenuAplicacion), ByVal oe As eMenuAplicacion) As String
        Try
            Dim cad As String = ""
            For Each Fila As eMenuAplicacion In lista

                If Fila.codigoRaiz_Men = oe.codigo_Men Then
                    If TienePadreHijos(lista, Fila) Then
                        'cad = cad & "<li><a href='main.aspx?modulo=" & Fila.enlace_men.Replace(".aspx", "") & "'>" & Fila.descripcion_Men & "</a></li>"
                        cad = cad & "<li><a href='#' onclick=f_Menu('" & Fila.enlace_men & "')>" & Fila.descripcion_Men & "</a></li>"
                    End If
                End If
            Next
            Return cad
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function NoTienePadre(ByVal oe As eMenuAplicacion) As Boolean
        Try
            If oe.codigoRaiz_Men = 0 Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function TieneHijos(ByVal lista As List(Of eMenuAplicacion), ByVal oe As eMenuAplicacion) As Boolean
        Try
            For Each Fila As eMenuAplicacion In lista

                If Fila.codigoRaiz_Men = oe.codigo_Men Then
                    Return True
                End If

            Next
            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function TienePadreHijos(ByVal lista As List(Of eMenuAplicacion), ByVal oe As eMenuAplicacion) As Boolean
        Try
            Dim p As Integer = 0
            Dim h As Integer = 0

            For Each Fila As eMenuAplicacion In lista

                If Fila.codigo_Men = oe.codigoRaiz_Men Then
                    p = 1
                End If
                If Fila.codigoRaiz_Men = oe.codigo_Men Then
                    h = 1
                End If
            Next
            If p = 1 And h = 1 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function

    'Protected Sub gvPreguntas_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPreguntas.RowCreated
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim ctrOpciones As New RadioButtonList
    '        Dim ctrValidar As New RequiredFieldValidator
    '        Dim fila As Data.DataRowView
    '        fila = e.Row.DataItem

    '        e.Row.Cells(2).ColumnSpan = 5
    '        e.Row.Cells(3).Visible = False
    '        e.Row.Cells(4).Visible = False
    '        e.Row.Cells(5).Visible = False
    '        e.Row.Cells(6).Visible = False


    '        If gvPreguntas.DataKeys.Item(e.Row.RowIndex).Values(1) <> "N" Then

    '            Dim NUM As Int16 = 4



    '            For i As Int16 = 0 To NUM
    '                ctrOpciones.Items.Add(" ")
    '                ctrOpciones.RepeatDirection = RepeatDirection.Horizontal


    '                If gvPreguntas.DataKeys.Item(e.Row.RowIndex).Values(2) = "A" Then

    '                    If i < 4 Then
    '                        ctrOpciones.Items.Item(i).Value = i + 1
    '                    Else
    '                        ctrOpciones.Items.Item(i).Value = 0
    '                    End If
    '                Else
    '                    If i < 4 Then
    '                        ctrOpciones.Items.Item(i).Value = NUM + 1
    '                    Else
    '                        ctrOpciones.Items.Item(i).Value = 0
    '                    End If
    '                    NUM -= 1
    '                End If

    '                Select Case i
    '                    Case 0
    '                        ctrOpciones.Items.Item(i).Text = "Nunca"
    '                    Case 1
    '                        ctrOpciones.Items.Item(i).Text = "Rara vez"
    '                    Case 2
    '                        ctrOpciones.Items.Item(i).Text = "Algunas veces"
    '                    Case 3
    '                        ctrOpciones.Items.Item(i).Text = "Casi siempre"
    '                    Case 4
    '                        ctrOpciones.Items.Item(i).Text = "Siempre"


    '                End Select

    '                'ctrOpciones.Items.Item(i).Text = i + 1
    '                'If ctrOpciones.Items.Item(i).Value = 0 Then
    '                '    ctrOpciones.Items.Item(i).Text = "NS/NA"
    '                'End If

    '                'ctrOpciones.Items.Item(i).Attributes.Add("Style", "display:block")
    '                ctrOpciones.Items.Item(i).Attributes.CssStyle.Add("display", "block")
    '                ctrOpciones.Items.Item(i).Attributes.Add("ID", "rdb" & i)

    '            Next
    '            ctrOpciones.ID = "opcion"

    '            'ctrOpciones.Attributes.Remove("style")
    '            'ctrOpciones.Visible = True
    '            'ctrOpciones.Attributes.CssStyle.Add("class", "MOSTRAR")

    '            ctrValidar.ControlToValidate = ctrOpciones.ID
    '            ctrValidar.ValidationGroup = "Guardar"
    '            ctrValidar.ErrorMessage = "Faltan preguntas por responder"
    '            ctrValidar.Text = "*"
    '            ctrValidar.ForeColor = Drawing.Color.Red
    '            e.Row.Cells(2).Controls.Add(ctrOpciones)
    '            e.Row.Cells(2).Controls.Add(ctrValidar)
    '        Else
    '            e.Row.Cells(0).Font.Bold = True
    '            e.Row.Cells(1).Font.Bold = True
    '            e.Row.BackColor = Drawing.Color.AliceBlue
    '        End If
    '    End If
    'End Sub

    'Protected Sub btnGuardarEncuesta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarEncuesta.Click

    '    Try
    '        Dim oe As New eEncuesta
    '        Dim ol As New lEncuesta
    '        Dim tbDatos As New Data.DataTable

    '        Dim codigo_eva, codigo_eed As Int32
    '        Dim codigo_res, i As Int16
    '        Dim codigo_cup As Int32 = Session("codigo_cup")
    '        Dim codigo_cac As Int32 = Session("Codigo_Cac")
    '        Dim codigo_per As Int32 = Session("codigo_per")
    '        Dim conrespuesta_eva As String
    '        Dim lista As New RadioButtonList

    '        oe.codigo_alu = Session("codigo_Alu")
    '        oe.codigo_cup = codigo_cup
    '        oe.codigo_per = codigo_per
    '        oe.codigo_cev = Session("codigo_cev")

    '        codigo_eed = ol.EAD_AgregarEncuestaEvaluacionDD(oe).Rows(0).Item("codigo_eed")

    '        'If codigo_eed > 0 Then

    '        For i = 1 To gvPreguntas.Rows.Count
    '            codigo_eva = gvPreguntas.DataKeys.Item(i - 1).Values(0)
    '            conrespuesta_eva = gvPreguntas.DataKeys.Item(i - 1).Values(1)
    '            'If conrespuesta_eva <> "N" Then
    '            lista = gvPreguntas.Controls(0).Controls(i).Controls(2).Controls(0)
    '            codigo_res = lista.SelectedValue
    '            oe.codigo_eva = codigo_eva
    '            oe.codigo_res = codigo_res
    '            oe.codigo_eed = codigo_eed

    '            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('" & codigo_eva & "-" & codigo_res & "-" & codigo_eed & "' )", True)

    '            ol.EAD_AgregarRespuestaEvaluacion(oe)
    '            'Else
    '            'ClientScript.RegisterStartupScript(Me.GetType, "no habilitado", "alert('Usted ya contestó la evaluación para este curso');", True)
    '            'End If
    '        Next
    '        'Else
    '        'ClientScript.RegisterStartupScript(Me.GetType, "no habilitado", "alert('Usted ya contestó la evaluación para este curso');", True)
    '        'End If
    '    Catch ex As Exception

    '        ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error " & ex.Message & "' )", True)
    '    End Try
    'End Sub






    Protected Sub LoginStatus1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginStatus1.Load


    End Sub
End Class

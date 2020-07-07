Imports iTextSharp.text
Imports iTextSharp.text.html
Imports iTextSharp.text.pdf
Imports iTextSharp.text.xml
Imports iTextSharp.text.html.simpleparser
Imports System.IO
Imports System.util
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Net


Partial Class escolaridad_frmSolEscolaridad
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("pagina") = "../../librerianet/escolaridad/logusat.png"
        Session("paginaPDF") = "../../images/escolaridad/"

        'Session("pagina") = "http://server-test/campusvirtual/images/escolaridad/logusat.png"
        'Session("paginaPDF") = "http://server-test/campusvirtual/images/escolaridad/"

        If Request.QueryString("xdownload") IsNot Nothing Then
            If Request.QueryString("xdownload") = "yes" Then
                descargar()
            End If
        End If

        ':: Seteamos el HTML del formato de la declaración Jurada ::
        datosDeclarante_html.InnerHtml = CargarDatosDeclarante()
        datosDerechoHabientes_html.InnerHtml = CargarListaDerechoHabientes()
        datosPieDeclarante_html.InnerHtml = datosPieDeclarante()
        If Not IsPostBack Then
            ListaDerechoHabientes()
        End If

        '======================================================================================================================'
        '::::: Tener en cuenta: 06.02.2014 ::::
        ':: El problema se presentaba en que al hacer clic en el boton exportar, se perdia el id  ::
        boton.InnerHtml = btn("cv_download.png", "Descargar PDF", "?xdownload=yes&id=" & Request.QueryString("id"), "_self")
        '======================================================================================================================'

    End Sub

    Private Sub ListaDerechoHabientes()
        Try
            Dim obj As New clsEscolaridad
            Dim dts As New Data.DataTable

            dts = obj.ListaDerechoHabientes(Me.Request.QueryString("id"))
            If dts.Rows.Count > 0 Then
                Me.lblnumeroregistros.Text = "Se encontraron ( <b><font color='red'>" & dts.Rows.Count.ToString & "</font></b> ) derechohabientes registrados."
                gvLista.DataSource = dts
                gvLista.DataBind()
            End If
            lblInstrucciones.Text = "<b><font color='#982828'> Instrucciones:</b><br> &nbsp;&nbsp;-Marcar con check los registros a solicitar. <br> &nbsp;&nbsp;-Completar los datos de las columnas Centro de Estudios(tipo y nombre),Grado,Centro Aplicacion y la documentación que va a presentar con la declaración jurada. <br> &nbsp;&nbsp;-Una vez completado los datos, hacer clic en el botón <font color='#20A7F5'>Solicitar Escolaridad</font>. <br> &nbsp;&nbsp;-Finalmente la columna Estado se mostrará <font color='#20A7F5'>Pendiente</font>, hasta su revisión correspondiente."
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLista.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(0).Text = e.Row.RowIndex + 1
                Dim combo As DropDownList = DirectCast(e.Row.FindControl("ddlNivel"), DropDownList)
                combo.ClearSelection()
                If combo IsNot DBNull.Value Then
                    Me.prcCargarComboGridView(combo, "nivel", 0)
                End If

                Dim Cb As CheckBox
                Cb = e.Row.FindControl("chkElegir")
                If (e.Row.Cells(9).Text = "PENDIENTE" Or e.Row.Cells(9).Text = "APROBADO") Then
                    'Cb.Visible = False
                    Cb.Enabled = False
                    ':: Comente el mostrar una imagen, debido a que al hacer un postback, se muestra los check.
                    'e.Row.Cells(1).Text = "<center><img src='../../images/escolaridad/enviado.png' alt='" & "Solicitud Enviada" & "'  style='border: 0px'/></center>"
                Else
                    Cb.Enabled = True
                End If

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Sub prcCargarComboGridView(ByVal cboCombo As DropDownList, ByVal tipo As String, ByVal parametro As Integer)
        Try
            Dim obj As New clsEscolaridad
            Dim dtsN As New Data.DataTable
            Dim dtsG As New Data.DataTable


            If tipo <> "grado" Then
                dtsN = obj.ListarNivelEscolaridad()
                cboCombo.DataSource = dtsN
                cboCombo.DataTextField = "Descripcion_niv"
                cboCombo.DataValueField = "Codigo_niv"
                cboCombo.DataBind()
            Else
                dtsG = obj.ListarGrados(parametro)
                cboCombo.DataSource = dtsG
                cboCombo.DataTextField = "Rango_gra"
                cboCombo.DataValueField = "Codigo_gra"
                cboCombo.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlNivel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim ddl As DropDownList = DirectCast(sender, DropDownList)
            Dim row As GridViewRow = TryCast(ddl.NamingContainer, GridViewRow)
            Dim i As Int16

            'ddlGrado

            If row IsNot Nothing Then
                'Response.Write(CType(row.FindControl("ddlNivel"), DropDownList).SelectedValue)
                Dim combo As DropDownList = DirectCast(row.FindControl("ddlGrado"), DropDownList)
                Dim text As TextBox = DirectCast(row.FindControl("txtCentroEstudios"), TextBox)

                combo.ClearSelection()
                If combo IsNot DBNull.Value Then
                    text.Focus()
                    combo.Enabled = True
                    Me.prcCargarComboGridView(combo, "grado", CType(row.FindControl("ddlNivel"), DropDownList).SelectedValue)
                End If
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnSolicitar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSolicitar.Click
        Try
            Dim obj As New clsEscolaridad
            Dim rpta As Integer = 0

            'Validamos que se haya seleccionado un registro ::::: dguevara :::30.01.2014 :::
            If ValidarSeleccion() = False Then
                Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ud. debe marcar con un check por lo menos un registro para porder hacer la solicitud de escolaridad');", True)
                Exit Sub
            End If

            'Validamos que no tenga estado PENDIENTE o APROBADO
            'Response.Write(ValidarEstadoSolicitud())
            'If ValidarEstadoSolicitud() = False Then
            '    Me.ClientScript.RegisterStartupScript(Me.GetType, "", "alert('No pueden ser enviadas solicitudes PENDIENTES o APROBADAS, favor de verificar.');", True)
            '    Exit Sub
            'End If

            'Validamos los campos obligatorios ::::: dguevara :::30.01.2014 :::
            If ValidarCampo() <> -1 Then
                Select Case ValidarCampo()
                    Case 0
                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ud. debe seleccionar un nivel para la columna centro de estudios de los registros con check.');", True)
                        Exit Sub
                    Case 1
                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ud. debe ingresar el nombre para la columna centro de estudios de los registros con check');", True)
                        Exit Sub
                    Case 2
                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ud. debe registrar la documentación que va adjuntar a su declaración jurada.');", True)
                        Me.txtDocumentos.Focus()
                        Exit Sub
                End Select
            End If

            '============================================'
            '::Ejecutamos el proceso de la solicitud :::
            '============================================'
            Dim fila As GridViewRow
            For i As Integer = 0 To gvLista.Rows.Count - 1
                fila = gvLista.Rows(i)
                Dim valor As Boolean = CType(fila.FindControl("chkElegir"), CheckBox).Checked
                If valor = True Then
                    Dim codigo_dhab As Integer = Me.gvLista.Rows(i).Cells(2).Text
                    Dim codigo_niv As Integer = CType(fila.FindControl("ddlNivel"), DropDownList).SelectedValue
                    Dim txtEstudios As String = CType(fila.FindControl("txtCentroEstudios"), TextBox).Text.ToString.Trim
                    Dim codigo_gra As String = CType(fila.FindControl("ddlGrado"), DropDownList).SelectedValue.ToString
                    Dim chkApli As Boolean = CType(fila.FindControl("chkApli"), CheckBox).Checked
                    '====================================================================================================================================================================
                    rpta = obj.AgregarSolicitud(codigo_dhab, codigo_niv, txtEstudios, CType(codigo_gra, Integer), IIf(chkApli = True, 1, 0), Me.txtDocumentos.Text.Trim.ToString)
                    '====================================================================================================================================================================
                End If

            Next
            'dguevara :: 30.01.2014 
            '':: respuesta al usuario::'
            If rpta > 0 Then
                Me.ClientScript.RegisterStartupScript(Me.GetType, "", "alert('La solicitud fue preocesada correctamente.');", True)
            End If
            Me.ListaDerechoHabientes()
            Me.txtDocumentos.Text = ""
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function ValidarEstadoSolicitud() As Boolean
        Try
            Dim sw As Boolean = True
            Dim fila As GridViewRow

            For i As Integer = 0 To gvLista.Rows.Count - 1
                fila = gvLista.Rows(i)
                Dim valor As Boolean = CType(fila.FindControl("chkElegir"), CheckBox).Checked
                If valor = True Then
                    Dim estado As String = Me.gvLista.Rows(i).Cells(9).Text
                    If estado <> "" Then
                        If (estado.ToString <> "PENDIENTE" Or estado.ToString <> "APROBADO") Then
                            Response.Write(estado)
                            Response.Write("<br>")
                            sw = False
                        End If
                    End If
                End If
            Next

            If sw = True Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Function ValidarCampo() As Integer
        ':: 30.01.2014 :: dguevara ::
        Try
            Dim sw As Boolean = False
            Dim fila As GridViewRow

            For i As Integer = 0 To gvLista.Rows.Count - 1
                fila = gvLista.Rows(i)
                Dim valor As Boolean = CType(fila.FindControl("chkElegir"), CheckBox).Checked
                If valor = True Then
                    Dim codigo_niv As Integer = CType(fila.FindControl("ddlNivel"), DropDownList).SelectedValue
                    Dim txtEstudios As String = CType(fila.FindControl("txtCentroEstudios"), TextBox).Text.ToString.Trim
                    'Dim codigo_gra As String = CType(fila.FindControl("ddlGrado"), DropDownList).SelectedValue.ToString
                    'Dim chkApli As Boolean = CType(fila.FindControl("chkApli"), CheckBox).Checked
                    If codigo_niv = 0 Then
                        Return 0
                    End If
                    If txtEstudios.Length = 0 Then
                        Return 1
                    End If
                    If Me.txtDocumentos.Text.Trim.Length = 0 Then
                        Return 2
                    End If
                End If
            Next
            Return -1
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Function ValidarSeleccion() As Boolean
        Try
            Dim sw As Boolean = False
            Dim fila As GridViewRow

            For i As Integer = 0 To gvLista.Rows.Count - 1
                fila = gvLista.Rows(i)
                Dim valor As Boolean = CType(fila.FindControl("chkElegir"), CheckBox).Checked
                If valor = True Then
                    sw = True
                End If
            Next
            If sw = True Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    '======================================================================================================='
    ':::::::::::::::::::::::::::::::::::::::::::: EXPORTAR A PDF :::::::::::::::::::::::::::::::::::::::::::'
    '======================================================================================================='

    ':::::::: Datos para la declaracion : dguevara 06.02.2014

    Function CargarDatosDeclarante(Optional ByVal xdown As Boolean = False) As String
        Dim ruta As String = Session("pagina")
        Dim datosdeclarante_html As String = ""

        Try

            Dim obj As New clsEscolaridad
            Dim dts As New Data.DataTable
            dts = obj.CargarDatosPersonales(Me.Request.QueryString("id"))

            If dts.Rows.Count > 0 Then

                datosdeclarante_html = CrearTablaInicial()

                datosdeclarante_html &= "<tr>"
                datosdeclarante_html &= "<td align=""center"" colspan=""4"" style=""text-align:center;padding:5px;"">"
                '==============================================================================================================================================================================================================================
                'Comentado:: Considerar la imagen genera error al exportar a PDF. queda pendiente para revisar este tema.
                'datosdeclarante_html &= "<img height=""100"" width=""150"" title=""Declaración Jurada de " & dts.Rows(0).Item("declarante") & """ src=""" & ruta & """ style=""height: 150px; width:150px; text-align:left;"" />"
                '==============================================================================================================================================================================================================================
                datosdeclarante_html &= "<font size=""4"">Universidad Católica Santo Toribio de Mogrovejo</font><br/><font size=""2"">Dirección Personal</font><br/><br/>"
                datosdeclarante_html &= "</td>"
                datosdeclarante_html &= "</tr>"

                'datosdeclarante_html &= TituloDocumento("DECLARACIÓN JURADA")
                datosdeclarante_html &= "<tr><td colspan=""4"" style=""text-align:center;padding:5px;"">DECLARACIÓN JURADA</td></tr>"
                datosdeclarante_html &= "<tr>"
                datosdeclarante_html &= "<td colspan=""4"" style=""text-align:left;padding:5px;"">"
                datosdeclarante_html &= "</td>"
                datosdeclarante_html &= "</tr>"

                datosdeclarante_html &= "<tr>"
                datosdeclarante_html &= "<td colspan=""4"" style=""text-align:justify;padding:5px;"">"
                datosdeclarante_html &= "<font size=""2"">Por intermedio del presente documento. Yo " & dts.Rows(0).Item("declarante") & " "
                datosdeclarante_html &= "Identificado con DNI N° " & dts.Rows(0).Item("nroDocIdentidad_Per") & " "
                datosdeclarante_html &= "con domicilio en " & dts.Rows(0).Item("direccion_Per") & " declaro bajo juramento " & " "
                datosdeclarante_html &= "tener los siguientes hijos en edad escolar o en educación universitaria.<br><br></font>"
                datosdeclarante_html &= "</td>"
                datosdeclarante_html &= "</tr>"

                If dts.Rows(0).Item("filtro") = 0 Then
                    datosdeclarante_html &= "<tr>"
                    datosdeclarante_html &= "<td colspan=""4"" style=""text-align:justify;padding:5px;"">"
                    datosdeclarante_html &= "<font size=""2"">Su tiempo de dedicacion es   <b>" & dts.Rows(0).Item("Descripcion_Ded") & " </b> no le corresponde escolaridad por favor comuniquese con personal </font>"
                    datosdeclarante_html &= "</td>"
                    datosdeclarante_html &= "</tr>"
                End If
                datosdeclarante_html &= CrearTablaFinal()
            End If



        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        Return datosdeclarante_html
    End Function

    Function CargarListaDerechoHabientes(Optional ByVal xdown As Boolean = False) As String
        '-------------------------------------------------------------'
        Dim experienciaLaboral_html As String = ""
        Dim obj As New clsEscolaridad
        Dim dts As New Data.DataTable
        dts = obj.ListarHijos(Me.Request.QueryString("id"))
        '-------------------------------------------------------------'



        Try
            experienciaLaboral_html = CrearTablaInicialDH()
            'experienciaLaboral_html &= CrearFilasHeadDH("<font size=""2""> Apellidos y Nombres <font size=""2"">", "Edad", "Centro Estudios", "Grado de Estudio")
            experienciaLaboral_html &= "<tr>"

            experienciaLaboral_html &= "<td style=""width:50%;"">"
            experienciaLaboral_html &= "<font size=""1""><b>APELLIDOS Y NOMBRES</b></font"
            experienciaLaboral_html &= "</td>"

            experienciaLaboral_html &= "<td align=""center"" style=""width:5%;"">"
            experienciaLaboral_html &= "<font size=""1""><b>EDAD</b><font>"
            experienciaLaboral_html &= "</td>"

            experienciaLaboral_html &= "<td align=""center"" style=""width:36%;"">"
            experienciaLaboral_html &= "<font size=""1""><b>CENTRO DE ESTUDIOS</b><font>"
            experienciaLaboral_html &= "</td>"

            experienciaLaboral_html &= "<td align=""center"" style=""width:10%;"">"
            experienciaLaboral_html &= "<font size=""1""><b>GRADO ESTUDIO</b><font>"
            experienciaLaboral_html &= "</td>"

            experienciaLaboral_html &= "</tr>"

            '-------------------------------------------------------------------------
            'grado_soe
            For i As Integer = 0 To dts.Rows.Count - 1
                experienciaLaboral_html &= CrearFilasDH("<font size=""1"">" & (i + 1).ToString & "- " & dts.Rows(i).Item("apellidosnombres").ToString & "</font>", _
                                                        "<font size=""1"">" & dts.Rows(i).Item("edad").ToString & "</font>", _
                                                        "<font size=""1"">" & dts.Rows(i).Item("centroestudios") & "</font>", _
                                                        "<font size=""1"">" & dts.Rows(i).Item("Rango_gra") & "</font>")
            Next
            '----------------------------------------------------------------------------
            experienciaLaboral_html &= "<br/><br/>"
            experienciaLaboral_html &= CrearTablaFinal()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        Return experienciaLaboral_html
    End Function

    Function datosPieDeclarante(Optional ByVal xdown As Boolean = False) As String
        Dim ruta As String = Session("pagina")
        Dim footerDeclaracionJurada_html As String = ""
        Dim obj As New clsEscolaridad
        Dim dts As New Data.DataTable

        Try

            Dim diames As String = Today.Day.ToString
            If diames.Length = 1 Then
                diames = "0" & diames
            End If

            footerDeclaracionJurada_html = CrearTablaInicial()
            footerDeclaracionJurada_html &= "<br><br>"
            footerDeclaracionJurada_html &= "<tr>"
            footerDeclaracionJurada_html &= "<td colspan=""4"" style=""text-align:justify;padding:5px;"">"
            footerDeclaracionJurada_html &= "<font size=""2""><br/>Para constancia a fin de dar valor a la presente declaración, firmo a las " & System.DateTime.Now.ToString("t").ToString & " del día " & diames & " de " & MonthName(Today.Date.Month).ToString & " de " & Today.Year.ToString & "</font><br/><br/><br/>"
            footerDeclaracionJurada_html &= "</td>"
            footerDeclaracionJurada_html &= "</tr>"

            footerDeclaracionJurada_html &= "<br/><br/>"
            footerDeclaracionJurada_html &= "<tr>"
            footerDeclaracionJurada_html &= "<td align=""center"" colspan=""4"">"
            footerDeclaracionJurada_html &= "FIRMA: _____________________________<br/>"
            footerDeclaracionJurada_html &= "</td>"
            footerDeclaracionJurada_html &= "</tr>"

            '** Saltos de linea  **'
            footerDeclaracionJurada_html &= "<br/><br/>"

            'Documuentos adjuntos:
            dts = obj.ListaDocumentosAdjuntos(Me.Request.QueryString("id"))
            footerDeclaracionJurada_html &= "<tr>"
            footerDeclaracionJurada_html &= "<td align=""left"" colspan=""4"">"
            footerDeclaracionJurada_html &= "<br/><br/>Documentos que Adjunto:"
            footerDeclaracionJurada_html &= "</td>"
            footerDeclaracionJurada_html &= "</tr>"


            If dts.Rows.Count > 0 Then
                For i As Integer = 0 To dts.Rows.Count - 1
                    footerDeclaracionJurada_html &= FilaGeneral(dts.Rows(i).Item("documentosadjuntos_soe").ToString)
                Next
            End If
            footerDeclaracionJurada_html &= CrearTablaFinal()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        Return footerDeclaracionJurada_html
    End Function


    Function FilaGeneral(ByVal dato1 As String) As String
        Dim html As String
        html = "<tr>"
        html &= "<td colspan=""4"" class="""">" & dato1 & "</td>"
        html &= "</tr>"
        Return html
    End Function

    Function TituloDocumento(ByVal dato1 As String) As String
        'format_titulodj

        Dim html As String
        html = "<tr>"
        html &= "<td colspan=""4"" style=""text-align:center;padding:5px;"" class=""format_titulodj"">" & dato1 & "</td>"
        html &= "</tr>"
        Return html
    End Function


    Function btn(ByVal img As String, ByVal text As String, ByVal enlace As String, Optional ByVal target As String = "_blank") As String
        btn = "<div class=""btn_content"">"
        btn &= "<div class=""btn_img"">"
        btn &= "<img src=""" & Session("paginaPDF") & img & """ width=""20""  height=""16"" />"
        btn &= "</div>"
        btn &= "<div class=""btn_title""><a target=""" & target & """ href=""" & enlace & """>" & text & "</a></div>"
        btn &= "<div class=""btn_clear""></div>"
        btn &= "</div>"
    End Function


    Function CrearTablaInicial() As String
        Dim html As String
        html = "<table cellspacing=""0"" cellpadding=""2"" border=""0"" class=""format_tb""> "
        html &= "<tbody>"
        Return html
    End Function

    Function CrearTablaInicialDH() As String
        Dim html As String
        html = "<table cellspacing=""0"" cellpadding=""2"" border=""1"" class=""""> "
        html &= "<tbody>"
        Return html
    End Function


    Function CrearTablaFinal() As String
        Dim html As String
        html = "</tbody>"
        html &= "</table>"
        Return html
    End Function


    'Filas para los Derecho Habientes
    Function CrearFilasHeadDH(ByVal dato1 As String, ByVal dato2 As String, _
                              ByVal dato3 As String, ByVal dato4 As String) As String

        'format_Htd
        Dim html As String
        html = "<tr>"
        html &= "<td style=""width:250px"" >" & dato1 & "</td>"
        html &= "<td align=""center"" style=""width:10px"" >" & dato2 & "</td>"
        html &= "<td style=""width:250px"">" & dato3 & "</td>"
        html &= "<td style=""width:50px"">" & dato4 & "</td>"
        html &= "</tr>"
        Return html
    End Function

    '::: Esta funcion crea las filas para los datos de los hijos...
    Function CrearFilasDH(ByVal dato1 As String, ByVal dato2 As String, ByVal dato3 As String, ByVal dato4 As String) As String
        Dim html As String
        html = "<tr>"
        html &= "<td style=""width:800px"">" & dato1 & "</td>"
        html &= "<td align=""center"" style=""width:10px"">" & dato2 & "</td>"
        html &= "<td align=""left"" style=""width:250px"">" & dato3 & "</td>"
        html &= "<td align=""center"" style=""width:50px"">" & dato4 & "</td>"
        html &= "</tr>"
        Return html
    End Function


    Function CrearFilasHead(ByVal dato1 As String, Optional ByVal classHead As String = "format_Htd") As String
        Dim html As String
        html = "<tr>"
        html &= "<td colspan=""2"" class=""" & classHead & """>" & dato1 & "</td>"
        html &= "</tr>"
        Return html
    End Function


    Function CrearFilas(ByVal dato1 As String, ByVal dato2 As String) As String
        Dim html As String
        html = "<tr>"
        html &= "<td class=""format_td1"">" & dato1 & "</td>"
        html &= "<td class=""format_td2"">" & dato2 & "</td>"
        html &= "</tr>"
        Return html
    End Function

    Function CrearFilas() As String
        Dim html As String
        html = "<tr><td></br></td><td></br></td></tr>"
        Return html
    End Function

    Sub descargar()
        Try
            Dim documento As New Document(PageSize.A4, 85, 85, 67, 50)
            Dim ms As New MemoryStream
            Dim writer As PdfWriter
            writer = PdfWriter.GetInstance(documento, ms)

            Dim textohtml As String = ""
            textohtml &= CargarDatosDeclarante(True)
            textohtml &= CargarListaDerechoHabientes(True)
            textohtml &= datosPieDeclarante(True)

            textohtml = textohtml.Replace("class=""format""", "style=""border: 1px solid #BFBFBF;""")
            textohtml = textohtml.Replace("class=""format_tb""", "style=""width:50%;border-collapse:collapse;font-weight:normal;""")
            textohtml = textohtml.Replace("class=""format_td1_Titulo""", "style="" background-color:white;color:#383636;padding-left:15px;font-weight:bold;font-size:9px; """)
            textohtml = textohtml.Replace("class=""format_td1  format_td2""", "style=""background-color:white;color: #383636;width:27%;padding-left:35px;font-weight:bold;font-size:7px;font-weight:200;text-transform:uppercase""")
            textohtml = textohtml.Replace("class=""format_td1""", "style=""text-align:justify;background-color:white;color: #545252;width:27%;padding-left:35px;font-weight:bold;font-size:8px;""")
            textohtml = textohtml.Replace("class=""format_td2""", "style=""text-align:justify;background-color:white;color: #383636;font-size:7px;font-weight:200;text-transform:uppercase;""")
            textohtml = textohtml.Replace("class=""format_td3""", "style=""font-weight:100; font-size:7px;padding:5px 50px 5px 50px;text-align:center;""")
            textohtml = textohtml.Replace("class=""format_td4""", "style=""font-weight:bold; font-size:8px; color: #383636; padding:5px 50px 5px 50px;  text-align:justify;""")
            textohtml = textohtml.Replace("class=""format_email""", "style=""text-transform:lowercase;color: #383636;font-size:8px;""")
            textohtml = textohtml.Replace("class=""format_Htd""", "style=""font-weight:bold; padding:9px; background-color:#F3F3F3;color:#003366;font-size:9px;text-transform:uppercase;border-bottom:1px solid #003366;""")
            textohtml = textohtml.Replace("class=""format_Htd_Name""", "style=""font-weight:bold;padding:10px; background-color:#F3F3F3;color:#003366;font-size:10px;border-bottom: 1px solid #003366;text-align:center;""")
            textohtml = textohtml.Replace("class=""format_Htd_Enlace""", "style=""font-weight:normal;background-color:white;color:#666464;font-size:9px;text-decoration:none;text-transform:none;""")
            textohtml = textohtml.Replace("class=""btn_content""", "style=""max-width:180px;border: 1px solid #A5A5A4;font-size:12px;cursor:hand;float:left;color:#003366;""")
            textohtml = textohtml.Replace("class=""btn_img""", "style=""float:left;padding:3px;""")
            textohtml = textohtml.Replace("class=""btn_title""", "style=""float:left;color:#003366;padding:2px 4px 2px 0px;text-transform:capitalize;font-weight:normal;""")
            textohtml = textohtml.Replace("class=""btn_clear""", "style=""clear:both;""")
            textohtml = textohtml.Replace("class=""format_update""", "style=""padding:15px; font-size:7px;font-weight:normal;  color: #545252;""")
            'Exit Sub
            '
            '------------------------------------------------------------------------------        
            'PDF
            '------------------------------------------------------------------------------                   
            'Paso a html
            Dim se As New StringReader(textohtml)
            Dim obj As New HTMLWorker(documento)

            'Dim rootPath As String = Server.MapPath("~")
            'Dim customfont As BaseFont
            'customfont = BaseFont.CreateFont(rootPath & "\Egresado\Belwel.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)
            'iTextSharp.text.FontFactory.Register(rootPath & "\Egresado\Belwel.ttf", "Belwe")
            'Dim fontbold As New Font(customfont, 12.5, Font.BOLD)

            'Seteo estilo para html
            Dim style As New StyleSheet
            'style.LoadTagStyle(HtmlTags.P, HtmlTags.SIZE, "12.5pt")
            'style.LoadTagStyle(HtmlTags.P, HtmlTags.FACE, "Belwe")
            'style.LoadTagStyle(HtmlTags.P, HtmlTags.LEADING, "15") 'interlineado
            'style.LoadTagStyle(HtmlTags.STYLE, HtmlTags.LEADING, "15") 'interlineado
            'obj.SetStyleSheet(style)
            documento.Open()
            obj.Parse(se)
            documento.Close()
            Response.Clear()
            'Dim reg As RegularExpressions.Regex
            'Dim textoOriginal As String = Session("nombreEgresado")
            'Dim textoNormalizado As String = textoOriginal.Normalize(NormalizationForm.FormD)

            ' reg = New RegularExpressions.Regex("[^a-zA-Z0-9 ]")
            'Dim nombreEgresadoSinAcentos As String = reg.Replace(textoNormalizado, "")
            Response.AddHeader("content-disposition", "attachment; filename=DeclaracionJuradaEscolaridad" & "" & ".pdf")
            'Response.AddHeader("content-disposition", "attachment; filename=BolsaDeTrabajoAlumniUSATCV.pdf")
            Response.ContentType = "application/pdf"
            Response.Buffer = True
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
            Response.OutputStream.Flush()
            Response.End()
        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

    
End Class

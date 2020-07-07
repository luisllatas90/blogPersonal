
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

Partial Class alumniusat
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("pagina") = "../../librerianet/egresado/"
            If Request.QueryString("xdownload") IsNot Nothing Then
                If Request.QueryString("xdownload") = "yes" Then
                    descargar()
                End If
            End If
            If Request.QueryString("xcod") IsNot Nothing Then
                Session("codigo_alu") = decode(Request.QueryString("xcod"))
                datosPersonales_html.InnerHtml = CargarDatosPersonales()
                experienciaLaboral_html.InnerHtml = CargarExperienciaLaboral()
                gradosAcademicos_html.InnerHtml = CargarGradosAcademicos()
                titulos_html.InnerHtml = CargarTitulos()
                idiomas_html.InnerHtml = CargarIdiomas()
                otros_html.InnerHtml = CargarOtrosEstudios()
                datosadicionales_html.InnerHtml = CargarDatosAdicionales()
                ultima.InnerHtml = Alumni_UltimoUpdate()
            Else
                Response.Redirect("Enlace AlumniUsat Incorrecto")
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " -  " & ex.StackTrace)
            'Response.Redirect("sesion.aspx")
        End Try

    End Sub

    Function CargarDatosPersonales(Optional ByVal xdown As Boolean = False) As String
        Dim ruta As String = Session("pagina")
        Dim datosPersonales_html As String = ""
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tbl As Data.DataTable
        tbl = obj.TraerDataTable("ALUMNI_ConsultarDatosEgresado", CInt(Session("codigo_alu")))
        If tbl.Rows.Count Then
            Session("codigo_PsoCV") = encode(tbl.Rows(0).Item("codigo_Pso"))
            datosPersonales_html = CrearTablaInicial()
            datosPersonales_html &= "<tr>"
            datosPersonales_html &= "<td colspan=""2"" style=""text-align:center;padding:5px;"">"
            Dim fotoEgresado As String = ""
            If tbl.Rows(0).Item("foto_ega") <> "" Then
                fotoEgresado = ("<img height=""100"" width=""90"" title=""Foto de " & tbl.Rows(0).Item("nombreEgresado") & """ src=""" & ruta & "fotos/" & tbl.Rows(0).Item("foto_ega") & """ style=""height: 100px; width:90px; text-align:center;"" />")
            Else
                tbl.Rows(0).Item("foto_ega") = IIf(tbl.Rows(0).Item("sexo_pso").ToString = "F", "female.png", "male.png")
                fotoEgresado = ("<img height=""100"" width=""90"" title=""Foto de " & tbl.Rows(0).Item("nombreEgresado") & """  src=""" & ruta & "fotos/" & tbl.Rows(0).Item("foto_ega") & """ style=""height: 100px; width:90px; text-align:center;"" />")
            End If
            Session("photoEgre") = tbl.Rows(0).Item("foto_ega")
            datosPersonales_html &= fotoEgresado
            datosPersonales_html &= "</td>"
            datosPersonales_html &= "</tr>"
            datosPersonales_html &= CrearFilasHead(tbl.Rows(0).Item("nombreEgresado"), "format_Htd_Name")
            Session("nombreEgresado") = tbl.Rows(0).Item("nombreEgresado").ToString.ToUpper
            datosPersonales_html &= CrearFilasHead(tbl.Rows(0).Item("perfil_ega").ToString.ToUpper, "format_td4")
            datosPersonales_html &= CrearFilasHead((tbl.Rows(0).Item("nombre_pro") & ", " & tbl.Rows(0).Item("nombre_dep")).ToString.ToUpper, "format_td3")
            If Not xdown Then
                datosPersonales_html &= "<tr>"
                datosPersonales_html &= btn("cv_download.png", "Descargar PDF", "?xdownload=yes", "_self")
                datosPersonales_html &= "</td>"
                datosPersonales_html &= "</tr>"
            End If

            datosPersonales_html &= CrearTablaFinal()

            datosPersonales_html &= CrearTablaInicial()
            datosPersonales_html &= CrearFilasHead("<u>DATOS PERSONALES</u>")
            datosPersonales_html &= CrearFilas("N° D.N.I.", tbl.Rows(0).Item("numeroDocIdent_Pso"))
            datosPersonales_html &= CrearFilas("FECHA DE NACIMIENTO", tbl.Rows(0).Item("fechanacimiento_pso"))
            datosPersonales_html &= CrearFilas("EDAD", Edad(tbl.Rows(0).Item("fechanacimiento_pso")) & " AÑOS")
            datosPersonales_html &= CrearFilas("DIRECCIÓN", tbl.Rows(0).Item("direccion_pso").ToString.ToUpper)
            datosPersonales_html &= CrearFilas("TELEFONO", tbl.Rows(0).Item("telefonoFijo_Pso") & IIf(tbl.Rows(0).Item("telefonoCelular_Pso") <> "", " | " & tbl.Rows(0).Item("telefonoCelular_Pso"), "") & IIf(tbl.Rows(0).Item("telefonoCelular2_Pso") <> "", "| " & tbl.Rows(0).Item("telefonoCelular2_Pso"), ""))
            datosPersonales_html &= "<td class=""format_td1"">CORREO ELECTRONICO</td><td class=""format_email"">" & tbl.Rows(0).Item("emailPrincipal_Pso") & IIf(tbl.Rows(0).Item("emailAlternativo_Pso") <> "", " | " & tbl.Rows(0).Item("emailAlternativo_Pso"), "") & "</td>"
            datosPersonales_html &= CrearFilas()
            datosPersonales_html &= CrearTablaFinal()

        Else
            datosPersonales_html = ""
        End If
        tbl.Dispose()
        obj.CerrarConexion()
        obj = Nothing
        Return datosPersonales_html
    End Function
    Function btn(ByVal img As String, ByVal text As String, ByVal enlace As String, Optional ByVal target As String = "_blank") As String
        btn = "<div class=""btn_content"">"
        btn &= "<div class=""btn_img"">"
        btn &= "<img src=""" & Session("pagina") & "archivos/" & img & """ width=""16""  height=""16"" />"
        btn &= "</div>"
        btn &= "<div class=""btn_title""><a target=""" & target & """ href=""" & enlace & """>" & text & "</a></div>"
        btn &= "<div class=""btn_clear""></div>"
        btn &= "</div>"
    End Function
    Function CargarExperienciaLaboral(Optional ByVal xdown As Boolean = False) As String
        Dim experienciaLaboral_html As String = ""
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim TablaData As New Data.DataTable
        TablaData = obj.TraerDataTable("ALUMNI_ConsultarAlumni_ExperienciaLaboral", Session("codigo_alu"))
        obj.CerrarConexion()
        obj = Nothing
        Dim tiempotrabajo As Integer = 0
        Dim txttiempotrabajo As String = ""
     
        If TablaData.Rows.Count Then
            experienciaLaboral_html = CrearTablaInicial()
            experienciaLaboral_html &= CrearFilasHead("<u>EXPERIENCIA LABORAL</u>")
            For i As Integer = 0 To TablaData.Rows.Count - 1
                tiempotrabajo = CInt(TablaData.Rows(i).Item("tiempotrabajo"))
                txttiempotrabajo = IIf(tiempotrabajo \ 12 = 0, tiempotrabajo.ToString & " mes(es) ", (tiempotrabajo \ 12).ToString & " año(s) y " & (tiempotrabajo Mod 12).ToString & " mes(es)")
                experienciaLaboral_html &= CrearFilasHead((i + 1).ToString & ".- " & TablaData.Rows(i).Item("Empresa"), "format_td1_Titulo")
                experienciaLaboral_html &= CrearFilas("CARGO DESEMPEÑADO", TablaData.Rows(i).Item("cargo").ToString.ToUpper)
                experienciaLaboral_html &= CrearFilas("TIPO DE SECTOR", TablaData.Rows(i).Item("Sector"))
                experienciaLaboral_html &= CrearFilas("ÁREA", TablaData.Rows(i).Item("area"))
                experienciaLaboral_html &= CrearFilas("FECHA", TablaData.Rows(i).Item("fecha_inicio") & " - " & IIf(TablaData.Rows(i).Item("fecha_fin") = "-", "Actualidad", TablaData.Rows(i).Item("fecha_fin")))
                experienciaLaboral_html &= CrearFilas("TIEMPO DE TRABAJO", txttiempotrabajo)
                experienciaLaboral_html &= CrearFilas("TIPO DE CONTRATO", TablaData.Rows(i).Item("tipocontrato"))
                experienciaLaboral_html &= CrearFilas("LUGAR DE TRABAJO", TablaData.Rows(i).Item("ciudad"))
                experienciaLaboral_html &= CrearFilas("LOGROS/DESCRIPCION", TablaData.Rows(i).Item("descripción").ToString.ToUpper)
                experienciaLaboral_html &= CrearFilas()
            Next
        experienciaLaboral_html &= CrearTablaFinal()
        End If

        TablaData.Dispose()
        Return experienciaLaboral_html
    End Function
    Function CargarGradosAcademicos(Optional ByVal xdown As Boolean = False) As String
        Dim gradosAcademicos_html As String
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim TablaData1 As New Data.DataTable
        TablaData1 = obj.TraerDataTable("ALUMNI_ConsultarAlumni_FormacionAcademica", Session("codigo_alu"), "G")
        obj.CerrarConexion()
        obj = Nothing
        gradosAcademicos_html = ""
        If TablaData1.Rows.Count Then
            gradosAcademicos_html = CrearTablaInicial()
            gradosAcademicos_html &= CrearFilasHead("<u>GRADOS ACADÉMICOS</u>")
            For i As Integer = 0 To TablaData1.Rows.Count - 1
                gradosAcademicos_html &= CrearFilasHead((i + 1).ToString & ".- " & TablaData1.Rows(i).Item("GradoObtenido"), "format_td1_Titulo")
                gradosAcademicos_html &= CrearFilas("UNIVERSIDAD", TablaData1.Rows(i).Item("Institucion"))
                gradosAcademicos_html &= CrearFilas("SITUACIÓN", TablaData1.Rows(i).Item("SITUACION"))
                gradosAcademicos_html &= CrearFilas("FECHA", TablaData1.Rows(i).Item("aingreso") & IIf(TablaData1.Rows(i).Item("aegreso") = 0, "", " - " & TablaData1.Rows(i).Item("aegreso")))
                gradosAcademicos_html &= CrearFilas("GRADUACIÓN", TablaData1.Rows(i).Item("FechaActo"))

                'If TablaData1.Rows(i).Item("GradoObtenido").ToString.Substring(0, 5) = "BACHI" And TablaData1.Rows(i).Item("codigo_Test") = 2 Then
                '    gradosAcademicos_html &= CrearFilas("TERCIO SUPERIOR", IIf(TablaData1.Rows(i).Item("tercio_ega"), "SI", "NO"))
                '    gradosAcademicos_html &= CrearFilas("QUINTO SUPERIOR", IIf(TablaData1.Rows(i).Item("quinto_ega"), "SI", "NO"))
                'End If

                gradosAcademicos_html &= CrearFilas()
            Next
            gradosAcademicos_html &= CrearTablaFinal()
        End If
        TablaData1.Dispose()
        Return gradosAcademicos_html
    End Function
    Function CargarTitulos(Optional ByVal xdown As Boolean = False) As String
        Dim obj As New ClsConectarDatos
        Dim titulos_html As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim TablaData1 As New Data.DataTable
        TablaData1 = obj.TraerDataTable("ALUMNI_ConsultarAlumni_FormacionAcademica", Session("codigo_alu"), "T")
        obj.CerrarConexion()
        obj = Nothing

        If TablaData1.Rows.Count Then
            titulos_html = CrearTablaInicial()
            titulos_html &= CrearFilasHead("<u>TITULOS PROFESIONALES</u>")
            For i As Integer = 0 To TablaData1.Rows.Count - 1
                titulos_html &= CrearFilasHead((i + 1).ToString & ".- " & TablaData1.Rows(i).Item("GradoObtenido"), "format_td1_Titulo")
                titulos_html &= CrearFilas("UNIVERSIDAD", TablaData1.Rows(i).Item("Institucion"))
                titulos_html &= CrearFilas("SITUACIÓN", TablaData1.Rows(i).Item("SITUACION"))
                titulos_html &= CrearFilas("FECHA", TablaData1.Rows(i).Item("aingreso") & IIf(TablaData1.Rows(i).Item("aegreso") = 0, "", " - " & TablaData1.Rows(i).Item("aegreso")))
                titulos_html &= CrearFilas("TITULACIÓN", TablaData1.Rows(i).Item("FechaActo"))
                titulos_html &= CrearFilas()
            Next
            titulos_html &= CrearTablaFinal()
        End If

        TablaData1.Dispose()
        Return titulos_html
    End Function
    Function CargarIdiomas(Optional ByVal xdown As Boolean = False) As String
        Dim obj As New ClsConectarDatos
        Dim idiomas_html As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim TablaData2 As New Data.DataTable
        TablaData2 = obj.TraerDataTable("ALUMNI_ConsultarAlumni_FormacionAcademica", Session("codigo_alu"), "I")
        obj.CerrarConexion()
        obj = Nothing
        
        If TablaData2.Rows.Count Then
            idiomas_html = CrearTablaInicial()
            idiomas_html &= CrearFilasHead("<u>IDIOMAS</u>")
            For i As Integer = 0 To TablaData2.Rows.Count - 1
                idiomas_html &= CrearFilasHead((i + 1).ToString & ".- " & TablaData2.Rows(i).Item("idioma"), "format_td1_Titulo")
                idiomas_html &= CrearFilas("CENTRO DE ESTUDIOS", TablaData2.Rows(i).Item("CentroEstudios"))
                idiomas_html &= CrearFilas("NIVEL ", " LECTURA: " & TablaData2.Rows(i).Item("NivelLectura") & " | ESCRITURA: " & TablaData2.Rows(i).Item("NivelEscritura") & " | HABLA: " & TablaData2.Rows(i).Item("NivelHabla"))
                idiomas_html &= CrearFilas("TIPO DE INSTITUCIÓN", TablaData2.Rows(i).Item("TipoInstitucion"))
                idiomas_html &= CrearFilas("AÑO", IIf(TablaData2.Rows(i).Item("AnioIngreso") = 0, "-", TablaData2.Rows(i).Item("AnioIngreso")) & IIf(TablaData2.Rows(i).Item("AnioEgreso") = 0, "", "- " & TablaData2.Rows(i).Item("AnioEgreso")))
                idiomas_html &= CrearFilas()
            Next
            idiomas_html &= CrearTablaFinal()
        End If



        TablaData2.Dispose()
        Return idiomas_html
    End Function
    Function CargarOtrosEstudios(Optional ByVal xdown As Boolean = False) As String
        Dim obj As New ClsConectarDatos
        Dim otros_html As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim TablaData2 As New Data.DataTable
        TablaData2 = obj.TraerDataTable("ALUMNI_ConsultarAlumni_OtrosEstudios", Session("codigo_alu")) '3ultimos registros
        obj.CerrarConexion()
        obj = Nothing
        
        If TablaData2.Rows.Count Then
            otros_html = CrearTablaInicial()
            otros_html &= CrearFilasHead("<u>OTROS ESTUDIOS</u>")
            For i As Integer = 0 To TablaData2.Rows.Count - 1
                otros_html &= CrearFilasHead((i + 1).ToString & ".- " & TablaData2.Rows(i).Item("Estudio").ToString.ToUpper, "format_td1_Titulo")
                otros_html &= CrearFilas("CENTRO DE ESTUDIOS", TablaData2.Rows(i).Item("institucion"))
                otros_html &= CrearFilas("TIPO DE INSTITUCIÓN", TablaData2.Rows(i).Item("tipoinst"))
                otros_html &= CrearFilas("AÑO", TablaData2.Rows(i).Item("aingreso") & IIf(TablaData2.Rows(i).Item("aegreso") = 0, "", " - " & TablaData2.Rows(i).Item("aegreso")))
                otros_html &= CrearFilas()
            Next
            otros_html &= CrearTablaFinal()
        End If
        TablaData2.Dispose()
        Return otros_html
    End Function
    Function CargarDatosAdicionales(Optional ByVal xdown As Boolean = False) As String
        Dim obj As New ClsConectarDatos
        Dim otros_html As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt_Egresado As New Data.DataTable
        dt_Egresado = obj.TraerDataTable("ALUMNI_BuscaEgresado", CInt(decode(Session("codigo_PsoCV"))))
        obj.CerrarConexion()
        obj = Nothing
        
        If dt_Egresado.Rows.Count Then
            otros_html = CrearTablaInicial()
            otros_html &= CrearFilasHead("<u>DATOS ADICIONALES</u>")
            For i As Integer = 0 To dt_Egresado.Rows.Count - 1
                otros_html &= CrearFilasHead(IIf(dt_Egresado.Rows(i).Item("otrosestudios_ega").ToString.Trim = "", "-", dt_Egresado.Rows(i).Item("otrosestudios_ega").ToString.ToUpper), "format_td1  format_td2")
                otros_html &= CrearFilas()
            Next            
            otros_html &= CrearTablaFinal()
        End If

        dt_Egresado.Dispose()
        Return otros_html
    End Function
    Function Alumni_UltimoUpdate(Optional ByVal xdown As Boolean = False) As String
        Dim obj As New ClsConectarDatos
        Dim otros_html As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt_Egresado As New Data.DataTable
        dt_Egresado = obj.TraerDataTable("Alumni_UltimoUpdate", CInt(decode(Session("codigo_PsoCV"))))
        obj.CerrarConexion()
        obj = Nothing
        If dt_Egresado.Rows.Count Then
            otros_html = "<table>"
            otros_html &= CrearFilas()
            otros_html &= CrearFilasHead("<i>Última Actualización: " & dt_Egresado.Rows(0).Item("FechaUltima").ToString & "</i>", "format_update")
            otros_html &= "</table>"

        End If
        dt_Egresado.Dispose()
        Return otros_html
    End Function
    Function Edad(ByVal fechaNacimiento As Date) As String
        Do While DateAdd("YYYY", 1, fechaNacimiento) <= Now 'comprobamos los años que ha cumplido						
            Edad = Edad + 1
            fechaNacimiento = DateAdd("YYYY", 1, fechaNacimiento) 'añadiendo años a la fecha de nacimiento
        Loop
    End Function

    Function CrearTablaInicial() As String
        Dim html As String
        html = "<table cellspacing=""0"" cellpadding=""2"" border=""0"" class=""format_tb""> "
        html &= "<tbody>"
        Return html
    End Function

    Function CrearTablaFinal() As String
        Dim html As String
        html = "</tbody>"
        html &= "</table>"
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
    Function encode(ByVal str As String) As String
        Return (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(str)))
    End Function
    Function decode(ByVal str As String) As String
        Return System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(str))
    End Function
    Sub descargar()
        Try
            Dim documento As New Document(PageSize.A4, 85, 85, 67, 50)
            Dim ms As New MemoryStream
            Dim writer As PdfWriter
            writer = PdfWriter.GetInstance(documento, ms)

            Dim textohtml As String = ""
            textohtml &= CargarDatosPersonales(True)
            textohtml &= CargarExperienciaLaboral(True)
            textohtml &= CargarGradosAcademicos(True)
            textohtml &= CargarTitulos(True)
            textohtml &= CargarIdiomas(True)
            textohtml &= CargarOtrosEstudios(True)
            textohtml &= CargarDatosAdicionales(True)
            textohtml &= Alumni_UltimoUpdate()

            'textohtml = textohtml.Replace(Session("pagina") & "fotos/" & Session("photoEgre"), Server.MapPath("~") & "/Egresado/fotos/" & Session("photoEgre"))
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

            Dim rootPath As String = Server.MapPath("~")
            Dim customfont As BaseFont
            customfont = BaseFont.CreateFont(rootPath & "\Egresado\Belwel.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)
            iTextSharp.text.FontFactory.Register(rootPath & "\Egresado\Belwel.ttf", "Belwe")
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
            Dim reg As RegularExpressions.Regex
            Dim textoOriginal As String = Session("nombreEgresado")
            Dim textoNormalizado As String = textoOriginal.Normalize(NormalizationForm.FormD)
            reg = New RegularExpressions.Regex("[^a-zA-Z0-9 ]")
            Dim nombreEgresadoSinAcentos As String = reg.Replace(textoNormalizado, "")
            Response.AddHeader("content-disposition", "attachment; filename=BolsaDeTrabajoAlumniUSAT-CV-" & nombreEgresadoSinAcentos & ".pdf")
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

Imports iTextSharp.text
Imports System.IO
Imports iTextSharp.text.pdf

Partial Class ComunicadoPersonal_frmComunicadoPersonalPDF
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'DATOS
    Dim md_ComunicadoPersonal As New d_ComunicadoPersonal

    'ENTIDADES
    Dim me_ComunicadoPersonal As e_ComunicadoPersonal

    Dim cod_user As Integer = 0
    Dim nombre_personal As String = ""
    Dim tipo_personal As String = ""
    Dim cargo_personal As String = ""
    Dim fecha_entrega As String = ""
    Dim fecha_trabajo As String = ""
#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim cod_user As String = Request("codigo_usu")

            Dim dt As New Data.DataTable : me_ComunicadoPersonal = New e_ComunicadoPersonal
            With me_ComunicadoPersonal
                .operacion = "CP1"
                .codigo_per = cod_user
            End With

            dt = md_ComunicadoPersonal.ListarComunicadoPersonal(me_ComunicadoPersonal)

            Dim codigo_cpe As Integer = 0

            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    nombre_personal = .Item("nombre_personal")
                    cargo_personal = .Item("cargo_personal")
                    tipo_personal = .Item("tipo_cpe")
                    fecha_entrega = .Item("fecha_entrega")
                    fecha_trabajo = .Item("fecha_trabajo")

                    If .Item("indDescarga_cpe").ToString.Trim.Equals("N") Then
                        codigo_cpe = CInt(.Item("codigo_cpe"))
                    End If
                End With

                If codigo_cpe > 0 Then
                    me_ComunicadoPersonal = New e_ComunicadoPersonal

                    With me_ComunicadoPersonal
                        .operacion = "L"
                        .codigo_cpe = codigo_cpe
                        .cod_user = cod_user
                    End With

                    md_ComunicadoPersonal.RegistrarComunicadoPersonal(me_ComunicadoPersonal)
                End If
            End If

            Call GenerarDocumento()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub GenerarDocumento()
        Try
            Dim ms As New MemoryStream
            Dim writer As PdfWriter

            'Dim doc As New Document(PageSize.A4, 75, 85, 67, 50)
            Dim doc As New Document(PageSize.A4, 40, 40, 40, 40)
            'doc.SetMargins(15.0F, 15.0F, 10.0F, 145.0F)

            writer = PdfWriter.GetInstance(doc, ms)

            '20200423 ==============================================================================================
            'Declaracion del tipo de letra
            Dim arial_narrow As iTextSharp.text.pdf.BaseFont = iTextSharp.text.pdf.BaseFont.CreateFont(Server.MapPath(".") & "/arial-narrow.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.EMBEDDED)
            'Dim fo_Letra As Font = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)
            'Dim fo_LetraNegrita As Font = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)
            'Dim fo_LetraNegritaSubrayada As Font = FontFactory.GetFont("Arial", 12, 5, BaseColor.BLACK)
            Dim fo_Letra As iTextSharp.text.Font
            fo_Letra = New iTextSharp.text.Font(arial_narrow, 12, Font.NORMAL, BaseColor.BLACK)
            Dim fo_LetraNegrita As iTextSharp.text.Font
            fo_LetraNegrita = New iTextSharp.text.Font(arial_narrow, 12, Font.BOLD, BaseColor.BLACK)
            Dim fo_LetraNegritaSubrayada As iTextSharp.text.Font
            fo_LetraNegritaSubrayada = New iTextSharp.text.Font(arial_narrow, 12, 5, BaseColor.BLACK)
            '_______________________________________________________________________________________________________

            Dim ls_Linea As Phrase
            Dim ls_Parrafo As Paragraph
            'Dim ls_ParrafoMargen As Paragraph

            doc.Open()
            ls_Parrafo = New Paragraph
            ls_Parrafo.Alignment = Element.ALIGN_JUSTIFIED

            'ls_ParrafoMargen = New Paragraph
            'ls_ParrafoMargen.Alignment = Element.ALIGN_JUSTIFIED
            'ls_ParrafoMargen.SetLeading(3.0F, 0.0F)            

            'ls_Linea = New Phrase("Chiclayo, 22 de abril de 2020" & vbCrLf & vbCrLf, fo_Letra)
            'ls_ParrafoMargen.Add(ls_Linea)
            'doc.Add(ls_ParrafoMargen)

            ls_Linea = New Phrase("Chiclayo, " & fecha_entrega & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("Señor(a)" & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase(nombre_personal & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase(cargo_personal & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("Presente. -" & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("De nuestra mayor consideración, " & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)            

            ls_Linea = New Phrase("Como una medida preventiva a fin de evitar y/o reducir el riesgo de contagio del COVID-19, considerando la naturaleza de los servicios por los cuales fue contratado y respetando lo dispuesto por el Decreto Supremo N° 010-2020-TR, le comunicamos que desde el ", fo_Letra)
            ls_Parrafo.Add(ls_Linea)            

            ls_Linea = New Phrase(fecha_trabajo & " ", fo_LetraNegrita)
            ls_Parrafo.Add(ls_Linea) 'Negrita            

            ls_Linea = New Phrase("usted ha estado ", fo_Letra)
            ls_Parrafo.Add(ls_Linea)            

            ls_Linea = New Phrase("prestando servicios bajo la modalidad del trabajo remoto", fo_LetraNegritaSubrayada)
            ls_Parrafo.Add(ls_Linea) 'Negrita y subrayado            

            ls_Linea = New Phrase(", para lo cual pedimos tomar en consideración la siguiente información:" & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)            

            ls_Linea = New Phrase("1. ", fo_LetraNegrita)
            ls_Parrafo.Add(ls_Linea) 'Negrita

            ls_Linea = New Phrase("PERÍODO DE DURACIÓN:", fo_LetraNegritaSubrayada)
            ls_Parrafo.Add(ls_Linea) 'Negrita y subrayado

            ls_Linea = New Phrase(vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("La prestación de servicios bajo la modalidad del trabajo remoto inició el ", fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase(fecha_trabajo & " ", fo_LetraNegrita)
            ls_Parrafo.Add(ls_Linea) 'Negrita

            ls_Linea = New Phrase("y finalizará de acuerdo a las disposiciones establecidas por el gobierno y a las necesidades de nuestra Institución." & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("2. ", fo_LetraNegrita)
            ls_Parrafo.Add(ls_Linea) 'Negrita

            ls_Linea = New Phrase("LUGAR DE PRESTACIÓN DE SERVICIOS:", fo_LetraNegritaSubrayada)
            ls_Parrafo.Add(ls_Linea) 'Negrita y subrayado

            ls_Linea = New Phrase(vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("El trabajo remoto será realizado por usted en su domicilio o lugar de aislamiento." & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("3. ", fo_LetraNegrita)
            ls_Parrafo.Add(ls_Linea) 'Negrita

            ls_Linea = New Phrase("MEDIO O MECANISMO PARA REALIZAR EL TRABAJO REMOTO:", fo_LetraNegritaSubrayada)
            ls_Parrafo.Add(ls_Linea) 'Negrita y subrayado

            ls_Linea = New Phrase(vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("El trabajo remoto será realizado por usted mediante el empleo de los siguientes medios o mecanismos:" & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            If Not tipo_personal.Equals("A") Then ls_Linea = New Phrase("(*) Acceso a la plataforma Zoom y Moodle." & vbCrLf, fo_Letra) : ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(*) Correo institucional." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(*) Campus virtual del trabajador." & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            If Not tipo_personal.Equals("A") Then ls_Linea = New Phrase("Asimismo, la Universidad Católica Santo Toribio de Mogrovejo le brindará facilidades de acceso a las plataformas virtuales implementadas para el desarrollo de sus funciones. Para tal efecto, usted ha recibido la capacitación correspondiente de manera previa a la implementación del trabajo remoto o el uso de las mismas, a fin de informarle los alcances del empleo y funcionamiento del referido mecanismo." & vbCrLf & vbCrLf, fo_Letra) : ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("4. ", fo_LetraNegrita)
            ls_Parrafo.Add(ls_Linea) 'Negrita

            ls_Linea = New Phrase("OBLIGACIONES Y FUNCIONES DEL TRABAJADOR:", fo_LetraNegritaSubrayada)
            ls_Parrafo.Add(ls_Linea) 'Negrita y subrayado

            ls_Linea = New Phrase(vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            If Not tipo_personal.Equals("A") Then
                ls_Linea = New Phrase("Durante todo el período de implementación del trabajo remoto, usted se encuentra obligado a cumplir con sus obligaciones laborales y tareas encargadas por nuestra Institución, las cuales fueron debidamente recepcionados por su persona y se encuentran en su expediente personal." & vbCrLf & vbCrLf, fo_Letra)
                ls_Parrafo.Add(ls_Linea)
            Else
                ls_Linea = New Phrase("Durante todo el período de implementación del trabajo remoto, usted se encuentra obligado a cumplir con sus deberes laborales y tareas encargadas por nuestra Institución, las cuales fueron debidamente recepcionados por su persona y se encuentran en su expediente personal." & vbCrLf & vbCrLf, fo_Letra)
                ls_Parrafo.Add(ls_Linea)
            End If

            ls_Linea = New Phrase("Sin perjuicio de ello, de manera explicativa y bajo ningún supuesto de modo limitativo, usted debe cumplir con realizar y observar lo siguiente:" & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            If Not tipo_personal.Equals("A") Then
                ls_Linea = New Phrase("(-) Cumplir con el Plan de Trabajo dispuesto por nuestra Institución." & vbCrLf, fo_Letra)
                ls_Parrafo.Add(ls_Linea)

                ls_Linea = New Phrase("(-) Desempeñar la carga académica y administrativa asignada por su Director de Departamento y/o jefe inmediato." & vbCrLf, fo_Letra)
                ls_Parrafo.Add(ls_Linea)
            End If

            ls_Linea = New Phrase("(-) Cumplir con la normativa vigente sobre seguridad de la información, protección y confidencialidad de los datos, y de la información proporcionada por nuestra Institución." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(-) Tomar en consideración las medidas y condiciones de seguridad y salud en el trabajo." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(-) Estar disponible, durante la jornada de trabajo, para las coordinaciones de carácter laboral que resulten necesarias para la ejecución del trabajo remoto y las actividades de nuestra Institución." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(-) Cumplir con las obligaciones indicadas por su jefe inmediato." & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("El incumplimiento de sus obligaciones laborales podrá ser sancionado conforme a lo establecido en la normativa interna de la Institución." & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("5. ", fo_LetraNegrita)
            ls_Parrafo.Add(ls_Linea) 'Negrita

            ls_Linea = New Phrase("MECANISMO DE SUPERVISIÓN Y REPORTE:", fo_LetraNegritaSubrayada)
            ls_Parrafo.Add(ls_Linea) 'Negrita y subrayado

            ls_Linea = New Phrase(vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("Por medio del presente documento, le informamos la implementación de los siguientes mecanismos de supervisión y reporte de las labores realizadas por usted durante el desarrollo del trabajo remoto:" & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(-) Llamadas telefónicas." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(-) Videollamadas." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(-) Correos electrónicos." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(-) Mensajería instantánea (mensajes de texto y WhatsApp)." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            If Not tipo_personal.Equals("A") Then
                ls_Linea = New Phrase("(-) Plataforma Zoom." & vbCrLf, fo_Letra)
                ls_Parrafo.Add(ls_Linea)

                ls_Linea = New Phrase("(-) Plataforma Moodle." & vbCrLf, fo_Letra)
                ls_Parrafo.Add(ls_Linea)
            End If

            ls_Linea = New Phrase(vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)


            If Not tipo_personal.Equals("A") Then
                ls_Linea = New Phrase("6. ", fo_LetraNegrita)
                ls_Parrafo.Add(ls_Linea) 'Negrita

                ls_Linea = New Phrase("CARGA ACADÉMICA:", fo_LetraNegritaSubrayada)
                ls_Parrafo.Add(ls_Linea) 'Negrita y subrayado

                ls_Linea = New Phrase(vbCrLf & vbCrLf, fo_Letra)
                ls_Parrafo.Add(ls_Linea)

                ls_Linea = New Phrase("Sobre el particular, usted desarrollará sus obligaciones laborales de acuerdo al régimen de dedicación bajo el cual se le ha contratado y conforme a la carga académica y administrativa asignada por su Director de Departamento o jefe inmediato." & vbCrLf & vbCrLf, fo_Letra)
                ls_Parrafo.Add(ls_Linea)
            Else
                ls_Linea = New Phrase("6. ", fo_LetraNegrita)
                ls_Parrafo.Add(ls_Linea) 'Negrita

                ls_Linea = New Phrase("JORNADA DE TRABAJO:", fo_LetraNegritaSubrayada)
                ls_Parrafo.Add(ls_Linea) 'Negrita y subrayado

                ls_Linea = New Phrase(vbCrLf & vbCrLf, fo_Letra)
                ls_Parrafo.Add(ls_Linea)

                ls_Linea = New Phrase("Sobre el particular, usted desarrollará sus obligaciones laborales conforme a la jornada laboral que consta en su contrato de trabajo." & vbCrLf & vbCrLf, fo_Letra)
                ls_Parrafo.Add(ls_Linea)
            End If

            ls_Linea = New Phrase("7. ", fo_LetraNegrita)
            ls_Parrafo.Add(ls_Linea) 'Negrita

            ls_Linea = New Phrase("RECOMENDACIONES DE SEGURIDAD Y SALUD EN EL TRABAJO:", fo_LetraNegritaSubrayada)
            ls_Parrafo.Add(ls_Linea) 'Negrita y subrayado

            ls_Linea = New Phrase(vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("Nuestra Institución le proporciona las medidas, condiciones y recomendaciones de seguridad y salud en el trabajo que deberá tomar en consideración durante el desarrollo del trabajo remoto, las mismas que son las siguientes:" & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(*) Respetar y aplicar las medidas de prevención de riesgos y de accidentes de trabajo y de generación o producción de enfermedades derivadas del trabajo o que afecten la salud de los trabajadores informadas a través de los Contratos de Trabajo, Reglamentos, Directivas, Capacitaciones, entre otros documentos proporcionados oportunamente por la Institución." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(*) Realizar pequeñas pausas durante el desarrollo de las funciones es adecuado para prevenir la fatiga física, mental y visual, además del estrés. En tal sentido, se sugiere realizar pausas de diez (10) minutos, por lo menos, cada dos (02) horas de trabajo frente a una computadora." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(*) Adoptar medidas para conciliar la vida familiar y laboral, para lo cual es preciso comunicar a los miembros de la familia sobre tus horarios de trabajo y descanso." & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("Asimismo, de manera enunciativa más no limitativa, deberá tomar en consideración las siguientes medidas y recomendaciones de prevención frente al COVID-19 (así como cualquier otra disposición del Ministerio de Salud o similar):" & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(*) Lavarse las manos frecuentemente, con agua y jabón, por un mínimo de veinte (20) segundos." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(*) Cubrirse la nariz y boca con el antebrazo o pañuelo desechable, al estornudar o toser." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(*) Evitar tocarse las manos, los ojos, la nariz y la boca, con las manos sin lavar." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(*) Evitar el contacto directo con personas con problemas respiratorios." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(*) Ante la presencia de síntomas leves (tos, dolor de garganta y fiebre), cubrirse al toser y lavarse las manos, así como permanecer en casa." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(*) Ante la presencia de síntomas severos (dificultad respiratoria y fiebre alta), comunicarse al 113." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(*) Evitar saludar de mano o beso en la mejilla." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(*) No auto medicarse." & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("(*) Se recomienda mantener limpias las superficies de su lugar de trabajo y su casa (mesa, baños, pisos, juguetes, entre otros), pasando un trapo con desinfectante (por ejemplo, lejía)." & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("Si requiere mayor información o tiene alguna consulta respecto a las recomendaciones señaladas en el presente documento comuníquese al correo electrónico ", fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("msalas@usat.edu.pe", fo_LetraNegritaSubrayada)
            ls_Parrafo.Add(ls_Linea) 'Formato de correo

            ls_Linea = New Phrase("." & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            ls_Linea = New Phrase("Atentamente," & vbCrLf & vbCrLf, fo_Letra)
            ls_Parrafo.Add(ls_Linea)

            'ls_Linea = New Phrase(vbCrLf & vbCrLf & vbCrLf, fo_Letra)
            'ls_Parrafo.Add(ls_Linea)

            '20200423 ==============================================================================================
            Dim firma As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(Server.MapPath(".") & "/firma.png")
            firma.ScalePercent(60.0F)
            firma.Alignment = iTextSharp.text.Element.ALIGN_LEFT
            ls_Parrafo.Add(firma)
            '________________________________________________________________________________________________________

            ls_Linea = New Phrase("Milagros Salas Vargas" & vbCrLf, fo_LetraNegrita)
            ls_Parrafo.Add(ls_Linea) 'Negrita 

            ls_Linea = New Phrase("Directora de Personal", fo_LetraNegrita)
            ls_Parrafo.Add(ls_Linea) 'Negrita 

            doc.Add(ls_Parrafo)

            doc.Close()

            Response.Clear()
            Response.AddHeader("content-disposition", "attachment; filename=CONDICIONES DE TRABAJO REMOTO.pdf")
            Response.ContentType = "application/pdf"
            Response.Buffer = True
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
            Response.OutputStream.Flush()
            Response.End()
        Catch ex As Exception
            
        End Try
    End Sub

#End Region

End Class

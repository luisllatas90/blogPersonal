Imports System.IO
Imports System.Security.Cryptography
Imports System.Collections.Generic
Imports System.Configuration
Imports System


Public Class clsComponenteTramiteVirtualCVE

#Region "variables"
    Private C As ClsConectarDatos
    Private codigo_trl As Integer
    Private codigo_dta As Integer
    Private codigo_dft As Integer
    Private estadoFlujo As String
    Private codigo_per As Integer
    Private estadoAprobacion As String
    Private observacionEvaluacion As String
    Private codigo_tfu As Integer
    Public tipoOperacion As String = "A"
    Private procesoEmailAprobacion As String
    Private procesoEmailRechazo As String
    Private orden_ftr As Integer
    Private descripcion_tfu As String

    Private tieneEmailAprobacion As Integer
    Private tieneEmailRechazo As String
    Private nombre_ctr As String


#End Region

#Region "propiedades"
    Public Property _nombre_ctr() As String
        Get
            Return nombre_ctr
        End Get
        Set(ByVal value As String)
            nombre_ctr = value
        End Set
    End Property

    Public Property _tieneEmailRechazo() As String
        Get
            Return tieneEmailRechazo
        End Get
        Set(ByVal value As String)
            tieneEmailRechazo = value
        End Set
    End Property

    Public Property _tieneEmailAprobacion() As Integer
        Get
            Return tieneEmailAprobacion
        End Get
        Set(ByVal value As Integer)
            tieneEmailAprobacion = value
        End Set
    End Property
    Public Property _descripcion_tfu() As String
        Get
            Return descripcion_tfu
        End Get
        Set(ByVal value As String)
            descripcion_tfu = value
        End Set
    End Property

    Public Property _orden_ftr() As Integer
        Get
            Return orden_ftr
        End Get
        Set(ByVal value As Integer)
            orden_ftr = value
        End Set
    End Property
    Public Property _procesoEmailRechazo() As String
        Get
            Return procesoEmailRechazo
        End Get
        Set(ByVal value As String)
            procesoEmailRechazo = value
        End Set
    End Property

    Public Property _procesoEmailAprobacion() As String
        Get
            Return procesoEmailAprobacion
        End Get
        Set(ByVal value As String)
            procesoEmailAprobacion = value
        End Set
    End Property


    Public Property _codigo_trl() As Integer
        Get
            Return codigo_trl
        End Get
        Set(ByVal value As Integer)
            codigo_trl = value
        End Set
    End Property


    Public Property _codigo_dta() As Integer
        Get
            Return codigo_dta
        End Get
        Set(ByVal value As Integer)
            codigo_dta = value
        End Set
    End Property


    Public Property _codigo_dft() As Integer
        Get
            Return codigo_dft
        End Get
        Set(ByVal value As Integer)
            codigo_dft = value
        End Set
    End Property



    Public Property _estadoFlujo() As String
        Get
            Return estadoFlujo
        End Get
        Set(ByVal value As String)
            estadoFlujo = value
        End Set
    End Property


    Public Property _codigo_per() As Integer
        Get
            Return codigo_per
        End Get
        Set(ByVal value As Integer)
            codigo_per = value
        End Set
    End Property


    Public Property _estadoAprobacion() As String
        Get
            Return estadoAprobacion
        End Get
        Set(ByVal value As String)
            estadoAprobacion = value
        End Set
    End Property

    Public Property _observacionEvaluacion() As String
        Get
            Return observacionEvaluacion
        End Get
        Set(ByVal value As String)
            observacionEvaluacion = value
        End Set
    End Property

    Public Property _codigo_tfu() As Integer
        Get
            Return codigo_tfu
        End Get
        Set(ByVal value As Integer)
            codigo_tfu = value
        End Set
    End Property

#End Region


    Sub New()
        C = New ClsConectarDatos
        C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        estadoFlujo = "" '"F"
        estadoAprobacion = "" '"A"
    End Sub


#Region "Metodos"

    Public Function mt_EvaluarTramite() As List(Of Dictionary(Of String, Object))
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim dict As New Dictionary(Of String, Object)()
        Try

            Dim rptaReq As Integer = 0
            Dim rptaFlujo As Integer = 0
            Dim dt As New Data.DataTable
            Dim dtReq As New Data.DataTable
            Dim dtFlujo As New Data.DataTable
            Dim clsMail As clsComponenteTramiteVirtualEmailCVE

            Dim rEmailAprob As Boolean

            C.AbrirConexion()
            C.IniciarTransaccion()
            dt = C.TraerDataTable("TRL_CMP_datostramite", tipoOperacion, codigo_trl, codigo_dta, codigo_tfu)
            'dict.Add("filas", dt.Rows.Count)
            If dt.Rows.Count = 1 Then
                'dict.Add("tieneRequisito", dt.Rows(0).Item("tieneRequisito"))
                'dict.Add("tieneFlujo", dt.Rows(0).Item("tieneFlujo"))
                If dt.Rows(0).Item("tieneRequisito") Then
                    dtReq = mt_ObtenerRequisitos()

                    If dtReq.Rows.Count > 0 Then
                        For i As Integer = 0 To dtReq.Rows.Count - 1

                            If codigo_tfu = dtReq.Rows(i).Item("codigo_tfu_req") Then
                                rptaReq = C.Ejecutar("TRL_TramiteRequisito_Registrar", "A", codigo_dta, dtReq.Rows(i).Item("codigo_dre"), dtReq.Rows(i).Item("codigo_dft"), 1, "F", codigo_per)

                                If rptaReq = 0 Then
                                    C.AbortarTransaccion()
                                    C.CerrarConexion()
                                    'dict.Add("error1", "Error al evaluar requisito")
                                    list.Add(dict)
                                    Return list
                                End If
                                codigo_dft = dtReq.Rows(i).Item("codigo_dft")

                            End If
                        Next
                    End If
                    'dict.Add("rptaReq", rptaReq)

                    If rptaReq > 0 Then
                        dict.Add("evaluacion", True)
                        dict.Add("registros evaluados", "Se evaluaron " & rptaReq.ToString & " requisitos")
                    End If
                    ' dict.Add("tieneEmailAprobacion", CBool(dt.Rows(0).Item("tieneEmailAprobacion")))
                    If CBool(dt.Rows(0).Item("tieneEmailAprobacion")) Then
                        'dict.Add("EmailAprobacion", "enviando...")
                        rEmailAprob = EnviaCorreo("E")
                        If rEmailAprob Then
                            dict.Add("email", True)
                        Else
                            dict.Add("email", False)
                        End If
                    End If

                End If


                If dt.Rows(0).Item("tieneFlujo") Then
                    'dict.Add("tieneFlujo2", dt.Rows(0).Item("tieneFlujo"))
                    dtFlujo = mt_ObtenerEvaluacionFlujo()
                    ' dict.Add("dtFlujo", dtFlujo.Rows.Count)

                    If dtFlujo.Rows.Count > 0 Then
                        For i As Integer = 0 To dtFlujo.Rows.Count - 1
                            ' dict.Add("if " & i, codigo_tfu.ToString & "=" & dtFlujo.Rows(i).Item("codigo_tfu"))
                            If codigo_tfu = dtFlujo.Rows(i).Item("codigo_tfu") Then

                                rptaFlujo = C.Ejecutar("TRL_DetalleFlujoTramite_Registrar", "A", codigo_dta, dtFlujo.Rows(i).Item("codigo_dft"), "F", codigo_per, estadoAprobacion, observacionEvaluacion)
                                'dict.Add("rptaFlujo", rptaFlujo)

                                If rptaFlujo = 0 Then
                                    C.AbortarTransaccion()
                                    C.CerrarConexion()
                                    'dict.Add("error1", "Error al evaluar flujo")
                                    list.Add(dict)
                                    Return list
                                End If
                                orden_ftr = dtFlujo.Rows(i).Item("orden_ftr")
                                descripcion_tfu = dtFlujo.Rows(i).Item("descripcion_Tfu")
                                tieneEmailAprobacion = dtFlujo.Rows(i).Item("tieneEmailAprobacion")
                                tieneEmailRechazo = dtFlujo.Rows(i).Item("tieneEmailRechazo")
                                procesoEmailAprobacion = dtFlujo.Rows(i).Item("procesoEmailAprobacion")
                                procesoEmailRechazo = dtFlujo.Rows(i).Item("procesoEmailRechazo")
                                nombre_ctr = dtFlujo.Rows(i).Item("descripcion_ctr")
                                'dict.Add("orden_ftr", orden_ftr)
                                'dict.Add("descripcion_tfu", descripcion_tfu)
                                'dict.Add("procesoEmailAprobacion", procesoEmailAprobacion)
                                'dict.Add("procesoEmailRechazo", procesoEmailRechazo)
                            End If
                        Next

                    End If

                    If rptaFlujo > 0 Then
                        dict.Add("evaluacion", True)
                        dict.Add("registos evaluados", "Se evaluaron " & rptaFlujo.ToString & " registros")
                    End If
                    'dict.Add("tieneEmailAprobacion", CBool(tieneEmailRechazo))
                    'dict.Add("tieneEmailRechazo", CBool(tieneEmailAprobacion))
                    If estadoAprobacion = "A" Then
                        If CBool(tieneEmailAprobacion) Then
                            'dict.Add("EmailAprobacion", "enviando...")

                            clsMail = New clsComponenteTramiteVirtualEmailCVE
                            clsMail.codigo_per = codigo_per
                            clsMail.codigo_dta = codigo_dta
                            clsMail.codigo_tfu = codigo_tfu
                            clsMail.orden_ftr = orden_ftr
                            clsMail.descripcion_tfu = descripcion_tfu
                            clsMail.codigo_apl = 64
                            clsMail.nombre_ctr = nombre_ctr
                            clsMail.cin_abreviatura = procesoEmailAprobacion
                            rEmailAprob = clsMail.mt_EnviarCorreoEvaluacionPasos()

                            'rEmailAprob = EnviaCorreo("E")
                            If rEmailAprob Then
                                dict.Add("email", True)
                            Else
                                dict.Add("email", False)
                            End If
                        End If

                    ElseIf estadoAprobacion = "R" Then
                        If CBool(tieneEmailRechazo) Then
                            'dict.Add("EmailRechazo", "enviando...")

                            clsMail = New clsComponenteTramiteVirtualEmailCVE
                            clsMail.codigo_per = codigo_per
                            clsMail.codigo_dta = codigo_dta
                            clsMail.codigo_tfu = codigo_tfu
                            clsMail.orden_ftr = orden_ftr
                            clsMail.descripcion_tfu = descripcion_tfu
                            clsMail.codigo_apl = 64
                            clsMail.nombre_ctr = nombre_ctr
                            clsMail.cin_abreviatura = procesoEmailRechazo
                            rEmailAprob = clsMail.mt_EnviarCorreoEvaluacionPasos()

                            'rEmailAprob = EnviaCorreo("E")
                            If rEmailAprob Then
                                dict.Add("email", True)
                            Else
                                dict.Add("email", False)
                            End If
                        End If

                    End If


                End If

            End If




                C.TerminarTransaccion()
                C.CerrarConexion()
                list.Add(dict)
                Return list

        Catch ex As Exception
            C.AbortarTransaccion()
            C.CerrarConexion()
            'dict.Add("evaluacion", False)
            dict.Add("errorCatch", ex.Message)
            Return list

        End Try

    End Function

    Public Function mt_ObtenerRequisitos() As Data.DataTable
        Try

            Dim dt As New Data.DataTable

            dt = C.TraerDataTable("TRL_TramiteRequisito_Listar", "2", codigo_dta)


            Return dt
        Catch ex As Exception

            Return Nothing
        End Try

    End Function

    Public Function mt_ObtenerEvaluacionFlujo() As Data.DataTable
        Try

            Dim dt As New Data.DataTable

            dt = C.TraerDataTable("TRL_TramiteFlujo_Listar", "2", codigo_dta)

            Return dt
        Catch ex As Exception

            Return Nothing
        End Try

    End Function

    Public Function mt_EnviarCorreoGyT() As Boolean
        Dim cls As New ClsMail

        Dim dt As New Data.DataTable
        Dim strMensaje As String = ""

        ' obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim De, Asunto As String
        Dim EmailDestino As String = ""
        Dim EmailDestino2 As String = ""
        Dim EmailVarios As String = ""
        Dim EmailGYT As String = ""
        Dim GradoAcademico As String = ""

        Dim rpta As Boolean = False
        Dim rptaEmail As String = ""

        Try

            dt = C.TraerDataTable("TRL_DatosGyTxDetalle", codigo_dta)


            If dt.Rows(0).Item("eMail_Alu").ToString <> "" Then
                EmailDestino = dt.Rows(0).Item("eMail_Alu")
                If dt.Rows(0).Item("UserPrincipalName").ToString <> "" Then
                    EmailDestino2 = dt.Rows(0).Item("UserPrincipalName")
                End If
            End If

            If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                EmailGYT = "pdiaz@usat.edu.pe;vtaboada@usat.edu.pe"
                EmailVarios = EmailDestino & ";" & EmailDestino2 & ";" & EmailGYT
            Else
                EmailVarios = "epena@usat.edu.pe;fatima.vasquez@usat.edu.pe"
            End If

            De = "campusvirtual@usat.edu.pe"
            Asunto = "[" & dt.Rows(0).Item("glosaCorrelativo_trl") & "] " & "Trámite Virtual Estudiante: " & dt.Rows(0).Item("descripcion_ctr").ToString

            If dt.Rows(0).Item("descripcion_ctr").ToString.Contains("BACHILLER") Then
                GradoAcademico = "Grado Académico de Bachiller"
            ElseIf dt.Rows(0).Item("descripcion_ctr").ToString.Contains("MAESTRÍA") Or dt.Rows(0).Item("descripcion_ctr").ToString.Contains("TITULO") Then
                GradoAcademico = "Grado Académico de Maestro"
            ElseIf dt.Rows(0).Item("descripcion_ctr").ToString.Contains("DOCTOR") Or dt.Rows(0).Item("descripcion_ctr").ToString.Contains("TITULO") Then
                GradoAcademico = "Grado Académico de Doctor"
            ElseIf dt.Rows(0).Item("descripcion_ctr").ToString.Contains("TÍTULO") Or dt.Rows(0).Item("descripcion_ctr").ToString.Contains("TITULO") Then
                GradoAcademico = "Título Profesional"
            End If

            strMensaje = "<p style='text-align:justify'>"
            strMensaje = strMensaje & "Estimado(a) <b>" & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & "</b>: <br/><br/>"

            strMensaje = strMensaje & "Se les invita a recoger su " & GradoAcademico & " en la Oficina de Grados y Títulos (2do. Piso del edificio de Gobierno), <b> portando su DNI original </b> (Documento Nacional de  Identidad) <b>requisito indispensable</b>. El horario de oficina es de lunes a viernes:"
            strMensaje = strMensaje & "</p>"


            strMensaje = strMensaje & "<p>"
            strMensaje = strMensaje & "<center>"
            strMensaje = strMensaje & "<table style='border:1px solid black; width:50%'>"
            strMensaje = strMensaje & "<tr>"
            strMensaje = strMensaje & "<td style='width:50%'>"
            strMensaje = strMensaje & "Mañana"
            strMensaje = strMensaje & "</td>"
            strMensaje = strMensaje & "<td style='width:50%'>"
            strMensaje = strMensaje & "8:30 am a 1:00 pm"
            strMensaje = strMensaje & "</td>"
            strMensaje = strMensaje & "</tr>"

            strMensaje = strMensaje & "<tr>"
            strMensaje = strMensaje & "<td style='width:50%'>"
            strMensaje = strMensaje & "Tarde"
            strMensaje = strMensaje & "</td>"
            strMensaje = strMensaje & "<td style='width:50%'>"
            strMensaje = strMensaje & "3:00 pm a 4:00 pm"
            strMensaje = strMensaje & "</td>"
            strMensaje = strMensaje & "</tr>"
            strMensaje = strMensaje & "</table>"
            strMensaje = strMensaje & "</center>"
            strMensaje = strMensaje & "</p>"

            strMensaje = strMensaje & "<p style='text-align:justify'>"
            strMensaje = strMensaje & "*<span style='font-weight:bold; text-decoration: underline;'>De Enero a Marzo solo turno mañana</span>"
            strMensaje = strMensaje & "</p>"

            strMensaje = strMensaje & "<b>A TENER EN CUENTA:</b>"
            strMensaje = strMensaje & "<p style='text-align:justify'>"

            strMensaje = strMensaje & "<ul  style='text-align:justify'>"
            strMensaje = strMensaje & "<li>"
            strMensaje = strMensaje & "Indicar en portería que se acercarán a esta oficina a recabar su diploma, por tanto necesitarán su DNI para presentarlo en la Oficina de Grados y Títulos."
            strMensaje = strMensaje & "</li>"
            strMensaje = strMensaje & "</ul>"
            strMensaje = strMensaje & "<ul  style='text-align:justify'>"
            strMensaje = strMensaje & "<li>"
            strMensaje = strMensaje & "La entrega del diploma es personal.<br>"
            strMensaje = strMensaje & "*<b>SOLO para las personas que NO LES SEA POSIBLE recoger su diploma personalmente,<span style='text-decoration: underline;'> descargar el formato de CARTA PODER en el campus</span></b> (Operaciones en línea>>Trámites Virtuales>>Requisitos para trámites) para ser llenada y firmada por su persona, también debe firmarse por el notario del lugar donde actualmente residen (Si reside en el exterior, el documento adjunto lo debe hacer visar por el Consulado del país donde Ud. actualmente reside). La persona que Ud. designe para recoger su diploma deberá presentarse en esta oficina portando la carta <b>original</b> y DNI para la identificación respectiva."
            strMensaje = strMensaje & "</li>"
            strMensaje = strMensaje & "</ul>"
            strMensaje = strMensaje & "<ul  style='text-align:justify'>"
            strMensaje = strMensaje & "<li>"
            strMensaje = strMensaje & "Para visualizar la inscripción de Diploma en la página de SUNEDU, el tiempo establecido es de 55 días hábiles a partir del día siguiente de haberse efectuadola Sesión del Consejo Universitario. (Incluye los días que SUNEDU toma para subir la información)."
            strMensaje = strMensaje & "</li>"
            strMensaje = strMensaje & "</ul>"

            strMensaje = strMensaje & "Gracias,<br>"
            strMensaje = strMensaje & "Oficina de Grados y Títulos"
            strMensaje = strMensaje & "</p>"

            'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto.ToUpper, strMensaje, True)
            rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, Asunto.ToUpper, strMensaje, True)

            Dim codigoEmail As String = ""
            Dim msgEmail As String = ""

            codigoEmail = fnObtenerRespuestaEmail(0, rptaEmail)
            msgEmail = fnObtenerRespuestaEmail(1, rptaEmail)

            If codigoEmail = "1" Then
                rpta = True
            Else
                rpta = False
            End If

            'RegistroBitacoraCorreo(De, EmailVarios, Asunto, strMensaje, rpta, dft, idta, codigoEmail, msgEmail)

            Return rpta
        Catch ex As Exception

            Return False
        End Try
    End Function

    Private Function EnviaCorreo(ByVal tipo As String) As Boolean
        Dim cls As New ClsMail

        Dim dt As New Data.DataTable
        Dim strMensaje As String = ""

        Dim De, Asunto As String
        Dim EmailDestino As String = ""
        Dim EmailDestino2 As String = ""
        Dim EmailVarios As String = ""

        Dim rpta As Boolean = False
        Dim rptaEmail As String = ""

        Try

            dt = C.TraerDataTable("TRL_DatosAlumnoxDetalle", codigo_dta)

            'If (dt.Rows.Count > 0) Then
            If dt.Rows(0).Item("eMail_Alu").ToString <> "" Then
                EmailDestino = dt.Rows(0).Item("eMail_Alu")

                If dt.Rows(0).Item("UserPrincipalName").ToString <> "" Then
                    EmailDestino2 = dt.Rows(0).Item("UserPrincipalName")
                    'EmailVarios = EmailDestino2			
                End If
            End If

            If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                EmailVarios = EmailDestino & ";" & EmailDestino2
            Else
                EmailVarios = "epena@usat.edu.pe;fatima.vasquez@usat.edu.pe"
            End If

            De = ""
            Asunto = ""

            'Correo de Fecha de Entrega
            If tipo = "F" Then
                De = "campusvirtual@usat.edu.pe"
                Asunto = "[" & dt.Rows(0).Item("glosaCorrelativo_trl") & "] " & "Fecha de Entrega Trámite"

                strMensaje = "Estimado(a) " & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & ": <br/><br/>"
                strMensaje = strMensaje & "El trámite " & dt.Rows(0).Item("glosaCorrelativo_trl")
                strMensaje = strMensaje & " ha sido actualizada la fecha de entrega para el día "
                strMensaje = strMensaje & dt.Rows(0).Item("fechaFin_dta").ToString & ".<br/><br/>"
                strMensaje = strMensaje & "<em>" & dt.Rows(0).Item("observacionAlumno_dft").ToString & "</em>"
                ' cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailVarios, "Fecha de Entrega Trámite", strMensaje, True, "", "")
                'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
                rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, Asunto, strMensaje, True)
            End If
            'Correo de Mensaje de Email
            If tipo = "C" Then
                De = "campusvirtual@usat.edu.pe"
                Asunto = "[" & dt.Rows(0).Item("glosaCorrelativo_trl") & "] " & "Mensaje de Trámite"

                strMensaje = "Estimado(a) " & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & ": <br/><br/>"
                strMensaje = strMensaje & "El trámite " & dt.Rows(0).Item("glosaCorrelativo_trl")
                strMensaje = strMensaje & " ha enviado el siguiente mensaje:  <br/><br/>"
                strMensaje = strMensaje & "<em>" & dt.Rows(0).Item("observacionAlumno_dft").ToString & "</em>"
                ' cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailVarios, "Fecha de Entrega Trámite", strMensaje, True, "", "")
                'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
                rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, Asunto, strMensaje, True)
            End If
            'Correo de Finaliza Tramite
            If tipo = "T" Then
                De = "campusvirtual@usat.edu.pe"
                Asunto = "[" & dt.Rows(0).Item("glosaCorrelativo_trl") & "] " & "Trámite Finalizado"

                strMensaje = "Estimado(a) " & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & ": <br/><br/>"
                strMensaje = strMensaje & "El documento solicitado <b>" & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper() & "</b> ya se encuentra disponible.<br/><br/>"
                strMensaje = strMensaje & "Puedes recoger tu documento en: <br/>"
                strMensaje = strMensaje & "<em>" & dt.Rows(0).Item("ubicacion_ctr") & " - " & dt.Rows(0).Item("observacionAlumno_dft").ToString & "</em><br/>"
                'cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailVarios, "Entrega Trámite", strMensaje, True, "", "")
                'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
                rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, "Trámite Finalizado", strMensaje, True)
            End If

            'Correo de Entrega Tramite
            If tipo = "E" Then
                De = "campusvirtual@usat.edu.pe"
                Asunto = "[" & dt.Rows(0).Item("glosaCorrelativo_trl") & "] " & "Entrega Trámite"

                strMensaje = "Estimado(a) " & dt.Rows(0).Item("nombres_Alu") & " " & dt.Rows(0).Item("apellidoPat_Alu") & " " & dt.Rows(0).Item("apellidoMat_Alu") & ": <br/><br/>"
                strMensaje = strMensaje & "El documento solicitado <b>" & dt.Rows(0).Item("descripcion_ctr").ToString.ToUpper() & "</b> ya ha sido entregado.<br/><br/>"
                'cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailVarios, "Entrega Trámite", strMensaje, True, "", "")
                'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
                rptaEmail = cls.EnviarMailVariosV3(De, EmailVarios, Asunto, strMensaje, True)
            End If
            '  End If
            '  End If

            Dim codigoEmail As String = ""
            Dim msgEmail As String = ""

            codigoEmail = fnObtenerRespuestaEmail(0, rptaEmail)
            msgEmail = fnObtenerRespuestaEmail(1, rptaEmail)

            If codigoEmail = "1" Then
                rpta = True
            Else
                rpta = False
            End If

            ' RegistroBitacoraCorreo(De, EmailVarios, Asunto, strMensaje, rpta, dft, dta, codigoEmail, msgEmail)

            cls = Nothing


            Return rpta

        Catch ex As Exception

            Return False
        End Try
    End Function

    'Public Function mt_Evaluar() As Object
    '    Try

    '        Dim rpta As Object
    '        C.AbrirConexion()
    '        rpta = C.Ejecutar("TRL_DetalleFlujoTramite_Registrar", tipoOperacion, codigo_dta, codigo_dft, estadoFlujo, codigo_per, estadoAprobacion, observacionEvaluacion)
    '        C.CerrarConexion()

    '        Return rpta
    '    Catch ex As Exception

    '        Return Nothing
    '    End Try

    'End Function

#End Region
#Region "Funciones"
    Private Function fnObtenerRespuestaEmail(ByVal tipo As Int16, ByVal respuesta As String) As String
        Try
            Dim Respuestas() As String
            Respuestas = respuesta.Split(",")  'Split(respuesta, ",")
            Return Respuestas(tipo).ToString


        Catch ex As Exception
            Return ""
        End Try
    End Function
#End Region
End Class


Public Class clsComponenteTramiteVirtualEmailCVE

#Region "variables"
    Private C As ClsConectarDatos
    Private _codigo_trl As Integer
    Private _codigo_alu As Integer
    Private _codigo_ctr As Integer
    Private _codigo_dta As Integer
    Private _codigo_dft As Integer
    Private _codigo_tfu As Integer
    Private _codigo_per As Integer
    Private _codigo_apl As Integer
    Private _estadoAprobacion As String
    Private _cin_abreviatura As String
    Private _orden_ftr As Integer
    Private _descripcion_tfu As String


    Private _nombre_ctr As String



#End Region

#Region "propiedades"

    Public Property nombre_ctr() As String
        Get
            Return _nombre_ctr
        End Get
        Set(ByVal value As String)
            _nombre_ctr = value
        End Set
    End Property
    Public Property descripcion_tfu() As String
        Get
            Return _descripcion_tfu
        End Get
        Set(ByVal value As String)
            _descripcion_tfu = value
        End Set
    End Property
    Public Property orden_ftr() As Integer
        Get
            Return _orden_ftr
        End Get
        Set(ByVal value As Integer)
            _orden_ftr = value
        End Set
    End Property
    Public Property cin_abreviatura() As String
        Get
            Return _cin_abreviatura
        End Get
        Set(ByVal value As String)
            _cin_abreviatura = value
        End Set
    End Property
    Public Property estadoAprobacion() As String
        Get
            Return _estadoAprobacion
        End Get
        Set(ByVal value As String)
            _estadoAprobacion = value
        End Set
    End Property
    Public Property codigo_apl() As Integer
        Get
            Return _codigo_apl
        End Get
        Set(ByVal value As Integer)
            _codigo_apl = value
        End Set
    End Property
    Public Property codigo_per() As Integer
        Get
            Return _codigo_per
        End Get
        Set(ByVal value As Integer)
            _codigo_per = value
        End Set
    End Property
    Public Property codigo_tfu() As Integer
        Get
            Return _codigo_tfu
        End Get
        Set(ByVal value As Integer)
            _codigo_tfu = value
        End Set
    End Property
    Public Property codigo_dft() As Integer
        Get
            Return _codigo_dft
        End Get
        Set(ByVal value As Integer)
            _codigo_dft = value
        End Set
    End Property
    Public Property codigo_dta() As Integer
        Get
            Return _codigo_dta
        End Get
        Set(ByVal value As Integer)
            _codigo_dta = value
        End Set
    End Property
    Public Property codigo_ctr() As Integer
        Get
            Return _codigo_ctr
        End Get
        Set(ByVal value As Integer)
            _codigo_ctr = value
        End Set
    End Property
    Public Property codigo_alu() As Integer
        Get
            Return _codigo_alu
        End Get
        Set(ByVal value As Integer)
            _codigo_alu = value
        End Set
    End Property
    Public Property codigo_trl() As Integer
        Get
            Return _codigo_trl
        End Get
        Set(ByVal value As Integer)
            _codigo_trl = value
        End Set
    End Property

#End Region

    Sub New()
        C = New ClsConectarDatos
        C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    End Sub

    Public Function mt_EnviarCorreoEvaluacionFinalSustentacion() As Boolean
        Try
            Dim EmailDestino As String = ""
            Dim EmailDestino2 As String = ""
            Dim EmailVarios As String = ""
            Dim alumno As String = ""
            Dim escuela As String = ""
            Dim tesis As String = ""
            Dim rpta As Boolean = False

            Dim dtapro As New Data.DataTable("data")
            Dim dtTesis As New Data.DataTable("data")

            C.AbrirConexion()

            dtapro = C.TraerDataTable("TRL_DatosAlumnoxDetalle", _codigo_dta)
            _codigo_alu = dtapro.Rows(0).Item("codigo_alu")
            dtTesis = C.TraerDataTable("TRL_InformacionProyectoTesis", _codigo_alu)

            C.CerrarConexion()

            alumno = "Bach. " & dtapro.Rows(0).Item("apellidoPat_Alu").ToString & " " & dtapro.Rows(0).Item("apellidoMat_Alu").ToString & " " & dtapro.Rows(0).Item("nombres_Alu").ToString
            escuela = dtapro.Rows(0).Item("nombre_Cpf").ToString

            tesis = dtTesis.Rows(0).Item("Titulo_Tes").ToString


            ' _codigo_apl = 72

            If dtapro.Rows(0).Item("eMail_Alu").ToString <> "" Then
                EmailDestino = dtapro.Rows(0).Item("eMail_Alu")

                If dtapro.Rows(0).Item("UserPrincipalName").ToString <> "" Then
                    EmailDestino2 = dtapro.Rows(0).Item("UserPrincipalName")

                End If

            End If

            If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                EmailVarios = EmailDestino & ";" & EmailDestino2
            Else
                EmailVarios = "epena@usat.edu.pe;fatima.vasquez@usat.edu.pe;hcano@usat.edu.pe"
            End If

            Dim codigo_envio As Integer = ClsComunicacionInstitucional.ObtenerCodigoEnvio(_codigo_per, _codigo_tfu, _codigo_apl)


            rpta = ClsComunicacionInstitucional.EnviarNotificacionEmail(codigo_envio, "SUST", _cin_abreviatura, 1, _codigo_per, "codigo_alu", _codigo_alu, _codigo_apl, EmailVarios, "epena@usat.edu.pe", "", "", escuela, alumno, tesis)



            dtapro = Nothing
            dtTesis = Nothing

            Return rpta

        Catch ex As Exception

            Return False
        End Try


    End Function

    Public Function mt_EnviarCorreoEvaluacionPasos() As Boolean
        Try
            Dim EmailDestino As String = ""
            Dim EmailDestino2 As String = ""
            Dim EmailVarios As String = ""
            Dim alumno As String = ""
            Dim escuela As String = ""
            Dim tesis As String = ""
            Dim rpta As Boolean = False

            Dim dtapro As New Data.DataTable("data")


            C.AbrirConexion()
            dtapro = C.TraerDataTable("TRL_DatosAlumnoxDetalle", _codigo_dta)
            C.CerrarConexion()
            _codigo_alu = dtapro.Rows(0).Item("codigo_alu")


            alumno = dtapro.Rows(0).Item("apellidoPat_Alu").ToString & " " & dtapro.Rows(0).Item("apellidoMat_Alu").ToString & " " & dtapro.Rows(0).Item("nombres_Alu").ToString
            escuela = dtapro.Rows(0).Item("nombre_Cpf").ToString


            ' _codigo_apl = 72

            If dtapro.Rows(0).Item("eMail_Alu").ToString <> "" Then
                EmailDestino = dtapro.Rows(0).Item("eMail_Alu")

                If dtapro.Rows(0).Item("UserPrincipalName").ToString <> "" Then
                    EmailDestino2 = dtapro.Rows(0).Item("UserPrincipalName")

                End If

            End If

            If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                EmailVarios = EmailDestino & ";" & EmailDestino2
            Else
                EmailVarios = "epena@usat.edu.pe;fatima.vasquez@usat.edu.pe;hcano@usat.edu.pe"
            End If

            Dim codigo_envio As Integer = ClsComunicacionInstitucional.ObtenerCodigoEnvio(_codigo_per, _codigo_tfu, _codigo_apl)


            rpta = ClsComunicacionInstitucional.EnviarNotificacionEmail(codigo_envio, "TRVE", _cin_abreviatura, 1, _codigo_per, "codigo_alu", _codigo_alu, _codigo_apl, EmailVarios, "epena@usat.edu.pe", "", "", escuela, alumno, _orden_ftr, _descripcion_tfu, _nombre_ctr)



            dtapro = Nothing


            Return rpta

        Catch ex As Exception

            Return False
        End Try


    End Function

End Class

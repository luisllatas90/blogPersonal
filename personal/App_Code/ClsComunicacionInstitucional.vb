Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic

#Region "ENTIDADES"

Public Class e_EnvioCorreosMasivo

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_ecm As String
    Public tipoCodigoEnvio_ecm As String
    Public codigoEnvio_ecm As String
    Public codigo_apl As String
    Public correo_destino As String
    Public correo_respuesta As String
    Public asunto As String
    Public cuerpo As String
    Public archivo_adjunto As String
    Public fecha_envio As String
    Public mailitem_id As String
    Public codigo_acc As String
    Public profile_name As String

    Public cod_user As String
    Public operacion As String

    'OTROS
    Public codigo_per As String

#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_ecm = String.Empty
        tipoCodigoEnvio_ecm = String.Empty
        codigoEnvio_ecm = String.Empty
        codigo_apl = String.Empty
        correo_destino = String.Empty
        correo_respuesta = String.Empty
        asunto = String.Empty
        cuerpo = String.Empty
        archivo_adjunto = String.Empty
        fecha_envio = "01/01/1901"
        mailitem_id = String.Empty
        codigo_acc = String.Empty
        profile_name = String.Empty

        cod_user = String.Empty
        operacion = String.Empty

        codigo_per = String.Empty
    End Sub

    Public Sub ParametrosNotificacion(<[ParamArray]()> ByVal parametros() As String)
        If parametros.Length > 0 Then
            Dim i As Integer = 1
            Dim num_parametro As String = String.Empty

            For Each parametro As String In parametros
                num_parametro = Strings.Right("000" & CStr(i), 3)

                Me.cuerpo = Me.cuerpo.Replace("@parametro_" & num_parametro, parametro)
                i += 1
            Next
        End If
    End Sub

#End Region

End Class

Public Class e_Notificaciones

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_not As String
    Public tipo_not As String
    Public clasificacion_not As String
    Public nombre_not As String
    Public abreviatura_not As String
    Public version_not As String
    Public asunto_not As String
    Public cuerpo_not As String
    Public profile_name As String

    Public operacion As String
    Public cod_user As String

#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_not = String.Empty
        tipo_not = String.Empty
        clasificacion_not = String.Empty
        nombre_not = String.Empty
        abreviatura_not = String.Empty
        version_not = String.Empty
        asunto_not = String.Empty
        cuerpo_not = String.Empty
        profile_name = String.Empty

        operacion = String.Empty
        cod_user = String.Empty
    End Sub

#End Region

End Class

Public Class e_EnvioNotificacion

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_env As String
    Public codigo_per As String
    Public codigo_tfu As String
    Public codigo_apl As String

    Public operacion As String
    Public cod_user As String

#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_env = String.Empty
        codigo_per = String.Empty
        codigo_tfu = String.Empty
        codigo_apl = String.Empty

        operacion = String.Empty
        cod_user = String.Empty
    End Sub

#End Region

End Class

Public Class e_EnvioNotificacionDetalle

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_end As String
    Public codigo_env As String
    Public codigo_not As String
    Public tipoCodigoMedio_end As String
    Public codigoMedio_end As String
    Public tipoCodigoDestinatario_end As String
    Public codigoDestinatario_end As String
    Public correoDestinatario_end As String
    Public celularDestinatario_end As String
    Public asunto_end As String
    Public cuerpo_end As String

    Public operacion As String
    Public cod_user As String
#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_end = String.Empty
        codigo_env = String.Empty
        codigo_not = String.Empty
        tipoCodigoMedio_end = String.Empty
        codigoMedio_end = String.Empty
        tipoCodigoDestinatario_end = String.Empty
        codigoDestinatario_end = String.Empty
        correoDestinatario_end = String.Empty
        celularDestinatario_end = String.Empty
        asunto_end = String.Empty
        cuerpo_end = String.Empty

        operacion = String.Empty
        cod_user = String.Empty
    End Sub

#End Region

End Class

#End Region

#Region "DATOS"

Public Class d_EnvioCorreosMasivo
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function RegistrarEnvioCorreosMasivo(ByVal le_EnvioCorreosMasivo As e_EnvioCorreosMasivo) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            dt = cnx.TraerDataTable("CINS_EnvioCorreosMasivoIUD", _
                                    le_EnvioCorreosMasivo.operacion, _
                                    le_EnvioCorreosMasivo.cod_user, _
                                    le_EnvioCorreosMasivo.codigo_ecm, _
                                    le_EnvioCorreosMasivo.tipoCodigoEnvio_ecm, _
                                    le_EnvioCorreosMasivo.codigoEnvio_ecm, _
                                    le_EnvioCorreosMasivo.codigo_apl, _
                                    le_EnvioCorreosMasivo.correo_destino, _
                                    le_EnvioCorreosMasivo.correo_respuesta, _
                                    le_EnvioCorreosMasivo.asunto, _
                                    le_EnvioCorreosMasivo.cuerpo, _
                                    le_EnvioCorreosMasivo.archivo_adjunto, _
                                    le_EnvioCorreosMasivo.fecha_envio, _
                                    le_EnvioCorreosMasivo.mailitem_id, _
                                    le_EnvioCorreosMasivo.codigo_acc, _
                                    le_EnvioCorreosMasivo.profile_name)
            'PERFIL

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Sub RegistrarBitacoraEnvio(ByVal le_EnvioCorreosMasivo As e_EnvioCorreosMasivo)
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            cnx.Ejecutar("Insertar_Alumni_BitacoraEnviaMail", _
                         le_EnvioCorreosMasivo.fecha_envio, _
                         le_EnvioCorreosMasivo.codigoEnvio_ecm, _
                         le_EnvioCorreosMasivo.correo_destino, _
                         le_EnvioCorreosMasivo.asunto, _
                         le_EnvioCorreosMasivo.cuerpo, _
                         le_EnvioCorreosMasivo.archivo_adjunto)

            cnx.TerminarTransaccion()
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Sub

    Public Function GetEnvioCorreosMasivo(ByVal codigo As Integer) As e_EnvioCorreosMasivo
        Try
            Dim me_EnvioCorreosMasivo As New e_EnvioCorreosMasivo

            If codigo > 0 Then
            Else
                With me_EnvioCorreosMasivo
                    .codigo_ecm = 0
                    .codigoEnvio_ecm = 0
                    .codigo_apl = 0
                    .fecha_envio = "01/01/1901"
                    .mailitem_id = 0
                    .codigo_acc = 0
                    .cod_user = 0
                End With
            End If

            Return me_EnvioCorreosMasivo
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_Notificaciones
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function ListarNotificacion(ByVal le_Notificacion As e_Notificaciones) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("CINS_NotificacionListar", le_Notificacion.operacion, _
                                    le_Notificacion.codigo_not, _
                                    le_Notificacion.tipo_not, _
                                    le_Notificacion.clasificacion_not, _
                                    le_Notificacion.abreviatura_not, _
                                    le_Notificacion.version_not)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function RegistrarNotificacion(ByVal le_Notificacion As e_Notificaciones) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("CINS_NotificacionIUD", le_Notificacion.operacion, _
                                    le_Notificacion.cod_user, _
                                    le_Notificacion.codigo_not, _
                                    le_Notificacion.tipo_not, _
                                    le_Notificacion.clasificacion_not, _
                                    le_Notificacion.nombre_not, _
                                    le_Notificacion.abreviatura_not, _
                                    le_Notificacion.version_not, _
                                    le_Notificacion.asunto_not, _
                                    le_Notificacion.cuerpo_not, _
                                    le_Notificacion.profile_name)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function ObtenerNotificacion(ByVal tipo As String, ByVal clasificacion As String, ByVal abreviatura As String, ByVal version As String) As e_Notificaciones
        Try
            Dim me_Notificacion As New e_Notificaciones

            If Not String.IsNullOrEmpty(tipo) AndAlso Not String.IsNullOrEmpty(clasificacion) _
                AndAlso Not String.IsNullOrEmpty(abreviatura) AndAlso Not String.IsNullOrEmpty(version) Then

                With me_Notificacion
                    .operacion = "GEN"
                    .tipo_not = tipo
                    .clasificacion_not = clasificacion
                    .abreviatura_not = abreviatura
                    .version_not = version
                End With

                dt = ListarNotificacion(me_Notificacion)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de notificación no ha sido encontrado.")

                me_Notificacion = New e_Notificaciones

                With me_Notificacion
                    .codigo_not = dt.Rows(0).Item("codigo_not")
                    .tipo_not = dt.Rows(0).Item("tipo_not")
                    .clasificacion_not = dt.Rows(0).Item("clasificacion_not")
                    .nombre_not = dt.Rows(0).Item("nombre_not")
                    .abreviatura_not = dt.Rows(0).Item("abreviatura_not")
                    .version_not = dt.Rows(0).Item("version_not")
                    .asunto_not = dt.Rows(0).Item("asunto_not")
                    .cuerpo_not = dt.Rows(0).Item("cuerpo_not")
                    .profile_name = dt.Rows(0).Item("profile_name")
                End With
            Else
                Throw New Exception("Debe enviar todos los parámetros para obtener el modelo de notificación.")
            End If

            Return me_Notificacion
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetNotificacion(ByVal codigo As Integer) As e_Notificaciones
        Try
            Dim me_Notificacion As New e_Notificaciones

            If codigo > 0 Then
                me_Notificacion.operacion = "GEN"
                me_Notificacion.codigo_not = codigo

                dt = ListarNotificacion(me_Notificacion)
                If dt.Rows.Count = 0 Then Throw New Exception("El registro de notificación no ha sido encontrado.")

                me_Notificacion = New e_Notificaciones
                With me_Notificacion
                    .codigo_not = dt.Rows(0).Item("codigo_not")
                    .tipo_not = dt.Rows(0).Item("tipo_not")
                    .clasificacion_not = dt.Rows(0).Item("clasificacion_not")
                    .nombre_not = dt.Rows(0).Item("nombre_not")
                    .abreviatura_not = dt.Rows(0).Item("abreviatura_not")
                    .version_not = dt.Rows(0).Item("version_not")
                    .asunto_not = dt.Rows(0).Item("asunto_not")
                    .cuerpo_not = dt.Rows(0).Item("cuerpo_not")
                    .profile_name = dt.Rows(0).Item("profile_name")
                End With
            Else
                With me_Notificacion
                    .codigo_not = 0
                    .cod_user = 0
                End With
            End If

            Return me_Notificacion
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_EnvioNotificacion
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function RegistrarEnvioNotificacion(ByVal le_EnvioNotificacion As e_EnvioNotificacion) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("CINS_EnvioNotificacionIUD", le_EnvioNotificacion.operacion, _
                                    le_EnvioNotificacion.cod_user, _
                                    le_EnvioNotificacion.codigo_env, _
                                    le_EnvioNotificacion.codigo_per, _
                                    le_EnvioNotificacion.codigo_tfu, _
                                    le_EnvioNotificacion.codigo_apl)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetEnvioNotificacion(ByVal codigo As Integer) As e_EnvioNotificacion
        Try
            Dim me_EnvioNotificacion As New e_EnvioNotificacion

            If codigo > 0 Then

            Else
                With me_EnvioNotificacion
                    .codigo_env = 0
                    .codigo_per = 0
                    .codigo_tfu = 0
                    .codigo_apl = 0
                    .cod_user = 0
                End With
            End If

            Return me_EnvioNotificacion
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_EnvioNotificacionDetalle
    Private cnx As ClsConectarDatos
    Private dt As DataTable

    Public Function RegistrarEnvioNotificacionDetalle(ByVal le_EnvioNotificacionDetalle As e_EnvioNotificacionDetalle) As DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("CINS_EnvioNotificacionDetalleIUD", le_EnvioNotificacionDetalle.operacion, _
                                    le_EnvioNotificacionDetalle.cod_user, _
                                    le_EnvioNotificacionDetalle.codigo_end, _
                                    le_EnvioNotificacionDetalle.codigo_env, _
                                    le_EnvioNotificacionDetalle.codigo_not, _
                                    le_EnvioNotificacionDetalle.tipoCodigoMedio_end, _
                                    le_EnvioNotificacionDetalle.codigoMedio_end, _
                                    le_EnvioNotificacionDetalle.tipoCodigoDestinatario_end, _
                                    le_EnvioNotificacionDetalle.codigoDestinatario_end, _
                                    le_EnvioNotificacionDetalle.correoDestinatario_end, _
                                    le_EnvioNotificacionDetalle.celularDestinatario_end, _
                                    le_EnvioNotificacionDetalle.asunto_end, _
                                    le_EnvioNotificacionDetalle.cuerpo_end)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

    Public Function GetEnvioNotificacionDetalle(ByVal codigo As Integer) As e_EnvioNotificacionDetalle
        Try
            Dim me_EnvioNotificacionDetalle As New e_EnvioNotificacionDetalle

            If codigo > 0 Then

            Else
                With me_EnvioNotificacionDetalle
                    .codigo_end = 0
                    .codigo_env = 0
                    .codigo_not = 0
                    .codigoMedio_end = 0
                    .codigoDestinatario_end = 0

                    .cod_user = 0
                End With
            End If

            Return me_EnvioNotificacionDetalle
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Public Class ClsComunicacionInstitucional
    Private cnx As ClsConectarDatos

    ''' <summary>
    ''' Genera un código que identifica a todas las notificaciones que serán enviadas.
    ''' </summary>
    ''' <param name="codigo_per_emisor">Código del personal que se encuentra logueado (codigo_per).</param>
    ''' <param name="codigo_tfu_emisor">Código del tipo función o rol del personal que se encuentra logueado (codigo_tfu).</param>
    ''' <param name="codigo_apl">Código de la aplicación desde donde se realiza el envío (codigo_apl).</param>
    ''' <returns>Retorna código de envío (codigo_env).</returns>
    ''' <remarks></remarks>
    Public Shared Function ObtenerCodigoEnvio(ByVal codigo_per_emisor As Integer, ByVal codigo_tfu_emisor As Integer, ByVal codigo_apl As Integer) As Integer
        Try
            Dim ld_EnvioNotificacion As New d_EnvioNotificacion

            Dim le_EnvioNotificacion As e_EnvioNotificacion = ld_EnvioNotificacion.GetEnvioNotificacion(0)

            With le_EnvioNotificacion
                .operacion = "I"
                .cod_user = codigo_per_emisor
                .codigo_per = codigo_per_emisor
                .codigo_tfu = codigo_tfu_emisor
                .codigo_apl = codigo_apl
            End With

            Dim dt As DataTable = ld_EnvioNotificacion.RegistrarEnvioNotificacion(le_EnvioNotificacion)

            If dt.Rows.Count = 0 Then Return 0

            Return CInt(dt.Rows(0).Item("codigo_env"))
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Envía una notificación de acuerdo a los parametros adjuntos.
    ''' </summary>
    ''' <param name="codigo_envio">Código de envío que agrupa a todas las notificaciones que serán enviadas en ese momento. Este valor debe ser generado.</param>
    ''' <param name="clasificacion_notificacion">Clase de notificación.</param>
    ''' <param name="abreviatura_notificacion">Abreviatura de notificación.</param>
    ''' <param name="version_notificacion">Versión de notificación.</param>
    ''' <param name="usuario_emisor">Código de Personal que realiza el envío (código_per).</param>
    ''' <param name="tipo_codigo_destinatario">Tipo de código que presenta el destinatario (Valores: codigo_pso, codigo_per, codigo_alu).</param>
    ''' <param name="codigo_destinatario">Código del destinatario, según el tipo.</param>
    ''' <param name="codigo_apl">Código de la aplicación o módulo desde donde se realiza el envío.</param>
    ''' <param name="correo_destinatario">Email del destinatario.</param>
    ''' <param name="correo_respuesta">Email al que puede responder el destinatario.</param>
    ''' <param name="asunto_correo">Este campo permite reemplazar el asunto por defecto que trae la notificación. Si se envía una cadena vacía, el asunto será el que viene por defecto.</param>
    ''' <param name="archivo_adjunto">Enlace de donde se encuentra almacenado el archivo adjunto, ya sea por SharedFiles u otro método.</param>
    ''' <param name="parametros">Parámetros que serán reemplazados en la plantilla de Notificación.</param>
    ''' <returns>Retorna True si el envío se registro satisfactoriamente.</returns>
    ''' <remarks></remarks>
    Public Shared Function EnviarNotificacionEmail(ByVal codigo_envio As Integer, ByVal clasificacion_notificacion As String, ByVal abreviatura_notificacion As String, ByVal version_notificacion As String, _
                                                   ByVal usuario_emisor As Integer, ByVal tipo_codigo_destinatario As String, ByVal codigo_destinatario As Integer, ByVal codigo_apl As Integer, _
                                                   ByVal correo_destinatario As String, ByVal correo_respuesta As String, ByVal asunto_correo As String, ByVal archivo_adjunto As String, ByVal ParamArray parametros() As String) As Boolean
        Try

            Dim le_Notificacion As e_Notificaciones : Dim ld_Notificacion As New d_Notificaciones
            Dim le_EnvioCorreosMasivo As New e_EnvioCorreosMasivo : Dim ld_EnvioCorreosMasivo As New d_EnvioCorreosMasivo
            Dim le_EnvioNotificacionDetalle As e_EnvioNotificacionDetalle : Dim ld_EnvioNotificacionDetalle As New d_EnvioNotificacionDetalle

            Dim codigo_ecm As Integer = 0

            Dim correo_valido As Boolean = ValidarEmail(correo_destinatario)

            correo_destinatario = correo_destinatario.Trim.TrimEnd(";")

            le_Notificacion = ld_Notificacion.ObtenerNotificacion("EMAIL", clasificacion_notificacion, abreviatura_notificacion, version_notificacion)

            If correo_valido Then
                le_EnvioCorreosMasivo = ld_EnvioCorreosMasivo.GetEnvioCorreosMasivo(0)

                With le_EnvioCorreosMasivo
                    .operacion = "I"
                    .cod_user = usuario_emisor
                    .tipoCodigoEnvio_ecm = tipo_codigo_destinatario
                    .codigoEnvio_ecm = codigo_destinatario
                    .codigo_apl = codigo_apl
                    .correo_destino = correo_destinatario
                    .correo_respuesta = correo_respuesta
                    .asunto = IIf(String.IsNullOrEmpty(asunto_correo), le_Notificacion.asunto_not, asunto_correo)
                    .cuerpo = le_Notificacion.cuerpo_not
                    .ParametrosNotificacion(parametros)
                    .profile_name = le_Notificacion.profile_name
                    .archivo_adjunto = archivo_adjunto
                End With

                Dim dt As DataTable = ld_EnvioCorreosMasivo.RegistrarEnvioCorreosMasivo(le_EnvioCorreosMasivo)

                If dt.Rows.Count > 0 Then codigo_ecm = CInt(dt.Rows(0).Item("codigo_ecm"))
            End If

            le_EnvioNotificacionDetalle = ld_EnvioNotificacionDetalle.GetEnvioNotificacionDetalle(0)

            With le_EnvioNotificacionDetalle
                .operacion = "I"
                .cod_user = usuario_emisor
                .codigo_env = codigo_envio
                .codigo_not = le_Notificacion.codigo_not
                .tipoCodigoMedio_end = "codigo_ecm"
                .codigoMedio_end = codigo_ecm
                .tipoCodigoDestinatario_end = tipo_codigo_destinatario
                .codigoDestinatario_end = codigo_destinatario
                .correoDestinatario_end = correo_destinatario
                .asunto_end = IIf(String.IsNullOrEmpty(asunto_correo), le_Notificacion.asunto_not, asunto_correo)
                .cuerpo_end = IIf(correo_valido, le_EnvioCorreosMasivo.cuerpo, le_Notificacion.cuerpo_not)
            End With

            ld_EnvioNotificacionDetalle.RegistrarEnvioNotificacionDetalle(le_EnvioNotificacionDetalle)

            If Not correo_valido Then Return correo_valido

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function ValidarEmail(ByVal email As String) As Boolean
        Try
            If String.IsNullOrEmpty(email) Then Return False

            email = email.Trim.TrimEnd(";")

            Dim estructura As String = "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$"

            Dim correos() As String
            Dim i As Integer
            correos = Split(email, ";")

            For i = LBound(correos) To UBound(correos)
                Dim match As Match = Regex.Match(correos(i).Trim(), estructura, RegexOptions.IgnoreCase)

                If Not match.Success Then
                    Return False
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class

#End Region

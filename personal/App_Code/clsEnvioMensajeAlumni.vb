Imports Microsoft.VisualBasic

Public Class clsEnvioMensajeAlumni
    'Enviar Mail a Uno
    Public Function EnviarUno(ByVal titulo As String, ByVal descripcion As String, _
                              ByVal RemiteId As Integer, ByVal DestinoId As Integer, _
                              Optional ByVal enviarMail As Boolean = False) As String
        Try
            Dim dtCod_Men As Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtCod_Men = obj.TraerDataTable("ALUMNI_RegistraMensaje", titulo, descripcion, RemiteId)

            If dtCod_Men.Rows.Count > 0 Then
                obj.Ejecutar("ALUMNI_RegistraLecturaMensaje", DestinoId, dtCod_Men.Rows(0).Item("codigo_men"))

                If (enviarMail = True) Then
                    Dim dtPersona As New Data.DataTable
                    Dim Mail As New ClsMail

                    dtPersona = obj.TraerDataTable("ALUMNI_RetornaMailPersona", DestinoId)
                    Dim dtDirector As New Data.DataTable
                    dtDirector = RetornaDirectorAlumni()

                    If (dtPersona.Rows.Count > 0) Then
                        If (dtDirector.Rows.Count > 0) Then
                            Mail.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual USAT", dtPersona.Rows(0).Item("Correo"), titulo, descripcion, True, "", dtDirector.Rows(0).Item("usuario_per").ToString() & "@usat.edu.pe")
                        Else
                            Mail.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual USAT", dtPersona.Rows(0).Item("Correo"), titulo, descripcion, True)
                        End If

                        dtCod_Men.Dispose()
                    Else
                        Return "La persona no tiene un correo electronico registrado"
                    End If

                End If
            End If
            obj.CerrarConexion()
            obj = Nothing
            Return ""
        Catch ex As Exception
            Return "Error: " & ex.Message
        End Try
    End Function


    'Enviar Mail a Varios
    Public Function EnviarVarios(ByVal titulo As String, ByVal descripcion As String, _
                              ByVal RemiteId As Integer, ByVal DestinoId() As Integer, _
                              Optional ByVal enviarMail As Boolean = False) As String
        Try
            Dim dtCod_Men As Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtCod_Men = obj.TraerDataTable("ALUMNI_RegistraMensaje", titulo, descripcion, RemiteId)
            Dim sw As Byte = 0
            If dtCod_Men.Rows.Count > 0 Then
                For i As Integer = 0 To DestinoId.Length - 1
                    obj.Ejecutar("ALUMNI_RegistraLecturaMensaje", DestinoId(i), dtCod_Men.Rows(0).Item("codigo_men"))

                    If (enviarMail = True) Then
                        Dim dtPersona As New Data.DataTable
                        Dim Mail As New ClsMail
                        Dim dtDirector As New Data.DataTable
                        dtDirector = RetornaDirectorAlumni()

                        dtPersona = obj.TraerDataTable("ALUMNI_RetornaMailPersona", DestinoId(i))

                        If (dtPersona.Rows.Count > 0) Then
                            If (dtDirector.Rows.Count > 0) Then
                                Mail.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual USAT", dtPersona.Rows(0).Item("Correo"), titulo, descripcion, True, "", dtDirector.Rows(0).Item("usuario_per").ToString() & "@usat.edu.pe")
                            Else
                                Mail.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual USAT", dtPersona.Rows(0).Item("Correo"), titulo, descripcion, True)
                            End If
                        Else
                            sw = 1
                        End If

                        dtCod_Men.Dispose()
                    End If
                Next
            End If

            obj.CerrarConexion()
            obj = Nothing

            If (sw = 1) Then
                Return "No se pudo enviar el mensaje a todos los usuarios."
            End If

            Return ""
        Catch ex As Exception
            Return "Error: " & ex.Message
        End Try
    End Function

    'Mensaje Leido
    Public Function MensajeLeido(ByVal MensajeDetId As Integer) As Boolean
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("ALUMNI_MensajeLeido", MensajeDetId)
            obj.CerrarConexion()
            obj = Nothing            
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Retorna Destinatario. Campos NombreCompleto y Correo    
    ''' </summary>
    ''' <param name="DestinoId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RetornaDestinatario(ByVal DestinoId As Integer) As Data.DataTable
        Try
            Dim dtPersona As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtPersona = obj.TraerDataTable("ALUMNI_RetornaMailPersona", DestinoId)
            obj.CerrarConexion()
            obj = Nothing

            Return dtPersona
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Retorna el Director de ALUMNI. Retoran codigo_per, usuario_per, Personal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RetornaDirectorAlumni() As Data.DataTable
        Try
            'Enviar correo a director de Alumni
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim dtDirector As New Data.DataTable
            obj.AbrirConexion()
            dtDirector = obj.TraerDataTable("ALUMNI_RetornaDirectorCco", 875)
            obj.CerrarConexion()

            Return dtDirector
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class

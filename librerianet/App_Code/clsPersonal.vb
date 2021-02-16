Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsPersonal
    Private cnx As New ClsConectarDatos

   Public Sub AbrirTransaccionCnx()
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.IniciarTransaccion()
    End Sub

    Public Sub CerrarTransaccionCnx()
        cnx.TerminarTransaccion()
    End Sub

    Public Sub CancelarTransaccionCnx()
        cnx.AbortarTransaccion()
    End Sub
    Public Function ConsultarDatosPersonales(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarPersonal", "HO", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function VerificaPeriodoLaborable() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_VerificaPeriodoLaborable")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarVistaHorario(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarHorarioPersonal", codigo_per, codigo_pel, semana)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarListaHorario(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarListaHorario", codigo_per, codigo_pel, semana)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarHorasControl() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarHorasControl")
        cnx.CerrarConexion()
        Return dts
    End Function
    '
    Public Function ConsultarPersonalDirectorDepartamento(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarPersonalDirectorDepartamento", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ConsultarCarreraProfesional() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarCarreraProfesional")
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Sub EliminarHorarioPersonal(ByVal codigo_hop As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_EliminarHorarioPersonal", codigo_hop)
        cnx.CerrarConexion()
    End Sub

    Public Function ConsultarTotalHorasSemana(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer) As Double
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        ConsultarTotalHorasSemana = cnx.TraerDataTable("PER_CalcularTotalHoras", codigo_per, codigo_pel, semana, 0).Rows(0).Item(0).ToString
        cnx.CerrarConexion()
    End Function

    Public Sub EnviarHorarioPersonal(ByVal codigo_per As Integer, ByVal horasSemanales As Integer, ByVal operador As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_FinalizarHorarioPersonal", codigo_per, horasSemanales, operador)
        cnx.CerrarConexion()
    End Sub

    'Se agrego el operador, por el tema de la tabla bitacorapersonal, xDguevara.
    Public Sub EnviarHorarioDirector(ByVal codigo_per As Integer, ByVal horasSemanales As Integer, _
                                     ByVal estadofinal As String, ByVal operador As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_FinalizarHorarioDirector", codigo_per, horasSemanales, estadofinal, operador)
        cnx.CerrarConexion()
    End Sub


    Public Function ConsultarHorariosGeneral() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultaGeneralHorarios")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Sub registrarBitacoraHorario(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal usuarioReg As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_RegistrarBitacoraHorario", codigo_per, codigo_pel, usuarioReg)
        cnx.CerrarConexion()
    End Sub

    Public Function ListaPeriodoLaboral(ByVal codigo_pel As Integer, ByVal descripcion_pel As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ListaPeriodoLaborable", codigo_pel, descripcion_pel)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function PeridoLaboralVigente(ByVal codigo_pel As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_VerificaPeriodoVigente", codigo_pel)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarListaCambiosHorarios(ByVal codigo_per As Integer, ByVal codigo_pel As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarBitacoraHorarios", "LI", codigo_per, codigo_pel)
        cnx.CerrarConexion()
        Return dts
    End Function

    'cambios al 26/03/2010

    Public Function ConsultarFacultad() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarFacultad")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Sub AgregarHorarioPersonal(ByVal diahop As String, ByVal horainicio_hop As String, ByVal horafin_hop As String, ByVal codigo_per As Integer, ByVal codigo_gph As Integer, ByVal tipodia_cpe As String, ByVal codigo_pel As Integer, ByVal codigo_cpf As Integer, ByVal EncargoEsc As String, ByVal ResolucionEncEsc As String, ByVal codigo_fac As Integer, ByVal semana As Integer)
        Dim cpf As Object
        Dim fac As Object
        If codigo_cpf = 0 Then
            cpf = System.DBNull.Value
        Else
            cpf = codigo_cpf
        End If
        If codigo_fac = 0 Then
            fac = System.DBNull.Value
        Else
            fac = codigo_fac
        End If
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_AgregarHorarioPersonal", diahop, horainicio_hop, horafin_hop, codigo_per, codigo_gph, tipodia_cpe, codigo_pel, cpf, EncargoEsc, ResolucionEncEsc, fac, semana)
        cnx.CerrarConexion()
    End Sub
    Public Function ConsultarPersonalDirectorPersonal() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarPersonalDirectorPersonal")
        cnx.CerrarConexion()
        Return dts
    End Function
    '--
    Public Function HabilitarModificarHorarioPersonal(ByVal codigo_per As Integer, _
                                                      ByVal estado_hop As String, _
                                                      ByVal operador As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        Dim mensaje As String = ""
        Dim correo As String = ""
        Dim objMail As New ClsMail
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_HabilitarModificarHorarioPersonal", codigo_per, 0, 0, estado_hop, operador)
        'correo = cnx.TraerDataTable("ConsultarPersonal", "CO", codigo_per).Rows(0).Item("emailUSAT")
        cnx.CerrarConexion()
        ''enviar el correo
        'Dim codigo_envio As Integer = ClsComunicacionInstitucional.ObtenerCodigoEnvio(684, 1, 81)

        'If Trim(LCase(correo)) <> "@usat.edu.pe" Then
        '    mensaje = "<br><br>Su registro de horario ha sido activado.<br><br> Ante una consulta sírvase comunicar con la Sra. Claudia Laos en Dirección de Personal. e-mail: claos@usat.edu.pe.<br><br>Atte.<br><br>Campus Virtual - USAT."
        '    objMail.EnviarMail("campusvirtual@usat.edu.pe", "Dirección de Personal", correo, "Registro de Horario Activado", mensaje, True)
        'End If
        Return dts
    End Function

    '--
    Public Function HabilitarModificarHorarioPersonalSalud(ByVal TIPO As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_ActivarCienciasSalud", TIPO)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Function HabilitarModificarHorarioBiblioteca() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_ActivarBiblioteca")
        cnx.CerrarConexion()
        Return dts
    End Function

    '--Anterior
    Public Sub AsignarHorarioAdministrativo(ByVal tipo As Integer, ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        Select Case tipo
            Case 0 : cnx.Ejecutar("PER_EliminarHorarioPersonalPeriodoLaborable", codigo_per, codigo_pel, semana)
            Case 1 : cnx.Ejecutar("PER_GenerarHorarioAdministrativo1", codigo_per, codigo_pel, semana)
            Case 2 : cnx.Ejecutar("PER_GenerarHorarioAdministrativo2", codigo_per, codigo_pel, semana)
            Case 3 : cnx.Ejecutar("PER_GenerarHorarioAdministrativo3", codigo_per, codigo_pel, semana)
            Case 4 : cnx.Ejecutar("PER_GenerarHorarioAdministrativo4", codigo_per, codigo_pel, semana)
        End Select
        cnx.CerrarConexion()
    End Sub
    '--
    Public Function ConsultarPeridoLaborable() As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarPeriodoLaborableVigente")
        cnx.CerrarConexion()

        If dts.Rows.Count > 0 Then
            Return dts.Rows(0).Item("codigo_pel")
        End If

        Return 0

    End Function

    Public Function ConsultarPeridoLaborable_SegunCicloAcademico(ByVal codigo_cac As Integer) As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarPeriodoLab_CicloAcademico", codigo_cac)
        cnx.CerrarConexion()

        If dts.Rows.Count > 0 Then
            Return dts.Rows(0).Item("codigo_pel")
        End If

        Return 0

    End Function



    'obj.EnviarEmailComunicado(vCodigo_per, txtObservacion.Text.Trim, Trabajador, idPer, txtAsunto.Text.Trim)
    Public Sub EnviarEmailComunicado(ByVal codigo_per As Integer, _
                                   ByVal obs As String, _
                                   ByVal nombre As String, _
                                   ByVal idusuario As Integer, _
                                    ByVal Asunto As String)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim objMail As New ClsMail
        Dim dts As New Data.DataTable
        Dim correo As String
        Dim mensaje As String
        Dim mensaje2 As String
        Dim usuariorevisa As String
        Dim correorevisa As String
        cnx.AbrirConexion()

        'guarda en tabla datospersonal ultimaobservacion
        cnx.Ejecutar("PER_ObservarHorarioPersonal", codigo_per, obs, idusuario)

        correo = cnx.TraerDataTable("ConsultarPersonal", "CO", codigo_per).Rows(0).Item("emailUSAT")

        usuariorevisa = cnx.TraerDataTable("ConsultarPersonal", "CO", idusuario).Rows(0).Item("personal")
        correorevisa = cnx.TraerDataTable("ConsultarPersonal", "CO", idusuario).Rows(0).Item("emailUSAT")
        cnx.CerrarConexion()



        'enviar el correo
        If Trim(LCase(correo)) <> "@usat.edu.pe" Then
            If idusuario = 533 Then  'VERIFICA QUE EL USUARIO SEA: [MIGUEL NECIOSUP]
                mensaje = "Estimado(a): " & nombre & "<br><br>Se le comunica que: <B>" & obs & "</B>.<br><br> Ante una consulta sírvase comunicar con la Sra. Claudia Laos en Dirección de Personal. e-mail: claos@usat.edu.pe.<br><br>Atte.<br><br>Campus Virtual - USAT."
            Else
                'Enviar correo a trabajador
                mensaje = "Estimado(a): " & nombre & "<br><br>Se le comunica que: <B>" & obs & "</B>.<br><br> Ante una consulta sírvase comunicar con el Sr(a). " & usuariorevisa & ". e-mail: " & correorevisa & ".<br><br>Atte.<br><br>Campus Virtual - USAT."
                'Enviar correo a Neciosup Informando que el jefe de area a modificado el horario.
                mensaje2 = "Estimada Sra. Claudia: <br><br>El Director (a) " & usuariorevisa & " ha enviado un aviso sobre el horario del trabajador " & nombre & ".<br><br>Atte.<br><br>Campus Virtual - USAT."
                objMail.EnviarMail("campusvirtual@usat.edu.pe", "Dirección de Personal", "claos@usat.edu.pe", "Modificacion de Horario: " & nombre, mensaje2, True)
            End If

            objMail.EnviarMail("campusvirtual@usat.edu.pe", "Dirección de Personal", correo, Asunto, mensaje, True)
        End If
    End Sub


    Public Sub EnviarEmailTesis(ByVal codigo_per As Integer, _
                                  ByVal obs As String, _
                                  ByVal nombre As String, _
                                  ByVal idusuario As Integer, _
                                   ByVal Asunto As String)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim objMail As New ClsMail
        Dim dts As New Data.DataTable
        Dim correo As String
        Dim mensaje As String
        Dim mensaje2 As String
        Dim usuariorevisa As String
        Dim correorevisa As String
        cnx.AbrirConexion()

        'guarda en tabla datospersonal ultimaobservacion
        cnx.Ejecutar("PER_ObservarHorarioPersonal", codigo_per, obs)
        'desbloquea horario
        cnx.Ejecutar("PER_HabilitarModificarHorarioPersonal", codigo_per, 0, 0)

        correo = cnx.TraerDataTable("ConsultarPersonal", "CO", codigo_per).Rows(0).Item("emailUSAT")

        usuariorevisa = cnx.TraerDataTable("ConsultarPersonal", "CO", idusuario).Rows(0).Item("personal")
        correorevisa = cnx.TraerDataTable("ConsultarPersonal", "CO", idusuario).Rows(0).Item("emailUSAT")
        cnx.CerrarConexion()



        'enviar el correo
        If Trim(LCase(correo)) <> "@usat.edu.pe" Then
            If idusuario = 1437 Then  'VERIFICA QUE EL USUARIO SEA: [NECIOSUP ZUÑIGA MIGUEL ANGEL]
                mensaje = "Estimado(a): " & nombre & "<br><br>Su registro de horario ha sido observado: <B>" & obs & "</B>.<br><br> Ante una consulta sírvase comunicar con la Sra. Claudia Laos en Dirección de Personal. e-mail: claos@usat.edu.pe <br><br>Atte.<br><br>Campus Virtual - USAT."
            Else
                'Enviar correo a trabajador
                mensaje = "Estimado(a): " & nombre & "<br><br>Su registro de horario ha sido observado: <B>" & obs & "</B>.<br><br> Ante una consulta sírvase comunicar con el Sr(a). " & usuariorevisa & ". e-mail: " & correorevisa & ".<br><br>Atte.<br><br>Campus Virtual - USAT."
                'Enviar correo a Neciosup Informando que el jefe de area a modificado el horario.
                mensaje2 = "Estimada Sra. Claudia: <br><br>El Director (a) " & usuariorevisa & " ha modificado el horario del trabajador " & nombre & ".<br><br>Atte.<br><br>Campus Virtual - USAT."
                objMail.EnviarMail("campusvirtual@usat.edu.pe", "Dirección de Personal", "claos@usat.edu.pe", "Modificacion de Horario: " & nombre, mensaje2, True)
            End If

            objMail.EnviarMail("campusvirtual@usat.edu.pe", "Dirección de Personal", correo, Asunto, mensaje, True)
        End If
    End Sub


    '--
    Public Sub ObservarHorario(ByVal codigo_per As Integer, _
                               ByVal obs As String, _
                               ByVal nombre As String, _
                               ByVal idusuario As Integer, _
                               ByVal estado_hop As String, _
                               ByVal operador As Integer)

        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim objMail As New ClsMail
        Dim dts As New Data.DataTable
        Dim correo As String
        Dim mensaje As String
        Dim mensaje2 As String
        Dim usuariorevisa As String
        Dim correorevisa As String
        cnx.AbrirConexion()

        'observa el horario
        'guarda en tabla datospersonal ultimaobservacion
        cnx.Ejecutar("PER_ObservarHorarioPersonal", codigo_per, obs, idusuario)

        'desbloquea horario
        cnx.Ejecutar("PER_HabilitarModificarHorarioPersonal", codigo_per, 0, 0, estado_hop, operador)

        correo = cnx.TraerDataTable("ConsultarPersonal", "CO", codigo_per).Rows(0).Item("emailUSAT")




        usuariorevisa = cnx.TraerDataTable("ConsultarPersonal", "CO", idusuario).Rows(0).Item("personal")
        correorevisa = cnx.TraerDataTable("ConsultarPersonal", "CO", idusuario).Rows(0).Item("emailUSAT")
        cnx.CerrarConexion()

        'enviar el correo
        If Trim(LCase(correo)) <> "@usat.edu.pe" Then
            If idusuario = 1437 Then  'VERIFICA QUE EL USUARIO SEA: [NECIOSUP ZUÑIGA MIGUEL ANGEL]
                mensaje = "Estimado(a): " & nombre & "<br><br>Su registro de horario ha sido observado: <B>" & obs & "</B>.<br><br> Ante una consulta sírvase comunicar con la Sra. Claudia Laos en Dirección de Personal. e-mail: claos@usat.edu.pe<br><br>Atte.<br><br>Campus Virtual - USAT."
            Else
                'Enviar correo a trabajador
                mensaje = "Estimado(a): " & nombre & "<br><br>Su registro de horario ha sido observado: <B>" & obs & "</B>.<br><br> Ante una consulta sírvase comunicar con el Sr(a). " & usuariorevisa & ". e-mail: " & correorevisa & ".<br><br>Atte.<br><br>Campus Virtual - USAT."
                'Enviar correo a Neciosup Informando que el jefe de area a modificado el horario.
                mensaje2 = "Estimada Sra. Claudia: <br><br>El Director (a) " & usuariorevisa & " ha modificado el horario del trabajador " & nombre & ".<br><br>Atte.<br><br>Campus Virtual - USAT."

                'Descomentar
                objMail.EnviarMail("campusvirtual@usat.edu.pe", "Dirección de Personal", "claos@usat.edu.pe", "Modificacion de Horario: " & nombre, mensaje2, True)
                'objMail.EnviarMail("campusvirtual@usat.edu.pe", "Dirección de Personal", "dguevara@usat.edu.pe", "Modificacion de Horario: " & nombre, mensaje2, True)
            End If

            'Descomentar
            objMail.EnviarMail("campusvirtual@usat.edu.pe", "Dirección de Personal", correo, "Registro de Horario Observado", mensaje, True)
            'objMail.EnviarMail("campusvirtual@usat.edu.pe", "Dirección de Personal", "dguevara@usat.edu.pe", "Modificacion de Horario: " & nombre, mensaje2, True)
        End If
    End Sub

    '--
    Public Function ConsultarObservacion(ByVal codigo_per As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarObservacionHorario", codigo_per)
        cnx.CerrarConexion()
        If dts.Rows.Count > 0 Then
            Return dts.Rows(0).Item("ultimaObservacionHorario_Per")
        End If

        Return ""

    End Function

    '--
    Public Function TotalHorasMes(ByVal codigo_per As Integer, ByVal codigo_pel As Integer) As Double
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarHorasMes", codigo_per, codigo_pel)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("HoraMes")
    End Function

    '--
    '------------  Agregador xDguevara 21.01.2013 -------------------------------
    Public Function FirmoContratoPersonal(ByVal codigo_per As Integer) As Boolean
        Dim dts As New Data.DataTable
        Dim sw As Boolean
        sw = False
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_VerificaFirmaContrato", codigo_per)
        cnx.CerrarConexion()
        If dts.Rows.Count > 0 Then

            If dts.Rows(0).Item("estado_Per") = 1 Then
                'No muestra el mensaje.
                sw = False
            Else
                'Muestra el mensaje.
                sw = True
            End If

        End If
        Return sw
    End Function
    '=============////


    Public Function EsCCSalud(ByVal codigo_per As Integer) As Boolean
        'La consulta devuelve registros cuando se trata de personal dentro del dpto
        'Ciencias de la salud
        'Porque solo ellos tienen un horario semanal
        'De ser asi devuelve true

        Dim dts As New Data.DataTable
        Dim sw As Boolean
        sw = False
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_EsCCSalud", codigo_per)
        cnx.CerrarConexion()
        If dts.Rows.Count > 0 Then
            sw = True
        End If
        Return sw
    End Function

    '--
    'Se usa en el frmHorario.aspx
    Public Function ConsultarRangoFechasSemana(ByVal semana As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarRangoFechasSemana", semana)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Rango")
    End Function

    '--
    Public Function ConsultarPeriodoLaborableVigente() As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarPeriodoLaborableVigente")
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("codigo_Pel")
    End Function

    '--
    Public Function ConsultarDptoAcademico() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarDptoAcademico")
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Function ConsultarCentroCostos(ByVal vCriterio As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarCentroCosto", "DE", vCriterio)
        cnx.CerrarConexion()
        Return dts
    End Function
    '--

    '-- Hcano 05-01-2017
    Public Function ConsultarCentroCostos_POA(ByVal vCriterio As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarCentroCosto", "PO", vCriterio)
        cnx.CerrarConexion()
        Return dts
    End Function
    '-- Fin Hcano
    '-- Hcano 02-01-2018
    Public Function ConsultarCentroCostosPlanilla_POA(ByVal vCriterio As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarCentroCosto", "PP", vCriterio)
        cnx.CerrarConexion()
        Return dts
    End Function
    '-- Fin Hcano

    Public Function InsertarCentroCostos(ByVal centrocostos As Integer, ByVal tipoactividad As Integer, ByVal esfacudep As Integer, ByVal xTipo As String, ByVal xPeriodoLaborable As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_InsertarCentroCostos", centrocostos, tipoactividad, esfacudep, xTipo, xPeriodoLaborable)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Function ConsultarCentroCostosSeleccionados(ByVal xTipoActividad As Integer, ByVal xesfacudep As Integer, ByVal xTipo As String, ByVal xPeriodoLaborable As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarCentroCostosSeleccionados", xTipoActividad, xesfacudep, xTipo, xPeriodoLaborable)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Function EliminarCentroCostos(ByVal centrocostos As Integer, ByVal tipoactividad As Integer, ByVal esfacudep As Integer, ByVal xTipo As String, ByVal xPeriodoLaborable As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_EliminarCentroCostos", centrocostos, tipoactividad, esfacudep, xTipo, xPeriodoLaborable)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Function ConsultarDuplicadoCentroCostos(ByVal centrocostos As Integer, ByVal xTipoActividad As Integer, ByVal xesfacudep As Integer, ByVal xTipo As String, ByVal xPeriodoLaborable As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarDuplicadoCentroCostos", centrocostos, xTipoActividad, xesfacudep, xTipo, xPeriodoLaborable)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Function ConsultarCarreraProfesional_v2() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarCarreraProfesional_v2")
        cnx.CerrarConexion()
        Return dts
    End Function
    'METODOS CREADOS EL 03/08/2011
    '--
    Public Function ConsultarHorasAsesoria(ByVal codigo_per As Integer, ByVal codigo_Cac As Integer) As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarHorasAsesoria", codigo_per, codigo_Cac)
        cnx.CerrarConexion()

        If Not IsNumeric(dts.Rows(0).Item("TotalHorasAsesoriaTesis")) Then
            Return 0
        Else
            Return dts.Rows(0).Item("TotalHorasAsesoriaTesis")
        End If

        Return 0
    End Function

    '#####################################  Agregado el 28/11/2011 x dguevara ###################################################
    Public Function ConsultarHorasAsesoria_V2(ByVal codigo_per As Integer, ByVal codigo_Cac As Integer) As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_HorasAsesoriaTesisMatriculadosCicloAcademico", codigo_per, codigo_Cac)
        cnx.CerrarConexion()

        If Not IsNumeric(dts.Rows(0).Item("TotalHorasTesisCac")) Then
            Return 0
        Else
            Return dts.Rows(0).Item("TotalHorasTesisCac")
        End If

        Return 0
    End Function

    Public Function ConsultarHorasAsesoria_GOPP(ByVal codigo_per As Integer, ByVal codigo_Cac As Integer) As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_HorasAsesoriaTesisMatriculadosCicloAcademico_GOPP", codigo_per, codigo_Cac)
        cnx.CerrarConexion()

        If Not IsNumeric(dts.Rows(0).Item("TotalHorasTesisCac")) Then
            Return 0
        Else
            Return dts.Rows(0).Item("TotalHorasTesisCac")
        End If

        Return 0
    End Function
    '#######################################################################################################################

    '--
    Public Sub AsignarHorarioAdministrativo_v2(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer, ByVal ccmatriz As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        'Este sp llama a PER_AgregarHorarioPersonal_v2
        cnx.Ejecutar("PER_GenerarHorarioAdministrativo_v2", codigo_per, codigo_pel, semana, ccmatriz)
        cnx.CerrarConexion()
    End Sub

    '--
    Public Function ConsultarListaHorario_v2(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarListaHorario_v2", codigo_per, codigo_pel, semana)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Function AsignarRefrigerio(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer, ByVal dia As String, ByVal inicio As String, ByVal fin As String) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        'Llama a PER_AgregarHorarioPersonal_v2
        dts = cnx.TraerDataTable("PER_AsignarRefrigerio", codigo_per, codigo_pel, semana, dia, inicio, fin)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Resultado").ToString
    End Function

    '--
    Public Sub AgregarHorarioPersonal_v2(ByVal diahop As String, ByVal horainicio_hop As String, _
    ByVal horafin_hop As String, ByVal codigo_per As Integer, ByVal codigo_gph As Integer, _
    ByVal abreviatura_td As String, ByVal codigo_pel As Integer, ByVal esfacudep As Integer, _
    ByVal EncargoEsc As String, ByVal ResolucionEncEsc As String, ByVal semana As Integer, _
    ByVal xtipo As String, ByVal centrocostos As Integer, ByVal observacion As String)

        'xtipo es una cadena que indica si es facultad, dpto o escuela
        Dim cpf As Object = 0   'codigo carrera profesional
        Dim fac As Object = 0   'codigo facultad
        Dim dac As Object = 0   'codigo departamento        
        Dim obj As New clsPersonal

        If xtipo = "Departamento" Then
            cpf = System.DBNull.Value
            fac = System.DBNull.Value
            dac = esfacudep
        ElseIf xtipo = "Facultad" Then
            cpf = System.DBNull.Value
            dac = System.DBNull.Value
            fac = esfacudep
        ElseIf xtipo = "Escuela" Then
            fac = System.DBNull.Value
            dac = System.DBNull.Value
            cpf = esfacudep
        Else
            cpf = System.DBNull.Value
            fac = System.DBNull.Value
            dac = System.DBNull.Value
        End If

        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_AgregarHorarioPersonal_v2", diahop, horainicio_hop, horafin_hop, codigo_per, _
        codigo_gph, abreviatura_td, codigo_pel, cpf, EncargoEsc, ResolucionEncEsc, fac, semana, _
       centrocostos, observacion, dac)
        cnx.CerrarConexion()
    End Sub

    '--
    Public Function Consultarabreviatura_td(ByVal tipoactividad As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_Consultarabreviatura_td", tipoactividad)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("abreviatura_td")
    End Function

    '### 01/12/2011 x dguevara ################
    'ValidarHorasGestionAcademicaConHorasLectivas

    Public Function ValidarHorasGestionAcademicaConHorasLectivas(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, _
                                                                 ByVal vSemana As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ValidarHorasGestionAcademicaConHorasLectivas", "HL", codigo_per, codigo_pel, vSemana)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("HoraMinuto").ToString
    End Function

    Public Function ConsultarHorasCargaAcademica(ByVal codigo_per As Integer, ByVal codigo_pel As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_TotalHorasProgramadasCargaAcademica", codigo_per, codigo_pel)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("HorasCarga").ToString
    End Function


    'retonra el 50% de las horas lectivas, para validarlo con el registro de las horas de gestiona academica del docente
    Public Function HoraGestionAcademicaSegunLectivas(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, _
                                                      ByVal vsemana As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ValidarHorasGestionAcademicaConHorasLectivas", "VL", codigo_per, codigo_pel, vsemana)
        cnx.CerrarConexion()
        Return dts
    End Function

    '-------------------------------------------------------------------------------------------------------------------------------
    '--
    Public Function ConsultarCicloAcademicoVigente() As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarCicloAcademicoVigente")
        cnx.CerrarConexion()
        If dts.Rows.Count > 0 Then
            Return dts.Rows(0).Item("codigo_Cac")
        End If

        Return 0
    End Function

    '--
    Public Function Consultarcolor_td(ByVal tipoactividad As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_Consultarabreviatura_td", tipoactividad)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("color_td")
    End Function

    '--
    Public Function ConsultarVistaHorario_v2(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarHorarioPersonal_v2", codigo_per, codigo_pel, semana)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Function ConsultaInformacionTipoActividad(ByVal CodigoTipoActividad As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultaInformacionTipoActividad", CodigoTipoActividad)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Function ConsultarTotalSemanas(ByVal codigo_pel As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarTotalSemanas", codigo_pel)
        cnx.CerrarConexion()
        Return dts
    End Function

    ''******************************************************************
    '***09.07.2014***'
    Public Function ConsultarOtrosDatos(ByVal tipo As String, ByVal codigo_pel As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarDatosHorario", tipo, codigo_pel)
        cnx.CerrarConexion()
        Return dts
    End Function
    ''******************************************************************





    'add 13.09.2013
    'Procedimiento para verificar que el trabajador haya registrado su horario para el periodo laboral, segun el codigo_cac
    Public Function verifica_registro_horario_personal(ByVal codigo_per As Integer, ByVal codigo_cac As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_verifica_registro_horario_personal", codigo_per, codigo_cac)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--

    Public Function ConsultarTotalHorasSemana_v2(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer) As Double
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        ConsultarTotalHorasSemana_v2 = cnx.TraerDataTable("PER_CalcularTotalHoras_v2", codigo_per, codigo_pel, semana, 0).Rows(0).Item(0).ToString
        cnx.CerrarConexion()

    End Function
    '--
    Public Function TotalHorasMes_v2(ByVal codigo_per As Integer, ByVal codigo_pel As Integer) As Double
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarHorasMes_v2", codigo_per, codigo_pel)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("HoraMes")
    End Function

    '--
    Public Function ConsultarFechaServidor() As DateTime
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarFechaServidor")
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("FechaServidor")
    End Function

    'Devuelve el nro de horas (horas y minutos) de asesoria de tesis del horario personal para comparar 
    'con el total de horas de asesoria de tesis asignada en la carga academica

    '--
    Public Function ConsultarHorasAsesoriaTesisHorario(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarHorasAsesoriaTesisHorario", codigo_per, codigo_pel, semana)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("TotalHoras")
    End Function

    '--
    Public Function ConsultarHorarioRefrigerio(ByVal codigo_per As Integer, ByVal codigo_pel As Integer) As Boolean
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarHorarioRefrigerio", codigo_per, codigo_pel)
        cnx.CerrarConexion()

        If IsDBNull(dts.Rows(0).Item("horainicio_hop")) Then
            Return False
        Else
            Return True
        End If
    End Function

    '--
    Public Sub AsignarEstadoHorario(ByVal codigo_per As Integer, ByVal estado As String)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_AsignarEstadoHorario", codigo_per, estado)
        cnx.CerrarConexion()
    End Sub

    '--
    Public Function ConsultarEstadoHorario(ByVal codigo_per As Integer) As DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultaEstadoHorario", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Sub ActualizarHorarioAcademico_Personal(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal codigo_cac As Integer, ByVal semana As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_ActualizarHorarioAcademico_Personal", codigo_per, codigo_pel, codigo_cac, semana)
        cnx.CerrarConexion()
    End Sub

    '--
    Public Function ConsultarHorasDocencia(ByVal codigo_per As Integer, ByVal codigo_pel As Integer) As DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarHorasDocencia", codigo_per, codigo_pel)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Function ConsultarHorasAdministrativo(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal dia As String, ByVal horainicio_doc As String, ByVal horafin_doc As String) As DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarHorasAdministrativo", codigo_per, codigo_pel, dia, horainicio_doc, horafin_doc)
        cnx.CerrarConexion()
        Return dts
    End Function

    'Al importar la carga academica, valida cruces de horarios
    'Public Sub VerificarCruceHorarios(ByVal horainicio_adm As String, ByVal horafin_adm As String, ByVal horainicio_doc As String, ByVal horafin_doc As String, ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal dia As String, ByVal semana As Integer)
    '    cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    cnx.AbrirConexion()
    '    cnx.Ejecutar("PER_VerificarCruceHorarios", horainicio_adm, horafin_adm, horainicio_doc, horafin_doc, codigo_per, codigo_pel, dia, semana)
    '    cnx.CerrarConexion()
    'End Sub

    '--
    Public Function VerificarCruceHorarios(ByVal horainicio_adm As String, ByVal horafin_adm As String, ByVal horainicio_doc As String, ByVal horafin_doc As String, ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal dia As String, ByVal semana As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_VerificarCruceHorarios", horainicio_adm, horafin_adm, horainicio_doc, horafin_doc, codigo_per, codigo_pel, dia, semana)
        Return dts.Rows(0).Item("Resultado")
        cnx.CerrarConexion()
    End Function


    'Public Sub ValidarCruceDocencia(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer, ByVal HoraInicio As String, ByVal HoraFin As String, ByVal dia As String, ByVal tipodia As String)
    '    cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    cnx.AbrirConexion()
    '    cnx.Ejecutar("PER_ValidarCruceDocencia", codigo_per, codigo_pel, semana, HoraInicio, HoraFin, dia, tipodia)
    '    cnx.CerrarConexion()
    '    'Return dts
    'End Sub


    '--
    Public Function ValidarCruceDocencia(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer, ByVal HoraInicio As String, ByVal HoraFin As String, ByVal dia As String, ByVal tipodia As String) As DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ValidarCruceDocencia", codigo_per, codigo_pel, semana, HoraInicio, HoraFin, dia, tipodia)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Function ValidarCruceHorarios(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer, ByVal HoraInicio As String, ByVal HoraFin As String, ByVal dia As String, ByVal tipodia As String) As DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ValidarCruceHorarios", codigo_per, codigo_pel, semana, HoraInicio, HoraFin, dia, tipodia)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Function ValidarCrucesFinal(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer, ByVal HoraInicio As String, ByVal HoraFin As String, ByVal dia As String, ByVal tipodia As String, ByVal cc As Integer, ByVal LugarPracticasExternas As String) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        'PER_ValidarCrucesFinal valida cruces, si no encuentra, ejecuta el sp PER_AgregarHorarioPersonal_v2
        'PER_AgregarHorarioPersonal_v2 valida el limite de hora inicio y fin
        dts = cnx.TraerDataTable("PER_ValidarCrucesFinal", codigo_per, codigo_pel, semana, HoraInicio, HoraFin, dia, tipodia, cc, LugarPracticasExternas)
        If dts.Rows.Count > 0 Then
            Return dts.Rows(0).Item("Resultado").ToString
        End If
        Return ""
        cnx.CerrarConexion()
    End Function

    '--
    Public Function ConsultarHorarioGeneral() As DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarHorarioGeneral")
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Function PreenvioHorarioPersonal(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer, ByVal horaInicio As String, ByVal horaFin As String, ByVal dia As String) As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_PreenvioHorarioPersonal", codigo_per, codigo_pel, semana, horaInicio, horaFin, dia)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Cantidad")
    End Function

    '--
    Public Function ConsultaTipoActividadPorAbreviatura(ByVal abreviaturatd As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultaTipoActividadPorAbreviatura", abreviaturatd)
        cnx.CerrarConexion()
        Return dts
    End Function

    ''--, ByVal fecha As String
    'Public Function RangoFechasSemanas(ByVal Semana As Integer) As Data.DataTable
    '    Dim dts As New Data.DataTable
    '    cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    cnx.AbrirConexion()
    '    dts = cnx.TraerDataTable("PER_RangoFechasSemanas", Semana)
    '    cnx.CerrarConexion()
    '    Return dts
    'End Function

    '--------------------------------------------
    '** Metodos creados para modificaion Horario. 12/08/2011
    '--
    Public Function ConsultarCentroCostosSeleccionados_v2(ByVal xTipoActividad As Integer, ByVal xesfacudep As Integer, ByVal xTipo As String, ByVal xPeriodoLaborable As Integer, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarCentroCostosSeleccionados_v2", xTipoActividad, xesfacudep, xTipo, xPeriodoLaborable, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    '-------------------------------------------------------------
    '** Metodos creados para Revision Horario. 12/08/2011
    '--
    Public Function ConsultarPersonalDirectorDepartamento_v2(ByVal codigo_per As Integer, ByVal estado_hop As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarPersonalDirectorDepartamento_v3", codigo_per, estado_hop)
        cnx.CerrarConexion()
        Return dts
    End Function

    '-----------------------------------------------------------
    '** Metodos creados para Consultar Historico de Horarios 15/08/2011
    '--
    Public Function ConsultarListaHorarioPorFecha(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal fecha As String, ByVal hora As String, ByVal semana As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarListaHorarioPorFecha", codigo_per, codigo_pel, fecha, hora, semana)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarVistaHorarioPorFecha(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal fecha As String, ByVal hora As String, ByVal semana As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarHorarioPersonalPorFecha", codigo_per, codigo_pel, fecha, hora, semana)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarVistaHorarioPorFecha_v2(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal fecha As String, ByVal hora As String, ByVal semana As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarHorarioPersonalPorFecha_v2", codigo_per, codigo_pel, fecha, hora, semana)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Sub RegistrarBitacoraObservacionHorario(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal obs As String)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim dts As New Data.DataTable
        cnx.AbrirConexion()
        'guarda el historico de observaciones
        cnx.Ejecutar("PER_RegistrarBitacoraObservacionHorario", codigo_per, codigo_pel, obs)
        cnx.CerrarConexion()
    End Sub


    '-------------------------------------------------
    '** Metodos creados para Consultar Historico de Horarios 16/08/2011

    'Public Function ConsultarBitacoraHorario(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal fecha As String, ByVal hora As String) As Data.DataTable
    '    Dim dts As New Data.DataTable
    '    cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    cnx.AbrirConexion()
    '    dts = cnx.TraerDataTable("PER_ConsultarBitacoraHorario", codigo_per, codigo_pel, fecha, hora)
    '    cnx.CerrarConexion()        
    '    Return dts
    'End Function

    '--
    Public Function ConsultarTotalHorasSemanaBitacora(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal fecha As String, ByVal hora As String, ByVal semana As Integer) As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_CalcularHorasSemanaBitacora", codigo_per, codigo_pel, fecha, hora, semana)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Horas")
    End Function
    '--
    Public Function ConsultarTotalHorasMesBitacora(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal fecha As String, ByVal hora As String, ByVal semana As Integer) As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_CalcularHorasMesBitacora", codigo_per, codigo_pel, fecha, hora, semana)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("HoraMes")
    End Function
    '--
    Public Function ConsultarBitacoraObservacion(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal fecha As String, ByVal hora As String) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarBitacoraObservacion", codigo_per, codigo_pel, fecha, hora)
        cnx.CerrarConexion()

        If dts.Rows.Count > 0 Then
            Return dts.Rows(0).Item("descripcion_obs").ToString
        Else
            Return ""
        End If
    End Function

    '--------------------
    'Metodos para Revisar Director Personal
    '--
    Public Function ConsultarPersonalDirectorPersonal_v2(ByVal estado As String, ByVal codigo_cco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarPersonalDirectorPersonal_v2", estado, codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function

    '




    '--
    Public Function ConsultarCentroCostosHP() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarCentroCostosHP")
        cnx.CerrarConexion()
        Return dts
    End Function

    '###### dguevara 01/12/2011
    Public Function CargaHorasLectivas(ByVal Codigo_per As Integer, ByVal codigo_pel As Integer, _
                                       ByVal semana As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_HorasLectivas", Codigo_per, codigo_pel, semana)

        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargaDiaHorasLectivas(ByVal Codigo_per As Integer, ByVal codigo_pel As Integer, _
                                       ByVal semana As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("", Codigo_per, codigo_pel, semana)

        cnx.CerrarConexion()
        Return dts
    End Function


    '**************************
    'Métodos para Calendario Computable
    '--
    Public Function ConsultarPeriodosLaborables(ByVal tipo As String, ByVal codigo_pel As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarPeriodosLaborables", tipo, codigo_pel)
        cnx.CerrarConexion()
        Return dts
    End Function
    '--
    Public Function ConsultarMes(ByVal mes As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarMes", mes)
        Return dts.Rows(0).Item("nombre_mes")
        cnx.CerrarConexion()
    End Function
    '--
    Public Sub InsertarSemanasControl(ByVal semana As Integer, ByVal mes As Integer, ByVal año As String, ByVal desde As String, ByVal hasta As String, ByVal codigo_pel As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim dts As New Data.DataTable
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_InsertarSemanasControl", semana, mes, año, desde, hasta, codigo_pel)
        cnx.CerrarConexion()
    End Sub
    '--
    Public Function ConsultarCalendarioComputable(ByVal codigo_pel As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarCalendarioComputable", codigo_pel)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Function ValidaDuplicidad(ByVal semana As Integer, ByVal mes As Integer, ByVal año As String, ByVal codigo_pel As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ValidaDuplicidad", semana, mes, año, codigo_pel)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Sub EliminarCalendarioComputable(ByVal codigo_Sec As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_EliminarCalendarioComputable", codigo_Sec)
        cnx.CerrarConexion()
    End Sub

    '**********************************************************************************
    'Metodos para Registrar Dia No Laborable 18/08/2011
    '--
    Public Sub RegistraDiaNoLaborable(ByVal descripcion As String, ByVal tipodnl As String, ByVal codigo_pel As Integer, ByVal fecha As String)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("sp_Pla_RegistraDiaNoLaborable", descripcion, tipodnl, codigo_pel, fecha)
        cnx.CerrarConexion()
    End Sub
    '--
    Public Function ConsultarDiaNoLaborable(ByVal codigo_pel As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarDiaNoLaborable", codigo_pel)
        cnx.CerrarConexion()
        Return dts
    End Function
    '--
    Public Sub EliminarDiaNoLaborable(ByVal codigo_dnl As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_EliminarDiaNoLaborable", codigo_dnl)
        cnx.CerrarConexion()
    End Sub
    '--
    Public Function ValidaDuplicidadDiaNoLab(ByVal fecha As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ValidaDuplicidadDiaNoLab", fecha)
        cnx.CerrarConexion()
        Return dts
    End Function

    '***************** 19/08/2011
    '--
    Public Function ValidarHorarioSemestralySemanal(ByVal codigo_pel As Integer, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ValidarHorarioSemestralySemanal", codigo_pel, codigo_per)
        Return dts
        cnx.CerrarConexion()
    End Function
    '--
    Public Function VerificarLimiteHoras(ByVal dia As String, ByVal inicio As String, ByVal fin As String, ByVal codigo_per As Integer, ByVal codigo_gph As Integer, ByVal tipodia As String, ByVal codigo_pel As Integer, ByVal codigo_cpf As Integer, ByVal encargo As String, ByVal resolucion As String, ByVal codigo_fac As Integer, ByVal semana As Integer, ByVal codigo_cco As Integer, ByVal observacion_hop As String, ByVal codigo_dac As Integer, ByVal accion As String) As Boolean
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_AgregarHorarioPersonal_v2", dia, inicio, fin, codigo_per, codigo_gph, tipodia, codigo_pel, codigo_cpf, encargo, resolucion, codigo_fac, semana, codigo_cco, observacion_hop, codigo_dac, accion)
        Return dts.Rows(0).Item("sw")
        cnx.CerrarConexion()
    End Function

    '--
    Public Function CalcularTotalHorasDiarias(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer, ByVal dia As String) As Double
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        CalcularTotalHorasDiarias = cnx.TraerDataTable("PER_CalcularTotalHorasDiarias", codigo_per, codigo_pel, semana, dia, 0).Rows(0).Item(0).ToString
        cnx.CerrarConexion()
    End Function

    '--21/06/2019  MNeciosup
    Public Function CalcularTotalMinutosRefrigerioDiario(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer, ByVal dia As String) As Double
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        CalcularTotalMinutosRefrigerioDiario = cnx.TraerDataTable("PER_CalcularTotalMinRefrigerio", codigo_per, codigo_pel, semana, dia, 0).Rows(0).Item(0).ToString
        cnx.CerrarConexion()
    End Function
    '--21/06/2019  MNeciosup

    Public Sub EnviarAlertaHorarios(ByVal codigo_per As Integer, ByVal nombre As String, ByVal idusuario As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim ObjMailNet As New ClsMail
        Dim dts As New Data.DataTable
        Dim correo As String
        Dim mensaje As String
        Dim usuariorevisa As String
        Dim correorevisa As String
        Dim para As String = ""

        cnx.AbrirConexion()

        correo = cnx.TraerDataTable("ConsultarPersonal", "CO", codigo_per).Rows(0).Item("emailUSAT")
        usuariorevisa = cnx.TraerDataTable("ConsultarPersonal", "CO", idusuario).Rows(0).Item("personal")
        correorevisa = cnx.TraerDataTable("ConsultarPersonal", "CO", idusuario).Rows(0).Item("emailUSAT")
        cnx.CerrarConexion()

        'enviar el correo
        If Trim(LCase(correo)) <> "@usat.edu.pe" Then
            para = "</br><font face='Courier'>" & "Estimada : <b>" & "LAOS DIAZ CLAUDIA " & "</b>"
            mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, el trabajador " & nombre & " esta intentando enviar su horario, pero sus horas semanales asignadas en el sistema de personal no coinciden con la distribución del horario registrado por el trabajador en el campus virtual, favor de verificar." & "</P>"
            mensaje = mensaje & "</br> Atte.<br><br>Campus Virtual - USAT.</font>"

            'Envia email al asistente de personal
            ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Investigaciones", "claos@usat.edu.pe", "Notificación Registro Horarios - Horas no coinciden con la distribución", para & mensaje, True)
            'Envia email al admin del sistema
            'ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Investigaciones", "dguevara@usat.edu.pe", "Notificación Registro Horarios - Horas no coinciden con la distribución", para & mensaje, True)
        End If

        cnx.CerrarConexion()
    End Sub


    Public Sub AprobarHorario(ByVal codigo_per As Integer, ByVal nombre As String, ByVal idusuario As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim objMail As New ClsMail
        Dim dts As New Data.DataTable
        Dim correo As String
        Dim mensaje As String
        Dim mensaje2 As String
        Dim usuariorevisa As String
        Dim correorevisa As String
        cnx.AbrirConexion()

        correo = cnx.TraerDataTable("ConsultarPersonal", "CO", codigo_per).Rows(0).Item("emailUSAT")


        usuariorevisa = cnx.TraerDataTable("ConsultarPersonal", "CO", idusuario).Rows(0).Item("personal")
        correorevisa = cnx.TraerDataTable("ConsultarPersonal", "CO", idusuario).Rows(0).Item("emailUSAT")
        cnx.CerrarConexion()
        'enviar el correo

        If Trim(LCase(correo)) <> "@usat.edu.pe" Then
            If idusuario = 1437 Then  'VERIFICA QUE EL USUARIO SEA: [NECIOSUP ZUÑIGA MIGUEL ANGEL]
                mensaje = "Estimado(a): " & nombre & "<br><br>Su registro de horario ha sido generado. <B>" & "</B><br><br> Ante una consulta sírvase comunicar con la Sra. Claudia Laos en Dirección de Personal. e-mail: claos@usat.edu.pe.<br><br>Atte.<br><br>Campus Virtual - USAT."
            Else
                'Enviar correo a trabajador
                mensaje = "Estimado(a): " & nombre & "<br><br>Su registro de horario ha sido generado. <B>" & "</B><br><br> Ante una consulta sírvase comunicar con el Sr(a). " & usuariorevisa & ". e-mail: " & correorevisa & ".<br><br>Atte.<br><br>Campus Virtual - USAT."
                'Enviar correo a Neciosup Informando que el jefe de area a modificado el horario.

                '···········DESCOMENTAR LAS DOS LINEAS SIGUIENTES:
                mensaje2 = "Estimada Sra. Claudia: <br><br>El Director (a) " & usuariorevisa & " ha modificado el horario del trabajador " & nombre & ".<br><br>Atte.<br><br>Campus Virtual - USAT."
                'Descomentar
                objMail.EnviarMail("campusvirtual@usat.edu.pe", "Dirección de Personal", "claos@usat.edu.pe", "Modificacion de Horario: " & nombre, mensaje2, True)
            End If
            'Descomentar
            'xii
            objMail.EnviarMail("campusvirtual@usat.edu.pe", "Dirección de Personal", correo, "Registro de Horario Aprobado", mensaje, True)
            '--------

            'objMail.EnviarMail("campusvirtual@usat.edu.pe", "Aviso", "dguevara@usat.edu.pe", "Alerta Horario:Registro de Horario Aprobado", mensaje, True)
        End If

        cnx.CerrarConexion()
    End Sub

    '************************ 22/08/2011
    '--
    Public Function ConsultarCarreraProfesionalyCentros() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarCarreraProfesionalyCentros")
        cnx.CerrarConexion()
        Return dts
    End Function


    '******************** 23/08/11 Mantenimiento Personal Exceptuado de Marcacion
    '--
    Public Function InsertarPersonalExceptuado(ByVal codigo_pel As Integer, ByVal codigo_per As Integer, ByVal fechainicio As String, ByVal fechafin As String) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_InsertarPersonalExceptuado", codigo_pel, codigo_per, fechainicio, fechafin)
        cnx.CerrarConexion()
        If dts.Rows.Count > 0 Then
            Return dts.Rows(0).Item("Mensaje").ToString
        Else
            Return ""
        End If
    End Function
    '--
    Public Function ConsultarPersonalExceptuado(ByVal codigo_pel As Integer) As DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarPersonalExceptuado", codigo_pel)
        cnx.CerrarConexion()
        If dts.Rows.Count > 0 Then
            Return dts
        Else
            Return Nothing
        End If
    End Function

    '--
    Public Sub EliminarPersonalExceptuado(ByVal codigo_pem As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_EliminarPersonalExceptuado", codigo_pem)
        cnx.CerrarConexion()
    End Sub

    '--
    'Mantenimiento Parametros Tolerancia
    Public Function ConsultarParametrosTolerancia(ByVal codigo_pel As Integer) As DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarParametrosTolerancia", codigo_pel)
        cnx.CerrarConexion()
        Return dts
    End Function
    '--
    Public Function InsertarParametroTolerancia(ByVal codigo_pel As Integer, ByVal abreviatura_pap As String, ByVal descripcion_pap As String, ByVal fechainicio As String, ByVal fechafin As String, ByVal valor_pap As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_InsertarParametroTolerancia", codigo_pel, abreviatura_pap, descripcion_pap, fechainicio, fechafin, valor_pap)
        cnx.CerrarConexion()
        If dts.Rows.Count > 0 Then
            Return dts.Rows(0).Item("Mensaje").ToString
        Else
            Return ""
        End If
    End Function

    '--
    Public Sub EliminarParametroTolerancia(ByVal codigo_pap As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_EliminarParametroTolerancia", codigo_pap)
        cnx.CerrarConexion()
    End Sub

    '--
    Public Function ImportarDiasNoLaborables(ByVal codigo_pel_aimportar As Integer, ByVal codigo_pel_actual As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ImportarDiasNoLaborables", codigo_pel_aimportar, codigo_pel_actual)
        cnx.CerrarConexion()
        If dts.Rows.Count > 0 Then
            Return dts.Rows(0).Item("Mensaje").ToString
        Else
            Return ""
        End If
    End Function

    '--
    Public Function ImportarPersonalExceptuado(ByVal codigo_pel_aimportar As Integer, ByVal codigo_pel_actual As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ImportarPersonalExceptuado", codigo_pel_aimportar, codigo_pel_actual)
        cnx.CerrarConexion()
        If dts.Rows.Count > 0 Then
            Return dts.Rows(0).Item("Mensaje").ToString
        Else
            Return ""
        End If
    End Function
    '--
    '------------------- 24/08/2011
    Public Function ConsultarPeriodosLaborablesDependientes(ByVal descripcion_pel As String) As DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarPeriodosLaborablesDependientes", descripcion_pel)
        cnx.CerrarConexion()
        Return dts
    End Function

    '--
    Public Function ImportarParametroTolerancia(ByVal codigo_pel_aimportar As Integer, ByVal codigo_pel_actual As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ImportarParametroTolerancia", codigo_pel_aimportar, codigo_pel_actual)
        cnx.CerrarConexion()
        If dts.Rows.Count > 0 Then
            Return dts.Rows(0).Item("Mensaje").ToString
        Else
            Return ""
        End If
    End Function

    '-------------------------- 02/09/11
    Public Function MesVigente(ByVal codigo_pel As Integer, ByVal mes As Integer, ByVal tipo As String) As DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_MesVigente", codigo_pel, mes, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    '------------------------------------ 05/09/11

    Public Function RangoFechasSemanasBitacora(ByVal Semana As Integer, ByVal mes_vigente As String, ByVal codigo_pel As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_RangoFechasSemanasBitacora", Semana, mes_vigente, codigo_pel)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ConsultarSemanasBitacora(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal mes_vigente As String) As DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarSemanasBitacora", codigo_per, codigo_pel, mes_vigente)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ValidarRegistroHorasAsTesis(ByVal codigo_pel As Integer, ByVal codigo_per As Integer, ByVal horastesis As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ValidarRegistroHorasAsTesis", codigo_pel, codigo_per, horastesis)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("resultado")
    End Function

    '---------- 26/09/2011
    Public Function FechaCierre(ByVal codigo_pel As Integer, ByVal fecha As String, ByVal tipo As String) As DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_FechaCierre", codigo_pel, fecha, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function PER_CalcularHorasSemanas(ByVal codigo_per As Integer, ByVal codigo_pel As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_CalcularHorasSemanas", codigo_per, codigo_pel, 0)
        cnx.CerrarConexion()
        Return dts
    End Function

    '------------------ 24/10/2011
    Public Function PER_ConsultarPersonal(ByVal tipo As String, ByVal nombre As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarPersonal", tipo, nombre)
        cnx.CerrarConexion()
        Return dts
    End Function

    '31/10/2011 Muestra tambien el aula para las horas de Docencia
    Public Function ConsultarVistaHorario_v3(ByVal codigo_per As Integer, ByVal codigo_pel As Integer, ByVal semana As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarHorarioPersonal_v3", codigo_per, codigo_pel, semana)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaRangoHoras(ByVal HInicio As String, ByVal HFin As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("Per_ListaRangoHoras", HInicio, HFin)
        Return dts
        cnx.CerrarConexion()
    End Function

    Public Sub ActualizarHorarioPersonal(ByVal Codigo_hop As Integer, ByVal HInicio As String, ByVal HFin As String)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_ActualizarHorarioPersonal", Codigo_hop, HInicio, HFin)
        cnx.CerrarConexion()
    End Sub


    Public Function VerificaCiclo() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("VerificaCicloVerano")
        cnx.CerrarConexion()
        Return dts
    End Function

    '17.01.2012 Verificar el estado del envio del horario

    Public Function VerificarEstadoEnvioHorario(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_VerificarEstadoEnvioHorario", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function PER_VerificaLugarPracticasExternas(ByVal codigo_per As Integer, _
                                                       ByVal codigo_pel As Integer, ByVal Semana As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_VerificaLugarPracticasExternas", codigo_per, codigo_pel, Semana)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ListaTipoPersonal() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ListaTipoPersonal")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaDedicacion() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ListaDedicacion")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargarListaPersonal(ByVal vFiltro As Integer, ByVal vEstado_hop As String, ByVal tipopersonal As String, _
                                        ByVal dedicacion As String, ByVal ceco As String, ByVal estado_Per As String, _
                                        ByVal codigo_Est As String, _
                                        ByVal envioDirector_Per As String, _
                                        ByVal envioDirPersonal_Per As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ListarTrabajadoresHorario", vFiltro, vEstado_hop, _
                                 tipopersonal, dedicacion, ceco, estado_Per, codigo_Est, _
                                 envioDirector_Per, envioDirPersonal_Per)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ListaEstadoHorario() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ListaEstadoHorarioPer")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarCentroCostosPer() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarCentroCostosPer")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CalificarHorario() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_CalificarHorario")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaEstadoPerHorario(ByVal vTipo As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ListaEstadosPerHorario", vTipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizaEstadoPersonal(ByVal vTipo As Integer, _
                                            ByVal Estado As Integer, _
                                            ByVal Codigo_per As Integer, _
                                            ByVal operador As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ActualizaEstadoPersonal", vTipo, Estado, Codigo_per, operador)
        cnx.CerrarConexion()
        Return dts
    End Function


    'Agregado 26.03.2012, debido a que cbox refrigerio etaba cargando sus registrs estaticamente. xDguevara
    Public Function ListaHorasRefrigerio() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ListaHorarioRefrigerio")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EjecutaOperacionesMasivas(ByVal vTipo As String, ByVal vCodigo_per As Integer, _
                                              ByVal EstadoEnvioDirector As Integer, ByVal EnvioDirPersonal As Integer, _
                                              ByVal EstadoPlanilla As Integer, ByVal EstadoCampus As Integer, _
                                              ByVal vOperador As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_OperacionesMasivasHorarios", vTipo, _
                                 vCodigo_per, _
                                 EstadoEnvioDirector, _
                                 EnvioDirPersonal, _
                                 EstadoPlanilla, _
                                 EstadoCampus, _
                                 vOperador)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function HorasTesisDiferentes() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_HorasTesisDiferentes")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaTesisDocente(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ListaTesisDocente", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarEstadosEnvio(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ActualizarEstadosEnvio", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function MuestraSemanaActual(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_MuestraSemanaActual", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaActividadesLeyenda() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ListaActividadesLeyenda")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ColorActividad(ByVal abreviatura As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ColorActividad", abreviatura)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaTrabajadoresCargaEnfermeria() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ListaTrabajadoresCargaEnfermeria")
        cnx.CerrarConexion()
        Return dts
    End Function

    'Actualizaciones 22.11.2012
    Public Function CargaPeridoLaboral() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PeriodoTipoActividad")
        cnx.CerrarConexion()
        Return dts
    End Function
    'Añadido x yperez 06.05.15
    Public Function CargaDptoAcad() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_lstTrabajadoresTipoActividad_dptoAcad")
        cnx.CerrarConexion()
        Return dts
    End Function
    'Modificado x yperez 06.05.15: param @area
    Public Function ListaTipoActividad(ByVal codigo_pel As Integer, ByVal codigo_per As Integer, ByVal codigo_ctf As Integer, ByVal formato As String, Optional ByVal area As String = "%") As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_lstTrabajadoresTipoActividad", codigo_pel, codigo_per, codigo_ctf, formato, area)
        cnx.CerrarConexion()
        Return dts
    End Function

    'Añadido x HCano 11.01.18 : Para Reporte distribucion de Carga Horaria
    Public Function ListaTipoActividad_v1(ByVal codigo_pel As Integer, ByVal codigo_per As Integer, ByVal codigo_ctf As Integer, ByVal formato As String, Optional ByVal area As String = "%") As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_lstTrabajadoresTipoActividad_v4", codigo_pel, codigo_per, codigo_ctf, formato, area)
        cnx.CerrarConexion()
        Return dts
    End Function


    '13.03.2013
    Public Sub ActualizarEstadoHop(ByVal codigo_per As Integer)
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PER_ActualizarEstadoHop", codigo_per)
        cnx.CerrarConexion()
    End Sub

    Public Function ConsultarDatosContrato(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("spPla_ConsultarDatosContrato", "UL", codigo_per, 0, "01/01/2014", "01/01/2014")
        cnx.CerrarConexion()
        Return dts
    End Function

    ' MNeciosup 29/08/2019 (Inicio)
    Public Function ConsultarPersonalExceptuadoHorarioAdministrativo(ByVal codigo_per As Integer) As Boolean
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarPersonalExceptuadoHorarioAdministrativo", codigo_per)
        cnx.CerrarConexion()
        If dts.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


    '-------------------------------------------------------------
    '** Metodo creado para Revision Horario. 16/09/2019 - Ahora filtra por adscripcion docente
    '--
    Public Function ConsultarPersonalDirectorDepartamento_v4(ByVal codigo_per As Integer, ByVal codigo_ctf As Integer, ByVal estado_hop As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarPersonalDirectorDepartamento_v4", codigo_per, codigo_ctf, estado_hop)
        cnx.CerrarConexion()
        Return dts
    End Function


End Class


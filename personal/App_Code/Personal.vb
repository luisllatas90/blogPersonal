'Clase Personal
'Escrita por            : Wilfredo Aljobin CUmpa
'Fecha                  : 20/10/2006
'Ultima Actualizacion   : 08/03/2007
'Observaciones          : Se utiliza para dar mantenimiento a la tabla personal
' y datos personal.

Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Diagnostics
Imports System.Web.Configuration

Public Class Personal
    Inherits System.Web.UI.Page
    Private _intIdPersonal As Integer
    Private _strConecctionString As String = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString

    Public WriteOnly Property codigo()
        Set(ByVal value)
            _intIdPersonal = value
        End Set
    End Property

    'Mostrar datos Relacionados a Personal

    Public Function ObtieneInvestigaciones(ByVal codigo_per As Integer) As DataTable
        Dim Obj As New ClsSqlServer(_strConecctionString)
        Return Obj.TraerDataTable("INV_ConsultarInvestigacionesHOjaVida", codigo_per)
    End Function
    Public Function ConsultarPublicaciones(ByVal codigo_per As Integer, Optional ByVal codigo_pub As Integer = 0) As DataTable
        Dim Obj As New ClsSqlServer(_strConecctionString)
        Return Obj.TraerDataTable("HvPersonalPublicacion_Listar", codigo_per, codigo_pub)
    End Function

    Public Function ObtieneDatosPersonales() As DataTable
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Adapter = New SqlDataAdapter("ConsultarPersonal", _strConecctionString)
        Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
        Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "PE"
        Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 1000).Value = _intIdPersonal.ToString
        Adapter.Fill(Tabla)
        ObtieneDatosPersonales = Tabla
        Adapter = Nothing
    End Function

    Public Function ObtieneDatosTitulos(ByVal idpersonal As String, ByVal tipo As String) As DataTable
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            Adapter = New SqlDataAdapter("ConsultarTituloProfesional", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = tipo
            Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = idpersonal
            Adapter.Fill(Tabla)
            Return Tabla
        Catch ex As Exception
            Return Nothing
        End Try
        Adapter = Nothing
    End Function

    Public Function ObtieneDatosGrados(ByVal idpersonal As String, ByVal tipo As String) As DataTable
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            Adapter = New SqlDataAdapter("ConsultarGradoAcademico", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = tipo
            Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = idpersonal
            Adapter.Fill(Tabla)
            Return Tabla
        Catch ex As Exception
            Return Nothing
        End Try
        Adapter = Nothing
    End Function

    Public Function ObtieneDatosIdiomas(ByVal idPersonal As String, ByVal tipo As String) As DataTable
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            Adapter = New SqlDataAdapter("ConsultarIdiomas", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = tipo
            Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = idPersonal.ToString
            Adapter.Fill(Tabla)
            Return Tabla
        Catch ex As Exception
            Return Nothing
        End Try
        Adapter = Nothing
    End Function


    Public Function VerificaDeclaracionJuradaPersonal(ByVal idPersonal As String) As DataTable
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            Adapter = New SqlDataAdapter("ConsultarDeclaracionJurada", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@codigo_per", SqlDbType.Int, 25).Value = idPersonal
            Adapter.Fill(Tabla)
            Return Tabla
        Catch ex As Exception
            Return Nothing
        End Try
        Adapter = Nothing
    End Function

    Public Function ObtieneDatosOtros(ByVal idpersonal As String, ByVal tipo As String) As DataTable
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            Adapter = New SqlDataAdapter("ConsultarOtrosEstudios", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = tipo
            Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = idpersonal
            Adapter.Fill(Tabla)
            Return Tabla
        Catch ex As Exception
            Return Nothing
        End Try
        Adapter = Nothing
    End Function

    Public Function ObtieneDatosExperiencia(ByVal idpersonal As Integer, ByVal tipo As String) As DataTable
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            Adapter = New SqlDataAdapter("ConsultarExperiencia", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = tipo
            Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = idpersonal
            Adapter.Fill(Tabla)
            Return Tabla
        Catch ex As Exception
            Return Nothing
        End Try
        Adapter = Nothing

    End Function

    Public Function ObtieneDistinciones(ByVal idpersonal As Integer, ByVal tipo As String) As DataTable
        Dim tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            Adapter = New SqlDataAdapter("ConsultarDIstinciones", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = tipo
            Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 20).Value = idpersonal.ToString
            Adapter.Fill(tabla)
            Return tabla
        Catch ex As Exception
            Return Nothing
        End Try
        Adapter = Nothing
    End Function

    Public Function ObtieneDatosAdicionales() As DataTable
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Adapter = New SqlDataAdapter("ConsultarPersonal", _strConecctionString)
        Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
        Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "AD"
        Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 1000).Value = _intIdPersonal.ToString
        Adapter.Fill(Tabla)
        Return Tabla
        Adapter = Nothing
    End Function

    Public Function ObtieneDatosEventos(ByVal tipo As String, ByVal idpersonal As String, ByVal param2 As String) As DataTable
        Dim tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Adapter = New SqlDataAdapter("ConsultarEvento", _strConecctionString)
        Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
        Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = tipo
        Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = idpersonal.ToString
        Adapter.SelectCommand.Parameters.Add("@param2", SqlDbType.VarChar, 25).Value = param2
        Adapter.SelectCommand.Parameters.Add("@param3", SqlDbType.VarChar, 25).Value = ""
        Adapter.Fill(tabla)
        Return tabla
        Adapter = Nothing
    End Function

    Public Function ObtieneLogin(ByVal param1 As String, ByVal param2 As Integer)
        If param1 Is Nothing Then
            param1 = "a"
        End If
        Dim Persona As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Return Persona.TraerDataTable("ConsultarPersonalLogin", "TO", param1.Trim, param2 )
    End Function

    Public Function ObtieneLoginDetalle(ByVal param1 As String, ByVal param2 As Integer)
        If param1 Is Nothing Or param1 = "" Then
            param1 = "1"
        End If
        Dim Persona As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Return Persona.TraerDataTable("ConsultarPersonalLogin", "PE", param1, param2)
    End Function

    Public Function ObtienePerfilPersonal(ByVal codigo_per As Integer) As String
        Dim Obj As New ClsSqlServer(_strConecctionString)
        Return Obj.TraerDataTable("ConsultarPerfilPersonal", codigo_per).Rows(0).Item(1).ToString
        Obj = Nothing
    End Function

    Public Function DocentesDepartamento(ByVal codigo_per As Integer) As DataTable
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            Adapter = New SqlDataAdapter("consultardocente", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "HT"
            Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = codigo_per.ToString
            Adapter.SelectCommand.Parameters.Add("@param2", SqlDbType.VarChar, 25).Value = ""
            Adapter.Fill(Tabla)
            Return Tabla
        Catch ex As Exception
            Return Nothing
        End Try
        Adapter = Nothing
    End Function

    Public Function DocentesDeparAcad(ByVal codigo_dac As Integer, _
                                      ByVal codigo_tpe As Integer, _
                                      ByVal estado_per As Integer) As DataTable
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            'Adapter = New SqlDataAdapter("consultardocente", _strConecctionString)
            Adapter = New SqlDataAdapter("ConsultarDocenteV2", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "DA"
            Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 25).Value = codigo_dac.ToString
            Adapter.SelectCommand.Parameters.Add("@param2", SqlDbType.VarChar, 25).Value = codigo_tpe.ToString
            Adapter.SelectCommand.Parameters.Add("@param3", SqlDbType.VarChar, 25).Value = estado_per.ToString  'para el fitro de planilla.

            Adapter.Fill(Tabla)
            Return Tabla
        Catch ex As Exception
            Return Nothing
        End Try
        Adapter = Nothing
    End Function

    Public Function DocentesAvanzada(ByVal codigo_dac As Integer, ByVal titulo As String, ByVal grado As String, _
    ByVal otros As String, ByVal idioma As String) As DataTable
        Dim tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            Adapter = New SqlDataAdapter("ConsultarHojaVida", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "TO"
            Adapter.SelectCommand.Parameters.Add("@titulo", SqlDbType.VarChar, 80).Value = titulo
            Adapter.SelectCommand.Parameters.Add("@grado", SqlDbType.VarChar, 80).Value = grado
            Adapter.SelectCommand.Parameters.Add("@estudios", SqlDbType.VarChar, 80).Value = otros
            Adapter.SelectCommand.Parameters.Add("@idiomas", SqlDbType.VarChar, 2).Value = idioma
            Adapter.SelectCommand.Parameters.Add("@codigo_dac", SqlDbType.Int).Value = codigo_dac
            Adapter.Fill(tabla)
            Return tabla
        Catch ex As Exception
            Return Nothing
        End Try
        Adapter = Nothing
    End Function

    Public Function DocentesAvanzada2(ByVal codigo_dac As Integer, ByVal titulo As String, ByVal grado As String, _
    ByVal otros As String, ByVal idioma As String, ByVal estado_per As Integer) As DataTable
        Dim tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Try
            Adapter = New SqlDataAdapter("ConsultarHojaVida2", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = "TO"
            Adapter.SelectCommand.Parameters.Add("@titulo", SqlDbType.VarChar, 80).Value = titulo
            Adapter.SelectCommand.Parameters.Add("@grado", SqlDbType.VarChar, 80).Value = grado
            Adapter.SelectCommand.Parameters.Add("@estudios", SqlDbType.VarChar, 80).Value = otros
            Adapter.SelectCommand.Parameters.Add("@idiomas", SqlDbType.VarChar, 2).Value = idioma
            Adapter.SelectCommand.Parameters.Add("@estado_per", SqlDbType.VarChar, 25).Value = estado_per.ToString 'para el fitro de estado de planilla
            Adapter.SelectCommand.Parameters.Add("@codigo_dac", SqlDbType.Int).Value = codigo_dac
            Adapter.Fill(tabla)
            Return tabla
        Catch ex As Exception
            Return Nothing
        End Try
        Adapter = Nothing
    End Function
    'Ingresar Datos de Personal
    Public Function GrabaDatosPersonales(ByVal strPaterno As String, _
                                         ByVal strSexo As String, _
                                         ByVal dateFecha As String, _
                                         ByVal strNacio As String, _
                                         ByVal strCivil As String, _
                                         ByVal strReligion As String, _
                                         ByVal strSacra As String, _
                                         ByVal strSangre As String, _
                                         ByVal codProv As Integer, _
                                         ByVal strDistrito As String, _
                                         ByVal strDireccion As String, _
                                         ByVal strTelcasa As String, _
                                         ByVal strTelCel As String, _
                                         ByVal strTelTrabajo As String, _
                                         ByVal strMail1 As String, _
                                         ByVal strMail2 As String, _
                                         ByVal strEmerNom As String, _
                                         ByVal strEmerDir As String, _
                                         ByVal strEmerTele As String, _
                                         ByVal Foto As FileUpload, _
                                         ByVal ruc As String, _
                                         ByVal numerohijos As Integer, _
                                         ByVal codigo_nive As String, _
                                         ByVal domicilioUbiGeo_Per As Integer, _
                                         ByVal domicilioUbiGeo2_Per As Integer, _
                                         ByVal codigo_tvia As String, _
                                         ByVal codigo2_tvia As String, _
                                         ByVal nombreVia_Per As String, _
                                         ByVal nombreVia2_Per As String, _
                                         ByVal numeroVia_Per As String, _
                                         ByVal numeroVia2_Per As String, _
                                         ByVal interiorVia_Per As String, _
                                         ByVal interiorVia2_Per As String, _
                                         ByVal referenciaESSALUD_Per As Integer, _
                                         ByVal referenciaESSALUD2_Per As Integer, _
                                         ByVal nombreZona_Per As String, _
                                         ByVal nombreZona2_Per As String, _
                                         ByVal referenciaZona_Per As String, _
                                         ByVal referenciaZona2_Per As String, _
                                         ByVal codigo_Efi As String, _
                                         ByVal codigoEfi_CTS As String, _
                                         ByVal nroCuenta_Per As String, _
                                         ByVal nroCuentaCTS_Per As String, _
                                         ByVal monedaCta_Per As String, _
                                         ByVal monedaCtaCTS_Per As String, _
                                         ByVal tipoCuenta_Per As String, _
                                         ByVal codigo_Rsa As Integer, _
                                         ByVal cuspp_Per As String, _
                                         ByVal codigo_EPS As String, _
                                         ByVal SCTRSalud_Per As String, _
                                         ByVal RegPensionario_Per As String, _
                                         ByVal SituacionEPS_Per As String, _
                                         ByVal FechaInscripcionRegPen_Per As Date, _
                                         ByVal fechaIni_Per As Date, _
                                         ByVal zona1 As String, _
                                         ByVal zona2 As String, _
                                         Optional ByVal operador_internet As String = "", _
                                         Optional ByVal operador_celular As String = "") As Integer

        Dim Cnx As New SqlConnection
        Dim strSQL As String = Nothing

        Dim path As String = Server.MapPath("../../imgpersonal/")
        Dim fileOK As Boolean = False
        Dim strFoto As String = "n"  'Doy el valor de n y validar que no se ingrese fotografia en el procedimiento
        Dim fileExtension As String = Nothing

        ' Aqui valido si existe la imagen que se sube
        If Foto.HasFile Then
            fileExtension = System.IO.Path.GetExtension(Foto.FileName).ToLower()
            'Return Foto.PostedFile.ContentLength

            Dim allowedExtensions As String() = {".jpg", ".jpeg", ".png", ".gif"}

            'Valido las extensiones de las imagenes que se suben
            For i As Integer = 0 To allowedExtensions.Length - 1
                If fileExtension = allowedExtensions(i) Then
                    fileOK = True
                End If
            Next

            If Foto.PostedFile.ContentLength > 61440 Then
                Return -3 'Aqui digo que la foto tiene mas de 60 Kb
                Exit Function
            ElseIf fileOK Then
                strFoto = _intIdPersonal.ToString & fileExtension
                Foto.PostedFile.SaveAs(path & strFoto)
            Else

                Return -2 'Aqui digo que la extension de la foto no es la correcta
                Exit Function
            End If
        End If


        Try
            Cnx.ConnectionString = _strConecctionString
            Cnx.Open()
            'Llamo al procedimiento para actualizar personal
            Dim UpdCommand As New SqlCommand("pa_personal_actualiza", Cnx)
            UpdCommand.CommandType = CommandType.StoredProcedure
            UpdCommand.Parameters.Add("@intIdPersona", SqlDbType.Int).Value = _intIdPersonal
            UpdCommand.Parameters.Add("@strSexo", SqlDbType.VarChar, 1).Value = strSexo
            UpdCommand.Parameters.Add("@datefecha", SqlDbType.DateTime).Value = dateFecha
            UpdCommand.Parameters.Add("@strNacio", SqlDbType.VarChar, 20).Value = strNacio
            UpdCommand.Parameters.Add("@strCivil", SqlDbType.VarChar, 20).Value = strCivil
            UpdCommand.Parameters.Add("@strReligion", SqlDbType.VarChar, 50).Value = strReligion
            UpdCommand.Parameters.Add("@strSacra", SqlDbType.VarChar, 20).Value = strSacra
            UpdCommand.Parameters.Add("@strSangre", SqlDbType.VarChar, 8).Value = strSangre
            UpdCommand.Parameters.Add("@CodPro", SqlDbType.Int).Value = codProv
            UpdCommand.Parameters.Add("@strDistrito", SqlDbType.VarChar, 15).Value = strDistrito
            UpdCommand.Parameters.Add("@strDireccion", SqlDbType.VarChar, 80).Value = strDireccion
            UpdCommand.Parameters.Add("@strTelCasa", SqlDbType.VarChar, 30).Value = strTelcasa
            UpdCommand.Parameters.Add("@strTelCel", SqlDbType.VarChar, 30).Value = strTelCel
            UpdCommand.Parameters.Add("@strTelTrabajo", SqlDbType.VarChar, 30).Value = strTelTrabajo
            UpdCommand.Parameters.Add("@strMail1", SqlDbType.VarChar, 30).Value = strMail1
            UpdCommand.Parameters.Add("@strMail2", SqlDbType.VarChar, 80).Value = strMail2
            UpdCommand.Parameters.Add("@strEmernOm", SqlDbType.VarChar, 80).Value = strEmerNom
            UpdCommand.Parameters.Add("@strEmerDir", SqlDbType.VarChar, 80).Value = strEmerDir
            UpdCommand.Parameters.Add("@strEmerTele", SqlDbType.VarChar, 30).Value = strEmerTele

            '--------------------------------------------------------------------------------------------
            UpdCommand.Parameters.Add("@Foto", SqlDbType.VarChar, 80).Value = strFoto
            '--------------------------------------------------------------------------------------------

            'Agregado xDaguevara 03.08.2012
            UpdCommand.Parameters.Add("@RUCTrabajador", SqlDbType.VarChar, 11).Value = ruc
            UpdCommand.Parameters.Add("@numeroHijos_Per", SqlDbType.Int).Value = numerohijos
            UpdCommand.Parameters.Add("@codigo_nive", SqlDbType.VarChar, 15).Value = codigo_nive
            UpdCommand.Parameters.Add("@domicilioUbiGeo_Per", SqlDbType.Int).Value = domicilioUbiGeo_Per
            UpdCommand.Parameters.Add("@domicilioUbiGeo2_Per", SqlDbType.Int).Value = domicilioUbiGeo2_Per
            UpdCommand.Parameters.Add("@codigo_tvia", SqlDbType.VarChar, 10).Value = codigo_tvia
            UpdCommand.Parameters.Add("@codigo2_tvia", SqlDbType.VarChar, 10).Value = codigo2_tvia

            UpdCommand.Parameters.Add("@nombreVia_Per", SqlDbType.VarChar, 400).Value = nombreVia_Per
            UpdCommand.Parameters.Add("@nombreVia2_Per", SqlDbType.VarChar, 400).Value = nombreVia2_Per

            UpdCommand.Parameters.Add("@numeroVia_Per", SqlDbType.VarChar, 400).Value = numeroVia_Per
            UpdCommand.Parameters.Add("@numeroVia2_Per", SqlDbType.VarChar, 400).Value = numeroVia2_Per

            UpdCommand.Parameters.Add("@interiorVia_Per", SqlDbType.VarChar, 400).Value = interiorVia_Per
            UpdCommand.Parameters.Add("@interiorVia2_Per", SqlDbType.VarChar, 400).Value = interiorVia2_Per

            UpdCommand.Parameters.Add("@referenciaESSALUD_Per", SqlDbType.Int).Value = referenciaESSALUD_Per
            UpdCommand.Parameters.Add("@referenciaESSALUD2_Per", SqlDbType.Int).Value = referenciaESSALUD2_Per

            UpdCommand.Parameters.Add("@nombreZona_Per", SqlDbType.VarChar, 400).Value = nombreZona_Per
            UpdCommand.Parameters.Add("@nombreZona2_Per", SqlDbType.VarChar, 400).Value = nombreZona2_Per

            UpdCommand.Parameters.Add("@referenciaZona_Per", SqlDbType.VarChar, 400).Value = referenciaZona_Per
            UpdCommand.Parameters.Add("@referenciaZona2_Per", SqlDbType.VarChar, 400).Value = referenciaZona2_Per

            UpdCommand.Parameters.Add("@codigo_Efi", SqlDbType.VarChar, 400).Value = codigo_Efi
            UpdCommand.Parameters.Add("@codigoEfi_CTS", SqlDbType.VarChar, 400).Value = codigoEfi_CTS

            UpdCommand.Parameters.Add("@nroCuenta_Per", SqlDbType.VarChar, 400).Value = nroCuenta_Per
            UpdCommand.Parameters.Add("@nroCuentaCTS_Per", SqlDbType.VarChar, 400).Value = nroCuentaCTS_Per

            UpdCommand.Parameters.Add("@monedaCta_Per", SqlDbType.VarChar, 400).Value = monedaCta_Per
            UpdCommand.Parameters.Add("@monedaCtaCTS_Per", SqlDbType.VarChar, 400).Value = monedaCtaCTS_Per

            UpdCommand.Parameters.Add("@tipoCuenta_Per", SqlDbType.VarChar, 10).Value = tipoCuenta_Per
            UpdCommand.Parameters.Add("@codigo_Rsa", SqlDbType.Int).Value = codigo_Rsa

            UpdCommand.Parameters.Add("@cuspp_Per", SqlDbType.VarChar, 300).Value = cuspp_Per

            UpdCommand.Parameters.Add("@codigo_EPS", SqlDbType.VarChar, 300).Value = codigo_EPS
            UpdCommand.Parameters.Add("@SCTRSalud_Per", SqlDbType.VarChar, 300).Value = SCTRSalud_Per
            UpdCommand.Parameters.Add("@RegPensionario_Per", SqlDbType.VarChar, 300).Value = RegPensionario_Per
            UpdCommand.Parameters.Add("@SituacionEPS_Per", SqlDbType.VarChar, 300).Value = SituacionEPS_Per
            UpdCommand.Parameters.Add("@FechaInscripcionRegPen_Per", SqlDbType.SmallDateTime).Value = FechaInscripcionRegPen_Per
            UpdCommand.Parameters.Add("@fechaIni_Per", SqlDbType.SmallDateTime).Value = fechaIni_Per

            UpdCommand.Parameters.Add("@zona1", SqlDbType.VarChar, 10).Value = zona1
            UpdCommand.Parameters.Add("@zona2", SqlDbType.VarChar, 10).Value = zona2

            UpdCommand.Parameters.Add("@operadorInternet_Per", SqlDbType.VarChar, 200).Value = operador_internet
            UpdCommand.Parameters.Add("@operadorCelular_Per", SqlDbType.VarChar, 200).Value = operador_celular

            'Ejecutamos.
            GrabaDatosPersonales = UpdCommand.ExecuteNonQuery()
        Catch ex As Exception
            GrabaDatosPersonales = -1       'Digo que existe un fallo en el procedimiento almacendao
        End Try
        If Cnx.State = ConnectionState.Open Then
            Cnx.Close()
            Cnx = Nothing
        End If
    End Function

    Public Function GrabaPDP(ByVal Foto As FileUpload) As Integer
        Dim Cnx As New SqlConnection
        Dim strSQL As String = Nothing
        Dim path As String = Server.MapPath("../../pdp/")
        Dim fileOK As Boolean = False
        Dim strFoto As String = "n"  'Doy el valor de n y validar que no se ingrese fotografia en el procedimiento
        Dim fileExtension As String = Nothing

        ' Aqui valido si existe la imagen que se sube
        If Foto.HasFile = True Then

            Dim allowedExtensions As String() = {".doc", ".docx", ".pdf"}
            fileExtension = System.IO.Path.GetExtension(Foto.FileName).ToLower()
            'Return Foto.PostedFile.ContentLength

            'Valido las extensiones de las imagenes que se suben
            For i As Integer = 0 To allowedExtensions.Length - 1
                If fileExtension.ToLower = allowedExtensions(i).ToLower Then
                    fileOK = True
                End If
            Next
            '1048576 1MB
            If Foto.PostedFile.ContentLength > (1052905) Then
                Return -3 'Aqui digo que la foto tiene mas de 1000 Kb - 1 Mb
                Exit Function
            ElseIf fileOK Then
                strFoto = _intIdPersonal.ToString & fileExtension
                Foto.PostedFile.SaveAs(path & strFoto)

            Else
                Return -2 'Aqui digo que la extension de la foto no es la correcta
                Exit Function
            End If
        End If
        Try
            Cnx.ConnectionString = _strConecctionString
            Cnx.Open()
            'Llamo al procedimiento para actualizar personal
            Dim UpdCommand As New SqlCommand("pa_personal_pdp", Cnx)
            UpdCommand.CommandType = CommandType.StoredProcedure
            UpdCommand.Parameters.Add("@intIdPersona", SqlDbType.Int).Value = _intIdPersonal
            UpdCommand.Parameters.Add("@pdp", SqlDbType.VarChar, 150).Value = strFoto
            GrabaPDP = UpdCommand.ExecuteNonQuery()
            Return 10 ' si está ok
        Catch ex As Exception
            GrabaPDP = -1       'Digo que existe un fallo en el procedimiento almacendao
        End Try
        If Cnx.State = ConnectionState.Open Then
            Cnx.Close()
            Cnx = Nothing
        End If
    End Function

    Public Function GrabarPerfilPersonal(ByVal codigo_per As Integer, ByVal perfil_per As String) As Integer
        Dim obj As New ClsSqlServer(_strConecctionString)
        Try
            obj.IniciarTransaccion()
            obj.Ejecutar("AgregarPerfilPersonal", codigo_per, perfil_per)
            obj.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            obj.AbortarTransaccion()
            Return -1
        End Try

    End Function

    Public Function ActualizarEstadoDeclaracionJurada(ByVal codigo_per As Integer) As Integer
        Dim obj As New ClsSqlServer(_strConecctionString)
        Try
            obj.IniciarTransaccion()
            obj.Ejecutar("ActualizarEstadoDeclaracionJurada", codigo_per)
            obj.TerminarTransaccion()
            Return 1
        Catch ex As Exception
            obj.AbortarTransaccion()
            Return -1
        End Try

    End Function

    Public Function GrabarTitulos(ByVal codigo_tpf As Integer, ByVal nombretitulo As String, _
    ByVal anioingreso_tpr As Integer, ByVal anioegreso_tpr As Integer, _
    ByVal aniograd_tpr As Integer, ByVal universidad_tpr As String, ByVal codigo_sit As Integer, _
    ByVal codigo_ins As Integer, ByVal codmod As Integer) As Integer

        Dim cnx As New SqlConnection
        Dim InsCommand As SqlCommand
        Try
            cnx.ConnectionString = _strConecctionString
            cnx.Open()
            'Llamo al procedimiento para agregar titulos
            InsCommand = New SqlCommand("Agregartituloprofesor", cnx)
            With InsCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@codigo_per", SqlDbType.Int).Value = _intIdPersonal
                .Parameters.Add("@codigo_tpf", SqlDbType.Int).Value = codigo_tpf
                .Parameters.Add("@NombreTitulo_TPr", SqlDbType.VarChar, 300).Value = nombretitulo
                .Parameters.Add("@anioingreso_tpr", SqlDbType.Int).Value = anioingreso_tpr
                .Parameters.Add("@anioegreso_tpr", SqlDbType.Int).Value = anioegreso_tpr
                .Parameters.Add("@aniograd_tpr", SqlDbType.Int).Value = aniograd_tpr
                .Parameters.Add("@universidad_tpr", SqlDbType.VarChar, 150).Value = universidad_tpr
                .Parameters.Add("@codigo_sit", SqlDbType.Int).Value = codigo_sit
                .Parameters.Add("@codigo_ins", SqlDbType.Int).Value = codigo_ins
                .Parameters.Add("@codigo_mod", SqlDbType.Int).Value = codmod    'add xdguevara 04.10.2013.
                GrabarTitulos = .ExecuteNonQuery
            End With
        Catch ex As Exception
            GrabarTitulos = -1
        End Try
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If
    End Function

    Public Function GrabarGrados(ByVal codigo_gra As Integer, ByVal desgrado As String, ByVal anioingreso_gpr As Integer, _
    ByVal anioegreso_gpr As Integer, ByVal aniograd_gpr As Integer, ByVal mencion_gpr As String, ByVal universidad_gpr As String, _
    ByVal codigo_sit As Integer, ByVal codigo_ins As Integer, ByVal codmod As Integer) As Integer

        Dim cnx As New SqlConnection
        Dim InsCommand As SqlCommand
        Try
            cnx.ConnectionString = _strConecctionString
            cnx.Open()
            InsCommand = New SqlCommand("Agregargradosprofesor", cnx)
            With InsCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@codigo_gra", SqlDbType.Int).Value = codigo_gra
                .Parameters.Add("@desgrado_gpr", SqlDbType.VarChar, 300).Value = desgrado
                .Parameters.Add("@codigo_per", SqlDbType.Int).Value = _intIdPersonal
                .Parameters.Add("@anioingreso_gpr", SqlDbType.Int).Value = anioingreso_gpr
                .Parameters.Add("@anioegreso_gpr", SqlDbType.Int).Value = anioegreso_gpr
                .Parameters.Add("@aniograd_gpr", SqlDbType.Int).Value = aniograd_gpr
                .Parameters.Add("@mencion_gpr", SqlDbType.VarChar, 300).Value = mencion_gpr
                .Parameters.Add("@universidad_gpr", SqlDbType.VarChar, 150).Value = universidad_gpr
                .Parameters.Add("@codigo_sit", SqlDbType.Int).Value = codigo_sit
                .Parameters.Add("@codigo_ins", SqlDbType.Int).Value = codigo_ins
                .Parameters.Add("@codigo_mod", SqlDbType.Int).Value = codmod        'add 04.10.2013 dguevara
                GrabarGrados = .ExecuteNonQuery
            End With

        Catch ex As Exception
            GrabarGrados = -1
        End Try
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If
    End Function

    Public Function GrabarIdiomas(ByVal codigo_sit As Integer, ByVal codigo_idi As Integer, ByVal centroestudios As String, ByVal aniograduacion As Integer, _
    ByVal observaciones As String, ByVal lee As String, ByVal habla As String, ByVal escribe As String, ByVal codigo_ins As Integer) As Integer
        Dim cnx As New SqlConnection
        Dim InsCommand As New SqlCommand
        Try
            cnx.ConnectionString = _strConecctionString
            cnx.Open()
            InsCommand = New SqlCommand("Agregaridiomasprofesor", cnx)
            With InsCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@codigo_sit", SqlDbType.TinyInt).Value = codigo_sit
                .Parameters.Add("@codigo_idi", SqlDbType.TinyInt).Value = codigo_idi
                .Parameters.Add("@centroestudios", SqlDbType.VarChar, 300).Value = centroestudios
                .Parameters.Add("@aniograduacion", SqlDbType.Int).Value = aniograduacion
                .Parameters.Add("@observaciones", SqlDbType.VarChar, 300).Value = observaciones
                .Parameters.Add("@lee", SqlDbType.Char, 2).Value = lee
                .Parameters.Add("@habla", SqlDbType.Char, 2).Value = habla
                .Parameters.Add("@escribe", SqlDbType.Char, 2).Value = escribe
                .Parameters.Add("@codigo_ins", SqlDbType.Int).Value = codigo_ins
                .Parameters.Add("@codigo_per", SqlDbType.Int).Value = _intIdPersonal
                GrabarIdiomas = .ExecuteNonQuery
            End With
        Catch ex As Exception
            GrabarIdiomas = -1
        End Try
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If
    End Function

    Public Function GrabarOtros(ByVal codigo_areaes As Integer, _
                                ByVal codigo_ins As Integer, _
                                ByVal nombre_ins As String, _
                                ByVal nombre_est As String, _
                                ByVal mes_inicio As String, _
                                ByVal anio_inicio As Integer, _
                                ByVal mes_fin As String, _
                                ByVal anio_fin As Integer, _
                                ByVal tipo_mod As Integer, _
                                ByVal actual_estudio As Integer, _
                                ByVal observacion As String, _
                                ByVal intSituacion As Integer) As Integer
        Dim cnx As New SqlConnection
        Dim InsCommand As New SqlCommand
        Try
            cnx.ConnectionString = _strConecctionString
            cnx.Open()
            InsCommand = New SqlCommand("InsertarOtrosestudios", cnx)
            With InsCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@codigo_areaes", SqlDbType.TinyInt).Value = codigo_areaes
                .Parameters.Add("@codigo_ins", SqlDbType.TinyInt).Value = codigo_ins
                .Parameters.Add("@nombre_ins", SqlDbType.VarChar, 300).Value = nombre_ins ' "nombre_ins" 'nombre_ins
                .Parameters.Add("@nombre_est", SqlDbType.VarChar, 300).Value = nombre_est
                .Parameters.Add("@mes_inicio", SqlDbType.VarChar, 12).Value = mes_inicio
                .Parameters.Add("@anio_inicio", SqlDbType.SmallInt).Value = anio_inicio
                .Parameters.Add("@mes_fin", SqlDbType.VarChar, 12).Value = mes_fin
                .Parameters.Add("@anio_fin", SqlDbType.SmallInt).Value = anio_fin
                .Parameters.Add("@tipo_mod", SqlDbType.SmallInt).Value = tipo_mod
                .Parameters.Add("@actual_estudio", SqlDbType.SmallInt).Value = actual_estudio
                .Parameters.Add("@codigo_per", SqlDbType.Int).Value = _intIdPersonal
                .Parameters.Add("@observacion", SqlDbType.VarChar, 300).Value = observacion
                .Parameters.Add("@codigo_sit", SqlDbType.TinyInt).Value = intSituacion
                GrabarOtros = .ExecuteNonQuery
            End With
        Catch ex As Exception
            GrabarOtros = -1
        End Try
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If

    End Function

    Public Function GrabarExperiencia(ByVal codigo_car As Integer, ByVal funcion_exp As String, ByVal fechainicio_exp As Date, ByVal fechafin_exp As Date, _
    ByVal descripcion_exp As String, ByVal codigo_tco As Integer, ByVal motivocese As String, ByVal ciudad As String, ByVal empresa As String, ByVal tipo_exp As String) As Integer
        Dim cnx As New SqlConnection
        Dim InsCommand As New SqlCommand
        Try
            cnx.ConnectionString = _strConecctionString
            cnx.Open()
            InsCommand = New SqlCommand("InsertarExperiencia", cnx)
            With InsCommand

                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@codigo_car", SqlDbType.Int).Value = codigo_car
                .Parameters.Add("@codigo_per", SqlDbType.Int).Value = _intIdPersonal
                .Parameters.Add("@funcion_exp", SqlDbType.VarChar, 300).Value = funcion_exp
                .Parameters.Add("@fechainicio_exp", SqlDbType.SmallDateTime).Value = fechainicio_exp
                .Parameters.Add("@fechafin_exp", SqlDbType.SmallDateTime).Value = fechafin_exp
                .Parameters.Add("@descripcion_exp", SqlDbType.VarChar, 800).Value = descripcion_exp
                .Parameters.Add("@codigo_tco", SqlDbType.Int).Value = codigo_tco
                .Parameters.Add("@motivocese", SqlDbType.VarChar, 20).Value = motivocese
                .Parameters.Add("@ciudad", SqlDbType.VarChar, 40).Value = ciudad
                .Parameters.Add("@empresa", SqlDbType.VarChar, 300).Value = empresa
                '** tipo experciencia *** -> EL:Exp.Laboral
                .Parameters.Add("@tipo_exp", SqlDbType.Char, 2).Value = tipo_exp
                GrabarExperiencia = .ExecuteNonQuery

            End With
        Catch ex As Exception
            GrabarExperiencia = -1
        End Try
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If
    End Function

    Public Function GrabarDistinciones(ByVal nombre_dis As String, ByVal otorgado_dis As String, ByVal ciudad_dis As String, _
    ByVal motivo_dis As String, ByVal codigo_tdis As Integer, ByVal fechaentrega As DateTime) As Integer
        Dim Cnx As New SqlConnection
        Dim InsCommand As SqlCommand
        Try
            Cnx.ConnectionString = _strConecctionString
            Cnx.Open()
            InsCommand = New SqlCommand("InsertarDistinciones", Cnx)
            With InsCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@codigo_per", SqlDbType.Int).Value = _intIdPersonal
                .Parameters.Add("@nombre_dis", SqlDbType.VarChar, 300).Value = nombre_dis
                .Parameters.Add("@otorgado_dis", SqlDbType.VarChar, 300).Value = otorgado_dis
                .Parameters.Add("@ciudad_dis", SqlDbType.VarChar, 50).Value = ciudad_dis
                .Parameters.Add("@motivo_dis", SqlDbType.VarChar, 800).Value = motivo_dis
                .Parameters.Add("@codigo_tdis", SqlDbType.Int).Value = codigo_tdis
                .Parameters.Add("@fechaentrega", SqlDbType.DateTime).Value = fechaentrega
                GrabarDistinciones = .ExecuteNonQuery
            End With
        Catch ex As Exception
            GrabarDistinciones = -1
        End Try
        If Cnx.State = ConnectionState.Open Then
            Cnx.Close()
            Cnx = Nothing
        End If
    End Function
    Public Function GrabarPublicaciones(ByVal nombre As String, ByVal editorial As String, ByVal procedencia As String, ByVal autoria As String, ByVal tipo As String, ByVal info As String) As Integer
        Dim Cnx As New SqlConnection
        Dim InsCommand As SqlCommand
        Try
            Cnx.ConnectionString = _strConecctionString
            Cnx.Open()
            InsCommand = New SqlCommand("HvPersonalPublicacion_Insertar", Cnx)
            With InsCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@codigo_per", SqlDbType.Int).Value = _intIdPersonal
                .Parameters.Add("@nombre", SqlDbType.VarChar, 800).Value = nombre
                .Parameters.Add("@editorial", SqlDbType.VarChar, 800).Value = editorial
                .Parameters.Add("@procedencia", SqlDbType.Char, 1).Value = procedencia
                .Parameters.Add("@autoria", SqlDbType.Char, 1).Value = autoria
                .Parameters.Add("@tipo", SqlDbType.Char, 2).Value = tipo
                .Parameters.Add("@info", SqlDbType.Char, 1).Value = info
                GrabarPublicaciones = .ExecuteNonQuery
            End With
        Catch ex As Exception
            GrabarPublicaciones = -1
        End Try
        If Cnx.State = ConnectionState.Open Then
            Cnx.Close()
            Cnx = Nothing
        End If
    End Function
    Public Function GrabarAdicionales(ByVal descripcion As String, ByVal habilidades As String, ByVal limitaciones As String, ByVal hobbies As String) As Integer
        Dim cnx As New SqlConnection
        Dim InsCommand As New SqlCommand
        Dim valor As Integer
        Try
            cnx.ConnectionString = _strConecctionString
            cnx.Open()
            InsCommand = New SqlCommand("pa_personal_actualiza_adicionales", cnx)
            With InsCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@codigo_per", SqlDbType.Int).Value = _intIdPersonal
                .Parameters.Add("@descripcion", SqlDbType.Text).Value = descripcion
                .Parameters.Add("@habilidades", SqlDbType.Text).Value = habilidades
                .Parameters.Add("@limitaciones", SqlDbType.Text).Value = limitaciones
                .Parameters.Add("@hobbies", SqlDbType.Text).Value = hobbies
                .Parameters.Add("@valor", SqlDbType.Int).Direction = ParameterDirection.InputOutput
                .Parameters("@valor").Value = valor
                .ExecuteNonQuery()
                valor = .Parameters("@valor").Value
                GrabarAdicionales = valor
            End With
        Catch ex As Exception
            GrabarAdicionales = -1
        End Try
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If
    End Function

    Public Function GrabarEventos(ByVal tipoInsertar As String, ByVal Descripcion As String, _
    ByVal inicio As Date, ByVal fin As Date, ByVal clase As Integer, ByVal tipoEven As Integer, _
    ByVal organizado As String, ByVal duracion As Integer, ByVal tipoduracion As Integer, _
    ByVal TipoParticipacion As String, ByVal codigo_evento As Integer, ByVal strProcencia As String) As Integer

        Dim Cnx As New SqlConnection
        Dim InsCmd As New SqlCommand

        Try
            Cnx.ConnectionString = _strConecctionString
            Cnx.Open()
            InsCmd = New SqlCommand("AgregarEvento", Cnx)
            InsCmd.CommandType = CommandType.StoredProcedure
            InsCmd.Parameters.Add("@tipo", SqlDbType.VarChar, 2).Value = tipoInsertar
            InsCmd.Parameters.Add("@descripcion_eve", SqlDbType.VarChar, 300).Value = Descripcion
            InsCmd.Parameters.Add("@fechaini", SqlDbType.DateTime).Value = inicio
            InsCmd.Parameters.Add("@fechafin", SqlDbType.DateTime).Value = fin
            InsCmd.Parameters.Add("@codigo_cev", SqlDbType.Int).Value = clase
            InsCmd.Parameters.Add("@codigo_tev", SqlDbType.Int).Value = tipoEven
            InsCmd.Parameters.Add("@organizado", SqlDbType.VarChar, 300).Value = organizado
            InsCmd.Parameters.Add("@duracion", SqlDbType.Int).Value = duracion
            InsCmd.Parameters.Add("@tipoduracion", SqlDbType.Int).Value = tipoduracion
            InsCmd.Parameters.Add("@codigo_per", SqlDbType.Int).Value = _intIdPersonal
            InsCmd.Parameters.Add("@strparticipacion", SqlDbType.VarChar, 50).Value = TipoParticipacion
            InsCmd.Parameters.Add("@codigo_evento", SqlDbType.Int).Value = codigo_evento
            InsCmd.Parameters.Add("@strProcencia", SqlDbType.VarChar, 1).Value = strProcencia   'add dguevara 04.11.2013 
            InsCmd.ExecuteNonQuery()
            GrabarEventos = 1
        Catch ex As Exception
            GrabarEventos = -1
        End Try
        If Cnx.State = ConnectionState.Open Then
            Cnx.Close()
            Cnx = Nothing
        End If

    End Function


    'Eliminar Datos de Personal
    Public Sub QuitarTitulos(ByVal CodTitPro As Integer)
        Dim Cnx As New SqlConnection
        Dim DelCommand As SqlCommand
        Dim strSQL As String
        Try
            strSQL = "Eliminartituloprofesor"
            Cnx.ConnectionString = _strConecctionString
            Cnx.Open()
            DelCommand = New SqlCommand(strSQL, Cnx)
            DelCommand.CommandType = CommandType.StoredProcedure
            DelCommand.Parameters.Add("@codigo_tpr", SqlDbType.Int).Value = CodTitPro
            DelCommand.ExecuteNonQuery()
        Catch ex As Exception

        End Try
        If Cnx.State = ConnectionState.Open Then
            Cnx.Close()
            Cnx = Nothing
        End If
    End Sub

    Public Sub QuitarGrados(ByVal CodGrado As Integer)
        Dim Cnx As New SqlConnection
        Dim DelCommand As New SqlCommand
        Dim strSQL As String
        strSQL = "Eliminargradosprofesor"
        Try
            Cnx.ConnectionString = _strConecctionString
            Cnx.Open()
            DelCommand = New SqlCommand(strSQL, Cnx)
            DelCommand.CommandType = CommandType.StoredProcedure
            DelCommand.Parameters.Add("@codigo_gpr", SqlDbType.Int).Value = CodGrado
            DelCommand.ExecuteNonQuery()
        Catch ex As Exception

        End Try
        If Cnx.State = ConnectionState.Open Then
            Cnx.Close()
            Cnx = Nothing
        End If
    End Sub

    Public Sub Quitaridiomas(ByVal codIdioma As Integer)
        Dim cnx As New SqlConnection
        Dim DelCommand As New SqlCommand
        Try
            cnx.ConnectionString = _strConecctionString
            cnx.Open()
            DelCommand = New SqlCommand("Eliminaridiomasprofesor", cnx)
            DelCommand.CommandType = CommandType.StoredProcedure
            DelCommand.Parameters.Add("@codigo_ipr", SqlDbType.Int).Value = codIdioma
            DelCommand.ExecuteNonQuery()
        Catch ex As Exception

        End Try
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If
    End Sub

    Public Sub QuitarOtros(ByVal codotros As Integer)
        Dim cnx As New SqlConnection
        Dim DelCommand As New SqlCommand
        Try
            cnx.ConnectionString = _strConecctionString
            cnx.Open()
            DelCommand = New SqlCommand("EliminarOtrosEstudios", cnx)
            DelCommand.CommandType = CommandType.StoredProcedure
            DelCommand.Parameters.Add("@param1", SqlDbType.Int).Value = codotros
            DelCommand.ExecuteNonQuery()
        Catch ex As Exception
        End Try
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If

    End Sub

    Public Sub QuitarExperiencia(ByVal codexperiencia As Integer)
        Dim cnx As New SqlConnection
        Dim DelCommand As New SqlCommand
        Try
            cnx.ConnectionString = _strConecctionString
            cnx.Open()
            DelCommand = New SqlCommand("EliminarExperienciaProf", cnx)
            DelCommand.CommandType = CommandType.StoredProcedure
            DelCommand.Parameters.Add("@codigo_exp", SqlDbType.Int).Value = codexperiencia
            DelCommand.ExecuteNonQuery()
        Catch ex As Exception
        End Try
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If

    End Sub

    Public Sub QuitarEventos(ByVal CodEvenPro As Integer, ByVal tipoeven As Integer)
        Dim Cnx As New SqlConnection
        Dim DelCommand As SqlCommand
        Dim tipo As String
        Dim strSQL As String
        If tipoeven = 1 Then
            tipo = "EA"
        Else
            tipo = "ES"
        End If
        Try
            strSQL = "EliminarEvento"
            Cnx.ConnectionString = _strConecctionString
            Cnx.Open()
            DelCommand = New SqlCommand(strSQL, Cnx)
            DelCommand.CommandType = CommandType.StoredProcedure
            DelCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = tipo
            DelCommand.Parameters.Add("@param1", SqlDbType.Int).Value = CodEvenPro
            DelCommand.ExecuteNonQuery()
        Catch ex As Exception

        End Try
        If Cnx.State = ConnectionState.Open Then
            Cnx.Close()
            Cnx = Nothing
        End If
    End Sub

    Public Sub QuitarDistinciones(ByVal codigo_dis As Integer)
        Dim cnx As New SqlConnection
        Dim DelCommand As New SqlCommand
        Try
            cnx.ConnectionString = _strConecctionString
            cnx.Open()
            DelCommand = New SqlCommand("eliminardistinciones", cnx)
            DelCommand.CommandType = CommandType.StoredProcedure
            DelCommand.Parameters.Add("@codigo_dis", SqlDbType.Int).Value = codigo_dis
            DelCommand.ExecuteNonQuery()
        Catch ex As Exception

        End Try
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If
    End Sub

    Public Sub QuitarPublicaciones(ByVal codigo_pub As Integer)
        Dim cnx As New SqlConnection
        Dim DelCommand As New SqlCommand
        Try
            cnx.ConnectionString = _strConecctionString
            cnx.Open()
            DelCommand = New SqlCommand("HvPersonalPublicacion_Eliminar", cnx)
            DelCommand.CommandType = CommandType.StoredProcedure
            DelCommand.Parameters.Add("@codigo_pub", SqlDbType.Int).Value = codigo_pub
            DelCommand.ExecuteNonQuery()
        Catch ex As Exception

        End Try
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If
    End Sub


    'Actualizar Modificaciones Datos de Personal

    Public Function ModificaTitulos(ByVal codigo_tpf As Integer, ByVal nombretitulo As String, _
    ByVal anioingreso_tpr As Integer, ByVal anioegreso_tpr As Integer, _
    ByVal aniograd_tpr As Integer, ByVal universidad_tpr As String, ByVal codigo_sit As Integer, _
    ByVal codigo_ins As Integer, ByVal codigo_tpr As Integer, ByVal codmod As Integer) As Integer
        Dim cnx As New SqlConnection
        Dim InsCommand As SqlCommand
        Try
            cnx.ConnectionString = _strConecctionString
            cnx.Open()
            'Llamo al procedimiento para agregar titulos
            InsCommand = New SqlCommand("ModificaTituloprofesor", cnx)
            With InsCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@codigo_tpr", SqlDbType.Int).Value = codigo_tpr
                .Parameters.Add("@codigo_tpf", SqlDbType.Int).Value = codigo_tpf
                .Parameters.Add("@NombreTitulo_TPr", SqlDbType.VarChar, 300).Value = nombretitulo
                .Parameters.Add("@anioingreso_tpr", SqlDbType.Int).Value = anioingreso_tpr
                .Parameters.Add("@anioegreso_tpr", SqlDbType.Int).Value = anioegreso_tpr
                .Parameters.Add("@aniograd_tpr", SqlDbType.Int).Value = aniograd_tpr
                .Parameters.Add("@universidad_tpr", SqlDbType.VarChar, 150).Value = universidad_tpr
                .Parameters.Add("@codigo_sit", SqlDbType.Int).Value = codigo_sit
                .Parameters.Add("@codigo_ins", SqlDbType.Int).Value = codigo_ins
                .Parameters.Add("@codigo_mod", SqlDbType.Int).Value = codmod    'add dguevara 04.10.2013
                ModificaTitulos = .ExecuteNonQuery
            End With
        Catch ex As Exception
            ModificaTitulos = -1
        End Try
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If
    End Function

    Public Function ModificaGrados(ByVal codigo_gra As Integer, _
                                   ByVal desgrado As String, _
                                   ByVal anioingreso_gpr As Integer, _
                                   ByVal anioegreso_gpr As Integer, _
                                   ByVal aniograd_gpr As Integer, _
                                   ByVal mencion_gpr As String, _
                                   ByVal universidad_gpr As String, _
                                   ByVal codigo_sit As Integer, _
                                   ByVal codigo_ins As Integer, _
                                   ByVal codigo_gpr As String, ByVal codmod As Integer) As Integer
        Dim cnx As New SqlConnection
        Dim InsCommand As SqlCommand
        Try
            cnx.ConnectionString = _strConecctionString
            cnx.Open()
            InsCommand = New SqlCommand("Modificagradosprofesor", cnx)
            With InsCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@codigo_gra", SqlDbType.Int).Value = codigo_gra
                .Parameters.Add("@desgrado_gpr", SqlDbType.VarChar, 300).Value = desgrado
                .Parameters.Add("@codigo_gpr", SqlDbType.Int).Value = codigo_gpr
                .Parameters.Add("@anioingreso_gpr", SqlDbType.Int).Value = anioingreso_gpr
                .Parameters.Add("@anioegreso_gpr", SqlDbType.Int).Value = anioegreso_gpr
                .Parameters.Add("@aniograd_gpr", SqlDbType.Int).Value = aniograd_gpr
                .Parameters.Add("@mencion_gpr", SqlDbType.VarChar, 300).Value = mencion_gpr
                .Parameters.Add("@universidad_gpr", SqlDbType.VarChar, 150).Value = universidad_gpr
                .Parameters.Add("@codigo_sit", SqlDbType.Int).Value = codigo_sit
                .Parameters.Add("@codigo_ins", SqlDbType.Int).Value = codigo_ins
                .Parameters.Add("@codigo_mod", SqlDbType.Int).Value = codmod        'add 04.10.2013 dguevara
                ModificaGrados = .ExecuteNonQuery
            End With

        Catch ex As Exception
            ModificaGrados = -1
        End Try
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If
    End Function

    Public Function ModificaIdiomas(ByVal codigo_sit As Integer, ByVal codigo_idi As Integer, ByVal centroestudios As String, ByVal aniograduacion As Integer, _
    ByVal observaciones As String, ByVal lee As String, ByVal habla As String, ByVal escribe As String, ByVal codigo_ins As Integer, ByVal codigo_ipr As Integer) As Integer
        Dim cnx As New SqlConnection
        Dim InsCommand As New SqlCommand
        Try
            cnx.ConnectionString = _strConecctionString
            cnx.Open()
            InsCommand = New SqlCommand("Modificaidiomasprofesor", cnx)
            With InsCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@codigo_sit", SqlDbType.TinyInt).Value = codigo_sit
                .Parameters.Add("@codigo_idi", SqlDbType.TinyInt).Value = codigo_idi
                .Parameters.Add("@centroestudios", SqlDbType.VarChar, 300).Value = centroestudios
                .Parameters.Add("@aniograduacion", SqlDbType.Int).Value = aniograduacion
                .Parameters.Add("@observaciones", SqlDbType.VarChar, 300).Value = observaciones
                .Parameters.Add("@lee", SqlDbType.Char, 2).Value = lee
                .Parameters.Add("@habla", SqlDbType.Char, 2).Value = habla
                .Parameters.Add("@escribe", SqlDbType.Char, 2).Value = escribe
                .Parameters.Add("@codigo_ins", SqlDbType.Int).Value = codigo_ins
                .Parameters.Add("@codigo_ipr", SqlDbType.Int).Value = codigo_ipr
                ModificaIdiomas = .ExecuteNonQuery
            End With
        Catch ex As Exception
            ModificaIdiomas = -1
        End Try
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If

    End Function

    Public Function ModificaOtrosEstudios(ByVal codigo_areaes As Integer, _
                                          ByVal codigo_ins As Integer, _
                                          ByVal nombre_ins As String, _
    ByVal nombre_est As String, ByVal mes_inicio As String, ByVal anio_inicio As Integer, ByVal mes_fin As String, ByVal anio_fin As Integer, _
    ByVal tipo_mod As Integer, ByVal actual_estudio As Integer, ByVal observacion As String, ByVal intSituacion As Integer, ByVal codigo_opr As Integer) As Integer
        Dim cnx As New SqlConnection
        Dim InsCommand As New SqlCommand
        Try
            cnx.ConnectionString = _strConecctionString
            cnx.Open()
            InsCommand = New SqlCommand("ModificaOtrosestudios", cnx)
            With InsCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@codigo_areaes", SqlDbType.TinyInt).Value = codigo_areaes
                .Parameters.Add("@codigo_ins", SqlDbType.TinyInt).Value = codigo_ins
                .Parameters.Add("@nombre_ins", SqlDbType.VarChar, 300).Value = nombre_ins
                .Parameters.Add("@nombre_est", SqlDbType.VarChar, 300).Value = nombre_est
                .Parameters.Add("@mes_inicio", SqlDbType.VarChar, 12).Value = mes_inicio
                .Parameters.Add("@anio_inicio", SqlDbType.SmallInt).Value = anio_inicio
                .Parameters.Add("@mes_fin", SqlDbType.VarChar, 12).Value = mes_fin
                .Parameters.Add("@anio_fin", SqlDbType.SmallInt).Value = anio_fin
                .Parameters.Add("@tipo_mod", SqlDbType.SmallInt).Value = tipo_mod
                .Parameters.Add("@actual_estudio", SqlDbType.SmallInt).Value = actual_estudio
                .Parameters.Add("@codigo_opr", SqlDbType.Int).Value = codigo_opr
                .Parameters.Add("@observacion", SqlDbType.VarChar, 300).Value = observacion
                .Parameters.Add("@codigo_sit", SqlDbType.TinyInt).Value = intSituacion
                ModificaOtrosEstudios = .ExecuteNonQuery
            End With
        Catch ex As Exception
            ModificaOtrosEstudios = -1
        End Try
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If

    End Function

    Public Function Modificarexperiencia(ByVal codigo_car As Integer, ByVal funcion_exp As String, ByVal fechainicio_exp As Date, ByVal fechafin_exp As Date, _
    ByVal descripcion_exp As String, ByVal codigo_tco As Integer, ByVal motivocese As String, ByVal ciudad As String, ByVal empresa As String, _
    ByVal codigo_exp As Integer, ByVal tipo_exp As String) As Integer

        Dim cnx As New SqlConnection
        Dim InsCommand As New SqlCommand
        Try
            cnx.ConnectionString = _strConecctionString
            cnx.Open()
            InsCommand = New SqlCommand("ModificarExperiencia", cnx)
            With InsCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@codigo_car", SqlDbType.Int).Value = codigo_car
                .Parameters.Add("@codigo_exp", SqlDbType.Int).Value = codigo_exp
                .Parameters.Add("@funcion_exp", SqlDbType.VarChar, 300).Value = funcion_exp
                .Parameters.Add("@fechainicio_exp", SqlDbType.SmallDateTime).Value = fechainicio_exp
                .Parameters.Add("@fechafin_exp", SqlDbType.SmallDateTime).Value = fechafin_exp
                .Parameters.Add("@descripcion_exp", SqlDbType.VarChar, 800).Value = descripcion_exp
                .Parameters.Add("@codigo_tco", SqlDbType.Int).Value = codigo_tco
                .Parameters.Add("@motivocese", SqlDbType.VarChar, 20).Value = motivocese
                .Parameters.Add("@ciudad", SqlDbType.VarChar, 40).Value = ciudad
                .Parameters.Add("@empresa", SqlDbType.VarChar, 300).Value = empresa
                '*** Tipo de experiencia ***------------------------------------------
                .Parameters.Add("@tipo_exp", SqlDbType.Char, 2).Value = tipo_exp
                Modificarexperiencia = .ExecuteNonQuery
            End With
        Catch ex As Exception
            Modificarexperiencia = -1
        End Try
        If cnx.State = ConnectionState.Open Then
            cnx.Close()
            cnx = Nothing
        End If

    End Function

    Public Function ModificarDistinciones(ByVal nombre_dis As String, _
                                          ByVal otorgado_dis As String, _
                                          ByVal ciudad_dis As String, _
                                          ByVal motivo_dis As String, _
                                          ByVal codigo_tdis As Integer, _
                                          ByVal fechaentrega As DateTime, _
                                          ByVal codigo_dis As Integer) As Integer
        Dim Cnx As New SqlConnection
        Dim InsCommand As SqlCommand
        Try
            Cnx.ConnectionString = _strConecctionString
            Cnx.Open()
            InsCommand = New SqlCommand("ModificarDistinciones", Cnx)
            With InsCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@codigo_dis", SqlDbType.Int).Value = codigo_dis
                .Parameters.Add("@nombre_dis", SqlDbType.VarChar, 300).Value = nombre_dis
                .Parameters.Add("@otorgado_dis", SqlDbType.VarChar, 300).Value = otorgado_dis
                .Parameters.Add("@ciudad_dis", SqlDbType.VarChar, 50).Value = ciudad_dis
                .Parameters.Add("@motivo_dis", SqlDbType.VarChar, 800).Value = motivo_dis
                .Parameters.Add("@codigo_tdis", SqlDbType.Int).Value = codigo_tdis
                .Parameters.Add("@fechaentrega", SqlDbType.DateTime).Value = fechaentrega
                ModificarDistinciones = .ExecuteNonQuery
            End With
        Catch ex As Exception
            ModificarDistinciones = -1
        End Try
        If Cnx.State = ConnectionState.Open Then
            Cnx.Close()
            Cnx = Nothing
        End If

    End Function

    Public Function ModificarPublicaciones(ByVal nombre As String, ByVal editorial As String, ByVal procedencia As String, ByVal autoria As String, ByVal tipo As String, ByVal info As String, ByVal codigo_pub As Integer) As Integer
        Dim Cnx As New SqlConnection
        Dim InsCommand As SqlCommand
        Try
            Cnx.ConnectionString = _strConecctionString
            Cnx.Open()
            InsCommand = New SqlCommand("HvPersonalPublicacion_Modificar", Cnx)
            With InsCommand
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@nombre", SqlDbType.VarChar, 800).Value = nombre
                .Parameters.Add("@editorial", SqlDbType.VarChar, 800).Value = editorial
                .Parameters.Add("@procedencia", SqlDbType.Char, 1).Value = procedencia
                .Parameters.Add("@autoria", SqlDbType.Char, 1).Value = autoria
                .Parameters.Add("@tipo", SqlDbType.Char, 2).Value = tipo
                .Parameters.Add("@info", SqlDbType.Char, 1).Value = info
                .Parameters.Add("@codigo_pub", SqlDbType.Char, 1).Value = codigo_pub
                ModificarPublicaciones = .ExecuteNonQuery
            End With
        Catch ex As Exception
            ModificarPublicaciones = -1
        End Try
        If Cnx.State = ConnectionState.Open Then
            Cnx.Close()
            Cnx = Nothing
        End If
    End Function
    ' Para el refistro de investigaciones anteriores al mpodulo desarrollado.

    Public Function NuevaInvestigacion(ByVal tipo As String, ByVal titulo_inv As String, ByVal fechaini As DateTime, _
   ByVal fechafin As DateTime, ByVal codigo_tin As Integer, ByVal codigo_eti As Integer, ByVal codigo_tem As Integer, _
   ByVal estado_inv As Integer, ByVal beneficiarios As String, ByVal fecha As DateTime, ByVal archivo1 As FileUpload, _
   ByVal archivo2 As FileUpload, ByVal ruta As String, ByVal institucion As String, ByVal codigo_per As String) As Integer

        Dim codigo_lip As Integer           'El codigo de la linea de investigacion
        Dim retorno As Integer              'El codigo de la investigacion
        Dim ObjInv As New ClsSqlServer(_strConecctionString)

        Try
            Dim nombreArchivo1, nombreArchivo2 As String

            ObjInv.IniciarTransaccion()
            ' ####################################################################
            ' Agregando una investigacion y devolviendo su codigo de investigacion
            ' ####################################################################
            retorno = ObjInv.Ejecutar("AgregarInvestigacionesExternas", "1", titulo_inv, fechaini, fechafin, codigo_tin, _
            codigo_eti, IIf(codigo_tem = 0, System.DBNull.Value, codigo_tem), estado_inv, beneficiarios, fecha, institucion, 0)

            ' ####################################################################
            ' Creando una carpeta y agregando los archivos de Informe y Resumen
            ' ####################################################################
            Dim Carpeta As New System.IO.DirectoryInfo(ruta & "\" & retorno)
            nombreArchivo1 = retorno & "informe" & System.IO.Path.GetExtension(archivo1.FileName).ToLower()
            If Carpeta.Exists = False Then : Carpeta.Create() : End If
            If archivo1.HasFile Then : archivo1.PostedFile.SaveAs(ruta & "\" & retorno & "\" & nombreArchivo1) : End If

            nombreArchivo2 = retorno & "resumen" & System.IO.Path.GetExtension(archivo2.FileName).ToLower()
            If archivo2.HasFile Then : archivo2.PostedFile.SaveAs(ruta & "\" & retorno & "\" & nombreArchivo2) : End If

            ' ####################################################################
            ' Modificar las rutas de los archivos que se suben.
            ' ####################################################################
            ObjInv.Ejecutar("ModificarRutaInvestigaciones", 3, retorno, nombreArchivo1, Now.Date)
            ObjInv.Ejecutar("ModificarRutaInvestigaciones", 4, retorno, nombreArchivo2, Now.Date)

            ' ####################################################################
            ' Consulto la linea de investigacion
            ' ####################################################################
            If codigo_tem <> 0 Then
                codigo_lip = ObjInv.TraerValor("ConsultarRetornarLineaPersonal", "1", codigo_tem, codigo_per, 0)
            End If

            ' ####################################################################
            ' Guardo el responsable de la investigacion que en este caso es el autor mismo
            ' ####################################################################
            If codigo_tem <> 0 Then
                ObjInv.Ejecutar("AgregarResponsable", "1", codigo_lip, 0, "", "", "", "", retorno, 1)
            Else
                ObjInv.Ejecutar("AgregarResponsable", "2", codigo_per, 0, "", "", "", "", retorno, 1)
            End If

            ObjInv.TerminarTransaccion()

            Return retorno              'Exito en el ingreso de los datos.

        Catch ex As Exception
            ObjInv.AbortarTransaccion()
            Return -1                  'Ocurrio un error al insertar el registro
        End Try

    End Function

    Public Function AgregarResponsable(ByVal tipo As String, ByVal codigo_lip As Integer, ByVal codigo_alu As Integer, ByVal nombre As String, _
   ByVal paterno As String, ByVal materno As String, ByVal centrolab As String, ByVal codigo_inv As Integer, ByVal codigo_tpi As Integer) As Integer
        Dim Cnx As SqlConnection
        Dim Cmd As SqlCommand
        Cnx = New SqlConnection(_strConecctionString)
        Try
            Cnx.Open()
            Cmd = New SqlCommand("AgregarResponsable", Cnx)
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = tipo
            Cmd.Parameters.Add("@codigo_LIP", SqlDbType.BigInt).Value = codigo_lip
            Cmd.Parameters.Add("@codigo_Alu", SqlDbType.BigInt).Value = codigo_alu
            Cmd.Parameters.Add("@nombreInv", SqlDbType.VarChar, 20).Value = nombre
            Cmd.Parameters.Add("@apellidoPatInv", SqlDbType.VarChar, 20).Value = paterno
            Cmd.Parameters.Add("@apellidoMatInv", SqlDbType.VarChar, 20).Value = materno
            Cmd.Parameters.Add("@CentroLabInv", SqlDbType.VarChar, 50).Value = centrolab
            Cmd.Parameters.Add("@codigo_Inv", SqlDbType.BigInt).Value = codigo_inv
            Cmd.Parameters.Add("@codigo_Tpi", SqlDbType.TinyInt).Value = codigo_tpi
            Cmd.ExecuteNonQuery()
            Return 1
        Catch ex As Exception
            Return -1
        End Try

        If Cnx.State = ConnectionState.Open Then
            Cnx.Close()
            Cnx = Nothing
        End If

    End Function

    Public Sub EliminarResponsableInv(ByVal codigo_res As Integer)
        Dim Cnx As SqlConnection
        Dim cmd As SqlCommand
        Cnx = New SqlConnection(_strConecctionString)
        Try
            Cnx.Open()
            cmd = New SqlCommand("EliminarResponsableInvestigacion", Cnx)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@codigo_Res", SqlDbType.BigInt).Value = codigo_res
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        End Try

        If Cnx.State = ConnectionState.Open Then
            Cnx.Close()
            Cnx = Nothing
        End If
    End Sub

    Public Function ConsultarUnidadesInvestigacion(ByVal tipo As String, ByVal param1 As String) As DataTable
        Dim Adapter As SqlDataAdapter
        Dim Tabla As New DataTable
        'Try
        Adapter = New SqlDataAdapter("ConsultarUnidadesInvestigacion", _strConecctionString)
        Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
        Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.VarChar, 2).Value = tipo
        Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 20).Value = param1
        Adapter.Fill(Tabla)
        Return Tabla
        'Catch ex As Exception
        'Return Nothing
        'End Try
    End Function

    Public Function ConsultarInvestigaciones(ByVal tipo As String, ByVal param1 As String) As DataTable
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            Return obj.TraerDataTable("ConsultarInvestigaciones", tipo, param1)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ConsultarPersonalCCInvestigacion(ByVal tipo As String, ByVal param1 As String, ByVal param2 As String) As DataTable
        Dim Adapter As SqlDataAdapter
        Dim Tabla As New DataTable
        Try
            Adapter = New SqlDataAdapter("ConsultarPersonalCCInvestigacion", _strConecctionString)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 2).Value = tipo
            Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 20).Value = param1
            Adapter.SelectCommand.Parameters.Add("@param2", SqlDbType.VarChar, 20).Value = param2
            Adapter.Fill(Tabla)
            Return Tabla
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    '----------- Agregado por Dguevara 02.08.2012--------------------------------

    Public Function CargarListaDepartamento() As DataTable
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        'Definimos el procedimiento y el tipo
        Adapter = New SqlDataAdapter("ConsultarLugares", _strConecctionString)
        Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
        'Los parametros que tendra el procedure
        Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.Char, 1).Value = "2"
        Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = "156"
        Adapter.SelectCommand.Parameters.Add("@param2", SqlDbType.VarChar, 50).Value = ""
        'Lllenamos la tabla que va a retornar.
        Adapter.Fill(Tabla)
        Return Tabla
        Adapter = Nothing
    End Function

    Public Function ConsultaCodigo_afp(ByVal codigo_reg As Integer) As DataTable
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Adapter = New SqlDataAdapter("PER_ConsultaCodigoAfp", _strConecctionString)
        Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
        Adapter.SelectCommand.Parameters.Add("@codigo_reg", SqlDbType.Int, 4).Value = codigo_reg
        'Lllenamos la tabla que va a retornar.
        Adapter.Fill(Tabla)
        Return Tabla
        Adapter = Nothing
    End Function

    Public Function DatosUbigeo(ByVal domicilioUbiGeo_Per As Integer) As DataTable
        Dim Tabla As New DataTable
        Dim Adapter As SqlDataAdapter
        Adapter = New SqlDataAdapter("ConsultarLugares", _strConecctionString)
        Adapter.SelectCommand.CommandType = CommandType.StoredProcedure

        Adapter.SelectCommand.Parameters.Add("@tipo", SqlDbType.VarChar, 10).Value = "8"
        Adapter.SelectCommand.Parameters.Add("@param1", SqlDbType.VarChar, 10).Value = domicilioUbiGeo_Per
        Adapter.SelectCommand.Parameters.Add("@param2", SqlDbType.VarChar, 10).Value = ""

        'Llenamos la tabla que va a retornar.
        Adapter.Fill(Tabla)
        Return Tabla
        Adapter = Nothing
    End Function

End Class

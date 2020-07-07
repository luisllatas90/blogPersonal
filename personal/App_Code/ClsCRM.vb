Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Security.Cryptography

Public Class ClsCRM
    Private cnx As New ClsConectarDatos

    'Public Function ListaInteresados(ByVal codigo_int As Integer) As Data.DataTable
    '    Dim dts As New Data.DataTable
    '    cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    cnx.AbrirConexion()
    '    dts = cnx.TraerDataTable("CRM_ListaInteresados", codigo_int)
    '    cnx.CerrarConexion()
    '    Return dts
    'End Function

    'Public Function ListaComunicacion(ByVal codigo_int As Integer) As Data.DataTable
    '    Dim dts As New Data.DataTable
    '    cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    cnx.AbrirConexion()
    '    dts = cnx.TraerDataTable("CRM_ListaComunicacion", codigo_int)
    '    cnx.CerrarConexion()
    '    Return dts
    'End Function

    Public Function ListaDepartamentos() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("spPla_ConsultarDepartamento", "TO", "")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaProvincias(ByVal codigo_dep As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("spPla_ConsultarProvincia", "ES", codigo_dep)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaDistritos(ByVal codigo_prov As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("spPla_ConsultarDistrito", "ES", codigo_prov)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaTipoComunicacion(ByVal tipo As String, ByVal descripcion As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_ListaTipoComunicacion", tipo, descripcion)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaMotivo(ByVal tipo As String, ByVal descripcion As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_ListaMotivo", tipo, descripcion)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaComunicacion(ByVal tipo As String, ByVal descripcion As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_ListaComunicacion", tipo, descripcion)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaAcuerdo(ByVal tipo As String, ByVal descripcion As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_ListaAcuerdo", tipo, descripcion)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarInteresado(ByVal codigo As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_EliminaInteresado", codigo)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function EliminarInteresadoV2(ByVal codigo_int As Integer) As Object()
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        Dim loResultado As Object() = cnx.Ejecutar("CRM_EliminaInteresado", codigo_int)
        cnx.CerrarConexion()
        Return loResultado
    End Function
    'Public Function EliminarComunicacion(ByVal codigo As Integer) As String
    '    Dim dts As New Data.DataTable
    '    cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    cnx.AbrirConexion()
    '    dts = cnx.TraerDataTable("CRM_EliminaComunicacion", codigo)
    '    cnx.CerrarConexion()
    '    Return dts.Rows(0).Item("Mensaje").ToString
    'End Function

    Public Function InsertarInteresado(ByVal tipo_doc As Integer, ByVal numdoc As String, ByVal apepat As String, ByVal apemat As String, ByVal nombres As String, ByVal cbodepartamento As Integer, ByVal cboprovincia As Integer, ByVal cbodistrito As Integer, ByVal direccion As String, ByVal telefono As String, ByVal email As String, ByVal celular As String, ByVal procedencia As String, ByVal grado As String, ByVal carrera As String, ByVal user_reg As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_InsertarInteresado", tipo_doc, numdoc, apepat, apemat, nombres, cbodepartamento, cboprovincia, cbodistrito, direccion, telefono, email, celular, procedencia, grado, carrera, user_reg)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    'Public Function ActualizarInteresado(ByVal codigo As Integer, ByVal tipo_doc As Integer, ByVal numdoc As String, ByVal apepat As String, ByVal apemat As String, ByVal nombres As String, ByVal cbodepartamento As Integer, ByVal cboprovincia As Integer, ByVal cbodistrito As Integer, ByVal direccion As String, ByVal telefono As String, ByVal email As String, ByVal celular As String, ByVal procedencia As String, ByVal grado As String, ByVal carrera As String, ByVal user_reg As Integer) As String
    '    Dim dts As New Data.DataTable
    '    cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    cnx.AbrirConexion()
    '    dts = cnx.TraerDataTable("CRM_ActualizarInteresado", codigo, tipo_doc, numdoc, apepat, apemat, nombres, cbodepartamento, cboprovincia, cbodistrito, direccion, telefono, email, celular, procedencia, grado, carrera, user_reg)
    '    cnx.CerrarConexion()
    '    Return dts.Rows(0).Item("Mensaje").ToString
    'End Function

    Public Function InsertarComunicacion(ByVal tipo_com As Integer, ByVal detalle As String, ByVal carrera As String, ByVal cod_int As Integer, ByVal usuario_reg As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_InsertaComunicacion", tipo_com, detalle, carrera, cod_int, usuario_reg)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    'Inicio Metodo Encriptar Edgard  05/09/2018
    Public Function EncriptaTexto(ByVal texto As String) As String
        'Dim result As String = ""
        'Dim encryted As Byte()
        'encryted = System.Text.Encoding.Unicode.GetBytes(_stringToEncrypt)
        'result = Convert.ToBase64String(encryted)
        'Return result

        Try

            Dim key As String = "KCARTSUNOB" & Date.Today.ToString("dd/MM/yyyy")
            Dim keyArray As Byte()
            Dim Arreglo_a_Cifrar As Byte() = UTF8Encoding.UTF8.GetBytes(texto)
            Dim hashmd5 As MD5CryptoServiceProvider = New MD5CryptoServiceProvider()
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key))
            hashmd5.Clear()
            Dim tdes As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider()
            tdes.Key = keyArray
            tdes.Mode = CipherMode.ECB
            tdes.Padding = PaddingMode.PKCS7
            Dim cTransform As ICryptoTransform = tdes.CreateEncryptor()
            Dim ArrayResultado As Byte() = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length)
            tdes.Clear()
            texto = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length)
        Catch __unusedException1__ As Exception
        End Try

        Return texto

    End Function

    Public Function DesencriptaTexto(ByVal textoEncriptado As String) As String
        'Dim result As String = ""
        'Dim decryted As Byte()
        'decryted = Convert.FromBase64String(_stringToDecrypt)
        'result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.Length)
        'Return result

        Try
            Dim key As String = "KCARTSUNOB" & Date.Today.ToString("dd/MM/yyyy")
            Dim keyArray As Byte()
            Dim Array_a_Descifrar As Byte() = Convert.FromBase64String(textoEncriptado)
            Dim hashmd5 As MD5CryptoServiceProvider = New MD5CryptoServiceProvider()
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key))
            hashmd5.Clear()
            Dim tdes As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider()
            tdes.Key = keyArray
            tdes.Mode = CipherMode.ECB
            tdes.Padding = PaddingMode.PKCS7
            Dim cTransform As ICryptoTransform = tdes.CreateDecryptor()
            Dim resultArray As Byte() = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length)
            tdes.Clear()
            textoEncriptado = UTF8Encoding.UTF8.GetString(resultArray)
        Catch __unusedException1__ As Exception
        End Try

        Return textoEncriptado
    End Function

    'Fin Metodos Encriptar Edgard  05/09/2018


    'Utilizado de Aqui para Abajo

    Public Function EncrytedString64(ByVal texto As String) As String
        Dim result As String = ""
        Dim encryted As Byte()
        encryted = System.Text.Encoding.Unicode.GetBytes(texto)
        result = Convert.ToBase64String(encryted)
        Return result
    End Function

    Public Function DecrytedString64(ByVal textoEncriptado As String) As String
        Dim result As String = ""
        Dim decryted As Byte()
        decryted = Convert.FromBase64String(textoEncriptado)
        result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.Length)
        Return result
    End Function

    Public Function ListaTipoEstudio(ByVal tipo As String, ByVal codigo As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ACAD_ConsultarTipoEstudio", tipo, codigo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaCicloAcademico(ByVal tipo As String, ByVal codigo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_ListaCicloAcademico", tipo, codigo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaConvocatorias(ByVal tipo As String, ByVal codigo_con As String, ByVal codigo_test As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_ListaConvocatorias", tipo, codigo_con, codigo_test)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ListarCentroCosto(ByVal tipoFuncion As Integer, ByVal codigoPer As Integer, ByVal codigoTest As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("EVE_ConsultarCentroCostosXPermisosXVisibilidad", tipoFuncion, codigoPer, "", codigoTest, 1)
        cnx.CerrarConexion()
        Return dt
    End Function


    Public Function ActualizarConvocatoria(ByVal codigo_con As Integer, ByVal codigo_test As Integer, ByVal codigo_cac As Integer, ByVal nombre_con As String, ByVal descripcion_con As String, ByVal fecha_ini As String, ByVal fecha_fin As String, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_ActualizarConvocatoria", codigo_con, codigo_test, codigo_cac, nombre_con, descripcion_con, fecha_ini, fecha_fin, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function EliminarConvocatoria(ByVal codigo_con As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_EliminarConvocatoria", codigo_con)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarEvento(ByVal codigo_eve As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_EliminarEvento", codigo_eve)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarTipoComunicacion(ByVal codigo_tcom As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_EliminarTipoComunicacion", codigo_tcom)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarMotivo(ByVal codigo_tcom As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_EliminarMotivo", codigo_tcom)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarComunicacion(ByVal codigo_com As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_EliminarComunicacion", codigo_com)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarAcuerdo(ByVal codigo_acu As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_EliminarAcuerdo", codigo_acu)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaEventos(ByVal tipo As String, ByVal codigo_eve As String, ByVal codigo_test As String, ByVal codigo_conv As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_ListaEventos", tipo, codigo_eve, codigo_test, codigo_conv)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ListaTipoDocumento() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_TipoDocumentoIdentidad", "TO", "")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaActividadPOA(ByVal tipo As String, ByVal codigo_eve As String, ByVal codigo_test As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_ListaActividadPOA", tipo, 0, 0)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ActualizarEvento(ByVal codigo_eve As Integer, ByVal nombre_eve As String, ByVal descripcion_eve As String, ByVal codigo_con As Integer, _
                                     ByVal codigo_acp As Integer, ByVal fecha_ini As String, ByVal fecha_fin As String, ByVal estado As Integer, _
                                     ByVal usuario_reg As Integer, ByVal detalleUbicacion As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_AcualizarEvento", codigo_eve, nombre_eve, descripcion_eve, codigo_con, codigo_acp, fecha_ini, fecha_fin, estado, usuario_reg, detalleUbicacion)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarComunicacion(ByVal codigo_com As Integer, ByVal codigo_tcom As Integer, ByVal codigo_int As Integer, ByVal codigo_mot As Integer, _
                                            ByVal detalle As String, ByVal codigo_per As Integer, ByVal estado_com As Integer, _
                                           ByVal user_reg As Integer, ByVal codigo_cat As Integer, ByVal codigo_eve As Integer, ByVal destinatario_com As String, ByVal remitente_com As String) As Data.DataTable

        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        Dim codigoEve As Object = IIf(codigo_eve <> 0, codigo_eve, DBNull.Value)

        dts = cnx.TraerDataTable("CRM_AcualizarComunicacion", codigo_com, codigo_tcom, codigo_int, codigo_mot, detalle, codigo_per, estado_com, user_reg, codigo_cat, codigoEve, destinatario_com, remitente_com)

        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarAcuerdo(ByVal codigo_acu As Integer, ByVal codigo_com As Integer, ByVal detalle_acu As String, ByVal fecha_acu As String, _
                                      ByVal hora_acu As String, ByVal estado_acu As Integer, ByVal user_reg As Integer) As Data.DataTable

        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_AcualizarAcuerdo", codigo_acu, codigo_com, detalle_acu, fecha_acu, hora_acu, estado_acu, user_reg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarTipoComunicacion(ByVal codigo_tcom As Integer, ByVal descripcion_tcom As String, ByVal estado_tcom As Integer, _
                                               ByVal usuario_reg As Integer, ByVal categoria_tcom As String, ByVal procedencia_tcom As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_AcualizarTipoComunicacion", codigo_tcom, descripcion_tcom, estado_tcom, usuario_reg, categoria_tcom, procedencia_tcom)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarMotivo(ByVal codigo_mot As Integer, ByVal descripcion_mot As String, ByVal estado_mot As Integer, _
                                     ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_AcualizarMotivo", codigo_mot, descripcion_mot, estado_mot, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function BuscaxTipoyNumDoc(ByVal opcion As String, ByVal codigo_int As Integer, ByVal tipo_doc As String, ByVal num_doc As String, ByVal apepat As String, ByVal apemat As String, ByVal nombre As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_BuscarInteresado", opcion, codigo_int, tipo_doc, num_doc, apepat, apemat, nombre)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ListaInteresadosxEvento(ByVal tipo As String, ByVal codigo_test As String, ByVal codigo_con As String, ByVal codigo_eve As String, ByVal letra_ini As String, ByVal letra_fin As String, ByVal texto As String, ByVal cod_sin As String, ByVal comunicacion As String, ByVal Acuerdo As String, ByVal codigo_cpf As String, ByVal codigo_cco As Integer, ByVal grados As String, ByVal fechaDesde As String, ByVal fechaHasta As String, ByVal codigoOri As Integer, ByVal fechaAcuerdo As String, ByVal prioridad As Integer, ByVal usuarioAcuerdo As Integer, ByVal codigos_ied As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_ListaInteresadosxEvento", tipo, codigo_test, codigo_con, codigo_eve, letra_ini, letra_fin, texto, cod_sin, comunicacion, Acuerdo, codigo_cpf, codigo_cco, grados, fechaDesde, fechaHasta, codigoOri, fechaAcuerdo, prioridad, usuarioAcuerdo, codigos_ied)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ListaInteresadosxEvento_V2(ByVal tipo As String, ByVal codigo_test As String, ByVal codigo_con As String, ByVal codigo_eve As String, _
                                               ByVal letra_ini As String, ByVal letra_fin As String, ByVal texto As String, ByVal cod_sin As String, _
                                               ByVal comunicacion As String, ByVal Acuerdo As String, ByVal codigo_cpf As String, ByVal codigo_cco As String, _
                                               ByVal grados As String, ByVal fechaDesde As String, ByVal fechaHasta As String, ByVal codigoOri As String, _
                                               ByVal fechaAcuerdo As String, ByVal prioridad As Integer, ByVal usuarioAcuerdo As Integer, _
                                               ByVal codigos_ied As String, ByVal origenInscripcion_alu As String, ByVal codigo_req As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_ListaInteresadosxEvento_V2", tipo, codigo_test, codigo_con, codigo_eve, letra_ini, letra_fin, texto, cod_sin, comunicacion, _
                                Acuerdo, codigo_cpf, codigo_cco, grados, fechaDesde, fechaHasta, codigoOri, fechaAcuerdo, prioridad, usuarioAcuerdo, codigos_ied, _
                                origenInscripcion_alu, codigo_req)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function BuscaInstitucionEducativa(ByVal cod_Reg As String, ByVal cod_prov As String, ByVal cod_dis As String, ByVal texto As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_BuscaIEducativa", cod_Reg, cod_prov, cod_dis, texto)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ActualizarInteresado(ByVal codigo_int As String, ByVal tipo_doc As Integer, ByVal num_doc As String, ByVal apepat As String, ByVal apemat As String, _
                                         ByVal nombre As String, ByVal fec_naci As String, ByVal cod_ie As String, ByVal cod_cpf As Integer, ByVal estado_int As Integer, _
                                         ByVal grado As String, ByVal usuario_reg As Integer, ByVal num_doc_confirmado As Boolean, ByVal anioEgreso As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_ActualizarInteresado", codigo_int, tipo_doc, num_doc, apepat, apemat, nombre, fec_naci, cod_ie, cod_cpf, estado_int, grado, usuario_reg, num_doc_confirmado, anioEgreso)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InscribirInteresado(ByVal codigo_int As Integer, ByVal cod_eve As Integer, ByVal cod_ori As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_InscribirInteresado", codigo_int, cod_eve, cod_ori, "R", usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaDirecciones(ByVal tipo As String, ByVal codigo_dir As Integer, ByVal codigo_interesado As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_ListaDirecciones", tipo, codigo_dir, codigo_interesado)
        cnx.CerrarConexion()
        Return dt
    End Function


    Public Function ActualizarDireccion(ByVal codigo_dir As Integer, ByVal codigo_int As Integer, ByVal codigo_reg As Integer, ByVal codigo_prov As Integer, ByVal codigo_dis As Integer, ByVal direccion As String, ByVal vigencia As Integer, ByVal user_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_ActualizarDireccion", codigo_dir, codigo_int, codigo_reg, codigo_prov, codigo_dis, direccion, vigencia, user_reg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaTelefonos(ByVal tipo As String, ByVal codigo_tel As Integer, ByVal codigo_interesado As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_ListaTelefonos", tipo, codigo_tel, codigo_interesado)
        cnx.CerrarConexion()
        Return dt
    End Function


    Public Function ActualizarTelefono(ByVal codigo_dir As Integer, ByVal codigo_int As Integer, ByVal tipo_tel As String, ByVal numero As String, ByVal detalle As String, ByVal vigencia As Integer, ByVal user_reg As Integer, ByVal pertenencia As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_ActualizarTelefono", codigo_dir, codigo_int, tipo_tel, numero, detalle, vigencia, user_reg, pertenencia)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaEMail(ByVal tipo As String, ByVal codigo_emi As Integer, ByVal codigo_interesado As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_ListaEMail", tipo, codigo_emi, codigo_interesado)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ActualizarEMail(ByVal codigo_emi As Integer, ByVal codigo_int As Integer, ByVal TipoEMail As String, ByVal Descripcion As String, _
                                    ByVal detalle As String, ByVal vigencia As Integer, ByVal user_reg As Integer, ByVal verificado As Boolean) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_ActualizarEMail", codigo_emi, codigo_int, TipoEMail, Descripcion, detalle, vigencia, user_reg, verificado)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaCarrerasxEvento(ByVal tipo As String, ByVal codigo_eve As Integer, ByVal codigo_test As String, ByVal opcional As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_ListaCarreraxEventoxTest", tipo, codigo_eve, codigo_test, opcional)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ListaSituacionInteresado(ByVal tipo As String, ByVal codigo_sin As Integer, ByVal opcional As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_ListaSituacionInteresado", tipo, codigo_sin, opcional)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ListaCarrera(ByVal tipo As String, ByVal codigo_cpi As Integer, ByVal codigo_interesado As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_ListaCarrera", tipo, codigo_cpi, codigo_interesado)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ActualizarCarrera(ByVal codigo_cpi As Integer, ByVal codigo_int As Integer, ByVal codigo_cpf As Integer, ByVal codigo_eve As Integer, ByVal detalle As String, ByVal prioridad As Integer, ByVal user_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_ActualizarCarrera", codigo_cpi, codigo_int, codigo_cpf, codigo_eve, detalle, prioridad, user_reg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarCarrera(ByVal codigo As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_EliminarCarrera", codigo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaInteresados(ByVal tipo As String, ByVal codigo_int As String, ByVal tipo_doc As String, ByVal num_doc As String, ByVal apepat As String, ByVal apemat As String, ByVal nombre As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_BuscarInteresado", tipo, codigo_int, tipo_doc, num_doc, apepat, apemat, nombre)
        cnx.CerrarConexion()
        Return dt
    End Function


    Public Function ListaPrioridad(ByVal codigo_int As String, ByVal codigo_cpi As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_ListaPrioridad", codigo_int, codigo_cpi)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ListaEstadoComunicacion(ByVal tipo As String, ByVal codigo As String, ByVal param1 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_ListaEstadoComunicacion", tipo, codigo, param1)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ActualizarEstadoComunicacion(ByVal codigo As Integer, ByVal descripcion As String, ByVal estado As Integer, _
                                 ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_ActualizarEstadoComunicacion", codigo, descripcion, estado, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function EliminarEstadoComunicacion(ByVal codigo As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_EliminarEstadoComunicacion", codigo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarPreguntas(ByVal tipo As String, ByVal codigo_pre As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_ListarPreguntas", tipo, codigo_pre)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ListarRespuestas(ByVal tipo As String, ByVal codigo_pre As Integer, ByVal codigo_int As Integer, ByVal codigo_eve As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_ListarRespuestas", tipo, codigo_pre, codigo_int, codigo_eve)
        cnx.CerrarConexion()
        Return dt
    End Function


    Public Function RegistrarRespuestaInteresado(ByVal codigo_eve As Integer, ByVal codigo_int As Integer, ByVal codigo_resp As Integer, _
                                    ByVal respuesta_otro As String, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_RegistrarRespuestaInteresado", codigo_eve, codigo_int, codigo_resp, respuesta_otro, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function EliminarRespuestaDePregunta(ByVal codigo_ipr As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_EliminarRespuestaDePregunta", codigo_ipr)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function MoverRespuestaDePregunta(ByVal codigo_ipr As Integer, ByVal direccion As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_MoverRespuestaDePregunta", codigo_ipr, direccion)
        cnx.CerrarConexion()
        Return dts
    End Function

    ' MIGRAR CON SQLBULKCOPY - NECESITA PERMISOS DE SYS.ADMIN - NO SE UTILIZO
    Public Function ImportarInteresados(ByVal nombre_tabla As String, ByVal datos As DataTable) As String
        Dim linea As String = "0"
        Try
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            cnx.AbrirConexion()
            linea = cnx.ImportarInteresados(nombre_tabla, datos)
            cnx.CerrarConexion()
            Return linea
        Catch ex As Exception
            Return ex.Message.ToString + linea
        End Try
    End Function

    'OBTENER ULTIMO ID DE ARCHIVO,NOMBRE Y RUTA INSERTARO EN ARCHIVOCOMPARTIDO PARA ACTUALIZAR EN LOS REGISTROS
    Public Function ObtenerUltimoIDArchivoCompartiod(ByVal idtabla As Integer, ByVal idtransaccion As String, ByVal nrooperacion As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ObtenerUltimoIDArchvoCompartido", idtabla, idtransaccion, nrooperacion)
        cnx.CerrarConexion()
        Return dts
    End Function


    ' MIGRAR : ENVIO TABLA A PROCEDIMIENTO ALMACENADO, SE DEBE DEFINIR CORRECTAMENTE LAS COLUMNAS DE LA TABLA Y EL PARAMETRO DEL PROCEDIMIENTO
    Public Function MigrarTabla(ByVal tabla As Data.DataTable) As Integer
        Dim respuesta As Integer
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        respuesta = cnx.ImportarTabla("[dbo].[MigrarExcelInteresado]", tabla)
        cnx.CerrarConexion()
        Return respuesta
    End Function

    Public Function MigrarExcelInteresados(ByVal rutaarchivo As String, ByVal codigo_Eve As Integer, ByVal idarchivocompartido As Integer, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSATIMPORT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_MigrarExcelInteresado", rutaarchivo, codigo_Eve, idarchivocompartido, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function MigrarExcelComunicaciones(ByVal rutaarchivo As String, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSATIMPORT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_MigrarExcelComunicaciones", rutaarchivo, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    'Por Luis Q.T. | 17DIC2018 | Obtener lista de intereses
    Public Function ListaInteres(ByVal cod_int As String, ByVal conv As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("PRO_ProgramacionEventoInteresado", conv, "%", cod_int)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ActualizarInteres(ByVal codigo_int As String, ByVal codigo_ori As Integer, ByVal eventos As String, ByVal checked As String, ByVal usuario_reg As Integer) As Object()
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        Dim loResultado As Object() = cnx.Ejecutar("PRO_ActualizarEventoInteresado", codigo_int, codigo_ori, eventos, checked, usuario_reg, "0", "", "0")
        cnx.CerrarConexion()
        Return loResultado
    End Function

    'Por Luis Q.T. | 16ENE2019 | Obtener información histórica general del interesado
    Public Function ListaInformacionGeneral(ByVal dni_int As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_obtenerInformacionGeneral", dni_int)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ListarHistorialProgramacion(ByVal codigoPro As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRO_ListarHistorialProgramacion", codigoPro)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaOrigen() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_ListarOrigenCRM")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ReenviarComunicacion(ByVal codigo_com As Integer, ByVal codigo_per As Integer) As Object()
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        Dim loResultado As Object() = cnx.Ejecutar("CRM_ReenviarComuniacion", codigo_com, codigo_per, "0", "")
        cnx.CerrarConexion()
        Return loResultado
    End Function

    Public Function ListaInstitucionEducativa(ByVal tipo As String, ByVal codigo As String, ByVal soloSecundaria As Boolean) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("WS_ConsultarInstitucionesEducativasPorUbicacion", tipo, codigo, soloSecundaria)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarInstitucionEducativaInteresadoPorConvocatoria(ByVal tipo As String, ByVal codigo As String, ByVal soloSecundaria As Boolean, ByVal codigoCon As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRM_InstitucionEducativaInteresadoPorubicacion", tipo, codigo, soloSecundaria, codigoCon)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarInteresadosPorFiltros(ByVal filtros As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_FiltraInteresadosPorFiltros", filtros)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ListarRequisitosEntregadosInteresado(ByVal codigoInt As Integer, ByVal codigoCon As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_ListarRequisitosEntregadosInteresado", codigoInt, codigoCon)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ListarAnexo(ByVal tipoConsulta As String, ByVal codigoPea As Integer, ByVal codigoPer As String, ByVal numeroPea As String, _
                                ByVal estadoPea As String, ByVal fechaRegPea As String, ByVal usuarioRegPea As Integer, ByVal fechaModPea As String, _
                                ByVal usuarioModPea As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_PersonalAnexo_Listar", tipoConsulta, codigoPea, codigoPer, numeroPea, estadoPea, fechaRegPea, _
                                usuarioRegPea, fechaModPea, usuarioModPea)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function AsignarAnexo(ByVal operacion As String, ByVal codigoPea As Integer, ByVal codigoPer As Integer, ByVal numeroPea As String, _
                                 ByVal estadoPea As String, ByVal codUsuario As Integer) As Object()
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        Dim loResultado As Object() = cnx.Ejecutar("CRM_PersonalAnexo_IUD", operacion, codigoPea, codigoPer, numeroPea, estadoPea, codUsuario)
        cnx.CerrarConexion()
        Return loResultado
    End Function

    Public Function ListarDatosCarreraProfesional(Optional ByVal tipoConsulta As String = "GEN", Optional ByVal codigoDcp As Integer = 0, _
                                                  Optional ByVal codigoCpf As Integer = 0, Optional ByVal videoUrlDcp As String = "", _
                                                  Optional ByVal brochureUrlDcp As String = "", Optional ByVal temarioUrlDcp As String = "", _
                                                  Optional ByVal estadoDcp As String = "A", Optional ByVal fechaRegDcp As String = "", _
                                                  Optional ByVal usuarioRegDcp As Integer = 0, Optional ByVal fechaModDcp As String = "", _
                                                  Optional ByVal usuarioModDcp As Integer = 0) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_DatosCarreraProfesional_Listar", tipoConsulta, codigoDcp, codigoCpf, videoUrlDcp, brochureUrlDcp, _
                                temarioUrlDcp, estadoDcp, fechaRegDcp, usuarioRegDcp, fechaModDcp, usuarioModDcp)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ListarRequisitosAdmision(ByVal codigoTest As String, ByVal codigoMin As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("CRM_ListarRequisitosAdmision", codigoTest, codigoMin)
        cnx.CerrarConexion()
        Return dt
    End Function
End Class

Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Xml



Partial Class persona
    

    Inherits System.Web.UI.Page
    'Dim usuario_session_ As String() = Session("perlogin").ToString.Split(New Char() {"\"c})
    'Dim usuario_session As String = usuario_session_(1)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load





        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try

            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim tipo As String = ""
            'Dim k As String = Request("k")
            Dim obj As New ClsCRM
            Dim arr As New List(Of String)
            Dim ope As String = ""
            Dim lst As Boolean = False
            Dim action As String = ""
            Dim codpk As Integer = 0
            Dim codigo_per As String = Session("id_per").ToString



            action = obj.DecrytedString64(Request.Form("process").ToString())

            'Data.Add("process", action)
            'list.Add(Data)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)



            Select Case action
                Case "Buscar"

                    'Data.Add("cco", CInt(obj.DecrytedString64(Request.Form("cco").ToString())))
                    'Data.Add("estado", Request.Form("cboEstadoParticipanteInsc").ToString())
                    'list.Add(Data)
                    'JSONresult = serializer.Serialize(list)
                    'Response.Write(JSONresult)

                    'Exit Sub
                    BuscarPersonaInscripcion(Request.Form("tipo").ToString(), Request.Form("valor").ToString(), CInt(obj.DecrytedString64(Request.Form("cco").ToString())))
                Case "RegPerSinC"
                    ' Dim expenddt As Date = Date.ParseExact(Request.Form("txtpsfechanac").ToString, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                    Dim objper As New persona()

                    With objper
                        .CodCentroCosto = CInt(obj.DecrytedString64(Request.Form("cco").ToString()))
                        .TipoDoc = Request.Form("cbopscTipoDoc").ToString
                        .NroDoc = Request.Form("txtpscnrodocident").ToString
                        .ApePat = Request.Form("txtpscapepat").ToString
                        .ApeMat = Request.Form("txtpscapemat").ToString
                        .Nombre = Request.Form("txtpscnombre").ToString
                        '.FechaNac = CDate(Request.Form("txtpsfechanac"))
                        .FechaNac = Request.Form("txtpsfechanac").ToString()
                        '.FechaNac = expenddt
                        .Sexo = Request.Form("cbopscSexo").ToString
                        .EstadoCivil = Request.Form("cbopscEstadoCivil").ToString
                        .EmailPrincipal = Request.Form("txtpscemailpri").ToString
                        .EmailAlternativo = Request.Form("txtpscemailalt").ToString
                        .Direccion = Request.Form("txtpscdireccion").ToString
                        .CodDepatarmento = Request.Form("cbopscdpto").ToString
                        .CodProvincia = Request.Form("cbopscdprov").ToString
                        .CodDist = Request.Form("cbopscddist").ToString
                        .TelefonoFijo = Request.Form("txtpscfono").ToString
                        .TelefonoMovil = Request.Form("txtpsccel").ToString
                        .Ruc = Request.Form("txtpscruc").ToString
                        .CodModadlidadIngreso = obj.DecrytedString64(Request.Form("cbopscModIng").ToString)
                        .codpersona = CInt(codigo_per)
                    End With

                    RegistrarPersonaSinCargo(objper)
            End Select

            'Data.Add("msje", Request)
            'JSONresult = serializer.Serialize(sr)
            'Response.Write(JSONresult)
        Catch ex As Exception
            Dim expenddt As Date = Date.ParseExact(Request.Form("txtpsfechanac").ToString, "dd/mm/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Data.Add("msje", False)
            Data.Add("fecNac", expenddt)
            Data.Add("error", ex.Message.ToString)
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub RegistrarPersonaSinCargo(ByVal objPer As persona)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim data As New Dictionary(Of String, Object)()
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim objCRM As New ClsCRM
            Dim codigo_psoNuevo(1) As String
            Dim codigo_cliNuevo(2) As String
            Dim tcl As String = "E"
            Dim cli As Integer = 0
            Dim sw As Boolean = True
            Dim tbcco As New Data.DataTable
            Dim id As String = Request.QueryString("id")

            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.IniciarTransaccion()

            tbcco = obj.TraerDataTable("dbo.EVE_ConsultarEventos", "2", objPer.CodCentroCosto, "0")

            '=================================================================
            'Grabar a la persona: Aquí se verifica si EXISTE.
            '=================================================================
            With objPer

                obj.Ejecutar("PERSON_Agregarpersona", .CodigoPso, _
                    UCase(.ApePat.Trim.ToString), UCase(.ApeMat.Trim.ToString), UCase(.Nombre.Trim.ToString), _
                    .FechaNac, .Sexo.ToString, _
                    .TipoDoc.ToString, .NroDoc.Trim.ToString, _
                    LCase(.EmailPrincipal.Trim.ToString), LCase(.EmailAlternativo.Trim.ToString), _
                    UCase(.Direccion.Trim.ToString), CInt(.CodDist.Trim.ToString), _
                    .TelefonoFijo.Trim.ToString, .TelefonoMovil.Trim.ToString, .EstadoCivil.ToString, .Ruc.Trim.ToString, _
                    1, "0").copyto(codigo_psoNuevo, 0)
                data.Add("codigo_psoNuevo", codigo_psoNuevo)
                data.Add("codigo_psoNuevo2", codigo_psoNuevo(0).ToString())

            End With

            If codigo_psoNuevo(0).ToString = "0" Then

                obj.AbortarTransaccion()
                data.Add("alert", "red")
                data.Add("msje", "Ocurrió un error al registrar los datos Persona. Contáctese con desarrollosistemas@usat.edu.pe")
                data.Add("codPso", codigo_psoNuevo(0).ToString)

            Else

                If CBool(tbcco.Rows(0).Item("gestionanotas_dev")) Then
                    '===============================================================================================================
                    'Grabar como ESTUDIANTE: Siempre y cuando gestione notas
                    '===============================================================================================================
                    With objPer

                        obj.Ejecutar("EVE_AgregarParticipanteEventoGestionaNotas", .CodCentroCosto, .CodModadlidadIngreso, _
                                     UCase(.ApePat.Trim.ToString), UCase(.ApeMat.Trim.ToString), UCase(.Nombre.Trim.ToString), _
                                     .FechaNac, .Sexo, .TipoDoc, .NroDoc.Trim.ToString, _
                                     LCase(.EmailPrincipal.Trim.ToString), LCase(.EmailAlternativo.Trim.ToString), _
                                     UCase(.Direccion.Trim.ToString), CInt(.CodDist.Trim.ToString), _
                                     .TelefonoFijo.Trim.ToString, .TelefonoMovil.Trim.ToString, .EstadoCivil, _
                                     Me.GeneraClave, .codpersona, codigo_psoNuevo(0), "0").copyto(codigo_cliNuevo, 0)
                    End With

                    If codigo_cliNuevo(0).ToString = "0" Or codigo_cliNuevo(0).ToString = "-1" Then
                        obj.AbortarTransaccion()
                        data.Add("alert", "red")
                        If codigo_cliNuevo(0).ToString = "0" Then
                            data.Add("msje", "Ocurrió un error al registrar los datos del participante. Contáctese con desarrollosistemas@usat.edu.pe")
                        End If
                        If codigo_cliNuevo(0).ToString = "-1" Then
                            data.Add("msje", "No puede registrar participantes en este evento, debido a que no se ha registrado un Plan de Estudios.")
                        End If
                    Else
                        obj.TerminarTransaccion()
                        data.Add("alert", "success")
                        data.Add("msje", "Se han registrado los datos correctamente.")
                        data.Add("codPso", codigo_psoNuevo(0).ToString)

                    End If
                Else
                    'obj.Ejecutar("EVE_AgregarParticipanteEventoNoGestionaNotas", cco, Me.dpModalidad.SelectedValue, _
                    '        UCase(Me.txtAPaterno.Text.Trim), UCase(Me.txtAMaterno.Text.Trim), UCase(Me.txtNombres.Text.Trim), _
                    '        CDate(Me.txtFechaNac.Text.Trim), Me.dpSexo.SelectedValue, _
                    '        Me.dpTipoDoc.SelectedValue, Me.txtdni.Text.Trim, _
                    '        LCase(Me.txtemail1.Text.Trim), LCase(Me.txtemail2.Text.Trim), _
                    '        UCase(Me.txtdireccion.Text.Trim), Me.dpdistrito.SelectedValue, _
                    '        Me.txttelefono.Text.Trim, Me.txtcelular.Text.Trim, Me.dpEstadoCivil.SelectedValue, _
                    '        Me.GeneraClave, id, codigo_psoNuevo(0), "E", "0").copyto(codigo_cliNuevo, 0)

                    'codigo_cliNuevo(0): es el tipo de cliente: P / E
                    'codigo_cliNuevo(1): es el ID de la persona o alumno
                    '===============================================================================================================
                    'Grabar como ESTUDIANTE: Siempre y cuando NO gestione notas
                    '===============================================================================================================
                    With objPer

                        obj.Ejecutar("EVE_AgregarParticipanteEventoNoGestionaNotas", .CodCentroCosto, .CodModadlidadIngreso, _
                                     UCase(.ApePat.Trim.ToString), UCase(.ApeMat.Trim.ToString), UCase(.Nombre.Trim.ToString), _
                                     .FechaNac, .Sexo, .TipoDoc, .NroDoc.Trim.ToString, _
                                     LCase(.EmailPrincipal.Trim.ToString), LCase(.EmailAlternativo.Trim.ToString), _
                                     UCase(.Direccion.Trim.ToString), CInt(.CodDist.Trim.ToString), _
                                     .TelefonoFijo.Trim.ToString, .TelefonoMovil.Trim.ToString, .EstadoCivil, _
                                     Me.GeneraClave, .codpersona, codigo_psoNuevo(0), "0").copyto(codigo_cliNuevo, 0)
                    End With

                    If codigo_cliNuevo(0).ToString = "0" Or codigo_cliNuevo(0).ToString = "-1" Then
                        obj.AbortarTransaccion()
                        data.Add("alert", "red")
                        If codigo_cliNuevo(0).ToString = "0" Then
                            data.Add("msje", "Ocurrió un error al registrar los datos del participante. Contáctese con desarrollosistemas@usat.edu.pe")
                        End If
                        If codigo_cliNuevo(0).ToString = "-1" Then
                            data.Add("msje", "No puede registrar participantes en este evento, debido a que no se ha registrado un Plan de Estudios.")
                        End If
                    Else
                        obj.TerminarTransaccion()
                        data.Add("alert", "success")
                        data.Add("msje", "Se han registrado los datos correctamente.")
                        data.Add("codPso", codigo_psoNuevo(0).ToString)

                    End If


                End If
                'obj.TerminarTransaccion()
                'data.Add("alert", "success")
                'data.Add("msje", "Se han registrado los datos correctamente.")
                'data.Add("codPso", codigo_psoNuevo(0).ToString)
            End If

            obj = Nothing

            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)

        Catch ex As Exception
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim data As New Dictionary(Of String, Object)()
            data.Add("msjex", False)
            data.Add("alert", "red")
            data.Add("fecnac", objPer.FechaNac)
            data.Add("msje", ex.Message.ToString)
            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)

        End Try
    End Sub

    Private Sub BuscarPersonaInscripcion(ByVal tipo As String, ByVal valor As String, ByVal cco As Integer, Optional ByVal mostrardni As Boolean = False)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim objCRM As New ClsCRM
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable


            If tipo = "DNIE" Then
                Dim data As New Dictionary(Of String, Object)()
                Dim tbcco As New Data.DataTable
                Dim ExistePersona As Boolean = False
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()

                '==================================
                'Buscar Centro de Costo
                '==================================
                tbcco = obj.TraerDataTable("dbo.EVE_ConsultarEventos", "2", cco, "0")
                '==================================
                'Buscar a la Persona
                '==================================
                tb = obj.TraerDataTable("dbo.PERSON_ConsultarPersona", tipo, valor)
                If tb.Rows.Count > 0 Then

                    ExistePersona = True
                    Dim tblalumno As New Data.DataTable
                    '==================================
                    'Buscar datos del alumno
                    '==================================
                    tblalumno = obj.TraerDataTable("PERSON_ConsultarAlumnoPersona", 0, tb.Rows(0).Item("codigo_Pso"), cco, 0)

                    If tblalumno.Rows.Count > 0 Then
                        data.Add("codMin", objCRM.EncrytedString64(tblalumno.Rows(0).Item("codigo_min").ToString))
                        If tbcco.Rows(0).Item("codigo_cpf") = 9 Then
                            'Me.dpModalidad.Enabled = False
                            data.Add("bloqMin", True)
                        Else
                            data.Add("bloqMin", False)
                        End If
                    End If

                    tblalumno.Dispose()
                    tblalumno = Nothing

                    data.Add("dpto", tb.Rows(0).Item("codigo_dep"))
                    data.Add("prov", tb.Rows(0).Item("codigo_pro"))
                    data.Add("dist", tb.Rows(0).Item("codigo_Dis"))


                End If
                obj.CerrarConexion()
                obj = Nothing

                If ExistePersona = True Then
                    data.Add("sw", True)
                    If mostrardni = True Then
                        If tb.Rows(0).Item("numeroDocIdent_Pso").ToString <> "" Then
                            data.Add("nrodoc", tb.Rows(0).Item("numeroDocIdent_Pso").ToString)
                            data.Add("bloqNrodoc", False)
                        Else
                            data.Add("bloqNrodoc", True)
                        End If
                    Else
                        data.Add("bloqNrodoc", False)
                    End If
                    data.Add("apepat", tb.Rows(0).Item("apellidoPaterno_Pso").ToString())
                    data.Add("apemat", tb.Rows(0).Item("apellidoMaterno_Pso").ToString())
                    data.Add("nombre", tb.Rows(0).Item("nombres_Pso").ToString())

                    If tb.Rows(0).Item("fechanacimiento_pso").ToString <> "" Then
                        data.Add("fecnac", CDate(tb.Rows(0).Item("fechaNacimiento_Pso")).ToShortDateString())
                    End If


                    '==================================
                    'Buscar Dpto/Prov/Distrito Persona
                    '==================================
                    data.Add("codPso", objCRM.EncrytedString64(tb.Rows(0).Item("codigo_Pso").ToString()))

                    If (tb.Rows(0).Item("estadoCivil_Pso") Is System.DBNull.Value = False) AndAlso (tb.Rows(0).Item("estadoCivil_Pso").ToString.Trim <> "") Then
                        data.Add("estadocivil", tb.Rows(0).Item("estadoCivil_Pso").ToString.ToUpper())
                    End If

                    If (tb.Rows(0).Item("sexo_Pso") Is System.DBNull.Value = False) AndAlso (tb.Rows(0).Item("sexo_Pso").ToString.Trim <> "") Then
                        data.Add("sexo", tb.Rows(0).Item("sexo_Pso").ToString.ToUpper)
                    Else
                        data.Add("sexo", "")
                    End If

                    data.Add("tipodoc", tb.Rows(0).Item("tipoDocIdent_Pso").ToString())
                    data.Add("emailpri", tb.Rows(0).Item("emailPrincipal_Pso").ToString())
                    data.Add("emailalt", tb.Rows(0).Item("emailAlternativo_Pso").ToString())
                    data.Add("direccion", tb.Rows(0).Item("direccion_Pso").ToString())
                    data.Add("fonofijo", tb.Rows(0).Item("telefonoFijo_Pso").ToString())
                    data.Add("fonomovil", tb.Rows(0).Item("telefonoCelular_Pso").ToString())
                    data.Add("ruc", tb.Rows(0).Item("nroRuc_Pso").ToString())

                    data.Add("dblNom", True)
                    data.Add("dblOtrosDatos", True)

                Else
                    data.Add("sw", False)
                    data.Add("alert", "warning")
                    data.Add("msje", "No se encontraron datos con el nro de documento: " & valor)
                    data.Add("codPso", "")
                    data.Add("dblNom", True)
                    data.Add("dblOtrosDatos", False)
                End If
                tb.Dispose()
                tb = Nothing


                'If tb.Rows.Count > 0 Then
                '    For i As Integer = 0 To tb.Rows.Count - 1
                '        Dim data As New Dictionary(Of String, Object)()
                '        If i = 0 Then data.Add("sw", True)
                '        data.Add("cCodpso", objCRM.EncrytedString64(tb.Rows(i).Item("codigo_Pso")))
                '        data.Add("nTipoDoc", tb.Rows(i).Item("TipoDoc").ToString)
                '        data.Add("nParticipante", tb.Rows(i).Item("Participante").ToString())
                '        data.Add("cCodUni", tb.Rows(i).Item("CodUniversitario"))
                '        data.Add("nCicloIng", tb.Rows(i).Item("cicloIng_Alu").ToString())
                '        data.Add("mCargoTotal", tb.Rows(i).Item(6))
                '        data.Add("mAbonoTotal", tb.Rows(i).Item(7))
                '        data.Add("mSaldoTotal", tb.Rows(i).Item(8))
                '        list.Add(data)
                '    Next
                'End If

                list.Add(data)


                JSONresult = serializer.Serialize(list)
                Response.Write(JSONresult)

            ElseIf tipo = "APE" Then
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()

                '==================================
                'Buscar a la Persona
                '==================================
                tb = obj.TraerDataTable("dbo.PERSON_ConsultarPersona", tipo, valor)
                If tb.Rows.Count > 0 Then
                    For i As Integer = 0 To tb.Rows.Count - 1
                        Dim data As New Dictionary(Of String, Object)()
                        If i = 0 Then
                            data.Add("sw", True)
                            data.Add("msje", "No se encontraron datos con los apellidos: " & valor)
                        End If


                        data.Add("cod", objCRM.EncrytedString64(tb.Rows(i).Item("codigo_Pso")))
                        data.Add("apepat", tb.Rows(i).Item("apellidoPaterno_Pso").ToString)
                        data.Add("apemat", tb.Rows(i).Item("apellidoMaterno_Pso").ToString())
                        data.Add("nombre", tb.Rows(i).Item("nombres_pso").ToString)
                        data.Add("nrodocident", tb.Rows(i).Item("numeroDocIdent_Pso").ToString())
                        data.Add("fechanac", Mid(tb.Rows(i).Item("fechanacimiento_pso").ToString(), 1, 10))
                        data.Add("email", tb.Rows(i).Item("emailPrincipal_Pso").ToString())
                        data.Add("direccion", tb.Rows(i).Item("direccion_pso").ToString())
                        list.Add(data)
                    Next
                End If

                obj = Nothing

                JSONresult = serializer.Serialize(list)
                Response.Write(JSONresult)
            ElseIf tipo = "COE" Then
                Dim data As New Dictionary(Of String, Object)()
                Dim tbcco As New Data.DataTable
                Dim ExistePersona As Boolean = False
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()

                Dim codpso As String = objCRM.DecrytedString64(valor)

                '==================================
                'Buscar Centro de Costo
                '==================================
                tbcco = obj.TraerDataTable("dbo.EVE_ConsultarEventos", "2", cco, "0")
                '==================================
                'Buscar a la Persona
                '==================================
                tb = obj.TraerDataTable("dbo.PERSON_ConsultarPersona", tipo, codpso)
                If tb.Rows.Count > 0 Then

                    ExistePersona = True
                    Dim tblalumno As New Data.DataTable
                    '==================================
                    'Buscar datos del alumno
                    '==================================
                    tblalumno = obj.TraerDataTable("PERSON_ConsultarAlumnoPersona", 0, tb.Rows(0).Item("codigo_Pso"), cco, 0)

                    If tblalumno.Rows.Count > 0 Then
                        data.Add("codMin", objCRM.EncrytedString64(tblalumno.Rows(0).Item("codigo_min").ToString))
                        If tbcco.Rows(0).Item("codigo_cpf") = 9 Then
                            'Me.dpModalidad.Enabled = False
                            data.Add("bloqMin", True)
                        Else
                            data.Add("bloqMin", False)
                        End If
                    End If

                    tblalumno.Dispose()
                    tblalumno = Nothing

                    data.Add("dpto", tb.Rows(0).Item("codigo_dep"))
                    data.Add("prov", tb.Rows(0).Item("codigo_pro"))
                    data.Add("dist", tb.Rows(0).Item("codigo_Dis"))


                End If
                obj.CerrarConexion()
                obj = Nothing

                If ExistePersona = True Then
                    data.Add("sw", True)
                    If mostrardni = True Then
                        If tb.Rows(0).Item("numeroDocIdent_Pso").ToString <> "" Then
                            data.Add("nrodoc", tb.Rows(0).Item("numeroDocIdent_Pso").ToString)
                            data.Add("bloqNrodoc", False)
                        Else
                            data.Add("bloqNrodoc", True)
                        End If
                    Else
                        data.Add("bloqNrodoc", False)
                    End If
                    data.Add("apepat", tb.Rows(0).Item("apellidoPaterno_Pso").ToString())
                    data.Add("apemat", tb.Rows(0).Item("apellidoMaterno_Pso").ToString())
                    data.Add("nombre", tb.Rows(0).Item("nombres_Pso").ToString())

                    If tb.Rows(0).Item("fechanacimiento_pso").ToString <> "" Then
                        data.Add("fecnac", CDate(tb.Rows(0).Item("fechaNacimiento_Pso")).ToShortDateString())
                    End If


                    '==================================
                    'Buscar Dpto/Prov/Distrito Persona
                    '==================================
                    data.Add("codPso", objCRM.EncrytedString64(tb.Rows(0).Item("codigo_Pso").ToString()))

                    If (tb.Rows(0).Item("estadoCivil_Pso") Is System.DBNull.Value = False) AndAlso (tb.Rows(0).Item("estadoCivil_Pso").ToString.Trim <> "") Then
                        data.Add("estadocivil", tb.Rows(0).Item("estadoCivil_Pso").ToString.ToUpper())
                    End If

                    If (tb.Rows(0).Item("sexo_Pso") Is System.DBNull.Value = False) AndAlso (tb.Rows(0).Item("sexo_Pso").ToString.Trim <> "") Then
                        data.Add("sexo", tb.Rows(0).Item("sexo_Pso").ToString.ToUpper)
                    Else
                        data.Add("sexo", "")
                    End If

                    data.Add("tipodoc", tb.Rows(0).Item("tipoDocIdent_Pso").ToString())
                    data.Add("emailpri", tb.Rows(0).Item("emailPrincipal_Pso").ToString())
                    data.Add("emailalt", tb.Rows(0).Item("emailAlternativo_Pso").ToString())
                    data.Add("direccion", tb.Rows(0).Item("direccion_Pso").ToString())
                    data.Add("fonofijo", tb.Rows(0).Item("telefonoFijo_Pso").ToString())
                    data.Add("fonomovil", tb.Rows(0).Item("telefonoCelular_Pso").ToString())
                    data.Add("ruc", tb.Rows(0).Item("nroRuc_Pso").ToString())
                    data.Add("nrodoc", tb.Rows(0).Item("numeroDocIdent_Pso").ToString())
                    data.Add("dblNom", True)
                    data.Add("dblOtrosDatos", True)

                Else
                    data.Add("sw", False)
                    data.Add("alert", "warning")
                    data.Add("msje", "No se encontraron datos con el nro de documento: " & valor)
                    data.Add("codPso", "")
                    data.Add("dblNom", True)
                    data.Add("dblOtrosDatos", False)
                End If
                tb.Dispose()
                tb = Nothing
                list.Add(data)

                JSONresult = serializer.Serialize(list)
                Response.Write(JSONresult)

            End If
        Catch ex As Exception
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim data As New Dictionary(Of String, Object)()
            data.Add("msj", False)
            data.Add("error", ex.Message.ToString)
            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Function GeneraLetra() As String
        GeneraLetra = Chr(((Rnd() * 100) Mod 25) + 65)
    End Function
    Private Function GeneraClave() As String
        Randomize()
        Dim Letras As String
        Dim Numeros As String
        Letras = GeneraLetra() & GeneraLetra()
        Numeros = Format((Rnd() * 8888) + 1111, "0000")
        GeneraClave = Letras & Numeros
    End Function


#Region "Propiedades Persona"
    Private _codigo_pso As Integer
    Public Property CodigoPso() As Integer
        Get
            Return _codigo_pso
        End Get
        Set(ByVal value As Integer)
            _codigo_pso = value
        End Set
    End Property
    Private _tipodoc As String
    Public Property TipoDoc() As String
        Get
            Return _tipodoc
        End Get
        Set(ByVal value As String)
            _tipodoc = value
        End Set
    End Property
    Private _nrodoc As String
    Public Property NroDoc() As String
        Get
            Return _nrodoc
        End Get
        Set(ByVal value As String)
            _nrodoc = value
        End Set
    End Property
    Private _apepat As String
    Public Property ApePat() As String
        Get
            Return _apepat
        End Get
        Set(ByVal value As String)
            _apepat = value
        End Set
    End Property
    Private _apemat As String
    Public Property ApeMat() As String
        Get
            Return _apemat
        End Get
        Set(ByVal value As String)
            _apemat = value
        End Set
    End Property
    Private _nombre As String
    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property
    Private _fechanac As Date
    Public Property FechaNac() As String
        Get
            Return _fechanac
        End Get
        Set(ByVal value As String)
            _fechanac = value
        End Set
    End Property
    Private _sexo As String
    Public Property Sexo() As String
        Get
            Return _sexo
        End Get
        Set(ByVal value As String)
            _sexo = value
        End Set
    End Property
    Private _estadoCivil As String
    Public Property EstadoCivil() As String
        Get
            Return _estadoCivil
        End Get
        Set(ByVal value As String)
            _estadoCivil = value
        End Set
    End Property
    Private _emailPrincipal As String
    Public Property EmailPrincipal() As String
        Get
            Return _emailPrincipal
        End Get
        Set(ByVal value As String)
            _emailPrincipal = value
        End Set
    End Property
    Private _emailAlternativo As String
    Public Property EmailAlternativo() As String
        Get
            Return _emailAlternativo
        End Get
        Set(ByVal value As String)
            _emailAlternativo = value
        End Set
    End Property
    Private _direccion As String
    Public Property Direccion() As String
        Get
            Return _direccion
        End Get
        Set(ByVal value As String)
            _direccion = value
        End Set
    End Property
    Private _codDpto As String
    Public Property CodDepatarmento() As String
        Get
            Return _codDpto
        End Get
        Set(ByVal value As String)
            _codDpto = value
        End Set
    End Property
    Private _codProv As String
    Public Property CodProvincia() As String
        Get
            Return _codProv
        End Get
        Set(ByVal value As String)
            _codProv = value
        End Set
    End Property
    Private _codDist As String
    Public Property CodDist() As String
        Get
            Return _codDist
        End Get
        Set(ByVal value As String)
            _codDist = value
        End Set
    End Property
    Private _telefonofijo As String
    Public Property TelefonoFijo() As String
        Get
            Return _telefonofijo
        End Get
        Set(ByVal value As String)
            _telefonofijo = value
        End Set
    End Property
    Private _telefonomovil As String
    Public Property TelefonoMovil() As String
        Get
            Return _telefonomovil
        End Get
        Set(ByVal value As String)
            _telefonomovil = value
        End Set
    End Property
    Private _codmodalidadingreso As Integer
    Public Property CodModadlidadIngreso() As Integer
        Get
            Return _codmodalidadingreso
        End Get
        Set(ByVal value As Integer)
            _codmodalidadingreso = value
        End Set
    End Property
    Private _codigo_cco As Integer
    Public Property CodCentroCosto() As Integer
        Get
            Return _codigo_cco
        End Get
        Set(ByVal value As Integer)
            _codigo_cco = value
        End Set
    End Property

    Private _ruc As String
    Public Property Ruc() As String
        Get
            Return _ruc
        End Get
        Set(ByVal value As String)
            _ruc = value
        End Set
    End Property


    Private _codpersona As Integer
    Public Property codpersona() As Integer
        Get
            Return _codpersona
        End Get
        Set(ByVal value As Integer)
            _codpersona = value
        End Set
    End Property


#End Region

    'Sub New(ByVal codigo_pso As Integer, ByVal tipodoc As String, ByVal nrodoc As String, ByVal apepat As String, ByVal apemat As String, ByVal nombre As String, _
    '        ByVal fechanac As Date, ByVal sexo As String, ByVal estadocivil As String, ByVal emaiprincipal As String, ByVal emailalternativo As String, _
    '        ByVal direccion As String, ByVal coddpto As String, ByVal codprov As String, ByVal coddist As String, ByVal telefonofijo As String, ByVal telefonomovil As String, _
    '        ByVal codmodalidadingreso As Integer, ByVal codcentrocosto As Integer)
    '    _codigo_pso = codigo_pso
    '    _tipodoc = tipodoc
    '    _nrodoc = nrodoc
    '    _apepat = apepat
    '    _apemat = apemat
    '    _nombre = nombre
    '    _fechanac = fechanac
    '    _sexo = sexo
    '    _estadoCivil = estadocivil
    '    _emailPrincipal = emaiprincipal
    '    _emailAlternativo = emailalternativo
    '    _direccion = direccion
    '    _codDpto = coddpto
    '    _codProv = codprov
    '    _codDist = coddist
    '    _telefonofijo = telefonofijo
    '    _telefonomovil = telefonomovil
    '    _codmodalidadingreso = codmodalidadingreso
    '    _codigo_cco = codcentrocosto
    'End Sub
    Sub New()

    End Sub

End Class
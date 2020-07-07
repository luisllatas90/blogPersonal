'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class Encuesta_AcreditacionUniversitaria_generales
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim DatAlumno As New Data.DataTable
            Dim Datos As New Data.DataTable
            If Request.QueryString("sesion") <> "" Then
                Session("codigo_alu") = Request.QueryString("sesion")
            End If
            ClsFunciones.LlenarListas(CboPais, Obj.TraerDataTable("ConsultarPais", "T", ""), "codigo_pai", "nombre_pai", "--Seleccione País--")
            ClsFunciones.LlenarListas(CboEscuela, Obj.TraerDataTable("ConsultarCarreraProfesional", "TO", ""), "codigo_cpf", "nombre_cpf")
            ClsFunciones.LlenarListas(CboEscuelaActual, Obj.TraerDataTable("ConsultarCarreraProfesional", "TO", ""), "codigo_cpf", "nombre_cpf")
            ClsFunciones.LlenarListas(CboCicloAcad, Obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_Cac")
            ClsFunciones.LlenarListas(CboDepartamento, Obj.TraerDataTable("sp_ver_departamento"), "codigo_dep", "nombre_dep", "--Seleccione Departamento--")
            ClsFunciones.LlenarListas(CboDepartamentoEst, Obj.TraerDataTable("sp_ver_departamento"), "codigo_dep", "nombre_dep", "--Seleccione Departamento--")
            Datos = Obj.TraerDataTable("AUN_ConsultarEstadoAcreditacionUniversitaria", "CG", Session("codigo_alu"))

            If Datos.Rows.Count = 1 Then
                CargarCaracteristicasGenerales(sender, e)
            Else
                NoMostrarDatos()
                DatAlumno = Obj.TraerDataTable("ConsultarAlumno", "AUN", Session("codigo_alu"))
                If DatAlumno.Rows.Count > 0 Then
                     With DatAlumno.Rows(0)
                        Me.TxtApellidoPat.Text = .Item("apellidoPat_Alu").ToString
                        Me.TxtApellidoMat.Text = .Item("apellidoMat_Alu").ToString
                        Me.TxtNombres.Text = .Item("nombres_Alu").ToString
                        Me.TxtFechaNac.Text = IIf(.Item("fechaNacimiento_Alu") Is System.DBNull.Value, "", .Item("fechaNacimiento_Alu"))
                        Me.TxtInsEducativa.Text = IIf(.Item("nombreColegio_Dal").ToString Is System.DBNull.Value, "", .Item("nombreColegio_Dal").ToString)
                        Dim CicloAcad As String
                        CicloAcad = IIf(.Item("SemIngreso").ToString Is System.DBNull.Value, "", .Item("SemIngreso").ToString)
                        For i As Int16 = 0 To Me.CboCicloAcad.Items.Count - 1
                            If CicloAcad = CboCicloAcad.Items(i).Text Then
                                CboCicloAcad.SelectedIndex = i
                            End If
                        Next
                        Me.CboEscuela.SelectedValue = IIf(.Item("EscuelaProfesional") Is System.DBNull.Value, "", .Item("EscuelaProfesional"))
                        Me.CboEscuelaActual.SelectedValue = IIf(.Item("EscuelaProfesional") Is System.DBNull.Value, "", .Item("EscuelaProfesional"))
                        Me.RbtSexo.SelectedValue = .Item("Sexo_Alu").ToString
                        Me.CboEstadoCivil.Text = IIf(.Item("estadoCivil_Dal").ToString Is System.DBNull.Value, "", .Item("estadoCivil_Dal").ToString)
                        'Me.CboDepartamento.SelectedValue = 13
                        Me.CboDepartamento_SelectedIndexChanged(sender, e)
                        Me.CboProvincia_SelectedIndexChanged(sender, e)
                        Me.CboPais.SelectedValue = 156
                        'Me.TxtInsEducativa.Text = IIf(.Item("nombre_col") Is System.DBNull.Value, "", .Item("nombre_col"))
                        Me.CboDepartamentoEst_SelectedIndexChanged(sender, e)
                        Me.CboProvinciaEst_SelectedIndexChanged(sender, e)
                        Me.CboDistritoEst.Text = IIf(.Item("nombredis_Col") Is System.DBNull.Value, "", .Item("nombredis_Col"))
                        Me.CboProvinciaEst.Text = IIf(.Item("nombrepro_Col") Is System.DBNull.Value, "", .Item("nombrepro_Col"))
                        Me.CboDepartamentoEst.Text = IIf(.Item("nombredep_Col") Is System.DBNull.Value, "", .Item("nombredep_Col"))
                        Me.CboCicloAcad.Enabled = False
                        Me.CboEscuela.Enabled = False
                    End With
                End If
            End If
        End If
    End Sub

    Private Sub NoMostrarDatos()
        Me.LblReligion.Visible = False
        Me.TxtReligion.Visible = False
        Me.LblSeguro.Visible = False
        Me.TxtSeguro.Visible = False
        Me.LblIglesia.Visible = False
        Me.TxtIglesia.Visible = False
        Me.LblDiscapacidad.Visible = False
        Me.TxtDiscapacidad.Visible = False
        Me.LblHijos.Visible = False
        Me.TxtNroHijos.Visible = False
        Me.CboReligion.Visible = False
    End Sub

    Private Sub CargarCaracteristicasGenerales(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim DatAlumno As New Data.DataTable
        DatAlumno = Obj.TraerDataTable("AUN_ConsultarAcreditacionUniversitaria", "CG", Session("codigo_alu"))
        If DatAlumno.Rows.Count > 0 Then
            With DatAlumno.Rows(0)
                TxtApellidoPat.Text = .Item("ApellidoPat_Aun").ToString
                TxtApellidoMat.Text = .Item("ApellidoMat_Aun").ToString
                TxtNombres.Text = .Item("NombresAlu_Aun").ToString
                TxtFechaNac.Text = .Item("FechaNac_Aun").ToString
                CboDepartamento.SelectedValue = .Item("DepartamentoNac_Aun")
                CboDepartamento_SelectedIndexChanged(sender, e)
                CboProvincia.SelectedValue = .Item("ProvinciaNac_Aun")
                CboProvincia_SelectedIndexChanged(sender, e)
                CboDistrito.SelectedValue = .Item("DistritoNac_Aun")
                CboPais.SelectedValue = .Item("PaisNac_Aun")
                TxtInsEducativa.Text = .Item("InstitucionEduSec_Aun").ToString
                CboDepartamentoEst.SelectedValue = Session("codigo_dep")
                CboProvinciaEst.SelectedValue = Session("codigo_pro")
                CboDistritoEst.SelectedValue = Session("codigo_dis")
                CboDepartamentoEst_SelectedIndexChanged(sender, e)
                CboProvinciaEst_SelectedIndexChanged(sender, e)
                CboTipoColegio.SelectedValue = Left(.Item("TipoInstitucionEdu_Aun"), 1)
                CboCicloAcad.SelectedValue = .Item("CicloIngreso_Aun")
                CboEscuela.SelectedValue = .Item("EscuelaIngreso_Aun")
                CboEscuelaActual.SelectedValue = .Item("EscuelaActual_Aun")
                CboIngreso.SelectedValue = .Item("Codigo_Mia")
                TxtOrigen.Text = .Item("DescripcionIngreso_Aun").ToString
                TxtPorConvenio.Text = .Item("PorConvenio_Aun").ToString
                CboModalidadEst.SelectedValue = Left(.Item("ModalidadEstudio_Aun"), 1)
                TxtEdad.Text = .Item("Edad_Aun").ToString
                RbtSexo.SelectedValue = .Item("Sexo_Aun")
                ChkEnCasa.Checked = CBool(.Item("PreparacionCasa_Aun"))
                ChkAcademias.Checked = CBool(.Item("PreparacionAcademia_Aun"))
                TxtAcademia.Text = .Item("Academia_Aun").ToString
                ChkCentroPre.Checked = CBool(.Item("PreparacionCentroUniv_Aun"))
                TxtCentroPre.Text = .Item("CentroPreUniv_Aun").ToString
                CboHablaIng.SelectedValue = Left(.Item("InglesHabla_Aun"), 1)
                CboLeeIng.SelectedValue = Left(.Item("InglesLee_Aun"), 1)
                CboEscribeIng.SelectedValue = Left(.Item("InglesEscribe_Aun"), 1)
                TxtInstitucionIng.Text = .Item("InglesInstitucion_Aun").ToString
                RbtCertificacionIng.SelectedValue = .Item("InglesTieneCertificacion_Aun")
                TxtCertificacionIng.Text = .Item("InglesCertificacion_Aun").ToString
                CboHablaFra.SelectedValue = Left(.Item("FrancesHabla_Aun"), 1)
                CboLeeFra.SelectedValue = Left(.Item("FrancesLee_Aun"), 1)
                CboEscribeFra.SelectedValue = Left(.Item("FrancesEscribe_Aun"), 1)
                TxtInstitucionFra.Text = .Item("FrancesInstitucion_Aun").ToString
                RbtCertificacionFra.SelectedValue = .Item("FrancesTieneCertificacion_Aun")
                TxtCertificacionFra.Text = .Item("FrancesCertificacion_Aun").ToString
                CboHablaIta.SelectedValue = Left(.Item("ItalianoHabla_Aun"), 1)
                CboLeeIta.SelectedValue = Left(.Item("ItalianoLee_Aun"), 1)
                CboEscribeIta.SelectedValue = Left(.Item("ItalianoEscribe_Aun"), 1)
                TxtInstitucionIta.Text = .Item("ItalianoInstitucion_Aun").ToString
                RbtCertificacionIta.SelectedValue = .Item("ItalianoTieneCertificacion_Aun")
                TxtCertificacionIta.Text = .Item("ItalianoCertificacion_Aun").ToString
                CboHablaOtr.SelectedValue = Left(.Item("OtroHabla_Aun"), 1)
                CboLeeOtr.SelectedValue = Left(.Item("OtroLee_Aun"), 1)
                CboEscribeOtr.SelectedValue = Left(.Item("OtroEscribe_Aun"), 1)
                TxtInstitucionOtr.Text = .Item("OtroInstitucion_Aun").ToString
                RbtCertificacionOtr.SelectedValue = .Item("OtroTieneCertificacion_Aun")
                TxtCertificacionOtr.Text = .Item("OtroCertificacion_Aun").ToString
                RbtDiscapacidad.SelectedValue = .Item("EsDescapacitado_Aun")
                TxtDiscapacidad.Text = .Item("Discapacidad_Aun").ToString
                RbtSeguro.SelectedValue = .Item("TieneSeguro_Aun")
                TxtSeguro.Text = .Item("CoberturaSeguro_Aun").ToString
                RbtReligion.SelectedValue = .Item("ProfesaReligion_Aun")
                If Left(.Item("Religon_Aun"), 1) = 1 Then
                    CboReligion.SelectedValue = Left(.Item("Religon_Aun"), 1)
                Else
                    TxtReligion.Text = Mid(.Item("Religon_Aun"), 1, .Item("Religon_Aun").ToString.Length).ToString
                End If
                ChkSacramentos.Items(0).Selected = CBool(.Item("Bautizo_Aun"))
                ChkSacramentos.Items(1).Selected = CBool(.Item("Reconciliacion_Aun"))
                ChkSacramentos.Items(2).Selected = CBool(.Item("Eucaristia_Aun"))
                ChkSacramentos.Items(3).Selected = CBool(.Item("Confirmacion_Aun"))
                ChkSacramentos.Items(4).Selected = CBool(.Item("UncionEnfermos_Aun"))
                ChkSacramentos.Items(5).Selected = CBool(.Item("MatrimonioIglesia_Aun"))
                ChkSacramentos.Items(6).Selected = CBool(.Item("OrdenSacerdotal_Aun"))
                RbtReconciliacion.SelectedValue = Left(.Item("ReconciliacionSac_Aun"), 1)
                RbtEucaristia.SelectedValue = Left(.Item("EucaristiaSac_Aun"), 1)
                RbtIglesia.SelectedValue = .Item("ParticipaIglesia_Aun")
                TxtIglesia.Text = .Item("GrupoParroquial_Aun").ToString
                CboEstadoCivil.SelectedValue = Left(.Item("EstadoCivil_Aun"), 1)
                RbtHijos.SelectedValue = .Item("TieneHijos_Aun")
                TxtNroHijos.Text = .Item("NumeroHijos_Aun")
                TxtNombreHijo1.Text = .Item("NombreHijo1_Aun").ToString
                RbtSexoHijo1.SelectedValue = IIf(.Item("SexoHijo1_Aun") Is System.DBNull.Value, "M", .Item("SexoHijo1_Aun"))
                Me.TxtFechaHijo1.Text = IIf(.Item("FechaNacHijo1_Aun") Is System.DBNull.Value, "", .Item("FechaNacHijo1_Aun"))
                TxtNombreHijo2.Text = .Item("NombreHijo2_Aun").ToString
                RbtSexoHijo2.SelectedValue = IIf(.Item("SexoHijo2_Aun") Is System.DBNull.Value, "M", .Item("SexoHijo2_Aun"))
                Me.TxtFechaHijo2.Text = IIf(.Item("FechaNacHijo2_Aun") Is System.DBNull.Value, "", .Item("FechaNacHijo2_Aun"))
                TxtNombreHijo3.Text = .Item("NombreHijo3_Aun").ToString
                RbtSexoHijo3.SelectedValue = IIf(.Item("SexoHijo3_Aun") Is System.DBNull.Value, "M", .Item("SexoHijo3_Aun"))
                Me.TxtFechaHijo3.Text = IIf(.Item("FechaNacHijo3_Aun") Is System.DBNull.Value, "", .Item("FechaNacHijo3_Aun"))
                TxtNombreHijo4.Text = .Item("NombreHijo4_Aun").ToString
                RbtSexoHijo4.SelectedValue = IIf(.Item("SexoHijo4_Aun") Is System.DBNull.Value, "M", .Item("SexoHijo4_Aun"))
                Me.TxtFechaHijo4.Text = IIf(.Item("FechaNacHijo4_Aun") Is System.DBNull.Value, "", .Item("FechaNacHijo4_Aun"))
                TxtNombreHijo5.Text = .Item("NombreHijo5_Aun").ToString
                RbtSexoHijo5.SelectedValue = IIf(.Item("SexoHijo5_Aun") Is System.DBNull.Value, "M", .Item("SexoHijo5_Aun"))
                Me.TxtFechaHijo5.Text = IIf(.Item("FechaNacHijo5_Aun") Is System.DBNull.Value, "", .Item("FechaNacHijo5_Aun"))
                TxtDni.Text = .Item("Dni_Aun").ToString
            End With
            RbtDiscapacidad_SelectedIndexChanged(sender, e)
            RbtSeguro_SelectedIndexChanged(sender, e)
            RbtReligion_SelectedIndexChanged(sender, e)
            RbtIglesia_SelectedIndexChanged(sender, e)
            RbtHijos_SelectedIndexChanged(sender, e)
            CboReligion_SelectedIndexChanged(sender, e)
            Me.CboCicloAcad.Enabled = False
            Me.CboEscuela.Enabled = False
        End If
    End Sub

    Protected Sub CboDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboDepartamento.SelectedIndexChanged
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(CboProvincia, Obj.TraerDataTable("sp_ver_provincia", Me.CboDepartamento.SelectedValue), "codigo_pro", "nombre_pro", "--Seleccione Provincia--")
    End Sub

    Protected Sub CboProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboProvincia.SelectedIndexChanged
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(CboDistrito, Obj.TraerDataTable("sp_ver_distrito", Me.CboProvincia.SelectedValue), "codigo_dis", "nombre_dis", "--Seleccione Distrito--")
    End Sub

    Protected Sub CboIngreso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboIngreso.SelectedIndexChanged
        If Me.CboIngreso.SelectedValue = 5 Then
            Me.LblEspecifique.Visible = True
            Me.LblEspecifique.Text = "Especifique Univ. de origen"
            Me.LblPorConvenio.Visible = False
            Me.LblPorConvenio.Visible = False
            Me.TxtPorConvenio.Visible = False
            Me.TxtOrigen.Visible = True
        ElseIf Me.CboIngreso.SelectedValue = 6 Then
            Me.LblEspecifique.Visible = True
            Me.LblEspecifique.Text = "Especifique Carrera de origen"
            Me.LblPorConvenio.Visible = True
            Me.TxtPorConvenio.Visible = True
            Me.TxtOrigen.Visible = True
        Else
            Me.LblEspecifique.Visible = False
            Me.LblPorConvenio.Visible = False
            Me.TxtPorConvenio.Visible = False
            Me.TxtOrigen.Visible = False
        End If
    End Sub

    Private Function VerificarHijo(ByVal nombre As String, ByVal sexo As String, ByVal fecha As String, ByVal nro As Int16, ByVal mensaje As String) As Int16
        If nombre = "" Or sexo = "" Or fecha = "" Then
            RegisterStartupScript("Hijo" & nro.ToString, "<script>alert('Indique los datos del " & mensaje & " hijo')</script>")
            Return 1
        Else
            Return 0
        End If
    End Function
    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim sw As Int16 = 0
        Try
            If Me.RbtHijos.SelectedValue = 1 Then
                If CboPais.SelectedValue <> 156 Then
                    
                End If

                If Me.TxtNroHijos.Text = "" Then
                    Me.LblHijos.ForeColor = Drawing.Color.Red
                    RegisterStartupScript("NroHijos", "<script>alert('Debe indicar el nro de hijos que tiene')</script>")
                    Me.TxtNroHijos.Focus()
                    Exit Sub
                Else
                    Select Case TxtNroHijos.Text
                        Case 1
                            sw = VerificarHijo(TxtNombreHijo1.Text, RbtSexoHijo1.SelectedValue, TxtFechaHijo1.Text, 1, "primer")
                        Case 2
                            sw += VerificarHijo(TxtNombreHijo1.Text, RbtSexoHijo1.SelectedValue, TxtFechaHijo1.Text, 1, "primer")
                            sw += VerificarHijo(TxtNombreHijo2.Text, RbtSexoHijo2.SelectedValue, TxtFechaHijo2.Text, 2, "segundo")
                        Case 3
                            sw += VerificarHijo(TxtNombreHijo1.Text, RbtSexoHijo1.SelectedValue, TxtFechaHijo1.Text, 1, "primer")
                            sw += VerificarHijo(TxtNombreHijo2.Text, RbtSexoHijo2.SelectedValue, TxtFechaHijo2.Text, 2, "segundo")
                            sw += VerificarHijo(TxtNombreHijo3.Text, RbtSexoHijo3.SelectedValue, TxtFechaHijo3.Text, 3, "tercer")
                        Case 4
                            sw += VerificarHijo(TxtNombreHijo1.Text, RbtSexoHijo1.SelectedValue, TxtFechaHijo1.Text, 1, "primer")
                            sw += VerificarHijo(TxtNombreHijo2.Text, RbtSexoHijo2.SelectedValue, TxtFechaHijo2.Text, 2, "segundo")
                            sw += VerificarHijo(TxtNombreHijo3.Text, RbtSexoHijo3.SelectedValue, TxtFechaHijo3.Text, 3, "tercer")
                            sw += VerificarHijo(TxtNombreHijo4.Text, RbtSexoHijo4.SelectedValue, TxtFechaHijo4.Text, 4, "cuarto")
                        Case 5
                            sw += VerificarHijo(TxtNombreHijo1.Text, RbtSexoHijo1.SelectedValue, TxtFechaHijo1.Text, 1, "primer")
                            sw += VerificarHijo(TxtNombreHijo2.Text, RbtSexoHijo2.SelectedValue, TxtFechaHijo2.Text, 2, "segundo")
                            sw += VerificarHijo(TxtNombreHijo3.Text, RbtSexoHijo3.SelectedValue, TxtFechaHijo3.Text, 3, "tercer")
                            sw += VerificarHijo(TxtNombreHijo4.Text, RbtSexoHijo4.SelectedValue, TxtFechaHijo4.Text, 4, "cuarto")
                            sw += VerificarHijo(TxtNombreHijo5.Text, RbtSexoHijo5.SelectedValue, TxtFechaHijo5.Text, 5, "quinto")
                    End Select
                    If sw > 0 Then
                        Exit Sub
                    End If
                End If
            End If
            If Me.RbtIglesia.SelectedValue = 1 Then
                If Me.TxtIglesia.Text = "" Then
                    RegisterStartupScript("Iglesia", "<script>alert('Especifique grupo parroquial o movimiento de la iglesia a la que pertenece')</script>")
                    LblIglesia.ForeColor = Drawing.Color.Red
                    TxtIglesia.Focus()
                    Exit Sub
                End If
            End If
            If Me.RbtReligion.SelectedValue = 1 Then
                If Me.CboReligion.SelectedValue = 2 Then
                    If Me.TxtReligion.Text = "" Then
                        RegisterStartupScript("Religion", "<script>alert('Especifique la religión que profesa')</script>")
                        LblReligion.ForeColor = Drawing.Color.Red
                        TxtReligion.Focus()
                        Exit Sub
                    End If
                End If
            End If
            If Me.RbtSeguro.SelectedValue = 1 Then
                If Me.TxtSeguro.Text = "" Then
                    RegisterStartupScript("Seguro", "<script>alert('Especifique con que seguro cuenta')</script>")
                    LblSeguro.ForeColor = Drawing.Color.Red
                    TxtSeguro.Focus()
                    Exit Sub
                End If
            End If
            If Me.RbtDiscapacidad.SelectedValue = 1 Then
                If Me.TxtDiscapacidad.Text = "" Then
                    RegisterStartupScript("Discapacidad", "<script>alert('Especifique que discapacidad presenta')</script>")
                    LblDiscapacidad.ForeColor = Drawing.Color.Red
                    TxtDiscapacidad.Focus()
                    Exit Sub
                End If
            End If
            If Me.CboIngreso.SelectedValue = 5 Or CboIngreso.SelectedValue = 6 Then
                If Me.TxtOrigen.Text = "" Then
                    RegisterStartupScript("Seguro", "<script>alert('Especifique origen de donde procede')</script>")
                    LblEspecifique.ForeColor = Drawing.Color.Red
                    LblPorConvenio.ForeColor = Drawing.Color.Red
                    TxtOrigen.Focus()
                    Exit Sub
                End If
            End If
            If Me.TxtEdad.Text >= 19 Then
                If Me.TxtDni.Text.Length < 8 Then
                    RegisterStartupScript("Dni", "<script>alert('El DNI debe contener 8 dígitos')</script>")
                    Exit Sub
                End If
            End If
            If ChkAcademias.Checked = True Then
                If Me.TxtAcademia.Text = "" Then
                    RegisterStartupScript("Academia", "<script>alert('Especifique en que Academia se preparó')</script>")
                    Me.LblAcademia.ForeColor = Drawing.Color.Red
                    Me.TxtAcademia.Focus()
                    Exit Sub
                End If
            End If
            If ChkCentroPre.Checked = True Then
                If TxtCentroPre.Text = "" Then
                    RegisterStartupScript("Centro Pre", "<script>alert('Especifique en que Centro Pre se preparó')</script>")
                    Me.LblCentroPre.ForeColor = Drawing.Color.Red
                    Me.TxtCentroPre.Focus()
                    Exit Sub
                End If
            End If
            GuardarDatos()
            VerificarAcreditacionUniversitariaCompleta()
            Exit Sub
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub GuardarDatos()
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Session("codigo_dis") = Me.CboDistritoEst.SelectedValue
        Session("codigo_pro") = Me.CboProvinciaEst.SelectedValue
        Session("codigo_Dep") = Me.CboDepartamentoEst.SelectedValue
        Session("codigo_aun") = Obj.Ejecutar("AUN_InsertarDatosGenerales", _
                                  CInt(Session("codigo_alu")), _
                                  Me.TxtApellidoPat.Text, _
                                  Me.TxtApellidoMat.Text, _
                                  Me.TxtNombres.Text, _
                                  Me.TxtFechaNac.Text, _
                                  Me.CboDepartamento.SelectedValue, _
                                  Me.CboProvincia.SelectedValue, _
                                  Me.CboDistrito.SelectedValue, _
                                  Me.CboPais.SelectedValue, _
                                  Me.TxtInsEducativa.Text, _
                                  Me.CboDistritoEst.SelectedItem.Text & " - " & Me.CboProvinciaEst.SelectedItem.Text & " - " & Me.CboDepartamentoEst.SelectedItem.Text, _
                                  Me.CboTipoColegio.SelectedValue & Me.CboTipoColegio.SelectedItem.Text.Trim, _
                                  Me.CboCicloAcad.SelectedValue, _
                                  Me.CboEscuela.SelectedValue, _
                                  Me.CboEscuelaActual.SelectedValue, _
                                  Me.CboIngreso.SelectedValue, _
                                  Me.TxtOrigen.Text, _
                                  Me.TxtPorConvenio.Text, _
                                  Me.CboModalidadEst.SelectedValue & Me.CboModalidadEst.SelectedItem.Text, _
                                  Me.TxtEdad.Text, _
                                  Me.RbtSexo.SelectedValue, _
                                  IIf(Me.ChkEnCasa.Checked, 1, 0), _
                                  IIf(Me.ChkAcademias.Checked, 1, 0), _
                                  Me.TxtAcademia.Text, _
                                  IIf(Me.ChkCentroPre.Checked, 1, 0), _
                                  Me.TxtCentroPre.Text, _
                                  Me.CboHablaIng.SelectedValue & Me.CboHablaIng.SelectedItem.Text, _
                                  Me.CboLeeIng.SelectedValue & Me.CboLeeIng.SelectedItem.Text, _
                                  Me.CboEscribeIng.SelectedValue & Me.CboEscribeIng.SelectedItem.Text, _
                                  Me.TxtInstitucionIng.Text, _
                                  Me.RbtCertificacionIng.SelectedValue, _
                                  Me.TxtCertificacionIng.Text, _
                                  Me.CboHablaFra.SelectedValue & Me.CboHablaFra.SelectedItem.Text, _
                                  Me.CboLeeFra.SelectedValue & Me.CboLeeFra.SelectedItem.Text, _
                                  Me.CboEscribeFra.SelectedValue & Me.CboEscribeFra.SelectedItem.Text, _
                                  Me.TxtInstitucionFra.Text, _
                                  Me.RbtCertificacionFra.SelectedValue, _
                                  Me.TxtCertificacionFra.Text, _
                                  Me.CboHablaIta.SelectedValue & Me.CboHablaIta.SelectedItem.Text, _
                                  Me.CboLeeIta.SelectedValue & Me.CboLeeIta.SelectedItem.Text, _
                                  Me.CboEscribeIta.SelectedValue & Me.CboEscribeIta.SelectedItem.Text, _
                                  Me.TxtInstitucionIta.Text, _
                                  Me.RbtCertificacionIta.SelectedValue, _
                                  Me.TxtCertificacionIta.Text, _
                                  Me.CboHablaOtr.SelectedValue & Me.CboHablaOtr.SelectedItem.Text, _
                                  Me.CboLeeOtr.SelectedValue & Me.CboLeeOtr.SelectedItem.Text, _
                                  Me.CboEscribeIta.SelectedValue & Me.CboEscribeIta.SelectedItem.Text, _
                                  Me.TxtInstitucionOtr.Text, _
                                  Me.RbtCertificacionOtr.SelectedValue, _
                                  Me.TxtCertificacionOtr.Text, _
                                  Me.RbtDiscapacidad.SelectedValue, _
                                  Me.TxtDiscapacidad.Text, _
                                  Me.RbtSeguro.SelectedValue, _
                                  Me.TxtSeguro.Text, _
                                  Me.RbtReligion.SelectedValue, _
                                  IIf(Me.CboReligion.SelectedValue = 1, "1" & Me.CboReligion.SelectedItem.Text, "2" & Me.TxtReligion.Text), _
                                  IIf(Me.ChkSacramentos.Items(0).Selected, 1, 0), _
                                  IIf(Me.ChkSacramentos.Items(1).Selected, 1, 0), _
                                  IIf(Me.ChkSacramentos.Items(2).Selected, 1, 0), _
                                  IIf(Me.ChkSacramentos.Items(3).Selected, 1, 0), _
                                  IIf(Me.ChkSacramentos.Items(4).Selected, 1, 0), _
                                  IIf(Me.ChkSacramentos.Items(5).Selected, 1, 0), _
                                  IIf(Me.ChkSacramentos.Items(6).Selected, 1, 0), _
                                  Me.RbtReconciliacion.SelectedValue & Me.RbtReconciliacion.SelectedItem.Text, _
                                  Me.RbtReconciliacion.SelectedValue & Me.RbtEucaristia.SelectedItem.Text, _
                                  Me.RbtIglesia.SelectedValue, _
                                  Me.TxtIglesia.Text, _
                                  Me.CboEstadoCivil.SelectedValue & Me.CboEstadoCivil.SelectedItem.Text, _
                                  Me.RbtHijos.SelectedValue, _
                                  CInt(Me.TxtNroHijos.Text), _
                                  Me.TxtNombreHijo1.Text, _
                                  Me.RbtSexoHijo1.SelectedValue, _
                                  IIf(Me.TxtFechaHijo1.Text = "", System.DBNull.Value, TxtFechaHijo1.Text), _
                                  Me.TxtNombreHijo2.Text, _
                                  Me.RbtSexoHijo2.SelectedValue, _
                                  IIf(Me.TxtFechaHijo2.Text = "", System.DBNull.Value, TxtFechaHijo2.Text), _
                                  Me.TxtNombreHijo3.Text, _
                                  Me.RbtSexoHijo3.SelectedValue, _
                                  IIf(Me.TxtFechaHijo3.Text = "", System.DBNull.Value, TxtFechaHijo3.Text), _
                                  Me.TxtNombreHijo4.Text, _
                                  Me.RbtSexoHijo4.SelectedValue, _
                                  IIf(Me.TxtFechaHijo4.Text = "", System.DBNull.Value, TxtFechaHijo4.Text), _
                                  Me.TxtNombreHijo5.Text, _
                                  Me.RbtSexoHijo5.SelectedValue, _
                                  IIf(Me.TxtFechaHijo5.Text = "", System.DBNull.Value, TxtFechaHijo5.Text), TxtDni.Text, 0)
    End Sub

    Private Sub VerificarAcreditacionUniversitariaCompleta()
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim datos As New Data.DataTable
        If CboCicloAcad.SelectedValue < 33 Then
            datos = Obj.TraerDataTable("AUN_ConsultarEstadoAcreditacionUniversitaria", "TO", Session("codigo_alu"))
            If datos.Rows.Count = 1 Then
                RegisterStartupScript("Acceso", "<script>alert('Gracias por llenar la encuesta, Ahora puedes acceder al Campus Virtual')</script>")
                'RegisterStartupScript("Redireccionar", "<script>location.href='http://www.usat.edu.pe/campusvirtual'</script>")
                RegisterStartupScript("Redireccionar", "<script>location.href='../..'</script>")
            Else
                Response.Redirect("investigacion.aspx")
            End If
        Else
            RegisterStartupScript("Acceso", "<script>alert('Gracias por llenar la encuesta, Ahora puedes acceder al Campus Virtual')</script>")
            'RegisterStartupScript("Redireccionar", "<script>location.href='http://www.usat.edu.pe/campusvirtual'</script>")
            RegisterStartupScript("Redireccionar", "<script>location.href='../..'</script>")
        End If
    End Sub

    Protected Sub RbtDiscapacidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtDiscapacidad.SelectedIndexChanged
        If Me.RbtDiscapacidad.SelectedValue = 1 Then
            Me.LblDiscapacidad.Visible = True
            Me.TxtDiscapacidad.Visible = True
        Else
            Me.LblDiscapacidad.Visible = False
            Me.TxtDiscapacidad.Visible = False
        End If
    End Sub

    Protected Sub CboReligion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboReligion.SelectedIndexChanged
        If Me.CboReligion.SelectedValue = 2 Then
            Me.LblReligion.Visible = True
            Me.TxtReligion.Visible = True
            Me.ChkSacramentos.Enabled = False
        Else
            Me.LblReligion.Visible = False
            Me.TxtReligion.Visible = False
            Me.ChkSacramentos.Enabled = True
        End If
    End Sub

    Protected Sub RbtHijos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtHijos.SelectedIndexChanged
        If Me.RbtHijos.SelectedValue = 1 Then
            Me.LblHijos.Visible = True
            Me.TxtNroHijos.Visible = True
        Else
            Me.LblHijos.Visible = False
            Me.TxtNroHijos.Visible = False
        End If
    End Sub

    Protected Sub RbtSeguro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtSeguro.SelectedIndexChanged
        If Me.RbtSeguro.SelectedValue = 1 Then
            Me.LblSeguro.Visible = True
            Me.TxtSeguro.Visible = True
        Else
            Me.LblSeguro.Visible = False
            Me.TxtSeguro.Visible = False
        End If
    End Sub

    Protected Sub TxtNroHijos_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNroHijos.TextChanged
        If TxtNroHijos.Text > 0 Then
            LblHijos.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Protected Sub TxtIglesia_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtIglesia.TextChanged
        If TxtIglesia.Text.Length > 0 Then
            LblIglesia.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Protected Sub TxtOrigen_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtOrigen.TextChanged
        If TxtOrigen.Text.Length > 0 Then
            LblEspecifique.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Protected Sub TxtPorConvenio_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPorConvenio.TextChanged
        If TxtPorConvenio.Text.Length > 0 Then
            LblPorConvenio.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Protected Sub TxtDiscapacidad_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDiscapacidad.TextChanged
        If TxtDiscapacidad.Text.Length > 0 Then
            LblDiscapacidad.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Protected Sub TxtSeguro_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtSeguro.TextChanged
        If TxtSeguro.Text.Length > 0 Then
            LblSeguro.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Protected Sub TxtReligion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtReligion.TextChanged
        If TxtReligion.Text.Length > 0 Then
            LblReligion.ForeColor = Drawing.Color.Black
        End If
    End Sub


    Protected Sub RbtReligion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtReligion.SelectedIndexChanged
        If RbtReligion.SelectedValue = 1 Then
            Me.CboReligion.Visible = True
        Else
            Me.CboReligion.Visible = False
        End If
    End Sub

    Protected Sub RbtIglesia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtIglesia.SelectedIndexChanged
        If RbtIglesia.SelectedValue = 1 Then
            Me.LblIglesia.Visible = True
            Me.TxtIglesia.Visible = True
        Else
            Me.LblIglesia.Visible = False
            Me.TxtIglesia.Visible = False
        End If
    End Sub

    Protected Sub CboDepartamentoEst_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboDepartamentoEst.SelectedIndexChanged
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(CboProvinciaEst, Obj.TraerDataTable("sp_ver_provincia", Me.CboDepartamentoEst.SelectedValue), "codigo_pro", "nombre_pro", "--Seleccione Provincia--")
    End Sub

    Protected Sub CboProvinciaEst_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboProvinciaEst.SelectedIndexChanged
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(CboDistritoEst, Obj.TraerDataTable("sp_ver_distrito", Me.CboProvinciaEst.SelectedValue), "codigo_dis", "nombre_dis", "--Seleccione Distrito--")
    End Sub

    Protected Sub TxtAcademia_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtAcademia.TextChanged
        If TxtAcademia.Text.Length > 0 Then
            If Me.TxtAcademia.Text <> "" Then
                Me.LblAcademia.ForeColor = Drawing.Color.Black
            End If
        End If
    End Sub

    Protected Sub TxtCentroPre_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCentroPre.TextChanged
        If TxtCentroPre.Text.Length > 0 Then
            If Me.TxtCentroPre.Text <> "" Then
                Me.LblCentroPre.ForeColor = Drawing.Color.Black
            End If
        End If
    End Sub

End Class

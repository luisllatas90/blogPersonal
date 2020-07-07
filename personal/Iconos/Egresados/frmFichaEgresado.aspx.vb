
Partial Class Egresado_frmFichaEgresado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            If (Request.QueryString("pso") <> Nothing) Then
                Dim Enc As New EncriptaCodigos.clsEncripta
                BuscarPersona(Enc.Decodifica(Request.QueryString("pso")).Substring(3), True)
            Else
                Response.Redirect("frmListaEgresados.aspx")
            End If
        End If
    End Sub

    Private Sub BuscarPersona(ByVal valor As String, Optional ByVal mostrardni As Boolean = False)
        Me.lblmensaje.Text = ""
        Dim obj As New ClsConectarDatos
        Dim ExistePersona As Boolean = False

        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim tbl As Data.DataTable
            '==================================
            'Buscar a la Persona
            '==================================
            obj.AbrirConexion()
            tbl = obj.TraerDataTable("ALUMNI_RetornaPersona", valor)
            obj.CerrarConexion()
            If tbl.Rows.Count > 0 Then
                ExistePersona = True
            End If

            If ExistePersona = True Then
                Me.hdcodigo_pso.Value = tbl.Rows(0).Item("codigo_Pso")
                If mostrardni = True Then
                    If tbl.Rows(0).Item("numeroDocIdent_Pso").ToString <> "" Then
                        Me.lblNumDocumento.Text = tbl.Rows(0).Item("numeroDocIdent_Pso").ToString
                    End If
                End If
                Me.lblAPaterno.Text = tbl.Rows(0).Item("apellidoPaterno_Pso")
                Me.lblAMaterno.Text = tbl.Rows(0).Item("apellidoMaterno_Pso").ToString
                Me.lblNombres.Text = tbl.Rows(0).Item("nombres_Pso")
                If tbl.Rows(0).Item("fechanacimiento_pso").ToString <> "" Then
                    Me.lblFechaNac.Text = CDate(tbl.Rows(0).Item("fechaNacimiento_Pso")).ToShortDateString
                End If

                If (tbl.Rows(0).Item("sexo_Pso") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("sexo_Pso").ToString.Trim <> "") Then
                    If (tbl.Rows(0).Item("sexo_Pso").ToString.ToUpper = "M") Then
                        Me.lblSexo.Text = "MASCULINO"
                    Else
                        Me.lblSexo.Text = "FEMENINO"
                    End If

                End If

                Me.lblConyugue.Text = tbl.Rows(0).Item("conyugue_pso").ToString
                Me.lblFecha.Text = tbl.Rows(0).Item("fechaMat_pso").ToString
                Me.lblDocumento.Text = tbl.Rows(0).Item("tipoDocIdent_Pso").ToString
                Me.lblemail1.Text = tbl.Rows(0).Item("emailPrincipal_Pso").ToString
                Me.lblemail2.Text = tbl.Rows(0).Item("emailAlternativo_Pso").ToString
                Me.lblDireccion.Text = tbl.Rows(0).Item("direccion_Pso").ToString
                Me.lblTelefono.Text = tbl.Rows(0).Item("telefonoFijo_Pso").ToString
                Me.lblCelular.Text = tbl.Rows(0).Item("telefonoCelular_Pso").ToString
                Me.lblModalidad.Text = tbl.Rows(0).Item("nombre_Min")

                If (tbl.Rows(0).Item("estadoCivil_Pso") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("estadoCivil_Pso").ToString.Trim <> "") Then
                    Me.lblEstadoCivil.Text = tbl.Rows(0).Item("estadoCivil_Pso").ToString.ToUpper
                End If
                Me.lblruc.Text = tbl.Rows(0).Item("nroRuc_Pso").ToString

                Me.lblDepartamento.Text = tbl.Rows(0).Item("nombre_Dep").ToString
                Me.lblProvincia.Text = tbl.Rows(0).Item("nombre_Pro").ToString
                Me.lblDistrito.Text = tbl.Rows(0).Item("nombre_Dis").ToString

                'Buscamos si existe el egresado
                Dim dt_Egresado As New Data.DataTable
                obj.AbrirConexion()
                dt_Egresado = obj.TraerDataTable("ALUMNI_BuscaEgresado", Me.hdcodigo_pso.Value)
                obj.CerrarConexion()

                If (dt_Egresado.Rows.Count > 0) Then
                    Me.lblNivel.Text = dt_Egresado.Rows(0).Item("nivel_Ega")
                    Me.txtFormacion.Text = dt_Egresado.Rows(0).Item("formacionacad_ega")
                    Me.txtExperiencia.Text = dt_Egresado.Rows(0).Item("experiencialab_ega")
                    Me.txtOtrosEstudios.Text = dt_Egresado.Rows(0).Item("otrosestudios_ega")
                    Me.HdFileCV.Value = dt_Egresado.Rows(0).Item("cv_Ega")
                    Me.HdFileFoto.Value = dt_Egresado.Rows(0).Item("foto_Ega")
                    Me.lblActualizacion.Text = dt_Egresado.Rows(0).Item("fechaActualizacion_Ega")
                    If (dt_Egresado.Rows(0).Item("visibilidad_Ega") = 0) Then
                        Me.chkMostrar.Checked = False
                    Else
                        Me.chkMostrar.Checked = True
                    End If

                    If (dt_Egresado.Rows(0).Item("actualmenteLabora_Ega") = "S") Then
                        Me.lblSituacion.Text = "Sí"
                    Else
                        Me.lblSituacion.Text = "No"
                    End If

                    If (dt_Egresado.Rows(0).Item("actualmenteLabora_Ega").ToString = "S") Then
                        Me.lblEmpresaLabora.Text = dt_Egresado.Rows(0).Item("EmpresaLabora_Ega")
                        Me.lblSector.Text = dt_Egresado.Rows(0).Item("nombre_sec")
                        If (dt_Egresado.Rows(0).Item("tipoEmpresa_Ega") = "P") Then
                            Me.lblTipoEmpresa.Text = "PRIVADA"
                        Else
                            Me.lblTipoEmpresa.Text = "PÚBLICA"
                        End If

                        Me.lblCargoActual.Text = dt_Egresado.Rows(0).Item("cargoActual_Ega")
                        Me.lblDireccionEmpresa.Text = dt_Egresado.Rows(0).Item("direccionEmpresa_Ega")
                        Me.lblTelefonoProfesional.Text = dt_Egresado.Rows(0).Item("telefono_Ega")
                        Me.lblCorreoProfesional.Text = dt_Egresado.Rows(0).Item("correoProfesional_Ega")
                    End If

                    If (dt_Egresado.Rows(0).Item("cv_Ega").ToString.Trim <> "") Then
                        Me.lnkDescarga.Enabled = True                        
                    Else
                        Me.lnkDescarga.Enabled = False
                    End If

                    If (dt_Egresado.Rows(0).Item("foto_Ega").ToString.Trim <> "") Then
                        ImgEgresado.ImageUrl = "../../librerianet/Egresado/fotos/" & dt_Egresado.Rows(0).Item("foto_Ega")
                    Else
                        ImgEgresado.ImageUrl = "../../librerianet/Egresado/archivos/no_disponible.jpg"
                    End If
                    '

                    If (dt_Egresado.Rows(0).Item("promedioTrabajo") = "3") Then
                        Me.chkTresMeses.Checked = True
                    Else
                        Me.chkTresMeses.Checked = False
                        Me.txtNumMeses.Text = dt_Egresado.Rows(0).Item("promedioTrabajo")
                    End If

                    Me.lblmensaje.Text = ""
                Else
                    Me.lblmensaje.Text = "No esta registrado como Egresado"
                End If
            End If
            tbl.Dispose()
            obj = Nothing
            tbl = Nothing
        Catch ex As Exception
            Me.lblmensaje.Text = "Error " & ex.Message
            Me.lblmensaje.Visible = True
            'obj.CerrarConexion()
            'obj = Nothing
        End Try
    End Sub

    Protected Sub lnkDescarga_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDescarga.Click
        Response.Redirect("curriculum/" & Me.HdFileCV.Value)
    End Sub

    Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Me.btnImprimir.Visible = False        
        Response.Write("<script>javascript:print();</script>")
        Me.btnImprimir.Visible = True        
    End Sub
End Class

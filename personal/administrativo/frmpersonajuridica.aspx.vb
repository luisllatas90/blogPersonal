
Partial Class administrativo_frmpersonajuridica
    Inherits System.Web.UI.Page

    Protected Sub lnkComprobarDNI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkComprobarDNI.Click

        LimpiarCajas()
        trConcidencias.Visible = False
        If ValidarNroRUC() = True Then
            lnkComprobarNombres.Text = "Clic aquí para buscar coincidencias"
            BuscarPersonaJuridica("RUC", Me.txtruc.Text.Trim)
        End If

    End Sub

    Private Sub BuscarPersonaJuridica(ByVal tipo As String, ByVal valor As String)

        Dim ObjPersona As New ClsConectarDatos
        Dim DatosDevueltos As New Data.DataTable
        Try
            ObjPersona.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            If tipo = "RUC" Then
                ObjPersona.AbrirConexion()
                DatosDevueltos = ObjPersona.TraerDataTable("ConsultarPostulanteOtros_Persona", "JU", valor.Trim)
                ObjPersona.CerrarConexion()

                If DatosDevueltos.Rows.Count > 0 Then
                    Me.lnkComprobarNombres.Visible = False
                    Me.hdcodigo_pso.Value = DatosDevueltos.Rows(0).Item("codigo_Pso").ToString
                    Me.hdcodigo_pot.Value = DatosDevueltos.Rows(0).Item("codigo_Pot").ToString
                    Me.txtNombres.Text = DatosDevueltos.Rows(0).Item("apellidoPatRazSoc_Pot").ToString
                    Me.txtdireccion.Text = DatosDevueltos.Rows(0).Item("direccion_Pot").ToString
                    Me.txttelefono.Text = DatosDevueltos.Rows(0).Item("telefono_Pot").ToString
                    Me.txtruc.Enabled = False
                    Me.txtNombres.Enabled = False
                    Me.cmdGuardar.Enabled = True
                Else
                    LimpiarCajas()
                    Me.txtruc.Enabled = True
                    Me.txtNombres.Enabled = True
                    Me.txtNombres.Focus()
                    Me.lnkComprobarNombres.Visible = True
                    Me.hdcodigo_pot.Value = ""
                End If
            End If

            If tipo = "RAZ" Then

                ObjPersona.AbrirConexion()
                DatosDevueltos = ObjPersona.TraerDataTable("ConsultarPostulanteOtros_Persona", "JU", valor.Trim)
                ObjPersona.CerrarConexion()

                If DatosDevueltos.Rows.Count > 0 Then
                    Me.grwCoincidencias.DataSource = DatosDevueltos
                    Me.grwCoincidencias.DataBind()
                    trConcidencias.Visible = True
                Else
                    Me.cmdGuardar.Enabled = True
                    trConcidencias.Visible = False
                End If

                
            End If

            If tipo = "COD" Then
                ObjPersona.AbrirConexion()
                DatosDevueltos = ObjPersona.TraerDataTable("ConsultarPostulanteOtros_Persona", "CO", valor)
                ObjPersona.CerrarConexion()
                If DatosDevueltos.Rows.Count > 0 Then
                    Me.cmdGuardar.Enabled = True
                    Me.lnkComprobarNombres.Visible = False
                    Me.txtruc.Text = DatosDevueltos.Rows(0).Item("nroDocIde_Pot").ToString.Trim
                    Me.txtNombres.Text = DatosDevueltos.Rows(0).Item("apellidoPatRazSoc_Pot").ToString
                    Me.txtdireccion.Text = DatosDevueltos.Rows(0).Item("direccion_Pot").ToString
                    Me.txttelefono.Text = DatosDevueltos.Rows(0).Item("telefono_Pot").ToString
                    If ValidarNroRUC(True) = False Then
                        Me.txtruc.Enabled = True
                    Else
                        Me.txtruc.Enabled = False
                    End If
                    Me.txtNombres.Enabled = False
                End If
            End If

        Catch ex As Exception
            ObjPersona.CerrarConexion()
            Me.lblmensaje.Text = ex.Message
        End Try


    End Sub

    Private Sub LimpiarCajas()
        Me.txtNombres.Text = ""
        Me.txtdireccion.Text = ""
        Me.txttelefono.Text = ""
    End Sub

    Private Function ValidarNroRUC(Optional ByVal IrAlFoco As Boolean = True) As Boolean
        'Limpiar txt
        Me.lblmensaje.Text = ""
        Me.lblmensaje.Visible = False
        'Validar DNI

        If Me.txtruc.Text.Length <> 11 OrElse IsNumeric(Me.txtruc.Text.Trim) = False OrElse Me.txtruc.Text = "00000000000" Then
            Me.lblmensaje.Text = "El número de RUC es incorrecto. Mínimo 11 caracteres"
            Me.lblmensaje.Visible = True
            If IrAlFoco = True Then Me.txtruc.Focus()
            Return False
        Else
            Return True
        End If
    End Function

    Protected Sub lnkComprobarNombres_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkComprobarNombres.Click
        'If ValidarNroRUC() = True Then
        lnkComprobarNombres.Text = "Clic aquí para buscar coincidencias"
        BuscarPersonaJuridica("RAZ", Me.txtNombres.Text.Trim)
        'End If
    End Sub

    Protected Sub grwCoincidencias_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwCoincidencias.SelectedIndexChanged
        Me.hdcodigo_pot.Value = Me.grwCoincidencias.DataKeys.Item(Me.grwCoincidencias.SelectedIndex).Values(0)
        Me.hdcodigo_pso.Value = Me.grwCoincidencias.DataKeys.Item(Me.grwCoincidencias.SelectedIndex).Values(1)
        'Me.DesbloquearOtrosDatos(True)
        BuscarPersonaJuridica("COD", Me.hdcodigo_pot.Value)
        trConcidencias.Visible = False
        lnkComprobarNombres.Text = "Clic aquí para buscar coincidencias"
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click

        Me.lblmensaje.Visible = False
        Dim ObjJuridico As New ClsConectarDatos
        Dim Codigo_per As Integer
        Codigo_per = Request.QueryString("id")
        ObjJuridico.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        If ValidarNroRUC(True) = True Then
            If Me.hdcodigo_pot.Value.ToString = "" Then
                Dim ValorDevueltos(2) As Integer
                ' Se modifican los datos del Cliente.
                Try
                    ObjJuridico.AbrirConexion()

                    ObjJuridico.Ejecutar("AgregarPostulanteOtros_Persona", "J", "", Me.txtNombres.Text.Trim, "", "", "RUC", Me.txtruc.Text.Trim, _
                                        Me.txtdireccion.Text.Trim, Me.txttelefono.Text.Trim, 0, 0, Me.txtruc.Text.Trim, Codigo_per, 0, 0).CopyTo(ValorDevueltos, 0)

                    ObjJuridico.CerrarConexion()
                    Me.hdcodigo_pso.Value = ValorDevueltos(0)
                    Me.hdcodigo_pot.Value = ValorDevueltos(1)
                    Response.Redirect("pec/frmgeneracioncargos.aspx?tcl=O&pso=" & Me.hdcodigo_pso.Value & "&cli=" & Me.hdcodigo_pot.Value & "&" & Request.QueryString.ToString)

                Catch ex As Exception
                    ObjJuridico.CerrarConexion()
                    Me.lblmensaje.Text = "Ocurrió un error al guardar los datos." & ex.Message
                    Me.lblmensaje.Visible = True
                End Try
            Else
                ' Se agregan los datos del cliente.
                Try
                    ObjJuridico.AbrirConexion()
                    ObjJuridico.Ejecutar("ModificarPostulanteOtros_Persona", Me.hdcodigo_pot.Value, "J", Me.txtNombres.Text.Trim, "", "", _
                                        "RUC", Me.txtruc.Text.Trim, Me.txtdireccion.Text.Trim, Me.txttelefono.Text.Trim, 0, 0, Me.txtruc.Text.Trim)
                    ObjJuridico.CerrarConexion()
                    Response.Redirect("pec/frmgeneracioncargos.aspx?tcl=O&pso=" & Me.hdcodigo_pso.Value & "&cli=" & Me.hdcodigo_pot.Value & "&" & Request.QueryString.ToString)
                Catch ex As Exception
                    ObjJuridico.CerrarConexion()
                    Me.lblmensaje.Text = "Ocurrió un error al guardar los datos." & ex.Message
                    Me.lblmensaje.Visible = True
                End Try

            End If
        End If

    End Sub

    Protected Sub cmdLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLimpiar.Click
        Me.hdcodigo_pot.Value = ""
        Me.trConcidencias.Visible = False
        Me.txtNombres.Enabled = False
        Me.txtruc.Text = ""
        LimpiarCajas()
        Me.txtruc.Enabled = True
        Me.txtNombres.Enabled = True
        Me.cmdGuardar.Enabled = False
        Me.txtruc.Focus()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.txtNombres.Enabled = False
        End If


    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Response.Redirect("pec/lstinscritoseventocargo.aspx?" & Page.Request.QueryString.ToString)
    End Sub
End Class

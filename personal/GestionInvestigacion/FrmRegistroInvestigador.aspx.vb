
Partial Class GestionInvestigacion_FrmRegistroInvestigador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            If IsPostBack = False Then

                CargarLineas()
                CargarAreasOCDE(0, "AR")
                Me.DivDina.Visible = False
                Me.DivOrcid.Visible = False
                Me.DivScopus.Visible = False
                Me.DivResearcherID.Visible = False
                Me.DivMensaje.Visible = False
                CargarDatos()
            End If
        Catch ex As Exception

            mensaje("alert alert-danger", "No se pudo cargar la página")

        End Try
    End Sub

    Private Sub CargarDatos()
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ConsultarPersonalInvestigador(Session("id_per"))

        Me.lblNombre.Text = dt.Rows(0).Item("apellidoPat_Per").ToString + " " + dt.Rows(0).Item("apellidoMat_Per").ToString + " " + dt.Rows(0).Item("Nombres_Per").ToString
        Me.lblNroDocumento.Text = dt.Rows(0).Item("nroDocIdentidad_Per")
        Me.lblTipo.Text = dt.Rows(0).Item("descripcion_Tpe")
        Me.lblArea.Text = dt.Rows(0).Item("Area")
        Me.lblDepartamento.Text = dt.Rows(0).Item("Departamento")
        Me.hdInv.Value = obj.EncrytedString64(dt.Rows(0).Item("codigo_inv"))
        Me.hdInv.Value = dt.Rows(0).Item("codigo_inv")


        Dim URL As String = "https://orcid.org/oauth/authorize?client_id=APP-ID59GM8T9DT29K07&response_type=code&scope=/authenticate&" _
                    + "family_names=" + StrConv(dt.Rows(0).Item("apellidoPat_Per").ToString + " " _
                    + dt.Rows(0).Item("apellidoMat_Per").ToString, VbStrConv.ProperCase) _
                    + "&given_names=" + StrConv(dt.Rows(0).Item("nombres_Per").ToString, VbStrConv.ProperCase) _
                    + "&email=" + dt.Rows(0).Item("correo").ToString _
                    + "&lang=es" _
                    + "&show_login=true" _
                    + "&redirect_uri=https://orcid.org/my-orcid"
        '' Para Pruebas
        '+ "&redirect_uri=http://serverdev/campusvirtual/personal/GestionInvestigacion/frmRegistroInvestigadores.aspx"

        Me.btnORCID.Attributes.Add("onclick", "CerrarSesionORCID();window.open('" + URL + "', '_blank', 'toolbar=no, scrollbars=yes, width=500, height=700, top=100, left=500'); return false;")

        If dt.Rows(0).Item("codigo_dis") <> "0" Then
            Me.cboArea.SelectedValue = dt.Rows(0).Item("codigo_ocde")
            CargarAreasOCDE(dt.Rows(0).Item("codigo_ocde"), "SA")
            Me.cboSubArea.SelectedValue = dt.Rows(0).Item("codigo_sa_ocde")
            CargarAreasOCDE(dt.Rows(0).Item("codigo_sa_ocde"), "DI")
            Me.cboDisciplina.SelectedValue = dt.Rows(0).Item("codigo_dis")
        End If

        If dt.Rows(0).Item("codigo_lin") <> "0" Then
            Me.cboLinea.SelectedValue = dt.Rows(0).Item("codigo_lin")
        End If

        If dt.Rows(0).Item("regina") <> "" Then
            Me.chkRegina.Checked = True
            Me.DivRenacyt.Visible = True
            Me.txtRenacyt.Text = dt.Rows(0).Item("regina")
        Else
            Me.chkRegina.Checked = False
            Me.DivRenacyt.Visible = False
            Me.txtRenacyt.Text = ""
        End If
        If dt.Rows(0).Item("dina") <> "" Then
            Me.chkDina.Checked = True
            Me.DivDina.Visible = True
            Me.txtUrlDina.Text = dt.Rows(0).Item("dina")
        Else
            Me.chkDina.Checked = False
            Me.DivDina.Visible = False
            Me.txtUrlDina.Text = ""
        End If
        If dt.Rows(0).Item("orcid") <> "" Then
            Me.chkOrcid.Checked = True
            Me.DivOrcid.Visible = True
            Me.txtOrcid.Text = dt.Rows(0).Item("orcid")
        Else
            Me.chkOrcid.Checked = False
            Me.DivOrcid.Visible = False
            Me.txtOrcid.Text = ""
        End If
        If dt.Rows(0).Item("scopus") <> "" Then
            Me.chkScopus.Checked = True
            Me.DivScopus.Visible = True
            Me.txtScopusID.Text = dt.Rows(0).Item("scopus")
        Else
            Me.chkScopus.Checked = False
            Me.DivScopus.Visible = False
            Me.txtScopusID.Text = ""
        End If
        If dt.Rows(0).Item("researcher") <> "" Then
            Me.chkResearcherID.Checked = True
            Me.DivResearcherID.Visible = True
            Me.txtResearcherID.Text = dt.Rows(0).Item("researcher")
        Else
            Me.chkResearcherID.Checked = False
            Me.DivResearcherID.Visible = False
            Me.txtResearcherID.Text = ""
        End If

        If dt.Rows(0).Item("codigo_inv") <> 0 Then
            Me.hdInv.Value = dt.Rows(0).Item("codigo_inv")
            If dt.Rows(0).Item("estado") = "EN EVALUACIÓN" Or dt.Rows(0).Item("estado") = "APROBADO" Or dt.Rows(0).Item("estado") = "RECHAZADO" Then
                Me.btnRegistrarInvestigador.Text = dt.Rows(0).Item("estado")
                Me.btnRegistrarInvestigador.Enabled = False
                deshabilitarControles()
            Else
                Me.btnRegistrarInvestigador.Text = "REENVIAR"
                Me.btnRegistrarInvestigador.Enabled = True
                HabilitarControles()
            End If
            ListarObservaciones(dt.Rows(0).Item("codigo_inv"))
        Else
            Me.hdInv.Value = 0
            Me.btnRegistrarInvestigador.Text = "Guardar"
            Me.btnRegistrarInvestigador.Enabled = True
            HabilitarControles()
        End If

    End Sub

    Private Sub ListarObservaciones(ByVal codigo As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListarObservacionesInvestigador(codigo, "INV")

        Dim obs As String = "<label style='font-size:12px;text-align:center;margin-botton:2px;color:black'>OBSERVACIONES</label></br>"
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("c_obs") = "0" Then
                    obs += "<label style='font-size:12px;line-height:10px;margin-botton:2px;color:red'>" + (i + 1).ToString + ".- " + dt.Rows(i).Item("d_fech") + " - OBSERVADO - " + dt.Rows(i).Item("d_obs") + "</label></br>"
                Else
                    obs += "<label style='font-size:12px;line-height:10px;margin-botton:2px;color:green'>" + (i + 1).ToString + ".- " + dt.Rows(i).Item("d_fech") + " - SOLUCIONADO - " + dt.Rows(i).Item("d_obs") + "</label></br>"
                End If
            Next
            Me.DivObservacionesInv.InnerHtml = obs
            Me.DivObservacionesInv.Attributes.Add("class", "alert alert-danger")
        Else
            Me.DivObservacionesInv.InnerHtml = ""
            Me.DivObservacionesInv.Attributes.Remove("class")
        End If
        
    End Sub

    Private Sub CargarLineas()
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListaLineasInvestigacion(0)
        Me.cboLinea.Items.Add(New ListItem("-- Seleccione --", ""))
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.cboLinea.Items.Add(New ListItem(dt.Rows(i).Item("nombre"), dt.Rows(i).Item("codigo")))
        Next
    End Sub

    Private Sub CargarAreasOCDE(ByVal codigo As Integer, ByVal tipo As String)
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListarAreasConocimientoOCDE(codigo, tipo)
        If tipo = "AR" Then 'AREA
            Me.cboArea.Items.Add(New ListItem("-- Seleccione --", ""))
            For i As Integer = 0 To dt.Rows.Count - 1
                Me.cboArea.Items.Add(New ListItem(dt.Rows(i).Item("descripcion"), dt.Rows(i).Item("codigo")))
            Next
        ElseIf tipo = "SA" Then ' SUBAREA
            Me.cboSubArea.Items.Clear()
            Me.cboSubArea.Items.Add(New ListItem("-- Seleccione --", ""))
            For i As Integer = 0 To dt.Rows.Count - 1
                Me.cboSubArea.Items.Add(New ListItem(dt.Rows(i).Item("descripcion"), dt.Rows(i).Item("codigo")))
            Next
        Else ' DISCIPLINA
            Me.cboDisciplina.Items.Clear()
            Me.cboDisciplina.Items.Add(New ListItem("-- Seleccione --", ""))
            For i As Integer = 0 To dt.Rows.Count - 1
                Me.cboDisciplina.Items.Add(New ListItem(dt.Rows(i).Item("descripcion"), dt.Rows(i).Item("codigo")))
            Next
        End If
    End Sub



    Protected Sub chkDina_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDina.CheckedChanged
        If chkDina.Checked = True Then
            Me.DivDina.Visible = True
        Else
            Me.DivDina.Visible = False
        End If
        Me.txtUrlDina.Text = ""
    End Sub

    Protected Sub chkOrcid_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOrcid.CheckedChanged
        If chkOrcid.Checked = True Then
            Me.DivOrcid.Visible = True
        Else
            Me.DivOrcid.Visible = False
        End If
        Me.txtOrcid.Text = ""
    End Sub

    Protected Sub chkScopus_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkScopus.CheckedChanged
        If chkScopus.Checked = True Then
            Me.DivScopus.Visible = True
        Else
            Me.DivScopus.Visible = False
        End If
        Me.txtScopusID.Text = ""
    End Sub

    Protected Sub chkResearcherID_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkResearcherID.CheckedChanged
        If chkResearcherID.Checked = True Then
            Me.DivResearcherID.Visible = True
        Else
            Me.DivResearcherID.Visible = False
        End If
        Me.txtResearcherID.Text = ""
    End Sub

    Function validar() As Boolean
        Me.DivMensaje.Attributes.Remove("class")
        Me.DivMensaje.Attributes.Add("class", "alert alert-danger")
        If Me.cboLinea.SelectedValue = "" Then
            Me.DivMensaje.InnerHtml = "Debe Seleccionar una Linea de Investigación"
            Return False
        End If
        If Me.cboArea.SelectedValue = "" Then
            Me.DivMensaje.InnerHtml = "Debe Seleccionar una área OCDE"
            Return False
        End If
        If Me.cboSubArea.SelectedValue = "" Then
            Me.DivMensaje.InnerHtml = "Debe Seleccionar una subarea OCDE"
            Return False
        End If
        If Me.cboDisciplina.SelectedValue = "" Then
            Me.DivMensaje.InnerHtml = "Debe Seleccionar una área Disciplina"
            Return False
        End If
        If Me.chkRegina.Checked = False Then
            Me.DivMensaje.InnerHtml = "Debe Seleccionar e Ingresar Código RENACYT(REGINA)"
            Return False
        End If
        If Me.chkRegina.Checked = True Then
            If Me.txtRenacyt.Text = "" Then
                Me.DivMensaje.InnerHtml = "Debe Ingresar código RENACYT(REGINA)"
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtRenacyt)
                Return False
            End If
        End If

        If Me.chkDina.Checked = False Then
            Me.DivMensaje.InnerHtml = "Debe Seleccionar e Ingresar url CTI VITAE(DINA)"
            Return False
        End If
        If Me.chkDina.Checked = True Then
            If Me.txtUrlDina.Text = "" Then
                Me.DivMensaje.InnerHtml = "Debe Ingresar url CTI VITAE(DINA)"
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtUrlDina)
                Return False
            End If
        End If
        If Me.chkOrcid.Checked = False Then
            Me.DivMensaje.InnerHtml = "Debe Seleccionar e Ingresar ORCID"
            Return False
        End If
        If Me.chkOrcid.Checked = True Then
            If Me.txtOrcid.Text = "" Then
                Me.DivMensaje.InnerHtml = "Debe Ingresar URL ORCID"
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtOrcid)
                Return False
            End If
            If Me.txtOrcid.Text <> "" Then
                Dim orcid As String
                Dim arreglo As String()
                orcid = Replace(Me.txtOrcid.Text, "https://orcid.org/", "")
                arreglo = orcid.Split("-")
                If arreglo.Length <> 4 Or orcid.Length <> 19 Then
                    Me.DivMensaje.InnerHtml = "Ingrese Correctamente Código ORCID debe tener la siguiente estructura: https://orcid.org/XXXX-XXXX-XXXX-XXXX"
                    ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtOrcid)
                    Return False
                End If
            End If
        End If
        If Me.chkScopus.Checked = True Then
            If Me.txtScopusID.Text = "" Then
                Me.DivMensaje.InnerHtml = "Debe Ingresar Scopus ID"
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtScopusID)
                Return False
            End If
        End If
        If Me.chkResearcherID.Checked = True Then
            If Me.txtResearcherID.Text = "" Then
                Me.DivMensaje.InnerHtml = "Debe Ingresar código de ResearcherID"
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtResearcherID)
                Return False
            End If
        End If
        Return True
    End Function


    Protected Sub btnRegistrarInvestigador_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrarInvestigador.Click
        Me.DivMensaje.Visible = True
        If validar() = True Then
            'Quitar el Mensaje de Validación
            mensaje("", "")

            'Mostrar Modal de Confirmación
            Me.mdRegistro.Attributes.Remove("class")
            Me.mdRegistro.Attributes.Add("class", "modal fade in")
            Me.mdRegistro.Attributes.CssStyle.Remove("display")
            Me.mdRegistro.Attributes.CssStyle.Add("display", "block")
        End If
    End Sub

    Private Sub deshabilitarControles()
        Me.cboLinea.Enabled = False
        Me.cboArea.Enabled = False
        Me.cboSubArea.Enabled = False
        Me.cboDisciplina.Enabled = False
        Me.chkRegina.Enabled = False
        Me.txtRenacyt.Enabled = False
        Me.chkDina.Enabled = False
        Me.txtUrlDina.Enabled = False
        Me.chkOrcid.Enabled = False
        Me.txtOrcid.Enabled = False
        Me.chkScopus.Enabled = False
        Me.txtScopusID.Enabled = False
        Me.chkResearcherID.Enabled = False
        Me.txtResearcherID.Enabled = False
    End Sub

    Private Sub HabilitarControles()
        Me.cboLinea.Enabled = True
        Me.cboArea.Enabled = True
        Me.cboSubArea.Enabled = True
        Me.cboDisciplina.Enabled = True
        Me.chkRegina.Enabled = True
        Me.txtRenacyt.Enabled = True
        Me.chkDina.Enabled = True
        Me.txtUrlDina.Enabled = True
        Me.chkOrcid.Enabled = True
        Me.txtOrcid.Enabled = True
        Me.chkScopus.Enabled = True
        Me.txtScopusID.Enabled = True
        Me.chkResearcherID.Enabled = True
        Me.txtResearcherID.Enabled = True
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        'Ocultar Modal de Confirmación
        Me.mdRegistro.Attributes.Remove("class")
        Me.mdRegistro.Attributes.Add("class", "modal fade")
        Me.mdRegistro.Attributes.CssStyle.Remove("display")
        Me.mdRegistro.Attributes.CssStyle.Add("display", "none")
    End Sub


    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If Session("id_per") = "" Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            Dim obj As New ClsGestionInvestigacion
            Dim dt As New Data.DataTable

            Dim regina As String = "0"
            Dim dina As String = ""
            Dim orcid As String = ""
            Dim scopusID As String = ""
            Dim ResearcherID As String = ""

            If chkRegina.Checked = True Then
                regina = Me.txtRenacyt.Text
            End If

            If chkDina.Checked = True Then
                dina = Me.txtUrlDina.Text
            End If

            If chkOrcid.Checked = True Then
                orcid = Me.txtOrcid.Text
            End If

            If chkScopus.Checked = True Then
                scopusID = Me.txtScopusID.Text
            End If

            If chkResearcherID.Checked = True Then
                ResearcherID = Me.txtResearcherID.Text
            End If

            dt = obj.ActualizarInvestigador(Me.hdInv.Value, Session("id_per"), Me.cboLinea.SelectedValue, Me.cboDisciplina.SelectedValue, regina, dina, orcid, scopusID, ResearcherID, Session("id_per"))

            If dt.Rows(0).Item("Respuesta") = 1 Then
                Me.hdInv.Value = dt.Rows(0).Item("cod")
                'Mensaje de Confirmación de Registro/Actualización
                mensaje(dt.Rows(0).Item("Mensaje"), "alert alert-success")
                deshabilitarControles()
                'Ocultar Modal de Confirmación
                Me.mdRegistro.Attributes.Remove("class")
                Me.mdRegistro.Attributes.Add("class", "modal fade")
                Me.mdRegistro.Attributes.CssStyle.Remove("display")
                Me.mdRegistro.Attributes.CssStyle.Add("display", "none")

                Me.btnRegistrarInvestigador.Text = "EN EVALUACIÓN"
                Me.btnRegistrarInvestigador.Enabled = False
                Response.Redirect("FrmRegistroInvestigador.aspx?id=" + Request.QueryString("id") + "&ctf=" + Request.QueryString("ctf"))
            Else
                mensaje("alert alert-danger", dt.Rows(0).Item("Mensaje"))
            End If

        Catch ex As Exception
            mensaje("alert alert-danger", "No se pudo guardar")
        End Try

    End Sub

    Private Sub mensaje(ByVal mensaje As String, ByVal tipo As String)
        Me.DivMensaje.InnerHtml = mensaje
        Me.DivMensaje.Attributes.Remove("class")
        Me.DivMensaje.Attributes.Add("class", tipo)
    End Sub

    Protected Sub cboArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboArea.SelectedIndexChanged
        If Me.cboArea.SelectedValue <> "" Then
            CargarAreasOCDE(Me.cboArea.SelectedValue, "SA")
        Else
            Me.cboSubArea.Items.Clear()
            Me.cboSubArea.Items.Add(New ListItem("-- Seleccione --", ""))
        End If
        Me.cboDisciplina.Items.Clear()
        Me.cboDisciplina.Items.Add(New ListItem("-- Seleccione --", ""))
    End Sub

    Protected Sub cboSubArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSubArea.SelectedIndexChanged
        If Me.cboSubArea.SelectedValue <> "" Then
            CargarAreasOCDE(Me.cboSubArea.SelectedValue, "DI")
        Else
            Me.cboDisciplina.Items.Clear()
            Me.cboDisciplina.Items.Add(New ListItem("-- Seleccione --", ""))
        End If
    End Sub

    Protected Sub chkRegina_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRegina.CheckedChanged
        If chkRegina.Checked = True Then
            Me.DivRenacyt.Visible = True
        Else
            Me.DivRenacyt.Visible = False
        End If
        Me.txtRenacyt.Text = ""
    End Sub
End Class

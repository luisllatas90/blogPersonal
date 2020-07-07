
Partial Class administrativo_propuestas2_Miembros_frmConsejoFacultad
    Inherits System.Web.UI.Page
    Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
    Dim tb As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            tb = ObjCnx.TraerDataTable("PRP_ListaConsejos_POA")
            Me.ddlFacultad.DataSource = tb
            Me.ddlFacultad.DataTextField = "nombre"
            Me.ddlFacultad.DataValueField = "codigo"

            Me.ddlFacultad.databind()
            Me.ddlFacultad.selectedvalue = 0

            If ddlFacultad.selectedvalue = 0 Then
                Me.Button3.enabled = False
            Else
                Me.Button3.enabled = True
            End If

            Me.hdPerActual.Value = 0
            Me.hdcodigo_Cjf.Value = 0
        End If
    End Sub

    Sub cargarPersonal()
        Dim tb As New Data.DataTable

        ' Response.Write("PRP_ListaPersonalConsejo_POA " & Me.ddlFacultad.SelectedValue & ",'T'")

        tb = ObjCnx.TraerDataTable("PRP_ListaPersonalConsejo_POA", Me.ddlFacultad.SelectedValue, "T")

        Me.ddlPersonal.DataSource = tb
        Me.ddlPersonal.DataTextField = "responsable_Cco"
        Me.ddlPersonal.DataValueField = "codigo_Pcc"
        Me.ddlPersonal.DataBind()

        Me.ddlPersonalNuevo.DataSource = tb
        Me.ddlPersonalNuevo.DataTextField = "responsable_Cco"
        Me.ddlPersonalNuevo.DataValueField = "codigo_Pcc"
        Me.ddlPersonalNuevo.DataBind()

        Call cargoPersonal(ddlPersonal.SelectedValue)
    End Sub

    Sub cargoPersonal(ByVal codigo_pcc As Integer)
        Dim tb As New Data.DataTable

        ' Response.Write("  PRP_ConsultarCargosPersonal_POA " & codigo_pcc)

        tb = ObjCnx.TraerDataTable("PRP_ConsultarCargosPersonal_POA", codigo_pcc)
        Me.ddlCargoPersonal.DataSource = tb
        Me.ddlCargoPersonal.DataTextField = "descripcion_Cgo"
        Me.ddlCargoPersonal.DataValueField = "codigo_Cgo"
        Me.ddlCargoPersonal.DataBind()
    End Sub

    Sub cargarPersonalAsignado()
        Dim tb As New Data.DataTable

        ' Response.Write("  PRP_ListaPersonalConsejo_POA '" & Me.ddlFacultad.SelectedValue & "', 'L'")

        tb = ObjCnx.TraerDataTable("PRP_ListaPersonalConsejo_POA", Me.ddlFacultad.SelectedValue, "L")
        Me.dgvLista.DataSource = tb
        Me.dgvLista.DataBind()
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call cargarPersonalAsignado()
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call cargarPersonal()
        Me.PanelConsulta.Visible = False
        Me.PanelRegistro.Visible = True
        Me.PanelLista.Visible = False
        Me.Button2.Enabled = False ' consultar
        Me.Button3.Enabled = False  'nuevo
        Me.Button4.Enabled = True  ' registrar
        Me.Button5.Enabled = True 'cancelar
    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.PanelConsulta.Visible = True
        Me.PanelRegistro.Visible = False
        Me.PanelActualizar.Visible = False
        Me.PanelLista.Visible = True
        Me.Button3.Enabled = True  'nuevo
        Me.Button4.Enabled = False  ' registrar
        Me.Button5.Enabled = False 'cancelar
        cargarPersonalAsignado()
        If Me.Button4.Text.Trim = "Actualizar" Then
            Me.Button4.Text = "Registrar"
        End If
    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            If Me.Button4.Text.Trim = "Actualizar" Then
                'Response.Write("PRP_RegistrarMiembroConsejo_v2 " & Me.ddlFacultad.SelectedValue & "," & Me.ddlPersonalNuevo.SelectedValue & "," & "C" & "," & ddlCargo.SelectedValue & "," & ddlCargoPersonal.SelectedValue & "," & Me.hdcodigo_Cjf.Value)
                ObjCnx.Ejecutar("PRP_RegistrarMiembroConsejo_v2", Me.ddlFacultad.SelectedValue, Me.ddlPersonalNuevo.SelectedValue, "C", ddlCargo.SelectedValue, ddlCargoPersonal.SelectedValue, Me.hdcodigo_Cjf.Value)

            Else
                Me.hdPerActual.Value = 0
                'Response.Write("PRP_RegistrarMiembroConsejo_v2 " & Me.ddlFacultad.SelectedValue & "," & Me.ddlPersonal.SelectedValue & ",'A'," & ddlCargo.SelectedValue & "," & ddlCargoPersonal.SelectedValue & "," & Me.hdcodigo_Cjf.Value)
                ObjCnx.Ejecutar("PRP_RegistrarMiembroConsejo_v2", Me.ddlFacultad.SelectedValue, Me.ddlPersonal.SelectedValue, "A", ddlCargo.SelectedValue, ddlCargoPersonal.SelectedValue, Me.hdcodigo_Cjf.Value)
            End If
            Call cargarPersonal()
            Button5_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgvLista_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgvLista.RowDeleting
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("PRP_RegistrarMiembroConsejo_v2", dgvLista.DataKeys(e.RowIndex).Values("codigo_Cjf"), 0, "I", 0)
        Call cargarPersonalAsignado()
        obj.CerrarConexion()
        obj = Nothing
        e.Cancel = True
    End Sub

    Protected Sub ddlFacultad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFacultad.SelectedIndexChanged
        Call cargarPersonalAsignado()
        If ddlFacultad.SelectedValue = 0 Then
            Me.Button3.Enabled = False
            Me.Label1.Text = ""
        Else
            Me.Button3.Enabled = True
            Me.Label1.Text = ": " & Me.ddlFacultad.SelectedItem.ToString
        End If
    End Sub

    Protected Sub dgvLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dgvLista.RowCommand
        If (e.CommandName = "Actualizar") Then
            Call cargarPersonal()
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Me.Button3.Enabled = False  'nuevo
            Me.Button4.Enabled = True  ' registrar
            Me.Button5.Enabled = True 'cancelar
            Me.Button4.Text = "          Actualizar"
            Me.PanelActualizar.Visible = True
            Me.PanelConsulta.Visible = False
            Me.PanelRegistro.Visible = False
            Me.PanelLista.Visible = False
            Me.lblPersonalActual.Text = dgvLista.DataKeys(index).Values("responsable_Cco")
            Me.lblCargoActual.Text = dgvLista.DataKeys(index).Values("Cargo_Cjf")
            Me.hdPerActual.Value = dgvLista.DataKeys(index).Values("codigo_pcc")
            Me.hdcodigo_Cjf.Value = dgvLista.DataKeys(index).Values("codigo_Cjf")
        End If
    End Sub

    Protected Sub ddlPersonal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPersonal.SelectedIndexChanged
        Call cargoPersonal(ddlPersonal.SelectedValue)
    End Sub
End Class


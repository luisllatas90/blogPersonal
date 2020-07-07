
Partial Class administrativo_propuestas2_Miembros_frmConsejoFacultad
    Inherits System.Web.UI.Page
    Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
    Dim tb As New data.datatable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            tb = ObjCnx.TraerDataTable("PRP_ListaFacultades")
            Me.ddlFacultad.datasource = tb
            Me.ddlFacultad.datatextfield = "nombre_fac"
            Me.ddlFacultad.datavaluefield = "codigo_fac"
            Me.ddlFacultad.databind()
            Me.ddlFacultad.selectedvalue = 0
            'cargarPersonal()
        End If
    End Sub
    Sub cargarPersonal()
        tb = New data.datatable
        tb = ObjCnx.TraerDataTable("PRP_ListaPersonal", Me.ddlFacultad.selectedvalue, "C")
        Me.ddlPersonal.datasource = tb
        Me.ddlPersonal.datatextfield = "responsable_Cco"
        Me.ddlPersonal.datavaluefield = "codigo_Pcc"
        Me.ddlPersonal.databind()
    End Sub
    Sub cargarPersonalAsignado()
        tb = New data.datatable
        tb = ObjCnx.TraerDataTable("PRP_ListaPersonal", Me.ddlFacultad.selectedvalue, "L")
        Me.dgvLista.datasource = tb        
        Me.dgvLista.databind()
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        cargarPersonalAsignado()
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click

        cargarPersonal()
        Me.PanelConsulta.visible = False
        Me.panelregistro.visible = True
        Me.panellista.visible = False
    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.PanelConsulta.visible = True
        Me.panelregistro.visible = False
        Me.panellista.visible = True
        Button2_Click(sender, e)
    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        ObjCnx.Ejecutar("PRP_RegistrarMiembroConsejo", Me.ddlFacultad.selectedvalue, Me.ddlPersonal.selectedvalue, "A", ddlCargo.selectedvalue)
        cargarPersonal()
        Button5_Click(sender, e)
    End Sub

    Protected Sub dgvLista_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgvLista.RowDeleting
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("PRP_RegistrarMiembroConsejo", dgvLista.DataKeys(e.RowIndex).Values("codigo_Cjf"), 0, "I", 0)
        Button5_Click(sender, e)
        obj.CerrarConexion()
        obj = Nothing
        e.Cancel = True
    End Sub

    Protected Sub dgvLista_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvLista.SelectedIndexChanged

    End Sub
End Class

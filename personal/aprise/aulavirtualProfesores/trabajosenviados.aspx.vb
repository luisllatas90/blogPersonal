
Partial Class trabajosenviados
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.lblTitulo.Text = Session("titulotarea")

        Dim Tabla As Data.DataTable
        Dim idestado As int16 = 0

        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
        Tabla = ObjDatos.TraerDataTable("DI_ConsultarAdminTareas", 3, Session("idusuario2"), Session("codigo_tfu2"), Session("idtarea2"), 0, Me.dtTipo.SelectedValue)

        Me.DataList1.DataSource = Tabla
        Me.DataList1.DataBind()
        If Tabla.Rows.Count > 0 Then
            Me.cmdEnviar.Text = "Enviar nuevo archivo"
        Else
            Me.lblMensaje.Visible = True
        End If

        Tabla = Nothing
        'Tabla.Dispose()

        ObjDatos = Nothing

        If Request.QueryString("idestadorecurso") = 1 Then
            Me.cmdEnviar.Visible = True
	    Me.cmdEnviar.OnClientClick = "AbrirPopUp('frmenviararchivo.aspx?accion=agregartareausuario&refidtareausuario=0','300','500');return(false)"	
        else
		Me.cmdEnviar.Visible = False
	End If
    End Sub

    Protected Sub cmdRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegresar.Click
        Session.RemoveAll()
        Response.Redirect("../../personal/aulavirtual/tareas/index.asp")
    End Sub
End Class

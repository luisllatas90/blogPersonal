
Partial Class LstUsuariosMVA
    Inherits System.Web.UI.Page


    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim ObjCnx As New ClsConectarDatos
        Dim Datos As Data.DataTable
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        ObjCnx.AbrirConexion()
        Datos = ObjCnx.TraerDataTable("dbo.EXT_ConsultarAlumnoExtranjero", Me.GridView1.DataKeys.Item(Me.GridView1.SelectedIndex).Values(0))
        With Datos.Rows(0)
            'Me.txtnombres.text = .item("Nombres")
            'Me.txtapellidos.text = .item("Apellidos")
            'Me.txtusuario.text = .item("Usuario")
            'Me.txtfuncion.text = .item("funcion")         
            'Me.txtestado.text = .item("Estado_Usu")
        End With
        ObjCnx.CerrarConexion()
    End Sub
End Class


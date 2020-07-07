
Partial Class academico_Identificacion
    Inherits System.Web.UI.Page

    Protected Sub cmdAcceder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAcceder.Click
        Dim ObjCnx As New ClsConectarDatos
        Dim Datos As New Data.DataTable
        Me.lblMensaje.Text = ""
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        ObjCnx.AbrirConexion()
        Datos = ObjCnx.TraerDataTable("consultaracceso", "A", Me.txtCodUniversitario.Text, Me.txtClave.Text)
        ObjCnx.CerrarConexion()
        If Datos.Rows.Count > 0 Then
            Session("ALU") = Datos.Rows(0).Item("codigo_Alu")
            Response.Redirect("lstdirectoriodni.aspx?Tipo=E&id=" & Datos.Rows(0).Item("codigo_Alu"))
        Else
            lblMensaje.Text = "Codigo o clave incorrecto, vuelva a intentarlo"
        End If
    End Sub
End Class

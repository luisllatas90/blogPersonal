
Partial Class Encuesta_PyD_EncuestaPyDMarketing
    Inherits System.Web.UI.Page

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim objCnx As New ClsConectarDatos
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objCnx.AbrirConexion()
        Dim internet, volantes, radio, periodicos, television, gigantografia, otros As Int16
        internet = IIf(cblMedio.Items(0).Selected = True, 1, 0)
        periodicos = IIf(cblMedio.Items(1).Selected = True, 1, 0)
        volantes = IIf(cblMedio.Items(2).Selected = True, 1, 0)
        television = IIf(cblMedio.Items(3).Selected = True, 1, 0)
        radio = IIf(cblMedio.Items(4).Selected = True, 1, 0)
        gigantografia = IIf(cblMedio.Items(5).Selected = True, 1, 0)
        otros = IIf(cblMedio.Items(6).Selected = True, 1, 0)
        objCnx.Ejecutar("ENC_PyDAgregarRespuestaPublicidad", Me.rblSexo.SelectedValue, Me.txtEdad.Text, Me.rblEscuela.SelectedItem.Text, _
                        Me.rblModalidad.SelectedItem.Text, internet, volantes, radio, periodicos, television, gigantografia, otros, Me.rblAvisos.SelectedItem.Text, _
                        Me.rblPublicidad.SelectedItem.Text, Me.txtLugares.Text, Me.rblVirtudes.SelectedItem.Text, Request.QueryString("codigo_alu"))
        ClientScript.RegisterStartupScript(Me.GetType, "Exito", "alert('Gracias por llenar la encuesta');", True)
        ClientScript.RegisterStartupScript(Me.GetType, "Cerrar", "window.close();", True)
        objCnx.CerrarConexion()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim datos As New Data.DataTable
            Dim objCnx As New ClsConectarDatos
            txtEdad.Attributes.Add("onKeyPress", "validarnumero()")
            objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objCnx.AbrirConexion()
            datos = objCnx.TraerDataTable("consultarAlumnoGeneral", "CO", Request.QueryString("codigo_alu"))
            If datos.Rows.Count > 0 Then
                Me.rblSexo.SelectedValue = datos.Rows(0).Item("sexo_alu")
                Me.rblEscuela.SelectedValue = datos.Rows(0).Item("codigo_cpf")
                Me.rblModalidad.SelectedValue = datos.Rows(0).Item("codigo_Min")
                Me.txtEdad.Text = Date.Now.Year - CDate(datos.Rows(0).Item("fechaNacimiento_Alu")).Year
            End If
            objCnx.CerrarConexion()
        End If
    End Sub
End Class

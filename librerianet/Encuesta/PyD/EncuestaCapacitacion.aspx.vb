
Partial Class librerianet_Encuesta_PyD_EncuestaCapacitacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim ObjCnx As New ClsConectarDatos
            Dim datosAlu As New Data.DataTable
            Dim ciclo() As Integer = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12}
            cboCiclo.DataSource = ciclo
            cboCiclo.DataBind()
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            datosAlu = ObjCnx.TraerDataTable("consultarAlumnoGeneral", "CO", Request.QueryString("cod"))
            ObjCnx.CerrarConexion()
            If datosAlu.Rows.Count > 0 Then
                Me.txtEscuela.Text = datosAlu.Rows(0).Item("nombre_cpf")
                Me.txtEdad.Text = Date.Now.Year - CDate(datosAlu.Rows(0).Item("fechaNacimiento_Alu")).Year
            End If
            txtTaller.Attributes.Add("onKeyPress", "validarnumero()")
            txtCharla.Attributes.Add("onKeyPress", "validarnumero()")
            txtSeminario.Attributes.Add("onKeyPress", "validarnumero()")
            txtCurso.Attributes.Add("onKeyPress", "validarnumero()")
        End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim ObjCnx As New ClsConectarDatos
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        ObjCnx.AbrirConexion()
        ObjCnx.Ejecutar("ENC_AgregarEncuestaCapacitacion", txtEscuela.Text, txtEdad.Text, cboCiclo.SelectedItem.Text, rblCompetencias.SelectedValue, _
                        IIf(cblTemas.Items(0).Selected, 1, 0), IIf(cblTemas.Items(1).Selected, 1, 0), IIf(cblTemas.Items(2).Selected, 1, 0), _
                        IIf(cblTemas.Items(3).Selected, 1, 0), IIf(cblTemas.Items(4).Selected, 1, 0), IIf(cblTemas.Items(5).Selected, 1, 0), _
                        IIf(cblTemas.Items(6).Selected, 1, 0), rblTiempo.SelectedItem.Text, txtExpectativas.Text, txtAtencion.Text, txtEpocaAnio.Text, _
                        txtCharla.Text, txtTaller.Text, txtSeminario.Text, txtCurso.Text, IIf(rblLugar.SelectedValue = 3, txtOtro.Text, rblLugar.SelectedItem.Text), Request.QueryString("cod"))
        ObjCnx.CerrarConexion()
        ClientScript.RegisterStartupScript(Me.GetType, "Exito", "alert('Se registraron correctamente los datos, Gracias por llenar la encuesta');", True)
        ClientScript.RegisterStartupScript(Me.GetType, "Cerrar", "window.close();", True)
    End Sub

    Protected Sub rblLugar_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblLugar.SelectedIndexChanged
        If rblLugar.SelectedItem.Value <> 3 Then
            Me.txtOtro.Text = ""
        End If
    End Sub
End Class


Partial Class administrativo_pec_frmRegistraCarreras
    Inherits System.Web.UI.Page

    Protected Sub CmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdCancelar.Click
        Response.Redirect("lstCarrerasProfesionales.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim DatTipo As New Data.DataTable
            Dim DatSubTipo As New Data.DataTable

            Dim ObjCarrea As New ClsConectarDatos
            ObjCarrea.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCarrea.AbrirConexion()
            DatTipo = ObjCarrea.TraerDataTable("EVE_ConsultarInformacionParaEvento", 0, "", "", "")
            ObjCarrea.CerrarConexion()
            ClsFunciones.LlenarListas(Me.DDLTipoEscuela, DatTipo, "codigo_test", "descripcion_test", "-- Seleccione Tipo --")
            If Request.QueryString("accion") = "M" Then
                Dim DatosEscuela As New Data.DataTable

                ObjCarrea.AbrirConexion()
                DatosEscuela = ObjCarrea.TraerDataTable("EVE_ConsultarInformacionParaEvento", 6, Request.QueryString("codcpf"), "", "")
                DatSubTipo = ObjCarrea.TraerDataTable("EVE_ConsultarInformacionParaEvento", 1, DatosEscuela.Rows(0).Item("codigo_test"), "", "")
                ObjCarrea.CerrarConexion()

                Me.TxtNombreEscuela.Text = DatosEscuela.Rows(0).Item("nombre_cpf").ToString.Trim
                Me.TxtAbreviatura.Text = DatosEscuela.Rows(0).Item("abreviatura_cpf").ToString.Trim
                If DatosEscuela.Rows(0).Item("vigencia_cpf") = 1 Then
                    Me.ChkVigencia.Checked = True
                Else
                    Me.ChkVigencia.Checked = False
                End If
                Me.DDLTipoEscuela.SelectedValue = DatosEscuela.Rows(0).Item("codigo_test")
                ClsFunciones.LlenarListas(Me.DDlSubTipo, DatSubTipo, "codigo_stest", "Descripcion_stest", "-- Selecciones Sub Tipo --")
                Me.DDlSubTipo.SelectedValue = DatosEscuela.Rows(0).Item("codigo_stest")

                Me.CmdCancelar.Attributes.Add("onclick", "self.parent.tb_remove();")
            Else
                Me.CmdCancelar.UseSubmitBehavior = True
            End If

        End If

    End Sub

    Protected Sub DDLTipoEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLTipoEscuela.SelectedIndexChanged

        Dim ObjSubTipo As New ClsConectarDatos
        Dim DatosSubTipo As New Data.DataTable
        ObjSubTipo.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        ObjSubTipo.AbrirConexion()
        DatosSubTipo = ObjSubTipo.TraerDataTable("EVE_ConsultarInformacionParaEvento", 1, Me.DDLTipoEscuela.SelectedValue, "", "")
        ObjSubTipo.CerrarConexion()

        ClsFunciones.LlenarListas(Me.DDlSubTipo, DatosSubTipo, "codigo_stest", "Descripcion_stest", "-- Selecciones Sub Tipo --")


    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click

        Dim ObjGuarda As New ClsConectarDatos
        ObjGuarda.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        If Request.QueryString("accion") = "A" Then
            Try
                ObjGuarda.AbrirConexion()
                ObjGuarda.Ejecutar("EVE_AgregarCarreraProfesional", Me.TxtNombreEscuela.Text.Trim, Me.TxtAbreviatura.Text.Trim, Me.ChkVigencia.Checked, 0, System.DBNull.Value, System.DBNull.Value, _
                                   0, 0, Me.DDLTipoEscuela.SelectedValue, Me.DDlSubTipo.SelectedValue)
                ObjGuarda.CerrarConexion()
                Page.RegisterStartupScript("ok", "<script>alert('Se han registrado los datos correctamente');location.href='lstCarrerasProfesionales.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "'</script>")
            Catch ex As Exception
                Me.LblMensaje.Text = "Ocurrió un error al regitrar los datos, inténtelo nuevamente"
                Me.LblMensaje.ForeColor = Drawing.Color.Red
            End Try
        Else
            Try
                ObjGuarda.AbrirConexion()
                ObjGuarda.Ejecutar("EVE_ModificarCarreraProfesional", Request.QueryString("codcpf"), Me.TxtNombreEscuela.Text.Trim, Me.TxtAbreviatura.Text.Trim, Me.ChkVigencia.Checked, 0, System.DBNull.Value, System.DBNull.Value, _
                                   0, 0, Me.DDLTipoEscuela.SelectedValue, Me.DDlSubTipo.SelectedValue)
                ObjGuarda.CerrarConexion()
                Page.RegisterStartupScript("ok", "<script>alert('Se han registrado los datos correctamente');window.parent.location.reload();self.parent.tb_remove()</script>")
            Catch ex As Exception
                Me.LblMensaje.Text = "Ocurrió un error al modificar los datos, inténtelo nuevamente"
                Me.LblMensaje.ForeColor = Drawing.Color.Red
            End Try

        End If

    End Sub
End Class


Partial Class academico_cargalectiva_frmRestriccionCursoAmbiente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.ddlCiclo, obj.TraerDataTable("ListaCicloAcademico"), "codigo_cac", "descripcion_cac", "<<Seleccione>>")
            obj.CerrarConexion()
            CargarCarrera()
        End If
    End Sub

    Sub CargarCarrera()
        Dim codigo_tfu As Int16 = Request.QueryString("ctf")
        Dim codigo_usu As Integer = Request.QueryString("id")
        Dim Modulo As Integer = Request.QueryString("mod")
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Dim objFun As New ClsFunciones
        objFun.CargarListas(Me.ddlCarreraProfesional, obj.TraerDataTable("EVE_ConsultarCarreraProfesional", Modulo, codigo_tfu, codigo_usu), "codigo_cpf", "nombre_cpf", "<<Seleccione>>")
        objFun = Nothing
        obj.CerrarConexion()

        obj = Nothing
    End Sub

    Sub CargarAmbientesDisponibles()
        Me.msg.innerHTML = ""
        ddlAmbientes.items.clear()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim objFun As New ClsFunciones
        objFun.CargarListas(Me.ddlAmbientes, obj.TraerDataTable("hRestriccionAmbCup_ConsultarAmbientesDisponibles", Me.ddlCarreraprofesional.selectedvalue, ddlCiclo.selectedvalue), "codigo_aam", "aula")
        objFun = Nothing
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub ddlCarreraProfesional_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarreraProfesional.SelectedIndexChanged
        CargarAmbientesDisponibles()
        CargarData()
    End Sub

    Protected Sub ddlCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCiclo.SelectedIndexChanged
        CargarCarrera()
        CargarAmbientesDisponibles()
    End Sub

   
    Sub CargarData()

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.gvData.datasource = obj.TraerDataTable("hRestriccionAmbCup_CursosProgramados", CInt(Request.QueryString("id")), Me.ddlCarreraprofesional.selectedvalue, ddlCiclo.selectedvalue, ddlCicloCur.selectedvalue)
        Me.gvData.Databind()
        obj.CerrarConexion()
        obj = Nothing

        'If Me.gvData.Rows.Count Then
        '    Me.button1.enabled = True
        'Else
        '    Me.button1.enabled = False
        'End If
    End Sub



    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim msgString As String = ""
        Dim Fila As GridViewRow
        Dim tb As data.datatable
        If (validaCheckActivo() = True) Then
            For i As Integer = 0 To Me.gvData.Rows.Count - 1
                Fila = Me.gvData.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                If (valor = True) Then

                    Dim obj As New ClsConectarDatos
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    obj.AbrirConexion()
                    tb = obj.TraerDataTable("hRestriccionAmbCup_CursosProgramadosRegistrar", Me.ddlAmbientes.selectedvalue, Me.gvData.DataKeys(i).Values("codigo_cup"), Me.ddlCiclo.selectedvalue)

                    If tb.rows(0).item(0) = "0" Then
                        msgString &= Me.gvData.DataKeys(i).Values("curso") & "</br>"
                    End If
                    obj.CerrarConexion()
                    obj = Nothing


                End If
            Next
        End If
        If msgString.Length > 0 Then
            Me.msg.innerHTML = "<b>No se registró,</b> vacantes <b>exceden la capacidad</b> del ambiente: </br> " & msgString
        Else
            Me.msg.innerHTML = ""
        End If
        CargarData()
    End Sub

    Private Function validaCheckActivo() As Boolean
        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Dim d As Integer = 0
        For i As Integer = 0 To Me.gvData.Rows.Count - 1
            Fila = Me.gvData.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If (valor = True) Then
                d = d + 1
                sw = 1
            End If
        Next

        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function

  

    Protected Sub gvData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvData.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "LimpiarAmbiente") Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("hRestriccionAmbCup_CursosProgramadosBorrar", gvData.DataKeys(index).Values("codigo"))
            CargarData()
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub

    Protected Sub gvData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If Not e.Row.Cells(4).Text.ToString = "-" Then
                e.Row.Cells(0).Text = ""
            End If
        End If
    End Sub


    Protected Sub ddlCicloCur_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCicloCur.SelectedIndexChanged
        CargarData()
    End Sub
End Class

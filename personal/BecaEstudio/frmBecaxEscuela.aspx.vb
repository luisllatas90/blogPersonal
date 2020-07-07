Partial Class BecaEstudio_frmBecaxCiclo
    Inherits System.Web.UI.Page

    Protected Sub gvBecaxEscuela_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvBecaxEscuela.RowUpdating
        'If Request.QueryString("id") <> "" Then
        '    Session("codigo_cpf") = Me.ddlEscuela.SelectedValue
        '    Session("Beca_codigo_cac") = Me.ddlCiclo.SelectedValue
        '    ' Session("codigo_bec") = Me.ddlTipoBeca.SelectedValue
        '    Dim objcnx As New ClsConectarDatos
        '    Dim valor As Integer
        '    valor = e.NewValues(0)
        '    objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        '    objcnx.AbrirConexion()
        '    objcnx.Ejecutar("Beca_RegistrarBecaxEscuela", Me.gvBecaxEscuela.DataKeys.Item(e.RowIndex).Values(0).ToString, CInt(Request.QueryString("id")), valor)
        '    objcnx.CerrarConexion()

        '    e.Cancel = True

        '    Response.Redirect("frmBecaxEscuela.aspx?id=" & Page.Request.QueryString("id") & "&ctf=" & Page.Request.QueryString("ctf"))
        'Else
        '    Response.Write("Debe iniciar sesión")
        'End If
    End Sub

    Sub CargarBecasAOtorgar()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("Beca_ConsultarBecasAOtorgar", Me.ddlCiclo.SelectedValue)
        obj.CerrarConexion()
        If tb.Rows.Count Then
            Session("beca_TotalOtorgar") = tb.Rows(9).Item("cantidad").ToString '#filas q devuelve Beca_ConsultarBecasAOtorgar
        Else
            Session("beca_TotalOtorgar") = 0
        End If
        Me.gvBecasAOtorgar.DataSource = tb
        Me.gvBecasAOtorgar.DataBind()

        obj.AbrirConexion()
        tb = obj.TraerDataTable("Beca_ConsultarBecaxEscuela2", Me.ddlCiclo.SelectedValue)
        obj.CerrarConexion()
        If tb.Rows.Count Then
            Dim sumaDif As Integer = 0
            For i As Integer = 0 To tb.Rows.Count - 1
                sumaDif = sumaDif + (CInt(tb.Rows(i).Item("TotalMatriculados").ToString) - CInt(tb.Rows(i).Item("TotalIngresantes").ToString))
            Next
            Session("SumaDif") = sumaDif
            Session("SumaDifTxt") = 5
        Else
            Session("SumaDif") = 0
        End If

        tb = Nothing
    End Sub
    Sub CargarCombos()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        tb = obj.TraerDataTable("Beca_ConsultarCicloAcademico")
        Me.ddlCiclo.DataSource = tb
        Me.ddlCiclo.DataTextField = "descripcion_cac"
        Me.ddlCiclo.DataValueField = "codigo_cac"
        Me.ddlCiclo.DataBind()
        If Session("Beca_codigo_cac") IsNot Nothing Then
            Me.ddlCiclo.SelectedValue = Session("Beca_codigo_cac")
        End If
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then            
            CargarCombos()
            Me.gvBecaxEscuela.GridLines = GridLines.Both
            Me.gvBecasAOtorgar.GridLines = GridLines.Both

        Else
            Session.Clear()
        End If
    End Sub

    Protected Sub gvBecaxEscuela_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvBecaxEscuela.RowDataBound
        Try
            Dim objcnx As New ClsConectarDatos
            Dim dt As New Data.DataTable
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(0).Text = e.Row.RowIndex + 1

                Dim CbBC As TextBox
                Dim CbBP As TextBox
                Dim lbDif As Label
                Dim lbBecasAsignar As Label

                lbDif = e.Row.FindControl("lblDiferencia")
                lbDif.Text = Integer.Parse(e.Row.Cells(3).Text) - Integer.Parse(e.Row.Cells(4).Text)
                lbBecasAsignar = e.Row.FindControl("txtBecasAsignar")
                Dim calc As Decimal = (Decimal.Parse(lbDif.Text) * CInt(Session("beca_TotalOtorgar"))) / CInt(Session("SumaDif"))
                lbBecasAsignar.Text = Decimal.Round(calc, 0)
                'verficarCantidad_bxe()
                CbBC = e.Row.FindControl("txtBecaCompleta")
                dt = objcnx.TraerDataTable("Beca_ConsultarBecaxEscuela", Me.ddlCiclo.SelectedValue, 1, Me.gvBecaxEscuela.DataKeys(e.Row.RowIndex).Values("codigo_cpf").ToString)

                If dt.Rows(0).Item("cantidad_bxe").ToString = "0" Then
                    CbBC.Text = e.Row.Cells(7).Text
                Else
                    CbBC.Text = dt.Rows(0).Item("cantidad_bxe").ToString
                End If

               ' If Me.gvBecaxEscuela.DataKeys(e.Row.RowIndex).Values("codigo_cpf").ToString = CInt(Session("SumaDifTxt")) Then lbBecasAsignar.Text = Decimal.Round(calc, 0) + 3
                'If Me.gvBecaxEscuela.DataKeys(e.Row.RowIndex).Values("codigo_cpf").ToString = CInt(Session("SumaDifTxt") + 27) Then lbBecasAsignar.Text = Decimal.Round(calc, 0) - 2
                'If Me.gvBecaxEscuela.DataKeys(e.Row.RowIndex).Values("codigo_cpf").ToString = CInt(Session("SumaDifTxt")) Then lbBecasAsignar.Text = Decimal.Round(calc, 0) + 2
                'If Me.gvBecaxEscuela.DataKeys(e.Row.RowIndex).Values("codigo_cpf").ToString = CInt(Session("SumaDifTxt") + 25) Then lbBecasAsignar.Text = Decimal.Round(calc, 0) - 2

                CbBP = e.Row.FindControl("txtBecaParcial")
                dt = objcnx.TraerDataTable("Beca_ConsultarBecaxEscuela", Me.ddlCiclo.SelectedValue, 2, Me.gvBecaxEscuela.DataKeys(e.Row.RowIndex).Values("codigo_cpf").ToString)
                If dt.Rows(0).Item("cantidad_bxe").ToString = "0" Then
                    CbBP.Text = (Integer.Parse(lbBecasAsignar.Text) - Integer.Parse(CbBC.Text)) * 2
                    If Integer.Parse(CbBP.Text) < 0 Then CbBP.Text = 0
                Else
                    CbBP.Text = dt.Rows(0).Item("cantidad_bxe").ToString
                End If

            End If
            objcnx.CerrarConexion()
            dt.Dispose()
            objcnx = Nothing

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Fila As GridViewRow
        Dim becaCompleta As Integer = 0
        Dim becaParcial As Integer = 0
        Dim valor As String
        For i As Integer = 0 To Me.gvBecaxEscuela.Rows.Count - 1
            Fila = Me.gvBecaxEscuela.Rows(i)
            valor = CType(Fila.FindControl("txtBecaCompleta"), TextBox).Text
            If (valor <> "") Then
                becaCompleta = CInt(valor)
            End If
            valor = CType(Fila.FindControl("txtBecaParcial"), TextBox).Text
            If (valor <> "") Then
                becaParcial = CInt(valor)            
            End If
            If CInt(Me.gvBecaxEscuela.DataKeys(i).Values("codigo_cpf") > 0) Then
                Dim objcnx As New ClsConectarDatos
                Dim dt As New Data.DataTable
                objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                objcnx.AbrirConexion()
                objcnx.Ejecutar("Beca_RegistrarBecaxEscuela2", CInt(Request.QueryString("id")), 1, Me.ddlCiclo.SelectedValue, CInt(becaCompleta), CInt(Me.gvBecaxEscuela.DataKeys(i).Values("codigo_cpf")))
                objcnx.Ejecutar("Beca_RegistrarBecaxEscuela2", CInt(Request.QueryString("id")), 2, Me.ddlCiclo.SelectedValue, CInt(becaParcial), CInt(Me.gvBecaxEscuela.DataKeys(i).Values("codigo_cpf")))
                objcnx.CerrarConexion()
            End If
        Next
    End Sub

    Protected Sub ddlCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCiclo.SelectedIndexChanged
        ' CargarBecasAOtorgar()
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            CargarBecasAOtorgar()
            Me.gvBecaxEscuela.DataSourceID = "SqlDataSource1"

        Catch ex As Exception
            Response.Write("aqui:" & ex.Message)
        End Try
        
    End Sub

    'Sub verficarCantidad_bxe()

    'End Sub

        

    Protected Sub btnCalcular_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalcular.Click
        Try
            Dim objcnx As New ClsConectarDatos
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()
            objcnx.Ejecutar("BECA_CALCULARNROMATRICULADOS_V2", Me.ddlCiclo.SelectedValue)
            objcnx.CerrarConexion()
        Catch ex As Exception
            Response.Write("aqui2" & ex.Message)
        End Try
    End Sub
End Class



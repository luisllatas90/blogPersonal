
Partial Class academico_estudiante_promedioTercioQuinto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("tipo") = "EST" Then
                ConsultarMerito("MED")
                Me.lnkMedioSup.ForeColor = Drawing.Color.Blue
                Me.lnkTercioSup.ForeColor = Drawing.Color.DodgerBlue
                Me.lnkQuintoSup.ForeColor = Drawing.Color.DodgerBlue
                Me.lnkMedioSup.Font.Underline = True
                Me.lnkTercioSup.Font.Underline = False
                Me.lnkQuintoSup.Font.Underline = False
            Else
                ConsultarMerito("TER")
                Me.lnkTercioSup.ForeColor = Drawing.Color.Blue
                Me.lnkMedioSup.ForeColor = Drawing.Color.DodgerBlue
                Me.lnkQuintoSup.ForeColor = Drawing.Color.DodgerBlue
                Me.lnkTercioSup.Font.Underline = True
                Me.lnkQuintoSup.Font.Underline = False
                Me.lnkMedioSup.Font.Underline = False
            End If
            
        End If
    End Sub

    Protected Sub gvCuadro_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCuadro.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem
            If Request.QueryString("tipo") = "EGR" Then
                If Request.QueryString("ce") < 69 Then
                    e.Row.Cells(0).Text = e.Row.RowIndex + 1
                End If
            End If
            'e.Row.Cells(0).Text = e.Row.RowIndex + 1
            If Fila.Item("codigo_alu") = Request.QueryString("codigo_alu") Then
                e.Row.BackColor = Drawing.Color.Yellow
            End If
        End If
    End Sub

    Protected Sub gvCuadroNuevo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCuadroNuevo.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            If Fila.Item("codigo_alu") = Request.QueryString("codigo_alu") Then
                e.Row.BackColor = Drawing.Color.Yellow
            End If
        End If
    End Sub

    Private Sub ConsultarMerito(ByVal Ver As String)
        Dim objCnx As New ClsConectarDatos
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objCnx.AbrirConexion()
        If Request.QueryString("tipo") = "EST" Then ' estudiante
            Me.lnkMedioSup.Visible = True
            gvCuadro.DataSource = objCnx.TraerDataTable("ACAD_ConsultarPromedioXCicloIngreso_V2", Ver, Request.QueryString("ci"), Request.QueryString("cpf"))
        Else ' Egresado 
            Me.lnkMedioSup.Visible = False
            If Request.QueryString("ce") >= 69 Then
                'Hcano 05/12/2019
                'Nueva Consulta de egresados del ciclo academico 2018-II en adelante
                gvCuadroNuevo.DataSource = objCnx.TraerDataTable("ACAD_ConsultarPromedioXCicloEgreso_V3", Ver, Request.QueryString("ce"), Request.QueryString("cpf"))
                gvCuadroNuevo.DataBind()
            Else
                gvCuadro.DataSource = objCnx.TraerDataTable("ACAD_ConsultarPromedioXCicloEgreso", Ver, Request.QueryString("ce"), Request.QueryString("cpf"))
            End If
        End If
        objCnx.CerrarConexion()
        gvCuadro.DataBind()
    End Sub

    Protected Sub lnkTercioSup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkTercioSup.Click
        ConsultarMerito("TER")
        Me.lnkTercioSup.ForeColor = Drawing.Color.Blue
        Me.lnkMedioSup.ForeColor = Drawing.Color.DodgerBlue
        Me.lnkQuintoSup.ForeColor = Drawing.Color.DodgerBlue
        Me.lnkTercioSup.Font.Underline = True
        Me.lnkQuintoSup.Font.Underline = False
        Me.lnkMedioSup.Font.Underline = False
    End Sub

    Protected Sub lnkQuintoSup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkQuintoSup.Click
        ConsultarMerito("QUI")
        Me.lnkTercioSup.ForeColor = Drawing.Color.DodgerBlue
        Me.lnkMedioSup.ForeColor = Drawing.Color.DodgerBlue
        Me.lnkQuintoSup.ForeColor = Drawing.Color.Blue
        Me.lnkMedioSup.Font.Underline = False
        Me.lnkTercioSup.Font.Underline = False
        Me.lnkQuintoSup.Font.Underline = True
    End Sub

    Protected Sub lnkMedioSup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMedioSup.Click
        ConsultarMerito("MED")
        Me.lnkMedioSup.ForeColor = Drawing.Color.Blue
        Me.lnkTercioSup.ForeColor = Drawing.Color.DodgerBlue
        Me.lnkQuintoSup.ForeColor = Drawing.Color.DodgerBlue
        Me.lnkMedioSup.Font.Underline = True
        Me.lnkTercioSup.Font.Underline = False
        Me.lnkQuintoSup.Font.Underline = False
    End Sub

End Class

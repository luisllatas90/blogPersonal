
Partial Class academico_cargalectiva_rptCargaAcademicaDocenteExceso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ListaCicloAcademico()
                ListaCarreraProfesional()
                ListaDedicación()
            End If
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try        
        'ddlDedicacion.selectedIndex = -1
    End Sub

    Private Sub ListaDedicación()

        Try
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim tbl As Data.DataTable

            tbl = obj.TraerDataTable("spPla_ConsultarDedicacion", "MT", "")
            ddlDedicacion.DataSource = tbl
            ddlDedicacion.DataTextField = "Descripcion_Ded"
            ddlDedicacion.DataValueField = "codigo_Ded"
            ddlDedicacion.dataBind()

            'ddlDedicacion.SelectedIndex = -1

        Catch ex As Exception
            Response.Write(ex.message)
        End Try
    End Sub

    Private Sub ListaCicloAcademico()
        Try
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim tbl As Data.DataTable
            tbl = obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0)
            ddlCicloAcademico.DataSource = tbl
            ddlCicloAcademico.DataTextField = "descripcion_Cac"
            ddlCicloAcademico.DataValueField = "codigo_Cac"
            ddlCicloAcademico.dataBind()

            'ddlCicloAcademico.SelectedIndex = -1
        Catch ex As Exception
            Response.Write(ex.message)
        End Try
    End Sub


    Private Sub ListaCarreraProfesional()
        Try
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim tbl As Data.DataTable
            tbl = obj.TraerDataTable("ConsultarCarreraProfesional", "MA", 0)

            ddlCarreraProfesional.DataSource = tbl
            ddlCarreraProfesional.DataTextField = "nombre_Cpf"
            ddlCarreraProfesional.DataValueField = "codigo_Cpf"
            ddlCarreraProfesional.dataBind()

            'ddlCarreraProfesional.SelectedIndex = -1
        Catch ex As Exception
            Response.Write(ex.message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim vMensaje As String
        Dim vTipo As String = ""

        Try
            If ddlCicloAcademico.selectedValue = -1 Then
                vMensaje = "Seleccione el ciclo academico"
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            Else
                vTipo = "A"
            End If

            If ddlDedicacion.selectedValue <> 0 Then
                vTipo = vTipo + "D"
            End If

            If ddlCarreraProfesional.selectedValue <> 0 Then
                vTipo = vTipo + "C"
            End If

            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim tbl As Data.DataTable

            Select Case vTipo
                Case "A"
                    tbl = obj.TraerDataTable("CAC_ListaDocentesCargaAcademicaExceso", "A", ddlCicloAcademico.selectedValue, 0, 0)
                    Exit Select
                Case "AD"
                    tbl = obj.TraerDataTable("CAC_ListaDocentesCargaAcademicaExceso", "AD", ddlCicloAcademico.selectedValue, ddlDedicacion.selectedValue, 0)
                Case "AC"
                    tbl = obj.TraerDataTable("CAC_ListaDocentesCargaAcademicaExceso", "AC", ddlCicloAcademico.selectedValue, 0, ddlCarreraProfesional.selectedValue)
                Case "ADC"
                    tbl = obj.TraerDataTable("CAC_ListaDocentesCargaAcademicaExceso", "ADC", ddlCicloAcademico.selectedValue, ddlDedicacion.selectedValue, ddlCarreraProfesional.selectedValue)
            End Select

            'Response.Write(tbl.Rows.Count)
            If tbl.Rows.Count > 0 Then
                lblNumreg.visible = True
                lblNumreg.text = "Se encontraron " & tbl.Rows.Count.tostring & " Registros"
                gvLista.DataSource = tbl
                gvLista.DataBind()
                gvLista.Columns(0).Visible = False
            End If

            'Response.Write(ddlCicloAcademico.selectedValue)
            'Response.Write("-")
            'Response.Write(ddlDedicacion.selectedValue)
            'Response.Write("-")
            'Response.Write(ddlCarreraProfesional.selectedValue)
        Catch ex As Exception
            Response.Write(ex.message)
        End Try
    End Sub

    Protected Sub ddlDedicacion_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDedicacion.DataBound
        'Valor 0
        ddlDedicacion.Items.Insert(0, New ListItem("TODOS", "0"))
    End Sub

    Protected Sub ddlCarreraProfesional_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarreraProfesional.DataBound
        'Valor 0
        ddlCarreraProfesional.Items.Insert(0, New ListItem("TODOS", "0"))
    End Sub


End Class


Partial Class academico_horarios_administrar_frmDescargarDataHorario
    Inherits System.Web.UI.Page
    Dim flag As Boolean = False
    Dim codCurso As String = ""
    Dim nombreCurso As String = ""
    Dim codCorrelativo As Integer = 0
    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        SubCargarData(1)
    End Sub

    Sub SubCargarData(ByVal hoja As Integer)
        Dim dt As New data.datatable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Select Case hoja
            Case 1
                dt = obj.TraerDataTable("Horario_CargarDataEntrada_Escuelas", Me.ddlCarreraProfesional.SelectedValue)
            Case 2
                dt = obj.TraerDataTable("Horario_CargarDataEntrada_Aulas", Me.ddlCarreraProfesional.SelectedValue, Me.ddlCiclo.SelectedValue)
            Case 3
                dt = obj.TraerDataTable("Horario_CargarDataEntrada_Profesores", Me.ddlCarreraProfesional.SelectedValue, Me.ddlCiclo.SelectedValue)

            Case 4
                dt = obj.TraerDataTable("Horario_CargarDataEntrada_Secciones", Me.ddlCarreraProfesional.SelectedValue, Me.ddlCiclo.SelectedValue)
                flag = True
        End Select
        obj.CerrarConexion()
        If dt.rows.count > 0 Then
            Me.GridView1.datasource = dt
        Else
            Me.GridView1.datasource = Nothing
        End If
        Me.GridView1.databind()
    End Sub

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

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        SubCargarData(2)
    End Sub

    Protected Sub LinkButton3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton3.Click
        SubCargarData(3)
    End Sub

    Protected Sub LinkButton4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton4.Click
        SubCargarData(4)
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If flag Then
            If e.Row.RowType = DataControlRowType.DataRow Then

                If nombreCurso = "" Then
                    nombreCurso = e.Row.Cells(0).Text  ' ANÁLISIS MATEMÁTICO I
                End If
                If codCurso = "" Then
                    codCurso = e.Row.Cells(1).Text  '23001
                End If

                If nombreCurso = e.Row.Cells(0).Text Then
                    If codCurso <> e.Row.Cells(1).Text Then                        
                        codCorrelativo += 1
                    End If
                Else
                    If codCurso <> e.Row.Cells(1).Text Then
                        codCorrelativo = 0
                    Else
                        codCorrelativo += 1
                    End If
                End If
                nombreCurso = e.Row.Cells(0).Text
                codCurso = e.Row.Cells(1).Text

                e.Row.Cells(1).Text = e.Row.Cells(1).Text & Format(codCorrelativo, "00")
            End If
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        exportar()
    End Sub

    Sub exportar()
        
        'Dim sb As StringBuilder = New StringBuilder()
        'Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        'Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)

        'Dim Page As Page = New Page()
        'Dim form As HtmlForm = New HtmlForm()
        'Page.EnableEventValidation = False
        'Page.DesignerInitialize()

        'Page.Controls.Add(form)
        'SubCargarData(1)
        'form.Controls.Add(Me.GridView1)

        'SubCargarData(2)
        'form.Controls.Add(Me.GridView1)


        'Page.RenderControl(htw)
        'Response.Clear()
        'Response.Buffer = True
        'Response.ContentType = "application/vnd.ms-excel"
        'Response.AddHeader("Content-Disposition", "attachment;filename=DataHorariosEntrada-.xls")
        'Response.Charset = "UTF-8"
        'Response.ContentEncoding = Encoding.Default
        'Response.Write(sb.ToString())
        'Response.End()
       
    End Sub
End Class

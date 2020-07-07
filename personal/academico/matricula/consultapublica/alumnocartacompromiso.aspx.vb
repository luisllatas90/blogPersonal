﻿
Partial Class academico_matricula_consultapublica_alumnocartacompromiso
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Then
            Response.Redirect("../../../../sinacceso.html")
        End If
        If Not Page.IsPostBack Then

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim objFun As New ClsFunciones

            ' objFun.CargarListas(Me.ddlEscuela, obj.TraerDataTable("ACAD_BuscaCarreraProfesionalv2", "1", 0, "", 0), "codigo_Cpf", "nombre_Cpf")
            objFun.CargarListas(Me.ddlCiclo, obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")

            obj.CerrarConexion()
            obj = Nothing

            Me.ddlCiclo.SelectedValue = Session("codigo_cac")

            fnLoading(False)
        End If


    End Sub


    Private Sub fnLoading(ByVal sw As Boolean)
        If sw Then
            Me.loading.Attributes.Remove("class")
            Me.loading.Attributes.Add("class", "hidden")
            Me.loader.Style("display") = "block"
        Else
            Me.loading.Attributes.Remove("class")
            Me.loading.Attributes.Add("class", "")
            Me.loader.Style("display") = "none"
        End If
    End Sub



    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        fnLoading(False)
        Try
            Dim codigo_per As Integer = CInt(Request.QueryString("id").ToString())

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            ' tb = obj.TraerDataTable("acad_consolidadoxescuela_padres", "P", Me.ddlCiclo.selectedvalue, Me.ddlPlan.SelectedValue)

            tb = obj.TraerDataTable("cartacompromiso_listar", codigo_per, Me.ddlCiclo.SelectedValue)


            If tb.Rows.Count > 0 Then
                btnExportar.Visible = True
            Else
                btnExportar.Visible = False
            End If


            Me.gData.DataSource = tb

            Me.gData.DataBind()
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        fnLoading(True)
    End Sub


    Protected Sub gData_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gData.PreRender
        If gData.Rows.Count > 0 Then
            gData.UseAccessibleHeader = True
            gData.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click



        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.gData.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gData)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=AlumnosCartadeCompromiso.xls")
        'Response.Charset = "UTF-8"
        'Response.ContentEncoding = Encoding.Default        
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Write(sb.ToString())
        Response.End()
    End Sub



End Class

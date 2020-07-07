Partial Class Egresado_campus_OfertasLaborales
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If CInt(Session("codigo_alu")) > 0 Then
                If IsPostBack = False Then
                    Session("carrera") = "0"
                    cargarCarreras()
                    'CargarOfertas()
                End If
                If Request.QueryString("x") IsNot Nothing And Request.QueryString("f") IsNot Nothing Then
                    If Request.QueryString("x") = "p" Then
                        Response.Redirect("OfertaLaboralPostular.aspx?xof=" & Request.QueryString("f"))
                    End If
                End If
            Else
                Response.Redirect("sesion.aspx")
            End If

        Catch ex As Exception
            'Response.Redirect("sesion.aspx")
            Response.Write(ex.Message & " -  " & ex.StackTrace)
        End Try

    End Sub

    Sub cargarCarreras()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As Data.DataTable
        dt = obj.TraerDataTable("ALUMNI_RetornaCarreraAlu", CInt(Session("codigo_alu")))
        If dt.Rows.Count Then
            Session("carrera") = CInt(dt.rows(0).item("puede"))
            Me.ddlCarrera.DataSource = dt
            Me.ddlCarrera.DataTextField = "nombre_Cpf"
            Me.ddlCarrera.DataValueField = "codigo_Cpf"
            Me.ddlCarrera.DataBind()
            Me.ddlCarrera.selectedvalue = CInt(dt.rows(0).item("puede"))

           
        End If
        dt.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub      
   
    Sub CargarOfertas()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim numero As Integer = 0
        Dim dt As New Data.DataTable
        Dim i As Integer = 0
        Me.dtablaOferta.InnerHtml = ""

        ' Response.Write("<script> alert('" & CInt(Session("codigo_alu")) & "')</script>")
        'Response.Write("<script> alert('" & Me.ddlCarrera.SelectedValue & "')</script>")
       

        dt = obj.TraerDataTable("ALUMNI_ListaOfertasxCarreraAluV2", CInt(Session("codigo_alu")), Me.ddlCarrera.SelectedValue, "V")

        'Response.Write("<script> alert('" & dt.Rows.Count.ToString & "')</script>")


        If dt.Rows.Count Then
            For i = 0 To dt.Rows.Count - 1
                Me.dtablaOferta.InnerHtml &= CreaTablaOferta(dt.Rows(i), i + 1)
            Next
            numero += dt.Rows.Count
        Else
            Me.dtablaOferta.InnerHtml = "No se encontraron ofertas laborales vigentes"
            Me.lblContador.Text = ""
        End If

        dt = New Data.DataTable
        dt = obj.TraerDataTable("ALUMNI_ListaOfertasxCarreraAluV2", CInt(Session("codigo_alu")), Me.ddlCarrera.SelectedValue, "N")

        'Response.Write("<script> alert(' Probando: " & dt.Rows.Count.ToString & "')</script>")

        If dt.Rows.Count Then
            For j As Integer = 0 To dt.Rows.Count - 1
                Me.dtablaOferta.InnerHtml &= CreaTablaOferta(dt.Rows(j), i + j + 1)
            Next
            numero += dt.Rows.Count
        Else
            If Me.dtablaOferta.InnerHtml = "" Then
                ' Me.dtablaOferta.InnerHtml = "No se encontraron ofertas laborales"
            End If
            Me.lblContador.Text = ""
        End If

        Me.lblContador.Text = "Se encontraron " & numero & " oferta(s) laboral(es)."

        dt.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Function CreaTablaOferta(ByVal dtData As System.Data.DataRow, ByVal i As Integer) As String
        Dim html As String = ""
        Dim obj As String = i.ToString
        html &= "<table class=""tablaOferta"" style=""border-bottom:0px;"" border=""0"">"
        html &= "<tr>"
        html &= "<td class=""filaTituloOferta"" >" & i & ".- " & dtData.Item("oferta").ToString.ToUpper & "</td>"
        html &= "<td class=""filaTituloOferta1"" >" & dtData.Item("empresa").ToString & "</td>"
        html &= "</tr>"
        Dim color As String = ""
        If Not CDate(Date.Today.ToShortDateString) > CDate(dtData.Item("fechaFinAnuncio").ToString) Then
            color = "red"
        End If

        html &= "<tr>"
        html &= "<td class=""filaDescripcion"" colspan=""2""> <b>Vigencia de Postulación&nbsp;&nbsp;&nbsp;</b> Desde: " & dtData.Item("fechaInicioAnuncio").ToString & "&nbsp;&nbsp;&nbsp;Hasta: <font color=""" & color & """> " & dtData.Item("fechaFinAnuncio").ToString & "</font> <br /><br /><a class=""StyleVerMas"" id=""vermasO_" & obj & """ onclick=""showDiv('" & obj & "');"" >Ver más detalle</a></td>"
        html &= "</tr>"

        html &= "<table class=""tablaOferta"" style=""border-top:0px;"" >"
        html &= "<tr>"
        html &= "<td width=""5%"" class=""CeldaTituloOferta"">Lugar</td>"
        html &= "<td  >" & dtData.Item("lugar").ToString & "</td>"
        html &= "</tr>"
        html &= "<tr>"
        html &= "<td class=""CeldaTituloOferta"">Tipo de Trabajo</td>"
        html &= "<td>" & dtData.Item("tipotrabajo_ofe").ToString & "</td>"
        html &= "</tr>"

        html &= "<tr class=""detalle_" & obj & """  style=""display:none;"">"
        html &= "<td class=""CeldaTituloOferta"">Descripción</td>"
        html &= "<td>" & dtData.Item("descripcion_ofe").ToString & "</td>"
        html &= "</tr>"

        html &= "<tr class=""detalle_" & obj & """  style=""display:none;"">"
        html &= "<td class=""CeldaTituloOferta"">Requisitos</td>"
        html &= "<td>" & dtData.Item("requisitos_ofe").ToString & "</td>"
        html &= "</tr>"

        html &= "<tr class=""detalle_" & obj & """  style=""display:none;"">"
        html &= "<td class=""CeldaTituloOferta"">Contacto</td>"
        html &= "<td>" & dtData.Item("contacto").ToString & "</td>"
        html &= "</tr>"

        If Me.ddlCarrera.SelectedValue = CInt(Session("carrera")) Then 'Solo postula a las ofertas de su carrera
            html &= "<td colspan=""3"" class=""CeldaEnlace"">"

            If CDate(Date.Today.ToShortDateString) > CDate(dtData.Item("fechaFinAnuncio").ToString) Then
                html &= "<a class=""btn"" href=""#"" disabled="""" >No Vigente</a>"
            ElseIf dtData.Item("modopostular_ofe").ToString = "W" Then
                html &= "<a class=""btn"" style=""color:navy;"" href=""http://" & dtData.Item("web_ofe").ToString & """ target=""_blank"">Postular</a>"
            ElseIf dtData.Item("modopostular_ofe").ToString = "C" Then
                If dtData.Item("enviado").ToString <> "0" Then
                    html &= "<a class=""btn"" style=""color:#e33439;"">CV Enviado</a>"
                Else
                    html &= "<a class=""btn"" style=""color:navy;"" href=""?x=p&f=" & encode(dtData.Item("codigo_ofe").ToString) & """>Postular</a>"
                End If
            Else
                html &= "<a class=""btn"" href=""#"" disabled=""disabled"" >Postular</a>"
            End If

            html &= "</td>"
            html &= "</tr>"
        End If
        html &= "</table></br>"
        html &= "</table>"
        Return html
    End Function


    Protected Sub ddlCarrera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarrera.SelectedIndexChanged
        CargarOfertas()
    End Sub

    Protected Sub ddlFecha_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFecha.SelectedIndexChanged
        CargarOfertas()
    End Sub

    Function encode(ByVal str As String) As String
        Return (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(str)))
    End Function

    Function decode(ByVal str As String) As String
        Return System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(str))
    End Function
  
End Class

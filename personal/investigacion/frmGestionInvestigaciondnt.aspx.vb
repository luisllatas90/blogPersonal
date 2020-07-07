Imports System.Data
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Globalization
Imports System.Web
Imports AjaxControlToolkit

Partial Class frmGestionInvestigaciondnt
    Inherits System.Web.UI.Page

#Region "Métodos y Funciones del Formulario"

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-PE")
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
        If Not IsPostBack Then
            CargarInvestigaciones()
            CargarUsuario()
        End If
    End Sub

    Protected Sub gvInvestigacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvInvestigacion.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvInvestigacion','Select$" & e.Row.RowIndex & "');")
                e.Row.Style.Add("cursor", "hand")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvInvestigacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvInvestigacion.SelectedIndexChanged
        Try
            pnlDetalle.CssClass = ""
            hfCodInvestigacion.Value = gvInvestigacion.SelectedRow.Cells(0).Text
            Dim objInv As New clsInvestigacion
            Dim dt As New DataTable
            dt = objInv.ConsultaInvestigaciones(hfCodInvestigacion.Value)
            lblTitulo.Text = dt.Rows(0).Item("titulo")
            lblFechaRegistro.Text = dt.Rows(0).Item("fechaRegistro")
            lblFecIni.Text = dt.Rows(0).Item("fechaInicio")
            lblFecFin.Text = dt.Rows(0).Item("fechaFin")
            lblPresupuesto.Text = dt.Rows(0).Item("presupuesto")
            lblFinanci.Text = dt.Rows(0).Item("tipoFinanciamiento")
            lblAmbito.Text = dt.Rows(0).Item("Ambito")
            lblLinea.Text = dt.Rows(0).Item("linea")
            lblEtapa.Text = dt.Rows(0).Item("etapa")
            lblTipo.Text = dt.Rows(0).Item("tipo")
            lblInstancia.Text = dt.Rows(0).Item("instancia")
            lblBeneficiarios.Text = dt.Rows(0).Item("beneficiarios")
            lblResumen.Text = dt.Rows(0).Item("resumen")

            'Creando la tabla Documentos
            Dim dtDocumentos As New Data.DataTable
            dtDocumentos.Columns.Add("extension", GetType(String))
            dtDocumentos.Columns.Add("nombre", GetType(String))
            dtDocumentos.Columns.Add("ruta", GetType(String))
            dtDocumentos.Columns.Add("documento", GetType(String))

            If dt.Rows(0).Item("rutaInforme") <> "" Then
                Dim myrow As Data.DataRow
                myrow = dtDocumentos.NewRow
                myrow("extension") = dt.Rows(0).Item("extInf")
                myrow("nombre") = "Informe"
                myrow("ruta") = dt.Rows(0).Item("rutaInforme")
                myrow("documento") = dt.Rows(0).Item("docInf")
                dtDocumentos.Rows.Add(myrow)
            End If
            If dt.Rows(0).Item("rutaProyecto") <> "" Then
                Dim myrow As Data.DataRow
                myrow = dtDocumentos.NewRow
                myrow("extension") = dt.Rows(0).Item("extPro")
                myrow("nombre") = "Proyecto"
                myrow("ruta") = dt.Rows(0).Item("rutaProyecto")
                myrow("documento") = dt.Rows(0).Item("docPro")
                dtDocumentos.Rows.Add(myrow)
            End If
            gvDocumentos.DataSource = dtDocumentos
            gvDocumentos.DataBind()

            gvBitacora.DataSource = objInv.ConsultaBitacora(hfCodInvestigacion.Value)
            gvBitacora.DataBind()

            CargarResumen()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarResumen()
        Dim objInv As New clsInvestigacion
        gvResumen.DataSource = objInv.ConsultaResumen(hfCodInvestigacion.Value)
        gvResumen.DataBind()
    End Sub

    Protected Sub gvInvestigacion_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvInvestigacion.PageIndexChanging
        gvInvestigacion.PageIndex = e.NewPageIndex
        CargarInvestigaciones()
    End Sub

    Protected Sub Menu1_MenuItemClick(ByVal sender As Object, _
      ByVal e As MenuEventArgs) Handles Menu1.MenuItemClick
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value)
        Dim i As Integer
        'Make the selected menu item reflect the correct imageurl
        For i = 0 To Menu1.Items.Count - 1
            If i = e.Item.Value Then
                If i = 0 Then
                    Menu1.Items(i).ImageUrl = "~/Images/TagButtons/btnInvDGSel.png"
                ElseIf i = 1 Then
                    Menu1.Items(i).ImageUrl = "~/Images/TagButtons/btnInvBitacoraSel.png"
                ElseIf i = 2 Then
                    Menu1.Items(i).ImageUrl = "~/Images/TagButtons/btnInvResumenSel.png"
                End If
            Else
                If i = 0 Then
                    Menu1.Items(i).ImageUrl = "~/Images/TagButtons/btnInvDG.png"
                ElseIf i = 1 Then
                    Menu1.Items(i).ImageUrl = "~/Images/TagButtons/btnInvBitacora.png"
                ElseIf i = 2 Then
                    Menu1.Items(i).ImageUrl = "~/Images/TagButtons/btnInvResumen.png"
                End If
            End If
        Next
    End Sub

    Protected Sub gvBitacora_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvBitacora.PageIndexChanging
        gvBitacora.PageIndex = e.NewPageIndex
        Dim objInv As New clsInvestigacion
        gvBitacora.DataSource = objInv.ConsultaBitacora(hfCodInvestigacion.Value)
        gvBitacora.DataBind()
    End Sub

    Protected Sub gvResumen_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvResumen.PageIndexChanging
        gvResumen.PageIndex = e.NewPageIndex
        CargarResumen()
    End Sub

    Protected Sub gvResumen_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvResumen.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvResumen','Select$" & e.Row.RowIndex & "');")
                e.Row.Style.Add("cursor", "hand")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvResumen_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvResumen.SelectedIndexChanged
        Dim hfAvance As New HiddenField
        Dim objInv As New clsInvestigacion
        hfAvance = gvResumen.SelectedRow.FindControl("hfAvance_id")
        dlObservaciones.DataSource = objInv.ConsultaObservacion(hfCodInvestigacion.Value, hfAvance.Value)
        dlObservaciones.DataBind()
    End Sub

#End Region

#Region "Métodos y Funciones de Usuario"

    Private Sub CargarInvestigaciones()
        Dim objInv As New clsInvestigacion
        gvInvestigacion.DataSource = objInv.ConsultaInvestigaciones(0)
        gvInvestigacion.DataBind()
    End Sub

    Private Sub CargarUsuario()
        Dim objPre As New ClsPresupuesto
        Dim dt As New DataTable
        dt = objPre.ObtenerDatosUsuario(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            hfUsuReg.Value = dt.Rows(0).Item("usuario_per")
        End If
    End Sub

#End Region

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim objInv As New clsInvestigacion
        Dim hf As New HiddenField
        hf = gvResumen.SelectedRow.FindControl("hfAvance_id")
        objInv.AbrirTransaccionCnx()
        objInv.AgregaObservacion(hfCodInvestigacion.Value, hf.Value, txtObs.text, Session("ruta"), Request.QueryString("id").ToString, hfUsuReg.Value)
        objInv.CerrarTransaccionCnx()
        dlObservaciones.DataSource = objInv.ConsultaObservacion(hfCodInvestigacion.Value, hf.Value)
        dlObservaciones.DataBind()
    End Sub

    Private Sub afuObservacion_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles afuObservacion.UploadedComplete
        Dim filePath As String
        Dim archivo As String = "\" & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & System.IO.Path.GetExtension(afuObservacion.PostedFile.FileName).ToString
        filePath = Server.MapPath("../../filesInvestigacion")
        filePath = filePath & "\" & hfCodInvestigacion.Value
        Dim carpeta As New System.IO.DirectoryInfo(filePath)
        If carpeta.Exists = False Then
            carpeta.Create()
        End If
        Session("ruta") = filePath & archivo
        afuObservacion.SaveAs(filePath & archivo)
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmRegistrarInvestigaciones.aspx?tipo=N&id=" & Request.QueryString("id").ToString & "';", True)
    End Sub

    Protected Sub lbModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim row As GridViewRow
        Dim lbModificar As LinkButton
        lbModificar = sender
        row = lbModificar.NamingContainer
        ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmRegistrarInvestigaciones.aspx?idInv=" & row.Cells.Item(0).Text & "&id=" & Request.QueryString("id").ToString & "&tipo=M';", True)
    End Sub

    Protected Sub hlNuevoHito_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mpeDescripcion.Show()
    End Sub

    '-########## dnt ###############
    Protected Sub hlEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim row As GridViewRow
            Dim hlEnviar As LinkButton
            Dim obj As New clsInvestigacion
            Dim dts As New Data.DataTable

            hlEnviar = sender
            row = hlEnviar.NamingContainer
            'Lo ejecute en un evento debido a que me estaba dando problemas ejecutarlo aqui!!
            EjecucionEnvios(row.Cells.Item(0).Text, Me.Request.QueryString("id"))
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EjecucionEnvios(ByVal id_investigacion As Integer, ByVal codigo_per As Integer)
        Try
            Dim exec As Integer = 0
            Dim obj As New clsInvestigacion

            obj.AbrirTransaccionCnx()
            exec = obj.EjecutaEnvioInvestigacion(id_investigacion, codigo_per, 0)
            obj.CerrarTransaccionCnx()
            obj = Nothing
            If exec > 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('El envio se ha realizado de forma satisfactoria.');", True)
                CargarInvestigaciones()
                EnviarMensaje(id_investigacion)
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Se produjo un error al enviar, favor de volver intentar.');", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EnviarMensaje(ByVal id_investigacion As Integer)
        Dim ObjMailNet As New ClsMail
        Dim mensaje As String = ""
        Dim para As String = ""
        Dim obj As New clsInvestigacion
        Dim dts As New Data.DataTable

        dts = obj.ListaInvestigacionRevisores(id_investigacion)
        For i As Integer = 0 To dts.Rows.Count - 1
            If dts.Rows(i).Item("email_Per") <> "" Then
                para = "</br>" & "Estimado(a): " & dts.Rows(i).Item("nombre_revisor").ToString.ToUpper
                mensaje = "</br>Se le comunica que, usted es miembro del " & dts.Rows(i).Item("nombre_comite").ToString.ToUpper & " y tiene como pendiente de revisión la investigación " & dts.Rows(i).Item("nombre_investigacion").ToString.ToUpper

                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Investigaciones", dts.Rows(i).Item("email_Per").ToString, "Miembro revisor de investigaciones USAT", para & mensaje, True)
            End If
        Next
    End Sub

    'Private Function GenerarMensaje() As String
    '    Dim strMensaje As String
    '    strMensaje = "</br>Se ha aprobado una nueva subasta con los siguientes Artículos: </br></br>"
    '    strMensaje = strMensaje & "<table border=1 width=100%><tr><td><b>Artículo</b></td><td><b>Cantidad</b></td><td><b>Total</b></td></tr>"
    '    For Each row As GridViewRow In gvArticulos.Rows
    '        strMensaje = strMensaje & "<tr><td>" & row.Cells.Item(1).Text & "</td><td>" & row.Cells.Item(2).Text & "</td><td>" & row.Cells.Item(3).Text & "</td></tr>"
    '    Next
    '    strMensaje = strMensaje & "</table>"
    '    Return strMensaje
    'End Function

End Class

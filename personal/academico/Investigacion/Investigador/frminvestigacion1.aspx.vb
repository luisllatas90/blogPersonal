Partial Class Investigador_frminvestigacion1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Me.Form.Attributes.Add("onSubmit", "return OcultarTabla();")

            Me.LblTitulo.Text = "Registrar una Nueva Investigación"
            Dim Datos As New Data.DataTable
            Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

            ClsFunciones.LlenarListas(Me.DDlUnidad, ObjDatos.TraerDataTable("ConsultarUnidadesInvestigacion", "15", ""), "codigo_cco", "descripcion_cco", "--- Seleccione Unidad de Investigación ---")

            Datos.Dispose()
            ClsFunciones.LlenarListas(Me.DDLArea, Datos, "codigo_Are", "nombre_are", "---- Seleccione Area de Investigación ----")
            ClsFunciones.LlenarListas(Me.DDLTematica, Datos, "codigo_Are", "nombre_are", "---- Seleccione Linea o Temática -----")
            Datos.Dispose()

            Dim codigo_inv As String
            codigo_inv = Request.QueryString("codigo_Inv")
            If codigo_inv <> "" Then

                Me.LblTitulo.Text = "Modificar una Investigación"
                Dim ObjInv As New Investigacion
                Datos = ObjInv.ConsultarInvestigaciones("13", codigo_inv.ToString)
                Me.TxtTitulo.Text = Datos.Rows(0).Item("Titulo_Inv")
                Me.DDlUnidad.SelectedValue = Datos.Rows(0).Item("codigo_cco")
                ClsFunciones.LlenarListas(Me.DDLArea, ObjDatos.TraerDataTable("INV_ConsultarUnidadesInvestigacion", 0, Me.DDlUnidad.SelectedValue), "codigo_Are", "nombre_are", "---- Seleccione Area de Investigación ----")
                Me.DDLArea.SelectedValue = Datos.Rows(0).Item("linea")
                ClsFunciones.LlenarListas(Me.DDLTematica, ObjDatos.TraerDataTable("INV_ConsultarUnidadesInvestigacion", Me.DDLArea.SelectedValue, Me.DDlUnidad.SelectedValue), "codigo_are", "nombre_are", "--- Seleccione Línea de Investigación ---")
                Me.DDLTematica.SelectedValue = Datos.Rows(0).Item("Codigo_are")

                Me.TxtDuracion.Text = Datos.Rows(0).Item("duracion_inv")
                Me.DDLDuracion.SelectedValue = Datos.Rows(0).Item("tipoduracion_inv")
                Me.DDLAmbito.SelectedValue = Datos.Rows(0).Item("ambito_inv")
                Me.DDLPoblacion.SelectedValue = Datos.Rows(0).Item("poblacion_inv")
                Me.TxtDetalle.Text = Datos.Rows(0).Item("detallezona_inv")
                Datos.Dispose()
                ObjInv = Nothing
            End If
        End If
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim strruta As String
        strruta = Server.MapPath("../../../../filesInvestigacion/")
        Dim objNuevoInv As New Investigacion
        Dim codigo_inv As String

        codigo_inv = Request.QueryString("codigo_inv")
        If codigo_inv = "" Then
            codigo_inv = objNuevoInv.NuevaInvestigacion("1", Me.TxtTitulo.Text, 1, Me.DDLTematica.SelectedValue, 1, _
            Me.FilePerfil, Me.TxtDuracion.Text, Me.DDLDuracion.SelectedValue, Me.DDLAmbito.SelectedValue, _
            Me.DDLPoblacion.SelectedValue, Me.TxtDetalle.Text.Trim, strruta, Request.QueryString("id")).ToString
        Else
            codigo_inv = objNuevoInv.ModificarInvestigacion(codigo_inv, Me.TxtTitulo.Text.Trim, 1, 1, Me.FilePerfil, strruta, "N", 0, _
            Request.QueryString("id"), Me.DDLTematica.SelectedValue, CInt(Me.TxtDuracion.Text), Me.DDLDuracion.SelectedValue, _
            Me.DDLAmbito.SelectedValue, Me.DDLPoblacion.SelectedValue, Me.TxtDetalle.Text.Trim, "")
        End If

        'Response.Write(codigo_inv)
        If codigo_inv <> "-1" Then
            Response.Redirect("agrega_responsables.aspx?codigo_Inv=" & codigo_inv)
        Else
            Dim scriptError As String
            scriptError = "<script>alert('Ocurrió un error insertar la investigación.')</script>"
            Page.RegisterStartupScript("Error", codigo_inv)
        End If
        objNuevoInv = Nothing
    End Sub

    Protected Sub DDlUnidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDlUnidad.SelectedIndexChanged
        Dim ObjArea As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim DATOS As New Data.DataTable
        ClsFunciones.LlenarListas(Me.DDLArea, ObjArea.TraerDataTable("INV_ConsultarUnidadesInvestigacion", 0, Me.DDlUnidad.SelectedValue), "codigo_are", "nombre_are", "--- Seleccione Area de Investigación ---")
        ClsFunciones.LlenarListas(Me.DDLTematica, DATOS, "codigo_Are", "nombre_are", "---- Seleccione Linea o Temática -----")
        DATOS = Nothing
    End Sub

    Protected Sub DDLArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLArea.SelectedIndexChanged
        Dim ObjArea As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(Me.DDLTematica, ObjArea.TraerDataTable("INV_ConsultarUnidadesInvestigacion", Me.DDLArea.SelectedValue, Me.DDlUnidad.SelectedValue), "codigo_are", "nombre_are", "--- Seleccione Línea de Investigación ---")
    End Sub
End Class

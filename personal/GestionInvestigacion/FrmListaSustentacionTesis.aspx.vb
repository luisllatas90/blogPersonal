﻿Imports System.Collections.Generic
Imports System.IO

Partial Class GestionInvestigacion_FrmListaSustentacionTesis
    Inherits System.Web.UI.Page
    Dim contador As Integer = 0
    Dim ruta As String = ConfigurationManager.AppSettings("SharedFiles")
    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmBusqueda.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            If IsPostBack = False Then
                Me.Detalle.Visible = False
                ConsultarTipoEstudio()
                ListarDepartamentos()
                ListarTipoParticipante()
                ListarAmbientes()
                'ScriptManager.GetCurrent(Me.Page).RegisterPostBackControl(Me.btnGuardar)
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub ConsultarTipoEstudio()
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ConsultarTipoEstudio("GT", 0)
        Me.ddlTipoEstudio.Items.Clear()
        Me.ddlTipoEstudio.Items.Add(New ListItem("[ -- Seleccione -- ]", ""))
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.ddlTipoEstudio.Items.Add(New ListItem(dt.Rows(i).Item("descripcion_test"), dt.Rows(i).Item("codigo_test")))
        Next
    End Sub

    Private Sub ConsultarCarreras(ByVal codigo_test As Integer, ByVal id As Integer, ByVal ctf As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        Dim codigo_apl As Integer = 72 'GESTION INVESTIGACIÓN
        Me.ddlCarrera.Items.Clear()
        Me.ddlCarrera.Items.Add(New ListItem("[ -- Seleccione -- ]", ""))
        dt = obj.ConsultarCarrerasxTipoEstudio(codigo_test, id, ctf, codigo_apl)
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.ddlCarrera.Items.Add(New ListItem(dt.Rows(i).Item("nombre_Cpf"), dt.Rows(i).Item("codigo_cpf")))
        Next
    End Sub

    Private Sub ListarDepartamentos()
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ConsultarDepartamentosAcademicos(1, "", "")
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.ddlDepartamento.Items.Add(New ListItem(dt.Rows(i).Item("descripcion"), dt.Rows(i).Item("codigo")))
        Next
    End Sub

    Private Sub ListarDocentes(ByVal codigo_dac As String)
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        Me.ddlDocente.Items.Clear()
        Me.ddlDocente.Items.Add(New ListItem("[ -- Seleccione -- ]", ""))
        dt = obj.ConsultarPersonalxDepartamentoAcademico(codigo_dac)
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.ddlDocente.Items.Add(New ListItem(dt.Rows(i).Item("descripcion"), dt.Rows(i).Item("codigo")))
        Next
    End Sub

    Private Sub ListarTipoParticipante()
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ConsultarTipoParticipante("3", "J")
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.ddlTipoParticipante.Items.Add(New ListItem(dt.Rows(i).Item("descripcion"), dt.Rows(i).Item("codigo")))
        Next
    End Sub

    Private Sub ListarJurados(ByVal codigo_Tes As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ListarParticipantesTesis(codigo_Tes, "J", "S")
        Me.gvJurados.DataSource = dt
        Me.gvJurados.DataBind()
        Dim contador As Integer = 0
        Dim contadorpendiente As Integer = 0
        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(i).Item("apruebadirector").ToString = "APROBADO" And (dt.Rows(i).Item("descripcion_tpi").ToString = "JURADO-PRESIDENTE" Or dt.Rows(i).Item("descripcion_tpi").ToString = "JURADO-SECRETARIO") Then
                contador = contador + 1
            End If
            If dt.Rows(i).Item("apruebadirector").ToString = "PENDIENTE" Then
                contadorpendiente = contadorpendiente + 1
            End If
        Next

        If contadorpendiente > 0 Or contador < 2 Then
            hdValidaResolucion.Value = 0
        Else
            hdValidaResolucion.Value = 1
        End If
    End Sub

    Private Sub ListarAmbientes()
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        Me.ddlAmbiente.Items.Clear()
        Me.ddlAmbiente.Items.Add(New ListItem("[ -- Seleccione -- ]", ""))
        dt = obj.ListarAmbientes()
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.ddlAmbiente.Items.Add(New ListItem(dt.Rows(i).Item("ambiente"), dt.Rows(i).Item("codigo_amb")))
        Next
    End Sub

    Protected Sub ddlTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoEstudio.SelectedIndexChanged
        If Me.ddlTipoEstudio.SelectedValue <> "" Then
            ConsultarCarreras(Me.ddlTipoEstudio.SelectedValue, Request("id"), Request("ctf"))
        Else
            Me.ddlCarrera.Items.Clear()
            Me.ddlCarrera.Items.Add(New ListItem("[ -- Seleccione -- ]", ""))
        End If
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        Try
            If validarBusqueda() = True Then
                Dim dt As New Data.DataTable
                Dim obj As New ClsGestionInvestigacion
                dt = obj.ConsultarAlumnosSustentacion(Me.ddlCarrera.SelectedValue, Me.ddlEstado.SelectedValue, Me.txtBusqueda.Text)
                Me.gvAlumnos.DataSource = dt
                Me.gvAlumnos.DataBind()
                Me.Detalle.Visible = False
                Me.Listado.Visible = True
            End If
        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "ocultarE", "fnLoading(false)", True)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + ex.Message.ToString + "')", True)
        End Try
    End Sub

    Function validarBusqueda() As Boolean
        If Me.ddlTipoEstudio.SelectedValue = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione un tipo de estudio')", True)
            Return False
        End If
        If Me.ddlCarrera.SelectedValue = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione una carrera')", True)
            Return False
        End If
        If Me.ddlEstado.SelectedValue = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione una estado')", True)
            Return False
        End If
        Return True
    End Function

    Protected Sub gvAlumnos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAlumnos.RowCommand
        If e.CommandName = "Ver" Then
            Me.Detalle.Visible = True
            Me.Listado.Visible = False

            Me.hdValidarActa.Value = 0
            Me.hdValidaResolucion.Value = 0

            Me.hdCodTes.Value = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_tes").ToString
            Me.hdCA.Value = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_alu").ToString

            Dim obj As New ClsGestionInvestigacion
            Dim dt As New Data.DataTable
            dt = obj.ConsultarTesisSustentacion(Me.hdCA.Value, Me.hdCodTes.Value)

            Me.hdExpediente.Value = dt.Rows(0).Item("NroExpediente")
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtTitulo)
            Me.txtTitulo.Text = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("titulo_tes").ToString


            Me.lblAlumno.Text = dt.Rows(0).Item("Alumno")
            Me.lblCodigoUniversitario.Text = dt.Rows(0).Item("codigoUniver_Alu")

            Me.ddlDepartamento.SelectedValue = ""
            Me.ddlDocente.Items.Clear()
            Me.ddlDocente.Items.Add(New ListItem("[ -- Seleccione -- ]", ""))
            Me.ddlTipoParticipante.SelectedValue = ""
            ListarJurados(Me.hdCodTes.Value)


            Me.txtfechainforme.Text = dt.Rows(0).Item("fechaInformeAsesor")
            If dt.Rows(0).Item("fechaInformeAsesor").ToString = "" Then
                Me.hdValidaResolucion.Value = -3
            End If

            Me.txtFechaResolucion.Text = dt.Rows(0).Item("fechaResolucion")
            If dt.Rows(0).Item("fechaResolucion").ToString = "" Then
                Me.hdValidaResolucion.Value = -2
            End If
            Me.txtNroResolucion.Text = dt.Rows(0).Item("NroResolucion")
            If dt.Rows(0).Item("NroResolucion") = "" Then
                Me.hdValidaResolucion.Value = -1
            End If
            Me.txtFecha.Text = dt.Rows(0).Item("fechaSustentacion")
            Me.ddlAmbiente.SelectedValue = dt.Rows(0).Item("codigo_amb")
            If dt.Rows(0).Item("codigo_amb").ToString = "" Then
                Me.hdValidarActa.Value = -2
            End If
            If dt.Rows(0).Item("fechaSustentacion").ToString = "" Then
                Me.hdValidarActa.Value = -1
            End If


            If dt.Rows(0).Item("informe") <> "" Then
                lkbDescargarInforme.Attributes.Add("class", "btn btn-xs btn-orange btn-radius")
                lkbDescargarInforme.Text = "Descargar Informe"
                lkbDescargarInforme.Attributes.Add("onclick", "fnDescargar('" + dt.Rows(0).Item("informe") + "'); return false")
            Else
                lkbDescargarInforme.Attributes.Remove("class")
                lkbDescargarInforme.Text = ""
                lkbDescargarInforme.Attributes.Remove("onclick")
            End If

            If dt.Rows(0).Item("resolucion") <> "" Then
                lbkDescargarResolucion.Attributes.Add("class", "btn btn-xs btn-orange btn-radius")
                lbkDescargarResolucion.Text = "Descargar Resolución"
                lbkDescargarResolucion.Attributes.Add("onclick", "fnDescargar('" + dt.Rows(0).Item("resolucion") + "'); return false")
            Else
                lbkDescargarResolucion.Attributes.Remove("class")
                lbkDescargarResolucion.Text = ""
                lbkDescargarResolucion.Attributes.Remove("onclick")
            End If

            If dt.Rows(0).Item("acta") <> "" Then
                lkbDescargarActa.Attributes.Add("class", "btn btn-xs btn-orange btn-radius")
                lkbDescargarActa.Text = "Descargar Informe"
                lkbDescargarActa.Attributes.Add("onclick", "fnDescargar('" + dt.Rows(0).Item("acta") + "'); return false")
            Else
                lkbDescargarActa.Attributes.Remove("class")
                lkbDescargarActa.Text = ""
                lkbDescargarActa.Attributes.Remove("onclick")
            End If
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "CargaControlCalendario", "Calendario()", True)

        End If
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            Me.Detalle.Visible = False
            Me.Listado.Visible = True
            
        Catch ex As Exception
            Me.lblmensaje.InnerText = ex.Message.ToString
        End Try
    End Sub


    Protected Sub ddlDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamento.SelectedIndexChanged
        If Me.ddlDepartamento.SelectedValue <> "" Then
            ListarDocentes(Me.ddlDepartamento.SelectedValue)
        Else
            Me.ddlDocente.Items.Clear()
            Me.ddlDocente.Items.Add(New ListItem("[ -- Seleccione -- ]", ""))

        End If
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsGestionInvestigacion
            If Me.ddlDocente.SelectedValue <> "" Then
                If Me.ddlTipoParticipante.SelectedValue <> "" Then
                    If validarJurados() = True Then
                        dt = obj.ActualizarAsesorTesis(0, Me.hdCodTes.Value, Me.ddlDocente.SelectedValue, "S", Me.ddlTipoParticipante.SelectedValue, 1, Session("id_per"))
                        If dt.Rows(0).Item("Respuesta") = 1 Then
                            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','Jurado Registrado Correctamente')", True)
                            ListarJurados(Me.hdCodTes.Value)
                            Me.ddlDocente.SelectedValue = ""
                            Me.ddlTipoParticipante.SelectedValue = ""
                            Me.hdValidaResolucion.Value = 0
                        Else
                            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Jurado no se pudo Registrar')", True)

                        End If
                    End If
                Else
                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione un rol para el docente a asignar')", True)
                End If
            Else
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione un Docente para asignar')", True)
            End If
        Catch ex As Exception
            Mensaje(ex.Message.ToString, "alert alert-danger")
        End Try
    End Sub

    Private Sub Mensaje(ByVal texto As String, ByVal tipo As String)
        Me.lblmensaje.InnerHtml = texto
        Me.lblmensaje.Attributes.Add("class", tipo)
    End Sub

    Protected Sub gvJurados_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvJurados.RowCommand
        If e.CommandName = "Quitar" Then
            Dim obj As New ClsGestionInvestigacion
            Dim dt As New Data.DataTable
            dt = obj.EliminarJuradoTesis(Me.gvJurados.DataKeys(e.CommandArgument).Values("codigo_jur"), Session("id_per"))
            If dt.Rows(0).Item("Respuesta") = 1 Then
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','Jurado eliminado correctamente')", True)
                ListarJurados(Me.hdCodTes.Value)
            Else
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Jurado no se pudo eliminar')", True)
            End If
        End If
    End Sub

    Public Function validarJurados() As Boolean
        If Me.gvJurados.Rows.Count = 3 Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Solo se permiten asignar 3 jurados')", True)
            Return False
        End If

        For i As Integer = 0 To Me.gvJurados.Rows.Count - 1
            If Me.gvJurados.DataKeys(i).Values("codigo_Per").ToString = Me.ddlDocente.SelectedValue Then
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Docente solo puede se asignado una sola vez')", True)
                Return False
                Exit For
            End If
            If Me.gvJurados.DataKeys(i).Values("codigo_Tpi").ToString = Me.ddlTipoParticipante.SelectedValue Then
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Rol solo puede se asignado una sola vez')", True)
                Return False
                Exit For
            End If
        Next
        Return True
    End Function


    Function validarGenerarResolución() As Boolean
        If Me.hdValidaResolucion.Value = "0" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Debe contar al menos con dos jurados(Presidente y secretario) asignados y aprobados por el director de área')", True)
            Return False
        End If
        If Me.hdValidaResolucion.Value = "-1" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Debe Guardar el N° de resolución antes de generar la Resolución')", True)
            Return False
        End If
        If Me.hdValidaResolucion.Value = "-2" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Debe guardar la fecha de resolución antes de generar la Resolución')", True)
            Return False
        End If
        If Me.hdValidaResolucion.Value = "-3" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Debe guardar la fecha informe de asesor')", True)
            Return False
        End If
        Return True
    End Function

    Function validarGenerarActa() As Boolean
        Return validarGenerarResolución()
        If Me.hdValidarActa.Value = "-1" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Debe Guardar la fecha y hora de sustentación')", True)
            Return False
        End If
        If Me.hdValidarActa.Value = "-2" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Debe guardar el ambiente donde se realizara la sustentación')", True)
            Return False
        End If
        Return True
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New ClsConectarDatos
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.IniciarTransaccion()

            Dim respuestaArchivo As String = ""
            Dim respuestaxml As New Xml

            If Me.ArchivoInforme.PostedFile.ContentLength < 26214400 And Me.ArchivoInforme.PostedFile.FileName <> "" Then
                respuestaArchivo = SubirArchivo(Me.hdCodTes.Value, Me.ArchivoInforme.PostedFile, "I")
                If respuestaArchivo.Contains("procesado correctamente") Then
                    obj.TraerDataTable("TES_ActualizarIDArchivoCompartidoTesis", 23, Me.hdCodTes.Value, 8, "S", 684)
                Else
                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','No se pudo guardar Archivo de Informe')", True)
                End If
            End If

            If Me.archivoResolucion.PostedFile.ContentLength < 20971520 And Me.archivoResolucion.PostedFile.FileName <> "" Then
                respuestaArchivo = SubirArchivo(Me.hdCodTes.Value, Me.archivoResolucion.PostedFile, "R")
                If respuestaArchivo.Contains("procesado correctamente") Then
                    obj.TraerDataTable("TES_ActualizarIDArchivoCompartidoTesis", 23, Me.hdCodTes.Value, 9, "S", 684)
                Else
                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','No se pudo guardar Archivo de Resolución')", True)
                End If
            End If

            If Me.archivoActa.PostedFile.ContentLength < 20971520 And Me.archivoActa.PostedFile.FileName <> "" Then
                respuestaArchivo = SubirArchivo(Me.hdCodTes.Value, Me.archivoActa.PostedFile, "A")
                If respuestaArchivo.Contains("procesado correctamente") Then
                    obj.TraerDataTable("TES_ActualizarIDArchivoCompartidoTesis", 23, Me.hdCodTes.Value, 10, "S", 684)
                Else
                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','No se pudo guardar Archivo de acta de sustentación')", True)
                End If
            End If

            
            obj.TraerDataTable("TES_ActualizarDatosSustentacion", Me.hdCodTes.Value, Me.txtTitulo.Text, Me.txtFecha.Text, Me.ddlAmbiente.SelectedValue, Me.txtNroResolucion.Text, Me.txtFechaResolucion.Text, Me.txtfechainforme.Text)

            obj.TerminarTransaccion()
            obj.CerrarConexion()
            'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','Archivo de informe guardado correctamente.')", True)
            'Mensaje("Archivo de informe Guardado correctamente", "alert alert-success")
            btnConsultar_Click(sender, e)

            Me.Detalle.Visible = False
            Me.Listado.Visible = True
        Catch ex As Exception
            obj.AbortarTransaccion()
            obj.CerrarConexion()
            'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + ex.Message.ToString + "')", True)
            'Response.Write("<script>fnMensaje('error','" + ex.Message.ToString + "')</script>")
            Mensaje(ex.Message.ToString, "alert alert-danger")
        End Try
    End Sub


    Function SubirArchivo(ByVal codigo As String, ByVal ArchivoSubir As HttpPostedFile, ByVal tipo As String) As String
        Try
            Dim idtabla As Integer = 23
            Dim post As HttpPostedFile = ArchivoSubir
            Dim cod_operacion As String = 0
            Dim Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim Usuario As String = Session("perlogin").ToString
            Dim Input(post.ContentLength) As Byte
            Dim etapa As String = "S"

            If tipo = "I" Then
                cod_operacion = 8
            ElseIf tipo = "R" Then
                cod_operacion = 9
            ElseIf tipo = "A" Then
                cod_operacion = 10
            End If

            Dim b As New BinaryReader(post.InputStream)
            Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)

            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)
            '  Dim list As New List(Of Dictionary(Of String, String))()
            list.Add("Fecha", Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
            list.Add("Nombre", Regex.Replace(System.IO.Path.GetFileName(post.FileName), "[^0-9A-Za-z._ ]", "").Replace(",", ""))
            list.Add("TransaccionId", codigo)
            list.Add("TablaId", idtabla)
            list.Add("NroOperacion", cod_operacion)
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Usuario)


            Return result

        Catch ex As Exception
            'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + ex.Message.ToString + "')", True)
            Return ex.Message.ToString()
        End Try

    End Function

    Private Sub ActualizarArchivoTesis(ByVal idtabla As Integer, ByVal idtransaccion As String, ByVal idoperacion As String, ByVal abreviatura As String, ByVal codigo_per As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarArchivosTesis(idtabla, idtransaccion, idoperacion, abreviatura, codigo_per)
            Dim data As New Dictionary(Of String, Object)()
            data.Add("cod", obj.EncrytedString64(idtransaccion))
            'data.Add("ruta", ruta)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "1 - ACTUALIZAR ARCHIVO COMP." + idtransaccion)
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub


    Protected Sub btnGenerarResolucion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerarResolucion.Click
        Try
            If validarGenerarResolución() = True Then
                Dim ruta As String = "GenerarPDF.aspx?tipo=RESO&t=" + Me.hdCodTes.Value + "&a=" + Me.hdCA.Value + "&x=" + Me.hdExpediente.Value
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Prueba2", "window.open('" + ruta + "','_blank')", True)
            End If
        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert1", "fnMensaje('error','No se pudo generar Resolución')", True)
        End Try
    End Sub

    Protected Sub btnGenerarActa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerarActa.Click
        Try
            If validarGenerarActa() = True Then
                Dim ruta As String = "GenerarPDF.aspx?tipo=ACTA&t=" + Me.hdCodTes.Value + "&a=" + Me.hdCA.Value + "&x=" + Me.hdExpediente.Value
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Prueba2", "window.open('" + ruta + "','_blank')", True)
            End If
             Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','No se pudo generar Acta')", True)
        End Try
    End Sub
End Class

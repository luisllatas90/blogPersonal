﻿Imports System.Data
Imports System.IO
Imports System.Collections.Generic

Partial Class FrmConformidadAutorizacionPublicacion
    Inherits System.Web.UI.Page
    Dim ruta As String = ConfigurationManager.AppSettings("SharedFiles")

    'Private Sub ListarSemestre()
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim tb As New Data.DataTable
    '    tb = obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0)
    '    Me.ddlSemestre.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
    '    If tb.Rows.Count > 0 Then
    '        For i As Integer = 0 To tb.Rows.Count - 1
    '            Me.ddlSemestre.Items.Add(New ListItem(tb.Rows(i).Item("descripcion_Cac"), tb.Rows(i).Item("codigo_Cac")))
    '            If tb.Rows(i).Item("vigencia_Cac") = "1" Then
    '                Me.ddlSemestre.SelectedValue = tb.Rows(i).Item("codigo_Cac")
    '            End If
    '        Next
    '    End If
    '    obj.CerrarConexion()
    'End Sub

    'Private Sub ListarCarreras()
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim tb As New Data.DataTable
    '    tb = obj.TraerDataTable("INV_ConsultarCarreraTrabajoInvestigacion", Me.ddlSemestre.SelectedValue, Session("id_per"), Request("ctf"))

    '    If tb.Rows.Count > 0 Then
    '        For i As Integer = 0 To tb.Rows.Count - 1
    '            Me.ddlCarrera.Items.Add(New ListItem(tb.Rows(i).Item("nombre_cpf"), tb.Rows(i).Item("codigo_cpf")))
    '        Next

    '    End If
    '    obj.CerrarConexion()
    'End Sub


    'Private Sub ListarAsignaturas(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer, ByVal codigo_per As Integer, ByVal ctf As Integer)
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim dt As New Data.DataTable
    '    dt = obj.TraerDataTable("INV_ConsultarCursosTrabajoInvestigacion", codigo_cac, codigo_cpf, codigo_per, ctf)
    '    'Me.ddlAsignatura.Items.Clear()
    '    'Me.ddlAsignatura.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
    '    If dt.Rows.Count > 0 Then
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            Me.ddlAsignatura.Items.Add(New ListItem(dt.Rows(i).Item("nombre_curso"), dt.Rows(i).Item("codigo_cup")))
    '        Next
    '    End If
    '    obj.CerrarConexion()
    'End Sub

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    If (Session("id_per") Is Nothing) Then
    '        Response.Redirect("../../../sinacceso.html")
    '    End If
    '    Try
    '        If IsPostBack = False Then
    '            ListarSemestre()
    '            If Me.ddlSemestre.SelectedValue <> "" Then
    '                ListarCarreras()
    '            End If
    '            Me.ddlEstado.Items.Clear()
    '            Me.ddlEstado.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
    '            If Request("ctf") = 1 Then ' ADMINISTRADOR
    '                Me.ddlEstado.Items.Add(New ListItem("PENDIENTE", "P"))
    '                Me.ddlEstado.Items.Add(New ListItem("CON RÚBRICA", "R"))
    '                Me.ddlEstado.Items.Add(New ListItem("CON ACTA", "A"))
    '                Me.ddlEstado.Items.Add(New ListItem("TODOS", "%"))
    '            ElseIf Request("ctf") = 9 Then ' DIRECTOR DE ESCUELA
    '                Me.ddlEstado.Items.Add(New ListItem("PENDIENTE", "R"))
    '                Me.ddlEstado.Items.Add(New ListItem("CON ACTA", "A"))
    '                Me.ddlEstado.Items.Add(New ListItem("TODOS", "%"))
    '            Else ' PROFESOR
    '                Me.ddlEstado.Items.Add(New ListItem("PENDIENTE", "P"))
    '                Me.ddlEstado.Items.Add(New ListItem("CON RÚBRICA", "R"))
    '                Me.ddlEstado.Items.Add(New ListItem("TODOS", "%"))

    '            End If

    '        End If
    '    Catch ex As Exception
    '        Response.Write(ex.Message.ToString)
    '    End Try
    'End Sub

    'Private Sub listarAlumnos(ByVal codigo_cac As String, ByVal codigo_cpf As String, ByVal codigo_cup As Integer, ByVal estado As String, ByVal codigo_per As Integer, ByVal ctf As Integer)
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim tb As New Data.DataTable
    '    tb = obj.TraerDataTable("INV_ConsultarAlumnosTrabajoInvestigacion", codigo_cac, codigo_cpf, codigo_cup, estado, codigo_per, ctf)
    '    If tb.Rows.Count > 0 Then
    '        Me.gvAlumnos.DataSource = tb
    '    Else
    '        Me.gvAlumnos.DataSource = ""
    '    End If
    '    Me.gvAlumnos.DataBind()
    '    obj.CerrarConexion()
    'End Sub

    'Private Sub CargarDatosTrabajoInvestigacion(ByVal codigo_tba As Integer, ByVal codigo_cac As Integer)
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim dt As New Data.DataTable
    '    dt = obj.TraerDataTable("INV_CargarDatosTrabajoInvestigacion", codigo_tba, codigo_cac)
    '    Me.hdcod.Value = codigo_tba
    '    Me.hdcac.Value = codigo_cac
    '    If dt.Rows.Count > 0 Then
    '        Me.txtcodigouniver.Text = dt.Rows(0).Item("codigoUniver_Alu").ToString
    '        Me.txtestudiante.Text = dt.Rows(0).Item("responsable").ToString
    '        Me.txtCarrera.Text = dt.Rows(0).Item("nombre_Cpf").ToString
    '        Me.txtTitulo.Text = dt.Rows(0).Item("titulo_tba").ToString
    '        Me.txtTipo.Text = dt.Rows(0).Item("tipo_tba").ToString
    '        Me.txtLinea.Text = dt.Rows(0).Item("linea").ToString
    '        Me.txtArea.Text = dt.Rows(0).Item("area").ToString
    '        Me.txtSubArea.Text = dt.Rows(0).Item("subarea").ToString
    '        Me.txtDisciplina.Text = dt.Rows(0).Item("disciplina").ToString
    '        If dt.Rows(0).Item("archivo").ToString <> "" Then
    '            Dim btn As New LinkButton
    '            btn.Text = "Descargar trabajo <i class='ion-android-attach'></i>"
    '            btn.CssClass = "btn btn-sm btn-warning"
    '            btn.OnClientClick = "fnDescargar('" + dt.Rows(0).Item("archivo") + "');return false;"
    '            Me.divArchivoTrabajo.Controls.Add(btn)
    '        End If
    '        If dt.Rows(0).Item("rubrica").ToString <> "" Then
    '            Dim btn1 As New LinkButton
    '            btn1.Text = "Descargar rúbrica <i class='ion-android-attach'></i>"
    '            btn1.CssClass = "btn btn-sm btn-info"
    '            btn1.OnClientClick = "fnDescargar('" + dt.Rows(0).Item("rubrica") + "');return false;"
    '            Me.divArchivoRubrica.Controls.Add(btn1)
    '        End If
    '        If dt.Rows(0).Item("acta").ToString <> "" Then
    '            Dim btn1 As New LinkButton
    '            btn1.Text = "Descargar acta <i class='ion-android-attach'></i>"
    '            btn1.CssClass = "btn btn-sm btn-success"
    '            btn1.OnClientClick = "fnDescargar('" + dt.Rows(0).Item("acta") + "');return false;"
    '            Me.DivArchivoActa.Controls.Add(btn1)
    '            Me.ddlCondicion.SelectedValue = dt.Rows(0).Item("observacion").ToString

    '        End If
    '    End If
    '    obj.CerrarConexion()
    'End Sub

    'Protected Sub ddlSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemestre.SelectedIndexChanged
    '    If (Session("id_per") Is Nothing) Then
    '        Response.Redirect("../../../sinacceso.html")
    '    End If

    '    Me.ddlCarrera.Items.Clear()
    '    Me.ddlCarrera.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
    '    Me.ddlAsignatura.Items.Clear()
    '    Me.ddlAsignatura.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
    '    If Me.ddlSemestre.SelectedValue <> "" Then
    '        ListarCarreras()
    '    End If
    'End Sub

    'Protected Sub ddlCarrera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarrera.SelectedIndexChanged
    '    If (Session("id_per") Is Nothing) Then
    '        Response.Redirect("../../../sinacceso.html")
    '    End If
    '    Me.ddlAsignatura.Items.Clear()
    '    Me.ddlAsignatura.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
    '    If Me.ddlSemestre.SelectedValue <> "" And Me.ddlCarrera.SelectedValue <> "" Then
    '        ListarAsignaturas(Me.ddlSemestre.SelectedValue, Me.ddlCarrera.SelectedValue, Session("id_per"), Request("ctf"))
    '    End If
    'End Sub


    'Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
    '    If (Session("id_per") Is Nothing) Then
    '        Response.Redirect("../../../sinacceso.html")
    '    End If
    '    Me.lblmensaje.InnerText = ""
    '    Me.lblmensaje.Attributes.Remove("class")
    '    If Me.ddlSemestre.SelectedValue <> "" Then
    '        If Me.ddlCarrera.SelectedValue <> "" Then
    '            If Me.ddlAsignatura.SelectedValue <> "" Then
    '                If Me.ddlEstado.SelectedValue <> "" Then
    '                    listarAlumnos(Me.ddlSemestre.SelectedValue, Me.ddlCarrera.SelectedValue, Me.ddlAsignatura.SelectedValue, Me.ddlEstado.SelectedValue, Session("id_per"), Request("ctf"))
    '                Else
    '                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione una estado')", True)
    '                End If
    '            Else
    '                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione una asignatura')", True)
    '            End If
    '        Else
    '            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione una carrera')", True)

    '        End If
    '    Else
    '        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione un semestre')", True)

    '    End If
    'End Sub

    'Protected Sub gvAlumnos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAlumnos.RowCommand
    '    If (Session("id_per") Is Nothing) Then
    '        Response.Redirect("../../../sinacceso.html")
    '    End If
    '    If e.CommandName = "Ver" Then
    '        Dim codigo As Integer = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_tba").ToString
    '        Dim codigo_cac As Integer = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_cac").ToString
    '        Me.btnGuardar.Visible = True
    '        Me.divArchivoRubrica.InnerHtml = ""
    '        Me.DivArchivoActa.InnerHtml = ""
    '        If Me.gvAlumnos.DataKeys(e.CommandArgument).Values("estado").ToString = "PENDIENTE" Then
    '            Me.btnGuardar.Text = "Guardar Rúbrica"
    '            Me.btnGuardar.OnClientClick = "return ValidarRubrica();"
    '            Me.DivRubrica.Visible = True
    '            Me.divArchivoRubrica.Visible = False
    '            Me.DivActa.Visible = False
    '            Me.DivArchivoActa.Visible = False
    '            Me.lblacta.Visible = False
    '            Me.condicion.Visible = False
    '            Me.btnGuardar.Visible = True
    '            Me.lblRubricaTexto.Visible = True
    '            If Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_tat").ToString = "0" Then
    '                Me.DivRubrica.Visible = False
    '                Me.btnGuardar.Visible = False
    '                Me.lblRubricaTexto.Visible = False
    '            End If
    '        ElseIf Me.gvAlumnos.DataKeys(e.CommandArgument).Values("estado").ToString = "CON RÚBRICA" Then
    '            Me.btnGuardar.Text = "Guardar Acta"
    '            Me.btnGuardar.OnClientClick = "return ValidarActa();"
    '            Me.btnGuardar.Visible = True
    '            Me.lblRubricaTexto.Visible = True
    '            Me.DivRubrica.Visible = False
    '            Me.divArchivoRubrica.Visible = True
    '            Me.DivActa.Visible = True
    '            Me.DivArchivoActa.Visible = False
    '            Me.lblacta.Visible = True
    '            Me.condicion.Visible = True
    '            Me.ddlCondicion.Enabled = True
    '            If Request("ctf") = 13 Then
    '                Me.lblacta.Visible = False
    '                Me.DivActa.Visible = False
    '                Me.DivArchivoActa.Visible = False
    '                Me.btnGuardar.Visible = False
    '                Me.condicion.Visible = False
    '            End If
    '        ElseIf Me.gvAlumnos.DataKeys(e.CommandArgument).Values("estado").ToString = "CON ACTA" Then
    '            Me.btnGuardar.Visible = False
    '            Me.lblRubricaTexto.Visible = True
    '            Me.DivRubrica.Visible = False
    '            Me.divArchivoRubrica.Visible = True
    '            Me.DivActa.Visible = False
    '            Me.DivArchivoActa.Visible = True
    '            Me.lblacta.Visible = True
    '            Me.condicion.Visible = True
    '            Me.ddlCondicion.Enabled = False
    '            If Request("ctf") = 13 Then
    '                Me.lblacta.Visible = False
    '                Me.DivActa.Visible = False
    '                Me.DivArchivoActa.Visible = False
    '                Me.btnGuardar.Visible = False
    '                Me.condicion.Visible = False
    '            End If
    '        End If
    '        CargarDatosTrabajoInvestigacion(codigo, codigo_cac)
    '        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "MostrarModal('mdEditar')", True)
    '    End If
    '    If e.CommandName = "Bloquear" Then
    '        Dim codigo As Integer = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_tba").ToString
    '        Dim codigo_cac As Integer = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_cac").ToString
    '        Dim bloqueo As Integer = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("bloqueo").ToString
    '    End If
    'End Sub

    'Private Sub ActualizarBloqueoTrabajo(ByVal codigo_tba As Integer, ByVal codigo_cac As Integer, ByVal bloqueo As Integer, ByVal codigo_per As Integer)
    '    Dim obj As New ClsConectarDatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim tb As New Data.DataTable
    '    tb = obj.TraerDataTable("INV_ActualizarBloqueoTrabajoInvestigacion", codigo_tba, codigo_cac, bloqueo, codigo_per)

    '    obj.CerrarConexion()
    '    If tb.Rows(0).Item("Respuesta") = 1 Then
    '        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','" + tb.Rows(0).Item("Mensaje") + "')", True)
    '    Else
    '        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + tb.Rows(0).Item("Mensaje") + "')", True)
    '    End If
    'End Sub

    'Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
    '    If (Session("id_per") Is Nothing) Then
    '        Response.Redirect("../../../sinacceso.html")
    '    End If
    '    If Me.btnGuardar.Text = "Guardar Rúbrica" Then
    '        If Me.archivorubrica.HasFile = True Then
    '            If validarArchivos() = True Then
    '                If SubirArchivo(29, Me.hdcod.Value, Me.archivorubrica.PostedFile, 3, Me.hdcac.Value, "") Then
    '                    listarAlumnos(Me.ddlSemestre.SelectedValue, Me.ddlCarrera.SelectedValue, Me.ddlAsignatura.SelectedValue, Me.ddlEstado.SelectedValue, Session("id_per"), Request("ctf"))
    '                    Me.lblmensaje.InnerText = "Rúbrica adjuntada correctamente"
    '                    Me.lblmensaje.Attributes.Add("class", "alert alert-success")
    '                Else
    '                    Me.lblmensaje.InnerText = "No se pudo subir rúbrica"
    '                    Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
    '                End If
    '            End If
    '        End If
    '    End If
    '    If Me.btnGuardar.Text = "Guardar Acta" Then
    '        If Me.archivoacta.HasFile = True Then
    '            If validarArchivos() = True Then
    '                If SubirArchivo(29, Me.hdcod.Value, Me.archivoacta.PostedFile, 4, Me.hdcac.Value, Me.ddlCondicion.SelectedValue) Then
    '                    listarAlumnos(Me.ddlSemestre.SelectedValue, Me.ddlCarrera.SelectedValue, Me.ddlAsignatura.SelectedValue, Me.ddlEstado.SelectedValue, Session("id_per"), Request("ctf"))
    '                    Me.lblmensaje.InnerText = "Acta adjuntada correctamente"
    '                    Me.lblmensaje.Attributes.Add("class", "alert alert-success")
    '                Else
    '                    Me.lblmensaje.InnerText = "No se pudo subir acta"
    '                    Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub


    'Function validarArchivos() As Boolean
    '    Me.lblmensaje.InnerText = ""
    '    Me.lblmensaje.Attributes.Remove("class")
    '    If Me.archivorubrica.HasFile = True Then
    '        If Me.archivorubrica.PostedFile.ContentLength > 20971520 Then
    '            Me.lblmensaje.InnerText = "Solo puede adjuntar archivos de un tamaño máximo de 20MB"
    '            Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
    '            Return False
    '        End If

    '        Dim Extensiones As String() = {".pdf"}
    '        Dim validarArchivo As Integer = -1
    '        validarArchivo = Array.IndexOf(Extensiones, System.IO.Path.GetExtension(archivorubrica.FileName))
    '        If validarArchivo = -1 Then
    '            Me.lblmensaje.InnerText = "Solo puede adjuntar archivos de rúbrica en formato .pdf"
    '            Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
    '            Return False
    '        End If

    '    End If
    '    If Me.archivoacta.HasFile = True Then
    '        If Me.archivoacta.PostedFile.ContentLength > 20971520 Then
    '            Me.lblmensaje.InnerText = "Solo puede adjuntar archivos de un tamaño máximo de 20MB"
    '            Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
    '            Return False
    '        End If

    '        Dim Extensiones As String() = {".pdf"}
    '        Dim validarArchivo As Integer = -1
    '        validarArchivo = Array.IndexOf(Extensiones, System.IO.Path.GetExtension(archivoacta.FileName))
    '        If validarArchivo = -1 Then
    '            Me.lblmensaje.InnerText = "Solo puede adjuntar archivos de acta en formato .pdf"
    '            Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
    '            Return False
    '        End If

    '    End If
    '    Return True
    'End Function

    'Function SubirArchivo(ByVal idtabla As Integer, ByVal codigo As String, ByVal ArchivoSubir As HttpPostedFile, ByVal nro_operacion As Integer, ByVal codigo_cac As Integer, ByVal observacion As String) As Boolean
    '    Try
    '        'Dim dt As New Data.DataTable
    '        'Dim obj As New ClsGestionInvestigacion

    '        Dim post As HttpPostedFile = ArchivoSubir
    '        Dim codigo_con As String = codigo
    '        Dim nrooperacion As String = nro_operacion 'NUMERO DE OPERACIÓN 11 PARA ARCHIVOS DE ASESORIAS
    '        Dim Fecha As String = Date.Now.ToString("dd/MM/yyyy")
    '        Dim Usuario As String = Session("perlogin").ToString
    '        Dim Input(post.ContentLength) As Byte
    '        ' Dim b As New BinaryReader(post.InputStream)
    '        '  Dim by() As Byte = b.ReadByte(post.ContentLength)

    '        Dim b As New BinaryReader(post.InputStream)
    '        Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
    '        Dim base64 = System.Convert.ToBase64String(binData)

    '        Dim wsCloud As New ClsArchivosCompartidos
    '        Dim list As New Dictionary(Of String, String)
    '        '  Dim list As New List(Of Dictionary(Of String, String))()
    '        list.Add("Fecha", Fecha)
    '        list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
    '        list.Add("Nombre", System.IO.Path.GetFileName(post.FileName))
    '        list.Add("TransaccionId", codigo_con)
    '        list.Add("TablaId", idtabla)
    '        list.Add("NroOperacion", nrooperacion)
    '        list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
    '        list.Add("Usuario", Usuario)
    '        list.Add("Equipo", "")
    '        list.Add("Ip", "")
    '        list.Add("param8", Usuario)

    '        Dim envelope As String = wsCloud.SoapEnvelope(list)
    '        Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Session("perlogin").ToString)
    '        'Me.TextBox1.Text = envelope
    '        If result.Contains("Registro procesado correctamente") Then
    '            Dim obj As New ClsConectarDatos
    '            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '            obj.AbrirConexion()
    '            Dim dt As New Data.DataTable
    '            dt = obj.TraerDataTable("INV_ActualizarArchivoTrabajoInvestigacionColaborador", idtabla, codigo, nrooperacion, codigo_cac, observacion, Session("id_per"))
    '            obj.CerrarConexion()
    '            Return True
    '        Else
    '            Return False
    '        End If

    '        'Me.lblMensaje.InnerText = envelope
    '        'Response.Write(result)
    '    Catch ex As Exception
    '        'Dim Data1 As New Dictionary(Of String, Object)()
    '        'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '        'Dim JSONresult As String = ""
    '        'Dim list As New List(Of Dictionary(Of String, Object))()
    '        'Data1.Add("rpta", "1 - SUBIR ARCHIVO")
    '        'Data1.Add("msje", ex.Message)
    '        'list.Add(Data1)
    '        'JSONresult = serializer.Serialize(list)
    '        'Me.txtbusqueda.Text = JSONresult
    '        'Me.TextBox1.Text = JSONresult
    '        'ClientScript.RegisterClientScriptBlock(Me.GetType(), "alerta", "<script>alert('" + JSONresult + "')</script>")
    '        'Response.Write(JSONresult)
    '        'Me.lblmensaje.InnerHtml = Me.lblmensaje.InnerHtml + "<div class='row' style='color:red'>" + ex.Message + "</div>"
    '        Return False
    '    End Try
    'End Function

    'Protected Sub gvAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAlumnos.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim estado As String = Me.gvAlumnos.DataKeys(e.Row.RowIndex).Values("estado").ToString
    '        If estado = "CON RÚBRICA" Or estado = "CON ACTA" Then
    '            Dim boton As LinkButton = DirectCast(e.Row.Cells(5).FindControl("btnBloquear"), LinkButton)
    '            Dim boton2 As LinkButton = DirectCast(e.Row.Cells(5).FindControl("btnAutorizar"), LinkButton)
    '            boton.Visible = False
    '            boton2.Visible = False
    '        Else
    '            If Me.gvAlumnos.DataKeys(e.Row.RowIndex).Values("bloqueo") = "1" Then
    '                Dim boton As LinkButton = DirectCast(e.Row.Cells(5).FindControl("btnBloquear"), LinkButton)
    '                boton.CssClass = "btn btn-warning btn-sm"
    '                boton.ToolTip = "Desbloquear"
    '                boton.Text = "<span class='fa fa-unlock'></span>"
    '                boton.OnClientClick = "return confirm('¿Está seguro que desea desbloquear trabajo?')"
    '            End If
    '            If Me.gvAlumnos.DataKeys(e.Row.RowIndex).Values("autoriza") = "1" Then
    '                Dim boton As LinkButton = DirectCast(e.Row.Cells(5).FindControl("btnAutorizar"), LinkButton)
    '                boton.CssClass = "btn btn-warning btn-sm"
    '                boton.ToolTip = "Quitar Autorización"
    '                boton.Text = "<span class='fa fa-times-circle'></span>"
    '                boton.OnClientClick = "return confirm('¿Estpa seguro que desea quitar autorización del registro?')"
    '            End If
    '        End If
    '    End If
    'End Sub
End Class


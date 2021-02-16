Imports System.Data
Imports System.IO
Imports System.Collections.Generic

Partial Class FrmActualizacionTrabajo
    Inherits System.Web.UI.Page
    Dim ruta As String = ConfigurationManager.AppSettings("SharedFiles")

    Private Sub ListarSemestre()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0)
        Me.ddlSemestre.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlSemestre.Items.Add(New ListItem(tb.Rows(i).Item("descripcion_Cac"), tb.Rows(i).Item("codigo_Cac")))
                If tb.Rows(i).Item("vigencia_Cac") = "1" Then
                    Me.ddlSemestre.SelectedValue = tb.Rows(i).Item("codigo_Cac")
                End If
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ListarCarreras()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("INV_ConsultarCarreraTrabajoInvestigacion", Me.ddlSemestre.SelectedValue, Session("id_per"), Request("ctf"))

        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlCarrera.Items.Add(New ListItem(tb.Rows(i).Item("nombre_cpf"), tb.Rows(i).Item("codigo_cpf")))
            Next

        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ListarAsignaturas(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer, ByVal codigo_per As Integer, ByVal ctf As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("INV_ConsultarCursosTrabajoInvestigacion", codigo_cac, codigo_cpf, codigo_per, ctf)
        'Me.ddlAsignatura.Items.Clear()
        'Me.ddlAsignatura.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Me.ddlAsignatura.Items.Add(New ListItem(dt.Rows(i).Item("nombre_curso"), dt.Rows(i).Item("codigo_cup")))
            Next
        End If
        obj.CerrarConexion()
    End Sub
    Private Sub ListarLineasUSAT()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("INV_ListaLineasInvestigacion", 0)
        Me.ddlLinea.Items.Add(New ListItem("[-- SELECCIONE --]", "0"))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlLinea.Items.Add(New ListItem(tb.Rows(i).Item("nombre"), tb.Rows(i).Item("codigo")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ListarTiposTrabajo()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("INV_ListarTiposTrabajoBachiller")
        Me.ddlTipoTrabajo.Items.Add(New ListItem("[-- SELECCIONE --]", "0"))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlTipoTrabajo.Items.Add(New ListItem(tb.Rows(i).Item("nombre_ttb"), tb.Rows(i).Item("codigo_ttb")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ListarArea(ByVal codigo_lin As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("INV_ListarAreasOcdexLineaUsat", codigo_lin)
        Me.ddlArea.Items.Clear()
        Me.ddlArea.Items.Add(New ListItem("[-- SELECCIONE --]", "0"))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlArea.Items.Add(New ListItem(tb.Rows(i).Item("descripcion_ocde"), tb.Rows(i).Item("codigo_ocde")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ListarLineaOCDE(ByVal codigo_lin As Integer, ByVal tipo As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("INV_listaAreasConocimientoOCDE", codigo_lin, tipo)
        If tipo = "SA" Then
            Me.ddlSubArea.Items.Clear()
            Me.ddlSubArea.Items.Add(New ListItem("[-- SELECCIONE --]", "0"))
            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Me.ddlSubArea.Items.Add(New ListItem(tb.Rows(i).Item("descripcion"), tb.Rows(i).Item("codigo")))
                Next
            End If
        End If
        If tipo = "DI" Then
            Me.ddlDisciplina.Items.Clear()
            Me.ddlDisciplina.Items.Add(New ListItem("[-- SELECCIONE --]", "0"))
            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Me.ddlDisciplina.Items.Add(New ListItem(tb.Rows(i).Item("descripcion"), tb.Rows(i).Item("codigo")))
                Next
            End If
        End If
        obj.CerrarConexion()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If IsPostBack = False Then
                ListarSemestre()
                If Me.ddlSemestre.SelectedValue <> "" Then
                    ListarCarreras()
                End If
                ListarLineasUSAT()
                ListarTiposTrabajo()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub listarAlumnos(ByVal codigo_cac As String, ByVal codigo_cpf As String, ByVal codigo_cup As Integer, ByVal codigo_per As Integer, ByVal ctf As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("INV_AlumnosMatriculadosTrabajoInvestigacion", codigo_cac, codigo_cpf, codigo_cup, codigo_per, ctf)
        If tb.Rows.Count > 0 Then
            Me.gvAlumnos.DataSource = tb
        Else
            Me.gvAlumnos.DataSource = ""
        End If
        Me.gvAlumnos.DataBind()
        obj.CerrarConexion()
        
    End Sub

    Private Sub CargarDatosTrabajoInvestigacion(ByVal codigo_tba As Integer, ByVal codigo_cac As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("INV_CargarDatosTrabajoInvestigacion", codigo_tba, codigo_cac)
        Me.hdcod.Value = codigo_tba
        Me.hdcac.Value = codigo_cac
        If dt.Rows.Count > 0 Then
            Dim str As String = ""
            For i As Integer = 0 To dt.Rows.Count - 1
                str += "<div class='form-group'>"
                str += "<label class='col-sm-3 col-md-3 control-label'>Código universitario</label>"
                str += "<div class='col-sm-3 col-md-2'>"
                str += "<input type='text' class='form-control' value='" + dt.Rows(i).Item("codigoUniver_Alu").ToString + "' readonly='readonly' >"
                str += "</div>"
                str += "<label class='col-sm-1 col-md-1 control-label'>Alumno</label>"
                str += "<div class='col-sm-5 col-md-6'>"
                str += "<input type='text' class='form-control' value='" + dt.Rows(i).Item("responsable").ToString + "' readonly='readonly' >"
                str += "</div>"
                str += "</div>"
            Next
            Me.alumnos.InnerHtml = str
            Me.txtTitulo.Text = dt.Rows(0).Item("titulo_tba").ToString
            Me.ddlTipoTrabajo.SelectedValue = dt.Rows(0).Item("codigo_ttb").ToString
            Me.ddlLinea.SelectedValue = dt.Rows(0).Item("codigo_lin").ToString
            If dt.Rows(0).Item("codigo_lin").ToString <> 0 Then
                ListarArea(dt.Rows(0).Item("codigo_lin").ToString)
            End If
            If dt.Rows(0).Item("codigo_area").ToString <> 0 Then
                Me.ddlArea.SelectedValue = dt.Rows(0).Item("codigo_area").ToString
                ListarLineaOCDE(dt.Rows(0).Item("codigo_area").ToString, "SA")
            End If
            If dt.Rows(0).Item("codigo_sub").ToString <> 0 Then
                Me.ddlSubArea.SelectedValue = dt.Rows(0).Item("codigo_sub").ToString
                ListarLineaOCDE(dt.Rows(0).Item("codigo_sub").ToString, "DI")
            End If
            If dt.Rows(0).Item("codigo_dis").ToString <> 0 Then
                Me.ddlDisciplina.SelectedValue = dt.Rows(0).Item("codigo_dis").ToString
            End If
            
            If dt.Rows(0).Item("archivo").ToString <> "" Then
                Dim btn As New LinkButton
                btn.Text = "Descargar trabajo <i class='ion-android-attach'></i>"
                btn.CssClass = "btn btn-sm btn-warning btn-radius"
                btn.OnClientClick = "fnDescargar('" + dt.Rows(0).Item("archivo") + "');return false;"
                Me.divArchivoTrabajo.InnerHtml = ""
                Me.divArchivoTrabajo.Controls.Add(btn)
            Else
                Me.divArchivoTrabajo.InnerHtml = "<ul><li>"
                Me.divArchivoTrabajo.InnerHtml += "<span style='color: Red; font-weight: bold;'>No cuenta con archivo de trabajo</span>"
                Me.divArchivoTrabajo.InnerHtml += "</li></ul>"
            End If
            If dt.Rows(0).Item("rubrica").ToString <> "" Then
                Dim btn1 As New LinkButton
                btn1.Text = "Descargar rúbrica <i class='ion-android-attach'></i>"
                btn1.CssClass = "btn btn-sm btn-info btn-radius"
                btn1.OnClientClick = "fnDescargar('" + dt.Rows(0).Item("rubrica") + "');return false;"
                Me.divArchivoRubrica.InnerHtml = ""
                Me.divArchivoRubrica.Controls.Add(btn1)
                Me.btnEliminarRubrica.Visible = True
                Me.hdrubrica.Value = dt.Rows(0).Item("codigo_rubrica")
            Else
                Me.hdrubrica.Value = 0
                Me.btnEliminarRubrica.Visible = False
                Me.divArchivoRubrica.InnerHtml = "<ul><li>"
                Me.divArchivoRubrica.InnerHtml += "<span style='color: Red; font-weight: bold;'>No cuenta con archivo de rúbrica</span>"
                Me.divArchivoRubrica.InnerHtml += "</li></ul>"
            End If
            If dt.Rows(0).Item("acta").ToString <> "" Then
                Dim btn1 As New LinkButton
                btn1.Text = "Descargar acta <i class='ion-android-attach'></i>"
                btn1.CssClass = "btn btn-sm btn-success btn-radius"
                btn1.OnClientClick = "fnDescargar('" + dt.Rows(0).Item("acta") + "');return false;"
                Me.divArchivoActa.InnerHtml = ""
                Me.divArchivoActa.Controls.Add(btn1)
                Me.btnEliminarActa.Visible = True
                Me.hdacta.Value = dt.Rows(0).Item("codigo_acta")
            Else
                Me.hdacta.Value = 0
                Me.btnEliminarActa.Visible = False
                Me.divArchivoActa.InnerHtml = "<ul><li>"
                Me.divArchivoActa.InnerHtml += "<span style='color: Red; font-weight: bold;'>No cuenta con archivo de acta</span>"
                Me.divArchivoActa.InnerHtml += "</li></ul>"
            End If
        End If
        obj.CerrarConexion()
    End Sub

    Protected Sub ddlSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemestre.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Me.ddlCarrera.Items.Clear()
        Me.ddlCarrera.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
        Me.ddlAsignatura.Items.Clear()
        Me.ddlAsignatura.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
        If Me.ddlSemestre.SelectedValue <> "" Then
            ListarCarreras()
        End If
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading1", "fnLoading(false);LoadingCarrera();", True)
        Me.gvAlumnos.DataSource = Nothing
        Me.gvAlumnos.DataBind()
        Me.divLista.Visible = True
        Me.DivMantenimiento.Visible = False
    End Sub

    Protected Sub ddlCarrera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarrera.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Me.ddlAsignatura.Items.Clear()
        Me.ddlAsignatura.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
        If Me.ddlSemestre.SelectedValue <> "" And Me.ddlCarrera.SelectedValue <> "" Then
            ListarAsignaturas(Me.ddlSemestre.SelectedValue, Me.ddlCarrera.SelectedValue, Session("id_per"), Request("ctf"))
        End If
        Me.gvAlumnos.DataSource = Nothing
        Me.gvAlumnos.DataBind()
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading1", "fnLoading(false);LoadingAsignatura();", True)
        Me.divLista.Visible = True
        Me.DivMantenimiento.Visible = False
    End Sub


    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If Me.ddlSemestre.SelectedValue <> "" Then
            If Me.ddlCarrera.SelectedValue <> "" Then
                If Me.ddlAsignatura.SelectedValue <> "" Then
                    listarAlumnos(Me.ddlSemestre.SelectedValue, Me.ddlCarrera.SelectedValue, Me.ddlAsignatura.SelectedValue, Session("id_per"), Request("ctf"))
                    Me.divLista.Visible = True
                    Me.DivMantenimiento.Visible = False
                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading2", "fnLoading(false)", True)
                Else
                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione una asignatura')", True)
                End If
            Else
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione una carrera')", True)
            End If
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione un semestre')", True)

        End If
        Me.lblmensaje.Attributes.Remove("class")
        Me.lblmensaje.InnerText = ""
    End Sub

    Protected Sub gvAlumnos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAlumnos.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If e.CommandName = "Ver" Then
            Me.hdcod.Value = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_tba").ToString()
            Me.hdcac.Value = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_cac").ToString
            Me.hdalu.Value = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_alu").ToString
            Me.divLista.Visible = False
            Me.DivMantenimiento.Visible = True
            Limpiar()
            If Me.hdcod.Value > 0 Then
                CargarDatosTrabajoInvestigacion(Me.hdcod.Value, Me.hdcac.Value)
            Else
                Dim str As String = ""
                str += "<div class='form-group'>"
                str += "<label class='col-sm-3 col-md-3 control-label'>Código universitario</label>"
                str += "<div class='col-sm-3 col-md-2'>"
                str += "<input type='text' class='form-control' value='" + Me.gvAlumnos.Rows(e.CommandArgument).Cells(1).Text + "' readonly='readonly' >"
                str += "</div>"
                str += "<label class='col-sm-1 col-md-1 control-label'>Alumno</label>"
                str += "<div class='col-sm-5 col-md-6'>"
                str += "<input type='text' class='form-control' value='" + Me.gvAlumnos.Rows(e.CommandArgument).Cells(2).Text + "' readonly='readonly' >"
                str += "</div>"
                str += "</div>"
                Me.Alumnos.InnerHtml = str
                Me.divArchivoTrabajo.InnerHtml = "<ul><li>"
                Me.divArchivoTrabajo.InnerHtml += "<span style='color: Red; font-weight: bold;'>No cuenta con archivo de trabajo</span>"
                Me.divArchivoTrabajo.InnerHtml += "</li></ul>"
                Me.divArchivoRubrica.InnerHtml = "<ul><li>"
                Me.divArchivoRubrica.InnerHtml += "<span style='color: Red; font-weight: bold;'>No cuenta con archivo de rúbrica</span>"
                Me.divArchivoRubrica.InnerHtml += "</li></ul>"
                Me.btnEliminarRubrica.Visible = False
                Me.divArchivoActa.InnerHtml = "<ul><li>"
                Me.divArchivoActa.InnerHtml += "<span style='color: Red; font-weight: bold;'>No cuenta con archivo de acta</span>"
                Me.divArchivoActa.InnerHtml += "</li></ul>"
                Me.btnEliminarActa.Visible = False
                Me.hdcac.Value = Me.ddlSemestre.SelectedValue
            End If
             End If
        Me.lblmensaje.Attributes.Remove("class")
        Me.lblmensaje.InnerText = ""
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Dim dt As New Data.DataTable
        dt = GuardarTrabajo(Me.hdcod.Value, Me.txtTitulo.Text, Me.ddlTipoTrabajo.SelectedValue, Me.ddlLinea.SelectedValue, Me.ddlDisciplina.SelectedValue, Me.hdalu.Value, Me.hdcac.Value, Session("id_per"))
        If dt.Rows(0).Item("Respuesta").ToString = "1" Then
            Me.hdcod.Value = dt.Rows(0).Item("cod").ToString
            Me.lblmensaje.Attributes.Add("class", "alert alert-success")
            Me.lblmensaje.InnerText = dt.Rows(0).Item("Mensaje").ToString
            Dim bandera As Integer = 1
            If Me.archivo.HasFile = True Then
                If validarArchivo() = True Then
                    If SubirArchivo(29, Me.hdcod.Value, Me.archivo.PostedFile, 1, 0, Session("id_per")) Then
                    Else
                        Me.lblmensaje.InnerText = dt.Rows(0).Item("Mensaje").ToString + " * No se pudo subir archivo de trabajo"
                        Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
                        bandera = 0
                    End If
                Else
                    bandera = 0

                End If
            End If

            If Me.archivorubrica.HasFile = True Then
                If validarRubrica() = True Then
                    If SubirArchivo(29, Me.hdcod.Value, Me.archivorubrica.PostedFile, 3, Me.hdcac.Value, Session("id_per")) Then
                    Else
                        Me.lblmensaje.InnerText = dt.Rows(0).Item("Mensaje").ToString + " * No se pudo subir archivo de rúbrica"
                        Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
                        bandera = 0
                    End If
                Else
                    bandera = 0
                End If
            End If
            If bandera = 1 Then
                listarAlumnos(Me.ddlSemestre.SelectedValue, Me.ddlCarrera.SelectedValue, Me.ddlAsignatura.SelectedValue, Session("id_per"), Request("ctf"))
                Me.divLista.Visible = True
                Me.DivMantenimiento.Visible = False
            Else
                CargarDatosTrabajoInvestigacion(Me.hdcod.Value, Me.hdcac.Value)
            End If
            
        Else
            Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
            Me.lblMensajeRegistro.InnerText = dt.Rows(0).Item("Respuesta").ToString
        End If
    End Sub


    Function validarArchivo() As Boolean
        Me.lblMensajeRegistro.InnerText = ""
        Me.lblMensajeRegistro.Attributes.Remove("class")
        If Me.archivo.HasFile = True Then
            If Me.archivo.PostedFile.ContentLength > 20971520 Then
                Me.lblMensajeRegistro.InnerText = "Solo puede adjuntar archivos de un tamaño máximo de 20MB"
                Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
                Return False
            End If

            Dim Extensiones As String() = {".pdf", ".rar", ".zip"}
            Dim validar As Integer = -1
            validar = Array.IndexOf(Extensiones, System.IO.Path.GetExtension(archivo.FileName))
            If validar = -1 Then
                Me.lblMensajeRegistro.InnerText = "Solo puede adjuntar archivos de trabajo en formato .pdf,.zip,.rar"
                Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
                Return False
            End If
        End If
        Return True
    End Function

    Function validarRubrica() As Boolean
        Me.lblMensajeRegistro.InnerText = ""
        Me.lblMensajeRegistro.Attributes.Remove("class")
        If Me.archivorubrica.HasFile = True Then
            If Me.archivorubrica.PostedFile.ContentLength > 20971520 Then
                Me.lblMensajeRegistro.InnerText = "Solo puede adjuntar archivos de un tamaño máximo de 20MB"
                Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
                Return False
            End If

            Dim Extensiones As String() = {".pdf", ".rar", ".zip"}
            Dim validar As Integer = -1
            validar = Array.IndexOf(Extensiones, System.IO.Path.GetExtension(archivorubrica.FileName))
            If validar = -1 Then
                Me.lblMensajeRegistro.InnerText = "Solo puede adjuntar archivos de rúbrica en formato .pdf,.zip,.rar"
                Me.lblMensajeRegistro.Attributes.Add("class", "alert alert-danger")
                Return False
            End If
        End If
        Return True
    End Function

    Function SubirArchivo(ByVal idtabla As Integer, ByVal codigo As String, ByVal ArchivoSubir As HttpPostedFile, ByVal nro_operacion As Integer, ByVal codigo_Cac As Integer, ByVal usuario_reg As Integer) As Boolean
        Try
            'Dim dt As New Data.DataTable
            'Dim obj As New ClsGestionInvestigacion

            Dim post As HttpPostedFile = ArchivoSubir
            Dim codigo_con As String = codigo
            Dim nrooperacion As String = nro_operacion 'NUMERO DE OPERACIÓN 11 PARA ARCHIVOS DE ASESORIAS
            Dim Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim Usuario As String = Session("perlogin").ToString
            Dim Input(post.ContentLength) As Byte
            ' Dim b As New BinaryReader(post.InputStream)
            '  Dim by() As Byte = b.ReadByte(post.ContentLength)

            Dim b As New BinaryReader(post.InputStream)
            Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)

            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)
            '  Dim list As New List(Of Dictionary(Of String, String))()
            list.Add("Fecha", Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
            list.Add("Nombre", System.IO.Path.GetFileName(post.FileName))
            list.Add("TransaccionId", codigo_con)
            list.Add("TablaId", idtabla)
            list.Add("NroOperacion", nrooperacion)
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Session("perlogin").ToString)
            'Me.TextBox1.Text = envelope
            If result.Contains("Registro procesado correctamente") Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                Dim dt As New Data.DataTable
                If nro_operacion = 1 Then
                    dt = obj.TraerDataTable("TIB_ActualizarIDArchivoTrabajo", idtabla, codigo, nrooperacion, usuario_reg)
                End If
                If nro_operacion = 3 Then
                    dt = obj.TraerDataTable("INV_ActualizarArchivoTrabajoInvestigacionColaborador", idtabla, codigo, nrooperacion, codigo_Cac, "", usuario_reg)
                End If
                obj.CerrarConexion()
                Return True
            Else
                Return False
            End If

            'Me.lblMensaje.InnerText = envelope
            'Response.Write(result)
        Catch ex As Exception
            'Dim Data1 As New Dictionary(Of String, Object)()
            'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            'Dim JSONresult As String = ""
            'Dim list As New List(Of Dictionary(Of String, Object))()
            'Data1.Add("rpta", "1 - SUBIR ARCHIVO")
            'Data1.Add("msje", ex.Message)
            'list.Add(Data1)
            'JSONresult = serializer.Serialize(list)
            'Me.txtbusqueda.Text = JSONresult
            'Me.TextBox1.Text = JSONresult
            'ClientScript.RegisterClientScriptBlock(Me.GetType(), "alerta", "<script>alert('" + JSONresult + "')</script>")
            'Response.Write(JSONresult)
            'Me.lblmensaje.InnerHtml = Me.lblmensaje.InnerHtml + "<div class='row' style='color:red'>" + ex.Message + "</div>"
            Return False
        End Try
    End Function

    Private Function GuardarActa(ByVal codigo_Tba As Integer, ByVal codigo_cac As Integer, ByVal condicion As String, ByVal usuario As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("INV_RegistrarEvaluacionTrabajoBachiller", codigo_Tba, codigo_cac, condicion, usuario)
        obj.CerrarConexion()
        Return dt
    End Function

    Private Function ActualizarCodigoActa(ByVal codigo_eva As Integer, ByVal codigo_dot As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("INV_ActualizarCodigoActaTrabajo", codigo_eva, codigo_dot)
        obj.CerrarConexion()
        Return dt
    End Function

    Protected Sub gvAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAlumnos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim codigo_Tba As Integer = Me.gvAlumnos.DataKeys(e.Row.RowIndex).Values("codigo_tba").ToString
            If codigo_Tba > 0 Then
                Dim boton As LinkButton = DirectCast(e.Row.Cells(3).FindControl("btnVer"), LinkButton)
                boton.CssClass = "btn btn-sm btn-warning btn-radius"
                boton.ToolTip = "Editar"
            End If
        End If
    End Sub

    Protected Sub ddlAsignatura_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAsignatura.SelectedIndexChanged
        Me.gvAlumnos.DataSource = Nothing
        Me.gvAlumnos.DataBind()
        Me.divLista.Visible = True
        Me.DivMantenimiento.Visible = False
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading3", "fnLoading(false);LoadingAsignatura();", True)
    End Sub

    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.divLista.Visible = True
        Me.DivMantenimiento.Visible = False
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading4", "fnLoading(false);LoadingAsignatura();", True)
    End Sub

    Protected Sub ddlLinea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLinea.SelectedIndexChanged
        Me.ddlArea.Items.Clear()
        Me.ddlArea.Items.Add(New ListItem("[-- SELECCIONE --]", 0))
        Me.ddlSubArea.Items.Clear()
        Me.ddlSubArea.Items.Add(New ListItem("[-- SELECCIONE --]", 0))
        Me.ddlDisciplina.Items.Clear()
        Me.ddlDisciplina.Items.Add(New ListItem("[-- SELECCIONE --]", 0))
        If Me.ddlLinea.SelectedValue <> 0 Then
            ListarArea(Me.ddlLinea.SelectedValue)
        End If
    End Sub

    Protected Sub ddlArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlArea.SelectedIndexChanged
        Me.ddlSubArea.Items.Clear()
        Me.ddlSubArea.Items.Add(New ListItem("[-- SELECCIONE --]", 0))
        Me.ddlDisciplina.Items.Clear()
        Me.ddlDisciplina.Items.Add(New ListItem("[-- SELECCIONE --]", 0))
        If Me.ddlArea.SelectedValue <> 0 Then
            ListarLineaOCDE(Me.ddlArea.SelectedValue, "SA")
        End If
    End Sub

    Protected Sub ddlSubArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubArea.SelectedIndexChanged
        ddlDisciplina.Items.Clear()
        Me.ddlDisciplina.Items.Add(New ListItem("[-- SELECCIONE --]", 0))
        If Me.ddlSubArea.SelectedValue <> 0 Then
            ListarLineaOCDE(Me.ddlSubArea.SelectedValue, "DI")
        End If
    End Sub

    Private Sub Limpiar()
        Me.txtTitulo.Text = ""
        Me.ddlTipoTrabajo.SelectedValue = 0
        Me.ddlLinea.SelectedValue = 0
        Me.ddlArea.Items.Clear()
        Me.ddlArea.Items.Add(New ListItem("[-- SELECCIONE --]", 0))
        Me.ddlSubArea.Items.Clear()
        Me.ddlSubArea.Items.Add(New ListItem("[-- SELECCIONE --]", 0))
        Me.ddlDisciplina.Items.Clear()
        Me.ddlDisciplina.Items.Add(New ListItem("[-- SELECCIONE --]", 0))
    End Sub

    Private Function GuardarTrabajo(ByVal codigo_tba As Integer, ByVal titulo As String, ByVal codigo_tip As Integer, ByVal codigo_lin As Integer, ByVal codigo_dis As Integer, ByVal codigo_alu As Integer, ByVal codigo_cac As Integer, ByVal usuario As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("TIB_ActualizarTrabajoBachiller", codigo_tba, titulo, codigo_tip, codigo_lin, codigo_dis, codigo_alu, codigo_cac, usuario)
        obj.CerrarConexion()
        Return dt
    End Function



    Protected Sub btnEliminarRubrica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminarRubrica.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If Me.hdrubrica.Value <> 0 Then
            Dim dt As New Data.DataTable
            dt = EliminarEvaluacion(Me.hdrubrica.Value, Session("id_per"))
            If dt.Rows(0).Item("Respuesta") = 1 Then
                CargarDatosTrabajoInvestigacion(Me.hdcod.Value, Me.hdcac.Value)
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje").ToString + "')", True)
            Else
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje").ToString + "')", True)
            End If
        End If
    End Sub

    Private Function EliminarEvaluacion(ByVal codigo_etb As Integer, ByVal usuario As Integer) As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("TIB_EliminarRevisionTrabajo", codigo_etb, usuario)
        obj.CerrarConexion()
        Return dt
    End Function

    Protected Sub btnEliminarActa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminarActa.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If Me.hdacta.Value <> 0 Then
            Dim dt As New Data.DataTable
            dt = EliminarEvaluacion(Me.hdacta.Value, Session("id_per"))
            If dt.Rows(0).Item("Respuesta") = 1 Then
                CargarDatosTrabajoInvestigacion(Me.hdcod.Value, Me.hdcac.Value)
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje").ToString + "')", True)
            Else
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje").ToString + "')", True)
            End If
        End If
    End Sub
End Class


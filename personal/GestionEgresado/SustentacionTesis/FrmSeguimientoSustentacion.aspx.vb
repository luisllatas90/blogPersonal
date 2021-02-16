Imports System.Data
Imports System.IO
Imports System.Collections.Generic

Partial Class FrmSeguimientoSustentacion
    Inherits System.Web.UI.Page
  
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If Me.txtBusqueda.Text.Length > 3 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim dtdatos As New Data.DataTable
            dtdatos = obj.TraerDataTable("SUST_BuscarAlumnoSeguimiento", Session("id_per"), Request("ctf"), Me.txtBusqueda.Text.Trim, 0)
            obj.CerrarConexion()
            Me.gvAlumnos.DataSource = dtdatos
            Me.gvAlumnos.DataBind()
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "err", "fnMensaje('error','Debe ingresar al menos 4 caracteres para buscar')", True)

        End If
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "load", "fnLoading(false)", True)

    End Sub
    Private Sub CargarDatos(ByVal codigo_alu As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_DatosSeguimientoAlumno", codigo_alu)
        obj.CerrarConexion()
        Me.lbladvertencia.Text = ""
        If dt.Rows.Count > 0 Then
            Me.hdtes.Value = dt.Rows(0).Item("codigo_tes")
            Me.txttitulo.Text = dt.Rows(0).Item("titulo_tes")
            For i As Integer = 0 To dt.Rows.Count - 1
                Me.txtautores.Text = Me.txtautores.Text + dt.Rows(i).Item("autor") + vbCrLf
            Next
         
            Me.txtfechaproyecto.Text = dt.Rows(0).Item("fechaproyecto").ToString
            Me.txttiempo.Text = dt.Rows(0).Item("tiempo").ToString

           

            If dt.Rows(0).Item("asesor").ToString <> "" And dt.Rows(0).Item("estadoasesor").ToString <> "" Then
                Me.txtasesor.Text = dt.Rows(0).Item("asesor").ToString & dt.Rows(0).Item("estadoasesor").ToString
            Else
                Me.txtasesor.Text = dt.Rows(0).Item("asesor").ToString
            End If

            If dt.Rows(0).Item("vigencia") = "NO" Then
                Me.lbladvertencia.Text = "(*) EL EGRESADO DEBERÁ SOLICITAR TRÁMITE DE EXTENSIÓN DE VIGENCIA"
            End If
            Me.txtvigente.Text = dt.Rows(0).Item("vigencia")

            If dt.Rows(0).Item("asesor").ToString = "" Or dt.Rows(0).Item("estadoasesor").ToString <> "" Then
                Me.lbladvertencia.Text = "(*) NO CUENTA CON ASESOR DE TESIS ACTUALIZADO, EL EGRESADO DEBE SOLICITAR TRÁMITE DE ACTUALIZACIÓN DE DATOS DE TESIS"
            End If

            If txtfechaproyecto.Text = "" Then
                lbladvertencia.Text = "(*) EGRESADO NO CUENTA CON FECHA DE SUSTENTACIÓN DE PROYECTO DE TESIS"
            End If

            Me.txtfechaenviotesis.Text = dt.Rows(0).Item("fechaenvio")
            Me.txttiempoasesor.Text = dt.Rows(0).Item("tiempoasesor")
            Me.txttieneobservacionasesor.Text = dt.Rows(0).Item("tieneobservacionesasesor")
            Me.txtfechaultimaobservacionasesor.Text = dt.Rows(0).Item("ultimaobservacionasesor")
            Me.txttieneconformidadasesor.Text = dt.Rows(0).Item("conformidadasesor")
            Me.txtfechaconformidadasesor.Text = dt.Rows(0).Item("fechaconformidad")

        Else
            Me.lbladvertencia.Text = "(*) NO CUENTA CON REGISTRO DE TESIS EN SGI, EL EGRESADO DEBE SOLICITAR TRÁMITE DE ACTUALIZACIÓN DE DATOS DE TESIS"

        End If
    End Sub

    Protected Sub gvAlumnos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAlumnos.RowCommand

        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If (e.CommandName = "Seleccionar") Then
                Dim codigo_alu As Integer = 0
                codigo_alu = Me.gvAlumnos.DataKeys(e.CommandArgument).Values("codigo_alu")
                Limpiar()
                CargarDatos(codigo_alu)
                CargarTramitesprevios(codigo_alu)
                CargarTramitessustentacion(codigo_alu)
                Me.Lista.Visible = False
                Me.Datos.Visible = True
                Me.DetalleTramitePrevio.Visible = False
            End If

        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + ex.Message.ToString + "')", True)
        End Try
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "load2", "fnLoading(false)", True)

    End Sub

    Private Sub CargarTramitesprevios(ByVal codigo_alu As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_TramitesPreviosSustentacion", codigo_alu)
        obj.CerrarConexion()
        If dt.Rows.Count = 0 Then
            Me.gvTramitesPrevios.DataSource = Nothing
        Else
            Me.gvTramitesPrevios.DataSource = dt
        End If
        Me.gvTramitesPrevios.DataBind()
    End Sub

    Private Sub CargarTramitessustentacion(ByVal codigo_alu As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_TramitesSustentacion", codigo_alu)
        obj.CerrarConexion()
        If dt.Rows.Count = 0 Then
            Me.gvTramitesSustentacion.DataSource = Nothing
        Else
            Me.gvTramitesSustentacion.DataSource = dt
        End If
        Me.gvTramitesSustentacion.DataBind()
    End Sub

    Private Sub Limpiar()
        Me.txttitulo.Text = ""
        Me.txtautores.Text = ""
        Me.txtfechaproyecto.Text = ""
        Me.txttiempo.Text = ""
        Me.txtvigente.Text = ""
        Me.gvTramitesPrevios.DataSource = Nothing
        Me.gvTramitesPrevios.DataBind()
        Me.txtasesor.Text = ""
        Me.txtfechaenviotesis.Text = ""
        Me.txttiempoasesor.Text = ""
        Me.txttieneobservacionasesor.Text = ""
        Me.txtfechaultimaobservacionasesor.Text = ""
        Me.txttieneconformidadasesor.Text = ""
        Me.txtfechaconformidadasesor.Text = ""
        Me.gvTramitesSustentacion.DataSource = Nothing
        Me.gvTramitesSustentacion.DataBind()
        Me.txtcodigotramite.Text = ""
        Me.txtfechatramite.Text = ""
        Me.txtestadotramite.Text = ""
        Me.DetalleTramitesustentacion.InnerHtml = ""
    End Sub

    Protected Sub gvTramitesSustentacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTramitesSustentacion.RowCommand
        Try
            If (e.CommandName = "Detalle") Then
                Dim codigo_dta As Integer = 0
                codigo_dta = Me.gvTramitesSustentacion.DataKeys(e.CommandArgument).Values("codigo_dta")
                Me.txtcodigotramite.Text = Me.gvTramitesSustentacion.Rows(e.CommandArgument).Cells(1).Text
                Me.txtestadotramite.Text = Me.gvTramitesSustentacion.Rows(e.CommandArgument).Cells(3).Text
                Me.txtfechatramite.Text = Me.gvTramitesSustentacion.Rows(e.CommandArgument).Cells(4).Text
                CargarDetalleTramitessustentacion(codigo_dta)
            End If
        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert3", "fnMensaje('error','" + ex.Message.ToString + "')", True)
        End Try
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "load4", "fnLoading(false)", True)

    End Sub

    Private Sub CargarDetalleTramitessustentacion(ByVal codigo_dta As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_DetalleFlujoSustentacion", codigo_dta)
        obj.CerrarConexion()
        Dim str As String = ""
        If dt.Rows.Count > 0 Then
            'str = str + "<div class='row'><div class='form-group'>"
            Dim texto As String = ""
            For i As Integer = 0 To dt.Rows.Count - 1
                'str = str + "<div class='row'><div class='form-group'>"
                texto = ""
                If i = 1 Then
                    texto = "REVISIÓN DE"
                End If
                If i = 2 Then
                    texto = "EMISIÓN DE RESOLUCIÓN DE CONSEJO DE FACULTAD -"
                End If
                If i = 3 Then
                    texto = "ACTO DE SUSTENTACIÓN -"
                End If

                str = str + "</br></br><h4 class='label label-primary' style='font-size: 12px;'>"
                str = str + (i + 1).ToString + ".- " + texto + " " + dt.Rows(i).Item("descripcion_Tfu") + "</h4>"
                'If dt.Rows(i).Item("orden_ftr") = 1 Then
                If dt.Rows(i).Item("estadoaprobacion").ToString = "A" Then
                    str = str + "&nbsp;<h4 class='label label-success' style='font-size: 12px;'>APROBADO: " + dt.Rows(i).Item("fechaModestado_dft") + "</h4>"
                   
                Else
                    str = str + "&nbsp;<h4 class='label label-danger' style='font-size: 12px;'>PENDIENTE</h4>"

                    'End If
                End If
                If i > 0 Then
                    If dt.Rows(i).Item("orden_ftr") = 2 And dt.Rows(i - 1).Item("estadoaprobacion").ToString = "A" Then
                        str += "</br>"
                        str += "</br>"
                        str += CargarJurados()
                    End If
                    If dt.Rows(i).Item("orden_ftr") = 3 And dt.Rows(i - 1).Item("estadoaprobacion").ToString = "A" Then
                        str += "</br>"
                        str += "</br>"
                        str += Cargarprogramacion()
                    End If
                    If dt.Rows(i).Item("orden_ftr") = 4 And dt.Rows(i - 1).Item("estadoaprobacion").ToString = "A" Then
                        str += "</br>"
                        str += "</br>"
                        str += CargarConformidadJurados("J")
                    End If
                    If dt.Rows(i).Item("orden_ftr") = 5 And dt.Rows(i - 1).Item("estadoaprobacion").ToString = "A" Then
                        str += "</br>"
                        str += "</br>"
                        str += CargarConformidadJurados("B")
                        str += CargarAutorizacionPublicacion()
                    End If

                End If
                'str = str + "</div></div>"
            Next
            'str = str + "</div></div>"

        End If
        Me.DetalleTramitesustentacion.InnerHtml = str
    End Sub

    Private Function CargarJurados() As String
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ListaJuradosSeguimiento", Me.hdtes.Value)
        obj.CerrarConexion()
        Dim str As String = ""
        If dt.Rows.Count > 0 Then
            str = str + "<table class='table table-condensed' style='border:solid 1px #000000;' >"
            str = str + "<thead style='background-color:#D9534F'>"
            str = str + "<tr>"
            str = str + "<th width='15%' style='border:solid 1px #ffffff;'>ROL</th>"
            str = str + "<th width='35%' style='border:solid 1px #ffffff;'>REVISOR</th>"
            str = str + "<th width='10%' style='border:solid 1px #ffffff;'>ESTADO</th>"
            str = str + "<th width='10%' style='border:solid 1px #ffffff;'>DIAS PENDIENTES</th>"
            str = str + "<th width='10%' style='border:solid 1px #ffffff;'>ÚLTIMA OBSERVACIÓN</th>"
            str = str + "<th width='10%' style='border:solid 1px #ffffff;'>TIPO CONFORMIDAD</th>"
            str = str + "<th width='10%' style='border:solid 1px #ffffff;'>FECHA CONFORMIDAD</th>"
            str = str + "</tr>"
            str = str + "</thead>"

            For i As Integer = 0 To dt.Rows.Count - 1
                str = str + "<tr style='border:solid 1px #000000;'>"
                str = str + "<td style='border:solid 1px #000000;' >" + dt.Rows(i).Item("descripcion_tpi") + "</td>"
                str = str + "<td style='border:solid 1px #000000;' >" + dt.Rows(i).Item("revisor") + "</td>"
               str = str + "<td style='border:solid 1px #000000;'>" + dt.Rows(i).Item("estado") + "</td>"
                str = str + "<td style='text-align:center;border:solid 1px #000000;'>" + dt.Rows(i).Item("diaspendientes").ToString + "</td>"
                str = str + "<td style='border:solid 1px #000000;'>" + dt.Rows(i).Item("ultimaobservacion").ToString + "</td>"
                str = str + "<td style='border:solid 1px #000000;'>" + dt.Rows(i).Item("tipo_conformidad").ToString + "</td>"
                str = str + "<td style='border:solid 1px #000000;'>" + dt.Rows(i).Item("fecha_conformidad").ToString + "</td>"
                str = str + "</tr>"
            Next
            str = str + "</table>"

        End If
        Return str
    End Function


    Private Function Cargarprogramacion() As String
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ListaProgramacionSeguimiento", Me.hdtes.Value)
        obj.CerrarConexion()
        Dim str As String = ""
        If dt.Rows.Count > 0 Then


            str = str + "<table class='table table-condensed' style='border:solid 1px #000000;' >"
            str = str + "<thead style='background-color:#D9534F'>"
            str = str + "<tr>"
            str = str + "<th width='30%' style='border:solid 1px #ffffff;'>ROL</th>"
            str = str + "<th width='40%' style='border:solid 1px #ffffff;'>PERSONAL</th>"
            str = str + "<th width='10%' style='border:solid 1px #ffffff;'>ESTADO</th>"
            str = str + "<th width='20%' style='border:solid 1px #ffffff;'>FECHA CONFORMIDAD</th>"
            str = str + "</tr>"
            str = str + "</thead>"

            For i As Integer = 0 To dt.Rows.Count - 1
                str = str + "<tr style='border:solid 1px #000000;'>"
                str = str + "<td style='border:solid 1px #000000;' >" + dt.Rows(i).Item("cargo") + "</td>"
                str = str + "<td style='border:solid 1px #000000;'>" + dt.Rows(i).Item("personal").ToString + "</td>"
                str = str + "<td style='border:solid 1px #000000;'>" + dt.Rows(i).Item("estado") + "</td>"
                str = str + "<td style='border:solid 1px #000000;'>" + dt.Rows(i).Item("fecha_reg").ToString + "</td>"
                str = str + "</tr>"
            Next
            str = str + "</table>"


            str += "<div class='row'><div class='form-group'>"
            str += "<label class='col-sm-12 col-md-12 control-label text-danger'><strong>DATOS DE PROGRAMACIÓN DE SUSTENTACIÓN</strong></label>"
            str += "</div>"
            str += "</div>"

            str += "<div class='row'><div class='form-group'>"
            str += "<label class='col-sm-3 col-md-3 control-label'>Resolución</label>"
            str += "<div class='col-sm-4 col-md-4'>"
            str += "<input type='textbox' class='form-control' style='font-weight:bold;' readonly='readonly' value='" + dt.Rows(0).Item("serieCorrelativo_dot").ToString + "' />"
            str += "</div>"
            str += "</div>"
            str += "</div>"

            str += "<div class='row'><div class='form-group'>"
            str += "<label class='col-sm-3 col-md-3 control-label'>Fecha de programación</label>"
            str += "<div class='col-sm-3 col-md-3'>"
            str += "<input type='textbox' class='form-control'  readonly='readonly' style='font-weight:bold;' value='" + dt.Rows(0).Item("fecha_programacion") + "' />"
            str += "</div>"
            str += "<label class='col-sm-3 col-md-3 control-label'>Hora de programación</label>"
            str += "<div class='col-sm-3 col-md-3'>"
            str += "<input type='textbox' class='form-control'  readonly='readonly' style='font-weight:bold;' value='" + dt.Rows(0).Item("hora_programacion") + "' />"
            str += "</div>"
            str += "</div>"
            str += "</div>"

            str += "<div class='row'><div class='form-group'>"
            str += "<label class='col-sm-3 col-md-3 control-label'>Ambiente</label>"
            str += "<div class='col-sm-8 col-md-8'>"
            str += "<input type='textbox' class='form-control' style='font-weight:bold;' readonly='readonly' value='" + dt.Rows(0).Item("ambiente") + "' />"
            str += "</div>"
            str += "</div>"
            str += "</div>"

        End If
        Return str
    End Function



    Private Function CargarConformidadJurados(ByVal tipo As String) As String
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ListaConformidadJuradoSustentacion", Me.hdtes.Value, tipo)
        obj.CerrarConexion()
        Dim str As String = ""
        If dt.Rows.Count > 0 Then

            str = str + "<table class='table table-condensed' style='border:solid 1px #000000;' >"
            str = str + "<thead style='background-color:#D9534F'>"
            str = str + "<tr>"
            str = str + "<th width='25%' style='border:solid 1px #ffffff;'>ROL</th>"
            str = str + "<th width='45%' style='border:solid 1px #ffffff;'>REVISOR</th>"
            str = str + "<th width='10%' style='border:solid 1px #ffffff;'>ESTADO</th>"
            str = str + "<th width='20%' style='border:solid 1px #ffffff;'>FECHA DE ULTIMA CALIFICACIÓN</th>"
            str = str + "</tr>"
            str = str + "</thead>"

            For i As Integer = 0 To dt.Rows.Count - 1
                str = str + "<tr style='border:solid 1px #000000;'>"
                If tipo = "J" Then
                    str = str + "<td style='border:solid 1px #000000;' >" + dt.Rows(i).Item("descripcion_tpi") + "</td>"
                    str = str + "<td style='border:solid 1px #000000;'>" + dt.Rows(i).Item("revisor").ToString + "</td>"
                Else
                    str = str + "<td style='border:solid 1px #000000;' >BIBLIOTECA</td>"
                    str = str + "<td style='border:solid 1px #000000;'>" + dt.Rows(i).Item("descripcion_tpi") + "</td>"
                End If
                 str = str + "<td style='border:solid 1px #000000;'>" + dt.Rows(i).Item("estado") + "</td>"
                str = str + "<td style='border:solid 1px #000000;'>" + dt.Rows(i).Item("fechaconformidad").ToString + "</td>"
                str = str + "</tr>"
            Next
            str = str + "</table>"

            If tipo = "J" Then
                str += "<div class='row'><div class='form-group'>"
                str += "<label class='col-sm-12 col-md-12 control-label text-danger'><strong>ESTADO DE LEVANTAMIENTO DE OBSERVACIONES DE SUSTENTACIÓN</strong></label>"
                str += "</div>"
                str += "</div>"


                str += "<div class='row'><div class='form-group'>"
                str += "<label class='col-sm-3 col-md-3 control-label'>Envío de actualización</label>"
                str += "<div class='col-sm-2 col-md-2'>"
                str += "<input type='textbox' class='form-control'  readonly='readonly' style='font-weight:bold;' value='" + dt.Rows(0).Item("enviopostsustentacion") + "' />"
                str += "</div>"
                str += "<label class='col-sm-3 col-md-3 control-label'>Fecha de envío</label>"
                str += "<div class='col-sm-2 col-md-2'>"
                str += "<input type='textbox' class='form-control'  readonly='readonly' style='font-weight:bold;' value='" + dt.Rows(0).Item("fechaenvio") + "' />"
                str += "</div>"
                str += "</div>"
                str += "</div>"
            End If

        End If
        Return str
    End Function


    Private Function CargarAutorizacionPublicacion() As String
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("SUST_ListaAutorizacionPublicacionSeguimiento", Me.hdtes.Value)
        obj.CerrarConexion()
        Dim str As String = ""
        If dt.Rows.Count > 0 Then
            str = str + "<table class='table table-condensed' style='border:solid 1px #000000;' >"
            str = str + "<thead style='background-color:#D9534F'>"
            str = str + "<tr>"
            str = str + "<th width='40%' style='border:solid 1px #ffffff;'>AUTOR</th>"
            str = str + "<th width='20%' style='border:solid 1px #ffffff;'>FECHA DE REGISTRO</th>"
            str = str + "<th width='20%' style='border:solid 1px #ffffff;'>TIPO DE AUTORIZACIÓN</th>"
            str = str + "<th width='20%' style='border:solid 1px #ffffff;'>ESTADO</th>"
            str = str + "</tr>"
            str = str + "</thead>"

            For i As Integer = 0 To dt.Rows.Count - 1
                str = str + "<tr style='border:solid 1px #000000;'>"
                str = str + "<td style='border:solid 1px #000000;' >" + dt.Rows(i).Item("estudiante") + "</td>"
                str = str + "<td style='border:solid 1px #000000;'>" + dt.Rows(i).Item("fecha_reg").ToString + "</td>"
                str = str + "<td style='border:solid 1px #000000;'>" + dt.Rows(i).Item("tipoautorizacion_dap") + "</td>"
                str = str + "<td style='text-align:center;border:solid 1px #000000;'>" + dt.Rows(i).Item("estado").ToString + "</td>"
                str = str + "</tr>"
            Next
            str = str + "</table>"

        End If
        Return str
    End Function

    Protected Sub btnatras_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnatras.Click
        Me.Lista.Visible = True
        Me.Datos.Visible = False
    End Sub

    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If IsPostBack = False Then
            Me.Datos.Visible = False
            Me.DetalleTramitePrevio.Visible = False
        End If
    End Sub


    Private Sub fnInformacionTramite(ByVal codigo_dta As Integer)
        'Response.Write(codigo_dta)

        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim str As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_informaciontramite", "2", codigo_dta)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then

                Me.lblInfEstNumero.Text = dt.Rows(0).Item("numero").ToString()
                Me.lblInfEstEscuela.Text = dt.Rows(0).Item("escuela").ToString()
                Me.lblInfEstAlumno.Text = dt.Rows(0).Item("estudiante").ToString()
                Me.lblInfEstEmail.Text = dt.Rows(0).Item("email").ToString()
                Me.lblInfEstTelefono.Text = dt.Rows(0).Item("telefonoMovil_Dal").ToString()
                Me.lblInfEstFechaNac.Text = dt.Rows(0).Item("fechaNacimiento_Alu").ToString()

                Me.lblInfEstDocIdent.Text = dt.Rows(0).Item("nroDocIdent_Alu").ToString()
                If dt.Rows(0).Item("estadoActual_Alu") = 1 Then
                    Me.lblInfEstEstado.Text = "Activo"
                Else
                    Me.lblInfEstEstado.Text = "Inactivo"
                End If

                If dt.Rows(0).Item("estadoDeuda_Alu").ToString() = "1" Then
                    Me.lblInfEstTieneDeuda.Text = "Si"
                Else
                    Me.lblInfEstTieneDeuda.Text = "No"
                End If

                If dt.Rows(0).Item("beneficio_Alu").ToString() = "1" Then
                    Me.lblInfEstBeneficio.Text = "Si"
                Else
                    Me.lblInfEstBeneficio.Text = "No"
                End If


                Me.lblInfEstRespPago.Text = dt.Rows(0).Item("PersonaApod_Dal").ToString()
                Me.lblInfEstDirRespPago.Text = dt.Rows(0).Item("direccionApod_Dal").ToString()
                Me.lblTramiteDescripcion.Text = dt.Rows(0).Item("observacion_trl").ToString()

            End If



        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            obj = Nothing
        End Try

    End Sub


    Private Sub MostrarInformacionEvaluadores(ByVal codigo_dta As Integer)
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_TramiteFlujo_Listar", "3", codigo_dta)
            obj.CerrarConexion()

            Me.gvFlujo.DataSource = dt
            Me.gvFlujo.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub MostrarInformacionAdicional(ByVal codigo_trl As Integer, ByVal codigo_dta As Integer)
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TRL_TramiteAdicionalInfo", "1", codigo_trl, codigo_dta)
            obj.CerrarConexion()

            Me.gDatosAdicional.DataSource = dt
            Me.gDatosAdicional.DataBind()

            If dt.Rows.Count > 0 Then
                Me.lblInfoAdicional.Visible = True
            Else
                Me.lblInfoAdicional.Visible = False
            End If

            'CalcularTotalesInformacionAdicional()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvTramitesPrevios_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTramitesPrevios.RowCommand
        If (e.CommandName = "Detalle") Then
            Dim codigo_trl As Integer = 0
            Dim codigo_dta As Integer = 0
            Dim fecha_pago As String = ""
            codigo_trl = Me.gvTramitesPrevios.DataKeys(e.CommandArgument).Values("codigo_Trl")
            codigo_dta = Me.gvTramitesPrevios.DataKeys(e.CommandArgument).Values("codigo_dta")
            fecha_pago = Me.gvTramitesPrevios.DataKeys(e.CommandArgument).Values("fecha_cin").ToString
            Me.lblTramite.Text = Me.gvTramitesPrevios.Rows(e.CommandArgument).Cells(2).Text
            Me.lblFechaTramite.Text = Me.gvTramitesPrevios.Rows(e.CommandArgument).Cells(3).Text
            Me.lblFechaPago.Text = fecha_pago
            fnInformacionTramite(codigo_dta)
            MostrarInformacionEvaluadores(codigo_dta)
            MostrarInformacionAdicional(codigo_trl, codigo_dta)
            Me.DetalleTramitePrevio.Visible = True
            Me.Datos.Visible = False
        End If
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "load5", "fnLoading(false)", True)

    End Sub

    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.DetalleTramitePrevio.Visible = False
        Me.Datos.Visible = True
    End Sub

    Protected Sub gDatosAdicional_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gDatosAdicional.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Cells(0).Font.Bold = True
            e.Row.Cells(0).BackColor = Drawing.Color.AliceBlue

            e.Row.Cells(1).Font.Size = 10
            e.Row.Cells(1).BackColor = Drawing.Color.Linen
            e.Row.Cells(2).Font.Size = 10
            e.Row.Cells(2).BackColor = Drawing.Color.Linen
            If e.Row.Cells(0).Text = "ARCHIVO" Then
                Dim myLink As HyperLink = New HyperLink()
                myLink.NavigateUrl = "javascript:void(0)"
                myLink.Text = "Descargar"
                myLink.CssClass = "btn btn-sm btn-primary btn-radius"
                myLink.Attributes.Add("onclick", "DescargarArchivo('" & e.Row.Cells(1).Text & "')")


                e.Row.Cells(1).Controls.Add(myLink)
            End If

        End If
    End Sub
End Class


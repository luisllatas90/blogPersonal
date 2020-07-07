﻿
Partial Class lstinscritoseventocargo
    Inherits System.Web.UI.Page
    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Dim pagina As String
        Me.ValidarSegunModulo(pagina)
        If pagina <> "" Then
            Response.Redirect(pagina & "&accion=A")
        End If
    End Sub
    Protected Sub grwListaPersonas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwListaPersonas.RowDataBound
        '22/12/2014 treyes Solicitud de Anulacion

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim cpa As String = ""
            fila = e.Row.DataItem

            e.Row.Cells(11).Text = "<a href='frmVerCargosAbonos.aspx?cco=" & Request.QueryString("cco") & "&pso=" & fila.Row("codigo_pso") & "&KeepThis=true&TB_iframe=true&height=400&width=700&modal=true' title='Cambiar estado' class='thickbox'>&nbsp;<img src='../../../images/previo.gif' border=0 /><a/>"
            Dim pagina As String
            Me.ValidarSegunModulo(pagina)
            If pagina <> "" Then
                e.Row.Cells(12).Text = "<a href='" & pagina & "&accion=M&pso=" & fila.Row("codigo_pso") & "&tcl=" & fila.Row("tcl") & "&cli=" & fila.Row("cli") & "&KeepThis=true&TB_iframe=true&height=460&width=700&modal=true' title='Cambiar estado' class='thickbox'>&nbsp;<img src='../../../images/editar.gif' border=0 /><a/>"
            End If


            If Request.QueryString("mod") = 10 Then
                e.Row.Cells(15).Text = "<a href='../frmExportaFichaPostulacionGO.aspx?pso=" & fila.Row("codigo_pso") & "&cli=" & fila.Row("cli") & "&KeepThis=true&TB_iframe=true&height=400&width=700&modal=false' title='Imprimir ficha de postulación' style='text-decoration:none;' >&nbsp;<img src='../../Images/ext/pdf.gif' border=0/><a/>"
            Else
                e.Row.Cells(15).Text = "<a href='../frmExportaFichaPostulacion.aspx?pso=" & fila.Row("codigo_pso") & "&cli=" & fila.Row("cli") & "&KeepThis=true&TB_iframe=true&height=400&width=700&modal=false' title='Imprimir ficha de postulación' style='text-decoration:none;' >&nbsp;<img src='../../Images/ext/pdf.gif' border=0/><a/>"
            End If

            e.Row.Cells(16).Text = "<a href='frmSolicitarAnulacion.aspx?id=" & Request.QueryString("id") & "&cco=" & Request.QueryString("cco") & "&pso=" & fila.Row("codigo_pso") & "&participante=" & fila.Row("participante") & "&KeepThis=true&TB_iframe=true&height=400&width=700&modal=true' title='Solicitar Anulación' class='thickbox'>&nbsp;<img src='../../Images/eliminar.gif' border=0 /><a/>" 'treyes
            'e.Row.Cells(17).Text = "<a target='_blank' href='FrmImprimirConvenio.aspx?tcl=" & e.Row.Cells(18).Text & "&cpa='" & e.Row.Cells(17).Text & ">Ver</a>"
            cpa = e.Row.Cells(17).Text
            If (e.Row.Cells(17).Text = "0" Or e.Row.Cells(18).Text = "0") Then
                'e.Row.Cells(17).Text = "<a target='_blank' href='https://intranet.usat.edu.pe/rptusat/?/PRIVADOS/PENSIONES/PEN_DeudasxCco&codigo_cco=" & Request.QueryString("cco") & "&codigo_pso=" & e.Row.Cells(19).Text & "'>Ver</a>"
                e.Row.Cells(17).Text = "<a target='_blank' href='http://serverdev/reportServer/?/PRIVADOS/PENSIONES/PEN_DeudasxCco&codigo_cco=" & Request.QueryString("cco") & "&codigo_pso=" & e.Row.Cells(19).Text & "'>Ver</a>"
            Else
                'e.Row.Cells(17).Text = "<a target='_blank' href='https://intranet.usat.edu.pe/rptusat/?/PRIVADOS/PENSIONES/PEN_ConvenioPago_v2&ctf=0&usuario=" & Me.HdID.Value & "&tcl=" & e.Row.Cells(18).Text & "&codigo_cpa=" & cpa & "'>Ver</a>"
                Dim codigo_cli As Integer = grwListaPersonas.DataKeys(e.Row.RowIndex).Values.Item("cli")
                e.Row.Cells(17).Text = "<a target='_blank' href='http://serverdev/reportServer/?/PRIVADOS/PENSIONES/PEN_ConvenioPago_v2&ctf=0&usuario=" & Me.HdID.Value & "&tcl=" & e.Row.Cells(18).Text & "&codigo=" & codigo_cli & "'>Ver</a>"
            End If


            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
        e.Row.Cells(18).Visible = False
        e.Row.Cells(19).Visible = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                Me.HdID.Value = Request.QueryString("id")
                Me.ddlEstado.SelectedValue = "A" ' Hcano : 02/10/17 SE COLOCA FILTRO ACTIVO POR DEFECTO
                CargarInscritosConCargo()
                ConsultarDatosEvento()
                'Me.cmdNuevoJuridica.Visible = Request.QueryString("mod") = 0
                'Me.cmdReporte.OnClientClick = "AbrirPopUp('rpteinscritoseventocargo.aspx?cco=" & Request.QueryString("cco") & "&estado=" & Me.ddlEstado.SelectedValue & "','600','800','yes','yes','yes','yes')"
                If Request.QueryString("ctf") = 1 Then
                    Me.cmdNuevoPersonaSinCargo.Visible = True
                End If
                '---------- SOLICITUD FTUESTA 13.11.14 -------------
                'Inicio HCano 06-02-18 : Se Permite Registro a 143 : COORDINADOR GENERAL DE PROFESIONALIZACION
                If (Request.QueryString("ctf") = 85 Or Request.QueryString("ctf") = 181) And Request.QueryString("MOD") = 3 Then
                    'If (Request.QueryString("ctf") = 181) And Request.QueryString("MOD") = 3 Then
                    'Fin HCano 06-02-18
                    Me.cmdNuevoJuridica.Visible = False
                    Me.cmdNuevoPersonaSinCargo.Visible = False
                    Me.cmdNuevo.Visible = False
                ElseIf Request.QueryString("ctf") = 90 And Request.QueryString("MOD") = 6 Then
                    Me.cmdNuevoJuridica.Visible = True
                End If
                '---------------------------------------------------
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub cmdActualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdActualizar.Click
        CargarInscritosConCargo()
    End Sub
    Private Sub CargarInscritosConCargo()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Me.grwListaPersonas.DataSource = obj.TraerDataTable("EVE_ConsultarInscritos", Request.QueryString("cco"))
        'Inicio Hcano 24-07-17
        'Me.grwListaPersonas.DataSource = obj.TraerDataTable("EVE_ConsultarInscritosxEstado", Request.QueryString("cco"), Me.ddlEstado.SelectedValue)
        Me.grwListaPersonas.DataSource = obj.TraerDataTable("EVE_ConsultarInscritosxEstado_v1", Request.QueryString("cco"), Me.ddlEstado.SelectedValue, Me.txtbuscar.Text)
        'Fin Hcano 24-07-17
        Me.grwListaPersonas.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Private Sub ValidarSegunModulo(ByRef Resultado As String)
        'Valida según el módulo para mostrar el botón de inscripción de Persona / E-PRE
        Select Case Request.QueryString("mod")
            Case "1" 'Epre
                If Request.QueryString("tab") = 1 Then 'Tab Inscripcion
                    Resultado = "../frmpersonaepre.aspx?mod=" & Request.QueryString("mod") & "&cco=" & Me.Request.QueryString("cco") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id") & "&tab=1"
                ElseIf Request.QueryString("tab") = 3 Then 'Tab Inscripcion Completa
                    Resultado = "../frmpersonaepre_largo.aspx?mod=" & Request.QueryString("mod") & "&cco=" & Me.Request.QueryString("cco") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id") & "&tab=3"
                End If

            Case Else 'Persona siempre


                Resultado = "../frmpersona.aspx?mod=" & Request.QueryString("mod") & "&cco=" & Me.Request.QueryString("cco") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id")
        End Select
    End Sub
    Protected Sub cmdNuevoJuridica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevoJuridica.Click
        Response.Redirect("../frmpersonajuridica.aspx?mod=" & Request.QueryString("mod") & "&cco=" & Me.Request.QueryString("cco") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id"))
    End Sub
    Protected Sub cmdNuevoPersonaSinCargo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevoPersonaSinCargo.Click
        Response.Redirect("../frmpersonasincargo.aspx?accion=A&mod=" & Request.QueryString("mod") & "&cco=" & Me.Request.QueryString("cco") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id"))
    End Sub
    Private Sub ConsultarDatosEvento()
        Dim obj As New ClsConectarDatos
        Dim dtsDatosEvento As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtsDatosEvento = obj.TraerDataTable("[EVE_ConsultarEventos]", 0, Request.QueryString("cco"), "")
        obj.CerrarConexion()
        obj = Nothing
        If DateAdd(DateInterval.Day, 1, dtsDatosEvento.Rows(0).Item("fechafinpropuesta_dev")) < Now() Then
            cmdNuevo.Enabled = False
            cmdNuevoJuridica.Enabled = False
        End If
    End Sub

    Protected Sub grwListaPersonas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwListaPersonas.SelectedIndexChanged
        Try
            Dim cls As New clsEvento
            Dim blnResultado As Boolean = False
            'Response.Write(Me.grwListaPersonas.SelectedRow.Cells(4).Text)
            blnResultado = cls.EnviaClavesAlumno(Me.grwListaPersonas.SelectedRow.Cells(4).Text, Request.QueryString("cco"))
            If (blnResultado = True) Then
                Response.Write("<script>alert('Correo Enviado')</script>")
            Else
                Response.Write("<script>alert('No se pudo enviar el correo electronico al alumno')</script>")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write("<script>alert('Error al enviar el correo electronico')</script>")
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click        
        CargarInscritosConCargo()
    End Sub

    Protected Sub cmdReporte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReporte.Click
        'Response.Write("<script>window.open('rpteinscritoseventocargo.aspx?cco=" & Request.QueryString("cco") & "&estado=" & Me.ddlEstado.SelectedValue & "','600','800','yes','yes','yes','yes')</script>")
        Response.Write("<script>window.open('rpteinscritoseventocargo.aspx?cco=" & Request.QueryString("cco") & "&estado=" & Me.ddlEstado.SelectedValue & "', 'Reporte', 'menubar=yes,location=yes,resizable=yes,scrollbars=yes,status=yes,width=800,height=600')</script>")
        'Response.Write("<script>window.open('www.google.com.pe','600','800','yes','yes','yes','yes')</script>")        
    End Sub

    Protected Sub cmdNuevo_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles cmdNuevo.Command

    End Sub

    Protected Sub btnBuscar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Load

    End Sub
End Class

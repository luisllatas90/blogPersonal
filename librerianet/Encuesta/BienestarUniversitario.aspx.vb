﻿'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class Encuesta_BienestarUniversitario
    Inherits System.Web.UI.Page

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        If Me.RbtBienestar.SelectedValue = 1 Then
            If Me.TxtBienestar.Text = "" Then
                RegisterStartupScript("Bienestar", "<script>alert('Debe registrar el programa de bienestar universitario que conoce')</script>")
                LblBienestar.ForeColor = Drawing.Color.Red
                Me.TxtBienestar.Focus()
                Exit Sub
            End If
        End If
        GuardarDatos()
    End Sub

    Private Sub VerificarAcreditacionUniversitariaCompleta()
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim datos As New Data.DataTable
        datos = Obj.TraerDataTable("AUN_ConsultarEstadoAcreditacionUniversitaria", "TO", Session("codigo_alu"))
        If datos.Rows.Count = 1 Then
            RegisterStartupScript("Acceso", "<script>alert('Gracias por llenar la encuesta, Ahora puedes acceder al Campus Virtual')</script>")
            'RegisterStartupScript("Redireccionar", "<script>location.href='http://www.usat.edu.pe/campusvirtual'</script>")
            RegisterStartupScript("Redireccionar", "<script>location.href='../..'</script>")
        Else
            Response.Redirect("AcreditacionUniversitaria_generales.aspx")
        End If
    End Sub

    Private Sub GuardarDatos()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim rpta As Int16
        rpta = obj.Ejecutar("AUN_ActualizarBienestarUniversitario", _
                           RbtBienestar.SelectedValue, _
                           TxtBienestar.Text, _
                           RbtAtencion.SelectedValue & RbtAtencion.SelectedItem.Text, _
                           RbtPsicologia.SelectedValue & RbtPsicologia.SelectedItem.Text, _
                           RbtPedagogia.SelectedValue & RbtPedagogia.SelectedItem.Text, _
                           RbtAsistencia.SelectedValue & RbtAsistencia.SelectedItem.Text, _
                           RbtDeportes.SelectedValue & RbtDeportes.SelectedItem.Text, _
                           RbtActividades.SelectedValue & RbtActividades.SelectedItem.Text, _
                           RbtBiblioteca.SelectedValue & RbtBiblioteca.SelectedItem.Text, _
                           RbtBibliotecaVirtual.SelectedValue & RbtBibliotecaVirtual.SelectedItem.Text, _
                           Date.Now.ToShortDateString, _
                           Session("codigo_alu"), 0)
        If rpta = 0 Then
            RegisterStartupScript("GuardarDatos", "<script>alert('Para guardar la sección 4 deberá haber registrado primero la sección 1')</script>")
            RegisterStartupScript("Redirect", "<script>document.location.href='AcreditacionUniversitaria_generales.aspx'</script>")
        Else
            VerificarAcreditacionUniversitariaCompleta()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim Datos As New Data.DataTable
            Datos = Obj.TraerDataTable("AUN_ConsultarEstadoAcreditacionUniversitaria", "BU", Session("codigo_alu"))
            If Datos.Rows.Count = 1 Then
                CargarBienestarUniversitario(sender, e)
            Else
                LblBienestar.Visible = False
                TxtBienestar.Visible = False
            End If
        End If
    End Sub
    Private Sub CargarBienestarUniversitario(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Datos As New Data.DataTable
        Datos = obj.TraerDataTable("AUN_ConsultarAcreditacionUniversitaria", "BU", Session("codigo_alu"))
        If Datos.Rows.Count > 0 Then
            With Datos.Rows(0)
                RbtBienestar.SelectedValue = .Item("ConoceProgBienestar_Aun")
                TxtBienestar.Text = .Item("ProgramaBienestar_Aun")
                RbtAtencion.SelectedValue = Left(.Item("AtencionMedica_Aun"), 1)
                RbtPsicologia.SelectedValue = Left(.Item("Psicologia_Aun"), 1)
                RbtPedagogia.SelectedValue = Left(.Item("Pedagogia_Aun"), 1)
                RbtAsistencia.SelectedValue = Left(.Item("AsistenciaSocial_Aun"), 1)
                RbtDeportes.SelectedValue = Left(.Item("Deportes_Aun"), 1)
                RbtActividades.SelectedValue = Left(.Item("Culturales_Aun"), 1)
                RbtBiblioteca.SelectedValue = Left(.Item("Biblioteca_Aun"), 1)
                RbtBibliotecaVirtual.SelectedValue = Left(.Item("BibliotecaVirtual_Aun"), 1)
            End With
            RbtBienestar_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Protected Sub RbtBienestar_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtBienestar.SelectedIndexChanged
        If RbtBienestar.SelectedValue = 2 Then
            LblBienestar.Visible = False
            TxtBienestar.Visible = False
        Else
            LblBienestar.Visible = True
            TxtBienestar.Visible = True
        End If
    End Sub
End Class

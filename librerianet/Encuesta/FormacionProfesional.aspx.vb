'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class Encuesta_FormacionProfesional
    Inherits System.Web.UI.Page

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        GuardarDatos()
    End Sub

    Private Sub GuardarDatos()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim rpta As Int16
        rpta = obj.Ejecutar("AUN_ActualizarFormacionProfesional", _
                            RbtEnsenanza.SelectedValue & RbtEnsenanza.SelectedItem.Text, _
                            RbtDesarrollo.SelectedValue & RbtDesarrollo.SelectedItem.Text, _
                            RbtSilabus.SelectedValue & RbtSilabus.SelectedItem.Text, _
                            RbtEvaluacion.SelectedValue & RbtEvaluacion.SelectedItem.Text, _
                            RbtBeneficios.SelectedValue & RbtBeneficios.SelectedItem.Text, _
                            RbtAyuda.SelectedValue & RbtAyuda.SelectedItem.Text, _
                            RbtTutoria.SelectedValue & RbtTutoria.SelectedItem.Text, _
                            Session("codigo_alu"), 0)
        If rpta = 0 Then
            RegisterStartupScript("GuardarDatos", "<script>alert('Para guardar la sección 3 deberá haber registrado primero la sección 1')</script>")
            RegisterStartupScript("Redirect", "<script>document.location.href='AcreditacionUniversitaria_generales.aspx'</script>")
        Else
            VerificarAcreditacionUniversitariaCompleta()
        End If
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
            Response.Redirect("BienestarUniversitario.aspx")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim Datos As New Data.DataTable
            Datos = Obj.TraerDataTable("AUN_ConsultarEstadoAcreditacionUniversitaria", "FP", Session("codigo_alu"))
            If Datos.Rows.Count = 1 Then
                CargarFormacionProfesional()
            End If
        End If
    End Sub
    Private Sub CargarFormacionProfesional()
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim DatFormacion As New Data.DataTable
        DatFormacion = Obj.TraerDataTable("AUN_ConsultarAcreditacionUniversitaria", "FP", Session("codigo_alu"))
        If DatFormacion.Rows.Count > 0 Then
            With DatFormacion.Rows(0)
                RbtEnsenanza.SelectedValue = Left(.Item("EstrategiasEnseñanza_Aun"), 1)
                RbtDesarrollo.SelectedValue = Left(.Item("EstrategiasDesarrollo_Aun"), 1)
                RbtSilabus.SelectedValue = Left(.Item("SilabusClases_Aun"), 1)
                RbtEvaluacion.SelectedValue = Left(.Item("EvaluacionAprendizaje_Aun"), 1)
                RbtBeneficios.SelectedValue = Left(.Item("BeneficionRecibidos_Aun"), 1)
                RbtAyuda.SelectedValue = Left(.Item("AyudaRecibida_Aun"), 1)
                RbtTutoria.SelectedValue = Left(.Item("TutoriaRecibida_Aun"), 1)
            End With
        End If
    End Sub
End Class

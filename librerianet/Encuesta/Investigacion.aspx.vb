﻿'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class Encuesta_Investigacion
    Inherits System.Web.UI.Page

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim rpta As Boolean
        rpta = ValidarDatos()
        If rpta = True Then
            GuardarDatos()
        End If
    End Sub

    Private Sub GuardarDatos()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim modoparticipacion As String
        Dim rpta As Int16
        If CboModoParticipacion.SelectedValue = 4 Then
            modoparticipacion = Me.TxtModoParticipacion.Text
        Else
            modoparticipacion = CboModoParticipacion.SelectedValue & CboModoParticipacion.SelectedItem.Text
        End If
        rpta = obj.Ejecutar("AUN_ActualizarInvestigacion", _
                          RbtParticipo.SelectedValue, _
                          TxtNroProyectos.Text, _
                          TxtTituloPro.Text, _
                          modoparticipacion, _
                          CboAnio.SelectedItem.Text, _
                          CboMes.SelectedItem.Text, _
                          TxtFinancio.Text, _
                          CboMedioVer.SelectedValue & CboMedioVer.SelectedItem.Text, _
                          RbtEvaluacion.SelectedValue, _
                          RbtDifusion.SelectedValue, _
                          TxtProyDifusion.Text, _
                          TxtAutorDifusion.Text, _
                          RbtDiscusion.SelectedValue, _
                          TxtProyDiscusion.Text, _
                          TxtAutorDiscusion.Text, _
                          RbtPropIntelectual.SelectedValue, _
                          TxtPropintelectual.Text, _
                          Session("codigo_alu"), 0)
        If rpta = 0 Then
            RegisterStartupScript("GuardarDatos", "<script>alert('Para guardar la sección 2 deberá haber registrado primero la sección 1')</script>")
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
            Response.Redirect("FormacionProfesional.aspx")
        End If
    End Sub
    Private Function ValidarDatos() As Boolean
        If RbtParticipo.SelectedValue = 1 Then
            If Me.TxtNroProyectos.Text <= 0 Then
                RegisterStartupScript("proyectos", "<script>alert('El número de proyectos debe ser mayor que cero')</script>")
                Me.LblProyecto.ForeColor = Drawing.Color.Red
                Me.TxtNroProyectos.Focus()
                Return False
                'Exit Function
            End If
            If Me.TxtTituloPro.Text = "" Then
                RegisterStartupScript("Titulo", "<script>alert('Especifique el nombre del proyecto en el que particípó')</script>")
                RegisterStartupScript("Titulo1", "<script>TDTitulo.style.color='red';</script>")
                Return False
                'Exit Function
            End If
        End If
        If RbtDifusion.SelectedValue = 1 Then
            If Me.TxtProyDifusion.Text = "" Then
                RegisterStartupScript("difusion", "<script>alert('Debe indicar el nombre del proyecto de difusión')</script>")
                LblDifusion.ForeColor = Drawing.Color.Red
                Me.TxtProyDifusion.Focus()
                Return False
                'Exit Function
            End If
            If Me.TxtAutorDifusion.Text = "" Then
                RegisterStartupScript("autor difusion", "<script>alert('Debe indicar los autores del proyecto de difusión')</script>")
                LblAutordifusion.ForeColor = Drawing.Color.Red
                Me.TxtAutorDifusion.Focus()
                Return False
                'Exit Function
            End If
        End If
        If RbtDiscusion.SelectedValue = 1 Then
            If Me.TxtProyDiscusion.Text = "" Then
                RegisterStartupScript("discusion", "<script>alert('Debe indicar el nombre del proyecto de discusión')</script>")
                LblDiscusion.ForeColor = Drawing.Color.Red
                Me.TxtProyDiscusion.Focus()
                Return False
                'Exit Function
            End If
            If Me.TxtAutorDiscusion.Text = "" Then
                RegisterStartupScript("autor discusion", "<script>alert('Debe indicar los autores del proyecto de discusión')</script>")
                LblAutordiscusion.ForeColor = Drawing.Color.Red
                Me.TxtAutorDiscusion.Focus()
                Return False
                'Exit Function
            End If
        End If
        If RbtPropIntelectual.SelectedValue = 1 Then
            If TxtPropintelectual.Text = "" Then
                RegisterStartupScript("Propiedad intelectual", "<script>alert('Debe indicar que propiedad intelectual conoce')</script>")
                LblPropiedad.ForeColor = Drawing.Color.Red
                Me.TxtPropintelectual.Focus()
                Return False
                'Exit Function
            End If
        End If
        If CboModoParticipacion.SelectedValue = 4 Then
            If TxtModoParticipacion.Text = "" Then
                RegisterStartupScript("Modo Participacion", "<script>alert('Debe indicar que modo de participación tuvo en el proyecto')</script>")
                Me.LblModoParticipacion.Visible = True
                Me.TxtModoParticipacion.Focus()
                Return False
                'Exit Function
            End If
        End If
        Return True
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                Dim Datos As New Data.DataTable
                'Llenar año
                For i As Int16 = Year(Now.Date) To 1900 Step -1
                    Me.CboAnio.Items.Add(i)
                Next
                'Llenar mes
                For i As Int16 = 12 To 1 Step -1
                    Me.CboMes.Items.Add(i)
                Next
                Datos = Obj.TraerDataTable("AUN_ConsultarEstadoAcreditacionUniversitaria", "IN", Session("codigo_alu"))
                If Datos.Rows.Count = 1 Then
                    CargarInvestigacion(sender, e)
                Else
                    NoMostrar()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CargarInvestigacion(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Datos As New Data.DataTable
        Datos = obj.TraerDataTable("AUN_ConsultarAcreditacionUniversitaria", "IN", Session("codigo_alu"))
        If Datos.Rows.Count > 0 Then
            With Datos.Rows(0)
                RbtParticipo.SelectedValue = .Item("ParticipoEnProyecto_Aun")
                TxtNroProyectos.Text = .Item("NumProyectos_Aun")
                TxtTituloPro.Text = .Item("TituloProyecto_Aun")
                CboModoParticipacion.SelectedValue = Left(.Item("ModoParticipacion_Aun"), 1)
                CboAnio.SelectedValue = .Item("AnioParticipo_Aun")
                CboMes.SelectedValue = .Item("MesesParticipo_Aun")
                TxtFinancio.Text = .Item("QuienFinancio_Aun")
                CboMedioVer.SelectedValue = .Item("MedioVerificacion_Aun")
                RbtEvaluacion.SelectedValue = Left(.Item("SatisfechoEvaluacion_Aun"), 1)
                RbtDifusion.SelectedValue = Left(.Item("EventoDifusion_Aun"), 1)
                TxtProyDifusion.Text = .Item("ProyectoDifusion_Aun")
                TxtAutorDifusion.Text = .Item("AutoresDifusion_Aun")
                RbtDiscusion.SelectedValue = .Item("EventoDiscusion_Aun")
                TxtProyDiscusion.Text = .Item("ProyectoDiscusion_Aun")
                TxtAutorDiscusion.Text = .Item("AutoresDiscusion_Aun")
                RbtPropIntelectual.SelectedValue = .Item("ConocePropIntelectual_Aun")
                TxtPropintelectual.Text = .Item("PropiedadIntelectual_Aun")
                RbtParticipo_SelectedIndexChanged(sender, e)
                RbtDifusion_SelectedIndexChanged(sender, e)
                RbtDiscusion_SelectedIndexChanged(sender, e)
                RbtPropIntelectual_SelectedIndexChanged(sender, e)
            End With
        End If
    End Sub

    Protected Sub CboModoParticipacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboModoParticipacion.SelectedIndexChanged
        If CboModoParticipacion.SelectedValue = 4 Then
            Me.TxtModoParticipacion.Visible = True
        Else
            Me.TxtModoParticipacion.Visible = False
        End If
    End Sub

    Protected Sub RbtParticipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtParticipo.SelectedIndexChanged
        If RbtParticipo.SelectedValue = 2 Then
            Me.LblProyecto.Visible = False
            Me.TxtNroProyectos.Visible = False
            Me.LblPase23.Visible = True
        Else
            Me.LblProyecto.Visible = True
            Me.TxtNroProyectos.Visible = True
            Me.LblPase23.Visible = False
        End If
    End Sub

    Protected Sub RbtDifusion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtDifusion.SelectedIndexChanged
        If RbtDifusion.SelectedValue = 2 Then
            LblDifusion.Visible = False
            TxtProyDifusion.Visible = False
            LblAutordifusion.Visible = False
            TxtAutorDifusion.Visible = False
        Else
            LblDifusion.Visible = True
            TxtProyDifusion.Visible = True
            LblAutordifusion.Visible = True
            TxtAutorDifusion.Visible = True
        End If
    End Sub

    Protected Sub RbtDiscusion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtDiscusion.SelectedIndexChanged
        If RbtDiscusion.SelectedValue = 2 Then
            LblDiscusion.Visible = False
            TxtProyDiscusion.Visible = False
            LblAutordiscusion.Visible = False
            TxtAutorDiscusion.Visible = False
        Else
            LblDiscusion.Visible = True
            TxtProyDiscusion.Visible = True
            LblAutordiscusion.Visible = True
            TxtAutorDiscusion.Visible = True
        End If
    End Sub

    Protected Sub RbtPropIntelectual_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtPropIntelectual.SelectedIndexChanged
        If RbtPropIntelectual.SelectedValue = 2 Then
            LblPropiedad.Visible = False
            TxtPropintelectual.Visible = False
        Else
            LblPropiedad.Visible = True
            TxtPropintelectual.Visible = True
        End If
    End Sub

    Protected Sub TxtNroProyectos_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNroProyectos.TextChanged
        If TxtNroProyectos.Text > 0 Then
            TxtNroProyectos.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Private Sub NoMostrar()
        Me.LblPase23.Visible = False
        Me.LblProyecto.Visible = False
        Me.TxtNroProyectos.Visible = False
        LblDifusion.Visible = False
        TxtProyDifusion.Visible = False
        LblAutordifusion.Visible = False
        TxtAutorDifusion.Visible = False
        LblDiscusion.Visible = False
        TxtProyDiscusion.Visible = False
        LblAutordiscusion.Visible = False
        TxtAutorDiscusion.Visible = False
        LblPropiedad.Visible = False
        TxtPropintelectual.Visible = False
    End Sub

    Protected Sub TxtProyDifusion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtProyDifusion.TextChanged
        If TxtProyDifusion.Text.Length > 0 Then
            LblDifusion.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Protected Sub TxtProyDiscusion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtProyDiscusion.TextChanged
        If TxtProyDiscusion.Text.Length > 0 Then
            LblDiscusion.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Protected Sub TxtAutorDifusion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtAutorDifusion.TextChanged
        If TxtAutorDifusion.Text.Length > 0 Then
            LblAutordifusion.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Protected Sub TxtAutorDiscusion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtAutorDiscusion.TextChanged
        If TxtAutorDiscusion.Text.Length > 0 Then
            LblAutordiscusion.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Protected Sub TxtPropintelectual_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPropintelectual.TextChanged
        If TxtPropintelectual.Text.Length > 0 Then
            LblPropiedad.ForeColor = Drawing.Color.Black
        End If
    End Sub
End Class

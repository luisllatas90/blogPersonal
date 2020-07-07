
Partial Class academico_notas_administrarconsultar_ConstanciaMatriculasNotas
    Inherits System.Web.UI.Page
#Region "Variables"

    Dim tipoestudio, codigo_tfu, curso, codigo_cac, tipo, codigo_usu, tipoPrint, codigo_alu, rutaReporte As String

    Dim md_Funciones As d_Funciones
    Dim md_Horario As d_Horario
    Dim me_CicloAcademico As e_CicloAcademico

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        codigo_tfu = Request.QueryString("ctf")
        'tipoestudio = Request.QueryString("mod")
        tipoestudio = "2"
        codigo_usu = Request.QueryString("id")
        tipoPrint = Request.QueryString("tipoPrint")

        rutaReporte = ConfigurationManager.AppSettings("RutaReporte")

        If IsPostBack = False Then

            Call mt_CargarComboCicloAcademico()
            Call mt_cargarComboTipoReporte()
            'Call mt_cargarCarrreraProfesional()
        End If

    End Sub
    Protected Sub lbBuscaAlu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbBuscaAlu.Click
        Try
            ifrmReporte.Attributes("src") = "about:blank"
            'validamos los 10 caracteres
            If Len(Me.txtCodigoAlu.Text.Trim()) <> 10 Then
                Call mt_ShowMessage("El código del alumno debe contener 10 caracteres..!", MessageType.warning)
                Me.txtDescripcionAlu.Text = String.Empty
                Me.txtCodigoAlu.Focus()

                Exit Sub
            Else
                Dim objcnx As New ClsConectarDatos
                objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                objcnx.AbrirConexion()
                Dim dt As Data.DataTable
                dt = objcnx.TraerDataTable("ConsultarAlumno", "CU", Me.txtCodigoAlu.Text.Trim())
                objcnx.CerrarConexion()
                If dt.Rows.Count > 0 Then
                    With dt.Rows(0)
                        Me.txtDescripcionAlu.Text = .Item("alumno")
                        Me.TxtCodAlu.Text = .Item("codigo_Alu")

                    End With
                Else
                    Call mt_ShowMessage("No se encontró el alumno..¡", MessageType.warning)
                    Me.txtDescripcionAlu.Text = String.Empty
                End If

            End If



            'Dim dt As New Data.DataTable
            'dt = md_Horario.Horario_ValidaCambioAmbiente(me_Horario)



        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
        
    End Sub
    Protected Sub ddlSemAca_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemAca.SelectedIndexChanged
        ifrmReporte.Attributes("src") = "about:blank"
    End Sub

    Protected Sub lbImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbImprimir.Click
        Try
            ''validaciones
            If Me.ddlTipoReporte.SelectedValue = "0" Then
                Call mt_ShowMessage("Debe seleccionar el tipo de reporte..!", MessageType.warning)
                Exit Sub
            End If
            If Me.ddlSemAca.SelectedValue = "" Then
                Call mt_ShowMessage("Debe seleccionar el ciclo académico..!", MessageType.warning)
                Exit Sub
            End If
            If Me.txtDescripcionAlu.Text = "" Then
                Call mt_ShowMessage("Debe seleccionar el alumno...!", MessageType.warning)
                Me.txtCodigoAlu.Focus()
                Exit Sub
            End If
            

            ''fin de validaciones

            ''si tipo de impresión Fich de MAtrícula
            If ddlTipoReporte.SelectedValue = "1" Then
                'Se quito el codigo usuario que estaba en duro
                ifrmReporte.Attributes("src") = rutaReporte & "PRIVADOS/ACADEMICO/ACAD_FichaMatricula&id=" & codigo_usu & "&ctf=" & codigo_tfu & "&codigo_Alu=" & Me.TxtCodAlu.Text & "&codigoUniver_Alu=" & Me.txtCodigoAlu.Text & "&codigo_cac=" & Me.ddlSemAca.SelectedValue

                ''si tipo de impresión Fich de Notas
            ElseIf ddlTipoReporte.SelectedValue = "2" Then
                'Se quito el codigo usuario que estaba en duro
                ifrmReporte.Attributes("src") = rutaReporte & "PRIVADOS/ACADEMICO/ACAD_FichaNotas&id=" & codigo_usu & "&ctf=" & codigo_tfu & "&codigo_alu=" & Me.TxtCodAlu.Text & "&codigoUniver_Alu=" & Me.txtCodigoAlu.Text & "&codigo_cac=" & Me.ddlSemAca.SelectedValue

            End If


            ''


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
        
    End Sub



#End Region

#Region "Métodos y Funciones"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboCicloAcademico()
        Try
            md_Funciones = New d_Funciones
            md_Horario = New d_Horario
            me_CicloAcademico = New e_CicloAcademico

            Dim dt As New Data.DataTable

            With me_CicloAcademico
                .tipooperacion = "TO"
                .tipocac = "0"
            End With

            dt = md_Horario.ObtenerCicloAcademicoHorario(me_CicloAcademico)

            Call md_Funciones.CargarCombo(Me.ddlSemAca, dt, "codigo_cac", "descripcion_cac", True, "[-- SELECCIONE --]", "")

        Catch ex As Exception
            Throw ex
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_cargarComboTipoReporte()
        '***** Tio de Reporte
        Me.ddlTipoReporte.Items.Clear()
        'Me.ddlTipoReporte.Items.Add("[--SELECCIONE--]")
        'Me.ddlTipoReporte.Items.Add("CONSTANCIA DE MATRICULA")
        'Me.ddlTipoReporte.Items.Add("CONSTANCIA DE NOTAS")

        Me.ddlTipoReporte.Items.Add(New ListItem("[--SELECCIONE--]", "0"))
        Me.ddlTipoReporte.Items.Add(New ListItem("FICHA DE MATRICULA", "1"))
        Me.ddlTipoReporte.Items.Add(New ListItem("FICHA DE NOTAS", "2"))

        Me.ddlTipoReporte.SelectedIndex = tipoPrint

        
    End Sub


#End Region
    
    
End Class

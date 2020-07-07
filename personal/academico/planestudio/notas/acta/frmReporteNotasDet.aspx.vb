
Partial Class academico_notas_frmReporteNotasDet
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            'Me.btnImprimir.Attributes.Add("onclick", "imprimir('N','','');return(false);")

            ''<input onClick="AbrirRegistro('D','<%=codigo_per%>','<%=nombre_per%>',<%=nivel%>)" name="cmdDescargar" type="button" value="    Reg. Notas" class="word2" disabled="true" style="width:90" />
            If (Request.QueryString("cup") IsNot Nothing) Then
                Me.hdCup.Value = Request.QueryString("cup")
                If (hdCup.Value = 0) Then
                    LimpiaDetalle()
                Else
                    CargaDetalle(Me.hdCup.Value)
                    'Me.btnImprimir.Attributes.Add("onclick", "AbrirRegistro('D','" & Request.QueryString("id") & "','" & Me.lblDocente.Text & "'," & Request.QueryString("ctf") & ")")
                End If
            Else
                LimpiaDetalle()
            End If
        End If
        
    End Sub

    Private Sub CargaDetalle(ByVal codigo_cup As Integer)
        Try
            Dim dt As New Data.DataTable
            Dim dtDatos As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtDatos = obj.TraerDataTable("ACAD_RetornaDatoCursoProgramado", codigo_cup)
            dt = obj.TraerDataTable("ACAD_ConsultarAlumnosMatriculados", codigo_cup, 0)
            obj.CerrarConexion()

            If (dtDatos.Rows.Count > 0) Then
                Me.lblCurso.Text = dtDatos.Rows(0).Item("nombre_Cur")
                Me.lblDocente.Text = dtDatos.Rows(0).Item("docente")
                Me.lblEstado.Text = dtDatos.Rows(0).Item("impresion_cup")
                Me.lblCiclo.Text = dtDatos.Rows(0).Item("descripcion_Cac")
                Me.hdper.Value = dtDatos.Rows(0).Item("codigo_Per")
                Me.hdCac.Value = dtDatos.Rows(0).Item("codigo_Cac")
                Me.hdIdentificador.Value = dtDatos.Rows(0).Item("identificador_Cur")
                Me.lblGrupo.Text = dtDatos.Rows(0).Item("grupoHor_Cup")
                Me.lblNCiclo.Text = dtDatos.Rows(0).Item("ciclo_Cur")
            End If

            Me.gvAlumnos.DataSource = dt
            Me.gvAlumnos.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar detalle: " & ex.Message
        End Try
    End Sub

    Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("ACAD_RegistraImpresionCurso", Me.hdCup.Value)
            obj.CerrarConexion()

            'Response.Write(" <script>onclientclick='imprimir('N','','');return(false)'</script>")
            'Response.Write("<script>imprimir('N','','');return(false)</script>")
            Response.Redirect("../administrarconsultar/rpteregistro.asp?codigo_cac=" & Me.hdCac.Value & "&descripcion_cac=" & Me.lblCiclo.Text & "&codigo_per=" & Me.hdper.Value & "&nombre_per=" & Me.lblDocente.Text & "&codigo_cup=" & Me.hdCup.Value & "&identificador_cur=" & Me.hdIdentificador.Value & "&nombre_cur=" & Me.lblCurso.Text & "&grupohor_cur=" + Me.lblGrupo.Text + "&ciclo_cur=" & Me.lblNCiclo.Text)

            LimpiaDetalle()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al imprimir el documento: " & ex.Message
        End Try
    End Sub

    Private Sub LimpiaDetalle()
        Me.gvAlumnos.DataSource = Nothing
        Me.gvAlumnos.DataBind()
        Me.lblCurso.Text = ""
        Me.lblDocente.Text = ""
        Me.lblEstado.Text = ""
        Me.lblCiclo.Text = ""
        Me.lblGrupo.Text = ""
        Me.lblNCiclo.Text = ""
        Me.hdCup.Value = 0
        Me.hdper.Value = 0
        Me.hdCac.Value = 0
        Me.hdIdentificador.Value = 0
    End Sub

    Protected Sub gvAlumnos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvAlumnos.SelectedIndexChanged

    End Sub
End Class

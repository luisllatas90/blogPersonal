Partial Class frmpersona
    Inherits System.Web.UI.Page
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load        
        If IsPostBack = False Then
            cargarDatosPagina()
            CargarDatosPersonales()
        End If
    End Sub
    Sub cargarDatosPagina()
        Me.DDlAnioI.Items.Add("Año")
        For i As Integer = (Now.Year - 0) To 1960 Step -1 'clluen
            Me.DDlAnioI.Items.Add(i.ToString)
        Next
        Me.DDlAnioF.Items.Add("En Curso")
        For f As Integer = (Now.Year - 0) To 1960 Step -1 'clluen
            Me.DDlAnioF.Items.Add(f.ToString)
        Next
    End Sub
    Sub MostrarFormularios()
        Me.txtarea.visible = True
    End Sub
    Sub OcultaFormularios()
        Me.txtarea.visible = False
    End Sub
    Sub CargarDatosPersonales()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tbl As Data.DataTable
        tbl = obj.TraerDataTable("ALUMNI_ConsultarExperienciaLaboral", Session("codigo_alu"))
        If tbl.Rows.Count Then
            'Me.hdcodigo_pso.Value = tbl.Rows(0).Item("codigo_Pso")
            Me.txtinstitucion.Text = tbl.Rows(0).Item("empresa").ToString
            Me.txtciudad.Text = tbl.Rows(0).Item("ciudad").ToString
            Me.txtdescripcion.Text = tbl.Rows(0).Item("descripcion_EXp").ToString
            Me.txtsector.Text = tbl.Rows(0).Item("sector_Exp").ToString
            Me.txtarea.Text = tbl.Rows(0).Item("area_Exp").ToString
            Me.dpMInicio.SelectedValue = -1
            If (tbl.Rows(0).Item("fechaInicio_Exp") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("fechaInicio_Exp").ToString.Trim <> "") Then
                Me.dpMInicio.SelectedValue = month(tbl.Rows(0).Item("fechaInicio_Exp").ToString)
            End If
            Me.dpMFin.SelectedValue = -1
            If (tbl.Rows(0).Item("fechaFin_Exp") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("fechaFin_Exp").ToString.Trim <> "") Then
                Me.dpMFin.SelectedValue = month(tbl.Rows(0).Item("fechaFin_Exp").ToString)
            End If

            If IsDate(tbl.Rows(0).Item("fechaInicio_Exp").ToString) Then
                DDlAnioI.SelectedValue = Year(tbl.Rows(0).Item("fechaInicio_Exp").ToString)      'AñoIni
            End If
            If IsDate(tbl.Rows(0).Item("fechaFin_Exp").ToString) Then
                DDlAnioF.SelectedValue = Year(tbl.Rows(0).Item("fechaFin_Exp").ToString)      'AñoIni
            End If
        End If
        tbl.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            obj.AbrirConexion()
            obj.Ejecutar("ALUMNI_ActualizaExperienciaLaboral", Session("codigo_alu"), "", "", Me.txtinstitucion.text, Me.txtciudad.text, Me.txtdescripcion.text, Me.txtsector.text, Me.txtarea.text)
            obj.CerrarConexion()
            Response.Write("<script>alert('experiencia laboral de Egresado Actualizado')</script>")
            cargarDatosPagina()
            CargarDatosPersonales()
        Catch ex As Exception
            Response.Write("<br /> error" & ex.Message & " -  " & ex.StackTrace)
        End Try
    End Sub
End Class


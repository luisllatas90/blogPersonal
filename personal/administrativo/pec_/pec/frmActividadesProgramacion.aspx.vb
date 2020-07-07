Partial Class administrativo_pec2_frmActividades
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            Me.Hdcodigo_aev.Value = Request.QueryString("codigo_aev")
            Me.txtNombre.Text = Request.QueryString("nom")
            Me.txtNombre.Font.Bold = True
            Me.cmdCancelar.Attributes.Add("onclick", "self.parent.tb_remove();")
            Me.txtHoraInicio.Attributes.Add("onkeyup", "mascara(this,':',patron,true)")
            Me.txtHoraFin.Attributes.Add("onkeyup", "mascara(this,':',patron,true)")
            Me.txtFecha.Attributes.Add("onkeyup", "mascara(this,'/',patron2,true)")
        End If
        CargaGrid()
    End Sub

    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            If validaFechas() Then
                obj.AbrirConexion()
                obj.Ejecutar("[EVE_AgregaActividadProgramacion]", Request.QueryString("codigo_aev"), Me.txtFecha.Text & " " & Me.txtHoraInicio.Text, Me.txtLugar.Text, Me.txtFecha.Text & " " & Me.txtHoraFin.Text)
                obj.CerrarConexion()
                LimpiarControles()
                Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('Registro Guardado')</SCRIPT>")
                CargaGrid()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarControles()
        Me.txtFecha.Text = ""
        Me.txtHoraInicio.Text = ""
        Me.txtHoraFin.Text = ""
        Me.txtLugar.Text = ""
    End Sub

    Private Function validaFechas() As Boolean
        If (Me.txtHoraInicio.Text.Trim.Length <> 5) Then
            Response.Write("<script>alert('Formato de hora incorrecto')</script>")
            Me.txtHoraInicio.Text = ""
            Return False
        End If

        If (Me.txtHoraFin.Text.Trim.Length <> 5) Then
            Response.Write("<script>alert('Formato de hora incorrecto')</script>")
            Me.txtHoraFin.Text = ""
            Return False
        End If

        Try
            Date.Parse(Me.txtFecha.Text)
        Catch ex As Exception
            Me.txtFecha.Text = ""
            Response.Write("<script>alert('Fecha incorrecta')</script>")
            Return False
        End Try

        Try
            Date.Parse(Me.txtFecha.Text & " " & Me.txtHoraInicio.Text)
        Catch ex As Exception
            Me.txtHoraInicio.Text = ""
            Response.Write("<script>alert('Formato de hora incorrecto.')</script>")
            Return False
        End Try

        Try
            Date.Parse(Me.txtFecha.Text & " " & Me.txtHoraFin.Text)
        Catch ex As Exception
            Me.txtHoraFin.Text = ""
            Response.Write("<script>alert('Formato de hora incorrecto')</script>")
            Return False
        End Try

        Dim fec1 As Date
        Dim fec2 As Date
        fec1 = Date.Parse(Me.txtFecha.Text & " " & Me.txtHoraInicio.Text)
        fec2 = Date.Parse(Me.txtFecha.Text & " " & Me.txtHoraFin.Text)
        If (fec1 > fec2) Then
            Response.Write("<script>alert('La fecha inicio no puede ser mayor a fecha fin')</script>")
            Me.txtHoraFin.Text = ""
            Return False
        End If

        Return True
    End Function

    Private Sub CargaGrid()
        Dim obj As New ClsConectarDatos
        Dim dtProgramacion As New Data.DataTable
        Dim codigo_aev As Integer = Request.QueryString("codigo_aev")

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtProgramacion = obj.TraerDataTable("[EVE_BuscaActividadProgramacion]", codigo_aev)
        obj.CerrarConexion()
        Me.gvActividadProgramacion.DataSource = dtProgramacion
        Me.gvActividadProgramacion.DataBind()
        dtProgramacion.Dispose()
    End Sub

    Protected Sub gvActividadProgramacion_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvActividadProgramacion.RowDeleting
        If (Me.gvActividadProgramacion.Rows.Count > 0) Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("EVE_EliminaActividadProgramacion", Me.gvActividadProgramacion.DataKeys(e.RowIndex).Item(0))
            obj.CerrarConexion()

            CargaGrid()
        End If
    End Sub

End Class

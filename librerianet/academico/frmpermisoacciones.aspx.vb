
Partial Class frmpermisoacciones
    Inherits System.Web.UI.Page
    Public tbldpto As Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.txtFechaInicio.Attributes.Add("OnKeyDown", "return false")
            Me.txtFechaFin.Attributes.Add("OnKeyDown", "return false")
            If Request.QueryString("codigo_apl") = "" Then
                Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                ClsFunciones.LlenarListas(Me.dpModulo, obj.TraerDataTable("ConsultarAplicacionUsuario", 1, 0, 0, 0), "codigo_apl", "descripcion_apl", "--Seleccione el Módulo desarrollado")
                Me.dpModulo.Visible = True
                Me.lblmodulo.Visible = True
                Me.lblTitulo.Text = "Permisos para acciones en un proceso de Matrícula vigente"
            Else
                CargarPersonalAccesoModulo(Request.QueryString("codigo_apl"))
                Me.dpModulo.Visible = False
                Me.lblmodulo.Visible = False
            End If
        End If
    End Sub
    Private Sub CargarPersonalAccesoModulo(ByVal codigo_apl As Integer)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("BDUSATConnectionString").ConnectionString)

        ClsFunciones.LlenarListas(Me.dpPersonal, obj.TraerDataTable("ConsultarAplicacionUsuario", 3, codigo_apl, 0, 0), "codigo_uap", "nombreusuario", "--Seleccione el Personal")
    End Sub
    Protected Sub dpPersonal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpPersonal.SelectedIndexChanged
        If Me.dpPersonal.SelectedValue <> -1 Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

            ClsFunciones.LlenarListas(Me.dpMenu, obj.TraerDataTable("ValidarPermisoAccionesEnProcesoMatricula", 1, 0, 0, 0), "nombretbl_acr", "descripcion_tacr", "--Seleccione el Menú de opciones")
        Else
            Me.dpMenu.Items.Clear()
        End If
    End Sub

    Protected Sub dpModulo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpModulo.SelectedIndexChanged
        CargarPersonalAccesoModulo(Me.dpModulo.SelectedValue)
    End Sub
    Protected Sub dpMenu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpMenu.SelectedIndexChanged
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("BDUSATConnectionString").ConnectionString)
        Dim tbl As New Data.DataTable

        tbl = obj.TraerDataTable("ValidarPermisoAccionesEnProcesoMatricula", 2, 0, dpPersonal.SelectedValue, dpMenu.SelectedValue)

        If tbl.Rows.Count > 0 Then
            Me.txtFechaInicio.Text = CDate(tbl.Rows(0).Item("fechaini_acr")).ToShortDateString
            Me.txtFechaFin.Text = CDate(tbl.Rows(0).Item("fechafin_acr")).ToShortDateString

            If tbl.Rows(0).Item("consultar_acr") = True Then
                Me.chkAcciones.Items(0).Selected = True
            End If
            If tbl.Rows(0).Item("agregar_acr") = True Then
                Me.chkAcciones.Items(1).Selected = True
            End If
            If tbl.Rows(0).Item("modificar_acr") = True Then
                Me.chkAcciones.Items(2).Selected = True
            End If
            If tbl.Rows(0).Item("eliminar_acr") = True Then
                Me.chkAcciones.Items(3).Selected = True
            End If
        Else
            Me.txtFechaInicio.Text = ""
            Me.txtFechaFin.Text = ""
            Me.chkAcciones.Items(0).Selected = False
            Me.chkAcciones.Items(1).Selected = False
            Me.chkAcciones.Items(2).Selected = False
            Me.chkAcciones.Items(3).Selected = False
        End If
        Me.Panel1.Visible = True
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("BDUSATConnectionString").ConnectionString)

        Try
            obj.IniciarTransaccion()
            obj.Ejecutar("AgregarAccesoRecurso", "S", 471, Me.dpPersonal.SelectedValue, Me.dpMenu.SelectedValue, 0, Me.chkAcciones.Items(0).Selected, Me.chkAcciones.Items(1).Selected, Me.chkAcciones.Items(2).Selected, Me.chkAcciones.Items(3).Selected, CDate(Me.txtFechaInicio.Text).ToShortDateString, CDate(Me.txtFechaFin.Text).ToShortDateString)
            obj.TerminarTransaccion()
            Response.Write("<script>alert('Se han guardado correctamente los datos')</script>")
        Catch
            obj.AbortarTransaccion()
            obj = Nothing
            Response.Write("Ha ocurrido un error al guardar")
        End Try
    End Sub
End Class

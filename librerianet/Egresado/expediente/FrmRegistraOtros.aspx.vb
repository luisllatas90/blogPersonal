Partial Class FrmRegistraOtros
    Inherits System.Web.UI.Page
    Public Editar As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack = True Then
            cargarDatosPagina()
        End If
        If Page.Request.QueryString("mod4") IsNot Nothing Then
            btnActualizar_Click(decode(Page.Request.QueryString("mod4")))
        End If
        Me.PanelDJ.Visible = False
        cargarJS()
    End Sub

    Sub cargarDatosPagina()
        CargarCentroestudio()
        Me.dpAnioIngreso.Items.Add("AÑO")
        Me.dpAnioEgreso.Items.Add("AÑO")
        For i As Integer = (Now.Year - 0) To 1960 Step -1
            Me.dpAnioIngreso.Items.Add(i.ToString)
            Me.dpAnioEgreso.Items.Add(i.ToString)
        Next
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("Alumni_AceptoDeclaracionJurada", CInt(Session("codigo_pso")), CInt(Session("codigo_alu")))
        If dt.Rows(0).Item("id") = "0" Then
            Me.lblnombre.Text = dt.Rows(0).Item("nombreEgresado")
            Session("codigo_pso") = dt.Rows(0).Item("codigo_pso")
            Me.PanelDJ.Visible = True
            Me.PanelRegistro.Visible = False
        Else
            registrar()
        End If
    End Sub
    Sub registrar()
        Try
            If validar() Then

                If codigo_FA.Value <> "0" Then
                    btnBorrar_Click(decode(Me.codigo_FA.Value))
                    Me.codigo_FA.Value = "0"
                End If

                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                Dim estado As String
                Dim fechaEgreso As String

                If CheckBox1.Checked Then
                    estado = "EN PROCESO"
                    fechaEgreso = "-"
                Else
                    estado = "CULMINADO"
                    fechaEgreso = Me.dpAnioEgreso.SelectedValue.ToString
                End If
                Dim fechagrad As String = ""

                Dim instit = Me.dptipoCE.SelectedValue.ToString
                Dim tipoInst = Me.txtcentro.Text.Trim.ToUpper ' Me.dpCE.SelectedValue.ToString

                obj.Ejecutar("ALUMNI_RegistrarFormacionAcademica", Session("codigo_alu"), Me.dpAnioIngreso.SelectedValue.ToString, fechaEgreso, fechagrad, Me.txtGrado.Text, tipoInst, instit, "", "", "O", "")
                obj.CerrarConexion()

                limpiar()
                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "parent.jQuery.fancybox.close();", True)
                ClientScript.RegisterStartupScript(Me.GetType, "nextpage", "parent.location.reload();", True)

            End If
        Catch ex As Exception
            Response.Write("<script>alert('ERROR');</script>")
        End Try
    End Sub
    Function validar() As Boolean
        Dim mensaje As String = ""
        If dpAnioIngreso.SelectedValue = "AÑO" Then
            mensaje &= "<br />Debe seleccionar año de ingreso."
        End If

        If Not Me.CheckBox1.Checked Then
            If dpAnioEgreso.SelectedValue = "AÑO" Then
                mensaje &= "<br />Debe seleccionar año de egreso."
            End If
        End If
        If dpAnioIngreso.SelectedValue <> "AÑO" And dpAnioEgreso.SelectedValue <> "AÑO" Then
            If CInt(dpAnioEgreso.SelectedValue) < CInt(dpAnioIngreso.SelectedValue) Then
                mensaje &= "<br />Fecha Egreso no puede ser menor que Fecha Ingreso"
            End If
        End If
        If Me.txtGrado.Text.Trim = "" Then
            mensaje &= "</br>Debe ingresar el grado obtenido."
        End If
        If mensaje <> "" Then
            lblMensajeFrm.Text = mensaje
            Return False
        Else
            lblMensajeFrm.Text = ""
            Return True
        End If
    End Function

    Sub limpiar()
        Dim mensaje As String = ""
        dpAnioIngreso.SelectedValue = "AÑO"
        dpAnioEgreso.SelectedValue = "AÑO"
        txtGrado.Text = ""
        Me.lblMensajeFrm.Text = ""
    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Me.dpAnioEgreso.Visible = Not Me.CheckBox1.Checked
        Me.Label1.Visible = Not Me.CheckBox1.Checked
    End Sub

    Sub btnActualizar_Click(ByVal codigo_FA As Integer)
        If Not IsPostBack = True Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim tbl As Data.DataTable
            tbl = obj.TraerDataTable("[ALUMNI_ConsultarAlumni_FormacionAcademicaxID]", codigo_FA)
            If tbl.Rows.Count Then
                Me.dpAnioIngreso.SelectedValue = -1
                If (tbl.Rows(0).Item("aingreso") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("aingreso").ToString.Trim <> "") Then
                    Me.dpAnioIngreso.SelectedValue = tbl.Rows(0).Item("aingreso").ToString.ToUpper
                End If

                If tbl.Rows(0).Item("aegreso").ToString = "-" Then
                    Me.CheckBox1.Checked = True
                Else
                    Me.CheckBox1.Checked = False
                    Me.dpAnioEgreso.Visible = True
                    Me.Label1.Visible = True
                    Me.dpAnioEgreso.SelectedValue = tbl.Rows(0).Item("aegreso").ToString.ToUpper
                End If

                Dim fechas() As String
                fechas = Split(tbl.Rows(0).Item("FechaActo"), "/")
                Me.txtGrado.Text = tbl.Rows(0).Item("GradoObtenido")

                Me.dptipoCE.SelectedValue = -1
                If (tbl.Rows(0).Item("TInstitucion") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("TInstitucion").ToString.Trim <> "") Then
                    Me.dptipoCE.SelectedValue = tbl.Rows(0).Item("TInstitucion").ToString.ToUpper
                End If

                'Me.dpCE.SelectedValue = -1
                'If (tbl.Rows(0).Item("Institucion") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("Institucion").ToString.Trim <> "") Then
                'Me.dpCE.SelectedValue = 
                ' End If
                Me.txtcentro.Text = tbl.Rows(0).Item("Institucion").ToString.ToUpper
                Me.codigo_FA.Value = encode(codigo_FA)
            End If
        End If
    End Sub

    Sub CargarCentroestudio()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ClsFunciones.LlenarListas(Me.dptipoCE, obj.TraerDataTable("ALUMNI_ConsultarDatosIdioma", "T"), "tipoCE", "tipoCE", "--Seleccione--")
        'ClsFunciones.LlenarListas(Me.dpCE, obj.TraerDataTable("ALUMNI_ConsultarDatosIdioma", "C"), "nombre", "nombre", "--Seleccione--")
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Function encode(ByVal str As String) As String
        Return (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(str)))
    End Function

    Function decode(ByVal str As String) As String
        Return System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(str))
    End Function

    Sub btnBorrar_Click(ByVal codigo_FA As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("ALUMNI_EliminarFormacion", codigo_FA)
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    'Protected Sub cmdGuardar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar1.Click
    '    ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "parent.jQuery.fancybox.close();", True)
    '    ClientScript.RegisterStartupScript(Me.GetType, "nextpage", "parent.location.reload();", True)
    'End Sub

    Protected Sub btnAcepta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAcepta.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("ALUMNI_InsertaBitacoraUpdateDatos", CInt(Session("codigo_pso")), "DJ")
        obj.CerrarConexion()
        obj = Nothing
        registrar()
    End Sub

    Protected Sub btnNoAcepta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNoAcepta.Click
        ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "parent.jQuery.fancybox.close();", True)
        ClientScript.RegisterStartupScript(Me.GetType, "nextpage", "parent.location.reload();", True)
    End Sub

    Function crearJS(ByVal sp As String, ByVal ctrl As String) As String
        Dim cadena As String = ""
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable(sp, "C")
        obj.CerrarConexion()
        If dt.Rows.Count Then
            cadena = "$(function() { var availableTags = ["""
            For i As Integer = 0 To dt.Rows.Count - 1

                cadena &= dt.Rows(i).Item("nombre").ToString & ""","""
            Next
            cadena = cadena.Substring(0, cadena.Length - 2)
            cadena &= "]; $( ""#" & ctrl & """ ).autocomplete({ source: availableTags });});"
        End If
        Return cadena
    End Function
    Sub cargarJS()
        Dim ruta As String = ""
        ruta = Server.MapPath("..\") & "\jquery\centros.js"
        System.IO.File.Delete(ruta)
        System.IO.File.AppendAllText(ruta, crearJS("ALUMNI_ConsultarDatosIdioma", "txtcentro"))
    End Sub

End Class
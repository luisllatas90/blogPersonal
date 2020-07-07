Partial Class FrmRegistraidioma
    Inherits System.Web.UI.Page
    Public Editar As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack = True Then
            cargarDatosPagina()
        End If
        If Page.Request.QueryString("mod3") IsNot Nothing Then
            btnActualizar_Click(decode(Page.Request.QueryString("mod3")))
        End If
        Me.PanelDJ.Visible = False
        cargarJS()
    End Sub

    Sub cargarDatosPagina()
        CargarDatosidioma()
        Me.dpAnioIngreso.Items.Add("AÑO")
        Me.dpAnioEgreso.Items.Add("AÑO")
        For i As Integer = (Now.Year - 0) To 1960 Step -1
            Me.dpAnioIngreso.Items.Add(i.ToString)
            Me.dpAnioEgreso.Items.Add(i.ToString)
        Next
    End Sub

    Sub CargarDatosidioma()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ClsFunciones.LlenarListas(Me.dpidioma, obj.TraerDataTable("ALUMNI_ConsultarDatosidioma", "I"), "idioma", "idioma", "--Seleccione--")
        ClsFunciones.LlenarListas(Me.dptipoCE, obj.TraerDataTable("ALUMNI_ConsultarDatosidioma", "T"), "tipoCE", "tipoCE", "--Seleccione--")
        'ClsFunciones.LlenarListas(Me.dpCE, obj.TraerDataTable("ALUMNI_ConsultarDatosidioma", "C"), "nombre", "nombre", "--Seleccione--")
        obj.CerrarConexion()
        obj = Nothing
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
                Dim anioIngreso As String
                Dim estado As String
                Dim anioEgreso As String
                Dim idioma As String

                anioIngreso = Me.dpAnioIngreso.SelectedValue.ToString

                If CheckBox1.Checked Then
                    estado = "EN PROCESO"
                    anioEgreso = "-"
                Else
                    estado = "CULMINADO"
                    anioEgreso = Me.dpAnioEgreso.SelectedValue.ToString
                End If

                '--------------------------------------------------------------------
                If (Me.dpidioma.SelectedValue = "-1") Then
                    idioma = "idioma"
                Else
                    idioma = Me.dpidioma.SelectedValue
                End If

                Dim tipoCE As String
                If (Me.dptipoCE.SelectedValue() = "-1") Then
                    tipoCE = "tipo Centro Estudios"
                Else
                    tipoCE = Me.dptipoCE.SelectedValue()
                End If

                Dim CE As String
                'If (Me.dpCE.SelectedValue() = "-1") Then
                '    CE = "Centro de Estudios"
                'Else
                '    CE = Me.dpCE.SelectedValue()
                'End If
                CE = Me.txtcentro.Text.Trim.ToString.ToUpper
                '--------------------------------------------------------------------

                'Dim Situacion As String = Me.dpSituacion.SelectedValue().ToString

                Dim Lectura As String = Me.dpNivel1.SelectedValue()
                Dim Escritura As String = Me.dpNivel2.SelectedValue()
                Dim Habla As String = Me.dpNivel3.SelectedValue()
                Dim NivelLEH As String = Lectura & "-" & Escritura & "-" & Habla

                obj.Ejecutar("ALUMNI_RegistrarFormacionAcademica", Session("codigo_alu"), anioIngreso, anioEgreso, "-", idioma, CE, tipoCE, "-", estado, "I", NivelLEH)
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

        If dpidioma.SelectedValue = "-1" Then
            mensaje &= "<br />Debe seleccionar idioma."
        End If

        If Me.txtcentro.Text.Trim = "" Then
            mensaje &= "<br />Debe ingresar Centro de estudios."
        End If

        If dptipoCE.SelectedValue = "-1" Then
            mensaje &= "<br />Debe seleccionar Tipo de Institución."
        End If

        If dpNivel1.SelectedValue = "-1" Then
            mensaje &= "<br />Debe seleccionar Nivel de Lectura."
        End If
        If dpNivel2.SelectedValue = "-1" Then
            mensaje &= "<br />Debe seleccionar Nivel de Escritura."
        End If
        If dpNivel3.SelectedValue = "-1" Then
            mensaje &= "<br />Debe seleccionar Nivel de Habla."
        End If
        'If dpSituacion.SelectedValue = "-1" Then
        '    mensaje &= "<br />Debe seleccionar Situacion."
        'End If
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
        'dpidioma.SelectedValue = ""
        'dpCE.SelectedValue = ""
        'dptipoCE.SelectedValue = ""
        dpNivel1.SelectedValue = ""
        dpNivel2.SelectedValue = ""
        dpNivel3.SelectedValue = ""
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

                Me.dpidioma.SelectedValue = -1
                If (tbl.Rows(0).Item("GradoObtenido") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("GradoObtenido").ToString.Trim <> "") Then
                    Me.dpidioma.SelectedValue = tbl.Rows(0).Item("GradoObtenido").ToString.ToUpper
                End If

                Me.dptipoCE.SelectedValue = -1
                If (tbl.Rows(0).Item("TInstitucion") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("TInstitucion").ToString.Trim <> "") Then
                    Me.dptipoCE.SelectedValue = tbl.Rows(0).Item("TInstitucion").ToString.ToUpper
                End If

                'Me.dpCE.SelectedValue = -1
                'If (tbl.Rows(0).Item("Institucion") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("Institucion").ToString.Trim <> "") Then
                '    Me.dpCE.SelectedValue = 
                'End If
                Me.txtcentro.Text = tbl.Rows(0).Item("Institucion").ToString.ToUpper

                Dim Nivel() As String
                Nivel = Split(tbl.Rows(0).Item("NIVEL"), "-")
                Me.dpNivel1.SelectedValue = Nivel(0)                                          'Lectura
                Me.dpNivel2.SelectedValue = Nivel(1)                                          'Escritura
                Me.dpNivel3.SelectedValue = Nivel(2)                                          'Habla

                'Me.dpSituacion.SelectedValue = -1
                'If (tbl.Rows(0).Item("SITUACION") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("SITUACION").ToString.Trim <> "") Then
                '    Me.dpSituacion.SelectedValue = tbl.Rows(0).Item("SITUACION").ToString.ToUpper
                'End If
                Me.codigo_FA.Value = encode(codigo_FA)
            End If
        End If
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


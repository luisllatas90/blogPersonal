Partial Class FormacionAcademicaRegistrar 'GRADO ACADÉMICO
    Inherits System.Web.UI.Page
    Public Editar As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            cargarDatosPagina()
            If Page.Request.QueryString("mod1") IsNot Nothing Then
                btnActualizar_Click(decode(Page.Request.QueryString("mod1")))
            End If
            Me.PanelDJ.Visible = False
            cargarJS()
        Catch ex As Exception
            response.write(Ex.message)
        End Try
        
    End Sub

    Sub cargarDatosPagina()
        Me.dpAnioIngreso.Items.Add("AÑO")
        Me.dpAnioEgreso.Items.Add("AÑO")
        Me.DDLAnio.Items.Add("AÑO")
        For i As Integer = (Now.Year - 0) To 1960 Step -1
            Me.dpAnioIngreso.Items.Add(i.ToString)
            Me.dpAnioEgreso.Items.Add(i.ToString)
            Me.DDLAnio.Items.Add(i.ToString)
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
                Dim diagrad As String = Me.dpdiagrad.SelectedValue()
                Dim mesgrad As String = Me.dpmesgrad.SelectedValue()
                Dim aniograd As String = Me.DDlAnio.SelectedValue.ToString
                Dim fechagrad As String

                If CheckBox1.Checked Then
                    estado = "EN PROCESO"
                    fechaEgreso = "-"
                    fechagrad = "-"
                Else
                    estado = "CULMINADO"
                    fechaEgreso = Me.dpAnioEgreso.SelectedValue.ToString
                    If diagrad <> "-1" And mesgrad <> "-1" And aniograd <> "AÑO" Then
                        fechagrad = diagrad & "/" & mesgrad & "/" & aniograd
                    Else
                        fechagrad = "-"
                    End If
                End If
                
                obj.Ejecutar("ALUMNI_RegistrarFormacionAcademica", Session("codigo_alu"), Me.dpAnioIngreso.SelectedValue.ToString, fechaEgreso, fechagrad, Me.txtGrado.Text, Me.txtUniversidad.Text, "", Me.dpProcedencia.SelectedValue.ToString, estado, "G", "")
                obj.CerrarConexion()

                limpiar()
                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "parent.jQuery.fancybox.close();", True)
                ClientScript.RegisterStartupScript(Me.GetType, "nextpage", "parent.location.reload();", True)

            End If
        Catch ex As Exception
            Response.Write("<br /> error" & ex.Message & " -  " & ex.StackTrace)
            'Response.Write("<script>alert('ERROR');</script>")
        End Try
    End Sub
    Function validar() As Boolean
        Dim mensaje As String = ""

        'If dpmesgrad.SelectedIndex = 0 Then
        '    mensaje &= "<br />Debe seleccionar mes de graduación."
        'End If
        If dpAnioIngreso.SelectedValue = "AÑO" Then
            mensaje &= "<br />Debe seleccionar año de ingreso."
        End If

        If Not Me.CheckBox1.Checked Then
            'If dpMFin.SelectedIndex = 0 Then
            '    mensaje &= "<br />Debe seleccionar mes de fin."
            'End If
            If dpAnioEgreso.SelectedValue = "AÑO" Then
                mensaje &= "<br />Debe seleccionar año de egreso."
            End If
        End If

        If dpAnioIngreso.SelectedValue <> "AÑO" And dpAnioEgreso.SelectedValue <> "AÑO" Then
            If CInt(dpAnioEgreso.SelectedValue) < CInt(dpAnioIngreso.SelectedValue) Then
                mensaje &= "<br />Fecha Egreso no puede ser menor que Fecha Ingreso"
            End If
        End If

        If dpdiagrad.SelectedValue() <> "-1" Or dpmesgrad.SelectedValue() <> "-1" Then
            If dpAnioEgreso.SelectedValue <> "AÑO" And DDLAnio.SelectedValue <> "AÑO" Then
                If CInt(DDLAnio.SelectedValue) < CInt(dpAnioEgreso.SelectedValue) Then
                    mensaje &= "<br />La Fecha de Graduación no puede ser menor que Fecha Egreso"
                End If
            End If
        End If
        
        If Me.txtGrado.Text.Trim = "" Then
            mensaje &= "</br>Debe ingresar el grado obtenido."
        End If

        If Me.txtUniversidad.Text.Trim = "" Then
            mensaje &= "<br />Debe ingresar el nombre de la Universidad o institución Educativa."
        End If

        If dpProcedencia.SelectedValue = "-1" Then
            mensaje &= "<br />Debe seleccionar procedencia."
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
        dpmesgrad.SelectedIndex = 0
        dpAnioIngreso.SelectedValue = "AÑO"
        dpAnioEgreso.SelectedValue = "AÑO"
        DDLAnio.SelectedValue = "AÑO"
        txtUniversidad.Text = ""
        txtGrado.Text = ""
        'dpProcedencia.SelectedValue = "seleccione"
        Me.lblMensajeFrm.Text = ""
    End Sub
    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        'Me.dpMFin.Visible = Not Me.CheckBox1.Checked
        Me.dpdiagrad.Visible = Not Me.CheckBox1.Checked
        Me.dpmesgrad.Visible = Not Me.CheckBox1.Checked
        Me.DDLanio.Visible = Not Me.CheckBox1.Checked

        Me.dpAnioEgreso.Visible = Not Me.CheckBox1.Checked
        Me.Label1.Visible = Not Me.CheckBox1.Checked
        Me.LblFG.Visible = Not Me.CheckBox1.Checked
        Me.Lblopcional.Visible = Not Me.CheckBox1.Checked
    End Sub
    'Protected Sub btnActualizar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    Sub btnActualizar_Click(ByVal codigo_FA As Integer)
        If Not IsPostBack = True Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim tbl As Data.DataTable
            tbl = obj.TraerDataTable("ALUMNI_ConsultarAlumni_FormacionAcademicaxID", codigo_FA)
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
                    Me.LblFG.Visible = True
                    Me.Lblopcional.Visible = True
                    Me.dpAnioEgreso.SelectedValue = tbl.Rows(0).Item("aegreso").ToString.ToUpper

                    Me.dpdiagrad.Visible = True
                    Me.dpmesgrad.Visible = True
                    Me.DDLanio.Visible = True

                End If

                If tbl.Rows(0).Item("FechaActo").ToString = "-" Then
                Else
                    Dim fechas() As String
                    fechas = Split(tbl.Rows(0).Item("FechaActo"), "/")
                    Me.dpdiagrad.SelectedValue = fechas(0)                                          'DIA
                    Me.dpmesgrad.SelectedValue = fechas(1)                                          'MES

                    If IsDate(tbl.Rows(0).Item("FechaActo").ToString) Then
                        Me.DDlAnio.SelectedValue = Year(tbl.Rows(0).Item("FechaActo").ToString)     'AÑO 
                    End If
                End If

                Me.txtGrado.Text = tbl.Rows(0).Item("GradoObtenido")
                Me.txtUniversidad.Text = tbl.Rows(0).Item("Institucion")

                If tbl.Rows(0).Item("PROCEDENCIA").ToString <> "" Then
                    Me.dpProcedencia.SelectedValue = tbl.Rows(0).Item("PROCEDENCIA").ToString
                Else
                    Me.dpProcedencia.SelectedValue = -1
                End If
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
        dt = obj.TraerDataTable(sp)
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
        ruta = Server.MapPath("..\") & "\jquery\universidades.js"
        System.IO.File.Delete(ruta)
        System.IO.File.AppendAllText(ruta, crearJS("ALUMNI_ListarUniversidad", "txtUniversidad"))        
    End Sub
End Class


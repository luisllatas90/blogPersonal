Partial Class ExperienciaLaboralRegistrar
    Inherits System.Web.UI.Page
    Public Editar As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            cargarDatosPagina()
            If Page.Request.QueryString("mod") IsNot Nothing Then
                btnActualizar_Click(decode(Page.Request.QueryString("mod")))
            End If
            'cargarJS()
            Me.PanelDJ.Visible = False

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub
    Sub cargarDatosPagina()
        Me.dpAñoInicio.Items.Add("AÑO")
        Me.dpAñoFin.Items.Add("AÑO")
        For i As Integer = (Now.Year - 0) To 1960 Step -1
            Me.dpAñoInicio.Items.Add(i.ToString)
            Me.dpAñoFin.Items.Add(i.ToString)
        Next
        'cargarJS()
    End Sub

    Function retornarEmpresa(ByVal cad As String) As String
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As Data.DataTable
        tb = obj.TraerDataTable("ALUMNI_ListarEmpresas", cad)
        obj.CerrarConexion()
        obj = Nothing
        If tb.Rows.Count Then
            Return tb.Rows(0).Item(0).ToString
        Else
            Return "0"
        End If
    End Function

    Function retornarSector(ByVal cad As String, Optional ByVal idpro As Integer = 0, Optional ByVal cadProveedor As String = "") As String
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As Data.DataTable
        tb = obj.TraerDataTable("ALUMNI_ListarSector", cad, cadProveedor)
        If tb.Rows.Count Then
            If tb.Rows(0).Item("idpro").ToString = "0" And tb.Rows.Count = 1 Then
                obj.Ejecutar("Alumni_ActualizarProveedorSector", idpro, tb.Rows(0).Item("codigo_sec").ToString)
            End If
            obj.CerrarConexion()
            obj = Nothing
            Return tb.Rows(0).Item("codigo_sec").ToString
        Else
            obj.CerrarConexion()
            obj = Nothing
            Return "0"
        End If
    End Function

    Function retornarDistrito(ByVal cad As String) As String
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As Data.DataTable
        tb = obj.TraerDataTable("ALUMNI_ListarCiudades", cad)
        obj.CerrarConexion()
        obj = Nothing
        If tb.Rows.Count Then
            Return tb.Rows(0).Item(0).ToString
        Else
            Return "0"
        End If
    End Function

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

                If codigo_Exp.Value <> "0" Then
                    btnBorrar_Click(decode(Me.codigo_Exp.Value))
                    Me.codigo_Exp.Value = "0"
                End If

                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                Dim estado As String
                Dim fechaFin As String
                Dim empresa As Integer
                Dim ciudad As Integer = 0
                Dim sector As Integer

                If CheckBox1.Checked Then
                    estado = "ACTUAL"
                    fechaFin = "-"
                Else
                    estado = "TERMINADO"
                    fechaFin = Me.dpMFin.SelectedValue.ToString & "/" & Me.dpAñoFin.SelectedValue.ToString
                End If

                empresa = CInt(retornarEmpresa(Me.txtinstitucion.Text.Trim))
                sector = CInt(retornarSector(Me.txtSector.Text.Trim, empresa, Me.txtinstitucion.Text.Trim))
                Dim cd() As String
                cd = Split(Me.txtciudad.Text.Trim, ",")


                If retornarEmpresa(Me.txtinstitucion.Text.Trim) = "0" Then
                    'registrar nueva empresa
                    Dim otb As Data.DataTable
                    otb = obj.TraerDataTable("ALUMNI_RegistrarEmpresa", Me.txtinstitucion.Text.Trim)
                    If otb.Rows.Count Then
                        If (CInt(otb.Rows(0).Item(0).ToString)) > 0 Then
                            empresa = CInt(otb.Rows(0).Item(0).ToString)
                        End If
                    End If
                End If

                If sector = "0" Then
                    'registrar nuevo sector
                    Dim otb As Data.DataTable
                    otb = obj.TraerDataTable("ALUMNI_RegistrarSector", Me.txtSector.Text.Trim, empresa)
                    If otb.Rows.Count Then
                        If (CInt(otb.Rows(0).Item(0).ToString)) > 0 Then
                            sector = CInt(otb.Rows(0).Item(0).ToString)
                        End If
                    End If
                End If
                Dim txtLugarExtranjero As String = DBNull.Value.ToString
                Dim reg As Boolean = False

                If Me.chkExtranjero.Checked Then
                    If Me.txtlugarextranjero.Text.Trim.Length > 0 Then
                        reg = True
                        txtLugarExtranjero = Me.txtlugarextranjero.Text.Trim
                    Else
                        ClientScript.RegisterStartupScript(Me.GetType, "Mensaje", "<script>alert('Debe escribir la ubicación del trabajo realizado en el extranjero')</script>")
                    End If
                Else
                    ciudad = CInt(retornarDistrito(cd(0)))
                    If ciudad <> 0 Then
                        reg = True
                        txtLugarExtranjero = Me.txtlugarextranjero.Text.Trim
                    Else
                        ClientScript.RegisterStartupScript(Me.GetType, "Mensaje", "<script>alert('Debe elegir un lugar de trabajo de la lista')</script>")
                    End If
                End If

                If reg Then
                    obj.Ejecutar("ALUMNI_RegistrarExperienciaLaboral", Session("codigo_alu"), _
                                 Me.dpMInicio.SelectedValue.ToString & "/" & Me.dpAñoInicio.SelectedValue.ToString, _
                                fechaFin, estado, empresa, ciudad, Me.txtdescripcion.Value.ToString.Trim, sector, _ 
                                Me.txtarea.Text.Trim, Me.txtcargo.Text.Trim, Me.txttipocontrato.Text.Trim, txtLugarExtranjero)                     

                    obj.CerrarConexion()
                    limpiar()
                    ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "parent.jQuery.fancybox.close();", True)
                    ClientScript.RegisterStartupScript(Me.GetType, "nextpage", "parent.location.reload();", True)                
                End If

            End If
        Catch ex As Exception
            Response.Write("<br /> error" & ex.Message & " -  " & ex.StackTrace)
            'Response.Write("<script>alert('ERROR');</script>")
        End Try
    End Sub

    Function validar() As Boolean
        Dim mensaje As String = ""

        If dpMInicio.SelectedIndex = 0 Then
            mensaje &= "<br />Debe seleccionar mes de inicio."
        End If
        If dpAñoInicio.SelectedValue = "AÑO" Then
            mensaje &= "<br />Debe seleccionar año de inicio."
        End If

        If Not Me.CheckBox1.Checked Then
            If dpMFin.SelectedIndex = 0 Then
                mensaje &= "<br />Debe seleccionar mes de fin."
            End If
            If dpAñoFin.SelectedValue = "AÑO" Then
                mensaje &= "<br />Debe seleccionar año de fin."
            End If
        End If
        If dpAñoInicio.SelectedValue <> "AÑO" And dpAñoFin.SelectedValue <> "AÑO" Then
            If CInt(dpAñoFin.SelectedValue) < CInt(dpAñoInicio.SelectedValue) Then
                mensaje &= "<br />Fecha Fin no puede ser menor que Fecha Inicio"
            End If
        End If
        If Me.txtinstitucion.Text.Trim = "" Then
            mensaje &= "<br />Debe ingresar el nombre de la institución."
        End If

        If Me.chkExtranjero.Checked Then
            If Me.txtlugarextranjero.Text.Trim = "" Then
                mensaje &= "<br />Debe ingresar el lugar en el extranjero"
            End If
        Else
            If Me.txtciudad.Text.Trim = "" Then
                mensaje &= "<br />Debe ingresar la ciudad."
            End If
        End If

        If Me.txtdescripcion.Value.ToString.Trim = "" Then
            mensaje &= "</br>Debe ingresar una breve descripción del cargo desempeñado."
        End If
        If Me.txtsector.Text.Trim = "" Then
            mensaje &= "</br>Debe ingresar el sector de la empresa."
        End If
        If Me.txtarea.Text.Trim = "" Then
            mensaje &= "</br>Debe ingresar el área."
        End If
        If Me.txtcargo.Text.Trim = "" Then
            mensaje &= "</br>Debe ingresar el cargo desempeñado"
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
        dpMInicio.SelectedIndex = 0
        dpAñoInicio.SelectedValue = "AÑO"
        dpAñoFin.SelectedValue = "AÑO"
        txtinstitucion.Text = ""
        txtciudad.Text = ""
        Me.txtdescripcion.Value = ""
        txtsector.Text = ""
        txtarea.Text = ""
        txtcargo.Text = ""
        Me.lblMensajeFrm.Text = ""
    End Sub


    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Me.dpMFin.Visible = Not Me.CheckBox1.Checked
        Me.dpAñoFin.Visible = Not Me.CheckBox1.Checked
        Me.Label1.Visible = Not Me.CheckBox1.Checked
    End Sub
    Sub cargarJS()
        Dim ruta As String = ""
        ruta = Server.MapPath("..\") & "\jquery\empresas.js"
        Dim fileCreatedDate As DateTime = System.IO.File.GetLastWriteTime(ruta)
        fileCreatedDate.Date.ToShortDateString()
        If Date.Today.ToShortDateString <> fileCreatedDate.Date.ToShortDateString() Then
            System.IO.File.Delete(ruta)
            System.IO.File.AppendAllText(ruta, crearJS("ALUMNI_ListarEmpresas", "txtinstitucion"))
            ruta = Server.MapPath("..\") & "\jquery\ciudades.js"
            System.IO.File.Delete(ruta)
            System.IO.File.AppendAllText(ruta, crearJS("ALUMNI_ListarCiudades", "txtciudad"))
            ruta = Server.MapPath("..\") & "\jquery\sectores.js"
            System.IO.File.Delete(ruta)
            System.IO.File.AppendAllText(ruta, crearJS("ALUMNI_ListarSector", "txtSector"))
            ruta = Server.MapPath("..\") & "\jquery\areas.js"
            System.IO.File.Delete(ruta)
            System.IO.File.AppendAllText(ruta, crearJS("ALUMNI_ListarAreas", "txtarea"))
            ruta = Server.MapPath("..\") & "\jquery\cargos.js"
            System.IO.File.Delete(ruta)
            System.IO.File.AppendAllText(ruta, crearJS("ALUMNI_ListarCargo", "txtcargo"))
            ruta = Server.MapPath("..\") & "\jquery\tipocontrato.js"
            System.IO.File.Delete(ruta)
            System.IO.File.AppendAllText(ruta, crearJS("ALUMNI_ListarTipoContrato", "txttipocontrato"))
        End If
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
                cadena &= dt.Rows(i).Item("nombre").ToString.Trim & ""","""
            Next
            cadena = cadena.Substring(0, cadena.Length - 2)
            cadena &= "]; $( ""#" & ctrl & """ ).autocomplete({ source: availableTags });});"
        End If
        Return cadena
    End Function
    'Protected Sub btnActualizar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
    Sub btnActualizar_Click(ByVal codigo_Exp As Integer)
        If Not IsPostBack = True Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim tbl As Data.DataTable
            tbl = obj.TraerDataTable("[ALUMNI_ConsultarAlumni_ExperienciaLaboralxID]", codigo_Exp)
            If tbl.Rows.Count Then
                Dim fechas() As String
                fechas = Split(tbl.Rows(0).Item("fecha_inicio"), "/")
                Me.dpMInicio.SelectedValue = fechas(0)
                Me.dpAñoInicio.SelectedValue = fechas(1)
                If tbl.Rows(0).Item("fecha_fin").ToString = "-" Then
                    Me.CheckBox1.Checked = True
                Else
                    Me.CheckBox1.Checked = False
                    fechas = Split(tbl.Rows(0).Item("fecha_fin"), "/")
                    Me.dpMFin.SelectedValue = fechas(0)
                    Me.dpAñoFin.SelectedValue = fechas(1)
                    Me.dpMFin.Visible = True
                    Me.dpAñoFin.Visible = True
                    Me.Label1.Visible = True
                End If
                Me.txtinstitucion.Text = tbl.Rows(0).Item("empresa")
                Me.txtSector.Text = tbl.Rows(0).Item("sector")

                If tbl.Rows(0).Item("ciudad") = "0" Then
                    Me.txtciudad.Text = ""
                    Me.chkExtranjero.Checked = True
                    Me.txtciudad.Enabled = False
                    Me.txtlugarextranjero.Text = tbl.Rows(0).Item("lugarextranjero")
                    Me.txtlugarextranjero.Enabled = True

                Else
                    Me.txtciudad.Text = tbl.Rows(0).Item("ciudad")
                    Me.chkExtranjero.Checked = False
                    Me.txtciudad.Enabled = True
                    Me.txtlugarextranjero.Text = ""
                    Me.txtlugarextranjero.Enabled = False
                End If

                Me.txtdescripcion.Value = tbl.Rows(0).Item("descripción")
                Me.txtarea.Text = tbl.Rows(0).Item("area")
                Me.txtcargo.Text = tbl.Rows(0).Item("cargo")
                Me.txttipocontrato.Text = tbl.Rows(0).Item("tipocontrato")
                Me.codigo_Exp.Value = encode(codigo_Exp)
            End If
        End If
        
    End Sub

    Function encode(ByVal str As String) As String
        Return (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(str)))
    End Function

    Function decode(ByVal str As String) As String
        Return System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(str))
    End Function
    Sub btnBorrar_Click(ByVal codigo_exp As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("ALUMNI_EliminarExperiencia", codigo_exp)
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub cmdGuardar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar1.Click
        ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "parent.jQuery.fancybox.close();", True)
        ClientScript.RegisterStartupScript(Me.GetType, "nextpage", "parent.location.reload();", True)
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

    Protected Sub chkExtranjero_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkExtranjero.CheckedChanged
        Me.txtciudad.Enabled = Not Me.chkExtranjero.Checked
        Me.txtlugarextranjero.Enabled = Me.chkExtranjero.Checked
        If Me.txtlugarextranjero.Enabled Then Me.txtlugarextranjero.Focus()
    End Sub
End Class


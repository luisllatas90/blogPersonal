Partial Class frmpersona
    Inherits System.Web.UI.Page
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                CargarDatosPersonales()
                CargarDatosEgresado()
            End If
            LoadDJ()
        Catch ex As Exception
            Response.Redirect("sesion.aspx")
            'Response.Write(ex.Message & " -  " & ex.StackTrace)
        End Try

    End Sub
    Sub LoadDJ()
        If Request.Form("aceptaDj") IsNot Nothing Then
            If Request.Form("aceptaDj") = "NO" Then
                InsertaDJ()
                Registrar()
            End If
        End If
        VerificarDJ()
    End Sub

    Sub VerificarDJ()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("Alumni_AceptoDeclaracionJurada", Me.hdcodigo_pso.Value)

        If dt.Rows(0).Item("id") <> "0" Then
            Me.cmdGuardar.CssClass = "guardar"
            Me.aceptaDj.Attributes.Add("value", "SI")
        Else
            Me.lblnombre.Text = dt.Rows(0).Item("nombreEgresado")
            Me.aceptaDj.Attributes.Add("value", "NO")
            Me.cmdGuardar.CssClass = "guardar  CallFancyBox_DeclaracionJurada"
        End If

    End Sub
    Sub InsertaDJ()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("ALUMNI_InsertaBitacoraUpdateDatos", Me.hdcodigo_pso.Value, "DJ")
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Sub CargarDatosPersonales()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Dim tbl As Data.DataTable
        tbl = obj.TraerDataTable("ALUMNI_ConsultarDatosEgresado", Session("codigo_alu")) 
        If tbl.Rows.Count Then
            Me.hdcodigo_pso.Value = tbl.Rows(0).Item("codigo_Pso")
        End If
    End Sub

    Function validar() As Boolean
        Dim mensaje As String = ""
        Dim cadena1 As String = Me.txtperfil.Value

        If Me.txtperfil.Value.IndexOf("<") > -1 Then
            mensaje &= "</br> (<) es un caracter especial no permitido."
        End If

        If cadena1.IndexOf(">") > -1 Then
            mensaje &= "</br> (>) es un caracter especial no permitido."
        End If

        If cadena1.IndexOf("[") > -1 Then
            mensaje &= "</br> ([) es un caracter especial no permitido."
        End If

        If cadena1.IndexOf("]") > -1 Then
            mensaje &= "</br> (]) es un caracter especial no permitido."
        End If

        If cadena1.IndexOf("{") > -1 Then
            mensaje &= "</br> ({) es un caracter especial no permitido."
        End If

        If cadena1.IndexOf("}") > -1 Then
            mensaje &= "</br> (}) es un caracter especial no permitido."
        End If

        If cadena1.IndexOf("$") > -1 Then
            mensaje &= "</br> ($) es un caracter especial no permitido."
        End If

        If Me.txtperfil.Value = "" Then
            mensaje &= "</br> Favor de registrar su perfil Profesional para continuar."
        End If

        If cadena1.IndexOf("'") > -1 Then
            mensaje &= "</br> (') es un caracter especial no permitido."
        End If
        If cadena1.IndexOf("´") > -1 Then
            mensaje &= "</br> (´) es un caracter especial no permitido."
        End If

        If cadena1.IndexOf("\") > -1 Then
            mensaje &= "</br> (\) es un caracter especial no permitido."
        End If

        If cadena1.IndexOf("/") > -1 Then
            mensaje &= "</br> (/) es un caracter especial no permitido."
        End If

        If cadena1.IndexOf("?") > -1 Then
            mensaje &= "</br> (?) es un caracter especial no permitido."
        End If

        If cadena1.IndexOf("¿") > -1 Then
            mensaje &= "</br> (¿) es un caracter especial no permitido."
        End If

        If cadena1.IndexOf("*") > -1 Then
            mensaje &= "</br> (*) es un caracter especial no permitido."
        End If

        If cadena1.IndexOf("@") > -1 Then
            mensaje &= "</br> (@) es un caracter especial no permitido."
        End If

        If cadena1.IndexOf("#") > -1 Then
            mensaje &= "</br> (#) es un caracter especial no permitido."
        End If

        If mensaje <> "" Then
            lblMensajeFrm.Text = mensaje
            Return False
        Else
            lblMensajeFrm.Text = ""
            Return True
        End If
    End Function
    Sub CargarDatosEgresado()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Dim dt_Egresado As New Data.DataTable
        dt_Egresado = obj.TraerDataTable("ALUMNI_BuscaEgresado", Me.hdcodigo_pso.Value)
        obj.CerrarConexion()
        obj = Nothing

        If (dt_Egresado.Rows.Count > 0) Then

            Me.txtperfil.Value = dt_Egresado.Rows(0).Item("perfil_ega").ToString()

            dt_Egresado.Dispose()
        End If

    End Sub
    
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        registrar()
    End Sub

    Sub registrar()
        Try
            If validar() Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                '*********************************************************
                Dim Caracteres As String = "<>‘/´\¡!?#:$%[_*@{}'+"
                Dim Cadena As String = Me.txtperfil.Value

                'Quitar(Caracteres)
                'WHILE Cadena LIKE "%[" + @Caracteres + "]%"
                '    Select Cadena = REPLACE(Cadena, SUBSTRING(Cadena, PATINDEX("%[" + Caracteres + "]%", Cadena), 1), "")
                'End While

                '*********************************************************
                obj.Ejecutar("ALUMNI_ActualizaDatosEgre", Session("codigo_alu"), "P", "", Me.txtperfil.Value, "", "", "", "", "")
                    obj.CerrarConexion()
                'Response.Write("<script>alert('Perfil del Egresado Actualizada'); location.reload('experiencialaboral.aspx');</script>")
                ' Response.Write("<script>location.reload('experiencialaboral.aspx');</script>")
                Response.Redirect("experiencialaboral.aspx")
                    CargarDatosPersonales()
                    CargarDatosEgresado()
            End If
        Catch ex As Exception
            Response.Write("<br /> error" & ex.Message & " -  " & ex.StackTrace)
        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("datospersonales.aspx")
    End Sub
End Class


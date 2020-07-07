Partial Class IdiomasyOtros
    Inherits System.Web.UI.Page
    Public Editar As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                CargarIdioma()
                CargarOtrosEstudios()
            End If
            If Page.Request.QueryString("del") IsNot Nothing Then
                btnBorrar_Click(decode(Page.Request.QueryString("del")))
            End If
        Catch ex As Exception
            Response.Redirect("sesion.aspx")
            'Response.Write(ex.Message & " -  " & ex.StackTrace)
        End Try

    End Sub
    Sub CargarIdioma()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tbl3 As Data.DataTable
        Dim html3 As String = ""
        tbl3 = obj.TraerDataTable("ALUMNI_ConsultarAlumni_FormacionAcademica", Session("codigo_alu"), "I")
        Me.GridIdioma.InnerHtml = Session("codigo_alu")
        Dim classCss As String = ""
        Dim qs3 As String = ""
        If tbl3.Rows.Count Then
            html3 &= "<table class=""tabla"" cellspacing=""0"">"
            html3 &= "<tr><th>Año de Ingreso</th><th>Año de Egreso</th><th>Situación</th><th>Idioma</th><th>Tipo de Institución</th><th>Centro de Estudios</th><th>Nivel Lectura</th><th>Nivel Escritura</th><th>Nivel Habla</th><th>Editar</th><th>Borrar</th></tr>"
            For i As Integer = 0 To tbl3.Rows.Count - 1
                If i Mod 2 Then
                    classCss = "fila1"
                Else
                    classCss = "fila2"
                End If

                qs3 = encode(tbl3.Rows(i).Item("id").ToString)
                html3 &= "<tr class=""" & classCss & """>"
                html3 &= "<td>" & tbl3.Rows(i).Item("AnioIngreso").ToString & "</td>"

                html3 &= "<td>" & IIf(tbl3.Rows(i).Item("AnioEgreso").ToString = "0", "-", tbl3.Rows(i).Item("AnioEgreso").ToString) & "</td>"

                html3 &= "<td>" & tbl3.Rows(i).Item("Situacion").ToString & "</td>"
                html3 &= "<td>" & tbl3.Rows(i).Item("idioma").ToString & "</td>"
                html3 &= "<td>" & tbl3.Rows(i).Item("TipoInstitucion").ToString & "</td>"
                html3 &= "<td>" & tbl3.Rows(i).Item("CentroEstudios").ToString & "</td>"
                html3 &= "<td>" & tbl3.Rows(i).Item("NivelLectura").ToString & "</td>"
                html3 &= "<td>" & tbl3.Rows(i).Item("NivelEscritura").ToString & "</td>"
                html3 &= "<td>" & tbl3.Rows(i).Item("NivelHabla").ToString & "</td>"

                If tbl3.Rows(i).Item("Borrar").ToString = "1" Then
                    html3 &= "<td><a class=""ifancybox"" href=""FrmRegistraIdioma.aspx?mod3=" & qs3 & """><img src=""../archivos/editar.png"" alt=""Editar"" /></a></td>"
                    html3 &= "<td><a class="""" href=""?del=" & qs3 & """ onclick=""return confirm_delete();""><img src=""../archivos/borrar.png"" alt=""Borrar"" /></a></td>"
                Else
                    html3 &= "<td><a></a></td>"
                    html3 &= "<td><a></a></td>"
                End If               
                html3 &= "</tr>"

            Next
            html3 &= "</table>"
            GridIdioma.InnerHtml = html3
        Else
            GridIdioma.InnerHtml = "<br /><font style=""font-size:11px;""><i>No se ha registrado información</i></font>"
        End If
        tbl3.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Sub CargarOtrosEstudios()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tbl3 As Data.DataTable
        Dim html3 As String = ""
        tbl3 = obj.TraerDataTable("ALUMNI_ConsultarAlumni_FormacionAcademica", Session("codigo_alu"), "O")
        Me.GridOtros.InnerHtml = Session("codigo_alu")
        Dim classCss As String = ""
        Dim qs4 As String = ""
        If tbl3.Rows.Count Then
            html3 &= "<table class=""tabla"" cellspacing=""0"">"
            html3 &= "<tr><th>Año de Ingreso</th><th>Año de Egreso</th><th>Estudio realizado</th><th>Tipo de Institución</th><th>Centro de Estudios</th><th>Editar</th><th>Borrar</th></tr>"
            For i As Integer = 0 To tbl3.Rows.Count - 1
                If i Mod 2 Then
                    classCss = "fila1"
                Else
                    classCss = "fila2"
                End If

                qs4 = encode(tbl3.Rows(i).Item("id").ToString)
                html3 &= "<tr class=""" & classCss & """>"
                html3 &= "<td>" & tbl3.Rows(i).Item("aingreso").ToString & "</td>"
                html3 &= "<td>" & IIf(tbl3.Rows(i).Item("aegreso").ToString = "0", "-", tbl3.Rows(i).Item("aegreso").ToString) & "</td>"
                'html3 &= "<td>" & tbl3.Rows(i).Item("Situacion").ToString & "</td>"
                html3 &= "<td>" & tbl3.Rows(i).Item("Estudio").ToString.ToUpper & "</td>"
                html3 &= "<td>" & tbl3.Rows(i).Item("TipoInst").ToString & "</td>"
                html3 &= "<td>" & tbl3.Rows(i).Item("institucion").ToString & "</td>"
                'html3 &= "<td>" & tbl3.Rows(i).Item("NivelLectura").ToString & "</td>"
                'html3 &= "<td>" & tbl3.Rows(i).Item("NivelEscritura").ToString & "</td>"
                'html3 &= "<td>" & tbl3.Rows(i).Item("NivelHabla").ToString & "</td>"

                If tbl3.Rows(i).Item("Borrar").ToString = "1" Then
                    html3 &= "<td><a class=""ifancybox"" href=""FrmRegistraOtros.aspx?mod4=" & qs4 & """><img src=""../archivos/editar.png"" alt=""Editar"" /></a></td>"
                    html3 &= "<td><a class="""" href=""?del=" & qs4 & """ onclick=""return confirm_delete();""><img src=""../archivos/borrar.png"" alt=""Borrar"" /></a></td>"
                Else
                    html3 &= "<td><a></a></td>"
                    html3 &= "<td><a></a></td>"
                End If                
                html3 &= "</tr>"
            Next
            html3 &= "</table>"
            GridOtros.InnerHtml = html3
        Else
            GridOtros.InnerHtml = "<br /><font style=""font-size:11px;""><i>No se ha registrado información</i></font>"
        End If
        tbl3.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Sub btnBorrar_Click(ByVal codigo_FA As Integer)
        Try
            eliminarFA(codigo_FA)
            CargarIdioma()
			CargarOtrosEstudios()
        Catch ex As Exception
        End Try
    End Sub
    Sub eliminarFA(ByVal codigo_FA As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("ALUMNI_EliminarFormacion", codigo_FA)
        obj.CerrarConexion()
        obj = Nothing
        CargarIdioma()
		CargarOtrosEstudios()
    End Sub

    Function encode(ByVal str As String) As String
        Return (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(str)))
    End Function

    Function decode(ByVal str As String) As String
        Return System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(str))
    End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("otros.aspx")
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Response.Redirect("GradosyTitulos.aspx")
    End Sub
End Class



Partial Class FormacionAcademica
    Inherits System.Web.UI.Page
    Public Editar As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            CargarGrado()
            CargarTitulo()
        End If
        If Page.Request.QueryString("del") IsNot Nothing Then
            btnBorrar_Click(decode(Page.Request.QueryString("del")))
        End If
    End Sub

    Sub CargarGrado()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tbl As Data.DataTable
        Dim html As String = ""
        tbl = obj.TraerDataTable("[ALUMNI_ConsultarAlumni_FormacionAcademica]", Session("codigo_alu"), "G")
        Me.GridFormacion.InnerHtml = Session("codigo_alu")
        Dim classCss As String = ""
        Dim qs1 As String = ""
        If tbl.Rows.Count Then
            'Me.Formacion.SelectedValue = "S"            
            html &= "<table class=""tabla"" cellspacing=""0"">"
            html &= "<tr><th>año de Ingreso</th><th>año de Egreso</th><th>Fecha de Graduación</th><th>Grado</th><th>universidad</th><th>Procedencia</th><th>Situación</th><th>Editar</th><th>Borrar</th></tr>"
            For i As Integer = 0 To tbl.Rows.Count - 1
                If i Mod 2 Then
                    classCss = "fila1"
                Else
                    classCss = "fila2"
                End If
                qs1 = encode(tbl.Rows(i).Item("id").ToString)
                html &= "<tr class=""" & classCss & """>"
                html &= "<td>" & tbl.Rows(i).Item("aingreso").ToString & "</td>"
                html &= "<td>" & tbl.Rows(i).Item("aegreso").ToString & "</td>"
                html &= "<td>" & tbl.Rows(i).Item("Fechaacto").ToString & "</td>"
                html &= "<td>" & tbl.Rows(i).Item("GradoObtenido").ToString & "</td>"
                html &= "<td>" & tbl.Rows(i).Item("Institucion").ToString & "</td>"
                html &= "<td>" & tbl.Rows(i).Item("PROCEDENCIA").ToString & "</td>"
                html &= "<td>" & tbl.Rows(i).Item("SITUACION").ToString & "</td>"

                If tbl.Rows(i).Item("Borrar").ToString = "1" Then
                    html &= "<td><a class=""ifancybox"" href=""FormacionAcademicaRegistrar.aspx?mod=" & qs1 & """><img src=""../archivos/editar.png"" alt=""Editar"" /></a></td>"
                    html &= "<td><a class="""" href=""?del=" & qs1 & """ onclick=""return confirm_delete();""><img src=""../archivos/borrar.png"" alt=""Borrar"" /></a></td>"
                Else
                    html &= "<td></td>"
                    html &= "<td></td>"
                End If
                html &= "</tr>"
            Next
            html &= "</table>"
            GridFormacion.InnerHtml = html
        Else
            'Me.Formacion.SelectedValue = "N"
            GridFormacion.InnerHtml = "<br /><font style=""font-size:11px;""><i>No se ha registrado información</i></font>"
        End If
        tbl.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Sub CargarTitulo()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tbl2 As Data.DataTable
        Dim html2 As String = ""
        tbl2 = obj.TraerDataTable("[ALUMNI_ConsultarAlumni_FormacionAcademica]", Session("codigo_alu"), "T")
        Me.GridTitulo.InnerHtml = Session("codigo_alu")
        Dim classCss As String = ""
        Dim qs2 As String = ""
        If tbl2.Rows.Count Then          
            html2 &= "<table class=""tabla"" cellspacing=""0"">"
            html2 &= "<tr><th>año de Ingreso</th><th>año de Egreso</th><th>Fecha de Titulación</th><th>Titulo</th><th>universidad</th><th>Procedencia</th><th>Situación</th><th>Editar</th><th>Borrar</th></tr>"
            For i As Integer = 0 To tbl2.Rows.Count - 1
                If i Mod 2 Then
                    classCss = "fila1"
                Else
                    classCss = "fila2"
                End If
                qs2 = encode(tbl2.Rows(i).Item("id").ToString)
                html2 &= "<tr class=""" & classCss & """>"
                html2 &= "<td>" & tbl2.Rows(i).Item("aingreso").ToString & "</td>"
                html2 &= "<td>" & tbl2.Rows(i).Item("aegreso").ToString & "</td>"
                html2 &= "<td>" & tbl2.Rows(i).Item("Fechaacto").ToString & "</td>"
                html2 &= "<td>" & tbl2.Rows(i).Item("GradoObtenido").ToString & "</td>"
                html2 &= "<td>" & tbl2.Rows(i).Item("Institucion").ToString & "</td>"
                html2 &= "<td>" & tbl2.Rows(i).Item("PROCEDENCIA").ToString & "</td>"
                html2 &= "<td>" & tbl2.Rows(i).Item("SITUACION").ToString & "</td>"
                html2 &= "<td><a class=""ifancybox"" href=""FrmRegistraTitulo.aspx?mod=" & qs2 & """><img src=""../archivos/editar.png"" alt=""Editar"" /></a></td>"
                html2 &= "<td><a class="""" href=""?del=" & qs2 & """ onclick=""return confirm_delete();""><img src=""../archivos/borrar.png"" alt=""Borrar"" /></a></td>"
                html2 &= "</tr>"
            Next
            html2 &= "</table>"
            GridTitulo.InnerHtml = html2
        Else
            GridTitulo.InnerHtml = "<br /><font style=""font-size:11px;""><i>No se ha registrado información</i></font>"
        End If
        tbl2.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    
    Sub btnBorrar_Click(ByVal codigo_FA As Integer)
        Try
            eliminarFA(codigo_FA)
            CargarGrado()
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
        CargarGrado()
        CargarTitulo()
        'CargarIdioma()
    End Sub

    Function encode(ByVal str As String) As String
        Return (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(str)))
    End Function

    Function decode(ByVal str As String) As String
        Return System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(str))
    End Function
End Class



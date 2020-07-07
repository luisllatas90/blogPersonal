Imports System.Text
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Partial Class personales
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Request.QueryString("id") <> "" And Session("id") = "" Then
        '    Session("Id") = Request.QueryString("id")
        'End If
        'If IsPostBack = False Then
        '    Me.CmdGuardar.Attributes.Add("OnMouseOver", "tabsobre(this,1)")
        '    Me.CmdGuardar.Attributes.Add("OnMouseOut", "tabsobre(this,2)")

        '    Me.DDLReligion.Attributes.Add("onchange", "javascript:religion();return false;")
        '    Me.Img.Attributes.Add("onmouseover", "ddrivetip('Ingrese una imagen haciendo click en examinar, con una extension (*.jpg) y tamaño no mayor a 60 Kb.')")
        '    Me.Img.Attributes.Add("onMouseout", "hideddrivetip()")
        '    Dim objCombos As New Combos
        '    objCombos.LlenaPais(Me.DDlNacionalidad)
        '    objCombos.LlenaDepartamento(Me.DDLDepartamento)
        '    objCombos.LlenaProvincia(Me.DDLProvincia, 0)
        '    objCombos.LlenaDistrito(Me.DDLDistrito, 0)
        '    objCombos = Nothing
        '    Me.DDlAño.Items.Add("Año")
        '    For i As Integer = (Now.Year - 10) To 1915 Step -1
        '        Me.DDlAño.Items.Add(i.ToString)
        '    Next
        '    Call LlenaDatosPersonal()
        'End If
    End Sub



    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim objPersonal As New Personal
        objPersonal.codigo = Request.QueryString("id")
        Dim valor As Int16 = 0
        Dim script As String
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings(1).ConnectionString)

        '---------------------------------------------------------------------------------------------------
        ' Puse las valicaciones aqui debido a que no se estaban tomando siempre, estan en la clase. xD
        '---------------------------------------------------------------------------------------------------
        If FuFoto.HasFile = False Then
            script = "<script>alert('Favor de seleccionar el documento a cargar.')</script>"
            Page.RegisterStartupScript("Mensajes", script)
            Exit Sub
        End If
        '1048576 1MB
        If FuFoto.PostedFile.ContentLength > (1052905) Then
            script = "<script>alert('El tamaño del archivo supera lo permitido no debe superar 1 Mb.')</script>"
            Page.RegisterStartupScript("Mensajes", script)
            Exit Sub
        End If
        '---------------------------------------------------------------------------------------------------

        valor = objPersonal.GrabaPDP(Me.FuFoto)

        If valor = -1 Then
            script = "<script>alert('Error en actualizar sus datos, inténtelo nuevamente')</script>"
        ElseIf valor = -2 Then
            script = "<script>alert('Error al ingresar su pdp, las extensiones permitidas son *.doc, *.docx ,*.pdf')</script>"
        ElseIf valor = -3 Then
            script = "<script>alert('El tamaño de la imagen no debe superar 1 Mb.')</script>"
        Else
            script = "<script>alert('Los datos se grabaron correctamente,verifique en la pagina VER HOJA')</script>"
            'Call LlenaDatosPersonal()
            Page.RegisterStartupScript("Mensajes", script)
            'Response.Redirect("perfil.aspx?menu=" & Request.QueryString("menu") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))

        End If
        objPersonal = Nothing
    End Sub

End Class

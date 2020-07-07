Imports System.Data
Partial Class Consultas_hojavida
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Datos As DataTable
        Dim ObjPersonal As New Personal
        ObjPersonal.codigo = Request.QueryString("codigo_per")
        Datos = ObjPersonal.ObtieneDatosPersonales()

        'Dim i As Int16
        With Datos.Rows(0)
            Me.LblNombres.Text = .Item(2).ToString & " " & .Item(0).ToString & " " & .Item(1).ToString                 'Nombres
            Me.LblDNI.Text = .Item(5).ToString                'Numero de Documento
            Me.LblCivil.Text = .Item(6).ToString           'estado civil
            If IsDate(.Item(7).ToString) Then
                Me.LblEdad.Text = DateDiff(DateInterval.Year, .Item(7), Now.Date)
                '    DDlDia.SelectedValue = Day(.Item(7).ToString)       'Dia de Nacimiento
                '    DDLMes.SelectedValue = Month(.Item(7).ToString)     'Mes de Nacimiento
                '    DDlAño.SelectedValue = Year(.Item(7).ToString)      'Año de Nacimiento
            End If
            Me.LblDireccion.Text = .Item(8).ToString             'Direccion
            Me.LblTelefono.Text = .Item(9).ToString             'Telefono Domicilio
            Me.LblEmail.Text = .Item(10).ToString                   'Mail 1
            Me.LblCelular.Text = .Item(14).ToString              'Telefono Celular
            Me.LblPerfil.Text = .Item("perfil_per").ToString
            'Me.LblPerfil.Style.Add("text-align", "justify")


            If .Item("foto").ToString = "" Then
                Me.ImgFoto.Visible = False
                'Me.imgfoto.ImageUrl = "images/fotovacia.gif"
            Else
                Me.ImgFoto.Visible = True
                Me.ImgFoto.ImageUrl = "../../../imgpersonal/" & .Item("foto").ToString
            End If
        End With
        ObjPersonal = Nothing
        If Me.DatIdiomas.Items.Count = 0 Then
            Me.LblIdiomas.Visible = False
        Else
            Me.LblIdiomas.Text = Me.LblIdiomas.Text & "<br><br>"
        End If

        If Me.DatInvestigacion.Items.Count = 0 Then
            Me.LblInvestigacion.Visible = False
        Else
            Me.LblInvestigacion.Text = Me.LblInvestigacion.Text & "<br><br>"
        End If

        If Me.DatSeminarios.Items.Count = 0 Then
            Me.LblSeminarios.Visible = False
        Else
            Me.LblSeminarios.Text = Me.LblSeminarios.Text & "<br><br>"
        End If

        If Me.DatExperiencia.Items.Count = 0 Then
            Me.LblExperiencia.Visible = False
        Else
            Me.LblExperiencia.Text = Me.LblExperiencia.Text & "<br><br>"
        End If

        If Me.DatOtros.Items.Count = 0 Then
            Me.LblActualizaciones.Visible = False
        Else
            Me.LblActualizaciones.Text = Me.LblActualizaciones.Text & "<br><br>"
        End If

        If Me.DatDistinciones.Items.Count = 0 Then
            Me.LblDistinciones.Visible = False
        Else
            Me.LblDistinciones.Text = Me.LblDistinciones.Text & "<br><br>"
        End If

        If Me.DatGrados.Items.Count = 0 Then
            Me.LblGrado.Visible = False
        Else
            Me.LblGrado.Text = Me.LblGrado.Text & "<br>"
        End If

        If Me.DatTitulos.Items.Count = 0 Then
            Me.LblTitulos.Visible = False
        Else
            Me.LblTitulos.Text = Me.LblTitulos.Text & "<br><br>"
        End If

        If Me.DatTitulos.Items.Count = 0 And Me.DatGrados.Items.Count = 0 Then
            Me.LblGrados.Visible = False
        Else
            Me.LblGrados.Text = Me.LblGrados.Text & "<br><br>"
        End If

        If Me.LblPerfil.Text = "" Then
            Me.LblPerfilEtiqueta.Visible = False
        Else
            Me.LblPerfilEtiqueta.Text = Me.LblPerfilEtiqueta.Text & "<br>"
            Me.LblPerfil.Text = Me.LblPerfil.Text & "<br><br>"
        End If
        If Datos.Rows(0).Item("pdp_per").ToString = "" Then
            pdp.visible = False
        Else
            pdp.visible = True
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-word"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & Me.LblNombres.Text & ".doc")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        Response.Write(HTML())
        Response.End()
    End Sub

    Public Function HTML() As String
        Dim page1 As New Page
        Dim form1 As New HtmlForm

        Me.Table2.EnableViewState = False
        page1.EnableViewState = False

        page1.Controls.Add(form1)
        form1.Controls.Add(Me.Table2)

        Dim Builder1 As New System.Text.StringBuilder
        Dim writer1 As New System.IO.StringWriter(Builder1)
        Dim writer2 As New HtmlTextWriter(writer1)

        page1.RenderControl(writer2)
        writer2.Write("<font face='verdana' size='2' color='#121212'>Curriculum generado por USAT<br>Actualizado al :" & Date.Now() & "</font>")
        page1.Dispose()
        page1 = Nothing
        Return Builder1.ToString
    End Function

    Protected Sub pdp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pdp.Click
        Dim Datos As DataTable
        Dim pdp As String
        Dim ObjPersonal As New Personal
        ObjPersonal.codigo = Request.QueryString("codigo_per")
        Datos = ObjPersonal.ObtieneDatosPersonales()
        pdp = Datos.Rows(0).Item("pdp_per").ToString
        ObjPersonal = Nothing
        Response.Redirect("http://www.usat.edu.pe/campusvirtual/personal/pdp/" & pdp)
    End Sub

End Class

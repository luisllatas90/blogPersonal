Imports System.Data

Partial Class agregatitulo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim objCOmbo As New Combos
            objCOmbo.llenaTitulo(Me.DDLTitulo)
            objCOmbo.LlenaTipoInstitucion(Me.DDLInstitucion)
            objCOmbo.LlenaSituacion(Me.DDLSituacion)
            Me.DDLTitulo.Attributes.Add("onchange", "mostrarcaja();return false;")
            Me.DDLCentro.Attributes.Add("onchange", "mostrarcaja2();return false;")

            Me.Img.Attributes.Add("onmouseover", "ddrivetip('Si el titulo no se encuentra en la lista anterior, seleccione OTRO y escriba el nombre en la caja de texto.')")
            Me.Img.Attributes.Add("onMouseout", "hideddrivetip()")
            Me.DDLATitulo.Items.Add("Pendiente")
            Me.DDLATitulo.Items(0).Value = "3000"
            Me.DDLAEgreso.Items.Add("Pendiente")
            Me.DDLAEgreso.Items(0).Value = "3000"
            For i As Int16 = Year(Now) To 1940 Step -1
                Me.DDlAIngreso.Items.Add(i)
                Me.DDLAEgreso.Items.Add(i)
                Me.DDLATitulo.Items.Add(i)
            Next

            If Request.QueryString("codigo_tpr") <> "" Then
                Dim objTitulo As New Personal
                Dim datos As DataTable
                datos = objTitulo.ObtieneDatosTitulos(Request.QueryString("codigo_tpr"), "MO")
                With datos.Rows(0)
                    Me.DDLTitulo.SelectedValue = .Item("codigo_tpf").ToString
                    Me.DDLInstitucion.SelectedValue = .Item("codigo_tis").ToString
                    Me.DDLSituacion.SelectedValue = .Item("codigo_sit").ToString
                    Me.DDlAIngreso.SelectedValue = .Item("anioingreso_tpr").ToString
                    Me.DDLAEgreso.SelectedValue = .Item("anioegreso_tpr").ToString
                    Me.DDLATitulo.SelectedValue = .Item("aniograd_tpr").ToString
                    Me.TxtOtrosTitulo.Text = .Item("nombretitulo_tpr").ToString
                    Me.TxtOtros.Text = .Item("universidad_tpr").ToString
                    If datos.Rows(0).Item("codigo_pai").ToString = "156" Then
                        Me.DDLProcedencia.SelectedValue = 1
                        objCOmbo.LlenaInstitucion(Me.DDLCentro, .Item("codigo_tis").ToString, "1")
                    Else
                        Me.DDLProcedencia.SelectedValue = 2
                        objCOmbo.LlenaInstitucion(Me.DDLCentro, .Item("codigo_tis").ToString, "2")
                    End If
                    Me.DDLCentro.SelectedValue = .Item("codigo_ins").ToString()
                End With
                objTitulo = Nothing
            Else
                objCOmbo.LlenaInstitucion(Me.DDLCentro, "1", "1")
            End If
            objCOmbo = Nothing
        End If
    End Sub

    Protected Sub DDLInstitucion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLInstitucion.SelectedIndexChanged
        Dim objcombo As New Combos
        objcombo.LlenaInstitucion(Me.DDLCentro, Me.DDLInstitucion.SelectedValue, Me.DDLProcedencia.SelectedValue)
        objcombo = Nothing
    End Sub

    Protected Sub DDLProcedencia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLProcedencia.SelectedIndexChanged
        Dim objcombo As New Combos
        objcombo.LlenaInstitucion(Me.DDLCentro, Me.DDLInstitucion.SelectedValue, Me.DDLProcedencia.SelectedValue)
        objcombo = Nothing
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim objpersonal As New Personal
        Dim codTpf, anoing, anoegr, anotitu, codins, codsit As Integer
        Dim strinstitucion, strtitulo As String
        Dim valor As Int16
        Try
            codTpf = CInt(Me.DDLTitulo.SelectedValue)
            anoing = CInt(Me.DDlAIngreso.SelectedValue)
            anoegr = CInt(Me.DDLAEgreso.SelectedValue)
            If anoegr = 3000 Then
                anoegr = 0
            End If
            anotitu = CInt(Me.DDLATitulo.SelectedValue)
            If anotitu = 3000 Then
                anotitu = 0
            End If
            codins = CInt(Me.DDLCentro.SelectedValue)
            codsit = CInt(Me.DDLSituacion.SelectedValue)
            strinstitucion = Me.TxtOtros.Text
            strtitulo = Me.TxtOtrosTitulo.Text
            objpersonal.codigo = CInt(Session("id"))
            If Request.QueryString("codigo_tpr") = "" Then
                valor = objpersonal.GrabarTitulos(codTpf, strtitulo, anoing, anoegr, anotitu, strinstitucion, codsit, codins)
            Else
                valor = objpersonal.ModificaTitulos(codTpf, strtitulo, anoing, anoegr, anotitu, strinstitucion, codsit, codins, Request.QueryString("codigo_tpr"))
            End If

            If valor = -1 Then
                Me.LblError.Text = "Ocurrio un error, intentelo nuevamente"
                Exit Sub
            End If
        Catch ex As Exception
            Me.LblError.Text = "Ocurrio un error, intentelo nuevamente"
        End Try
        Response.Write("<script>window.opener.location='educacionuniversitaria.aspx?id=" & Session("id") & "';window.close();</script>")
    End Sub

End Class

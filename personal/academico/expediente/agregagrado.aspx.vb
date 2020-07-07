Imports System.Data

Partial Class agregagrado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.DDLGrado.Attributes.Add("onchange", "mostrarcaja();return false;")
            Me.DDLCentro.Attributes.Add("onchange", "mostrarcaja2(); return false;")
            Dim objCOmbo As New Combos
            objCOmbo.LlenaTipoInstitucion(Me.DDLInstitucion)
            objCOmbo.LlenaSituacion(Me.DDLSituacion)
            Me.DDLATitulo.Items.Add("Pendiente")
            Me.DDLATitulo.Items(0).Value = "3000"
            Me.DDLAEgreso.Items.Add("Pendiente")
            Me.DDLAEgreso.Items(0).Value = "3000"
            For i As Int16 = Year(Now) To 1940 Step -1
                Me.DDlAIngreso.Items.Add(i)
                Me.DDLAEgreso.Items.Add(i)
                Me.DDLATitulo.Items.Add(i)
            Next
            If Request.QueryString("codigo_gpr") <> "" Then
                Dim objGrados As New Personal
                Dim Datos As datatable
                Datos = objGrados.ObtieneDatosGrados(Request.QueryString("codigo_gpr"), "MO")
                With Datos.Rows(0)
                    Me.DDLTipoGrado.SelectedValue = .Item("codigo_tgr")
                    objCOmbo.LlenaGrados(Me.DDLGrado, Me.DDLTipoGrado.SelectedValue)
                    Me.DDLGrado.SelectedValue = .Item("codigo_gra")
                    Me.TxtOtrosGrados.Text = .Item("desgrado_gpr")
                    Me.TxtMención.Text = .Item("mencion_gpr")
                    Me.DDlAIngreso.SelectedValue = .Item("anioingreso_gpr")
                    Me.DDLAEgreso.SelectedValue = .Item("anioegreso_gpr")
                    Me.DDLATitulo.SelectedValue = .Item("aniograd_gpr")
                    If Datos.Rows(0).Item("codigo_pai").ToString = "156" Then
                        Me.DDLProcedencia.SelectedValue = 1
                        objCOmbo.LlenaInstitucion(Me.DDLCentro, .Item("codigo_tis").ToString, "1")
                    Else
                        Me.DDLProcedencia.SelectedValue = 2
                        objCOmbo.LlenaInstitucion(Me.DDLCentro, .Item("codigo_tis").ToString, "2")
                    End If
                    Me.DDLCentro.SelectedValue = .Item("codigo_ins")
                    Me.DDLSituacion.SelectedValue = .Item("codigo_sit")
                    Me.TxtOtros.Text = .Item("universidad_gpr")
                End With
                objGrados = Nothing
            Else
                objCOmbo.LlenaInstitucion(Me.DDLCentro, "1", "1")
                objCOmbo.LlenaGrados(Me.DDLGrado, Me.DDLTipoGrado.SelectedValue)
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

    Protected Sub DDLTipoGrado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLTipoGrado.SelectedIndexChanged
        Dim objCombo As New Combos
        objCombo.LlenaGrados(Me.DDLGrado, Me.DDLTipoGrado.SelectedValue)
        objCombo = Nothing
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim objpersonal As New Personal
        Dim codGpr, anoing, anoegr, anotitu, codins, codsit As Integer
        Dim strinstitucion As String
        Dim StrMencion As String
        Dim strgrado As String
        Dim valor As Int16
        Try
            codGpr = CInt(Me.DDLGrado.SelectedValue)
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
            StrMencion = Me.TxtMención.Text
            strgrado = Me.TxtOtrosGrados.Text
            objpersonal.codigo = CInt(Session("id"))
            If Request.QueryString("codigo_gpr") = "" Then
                valor = objpersonal.GrabarGrados(codGpr, strgrado, anoing, anoegr, anotitu, StrMencion, strinstitucion, codsit, codins)
            Else
                valor = objpersonal.ModificaGrados(codGpr, strgrado, anoing, anoegr, anotitu, StrMencion, strinstitucion, codsit, codins, Request.QueryString("codigo_gpr"))
            End If

            If valor = -1 Then
                Me.LblError.Text = "Ocurrio un error, intentelo nuevamente"
                Exit Sub
            End If
        Catch ex As Exception
            Me.LblError.Text = "Ocurrio un error, intentelo nuevamente"
            objpersonal = Nothing
        End Try
        objpersonal = Nothing
        Response.Write("<script>window.opener.location='educacionuniversitaria.aspx?id=" & Session("id") & "';window.close();</script>")
    End Sub
End Class

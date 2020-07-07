Imports System.Data

Partial Class agregaotros
    Inherits System.Web.UI.Page

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If IsPostBack = False Then
            Dim ObjCombo As New Combos
            Me.DDLCentro.Attributes.Add("onchange", "mostrarcaja2(); return false;")
            ObjCombo.LlenaAreaEstudio(Me.DDLArea)
            ObjCombo.LlenaTipoInstitucion(Me.DDLInstitucion)
            ObjCombo.LlenaSituacion(Me.DDLSituacion)
            Me.DDLAnioFin.Items.Add("En Curso")
            Me.DDLAnioFin.Items(0).Value = 0
            For i As Integer = Now.Year To 1980 Step -1
                Me.DDLAnioIni.Items.Add(i)
                Me.DDLAnioFin.Items.Add(i)
            Next

            If Request.QueryString("codigo_opr") <> "" Then
                Dim ObjOtros As New Personal
                Dim datos As New DataTable
                datos = ObjOtros.ObtieneDatosOtros(Request.QueryString("codigo_opr"), "MO")
                With datos.Rows(0)
                    Me.DDLArea.SelectedValue = .Item("codigo_areaes")
                    Me.TxtEstudio.Text = .Item("nombre_est")
                    Me.DDLInstitucion.SelectedValue = .Item("codigo_tis")
                    If .Item("codigo_pai") = "156" Then
                        Me.DDLProcedencia.SelectedValue = 1
                        ObjCombo.LlenaInstitucion(Me.DDLCentro, .Item("codigo_tis"), "1")
                    Else
                        Me.DDLProcedencia.SelectedValue = 2
                        ObjCombo.LlenaInstitucion(Me.DDLCentro, .Item("codigo_tis"), "2")
                    End If
                    Me.TxtOtros.Text = .Item("nombre_ins")
                    Me.DDLMesIni.Text = .Item("mes_inicio")
                    Me.DDLAnioIni.SelectedValue = .Item("anio_inicio")
                    Me.DDLMesFin.Text = .Item("mes_fin")
                    Me.DDLAnioFin.SelectedValue = .Item("anio_fin")
                    Me.DDLModalidad.SelectedValue = .Item("tipo_mod")
                    Me.DDLCursa.SelectedValue = .Item("actual_estudio")
                    Me.DDLSituacion.SelectedValue = .Item("codigo_sit")
                    Me.DDLCentro.SelectedValue = .Item("codigo_ins")
                    Me.TXtObservaciones.Text = .Item("observacion")
                End With
            Else
                ObjCombo.LlenaInstitucion(Me.DDLCentro, "1", "1")
            End If

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
        Dim Objpersonal As Personal

        Dim intArea, intAnioIni, intAnioFin, intCentro, valor, intTipo, intCiclo, intsituacion As Int16
        Dim strMesIni, strMesFin As String
        Dim strOtros = "", strObservaciones, strEstudio As String

        Try
            intArea = Me.DDLArea.SelectedValue
            strEstudio = Me.TxtEstudio.Text
            intCentro = Me.DDLCentro.SelectedValue
            If intCentro = 1 Or (intCentro >= 190 And intCentro <= 204) Then
                strOtros = Me.TxtOtros.Text
            End If
            intTipo = Me.DDLModalidad.SelectedValue
            intsituacion = Me.DDLSituacion.SelectedValue
            strObservaciones = Me.TXtObservaciones.Text
            intCiclo = Me.DDLCursa.SelectedValue
            strMesIni = Me.DDLMesIni.SelectedValue
            strMesFin = Me.DDLMesFin.SelectedValue
            intAnioIni = Me.DDLAnioIni.SelectedValue
            intAnioFin = Me.DDLAnioFin.SelectedValue

            Objpersonal = New Personal
            Objpersonal.codigo = Session("id")
            If Request.QueryString("codigo_opr") = "" Then
                valor = Objpersonal.GrabarOtros(intArea, intCentro, strOtros, strEstudio, strMesIni, intAnioIni, strMesFin, intAnioFin, intTipo, intCiclo, strObservaciones, intsituacion)
            Else
                valor = Objpersonal.ModificaOtrosEstudios(intArea, intCentro, strOtros, strEstudio, strMesIni, intAnioIni, strMesFin, intAnioFin, intTipo, intCiclo, strObservaciones, intsituacion, CInt(Request.QueryString("codigo_opr")))
            End If
            If valor = -1 Then
                Me.LblError.Text = "Ocurrio un error, intentelo nuevamente"
                Exit Sub
            End If
        Catch ex As Exception
            Me.LblError.Text = "Ocurrio un errdddddor, intentelo nuevamente"
            Objpersonal = Nothing
        End Try
        Objpersonal = Nothing
        Response.Write("<script>window.opener.location='idiomas.aspx?id=" & Session("id") & "';window.close();</script>")
    End Sub
End Class

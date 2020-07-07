Imports System.Data
Partial Class agregaidiomas
    Inherits System.Web.UI.Page

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
        Dim intIdioma, intAno, intTipoIns, intCentro, valor As Int16
        Dim intLee, intHabla, intEscribe, intSituacion As Int16
        Dim strOtros = "", strObservaciones As String
        Try
            intIdioma = Me.DDLIdioma.SelectedValue
            intAno = Me.DDlAno.SelectedValue
            intTipoIns = Me.DDLInstitucion.SelectedValue
            intCentro = Me.DDLCentro.SelectedValue
            If intCentro = 1 Or (intCentro >= 190 And intCentro <= 204) Then
                strOtros = Me.TxtOtros.Text
            End If
            intLee = Me.DDLLee.SelectedValue
            intHabla = Me.DDLHabla.SelectedValue
            intEscribe = Me.DDLEscribe.SelectedValue
            strObservaciones = Me.TXtObservaciones.Text
            intSituacion = Me.DDLSituacion.SelectedValue
            Objpersonal = New Personal
            Objpersonal.codigo = Session("id")
            If Request.QueryString("codigo_ipr") = "" Then
                valor = Objpersonal.GrabarIdiomas(intSituacion, intIdioma, strOtros, intAno, strObservaciones, intLee, intHabla, intEscribe, intCentro)
            Else
                valor = Objpersonal.ModificaIdiomas(intSituacion, intIdioma, strOtros, intAno, strObservaciones, intLee, intHabla, intEscribe, intCentro, Request.QueryString("codigo_ipr"))
            End If
            If valor = -1 Then
                Me.LblError.Text = "Ocurrio un error, intentelo nuevamente"
                Exit Sub
            End If
        Catch ex As Exception
            Me.LblError.Text = "Ocurrio un error, intentelo nuevamente"
            Objpersonal = Nothing
        End Try
        Objpersonal = Nothing
        Response.Write("<script>window.opener.location='idiomas.aspx?id=" & Session("id") & "';window.close();</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim objCombo As New Combos
            Dim i As Int16
            For i = Year(Date.Now) To 1960 Step -1
                Me.DDlAno.Items.Add(i)
            Next
            Me.DDLCentro.Attributes.Add("onchange", "mostrarcaja2(); return false;")
            objCombo.LlenaIdiomas(Me.DDLIdioma, "")
            objCombo.LlenaTipoInstitucion(Me.DDLInstitucion)
            objCombo.LlenaSituacion(Me.DDLSituacion)
            If Request.QueryString("codigo_ipr") <> "" Then
                Dim ObjPersonal As New Personal
                Dim Datos As DataTable
                Datos = ObjPersonal.ObtieneDatosIdiomas(Request.QueryString("codigo_ipr"), "MO")
                With Datos.Rows(0)
                    Me.DDLIdioma.SelectedValue = .Item("codigo_idi")
                    Me.DDlAno.SelectedValue = .Item("aniograduacion")
                    Me.DDLSituacion.SelectedValue = .Item("codigo_sit")
                    Me.DDLInstitucion.SelectedValue = .Item("codigo_tis")
                    If .Item("codigo_pai") = "156" Then
                        Me.DDLProcedencia.SelectedValue = 1
                        objCombo.LlenaInstitucion(Me.DDLCentro, .Item("codigo_tis"), "1")
                    Else
                        Me.DDLProcedencia.SelectedValue = 2
                        objCombo.LlenaInstitucion(Me.DDLCentro, .Item("codigo_tis"), "2")
                    End If
                    Me.DDLCentro.SelectedValue = .Item("codigo_ins")
                    Me.TxtOtros.Text = .Item("centroestudios").ToString
                    Me.TXtObservaciones.Text = .Item("observaciones").ToString
                    Me.DDLLee.SelectedValue = .Item("lee")
                    Me.DDLHabla.SelectedValue = .Item("habla")
                    Me.DDLEscribe.SelectedValue = .Item("escribe")
                End With
            Else
                objCombo.LlenaInstitucion(Me.DDLCentro, "1", "1")
            End If
        End If
    End Sub
End Class

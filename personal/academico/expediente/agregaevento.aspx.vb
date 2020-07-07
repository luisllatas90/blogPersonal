
Imports System.Data
Partial Class agregaexperiencia
    Inherits System.Web.UI.Page


    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If IsPostBack = False Then
            Me.TxtHoras.Attributes.Add("OnKeyPress", "validarnumero()")
            Me.LstEventos.Attributes.Add("onchange", "vernuevo()")
            Me.Img.Attributes.Add("onmouseover", "ddrivetip('Seleccione tipo de evento e ingrese un texto de busqueda para mostrar la lista de eventos registrados.')")
            Me.Img.Attributes.Add("onMouseout", "hideddrivetip()")
            Dim ObjCombo As New Combos
            ObjCombo.LlenaClaseEvento(Me.DDLClaseEven)
            ObjCombo.LlenaTipoEvento(Me.DDLTIpo, 1)
            ObjCombo.LlenaTipoParticipacion(Me.ChkParticipa)

            Me.DDLFinAño.Items.Add("Año")
            Me.DDLIniAño.Items.Add("Año")
            Me.DDLIniAño.Items(0).Value = 0
            Me.DDLFinAño.Items(0).Value = 0
            For i As Int16 = Year(Now) To 1940 Step -1
                Me.DDLIniAño.Items.Add(i)
                Me.DDLFinAño.Items.Add(i)
            Next
            Me.HddLista.Value = Me.ChkParticipa.Items.Count

            Me.LstEventos.Items.Clear()
            Me.LstEventos.Items.Add("<---- Otros ---->")
            Me.LstEventos.Items(0).Value = 0

            If Request.QueryString("codigo_eve") <> "" Then
                Dim Datos As DataTable
                Dim ObjEventos As New Personal
                Datos = ObjEventos.ObtieneDatosEventos("MO", Request.QueryString("tipo"), Request.QueryString("codigo_eve"))
                With Datos.Rows(0)
                    Me.DDLTIpo.SelectedValue = .Item("tipoevento")
                    Me.DDLClaseEven.SelectedValue = .Item("claseevento")
                    If .Item("modo") = 1 Then
                        Me.RbAcademico.Checked = True
                        ObjCombo.LlenaEventos(Me.LstEventos, "", .Item("tipoevento"), .Item("claseevento"), 1)
                    Else
                        Me.RbSocial.Checked = False
                        ObjCombo.LlenaEventos(Me.LstEventos, "", .Item("tipoevento"), 2, 1)
                    End If
                    Me.LstEventos.SelectedValue = .Item("codevento")
                    Me.ChkParticipa.SelectedValue = .Item("tipoparticipa")
                End With
            Else
                Me.DDLTIpo.SelectedValue = 7
                ObjCombo.LlenaEventos(Me.LstEventos, "a", 7, 2, 1)
            End If
            ObjCombo = Nothing
        End If
    End Sub

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim ObjCombos As New Combos
        Dim tipo As Integer
        If Me.RbAcademico.Checked = True Then
            tipo = 1
        Else
            tipo = 2
        End If
        ObjCombos.LlenaEventos(Me.LstEventos, Me.TxtBuscar.Text, Me.DDLTIpo.SelectedValue, Me.DDLClaseEven.SelectedValue, tipo)
        ObjCombos = Nothing
    End Sub

    Protected Sub RbAcademico_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbAcademico.CheckedChanged
        Dim objcombo As New Combos
        objcombo.LlenaTipoEvento(Me.DDLTIpo, 1)
        objcombo.LlenaEventos(Me.LstEventos, "", "", "", 1)
        objcombo = Nothing
        Me.DDLClaseEven.Visible = True
    End Sub

    Protected Sub RbSocial_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbSocial.CheckedChanged
        Dim objcombo As New Combos
        objcombo.LlenaTipoEvento(Me.DDLTIpo, 2)
        objcombo.LlenaEventos(Me.LstEventos, "", "", "", 2)
        objcombo = Nothing
        Me.DDLClaseEven.Visible = False
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim ObjPersonal As New Personal
        Dim cadena As String = Nothing
        Dim tipoInserta As String
        Dim tipoEven, claseEven, evento, strNombre, strorganizado As String
        Dim Fini, FFin As Date
        Dim intDuracion, tipoDuracion, valor As Integer
        strNombre = ""
        strorganizado = ""
        intDuracion = 0
        Try
            For i As Integer = 0 To Me.ChkParticipa.Items.Count - 1
                If Me.ChkParticipa.Items(i).Selected = True Then
                    cadena = cadena & Me.ChkParticipa.Items(i).Value & ","
                End If
            Next
            If Me.RbAcademico.Checked = True Then
                tipoInserta = "AC"
            Else
                tipoInserta = "SO"
            End If
            tipoEven = Me.DDLTIpo.SelectedValue
            claseEven = Me.DDLClaseEven.SelectedValue

            evento = Me.LstEventos.SelectedValue
            If evento = 0 Then
                strNombre = Me.TxtOtro.Text
                strorganizado = Me.TxtOrganizado.Text
                Fini = CType(Me.DDLIniDia.SelectedValue & "/" & Me.DDLIniMes.SelectedValue & "/" & Me.DDLIniAño.SelectedValue, Date)
                FFin = CType(Me.DDLFinDIa.SelectedValue & "/" & Me.DDLFinMes.SelectedValue & "/" & Me.DDLFinAño.SelectedValue, Date)
                intDuracion = CInt(Me.TxtHoras.Text)
                tipoDuracion = Me.DDLDuracion.SelectedValue
            Else
                Fini = #1/1/1900#
                FFin = #1/1/1900#
            End If
            ObjPersonal.codigo = Session("id")
            valor = ObjPersonal.GrabarEventos(tipoInserta, strNombre, Fini, FFin, claseEven, tipoEven, strorganizado, _
            intDuracion, tipoDuracion, cadena, evento)
            If valor = -1 Then
                Me.LblError.Text = "Ocurio un error al insertar los datos, intentelo nuevamente"
            Else
                ObjPersonal = Nothing
                Response.Write("<script>window.opener.location.href='experiencia.aspx?id=" & Session("id") & "';window.close();</script>")
            End If
        Catch ex As Exception
            Me.LblError.Text = "Error al insertar los datos, intentelo nuevamente"
            ObjPersonal = Nothing
        End Try
    End Sub
End Class

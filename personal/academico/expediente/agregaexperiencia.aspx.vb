Imports System.Data

Partial Class agregaexperiencia
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim objCombo As New Combos
            objCombo.LlenaCargos(Me.DDLCargo)
            objCombo.LlenaTipoContrato(Me.DDLContrato)
            Me.DDLAnioFin.Items.Add("En Curso")
            Me.DDLAnioFin.Items(0).Value = 0
            For i As Integer = Now.Year To 1960 Step -1
                Me.DDLAnioIni.Items.Add(i)
                Me.DDLAnioFin.Items.Add(i)
            Next
            Me.DDLMesFin.Attributes.Add("onchange", "activar();")
            Me.DDLAnioFin.Attributes.Add("onchange", "activar();")
            If Request.QueryString("codigo_exp") <> "" Then
                Dim Datos As New DataTable
                Dim ObjExperiencia As New Personal
                Datos = ObjExperiencia.ObtieneDatosExperiencia(Request.QueryString("codigo_exp"), "MO")
                With Datos.Rows(0)
                    Me.TxtEmpresa.Text = .Item("empresa")
                    Me.TxtCiudad.Text = .Item("ciudad")
                    Me.DDLContrato.SelectedValue = .Item("codigo_tco")
                    Me.DDLCargo.SelectedValue = .Item("codigo_car")
                    Me.TxtFuncion.Text = .Item("funcion_exp")
                    Me.DDLMesIni.SelectedValue = .Item("mesini")
                    Me.DDLAnioIni.SelectedValue = .Item("anioini")
                    Me.DDLMesFin.SelectedValue = .Item("mesfin")
                    Me.DDLAnioFin.SelectedValue = .Item("aniofin")
                    Me.DDLCese.SelectedValue = .Item("motivocese")
                    Me.TxtDescripcion.Text = .Item("descripcion_exp")
                End With
            End If

        End If
            End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim strEmpresa, strCiudad, strFuncion, strDescripcion, strCese As String
        Dim FechaIni, FechaFin As Date
        Dim intContrato, intcargo, anioFin, valor As Int16
        Dim ObjPersonal As New Personal
        ObjPersonal.codigo = Session("id")
        Try
            strEmpresa = Me.TxtEmpresa.Text
            strCiudad = Me.TxtCiudad.Text
            strFuncion = Me.TxtFuncion.Text
            strDescripcion = Me.TxtDescripcion.Text
            strCese = Me.DDLCese.SelectedValue
            intContrato = Me.DDLContrato.SelectedValue
            intcargo = Me.DDLCargo.SelectedValue
            FechaIni = CDate("01/" & Me.DDLMesIni.SelectedValue & "/" & Me.DDLAnioIni.SelectedValue)
            anioFin = Me.DDLAnioFin.SelectedValue
            If anioFin = 0 Then
                FechaFin = #1/1/1900#
            Else
                FechaFin = CDate("01/" & Me.DDLMesFin.SelectedValue & "/" & Me.DDLAnioFin.SelectedValue)
            End If
            If Request.QueryString("codigo_exp") = "" Then
                valor = ObjPersonal.GrabarExperiencia(intcargo, strFuncion, FechaIni, FechaFin, strDescripcion, intContrato, strCese, strCiudad, strEmpresa)
            Else
                valor = ObjPersonal.Modificarexperiencia(intcargo, strFuncion, FechaIni, FechaFin, strDescripcion, intContrato, strCese, strCiudad, strEmpresa, CInt(Request.QueryString("codigo_exp")))
            End If

            If valor = -1 Then
                Me.LblError.Text = "Ocurrio un error, intentelo nuevamente"
                Exit Sub
            End If
        Catch ex As Exception
            Me.LblError.Text = "Ocurrio un error, intentelo nuevamente"
            ObjPersonal = Nothing
        End Try
        ObjPersonal = Nothing
        Response.Write("<script>window.opener.location='experiencia.aspx?id=" & Session("id") & "';window.close();</script>")
    End Sub
End Class

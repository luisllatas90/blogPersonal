
Partial Class medicina_frmevaluaciones
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        For i As Int16 = 1 To 5
            Dim ChekVer As CheckBox = CType(FindControl("ChkVer_" & i.ToString), CheckBox)
            Dim TxtFecha As TextBox = CType(FindControl("TxtFechaIni_" & i.ToString), TextBox)
            Dim TxtPonderado As TextBox = CType(FindControl("TxtPeso_" & i.ToString), TextBox)
            Dim TxtNombre As TextBox = CType(FindControl("TxtNombre_" & i.ToString), TextBox)
            Dim ChkEst As CheckBox = CType(FindControl("ChkEst_" & i.ToString), CheckBox)

            TxtPonderado.Attributes.Add("OnKeyPress", "numeros();")
            TxtFecha.Attributes.Add("OnKeyDown", "javascript:return false;")
            ChekVer.Attributes.Add("OnClick", "activacajas(this," & i.ToString & ")")

            TxtPonderado.Attributes.Add("disabled", "disabled")
            TxtFecha.Attributes.Add("disabled", "disabled")
            ChkEst.Attributes.Add("disabled", "disabled")
            TxtNombre.Attributes.Add("disabled", "disabled")
        Next

        If IsPostBack = False Then
            If Request.QueryString("e") = "m" Then
                Me.LblTitulos.Text = "Actualizar Evaluacion"
                Dim Datos As New Data.DataTable
                Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                Datos = ObjDatos.TraerDataTable("MED_ConsultarEvaluacionDescripcion", Request.QueryString("act"))
                Me.ChkVer_1.Checked = True

                Me.TxtNombre_1.Text = Datos.Rows(0).Item("nombre")
                Me.TxtFechaIni_1.Text = CDate(Datos.Rows(0).Item("fechaini_act")).ToShortDateString
                Me.TxtPeso_1.Text = Datos.Rows(0).Item("peso_Act").ToString

                If Datos.Rows(0).Item("estado_act").ToString = "Habilitado" Then
                    Me.ChkEst_1.Checked = True
                Else
                    Me.ChkEst_1.Checked = False
                End If

                Me.TxtNombre_1.Attributes.Remove("disabled")
                Me.TxtFechaIni_1.Attributes.Remove("disabled")
                Me.TxtPeso_1.Attributes.Remove("disabled")
                Me.ChkEst_1.Attributes.Remove("disabled")

                Me.ChkVer_2.Enabled = False
                Me.ChkVer_3.Enabled = False
                Me.ChkVer_4.Enabled = False
                Me.ChkVer_5.Enabled = False

                If Datos.Rows(0).Item("estadorealizado_Act").ToString = "S" Then
                    Me.CmdGuardar.Enabled = False
                    Me.LblMensaje.Text = "No se puede modificar una Evaluación que ya se ha realizado."
                    Me.LblMensaje.ForeColor = Drawing.Color.Red
                End If
            Else
                Me.LblTitulos.Text = "Registrar Evaluaciónes"
            End If
        End If

        

        
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click

        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        If Request.QueryString("e") = "m" Then
            Try
                ObjDatos.IniciarTransaccion()
                ObjDatos.Ejecutar("MEd_ActualizarEvaluacion", Request.QueryString("act"), Request.QueryString("syl"), Me.TxtNombre_1.Text.Trim, 1, _
                CDate(Me.TxtFechaIni_1.Text), CDate(Me.TxtFechaIni_1.Text), (CInt(Me.ChkEst_1.Checked) * -1).ToString, 4, 1, CDbl(Me.TxtPeso_1.Text))
                ObjDatos.TerminarTransaccion()
                Response.Write("<script>window.opener.location.reload();window.close();</script>")
                'Response.Write(Request.QueryString("acr") & "<br>" & Request.QueryString("syl") & "<br>" & Me.TxtNombre_1.Text)
            Catch ex As Exception
                ObjDatos.AbortarTransaccion()
                Me.LblMensaje.ForeColor = Drawing.Color.Blue
                Me.LblMensaje.Text = "Ocurrio un error al procesar los datos. "
            End Try
        Else
            Try
                ObjDatos.IniciarTransaccion()
                For i As Int16 = 1 To 5
                    Dim ChekVer As CheckBox = CType(FindControl("ChkVer_" & i.ToString), CheckBox)
                    Dim CajaTextoNombre As TextBox = CType(FindControl("TxtNombre_" & i.ToString), TextBox)
                    Dim CajaTextoFecha As TextBox = CType(FindControl("TxtFechaIni_" & i.ToString), TextBox)
                    Dim CajaTExtoPEso As TextBox = CType(FindControl("TxtPeso_" & i.ToString), TextBox)
                    Dim ChkEstado As CheckBox = CType(FindControl("ChkEst_" & i.ToString), CheckBox)
                    If ChekVer.Checked = True Then
                        ObjDatos.Ejecutar("MED_InsertarEvaluaciones", Request.QueryString("syl"), CajaTextoNombre.Text, Request.QueryString("pa"), _
                        1, CDate(CajaTextoFecha.Text), CDate(CajaTextoFecha.Text), (CInt(ChkEstado.Checked) * -1).ToString, 4, 1, CDbl(CajaTExtoPEso.Text), 0)
                    End If
                Next
                ObjDatos.TerminarTransaccion()
                Response.Write("<script>window.opener.location.reload();window.close();</script>")
            Catch ex As Exception
                ObjDatos.AbortarTransaccion()
                Me.LblMensaje.ForeColor = Drawing.Color.Red
                Me.LblMensaje.Text = "Ocurrio un error al procesar los datos. " & ex.Message
            End Try
        End If
    End Sub
End Class

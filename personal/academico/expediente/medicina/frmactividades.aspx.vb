
Partial Class medicina_frmactividades
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.TxtFechaIni.Attributes.Add("OnKeyDown", "javascript: return false;")
            Me.TxtHoraIni.Attributes.Add("OnKeyPress", "numeros();")
            Me.TxtMinutoIni.Attributes.Add("OnKeyPress", "numeros();")

            LlenaCombos()
            If Request.QueryString("e") = "m" Then
                Me.LblTitulos.Text = "Actualizar Actividades"
                Dim Datos As New Data.DataTable
                Dim Hora As Integer
                Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                Datos = ObjDatos.TraerDataTable("MED_ConsultarActividadDescripcion", Request.QueryString("act"))
                Me.TxtNombre.Text = Datos.Rows(0).Item("nombre")
                Me.DDLTipoActividad.SelectedValue = Datos.Rows(0).Item("codtipodescripcion")
                Me.DDLSemana.SelectedValue = Datos.Rows(0).Item("semana_act")
                Me.TxtFechaIni.Text = CDate(Datos.Rows(0).Item("fechaini_act")).ToShortDateString

                Hora = CDate(Datos.Rows(0).Item("fechaini_act")).Hour

                If Hora > 12 Then
                    Me.TxtHoraIni.Text = (Hora - 12).ToString
                    Me.DDlHorario.SelectedValue = 2
                Else
                    Me.TxtHoraIni.Text = Hora.ToString
                    Me.DDlHorario.SelectedValue = 1
                End If

                Me.TxtMinutoIni.Text = CDate(Datos.Rows(0).Item("fechaini_act")).Minute.ToString
                If Datos.Rows(0).Item("estado_act").ToString = "Habilitado" Then
                    Me.ChkEstado.Checked = True
                Else
                    Me.ChkEstado.Checked = False
                End If

                If Datos.Rows(0).Item("estadorealizado_Act").ToString = "S" Then
                    Me.CmdGuardar.Enabled = False
                    Me.LblMensaje.Text = "No se puede modificar una Actividad que ya se ha realizado."
                    Me.LblMensaje.ForeColor = Drawing.Color.Red
                End If
            Else
                Me.LblTitulos.Text = "Registrar una Actividad de " & Request.QueryString("txtact")
            End If
        End If
    End Sub

    Private Sub LlenaCombos()
        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(Me.DDLTipoActividad, ObjDatos.TraerDataTable("MED_ConsultarTipoActividad", "A"), "codigo_tac", "descripcion_act", "-- Selecione Tipo Actividad --")
       
        Me.DDLSemana.Items.Add("- Semana -")
        Me.DDLSemana.Items(0).Value = 0
        For i As Int16 = 1 To 17
            Me.DDLSemana.Items.Add(i)
        Next
        ObjDatos = Nothing

    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim fechaini As DateTime
        Dim Horainicio As Integer

        If Me.DDlHorario.SelectedValue = 1 Then
            If CInt(Me.TxtHoraIni.Text) = 12 Then
                Horainicio = 0
            Else
                Horainicio = CInt(Me.TxtHoraIni.Text)
            End If
        Else
            Horainicio = CInt(Me.TxtHoraIni.Text)
            If Horainicio = 12 Then
                Horainicio = 12
            Else
                Horainicio = Horainicio + 12
            End If
        End If

        fechaini = CDate(Me.TxtFechaIni.Text & " " & Horainicio.ToString & ":" & Me.TxtMinutoIni.Text)

        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        If Request.QueryString("e") = "m" Then
            Try
                ObjDatos.IniciarTransaccion()
                ObjDatos.Ejecutar("MEd_ActualizarActividades", Request.QueryString("act"), Request.QueryString("syl"), Me.TxtNombre.Text.Trim, Me.DDLSemana.SelectedValue, _
                fechaini, fechaini, (CInt(Me.ChkEstado.Checked) * -1).ToString, Me.DDLTipoActividad.SelectedValue)
                ObjDatos.TerminarTransaccion()
                Response.Write("<script>window.opener.location.reload();window.close();</script>")
            Catch ex As Exception
                ObjDatos.AbortarTransaccion()
                Me.LblMensaje.ForeColor = Drawing.Color.Blue
                Me.LblMensaje.Text = "Ocurrio un error al procesar los datos. "
            End Try
        Else
            Try
                ObjDatos.IniciarTransaccion()
                ObjDatos.Ejecutar("MED_InsertarActividades", Request.QueryString("syl"), Me.TxtNombre.Text.Trim, Request.QueryString("pa"), _
                Me.DDLSemana.SelectedValue, fechaini, fechaini, (CInt(Me.ChkEstado.Checked) * -1).ToString, Me.DDLTipoActividad.SelectedValue, 0)
                ObjDatos.TerminarTransaccion()
                Response.Write("<script>window.opener.location.reload();window.close();</script>")
            Catch ex As Exception
                ObjDatos.AbortarTransaccion()
                Me.LblMensaje.ForeColor = Drawing.Color.Blue
                Me.LblMensaje.Text = "Ocurrio un error al procesar los datos. " & ex.Message
            End Try
        End If
    End Sub
End Class

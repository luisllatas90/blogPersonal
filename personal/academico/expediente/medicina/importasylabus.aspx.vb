
Partial Class medicina_importasylabus
    Inherits System.Web.UI.Page

    Public ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim objDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings(1).ConnectionString)
        Dim objCombo As New ClsFunciones
        If IsPostBack = False Then
            ClsFunciones.LlenarListas(Me.DDLCiclo, objDatos.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")

        End If
    End Sub

    Protected Sub DDLCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLCiclo.SelectedIndexChanged
        'Dim objDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings(1).ConnectionString)
        Dim objCombo As New ClsFunciones
        ClsFunciones.LlenarListas(Me.RbSylabus, objDatos.TraerDataTable("MED_ConsultarSylabusImportar", Me.DDLCiclo.SelectedValue, Request.QueryString("codigo_per")), "codigo_syl", "nombre_cur")
    End Sub


    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim Datos As New Data.DataTable
        Dim Codigo_syl As Integer

        If Me.RbSylabus.Items.Count = 0 Then
            Me.LblMensaje.ForeColor = Drawing.Color.Red
            Me.LblMensaje.Text = "No existen sylabus para importar"
        ElseIf Me.RbSylabus.SelectedIndex = -1 Then
            Me.LblMensaje.ForeColor = Drawing.Color.Red
            Me.LblMensaje.Text = "Debe seleccionar un sylabus para poder importar."
        Else
            Try
                ObjDatos.IniciarTransaccion()
                Codigo_syl = ObjDatos.Ejecutar("MED_ImportarSylabus", Me.RbSylabus.SelectedValue, Me.Request.QueryString("codigo_cup"), 0)
                Dim codigo_Actinsertar As Integer

                If Me.ChkActividades.Checked = True Then
                    Datos = ObjDatos.TraerDataTable("MED_COnsultarActividades", 0, Me.RbSylabus.SelectedValue, "A")
                    For i As Integer = 0 To Datos.Rows.Count - 1
                        codigo_Actinsertar = ObjDatos.Ejecutar("MED_InsertarActividades", Codigo_syl, Datos.Rows(i).Item("descripcion_act"), 0, _
                        1, Datos.Rows(i).Item("fechaini_act"), Datos.Rows(i).Item("fechafin_act"), (CInt(True) * -1).ToString, Datos.Rows(i).Item("codigo_tac"), 0)
                        InsertarActividades(Me.RbSylabus.SelectedValue, Codigo_syl, Datos.Rows(i).Item("codigo_Act"), codigo_Actinsertar)
                    Next
                    Datos.Dispose()
                    Datos = Nothing
                End If

                If Me.ChkEvaluaciones.Checked = True Then
                    Datos = ObjDatos.TraerDataTable("MED_COnsultarActividades", 0, Me.RbSylabus.SelectedValue, "E")
                    For i As Int32 = 0 To Datos.Rows.Count - 1
                        codigo_Actinsertar = ObjDatos.Ejecutar("MED_InsertarEvaluaciones", Codigo_syl, "Evaluaciones", 0, 1, Now, Now, (CInt(True) * -1).ToString, 4, 1, 1, 0)
                        InsertarEvaluaciones(Me.RbSylabus.SelectedValue, Codigo_syl, Datos.Rows(i).Item("codigo_act"), codigo_Actinsertar)
                    Next
                    Datos.Dispose()
                    Datos = Nothing
                Else
                    Dim Codigo_Act As Integer
                    codigo_act = ObjDatos.Ejecutar("MED_InsertarEvaluaciones", Codigo_syl, "Evaluaciones", 0, 1, Now, Now, (CInt(True) * -1).ToString, 4, 1, 1, 0)

                    ObjDatos.Ejecutar("MED_InsertarEvaluaciones", Codigo_syl, "Cognitivo", codigo_act, 1, Now, Now, (CInt(True) * -1).ToString, 4, 1, 1, 0)
                    ObjDatos.Ejecutar("MED_InsertarEvaluaciones", Codigo_syl, "Actitudinal", codigo_act, 1, Now, Now, (CInt(True) * -1).ToString, 4, 1, 1, 0)
                    ObjDatos.Ejecutar("MED_InsertarEvaluaciones", Codigo_syl, "Procedimental", Codigo_Act, 1, Now, Now, (CInt(True) * -1).ToString, 4, 1, 1, 0)

                End If

                ObjDatos.TerminarTransaccion()
                Response.Write("<script>alert('Se guardaron los datos correctamente.'); window.opener.location.reload(); window.close(); </script>")

            Catch ex As Exception
                ObjDatos.AbortarTransaccion()
                Me.LblMensaje.ForeColor = Drawing.Color.Red
                Me.LblMensaje.Text = "Ocurrio un error al procesar los datos, intentelo nuevamente."
            End Try
        End If

    End Sub


    Private Sub InsertarEvaluaciones(ByVal Codigo_syl_antiguo As Integer, ByVal codigosyl_nuevo As Integer, ByVal codigo_act As Integer, ByVal codigo_ActInsertar As Integer)
        Dim Tabla As New Data.DataTable
        Dim Codigoinsertaractividad As Integer
        Tabla = ObjDatos.TraerDataTable("MED_COnsultarActividades", codigo_act, Codigo_syl_antiguo, "E")
        For i As Int32 = 0 To Tabla.Rows.Count - 1
            Codigoinsertaractividad = ObjDatos.Ejecutar("MED_InsertarEvaluaciones", codigosyl_nuevo, Tabla.Rows(i).Item("descripcion_act"), codigo_ActInsertar, 1, CDate(Tabla.Rows(i).Item("fechaini_act")), CDate(Tabla.Rows(i).Item("fechaini_act")), (CInt(True) * -1).ToString, 4, 1, 1, 0)
            InsertarEvaluaciones(Codigo_syl_antiguo, codigosyl_nuevo, Tabla.Rows(i).Item("codigo_act"), Codigoinsertaractividad)
        Next
        Tabla.Dispose()
        Tabla = Nothing
    End Sub



    Private Sub InsertarActividades(ByVal codigo_syl_antiguo As Integer, ByVal Codigosyl_nuevo As Integer, ByVal codigo_act As Integer, ByVal codigo_ActInsertar As Integer)
        Dim Tabla As New Data.DataTable
        Dim Codigoinsertaractividad As Integer
        Tabla = ObjDatos.TraerDataTable("MED_COnsultarActividades", codigo_act, codigo_syl_antiguo, "A")
        For i As Int32 = 0 To Tabla.Rows.Count - 1

            Codigoinsertaractividad = ObjDatos.Ejecutar("MED_InsertarActividades", Codigosyl_nuevo, Tabla.Rows(i).Item("descripcion_act"), codigo_ActInsertar, _
            1, Tabla.Rows(i).Item("fechaini_act"), Tabla.Rows(i).Item("fechafin_act"), (CInt(True) * -1).ToString, Tabla.Rows(i).Item("codigo_tac"), 0)

            InsertarActividades(codigo_syl_antiguo, Codigosyl_nuevo, Tabla.Rows(i).Item("codigo_Act"), Codigoinsertaractividad)
        Next
        Tabla.Dispose()
        Tabla = Nothing
    End Sub

End Class

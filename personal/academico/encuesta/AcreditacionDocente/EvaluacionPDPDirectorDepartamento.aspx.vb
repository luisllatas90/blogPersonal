'**************************************************************
'Desarrollado por hreyes 22/09/09
'Formulario de evaluación del PDP docente
'@sistema = Campus Virtual
'@modulo = Evaluación docente
'**************************************************************
Partial Class Encuesta_AcreditacionDocente_EvaluacionPDPDirectorDepartamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objcnx As New ClsConectarDatos
            Dim objFun As New ClsFunciones
            Dim codigo_tfu As Int16
            Dim datosDepAcad As Data.DataTable
            'Try
            lblFecha.Text = Date.Now
            Dim funciones As New ClsFunciones
            Dim datos As New Data.DataTable
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            codigo_tfu = Request.QueryString("ctf")
            objcnx.AbrirConexion()
            If codigo_tfu = 1 Or codigo_tfu = 7 Or codigo_tfu = 16 Then
                ClsFunciones.LlenarListas(Me.cboDepartamento, objcnx.TraerDataTable("ConsultarDepartamentoAcademico", "AL", ""), "codigo_dac", "nombre_dac")
            Else
                datosDepAcad = objcnx.TraerDataTable("ConsultarAccesoRecurso", "11", Request.QueryString("id"), "", "")
                If datosDepAcad.Rows.Count > 0 Then
                    objFun.CargarListas(Me.cboDepartamento, datosDepAcad, "codigo_Dac", "Nombre")
                    cboDepartamento_SelectedIndexChanged(sender, e)
                Else
                    cboDepartamento.Items.Clear()
                    cboDepartamento.Items.Add(">>No definido<<")
                    cboDepartamento.Items(0).Value = -1
                End If
            End If
            ' funciones.CargarListas(cboDepartamento, objcnx.TraerDataTable("EAD_ConsultarPermisosEvaluacionPDP", Request.QueryString("id")), "codigo_dac", "nombre_dac")
            objcnx.CerrarConexion()
            cmdGuardarAbajo.Visible = False
            cmdGuardarArriba.Visible = False
            If cboDepartamento.Items.Count > 0 Then
                cboDepartamento_SelectedIndexChanged(sender, e)
            End If
            'Catch ex As Exception
            '    Response.Write(ex.Message)
            '    'ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error al procesar los datos')", True)
            'End Try
            objcnx = Nothing
        End If
    End Sub

    Protected Sub cboDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDepartamento.SelectedIndexChanged
        Dim objcnx As New ClsConectarDatos
        Try
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()
            gvdocentes.DataSource = objcnx.TraerDataTable("EAD_ConsultarPDP", cboDepartamento.SelectedValue)
            objcnx.CerrarConexion()
            gvdocentes.DataBind()
            cmdGuardarArriba.Visible = True
            cmdGuardarAbajo.Visible = True
        Catch ex As Exception
            Response.Write(ex.Message)
            'ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error al procesar los datos')", True)
        End Try
        objcnx = Nothing
    End Sub

    Protected Sub gvdocentes_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvdocentes.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            '.:: Declaracion de variables ::.'
            Dim fila As Data.DataRowView
            Dim funciones As New ClsFunciones
            Dim ListRbt As New RadioButtonList ' lista de opciones para pregunta pdp a tiempo
            Dim comboCoherencia As New DropDownList ' opciones para coherencia de pdp
            Dim comboCumplimiento As New DropDownList ' opciones para cumplimiento de pdp

            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Style.Add("cursor", "hand")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            'Llenar opciones presento pdp a tiempo
            ListRbt.Items.Add("Si")
            ListRbt.Items(0).Value = "S"
            ListRbt.Items.Add("No")
            ListRbt.Items(1).Value = "N"
            ListRbt.RepeatDirection = RepeatDirection.Horizontal
            ListRbt.ID = "combo"
            'Response.Write(e.Row.Cells(4).Text)
            If e.Row.Cells(4).Text <> "" Then
                ListRbt.SelectedValue = e.Row.Cells(4).Text.Trim
            End If
            e.Row.Cells(4).Controls.Add(ListRbt)

            'Llenar opciones coherencia del pdp
            comboCoherencia.Items.Add("--Seleccione una opción--")
            comboCoherencia.Items(0).Value = 0
            comboCoherencia.Items.Add("Las necesidades de la escuela")
            comboCoherencia.Items(1).Value = 1
            comboCoherencia.Items.Add("Las necesidades del Dpto. Académico")
            comboCoherencia.Items(2).Value = 2
            comboCoherencia.Items.Add("Sus necesidades personales")
            comboCoherencia.Items(3).Value = 3
            comboCoherencia.ID = "CboCoherencia"
            If e.Row.Cells(5).Text <> "" Then
                funciones.Seleccionar(e.Row.Cells(5).Text.Trim, comboCoherencia)
            End If
            e.Row.Cells(5).Controls.Add(comboCoherencia)

            'Llenar opciones de cumplimiento del pdp
            comboCumplimiento.Items.Add("--Seleccione una opción--")
            comboCumplimiento.Items(0).Value = 0
            comboCumplimiento.Items.Add("En su totalidad")
            comboCumplimiento.Items(1).Value = 1
            comboCumplimiento.Items.Add("En forma parcial")
            comboCumplimiento.Items(2).Value = 2
            comboCumplimiento.Items.Add("No cumplió")
            comboCumplimiento.Items(3).Value = 3
            comboCumplimiento.ID = "cboCumplimiento"
            If e.Row.Cells(6).Text <> "" Then
                funciones.Seleccionar(e.Row.Cells(6).Text.Trim, comboCumplimiento)
            End If
            e.Row.Cells(6).Controls.Add(comboCumplimiento)
        End If
    End Sub

    Protected Sub gvdocentes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvdocentes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            '.:: Declaracion de variables ::.'
            Dim fila As Data.DataRowView
            Dim funciones As New ClsFunciones
            Dim chek As New CheckBox ' indica si el pdp fue subido por campus virtual
            Dim ListRbt As New RadioButtonList ' lista de opciones para pregunta pdp a tiempo
            Dim comboCoherencia As New DropDownList ' opciones para coherencia de pdp
            Dim comboCumplimiento As New DropDownList ' opciones para cumplimiento de pdp

            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Style.Add("cursor", "hand")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            'Marcar check
            If fila.Row.Item("presentopdp") = "" Then
                chek.Checked = False
            Else
                chek.Checked = True
            End If
            chek.Enabled = False
            e.Row.Cells(1).Controls.Add(chek)

            'Llenar opciones presento pdp a tiempo
            ListRbt.Items.Add("Si")
            ListRbt.Items(0).Value = "S"
            ListRbt.Items.Add("No")
            ListRbt.Items(1).Value = "N"
            ListRbt.RepeatDirection = RepeatDirection.Horizontal
            ListRbt.ID = "combo"

            If e.Row.Cells(4).Text <> "" Then
                ListRbt.SelectedValue = e.Row.Cells(4).Text.Trim
            End If
            e.Row.Cells(4).Controls.Add(ListRbt)


            'Llenar opciones coherencia del pdp
            comboCoherencia.Items.Add("--Seleccione una opción--")
            comboCoherencia.Items(0).Value = 0
            comboCoherencia.Items.Add("Las necesidades de la escuela")
            comboCoherencia.Items(1).Value = 1
            comboCoherencia.Items.Add("Las necesidades del Dpto. Académico")
            comboCoherencia.Items(2).Value = 2
            comboCoherencia.Items.Add("Sus necesidades personales")
            comboCoherencia.Items(3).Value = 3
            comboCoherencia.ID = "CboCoherencia"

            If e.Row.Cells(5).Text <> "" Then
                funciones.Seleccionar(e.Row.Cells(5).Text.Trim, comboCoherencia)
            End If
            e.Row.Cells(5).Controls.Add(comboCoherencia)

            'Llenar opciones de cumplimiento del pdp
            comboCumplimiento.Items.Add("--Seleccione una opción--")
            comboCumplimiento.Items(0).Value = 0
            comboCumplimiento.Items.Add("En su totalidad")
            comboCumplimiento.Items(1).Value = 1
            comboCumplimiento.Items.Add("En forma parcial")
            comboCumplimiento.Items(2).Value = 2
            comboCumplimiento.Items.Add("No cumplió")
            comboCumplimiento.Items(3).Value = 3
            comboCumplimiento.ID = "cboCumplimiento"

            If e.Row.Cells(6).Text <> "" Then
                funciones.Seleccionar(e.Row.Cells(6).Text.Trim, comboCumplimiento)
            End If
            e.Row.Cells(6).Controls.Add(comboCumplimiento)
        End If
    End Sub

    Protected Sub cmdGuardarArriba_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarArriba.Click, cmdGuardarAbajo.Click
        Dim i, j, num As Int16
        Dim codigo_per As Int32
        Dim objcnx As New ClsConectarDatos
        objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        num = 0
        Try
            Dim lista As New RadioButtonList
            Dim comboCoherencia As New DropDownList
            Dim comboCumplimiento As New DropDownList

            For i = 1 To gvdocentes.Rows.Count
                lista = gvdocentes.Controls(0).Controls(i).Controls(4).Controls(j)
                comboCoherencia = gvdocentes.Controls(0).Controls(i).Controls(5).Controls(j)
                comboCumplimiento = gvdocentes.Controls(0).Controls(i).Controls(6).Controls(j)

                'Guardo los datos de todos los que han sido respondidos
                If lista.SelectedValue <> "" And comboCoherencia.SelectedValue > 0 And comboCumplimiento.SelectedValue > 0 Then
                    objcnx.IniciarTransaccion()
                    codigo_per = gvdocentes.DataKeys.Item(i - 1).Values(0)
                    objcnx.Ejecutar("EAD_AgregarEvaluacionAPDP", lista.SelectedValue, comboCoherencia.SelectedItem.Text, _
                                    comboCumplimiento.SelectedItem.Text, comboCoherencia.SelectedValue, _
                                    comboCumplimiento.SelectedValue, codigo_per)
                    objcnx.TerminarTransaccion()
                    num += 1
                End If
            Next
            cboDepartamento_SelectedIndexChanged(sender, e)
            If num > 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "correcto", "alert('Se guardaron correctamente los datos')", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "correcto", "alert('Usted debe responder las tres preguntas de cada profesor para guardar sus datos')", True)
            End If
        Catch ex As Exception
            objcnx.AbortarTransaccion()
            Response.Write(ex.Message)
            'ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error al procesar los datos')", True)
        End Try
        objcnx = Nothing
    End Sub
End Class

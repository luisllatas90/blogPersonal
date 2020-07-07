
Partial Class DirectorDepartamento_administrarpersonallineas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim ObjArbol As New Investigacion
            ClsFunciones.LlenarListas(Me.DDLUnidad, ObjArbol.ConsultarUnidadesInvestigacion("1D", Request.QueryString("id")), 0, 1, "---- Seleccione Unidad de Investigacion ----")
            ObjArbol = Nothing
        End If
    End Sub

    Protected Sub DDLUnidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLUnidad.SelectedIndexChanged
        Dim ObjArbol As New Investigacion
        Me.HddCodigoCco.Value = Me.DDLUnidad.SelectedValue
        ClsFunciones.LlenarListas(Me.DDLArea, ObjArbol.ConsultarUnidadesInvestigacion_New(0, Me.HddCodigoCco.Value), 0, 1, "---- Seleccione Area de Investigación ----")
        Me.LstFaltantes.Items.Clear()
        Me.LstActivos.Items.Clear()
        Me.LblTematica.Visible = False
        Me.DDLTematica.Visible = False
        ObjArbol = Nothing
    End Sub

    Protected Sub DDLArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLArea.SelectedIndexChanged
        Dim ObjArbol As New Investigacion
        ClsFunciones.LlenarListas(Me.DDLLinea, ObjArbol.ConsultarUnidadesInvestigacion_New(Me.DDLArea.SelectedValue, Me.HddCodigoCco.Value), 0, 1, "---- Seleccione Linea de Investigación ----")
        Me.LstFaltantes.Items.Clear()
        Me.LstActivos.Items.Clear()
        Me.LblTematica.Visible = False
        Me.DDLTematica.Visible = False
        ObjArbol = Nothing
    End Sub

    Protected Sub DDLLinea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLLinea.SelectedIndexChanged
        Dim ObjInv As New Investigacion
        Dim Tematicas As New Data.DataTable
        Tematicas = ObjInv.ConsultarUnidadesInvestigacion_New(Me.DDLLinea.SelectedValue, Me.HddCodigoCco.Value)
        If Tematicas.Rows.Count > 0 Then
            Me.LblTematica.Visible = True
            Me.DDLTematica.Visible = True
            ClsFunciones.LlenarListas(DDLTematica, Tematicas, 0, 1, "---- Seleccione Tematica ----")

        Else
            Me.LblTematica.Visible = False
            Me.DDLTematica.Visible = False
            CargarPErsonal(Me.DDLLinea.SelectedValue)
        End If
        ObjInv = Nothing
    End Sub

    Protected Sub CmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAgregar.Click
        Dim lista As New SortedList
        For i As Int32 = 0 To Me.LstFaltantes.GetSelectedIndices.Length - 1
            lista.Add(i, Me.LstFaltantes.Items(Me.LstFaltantes.GetSelectedIndices.GetValue(i)).Value)
        Next
        Dim objinv As New Investigacion
        Dim Codigo_are As Integer

        If Me.DDLTematica.Visible = True Then
            Codigo_are = Me.DDLTematica.SelectedValue
        Else
            Codigo_are = Me.DDLLinea.SelectedValue
        End If

        If objinv.AgregarPersonalALinea(lista, Codigo_are) <> -1 Then
            ClsFunciones.LlenarListas(Me.LstFaltantes, objinv.ConsultarPersonaldeLineaInvestigacion(Me.HddCodigoCco.Value, Codigo_are, 2), "codigo_pcc", "datos_per")
            ClsFunciones.LlenarListas(Me.LstActivos, objinv.ConsultarPersonaldeLineaInvestigacion(Me.HddCodigoCco.Value, Codigo_are, 1), "codigo_pcc", "datos_per")

            If Me.LstFaltantes.Items.Count = 0 Then
                Me.CmdAgregar.Enabled = False
            Else
                Me.CmdAgregar.Enabled = True
            End If
            If Me.LstActivos.Items.Count = 0 Then
                Me.CmdRetirar.Enabled = False
            Else
                Me.CmdRetirar.Enabled = True
            End If

            Me.Label1.ForeColor = Drawing.Color.Blue
            Me.Label1.Text = "Se agregaron " & lista.Count.ToString & " personas con satisfacción."
        Else
            Me.Label1.ForeColor = Drawing.Color.Red
            Me.Label1.Text = "Ocurrió un error al procesar los datos, intentelo nuevamente"
        End If
        objinv = Nothing
    End Sub

    Protected Sub CmdRetirar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdRetirar.Click
        Dim lista As New SortedList
        For i As Int32 = 0 To Me.LstActivos.GetSelectedIndices.Length - 1
            lista.Add(i, Me.LstActivos.Items(Me.LstActivos.GetSelectedIndices.GetValue(i)).Value)
        Next
        Dim Codigo_are As Integer
        If Me.DDLTematica.Visible = True Then
            Codigo_are = Me.DDLTematica.SelectedValue
        Else
            Codigo_are = Me.DDLLinea.SelectedValue
        End If

        Dim objinv As New Investigacion
        If objinv.RetirarPersonaldelinea(lista, Codigo_are) <> -1 Then
            ClsFunciones.LlenarListas(Me.LstFaltantes, objinv.ConsultarPersonaldeLineaInvestigacion(Me.HddCodigoCco.Value, Codigo_are, 2), "codigo_pcc", "datos_per")
            ClsFunciones.LlenarListas(Me.LstActivos, objinv.ConsultarPersonaldeLineaInvestigacion(Me.HddCodigoCco.Value, Codigo_are, 1), "codigo_pcc", "datos_per")
            If Me.LstFaltantes.Items.Count = 0 Then
                Me.CmdAgregar.Enabled = False
            Else
                Me.CmdAgregar.Enabled = True
            End If
            If Me.LstActivos.Items.Count = 0 Then
                Me.CmdRetirar.Enabled = False
            Else
                Me.CmdRetirar.Enabled = True
            End If
            Me.Label1.ForeColor = Drawing.Color.Blue
            Me.Label1.Text = "Se Retiraron " & lista.Count.ToString & " personas con satisfacción."
        Else
            Me.Label1.ForeColor = Drawing.Color.Red
            Me.Label1.Text = "Ocurrió un error al procesar los datos, intentelo nuevamente"
        End If
        objinv = Nothing
    End Sub

    Protected Sub DDLTematica_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLTematica.SelectedIndexChanged
        CargarPErsonal(Me.DDLTematica.SelectedValue)
    End Sub

    Protected Sub CargarPErsonal(ByVal codigo_are As Integer)
        Dim ObjInv As New Investigacion
        ClsFunciones.LlenarListas(Me.LstFaltantes, ObjInv.ConsultarPersonaldeLineaInvestigacion(Me.HddCodigoCco.Value, codigo_are, 2), "codigo_pcc", "datos_per")
        ClsFunciones.LlenarListas(Me.LstActivos, ObjInv.ConsultarPersonaldeLineaInvestigacion(Me.HddCodigoCco.Value, codigo_are, 1), "codigo_pcc", "datos_per")
        If Me.LstFaltantes.Items.Count = 0 Then
            Me.CmdAgregar.Enabled = False
        Else
            Me.CmdAgregar.Enabled = True
        End If
        If Me.LstActivos.Items.Count = 0 Then
            Me.CmdRetirar.Enabled = False
        Else
            Me.CmdRetirar.Enabled = True
        End If
        Me.Label1.Text = ""
        ObjInv = Nothing

    End Sub

End Class

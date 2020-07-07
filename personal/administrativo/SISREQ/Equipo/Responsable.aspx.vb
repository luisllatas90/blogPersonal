
Partial Class Responsable
    Inherits System.Web.UI.Page

    Protected Sub frmresponsable_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmresponsable.Load
        If IsPostBack = False Then
            Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim datos As New Data.DataTable
            Dim id_sol As Int32
            id_sol = CInt(Request.QueryString("field"))
            datos = Objcnx.TraerDataTable("paReq_ConsultarSolicitud", id_sol)
            Me.LblSolicitud.Text = datos.Rows(0).Item("descripcion_sol").ToString.ToUpper
            Me.LblPrioridad.Text = datos.Rows(0).Item("prioridad").ToString.ToUpper
            Me.LblArea.Text = datos.Rows(0).Item("descripcion_cco").ToString.ToUpper
            Me.LblTipoSol.Text = datos.Rows(0).Item("descripcion_tsol").ToString.ToUpper
            Me.LblRegistradoPor.Text = datos.Rows(0).Item("registradopor_sol").ToString.ToUpper
            ClsFunciones.LlenarListas(Me.LstEquipo, Objcnx.TraerDataTable("paReq_ConsultarEquipo"), "codigo_per", "nombres")
            Objcnx = Nothing
        End If
    End Sub

    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click

        For i As Int32 = 0 To Me.LstEquipo.GetSelectedIndices.Length - 1
            Dim ItemList As New System.Web.UI.WebControls.ListItem
            ItemList.Value = Me.LstEquipo.Items(Me.LstEquipo.GetSelectedIndices.GetValue(i)).Value
            ItemList.Text = Me.LstEquipo.Items(Me.LstEquipo.GetSelectedIndices.GetValue(i)).Text
            Me.LstAsignados.Items.Add(ItemList)
        Next
        For i As Int32 = Me.LstEquipo.GetSelectedIndices.Length - 1 To 0 Step -1
            Me.LstEquipo.Items.RemoveAt(Me.LstEquipo.GetSelectedIndices.GetValue(i))
        Next
    End Sub

    Protected Sub CmdQuitar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdQuitar.Click

        For i As Int32 = 0 To Me.LstAsignados.GetSelectedIndices.Length - 1
            Dim ItemList As New System.Web.UI.WebControls.ListItem
            ItemList.Value = Me.LstAsignados.Items(Me.LstAsignados.GetSelectedIndices.GetValue(i)).Value
            ItemList.Text = Me.LstAsignados.Items(Me.LstAsignados.GetSelectedIndices.GetValue(i)).Text
            Me.LstEquipo.Items.Add(ItemList)
        Next
        For i As Int32 = Me.LstAsignados.GetSelectedIndices.Length - 1 To 0 Step -1
            Me.LstAsignados.Items.RemoveAt(Me.LstAsignados.GetSelectedIndices.GetValue(i))
        Next
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim id_sol As Int32
        id_sol = Request.QueryString("field")
        Try
            Objcnx.IniciarTransaccion()
            For i As Int16 = 0 To Me.LstAsignados.Items.Count - 1
                'Response.Write(Me.LstAsignados.Items.Item(i).Value + id_sol.ToString + "</br>")
                Objcnx.Ejecutar("paReq_InsertarResponsable", Me.LstAsignados.Items.Item(i).Value, id_sol.ToString)
                Objcnx.Ejecutar("paReq_UpdateBitacora_Codper", CInt(Request.QueryString("id")))
            Next
            If (Me.LstAsignados.Items.Count = 1) Then
                Objcnx.Ejecutar("paReq_ActualizarResponsableDeCronograma", 1, id_sol, Me.LstAsignados.Items.Item(0).Value)
                Objcnx.Ejecutar("paReq_CambiarEstadoSolicitud", id_sol)
            End If
            Objcnx.TerminarTransaccion()
            Response.Write("<script>alert('Se registraron correctamente los datos'); location.href='AsignarResponsable.aspx' </script>")
            'Response.Redirect("AsignarResponsable.aspx")
        Catch ex As Exception
            Objcnx.AbortarTransaccion()
            Response.Write("<script>alert('Ocurrio un error al procesar los datos ') </script>")
            Response.Write(ex.Message)
        End Try
        Objcnx = Nothing
    End Sub

End Class

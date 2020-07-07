
Partial Class Equipo_SeleccionarResponsable
    Inherits System.Web.UI.Page
    Public cod_per As Int32
    Public cod_sol As Int32
    Public asignar As Int32

    Protected Sub frmResponsable_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmResponsable.Load
        cod_per = Request.QueryString("id")
        cod_sol = Request.QueryString("field")
        If Not IsPostBack Then
            Dim rs As Data.DataTable
            Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            rs = Objcnx.TraerDataTable("paReq_ConsultarSolicitud", cod_sol)
            Me.lblSolicitud.Text = rs.Rows(0).Item("descripcion_sol").ToString.ToUpper
            Me.lblTipo.Text = rs.Rows(0).Item("descripcion_tsol").ToString.ToUpper
            Me.lblArea.Text = rs.Rows(0).Item("descripcion_cco").ToString.ToUpper
            Me.lblModulo.Text = rs.Rows(0).Item("descripcion_apl").ToString.ToUpper
            Me.lblPrioridad.Text = rs.Rows(0).Item("Prioridad").ToString.ToUpper
            Me.lblRegistradoPor.Text = rs.Rows(0).Item("registradopor_sol").ToString.ToUpper
            rs.Clear()
            rs = Nothing
            rs = Objcnx.TraerDataTable("paReq_EquipoAsignadaASolicitud", cod_sol)

            If rs.Rows.Count > 0 Then
                Dim i As Int16 = 0
                Dim EsResponsable As Int16 = 0
                Do While (i < rs.Rows.Count)
                    Me.RblEquipo.Items.Add(rs.Rows(i).Item("nombres").ToString)
                    Me.RblEquipo.Items(i).Value = rs.Rows(i).Item("codigo_per")
                    'Me.RblEquipo.Items(i).Value = rs.Rows(i).Item("id_solequ")
                    EsResponsable = rs.Rows(i).Item("responsable")
                    If EsResponsable = 1 Then
                        Me.RblEquipo.SelectedIndex = i
                    End If
                    i += 1
                Loop
            End If
        End If
    End Sub

    Protected Sub CmdCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdCerrar.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            ObjCnx.IniciarTransaccion()
            With RblEquipo
                For i As Int16 = 0 To .Items.Count - 1
                    If .Items(i).Selected = True Then
                        ObjCnx.Ejecutar("paReq_ActualizarResponsableDeCronograma", 1, cod_sol, .Items(i).Value)
                    Else
                        ObjCnx.Ejecutar("paReq_ActualizarResponsableDeCronograma", 0, cod_sol, .Items(i).Value)
                    End If
                Next
            End With
            ObjCnx.Ejecutar("paReq_CambiarEstadoSolicitud", cod_sol)
            ObjCnx.TerminarTransaccion()
            Response.Write("<script>alert('Se registraron los datos correctamente'); window.opener.location.reload();window.close();</script>")
        Catch ex As Exception
            ObjCnx.AbortarTransaccion()
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrió un error al procesar el registro');</script>")
        End Try
        ObjCnx = Nothing
    End Sub

End Class

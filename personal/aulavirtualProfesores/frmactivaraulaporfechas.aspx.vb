
Partial Class frmCargaPorFecha

    Inherits System.Web.UI.Page
    Protected Sub cmdHabilitar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHabilitar.Click
        Dim I As Integer
        Dim mensaje As String = ""
        Dim Fila As GridViewRow
        Dim ruta As String = "T:\documentos aula virtual\archivoscv\"
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim ObjAula As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)

        Try
            For I = 0 To Me.GridView1.Rows.Count - 1
                Fila = Me.GridView1.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        '==================================
                        ' Guardar los datos
                        '==================================
                        Dim idcursovirtual As String
                        obj.IniciarTransaccion()
                        mensaje = mensaje & "<br>" & obj.Ejecutar("AgregarCursoVirtual", "I", Me.GridView1.DataKeys.Item(Fila.RowIndex).Values("codigo_per"), Me.GridView1.DataKeys.Item(Fila.RowIndex).Values("codigo_cup"), Me.dpCiclo.SelectedValue, Me.GridView1.DataKeys.Item(Fila.RowIndex).Values("login_per"), 0, 0)
                        obj.TerminarTransaccion()
                        'Consultar si se ha creado el Curso
                        Dim tbl As Data.DataTable
                        tbl = ObjAula.TraerDataTable("ConsultarCursoVirtual", 5, Me.GridView1.DataKeys.Item(Fila.RowIndex).Values("codigo_cup"), 0, 0)
                        If tbl.Rows.Count > 0 Then
                            idcursovirtual = tbl.Rows(0).Item("idCursovirtual")
                            Dim doc As New System.IO.DirectoryInfo(ruta & idcursovirtual & "\documentos")
                            If doc.Exists = False Then doc.Create()

                            Dim tar As New System.IO.DirectoryInfo(ruta & idcursovirtual & "\tareas")
                            If tar.Exists = False Then tar.Create()

                            Dim img As New System.IO.DirectoryInfo(ruta & idcursovirtual & "\images")
                            If img.Exists = False Then img.Create()
                        End If

                    End If
                End If
            Next
            obj = Nothing
            ObjAula = Nothing
            Me.GridView1.Visible = False
            Me.lblMensaje.Text = mensaje
            Page.RegisterStartupScript("HabilitarAula", "<script>alert('Se han creado las aulas virtuales correctamente.\nHaga clic en ACEPTAR para regresar');location.href='frmactivaraulaporfechas.aspx'</script>")

        Catch ex As Exception
            obj.AbortarTransaccion()
            Me.cmdHabilitar.Visible = False
            Me.lblMensaje.Text = "Ocurrió un Error al habilitar las aulas virtuales." & Chr(13) & ex.Message
            obj = Nothing
        End Try
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "HabilitarEnvio(this)")
        End If
    End Sub
    Private Sub frmCargaPorFecha_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ClsFunciones.LlenarListas(Me.dpCiclo, obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0), "codigo_cac", "descripcion_cac")
            ClsFunciones.LlenarListas(Me.dpEscuela, obj.TraerDataTable("ConsultarCarreraProfesional", "MA", 0), "codigo_cpf", "nombre_cpf")
            obj = Nothing
            MostrarCargaAcademica()
        End If
    End Sub
    Private Sub MostrarCargaAcademica()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        Me.GridView1.DataSource = obj.TraerDataTable("ConsultarCargaAcademicaPorFecha", Me.dpTipo.SelectedValue, Me.dpCiclo.SelectedValue, Me.dpEscuela.SelectedValue)
        Me.GridView1.DataBind()
        obj = Nothing
    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        MostrarCargaAcademica()
    End Sub
End Class

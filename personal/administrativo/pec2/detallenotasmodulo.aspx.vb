
Partial Class detallenotasmodulo
    Inherits System.Web.UI.Page
    Dim a, d, p As Int16
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            MostrarDatosModulo()
        End If
    End Sub

    Protected Sub grwParticipantes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwParticipantes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            '========================================
            'Validar Si está RETIRADO
            '========================================
            If fila("estado_dma").ToString = "R" Then
                e.Row.Cells(6).Text = "" 'Ocultar botón Edición
            ElseIf fila("condicion_dma").ToString = "P" Then
                p = p + 1
            Else
                '========================================
                'Asignar CSS a las notas registradas
                '========================================
                If fila("condicion_dma").ToString = "A" Then
                    a = a + 1
                    e.Row.Cells(5).ForeColor = Drawing.Color.Blue
                Else
                    d = d + 1
                    e.Row.Cells(5).ForeColor = Drawing.Color.Red
                End If
            End If
        End If
    End Sub

    Private Sub MostrarDatosModulo()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tblmodulopec As Data.DataTable
        tblmodulopec = obj.TraerDataTable("PEC_ConsultarProgramacionPEC", 1, Request.QueryString("p"), Request.QueryString("c"), 0)

        If tblmodulopec.Rows.Count > 0 Then
            'Mostrar datos del módulo
            Me.lblDescripcion_pes.Text = tblmodulopec.Rows(0).Item("descripcion_pes")
            Me.lblnombre_cur.Text = tblmodulopec.Rows(0).Item("nombre_cur")
            Me.lblFechaInicio.Text = tblmodulopec.Rows(0).Item("fechainicio_cup")
            Me.lblFechaFin.Text = tblmodulopec.Rows(0).Item("fechafin_cup")
            Me.lblProfesor.Text = tblmodulopec.Rows(0).Item("profesor")
            Me.lblEstadoNota.Text = IIf(tblmodulopec.Rows(0).Item("estadonota_cup") = "P", "Notas por Registrar", "Notas Registradas")

            'Mostrar participantes
            Me.grwParticipantes.DataSource = obj.TraerDataTable("PEC_NotasFinalesModulosPEC", Request.QueryString("p"), Request.QueryString("c"))
            Me.grwParticipantes.DataBind()

            'Mostrar nombre del Curso
            Me.Page.Title = "Acta de Registro de Notas: " & Me.lblnombre_cur.Text.ToString
        End If
        tblmodulopec.Dispose()
        obj.CerrarConexion()
        obj = Nothing
        'Mostrar totales
        Me.lblP.Text = "Pendientes: " & p.ToString
        Me.lblA.Text = "Aprobados: " & a.ToString
        Me.lblD.Text = "Desaprobados: " & d.ToString
    End Sub
End Class
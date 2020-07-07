
Partial Class AsignarResponsable
    Inherits System.Web.UI.Page

    Protected Sub CmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdConsultar.Click
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        If Me.CboCampo.SelectedValue = 0 Then
            Me.GvSolicitud.DataSource = Objcnx.TraerDataTable("paReq_SolicitudesPorAsignar", 0, 0)
            Me.GvSolicitud.DataBind()
        Else
            Me.GvSolicitud.DataSource = Objcnx.TraerDataTable("paReq_SolicitudesPorAsignar", Me.CboValor.SelectedValue, Me.CboCampo.SelectedValue)
            Me.GvSolicitud.DataBind()
        End If
        Me.LblTotal.Text = "Total de registros: " & Me.GvSolicitud.Rows.Count

    End Sub

    Protected Sub CboCampo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboCampo.SelectedIndexChanged
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Select Case Me.CboCampo.SelectedValue
            Case 0
                Me.CboValor.Enabled = False
            Case 1
                Me.CboValor.Enabled = True
                Me.CboValor.Items.Clear()
                Dim Prioridad() As String = {"-- Seleccione Prioridad --", "Muy Baja", "Baja", "Media", "Alta", "Muy Alta"}
                Me.CboValor.DataSource = Prioridad
                Me.CboValor.DataBind()
                Me.CboValor.Items(0).Value = -1
                For i As Int16 = 1 To 5
                    Me.CboValor.Items(i).Value = i
                Next
            Case 2
                Me.CboValor.Enabled = True
                ClsFunciones.LlenarListas(Me.CboValor, Objcnx.TraerDataTable("paReq_ConsultarTipoSolicitud", "s"), "id_tsol", "descripcion_tsol", "-- Seleccione Tipo Solicitud --")
            Case 3
                Me.CboValor.Enabled = True
                ClsFunciones.LlenarListas(Me.CboValor, Objcnx.TraerDataTable("paReq_ConsultarCentroCosto"), "codigo_cco", "descripcion_cco", "-- Seleccione Area --")
            Case 4
                Me.CboValor.Enabled = True
                ClsFunciones.LlenarListas(Me.CboValor, Objcnx.TraerDataTable("paReq_consultaraplicacion"), "codigo_apl", "descripcion_apl", "-- Seleccione Aplicación--")
        End Select
    End Sub

    Protected Sub GvSolicitud_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvSolicitud.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("onMouseOver", "pintarcelda(this)")
            e.Row.Attributes.Add("onMouseOut", "despintarcelda(this)")
            e.Row.Cells(12).Text = "<a href='Responsable.aspx?field=" & fila.Row("id_sol").ToString & "'><img border=0 src='../images/asignar.gif'></a>"
        End If
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CmdConsultar_Click(sender, e)
        End If
    End Sub
End Class

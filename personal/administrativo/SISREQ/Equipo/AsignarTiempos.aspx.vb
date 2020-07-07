
Partial Class Equipo_Estados
    Inherits System.Web.UI.Page
    Public cod_per As Int32

    Protected Sub frmModulo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmModulo.Load
        cod_per = CInt(Request.QueryString("id"))
        If Not IsPostBack Then
            Me.CboCampo.SelectedValue = 0
        End If
    End Sub

    Protected Sub CboCampo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboCampo.SelectedIndexChanged
        Dim objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Select Case Me.CboCampo.SelectedValue
            Case 0
                Me.GvSolicitudes.DataBind()
                Me.cboValor.Visible = False
                LblRegistros.Text = "Total de registros: " & Me.GvSolicitudes.Rows.Count
            Case 1
                ClsFunciones.LlenarListas(Me.cboValor, objcnx.TraerDataTable("paReq_consultaraplicacion"), "codigo_apl", "descripcion_apl")
                Me.cboValor.Visible = True
                LblRegistros.Text = "Total de registros: 0"
            Case 2
                Dim DatFun As New Data.DataTable
                Dim i As Int16, j As Int16 = 0
                DatFun = objcnx.TraerDataTable("paReq_ConsultarTipoSolicitud", "s")
                Me.cboValor.Items.Clear()
                For i = 0 To DatFun.Rows.Count - 1
                    If (CInt(DatFun.Rows(i).Item("id_tsol").ToString) <> 1) Then
                        Me.cboValor.Items.Add(DatFun.Rows(i).Item("descripcion_tsol").ToString)
                        Me.cboValor.Items(j).Value = CInt(DatFun.Rows(i).Item("id_tsol").ToString)
                        j += 1
                    End If
                Next
                'ClsFunciones.LlenarListas(Me.cboValor, objcnx.TraerDataTable("paReq_ConsultarTipoSolicitud", "s"), "id_tsol", "descripcion_tsol", "--Seleccione Tipo de Solicitud--")
                Me.cboValor.Visible = True
                LblRegistros.Text = "Total de registros: 0"
        End Select
    End Sub

    Protected Sub cboValor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboValor.SelectedIndexChanged
        Me.GvSolicitudes.DataBind()
        LblRegistros.Text = "Total de registros: " & Me.GvSolicitudes.Rows.Count
    End Sub

    Protected Sub GvSolicitudes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvSolicitudes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim cadena As String
            fila = e.Row.DataItem
            If CInt(fila.Row("cant").ToString) > 1 Then
                e.Row.ForeColor = Drawing.Color.FromArgb(26, 132, 26) 'Solicitud
            Else
                e.Row.ForeColor = Drawing.Color.FromArgb(61, 132, 211) 'Requerimiento
            End If
            e.Row.Attributes.Add("onMouseOver", "pintarcelda(this)")
            e.Row.Attributes.Add("onMouseOut", "despintarcelda(this)")
            cadena = "'DatosSolicitud.aspx?cod_sol=" & fila.Row("id_sol").ToString & "'"
            e.Row.Attributes.Add("onClick", "javascript:ifSolicitud.document.location.href=" & cadena)
            'e.Row.Cells(14).Text = "<a href='Planificar.aspx?id_sol=" & fila.Row("id_sol").ToString & "&id=" & cod_per.ToString & "'><img border=0 src='../images/ver.gif'></a>"
            e.Row.Cells(14).Text = "<div style='cursor:hand' onclick=AbrirPopUp('Planificar.aspx?id_sol=" & fila.Row("id_sol").ToString & "&id=" & cod_per.ToString & "&id_act=" & fila.Row("id_sol").ToString & "&tipo=s&Pag=C','350','550')><img border=0 src='../images/vcalendar.gif'></div>"
        End If

    End Sub

End Class


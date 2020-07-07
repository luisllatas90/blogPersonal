
Partial Class Consultas_SolicitudesPendientes
    Inherits System.Web.UI.Page
    Protected Sub CboCampo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboCampo.SelectedIndexChanged

        If Me.CboCampo.SelectedValue = 2 Then
            Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Me.CboResponsable.Enabled = True
            Me.CboResponsable.Items.Clear()
            ClsFunciones.LlenarListas(Me.CboResponsable, Objcnx.TraerDataTable("paReq_ConsultarEquipo"), "codigo_per", "nombres", "-- Seleccione Persona --")
            Objcnx = Nothing
        Else
            Me.CboResponsable.Items.Clear()
            Me.CboResponsable.Items.Add("-- Todos --")
            Me.CboResponsable.Items(0).Value = 0
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvRequerimientos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Dim cadena As String
            Fila = e.Row.DataItem
            cadena = "../Equipo/DatosSolicitud.aspx?cod_sol=" & Fila.Item("id_sol").ToString & ""
            'e.Row.Attributes.Add("onClick", "javascript:ifDatos.document.location.href=" & cadena & "; pintarcelda(this)")
            'e.Row.Attributes.Add("onMouseOut", "despintarcelda(this)")
            e.Row.Cells(14).Text = "<a href='../Equipo/EditarSolicitud.aspx?id_sol=" & Fila.Row("id_sol").ToString & "&id=" & Request.QueryString("id").ToString & "'><img border=0 src='../images/editar.gif'></a>"

            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "ResaltarfilaDetalle_net('',this,'" & cadena & "');")

            e.Row.Attributes.Add("id", "fila" & e.Row.RowIndex & "")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")

            e.Row.Style.Add("cursor", "hand")
            Select Case Fila.Item("id_est")
                Case 1 : e.Row.Cells(13).ForeColor = Drawing.Color.Red
                Case 2
                    e.Row.Cells(13).ForeColor = System.Drawing.Color.FromArgb(240, 206, 15)

                Case 3 : e.Row.Cells(13).ForeColor = Drawing.Color.Blue
                Case 4 : e.Row.Cells(13).ForeColor = Drawing.Color.Green
            End Select
        End If
    End Sub

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If Not IsPostBack Then
            If CInt(Request.QueryString("id").ToString) = 238 Then
                Me.CboCampo.Visible = True
                Me.CboResponsable.Visible = True
                Me.LblConsultar.Text = "CONSULTAR"
                Session("cod_per") = CboResponsable.SelectedValue
            Else
                Me.CboCampo.Visible = False
                Me.CboResponsable.Visible = False
                Me.LblConsultar.Text = "SOLICITUDES ASIGNADAS"
                Session("cod_per") = CInt(Request.QueryString("id").ToString)
            End If
        End If
    End Sub

    Protected Sub CboResponsable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboResponsable.SelectedIndexChanged
        Session("cod_per") = CboResponsable.SelectedValue
    End Sub

End Class

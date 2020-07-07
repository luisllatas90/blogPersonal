
Partial Class SisSolicitudes_SolicitudesObservadas
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim datos As New Data.DataTable
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            datos = Obj.TraerDataTable("SOL_ConsultarSolicitudesObservadas", 3, Request.QueryString("id"), 0)
            '#### Retorna cero cuando el select es vacío ####
            If datos.Rows.Count > 0 Then

                If datos.Rows(0).Item("codigo_sol") <> 0 Then
                    Me.GvSolicitudes.DataSource = datos
                    Me.GvSolicitudes.DataBind()
                End If
            End If
            Page.RegisterStartupScript("Habilitar3", "<script>TbAlumno.style.visibility='hidden';</script>")
            Page.RegisterStartupScript("Habilitar4", "<script>TbPestanas.style.visibility='hidden';</script>")
            Me.CmdVerEvaluacion.Visible = False
        End If
    End Sub


    Protected Sub GvSolicitudes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvSolicitudes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & Fila.Row("codigo_sol").ToString & "")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('GvSolicitudes','Select$" & e.Row.RowIndex & "'); HabilitarBoton('M',this,'" & Fila.Row("codigo_sol").ToString & "');") ')
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            'e.Row.Cells(0).Text = e.Row.RowIndex + 1
            e.Row.Style.Add("cursor", "hand")
            Select Case Fila.Item("Estado")
                Case "P" : e.Row.Cells(8).ForeColor = Drawing.Color.Red
                    e.Row.Cells(8).Text = "Pendiente"
                Case "T" : e.Row.Cells(8).ForeColor = Drawing.Color.Green
                    e.Row.Cells(8).Text = "Finalizada"
            End Select
        End If
    End Sub

    Protected Sub GvSolicitudes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvSolicitudes.SelectedIndexChanged
        'Recuperar dato de un data key
        Me.HddAlumno.Value = Me.GvSolicitudes.DataKeys.Item(Me.GvSolicitudes.SelectedIndex).Values(1).ToString
        Me.HddNumero_sol.Value = Me.GvSolicitudes.DataKeys.Item(Me.GvSolicitudes.SelectedIndex).Values(0).ToString
        Me.txtelegido.Value = Me.GvSolicitudes.SelectedRow.Attributes("id")
        Dim Ruta As New EncriptaCodigos.clsEncripta
        Me.ImgFoto.Dispose()
        Me.ImgFoto.Visible = True
        Me.ImgFoto.ImageUrl = ""
        '---------------------------------------------------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: dguevara
        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
        '---------------------------------------------------------------------------------------------------------------
        Me.ImgFoto.ImageUrl = "//intranet.usat.edu.pe/imgestudiantes/" & Ruta.CodificaWeb("069" & Me.HddAlumno.Value)
        Me.ImgFoto.DataBind()
        Page.RegisterStartupScript("Habilitar3", "<script>TbAlumno.style.visibility='visible';</script>")
        Page.RegisterStartupScript("Habilitar4", "<script>TbPestanas.style.visibility='visible';</script>")
        Me.CmdVerEvaluacion.Visible = True
        CmdVerEvaluacion.Attributes.Add("OnClick", "AbrirPopUp('ConsultarInformes.aspx?codigo_univ=" & Me.HddAlumno.Value & "&numero_sol=" & HddNumero_sol.Value & "&id=" & Request.QueryString("id") & "','450','600'); return false;")
    End Sub

End Class

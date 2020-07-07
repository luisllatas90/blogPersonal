
Partial Class AgregarActividades
    Inherits System.Web.UI.Page

    Protected Sub LnkVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkVolver.Click
        Response.Redirect("ListaRequerimientos.aspx?id_sol=" & Request.QueryString("id_sol") & "&id=" & Request.QueryString("id"))
    End Sub


    Protected Sub frmActividades_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmActividades.Load
        If Not IsPostBack Then
            Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ClsFunciones.LlenarListas(Me.RbtResponsable, Objcnx.TraerDataTable("paReq_EquipoAsignadaASolicitud", CInt(Request.QueryString("id_sol"))), "id_solequ", "nombres")
            ConsultarActividades()
            Me.RbtResponsable.SelectedIndex = 0
        End If
    End Sub


    Protected Sub ImgAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgAdd.Click
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            Dim id_Act As Int32
            Objcnx.IniciarTransaccion()
            id_Act = Objcnx.Ejecutar("paReq_InsertarActividadCronograma", Me.TxtActividad.Text, Me.RbtResponsable.SelectedValue, CInt(Request.QueryString("id_req")), 0)
            Objcnx.TerminarTransaccion()
            Objcnx.Ejecutar("paReq_InsertarCronograma", Me.TxtFini.Text, Me.TxtFfin.Text, Me.TxtObservacion.Text, id_Act, "r")
            Objcnx.Ejecutar("paReq_ActualizarFechasActividadCro", CInt(Request.QueryString("id_req")))

            Page.RegisterStartupScript("Correcto", "<script>alert('Se registraron correctamente los datos')</script>")
            ConsultarActividades()
        Catch ex As Exception
            Objcnx.AbortarTransaccion()
            Response.Write(ex.Message)
        End Try
        Objcnx = Nothing
    End Sub

    Protected Sub ImgActualizar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgActualizar.Click
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim id_Act As Int32
        Try
            Objcnx.IniciarTransaccion()
            id_Act = Objcnx.Ejecutar("paReq_InsertarActividadCronograma", Me.TxtActividad.Text, Me.RbtResponsable.SelectedValue, CInt(Request.QueryString("id_req")), 0)
            Objcnx.TerminarTransaccion()
            ConsultarActividades()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ConsultarActividades()
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Me.GvActividades.DataSource = Objcnx.TraerDataTable("paReq_ConsultarActividad", CInt(Request.QueryString("id_req")))
        Me.GvActividades.DataBind()
    End Sub

    Protected Sub GvActividades_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvActividades.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('GvActividades','Select$" & e.Row.RowIndex & "'); SeleccionarFila(); ResaltarPestana('0','','');")

            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")

            e.Row.Style.Add("cursor", "hand")
        End If
    End Sub

    Protected Sub GvActividades_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GvActividades.RowDeleting
        'Dim keyActividad As Int32
        'Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStringS("CNXBDUSAT").ConnectionString)
        'keyActividad = Me.GvActividades.DataKeys.Item(Me.GvActividades.SelectedIndex).Values(0).ToString()
        'Objcnx.Ejecutar("paReq_EliminarActividad", keyActividad)
        'Objcnx.Ejecutar("paReq_EliminarRequerimiento", e.Keys.Item(0), rpta)
    End Sub

    Protected Sub ImgQuit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgQuit.Click
        Dim keyActividad As Int32
        Dim Objcnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        keyActividad = Me.GvActividades.DataKeys.Item(Me.GvActividades.SelectedIndex).Values(0).ToString()
        Objcnx.Ejecutar("paReq_EliminarActividad", keyActividad)
        ConsultarActividades()
    End Sub

    Protected Sub GvActividades_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvActividades.SelectedIndexChanged
        Dim keyActividad As Int32
        keyActividad = Me.GvActividades.DataKeys.Item(Me.GvActividades.SelectedIndex).Values(0).ToString()
        'Me.TxtActividad.Text = GvActividades.Rows().Cells(0).ToString()
        Me.TxtActividad.Text = GvActividades.SelectedRow.Cells(1).Text
        Me.RbtResponsable.SelectedValue = Me.GvActividades.DataKeys.Item(Me.GvActividades.SelectedIndex).Values(1).ToString()
        Me.TxtFini.Text = GvActividades.SelectedRow.Cells(4).Text
        Me.TxtFfin.Text = GvActividades.SelectedRow.Cells(5).Text
        Me.TxtObservacion.Text = GvActividades.SelectedRow.Cells(6).Text
    End Sub


End Class

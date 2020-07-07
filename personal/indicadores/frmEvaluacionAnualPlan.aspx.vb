
Partial Class indicadores_frmEvaluacionAnualPlan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargarAñosRegistrados()
                CargarDocumentos()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarAñosRegistrados()
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            dts = obj.CargarAñosRegistrados()
            If dts.Rows.Count > 0 Then
                Me.ddlAnioFiltro.DataSource = dts
                Me.ddlAnioFiltro.DataTextField = "descripcion"
                Me.ddlAnioFiltro.DataValueField = "codigo"
                Me.ddlAnioFiltro.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub CargarDocumentos()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListaEvaluacionesPlanResponsable(Me.Request.QueryString("id"), Me.Request.QueryString("ctf"), Me.ddlAnioFiltro.SelectedValue)
            If dts.Rows.Count > 0 Then
                gvLista.DataSource = dts
                gvLista.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvLista.RowCancelingEdit
        Try
            gvLista.EditIndex = -1
            CargarDocumentos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvLista.RowUpdating
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            Dim RowID As Integer = Convert.ToInt32(gvLista.DataKeys(e.RowIndex).Value)
            Dim fileupload As FileUpload = gvLista.Rows(e.RowIndex).FindControl("FileArchivo")

            If fileupload.HasFile = False Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('No se ha especificado un archivo, verifique.');", True)
            Else
                Dim filePath As String
                Dim archivo As String = "\" & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & System.IO.Path.GetExtension(fileupload.FileName).ToString
                Dim archivoBD As String = "/" & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & System.IO.Path.GetExtension(fileupload.FileName).ToString

                filePath = Server.MapPath("../../filesIndicadores")
                filePath = filePath & "\" & RowID

                Dim carpeta As New System.IO.DirectoryInfo(filePath)
                If carpeta.Exists = False Then
                    carpeta.Create()
                End If
                fileupload.PostedFile.SaveAs(filePath & archivo)
                'Dim ruta As String = filePath & archivo
                Dim ruta As String = archivoBD

                '--/ Actualiza la ruta del archivo
                dts = obj.AgregarDocumentoPlan(RowID, ruta)
                If dts.Rows(0).Item("rpt") > 0 Then
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('El Archivo fue cargado al plan.');", True)
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Error, al cargar el documuento al Plan.');", True)
                End If
            End If
            gvLista.EditIndex = -1
            CargarDocumentos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvLista.RowEditing
        Try
            gvLista.EditIndex = e.NewEditIndex
            CargarDocumentos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLista.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(1).Enabled = False
                e.Row.Cells(2).Enabled = False
                e.Row.Cells(3).Enabled = False
                e.Row.Cells(4).Enabled = False
                e.Row.Cells(5).Enabled = False
                e.Row.Cells(6).Enabled = False

                'Aqui bloqueamos el modificar para impedir que suban el archivo fuera de las fechas.
                Dim plazo As Integer = gvLista.DataKeys(e.Row.RowIndex).Values(1)
                'Response.Write(gvLista.DataKeys(e.Row.RowIndex).Values(1))
                If plazo = 0 Then
                    e.Row.Cells(9).Enabled = False
                    e.Row.Cells(9).Text = "<center><img src='../images/var_sistema.png' style='border: 0px' alt='Bloqueado'/></center>"
                Else
                    e.Row.Cells(9).Enabled = True
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlAnioFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAnioFiltro.SelectedIndexChanged
        Try
            CargarDocumentos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class

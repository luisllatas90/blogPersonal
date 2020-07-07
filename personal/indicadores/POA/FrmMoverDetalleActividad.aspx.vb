
Partial Class indicadores_POA_FrmMoverDetalleActividad
    Inherits System.Web.UI.Page



    Sub cargarDetalle(ByVal codigo_dap As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        dt = obj.POA_DatosDetalleActividad(codigo_dap)
        Me.lblpoa.Text = dt.Rows(0).Item("nombre_poa").ToString
        Me.lblacp.Text = dt.Rows(0).Item("resumen_acp").ToString
        Me.lbldap.Text = dt.Rows(0).Item("descripcion_dap").ToString
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If IsPostBack = False Then
            Try
                Dim codigo_acp, codigo_poa, codigo_dap As Integer
                codigo_poa = Request.QueryString("codigo_poa")
                codigo_acp = Request.QueryString("codigo_acp")
                codigo_dap = Request.QueryString("codigo_dap")

                Me.cargarDetalle(codigo_dap)
                CargaProgProy(codigo_dap, codigo_acp)
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End If
       

    End Sub
    Function cadena_retro() As String
        Dim codigo_acp, codigo_poa, codigo_dap, ctf As Integer
        Dim cb1, cb2, cb3, cb4 As String
        Dim cadena As String
        codigo_poa = Request.QueryString("codigo_poa")
        codigo_acp = Request.QueryString("codigo_acp")
        codigo_dap = Request.QueryString("codigo_dap")
        cb1 = Request.QueryString("cb1")
        cb2 = Request.QueryString("cb2")
        cb3 = Request.QueryString("cb3")
        cb4 = Request.QueryString("cb4")
        ctf = Request.QueryString("ctf")
        cadena = "cb1=" & cb1 & "&cb2=" & cb2 & "&cb3=" & cb3 & "&cb4=" & cb4 & "&id=" & Session("id_per") & "&ctf=" & ctf
        cadena = cadena & "&codigo_poa=" & codigo_poa & "&codigo_acp=" & codigo_acp & "&tipo_acc=PL&back=pto"
        Return cadena
    End Function

    Sub CargaProgProy(ByVal codigo_dap As Integer, ByVal codigo_acp As Integer)
        Dim objPoa As New clsPlanOperativoAnual
        Dim dt As New Data.DataTable
        dt = objPoa.POA_ListaProgProyPorCodDap(codigo_dap, codigo_acp)
        Me.ddlProgProy.DataSource = dt
        Me.ddlProgProy.DataTextField = "descripcion"
        Me.ddlProgProy.DataValueField = "codigo"
        Me.ddlProgProy.DataBind()
        objPoa = Nothing
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("FrmMantenimientoActividadesPOA.aspx?" & cadena_retro())
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Me.aviso.Attributes.Clear()
        Me.lblrpta.Text = ""
        If Me.ddlProgProy.SelectedValue <> 0 Then
            Dim obj As New clsPlanOperativoAnual
            If obj.POA_MoverDetalleActividad(Me.ddlProgProy.SelectedValue, Request.QueryString("codigo_dap")).ToString = "1" Then
                Me.aviso.Attributes.Add("class", "mensajeExito")
                Me.lblrpta.Text = "Programa/Proyecto Observado Correctamente."
                Response.Redirect("FrmMantenimientoActividadesPOA.aspx?" & cadena_retro())
            Else
                Me.aviso.Attributes.Add("class", "mensajeError")
                Me.lblrpta.Text = "Actividad No puedo Ser Transferida."
            End If

        Else
            Me.aviso.Attributes.Add("class", "mensajeError")
            Me.lblrpta.Text = "Debe Seleccionar un Programa/Proyecto"
        End If

    End Sub
End Class

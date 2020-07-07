
Partial Class indicadores_POA_FrmAgregarCecosAntiguosEnPOA
    Inherits System.Web.UI.Page

    Sub CargaEjercicio()
        Dim obj As New clsPlanOperativoAnual
        Dim dtEjercicioPresupuestal As New Data.DataTable
        dtEjercicioPresupuestal = obj.ListaEjercicio
        Me.ddlEjercicio.DataSource = dtEjercicioPresupuestal
        Me.ddlEjercicio.DataTextField = "descripcion"
        Me.ddlEjercicio.DataValueField = "codigo"
        Me.ddlEjercicio.DataBind()

        dtEjercicioPresupuestal.Dispose()
        obj = Nothing
    End Sub

    Sub Carga_Peis()
        Dim obj As New clsPlanOperativoAnual
        Dim dtPEI As New Data.DataTable
        dtPEI = obj.ListaPeis
        Me.ddlPei.DataSource = dtPEI
        Me.ddlPei.DataTextField = "descripcion"
        Me.ddlPei.DataValueField = "codigo"
        Me.ddlPei.DataBind()
        dtPEI.Dispose()
        obj = Nothing
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If IsPostBack = False Then
                CargaEjercicio()
                Carga_Peis()
                CargaPersonal()
                Me.ddlResponsable.SelectedValue = 1739
                Me.Panel1.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub

    Sub CargaPoas(ByVal codigo_pla As Integer, ByVal codigo_ejp As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.POA_ListaPoasActividad(Me.ddlPei.SelectedValue, Me.ddlEjercicio.SelectedValue, Session("id_per"), Request.QueryString("ctf"), "T")
        'dtt = obj.ListaPoasxInstanciaEstado(codigo_pla, codigo_ejp, "PTO", Request.QueryString("id"), Request.QueryString("ctf"))

        Me.ddlPoa.DataSource = dtt
        Me.ddlPoa.DataTextField = "nombre_poa"
        Me.ddlPoa.DataValueField = "codigo_poa"
        Me.ddlPoa.DataBind()
        dtt.Dispose()
        obj = Nothing
        Me.ddlPoa.Items.Insert(0, New ListItem("SELECCIONE POA", "0"))

        'Me.ddlPoa.Items.Insert(0, New ListItem("--SELECCIONE--", "0"))
    End Sub

    Sub ObtenerResponsable(ByVal codigo_poa As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        dtt = obj.POA_ResponsablePOA(codigo_poa)
        'If dtt.Rows.Count = 1 Then
        '    Me.lblResponsablePOA.InnerHtml = dtt.Rows(0).Item("responsable").ToString
        'Else
        '    Me.lblResponsablePOA.InnerHtml = ""
        'End If

        dtt.Dispose()
        obj = Nothing
        'Me.ddlPoa.Items.Insert(0, New ListItem("--SELECCIONE--", "0"))
    End Sub

    Sub CargaPersonal()
        Dim dt As New Data.DataTable
        Dim objpoa As New clsPlanOperativoAnual
        dt = objpoa.POA_ListaPersonalxCeco(0, 0)
        Me.ddlResponsable.DataSource = dt
        Me.ddlResponsable.DataTextField = "Personal"
        Me.ddlResponsable.DataValueField = "codigo"
        Me.ddlResponsable.DataBind()
    End Sub


    Protected Sub ddlEjercicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEjercicio.SelectedIndexChanged
        Me.Mensaje.InnerText = ""
        Me.Mensaje.Attributes.Remove("class")
        CargaPoas(Me.ddlPei.SelectedValue, Me.ddlEjercicio.SelectedValue)
        'Me.lblResponsablePOA.InnerHtml = ""
    End Sub

    Protected Sub ddlPei_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPei.SelectedIndexChanged
        Me.Mensaje.InnerText = ""
        Me.Mensaje.Attributes.Remove("class")
        CargaPoas(Me.ddlPei.SelectedValue, Me.ddlEjercicio.SelectedValue)
        'Me.lblResponsablePOA.InnerHtml = ""
    End Sub

    Protected Sub ddlPoa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPoa.SelectedIndexChanged
        Me.Mensaje.InnerText = ""
        Me.Mensaje.Attributes.Remove("class")
        If Me.ddlPoa.SelectedValue = "" Then
            'Me.lblResponsablePOA.InnerHtml = ""
        Else
            ObtenerResponsable(Me.ddlPoa.SelectedValue)
        End If
    End Sub

    Function validar() As Boolean
        Me.Mensaje.InnerText = ""
        Me.Mensaje.Attributes.Remove("class")
        If Me.hdCeco.Value = "0" Then
            FnMensaje("Busque y seleccione un Centro de costos", "danger")
            Return False
        End If
        If Me.ddlEjercicio.SelectedValue = 0 Then
            FnMensaje("Seleccione un Ejercicio Presupuestal", "danger")
            Return False
        End If
        If Me.ddlPei.SelectedValue = 0 Then
            FnMensaje("Seleccione un Plan Estrategico", "danger")
            Return False
        End If
        If Me.ddlPoa.SelectedValue = 0 Then
            FnMensaje("Seleccione un Plan Operativo Anual (POA)", "danger")
            Return False
        End If
        If Me.ddlResponsable.SelectedValue = 0 Then
            FnMensaje("Seleccione un Nuevo Responsable", "danger")
            Return False
        End If
        Return True
    End Function

    Protected Sub btnCambiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCambiar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If validar() = True Then
                Dim Obj As New clsPlanOperativoAnual
                Dim dt As New Data.DataTable
                dt = Obj.POA_AgregarCentroCostoAntiguoPOA(Me.hdCeco.Value, Me.ddlEjercicio.SelectedValue, Me.ddlPoa.SelectedValue, Me.ddlResponsable.SelectedValue, Session("id_per"))
                If dt.Rows(0).Item("rpta") = "1" Then
                    FnMensaje(dt.Rows(0).Item("msje").ToString, "success")
                    Limpiar()
                Else
                    FnMensaje(dt.Rows(0).Item("msje").ToString, "danger") ' Serverdev
                    'FnMensaje("No se Puedo Importar Archivo.", "danger") ' Producción
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub FnMensaje(ByVal mensaje As String, ByVal tipo As String)
        Me.Mensaje.InnerText = mensaje
        Me.Mensaje.Attributes.Add("class", "alert alert-" + tipo + "")
    End Sub

    Private Sub Limpiar()
        Me.hdCeco.Value = 0
        Me.ddlEjercicio.SelectedValue = 0
        Me.ddlPei.SelectedValue = 0
        Me.ddlPoa.SelectedValue = 0
        Me.ddlResponsable.SelectedValue = 0
        'Me.lblResponsablePOA.InnerText = ""
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Me.Panel1.Visible = False
        If Me.txtCeco.Text <> "" And Len(Me.txtCeco.Text) >= 3 Then
            Dim obj As New clsPlanOperativoAnual
            Dim dt As New Data.DataTable
            dt = obj.POA_ConsultarCentroCosto(1, Session("id_per"), Me.txtCeco.Text, "")
            Me.gvCeco.DataSource = dt
            Me.gvCeco.DataBind()
            Me.Panel1.Visible = True
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "ocultarE", "alert('Debe ingresar al menos 3 caracteres para buscar el centro de costo.')", True)
        End If
    End Sub

    Protected Sub gvCeco_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCeco.RowCommand
        If e.CommandName = "Seleccionar" Then
            Me.txtCeco.Text = ""
            Me.hdCeco.Value = Me.gvCeco.DataKeys(e.CommandArgument).Values("codigo_cco")
            Me.lblcentrocostos.Text = Me.gvCeco.Rows(e.CommandArgument).Cells(0).Text
            Me.Panel1.Visible = False
        End If
    End Sub
End Class

﻿
Partial Class librerianet_cargaacademica_frmsolicitudcambios
    Inherits System.Web.UI.Page
    Dim Codigo_per As Int32
    Dim contador As Int16
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        CargaPermisos()
    End Sub

    Protected Sub grwPermisos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwPermisos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            Contador = Contador + 1
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "HabilitarEnvio(this)")
            'Combinar celdas
            If Codigo_per = fila("OperadorAut_acr") Then
                e.Row.Cells(0).Text = ""
                e.Row.Cells(1).Text = ""
                e.Row.Cells(2).Text = ""
                e.Row.Cells(3).Text = ""
                e.Row.Cells(4).Text = ""
                contador = contador - 1
            Else
                e.Row.Cells(0).CssClass = "bordesup"
                e.Row.Cells(1).CssClass = "bordesup"
                e.Row.Cells(2).CssClass = "bordesup"
                e.Row.Cells(3).CssClass = "bordesup"
                e.Row.Cells(4).CssClass = "bordesup"
                e.Row.VerticalAlign = VerticalAlign.Middle
                Codigo_per = fila("OperadorAut_acr").ToString()
                e.Row.Cells(0).Text = contador
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            CargaPermisos()
        End If
    End Sub

    Private Sub CargaPermisos()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Me.grwPermisos.DataSource = obj.TraerDataTable("ConsultarAccesoRecurso", 7, Me.dpFiltro.SelectedValue, Me.dpnombretbl_acr.SelectedValue, 0)
        Me.grwPermisos.DataBind()

        Me.cmdGuardar.Enabled = grwPermisos.Rows.Count > 0
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim I As Integer
        Dim Fila As GridViewRow
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim email As String

        Try
            For I = 0 To Me.grwPermisos.Rows.Count - 1
                Fila = Me.grwPermisos.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        '==================================
                        ' Guardar los datos
                        '==================================
                        email = obj.Ejecutar("AgregarAccesoRecurso", "M", Request.QueryString("id"), Me.grwPermisos.DataKeys.Item(Fila.RowIndex).Values("OperadorAut_acr"), Me.dpnombretbl_acr.SelectedValue, Me.grwPermisos.DataKeys.Item(Fila.RowIndex).Values("codigo_acr"), 1, 1, 1, 1, System.DBNull.Value, System.DBNull.Value, 0, System.DBNull.Value)
                        'Enviar Email
                        EnviarMensaje(Me.grwPermisos.DataKeys.Item(Fila.RowIndex).Values("fechareg_acr"), email)
                    End If
                End If
            Next
            
	    Me.grwPermisos.DataSource = obj.TraerDataTable("ConsultarAccesoRecurso", 7, Me.dpFiltro.SelectedValue, Me.dpnombretbl_acr.SelectedValue, 0)
            Me.grwPermisos.DataBind()
	    obj = Nothing

            Page.RegisterStartupScript("CambioEstado", "<script>alert('Los cambios se guardaron correctamente. Además se envió un email de aprobación al solicitante')</script>")
        Catch ex As Exception
            Me.cmdGuardar.Visible = False
            Me.lblmensaje.Text = "Ocurrió un Error al Registrar el estado. Intente mas tarde." & Chr(13) & ex.Message
            obj = Nothing
        End Try
    End Sub
    Public Sub EnviarMensaje(ByVal fecha As Date, ByVal correo As String)
        'Enviar email
        Dim ObjMailNet As New ClsMail
        Dim Mensaje, AsuntoCorreo, ConCopiaA As String

        Mensaje = "<h3 style='font-color:blue'>ViceRectorado Académico ha APROBADO su solicitud para Modificar Carga Académica.</h3>"
        Mensaje = Mensaje & "<h4>El acceso estará habilitado hasta el " & DateAdd(DateInterval.Day, 1, fecha) & "</h2>"
        '---------------------------------------------------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: dguevara
        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
        '----------------------
        Mensaje = Mensaje & "<h4>Por favor ingresar al Campus Virtual para realizar sus modificaciones.<a href=""http://intranet.usat.edu.pe/campusvirtual"">Haga clic aquí</a></h4>"
        Mensaje = Mensaje & "<h5>Si el enlace no funciona ingresar a www.usat.edu.pe/campusvirtual, Módulo Académico, Movimientos, Carga Académica</h5>"

        AsuntoCorreo = "Solicitud de Acceso APROBADA"
        ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Módulo de Carga Académica", correo, AsuntoCorreo, Mensaje, True)
        ObjMailNet = Nothing
    End Sub
End Class

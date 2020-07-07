
Partial Class personal_frmModificarHorasLectivas
    Inherits System.Web.UI.Page

    Dim codigo_per As Integer
    Dim codigo_pel As Integer
    Dim vSemana As Integer

    Dim HInicio As String
    Dim HFin As String
    Dim vMensaje As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            codigo_per = Request.QueryString("Codigo_per")
            codigo_pel = Request.QueryString("Codigo_pel")
            vSemana = Request.QueryString("Semana")

            'Response.Write(vSemana)
            'Response.Write("-")
            'Response.Write(codigo_per)
            'Response.Write("-")
            'Response.Write(codigo_pel)

            CargaHorasLectivas()
            ConsultarHorasLectivas()
            ConsultarHorasCargaAcademica()
            EstadoControles(False)
            lblMensaje.Visible = False
        End If
    End Sub

    Private Sub ConsultarHorasCargaAcademica()
        Try
            Dim obj As New clsPersonal
            codigo_per = Request.QueryString("Codigo_per")
            codigo_pel = Request.QueryString("Codigo_pel")
            Dim HoraMinuto As String = obj.ConsultarHorasCargaAcademica(codigo_per, codigo_pel)
            lblHCA.Text = HoraMinuto
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ConsultarHorasLectivas()
        Try
            Dim obj As New clsPersonal
            codigo_per = Request.QueryString("Codigo_per")
            codigo_pel = Request.QueryString("Codigo_pel")
            vSemana = Request.QueryString("Semana")
            Dim HoraMinuto As String = obj.ValidarHorasGestionAcademicaConHorasLectivas(codigo_per, codigo_pel, vSemana)
            lblHLH.Text = HoraMinuto.Trim.ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EstadoControles(ByVal vEstado As Boolean)
        Try
            ddlHoraFin.Enabled = vEstado
            ddlHoraIni.Enabled = vEstado
            btnAgregar.Enabled = vEstado
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaCbxDia()
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsPersonal
            dts = obj.CargaHorasLectivas(codigo_per, codigo_pel, vSemana)
            dts = obj.CargaDiaHorasLectivas(codigo_per, codigo_pel, vSemana)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaHorasLectivas()
        'Carga CC del personal que tiene registrado horario personal
        Dim dts As New Data.DataTable
        Dim obj As New clsPersonal
        dts = obj.CargaHorasLectivas(codigo_per, codigo_pel, vSemana)
        gvHrsLista.DataSource = dts
        gvHrsLista.DataBind()
    End Sub

    'Eliminar un registro
    Protected Sub gvHrsLista_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvHrsLista.RowDeleting
        Try
            Dim obj As New clsPersonal
            obj.EliminarHorarioPersonal(gvHrsLista.Rows(e.RowIndex).Cells(0).Text)

            'Response.Write(Convert.ToInt32(gvHrsLista.DataKeys(e.RowIndex).Value))
            'obj.EliminarHorarioPersonal(Convert.ToInt32(gvHrsLista.DataKeys(e.RowIndex).Value))

            'Lineas para refrescar la pagina 
            codigo_per = Request.QueryString("Codigo_per")
            codigo_pel = Request.QueryString("Codigo_pel")
            vSemana = Request.QueryString("Semana")
            CargaHorasLectivas()

            lblMensaje.Visible = True
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Text = "(*) El registro ha sido actualizado correctamente."
            EstadoControles(False)
            ConsultarHorasLectivas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvHrsLista_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvHrsLista.SelectedIndexChanged
        Try
            lblDIA.Text = gvHrsLista.Rows(gvHrsLista.SelectedIndex).Cells(1).Text
            HInicio = gvHrsLista.Rows(gvHrsLista.SelectedIndex).Cells(2).Text
            HFin = gvHrsLista.Rows(gvHrsLista.SelectedIndex).Cells(3).Text

            Dim obj As New clsPersonal
            Dim dtsHI As New Data.DataTable
            Dim dtsHF As New Data.DataTable

            dtsHI = obj.ListaRangoHoras(HInicio, HFin)
            ddlHoraIni.DataTextField = "horaInicio"
            ddlHoraIni.DataValueField = "horaInicio"
            ddlHoraIni.DataSource = dtsHI
            ddlHoraIni.DataBind()
            ddlHoraIni.SelectedIndex = -1

            dtsHF = obj.ListaRangoHoras(HInicio, HFin)
            ddlHoraFin.DataTextField = "horaFin"
            ddlHoraFin.DataValueField = "horaFin"
            ddlHoraFin.DataSource = dtsHF
            ddlHoraFin.DataBind()
            ddlHoraFin.SelectedIndex = -1

            EstadoControles(True)
            lblMensaje.Visible = False

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            Dim obj As New clsPersonal
            If ddlHoraIni.Text = "--Seleccione--" Then
                vMensaje = vMensaje & "Especificar Hora Inicio. "
                Dim myscript As String = "alert('" & vMensaje.Trim & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If ddlHoraFin.Text = "--Seleccione--" Then
                vMensaje = vMensaje & "Especificar Hora Fin. "
                Dim myscript As String = "alert('" & vMensaje.Trim & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If ddlHoraIni.Text >= ddlHoraFin.Text Then
                vMensaje = "La hora de inicio debe ser menor que la hora fin."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If


            obj.ActualizarHorarioPersonal(gvHrsLista.Rows(gvHrsLista.SelectedIndex).Cells(0).Text, ddlHoraIni.Text, ddlHoraFin.Text)
            codigo_per = Request.QueryString("Codigo_per")
            codigo_pel = Request.QueryString("Codigo_pel")
            vSemana = Request.QueryString("Semana")

            'Lineas para refrescar la pagina 
            CargaHorasLectivas()
            lblMensaje.Visible = True
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Text = "(*) El registro ha sido actualizado correctamente."
            EstadoControles(False)
            ConsultarHorasLectivas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class


Partial Class frmactvevaluacion
    Inherits System.Web.UI.Page
    Private Sub MarcarCheck(ByVal chk As System.Object, ByVal Datos As System.Data.DataTable, _
    ByVal ColValor As System.Object, ByVal ColDescripcion As System.Object)
        'El valor predeterminado cuando no existe seleccion es (-1)
        chk.Items.Clear()
        For i As Integer = 0 To Datos.Rows.Count - 1
            chk.Items.Add(Datos.Rows(i).Item(ColDescripcion).ToString)
            chk.Items(i).Value = Datos.Rows(i).Item(ColValor).ToString
            If CInt(Datos.Rows(i).Item("Marca")) > 0 Then
                chk.items(i).selected = True
            End If
        Next
    End Sub

    Protected Sub dtmodo_aev_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtmodo_aev.SelectedIndexChanged
        If dtmodo_aev.SelectedValue = "F" Then
            Me.dtcodigo_tca.Visible = True
            Me.chkcodigo_aev.Visible = True
            Me.Label2.Visible = True
            Me.Label3.Visible = True
        Else
            Me.dtcodigo_tca.Visible = False
            Me.chkcodigo_aev.Visible = False
            Me.Label2.Visible = False
            Me.Label3.Visible = False
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)

            ClsFunciones.LlenarListas(Me.dtcodigo_tca, obj.TraerDataTable("DI_ConsultarEvaluacionParticipante", 3, 0, 0, 0), "codigo_tca", "descripcion_tca")
            Me.MarcarCheck(Me.chkcodigo_aev, obj.TraerDataTable("DI_ConsultarEvaluacionParticipante", 4, Request.QueryString("tiporeg_aev"), Request.QueryString("codigo_aev"), Session("idcursovirtual2")), "codigo_aev", "descripcion_aev")
            For i As Integer = 0 To 100
                Me.dtlimiteval_aev.Items.Add(i)
                Me.dtlimiteval_aev.Items(i).Value = i
            Next

            Me.FechaIni.Text = Now.ToShortDateString '
            Me.HoraIni.SelectedValue = Now.Hour
            Me.FechaFin.Text = DateAdd(DateInterval.Day, 1, Now).ToShortDateString '
            Me.HoraFin.SelectedValue = Now.Hour
            Me.dtlimiteval_aev.SelectedValue = 20

            If Request.QueryString("accion") = "M" Then
                Dim datos As Data.DataTable
                datos = obj.TraerDataTable("DI_ConsultarEvaluacionParticipante", 6, Request.QueryString("codigo_aev"), 0, 0)

                Me.dttipoval_aev.SelectedValue = datos.Rows(0).Item("tipoval_aev").ToString
                Me.FechaIni.Text = CDate(datos.Rows(0).Item("fechainicio_aev").ToString).ToShortDateString
                Me.FechaFin.Text = CDate(datos.Rows(0).Item("fechafin_aev").ToString).ToShortDateString
                Me.HoraIni.SelectedValue = Mid(datos.Rows(0).Item("fechainicio_aev").ToString, 12, 2)
                Me.HoraFin.SelectedValue = Mid(datos.Rows(0).Item("fechafin_aev").ToString, 12, 2)
                Me.MinIni.SelectedValue = Mid(datos.Rows(0).Item("fechainicio_aev").ToString, 15, 2)
                Me.MinFin.SelectedValue = Mid(datos.Rows(0).Item("fechafin_aev").ToString, 15, 2)
                Me.txtdescripcion_aev.Text = datos.Rows(0).Item("descripcion_aev").ToString
                Me.dtcodigo_tca.SelectedValue = datos.Rows(0).Item("codigo_tca").ToString
                Me.dtmodo_aev.SelectedValue = datos.Rows(0).Item("modo_aev").ToString
                Me.dtlimiteval_aev.SelectedValue = datos.Rows(0).Item("limiteval_aev").ToString

                If datos.Rows(0).Item("modo_aev").ToString = "F" Then
                    Me.chkcodigo_aev.Visible = True
                    Me.dtcodigo_tca.Visible = True
                    Me.Label2.Visible = True
                    Me.Label3.Visible = True
                End If
                If datos.Rows(0).Item("tipoval_aev").ToString = "C" Then
                    Me.dtlimiteval_aev.Visible = True
                    Me.Label4.Visible = True
                End If

                datos = Nothing
            End If
            obj = Nothing
        End If
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click

        Dim codigo_aev As String
        Dim finicio, ffin As DateTime
        finicio = CDate(Me.FechaIni.Text & " " & Me.HoraIni.Text & ":" & Me.MinIni.Text & ":00")
        ffin = CDate(Me.FechaFin.Text & " " & Me.HoraFin.Text & ":" & Me.MinFin.Text & ":00")

        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
        Try
            obj.IniciarTransaccion()
            If Request.QueryString("accion") = "M" Then

                obj.Ejecutar("DI_ModificarActividadEvaluacion", Request.QueryString("codigo_aev"), Me.dttipoval_aev.SelectedValue, finicio, ffin, Me.txtdescripcion_aev.Text.Trim, Session("codigo_usu2"), Me.dtcodigo_tca.SelectedValue, Me.dtmodo_aev.SelectedValue, Me.dtlimiteval_aev.SelectedValue)
                codigo_aev = Request.QueryString("codigo_aev")
            Else
                codigo_aev = obj.Ejecutar("DI_AgregarActividadEvaluacion", Request.QueryString("tiporeg_aev"), Me.dttipoval_aev.SelectedValue, finicio, ffin, Me.txtdescripcion_aev.Text.Trim, Session("idcursovirtual2"), Session("codigo_usu2"), Me.dtcodigo_tca.SelectedValue, Me.dtmodo_aev.SelectedValue, Me.dtlimiteval_aev.SelectedValue, 0)
            End If
            If Me.dtmodo_aev.SelectedValue = "F" Then
                For i As Int16 = 0 To Me.chkcodigo_aev.Items.Count - 1
                    If Me.chkcodigo_aev.Items(i).Selected = True Then
                        obj.Ejecutar("DI_AgregarRefActividadEvaluacion", codigo_aev, chkcodigo_aev.Items(i).Value)
                    End If
                Next
            End If
            obj.TerminarTransaccion()
            Response.Redirect("frmevaluaciones.aspx?idusuario=" & Session("codigo_usu2") & "&idcursovirtual=" & Session("idcursovirtual2") & "&idvisita=" & Session("idvisita2"))
        Catch ex As Exception
            obj.AbortarTransaccion()
            Response.Write("Ha ocurrido un error. " & ex.Message)
        End Try

        obj = Nothing
    End Sub

    Protected Sub dttipoval_aev_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dttipoval_aev.SelectedIndexChanged
        If dttipoval_aev.SelectedValue = "C" Then
            Me.dtlimiteval_aev.Visible = True
            Me.Label4.Visible = True
        Else
            Me.dtlimiteval_aev.Visible = False
            Me.Label4.Visible = False
        End If
    End Sub

    Protected Sub CmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdCancelar.Click
        Response.Redirect("frmevaluaciones.aspx?idusuario=" & Session("codigo_usu2") & "&idcursovirtual=" & Session("idcursovirtual2") & "&idvisita=" & Session("idvisita2"))
    End Sub
End Class


Partial Class librerianet_personal_frmVistadeHorario
    Inherits System.Web.UI.Page

    Dim codigo_per As Integer
    Dim codigo_pel As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        codigo_per = Request.QueryString("id")

        'codigo_per = 684 

        'función que devuelva el codigo_pel vigente
        Dim obj As New clsPersonal
        codigo_pel = obj.ConsultarPeridoLaborable
        'Response.Write(codigo_pel)
        If Not IsPostBack Then
            cargarControles()
            consultarVistaHorario()
            consultarListaHorario()
            consultarDatosGenerales()
        End If
        ConsultarListaCambiosHorarios()
    End Sub


    Private Sub ConsultarListaCambiosHorarios()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarListaCambiosHorarios(codigo_per, codigo_pel)
        gvListaCambios.DataSource = dts
        gvListaCambios.DataBind()
    End Sub
    Private Sub cargarControles()
        'Dim obj As New clsPersonal
        'Dim dts As New Data.DataTable
        'Dim dtsEscuela As New Data.DataTable

        'Dim dtsFacultad As New Data.DataTable

        'dts = obj.ConsultarHorasControl()
        'ddlHoraInicio.DataSource = dts
        'ddlHoraInicio.DataTextField = "hora"
        'ddlHoraInicio.DataValueField = "hora"
        'ddlHoraInicio.DataBind()
        'ddlHoraFin.DataSource = dts
        'ddlHoraFin.DataTextField = "hora"
        'ddlHoraFin.DataValueField = "hora"
        'ddlHoraFin.DataBind()
        ''cargo la lista de escuelas
        'dtsEscuela = obj.ConsultarCarreraProfesional()
        'ddlEscuela.DataSource = dtsEscuela
        'ddlEscuela.DataTextField = "nombre_cpf"
        'ddlEscuela.DataValueField = "codigo_cpf"
        'ddlEscuela.DataBind()
        ''cargo la lista de facultades
        'dtsFacultad = obj.ConsultarFacultad()
        'ddlFacultad.DataSource = dtsFacultad
        'ddlFacultad.DataTextField = "nombre_fac"
        'ddlFacultad.DataValueField = "codigo_fac"
        'ddlFacultad.DataBind()

    End Sub
    Private Sub consultarDatosGenerales()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable


    End Sub
    Private Sub consultarVistaHorario()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarVistaHorario(codigo_per, codigo_pel)
        gvVistaHorario.DataSource = dts
        gvVistaHorario.DataBind()

        lblHorasSemanales.text = obj.ConsultarTotalHorasSemana(codigo_per, codigo_pel)
    End Sub
    Private Sub consultarListaHorario()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarListaHorario(codigo_per, codigo_pel)
        gvEditHorario.DataSource = dts
        gvEditHorario.DataBind()
    End Sub



    Protected Sub gvEditHorario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEditHorario.RowDataBound
        e.Row.Cells(1).Visible = False
    End Sub



    'Protected Sub gvEditHorario_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvEditHorario.RowDeleting
    '    Dim obj As New clsPersonal
    '    obj.EliminarHorarioPersonal(gvEditHorario.Rows(e.RowIndex).Cells(1).Text)
    '    consultarVistaHorario()
    '    consultarListaHorario()
    'End Sub

    Protected Sub gvVistaHorario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvVistaHorario.RowDataBound
        e.Row.Cells(2).Width = 40
        e.Row.Cells(3).Width = 40
        e.Row.Cells(4).Width = 40
        e.Row.Cells(5).Width = 40
        e.Row.Cells(6).Width = 40
        e.Row.Cells(7).Width = 40
        Select Case e.Row.Cells(2).Text
            Case "I"
                e.Row.Cells(2).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(2).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(2).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(2).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(2).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(2).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(2).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(2).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(2).BackColor = Drawing.Color.Lime
        End Select

        Select Case e.Row.Cells(3).Text
            Case "I"
                e.Row.Cells(3).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(3).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(3).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(3).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(3).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(3).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(3).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(3).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(3).BackColor = Drawing.Color.Lime
        End Select
        Select Case e.Row.Cells(4).Text
            Case "I"
                e.Row.Cells(4).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(4).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(4).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(4).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(4).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(4).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(4).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(4).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(4).BackColor = Drawing.Color.Lime
        End Select
        Select Case e.Row.Cells(5).Text
            Case "I"
                e.Row.Cells(5).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(5).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(5).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(5).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(5).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(5).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(5).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(5).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(5).BackColor = Drawing.Color.Lime
        End Select
        Select Case e.Row.Cells(6).Text
            Case "I"
                e.Row.Cells(6).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(6).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(6).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(6).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(6).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(6).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(6).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(6).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(6).BackColor = Drawing.Color.Lime
        End Select
        Select Case e.Row.Cells(7).Text
            Case "I"
                e.Row.Cells(7).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(7).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(7).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(7).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(7).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(7).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(7).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(7).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(7).BackColor = Drawing.Color.Lime
        End Select
        If e.Row.Cells(0).Text = "08:00" Or e.Row.Cells(0).Text = "16:30" Then
            e.Row.Cells(0).ForeColor = Drawing.Color.Blue
            e.Row.Cells(1).ForeColor = Drawing.Color.Blue
        End If
    End Sub

    Protected Sub gvEditHorario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvEditHorario.SelectedIndexChanged

    End Sub
End Class

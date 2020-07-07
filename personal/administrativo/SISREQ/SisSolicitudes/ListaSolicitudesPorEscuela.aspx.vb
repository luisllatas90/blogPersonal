
Partial Class SisSolicitudes_ListaSolicitudesPorEscuela
    Inherits System.Web.UI.Page
    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Me.CboVer.SelectedValue = 0
        Me.GvSolicitudes.DataSource = Obj.TraerDataTable("SOL_ConsultarSolicitudesPorEscuela", Me.CboSeleccionar.SelectedValue, Request.QueryString("id"), Me.TxtBuscar.Text)
        Me.GvSolicitudes.DataBind()
        Me.GvSolicitudes.SelectedIndex = -1
        Me.LblTotal.Text = Me.GvSolicitudes.Rows.Count.ToString & " registro(s)"
        Me.LblTotal.ForeColor = Drawing.Color.Blue
        ActivarControles(False)
    End Sub

    Protected Sub CboSeleccionar_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboSeleccionar.SelectedIndexChanged
        If Me.CboSeleccionar.SelectedValue = 1 Then
            Me.TxtBuscar.MaxLength = 10
        Else
            Me.TxtBuscar.MaxLength = 50
        End If
        ActivarControles(False)
    End Sub

    Protected Sub GvSolicitudes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvSolicitudes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & Fila.Row("codigo_sol").ToString & "")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('GvSolicitudes','Select$" & e.Row.RowIndex & "');")

            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")

            e.Row.Style.Add("cursor", "hand")

        End If
    End Sub

    Protected Sub GvSolicitudes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvSolicitudes.SelectedIndexChanged
        Me.HddAlumno.Value = Me.GvSolicitudes.DataKeys.Item(Me.GvSolicitudes.SelectedIndex).Values(1).ToString
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
        ActivarControles(True)
        Me.LblEstadoDes.Text = Me.GvSolicitudes.DataKeys.Item(Me.GvSolicitudes.SelectedIndex).Values(2).ToString
        With LblEstadoDes
            Select Case .Text
                Case "P"
                    .Text = "Pendiente"
                    .ForeColor = Drawing.Color.Red
                    .Font.Strikeout = False
                Case "T"
                    .Text = "Finalizada"
                    .ForeColor = Drawing.Color.Green
                    .Font.Strikeout = False
                Case "A"
                    .Text = "Anulada"
                    .ForeColor = Drawing.Color.Red
                    .Font.Strikeout = True
            End Select
        End With

    End Sub

    Protected Sub ActivarControles(ByVal valor As Boolean)

        Me.LblAsuntos.Visible = valor
        Me.LblDatos.Visible = valor
        Me.LblEstado.Visible = valor
        Me.LblMotivos.Visible = valor
        Me.DvEstudiante.Visible = valor
        Me.ImgFoto.Visible = valor
        Me.GvEvaluacion.Visible = valor
        Me.DlAsuntos.Visible = valor
        Me.DlMotivos.Visible = valor
        Me.DlSolicitud.Visible = valor
        Me.LblEstadoDes.Visible = valor
        Me.LblEstadoText.Visible = valor
        If valor = False Then
            Page.RegisterStartupScript("Solicitud", "<script>DivInformes.style.visibility ='visible'</script>")
        Else
            Page.RegisterStartupScript("Solicitud", "<script>DivInformes.style.visibility ='hidden'</script>")
        End If
    End Sub

    Protected Sub GvEvaluacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvEvaluacion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem
            If CInt(Fila.Item("codigo_res")) = 0 Then 'Verifica si las instancias ya resolvieron la solicitud
                'Verifica si es Julia quien evalua no queda como pendiente por ella tiene la funcion de visualizacion mas no de calificar
                If Fila.Item("codigo_per") = 473 Then '-->Codigo per de Julia Danjanovic
                    e.Row.Cells(2).ForeColor = Drawing.Color.Green
                    e.Row.Cells(2).Text = "-"
                Else
                    e.Row.Cells(2).ForeColor = Drawing.Color.Red
                    e.Row.Cells(2).Text = "Pendiente"
                End If
            Else
                e.Row.Cells(2).ForeColor = Drawing.Color.Green
                e.Row.Cells(2).Text = "Ya dió respuesta"
                If e.Row.Cells(3).Text = "APROBADO" Then
                    e.Row.Cells(3).ForeColor = Drawing.Color.Blue
                Else
                    e.Row.Cells(3).ForeColor = Drawing.Color.Red
                End If
            End If
        End If
       
    End Sub


    Protected Sub CboVer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboVer.SelectedIndexChanged
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Escuela_Dat As New Data.DataTable

        If Me.CboVer.SelectedValue <> "0" Then
            Escuela_Dat = Obj.TraerDataTable("SOL_ConsultarSolicitudesPorEscuela", 2, Request.QueryString("id"), Me.CboVer.SelectedValue)
        Else
            Escuela_Dat = Obj.TraerDataTable("SOL_ConsultarSolicitudesPorEscuela", 1, Request.QueryString("id"), "")
        End If
        If Escuela_Dat.Rows.Count > 0 Then
            If Escuela_Dat.Rows(0).Item("codigo_sol") IsNot System.DBNull.Value Then
                Me.GvSolicitudes.DataSource = Escuela_Dat
                Me.GvSolicitudes.DataBind()
                Me.LblTotal.Text = Me.GvSolicitudes.Rows.Count.ToString & " registro(s)"
                Me.LblTotal.ForeColor = Drawing.Color.Blue
            Else
                Me.GvSolicitudes.DataSource = Nothing
                Me.GvSolicitudes.DataBind()
            End If
        Else
            Me.GvSolicitudes.DataSource = Nothing
            Me.GvSolicitudes.DataBind()
        End If
        ActivarControles(False)
    End Sub

    Protected Sub TxtBuscar_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtBuscar.TextChanged
        ActivarControles(False)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim Escuela_Dat As New Data.DataTable
            Escuela_Dat = Obj.TraerDataTable("SOL_ConsultarSolicitudesPorEscuela", 1, Request.QueryString("id"), "")
            If Escuela_Dat.Rows.Count > 0 Then
                If Escuela_Dat.Rows(0).Item("codigo_sol") IsNot System.DBNull.Value Then
                    Me.GvSolicitudes.DataSource = Escuela_Dat
                    Me.GvSolicitudes.DataBind()
                    Me.LblTotal.Text = Me.GvSolicitudes.Rows.Count.ToString & " registro(s)"
                    Me.LblTotal.ForeColor = Drawing.Color.Blue
                Else
                    Me.GvSolicitudes.DataSource = Nothing
                    Me.GvSolicitudes.DataBind()
                End If
            Else
                Me.GvSolicitudes.DataSource = Nothing
                Me.GvSolicitudes.DataBind()
            End If
            ActivarControles(False)
        End If
    End Sub

    Protected Sub DvEstudiante_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DvEstudiante.DataBound
        If DvEstudiante.Rows(5).Cells(1).Text = "Activo" Then
            DvEstudiante.Rows(5).Cells(1).ForeColor = Drawing.Color.Blue
        Else
            DvEstudiante.Rows(5).Cells(1).ForeColor = Drawing.Color.Red
        End If

        If DvEstudiante.Rows(7).Cells(1).Text = "Si" Then
            DvEstudiante.Rows(7).Cells(1).ForeColor = Drawing.Color.Red
        Else
            DvEstudiante.Rows(7).Cells(1).ForeColor = Drawing.Color.Blue
        End If
        DvEstudiante.Rows(6).Cells(1).ForeColor = Drawing.Color.Blue
    End Sub


End Class


Partial Class medicina_administrador_BusquedaAlumno
    Inherits System.Web.UI.Page
    Dim cod_alu As Int32
    Dim nombres As String

    Protected Sub DdlBusqueda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlBusqueda.SelectedIndexChanged
        If Me.DdlBusqueda.SelectedValue = 1 Then
            Me.TxtBusqueda.MaxLength = 10
        Else
            Me.TxtBusqueda.MaxLength = 50
        End If
    End Sub


    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        obj.AbrirConexion()
        If Me.DdlBusqueda.SelectedValue = 1 Then
            Me.GridAlumnos.DataSource = obj.TraerDataTable("med_consultaralumno", "CU", Me.TxtBusqueda.Text)
        Else
            Me.GridAlumnos.DataSource = obj.TraerDataTable("med_consultaralumno", "AL", Me.TxtBusqueda.Text)
        End If
        obj.CerrarConexion()
        Me.GridAlumnos.DataBind()
    End Sub

    Protected Sub GridAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridAlumnos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            'e.Row.Attributes.Add("OnClick", "ResaltarfilaDetalle_net('',this,'DetalleAlumno.aspx?CodUniv=" & e.Row.Cells(1).Text & "&Nombres=" & e.Row.Cells(2).Text & "&cod_alu=" & fila.Row("codigo_alu") & "')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('GridAlumnos','Select$" & e.Row.RowIndex & "');") 'location.href='DetalleAlumno.aspx?CodUniv=" & e.Row.Cells(1).Text & "&Nombres=" & e.Row.Cells(2).Text & "&cod_alu=" & fila.Row("codigo_alu") & "'")

            cod_alu = fila.Row("codigo_alu")
            nombres = fila.Row("alumno")
            If fila.row("estadoDeuda_Alu") = 1 Then
                e.Row.Cells(5).Text = "Si"
                e.Row.Cells(5).ForeColor = Drawing.Color.Blue
            Else
                e.Row.Cells(5).Text = "No"
                e.Row.Cells(5).ForeColor = Drawing.Color.red
            End If
            If fila.row("estadoActual_Alu") = 1 Then
                e.Row.Cells(4).Text = "Activo"
                e.Row.Cells(4).ForeColor = Drawing.Color.Blue
            Else
                e.Row.Cells(4).Text = "Inactivo"
                e.Row.Cells(4).ForeColor = Drawing.Color.Red
            End If
        End If
    End Sub


    Protected Sub page_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            Me.pnlDetalle.Visible = False
            'Ciclo académico 
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.DdlCicloAcad, obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")
            Me.DdlCicloAcad.SelectedIndex = 0
            obj.CerrarConexion()

        End If
    End Sub

    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        'http://localhost/notas/medicina/administrador/BusquedaAlumno.aspx
        If Not IsPostBack Then
            If Request.QueryString("id") = "" And Request.QueryString("ctf") = "" Then
                'Response.Redirect("http://www.usat.edu.pe")
                'xDguevara
                Response.Redirect("//intranet.usat.edu.pe/campusvirtual")
            End If
        End If
    End Sub

    Protected Sub GridAlumnos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridAlumnos.SelectedIndexChanged
        Dim cod_alu As Int64
        Me.pnlDetalle.Visible = True
        cod_alu = Me.GridAlumnos.SelectedValue
        ConsultarNotasAsistenciasAlumno(cod_alu)
        Me.LblCodUniv.Text = GridAlumnos.SelectedRow.Cells(1).Text
        Me.LblNombres.Text = GridAlumnos.SelectedRow.Cells(2).Text
    End Sub


    Protected Sub DdlCicloAcad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DdlCicloAcad.SelectedIndexChanged
        Dim cod_alu As Int64
        cod_alu = Me.GridAlumnos.SelectedValue
        ConsultarNotasAsistenciasAlumno(cod_alu)
    End Sub

    Private Sub ConsultarNotasAsistenciasAlumno(ByVal cod_alu As Int64)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.GridBoleta.DataSource = obj.TraerDataTable("MED_BoletaDetalleAlumno", cod_alu, Me.DdlCicloAcad.SelectedValue)
        Me.GridBoleta.DataBind()
        obj.CerrarConexion()

    End Sub

    Protected Sub GridBoleta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridBoleta.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('GridBoleta','Select$" & e.Row.RowIndex & "')")
        End If
    End Sub

    Protected Sub CmdAsistencias_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAsistencias.Click
        Dim cod_syl, cod_dma As Int64
        'Response.Write(Me.GridBoleta.DataKeys.Item(Me.GridBoleta.SelectedIndex).Values(1))
        If GridBoleta.SelectedValue > 0 Then
            cod_syl = CInt(Me.GridBoleta.DataKeys.Item(Me.GridBoleta.SelectedIndex).Values(1))
            cod_dma = CInt(Me.GridBoleta.DataKeys.Item(Me.GridBoleta.SelectedIndex).Values(2))

            Response.Redirect("reporteasistenciaynotas.aspx?codigo_cup=" & Me.GridBoleta.SelectedValue & _
                    "&codigo_syl=" & cod_syl & _
                    "&codigo_dma=" & cod_dma & _
                    "&codu=" & Me.LblCodUniv.Text & _
                    "&codigo_sem=" & Me.DdlCicloAcad.SelectedValue & _
                    "&nombre_cur=" & GridBoleta.SelectedRow.Cells(1).Text & _
                    "&nombre_per=" & GridBoleta.SelectedRow.Cells(2).Text & _
                    "&nom=" & Me.LblNombres.Text)
        Else
            ClientScript.RegisterStartupScript(GridBoleta.GetType, "Alerta", "alert('Seleccione un curso para visualizar asistencias y notas');", True)
        End If
    End Sub



    Protected Sub CmdConsolidado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdConsolidado.Click
        Response.Redirect("detalleAlumno.aspx?codu=" & Me.LblCodUniv.Text & "&id=" & Me.GridAlumnos.SelectedValue & "&cac=" & Me.DdlCicloAcad.SelectedValue)
    End Sub
End Class


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
            Me.GridAlumnos.DataSource = obj.TraerDataTable("med_consultaralumno", "CU3", Me.TxtBusqueda.Text)
        Else
            Me.GridAlumnos.DataSource = obj.TraerDataTable("med_consultaralumno", "AL3", Me.TxtBusqueda.Text)
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
            ' e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('GridAlumnos','Select$" & e.Row.RowIndex & "');") 'location.href='DetalleAlumno.aspx?CodUniv=" & e.Row.Cells(1).Text & "&Nombres=" & e.Row.Cells(2).Text & "&cod_alu=" & fila.Row("codigo_alu") & "'")

            cod_alu = fila.Row("codigo_alu")
            nombres = fila.Row("alumno")
            If fila.row("estadoDeuda_Alu") = 1 Then
                e.Row.Cells(5).Text = "Si"
                e.Row.Cells(5).ForeColor = Drawing.Color.RED
            Else
                e.Row.Cells(5).Text = "No"
                e.Row.Cells(5).ForeColor = Drawing.Color.blue
            End If
            If fila.row("estadoActual_Alu") = 1 Then
                e.Row.Cells(4).Text = "Activo"
                e.Row.Cells(4).ForeColor = Drawing.Color.Blue
            Else
                e.Row.Cells(4).Text = "Inactivo"
                e.Row.Cells(4).ForeColor = Drawing.Color.Red
            End If

            'Change xDguevara 02.11.2012
            e.Row.Cells(6).Text = "<a href='//intranet.usat.edu.pe/rptusat/?/PRIVADOS/ACADEMICO/ACAD_RepAsistConsoliAlumnoMDL&codigo_cac=" & Me.DdlCicloAcad.SelectedValue & "&codigo_alu=" & cod_alu & "' title='Clic aquí para mostrar el registro de asistencias'>Ver</a>"
            e.Row.Cells(7).Text = "<a href='//intranet.usat.edu.pe/rptusat/?/PRIVADOS/ACADEMICO/ACAD_RepNotasConsoliAlumnoMDL&codigo_cac=" & Me.DdlCicloAcad.SelectedValue & "&codigo_alu=" & cod_alu & "' title='Clic aquí para mostrar las notas parciales'>Ver</a>"
            'Response.Redirect("ReptConsolidadoWeb.aspx?codal=" & CInt(Me.GridAlumnos.DataKeys.Item(Me.GridAlumnos.SelectedIndex).Values(0)) & "&cac=" & Me.DdlCicloAcad.SelectedValue)

            'e.Row.Cells(6).Text = "<a href='ReptConsolidadoWebAsistenciasMDL.aspx?codal=" & cod_alu & "&cac=" & Me.DdlCicloAcad.SelectedValue & "' title='Clic aquí para mostrar el registro de asistencias'>Ver</a>"

        End If

    End Sub

    Protected Sub page_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.DdlCicloAcad, obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")
            Me.DdlCicloAcad.SelectedIndex = 0
            obj.CerrarConexion()
            Me.DdlBusqueda.SelectedValue = 2

        End If
    End Sub

    Protected Sub GridAlumnos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridAlumnos.SelectedIndexChanged
        'Response.Redirect("ReptConsolidadoWeb.aspx?codal=" & CInt(Me.GridAlumnos.DataKeys.Item(Me.GridAlumnos.SelectedIndex).Values(0)) & "&cac=" & Me.DdlCicloAcad.SelectedValue)
    End Sub

End Class

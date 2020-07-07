
Partial Class investigaciones_investigaciones
    Inherits System.Web.UI.Page

    Private Sub LlenarInvestigaciones()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim ObjCombos As New ClsFunciones
            Dim Objinv As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim Escuelas As New Data.DataTable
            Escuelas = Objinv.TraerDataTable("ConsultarCarreraProfesional", "IV", "")
            ObjCombos.LlenarListas(Me.DDLEscuela, Escuelas, "codigo_cpf", "nombre_cpf", "---- Seleccione Escuela Profesional ----")
            Me.GridCursos.DataBind()
        End If

    End Sub

    Protected Sub DDLEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLEscuela.SelectedIndexChanged
        Dim ObjCombos As New ClsFunciones
        Dim Objinv As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Cursos As New Data.DataTable
        Cursos = Objinv.TraerDataTable("INVALU_ConsultarCursosEscuela", Me.DDLEscuela.SelectedValue)
        Me.GridCursos.DataSource = Cursos
        Me.GridCursos.DataBind()
    End Sub

    Protected Sub GridCursos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridCursos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem 'Los datos devueltos (cuando quiero saber la data que llega)
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "ResaltarfilaDetalle_net('',this,'detalleinvestigacion.aspx?cpf=" & Me.DDLEscuela.SelectedValue & "&Inv=" & fila.Row("curso").ToString & "')")
            e.Row.Attributes.Add("id", "fila" & (e.Row.RowIndex + 1).ToString & "")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
        End If
    End Sub
End Class


Partial Class Egresados_frmListaEgresadosNew
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim obj As New ClsConectarDatos
        Dim dt As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        '#Nivel
        Me.ddlNivel.Items.Add("TODOS")
        Me.ddlNivel.Items.Add("PRE GRADO")
        Me.ddlNivel.Items.Add("POST GRADO")
        Me.ddlNivel.Items.Add("POST TITULO")

        '#Modalidad
        dt = New Data.DataTable
        dt = obj.TraerDataTable("ALUMNI_ListarModalidad")
        Me.ddlModalidad.DataSource = dt
        Me.ddlModalidad.DataTextField = "nombre"
        Me.ddlModalidad.DataValueField = "codigo"
        Me.ddlModalidad.DataBind()
        dt.Dispose()

        '#Facultad
        dt = New Data.DataTable
        'dt = obj.TraerDataTable("ALUMNI_ListarFacultad")
        If Request.QueryString("ctf") = 145 Then
            dt = obj.TraerDataTable("ALUMNI_ListarFacultadCoordinador", Request.QueryString("ID"))
        Else
            dt = obj.TraerDataTable("ALUMNI_ListarFacultad")
        End If
        Me.ddlFacultad.DataSource = dt
        Me.ddlFacultad.DataTextField = "nombre"
        Me.ddlFacultad.DataValueField = "codigo"
        Me.ddlFacultad.DataBind()
        Me.ddlFacultad.SelectedValue = 0
        dt.Dispose()

        '#Escuela
        dt = New Data.DataTable
        If Request.QueryString("ctf") = 145 Then
            If Me.ddlFacultad.SelectedItem.Text = "TODOS" Then
                dt = obj.TraerDataTable("ALUMNI_ListarCarreraProfesionalPersonal", 2, Request.QueryString("ID"), "%")
            Else
                dt = obj.TraerDataTable("ALUMNI_ListarCarreraProfesionalPersonal", 2, Request.QueryString("ID"), ddlFacultad.SelectedValue)
            End If
        Else
            '' Modificado
            dt = obj.TraerDataTable("ALUMNI_ListarCarreraProfesional", "2", "2")
        End If

        Me.ddlEscuela.DataSource = dt
        Me.ddlEscuela.DataTextField = "nombre"
        Me.ddlEscuela.DataValueField = "codigo"
        Me.ddlEscuela.DataBind()
        dt.Dispose()
        obj.CerrarConexion()
        obj = Nothing

        '#Años
        Me.ddlEgreso.Items.Add("TODOS")
        Me.ddlBachiller.Items.Add("TODOS")
        Me.ddlTitulo.Items.Add("TODOS")
        For i As Integer = 2004 To Date.Today.Year
            Me.ddlEgreso.Items.Add(i)
            Me.ddlBachiller.Items.Add(i)
            Me.ddlTitulo.Items.Add(i)
        Next
        Me.ddlEgreso.SelectedIndex = 0
        Me.ddlBachiller.SelectedIndex = 0
        Me.ddlTitulo.SelectedIndex = 0

    End Sub
    Protected Sub ddlFacultad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFacultad.SelectedIndexChanged
        Try
            If Me.ddlFacultad.SelectedIndex > -1 Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()

                If Request.QueryString("ctf") = 145 Then
                    If Me.ddlFacultad.SelectedItem.Text = "TODOS" Then
                        Me.ddlEscuela.DataSource = obj.TraerDataTable("ALUMNI_ListarCarreraProfesionalPersonal", 2, Request.QueryString("ID"), "%")
                    Else
                        Me.ddlEscuela.DataSource = obj.TraerDataTable("ALUMNI_ListarCarreraProfesionalPersonal", 2, Request.QueryString("ID"), ddlFacultad.SelectedValue)
                    End If
                Else
                    Me.ddlEscuela.DataSource = obj.TraerDataTable("Alumni_ListarEscuelasxFacultad", CInt(Me.ddlFacultad.SelectedValue))
                End If

                Me.ddlEscuela.DataTextField = "nombre"
                Me.ddlEscuela.DataValueField = "codigo"

                Me.ddlEscuela.DataBind()
                obj.CerrarConexion()
                obj = Nothing
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
End Class

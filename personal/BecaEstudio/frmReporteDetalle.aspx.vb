
Partial Class BecaEstudio_frmReporteDetalle
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarCombos()

        Else
            Session.Clear()
        End If
    End Sub
    Sub CargarCombos()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        tb = obj.TraerDataTable("Beca_ConsultarCicloAcademico")
        Me.ddlCiclo.DataSource = tb
        Me.ddlCiclo.DataTextField = "descripcion_cac"
        Me.ddlCiclo.DataValueField = "codigo_cac"
        Me.ddlCiclo.DataBind()
        If Session("Beca_codigo_cac") IsNot Nothing Then
            Me.ddlCiclo.SelectedValue = Session("codigo_cac")
        End If

        tb = obj.TraerDataTable("Beca_ConsultarCarreraProfesional")
        Me.ddlEscuela.DataSource = tb
        Me.ddlEscuela.DataTextField = "nombre_cpf"
        Me.ddlEscuela.DataValueField = "codigo_cpf"
        Me.ddlEscuela.DataBind()
        If Session("codigo_cpf") IsNot Nothing Then
            Me.ddlEscuela.SelectedValue = Session("codigo_cpf")
        End If

        tb = obj.TraerDataTable("Beca_ConsultarBecaTipo")
        Me.ddlTipoBeca.DataSource = tb
        Me.ddlTipoBeca.DataTextField = "descripcion_bec"
        Me.ddlTipoBeca.DataValueField = "codigo_bec"
        Me.ddlTipoBeca.DataBind()
        If Session("codigo_bec") IsNot Nothing Then
            Me.ddlTipoBeca.SelectedValue = Session("codigo_bec")
        End If

        obj.CerrarConexion()
        obj = Nothing
    End Sub
End Class


Partial Class BecaEstudio_frmBecaxConvenio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargarCombos()
                CargarRegistros()

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarRegistros()
        Try
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("Beca_ListaBecaSolicitudPersonal", 0, Me.ddlCiclo.SelectedValue)
            'Response.Write(tb.Rows.Count)
            If tb.Rows.Count > 0 Then
                Me.gvListaBecas.DataSource = tb
            Else
                Me.gvListaBecas.DataSource = Nothing
            End If
            Me.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
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

   


       


        obj.CerrarConexion()
        obj = Nothing
    End Sub

  

  

   

    

   

   


   

    



 
   

    Protected Sub ddlCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCiclo.SelectedIndexChanged
        CargarRegistros()
    End Sub


End Class

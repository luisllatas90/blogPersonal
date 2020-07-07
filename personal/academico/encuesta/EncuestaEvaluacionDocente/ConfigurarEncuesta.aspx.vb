Imports System.Data

Partial Class academico_encuesta_EncuestaEvaluacionDocente_ConfigurarEncuesta
    Inherits System.Web.UI.Page


    Public Sub ConsultarDocentes()
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("[ConsultarCicloAcademico]", "VIG", "1")

            obj.CerrarConexion()
            If dt.Rows.Count Then
                Session("EVAl_codigocac") = dt.Rows(0).Item("codigo_Cac")
                Me.ddlSemestre.DataSource = dt
                Me.ddlSemestre.DataTextField = "descripcion_cac"
                Me.ddlSemestre.DataValueField = "codigo_Cac"

            Else
                Me.ddlSemestre.DataSource = Nothing
            End If
            Me.ddlSemestre.DataBind()
        Catch ex As Exception

        End Try

    End Sub
    Public Sub ConsultarTablaCalificacion()

        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("[EVAConfig_ConsultarTablaCalificacion]", Me.ddlSemestre.SelectedValue, Me.ddlTipoEncuesta.SelectedValue, Me.ddlTipoPuntuacion.SelectedValue, Me.ddlItem.SelectedValue)

            obj.CerrarConexion()
            If dt.Rows.Count Then

                Me.dgvItems.DataSource = dt

            Else
                Me.dgvItems.DataSource = Nothing
            End If
            Me.dgvItems.DataBind()
        Catch ex As Exception

        End Try

    End Sub
    Public Sub ConsultarItems()
        Try
            If Me.ddlTipoPuntuacion.SelectedValue <> "GE" Then
                Me.ddlItem.Enabled = True
                Dim dt As New Data.DataTable
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()

                dt = obj.TraerDataTable("[EVAConfig_listarItems]", Me.ddlTipoPuntuacion.SelectedValue)

                obj.CerrarConexion()
                If dt.Rows.Count Then

                    Me.ddlItem.DataSource = dt
                    Me.ddlItem.DataTextField = "nombre"
                    Me.ddlItem.DataValueField = "codigo"
                Else
                    Me.ddlItem.DataSource = Nothing
                End If
                Me.ddlItem.DataBind()

            Else
                Me.ddlItem.DataSource = Nothing
                Me.ddlItem.Items.Clear()
                Me.ddlItem.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ConsultarDocentes()
            ConsultarItems()
            ConsultarTablaCalificacion()
        End If
    End Sub

    Protected Sub ddlTipoPuntuacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoPuntuacion.SelectedIndexChanged
        ConsultarItems()
    End Sub


    Protected Sub ddlItem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlItem.SelectedIndexChanged
        ConsultarTablaCalificacion()
    End Sub

    Protected Sub ddlTipoEncuesta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoEncuesta.SelectedIndexChanged
       
        'ConsultarItems()
        'ConsultarTablaCalificacion()
    End Sub

    

    Protected Sub GridView2_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridView2.RowUpdating

    End Sub
End Class

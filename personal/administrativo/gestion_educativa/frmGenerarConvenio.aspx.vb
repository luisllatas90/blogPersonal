Imports System.Collections.Generic

Partial Class administrativo_gestion_educativa_frmGenerarConvenio
    Inherits System.Web.UI.Page

#Region "Variables"
    Private mo_RepoAdmision As New ClsAdmision
    Private ms_CodigoAlu As String = "0"
    Private ms_TipoCparc As String = "CONVENIO"
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not String.IsNullOrEmpty(Request.QueryString("alu")) Then
            ms_CodigoAlu = Request.QueryString("alu")
        End If

        If Not IsPostBack Then
            BuscarDeudasPendientes(ms_CodigoAlu, ms_TipoCparc)
            lr_CargarGrillaCuotas()
        End If
    End Sub

    Protected Sub grwConvenioParticipante_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwConvenioParticipante.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _cellsRow As TableCellCollection = e.Row.Cells
            _cellsRow(0).Text = e.Row.RowIndex + 1
            grwConvenioParticipante.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub btnGuardar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerarConvenio.ServerClick
        lr_Guardar()
        udpGenerarConvenio.Update()
    End Sub
#End Region

#Region "Métodos"
    Private Sub lr_CargarGrillaCuotas()
        Dim lo_DtCuotas As Data.DataTable = mo_RepoAdmision.ListarCuotasConvenio(ms_CodigoAlu, ms_TipoCparc)
        grwConvenioParticipante.DataSource = lo_DtCuotas
        grwConvenioParticipante.DataBind()

        spnSinCuotas.Visible = Not (lo_DtCuotas.Rows.Count > 0)
        btnGenerarConvenio.Visible = (lo_DtCuotas.Rows.Count > 0)
    End Sub

    Private Sub BuscarDeudasPendientes(ByVal ls_CodigoAlu As String, ByVal ls_TipoCparc As String)
        Dim lo_DtDeudas As New Data.DataTable
        If Not String.IsNullOrEmpty(ls_CodigoAlu) Then
            lo_DtDeudas = mo_RepoAdmision.ConsultarDeudasPorAlumnoTipoParticipante(ls_CodigoAlu, ls_TipoCparc)
        End If

        udpDeudas.Visible = (lo_DtDeudas.Rows.Count > 0)
        udpGenerarConvenio.Visible = Not (lo_DtDeudas.Rows.Count > 0)

        grwDeudas.DataSource = lo_DtDeudas
        grwDeudas.DataBind()
        udpDeudas.Update()
    End Sub

    Private Sub lr_Guardar()
        Try
            Dim ls_CodigoAlu As String = Request.Params("alu")
            Dim ls_UsuarioReg As String = Request.Params("id")

            Dim lo_Respuesta As Dictionary(Of String, String) = mo_RepoAdmision.GenerarConvenioParticipante(ms_CodigoAlu, ms_TipoCparc)
            With respuestaPostback.Attributes
                .Item("data-ispostback") = True
                .Item("data-rpta") = lo_Respuesta.Item("rpta")
                If lo_Respuesta.Item("rpta") = "-1" Then
                    .Item("data-msg") = "Ha ocurrido un error en el servidor"
                    mensajeError.InnerHtml = "No excepción"
                Else
                    .Item("data-msg") = lo_Respuesta.Item("msg")
                End If
            End With
        Catch ex As Exception
            mensajeError.InnerHtml = ex.Message
            With respuestaPostback.Attributes
                .Item("data-ispostback") = True
                .Item("data-rpta") = "-1"
                .Item("data-msg") = "Ha ocurrido un error en el servidor"
            End With
            Throw ex
        End Try
    End Sub
#End Region
End Class

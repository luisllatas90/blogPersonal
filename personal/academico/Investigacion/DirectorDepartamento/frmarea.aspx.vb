Imports System.Data
Partial Class DirectorDepartamento_frmarea
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Tabla As New DataTable
        Dim ObjInvestigacion As New Investigacion
        If IsPostBack = False Then
            If Request.QueryString("modo") = "n" Then
                Me.LblTitulo.Text = "Registrar Area de Investigación"
                Tabla = ObjInvestigacion.ConsultarPersonalCCInvestigacion("2", "", Request.QueryString("codigo"))
                Me.LstCoordinador.Items.Clear()
                Me.LstCoordinador.DataSource = Tabla
                Me.LstCoordinador.DataValueField = Tabla.Columns(0).ToString
                Me.LstCoordinador.DataTextField = Tabla.Columns(1).ToString
                Me.LstCoordinador.DataBind()
                Tabla.Dispose()
            Else
                Tabla = ObjInvestigacion.ConsultarPersonalCCInvestigacion("2", "", Request.QueryString("codigo1"))
                Me.LstCoordinador.Items.Clear()
                Me.LstCoordinador.DataSource = Tabla
                Me.LstCoordinador.DataValueField = Tabla.Columns(0).ToString
                Me.LstCoordinador.DataTextField = Tabla.Columns(1).ToString
                Me.LstCoordinador.DataBind()
                Tabla.Dispose()
                Me.LblTitulo.Text = "Modificar Area de Investigación"
                Tabla = ObjInvestigacion.ConsultarUnidadesInvestigacion("5", Request.QueryString("codigo"))
                Me.TxtNomArea.Text = Tabla.Rows(0).Item("nombre_Are").ToString
                Me.TxtProposito.Text = Tabla.Rows(0).Item("proposito_Are").ToString
                Me.LstCoordinador.SelectedValue = Tabla.Rows(0).Item("codigo_Pcc")
            End If
        End If
    End Sub

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim Tabla As New DataTable
        Dim ObjPersonalCCI As New Investigacion
        Tabla = ObjPersonalCCI.ConsultarPersonalCCInvestigacion("1", Me.TxtBuscar.Text, Request.QueryString("codigo"))
        Me.LstCoordinador.Items.Clear()
        Me.LstCoordinador.DataSource = Tabla
        Me.LstCoordinador.DataValueField = Tabla.Columns(0).ToString
        Me.LstCoordinador.DataTextField = Tabla.Columns(1).ToString
        Me.LstCoordinador.DataBind()
        Tabla = Nothing
        ObjPersonalCCI = Nothing
    End Sub

   
    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim objInv As New Investigacion
        If Request.QueryString("modo") = "n" Then
            If objInv.AgregarAreas(Me.TxtNomArea.Text.Trim, Request.QueryString("codigo"), Me.TxtProposito.Text.Trim, Me.LstCoordinador.SelectedValue) = -1 Then
                Me.LblError.ForeColor = Drawing.Color.Red
                Me.LblError.Text = "Ocurrió un error al procesar los datos"
            Else
                Response.Write("<script>window.opener.location.reload();window.close();</script>")
            End If
        Else
            If objInv.ModificarAreas(Me.TxtNomArea.Text.Trim, Request.QueryString("codigo"), Me.TxtProposito.Text.Trim, Me.LstCoordinador.SelectedValue) = -1 Then
                Me.LblError.ForeColor = Drawing.Color.Red
                Me.LblError.Text = "Ocurrió un error al procesar los datos"
            Else
                Response.Write("<script>window.opener.location.reload();window.close();</script>")
            End If
        End If
    End Sub
End Class

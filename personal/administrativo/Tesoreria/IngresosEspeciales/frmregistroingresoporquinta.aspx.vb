
Partial Class frmregistroingresoporquinta
    Inherits System.Web.UI.Page

    Dim cn As New clsaccesodatos

    Sub MostrarPrograma()
        Dim dts As New System.Data.DataSet

        dts = cn.consultar("dbo.spConsultarPrograma", "3", Session("codigo_TCL"), "", "", "")


        Me.cboprograma.DataTextField = "descripcion_pro"
        Me.cboprograma.DataValueField = "codigo_pro"
        Me.cboprograma.DataSource = dts.Tables("consulta")
        Me.cboprograma.DataBind()


    End Sub

    Sub MostrarTipoPlanilla()
        Dim dts As New System.Data.DataSet

        dts = cn.consultar("dbo.sp_vertipoplanilla", "TO", "", "", "", "", "", "")


        Me.cbotipoplanilla.DataTextField = "descripcion_tplla"
        Me.cbotipoplanilla.DataValueField = "codigo_tplla"
        Me.cbotipoplanilla.DataSource = dts.Tables("consulta")
        Me.cbotipoplanilla.DataBind()
    End Sub

    Sub MostrarMoneda()
        Dim dts As New System.Data.DataSet

        dts = cn.consultar("dbo.sp_vertipocambio", "", "", "", "", "", "", "")


        Me.cboMoneda.DataTextField = "descripcion_tip"
        Me.cboMoneda.DataValueField = "codigo_tip"
        Me.cboMoneda.DataSource = dts.Tables("consulta")
        Me.cboMoneda.DataBind()
    End Sub
    Sub mostrarañoMes()
        Dim i As Integer
        Me.cbomes.Items.Clear()
        Me.cboaño.Items.Clear()
        For i = 1 To 12
            Me.cboaño.Items.Add(2007 + i)
            Me.cbomes.Items.Add(MonthName(i).ToString.ToUpper)
        Next
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.IsPostBack = False Then
            cn.abrirconexion()
            Me.MostrarTipoPlanilla()
            Me.MostrarMoneda()
            Me.MostrarPrograma()
            cn.cerrarconexion()
            mostrarañoMes()
        End If

    End Sub

    Protected Sub cmdcancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcancelar.Click
        Me.Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub cmdgrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdgrabar.Click
        ' agregar lo items
        Dim rpta As Integer, mensaje As String = ""
        Try
            cn.abrirconexiontrans()
            cn.ejecutar("dbo.spAgregarInformePrograma", False, rpta, mensaje, Me.cboprograma.SelectedValue, _
                                                                                Me.cbotipoplanilla.SelectedValue, _
                                                                                Me.cbomes.SelectedIndex + 1, _
                                                                                Me.cboaño.Text, _
                                                                                Me.cboMoneda.SelectedValue, _
                                                                                Me.txtdescripcion.Text, _
                                                                               0, "")

            If rpta > 0 Then
                cn.cerrarconexiontrans()
                Me.Response.Write("<script>alert('Se ha grabado correctamente la información');window.opener.location.href=window.opener.location.href;window.close();</script>")
            Else
                cn.cancelarconexiontrans()
                Me.lblmensaje.Text = mensaje & ", se ha cancelado la operación"

            End If

        Catch ex As Exception
            cn.cancelarconexiontrans()
            Me.lblmensaje.Text = "Ocurrió un error en la operación y ha sido cancelada : " & ex.Message
        End Try
    End Sub
End Class

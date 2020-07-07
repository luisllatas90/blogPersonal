Imports System.Data

Partial Class frmdetallerendicion

    Inherits System.Web.UI.Page
    Public cn As New clsaccesodatos, dts As New DataSet
    Public Mostrarfinalizar As Boolean
    Public estado_rendicion As String

    Sub mostrarinformacion()

        ' verificar el estado de la rendicio
        Dim dtsrendicion As New DataSet
        cn.abrirconexion()
        dts = cn.consultar("sp_verdetallerendicion", "PR", Me.hd.Value.ToString, "", "", "", "")
        Dim dtsdetalleegreso As New DataSet
        ' el detalle del egreso por el cpodigo de rendición
        dtsdetalleegreso = cn.consultar("dbo.sp_verdocumentoemitidos", "VDREND", Me.hd.Value, "", "", "", "")
        cn.cerrarconexion()

        ' mostrar la información del egreso y el detalle
        Me.lbldocumento.Text = dtsdetalleegreso.Tables("consulta").Rows(0).Item("descripcion_tdo")
        Me.lblfecha.Text = dtsdetalleegreso.Tables("consulta").Rows(0).Item("fechagen_egr")
        Me.lblimporteentregado.Text = dtsdetalleegreso.Tables("consulta").Rows(0).Item("importe_deg")
        Me.lblrubro.Text = dtsdetalleegreso.Tables("consulta").Rows(0).Item("descripcion_rub")
        Me.lblnumero.Text = dtsdetalleegreso.Tables("consulta").Rows(0).Item("seriedoc_egr").ToString & "-" & dtsdetalleegreso.Tables("consulta").Rows(0).Item("numerodoc_egr")
        Me.lblimporterendido.Text = dtsdetalleegreso.Tables("consulta").Rows(0).Item("montorendido_deg")
        Me.lblimportedevuelto.Text = dtsdetalleegreso.Tables("consulta").Rows(0).Item("montodevuelto_deg")
        Me.lblusuario.Text = dtsdetalleegreso.Tables("consulta").Rows(0).Item("usuarioreg_egr")
        Me.lblsaldo.Text = dtsdetalleegreso.Tables("consulta").Rows(0).Item("saldorendir_deg")
        Me.lblobservacion.Text = dtsdetalleegreso.Tables("consulta").Rows(0).Item("observacion_deg")
        Me.lblmoneda.Text = dtsdetalleegreso.Tables("consulta").Rows(0).Item("descripcion_tip")

        'Mostrar la información del detalle del egreso

        Me.lstinformacion.DataSource = dts
        Me.lstinformacion.DataBind()


        If dts.Tables("consulta").Rows.Count = 0 Then
            Me.lblmensaje.Text = "No existen detalles para esta rendición"
        Else
            Me.lblmensaje.Text = ""
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            Dim dtsestadorendicion As System.Data.DataSet, EstadoFinalizar As String

            Me.hd.Value = Me.Request.QueryString("codigo_rend") ' codigo de la rendicion
            Me.hdpagina.Value = Me.Request.QueryString("pagina")

            cn.abrirconexion()
            dtsestadorendicion = cn.consultar("dbo.sp_verrendicion", "BCO", Me.hd.Value, "", "", "", "")

            cn.cerrarconexion()

            EstadoFinalizar = dtsestadorendicion.Tables("consulta").Rows(0).Item("estado_rend")
            If EstadoFinalizar = "P" Then
                Mostrarfinalizar = True
            Else
                Mostrarfinalizar = False
            End If
            Me.hdestado.Value = EstadoFinalizar
            estado_rendicion = EstadoFinalizar
            mostrarinformacion()
        End If

    End Sub

    Protected Sub cmdagregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ' ShowDialogModal("urlPopup",window,"Propiedades");

        'Response.Write("<script> window.open('frmregistrardetallerendicion.aspx?codigo_rend=" & Me.hd.Value.ToString.Trim & "','frmadjuntararchivo','toolbar=no,width=980,height=380') </script>")
    End Sub

    Protected Sub lstinformacion_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles lstinformacion.RowCreated

    End Sub

    Protected Sub lstinformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles lstinformacion.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Cells(5).Attributes.Add("onclick", "window.open('frmsubirarchivo.aspx?codigo_dren=" & e.Row.Cells(0).Text.ToString & "&estado=" & estado_rendicion & "','frmadjuntararchivo','toolbar=no,width=850,height=460')")
            e.Row.Attributes.Add("seleccionado", 0)
            e.Row.Attributes.Add("onclick", "pintarfila(this,'" & Me.lstinformacion.ID.ToString & "');document.all.documentodetalle.src='frmadjuntararchivo.aspx?codigo_dren=" & e.Row.Cells(0).Text.ToString & "&estado=" & estado_rendicion & "'")
            e.Row.ID = e.Row.Cells(0).Text.ToString
            e.Row.Cells(7).Attributes.Add("ALT", "Quitar registro")
            'e.Row.Attributes.Add("onclick", "pintarfila(this)")


            e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")
            e.Row.Attributes.Add("onmouseleave", "resaltar(this,0)")
            'e.Row.Attributes.Add("onclick", "window.open('frmadjuntararchivo.aspx?codigo_dren=1','as','toolbar=no')")
        End If
    End Sub

    Protected Sub lstinformacion_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles lstinformacion.RowDeleting
        Dim rpta As Integer, mensaje As String = ""

        cn.abrirconexiontrans()
        cn.ejecutar("dbo.sp_quitardetallerendicion", False, rpta, mensaje, CInt(Me.lstinformacion.Rows(e.RowIndex).Cells(0).Text), 0, "")
        cn.cerrarconexiontrans()
        mostrarinformacion()
    End Sub

    Protected Sub cmdcancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcancelar.Click
        Response.Write("<script>window.close(); </script>")
    End Sub

    Protected Sub hd_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles hd.ValueChanged

    End Sub

    Protected Sub lstinformacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstinformacion.SelectedIndexChanged

    End Sub

    Protected Sub form1_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Init

    End Sub
End Class

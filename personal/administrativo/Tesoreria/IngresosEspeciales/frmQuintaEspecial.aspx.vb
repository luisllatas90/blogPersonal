
Partial Class frmQuintaEspecial
    Inherits System.Web.UI.Page

    Dim cn As New clsaccesodatos
    Dim dtsInformePrograma As New System.Data.DataSet
    Dim codigo_ipr As Integer
    Sub CargarPersonal()
        Dim dts As New System.Data.DataSet, i As Integer


        dts = cn.consultar("dbo.sp_vercliente", "1", "", "", "", "", "", "")

        ' cargar en el tree view 

        Dim pad As New TreeNode
        pad.Text = "Personal disponible"
        pad.Value = "0"

        Me.trvpersonal.Nodes.Add(pad)
        For i = 1 To dts.Tables("consulta").Rows.Count
            Dim nod As New TreeNode
            nod.SelectAction = TreeNodeSelectAction.None
            nod.Value = dts.Tables("consulta").Rows(i - 1).Item("codigo_tcl").ToString.ToUpper
            nod.Text = dts.Tables("consulta").Rows(i - 1).Item("nombres").ToString.ToUpper
            pad.ChildNodes.Add(nod)
        Next

    End Sub

    Sub mostrarinformacionPrograma()
        Dim dtsPrograma As New System.Data.DataSet
        Me.lblimportetotal.Text = "0.00"
        dtsPrograma = cn.consultar("dbo.ConsultarInformePrograma", "2", codigo_ipr, "", "", "")
        If dtsPrograma.Tables("consulta").Rows.Count > 0 Then
            Me.lblprograma.Text = dtsPrograma.Tables("consulta").Rows(0).Item("descripcion_pro")
            Me.lblplanilla.Text = dtsPrograma.Tables("consulta").Rows(0).Item("descripcion_tplla")
            Me.lblmes.Text = dtsPrograma.Tables("consulta").Rows(0).Item("mes_ipr")
            Me.lblaño.Text = dtsPrograma.Tables("consulta").Rows(0).Item("año_ipr")
            Me.lblimportetotal.Text = "Importe total del Informe : " & Format(dtsPrograma.Tables("consulta").Rows(0).Item("importe_ipr"), "###,###,##0.00")
        End If

    End Sub
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        codigo_ipr = Me.Request.QueryString("codigo_ipr")
        If Me.IsPostBack = False Then
            cn.abrirconexion()
            Me.CargarPersonal()
            Me.mostrarinformacionPrograma()
            cn.cerrarconexion()
            Me.mostrarInformacion()
        End If

    End Sub

    Sub mostrarInformacion()

        cn.abrirconexion()
        dtsInformePrograma = cn.consultar("dbo.ConsultarInformePrograma", "3", codigo_ipr, "", "", "", "", "", "", "", "")
        mostrarinformacionPrograma()
        cn.cerrarconexion()
        Me.lstinformacion.DataSource = dtsInformePrograma.Tables("consulta")
        Me.lstinformacion.DataBind()
        ' mostrar totaes
    End Sub
    
    Protected Sub cmdagregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdagregar.Click

        Dim rpta As Integer, mensaje As String
        ' agregar a la base y mostralo
        'Me.lstinformacion.Enabled = False
        Dim trv As New TreeNode
        trv = Me.trvpersonal.Nodes(0)
        Dim i As Integer
        cn.abrirconexiontrans()
        For i = 1 To trv.ChildNodes.Count
            If trv.ChildNodes(i - 1).Checked = True And CDbl(trv.ChildNodes(i - 1).Value) > 0 Then
                cn.ejecutar("dbo.spAgregarDetalleInformePrograma", False, rpta, mensaje, codigo_ipr, trv.ChildNodes(i - 1).Value, "0.00", 0, "")
                'dat.Rows.Add(trv.ChildNodes(i - 1).Value, trv.ChildNodes(i - 1).Text, "0.00")
                If rpta <= 0 Then
                    cn.cancelarconexiontrans()
                    Me.mostrarInformacion()
                    Me.lblobservacion.Text = mensaje
                    Exit Sub
                End If
            End If
        Next
        cn.cerrarconexiontrans()
        Me.mostrarInformacion()
        'Me.lstinformacion.Enabled = True
    End Sub

    Protected Sub trvpersonal_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvpersonal.SelectedNodeChanged

    End Sub

    Protected Sub lstinformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles lstinformacion.RowDataBound
        ' cargando la información

        e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")
        e.Row.Attributes.Add("onmouseleave", "resaltar(this,0)")
        e.Row.Cells(5).Attributes.Add("onclick", "confirm('¿Está Ud. eliminar este registro?');")
    End Sub


    Protected Sub cmdregistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdregistrar.Click

        Dim txt As TextBox, rpta As Integer, mensaje As String = ""
        Dim i As Integer
        cn.abrirconexiontrans()
        For i = 1 To Me.lstinformacion.Rows.Count
            txt = CType(Me.lstinformacion.Rows(i - 1).FindControl("txtimporte_dip"), TextBox)
            ' guardar los cambios en el Sistema
            Me.lblobservacion.Text = ""
            If txt.Text <> "" And IsNumeric(txt.Text) = True Then
                cn.ejecutar("dbo.spmodificarImporteDetalleInformePrograma", False, rpta, mensaje, Me.lstinformacion.Rows(i - 1).Cells(1).Text, CDbl(txt.Text), 0, "")
            End If
            If txt.Text <> "" And IsNumeric(txt.Text) = False Then
                Me.lblobservacion.Text = "Se han ingresado valores para los importes no válidos, se ha cancelado la operación"
                cn.cancelarconexiontrans()
                'Me.mostrarInformacion()
                Exit Sub
            End If
        Next
        cn.cerrarconexiontrans()
        Me.mostrarInformacion()
        'Response.Write("<script>alert('Se han guardado los cambios satisfactoriamente')</script>")


    End Sub

    Protected Sub cmdeliminarseleccionados_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdeliminarseleccionados.Click
        Dim chk As CheckBox, rpta As Integer, mensaje As String = ""
        Dim i As Integer
        cn.abrirconexiontrans()
        For i = 1 To Me.lstinformacion.Rows.Count
            chk = CType(Me.lstinformacion.Rows(i - 1).FindControl("chkseleccion"), CheckBox)
            ' guardar los cambios en el Sistema
            If chk.Checked = True Then
                cn.ejecutar("dbo.spAnularDetalleInformePrograma", False, rpta, mensaje, Me.lstinformacion.Rows(i - 1).Cells(1).Text, 0, "")
            End If
        Next
        cn.cerrarconexiontrans()
        Me.mostrarInformacion()

        'Response.Write("<script>alert('Se han guardado los cambios satisfactoriamente')</script>")


    End Sub

    Protected Sub cmdfinalizarEdicion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdfinalizarEdicion.Click
        'finalizar la edición del informe y ponerlo a disposición de contabilidad  tesoreria para el procesamiento
        Dim rpta As Integer, mensaje As String = ""
        cn.abrirconexiontrans()
        cn.ejecutar("dbo.spfinalizarEdicionInformePrograma", False, rpta, mensaje, codigo_ipr, 0, "")
        If rpta > 0 Then
            cn.cerrarconexiontrans()
            Response.Write("<script>alert ('" & mensaje & "');window.opener.location.href=window.opener.location.href;window.close();</script>")
        Else
            cn.cancelarconexiontrans()
            Response.Write("<script>alert ('" & mensaje & "se ha cancelado la operación');</script>")
        End If


    End Sub

    Protected Sub lstinformacion_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles lstinformacion.RowDeleting
        Dim fila As Integer, rpta As Integer, mensaje As String = ""
        fila = e.RowIndex
        cn.abrirconexiontrans()
        cn.ejecutar("dbo.spAnularDetalleInformePrograma", False, rpta, mensaje, Me.lstinformacion.Rows(fila).Cells(1).Text, 0, "")
        cn.cerrarconexiontrans()
        'Response.Write("<script>Se eliminó registro</script>")
        mostrarInformacion()
    End Sub

    Protected Sub lstinformacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstinformacion.SelectedIndexChanged

    End Sub
End Class

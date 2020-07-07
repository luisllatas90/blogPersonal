Imports System.Data

Partial Class frmmonitoreoderendiciones
    Inherits System.Web.UI.Page
    'Dim cn As New clsaccesodatos
    Public dtscliente As New System.Data.DataSet, k As Integer
    Public cn As New clsaccesodatos
    Sub mostrarcliente()

        Dim dts As New DataSet
        cn.abrirconexion()
        dts = cn.consultar("sp_buscacliente", "BT", "PE", "%", "", "")
        cn.cerrarconexion()

        Me.cbocliente.DataSource = dts
        Me.cbocliente.DataTextField = "nombres"
        Me.cbocliente.DataValueField = "codigo_tcl"
        Me.cbocliente.DataBind()
        Me.cbocliente.SelectedValue = 0
    End Sub

    
    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If Me.IsPostBack = False Then
            Call mostrarcliente()
        End If
    End Sub


    Protected Sub cmdexportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexportar.Click

    End Sub

    Protected Sub lstinformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("ondblclick", "MostrarDetalleRendicion('" & e.Row.Cells(0).Text.ToString & "')")
            e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")
            e.Row.Attributes.Add("onmouseout", "resaltar(this,0)")
            'e.Row.Attributes.Add("onclick", "window.open('frmadjuntararchivo.aspx?codigo_dren=1','as','toolbar=no')")
        End If
    End Sub

   
    Protected Sub chktodos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chktodos.CheckedChanged
        'Me.cbocliente.Enabled = Not Me.chktodos.Checked
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            cn.abrirconexion()
            dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
            cn.cerrarconexion()
        End If
    End Sub

    Protected Sub cmdbuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdbuscar.Click
        cn.abrirconexion()
        If Me.chktodos.Checked = False Then
            dtscliente = cn.consultar("dbo.spDocumentosEgresoRendir", "5", Me.cboestado.Text, Me.cbocliente.SelectedValue, "", "", "")
        Else
            dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Me.cboestado.Text, "", "", "", "")
        End If
        cn.cerrarconexion()
    End Sub
End Class

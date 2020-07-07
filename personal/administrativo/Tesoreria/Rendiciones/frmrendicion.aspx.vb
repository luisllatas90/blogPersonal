Imports System.Data

Partial Class _Default
    Inherits System.Web.UI.Page
    Public dtscliente As New System.Data.DataSet, k As Integer
    Public cn As New clsaccesodatos

    Sub mostrarregistros()

    End Sub

    Protected Sub cboestado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboestado.SelectedIndexChanged
        mostrarregistros()
    End Sub

    Protected Sub lstinformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            e.Row.Attributes.Add("seleccionado", 0)
            e.Row.Cells(10).Attributes.Add("onclick", "MostrarDetalleRendicion('" & e.Row.Cells(0).Text.ToString & "')")
            'e.Row.Attributes.Add("onclick", "window.open('frmadjuntararchivo.aspx?codigo_dren=1','as','toolbar=no')")
            'e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")

            e.Row.Attributes.Add("onmouseenter", "resaltar(this,1)")
            e.Row.Attributes.Add("onmouseleave", "resaltar(this,0)")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call mostrar()
        'If Me.IsPostBack = False Then
        ' cn.abrirconexion()
        ' dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
        ' cn.cerrarconexion()
        ' End If

    End Sub

    Sub mostrar()
        Dim codigo_per As String
        codigo_per = Request.QueryString("id")
        Session("codigo_per") = Request.QueryString("id")
        cn.abrirconexion()
        'dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
        dtscliente = cn.consultar("dbo.spDocumentosEgresoRendir", "6", Me.cboestado.Text, codigo_per, "", "", "")
        cn.cerrarconexion()
    End Sub
    Protected Sub cmdbuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdbuscar.Click
        Call mostrar()
    End Sub
End Class

Imports System.Data

Partial Class frmadjuntararchivo
    Inherits System.Web.UI.Page
    Dim cn As New clsaccesodatos
    Public codigo_dren As Integer
    Public Mostrarfinalizar As String

    Sub mostrarinformacion()
        Dim dts As New DataSet
        cn.abrirconexion()
        dts = cn.consultar("sp_verdocumentodetalle", "DD", Me.hfinformacion.Value.ToString, "", "", "")
        cn.cerrarconexion()
        Me.lstinformacion.DataSource = dts
        Me.lstinformacion.DataBind()
        If dts.Tables("consulta").Rows.Count = 0 Then
            Me.lblmensaje.Text = "No se han adjuntado documentos"
        Else
            Me.lblmensaje.Text = ""
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim dtsestadorendicion As New DataSet
        Dim estado As String
        If Me.IsPostBack = False Then


            Me.hfinformacion.Value = Me.Request("codigo_dren")
            Mostrarfinalizar = "none"
            estado = Me.Request("estado")
            If estado = "P" Then
                Mostrarfinalizar = "block"
            End If
            mostrarinformacion()


        End If
    End Sub

    Protected Sub cmdsubir_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dts As New DataSet, extension As String
        Dim codigoarchivo As Integer, mensaje As String = ""
        'If Me.FileUpload1.FileName.Trim = "" Then
        ' Me.lblmensaje.Text = "No se seleccionado archivo"
        ' Exit Sub
        ' End If'



        'cn.abrirconexiontrans()
        'cn.ejecutar("sp_agregararchivo", False, codigoarchivo, mensaje, CInt(Me.hfinformacion.Value), Me.txtdescripcion.Text, 0, "")
        'cn.cerrarconexiontrans()
        'extension = System.IO.Path.GetExtension(Me.FileUpload1.FileName)
        'Me.FileUpload1.SaveAs("c:\Archivosderendicion\A" & codigoarchivo & "." & extension)
        'Response.Write("<script>alert('" & mensaje & "')</script>")
        'Me.lblmensaje.Text = ""
    End Sub

    Protected Sub lstinformacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles lstinformacion.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(7).Text = "<img border='0' src='iconos/" & e.Row.Cells(2).Text.ToString & "' width='12' height='10'>"
            e.Row.Cells(6).Text = "<a TARGET='_blank' href='frmdescargar.aspx?ruta=A" & e.Row.Cells(0).Text.ToString.Trim & e.Row.Cells(1).Text.ToString & "'><img border='0' src='download.gif' width='12' height='10'>"
        End If

    End Sub

    Protected Sub lstinformacion_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles lstinformacion.RowDeleting
        Dim rpta As Integer, mensaje As String = ""
        cn.abrirconexiontrans()
        cn.ejecutar("sp_quitardetallerendicion", False, rpta, mensaje, CInt(Me.lstinformacion.Rows(e.RowIndex).Cells(0).Text), 0, "")
        cn.cerrarconexiontrans()
        mostrarinformacion()
    End Sub

    Protected Sub lstinformacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstinformacion.SelectedIndexChanged

    End Sub

    Protected Sub cmdagregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Response.Write("<script> window.open('frmsubirarchivo.aspx?codigo_dren=" & Me.hfinformacion.Value.ToString & "','frmsubirarchivo','toolbar=no, width=600, height=200') </script>")

    End Sub
End Class

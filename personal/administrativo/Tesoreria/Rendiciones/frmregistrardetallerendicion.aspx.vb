

Imports System.Data
Imports Scripting

Partial Class frmregistrardetallerendicion
    Inherits System.Web.UI.Page
    Dim cn As New clsaccesodatos

    Sub mostrartipodocumento()
        Dim dts As New DataSet
        cn.abrirconexion()
        'dts = cn.consultar("sp_vertipodocumento", "TE", "%")
        dts = cn.consultar("TES_DocumentosWebTesoreria")
        cn.cerrarconexion()

        Me.cbotipodocumento.DataSource = dts
        Me.cbotipodocumento.DataTextField = "descripcion_tdo"
        Me.cbotipodocumento.DataValueField = "codigo_tdo"
        Me.cbotipodocumento.DataBind()
        Me.cbotipodocumento.SelectedValue = 0

    End Sub

    Protected Sub cmdagregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdagregar.Click
        Dim codigorpta_dren As Integer, mensaje As String = ""
        If IsDate(Me.txtfecha.Text) = False Then Me.lblmensaje.Text = "No se ha especificado una fecha Váida" : Exit Sub
        If IsNumeric(Me.txtimporte.Text) = False Then Me.lblmensaje.Text = "El importe debe ser un valor numérico mayor a 0" : Exit Sub
        If CDbl(Me.txtimporte.Text) <= 0 Then Me.lblmensaje.Text = "El importe debe ser un valor numérico mayor a 0" : Exit Sub

        If Me.cbotipodocumento.SelectedValue <> 0 Then
            If Me.txtinstitucion.Text = "" Then Me.lblmensaje.Text = "Proporcione nombre de la Institución" : Exit Sub
        End If
        '    If Me.FileUpload1.FileName.Trim = "" And Me.FileUpload2.FileName.Trim = "" Then
        '        Me.lblmensaje.Text = "Debe ingresar archivo que contiene imagen del Documento"
        '        Exit Sub
        '    End If
        'End If
        'Response.Write()

        cn.abrirconexiontrans()
        cn.ejecutar("sp_agregardetallerendicion", False, codigorpta_dren, mensaje, _
                            CInt(Me.HiddenField1.Value), _
                            Me.txtdescripcion.Text, _
                            Me.txtfecha.Text, _
                            CDbl(Me.txtimporte.Text), _
                            Me.cbotipodocumento.SelectedValue, _
                            Me.txtnumero.Text, _
                            Me.txtinstitucion.Text, _
                            0, "")
        '*********************************************************************************************************************
        '                                       agregar(archivos)
        '*********************************************************************************************************************

        If codigorpta_dren <= 0 Then
            cn.cancelarconexiontrans()
            Response.Write("<script> alert('" & mensaje & "'); window.opener.location.reload(); window.close()</script>")
            Exit Sub
        End If


        Dim codigoarchivo As Integer, extension As String
        Dim fso As New FileSystemObject

        cn.cerrarconexiontrans()
        Response.Write("<script> alert('Información guardada correctamente'); window.opener.location.reload(); window.close()</script>")
    End Sub

    Protected Sub cmdcancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcancelar.Click
        Response.Write("<script>window.close(); </script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            Me.HiddenField1.Value = Me.Request.QueryString("codigo_rend")
        End If
    End Sub

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If Me.IsPostBack = False Then
            mostrartipodocumento()
        End If
    End Sub

    Protected Sub cbotipodocumento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbotipodocumento.SelectedIndexChanged
        If Me.cbotipodocumento.SelectedValue = 0 Then
            Me.txtinstitucion.Enabled = False
            Me.txtnumero.Enabled = False
            Me.txtnumero.BackColor = Drawing.Color.Silver
            Me.txtnumero.Text = ""
            Me.txtinstitucion.Text = ""
            Me.txtinstitucion.BackColor = Drawing.Color.Silver

        Else
            Me.txtinstitucion.Enabled = True
            Me.txtnumero.Enabled = True
            Me.txtnumero.BackColor = Drawing.Color.White
            Me.txtinstitucion.BackColor = Drawing.Color.White
        End If
    End Sub

    
End Class

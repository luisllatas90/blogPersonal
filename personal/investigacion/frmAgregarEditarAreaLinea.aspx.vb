
Partial Class investigacion_frmAgregarEditarComite
    Inherits System.Web.UI.Page

    Dim action As String = "" ' A: Agregar  / E: Editar
    Dim type As String = "" ' A: Area  / L: Linea
    Dim xid As Integer = 0
    Dim name As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        action = Request.QueryString("act")
        type = Request.QueryString("typ")
        xid = CInt(Request.QueryString("xid"))
        If Not IsPostBack Then
            Dim txt As String = ""
            txt = IIf(type = "A", "Área", "Línea")
            Me.lblTitle1.Text = txt
            Me.lblTitle2.Text = txt
            name = Request.QueryString("xna")
            Me.txtName.Text = name
        End If
        Me.txtName.Focus()
    End Sub

    Protected Sub btbGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btbGuardar.Click
        If Me.txtName.Text.Trim <> "" Then
            If action = "A" Then
                Add()
            Else
                Upd()
            End If
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "parent.jQuery.fancybox.close();", True)
            ClientScript.RegisterStartupScript(Me.GetType, "nextpage", "parent.location.reload();", True)
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Debe ingresar el nombre');", True)
        End If
    End Sub

    Function Add() As Integer
        Dim obj As New clsInvestigacion
        Dim exec As Integer
        obj.AbrirTransaccionCnx()
        If (type = "A") Then
            exec = obj.InsertarArea(Me.txtName.Text, xid, CInt(Request.QueryString("id")))
        Else
            exec = obj.InsertarLinea(Me.txtName.Text, xid, CInt(Request.QueryString("id")))
        End If
        obj.CerrarTransaccionCnx()
        obj = Nothing
        Return exec
    End Function

    Sub Upd()
        Dim obj As New clsInvestigacion
        Dim exec As Integer = 0
        obj.AbrirTransaccionCnx()
        If (type = "A") Then
            obj.ActualizarArea(Me.txtName.Text, xid, CInt(Request.QueryString("id")))
        Else
            obj.ActualizarLinea(Me.txtName.Text, xid, CInt(Request.QueryString("id")))
        End If
        obj.CerrarTransaccionCnx()
        obj = Nothing
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "parent.jQuery.fancybox.close();", True)
    End Sub

End Class

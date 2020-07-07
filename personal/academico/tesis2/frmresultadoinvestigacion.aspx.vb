
Partial Class frmresultadoinvestigacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Label1.Text = Date.Now

        If Request.QueryString("instancia") = "D" Then
            Me.Panel1.Visible = True
            Me.Panel2.Visible = False
        End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        GuardarDatos()
        
    End Sub

    Protected Sub cmdSi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSi.Click
        GuardarDatos()
    End Sub

    Private Sub GuardarDatos()
        If Request.QueryString("tipo") = "P" Then
            Session("notafecha") = Date.Now
            Session("notafase") = "PROYECTO"
            Session("tipo") = "ASESOR"
            Session("notaautor") = Session("asesor")
            Session("notaresultado") = Me.dpResultado.Text
            Session("nota") = Me.txtNota.Text
            Session("notaobs") = Me.txtObs.Text
        End If

        If Request.QueryString("tipo") = "E" Then
            Session("notafecha2") = Date.Now
            Session("notafase2") = "EJECUCIÓN"
            Session("tipo2") = "ASESOR"
            Session("notaautor2") = Session("asesor")
            Session("notaresultado2") = Me.dpResultado.Text
            Session("nota2") = Me.txtNota.Text
            Session("notaobs2") = Me.txtObs.Text
        End If

        If Request.QueryString("tipo") = "I" Then
            Session("notafecha3") = Date.Now
            Session("notafase3") = "INFORME"
            Session("tipo3") = "ASESOR"
            Session("notaautor3") = Session("asesor")
            Session("notaresultado3") = Me.dpResultado.Text
            Session("nota3") = Me.txtNota.Text
            Session("notaobs3") = Me.txtObs.Text
        End If

        Response.Write("<script>window.opener.location.reload();window.close()</script>")
    End Sub

    Protected Sub cmdNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNo.Click
        Me.Panel1.Visible = False
        Me.Panel2.Visible = True
    End Sub
End Class

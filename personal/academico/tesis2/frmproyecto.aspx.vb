
Partial Class frmproyecto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim tipo As String
            tipo = Request.QueryString("tipo")

            If tipo = 1 Then
                Me.Panel1.Visible = True
                Me.Panel2.Visible = True
            End If

            Me.txtAutor.Attributes.Add("OnKeyDown", "return false;")
            Me.txtAsesor.Attributes.Add("OnKeyDown", "return false;")
            Me.txtcodigo.Text = Session("codigo")
            Me.lblestado.Text = IIf(Session("estado") = "", "PROCESO", Session("estado"))
            Me.dpFase.Text = Session("fase")
            Me.txtTitulo.Text = Session("titulo")
            Me.txtResumen.Text = Session("resumen")
            Me.txtAutor.Text = Session("autor")
            Me.txtAsesor.Text = Session("asesor")
            Me.txtTematica.Text = Session("tematica")
            Me.txtFechaInicio.Text = Session("fechainicio")
            Me.txtFechaFin.Text = Session("fechafin")
        End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Session("codigo") = Me.txtcodigo.Text
        Session("estado") = Me.lblestado.Text
        Session("fase") = Me.dpFase.Text
        Session("titulo") = Me.txtTitulo.Text
        Session("resumen") = Me.txtResumen.Text
        Session("autor") = Me.txtAutor.Text
        Session("asesor") = Me.txtAsesor.Text
        Session("tematica") = Me.txtTematica.Text
        Session("fechainicio") = Me.txtFechaInicio.Text
        Session("fechafin") = Me.txtFechaFin.Text

        lblmensaje.Text = "Se han guardado correctamente los datos"
        lblmensaje.Visible = True
    End Sub
End Class

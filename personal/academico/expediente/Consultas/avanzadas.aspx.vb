Imports System.Data
Imports System.Data.SqlClient

Partial Class Consultas_avanzadas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim ObjCombos As New Combos
            ObjCombos.LlenaDepartamentoAcademico(Me.DDLDepAcad)
            ObjCombos.LlenaEstadoPlla(DDLEstado)
            ObjCombos = Nothing
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim tabla As DataTable
        Dim objpersonal As New Personal
        Dim idioma As String
        If Me.ChkIdi.Checked = True Then
            idioma = "SI"
        Else
            idioma = ""
        End If
        tabla = objpersonal.DocentesAvanzada2(Me.DDLDepAcad.SelectedValue, Me.TxtTitulo.Text, Me.TxtGrado.Text, Me.TxtOtros.Text, idioma, Me.DDLEstado.SelectedValue) 'Se añadió DDLEstado(estado de Planilla)
        Me.LblCantidad.Text = tabla.Rows.Count.ToString
        Me.DataList1.DataSource = tabla
        Me.DataList1.DataBind()
        objpersonal = Nothing
    End Sub
End Class

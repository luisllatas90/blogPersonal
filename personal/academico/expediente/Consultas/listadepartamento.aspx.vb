Imports System.Data
Partial Class Consultas_listadepartamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Tabla As DataTable
        If IsPostBack = False Then
            Dim objCombos As New Combos
            objCombos.LlenaCentroCostos(Me.DDLDepAcad, "", "HV")
            objCombos.LlenaTipoPersonal(ddlTipoPersonal)
            objCombos.LlenaEstadoPlla(ddlEstadoPlla)
            objCombos = Nothing
        End If

        Dim ObjPersonal As New Personal

        'Response.Write("Acad: " & DDLDepAcad.SelectedValue)
        'Response.Write("<br />")
        'Response.Write("tpe: " & ddlTipoPersonal.SelectedValue)
        'Response.Write("<br />")
        'Response.Write("estado: " & ddlEstadoPlla.SelectedValue)

        Tabla = ObjPersonal.DocentesDeparAcad(Me.DDLDepAcad.SelectedValue, Me.ddlTipoPersonal.SelectedValue, Me.ddlEstadoPlla.SelectedValue)
        Me.LblCantidad.Text = Tabla.Rows.Count.ToString
        ObjPersonal = Nothing
    End Sub
End Class

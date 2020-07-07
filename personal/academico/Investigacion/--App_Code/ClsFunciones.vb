Imports Microsoft.VisualBasic

Public Class ClsFunciones
    Public Shared Sub LlenarListas(ByVal Combo As System.Object, ByVal Datos As System.Data.DataTable, _
    ByVal ColValor As System.Object, ByVal ColDescripcion As System.Object)
        Combo.Items.Clear()
        Combo.DataSource = Datos
        Combo.DataValueField = Datos.Columns(ColValor).ToString
        Combo.DataTextField = Datos.Columns(ColDescripcion).ToString
        Combo.DataBind()
    End Sub

    Public Shared Sub LlenarListas(ByVal Combo As System.Object, ByVal Datos As System.Data.DataTable, _
    ByVal ColValor As System.Object, ByVal ColDescripcion As System.Object, ByVal Mensaje As String)
        'El valor predeterminado cuando no existe seleccion es (-1)
        Combo.Items.Clear()
        Combo.Items.Add(Mensaje)
        Combo.Items(0).Value = 0
        For i As Integer = 1 To Datos.Rows.Count
            Combo.Items.Add(Datos.Rows(i - 1).Item(ColDescripcion).ToString)
            Combo.Items(i).Value = Datos.Rows(i - 1).Item(ColValor).ToString
        Next

    End Sub
End Class


Partial Class academico_frmasignarprofesor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("codigo_cur") <> "" Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim RS As Data.DataTable
            RS = obj.TraerDataTable("ConsultarCargaAcademicaDpto", 2, Request.QueryString("codigo_cur"), Request.QueryString("codigo_cac"), 0)
            Me.Label1.Text = RS.Rows(0).Item("identificador_cur").ToString
            Me.Label2.Text = RS.Rows(0).Item("nombre_cur").ToString

            Dim nombre_cpf, nEscuela As String
            nombre_cpf = ""

            For i As Int16 = 0 To RS.Rows.Count - 1
                Dim Fila As TableRow
                Dim Celda0 As New TableCell
                Dim Celda1 As New TableCell
                Dim Celda2 As New TableCell
                Dim Celda3 As New TableCell
                Dim Celda4 As New TableCell
                Dim Celda5 As New TableCell

                nEscuela = "&nbsp;"
                If nombre_cpf <> RS.Rows(i).Item("nombre_cpf") Then
                    nombre_cpf = RS.Rows(i).Item("nombre_cpf")
                    nEscuela = nombre_cpf
                End If

                Fila = New TableRow
                Celda0.Text = RS.Rows(i).Item("grupohor_cup").ToString
                Celda1.Text = nEscuela
                Celda2.Text = RS.Rows(i).Item("descripcion_pes").ToString
                Celda3.Text = RS.Rows(i).Item("profesor").ToString
                Celda4.Text = RS.Rows(i).Item("estado").ToString

                Dim cmd As New Button
                cmd.Text = "Modificar"

                If RS.Rows(i).Item("profesor").ToString.ToString = "-No definido-" Then
                    cmd.Text = "Agregar"
                End If
                Celda5.Controls.Add(cmd)
                Fila.Cells.Add(Celda0)
                Fila.Cells.Add(Celda1)
                Fila.Cells.Add(Celda2)
                Fila.Cells.Add(Celda3)
                Fila.Cells.Add(Celda4)
                Fila.Cells.Add(Celda5)
                Me.tblAsignacion.Rows.Add(Fila)
            Next
            RS.Dispose()
        End If
    End Sub
End Class

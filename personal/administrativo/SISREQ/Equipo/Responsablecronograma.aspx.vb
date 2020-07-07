
Partial Class Equipo_AgregarResponsableASolicitud
    Inherits System.Web.UI.Page
    Private asignar As Int16
    Public cod_per As Int32
    Public cod_sol As Int16

    'Para determinar el campo de busqueda y el valor que debe mostrar
    Protected Sub CboCampo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboCampo.SelectedIndexChanged
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        With Me.CboCampo
            If .SelectedValue = 0 Or .SelectedValue = -1 Then
                Me.CboValor.Enabled = False
                Me.CboValor.SelectedValue = -1
                Me.CboValor.Items.Clear()
            Else
                Select Case .SelectedValue
                    Case 1
                        ClsFunciones.LlenarListas(Me.CboValor, ObjCnx.TraerDataTable("paReq_consultaraplicacion"), "codigo_apl", "descripcion_apl", "--Seleccione Módulo--")
                    Case 2
                        Dim Prioridad() As String = {"-- Seleccione Prioridad --", "Muy Baja", "Baja", "Media", "Alta", "Muy Alta"}
                        Me.CboValor.DataSource = Prioridad
                        Me.CboValor.DataBind()
                        Me.CboValor.Items(0).Value = -1
                        For i As Int16 = 1 To 5
                            Me.CboValor.Items(i).Value = i
                        Next
                    Case 3
                        ClsFunciones.LlenarListas(Me.CboValor, ObjCnx.TraerDataTable("paReq_ConsultarCentroCosto"), "codigo_cco", "descripcion_cco", "--Seleccione Área--")
                End Select
                Me.CboValor.Enabled = True
                Me.CboValor.SelectedValue = -1
            End If
        End With
    End Sub

    Protected Sub GvSinAsignar_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvSinAsignar.DataBound
        Me.lblTotal.Text = "Total de registros: " & Me.GvSinAsignar.Rows.Count
    End Sub


    Protected Sub GvSinAsignar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvSinAsignar.RowDataBound
        asignar = Me.CboSolicitudPor.SelectedValue
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim enlace As String
            fila = e.Row.DataItem
            e.Row.Cells(1).Attributes.Add("tooltip", "<table><tr><td width='300'><b>" & fila.Row("descripcion_tsol").ToString & "</b><br>" & fila.Row("descripcion_sol").ToString & "</td></tr></table>")
            e.Row.Attributes.Add("OnMouseOver", "pintarcelda(this)")
            e.Row.Attributes.Add("OnMouseOut", "despintarcelda(this)")
            enlace = ""
            If asignar = 0 Then ' Muestra las solicitudes sin responsable de cronograma
                enlace &= "'SeleccionarResponsable.aspx"
                enlace &= "?field=" & fila.Row("id_sol").ToString & "&id=" & cod_per.ToString & "&asignar=" & asignar.ToString & "'"
                e.Row.Cells(15).Text = "<div style='cursor:hand' onClick=AbrirPopUp(" & enlace & ",300,480)><img border=0 src='../images/agregar.gif'></div>"
            Else ' Muestra las solicitudes con responsable de cronograma
                enlace &= "'SeleccionarResponsable.aspx"
                enlace &= "?field=" & fila.Row("id_sol").ToString & "&id=" & cod_per.ToString & "&asignar=" & asignar.ToString & "'"
                e.Row.Cells(15).Text = "<div style='cursor:hand' onClick=AbrirPopUp(" & enlace & ",300,480)><img border=0 src='../images/editar.gif'></div>"
                'e.Row.Cells(15).Text = "<div style='cursor:hand' onClick=showModalDialog(" & enlace & ")><img border=0 src='../images/editar.gif'></div>"
            End If
        End If
    End Sub

    Protected Sub frmResponsableCronograma_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmResponsableCronograma.Load
        asignar = 0
        cod_per = Request.QueryString("id")
        cod_sol = Request.QueryString("field")
    End Sub
End Class



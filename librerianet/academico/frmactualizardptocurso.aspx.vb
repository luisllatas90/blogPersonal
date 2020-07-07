
Partial Class frmactualizardptocurso
    Inherits System.Web.UI.Page
    Public tbldpto As Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        me.cmdGuardar.visible=false
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        'Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("BDUSATConnectionString").ConnectionString)
        'Dim Tbl As New Data.DataTable

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            Dim Combo As New DropDownList
            Combo.ID = "codigo_dac"
            ClsFunciones.LlenarListas(Combo, tbldpto, "codigo_dac", "nombre_dac")
            Combo.SelectedValue = e.Row.Cells(3).Text
            e.Row.Cells(3).Controls.Add(Combo)
            'onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')"
            e.Row.Attributes.Add("onMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("onMouseOut", "Resaltar(0,this,'S')")
        End If
    End Sub
    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim valor As String
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        If Me.GridView1.Rows.Count > 0 Then
            For j As Integer = 0 To Me.GridView1.Rows.Count
                For i As Integer = 0 To GridView1.Controls(0).Controls(j).Controls(3).Controls.Count - 1
                    obj.IniciarTransaccion()
                    Try
                        Dim controlCombo As DropDownList
                        Dim celda As TableCell
                        celda = Me.GridView1.Controls(0).Controls(j).Controls(0)
                        'Response.Write(celda.Text)
                        controlCombo = GridView1.Controls(0).Controls(j).Controls(3).Controls(i)
                        valor = controlCombo.ToolTip & "<br>"

                        ' El numero de la solicitud es celda.text
                        ' El valor del combo a grabar es controlcombo.selectedvalue 

                        obj.Ejecutar("ActualizarDptoCurso", celda.Text, controlCombo.SelectedValue)

                        ' para actualizar bitacora - codigo_per
                        'Response.Write(controlCombo.SelectedValue & "--" & celda.Text & "<br>")
                        obj.TerminarTransaccion()
                    Catch ex As Exception
                        obj.AbortarTransaccion()
                        Response.Write("<SCRIPT>alert('Ocurrio un error al procesar los datos')</SCRIPT>")
                        'Response.Write(ex.Message)
                    End Try
                Next
            Next
            Response.Write("<SCRIPT>alert('Se actualizaron los datos correctamente'); </SCRIPT>")
        Else
            Response.Write("<script>alert('No hay registros para guardar')</script>")
        End If
        obj = Nothing
    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        BuscarCursos(Me.txtTermino.Text)
    End Sub
    Private Sub BuscarCursos(ByVal termino As String)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Tbl As New Data.DataTable


        Tbl = obj.TraerDataTable("ConsultarCargaAcademicaDpto", 6, termino, 0, 0)
        tbldpto = obj.TraerDataTable("ConsultarDepartamentoAcademico", "TO", 0)

	if tbl.rows.count>0 then 
		me.cmdGuardar.visible=true
	end if

        Me.GridView1.DataSource = Tbl
        Me.GridView1.DataBind()
        obj = Nothing

    End Sub
End Class

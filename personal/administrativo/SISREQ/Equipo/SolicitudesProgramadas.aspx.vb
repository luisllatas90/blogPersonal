
Partial Class Equipo_SolicitudesProgramadas
    Inherits System.Web.UI.Page

    Protected Sub CboCampo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboCampo.SelectedIndexChanged
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        With Me.CboCampo
            Select Case .SelectedValue
                Case 0
                    Me.CboValor.Visible = False
                Case 1
                    ClsFunciones.LlenarListas(Me.CboValor, ObjCnx.TraerDataTable("paReq_consultaraplicacion"), "codigo_apl", "descripcion_apl", "--Seleccione Módulo--")
                    Me.CboValor.Visible = True
                Case 2
                    Dim Prioridad() As String = {"--Seleccione Prioridad--", "Muy Baja", "Baja", "Media", "Alta", "Muy Alta"}
                    Me.CboValor.DataSource = Prioridad
                    Me.CboValor.DataBind()
                    Me.CboValor.Items(0).Value = -1
                    For i As Int16 = 1 To 5
                        Me.CboValor.Items(i).Value = i
                    Next
                    Me.CboValor.Visible = True
                Case 3
                    ClsFunciones.LlenarListas(Me.CboValor, ObjCnx.TraerDataTable("paReq_ConsultarCentroCosto"), "codigo_cco", "descripcion_cco", "--Seleccione Área--")
                    Me.CboValor.Visible = True
            End Select
        End With
    End Sub
End Class

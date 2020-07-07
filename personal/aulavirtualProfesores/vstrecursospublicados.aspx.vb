
Partial Class vstrecursospublicados
    Inherits System.Web.UI.Page
    Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.hdIdCursoVirtual.Value = Request.QueryString("idcursovirtual")

        If Request.QueryString("idcursovirtual") <> "" Then
            If IsPostBack = False Then
                Dim tblCurso As Data.DataTable
                tblCurso = Obj.TraerDataTable("ConsultarCursoVirtual", 3, Me.hdIdCursoVirtual.Value, 0, 0)

                Me.lbltitulo.Text = tblCurso.Rows(0).Item("titulocursovirtual")
                Me.hdCodigo_apl.Value = tblCurso.Rows(0).Item("codigo_apl")
                Me.Title = tblCurso.Rows(0).Item("titulocursovirtual")
                tblCurso = Nothing
                If Int(Me.hdCodigo_apl.Value) > 4 Then
                    Me.hpSP.Value = "DI_RecursosCursoVirtual_vN"
                End If

                Me.trw.Nodes.Clear()
                CargarDocumentos(Me.hpSP.Value, Me.hdIdCursoVirtual.Value, Nothing, 0)
                Obj = Nothing
                'Response.Write("postback")
            End If
        End If
    End Sub
    Private Sub CargarDocumentos(ByVal sp As String, ByVal idcursovirtual As Integer, ByVal Nodo As TreeNode, ByVal refcodigo_ccv As Integer)
        Dim Tabla As Data.DataTable
        Tabla = Obj.TraerDataTable(sp, idcursovirtual, refcodigo_ccv)
        For i As Int32 = 0 To Tabla.Rows.Count - 1
            Dim Nodo_X As New TreeNode
            If Tabla.Rows(i).Item("codigo_tre") = "A" Then
                Nodo_X.PopulateOnDemand = False
                Nodo_X.Target = "_blank"

                If Tabla.Rows(i).Item("nombrearchivo") <> "" Then
                    Nodo_X.NavigateUrl = "http://www.usat.edu.pe/campusvirtual/archivoscv/" & Tabla.Rows(i).Item("nombrearchivo")
                End If
            End If
            Nodo_X.ExpandAll()
            Nodo_X.ImageUrl = Tabla.Rows(i).Item("icono_tre")
            Nodo_X.Text = "&nbsp;" & Tabla.Rows(i).Item("titulo_ccv")
            Nodo_X.Value = Tabla.Rows(i).Item("codigo_ccv")

            If IsNothing(Nodo) Then
                Me.trw.Nodes.Add(Nodo_X)
            Else
                Nodo.ChildNodes.Add(Nodo_X)
            End If

            If Tabla.Rows(i).Item("total_ccv") > 0 Then
                CargarDocumentos(sp, idcursovirtual, Nodo_X, Tabla.Rows(i).Item("codigo_ccv"))
            End If
            Nodo_X = Nothing
        Next
        Tabla.Dispose()
        Tabla = Nothing
    End Sub
    Private Sub CargarTareas(ByVal sp As String)
        Dim tblTareas As Data.DataTable

        'Cargar PADRES
        tblTareas = Obj.TraerDataTable(sp, 0, hdIdCursoVirtual.Value, 0)

        For i As Int32 = 0 To tblTareas.Rows.Count - 1
            Dim Nodo_X As New TreeNode

            Nodo_X.ExpandAll()
            Nodo_X.ImageUrl = tblTareas.Rows(i).Item("icono_tre")
            Nodo_X.Text = "&nbsp;" & tblTareas.Rows(i).Item("titulo_ccv")
            Nodo_X.Value = tblTareas.Rows(i).Item("codigo_ccv")
            Me.trw.Nodes.Add(Nodo_X)

            'Cargar HIJOS
            If tblTareas.Rows(i).Item("total_ccv") > 0 Then
                Dim tblTareaUsuario As Data.DataTable
                tblTareaUsuario = Obj.TraerDataTable(sp, 1, hdIdCursoVirtual.Value, tblTareas.Rows(i).Item("codigo_ccv"))

                For j As Int32 = 0 To tblTareaUsuario.Rows.Count - 1
                    Dim Nodo_Y As New TreeNode

                    Nodo_Y.PopulateOnDemand = False
                    Nodo_Y.Target = "_blank"
                    Nodo_Y.ImageUrl = tblTareaUsuario.Rows(j).Item("icono_tre")
                    Nodo_Y.Text = "&nbsp;" & tblTareaUsuario.Rows(j).Item("titulo_ccv")
                    Nodo_Y.Value = tblTareaUsuario.Rows(j).Item("codigo_ccv")

                    If tblTareaUsuario.Rows(j).Item("nombrearchivo") <> "" Then
                        Nodo_Y.NavigateUrl = "http://www.usat.edu.pe/campusvirtual/archivoscv/" & tblTareaUsuario.Rows(j).Item("nombrearchivo")
                    End If

                    Nodo_X.ChildNodes.Add(Nodo_Y)
                Next
                tblTareaUsuario.Dispose()
                tblTareaUsuario = Nothing
            End If
            Nodo_X = Nothing
        Next
        tblTareas.Dispose()
        tblTareas = Nothing
    End Sub
    Protected Sub imgBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBuscar.Click
        Me.trw.Nodes.Clear()
        Me.imgExportar.Visible = False
        Select Case Me.cboTipoRecurso.SelectedValue
            Case "D"
                Me.CargarDocumentos(Me.hpSP.Value, hdIdCursoVirtual.Value, Nothing, 0)
                Me.imgExportar.Visible = True
            Case "T"
                Me.CargarTareas("DI_TareasCursoVirtual_vA")
        End Select
        Obj = Nothing
    End Sub
    Protected Sub imgExportar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgExportar.Click
        Response.Redirect("vstdocumentosexportados.aspx?id=" & hdIdCursoVirtual.Value)
    End Sub
End Class

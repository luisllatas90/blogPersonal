
Partial Class admintareas
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("vez") = 1 Then
            Session("idusuario2") = Replace(Request.QueryString("idusuario"), "***", "\")
            Session("idtarea2") = Request.QueryString("idtarea")
            Session("idcursovirtual2") = Request.QueryString("idcursovirtual")
            Session("idvisita2") = Request.QueryString("idvisita")
            Session("codigo_tfu2") = Request.QueryString("codigo_tfu")
            Session("titulotarea") = Request.QueryString("titulotarea")
            Session("idestadorecurso") = Request.QueryString("idestadorecurso")
        End If

        If Session("codigo_tfu2") > 1 Then
            Response.Redirect("trabajosenviados.aspx?idestadorecurso=" + Session("idestadorecurso"))
            Exit Sub
        End If

        Me.cmdEnviar.OnClientClick = "AbrirPopUp('frmenviararchivo.aspx?accion=agregartareausuario&refidtareausuario=0','300','500');return(false)"
        Me.lblTitulo.Text = Session("titulotarea")

        Me.trw.Nodes.Clear()
        Dim Tabla As Data.DataTable
        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
        Tabla = ObjDatos.TraerDataTable("DI_ConsultarAdminTareas", 1, Session("idusuario2"), Session("codigo_tfu2"), Session("idtarea2"), 0, Me.dtTipo.SelectedValue)


        If Tabla.Rows.Count = 0 Then
            Me.lblMensaje.Visible = True
        End If

        Dim nusuario, estado As String
        Dim j As Integer
        nusuario = ""
        j = 0
        For i As Int16 = 0 To Tabla.Rows.Count - 1
            Dim Nodo As TreeNode
            If nusuario <> Tabla.Rows(i).Item("nombreusuario") Then
                Nodo = New TreeNode
                nusuario = Tabla.Rows(i).Item("nombreusuario")
                j = j + 1
                Nodo.SelectAction = TreeNodeSelectAction.Expand
                Nodo.Expanded = IIf(Me.dtTipo.SelectedValue = "P", True, False)
                'Nodo.ImageUrl = Right(Tabla1.Rows(i).Item("archivo"), 3)
                Nodo.Text = "&nbsp;" & j & ". " & Tabla.Rows(i).Item("nombreusuario")
                Nodo.Value = Tabla.Rows(i).Item("idusuario")
                Me.trw.Nodes.Add(Nodo)
            End If

            '*********************************
            'Mostrar archivos del estudiante
            '*********************************
            Dim Nodo_X As New TreeNode
            Nodo_X.SelectAction = TreeNodeSelectAction.None
            Nodo_X.PopulateOnDemand = False
            'Nodo_X.ShowCheckBox = True
            Nodo_X.ImageUrl = "../../images/ext/" & Right(Tabla.Rows(i).Item("archivo"), 3) & ".gif"
            estado = "<img src=""../../images/ecerrado.gif"" alt=""No Leido por el profesor"">&nbsp;"

            If IsDBNull(Tabla.Rows(i).Item("fechaultimarevision")) = False Then
                estado = "<img src=""../../images/eabierto.gif"" alt=""Leido por el profesor"">&nbsp;"
            End If

            If (Tabla.Rows(i).Item("estadotarea") = "E") Then
                estado = estado & "<img src=""../../images/p1.gif"" alt=""Archivo enviado por el participante"">&nbsp;"
            Else
                estado = estado & "<img src=""../../images/usuarios.jpg"" alt=""Archivo enviado por el profesor"">&nbsp;"
            End If

            Nodo_X.Text = "&nbsp;" & estado & Tabla.Rows(i).Item("fechareg") & CrearAcciones(Tabla.Rows(i).Item("idtareausuario"))
            Nodo_X.Value = Tabla.Rows(i).Item("idtareausuario")
            Nodo.ChildNodes.Add(Nodo_X)
            Nodo_X = Nothing
        Next
        Tabla.Dispose()
        Tabla = Nothing
        ObjDatos = Nothing

    End Sub
    Private Function CrearAcciones(ByVal idtareausuario As Integer) As String
        CrearAcciones = "&nbsp;&nbsp;&nbsp;"
        CrearAcciones += "&nbsp;&nbsp;[&nbsp;<a target=""_blank"" href=""descargararchivo.aspx?accion=D&idtareausuario=" & idtareausuario & """><img src='../../images/download.gif' border=0>&nbsp;Descargar</a>"
        CrearAcciones += "&nbsp;|&nbsp;&nbsp;<a href=""JavaScript:AbrirAcciones(2,'frmenviararchivo.aspx?accion=agregartareausuario&refidtareausuario=0&idtareausuario=" & idtareausuario & "')""><img src='../../images/anadir.gif' border=0>&nbsp;Añadir</a>"
        CrearAcciones += "&nbsp;|&nbsp;&nbsp;<a href=""JavaScript:AbrirAcciones(4,'descargararchivo.aspx?accion=E&idtareausuario=" & idtareausuario & "')""><img src='../../images/eliminar.gif' border=0>&nbsp;Eliminar</a>"
        CrearAcciones += "&nbsp;|&nbsp;&nbsp;<a href=""JavaScript:AbrirAcciones(2,'frmenviararchivo.aspx?accion=agregarcomentario&idtareausuario=" & idtareausuario & "')""><img src='../../images/editar.gif' border=0>&nbsp;Comentar</a>"
        CrearAcciones += "&nbsp;|&nbsp;&nbsp;<a href=""JavaScript:AbrirAcciones(3,'detalletareausuario.aspx?idtareausuario=" & idtareausuario & "')""><img src='../../images/propiedades.gif' border=0>&nbsp;Ver detalle</a>&nbsp;]"
    End Function
    'Protected Sub cmdRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegresar.Click
    '    Session.RemoveAll()
    '    Response.Redirect("../../personal/aulavirtual/tareas/index.asp")
    'End Sub
End Class

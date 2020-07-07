﻿
Partial Class administrativo_propuestas2_proponente_ListaConfiguracionUsuariosEliminan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id") = "" Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If (IsPostBack = False) Then
            Call cargarPersonal()
            Call wf_Consultar()
        End If
    End Sub

  
    Sub cargarPersonal()
        Dim dtt As New Data.DataTable
        Dim obj As New ClsConectarDatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtt = obj.TraerDataTable("PRP_ListaPersonalConsejo_POA", 0, "A")
        obj.CerrarConexion()

        Me.ddl_Personal.DataSource = dtt
        Me.ddl_Personal.DataTextField = "responsable_per"
        Me.ddl_Personal.DataValueField = "codigo_Per"
        Me.ddl_Personal.DataBind()
        dtt.Dispose()
        obj = Nothing
    End Sub

    Function wf_validate() As Boolean
        Dim mensaje As String = ""
        If ddl_Personal.SelectedValue = 0 Then
            mensaje = "<h3 style=" & "color:blue;" & ">Seleccione una Persona</h3>"
            Response.Write(mensaje)
            Return False
        End If

        If ddl_Eliminar.SelectedValue = 0 Then
            mensaje = "<h3 style=" & "color:blue;" & ">Seleccione permiso a Eliminar</h3>"
            Response.Write(mensaje)
            Return False
        End If

        If ddl_mail.SelectedValue = 0 Then
            mensaje = "<h3 style=" & "color:blue;" & ">Seleccione si reciben E-Mail</h3>"
            Response.Write(mensaje)
            Return False
        End If


        Dim dtt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim codigo_per As String = ddl_Personal.SelectedValue
        
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtt = obj.TraerDataTable("PRP_RegistrarEliminaPropuestas", 0, codigo_per, "%", "%", "%", "%", 0, "V")
        obj.CerrarConexion()

        If dtt.Rows.Count > 0 Then
            If dtt.Rows(0).Item(0) <> 0 Then
                mensaje = "<h3 style=" & "color:blue;" & ">" & ddl_Personal.SelectedItem.ToString & " ya se encuentra registrado</h3>"
                Response.Write(mensaje)
                Return False
            End If
        End If

        Return True
    End Function

    Protected Sub cmdRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegistrar.Click
        If wf_validate() = False Then
            Return
        End If

        Try
            Dim codigo_elp As Integer = 0
            Dim codigo_per As Integer = ddl_Personal.SelectedValue
            Dim elimina_elp As Integer = ddl_Eliminar.SelectedValue
            Dim enviaMail As Integer = ddl_mail.SelectedValue
            Dim usuario As Integer = Request.QueryString("id")
            Dim TodosMails As Integer = IIf(chk_TodosMails.Checked = True, 1, 0)

            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ObjCnx.Ejecutar("PRP_RegistrarEliminaPropuestas", codigo_elp, codigo_per, elimina_elp, enviaMail, usuario, "", TodosMails, "I")

            ddl_Personal.SelectedIndex = -1
            ddl_Eliminar.SelectedIndex = -1
            ddl_mail.SelectedIndex = -1

            Response.Write("<script>alert('Se registro correctamente')</script>")
            Call wf_Consultar()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub wf_Consultar()
        Try
            Dim dtConsultar As New Data.DataTable
            Dim obj As New ClsConectarDatos

            Dim codigo_elp As String = "0"
            Dim codigo_per As String = IIf(ddl_Personal.SelectedValue = 0, "%", ddl_Personal.SelectedValue)
            Dim elimina_elp As String = IIf(ddl_Eliminar.SelectedValue = 0, "%", ddl_Eliminar.SelectedValue)
            Dim enviaMail As String = IIf(ddl_mail.SelectedValue = 0, "%", ddl_mail.SelectedValue)
            Dim estado As String = ddlEstado.SelectedValue
            Dim usuario As String = Request.QueryString("id")

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtConsultar = obj.TraerDataTable("PRP_RegistrarEliminaPropuestas", codigo_elp, codigo_per, elimina_elp, enviaMail, usuario, estado, 0, "C")
            obj.CerrarConexion()

            dgvPropuestas.DataSource = dtConsultar
            dgvPropuestas.DataBind()
            dgvPropuestas.Dispose()
            obj = Nothing

            lbl_numeroItems.Text = dgvPropuestas.Rows.Count.ToString & " REGISTROS ENCONTRADOS"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    
    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        Call wf_Consultar()
    End Sub

    Protected Sub dgvPropuestas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvPropuestas.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem
                e.Row.Attributes.Add("id", "" & fila.Row("codigo_elp").ToString & "")
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "HabilitarBoton('M',this)")
                e.Row.Attributes.Add("Class", "Sel")
                e.Row.Attributes.Add("Typ", "Sel")
            End If

            If Me.ddlEstado.Text <> "1" Then
                e.Row.Cells(4).Text = ""
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgvPropuestas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgvPropuestas.RowDeleting
        Try
            Dim codigo_elp As Integer = txtelegido.Value
            Dim codigo_per As Integer = 0
            Dim elimina_elp As Integer = 0
            Dim enviaMail As Integer = 0
            Dim usuario As Integer = Request.QueryString("id")

            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            'Response.Write("PRP_RegistrarEliminaPropuestas '" & codigo_elp & "','" & codigo_per & "','" & elimina_elp & "','" & enviaMail & "','" & usuario & "','" & "" & "',0,'" & "U" & "'")
            ObjCnx.Ejecutar("PRP_RegistrarEliminaPropuestas", codigo_elp, codigo_per, elimina_elp, enviaMail, usuario, "", 0, "U")

            Call wf_Consultar()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgvPropuestas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvPropuestas.SelectedIndexChanged

    End Sub
End Class

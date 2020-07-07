
Partial Class frmCambiarEstadoAlumnoPlanEstudio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim tbl As Data.DataTable
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")
            Dim codigo_test As Integer = Request.QueryString("mod")
            '=================================
            'Permisos por Escuela
            '=================================
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tbl = obj.TraerDataTable("EVE_ConsultarCarreraProfesional", codigo_test, codigo_tfu, codigo_usu)

            '=================================
            'Llenar combos
            '=================================
            ClsFunciones.LlenarListas(Me.dpCodigo_cpf, tbl, "codigo_cpf", "nombre_cpf", "--Seleccione la Escuela Profesional--")
            tbl.Dispose()
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Me.cmdguardar.visible = False
        Me.lblmensaje.visible = False
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Me.grwPosiblesEgresados.datasource = obj.TraerDataTable("ConsultarEgresadoPlanEstudio", Me.dptipo.selectedvalue, Me.dpcodigo_cpf.selectedvalue)
        Me.grwPosiblesEgresados.databind()
        obj.CerrarConexion()
        obj = Nothing

        If grwPosiblesEgresados.rows.count > 0 And Me.dptipo.selectedvalue = 0 Then
            cmdguardar.visible = True
            Me.lblmensaje.visible = True
        End If
    End Sub
    Protected Sub grwPosiblesEgresados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwPosiblesEgresados.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(1).Text = e.Row.RowIndex + 1
            e.Row.Cells(5).Text = IIf(fila.Row("estadoactual_alu") = 0, "Inactivo", "Activo")
            If dptipo.selectedvalue = 1 Then
                e.Row.Cells(0).Text = ""
            ElseIf fila.row("DebeTesis") > 0 Then
                e.Row.Cells(0).Text = ""
            Else
                CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")
            End If
        End If
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim Fila As GridViewRow
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim codigo_usu As Integer = Request.QueryString("id")
        
        Me.cmdGuardar.Enabled = False
        Try
            obj.AbrirConexion()
            '==================================
            'Desactivar los planes de estudio
            '==================================
            For I As Int16 = 0 To Me.grwPosiblesEgresados.Rows.Count - 1
                Fila = Me.grwPosiblesEgresados.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        obj.Ejecutar("FinalizarAlumnoPlanEstudio", Me.grwPosiblesEgresados.DataKeys.Item(Fila.RowIndex).Values("codigo_alu"), codigo_usu)
                    End If
                End If
            Next

            Me.grwPosiblesEgresados.datasource = obj.TraerDataTable("ConsultarEgresadoPlanEstudio", Me.dptipo.selectedvalue, Me.dpcodigo_cpf.selectedvalue)
            Me.grwPosiblesEgresados.databind()
            obj.CerrarConexion()
            Me.cmdGuardar.Enabled = True
            Page.RegisterStartupScript("ok", "alert('Se han actualizado los datos correctamente');")
        Catch ex As Exception
            obj = Nothing
            Me.cmdGuardar.Enabled = True
            Page.RegisterStartupScript("error", "alert('Ocurrió un Error al actualizar los datos \n Contáctese con desarrollosistemas@usat.edu.pe');")
        End Try
    End Sub
End Class

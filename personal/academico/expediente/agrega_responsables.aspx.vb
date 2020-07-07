Imports System.Data
Partial Class Investigador_agrega_responsables
    Inherits System.Web.UI.Page

    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLTipoPersona.SelectedIndexChanged
        Me.LstPersonal.Items.Clear()
        Me.LstPersonal.AutoPostBack = False
        Me.TxtBuscar.Text = ""
        Me.LblMensaje.Text = ""
        If Me.DDLTipoPersona.SelectedValue = 1 Then
            Me.PanPersonal.Visible = True
            Me.PanExterno.Visible = False
            Me.DDLLinea.Visible = True
            Me.LblLinea.Visible = True
        ElseIf Me.DDLTipoPersona.SelectedValue = 2 Then
            Me.PanPersonal.Visible = True
            Me.PanExterno.Visible = False
            Me.DDLLinea.Visible = False
            Me.LblLinea.Visible = False
        Else
            Me.PanPersonal.Visible = False
            Me.PanExterno.Visible = True
            Me.DDLLinea.Visible = False
            Me.LblLinea.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.TxtBuscar.Attributes.Add("OnKeyPress", "if(event.keyCode==13){document.form1.CmdBuscar.focus(); return false;}")
        If IsPostBack = False Then
            Me.HidenCodigoInv.Value = Request.QueryString("codigo_Inv")
            Dim ObjLinea As New combos
            ObjLinea.LlenaLineaPersonal(Me.DDLLinea, "")
            ObjLinea.ConsultarTipoParticipacionInvestigacion(Me.DDLTipoParticipacion)
            ObjLinea = Nothing
            Me.PanPersonal.Visible = True
            Me.PanExterno.Visible = False
        End If
        Me.GridView1.DataBind()
    End Sub

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim Tabla As DataTable
        Me.GridView1.DataBind()
        Me.LblMensaje.Text = ""
        If Me.DDLTipoPersona.SelectedValue = 1 Then
            Dim ObjPersonal As New Personal
            Dim ObjLineas As New combos
            Tabla = ObjPersonal.ConsultarUnidadesInvestigacion("9", Me.TxtBuscar.Text)
            Me.LstPersonal.Items.Clear()
            Me.LstPersonal.DataSource = Tabla
            Me.LstPersonal.DataTextField = Tabla.Columns(1).ToString
            Me.LstPersonal.DataValueField = Tabla.Columns(0).ToString
            Me.LstPersonal.DataBind()
            Me.LstPersonal.AutoPostBack = True
            ObjLineas.LlenaLineaPersonal(Me.DDLLinea, "")
            ObjPersonal = Nothing
            ObjLineas = Nothing

        ElseIf Me.DDLTipoPersona.SelectedValue = 2 Then
            Dim ObjPersonal As New Personal
            Dim ObjLineas As New combos
            Tabla = ObjPersonal.ConsultarInvestigaciones("7", Me.TxtBuscar.Text)
            Me.LstPersonal.Items.Clear()
            Me.LstPersonal.DataSource = Tabla
            Me.LstPersonal.DataTextField = Tabla.Columns(1).ToString
            Me.LstPersonal.DataValueField = Tabla.Columns(0).ToString
            Me.LstPersonal.DataBind()
            Me.LstPersonal.AutoPostBack = False
            ObjLineas.LlenaLineaPersonal(Me.DDLLinea, "")
            ObjLineas = Nothing
            ObjPersonal = Nothing
        End If
    End Sub

    Protected Sub LstPersonal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LstPersonal.SelectedIndexChanged
        Dim ObjLinea As New combos
        ObjLinea.LlenaLineaPersonal(Me.DDLLinea, Me.LstPersonal.SelectedValue)
        ObjLinea = Nothing
        Me.LblMensaje.Text = ""
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            If fila.Row("datos_per") IsNot System.DBNull.Value Then
                e.Row.Cells(5).Text = fila.Row("datos_per").ToString
                e.Row.Cells(3).Text = "Personal sin Linea"
            ElseIf fila.Row("datos2_per") IsNot System.DBNull.Value Then
                e.Row.Cells(5).Text = fila.Row("datos2_per").ToString & " - " & fila.Row("nombre_lin").ToString
                e.Row.Cells(3).Text = "Personal con Linea"
            ElseIf fila.Row("datos_alu") IsNot System.DBNull.Value Then
                e.Row.Cells(5).Text = fila.Row("datos_alu").ToString
                e.Row.Cells(3).Text = "Alumno"
            ElseIf fila.Row("datos_ext") IsNot System.DBNull.Value Then
                e.Row.Cells(5).Text = fila.Row("datos_ext").ToString
                e.Row.Cells(3).Text = "Personal Externo"
            End If
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("id", "fila" & fila.Row("codigo_res").ToString & "")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            If e.Row.RowIndex = 0 Then
                e.Row.Cells(9).Text = ""
            End If

        End If
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Try
            Dim ObjResp As New Personal
            ObjResp.EliminarResponsableInv(e.Keys.Item(0))
            ObjResp = Nothing
            e.Cancel = True
            Me.GridView1.DataBind()
            Me.LblMensaje.Text = "Se eliminó el registro satisfactoriamente"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAgregar.Click
        Dim ObjResponsable As New Personal
        If Me.DDLTipoPersona.SelectedValue = 1 Then
            Dim codigolinea As Integer
            codigolinea = Me.DDLLinea.SelectedValue
            If codigolinea = 0 Then
                If ObjResponsable.AgregarResponsable("2", Me.LstPersonal.SelectedValue, 0, "", "", "", "", Int(Me.HidenCodigoInv.Value), Me.DDLTipoParticipacion.SelectedValue) = 1 Then
                    Me.LblMensaje.ForeColor = Drawing.Color.Navy
                    Me.LblMensaje.Text = "Se ingreso el registro satisfactoriamente"
                    Me.GridView1.DataBind()
                    Me.LstPersonal.SelectedIndex = -1
                    Me.DDLTipoParticipacion.SelectedIndex = -1
                Else
                    Me.LblMensaje.ForeColor = Drawing.Color.Red
                    Me.LblMensaje.Text = "Ocurrio un error en el ingreso de los datos"
                End If
            Else
                If ObjResponsable.AgregarResponsable("1", Me.DDLLinea.SelectedValue, 0, "", "", "", "", Int(Me.HidenCodigoInv.Value), Me.DDLTipoParticipacion.SelectedValue) = 1 Then
                    Me.LblMensaje.ForeColor = Drawing.Color.Navy
                    Me.LblMensaje.Text = "Se ingreso el registro satisfactoriamente"
                    Me.GridView1.DataBind()
                    Me.LstPersonal.SelectedIndex = -1
                    Me.DDLTipoParticipacion.SelectedIndex = -1
                Else
                    Me.LblMensaje.ForeColor = Drawing.Color.Red
                    Me.LblMensaje.Text = "Ocurrio un error en el ingreso de los datos"
                End If
            End If
        ElseIf Me.DDLTipoPersona.SelectedValue = 2 Then
            If ObjResponsable.AgregarResponsable("3", 0, Me.LstPersonal.SelectedValue, "", "", "", "", Int(Me.HidenCodigoInv.Value), Me.DDLTipoParticipacion.SelectedValue) = 1 Then
                Me.LblMensaje.ForeColor = Drawing.Color.Navy
                Me.LblMensaje.Text = "Se ingreso el registro satisfactoriamente"
                Me.GridView1.DataBind()
                Me.LstPersonal.SelectedIndex = -1
                Me.DDLTipoParticipacion.SelectedIndex = -1
            Else
                Me.LblMensaje.ForeColor = Drawing.Color.Red
                Me.LblMensaje.Text = "Ocurrio un error en el ingreso de los datos"
            End If

        ElseIf Me.DDLTipoPersona.SelectedValue = 3 Then
            If ObjResponsable.AgregarResponsable("4", 0, 0, Me.TxtNombre.Text, Me.TxtPaterno.Text, Me.TxtMaterno.Text, Me.TxtCentroLab.Text, Int(Me.HidenCodigoInv.Value), Me.DDLTipoParticipacion.SelectedValue) = 1 Then
                Me.LblMensaje.ForeColor = Drawing.Color.Navy
                Me.LblMensaje.Text = "Se ingreso el registro satisfactoriamente"
                Me.GridView1.DataBind()
                Me.DDLTipoParticipacion.SelectedIndex = -1
                Me.TxtCentroLab.Text = ""
                Me.TxtMaterno.Text = ""
                Me.TxtNombre.Text = ""
                Me.TxtPaterno.Text = ""
            Else
                Me.LblMensaje.ForeColor = Drawing.Color.Red
                Me.LblMensaje.Text = "Ocurrio un error en el ingreso de los datos"
            End If
        End If
        ObjResponsable = Nothing
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

    End Sub
End Class


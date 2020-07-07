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
            'Me.DDLLinea.Visible = True
            'Me.LblLinea.Visible = True
        ElseIf Me.DDLTipoPersona.SelectedValue = 2 Then
            Me.PanPersonal.Visible = True
            Me.PanExterno.Visible = False
            'Me.DDLLinea.Visible = False
            'Me.LblLinea.Visible = False
        Else
            Me.PanPersonal.Visible = False
            Me.PanExterno.Visible = True
            'Me.DDLLinea.Visible = False
            'Me.LblLinea.Visible = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.TxtBuscar.Attributes.Add("OnKeyPress", "if(event.keyCode==13){document.form1.CmdBuscar.focus(); return false;}")
        If Request.QueryString("modo") <> "ant" Then
            Me.CmdFinalizar.Attributes.Add("OnClick", "window.opener.location.reload(); window.close(); return false;")
        End If

        If IsPostBack = False Then
            Me.HidenCodigoInv.Value = Request.QueryString("codigo_Inv")
            Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim Datos As New DataTable
            'Datos = ObjDatos.TraerDataTable("ConsultarLineasDePersonal", 3, "")
            'ClsFunciones.LlenarListas(Me.DDLLinea, Datos, 0, 1, "----- Seleccione Linea de Personal -----")
            Datos.Dispose()
            Datos = ObjDatos.TraerDataTable("ConsultarTipoParticipacionInvestigacion", 1, "")
            ClsFunciones.LlenarListas(Me.DDLTipoParticipacion, Datos, 0, 1, "----- Seleccione Tipo de participacion -----")
            Datos.Dispose()
            ObjDatos = Nothing
            Me.PanPersonal.Visible = True
            Me.PanExterno.Visible = False
            'iif (Right(Eval("rutaProyecto_Inv"),3)="pdf","../../../../images/ext/pdf.gif",iif (Right(Eval("rutaProyecto_Inv"),3)="doc","../../../../images/ext/doc.gif",iif (Right(Eval("rutaProyecto_Inv"),3)="zip","../../../../images/ext/zip.gif","../../../../images/ext/rar.gif")))


        End If
        Me.GridView1.DataBind()
    End Sub

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim Tabla As DataTable

        Me.GridView1.DataBind()
        Me.LblMensaje.Text = ""
        If Me.DDLTipoPersona.SelectedValue = 1 Then
            Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            'ClsFunciones.LlenarListas(Me.DDLLinea, ObjDatos.TraerDataTable("ConsultarLineasDePersonal", 3, ""), 0, 1, "----- Seleccione Linea de Personal -----")
            ObjDatos = Nothing
            Dim ObjPersonal As New Investigacion
            Tabla = ObjPersonal.ConsultarUnidadesInvestigacion("9", Me.TxtBuscar.Text)
            Me.LstPersonal.Items.Clear()
            Me.LstPersonal.DataSource = Tabla
            Me.LstPersonal.DataTextField = Tabla.Columns(1).ToString
            Me.LstPersonal.DataValueField = Tabla.Columns(0).ToString
            Me.LstPersonal.DataBind()
            Me.LstPersonal.AutoPostBack = True
            ObjPersonal = Nothing

        ElseIf Me.DDLTipoPersona.SelectedValue = 2 Then
            Dim ObjPersonal As New Investigacion
            Dim objdatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Tabla = ObjPersonal.ConsultarInvestigaciones("7", Me.TxtBuscar.Text)
            'ClsFunciones.LlenarListas(Me.DDLLinea, objdatos.TraerDataTable("ConsultarLineasDePersonal", 3, ""), 0, 1, "----- Seleccione Linea de Personal -----")
            Me.LstPersonal.Items.Clear()
            Me.LstPersonal.DataSource = Tabla
            Me.LstPersonal.DataTextField = Tabla.Columns(1).ToString
            Me.LstPersonal.DataValueField = Tabla.Columns(0).ToString
            Me.LstPersonal.DataBind()
            Me.LstPersonal.AutoPostBack = False
            objdatos = Nothing
            ObjPersonal = Nothing
        End If
    End Sub

    Protected Sub LstPersonal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LstPersonal.SelectedIndexChanged
        'Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        'ClsFunciones.LlenarListas(Me.DDLLinea, ObjDatos.TraerDataTable("INV_ConsultarTemasInvestigacion", Me.LstPersonal.SelectedValue), "codigo_lip", "nombre_are", "----- Seleccione Linea de Personal -----")
        'ObjDatos = Nothing
        Me.LblMensaje.Text = ""
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            If fila.Row("datos_per") IsNot System.DBNull.Value Then
                e.Row.Cells(5).Text = fila.Row("datos_per").ToString
                'e.Row.Cells(3).Text = "Personal sin Linea"
            ElseIf fila.Row("datos2_per") IsNot System.DBNull.Value Then
                e.Row.Cells(5).Text = fila.Row("datos2_per").ToString & " - " & fila.Row("nombre_lin").ToString
                'e.Row.Cells(3).Text = "Personal con Linea"
            ElseIf fila.Row("datos_alu") IsNot System.DBNull.Value Then
                e.Row.Cells(5).Text = fila.Row("datos_alu").ToString
                'e.Row.Cells(3).Text = "Alumno"
            ElseIf fila.Row("datos_ext") IsNot System.DBNull.Value Then
                e.Row.Cells(5).Text = fila.Row("datos_ext").ToString
                'e.Row.Cells(3).Text = "Personal Externo"
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
            Dim ObjResp As New Investigacion
            ObjResp.EliminarResponsableInv(e.Keys.Item(0))
            ObjResp = Nothing
            e.Cancel = True
            Me.GridView1.DataBind()
            Me.LblMensaje.Text = "Se eliminó el registro satisfactoriamente"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAgregar.Click
        Dim ObjResponsable As New Investigacion
        If Me.DDLTipoPersona.SelectedValue = 1 Then
            'Dim codigolinea As Integer
            'codigolinea = Me.DDLLinea.SelectedValue
            'If codigolinea = 0 Then
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
            'Else
            '    If ObjResponsable.AgregarResponsable("1", Me.DDLLinea.SelectedValue, 0, "", "", "", "", Int(Me.HidenCodigoInv.Value), Me.DDLTipoParticipacion.SelectedValue) = 1 Then
            '        Me.LblMensaje.ForeColor = Drawing.Color.Navy
            '        Me.LblMensaje.Text = "Se ingreso el registro satisfactoriamente"
            '        Me.GridView1.DataBind()
            '        Me.LstPersonal.SelectedIndex = -1
            '        Me.DDLTipoParticipacion.SelectedIndex = -1
            '    Else
            '        Me.LblMensaje.ForeColor = Drawing.Color.Red
            '        Me.LblMensaje.Text = "Ocurrio un error en el ingreso de los datos"
            '    End If
            'End If
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

    Protected Sub CmdFinalizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdFinalizar.Click
        Response.Redirect("frminvestigacionotros.aspx?id=" & Request.QueryString("id"))
    End Sub
End Class


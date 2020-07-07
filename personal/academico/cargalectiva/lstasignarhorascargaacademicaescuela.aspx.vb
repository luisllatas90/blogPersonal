﻿
Partial Class lstasignarhorascargaacademicaescuela
    Inherits System.Web.UI.Page
    Dim codigo_cup As Int32 = -1
    Dim contador As Int16 = 0
    Dim PrimeraFila As Int16 = -1
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this)")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this)")

                contador = contador + 1
                'Combinar celdas
                If codigo_cup = fila("codigo_cup") Then
                    e.Row.Cells(0).Text = ""
                    e.Row.Cells(1).Text = ""
                    e.Row.Cells(2).Text = ""
                    e.Row.Cells(3).Text = ""
                    contador = contador - 1
                Else
                    e.Row.Cells(0).CssClass = "bordesup"
                    e.Row.Cells(1).CssClass = "bordesup"
                    e.Row.Cells(2).CssClass = "bordesup"
                    e.Row.Cells(3).CssClass = "bordesup"
                    e.Row.VerticalAlign = VerticalAlign.Middle
                    codigo_cup = fila("codigo_cup").ToString()
                    PrimeraFila = e.Row.RowIndex

                    'e.Row.Cells(0).Text = contador
                End If
                'Asignar linea separadora
                e.Row.Cells(4).CssClass = "bordesup" 'Autor
                e.Row.Cells(5).CssClass = "bordesup" 'Estado
                e.Row.Cells(6).CssClass = "bordesup"
                e.Row.Cells(7).CssClass = "bordesup"
                e.Row.Cells(8).CssClass = "bordesup"

                '=================================================================
                'Sólo se asigna horas, cuando se haya asignado la CARGA ACADEMICA
                '=================================================================
                Dim txtclase As TextBox = CType(e.Row.FindControl("txtHorasClase"), TextBox)
                Dim txtasesoria As TextBox = CType(e.Row.FindControl("txtHorasAsesoria"), TextBox)

                If fila.Row("totalHorasAula").ToString <> "" Then
                    e.Row.Cells(4).CssClass = "lineaprofesor"
                    e.Row.Cells(5).CssClass = "lineaprofesor"

                    txtclase.Visible = True
                    txtasesoria.Visible = True

                    txtclase.Attributes.Add("onkeypress", "validarnumero()")
                    txtasesoria.Attributes.Add("onkeypress", "validarnumero()")
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") Is Nothing Then
            Response.Redirect("../../../sinacceso.html")
        End If

        Try
            If IsPostBack = False Then
                Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                Dim tbl As Data.DataTable
                Dim codigo_tfu As Int16 = Request.QueryString("ctf")
                Dim codigo_usu As Integer = Session("id_per")
                Dim modulo As Integer = Request.QueryString("mod")
                '=================================
                'Permisos por Escuela
                '=================================
                tbl = obj.TraerDataTable("EVE_ConsultarCarreraProfesional", modulo, codigo_tfu, codigo_usu)

                ClsFunciones.LlenarListas(Me.dpEscuela, tbl, "codigo_cpf", "nombre_cpf", "--Seleccione--")
                ClsFunciones.LlenarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "UCI", 0), "codigo_cac", "descripcion_cac")
                obj = Nothing
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        If Me.dpEscuela.SelectedValue <> -1 Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Me.GridView1.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 8, Me.dpCodigo_cac.SelectedValue, Me.dpEscuela.SelectedValue, Me.dpFiltro.SelectedValue, 0)
            Me.GridView1.DataBind()
            obj = Nothing
            Me.GridView1.Visible = True
            Me.lblmensaje.Text = contador & " cursos programados."
            Me.cmdGuardar.Visible = Me.GridView1.Rows.Count > 0
        Else
            Me.lblmensaje.Text = "0 cursos programados."
            Me.GridView1.Visible = False
        End If
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim I As Integer
        Dim Fila As GridViewRow
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Me.lblmensaje.Text = ""
        Try
            'obj.IniciarTransaccion()
            For I = 0 To Me.GridView1.Rows.Count - 1
                Fila = Me.GridView1.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    '==================================
                    ' Guardar los datos
                    '==================================
                    Dim cbxtipo As DropDownList = CType(Fila.FindControl("cboTipoClase"), DropDownList)
                    Dim txtclase As TextBox = CType(Fila.FindControl("txtHorasClase"), TextBox)
                    Dim txtasesoria As TextBox = CType(Fila.FindControl("txtHorasAsesoria"), TextBox)
                    Dim dp As DropDownList = CType(Fila.FindControl("dpcodigo_fun"), DropDownList)

                    If txtclase.Text.ToString = "" Then txtclase.Text = 0
                    If txtasesoria.Text.ToString = "" Then txtasesoria.Text = 0
                    If cbxtipo.SelectedValue = "" Then cbxtipo.SelectedValue = "TE"

                    obj.Ejecutar("CAR_ModificarHorasCargaAcademica", Me.dpCodigo_cac.SelectedValue, Me.GridView1.DataKeys.Item(Fila.RowIndex).Values("codigo_cup"), Me.GridView1.DataKeys.Item(Fila.RowIndex).Values("codigo_perAsig"), txtclase.Text, txtasesoria.Text, Session("id_per"), dp.SelectedValue, cbxtipo.SelectedValue)
                End If
            Next
            'obj.TerminarTransaccion()
            Page.RegisterStartupScript("Grabar", "<script>alert('Los datos de han guardado correctamente')</script>")
            Me.GridView1.DataBind()
            Me.GridView1.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 8, Me.dpCodigo_cac.SelectedValue, Me.dpEscuela.SelectedValue, 2, 0)
            Me.GridView1.DataBind()
        Catch ex As Exception
            '    'obj.AbortarTransaccion()
            '    Me.cmdGuardar.Visible = False
            Page.RegisterStartupScript("Grabar", "<script>alert('Ocurrió un Error al Registrar el estado. Intente mas tarde." & Chr(13) & ex.Message & "'")
        End Try
        obj = Nothing
    End Sub

    Protected Sub cmdVer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVer.Click
        'Response.Redirect("rptecargaacademicaescuela.aspx?id=" & Session("id_per") & "&ctf=" & Request.QueryString("ctf"))
        Response.Redirect("rptecargaacademicaescuela.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&mod=" & Request.QueryString("mod"))
    End Sub
End Class

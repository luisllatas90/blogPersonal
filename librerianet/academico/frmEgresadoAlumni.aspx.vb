﻿
Partial Class academico_frmEgresadoAlumni
    Inherits System.Web.UI.Page


    Protected Sub grwPosiblesEgresados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwPosiblesEgresados.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            'e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(1).Text = e.Row.RowIndex + 1
            e.Row.Cells(5).Text = IIf(fila.Row("estadoactual_alu") = 0, "Inactivo", "Activo")
            If e.Row.Cells(5).Text = "Activo" Then
                e.Row.Cells(5).Font.Bold = True
            Else
                e.Row.Cells(5).Font.Bold = False
                e.Row.Cells(5).ForeColor = System.Drawing.Color.Red
            End If

            CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")

            e.Row.Cells(13).Text = "-" 'DebeIdiomas(Me.grwPosiblesEgresados.DataKeys.Item(e.Row.RowIndex).Values("codigo_alu"), Me.dpCodigo_pes.SelectedValue)
            'If e.Row.Cells(13).Text = "NO" Then
            '    e.Row.Cells(13).Font.Bold = True
            'Else
            '    e.Row.Cells(13).Font.Bold = False
            '    e.Row.Cells(13).ForeColor = System.Drawing.Color.Red
            'End If

            'If dpTipo.SelectedValue = 1 Then
            'e.Row.Cells(0).Text = ""
            'ElseIf fila.Row("DebeTesis") > 0 Then
            'e.Row.Cells(0).Text = ""
            'Else
            'CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")
            'End If
        End If
    End Sub

    Private Sub CargaCicloAcademico()
        Try
            Dim objcnx As New ClsConectarDatos
            Dim objFun As New ClsFunciones
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()
            Dim TablaCronograma As data.datatable
            TablaCronograma = objcnx.TraerDataTable("ConsultarCicloAcademico", "EGR", Request.QueryString("mod"))
            objFun.CargarListas(Me.cboCiclo, TablaCronograma, "codigo_cac", "descripcion_cac")
            CargarCronograma()
            TablaCronograma = Nothing
            objcnx.CerrarConexion()
        Catch ex As Exception
            Me.cmdGuardar.Enabled = False
            Page.RegisterStartupScript("error", "alert('Ocurrió un error al cargar el ciclo académico');")
        End Try        
    End Sub
    Sub cargarPlanes()
        Dim tbl As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tbl = obj.TraerDataTable("Alumni_ListarPlanEstudiosxCarrera", Me.dpCodigo_cpf.SelectedValue)
        ClsFunciones.LlenarListas(Me.dpCodigo_pes, tbl, "codigo_pes", "descripcion_Pes", "--Seleccione el Plan de Estudios--")
        obj.CerrarConexion()
        obj = Nothing
        tbl = Nothing
    End Sub

    Private Function DebeIdiomas(ByVal codigo_alu As Integer, ByVal codigo_pes As Integer) As String

        Dim objcnx As New ClsConectarDatos
        Dim objFun As New ClsFunciones
        objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objcnx.AbrirConexion()
        Dim tb As data.datatable
        tb = objcnx.TraerDataTable("Acad_AlumnoDebeIdiomas", codigo_alu, codigo_pes)

        If tb.rows.count > 0 Then
            If tb.rows(0).item("debe") = "0" Then
                DebeIdiomas = "NO"
            Else
                DebeIdiomas = "SI"
            End If
        Else
            DebeIdiomas = "-"
        End If

        tb = Nothing
        objcnx.CerrarConexion()

        Return DebeIdiomas
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("id_per") Is Nothing Then
        '    Response.Redirect("../../sinacceso.html")
        'End If

        'If (Request.QueryString("id") IsNot Nothing _
        '    And Request.QueryString("ctf") IsNot Nothing _
        '    And Request.QueryString("mod") IsNot Nothing) Then

        If IsPostBack = False Then

            Dim tbl As Data.DataTable
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id") 'Session("id_per") '
            Dim codigo_test As Integer = Request.QueryString("mod")
            '=================================
            'Permisos por Escuela
            '=================================
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            'tbl = obj.TraerDataTable("EVE_ConsultarCarreraProfesional", codigo_test, codigo_tfu, codigo_usu)



            'tbl = obj.TraerDataTable("Alumni_ListarCarrerasxAcceso", codigo_usu, codigo_tfu)
            tbl = obj.TraerDataTable("Alumni_ListarCarrerasxAccesoxtest", codigo_usu, codigo_tfu, codigo_test)

            '=================================
            'Llenar combos
            '=================================
            ClsFunciones.LlenarListas(Me.dpCodigo_cpf, tbl, "codigo_cpf", "nombre_cpf", "--Seleccione la Carrera Profesional--")


            cargarPlanes()
            tbl.Dispose()
            obj.CerrarConexion()
            obj = Nothing

            CargaCicloAcademico()
        End If
        'Else
        'Response.Redirect("../ErrorSistema.aspx")
        'End If
    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Try
            Me.cmdGuardar.Visible = False
            Me.lblmensaje.Visible = False
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'Me.grwPosiblesEgresados.DataSource = obj.TraerDataTable("ACAD_ConsultarEgresadoPes", Me.dpTipo.SelectedValue, Me.dpCodigo_cpf.SelectedValue)
            Me.grwPosiblesEgresados.DataSource = obj.TraerDataTable("ACAD_ConsultarAlumnoEgresado", Me.dpCodigo_cpf.SelectedValue, Me.txtAlumno.Text, Me.dpCodigo_pes.SelectedValue)
            Me.grwPosiblesEgresados.DataBind()
            obj.CerrarConexion()
            obj = Nothing

            'Response.Write(Me.dpCodigo_cpf.SelectedValue)
            'Response.Write("<br>")
            'Response.Write(Me.dpCodigo_pes.SelectedValue)

            If grwPosiblesEgresados.Rows.Count > 0 Then 'And Me.dpTipo.SelectedValue = 0 Then
                cmdGuardar.Visible = True
                Me.lblmensaje.Visible = True
            End If
        Catch ex As Exception
            Me.lblAviso.Text = "Error al realizar busqueda: " & ex.Message
        End Try
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim Fila As GridViewRow
        Dim obj As New ClsConectarDatos
        Dim mail As New ClsMail
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim codigo_usu As Integer = Session("id_per") 'Request.QueryString("id")

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
                        'obj.Ejecutar("ALUMNI_InsertaEgresado", Me.grwPosiblesEgresados.DataKeys.Item(Fila.RowIndex).Values("codigo_alu"), codigo_usu, "Campus de Personal. ", Me.cboCiclo.SelectedValue)
                        obj.Ejecutar("ALUMNI_InsertaEgresadoV2", Me.grwPosiblesEgresados.DataKeys.Item(Fila.RowIndex).Values("codigo_alu"), codigo_usu, "Campus de Personal. ", Me.cboCiclo.SelectedValue)
                    End If
                End If
            Next
            obj.CerrarConexion()

            Dim sw As Byte = 0
            Dim strDescripcion As String = ""
            strDescripcion = ""
            obj.AbrirConexion()
            For I As Int16 = 0 To Me.grwPosiblesEgresados.Rows.Count - 1
                Fila = Me.grwPosiblesEgresados.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then                        
                        sw = 0
                        If (Me.grwPosiblesEgresados.Rows(I).Cells(6).Text.Trim = "") Then
                            sw = 1
                            mail.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual USAT", Me.grwPosiblesEgresados.Rows(I).Cells(6).Text, "Egresado USAT", strDescripcion, True)
                        End If

                        If (Me.grwPosiblesEgresados.Rows(I).Cells(7).Text.Trim = "") Then
                            sw = 1
                            mail.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual USAT", Me.grwPosiblesEgresados.Rows(I).Cells(7).Text, "Egresado USAT", strDescripcion, True)
                        End If

                        If (sw = 0) Then

                        End If

                    End If
                End If
            Next

            'Me.grwPosiblesEgresados.DataSource = obj.TraerDataTable("ACAD_ConsultarEgresadoPes", Me.dpTipo.SelectedValue, Me.dpCodigo_cpf.SelectedValue)
            Me.grwPosiblesEgresados.DataSource = obj.TraerDataTable("ACAD_ConsultarAlumnoEgresado", Me.dpCodigo_cpf.SelectedValue, Me.txtAlumno.Text)
            Me.grwPosiblesEgresados.DataBind()
            obj.CerrarConexion()
            Me.cmdGuardar.Enabled = True
            Me.lblAviso.Text = "Se han actualizado los datos correctamente."
            'Page.RegisterStartupScript("ok", "alert('Se han actualizado los datos correctamente');")
        Catch ex As Exception
            obj = Nothing
            Me.cmdGuardar.Enabled = True
            If ex.Message.Contains("addresses") Then
                Me.lblAviso.Text = "Se actualizó al estudiante como egresado, pero ocurrió un problema con el envío del correo."
            Else
                Me.lblAviso.Text = "Ocurrió un Error al actualizar los datos. Contáctese con desarrollosistemas@usat.edu.pe : " & ex.Message
            End If
            'Me.lblAviso.Text = ex.Message
            'Page.RegisterStartupScript("error", "alert('Ocurrió un Error al actualizar los datos \n Contáctese con desarrollosistemas@ussat.edu.pe');")
        End Try
    End Sub

    Protected Sub dpCodigo_cpf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpCodigo_cpf.SelectedIndexChanged
        cargarPlanes()
    End Sub

    Sub CargarCronograma()
        Try
            Dim objcnx As New ClsConectarDatos
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()
            Dim TablaCronograma As data.datatable
            TablaCronograma = objcnx.TraerDataTable("ConsultarCicloAcademico", "EGR", Request.QueryString("mod"))
            objcnx.CerrarConexion()
            Me.lblCronograma.text = "No establecido"
            Dim y As Integer = 0
            For x As Integer = 0 To TablaCronograma.rows.count - 1
                If TablaCronograma.rows(x).item("descripcion_cac").toString = Me.cboCiclo.selecteditem.text Then
                    Me.lblCronograma.text = iif(TablaCronograma.rows(x).item("fechaini_cro").toString <> "", TablaCronograma.rows(x).item("fechaini_cro").toString.substring(0, 10), "") & " al " & iif(TablaCronograma.rows(x).item("fechafin_cro").toString <> "", TablaCronograma.rows(x).item("fechafin_cro").toString.substring(0, 10), "")
                    y = x
                    Exit For
                End If
            Next
            If Me.lblCronograma.text = " al " Then
                Me.lblCronograma.text = "No establecido"
                Me.cmdGuardar.enabled = False
            Else
                If Date.Now >= CDate(TablaCronograma.rows(y).item("fechaini_cro")) And Date.Now <= CDate(TablaCronograma.rows(y).item("fechafin_cro")) Then
                    Me.cmdGuardar.enabled = True
                Else
                    Me.cmdGuardar.enabled = False
                End If
            End If

            'MTesen indicó que para Coordinacion academica y Dirección académica debe tener activo siempre el boton
            If (Request.QueryString("ctf") = 85 Or Request.QueryString("ctf") = 181) Then
                Me.cmdGuardar.Enabled = True
            End If
        Catch ex As Exception
        End Try      
    End Sub
    Protected Sub cboCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCiclo.SelectedIndexChanged
        CargarCronograma()
    End Sub
End Class

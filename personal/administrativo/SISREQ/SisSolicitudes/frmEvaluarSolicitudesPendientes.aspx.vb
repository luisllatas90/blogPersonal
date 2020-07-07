﻿
Partial Class SisSolicitudes_ListaDeSolicitudes
    Inherits System.Web.UI.Page

    Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
    Protected Sub llenarGvSolicitudes()
        Try
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)


            Select Case Me.CboVer.SelectedValue
                Case "P"

                    If cboCicloAcad.SelectedValue = -1 Then
                        Me.GvSolicitudes.DataSource = Obj.TraerDataTable("SOL_ConsultarSolicitudesEvaluador", 1, Request.QueryString("id"), 0, 0)
                    Else
                        Me.GvSolicitudes.DataSource = Obj.TraerDataTable("SOL_ConsultarSolicitudesEvaluador", 1, Request.QueryString("id"), Me.cboCicloAcad.SelectedValue, 0)
                    End If

                    Me.BOTONES.VISIBLE = True
                    Me.lblObservacion.VISIBLE = True
                    Me.txtObservacion.VISIBLE = True
                Case "1", "2"
                    If cboCicloAcad.SelectedValue = -1 Then
                        Me.GvSolicitudes.DataSource = Obj.TraerDataTable("SOL_ConsultarSolicitudesEvaluador", 2, Request.QueryString("id"), 0, Me.CboVer.SelectedValue)
                    Else
                        Me.GvSolicitudes.DataSource = Obj.TraerDataTable("SOL_ConsultarSolicitudesEvaluador", 2, Request.QueryString("id"), Me.cboCicloAcad.SelectedValue, Me.CboVer.SelectedValue)
                    End If
                    Me.BOTONES.VISIBLE = False
                    Me.lblObservacion.VISIBLE = False
                    Me.txtObservacion.VISIBLE = False
                Case Else

            End Select

            Me.GvSolicitudes.DataBind()
            ActivarControles("hidden")
            GvSolicitudes.PageIndex = 0
        Catch ex As Exception
            Response.Write(ex)
            ClientScript.RegisterStartupScript(Me.GetType, "ErrVer", "alert('Ocurrió un error al consultar los datos')", True)

        End Try

    End Sub

    Protected Sub GvSolicitudes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvSolicitudes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('GvSolicitudes','Select$" & e.Row.RowIndex & "');")
            e.Row.Style.Add("cursor", "hand")
        End If

    End Sub

    Protected Sub GvSolicitudes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvSolicitudes.SelectedIndexChanged
        Try

            Dim datos, datos_motivo, datos_asunto, datos_Aux, corresponde, asistencia As New Data.DataTable
            Dim asunto, motivo As String
            Dim DirectorEscuela, Dat_Director As Data.DataTable

           
            Me.txtObservacion.text = ""
            Me.lblUltAsistencia.text = "Fecha última asistencia: "
            Me.HddCodigoSol.Value = Me.GvSolicitudes.DataKeys.Item(Me.GvSolicitudes.SelectedIndex).Values(0).ToString
            Me.HddAlumno.Value = Me.GvSolicitudes.DataKeys.Item(Me.GvSolicitudes.SelectedIndex).Values(1).ToString
            datos_asunto = Obj.TraerDataTable("SOL_ConsultarSolicitudesPendientes", 4, Me.HddCodigoSol.Value.Trim)
            hddCodigo_tas.Value = CInt(datos_asunto.Rows(0).Item("codigo_tas"))


            'Verifica si quien registra es el director de escuela y asigna el centro de costo para saber si tiene un asistente para evaluar
            HddEsDirector.Value = 0
            DirectorEscuela = Obj.TraerDataTable("SOL_ConsultarEvaluacionSolicitud", 3, Me.HddCodigoSol.Value, CInt(Request.QueryString("id")))
           
            If DirectorEscuela.Rows.Count > 0 Then
                HddEsDirector.Value = 1
                Me.HddCodigoCco.Value = DirectorEscuela.Rows(0).Item("codigo_cco").ToString
            Else
                Dat_Director = Obj.TraerDataTable("SOL_VerificaDirectorEscuela", CInt(Request.QueryString("id")))
                If Dat_Director.Rows.Count > 0 Then
                    Me.HddCodigoCco.Value = Dat_Director.Rows(0).Item("codigo_cco").ToString
                    DirectorEscuela = Obj.TraerDataTable("SOL_ConsultarEvaluacionSolicitud", 3, Me.HddCodigoSol.Value, CInt(Request.QueryString("id")))
                    If DirectorEscuela.Rows.Count > 0 Then
                        HddEsDirector.Value = 1
                    End If
                End If
            End If
            
            Dim Ruta As New EncriptaCodigos.clsEncripta
            Me.LblEstado.Text = Me.GvSolicitudes.DataKeys.Item(Me.GvSolicitudes.SelectedIndex).Values(2).ToString
            Select Case Me.LblEstado.Text
                Case "P"
                    Me.LblEstado.ForeColor = Drawing.Color.Red
                    Me.LblEstado.Font.Strikeout = False
                    Me.LblEstado.Text = "Pendiente"
                Case "T"
                    Me.LblEstado.ForeColor = Drawing.Color.Green
                    Me.LblEstado.Font.Strikeout = False
                    Me.LblEstado.Text = "Finalizada"
                Case "A"
                    Me.LblEstado.ForeColor = Drawing.Color.Red
                    Me.LblEstado.Font.Strikeout = True
                    Me.LblEstado.Text = "Anulada"
            End Select

            If LblEstado.Text <> "Anulada" Then
                If HddEsDirector.Value = 1 Then
                    If DirectorEscuela.Rows(0).Item("codigo_res") = 0 Then
                        Me.CmdAprobarEval.Enabled = True
                    Else
                        Me.CmdAprobarEval.Enabled = False
                    End If
                Else
                    datos_Aux = Obj.TraerDataTable("SOL_ConsultarEvaluadorAuxiliar", 2, CInt(Request.QueryString("id")), HddCodigoSol.Value)
                    If datos_Aux.Rows.Count > 0 Then
                        HddCodigoCco.Value = datos_Aux.Rows(0).Item("codigo_cco").ToString
                        If CInt(datos_Aux.Rows(0).Item("codigo_res")) > 0 Then
                            CmdAprobarEval.Enabled = False
                        Else
                            CmdAprobarEval.Enabled = True
                        End If
                    Else
                        CmdAprobarEval.Enabled = False
                    End If
                End If
            Else
                CmdAprobarEval.Enabled = False
            End If
           
            Me.ImgFoto.Dispose()
            Me.ImgFoto.Visible = True
            Me.ImgFoto.ImageUrl = ""
            '---------------------------------------------------------------------------------------------------------------
            'Fecha: 29.10.2012
            'Usuario: dguevara
            'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
            '---------------------------------------------------------------------------------------------------------------
            Me.ImgFoto.ImageUrl = "//intranet.usat.edu.pe/imgestudiantes/" & Ruta.CodificaWeb("069" & Me.HddAlumno.Value)
            Me.ImgFoto.DataBind()

            ActivarControles("visible")

            If Me.CboVer.SelectedValue = "P" Then
                asistencia = Obj.TraerDataTable("SOL_ConsultarUltimaAsistencia", Me.HddAlumno.Value)

                If asistencia.Rows.Count > 0 Then

                    If asistencia.Rows(0).Item("ultima_asistencia").ToString <> "" Then

                        Me.lblUltAsistencia.Text = Me.lblUltAsistencia.Text & asistencia.Rows(0).Item("ultima_asistencia").ToString
                    End If

                End If
                Me.txtObservacion.Text = ""
                Me.txtObservacion.Enabled = True
                Me.txtObservacion.Visible = True
                Me.botones.visible = True
                Me.lblUltAsistencia.Visible = True
            Else
                Me.txtObservacion.Visible = True
                Me.lblObservacion.Visible = True
                Me.lblUltAsistencia.Visible = False
                'ClientScript.RegisterStartupScript(Me.GetType, "ErrVer", "alert('" & DirectorEscuela.Rows(0).Item("observacion_Eva").ToString.Replace(char(10), "<br>") & "')", True)
                Me.txtObservacion.Text = DirectorEscuela.Rows(0).Item("observacion_Eva").ToString
                Me.txtObservacion.Enabled = False
            End If


        Catch ex As Exception
            Response.Write(ex)
            ClientScript.RegisterStartupScript(Me.GetType, "ErrVer", "alert('Ocurrió un error al consultar los datos')", True)

        End Try
    End Sub


    Protected Sub ActivarControles(ByVal valor As String)
        Page.RegisterStartupScript("Solicitud", "<script>tblDatos.style.visibility ='" & valor & "'</script>")
    End Sub
    Protected Sub LimpiarControlesHidden()
        Me.HddAlumno.Value = ""
        Me.hddCodigo_tas.Value = ""
        Me.HddCodigoSol.Value = ""
        Me.HddCodigoCco.Value = ""
        Me.HddEsDirector.Value = ""
    End Sub

    Protected Sub GvEvaluacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvEvaluacion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem
            If CInt(Fila.Item("codigo_res")) = 0 Then 'Verifica si las instancias ya resolvieron la solicitud
                'Verifica si es Julia quien evalua no queda como pendiente por ella tiene la funcion de visualizacion mas no de calificar
                If Fila.Item("codigo_per") = 473 Then '-->Codigo per de Julia Danjanovic
                    e.Row.Cells(2).ForeColor = Drawing.Color.Green
                    e.Row.Cells(2).Text = "-"
                Else
                    e.Row.Cells(2).ForeColor = Drawing.Color.Red
                    e.Row.Cells(2).Text = "Pendiente"
                End If
                If Fila.Item("codigo_per") = Request.QueryString("id") Then
                    e.Row.Font.Bold = True
                End If
            Else
                e.Row.Cells(2).ForeColor = Drawing.Color.Green
                e.Row.Cells(2).Text = "Ya dió respuesta"
                If e.Row.Cells(3).Text = "APROBADO" Then
                    e.Row.Cells(3).ForeColor = Drawing.Color.Blue
                Else
                    e.Row.Cells(3).ForeColor = Drawing.Color.Red
                End If
            End If
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                ClsFunciones.LlenarListas(cboCicloAcad, Obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac", "Todos")
                ActivarControles("hidden")
                Me.CboVer.SelectedValue = "P"
                Me.cboCicloAcad.SelectedValue = getCicloActual()
                llenarGvSolicitudes()
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "ErrLoad", "alert('Ocurrió un error al consultar los datos')", True)
        End Try
    End Sub

    Protected Sub DvEstudiante_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles DvEstudiante.DataBound
        If DvEstudiante.Rows.Count > 0 Then
            If DvEstudiante.Rows(5).Cells(1).Text = "Activo" Then
                DvEstudiante.Rows(5).Cells(1).ForeColor = Drawing.Color.Blue
            Else
                DvEstudiante.Rows(5).Cells(1).ForeColor = Drawing.Color.Red
            End If

            If DvEstudiante.Rows(7).Cells(1).Text = "Si" Then
                DvEstudiante.Rows(7).Cells(1).ForeColor = Drawing.Color.Red
            Else
                DvEstudiante.Rows(7).Cells(1).ForeColor = Drawing.Color.Blue
            End If

            DvEstudiante.Rows(6).Cells(1).ForeColor = Drawing.Color.Red
        End If
    End Sub


    Protected Sub GvSolicitudes_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GvSolicitudes.PageIndexChanging
        GvSolicitudes.PageIndex = e.NewPageIndex
        Me.GvSolicitudes.SelectedIndex = -1
        llenarGvSolicitudes()
    End Sub
    Protected Sub CmdDesaprobarEval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdDesaprobarEval.Click
        Dim asistencia As New Data.DataTable
        Try

            Dim observacion As String
            If hddCodigo_tas.Value < 3 Or hddCodigo_tas.Value = 15 Then
                observacion = Me.txtObservacion.Text & " | " & Me.lblUltAsistencia.Text
            Else
                observacion = Me.txtObservacion.Text

            End If

            Obj.IniciarTransaccion()
            Obj.Ejecutar("SOL_AgregarEvaluacionSolicitudCliente", 2, Me.HddCodigoSol.Value, 2, Me.HddCodigoCco.Value, 0, observacion, CInt(Request.QueryString("id").ToString))
            Obj.TerminarTransaccion()


            ClientScript.RegisterStartupScript(Me.GetType, "ErrVer", "alert('Solicitud Desaprobada')", True)

            Me.GvSolicitudes.SelectedIndex = -1
            Me.txtObservacion.text = ""

            ActivarControles("hidden")
            llenarGvSolicitudes()

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "ErrVer", "alert('Ocurrió un error al consultar los datos')", True)

        End Try
    End Sub
    Protected Sub CmdAprobarEval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAprobarEval.Click
        Try

            Dim observacion As String
            If hddCodigo_tas.Value < 3 Or hddCodigo_tas.Value = 15 Then
                observacion = Me.txtObservacion.Text & " | " & Me.lblUltAsistencia.Text
            Else
                observacion = Me.txtObservacion.Text

            End If

            Obj.IniciarTransaccion()
            Obj.Ejecutar("SOL_AgregarEvaluacionSolicitudCliente", 2, Me.HddCodigoSol.Value, 1, Me.HddCodigoCco.Value, 0, observacion, CInt(Request.QueryString("id").ToString))
            Obj.TerminarTransaccion()


            ClientScript.RegisterStartupScript(Me.GetType, "ErrVer", "alert('Solicitud Aprobada')", True)

            Me.GvSolicitudes.SelectedIndex = -1
            Me.txtObservacion.text = ""

            ActivarControles("hidden")
            llenarGvSolicitudes()
        Catch ex As Exception
            Response.Write(ex)
            ClientScript.RegisterStartupScript(Me.GetType, "ErrVer", "alert('Ocurrió un error al consultar los datos')", True)
        End Try
    End Sub

    Protected Sub CboVer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboVer.SelectedIndexChanged
        Try
            llenarGvSolicitudes()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub cboCicloAcad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCicloAcad.SelectedIndexChanged
        Try
            llenarGvSolicitudes()
        Catch ex As Exception
        End Try
    End Sub
    Protected Function getCicloActual() As String
        Dim dat As New Data.DataTable
        dat = Obj.TraerDataTable("ConsultarCicloAcademico", "CV", "1")

        If dat.Rows.Count > 0 Then
            Return dat.Rows(0).Item("codigo_cac").ToString
        End If

    End Function
End Class


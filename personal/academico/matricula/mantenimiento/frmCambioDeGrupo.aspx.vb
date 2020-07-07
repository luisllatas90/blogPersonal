﻿
Partial Class academico_matricula_administrar_frmCambioDeGrupo
    Inherits System.Web.UI.Page
    Dim FilaAnterior As Int32


    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        ConsultarCursosMatriculados()
        ConsultarCronograma()
        Me.lblMensaje.Text = ""
        Me.lblDatos.Text = ""
        Me.rblHorarios.Items.Clear()
        '### Si la fecha de cronograma ya venció no se permitirá cambio de grupos ###
        If hddDisponible.Value = False Then
            Me.cmdGuardar.Enabled = hddDisponible.Value
            lblMensaje.Text = "La fecha para cambio de grupos ha finalizado, verifique el cronograma académico"
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Font.Bold = True
            cmdGuardar.Enabled = False
        End If
    End Sub

    Private Sub ConsultarCronograma()
        Dim ObjCnx As New ClsConectarDatos
        Dim datos As Data.DataTable
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        '### Consulta cronograma para cambio de grupo ###
        'Tipo = "GE" es cambio de grupo por evaluacion y registro
        ObjCnx.AbrirConexion()
        datos = ObjCnx.TraerDataTable("ACAD_ConsultarCronograma", Request.QueryString("tipo"), Me.ddlCicloAcad.SelectedValue, Request.QueryString("mod"))
        ObjCnx.CerrarConexion()
        If datos.Rows.Count > 0 Then
            hddDisponible.Value = True
        Else
            hddDisponible.Value = False
        End If
    End Sub

    Sub ConsultarCursosMatriculados()
        Dim ObjCnx As New ClsConectarDatos
        Dim datosAlu As New Data.DataTable
        Dim Ruta As New EncriptaCodigos.clsEncripta
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        '### Consulta datos del alumno ###
        ObjCnx.AbrirConexion()
        'datosAlu = ObjCnx.TraerDataTable("dbo.SOL_ConsultarAlumnoParaSolicitud", 1, Me.txtCodUniversitario.Text)
        datosAlu = ObjCnx.TraerDataTable("EVE_ConsultarAlumnoParaMatricula", Me.txtCodUniversitario.Text, Request.QueryString("mod"), 0)
        ObjCnx.CerrarConexion()

        ImgFoto.Width = 80
        ImgFoto.BorderColor = Drawing.Color.Black
        ImgFoto.BorderWidth = 1
        If datosAlu.Rows.Count > 0 Then
            With datosAlu.Rows(0)
                If .Item("codigo_test") = Request.QueryString("mod") Then
                    '---------------------------------------------------------------------------------------------------------------
                    'Fecha: 29.10.2012
                    'Usuario: dguevara
                    'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
                    '---------------------------------------------------------------------------------------------------------------
                    Me.ImgFoto.ImageUrl = "//intranet.usat.edu.pe/imgestudiantes/" & Ruta.CodificaWeb("069" & .Item("codigouniver_alu").ToString)
                    hddCodigo_alu.Value = .Item("codigo_alu")
                    hddCodigo_pes.Value = .Item("codigo_pes")
                    If datosAlu.Rows.Count > 0 Then
                        MostrarDatos(True)
                        FvDatos.DataSource = datosAlu
                        FvDatos.DataBind()
                        ObjCnx.AbrirConexion()
                        'gvDetalleMatricula.DataSource = ObjCnx.TraerDataTable("ACAD_ConsultarCursosMatriculadosPorAsesor", .Item("codigo_alu"), .Item("codigo_pes"), ddlCicloAcad.SelectedValue, Request.QueryString("ctf"))
                        gvDetalleMatricula.DataSource = ObjCnx.TraerDataTable("ConsultarCursosMatriculadosPorAsesor", .Item("codigo_alu"), .Item("codigo_pes"), ddlCicloAcad.SelectedValue, Request.QueryString("ctf"), 15)                        

                        gvDetalleMatricula.DataBind()
                        ObjCnx.CerrarConexion()
                    Else
                        Me.pnlDatos.Visible = False
                    End If
                    Me.ImgFoto.AlternateText = .Item("codigouniver_alu")
                Else
                    '### si el estudiante es de otro tipo de estudios ###
                    ClientScript.RegisterStartupScript(Me.GetType, "alumno", "alert('El estudiante pertenece a otra modalidad de estudio')", True)
                    MostrarDatos(False)
                End If
            End With
        Else
            '### no hay datos de alumnos ###
            ClientScript.RegisterStartupScript(Me.GetType, "alumno", "alert('El estudiante pertenece a otra modalidad de estudio')", True)
            MostrarDatos(False)
        End If
        Me.gvHorario.Visible = False

    End Sub

    Sub MostrarDatos(ByVal estado As Boolean)
        Me.pnlDatos.Visible = estado
        Me.pnlCambioGrupo.Visible = estado
        Me.gvDetalleMatricula.Visible = estado
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Validar si existe la sesión
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If Not IsPostBack Then
            Dim ObjCnx As New ClsConectarDatos
            Dim ObjFun As New ClsFunciones
            MostrarDatos(False)
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            ObjFun.CargarListas(Me.ddlCicloAcad, ObjCnx.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")
            'ddlCicloAcad.SelectedValue = ObjCnx.TraerDataTable("ConsultarCicloAcademico", "CV", 1).Rows(0).Item("codigo_cac")
            ddlCicloAcad.SelectedValue = ObjCnx.TraerDataTable("ACAD_RetornaCicloVigenteTipoEstudio", Request.QueryString("mod")).Rows(0).Item("codigo_cac")
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            ObjFun = Nothing
            FilaAnterior = 0 ' tomo el primer valor
            Me.ddlCicloAcad.Enabled = False
        End If
    End Sub

 
    Protected Sub gvDetalleMatricula_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalleMatricula.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            If fila.Row.Item("PermitirRetiro") = "S" Then
                If fila.Row.Item("estado_dma").ToString = "R" Then
                    e.Row.Cells(7).ForeColor = Drawing.Color.Red
                    e.Row.Cells(8).Enabled = False
                Else
                    e.Row.Cells(7).ForeColor = Drawing.Color.Blue
                    e.Row.Cells(8).Enabled = True
                End If
            Else
                If fila.Row.Item("estado_dma").ToString = "R" Then
                    e.Row.Cells(7).ForeColor = Drawing.Color.Red
                    e.Row.Cells(8).Enabled = False
                Else
                    e.Row.Cells(7).ForeColor = Drawing.Color.Blue
                    e.Row.Cells(8).Enabled = True
                End If
                'e.Row.Cells(8).Enabled = False
                'If fila.Row.Item("estado_dma").ToString = "R" Then
                '    e.Row.Cells(7).ForeColor = Drawing.Color.Red
                'Else
                '    e.Row.Cells(7).ForeColor = Drawing.Color.Blue
                'End If
            End If
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvDetalleMatricula','Select$" & e.Row.RowIndex & "')")
            e.Row.Style.Add("cursor", "hand")
        End If
    End Sub



    Protected Sub gvHorario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvHorario.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim VacantesDisponibles As String
            fila = e.Row.DataItem

            With fila.Row
                
                If .Item("codigo_cup") <> FilaAnterior Then
                    FilaAnterior = .Item("codigo_cup")
                    If .Item("dia_lho") <> "" Or .Item("dia_lho") IsNot DBNull.Value Then
                        e.Row.Cells(1).Text = "- " & ConvDia(.Item("dia_Lho")) & " " & .Item("nombre_hor") & "-" & .Item("horafin_Lho") & _
                                              "<br>&nbsp;&nbsp;" & .Item("ambiente") & "(Hrs. " & .Item("tipohoracur_lho") & ")"
                        e.Row.Cells(1).ForeColor = Drawing.Color.Black
                    Else
                        e.Row.Cells(1).Text = "[No hay horario registrado]"
                        e.Row.Cells(1).ForeColor = Drawing.Color.Red
                        If .Item("codigo_test") <> 2 Then 'ACTIVAR PARA PROGRAMAS A PESAR QUE NO HAYA HORARIOS
                            e.Row.Cells(4).Enabled = True
                        Else
                            e.Row.Cells(4).Enabled = False
                        End If

                    End If
                    VacantesDisponibles = (CInt(.Item("vacantes_cup")) - CInt(.Item("nroMatriculados"))).ToString
                    e.Row.Cells(2).Text = .Item("docente") & "<br> Inicio: " & .Item("fechainicio_cup") & " Fin " & .Item("fechafin_cup") & FilaAnterior
                    e.Row.Cells(4).Enabled = True
                    If .Item("EsCursomatriculado") = 0 Then
                        If VacantesDisponibles <= 0 Then
                            VacantesDisponibles = "[GRUPO CERRADO]"
                            e.Row.Cells(4).Enabled = False
                        Else
                            VacantesDisponibles = VacantesDisponibles & " vacantes disponibles"
                            e.Row.Cells(4).Enabled = True
                        End If
                        e.Row.Cells(3).Text = VacantesDisponibles
                    End If

                Else
                    e.Row.Cells(0).CssClass = "Borrarlineasuperior"
                    e.Row.Cells(1).CssClass = "Borrarlineasuperior"
                    e.Row.Cells(2).CssClass = "Borrarlineasuperior"
                    e.Row.Cells(3).CssClass = "Borrarlineasuperior"
                    e.Row.Cells(4).CssClass = "Borrarlineasuperior"
                    e.Row.Cells(0).Text = ""
                    e.Row.Cells(1).Text = "- " & ConvDia(.Item("dia_Lho")) & " " & .Item("nombre_hor") & "-" & .Item("horafin_Lho") & _
                                          "<br>&nbsp;&nbsp;" & .Item("ambiente") & "(Hrs. " & .Item("tipohoracur_lho") & ")"
                    e.Row.Cells(2).Text = ""
                    e.Row.Cells(3).Text = ""
                    e.Row.Cells(4).Text = ""
                End If


            End With
        End If
    End Sub

    Private Function ConvDia(ByVal digitos As String) As String
        Dim Dia As String = ""
        Select Case digitos
            Case "LU"
                Dia = "Lunes"
            Case "MA"
                Dia = "Martes"
            Case "MI"
                Dia = "Miércoles"
            Case "JU"
                Dia = "Jueves"
            Case "VI"
                Dia = "Viernes"
            Case "SA"
                Dia = "Sábado"
            Case "DO"
                Dia = "Domingo"
        End Select
        Return Dia
    End Function


    Protected Sub gvDetalleMatricula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDetalleMatricula.SelectedIndexChanged
        Try
            Dim permitirRetiro As String
            permitirRetiro = Me.gvDetalleMatricula.DataKeys.Item(gvDetalleMatricula.SelectedIndex).Values(2).ToString()

            If Me.gvDetalleMatricula.SelectedRow.Cells(7).Text.Trim <> "Retirado" Then

                Dim ObjCnx As New ClsConectarDatos
                Dim codigo_cup As Integer
                Dim datos As Data.DataTable
                Me.gvHorario.Visible = True
                codigo_cup = Me.gvDetalleMatricula.DataKeys.Item(gvDetalleMatricula.SelectedIndex).Value
                '### Consultar Cursos programados para cambio de grupo excepto el que está matriculado el estudiante actualmente ###

                ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                ObjCnx.AbrirConexion()
                datos = ObjCnx.TraerDataTable("MAT_ConsultarCursoProgramadoCambioGrupo", hddCodigo_alu.Value, codigo_cup, hddCodigo_pes.Value)

                'gvHorario.DataSource = datos
                'gvHorario.DataBind()
                ObjCnx.CerrarConexion()

                Dim datHorarios As New Data.DataTable

                Dim Col1 As New Data.DataColumn("codigo_cup", Type.GetType("System.Int32"))
                Dim Col2 As New Data.DataColumn("Descripcion", Type.GetType("System.String"))

                datHorarios.Columns.Add(Col1)
                datHorarios.Columns.Add(Col2)
                FilaAnterior = 0
                Me.lblMensaje.Text = ""

                Dim Descripcion As String = ""
                Dim Horarios As String = ""
                For i As Int16 = 0 To datos.Rows.Count - 2

                    If datos.Rows(i).Item("codigo_cup") <> datos.Rows(i + 1).Item("codigo_cup") Then
                        With datos.Rows(i)
                            Dim VacantesDisponibles As Int32

                            VacantesDisponibles = (CInt(.Item("vacantes_cup")) - CInt(.Item("nroMatriculados"))).ToString

                            If IIf(.Item("dia_lho") Is DBNull.Value, "", .Item("dia_lho")) <> "" Or .Item("dia_lho") IsNot DBNull.Value Then
                                Horarios = Horarios & IIf(Horarios = "", "", " || ") & ConvDia(.Item("dia_Lho")) & " " & .Item("nombre_hor") & "-" & .Item("horafin_Lho") & " " & .Item("ambiente") & "(Hrs. " & .Item("tipohoracur_lho") & ")"
                                Descripcion = "<b>(" & .Item("grupohor_cup") & ") </b>" & _
                                              IIf(VacantesDisponibles <= 0 Or .Item("estado_cup") = 0, "<font color='red'>[GRUPO CERRADO]</font><br>", "<font color='blue'>" & VacantesDisponibles & " vacantes disponibles </font><br>") & _
                                              .Item("docente") & "<br> Inicio: " & .Item("fechainicio_cup") & " Fin " & .Item("fechafin_cup") & _
                                              "<br>" & Horarios & IIf(.Item("soloPrimerCiclo_cup") = True, "<font color='green'> [Exclusivo 1er ciclo] </font>", "")


                            Else
                                Horarios = Horarios & IIf(Horarios = "", "", " || ") & " [No hay horario registrado]"
                                Descripcion = "<b>(" & .Item("grupohor_cup") & ") </b>" & _
                                              IIf(VacantesDisponibles <= 0 Or .Item("estado_cup") = 0, "<font color='red'>[GRUPO CERRADO]</font><br>", "<font color='blue'>" & VacantesDisponibles & " vacantes disponibles </font><br>") & _
                                              .Item("docente") & "<br> Inicio: " & .Item("fechainicio_cup") & " Fin " & .Item("fechafin_cup") & _
                                              Horarios & IIf(.Item("soloPrimerCiclo_cup") = True, "<font color='green'> [Exclusivo 1er ciclo] </font>", "")
                            End If

                            Dim Fila As Data.DataRow = datHorarios.NewRow
                            Fila.Item("codigo_cup") = IIf(VacantesDisponibles <= 0 Or .Item("estado_cup") = 0, datos.Rows(i).Item("codigo_cup") * -1, datos.Rows(i).Item("codigo_cup")) 'datos.Rows(i).Item("codigo_cup")
                            Fila.Item("Descripcion") = Descripcion
                            datHorarios.Rows.Add(Fila)
                            FilaAnterior = datos.Rows(i).Item("codigo_cup")
                            Horarios = ""
                        End With
                    Else
                        With datos.Rows(i)

                            If IIf(.Item("dia_lho") Is DBNull.Value, "", .Item("dia_lho")) <> "" Or .Item("dia_lho") IsNot DBNull.Value Then
                                Horarios = Horarios & IIf(Horarios = "", "", " || ") & ConvDia(.Item("dia_Lho")) & " " & .Item("nombre_hor") & "-" & .Item("horafin_Lho") & " " & .Item("ambiente") & "(Hrs. " & .Item("tipohoracur_lho") & ")"
                            Else
                                Horarios = Horarios & IIf(Horarios = "", "", " || ") & " [No hay horario registrado]"
                            End If
                        End With
                    End If
                Next

                If datos.Rows.Count > 0 Then

                    If FilaAnterior <> datos.Rows(datos.Rows.Count - 1).Item("codigo_cup") Then
                        'Descripcion = datos.Rows(datos.Rows.Count - 1).Item("grupohor_cup")
                        'Dim Fila As Data.DataRow = datHorarios.NewRow
                        'Fila.Item("codigo_cup") = FilaAnterior
                        'Fila.Item("Descripcion") = Descripcion
                        'datHorarios.Rows.Add(Fila)

                        With datos.Rows(datos.Rows.Count - 1)
                            Dim VacantesDisponibles As Int32

                            VacantesDisponibles = (CInt(.Item("vacantes_cup")) - CInt(.Item("nroMatriculados"))).ToString

                            If IIf(.Item("dia_lho") Is DBNull.Value, "", .Item("dia_lho")) <> "" Or .Item("dia_lho") IsNot DBNull.Value Then
                                Horarios = Horarios & IIf(Horarios = "", "", " || ") & ConvDia(.Item("dia_Lho")) & " " & .Item("nombre_hor") & "-" & .Item("horafin_Lho") & " " & .Item("ambiente") & "(Hrs. " & .Item("tipohoracur_lho") & ")"
                                Descripcion = "<b>(" & .Item("grupohor_cup") & ") </b>" & _
                                              IIf(VacantesDisponibles <= 0 Or .Item("estado_cup") = 0, "<font color='red'>[GRUPO CERRADO]</font><br>", "<font color='blue'>" & VacantesDisponibles & " vacantes disponibles </font><br>") & _
                                              .Item("docente") & "<br> Inicio: " & .Item("fechainicio_cup") & " Fin " & .Item("fechafin_cup") & _
                                              "<br> " & Horarios & IIf(.Item("soloPrimerCiclo_cup") = True, "<font color='green'> [Exclusivo 1er ciclo] </font>", "")


                            Else
                                Horarios = Horarios & IIf(Horarios = "", "", " || ") & " [No hay horario registrado]"
                                Descripcion = "<b>(" & .Item("grupohor_cup") & ") </b>" & _
                                              IIf(VacantesDisponibles <= 0 Or .Item("estado_cup") = 0, "<font color='red'>[GRUPO CERRADO]</font><br>", "<font color='blue'>" & VacantesDisponibles & " vacantes disponibles </font><br>") & _
                                              .Item("docente") & "<br> Inicio: " & .Item("fechainicio_cup") & " Fin " & .Item("fechafin_cup") & _
                                              Horarios & IIf(.Item("soloPrimerCiclo_cup") = True, "<font color='green'> [Exclusivo 1er ciclo] </font>", "")

                            End If

                            Dim Fila As Data.DataRow = datHorarios.NewRow
                            Fila.Item("codigo_cup") = IIf(VacantesDisponibles <= 0 Or .Item("estado_cup") = 0, .Item("codigo_cup") * -1, .Item("codigo_cup"))
                            Fila.Item("Descripcion") = Descripcion

                            datHorarios.Rows.Add(Fila)

                        End With
                    Else
                        'Descripcion = Descripcion + datos.Rows(datos.Rows.Count - 1).Item("grupohor_cup")
                        'Dim Fila As Data.DataRow = datHorarios.NewRow
                        'Fila.Item("codigo_cup") = FilaAnterior
                        'Fila.Item("Descripcion") = Descripcion
                        'datHorarios.Rows.Add(Fila)

                        With datos.Rows(datos.Rows.Count - 1)
                            Dim VacantesDisponibles As Int32

                            VacantesDisponibles = (CInt(.Item("vacantes_cup")) - CInt(.Item("nroMatriculados"))).ToString

                            If IIf(.Item("dia_lho") Is DBNull.Value, "", .Item("dia_lho")) <> "" Or .Item("dia_lho") IsNot DBNull.Value Then
                                Horarios = Horarios & IIf(Horarios = "", "", " || ") & ConvDia(.Item("dia_Lho")) & " " & .Item("nombre_hor") & "-" & .Item("horafin_Lho") & " " & .Item("ambiente") & "(Hrs. " & .Item("tipohoracur_lho") & ")"
                                Descripcion = "(" & .Item("grupohor_cup") & ") " & _
                                              IIf(VacantesDisponibles <= 0 Or .Item("estado_cup") = 0, "<font color='red'>[GRUPO CERRADO]</font><br>", "<font color='blue'>" & VacantesDisponibles & " vacantes disponibles </font><br>") & _
                                              .Item("docente") & "<br> Inicio: " & .Item("fechainicio_cup") & " Fin " & .Item("fechafin_cup") & _
                                              "<br>" & Horarios & IIf(.Item("soloPrimerCiclo_cup") = True, "<font color='green'> [Exclusivo 1er ciclo] </font>", "")


                            Else
                                Horarios = Horarios & IIf(Horarios = "", "", " || ") & " [No hay horario registrado]"
                                Descripcion = "(" & .Item("grupohor_cup") & ") " & _
                                              IIf(VacantesDisponibles <= 0 Or .Item("estado_cup") = 0, "<font color='red'>[GRUPO CERRADO]</font><br>", "<font color='blue'>" & VacantesDisponibles & " vacantes disponibles </font><br>") & _
                                              .Item("docente") & "<br> Inicio: " & .Item("fechainicio_cup") & " Fin " & .Item("fechafin_cup") & _
                                              Horarios & IIf(.Item("soloPrimerCiclo_cup") = True, "<font color='green'> [Exclusivo 1er ciclo] </font>", "")
                            End If

                            Dim Fila As Data.DataRow = datHorarios.NewRow
                            Fila.Item("codigo_cup") = IIf(VacantesDisponibles <= 0 Or .Item("estado_cup") = 0, FilaAnterior * -1, FilaAnterior)
                            Fila.Item("Descripcion") = Descripcion
                            datHorarios.Rows.Add(Fila)

                        End With
                    End If
                End If

                ClsFunciones.LlenarListas(rblHorarios, datHorarios, "codigo_cup", "Descripcion")
                If hddDisponible.Value = True Then
                    Me.lblMensaje.Font.Bold = False
                    If rblHorarios.Items.Count > 0 Then
                        cmdGuardar.Enabled = True
                        Me.lblDatos.Text = ""
                    Else
                        cmdGuardar.Enabled = False
                        Me.lblDatos.Text = "No hay mas grupos"
                        Me.lblDatos.ForeColor = Drawing.Color.Red
                    End If
                Else
                    lblMensaje.Text = "La fecha para cambio de grupos a finalizado, verifique el cronograma académico"
                    lblMensaje.ForeColor = Drawing.Color.Red
                    cmdGuardar.Enabled = False
                    Me.lblMensaje.Font.Bold = True
                End If
            Else
                rblHorarios.Items.Clear()
                Me.lblDatos.Text = "Curso bloqueado para cambio de grupo"
                Me.lblDatos.ForeColor = Drawing.Color.Red
                cmdGuardar.Enabled = False
            End If
        Catch ex As Exception
            lblMensaje.Text = ex.Message
        End Try
    End Sub

    
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        If rblHorarios.Items.Count > 0 Then
            Dim ObjCnx As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim codigo_cup_ant, codigo_dma, codigo_mar As Integer
            Dim mensajes(1) As String

            codigo_cup_ant = Me.gvDetalleMatricula.DataKeys.Item(gvDetalleMatricula.SelectedIndex).Values(0).ToString
            codigo_dma = CInt(Me.gvDetalleMatricula.DataKeys.Item(gvDetalleMatricula.SelectedIndex).Values(1).ToString)
            codigo_mar = 1

            '### Graba el cambio de grupo, internamente está la bitacora ###
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            dt = ObjCnx.TraerDataTable("ACAD_ValidaCruceHorarioCambioGrupo", Me.rblHorarios.SelectedValue, codigo_cup_ant, codigo_dma)
            ObjCnx.CerrarConexion()
            If (dt.Rows.Count > 0) Then 'Existe cruce
                Response.Write("<script>alert('Existe cruce de horario')</script>")
            Else
                ObjCnx.AbrirConexion()
                'ObjCnx.Ejecutar("MAT_CambiarDeGrupo", Me.rblHorarios.SelectedValue, Request.ServerVariables("REMOTE_ADDR"), Session("id_per"), codigo_dma, codigo_cup_ant, Me.gvDetalleMatricula.SelectedRow.Cells(1).Text.Trim, "").copyto(mensajes, 0)
                ObjCnx.Ejecutar("MAT_CambiarDeGrupoAsesor", Me.rblHorarios.SelectedValue, Request.ServerVariables("REMOTE_ADDR"), Session("id_per"), codigo_dma, codigo_cup_ant, Me.gvDetalleMatricula.SelectedRow.Cells(1).Text.Trim, "ADM", codigo_mar, "").copyto(mensajes, 0)
                ObjCnx.CerrarConexion()
                Select Case (Left(mensajes(0), 1))
                    Case 1, 3, 4 ' 1: ocurrio error, 3: no hay vacantes
                        lblMensaje.ForeColor = Drawing.Color.Red
                    Case 2 ' 2: registrado correctamente
                        lblMensaje.ForeColor = Drawing.Color.Blue
                End Select

                lblMensaje.Text = Mid(mensajes(0), 2, mensajes(0).Length)
                ConsultarCursosMatriculados()
                Me.rblHorarios.Items.Clear()
            End If
            
        End If
    End Sub



End Class
﻿'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class academico_matricula_administrar_frmCambioDeGrupo
    Inherits System.Web.UI.Page
    Dim FilaAnterior As Int32


    Private Sub ConsultarCronograma()
        Dim ObjCnx As New ClsConectarDatos
        Dim datos As Data.DataTable
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        '### Consulta cronograma para cambio de grupo ###
        'Tipo = "GE" es cambio de grupo por evaluacion y registro
        ObjCnx.AbrirConexion()
        datos = ObjCnx.TraerDataTable("ACAD_ConsultarCronograma", "CGALU", Me.hddCac.Value, 2)
        ObjCnx.CerrarConexion()
        If datos.Rows.Count > 0 Then
            hddDisponible.Value = True
        Else
            hddDisponible.Value = False
            lblMensaje.Text = "La fecha para cambio de grupos a finalizado, verifique el cronograma académico"
        End If
    End Sub

    Sub ConsultarCursosMatriculados(ByVal codUniversitario As String)
        Dim ObjCnx As New ClsConectarDatos
        Dim datosAlu, datosCursos As New Data.DataTable
        Dim Ruta As New EncriptaCodigos.clsEncripta
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        '### Consulta datos del alumno ###
        ObjCnx.AbrirConexion()
        'datosAlu = ObjCnx.TraerDataTable("dbo.SOL_ConsultarAlumnoParaSolicitud", 1, Me.txtCodUniversitario.Text)
        datosAlu = ObjCnx.TraerDataTable("EVE_ConsultarAlumnoParaCambioDeGrupo", codUniversitario)
        ObjCnx.CerrarConexion()

        ImgFoto.Width = 80
        ImgFoto.BorderColor = Drawing.Color.Black
        ImgFoto.BorderWidth = 1
        If datosAlu.Rows.Count > 0 Then
            With datosAlu.Rows(0)
                'Me.ImgFoto.ImageUrl = "http://www.usat.edu.pe/imgestudiantes/" & Ruta.CodificaWeb("069" & .Item("codigouniver_alu").ToString)
                Me.ImgFoto.ImageUrl = "//intranet.usat.edu.pe/imgestudiantes/" & Ruta.CodificaWeb("069" & .Item("codigouniver_alu").ToString)
                hddCodigo_alu.Value = .Item("codigo_alu")
                hddCodigo_pes.Value = .Item("codigo_pes")
                If datosAlu.Rows.Count > 0 Then
                    MostrarDatos(True)
                    FvDatos.DataSource = datosAlu
                    FvDatos.DataBind()
                    ObjCnx.AbrirConexion()
                    datosCursos = ObjCnx.TraerDataTable("ACAD_ConsultarCursosMatriculadosPorAlumno", .Item("codigo_alu"), .Item("codigo_pes"), hddCac.Value)
                    ObjCnx.CerrarConexion()
                    If datosCursos.Rows.Count > 0 Then
                        If datosCursos.Select("PermitirRetiro = 'S'").Length = 0 Then
                            hddDisponible.Value = False
                            Me.lblMensaje.Text = "Deberá esperar al día de mañana para realizar un nuevo cambio de grupo, debido a que ha completado el límite de cambios por día"
                            Me.lblMensaje.ForeColor = Drawing.Color.Red
                        End If
                        gvDetalleMatricula.DataSource = datosCursos
                    Else
                        gvDetalleMatricula.DataSource = Nothing
                    End If
                    gvDetalleMatricula.DataBind()
                Else
                    Me.pnlDatos.Visible = False
                End If
                Me.ImgFoto.AlternateText = .Item("codigouniver_alu")
            End With
        Else
            '### no hay datos de alumnos ###
            ClientScript.RegisterStartupScript(Me.GetType, "alumno", "alert('Esta opción está habilitada sólo para pre-grado')", True)
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
        Try
            If (Session("codigo_alu") IsNot Nothing) Then
                If Not IsPostBack Then
                    Dim ObjCnx As New ClsConectarDatos
                    'Dim ObjCif As New PryCifradoNet.ClsCifradoNet
                    Dim codUniver As String
                    Dim dt As New Data.DataTable
                    Dim CodAlu As Integer

                    ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    ObjCnx.AbrirConexion()
                    dt = ObjCnx.TraerDataTable("ACAD_RetornaCodUniversitario", Session("codigo_alu"))
                    ObjCnx.CerrarConexion()
                    CodAlu = Session("codigo_alu")
                    hdCodUniversitario.Value = dt.Rows(0).Item("codigoUniver_Alu").ToString
                    codUniver = hdCodUniversitario.Value

                    MostrarDatos(False)
                    ObjCnx.AbrirConexion()
                    hddCac.Value = ObjCnx.TraerDataTable("ConsultarCicloAcademico", "CV", 1).Rows(0).Item("codigo_cac")
                    ObjCnx.CerrarConexion()
                    ObjCnx = Nothing
                    FilaAnterior = 0 ' tomo el primer valor
                    Me.lblMensaje.Text = ""
                    Me.lblDatos.Text = ""
                    Me.rblHorarios.Items.Clear()

                    ConsultarCronograma()
                    '### Si la fecha de cronograma ya venció no se permitirá cambio de grupos ###
                    If hddDisponible.Value = False Then
                        Me.cmdGuardar.Enabled = hddDisponible.Value
                        lblMensaje.ForeColor = Drawing.Color.Red
                        lblMensaje.Font.Bold = True
                        cmdGuardar.Enabled = False
                    End If

                    If (dt.Rows(0).Item("CPF") = 31 Or dt.Rows(0).Item("CPF") = 24) Then
                        hddDisponible.Value = False
                        lblMensaje.Text = "No puede realizar cambio de grupo por este medio"
                        lblMensaje.Font.Bold = True
                        lblMensaje.Font.Size = 12
                        rest.Visible = False

                    Else
                        ConsultarCursosMatriculados(codUniver)
                    End If
                End If
            Else
                Response.Redirect("../../ErrorSistema.aspx")
            End If
        Catch ex As Exception
            Response.Write("Ocurrió un error: " & ex.Message & " -" & ex.StackTrace)
        End Try
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
                e.Row.Cells(8).Enabled = False
                If fila.Row.Item("estado_dma").ToString = "R" Then
                    e.Row.Cells(7).ForeColor = Drawing.Color.Red
                Else
                    e.Row.Cells(7).ForeColor = Drawing.Color.Blue
                End If
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

            If Me.gvDetalleMatricula.SelectedRow.Cells(7).Text.Trim <> "Retirado" And permitirRetiro = "S" Then

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
            lblMensaje.Text = ex.Message & " -" & ex.StackTrace
        End Try
    End Sub


    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim dtCruce As New Data.DataTable
        Try
            If rblHorarios.Items.Count > 0 Then
                Dim ObjCnx As New ClsConectarDatos
                Dim codigo_cup_ant, codigo_dma As Integer
                Dim mensajes(1) As String

                'Dim ObjCif As New PryCifradoNet.ClsCifradoNet
                Dim codUniver, CodAlu As String


                codUniver = hdCodUniversitario.Value.Trim
                CodAlu = Session("codigo_alu")

                'codUniver = ObjCif.DesCifrado(CadReci.Substring(16, 16), CadReci.Substring(0, 16)).ToString.Substring(6, 10)
                'CodAlu = ObjCif.DesCifrado(AluReci.Substring(16, 16), AluReci.Substring(0, 16)).ToString.Substring(6, 10)

                codigo_cup_ant = Me.gvDetalleMatricula.DataKeys.Item(gvDetalleMatricula.SelectedIndex).Values(0).ToString
                codigo_dma = CInt(Me.gvDetalleMatricula.DataKeys.Item(gvDetalleMatricula.SelectedIndex).Values(1).ToString)


                ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                ObjCnx.AbrirConexion()
                dtCruce = ObjCnx.TraerDataTable("ACAD_ValidaCruceHorarioCambioGrupo", Me.rblHorarios.SelectedValue, codigo_cup_ant, codigo_dma)
                ObjCnx.CerrarConexion()

                If (dtCruce.Rows.Count > 0) Then 'Existe cruce
                    Response.Write("<script>alert('Existe cruce de horario')</script>")
                Else
                    '### Graba el cambio de grupo, internamente está la bitacora ###
                    ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    ObjCnx.AbrirConexion()
                    ObjCnx.Ejecutar("MAT_CambiarDeGrupo", Me.rblHorarios.SelectedValue, Request.ServerVariables("REMOTE_ADDR"), CodAlu, codigo_dma, codigo_cup_ant, Me.gvDetalleMatricula.SelectedRow.Cells(1).Text.Trim, "").copyto(mensajes, 0)
                    ObjCnx.CerrarConexion()
                    Select Case (Left(mensajes(0), 1))
                        Case 1, 3, 4 ' 1: ocurrio error, 3: no hay vacantes
                            lblMensaje.ForeColor = Drawing.Color.Red
                        Case 2 ' 2: registrado correctamente
                            lblMensaje.ForeColor = Drawing.Color.Blue
                    End Select

                    lblMensaje.Text = Mid(mensajes(0), 2, mensajes(0).Length)
                    ConsultarCursosMatriculados(codUniver)
                    Me.rblHorarios.Items.Clear()
                    Response.Write("<script>alert('Cambio de grupo realizado')</script>")
                End If
            End If
        Catch ex As Exception
            Response.Write("Error al guardar: " & ex.Message & " -" & ex.StackTrace)
        End Try
    End Sub



End Class
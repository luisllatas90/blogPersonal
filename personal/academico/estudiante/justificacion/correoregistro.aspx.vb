﻿
Partial Class academico_estudiante_separacion_Separacion
    Inherits System.Web.UI.Page


    Public Enum MessageType
        Success
        Errors
        Info
        Warning
    End Enum

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub
 

    Protected Sub GvAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvAlumnos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem
            'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            'e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('GvAlumnos','Select$" & e.Row.RowIndex & "')")

            If Not Request.QueryString("ctf") = 1 Then
                'e.Row.Cells(12).Text = ""
                Me.GvAlumnos.Columns(12).Visible = False
            Else
                Me.GvAlumnos.Columns(12).Visible = True

            End If
        End If
    End Sub

    Protected Sub GvAlumnos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvAlumnos.SelectedIndexChanged
        Session("codigo_alu") = 0
        Session("codigo_trl") = 0
        Me.gridSesiones.DataSource = Nothing
        Me.gridSesiones.DataBind()
        Me.txtDescripcion.Text = ""
        Me.lblMotivo.Text = ""
        Me.txtObs.Text = ""

        Dim objCnx As New ClsConectarDatos
        Dim datos As New Data.DataTable, datostramite As New Data.DataTable
        Dim codigo_alu As Int32, codigo_pes As Int16
        Dim Ruta As New EncriptaCodigos.clsEncripta

        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        codigo_alu = Me.GvAlumnos.DataKeys.Item(Me.GvAlumnos.SelectedIndex).Values(0).ToString()
        Session("codigo_alu") = codigo_alu
        codigo_pes = Me.GvAlumnos.DataKeys.Item(Me.GvAlumnos.SelectedIndex).Values(1).ToString()
        objCnx.AbrirConexion()
        datos = objCnx.TraerDataTable("GyT_ConsultarPlanEstudioMatricula_V2", "PE", codigo_alu, codigo_pes)

        datostramite = objCnx.TraerDataTable("TRL_TramiteAdicionalInfo", 1, Me.GvAlumnos.DataKeys.Item(Me.GvAlumnos.SelectedIndex).Values("codigo_trl").ToString(), Me.GvAlumnos.DataKeys.Item(Me.GvAlumnos.SelectedIndex).Values("codigo_dta").ToString())

        Session("codigo_trl") = Me.GvAlumnos.DataKeys.Item(Me.GvAlumnos.SelectedIndex).Values("codigo_trl").ToString()

        objCnx.CerrarConexion()

        Me.txtDesde.Text = Me.GvAlumnos.DataKeys.Item(Me.GvAlumnos.SelectedIndex).Values("fechaIni").ToString()
        Me.txtHasta.Text = Me.GvAlumnos.DataKeys.Item(Me.GvAlumnos.SelectedIndex).Values("fechaFin").ToString()

        If datos.Rows.Count > 0 Then
            With datos.Rows(0)

                Me.LblCodigoUniv.Text = .Item("codigouniver_alu").ToString
                Me.LblNombres.Text = .Item("nombres").ToString
                Me.lblPlanEstudio.Text = .Item("descripcion_pes").ToString
                Me.LblEstado.Text = Me.GvAlumnos.SelectedRow.Cells(4).Text
                Me.txtDescripcion.Text = Me.GvAlumnos.DataKeys.Item(Me.GvAlumnos.SelectedIndex).Values("observacion_trl").ToString()
                Me.LblEstado.ForeColor = IIf(Me.GvAlumnos.SelectedRow.Cells(4).Text = "Activo", Drawing.Color.Blue, Drawing.Color.Red)
                If .Item("foto_alu") = 1 Then
                    ImgFoto.ImageUrl = "//intranet.usat.edu.pe/imgestudiantes/" & Ruta.CodificaWeb("069" & .Item("codigouniver_alu").ToString)
                Else
                    ImgFoto.ImageUrl = Request.ApplicationPath & "/images/Sin_foto.jpg"
                End If

            End With


            With datostramite
                For i As Integer = 0 To datostramite.Rows.Count - 1
                    If datostramite.Rows(i).Item("tabla").ToString = "MOTIVOS" Then

                        'cambiar por campo que defina el motivo

                        Select Case datostramite.Rows(i).Item("VALORCAMPO").ToString
                            Case "Por muerte de un familiar hasta el cuarto grado de consanguinidad y segundo de afinidad, o la muerte del cónyuge para lo cual deberá presentar Certificado de Defunción correspondiente, acreditando la relación de parentesco."
                                hdMotivo.Value = "MUERTE DE UN FAMILIAR"
                            Case "Por razones de salud del estudiante, debiendo acreditar dicha situación mediante certificado médico expedido por un profesional particular o un establecimiento de salud, debidamente visado por el Ministerio de Salud o EsSalud, en ambos casos. Si se trata de consulta o atención médica, bastará con la presentación de la boleta de venta o ticket de atención y las indicaciones o recetas médicas."
                                hdMotivo.Value = "SALUD"
                            Case "Por citación judicial, debiendo acreditar dicha situación mediante copia legalizada notarialmente del documento correspondiente."
                                hdMotivo.Value = "CITACIÓN JUDICIAL"
                            Case "Por participación del estudiante en algún certamen académico, deportivo, religioso o de cualquier otra índole que implique representación de la Facultad o de la Universidad."
                                hdMotivo.Value = "REPRESENTACIÓN DE LA FACULTAD / UNIVERSIDAD"
                            Case Else
                                hdMotivo.Value = "-"
                        End Select

                        Me.lblMotivo.Text = hdMotivo.Value 'datostramite.Rows(i).Item("VALORCAMPO").ToString '"Por razones de salud del estudiante" 'datostramite.Rows(i).Item("VALORCAMPO").ToString

                    ElseIf datostramite.Rows(i).Item("tabla").ToString = "OBSERVACION" Then
                        Me.txtObs.Text = datostramite.Rows(i).Item("VALORCAMPO").ToString
                    End If
                Next


            End With

            If Me.GvAlumnos.SelectedRow.Cells(10).Text = "REGISTRADA" Then
                Me.btnRegistrar.Enabled = False

            Else
                Me.btnRegistrar.Enabled = True
                Me.gridSesiones.Enabled = True
            End If

        Else

        End If


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If Not IsPostBack Then
            CargarTramites()

        End If
    End Sub

    Sub CargarTramites()
        Dim objCnx As New ClsConectarDatos
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objCnx.AbrirConexion()
        Dim datos As New Data.DataTable
        datos = objCnx.TraerDataTable("Alumno_ListarTramiteJusti", CInt(Session("id_per")), CInt(Request.QueryString("ctf")), Request.QueryString("mod"))
        Me.GvAlumnos.DataSource = datos
        Me.GvAlumnos.DataBind()
        objCnx.CerrarConexion()
    End Sub
  
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim objCnx As New ClsConectarDatos
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim datos As New Data.DataTable
        objCnx.AbrirConexion()
        
        datos = objCnx.TraerDataTable("Alumno_ListarHorarioSesiones", Session("codigo_alu"), Me.txtDesde.Text, Me.txtHasta.Text)
        objCnx.CerrarConexion()

        Me.gridSesiones.DataSource = datos
        Me.gridSesiones.databind()
    End Sub

    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar.Click
        Dim Fila As GridViewRow
        Dim dt As New Data.DataTable
        Dim codigo_alu As Integer, codigo_cac As Integer, codigo_cup As Integer, codigo_lho As Integer, codigo_trl As Integer
        Dim fechaclase As Date

        Dim objCnx As New ClsConectarDatos
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim sw As Byte = 0
        Try
            For I As Int16 = 0 To Me.gridSesiones.Rows.Count - 1
                Fila = Me.gridSesiones.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        sw = 1
                    End If
                End If
            Next

            If (sw = 0) Then
                ShowMessage("Debe seleccionar algun registro.", MessageType.Warning)
                Exit Sub
            End If

            Dim njust As Integer = 0

            For I As Int16 = 0 To Me.gridSesiones.Rows.Count - 1
                Fila = Me.gridSesiones.Rows(I)
                ' Response.Write(I.ToString)
                If Fila.RowType = DataControlRowType.DataRow Then

                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        njust = njust + 1
                        codigo_alu = Session("codigo_alu") 'Me.GvAlumnos.DataKeys.Item(Me.GvAlumnos.SelectedIndex).Values(0).ToString()
                        codigo_cac = gridSesiones.DataKeys(Fila.RowIndex).Values("codigo_cac")
                        codigo_cup = gridSesiones.DataKeys(Fila.RowIndex).Values("codigo_cup")
                        codigo_lho = gridSesiones.DataKeys(Fila.RowIndex).Values("codigo_lho")
                        fechaclase = gridSesiones.DataKeys(Fila.RowIndex).Values("fechaclase")
                        codigo_trl = GvAlumnos.DataKeys(Me.GvAlumnos.SelectedRow.RowIndex).Values("codigo_trl")
                        objCnx.AbrirConexion()
                        objCnx.Ejecutar("Alumno_RegistrarJustificacionAlumno", codigo_alu, codigo_cac, codigo_cup, codigo_lho, fechaclase, CInt(Session("id_per")), codigo_trl)
                        objCnx.CerrarConexion()
                    End If
                End If
            Next

            If njust > 1 Then
                'ShowMessage("Se han procesado " & njust.ToString & " justificaciones. Se envió notificación por correo.", MessageType.Success)
                ShowMessage("Se ha registrado justificación de inasistencia para  " & njust.ToString & " sesiones de clase. Se envió notificación por correo.", MessageType.Success)

            ElseIf njust = 1 Then
                'ShowMessage("Se han procesado " & njust.ToString & " justificación. Se envió notificación por correo.", MessageType.Success)
                ShowMessage("Se ha registrado justificación de inasistencia para  " & njust.ToString & " sesión de clase. Se envió notificación por correo.", MessageType.Success)
            Else
                ShowMessage("No se han procesado justificaciones", MessageType.Warning)
            End If

            If njust > 0 Then
                Call EnviarCorreo(codigo_trl)
            End If

            btnBuscar_Click(sender, e)
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Warning)
        End Try
    End Sub
    Function EnviarCorreo(ByVal codigo_trl As Integer) As Boolean
        Dim CorreoReply, CorreoPara, CorreoCopia As String
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tbDocentes, tbCursos As New Data.DataTable
        obj.AbrirConexion()
        tbDocentes = obj.TraerDataTable("Alumno_JustificacionCorreo", codigo_trl, "D", 0)
        obj.CerrarConexion()

        Dim objCorreo As New ClsMail
        Dim bodycorreo As String

        If tbDocentes.Rows.Count Then

            For j As Integer = 0 To tbDocentes.Rows.Count - 1
                bodycorreo = "<html>"
                bodycorreo = bodycorreo & "<body style=""font-size:12px;text-align:justify; font-family:Tahoma,Verdana,Segoe,sans-serif;""> <div style=""color:#284775; Background-color:white; border-color:#284775; border:1px solid; padding:10px;"">"
                bodycorreo = bodycorreo & "<table>"
                bodycorreo = bodycorreo & "<tr><td>Estimado Docente reciba un saludo cordial.</td></tr>"
                bodycorreo = bodycorreo & "<tr><td>El estudiante " & tbDocentes.Rows(0).Item("Estudiante") & " ha presentado la solicitud N°" & tbDocentes.Rows(0).Item("solicitud") & " de justificación</td></tr>"
                bodycorreo = bodycorreo & "<tr><td>de inasistencias por motivos de " & hdMotivo.Value & " del estudiante adjuntando los documentos necesarios,</td></tr>"
                bodycorreo = bodycorreo & "<tr><td>los cuales han <b><u>sido evaluados.</b></u> Por lo cual, la <b><u></u> solicitud ha sido APROBADA</b></td></tr>"
                bodycorreo = bodycorreo & "<tr><td>por Dirección de Escuela.</td></tr>"
                bodycorreo = bodycorreo & "<tr><td> </td></tr>"
                bodycorreo = bodycorreo & "<tr><td>La presente es para justificar la inasistencia de:</td></tr>"
                bodycorreo = bodycorreo & "<tr><td>"

                obj.AbrirConexion()
                tbCursos = obj.TraerDataTable("Alumno_JustificacionCorreo", codigo_trl, "C", tbDocentes.Rows(j).Item("codigo_Cup"))
                obj.CerrarConexion()

                If tbCursos.Rows.Count Then

                    bodycorreo = bodycorreo & "<table style=""font-size:12px;font-family:Tahoma,Verdana,Segoe,sans-serif;border:#99bae2 1px solid;"" cellSpacing=0 cellPadding=4 border=""1"">"
                    bodycorreo = bodycorreo & "<tr style=""color:  #284775; background-color:#E8EEF7; font-weight:bold;""><td>Curso</td><td>Fecha</td><td>Horario</td></tr>"
                    For i As Integer = 0 To tbCursos.Rows.Count - 1
                        bodycorreo = bodycorreo & "<tr><td>" & tbCursos.Rows(i).Item("curso") & "</td><td>" & tbCursos.Rows(i).Item("fecha") & "</td><td>" & tbCursos.Rows(i).Item("hora") & "</td></tr>"
                    Next
                    bodycorreo = bodycorreo & "</table>"
                End If
                bodycorreo = bodycorreo & "</td></tr><tr><td><br/><br/>Dirección de Escuela.</td></tr></table>"
                bodycorreo = bodycorreo & "</div></body></html>"

                If bodycorreo <> "" Then
                    Try
                        CorreoPara = tbDocentes.Rows(j).Item("correoDocente").ToString
                        CorreoCopia = tbDocentes.Rows(j).Item("correoDirector").ToString & ";" & tbDocentes.Rows(j).Item("correoEstudiante").ToString
                        CorreoReply = tbDocentes.Rows(j).Item("correoDirector").ToString
                        'objCorreo.EnviarMail("campusvirtual@usat.edu.pe", "Sistema Trámites Virtuales - Campus Virtual", CorreoPara, "Notificación: Justificación de Inasistencia", bodycorreo, True, CorreoCopia, CorreoReply)
                        objCorreo.EnviarMail("campusvirtual@usat.edu.pe", "Sistema Trámites Virtuales - Campus Virtual", "yperez@usat.edu.pe", "COPIA - Notificación: Justificación de Inasistencia", bodycorreo & " <br> correo docente: " & CorreoPara & " correocopia: " & CorreoCopia, True, "", "")
                    Catch ex As Exception
                        Response.Write("<script>alert('" & ex.Message & "')</script>")
                        Return False
                    End Try
                End If
            Next
            Return True
        End If
    End Function
    Protected Sub gridSesiones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridSesiones.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem


            If CInt(gridSesiones.DataKeys(e.Row.RowIndex).Values("codigo_justi")) > 0 Then
                e.Row.Cells(0).Text = "Justificado"
            End If

            If Me.btnRegistrar.Enabled = False And e.Row.Cells(0).Text <> "Justificado" Then
                e.Row.Cells(0).Text = ""
            End If
            
        End If
    End Sub

 
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        EnviarCorreo(Session("codigo_trl"))
    End Sub
End Class


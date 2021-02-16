Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic
Imports System.Web.Script.Serialization

Partial Class academico_estudiante_frmHistorialAcademico
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES
    Dim me_Alumno As e_Alumno

    'DATOS
    Dim md_Alumno As New d_Alumno
    Dim md_Funciones As New d_Funciones
    Dim md_Encripta As New EncriptaCodigos.clsEncripta

    'VARIABLES
    Dim cod_user As Integer = 0
    Dim cod_tfu As Integer = 0

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum
#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing OrElse Session("perlogin") Is Nothing) Then
                Response.Redirect("../../../sinacceso.html")
            End If

            cod_user = Session("id_per")
            cod_tfu = Request.QueryString("ctf")

            If IsPostBack = False Then
                Session("frmHistorialAcademico.codigo_alu") = Nothing
                Session("frmHistorialAcademico.foto_alu") = Nothing
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Call mt_LimpiarControles()
            Call mt_ConsultarAlumno()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Try
            Call mt_Exportar()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_UpdatePanel(ByVal ls_panel As String)
        Try
            Select Case ls_panel
                Case "Filtros"
                    Me.udpFiltros.Update()                    

                Case "Lista"
                    Me.udpLista.Update()                    

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControles()
        Try
            Session("frmHistorialAcademico.codigo_alu") = Nothing
            Session("frmHistorialAcademico.foto_alu") = Nothing
            Me.tblDatos.InnerHtml = String.Empty
            Me.tblHistorial.InnerHtml = String.Empty

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_ConsultarAlumno() As Boolean
        Try
            Dim ls_tabla As String = String.Empty

            If String.IsNullOrEmpty(Me.txtCodigoFiltro.Text) Then mt_ShowMessage("Debe ingresar el código universitario del estudiante.", MessageType.warning) : Me.txtCodigoFiltro.Focus() : Return False

            Dim dt As New DataTable : me_Alumno = New e_Alumno

            me_Alumno.codigoUniver_alu = Me.txtCodigoFiltro.Text.Trim

            dt = md_Alumno.ConsultarAlumnoMatricula(me_Alumno)

            If dt.Rows.Count = 0 Then mt_ShowMessage("El estudiante no existe en la base de datos del sistema.", MessageType.warning) : Me.txtCodigoFiltro.Focus() : Return False

            With dt.Rows(0)
                If mt_CargarDatos(CInt(.Item("codigo_alu")), CInt(.Item("estadodeuda_alu"))) Then
                    Session("frmHistorialAcademico.codigo_alu") = CInt(.Item("codigo_alu"))
                    Session("frmHistorialAcademico.foto_alu") = md_Encripta.CodificaWeb("069" & .Item("codigoUniver_alu"))

                    ls_tabla &= "<table class='table table-simple'>"
                    ls_tabla &= "<tr>"
                    ls_tabla &= "<td rowspan='5'><img border='0' src='//intranet.usat.edu.pe/imgestudiantes/" & md_Encripta.CodificaWeb("069" & .Item("codigoUniver_alu")) & "' width='95' height='106' alt='Sin Foto'/></td>"
                    ls_tabla &= "<td class='table-datos-1'>CÓDIGO UNIVERSITARIO:</td>"
                    ls_tabla &= "<td colspan='3' class='table-datos-2'>" & .Item("codigoUniver_alu") & "</td>"
                    ls_tabla &= "</tr>"
                    ls_tabla &= "<tr>"
                    ls_tabla &= "<td class='table-datos-1'>APELLIDOS Y NOMBRES:</td>"
                    ls_tabla &= "<td colspan='3' class='table-datos-2'>" & .Item("alumno") & "</td>"
                    ls_tabla &= "</tr>"
                    ls_tabla &= "<tr>"
                    ls_tabla &= "<td class='table-datos-1'>ESCUELA PROFESIONAL:</td>"
                    ls_tabla &= "<td colspan='3' class='table-datos-2'>" & .Item("nombre_cpf") & "</td>"
                    ls_tabla &= "</tr>"
                    ls_tabla &= "<tr>"
                    ls_tabla &= "<td class='table-datos-1'>CICLO DE INGRESO:</td>"
                    ls_tabla &= "<td class='table-datos-2'>" & .Item("cicloIng_alu") & "</td>"
                    ls_tabla &= "<td class='table-datos-1'>MODALIDAD:</td>"
                    ls_tabla &= "<td class='table-datos-2'>" & .Item("nombre_min") & "</td>"
                    ls_tabla &= "</tr>"
                    ls_tabla &= "<tr>"
                    ls_tabla &= "<td class='table-datos-1'>PLAN DE ESTUDIO:</td>"
                    ls_tabla &= "<td colspan='3' class='table-datos-2'>" & .Item("descripcion_pes") & "</td>"
                    ls_tabla &= "</tr>"
                    ls_tabla &= "</table>"
                    ls_tabla &= "<hr/>"

                    Me.tblDatos.InnerHtml = ls_tabla
                Else
                    Return False
                End If
            End With

            Call mt_UpdatePanel("Lista")

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_CargarDatos(ByVal codigo_alu As Integer, ByVal estado_deuda As Integer) As Boolean
        Try
            Dim ls_tabla As String = String.Empty
            Dim dt As New DataTable : me_Alumno = New e_Alumno

            With me_Alumno
                .operacion = "TO"
                .codigo_alu = codigo_alu
            End With

            dt = md_Alumno.ListarHistorialAcademico(me_Alumno)

            If dt.Rows.Count = 0 Then mt_ShowMessage("No se ha registrado historial académico para el estudiante.", MessageType.warning) : Me.txtCodigoFiltro.Focus() : Return False

            If estado_deuda = 1 AndAlso estado_deuda <> 1 AndAlso estado_deuda <> 9 AndAlso estado_deuda <> 16 AndAlso _
                estado_deuda <> 25 AndAlso estado_deuda <> 26 AndAlso estado_deuda <> 30 AndAlso estado_deuda <> 35 Then

                mt_ShowMessage("Lo sentimos no se puede mostrar su historial académico del estudiante, por favor comuníquese con la Oficina de Pensiones para que se le indique el motivo.", MessageType.warning)
                Me.txtCodigoFiltro.Focus() : Return False
            End If

            If dt.Rows.Count > 0 Then
                ls_tabla &= "<table class='table table-simple'>"
                ls_tabla &= "<thead>"
                ls_tabla &= "<tr>"
                ls_tabla &= "<th>SEMESTRE</th>"
                ls_tabla &= "<th>ÁREA</th>"
                ls_tabla &= "<th>CÓDIGO</th>"
                ls_tabla &= "<th>NOMBRE DEL CURSO</th>"
                ls_tabla &= "<th>CICLO</th>"
                ls_tabla &= "<th>CRÉDITOS</th>"
                ls_tabla &= "<th>GRUPO HORARIO</th>"
                ls_tabla &= "<th>VECES DESAPROB.</th>"
                ls_tabla &= "<th>PROMEDIO</th>"
                ls_tabla &= "</tr>"
                ls_tabla &= "</thead>"

                Dim total_creditos As Double = 0 : Dim nota_credito As Integer = 0
                Dim total_general As Double = 0 : Dim total_aprobados As Double = 0
                Dim nombre_curso As String = String.Empty : Dim es_convalidado As Boolean = False
                Dim es_inhabilitado As Integer = 0 : Dim semestre As String = String.Empty
                Dim total_aprob_cred As Double = 0 : Dim nota_ciclo As String = String.Empty
                Dim etiq_inhabilitado As String = String.Empty

                semestre = dt.Rows(0).Item("descripcion_cac").ToString()

                For i As Integer = 0 To dt.Rows.Count - 1
                    With dt.Rows(i)
                        es_convalidado = fu_VerCursoConvalidado(.Item("nombre_cur"), .Item("tipomatricula_dma"), nombre_curso)
                        es_inhabilitado = .Item("inhabilitado_dma")

                        If semestre <> .Item("descripcion_cac") Then
                            semestre = .Item("descripcion_cac")
                            ls_tabla &= fu_PonderadoCAC(CDbl(dt.Rows(i - 1).Item("sumaTotal_mat")), CDbl(dt.Rows(i - 1).Item("creditosTotal_mat")), dt.Rows(i - 1).Item("notaminima_cac"))

                            total_general += total_creditos
                            total_aprobados += total_aprob_cred

                            'Reinicia variables que Agrupan datos por semestre académico
                            total_creditos = 0 : nota_credito = 0 : total_aprob_cred = 0
                        End If

                        If Not (es_convalidado) Then
                            total_creditos += CDbl(.Item("creditocur_dma")) 'Sumatoria de Créditos matriculados
                            nota_credito += CDbl(.Item("notacredito"))  'Sumatorio de Nota * Crédito(Calculado)
                        End If

                        If .Item("condicion_dma") = "A" And .Item("estado_dma") <> "R" Then
                            total_aprob_cred += CDbl(.Item("creditocur_dma"))
                        End If

                        nota_ciclo = "-" : etiq_inhabilitado = String.Empty

                        If .Item("estado_dma") <> "R" Then
                            If es_inhabilitado = 0 Then '#EPENA GLPI: 41509 
                                nota_ciclo = fu_VerColorNota("I", .Item("notafinal_dma"), .Item("condicion_Dma"), False)
                            Else
                                etiq_inhabilitado = " - <font style='font-weight:bold;'>INHABILITADO</fotn>"
                            End If
                        End If

                        ls_tabla &= "<tr>"
                        ls_tabla &= "<td>" & .Item("descripcion_cac") & "</td>"
                        ls_tabla &= "<td>" & .Item("tipoCurso_Dma") & "</td>"
                        ls_tabla &= "<td>" & .Item("identificador_Cur") & "</td>"
                        ls_tabla &= "<td style='text-align: left !important;'>" & nombre_curso & etiq_inhabilitado & "</td>"
                        ls_tabla &= "<td>" & fu_ObtenerRomano(.Item("ciclo_cur")) & "</td>"
                        ls_tabla &= "<td>" & .Item("creditocur_dma") & "</td>"
                        ls_tabla &= "<td>" & .Item("grupohor_cup") & "</td>"
                        ls_tabla &= "<td>" & fu_MensajeVD(.Item("vecesCurso_Dma")) & "</td>"
                        ls_tabla &= "<td>" & nota_ciclo & "</td>"

                        If i = dt.Rows.Count - 1 Then
                            ls_tabla &= fu_PonderadoCAC(CDbl(.Item("sumaTotal_mat")), CDbl(.Item("creditosTotal_mat")), .Item("notaminima_cac"))

                            total_general += total_creditos
                            total_aprobados += total_aprob_cred
                        End If
                    End With
                Next

                ls_tabla &= "</table>"
                ls_tabla &= "<hr/>"
                ls_tabla &= "<p class='totales'>Total de Créditos Matriculados sin Convalidaciones: " & total_general & "</p>"
                ls_tabla &= "<p class='totales'>Total de Créditos Aprobados (incluye convalidación): " & total_aprobados & "</p><br/>"

                Me.tblHistorial.InnerHtml = ls_tabla
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_PonderadoCAC(ByVal nota_credito As Double, ByVal total_credito As Double, ByVal limite As String) As String
        Try
            Dim formato_ponderado As String = String.Empty

            Dim ponderado_calculado As Double = 0
            Dim ponderado As String = String.Empty

            If nota_credito <> 0 Then
                ponderado_calculado = nota_credito / total_credito
                ponderado = fu_VerColorNota("T", ponderado_calculado, limite, True)
            End If

            formato_ponderado &= "<tr>"
            formato_ponderado &= "<td colspan='5' style='text-align: right !important; font-size: 0.75rem; font-weight: bold;'>TOTAL</td>"
            formato_ponderado &= "<td style='font-size: 0.75rem; font-weight: bold;'>" & total_credito & "</td>"
            formato_ponderado &= "<td>&nbsp;</td>"
            formato_ponderado &= "<td>&nbsp;</td>"
            formato_ponderado &= "<td style='font-size: 0.75rem; font-weight: bold;'>" & ponderado & "</td></tr>"

            Return formato_ponderado
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
            Return String.Empty
        End Try
    End Function

    Private Function fu_VerCursoConvalidado(ByVal curso As String, ByVal tipo_matricula As String, ByRef etiqueta_curso As String) As Boolean
        Try
            Select Case tipo_matricula
                Case "C" 'Convalidado
                    etiqueta_curso = curso & " <font color=""#008080"">(CONVALIDADO)</font>"
                    Return True

                Case "S" 'Suficiencia
                    etiqueta_curso = "<font color=""#996633"">" & curso & "</font>"

                Case "U" 'Exámen de Ubicación
                    etiqueta_curso = "<font color=""#9900CC"">" & curso & "</font>"

                Case Else
                    etiqueta_curso = curso

            End Select

            Return False
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_VerColorNota(ByVal tipo_nota As String, ByVal nota As Double, ByVal limite As String, ByVal aplica_decimales As Boolean) As String
        Try
            Dim color As String = String.Empty

            If tipo_nota = "I" Then 'Ponderado por curso
                color = IIf(limite = "D", "#FF0000", "#0000FF")
            Else 'Ponderado por semestre
                color = IIf(nota < CDbl(limite), "#FF0000", "#0000FF")
            End If

            nota = IIf(aplica_decimales = True, FormatNumber(nota, 2), nota)

            Return "<font color=""" & color & """>" & nota & "</font>"
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
            Return String.Empty
        End Try
    End Function

    Private Function fu_MensajeVD(ByVal num_mat As String) As String
        Try
            If num_mat <> 0 Then
                Return num_mat
            End If

            Return "&nbsp;"
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
            Return String.Empty
        End Try
    End Function

    Private Function mt_Exportar() As Boolean
        Try
            If Not Session("frmHistorialAcademico.codigo_alu") Is Nothing Then
                Dim ruta_reporte As String = ConfigurationManager.AppSettings("RutaReporte") & "PRIVADOS/ACADEMICO/ACAD_HistorialNotas&id=" & Session("frmHistorialAcademico.codigo_alu") & "&foto=" & Session("frmHistorialAcademico.foto_alu")

                Me.udpScripts.Update()
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "openwindows", "window.open('" & ruta_reporte & "');", True)
            Else
                mt_ShowMessage("Debe buscar al estudiante antes de exportar su historial académico.", MessageType.warning)
                Return False
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ObtenerRomano(ByVal numero As String) As String
        Try
            Dim numero_romano As String = "-"

            Select Case numero
                Case 1 : numero_romano = "I"
                Case 2 : numero_romano = "II"
                Case 3 : numero_romano = "III"
                Case 4 : numero_romano = "IV"
                Case 5 : numero_romano = "V"
                Case 6 : numero_romano = "VI"
                Case 7 : numero_romano = "VII"
                Case 8 : numero_romano = "VIII"
                Case 9 : numero_romano = "IX"
                Case 10 : numero_romano = "X"
            End Select

            Return numero_romano
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
            Return String.Empty
        End Try
    End Function

#End Region

End Class


Partial Class librerianet_academico_DetalleAlumnoXCurso
    Inherits System.Web.UI.Page
    Dim cod_alu As Int16
    Dim CodUniv As String
    Private NumAsis As Integer = 0
    Private NumFalt As Integer = 0
    Private NumTard As Integer = 0

   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Try
        '    If Page.Request.UrlReferrer Is Nothing OrElse Page.Request.UrlReferrer.ToString.Contains("http://www.usat.edu.pe/campusvirtual") = False Then
        '        Response.Write("<br>Acceso Denegado")
        '        Form.Controls.Clear()
        '        Exit Sub
        '    End If
        'Catch ex As Exception
        '    Response.Write("Ocurrió un error, inténtelo nuevamente")
        'End Try


        If Not IsPostBack Then
            Dim c As Control = LoadControl("controles/CtrlFotoAlumno.ascx")
            Dim objAlu As New ClsConectarDatos
            Dim datosAlu, datosCurso As New Data.DataTable
            Dim sylabus As Integer

            If Request.QueryString("syl") = -1 Or Request.QueryString("syl") Is Nothing Then
                gvConsolidado.DataSourceID = SqlDSAsistenciayNotas.ID
                sylabus = -1
            ElseIf Request.QueryString("syl") = "0" Then
                gvConsolidado.DataSourceID = Nothing
                sylabus = 0
            Else
                gvConsolidado.DataSourceID = SqlDSAsistenciayNotas0.ID
                sylabus = Request.QueryString("syl")
            End If
            'Dim objEnc As New EncriptaCodigos.clsEncripta
            Dim ObjCif As New PryCifradoNet.ClsCifradoNet
            Dim codUniver As String
            'codUniver = Mid(objEnc.Decodifica(Request.QueryString("cod")), 4)

            Dim CadReci As String
            CadReci = Request.QueryString("cod").Trim

            codUniver = ObjCif.DesCifrado(CadReci.Substring(16, 16), CadReci.Substring(0, 16)).ToString.Substring(6, 10)
            CType(c, CtrlFotoAlumno).CodigoUniversitario = codUniver

            'CType(c, CtrlFotoAlumno).CodigoUniversitario = Request.QueryString("al")

            CType(c, CtrlFotoAlumno).Nombre = Request.QueryString("nom")
            Me.TblCelFoto.Controls.Add(c)
            Me.lblAlumno.Text = Request.QueryString("nom")
            objAlu.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objAlu.AbrirConexion()

            'datosAlu = objAlu.TraerDataTable("consultarAlumno", "COD", Request.QueryString("al"))
            datosAlu = objAlu.TraerDataTable("consultarAlumno", "COD", codUniver)

            If datosAlu.Rows.Count > 0 Then
                Me.lblAlumno.Text = "<b>ALUMNO: </b>" & datosAlu.Rows(0).Item("nombres")


                Session("codigo_alu") = datosAlu.Rows(0).Item("codigo_alu")

            End If
            Session("codigo_cac") = objAlu.TraerDataTable("ConsultarCicloAcademico", "CV", 1).Rows(0).Item("codigo_cac")
            Me.lblCicloAcad.Text = "<b>| CICLO ACADÉMICO: </b>" & objAlu.TraerDataTable("ConsultarCicloAcademico", "CO", CInt(Session("codigo_cac"))).Rows(0).Item("descripcion_cac")
            datosCurso = objAlu.TraerDataTable("MED_BoletaDetalleAlumno", Session("codigo_alu"), Session("codigo_cac"))
            If datosCurso.Rows.Count > 0 Then
                Dim separador As New Label
                Dim MenuCurso As New LinkButton
                MenuCurso.Text = "VER TODOS"
                MenuCurso.ID = -1
                If sylabus = "-1" Then
                    MenuCurso.ForeColor = Drawing.Color.Red
                    MenuCurso.Font.Size = 8
                Else
                    MenuCurso.ForeColor = Drawing.Color.Blue
                    MenuCurso.Font.Size = 7
                End If
                separador.Text = " | "
                separador.Font.Bold = True
                cellCursos.Controls.Add(separador)
                separador = New Label
                separador.Text = " | "
                separador.Font.Bold = True
                MenuCurso.Attributes.Add("OnClick", "javascript:this.href='DetalleAlumnoXCurso.aspx?cod=" & Request.QueryString("cod") & "&syl=-1';this.style.color='#FF0000';")
                cellCursos.Controls.Add(MenuCurso)
                cellCursos.Controls.Add(separador)
                For i As Int16 = 0 To datosCurso.Rows.Count - 1
                    MenuCurso = New LinkButton
                    separador = New Label
                    MenuCurso.ID = datosCurso.Rows(i).Item("codigo_cup").ToString & i
                    MenuCurso.Text = datosCurso.Rows(i).Item("nombre_cur").ToString
                    If sylabus = datosCurso.Rows(i).Item("codigo_syl") Then
                        MenuCurso.ForeColor = Drawing.Color.Red
                        MenuCurso.Font.Size = 8
                    Else
                        MenuCurso.ForeColor = Drawing.Color.Blue
                        MenuCurso.Font.Size = 7
                    End If
                    separador.Text = " | "
                    separador.Font.Bold = True
                    cellCursos.Controls.Add(MenuCurso)
                    cellCursos.Controls.Add(separador)
                    MenuCurso.Attributes.Add("OnClick", "javascript:this.href='DetalleAlumnoXCurso.aspx?cod=" & Request.QueryString("cod") & "&syl=" & datosCurso.Rows(i).Item("codigo_syl").ToString & "&dma=" & datosCurso.Rows(i).Item("codigo_dma").ToString & "';this.style.color='#FF0000';")
                Next
            End If
            objAlu.CerrarConexion()

        End If
    End Sub


    Private Sub Axls(ByVal nombrearchivo As String, ByRef grid As GridView, ByVal titulo As String, ByVal piedepagina As String)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & nombrearchivo & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default

        Response.Write(ClsFunciones.HTML(Me.gvConsolidado, titulo, piedepagina))
        Response.End()
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Axls("ReporteAsistenciaNotas", gvConsolidado, "Reporte de Asistencia y notas " & Me.lblCicloAcad.Text, "Sistema de Notas y Asistencias - Campus Virtual USAT")
    End Sub


    Protected Sub gvConsolidado_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvConsolidado.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim consideraasistencia As Int16
            fila = e.Row.DataItem

            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            e.Row.Cells(5).Attributes.Add("tooltip", "<table><tr><td width='300'><b>TEMAS:" & "</b><br>" & fila.Row("temas_act").ToString & "</td></tr></table>")
            e.Row.Cells(5).Attributes.Add("style", "cursor:hand")

            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            '** Notas ***
            If fila.Row("considerarnota_act").ToString.ToUpper = "S" Then
                e.Row.Cells(7).Text = "SI"
            Else
                e.Row.Cells(7).Text = "NO"
                e.Row.Cells(10).Text = "<img src='../../images/bloquear.gif' alt='La actividad no considera Ingreso de Notas'>"
            End If

            '*** Asistencia ***
            consideraasistencia = IIf(fila.Row("ConsiderarAsistencia_act") Is DBNull.Value, 0, IIf(fila.Row("ConsiderarAsistencia_act") = True, 1, 0))

            If consideraasistencia = 1 Then
                e.Row.Cells(6).Text = "SI"
                e.Row.Cells(6).ForeColor = Drawing.Color.Black

                If fila.Row("CalifNum_dact") Is DBNull.Value Or fila.Row("CalifNum_dact").ToString.Trim = "" Then
                    e.Row.Cells(9).Text = "-"
                Else
                    e.Row.Cells(9).Text = fila.Row("CalifNum_dact")
                End If

                If ((fila.Row("tipoasistencia_act") Is System.DBNull.Value) Or (fila.Row("tipoasistencia_act").ToString.ToUpper = "F")) Then
                    e.Row.Cells(8).Text = "FALTÓ"
                    e.Row.Cells(8).ForeColor = Drawing.Color.Red
                    e.Row.Cells(9).Text = "---"
                    NumFalt = NumFalt + 1
                ElseIf fila.Row("tipoasistencia_act").ToString.ToUpper = "A" Then
                    e.Row.Cells(8).Text = "ASISTIÓ"
                    e.Row.Cells(8).ForeColor = Drawing.Color.Blue
                    NumAsis = NumAsis + 1
                Else
                    e.Row.Cells(8).Text = "TARDANTE"
                    e.Row.Cells(8).ForeColor = Drawing.Color.Orange
                    NumTard = NumTard + 1
                End If
            Else
                e.Row.Cells(6).Text = "NO"
                e.Row.Cells(6).ForeColor = Drawing.Color.Red
                e.Row.Cells(8).Text = "-"
                If fila.Row("CalifNum_dact") Is DBNull.Value Then
                    e.Row.Cells(9).Text = "-"
                Else
                    e.Row.Cells(9).Text = fila.Row("CalifNum_dact").ToString
                End If
            End If

        End If
        Me.LblAsistencia.Text = NumAsis
        Me.LblTardanzas.Text = NumTard
        Me.LblFaltas.Text = NumFalt

    End Sub

End Class

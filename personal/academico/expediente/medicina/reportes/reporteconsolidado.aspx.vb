
Partial Class medicina_reportes_reporteconsolidado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Codigo_cup As Integer
        Dim tblCurso As Data.DataTable
        Dim Alumnos As New Data.DataTable
        Dim Codigo_alu As Integer
        Dim Contador As Integer
        Dim con As Integer = 0

        Codigo_cup = Request.QueryString("codigo_cup")

        tblCurso = obj.TraerDataTable("ConsultarCursoProgramado", 10, Codigo_cup, 0, 0, 0)
        If tblCurso.Rows.Count > 0 Then
            Me.lblcurso.Text = tblCurso.Rows(0).Item("nombre_cur") & " - Grupo (" & tblCurso.Rows(0).Item("grupohor_cup") & ")"
            Me.lblInicio.Text = tblCurso.Rows(0).Item("fechainicio_cup")
            Me.lblFin.Text = tblCurso.Rows(0).Item("fechafin_cup")
        End If
        Codigo_alu = 0
        Contador = 1
        Alumnos = obj.TraerDataTable("MED_ConsultarAlumnosCurso", 0, Codigo_cup)

        If Alumnos.Rows.Count > 0 Then
            Dim Fila As New TableRow
            For i As Int16 = 0 To Alumnos.Rows.Count - 1
                If Codigo_alu <> CInt(Alumnos.Rows(i).Item("codigo_Alu").ToString) Then
                    Codigo_alu = CInt(Alumnos.Rows(i).Item("codigo_Alu").ToString)
                    Dim Columna As New TableCell
                    Dim c As Control = LoadControl("../controles/CtrlFotoAlumno.ascx")

                    CType(c, CtrlFotoAlumno).Enlace = "javascript:location.href='reporteasistenciaynotas.aspx?codigo_dma=" & Alumnos.Rows(i).Item("codigo_dma").ToString & _
                                                      "&codigo_syl=" & Request.QueryString("codigo_syl") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&codu=" & _
                                                      Alumnos.Rows(i).Item("codigouniver_Alu").ToString & "&nom=" & Alumnos.Rows(i).Item("alumno").ToString & "'"
                    CType(c, CtrlFotoAlumno).CodigoUniversitario = Alumnos.Rows(i).Item("codigouniver_Alu").ToString
                    CType(c, CtrlFotoAlumno).Nombre = Alumnos.Rows(i).Item("alumno").ToString
                    Columna.Controls.Add(c)

                    Fila.Cells.Add(Columna)
                    con = con + 1

                    If con Mod 5 = 0 Then
                        Me.TblAsistencia.Rows.Add(Fila)
                        Fila = New TableRow
                    End If
                    If con > Alumnos.Rows.Count - 1 Then
                        Me.TblAsistencia.Rows.Add(Fila)
                    End If
                End If
            Next
        End If
    End Sub
End Class

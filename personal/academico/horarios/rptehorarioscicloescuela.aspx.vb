
Partial Class rptehorarioscicloescuela
    Inherits System.Web.UI.Page
    Dim codigo_cup As Int32 = -1
    Dim contador As Int16 = 0
    Dim PrimeraFila As Int16 = -1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '1	ADMINISTRADOR DEL SISTEMA
        '9	DIRECTOR DE ESCUELA
        '15	DIRECTOR DE DEPARTAMENTO ACADEMICO
        '23	DECANO DE FACULTAD
        '32	ASISTENTE DE DIRECTORES DE ESCUELA
        '85 COORDINACIÓN(ACADÉMICA)
        '116  VICERRECTORADO DE PROFESORES
        If IsPostBack = False Then
            Dim tbl As Data.DataTable
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")
            Dim codigo_mod As Integer = Request.QueryString("mod")
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            If codigo_tfu = 1 Or codigo_tfu = 116 Or codigo_tfu = 85 _
            Or codigo_tfu = 23 _
            Or codigo_tfu = 15 Or codigo_tfu = 181 Then
                'tbl = obj.TraerDataTable("ConsultarCarreraProfesional", "MA", 0)
                tbl = obj.TraerDataTable("ConsultarCarreraProfesional", "PG", Request.QueryString("id"))
            Else
                If codigo_tfu = 9 Or codigo_tfu = 32 Then 'Por autorización de DA no restringir a Directores de Escuela 
                    tbl = obj.TraerDataTable("ConsultarCarreraProfesional", "MP", "")
                Else
                    tbl = obj.TraerDataTable("consultaracceso", "ESC", Request.QueryString("mod"), codigo_usu)
                End If
            End If

            tbl.Dispose()

            ClsFunciones.LlenarListas(Me.dpEscuela, tbl, "codigo_cpf", "nombre_cpf", "--Seleccione--")
            ClsFunciones.LlenarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "to", 0), "codigo_cac", "descripcion_cac", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Try

            '1	ADMINISTRADOR DEL SISTEMA
            '9	DIRECTOR DE ESCUELA
            '11	DIRECCIÓN DE CURSOS COMPLEMENTARIOS
            '15	DIRECTOR DE DEPARTAMENTO ACADEMICO
            '23	DECANO DE FACULTAD
            '25	VICERRECTOR DE ESTUDIANTES
            '32	ASISTENTE DE DIRECTORES DE ESCUELA
            '41 VICERECTOR(ACADÉMICO)
            '85 COORDINACIÓN(ACADÉMICA)
            '116  VICERRECTORADO DE PROFESORES
        
            Dim codigo_cac As Integer
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            If Me.dpEscuela.SelectedValue <> -1 Then
                obj.AbrirConexion()
                codigo_cac = obj.TraerDataTable("ConsultarCicloAcademico", "CV", 1).Rows(0).Item("codigo_cac")
                obj.CerrarConexion()
                If (Me.dpCodigo_cac.SelectedValue <= codigo_cac) _
                    Or (Request.QueryString("ctf") = "1" Or Request.QueryString("ctf") = "15" _
                        Or Request.QueryString("ctf") = "41" Or Request.QueryString("ctf") = "9" _
                        Or Request.QueryString("ctf") = "11" Or Request.QueryString("ctf") = "25" _
                        Or Request.QueryString("ctf") = "85" Or Request.QueryString("ctf") = "23" _
                        Or Request.QueryString("ctf") = "116" Or Request.QueryString("ctf") = "32" _
                        Or Request.QueryString("ctf") = "181" Or Request.QueryString("ctf") = "212") Then
                    Me.lblMensaje.Text = ""
                    obj.AbrirConexion()
                    Me.dtCiclos.DataSource = obj.TraerDataTable("ConsultarHorarios", 16, Me.dpCodigo_cac.SelectedValue, Me.dpEscuela.SelectedValue, 0)
                    Me.dtCiclos.DataBind()
                    obj.CerrarConexion()
                    obj = Nothing
                    If dtCiclos.Items.Count = 0 Then
                        Me.lblMensaje.Text = "No se han registrado horarios en las asignaturas programadas"
                    End If
                Else
                    dtCiclos.DataSource = Nothing
                    dtCiclos.DataBind()
                    Me.lblMensaje.Text = "Usted no puede visualizar horarios de semestres superiores al actual"
                End If
            End If
        Catch ex As Exception
            Response.Write("1-" & ex.Message)
        End Try
    End Sub
    Protected Sub dtCiclos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dtCiclos.ItemDataBound
        Dim lblciclo As Label = CType(e.Item.FindControl("lblciclo"), Label)
        Dim lblgrupo As Label = CType(e.Item.FindControl("lblgrupo"), Label)

        If lblciclo.Text <> "" Then
            Dim gr As Table
            gr = CType(e.Item.FindControl("tblHorario"), Table)
            Me.MostrarHorario(gr, lblciclo.Text, lblgrupo.Text)
        End If
    End Sub
    Private Sub MostrarHorario(ByVal tblhorario As Table, ByVal ciclo_cur As Int16, ByVal grupo As String)
        Try
            Dim eFila As TableRow
            Dim Celda As TableCell
            Dim UltimoDia As Int16 = 7
            Dim inicioBD, finBD As Integer
            Dim diaBD, dia As String
            Dim hora As Integer

            Dim obj As New clsaccesodatos
            Dim tbl As Data.DataTable
            obj.abrirconexion()
            tbl = obj.TraerDataTable("HOR_ConsultarHorariosCicloEscuelaGrupo", Me.dpCodigo_cac.SelectedValue, Me.dpEscuela.SelectedValue, ciclo_cur, grupo, Me.txtinicio.Text, Me.txtfin.Text)
            obj.cerrarconexion()

            'Llenar las filas con horas
            For f As Int16 = 7 To 23 'Me.dpInicio.Items.Count - 1
                eFila = New TableRow
                'Llenar celdas por día
                For c As Int16 = 1 To UltimoDia + 1
                    If c = 1 Then
                        Celda = New TableCell

                        'Celda.Text = f.ToString & ":10" & " - " & (f + 1).ToString & ":00"     linea anterior con los 10 min.

                        '=============================================================================================================
                        Celda.Text = f.ToString & ":00" & " - " & (f + 1).ToString & ":00"      'linea paara mostrar las horas exactas
                        '=============================================================================================================

                        eFila.Cells.Add(Celda)
                        Celda = Nothing
                    Else 'Marcar la celda ocupada
                        Celda = New TableCell

                        If c = 2 Then dia = "LU"
                        If c = 3 Then dia = "MA"
                        If c = 4 Then dia = "MI"
                        If c = 5 Then dia = "JU"
                        If c = 6 Then dia = "VI"
                        If c = 7 Then dia = "SA"
                        If c = 8 Then dia = "DO"

                        hora = AnchoHora(f) 'Aumenta 6 para que se inicie a las 7:00 am

                        For h As Int16 = 0 To tbl.Rows.Count - 1

                            diaBD = Mid(tbl.Rows(h).Item("dia_lho").ToString, 1, 2)
                            inicioBD = Mid(tbl.Rows(h).Item("nombre_hor").ToString, 1, 2)
                            finBD = Mid(tbl.Rows(h).Item("horafin_lho").ToString, 1, 2)

                            'si el día es el mismo y la horaactual es menor que horafin y mayor que la horainicio
                            If Trim(dia) = Trim(diaBD) And Int(hora) >= Int(inicioBD) And Int(hora) < Int(finBD) Then
                                Celda.CssClass = "Marcado" 'tbl.Rows(h).Item("color_hor").ToString
                                If tbl.Rows(h).Item("codigo_cpf") <> Me.dpEscuela.SelectedValue Then
                                    Celda.CssClass = "otraEsc"
                                End If

                                Dim docente As String
                                docente = Me.FormatearNombreProfesor(tbl.Rows(h).Item("nombres_Per").ToString, tbl.Rows(h).Item("apellidopat_per").ToString, tbl.Rows(h).Item("apellidomat_per").ToString)
                                If tbl.Rows(h).Item("estadoHorario_lho").ToString = "A" Then
                                    Celda.Text = Celda.Text & tbl.Rows(h).Item("nombre_cur").ToString & "<br><font color='blue'>" & tbl.Rows(h).Item("ambiente").ToString & "</font><br>" & docente & "<br><br>"
                                Else
                                    Celda.Text = Celda.Text & tbl.Rows(h).Item("nombre_cur").ToString & "<br><font color='red'>" & tbl.Rows(h).Item("ambiente").ToString & "</font><br>" & docente & "<br><br>"
                                End If
                            End If
                        Next
                        eFila.Cells.Add(Celda)
                        'Celda.Text = ""
                        Celda = Nothing
                    End If
                Next

                tblhorario.Rows.Add(eFila)
            Next
            tbl.Dispose()
        Catch ex As Exception
            Response.Write("2-" & ex.Message)
        End Try
    End Sub
    Private Function FormatearNombreProfesor(ByVal n As String, ByVal ap As String, ByVal am As String) As String
        'Extrar un solo nombre
        Dim str() As String
        Dim nombre As String
        str = Split(n, " ")
        If UBound(str) = 0 Then 'No tiene 2 nombres
            nombre = n
        Else
            nombre = str(0)
        End If
        'concatenar apellido paterno y la primera letra del ap. materno
        FormatearNombreProfesor = nombre & " " & ap & " " & Left(am, 1) & "."
    End Function
    Private Function AnchoHora(ByVal cad As String) As String
        If Len(cad) < 2 Then
            AnchoHora = "0" & cad
        Else
            AnchoHora = cad
        End If
    End Function
    Protected Sub dpCodigo_cac_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpCodigo_cac.SelectedIndexChanged
        Me.cmdBuscar.Visible = False
        Me.dtCiclos.Visible = False
        If dpCodigo_cac.SelectedValue <> -1 Then
            Dim obj As New ClsConectarDatos
            Dim tbl As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            tbl = obj.TraerDataTable("ConsultarCicloAcademico", "CO", Me.dpCodigo_cac.SelectedValue)
            Me.txtinicio.Text = tbl.Rows(0).Item("FechaIniClases_cac").ToString
            Me.txtfin.Text = tbl.Rows(0).Item("FechaFinClases_cac").ToString
            obj.CerrarConexion()
            obj = Nothing
            If tbl.Rows.Count > 0 Then
                Me.cmdBuscar.Visible = True
                Me.dtCiclos.Visible = True
            End If
        End If
    End Sub
End Class

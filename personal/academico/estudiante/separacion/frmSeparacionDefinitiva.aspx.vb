
Partial Class academico_estudiante_separacion_frmSeparacionDefinitiva
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        Try
            If (Request.QueryString("codUniv") IsNot Nothing) Then
                Dim cls As New ClsConectarDatos
                Dim dt, dtSep As New Data.DataTable
                Dim tipo As Integer = 0
                cls.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                cls.AbrirConexion()
                dt = cls.TraerDataTable("MAT_DatosCartaRetirado", Request.QueryString("codUniv"))
                If (Request.QueryString("codSep") <> "") Then                    
                    dtSep = cls.TraerDataTable("SEP_DatosSeparacion", Request.QueryString("codSep"))
                End If
                cls.CerrarConexion()
                If (dt.Rows.Count > 0) Then                    
                    If (Request.QueryString("res").ToString.Trim = "0") Then
                        If (dtSep.Rows.Count > 0) Then                            
                            Me.lblResolucion.Text = "Resolución Nro. " & dtSep.Rows(0).Item("nroResolucion")
                        End If
                    Else
                        Me.lblResolucion.Text = "Resolución Nro. " & Request.QueryString("res").ToString.Trim
                    End If

                    If (Request.QueryString("tipo") = "0") Then
                        If (dtSep.Rows.Count > 0) Then                            
                            tipo = dtSep.Rows(0).Item("codigo_tse")
                        End If
                    Else
                        tipo = Request.QueryString("tipo")
                    End If


                    If (tipo = 1) Then
                        Me.DivTemporal.Visible = True
                        Me.DivDefinitivo.Visible = False
                        Me.lblArticulo.Text = "artículo Nº 43"
                        Me.lblArticulo2.Text = "artículo Nº 43"
                        Me.lblTipoSeparacion.Text = "Separar temporalmente, por un año "
                        'Me.lblTipoSeparacion2.Text = "separación temporal"
                        Me.lblTitulo.Text = "SEPARACION TEMPORAL"
                        Me.lblVeces.Text = "tercera vez"
                        Me.lblVeces2.Text = "tercera vez"
                    Else
                        Me.DivTemporal.Visible = False
                        Me.DivDefinitivo.Visible = True
                        Me.lblArticulo.Text = "artículo Nº 44"
                        Me.lblArticulo2.Text = "artículo Nº 44"
                        Me.lblTipoSeparacion.Text = "Separar definitivamente"
                        Me.lblTipoSeparacion2.Text = "separación definitiva"
                        Me.lblTitulo.Text = "SEPARACION DEFINITIVA"
                        Me.lblVeces.Text = "cuarta vez"
                        Me.lblVeces2.Text = "cuarta vez"
                    End If
                    'Me.lblResolucion1.Text = Request.QueryString("res").ToString.Trim
                    Me.lblEstudiante.Text = dt.Rows(0).Item("Estudiante").ToString()
                    Me.lblEstudiante1.Text = dt.Rows(0).Item("Estudiante").ToString()
                    Me.lblEstudiante2.Text = dt.Rows(0).Item("Estudiante").ToString()
                    Me.lblEstudiante3.Text = dt.Rows(0).Item("Estudiante").ToString()
                    Me.lblEstudiante4.Text = dt.Rows(0).Item("Estudiante").ToString()
                    Me.lblDirectorEscuela.Text = dt.Rows(0).Item("DirectorEscuela").ToString()
                    Me.lblmatricula.Text = dt.Rows(0).Item("CodMatricula").ToString()
                    Me.lblEscuela.Text = dt.Rows(0).Item("nombre_Cpf").ToString()
                    'Me.lblEscuela1.Text = dt.Rows(0).Item("nombre_Cpf").ToString()
                    'Me.lblFecha.Text = "Chiclayo, " & Date.Now.ToString("dd MMMM yyyy")
                    Me.lblFecha.Text = "Chiclayo, " & Date.Now.Day.ToString & " de " & RetornaMes(Date.Now.Month) & " de " & Date.Now.Year.ToString

                    'Me.lblFecha1.Text = Date.Now.ToString("dd MMMM yyyy")

                    '******************** Detalle de los cursos ********************
                    Dim dtDetalle As New Data.DataTable
                    cls.AbrirConexion()
                    dtDetalle = cls.TraerDataTable("MAT_SeparacionCursos", dt.Rows(0).Item("codigo_alu"))
                    cls.CerrarConexion()
                    Me.gvCursos.DataSource = dtDetalle
                    Me.gvCursos.DataBind()

                    '******************** Semestre Activo ********************
                    Dim dtCiclo As New Data.DataTable
                    cls.AbrirConexion()
                    dtCiclo = cls.TraerDataTable("MAT_SeparacionUltimoCiclo", dt.Rows(0).Item("codigo_alu"))
                    cls.CerrarConexion()
                    If (dtCiclo.Rows.Count > 0) Then
                        'Me.lblSemestre.Text = dtCiclo.Rows(0).Item("descripcion_Cac").ToString
                    End If
                ElseIf (dt.Rows.Count = 0) Then
                    Response.Write("<script>window.close()</script>")
                End If
                cls = Nothing
            Else
                Response.Write("<script>window.close()</script>")
            End If
        Catch ex As Exception
            Response.Write("Error:" & ex.Message)
        End Try
    End Sub

    Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Me.btnImprimir.Visible = False
        Response.Write("<script>javascript:print();</script>")
        Me.btnImprimir.Visible = True
    End Sub

    Private Function RetornaMes(ByVal mes As String) As String
        Dim strMes As String = ""

        Select Case mes
            Case 1 : strMes = "Enero"
            Case 2 : strMes = "Febrero"
            Case 3 : strMes = "Marzo"
            Case 4 : strMes = "Abril"
            Case 5 : strMes = "Mayo"
            Case 6 : strMes = "Junio"
            Case 7 : strMes = "Julio"
            Case 8 : strMes = "Agosto"
            Case 9 : strMes = "Setiembre"
            Case 10 : strMes = "Octubre"
            Case 11 : strMes = "Noviembre"
            Case 12 : strMes = "Diciembre"
        End Select

        Return strMes
    End Function
End Class

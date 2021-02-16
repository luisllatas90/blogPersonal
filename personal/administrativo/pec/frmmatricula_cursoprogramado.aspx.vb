Imports System.Security.Cryptography
Partial Class frmmatricula_cursoprogramado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")
            '=================================
            'Permisos por CECO
            '=================================
            Dim obj As New ClsConectarDatos
            Dim objfun As New ClsFunciones
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            'objfun.CargarListas(cboCecos, obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), "", Request.QueryString("mod")), "codigo_Cco", "Nombre", ">> Seleccione<<")
            objfun.CargarListas(cboCecos, obj.TraerDataTable("EVE_BuscaCcoId", Request.QueryString("id"), Request.QueryString("ctf"), Request.QueryString("mod"), "S"), "codigo_Cco", "descripcion_cco", ">> Seleccione<<")
            '=================================
            'Llenar combos
            '=================================
            objfun.CargarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0), "codigo_cac", "descripcion_cac")

            obj.CerrarConexion()
            objfun = Nothing
            obj = Nothing
            Response.Write(Session("capl"))
        End If
    End Sub

    Private Sub ValidaCronograma()
        Dim dt As New Data.DataTable
        Dim cls As New ClsConectarDatos
        Dim f1 As Date
        Dim f2 As Date
        cls.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try

            Dim abr As String = ""
            Dim tipo As String = ""
            Dim cpal As String = ""

            'VALORES TFU
            '1	    ADMINISTRADOR DEL SISTEMA	
            '9	    DIRECTOR DE ESCUELA	
            '138	COORDINADOR DE DIRECCIÓN ACADÉMICA
            '182	COORDINADOR ACADÉMICO  
            '214	COORDINADOR ACADÉMICO ADMISIÓN  
            '222    COORDINADOR ACADÉMICO DE POSGRADO	

            'VALORES ACT
            '1	    MATRICULA: VÍA CAMPUS ESTUDIANTE	MATALU	6
            '9	    MATRICULA: VÍA CAMPUS ASESOR	MATASE	4
            '12	    MATRICULA: VIA CAMPUS COORD. ACADEMICA	MATEVRE	5
            '22	    MATRICULA: RETIRO Y AGREGADOS ASESORES	RAASE	9


            'cpal = DesencriptarMenu(Request.QueryString("capl"))            

            Select Case Request.QueryString("mod") 'JBANDA 16-10-2020
                Case 1 : cpal = 32
                Case 3 : cpal = 30
                Case 4 : cpal = 43
                Case 5 : cpal = 37
                Case Else : cpal = 0
            End Select

            'Response.Write(cpal)
            cls.AbrirConexion()
            ' dt = cls.TraerDataTable("MAT_consultarTipoFuncion", Session("id_per"), 37)
            dt = cls.TraerDataTable("MAT_consultarTipoFuncion", Session("id_per"), cpal)
            cls.CerrarConexion()

            Dim ctf As Integer = 0
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("codigo_Tfu") = Request.QueryString("ctf") Then
                        ctf = dt.Rows(i).Item("codigo_Tfu")
                    End If
                Next
                ' Response.Write(ctf)
                If ctf = 1 Or ctf = 138 Then
                    abr = "MATEVRE" '12
                ElseIf ctf = 9 Or ctf = 182 Or ctf = 222 Then  'EPENA 29102019 ID-28314-  SE AGREGO EL CODIGO_TFU 222: COORDINADOR ACADÉMICO DE POSGRADO
                    abr = "MATASE" '9
                ElseIf ctf = 214 Then 'Por JQuepuy | 05ENE2021
                    abr = "RAASE"
                Else
                    abr = ""
                End If
            End If

            cls.AbrirConexion()
            dt = cls.TraerDataTable("ACAD_ConsultarCronograma", abr, Me.dpCodigo_cac.SelectedValue, Request.QueryString("mod"))
            cls.CerrarConexion()            

			If Request.QueryString("mod")<> 6 then
		
				If dt.Rows.Count = 0 Then
					'ShowMessage("No puede registrar silabos por encontrarse fuera de fecha, coordinar con Dirección académica", MessageType.Error)
					Me.lblMensaje.Text = "No tiene acceso a matricular, coordinar con Dirección académica"

					HdCronograma.Value = False
                    ' lblMensaje.Style.Item("display") = "block"
					lblMensaje.CssClass = "MensajeError"
				Else
					f1 = dt.Rows(0).Item("fechaIni_Cro")
					f2 = dt.Rows(0).Item("fechaFin_Cro")

					Me.lblMensaje.Text = "Fechas definidas para matricular: " & f1.ToString("dd/MM/yyyy") & "-" & f2.ToString("dd/MM/yyyy")
					HdCronograma.Value = True
					' lblMensaje.Style.Item("display") = "block"
					lblMensaje.CssClass = "MensajeOk"
					'ShowMessage("Fechas definidas para subir silabos: " & f1.ToString("dd/MM/yyyy") & "-" & f1.ToString("dd/MM/yyyy"), MessageType.Info)
				End If
				
			End If

        Catch ex As Exception
            'ShowMessage("Error: " & ex.Message.ToString, MessageType.Error)
            'JBANDA 16-10-2020
            lblMensaje.Text = ex.Message.ToString
            lblMensaje.CssClass = "MensajeError"
            HdCronograma.Value = False
        End Try
    End Sub

    Private Sub CargarDetalleGrupoHorario(ByVal tipo As String, ByVal ID As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.grwAlumnosPlan.DataBind()
        Me.fraAlumnosPlan.Visible = True


        'Response.Write(Me.cboCecos.SelectedValue)
        'Response.Write("<br />")
        'Response.Write(Me.hdcodigo_cup.Value)


        'Mostrar cursos disponibles para PROGRAMAR
        'If Request.QueryString("mod") = 3 Then
        Me.grwAlumnosPlan.DataSource = obj.TraerDataTable("EVE_ConsultarAlumnoXCentroCostos_profecionalizacion", Me.cboCecos.SelectedValue, Me.hdcodigo_cup.Value)
        'Else
        'Me.grwAlumnosPlan.DataSource = obj.TraerDataTable("EVE_ConsultarAlumnoXCentroCostos", Me.cboCecos.SelectedValue, Me.hdcodigo_cup.Value)
        'End If


        Me.grwAlumnosPlan.DataBind()
        Me.fraAlumnosPlan.Visible = (Me.grwAlumnosPlan.Rows.Count > 0)

        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim Fila As GridViewRow
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim codigo_usu As Integer = Request.QueryString("id")
        Dim valoresdevueltos(1) As String

        'Me.cmdGuardar.Enabled = False
        Try
            Dim marcados As Int16 = 0
            For I As Int16 = 0 To Me.grwAlumnosPlan.Rows.Count - 1
                Fila = Me.grwAlumnosPlan.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        marcados = marcados + 1
                    End If
                End If
            Next
            Page.RegisterStartupScript("falta", "alert('" & marcados & "');")
            If marcados = 0 Then
                Page.RegisterStartupScript("falta", "alert('Debe marcar a los estudiantes que se matricularán en el grupo horario');")
                Exit Sub
            End If

            obj.AbrirConexion()
            '==================================
            'Matricular a los estudiantes
            '==================================
            Dim mensaje() As Object
            For I As Int16 = 0 To Me.grwAlumnosPlan.Rows.Count - 1
                Fila = Me.grwAlumnosPlan.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then                    
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        'Response.Write("AgregarMatriculaWeb 'A', '" & Me.grwAlumnosPlan.DataKeys.Item(Fila.RowIndex).Values("codigo_alu") & "', '" & dpCodigo_pes.SelectedValue & "', '" & Me.dpCodigo_cac.SelectedValue & "', 'P', 'Matricula por MPE', '" & hdcodigo_cup.Value & "','1', 'N', 'P', 'MAT', '" & codigo_usu & "', 'PC', NULL, NULL, 1, NULL")
                        'Response.Write(Me.hdcodigo_cup.Value)
                        mensaje = obj.Ejecutar("AgregarMatriculaWeb", "A", Me.grwAlumnosPlan.DataKeys.Item(Fila.RowIndex).Values("codigo_alu"), dpCodigo_pes.SelectedValue, Me.dpCodigo_cac.SelectedValue, "P", "Matricula por MPE", hdcodigo_cup.Value & ",", "1,", "N", "P", "MAT", codigo_usu, "PC", System.DBNull.Value, System.DBNull.Value, 1, System.DBNull.Value)
                        If (mensaje(0).ToString.Contains("history") = True) Then
                            Response.Write(mensaje(0).ToString.Substring(17))
                            Return
                        End If
                    End If
                End If
            Next

            '==================================
            'Actualizar Controles
            '==================================
            Me.grwGruposProgramados.DataSource = obj.TraerDataTable("ConsultarDatosProgramaEspecial", 7, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.cboCecos.SelectedValue, 0)
            Me.grwGruposProgramados.DataBind()

            obj.CerrarConexion()
            obj = Nothing

            'Me.LimpiarDatos(True)
            Page.RegisterStartupScript("Matriculados", "<script>alert('Las matrículas se han registrado correctamente');</script>")
            cmdVer_Click(sender, e)
        Catch ex As Exception
            obj = Nothing
            hdcodigo_cup.Value = 0
            Page.RegisterStartupScript("error", "alert('Ocurrió un error en el registro: " & Err.Description & "');")
        End Try
    End Sub
    Private Sub LimpiarDatos(ByVal estado As Boolean)
        Me.fraAlumnosPlan.Visible = False
        Me.fraGruposProgramados.Visible = estado
        Me.grwAlumnosPlan.DataBind()
        Me.hdcodigo_cup.Value = 0
    End Sub
    Protected Sub grwGruposProgramados_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwGruposProgramados.RowCommand
        Me.hdcodigo_cup.Value = 0
        If (e.CommandName = "editar") Then
            Me.fraGruposProgramados.Visible = False
            Me.hdcodigo_cup.Value = Convert.ToString(Me.grwGruposProgramados.DataKeys(Convert.ToInt32(e.CommandArgument)).Value)
            Dim FilaSeleccionada As GridViewRow = grwGruposProgramados.Rows(Convert.ToInt32(e.CommandArgument))
            Me.lblnombre_cur.Text = FilaSeleccionada.Cells(0).Text & " (Grupo: " & FilaSeleccionada.Cells(2).Text & ")"

            Me.CargarDetalleGrupoHorario(3, Me.hdcodigo_cup.Value)
        End If
    End Sub
    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        LimpiarDatos(True)
    End Sub
    Protected Sub grwGruposProgramados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwGruposProgramados.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            Me.lblGrupos.Text = "Lista de Grupos Horario Programados (" & e.Row.RowIndex + 1 & ")"
            'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            'e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(7).Text = IIf(fila.Row("estado_cup") = False, "Cerrado", "Abierto")

            'Cargar Profesores y Horario
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim gr As BulletedList = CType(e.Row.FindControl("lstProfesores"), BulletedList)
            gr.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 4, fila.Row("codigo_cup"), Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, 0)
            gr.DataBind()
            obj.CerrarConexion()
            obj = Nothing


            If Me.HdCronograma.Value = "False" Then
                e.Row.Cells(11).Text = ""
            End If



        End If
    End Sub


    Private Sub RefreshGrid()

        For Each _Row As GridViewRow In grwGruposProgramados.Rows
            grwGruposProgramados_RowDataBound(grwGruposProgramados, New GridViewRowEventArgs(_Row))
        Next

    End Sub
    Protected Sub grwAlumnosPlan_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwAlumnosPlan.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            'e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")
            e.Row.Cells(1).Text = e.Row.RowIndex + 1
            e.Row.Cells(5).Text = IIf(fila.Row("estadoactual_alu") = 0, "Inactivo", "Activo")
            e.Row.Cells(6).Text = IIf(fila.Row("numeroDeudas") > 0, "Con Deuda", "Sin Deuda")
            e.Row.Cells(6).ForeColor = IIf(fila.Row("estadodeuda_alu") = 1, Drawing.Color.Red, Drawing.Color.Black)          
            e.Row.Cells(8).Text = IIf(fila.Row("matriculado") = 0, "No", "Sí")




            'If CInt(fila.Row("matriculado")) > 0 Or fila.Row("estadoactual_alu") = 0 Then  'Or fila.Row("estadodeuda_alu") = 1 ' se quitó validación por deuda
            'e.Row.Cells(0).Text = ""
            'End If

            'If fila.Row("estadoactual_alu") = 0 Then  'Or fila.Row("estadodeuda_alu") = 1 ' se quitó validación por deuda
            '    e.Row.Cells(0).Text = ""

            'ElseIf CInt(fila.Row("matriculado")) > 0 Then

            '    If fila.Row("CumpleRecquisito") = "SI" Then
            '        CType(e.Row.FindControl("chkElegir"), CheckBox).Checked = True

            '    Else
            '        e.Row.Cells(0).Text = ""
            '    End If
            'ElseIf fila.Row("estadoactual_alu") = 1 Then
            '    If fila.Row("CumpleRecquisito") = "SI" Then
            '        CType(e.Row.FindControl("chkElegir"), CheckBox).Checked = True
            '        'e.Row.Cells(0).Text = True
            '    Else
            '        e.Row.Cells(0).Text = ""
            '    End If
            'End If

            'If fila.Row("numeroDeudas") > 2 And Request.QueryString("mod").ToString = "3" Then -- DEBEN MAS DE UNA CUOTA CAMBIO 22.09



            'If (fila.Row("numeroDeudas") > 1 And _
            '        (Request.QueryString("mod").ToString = "3" _
            '         Or Request.QueryString("mod").ToString = "5" _
            '         Or Request.QueryString("mod").ToString = "8")) Then
            '    e.Row.Cells(0).Text = ""
            'End If

            'If fila.Row("tieneconvenio") = "No" And Request.QueryString("mod").ToString = "3" Then
            '    e.Row.Cells(0).Text = ""
            'End If
            If fila.Row("estadoactual_alu") = 1 Then
                If fila.Row("matriculado") = 0 Then
                    If fila.Row("numeroDeudas") <= 1 Then
                        If fila.Row("tieneconvenio") = "Si" Then
                            If fila.Row("CursoAprobado").ToString.Contains("Aprobo Curso") = False Then

                                If fila.Row("CumpleRecquisito") = "SI" Then

                                    e.Row.Cells(1).Font.Bold = True
                                    e.Row.Cells(2).Font.Bold = True
                                    e.Row.Cells(3).Font.Bold = True
                                    e.Row.Cells(4).Font.Bold = True
                                    e.Row.Cells(5).Font.Bold = True
                                    e.Row.Cells(6).Font.Bold = True
                                    e.Row.Cells(7).Font.Bold = True
                                    e.Row.Cells(8).Font.Bold = True
                                    e.Row.Cells(9).Font.Bold = True
                                    e.Row.Cells(10).Font.Bold = True
                                    e.Row.Cells(11).Font.Bold = True
                                Else
                                    If fila.Row("CantReq") = 0 Then

                                        e.Row.Cells(1).Font.Bold = True
                                        e.Row.Cells(2).Font.Bold = True
                                        e.Row.Cells(3).Font.Bold = True
                                        e.Row.Cells(4).Font.Bold = True
                                        e.Row.Cells(5).Font.Bold = True
                                        e.Row.Cells(6).Font.Bold = True
                                        e.Row.Cells(7).Font.Bold = True
                                        e.Row.Cells(8).Font.Bold = True
                                        e.Row.Cells(9).Font.Bold = True
                                        e.Row.Cells(10).Font.Bold = True
                                        e.Row.Cells(11).Font.Bold = True
                                    Else
                                        If Request.Params("ctf") = "85" Or Request.Params("ctf") = "181" Then

                                        Else
                                            e.Row.Cells(0).Text = ""
                                        End If
                                        'e.Row.Cells(0).Text = ""
                                        e.Row.Cells(1).ForeColor = System.Drawing.Color.Red
                                        e.Row.Cells(2).ForeColor = System.Drawing.Color.Red
                                        e.Row.Cells(3).ForeColor = System.Drawing.Color.Red
                                        e.Row.Cells(4).ForeColor = System.Drawing.Color.Red
                                        e.Row.Cells(5).ForeColor = System.Drawing.Color.Red
                                        e.Row.Cells(6).ForeColor = System.Drawing.Color.Red
                                        e.Row.Cells(7).ForeColor = System.Drawing.Color.Red
                                        e.Row.Cells(8).ForeColor = System.Drawing.Color.Red
                                        e.Row.Cells(9).ForeColor = System.Drawing.Color.Red
                                        e.Row.Cells(10).ForeColor = System.Drawing.Color.Red
                                        e.Row.Cells(11).ForeColor = System.Drawing.Color.Red
                                    End If
                                End If

                            Else
                                e.Row.Cells(0).Text = ""
                            End If
                        Else
                            If (Request.QueryString("mod") <> 1) Then
                                e.Row.Cells(0).Text = ""
                            End If
                        End If
                    Else
                        e.Row.Cells(0).Text = ""
                    End If
                Else
                    e.Row.Cells(0).Text = ""
                End If
            Else
                e.Row.Cells(0).Text = ""
            End If

   

        End If
    End Sub
    Protected Sub grwGruposProgramados_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwGruposProgramados.RowDeleting
        Dim obj As New ClsConectarDatos
        Dim valoresdevueltos(1) As String
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("EliminarCursoProgramado", grwGruposProgramados.DataKeys(e.RowIndex).Values("codigo_cup"), 0).copyto(valoresdevueltos, 0)
        'Cargar los Cursos Programados
        Me.grwGruposProgramados.DataSource = obj.TraerDataTable("ConsultarDatosProgramaEspecial", 7, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, 0, 0)
        Me.grwGruposProgramados.DataBind()
        obj.CerrarConexion()
        obj = Nothing
        e.Cancel = True
        Me.lblGrupos.Text = "Lista de Grupos Horario Programados (" & Me.grwGruposProgramados.Rows.Count & ")"
    End Sub
    Protected Sub cmdVer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVer.Click
        ValidaCronograma()
        LimpiarDatos(False)

        If Me.dpCodigo_pes.SelectedValue <> -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            '=================================
            'Cargar Grupos Programados
            '=================================            
            '==========================================================================================================================================================================================
            'Linea que funcionaba hasta antes del 07.08.2013
            'Me.grwGruposProgramados.DataSource = obj.TraerDataTable("EVE_ConsultarProgramacionAcademica", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.cboCecos.SelectedValue)
            '==========================================================================================================================================================================================

            'usuario    : xDguevara: 
            'fecha      : 08.08.2013 
            'solicitado : esaavedra  
            'Se cambio por esto, por los parametros que se agrego del filtro y el curso.

            'Para Produccion:
            Me.grwGruposProgramados.DataSource = obj.TraerDataTable("EVE_ConsultarProgramacionAcademica", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.cboCecos.SelectedValue, 1, "")

            'Para hacer pruebas descomentar, porque no hay data:
            'Me.grwGruposProgramados.DataSource = obj.TraerDataTable("EVE_ConsultarProgramacionAcademica", 71, 627, 6362, 1, "")

            Me.grwGruposProgramados.DataBind()

            obj.CerrarConexion()
            obj = Nothing
            If Me.grwGruposProgramados.Rows.Count = 0 Then
                Me.lblGrupos.Text = "Programar Nuevos Grupos Horario"
            Else
                Me.lblGrupos.Text = "Lista de Grupos Horario Programados"
            End If
            Me.fraGruposProgramados.Visible = True
        End If
    End Sub
    Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCecos.SelectedIndexChanged
        Me.dpCodigo_pes.Items.Clear()
        Me.grwGruposProgramados.Dispose()
        LimpiarDatos(False)
        Me.cmdVer.Visible = False
        If cboCecos.SelectedValue <> -1 Then
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")

            Dim obj As New ClsConectarDatos
            Dim objfun As New ClsFunciones
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            '=================================
            'Llenar Planes de Estudio
            '=================================
            objfun.CargarListas(Me.dpCodigo_pes, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 10, 1, Me.cboCecos.SelectedValue, 0), "codigo_pes", "descripcion_pes")


            objfun = Nothing
            obj.CerrarConexion()
            obj = Nothing
            Me.cmdVer.Visible = Me.dpCodigo_pes.Items.Count > 0
        End If
    End Sub

    Protected Sub grwAlumnosPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwAlumnosPlan.SelectedIndexChanged

    End Sub

    Protected Sub grwGruposProgramados_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwGruposProgramados.SelectedIndexChanged

    End Sub
    Public Function DesencriptarMenu(ByVal Input As String) As String
        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Convert.FromBase64String(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV
        Return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))
    End Function
End Class

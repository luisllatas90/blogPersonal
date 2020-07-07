Imports System.Data
Imports System.Security.Cryptography
Partial Class academico_cargalectiva_FrmConfiguraSustitorio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load                
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If (IsPostBack = False) Then
            VerificaTipoUsuario()
            CargaEscuela()
            CargaCiclo()
            VerificaCronograma()

            CargarFechas()
            'CargarCapacidad()
            CargarFechasExamen()

            Dim ctr As Integer = 0
            Me.hdFn.Value = Encriptar(fnVerificarFuncion().ToString)




            verificarFuncionesDirectorEscuela() ' #EPENA 26/11/2019
            verificarFuncionesDirDepartamentoAcademico() ' #EPENA 27/11/2019

            '# EPENA 27/01/2019
            ddlDocenteEdit.DataSource = ListaDocente()
            ddlDocenteEdit.DataTextField = "Docente"
            ddlDocenteEdit.DataValueField = "codigo_per"
            ddlDocenteEdit.DataBind()
            '# EPENA 27/01/2019

        End If
        ''Para asignar o solicitar
        'If Page.Request.QueryString("codigo_amb") > 0 And Page.Request.QueryString("estado") <> "" Then
        '    Dim codigo_lho As Integer = 0
        '    Dim obj As New ClsConectarDatos
        '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        '    codigo_lho = Session("h_codigolho_REC")
        '    obj.AbrirConexion()
        '    obj.Ejecutar("HorarioPE_RegistrarAmbienteSol", codigo_lho, CInt(Page.Request.QueryString("codigo_amb")), Page.Request.QueryString("estado"))
        '    If EnviarCorreo(codigo_lho) Then
        '        Session("h_codigolho_REC") = 0
        '        Me.PanelExaRec.visible = True
        '        Me.PanelHorarioRegistro.visible = False
        '        Me.PanelBuscar.visible = False

        '    Else
        '        Response.Write("<script>alert('Ocurrió un error al enviar el correo 1')</script>")
        '    End If
        'End If
    End Sub

    Private Sub verificarFuncionesDirDepartamentoAcademico() ' #EPENA 27/11/2019
       
        If fnEsDirectorDepartamento() Then
            Me.btnCopiarHorario.Enabled = False
            For Each item As ListItem In cboEstado.Items

                If item.Value = 0 Then
                    item.Enabled = False
                End If
            Next
        Else

            For Each item As ListItem In cboEstado.Items

                If item.Value = 0 Then
                    item.Enabled = True
                End If
            Next
        End If

    End Sub

    Private Sub verificarFuncionesDirectorEscuela() ' #EPENA 26/11/2019
        If fnEsDirectorEscuela() Or fnEsCoordinadorDA() Or fnEsDirectorAcademico() Or fnEsAdministradorSistema() Then
            Me.btnGenerar.Enabled = True
        Else

            Me.btnGenerar.Enabled = False
        End If
    End Sub

    Private Function fnVerificarFuncion() As Integer ' #EPENA 261/11/2019
        Dim dt As New Data.DataTable
        Dim cls As New ClsConectarDatos
        cls.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try

            Dim rpta As Boolean = False
            Dim tipo As String = ""

            '1	    ADMINISTRADOR DEL SISTEMA	
            '9	    DIRECTOR DE ESCUELA	
            '15 	DIRECTOR DE DEPARTAMENTO ACADEMICO
            '181	DIRECCIÓN ACADÉMICA 
            '182	COORDINADOR ACADÉMICO  
            '85	   COORD DIRECCION ACADÉMICA

            cls.AbrirConexion()
            dt = cls.TraerDataTable("MAT_consultarTipoFuncion", Session("id_per"), 1)
            cls.CerrarConexion()

            Dim ctf As Integer = 0

            If dt.Rows.Count > 0 Then

                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("codigo_Tfu") = Request.QueryString("ctf") Then
                        ctf = dt.Rows(i).Item("codigo_Tfu")
                        'hdFn.Value = Encriptar(ctf.ToString)
                    End If
                Next

            End If

            Return ctf
        Catch ex As Exception

            Return 0
        End Try

    End Function

    Private Function fnEsDirectorAcademico() As Boolean ' #EPENA 261/11/2019
        Try
            Dim rpta As Boolean = False
            Dim ctf As Integer = 0
            ctf = CInt(Desencriptar(Me.hdFn.Value.ToString))
            If ctf = 181 Then
                rpta = True
            Else
                rpta = False
            End If
            Return rpta
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function fnEsDirectorDepartamento() As Boolean ' #EPENA 261/11/2019
        Try
            Dim rpta As Boolean = False
            Dim ctf As Integer = 0
            ctf = CInt(Desencriptar(Me.hdFn.Value.ToString))
            If ctf = 15 Then
                rpta = True
            Else
                rpta = False
            End If
            Return rpta
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function fnEsCoordinadorDA() As Boolean ' #EPENA 261/11/2019

        Try

            Dim rpta As Boolean = False


            Dim ctf As Integer = 0

            ctf = CInt(Desencriptar(Me.hdFn.Value.ToString))


            If ctf = 85 Then
                rpta = True
            Else
                rpta = False
            End If

            Return rpta
        Catch ex As Exception
            Return False

        End Try

    End Function
    Private Function fnEsDirectorEscuela() As Boolean ' #EPENA 261/11/2019
        Try

            Dim rpta As Boolean = False
            Dim tipo As String = ""

            Dim ctf As Integer = 0

            ctf = CInt(Desencriptar(Me.hdFn.Value.ToString))
            If ctf = 9 Then
                rpta = True
            Else
                rpta = False
            End If

            Return rpta

        Catch ex As Exception
            Return False

        End Try

    End Function

    Private Function fnEsAdministradorSistema() As Boolean ' #EPENA 261/11/2019
        Try

            Dim rpta As Boolean = False
            Dim tipo As String = ""

            Dim ctf As Integer = 0

            ctf = CInt(Desencriptar(Me.hdFn.Value.ToString))
            If ctf = 1 Then
                rpta = True
            Else
                rpta = False
            End If

            Return rpta

        Catch ex As Exception
            Return False

        End Try

    End Function

    Public Function Encriptar(ByVal Input As String) As String

        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Encoding.UTF8.GetBytes(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV

        Return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function

    Public Function Desencriptar(ByVal Input As String) As String
        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Convert.FromBase64String(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV
        Return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))
    End Function

  

    Function EnviarCorreo(ByVal codigo_lho As Integer) As Boolean
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tbCorreo As New Data.DataTable
        obj.AbrirConexion()
        tbCorreo = obj.TraerDataTable("HorarioPE_EnviarCorreo", codigo_lho)
        obj.CerrarConexion()
        obj = Nothing
        Dim objCorreo As New ClsEnvioMailAmbiente
        Dim bodycorreo As String
        If tbCorreo.Rows.Count Then
            bodycorreo = "<html>"
            bodycorreo = bodycorreo & "<body style=""font-size:12px;text-align:justify; font-family:Tahoma;""> <div style=""color:#284775; Background-color:white; border-color:#284775; border:1px solid; padding:10px;"">"
            bodycorreo = bodycorreo & "<p><b>" & tbCorreo.Rows(0).Item("header") & "</b></p>"
            bodycorreo = bodycorreo & "<p>" & tbCorreo.Rows(0).Item("cco") & "</p>"
            bodycorreo = bodycorreo & "<p>" & tbCorreo.Rows(0).Item("descripcion") & "</p>"
            bodycorreo = bodycorreo & "<p>" & tbCorreo.Rows(0).Item("body") & "</p>"
            bodycorreo = bodycorreo & "<table style=""font-size:12px;font-family:Tahoma;border:#99bae2 1px solid;"" cellSpacing=0 cellPadding=4 border=""0"">"
            bodycorreo = bodycorreo & "<tr style=""color:  #284775; background-color:#E8EEF7; font-weight:bold;""><td>Día</td><td>Fecha</td><td>Ambiente</td><td>Horario</td><td>Capacidad</td><td>Ubicación</td></tr>"
            bodycorreo = bodycorreo & "<tr><td>" & tbCorreo.Rows(0).Item("dia") & "</td><td>" & tbCorreo.Rows(0).Item("fechaInicio") & "</td><td>" & tbCorreo.Rows(0).Item("Ambiente") & "</td><td>" & tbCorreo.Rows(0).Item("Horario") & "</td><td style=""text-align:center;"">" & tbCorreo.Rows(0).Item("capacidad") & "</td><td>" & tbCorreo.Rows(0).Item("ubicacion") & "</td></tr>"
            bodycorreo = bodycorreo & "</table>"
            bodycorreo = bodycorreo & "<p>" & tbCorreo.Rows(0).Item("footer") & "</p>"
            bodycorreo = bodycorreo & "<p> Atte,<br/><b>" & tbCorreo.Rows(0).Item("firma") & "</b></p>"
            bodycorreo = bodycorreo & "</div></body></html>"
            Try
                'tbCorreo.Rows(0).Item("EnviarA") = "yperez@usat.edu.pe"
                'tbCorreo.Rows(0).Item("cc") = ""
                objCorreo.EnviarMailAd("campusvirtual@usat.edu.pe", tbCorreo.Rows(0).Item("firma"), tbCorreo.Rows(0).Item("EnviarA"), tbCorreo.Rows(0).Item("SubjectA") & " - Módulo de Solicitud de Ambientes", bodycorreo, True, tbCorreo.Rows(0).Item("cc"))
                Return True
            Catch ex As Exception
                Response.Write("<script>alert('" & ex.Message & "')</script>")
            End Try
        Else
            objCorreo.EnviarMailAd("campusvirtual@usat.edu.pe", "error", "yperez@usat.edu.pe", "error - Módulo de Solicitud de Ambientes", codigo_lho, True, "")
            Return True
        End If
    End Function

    Private Sub VerificaCronograma()

        Dim obj1 As New ClsConectarDatos
        obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim dt As New Data.DataTable
        Try
            obj1.AbrirConexion()
            dt = obj1.TraerDataTable("ACAD_VerificaCronogramaExamen")
            obj1.CerrarConexion()
            If (dt.Rows.Count = 0) Then
                Me.btnBuscar.Enabled = False
                Me.btnGenerar.Enabled = False
                Me.lblMensaje.Text = "El cronograma no permite registrar programación de examenes de recuperación."
            Else
                Me.btnBuscar.Enabled = True
                Me.btnGenerar.Enabled = True
                Me.lblMensaje.Text = ""
            End If
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al verificar el cronograma: " & ex.Message
            Me.btnBuscar.Enabled = False
            Me.btnGenerar.Enabled = False
        End Try
    End Sub

    Private Sub VerificaTipoUsuario()
        Dim obj1 As New ClsConectarDatos
        obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString


        Dim dt As New Data.DataTable
        Try
            'Verificamos si el profesor es de filosofia
            obj1.AbrirConexion()
            dt = obj1.TraerDataTable("ACAD_VerificaProfesorFilosofia", Session("id_per"))
            obj1.CerrarConexion()
            If (dt.Rows.Count = 0) Then
                Me.HdTipo.Value = "N"   'No es de Filosofia
            ElseIf (dt.Rows.Count > 0) Then
                Me.HdTipo.Value = "S"   'Si es de Filosofia
                Me.cboEscuela.Visible = False
                Me.texto.InnerHtml = ""
            End If
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al reconocer el usuario: " & ex.Message
        End Try
    End Sub

    Private Sub CargaCiclo()
        Dim dt As New Data.DataTable
        Dim obj1 As New ClsConectarDatos
        obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            obj1.AbrirConexion()
            dt = obj1.TraerDataTable("ACAD_ListaCicloAcademico")
            obj1.CerrarConexion()
            obj1 = Nothing

            Me.cboCiclo.DataSource = dt
            Me.cboCiclo.DataTextField = "descripcion_cac"
            Me.cboCiclo.DataValueField = "codigo_cac"
            Me.cboCiclo.DataBind()
            dt = Nothing

        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar los ciclos académicos: " & ex.Message
        End Try
    End Sub

    Private Sub CargaEscuela()
        Dim dt As New Data.DataTable
        Try
            Dim obj1 As New ClsConectarDatos
            obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj1.AbrirConexion()
            dt = obj1.TraerDataTable("ACAD_ListaEscuelaPreGrado", Session("id_per"))
            obj1.CerrarConexion()
            obj1 = Nothing

            Me.cboEscuela.DataSource = dt
            Me.cboEscuela.DataTextField = "nombre_cpf"
            Me.cboEscuela.DataValueField = "codigo_cpf"
            Me.cboEscuela.DataBind()

            If (dt.Rows.Count > 0) Then
                Me.btnBuscar.Enabled = True
                Me.btnGenerar.Enabled = True
            Else
                Me.btnBuscar.Enabled = False
                Me.btnGenerar.Enabled = False
            End If
            dt = Nothing
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar las carreras profesionales: " & ex.Message
            Me.btnBuscar.Enabled = False
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Buscar()
        If Me.cboEstado.SelectedValue = "1" Then
            btnGenerar.Enabled = False
            btnCopiarHorario.Enabled = True
        Else
            btnGenerar.Enabled = True
            btnCopiarHorario.Enabled = False
        End If

        verificarFuncionesDirectorEscuela() ' #EPENA 261/11/2019
        verificarFuncionesDirDepartamentoAcademico() ' #EPENA 261/11/2019
    End Sub

    Protected Sub gvDatos_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvDatos.RowCancelingEdit
        gvDatos.EditIndex = -1
        Me.btnBuscar.Enabled = True
        Me.btnGenerar.Enabled = True
        Buscar()
    End Sub

    Protected Sub gvDatos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDatos.RowDataBound

        Try
            Dim celda As TableCellCollection = e.Row.Cells


            If e.Row.RowType = DataControlRowType.Header Then
                If fnEsDirectorDepartamento() Then
                    celda(9).Visible = False
                ElseIf fnEsDirectorEscuela() Then
                    celda(9).Visible = False
                ElseIf fnEsCoordinadorDA() Then
                    celda(9).Visible = True
                Else
                    celda(9).Visible = True
                End If
                'Response.Write("1" & celda(9).Text)
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
   

                If fnEsDirectorDepartamento() Then
                    celda(9).Visible = False
                ElseIf fnEsDirectorEscuela() Then
                    celda(9).Visible = False
                ElseIf fnEsCoordinadorDA() Then
                    celda(9).Visible = True
                ElseIf fnEsDirectorAcademico() Then
                    celda(9).Visible = True

                Else
                    celda(9).Visible = True
                End If

                'Response.Write("2" & celda(9).Text)
            End If


        Catch ex As Exception
            Response.Write(ex.Message & " -- " & ex.StackTrace)
        End Try
    End Sub

    Protected Sub gvDatos_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvDatos.RowEditing
        Try
            Me.btnBuscar.Enabled = False
            Me.btnGenerar.Enabled = False
            gvDatos.EditIndex = e.NewEditIndex

            Dim id As Integer = Convert.ToInt32(gvDatos.DataKeys(e.NewEditIndex).Value)
            Dim row As DataRow = BuscarRegistro(id) 'Debe ir la fila

            Buscar()

            Dim combo As DropDownList = TryCast(gvDatos.Rows(e.NewEditIndex).FindControl("ddlDocente"), DropDownList)
            Dim texto As TextBox = TryCast(gvDatos.Rows(e.NewEditIndex).FindControl("txtAsistencia"), TextBox)
            Dim texto2 As TextBox = TryCast(gvDatos.Rows(e.NewEditIndex).FindControl("txtVacantes"), TextBox)

            If combo IsNot Nothing Then
                combo.DataSource = ListaDocente()
                combo.DataTextField = "Docente"
                combo.DataValueField = "codigo_per"
                combo.DataBind()
            End If
            'Response.Write("<script>alert(" & Convert.ToString(row("codigo_per")) & ");</script>")
            combo.SelectedValue = Convert.ToString(row("codigo_per"))
            texto.Text = Convert.ToString(row("asistenciarec_cup"))
            texto2.Text = Convert.ToString(row("vacantes_Cup"))

            ' Response.Write(fnEsDirectorDepartamento())
            If fnEsDirectorDepartamento() Then
                combo.Enabled = True
                texto2.Enabled = False
                texto.Enabled = False

            ElseIf fnEsDirectorEscuela() Then
                combo.Enabled = False
                texto2.Enabled = True
                texto.Enabled = False

            ElseIf fnEsCoordinadorDA() Then
                combo.Enabled = True
                texto2.Enabled = True
                texto.Enabled = True

            ElseIf fnEsDirectorAcademico() Then
                combo.Enabled = True
                texto2.Enabled = True
                texto.Enabled = True
                texto.Text = 70

            Else ' por defecto
                combo.Enabled = True
                texto2.Enabled = True
                texto.Enabled = True

            End If




        Catch ex As Exception
            Me.lblMensaje.Text = "Error: " & ex.Message & "<>" & ex.Source
        End Try
    End Sub

    Private Sub Buscar()

        Dim obj1 As New ClsConectarDatos
        obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim codigo_fun As Integer = CInt(Desencriptar(Me.hdFn.Value.ToString))
        Dim codigo_per As Integer = CInt(Session("id_per"))

        Dim dt As New Data.DataTable
        Dim Tipo As Integer = 0
        Try
            If (Me.HdTipo.Value = "S") Then
                Tipo = 2
            ElseIf (Me.HdTipo.Value = "N") Then
                Tipo = 0
            End If

            obj1.AbrirConexion()
            dt = obj1.TraerDataTable("ACAD_ListaCursosProgramados", Me.cboEstado.SelectedValue + Tipo, 0, Me.cboCiclo.SelectedValue, Me.txtCurso.Text, Me.cboEscuela.SelectedValue, Me.ddlCiclo_cur.SelectedValue, codigo_fun, codigo_per)
            obj1.CerrarConexion()

            If (Me.cboEstado.SelectedValue = 0) Then
                Me.gvDatos.DataSource = dt
                Me.gvDatos.DataBind()

                Me.gvDatos.Visible = True
                Me.gvProgramado.Visible = False
            Else
                Me.gvProgramado.DataSource = dt
                Me.gvProgramado.DataBind()

                Me.gvDatos.Visible = False
                Me.gvProgramado.Visible = True



            End If

            dt = Nothing
            obj1 = Nothing
            Me.lblMensaje.Text = ""

            Session("HB_codigo_cpf") = Me.cboEscuela.SelectedValue
            Session("HB_estado") = Me.cboEstado.SelectedValue
            Session("HB_curso") = Me.txtCurso.Text

         

        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar la consulta: " & ex.Message
        End Try
    End Sub

    Private Function BuscarRegistro(ByVal codigo_cup As Integer) As DataRow
        Dim obj1 As New ClsConectarDatos
        obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim dt As New Data.DataTable
        Try
            Dim Tipo As Integer = 0



            If (Me.HdTipo.Value = "S") Then
                Tipo = 2
            ElseIf (Me.HdTipo.Value = "N") Then
                Tipo = 0
            End If

            obj1.AbrirConexion()
            dt = obj1.TraerDataTable("ACAD_ListaCursosProgramados", Me.cboEstado.SelectedValue + Tipo, codigo_cup, Me.cboCiclo.SelectedValue, "", 0, Me.ddlCiclo_cur.SelectedValue, 0, 0)
            obj1.CerrarConexion()

            obj1 = Nothing

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al buscar el registro: " & ex.Message
            Return Nothing
        End Try
    End Function

    Private Function ListaDocente() As Data.DataTable
        Dim dt As New Data.DataTable
        Try
            Dim codigo_fun As Integer = CInt(Desencriptar(Me.hdFn.Value.ToString))
            Dim codigo_cac As Integer = Me.cboCiclo.SelectedValue
            Dim codigo_per As Integer = CInt(Session("id_per"))

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_ListaDocentesDptoAcad", codigo_fun, codigo_per, codigo_cac)
            obj.CerrarConexion()
            obj = Nothing

            Return dt
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al buscar al cargar docentes: " & ex.Message
            Return Nothing
        End Try
    End Function

    Protected Sub gvDatos_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvDatos.RowUpdating

        Dim obj1 As New ClsConectarDatos
        obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim dt As New DataTable
        Dim dtCarga As New DataTable
        Dim sw As Byte = 0
        Try
            Dim id As Integer = Convert.ToInt32(gvDatos.DataKeys(e.RowIndex).Value)
            Dim combo As DropDownList = TryCast(gvDatos.Rows(e.RowIndex).FindControl("ddlDocente"), DropDownList)
            Dim personal As Integer = Convert.ToInt32(combo.SelectedValue)

            Dim texto As TextBox = TryCast(gvDatos.Rows(e.RowIndex).FindControl("txtAsistencia"), TextBox)
   
            Dim texto2 As TextBox = TryCast(gvDatos.Rows(e.RowIndex).FindControl("txtVacantes"), TextBox)

            'Buscamos cursos relacionados
            obj1.AbrirConexion()
            dt = obj1.TraerDataTable("ACAD_BuscaCursosxCursoPadre", id)
            obj1.CerrarConexion()

            'For i As Integer = 0 To dt.Rows.Count - 1
            'dtCarga = obj.TraerDataTable("ACAD_VerificaCargaAcademica", dt.Rows(i).Item("codigo_Cup"))
            'If (dtCarga.Rows.Count = 0) Then
            'sw = 1
            'Me.lblMensaje.Text = "El curso no tiene docente asignado."
            'End If
            'Next

            If (sw = 0) Then
                Me.lblMensaje.Text = "sw = 0"
                For i As Integer = 0 To dt.Rows.Count - 1
                    'Generamos los cursos para el examen de recuperacion
                    obj1.AbrirConexion()
                    obj1.Ejecutar("ACAD_CreaCursoSustitutorio", dt.Rows(i).Item("codigo_Cup"), personal, Session("id_per"), texto.Text, texto2.Text)
                    obj1.CerrarConexion()
                Next
            End If

            gvDatos.EditIndex = -1
            Me.btnBuscar.Enabled = True
            Me.btnGenerar.Enabled = True
            Buscar()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al actualizar registro: " & ex.Message
        End Try
    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim Fila As GridViewRow
        Dim dt As New DataTable

        Dim obj1 As New ClsConectarDatos
        obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim sw As Byte = 0
        Try
            For I As Int16 = 0 To Me.gvDatos.Rows.Count - 1
                Fila = Me.gvDatos.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        sw = 1
                    End If
                End If
            Next

            If (sw = 0) Then
                Me.lblMensaje.Text = "Debe seleccionar algun registro."
                Exit Sub
            End If

         
         
            For I As Int16 = 0 To Me.gvDatos.Rows.Count - 1
                Fila = Me.gvDatos.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    'Solo los que tienen el check activo
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then

                        'Buscamos cursos relacionados
                        obj1.AbrirConexion()
                        dt = obj1.TraerDataTable("ACAD_BuscaCursosxCursoPadre", gvDatos.DataKeys(I).Value)
                        obj1.CerrarConexion()

                        'Registramos los cursos relaciondos                              
                        For j As Integer = 0 To dt.Rows.Count - 1

                            obj1.AbrirConexion()
                            obj1.Ejecutar("ACAD_CreaCursoSustitutorio", dt.Rows(j).Item("codigo_cup"), 0, Session("id_per"), dt.Rows(j).Item("asistenciarec_cup"), dt.Rows(j).Item("vacantes_cup"))
                            obj1.CerrarConexion()

                        Next
                    End If
                End If
            Next

            Me.lblMensaje.Text = ""
            Buscar()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al generar: " & ex.Message
        End Try
    End Sub

    Protected Sub gvProgramado_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvProgramado.RowDeleting
        Dim obj1 As New ClsConectarDatos
        obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString


        Dim dt As New DataTable
        Try
            'Buscamos matriculados en los cursos programados para examen
            obj1.AbrirConexion()
            dt = obj1.TraerDataTable("ACAD_VerificaMatriculadosGrupo", gvProgramado.DataKeys(e.RowIndex).Value.ToString)
            obj1.CerrarConexion()
            If (dt.Rows.Count = 0) Then
                'Si no existen matriculados elimina el curso
                obj1.AbrirConexion()
                obj1.Ejecutar("ACAD_EliminaGrupoRecuperacion", gvProgramado.DataKeys(e.RowIndex).Value.ToString, Session("id_per"))
                obj1.CerrarConexion()
                Me.lblMensaje.Text = ""
            Else
                Me.lblMensaje.Text = "No se puede eliminar el curso porque tiene alumnos matriculados."
            End If
            Buscar()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al eliminar curso: " & ex.Message
        End Try
    End Sub

    Protected Sub gvProgramado_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProgramado.RowCommand
        Try

        
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Session("vacantes_cup_rec") = 0
            If (e.CommandName = "SolicitarAmbiente") Then
                Session("h_codigo_cup_REC") = gvProgramado.DataKeys(index).Values("codigo_cup")
                Session("vacantes_cup_rec") = gvProgramado.DataKeys(index).Values("vacantes_cup")
                CargarCapacidad()
                Me.PanelHorarioRegistro.Visible = True
                Me.PanelExaRec.Visible = False
                Me.lblNombreCur.Text = Me.cboCiclo.SelectedItem.Text & " - " & gvProgramado.DataKeys(index).Values("nombre_Cur") & " - " & gvProgramado.DataKeys(index).Values("grupoHor_Cup")
                Me.txtDescripcion.Text = Me.lblNombreCur.Text
                Me.lblNombreCarrera.Text = gvProgramado.DataKeys(index).Values("nombre_cpf") ' Me.cboEscuela.selecteditem.text
                Me.lblNombreCur0.Text = Me.lblNombreCur.Text
                Me.lblNombreCarrera0.Text = Me.lblNombreCarrera.Text
                Me.btnBuscar.Enabled = False
                Me.btnGenerar.Enabled = False
            End If
            If (e.CommandName = "EliminarHorario") Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                obj.Ejecutar("HorarioPE_EliminarLH", gvProgramado.DataKeys(index).Values("codigo_lho"))
                obj.CerrarConexion()
                obj = Nothing
                btnBuscar_Click(sender, e)
            End If

            '# EPENA 27/01/2019 INICIO
            If (e.CommandName = "EditarDocente") Then
                Me.lblNombreCur2.Text = Me.cboCiclo.SelectedItem.Text & " - " & gvProgramado.DataKeys(index).Values("nombre_Cur") & " - " & gvProgramado.DataKeys(index).Values("grupoHor_Cup")
                Me.PanelEditarDocente.Visible = True
                Dim per As Integer = gvProgramado.DataKeys(index).Values("codigo_Per")
                hdCupEdit.Value = gvProgramado.DataKeys(index).Values("codigo_cup")
                'Me.ddlDocenteEdit.SelectedValue = per

                Dim match As ListItem = ddlDocenteEdit.Items.FindByValue(per)
                If match Is Nothing Then
                    ddlDocenteEdit.SelectedIndex = 0
                Else
                    Me.ddlDocenteEdit.SelectedValue = per
                End If


                Me.gvProgramado.Enabled = False
                Me.ddlDocenteEdit.Focus()
            End If
            '# EPENA 27/01/2019 FIN
        Catch ex As Exception
            Response.Write(ex.Message & " -- " & ex.StackTrace)
        End Try
    End Sub
    '# EPENA 27/01/2019
    Private Sub btnGuardarDocenteEdit_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarDocenteEdit.Click
        Try
            Dim codigo_cup As Integer = CInt(Me.hdCupEdit.Value)
            Dim codigo_per As Integer = CInt(Me.ddlDocenteEdit.SelectedValue)

            Dim obj2 As New ClsConectarDatos
            obj2.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            Dim rpta As Boolean
            obj2.AbrirConexion()
            rpta = obj2.Ejecutar("horarioPe_ActualizarDocente", codigo_cup, codigo_per)
            obj2.CerrarConexion()


            If rpta Then
                Me.gvProgramado.Enabled = True
                Me.hdCupEdit.Value = ""
                Me.ddlDocenteEdit.SelectedIndex = 0
                Me.PanelEditarDocente.Visible = False
                btnBuscar_Click(sender, e)
            End If

        Catch ex As Exception
            Response.Write(ex.Message & " -- " & ex.StackTrace)
        End Try

    End Sub
    '# EPENA 27/01/2019
    Private Sub btnCancelarDocenteEdit_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarDocenteEdit.Click
        Me.gvProgramado.Enabled = True
        Me.hdCupEdit.Value = ""
        Me.ddlDocenteEdit.SelectedIndex = 0
        Me.PanelEditarDocente.Visible = False
    End Sub


    Sub CargarFechasExamen()
        ddlDia.Items.clear()

        Dim obj1 As New ClsConectarDatos
        obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim dt As New Data.DataTable
        Dim fechaInicio As Date
        Dim fechaFin As Date
        obj1.AbrirConexion()
        ClsFunciones.LlenarListas(Me.ddlTipoAmbiente, obj1.TraerDataTable("AsignarAmbiente_ListarAmbientes"), "codigo_tam", "descripcion_Tam", "<<TODOS>>")
        Dim tb As New Data.DataTable
        tb = obj1.TraerDataTable("horarioPe_consultarCronogramaRec", Me.cboCiclo.SelectedValue)
        obj1.CerrarConexion()

        obj1 = Nothing
        If tb.rows.count > 0 Then
            fechaInicio = tb.rows(0).item("fechaIni_Cro")
            fechaFin = tb.rows(0).item("fechaFin_Cro")

            Dim nombreFecha As String = ""
            If fechaInicio < fechaFin Then
                Do
                    Dim fechaInicioG As New Date(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day)
                    nombreFecha = WeekdayName(Weekday(fechaInicio)).ToUpper & " " & fechaInicioG.ToString.Substring(0, 10)
                    ddlDia.Items.Add(New ListItem(nombreFecha, fechaInicioG))
                    fechaInicio = fechaInicio.AddDays(1)
                Loop While fechaInicio <= fechaFin
            End If
        End If

    End Sub

    Sub CargarFechas()
        Dim item As String
        For i As Integer = 7 To 23
            If i < 10 Then
                item = "0" & i.ToString
            Else
                item = i.ToString
            End If
            Me.ddlInicioHora.Items.Add(New ListItem(item.ToString(), item.ToString()))
            Me.ddlFinHora.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i
        Me.ddlInicioHora.SelectedIndex = 0
        Me.ddlFinHora.SelectedIndex = 1

        For i As Integer = 0 To 59
            If i < 10 Then
                item = "0" & i.ToString
            Else
                item = i.ToString
            End If
            Me.ddlInicioMinuto.Items.Add(New ListItem(item.ToString(), item.ToString()))
            Me.ddlFinMinuto.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i
        Me.ddlFinHora.SelectedIndex = 21

    End Sub

    Sub CargarCapacidad()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.ddlCap.DataSource = obj.TraerDataTable("HorarioPE_ConsultarCapacidad", Session("vacantes_cup_rec"))
        Me.ddlCap.DataTextField = "capacidad_amb"
        Me.ddlCap.DataValueField = "capacidad_amb"
        Me.ddlCap.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub cboCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCiclo.SelectedIndexChanged
        CargarFechasExamen()
    
        ddlDocenteEdit.DataSource = ListaDocente()
        ddlDocenteEdit.DataTextField = "Docente"
        ddlDocenteEdit.DataValueField = "codigo_per"
        ddlDocenteEdit.DataBind()

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Session("h_codigo_cup_REC") = 0
        Me.PanelHorarioRegistro.visible = False
        Me.PanelExaRec.visible = True
        Me.btnBuscar.enabled = True
        Me.btnGenerar.enabled = True
    End Sub

    Protected Sub btnRegistrarPers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrarPers.Click
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim día As String
        Dim nombre_hor As String = ""
        Dim horaFin As String = ""
        Dim usu As Integer = CInt(Session("id_per"))
        Dim fechaInicio As New Date
        Dim fechaFin As New Date

        día = WeekdayName(Weekday(CDate(Me.ddlDia.SelectedValue))).substring(0, 2).toupper()
        nombre_hor = Me.ddlInicioHora.SelectedItem.Text & ":" & Me.ddlInicioMinuto.SelectedItem.Text
        horaFin = Me.ddlFinHora.SelectedItem.Text & ":" & Me.ddlFinMinuto.SelectedItem.Text
        fechaInicio = New Date(CInt(DatePart(DateInterval.Year, CDate(Me.ddlDia.SelectedValue))), CInt(DatePart(DateInterval.Month, CDate(Me.ddlDia.SelectedValue))), CInt(DatePart(DateInterval.Day, CDate(Me.ddlDia.SelectedValue))), CInt(nombre_hor.Substring(0, 2)), CInt(nombre_hor.Substring(3, 2)), 0)
        fechaFin = New Date(CInt(DatePart(DateInterval.Year, CDate(Me.ddlDia.SelectedValue))), CInt(DatePart(DateInterval.Month, CDate(Me.ddlDia.SelectedValue))), CInt(DatePart(DateInterval.Day, CDate(Me.ddlDia.SelectedValue))), CInt(horaFin.Substring(0, 2)), CInt(horaFin.Substring(3, 2)), 0)


        obj.AbrirConexion()
        tb = obj.TraerDataTable("HorarioPE_Registrar", día, Session("h_codigo_cup_REC"), nombre_hor, horaFin, usu, fechaInicio, fechaFin, 1, IIf(Me.txtDescripcion.Text.Length > 0, Me.txtDescripcion.Text.Trim, DBNull.Value), Me.ddlCap.SelectedValue, 12, 0)
        obj.CerrarConexion()

        If tb.rows.count > 0 Then
            Session("h_codigolho_REC") = tb.rows(0).item(0)
            Me.lblHorario.text = día & " " & Me.ddlDia.SelectedValue & " " & nombre_hor & " a " & horaFin

            Me.PanelHorarioRegistro.visible = False
            Me.panelBuscar.visible = True
        End If
    End Sub


    Protected Sub gridAmbientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAmbientes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If gridAmbientes.DataKeys(e.Row.RowIndex).Values("Accion").ToString > 0 Then
                Dim tb As New Data.DataTable
                Dim ultimo As Integer = e.Row.Cells.Count
                tb = gridAmbientes.DataSource

                'Preferencial
                If e.Row.Cells(0).Text = "S" Then
                    e.Row.Cells(0).Text = "<img src='private/images/star.png' title='Ambiente preferencial'>"
                End If
                'Normal
                If e.Row.Cells(0).Text = "N" Then
                    e.Row.Cells(0).Text = "<img src='private/images/door.png' title='Ambiente'>"
                End If
                tb.Dispose()
            End If

            Dim btnDocente As Button
            btnDocente = e.Row.FindControl("Button1")


        End If
    End Sub

    Protected Sub btnBuscarH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarH.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim tb As New Data.DataTable
        Dim tbAmb As New Data.DataTable
        Dim idsamb As String = ""
        obj.AbrirConexion()
        tb = obj.TraerDataTable("HorarioPE_ConsultarAsignacionAmbienteSol2", Session("h_codigolho_REC"), 0, 0, 0, 0, 0, 0, Me.ddlTipoAmbiente.SelectedValue)
        'Response.Write(Session("h_codigolho_REC"))
        obj.CerrarConexion()
        If tb.Rows.Count Then
            For i As Integer = 0 To tb.Rows.Count - 1
                idsamb = idsamb & tb.Rows(i).Item("codigo_amb").ToString & ","
            Next
            idsamb = idsamb.ToString.Substring(0, idsamb.Length - 1)
            obj.AbrirConexion()
            Me.gridAmbientes.DataSource = obj.TraerDataTable("Ambiente_ListarAmbienteCaracSol", idsamb)
            Me.gridAmbientes.DataBind()
            obj.CerrarConexion()
        Else
            Me.gridAmbientes.DataSource = Nothing
            Me.gridAmbientes.DataBind()
        End If
        obj = Nothing
    End Sub

    Protected Sub gridAmbientes_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridAmbientes.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "AsignarAmbiente") Then
            Dim codigo_lho As Integer = 0, estado As String = ""
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            codigo_lho = Session("h_codigolho_REC")
            obj.AbrirConexion()
            If gridAmbientes.DataKeys(index).Values("Tipo") = "S" Then
                estado = "P"
            Else
                estado = "A"
            End If


            obj.Ejecutar("HorarioPE_RegistrarAmbienteSol", codigo_lho, gridAmbientes.DataKeys(index).Values("Accion"), estado)

            Dim rptaCorreo As Boolean = False

            If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                rptaCorreo = EnviarCorreo(codigo_lho)
            Else
                rptaCorreo = True
            End If


            If rptaCorreo Then
                'If 1 = 1 Then
                Session("h_codigolho_REC") = 0
                Me.PanelExaRec.Visible = True
                Me.PanelHorarioRegistro.Visible = False
                Me.PanelBuscar.Visible = False
                Me.btnBuscar.Enabled = True
                Me.btnGenerar.Enabled = True

                btnBuscar_Click(sender, e)
                Me.gridAmbientes.DataSource = Nothing
                Me.gridAmbientes.DataBind()
                Me.ddlTipoAmbiente.SelectedIndex = 0
            Else
                'Response.Write("<script>alert('Ocurrió un error al enviar el correo 1')</script>")
            End If


        End If
    End Sub


    Protected Sub gvProgramado_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvProgramado.RowDataBound

        Try

        
            Dim btnHorario As Button, btnHorarioE As Button
            Dim chkPadre As CheckBox
            Dim chkHijo As CheckBox
            btnHorario = e.Row.FindControl("Button1")
            btnHorarioE = e.Row.FindControl("btnEliminarHorario")
            chkPadre = e.Row.FindControl("chkElegirPadre")
            chkHijo = e.Row.FindControl("chkElegirHijo")

            'Dim celda As TableCellCollection = e.Row.Cells
            If e.Row.RowType = DataControlRowType.Header Then ' #EPENA 27/11/2019

                If fnEsDirectorDepartamento() Then
                    e.Row.Cells(6).Visible = False
                    e.Row.Cells(9).Visible = False
                    ' Me.btnCopiarHorario.Enabled = False
                ElseIf fnEsDirectorAcademico() Then
                    e.Row.Cells(6).Visible = True
                    e.Row.Cells(9).Visible = True
                    ' Me.btnCopiarHorario.Enabled = True
                ElseIf fnEsCoordinadorDA() Then
                    e.Row.Cells(6).Visible = True
                    e.Row.Cells(9).Visible = True
                    'Me.btnCopiarHorario.Enabled = False

                ElseIf fnEsDirectorEscuela() Then
                    e.Row.Cells(6).Visible = True
                    e.Row.Cells(9).Visible = True
                    'Me.btnCopiarHorario.Enabled = True
                ElseIf fnEsAdministradorSistema() Then
                    e.Row.Cells(6).Visible = True
                    e.Row.Cells(9).Visible = True
                    'Me.btnCopiarHorario.Enabled = True
                Else
                    e.Row.Cells(6).Visible = False
                    e.Row.Cells(9).Visible = False
                    'Me.btnCopiarHorario.Enabled = False
                End If

            End If

                If e.Row.RowType = DataControlRowType.DataRow Then
                    If gvProgramado.DataKeys(e.Row.RowIndex).Values("ambiente") <> "-" Then
                        btnHorario.Enabled = False
                        chkPadre.Enabled = True
                        chkHijo.Enabled = False
                        btnHorarioE.Enabled = True
                    Else
                        btnHorarioE.Enabled = False
                        btnHorario.Enabled = True
                        chkPadre.Enabled = False
                        chkHijo.Enabled = True
                    End If

                    Dim btnAsignarHorario As Button
                    btnAsignarHorario = e.Row.FindControl("Button1")
                    Dim btnEditarDocenteProgramado As Button
                    btnEditarDocenteProgramado = e.Row.FindControl("btnEditarDocenteProgramado")
                    Dim btnEliminarHorario As Button
                    btnEliminarHorario = e.Row.FindControl("btnEliminarHorario")

                    If fnEsDirectorDepartamento() Then
                        btnAsignarHorario.Enabled = False
                        btnEliminarHorario.Enabled = False
                    btnEditarDocenteProgramado.Enabled = True
                    e.Row.Cells(6).Visible = False
                    e.Row.Cells(9).Visible = False

                    ElseIf fnEsDirectorAcademico() Then ' #EPENA 27/11/2019
                        btnAsignarHorario.Enabled = True
                        btnEliminarHorario.Enabled = True
                        btnEditarDocenteProgramado.Enabled = True
                    e.Row.Cells(6).Visible = True
                    e.Row.Cells(9).Visible = True

                    ElseIf fnEsCoordinadorDA() Then ' #EPENA 27/11/2019
                        btnAsignarHorario.Enabled = True
                        btnEliminarHorario.Enabled = True
                        btnEditarDocenteProgramado.Enabled = True
                    e.Row.Cells(6).Visible = True
                    e.Row.Cells(9).Visible = True

                ElseIf fnEsDirectorEscuela() Then
                    btnAsignarHorario.Enabled = True
                    btnEliminarHorario.Enabled = True
                    btnEditarDocenteProgramado.Enabled = False
                    e.Row.Cells(6).Visible = True
                    e.Row.Cells(9).Visible = True

                Else
                    btnAsignarHorario.Enabled = True
                    btnEliminarHorario.Enabled = True
                    btnEditarDocenteProgramado.Enabled = True
                    e.Row.Cells(6).Visible = True
                    e.Row.Cells(9).Visible = True

                    ' e.Row.Cells(8).Text = ""  ' no asigna o elimina docente
                    End If


                End If


        Catch ex As Exception
            Response.Write(ex.Message & " -- " & ex.StackTrace)
        End Try
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        Me.PanelBuscar.visible = False
        Me.pnlPregunta.visible = True
        Me.lblFecha.text = Me.lblHorario.text
        Me.lblActividad.text = txtDescripcion.text


    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        Me.PanelBuscar.visible = True
        Me.pnlPregunta.visible = False

    End Sub

    Protected Sub btnSi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSi.Click
        'Elimnar el registro de horario


        If Session("h_codigolho_REC") IsNot Nothing Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("HorarioPE_EliminarLH", CInt(Session("h_codigolho_REC")))
            obj.CerrarConexion()
            obj = Nothing
            Session("h_codigolho_REC") = 0
            Session("h_codigo_cup_REC") = 0
            Me.PanelHorarioRegistro.visible = False
            Me.PanelExaRec.visible = True
            Me.btnBuscar.enabled = True
            Me.btnGenerar.enabled = True
            Me.pnlPregunta.visible = False
        End If

    End Sub

    Protected Sub btnCopiarHorario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopiarHorario.Click
        Dim Fila As GridViewRow
        Dim FilaPadre As GridViewRow
        Dim FilaHijo As GridViewRow
        Dim dt As New DataTable

        Dim sw As Byte = 0
        Dim numPadres As Integer = 0
        Dim numHijos As Integer = 0

        ' #Solo puede eligir un registro principal
        For I As Int16 = 0 To Me.gvProgramado.Rows.Count - 1
            Fila = Me.gvProgramado.Rows(I)
            If Fila.RowType = DataControlRowType.DataRow Then
                If CType(Fila.FindControl("chkElegirPadre"), CheckBox).Checked = True Then
                    numPadres = numPadres + 1
                End If
            End If
        Next

        If (numPadres > 1) Then
            Me.lblMensaje.Text = "Solo puede elegir un registro principal"
            Exit Sub
        Else
            Me.lblMensaje.Text = ""
        End If

        '# Debe elegir por lo menos 1 registro hijo
        For I As Int16 = 0 To Me.gvProgramado.Rows.Count - 1
            Fila = Me.gvProgramado.Rows(I)
            If Fila.RowType = DataControlRowType.DataRow Then
                If CType(Fila.FindControl("chkElegirHijo"), CheckBox).Checked = True Then
                    numHijos = numHijos + 1
                End If
            End If
        Next

        If (numHijos < 1) Then
            Me.lblMensaje.Text = "Debe elegir por lo menos 1 registro sin ambiente"
            Exit Sub
        Else
            Me.lblMensaje.Text = ""
        End If


        '#Copiar horarios
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

      


        For I As Int16 = 0 To Me.gvProgramado.Rows.Count - 1
            FilaPadre = Me.gvProgramado.Rows(I)
            If FilaPadre.RowType = DataControlRowType.DataRow Then
                If CType(FilaPadre.FindControl("chkElegirPadre"), CheckBox).Checked = True Then
                    For j As Integer = 0 To Me.gvProgramado.Rows.Count - 1
                        FilaHijo = Me.gvProgramado.Rows(j)
                        If FilaHijo.RowType = DataControlRowType.DataRow Then
                            If CType(FilaHijo.FindControl("chkElegirHijo"), CheckBox).Checked = True Then
                                obj.AbrirConexion()
                                obj.Ejecutar("Acad_RecuperacionCopiarHorario", gvProgramado.DataKeys(I).Values("codigo_cup"), gvProgramado.DataKeys(j).Values("codigo_cup"))
                                obj.CerrarConexion()
                            End If
                        End If
                    Next
                End If
            End If
        Next

        btnBuscar_Click(sender, e)
    End Sub
End Class


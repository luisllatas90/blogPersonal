Imports System.Security.Cryptography

Partial Class CriteriosPrioridad
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../sinacceso.html")
        End If

        If Not IsPostBack Then
            consultar()
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
            If Request("mod") = Nothing Then
                dt = cls.TraerDataTable("MAT_consultarTipoFuncion", Session("id_per"), 1)

            Else
                dt = cls.TraerDataTable("MAT_consultarTipoFuncion", Session("id_per"), Request("mod"))

            End If
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

    Private Sub consultar()

        Dim objcnx As New ClsConectarDatos
        Dim objFun As New ClsFunciones
        objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objcnx.AbrirConexion()
        objFun.CargarListas(Me.cboTipoEstudio, objcnx.TraerDataTable("ACAD_ConsultarTipoEstudio", "TO", 0), "codigo_test", "descripcion_test")
        objFun.CargarListas(Me.cboCicloAcademico, objcnx.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")
        objcnx.CerrarConexion()
        Me.cboTipoEstudio.SelectedValue = 2

        ' gvCronograma.HeaderRow.TableSection = TableRowSection.TableHeader

    End Sub

    'Protected Sub gvCronograma_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCronograma.RowCommand
    '    Try
    '        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
    '        Dim codigo_crmat As Integer = 0
    '        Dim codigo_cac As Integer = 0
    '        Dim codigo_act As Integer = 0
    '        Dim codigo_test As Integer = 0

    '        If (e.CommandName = "configurar") Then
    '            tableMat.Visible = True
    '            Me.divCro.Visible = False
    '            Me.divCroMat.Visible = True
    '            tituloMat.InnerHtml = "[" & cboTipoEstudio.SelectedItem.Text & "] " & gvCronograma.DataKeys(index).Values("Ciclo Acad.").ToString & " " & gvCronograma.DataKeys(index).Values("Actividad").ToString
    '            codigo_cac = gvCronograma.DataKeys(index).Values("codigo_Cac")
    '            codigo_act = gvCronograma.DataKeys(index).Values("codigo_Act")
    '            codigo_test = gvCronograma.DataKeys(index).Values("codigo_test")
    '            Session("selcodigo_Cac") = codigo_cac
    '            Session("selcodigo_Act") = codigo_act
    '            Session("selcodigo_Test") = codigo_test

    '            'Response.Write(Session("selcodigo_Test"))
    '            consultarMatricula(codigo_crmat, codigo_cac, codigo_act, codigo_test)

    '        End If

    '        ' gvCronograma.HeaderRow.TableSection = TableRowSection.TableHeader
    '    Catch ex As Exception
    '        'Response.Write(ex.Message)

    '    End Try
    'End Sub

    'Protected Sub gvCronograma_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvCronograma.RowUpdating
    '    Dim objcnx As New ClsConectarDatos
    '    Dim fechaini, fechafin As Date
    '    Dim observacion As String
    '    Try
    '        If Date.TryParse(e.NewValues(0), fechaini) And Date.TryParse(e.NewValues(1), fechafin) Then
    '            fechaini = e.NewValues(0)
    '            fechafin = e.NewValues(1)
    '            observacion = ""
    '            observacion = IIf(e.NewValues(2) Is DBNull.Value Or e.NewValues(2) = "", "", e.NewValues(2))
    '            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '            objcnx.AbrirConexion()
    '            objcnx.Ejecutar("MAT_ActualizarCronograma", fechaini, fechafin, observacion, Me.gvCronograma.DataKeys.Item(e.RowIndex).Values(0).ToString, Session("id_per"))
    '            objcnx.CerrarConexion()
    '            Me.gvCronograma.EditIndex = -1
    '        Else
    '            Response.Write("<script language=javascript>alert('Por favor verificar las fechas antes de actualizar');</script>")
    '        End If

    '    Catch ex As Exception
    '    Finally
    '        e.Cancel = True
    '    End Try

    'End Sub

    'Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
    '    Dim objcnx As New ClsConectarDatos
    '    Try
    '        objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        objcnx.AbrirConexion()
    '        objcnx.Ejecutar("ACAD_GeneraCronograma", Me.cboCicloAcademico.SelectedValue, cboTipoEstudio.SelectedValue, Session("id_per"))
    '        objcnx.CerrarConexion()
    '    Catch ex As Exception
    '        Response.Write("Error: " & ex.Message)
    '    End Try
    'End Sub

    'Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
    '    Dim objcnx As New ClsConectarDatos
    '    Dim codigo_tfu As Integer = CInt(Desencriptar(Me.hdFn.Value.ToString))
    '    Dim rpta As Boolean = False
    '    Try

    '        objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        objcnx.AbrirConexion()
    '        rpta = objcnx.Ejecutar("ACAD_GeneraCronogramaV2", Me.cboCicloAcademico.SelectedValue, cboTipoEstudio.SelectedValue, Session("id_per"), codigo_tfu)
    '        objcnx.CerrarConexion()

    '        If rpta Then
    '            consultar()
    '        End If



    '    Catch ex As Exception
    '        Response.Write("Error: " & ex.Message)
    '    End Try
    'End Sub



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

    Private Sub consultarMatricula(ByVal codigo_crmat As Integer, ByVal codigo_Cac As Integer, ByVal codigo_Act As Integer, ByVal codigo_Test As Integer)
        Try
            ' divLoad.Visible = True

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("cronograma_matriculaListar", codigo_crmat, codigo_Cac, codigo_Act, codigo_Test)
            'Me.lstGrupo.DataSource = tb
            'Me.lstGrupo.DataBind()
            obj.CerrarConexion()
            obj = Nothing
            'lstGrupo.HeaderRow.TableSection = TableRowSection.TableHeader

            'divLoad.Visible = False

        Catch ex As Exception

        End Try

    End Sub


    Sub limpiar()
        'Me.txtppsi.Value = String.Empty
        'Me.txtppsf.Value = String.Empty
        'Me.txtid.Value = String.Empty
        'Me.txtfi.Text = String.Empty
        'Me.txtff.Text = String.Empty
        'Me.tdRegistro.InnerHtml = String.Empty
    End Sub

    'Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
    '    Try

    '        Dim obj As New ClsConectarDatos
    '        Dim lblResultado As Boolean


    '        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        obj.AbrirConexion()

    '        If Validar() Then

    '            If txtid.Value = "" Then
    '                lblResultado = obj.Ejecutar("cronograma_matriculaRegistro", "I", 0, CInt(Session("selcodigo_Cac")), CInt(Session("selcodigo_Act")), CInt(Session("selcodigo_Test")), txtppsi.Value, txtppsf.Value, txtfi.Text, txtff.Text, 1, Session("perlogin"))
    '            Else
    '                lblResultado = obj.Ejecutar("cronograma_matriculaRegistro", "A", Desencriptar(txtid.Value), CInt(Session("selcodigo_Cac")), CInt(Session("selcodigo_Act")), CInt(Session("selcodigo_Test")), txtppsi.Value, txtppsf.Value, txtfi.Text, txtff.Text, 1, Session("perlogin"))
    '            End If
    '            ' Response.Write(Session("selcodigo_Act"))
    '            'Response.Write(Session("selcodigo_Cac"))

    '            obj.CerrarConexion()
    '            If lblResultado Then
    '                ' Response.Write("V")
    '                consultarMatricula(0, CInt(Session("selcodigo_Cac")), CInt(Session("selcodigo_Act")), CInt(Session("selcodigo_Test")))
    '                cancelar()
    '                consultar()
    '            Else
    '                Response.Write("Error")
    '            End If

    '        End If

    '    Catch ex As Exception
    '        Response.Write(ex.Message)

    '    End Try
    'End Sub

    'Protected Sub gvCronograma_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCronograma.RowDataBound
    '    Dim index As Integer
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim btn As button
    '        btn = e.Row.FindControl("BtnMostrar")
    '        index = e.Row.RowIndex

    '        btn.enabled = False
    '        If Me.cboTipoEstudio.SelectedValue = 2 Or Me.cboTipoEstudio.SelectedValue = 10 Then
    '            If (gvCronograma.DataKeys(e.Row.RowIndex).Values("codigo_Act") = "1" Or gvCronograma.DataKeys(e.Row.RowIndex).Values("codigo_Act") = "48" _
    '                    Or gvCronograma.DataKeys(e.Row.RowIndex).Values("codigo_Act") = "49" _
    '                    Or gvCronograma.DataKeys(e.Row.RowIndex).Values("codigo_Act") = "50" _
    '                    Or gvCronograma.DataKeys(e.Row.RowIndex).Values("codigo_Act") = "51") Then
    '                btn.Enabled = True
    '                '9 MATRICULA VÍA CAMPUS ASESOR
    '                '12 MATRICULA VIA CAMPUS COORD. ACADEMICA
    '                '1 MATRICULA VÍA CAMPUS ESTUDIANTE 
    '                '37	MATRICULA: VÍA CAMPUS DIR. ESCUELA
    '            End If
    '        End If
    '    End If

    'End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click

        'Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        'Me.GvAlumnos.DataSource = obj.TraerDataTable("EVE_ConsultarAlumnosPorModulo_v2", -1, trim(txtcodigo.text))

        'divLista.visible = True
        gvLista.visible = True
        'Me.GvAlumnos.visible = True

        'Me.GvAlumnos.DataBind()

        'ocultar()

    End Sub
End Class


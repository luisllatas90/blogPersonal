Imports System.Security.Cryptography

Partial Class academico_matricula_administrar_AdministrarCronograma
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If Not IsPostBack Then

            Me.hdFn.Value = fnVerificarFuncion()
            If Me.hdFn.Value = 0 Then
                Response.Redirect("../../../../sinacceso.html")
            Else
                Me.hdFn.Value = Encriptar(Me.hdFn.Value.ToString)
                Me.hd.Value = Me.hdFn.Value.ToString
                tableMat.Visible = False
                divLoad.Visible = False
                consultar()
            End If

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

    Protected Sub gvCronograma_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCronograma.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim codigo_crmat As Integer = 0
            Dim codigo_cac As Integer = 0
            Dim codigo_act As Integer = 0
           
            If (e.CommandName = "configurar") Then
                tableMat.Visible = True
                Me.divCro.Visible = False
                Me.divCroMat.Visible = True
                tituloMat.InnerHtml = "[" & cboTipoEstudio.SelectedItem.Text & "] " & gvCronograma.DataKeys(index).Values("Ciclo Acad.").ToString & " " & gvCronograma.DataKeys(index).Values("Actividad").ToString
                codigo_cac = gvCronograma.DataKeys(index).Values("codigo_Cac")
                codigo_act = gvCronograma.DataKeys(index).Values("codigo_Act")
                Session("selcodigo_Cac") = codigo_cac
                Session("selcodigo_Act") = codigo_act


                consultarMatricula(codigo_crmat, codigo_cac, codigo_act)

            End If

            ' gvCronograma.HeaderRow.TableSection = TableRowSection.TableHeader
        Catch ex As Exception
            'Response.Write(ex.Message)

        End Try
    End Sub

    Protected Sub lstGrupo_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles lstGrupo.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim codigo_crmat As Integer = 0

            If (e.CommandName = "Editar") Then
                tableMat.Visible = True
                Me.divCro.Visible = False
                Me.divCroMat.Visible = True
                'tituloMat.InnerHtml = "[" & cboTipoEstudio.SelectedItem.Text & "] " & gvCronograma.DataKeys(index).Values("Ciclo Acad.").ToString & " " & gvCronograma.DataKeys(index).Values("Actividad").ToString
                codigo_crmat = lstGrupo.DataKeys(index).Values("codigo_crmat")

                ' Response.Write(codigo_crmat)
                Dim obj As New ClsConectarDatos
                Dim tb As New Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                tb = obj.TraerDataTable("cronograma_matriculaListar", codigo_crmat, 0, 0)
                obj.CerrarConexion()
                obj = Nothing
                nuevo()
                txtid.Value = Encriptar(codigo_crmat)
                tdRegistro.InnerHtml = "Editar Registro PPS INI - PPS FIN: " & tb.Rows(0).Item(3).ToString & "-" & tb.Rows(0).Item(4).ToString
                If tb.Rows.Count > 0 Then
                    Me.txtppsi.Value = tb.Rows(0).Item(3)
                    Me.txtppsf.Value = tb.Rows(0).Item(4)
                    Me.txtfi.Text = CDate(tb.Rows(0).Item(5)).ToString("dd/MM/yyyy HH:mm")
                    Me.txtff.Text = CDate(tb.Rows(0).Item(6)).ToString("dd/MM/yyyy HH:mm")

                End If
                Me.txtppsi.Focus()

            ElseIf (e.CommandName = "Eliminar") Then
                Dim obj As New ClsConectarDatos
                Dim lblResultado As Boolean
                codigo_crmat = lstGrupo.DataKeys(index).Values("codigo_crmat")
                'Response.Write("id: " & Desencriptar(txtid.Value))
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                lblResultado = obj.Ejecutar("cronograma_matriculaRegistro", "E", codigo_crmat, 0, 0, 0, 0, "01/01/1901", "01/01/1901", 0, Session("perlogin"))
                obj.CerrarConexion()

                If lblResultado Then
                    consultarMatricula(0, CInt(Session("selcodigo_Cac")), CInt(Session("selcodigo_Act")))
                Else
                    Response.Write("Error")
                End If
            End If

            ' gvCronograma.HeaderRow.TableSection = TableRowSection.TableHeader
        Catch ex As Exception
            'Response.Write(ex.Message)

        End Try
    End Sub

    Protected Sub gvCronograma_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvCronograma.RowUpdating
        Dim objcnx As New ClsConectarDatos
        Dim fechaini, fechafin As Date
        Dim observacion As String
        Try
            If Date.TryParse(e.NewValues(0), fechaini) And Date.TryParse(e.NewValues(1), fechafin) Then
                fechaini = e.NewValues(0)
                fechafin = e.NewValues(1)
                observacion = ""
                observacion = IIf(e.NewValues(2) Is DBNull.Value Or e.NewValues(2) = "", "", e.NewValues(2))
                objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                objcnx.AbrirConexion()
                objcnx.Ejecutar("MAT_ActualizarCronograma", fechaini, fechafin, observacion, Me.gvCronograma.DataKeys.Item(e.RowIndex).Values(0).ToString, Session("id_per"))
                objcnx.CerrarConexion()
                Me.gvCronograma.EditIndex = -1
            Else
                Response.Write("<script language=javascript>alert('Por favor verificar las fechas antes de actualizar');</script>")
            End If

        Catch ex As Exception
        Finally
            e.Cancel = True
        End Try

    End Sub

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

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim objcnx As New ClsConectarDatos
        Dim codigo_tfu As Integer = CInt(Desencriptar(Me.hdFn.Value.ToString))
        Dim rpta As Boolean = False
        Try

            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()
            rpta = objcnx.Ejecutar("ACAD_GeneraCronogramaV2", Me.cboCicloAcademico.SelectedValue, cboTipoEstudio.SelectedValue, Session("id_per"), codigo_tfu)
            objcnx.CerrarConexion()

            If rpta Then
                consultar()
            End If



        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub



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

    Private Sub consultarMatricula(ByVal codigo_crmat As Integer, ByVal codigo_Cac As Integer, ByVal codigo_Act As Integer)
        Try
            divLoad.Visible = True

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("cronograma_matriculaListar", codigo_crmat, codigo_Cac, codigo_Act)
            Me.lstGrupo.DataSource = tb
            Me.lstGrupo.DataBind()
            obj.CerrarConexion()
            obj = Nothing
            'lstGrupo.HeaderRow.TableSection = TableRowSection.TableHeader


            divLoad.Visible = False
        Catch ex As Exception

        End Try
        
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        cancelar()
    End Sub

    Sub cancelar()
        Me.btnConsultar.Enabled = True
        Me.Registro.Visible = False
        Me.lstGrupo.Visible = True
        Me.btnNuevo.Enabled = True
        Me.tdRegistro.InnerHtml = String.Empty
        limpiar()
        'lstGrupo.HeaderRow.TableSection = TableRowSection.TableHeader
    End Sub
    Sub limpiar()
        Me.txtppsi.Value = String.Empty
        Me.txtppsf.Value = String.Empty
        Me.txtid.Value = String.Empty
        Me.txtfi.Text = String.Empty
        Me.txtff.Text = String.Empty
        Me.tdRegistro.InnerHtml = String.Empty
    End Sub
    Sub nuevo()
        Me.Registro.Visible = True
        ' Me.lstGrupo.Visible = False
        Me.btnConsultar.Enabled = False
        Me.btnNuevo.Enabled = False
        Me.tdRegistro.InnerHtml = "Nuevo Registro"
        Me.txtppsi.Focus()
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        nuevo()
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Me.divCro.Visible = True
        Me.divCroMat.Visible = False
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try

            Dim obj As New ClsConectarDatos
            Dim lblResultado As Boolean
           

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

         

            If Validar() Then



                If txtid.Value = "" Then
                    lblResultado = obj.Ejecutar("cronograma_matriculaRegistro", "I", 0, CInt(Session("selcodigo_Cac")), CInt(Session("selcodigo_Act")), txtppsi.Value, txtppsf.Value, txtfi.Text, txtff.Text, 1, Session("perlogin"))
                Else
                    lblResultado = obj.Ejecutar("cronograma_matriculaRegistro", "A", Desencriptar(txtid.Value), CInt(Session("selcodigo_Cac")), CInt(Session("selcodigo_Act")), txtppsi.Value, txtppsf.Value, txtfi.Text, txtff.Text, 1, Session("perlogin"))
                End If
                ' Response.Write(Session("selcodigo_Act"))
                'Response.Write(Session("selcodigo_Cac"))

                obj.CerrarConexion()
                If lblResultado Then
                    ' Response.Write("V")
                    consultarMatricula(0, CInt(Session("selcodigo_Cac")), CInt(Session("selcodigo_Act")))
                    cancelar()
                    consultar()
                Else
                    Response.Write("Error")
                End If




            End If

        Catch ex As Exception
            Response.Write(ex.Message)

        End Try
    End Sub

    Private Function Validar() As Boolean
        Dim ni As Decimal
        Dim nf As Decimal
        Dim fi As Date
        Dim ff As Date

        ni = CDec(Me.txtppsi.Value)
        nf = CDec(Me.txtppsf.Value)

        fi = CDate(Me.txtfi.Text)
        ff = CDate(Me.txtff.Text)
        

        If ni > nf Then
            'Response.Write(ni)
            'Response.Write(nf)
            Response.Write("(*) Prom. Pond. Semestral Final debe ser mayor a Prom. Pond. Semestral Incial")
            Return False
        End If

        If fi > ff Then
            'Response.Write(fi)
            'Response.Write(ff)
            Response.Write("(*) Fecha Fin debe ser mayor a Fecha Inicio")
            Return False
        End If
        

        Return True

    End Function


    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click

        consultarMatricula(0, CInt(Session("selcodigo_Cac")), CInt(Session("selcodigo_Act")))
    End Sub

    Protected Sub gvCronograma_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCronograma.RowDataBound
        Dim index As Integer
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As button
            btn = e.Row.FindControl("BtnMostrar")
            index = e.Row.RowIndex

            btn.enabled = False
            If Me.cboTipoEstudio.SelectedValue = 2 Then
                If (gvCronograma.DataKeys(e.Row.RowIndex).Values("codigo_Act") = "1" Or gvCronograma.DataKeys(e.Row.RowIndex).Values("codigo_Act") = "9" _
                        Or gvCronograma.DataKeys(e.Row.RowIndex).Values("codigo_Act") = "12" _
                        Or gvCronograma.DataKeys(e.Row.RowIndex).Values("codigo_Act") = "37") Then
                    btn.Enabled = True
                    '9 MATRICULA VÍA CAMPUS ASESOR
                    '12 MATRICULA VIA CAMPUS COORD. ACADEMICA
                    '1 MATRICULA VÍA CAMPUS ESTUDIANTE 
                    '37	MATRICULA: VÍA CAMPUS DIR. ESCUELA
                End If
            End If
        End If

    End Sub

  
End Class


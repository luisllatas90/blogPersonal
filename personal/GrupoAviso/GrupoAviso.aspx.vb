Imports System.Security.Cryptography

Partial Class GruposAviso_GrupoAviso
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        
            If Not Page.IsPostBack Then
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                Dim objFun As New ClsFunciones

                objFun.CargarListas(Me.ddlTipoEstudio, obj.TraerDataTable("ACAD_ConsultarTipoEstudio", "TO", 0), "codigo_test", "descripcion_test", " --SELECCIONE TIPO DE ESTUDIOS-- ")
                objFun.CargarListas(Me.ddlTipo, obj.TraerDataTable("GrupoAvisoTipoListar", 0, "%"), "codigo_Tga", "nombre_tga", " --SELECCIONE TIPO DE AVISO-- ")
                objFun.CargarListas(Me.ddlFacultad, obj.TraerDataTable("ConsultarFacultad", "TO", "%"), "codigo_Fac", "nombre_Fac", " --SELECCIONE FACULTAD-- ")
                obj.CerrarConexion()
                cargarEscuela()
                consultar()
                'limpiar()


            End If
            lstGrupo.HeaderRow.TableSection = TableRowSection.TableHeader
        Catch ex As Exception
            Response.Write(ex.message)
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        nuevo()
    End Sub
    Sub nuevo()
        Me.Registro.Visible = True
        Me.lstGrupo.Visible = False
        Me.btnConsultar.Enabled = False
        Me.btnNuevo.Enabled = False
    End Sub
    Protected Sub ddlFacultad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFacultad.SelectedIndexChanged
        cargarEscuela()
        concatenarNombreGrupo()
    End Sub

    Protected Sub ddlTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoEstudio.SelectedIndexChanged
        cargarEscuela()
        concatenarNombreGrupo()
    End Sub

    Private Sub cargarEscuela()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim objFun As New ClsFunciones
        objFun.CargarListas(Me.ddlEscuela, obj.TraerDataTable("MAT_BuscaFacultad_v2", 0, Me.ddlFacultad.SelectedValue, Me.ddlTipoEstudio.SelectedValue), "codigo_Cpf", "nombre_Cpf", " --SELECCIONE ESCUELA-- ")
        obj.CerrarConexion()
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

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        cancelar()
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try

            Dim obj As New ClsConectarDatos
            Dim lblResultado As Boolean
            Dim tipo As Integer = Me.ddlTipo.SelectedValue
            Dim facultad As Integer = Me.ddlFacultad.SelectedValue
            Dim escuela As Integer = Me.ddlEscuela.SelectedValue
            Dim test As Integer = Me.ddlTipoEstudio.SelectedValue

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            If tipo < 0 Then tipo = 0

            If facultad < 0 Then facultad = 0
            If escuela < 0 Then escuela = 0
            If test < 0 Then test = 0

            If txtid.Value = "" Then
                lblResultado = obj.Ejecutar("GrupoAvisoIAE", "I", 0, Me.txtNombre.Value, tipo, facultad, escuela, test, Session("perlogin"), 1)
            Else
                lblResultado = obj.Ejecutar("GrupoAvisoIAE", "A", Desencriptar(txtid.Value), Me.txtNombre.Value, tipo, facultad, escuela, test, Session("perlogin"), 1)
            End If

            obj.CerrarConexion()
            If lblResultado Then
                ' Response.Write("V")
                cancelar()
                consultar()
            Else
                Response.Write("Error")
            End If

        Catch ex As Exception
            Response.Write(ex.Message)

        End Try
    End Sub

    Sub cancelar()
        Me.btnConsultar.Enabled = True
        Me.Registro.Visible = False
        Me.lstGrupo.Visible = True
        Me.btnNuevo.Enabled = True
        limpiar()
        lstGrupo.HeaderRow.TableSection = TableRowSection.TableHeader
    End Sub

    Sub limpiar()
        Me.txtNombre.Value = String.Empty
        Me.txtid.Value = String.Empty
        Me.ddlTipo.SelectedIndex = 0
        Me.ddlFacultad.SelectedIndex = 0
        Me.ddlTipoEstudio.SelectedIndex = 0
        Me.ddlEscuela.SelectedIndex = 0
    End Sub

    Sub concatenarNombreGrupo()
        Try
            If Me.ddlTipo.SelectedItem.Text.Trim = "AVISOS USAT" Then
                Me.txtNombre.Value = Me.ddlTipo.SelectedItem.Text & "/ TODO USAT"
            ElseIf Me.ddlTipo.SelectedItem.Text.Trim = "AVISOS FACULTAD" Then
                Me.txtNombre.Value = Me.ddlTipo.SelectedItem.Text & "/" & Me.ddlFacultad.SelectedItem.Text
            Else


                Me.txtNombre.Value = Me.ddlTipo.SelectedItem.Text & "/" & Me.ddlFacultad.SelectedItem.Text & "/" & Me.ddlEscuela.SelectedItem.Text & "/TIPO ESTUDIO: " & Me.ddlTipoEstudio.SelectedItem.Text

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipo.SelectedIndexChanged
        concatenarNombreGrupo()
    End Sub

    Protected Sub ddlEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEscuela.SelectedIndexChanged
        concatenarNombreGrupo()
        ' Response.Write(Me.ddlEscuela.SelectedItem.Text.Trim)

    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        consultar()
    End Sub
    Private Sub consultar()

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("GrupoAvisoListar", 0, "%", 0)
        Me.lstGrupo.DataSource = tb
        Me.lstGrupo.DataBind()
        obj.CerrarConexion()
        obj = Nothing
        lstGrupo.HeaderRow.TableSection = TableRowSection.TableHeader
    End Sub
    Private Sub Acceso()
        Me.Accesos.Visible = True
        Me.lstGrupo.Visible = False
        Me.btnConsultar.Enabled = False
        Me.btnNuevo.Enabled = False
    End Sub
    Sub cancelarAcceso()
        Me.btnConsultar.Enabled = True
        Me.Accesos.Visible = False
        Me.lstGrupo.Visible = True
        Me.btnNuevo.Enabled = True
        lstGrupo.HeaderRow.TableSection = TableRowSection.TableHeader
    End Sub
    Protected Sub lstGrupo_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles lstGrupo.RowCommand

        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            If (e.CommandName = "Editar") Then
                Dim obj As New ClsConectarDatos
                Dim tb As New Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                tb = obj.TraerDataTable("GrupoAvisoListar", lstGrupo.DataKeys(index).Values("codigo_Gav"), "%", 0)
                obj.CerrarConexion()
                obj = Nothing
                nuevo()
                txtid.Value = Encriptar(lstGrupo.DataKeys(index).Values("codigo_Gav"))
                tdRegistro.InnerHtml = lstGrupo.Rows(index).Cells(1).Text.Trim.ToString
                If tb.Rows.Count > 0 Then
                    Me.txtNombre.Value = tb.Rows(0).Item(1)
                    Me.ddlTipo.SelectedValue = tb.Rows(0).Item(2)
                    Me.ddlFacultad.SelectedValue = tb.Rows(0).Item(4)
                    If Not IsDBNull(tb.Rows(0).Item(6)) Then Me.ddlTipoEstudio.SelectedValue = tb.Rows(0).Item(6)
                    If Not IsDBNull(tb.Rows(0).Item(6)) Then cargarEscuela()
                    If Not IsDBNull(tb.Rows(0).Item(5)) Then Me.ddlEscuela.SelectedValue = tb.Rows(0).Item(5)
                End If
                Me.txtNombre.Focus()
                lstGrupo.HeaderRow.TableSection = TableRowSection.TableHeader
            ElseIf (e.CommandName = "Acceso") Then
                Dim obj As New ClsConectarDatos
                Dim tb As New Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                tb = obj.TraerDataTable("GrupoAvisoListar", lstGrupo.DataKeys(index).Values("codigo_Gav"), "%", 0)
                obj.CerrarConexion()
                obj = Nothing
                Acceso()
                txtidg.Value = Encriptar(lstGrupo.DataKeys(index).Values("codigo_Gav"))
                tdRegistroG.InnerHtml = "ACCESOS - " & lstGrupo.Rows(index).Cells(1).Text.Trim.ToString
                'If tb.Rows.Count > 0 Then
                '    Me.txtNombre.Value = tb.Rows(0).Item(1)
                '    Me.ddlTipo.SelectedValue = tb.Rows(0).Item(2)
                '    Me.ddlFacultad.SelectedValue = tb.Rows(0).Item(4)
                '    If Not IsDBNull(tb.Rows(0).Item(6)) Then Me.ddlTipoEstudio.SelectedValue = tb.Rows(0).Item(6)
                '    If Not IsDBNull(tb.Rows(0).Item(6)) Then cargarEscuela()
                '    If Not IsDBNull(tb.Rows(0).Item(5)) Then Me.ddlEscuela.SelectedValue = tb.Rows(0).Item(5)
                'End If

                consultarPersonal(lstGrupo.DataKeys(index).Values("codigo_Gav"))
                lstGrupo.HeaderRow.TableSection = TableRowSection.TableHeader

            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Function validaCheckActivo() As Boolean
        Dim Fila As GridViewRow
        Dim sw As Byte = 0
        Dim d As Integer = 0
        Dim destinatarios As String = ""
        'Me.lblDestinatario.Text = ""
        For i As Integer = 0 To Me.lstAcceso.Rows.Count - 1
            'Capturamos las filas que estan activas
            Fila = Me.lstAcceso.Rows(i)
            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
            If (valor = True) Then
                d = d + 1
                sw = 1
            End If
        Next
        If (sw = 1) Then
            Return True
        End If

        Return False
    End Function
    Private Sub consultarPersonal(ByVal codigo_Gav As Integer)
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("GrupoAvisoDetalleListar", codigo_Gav)
        Me.lstAcceso.DataSource = tb
        Me.lstAcceso.DataBind()
        obj.CerrarConexion()
        obj = Nothing

        lstAcceso.HeaderRow.TableSection = TableRowSection.TableHeader
    End Sub

    Protected Sub btnCancelarAcceso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarAcceso.Click
        cancelarAcceso()
    End Sub

    Protected Sub lstAcceso_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles lstAcceso.RowDataBound
        Dim index As Integer = 0
        If e.Row.RowType = DataControlRowType.DataRow Then
            index = e.Row.RowIndex
            Dim checkAcceso As CheckBox
            checkAcceso = e.Row.FindControl("chkElegir")

            If lstAcceso.DataKeys(index).Values("acceso") = 0 Then
                checkAcceso.Checked = False

            Else
                checkAcceso.Checked = True
            End If

        End If
    End Sub

    'Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
    '    Try
    '        Dim obj As New ClsConectarDatos
    '        Dim tb As New Data.DataTable
    '        Dim valoresdevueltos(1) As Integer

    '        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        obj.AbrirConexion()

    '        Dim Fila As GridViewRow
    '        For i As Integer = 0 To Me.gvListaBecas.Rows.Count - 1
    '            Fila = Me.gvListaBecas.Rows(i)
    '            Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
    '            If (valor = True) Then
    '                If CInt(Me.gvListaBecas.DataKeys(i).Values("codigo_bso").ToString) > 0 Then
    '                    tb = obj.TraerDataTable("Beca_RegistraSolictudBecaPersonal", _
    '                                            Me.gvListaBecas.DataKeys(i).Values("codigo_bso").ToString, _
    '                                            CInt(Me.ddlCobertura.SelectedValue))

    '                    If tb.Rows(0).Item("rpta") = -1 Then
    '                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ocurrio un error al tratar de registrar la solicitud.');", True)
    '                        Exit Sub
    '                    Else
    '                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('La solicitud fue registrada correctamente.');", True)
    '                    End If
    '                End If
    '            End If
    '        Next
    '        CargarRegistros()
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    Protected Sub btnGrabarAcceso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabarAcceso.Click
        Try
            Dim obj As New ClsConectarDatos
            Dim lblResultado As Boolean
            Dim idg As Integer
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()


            idg = CInt(Desencriptar(txtidg.Value))
            ' Response.Write(txtidg.Value)

            If txtidg.Value <> "" Then

                lblResultado = obj.Ejecutar("GrupoAvisoDetalleIAE", "E", 0, idg, 0)
                Dim Fila As GridViewRow
                For i As Integer = 0 To Me.lstAcceso.Rows.Count - 1
                    Fila = Me.lstAcceso.Rows(i)
                    Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                    If (valor = True) Then
                        ' Response.Write(CInt(Me.lstAcceso.DataKeys(i).Values("codigo_per").ToString) & "<br>")
                        lblResultado = obj.Ejecutar("GrupoAvisoDetalleIAE", "I", 0, idg, CInt(Me.lstAcceso.DataKeys(i).Values("codigo_per").ToString))
                    End If
                Next
            End If
            '  lblResultado = obj.Ejecutar("GrupoAvisoIAE", "I", 0, Me.txtNombre.Value, tipo, facultad, escuela, test, Session(





            If lblResultado Then

                cancelarAcceso()

            Else
                Response.Write("Error")


            End If

            obj.CerrarConexion()
        Catch ex As Exception
            Response.Write(ex.Message)

        End Try
    End Sub


End Class

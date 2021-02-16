Imports System.Security.Cryptography

Partial Class categoria
    Inherits System.Web.UI.Page

    '  Private _id As Integer = 0
    ' Private condicion As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim objFun As New ClsFunciones

            objFun.CargarListas(Me.ddlCiclo, obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")
            objFun.CargarListas(Me.ddlEscuela, obj.TraerDataTable("EVE_ConsultarCarreraProfesional", "2", 1, 684), "codigo_cpf", "nombre_cpf")
            objFun.CargarListas(Me.ddlCicloReg, obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")
            objFun.CargarListas(Me.ddlEscuelaReg, obj.TraerDataTable("EVE_ConsultarCarreraProfesional", "2", 1, 684), "codigo_cpf", "nombre_cpf")
            objFun.CargarListas(Me.ddlCatReg, obj.TraerDataTable("CategoriaPensionConsultar", 0, "%"), "codigo", "categoria")
            Me.ddlEscuela.SelectedValue = "0"
            Me.ddlEscuelaReg.SelectedValue = "0"
            Me.ddlCicloReg.enabled = False
        End If
    End Sub




    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        consultar()

    End Sub
    Private Sub consultar()

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim Fila As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("CategoriaEstudianteIEConsultar", "1", 0, Me.ddlCiclo.SelectedValue, Me.ddlEscuela.SelectedValue, 0)       
        Me.lstReglas.DataSource = tb
        Me.lstReglas.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub



    Protected Sub lstReglas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles lstReglas.RowCommand

        Try

            Response.Write(e.CommandName)

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            If (e.CommandName = "Editar") Then
                Me.btnConsultar.Enabled = False
                Me.Registro.Visible = True
                Me.ddlCiclo.enabled = False
                Me.ddlEscuela.enabled = False
                Me.lstReglas.Enabled = False

                txtid.Value = Encriptar(lstReglas.DataKeys(index).Values("codigo"))
                ' Response.Write(lstReglas.DataKeys(index).Values("codigo"))
                ' condicion = lstReglas.Rows(index).Cells(1).Text.ToString
                tdRegistro.InnerHtml = "Nueva Categoria Por Escuela - Categoria"
                'Response.Write(lstReglas.DataKeys(index).Values("codigo_cpf"))

                Me.ddlEscuelaReg.SelectedValue = lstReglas.DataKeys(index).Values("codigo_cpf")
                Me.ddlCicloReg.selectedValue = lstReglas.DataKeys(index).Values("codigo_cac")
                Me.ddlCatReg.selectedValue = lstReglas.DataKeys(index).Values("codigo_catp")

                '  Me.ddlCicloReg.SelectedValue = lstReglas.Rows(index).Cells(5).Text.Trim.ToString
                ' Me.ddlCatReg.SelectedValue = lstReglas.Rows(index).Cells(4).Text.Trim.ToString
                'Response.Write(txtid.Value)


                '  tb = obj.TraerDataTable("acad_consolidadoxescuela_padres", "H", gData.DataKeys(index).Values("codigo_cup"), 0)
            ElseIf (e.CommandName = "Eliminar") Then
                Dim obj As New ClsConectarDatos
                Dim lblResultado As Boolean
                'Response.Write("id: " & Desencriptar(txtid.Value))
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                lblResultado = obj.Ejecutar("CategoriaEstudianteIEReg", "E", CInt(lstReglas.DataKeys(index).Values("codigo")), 0, 0, 0, Session("perlogin"))
                obj.CerrarConexion()

                If lblResultado Then
                    consultar()
                Else
                    Response.Write("Error")
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.message)
        End Try
    End Sub


    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try

       
            Dim obj As New ClsConectarDatos
            Dim lblResultado As Boolean
            'Response.Write("id: " & Desencriptar(txtid.Value))
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            If txtid.Value = "" Then
                lblResultado = obj.Ejecutar("CategoriaEstudianteIEReg", "I", 0, Me.ddlCatReg.SelectedValue, Me.ddlCicloReg.SelectedValue, Me.ddlEscuelaReg.SelectedValue, Session("perlogin"))
            Else
                lblResultado = obj.Ejecutar("CategoriaEstudianteIEReg", "A", Desencriptar(txtid.Value), Me.ddlCatReg.SelectedValue, Me.ddlCicloReg.SelectedValue, Me.ddlEscuelaReg.SelectedValue, Session("perlogin"))
            End If

            obj.CerrarConexion()
            If lblResultado Then
                ' Response.Write("V")
                cancelar()
                consultar()
                '  consultar()
            Else
                Response.Write("Error")
            End If
        Catch ex As Exception
            Response.Write(ex.message)
        End Try
    End Sub


    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
       cancelar
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

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        nuevo()
    End Sub
    Sub nuevo()
        Me.ddlCiclo.enabled = False
        Me.ddlEscuela.enabled = False
        Me.Registro.Visible = True
        Me.lstReglas.Enabled = False
        Me.btnConsultar.Enabled = False
        Me.btnNuevo.Enabled = False
        tdRegistro.InnerHtml = "Nueva Categoria Por Colegio"
        '  Me.txtnombre.focus()
    End Sub
    Sub cancelar()
        Me.ddlCiclo.enabled = True
        Me.ddlEscuela.enabled = True
        Me.btnConsultar.Enabled = True
        Me.Registro.Visible = False
        Me.lstReglas.Enabled = True
        Me.lstreglas.Visible = True
        Me.btnNuevo.Enabled = True
        Me.ddlEscuelaReg.SelectedValue = "0"
        limpiar()
    End Sub
    Sub limpiar()

        Me.txtid.Value = String.Empty
        tdRegistro.InnerHtml = ""
        Me.ddlCatReg.selectedValue = "1"
    End Sub


End Class

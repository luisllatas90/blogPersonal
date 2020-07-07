Imports System.Security.Cryptography

Partial Class categoria
    Inherits System.Web.UI.Page

    Private _id As Integer = 0
    ' Private condicion As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            consultar()

        End If
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        consultar()

    End Sub
    Private Sub consultar()
        Try
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim Fila As Integer
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("CategoriaPensionConsultar", 0, "%")

            Me.lstReglas.DataSource = tb
            Me.lstReglas.DataBind()
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.message)

        End Try
     
    End Sub



    Protected Sub lstReglas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles lstReglas.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "Editar") Then
            Me.btnConsultar.Enabled = False
            Me.Registro.Visible = True
            Me.lstReglas.Enabled = False
            txtid.Value = Encriptar(lstReglas.DataKeys(index).Values("codigo"))
            ' condicion = lstReglas.Rows(index).Cells(1).Text.ToString
            tdRegistro.InnerHtml = "Categoria " & lstReglas.Rows(index).Cells(1).Text.Trim.ToString
            txtnombre.Value = lstReglas.Rows(index).Cells(1).Text.Trim.ToString
            txtprecio.value = lstReglas.Rows(index).Cells(2).Text.Trim.ToString

            'Response.Write(txtid.Value)

            Me.txtnombre.Focus()
            '  tb = obj.TraerDataTable("acad_consolidadoxescuela_padres", "H", gData.DataKeys(index).Values("codigo_cup"), 0)

        End If
    End Sub


    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try
            Dim obj As New ClsConectarDatos
            Dim lblResultado As Boolean
            '  Response.Write("id: " & Desencriptar(txtid.Value))
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            If txtid.Value = "" Then
                lblResultado = obj.Ejecutar("CategoriaPensionReg", "I", 0, Me.txtNombre.Value, CDec(Me.txtprecio.Value), Session("perlogin"))
            Else
                lblResultado = obj.Ejecutar("CategoriaPensionReg", "A", Desencriptar(txtid.Value), Me.txtNombre.Value, CDec(Me.txtprecio.Value), Session("perlogin"))
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
            Response.Write(ex.Message)
        End Try


    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        cancelar()
    End Sub
    Sub cancelar()
        Me.btnConsultar.Enabled = True
        Me.Registro.Visible = False
        Me.lstReglas.Enabled = True
        Me.lstreglas.Visible = True
        Me.btnNuevo.Enabled = True
        limpiar()
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
        Me.Registro.Visible = True
        Me.lstReglas.Enabled = False
        Me.btnConsultar.Enabled = False
        Me.btnNuevo.Enabled = False
        tdRegistro.InnerHtml = "Nueva Categoria"
        Me.txtnombre.focus()
    End Sub

    Sub limpiar()
        Me.txtNombre.Value = String.Empty
        Me.txtprecio.Value = String.Empty
        Me.txtid.Value = String.Empty
        tdRegistro.InnerHtml = ""
    End Sub


End Class

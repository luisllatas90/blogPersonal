Imports System.Security.Cryptography

Partial Class academico_matricula_reglas_reglasmatricula
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
        tb = obj.TraerDataTable("ReglaMatriculaConsultar", "1", 0, Me.ddlCiclo.SelectedValue, "%")
        If Not tb Is Nothing Then

            If tb.Rows.Count > 0 Then
                btnCargar.Visible = False
            Else
                btnCargar.Visible = True
            End If


        End If
        Me.lstReglas.DataSource = tb
        Me.lstReglas.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub btnCargar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCargar.Click
        Dim obj As New ClsConectarDatos
        Dim lblResultado As Boolean
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        lblResultado = obj.Ejecutar("ReglaMatriculaReg", "C", 0, Me.ddlCiclo.SelectedValue, "", 0, 0)
        obj.CerrarConexion()
        If lblResultado Then
            ' Response.Write("V")
            consultar()
        Else
            btnCargar.Visible = False
            ' Response.Write("F")
        End If

    End Sub

    Protected Sub lstReglas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles lstReglas.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "Editar") Then
            Me.btnConsultar.Enabled = False
            Me.Registro.Visible = True
            Me.ddlCiclo.enabled = False
            Me.lstReglas.Enabled = False
            txtid.Value = Encriptar(lstReglas.DataKeys(index).Values("codigo_rMat"))
            ' condicion = lstReglas.Rows(index).Cells(1).Text.ToString
            tdRegistro.InnerHtml = lstReglas.Rows(index).Cells(2).Text.Trim.ToString
            txtMaxAdelantar.Value = lstReglas.Rows(index).Cells(3).Text.Trim.ToString
            txtMaxNivelar.Value = lstReglas.Rows(index).Cells(4).Text.Trim.ToString

            'Response.Write(txtid.Value)

            Me.txtMaxAdelantar.Focus()
            '  tb = obj.TraerDataTable("acad_consolidadoxescuela_padres", "H", gData.DataKeys(index).Values("codigo_cup"), 0)

        End If
    End Sub


    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        Dim lblResultado As Boolean
        'Response.Write("id: " & Desencriptar(txtid.Value))
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        lblResultado = obj.Ejecutar("ReglaMatriculaReg", "A", Desencriptar(txtid.Value), Me.ddlCiclo.SelectedValue, "", CInt(Me.txtMaxAdelantar.Value), CInt(Me.txtMaxNivelar.Value))
        obj.CerrarConexion()
        If lblResultado Then
            ' Response.Write("V")
            Me.btnConsultar.Enabled = True
            Me.Registro.Visible = False
            Me.ddlCiclo.enabled = True
            Me.lstReglas.Enabled = True
            consultar()
        Else

            Response.Write("Error")
        End If
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.btnConsultar.Enabled = True
        Me.Registro.Visible = False
        Me.ddlCiclo.enabled = True
        Me.lstReglas.Enabled = True
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
End Class

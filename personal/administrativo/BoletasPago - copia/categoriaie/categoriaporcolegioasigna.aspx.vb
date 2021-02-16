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

            objFun.CargarListas(Me.ddlDepartamento, obj.TraerDataTable("ConsultarLugares", "2", "156", "0"), "codigo_Dep", "nombre_Dep")
            objFun.CargarListas(Me.ddlProvincia, obj.TraerDataTable("ConsultarLugares", "3", Me.ddlDepartamento.selectedValue, "0"), "codigo_Pro", "nombre_Pro")
            objFun.CargarListas(Me.ddlDistrito, obj.TraerDataTable("ConsultarLugares", "4", Me.ddlProvincia.selectedValue, "0"), "codigo_Dis", "nombre_Dis")


            objFun.CargarListas(Me.ddlDepartamentoReg, obj.TraerDataTable("ConsultarLugares", "2", "156", "0"), "codigo_Dep", "nombre_Dep")
            objFun.CargarListas(Me.ddlProvinciaReg, obj.TraerDataTable("ConsultarLugares", "3", Me.ddlDepartamentoReg.selectedValue, "0"), "codigo_Pro", "nombre_Pro")
            objFun.CargarListas(Me.ddlDistritoReg, obj.TraerDataTable("ConsultarLugares", "4", Me.ddlProvinciaReg.selectedValue, "0"), "codigo_Dis", "nombre_Dis")

            objFun.CargarListas(Me.ddlCatReg, obj.TraerDataTable("CategoriaPensionConsultar", 0, "%"), "codigo", "categoria")

            objFun.CargarListas(Me.ddlColegioReg, obj.TraerDataTable("PEC_ConsultarInstitucionesEducativasPorUbicacion", "DIS", Me.ddlDistritoReg.selectedValue, Nothing), "codigo_ied", "Nombre_ied")
            obj.CerrarConexion()


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
            tb = obj.TraerDataTable("CategoriaEstudianteIEasignaConsultar", "1", 0, Me.ddlDistrito.SelectedValue, 0)
            Me.lstReglas.DataSource = tb
            Me.lstReglas.DataBind()
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.message)
        End Try

    End Sub



    Protected Sub lstReglas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles lstReglas.RowCommand
        Try

       
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            If (e.CommandName = "Editar") Then
                Me.btnConsultar.Enabled = False
                Me.Registro.Visible = True


                Me.lstReglas.Enabled = False


                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                Dim objFun As New ClsFunciones

                txtid.Value = Encriptar(lstReglas.DataKeys(index).Values("codigo"))
                ' Response.Write(lstReglas.DataKeys(index).Values("codigo"))
                ' condicion = lstReglas.Rows(index).Cells(1).Text.ToString
                tdRegistro.InnerHtml = "Asigna Categoria a Colegio"
                'Response.Write(lstReglas.DataKeys(index).Values("codigo_cpf"))

                'Me.ddlEscuelaReg.SelectedValue = lstReglas.DataKeys(index).Values("codigo_cpf")
                'Me.ddlCicloReg.selectedValue = lstReglas.DataKeys(index).Values("codigo_cac")
                Me.ddlCatReg.selectedValue = lstReglas.DataKeys(index).Values("codcategoria")
                Me.ddlDepartamentoReg.selectedvalue = lstReglas.DataKeys(index).Values("codigo_Dep")
                objFun.CargarListas(Me.ddlProvinciaReg, obj.TraerDataTable("ConsultarLugares", "3", Me.ddlDepartamentoReg.selectedValue, "0"), "codigo_Pro", "nombre_Pro")

                Me.ddlProvinciaReg.selectedValue = lstReglas.DataKeys(index).Values("codigo_Prov")
                objFun.CargarListas(Me.ddlDistritoReg, obj.TraerDataTable("ConsultarLugares", "4", Me.ddlProvinciaReg.selectedValue, "0"), "codigo_Dis", "nombre_Dis")

                Me.ddlDistritoReg.selectedValue = lstReglas.DataKeys(index).Values("codigo_Dis")

                '  tb = obj.TraerDataTable("acad_consolidadoxescuela_padres", "H", gData.DataKeys(index).Values("codigo_cup"), 0)
                objFun.CargarListas(Me.ddlColegioReg, obj.TraerDataTable("PEC_ConsultarInstitucionesEducativasPorUbicacion", "DIS", Me.ddlDistritoReg.selectedValue, Nothing), "codigo_ied", "Nombre_ied")

                Me.ddlColegioReg.selectedValue = lstReglas.DataKeys(index).Values("codcolegio")
                obj.CerrarConexion()
            End If

        Catch ex As Exception

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
                lblResultado = obj.Ejecutar("CategoriaEstudianteIEasignaRegistro", "I", 0, Me.ddlCatReg.SelectedValue, Me.ddlColegioReg.SelectedValue, Session("perlogin"))
            Else
                lblResultado = obj.Ejecutar("CategoriaEstudianteIEasignaRegistro", "A", Desencriptar(txtid.Value), Me.ddlCatReg.SelectedValue, Me.ddlColegioReg.SelectedValue, Session("perlogin"))
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
        Me.ddlDepartamento.enabled = False
        Me.ddlProvincia.enabled = False
        Me.ddlDistrito.enabled = False
        Me.Registro.Visible = True
        Me.lstReglas.Enabled = False
        Me.btnConsultar.Enabled = False
        Me.btnNuevo.Enabled = False
        tdRegistro.InnerHtml = "Asiganar Categoria a Colegio"
        '  Me.txtnombre.focus()
    End Sub
    Sub cancelar()
        Me.ddlDepartamento.enabled = True
        Me.ddlProvincia.enabled = True
        Me.ddlDistrito.enabled = True
        Me.btnConsultar.Enabled = True
        Me.Registro.Visible = False
        Me.lstReglas.Enabled = True
        Me.lstreglas.Visible = True
        Me.btnNuevo.Enabled = True
        limpiar()
    End Sub
    Sub limpiar()
        Me.txtid.Value = String.Empty
        tdRegistro.InnerHtml = ""
        Me.ddlCatReg.selectedValue = "1"
    End Sub


    Protected Sub ddlDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamento.SelectedIndexChanged
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim objFun As New ClsFunciones
            objFun.CargarListas(Me.ddlProvincia, obj.TraerDataTable("ConsultarLugares", "3", Me.ddlDepartamento.selectedValue, "0"), "codigo_Pro", "nombre_Pro")
            objFun.CargarListas(Me.ddlDistrito, obj.TraerDataTable("ConsultarLugares", "4", Me.ddlProvincia.selectedValue, "0"), "codigo_Dis", "nombre_Dis")
            obj.CerrarConexion()
        Catch ex As Exception
            Response.Write(ex.message)
        End Try
        
    End Sub

    Protected Sub ddlProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProvincia.SelectedIndexChanged
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim objFun As New ClsFunciones
            objFun.CargarListas(Me.ddlDistrito, obj.TraerDataTable("ConsultarLugares", "4", Me.ddlProvincia.selectedValue, "0"), "codigo_Dis", "nombre_Dis")
            obj.CerrarConexion()
        Catch ex As Exception
            Response.Write(ex.message)
        End Try
        
    End Sub

    Protected Sub ddlDistritoReg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDistritoReg.SelectedIndexChanged
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim objFun As New ClsFunciones
            objFun.CargarListas(Me.ddlColegioReg, obj.TraerDataTable("PEC_ConsultarInstitucionesEducativasPorUbicacion", "DIS", Me.ddlDistritoReg.selectedValue, Nothing), "codigo_ied", "Nombre_ied")
            obj.CerrarConexion()
        Catch ex As Exception
            Response.Write(ex.message)
        End Try
        
    End Sub

    Protected Sub ddlDepartamentoReg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamentoReg.SelectedIndexChanged
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim objFun As New ClsFunciones
            objFun.CargarListas(Me.ddlProvinciaReg, obj.TraerDataTable("ConsultarLugares", "3", Me.ddlDepartamentoReg.selectedValue, "0"), "codigo_Pro", "nombre_Pro")
            objFun.CargarListas(Me.ddlDistritoReg, obj.TraerDataTable("ConsultarLugares", "4", Me.ddlProvinciaReg.selectedValue, "0"), "codigo_Dis", "nombre_Dis")
            objFun.CargarListas(Me.ddlColegioReg, obj.TraerDataTable("PEC_ConsultarInstitucionesEducativasPorUbicacion", "DIS", Me.ddlDistritoReg.selectedValue, Nothing), "codigo_ied", "Nombre_ied")
            obj.CerrarConexion()
        Catch ex As Exception
            Response.Write(ex.message)
        End Try

    End Sub
End Class

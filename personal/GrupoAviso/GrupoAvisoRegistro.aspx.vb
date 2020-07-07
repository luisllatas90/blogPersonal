Imports System.Security.Cryptography
Imports System.IO

Partial Class GrupoAviso_GrupoAvisoRegistro
    Inherits System.Web.UI.Page
    Dim rutaFile As String = "../GrupoAviso/upload/file/"
    Dim rutaImg As String = "../GrupoAviso/upload/img/"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If Not Page.IsPostBack Then
                divEnviar.Visible = False

                ' Response.Write(Session("id_per"))
                consultar()
                cargarNotificacionTipo()
                'limpiar()


            End If
            lstGrupo.HeaderRow.TableSection = TableRowSection.TableHeader
        Catch ex As Exception
            Response.Write(ex.message)
        End Try
    End Sub
    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        consultar()
    End Sub
    Private Sub consultar()
        Try
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("NotificacionListar", 0, "%", "%", Session("id_per"))
            Me.lstGrupo.DataSource = tb
            Me.lstGrupo.DataBind()
            obj.CerrarConexion()
            obj = Nothing
            If tb.Rows.Count > 0 Then
                lstGrupo.HeaderRow.TableSection = TableRowSection.TableHeader
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub lstGrupo_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles lstGrupo.RowCommand

        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            If (e.CommandName = "Editar") Then
                Dim obj As New ClsConectarDatos
                Dim tb As New Data.DataTable
                Dim str As New StringBuilder
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                tb = obj.TraerDataTable("NotificacionListar", lstGrupo.DataKeys(index).Values("codigo_noti"), "", "", 0)
                obj.CerrarConexion()
                obj = Nothing
                nuevo()
                txtid.Value = Encriptar(lstGrupo.DataKeys(index).Values("codigo_noti"))
                tdRegistro.InnerHtml = lstGrupo.Rows(index).Cells(1).Text.Trim.ToString

                If tb.Rows.Count > 0 Then
                    'Me.ddlTipoNot.SelectedValue = tb.Rows(0).Item(13).ToString
                    If Not IsDBNull(tb.Rows(0).Item(13)) Then Me.ddlTipoNot.SelectedValue = tb.Rows(0).Item(12)
                    Me.txtTitulo.Value = tb.Rows(0).Item(1)
                    Me.txtDescripcion.Text = tb.Rows(0).Item(2)
                    If tb.Rows(0).Item(7).ToString.Trim <> "" Then
                        img.Visible = True
                        Me.img.ImageUrl = rutaImg & tb.Rows(0).Item(9).ToString & "-" & tb.Rows(0).Item(7).ToString
                    Else
                        img.Visible = False
                    End If

                    If tb.Rows(0).Item(4).ToString.Trim <> "" Then
                        str.Append("<ul><li><a href='" & rutaFile & tb.Rows(0).Item(6).ToString & "-" & tb.Rows(0).Item(4).ToString & "'>" & tb.Rows(0).Item(5).ToString & "</a></li></ul>")
                        'Response.Write(str)
                        Me.divLnk.InnerHtml = str.ToString
                        ' Me.img.ImageUrl = rutaImg & tb.Rows(0).Item(6).ToString & "-" & tb.Rows(0).Item(5).ToString
                    End If
                    Me.txtFileDesc.Text = tb.Rows(0).Item(5).ToString
                    Me.txtImagenDesc.Text = tb.Rows(0).Item(8).ToString

                End If
                Me.txtTitulo.Focus()
                activar(True)
                Me.txts.Value = 0
                Me.divEnviar.Visible = False
                'lstGrupo.HeaderRow.TableSection = TableRowSection.TableHeader
            ElseIf (e.CommandName = "Enviar") Then
                Dim obj As New ClsConectarDatos
                Dim tb As New Data.DataTable
                Dim str As New StringBuilder
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                tb = obj.TraerDataTable("NotificacionListar", lstGrupo.DataKeys(index).Values("codigo_noti"), "", "", 0)
                obj.CerrarConexion()
                obj = Nothing
                nuevo()
                txtid.Value = Encriptar(lstGrupo.DataKeys(index).Values("codigo_noti"))
                tdRegistro.InnerHtml = lstGrupo.Rows(index).Cells(1).Text.Trim.ToString

                If tb.Rows.Count > 0 Then
                    ddlTipoNot.SelectedValue = tb.Rows(0).Item(12)
                    Me.txtTitulo.Value = tb.Rows(0).Item(11)
                    Me.txtDescripcion.Text = tb.Rows(0).Item(2)
                    If tb.Rows(0).Item(7).ToString.Trim <> "" Then
                        Me.img.ImageUrl = rutaImg & tb.Rows(0).Item(9).ToString & "-" & tb.Rows(0).Item(7).ToString
                    End If

                    If tb.Rows(0).Item(4).ToString.Trim <> "" Then
                        str.Append("<ul><li><a href='" & rutaFile & tb.Rows(0).Item(6).ToString & "-" & tb.Rows(0).Item(4).ToString & "'>" & tb.Rows(0).Item(5).ToString & "</a></li></ul>")
                        'Response.Write(str)
                        Me.divLnk.InnerHtml = str.ToString
                        ' Me.img.ImageUrl = rutaImg & tb.Rows(0).Item(6).ToString & "-" & tb.Rows(0).Item(5).ToString
                    End If
                    Me.txtFileDesc.Text = tb.Rows(0).Item(5).ToString
                    Me.txtImagenDesc.Text = tb.Rows(0).Item(8).ToString

                End If
                Me.txtTitulo.Focus()
                activar(False)
                Me.txts.Value = 1
                Me.divEnviar.Visible = True

                If tb.Rows(0).Item(10).ToString = "Enviado" Then
                    btnGrabar.visible = False
                Else
                    btnGrabar.visible = True
                End If

                cargarGrupo()
            ElseIf (e.CommandName = "Eliminar") Then
                Dim obj As New ClsConectarDatos
                Dim lblResultado As Boolean

                'Response.Write("id: " & Desencriptar(txtid.Value))
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                '  lblResultado = obj.Ejecutar("NotificacionIAE", "A", Desencriptar(txtid.Value), Me.txtTitulo.Value, Me.txtDescripcion.Text, nombreFile1, nombreFile, txtFileDesc.Text, nombreImg1, nombreImg, txtImagenDesc.Text, Session("perlogin"), 1)
                lblResultado = obj.Ejecutar("NotificacionIAE", "E", CInt(lstGrupo.DataKeys(index).Values("codigo_noti")), "", 0, "", "", "", "", "", "", "", Session("perlogin"), 0, Session("id_per"))


                obj.CerrarConexion()

                If lblResultado Then
                    consultar()
                Else
                    Response.Write("Ha ocurrido un problema, consultar con el administrador del sistema")
                End If

            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub cargarGrupo()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim objFun As New ClsFunciones
        objFun.CargarListas(Me.ddlGrupo, obj.TraerDataTable("GrupoAvisoDetalleListar2", 0, Session("id_per")), "codigo_Gav", "nombre_Gav", " --SELECCIONE GRUPO-- ")
        obj.CerrarConexion()
    End Sub
    Private Sub cargarNotificacionTipo()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim objFun As New ClsFunciones
        objFun.CargarListas(Me.ddlTipoNot, obj.TraerDataTable("NotificacionTipoListar", 0, ""), "codigo_Tnot", "nombre_Tnot", " --SELECCIONE TIPO NOTIFICACION-- ")
        obj.CerrarConexion()
    End Sub
    Sub activar(ByVal sw As Boolean)
        Me.txtTitulo.Disabled = Not sw
        Me.txtDescripcion.Enabled = sw
        Me.fileArchivo.Enabled = sw
        Me.txtFileDesc.Enabled = sw
        Me.fileImagen.Enabled = sw
        Me.txtImagenDesc.Enabled = sw

    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        nuevo()
        Me.btnGrabar.visible = True
    End Sub

    Sub nuevo()
        Me.Registro.Visible = True
        Me.lstGrupo.Visible = False
        Me.btnConsultar.Enabled = False
        Me.btnNuevo.Enabled = False
        Me.img.Visible = False
    End Sub

    Private Sub upLoadImg()
        Try
            If IsPostBack Then
                Dim sExt As String = String.Empty
                Dim sName As String = String.Empty


                If fileImagen.HasFile Then
                    sName = CStr(Date.Now.ToString("dd-MM-yyyy HH-mm")) & "-" & fileImagen.FileName
                    sExt = Path.GetExtension(sName)
                    fileImagen.SaveAs(MapPath("~/GrupoAviso/upload/img/" & sName))
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub upLoadFile()
        Try
            If IsPostBack Then
                Dim sExt As String = String.Empty
                Dim sName As String = String.Empty

                If fileArchivo.HasFile Then
                    sName = CStr(Date.Now.ToString("dd-MM-yyyy HH-mm")) & "-" & fileArchivo.FileName
                    sExt = Path.GetExtension(sName)
                    fileArchivo.SaveAs(MapPath("~/GrupoAviso/upload/file/" & sName))

                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Function validar() As Boolean
        If Me.txtTitulo.Value = "" Then
            Response.Write("(*) Ingresar Titulo de la Notificacion")
            Me.txtTitulo.Focus()
            Return False
        End If
        If Me.txtDescripcion.Text = "" Then
            Response.Write("(*) Ingresar Descripcion de la Notificacion")
            Me.txtTitulo.Focus()
            Return False
        End If

        Return True

    End Function


    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try

            Dim obj As New ClsConectarDatos
            Dim lblResultado As Boolean
            Dim nombreFile As String = String.Empty
            Dim nombreFile1 As String = String.Empty
            Dim nombreImg As String = String.Empty
            Dim nombreImg1 As String = String.Empty


            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            nombreFile = fileArchivo.FileName
            If nombreFile <> "" Then
                nombreFile1 = CStr(Date.Now.ToString("dd-MM-yyyy HH-mm"))
            End If
            nombreImg = fileImagen.FileName
            If nombreImg <> "" Then
                nombreImg1 = CStr(Date.Now.ToString("dd-MM-yyyy HH-mm"))
            End If
            If txtid.Value = "" Then
                lblResultado = obj.Ejecutar("NotificacionIAE", "I", 0, Me.txtTitulo.Value, Me.ddlTipoNot.SelectedValue, Me.txtDescripcion.Text, nombreFile1, nombreFile, txtFileDesc.Text, nombreImg1, nombreImg, txtImagenDesc.Text, Session("perlogin"), 1, Session("id_per"))
            Else
                'Response.Write(Me.txts.Value)
                If Me.txts.Value = 1 Then
                    If CInt(ddlGrupo.SelectedValue) = -1 Then

                        Response.Write("(*) Seleccione Grupo de Aviso")
                        ddlGrupo.Focus()
                    Else
                        lblResultado = obj.Ejecutar("NotificacionAlumnoEnvio", 0, Desencriptar(txtid.Value), 0, 0, Session("id_per"), ddlGrupo.SelectedValue)
                    End If

                Else
                    lblResultado = obj.Ejecutar("NotificacionIAE", "A", Desencriptar(txtid.Value), Me.txtTitulo.Value, Me.ddlTipoNot.SelectedValue, Me.txtDescripcion.Text, nombreFile1, nombreFile, txtFileDesc.Text, nombreImg1, nombreImg, txtImagenDesc.Text, Session("perlogin"), 1, Session("id_per"))
                End If

            End If
            'Response.Write("nombreFile1: " & nombreFile1)
            obj.CerrarConexion()
            If lblResultado Then
                upLoadFile()
                upLoadImg()
                cancelar()
                consultar()
            Else
                Response.Write("Ha ocurrido un problema, consultar con el administrador del sistema")
            End If

        Catch ex As Exception
            Response.Write(ex.Message)

        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        cancelar()
       
    End Sub

    Sub cancelar()
        Try
            Me.btnConsultar.Enabled = True
            Me.Registro.Visible = False
            Me.lstGrupo.Visible = True
            Me.btnNuevo.Enabled = True
            If Me.txts.Value = "1" Then
                activar(True)
            End If
            limpiar()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try


        'lstGrupo.HeaderRow.TableSection = TableRowSection.TableHeader

    End Sub


    Sub limpiar()
        Me.txts.Value = String.Empty
        Me.txtTitulo.Value = String.Empty
        Me.txtid.Value = String.Empty
        Me.txtDescripcion.Text = String.Empty
        Me.fileArchivo.Dispose()
        Me.txtFileDesc.Text = String.Empty
        Me.fileImagen.Dispose()
        Me.img.ImageUrl = ""
        Me.txtImagenDesc.Text = String.Empty
        Me.divLnk.InnerHtml = ""
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

﻿Imports System.IO
Imports System.Security.Cryptography
Imports System.Collections.Generic

Partial Class AdministrarActividadTipoFuncion
    Inherits System.Web.UI.Page

    Private C As ClsConectarDatos
    Private cod_user As Integer '= 684
    Private cod_ctf As Integer '= 1

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum
    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../../../sinacceso.html")

            Else

                cod_user = Session("id_per")
                cod_ctf = Request.QueryString("ctf")

                If Not IsPostBack Then
                   


                    Call mt_CargarTipoFuncion()
                    ' Call mt_ListarConceptoTramite()
                    Me.ddlActividadBsq.Focus()
                Else
                    'Call RefreshGrid()
                End If
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub


    Protected Sub ddlActividadBsq_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlActividadBsq.SelectedIndexChanged
        mt_ListarTipoFuncion()
    End Sub

    Protected Sub btnBuscarTipoFuncion_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarTipoFuncion.Click
        mt_ListarTipoFuncion()
    End Sub
    Protected Sub grwTipoFuncion_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwTipoFuncion.PreRender
        If grwTipoFuncion.Rows.Count > 0 Then
            grwTipoFuncion.UseAccessibleHeader = True
            grwTipoFuncion.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub grwTipoFuncion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwTipoFuncion.RowDataBound
        Try

            Dim index As Integer = 0


            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _sel As String = grwTipoFuncion.DataKeys(e.Row.RowIndex).Values(2).ToString() ' sel
                Dim sel As Integer = fnDevuelveNumEntero(Me.spTotal.InnerHtml)
                index = e.Row.RowIndex
                Dim checkAcceso As CheckBox
                checkAcceso = e.Row.FindControl("chkElegir")
                ' Response.Write(e.Row.FindControl("sel"))
                'checkAcceso.Checked = True

                If _sel > 0 Then
                    checkAcceso.Checked = True
                    sel = sel + 1
                Else
                    checkAcceso.Checked = False

                End If

                Me.spTotal.InnerHtml = sel.ToString
            End If
        Catch ex As Exception
            Response.Write("grwTipoFuncion_RowDataBound : " & ex.Message & "--" & ex.StackTrace)
        End Try
    End Sub
    Protected Sub chckchanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim contador As Integer = 0
        Dim chckheader As CheckBox = CType(grwTipoFuncion.HeaderRow.FindControl("chkall"), CheckBox)
        'contador = 0
        For Each row As GridViewRow In grwTipoFuncion.Rows
            Dim chckrw As CheckBox = CType(row.FindControl("chkElegir"), CheckBox)
            If chckheader.Checked = True Then
                chckrw.Checked = True
            Else
                chckrw.Checked = False
            End If
            If chckrw.Checked = True Then
                contador = contador + 1
                '  row.ControlStyle.BackColor = Drawing.Color.AntiqueWhite
                row.ControlStyle.Font.Bold = True
            Else
                '  row.ControlStyle.BackColor = Drawing.Color.White
                row.ControlStyle.Font.Bold = False
            End If
        Next
        'Me.lblContadorSeleccionado.Text = contador.ToString
    End Sub

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType, Optional ByVal modal As Boolean = False)

        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")

    End Sub
    Private Sub mt_ListarTipoFuncion()
        Try
            Me.spTotal.InnerHtml = "0"
            Dim dt As New Data.DataTable("Data")
            Dim tipo As String = ""

            Dim codigo_act As Integer = fnDevuelveNumEntero(Me.ddlActividadBsq.SelectedValue)

            C.AbrirConexion()
            dt = C.TraerDataTable("ACT_TipoFuncion_Listar", tipo, 0, codigo_act)
            Me.grwTipoFuncion.DataSource = dt
            Me.grwTipoFuncion.DataBind()
            dt.Dispose()

            C.CerrarConexion()

            Me.grwTipoFuncion.Focus()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub
    Private Sub mt_CargarTipoFuncion()
        Try
            Dim dt As New Data.DataTable("Data")
            C.AbrirConexion()

            dt = C.TraerDataTable("TRL_ActividadCronograma_Listar", "3", 0, "")

            Me.ddlActividadBsq.DataSource = dt
            Me.ddlActividadBsq.DataValueField = "codigo_Act"
            Me.ddlActividadBsq.DataTextField = "descripcion_Act"
            Me.ddlActividadBsq.DataBind()
            dt.Dispose()
            C.CerrarConexion()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGuardarTipoFuncion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarTipoFuncion.Click
        If mt_GuardarAcceso() Then
            mt_ListarTipoFuncion()
        End If
    End Sub

    Private Function mt_GuardarAcceso() As Boolean
        Try
            Dim rpta As Boolean = False
            Dim ip As String = Request.ServerVariables("REMOTE_ADDR").ToString()
            Dim host As String = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName
            Dim dt As New Data.DataTable
            Dim filas As Integer = grwTipoFuncion.Rows.Count
            Dim i As Integer = 0
            Dim Fila As GridViewRow
            Dim CLSacttfu As New clsActividadTfu
            Dim ID As Integer
            Dim TFU As Integer
            Dim ACT As Integer = fnDevuelveNumEntero(Me.ddlActividadBsq.SelectedValue)
            With CLSacttfu
                For i = 0 To filas - 1
                    Fila = Me.grwTipoFuncion.Rows(i)
                    Dim clsDetTfu As New clsActividadTfu
                    Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked

                    ID = fnDevuelveNumEntero(Me.grwTipoFuncion.DataKeys(i).Values(3).ToString)
                    TFU = fnDevuelveNumEntero(Me.grwTipoFuncion.DataKeys(i).Values(0).ToString)

                    With clsDetTfu

                        If ID > 0 Then
                            .tipooperacion = "A"

                        Else
                            .tipooperacion = "I"

                        End If
                        ._codigo_acttfu = ID
                        ._codigo_tfu = TFU
                        ._codigo_act = ACT
                        ._codigo_per = fnDevuelveNumEntero(Session("id_per"))

                        If (valor = True) Then
                            ._acceso = True
                        Else
                            ._acceso = False
                        End If



                    End With
                    CLSacttfu.tfuasignada.Add(clsDetTfu)

                Next


            End With



            rpta = CLSacttfu.Asignar()
            'rpta = True

            If rpta Then


                ' ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('Tr&aacute;mite registrado con &eacute;xito', 'success');</script>")
                Call mt_ShowMessage("Función asignado con &eacute;xito", MessageType.Success)
            Else
                rpta = False
                Call mt_ShowMessage("Error al asignar Función", MessageType.Error)
                'ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('Error al registrar tr&aacute;mite', 'danger');</script>")
            End If


            CLSacttfu = Nothing





            Return rpta
        Catch ex As Exception

            'Response.Write(ex.Message & "----" & ex.StackTrace)
            'ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('" & ex.Message.Replace("'", "") & "', 'danger');</script>")
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
            Return False
        End Try
    End Function

#Region "Funciones"

    Public Function fnDevuelveNumEntero(ByVal input As String) As Integer
        Dim r As Integer = 0

        If input = "" Then
            r = 0
        Else
            r = CInt(input)
        End If

        Return r
    End Function
    Public Function fnDevuelveNumDecimal(ByVal input As String) As Decimal
        Dim r As Decimal = 0.0

        If input = "" Then
            r = 0.0
        Else
            r = CDec(input)
        End If

        Return r
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

#End Region






End Class
Public Class clsActividadTfu
    Private C As ClsConectarDatos
    Public tipooperacion As String


    Private codigo_acttfu As Integer
    Private codigo_tfu As Integer
    Private codigo_act As Integer
    Private acceso As Boolean

    Private codigo_per As Integer


    Public tfuasignada As New List(Of clsActividadTfu)


    Public Property _acceso() As Boolean
        Get
            Return acceso
        End Get
        Set(ByVal value As Boolean)
            acceso = value
        End Set
    End Property

    Public Property _codigo_per() As Integer
        Get
            Return codigo_per
        End Get
        Set(ByVal value As Integer)
            codigo_per = value
        End Set
    End Property

    Public Property _codigo_act() As Integer
        Get
            Return codigo_act
        End Get
        Set(ByVal value As Integer)
            codigo_act = value
        End Set
    End Property


    Public Property _codigo_tfu() As Integer
        Get
            Return codigo_tfu
        End Get
        Set(ByVal value As Integer)
            codigo_tfu = value
        End Set
    End Property

    Public Property _codigo_acttfu() As Integer
        Get
            Return codigo_acttfu
        End Get
        Set(ByVal value As Integer)
            codigo_acttfu = value
        End Set
    End Property


    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        End If

    End Sub

    Public Function Asignar() As Boolean
        Try
            Dim rpta As Boolean = False
            Dim id As Integer = 0
            C.IniciarTransaccion()

            For Each detalle As clsActividadTfu In tfuasignada
                With detalle
                    rpta = C.Ejecutar("ACT_TipoFuncion_Asignar", .tipooperacion, ._codigo_acttfu, ._codigo_act, ._codigo_tfu, ._codigo_per, ._acceso)

                    If rpta = False Then
                        C.AbortarTransaccion()
                        Return False
                    End If
                End With
            Next




            C.TerminarTransaccion()
            Return rpta
        Catch ex As Exception
            C.TerminarTransaccion()
            Return False
        End Try
    End Function
End Class
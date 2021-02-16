Imports System.IO
Imports System.Security.Cryptography
Imports System.Drawing
Imports System.Data

Partial Class BienestarEstudiantil_maestros_Tableroreglas
    Inherits System.Web.UI.Page

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        fnMostrar(False)
        CargaDatos()
        Me.cboPerfil.focus()

    End Sub

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub


#Region "Eventos"

    Protected Sub gvDatos_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDatos.PreRender
        If gvDatos.Rows.Count > 0 Then
            gvDatos.UseAccessibleHeader = True
            gvDatos.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub




    Protected Sub gvDatos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDatos.RowCommand
        Try

            '' Response.Write(e.CommandArgument)
            'Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            '' Response.Write(e.CommandName)
            'If (e.CommandName = "Evaluar") Then
            '    Dim codigo_dta As Integer = 0
            '    hddtareq.Value = Encriptar(gvDatos.DataKeys(index).Values("codigo_dta").ToString)
            '    codigo_dta = gvDatos.DataKeys(index).Values("codigo_dta")
            '    Dim fechaPago As String = ""
            '    If Not IsDBNull(gvDatos.DataKeys(index).Values("fecha_cin")) Then
            '        fechaPago = gvDatos.DataKeys(index).Values("fecha_cin")
            '    End If
            '    Me.lblSemestreMatriculado.Visible = False
            '    Me.lblTotalSemestre.Visible = False
            '    Me.lblInfoAdicional.Visible = False


            '    Dim codigoAlu As Integer = 0
            '    codigoAlu = gvDatos.DataKeys(index).Values("codigo_Alu")

            '    Dim codigoUniver As String = ""
            '    codigoUniver = gvDatos.DataKeys(index).Values("codigoUniver_Alu")

            '    Dim codigo_trl As Integer = 0
            '    codigo_trl = gvDatos.DataKeys(index).Values("codigo_trl")

            '    Dim tramite As String = ""
            '    tramite = gvDatos.DataKeys(index).Values("descripcion_ctr")

            '    Dim fechaTramite As String = ""
            '    fechaTramite = gvDatos.DataKeys(index).Values("fechaReg_trl").ToString

            '    fnMostrarEvaluar(True)
            '    ' fnInformacionTramite(CInt(gvDatos.DataKeys(index).Values("codigo_dta").ToString))
            '    fnInformacionTramite(CInt(gvDatos.DataKeys(index).Values("codigo_dta").ToString))
            '    accionURL(codigoUniver, codigoAlu, "", codigo_trl, codigo_dta, tramite, fechaPago, fechaTramite)
            '    MostrarInformacionEvaluadores(codigo_dta)
            'End If
        Catch ex As Exception
            Response.Write("Error gvDatos_RowCommand: " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub


    Protected Sub btnCrear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCrear.Click
        fnMostrar(True)
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        CargaDatos()
    End Sub

#End Region

#Region "Procedimiento"



    Private Sub CargaDatos()


        Dim miDataTable As New DataTable
        miDataTable.Columns.Add("ID")
        miDataTable.Columns.Add("PERFIL")
        miDataTable.Columns.Add("REGLA")
        miDataTable.Columns.Add("DESCRIPCION")
        miDataTable.Columns.Add("OPERACION")
        miDataTable.Columns.Add("VALOR")

        'Renglon es la variable que adicionara renglones a mi datatable
        Dim Renglon As DataRow = miDataTable.NewRow()
        Renglon("ID") = "1"
        Renglon("PERFIL") = "ESTUDIANTE"
        Renglon("REGLA") = "RFC001"
        Renglon("DESCRIPCION") = "CUMPLE CRONOGRAMA INSCRIPCION"
        Renglon("OPERACION") = "="
        Renglon("VALOR") = "1"
        miDataTable.Rows.Add(Renglon)

        Renglon = miDataTable.NewRow()
        Renglon("ID") = "2"
        Renglon("PERFIL") = "ESTUDIANTE"
        Renglon("REGLA") = "RFC002"
        Renglon("DESCRIPCION") = "CUMPLE CRONOGRAMA AGREGADOS Y RETIROS"
        Renglon("OPERACION") = "="
        Renglon("VALOR") = "1"
        miDataTable.Rows.Add(Renglon)

        Renglon = miDataTable.NewRow()
        Renglon("ID") = "3"
        Renglon("PERFIL") = "ESTUDIANTE"
        Renglon("REGLA") = "RFC003"
        Renglon("DESCRIPCION") = "CUMPLE CRONOGRAMA CAMBIO DE GRUPO"
        Renglon("OPERACION") = "="
        Renglon("VALOR") = "1"
        miDataTable.Rows.Add(Renglon)









        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            'dt = obj.TraerDataTable("TRL_BuscaTramiteLinea", Me.txtAlumno.Text.Replace(" ", "%"), Me.cboEstado.SelectedValue, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"))
            'dt = obj.TraerDataTable("TRL_TramitesGyTMigracion", "1", "", Me.txtnroTramite.Text, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"), 0)
            obj.CerrarConexion()

            Me.gvDatos.DataSource = miDataTable
            Me.gvDatos.DataBind()


            'Response.Write(dt.rows.count)
            dt = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub




    Private Sub fnMostrar(ByVal sw As Boolean)

        If sw Then
            Me.pnlLista.Visible = False
            Me.pnlRegistro.Visible = True
        Else
            Me.pnlLista.Visible = True
            Me.pnlRegistro.Visible = False

        End If

    End Sub

#End Region

#Region "Funciones"
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

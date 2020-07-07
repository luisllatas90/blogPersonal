Imports System.Collections.Generic
Imports System.IO

Partial Class administrativo_activofijo_darbajaActivoFijo
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private ruta As String = "http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx" ' Desarrollo
    'Private ruta As String = "http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx" ' Producción

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../../sinacceso.html")
            End If
            If IsPostBack = False Then
                mt_CargarCombo()
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnDarBaja_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Editar" & "');</script>")
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim Data As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim codigo_af, idtabla As Integer
        Dim respuesta As String = ""
        Try
            If Not (IsDBNull(Session("perlogin"))) And Session("perlogin").ToString <> "" Then
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                codigo_af = CInt(Me.hdIdActivoFijo.Value)
                obj.AbrirConexion()
                obj.Ejecutar("AF_darbajaActivoFijo", codigo_af, Request.QueryString("id"), Me.cboTipoBien.SelectedValue, Me.cboMotivoBaja.SelectedValue, CDate(Me.txtFecha.Text))
                obj.CerrarConexion()
                idtabla = 15 ' Desarrollo
                'idtabla = 12 ' Produccion
                If Me.fuArchivo.HasFile Then
                    Dim Archivos As HttpFileCollection = Request.Files
                    For i As Integer = 0 To Archivos.Count - 1
                        'Data.Add("Step", idtabla & "-" & codigo_af & "-" & Archivos(i).FileName)
                        fc_SubirArchivo(idtabla, codigo_af, Archivos(i))
                    Next
                End If
                mt_CargarDatos()
            Else
                Throw New Exception("Inicie Session")
            End If
            
            'Data.Add("Status", "OK")
            'Data.Add("Message", respuesta)
            'list.Add(Data)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)

            mt_ShowMessage("El Activo se dio de baja correctamente", MessageType.Success)

        Catch ex As Exception
            mt_ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", " "), MessageType.Error)
            'Data.Add("Status", "Fail")
            'Data.Add("Message", ex.Message & " - " & Session("perlogin").ToString)
            'list.Add(Data)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        mt_CargarDatos()
    End Sub

    Protected Sub gvActivoFijo_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvActivoFijo.RowCommand
        Try
            'Dim obj As New ClsConectarDatos
            'Dim codigo_af As Integer
            Dim index As Integer
            index = CInt(e.CommandArgument)
            'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            If e.CommandName = "DarBaja" Then
                'codigo_af = Me.gvActivoFijo.DataKeys(index).Values("codigo_af")
                'obj.AbrirConexion()
                'obj.Ejecutar("AF_darbajaActivoFijo", codigo_af, Request.QueryString("id"))
                'obj.CerrarConexion()
                'mt_CargarDatos()
                Me.hdIdActivoFijo.Value = Me.gvActivoFijo.DataKeys(index).Values("codigo_af")
                Me.hdMotivoBaja.Value = Me.gvActivoFijo.DataKeys(index).Values("motivo_af")
            End If
        Catch ex As Exception
            mt_ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub mt_CargarCombo()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("AF_listadoUbicacionAF2", 0)
            obj.CerrarConexion()
            ClsFunciones.LlenarListas(Me.cboUbicacion, dt, "codigo_uba", "descripcion_uba")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("AF_listarActivoFijo3", Me.txtCodigo.Text, Me.cboUbicacion.SelectedValue)
            obj.CerrarConexion()
            Me.gvActivoFijo.DataSource = dt
            Me.gvActivoFijo.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

#End Region

#Region "Funciones"

    Function fc_SubirArchivo(ByVal idTabla As String, ByVal codigo As String, ByVal file As HttpPostedFile) As String
        Dim list As New Dictionary(Of String, String)
        Try
            Dim _TablaId As String = idTabla
            Dim _Archivo As HttpPostedFile = file
            Dim _TransaccionId As String = codigo
            Dim _Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim _Usuario As String = Session("perlogin").ToString
            Dim Input(_Archivo.ContentLength) As Byte

            Dim br As New BinaryReader(_Archivo.InputStream)
            Dim binData As Byte() = br.ReadBytes(_Archivo.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)
            Dim _Nombre As String = Path.GetFileName(_Archivo.FileName.Replace("&", "_").Replace("'", "_").Replace("*", "_"))

            Dim wsCloud As New ClsArchivosCompartidos

            list.Add("Fecha", _Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(_Archivo.FileName))
            list.Add("Nombre", _Nombre)
            list.Add("TransaccionId", _TransaccionId)
            list.Add("TablaId", _TablaId)
            list.Add("NroOperacion", "")
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", _Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", _Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Session("perlogin").ToString)

            Return result

            'Return "prueba"

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region


End Class

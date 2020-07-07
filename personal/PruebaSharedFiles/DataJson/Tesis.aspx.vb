Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class DataJson_GestionInvestigacion_Tesis
    Inherits System.Web.UI.Page

    Dim ruta As String = ConfigurationManager.AppSettings("SharedFiles")
    Dim rutareporte As String = ConfigurationManager.AppSettings("RutaReporte")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim obj As New ClsGestionInvestigacion
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            If Session("id_per") = "" Then
                Data.Add("idper", Session("id_per"))
                Data.Add("rpta", "SESIÓN EXPIRADA")
                list.Add(Data)
                JSONresult = serializer.Serialize(list)
                Response.Write(JSONresult)
            Else
                Dim k As String = "0" 'Request("k")
                Dim f As String = ""

                'Select Case obj.DecrytedString64(Request("action"))
                Select Case Request("action")

                    Case "SurbirArchivo"
                        Dim ArchivoASubir As HttpPostedFile = HttpContext.Current.Request.Files("ArchivoASubir")
                        Dim codigo As String = obj.DecrytedString64(Request("codigo")).ToString
                        Dim tipo As String = Request("tipo")
                        SubirArchivo(codigo, ArchivoASubir, tipo)

                End Select
            End If

        Catch ex As Exception
            Data.Add("idper", Session("id_per"))
            Data.Add("rpta", ex.Message & "0 - LOAD")
            Data.Add("LINEA", ex.StackTrace & "0 - LOAD")
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub



    Sub SubirArchivo(ByVal codigo As String, ByVal ArchivoSubir As HttpPostedFile, ByVal tipo As String)
        Try
            Dim idtabla As Integer = 23
            Dim post As HttpPostedFile = ArchivoSubir
            Dim cod_operacion As String = 0
            Dim Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim Usuario As String = Session("perlogin").ToString
            Dim Input(post.ContentLength) As Byte
            Dim etapa As String = "P"

            If tipo = "PROYECTO" Then
                cod_operacion = 1
            End If
            If tipo = "ACTA" Then
                cod_operacion = 2
            End If

            If tipo = "SIMILITUDPROYECTO" Then
                cod_operacion = 3
            End If

            If tipo = "PREINFORME" Then
                cod_operacion = 4
                etapa = "E"
            End If

            If tipo = "INFORME" Then
                cod_operacion = 5
                etapa = "I"
            End If

            If tipo = "ACTAINFORME" Then
                cod_operacion = 6
                etapa = "I"
            End If
            If tipo = "SIMILITUDINFORME" Then
                cod_operacion = 7
                etapa = "I"
            End If

            Dim b As New BinaryReader(post.InputStream)
            Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)

            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)
            '  Dim list As New List(Of Dictionary(Of String, String))()
            list.Add("Fecha", Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
            'list.Add("Nombre", System.IO.Path.GetFileName(post.FileName).Replace("&", "_").Replace("'", "_").Replace("*", "_").Replace("á", "_").Replace("é", "_").Replace("í", "_").Replace("ó", "_").Replace("ú", "_").Replace("Á", "_").Replace("É", "_").Replace("Í", "_").Replace("Ó", "_").Replace("Ú", "_").Replace("Ñ", "_").Replace("ñ", "_").Replace(",", "_"))
            list.Add("Nombre", Regex.Replace(System.IO.Path.GetFileName(post.FileName), "[^0-9A-Za-z._ ]", "").Replace(",", ""))
            list.Add("TransaccionId", codigo)
            list.Add("TablaId", idtabla)
            list.Add("NroOperacion", cod_operacion)
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Usuario)

            ActualizarArchivoTesis(idtabla, codigo, cod_operacion, etapa, Session("id_per"))

            Response.Write(result)
        Catch ex As Exception
            Dim Data1 As New Dictionary(Of String, Object)()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim list As New List(Of Dictionary(Of String, Object))()
            Data1.Add("rpta", "1 - LIST")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try

    End Sub


    Private Sub ActualizarArchivoTesis(ByVal idtabla As Integer, ByVal idtransaccion As String, ByVal idoperacion As String, ByVal abreviatura As String, ByVal codigo_per As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim Data1 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarArchivosTesis(idtabla, idtransaccion, idoperacion, abreviatura, codigo_per)
            Dim data As New Dictionary(Of String, Object)()
            data.Add("cod", obj.EncrytedString64(idtransaccion))
            'data.Add("ruta", ruta)
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data1.Add("rpta", "1 - ACTUALIZAR ARCHIVO COMP." + idtransaccion)
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub


End Class

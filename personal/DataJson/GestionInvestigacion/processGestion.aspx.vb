Imports System.Web.Security
Imports System.Data
Imports System.Data.DataTable
Imports System.Collections.Generic
Imports System.Data.DataRow
Imports System.Data.DataColumn
Imports System.IO
Imports System.Web.HttpRequest
Imports System.Diagnostics
Imports System.Xml
Imports System.Xml.Serialization


Partial Class DataJson_GestionInvestigacion_processGestion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsGestionInvestigacion
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Response.Write("---------------------------------")
        Response.Write(Request("action"))
        Try
            Select Case Request("action")
                'JR INICIO
                Case "gRegistrarInvestigador"
                    'Response.Write("EEEEEEEEEEEEEEEEEEEE")
                    RegistrarInvestigador()
                    'Response.Write("SSSSSSSSSSSSSSSSSSSS")
                    'JR FIN
            End Select

        Catch ex As Exception
            Data.Add("msje", ex.Message)
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub

    Sub RegistrarInvestigador()
        Dim JSONresult As String = ""
        Try
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim obj As New ClsConectarDatos
            Dim obj1 As New ClsGestionInvestigacion

            Dim Codigo_user As Integer = obj1.DecrytedString64(Request("hdUser"))
            Dim Url_dina As Integer = Request("txtURLDina")

            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            tb = obj.TraerDataTable("INV_registrarInvestigador", Codigo_user, Url_dina, Codigo_user)
            obj.CerrarConexion()

            For i As Integer = 0 To tb.Rows.Count - 1
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("Status", tb.Rows(i).Item("Status"))
                Data.Add("Message", tb.Rows(i).Item("Message"))
                Data.Add("Code", tb.Rows(i).Item("Code"))
                list.Add(Data)
            Next

            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            'Response.Write("messaaaaage" & ex.Message.ToString)
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dict As New Dictionary(Of String, Object)()
            dict.Add("alert", "error")
            dict.Add("msje", "Error al Registrar el Investigador")
            list.Add(dict)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try

    End Sub






End Class

Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Web
Imports System.Net
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.VisualBasic
Imports System
Imports System.Security.AccessControl
Partial Class DataJson_PredictorDiserccion_Variables
    Inherits System.Web.UI.Page
   

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FPost As String = Request.Form("Funcion")
        ' Response.Write("sdfsd" + FPost)
          Select FPost
            Case "Grabar"
                GrabarVariable()
            Case "Listar"
                ListarVariables()
        End Select
    End Sub
    Sub GrabarVariable()
        Dim Codigo_Var As String = Request.Form("txtCodigoVar")
        Dim Nombre_Var As String = Request.Form("txtnombre")
        Dim Descripcion_Var As String = Request.Form("txtDescripcion")
        Dim NombreDesc_var As String = Request.Form("txtnombreDesc")
        Dim Origen_Var As String = Request.Form("cboOrigen")
        Dim Signo_Var As String = Request.Form("cboSigno")
        Dim Estado_Var As Boolean
        If Request.Form("chkestado") Is Nothing Then
            Estado_Var = False
            '  Response.Write(1)
        Else
            Estado_Var = True
            ' Response.Write(2)
        End If


        Dim Fecha_Var As Date = Request.Form("txtFecha")
        ' Response.Write(Fecha_Var)
        Dim Usuario_Var As String = Session("perlogin")
        Dim op As Integer = Request.Form("op")
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))

        tb = obj.TraerDataTable("dbo.PRED_USPVARIABLE", op, Codigo_Var, NombreDesc_var, Nombre_Var, Descripcion_Var, Origen_Var, Estado_Var, Fecha_Var, Usuario_Var, Signo_Var)
        obj.CerrarConexion()


        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
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
    End Sub
    Sub ListarVariables()
         


         
        Dim op As Integer = 1
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))

        tb = obj.TraerDataTable("dbo.PRED_LISTVARIABLE", op)
        obj.CerrarConexion()


        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("CodigoVar", tb.Rows(i).Item("CodigoVar"))
            Data.Add("NombreDescVar", tb.Rows(i).Item("NombreDescVar"))
            Data.Add("NombreVar", tb.Rows(i).Item("NombreVar"))
            Data.Add("DescripcionVar", tb.Rows(i).Item("DescripcionVar"))
            Data.Add("OrigenVar", tb.Rows(i).Item("OrigenVar"))
            Data.Add("SignoVar", tb.Rows(i).Item("SignoVar"))
            Data.Add("EstadoVar", tb.Rows(i).Item("EstadoVar"))
            Data.Add("EstadoText", tb.Rows(i).Item("EstadoText"))
            Data.Add("FechaVar", tb.Rows(i).Item("FechaVar"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'FechaVar
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
End Class

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
Partial Class DataJson_PredictorDesercion_FormulasDesercion
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FPost As String = Request.Form("Funcion")
        ' Response.Write("sdfsd" + FPost)
        Select Case FPost
            Case "CicloAcademico"
                CicloAcdemico()
            Case "Grabar"
                GrabarFormula()
            Case "Listar"
                ListarFormula()
            Case "Editar"
                ListarFormulaVariable()
            Case "Desactivar"
                DesactivarCoeficiente()
            Case "AnulaFormula"
                AnularFormular()
        End Select


    End Sub
    Sub GrabarFormula()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        Dim Codigo_Fml As Integer = Request.Form("txtCodigoFml")
        Dim Nombre_Fml As String = Request.Form("txtNombreFormula")
        Dim Descripcion_Fml As String = "" ' Request.Form("DescripcionFml")
        Dim Definicion_Fml As String = "" ' Request.Form("txtDefinicionFml")
        Dim Fecha_Fml As DateTime = Request.Form("txtFecha")
        Dim VarIndependiente_Fml As String = Request.Form("txtIntercep")
        Dim Codigo_Cac As Integer = Request.Form("cboCicloAcademicoR")

        '  Response.Write(Fecha_Fml)
        Dim variables() As Object = serializer.DeserializeObject(Request.Form("variables"))
        Dim Usuario As String = Request.Form("Session") ' Session("perlogin")
        Dim Estado_Var As Boolean
        If Request.Form("chkestado") Is Nothing Then
            Estado_Var = False
            '  Response.Write(1)
        Else
            Estado_Var = True
            ' Response.Write(2)
        End If



        ' Response.Write(Fecha_Var)

        Dim op As Integer = Request.Form("op")
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))

        tb = obj.TraerDataTable("dbo.PRED_USPFORMULADESERCION", op, Codigo_Fml, Nombre_Fml, Descripcion_Fml, Definicion_Fml, Fecha_Fml, VarIndependiente_Fml, Codigo_Cac, Usuario, Estado_Var)
        obj.CerrarConexion()


        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", tb.Rows(i).Item("Status"))
            Data.Add("Message", tb.Rows(i).Item("Message"))
            Data.Add("Code", tb.Rows(i).Item("Code"))

            If tb.Rows(i).Item("Status") = "OK" Then
                GrabarVariableFormula(variables, tb.Rows(i).Item("Code"), op, Usuario)
            End If

            list.Add(Data)
        Next


        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Function GrabarVariableFormula(ByVal varibles() As Object, ByVal CodigoFml As Integer, ByVal op As Integer, ByVal Usuario As String) As Data.DataTable
        ' Dim Usuario As String = usuario 'Session("perlogin")

        Dim detalle() As Object = varibles
        For i As Integer = 0 To detalle.Length - 1
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim cif As New ClsCRM

            If detalle(i).Item("CodigoVarFml") = 0 Then
                op = 1
            End If

            tb = obj.TraerDataTable("dbo.PRED_UPSFORMULAVARIABLE", op, detalle(i).Item("CodigoVarFml"), CodigoFml, detalle(i).Item("variableId"), detalle(i).Item("coeficiente"), Usuario, 1, "")
            obj.CerrarConexion()
        Next
    End Function
    Sub ListarFormula()
        '   Dim op As Integer = 1
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        Dim Codigo_cac = Request.Form("CodigoCac")
        Dim op As Integer = Request.Form("op")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))
        tb = obj.TraerDataTable("dbo.PRED_LISTFORMULADESERCION", Codigo_cac, op)
        obj.CerrarConexion()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("CodigoFml", tb.Rows(i).Item("CodigoFml"))
            Data.Add("NombreFml", tb.Rows(i).Item("NombreFml"))
            Data.Add("VarIndependienteFml", tb.Rows(i).Item("VarIndependienteFml"))
            Data.Add("CicloAcademico", tb.Rows(i).Item("CicloAcademico"))
            Data.Add("DefinicionFml", tb.Rows(i).Item("DefinicionFml"))
            Data.Add("Fecha", tb.Rows(i).Item("Fecha"))
            Data.Add("Estado", tb.Rows(i).Item("Estado"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'FechaVar
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub CicloAcdemico()
        Dim op As Integer = 1
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))
        tb = obj.TraerDataTable("dbo.ConsultarCicloAcademico", "TO2", "")
        obj.CerrarConexion()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Value", tb.Rows(i).Item("codigo_Cac"))
            Data.Add("Label", tb.Rows(i).Item("descripcion_Cac"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'FechaVar
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub ListarFormulaVariable()
        Dim op As Integer = 1
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        Dim Codigofml As Integer = Request.Form("CodigoFml")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        ' Response.Write(Request.Form("CodigoFml"))
        tb = obj.TraerDataTable("dbo.PRED_LISTFORMULAVARIABLE", Codigofml)
        ' Response.Write(tb.Rows.Count)
        obj.CerrarConexion()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("CodigoFml", tb.Rows(i).Item("CodigoFml"))
            Data.Add("NombreFml", tb.Rows(i).Item("NombreFml"))
            Data.Add("VarIndependienteFml", tb.Rows(i).Item("VarIndependienteFml"))
            Data.Add("FechaFml", Format(tb.Rows(i).Item("FechaFml"), "dd/MM/yyyy"))
            Data.Add("DefinicionFml", tb.Rows(i).Item("DefinicionFml"))
            Data.Add("DescripcionFml", tb.Rows(i).Item("DescripcionFml"))
            Data.Add("CodigoVar", tb.Rows(i).Item("CodigoVar"))
            Data.Add("NombreVar", tb.Rows(i).Item("NombreVar"))
            Data.Add("Coeficiente", tb.Rows(i).Item("Coeficiente"))
            Data.Add("NombreDescVar", tb.Rows(i).Item("NombreDescVar"))
            Data.Add("CodigoVarFml", tb.Rows(i).Item("CodigoVarFml"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'FechaVar
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub DesactivarCoeficiente()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        Dim Codigo_VarFml As Integer = Request.Form("CodigoVarFml")

        Dim Usuario As String = Request.Form("Session") ' Session("perlogin")

        Dim op As Integer = Request.Form("op")
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))

        tb = obj.TraerDataTable("dbo.PRED_USPELIMINARCOEFICIENTE", Codigo_VarFml, Usuario)
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


        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub


    Sub AnularFormular()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        Dim Codigo_Fml As Integer = Request.Form("codigo_fml")

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))

        tb = obj.TraerDataTable("dbo.PRED_USPINACTIVAFORMULA", Codigo_Fml)
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


        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
End Class
Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Xml

Partial Class scriptJSON_inscripcioneventoPre
    Inherits System.Web.UI.Page
    'Dim usuario_session_ As String() = Session("perlogin").ToString.Split(New Char() {"\"c})
    'Dim usuario_session As String = usuario_session_(1)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try
            
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim tipo As String = ""
            'Dim k As String = Request("k")
            Dim obj As New ClsCRM
            Dim arr As New List(Of String)
            Dim ope As String = ""
            Dim lst As Boolean = False
            Dim action As String = ""
            Dim codpk As Integer = 0
            Dim codigo_per As String = Session("id_per").ToString



            action = obj.DecrytedString64(Request.Form("process").ToString())

            'Data.Add("process", action)
            'list.Add(Data)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)



            Select Case action
                Case "Listar"

                    'Data.Add("cco", CInt(obj.DecrytedString64(Request.Form("cco").ToString())))
                    'Data.Add("estado", Request.Form("cboEstadoParticipanteInsc").ToString())
                    'list.Add(Data)
                    'JSONresult = serializer.Serialize(list)
                    'Response.Write(JSONresult)

                    'Exit Sub
                    CargarInscritosConCargo(CInt(obj.DecrytedString64(Request.Form("cco").ToString())), Request.Form("cboEstadoParticipanteInsc").ToString())

              
            End Select

            'Data.Add("msje", Request)
            'JSONresult = serializer.Serialize(sr)
            'Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("msje", False)
            Data.Add("error", ex.Message.ToString)
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub CargarInscritosConCargo(ByVal codigo_cco As Integer, ByVal estado As String)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim objCRM As New ClsCRM
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            'Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.EVE_ConsultarInscritosxEstado", codigo_cco, estado)
            obj.CerrarConexion()

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    If i = 0 Then data.Add("sw", True)
                    data.Add("cCodpso", objCRM.EncrytedString64(tb.Rows(i).Item("codigo_Pso")))
                    data.Add("nTipoDoc", tb.Rows(i).Item("TipoDoc").ToString)
                    data.Add("nParticipante", tb.Rows(i).Item("Participante").ToString())
                    data.Add("cCodUni", tb.Rows(i).Item("CodUniversitario"))
                    data.Add("nCicloIng", tb.Rows(i).Item("cicloIng_Alu").ToString())
                    data.Add("mCargoTotal", tb.Rows(i).Item(6))
                    data.Add("mAbonoTotal", tb.Rows(i).Item(7))
                    data.Add("mSaldoTotal", tb.Rows(i).Item(8))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception

        End Try
    End Sub


End Class

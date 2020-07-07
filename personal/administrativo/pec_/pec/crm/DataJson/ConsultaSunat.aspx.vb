Imports System.Data
Partial Class administrativo_Tesoreria_Rendiciones_AppRendiciones_DataJson_ConsultaSunat
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim NroRuc As String = Request.QueryString("NroRuc")
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.Datatable
        Dim cn As New clsaccesodatos
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ' dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
        tb = obj.TraerDataTable("dbo.USP_EXISTENCIA_PSO_BY_LIST", NroRuc)
        obj.CerrarConexion()

        If tb.rows.Count > 0 Then
            Dim info As DSNStructInfoRuc = New DSNStructInfoRuc()
            For Each dr As DataRow In tb.rows
                info.Codigo = dr("Codigo")
                info.Departamento = dr("Departamento")
                info.Provincia = dr("Provincia")
                info.NombreRazonSocial = dr("NombreRazonSocial")
                info.Direccion = dr("Direccion")
                info.NumRuc = dr("NumRuc")
                info.NombreComercial = ""
            Next
            JSONresult = serializer.Serialize(info)
            Response.Write(JSONresult)
        Else
            Dim con As New DSNComponenteSunat
            con.SetParamsSearch(True, True, True)
            Dim info As DSNStructInfoRuc = con.GetInformacionPersona(NroRuc)
           
            JSONresult = serializer.Serialize(info)
            Response.Write(JSONresult)
        End If

    End Sub
End Class

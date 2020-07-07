Imports System.Data
Partial Class academico_expediente_adminhojavida
    Inherits System.Web.UI.Page
    'Set rsDoc= Obj.Consultar("ConsultarDocente","FO","CL",codigo_cac,0)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(Me.ddlDocente, Obj.TraerDataTable("ConsultarDocente", "CL", 61, 0), "codigo_per", "docente")

        Obj = Nothing        
    End Sub
End Class

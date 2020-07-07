
Partial Class personal_academico_estudiante_historial
    Inherits System.Web.UI.Page
    Dim crd As Integer = 0
    Dim notacrd As Double = 0
    Dim crdAR As Int16 = 0
    Dim crdAC As Int16 = 0
    Dim AAR As Int16 = 0
    Dim AAC As Int16 = 0
    Dim codigo_alu As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objEnc As New EncriptaCodigos.clsEncripta

        codigo_alu = ((Mid(objEnc.Decodifica(Request.QueryString("ctm")), 4)))
        'Response.Write(codigo_alu)
     
    End Sub

    Protected Sub cmdParticipar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdParticipar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("EVE_RegistrarParticipacionVigilia", codigo_alu, 1)
        obj.CerrarConexion()
        Me.cmdnoParicipo.Enabled = False
        cmdParticipar.Enabled = False
    End Sub

    Protected Sub cmdnoParicipo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdnoParicipo.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("EVE_RegistrarParticipacionVigilia", codigo_alu, 0)
        obj.CerrarConexion()
        Me.cmdnoParicipo.Enabled = False
        cmdParticipar.Enabled = False
    End Sub
End Class

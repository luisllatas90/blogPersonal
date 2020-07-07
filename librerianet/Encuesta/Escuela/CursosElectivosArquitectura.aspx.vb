Imports Microsoft.VisualBasic

Partial Class Encuesta_Escuela_CursosElectivosArquitectura
    Inherits System.Web.UI.Page

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim nro(4) As String
        Dim cant As Int16
        Dim i As Integer
        Dim NroControles As Int16
        Dim objEnc As New EncriptaCodigos.clsEncripta
        Dim rpta(1) As Integer
        'Dim codigo_alu As Integer

        NroControles = Me.Form.Controls.Count - 1
        cant = 0
        For i = 0 To 3
            nro(i) = ""
        Next
        i = 1
        Do
            Dim CtrlGeneral As New Control
            CtrlGeneral = Me.Form.Controls.Item(i)
            If CtrlGeneral.GetType.ToString = "System.Web.UI.WebControls.CheckBox" Then
                Dim CtrlChk As New CheckBox
                CtrlChk = CType(CtrlGeneral, CheckBox)
                If CtrlChk.Checked = True Then
                    'CtrlChk.ClientID.ToString 
                    nro(cant) = CtrlChk.Text
                    cant = cant + 1
                End If
            End If
            i = i + 1
        Loop While (i <= NroControles And cant < 4)
        Dim objCnx As New ClsConectarDatos
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objCnx.AbrirConexion()
        'codigo_alu = objCnx.TraerDataTable("ConsultarAlumno", "AC", objEnc.Decodifica(Request.QueryString("x"))).Rows(0).Item("codigo_alu")
        objCnx.Ejecutar("ENC_AgregarCursosVeranoArq", nro(0), nro(1), nro(2), nro(3), Mid(objEnc.Decodifica(Request.QueryString("x")), 4), 0).copyto(rpta, 0)
        objCnx.CerrarConexion()
        If rpta(0) = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "Exito", "alert('Usted ya ha contestado la encuesta anteriormente');", True)
        ElseIf rpta(0) = 1 Then
            ClientScript.RegisterStartupScript(Me.GetType, "Exito", "alert('Gracias por contestar la encuesta');", True)
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "Exito", "alert('Usted no pertenece a la Escuela de Arquitectura');", True)
        End If

        ClientScript.RegisterStartupScript(Me.GetType, "Cerrar", "window.close();", True)

    End Sub

    Protected Sub cmdCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCerrar.Click
        ClientScript.RegisterStartupScript(Me.GetType, "Cerrar", "window.close();", True)
    End Sub
End Class

Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class frmOperacionListaAplicaciones
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'VARIABLES
    Dim cod_user As Integer = 0    

    Dim list As New List(Of Dictionary(Of String, Object))()
    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    Dim dict As New Dictionary(Of String, Object)()
    Dim json_result As String = String.Empty

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing OrElse Session("perlogin") Is Nothing) Then                
                Call mt_ShowMessage("Sesión expirada.", "error", "error")
            End If

            cod_user = Session("id_per")

            If String.IsNullOrEmpty(Request.QueryString("operacion")) Then                
                Call mt_ShowMessage("No se ha definido operación a realizar.", "error", "error")
            Else
                Dim operacion As String = Request.QueryString("operacion").ToString.Trim

                Select Case operacion
                    Case "REGISTRAR_ASISTENCIA"
                        Call mt_RegistrarAsistencia(CInt(Request.QueryString("codigo_cpe")), Request.QueryString("nro_documento"), Request.QueryString("tipo"))

                End Select
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub mt_ShowMessage(ByVal mensaje As String, ByVal tipo_mensaje As String, ByVal resultado_operacion As String)
        Try
            dict.Add("mensaje", mensaje)
            dict.Add("tipo_mensaje", tipo_mensaje)
            dict.Add("resultado_operacion", resultado_operacion)

            list.Add(dict)

            json_result = serializer.Serialize(list)
            Response.Write(json_result)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_RegistrarAsistencia(ByVal codigo_cpe As Integer, ByVal nro_documento As String, ByVal tipo_operacion As String)
        Try
            If Not fu_ValidarRegistrarAsistencia(codigo_cpe, nro_documento, tipo_operacion) Then Exit Sub

            Dim le_Marcaciones As e_Marcaciones : Dim ld_Marcaciones As New d_Marcaciones

            le_Marcaciones = ld_Marcaciones.GetMarcaciones(0)

            With le_Marcaciones
                .operacion = "I"
                .dni_per = nro_documento
                .id_marcador = "14" 'MARCADOR VIRTUAL
                .tipo_mar = "A"
                .codigo_cpe = codigo_cpe
                .tipo_operacion = tipo_operacion
            End With

            ld_Marcaciones.RegistrarMarcaciones(le_Marcaciones)

            If tipo_operacion = "I" Then
                Call mt_ShowMessage("¡Su ingreso se registró exitosamente!", "success", "success")
            ElseIf tipo_operacion = "F" Then
                Call mt_ShowMessage("¡Su salida se registró exitosamente!", "success", "success")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function fu_ValidarRegistrarAsistencia(ByVal codigo_cpe As Integer, ByVal nro_documento As String, ByVal tipo_operacion As String) As Boolean
        Try
            If cod_user = 0 Then mt_ShowMessage("Código de usuario inválido.", "warning", "warning") : Return False
            If codigo_cpe = 0 Then mt_ShowMessage("Código de horario inválido.", "warning", "warning") : Return False
            If String.IsNullOrEmpty(nro_documento) Then mt_ShowMessage("Número de documento de identidad inválido.", "warning", "warning") : Return False
            If tipo_operacion <> "I" AndAlso tipo_operacion <> "F" Then mt_ShowMessage("Tipo de operación inválido.", "warning", "warning") : Return False

            Dim dt As New DataTable
            Dim le_ControlPersonal As New e_ControlPersonal : Dim ld_ControlPersonal As New d_ControlPersonal

            With le_ControlPersonal
                .operacion = "VAL"
                .codigo_cpe = codigo_cpe
                .codigo_per = cod_user
                .tipo = tipo_operacion
            End With

            dt = ld_ControlPersonal.ListarControlPersonal(le_ControlPersonal)

            If dt.Rows.Count = 0 Then
                If tipo_operacion = "I" Then
                    mt_ShowMessage("Estimado usuario, se encuentra fuera del tiempo establecido para registrar su ingreso.", "warning", "warning") : Return False
                ElseIf tipo_operacion = "F" Then
                    mt_ShowMessage("Estimado usuario, se encuentra fuera del tiempo establecido para registrar su salida.", "warning", "warning") : Return False
                End If
            Else
                For i As Integer = 0 To dt.Rows.Count - 1
                    If Not String.IsNullOrEmpty(dt.Rows(i).Item("mensaje").ToString) Then
                        mt_ShowMessage(dt.Rows(i).Item("mensaje").ToString, "warning", "warning") : Return False
                    End If
                Next                
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class

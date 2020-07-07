Imports Personal
Imports System.Collections.Generic
Imports System.Net
Imports System.IO

Partial Class FrmInscripcionCongreso
    Inherits System.Web.UI.Page


    Protected Sub btnInscribir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInscribir.Click

        Try
            Dim list As New Dictionary(Of String, String)
            ' Response.Write(IdArchivo)
            'list.Add("codigo_cco", Me.txtceco.Text)
            If Request.QueryString("cco") <> "" Then
                list.Add("codigo_cco", Request.QueryString("cco"))
            Else
                list.Add("codigo_cco", "0")
            End If

            list.Add("tipo_doc", Me.ddltipodoc.SelectedValue)
            list.Add("nro_doc", Me.txtnrodoc.Text)
            list.Add("apepat", Me.txtapepat.Text)
            list.Add("apemat", Me.txtapemat.Text)
            list.Add("nombre", Me.txtnombres.Text)
            list.Add("fecha_nac", Me.txtfechaNacimiento.Value)
            list.Add("sexo", Me.ddlsexo.SelectedValue)
            list.Add("email", Me.txtemail.Text)
            list.Add("universidad", Me.codUniversidad.Value)
            Dim obj As New ClsCongreso
            Dim envelope As String = obj.SoapEnvelope(list)

            Dim result As String = obj.PeticionRequestSoap("http://serverdev/campusvirtual/Congreso/CongresoUsat.asmx", envelope, "http://tempuri.org/Inscripcion", "serverdev\esaavedra") ' Session("perlogin").ToString
            result = obj.ResultFile(result)
            If result = -1 Then
                Me.mensaje.Attributes.Add("class", "alert alert-danger")
                Me.mensaje.InnerHtml() = "<p>No se pudo Inscribir.</p>"
            ElseIf result = -2 Then
                limpiar()
                Me.mensaje.Attributes.Remove("class")
                Me.mensaje.InnerHtml = ""
                Me.mensaje.Attributes.Add("class", "alert alert-danger")
                Me.mensaje.InnerHtml() = "<p>Ya se encuentra Registrado.</p>"
            Else
                limpiar()
                Me.mensaje.Attributes.Remove("class")
                Me.mensaje.InnerHtml = ""
                Me.mensaje.Attributes.Add("class", "alert alert-success")
                Me.mensaje.InnerHtml() = "<p>Registado Correctamente. cod : " + result + "</p>"
            End If

        Catch ex As Exception
            Me.mensaje.Attributes.Add("class", "alert alert-danger")
            Me.mensaje.InnerHtml() = "<p>No se pudo Inscribir.</p>" + ex.Message.ToString

        End Try

    End Sub


    Sub limpiar()
        Me.ddltipodoc.SelectedValue = 0
        Me.txtnrodoc.Text = ""
        Me.txtapepat.Text = ""
        Me.txtapemat.Text = ""
        Me.txtnombres.Text = ""
        Me.txtfechaNacimiento.Value = ""
        Me.ddlsexo.SelectedValue = 0
        Me.txtemail.Text = ""
        Me.ddlUniversidad.Value = 0
        Me.codUniversidad.Value = 0
    End Sub

    'Protected Sub ListaUniversidades()
    '    Try
    '        Dim obj As New ClsCongreso
    '        Dim envelope As String = obj.SoapEnvelopeVacio()
    '        Dim result As String

    '        result = obj.PeticionRequestSoap2("http://serverdev/campusvirtual/Congreso/CongresoUsat.asmx", envelope, "http://tempuri.org/Universidades", "serverdev\esaavedra") ' Session("perlogin").ToString
    '        result = obj.ResultFile(result)
    '        Response.Write(result)
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If IsPostBack = False Then
            '

        End If
    End Sub
End Class

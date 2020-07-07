Imports System.IO
Imports System.Collections.Generic
Imports System.Net
Partial Class logistica_RegularizarArchivos
    Inherits System.Web.UI.Page
    Dim objL As New ClsLogistica
    Dim ob As New ClsArchivosCompartidos

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try

            Dim rpta As String
            For index As Integer = 0 To Me.GridView1.Rows.Count - 1
				Response.Write("antes if br/>")
                If (File.Exists(MapPath("archivos/" & Me.GridView1.Rows(index).Cells(2).Text))) Then
                    Dim dict As New Dictionary(Of String, String)
					Response.Write("despues if")
                    dict.Add("Fecha", CType(Me.GridView1.Rows(index).Cells(0).Text, Date).ToString)
                    dict.Add("Extencion", Me.GridView1.Rows(index).Cells(1).Text)
                    dict.Add("Nombre", Me.GridView1.Rows(index).Cells(2).Text)
                    dict.Add("TransaccionId", Me.GridView1.Rows(index).Cells(3).Text)
                    dict.Add("TablaId", "2")
                    dict.Add("NroOperacion", Me.GridView1.Rows(index).Cells(4).Text)
                    dict.Add("Archivo", objL.ConvertFileToBase64(MapPath("archivos/" & Me.GridView1.Rows(index).Cells(2).Text)))
                    dict.Add("Usuario", Session("perlogin"))
                    dict.Add("Equipo", Split(Dns.GetHostEntry(Request.ServerVariables("remote_addr")).HostName, ".")(0).ToUpper)
                    dict.Add("Ip", "")
                    dict.Add("param8", Session("perlogin").ToString)
                    Response.Write(ob.SoapEnvelope(dict))
					Response.Write("<br/>")
                    'rpta = ob.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", ob.SoapEnvelope(dict), "http://usat.edu.pe/UploadFile", Session("perlogin"))
                    rpta = ob.PeticionRequestSoap("http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", ob.SoapEnvelope(dict), "http://usat.edu.pe/UploadFile", Session("perlogin"))
                    Response.Write(rpta)
                    If rpta.Contains("OK") Then
                        Response.Write("codigo_dep:" & Me.GridView1.Rows(index).Cells(3).Text & "-" & "codigo_ped:" & Me.GridView1.Rows(index).Cells(4).Text & "-" & "codigo_ped:" & Me.GridView1.Rows(index).Cells(2).Text)
                        'System.IO.File.Delete(MapPath("archivos/" & Me.GridView1.Rows(index).Cells(2).Text))
                    End If

                End If

            Next
           Response.Write("fin")
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub
   
    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        Dim dirs() As String
        Dim cantidad As Integer
        dirs = Directory.GetFiles(MapPath("archivos/"))

        cantidad = dirs.Length
        Response.Write(cantidad & " / " & Me.GridView1.Rows.Count)
    End Sub
    Protected Function ReemplazaTilde()

    End Function
End Class

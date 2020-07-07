﻿Imports System.Collections.Generic
Imports System.Xml
Imports System.IO

Partial Class CRISUSAT_FrmUnidadOrganizacional
    Inherits System.Web.UI.Page

    Public ruta As String = "http://10.10.1.203/webservices/"
    'Public SOAPAaction As String = "http://4science.github.io/dspace-cris/definitions/cris/NormalAuthQueryRequest"
    Public SOAPAaction As String = "http://10.10.1.203/dspace-cris/definitions/NormalAuthQueryRequest"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("id_per") = "" Then
        '    Response.Redirect("../../sinacceso.html")
        'End If
        Try
            If IsPostBack = False Then

            End If
        Catch ex As Exception
            'mensaje("alert alert-danger", "No se pudo cargar la página")
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Function ConsultarPersona() As Data.DataTable
        Dim dt As New Data.DataTable
        Try
            Dim obj As New ClsDspaceCrisUsat

            Dim list As New Dictionary(Of String, String)
            '  Dim list As New List(Of Dictionary(Of String, String))()
            list.Add("sql", "*:*") ' Consulta SOLR
            list.Add("FilasPorPagina", "1")
            list.Add("PaginaInicio", "0")
            list.Add("EntidadSimple", "researcher")
            list.Add("Columnas", "fullName personalsite email orcid interests biography usatID")
            list.Add("Entidad", "researcherPages")

            Dim envelope As String = obj.SoapEnvelopeListar(list)
            Dim result As String = obj.PeticionRequestSoap(ruta, envelope, SOAPAaction, "")

            'Me.txtRespuesta.Text = result

            dt.Columns.Add("cod_rp") 'Columna2
            dt.Columns.Add("Persona") 'Columna2
            dt.Columns.Add("Web") 'Columna2
            dt.Columns.Add("email") 'Columna2
            dt.Columns.Add("orcid") 'Columna2
            dt.Columns.Add("intereses") 'Columna2
            dt.Columns.Add("biografia") 'Columna2
            dt.Columns.Add("usatID") 'Columna2
            'dt.Columns.Add("UnidadPadre") 'Columna2

            Dim doc As New XmlDocument()
            doc.LoadXml(result)

            Dim nodos As XmlNodeList = doc.GetElementsByTagName("rp:crisobject")

            If nodos.Count > 0 Then ' Verificamos si hay filas para mostrar
                Dim i As Integer
                For i = 0 To nodos.Count - 1 ' recorremos las filas

                    Dim arreglo(nodos(i).ChildNodes.Count + 1) As String ' declaro arreglo variable para cada fila

                    'arreglo(0) = nodos(i).Attributes("businessID").Value.ToString()
                    arreglo(0) = nodos(i).Attributes("publicID").Value.ToString()
                    For j As Integer = 0 To nodos(i).ChildNodes.Count - 1 'llenamos arreglo (5 Columnas maximo por ahora [sino agregar columnas en #001])
                        'arreglo(j + 1) = nodos(i).ChildNodes(j).InnerText.ToString
                        If nodos(i).SelectSingleNode("rp:fullName").BaseURI <> "" Then
                            arreglo(j + 1) = nodos(i).Item("rp:email").InnerText

                        End If

                        'Dim doc1 As New XmlDocument()
                        'doc1.LoadXml(nodos(i).InnerXml)
                        'Me.txtRespuesta.Text = doc1.SelectSingleNode("rp:fullName").InnerText
                    Next j

                    dt.Rows.Add(arreglo) ' Agrego Fila con arreglo

                Next i

                Me.gvUnidadOrganizacional1.DataSource = dt
                Me.gvUnidadOrganizacional1.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
        Return dt
    End Function

    Protected Sub ddTipoPersona_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddTipoPersona.SelectedIndexChanged
        Try

            'EN BD DSPACECRIS
            Dim dtDspace As New Data.DataTable
            dtDspace = ConsultarPersona()

            Me.gvUnidadOrganizacional.DataSource = dtDspace
            Me.gvUnidadOrganizacional.DataBind()


            'If Me.ddTipoUnidad.SelectedValue <> "0" Then
            '    'En BDUSAT
            '    Dim objUSATCRIS As New ClsUSATCRIS
            '    Dim dtUsat As New Data.DataTable
            '    dtUsat = objUSATCRIS.ListarUnidadOrganizacional(Me.ddTipoUnidad.SelectedValue.ToString)
            '    'EN BD DSPACECRIS
            '    Dim dtDspace As New Data.DataTable
            '    dtDspace = ConsultarUnidadOrganizacional()

            '    dtUsat.Columns.Add("USATCRIS")
            '    dtUsat.Columns.Add("IDCRIS")
            '    If dtUsat.Rows.Count > 0 Then

            '        For i As Integer = 0 To dtUsat.Rows.Count - 1
            '            If dtDspace.Rows.Count > 0 Then
            '                dtUsat.Rows(i).Item("USATCRIS") = ""
            '                dtUsat.Rows(i).Item("IDCRIS") = ""
            '                For j As Integer = 0 To dtDspace.Rows.Count - 1
            '                    If dtUsat.Rows(i).Item("codigo").ToString = dtDspace.Rows(j).Item("ID").ToString Then
            '                        dtUsat.Rows(i).Item("USATCRIS") = "SI"
            '                        dtUsat.Rows(i).Item("IDCRIS") = "ou" + String.Format("{0:00000}", CInt(dtDspace.Rows(j).Item("cod_ou").ToString))
            '                        Exit For
            '                    End If
            '                Next
            '            End If
            '        Next
            '        Me.gvUnidadOrganizacional.DataSource = dtUsat
            '        Me.gvUnidadOrganizacional.DataBind()
            '    End If
            'Else

            '    Me.gvUnidadOrganizacional.DataSource = ""
            '    Me.gvUnidadOrganizacional.DataBind()
            'End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Try

            'Response.Clear()
            'Response.Buffer = True
            'Response.ContentType = "application/vnd.ms-excel; charset=UTF-8;base64"
            'Response.Charset = "UTF-8"
            'Response.ContentEncoding = System.Text.Encoding.UTF8

            'Response.AddHeader("content-disposition", "attachment;filename=exportOU" & ".xls")

            'Response.ContentType = "application/vnd.ms-excel"
            ''Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            ''Dim sWriter As New StringWriter()
            ''Dim hWriter As New HtmlTextWriter(sWriter)

            'Dim Cab As New StringBuilder

            'Dim filas As Integer = 0
            'filas = Me.gvUnidadOrganizacional.Rows.Count

            'Cab.Append("<table>")
            'Cab.Append("<thead>")
            'Cab.Append("<tr>")
            'Cab.Append("<th>ACTION</th>")
            'Cab.Append("<th>CRISID</th>")
            'Cab.Append("<th>UUID</th>")
            'Cab.Append("<th>SOURCEREF</th>")
            'Cab.Append("<th>SOURCEID</th>")
            'Cab.Append("<th>name</th>")
            ''Cab.Append("<th scope='col'>cfAcro</th>")
            'Cab.Append("<th>pePostAddress</th>")
            'Cab.Append("<th>peUbigeoSede</th>")
            'Cab.Append("<th>iso-country</th>")
            'Cab.Append("<th>NONE</th>")
            'Cab.Append("</tr>")
            'Cab.Append("</thead>")
            'Cab.Append("<tbody>")
            'With gvUnidadOrganizacional
            '    For i As Integer = 0 To filas - 1
            '        Cab.Append("<tr>")
            '        If .Rows(i).Cells(2).Text.ToString = "SI" Then 'SI YA CUENTA CON UN ID EN CRIS ACTUALIZAMOS
            '            Cab.Append("<td>UPDATE</td>")
            '        Else
            '            Cab.Append("<td>SHOW</td>") ' SI NO CREAMOS VISIBLE
            '        End If

            '        If .Rows(i).Cells(2).Text.ToString = "SI" Then
            '            Cab.Append("<td>" & .Rows(i).Cells(3).Text.ToString & "</td>")
            '        Else
            '            Cab.Append("<td></td>")
            '        End If
            '        Cab.Append("<td></td>")
            '        Cab.Append("<td></td>")
            '        Cab.Append("<td>" & .Rows(i).Cells(0).Text.ToString & "</td>")
            '        Cab.Append("<td>[visibility=PUBLIC]" & .Rows(i).Cells(1).Text.ToString & "</td>")
            '        'Cab.Append("<td align='center'>" & .Rows(i).Cells(4).Text.ToString & "</td>")
            '        'Cab.Append("<td align='center'>" & .Rows(i).Cells(5).Text.ToString & "</td>")
            '        Cab.Append("<td>[visibility=PUBLIC]Av. San Josemaría Escrivá de Balaguer Nº 855 Chiclayo - Perú</td>")
            '        Cab.Append("<td>[visibility=PUBLIC]http://purl.org/pe-repo/inei/ubigeo/1401</td>")
            '        Cab.Append("<td>[visibility=PUBLIC]PE</td>")
            '        Cab.Append("<td>#</td>")
            '        Cab.Append("</tr>")
            '    Next

            'End With
            'Cab.Append("</tbody>")
            'Cab.Append("</table>")

            'Response.Write("<html><head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'></head><body>" & Cab.ToString & "</body></html>")

            'Response.Flush()
            'Response.[End]()
        Catch ex As Exception
            Response.Write(ex.ToString())
        End Try
    End Sub
End Class

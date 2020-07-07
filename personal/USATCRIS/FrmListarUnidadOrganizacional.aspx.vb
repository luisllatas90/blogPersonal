Imports System.Collections.Generic
Imports System.Net
Imports System.IO
Imports System.Xml

'Imports System.Xml.Serialization
Partial Class USATCRIS_FrmListarUnidadOrganizacional
    Inherits System.Web.UI.Page

    Public ruta As String = "http://10.10.1.202:8080/jspui/webservices/"
    Public SOAPAaction As String = "http://4science.github.io/dspace-cris/definitions/cris/NormalAuthQueryRequest"

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        Try

            If Not IsPostBack Then
                Dim obj As New ClsDspaceCrisUsat
                Dim list As New Dictionary(Of String, String)
                '  Dim list As New List(Of Dictionary(Of String, String))()
                list.Add("Fecha", "")
                Dim envelope As String = obj.SoapEnvelopeListar(list)

                Dim result As String = obj.PeticionRequestSoap(ruta, envelope, SOAPAaction, "")
                'Dim reader As New StreamReader(result)

                'Dim result As String = obj.UrlServices(envelope, ruta)
                'Dim dt As New Data.DataTable
                'dt.WriteXml(result)
                'Response.Write(reader.ReadToEnd())

                'Response.Write(result)

                '#001
                Dim dt As New Data.DataTable

                dt.Columns.Add("Nombre") 'Columna1
                dt.Columns.Add("Descripcion") 'Columna2
                dt.Columns.Add("Director") 'Columna3
                dt.Columns.Add("Ciudad") 'Columna4
                dt.Columns.Add("Dependencia") 'Columna5


                Dim doc As New XmlDocument()
                doc.LoadXml(result)

                Dim nodos As XmlNodeList= doc.GetElementsByTagName("orgunit:crisobject")
                If nodos.Count > 0 Then ' Verificamos si hay filas para mostrar
                    Dim i As Integer
                    For i = 0 To nodos.Count - 1 ' recorremos las filas
                        Dim arreglo(nodos(i).ChildNodes.Count - 1) As String ' declaro arreglo variable para cada fila
                        For j As Integer = 0 To nodos(i).ChildNodes.Count - 1 'llenamos arreglo (5 Columnas maximo por ahora [sino agregar columnas en #001])
                            arreglo(j) = nodos(i).ChildNodes(j).InnerText.ToString
                        Next j
                        dt.Rows.Add(arreglo) ' Agrego Fila con arreglo
                    Next i

                    Me.gvUnidadOrg.DataSource = dt
                    Me.gvUnidadOrg.Sort(1, SortDirection.Ascending)
                    Me.gvUnidadOrg.DataBind()
                Else
                    Me.gvUnidadOrg.DataSource = Nothing
                    Me.gvUnidadOrg.DataBind()
                End If
                'Response.Write("<script>alert('" + nodoraiz.Item.ToString + " - " + elemento.Item.ToString + "')</script>")
                'Response.Write("<script>alert('" + nodos.Count.ToString + "')</script>")


                ' Para Investigadores

                Dim listInv As New Dictionary(Of String, String)
                '  Dim list As New List(Of Dictionary(Of String, String))()
                listInv.Add("Fecha", "")
                Dim envelopeInv As String = obj.SoapEnvelopeINV(listInv)
                'Response.Write(envelopeInv)
                Dim resultInv As String = obj.PeticionRequestSoap(ruta, envelopeInv, SOAPAaction, "")

                Dim dtI As New Data.DataTable

                dtI.Columns.Add("Investigador") 'Columna1
                dtI.Columns.Add("Email") 'Columna2
                dtI.Columns.Add("Orcid") 'Columna3


                Dim docI As New XmlDocument()
                docI.LoadXml(resultInv)

                nodos = docI.GetElementsByTagName("rp:crisobject")
                If nodos.Count > 0 Then ' Verificamos si hay filas para mostrar
                    For i As Integer = 0 To nodos.Count - 1 ' recorremos las filas
                        Dim arreglo(nodos(i).ChildNodes.Count - 1) As String ' declaro arreglo variable para cada fila                        
                        For j As Integer = 0 To nodos(i).ChildNodes.Count - 1
                            arreglo(j) = nodos(i).ChildNodes(j).InnerText.ToString
                        Next j
                        dtI.Rows.Add(arreglo) ' Agrego Fila con arreglo
                    Next i

                    Me.gvInvestigadores.DataSource = dtI
                    Me.gvInvestigadores.DataBind()
                    Me.gvInvestigadores.Sort(0, SortDirection.Ascending)

                Else
                    Me.gvInvestigadores.DataSource = Nothing
                    Me.gvInvestigadores.DataBind()
                End If


            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub



    Protected Sub gvInvestigadores_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvInvestigadores.Sorting

    End Sub

    Protected Sub gvUnidadOrg_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvUnidadOrg.Sorting

    End Sub
End Class

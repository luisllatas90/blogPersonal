Imports System.Data

Partial Class consultarhorariodpto
    Inherits System.Web.UI.Page
    Private Function AnchoHora(ByVal cad As String) As String
        If Len(cad) < 2 Then
            AnchoHora = "0" & cad
        Else
            AnchoHora = cad
        End If
    End Function
    Private Sub MostrarHorario()
        Dim eFila As TableRow
        Dim Celda As TableCell
        Dim UltimoDia As Int16 = 7
        Dim inicioBD, finBD As Integer
        Dim diaBD, dia As String
        Dim hora As Integer


        'Me.tblHorario.Rows.Clear()

        Dim obj As New clsaccesodatos
        Dim tbl As Data.DataTable
        obj.abrirconexion()
        tbl = obj.TraerDataTable("ConsultarHorarios", 14, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_dac.SelectedValue, 0)
        obj.cerrarconexion()

        'Llenar las filas con horas
        For f As Int16 = 7 To 23 'Me.dpInicio.Items.Count - 1
            eFila = New TableRow
            'Llenar celdas por día
            For c As Int16 = 1 To UltimoDia + 1

                If c = 1 Then
                    Celda = New TableCell

                    'Celda.Text = f.ToString & ":10" & " - " & (f + 1).ToString & ":00" 'linea de codigo que incluye los 10 min

                    '==============================================================================
                    Celda.Text = f.ToString & ":00" & " - " & (f + 1).ToString & ":00"      'linea para mostrar horas exactas
                    '==============================================================================

                    eFila.Cells.Add(Celda)
                    Celda = Nothing
                Else 'Marcar la celda ocupada
                    Celda = New TableCell

                    If c = 2 Then dia = "LU"
                    If c = 3 Then dia = "MA"
                    If c = 4 Then dia = "MI"
                    If c = 5 Then dia = "JU"
                    If c = 6 Then dia = "VI"
                    If c = 7 Then dia = "SA"
                    If c = 8 Then dia = "DO"

                    hora = AnchoHora(f) 'Aumenta 6 para que se inicie a las 7:00 am

                    For h As Int16 = 0 To tbl.Rows.Count - 1

                        diaBD = Mid(tbl.Rows(h).Item("dia_lho").ToString, 1, 2)
                        inicioBD = Mid(tbl.Rows(h).Item("nombre_hor").ToString, 1, 2)
                        finBD = Mid(tbl.Rows(h).Item("horafin_lho").ToString, 1, 2)

                        'si el día es el mismo y la horaactual es menor que horafin y mayor que la horainicio
                        If Trim(dia) = Trim(diaBD) And Int(hora) >= Int(inicioBD) And Int(hora) < Int(finBD) Then
                            Celda.CssClass = "Marcado" 'tbl.Rows(h).Item("color_hor").ToString
                            If tbl.Rows(h).Item("estadoHorario_lho").ToString = "A" Then
                                Celda.Text = tbl.Rows(h).Item("docente").ToString & "<br />" & tbl.Rows(h).Item("nombre_cur").ToString & "-<font color='blue'>" & tbl.Rows(h).Item("ambiente").ToString & "</font>"
                            Else
                                Celda.Text = tbl.Rows(h).Item("docente").ToString & "<br />" & tbl.Rows(h).Item("nombre_cur").ToString & "-<font color='red'>" & tbl.Rows(h).Item("ambiente").ToString & "</font>"
                            End If
                        End If
                    Next
                    eFila.Cells.Add(Celda)
                    Celda = Nothing
                End If
            Next

            Me.tblHorario.Rows.Add(eFila)
        Next

        tbl.Dispose()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New clsaccesodatos
            Dim lista As New ClsFunciones
            obj.abrirconexion()
            lista.CargarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0), "codigo_cac", "descripcion_cac", "--Seleccionar--")
            lista.CargarListas(Me.dpCodigo_dac, obj.TraerDataTable("ConsultarDepartamentoAcademico", "TO", 0), "codigo_dac", "nombre_Dac")

            obj.cerrarconexion()
            obj = Nothing
            lista = Nothing
        End If
    End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Me.MostrarHorario()
    End Sub
End Class
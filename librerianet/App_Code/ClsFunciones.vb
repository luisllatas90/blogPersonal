Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Public Class ClsFunciones
    Public Shared Sub LlenarListas(ByVal Combo As System.Object, ByVal Datos As System.Data.DataTable, _
    ByVal ColValor As System.Object, ByVal ColDescripcion As System.Object)
        Combo.Items.Clear()
        Combo.DataSource = Datos
        Combo.DataValueField = Datos.Columns(ColValor).ToString
        Combo.DataTextField = Datos.Columns(ColDescripcion).ToString
        Combo.DataBind()
    End Sub

    Public Shared Sub LlenarListas(ByVal Combo As System.Object, ByVal Datos As System.Data.DataTable, _
    ByVal ColValor As System.Object, ByVal ColDescripcion As System.Object, ByVal Mensaje As String)
        'El valor predeterminado cuando no existe seleccion es (-2)
        Combo.Items.Clear()
        Combo.Items.Add(Mensaje)
        Combo.Items(0).Value = -1
        For i As Integer = 1 To Datos.Rows.Count
            Combo.Items.Add(Datos.Rows(i - 1).Item(ColDescripcion).ToString)
            Combo.Items(i).Value = Datos.Rows(i - 1).Item(ColValor).ToString
        Next
    End Sub

    Public Shared Sub LlenarListas(Of T)(ByVal Combo As System.Object, ByVal lst_Datos As List(Of T), _
    ByVal ColValor As System.Object, ByVal ColDescripcion As System.Object)
        Combo.Items.Clear()
        Combo.DataSource = lst_Datos
        Combo.DataValueField = ColValor
        Combo.DataTextField = ColDescripcion
        Combo.DataBind()
    End Sub

    Public Shared Sub LlenarListas(Of T)(ByVal Combo As System.Object, ByVal lst_Datos As List(Of T), _
    ByVal ColValor As System.Object, ByVal ColDescripcion As System.Object, ByVal Mensaje As String)
        Combo.Items.Clear()
        Combo.Items.Add(Mensaje)
        Combo.Items(0).Value = -1
        For Each _Dato As T In lst_Datos
            Combo.Items.Add(CallByName(_Dato, ColDescripcion, CallType.Get).ToString)
            Combo.Items(Combo.Items.count - 1).value = CallByName(_Dato, ColValor, CallType.Get).ToString
        Next
    End Sub

    Public Shared Function CargaCalendario() As String
        Dim Rutina As String
        Rutina = "<script>"
        Rutina = Rutina & "PopCalendar=getCalendarInstance();"
        Rutina = Rutina & "PopCalendar.startAt = 0;"
        Rutina = Rutina & "PopCalendar.showWeekNumber = 0;"
        Rutina = Rutina & "PopCalendar.showToday = 1;"
        Rutina = Rutina & "PopCalendar.showWeekend = 1;"
        Rutina = Rutina & "PopCalendar.showHolidays = 1;"
        Rutina = Rutina & "PopCalendar.selectWeekend = 0;"
        Rutina = Rutina & "PopCalendar.selectHoliday = 0;"
        Rutina = Rutina & "PopCalendar.addCarnival = 0;"
        Rutina = Rutina & "PopCalendar.addGoodFriday = 1;"
        Rutina = Rutina & "PopCalendar.language = 0;"
        Rutina = Rutina & "PopCalendar.defaultFormat ='mm-dd-yyyy';"
        Rutina = Rutina & "PopCalendar.fixedX = -1;"
        Rutina = Rutina & "PopCalendar.fixedY = -1;"
        Rutina = Rutina & "PopCalendar.fade = 0;"
        Rutina = Rutina & "PopCalendar.shadow = 1;"
        Rutina = Rutina & "PopCalendar.move = 1;"
        Rutina = Rutina & "PopCalendar.saveMovePos = 1;"
        Rutina = Rutina & "PopCalendar.centuryLimit = 40;"
        Rutina = Rutina & "PopCalendar.initCalendar();"
        Rutina = Rutina & "</script>"
        Return Rutina
    End Function

    Public Shared Function DevuelveDatos(ByVal cod_Parametro As Integer) As String

        Select Case cod_Parametro
            Case 1 : Return "Sistema de Gestion Hospitalaria"
            Case Else : Return Nothing
        End Select

    End Function


    Public Shared Function HTML(ByVal gridview As GridView, ByVal titulo As String, ByVal piepagina As String) As String
        Try
            Dim Page1 As New Page()
            Dim Form2 As New HtmlForm()
            Dim label As New Label
            Dim labelFecha As New Label
            Dim lblPiePag As New Label

            Page1.EnableViewState = False
            Page1.Controls.Add(Form2)
            Page1.EnableEventValidation = False

            label.Text = titulo
            labelFecha.Font.Size = 8
            labelFecha.Text = "<br>Emitido: " & Now.ToString & "<br><br>"

            label.ForeColor = Drawing.Color.Black
            label.Font.Bold = True
            label.Font.Size = 12
            lblPiePag.Text = piepagina
            lblPiePag.ForeColor = Drawing.Color.Blue
            label.Font.Size = 10
            Form2.Controls.Add(label)
            Form2.Controls.Add(labelFecha)
            Form2.Controls.Add(gridview)
            Form2.Controls.Add(lblPiePag)

            Dim builder1 As New System.Text.StringBuilder()
            Dim writer1 As New System.IO.StringWriter(builder1)
            Dim writer2 As New HtmlTextWriter(writer1)

            Page1.DesignerInitialize()
            Page1.RenderControl(writer2)
            Page1.Dispose()
            Page1 = Nothing

            Return builder1.ToString()
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub Seleccionar(ByVal texto As String, ByRef combo As RadioButtonList)
        Dim i As Int16
        For i = 0 To combo.Items.Count - 1
            If combo.Items(i).Text = texto Then
                combo.SelectedIndex = i
            End If
        Next
    End Sub

    Public Sub Seleccionar(ByVal texto As String, ByRef combo As DropDownList)
        Dim i As Int16
        For i = 0 To combo.Items.Count - 1
            If combo.Items(i).Text = texto Then
                combo.SelectedIndex = i
            End If
        Next
    End Sub

    Public Sub Seleccionar(ByVal texto As String, ByRef combo As DropDownList, ByRef cajatexto As TextBox, ByVal valorcombo As String)
        Dim i, sw As Int16
        sw = 0
        For i = 0 To combo.Items.Count - 1
            If combo.Items(i).Text = HttpUtility.HtmlDecode(texto) Then
                combo.SelectedIndex = i
                sw = 1
            End If
        Next
        If sw = 0 Then
            cajatexto.Text = HttpUtility.HtmlDecode(texto)
            combo.SelectedValue = valorcombo
        End If
    End Sub

    Public Sub CargarListas(ByVal Combo As System.Object, ByVal Datos As System.Data.DataTable, _
    ByVal ColValor As System.Object, ByVal ColDescripcion As System.Object)
        Combo.Items.Clear()
        Combo.DataSource = Datos
        Combo.DataValueField = Datos.Columns(ColValor).ToString
        Combo.DataTextField = Datos.Columns(ColDescripcion).ToString
        Combo.DataBind()
    End Sub

    Public Sub CargarListas(ByVal Combo As System.Object, ByVal Datos As System.Data.DataTable, _
    ByVal ColValor As System.Object, ByVal ColDescripcion As System.Object, ByVal Mensaje As String)
        'El valor predeterminado cuando no existe seleccion es (-1)
        Combo.Items.Clear()
        Combo.Items.Add(Mensaje)
        Combo.Items(0).Value = -1
        For i As Integer = 1 To Datos.Rows.Count
            Combo.Items.Add(Datos.Rows(i - 1).Item(ColDescripcion).ToString)
            Combo.Items(i).Value = Datos.Rows(i - 1).Item(ColValor).ToString
        Next
    End Sub
End Class

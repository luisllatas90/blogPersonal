Imports Microsoft.VisualBasic

 

Public Class ClsFunciones    
    Public shared Sub LlenarListas(ByVal Combo As System.Object, ByVal Datos As System.Data.DataTable, _
    ByVal ColValor As System.Object, ByVal ColDescripcion As System.Object)
        Combo.Items.Clear()
        Combo.DataSource = Datos
        Combo.DataValueField = Datos.Columns(ColValor).ToString
        Combo.DataTextField = Datos.Columns(ColDescripcion).ToString
        Combo.DataBind()
    End Sub

    Public shared Sub LlenarListas(ByVal Combo As System.Object, ByVal Datos As System.Data.DataTable, _
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


    Public Shared Function CargaCalendario() As String 'Shared=para que se referencie sin crearlo como objeto sino como método
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

    Public Shared Function HTML(ByVal gridview As GridView, ByVal titulo As String, ByVal piepagina As String) As String
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
    End Function
End Class

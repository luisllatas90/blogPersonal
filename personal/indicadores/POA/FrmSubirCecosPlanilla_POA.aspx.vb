Imports System.Data.OleDb
Imports System.Data
Imports System.IO

Partial Class indicadores_POA_PROTOTIPOS_FrmSubirCecosPlanilla_POA
    Inherits System.Web.UI.Page

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If FileUpload1.HasFile Then
            Dim FileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)
            'Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FolderPath As String = "CargaPlanilla/"

            Dim FilePath As String = Server.MapPath(FolderPath + FileName)
            FileUpload1.SaveAs(FilePath)
            'Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text)
            If Extension = ".xls" Then
                Import_To_Grid(FilePath, Extension, "Yes") ' Yes: Con Encabezado, No : Sin Encabezado
            Else
                GridView1.DataSource = Nothing
                GridView1.DataBind()
            End If

        End If
    End Sub

    Private Sub Import_To_Grid(ByVal FilePath As String, ByVal Extension As String, ByVal isHDR As String)
        Dim conStr As String = ""
       
        'Select Case Extension
        'Case ".xls"
        'Excel 97-03
        conStr = ConfigurationManager.ConnectionStrings("Excel03ConString") _
                   .ConnectionString
        'Exit Select
        'Case ".xlsx"
        '    'Excel 07
        '    conStr = ConfigurationManager.ConnectionStrings("Excel07ConString") _
        '              .ConnectionString
        '    Exit Select
        'End Select
        conStr = String.Format(conStr, FilePath, isHDR)

        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        'Get the name of First Sheet
        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
        connExcel.Close()

        'Read Data from First Sheet
        connExcel.Open()
        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()

        'Bind Data to GridView
        GridView1.Caption = "Centros de Costo para Programas de Planilla" ' Titulo de Tabla
        GridView1.DataSource = dt
        GridView1.DataBind()

    End Sub

    Protected Sub btnPlantilla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPlantilla.Click, btnPlantilla.Click
        Dim rutaPlantilla As String = "CargaPlanilla/Plantilla.xls"

        'Limpiamos la salida
        Response.Clear()
        'Con esto le decimos al browser que la salida sera descargable
        Response.ContentType = "application/octet-stream"
        'esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
        Response.AddHeader("Content-Disposition", "attachment; filename=Plantilla.xls")
        'Escribimos el fichero a enviar 
        Response.WriteFile(rutaPlantilla)
        'volcamos el stream 
        Response.Flush()
        'Enviamos todo el encabezado ahora
        Response.End()
    End Sub

    'Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
    '    If FileUpload1.HasFile Then
    '        Dim FileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
    '        Dim Extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)
    '        'Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
    '        Dim FolderPath As String = "CargaPlanilla/"

    '        Dim FilePath As String = Server.MapPath(FolderPath + FileName)
    '        FileUpload1.SaveAs(FilePath)
    '        'Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text)
    '        Import_To_Grid(FilePath, Extension, "Yes") ' Yes: Con Encabezado, No : Sin Encabezado
    '    End If
    'End Sub


End Class


Imports System.Data.OleDb
Imports Microsoft.SqlServer
Imports System.Data.SqlClient
Imports System.IO
Imports System.Collections.Generic

Partial Class Crm_FrmImportarInteresados
    Inherits System.Web.UI.Page

    Private cnx As New ClsConectarDatos
    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                CargarTipoEstudio()
                'Dim ScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
                'ScriptManager.RegisterPostBackControl(btnImportar)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarTipoEstudio()
        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaTipoEstudio("TO", 0)
        Me.ddlTipoEstudio.Items.Clear()
        Me.ddlTipoEstudio.Items.Add(New ListItem("--Seleccione--", ""))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim Lista As New ListItem(dt.Rows(i).Item("descripcion_test").ToString, dt.Rows(i).Item("codigo_test").ToString)
                Me.ddlTipoEstudio.Items.Add(Lista)
            Next
        End If
    End Sub

    Private Sub CargarConvocatorias()
        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaConvocatorias("C", 0, Me.ddlTipoEstudio.SelectedValue)
        Me.ddlConvocatoria.Items.Clear()
        Me.ddlConvocatoria.Items.Add(New ListItem("--Seleccione--", ""))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim Lista As New ListItem(dt.Rows(i).Item("descripcion").ToString, dt.Rows(i).Item("codigo").ToString)
                Me.ddlConvocatoria.Items.Add(Lista)
            Next
        End If
    End Sub

    Private Sub CargarEventos()
        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaEventos("C", 0, Me.ddlTipoEstudio.SelectedValue, Me.ddlConvocatoria.SelectedValue)
        Me.ddlEvento.Items.Clear()
        Me.ddlEvento.Items.Add(New ListItem("--Seleccione--", ""))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim Lista As New ListItem(dt.Rows(i).Item("descripcion").ToString, dt.Rows(i).Item("codigo").ToString)
                Me.ddlEvento.Items.Add(Lista)
            Next
        End If
    End Sub

    Protected Sub ddlTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoEstudio.SelectedIndexChanged
        Me.ddlEvento.Items.Clear()
        Me.ddlEvento.Items.Add(New ListItem("--Seleccione--", ""))
        If Me.ddlTipoEstudio.SelectedValue <> "" Then
            CargarConvocatorias()
        Else
            Me.ddlConvocatoria.Items.Clear()
            Me.ddlConvocatoria.Items.Add(New ListItem("--Seleccione--", ""))
        End If
    End Sub

    Protected Sub ddlConvocatoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlConvocatoria.SelectedIndexChanged
        If Me.ddlConvocatoria.SelectedValue <> "" Then
            CargarEventos()
        Else
            Me.ddlEvento.Items.Clear()
            Me.ddlEvento.Items.Add(New ListItem("--Seleccione--", ""))
        End If
    End Sub



    Protected Sub ButtonImportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportar.Click
        If Session("id_per") = "" Or Request.QueryString("id") = "" Then
            Response.Redirect("../../sinacceso.html")
        End If

        If Validar() = True Then

            'Dim path As String = Server.MapPath("../../ArchivosExportadosCRM/")
            Dim obj As New ClsCRM
            'Dim rpta As String = "0"

            Dim fileOK As Boolean = False
            If ArchivoASubir.HasFile Then
                Dim fileExtension As String
                fileExtension = System.IO.Path.GetExtension(ArchivoASubir.FileName).ToLower()
                'Dim ExtensionesPermitidas As String() = {".xls", ".xlsx"}
                Dim ExtensionesPermitidas As String() = {".csv"}

                For i As Integer = 0 To ExtensionesPermitidas.Length - 1
                    If fileExtension = ExtensionesPermitidas(i) Then
                        fileOK = True
                    End If
                Next

                If fileOK Then
                    Try
                        'Guardar Archivo
                        'Dim nombrefinal As String = Now.ToString("yyyy_MM_dd_H_mm_ss_") & Me.ArchivoASubir.FileName
                        'Dim rutafinal As String = path & nombrefinal
                        'Me.ArchivoASubir.PostedFile.SaveAs(rutafinal)

                        Dim RutaArchivo As String = ""
                        Dim nombrefinal As String = ""
                        Dim idarchivocompartido As Integer
                        Dim extension As String = ""
                        Dim idtabla As Integer = 10 ' ID DE TABLAARCHIVO
                        Dim idtransaccion As Integer = 0 ' ID DE TABLA RELACIONADA (EN ESTE CASO NO TENEMOS UN SOLO REGISTRO SINO VARIOS SE CONSIDERA 0)
                        Dim nrooperacion As Integer = 0 ' ID DE TABLA RELACIONADA OPCIONAL (EN ESTE CASO NO TENEMOS UN SOLO REGISTRO SINO VARIOS SE CONSIDERA 0)
                        Dim archivo As HttpPostedFile = HttpContext.Current.Request.Files("ArchivoASubir")
                        Dim codigo_per As Integer = Session("id_per")
                        SubirArchivo(idtabla, 0, archivo, 0, codigo_per)

                        'rpta = "1"

                        Dim dta As New Data.DataTable
                        dta = obj.ObtenerUltimoIDArchivoCompartiod(idtabla, idtransaccion, nrooperacion)
                        RutaArchivo = dta.Rows(0).Item("ruta").ToString
                        nombrefinal = dta.Rows(0).Item("NombreArchivo").ToString
                        idarchivocompartido = dta.Rows(0).Item("idarchivo")
                        extension = dta.Rows(0).Item("Extension")

                        'rpta = "2"

                        'Dim conexion As String
                        ''If System.IO.Path.GetExtension(Me.ArchivoASubir.FileName) = ".xls" Then
                        'If extension = ".xls" Then
                        '    'Archivos Excel 2003 (.xls)
                        '    conexion = "Provider=Microsoft.Jet.Oledb.4.0; Data Source='" + rutafinal + "';Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'"
                        'Else
                        '    'Archivos Excel 2007 a más (.xlsx)
                        '    conexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + rutafinal + "';Extended Properties='Excel 12.0 Xml;HDR=YES'"
                        'End If

                        'Dim cnn As OleDbConnection = New OleDbConnection(conexion)

                        'rpta = "3"

                        'cnn.Open()

                        'rpta = "4"

                        'Dim sql As String
                        'Dim command As OleDbCommand
                        'Dim da As OleDbDataAdapter
                        'Dim dt As New Data.DataTable

                        'sql = "SELECT * FROM [BASE GENERAL$] WHERE [NOMBRES] <> '' AND [APELLIDO_PATERNO] <> ''"
                        'command = New OleDbCommand(sql, cnn)
                        'da = New OleDbDataAdapter(command)
                        'da.Fill(dt)

                        'rpta = "5"

                        'sql = "SELECT  [FECHA],[DNI],[APELLIDO_PATERNO],[APELLIDO_MATERNO],[NOMBRES],[CELULAR],[EMAIL],[DIRECCION],[ID_DISTRITO],[DISTRITO],[PROVINCIA]" + _
                        '        ",[ID_COLEGIO],[COLEGIO],[ID_NIVEL],[NIVEL],[ID_CARRERA],[CARRERA],[ID_EVENTO],[CATEGORIA],[TIPO],0 AS [PROCESADO]" + _
                        '        ",DATE() AS [FECHA_REG],'" + Session("id_per") + "' AS [codigo_per]," + idarchivocompartido.ToString + " AS [IdArchivosCompartidos]" + _
                        '        " FROM [BASE GENERAL$] WHERE [NOMBRES] <> '' AND [APELLIDO_PATERNO] <> ''"

                        'command = New OleDbCommand(sql, cnn)
                        'da = New OleDbDataAdapter(command)

                        'da.Fill(dt)
                        'cnn.Close()

                        'rpta = "6"

                        'If dt.Rows.Count > 0 Then

                        '    Dim dtMigrar As New Data.DataTable

                        '    dtMigrar.Columns.Add("FECHA", Type.GetType("System.DateTime"))
                        '    dtMigrar.Columns.Add("DNI", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("APELLIDO_PATERNO", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("APELLIDO_MATERNO", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("NOMBRES", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("CELULAR", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("EMAIL", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("DIRECCION", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("ID_DISTRITO", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("DISTRITO", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("PROVINCIA", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("ID_COLEGIO", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("COLEGIO", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("ID_NIVEL", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("NIVEL", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("ID_CARRERA", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("CARRERA", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("ID_EVENTO", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("CATEGORIA", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("TIPO", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("PROCESADO", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("NOMBRE_ARCHIVO", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("FECHA_REG", Type.GetType("System.DateTime"))
                        '    dtMigrar.Columns.Add("codigo_per", Type.GetType("System.String"))
                        '    dtMigrar.Columns.Add("IdArchivosCompartidos", Type.GetType("System.Int32"))

                        '    rpta = "7"

                        '    Dim usuario_reg As String = "0"

                        '    If Session("id_per") <> "" Then
                        '        usuario_reg = Session("id_per")
                        '    End If

                        '    For i As Integer = 0 To dt.Rows.Count() - 1
                        '        dtMigrar.Rows.Add(dt.Rows(i).Item("FECHA").ToString, dt.Rows(i).Item("DNI").ToString, dt.Rows(i).Item("APELLIDO_PATERNO").ToString, dt.Rows(i).Item("APELLIDO_MATERNO").ToString, dt.Rows(i).Item("NOMBRES").ToString _
                        '                          , dt.Rows(i).Item("CELULAR").ToString, dt.Rows(i).Item("EMAIL").ToString, dt.Rows(i).Item("DIRECCION").ToString _
                        '                          , dt.Rows(i).Item("ID_DISTRITO").ToString, dt.Rows(i).Item("DISTRITO").ToString, dt.Rows(i).Item("PROVINCIA").ToString, dt.Rows(i).Item("ID_COLEGIO").ToString _
                        '                          , dt.Rows(i).Item("COLEGIO").ToString, dt.Rows(i).Item("ID_NIVEL").ToString, dt.Rows(i).Item("NIVEL").ToString, dt.Rows(i).Item("ID_CARRERA").ToString _
                        '                          , dt.Rows(i).Item("CARRERA").ToString, dt.Rows(i).Item("ID_EVENTO").ToString, dt.Rows(i).Item("CATEGORIA").ToString, dt.Rows(i).Item("TIPO").ToString _
                        '                          , "0", nombrefinal, Now.ToString("yyyy-MM-dd H:mm:ss"), usuario_reg, dt.Rows(i).Item("IdArchivosCompartidos"))
                        '    Next

                        '    rpta = "8"

                        'Dim respuesta As Integer = 0
                        'respuesta = obj.MigrarTabla(dtMigrar)

                        'Obtenemos Respuesta de Migración en una tabla
                        Dim dt As New Data.DataTable
                        dt = obj.MigrarExcelInteresados(RutaArchivo, Me.ddlEvento.SelectedValue, idarchivocompartido, codigo_per)

                        'Eliminamos el Archivo Importado (Por indicación de GLEON)
                        'File.Delete(RutaArchivo)

                        'Mensaje de Confirmación o error
                        If dt.Rows(0).Item("Respuesta") = 1 Then
                            FnMensaje(dt.Rows(0).Item("Mensaje").ToString, "success")
                        Else
                            FnMensaje(dt.Rows(0).Item("Mensaje").ToString, "danger") ' Serverdev
                            'FnMensaje("No se Puedo Importar Archivo.", "danger") ' Producción
                        End If

                        'rpta = rpta + "9"
                        'rpta = respuesta
                        'rpta = rpta + "10"
                        'FnMensaje("N° de Interesados Importados: " + respuesta.ToString, "success")
                        'Else
                        'FnMensaje("El Archivo no Cuenta con Filas que cumplan los requisitos minimos: DNI, APELLIDO_PATERNO, NOMBRES.", "danger")
                        'End If

                    Catch ex As Exception
                        'Select Case rpta
                        '    Case "1"
                        '        FnMensaje(rpta + "Archivo no se Pudo Subir.", "danger")
                        '    Case "2"
                        '        FnMensaje(rpta + "No se pudo leer Archivo inconvenientes con la extension ('.xls','.xlsx') o No se pudo generar cadena de conexion con Archivo.", "danger")
                        '    Case "3"
                        '        FnMensaje(rpta + "No se pudo Abrir Conexion a Archivo.", "danger")
                        '    Case "4"
                        '        FnMensaje(rpta + "Abrio conexion, pero no pudo ejecutar SQL, Verificar Archivo, Hoja de Excel [BASE GENERAL] no se encuentra en Archivo.", "danger")
                        '    Case "5"
                        '        FnMensaje(rpta + "Verificar Archivo, Hoja de Excel [BASE GENERAL] no cuenta con la Estructura Correcta.", "danger")
                        '    Case Else
                        '        FnMensaje(rpta + " - " + ex.Message.ToString, "danger")
                        'End Select
                        FnMensaje(ex.Message.ToString, "danger")
                    End Try
                Else
                    FnMensaje("Solo se aceptan Archivos de Excel (.xls ó .xlsx)", "danger")
                End If
            End If
        End If
    End Sub

    Private Sub FnMensaje(ByVal mensaje As String, ByVal tipo As String)
        Me.Mensaje.InnerText = mensaje
        Me.Mensaje.Attributes.Add("class", "alert alert-" + tipo + "")
    End Sub

    Private Function Validar() As Boolean
        Me.Mensaje.InnerText = ""
        Me.Mensaje.Attributes.Remove("class")
        If Me.ddlTipoEstudio.SelectedValue = "" Then
            FnMensaje("Seleccione un Tipo de Estudio", "danger")
            Return False
        End If
        If Me.ddlConvocatoria.SelectedValue = "" Then
            FnMensaje("Seleccione una Convocatoria", "danger")

            Return False
        End If
        If Me.ddlEvento.SelectedValue = "" Then
            FnMensaje("Seleccione un Evento", "danger")
            Return False
        End If
        If Me.ArchivoASubir.HasFile = False Then
            FnMensaje("Adjuntar Archivo a Importar", "danger")
            Return False
        End If

        'If System.IO.Path.GetExtension(Me.ArchivoASubir.FileName) <> ".xls" And System.IO.Path.GetExtension(Me.ArchivoASubir.FileName) <> ".xlsx" Then
        If System.IO.Path.GetExtension(Me.ArchivoASubir.FileName) <> ".csv" Then
            FnMensaje("Solo se aceptan Archivos de formato de la plantilla (.csv)", "danger")
            Return False
        End If

        Return True
    End Function

    Sub SubirArchivo(ByVal id_tabla As Integer, ByVal nro_transaccion As String, ByVal ArchivoaSubir As HttpPostedFile, ByVal tipo As String, ByVal usuario_per As Integer)
        Try
            Dim codigo_tablaArchivo As String = id_tabla ' ID de tablaArchivo
            Dim archivo As HttpPostedFile = ArchivoaSubir
            Dim nro_operacion As String = ""
            Dim id_tablaProviene As String = nro_transaccion

            Dim Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim Usuario As String = Session("perlogin").ToString
            Dim Input(archivo.ContentLength) As Byte

            Dim b As New BinaryReader(archivo.InputStream)
            Dim binData As Byte() = b.ReadBytes(archivo.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)

            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)

            Dim nombre_archivo As String = System.IO.Path.GetFileName(archivo.FileName.Replace("&", "_").Replace("'", "_").Replace("*", "_"))

            list.Add("Fecha", Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(archivo.FileName))
            list.Add("Nombre", nombre_archivo)
            list.Add("TransaccionId", id_tablaProviene)
            list.Add("TablaId", codigo_tablaArchivo)
            list.Add("NroOperacion", nro_operacion)
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "https://usat.edu.pe/UploadFile", Session("perlogin").ToString)

            'ACTUALIZAR ID GENERADO EN SHARED FILES
            'Dim obj As New ClsGestionInvestigacion
            'Dim dt As New Data.DataTable
            'dt = obj.ActualizarArchivosTesis(codigo_tablaArchivo, codigo_tes, NroRend, abrev_etapa, usuario_per)
            '
            'Response.Write(result)
        Catch ex As Exception
            Dim Data1 As New Dictionary(Of String, Object)()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim list As New List(Of Dictionary(Of String, Object))()
            Data1.Add("rpta", "1 - SUBIR ARCHIVO")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

End Class

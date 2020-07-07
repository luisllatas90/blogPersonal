
Partial Class librerianet_aulavirtual_recuperartareasborradas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ruta As String = "T:\documentos aula virtual\archivoscv\"
        Dim tbl As Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString
        obj.AbrirConexion()
        tbl = obj.TraerDataTable("REC_ConsultarTareasDeCursos")
        obj.CerrarConexion()


        'Recorrer Cursos Virtuales con TAREAS y compartarlo con el directorio
        Dim carpeta, sArchivos() As String
        Dim archivo As String
        Dim archivoInfo As System.IO.FileInfo
        obj.AbrirConexion()

        For i As Integer = 0 To tbl.Rows.Count - 1
            carpeta = tbl.Rows(i).Item("idcursovirtual") & "\tareas\"
            Dim tar As New System.IO.DirectoryInfo(ruta & carpeta)
            If tar.Exists = True Then
                sArchivos = System.IO.Directory.GetFiles(ruta & carpeta)
                'Response.Write(ruta & carpeta "<br />")
                For Each archivo In sArchivos
                    archivoInfo = New System.IO.FileInfo(archivo)
                    Response.Write(archivoInfo.FullName & "<br />")
                    obj.Ejecutar("REC_AgregarTareasRecuperadas", archivoInfo.CreationTime.ToString, Replace(carpeta, "\", "/") & archivoInfo.Name, archivoInfo.Name, archivoInfo.Extension,tbl.Rows(i).Item("idcursovirtual"))
                Next
            End If
        Next
        'actualizar autores
        'obj.Ejecutar("REC_ActualizarAutores")

        obj.CerrarConexion()

        tbl.Dispose()
        tbl = Nothing
        obj = Nothing
        Response.Write("<h1>Terminado</h1>")
    End Sub
End Class

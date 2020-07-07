Imports System.IO

Partial Class indicadores_FrmSubirArchivos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            CargarComboPlan()
            CargaFacultad()
        End If
    End Sub

    Private Sub CargarComboPlan()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListaPlanes(Me.Request.QueryString("ctf"), Request.QueryString("id"))

            If dts.Rows.Count > 0 Then
                cboPlan.DataSource = dts
                cboPlan.DataTextField = "Descripcion"
                cboPlan.DataValueField = "Codigo"
                cboPlan.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarCentroCostosPlan(ByVal vcodigo_pla As Integer)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable


            If vcodigo_pla > 0 Then
                dts = obj.ListaCecosPlan(vcodigo_pla)
                If dts.Rows.Count > 0 Then
                    cboFacultad.DataSource = dts
                    cboFacultad.DataTextField = "Descripcion"
                    cboFacultad.DataValueField = "Codigo"
                    cboFacultad.DataBind()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaFacultad()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("IND_ListaFacultades")
            obj.CerrarConexion()

            If (dt.Rows.Count > 0) Then
                cboFacultad.DataSource = dt
                cboFacultad.DataTextField = "nombre_Fac"
                cboFacultad.DataValueField = "codigo_Cco"
                cboFacultad.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnSubirArchivo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubirArchivo.Click
        Dim obj As New ClsConectarDatos

        If (FileUpload1.HasFile) Then
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim sExt As String = ""
            Dim sName As String = ""
            Dim sTitle As String = ""
            Dim nroVeces As Integer = 0
            sName = FileUpload1.FileName
            sExt = Path.GetExtension(sName)
            If ValidaExtension(sExt) Then
                While nroVeces < 3
                    sTitle = generaNombreArchivo()
                    'Que repita máximo 3 veces en caso exista el archivo
                    If (ExisteArchivo(generaNombreArchivo()) = True) Then
                        nroVeces = nroVeces + 1
                    Else
                        nroVeces = 4
                    End If
                End While

                FileUpload1.SaveAs(MapPath("archivos/" & sTitle & sExt))

                obj.AbrirConexion()
                obj.Ejecutar("IND_ArchivoPlan", Me.cboPlan.SelectedValue, Me.txtTitulo.Text, Me.txtDescripcion.Text, sTitle & sExt, Me.cboFacultad.SelectedValue)
                obj.CerrarConexion()

                Me.lblExito.Text = "Se guardó correctamente."
                Me.lblAviso.Text = ""

                LimpiarControles()
            Else
                Me.lblExito.Text = ""
                Me.lblAviso.Text = "La extensión del archivo no está permitida."
            End If
        End If
    End Sub

    Private Function ValidaExtension(ByVal sExtension As String) As Boolean
        Select Case sExtension
            Case ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".doc", ".docx", ".xls", ".xlsx", ".pdf", ".zip", ".rar", ".ppt", ".pptx"
                Return True
            Case Else
                Return False
        End Select
    End Function

    Public Function ExisteArchivo(ByVal strNombre As String) As Boolean
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("IND_VerificaArchivo", strNombre)
            obj.CerrarConexion()

            If (dt.Rows.Count > 0) Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Me.lblAviso.Text = "El archivo ya existe"
            Return False
        End Try
    End Function

    Public Function generaNombreArchivo() As String
        Try
            Dim rnd As New Random
            Dim ubicacion As Integer
            Dim strNumeros As String = "0123456789"
            Dim strLetraMin As String = "abcdefghijklmnopqrstuvwxyz"
            Dim strLetraMay As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            Dim Token As String = ""
            Dim strCadena As String = ""
            strCadena = strLetraMin & strNumeros & strLetraMay
            While Token.Length < 10
                ubicacion = rnd.Next(0, strCadena.Length)
                If (ubicacion = 62) Then
                    Token = Token & strCadena.Substring(ubicacion - 1, 1)
                End If
                If (ubicacion < 62) Then
                    Token = Token & strCadena.Substring(ubicacion, 1)
                End If
            End While

            Return Token
        Catch ex As Exception
            Return "-1"
        End Try
    End Function

    Private Sub LimpiarControles()
        Me.txtDescripcion.Text = ""
        Me.txtTitulo.Text = ""
        Me.FileUpload1.Dispose()
        Me.lblExito.Text = ""
        Me.lblAviso.Text = ""
    End Sub

    Private Sub CargaArchivos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Try
            If (Me.cboPlan.Items.Count > 0) Then                
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dt = obj.TraerDataTable("IND_ListaArchivosPlan", Me.cboPlan.SelectedValue, Me.cboFacultad.SelectedValue)
                obj.CerrarConexion()

                Me.gvArchivos.DataSource = dt
                Me.gvArchivos.DataBind()
            End If
        Catch ex As Exception
            Me.lblAviso.Text = "Error al cargar los archivos"
        End Try
    End Sub

    Protected Sub gvArchivos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvArchivos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then                        
            'Muestra al usuario si la variable tiene registrados valores en los diferentes periodos.
            If e.Row.Cells(3).Text <> "" Then
                e.Row.Cells(3).Text = "<center><a href='archivos/" & e.Row.Cells(3).Text & "'><img src='../images/desc.png' style='border: 0px' alt='Descargar archivo'/></a></center>"                
            Else
                e.Row.Cells(3).Text = "-"
            End If

            e.Row.Cells(4).Text = "<center><img src='../images/eliminar.gif' style='border: 0px' alt='Eliminar archivo'/></center>"
        End If
    End Sub

    Protected Sub gvArchivos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvArchivos.RowDeleting
        Dim obj As New ClsConectarDatos
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("IND_EliminaArchivoPlan", Me.gvArchivos.DataKeys(e.RowIndex).Value.ToString())
            obj.CerrarConexion()

            'Refrescar campos            
            CargaArchivos()
            LimpiarControles()
        Catch ex As Exception
            Me.lblAviso.Text = "Error al eliminar el archivo"
        End Try

    End Sub

    Protected Sub cboFacultad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFacultad.SelectedIndexChanged
        CargaArchivos()
    End Sub

    Protected Sub cboPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPlan.SelectedIndexChanged
        CargaArchivos()
    End Sub
End Class

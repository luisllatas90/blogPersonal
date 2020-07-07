Imports System.Configuration
Imports System.Diagnostics

Partial Class propuestas_adjuntar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.CmdSalir.Attributes.Add("onclick", "javascript:window.close(); return false;")
			Me.FileArchivo.Attributes.Add("OnChange", "ValidarEnvio()")
            Dim codigo_prp, codigo_dap, codigo_dip, codigo_cop, modifica As String
            codigo_prp = Request.QueryString("codigo_prp")
            codigo_dap = Request.QueryString("codigo_dap")
            codigo_dip = Request.QueryString("codigo_dip")
            codigo_cop = Request.QueryString("codigo_cop")
            modifica = Request.QueryString("modifica")
            If codigo_dap = "" Then : codigo_dap = "0" : End If
            If codigo_dip = "" Then : codigo_dip = "0" : End If
            Call ListarArchivos(codigo_cop, codigo_dap, codigo_dip)
        End If
        
    End Sub

    Protected Sub CmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAgregar.Click
        Dim strruta As String
        Dim nombrearchivo1 As String
        Dim codigo_prp, codigo_dap, codigo_dip, codigo_cop, modifica As String

        codigo_prp = Request.QueryString("codigo_prp")
        codigo_dap = Request.QueryString("codigo_dap")
        codigo_dip = Request.QueryString("codigo_dip")
        codigo_cop = Request.QueryString("codigo_cop")
        modifica = Request.QueryString("modifica")

        If codigo_dip = "" Then : codigo_dip = "0" : End If
        If codigo_dap = "" Then : codigo_dap = "0" : End If

        strruta = Server.MapPath("../../filespropuestas/")
        Dim Carpeta As New System.IO.DirectoryInfo(strruta & "\" & codigo_prp.ToString)
        If Carpeta.Exists = False Then
            Carpeta.Create()
        End If

        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        If Me.FileArchivo.HasFile Then
            Try
                nombrearchivo1 = codigo_prp.ToString & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & System.IO.Path.GetExtension(Me.FileArchivo.FileName).ToString
                Me.FileArchivo.PostedFile.SaveAs(strruta & "\" & codigo_prp.ToString & "\" & nombrearchivo1)

                Obj.IniciarTransaccion()
                If codigo_dap = "0" And codigo_dip = "0" Then
                    codigo_dap = "C" & codigo_cop
                    Obj.Ejecutar("RegistraArchivoPropuesta", codigo_dap, nombrearchivo1, Me.TxtNombre.Text)
                    codigo_dap = "0"
                    codigo_dip = "0"
                End If

                If codigo_dap <> "0" Then
                    Obj.Ejecutar("RegistraArchivoPropuesta", codigo_dap, nombrearchivo1, Me.TxtNombre.Text)
                End If

                If codigo_dip <> "0" Then
                    Obj.Ejecutar("RegistraArchivoPropuestaInforme", codigo_dip, nombrearchivo1, Me.TxtNombre.Text)
                End If
                Obj.TerminarTransaccion()

                Call ListarArchivos(codigo_cop, codigo_dap, codigo_dip)
                Me.TxtNombre.Text = ""

                Me.LblMensaje.ForeColor = Drawing.Color.Blue
                Me.LblMensaje.Text = "Se agregó el archivo satisfactoriamente."

            Catch ex As Exception
                Me.LblMensaje.ForeColor = Drawing.Color.Red
                Me.LblMensaje.Text = "Ocurrió un error al procesar el archivo."
            End Try

        End If

    End Sub

    Private Sub ListarArchivos(ByVal codigo_cop As String, ByVal codigo_dap As String, ByVal codigo_dip As String)
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Datos As Data.DataTable

        If codigo_cop <> "" Then
            Datos = Obj.TraerDataTable("ConsultarArchivosPropuesta", "TO", codigo_cop)
        Else
            If codigo_dap <> "0" Then
                Datos = Obj.TraerDataTable("ConsultarArchivosPropuesta", "CP", codigo_dap)
            Else
                Datos = Obj.TraerDataTable("ConsultarArchivosPropuesta", "CI", codigo_dip)
            End If
        End If
        Me.GridView1.DataSource = Datos
        Me.GridView1.DataBind()

        Obj = Nothing
        Datos.Dispose()
        Datos = Nothing
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim ext As String
            fila = e.Row.DataItem 'Los datos devueltos (cuando quiero saber la data que llega)
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "ponervalortext('" & fila.Row("nombre_apr").ToString & "','" & fila.Row("codigo_apr").ToString & "')")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            e.Row.Attributes.Add("id", "fila" & fila.Row("codigo_apr").ToString & "")

            ext = Right(fila.Row(2).ToString, 3)
            e.Row.Cells(2).Text = "<img src='../../images/ext/" & ext & ".gif'>"
        End If

    End Sub

    Protected Sub CmdQuitar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdQuitar.Click
        Dim Ruta As String
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            Ruta = Server.MapPath("../../filespropuestas/") & Request.QueryString("codigo_prp") & "\" & Me.txtnombrearchivo.Value
            Kill(Ruta)
            obj.IniciarTransaccion()
            obj.Ejecutar("EliminarArchivoPropuesta", Me.txtnombrearchivo.Value)
            obj.TerminarTransaccion()

            Dim codigo_prp, codigo_dap, codigo_dip, codigo_cop As String
            codigo_prp = Request.QueryString("codigo_prp")
            codigo_dap = Request.QueryString("codigo_dap")
            codigo_dip = Request.QueryString("codigo_dip")
            codigo_cop = Request.QueryString("codigo_cop")
            Call ListarArchivos(codigo_cop, codigo_dap, codigo_dip)
            Me.LblMensaje.ForeColor = Drawing.Color.Blue
            Me.LblMensaje.Text = "Se eliminó el archivo correctamente."
        Catch ex As Exception
            obj.AbortarTransaccion()
            Me.LblMensaje.ForeColor = Drawing.Color.Red
            Me.LblMensaje.Text = "Ocurrió un error al procesar el archivo."
        End Try
        

    End Sub
End Class

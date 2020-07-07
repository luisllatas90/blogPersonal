Imports System.IO
Imports System.Data

Partial Class academico_matricula_foro_frmIncidenciaDA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            If Request.QueryString("mod") Is Nothing Then
                CargaEscuelas(Session("id_per"), 2)
            Else
                CargaEscuelas(Session("id_per"), Request.QueryString("mod"))
            End If

            RetornaCicloActual()

            Me.btnCerrar.Attributes.Add("onClick", "javascript:CerrarResponder();")

            If Me.cboEscuelas.Items.Count > 0 Then
                CargaIncidencias()
            End If
        End If
    End Sub

    Private Sub RetornaCicloActual()
        Dim obj As New ClsConectarDatos
        Dim dt As New DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            'If Request.QueryString("mod") = 2 Then
            '    dt = obj.TraerDataTable("EVE_CicloAcademicoActual")
            'Else
            '    dt = obj.TraerDataTable("EVE_CicloAcademicoActualGO")
            'End If

            dt = obj.TraerDataTable("ConsultarCicloAcademico", "AU3", Request.QueryString("mod").ToString)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                Me.HdCiclo.Value = dt.Rows(0).Item("codigo_Cac")
            End If
        Catch ex As Exception
            Me.lblError.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub CargaEscuelas(ByVal codigo_per As Integer, ByVal codigo_test As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("MAT_AccesoPersonalxEscuelas", "T", codigo_per, codigo_test)
            obj.CerrarConexion()

            Me.cboEscuelas.DataSource = dt
            Me.cboEscuelas.DataValueField = "codigo_cpf"
            Me.cboEscuelas.DataTextField = "nombre_cpf"
            Me.cboEscuelas.DataBind()

            If Me.cboEscuelas.Items.Count > 0 Then
                Me.cboEscuelas.SelectedValue = 0
            End If
        Catch ex As Exception
            Me.lblError.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub CargaIncidencias()
        Dim obj As New ClsConectarDatos
        Dim dt As New DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            If Me.cboInstancia.SelectedValue = "T" Then
                dt = obj.TraerDataTable("MAT_ListaIncidenteForoDA", 0, 0, 0, Me.cboEscuelas.SelectedValue, Me.HdCiclo.Value, "P", "D", Me.cboInstancia.SelectedValue, 0)            
            Else
                dt = obj.TraerDataTable("MAT_ListaIncidenteForoDA", 0, 0, 0, Me.cboEscuelas.SelectedValue, Me.HdCiclo.Value, "P", "D", Me.cboInstancia.SelectedValue, Session("id_per"))
            End If

            obj.CerrarConexion()
            Me.gvListaIncidente.DataSource = dt
            Me.gvListaIncidente.DataBind()
        Catch ex As Exception
            Me.lblError.Text = "Error: " & ex.Message
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        CargaIncidencias()
    End Sub

    Protected Sub gvListaIncidente_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaIncidente.RowDataBound
        e.Row.Cells(0).Visible = False        
    End Sub

    Protected Sub gvListaIncidente_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvListaIncidente.RowDeleting
        Dim obj As New ClsConectarDatos
        Dim codIncidencia As Integer
        Try
            'Derivamos la incidencia a Dirección académica
            codIncidencia = Me.gvListaIncidente.DataKeys(e.RowIndex).Values(0)
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("MAT_IncidenteAccionIncidencia", codIncidencia, Session("id_per"), "S", "P")
            obj.CerrarConexion()

            CargaIncidencias()
        Catch ex As Exception
            Me.lblError.Text = "Error: " & ex.Message
        End Try
    End Sub

    Protected Sub gvListaIncidente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvListaIncidente.SelectedIndexChanged        
        Me.lblError.Text = ""
        Dim strScript As String = ""
        strScript = "<script type='text/javascript' language='javascript'>"
        strScript = strScript & "CerrarResponder(); muestraform();"
        strScript = strScript & "</script>"
        Me.HdInstancia.Value = "D"
        Me.HdIncidente.Value = Me.gvListaIncidente.Rows(Me.gvListaIncidente.SelectedIndex).Cells(0).Text
        If (Me.gvListaIncidente.Rows(Me.gvListaIncidente.SelectedIndex).Cells(10).Text = "") Then
            Me.HdIncidentePadre.Value = "0"
        Else
            Me.HdIncidentePadre.Value = Me.gvListaIncidente.Rows(Me.gvListaIncidente.SelectedIndex).Cells(10).Text
        End If



        LimpiarDatos()
        CargaDatos()
        CargaIncidenciaAlumno()
        ClientScript.RegisterStartupScript(sender.GetType, "MuestraForm", strScript)
    End Sub

    Protected Sub btnResponder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResponder.Click
        Dim obj As New ClsConectarDatos
        Dim sName As String = "", sExt As String = "", sTitle As String = "", rutaAdjunto As String = ""
        Dim strScript As String = ""
        strScript = "<script type='text/javascript' language='javascript'>"
        strScript = strScript & "CerrarResponder();"
        strScript = strScript & "</script>"
        'rutaAdjunto = "../../../../campusvirtualestudiante/campusvirtualestudiante/filesIncidentes/"
        rutaAdjunto = "//intranet.usat.edu.pe/campusestudiante/filesIncidentes/"
        Try
            If (Me.txtResponder.Text.Trim = "") Then
                Me.lblError.Text = "Debe ingresar una respuesta"
                Exit Sub
            End If

            If FileUpload1.HasFile Then
                sName = FileUpload1.FileName
                sExt = Path.GetExtension(sName)
                sTitle = generaNombreArchivo()                
                'FileUpload1.SaveAs(MapPath(rutaAdjunto & sTitle & sExt))
                FileUpload1.SaveAs(Server.MapPath(rutaAdjunto & EncrytedString64(Me.HdIncidentePadre.Value) & "-" & sTitle & sExt))
            End If

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("MAT_RespondeIncidentePersonal", Me.HdIncidente.Value, Session("id_per"), _
                         Me.HdInstancia.Value, Me.txtResponder.Text, sTitle & sExt)
            obj.CerrarConexion()
            LimpiarDatos()
            CargaIncidencias()
            Me.lblError.Text = ""
            ClientScript.RegisterStartupScript(sender.GetType, "OcultaResponder", strScript)
        Catch ex As Exception
            Me.lblError.Text = "Error: " & ex.Message            
        End Try
    End Sub

    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.lblError.Text = ""
    End Sub

    Private Sub CargaDatos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim rutaAdjunto As String = ""
        Dim Cod As String
        'rutaAdjunto = "http://serverdev/campusestudiante/filesIncidentes/"
        rutaAdjunto = "//intranet.usat.edu.pe/campusestudiante/filesIncidentes/"
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("MAT_ListaIncidenteForoDA", Me.HdIncidente.Value, 0, 0, 0, 0, "%", "%", "T", 0)
            obj.CerrarConexion()

            If (dt.Rows.Count > 0) Then
                Me.lblIncidencia.Text = FormatoID(dt.Rows(0).Item("numero"))
                Me.HdIncidentePadre.Value = dt.Rows(0).Item("codigo_incidencia_ref")
                Me.lblRespuestaEscuela.Text = dt.Rows(0).Item("Mensaje")
                Me.lblAtendidoPor.Text = dt.Rows(0).Item("Personal")
                If (dt.Rows(0).Item("adjunto").ToString.Trim = "") Then
                    Me.lblAdjuntoEscuela.Enabled = False
                    Me.lblAdjuntoEscuela.Text = "(Sin adjunto)"
                Else
                    Me.lblAdjuntoEscuela.Enabled = True
                    Me.lblAdjuntoEscuela.Text = "Ver adjunto"
                    Cod = EncrytedString64(dt.Rows(0).Item("codigo_incidencia_ref").ToString)
                    'Me.lblAdjuntoEscuela.NavigateUrl = "archivos/" & dt.Rows(0).Item("adjunto")
                    Me.lblAdjuntoEscuela.NavigateUrl = rutaAdjunto & Cod & "-" & dt.Rows(0).Item("adjunto")
                End If
                Me.lblPlanEstudio.Text = dt.Rows(0).Item("descripcion_Pes")
            End If
        Catch ex As Exception
            Me.lblError.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub CargaIncidenciaAlumno()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim strRuta As String
        Dim Cod As String
        Try
            'strRuta = "http://serverdev/campusvirtual/CampusVirtualEstudiante/CampusVirtualEstudiante/files/"
            strRuta = "//intranet.usat.edu.pe/campusestudiante/filesIncidentes/"
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("MAT_ListaIncidenteForo", Me.HdIncidentePadre.Value, 0, 0, 0, 0, "%", "%")
            obj.CerrarConexion()

            If (dt.Rows.Count > 0) Then
                Me.lblIncidenciaPadre.Text = FormatoID(dt.Rows(0).Item("numero"))
                Me.lblEscuela.Text = dt.Rows(0).Item("nombre_Cpf")
                Me.lblCodUniv.Text = dt.Rows(0).Item("codigoUniver_Alu")
                Me.lblAlumno.Text = dt.Rows(0).Item("Estudiante")
                Me.lblPlanEstudio.Text = dt.Rows(0).Item("descripcion_Pes")
                Me.lblAsunto.Text = dt.Rows(0).Item("asunto")
                Me.lblMensaje.Text = dt.Rows(0).Item("Mensaje")
                Me.lblCorreo.Text = dt.Rows(0).Item("email")
                Me.lblTelefono.Text = dt.Rows(0).Item("telefonos")
                If (dt.Rows(0).Item("adjunto").ToString.Trim = "") Then
                    Me.lblAdjunto.Enabled = False
                    Me.lblAdjunto.Text = "(Sin adjunto)"
                Else
                    Me.lblAdjunto.Enabled = True
                    Me.lblAdjuntoEscuela.Text = "Ver adjunto"
                    Cod = EncrytedString64(dt.Rows(0).Item("codigo_incidencia").ToString)
                    Me.lblAdjunto.NavigateUrl = strRuta & Cod & "-" & dt.Rows(0).Item("adjunto")
                End If

                'Cargar la Foto
                Dim ruta As String
                Dim obEnc As Object
                obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")
                ruta = obEnc.CodificaWeb("069" & Me.lblCodUniv.Text)
                ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
                Me.FotoAlumno.ImageUrl = ruta
                obEnc = Nothing
            End If
        Catch ex As Exception
            Me.lblError.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Function FormatoID(ByVal valor As String) As String
        Dim strFormato As String = ""
        strFormato = valor
        While strFormato.Length <= 6
            strFormato = "0" & strFormato
        End While

        Return strFormato
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

    Protected Sub btnDerivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDerivar.Click        
        Dim obj As New ClsConectarDatos
        Dim sName As String = "", sExt As String = "", sTitle As String = ""
        'Dim rutaAdjunto As String = "../../../../campusvirtualestudiante/campusvirtualestudiante/filesIncidentes/"
        Dim rutaAdjunto As String = "//intranet.usat.edu.pe/campusestudiante/filesIncidentes/"
        Try
            If (Me.txtResponder.Text.Trim = "") Then
                Me.lblError.Text = "Debe ingresar una respuesta"
                Exit Sub
            End If

            If FileUpload1.HasFile Then
                sName = FileUpload1.FileName
                sExt = Path.GetExtension(sName)
                sTitle = generaNombreArchivo()
                'FileUpload1.SaveAs(MapPath("archivos/" & sTitle & sExt))
                FileUpload1.SaveAs(Server.MapPath(rutaAdjunto & EncrytedString64(Me.HdIncidentePadre.Value) & "-" & sTitle & sExt))
            End If

            'Derivamos la incidencia a Dirección académica
            If Not Me.txtResponder.Text.Trim = "" Then
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                obj.Ejecutar("MAT_IncidenteAccionIncidencia", Me.HdIncidente.Value, Session("id_per"), "S", "P", Me.txtResponder.Text, sTitle & sExt)
                obj.CerrarConexion()
                LimpiarDatos()
                CargaIncidencias()
            Else
                Me.txtResponder.Focus()
            End If
            Me.lblError.Text = ""
        Catch ex As Exception
            Me.lblError.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub LimpiarDatos()       
        'Alumno
        Me.lblIncidenciaPadre.Text = ""
        Me.lblEscuela.Text = ""
        Me.lblCodUniv.Text = ""
        Me.lblAlumno.Text = ""
        Me.lblAsunto.Text = ""
        Me.lblMensaje.Text = ""
        Me.lblAdjunto.Enabled = False
        Me.lblAdjunto.Text = "(Sin adjunto)"
        'Escuela
        Me.lblIncidencia.Text = ""
        Me.lblRespuestaEscuela.Text = ""
        Me.lblAtendidoPor.Text = ""
        Me.lblAdjuntoEscuela.Enabled = False
        Me.lblAdjuntoEscuela.Text = "(Sin adjunto)"
        Me.txtResponder.Text = ""
    End Sub

    Protected Sub btnRevisarApropiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRevisarApropiar.Click
        Dim obj As New ClsConectarDatos
        Dim codIncidencia As Integer
        Try
            'Apropiamos la incidencia 
            codIncidencia = Session("CodInc")
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("MAT_ApropiarIncidenteDA", Me.HdIncidente.Value, Session("id_per"))
            obj.CerrarConexion()

            LimpiarDatos()
            CargaIncidencias()                     
        Catch ex As Exception
            Me.lblError.Text = "Error: " & ex.Message
        End Try
    End Sub

    Public Function EncrytedString64(ByVal _stringToEncrypt As String) As String
        Dim result As String = ""
        Dim encryted As Byte()
        encryted = System.Text.Encoding.Unicode.GetBytes(_stringToEncrypt)
        result = Convert.ToBase64String(encryted)
        Return result
    End Function
End Class

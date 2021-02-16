Imports System.Data
Imports System.IO
Partial Class academico_matricula_foro_frmListaMensajesFC
    Inherits System.Web.UI.Page
    Public rutaFile As String = "", rutaAdjunto As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'rutaFile = "http://serverdev/campusvirtual/CampusVirtualEstudiante/CampusVirtualEstudiante/files/" -- serverdev
        rutaFile = "//intranet.usat.edu.pe/campusestudiante/filesIncidentes/"

        'rutaAdjunto = "../../../../campusvirtualestudiante/campusvirtualestudiante/filesIncidentes/"
        rutaAdjunto = "//intranet.usat.edu.pe/campusestudiante/filesIncidentes/"   'serverreal
        id_per.value = Session("id_per")

        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            If Request.QueryString("mod") Is Nothing Then
                CargaEscuelas(id_per.Value, 2)
            Else
                CargaEscuelas(id_per.Value, Request.QueryString("mod"))
            End If

            CodInc.Value = 0
            RetornaCicloActual()
            Me.txtHasta.Value = Date.Now.Date
            If Me.cboEscuelas.Items.Count >= 1 Then
                CargaIncidencias(Me.cboEscuelas.SelectedValue)
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
            Response.Write(ex.Message & "--->" & ex.source)
        End Try
    End Sub

    Private Sub CargaEscuelas(ByVal codigo_per As Integer, ByVal codigo_test As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("MAT_AccesoPersonalxEscuelas", "", codigo_per, codigo_test)
            obj.CerrarConexion()

            Me.cboEscuelas.DataSource = dt
            Me.cboEscuelas.DataValueField = "codigo_cpf"
            Me.cboEscuelas.DataTextField = "nombre_cpf"
            Me.cboEscuelas.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & "--->" & ex.Source)
        End Try
    End Sub

    Private Sub CargaIncidencias(ByVal codigo_cpf As Integer, Optional ByVal codigo_in As Integer = 0)
        Dim obj As New ClsConectarDatos
        Dim dt As New DataTable
        Try
            If Me.txtHasta.value <> "" Then
                Me.txtHasta.value = CDate(Me.txtHasta.value)
            Else
                Me.txtHasta.value = Date.Now.Date
            End If
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            If Me.chkCodUniv.checked Then
                dt = obj.TraerDataTable("MAT_ListaIncidenteForo_Escuela", codigo_in, 0, 0, codigo_cpf, Me.HdCiclo.Value, "%", "%", 0, Me.txtHasta.value, Me.txtcoduniv.text)
            Else
                dt = obj.TraerDataTable("MAT_ListaIncidenteForo_Escuela", codigo_in, 0, 0, codigo_cpf, Me.HdCiclo.Value, Me.cboEstado.selectedvalue, Me.cboInstancia.selectedvalue, iif(Me.chkFecha.checked, 1, 0), Me.txtHasta.value, iif(Me.chkCodUniv.checked, Me.txtcoduniv.text, "%"))
            End If

            obj.CerrarConexion()

            Me.gvListaIncidente.DataSource = dt
            Me.gvListaIncidente.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message & "->" & ex.source)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        CargaIncidencias(Me.cboEscuelas.SelectedValue)
    End Sub



    Protected Sub gvListaIncidente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvListaIncidente.SelectedIndexChanged
        Try
            Dim row As GridViewRow = gvListaIncidente.SelectedRow

            'Session("CodInc") = Me.gvListaIncidente.DataKeys(Me.gvListaIncidente.SelectedIndex).Values(0)
            CodInc.Value = Me.gvListaIncidente.DataKeys(Me.gvListaIncidente.SelectedIndex).Values(0)
            'Session("CodIncRaiz") = Me.gvListaIncidente.DataKeys(Me.gvListaIncidente.SelectedIndex).Values(7)
            CodIncRaiz.Value = Me.gvListaIncidente.DataKeys(Me.gvListaIncidente.SelectedIndex).Values(7)

            Me.lblFecha.Text = row.Cells(4).Text
            Me.lblNro.Text = row.Cells(3).Text
            Me.lblCodUniv.Text = row.Cells(5).Text
            Me.lblEstudiante.Text = row.Cells(6).Text
            Me.lblEscuela.Text = Me.gvListaIncidente.DataKeys(Me.gvListaIncidente.SelectedIndex).Values(2)
            Me.lblPlanEstudio.Text = Me.gvListaIncidente.DataKeys(Me.gvListaIncidente.SelectedIndex).Values(11)
            Me.lblAsunto.Text = row.Cells(7).Text
            Me.txtMensaje.Text = Me.gvListaIncidente.DataKeys(Me.gvListaIncidente.SelectedIndex).Values(3)
            Me.lblEmail.Text = Me.gvListaIncidente.DataKeys(Me.gvListaIncidente.SelectedIndex).Values(8)
            Me.lblTelefono.Text = Me.gvListaIncidente.DataKeys(Me.gvListaIncidente.SelectedIndex).Values(9)
            'Me.lblEstado.text = Me.gvListaIncidente.DataKeys(Me.gvListaIncidente.SelectedIndex).Values(4)
            'Me.lblInstancia.text = Me.gvListaIncidente.DataKeys(Me.gvListaIncidente.SelectedIndex).Values(5)
            Me.LinkHistorial.NavigateUrl = "../../../../librerianet/academico/historial_personal.aspx?id=" & Me.gvListaIncidente.DataKeys(Me.gvListaIncidente.SelectedIndex).Values(10)
            '

            If Me.gvListaIncidente.DataKeys(Me.gvListaIncidente.SelectedIndex).Values(4) = "Resuelto" Or Me.gvListaIncidente.DataKeys(Me.gvListaIncidente.SelectedIndex).Values(5) = "Dirección Académica" Then
                Me.btnResponder.Enabled = False
                Me.btnRevisarApropiar.Enabled = False
                Me.btnDerivar.Enabled = False
                txtResponder.Enabled = False
                FileUpload1.Enabled = False
            Else
                Me.btnResponder.Enabled = True
                Me.btnRevisarApropiar.Enabled = True
                Me.btnDerivar.Enabled = True
                txtResponder.Enabled = True
                FileUpload1.Enabled = True
            End If

            'Cargar adjunto
            'If Me.gvListaIncidente.DataKeys(Me.gvListaIncidente.SelectedIndex).Values(6) <> "" Then
            Me.EnlaceAdjunto.Enabled = True
            Dim cod As String
            cod = EncrytedString64(CodIncRaiz.Value)
            Me.EnlaceAdjunto.NavigateUrl = rutaFile & cod & "-" & Me.gvListaIncidente.DataKeys(Me.gvListaIncidente.SelectedIndex).Values(6)
            'Else
            'Me.EnlaceAdjunto.enabled = False
            'Me.EnlaceAdjunto.text = "(Sin Adjunto)"
            'End If

            'Cargar la Foto
            Dim ruta As String
            Dim obEnc As Object
            obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")
            ruta = obEnc.CodificaWeb("069" & row.Cells(5).Text)
            ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
            Me.FotoAlumno.ImageUrl = ruta
            obEnc = Nothing
            Me.lblmensaje.Text = ""
            Me.txtResponder.Text = ""

            CargaIncidencias(0, CodInc.Value)

            Me.PanelLista.Enabled = False
            Me.PanelBusqueda.Enabled = False

            Me.PanelDetalle.Visible = True
            txtResponder.Focus()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub





    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResponder0.Click
        Me.panelLista.visible = True
        Me.panelDetalle.visible = False
        Me.panelLista.enabled = True
        Me.panelBusqueda.enabled = True
        CodInc.value = 0
        CargaIncidencias(Me.cboEscuelas.SelectedValue, CodInc.value)

    End Sub


    '    Me.btnResponder.Attributes.Add("OnClientClick", "muestraform(" & CInt(Session("CodInc")) & ");")
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

    Protected Sub btnResponder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResponder.Click
        Me.lblmensaje.text = ""
        Dim obj As New ClsConectarDatos
        Dim sName As String = "", sExt As String = "", sTitle As String = ""
        Dim cadAdjunto As String = ""
        Try
            If FileUpload1.HasFile Then
                sName = FileUpload1.FileName
                sExt = Path.GetExtension(sName)
                'sTitle = generaNombreArchivo()
                cadAdjunto = sName
                FileUpload1.SaveAs(Server.MapPath(rutaAdjunto & EncrytedString64(CodIncRaiz.value) & "-" & cadAdjunto))
            End If

            If Not Me.txtResponder.text.trim = "" Then
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                obj.Ejecutar("MAT_RespondeIncidentePersonal", CodInc.value, id_per.value, _
                             "E", Me.txtResponder.Text, cadAdjunto)
                obj.CerrarConexion()
                CargaIncidencias(Me.cboEscuelas.SelectedValue)
                Call btnRegresar_Click(sender, e)
            Else
                Me.lblmensaje.text = "Por favor, escriba una respuesta al estudiante."
                Me.txtResponder.focus()
            End If

        Catch ex As Exception
            Response.Write(ex.Message & "--->" & ex.source)
        End Try
    End Sub


    Protected Sub chkCodUniv_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCodUniv.CheckedChanged

        cboEstado.enabled = Not chkCodUniv.checked
        cboInstancia.enabled = Not chkCodUniv.checked
        chkFecha.enabled = Not chkCodUniv.checked
        If chkCodUniv.checked Then
            Me.txtcoduniv.focus()
        Else
            Me.txtcoduniv.text = ""
        End If

    End Sub





    Protected Sub btnDerivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDerivar.Click
        Me.lblmensaje.text = ""
        Dim obj As New ClsConectarDatos
        Dim codIncidencia As Integer
        Dim sName As String = "", sExt As String = "", sTitle As String = ""
        Dim cadAdjunto As String = ""
        Try
            If FileUpload1.HasFile Then
                sName = FileUpload1.FileName
                sExt = Path.GetExtension(sName)
                'sTitle = generaNombreArchivo()
                cadAdjunto = sName
                FileUpload1.SaveAs(Server.MapPath(rutaAdjunto & EncrytedString64(CodIncRaiz.value) & "-" & cadAdjunto))
            End If

            'Derivamos la incidencia a Dirección académica
            If Not Me.txtResponder.text.trim = "" Then
                codIncidencia = CodInc.value
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                obj.Ejecutar("MAT_IncidenteAccionIncidencia", codIncidencia, id_per.value, "D", "P", Me.txtResponder.Text, cadAdjunto)
                obj.CerrarConexion()
                CargaIncidencias(Me.cboEscuelas.SelectedValue)
                Call btnRegresar_Click(sender, e)
            Else
                Me.lblmensaje.text = "Por favor, escriba el motivo para derivar a Dirección Académica"
                Me.txtResponder.focus()
            End If

        Catch ex As Exception
            Response.Write(ex.Message & "--->" & ex.source)
        End Try
    End Sub

    Public Function EncrytedString64(ByVal _stringToEncrypt As String) As String
        Dim result As String = ""
        Dim encryted As Byte()
        encryted = System.Text.Encoding.Unicode.GetBytes(_stringToEncrypt)
        result = Convert.ToBase64String(encryted)
        Return result
    End Function

    Public Function DecrytedString64(ByVal _stringToDecrypt As String) As String
        Dim result As String = ""
        Dim decryted As Byte()
        decryted = Convert.FromBase64String(_stringToDecrypt)
        result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.Length)
        Return result
    End Function


    Protected Sub btnRevisarApropiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRevisarApropiar.Click

        Dim obj As New ClsConectarDatos
        Dim codIncidencia As Integer
        Try
            'Apropiamos la incidencia 
            codIncidencia = CodInc.value
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("MAT_IncidenteAccionIncidencia", codIncidencia, id_per.value, "E", "P", "", "")
            obj.CerrarConexion()

            CargaIncidencias(Me.cboEscuelas.SelectedValue)
            Call btnRegresar_Click(sender, e)
            ' End If
        Catch ex As Exception
            Response.Write(ex.Message & "--->" & ex.source)
        End Try
    End Sub


    Protected Sub gvListaIncidente_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaIncidente.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If (gvListaIncidente.DataKeys(e.Row.RowIndex).Values("incidencia_raiz").ToString <> gvListaIncidente.DataKeys(e.Row.RowIndex).Values("codigo_incidencia").ToString) Then
                e.Row.Cells(3).Text = "<img src='img\mensaje_respuesta.png' style='padding-left:7px;'/>"
            End If
        End If

    End Sub
End Class

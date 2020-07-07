
Partial Class Egresado_DetAlumnos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            If (Request.QueryString("ofe") <> Nothing) Then
                Me.HdCodigo_ofe.Value = Request.QueryString("ofe")
                BuscaCarrerasOferta(Me.HdCodigo_ofe.Value)
                
                If (Request.QueryString("cpf") <> Nothing) Then
                    CargaGridAlumnos("TO")
                End If
            End If

            Me.lblMensaje.Text = ""
        End If
    End Sub

    ''' <summary>
    ''' Busca las carreras relacionadas a la oferta
    ''' </summary>
    ''' <param name="codigo_Ofe"></param>
    ''' <remarks></remarks>
    Private Sub BuscaCarrerasOferta(ByVal codigo_Ofe As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim dtDetalle As New Data.DataTable
        obj.AbrirConexion()
        dtDetalle = obj.TraerDataTable("ALUMNI_DetalleOferta", Me.HdCodigo_ofe.Value)
        obj.CerrarConexion()

        For i As Integer = 0 To dtDetalle.Rows.Count - 2
            Me.HdListaCarreras.Value = Me.HdListaCarreras.Value & dtDetalle.Rows(i).Item("codigo_cpf").ToString & ","
        Next
        Me.HdListaCarreras.Value = Me.HdListaCarreras.Value & dtDetalle.Rows(dtDetalle.Rows.Count - 1).Item("codigo_cpf").ToString

        dtDetalle.Dispose()
        obj = Nothing
    End Sub

    Private Sub CargaGridAlumnos(ByVal Tipo As String)
        Try
            Dim dtAlumnos As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dtAlumnos = obj.TraerDataTable("ALUMNI_BuscaAlumnosxCarrera", Tipo, HdListaCarreras.Value, 0)
            obj.CerrarConexion()
            Me.gvwAlumnos.DataSource = dtAlumnos
            Me.gvwAlumnos.DataBind()
            dtAlumnos.Dispose()
            obj = Nothing
            Me.lblNumRegistros.Text = Me.gvwAlumnos.Rows.Count.ToString & " registos encontrados"
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try        
    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Dim Fila As GridViewRow
        Dim Enviar As New ClsMail
        Dim strMensaje As String = ""
        Dim i As Integer

        'Capturamos Datos de Oferta Laboral
        Dim dtDatos As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtDatos = obj.TraerDataTable("ALUMNI_RetornaOferta", Me.HdCodigo_ofe.Value)
        obj.CerrarConexion()

        'Enviar correo a director de Alumni
        Dim dtDirector As New Data.DataTable
        obj.AbrirConexion()
        dtDirector = obj.TraerDataTable("ALUMNI_RetornaDirectorCco", 875)
        obj.CerrarConexion()

        If (dtDatos.Rows.Count > 0) Then
            'Mensaje a Enviar            
            strMensaje = strMensaje & "<center>"
            strMensaje = strMensaje & "<table width='70%'>"
            strMensaje = strMensaje & "<tr>"
            strMensaje = strMensaje & "<td colspan='2' align='center'><b> OFERTA LABORAL </b></td>"
            strMensaje = strMensaje & "</tr>"
            strMensaje = strMensaje & "<tr>"
            strMensaje = strMensaje & "<td style='width:20%'><b>EMPRESA:</b></td>"
            strMensaje = strMensaje & "<td>" & dtDatos.Rows(0).Item("nombrePro").ToString.ToUpper & "</td>"
            strMensaje = strMensaje & "</tr>"
            strMensaje = strMensaje & "<tr>"
            strMensaje = strMensaje & "<td><b>TITULO:</b></td>"
            strMensaje = strMensaje & "<td>" & dtDatos.Rows(0).Item("titulo_ofe").ToString.ToUpper & "</td>"
            strMensaje = strMensaje & "</tr>"
            strMensaje = strMensaje & "<tr>"
            strMensaje = strMensaje & "<td><b>DESCRIPCION:</b></td>"
            strMensaje = strMensaje & "<td>" & dtDatos.Rows(0).Item("descripcion_ofe").ToString.ToUpper & "</td>"
            strMensaje = strMensaje & "</tr>"
            strMensaje = strMensaje & "<tr>"
            strMensaje = strMensaje & "<td><b>REQUISITO:</b></td>"
            strMensaje = strMensaje & "<td>" & dtDatos.Rows(0).Item("requisitos_ofe").ToString.ToUpper & "</td>"
            strMensaje = strMensaje & "</tr>"
            strMensaje = strMensaje & "<tr>"
            strMensaje = strMensaje & "<td><b>LUGAR:</b></td>"
            strMensaje = strMensaje & "<td>" & dtDatos.Rows(0).Item("lugar_ofe").ToString.ToUpper & "</td>"
            strMensaje = strMensaje & "</tr>"
            strMensaje = strMensaje & "<tr>"
            strMensaje = strMensaje & "<td><b>DURACION:</b></td>"
            strMensaje = strMensaje & "<td>" & dtDatos.Rows(0).Item("duracion_ofe").ToString.ToUpper & "</td>"
            strMensaje = strMensaje & "</tr>"
            strMensaje = strMensaje & "<tr>"
            strMensaje = strMensaje & "<td><b>TIPO TRABAJO:</b></td>"
            strMensaje = strMensaje & "<td>" & dtDatos.Rows(0).Item("tipotrabajo_ofe").ToString.ToUpper & "</td>"
            strMensaje = strMensaje & "</tr>"
            strMensaje = strMensaje & "<tr>"
            strMensaje = strMensaje & "<td><b>CONTACTO:</b></td>"
            strMensaje = strMensaje & "<td>" & dtDatos.Rows(0).Item("contacto_ofe").ToString.ToUpper & "</td>"
            strMensaje = strMensaje & "</tr>"
            strMensaje = strMensaje & "<tr>"
            strMensaje = strMensaje & "<td><b>CORREO:</b></td>"
            strMensaje = strMensaje & "<td>" & dtDatos.Rows(0).Item("correocontacto_ofe") & "</td>"
            strMensaje = strMensaje & "</tr>"
            strMensaje = strMensaje & "<tr>"
            strMensaje = strMensaje & "<td><b>TELEFONO:</b></td>"
            strMensaje = strMensaje & "<td>" & dtDatos.Rows(0).Item("telefonocontacto_ofe").ToString.ToUpper & "</td>"
            strMensaje = strMensaje & "</tr>"
            strMensaje = strMensaje & "</table>"
            strMensaje = strMensaje & "</center>"

            For i = 0 To Me.gvwAlumnos.Rows.Count - 1
                'Capturamos las filas que estan activas
                Fila = Me.gvwAlumnos.Rows(i)
                Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                If (valor = True) Then
                    Dim strCabecera As String = ""
                    strCabecera = "Estimado " & Me.gvwAlumnos.Rows(i).Cells(2).Text & "<br />"
                    strCabecera = strCabecera & "La dirección de ALUMNI le hace llegar la siguiente oferta laboral, esperando sea de su interés <br /><br />"

                    Dim strFirma As String = ""
                    Dim strCorreoDirector As String = dtDirector.Rows(0).Item("usuario_per").ToString & "@usat.edu.pe"
                    strFirma = "<br /><br /> Atte. <br /><br />"
                    strFirma = strFirma & dtDirector.Rows(0).Item("Personal") & "<br />"
                    strFirma = strFirma & dtDirector.Rows(0).Item("usuario_per").ToString & "@usat.edu.pe <br />"
                    strFirma = strFirma & "Director de ALUMNI"

                    Enviar.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual USAT", Me.gvwAlumnos.Rows(i).Cells(5).Text.Trim, "Oferta Laboral", strCabecera & strMensaje & strFirma, True, "", strCorreoDirector)
                End If
            Next

            Me.lblMensaje.Text = "Mensaje(s) enviado(s)"
        Else
            Me.lblMensaje.Text = "No se encontraron ofertas relacionadas"
        End If

        dtDatos.Dispose()
        obj = Nothing
    End Sub

    Protected Sub gvwAlumnos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvwAlumnos.PageIndexChanging
        gvwAlumnos.PageIndex = e.NewPageIndex()        
        CargaGridAlumnos("TO")
    End Sub

    Protected Sub gvwAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvwAlumnos.RowDataBound        
        Dim Enc As New EncriptaCodigos.clsEncripta

        If (e.Row.RowIndex > -1) Then
            e.Row.Cells(7).Text = "<center><a href='frmFichaEgresado.aspx?pso=" & Enc.Codifica("069" & e.Row.Cells(1).Text) & "' target='_blank'><img src='../../images/previo.gif' style='border: 0px'/></a></center>"
            e.Row.Cells(8).Text = "<center><a href='../../librerianet/academico/historialcc.aspx?id=" & e.Row.Cells(9).Text & "' target='_blank'><img src='../../images/librohoja.gif' style='border: 0px'/></a></center>"
        End If
    End Sub

    Protected Sub gvwAlumnos_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvwAlumnos.DataBound
        gvwAlumnos.Columns(1).Visible = False
        gvwAlumnos.Columns(9).Visible = False
    End Sub

    Protected Sub btnSeleccionar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeleccionar.Click        
        CargaGridAlumnos(Me.cboEgresado.SelectedValue)
    End Sub
End Class

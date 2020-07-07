
Partial Class demo_listaaplicaciones2020
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private oeAccesoDatos As clsaccesodatos
    Private oeNotificacion As e_Notificacion, odNotifiacion As d_Notificacion

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If
            mt_CrearItems()
            mt_CrearNotificaciones()
            mt_CrearListaAplicacion()
            mt_CrearPublicidad()
            Session("gcodigo_ctfu") = ""
            Session("gcodigo_apl") = ""
            Session("gdescri_apl") = ""
            Session("gdescri_tfu") = ""
            Session("gestilo_apl") = ""
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CrearItems()
        Dim ul As literal
        Dim _dias, _noti As Integer
        Dim dt As New data.datatable
        Try
            ' Obtener las aplicaciones asignados al usuario
            oeAccesoDatos = New clsaccesodatos
            oeAccesoDatos.abrirconexion()
            Session("dtAcceso") = oeAccesoDatos.TraerDataTable("ConsultarAplicacionUsuario", "8", "P", Session("id_per"), "")
            oeAccesoDatos.cerrarconexion()
            oeAccesoDatos = Nothing
            dt = CType(Session("dtAcceso"), Data.DataTable)
            ' Inicializar variables
            _dias = dt.rows(0).item("dias")
            ' Obtebner las Notificaciones del usuario
            oeNotificacion = New e_Notificacion : odNotifiacion = New d_Notificacion
            With oeNotificacion
                .tipo_operacion = "GEN" : .codigo_per = Session("id_per")
            End With
            Session("dtNotificacion") = odNotifiacion.fc_ListarNotificaciones(oeNotificacion)
            dt = CType(Session("dtNotificacion"), Data.DataTable)
            _noti = dt.rows.count
            ' Generar los item de la cabecera del webmaster
            ul = New literal
            ul.text = "<ul class='nav navbar-nav navbar-right'>"
            ul.text += "    <li><a href='#'>Usuario: " & Session("nombreper") & " </a></li>"
            ul.text += "    <li><a href='../servicios/GuiaCambioPassword.pdf' > Su contraseña expira en <span class='label label-warning' style='font-size: 13px;'>" & _dias & " días</span></a></li>"
            ul.text += "    <li><a href='#' data-toggle='tooltip' data-placement='bottom' title='Tiene Notificaciones pendientes'>Notificaciones <span class='badge'>" & _noti & "</span></a></li>"
            ul.text += "    <li><a href='../cerrar.asp'>[Cerrar Sesión]</a></li>"
            ul.text += "</ul>"
            Me.myNavbar.controls.add(ul)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CrearNotificaciones()

        Dim dv As literal
        Dim dtNoti As New System.Data.DataTable

        dtNoti = CType(Session("dtNotificacion"), Data.DataTable)

        For x As Integer = 0 To dtNoti.Rows.Count - 1
            dv = New literal
            dv.text = "<div class='alert alert-success fade in'>"
            dv.text += "    <a href='#' class='close' data-dismiss='alert' aria-label='close'>×</a>"
            dv.text += "    <p><strong>" & dtNoti.Rows(x).Item("asunto_not") & "</strong></p>"
            dv.text += "    " & dtNoti.Rows(x).Item("descripcion_not") & ""
            dv.text += "</div>"
            Me.divAlert.controls.add(dv)
        Next

        'dv = New literal
        'dv.text = "<div class='alert alert-success fade in'>"
        'dv.text += "    <a href='#' class='close' data-dismiss='alert' aria-label='close'>×</a>"
        'dv.text += "    <p><strong>Notificación 1!</strong></p>"
        'dv.text += "    Este es un ejemplo para notificar. <a href='#' class='alert-link'>Click Aquí</a>."
        'dv.text += "</div>"
        'Me.divAlert.controls.add(dv)

        'dv = New literal
        'dv.text = "<div class='alert alert-info fade in'>"
        'dv.text += "    <a href='#' class='close' data-dismiss='alert' aria-label='close'>×</a>"
        'dv.text += "    <p><strong>Notificación 2!</strong></p>"
        'dv.text += "    Este es un ejemplo para notificar. <a href='#' class='alert-link'>Click Aquí</a>."
        'dv.text += "</div>"
        'Me.divAlert.controls.add(dv)

        'dv = New literal
        'dv.text = "<div class='alert alert-warning fade in'>"
        'dv.text += "    <a href='#' class='close' data-dismiss='alert' aria-label='close'>×</a>"
        'dv.text += "    <p><strong>Notificación 3!</strong></p>"
        'dv.text += "    Este es un ejemplo para notificar. <a href='#' class='alert-link'>Click Aquí</a>."
        'dv.text += "</div>"
        'Me.divAlert.controls.add(dv)

        'dv = New literal
        'dv.text = "<div class='alert alert-danger fade in'>"
        'dv.text += "    <a href='#' class='close' data-dismiss='alert' aria-label='close'>×</a>"
        'dv.text += "    <p><strong>Notificación 4!</strong></p>"
        'dv.text += "    Este es un ejemplo para notificar. <a href='#' class='alert-link'>Click Aquí</a>."
        'dv.text += "</div>"
        'Me.divAlert.controls.add(dv)

        ' Ads Servicios TI
        Dim _link As String = "top.location.href='../abriraplicacion2020.asp?codigo_tfu=1&amp;codigo_apl=61&amp;descripcion_apl=SERVICIOS TI&amp;estilo_apl=O'"
        dv = New literal
        dv.text = "<div class='well'>"
        dv.text += "    <p>"
        dv.text += "        <a onclick = """ & _link & """ target='contenido'> "
        dv.text += "            <img id='imgServiciosTI' border='1' src='../images/menus/serviciostimesa.png?X=1' />"
        dv.text += "        </a>"
        dv.text += "    </p>"
        dv.text += "</div>"
        Me.divAlert.controls.add(dv)

    End Sub

    Private Sub mt_CrearListaAplicacion()
        Dim dtAcceso As New data.datatable, dt As New data.datatable
        Dim dv As Data.DataView
        Try

            dtAcceso = CType(Session("dtAcceso"), Data.DataTable)

            dv = New Data.DataView(dtAcceso, "tipo_apl = 'AC' ", "", Data.DataViewRowState.CurrentRows)
            dt = dv.totable
            mt_CrearTipoAplicacion("AC", dt)

            dv = New Data.DataView(dtAcceso, "tipo_apl = 'AD' ", "", Data.DataViewRowState.CurrentRows)
            dt = dv.totable
            mt_CrearTipoAplicacion("AD", dt)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CrearPublicidad()
        Dim dv As literal
        Dim dtReglamento As New data.datatable
        Try
            oeAccesoDatos = New clsaccesodatos
            oeAccesoDatos.abrirconexion()
            dtReglamento = oeAccesoDatos.TraerDataTable("ConsultarAceptacionReglamento", "1", Session("id_per"))
            oeAccesoDatos.cerrarconexion()

            ' Ads Reglamento Interno
            If dtReglamento.rows.count > 0 Then
                If dtReglamento.rows(0).item("nro") = "0" Then
                    dv = New literal
                    dv.text = "<div class='well'>"
                    dv.text += "<div class='alert alert-danger fade in'>"
                    dv.text += "    <h4>"
                    dv.text += "        Reglamento Interno de Seguridad y Salud en el Trabajo(RISST) V.02 &nbsp;"
                    dv.text += "        <button onclick='ReglamentoSST()'>"
                    dv.text += "            <img src='../images/descargar_reglamento.png' height='30' width='30' />"
                    dv.text += "        </button>"
                    dv.text += "    </h4>"
                    dv.text += "    <input type='checkbox' id='chkTerminos' checked='checked' />"
                    dv.text += "    <label style='font-weight: bold;'>He leído y conozco las actualizaciones del reglamento</label>"
                    dv.text += "    <input type='button' style='align: right' value='Aceptar' onclick='AceptarReglamento(""" & Session("id_per") & """)' class='btn btn-primary' />"
                    dv.text += "</div>"
                    dv.text += "</div>"
                    Me.divAds.controls.add(dv)
                End If
            End If

            ' Ads Anuncios
            dv = New literal
            dv.text = "<div class='well' style='padding: 0px;'>"
            dv.text += "<iframe src='http://www.usat.edu.pe/anuncios/index2.php' width='100%' height='100%' style='position: relative; overflow: hidden; height: 480px;'></iframe>"
            dv.text += "</div>"
            Me.divAds.controls.add(dv)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CrearTipoAplicacion(ByVal tipo_apl As String, ByVal dt As data.datatable)
        Dim div As literal
        Dim _col As Integer = 3, _item As Integer = 0
        Dim _vinculo As String = ""
        Dim _flag As Boolean = False
        Dim dtTF As data.datatable
        For i As Integer = 0 To dt.rows.count - 1
            _item += 1
            _flag = False
            div = New literal
            If _item = 1 Then
                If i = 0 Then
                    div.text = "<h3>" & iif(tipo_apl = "AC", "Sistemas Académicos", "Sistemas Administrativos") & "</h3>"
                    div.text += "<div class='row'>"
                Else
                    div.text = "<div class='row'>"
                End If
            End If
            If dt.rows(i).item("codigo_apl") = "0" Then
                _vinculo = "top.location.href='" & dt.rows(i).item("enlace_apl") & "'"
            ElseIf dt.rows(i).item("total_tfu") = "1" Then
                '_vinculo = "top.location.href='../abriraplicacion2020.asp?codigo_tfu=" & dt.rows(i).item("codigo_tfu") & "&amp;codigo_apl=" & dt.rows(i).item("codigo_apl") & _
                '"&amp;descripcion_apl=" & dt.rows(i).item("descripcion_apl") & "&amp;estilo_apl=" & dt.rows(i).item("estilo_apl") & "'"
                _vinculo = "fc_AbrirFuncion('" & dt.rows(i).item("codigo_tfu") & "','" & dt.rows(i).item("codigo_apl") & "','" & dt.rows(i).item("descripcion_apl") & _
                            "','" & dt.rows(i).item("estilo_apl") & "','" & dt.rows(i).item("descripcion_tfu") & "')"
            Else
                _flag = True
                dtTF = New data.datatable
                oeAccesoDatos = New clsaccesodatos
                oeAccesoDatos.abrirconexion()
                dtTF = oeAccesoDatos.TraerDataTable("ConsultarAplicacionUsuario", "15", Session("id_per"), dt.rows(i).item("codigo_apl"), "")
                oeAccesoDatos.cerrarconexion()
                oeAccesoDatos = Nothing
            End If
            div.text += "<div class='col-md-4'>"
            div.text += "    <div class='thumbnail'>"
            If _flag Then
                div.text += "   <a class='dropdown-toggle' data-toggle='dropdown' href='#' >"
            Else
                div.text += "   <a onclick = """ & _vinculo & """ href='#' >"
            End If
            div.text += "        <div class='media'>"
            div.text += "            <div class='media-left'>"
            div.text += "                <img src='../../images/menus/" & dt.rows(i).item("icono_apl") & "' class='media-object' style='width:60px'>"
            div.text += "            </div>"
            div.text += "            <div class='media-body'>"
            div.text += "                <h6 class='media-heading'>" & dt.rows(i).item("descripcion_apl")
            If _flag Then
                div.text += "           <span class='caret'></span>"
            End If
            div.text += "               </h6>"
            If dt.rows(i).item("total_tfu") = 1 Then
                div.text += "                <p>" & dt.rows(i).item("descripcion_tfu") & "</p>"
            End If
            div.text += "            </div>"
            div.text += "        </div>"
            div.text += "        </a>"
            If _flag Then
                div.text += "   <ul class='dropdown-menu'>"
                If dtTF.rows.count > 0 Then
                    div.text += "<li class='dropdown-header'>Seleccione Perfil para ingresar: </li>"
                    div.text += "<li class='divider'></li>"
                    For y As Integer = 0 To dtTF.rows.count - 1
                        '_vinculo = "top.location.href='../abriraplicacion2020.asp?codigo_tfu=" & dtTF.rows(y).item("codigo_tfu") & "&amp;codigo_apl=" & dtTF.rows(y).item("codigo_apl") & _
                        '            "&amp;descripcion_apl=" & dt.rows(i).item("descripcion_apl") & "&amp;estilo_apl=" & dt.rows(i).item("estilo_apl") & "'"
                        _vinculo = "fc_AbrirFuncion('" & dtTF.rows(y).item("codigo_tfu") & "','" & dt.rows(i).item("codigo_apl") & "','" & dt.rows(i).item("descripcion_apl") & _
                                    "','" & dt.rows(i).item("estilo_apl") & "','" & dt.rows(i).item("descripcion_tfu") & "')"
                        div.text += "<li><a onclick = """ & _vinculo & """ href='#'><p>" & dtTF.rows(y).item("descripcion_tfu") & "</p></a></li>"
                    Next
                End If
                div.text += "   </ul>"
            End If
            div.text += "    </div>"
            div.text += "</div>"
            If _item = _col Then
                _item = 0
                div.text += "</div>"
            Else
                If i = dt.rows.count - 1 Then
                    div.text += "</div>"
                End If
            End If
            Me.divApl.controls.add(div)
        Next
    End Sub

#End Region

End Class
